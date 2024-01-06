using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Models
{
    public class Button
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Url { get; set; }
        public List<Button> Sub_Button { get; set; }
    }
}
