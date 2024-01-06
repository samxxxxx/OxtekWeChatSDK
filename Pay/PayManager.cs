using OxetekWeChatSDK.Request;
using OxetekWeChatSDK.Response;
using OxetekWeChatSDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxetekWeChatSDK.Pay
{
    public class PayManager
    {
        private static NLog.ILogger logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 生成付款二维码,模式一
        /// </summary>
        /// <param name="mchKey"> key设置路径：微信商户平台(pay.weixin.qq.com)-->账户设置-->API安全-->密钥设置</param>
        /// <returns></returns>
        public static string BizPayUrl(BizPayUrlRequest request, string mchKey)
        {
            //二维码内容：weixin://wxpay/bizpayurl?sign=XXXXX&appid=XXXXX&mch_id=XXXXX&product_id=XXXXXX&time_stamp=XXXXXX&nonce_str=XXXXX
            var headStr = "weixin://wxpay/bizpayurl?";
            //var sortStr = $"sign=&appid={request.AppId}&mch_id={request.Mch_Id}&product_id={request.Product_Id}&time_stamp={request.Time_Stamp}&nonce_str={request.Nonce_Str}";
            //sortStr = RequestParamUtil.ASCIIKeySort(sortStr);
            var payData = new WxPayData(mchKey);

            payData.SetValue("appid", request.AppId);
            payData.SetValue("mch_id", request.Mch_Id);
            payData.SetValue("product_id", request.Product_Id);
            payData.SetValue("time_stamp", request.Time_Stamp);
            payData.SetValue("nonce_str", request.Nonce_Str);

            var url = WxPayApi.BizPayUrl(payData);

            return $"{headStr}{url}";
        }

        /// <summary>
        /// 生成直接支付url，支付url有效期为2小时,模式二
        /// param productId 商品ID
        /// return 模式二URL
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="mchKey"></param>
        /// <returns></returns>
        public static async Task<string> GetPayUrl(UnifiedOrderRequest request, string appId, string mchId, string mchKey, string ip)
        {
            //Log.Info(this.GetType().ToString(), "Native pay mode 2 url is producing...");

            WxPayData data = new WxPayData(mchKey);
            data.SetValue("body", request.Body);//商品描述
            data.SetValue("attach", request.Attach);//附加数据
            data.SetValue("out_trade_no", request.Out_Trade_No);//随机字符串
            data.SetValue("total_fee", request.Total_Fee);//总金额
            data.SetValue("time_start", request.Time_Start);//交易起始时间
            data.SetValue("time_expire", request.Time_Expire);//交易结束时间
            data.SetValue("goods_tag", request.Goods_Tag);//商品标记
            data.SetValue("trade_type", "NATIVE");//交易类型
            data.SetValue("product_id", request.Product_Id);//商品ID
            data.SetValue("notify_url", request.Notify_Url);

            WxPayData result = await UnifiedOrder(data, appId, mchId, mchKey, ip);//调用统一下单接口
            string url = result.GetValue("code_url").ToString();//获得统一下单接口返回的二维码链接

            //Log.Info(this.GetType().ToString(), "Get native pay mode 2 url : " + url);
            return url;
        }

        /**
        *    
        * 查询订单
        * @param WxPayData inputObj 提交给查询订单API的参数
        * @param int timeOut 超时时间
        * @throws WxPayException
        * @return 成功时返回订单查询结果，其他抛异常
        */
        //mchKey: 微信商户平台(pay.weixin.qq.com)-->账户设置-->API安全-->密钥设置
        public static WxPayData OrderQuery(WxPayData inputObj, string appId, string mchId, string mchKey, int timeOut = 30)
        {
#if DEBUG
            string url = "https://api.mch.weixin.qq.com/sandboxnew/pay/orderquery";
#else
            string url = "https://api.mch.weixin.qq.com/pay/orderquery";
#endif
            //检测必填参数
            if (!inputObj.IsSet("out_trade_no") && !inputObj.IsSet("transaction_id"))
            {
                throw new WxPayException("订单查询接口中，out_trade_no、transaction_id至少填一个！");
            }

            inputObj.SetValue("appid", appId);//公众账号ID
            inputObj.SetValue("mch_id", mchId);//商户号
            inputObj.SetValue("nonce_str", WxPayApi.GenerateNonceStr());//随机字符串
            //inputObj.SetValue("sign_type", WxPayData.SIGN_TYPE_HMAC_SHA256);//签名类型
            inputObj.SetValue("sign", inputObj.MakeSign());//签名


            string xml = inputObj.ToXml();

            var start = DateTime.Now;

            logger.Debug("WxPayApi", "OrderQuery request : " + xml);

            //string response = HttpService.Post(xml, url, false, timeOut);//调用HTTP通信接口提交数据
            var response = HttpUtil.Post(url, xml).GetAwaiter().GetResult();

            logger.Debug("WxPayApi", "OrderQuery response : " + response);

            var end = DateTime.Now;
            int timeCost = (int)(end - start).TotalMilliseconds;//获得接口耗时

            //将xml格式的数据转化为对象以返回
            WxPayData result = new WxPayData(mchKey);
            result.FromXml(response);

            //ReportCostTime(url, timeCost, result);//测速上报

            return result;
        }

        /// <summary>
        /// 统一下单
        /// </summary>
        /// <param name="inputObj"></param>
        /// <param name="appId"></param>
        /// <param name="mchId"></param>
        /// <param name="mchKey"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static async Task<UnifiedOrderResponse> UnifiedOrderEntity(WxPayData inputObj, string appId, string mchId, string mchKey, string ip)
        {
            /*
            沙箱测试方法：
            1、https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=23_1 生成sandbox_signkey值 替换商户key，total_fee按【WXPayAssist】这个公众号内的金额设置，其他保持不变
            2、在url中增加 sandboxnew
            https://pay.weixin.qq.com/wiki/doc/api/wxa/wxa_api.php?chapter=20_1
             
            校验签名
            签名类型：MD5
            校验方式：自定义参数
            1) mch_id
            2) nonce_str
            3) 商户Key
            用这三个参数生成签名
            最终得到用于提交的xml：

            <xml>
	            <mch_id><![CDATA[1469900902]]></mch_id>
	            <nonce_str><![CDATA[5K8264ILTKCH16CQ2502SI8ZNMTM67VS]]></nonce_str>
	            <sign>6E573C1EC84D2C08FF6A5A72E890852E</sign>
            </xml>

            Post XML后会得到以下 【sandbox_signkey】----**********************替换商户key
            <xml>
              <return_code><![CDATA[SUCCESS]]></return_code>
              <return_msg><![CDATA[ok]]></return_msg>
              <sandbox_signkey><![CDATA[cfe0a7745bd838d31103a3604b52e99d]]></sandbox_signkey>
            </xml>

             */



            var data = await UnifiedOrder(inputObj, appId, mchId, mchKey, ip);

            logger.Debug($"统一下单返回数据:{data.ToJson()}");
            try
            {
                var rsp = new UnifiedOrderResponse()
                {
                    AppId = data.GetValue("appid").To<string>(),
                    //CodeUrl = data.GetValue("code_url").ToString(),
                    DeviceInfo = data.GetValue("device_info").To<string>(),
                    ErrCode = data.GetValue("err_code").To<string>(),
                    ErrCodeDes = data.GetValue("err_code_des").To<string>(),
                    MchId = data.GetValue("mch_id").To<string>(),
                    //MWebUrl= inputObj.GetValue("device_info").ToString(),
                    NonceStr = data.GetValue("nonce_str").To<string>(),
                    PrepayId = data.GetValue("prepay_id").To<string>(),
                    ResultCode = data.GetValue("result_code").To<string>(),
                    ReturnCode = data.GetValue("return_code").To<string>(),
                    ReturnMsg = data.GetValue("return_msg").To<string>(),
                    Sign = data.GetValue("sign").To<string>(),
                    //SubAppId= inputObj.GetValue("device_info").ToString(),
                    //SubMchId= inputObj.GetValue("device_info").ToString(),
                    TradeType = data.GetValue("trade_type").To<string>(),
                };
                if (data.IsSet("code_url"))
                {
                    rsp.CodeUrl = data.GetValue("code_url").ToString();
                }
                logger.Debug($"UnifiedOrderResponse:{Newtonsoft.Json.JsonConvert.SerializeObject(rsp)}");
                return rsp;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// 统一下单
        /// </summary>
        /// <param name="inputObj"></param>
        /// <param name="appId"></param>
        /// <param name="mchId"></param>
        /// <param name="mchKey"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static async Task<WxPayData> UnifiedOrder(WxPayData inputObj, string appId, string mchId, string mchKey, string ip)
        {
            //https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_1
#if DEBUG
            string url = "https://api.mch.weixin.qq.com/sandboxnew/pay/unifiedorder";
#else
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
#endif

            //检测必填参数
            if (!inputObj.IsSet("out_trade_no"))
            {
                throw new WxPayException("缺少统一支付接口必填参数out_trade_no！");
            }
            else if (!inputObj.IsSet("body"))
            {
                throw new WxPayException("缺少统一支付接口必填参数body！");
            }
            else if (!inputObj.IsSet("total_fee"))
            {
                throw new WxPayException("缺少统一支付接口必填参数total_fee！");
            }
            else if (!inputObj.IsSet("trade_type"))
            {
                throw new WxPayException("缺少统一支付接口必填参数trade_type！");
            }

            //关联参数
            if (inputObj.GetValue("trade_type").ToString() == "JSAPI" && !inputObj.IsSet("openid"))
            {
                throw new WxPayException("统一支付接口中，缺少必填参数openid！trade_type为JSAPI时，openid为必填参数！");
            }
            if (inputObj.GetValue("trade_type").ToString() == "NATIVE" && !inputObj.IsSet("product_id"))
            {
                throw new WxPayException("统一支付接口中，缺少必填参数product_id！trade_type为JSAPI时，product_id为必填参数！");
            }

            //异步通知url未设置，则使用配置文件中的url
            if (!inputObj.IsSet("notify_url"))
            {
                throw new WxPayException("统一支付接口中，缺少必填参数notify_url！");
                //inputObj.SetValue("notify_url", WxPayConfig.GetConfig().GetNotifyUrl());//异步通知url
            }

            inputObj.SetValue("appid", appId);//公众账号ID
            inputObj.SetValue("mch_id", mchId);//商户号
            inputObj.SetValue("spbill_create_ip", ip);//终端ip	  	    
            inputObj.SetValue("nonce_str", WxPayApi.GenerateNonceStr());//随机字符串
            inputObj.SetValue("sign_type", "MD5");//签名类型

            //签名
            inputObj.SetValue("sign", inputObj.MakeSign());
            string xml = inputObj.ToXml();

            var start = DateTime.Now;

            logger.Debug($"WxPayApi UnfiedOrder request : {xml}");
            //string response = HttpService.Post(xml, url, false, timeOut);
            string response = await HttpUtil.Post(url, xml, isString: true);
            logger.Debug($"WxPayApi UnfiedOrder response : {response}");

            var end = DateTime.Now;
            int timeCost = (int)(end - start).TotalMilliseconds;

            WxPayData result = new WxPayData(mchKey);
            result.FromXml(response);

            //ReportCostTime(url, timeCost, result);//测速上报

            return result;

        }
        /// <summary>
        /// <see cref="https://pay.weixin.qq.com/wiki/doc/api/tools/mch_pay.php?chapter=14_2"/>
        /// 用于企业向微信用户个人付款 目前支持向指定微信用户的openid付款。
        /// </summary>
        /// <param name="transferData"></param>
        /// <param name="cert">证书路径</param>
        /// <param name="certPassword">证书密码</param>
        /// <param name="mchKey">商户密钥</param>
        /// <returns></returns>
        public static async Task<WxPayData> TransfersAsync(TransferData transferData, string cert, string certPassword, string mchKey)
        {
            var url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers";


            WxPayData payData = new WxPayData(mchKey);

            payData.SetValue("mch_appid", transferData.MchAppId);
            payData.SetValue("mchid", transferData.MchId);
            if (!string.IsNullOrWhiteSpace(transferData.DeviceInfo))
                payData.SetValue("device_info", transferData.DeviceInfo);

            payData.SetValue("nonce_str", transferData.NonceStr);
            payData.SetValue("partner_trade_no", transferData.OutTradeNo);
            payData.SetValue("openid", transferData.OpenId);
            payData.SetValue("check_name", transferData.CheckName);
            if (!string.IsNullOrWhiteSpace(transferData.ReUserName))
                payData.SetValue("re_user_name", transferData.ReUserName);

            payData.SetValue("amount", transferData.Amount);
            payData.SetValue("desc", transferData.Desc);
            payData.SetValue("spbill_create_ip", transferData.SpbillCreateIP);

            payData.SetValue("sign", payData.MakeSign());


            var xml = payData.ToXml();

            var resp = await HttpUtil.CertPostAsync(url, xml, cert, certPassword);
            WxPayData result = new WxPayData(mchKey);
            result.FromXml(resp, false);

            return result;
        }

        /// <summary>
        /// 用于商户的企业付款操作进行结果查询，返回付款操作详细结果。查询企业付款API只支持查询30天内的订单，30天之前的订单请登录商户平台查询。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cert">证书路径</param>
        /// <param name="certPassword">证书密码</param>
        /// <param name="mchKey">商户密钥</param>
        /// <returns></returns>
        public static async Task<WxPayData> GetTransferInfoAsync(GetTransferData data, string cert, string certPassword, string mchKey)
        {
            var url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/gettransferinfo";


            WxPayData payData = new WxPayData(mchKey);


            payData.SetValue("nonce_str", data.NonceStr);
            payData.SetValue("partner_trade_no", data.PartnerTradeNo);
            payData.SetValue("mch_id", data.MchId);
            payData.SetValue("appid", data.AppId);

            payData.SetValue("sign", payData.MakeSign());


            var xml = payData.ToXml();
            var resp = await HttpUtil.CertPostAsync(url, xml, cert, certPassword);
            WxPayData result = new WxPayData(mchKey);
            result.FromXml(resp);

            return result;
        }

    }
}
