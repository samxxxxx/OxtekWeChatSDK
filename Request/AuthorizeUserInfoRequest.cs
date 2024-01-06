using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Request
{
    /// <summary>
    /// 第四步：拉取用户信息(需scope为 snsapi_userinfo)
    /// </summary>
    public class AuthorizeUserInfoRequest : BasicRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="access_Token">网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同</param>
        /// <param name="openId">用户的唯一标识</param>
        /// <param name="lang">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语</param>
        public AuthorizeUserInfoRequest(string access_Token, string openId, string lang = "zh_CN")
        {
            Access_Token = access_Token;
            OpenId = openId;
            Lang = lang;
        }
        /// <summary>
        /// 网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
        /// </summary>
        public string Access_Token { get; set; }
        /// <summary>
        /// 用户的唯一标识
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语
        /// </summary>
        public string Lang { get; set; }

        public override string BuildUrl()
        {
            return $"https://api.weixin.qq.com/sns/userinfo?access_token={Access_Token}&openid={OpenId}&lang={Lang}";
        }
    }
}
