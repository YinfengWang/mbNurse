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
using Encryption;
using Microsoft.Win32;

namespace HISPlus
{
    /// <summary>
    /// frmLoginCom 的摘要说明。
    /// </summary>
    public class frmLoginCom
    {
        // 窗体变量
        private UserDbI userDbI = null;
        private HospitalDbI hospitalDbI = null;
        private AppManagerDbI appDbI = null;
        /// <summary>
        /// 授权提醒日期。在最后一个月内，每天提醒一次。
        /// </summary>
        private DateTime AUTH_REMIND_DAY;

        public frmLoginCom()
        {
            userDbI = new UserDbI(GVars.OracleAccess);
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
            string oraConnectStr = getConnStr("DATABASE", "ORA_CONNECT");

            // 客户端是否采用Oracle字符集
            if ((GVars.IniFile.ReadString("APP", "ORA_NLS_ZHS", "TRUE").ToUpper().Trim().Equals("TRUE")) == false)
            {
                if (GVars.OracleAccess == null || GVars.OracleAccess.GetType() != typeof(OracleAccess))
                {
                    GVars.OracleAccess = new OracleAccess();
                }
            }
            else
            {
                if (GVars.OracleAccess == null || GVars.OracleAccess.GetType() != typeof(OleDbAccess))
                {
                    GVars.OracleAccess = new OleDbAccess();
                }

                string dataSrc = ConnectionStringHelper.GetParameter(oraConnectStr, "Data Source");
                string user = ConnectionStringHelper.GetParameter(oraConnectStr, "User ID");
                string pwd = ConnectionStringHelper.GetParameter(oraConnectStr, "password");

                oraConnectStr = ConnectionStringHelper.GetOleConnectionString(dataSrc, user, pwd);
            }

            //GVars.OracleAccess.KeepConnection = false;           // 保持数据库连接
            GVars.OracleAccess.DisConnect();
            GVars.OracleAccess.ConnectionString = oraConnectStr;

            // 不同字符集的处理
            setOracleNlsLang();

            // 设置数据接口
            // MessageBox.Show(GVars.OracleAccess.ConnectionString);

            userDbI = new UserDbI(GVars.OracleAccess);
            hospitalDbI = new HospitalDbI(GVars.OracleAccess);
            appDbI = new AppManagerDbI(GVars.OracleAccess);

            // 获取科室ID
            GVars.User.DeptCode = GVars.IniFile.ReadString("APP", "WARD_CODE", string.Empty);
            GVars.User.DeptName = hospitalDbI.Get_DeptName(GVars.User.DeptCode);

            string day = GVars.IniFile.ReadString("APP", "AUTH_REMIND_DAY", string.Empty);
            DateTime.TryParse(day, out AUTH_REMIND_DAY);

            DateTime currentDate = GVars.OracleAccess.GetSysDate().Date;
            if (AUTH_REMIND_DAY == DateTime.MinValue)
            {
                AUTH_REMIND_DAY = currentDate;
            }
            if (AUTH_REMIND_DAY != currentDate.AddDays(1))
                GVars.IniFile.WriteString("APP", "AUTH_REMIND_DAY", currentDate.AddDays(1).ToShortDateString());

            return true;
        }

        /// <summary>
        /// 获取程序有效期
        /// </summary>
        /// <returns></returns>
        public int GetValidityDate()
        {
            string key = appDbI.GetKey();

            string splitFlag = "|";
            if (!key.Contains(splitFlag))
                throw new Exception("无效授权!");

            string[] keys = key.Split('|');
            if (key.Length < 3)
                throw new Exception("无效授权!无效参数!");

            //GVars.App


            //    throw new Exception("非法授权!");

            //Console.WriteLine("解密：" + RsaHelper.DecryptString(en, "AwEAAazZlydqserfdtfGZ+WKeZxTgOO0hcP0tXbd4rrxEp7XtvfGidIsEFybINvSL3crJ/IpHEmHWy7ch0U0K2P2AX5LIwvbq+AbWU1SmkFNNNMjmZB/bFREKwteW/vuOtypErE9+23TRcqAjVLndfI8I2C+QYej86ks+vp8a7Dp7nk1") + "\r\n");

            return 1;
        }

        /// <summary>
        /// 检查用户名密码是否正确
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>TRUE: 成功; FALSE:失败</returns>
        public bool ChkUserPwd(string userName, string pwd)
        {
            if (GVars.IniFile.ReadString("APP", "APP3.3", "").Trim().ToUpper().Equals("TRUE") == true)
            {
                return userDbI.ChkPwd(userName, pwd);
            }
            else
            {
                return userDbI.ChkPwd_Oracle(userName, pwd);
            }
        }


        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <returns></returns>
        public bool ChangeUserPwd(string userName, string pwd)
        {
            if (GVars.IniFile.ReadString("APP", "APP3.3", "").Trim().ToUpper().Equals("TRUE") == true)
            {
                return userDbI.ChangePwd(userName, pwd);
            }
            else
            {
                return userDbI.ChangePwd_Oracle(userName, pwd);
            }
        }


        /// <summary>
        /// 获取用户的信息
        /// </summary>
        /// <param name="dbUser">DbUser</param>
        /// <returns></returns>
        public bool GetUserInfo(string dbUser)
        {
            // 获取用户信息
            DataSet dsUserInfo = userDbI.GetUserInfo(dbUser);

            //获取用户所在的科室列表
            DataSet dsUserGroup = userDbI.GetUserGroup(dbUser);
            if (dsUserGroup != null && dsUserGroup.Tables[0].Rows.Count > 0)
            {
                GVars.User.DsDepts = dsUserGroup;
            }
            else
            {
                
                
                //如果为空:返回自己的科室代码
                //GVars.User.DsDepts = userDbI.GetUserDeptInfo(dbUser);
                return false;
            }
           
            if (dsUserInfo != null || dsUserInfo.Tables.Count > 0 || dsUserInfo.Tables[0].Rows.Count > 0)
            {
                DataRow drUser = dsUserInfo.Tables[0].Rows[0];

                GVars.User.ID = drUser["ID"].ToString();
                GVars.User.UserName = dbUser;
                GVars.User.Name = drUser["NAME"].ToString();
                //GVars.User.PWD      = drUser["PASSWORD"].ToString();

                foreach (DataRow dr in dsUserInfo.Tables[0].Rows)
                {
                    GVars.User.Add_Dept(dr["DEPT_CODE"].ToString(), dr["TITLE"].ToString(), dr["JOB"].ToString());
                }

                // 刷新相关信息
                GVars.User.Refresh();
            }

            // userDbI.GetUserInfo(dbUser, ref GVars.User);

            // 获取用户权限
            // GVars.User.Rights = userDbI.GetUserRights(GVars.App.Right, GVars.User.ID);
            GVars.User.Rights = userDbI.GetUserRights("001", GVars.User.ID);

            return true;
        }


        /// <summary>
        /// 检查应用程序的授权
        /// </summary>
        /// <returns></returns>
        public bool CheckAuthorization()
        {
            // 获取程序ID号
            if (GVars.App.Right.Trim().Length == 0)
            {
                GVars.Msg.MsgId = "E";
                GVars.Msg.MsgContent.Add("请设置 Application.xml中节点RIGHT_ID");
                return false;
            }

            // 获取程序名称
            GVars.App.Name = appDbI.GetAppName(GVars.App.Right);
            if (GVars.App.Name.Trim().Length == 0)
            {
                GVars.Msg.MsgId = "E";
                GVars.Msg.MsgContent.Add("您没有购买移动护理系统，如果您要使用，请与供应商联系。");
                return false;
            }

            // 获取授权码
            string key = appDbI.GetAppAuthorizeCode(GVars.App.Name);
            if (key.Trim().Length == 0)
            {
                GVars.Msg.MsgId = "E";
                GVars.Msg.MsgContent.Add("您没有移动护理系统注册码，如果您要使用，请与供应商联系。");
                return false;
            }

            // 判断医院名称
            string hospitalName = hospitalDbI.GetHospitalName();
            if (hospitalName.Trim().Length == 0)
            {
                GVars.Msg.MsgId = "E";
                GVars.Msg.MsgContent.Add("请设置医院名称。");

                return false;
            }


            //string rtn = Md5.EncryptC("赤峰市医院T_"+dd, null);
            // 后台应用程序校验                        
            string ticksStr;
            try
            {
                ticksStr = Md5.DecryptServer(key, hospitalName);
            }
            catch (Exception)
            {
                ticksStr = string.Empty;
            }
            string reason = string.Empty;
            if (ticksStr.Contains("T_"))
            {
                long ticks = 0;
                long.TryParse(ticksStr.Substring(ticksStr.IndexOf('_') + 1), out ticks);
                if (ticks > 0 &&
                    ticksStr.Substring(0, ticksStr.IndexOf("T_")) == hospitalName)
                {
                    DateTime keyDate = new DateTime(ticks);
                    DateTime currentDate = GVars.OracleAccess.GetSysDate();
                    int lastDate = (keyDate.Date - currentDate.Date).Days;

                    if (lastDate < 0)
                    {
                        GVars.Msg.MsgId = "E";
                        GVars.Msg.MsgContent.Add("移动护理系统到期，不能继续使用，请您立刻与供应商联系。");

                        return false;
                    }

                    if (lastDate <= 30 && AUTH_REMIND_DAY < currentDate)
                    {
                        GVars.Msg.MsgId = "I";
                        GVars.Msg.MsgContent.Add("移动护理系统还有 " + lastDate + " 天使用权,请及时与供应商联系。");
                        GVars.Msg.Show();
                        return true;
                    }
                }
                else
                    reason = "应用程序无效授权!";
            }
            else
                reason = "应用程序无效授权!";

            if (reason.Length > 0)
            {
                GVars.Msg.MsgId = "E";
                //GVars.Msg.MsgContent.Add("非法授权，如果您要使用，请与供应商联系。");
                GVars.Msg.MsgContent.Add("您没有购买移动护理系统，如果您要使用，请与供应商联系。");
                return false;
            }

            return true;


            // 解码            
            key = EnDecrypt.JhaRdecrypt(key);
            //为了便于调试，这个地方使用的是东阿县人民医院的，改加密采用his统一的方法,正式使用把它注释掉
            //key = "DAXRMYYMOBILENURSING20301231";
            if (key.Trim().Length == 0)
            {
                GVars.Msg.MsgId = "E";
                GVars.Msg.MsgContent.Add("解密错误，请与供应商联系。");

                return false;
            }



            hospitalName = Coding.GetChineseSpell(hospitalName);
            if (key.StartsWith(hospitalName) == false)
            {
                GVars.Msg.MsgId = "E";
                GVars.Msg.MsgContent.Add("您没有购买医院信息系统，如果您要使用，请与供应商联系。");
                //((IDisposable)HISPlus.EnDecrypt).Dispose();
                return false;
            }

            key = key.Substring(hospitalName.Length);

            // 判断程序名称
            if (key.StartsWith(GVars.App.Name) == false)
            {
                GVars.Msg.MsgId = "E";
                GVars.Msg.MsgContent.Add("您没有购买移动护理系统，如果您要使用，请与供应商联系。");

                return false;
            }

            key = key.Substring(GVars.App.Name.Length);

            // 判断使用日期
            int year = 0;
            int month = 0;
            int day = 0;

            if (key.Length >= 4)
            {
                int.TryParse(key.Substring(0, 4), out year);
                key = key.Substring(4);
            }

            if (key.Length >= 2)
            {
                int.TryParse(key.Substring(0, 2), out month);
                key = key.Substring(2);
            }

            if (key.Length >= 2)
            {
                int.TryParse(key.Substring(0, 2), out day);
            }

            DateTime dtUsage = DateTime.Now.AddYears(-1);
            if (year > 0 && month > 0 && day > 0)
            {
                dtUsage = new DateTime(year, month, day);
            }

            // 现在的日期
            DateTime dtNow = GVars.OracleAccess.GetSysDate();

            TimeSpan tspan = dtUsage.Subtract(dtNow);

            if (tspan.TotalDays < 30)
            {
                if (tspan.TotalDays < 0)
                {
                    GVars.Msg.MsgId = "E";
                    GVars.Msg.MsgContent.Add("移动护理系统到期，不能继续使用，请您立刻与供应商联系。");

                    return false;
                }
                else
                {
                    GVars.Msg.MsgId = "I";
                    GVars.Msg.MsgContent.Add("移动护理系统还有 " + tspan.TotalDays.ToString() + " 天使用权,请及时与供应商联系。");

                    GVars.Msg.Show();
                    return true;
                }
            }

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


        #region Oracle字符集处理
        private string oldNlslang = string.Empty;


        /// <summary>
        /// 设置字符集
        /// </summary>
        private void setOracleNlsLang()
        {
            // 不同字符集的处理
            string nlsLangKey = GVars.IniFile.ReadString("DATABASE", "ORA_NLS_LANG", string.Empty);

            if (nlsLangKey.Length > 0)
            {
                //GVars.OracleAccess.KeepConnection = false;           // 保持数据库连接
                GVars.OracleAccess.DisConnect();

                //GVars.OracleAccess.KeepConnection = true;           // 保持数据库连接

                try
                {
                    oldNlslang = oracleNlsLang_Zh(nlsLangKey);
                    GVars.OracleAccess.SelectValue("SELECT SYSDATE FROM DUAL");
                }
                catch
                {
                }
                finally
                {
                    oracleNlsLang_Restore(nlsLangKey, oldNlslang);
                }
            }
        }


        /// <summary>
        /// 修改Oracle字符集为英文
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string oracleNlsLang_En(string key)
        {
            string preValue = Registry.GetValue(key, "NLS_LANG", null).ToString();

            if (preValue.Length > 0)
            {
                Registry.SetValue(key, "NLS_LANG", "AMERICAN_AMERICA.US7ASCII");
            }

            return preValue;
        }


        /// <summary>
        /// 修改Oracle字符集为中文
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string oracleNlsLang_Zh(string key)
        {
            string preValue = Registry.GetValue(key, "NLS_LANG", null).ToString();

            if (preValue.Length > 0)
            {
                Registry.SetValue(key, "NLS_LANG", "SIMPLIFIED CHINESE_CHINA.ZHS16GBK");
            }

            return preValue;
        }


        /// <summary>
        /// 还原Oracle字符集
        /// </summary>
        /// <param name="key"></param>
        /// <param name="oldValue"></param>
        /// <returns></returns>
        private bool oracleNlsLang_Restore(string key, string oldValue)
        {
            if (oldValue.Length == 0)
            {
                return true;
            }

            Registry.SetValue(key, "NLS_LANG", oldValue);

            return true;
        }
        #endregion
    }
}
