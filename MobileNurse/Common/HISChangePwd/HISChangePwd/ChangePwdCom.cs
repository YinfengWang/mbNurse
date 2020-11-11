//------------------------------------------------------------------------------------
//
//  系统名称        : 医院信息系统
//  子系统名称      : 通用模块
//  对象类型        : 
//  类名            : frmLoginCom.cs
//  功能概要        : 登录界面共通模块
//  作成者          : 付军
//  作成日          : 2007-05-23
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        :
//  变更者          :
//  变更内容        :
//  版本            :
//------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Data;

namespace HISPlus
{
	/// <summary>
	/// frmLoginCom 的摘要说明。
	/// </summary>
	public class ChangePwdCom
	{
        private UserDbI userDbI = null;

		public ChangePwdCom()
		{
            userDbI = new UserDbI(GVars.OracleAccess);
		}
        
        
        ///// <summary>
        ///// 检查用户名密码是否正确
        ///// </summary>
        ///// <param name="userName">用户名</param>
        ///// <param name="pwd">密码</param>
        ///// <returns>TRUE: 成功; FALSE:失败</returns>
        //public bool ChkUserPwd(string userName, string pwd)
        //{
        //    userName = userName.Trim().ToUpper();
        //    pwd      = pwd.Trim().ToUpper();

        //    return userDbI.ChkPwd(userName, pwd);
        //}
        

        ///// <summary>
        ///// 修改用户密码
        ///// </summary>
        ///// <returns></returns>
        //public bool ChangeUserPwd(string userName, string pwd)
        //{
        //    userName = userName.Trim().ToUpper();
        //    pwd      = pwd.Trim().ToUpper();

        //    return userDbI.ChangePwd(userName, pwd);
        //}
        
        
        
        /// <summary>
        /// 检查用户名密码是否正确
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>TRUE: 成功; FALSE:失败</returns>
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
        /// 修改用户密码
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
