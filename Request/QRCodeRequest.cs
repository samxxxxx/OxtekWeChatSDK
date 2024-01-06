using OxetekWeChatSDK.Models.QRCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Request
{
    public class QRCodeRequest
    {
        /// <summary>
        /// 该二维码有效时间，以秒为单位。 最大不超过2592000（即30天），此字段如果不填，则默认有效期为30秒。
        /// </summary>
        public int Expire_Seconds { get; set; }
        /// <summary>
        /// 二维码类型，QR_SCENE为临时的整型参数值，QR_STR_SCENE为临时的字符串参数值，QR_LIMIT_SCENE为永久的整型参数值，QR_LIMIT_STR_SCENE为永久的字符串参数值
        /// 值请参考：<see cref="QRCode.ActionNameType"/>
        /// </summary>
        public string Action_Name { get; set; }
        /// <summary>
        /// 二维码详细信息
        /// </summary>
        public QRCodeActionInfo Action_Info { get; set; }
    }
}
