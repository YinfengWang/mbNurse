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
    public class UserCls: PersonCls
    {
        #region 变量定义
        public DataSet      Rights          = null;                     // 用户具有的权限

        protected string    _userName       = string.Empty;             // 在系统中的名称
        protected string    _id             = string.Empty;             // 用户ID号
        protected string    _pwd            = string.Empty;             // 用户密码

        protected string    _deptCode       = string.Empty;             // 用户当前所处科室代码
        protected string    _deptName       = string.Empty;             // 用户当前所处科室名称 (注: 没有与_deptCode建立内部关联)
        protected string    _title          = string.Empty;             // 用户在当前所处科室的头衔
        protected string    _job            = string.Empty;             // 用户在当前所处室的工作性质

        protected ArrayList _deptCodeList   = new ArrayList();          // 用户属于多个科室
        protected ArrayList _titleList      = new ArrayList();          // 用户在各个科室的头衔
        protected ArrayList _jobList        = new ArrayList();          // 用户在各个科室的工作性质
        #endregion

        public UserCls()
        {
        }


        #region 属性
        public string UserName
        {
            get { return _userName;}
            set { _userName = value;}
        }

        
        public string ID
        {
            get { return _id;}
            set { _id = value;}
        }


        public string DeptCode
        {
            get 
            {
                if (_deptCodeList.Count == 1)
                {
                    _deptCode = _deptCodeList[0].ToString();
                }
                
                return _deptCode;
            }

            set
            {
                _deptCode = value;
                
                Refresh();
            }
        }


        public string DeptName
        {
            get { return _deptName; }
            set { _deptName = value; }
        }


        public string Title
        {
            get { return _title;}
        }


        public string Job
        {
            get { return _job;}
        }


        public string PWD
        {
            get { return _pwd;}
            set { _pwd = value;}
        }
        #endregion


        #region 方法
        /// <summary>
        /// 刷新
        /// </summary>
        public void Refresh()
        { 
            // 刷新一Title与Job
            _title  = string.Empty;
            _job    = string.Empty;

            int chkResult = ChkDept(_deptCode);
            
            if (chkResult != ComConst.VAL.FAILED)
            {
                _title  = _titleList[chkResult].ToString();
                _job    = _jobList[chkResult].ToString();
            }
        }


        /// <summary>
        /// 添加用户所属科室
        /// </summary>
        /// <param name="deptCode"></param>
        public void Add_Dept(string deptCode, string title, string job)
        {
            if (ChkDept(deptCode) != ComConst.VAL.FAILED)
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
        #endregion
    }
}
