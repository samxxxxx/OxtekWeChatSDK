using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Models.QRCode
{
    public class QRCodeScene
    {
        /// <summary>
        /// 场景值ID（字符串形式的ID），字符串类型，长度限制为1到64
        /// </summary>
        public string Scene_Str { get; set; }
        /// <summary>
        /// 场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000（目前参数只支持1--100000）
        /// </summary>
        public uint Scene_Id { get; set; }
    }
}
