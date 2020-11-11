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
        // 应用程序
        public      static AppCur          App             = new AppCur();                      // 应用程序

        // 数据库访问
        public      static DbAccess        OracleAccess;                                        // 与数据库的连接
        public      static DbAccess        SqlserverAccess;                                     // 与Sqlserver数据库的连接
        
        // 应用组件1
        public      static IniFile         IniFile         = new IniFile();                     // 本地配置文件
        public      static Message         Msg             = new Message();                     // 消息对象

		public      static UserCls         User            = new UserCls();                     // 用户
        public      static UserDbI         UserDbI;                                             // 与用户有关的数据访问
        
        public      static HospitalDbI     HospitalDbI;
        
        // 应用组件2
        public      static EnDecrypt       EnDecryptor     = new EnDecrypt();                   // 加密器
        public      static OrderSplitter   OrderConvertor  = new OrderSplitter();               // 医嘱拆分器
        
        
        public GVars()
		{
		}

        public static void ReloadDbSetting()
        {
            UserDbI = new UserDbI(OracleAccess, SqlserverAccess);
            HospitalDbI = new HospitalDbI(OracleAccess);
            
            GVars.OrderConvertor.OracleAccess = GVars.OracleAccess;
        }
	}
}
