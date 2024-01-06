using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Response
{
    public class AccessTokenResponse : ErrorMessage
    {
        public string Access_Token { get; set; }
        public double Expires_In { get; set; }
    }
}
