using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.ThirdParty.Request
{
    public class ComponentAccessTokenRequest : ComponentBaseRequest
    {
        public string Component_AppSecret { get; set; }
        public string Component_Verify_Ticket { get; set; }
    }
}
