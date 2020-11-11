//------------------------------------------------------------------------------------
//
//  系统名称        : 医院信息系统
//  子系统名称      : 通用模块
//  对象类型        : 
//  类名            : frmLoginCom.cs
//  功能概要        : 登录界面共通模块
//  作成者          : 付军
//  作成日          : 2007-05-23
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
using System.Data;
using System.Windows.Forms;
using Microsoft.Win32;

namespace HISPlus
{
	/// <summary>
	/// frmLoginCom 的摘要说明。
	/// </summary>
	public class LoginCom
	{
        // 窗体变量
		public LoginCom()
		{
		}
        
        
        /// <summary>
        /// 加载本地配置文件设置
        /// </summary>
        /// <returns></returns>
        public bool LoadAppSetting_Local()
        { 
            // 打开配置文件
            if (File.Exists(GVars.IniFile.FileName) == false)
            {
                GVars.Msg.MsgId = "ED001";
                GVars.Msg.MsgContent.Add(GVars.IniFile.FileName);       // 找不到初始化文件。请确认{0}位于执行路径下
                return false;
            }
            
            // 获取数据库连接字符串
            string oraConnectHis = getConnStr("DATABASE", "ORA_HIS");
            string oraConnectMobile = getConnStr("DATABASE", "ORA_MOBILE");
            
            // 数据库连接
            GVars.OracleMobile = new OracleAccess();
            GVars.OracleMobile.ConnectionString = oraConnectMobile;
            GVars.OracleHis    = new OleDbAccess();

            string dataSrc = ConnectionStringHelper.GetParameter(oraConnectHis, "Data Source");
            string user     = ConnectionStringHelper.GetParameter(oraConnectHis, "User ID");
            string pwd      = ConnectionStringHelper.GetParameter(oraConnectHis, "password").ToLower();
            //provider=msdaora;SERVER=172.16.58.4;Data Source=hisrun;User ID=ydhl;Password=ydhl
            //GVars.OracleHis.ConnectionString = "provider=sqloledb;SERVER=172.16.58.4;database=hisrun;User ID=ydhl;Password=ydhl";//河北工程大学sql连接
            //GVars.OracleHis.ConnectionString = oraConnectHis; //河北工程大学sql连接
            GVars.OracleHis.ConnectionString = ConnectionStringHelper.GetOleConnectionString(dataSrc, user, pwd).Replace("provider=msdaora;", "provider=OraOLEDB.Oracle;");
            
            // 设置数据接口
            // MessageBox.Show(GVars.OracleAccess.ConnectionString);
            
            // 获取科室ID
            GVars.User.DeptCode = GVars.IniFile.ReadString("APP", "WARD_CODE", string.Empty);
            //liubo 20110109针对界面不能显示哪个病区的拆分添加
            HospitalDbI hospitalDbI = new HospitalDbI(GVars.OracleMobile);
            GVars.User.DeptName = hospitalDbI.Get_DeptName(GVars.User.DeptCode);

            //添加结束
            return true;
        }

        
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
	}
}
