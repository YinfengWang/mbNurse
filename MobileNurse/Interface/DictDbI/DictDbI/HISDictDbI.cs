using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public class HISDictDbI
    {
        #region 变量
        protected DbAccess _connection;
        #endregion

        public HISDictDbI(DbAccess conn)
        {
            _connection = conn;
        }

        
        /// <summary>
        /// 获取字典分组
        /// </summary>
        /// <returns></returns>
        public DataSet GetDictGroup()
        {
            string sql = "SELECT * FROM HIS_DICT_GROUP ORDER BY NODE_ID";
            return _connection.SelectData(sql, "HIS_DICT_GROUP");
        }
        
        
        /// <summary>
        /// 保存字典分组信息
        /// </summary>
        /// <param name="dsChanged"></param>
        /// <returns></returns>
        public bool SaveDictGroupInfo(DataSet dsChanged)
        {
            string sql = "SELECT * FROM HIS_DICT_GROUP";
            _connection.Update(ref dsChanged, "HIS_DICT_GROUP", sql);
            
            return true;
        }
        
        
        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetDictList()
        {
            string sql = "SELECT * FROM HIS_DICT ORDER BY DICT_ID";
            return _connection.SelectData(sql, "HIS_DICT");
        }
        
        
        /// <summary>
        /// 保存字典信息
        /// </summary>
        /// <param name="dsChanged"></param>
        /// <returns></returns>
        public bool SaveDictInfo(DataSet dsChanged)
        {
            _connection.Update(ref dsChanged);
            //string sql = "SELECT * FROM HIS_DICT";
            //_connection.Update(ref dsChanged, "HIS_DICT", sql);
            
            return true;
        }
        
        
        /// <summary>
        /// 获取字典项目
        /// </summary>
        /// <param name="dictId"></param>
        /// <returns></returns>
        public DataSet GetDictItem(string dictId)
        {
            if (dictId.Trim().Length == 0)
            {
                return null;
            }
            
            string sql = "SELECT * FROM HIS_DICT_ITEM WHERE DICT_ID = " + SqlManager.SqlConvert(dictId)
                        + "ORDER BY ITEM_ID";
            
            return _connection.SelectData(sql, "HIS_DICT_ITEM");
        }
        
        
        /// <summary>
        /// 保存字典项目
        /// </summary>
        /// <param name="dsChanged"></param>
        /// <param name="dictId"></param>
        /// <returns></returns>
        public bool SaveDictItem(DataSet dsChanged, string dictId)
        {
            _connection.Update(ref dsChanged);
            //string sql = "SELECT * FROM HIS_DICT_ITEM WHERE DICT_ID = " + SqlManager.SqlConvert(dictId)
            //            + "ORDER BY ITEM_ID";
            
            //_connection.Update(ref dsChanged, "HIS_DICT_ITEM", sql);
            
            return true;
        }
        
        
        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="dictId"></param>
        /// <returns></returns>
        public bool DeleteDict(string dictId)
        {
            System.Collections.ArrayList arrSql = new System.Collections.ArrayList();
            
            arrSql.Add("DELETE FROM HIS_DICT_GROUP WHERE DICT_ID = " + SqlManager.SqlConvert(dictId));
            arrSql.Add("DELETE FROM HIS_DICT WHERE DICT_ID = " + SqlManager.SqlConvert(dictId));
            
            _connection.ExecuteNoQuery(ref arrSql);
            
            return true;
        }
        
        
        /// <summary>
        /// 移除字典
        /// </summary>
        /// <param name="dictId"></param>
        /// <returns></returns>
        public bool RemoveDictFromGroup(string nodeId)
        {
            string sql = "DELETE FROM HIS_DICT_GROUP "
                       + "WHERE NODE_ID = " + SqlManager.SqlConvert(nodeId);
            
            _connection.ExecuteNoQuery(sql);
            
            return true;
        }        
        
        
        /// <summary>
        /// 获取字典项目内容
        /// </summary>
        /// <param name="dictId"></param>
        /// <returns></returns>
        public DataSet GetDictItemContent(string dictId, string deptCode)
        {
            string sql = "SELECT * FROM HIS_DICT_ITEM_CONTENT "
                        + "WHERE DICT_ID = " + SqlManager.SqlConvert(dictId);
            if (deptCode.Length > 0)
            {
                sql += "AND DEPT_CODE = " + SqlManager.SqlConvert(deptCode);
            }
            
            return _connection.SelectData(sql, "HIS_DICT_ITEM_CONTENT");
        }
                
        
        /// <summary>
        /// 获取字典项目内容
        /// </summary>
        /// <param name="dictId"></param>
        /// <returns></returns>
        public string GetDictItemContent(string dictId, string itemId, string deptCode)
        {
            string sql = "SELECT CONTENT FROM HIS_DICT_ITEM_CONTENT "
                        + "WHERE DICT_ID = " + SqlManager.SqlConvert(dictId)
                        +   "AND ITEM_ID = " + SqlManager.SqlConvert(itemId);
            if (deptCode.Length > 0)
            {
                sql += "AND DEPT_CODE = " + SqlManager.SqlConvert(deptCode);
            }
            
            if (_connection.SelectValue(sql) == true)
            {
                return _connection.GetResult(0);
            }
            else
            {
                return string.Empty;
            }
        }
        
        
        /// <summary>
        /// 保存字典项目内容
        /// </summary>
        /// <param name="dsChanged"></param>
        /// <param name="dictId"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public bool SaveDictItemContent(DataSet dsChanged, string dictId, string deptCode)
        {
            string sql = "SELECT * FROM HIS_DICT_ITEM_CONTENT "
                        + "WHERE DICT_ID = " + SqlManager.SqlConvert(dictId);
            
            if (deptCode.Length > 0)
            {
                sql += "AND DEPT_CODE = " + SqlManager.SqlConvert(deptCode);
            }
            
            _connection.Update(ref dsChanged, "HIS_DICT_ITEM_CONTENT", sql);
            
            return true;
        }
        
        
        /// <summary>
        /// 获取PIO记录
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public DataSet GetPIORec(string patientId, string visitId, string dictId)
        {
            string sql = "SELECT * FROM NURSING_PIO_REC "
                        + "WHERE PATIENT_ID = " + SQL.SqlConvert(patientId)
                        +       "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                        +       "AND DICT_ID = " + SQL.SqlConvert(dictId);
            
            return _connection.SelectData(sql, "NURSING_PIO_REC");
        }
        
        
        /// <summary>
        /// 获取PIO记录
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public DataSet GetPIORec(string patientId, string visitId, string dictId, DateTime dtRngStart, DateTime dtRngEnd)
        {
            string sql = "SELECT * FROM NURSING_PIO_REC "
                        + "WHERE PATIENT_ID = " + SQL.SqlConvert(patientId)
                        +       "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                        +       "AND DICT_ID = " + SQL.SqlConvert(dictId)
                        +       "AND START_DATE >= " + SQL.GetOraDbDate_Short(dtRngStart)
                        +       "AND START_DATE < " + SQL.GetOraDbDate_Short(dtRngEnd.AddDays(1));
            
            return _connection.SelectData(sql, "NURSING_PIO_REC");
        }
        
        
        /// <summary>
        /// 保存PIO记录
        /// </summary>
        /// <param name="dsChanged"></param>
        /// <returns></returns>
        public bool SavePIORec(DataSet dsChanged)
        {
            _connection.Update(ref dsChanged);
            return true;
        }
    }
}
