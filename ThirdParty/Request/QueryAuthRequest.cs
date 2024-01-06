using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.ThirdParty.Request
{
    public class QueryAuthRequest : ComponentBaseRequest
    {
        public string Authorization_Code { get; set; }
    }
}
