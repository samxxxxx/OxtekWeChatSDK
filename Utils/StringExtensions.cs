using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Utils
{
    public static class StringExtensions
    {

        public static T To<T>(this object obj)
        {
            try
            {
                if (obj == null)
                    return default;
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            catch
            {
                return default;
            }
        }
    }
}
