//------------------------------------------------------------------------------------
//
//  系统名称        : 医生工作站
//  子系统名称      : 通用模块
//  对象类型        : 
//  类名            : EnDecrypt.cs
//  功能概要        : 加密解密模块
//  作成者          : 付军
//  作成日          : 2007-01-19
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        :
//  变更者          :
//  变更内容        :
//  版本            :
//------------------------------------------------------------------------------------

using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace HISPlus
{
    /// <summary>
    /// En_Decrypt 的摘要说明。
    /// </summary>
    public class EnDecrypt
    {
        #region 外部方法接口
        [DllImport("tjdtool.dll")]
        public static extern bool JhaRdecrypt(IntPtr a, IntPtr b);

        [DllImport("commpub.dll")]
        public static extern bool commpubinit();
        #endregion

        public enum SymmProvEnum : int
        {
            DES, RC2, Rijndael, TripleDes
        }


        #region 变量
        private SymmetricAlgorithm cryptAlgorithm;

        private string key = string.Empty;             // 密钥
        private string iv = string.Empty;				// 初始化向量
        #endregion


        #region 构造函数
        /// <remarks>
        /// 使用自定义SymmetricAlgorithm类的构造器.
        /// </remarks>
        public EnDecrypt()
        {
            cryptAlgorithm = new TripleDESCryptoServiceProvider();
            cryptAlgorithm.Mode = CipherMode.CBC;

            key = "Guz(%&hj7x89*$yuBI04o6FtmaT5&fvHUFCy76*h%(HilJ$lhj!y6&(*jkP87jH7";
            iv = "E4ghj*Ghg7!r)Ifb&95GUY86GfghUb#er5+HBh(u%g6HJ(&jhWk7&!hg4ui%$hjk";
        }


        /// <remarks>
        /// 使用.Net SymmetricAlgorithm 类的构造器.
        /// </remarks>
        public EnDecrypt(SymmProvEnum NetSelected)
        {
            switch (NetSelected)
            {
                case SymmProvEnum.DES:
                    cryptAlgorithm = new DESCryptoServiceProvider();
                    break;

                case SymmProvEnum.RC2:
                    cryptAlgorithm = new RC2CryptoServiceProvider();
                    break;

                case SymmProvEnum.Rijndael:
                    cryptAlgorithm = new RijndaelManaged();
                    break;

                case SymmProvEnum.TripleDes:
                    cryptAlgorithm = new TripleDESCryptoServiceProvider();
                    break;
            }

            cryptAlgorithm.Mode = CipherMode.CBC;

            key = "Guz(%&hj7x89*$yuBI04o6FtmaT5&fvHUFCy76*h%(HilJ$lhj!y6&(*jkP87jH7";
            iv = "E4ghj*Ghg7!r)Ifb&95GUY86GfghUb#er5+HBh(u%g6HJ(&jhWk7&!hg4ui%$hjk";
        }


        /// <remarks>
        /// 使用自定义SymmetricAlgorithm类的构造器.
        /// </remarks>
        public EnDecrypt(SymmetricAlgorithm ServiceProvider)
        {
            cryptAlgorithm = ServiceProvider;
        }
        #endregion


        #region 方法
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="Source">要加密的字符串</param>
        /// <param name="Key">密钥</param>
        /// <returns>加密后的字符串</returns>
        public string Encrypt(string src)
        {
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(src);
            MemoryStream ms = new MemoryStream();
            cryptAlgorithm.Key = GetLegalKey();
            cryptAlgorithm.IV = GetLegalIV();

            ICryptoTransform encrypto = cryptAlgorithm.CreateEncryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);

            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            ms.Close();

            byte[] bytOut = ms.ToArray();

            return Convert.ToBase64String(bytOut);
        }


        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="Source">要解密的字符串</param>
        /// <param name="Key">密钥</param>
        /// <returns>解密后的字符串</returns>
        public string Decrypt(string src)
        {
            byte[] bytIn = Convert.FromBase64String(src);

            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
            cryptAlgorithm.Key = GetLegalKey();
            cryptAlgorithm.IV = GetLegalIV();

            ICryptoTransform encrypto = cryptAlgorithm.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }


        /// <summary>
        /// 军卫系统的加密算法
        /// </summary>
        /// <param name="src">明文</param>
        /// <returns>密文</returns>
        static public string Encrypt_JW(string src)
        {
            int k;

            string oneChar = string.Empty;
            string result = string.Empty;

            if (src.Length == 0)
            {
                return string.Empty;
            }

            src = src.Trim();

            for (int i = 0; i < src.Length; i++)
            {
                oneChar = src.Substring(i, 1);

                char[] arrChar = oneChar.ToCharArray();

                if (i % 2 == 0)
                {
                    k = arrChar[0] - i + 8 - 1;
                }
                else
                {
                    k = arrChar[0] + i - 32 + 1;
                }

                result += (char)k;
            }

            return result;
        }


        /// <summary>
        /// 军卫系统的解密算法
        /// </summary>
        /// <param name="src">明文</param>
        /// <returns>密文</returns>
        static public string Decrypt_JW(string src)
        {
            int k;

            string oneChar = string.Empty;
            string result = string.Empty;

            if (src.Length == 0)
            {
                return string.Empty;
            }

            src = src.Trim();

            for (int i = 0; i < src.Length; i++)
            {
                oneChar = src.Substring(i, 1);

                char[] arrChar = oneChar.ToCharArray();

                if (i % 2 == 0)
                {
                    k = arrChar[0] + i - 8 + 1;
                }
                else
                {
                    k = arrChar[0] - i + 32 - 1;
                }

                result += (char)k;
            }
            return result;
        }

        /// <summary>
        /// 授权解压
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        static public string JhaRdecrypt(string src)
        {
            // 条件检查
            if (src.Trim().Length == 0)
            {
                return string.Empty;
            }

            // 正常处理
            string dest = new string(' ', 255);

            IntPtr ptrIn = Marshal.StringToHGlobalAnsi(src);
            IntPtr ptrOut = Marshal.StringToHGlobalAnsi(dest);
            bool result = JhaRdecrypt(ptrIn, ptrOut);

            if (result == true)
            {
                dest = Marshal.PtrToStringAnsi(ptrOut);
            }
            else
            {
                dest = string.Empty;
            }

            return dest;
        }
        #endregion


        #region 共通函数
        /// <remarks>
        /// 获取密钥
        /// </remarks>
        private byte[] GetLegalKey()
        {
            cryptAlgorithm.GenerateKey();

            byte[] bytTemp = cryptAlgorithm.Key;
            int keyLength = bytTemp.Length;

            if (key.Length > keyLength)
            {
                key = key.Substring(0, keyLength);
            }
            else if (key.Length < keyLength)
            {
                key = key.PadRight(keyLength, ' ');
            }

            return ASCIIEncoding.ASCII.GetBytes(key);
        }


        /// <summary>
        /// 获得初始向量IV
        /// </summary>
        /// <returns>初试向量IV</returns>
        private byte[] GetLegalIV()
        {
            cryptAlgorithm.GenerateIV();

            byte[] bytTemp = cryptAlgorithm.IV;
            int iVLength = bytTemp.Length;

            if (iv.Length > iVLength)
            {
                iv = iv.Substring(0, iVLength);
            }
            else if (iv.Length < iVLength)
            {
                iv = iv.PadRight(iVLength, ' ');
            }

            return ASCIIEncoding.ASCII.GetBytes(iv);
        }
        #endregion
    }
}
