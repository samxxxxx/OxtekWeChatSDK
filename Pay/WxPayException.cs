
#region Version Info
/**************************************************************** 
**************************************************************** 
* 作    者：SAM
* 邮    箱:	support@oxetek.com
* CLR 版本：4.0.30319.42000 
* 创建时间：2018-11-10 16:32:43 
* 当前版本：1.0.0.0
* 
* 描述说明： 
* 
* 修改历史： 
****************************************************************** 
* Copyright @ SAM 2018 All rights reserved**********************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Pay
{
    public class WxPayException : Exception
    {
        public WxPayException(string msg) : base(msg)
        {

        }
    }
}
