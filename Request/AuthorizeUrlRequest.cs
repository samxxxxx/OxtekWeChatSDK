using OxetekWeChatSDK.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Request
{
    /// <summary>
    /// 用户同意授权，获取code
    /// </summary>
    public class AuthorizeUrlRequest : BasicRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId">公众号的唯一标识</param>
        /// <param name="redirect_Uri">授权后重定向的回调链接地址， 请使用 urlEncode 对链接进行处理</param>
        /// <param name="response_Type">返回类型，请填写code</param>
        /// <param name="scope">应用授权作用域，snsapi_base （不弹出授权页面，直接跳转，只能获取用户openid），snsapi_userinfo （弹出授权页面，可通过openid拿到昵称、性别、所在地。并且， 即使在未关注的情况下，只要用户授权，也能获取其信息 ）</param>
        /// <param name="state">重定向后会带上state参数，开发者可以填写a-zA-Z0-9的参数值，最多128字节</param>
        public AuthorizeUrlRequest(string appId, string redirect_Uri, string response_Type = "code", AuthorizeScope scope = AuthorizeScope.snsapi_base, string state = null)
        {
            AppId = appId;
            Redirect_Uri = redirect_Uri;
            Response_Type = response_Type;
            Scope = scope;
            State = state;
        }
        /// <summary>
        /// 公众号的唯一标识
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 授权后重定向的回调链接地址， 请使用 urlEncode 对链接进行处理
        /// </summary>
        public string Redirect_Uri { get; set; }
        /// <summary>
        /// 返回类型，请填写code
        /// </summary>
        public string Response_Type { get; set; }
        /// <summary>
        /// 应用授权作用域，snsapi_base （不弹出授权页面，直接跳转，只能获取用户openid），
        /// snsapi_userinfo （弹出授权页面，可通过openid拿到昵称、性别、所在地。并且， 即使在未关注的情况下，只要用户授权，也能获取其信息 ）
        /// </summary>
        public AuthorizeScope Scope { get; set; }
        /// <summary>
        /// 重定向后会带上state参数，开发者可以填写a-zA-Z0-9的参数值，最多128字节
        /// </summary>
        public string State { get; set; }

        public override string BuildUrl()
        {
            var url = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={AppId}&redirect_uri={Redirect_Uri}&response_type={Response_Type}&scope={Scope.ToString()}&state={State ?? ""}#wechat_redirect";
            return url;
        }
    }
}
