using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OxetekWeChatSDK.Utils
{
    public static class RequestParamUtil
    {
        /// <summary>
        /// ascii升排序 会自动过滤sign参数
        /// </summary>
        /// <param name="uriParam"></param>
        /// <returns></returns>
        public static string ASCIIKeySort(string uriParam)
        {
            //var sortStr = $"sign=&appid={request.AppId}&mch_id={request.Mch_Id}&product_id={request.Product_Id}&time_stamp={request.Time_Stamp}&nonce_str={request.Nonce_Str}";
            var arr = uriParam.Split("&");
            var dic = new Dictionary<string, string>();
            foreach (var value in arr)
            {
                var item = value.Split("=");
                if (item.Length > 0)
                {
                    dic.Add(item[0], item[1]);
                }
            }

            var keys = dic.Keys.ToArray();
            Array.Sort(keys, string.CompareOrdinal);
            var str = new StringBuilder();
            foreach (var key in keys)
            {
                if (key != "sign")
                    str.Append($"{key}={dic[key]}&");
            }

            var newstr = str.ToString();
            return newstr.Substring(1, newstr.Length - 1);
        }
    }
}
