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
        /// ����: ???????
        /// </summary>
        public static readonly string TableName = "PAT_VISIT";

        /// <summary>
        /// ������
        /// </summary>
        public static readonly string All = "*";

  
        /// <summary>
        /// ����: ���˱�ʶ
        /// </summary>
        public static readonly string ColName_PatientId = "PAT_VISIT.PATIENT_ID";

        /// <summary>
        /// ���˱�ʶ
        /// </summary>
        [AttributeColumn(ColumnName = "PATIENT_ID",ColType = "VARCHAR2",CanNullOrEmpty = false,MaxLength= 10,IsPrimaryKey = true)]
        public string PatientId
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ���˱���סԺ��ʶ
        /// </summary>
        public static readonly string ColName_VisitId = "PAT_VISIT.VISIT_ID";

        /// <summary>
        /// ���˱���סԺ��ʶ
        /// </summary>
        [AttributeColumn(ColumnName = "VISIT_ID",ColType = "NUMBER_2_0",CanNullOrEmpty = false,IsPrimaryKey = true)]
        public Decimal VisitId
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ժ����
        /// </summary>
        public static readonly string ColName_DeptAdmissionTo = "PAT_VISIT.DEPT_ADMISSION_TO";

        /// <summary>
        /// ��Ժ����
        /// </summary>
        [AttributeColumn(ColumnName = "DEPT_ADMISSION_TO",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 8)]
        public string DeptAdmissionTo
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ժ���ڼ�ʱ��
        /// </summary>
        public static readonly string ColName_AdmissionDateTime = "PAT_VISIT.ADMISSION_DATE_TIME";

        /// <summary>
        /// ��Ժ���ڼ�ʱ��
        /// </summary>
        [AttributeColumn(ColumnName = "ADMISSION_DATE_TIME",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime AdmissionDateTime
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ժ����
        /// </summary>
        public static readonly string ColName_DeptDischargeFrom = "PAT_VISIT.DEPT_DISCHARGE_FROM";

        /// <summary>
        /// ��Ժ����
        /// </summary>
        [AttributeColumn(ColumnName = "DEPT_DISCHARGE_FROM",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 8)]
        public string DeptDischargeFrom
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ժ���ڼ�ʱ��
        /// </summary>
        public static readonly string ColName_DischargeDateTime = "PAT_VISIT.DISCHARGE_DATE_TIME";

        /// <summary>
        /// ��Ժ���ڼ�ʱ��
        /// </summary>
        [AttributeColumn(ColumnName = "DISCHARGE_DATE_TIME",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime DischargeDateTime
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ְҵ
        /// </summary>
        public static readonly string ColName_Occupation = "PAT_VISIT.OCCUPATION";

        /// <summary>
        /// ְҵ
        /// </summary>
        [AttributeColumn(ColumnName = "OCCUPATION",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 2)]
        public string Occupation
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ����״��
        /// </summary>
        public static readonly string ColName_MaritalStatus = "PAT_VISIT.MARITAL_STATUS";

        /// <summary>
        /// ����״��
        /// </summary>
        [AttributeColumn(ColumnName = "MARITAL_STATUS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 4)]
        public string MaritalStatus
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ���
        /// </summary>
        public static readonly string ColName_Identity = "PAT_VISIT.IDENTITY";

        /// <summary>
        /// ���
        /// </summary>
        [AttributeColumn(ColumnName = "IDENTITY",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string Identity
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ����
        /// </summary>
        public static readonly string ColName_ArmedServices = "PAT_VISIT.ARMED_SERVICES";

        /// <summary>
        /// ����
        /// </summary>
        [AttributeColumn(ColumnName = "ARMED_SERVICES",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 4)]
        public string ArmedServices
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ����
        /// </summary>
        public static readonly string ColName_Duty = "PAT_VISIT.DUTY";

        /// <summary>
        /// ����
        /// </summary>
        [AttributeColumn(ColumnName = "DUTY",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 19)]
        public string Duty
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ������λ
        /// </summary>
        public static readonly string ColName_TopUnit = "PAT_VISIT.TOP_UNIT";

        /// <summary>
        /// ������λ
        /// </summary>
        [AttributeColumn(ColumnName = "TOP_UNIT",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string TopUnit
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �ѱ�
        /// </summary>
        public static readonly string ColName_ServiceSystemIndicator = "PAT_VISIT.SERVICE_SYSTEM_INDICATOR";

        /// <summary>
        /// �ѱ�
        /// </summary>
        [AttributeColumn(ColumnName = "SERVICE_SYSTEM_INDICATOR",ColType = "NUMBER_1_0",CanNullOrEmpty = true)]
        public Decimal ServiceSystemIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��ͬ��λ
        /// </summary>
        public static readonly string ColName_UnitInContract = "PAT_VISIT.UNIT_IN_CONTRACT";

        /// <summary>
        /// ��ͬ��λ
        /// </summary>
        [AttributeColumn(ColumnName = "UNIT_IN_CONTRACT",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 11)]
        public string UnitInContract
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ҽ�����
        /// </summary>
        public static readonly string ColName_ChargeType = "PAT_VISIT.CHARGE_TYPE";

        /// <summary>
        /// ҽ�����
        /// </summary>
        [AttributeColumn(ColumnName = "CHARGE_TYPE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 20)]
        public string ChargeType
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��ְ��־
        /// </summary>
        public static readonly string ColName_WorkingStatus = "PAT_VISIT.WORKING_STATUS";

        /// <summary>
        /// ��ְ��־
        /// </summary>
        [AttributeColumn(ColumnName = "WORKING_STATUS",ColType = "NUMBER_1_0",CanNullOrEmpty = true)]
        public Decimal WorkingStatus
        {
          get;
          set;
        }

        /// <summary>
        /// ����: 
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
        /// ����: ҽ�Ʊ��պ�
        /// </summary>
        public static readonly string ColName_InsuranceNo = "PAT_VISIT.INSURANCE_NO";

        /// <summary>
        /// ҽ�Ʊ��պ�
        /// </summary>
        [AttributeColumn(ColumnName = "INSURANCE_NO",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 18)]
        public string InsuranceNo
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ������λ
        /// </summary>
        public static readonly string ColName_ServiceAgency = "PAT_VISIT.SERVICE_AGENCY";

        /// <summary>
        /// ������λ
        /// </summary>
        [AttributeColumn(ColumnName = "SERVICE_AGENCY",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 100)]
        public string ServiceAgency
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ͨ�ŵ�ַ(���ڵ�ַ)
        /// </summary>
        public static readonly string ColName_MailingAddress = "PAT_VISIT.MAILING_ADDRESS";

        /// <summary>
        /// ͨ�ŵ�ַ(���ڵ�ַ)
        /// </summary>
        [AttributeColumn(ColumnName = "MAILING_ADDRESS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 200)]
        public string MailingAddress
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��������
        /// </summary>
        public static readonly string ColName_ZipCode = "PAT_VISIT.ZIP_CODE";

        /// <summary>
        /// ��������
        /// </summary>
        [AttributeColumn(ColumnName = "ZIP_CODE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 6)]
        public string ZipCode
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��ϵ������
        /// </summary>
        public static readonly string ColName_NextOfKin = "PAT_VISIT.NEXT_OF_KIN";

        /// <summary>
        /// ��ϵ������
        /// </summary>
        [AttributeColumn(ColumnName = "NEXT_OF_KIN",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 20)]
        public string NextOfKin
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ����ϵ�˹�ϵ
        /// </summary>
        public static readonly string ColName_Relationship = "PAT_VISIT.RELATIONSHIP";

        /// <summary>
        /// ����ϵ�˹�ϵ
        /// </summary>
        [AttributeColumn(ColumnName = "RELATIONSHIP",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 2)]
        public string Relationship
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��ϵ�˵�ַ
        /// </summary>
        public static readonly string ColName_NextOfKinAddr = "PAT_VISIT.NEXT_OF_KIN_ADDR";

        /// <summary>
        /// ��ϵ�˵�ַ
        /// </summary>
        [AttributeColumn(ColumnName = "NEXT_OF_KIN_ADDR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 40)]
        public string NextOfKinAddr
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��ϵ����������
        /// </summary>
        public static readonly string ColName_NextOfKinZipcode = "PAT_VISIT.NEXT_OF_KIN_ZIPCODE";

        /// <summary>
        /// ��ϵ����������
        /// </summary>
        [AttributeColumn(ColumnName = "NEXT_OF_KIN_ZIPCODE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 6)]
        public string NextOfKinZipcode
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��ϵ�˵绰
        /// </summary>
        public static readonly string ColName_NextOfKinPhone = "PAT_VISIT.NEXT_OF_KIN_PHONE";

        /// <summary>
        /// ��ϵ�˵绰
        /// </summary>
        [AttributeColumn(ColumnName = "NEXT_OF_KIN_PHONE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 16)]
        public string NextOfKinPhone
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ժ��ʽ
        /// </summary>
        public static readonly string ColName_PatientClass = "PAT_VISIT.PATIENT_CLASS";

        /// <summary>
        /// ��Ժ��ʽ
        /// </summary>
        [AttributeColumn(ColumnName = "PATIENT_CLASS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 10)]
        public string PatientClass
        {
          get;
          set;
        }

        /// <summary>
        /// ����: סԺĿ��
        /// </summary>
        public static readonly string ColName_AdmissionCause = "PAT_VISIT.ADMISSION_CAUSE";

        /// <summary>
        /// סԺĿ��
        /// </summary>
        [AttributeColumn(ColumnName = "ADMISSION_CAUSE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 8)]
        public string AdmissionCause
        {
          get;
          set;
        }

        /// <summary>
        /// ����: סԺĿ��
        /// </summary>
        public static readonly string ColName_ConsultingDate = "PAT_VISIT.CONSULTING_DATE";

        /// <summary>
        /// סԺĿ��
        /// </summary>
        [AttributeColumn(ColumnName = "CONSULTING_DATE",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime ConsultingDate
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ժ����
        /// </summary>
        public static readonly string ColName_PatAdmCondition = "PAT_VISIT.PAT_ADM_CONDITION";

        /// <summary>
        /// ��Ժ����
        /// </summary>
        [AttributeColumn(ColumnName = "PAT_ADM_CONDITION",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string PatAdmCondition
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ����ҽʦ
        /// </summary>
        public static readonly string ColName_ConsultingDoctor = "PAT_VISIT.CONSULTING_DOCTOR";

        /// <summary>
        /// ����ҽʦ
        /// </summary>
        [AttributeColumn(ColumnName = "CONSULTING_DOCTOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string ConsultingDoctor
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ����סԺ��
        /// </summary>
        public static readonly string ColName_AdmittedBy = "PAT_VISIT.ADMITTED_BY";

        /// <summary>
        /// ����סԺ��
        /// </summary>
        [AttributeColumn(ColumnName = "ADMITTED_BY",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string AdmittedBy
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ���ȴ���
        /// </summary>
        public static readonly string ColName_EmerTreatTimes = "PAT_VISIT.EMER_TREAT_TIMES";

        /// <summary>
        /// ���ȴ���
        /// </summary>
        [AttributeColumn(ColumnName = "EMER_TREAT_TIMES",ColType = "NUMBER_2_0",CanNullOrEmpty = true)]
        public Decimal EmerTreatTimes
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ���ȳɹ�����
        /// </summary>
        public static readonly string ColName_EscEmerTimes = "PAT_VISIT.ESC_EMER_TIMES";

        /// <summary>
        /// ���ȳɹ�����
        /// </summary>
        [AttributeColumn(ColumnName = "ESC_EMER_TIMES",ColType = "NUMBER_2_0",CanNullOrEmpty = true)]
        public Decimal EscEmerTimes
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��������
        /// </summary>
        public static readonly string ColName_SeriousCondDays = "PAT_VISIT.SERIOUS_COND_DAYS";

        /// <summary>
        /// ��������
        /// </summary>
        [AttributeColumn(ColumnName = "SERIOUS_COND_DAYS",ColType = "NUMBER_4_0",CanNullOrEmpty = true)]
        public Decimal SeriousCondDays
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Σ����
        /// </summary>
        public static readonly string ColName_CriticalCondDays = "PAT_VISIT.CRITICAL_COND_DAYS";

        /// <summary>
        /// ��Σ����
        /// </summary>
        [AttributeColumn(ColumnName = "CRITICAL_COND_DAYS",ColType = "NUMBER_4_0",CanNullOrEmpty = true)]
        public Decimal CriticalCondDays
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ICU����
        /// </summary>
        public static readonly string ColName_IcuDays = "PAT_VISIT.ICU_DAYS";

        /// <summary>
        /// ICU����
        /// </summary>
        [AttributeColumn(ColumnName = "ICU_DAYS",ColType = "NUMBER_4_0",CanNullOrEmpty = true)]
        public Decimal IcuDays
        {
          get;
          set;
        }

        /// <summary>
        /// ����: CCU����
        /// </summary>
        public static readonly string ColName_CcuDays = "PAT_VISIT.CCU_DAYS";

        /// <summary>
        /// CCU����
        /// </summary>
        [AttributeColumn(ColumnName = "CCU_DAYS",ColType = "NUMBER_4_0",CanNullOrEmpty = true)]
        public Decimal CcuDays
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �ر�������
        /// </summary>
        public static readonly string ColName_SpecLevelNursDays = "PAT_VISIT.SPEC_LEVEL_NURS_DAYS";

        /// <summary>
        /// �ر�������
        /// </summary>
        [AttributeColumn(ColumnName = "SPEC_LEVEL_NURS_DAYS",ColType = "NUMBER_4_0",CanNullOrEmpty = true)]
        public Decimal SpecLevelNursDays
        {
          get;
          set;
        }

        /// <summary>
        /// ����: һ����������
        /// </summary>
        public static readonly string ColName_FirstLevelNursDays = "PAT_VISIT.FIRST_LEVEL_NURS_DAYS";

        /// <summary>
        /// һ����������
        /// </summary>
        [AttributeColumn(ColumnName = "FIRST_LEVEL_NURS_DAYS",ColType = "NUMBER_4_0",CanNullOrEmpty = true)]
        public Decimal FirstLevelNursDays
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ������������
        /// </summary>
        public static readonly string ColName_SecondLevelNursDays = "PAT_VISIT.SECOND_LEVEL_NURS_DAYS";

        /// <summary>
        /// ������������
        /// </summary>
        [AttributeColumn(ColumnName = "SECOND_LEVEL_NURS_DAYS",ColType = "NUMBER_4_0",CanNullOrEmpty = true)]
        public Decimal SecondLevelNursDays
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ʬ���ʶ
        /// </summary>
        public static readonly string ColName_AutopsyIndicator = "PAT_VISIT.AUTOPSY_INDICATOR";

        /// <summary>
        /// ʬ���ʶ
        /// </summary>
        [AttributeColumn(ColumnName = "AUTOPSY_INDICATOR",ColType = "NUMBER_1_0",CanNullOrEmpty = true)]
        public Decimal AutopsyIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// ����: Ѫ��
        /// </summary>
        public static readonly string ColName_BloodType = "PAT_VISIT.BLOOD_TYPE";

        /// <summary>
        /// Ѫ��
        /// </summary>
        [AttributeColumn(ColumnName = "BLOOD_TYPE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 4)]
        public string BloodType
        {
          get;
          set;
        }

        /// <summary>
        /// ����: RhѪ��
        /// </summary>
        public static readonly string ColName_BloodTypeRh = "PAT_VISIT.BLOOD_TYPE_RH";

        /// <summary>
        /// RhѪ��
        /// </summary>
        [AttributeColumn(ColumnName = "BLOOD_TYPE_RH",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 4)]
        public string BloodTypeRh
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Һ��Ӧ����
        /// </summary>
        public static readonly string ColName_InfusionReactTimes = "PAT_VISIT.INFUSION_REACT_TIMES";

        /// <summary>
        /// ��Һ��Ӧ����
        /// </summary>
        [AttributeColumn(ColumnName = "INFUSION_REACT_TIMES",ColType = "NUMBER_2_0",CanNullOrEmpty = true)]
        public Decimal InfusionReactTimes
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ѫ����
        /// </summary>
        public static readonly string ColName_BloodTranTimes = "PAT_VISIT.BLOOD_TRAN_TIMES";

        /// <summary>
        /// ��Ѫ����
        /// </summary>
        [AttributeColumn(ColumnName = "BLOOD_TRAN_TIMES",ColType = "NUMBER_2_0",CanNullOrEmpty = true)]
        public Decimal BloodTranTimes
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ѫ����
        /// </summary>
        public static readonly string ColName_BloodTranVol = "PAT_VISIT.BLOOD_TRAN_VOL";

        /// <summary>
        /// ��Ѫ����
        /// </summary>
        [AttributeColumn(ColumnName = "BLOOD_TRAN_VOL",ColType = "NUMBER_5_0",CanNullOrEmpty = true)]
        public Decimal BloodTranVol
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ѫ��Ӧ����
        /// </summary>
        public static readonly string ColName_BloodTranReactTimes = "PAT_VISIT.BLOOD_TRAN_REACT_TIMES";

        /// <summary>
        /// ��Ѫ��Ӧ����
        /// </summary>
        [AttributeColumn(ColumnName = "BLOOD_TRAN_REACT_TIMES",ColType = "NUMBER_2_0",CanNullOrEmpty = true)]
        public Decimal BloodTranReactTimes
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �����촯����
        /// </summary>
        public static readonly string ColName_DecubitalUlcerTimes = "PAT_VISIT.DECUBITAL_ULCER_TIMES";

        /// <summary>
        /// �����촯����
        /// </summary>
        [AttributeColumn(ColumnName = "DECUBITAL_ULCER_TIMES",ColType = "NUMBER_2_0",CanNullOrEmpty = true)]
        public Decimal DecubitalUlcerTimes
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ����ҩ��
        /// </summary>
        public static readonly string ColName_AlergyDrugs = "PAT_VISIT.ALERGY_DRUGS";

        /// <summary>
        /// ����ҩ��
        /// </summary>
        [AttributeColumn(ColumnName = "ALERGY_DRUGS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 80)]
        public string AlergyDrugs
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ������Ӧҩ��
        /// </summary>
        public static readonly string ColName_AdverseReactionDrugs = "PAT_VISIT.ADVERSE_REACTION_DRUGS";

        /// <summary>
        /// ������Ӧҩ��
        /// </summary>
        [AttributeColumn(ColumnName = "ADVERSE_REACTION_DRUGS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 80)]
        public string AdverseReactionDrugs
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ������ֵ
        /// </summary>
        public static readonly string ColName_MrValue = "PAT_VISIT.MR_VALUE";

        /// <summary>
        /// ������ֵ
        /// </summary>
        [AttributeColumn(ColumnName = "MR_VALUE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 4)]
        public string MrValue
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��������
        /// </summary>
        public static readonly string ColName_MrQuality = "PAT_VISIT.MR_QUALITY";

        /// <summary>
        /// ��������
        /// </summary>
        [AttributeColumn(ColumnName = "MR_QUALITY",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 2)]
        public string MrQuality
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �����־
        /// </summary>
        public static readonly string ColName_FollowIndicator = "PAT_VISIT.FOLLOW_INDICATOR";

        /// <summary>
        /// �����־
        /// </summary>
        [AttributeColumn(ColumnName = "FOLLOW_INDICATOR",ColType = "NUMBER_1_0",CanNullOrEmpty = true)]
        public Decimal FollowIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��������
        /// </summary>
        public static readonly string ColName_FollowInterval = "PAT_VISIT.FOLLOW_INTERVAL";

        /// <summary>
        /// ��������
        /// </summary>
        [AttributeColumn(ColumnName = "FOLLOW_INTERVAL",ColType = "NUMBER_2_0",CanNullOrEmpty = true)]
        public Decimal FollowInterval
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �������޵�λ
        /// </summary>
        public static readonly string ColName_FollowIntervalUnits = "PAT_VISIT.FOLLOW_INTERVAL_UNITS";

        /// <summary>
        /// �������޵�λ
        /// </summary>
        [AttributeColumn(ColumnName = "FOLLOW_INTERVAL_UNITS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 2)]
        public string FollowIntervalUnits
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ������
        /// </summary>
        public static readonly string ColName_Director = "PAT_VISIT.DIRECTOR";

        /// <summary>
        /// ������
        /// </summary>
        [AttributeColumn(ColumnName = "DIRECTOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 20)]
        public string Director
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ����ҽʦ
        /// </summary>
        public static readonly string ColName_AttendingDoctor = "PAT_VISIT.ATTENDING_DOCTOR";

        /// <summary>
        /// ����ҽʦ
        /// </summary>
        [AttributeColumn(ColumnName = "ATTENDING_DOCTOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 20)]
        public string AttendingDoctor
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ����ҽʦ
        /// </summary>
        public static readonly string ColName_DoctorInCharge = "PAT_VISIT.DOCTOR_IN_CHARGE";

        /// <summary>
        /// ����ҽʦ
        /// </summary>
        [AttributeColumn(ColumnName = "DOCTOR_IN_CHARGE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 20)]
        public string DoctorInCharge
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ժ��ʽ
        /// </summary>
        public static readonly string ColName_DischargeDisposition = "PAT_VISIT.DISCHARGE_DISPOSITION";

        /// <summary>
        /// ��Ժ��ʽ
        /// </summary>
        [AttributeColumn(ColumnName = "DISCHARGE_DISPOSITION",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string DischargeDisposition
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �ܷ���
        /// </summary>
        public static readonly string ColName_TotalCosts = "PAT_VISIT.TOTAL_COSTS";

        /// <summary>
        /// �ܷ���
        /// </summary>
        [AttributeColumn(ColumnName = "TOTAL_COSTS",ColType = "NUMBER_12_4",CanNullOrEmpty = true)]
        public Decimal TotalCosts
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ʵ������
        /// </summary>
        public static readonly string ColName_TotalPayments = "PAT_VISIT.TOTAL_PAYMENTS";

        /// <summary>
        /// ʵ������
        /// </summary>
        [AttributeColumn(ColumnName = "TOTAL_PAYMENTS",ColType = "NUMBER_12_4",CanNullOrEmpty = true)]
        public Decimal TotalPayments
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ŀ����
        /// </summary>
        public static readonly string ColName_CatalogDate = "PAT_VISIT.CATALOG_DATE";

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        [AttributeColumn(ColumnName = "CATALOG_DATE",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime CatalogDate
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ŀ��
        /// </summary>
        public static readonly string ColName_Cataloger = "PAT_VISIT.CATALOGER";

        /// <summary>
        /// ��Ŀ��
        /// </summary>
        [AttributeColumn(ColumnName = "CATALOGER",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 20)]
        public string Cataloger
        {
          get;
          set;
        }

        /// <summary>
        /// ����: HbsAg
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
        /// ����: HCV-Ab
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
        /// ����: HIV_AB
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
        /// ����: ����ҽʦ
        /// </summary>
        public static readonly string ColName_ChiefDoctor = "PAT_VISIT.CHIEF_DOCTOR";

        /// <summary>
        /// ����ҽʦ
        /// </summary>
        [AttributeColumn(ColumnName = "CHIEF_DOCTOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string ChiefDoctor
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ����ҽʦ
        /// </summary>
        public static readonly string ColName_AdvancedStudiesDoctor = "PAT_VISIT.ADVANCED_STUDIES_DOCTOR";

        /// <summary>
        /// ����ҽʦ
        /// </summary>
        [AttributeColumn(ColumnName = "ADVANCED_STUDIES_DOCTOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string AdvancedStudiesDoctor
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �о���ʵϰҽʦ
        /// </summary>
        public static readonly string ColName_PracticeDoctorOfGraduate = "PAT_VISIT.PRACTICE_DOCTOR_OF_GRADUATE";

        /// <summary>
        /// �о���ʵϰҽʦ
        /// </summary>
        [AttributeColumn(ColumnName = "PRACTICE_DOCTOR_OF_GRADUATE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string PracticeDoctorOfGraduate
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ʵϰҽʦ
        /// </summary>
        public static readonly string ColName_PracticeDoctor = "PAT_VISIT.PRACTICE_DOCTOR";

        /// <summary>
        /// ʵϰҽʦ
        /// </summary>
        [AttributeColumn(ColumnName = "PRACTICE_DOCTOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string PracticeDoctor
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �ʿ�ҽʦ
        /// </summary>
        public static readonly string ColName_DoctorOfControlQuality = "PAT_VISIT.DOCTOR_OF_CONTROL_QUALITY";

        /// <summary>
        /// �ʿ�ҽʦ
        /// </summary>
        [AttributeColumn(ColumnName = "DOCTOR_OF_CONTROL_QUALITY",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string DoctorOfControlQuality
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �ʿػ�ʿ
        /// </summary>
        public static readonly string ColName_NurseOfControlQuality = "PAT_VISIT.NURSE_OF_CONTROL_QUALITY";

        /// <summary>
        /// �ʿػ�ʿ
        /// </summary>
        [AttributeColumn(ColumnName = "NURSE_OF_CONTROL_QUALITY",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string NurseOfControlQuality
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �ʿ�����
        /// </summary>
        public static readonly string ColName_DateOfControlQuality = "PAT_VISIT.DATE_OF_CONTROL_QUALITY";

        /// <summary>
        /// �ʿ�����
        /// </summary>
        [AttributeColumn(ColumnName = "DATE_OF_CONTROL_QUALITY",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime DateOfControlQuality
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �Ƿ�Ϊ��Ժ��һ��
        /// </summary>
        public static readonly string ColName_FirstCaseIndicator = "PAT_VISIT.FIRST_CASE_INDICATOR";

        /// <summary>
        /// �Ƿ�Ϊ��Ժ��һ��
        /// </summary>
        [AttributeColumn(ColumnName = "FIRST_CASE_INDICATOR",ColType = "NUMBER_1_0",CanNullOrEmpty = true)]
        public Decimal FirstCaseIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ������������
        /// </summary>
        public static readonly string ColName_ThirdLevelNursDays = "PAT_VISIT.THIRD_LEVEL_NURS_DAYS";

        /// <summary>
        /// ������������
        /// </summary>
        [AttributeColumn(ColumnName = "THIRD_LEVEL_NURS_DAYS",ColType = "NUMBER_4_0",CanNullOrEmpty = true)]
        public Decimal ThirdLevelNursDays
        {
          get;
          set;
        }

        /// <summary>
        /// ����: X�ߺ�
        /// </summary>
        public static readonly string ColName_XExamNo = "PAT_VISIT.X_EXAM_NO";

        /// <summary>
        /// X�ߺ�
        /// </summary>
        [AttributeColumn(ColumnName = "X_EXAM_NO",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 10)]
        public string XExamNo
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ժǰ����Ժ����
        /// </summary>
        public static readonly string ColName_TreatedInOthersIndicator = "PAT_VISIT.TREATED_IN_OTHERS_INDICATOR";

        /// <summary>
        /// ��Ժǰ����Ժ����
        /// </summary>
        [AttributeColumn(ColumnName = "TREATED_IN_OTHERS_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string TreatedInOthersIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �������
        /// </summary>
        public static readonly string ColName_TreatMethod = "PAT_VISIT.TREAT_METHOD";

        /// <summary>
        /// �������
        /// </summary>
        [AttributeColumn(ColumnName = "TREAT_METHOD",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string TreatMethod
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ������ҩ�Ƽ�
        /// </summary>
        public static readonly string ColName_HospMadeMedicineIndicator = "PAT_VISIT.HOSP_MADE_MEDICINE_INDICATOR";

        /// <summary>
        /// ������ҩ�Ƽ�
        /// </summary>
        [AttributeColumn(ColumnName = "HOSP_MADE_MEDICINE_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string HospMadeMedicineIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �����
        /// </summary>
        public static readonly string ColName_PathologyNo = "PAT_VISIT.PATHOLOGY_NO";

        /// <summary>
        /// �����
        /// </summary>
        [AttributeColumn(ColumnName = "PATHOLOGY_NO",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 10)]
        public string PathologyNo
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �ϼ�ҽ��ָ������
        /// </summary>
        public static readonly string ColName_UpperDoctorGuideEffect = "PAT_VISIT.UPPER_DOCTOR_GUIDE_EFFECT";

        /// <summary>
        /// �ϼ�ҽ��ָ������
        /// </summary>
        [AttributeColumn(ColumnName = "UPPER_DOCTOR_GUIDE_EFFECT",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string UpperDoctorGuideEffect
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ���ȷ���
        /// </summary>
        public static readonly string ColName_EmerTreatMethod = "PAT_VISIT.EMER_TREAT_METHOD";

        /// <summary>
        /// ���ȷ���
        /// </summary>
        [AttributeColumn(ColumnName = "EMER_TREAT_METHOD",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string EmerTreatMethod
        {
          get;
          set;
        }

        /// <summary>
        /// ����: סԺ�ڼ��Ƿ���ּ�֢
        /// </summary>
        public static readonly string ColName_IctusIndicator = "PAT_VISIT.ICTUS_INDICATOR";

        /// <summary>
        /// סԺ�ڼ��Ƿ���ּ�֢
        /// </summary>
        [AttributeColumn(ColumnName = "ICTUS_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string IctusIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// ����: סԺ�ڼ��Ƿ����Σ��
        /// </summary>
        public static readonly string ColName_DifficultyIndicator = "PAT_VISIT.DIFFICULTY_INDICATOR";

        /// <summary>
        /// סԺ�ڼ��Ƿ����Σ��
        /// </summary>
        [AttributeColumn(ColumnName = "DIFFICULTY_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string DifficultyIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ������������
        /// </summary>
        public static readonly string ColName_FromOtherPlaceIndicator = "PAT_VISIT.FROM_OTHER_PLACE_INDICATOR";

        /// <summary>
        /// ������������
        /// </summary>
        [AttributeColumn(ColumnName = "FROM_OTHER_PLACE_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string FromOtherPlaceIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ժ�������
        /// </summary>
        public static readonly string ColName_SuspicionIndicator = "PAT_VISIT.SUSPICION_INDICATOR";

        /// <summary>
        /// ��Ժ�������
        /// </summary>
        [AttributeColumn(ColumnName = "SUSPICION_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string SuspicionIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��ҽ��ɫ����
        /// </summary>
        public static readonly string ColName_ChineseMedicineIndicator = "PAT_VISIT.CHINESE_MEDICINE_INDICATOR";

        /// <summary>
        /// ��ҽ��ɫ����
        /// </summary>
        [AttributeColumn(ColumnName = "CHINESE_MEDICINE_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string ChineseMedicineIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��������
        /// </summary>
        public static readonly string ColName_OperationScale = "PAT_VISIT.OPERATION_SCALE";

        /// <summary>
        /// ��������
        /// </summary>
        [AttributeColumn(ColumnName = "OPERATION_SCALE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string OperationScale
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��֤׼ȷ��
        /// </summary>
        public static readonly string ColName_DiagnosisCorrectness = "PAT_VISIT.DIAGNOSIS_CORRECTNESS";

        /// <summary>
        /// ��֤׼ȷ��
        /// </summary>
        [AttributeColumn(ColumnName = "DIAGNOSIS_CORRECTNESS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string DiagnosisCorrectness
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �η�׼ȷ��
        /// </summary>
        public static readonly string ColName_TreatMethodCorrectness = "PAT_VISIT.TREAT_METHOD_CORRECTNESS";

        /// <summary>
        /// �η�׼ȷ��
        /// </summary>
        [AttributeColumn(ColumnName = "TREAT_METHOD_CORRECTNESS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string TreatMethodCorrectness
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��ҩ׼ȷ��
        /// </summary>
        public static readonly string ColName_PrescriptionCorrectness = "PAT_VISIT.PRESCRIPTION_CORRECTNESS";

        /// <summary>
        /// ��ҩ׼ȷ��
        /// </summary>
        [AttributeColumn(ColumnName = "PRESCRIPTION_CORRECTNESS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string PrescriptionCorrectness
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ������д��ȫ
        /// </summary>
        public static readonly string ColName_MrCompleteIndicator = "PAT_VISIT.MR_COMPLETE_INDICATOR";

        /// <summary>
        /// ������д��ȫ
        /// </summary>
        [AttributeColumn(ColumnName = "MR_COMPLETE_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string MrCompleteIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ҽѧ����Ӧ����ȷ
        /// </summary>
        public static readonly string ColName_MedicalTermCorrectness = "PAT_VISIT.MEDICAL_TERM_CORRECTNESS";

        /// <summary>
        /// ҽѧ����Ӧ����ȷ
        /// </summary>
        [AttributeColumn(ColumnName = "MEDICAL_TERM_CORRECTNESS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string MedicalTermCorrectness
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��������ж�
        /// </summary>
        public static readonly string ColName_TreatMethodJudgement = "PAT_VISIT.TREAT_METHOD_JUDGEMENT";

        /// <summary>
        /// ��������ж�
        /// </summary>
        [AttributeColumn(ColumnName = "TREAT_METHOD_JUDGEMENT",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string TreatMethodJudgement
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ���λ�ʿ
        /// </summary>
        public static readonly string ColName_DutyNurse = "PAT_VISIT.DUTY_NURSE";

        /// <summary>
        /// ���λ�ʿ
        /// </summary>
        [AttributeColumn(ColumnName = "DUTY_NURSE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 8)]
        public string DutyNurse
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ����ԭ��
        /// </summary>
        public static readonly string ColName_DeathReason = "PAT_VISIT.DEATH_REASON";

        /// <summary>
        /// ����ԭ��
        /// </summary>
        [AttributeColumn(ColumnName = "DEATH_REASON",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 40)]
        public string DeathReason
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ����ʱ��
        /// </summary>
        public static readonly string ColName_DeathDateTime = "PAT_VISIT.DEATH_DATE_TIME";

        /// <summary>
        /// ����ʱ��
        /// </summary>
        [AttributeColumn(ColumnName = "DEATH_DATE_TIME",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime DeathDateTime
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ���в���
        /// </summary>
        public static readonly string ColName_ScienceResearchIndicator = "PAT_VISIT.SCIENCE_RESEARCH_INDICATOR";

        /// <summary>
        /// ���в���
        /// </summary>
        [AttributeColumn(ColumnName = "SCIENCE_RESEARCH_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string ScienceResearchIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ����Ϊ��Ժ��һ��
        /// </summary>
        public static readonly string ColName_FirstOperationIndicator = "PAT_VISIT.FIRST_OPERATION_INDICATOR";

        /// <summary>
        /// ����Ϊ��Ժ��һ��
        /// </summary>
        [AttributeColumn(ColumnName = "FIRST_OPERATION_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string FirstOperationIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ����Ϊ��Ժ��һ��
        /// </summary>
        public static readonly string ColName_FirstTreatmentIndicator = "PAT_VISIT.FIRST_TREATMENT_INDICATOR";

        /// <summary>
        /// ����Ϊ��Ժ��һ��
        /// </summary>
        [AttributeColumn(ColumnName = "FIRST_TREATMENT_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string FirstTreatmentIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ���Ϊ��Ժ��һ��
        /// </summary>
        public static readonly string ColName_FirstExaminationIndicator = "PAT_VISIT.FIRST_EXAMINATION_INDICATOR";

        /// <summary>
        /// ���Ϊ��Ժ��һ��
        /// </summary>
        [AttributeColumn(ColumnName = "FIRST_EXAMINATION_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string FirstExaminationIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ���Ϊ��Ժ��һ��
        /// </summary>
        public static readonly string ColName_FirstDiagnosisIndicator = "PAT_VISIT.FIRST_DIAGNOSIS_INDICATOR";

        /// <summary>
        /// ���Ϊ��Ժ��һ��
        /// </summary>
        [AttributeColumn(ColumnName = "FIRST_DIAGNOSIS_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string FirstDiagnosisIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// ����: סԺ�ڼ��Ƿ����Σ��
        /// </summary>
        public static readonly string ColName_SeriousIndicator = "PAT_VISIT.SERIOUS_INDICATOR";

        /// <summary>
        /// סԺ�ڼ��Ƿ����Σ��
        /// </summary>
        [AttributeColumn(ColumnName = "SERIOUS_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string SeriousIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ժ����
        /// </summary>
        public static readonly string ColName_AdtRoomNo = "PAT_VISIT.ADT_ROOM_NO";

        /// <summary>
        /// ��Ժ����
        /// </summary>
        [AttributeColumn(ColumnName = "ADT_ROOM_NO",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 4)]
        public string AdtRoomNo
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ժ����
        /// </summary>
        public static readonly string ColName_DdtRoomNo = "PAT_VISIT.DDT_ROOM_NO";

        /// <summary>
        /// ��Ժ����
        /// </summary>
        [AttributeColumn(ColumnName = "DDT_ROOM_NO",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 4)]
        public string DdtRoomNo
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ⱦ���
        /// </summary>
        public static readonly string ColName_InfectIndicator = "PAT_VISIT.INFECT_INDICATOR";

        /// <summary>
        /// ��Ⱦ���
        /// </summary>
        [AttributeColumn(ColumnName = "INFECT_INDICATOR",ColType = "NUMBER_1_0",CanNullOrEmpty = true)]
        public Decimal InfectIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �����ȼ�
        /// </summary>
        public static readonly string ColName_HealthLevel = "PAT_VISIT.HEALTH_LEVEL";

        /// <summary>
        /// �����ȼ�
        /// </summary>
        [AttributeColumn(ColumnName = "HEALTH_LEVEL",ColType = "CHAR",CanNullOrEmpty = true,MaxLength= 2)]
        public string HealthLevel
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��ϴ�©
        /// </summary>
        public static readonly string ColName_MrInfectReport = "PAT_VISIT.MR_INFECT_REPORT";

        /// <summary>
        /// ��ϴ�©
        /// </summary>
        [AttributeColumn(ColumnName = "MR_INFECT_REPORT",ColType = "CHAR",CanNullOrEmpty = true,MaxLength= 4)]
        public string MrInfectReport
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �Ƿ�������
        /// </summary>
        public static readonly string ColName_Newborn = "PAT_VISIT.NEWBORN";

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        [AttributeColumn(ColumnName = "NEWBORN",ColType = "CHAR",CanNullOrEmpty = true,MaxLength= 1)]
        public string Newborn
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ���յ���
        /// </summary>
        public static readonly string ColName_InsuranceAera = "PAT_VISIT.INSURANCE_AERA";

        /// <summary>
        /// ���յ���
        /// </summary>
        [AttributeColumn(ColumnName = "INSURANCE_AERA",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 60)]
        public string InsuranceAera
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ����
        /// </summary>
        public static readonly string ColName_BodyWeight = "PAT_VISIT.BODY_WEIGHT";

        /// <summary>
        /// ����
        /// </summary>
        [AttributeColumn(ColumnName = "BODY_WEIGHT",ColType = "NUMBER_5_2",CanNullOrEmpty = true)]
        public Decimal BodyWeight
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ���
        /// </summary>
        public static readonly string ColName_BodyHeight = "PAT_VISIT.BODY_HEIGHT";

        /// <summary>
        /// ���
        /// </summary>
        [AttributeColumn(ColumnName = "BODY_HEIGHT",ColType = "NUMBER_4_1",CanNullOrEmpty = true)]
        public Decimal BodyHeight
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ������λ��������
        /// </summary>
        public static readonly string ColName_BusinessZipCode = "PAT_VISIT.BUSINESS_ZIP_CODE";

        /// <summary>
        /// ������λ��������
        /// </summary>
        [AttributeColumn(ColumnName = "BUSINESS_ZIP_CODE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 6)]
        public string BusinessZipCode
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Һ����
        /// </summary>
        public static readonly string ColName_InfusionTranTimes = "PAT_VISIT.INFUSION_TRAN_TIMES";

        /// <summary>
        /// ��Һ����
        /// </summary>
        [AttributeColumn(ColumnName = "INFUSION_TRAN_TIMES",ColType = "NUMBER_22_0",CanNullOrEmpty = true)]
        public Decimal InfusionTranTimes
        {
          get;
          set;
        }

        /// <summary>
        /// ����: 
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
        /// ����: ��ҳ�鵵����
        /// </summary>
        public static readonly string ColName_DocumDate = "PAT_VISIT.DOCUM_DATE";

        /// <summary>
        /// ��ҳ�鵵����
        /// </summary>
        [AttributeColumn(ColumnName = "DOCUM_DATE",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime DocumDate
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �鵵����
        /// </summary>
        public static readonly string ColName_DocumDays = "PAT_VISIT.DOCUM_DAYS";

        /// <summary>
        /// �鵵����
        /// </summary>
        [AttributeColumn(ColumnName = "DOCUM_DAYS",ColType = "NUMBER_3_0",CanNullOrEmpty = true)]
        public Decimal DocumDays
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��ҳ�鵵��Ա
        /// </summary>
        public static readonly string ColName_DocumPerson = "PAT_VISIT.DOCUM_PERSON";

        /// <summary>
        /// ��ҳ�鵵��Ա
        /// </summary>
        [AttributeColumn(ColumnName = "DOCUM_PERSON",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string DocumPerson
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ⱦ����־(1�ѱ� 2δ�� 3��)
        /// </summary>
        public static readonly string ColName_ZymosisIndicator = "PAT_VISIT.ZYMOSIS_INDICATOR";

        /// <summary>
        /// ��Ⱦ����־(1�ѱ� 2δ�� 3��)
        /// </summary>
        [AttributeColumn(ColumnName = "ZYMOSIS_INDICATOR",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string ZymosisIndicator
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �ϱ�����
        /// </summary>
        public static readonly string ColName_ZymosisDate = "PAT_VISIT.ZYMOSIS_DATE";

        /// <summary>
        /// �ϱ�����
        /// </summary>
        [AttributeColumn(ColumnName = "ZYMOSIS_DATE",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime ZymosisDate
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��������ʱ
        /// </summary>
        public static readonly string ColName_BreathMachTimes = "PAT_VISIT.BREATH_MACH_TIMES";

        /// <summary>
        /// ��������ʱ
        /// </summary>
        [AttributeColumn(ColumnName = "BREATH_MACH_TIMES",ColType = "NUMBER_3_0",CanNullOrEmpty = true)]
        public Decimal BreathMachTimes
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ����ʱ��(��ԺǰСʱ)
        /// </summary>
        public static readonly string ColName_ComaTimesB1 = "PAT_VISIT.COMA_TIMES_B1";

        /// <summary>
        /// ����ʱ��(��ԺǰСʱ)
        /// </summary>
        [AttributeColumn(ColumnName = "COMA_TIMES_B1",ColType = "NUMBER_3_0",CanNullOrEmpty = true)]
        public Decimal ComaTimesB1
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ժǰ����
        /// </summary>
        public static readonly string ColName_ComaTimesB2 = "PAT_VISIT.COMA_TIMES_B2";

        /// <summary>
        /// ��Ժǰ����
        /// </summary>
        [AttributeColumn(ColumnName = "COMA_TIMES_B2",ColType = "NUMBER_3_0",CanNullOrEmpty = true)]
        public Decimal ComaTimesB2
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ժ��Сʱ
        /// </summary>
        public static readonly string ColName_ComaTimesA1 = "PAT_VISIT.COMA_TIMES_A1";

        /// <summary>
        /// ��Ժ��Сʱ
        /// </summary>
        [AttributeColumn(ColumnName = "COMA_TIMES_A1",ColType = "NUMBER_3_0",CanNullOrEmpty = true)]
        public Decimal ComaTimesA1
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ժ�����
        /// </summary>
        public static readonly string ColName_ComaTimesA2 = "PAT_VISIT.COMA_TIMES_A2";

        /// <summary>
        /// ��Ժ�����
        /// </summary>
        [AttributeColumn(ColumnName = "COMA_TIMES_A2",ColType = "NUMBER_3_0",CanNullOrEmpty = true)]
        public Decimal ComaTimesA2
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ת��ҽԺ����
        /// </summary>
        public static readonly string ColName_TransHospital = "PAT_VISIT.TRANS_HOSPITAL";

        /// <summary>
        /// ת��ҽԺ����
        /// </summary>
        [AttributeColumn(ColumnName = "TRANS_HOSPITAL",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 100)]
        public string TransHospital
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ת��������
        /// </summary>
        public static readonly string ColName_TransCommunity = "PAT_VISIT.TRANS_COMMUNITY";

        /// <summary>
        /// ת��������
        /// </summary>
        [AttributeColumn(ColumnName = "TRANS_COMMUNITY",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 100)]
        public string TransCommunity
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ������λ�绰
        /// </summary>
        public static readonly string ColName_PhoneNumberBusiness = "PAT_VISIT.PHONE_NUMBER_BUSINESS";

        /// <summary>
        /// ������λ�绰
        /// </summary>
        [AttributeColumn(ColumnName = "PHONE_NUMBER_BUSINESS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 16)]
        public string PhoneNumberBusiness
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ������
        /// </summary>
        public static readonly string ColName_MrBinder = "PAT_VISIT.MR_BINDER";

        /// <summary>
        /// ������
        /// </summary>
        [AttributeColumn(ColumnName = "MR_BINDER",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 30)]
        public string MrBinder
        {
          get;
          set;
        }

        /// <summary>
        /// ����: 
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
        /// ����: �Һž�������
        /// </summary>
        public static readonly string ColName_VisitDate = "PAT_VISIT.VISIT_DATE";

        /// <summary>
        /// �Һž�������
        /// </summary>
        [AttributeColumn(ColumnName = "VISIT_DATE",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime VisitDate
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �Һž������
        /// </summary>
        public static readonly string ColName_VisitNo = "PAT_VISIT.VISIT_NO";

        /// <summary>
        /// �Һž������
        /// </summary>
        [AttributeColumn(ColumnName = "VISIT_NO",ColType = "NUMBER_5_0",CanNullOrEmpty = true)]
        public Decimal VisitNo
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ժʱ�Ĵ�λ��
        /// </summary>
        public static readonly string ColName_DischargeBedNo = "PAT_VISIT.DISCHARGE_BED_NO";

        /// <summary>
        /// ��Ժʱ�Ĵ�λ��
        /// </summary>
        [AttributeColumn(ColumnName = "DISCHARGE_BED_NO",ColType = "NUMBER_8_0",CanNullOrEmpty = true)]
        public Decimal DischargeBedNo
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �������ڵص绰
        /// </summary>
        public static readonly string ColName_PhoneNumberHome = "PAT_VISIT.PHONE_NUMBER_HOME";

        /// <summary>
        /// �������ڵص绰
        /// </summary>
        [AttributeColumn(ColumnName = "PHONE_NUMBER_HOME",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 16)]
        public string PhoneNumberHome
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ������Դ��ַ1--���б�����2--����������3--��ʡ���С�4--��ʡ��
        /// </summary>
        public static readonly string ColName_PatientArea = "PAT_VISIT.PATIENT_AREA";

        /// <summary>
        /// ������Դ��ַ1--���б�����2--����������3--��ʡ���С�4--��ʡ��
        /// </summary>
        [AttributeColumn(ColumnName = "PATIENT_AREA",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 6)]
        public string PatientArea
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ժ����Ԫ
        /// </summary>
        public static readonly string ColName_DischargeWardCode = "PAT_VISIT.DISCHARGE_WARD_CODE";

        /// <summary>
        /// ��Ժ����Ԫ
        /// </summary>
        [AttributeColumn(ColumnName = "DISCHARGE_WARD_CODE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 8)]
        public string DischargeWardCode
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ŀ״̬
        /// </summary>
        public static readonly string ColName_CatalogStatus = "PAT_VISIT.CATALOG_STATUS";

        /// <summary>
        /// ��Ŀ״̬
        /// </summary>
        [AttributeColumn(ColumnName = "CATALOG_STATUS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string CatalogStatus
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ���һ����������
        /// </summary>
        public static readonly string ColName_LockedDate = "PAT_VISIT.LOCKED_DATE";

        /// <summary>
        /// ���һ����������
        /// </summary>
        [AttributeColumn(ColumnName = "LOCKED_DATE",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime LockedDate
        {
          get;
          set;
        }

        /// <summary>
        /// ����: 
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
        /// ����: ���ú�
        /// </summary>
        public static readonly string ColName_InpSerialNo = "PAT_VISIT.INP_SERIAL_NO";

        /// <summary>
        /// ���ú�
        /// </summary>
        [AttributeColumn(ColumnName = "INP_SERIAL_NO",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 20)]
        public string InpSerialNo
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ժʱ�Ĵ�λ��
        /// </summary>
        public static readonly string ColName_AdmissionBedNo = "PAT_VISIT.ADMISSION_BED_NO";

        /// <summary>
        /// ��Ժʱ�Ĵ�λ��
        /// </summary>
        [AttributeColumn(ColumnName = "ADMISSION_BED_NO",ColType = "NUMBER_8_0",CanNullOrEmpty = true)]
        public Decimal AdmissionBedNo
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ժȷ������
        /// </summary>
        public static readonly string ColName_DiagnoseDate = "PAT_VISIT.DIAGNOSE_DATE";

        /// <summary>
        /// ��Ժȷ������
        /// </summary>
        [AttributeColumn(ColumnName = "DIAGNOSE_DATE",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime DiagnoseDate
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ҽ�Ƹ��ʽ
        /// </summary>
        public static readonly string ColName_MedicalPayWay = "PAT_VISIT.MEDICAL_PAY_WAY";

        /// <summary>
        /// ҽ�Ƹ��ʽ
        /// </summary>
        [AttributeColumn(ColumnName = "MEDICAL_PAY_WAY",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string MedicalPayWay
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ������Ŀ������ͳ�Ʊ�־����ֵΪ1ʱ����Ŀ����ͳ��
        /// </summary>
        public static readonly string ColName_StatisticsFlag = "PAT_VISIT.STATISTICS_FLAG";

        /// <summary>
        /// ������Ŀ������ͳ�Ʊ�־����ֵΪ1ʱ����Ŀ����ͳ��
        /// </summary>
        [AttributeColumn(ColumnName = "STATISTICS_FLAG",ColType = "NUMBER_1_0",CanNullOrEmpty = true)]
        public Decimal StatisticsFlag
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ���ܵǼǱ�־
        /// </summary>
        public static readonly string ColName_DocumFlag = "PAT_VISIT.DOCUM_FLAG";

        /// <summary>
        /// ���ܵǼǱ�־
        /// </summary>
        [AttributeColumn(ColumnName = "DOCUM_FLAG",ColType = "NUMBER_22_0",CanNullOrEmpty = true)]
        public Decimal DocumFlag
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ͳ������Ժȷ������
        /// </summary>
        public static readonly string ColName_StatisticsDiagnoseDate = "PAT_VISIT.STATISTICS_DIAGNOSE_DATE";

        /// <summary>
        /// ͳ������Ժȷ������
        /// </summary>
        [AttributeColumn(ColumnName = "STATISTICS_DIAGNOSE_DATE",ColType = "DATE",CanNullOrEmpty = true)]
        public DateTime StatisticsDiagnoseDate
        {
          get;
          set;
        }

        /// <summary>
        /// ����: 
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
        /// ����: 
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
        /// ����: 
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
        /// ����: ��������������
        /// </summary>
        public static readonly string ColName_WeightBirth = "PAT_VISIT.WEIGHT_BIRTH";

        /// <summary>
        /// ��������������
        /// </summary>
        [AttributeColumn(ColumnName = "WEIGHT_BIRTH",ColType = "NUMBER_5_0",CanNullOrEmpty = true)]
        public Decimal WeightBirth
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ժǰ��������
        /// </summary>
        public static readonly string ColName_ComaTimesB0 = "PAT_VISIT.COMA_TIMES_B0";

        /// <summary>
        /// ��Ժǰ��������
        /// </summary>
        [AttributeColumn(ColumnName = "COMA_TIMES_B0",ColType = "NUMBER_3_0",CanNullOrEmpty = true)]
        public Decimal ComaTimesB0
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ժ���������
        /// </summary>
        public static readonly string ColName_ComaTimesA0 = "PAT_VISIT.COMA_TIMES_A0";

        /// <summary>
        /// ��Ժ���������
        /// </summary>
        [AttributeColumn(ColumnName = "COMA_TIMES_A0",ColType = "NUMBER_3_0",CanNullOrEmpty = true)]
        public Decimal ComaTimesA0
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��סַ���ƣ��ط��ã�
        /// </summary>
        public static readonly string ColName_PatientAreaAddress = "PAT_VISIT.PATIENT_AREA_ADDRESS";

        /// <summary>
        /// ��סַ���ƣ��ط��ã�
        /// </summary>
        [AttributeColumn(ColumnName = "PATIENT_AREA_ADDRESS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 200)]
        public string PatientAreaAddress
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��סַ����/�ֵ�����
        /// </summary>
        public static readonly string ColName_PatStreetAddress = "PAT_VISIT.PAT_STREET_ADDRESS";

        /// <summary>
        /// ��סַ����/�ֵ�����
        /// </summary>
        [AttributeColumn(ColumnName = "PAT_STREET_ADDRESS",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 10)]
        public string PatStreetAddress
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��סַ�绰
        /// </summary>
        public static readonly string ColName_PatPhone = "PAT_VISIT.PAT_PHONE";

        /// <summary>
        /// ��סַ�绰
        /// </summary>
        [AttributeColumn(ColumnName = "PAT_PHONE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 16)]
        public string PatPhone
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��סַ�ʱ�
        /// </summary>
        public static readonly string ColName_PatZip = "PAT_VISIT.PAT_ZIP";

        /// <summary>
        /// ��סַ�ʱ�
        /// </summary>
        [AttributeColumn(ColumnName = "PAT_ZIP",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 6)]
        public string PatZip
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �Ƿ���31��������Ժ�ƻ���1��2�У�
        /// </summary>
        public static readonly string ColName_Plan31Admission = "PAT_VISIT.PLAN_31_ADMISSION";

        /// <summary>
        /// �Ƿ���31��������Ժ�ƻ���1��2�У�
        /// </summary>
        [AttributeColumn(ColumnName = "PLAN_31_ADMISSION",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string Plan31Admission
        {
          get;
          set;
        }

        /// <summary>
        /// ����: 31��������ԺĿ��
        /// </summary>
        public static readonly string ColName_Reason31Admission = "PAT_VISIT.REASON_31_ADMISSION";

        /// <summary>
        /// 31��������ԺĿ��
        /// </summary>
        [AttributeColumn(ColumnName = "REASON_31_ADMISSION",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 100)]
        public string Reason31Admission
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��������TNM��Tֵ
        /// </summary>
        public static readonly string ColName_TumorT = "PAT_VISIT.TUMOR_T";

        /// <summary>
        /// ��������TNM��Tֵ
        /// </summary>
        [AttributeColumn(ColumnName = "TUMOR_T",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 10)]
        public string TumorT
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��������TNM��Nֵ
        /// </summary>
        public static readonly string ColName_TumorN = "PAT_VISIT.TUMOR_N";

        /// <summary>
        /// ��������TNM��Nֵ
        /// </summary>
        [AttributeColumn(ColumnName = "TUMOR_N",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 10)]
        public string TumorN
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��������TNM��Mֵ
        /// </summary>
        public static readonly string ColName_TumorM = "PAT_VISIT.TUMOR_M";

        /// <summary>
        /// ��������TNM��Mֵ
        /// </summary>
        [AttributeColumn(ColumnName = "TUMOR_M",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 10)]
        public string TumorM
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ����0-�����ڷ�(0 0�ڡ�1 ���ڡ�2 ���ڡ�3 ���ڡ�4 ���ڡ�9 ����)
        /// </summary>
        public static readonly string ColName_TumorStage = "PAT_VISIT.TUMOR_STAGE";

        /// <summary>
        /// ����0-�����ڷ�(0 0�ڡ�1 ���ڡ�2 ���ڡ�3 ���ڡ�4 ���ڡ�9 ����)
        /// </summary>
        [AttributeColumn(ColumnName = "TUMOR_STAGE",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 10)]
        public string TumorStage
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �ճ�����������������ADL����Ժ�÷�
        /// </summary>
        public static readonly string ColName_AdlAdm = "PAT_VISIT.ADL_ADM";

        /// <summary>
        /// �ճ�����������������ADL����Ժ�÷�
        /// </summary>
        [AttributeColumn(ColumnName = "ADL_ADM",ColType = "NUMBER_6_2",CanNullOrEmpty = true)]
        public Decimal AdlAdm
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �ճ�����������������ADL����Ժ�÷�
        /// </summary>
        public static readonly string ColName_AdlDis = "PAT_VISIT.ADL_DIS";

        /// <summary>
        /// �ճ�����������������ADL����Ժ�÷�
        /// </summary>
        [AttributeColumn(ColumnName = "ADL_DIS",ColType = "NUMBER_6_2",CanNullOrEmpty = true)]
        public Decimal AdlDis
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �������������߱���
        /// </summary>
        public static readonly string ColName_BasisOn = "PAT_VISIT.BASIS_ON";

        /// <summary>
        /// �������������߱���
        /// </summary>
        [AttributeColumn(ColumnName = "BASIS_ON",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 8)]
        public string BasisOn
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �������������߱���
        /// </summary>
        public static readonly string ColName_DiffId = "PAT_VISIT.DIFF_ID";

        /// <summary>
        /// �������������߱���
        /// </summary>
        [AttributeColumn(ColumnName = "DIFF_ID",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 8)]
        public string DiffId
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ֻ���������˲��ܽ���-ʼ�ڱ���������ҽԺ
        /// </summary>
        public static readonly string ColName_LockedUser = "PAT_VISIT.LOCKED_USER";

        /// <summary>
        /// ֻ���������˲��ܽ���-ʼ�ڱ���������ҽԺ
        /// </summary>
        [AttributeColumn(ColumnName = "LOCKED_USER",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 20)]
        public string LockedUser
        {
          get;
          set;
        }

        /// <summary>
        /// ����: סԺ��
        /// </summary>
        public static readonly string ColName_InpNo = "PAT_VISIT.INP_NO";

        /// <summary>
        /// סԺ��
        /// </summary>
        [AttributeColumn(ColumnName = "INP_NO",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 10)]
        public string InpNo
        {
          get;
          set;
        }

        /// <summary>
        /// ����: 
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
        /// ����: 
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
        /// ����: 
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
        /// ����: ��Ⱦ���ϱ�״̬
        /// </summary>
        public static readonly string ColName_InfectiousDiseaseReport = "PAT_VISIT.INFECTIOUS_DISEASE_REPORT";

        /// <summary>
        /// ��Ⱦ���ϱ�״̬
        /// </summary>
        [AttributeColumn(ColumnName = "INFECTIOUS_DISEASE_REPORT",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string InfectiousDiseaseReport
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ϸ��������ҩ��
        /// </summary>
        public static readonly string ColName_GermicultureDrugTest = "PAT_VISIT.GERMICULTURE_DRUG_TEST";

        /// <summary>
        /// ϸ��������ҩ��
        /// </summary>
        [AttributeColumn(ColumnName = "GERMICULTURE_DRUG_TEST",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string GermicultureDrugTest
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ����������ʹ�����
        /// </summary>
        public static readonly string ColName_AntibioticsUnion = "PAT_VISIT.ANTIBIOTICS_UNION";

        /// <summary>
        /// ����������ʹ�����
        /// </summary>
        [AttributeColumn(ColumnName = "ANTIBIOTICS_UNION",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string AntibioticsUnion
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ʹ�ÿ�����������
        /// </summary>
        public static readonly string ColName_AntibioticsCount = "PAT_VISIT.ANTIBIOTICS_COUNT";

        /// <summary>
        /// ʹ�ÿ�����������
        /// </summary>
        [AttributeColumn(ColumnName = "ANTIBIOTICS_COUNT",ColType = "NUMBER_2_0",CanNullOrEmpty = true)]
        public Decimal AntibioticsCount
        {
          get;
          set;
        }

        /// <summary>
        /// ����: �Ƿ�ʹ�ÿ�����
        /// </summary>
        public static readonly string ColName_AntibioticsUsed = "PAT_VISIT.ANTIBIOTICS_USED";

        /// <summary>
        /// �Ƿ�ʹ�ÿ�����
        /// </summary>
        [AttributeColumn(ColumnName = "ANTIBIOTICS_USED",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string AntibioticsUsed
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��Ѫ��Ӧ
        /// </summary>
        public static readonly string ColName_BloodTranFlag = "PAT_VISIT.BLOOD_TRAN_FLAG";

        /// <summary>
        /// ��Ѫ��Ӧ
        /// </summary>
        [AttributeColumn(ColumnName = "BLOOD_TRAN_FLAG",ColType = "VARCHAR2",CanNullOrEmpty = true,MaxLength= 1)]
        public string BloodTranFlag
        {
          get;
          set;
        }

        /// <summary>
        /// ����: ��ϸ���
        /// </summary>
        public static readonly string ColName_RegSubNo = "PAT_VISIT.REG_SUB_NO";

        /// <summary>
        /// ��ϸ���
        /// </summary>
        [AttributeColumn(ColumnName = "REG_SUB_NO",ColType = "NUMBER_2_0",CanNullOrEmpty = true)]
        public Decimal RegSubNo
        {
          get;
          set;
        }
    }
}

