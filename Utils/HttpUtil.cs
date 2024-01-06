using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
//using Oxetek.Helper;

namespace OxetekWeChatSDK.Utils
{
    public static class HttpUtil
    {
        private static HttpClient _client = null;
        private static HttpClientHandler _handler = null;
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private static ConcurrentDictionary<string, HttpClient> _clientManager = null;

        static HttpUtil()
        {
            Init();
        }

        public static void Init(CookieContainer cookies = null)
        {
            _logger.Debug("httpUtil ~");

            if (_client != null)
                _client.Dispose();

            _handler = new HttpClientHandler()
            {
                UseCookies = true,//自动附加cookie
            };

            if (cookies != null && cookies.Count > 0)
            {
                _handler.CookieContainer = cookies;
            }
#if !DEBUG
                _handler.UseProxy = false;
#endif
            _client = new HttpClient(_handler);

        }

        //public static HttpClientHandler HttpHandler => _handler;
        public static HttpClient Client => _client;

        /// <summary>
        /// 确保client一定存在，不存在则添加新的client
        /// </summary>
        /// <param name="host"></param>
        /// <param name="client"></param>
        private static void EnsureClient(string host, HttpClientHandler handler, out HttpClient client)
        {
            if (!_clientManager.TryGetValue(host, out client))
            {
                client = new HttpClient(handler);
                _clientManager.TryAdd(host, client);
            }
        }
        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="value"></param>
        public static void SetCookie(Uri uri, string value)
        {
            HttpClient client = null;
            HttpClientHandler handler = new HttpClientHandler()
            {
                UseCookies = true,
            };
            EnsureClient(uri.Host, handler, out client);
            client.DefaultRequestHeaders.Add("Cookie", value);
        }

        public static async Task<string> Get(string url)
        {
            _logger.Debug($"get url:{url}");

            var str = await _client.GetStringAsync(url);

            _logger.Debug($"return string:{str}");
            return str;
        }

        public static async Task<T> Get<T>(string url)
        {
            var res = await Get(url);
            return JsonConvert.DeserializeObject<T>(res);
        }

        public static async Task<Stream> GetStream(string url)
        {
            return await _client.GetStreamAsync(url);
        }

        public static async Task<byte[]> GetBytes(string url)
        {
            return await _client.GetByteArrayAsync(url);
        }

        public static async Task<string> Post(string url, object data, JsonSerializerSettings jssettings = null, bool isString = false)
        {
            _logger.Debug($"post url:{url}");

            using (MemoryStream ms = new MemoryStream())
            {
                var postData = string.Empty;

                isString = data.GetType() == typeof(string);

                if (isString)
                    postData = data.ToString();
                else
                    postData = JsonConvert.SerializeObject(data, jssettings);

                var bytes = Encoding.UTF8.GetBytes(postData);
                await ms.WriteAsync(bytes, 0, bytes.Length);
                ms.Seek(0, SeekOrigin.Begin);

                var hc = new StreamContent(ms);
                var postRes = await _client.PostAsync(url, hc);
                var str = await postRes.Content.ReadAsStringAsync();

                _logger.Debug($"post data:{postData}");
                _logger.Debug($"return string:{str}");

                return str;
            };
        }

        public static async Task<T> Post<T>(string url, object data, JsonSerializerSettings jssettings = null)
        {
            var res = await Post(url, data, jssettings);
            return JsonConvert.DeserializeObject<T>(res);
        }

        public static async Task<byte[]> Post(string url, byte[] postData)
        {
            _logger.Debug($"post url:{url}");

            using (MemoryStream ms = new MemoryStream())
            {
                await ms.WriteAsync(postData, 0, postData.Length);
                ms.Seek(0, SeekOrigin.Begin);

                var hc = new StreamContent(ms);
                var postRes = await _client.PostAsync(url, hc);
                var bytes = await postRes.Content.ReadAsByteArrayAsync();

                _logger.Debug($"post data:{postData}");
                _logger.Debug($"return bytes len:{bytes.Length}");

                return bytes;
            };
        }

        /// <summary>
        /// 发送上传文件请求
        /// </summary>
        /// <typeparam name="T">要返回的类</typeparam>
        /// <param name="url">post地址</param>
        /// <param name="fileData">文件数据</param>
        /// <param name="contentDisposition">post 的body 中Content-Disposition部分</param>
        /// <returns></returns>
        public static async Task<T> PostFile<T>(string url, byte[] fileData, ContentDispositionHeaderValue contentDisposition) where T : class
        {
            //var url = $"https://api.weixin.qq.com/cgi-bin/media/upload?access_token={accessToken}&type={type.ToString().ToLower()}";


            var boundary = "----" + DateTime.Now.Ticks.ToString("X");
            using (var content = new MultipartFormDataContent(boundary))
            {
                //var filePath = @"C:\Users\SAM\Desktop\57ef6de43e599558.jpg";
                //var image = await File.ReadAllBytesAsync(filePath);

                var media = new ByteArrayContent(fileData);
                //media.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");//body header部分

                //一定要加这个引号在内，否则 会出现 错误
                media.Headers.ContentDisposition = contentDisposition;
                //media.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                //{
                //    FileName = "\"57ef6de43e599558.jpg\"",
                //    Name = "\"meida\""
                //};
                //是否要先移除后才会不有重复的header
                //content.Headers.Remove("filename");
                content.Add(media);
                //content.Add(new StringContent("media"), "name");
                //content.Add(new StringContent("57ef6de43e599558.jpg"), "filename");

                //设置请求头
                //content.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                //需要覆盖Content-Type，因为content-type内的boundary参数值不需要引号，故替换原有的
                content.Headers.Remove("Content-Type");
                content.Headers.TryAddWithoutValidation("Content-Type", $"multipart/form-data; boundary={boundary}");

                using (var message = await _client.PostAsync(url, content))
                {
                    var res = await message.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(res);// input.ToObject<T>();
                }
            }

        }

        /// <summary>
        /// 带证书发送请求
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="data">发送数据</param>
        /// <param name="cert">证书路径</param>
        /// <param name="certPassword">证书密码</param>
        /// <param name="timeOut">超时</param>
        /// <returns>返回响应内容</returns>
        public static async Task<string> CertPostAsync(string url, string data, string cert, string certPassword, int timeOut = 10000)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                X509Certificate2 cer = new X509Certificate2(cert, certPassword, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);

                var httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                var resp = await RequestContent(url, () =>
                {

                    return new HttpRequestMessage()
                    {

                        Content = httpContent,
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(url),
                    };

                }, cer: cer);

                /*
                 * <xml>
<return_code><![CDATA[SUCCESS]]></return_code>
<return_msg><![CDATA[NO_AUTH]]></return_msg>
<mch_appid><![CDATA[wx0c5dd53114604111]]></mch_appid>
<mchid><![CDATA[1469900902]]></mchid>
<result_code><![CDATA[FAIL]]></result_code>
<err_code><![CDATA[NO_AUTH]]></err_code>
<err_code_des><![CDATA[产品权限验证失败,请查看您当前是否具有该产品的权限]]></err_code_des>
</xml>
*/
                return resp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="requestHeader">设置请求头</param>
        /// <param name="cookies">cookies</param>
        /// <param name="proxy"></param>
        /// <param name="cer"></param>
        /// <returns></returns>
        public static async Task<string> RequestContent(string url, Func<HttpRequestMessage> requestHeader = null, CookieContainer cookies = null, IWebProxy proxy = null, X509Certificate2 cer = null)
        {
            //HttpClientHandler handler = new HttpClientHandler();
            var uri = new Uri(url);
            _logger.Debug($"request url {url}");

            if (cookies != null && cookies.Count > 0)
            {
                _handler.UseCookies = true;
                _handler.CookieContainer = cookies;
                _handler.UseProxy = true;//如果设置成false，不使用代理，避免fildder等抓包工具
            }

            if (proxy != null)
            {
                _handler.UseProxy = true;
                _handler.Proxy = proxy;
            }

            if (cer != null)
            {
                _handler.ClientCertificates.Add(cer);
            }

            var requestMessage = requestHeader?.Invoke();
            if (requestHeader == null)
            {
                HttpContent httpContent = null;
                requestMessage = new HttpRequestMessage()
                {
                    RequestUri = uri,
                    Method = HttpMethod.Get,
                    Content = httpContent
                };
            }
            if (requestMessage.RequestUri == null)
            {
                requestMessage.RequestUri = uri;
            }

            //HttpClient client = null;
            //EnsureClient(uri.Host, handler, out client);

            _logger.Debug($"request baseAddress {_client.BaseAddress}");
            var response = await _client.SendAsync(requestMessage, System.Threading.CancellationToken.None);
            response.EnsureSuccessStatusCode();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                //GetLastError = request.StatusCode;
                //GetLastErrorText = request.RequestMessage.RequestUri.AbsoluteUri;

            }
            var isGzip = response.Content.Headers.ContentEncoding.Contains("gzip");
            var stream = await response.Content.ReadAsStreamAsync();

            if (isGzip)
            {
                stream = new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress);
            }
            using (var streamReader = new StreamReader(stream, Encoding.UTF8))
            {
                var resp = streamReader.ReadToEnd();

                response.Headers.TryGetValues("set-cookie", out IEnumerable<string> cookieValues);
                //ReceiveSetCookie = cookieValues;

                if (resp.Length > 1024)
                {
                    _logger.Debug($"resposne text {resp.Substring(0, 1024)}");
                }
                else
                {
                    _logger.Debug($"resposne text {resp}");
                }

                return resp;
            }
        }
    }
}
