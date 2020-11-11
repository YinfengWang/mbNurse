//------------------------------------------------------------------------------------
//  ϵͳ����        : ͨ��ģ��
//  ��ϵͳ����      : 
//  ��������        : 
//  ����            : OracleAccess.cs
//  ���ܸ�Ҫ        : ����Oracle���ݿ�Ľӿ�
//  ������          : ����
//  ������          : 2007-01-17
//  �汾            : 1.0.0.0
// 
//------------------< �����ʷ >------------------------------------------------------
//  �������        :
//  �����          :
//  �������        :
//  �汾            :
//------------------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Data.Common;
using System.Collections;

using DBAdapter = System.Data.SqlServerCe.SqlCeDataAdapter;
using DBCommand = System.Data.SqlServerCe.SqlCeCommand;
using DBCmdBuilder = System.Data.SqlServerCe.SqlCeCommandBuilder;
using SQL = HISPlus.SqlManager;

namespace HISPlus
{
	/// <summary>
	/// OracleAccess ��ժҪ˵����
	/// </summary>
	public class SqlceAccess: DbAccess
    {
        public SqlceAccess()
		{
            _connection = new SqlCeConnection();
		}


        public SqlceAccess(string connectString)
		{
            _connection = new SqlCeConnection(connectString);
		}


        #region ����
        /// <summary>
		/// ��ѯ����
		/// </summary>
		/// <param name="sqlSel">��ѯ���</param>
		/// <param name="tableName">����</param>
		/// <returns>��ѯ���DataSet</returns>
		public override void SelectData(string sqlSel, string tableName, ref DataSet ds, bool blnWithKey)
		{
			// ���ԭ��������
            if (ds.Tables.IndexOf(tableName) >= 0)
            {
                ds.Tables.Remove(tableName);
            }
            
			// ��ѯ����
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
		/// ����DB
		/// </summary>
		/// <param name="ds">����ԴDataSet</param>
		/// <param name="tableName">Ҫ���µı���</param>
		/// <param name="sqlSel">��ȡ����Դ��Sql���</param>
		/// <param name="conn">���ݿ�����</param>
		/// <returns>TRUE: ����ɹ�; FALSE: ����ʧ��</returns>
		public override void Update(ref DataSet ds, string tableName, string sqlSel)
		{
			bool blnInTrans		= (_trans != null);
            
            try
            {
                // �����������û�д�
                if (this.IsConnected == false)
                {
                    Connect();
                }

                // ���û������
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
                    adapter.AcceptChangesDuringUpdate = true;
                    DBCmdBuilder cmdBuilder = new DBCmdBuilder(adapter);
                    
                    if (tableName.Length == 0)
                    {
                        adapter.Update(ds);
                    }
                    else
                    {
                        adapter.Update(ds, tableName);
                    }

                    if (blnInTrans == false)
                    {
                        Commit();
                    }

                    ds.AcceptChanges();
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
        /// ����DB
        /// </summary>
        /// <param name="dataRows"></param>
        /// <param name="tableName"></param>
        /// <param name="sqlSel"></param>
        public override void Update(ref DataRow[] dataRows, string sqlSel)
        { 
            bool blnInTrans		= (_trans != null);
            
            try
            {
                // �����������û�д�
                if (this.IsConnected == false)
                {
                    Connect();
                }

                // ���û������
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
                    adapter.AcceptChangesDuringUpdate = true;
                    DBCmdBuilder cmdBuilder = new DBCmdBuilder(adapter);

                    adapter.Update(dataRows);

                    if (blnInTrans == false)
                    {
                        Commit();
                    }
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
        /// ������DataSet �������ݱ�
        /// </summary>
        /// <param name="dsChanged"></param>
        public override void Update(ref DataSet dsChanged)
        {
            // ��������
            DataColumn[] dcPrimary = new DataColumn[dsChanged.Tables[0].PrimaryKey.Length];
            for(int c = 0; c < dcPrimary.Length; c++)
            {
                dcPrimary[c] = dsChanged.Tables[0].PrimaryKey[c];
            }
            
            // �������
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
                        filter += SQL.SqlConvert(((DateTime)(dr[dc.ColumnName])).ToString(ComConst.FMT_DATE.LONG));    
                    }
                    else
                    {
                        filter += SQL.SqlConvert(dr[dc.ColumnName].ToString());
                    }
                    
                    blnFirst = false;
                }
                
                if (blnDel) dr.Delete();
                
                // �����ɾ��
                if (dr.RowState == DataRowState.Deleted)
                {
                    sql = "DELETE FROM " + tableName + " WHERE " + filter;
                    arrSql.Add(sql);
                    continue;
                }
                
                // ���������
                if (dr.RowState == DataRowState.Added)
                {
                    string colList = string.Empty;
                    string valList = string.Empty;
                    
                    blnFirst = true;
                    
                    foreach(DataColumn dc1 in dsChanged.Tables[0].Columns)
                    {
                        if (blnFirst == false) 
                        {
                            colList += ",";
                            valList += ",";
                        }
                        
                        colList += dc1.ColumnName;
                        
                        if (dc1.DataType == dateTime)
                        {
                            if (dr[dc1.ColumnName] == DBNull.Value)
                            {
                                valList += " NULL ";
                            }
                            else
                            {
                                valList += SQL.SqlConvert(((DateTime)(dr[dc1.ColumnName])).ToString(ComConst.FMT_DATE.LONG));
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
                
                // ������޸�
                if (dr.RowState == DataRowState.Modified)
                {
                    string valPair = string.Empty;
                    
                    blnFirst = true;
                    foreach(DataColumn dc2 in dsChanged.Tables[0].Columns)
                    {
                        if (blnFirst == false) 
                        {
                            valPair += ",";
                        }
                        
                        valPair += dc2.ColumnName + " = ";
                        
                        if (dc2.DataType == dateTime)
                        {
                            if (dr[dc2.ColumnName] == DBNull.Value)
                            {
                                valPair += " NULL ";
                            }
                            else
                            {
                                valPair += SQL.SqlConvert(((DateTime)(dr[dc2.ColumnName])).ToString(ComConst.FMT_DATE.LONG));
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
            
            // �ػ�����
            dsChanged.Tables[0].PrimaryKey = dcPrimary;
            
            ExecuteNoQuery(ref arrSql);
            
            dsChanged.AcceptChanges();
        }
		#endregion
    }
}
