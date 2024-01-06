using OxetekWeChatSDK.Response;
using OxetekWeChatSDK.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OxetekWeChatSDK.Material
{
    public class MaterialManager
    {
        /// <summary>
        /// 新增临时素材
        /// </summary>
        /// <param name="accessToken">访问token</param>
        /// <param name="type">上传文件类型</param>
        /// <param name="uploadFilePath">文件路径</param>
        /// <returns></returns>
        public static async Task<UploadTempMediaResponse> UploadTempMedia(string accessToken, MediaType type, string uploadFilePath)
        {
            var url = $"https://api.weixin.qq.com/cgi-bin/media/upload?access_token={accessToken}&type={type.ToString().ToLower()}";
            var data = await File.ReadAllBytesAsync(uploadFilePath);
            var fileName = Path.GetFileName(uploadFilePath);

            return await HttpUtil.PostFile<UploadTempMediaResponse>(url, data, new ContentDispositionHeaderValue("attachment")
            {
                FileName = "\"" + fileName + "\"",
                Name = "\"meida\""
            });


            //using (var client = new HttpClient())
            //{
            //    var boundary = "----" + DateTime.Now.Ticks.ToString("X");
            //    using (var content = new MultipartFormDataContent(boundary))
            //    {
            //        var filePath = @"C:\Users\SAM\Desktop\57ef6de43e599558.jpg";
            //        var image = await File.ReadAllBytesAsync(filePath);

            //        var media = new ByteArrayContent(image);
            //        media.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");//body header部分

            //        一定要加这个引号在内，否则 会出现 错误
            //        media.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            //        {
            //            FileName = "\"57ef6de43e599558.jpg\"",
            //            Name = "\"meida\""
            //        };
            //        是否要先移除后才会不有重复的header
            //        content.Headers.Remove("filename");
            //        content.Add(media);
            //        content.Add(new StringContent("media"), "name");
            //        content.Add(new StringContent("57ef6de43e599558.jpg"), "filename");

            //        设置请求头
            //        content.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
            //        需要覆盖Content-Type，因为content-type内的boundary参数值不需要引号，故替换原有的
            //        content.Headers.Remove("Content-Type");
            //        content.Headers.TryAddWithoutValidation("Content-Type", $"multipart/form-data; boundary={boundary}");

            //        using (var message = await client.PostAsync(url, content))
            //        {
            //            var input = await message.Content.ReadAsStringAsync();

            //            return input.ToObject<UploadTempMediaResponse>();
            //            return !string.IsNullOrWhiteSpace(input) ? Regex.Match(input, @"http://\w*\.directupload\.net/images/\d*/\w*\.[a-z]{3}").Value : null;
            //        }
            //    }
            //}

            //return true;
        }
    }
}
