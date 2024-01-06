using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Request
{
    public class JsCode2SessionRequest : BasicRequest
    {

        public string Code { get; set; }
        public string AppId { get; set; }
        public string AppSecret { get; set; }

        public override string BuildUrl()
        {
            return $"https://api.weixin.qq.com/sns/jscode2session?code={Code}&appid={AppId}&secret={AppSecret}&js_code={Code}&grant_type=authorization_code";
        }
    }
}
