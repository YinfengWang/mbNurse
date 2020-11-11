using System;
using Microsoft.Win32;

namespace HISPlus
{
	/// <summary>
	/// 数据库连接字符串的相关例程集合
	/// </summary>
	/// <remarks>本对象定义了一些处理数据库连接字符串的例程,
	/// 包括创建OLEDB数据库连接字符串的函数,分析数据库连接字符串的函数,
	/// 本对象不可实例化,其所有可用的成员都是静态的,可直接调用</remarks>
	public sealed class ConnectionStringHelper
	{        
		/// <summary>
		/// 本对象不能实例化
		/// </summary>
		private ConnectionStringHelper()
		{
		}


		/// <summary>
		/// 获得一个Jet40(Access2000)的数据库连接字符串
		/// </summary>
		/// <param name="strFileName">数据库文件名</param>
		/// <param name="strPassword">密码，若不存在密码则可使用空引用(null)</param>
		/// <returns>Jet40的数据库连接字符串</returns>
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
		/// 获得一个ORACLE的数据库连接字符串
		/// </summary>
		/// <param name="strSourceName">数据源名</param>
		/// <param name="strUserName">用户名</param>
		/// <param name="strPassword">密码</param>
		/// <returns>Oracle数据库连接字符串</returns>
		public static string GetOracleConnectionString(string tns, string userName, string pwd)
		{
            //return "Provider=OraOLEDB.Oracle.1;Password=" + pwd + ";Persist Security Info=True;User ID=" 
            //    + userName + ";Data Source=" + tns + ";Extended Properties=\"\"";

            return "Data Source=" + tns + ";User ID=" + userName + ";Password=" + pwd;
		}


		/// <summary>
		/// 获得一个SQLServer的数据库连接字符串
		/// </summary>
		/// <param name="strServerName">服务器名称</param>
		/// <param name="strUserName">用户名</param>
		/// <param name="strDBName">数据库名</param>
		/// <param name="strPassword">密码</param>
		/// <returns>SQLServer数据库连接字符串</returns>
		public static string GetSQLServerConnectionString(string serverName, string dbName, string userName, string pwd)
		{
            //return "Provider=SQLOLEDB.1;Password=" + pwd + ";Persist Security Info=True;User ID=" + userName 
            //    + ";Initial Catalog=" + dbName + ";Data Source=" + serverName ;

            return "SERVER=" + serverName + ";DATABASE=" + dbName + ";UID=" + userName + ";PWD=" + pwd;
		}


		/// <summary>
		/// 获得一个不带 Provider 部分的 SQLServer的数据库连接字符串,一般用于 SqlConnection 对象
		/// </summary>
		/// <remarks>专门连接SQLServer的 System.Data.SqlClient.SqlConnection 使用的数据库连接字符串
		/// 没有 Provider 部分,其他和 Oledb 的 SQLServer 数据库连接字符串一样,
		/// 因此本函数根据此特性进行了处理,生成的数据库连接字符串没有 Provider 部分</remarks>
		/// <param name="strServerName">服务器名称</param>
		/// <param name="strUserName">用户名</param>
		/// <param name="strDBName">数据库名</param>
		/// <param name="strPassword">密码</param>
		/// <returns>SQLServer数据库连接字符串</returns>
		public static string GetSQLServerConnectionStringWithoutProvider(string serverName, string dbName, string userName, string pwd)
		{
			return "Password=" + pwd + ";Persist Security Info=True;User ID=" + userName 
                + ";Initial Catalog=" + dbName + ";Data Source=" + serverName ;
		}
		

        /// <summary>
		/// 获得一个ODBC的OLEDB数据库连接字符串
		/// </summary>
		/// <param name="strSource">数据源名称</param>
		/// <param name="strUserName">用户名</param>
		/// <param name="strPassword">密码</param>
		/// <returns>ODBC的OLEDB数据库连接字符串</returns>
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
		/// 获得数据库连接字符串中指定的数据提供者名称(Provider参数值)
		/// </summary>
		/// <param name="strConnection">数据库连接字符串</param>
		/// <returns>数据提供者名称</returns>
		public static string GetProvider(string connectionString)
		{
			return GetParameter(connectionString, "Provider");
		}


		/// <summary>
		/// 获得数据库连接字符串中指定名称的参数值
		/// </summary>
		/// <param name="strConnection">数据库连接字符串</param>
		/// <param name="strParameterName">参数名称</param>
		/// <returns>参数值,若不存在指定名称的参数则返回空引用</returns>
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
		/// 获取默认的数据库连接字符串
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
                    throw new Exception("请在注册表 HKEY_LOCAL_MACHINE\\SOFTWARE\\Connection 中设置数据库连接!");
                }
                
                string connString = regkey.GetValue("Oracle").ToString();
                if (connString.Trim().Length == 0)
                {
                    throw new Exception("请在注册表 HKEY_LOCAL_MACHINE\\SOFTWARE\\Connection 中设置数据库连接!");
                }
                
                EnDecrypt a = new EnDecrypt();
                connString = a.Decrypt(connString);
                
                conn.ConnectionString = connString;
		    }
		    
		    return true;
		}
	}
}