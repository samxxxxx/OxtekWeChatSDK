using OxetekWeChatSDK.Models.CustomService;
using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Request
{
    public class SendMessageRequest
    {
        public SendMessageRequest()
        {
            Text = new TextMessage();
        }
        /// <summary>
        /// 接收方的openid
        /// </summary>
        public string ToUser { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; set; }
        public TextMessage Text { get; set; }
    }
}
