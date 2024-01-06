
#region Version Info
/**************************************************************** 
**************************************************************** 
* 作    者：SAM
* 邮    箱:	support@oxetek.com
* CLR 版本：4.0.30319.42000
* 创建时间：2020-2-10 1:43:05
* 当前版本：1.0.0.0
* 
* 描述说明： 
* 
* 修改历史： 
* timestamp: 2820558e-a2ed-4852-a548-9bd27c81e769
****************************************************************** 
* Copyright @ SAM 2020 All rights reserved**********************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Pay
{
    public class GetTransferData
    {
        public GetTransferData()
        {
            NonceStr = Guid.NewGuid().ToString("N").Substring(0, 16);
        }
        /// <summary>
        /// 公众账号ID [appid]
        /// </summary>
        public string AppId
        {
            get;
            set;
        }

        /// <summary>
        /// 商户号 [mch_id]
        /// </summary>
        public string MchId
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
        }

        /// <summary>
        /// 商户订单号，[partner_trade_no]
        /// </summary>
        public string PartnerTradeNo
        {
            get;
            set;
        }

    }
}
