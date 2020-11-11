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
    public class UserCls: PersonCls
    {
        #region ��������
        public DataSet      Rights          = null;                     // �û����е�Ȩ��

        protected string    _userName       = string.Empty;             // ��ϵͳ�е�����
        protected string    _id             = string.Empty;             // �û�ID��
        protected string    _pwd            = string.Empty;             // �û�����

        protected string    _deptCode       = string.Empty;             // �û���ǰ�������Ҵ���
        protected string    _deptName       = string.Empty;             // �û���ǰ������������ (ע: û����_deptCode�����ڲ�����)
        protected string    _title          = string.Empty;             // �û��ڵ�ǰ�������ҵ�ͷ��
        protected string    _job            = string.Empty;             // �û��ڵ�ǰ�����ҵĹ�������

        protected ArrayList _deptCodeList   = new ArrayList();          // �û����ڶ������
        protected ArrayList _titleList      = new ArrayList();          // �û��ڸ������ҵ�ͷ��
        protected ArrayList _jobList        = new ArrayList();          // �û��ڸ������ҵĹ�������
        #endregion

        public UserCls()
        {
        }


        #region ����
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


        #region ����
        /// <summary>
        /// ˢ��
        /// </summary>
        public void Refresh()
        { 
            // ˢ��һTitle��Job
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
        /// ����û���������
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
        #endregion
    }
}
