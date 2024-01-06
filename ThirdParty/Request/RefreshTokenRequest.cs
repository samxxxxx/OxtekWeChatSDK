using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.ThirdParty.Request
{
    public class RefreshTokenRequest : ComponentBaseRequest
    {
        public string Authorizer_AppId { get; set; }
        public string Authorizer_Refresh_Token { get; set; }
    }
}
