//------------------------------------------------------------------------------------
//
//  ϵͳ����        : ҽ������վ
//  ��ϵͳ����      : ͨ��ģ��
//  ��������        : 
//  ����            : UniversalVarApp.cs
//  ���ܸ�Ҫ        : Ӧ�ó�������
//  ������          : ����
//  ������          : 2007-01-19
//  �汾            : 1.0.0.0
// 
//------------------< �����ʷ >------------------------------------------------------
//  �������        :
//  �����          :
//  �������        :
//  �汾            :
//------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;

namespace HISPlus
{
	/// <summary>
	/// UniversalVarApp ��ժҪ˵����
	/// </summary>
	public class App
    {
        protected string    _name           = string.Empty;             // Ӧ�ó��������
        protected string    _title          = string.Empty;             // Ӧ�ó���ı���
        protected string    _copyRight      = string.Empty;             // ��Ȩ
        protected string    _version        = string.Empty;             // �汾
        
        protected bool      _oneInstance    = true;                     // �Ƿ�ֻ����һ��ʵ��

        protected DateTime  _dtNow          = DataType.DateTime_Null(); // ��ǰʱ��

        protected string    _right          = string.Empty;             // Ȩ��
        protected bool      _verified       = false;                    // ͨ����֤
        
        protected bool      _maxMdiFrm      = true;                     // �Ƿ����������
        
        protected bool      _questionExit   = true;                     // �˳�ϵͳǰ�Ƿ�ѯ��
        
        protected bool      _userInput      = true;                     // �Ƿ������������Ϊ�û�����
        
        protected bool      _resetLocalTime = false;                    // �Ƿ��÷�����ʱ�����ñ���ʱ��
        
        protected DataSet   _dsParameters   = null;                     // Ӧ�ó������
        
        public App()
		{
        }


        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get { return _name;}
            set { _name = value;}
        }


        /// <summary>
        /// ����
        /// </summary>
        public string Title
        {
            get { return _title;}
            set { _title = value;}
        }


        /// <summary>
        /// ��Ȩ
        /// </summary>
        public string CopyRight
        {
            get { return _copyRight;}
            set { _copyRight = value;}
        }


        /// <summary>
        /// �汾
        /// </summary>
        public string Version
        {
            get { return _version;}
            set { _version = value;}
        }


        /// <summary>
        /// �Ƿ�ֻ����һ��ʵ��
        /// </summary>
        public bool OneInstance
        {
            get { return _oneInstance;}
            set { _oneInstance = value;}
        }


        /// <summary>
        ///  ���ڵ�ʱ��
        /// </summary>
        public DateTime Now
        {
            get { return _dtNow;}
            set { _dtNow = value;}
        }


        /// <summary>
        /// Ȩ�� (��ʱ����)
        /// </summary>
        public string Right
        {
            get { return _right;}
            set { _right = value;}
        }


        /// <summary>
        /// �Ƿ�ͨ����֤
        /// </summary>
        public bool Verified
        {
            get { return _verified;}
            set { _verified = value;}
        }


        /// <summary>
        /// �Ƿ����������
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
        /// �˳�ǰ�Ƿ�ѯ��
        /// </summary>
        public bool QuestionExit
        {
            get { return _questionExit;}
            set { _questionExit = value;}
        }


        /// <summary>
        /// �Ƿ����û�������
        /// </summary>
        public bool UserInput
        {
            get { return _userInput;}
            set { _userInput = value;}
        }


        /// <summary>
        /// �Ƿ��÷�����ʱ���������ñ���ʱ��
        /// </summary>
        public bool ResetLocalTime
        {
            get { return _resetLocalTime; }
            set { _resetLocalTime = value;}
        }
        
        
        /// <summary>
        /// Ӧ�ó������
        /// </summary>
        public DataSet AppParameters
        {
            get {return _dsParameters; }
        }
        #endregion


        #region ����
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
        /// ����ϵͳ����ʱ��
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
        /// ���¼���Ӧ�ó������
        /// </summary>
        public void ReloadParameters()
        {
            string sql = "SELECT * FROM APP_CONFIG WHERE APP_CODE IS NULL OR APP_CODE = " + SqlManager.SqlConvert(_right);
            
            _dsParameters = GVars.OracleMobile.SelectData(sql);
        }
        
        
        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <returns></returns>
        public string GetParameters(string deptCode, string userId, string paramName)
        {
            // �������
            if (paramName.Length == 0)
            {
                return string.Empty;
            }
            
            if (_dsParameters == null || _dsParameters.Tables.Count == 0)
            {
                return string.Empty;
            }
            
            // ���ɹ�������
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
            
            // ���Ҳ���
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
