using System;
using System.Collections.Generic;
using System.Text;

namespace HISPlus
{
    // 病人改变事件类型定义
    public delegate void PatientChangedEventHandler(object sender, PatientEventArgs e);
    // 病人列表刷新时间//2015.11.25 add
    public delegate void PatientListRefreshEventHandler(object sender, PatientEventArgs e);

    // 病人改变事件参数
    public class PatientEventArgs : EventArgs
    {
        protected string _patientId = string.Empty;
        protected string _visitId;
        protected string _deptCode;//2015.11.25 add
        protected string _state;
        public PatientEventArgs(string patientId, string visitId)
        {
            _patientId = patientId;
            _visitId = visitId;
        }
        public PatientEventArgs(string patientId, string visitId, string state)
        {
            _patientId = patientId;
            _visitId = visitId;
            _state = state;
        }
        //2015.11.25 add
        public PatientEventArgs(string deptCode)
        {
            _deptCode = deptCode;
        }

        /// <summary>
        /// 属性[病人ID]
        /// </summary>
        public string PatientId
        {
            get
            {
                return _patientId;
            }
        }


        /// <summary>
        /// 属性[就诊序号]
        /// </summary>
        public string VisitId
        {
            get
            {
                return _visitId;
            }
        }

        /// <summary>
        /// 属性-科室
        /// </summary>
        public string DeptCode
        {
            get
            {
                return _deptCode;
            }
        }
        /// <summary>
        /// 在院状态
        /// </summary>
        public string STATE
        {
            get
            {
                return _state;
            }
        }
    }

}
