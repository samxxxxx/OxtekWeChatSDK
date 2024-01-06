using OxetekWeChatSDK.Request;
using OxetekWeChatSDK.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OxetekWeChatSDK.User
{
    public class UserManager
    {
        public static async Task<T> Info<T>(UserInfoRequest request)
        {
            var url = request.BuildUrl();
            return await HttpUtil.Get<T>(url);
        }
    }
}
