using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public class HealthEduDbI
    {
        #region 变量
        protected DbAccess _connection;
        #endregion

        public HealthEduDbI(DbAccess conn)
        {
            _connection = conn;
        }
        
        
        /// <summary>
        /// 获取病人的健康教育记录
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public DataSet GetHealthEduRec(string deptCode, string patientId, string visitId)
        {
            string sql = string.Empty;            
            string sqlItem = "(SELECT {0} FROM HEALTH_EDU_REC WHERE ";
            sqlItem +=      "PATIENT_ID = " + SQL.SqlConvert(patientId);
            sqlItem +=      "AND VISIT_ID = " + SQL.SqlConvert(visitId);
            sqlItem +=      "AND DEPT_CODE = " + SQL.SqlConvert(deptCode);
            sqlItem +=      "AND ITEM_ID = A.ITEM_ID ";
            sqlItem +=      ") {1} ";
            
            sql = "SELECT A.ITEM_ID, ";
            sql +=       "A.ITEM_NAME, ";
            sql +=       SQL.SqlConvert(patientId) + " PATIENT_ID, ";
            sql +=       SQL.SqlConvert(visitId) + " VISIT_ID, ";
            sql +=       SQL.SqlConvert(deptCode) + " DEPT_CODE, ";
            sql +=       string.Format(sqlItem, "EDU_DATE", "EDU_DATE") + ", ";
            sql +=       string.Format(sqlItem, "EDU_NURSE", "EDU_NURSE") + ", ";
            sql +=       string.Format(sqlItem, "EDU_OBJECT", "EDU_OBJECT") + ", ";
            sql +=       string.Format(sqlItem, "EDU_ITEM", "EDU_ITEM") + ", ";
            sql +=       string.Format(sqlItem, "ITEM_TYPE", "ITEM_TYPE") + ", ";            
            sql +=       string.Format(sqlItem, "PRECONDITION", "PRECONDITION") + ", ";
            sql +=       string.Format(sqlItem, "EDU_METHOD", "EDU_METHOD") + ", ";
            sql +=       string.Format(sqlItem, "MASTERED_DEGREE", "MASTERED_DEGREE") + ", ";
            sql +=       string.Format(sqlItem, "EDU_CLOG", "EDU_CLOG") + ", ";
            sql +=       string.Format(sqlItem, "MEMO", "MEMO");
            
            sql += "FROM HIS_DICT_ITEM A ";
            sql += "WHERE A.DICT_ID = '07' ";
            sql += "ORDER BY A.ITEM_ID ";
            
            DataSet ds = _connection.SelectData(sql, "HEALTH_EDU_REC");
            
            // 数据处理
            string itemId   = string.Empty;
            string itemType = string.Empty;
            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                
                // 获取类别
                if (itemId.Length == 0)
                {
                    itemId      = dr["ITEM_ID"].ToString().Trim();
                    itemType    = dr["ITEM_NAME"].ToString();
                    
                    dr.Delete();
                    
                    continue;
                }
                
                if (itemId.Length == dr["ITEM_ID"].ToString().Trim().Length)
                {
                    itemId      = dr["ITEM_ID"].ToString().Trim();
                    itemType    = dr["ITEM_NAME"].ToString();
                    
                    dr.Delete();
                    
                    continue;
                }
                
                // 更新属性
                if (dr["ITEM_ID"].ToString().StartsWith(itemId) == true)
                {
                    if (dr["ITEM_TYPE"].ToString().Length == 0)
                    {
                        dr["ITEM_TYPE"] = itemType;
                    }
                }
            }
            
            ds.AcceptChanges();
            
            return ds;
        }
        
        
        /// <summary>
        /// 保存健康教育记录
        /// </summary>
        /// <returns></returns>
        public bool SaveHealEduRec(DataSet dsChanged, string deptCode, string patientId, string visitId)
        {
            if (dsChanged == null || dsChanged.Tables.Count == 0 
                || dsChanged.Tables[0].Rows.Count == 0)
            {
                return true;
            }
            
            // 获取健康教育记录
            StringBuilder sb = new StringBuilder(); 
            sb.Append("SELECT * FROM HEALTH_EDU_REC "                             );
            sb.Append("WHERE PATIENT_ID = " + SQL.SqlConvert(patientId)           );
            sb.Append(    "AND VISIT_ID = " + SQL.SqlConvert(visitId)             );
            sb.Append(    "AND DEPT_CODE = " + SQL.SqlConvert(deptCode)           );
            sb.Append("ORDER BY ITEM_ID "                                         );
            
            DataSet dsRec = _connection.SelectData(sb.ToString(), "HEALTH_EDU_REC");            
            
            // 保存 到缓存
            DataRow[] drFind = null;
            DataRow drEdit = null;
            foreach(DataRow dr in dsChanged.Tables[0].Rows)
            {
                // 获取记录
                string itemId = dr["ITEM_ID"].ToString();
                string filter = "ITEM_ID = " + SQL.SqlConvert(itemId);
                
                drFind = dsRec.Tables[0].Select(filter);
                if (drFind.Length == 0)
                {
                    drEdit = dsRec.Tables[0].NewRow();
                }
                else
                {
                    drEdit = drFind[0];
                }
                
                // 赋值
                drEdit["DEPT_CODE"]     = deptCode;
                drEdit["PATIENT_ID"]    = patientId;
                drEdit["VISIT_ID"]      = visitId;
                drEdit["ITEM_ID"]       = itemId;
                drEdit["EDU_DATE"]      = dr["EDU_DATE"];
                drEdit["ITEM_TYPE"]     = dr["ITEM_TYPE"];
                drEdit["EDU_ITEM"]      = dr["ITEM_NAME"];
                drEdit["EDU_OBJECT"]    = dr["EDU_OBJECT"];
                drEdit["EDU_NURSE"]     = dr["EDU_NURSE"];
                
                drEdit["PRECONDITION"]  = dr["PRECONDITION"];
                drEdit["EDU_METHOD"]    = dr["EDU_METHOD"];
                drEdit["MASTERED_DEGREE"] = dr["MASTERED_DEGREE"];
                drEdit["EDU_CLOG"]      = dr["EDU_CLOG"];
                drEdit["MEMO"]          = dr["MEMO"];
                
                if (drFind.Length == 0)
                {
                    dsRec.Tables[0].Rows.Add(drEdit);
                }
            }
            
            // 保存到DB
            _connection.Update(ref dsRec, "HEALTH_EDU_REC", sb.ToString());
            
            return true;
        }
    }
}
