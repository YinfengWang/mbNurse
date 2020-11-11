//------------------------------------------------------------------------------------
//
//  ϵͳ����        : ҽ������վ
//  ��ϵͳ����      : ͨ��ģ��
//  ��������        : 
//  ����            : EnDecrypt.cs
//  ���ܸ�Ҫ        : ���ܽ���ģ��
//  ������          : ����
//  ������          : 2007-01-19
//  �汾            : 1.0.0.0
// 
//------------------< �����ʷ >------------------------------------------------------
//  �������        :
//  �����          :
//  �������        :
//  �汾            :
//------------------------------------------------------------------------------------

using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace HISPlus
{
    /// <summary>
    /// En_Decrypt ��ժҪ˵����
    /// </summary>
    public class EnDecrypt
    {
        #region �ⲿ�����ӿ�
        [DllImport("tjdtool.dll")]
        public static extern bool JhaRdecrypt(IntPtr a, IntPtr b);

        [DllImport("commpub.dll")]
        public static extern bool commpubinit();
        #endregion

        public enum SymmProvEnum : int
        {
            DES, RC2, Rijndael, TripleDes
        }


        #region ����
        private SymmetricAlgorithm cryptAlgorithm;

        private string key = string.Empty;             // ��Կ
        private string iv = string.Empty;				// ��ʼ������
        #endregion


        #region ���캯��
        /// <remarks>
        /// ʹ���Զ���SymmetricAlgorithm��Ĺ�����.
        /// </remarks>
        public EnDecrypt()
        {
            cryptAlgorithm = new TripleDESCryptoServiceProvider();
            cryptAlgorithm.Mode = CipherMode.CBC;

            key = "Guz(%&hj7x89*$yuBI04o6FtmaT5&fvHUFCy76*h%(HilJ$lhj!y6&(*jkP87jH7";
            iv = "E4ghj*Ghg7!r)Ifb&95GUY86GfghUb#er5+HBh(u%g6HJ(&jhWk7&!hg4ui%$hjk";
        }


        /// <remarks>
        /// ʹ��.Net SymmetricAlgorithm ��Ĺ�����.
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
        /// ʹ���Զ���SymmetricAlgorithm��Ĺ�����.
        /// </remarks>
        public EnDecrypt(SymmetricAlgorithm ServiceProvider)
        {
            cryptAlgorithm = ServiceProvider;
        }
        #endregion


        #region ����
        /// <summary>
        /// �����ַ���
        /// </summary>
        /// <param name="Source">Ҫ���ܵ��ַ���</param>
        /// <param name="Key">��Կ</param>
        /// <returns>���ܺ���ַ���</returns>
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
        /// �����ַ���
        /// </summary>
        /// <param name="Source">Ҫ���ܵ��ַ���</param>
        /// <param name="Key">��Կ</param>
        /// <returns>���ܺ���ַ���</returns>
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
        /// ����ϵͳ�ļ����㷨
        /// </summary>
        /// <param name="src">����</param>
        /// <returns>����</returns>
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
        /// ����ϵͳ�Ľ����㷨
        /// </summary>
        /// <param name="src">����</param>
        /// <returns>����</returns>
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
        /// ��Ȩ��ѹ
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        static public string JhaRdecrypt(string src)
        {
            // �������
            if (src.Trim().Length == 0)
            {
                return string.Empty;
            }

            // ��������
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


        #region ��ͨ����
        /// <remarks>
        /// ��ȡ��Կ
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
        /// ��ó�ʼ����IV
        /// </summary>
        /// <returns>��������IV</returns>
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
