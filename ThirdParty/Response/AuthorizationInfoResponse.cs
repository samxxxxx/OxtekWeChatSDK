using OxetekWeChatSDK.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.ThirdParty.Response
{

    public class AuthorizationInfoResponse : ErrorMessage
    {
        /// <summary>
        /// 授权信息
        /// </summary>
        public AuthorizationInfo Authorization_Info { get; set; }
    }
    /// <summary>
    /// 授权信息
    /// </summary>
    public class AuthorizationInfo
    {
        /// <summary>
        /// 授权方appid
        /// </summary>
        public string Authorizer_AppId { get; set; }
        /// <summary>
        /// 授权方接口调用凭据（在授权的公众号或小程序具备API权限时，才有此返回值），也简称为令牌
        /// </summary>
        public string Authorizer_Access_Token { get; set; }
        /// <summary>
        /// 有效期（在授权的公众号或小程序具备API权限时，才有此返回值）
        /// </summary>
        public int Expires_In { get; set; }
        /// <summary>
        /// 接口调用凭据刷新令牌（在授权的公众号具备API权限时，才有此返回值），刷新令牌主要用于第三方平台获取和刷新已授权用户的access_token，只会在授权时刻提供，请妥善保存。 一旦丢失，只能让用户重新授权，才能再次拿到新的刷新令牌
        /// </summary>
        public string Authorizer_Refresh_Token { get; set; }
    }
}
