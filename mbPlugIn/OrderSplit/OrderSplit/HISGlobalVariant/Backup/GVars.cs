//------------------------------------------------------------------------------------
//
//  系统名称        : 医生工作站
//  子系统名称      : 通用模块
//  对象类型        : 
//  类名            : UniversalVarApp.cs
//  功能概要        : 应用程序配置
//  作成者          : 付军
//  作成日          : 2007-01-19
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        :
//  变更者          :
//  变更内容        :
//  版本            :
//------------------------------------------------------------------------------------
using System;
using System.Data;

namespace HISPlus
{
	/// <summary>
	/// UniversalVarApp 的摘要说明。
	/// </summary>
	public class GVars
	{
        /// <summary>
        /// 应用程序
        /// </summary>
        public      static App             App             = new App();                         // 应用程序


        /// <summary>
        /// Oracle数据库的访问对象
        /// </summary>
        public      static DbAccess        OracleHis;                                           // 与数据库的连接
        public      static DbAccess        OracleMobile;                                        // 与数据库的连接
        
        /// <summary>
        /// Ini文件
        /// </summary>
        public      static IniFile         IniFile         = new IniFile();                     // 本地配置文件


        /// <summary>
        /// 消息对象
        /// </summary>
        public      static Message         Msg             = new Message();                     // 消息对象


        /// <summary>
        /// 当前用户对象
        /// </summary>
		public      static UserCls         User            = new UserCls();                     // 用户
        
        
        /// <summary>
        /// 当前病人对象
        /// </summary>
        public      static PatientCls      Patient         = new PatientCls();                  // 病人
        
        
        /// <summary>
        /// 加密解密工具
        /// </summary>
        public      static EnDecrypt       EnDecryptor     = new EnDecrypt();                   // 加密器
        

        public GVars()
		{
		}
	}
}
