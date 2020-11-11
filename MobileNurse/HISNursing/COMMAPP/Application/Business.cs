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
	public class Business
	{
        // 护理
        public struct Nurse
        {
            public static int EventStartTime;                                                   // 护理事件开始时间(主要护理事件)
        }


        // 用药途径
        public struct Administration
        {
            public static string Drug       = string.Empty;                                     // 服药途径
            public static string Inject     = string.Empty;                                     // 注射途径
            public static string Transfuse  = string.Empty;                                     // 输液途径
        }


        // 功能
        public struct Function
        {
            public static bool AutoSplitOrders = false;                                         // 是否自动拆分医嘱
        }

        public Business()
		{
		}                      
	}
}
