using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Common
{
    public class MD5
    {
        #region 使用MD5进行数据加密。

        /// <summary>    
        /// 使用MD5进行数据加密。    
        /// </summary>    
        /// <param name="str">需要加密的数据。加密类型默认为：密码加密</param>    
        /// <param name="kind"></param>    
        /// <returns>返回加密的数据</returns>    
        public static string GetMD5(string str)
        {
            string reStr = "";
          
            ////获取要加密的字段，并转化为Byte[]数组    
            //byte[] data = System.Text.Encoding.Unicode.GetBytes(str.ToCharArray());

            ////建立加密服务    
            //System.Security.Cryptography.MD5 md5 = new
            //System.Security.Cryptography.MD5CryptoServiceProvider();

            ////1代表加密Byte[]数组    
            //if (kind == 1)
            //{
            //    //加密Byte[]数组    
            //    byte[] result = md5.ComputeHash(data);
            //    //将加密后的数组转化为字段    
            //    string sResult = System.Text.Encoding.Unicode.GetString(result);
            //    //MD5普通加密    
            //    reStr = sResult.ToString();
            //}
            ////2代表作为密码方式加密    
            //else if (kind == 2)

            {
                //作为密码方式加密    
                string EnPswdStr = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile
               (str, "MD5");
                //MD5密码加密    
                reStr = EnPswdStr;
            }
            return reStr.ToString();
        }
        #endregion

        ///// <summary>    
        ///// 对数据进行加密。    
        ///// </summary>    
        ///// <param name="strText">传入加密的数据</param>    
        ///// <returns>返回加密后的字字符串</returns>    
        //public string EncryptText(String strText)
        //{
        //    return Encrypt(strText, "&%#@?,:*_");
        //}

        ///// <summary>    
        ///// 对数据进行解密。    
        ///// </summary>    
        ///// <param name="strText">传入解密的数据</param>    
        ///// <returns>返回解密后的字字符串</returns>    
        //public String DecryptText(String strText)
        //{
        //    return Decrypt(strText, "&%#@?,:*_");
        //}

        //#region 加密解密函数

        ///// <summary>    
        ///// 加密函数    
        ///// </summary>    
        ///// <param name="strText"></param>    
        ///// <param name="strEncrKey"></param>    
        ///// <returns></returns>    
        //private String Encrypt(String strText, String strEncrKey)
        //{

        //    Byte[] byKey = { };
        //    Byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        //    try
        //    {
        //        byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
        //        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //        Byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
        //        MemoryStream ms = new MemoryStream();
        //        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV),
        //   CryptoStreamMode.Write);
        //        cs.Write(inputByteArray, 0, inputByteArray.Length);
        //        cs.FlushFinalBlock();
        //        return Convert.ToBase64String(ms.ToArray());
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        ///// <summary>    
        ///// 解密函数    
        ///// </summary>    
        ///// <param name="strText"></param>    
        ///// <param name="sDecrKey"></param>    
        ///// <returns></returns>    
        //private String Decrypt(String strText, String sDecrKey)
        //{

        //    Byte[] byKey = { };
        //    Byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        //    Byte[] inputByteArray = new byte[strText.Length];
        //    try
        //    {
        //        byKey = System.Text.Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
        //        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //        inputByteArray = Convert.FromBase64String(strText);
        //        MemoryStream ms = new MemoryStream();
        //        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV),
        //   CryptoStreamMode.Write);
        //        cs.Write(inputByteArray, 0, inputByteArray.Length);
        //        cs.FlushFinalBlock();
        //        System.Text.Encoding encoding = System.Text.Encoding.UTF8;
        //        return encoding.GetString(ms.ToArray());
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
        //#endregion
    }
}
