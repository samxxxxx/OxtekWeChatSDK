using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OxetekWeChatSDK.Response;
using OxetekWeChatSDK.Request;
using OxetekWeChatSDK.Utils;

namespace OxetekWeChatSDK
{
    public class OAuth
    {
        private static NLog.ILogger logger = NLog.LogManager.GetCurrentClassLogger();
        public static DateTime LastUpdate { private set; get; }
        /// <summary>
        /// 检查签名是否正确:
        /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421135319
        /// </summary>
        /// <returns></returns>
        public static bool CheckSignature(string signature, string timestamp, string nonce, string token)
        {
            var arr = new[] { token, timestamp, nonce }.OrderBy(z => z).ToArray();
            var arrString = string.Join("", arr);
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));
            StringBuilder enText = new StringBuilder();
            foreach (var b in sha1Arr)
            {
                enText.AppendFormat("{0:x2}", b);
            }

            return signature == enText.ToString();
        }

        /// <summary>
        /// token是否过期
        /// </summary>
        /// <returns></returns>
        public static bool IsExpired
        {
            get
            {
                //小于当前时间表示过期
                var expired = LastUpdate.AddSeconds(ExpiresIn) <= DateTime.Now;
                logger.Debug($"{expired}, expiresIn {ExpiresIn}, lastUpdate {LastUpdate.ToString("yyyy-MM-dd HH:mm:ss")} expired time {LastUpdate.AddSeconds(ExpiresIn).ToString("yyyy-MM-dd HH:mm:ss")}, datetime now {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                return expired;
            }
        }

        public static double ExpiresIn
        {
            private set; get;
        }

        private static string _accessToken;

        public static string AccessToken
        {
            get
            {
                if (IsExpired)
                {
                    throw new Exception("session key has expired");
                }

                return _accessToken;
            }
        }

        /// <summary>
        /// 获取访问token
        /// 参数:
        /// appid 应用id *必填
        /// appsecret 密钥 *必填
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<string> GetAccessToken(AccessTokenRequest data)
        {
            //https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET
            var url = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={data.AppID}&secret={data.AppSecret}";

            if (!IsExpired)
            {
                return AccessToken;
            }

            var resEntity = await HttpUtil.Get<AccessTokenResponse>(url);

            if (resEntity.Success)
            {
                LastUpdate = DateTime.Now;
                ExpiresIn = resEntity.Expires_In;
                _accessToken = resEntity.Access_Token;
            }

            logger.Debug($"update last time {Newtonsoft.Json.JsonConvert.SerializeObject(resEntity)}");

            return resEntity.Access_Token;
        }

        /// <summary>
        /// 设置token为过期
        /// </summary>
        public static void SetExpired()
        {
            LastUpdate = DateTime.MinValue;
        }
        /////https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140842
        //public static async Task<string> RefreshToken(BasicData data)
        //{
        //    //https://api.weixin.qq.com/sns/oauth2/refresh_token?appid=APPID&grant_type=refresh_token&refresh_token=REFRESH_TOKEN

        //    var url = $"https://api.weixin.qq.com/sns/oauth2/refresh_token?appid=APPID&grant_type=refresh_token&refresh_token=REFRESH_TOKEN";
        //}


        public static string GetAuthorizeUrl(AuthorizeUrlRequest request)
        {
            return request.BuildUrl();
        }

        /// <summary>
        /// 获取用于访问用户信息的accesstoken
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<AuthorizeAccessTokenResponse> GetAuthorizeToken(AuthorizeAccessTokenRequest request)
        {
            var url = request.BuildUrl();
            return await HttpUtil.Get<AuthorizeAccessTokenResponse>(url);
        }

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<AuthorizeUserInfoReponse> GetAuthorizeUserInfo(AuthorizeUserInfoRequest request)
        {
            var url = request.BuildUrl();
            return await HttpUtil.Get<AuthorizeUserInfoReponse>(url);
        }
    }
}
