using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Response
{
    public class JsCode2SessionReponse
    {
        /*
         *  返回结果:
            "openid":"xxxxxx",
            "session_key":"xxxxx",
            "unionid":"xxxxx",
            "errcode":0,
            "errmsg":"xxxxx"
        */

        public string openid { get; set; }
        public string session_key { get; set; }
        public string unionid { get; set; }
        public string errcode { get; set; }
        public string errmsg { get; set; }
    }
}
