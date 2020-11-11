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
        
        public UserDbI(DbAccess oraConnect)
        {
            _oraConnect = oraConnect;
        }
        

        #region ��ȡ�û���Ϣ
        /// <summary>
        /// ��ȡ�û���Ϣ
        /// </summary>
        /// <param name="patientId">�û���</param>
        /// <returns></returns>
        public DataSet GetUserInfo(string userName)
        {
            userName = userName.ToUpper().Trim();

            //StringBuilder sb = new StringBuilder(); 
            //sb.Append("SELECT DEPT_CODE, "                                                       ); // ���Ҵ���
            //sb.Append(    "NAME, "                                                               ); // ����
            //sb.Append(    "TITLE, "                                                              ); // ְ��
            //sb.Append(    "JOB, "                                                                ); // �������
            //sb.Append(    "USER_NAME, "                                                          ); // ��ϵͳ�û���
            //sb.Append(    "ID, "                                                                 ); // ������ԱID
            //sb.Append(    "PASSWORD "                                                            ); // ����
            //sb.Append("FROM STAFF_DICT "                                                         ); // ������Ա�ֵ�
            //sb.Append("WHERE USER_NAME = " + SQL.SqlConvert(userName)                            ); // ��ϵͳ�û���
            
            //return _oraConnect.SelectData(sb.ToString());
            
            string sql = "SELECT * FROM STAFF_DICT WHERE USER_NAME = " + SQL.SqlConvert(userName);
            return _oraConnect.SelectData(sql);
        }

        
        /// <summary>
        /// �����û���Ϣ
        /// </summary>
        /// <returns></returns>
        public bool SaveUserInfo(string userId, string workNo, string userName, string pwd, string inputNo)
        {
            DateTime dtNow = _oraConnect.GetSysDate();

            // ���浽����
            string sql = "SELECT * FROM USER_DICT WHERE USER_ID = " + SqlManager.SqlConvert(userId);
            DataSet ds = _oraConnect.SelectData(sql, "USER_DICT");

            DataRow drEdit = null;
            if (ds.Tables[0].Rows.Count == 0)
            {
                drEdit = ds.Tables[0].NewRow();
            }
            else
            {
                drEdit = ds.Tables[0].Rows[0];
            }

            drEdit["USER_ID"]   = userId;
            drEdit["WORK_NO"]   = workNo;
            drEdit["USER_NAME"] = userName;
            drEdit["PWD"]       = pwd;
            drEdit["INPUT_NO"]  = inputNo;

            if (ds.Tables[0].Rows.Count == 0)
            {
                drEdit["CREATE_DATE"] = dtNow;
                ds.Tables[0].Rows.Add(drEdit);
            }

            drEdit["UPDATE_DATE"] = dtNow;

            // ���浽Db
            _oraConnect.Update(ref ds, "USER_DICT", sql);
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
            return ChkPwd_Table(userName, pwd);
        }


        /// <summary>
        /// ����û����������Ƿ���ȷ
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool ChkPwd_Table(string userName, string pwd)
        { 
            string sql = "SELECT ID FROM STAFF_DICT ";
            sql += "WHERE UPPER(USER_NAME) = " + SQL.SqlConvert(userName);
            sql +=   "AND PASSWORD = " + SQL.SqlConvert(EnDecrypt.Encrypt_JW(pwd));
            
            if(_oraConnect.SelectValue(sql) == false)
            {
                //sql = "SELECT PASSWORD FROM STAFF_DICT ";
                //sql += "WHERE UPPER(USER_NAME) = " + SQL.SqlConvert(userName);
                
                //DataSet ds = _oraConnect.SelectData(sql);
                //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //{
                //    pwd = ds.Tables[0].Rows[0]["PASSWORD"].ToString();
                    
                //    throw new Exception("�û�:" + userName + " ����:" + EnDecrypt.Decrypt_JW(pwd) + " ���ܺ�:" + pwd);
                //}
                //else
                //{
                //    throw new Exception("û�ҵ��û�: " + userName);
                //}
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// ����û����������Ƿ���ȷ
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool ChkPwd_Oracle(string userName, string pwd)
        { 
            try
            {
                string userId = userName.ToUpper().Trim();
                pwd = pwd.ToUpper().Trim();
                
                // ��֯�����ַ���
                // user id=system;password=manager            
                string connStr = _oraConnect.ConnectionString.ToUpper();
                string[] arrParts = connStr.Split(";".ToCharArray());
                
                for (int i = 0; i < arrParts.Length; i++)
                {
                    string[] arrSubParts = arrParts[i].Split("=".ToCharArray());

                    arrSubParts[0] = arrSubParts[0].Trim();
                    if (arrSubParts[0].ToUpper().Trim().IndexOf("USER") == 0)
                    {
                        arrSubParts[0] = "USER ID";
                        if (arrSubParts.Length > 1) { arrSubParts[1] = userId; }
                    }

                    if (arrSubParts[0].ToUpper().Trim().IndexOf("PASSWORD") == 0)
                    {
                        if (arrSubParts.Length > 1) { arrSubParts[1] = pwd; }
                    }

                    arrParts[i] = arrSubParts[0];
                    for (int j = 1; j < arrSubParts.Length; j++)
                    {
                        arrParts[i] += "=" + arrSubParts[j];
                    }
                }

                connStr = arrParts[0];
                for (int i = 1; i < arrParts.Length; i++)
                {
                    connStr += ";" + arrParts[i];
                }
                
                // ���Ӳ���
                _oraConnect.Connect(connStr);
                _oraConnect.DisConnect();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        public bool ChangePwd(string userName, string pwd)
        {
            return ChangePwd_Table(userName, pwd);
        }


        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        public bool ChangePwd_Table(string userName, string pwd)
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
        public bool ChangePwd_Oracle(string userName, string pwd)
        {
            try
            {
                string userId = userName.ToUpper().Trim();
                string newPwd = pwd.ToUpper().Trim();
                
                string strCmd = "ALTER USER " + userId + " IDENTIFIED BY " + newPwd;
                _oraConnect.ExecuteNoQuery(strCmd);
                
                return true;
            }
            catch (Exception ex)
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
        public DataSet GetUserRights(string appCode, string userId)
        { 
            StringBuilder sb = new StringBuilder(); 
            sb.Append("SELECT ROLE_RIGHTS.MODULE_CODE, "                                         );
            sb.Append(    "ROLE_RIGHTS.MODULE_RIGHT "                                            );
            sb.Append("FROM USER_DICT, "                                                         );
            sb.Append(    "USER_ROLES, "                                                         );
            sb.Append(    "ROLE_RIGHTS, "                                                        );
            sb.Append(    "APP_DICT, "                                                           );
            sb.Append(    "APP_VS_MODULE "                                                       );
            sb.Append("WHERE APP_DICT.APP_CODE = " + SqlManager.SqlConvert(appCode)              );
            sb.Append(    "AND USER_DICT.USER_ID = " + SqlManager.SqlConvert(userId)             );
            sb.Append(    "AND USER_DICT.USER_ID = USER_ROLES.USER_ID "                          );
            sb.Append(    "AND USER_ROLES.ROLE_ID = ROLE_RIGHTS.ROLE_ID "                        );
            sb.Append(    "AND APP_DICT.APP_CODE = APP_VS_MODULE.APP_CODE "                      );
            sb.Append(    "AND ROLE_RIGHTS.MODULE_CODE = APP_VS_MODULE.MODULE_CODE "             );
            
            string sql = sb.ToString();
            return _oraConnect.SelectData_NoKey(sql);
        }


        public DataSet GetUserRoles(string userId)
        { 
            string sql = "SELECT * FROM USER_ROLES WHERE USER_ID = " + SqlManager.SqlConvert(userId);

            return _oraConnect.SelectData(sql, "USER_ROLES");
        }


        public bool SaveUserRoles(ref DataSet dsUserRoles, string userId)
        {
            string sql = "SELECT * FROM USER_ROLES WHERE USER_ID = " + SqlManager.SqlConvert(userId);

            _oraConnect.Update(ref dsUserRoles, "USER_ROLES", sql);

            return true;
        }
        #endregion
    }
}
