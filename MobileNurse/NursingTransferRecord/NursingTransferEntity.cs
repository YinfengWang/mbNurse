using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HISPlus
{
    public class NursingTransferEntity
    {
        /// <summary>
        /// GUID
        /// </summary>
        public string RGUID { get; set; }

        /// <summary>
        /// 病人ID
        /// </summary>
        public string PATIENT_ID { get;set;}
        
        /// <summary>
        /// 住院次数
        /// </summary>
        public string VISIT_ID { get; set; }

        /// <summary>
        /// 病区代码
        /// </summary>
        public string WARD_CODE { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        public string BED_NO { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 诊断
        /// </summary>
        public string DIAGNOSIS_NAME { get; set; }

        /// <summary>
        /// 动态情况
        /// </summary>
        public string DYNAMIC_SITUATION { get; set; }

        /// <summary>
        /// 患者处置、病情观察及交班内容
        /// </summary>
        public string TRANSFER_CONTENT { get; set; }

        /// <summary>
        /// 交班者
        /// </summary>
        public string TRANSFERER { get; set; }

        /// <summary>
        /// 接班者
        /// </summary>
        public string RECEIVEER { get; set; }

        /// <summary>
        /// 交班时间内
        /// </summary>
        public string EXECUTE_TIME { get; set; }

        /// <summary>
        /// 是否废除 T:作废   F:正常
        /// </summary>
        public string IS_ABOLISH { get; set; }

        /// <summary>
        /// 护理事件
        /// </summary>
        public string NURSE_EVENT { get; set; }

        /// <summary>
        /// 护理等级
        /// </summary>
        public string NURSE_CLASS { get; set; }
    }
}
