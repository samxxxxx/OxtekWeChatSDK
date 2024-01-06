using OxetekWeChatSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Response
{
    public class GetMenuResponse : ErrorMessage
    {
        public Menu Menu { get; set; }
        public Menu ConditionalMenu { get; set; }
        /// <summary>
        /// 菜单数
        /// </summary>
        public int Count
        {
            get
            {
                if (Menu == null) return 0;
                return Menu.Button.Count;
            }
        }
    }
}
