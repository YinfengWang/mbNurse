//------------------------------------------------------------------------------------
//  ����            : PatientCls.cs
//  ���ܸ�Ҫ        : ������
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
using System.Collections.Generic;
using System.Text;

namespace HISPlus
{
    /// <summary>
    /// ����
    /// </summary>
    public class PatientCls: PersonCls
    {
        #region ��������
        protected string    _patientId  = string.Empty;                 // ����ID��
        protected string    _visitId    = string.Empty;                 // �������

        protected string    _chargeType = string.Empty;                 // �շ�����

        protected DateTime  _inpDate    = DataType.DateTime_Null();     // ��Ժ����
        protected string    _diagnosis  = string.Empty;                 // ��Ҫ���

        protected string    _deptCode   = string.Empty;                 // ����
        protected string    _bedNo      = string.Empty;                 // ����
        protected string    _bedLabel   = string.Empty;                 // ����
        protected string _state = string.Empty; //��Ժ״̬
        protected string _deptName = string.Empty;
        #endregion

        public PatientCls()
        {
        }

        public string DeptName
        {
            get { return _deptName; }
            set { _deptName = value; }
        }

        #region ����
        /// <summary>
        /// ��Ժ״̬
        /// </summary>
        public string STATE
        {
            get { return _state; }
            set { _state = value; }
        }

        /// <summary>
        /// ����ID��
        /// </summary>
        public string ID
        {
            get { return _patientId;}
            set { _patientId = value;}
        }

        
        /// <summary>
        /// ���ξ������
        /// </summary>
        public string VisitId
        {
            get { return _visitId;}
            set { _visitId = value;}
        }


        /// <summary>
        /// �շ�����
        /// </summary>
        public string ChargeType
        {
            get { return _chargeType;}
            set { _chargeType = value;}
        }


        /// <summary>
        /// ��Ժ����
        /// </summary>
        public DateTime InpDate
        {
            get { return _inpDate;}
            set { _inpDate = value;}
        }


        /// <summary>
        /// ��ǰ���
        /// </summary>
        public string Diagnosis
        {
            get { return _diagnosis;}
            set { _diagnosis = value;}
        }


        /// <summary>
        /// �������Ҵ���
        /// </summary>
        public string DeptCode
        {
            get { return _deptCode;}
            set { _deptCode = value;}
        }


        /// <summary>
        /// ��ǰ����
        /// </summary>
        public string BedNo
        {
            get { return _bedNo;}
            set { _bedNo = value;}
        }


        /// <summary>
        /// ��ǰ����
        /// </summary>
        public string BedLabel
        {
            get { return _bedLabel;}
            set { _bedLabel = value;}
        }
        #endregion


        #region ����
        #endregion
    }
}
