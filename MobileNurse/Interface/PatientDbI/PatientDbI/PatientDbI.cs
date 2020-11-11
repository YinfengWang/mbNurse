using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public struct PAT_INHOS_STATE
    {
        /// <summary>
        /// 出院
        /// </summary>
        public const string OUT = "出院";
        /// <summary>
        /// 在院
        /// </summary>
        public const string IN = "在院";
        /// <summary>
        /// 带入科
        /// </summary>
        public const string WAIT = "待入科";
        /// <summary>
        /// 新入
        /// </summary>
        public const string NEW = "新入";
        /// <summary>
        /// 转科
        /// </summary>
        public const string TRANSFER = "转科";

    }

    public class PatientDbI
    {
        protected DbAccess _connection;

        public PatientDbI(DbAccess dbConnect)
        {
            _connection = dbConnect;
        }
        /// <summary>
        /// 病人在院状态。注：应取数据字典
        /// </summary>


        #region 获取病人信息
        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public virtual DataSet GetPatientInfo(string patientId)
        {
            string sql = "SELECT * FROM PATIENT_INFO WHERE PATIENT_ID = " + SQL.SqlConvert(patientId.Trim());

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
            string sql = "SELECT * FROM PATIENT_INFO WHERE "
                        + "PATIENT_ID = " + SQL.SqlConvert(patientId.Trim())
                        + "AND VISIT_ID = " + SQL.SqlConvert(visitId.Trim());

            DataSet ds = _connection.SelectData(sql);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                sql = "SELECT * FROM PATIENT_INFO_TOMBSTONE WHERE "
                        + "PATIENT_ID = " + SQL.SqlConvert(patientId.Trim())
                        + "AND VISIT_ID = " + SQL.SqlConvert(visitId.Trim())
                        + "ORDER BY UPDATE_TIMESTAMP "
                        + "DESC";
                return _connection.SelectData(sql);
            }
            else
            {
                return ds;
            }
        }

        /// <summary>
        /// 获取病人基本信息
        /// </summary>
        /// <param name="patientId">病人标识号</param>
        /// <returns></returns>
        public virtual DataSet GetInpPatientInfoByID(string patientId)
        {
            string sql = "SELECT * FROM PATIENT_INFO WHERE "
                        + "PATIENT_ID = " + SQL.SqlConvert(patientId.Trim());

            DataSet ds = _connection.SelectData(sql);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                sql = "SELECT * FROM PATIENT_INFO_TOMBSTONE WHERE "
                        + "PATIENT_ID = " + SQL.SqlConvert(patientId.Trim());
                return _connection.SelectData(sql);
            }
            else
            {
                return ds;
            }
        }


        public virtual DataSet GetPatientInfo_FromID(string patientId)
        {
            string sql = "SELECT * FROM PATIENT_INFO WHERE PATIENT_ID = " + SQL.SqlConvert(patientId.Trim());

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
            string sql = "SELECT * FROM PATIENT_INFO "
                       + "WHERE BED_LABEL = " + SQL.SqlConvert(bedLabel)
                       + "AND WARD_CODE = " + SQL.SqlConvert(wardCode);

            return _connection.SelectData(sql);
        }


        /// <summary>
        /// 获取整个病区的病人列表
        /// </summary>
        /// <param name="wardCode"></param>
        /// <returns></returns>

        public virtual DataSet GetWardPatientList(string wardCode)
        {
            string sql = "SELECT * FROM PATIENT_INFO WHERE ";

            // 待入科病人
            if (wardCode.Equals("-1") == true)
            {
                //原来的
                //sql += "(WARD_CODE IS NULL OR DEPT_CODE IS NULL) ";
                //LB20110413修改
                sql += "WARD_CODE IS NULL ";
            }
            else
            {
                sql += "WARD_CODE = " + SQL.SqlConvert(wardCode);
            }

            sql += "ORDER BY ";
            sql += "BED_NO ";

            return _connection.SelectData(sql);
        }

        /// <summary>
        /// 获取整个病区的病人列表
        /// </summary>
        /// <param name="wardCode"></param>
        /// <param name="inHos">病人在院状态 PAT_INHOS_STATE</param>
        /// <returns></returns>

        public virtual DataSet GetWardPatientList(string wardCode, string patId, DateTime dtStart, DateTime dtEnd, string inHos)
        {
            string tableName = string.Empty;
            string filterWard = string.Empty;
            string filterPat = string.Empty;
            string filterTime = string.Empty;
            string filter = string.Empty;
            string sql = string.Empty;
            switch (inHos)
            {
                case PAT_INHOS_STATE.IN:
                    tableName = "PATIENT_INFO";
                    filterPat = "PATIENT_ID LIKE " + SQL.SqlConvert(patId + "%");
                    filterWard = "WARD_CODE = " + SQL.SqlConvert(wardCode);
                    filterTime = "(ADMISSION_DATE_TIME >= " + SQL.GetOraDbDate(dtStart.ToString(ComConst.FMT_DATE.LONG));
                    filterTime += "AND ADMISSION_DATE_TIME < " + SQL.GetOraDbDate(dtEnd.ToString(ComConst.FMT_DATE.LONG));
                    filterTime += ") ";
                    sql = "SELECT * FROM " + tableName + " WHERE ";
                    break;
                case PAT_INHOS_STATE.WAIT:
                    tableName = "PATIENT_INFO";
                    filterPat = "PATIENT_ID LIKE " + SQL.SqlConvert(patId + "%");
                    filterWard = "WARD_CODE IS NULL ";
                    filterTime = "(ADMISSION_DATE_TIME >= " + SQL.GetOraDbDate(dtStart.ToString(ComConst.FMT_DATE.LONG));
                    filterTime += "AND ADMISSION_DATE_TIME < " + SQL.GetOraDbDate(dtEnd.ToString(ComConst.FMT_DATE.LONG));
                    filterTime += ") ";
                    sql = "SELECT * FROM " + tableName + " WHERE ";
                    break;
                case PAT_INHOS_STATE.OUT:
                    tableName = "PATIENT_INFO_TOMBSTONE";
                    filterPat = "PATIENT_ID LIKE " + SQL.SqlConvert(patId + "%");
                    filterWard = "WARD_CODE = " + SQL.SqlConvert(wardCode);
                    filterTime = "(CREATE_TIMESTAMP >= " + SQL.GetOraDbDate(dtStart.ToString(ComConst.FMT_DATE.LONG));
                    filterTime += "AND CREATE_TIMESTAMP < " + SQL.GetOraDbDate(dtEnd.ToString(ComConst.FMT_DATE.LONG));
                    filterTime += ") ";
                    sql = "SELECT * FROM " + tableName + " WHERE ";
                    break;
                case PAT_INHOS_STATE.TRANSFER:
                    //tableName = "PATIENT_INFO";
                    filterPat = "b.PATIENT_ID LIKE " + SQL.SqlConvert(patId + "%");
                    filterWard = "b.WARD_CODE = " + SQL.SqlConvert(wardCode);
                    filterTime = "(log_date_time >= " + SQL.GetOraDbDate(dtStart.ToString(ComConst.FMT_DATE.LONG));
                    filterTime += "AND log_date_time < " + SQL.GetOraDbDate(dtEnd.ToString(ComConst.FMT_DATE.LONG));
                    filterTime += ")    ";
                    sql = "select b.ward_code,b.dept_code,a.patient_id,a.visit_id,";
                    sql += "  a.inp_no,a.admission_date_time,a.name,a.sex,";
                    sql += "  a.date_of_birth,a.diagnosis,a.alergy_drugs,a.doctor_in_charge,";
                    sql += "   a.bed_no,a.bed_label,a.patient_status_name,a.nursing_class,";
                    sql += "   a.nursing_class_name,a.nursing_class_color,c.dept_name,a.create_timestamp,";
                    sql += "   a.update_timestamp,a.dept_code as current_dept_code,a.ward_name as current_ward_name,a.room_no,";
                    sql += "   a.adm_ward_date_time,a.id_no";
                    sql += "   from patient_info a,adt_log b,mobile.dept_dict c ";
                    sql += "  where a.patient_id = b.patient_id";
                    sql += "   and a.visit_id = b.visit_id and b.dept_code = c.dept_code";
                    sql += "  and b.action = 'E' and ";
                    break;

            }
            if (String.IsNullOrEmpty(wardCode))
                filter += filterPat + " AND " + filterTime;
            else
                filter += filterPat + " AND " + filterWard + " AND " + filterTime;

            sql += filter;
            sql += " ORDER BY BED_NO ";

            return _connection.SelectData(sql);
        }
        #endregion
    }
}
