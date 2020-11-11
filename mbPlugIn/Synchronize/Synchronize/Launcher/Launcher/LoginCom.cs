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
using System.Windows.Forms;
using Microsoft.Win32;

namespace HISPlus
{
	/// <summary>
	/// frmLoginCom ��ժҪ˵����
	/// </summary>
	public class LoginCom
	{
        // �������
		public LoginCom()
		{
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
            string oraConnectHis = getConnStr("DATABASE", "ORA_HIS");
            string oraConnectMobile = getConnStr("DATABASE", "ORA_MOBILE");
            
            // ���ݿ�����
            GVars.OracleMobile = new OracleAccess();
            GVars.OracleMobile.ConnectionString = oraConnectMobile;
            GVars.OracleHis    = new OleDbAccess();

            string dataSrc = ConnectionStringHelper.GetParameter(oraConnectHis, "Data Source");
            string user     = ConnectionStringHelper.GetParameter(oraConnectHis, "User ID");
            string pwd      = ConnectionStringHelper.GetParameter(oraConnectHis, "password").ToLower();
            //provider=msdaora;SERVER=172.16.58.4;Data Source=hisrun;User ID=ydhl;Password=ydhl
            //GVars.OracleHis.ConnectionString = "provider=sqloledb;SERVER=172.16.58.4;database=hisrun;User ID=ydhl;Password=ydhl";//�ӱ����̴�ѧsql����
            //GVars.OracleHis.ConnectionString = oraConnectHis; //�ӱ����̴�ѧsql����
            GVars.OracleHis.ConnectionString = ConnectionStringHelper.GetOleConnectionString(dataSrc, user, pwd).Replace("provider=msdaora;", "provider=OraOLEDB.Oracle;");
            
            // �������ݽӿ�
            // MessageBox.Show(GVars.OracleAccess.ConnectionString);
            
            // ��ȡ����ID
            GVars.User.DeptCode = GVars.IniFile.ReadString("APP", "WARD_CODE", string.Empty);
            //liubo 20110109��Խ��治����ʾ�ĸ������Ĳ�����
            HospitalDbI hospitalDbI = new HospitalDbI(GVars.OracleMobile);
            GVars.User.DeptName = hospitalDbI.Get_DeptName(GVars.User.DeptCode);

            //��ӽ���
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
	}
}
