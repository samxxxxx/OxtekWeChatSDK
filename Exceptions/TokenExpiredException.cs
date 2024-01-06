using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Exceptions
{
    public class TokenExpiredException : Exception
    {
        public TokenExpiredException() : base("token has expired")
        {

        }
        public TokenExpiredException(string message) : base(message)
        {

        }
        public TokenExpiredException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
