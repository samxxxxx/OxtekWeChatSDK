using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Response
{
    public class UploadTempMediaResponse : ErrorMessage
    {
        public string Type { get; set; }
        public string Media_Id { get; set; }
        public int Created_At { get; set; }
    }
}
