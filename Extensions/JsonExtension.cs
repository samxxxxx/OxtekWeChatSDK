using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Extensions
{
    public static class JsonExtension
    {
        public static T ToObject<T>(this string str, Newtonsoft.Json.JsonSerializerSettings settings = null)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str, settings);
        }

        public static string ToJsonString(this object obj, Newtonsoft.Json.JsonSerializerSettings settings = null)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, settings);
        }
    }
}
