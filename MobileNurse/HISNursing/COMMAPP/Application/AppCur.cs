//------------------------------------------------------------------------------------
//
//  系统名称        : 医生工作站
//  子系统名称      : 通用模块
//  对象类型        : 
//  类名            : UniversalVarApp.cs
//  功能概要        : 应用程序配置
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
using System.Data;
using System.Diagnostics;
using System.IO;

namespace HISPlus
{
	/// <summary>
	/// UniversalVarApp 的摘要说明。
	/// </summary>
	public class AppCur : App
    {
        protected bool _oraUserManager  = false;                        // 是否用Ora管理用户
        protected bool _nls_zhs         = false;                        // 客户端是否采用中文字符集

        public AppCur()
		{
        }


        #region 属性
        public bool OraUserManager
        {
            get { return _oraUserManager;}
            set { _oraUserManager = value;}
        }


        public bool OraNlsZhs
        {
            get { return _nls_zhs;}
            set { _nls_zhs = value;}
        }
        #endregion


        #region 接口
        /// <summary>
        /// 加载本地配置文件设置
        /// </summary>
        /// <returns></returns>
        public bool LoadLocalSetting()
        { 
            // 打开配置文件
            if (File.Exists(GVars.IniFile.FileName) == false)
            {
                GVars.Msg.MsgId = "ED001";
                GVars.Msg.MsgContent.Add(GVars.IniFile);                // 找不到初始化文件。请确认{0}位于路径下
                return false;
            }
            
            // 获取病区代码
            GVars.User.DeptCode = GVars.IniFile.ReadString("WARD", "WARD_CODE", string.Empty);
            
            // 是否用Oracle管理用用户
            GVars.App.OraUserManager     = !(GVars.IniFile.ReadString("APP", "APP3.3", "TRUE").ToUpper().Trim().Equals("TRUE"));
            
            // 获取数据库连接字符串
            string oraConnectStr = getConnStr("DATABASE", "ORA_CONNECT");
            
            // 获取数据库连接字符串
            if (GVars.SqlserverAccess == null)
            {
                GVars.SqlserverAccess = new SqlserverAccess();
            }

            GVars.SqlserverAccess.ConnectionString = getConnStr("DATABASE", "SQL_CONNECT");
            
            // 客户端是否采用Oracle字符集
            GVars.App.OraNlsZhs      = (GVars.IniFile.ReadString("APP", "ORA_NLS_ZHS", "TRUE").ToUpper().Trim().Equals("TRUE"));

            if (GVars.App.OraNlsZhs == false)
            {
                if (GVars.OracleAccess == null || GVars.OracleAccess.GetType() != typeof(OleDbAccess))
                {
                    GVars.OracleAccess = new OleDbAccess();
                }

                string dataSrc  = ConnectionStringHelper.GetParameter(oraConnectStr, "Data Source");
                string user     = ConnectionStringHelper.GetParameter(oraConnectStr, "User ID");
                string pwd      = ConnectionStringHelper.GetParameter(oraConnectStr, "password");

                GVars.OracleAccess.ConnectionString = ConnectionStringHelper.GetOleConnectionString(dataSrc, user, pwd);
            }
            else
            {
                if (GVars.OracleAccess == null || GVars.OracleAccess.GetType() != typeof(OracleAccess))
                {
                    GVars.OracleAccess = new OracleAccess();
                }

                GVars.OracleAccess.ConnectionString = oraConnectStr;
            }

            // 是否自动拆分医嘱
            Business.Function.AutoSplitOrders = (1 == GVars.IniFile.ReadInt("SETTING", "SPLITE_ORDERS", 0));

            // 初始化
            GVars.ReloadDbSetting();

            return true;
        }
        #endregion


        #region 共通函数
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
        private string getConnStr(string section, string key)
        { 
            string connStr = GVars.IniFile.ReadString(section, key, string.Empty).Trim();
            
            if (connStr.Trim().Length == 0)
            {
                GVars.Msg.MsgId = "ED003";                              // {0}文件中没有找到数据库连接字符串!"
                GVars.Msg.MsgContent.Add(GVars.IniFile.FileName);
                return string.Empty;
            }

            try
            {
                connStr = GVars.EnDecryptor.Decrypt(connStr);
            }
            catch
            {
                GVars.Msg.MsgId = "ED004";                              // 连接字符串解密失败!
                return string.Empty;
            }

            return connStr;
        }
        #endregion
    }
}
