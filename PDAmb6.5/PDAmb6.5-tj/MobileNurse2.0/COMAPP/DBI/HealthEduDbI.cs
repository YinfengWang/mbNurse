using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public class HealthEduDbI
    {
        public HealthEduDbI()
        {
        }
        
        
        /// <summary>
        /// ��ȡ����������Ŀ
        /// </summary>
        /// <returns></returns>
        public DataSet GetDictItem(string dictId)
        {
            string sql = "SELECT * FROM HIS_DICT_ITEM "
                        + "WHERE DICT_ID = " + SQL.SqlConvert(dictId);
            
            return GVars.sqlceLocal.SelectData(sql, "HIS_DICT_ITEM");
        }
        
        
        /// <summary>
        /// ��ȡ����������¼
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public DataSet GetHealthEduRec(string deptCode, string patientId, string visitId)
        {
            string sql = "SELECT * FROM HEALTH_EDU_REC "
                        + "WHERE DEPT_CODE = " + SQL.SqlConvert(deptCode)
                        +       "AND PATIENT_ID = " + SQL.SqlConvert(patientId)
                        +       "AND VISIT_ID = " + SQL.SqlConvert(visitId);
            
            return GVars.sqlceLocal.SelectData(sql, "HEALTH_EDU_REC");
        }
        
        
        /// <summary>
        /// ���潡��������¼
        /// </summary>
        /// <param name="dsChanged"></param>
        /// <returns></returns>
        public bool SaveHealthEduRec(DataSet dsChanged)
        {
            GVars.sqlceLocal.Update(ref dsChanged);
            return true;
        }
        
        
        /// <summary>
        /// ���潡��������¼
        /// </summary>
        /// <returns></returns>
        public bool SaveHealthEduRecDel(ref DataSet dsDel)
        {
            string sql = "SELECT * FROM HEALTH_EDU_REC_TOMBSTONE "
                        + "WHERE (1 = 2)";
            
            GVars.sqlceLocal.Update(ref dsDel, "HEALTH_EDU_REC_TOMBSTONE", sql);
            
            return true;
        }  

        
        
        /// <summary>
        /// ��ȡ������Ŀ����
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public string GetHealthEduItemContent(string itemId, string deptCode)
        {
            string sql = "SELECT * FROM HIS_DICT_ITEM_CONTENT "
                        + "WHERE DICT_ID = '07' "
                        +       "AND DEPT_CODE = " + SQL.SqlConvert(deptCode)
                        +       "AND ITEM_ID = " + SQL.SqlConvert(itemId);
            
            DataSet ds = GVars.sqlceLocal.SelectData(sql);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return string.Empty;
            }
            else
            {
                return ds.Tables[0].Rows[0]["CONTENT"].ToString();
            }
        }
    }
}
