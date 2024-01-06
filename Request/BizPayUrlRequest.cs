using OxetekWeChatSDK.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Request
{
    public class BizPayUrlRequest
    {
        public BizPayUrlRequest()
        {
            Nonce_Str = Guid.NewGuid().ToString("N").ToLower();
            Time_Stamp = DateTimeUtil.ConvertToTimeStamp(DateTime.Now).ToString().Substring(0, 10);

        }
        /// <summary>
        /// 是 公众账号ID 微信分配的公众账号ID
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 是 商户号 微信支付分配的商户号
        /// </summary>
        public string Mch_Id { get; set; }
        /// <summary>
        /// 是 时间戳 系统当前时间，定义规则详见时间戳 长度10位 部分系统取到的值为毫秒级，需要转换成秒(10位数字)。
        /// </summary>
        public string Time_Stamp { get; set; }
        /// <summary>
        /// 是 随机字符串 随机字符串，不长于32位。推荐随机数生成算法
        /// </summary>
        public string Nonce_Str { get; set; }
        /// <summary>
        /// 是 商品ID 商户定义的商品id 或者订单号
        /// </summary>
        public string Product_Id { get; set; }
        /// <summary>
        /// 是 	签名，详见签名生成算法
        /// </summary>
        public string Sign { get; set; }

    }
}
