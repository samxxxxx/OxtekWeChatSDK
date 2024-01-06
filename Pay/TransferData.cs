
#region Version Info
/**************************************************************** 
**************************************************************** 
* 作    者：SAM
* 邮    箱:	support@oxetek.com
* CLR 版本：4.0.30319.42000
* 创建时间：2020-2-9 23:25:17
* 当前版本：1.0.0.0
* 
* 描述说明： 
* 
* 修改历史： 
* timestamp: 8f353e0b-a5f8-4477-a534-c04773f9aa33
****************************************************************** 
* Copyright @ SAM 2020 All rights reserved**********************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Pay
{
    public class TransferData
    {
        public TransferData()
        {
            CheckName = "FORCE_CHECK";
            NonceStr = Guid.NewGuid().ToString("N").Substring(0, 16);
        }
        /// <summary>
        /// 微信分配的公众账号ID（企业号corpid即为此appId） [mch_appid]
        /// </summary>
        public string MchAppId
        {
            get;
            set;
        }

        /// <summary>
        /// 商户号 [mchid]
        /// </summary>
        public string MchId
        {
            get;
            set;
        }

        /// <summary>
        /// 微信支付分配的终端设备号 [device_info]
        /// </summary>
        public string DeviceInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 随机字符串 [nonce_str]
        /// </summary>
        public string NonceStr
        {
            get;
            private set;
        }

        /// <summary>
        /// 商家订单号 [partner_trade_no]
        /// </summary>
        public string OutTradeNo
        {
            get;
            set;
        }

        /// <summary>
        /// 用户openid [openid]
        /// </summary>
        public string OpenId
        {
            get;
            set;
        }

        /// <summary>
        /// 校验用户姓名选项 [check_name] NO_CHECK：不校验真实姓名
        /// FORCE_CHECK：强校验真实姓名
        /// </summary>
        public string CheckName
        {
            get;
            set;
        }

        /// <summary>
        /// 收款用户姓名 [re_user_name]
        /// </summary>
        public string ReUserName
        {
            get;
            set;
        }

        /// <summary>
        /// 金额 [amount]
        /// </summary>
        public long Amount
        {
            get;
            set;
        }

        /// <summary>
        /// 企业付款描述信息 [desc]
        /// </summary>
        public string Desc
        {
            get;
            set;
        }

        /// <summary>
        /// Ip地址 [spbill_create_ip]
        /// </summary>
        public string SpbillCreateIP
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Key
        {
            get;
            set;
        }
    }
}
