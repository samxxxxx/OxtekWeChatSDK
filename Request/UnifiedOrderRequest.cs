using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Request
{
    public class UnifiedOrderRequest
    {
        public UnifiedOrderRequest()
        {
            Fee_Type = "CNY";
            Sign_Type = "MD5";
            Nonce_Str = Guid.NewGuid().ToString("N").ToLower();
            Device_Info = "WEB";
        }
        /// <summary>
        /// 是 公众账号ID，微信支付分配的公众账号ID（企业号corpid即为此appId）
        /// </summary>
        public string AppId { get; set; }

        /// <summary>   
        /// 是 商户号 微信支付分配的商户号
        /// </summary>
        public string Mch_Id { get; set; }
        /// <summary>
        /// 否 设备号 自定义参数，可以为终端设备号(门店号或收银设备ID)，PC网页或公众号内支付可以传"WEB"
        /// </summary>
        public string Device_Info { get; set; }
        /// <summary>
        /// 是 随机字符串 随机字符串，长度要求在32位以内。推荐随机数生成算法
        /// </summary>
        public string Nonce_Str { get; set; }
        /// <summary>
        /// 是 签名	 通过签名算法计算得出的签名值，详见签名生成算法
        /// </summary>
        public string Sign { get; set; }
        /// <summary>
        /// 是 签名类型 签名类型，默认为MD5，支持HMAC-SHA256和MD5。
        /// </summary>
        public string Sign_Type { get; set; }
        /// <summary>
        /// 是 商品描述 商品简单描述，该字段请按照规范传递，具体请见参数规定
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 否 商品详情 商品详细描述，对于使用单品优惠的商户，改字段必须按照规范上传，详见“单品优惠参数说明”
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 否 附加数据 附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用。
        /// </summary>
        public string Attach { get; set; }
        /// <summary>
        /// 是 商户订单号 商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|* 且在同一个商户号下唯一。详见商户订单号
        /// </summary>
        public string Out_Trade_No { get; set; }
        /// <summary>
        /// 否 标价币种 符合ISO 4217标准的三位字母代码，默认人民币：CNY，详细列表请参见货币类型
        /// </summary>
        public string Fee_Type { get; set; }
        /// <summary>
        /// 是 标价金额 订单总金额，单位为分，详见支付金额
        /// </summary>
        public string Total_Fee { get; set; }
        /// <summary>
        /// 是 终端IP 支持IPV4和IPV6两种格式的IP地址。调用微信支付API的机器IP
        /// </summary>
        public string Spbill_Create_Ip { get; set; }
        /// <summary>
        /// 否 交易起始时间 订单生成时间，格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010。其他详见时间规则
        /// </summary>
        public string Time_Start { get; set; }
        /// <summary>
        /// 否 交易结束时间 订单失效时间，格式为yyyyMMddHHmmss，如2009年12月27日9点10分10秒表示为20091227091010。订单失效时间是针对订单号而言的，由于在请求支付的时候有一个必传参数prepay_id只有两小时的有效期，所以在重入时间超过2小时的时候需要重新请求下单接口获取新的prepay_id。其他详见时间规则        建议：最短失效时间间隔大于1分钟
        /// </summary>
        public string Time_Expire { get; set; }
        /// <summary>
        /// 否 订单优惠标记 订单优惠标记，使用代金券或立减优惠功能时需要的参数，说明详见代金券或立减优惠
        /// </summary>
        public string Goods_Tag { get; set; }
        /// <summary>
        /// 是 通知地址	异步接收微信支付结果通知的回调地址，通知url必须为外网可访问的url，不能携带参数。
        /// </summary>
        public string Notify_Url { get; set; }
        /// <summary>
        /// 是 JSAPI -JSAPI支付 NATIVE -Native支付 APP -APP支付 说明详见参数规定交易类型
        /// </summary>
        public string Trade_Type { get; set; }
        /// <summary>
        /// 否 商品ID trade_type=NATIVE时，此参数必传。此参数为二维码中包含的商品ID，商户自行定义。
        /// </summary>
        public string Product_Id { get; set; }
        /// <summary>
        /// 否 指定支付方式	上传此参数no_credit--可限制用户不能使用信用卡支付
        /// </summary>
        public string Limit_Pay { get; set; }
        /// <summary>
        /// 否 用户标识	trade_type=JSAPI时（即JSAPI支付），此参数必传，此参数为微信用户在商户对应appid下的唯一标识。openid如何获取，可参考【获取openid】。企业号请使用【企业号OAuth2.0接口】获取企业号内成员userid，再调用【企业号userid转openid接口】进行转换
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 否 电子发票入口开放标识 Y，传入Y时，支付成功消息和支付详情页将出现开票入口。需要在微信支付商户平台或微信公众平台开通电子发票功能，传此字段才可生效
        /// </summary>
        public string Receipt { get; set; }
        /// <summary>
        /// 否 场景信息 该字段常用于线下活动时的场景信息上报，支持上报实际门店信息，商户也可以按需求自己上报相关信息。该字段为JSON对象数据，对象格式为{"store_info":{"id": "门店ID","name": "名称","area_code": "编码","address": "地址" }} ，字段详细说明请点击行前的+展开
        /// </summary>
        public string Scene_Info { get; set; }
    }
}
