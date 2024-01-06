
#region Version Info
/**************************************************************** 
**************************************************************** 
* 作    者：SAM
* 邮    箱:	support@oxetek.com
* CLR 版本：4.0.30319.42000 
* 创建时间：2018-11-10 16:38:02 
* 当前版本：1.0.0.0
* 
* 描述说明： 
* 
* 修改历史： 
****************************************************************** 
* Copyright @ SAM 2018 All rights reserved**********************/
#endregion

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace OxetekWeChatSDK.Pay
{
    public class RandomGenerator
    {
        //readonly RNGCryptoServiceProvider csp;

        //public RandomGenerator()
        //{
        //    csp = new RNGCryptoServiceProvider();
        //}

        public int Next(int minValue, int maxExclusiveValue)
        {
            if (minValue >= maxExclusiveValue)
                throw new ArgumentOutOfRangeException("minValue must be lower than maxExclusiveValue");

            long diff = (long)maxExclusiveValue - minValue;
            long upperBound = uint.MaxValue / diff * diff;

            uint ui;
            do
            {
                ui = GetRandomUInt();
            } while (ui >= upperBound);
            return (int)(minValue + ui % diff);
        }

        public uint GetRandomUInt()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(sizeof(uint));
            return BitConverter.ToUInt32(randomBytes, 0);
        }

        //private byte[] GenerateRandomBytes(int bytesNumber)
        //{
        //    byte[] buffer = new byte[bytesNumber];
        //    csp.GetBytes(buffer);
        //    return buffer;
        //}
    }
}
