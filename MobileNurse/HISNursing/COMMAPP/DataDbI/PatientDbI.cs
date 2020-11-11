using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public class PatientDbI
    {
        protected DbAccess _connection;

        public PatientDbI(DbAccess dbConnect)
        {
            _connection = dbConnect;
        }


        #region 获取病人信息
        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public virtual DataSet GetPatientInfo(string patientId)
        {
            string sql = "SELECT * FROM PAT_MASTER_INDEX WHERE PATIENT_ID = " + SQL.SqlConvert(patientId.Trim());

            return _connection.SelectData(sql);
        }


        /// <summary>
        /// 获取病人基本信息
        /// </summary>
        /// <param name="patientId">病人标识号</param>
        /// <param name="visitId">本次就诊序号</param>
        /// <returns></returns>
        public virtual DataSet GetInpPatientInfo_FromID(string patientId, string visitId)
        {
            string sql = string.Empty;

            sql = "SELECT ";
            sql +=      "PAT_MASTER_INDEX.NAME, ";                                      // 姓名
            sql +=      "PAT_MASTER_INDEX.SEX, ";                                       // 性别
            sql +=      "PAT_MASTER_INDEX.DATE_OF_BIRTH, ";                             // 出生日期
            sql +=      "PATS_IN_HOSPITAL.DIAGNOSIS, ";                                 // 主要诊断

            sql +=      "PATS_IN_HOSPITAL.VISIT_ID, ";                                  // 本次住院标识
            sql +=      "PAT_MASTER_INDEX.PATIENT_ID, ";                                // 病人标识号
            sql +=      "PAT_MASTER_INDEX.INP_NO, ";                                    // 住院号

            sql +=     "(SELECT NURSING_CLASS_DICT.NURSING_CLASS_NAME ";
            sql +=      "FROM NURSING_CLASS_DICT ";
            sql +=      "WHERE NURSING_CLASS_DICT.NURSING_CLASS_CODE = PATS_IN_HOSPITAL.NURSING_CLASS) ";
            sql +=      "NURSING_CLASS_NAME, ";                                         // 护理等级名称

            sql +=      "PATS_IN_HOSPITAL.DOCTOR_IN_CHARGE, ";                          // 主治医生

            sql +=      "PATS_IN_HOSPITAL.BED_NO, ";								    // 床号
            sql +=      "(SELECT BED_REC.BED_LABEL ";
            sql +=       "FROM BED_REC ";
            sql +=       "WHERE BED_REC.BED_NO = PATS_IN_HOSPITAL.BED_NO ";
            sql +=          "AND BED_REC.WARD_CODE = PATS_IN_HOSPITAL.WARD_CODE) ";
            sql +=       "BED_LABEL, ";                                                 // 床标号

            sql +=      "(SELECT PATIENT_STATUS_DICT.PATIENT_STATUS_NAME ";
            sql +=        "FROM PATIENT_STATUS_DICT ";
            sql +=        "WHERE PATIENT_STATUS_DICT.PATIENT_STATUS_NAME = PATS_IN_HOSPITAL.PATIENT_CONDITION) ";
            sql +=      "PATIENT_STATUS_NAME, ";                                        // 病情状态

            sql +=      "PATS_IN_HOSPITAL.ADMISSION_DATE_TIME, ";                       // 入院日期及时间

            sql +=      "(SELECT DEPT_NAME ";
            sql +=          "FROM DEPT_DICT ";
            sql +=          "WHERE DEPT_CODE = ";
            sql +=              "(CASE WHEN PATS_IN_HOSPITAL.LEND_INDICATOR = 1 ";
            sql +=               "THEN PATS_IN_HOSPITAL.DEPT_CODE_LEND ";
            sql +=               "ELSE PATS_IN_HOSPITAL.DEPT_CODE END)) ";
            sql +=      "DEPT_NAME ";                                                   // 所在科室

            sql += "FROM ";
            sql +=      "PATS_IN_HOSPITAL, ";                                           // 在院病人记录
            sql +=      "PAT_MASTER_INDEX ";                                            // 病人主索引

            sql += "WHERE ";
            sql +=      "PATS_IN_HOSPITAL.PATIENT_ID = " + SQL.SqlConvert(patientId);
            sql +=      " AND PATS_IN_HOSPITAL.VISIT_ID = " + SQL.SqlConvert(visitId);
            sql +=      " AND PATS_IN_HOSPITAL.PATIENT_ID = PAT_MASTER_INDEX.PATIENT_ID ";

            return _connection.SelectData(sql);
        }
        

        /// <summary>
        /// 获取病人基本信息
        /// </summary>
        /// <param name="bedLabel">床标号</param>
        /// <param name="wardCode">病区代码</param>
        /// <returns></returns>
        public virtual DataSet GetInpPatientInfo_FromBedLabel(string bedLabel, string wardCode)
        {
            string sql = string.Empty;

            sql = "SELECT ";
            sql+=    "PAT_MASTER_INDEX.NAME, ";                                      // 姓名
            sql+=    "PAT_MASTER_INDEX.SEX, ";                                       // 性别
            sql+=    "PAT_MASTER_INDEX.DATE_OF_BIRTH, ";                             // 出生日期
            sql+=    "PATS_IN_HOSPITAL.DIAGNOSIS, ";                                 // 主要诊断
                        
            sql+=    "PATS_IN_HOSPITAL.VISIT_ID, ";                                  // 本次住院标识
            sql+=    "PAT_MASTER_INDEX.PATIENT_ID, ";                                // 病人标识号
            sql+=    "PAT_MASTER_INDEX.INP_NO, ";                                    // 住院号
            sql+=    "PAT_MASTER_INDEX.CHARGE_TYPE, ";                               // 费别

            sql+=	 "BED_REC.BED_NO, ";								             // 床号
            sql+=    "BED_REC.BED_LABEL, ";                                          // 床标号
            sql+=    "BED_REC.ROOM_NO, ";                                            // 房间号

            sql+=    "PATS_IN_HOSPITAL.ADMISSION_DATE_TIME, ";                       // 入院日期及时间

            sql+=    "(SELECT DEPT_NAME ";
            sql+=    "FROM DEPT_DICT ";
            sql+=    "WHERE DEPT_CODE = ";
            sql+=        "(CASE WHEN PATS_IN_HOSPITAL.LEND_INDICATOR = 1 ";
            sql+=         "THEN PATS_IN_HOSPITAL.DEPT_CODE_LEND ";
            sql+=         "ELSE PATS_IN_HOSPITAL.DEPT_CODE END)) ";
            sql+=    "DEPT_NAME ";                                                   // 所在科室
            
            sql+= "FROM ";
            sql+=    "PATS_IN_HOSPITAL, ";                                           // 在院病人记录
            sql+=    "PAT_MASTER_INDEX, ";                                           // 病人主索引
            sql+=    "BED_REC ";                                                     // 床位记录

            sql+= "WHERE ";
            sql+=    "(BED_REC.BED_NO = PATS_IN_HOSPITAL.BED_NO ";
            sql+=    "AND BED_REC.WARD_CODE = PATS_IN_HOSPITAL.WARD_CODE) ";
            
            // 床位号
            sql+=    "AND BED_REC.BED_LABEL = " + SQL.SqlConvert(bedLabel);
            sql+=    "AND BED_REC.WARD_CODE = " + SQL.SqlConvert(wardCode);
            sql+=    "AND PATS_IN_HOSPITAL.PATIENT_ID = PAT_MASTER_INDEX.PATIENT_ID ";

            return _connection.SelectData(sql);
        }


        /// <summary>
        /// 获取整个病区的病人列表
        /// </summary>
        /// <param name="wardCode"></param>
        /// <returns></returns>
        public virtual DataSet GetWardPatientList(string wardCode)
        { 
            string sql = string.Empty;

            sql = "SELECT ";
            sql +=      "PAT_MASTER_INDEX.NAME, ";                                      // 姓名
            sql +=      "PAT_MASTER_INDEX.SEX, ";                                       // 性别
            sql +=      "PAT_MASTER_INDEX.DATE_OF_BIRTH, ";                             // 出生日期
            sql +=      "PATS_IN_HOSPITAL.DIAGNOSIS, ";                                 // 主要诊断

            sql +=      "PATS_IN_HOSPITAL.VISIT_ID, ";                                  // 本次住院标识
            sql +=      "PAT_MASTER_INDEX.PATIENT_ID, ";                                // 病人标识号
            sql +=      "PAT_MASTER_INDEX.INP_NO, ";                                    // 住院号

            sql +=     "(SELECT NURSING_CLASS_DICT.NURSING_CLASS_NAME ";
            sql +=      "FROM NURSING_CLASS_DICT ";
            sql +=      "WHERE NURSING_CLASS_DICT.NURSING_CLASS_CODE = PATS_IN_HOSPITAL.NURSING_CLASS) ";
            sql +=      "NURSING_CLASS_NAME, ";                                         // 护理等级名称

            sql +=      "PATS_IN_HOSPITAL.DOCTOR_IN_CHARGE, ";                          // 主治医生

            sql +=      "PATS_IN_HOSPITAL.BED_NO, ";								    // 床号
            sql +=      "(SELECT BED_REC.BED_LABEL ";
            sql +=       "FROM BED_REC ";
            sql +=       "WHERE BED_REC.BED_NO = PATS_IN_HOSPITAL.BED_NO ";
            sql +=          "AND BED_REC.WARD_CODE = PATS_IN_HOSPITAL.WARD_CODE) ";
            sql +=       "BED_LABEL, ";                                                 // 床标号

            sql +=      "(SELECT PATIENT_STATUS_DICT.PATIENT_STATUS_NAME ";
            sql +=        "FROM PATIENT_STATUS_DICT ";
            sql +=        "WHERE PATIENT_STATUS_DICT.PATIENT_STATUS_NAME = PATS_IN_HOSPITAL.PATIENT_CONDITION) ";
            sql +=      "PATIENT_STATUS_NAME, ";                                        // 病情状态

            sql +=      "PATS_IN_HOSPITAL.ADMISSION_DATE_TIME, ";                       // 入院日期及时间

            sql +=      "(SELECT DEPT_NAME ";
            sql +=          "FROM DEPT_DICT ";
            sql +=          "WHERE DEPT_CODE = ";
            sql +=              "(CASE WHEN PATS_IN_HOSPITAL.LEND_INDICATOR = 1 ";
            sql +=               "THEN PATS_IN_HOSPITAL.DEPT_CODE_LEND ";
            sql +=               "ELSE PATS_IN_HOSPITAL.DEPT_CODE END)) ";
            sql +=      "DEPT_NAME ";                                                   // 所在科室

            sql += "FROM ";
            sql +=      "PATS_IN_HOSPITAL, ";                                           // 在院病人记录
            sql +=      "PAT_MASTER_INDEX ";                                            // 病人主索引

            sql += "WHERE ";
            sql +=      "PATS_IN_HOSPITAL.WARD_CODE = " + SQL.SqlConvert(wardCode);
            sql +=      " AND PATS_IN_HOSPITAL.PATIENT_ID = PAT_MASTER_INDEX.PATIENT_ID ";
            
            sql += "ORDER BY ";
            sql +=      "PATS_IN_HOSPITAL.BED_NO ";

            return _connection.SelectData(sql);
        }
        #endregion


        #region 获取门诊信息
        /// <summary>
        /// 获取门诊病人处方列表
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public DataSet GetOutpPrescList(string patientId)
        { 
            StringBuilder sb = new StringBuilder(); 
            sb.Append("SELECT DISTINCT "                                                         );
            sb.Append(    "OUTP_PRESC.SERIAL_NO, "                                               ); // 主键
            sb.Append(    "OUTP_PRESC.PRESC_NO, "                                                ); // 处方序号
            sb.Append(    "OUTP_PRESC.VISIT_DATE, "                                              ); // 就诊日期
            sb.Append(    "OUTP_PRESC.VISIT_NO, "                                                ); // 就诊序号
            sb.Append(    "OUTP_ORDERS.ORDERED_BY, "                                             ); // 开单科室
            sb.Append(    "OUTP_ORDERS.DOCTOR, "                                                 ); // 处方医生
            sb.Append(    "OUTP_ORDERS.ORDER_DATE, "                                             ); // 开单时间
            sb.Append(    "OUTP_PRESC.CHARGE_INDICATOR "                                         ); // 收费标识
            sb.Append("FROM OUTP_ORDERS, "                                                       ); // 门诊医嘱主记录
            sb.Append(    "OUTP_PRESC "                                                          ); // 处方医嘱明细记录
            sb.Append("WHERE OUTP_PRESC.SERIAL_NO = OUTP_ORDERS.SERIAL_NO "                      ); // 流水号
            sb.Append(    "AND OUTP_ORDERS.PATIENT_ID = " + SQL.SqlConvert(patientId)            ); // 病人标识号

            return _connection.SelectData(sb.ToString());
        }
        #endregion
    }
}
