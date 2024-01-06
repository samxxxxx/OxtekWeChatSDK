using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Request
{
    //https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140842
    /// <summary>
    /// 第二步：通过code换取网页授权access_token
    /// </summary>
    public class AuthorizeAccessTokenRequest : BasicRequest
    {
        public AuthorizeAccessTokenRequest(string appId, string secret, string code, string grant_Type = "authorization_code")
        {
            AppId = appId;
            Secret = secret;
            Code = code;
            Grant_Type = grant_Type;
        }
        /// <summary>
        /// 公众号的唯一标识
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 公众号的appsecret
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// 填写第一步获取的code参数
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 填写为authorization_code
        /// </summary>
        public string Grant_Type { get; set; }
        public override string BuildUrl()
        {
            return $"https://api.weixin.qq.com/sns/oauth2/access_token?appid={AppId}&secret={Secret}&code={Code}&grant_type={Grant_Type}";
        }
    }
}
