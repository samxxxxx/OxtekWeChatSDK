using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Messages
{
    /// <summary>
    /// 二维码中的数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventData<T> where T : class, new()
    {
        public EventData(T data)
        {
            Data = data;
        }
        public T Data { get; set; }
    }
}
