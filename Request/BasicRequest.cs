using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Request
{
    public abstract class BasicRequest
    {
        /// <summary>
        /// 生成url
        /// </summary>
        /// <returns></returns>
        public abstract string BuildUrl();

    }
}
