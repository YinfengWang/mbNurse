//------------------------------------------------------------------------------------
//
//  ϵͳ����        : ҽԺ��Ϣϵͳ
//  ��ϵͳ����      : ͨ��ģ��
//  ��������        : 
//  ����            : frmLoginCom.cs
//  ���ܸ�Ҫ        : ��¼���湲ͨģ��
//  ������          : ����
//  ������          : 2007-05-23
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
using System.Data;
using Encryption;
using Microsoft.Win32;

namespace HISPlus
{
    /// <summary>
    /// frmLoginCom ��ժҪ˵����
    /// </summary>
    public class frmLoginCom
    {
        // �������
        private UserDbI userDbI = null;
        private HospitalDbI hospitalDbI = null;
        private AppManagerDbI appDbI = null;
        /// <summary>
        /// ��Ȩ�������ڡ������һ�����ڣ�ÿ������һ�Ρ�
        /// </summary>
        private DateTime AUTH_REMIND_DAY;

        public frmLoginCom()
        {
            userDbI = new UserDbI(GVars.OracleAccess);
        }

        /// <summary>
        /// ���ر��������ļ�����
        /// </summary>
        /// <returns></returns>
        public bool LoadAppSetting_Local()
        {
            // �������ļ�
            if (File.Exists(GVars.IniFile.FileName) == false)
            {
                GVars.Msg.MsgId = "ED001";
                GVars.Msg.MsgContent.Add(GVars.IniFile.FileName);       // �Ҳ�����ʼ���ļ�����ȷ��{0}λ��ִ��·����
                return false;
            }

            // ��ȡ���ݿ������ַ���
            string oraConnectStr = getConnStr("DATABASE", "ORA_CONNECT");

            // �ͻ����Ƿ����Oracle�ַ���
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

            //GVars.OracleAccess.KeepConnection = false;           // �������ݿ�����
            GVars.OracleAccess.DisConnect();
            GVars.OracleAccess.ConnectionString = oraConnectStr;

            // ��ͬ�ַ����Ĵ���
            setOracleNlsLang();

            // �������ݽӿ�
            // MessageBox.Show(GVars.OracleAccess.ConnectionString);

            userDbI = new UserDbI(GVars.OracleAccess);
            hospitalDbI = new HospitalDbI(GVars.OracleAccess);
            appDbI = new AppManagerDbI(GVars.OracleAccess);

            // ��ȡ����ID
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
        /// ��ȡ������Ч��
        /// </summary>
        /// <returns></returns>
        public int GetValidityDate()
        {
            string key = appDbI.GetKey();

            string splitFlag = "|";
            if (!key.Contains(splitFlag))
                throw new Exception("��Ч��Ȩ!");

            string[] keys = key.Split('|');
            if (key.Length < 3)
                throw new Exception("��Ч��Ȩ!��Ч����!");

            //GVars.App


            //    throw new Exception("�Ƿ���Ȩ!");

            //Console.WriteLine("���ܣ�" + RsaHelper.DecryptString(en, "AwEAAazZlydqserfdtfGZ+WKeZxTgOO0hcP0tXbd4rrxEp7XtvfGidIsEFybINvSL3crJ/IpHEmHWy7ch0U0K2P2AX5LIwvbq+AbWU1SmkFNNNMjmZB/bFREKwteW/vuOtypErE9+23TRcqAjVLndfI8I2C+QYej86ks+vp8a7Dp7nk1") + "\r\n");

            return 1;
        }

        /// <summary>
        /// ����û��������Ƿ���ȷ
        /// </summary>
        /// <param name="userName">�û���</param>
        /// <param name="pwd">����</param>
        /// <returns>TRUE: �ɹ�; FALSE:ʧ��</returns>
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
        /// �޸��û�����
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
        /// ��ȡ�û�����Ϣ
        /// </summary>
        /// <param name="dbUser">DbUser</param>
        /// <returns></returns>
        public bool GetUserInfo(string dbUser)
        {
            // ��ȡ�û���Ϣ
            DataSet dsUserInfo = userDbI.GetUserInfo(dbUser);

            //��ȡ�û����ڵĿ����б�
            DataSet dsUserGroup = userDbI.GetUserGroup(dbUser);
            if (dsUserGroup != null && dsUserGroup.Tables[0].Rows.Count > 0)
            {
                GVars.User.DsDepts = dsUserGroup;
            }
            else
            {
                
                
                //���Ϊ��:�����Լ��Ŀ��Ҵ���
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

                // ˢ�������Ϣ
                GVars.User.Refresh();
            }

            // userDbI.GetUserInfo(dbUser, ref GVars.User);

            // ��ȡ�û�Ȩ��
            // GVars.User.Rights = userDbI.GetUserRights(GVars.App.Right, GVars.User.ID);
            GVars.User.Rights = userDbI.GetUserRights("001", GVars.User.ID);

            return true;
        }


        /// <summary>
        /// ���Ӧ�ó������Ȩ
        /// </summary>
        /// <returns></returns>
        public bool CheckAuthorization()
        {
            // ��ȡ����ID��
            if (GVars.App.Right.Trim().Length == 0)
            {
                GVars.Msg.MsgId = "E";
                GVars.Msg.MsgContent.Add("������ Application.xml�нڵ�RIGHT_ID");
                return false;
            }

            // ��ȡ��������
            GVars.App.Name = appDbI.GetAppName(GVars.App.Right);
            if (GVars.App.Name.Trim().Length == 0)
            {
                GVars.Msg.MsgId = "E";
                GVars.Msg.MsgContent.Add("��û�й����ƶ�����ϵͳ�������Ҫʹ�ã����빩Ӧ����ϵ��");
                return false;
            }

            // ��ȡ��Ȩ��
            string key = appDbI.GetAppAuthorizeCode(GVars.App.Name);
            if (key.Trim().Length == 0)
            {
                GVars.Msg.MsgId = "E";
                GVars.Msg.MsgContent.Add("��û���ƶ�����ϵͳע���룬�����Ҫʹ�ã����빩Ӧ����ϵ��");
                return false;
            }

            // �ж�ҽԺ����
            string hospitalName = hospitalDbI.GetHospitalName();
            if (hospitalName.Trim().Length == 0)
            {
                GVars.Msg.MsgId = "E";
                GVars.Msg.MsgContent.Add("������ҽԺ���ơ�");

                return false;
            }


            //string rtn = Md5.EncryptC("�����ҽԺT_"+dd, null);
            // ��̨Ӧ�ó���У��                        
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
                        GVars.Msg.MsgContent.Add("�ƶ�����ϵͳ���ڣ����ܼ���ʹ�ã����������빩Ӧ����ϵ��");

                        return false;
                    }

                    if (lastDate <= 30 && AUTH_REMIND_DAY < currentDate)
                    {
                        GVars.Msg.MsgId = "I";
                        GVars.Msg.MsgContent.Add("�ƶ�����ϵͳ���� " + lastDate + " ��ʹ��Ȩ,�뼰ʱ�빩Ӧ����ϵ��");
                        GVars.Msg.Show();
                        return true;
                    }
                }
                else
                    reason = "Ӧ�ó�����Ч��Ȩ!";
            }
            else
                reason = "Ӧ�ó�����Ч��Ȩ!";

            if (reason.Length > 0)
            {
                GVars.Msg.MsgId = "E";
                //GVars.Msg.MsgContent.Add("�Ƿ���Ȩ�������Ҫʹ�ã����빩Ӧ����ϵ��");
                GVars.Msg.MsgContent.Add("��û�й����ƶ�����ϵͳ�������Ҫʹ�ã����빩Ӧ����ϵ��");
                return false;
            }

            return true;


            // ����            
            key = EnDecrypt.JhaRdecrypt(key);
            //Ϊ�˱��ڵ��ԣ�����ط�ʹ�õ��Ƕ���������ҽԺ�ģ��ļ��ܲ���hisͳһ�ķ���,��ʽʹ�ð���ע�͵�
            //key = "DAXRMYYMOBILENURSING20301231";
            if (key.Trim().Length == 0)
            {
                GVars.Msg.MsgId = "E";
                GVars.Msg.MsgContent.Add("���ܴ������빩Ӧ����ϵ��");

                return false;
            }



            hospitalName = Coding.GetChineseSpell(hospitalName);
            if (key.StartsWith(hospitalName) == false)
            {
                GVars.Msg.MsgId = "E";
                GVars.Msg.MsgContent.Add("��û�й���ҽԺ��Ϣϵͳ�������Ҫʹ�ã����빩Ӧ����ϵ��");
                //((IDisposable)HISPlus.EnDecrypt).Dispose();
                return false;
            }

            key = key.Substring(hospitalName.Length);

            // �жϳ�������
            if (key.StartsWith(GVars.App.Name) == false)
            {
                GVars.Msg.MsgId = "E";
                GVars.Msg.MsgContent.Add("��û�й����ƶ�����ϵͳ�������Ҫʹ�ã����빩Ӧ����ϵ��");

                return false;
            }

            key = key.Substring(GVars.App.Name.Length);

            // �ж�ʹ������
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

            // ���ڵ�����
            DateTime dtNow = GVars.OracleAccess.GetSysDate();

            TimeSpan tspan = dtUsage.Subtract(dtNow);

            if (tspan.TotalDays < 30)
            {
                if (tspan.TotalDays < 0)
                {
                    GVars.Msg.MsgId = "E";
                    GVars.Msg.MsgContent.Add("�ƶ�����ϵͳ���ڣ����ܼ���ʹ�ã����������빩Ӧ����ϵ��");

                    return false;
                }
                else
                {
                    GVars.Msg.MsgId = "I";
                    GVars.Msg.MsgContent.Add("�ƶ�����ϵͳ���� " + tspan.TotalDays.ToString() + " ��ʹ��Ȩ,�뼰ʱ�빩Ӧ����ϵ��");

                    GVars.Msg.Show();
                    return true;
                }
            }

            return true;
        }


        /// <summary>
        /// ��ȡ�����ַ���
        /// </summary>
        /// <returns></returns>
        private string getConnStr(string section, string key)
        {
            string connStr = GVars.IniFile.ReadString(section, key, string.Empty).Trim();

            if (connStr.Trim().Length == 0)
            {
                GVars.Msg.MsgId = "ED003";                              // {0}�ļ���û���ҵ����ݿ������ַ���!"
                GVars.Msg.MsgContent.Add(GVars.IniFile.FileName);
                return string.Empty;
            }

            try
            {
                connStr = GVars.EnDecryptor.Decrypt(connStr);
            }
            catch
            {
                GVars.Msg.MsgId = "ED004";                              // �����ַ�������ʧ��!
                return string.Empty;
            }

            return connStr;
        }


        #region Oracle�ַ�������
        private string oldNlslang = string.Empty;


        /// <summary>
        /// �����ַ���
        /// </summary>
        private void setOracleNlsLang()
        {
            // ��ͬ�ַ����Ĵ���
            string nlsLangKey = GVars.IniFile.ReadString("DATABASE", "ORA_NLS_LANG", string.Empty);

            if (nlsLangKey.Length > 0)
            {
                //GVars.OracleAccess.KeepConnection = false;           // �������ݿ�����
                GVars.OracleAccess.DisConnect();

                //GVars.OracleAccess.KeepConnection = true;           // �������ݿ�����

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
        /// �޸�Oracle�ַ���ΪӢ��
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
        /// �޸�Oracle�ַ���Ϊ����
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
        /// ��ԭOracle�ַ���
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
