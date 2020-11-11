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
        #endregion

        public PatientCls()
        {
        }


        #region ����
        public string ID
        {
            get { return _patientId;}
            set { _patientId = value;}
        }

        
        public string VisitId
        {
            get { return _visitId;}
            set { _visitId = value;}
        }


        public string ChargeType
        {
            get { return _chargeType;}
            set { _chargeType = value;}
        }


        public DateTime InpDate
        {
            get { return _inpDate;}
            set { _inpDate = value;}
        }


        public string Diagnosis
        {
            get { return _diagnosis;}
            set { _diagnosis = value;}
        }


        public string DeptCode
        {
            get { return _deptCode;}
            set { _deptCode = value;}
        }


        public string BedNo
        {
            get { return _bedNo;}
            set { _bedNo = value;}
        }


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
