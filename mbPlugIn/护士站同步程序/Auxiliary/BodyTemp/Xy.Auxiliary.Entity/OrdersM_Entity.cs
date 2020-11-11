// ***********************************************************************
// Assembly         : Xy.Auxiliary.Entity
// Author           : LPD
// Created          : 12-07-2015
//
// Last Modified By : LPD
// Last Modified On : 12-07-2015
// ***********************************************************************
// <copyright file="Orders_M_Entity.cs" company="心医国际">
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
    /// Order_M表实体
    /// </summary>
    [Serializable]
    public class OrdersM_Entity
    {
        public OrdersM_Entity()
		{}
		#region Model
		private string _ward_code;
		private string _patient_id;
		private decimal _visit_id;
		private decimal _order_no;
		private decimal _order_sub_no;
		private decimal? _repeat_indicator;
		private string _order_class;
		private string _order_class_name;
		private string _order_status;
		private string _order_status_name;
		private string _order_text;
		private string _order_code;
		private decimal? _dosage;
		private string _dosage_units;
		private string _administration;
		private decimal? _duration;
		private string _duration_units;
		private DateTime? _start_date_time;
		private DateTime? _stop_date_time;
		private string _frequency;
		private decimal? _freq_counter;
		private decimal? _freq_interval;
		private string _freq_interval_unit;
		private string _freq_detail;
		private decimal? _degree;
		private string _perform_schedule;
		private string _perform_result;
		private string _ordering_dept;
		private string _doctor;
		private string _stop_doctor;
		private string _nurse;
		private string _stop_nurse;
		private DateTime _create_timestamp;
		private DateTime _update_timestamp;
		private string _split_memo;
		private decimal? _split_stauts;
		private string _processing_nurse;
		private DateTime? _conversion_date_time;

		/// <summary>
		/// 病区
		/// </summary>
		public string WARD_CODE
		{
			set{ _ward_code=value;}
			get{return _ward_code;}
		}
		/// <summary>
		/// 病人ID
		/// </summary>
		public string PATIENT_ID
		{
			set{ _patient_id=value;}
			get{return _patient_id;}
		}
		/// <summary>
		/// 住院次数
		/// </summary>
		public decimal VISIT_ID
		{
			set{ _visit_id=value;}
			get{return _visit_id;}
		}
		/// <summary>
		/// 医嘱号
		/// </summary>
		public decimal ORDER_NO
		{
			set{ _order_no=value;}
			get{return _order_no;}
		}
		/// <summary>
		/// 医嘱子序号
		/// </summary>
		public decimal ORDER_SUB_NO
		{
			set{ _order_sub_no=value;}
			get{return _order_sub_no;}
		}
		/// <summary>
        /// 长期医嘱标志 1-长期，0-临时
		/// </summary>
		public decimal? REPEAT_INDICATOR
		{
			set{ _repeat_indicator=value;}
			get{return _repeat_indicator;}
		}
		/// <summary>
        /// 医嘱类别
		/// </summary>
		public string ORDER_CLASS
		{
			set{ _order_class=value;}
			get{return _order_class;}
		}
		/// <summary>
        /// 医嘱类别名称
		/// </summary>
		public string ORDER_CLASS_NAME
		{
			set{ _order_class_name=value;}
			get{return _order_class_name;}
		}
		/// <summary>
        /// 医嘱状态
		/// </summary>
		public string ORDER_STATUS
		{
			set{ _order_status=value;}
			get{return _order_status;}
		}
		/// <summary>
        /// 医嘱状态名称
		/// </summary>
		public string ORDER_STATUS_NAME
		{
			set{ _order_status_name=value;}
			get{return _order_status_name;}
		}
		/// <summary>
        /// 医嘱正文
		/// </summary>
		public string ORDER_TEXT
		{
			set{ _order_text=value;}
			get{return _order_text;}
		}
		/// <summary>
        /// 医嘱代码
		/// </summary>
		public string ORDER_CODE
		{
			set{ _order_code=value;}
			get{return _order_code;}
		}
		/// <summary>
        /// 药品一次使用剂量
		/// </summary>
		public decimal? DOSAGE
		{
			set{ _dosage=value;}
			get{return _dosage;}
		}
		/// <summary>
        /// 剂量单位
		/// </summary>
		public string DOSAGE_UNITS
		{
			set{ _dosage_units=value;}
			get{return _dosage_units;}
		}
		/// <summary>
        /// 给药途径和方法
		/// </summary>
		public string ADMINISTRATION
		{
			set{ _administration=value;}
			get{return _administration;}
		}
		/// <summary>
        /// 持续时间
		/// </summary>
		public decimal? DURATION
		{
			set{ _duration=value;}
			get{return _duration;}
		}
		/// <summary>
        /// 持续时间单位
		/// </summary>
		public string DURATION_UNITS
		{
			set{ _duration_units=value;}
			get{return _duration_units;}
		}
		/// <summary>
        /// 起始日期及时间
		/// </summary>
		public DateTime? START_DATE_TIME
		{
			set{ _start_date_time=value;}
			get{return _start_date_time;}
		}
		/// <summary>
        /// 停止日期及时间
		/// </summary>
		public DateTime? STOP_DATE_TIME
		{
			set{ _stop_date_time=value;}
			get{return _stop_date_time;}
		}
		/// <summary>
        /// 执行频率描述
		/// </summary>
		public string FREQUENCY
		{
			set{ _frequency=value;}
			get{return _frequency;}
		}
		/// <summary>
        /// 频率次数
		/// </summary>
		public decimal? FREQ_COUNTER
		{
			set{ _freq_counter=value;}
			get{return _freq_counter;}
		}
		/// <summary>
        /// 频率间隔
		/// </summary>
		public decimal? FREQ_INTERVAL
		{
			set{ _freq_interval=value;}
			get{return _freq_interval;}
		}
		/// <summary>
        /// 频率间隔单位
		/// </summary>
		public string FREQ_INTERVAL_UNIT
		{
			set{ _freq_interval_unit=value;}
			get{return _freq_interval_unit;}
		}
		/// <summary>
        /// 执行时间详细描述
		/// </summary>
		public string FREQ_DETAIL
		{
			set{ _freq_detail=value;}
			get{return _freq_detail;}
		}
		/// <summary>
        /// 执行次数
		/// </summary>
		public decimal? DEGREE
		{
			set{ _degree=value;}
			get{return _degree;}
		}
		/// <summary>
        /// 护士执行时间
		/// </summary>
		public string PERFORM_SCHEDULE
		{
			set{ _perform_schedule=value;}
			get{return _perform_schedule;}
		}
		/// <summary>
        /// 执行结果
		/// </summary>
		public string PERFORM_RESULT
		{
			set{ _perform_result=value;}
			get{return _perform_result;}
		}
		/// <summary>
        /// 开医嘱科室
		/// </summary>
		public string ORDERING_DEPT
		{
			set{ _ordering_dept=value;}
			get{return _ordering_dept;}
		}
		/// <summary>
		/// 开遗嘱医生
		/// </summary>
		public string DOCTOR
		{
			set{ _doctor=value;}
			get{return _doctor;}
		}
		/// <summary>
		/// 停遗嘱医生
		/// </summary>
		public string STOP_DOCTOR
		{
			set{ _stop_doctor=value;}
			get{return _stop_doctor;}
		}
		/// <summary>
		/// 校对护士
		/// </summary>
		public string NURSE
		{
			set{ _nurse=value;}
			get{return _nurse;}
		}
		/// <summary>
		/// 停遗嘱校对护士
		/// </summary>
		public string STOP_NURSE
		{
			set{ _stop_nurse=value;}
			get{return _stop_nurse;}
		}
		/// <summary>
        /// 创建时间
		/// </summary>
		public DateTime CREATE_TIMESTAMP
		{
			set{ _create_timestamp=value;}
			get{return _create_timestamp;}
		}
		/// <summary>
        /// 修改时间
		/// </summary>
		public DateTime UPDATE_TIMESTAMP
		{
			set{ _update_timestamp=value;}
			get{return _update_timestamp;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SPLIT_MEMO
		{
			set{ _split_memo=value;}
			get{return _split_memo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? SPLIT_STAUTS
		{
			set{ _split_stauts=value;}
			get{return _split_stauts;}
		}
		/// <summary>
		/// 新增加的转抄护士
		/// </summary>
		public string PROCESSING_NURSE
		{
			set{ _processing_nurse=value;}
			get{return _processing_nurse;}
		}
		/// <summary>
		/// 拆分日期
		/// </summary>
		public DateTime? CONVERSION_DATE_TIME
		{
			set{ _conversion_date_time=value;}
			get{return _conversion_date_time;}
		}
		#endregion Model
    }


}
