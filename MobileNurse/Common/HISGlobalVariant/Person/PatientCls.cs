//------------------------------------------------------------------------------------
//  类名            : PatientCls.cs
//  功能概要        : 病人类
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
using System.Collections.Generic;
using System.Text;

namespace HISPlus
{
    /// <summary>
    /// 病人
    /// </summary>
    public class PatientCls: PersonCls
    {
        #region 变量定义
        protected string    _patientId  = string.Empty;                 // 病人ID号
        protected string    _visitId    = string.Empty;                 // 就诊序号

        protected string    _chargeType = string.Empty;                 // 收费类型

        protected DateTime  _inpDate    = DataType.DateTime_Null();     // 入院日期
        protected string    _diagnosis  = string.Empty;                 // 主要诊断

        protected string    _deptCode   = string.Empty;                 // 科室
        protected string    _bedNo      = string.Empty;                 // 床号
        protected string    _bedLabel   = string.Empty;                 // 床标
        protected string _state = string.Empty; //在院状态
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

        #region 属性
        /// <summary>
        /// 在院状态
        /// </summary>
        public string STATE
        {
            get { return _state; }
            set { _state = value; }
        }

        /// <summary>
        /// 病人ID号
        /// </summary>
        public string ID
        {
            get { return _patientId;}
            set { _patientId = value;}
        }

        
        /// <summary>
        /// 本次就诊序号
        /// </summary>
        public string VisitId
        {
            get { return _visitId;}
            set { _visitId = value;}
        }


        /// <summary>
        /// 收费类型
        /// </summary>
        public string ChargeType
        {
            get { return _chargeType;}
            set { _chargeType = value;}
        }


        /// <summary>
        /// 入院日期
        /// </summary>
        public DateTime InpDate
        {
            get { return _inpDate;}
            set { _inpDate = value;}
        }


        /// <summary>
        /// 当前诊断
        /// </summary>
        public string Diagnosis
        {
            get { return _diagnosis;}
            set { _diagnosis = value;}
        }


        /// <summary>
        /// 所属科室代码
        /// </summary>
        public string DeptCode
        {
            get { return _deptCode;}
            set { _deptCode = value;}
        }


        /// <summary>
        /// 当前床号
        /// </summary>
        public string BedNo
        {
            get { return _bedNo;}
            set { _bedNo = value;}
        }


        /// <summary>
        /// 当前床标
        /// </summary>
        public string BedLabel
        {
            get { return _bedLabel;}
            set { _bedLabel = value;}
        }
        #endregion


        #region 方法
        #endregion
    }
}
