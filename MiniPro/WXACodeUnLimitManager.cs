
#region Version Info
/**************************************************************** 
**************************************************************** 
* 作    者：SAM
* 邮    箱:	support@oxetek.com
* CLR 版本：4.0.30319.42000
* 创建时间：2020-1-3 14:20:02
* 当前版本：1.0.0.0
* 
* 描述说明： 
* 
* 修改历史： 
* timestamp: c0dd3140-3fb7-43e6-bc26-84d19ef2ba02
****************************************************************** 
* Copyright @ SAM 2020 All rights reserved**********************/
#endregion

using Newtonsoft.Json;
using OxetekWeChatSDK.Request;
using OxetekWeChatSDK.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OxetekWeChatSDK.MiniPro
{
    /// <summary>
    /// 获取小程序码，适用于需要的码数量极多的业务场景。通过该接口生成的小程序码，永久有效，数量暂无限制。 更多用法详见 获取二维码。
    /// </summary>
    public class WXACodeUnLimitManager
    {
        /// <summary>
        /// 获取小程序码 适用于需要的码数量极多的业务场景。通过该接口生成的小程序码，永久有效，数量暂无限制。 更多用法详见 获取二维码。
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<byte[]> GetWXACodeUnLimit(string accessToken, WXACodeUnLimitRequest request)
        {
            var url = $"https://api.weixin.qq.com/wxa/getwxacodeunlimit?access_token={accessToken}";
            var jsonData = JsonConvert.SerializeObject(request, new JsonSerializerSettings()
            {
                ContractResolver = new LowerPropertyNameJsonSerializer()
            });
            var bytes = Encoding.UTF8.GetBytes(jsonData);

            var data = await HttpUtil.Post(url, bytes);

            return data;
        }

    }
}
