using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OxetekWeChatSDK.ThirdParty;
using OxetekWeChatSDK.Response;
using OxetekWeChatSDK.Utils;

namespace OxetekWeChatSDK.JSBridge
{
    public class JSBridgeManager
    {
        private static NLog.ILogger logger = NLog.LogManager.GetCurrentClassLogger();
        private static JSBridgeResponse _jsTicket;
        private static DateTime lastUpdateTime;
        private static bool IsExpired
        {
            get
            {
                if (lastUpdateTime == DateTime.MinValue)
                    return true;
                if (_jsTicket == null)
                    return true;
                return lastUpdateTime.AddSeconds(int.Parse(_jsTicket.ExpiresIn)) <= DateTime.Now;
            }
        }
        /// <summary>
        /// 生成签名之前必须先了解一下jsapi_ticket，jsapi_ticket是公众号用于调用微信JS接口的临时票据。正常情况下，jsapi_ticket的有效期为7200秒，通过access_token来获取。由于获取jsapi_ticket的api调用次数非常有限，频繁刷新jsapi_ticket会导致api调用受限，影响自身业务，开发者必须在自己的服务全局缓存jsapi_ticket 。
        ///        参考以下文档获取access_token（有效期7200秒，开发者必须在自己的服务全局缓存access_token）：<![CDATA[https://developers.weixin.qq.com/doc/offiaccount/Basic_Information/Get_access_token.html]]>
        ///用第一步拿到的access_token 采用http GET方式请求获得jsapi_ticket（有效期7200秒，开发者必须在自己的服务全局缓存jsapi_ticket）：<![CDATA[https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=ACCESS_TOKEN&type=jsapi]]>
        /// 
        /// <see cref="https://developers.weixin.qq.com/doc/offiaccount/OA_Web_Apps/JS-SDK.html#54"/>
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static async Task<JSBridgeResponse> GetJSTicket(string accessToken, string type = "jsapi")
        {
            if (!IsExpired)
            {
                return _jsTicket;
            }

            var url = $"https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={accessToken}&type={type}";

            var rsp = await HttpUtil.Get<JSBridgeResponse>(url);
            if (rsp.ErrCode == "0")
            {
                lastUpdateTime = DateTime.Now;
                _jsTicket = rsp;
            }
            return _jsTicket;
        }

        /// <summary>
        /// SHA1签名jsticket
        /// </summary>
        /// <param name="noncestr"></param>
        /// <param name="jsapi_ticket"></param>
        /// <param name="timestamp"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetJSTicketSign(string noncestr, string jsapi_ticket, string timestamp, string url)
        {
            var hash = new Hashtable()
            {
                { "jsapi_ticket",jsapi_ticket},
                { "noncestr",noncestr},
                { "timestamp",timestamp},
                { "url",url},
            };
            StringBuilder stringBuilder = new StringBuilder();
            ArrayList arrayList = new ArrayList(hash.Keys);
            arrayList.Sort();
            foreach (object item in arrayList)
            {
                if (hash[item] != null)
                {
                    string text = (string)hash[item];
                    if (stringBuilder.Length == 0)
                    {
                        stringBuilder.Append(item + "=" + text);
                    }
                    else
                    {
                        stringBuilder.Append("&" + item + "=" + text);
                    }
                }
            }

            logger.Debug($"GetJSTicketSign:{stringBuilder.ToString()}");
            return Cryptography.Cryptography.GetSha1(stringBuilder.ToString());
        }

    }
}
