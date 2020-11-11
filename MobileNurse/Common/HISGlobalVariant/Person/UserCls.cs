//------------------------------------------------------------------------------------
//  ����            : UserCls.cs
//  ���ܸ�Ҫ        : ϵͳ�û���
//  ������          : ����
//  ������          : 2008-04-10
//  �汾            : 1.0.0.0
// 
//------------------< �����ʷ >------------------------------------------------------
//  �������        : 
//  �����          : 
//  �������        : 
//  �汾            : 
//------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace HISPlus
{
    /// <summary>
    /// �û�
    /// </summary>
    public class UserCls : PersonCls
    {
        #region ��������
        /// <summary>
        /// �û����е�Ȩ�� (��ĳһӦ�ó���)
        /// </summary>
        public DataSet Rights = null;                     // �û����е�Ȩ��

        protected string _userName = string.Empty;             // ��ϵͳ�е�����
        protected string _id = string.Empty;             // �û�ID��
        protected string _pwd = string.Empty;             // �û�����

        protected string _deptCode = string.Empty;             // �û���ǰ�������Ҵ���
        protected string _deptName = string.Empty;             // �û���ǰ������������ (ע: û����_deptCode�����ڲ�����)
        protected string _title = string.Empty;             // �û��ڵ�ǰ�������ҵ�ͷ��
        protected string _job = string.Empty;             // �û��ڵ�ǰ�����ҵĹ�������

        protected ArrayList _deptCodeList = new ArrayList();          // �û����ڶ������
        protected ArrayList _titleList = new ArrayList();          // �û��ڸ������ҵ�ͷ��
        protected ArrayList _jobList = new ArrayList();          // �û��ڸ������ҵĹ�������
        protected DataSet _dsDepts = new DataSet();



        #endregion

        public UserCls()
        {
        }


        #region ����
        /// <summary>
        /// �û���
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }


        /// <summary>
        /// �û�ID��
        /// </summary>
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }


        /// <summary>
        /// ���Ҵ���
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
        /// ��������
        /// </summary>
        public string DeptName
        {
            get { return _deptName; }
            set { _deptName = value; }
        }


        /// <summary>
        /// ͷ��
        /// </summary>
        public string Title
        {
            get { return _title; }
        }


        /// <summary>
        /// ��������
        /// </summary>
        public string Job
        {
            get { return _job; }
        }


        /// <summary>
        /// ����
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


        #region ����
        /// <summary>
        /// ˢ��
        /// </summary>
        public void Refresh()
        {
            // ˢ��һTitle��Job
            _title = string.Empty;
            _job = string.Empty;

            int userDeptCount = 0;

            if (DsDepts != null && DsDepts.Tables.Count > 0 && DsDepts.Tables[0].Rows.Count > 0)
            {
                //��֤��ǰ����LAUNCHER.INI���ң��Ƿ�����ڿ�������
                for (int i = 0; i < DsDepts.Tables[0].Rows.Count; i++)
                {
                    if (!DsDepts.Tables[0].Rows[i]["DEPT_CODE"].ToString().Equals(_deptCode))
                    {
                        userDeptCount++;
                    }
                }

                if (userDeptCount == DsDepts.Tables[0].Rows.Count)
                {
                    //Ĭ��ѡ�������ĵ�һ��
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
        /// ����û���������
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
        /// �ж��û��Ƿ�����ĳ������
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
        /// �������
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool ChkPwd(string pwd)
        {
            return _pwd.Equals(pwd.Trim().ToUpper());
        }


        /// <summary>
        /// ����Ƿ����Ȩ��
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
        /// ��ȡ�û���ĳһ�����Ͼ���Ȩ��
        /// </summary>
        /// <param name="formId">����ID</param>
        /// <returns>
        /// ����Ȩ�޵��ַ���, ��1,2,3
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
