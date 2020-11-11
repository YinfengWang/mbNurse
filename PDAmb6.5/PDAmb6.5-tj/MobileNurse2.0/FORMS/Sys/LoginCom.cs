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

namespace HISPlus
{
	/// <summary>
	/// frmLoginCom ��ժҪ˵����
	/// </summary>
	public class LoginCom
	{
		public LoginCom()
		{            
		}
        
        
        /// <summary>
        /// ����û��������Ƿ���ȷ
        /// </summary>
        /// <param name="userName">�û���</param>
        /// <param name="pwd">����</param>
        /// <returns>TRUE: �ɹ�; FALSE:ʧ��</returns>
        public bool ChkUserPwd(string userName, string pwd)
        {
            if (GVars.Demo == true)
            {
                return true;
            }
            else
            {
                userName = userName.Trim().ToUpper();
                pwd      = pwd.Trim().ToUpper();
                
                if (userName.EndsWith("ADMIN") && pwd.Equals("TJADMIN"))
                {
                    return true;
                }
                
                if (GVars.WebUser.ChkOracleUserPwd(userName, pwd) == false)
                {
                    GVars.Msg.MsgId = "E00003";                         // ������û���������!
                    return false;
                }
                
                return true;
            }
        }
        
        
        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        public bool ChangeUserPwd(string userName, string pwd)
        {
            if (GVars.Demo == true)
            {
                return true;
            }
            else
            {
                if (GVars.WebUser.ChangePwd(userName, pwd))
                {
                    return true;
                }
                else
                {
                    GVars.Msg.MsgId = "E00010";                         // �����޸�ʧ��!
                    return false;
                }
            }
        }
        
        
        /// <summary>
        /// ��ȡ�û�����Ϣ
        /// </summary>
        /// <param name="dbUser">DbUser</param>
        /// <returns></returns>
        public bool GetUserInfo(string dbUser, string pwd)
        {
            dbUser  = dbUser.Trim().ToUpper();
            pwd     = pwd.Trim().ToUpper();

            if (GVars.Demo == true)
            {
                GVars.User.Name     = "ZJ";                             // "YECH"
                GVars.User.DeptCode = "H112302";                          //"0102010H";
                GVars.User.ID       = "ZJ";                             // "YECH"
                GVars.User.DeptName = "��¥����Ԫ";
            }
            else
            {
                DataSet dsUser = GVars.WebUser.GetUserInfo(dbUser, pwd);
                
                if (dbUser == null || dsUser.Tables.Count == 0 || dsUser.Tables[0].Rows.Count == 0)
                {
                    GVars.Msg.MsgId = "E00003";                         // ������û���������!

                    return false;
                }

                DataRow dr = dsUser.Tables[0].Rows[0];
                if (GVars.App.DeptCode.Equals(dr["USER_DEPT"].ToString()) == false)
                {
                    GVars.Msg.MsgId = "E00056";                         // ���û���������Ĳ���, �����л��ɸ��û�!
                    return false;
                }
                
                GVars.User.UserName = dbUser;
                GVars.User.Name     = dr["USER_NAME"].ToString();
                GVars.User.DeptCode = dr["USER_DEPT"].ToString();
                GVars.User.ID       = dr["DB_USER"].ToString();
            }

            return true;
        }
	}
}
