using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Messages
{
    public class BasicData
    {
        public string HostDomain { get; set; }
        public string Token { set; get; }
        public string EncodingAESKey { set; get; }
        public string AppID { set; get; }
        public string AppSecret { set; get; }

        /// <summary>
        /// 公众号消息校验Token: 开发者在代替公众号接收到消息时，用此Token来校验消息。用法与普通公众号token一致
        /// </summary>
        public string ThirdPartyToken { get; set; }
        /// <summary>
        /// 第三方平台appsecret
        /// </summary>
        public string ThirdPartyAppSecret { get; set; }
        /// <summary>
        /// 公众号消息加解密Key:在代替公众号收发消息过程中使用。必须是长度为43位的字符串，只能是字母和数字。用法与普通公众号symmetric_key一致
        /// </summary>
        public string ThirdPartyAESKey { get; set; }
        public string ThirdPartyAppId { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { get; set; }
        /// <summary>
        /// 商户密钥Key
        /// </summary>
        public string MchKey { get; set; }
        /// <summary>
        /// 付款后通知的地址
        /// </summary>
        public string NotifyUrl { get; set; }
    }
}
