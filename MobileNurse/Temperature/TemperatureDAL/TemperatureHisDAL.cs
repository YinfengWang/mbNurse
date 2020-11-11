using System;
using System.Data;

namespace TemperatureDAL
{
    public class TemperatureHisDal : ITemperatureDal
    {
        /// <summary>
        /// 禁用默认实例
        /// </summary>
        private TemperatureHisDal() { }

        ///// <summary>
        ///// 必须指定连接字符串
        ///// </summary>
        ///// <param name="connStr"></param>
        //public TemperatureHisDal(string connStr)
        //{
        //    OracleHelper.ConnectionString = connStr;
        //}

        //public int CaclThermometerWeekCount(string patientId, int visitId)
        //{
        //    PatVisitModel model = PatVisitModel.GetInstance()
        //        .Select(PatVisitModel.ColName_AdmissionDateTime)
        //        .Where(p => p.PatientId == patientId && p.VisitId == visitId).TOList().FirstOrDefault();

        //    //string sqlCmd = "select ADMISSION_DATE_TIME from pat_visit where patient_id = '" + patientId + "' and visit_id = " + visitId.ToString();
        //    //object obj = OracleHelper.ExecuteScalar(sqlCmd);

        //    DateTime ADMISSION_DATE = model == null ? DateTime.Now : model.AdmissionDateTime;

        //    TimeSpan DaySpan = (TimeSpan)(DateTime.Now.AddDays(1.0) - ADMISSION_DATE);
        //    double DaySum = DaySpan.Days;

        //    return (int)Math.Ceiling(DaySum / 7);
        //}

        //public bool DeleteThermometerLinePicFile(string filePath)
        //{
        //    try
        //    {
        //        File.Delete(filePath);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

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
        /// <param name="str_startT">开始日期</param>
        /// <param name="str_endT">结束日期</param>
        /// <returns></returns>
        public DataTable GetPatientVitalSignal1(string patientId, string str_startT, string str_endT)
        {
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
            @"' and time_point >= to_date('" + str_startT + @"','yyyy-mm-dd')  
            and time_point < to_date('" + str_endT + "','yyyy-mm-dd')+1 order by time_point,VITAL_CODE").Tables[0];
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
            DataSet ds = _connection.SelectData(@"SELECT * FROM V_WIRELESS_PATS_INDEX A 
                where patient_id = '" + patientId + "'  and visit_id=" + visitId + " ");

            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                throw new Exception("无该病人住院信息.请检查后重试.");
            return ds;
        }

        /// <summary>
        /// 获取住院时间
        /// </summary>
        /// <param name="patientId">病人ID</param>
        /// <param name="visitId">住院次数</param>
        /// <returns></returns>
        public DateTime GetAdmissionDate(string patientId, int visitId)
        {
            // 病人住院主记录表.入院日期及时间
            object obj = OracleHelper.ExecuteScalar("select ADMISSION_DATE_TIME from pat_visit where patient_id = '" + patientId + "' and visit_id = " + visitId.ToString());
            if (obj != null)
            {
                return Convert.ToDateTime(obj);
            }
            else
                throw new Exception("无该病人住院信息.请检查后重试.");
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
             + " and  VISIT_ID=" + visitId.ToString();
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
            string strSQL1 = " select t.log_Date_Time ,a.dept_name from INPADM.ADT_LOG t ,comm.dept_dict a where   t.dept_code=a.dept_code and t.patient_id ='";
            string strTmp = strSQL1;
            strSQL1 = strTmp + patientId + "' AND t.VISIT_ID = " + visitId.ToString() + "  AND t.action='C'";

            return OracleHelper.ExecuteScalar(strSQL1).ToString();
        }

        /// <summary>
        /// 获取病人变化记录
        /// </summary>
        /// <param name="patientId">病人ID</param>
        /// <param name="visitId">住院次数</param>
        /// <returns></returns>
        public DataSet GetAdtLogCD(string patientId, int visitId)
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
