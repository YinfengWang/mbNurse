//------------------------------------------------------------------------------------
//
//  系统名称        : 医生工作站
//  子系统名称      : 通用模块
//  对象类型        : 
//  类名            : IniFile.cs
//  功能概要        : Ini文件操作
//  作成者          : 付军
//  作成日          : 2007-01-25
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        :
//  变更者          :
//  变更内容        :
//  版本            :
//------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace HISPlus
{
    /// <summary>
    /// IniFile 的摘要说明。
    /// </summary>
    public class IniFile
    {
        #region 变量
        protected const int VALUE_MAX_LEN = 1024;					// Ini文件中值的最大长度

        protected string _configFile = "Config.ini";         // 配置文件
        #endregion


        public IniFile()
        {
        }


        public IniFile(string fileName)
        {
            _configFile = fileName;
        }


        #region 属性
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
        /// 获取Ini文件中的数字值
        /// </summary>
        /// <param name="lpAppName">段名</param>
        /// <param name="lpKeyName">键名</param>
        /// <param name="nDefault">默认值</param>
        /// <param name="lpFileName">Ini文件</param>
        /// <returns>Ini文件中设置的值</returns>
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

        /// <summary>
        /// 获取Ini文件中的字符串
        /// </summary>
        /// <param name="lpAppName">段名</param>
        /// <param name="lpKeyName">键名</param>
        /// <param name="lpDefault">默认值</param></param>
        /// <param name="lpReturnedString">存放返回值的缓冲区</param>
        /// <param name="nSize">缓冲区大小</param>
        /// <param name="lpFileName">Ini文件</param>
        /// <returns>Ini文件中设置的值</returns>
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        /// <summary>
        /// 设置Ini文件中的值
        /// </summary>
        /// <param name="lpAppName">段名</param>
        /// <param name="lpKeyName">键名</param>
        /// <param name="lpString">值</param>
        /// <param name="lpFileName">Ini文件</param>
        /// <returns>TRUE: 成功; FALSE: 失败</returns>
        [DllImport("kernel32")]
        public static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        #endregion


        #region Ini操作函数
        /// <summary>
        /// 读Ini文件中的Int值
        /// </summary>
        /// <param name="section">段名</param>
        /// <param name="key">键名</param>
        /// <param name="def">默认值</param>
        /// <returns>Ini文件中设置的值</returns>
        public int ReadInt(string section, string key, int def)
        {
            return GetPrivateProfileInt(section, key, def, _configFile);
        }


        /// <summary>
        /// 读Ini文件中的字符串值
        /// </summary>
        /// <param name="section">段名</param>
        /// <param name="key">键名</param>
        /// <param name="def">默认值</param>
        /// <returns>Ini文件中设置的值</returns>
        public string ReadString(string section, string key, string def)
        {
            StringBuilder strBuilder = new StringBuilder(VALUE_MAX_LEN);
            GetPrivateProfileString(section, key, def, strBuilder, VALUE_MAX_LEN, _configFile);        
            return strBuilder.ToString();
        }


        /// <summary>
        /// 写Ini文件
        /// </summary>
        /// <param name="section">段名</param>
        /// <param name="key">键名</param>
        /// <param name="strVal">要写入的字符串</param>
        public void WriteString(string section, string key, string strVal)
        {
            WritePrivateProfileString(section, key, strVal, _configFile);
        }


        /// <summary>
        /// 删除键
        /// </summary>
        /// <param name="section">段名</param>
        /// <param name="key">键名</param>
        public void DelKey(string section, string key)
        {
            WritePrivateProfileString(section, key, null, _configFile);
        }


        /// <summary>
        /// 删除段
        /// </summary>
        /// <param name="section">段名</param>
        public void DelSection(string section)
        {
            WritePrivateProfileString(section, null, null, _configFile);
        }
        #endregion
    }
}
