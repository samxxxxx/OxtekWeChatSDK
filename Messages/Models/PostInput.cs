using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Messages.Models
{
    /// <summary>
    /// signature=93ffff71da09178a451dceb0303e63d20e5557d6&timestamp=1548663383&nonce=658615278&openid=oz4Xnvl6tI1WokLnyIj6GM5M0pRc
    /// 
    /// 第三方平台
    /// appid=/wx0c5dd53114604111&signature=218e95c4ec152f4f5f158f6a8d300f61e75e04ff&timestamp=1550978255&nonce=2105967766&openid=oz4Xnvl6tI1WokLnyIj6GM5M0pRc&encrypt_type=aes&msg_signature=391905e138928869004f065725ff5b55d1086ac9
    /// </summary>
    public class PostInput : BasicData
    {
        public string OpenId { get; set; }
        public string TimeStamp { get; set; }
        public string Signature { get; set; }
        public string Nonce { get; set; }
    }
}
