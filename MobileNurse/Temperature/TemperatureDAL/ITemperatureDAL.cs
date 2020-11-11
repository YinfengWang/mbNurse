using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonEntity;

namespace TemperatureDAL
{
    public interface ITemperatureDal
    {
        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <param name="patientId">病人ID</param>
        /// <param name="visitId">住院次数</param>
        /// <returns></returns>
        DataSet GetPatientInfo(string patientId, int visitId);

        /// <summary>
        /// 获取病人体征信息
        /// </summary>
        /// <param name="patientId">病人ID</param>
        /// <param name="visitId">住院次数</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        DataTable GetPatientVitalSignal1(string patientId, int visitId, DateTime startDate, DateTime endDate);

        //DataSet GetAdtLogCd(string patientId, int visitId);
        //string GetAdtLogC(string patientId, int visitId);
        //DataSet GetAdtLogD(string patientId, int visitId, string toShortDateString, string s);
        //DataSet GetOperationInfo(string patientId, int visitId);
    }
}
