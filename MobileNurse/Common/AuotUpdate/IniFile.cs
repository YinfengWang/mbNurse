using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoUpdate
{
    public class IniFile
    {
        protected string _configFile;
        protected const int VALUE_MAX_LEN = 0x400;

        public IniFile()
        {
            this._configFile = "AutoUpdate.ini";
        }

        public IniFile(string fileName)
        {
            this._configFile = "AutoUpdate.ini";
            this._configFile = fileName;
        }

        public void DelKey(string section, string key)
        {
            WritePrivateProfileString(section, key, null, this._configFile);
        }

        public void DelSection(string section)
        {
            WritePrivateProfileString(section, null, null, this._configFile);
        }

        [DllImport("kernel32")]
        public static extern int GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
        public int ReadInt(string section, string key, int def)
        {
            return GetPrivateProfileInt(section, key, def, this._configFile);
        }

        public string ReadString(string section, string key, string def)
        {
            StringBuilder lpReturnedString = new StringBuilder(0x400);
            GetPrivateProfileString(section, key, def, lpReturnedString, 0x400, this._configFile);
            return lpReturnedString.ToString();
        }

        [DllImport("kernel32")]
        public static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        public void WriteString(string section, string key, string strVal)
        {
            WritePrivateProfileString(section, key, strVal, this._configFile);
        }

        public string FileName
        {
            get
            {
                return this._configFile;
            }
            set
            {
                this._configFile = value;
            }
        }
    }
}

