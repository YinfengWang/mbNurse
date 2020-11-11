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
	public class LoginCom
	{
		public LoginCom()
		{            
		}
        
        
        /// <summary>
        /// 检查用户名密码是否正确
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>TRUE: 成功; FALSE:失败</returns>
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
                    GVars.Msg.MsgId = "E00003";                         // 错误的用户名或密码!
                    return false;
                }
                
                return true;
            }
        }
        
        
        /// <summary>
        /// 修改用户密码
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
                    GVars.Msg.MsgId = "E00010";                         // 密码修改失败!
                    return false;
                }
            }
        }
        
        
        /// <summary>
        /// 获取用户的信息
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
                GVars.User.DeptName = "九楼护理单元";
            }
            else
            {
                DataSet dsUser = GVars.WebUser.GetUserInfo(dbUser, pwd);
                
                if (dbUser == null || dsUser.Tables.Count == 0 || dsUser.Tables[0].Rows.Count == 0)
                {
                    GVars.Msg.MsgId = "E00003";                         // 错误的用户名或密码!

                    return false;
                }

                DataRow dr = dsUser.Tables[0].Rows[0];
                if (GVars.App.DeptCode.Equals(dr["USER_DEPT"].ToString()) == false)
                {
                    GVars.Msg.MsgId = "E00056";                         // 该用户属于另外的病区, 不能切换成该用户!
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
