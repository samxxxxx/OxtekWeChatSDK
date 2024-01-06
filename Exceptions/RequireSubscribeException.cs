
#region Version Info
/**************************************************************** 
**************************************************************** 
* 作    者：SAM
* 邮    箱:	support@oxetek.com
* CLR 版本：4.0.30319.42000
* 创建时间：2019-08-29 10:31:28
* 当前版本：1.0.0.0
* 
* 描述说明： 
* 
* 修改历史： 
* timestamp: 98e78c75-e882-4c17-97a5-cb48bc6d3318
****************************************************************** 
* Copyright @ SAM 2019 All rights reserved**********************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Exceptions
{
    public class RequireSubscribeException : Exception
    {
        public RequireSubscribeException() : base("require subscribe")
        {

        }
        public RequireSubscribeException(string message) : base(message)
        {

        }
        public RequireSubscribeException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
