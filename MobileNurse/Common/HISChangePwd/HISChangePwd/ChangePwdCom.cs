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
	public class ChangePwdCom
	{
        private UserDbI userDbI = null;

		public ChangePwdCom()
		{
            userDbI = new UserDbI(GVars.OracleAccess);
		}
        
        
        ///// <summary>
        ///// ����û��������Ƿ���ȷ
        ///// </summary>
        ///// <param name="userName">�û���</param>
        ///// <param name="pwd">����</param>
        ///// <returns>TRUE: �ɹ�; FALSE:ʧ��</returns>
        //public bool ChkUserPwd(string userName, string pwd)
        //{
        //    userName = userName.Trim().ToUpper();
        //    pwd      = pwd.Trim().ToUpper();

        //    return userDbI.ChkPwd(userName, pwd);
        //}
        

        ///// <summary>
        ///// �޸��û�����
        ///// </summary>
        ///// <returns></returns>
        //public bool ChangeUserPwd(string userName, string pwd)
        //{
        //    userName = userName.Trim().ToUpper();
        //    pwd      = pwd.Trim().ToUpper();

        //    return userDbI.ChangePwd(userName, pwd);
        //}
        
        
        
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
	}
}
