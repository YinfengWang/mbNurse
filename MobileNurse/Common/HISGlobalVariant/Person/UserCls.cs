//------------------------------------------------------------------------------------
//  类名            : UserCls.cs
//  功能概要        : 系统用户类
//  作成者          : 付军
//  作成日          : 2008-04-10
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        : 
//  变更者          : 
//  变更内容        : 
//  版本            : 
//------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace HISPlus
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserCls : PersonCls
    {
        #region 变量定义
        /// <summary>
        /// 用户具有的权限 (在某一应用程中)
        /// </summary>
        public DataSet Rights = null;                     // 用户具有的权限

        protected string _userName = string.Empty;             // 在系统中的名称
        protected string _id = string.Empty;             // 用户ID号
        protected string _pwd = string.Empty;             // 用户密码

        protected string _deptCode = string.Empty;             // 用户当前所处科室代码
        protected string _deptName = string.Empty;             // 用户当前所处科室名称 (注: 没有与_deptCode建立内部关联)
        protected string _title = string.Empty;             // 用户在当前所处科室的头衔
        protected string _job = string.Empty;             // 用户在当前所处室的工作性质

        protected ArrayList _deptCodeList = new ArrayList();          // 用户属于多个科室
        protected ArrayList _titleList = new ArrayList();          // 用户在各个科室的头衔
        protected ArrayList _jobList = new ArrayList();          // 用户在各个科室的工作性质
        protected DataSet _dsDepts = new DataSet();



        #endregion

        public UserCls()
        {
        }


        #region 属性
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }


        /// <summary>
        /// 用户ID号
        /// </summary>
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }


        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode
        {
            get
            {
                //if (_deptCodeList.Count == 1)
                //{
                //    _deptCode = _deptCodeList[0].ToString();
                //}

                return _deptCode;
            }

            set
            {
                _deptCode = value;

                Refresh();
            }
        }


        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName
        {
            get { return _deptName; }
            set { _deptName = value; }
        }


        /// <summary>
        /// 头衔
        /// </summary>
        public string Title
        {
            get { return _title; }
        }


        /// <summary>
        /// 工作性质
        /// </summary>
        public string Job
        {
            get { return _job; }
        }


        /// <summary>
        /// 密码
        /// </summary>
        public string PWD
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

        public DataSet DsDepts
        {
            get { return _dsDepts; }
            set { _dsDepts = value; }
        }

        #endregion


        #region 方法
        /// <summary>
        /// 刷新
        /// </summary>
        public void Refresh()
        {
            // 刷新一Title与Job
            _title = string.Empty;
            _job = string.Empty;

            int userDeptCount = 0;

            if (DsDepts != null && DsDepts.Tables.Count > 0 && DsDepts.Tables[0].Rows.Count > 0)
            {
                //验证当前程序LAUNCHER.INI科室，是否存在于科室组中
                for (int i = 0; i < DsDepts.Tables[0].Rows.Count; i++)
                {
                    if (!DsDepts.Tables[0].Rows[i]["DEPT_CODE"].ToString().Equals(_deptCode))
                    {
                        userDeptCount++;
                    }
                }

                if (userDeptCount == DsDepts.Tables[0].Rows.Count)
                {
                    //默认选择科室组的第一个
                    _deptCode = DsDepts.Tables[0].Rows[0]["DEPT_CODE"].ToString();
                }
            }

            int chkResult = ChkDept(_deptCode);

            if (chkResult != ComConst.VAL.FAILED)
            {
                _title = _titleList[chkResult].ToString();
                _job = _jobList[chkResult].ToString();
            }
        }


        /// <summary>
        /// 添加用户所属科室
        /// </summary>
        /// <param name="deptCode"></param>
        public void Add_Dept(string deptCode, string title, string job)
        {
            if (ChkDept(deptCode) == ComConst.VAL.FAILED)
            {
                _deptCodeList.Add(deptCode);
                _titleList.Add(title);
                _jobList.Add(job);
            }
        }


        /// <summary>
        /// 判断用户是否属于某个科室
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public int ChkDept(string deptCode)
        {
            for (int i = 0; i < _deptCodeList.Count; i++)
            {
                if (_deptCodeList[i].ToString().Equals(deptCode) == true)
                {
                    return i;
                }
            }

            return ComConst.VAL.FAILED;
        }


        /// <summary>
        /// 检查密码
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool ChkPwd(string pwd)
        {
            return _pwd.Equals(pwd.Trim().ToUpper());
        }


        /// <summary>
        /// 检查是否具有权限
        /// </summary>
        /// <returns></returns>
        public bool HasRights()
        {
            if (Rights == null || Rights.Tables.Count == 0 || Rights.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 获取用户在某一窗体上具有权限
        /// </summary>
        /// <param name="formId">窗体ID</param>
        /// <returns>
        /// 代表权限的字符串, 如1,2,3
        /// </returns>
        public string GetUserFrmRight(string formId)
        {
            if (GVars.User._deptCodeList.Contains(GVars.User._deptCode) == false)
            {
                return string.Empty;
            }

            string filter = "MODULE_CODE = " + SqlManager.SqlConvert(formId);

            DataRow[] drFind = Rights.Tables[0].Select(filter);

            string rights = string.Empty;

            for (int i = 0; i < drFind.Length; i++)
            {
                DataRow dr = drFind[i];

                string roleRight = dr["MODULE_RIGHT"].ToString().Trim();
                if (roleRight.Length == 0)
                {
                    continue;
                }

                if (rights.Length > 0) rights += ComConst.STR.COMMA;
                rights += roleRight;
            }

            return rights;
        }
        #endregion
    }
}
