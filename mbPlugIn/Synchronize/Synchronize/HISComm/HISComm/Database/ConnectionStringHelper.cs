using System;
using Microsoft.Win32;

namespace HISPlus
{
	/// <summary>
	/// ���ݿ������ַ�����������̼���
	/// </summary>
	/// <remarks>����������һЩ�������ݿ������ַ���������,
	/// ��������OLEDB���ݿ������ַ����ĺ���,�������ݿ������ַ����ĺ���,
	/// �����󲻿�ʵ����,�����п��õĳ�Ա���Ǿ�̬��,��ֱ�ӵ���</remarks>
	public sealed class ConnectionStringHelper
	{        
		/// <summary>
		/// ��������ʵ����
		/// </summary>
		private ConnectionStringHelper()
		{
		}


		/// <summary>
		/// ���һ��Jet40(Access2000)�����ݿ������ַ���
		/// </summary>
		/// <param name="strFileName">���ݿ��ļ���</param>
		/// <param name="strPassword">���룬���������������ʹ�ÿ�����(null)</param>
		/// <returns>Jet40�����ݿ������ַ���</returns>
		public static string GetJet40ConnectionString(string fileName, string pwd)
		{
			string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName;

            if (pwd != null && pwd.Length > 0)
            {
                strConn = strConn + ";Jet OLEDB:Database password=" + pwd;
            }
            
            return strConn ;
		}


		/// <summary>
		/// ���һ��ORACLE�����ݿ������ַ���
		/// </summary>
		/// <param name="strSourceName">����Դ��</param>
		/// <param name="strUserName">�û���</param>
		/// <param name="strPassword">����</param>
		/// <returns>Oracle���ݿ������ַ���</returns>
		public static string GetOracleConnectionString(string tns, string userName, string pwd)
		{
            //return "Provider=OraOLEDB.Oracle.1;Password=" + pwd + ";Persist Security Info=True;User ID=" 
            //    + userName + ";Data Source=" + tns + ";Extended Properties=\"\"";

            return "Data Source=" + tns + ";User ID=" + userName + ";Password=" + pwd;
		}


		/// <summary>
		/// ���һ��SQLServer�����ݿ������ַ���
		/// </summary>
		/// <param name="strServerName">����������</param>
		/// <param name="strUserName">�û���</param>
		/// <param name="strDBName">���ݿ���</param>
		/// <param name="strPassword">����</param>
		/// <returns>SQLServer���ݿ������ַ���</returns>
		public static string GetSQLServerConnectionString(string serverName, string dbName, string userName, string pwd)
		{
            //return "Provider=SQLOLEDB.1;Password=" + pwd + ";Persist Security Info=True;User ID=" + userName 
            //    + ";Initial Catalog=" + dbName + ";Data Source=" + serverName ;

            return "SERVER=" + serverName + ";DATABASE=" + dbName + ";UID=" + userName + ";PWD=" + pwd;
		}


		/// <summary>
		/// ���һ������ Provider ���ֵ� SQLServer�����ݿ������ַ���,һ������ SqlConnection ����
		/// </summary>
		/// <remarks>ר������SQLServer�� System.Data.SqlClient.SqlConnection ʹ�õ����ݿ������ַ���
		/// û�� Provider ����,������ Oledb �� SQLServer ���ݿ������ַ���һ��,
		/// ��˱��������ݴ����Խ����˴���,���ɵ����ݿ������ַ���û�� Provider ����</remarks>
		/// <param name="strServerName">����������</param>
		/// <param name="strUserName">�û���</param>
		/// <param name="strDBName">���ݿ���</param>
		/// <param name="strPassword">����</param>
		/// <returns>SQLServer���ݿ������ַ���</returns>
		public static string GetSQLServerConnectionStringWithoutProvider(string serverName, string dbName, string userName, string pwd)
		{
			return "Password=" + pwd + ";Persist Security Info=True;User ID=" + userName 
                + ";Initial Catalog=" + dbName + ";Data Source=" + serverName ;
		}
		

        /// <summary>
		/// ���һ��ODBC��OLEDB���ݿ������ַ���
		/// </summary>
		/// <param name="strSource">����Դ����</param>
		/// <param name="strUserName">�û���</param>
		/// <param name="strPassword">����</param>
		/// <returns>ODBC��OLEDB���ݿ������ַ���</returns>
		public static string GetODBCConnectionString(string dataSource, string userName, string pwd)
		{
			return "Provider=MSDASQL.1;Password=" + pwd + ";Persist Security Info=True;User ID=" + userName 
                + ";Data Source=" + dataSource ;
		}


        public static string GetOleConnectionString(string dataSource, string userName, string pwd)
        {
          
            return "provider=msdaora;Data Source=" + dataSource + ";User ID=" + userName + ";Password=" + pwd;
        }


        public static string GetSqlCeConnectionString(string dbFile, string pwd)
        {
            return "Data Source = " + dbFile + @"; Password ='" + pwd + "';";
        }


		/// <summary>
		/// ������ݿ������ַ�����ָ���������ṩ������(Provider����ֵ)
		/// </summary>
		/// <param name="strConnection">���ݿ������ַ���</param>
		/// <returns>�����ṩ������</returns>
		public static string GetProvider(string connectionString)
		{
			return GetParameter(connectionString, "Provider");
		}


		/// <summary>
		/// ������ݿ������ַ�����ָ�����ƵĲ���ֵ
		/// </summary>
		/// <param name="strConnection">���ݿ������ַ���</param>
		/// <param name="strParameterName">��������</param>
		/// <returns>����ֵ,��������ָ�����ƵĲ����򷵻ؿ�����</returns>
		public static string GetParameter(string connectionString, string parameterName)
		{
		    parameterName = parameterName.ToUpper().Trim();
			string[] items = connectionString.ToUpper().Split(';');

			foreach( string item in items )
			{
			    if (item.Equals(parameterName) == true)
                {
                    return string.Empty;
                }

				int index = item.IndexOf('=');
				if( index > 0 )
				{
					string itemName = item.Substring(0 , index).Trim();
                    if (itemName.Equals(("Password")))
                    {
                        itemName = itemName.ToLower();
                    }
					if(itemName.Equals(parameterName) == true)
					{
						string itemValue = item.Substring(index + 1);
						return itemValue ;
					}
				}
			}

			return null;
		}
		
		
		/// <summary>
		/// ��ȡĬ�ϵ����ݿ������ַ���
		/// </summary>
		/// <returns></returns>
		public static bool CheckOracleConnection(ref OracleAccess conn)
		{
		    if (conn == null) conn = new OracleAccess();
		    if (conn.ConnectionString.Length == 0)
		    {
		        string subKey = "SOFTWARE\\Connection";
		        
		        RegistryKey regkey = Registry.LocalMachine.OpenSubKey(subKey);
                if (regkey == null)
                {
                    Registry.LocalMachine.CreateSubKey(subKey);
                    throw new Exception("����ע��� HKEY_LOCAL_MACHINE\\SOFTWARE\\Connection ���������ݿ�����!");
                }
                
                string connString = regkey.GetValue("Oracle").ToString();
                if (connString.Trim().Length == 0)
                {
                    throw new Exception("����ע��� HKEY_LOCAL_MACHINE\\SOFTWARE\\Connection ���������ݿ�����!");
                }
                
                EnDecrypt a = new EnDecrypt();
                connString = a.Decrypt(connString);
                
                conn.ConnectionString = connString;
		    }
		    
		    return true;
		}
	}
}