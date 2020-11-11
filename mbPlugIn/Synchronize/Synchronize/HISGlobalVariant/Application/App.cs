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
        
        protected bool      _oneInstance    = true;                     // 是否只运行一个实例

        protected DateTime  _dtNow          = DataType.DateTime_Null(); // 当前时间

        protected string    _right          = string.Empty;             // 权限
        protected bool      _verified       = false;                    // 通过验证
        
        protected bool      _maxMdiFrm      = true;                     // 是否最大化主窗体
        
        protected bool      _questionExit   = true;                     // 退出系统前是否询问
        
        protected bool      _userInput      = true;                     // 是否接下来的输入为用户输入
        
        protected bool      _resetLocalTime = false;                    // 是否用服务器时间设置本地时间
        
        protected DataSet   _dsParameters   = null;                     // 应用程序参数
        
        public App()
		{
        }


        #region 属性
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _name;}
            set { _name = value;}
        }


        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _title;}
            set { _title = value;}
        }


        /// <summary>
        /// 版权
        /// </summary>
        public string CopyRight
        {
            get { return _copyRight;}
            set { _copyRight = value;}
        }


        /// <summary>
        /// 版本
        /// </summary>
        public string Version
        {
            get { return _version;}
            set { _version = value;}
        }


        /// <summary>
        /// 是否只运行一次实例
        /// </summary>
        public bool OneInstance
        {
            get { return _oneInstance;}
            set { _oneInstance = value;}
        }


        /// <summary>
        ///  现在的时间
        /// </summary>
        public DateTime Now
        {
            get { return _dtNow;}
            set { _dtNow = value;}
        }


        /// <summary>
        /// 权限 (暂时无用)
        /// </summary>
        public string Right
        {
            get { return _right;}
            set { _right = value;}
        }


        /// <summary>
        /// 是否通过验证
        /// </summary>
        public bool Verified
        {
            get { return _verified;}
            set { _verified = value;}
        }


        /// <summary>
        /// 是否最大化主窗体
        /// </summary>
        public bool MaxMdiFrm
        {
            get
            {
                return _maxMdiFrm;
            }
            set
            {
                _maxMdiFrm = value;
            }
        }


        /// <summary>
        /// 退出前是否询问
        /// </summary>
        public bool QuestionExit
        {
            get { return _questionExit;}
            set { _questionExit = value;}
        }


        /// <summary>
        /// 是否是用户的输入
        /// </summary>
        public bool UserInput
        {
            get { return _userInput;}
            set { _userInput = value;}
        }


        /// <summary>
        /// 是否用服务器时间重新设置本地时间
        /// </summary>
        public bool ResetLocalTime
        {
            get { return _resetLocalTime; }
            set { _resetLocalTime = value;}
        }
        
        
        /// <summary>
        /// 应用程序参数
        /// </summary>
        public DataSet AppParameters
        {
            get {return _dsParameters; }
        }
        #endregion


        #region 方法
        public bool IsRun()
        {
            string procName     = Process.GetCurrentProcess().ProcessName;
            Process[] instances = Process.GetProcessesByName(procName);

            if (instances.Length > 1 && procName.IndexOf(ComConst.STR.POINT) <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 设置系统日期时间
        /// </summary>
        /// <param name="dtNew"></param>
        /// <returns></returns>
        public static bool SetSystemTime(DateTime dtNew)
        { 
            dtNew = dtNew.AddHours(-1 * TimeZone.CurrentTimeZone.GetUtcOffset(new DateTime(2001,09,01)).Hours);

            Win32API.SystemTime st = new Win32API.SystemTime();

            st.year         = Convert.ToUInt16(dtNew.Year);
            st.month        = Convert.ToUInt16(dtNew.Month);
            st.day          = Convert.ToUInt16(dtNew.Day);
            st.dayofweek    = Convert.ToUInt16(dtNew.DayOfWeek);
            st.hour         = Convert.ToUInt16(dtNew.Hour);// - TimeZone.CurrentTimeZone.GetUtcOffset(new DateTime(2001,09,01)).Hours);
            st.minute       = Convert.ToUInt16(dtNew.Minute);
            st.second       = Convert.ToUInt16(dtNew.Second);
            st.milliseconds = Convert.ToUInt16(dtNew.Millisecond);

            return Win32API.SetSystemTime(st);
        }
        
        
        /// <summary>
        /// 重新加载应用程序参数
        /// </summary>
        public void ReloadParameters()
        {
            string sql = "SELECT * FROM APP_CONFIG WHERE APP_CODE IS NULL OR APP_CODE = " + SqlManager.SqlConvert(_right);
            
            _dsParameters = GVars.OracleMobile.SelectData(sql);
        }
        
        
        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <returns></returns>
        public string GetParameters(string deptCode, string userId, string paramName)
        {
            // 条件检查
            if (paramName.Length == 0)
            {
                return string.Empty;
            }
            
            if (_dsParameters == null || _dsParameters.Tables.Count == 0)
            {
                return string.Empty;
            }
            
            // 生成过滤条件
            string filter = string.Empty;
            
            if (deptCode.Length > 0)
            {
                if (filter.Length > 0) filter += " AND ";
                
                filter += "(DEPT_CODE IS NULL OR DEPT_CODE = " + SqlManager.SqlConvert(deptCode) + ")";
            }
            
            if (userId.Length > 0)
            {
                if (filter.Length > 0) filter += " AND ";
                
                filter += "(USER_ID IS NULL OR USER_ID = " + SqlManager.SqlConvert(userId) + ")";
            }
            
            if (filter.Length > 0) filter += " AND ";
            filter += "PARAMETER_NAME = " + SqlManager.SqlConvert(paramName);
            
            // 查找参数
            DataRow[] drFind = _dsParameters.Tables[0].Select(filter, "DEPT_CODE DESC, USER_ID DESC");
            
            if (drFind.Length > 0)
            {
                return drFind[0]["PARAMETER_VALUE"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
