using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using HISPlus;
using SQL = HISPlus.SqlManager;
namespace DocDAL
{
    public class DocNursingDbi
    {
        protected DbAccess _connection;

        public DocNursingDbi(DbAccess dbConnect)
        {
            _connection = dbConnect;
        }

        /// <summary>
        /// 获取病人不达标的评估单
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <param name="templateId">评估单模版ID</param>
        /// <returns></returns>
        public virtual DataSet GetDocNursingNotStard(string patientId,string visitId,decimal templateId)
        {
            string sql = "SELECT * FROM v_not_standard_doc WHERE "
                        + " PATIENT_ID = " + SQL.SqlConvert(patientId.Trim())
                        + " AND VISIT_NO = " + SQL.SqlConvert(visitId.ToString())
                        + " AND TEMPLATE_ID = " + SQL.SqlConvert(templateId.ToString());
            return _connection.SelectData(sql);
        }

        /// <summary>
        /// 获取病人[节点项目]不达标的评估单
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <param name="templateId">评估单模版ID</param>
        /// <returns></returns>
        public virtual DataSet GetDocItemNursingNotStard(string patientId, string visitId, decimal templateId)
        {
            string sql = "SELECT * FROM v_not_standard_docItem WHERE "
                        + " PATIENT_ID = " + SQL.SqlConvert(patientId.Trim())
                        + " AND VISIT_NO = " + SQL.SqlConvert(visitId.ToString())
                        + " AND TEMPLATE_ID = " + SQL.SqlConvert(templateId.ToString());
            return _connection.SelectData(sql);
        }

        /// <summary>
        /// 获取【最近】不达标的评估单的[病人+评估单]列表
        /// </summary>
        /// <param name="wardCode"></param>
        /// <returns></returns>
        public virtual DataSet GetDocNotStandPatInWard(string wardCode)
        {
            string sql = "SELECT * FROM v_not_standard_docpat_template WHERE "
                        + " WARD_CODE = " + SQL.SqlConvert(wardCode.ToString());
            return _connection.SelectData(sql);
        }

        /// <summary>
        /// 获取【最近】不达标的评估单的[病人+评估单]列表
        /// </summary>
        /// <param name="wardCode"></param>
        /// <returns></returns>
        public virtual DataSet GetDocNotStandPatInWard11(string wardCode)
        {
            string sql = "SELECT * FROM v_not_standard_docpat_template WHERE "
                        + " WARD_CODE = " + SQL.SqlConvert(wardCode.ToString());
            return _connection.SelectData(sql);
        }

        /// <summary>
        /// 获取需要预警的文书模版
        /// </summary>
        /// <returns>TEMPLATE_ID</returns>
        public virtual DataSet GetDocTemplateAlarm()
        {
            string sql = "select TEMPLATE_ID from mobile.doc_template a where a.is_alarm = 1 order by a.alarm_serial_no";
            return _connection.SelectData(sql);
        }

        public DataSet GetDocNursingRecordDs(string docNursingId)
        {
            string sql = "select * from doc_nursing_record t where t.doc_nursing_id='" + docNursingId + "'";
            return _connection.SelectData(sql, "doc_nursing_record");
        }

        public int SaveDocNursingRecordDs(DataSet docNursingRecordDs)
        {
            return _connection.Update(ref docNursingRecordDs);
        }
    }
}
