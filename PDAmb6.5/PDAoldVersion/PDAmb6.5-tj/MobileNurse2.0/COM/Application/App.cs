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
using System.Diagnostics;

namespace HISPlus
{
	/// <summary>
	/// UniversalVarApp 的摘要说明。
	/// </summary>
	public class App
    {
        protected string    _name           = string.Empty;             // 应用程序的名称
        protected string    _title          = string.Empty;             // 应用程序的标题
        protected string    _copyRight      = string.Empty;             // 版权
        protected string    _version        = string.Empty;             // 版本
        
        protected DateTime  _dtNow          = DataType.DateTime_Null();

        protected string    _right          = string.Empty;             // 权限
        protected bool      _verified       = false;                    // 通过验证
        
        protected bool      _questionExit   = true;                     // 退出系统前是否询问
        
        protected bool      _userInput      = true;                     // 是否接下来的输入为用户输入
        
        public App()
		{
        }


        #region 属性
        public string Name
        {
            get { return _name;}
            set { _name = value;}
        }


        public string Title
        {
            get { return _title;}
            set { _title = value;}
        }


        public string CopyRight
        {
            get { return _copyRight;}
            set { _copyRight = value;}
        }


        public string Version
        {
            get { return _version;}
            set { _version = value;}
        }


        public DateTime Now
        {
            get { return _dtNow;}
            set { _dtNow = value;}
        }


        public string Right
        {
            get { return _right;}
            set { _right = value;}
        }


        public bool Verified
        {
            get { return _verified;}
            set { _verified = value;}
        }


        public bool QuestionExit
        {
            get { return _questionExit;}
            set { _questionExit = value;}
        }


        public bool UserInput
        {
            get { return _userInput;}
            set { _userInput = value;}
        }
        #endregion
    }
}
