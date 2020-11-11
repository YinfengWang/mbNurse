using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CommonEntity;
using HISPlus;
using SQL = HISPlus.SqlManager;
using System.Text;

namespace TemperatureDAL
{
    public class TemperatureMobileDal : ITemperatureDal
    {
        #region 变量
        protected DbAccess _connection;
        #endregion

        public TemperatureMobileDal(DbAccess conn)
        {
            _connection = conn;
        }

        //public DataSet GetPatientOneVitalSignalbyVisitId(string patientId, int visitId, string str_vital_signs)
        //{
        //    return _connection.SelectData("select time_point,Vital_signs,Vital_signs_values,Units  from sinldo.vital_signs_rec where patient_id = '" + patientId + "'  and visit_id=" + visitId + "  and vital_signs like  '" + str_vital_signs + "%'  order by time_point");
        //}

        //public DataSet GetPatientPhysicalInfo(string string_patient_id)
        //{
        //    return _connection.SelectData(" Select * from sinldo.vital_signs_rec where vital_signs_rec.patient_id = '" + string_patient_id + "'");
        //}

        //public DataSet GetPatientVitalSignal(string patientId, string str_startT, string str_endT, string str_vital_signs)
        //{
        //    return _connection.SelectData(" Select time_point,VITAL_SIGNS_Values from sinldo.vital_signs_rec where vital_signs_rec.patient_id = '" + patientId + "' and  vital_signs like  '" + str_vital_signs + "%' and time_point between to_date('" + str_startT + "','yyyy-mm-dd') and to_date('" + str_endT + "','yyyy-mm-dd') order by time_point");
        //}

        /// <summary>
        /// 获取病人体征信息
        /// </summary>
        /// <param name="patientId">病人ID</param>
        /// <param name="visitId">住院次数</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public DataTable GetPatientVitalSignal1(string patientId, int visitId, DateTime startDate, DateTime endDate)
        {
            // 获取护理事件 old code 
            // string sql = string.Empty;

            // sql = "SELECT VITAL_SIGNS_REC.*, NURSING_ITEM_DICT.ATTRIBUTE ";
            // sql += "FROM VITAL_SIGNS_REC, NURSING_ITEM_DICT ";
            // sql += "WHERE VITAL_SIGNS_REC.PATIENT_ID = " + SQL.SqlConvert(patientId);
            // sql += "AND VITAL_SIGNS_REC.VISIT_ID = " + visitId;
            // sql += "AND VITAL_SIGNS_REC.WARD_CODE = NURSING_ITEM_DICT.WARD_CODE ";
            // sql += "AND VITAL_SIGNS_REC.VITAL_CODE = NURSING_ITEM_DICT.VITAL_CODE ";
            // sql += @" and time_point >= " + SqlManager.GetOraDbDate(startDate.ToString(ComConst.FMT_DATE.LONG)) +
            //" and time_point <" + SqlManager.GetOraDbDate(endDate.ToString(ComConst.FMT_DATE.LONG)) +
            //" order by time_point,VITAL_SIGNS_REC.VITAL_CODE";

            ///////////-------------------------------///////////////
            //护理事件手术、分娩包含14天之前的数据
            string sql = string.Empty;
            sql = "select * from (";
            sql += "SELECT VITAL_SIGNS_REC.*, NURSING_ITEM_DICT.ATTRIBUTE ";
            sql += "FROM VITAL_SIGNS_REC, NURSING_ITEM_DICT ";
            sql += "WHERE VITAL_SIGNS_REC.PATIENT_ID = " + SQL.SqlConvert(patientId);
            sql += "AND VITAL_SIGNS_REC.VISIT_ID = " + visitId;
            sql += "AND VITAL_SIGNS_REC.WARD_CODE = NURSING_ITEM_DICT.WARD_CODE ";
            sql += "AND VITAL_SIGNS_REC.VITAL_CODE = NURSING_ITEM_DICT.VITAL_CODE ";
            sql += @" and time_point >= " + SqlManager.GetOraDbDate(startDate.ToString(ComConst.FMT_DATE.LONG)) +
           " and time_point <" + SqlManager.GetOraDbDate(endDate.ToString(ComConst.FMT_DATE.LONG));
            sql += "  union  ";
            sql += "SELECT VITAL_SIGNS_REC.*, NURSING_ITEM_DICT.ATTRIBUTE ";
            sql += "FROM VITAL_SIGNS_REC, NURSING_ITEM_DICT ";
            sql += "WHERE VITAL_SIGNS_REC.PATIENT_ID = " + SQL.SqlConvert(patientId);
            sql += "AND VITAL_SIGNS_REC.VISIT_ID = " + visitId;
            sql += "AND VITAL_SIGNS_REC.WARD_CODE = NURSING_ITEM_DICT.WARD_CODE ";
            sql += "AND VITAL_SIGNS_REC.VITAL_CODE = NURSING_ITEM_DICT.VITAL_CODE ";
            sql += @" and time_point >= " + SqlManager.GetOraDbDate(startDate.AddDays(-14).ToString(ComConst.FMT_DATE.LONG));
            sql += @"AND (VITAL_SIGNS_REC.VITAL_SIGNS LIKE '手%术' OR VITAL_SIGNS_REC.VITAL_SIGNS LIKE '分%娩')  ";
            sql += " ) order by time_point,VITAL_CODE ";
            DataSet ds = _connection.SelectData_NoKey(sql, "VITAL_SIGNS_REC");

            return ds.Tables[0];

            // !!! vital_signs_rec表属于ORDADM.不属于sinldo. [2013年8月19日]
            // vital_signs_rec病人体征记录(如某时间某体征温度等)
            return _connection.SelectData(
@" SELECT TIME_POINT,
       VITAL_SIGNS,
       NVL(TO_CHAR(VITAL_SIGNS_VALUES), TO_CHAR(VITAL_SIGNS_CVALUES)) AS VITAL_SIGNS_VALUES,
       UNITS
  FROM VITAL_SIGNS_REC
WHERE 
VITAL_SIGNS_REC.TIME_POINT IS NOT NULL 
AND NVL(TO_CHAR(VITAL_SIGNS_VALUES), TO_CHAR(VITAL_SIGNS_CVALUES)) IS NOT NULL
and vital_signs_rec.patient_id = '" +
                                      patientId +
            @"' and time_point >= to_date('" + startDate + @"','yyyy-mm-dd')  
            and time_point < to_date('" + endDate + "','yyyy-mm-dd')+1 order by time_point,VITAL_CODE").Tables[0];
        }

        //        public DataSet GetPatientVitalSignalbyVisitId(string patientId, int visitId)
        //        {
        //            return _connection.SelectData(@"select time_point,Vital_signs,Vital_signs_values
        //                ,Units  from sinldo.vital_signs_rec where patient_id = '"
        //                + patientId + "'  and visit_id=" + visitId + "  order by time_point");
        //        }

        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <param name="patientId">病人ID</param>
        /// <param name="visitId">住院次数</param>
        /// <returns></returns>
        public DataSet GetPatientInfo(string patientId, int visitId)
        {
            //IList<PatientInfo> patients = EntityOper.GetInstance().FindByProperty<PatientInfo>(
            //    new[] { "Id.PatientId", "Id.VisitId" },
            //    new object[] { patientId, Convert.ToByte(visitId) });

            //// 如果住院信息没有找到,就只取病人信息
            //if (!patients.Any())
            //{
            //    patients = EntityOper.GetInstance().FindByProperty<PatientInfo>(
            //        "Id.PatientId", patientId);
            //}

            //if (patients.Count > 0)
            //    return patients.First();


            //throw new Exception("未找到该病人住院信息.请检查后重试.");


            string sql = "SELECT * FROM PATIENT_INFO WHERE "
                      + "PATIENT_ID = " + SQL.SqlConvert(patientId.Trim())
                      + "AND VISIT_ID = " + SQL.SqlConvert(visitId.ToString());

            DataSet ds = _connection.SelectData(sql);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                sql = "SELECT * FROM PATIENT_INFO_TOMBSTONE WHERE "
                        + "PATIENT_ID = " + SQL.SqlConvert(patientId.Trim())
                        + "AND VISIT_ID = " + SQL.SqlConvert(visitId.ToString())
                        + "ORDER BY BED_NO ";
                sql += "DESC";
                return _connection.SelectData(sql);
            }
            else
            {
                return ds;
            }


        }

        /// <summary>
        /// 获取手术结束日期及时间列表
        /// </summary>
        /// <param name="patientId">病人ID</param>
        /// <param name="visitId">住院次数</param>
        /// <returns></returns>
        public DataSet GetOperationInfo(string patientId, int visitId)
        {
            //string strSQL = "select to_char(end_date_time,'yyyy-mm-dd hh24:mi:ss') as end_date_time from operation_master "
            //    + " where patient_id='" + patientId + "'"
            //    + " and  VISIT_ID=" + visitId.ToString();

            string strSQL = "select to_char(TIME_POINT,'yyyy-mm-dd hh24:mi:ss') as end_date_time,VITAL_SIGNS from VITAL_SIGNS_REC "
             + " where patient_id='" + patientId + "'"
             + " AND CLASS_CODE='C' AND VITAL_SIGNS like '手术%' "
             + " and  VISIT_ID=" + visitId;
            return _connection.SelectData(strSQL);
        }

        //D	辅助护理事件
        //C	主要护理事件
        //B	辅助护理项目
        //A	主要护理项目
        //E	其它护理项目

        /// <summary>
        /// 获取病人变化记录(转入转出记录)
        /// </summary>
        /// <param name="patientId">病人ID</param>
        /// <param name="visitId">住院次数</param>
        /// <param name="str_startT">开始日期</param>
        /// <param name="str_endT">结束日期</param>
        /// <returns></returns>
        public DataSet GetAdtLogD(string patientId, int visitId, string str_startT, string str_endT)
        {
            DataSet myds1 = new DataSet();
            // 
            string strSQL1 = " select t.log_Date_Time ,a.dept_name from INPADM.ADT_LOG t ,comm.dept_dict a where   t.dept_code=a.dept_code and t.patient_id ='"
             + patientId + "' AND t.VISIT_ID = " + visitId.ToString() + "  AND t.action='D'"
                 +
                @" and t.log_Date_Time >= to_date('" + str_startT + @"','yyyy-mm-dd')  
                and t.log_Date_Time < to_date('" + str_endT + "','yyyy-mm-dd')+1 ";
            myds1 = _connection.SelectData(strSQL1);

            return myds1;
        }


        /// <summary>
        /// 获取病人变化记录(入院时间)
        /// </summary>
        /// <param name="patientId">病人ID</param>
        /// <param name="visitId">住院次数</param>
        /// <returns></returns>
        public string GetAdtLogC(string patientId, int visitId)
        {
            string sql = " select t.log_Date_Time ,a.dept_name from INPADM.ADT_LOG t ,comm.dept_dict a where   t.dept_code=a.dept_code and t.patient_id ='" + patientId + "' AND t.VISIT_ID = " + visitId.ToString() + "  AND t.action='C'";
            if (_connection.SelectValue(sql) == true)
            {
                return _connection.GetResult(0);
            }
            return string.Empty;

            //string strSQL1 = " select t.log_Date_Time ,a.dept_name from INPADM.ADT_LOG t ,comm.dept_dict a where   t.dept_code=a.dept_code and t.patient_id ='";
            //string strTmp = strSQL1;
            //strSQL1 = strTmp + patientId + "' AND t.VISIT_ID = " + visitId.ToString() + "  AND t.action='C'";

            //return OracleHelper.ExecuteScalar(strSQL1).ToString();
        }

        /// <summary>
        /// 获取病人变化记录
        /// </summary>
        /// <param name="patientId">病人ID</param>
        /// <param name="visitId">住院次数</param>
        /// <returns></returns>
        public DataSet GetAdtLogCd(string patientId, int visitId)
        {
            DataSet myds1 = new DataSet();
            // 
            string strSQL1 = " select t.log_Date_Time ,a.dept_name from INPADM.ADT_LOG t ,comm.dept_dict a where   t.dept_code=a.dept_code and t.patient_id ='";
            string strTmp = strSQL1;
            strSQL1 = strTmp + patientId + "' AND t.VISIT_ID = " + visitId.ToString() + " AND ( t.action='D' or t.action='C')";
            myds1 = _connection.SelectData(strSQL1);

            return myds1;
        }
    }
}
