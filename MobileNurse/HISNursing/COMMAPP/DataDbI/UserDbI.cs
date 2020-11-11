using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public class UserDbI
    {
        protected DbAccess  _oraConnect;
        protected DbAccess  _sqlConnect;

        protected bool      _oraUserManager = false;                    // 是否采用Oracle管理用户
        
        public UserDbI(DbAccess oraConnect, DbAccess sqlConnect)
        {
            _oraConnect = oraConnect;
            _sqlConnect = sqlConnect;
        }


        #region 属性
        public bool OraUserManager
        {
            get { return _oraUserManager;}
            set { _oraUserManager = value;}
        }
        #endregion


        #region 获取用户信息
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="patientId">用户名</param>
        /// <returns></returns>
        public DataSet GetUserInfo(string userName)
        {
            userName = userName.ToUpper().Trim();

            StringBuilder sb = new StringBuilder(); 
            sb.Append("SELECT DEPT_CODE, "                                                       ); // 科室代码
            sb.Append(    "NAME, "                                                               ); // 姓名
            sb.Append(    "TITLE, "                                                              ); // 职称
            sb.Append(    "JOB, "                                                                ); // 工作类别
            sb.Append(    "USER_NAME, "                                                          ); // 本系统用户名
            sb.Append(    "ID, "                                                                 ); // 工作人员ID
            sb.Append(    "PASSWORD, "                                                           ); // 密码
            sb.Append(    "SYS_FLAG "                                                            );
            sb.Append("FROM STAFF_DICT "                                                         ); // 工作人员字典
            sb.Append("WHERE USER_NAME = " + SQL.SqlConvert(userName)                            ); // 本系统用户名
            
            return _oraConnect.SelectData(sb.ToString());
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool GetUserInfo(string userName, ref UserCls user)
        {
            // 获取用户信息
            DataSet ds = GetUserInfo(userName);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }

            // 加载用户信息
            DataRow drUser = ds.Tables[0].Rows[0];

            user.ID         = drUser["ID"].ToString();
            user.UserName   = userName;
            user.Name       = drUser["NAME"].ToString();
            user.PWD        = drUser["PASSWORD"].ToString();            

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                user.Add_Dept(dr["DEPT_CODE"].ToString(), dr["TITLE"].ToString(), dr["JOB"].ToString());
            }

            // 刷新相关信息
            user.Refresh();

            return true;
        }
        #endregion


        #region 用户密码
        /// <summary>
        /// 检查用户名与密码是否正确
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool ChkPwd(string userName, string pwd)
        {
            if (_oraUserManager == false)
            {
                return chkPwd_Table(userName, pwd);
            }
            else
            {
                return chkPwd_Ora(userName, pwd);
            }
        }


        /// <summary>
        /// 检查用户名与密码是否正确
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        private bool chkPwd_Table(string userName, string pwd)
        { 
            string sql = "SELECT ID FROM STAFF_DICT ";
            sql += "WHERE UPPER(USER_NAME) = " + SQL.SqlConvert(userName);
            sql +=   "AND PASSWORD = " + SQL.SqlConvert(EnDecrypt.Encrypt_JW(pwd));
            
            return _oraConnect.SelectValue(sql);
        }


        /// <summary>
        /// 检查用户名与密码是否正确
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        private bool chkPwd_Ora(string userName, string pwd)
        {
            OracleAccess conn = new OracleAccess();

            try
            {                
                string dataSrc = ConnectionStringHelper.GetParameter(_oraConnect.ConnectionString, "Data Source");
                conn.ConnectionString = ConnectionStringHelper.GetOracleConnectionString(dataSrc, userName, pwd);

                conn.Connect();

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                conn.DisConnect();
            }
        }


        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <returns></returns>
        public bool ChangePwd(string userName, string pwd)
        {
            if (_oraUserManager == false)
            {
                return changePwd_Table(userName, pwd);
            }
            else
            {
                return changePwd_Ora(userName, pwd);
            }
        }


        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <returns></returns>
        private bool changePwd_Table(string userName, string pwd)
        {
            try
            {
                string sql = "UPDATE STAFF_DICT SET PASSWORD = " + SQL.SqlConvert(EnDecrypt.Encrypt_JW(pwd));
                sql += " WHERE USER_NAME = " + SQL.SqlConvert(userName.ToUpper());

                _oraConnect.ExecuteNoQuery(sql);

                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <returns></returns>
        private bool changePwd_Ora(string userName, string pwd)
        {
            try
            {
                string sql = "ALTER USER " + userName + " IDENTIFIED BY " + pwd;
                _oraConnect.ExecuteNoQuery(sql);
                
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        #endregion


        #region 用户权限
        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <returns></returns>
        public DataSet GetRights(string appRightId, string userId)
        { 
            string sql = string.Empty;

            sql = "SELECT * FROM USER_RIGHTS ";
            sql += "WHERE ";
            sql += "USER_ID = " + HISPlus.SqlManager.SqlConvert(userId);
            sql += " AND RIGHT_ID LIKE '" + appRightId + "%'";
            
            return _sqlConnect.SelectData(sql);
        }


        /// <summary>
        /// 检查是否在某一功能上具有更高权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public bool ChkSuperUser(string userId, string functionId)
        { 
            string sql = "SELECT RIGHT_ID FROM USER_RIGHTS " 
                        + "WHERE USER_ID = " + SqlManager.SqlConvert(userId)
                        + " AND RIGHT_ID LIKE '" + functionId + "%'";

            DataSet ds = _sqlConnect.SelectData(sql);

            if (ds == null || ds.Tables.Count == 0)
            {
                return false;
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["RIGHT_ID"].ToString().EndsWith("2") == true)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
    }
}
