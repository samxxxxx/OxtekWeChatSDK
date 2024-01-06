using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Response
{
    public class ErrorMessage
    {
        public string ErrCode { get; set; }
        public string ErrMsg { get; set; }
        public bool Success
        {
            get { return ErrCode == null || ErrCode == "0"; }
        }
    }
}
