
#region Version Info
/**************************************************************** 
**************************************************************** 
* 作    者：SAM
* 邮    箱:	support@oxetek.com
* CLR 版本：4.0.30319.42000
* 创建时间：2019-11-30 19:37:34
* 当前版本：1.0.0.0
* 
* 描述说明： 
* 
* 修改历史： 
* timestamp: 900f6314-0b83-479d-8587-e3413b397883
****************************************************************** 
* Copyright @ SAM 2019 All rights reserved**********************/
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace OxetekWeChatSDK.Cryptography
{
    public static class Cryptography
    {

        /*
         https://developers.weixin.qq.com/miniprogram/dev/framework/open-ability/signature.html
             加密数据解密算法
接口如果涉及敏感数据（如wx.getUserInfo当中的 openId 和 unionId），接口的明文内容将不包含这些敏感数据。开发者如需要获取敏感数据，需要对接口返回的加密数据(encryptedData) 进行对称解密。 解密算法如下：

对称解密使用的算法为 AES-128-CBC，数据采用PKCS#7填充。
对称解密的目标密文为 Base64_Decode(encryptedData)。
对称解密秘钥 aeskey = Base64_Decode(session_key), aeskey 是16字节。
对称解密算法初始向量 为Base64_Decode(iv)，其中iv由数据接口返回。
微信官方提供了多种编程语言的示例代码（（点击下载）。每种语言类型的接口名字均一致。调用方式可以参照示例。

另外，为了应用能校验数据的有效性，会在敏感数据加上数据水印( watermark )              
             */
        /// <summary>
        /// 微信小程序 encryptedData 解密
        /// </summary>
        /// <param name="encryptedDataStr"></param>
        /// <param name="key">session_key</param>
        /// <param name="iv">iv</param>
        /// <returns></returns>
        public static string AES_decrypt4mini(string encryptedDataStr, string sessionKey, string iv)
        {
            var rijalg = Aes.Create();
            // RijndaelManaged rijalg = new RijndaelManaged();
            //----------------- 
            //设置 cipher 格式 AES-128-CBC 

            rijalg.KeySize = 128;

            rijalg.Padding = PaddingMode.PKCS7;
            rijalg.Mode = CipherMode.CBC;

            rijalg.Key = Convert.FromBase64String(sessionKey);
            rijalg.IV = Convert.FromBase64String(iv);


            byte[] encryptedData = Convert.FromBase64String(encryptedDataStr);
            //解密 
            ICryptoTransform decryptor = rijalg.CreateDecryptor(rijalg.Key, rijalg.IV);

            string result;

            using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        result = srDecrypt.ReadToEnd();
                    }
                }
            }
            return result;
        }

        /// <summary>
		/// 采用SHA-1算法加密字符串（小写）
		/// </summary>
		/// <param name="encypStr">需要加密的字符串</param>
		/// <returns></returns>
		public static string GetSha1(string encypStr)
        {
            byte[] array = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(encypStr));
            StringBuilder stringBuilder = new StringBuilder();
            byte[] array2 = array;
            foreach (byte b in array2)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

    }
}
