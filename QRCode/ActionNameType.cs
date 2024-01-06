using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.QRCode
{
    /// <summary>
    /// 二维码类型，QR_SCENE为临时的整型参数值，QR_STR_SCENE为临时的字符串参数值，QR_LIMIT_SCENE为永久的整型参数值，QR_LIMIT_STR_SCENE为永久的字符串参数值
    /// </summary>
    public class ActionNameType
    {
        //二维码类型，QR_SCENE为临时的整型参数值，QR_STR_SCENE为临时的字符串参数值，QR_LIMIT_SCENE为永久的整型参数值，QR_LIMIT_STR_SCENE为永久的字符串参数值
        //string QR_SCENE ="";
        /// <summary>
        /// QR_SCENE为临时的整型参数值
        /// </summary>
        public static string QR_SCENE => "QR_SCENE";
        /// <summary>
        /// QR_STR_SCENE为临时的字符串参数值
        /// </summary>
        public static string QR_STR_SCENE => "QR_STR_SCENE";
        /// <summary>
        /// QR_LIMIT_SCENE为永久的整型参数值
        /// </summary>
        public static string QR_LIMIT_SCENE => "QR_LIMIT_SCENE";
        /// <summary>
        /// QR_LIMIT_STR_SCENE为永久的字符串参数值
        /// </summary>
        public static string QR_LIMIT_STR_SCENE => "QR_LIMIT_STR_SCENE";

    }
}
