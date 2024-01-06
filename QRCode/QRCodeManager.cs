using Newtonsoft.Json;
using OxetekWeChatSDK.Request;
using OxetekWeChatSDK.Response;
using OxetekWeChatSDK.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OxetekWeChatSDK.QRCode
{
    public class QRCodeManager
    {
        /// <summary>
        /// 创建二维码ticket
        /// 
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <![CDATA[https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1443433542]]>
        public static async Task<QRCodeTicketResponse> CreateTicket(string accessToken, QRCodeRequest request)
        {
            var url = $"https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={accessToken}";

            return await HttpUtil.Post<QRCodeTicketResponse>(url, request, new JsonSerializerSettings()
            {
                ContractResolver = new LowerPropertyNameJsonSerializer()
            });
        }

        /// <summary>
        /// 下载二维码
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="saveFilePath"></param>
        /// <returns>保存二维码路径</returns>
        public static async Task<string> Download(string ticket, string saveFilePath)
        {
            var url = $"https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={ticket}";
            var data = await HttpUtil.GetBytes(url);
            if (data.Length > 0)
            {
                await FileUtil.WriteFile(data, saveFilePath);
                return saveFilePath;
            }
            return string.Empty;
        }

    }
}
