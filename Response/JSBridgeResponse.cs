using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Response
{
    public class JSBridgeResponse : ErrorMessage
    {
        public string Ticket { get; set; }
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }
    }
}
