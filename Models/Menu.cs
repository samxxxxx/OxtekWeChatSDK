using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Models
{
    public class Menu
    {
        public Menu()
        {
            Button = new List<Button>();
        }
        public List<Button> Button { get; set; }
    }
}
