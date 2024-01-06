using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK
{
    /// <summary>
    /// 将属性名全部小写
    /// </summary>
    public class LowerPropertyNameJsonSerializer : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}
