using Newtonsoft.Json;
using OxetekWeChatSDK.Response;
using OxetekWeChatSDK.Utils;
using OxetekWeChatSDK.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OxetekWeChatSDK.CustomService
{
    public class CustomServiceManager
    {
        /// <summary>
        /// 客服接口-发送文本消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="toUserOpenId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static async Task<bool> SendTextMessage(string accessToken, string toUserOpenId, string content)
        {
            var data = new SendMessageRequest()
            {
                MsgType = "text",
                ToUser = toUserOpenId,
                Text = new Models.CustomService.TextMessage()
                {
                    Content = content
                }
            };
            var url = $"https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={accessToken}";
            var res = await HttpUtil.Post<ErrorMessage>(url, data, new JsonSerializerSettings()
            {
                ContractResolver = new LowerPropertyNameJsonSerializer()
            });

            return res.Success;
        }
    }
}
