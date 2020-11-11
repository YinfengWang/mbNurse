using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Encryption
{
    public class Md5
    {
        private static string str1 = "SERVER";

        private static string EncryptString(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encryptedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sbuilder = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sbuilder.AppendFormat("{0:X2}", encryptedBytes[i]);
            }
            return sbuilder.ToString();
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string Encrypt(string str)
        {
            return EncryptString(EncryptString(str));
        }

        /// <summary> 
        /// 解密字符串【SERVER】
        /// </summary> 
        /// <param name="decryptString">待解密的字符串</param> 
        /// <param name="decryptKey">解密密钥</param> 
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns> 
        public static string DecryptServer(string decryptString, string key)
        {
            string name = Encrypt(key).Substring(0, 2);
            if (decryptString.StartsWith(name))
            {
                return key + "T_" + DecryptS(DecryptS(decryptString.Substring(2), name), str1).Substring(9);
            }
            else
                // 无效医院名称
                return "-1";
        }

        /// <summary> 
        /// 解密字符串【SERVER】
        /// </summary> 
        /// <param name="decryptString">待解密的字符串</param> 
        /// <param name="decryptKey">解密密钥</param> 
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns> 
        private static string DecryptS(string decryptString, string key)
        {
            try
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                byte[] rgbKey = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(
                   key
                  ));

                byte[] rgbIV = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(
                     System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(
                     key, "SHA1")
                    ));

                byte[] inputByteArray = Convert.FromBase64String(decryptString);

                TripleDES DES = TripleDESCryptoServiceProvider.Create();

                DES.Mode = CipherMode.CBC;
                DES.KeySize = 128;
                DES.Padding = PaddingMode.ISO10126;

                MemoryStream mStream = new MemoryStream();

                CryptoStream cStream = new CryptoStream(mStream, DES.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);

                cStream.Write(inputByteArray, 0, inputByteArray.Length);

                cStream.FlushFinalBlock();

                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
