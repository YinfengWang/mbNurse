using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public class EvaluationDbI
    {
        public EvaluationDbI()
        {
        }
        
        
        /// <summary>
        /// 获取评估项目
        /// </summary>
        /// <returns></returns>
        public DataSet GetDictItem(string dictId)
        {
            string sql = "SELECT * FROM HIS_DICT_ITEM "
                        + "WHERE DICT_ID = " + SQL.SqlConvert(dictId);

            return GVars.sqlceLocal.SelectData(sql, "HIS_DICT_ITEM");
        }

        /// <summary>
        /// 获取评估项目，李治鹏， 2014-11-13
        /// </summary>
        /// <returns></returns>
        public DataSet GetDictItem2(string tmpTemplateId)
        {
            string sql = "SELECT * FROM DOC_TEMPLATE_ELEMENT "
                        + "WHERE TEMPLATE_ID = " + SQL.SqlConvert(tmpTemplateId) + "ORDER BY PARENT_ID,SORT_ID";

            return GVars.sqlceLocal.SelectData(sql, "DOC_TEMPLATE_ELEMENT");
        }
        
        
        /// <summary>
        /// 获取评估项目
        /// </summary>
        /// <returns></returns>
        public DataSet GetEvalRec(DateTime dtNow, string dictId, string patientId, string visitId)
        {
            string sql = "SELECT * FROM PAT_EVALUATION_M "
                        + "WHERE DICT_ID = " + SQL.SqlConvert(dictId)
                        +       "AND PATIENT_ID = " + SQL.SqlConvert(patientId)
                        +       "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                        +       "AND RECORD_DATE = " + SQL.SqlConvert(dtNow.ToString(ComConst.FMT_DATE.LONG));
            
            return GVars.sqlceLocal.SelectData(sql, "PAT_EVALUATION_M");
        }
        
        
        /// <summary>
        /// 获取最近一次评估时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetLastRecDate(string dictId, string patientId, string visitId)
        {
            string sql = "SELECT MAX(RECORD_DATE) RECORD_DATE FROM PAT_EVALUATION_M "
                        + "WHERE DICT_ID = " + SQL.SqlConvert(dictId)
                        +   "AND PATIENT_ID = " + SQL.SqlConvert(patientId)
                        +   "AND VISIT_ID = " + SQL.SqlConvert(visitId);
            
            DataSet ds = GVars.sqlceLocal.SelectData(sql, "PAT_EVALUATION_M");
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return GVars.GetDateNow();
            }
            else if (ds.Tables[0].Rows[0]["RECORD_DATE"] == DBNull.Value)
            {
                return GVars.GetDateNow();
            }
            else
            {                
                return (DateTime)(ds.Tables[0].Rows[0]["RECORD_DATE"]);
            }
        }
        
        
        /// <summary>
        /// 保存评估项目
        /// </summary>
        /// <returns></returns>
        public bool SaveEvalRec(ref DataSet dsChanged, DateTime dtRecord, string dictId, string patientId, string visitId)
        {
            GVars.sqlceLocal.Update(ref dsChanged);
                        
            return true;
        }        
        
        
        /// <summary>
        /// 保存评估项目
        /// </summary>
        /// <returns></returns>
        public bool SaveEvalRecDel(ref DataSet dsDel)
        {
            string sql = "SELECT * DOC_NURSING_RECORD_TOMBSTONE "
                        + "WHERE (1 = 2)";

            GVars.sqlceLocal.Update(ref dsDel, "DOC_NURSING_RECORD_TOMBSTONE", sql);
            
            return true;
        }  
        
        
        /// <summary>
        /// 获取评估项目
        /// </summary>
        /// <returns></returns>
        public DataSet GetEvalRecOne(string dictId, string patientId, string visitId)
        {
            //string sql = "SELECT * FROM PAT_EVALUATION_M "
            //            + "WHERE DICT_ID = " + SQL.SqlConvert(dictId)
            //            +       "AND PATIENT_ID = " + SQL.SqlConvert(patientId)
            //            +       "AND VISIT_ID = " + SQL.SqlConvert(visitId);

            //return GVars.sqlceLocal.SelectData(sql, "PAT_EVALUATION_M");

            string sql = "SELECT * FROM DOC_NURSRING "
                        + "WHERE DOC_NURSING_ID = " + SQL.SqlConvert(dictId)
                        + "AND PATIENT_ID = " + SQL.SqlConvert(patientId)
                        + "AND VISIT_ID = " + SQL.SqlConvert(visitId);

            return GVars.sqlceLocal.SelectData(sql, "PAT_EVALUATION_M");
        }

        /// <summary>
        /// 获取评估项目
        /// </summary>
        /// <returns></returns>
        public DataSet GetEvalRecOne2(string tmpDocNuringId)
        {

            string sql = "SELECT * FROM DOC_NURSING_RECORD "
                        + "WHERE DOC_NURSING_ID = " + "'"+ tmpDocNuringId + "'";

            return GVars.sqlceLocal.SelectData(sql, "DOC_NURSING_RECORD");
        }


        /// <summary>
        /// 保存评估项目
        /// </summary>
        /// <returns></returns>
        public bool addNewMasterRecord(ref DataSet dsChanged)
        {
            GVars.sqlceLocal.Update(ref dsChanged);

            return true;
        }

        /// <summary>
        /// 保存评估项目
        /// </summary>
        /// <returns></returns>
        public bool SaveEvalRecOne(ref DataSet dsChanged, DateTime dtRecord, string dictId, string patientId, string visitId)
        {
            GVars.sqlceLocal.Update(ref dsChanged);
            
            return true;
        }
        
        
        /// <summary>
        /// 保存评估项目
        /// </summary>
        /// <returns></returns>
        public bool SaveEvalRecOneDel(ref DataSet dsDel)
        {
            string sql = "SELECT * FROM DOC_NURSING_RECORD_TOMBSTONE "
                        + "WHERE (1 = 2)";

            GVars.sqlceLocal.Update(ref dsDel, "DOC_NURSING_RECORD_TOMBSTONE", sql);
            
            return true;
        }                                
    }
}
