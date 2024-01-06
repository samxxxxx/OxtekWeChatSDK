using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Messages
{
    public enum EventType
    {
        /// <summary>
        /// 用户扫描带场景值二维码时，用户未关注时，进行关注后的事件推送
        /// </summary>
        Subscribe,
        /// <summary>
        /// 用户扫描带场景值二维码时，取消关注事件
        /// </summary>
        UnSubscribe,
        /*
         * https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140454
         扫描带参数二维码事件
用户扫描带场景值二维码时，可能推送以下两种事件：

如果用户还未关注公众号，则用户可以关注公众号，关注后微信会将带场景值关注事件推送给开发者。
如果用户已经关注公众号，则微信会将带场景值扫描事件推送给开发者。
*/
        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        Location,
        /// <summary>
        /// 自定义菜单事件
        /// </summary>
        Click,
        /// <summary>
        /// 点击菜单跳转链接时的事件推送
        /// </summary>
        View,
        /// <summary>
        /// 用户扫描带场景值二维码时，用户已关注时的事件推送
        /// </summary>
        Scan
    }
}
