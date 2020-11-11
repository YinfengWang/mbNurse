// ***********************************************************************
// Assembly         : Xy.Auxiliary.Entity
// Author           : LPD
// Created          : 12-07-2015
//
// Last Modified By : LPD
// Last Modified On : 12-07-2015
// ***********************************************************************
// <copyright file="Orders_Execute_Entity.cs" company="心医国际">
//     Copyright (c) 心医国际. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// The Entity namespace.
/// </summary>
namespace Xy.Auxiliary.Entity
{
    /// <summary>
    /// Orders_Execute表实体
    /// </summary>
    [Serializable]
    public class OrdersExecute_Entity
    {
        public OrdersExecute_Entity()
        { }
        #region Model
        private DateTime _conversion_date_time;
        private string _patient_id;
        private decimal _visit_id;
        private decimal _order_no;
        private decimal _order_sub_no;
        private string _orders_perform_schedule;
        private decimal? _repeat_indicator;
        private string _order_class;
        private string _order_text;
        private string _order_code;
        private decimal? _dosage;
        private string _dosage_units;
        private string _administration;
        private decimal? _duration;
        private string _duration_units;
        private string _frequency;
        private decimal? _freq_counter;
        private decimal? _freq_interval;
        private string _freq_interval_unit;
        private string _freq_detail;
        private string _perform_schedule;
        private string _perform_result;
        private string _conversion_nurse;
        private DateTime? _execute_date_time;
        private string _execute_nurse;
        private decimal? _drug_billing_attr;
        private decimal? _billing_attr;
        private decimal? _costs;
        private decimal? _is_execute;
        private decimal? _charge;
        private DateTime? _drug_date_time;
        private DateTime? _schedule_perform_time;
        private decimal? _print_flag;
        private string _ward_code;
        private string _nurse;
        private string _processing_nurse;
        private string _memo;
        private DateTime _create_timestamp;
        private DateTime _update_timestamp;
        private string _reamrk;

        /// <summary>
        /// 备注
        /// </summary>
        public string Reamrk
        {
            get { return _reamrk; }
            set { _reamrk = value; }
        }
        /// <summary>
        /// 拆分时间
        /// </summary>
        public DateTime CONVERSION_DATE_TIME
        {
            set { _conversion_date_time = value; }
            get { return _conversion_date_time; }
        }
        /// <summary>
        /// 病人ID
        /// </summary>
        public string PATIENT_ID
        {
            set { _patient_id = value; }
            get { return _patient_id; }
        }
        /// <summary>
        /// 就诊序号
        /// </summary>
        public decimal VISIT_ID
        {
            set { _visit_id = value; }
            get { return _visit_id; }
        }
        /// <summary>
        /// 医嘱序号
        /// </summary>
        public decimal ORDER_NO
        {
            set { _order_no = value; }
            get { return _order_no; }
        }
        /// <summary>
        /// 子医嘱序号
        /// </summary>
        public decimal ORDER_SUB_NO
        {
            set { _order_sub_no = value; }
            get { return _order_sub_no; }
        }
        /// <summary>
        /// 计划执行时间
        /// </summary>
        public string ORDERS_PERFORM_SCHEDULE
        {
            set { _orders_perform_schedule = value; }
            get { return _orders_perform_schedule; }
        }
        /// <summary>
        /// 长临标识
        /// </summary>
        public decimal? REPEAT_INDICATOR
        {
            set { _repeat_indicator = value; }
            get { return _repeat_indicator; }
        }
        /// <summary>
        /// 医嘱类别
        /// </summary>
        public string ORDER_CLASS
        {
            set { _order_class = value; }
            get { return _order_class; }
        }
        /// <summary>
        /// 医嘱内容
        /// </summary>
        public string ORDER_TEXT
        {
            set { _order_text = value; }
            get { return _order_text; }
        }
        /// <summary>
        /// 医嘱代码
        /// </summary>
        public string ORDER_CODE
        {
            set { _order_code = value; }
            get { return _order_code; }
        }
        /// <summary>
        /// 次剂量
        /// </summary>
        public decimal? DOSAGE
        {
            set { _dosage = value; }
            get { return _dosage; }
        }
        /// <summary>
        /// 剂量单位
        /// </summary>
        public string DOSAGE_UNITS
        {
            set { _dosage_units = value; }
            get { return _dosage_units; }
        }
        /// <summary>
        /// 给药途径和方法
        /// </summary>
        public string ADMINISTRATION
        {
            set { _administration = value; }
            get { return _administration; }
        }
        /// <summary>
        /// 持续时间
        /// </summary>
        public decimal? DURATION
        {
            set { _duration = value; }
            get { return _duration; }
        }
        /// <summary>
        /// 持续时间单位
        /// </summary>
        public string DURATION_UNITS
        {
            set { _duration_units = value; }
            get { return _duration_units; }
        }
        /// <summary>
        /// 执行频率描述
        /// </summary>
        public string FREQUENCY
        {
            set { _frequency = value; }
            get { return _frequency; }
        }
        /// <summary>
        /// 频率次数
        /// </summary>
        public decimal? FREQ_COUNTER
        {
            set { _freq_counter = value; }
            get { return _freq_counter; }
        }
        /// <summary>
        /// 频率间隔
        /// </summary>
        public decimal? FREQ_INTERVAL
        {
            set { _freq_interval = value; }
            get { return _freq_interval; }
        }
        /// <summary>
        /// 频率间隔单位
        /// </summary>
        public string FREQ_INTERVAL_UNIT
        {
            set { _freq_interval_unit = value; }
            get { return _freq_interval_unit; }
        }
        /// <summary>
        /// 执行时间详细描述
        /// </summary>
        public string FREQ_DETAIL
        {
            set { _freq_detail = value; }
            get { return _freq_detail; }
        }
        /// <summary>
        /// 护士执行时间
        /// </summary>
        public string PERFORM_SCHEDULE
        {
            set { _perform_schedule = value; }
            get { return _perform_schedule; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PERFORM_RESULT
        {
            set { _perform_result = value; }
            get { return _perform_result; }
        }
        /// <summary>
        /// 执行单生成护士
        /// </summary>
        public string CONVERSION_NURSE
        {
            set { _conversion_nurse = value; }
            get { return _conversion_nurse; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EXECUTE_DATE_TIME
        {
            set { _execute_date_time = value; }
            get { return _execute_date_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EXECUTE_NURSE
        {
            set { _execute_nurse = value; }
            get { return _execute_nurse; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? DRUG_BILLING_ATTR
        {
            set { _drug_billing_attr = value; }
            get { return _drug_billing_attr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? BILLING_ATTR
        {
            set { _billing_attr = value; }
            get { return _billing_attr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? COSTS
        {
            set { _costs = value; }
            get { return _costs; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? IS_EXECUTE
        {
            set { _is_execute = value; }
            get { return _is_execute; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? CHARGE
        {
            set { _charge = value; }
            get { return _charge; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DRUG_DATE_TIME
        {
            set { _drug_date_time = value; }
            get { return _drug_date_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SCHEDULE_PERFORM_TIME
        {
            set { _schedule_perform_time = value; }
            get { return _schedule_perform_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? PRINT_FLAG
        {
            set { _print_flag = value; }
            get { return _print_flag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WARD_CODE
        {
            set { _ward_code = value; }
            get { return _ward_code; }
        }
        /// <summary>
        /// 转抄护士
        /// </summary>
        public string NURSE
        {
            set { _nurse = value; }
            get { return _nurse; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PROCESSING_NURSE
        {
            set { _processing_nurse = value; }
            get { return _processing_nurse; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MEMO
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CREATE_TIMESTAMP
        {
            set { _create_timestamp = value; }
            get { return _create_timestamp; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UPDATE_TIMESTAMP
        {
            set { _update_timestamp = value; }
            get { return _update_timestamp; }
        }
        #endregion Model
    }
}
