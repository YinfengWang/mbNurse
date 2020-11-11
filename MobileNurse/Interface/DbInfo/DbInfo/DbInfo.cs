using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public class DbInfo: MarshalByRefObject
    {
        #region 变量
        protected DbAccess _connection;
        #endregion
        
        public DbInfo(DbAccess conn)
        {
            _connection = conn;
        }
        
        
        /// <summary>
        /// 获取数据库日期
        /// </summary>
        /// <returns></returns>
        public DateTime GetSysDate()
        {
            return _connection.GetSysDate();
        }
        
        
        /// <summary>
        /// 获取sql语句的返回值, 如果成功, 应用GetResult方法获取返回值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool SelectValue(string sql)
        {
            return _connection.SelectValue(sql);
        }
        
        
        /// <summary>
        /// 获取返回值
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public string GetResult(int col)
        {
            return _connection.GetResult(col);
        }
        
        
        
        /// <summary>
        /// 单值查询
        /// </summary>
        /// <returns></returns>
        public string GetValueBySql(string sql)
        {
            if (_connection.SelectValue(sql) == true)
            {
                return _connection.GetResult(0);
            }
            else
            {
                return null;
            }
        }
        
        
        /// <summary>
        /// 通过SQL语句获取数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetData(string sql, string tableName)
        {
            return _connection.SelectData_NoKey(sql, tableName);
        }
        
        
        /// <summary>
        /// 通过SQL语句获取数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetTableDataBySQL(string sql, string tableName)
        {
            return _connection.SelectData(sql, tableName);
        }
        
                
        /// <summary>
        /// 获取表中数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public DataSet GetTableData(string tableName, string filter)
        {
            string sql = "SELECT * FROM " + tableName;
            
            if (filter.Trim().Length > 0)
            {
                sql += " WHERE " + filter;
            }
            
            return _connection.SelectData(sql, tableName);
        }

  
        
        /// <summary>
        /// 获取表字段的备注
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet GetTableColComment(string tableName)
        {
            string sql = "SELECT COLUMN_NAME,COMMENTS FROM USER_COL_COMMENTS "
                        + "WHERE TABLE_NAME=" + SQL.SqlConvert(tableName.ToUpper());
            
            return _connection.SelectData(sql, tableName);
        }
        
        
        /// <summary>
        /// 保存表中数据
        /// </summary>
        /// <param name="dsChanged"></param>
        /// <returns></returns>
        public bool SaveTableData(DataSet dsChanged)
        {
            _connection.Update(ref dsChanged, dsChanged.Tables[0].TableName, string.Empty);
            dsChanged.AcceptChanges();
            return true;
        }
        
        
        /// <summary>
        /// 多表保存
        /// </summary>
        /// <param name="dsChanged1"></param>
        /// <param name="dsChanged2"></param>
        /// <param name="dsChanged3"></param>
        /// <returns></returns>
        public bool SaveTablesData(DataSet dsChanged1, DataSet dsChanged2, DataSet dsChanged3)
        {
            _connection.BeginTrans();
            
            try
            {
                if (dsChanged1 != null && dsChanged1.HasChanges())
                {
                    _connection.Update(ref dsChanged1, dsChanged1.Tables[0].TableName, string.Empty);
                }
                
                if (dsChanged2 != null && dsChanged2.HasChanges())
                {
                    _connection.Update(ref dsChanged2, dsChanged2.Tables[0].TableName, string.Empty);
                }
                
                if (dsChanged3 != null && dsChanged3.HasChanges())
                {
                    _connection.Update(ref dsChanged3, dsChanged3.Tables[0].TableName, string.Empty);
                }
                
                _connection.Commit();
                
                return true;
            }
            catch(Exception ex)
            {
                _connection.RollBack();
                
                throw ex;
            }
        }
        
        
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="arrDataSet">由DataSet 组成的数组</param>
        /// <returns>TRUE: 成功; FALSE: 失败</returns>
        public bool SaveTableData(ArrayList arrDataSet)
        {
            _connection.BeginTrans();
            
            try
            {
                for(int i = 0; i < arrDataSet.Count; i++)
                {
                    DataSet ds = arrDataSet[i] as DataSet;
                    
                    if (ds != null && ds.HasChanges())
                    {
                        _connection.Update(ref ds, ds.Tables[0].TableName, string.Empty);
                    }
                }
                
                _connection.Commit();
                
                return true;
            }
            catch(Exception ex)
            {
                _connection.RollBack();
                
                throw ex;
            }
        }
        
        
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="arrSql"></param>
        /// <param name="dataSets"></param>
        /// <returns></returns>
        public bool SaveData(ArrayList arrSql, object[] arrDataSet)
        {
            _connection.BeginTrans();
            
            try
            {
                // SQL语句
                if (arrSql != null && arrSql.Count > 0)
                {
                    _connection.ExecuteNoQuery(ref arrSql);
                }
                
                // DataSet 
                if (arrDataSet != null)
                {
                    DataSet ds = null;
                    for(int i = 0; i < arrDataSet.Length; i++)
                    {
                        ds = arrDataSet[i] as DataSet;
                        
                        if (ds != null && ds.HasChanges())
                        {
                            _connection.Update(ref ds, ds.Tables[0].TableName, string.Empty);
                        }
                    }
                }
                
                _connection.Commit();
                
                return true;
            }
            catch(Exception ex)
            {
                _connection.RollBack();
                
                throw ex;
            }
        }
        
        
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <returns></returns>
        public bool ExecuteSql(string sql)
        {
            _connection.ExecuteNoQuery(sql);
            
            return true;            
        }
    }
}
