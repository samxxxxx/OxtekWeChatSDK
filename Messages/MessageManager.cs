using Newtonsoft.Json;
using OxetekWeChatSDK;
using OxetekWeChatSDK.Request;
using OxetekWeChatSDK.Response;
using OxetekWeChatSDK.Utils;
using OxetekWeChatSDK.Exceptions;
using OxetekWeChatSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OxetekWeChatSDK.Messages
{
    public class MessageManager
    {
        /// <summary>
        /// 回复图片消息
        /// </summary>
        /// <param name="toUserName">接收方帐号（收到的OpenID）</param>
        /// <param name="fromUserName">开发者微信号</param>
        /// <param name="mediaId">通过素材管理中的接口上传多媒体文件，得到的id。</param>
        /// <returns></returns>
        /// <![CDATA[https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140543]]>
        public static string ReplayImage(string toUserName, string fromUserName, string mediaId)
        {
            //注意：CDATA不能有空格
            var createTime = DateTimeUtil.ConvertToTimeStamp(DateTime.Now).ToString().Substring(0, 10);
            return $"<xml><ToUserName><![CDATA[{toUserName}]]></ToUserName><FromUserName><![CDATA[{fromUserName}]]></FromUserName><CreateTime>{createTime}</CreateTime><MsgType><![CDATA[image]]></MsgType><Image><MediaId><![CDATA[{mediaId}]]></MediaId></Image></xml>";
        }

        public static string ReplayText(string toUserName, string fromUserName, string text)
        {
            //注意：CDATA不能有空格
            var createTime = DateTimeUtil.ConvertToTimeStamp(DateTime.Now).ToString().Substring(0, 10);
            return $"<xml><ToUserName><![CDATA[{toUserName}]]></ToUserName><FromUserName><![CDATA[{fromUserName}]]></FromUserName><CreateTime>{createTime}</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{text}]]></Content></xml>";
        }

        /// <summary>
        /// 发送模板消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <remarks>如果token过期，需要使用oauth.getaccesstoken重新获取token<see cref="OAuth.GetAccessToken(AccessTokenRequest)"/></remarks>
        public static async Task<TemplateMessageErrorMessage> SendTemplateMessage(SendTemplateMessageRequest input)
        {
            if (OAuth.IsExpired)
            {
                throw new TokenExpiredException();
            }
            var url = input.BuildUrl();

            return await HttpUtil.Post<TemplateMessageErrorMessage>(url, input, new JsonSerializerSettings()
            {
                ContractResolver = new LowerPropertyNameJsonSerializer()
            });
        }

    }
}
