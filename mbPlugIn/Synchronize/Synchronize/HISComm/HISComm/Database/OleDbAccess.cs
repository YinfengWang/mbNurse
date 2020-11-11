//------------------------------------------------------------------------------------
//  系统名称        : 通用模块
//  子系统名称      : 
//  对象类型        : 
//  类名            : OracleAccess.cs
//  功能概要        : 访问Oracle数据库的接口
//  作成者          : 付军
//  作成日          : 2007-01-17
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        :
//  变更者          :
//  变更内容        :
//  版本            :
//------------------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;
using System.Collections;

using DBAdapter     = System.Data.OleDb.OleDbDataAdapter;
using DBCommand     = System.Data.OleDb.OleDbCommand;
using DBCmdBuilder  = System.Data.OleDb.OleDbCommandBuilder;

using SQL           = HISPlus.SqlManager;

namespace HISPlus
{
	/// <summary>
	/// OracleAccess 的摘要说明。
	/// </summary>
	public class OleDbAccess: DbAccess
    {
        public OleDbAccess()
		{
            _connection = new OleDbConnection();
		}


        public OleDbAccess(string connectString)
		{
            _connection = new OleDbConnection(connectString);
		}
        

        #region 方法
        /// <summary>
		/// 查询数据
		/// </summary>
		/// <param name="sqlSel">查询语句</param>
		/// <param name="tableName">表名</param>
		/// <returns>查询结果DataSet</returns>
		public override void SelectData(string sqlSel, string tableName, ref DataSet ds, bool blnWithKey)
		{
			// 清除原来的数据
            if (ds.Tables.IndexOf(tableName) >= 0)
            {
                ds.Tables.Remove(tableName);
            }
            
			// 查询数据
			try
			{
				Connect();
				
                IDbCommand cmd  = _connection.CreateCommand();
                cmd.CommandText = sqlSel;
                cmd.Connection  = _connection;
                
                DBAdapter adapter = new DBAdapter((DBCommand)cmd);

				if (blnWithKey == true)
                {
                    adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                }

				adapter.Fill(ds, tableName);
			}
			finally
			{
                DisConnect();
			}
		}
		
        
		/// <summary>
		/// 更新DB
		/// </summary>
		/// <param name="ds">数据源DataSet</param>
		/// <param name="tableName">要更新的表名</param>
		/// <param name="sqlSel">获取数据源的Sql语句</param>
		/// <param name="conn">数据库连接</param>
		/// <returns>TRUE: 保存成功; FALSE: 保存失败</returns>
		public override int Update(ref DataSet ds, string tableName, string sqlSel)
		{
			bool blnInTrans		= (_trans != null);
            
            if (sqlSel.Length == 0)
            {
                sqlSel = "SELECT * FROM " + tableName;
            }
            
            try
            {
                // 如果数据连接没有打开
                if (this.IsConnected == false)
                {
                    Connect();
                }

                // 如果没有事务
                if (blnInTrans == false)
                {
                    BeginTrans();
                }

                try
                {
                    IDbCommand cmd  = _connection.CreateCommand();
                    cmd.CommandText = sqlSel;
                    cmd.Connection  = _connection;
                    cmd.Transaction = _trans;

                    DBAdapter adapter = new DBAdapter((DBCommand)cmd);
                    
                    DBCmdBuilder cmdBuilder = new DBCmdBuilder(adapter);
                    cmdBuilder.ConflictOption = ConflictOption.OverwriteChanges;
                    
                    int  result = 0;
                    
                    if (tableName.Length == 0)
                    {
                        result = adapter.Update(ds);
                    }
                    else
                    {
                        result = adapter.Update(ds, tableName);
                    }

                    if (blnInTrans == false)
                    {
                        Commit();
                    }
                    
                    ds.AcceptChanges();
                    
                    return result;
                }
                catch (Exception ex)
                {
                    RollBack();
                    throw ex;
                }
            }
			finally
			{
				if (blnInTrans == false)
				{
                    DisConnect();
				}
			}
		}

        
        /// <summary>
        /// 更新DB
        /// </summary>
        /// <param name="dataRows"></param>
        /// <param name="tableName"></param>
        /// <param name="sqlSel"></param>
        public override int Update(ref DataRow[] dataRows, string sqlSel)
        { 
            bool blnInTrans		= (_trans != null);
            
            try
            {
                // 如果数据连接没有打开
                if (this.IsConnected == false)
                {
                    Connect();
                }

                // 如果没有事务
                if (blnInTrans == false)
                {
                    BeginTrans();
                }

                try
                {
                    IDbCommand cmd  = _connection.CreateCommand();
                    cmd.CommandText = sqlSel;
                    cmd.Connection  = _connection;
                    cmd.Transaction = _trans;

                    DBAdapter adapter = new DBAdapter((DBCommand)cmd);
                    DBCmdBuilder cmdBuilder = new DBCmdBuilder(adapter);
                    cmdBuilder.ConflictOption = ConflictOption.OverwriteChanges;
                    
                    int  result     = adapter.Update(dataRows);

                    if (blnInTrans == false)
                    {
                        Commit();
                    }
                    
                    return result;
                }
                catch (Exception ex)
                {
                    RollBack();
                    throw ex;
                }
            }
			finally
			{
				if (blnInTrans == false)
				{
                    DisConnect();
				}
			}
        }
        
        
        /// <summary>
        /// 仅根据DataSet 更新数据表
        /// </summary>
        /// <param name="dsChanged"></param>
        public override int Update(ref DataSet dsChanged)
        {
            return Update(ref dsChanged, dsChanged.Tables[0].TableName, string.Empty);
            
            // 缓存主键
            DataColumn[] dcPrimary = new DataColumn[dsChanged.Tables[0].PrimaryKey.Length];
            for(int c = 0; c < dcPrimary.Length; c++)
            {
                dcPrimary[c] = dsChanged.Tables[0].PrimaryKey[c];
            }
            
            // 清除主键
            dsChanged.Tables[0].PrimaryKey = null;
            
            Type dateTime = Type.GetType("System.DateTime");
            
            DataColumn dc = null;
            
            string tableName = dsChanged.Tables[0].TableName;
            
            ArrayList arrSql = new ArrayList();
            foreach(DataRow dr in dsChanged.Tables[0].Rows)
            {
                if (dr.RowState == DataRowState.Unchanged || dr.RowState == DataRowState.Detached)
                {
                    continue;
                }
                
                string  sql         = string.Empty;
                string  filter      = string.Empty;
                bool    blnFirst    = true;                
                
                dc = null;
                                
                bool blnDel = (dr.RowState == DataRowState.Deleted);
                if (blnDel) dr.RejectChanges();
                
                for(int i = 0; i < dcPrimary.Length; i++)
                {
                    dc = dcPrimary[i];
                    
                    if (blnFirst == false) filter += " AND ";
                    
                    filter += dc.ColumnName + " = ";
                    
                    if (dc.DataType == dateTime)
                    {
                        filter += SQL.GetOraDbDate((DateTime)(dr[dc.ColumnName]));
                    }
                    else
                    {
                        filter += SQL.SqlConvert(dr[dc.ColumnName].ToString());
                    }
                    
                    blnFirst = false;
                }
                
                if (blnDel) dr.Delete();
                
                // 如果是删除
                if (dr.RowState == DataRowState.Deleted)
                {
                    sql = "DELETE FROM " + tableName + " WHERE " + filter;
                    arrSql.Add(sql);
                    continue;
                }
                
                // 如果是新增
                if (dr.RowState == DataRowState.Added)
                {
                    string colList = string.Empty;
                    string valList = string.Empty;
                    
                    blnFirst = true;
                    
                    foreach(DataColumn dc1 in dsChanged.Tables[0].Columns)
                    {
                        if (dc1.ColumnName.ToUpper().EndsWith("TIMESTAMP") == true)
                        {
                            continue;
                        }
                        
                        if (blnFirst == false) 
                        {
                            colList += ",";
                            valList += ",";
                        }
                        
                        colList += dc1.ColumnName;
                        
                        if (dc1.DataType == dateTime)
                        {
                            if (dr[dc1.ColumnName] == DBNull.Value || dr[dc1.ColumnName].ToString().Length == 0)
                            {
                                valList += " NULL ";
                            }
                            else
                            {
                                valList += SQL.GetOraDbDate((DateTime)(dr[dc1.ColumnName]));
                            }
                        }
                        else
                        {
                            valList += SQL.SqlConvert(dr[dc1.ColumnName].ToString());
                        }
                        
                        blnFirst = false;
                    }
                    
                    sql = "INSERT INTO " + tableName + " (" + colList + ") VALUES (" + valList + ")";
                    arrSql.Add(sql);
                    
                    continue;
                }
                
                // 如果是修改
                if (dr.RowState == DataRowState.Modified)
                {
                    string valPair = string.Empty;
                    
                    blnFirst = true;
                    foreach(DataColumn dc2 in dsChanged.Tables[0].Columns)
                    {
                        if (dc2.ColumnName.ToUpper().EndsWith("TIMESTAMP") == true)
                        {
                            continue;
                        }
                        
                        if (blnFirst == false) 
                        {
                            valPair += ",";
                        }
                        
                        valPair += dc2.ColumnName + " = ";
                        
                        if (dc2.DataType == dateTime)
                        {
                            if (dr[dc2.ColumnName] == DBNull.Value || dr[dc2.ColumnName].ToString().Length == 0)
                            {
                                valPair += " NULL ";
                            }
                            else
                            {
                                valPair += SQL.GetOraDbDate((DateTime)(dr[dc2.ColumnName]));
                            }
                        }
                        else
                        {
                            valPair += SQL.SqlConvert(dr[dc2.ColumnName].ToString());
                        }
                        
                        blnFirst = false;
                    }
                    
                    sql = "UPDATE " + tableName + " SET ";
                    sql += valPair;
                    sql += " WHERE " + filter;
                    
                    arrSql.Add(sql);
                    
                    continue;
                }
            }
            
            // 回恢主键
            dsChanged.Tables[0].PrimaryKey = dcPrimary;
            
            ExecuteNoQuery(ref arrSql);
            
            dsChanged.AcceptChanges();
            
            return arrSql.Count;
        }                        
		#endregion


        #region 数据库信息获取
        /// <summary>
        /// 获取系统日期
        /// </summary>
        /// <returns></returns>
        public override DateTime GetSysDate()
        {
            string sql = "SELECT SYSDATE FROM DUAL";
                        
            if (SelectValue(sql) == true)
            {
                return DateTime.Parse(GetResult(0));
            }
            else
            {
                throw new Exception("获取系统日期失败!");
            }
        }
        #endregion
    }
}
