using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Response
{
    public class UnifiedOrderResponse
    {
        public string ReturnCode
        {
            get;
            set;
        }

        public string ReturnMsg
        {
            get;
            set;
        }

        /// <summary>
        /// 微信支付分配的终端设备号
        /// </summary>
        public string DeviceInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 交易类型:JSAPI、NATIVE、APP
        /// </summary>
        public string TradeType
        {
            get;
            set;
        }

        /// <summary>
        /// 微信生成的预支付ID，用于后续接口调用中使用
        /// </summary>    
        public string PrepayId
        {
            get;
            set;
        }

        /// <summary>
        /// trade_type为NATIVE时有返回，此参数可直接生成二维码展示出来进行扫码支付
        /// </summary>
        public string CodeUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 在H5支付时返回
        /// </summary>
        public string MWebUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 微信分配的公众账号ID（付款到银行卡接口，此字段不提供）
        /// </summary>
        public string AppId
        {
            get;
            set;
        }

        /// <summary>
        /// 微信支付分配的商户号
        /// </summary>
        public string MchId
        {
            get;
            set;
        }

        /// <summary>
        /// 子商户公众账号ID
        /// </summary>
        public string SubAppId
        {
            get;
            set;
        }

        /// <summary>
        /// 子商户号
        /// </summary>
        public string SubMchId
        {
            get;
            set;
        }

        /// <summary>
        /// 随机字符串，不长于32 位
        /// </summary>
        public string NonceStr
        {
            get;
            set;
        }

        /// <summary>
        /// 签名
        /// </summary>
        public string Sign
        {
            get;
            set;
        }

        /// <summary>
        /// SUCCESS/FAIL
        /// </summary>
        public string ResultCode
        {
            get;
            set;
        }

        public string ErrCode
        {
            get;
            set;
        }

        public string ErrCodeDes
        {
            get;
            set;
        }
    }
}
