using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OxetekWeChatSDK.Request;
using OxetekWeChatSDK.Response;
using OxetekWeChatSDK.Utils;

namespace OxetekWeChatSDK.MiniPro
{
    public class OpenApi
    {

        public static Task<JsCode2SessionReponse> Code2Session(JsCode2SessionRequest request)
        {
            var url = request.BuildUrl();
            return HttpUtil.Get<JsCode2SessionReponse>(url);
        }

    }
}
