﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.ThirdParty.Request
{
    public class SetAuthorizerOptionRequest : ComponentBaseRequest
    {
        public string Authorizer_AppId { get; set; }
        public string Option_Name { get; set; }
        public string Option_Value { get; set; }
    }
}
