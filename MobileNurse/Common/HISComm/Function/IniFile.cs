//------------------------------------------------------------------------------------
//
//  ϵͳ����        : ҽ������վ
//  ��ϵͳ����      : ͨ��ģ��
//  ��������        : 
//  ����            : IniFile.cs
//  ���ܸ�Ҫ        : Ini�ļ�����
//  ������          : ����
//  ������          : 2007-01-25
//  �汾            : 1.0.0.0
// 
//------------------< �����ʷ >------------------------------------------------------
//  �������        :
//  �����          :
//  �������        :
//  �汾            :
//------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace HISPlus
{
    /// <summary>
    /// IniFile ��ժҪ˵����
    /// </summary>
    public class IniFile
    {
        #region ����
        protected const int VALUE_MAX_LEN = 1024;					// Ini�ļ���ֵ����󳤶�

        protected string _configFile = "Config.ini";         // �����ļ�
        #endregion


        public IniFile()
        {
        }


        public IniFile(string fileName)
        {
            _configFile = fileName;
        }


        #region ����
        public string FileName
        {
            get
            {
                return _configFile;
            }
            set
            {
                _configFile = value;
            }
        }
        #endregion


        #region WindowsAPI
        /// <summary>
        /// ��ȡIni�ļ��е�����ֵ
        /// </summary>
        /// <param name="lpAppName">����</param>
        /// <param name="lpKeyName">����</param>
        /// <param name="nDefault">Ĭ��ֵ</param>
        /// <param name="lpFileName">Ini�ļ�</param>
        /// <returns>Ini�ļ������õ�ֵ</returns>
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

        /// <summary>
        /// ��ȡIni�ļ��е��ַ���
        /// </summary>
        /// <param name="lpAppName">����</param>
        /// <param name="lpKeyName">����</param>
        /// <param name="lpDefault">Ĭ��ֵ</param></param>
        /// <param name="lpReturnedString">��ŷ���ֵ�Ļ�����</param>
        /// <param name="nSize">��������С</param>
        /// <param name="lpFileName">Ini�ļ�</param>
        /// <returns>Ini�ļ������õ�ֵ</returns>
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        /// <summary>
        /// ����Ini�ļ��е�ֵ
        /// </summary>
        /// <param name="lpAppName">����</param>
        /// <param name="lpKeyName">����</param>
        /// <param name="lpString">ֵ</param>
        /// <param name="lpFileName">Ini�ļ�</param>
        /// <returns>TRUE: �ɹ�; FALSE: ʧ��</returns>
        [DllImport("kernel32")]
        public static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        #endregion


        #region Ini��������
        /// <summary>
        /// ��Ini�ļ��е�Intֵ
        /// </summary>
        /// <param name="section">����</param>
        /// <param name="key">����</param>
        /// <param name="def">Ĭ��ֵ</param>
        /// <returns>Ini�ļ������õ�ֵ</returns>
        public int ReadInt(string section, string key, int def)
        {
            return GetPrivateProfileInt(section, key, def, _configFile);
        }


        /// <summary>
        /// ��Ini�ļ��е��ַ���ֵ
        /// </summary>
        /// <param name="section">����</param>
        /// <param name="key">����</param>
        /// <param name="def">Ĭ��ֵ</param>
        /// <returns>Ini�ļ������õ�ֵ</returns>
        public string ReadString(string section, string key, string def)
        {
            StringBuilder strBuilder = new StringBuilder(VALUE_MAX_LEN);
            GetPrivateProfileString(section, key, def, strBuilder, VALUE_MAX_LEN, _configFile);        
            return strBuilder.ToString();
        }


        /// <summary>
        /// дIni�ļ�
        /// </summary>
        /// <param name="section">����</param>
        /// <param name="key">����</param>
        /// <param name="strVal">Ҫд����ַ���</param>
        public void WriteString(string section, string key, string strVal)
        {
            WritePrivateProfileString(section, key, strVal, _configFile);
        }


        /// <summary>
        /// ɾ����
        /// </summary>
        /// <param name="section">����</param>
        /// <param name="key">����</param>
        public void DelKey(string section, string key)
        {
            WritePrivateProfileString(section, key, null, _configFile);
        }


        /// <summary>
        /// ɾ����
        /// </summary>
        /// <param name="section">����</param>
        public void DelSection(string section)
        {
            WritePrivateProfileString(section, null, null, _configFile);
        }
        #endregion
    }
}
