using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Request
{
    public class AccessTokenRequest
    {
        public AccessTokenRequest(string appId, string appSecret)
        {
            AppID = appId;
            AppSecret = appSecret;
        }
        /// <summary>
        /// APPID
        /// </summary>
        public string AppID { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string AppSecret { get; set; }
    }
}
