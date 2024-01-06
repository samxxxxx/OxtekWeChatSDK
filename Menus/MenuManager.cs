using Newtonsoft.Json;
using OxetekWeChatSDK.Models;
using OxetekWeChatSDK.Response;
using OxetekWeChatSDK.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OxetekWeChatSDK.Menus
{
    public class MenuManager
    {
        /// <summary>
        /// 
        /// 自定义菜单创建接口
        /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421141013
        /// 1、自定义菜单最多包括3个一级菜单，每个一级菜单最多包含5个二级菜单。
        ///2、一级菜单最多4个汉字，二级菜单最多7个汉字，多出来的部分将会以“...”代替。
        ///3、创建自定义菜单后，菜单的刷新策略是，在用户进入公众号会话页或公众号profile页时，如果发现上一次拉取菜单的请求在5分钟以前，就会拉取一下菜单，如果菜单有更新，就会刷新客户端的菜单。测试时可以尝试取消关注公众账号后再次关注，则可以看到创建后的效果。

        /// </summary>
        /// <returns></returns>
        public static async Task<bool> Create(Menu menu, string accessToken)
        {
            var url = $"https://api.weixin.qq.com/cgi-bin/menu/create?access_token={accessToken}";
            var res = await HttpUtil.Post<ErrorMessage>(url, menu, new JsonSerializerSettings()
            {
                ContractResolver = new LowerPropertyNameJsonSerializer()
            });

            return res.Success;
        }

        /// <summary>
        /// 自定义菜单查询接口
        /// https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421141014
        /// 使用接口创建自定义菜单后，开发者还可使用接口查询自定义菜单的结构。另外请注意，在设置了个性化菜单后，使用本自定义菜单查询接口可以获取默认菜单和全部个性化菜单信息。
        /// </summary>
        /// <returns></returns>

        public static async Task<GetMenuResponse> Query(string accessToken)
        {
            //https://api.weixin.qq.com/cgi-bin/menu/get?access_token=ACCESS_TOKEN
            var url = $"https://api.weixin.qq.com/cgi-bin/menu/get?access_token={accessToken}";
            var res = await HttpUtil.Get<GetMenuResponse>(url);
            return res;
        }
    }
}
