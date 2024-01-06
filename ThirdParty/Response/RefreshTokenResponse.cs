using OxetekWeChatSDK.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.ThirdParty.Response
{
    public class RefreshTokenResponse : ErrorMessage
    {
        /// <summary>
        /// 第三方平台appid
        /// </summary>
        public string Authorizer_Access_Token { get; set; }
        /// <summary>
        /// 有效期，为2小时
        /// </summary>
        public int Expires_In { get; set; }

        /// <summary>
        /// 刷新令牌
        /// </summary>
        public string Authorizer_Refresh_Token { get; set; }
    }
}
