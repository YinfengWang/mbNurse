//------------------------------------------------------------------------------------
//
//  ϵͳ����        : ҽ������վ
//  ��ϵͳ����      : ͨ��ģ��
//  ��������        : 
//  ����            : UniversalVarApp.cs
//  ���ܸ�Ҫ        : Ӧ�ó�������
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
using System.Data;
using System.Diagnostics;
using System.IO;

namespace HISPlus
{
	/// <summary>
	/// UniversalVarApp ��ժҪ˵����
	/// </summary>
	public class AppCur : App
    {
        protected bool _oraUserManager  = false;                        // �Ƿ���Ora�����û�
        protected bool _nls_zhs         = false;                        // �ͻ����Ƿ���������ַ���

        public AppCur()
		{
        }


        #region ����
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


        #region �ӿ�
        /// <summary>
        /// ���ر��������ļ�����
        /// </summary>
        /// <returns></returns>
        public bool LoadLocalSetting()
        { 
            // �������ļ�
            if (File.Exists(GVars.IniFile.FileName) == false)
            {
                GVars.Msg.MsgId = "ED001";
                GVars.Msg.MsgContent.Add(GVars.IniFile);                // �Ҳ�����ʼ���ļ�����ȷ��{0}λ��·����
                return false;
            }
            
            // ��ȡ��������
            GVars.User.DeptCode = GVars.IniFile.ReadString("WARD", "WARD_CODE", string.Empty);
            
            // �Ƿ���Oracle�������û�
            GVars.App.OraUserManager     = !(GVars.IniFile.ReadString("APP", "APP3.3", "TRUE").ToUpper().Trim().Equals("TRUE"));
            
            // ��ȡ���ݿ������ַ���
            string oraConnectStr = getConnStr("DATABASE", "ORA_CONNECT");
            
            // ��ȡ���ݿ������ַ���
            if (GVars.SqlserverAccess == null)
            {
                GVars.SqlserverAccess = new SqlserverAccess();
            }

            GVars.SqlserverAccess.ConnectionString = getConnStr("DATABASE", "SQL_CONNECT");
            
            // �ͻ����Ƿ����Oracle�ַ���
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

            // �Ƿ��Զ����ҽ��
            Business.Function.AutoSplitOrders = (1 == GVars.IniFile.ReadInt("SETTING", "SPLITE_ORDERS", 0));

            // ��ʼ��
            GVars.ReloadDbSetting();

            return true;
        }
        #endregion


        #region ��ͨ����
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
        #endregion
    }
}
