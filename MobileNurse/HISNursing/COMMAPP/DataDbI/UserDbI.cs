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

        protected bool      _oraUserManager = false;                    // �Ƿ����Oracle�����û�
        
        public UserDbI(DbAccess oraConnect, DbAccess sqlConnect)
        {
            _oraConnect = oraConnect;
            _sqlConnect = sqlConnect;
        }


        #region ����
        public bool OraUserManager
        {
            get { return _oraUserManager;}
            set { _oraUserManager = value;}
        }
        #endregion


        #region ��ȡ�û���Ϣ
        /// <summary>
        /// ��ȡ�û���Ϣ
        /// </summary>
        /// <param name="patientId">�û���</param>
        /// <returns></returns>
        public DataSet GetUserInfo(string userName)
        {
            userName = userName.ToUpper().Trim();

            StringBuilder sb = new StringBuilder(); 
            sb.Append("SELECT DEPT_CODE, "                                                       ); // ���Ҵ���
            sb.Append(    "NAME, "                                                               ); // ����
            sb.Append(    "TITLE, "                                                              ); // ְ��
            sb.Append(    "JOB, "                                                                ); // �������
            sb.Append(    "USER_NAME, "                                                          ); // ��ϵͳ�û���
            sb.Append(    "ID, "                                                                 ); // ������ԱID
            sb.Append(    "PASSWORD, "                                                           ); // ����
            sb.Append(    "SYS_FLAG "                                                            );
            sb.Append("FROM STAFF_DICT "                                                         ); // ������Ա�ֵ�
            sb.Append("WHERE USER_NAME = " + SQL.SqlConvert(userName)                            ); // ��ϵͳ�û���
            
            return _oraConnect.SelectData(sb.ToString());
        }


        /// <summary>
        /// ��ȡ�û���Ϣ
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool GetUserInfo(string userName, ref UserCls user)
        {
            // ��ȡ�û���Ϣ
            DataSet ds = GetUserInfo(userName);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }

            // �����û���Ϣ
            DataRow drUser = ds.Tables[0].Rows[0];

            user.ID         = drUser["ID"].ToString();
            user.UserName   = userName;
            user.Name       = drUser["NAME"].ToString();
            user.PWD        = drUser["PASSWORD"].ToString();            

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                user.Add_Dept(dr["DEPT_CODE"].ToString(), dr["TITLE"].ToString(), dr["JOB"].ToString());
            }

            // ˢ�������Ϣ
            user.Refresh();

            return true;
        }
        #endregion


        #region �û�����
        /// <summary>
        /// ����û����������Ƿ���ȷ
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
        /// ����û����������Ƿ���ȷ
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
        /// ����û����������Ƿ���ȷ
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
        /// �޸��û�����
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
        /// �޸��û�����
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
        /// �޸��û�����
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


        #region �û�Ȩ��
        /// <summary>
        /// ��ȡ�û�Ȩ��
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
        /// ����Ƿ���ĳһ�����Ͼ��и���Ȩ��
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
