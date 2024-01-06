using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Messages
{
    public static class EnumTypeManager
    {
        /// <summary>
        /// 将值转换成枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T ToEnumType<T>(this string type) where T : struct
        {
            if (Enum.TryParse(type, true, out T parseType))
            {
                return parseType;
            }
            return default;
        }
    }
    public enum MsgType
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        Text,
        /// <summary>
        /// 图片消息
        /// </summary>
        Image,
        /// <summary>
        /// 语音消息
        /// </summary>
        Voice,
        /// <summary>
        /// 视频消息
        /// </summary>
        Video,
        /// <summary>
        /// 小视频消息
        /// </summary>
        ShortVideo,
        /// <summary>
        /// 地理位置消息
        /// </summary>
        Location,
        /// <summary>
        /// 链接消息
        /// </summary>
        Link,
        /// <summary>
        /// 接收事件推送：
        /// 关注/取消关注事件
        /// 扫描带参数二维码事件
        /// 上报地理位置事件
        /// 自定义菜单事件
        /// </summary>
        Event,

        UnKnow

    }
}
