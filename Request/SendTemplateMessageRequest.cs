using OxetekWeChatSDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Request
{
    public class SendTemplateMessageRequest : BasicRequest
    {
        /// <summary>
        /// 接收者openid
        /// </summary>
        public string ToUser { get; set; }
        /// <summary>
        /// 模板ID
        /// </summary>
        public string Template_Id { get; set; }
        /// <summary>
        /// 模板跳转链接（海外帐号没有跳转能力）
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 模板数据
        /// </summary>
        public object Data { get; set; }

        public override string BuildUrl()
        {
            return $"https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={OAuth.AccessToken}";
        }
    }
}
