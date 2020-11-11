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
                        + "ORDER BY "
                        +   "DESC";
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
                       +    "AND WARD_CODE = " + SQL.SqlConvert(wardCode);
            
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
                sql += "(WARD_CODE IS NULL OR DEPT_CODE IS NULL) ";
            }
            else
            {
                sql += "WARD_CODE = " + SQL.SqlConvert(wardCode);
            }
            
            sql += "ORDER BY ";
            sql +=      "BED_NO ";
            
            return _connection.SelectData(sql);
        }
        #endregion
    }
}
