using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using BaseEntity;

namespace TemperatureModel
{
    /// <summary>
    /// ???????
    /// </summary>
    [AttributeTable(TableName = "PAT_VISIT")]
    public class PatVisitModel : Entity
    {
        private static PatVisitModel  _PatVisitModel;

        public static PatVisitModel GetInstance()
        {
            if (_PatVisitModel == null)
                _PatVisitModel = Activator.CreateInstance<PatVisitModel>();
            return _PatVisitModel;
        }
        /// <summary>
        /// 表名: ???????
        /// </summary>
        public static readonly string TableName = "PAT_VISIT";

        /// <summary>
        /// 所有列
        /// </summary>
        public static readonly string All = "*";

  
        /// <summary>
        /// 列名: 病人标识
        /// </summary>
        public static readonly string ColName_PatientId = "PAT_VISIT.PATIENT_ID";

        /// <summary>
        /// 病人标识
        /// </summary>
        [AttributeColumn(ColumnName = "PATIENT_ID",ColType = "VARCHAR2",CanNullOrEmpty = false,MaxLength= 10,IsPrimaryKey = true)]
        public string PatientId
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 病人本次住院标识
        /// </summary>
        public static readonly string ColName_VisitId = "PAT_VISIT.VISIT_ID";

        /// <summary>
        /// 病人本次住院标识
        /// </summary>
        [AttributeColumn(ColumnName = "VISIT_ID",ColType = "NUMBER_2_0",CanNullOrEmpty = false,IsPrimaryKey = true)]
        public Decimal VisitId
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 入院科室
        /// </summary>
        public static readonly string ColName_DeptAdmissionTo = "PAT_VISIT.DEPT_ADMISSION_TO";

        /// <summary>
        /// 入院科室
        /// </summary>
        [AttributeColumn(ColumnName = "DEPT_ADMISSION_TO",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 8)]
        public string DeptAdmissionTo
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 入院日期及时间
        /// </summary>
        public static readonly string ColName_AdmissionDateTime = "PAT_VISIT.ADMISSION_DATE_TIME";

        /// <summary>
        /// 入院日期及时间
        /// </summary>
        [AttributeColumn(ColumnName = "ADMISSION_DATE_TIME",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime AdmissionDateTime
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 出院科室
        /// </summary>
        public static readonly string ColName_DeptDischargeFrom = "PAT_VISIT.DEPT_DISCHARGE_FROM";

        /// <summary>
        /// 出院科室
        /// </summary>
        [AttributeColumn(ColumnName = "DEPT_DISCHARGE_FROM",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 8)]
        public string DeptDischargeFrom
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 出院日期及时间
        /// </summary>
        public static readonly string ColName_DischargeDateTime = "PAT_VISIT.DISCHARGE_DATE_TIME";

        /// <summary>
        /// 出院日期及时间
        /// </summary>
        [AttributeColumn(ColumnName = "DISCHARGE_DATE_TIME",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime DischargeDateTime
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 职业
        /// </summary>
        public static readonly string ColName_Occupation = "PAT_VISIT.OCCUPATION";

        /// <summary>
        /// 职业
        /// </summary>
        [AttributeColumn(ColumnName = "OCCUPATION",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 2)]
        public string Occupation
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 婚姻状况
        /// </summary>
        public static readonly string ColName_MaritalStatus = "PAT_VISIT.MARITAL_STATUS";

        /// <summary>
        /// 婚姻状况
        /// </summary>
        [AttributeColumn(ColumnName = "MARITAL_STATUS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 4)]
        public string MaritalStatus
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 身份
        /// </summary>
        public static readonly string ColName_Identity = "PAT_VISIT.IDENTITY";

        /// <summary>
        /// 身份
        /// </summary>
        [AttributeColumn(ColumnName = "IDENTITY",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string Identity
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 军种
        /// </summary>
        public static readonly string ColName_ArmedServices = "PAT_VISIT.ARMED_SERVICES";

        /// <summary>
        /// 军种
        /// </summary>
        [AttributeColumn(ColumnName = "ARMED_SERVICES",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 4)]
        public string ArmedServices
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 勤务
        /// </summary>
        public static readonly string ColName_Duty = "PAT_VISIT.DUTY";

        /// <summary>
        /// 勤务
        /// </summary>
        [AttributeColumn(ColumnName = "DUTY",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 19)]
        public string Duty
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 隶属大单位
        /// </summary>
        public static readonly string ColName_TopUnit = "PAT_VISIT.TOP_UNIT";

        /// <summary>
        /// 隶属大单位
        /// </summary>
        [AttributeColumn(ColumnName = "TOP_UNIT",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string TopUnit
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 费别
        /// </summary>
        public static readonly string ColName_ServiceSystemIndicator = "PAT_VISIT.SERVICE_SYSTEM_INDICATOR";

        /// <summary>
        /// 费别
        /// </summary>
        [AttributeColumn(ColumnName = "SERVICE_SYSTEM_INDICATOR",ColType = "NUMBER_1_0",CanNullOrEmpty = true)]
        public Decimal ServiceSystemIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 合同单位
        /// </summary>
        public static readonly string ColName_UnitInContract = "PAT_VISIT.UNIT_IN_CONTRACT";

        /// <summary>
        /// 合同单位
        /// </summary>
        [AttributeColumn(ColumnName = "UNIT_IN_CONTRACT",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 11)]
        public string UnitInContract
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 医保类别
        /// </summary>
        public static readonly string ColName_ChargeType = "PAT_VISIT.CHARGE_TYPE";

        /// <summary>
        /// 医保类别
        /// </summary>
        [AttributeColumn(ColumnName = "CHARGE_TYPE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 20)]
        public string ChargeType
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 在职标志
        /// </summary>
        public static readonly string ColName_WorkingStatus = "PAT_VISIT.WORKING_STATUS";

        /// <summary>
        /// 在职标志
        /// </summary>
        [AttributeColumn(ColumnName = "WORKING_STATUS",ColType = "NUMBER_1_0",CanNullOrEmpty = true)]
        public Decimal WorkingStatus
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 
        /// </summary>
        public static readonly string ColName_InsuranceType = "PAT_VISIT.INSURANCE_TYPE";

        /// <summary>
        /// 
        /// </summary>
        [AttributeColumn(ColumnName = "INSURANCE_TYPE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 16)]
        public string InsuranceType
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 医疗保险号
        /// </summary>
        public static readonly string ColName_InsuranceNo = "PAT_VISIT.INSURANCE_NO";

        /// <summary>
        /// 医疗保险号
        /// </summary>
        [AttributeColumn(ColumnName = "INSURANCE_NO",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 18)]
        public string InsuranceNo
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 工作单位
        /// </summary>
        public static readonly string ColName_ServiceAgency = "PAT_VISIT.SERVICE_AGENCY";

        /// <summary>
        /// 工作单位
        /// </summary>
        [AttributeColumn(ColumnName = "SERVICE_AGENCY",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 100)]
        public string ServiceAgency
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 通信地址(户口地址)
        /// </summary>
        public static readonly string ColName_MailingAddress = "PAT_VISIT.MAILING_ADDRESS";

        /// <summary>
        /// 通信地址(户口地址)
        /// </summary>
        [AttributeColumn(ColumnName = "MAILING_ADDRESS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 200)]
        public string MailingAddress
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 邮政编码
        /// </summary>
        public static readonly string ColName_ZipCode = "PAT_VISIT.ZIP_CODE";

        /// <summary>
        /// 邮政编码
        /// </summary>
        [AttributeColumn(ColumnName = "ZIP_CODE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 6)]
        public string ZipCode
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 联系人姓名
        /// </summary>
        public static readonly string ColName_NextOfKin = "PAT_VISIT.NEXT_OF_KIN";

        /// <summary>
        /// 联系人姓名
        /// </summary>
        [AttributeColumn(ColumnName = "NEXT_OF_KIN",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 20)]
        public string NextOfKin
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 与联系人关系
        /// </summary>
        public static readonly string ColName_Relationship = "PAT_VISIT.RELATIONSHIP";

        /// <summary>
        /// 与联系人关系
        /// </summary>
        [AttributeColumn(ColumnName = "RELATIONSHIP",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 2)]
        public string Relationship
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 联系人地址
        /// </summary>
        public static readonly string ColName_NextOfKinAddr = "PAT_VISIT.NEXT_OF_KIN_ADDR";

        /// <summary>
        /// 联系人地址
        /// </summary>
        [AttributeColumn(ColumnName = "NEXT_OF_KIN_ADDR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 40)]
        public string NextOfKinAddr
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 联系人邮政编码
        /// </summary>
        public static readonly string ColName_NextOfKinZipcode = "PAT_VISIT.NEXT_OF_KIN_ZIPCODE";

        /// <summary>
        /// 联系人邮政编码
        /// </summary>
        [AttributeColumn(ColumnName = "NEXT_OF_KIN_ZIPCODE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 6)]
        public string NextOfKinZipcode
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 联系人电话
        /// </summary>
        public static readonly string ColName_NextOfKinPhone = "PAT_VISIT.NEXT_OF_KIN_PHONE";

        /// <summary>
        /// 联系人电话
        /// </summary>
        [AttributeColumn(ColumnName = "NEXT_OF_KIN_PHONE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 16)]
        public string NextOfKinPhone
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 入院方式
        /// </summary>
        public static readonly string ColName_PatientClass = "PAT_VISIT.PATIENT_CLASS";

        /// <summary>
        /// 入院方式
        /// </summary>
        [AttributeColumn(ColumnName = "PATIENT_CLASS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 10)]
        public string PatientClass
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 住院目的
        /// </summary>
        public static readonly string ColName_AdmissionCause = "PAT_VISIT.ADMISSION_CAUSE";

        /// <summary>
        /// 住院目的
        /// </summary>
        [AttributeColumn(ColumnName = "ADMISSION_CAUSE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 8)]
        public string AdmissionCause
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 住院目的
        /// </summary>
        public static readonly string ColName_ConsultingDate = "PAT_VISIT.CONSULTING_DATE";

        /// <summary>
        /// 住院目的
        /// </summary>
        [AttributeColumn(ColumnName = "CONSULTING_DATE",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime ConsultingDate
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 入院病情
        /// </summary>
        public static readonly string ColName_PatAdmCondition = "PAT_VISIT.PAT_ADM_CONDITION";

        /// <summary>
        /// 入院病情
        /// </summary>
        [AttributeColumn(ColumnName = "PAT_ADM_CONDITION",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string PatAdmCondition
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 门诊医师
        /// </summary>
        public static readonly string ColName_ConsultingDoctor = "PAT_VISIT.CONSULTING_DOCTOR";

        /// <summary>
        /// 门诊医师
        /// </summary>
        [AttributeColumn(ColumnName = "CONSULTING_DOCTOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string ConsultingDoctor
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 办理住院者
        /// </summary>
        public static readonly string ColName_AdmittedBy = "PAT_VISIT.ADMITTED_BY";

        /// <summary>
        /// 办理住院者
        /// </summary>
        [AttributeColumn(ColumnName = "ADMITTED_BY",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string AdmittedBy
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 抢救次数
        /// </summary>
        public static readonly string ColName_EmerTreatTimes = "PAT_VISIT.EMER_TREAT_TIMES";

        /// <summary>
        /// 抢救次数
        /// </summary>
        [AttributeColumn(ColumnName = "EMER_TREAT_TIMES",ColType = "NUMBER_2_0",CanNullOrEmpty = true)]
        public Decimal EmerTreatTimes
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 抢救成功次数
        /// </summary>
        public static readonly string ColName_EscEmerTimes = "PAT_VISIT.ESC_EMER_TIMES";

        /// <summary>
        /// 抢救成功次数
        /// </summary>
        [AttributeColumn(ColumnName = "ESC_EMER_TIMES",ColType = "NUMBER_2_0",CanNullOrEmpty = true)]
        public Decimal EscEmerTimes
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 病重天数
        /// </summary>
        public static readonly string ColName_SeriousCondDays = "PAT_VISIT.SERIOUS_COND_DAYS";

        /// <summary>
        /// 病重天数
        /// </summary>
        [AttributeColumn(ColumnName = "SERIOUS_COND_DAYS",ColType = "NUMBER_4_0",CanNullOrEmpty = true)]
        public Decimal SeriousCondDays
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 病危天数
        /// </summary>
        public static readonly string ColName_CriticalCondDays = "PAT_VISIT.CRITICAL_COND_DAYS";

        /// <summary>
        /// 病危天数
        /// </summary>
        [AttributeColumn(ColumnName = "CRITICAL_COND_DAYS",ColType = "NUMBER_4_0",CanNullOrEmpty = true)]
        public Decimal CriticalCondDays
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: ICU天数
        /// </summary>
        public static readonly string ColName_IcuDays = "PAT_VISIT.ICU_DAYS";

        /// <summary>
        /// ICU天数
        /// </summary>
        [AttributeColumn(ColumnName = "ICU_DAYS",ColType = "NUMBER_4_0",CanNullOrEmpty = true)]
        public Decimal IcuDays
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: CCU天数
        /// </summary>
        public static readonly string ColName_CcuDays = "PAT_VISIT.CCU_DAYS";

        /// <summary>
        /// CCU天数
        /// </summary>
        [AttributeColumn(ColumnName = "CCU_DAYS",ColType = "NUMBER_4_0",CanNullOrEmpty = true)]
        public Decimal CcuDays
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 特别护理天数
        /// </summary>
        public static readonly string ColName_SpecLevelNursDays = "PAT_VISIT.SPEC_LEVEL_NURS_DAYS";

        /// <summary>
        /// 特别护理天数
        /// </summary>
        [AttributeColumn(ColumnName = "SPEC_LEVEL_NURS_DAYS",ColType = "NUMBER_4_0",CanNullOrEmpty = true)]
        public Decimal SpecLevelNursDays
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 一级护理天数
        /// </summary>
        public static readonly string ColName_FirstLevelNursDays = "PAT_VISIT.FIRST_LEVEL_NURS_DAYS";

        /// <summary>
        /// 一级护理天数
        /// </summary>
        [AttributeColumn(ColumnName = "FIRST_LEVEL_NURS_DAYS",ColType = "NUMBER_4_0",CanNullOrEmpty = true)]
        public Decimal FirstLevelNursDays
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 二级护理天数
        /// </summary>
        public static readonly string ColName_SecondLevelNursDays = "PAT_VISIT.SECOND_LEVEL_NURS_DAYS";

        /// <summary>
        /// 二级护理天数
        /// </summary>
        [AttributeColumn(ColumnName = "SECOND_LEVEL_NURS_DAYS",ColType = "NUMBER_4_0",CanNullOrEmpty = true)]
        public Decimal SecondLevelNursDays
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 尸检标识
        /// </summary>
        public static readonly string ColName_AutopsyIndicator = "PAT_VISIT.AUTOPSY_INDICATOR";

        /// <summary>
        /// 尸检标识
        /// </summary>
        [AttributeColumn(ColumnName = "AUTOPSY_INDICATOR",ColType = "NUMBER_1_0",CanNullOrEmpty = true)]
        public Decimal AutopsyIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 血型
        /// </summary>
        public static readonly string ColName_BloodType = "PAT_VISIT.BLOOD_TYPE";

        /// <summary>
        /// 血型
        /// </summary>
        [AttributeColumn(ColumnName = "BLOOD_TYPE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 4)]
        public string BloodType
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: Rh血型
        /// </summary>
        public static readonly string ColName_BloodTypeRh = "PAT_VISIT.BLOOD_TYPE_RH";

        /// <summary>
        /// Rh血型
        /// </summary>
        [AttributeColumn(ColumnName = "BLOOD_TYPE_RH",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 4)]
        public string BloodTypeRh
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 输液反应次数
        /// </summary>
        public static readonly string ColName_InfusionReactTimes = "PAT_VISIT.INFUSION_REACT_TIMES";

        /// <summary>
        /// 输液反应次数
        /// </summary>
        [AttributeColumn(ColumnName = "INFUSION_REACT_TIMES",ColType = "NUMBER_2_0",CanNullOrEmpty = true)]
        public Decimal InfusionReactTimes
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 输血次数
        /// </summary>
        public static readonly string ColName_BloodTranTimes = "PAT_VISIT.BLOOD_TRAN_TIMES";

        /// <summary>
        /// 输血次数
        /// </summary>
        [AttributeColumn(ColumnName = "BLOOD_TRAN_TIMES",ColType = "NUMBER_2_0",CanNullOrEmpty = true)]
        public Decimal BloodTranTimes
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 输血总量
        /// </summary>
        public static readonly string ColName_BloodTranVol = "PAT_VISIT.BLOOD_TRAN_VOL";

        /// <summary>
        /// 输血总量
        /// </summary>
        [AttributeColumn(ColumnName = "BLOOD_TRAN_VOL",ColType = "NUMBER_5_0",CanNullOrEmpty = true)]
        public Decimal BloodTranVol
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 输血反应次数
        /// </summary>
        public static readonly string ColName_BloodTranReactTimes = "PAT_VISIT.BLOOD_TRAN_REACT_TIMES";

        /// <summary>
        /// 输血反应次数
        /// </summary>
        [AttributeColumn(ColumnName = "BLOOD_TRAN_REACT_TIMES",ColType = "NUMBER_2_0",CanNullOrEmpty = true)]
        public Decimal BloodTranReactTimes
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 发生褥疮次数
        /// </summary>
        public static readonly string ColName_DecubitalUlcerTimes = "PAT_VISIT.DECUBITAL_ULCER_TIMES";

        /// <summary>
        /// 发生褥疮次数
        /// </summary>
        [AttributeColumn(ColumnName = "DECUBITAL_ULCER_TIMES",ColType = "NUMBER_2_0",CanNullOrEmpty = true)]
        public Decimal DecubitalUlcerTimes
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 过敏药物
        /// </summary>
        public static readonly string ColName_AlergyDrugs = "PAT_VISIT.ALERGY_DRUGS";

        /// <summary>
        /// 过敏药物
        /// </summary>
        [AttributeColumn(ColumnName = "ALERGY_DRUGS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 80)]
        public string AlergyDrugs
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 不良反应药物
        /// </summary>
        public static readonly string ColName_AdverseReactionDrugs = "PAT_VISIT.ADVERSE_REACTION_DRUGS";

        /// <summary>
        /// 不良反应药物
        /// </summary>
        [AttributeColumn(ColumnName = "ADVERSE_REACTION_DRUGS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 80)]
        public string AdverseReactionDrugs
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 病案价值
        /// </summary>
        public static readonly string ColName_MrValue = "PAT_VISIT.MR_VALUE";

        /// <summary>
        /// 病案价值
        /// </summary>
        [AttributeColumn(ColumnName = "MR_VALUE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 4)]
        public string MrValue
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 病案质量
        /// </summary>
        public static readonly string ColName_MrQuality = "PAT_VISIT.MR_QUALITY";

        /// <summary>
        /// 病案质量
        /// </summary>
        [AttributeColumn(ColumnName = "MR_QUALITY",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 2)]
        public string MrQuality
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 随诊标志
        /// </summary>
        public static readonly string ColName_FollowIndicator = "PAT_VISIT.FOLLOW_INDICATOR";

        /// <summary>
        /// 随诊标志
        /// </summary>
        [AttributeColumn(ColumnName = "FOLLOW_INDICATOR",ColType = "NUMBER_1_0",CanNullOrEmpty = true)]
        public Decimal FollowIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 随诊期限
        /// </summary>
        public static readonly string ColName_FollowInterval = "PAT_VISIT.FOLLOW_INTERVAL";

        /// <summary>
        /// 随诊期限
        /// </summary>
        [AttributeColumn(ColumnName = "FOLLOW_INTERVAL",ColType = "NUMBER_2_0",CanNullOrEmpty = true)]
        public Decimal FollowInterval
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 随诊期限单位
        /// </summary>
        public static readonly string ColName_FollowIntervalUnits = "PAT_VISIT.FOLLOW_INTERVAL_UNITS";

        /// <summary>
        /// 随诊期限单位
        /// </summary>
        [AttributeColumn(ColumnName = "FOLLOW_INTERVAL_UNITS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 2)]
        public string FollowIntervalUnits
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 科主任
        /// </summary>
        public static readonly string ColName_Director = "PAT_VISIT.DIRECTOR";

        /// <summary>
        /// 科主任
        /// </summary>
        [AttributeColumn(ColumnName = "DIRECTOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 20)]
        public string Director
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 主治医师
        /// </summary>
        public static readonly string ColName_AttendingDoctor = "PAT_VISIT.ATTENDING_DOCTOR";

        /// <summary>
        /// 主治医师
        /// </summary>
        [AttributeColumn(ColumnName = "ATTENDING_DOCTOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 20)]
        public string AttendingDoctor
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 经治医师
        /// </summary>
        public static readonly string ColName_DoctorInCharge = "PAT_VISIT.DOCTOR_IN_CHARGE";

        /// <summary>
        /// 经治医师
        /// </summary>
        [AttributeColumn(ColumnName = "DOCTOR_IN_CHARGE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 20)]
        public string DoctorInCharge
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 出院方式
        /// </summary>
        public static readonly string ColName_DischargeDisposition = "PAT_VISIT.DISCHARGE_DISPOSITION";

        /// <summary>
        /// 出院方式
        /// </summary>
        [AttributeColumn(ColumnName = "DISCHARGE_DISPOSITION",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string DischargeDisposition
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 总费用
        /// </summary>
        public static readonly string ColName_TotalCosts = "PAT_VISIT.TOTAL_COSTS";

        /// <summary>
        /// 总费用
        /// </summary>
        [AttributeColumn(ColumnName = "TOTAL_COSTS",ColType = "NUMBER_12_4",CanNullOrEmpty = true)]
        public Decimal TotalCosts
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 实付费用
        /// </summary>
        public static readonly string ColName_TotalPayments = "PAT_VISIT.TOTAL_PAYMENTS";

        /// <summary>
        /// 实付费用
        /// </summary>
        [AttributeColumn(ColumnName = "TOTAL_PAYMENTS",ColType = "NUMBER_12_4",CanNullOrEmpty = true)]
        public Decimal TotalPayments
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 编目日期
        /// </summary>
        public static readonly string ColName_CatalogDate = "PAT_VISIT.CATALOG_DATE";

        /// <summary>
        /// 编目日期
        /// </summary>
        [AttributeColumn(ColumnName = "CATALOG_DATE",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime CatalogDate
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 编目人
        /// </summary>
        public static readonly string ColName_Cataloger = "PAT_VISIT.CATALOGER";

        /// <summary>
        /// 编目人
        /// </summary>
        [AttributeColumn(ColumnName = "CATALOGER",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 20)]
        public string Cataloger
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: HbsAg
        /// </summary>
        public static readonly string ColName_HbsagIndicator = "PAT_VISIT.HBSAG_INDICATOR";

        /// <summary>
        /// HbsAg
        /// </summary>
        [AttributeColumn(ColumnName = "HBSAG_INDICATOR",ColType = "NUMBER_1_0",CanNullOrEmpty = true)]
        public Decimal HbsagIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: HCV-Ab
        /// </summary>
        public static readonly string ColName_HcvAbIndicator = "PAT_VISIT.HCV_AB_INDICATOR";

        /// <summary>
        /// HCV-Ab
        /// </summary>
        [AttributeColumn(ColumnName = "HCV_AB_INDICATOR",ColType = "NUMBER_1_0",CanNullOrEmpty = true)]
        public Decimal HcvAbIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: HIV_AB
        /// </summary>
        public static readonly string ColName_HivAbIndicator = "PAT_VISIT.HIV_AB_INDICATOR";

        /// <summary>
        /// HIV_AB
        /// </summary>
        [AttributeColumn(ColumnName = "HIV_AB_INDICATOR",ColType = "NUMBER_1_0",CanNullOrEmpty = true)]
        public Decimal HivAbIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 主任医师
        /// </summary>
        public static readonly string ColName_ChiefDoctor = "PAT_VISIT.CHIEF_DOCTOR";

        /// <summary>
        /// 主任医师
        /// </summary>
        [AttributeColumn(ColumnName = "CHIEF_DOCTOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string ChiefDoctor
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 进修医师
        /// </summary>
        public static readonly string ColName_AdvancedStudiesDoctor = "PAT_VISIT.ADVANCED_STUDIES_DOCTOR";

        /// <summary>
        /// 进修医师
        /// </summary>
        [AttributeColumn(ColumnName = "ADVANCED_STUDIES_DOCTOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string AdvancedStudiesDoctor
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 研究生实习医师
        /// </summary>
        public static readonly string ColName_PracticeDoctorOfGraduate = "PAT_VISIT.PRACTICE_DOCTOR_OF_GRADUATE";

        /// <summary>
        /// 研究生实习医师
        /// </summary>
        [AttributeColumn(ColumnName = "PRACTICE_DOCTOR_OF_GRADUATE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string PracticeDoctorOfGraduate
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 实习医师
        /// </summary>
        public static readonly string ColName_PracticeDoctor = "PAT_VISIT.PRACTICE_DOCTOR";

        /// <summary>
        /// 实习医师
        /// </summary>
        [AttributeColumn(ColumnName = "PRACTICE_DOCTOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string PracticeDoctor
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 质控医师
        /// </summary>
        public static readonly string ColName_DoctorOfControlQuality = "PAT_VISIT.DOCTOR_OF_CONTROL_QUALITY";

        /// <summary>
        /// 质控医师
        /// </summary>
        [AttributeColumn(ColumnName = "DOCTOR_OF_CONTROL_QUALITY",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string DoctorOfControlQuality
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 质控护士
        /// </summary>
        public static readonly string ColName_NurseOfControlQuality = "PAT_VISIT.NURSE_OF_CONTROL_QUALITY";

        /// <summary>
        /// 质控护士
        /// </summary>
        [AttributeColumn(ColumnName = "NURSE_OF_CONTROL_QUALITY",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string NurseOfControlQuality
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 质控日期
        /// </summary>
        public static readonly string ColName_DateOfControlQuality = "PAT_VISIT.DATE_OF_CONTROL_QUALITY";

        /// <summary>
        /// 质控日期
        /// </summary>
        [AttributeColumn(ColumnName = "DATE_OF_CONTROL_QUALITY",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime DateOfControlQuality
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 是否为本院第一例
        /// </summary>
        public static readonly string ColName_FirstCaseIndicator = "PAT_VISIT.FIRST_CASE_INDICATOR";

        /// <summary>
        /// 是否为本院第一例
        /// </summary>
        [AttributeColumn(ColumnName = "FIRST_CASE_INDICATOR",ColType = "NUMBER_1_0",CanNullOrEmpty = true)]
        public Decimal FirstCaseIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 三级护理天数
        /// </summary>
        public static readonly string ColName_ThirdLevelNursDays = "PAT_VISIT.THIRD_LEVEL_NURS_DAYS";

        /// <summary>
        /// 三级护理天数
        /// </summary>
        [AttributeColumn(ColumnName = "THIRD_LEVEL_NURS_DAYS",ColType = "NUMBER_4_0",CanNullOrEmpty = true)]
        public Decimal ThirdLevelNursDays
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: X线号
        /// </summary>
        public static readonly string ColName_XExamNo = "PAT_VISIT.X_EXAM_NO";

        /// <summary>
        /// X线号
        /// </summary>
        [AttributeColumn(ColumnName = "X_EXAM_NO",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 10)]
        public string XExamNo
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 入院前经外院诊治
        /// </summary>
        public static readonly string ColName_TreatedInOthersIndicator = "PAT_VISIT.TREATED_IN_OTHERS_INDICATOR";

        /// <summary>
        /// 入院前经外院诊治
        /// </summary>
        [AttributeColumn(ColumnName = "TREATED_IN_OTHERS_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string TreatedInOthersIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 治疗类别
        /// </summary>
        public static readonly string ColName_TreatMethod = "PAT_VISIT.TREAT_METHOD";

        /// <summary>
        /// 治疗类别
        /// </summary>
        [AttributeColumn(ColumnName = "TREAT_METHOD",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string TreatMethod
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 自制中药制剂
        /// </summary>
        public static readonly string ColName_HospMadeMedicineIndicator = "PAT_VISIT.HOSP_MADE_MEDICINE_INDICATOR";

        /// <summary>
        /// 自制中药制剂
        /// </summary>
        [AttributeColumn(ColumnName = "HOSP_MADE_MEDICINE_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string HospMadeMedicineIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 病理号
        /// </summary>
        public static readonly string ColName_PathologyNo = "PAT_VISIT.PATHOLOGY_NO";

        /// <summary>
        /// 病理号
        /// </summary>
        [AttributeColumn(ColumnName = "PATHOLOGY_NO",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 10)]
        public string PathologyNo
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 上级医生指导作用
        /// </summary>
        public static readonly string ColName_UpperDoctorGuideEffect = "PAT_VISIT.UPPER_DOCTOR_GUIDE_EFFECT";

        /// <summary>
        /// 上级医生指导作用
        /// </summary>
        [AttributeColumn(ColumnName = "UPPER_DOCTOR_GUIDE_EFFECT",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string UpperDoctorGuideEffect
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 抢救方法
        /// </summary>
        public static readonly string ColName_EmerTreatMethod = "PAT_VISIT.EMER_TREAT_METHOD";

        /// <summary>
        /// 抢救方法
        /// </summary>
        [AttributeColumn(ColumnName = "EMER_TREAT_METHOD",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string EmerTreatMethod
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 住院期间是否出现急症
        /// </summary>
        public static readonly string ColName_IctusIndicator = "PAT_VISIT.ICTUS_INDICATOR";

        /// <summary>
        /// 住院期间是否出现急症
        /// </summary>
        [AttributeColumn(ColumnName = "ICTUS_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string IctusIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 住院期间是否出现危难
        /// </summary>
        public static readonly string ColName_DifficultyIndicator = "PAT_VISIT.DIFFICULTY_INDICATOR";

        /// <summary>
        /// 住院期间是否出现危难
        /// </summary>
        [AttributeColumn(ColumnName = "DIFFICULTY_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string DifficultyIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 外县市来病人
        /// </summary>
        public static readonly string ColName_FromOtherPlaceIndicator = "PAT_VISIT.FROM_OTHER_PLACE_INDICATOR";

        /// <summary>
        /// 外县市来病人
        /// </summary>
        [AttributeColumn(ColumnName = "FROM_OTHER_PLACE_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string FromOtherPlaceIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 出院诊断疑诊
        /// </summary>
        public static readonly string ColName_SuspicionIndicator = "PAT_VISIT.SUSPICION_INDICATOR";

        /// <summary>
        /// 出院诊断疑诊
        /// </summary>
        [AttributeColumn(ColumnName = "SUSPICION_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string SuspicionIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 中医特色治疗
        /// </summary>
        public static readonly string ColName_ChineseMedicineIndicator = "PAT_VISIT.CHINESE_MEDICINE_INDICATOR";

        /// <summary>
        /// 中医特色治疗
        /// </summary>
        [AttributeColumn(ColumnName = "CHINESE_MEDICINE_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string ChineseMedicineIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 手术级别
        /// </summary>
        public static readonly string ColName_OperationScale = "PAT_VISIT.OPERATION_SCALE";

        /// <summary>
        /// 手术级别
        /// </summary>
        [AttributeColumn(ColumnName = "OPERATION_SCALE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string OperationScale
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 辨证准确度
        /// </summary>
        public static readonly string ColName_DiagnosisCorrectness = "PAT_VISIT.DIAGNOSIS_CORRECTNESS";

        /// <summary>
        /// 辨证准确度
        /// </summary>
        [AttributeColumn(ColumnName = "DIAGNOSIS_CORRECTNESS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string DiagnosisCorrectness
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 治法准确度
        /// </summary>
        public static readonly string ColName_TreatMethodCorrectness = "PAT_VISIT.TREAT_METHOD_CORRECTNESS";

        /// <summary>
        /// 治法准确度
        /// </summary>
        [AttributeColumn(ColumnName = "TREAT_METHOD_CORRECTNESS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string TreatMethodCorrectness
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 方药准确度
        /// </summary>
        public static readonly string ColName_PrescriptionCorrectness = "PAT_VISIT.PRESCRIPTION_CORRECTNESS";

        /// <summary>
        /// 方药准确度
        /// </summary>
        [AttributeColumn(ColumnName = "PRESCRIPTION_CORRECTNESS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string PrescriptionCorrectness
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 病案书写齐全
        /// </summary>
        public static readonly string ColName_MrCompleteIndicator = "PAT_VISIT.MR_COMPLETE_INDICATOR";

        /// <summary>
        /// 病案书写齐全
        /// </summary>
        [AttributeColumn(ColumnName = "MR_COMPLETE_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string MrCompleteIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 医学术语应用正确
        /// </summary>
        public static readonly string ColName_MedicalTermCorrectness = "PAT_VISIT.MEDICAL_TERM_CORRECTNESS";

        /// <summary>
        /// 医学术语应用正确
        /// </summary>
        [AttributeColumn(ColumnName = "MEDICAL_TERM_CORRECTNESS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string MedicalTermCorrectness
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 治疗类别判断
        /// </summary>
        public static readonly string ColName_TreatMethodJudgement = "PAT_VISIT.TREAT_METHOD_JUDGEMENT";

        /// <summary>
        /// 治疗类别判断
        /// </summary>
        [AttributeColumn(ColumnName = "TREAT_METHOD_JUDGEMENT",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string TreatMethodJudgement
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 责任护士
        /// </summary>
        public static readonly string ColName_DutyNurse = "PAT_VISIT.DUTY_NURSE";

        /// <summary>
        /// 责任护士
        /// </summary>
        [AttributeColumn(ColumnName = "DUTY_NURSE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 8)]
        public string DutyNurse
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 死亡原因
        /// </summary>
        public static readonly string ColName_DeathReason = "PAT_VISIT.DEATH_REASON";

        /// <summary>
        /// 死亡原因
        /// </summary>
        [AttributeColumn(ColumnName = "DEATH_REASON",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 40)]
        public string DeathReason
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 死亡时间
        /// </summary>
        public static readonly string ColName_DeathDateTime = "PAT_VISIT.DEATH_DATE_TIME";

        /// <summary>
        /// 死亡时间
        /// </summary>
        [AttributeColumn(ColumnName = "DEATH_DATE_TIME",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime DeathDateTime
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 科研病历
        /// </summary>
        public static readonly string ColName_ScienceResearchIndicator = "PAT_VISIT.SCIENCE_RESEARCH_INDICATOR";

        /// <summary>
        /// 科研病历
        /// </summary>
        [AttributeColumn(ColumnName = "SCIENCE_RESEARCH_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string ScienceResearchIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 手术为本院第一例
        /// </summary>
        public static readonly string ColName_FirstOperationIndicator = "PAT_VISIT.FIRST_OPERATION_INDICATOR";

        /// <summary>
        /// 手术为本院第一例
        /// </summary>
        [AttributeColumn(ColumnName = "FIRST_OPERATION_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string FirstOperationIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 治疗为本院第一例
        /// </summary>
        public static readonly string ColName_FirstTreatmentIndicator = "PAT_VISIT.FIRST_TREATMENT_INDICATOR";

        /// <summary>
        /// 治疗为本院第一例
        /// </summary>
        [AttributeColumn(ColumnName = "FIRST_TREATMENT_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string FirstTreatmentIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 检查为本院第一例
        /// </summary>
        public static readonly string ColName_FirstExaminationIndicator = "PAT_VISIT.FIRST_EXAMINATION_INDICATOR";

        /// <summary>
        /// 检查为本院第一例
        /// </summary>
        [AttributeColumn(ColumnName = "FIRST_EXAMINATION_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string FirstExaminationIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 诊断为本院第一例
        /// </summary>
        public static readonly string ColName_FirstDiagnosisIndicator = "PAT_VISIT.FIRST_DIAGNOSIS_INDICATOR";

        /// <summary>
        /// 诊断为本院第一例
        /// </summary>
        [AttributeColumn(ColumnName = "FIRST_DIAGNOSIS_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string FirstDiagnosisIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 住院期间是否出现危重
        /// </summary>
        public static readonly string ColName_SeriousIndicator = "PAT_VISIT.SERIOUS_INDICATOR";

        /// <summary>
        /// 住院期间是否出现危重
        /// </summary>
        [AttributeColumn(ColumnName = "SERIOUS_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string SeriousIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 入院病室
        /// </summary>
        public static readonly string ColName_AdtRoomNo = "PAT_VISIT.ADT_ROOM_NO";

        /// <summary>
        /// 入院病室
        /// </summary>
        [AttributeColumn(ColumnName = "ADT_ROOM_NO",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 4)]
        public string AdtRoomNo
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 出院病室
        /// </summary>
        public static readonly string ColName_DdtRoomNo = "PAT_VISIT.DDT_ROOM_NO";

        /// <summary>
        /// 出院病室
        /// </summary>
        [AttributeColumn(ColumnName = "DDT_ROOM_NO",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 4)]
        public string DdtRoomNo
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 感染类别
        /// </summary>
        public static readonly string ColName_InfectIndicator = "PAT_VISIT.INFECT_INDICATOR";

        /// <summary>
        /// 感染类别
        /// </summary>
        [AttributeColumn(ColumnName = "INFECT_INDICATOR",ColType = "NUMBER_1_0",CanNullOrEmpty = true)]
        public Decimal InfectIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 健康等级
        /// </summary>
        public static readonly string ColName_HealthLevel = "PAT_VISIT.HEALTH_LEVEL";

        /// <summary>
        /// 健康等级
        /// </summary>
        [AttributeColumn(ColumnName = "HEALTH_LEVEL",ColType = "CHAR",CanNullOrEmpty = true,MaxLength= 2)]
        public string HealthLevel
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 诊断错漏
        /// </summary>
        public static readonly string ColName_MrInfectReport = "PAT_VISIT.MR_INFECT_REPORT";

        /// <summary>
        /// 诊断错漏
        /// </summary>
        [AttributeColumn(ColumnName = "MR_INFECT_REPORT",ColType = "CHAR",CanNullOrEmpty = true,MaxLength= 4)]
        public string MrInfectReport
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 是否新生儿
        /// </summary>
        public static readonly string ColName_Newborn = "PAT_VISIT.NEWBORN";

        /// <summary>
        /// 是否新生儿
        /// </summary>
        [AttributeColumn(ColumnName = "NEWBORN",ColType = "CHAR",CanNullOrEmpty = true,MaxLength= 1)]
        public string Newborn
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 保险地区
        /// </summary>
        public static readonly string ColName_InsuranceAera = "PAT_VISIT.INSURANCE_AERA";

        /// <summary>
        /// 保险地区
        /// </summary>
        [AttributeColumn(ColumnName = "INSURANCE_AERA",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 60)]
        public string InsuranceAera
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 体重
        /// </summary>
        public static readonly string ColName_BodyWeight = "PAT_VISIT.BODY_WEIGHT";

        /// <summary>
        /// 体重
        /// </summary>
        [AttributeColumn(ColumnName = "BODY_WEIGHT",ColType = "NUMBER_5_2",CanNullOrEmpty = true)]
        public Decimal BodyWeight
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 身高
        /// </summary>
        public static readonly string ColName_BodyHeight = "PAT_VISIT.BODY_HEIGHT";

        /// <summary>
        /// 身高
        /// </summary>
        [AttributeColumn(ColumnName = "BODY_HEIGHT",ColType = "NUMBER_4_1",CanNullOrEmpty = true)]
        public Decimal BodyHeight
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 工作单位邮政编码
        /// </summary>
        public static readonly string ColName_BusinessZipCode = "PAT_VISIT.BUSINESS_ZIP_CODE";

        /// <summary>
        /// 工作单位邮政编码
        /// </summary>
        [AttributeColumn(ColumnName = "BUSINESS_ZIP_CODE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 6)]
        public string BusinessZipCode
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 输液次数
        /// </summary>
        public static readonly string ColName_InfusionTranTimes = "PAT_VISIT.INFUSION_TRAN_TIMES";

        /// <summary>
        /// 输液次数
        /// </summary>
        [AttributeColumn(ColumnName = "INFUSION_TRAN_TIMES",ColType = "NUMBER_22_0",CanNullOrEmpty = true)]
        public Decimal InfusionTranTimes
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 
        /// </summary>
        public static readonly string ColName_ChangeIndicator = "PAT_VISIT.CHANGE_INDICATOR";

        /// <summary>
        /// 
        /// </summary>
        [AttributeColumn(ColumnName = "CHANGE_INDICATOR",ColType = "NUMBER_1_0",CanNullOrEmpty = true)]
        public Decimal ChangeIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 首页归档日期
        /// </summary>
        public static readonly string ColName_DocumDate = "PAT_VISIT.DOCUM_DATE";

        /// <summary>
        /// 首页归档日期
        /// </summary>
        [AttributeColumn(ColumnName = "DOCUM_DATE",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime DocumDate
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 归档天数
        /// </summary>
        public static readonly string ColName_DocumDays = "PAT_VISIT.DOCUM_DAYS";

        /// <summary>
        /// 归档天数
        /// </summary>
        [AttributeColumn(ColumnName = "DOCUM_DAYS",ColType = "NUMBER_3_0",CanNullOrEmpty = true)]
        public Decimal DocumDays
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 首页归档人员
        /// </summary>
        public static readonly string ColName_DocumPerson = "PAT_VISIT.DOCUM_PERSON";

        /// <summary>
        /// 首页归档人员
        /// </summary>
        [AttributeColumn(ColumnName = "DOCUM_PERSON",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string DocumPerson
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 传染病标志(1已报 2未报 3无)
        /// </summary>
        public static readonly string ColName_ZymosisIndicator = "PAT_VISIT.ZYMOSIS_INDICATOR";

        /// <summary>
        /// 传染病标志(1已报 2未报 3无)
        /// </summary>
        [AttributeColumn(ColumnName = "ZYMOSIS_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string ZymosisIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 上报日期
        /// </summary>
        public static readonly string ColName_ZymosisDate = "PAT_VISIT.ZYMOSIS_DATE";

        /// <summary>
        /// 上报日期
        /// </summary>
        [AttributeColumn(ColumnName = "ZYMOSIS_DATE",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime ZymosisDate
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 呼吸机用时
        /// </summary>
        public static readonly string ColName_BreathMachTimes = "PAT_VISIT.BREATH_MACH_TIMES";

        /// <summary>
        /// 呼吸机用时
        /// </summary>
        [AttributeColumn(ColumnName = "BREATH_MACH_TIMES",ColType = "NUMBER_3_0",CanNullOrEmpty = true)]
        public Decimal BreathMachTimes
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 昏迷时间(入院前小时)
        /// </summary>
        public static readonly string ColName_ComaTimesB1 = "PAT_VISIT.COMA_TIMES_B1";

        /// <summary>
        /// 昏迷时间(入院前小时)
        /// </summary>
        [AttributeColumn(ColumnName = "COMA_TIMES_B1",ColType = "NUMBER_3_0",CanNullOrEmpty = true)]
        public Decimal ComaTimesB1
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 入院前分钟
        /// </summary>
        public static readonly string ColName_ComaTimesB2 = "PAT_VISIT.COMA_TIMES_B2";

        /// <summary>
        /// 入院前分钟
        /// </summary>
        [AttributeColumn(ColumnName = "COMA_TIMES_B2",ColType = "NUMBER_3_0",CanNullOrEmpty = true)]
        public Decimal ComaTimesB2
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 入院后小时
        /// </summary>
        public static readonly string ColName_ComaTimesA1 = "PAT_VISIT.COMA_TIMES_A1";

        /// <summary>
        /// 入院后小时
        /// </summary>
        [AttributeColumn(ColumnName = "COMA_TIMES_A1",ColType = "NUMBER_3_0",CanNullOrEmpty = true)]
        public Decimal ComaTimesA1
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 入院后分钟
        /// </summary>
        public static readonly string ColName_ComaTimesA2 = "PAT_VISIT.COMA_TIMES_A2";

        /// <summary>
        /// 入院后分钟
        /// </summary>
        [AttributeColumn(ColumnName = "COMA_TIMES_A2",ColType = "NUMBER_3_0",CanNullOrEmpty = true)]
        public Decimal ComaTimesA2
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 转入医院名称
        /// </summary>
        public static readonly string ColName_TransHospital = "PAT_VISIT.TRANS_HOSPITAL";

        /// <summary>
        /// 转入医院名称
        /// </summary>
        [AttributeColumn(ColumnName = "TRANS_HOSPITAL",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 100)]
        public string TransHospital
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 转社区名称
        /// </summary>
        public static readonly string ColName_TransCommunity = "PAT_VISIT.TRANS_COMMUNITY";

        /// <summary>
        /// 转社区名称
        /// </summary>
        [AttributeColumn(ColumnName = "TRANS_COMMUNITY",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 100)]
        public string TransCommunity
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 工作单位电话
        /// </summary>
        public static readonly string ColName_PhoneNumberBusiness = "PAT_VISIT.PHONE_NUMBER_BUSINESS";

        /// <summary>
        /// 工作单位电话
        /// </summary>
        [AttributeColumn(ColumnName = "PHONE_NUMBER_BUSINESS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 16)]
        public string PhoneNumberBusiness
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 整理者
        /// </summary>
        public static readonly string ColName_MrBinder = "PAT_VISIT.MR_BINDER";

        /// <summary>
        /// 整理者
        /// </summary>
        [AttributeColumn(ColumnName = "MR_BINDER",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string MrBinder
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 
        /// </summary>
        public static readonly string ColName_CarryPersonNumber = "PAT_VISIT.CARRY_PERSON_NUMBER";

        /// <summary>
        /// 
        /// </summary>
        [AttributeColumn(ColumnName = "CARRY_PERSON_NUMBER",ColType = "NUMBER_22_0",CanNullOrEmpty = true)]
        public Decimal CarryPersonNumber
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 挂号就诊日期
        /// </summary>
        public static readonly string ColName_VisitDate = "PAT_VISIT.VISIT_DATE";

        /// <summary>
        /// 挂号就诊日期
        /// </summary>
        [AttributeColumn(ColumnName = "VISIT_DATE",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime VisitDate
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 挂号就诊序号
        /// </summary>
        public static readonly string ColName_VisitNo = "PAT_VISIT.VISIT_NO";

        /// <summary>
        /// 挂号就诊序号
        /// </summary>
        [AttributeColumn(ColumnName = "VISIT_NO",ColType = "NUMBER_5_0",CanNullOrEmpty = true)]
        public Decimal VisitNo
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 出院时的床位号
        /// </summary>
        public static readonly string ColName_DischargeBedNo = "PAT_VISIT.DISCHARGE_BED_NO";

        /// <summary>
        /// 出院时的床位号
        /// </summary>
        [AttributeColumn(ColumnName = "DISCHARGE_BED_NO",ColType = "NUMBER_8_0",CanNullOrEmpty = true)]
        public Decimal DischargeBedNo
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 户口所在地电话
        /// </summary>
        public static readonly string ColName_PhoneNumberHome = "PAT_VISIT.PHONE_NUMBER_HOME";

        /// <summary>
        /// 户口所在地电话
        /// </summary>
        [AttributeColumn(ColumnName = "PHONE_NUMBER_HOME",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 16)]
        public string PhoneNumberHome
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 病人来源地址1--本市本区、2--本市外区、3--本省外市、4--外省市
        /// </summary>
        public static readonly string ColName_PatientArea = "PAT_VISIT.PATIENT_AREA";

        /// <summary>
        /// 病人来源地址1--本市本区、2--本市外区、3--本省外市、4--外省市
        /// </summary>
        [AttributeColumn(ColumnName = "PATIENT_AREA",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 6)]
        public string PatientArea
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 出院护理单元
        /// </summary>
        public static readonly string ColName_DischargeWardCode = "PAT_VISIT.DISCHARGE_WARD_CODE";

        /// <summary>
        /// 出院护理单元
        /// </summary>
        [AttributeColumn(ColumnName = "DISCHARGE_WARD_CODE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 8)]
        public string DischargeWardCode
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 编目状态
        /// </summary>
        public static readonly string ColName_CatalogStatus = "PAT_VISIT.CATALOG_STATUS";

        /// <summary>
        /// 编目状态
        /// </summary>
        [AttributeColumn(ColumnName = "CATALOG_STATUS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string CatalogStatus
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 最后一次锁定日期
        /// </summary>
        public static readonly string ColName_LockedDate = "PAT_VISIT.LOCKED_DATE";

        /// <summary>
        /// 最后一次锁定日期
        /// </summary>
        [AttributeColumn(ColumnName = "LOCKED_DATE",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime LockedDate
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 
        /// </summary>
        public static readonly string ColName_TransIns = "PAT_VISIT.TRANS_INS";

        /// <summary>
        /// 
        /// </summary>
        [AttributeColumn(ColumnName = "TRANS_INS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string TransIns
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 曾用号
        /// </summary>
        public static readonly string ColName_InpSerialNo = "PAT_VISIT.INP_SERIAL_NO";

        /// <summary>
        /// 曾用号
        /// </summary>
        [AttributeColumn(ColumnName = "INP_SERIAL_NO",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 20)]
        public string InpSerialNo
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 入院时的床位号
        /// </summary>
        public static readonly string ColName_AdmissionBedNo = "PAT_VISIT.ADMISSION_BED_NO";

        /// <summary>
        /// 入院时的床位号
        /// </summary>
        [AttributeColumn(ColumnName = "ADMISSION_BED_NO",ColType = "NUMBER_8_0",CanNullOrEmpty = true)]
        public Decimal AdmissionBedNo
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 入院确诊日期
        /// </summary>
        public static readonly string ColName_DiagnoseDate = "PAT_VISIT.DIAGNOSE_DATE";

        /// <summary>
        /// 入院确诊日期
        /// </summary>
        [AttributeColumn(ColumnName = "DIAGNOSE_DATE",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime DiagnoseDate
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 医疗付款方式
        /// </summary>
        public static readonly string ColName_MedicalPayWay = "PAT_VISIT.MEDICAL_PAY_WAY";

        /// <summary>
        /// 医疗付款方式
        /// </summary>
        [AttributeColumn(ColumnName = "MEDICAL_PAY_WAY",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string MedicalPayWay
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 病案编目、进入统计标志，当值为1时不编目，不统计
        /// </summary>
        public static readonly string ColName_StatisticsFlag = "PAT_VISIT.STATISTICS_FLAG";

        /// <summary>
        /// 病案编目、进入统计标志，当值为1时不编目，不统计
        /// </summary>
        [AttributeColumn(ColumnName = "STATISTICS_FLAG",ColType = "NUMBER_1_0",CanNullOrEmpty = true)]
        public Decimal StatisticsFlag
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 接受登记标志
        /// </summary>
        public static readonly string ColName_DocumFlag = "PAT_VISIT.DOCUM_FLAG";

        /// <summary>
        /// 接受登记标志
        /// </summary>
        [AttributeColumn(ColumnName = "DOCUM_FLAG",ColType = "NUMBER_22_0",CanNullOrEmpty = true)]
        public Decimal DocumFlag
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 统计用入院确诊日期
        /// </summary>
        public static readonly string ColName_StatisticsDiagnoseDate = "PAT_VISIT.STATISTICS_DIAGNOSE_DATE";

        /// <summary>
        /// 统计用入院确诊日期
        /// </summary>
        [AttributeColumn(ColumnName = "STATISTICS_DIAGNOSE_DATE",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime StatisticsDiagnoseDate
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 
        /// </summary>
        public static readonly string ColName_Ychz = "PAT_VISIT.YCHZ";

        /// <summary>
        /// 
        /// </summary>
        [AttributeColumn(ColumnName = "YCHZ",ColType = "NUMBER_2_0",CanNullOrEmpty = true)]
        public Decimal Ychz
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 
        /// </summary>
        public static readonly string ColName_Yjhz = "PAT_VISIT.YJHZ";

        /// <summary>
        /// 
        /// </summary>
        [AttributeColumn(ColumnName = "YJHZ",ColType = "NUMBER_2_0",CanNullOrEmpty = true)]
        public Decimal Yjhz
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 
        /// </summary>
        public static readonly string ColName_Drgs = "PAT_VISIT.DRGS";

        /// <summary>
        /// 
        /// </summary>
        [AttributeColumn(ColumnName = "DRGS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 2)]
        public string Drgs
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 新生儿出生体重
        /// </summary>
        public static readonly string ColName_WeightBirth = "PAT_VISIT.WEIGHT_BIRTH";

        /// <summary>
        /// 新生儿出生体重
        /// </summary>
        [AttributeColumn(ColumnName = "WEIGHT_BIRTH",ColType = "NUMBER_5_0",CanNullOrEmpty = true)]
        public Decimal WeightBirth
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 入院前昏迷天数
        /// </summary>
        public static readonly string ColName_ComaTimesB0 = "PAT_VISIT.COMA_TIMES_B0";

        /// <summary>
        /// 入院前昏迷天数
        /// </summary>
        [AttributeColumn(ColumnName = "COMA_TIMES_B0",ColType = "NUMBER_3_0",CanNullOrEmpty = true)]
        public Decimal ComaTimesB0
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 入院后昏迷天数
        /// </summary>
        public static readonly string ColName_ComaTimesA0 = "PAT_VISIT.COMA_TIMES_A0";

        /// <summary>
        /// 入院后昏迷天数
        /// </summary>
        [AttributeColumn(ColumnName = "COMA_TIMES_A0",ColType = "NUMBER_3_0",CanNullOrEmpty = true)]
        public Decimal ComaTimesA0
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 现住址名称（回访用）
        /// </summary>
        public static readonly string ColName_PatientAreaAddress = "PAT_VISIT.PATIENT_AREA_ADDRESS";

        /// <summary>
        /// 现住址名称（回访用）
        /// </summary>
        [AttributeColumn(ColumnName = "PATIENT_AREA_ADDRESS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 200)]
        public string PatientAreaAddress
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 现住址乡镇/街道代码
        /// </summary>
        public static readonly string ColName_PatStreetAddress = "PAT_VISIT.PAT_STREET_ADDRESS";

        /// <summary>
        /// 现住址乡镇/街道代码
        /// </summary>
        [AttributeColumn(ColumnName = "PAT_STREET_ADDRESS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 10)]
        public string PatStreetAddress
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 现住址电话
        /// </summary>
        public static readonly string ColName_PatPhone = "PAT_VISIT.PAT_PHONE";

        /// <summary>
        /// 现住址电话
        /// </summary>
        [AttributeColumn(ColumnName = "PAT_PHONE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 16)]
        public string PatPhone
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 现住址邮编
        /// </summary>
        public static readonly string ColName_PatZip = "PAT_VISIT.PAT_ZIP";

        /// <summary>
        /// 现住址邮编
        /// </summary>
        [AttributeColumn(ColumnName = "PAT_ZIP",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 6)]
        public string PatZip
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 是否有31日内再入院计划（1无2有）
        /// </summary>
        public static readonly string ColName_Plan31Admission = "PAT_VISIT.PLAN_31_ADMISSION";

        /// <summary>
        /// 是否有31日内再入院计划（1无2有）
        /// </summary>
        [AttributeColumn(ColumnName = "PLAN_31_ADMISSION",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string Plan31Admission
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 31日内再入院目的
        /// </summary>
        public static readonly string ColName_Reason31Admission = "PAT_VISIT.REASON_31_ADMISSION";

        /// <summary>
        /// 31日内再入院目的
        /// </summary>
        [AttributeColumn(ColumnName = "REASON_31_ADMISSION",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 100)]
        public string Reason31Admission
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 肿瘤分期TNM法T值
        /// </summary>
        public static readonly string ColName_TumorT = "PAT_VISIT.TUMOR_T";

        /// <summary>
        /// 肿瘤分期TNM法T值
        /// </summary>
        [AttributeColumn(ColumnName = "TUMOR_T",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 10)]
        public string TumorT
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 肿瘤分期TNM法N值
        /// </summary>
        public static readonly string ColName_TumorN = "PAT_VISIT.TUMOR_N";

        /// <summary>
        /// 肿瘤分期TNM法N值
        /// </summary>
        [AttributeColumn(ColumnName = "TUMOR_N",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 10)]
        public string TumorN
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 肿瘤分期TNM法M值
        /// </summary>
        public static readonly string ColName_TumorM = "PAT_VISIT.TUMOR_M";

        /// <summary>
        /// 肿瘤分期TNM法M值
        /// </summary>
        [AttributeColumn(ColumnName = "TUMOR_M",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 10)]
        public string TumorM
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 肿瘤0-Ⅳ分期法(0 0期、1 Ⅰ期、2 Ⅱ期、3 Ⅲ期、4 Ⅳ期、9 不详)
        /// </summary>
        public static readonly string ColName_TumorStage = "PAT_VISIT.TUMOR_STAGE";

        /// <summary>
        /// 肿瘤0-Ⅳ分期法(0 0期、1 Ⅰ期、2 Ⅱ期、3 Ⅲ期、4 Ⅳ期、9 不详)
        /// </summary>
        [AttributeColumn(ColumnName = "TUMOR_STAGE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 10)]
        public string TumorStage
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 日常生活能力评定量表（ADL）入院得分
        /// </summary>
        public static readonly string ColName_AdlAdm = "PAT_VISIT.ADL_ADM";

        /// <summary>
        /// 日常生活能力评定量表（ADL）入院得分
        /// </summary>
        [AttributeColumn(ColumnName = "ADL_ADM",ColType = "NUMBER_6_2",CanNullOrEmpty = true)]
        public Decimal AdlAdm
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 日常生活能力评定量表（ADL）出院得分
        /// </summary>
        public static readonly string ColName_AdlDis = "PAT_VISIT.ADL_DIS";

        /// <summary>
        /// 日常生活能力评定量表（ADL）出院得分
        /// </summary>
        [AttributeColumn(ColumnName = "ADL_DIS",ColType = "NUMBER_6_2",CanNullOrEmpty = true)]
        public Decimal AdlDis
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 北京市肿瘤患者必填
        /// </summary>
        public static readonly string ColName_BasisOn = "PAT_VISIT.BASIS_ON";

        /// <summary>
        /// 北京市肿瘤患者必填
        /// </summary>
        [AttributeColumn(ColumnName = "BASIS_ON",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 8)]
        public string BasisOn
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 北京市肿瘤患者必填
        /// </summary>
        public static readonly string ColName_DiffId = "PAT_VISIT.DIFF_ID";

        /// <summary>
        /// 北京市肿瘤患者必填
        /// </summary>
        [AttributeColumn(ColumnName = "DIFF_ID",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 8)]
        public string DiffId
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 只有锁定的人才能解锁-始于北京航空总医院
        /// </summary>
        public static readonly string ColName_LockedUser = "PAT_VISIT.LOCKED_USER";

        /// <summary>
        /// 只有锁定的人才能解锁-始于北京航空总医院
        /// </summary>
        [AttributeColumn(ColumnName = "LOCKED_USER",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 20)]
        public string LockedUser
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 住院号
        /// </summary>
        public static readonly string ColName_InpNo = "PAT_VISIT.INP_NO";

        /// <summary>
        /// 住院号
        /// </summary>
        [AttributeColumn(ColumnName = "INP_NO",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 10)]
        public string InpNo
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 
        /// </summary>
        public static readonly string ColName_ThirdLevelNurseDays = "PAT_VISIT.THIRD_LEVEL_NURSE_DAYS";

        /// <summary>
        /// 
        /// </summary>
        [AttributeColumn(ColumnName = "THIRD_LEVEL_NURSE_DAYS",ColType = "NUMBER_3_0",CanNullOrEmpty = true)]
        public Decimal ThirdLevelNurseDays
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 
        /// </summary>
        public static readonly string ColName_InfusionReactIndicator = "PAT_VISIT.INFUSION_REACT_INDICATOR";

        /// <summary>
        /// 
        /// </summary>
        [AttributeColumn(ColumnName = "INFUSION_REACT_INDICATOR",ColType = "NUMBER_1_0",CanNullOrEmpty = true)]
        public Decimal InfusionReactIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 
        /// </summary>
        public static readonly string ColName_PperDoctorGuideEffect = "PAT_VISIT.PPER_DOCTOR_GUIDE_EFFECT";

        /// <summary>
        /// 
        /// </summary>
        [AttributeColumn(ColumnName = "PPER_DOCTOR_GUIDE_EFFECT",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 20)]
        public string PperDoctorGuideEffect
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 传染病上报状态
        /// </summary>
        public static readonly string ColName_InfectiousDiseaseReport = "PAT_VISIT.INFECTIOUS_DISEASE_REPORT";

        /// <summary>
        /// 传染病上报状态
        /// </summary>
        [AttributeColumn(ColumnName = "INFECTIOUS_DISEASE_REPORT",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string InfectiousDiseaseReport
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 细菌培养及药敏
        /// </summary>
        public static readonly string ColName_GermicultureDrugTest = "PAT_VISIT.GERMICULTURE_DRUG_TEST";

        /// <summary>
        /// 细菌培养及药敏
        /// </summary>
        [AttributeColumn(ColumnName = "GERMICULTURE_DRUG_TEST",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string GermicultureDrugTest
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 抗菌素联合使用情况
        /// </summary>
        public static readonly string ColName_AntibioticsUnion = "PAT_VISIT.ANTIBIOTICS_UNION";

        /// <summary>
        /// 抗菌素联合使用情况
        /// </summary>
        [AttributeColumn(ColumnName = "ANTIBIOTICS_UNION",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string AntibioticsUnion
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 使用抗菌素种类数
        /// </summary>
        public static readonly string ColName_AntibioticsCount = "PAT_VISIT.ANTIBIOTICS_COUNT";

        /// <summary>
        /// 使用抗菌素种类数
        /// </summary>
        [AttributeColumn(ColumnName = "ANTIBIOTICS_COUNT",ColType = "NUMBER_2_0",CanNullOrEmpty = true)]
        public Decimal AntibioticsCount
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 是否使用抗菌素
        /// </summary>
        public static readonly string ColName_AntibioticsUsed = "PAT_VISIT.ANTIBIOTICS_USED";

        /// <summary>
        /// 是否使用抗菌素
        /// </summary>
        [AttributeColumn(ColumnName = "ANTIBIOTICS_USED",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string AntibioticsUsed
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 输血反应
        /// </summary>
        public static readonly string ColName_BloodTranFlag = "PAT_VISIT.BLOOD_TRAN_FLAG";

        /// <summary>
        /// 输血反应
        /// </summary>
        [AttributeColumn(ColumnName = "BLOOD_TRAN_FLAG",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string BloodTranFlag
        {
          get;
          set;
        }

        /// <summary>
        /// 列名: 明细序号
        /// </summary>
        public static readonly string ColName_RegSubNo = "PAT_VISIT.REG_SUB_NO";

        /// <summary>
        /// 明细序号
        /// </summary>
        [AttributeColumn(ColumnName = "REG_SUB_NO",ColType = "NUMBER_2_0",CanNullOrEmpty = true)]
        public Decimal RegSubNo
        {
          get;
          set;
        }
    }
}

