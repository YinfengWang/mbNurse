//------------------------------------------------------------------------------------
//  类名            : DbAccess.cs
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
using System.Data.Common;
using System.Collections;
using System.IO;

namespace HISPlus
{
    /// <summary>
    /// OracleAccess 的摘要说明。
    /// </summary>
    public class DbAccess
    {
        #region 变量
        protected const int _MAX_COLS = 10;                                   // 单行选择时默认最大选10列

        public bool Logging = false;                                // 是否Log SQL

        protected string _connectStr = string.Empty;                         // 连接字符串
        protected IDbConnection _connection = null;                                 // 数据库连接对象        		
        protected IDbTransaction _trans = null;

        protected object[] _arrSelResult = new object[_MAX_COLS];                // 默认为10个字段

        private readonly string _STR_NO_CONNECTION = "未指定连接";
        private readonly string _STR_INDEX_OUT_RNG = "索引超出范围";
        #endregion


        public DbAccess()
        {
        }


        public DbAccess(IDbConnection connect)
        {
            if (connect == null)
            {
                throw new Exception(_STR_NO_CONNECTION);
            }

            _connectStr = connect.ConnectionString;
            _connection = connect;
        }


        #region 属性
        /// <summary>
        /// 与数据库的连接
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                return _connection;
            }
            set
            {
                _connectStr = value.ConnectionString;
                _connection = value;
            }
        }


        /// <summary>
        /// 事务
        /// </summary>
        public IDbTransaction Trans
        {
            get
            {
                return _trans;
            }
            set
            {
                _trans = value;

                if (_trans != null)
                {
                    _connection = _trans.Connection;
                }
            }
        }


        /// <summary>
        /// 是否与数据库连接
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return (_connection != null
                     && _connection.State != ConnectionState.Broken
                     && _connection.State != ConnectionState.Closed
                     && _connection.State != ConnectionState.Connecting);
            }
        }


        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return _connectStr;
            }
            set
            {
                _connectStr = value;
                _connection.ConnectionString = _connectStr;
            }
        }
        #endregion


        #region 接口
        /// <summary>
        /// Oracle数据库的连接
        /// </summary>
        /// <returns>返回连接</returns>
        public void Connect()
        {
            // 连接实例是否存在
            assertConnection();

            if (this.IsConnected == false)
            {
                _trans = null;

                if (_connectStr.Length > 0)
                {
                    _connection.ConnectionString = _connectStr;
                }

                _connection.Open();
            }
        }


        /// <summary>
        /// Oracle数据库的连接
        /// </summary>
        /// <param name="connectString">连接字符串</param>
        /// <returns>返回连接</returns>
        public void Connect(string connectString)
        {
            // 连接实例是否存在
            assertConnection();

            // 如果已经连接
            if (this.IsConnected == true)
            {
                // 如果连接字符串与指定的连接字符串相同, 不处理
                if (_connection.ConnectionString.Equals(connectString) == true)
                {
                    return;
                }
                else
                {
                    // 如果有事务
                    if (_trans != null)
                    {
                        _trans = null;
                    }

                    // 关闭连接
                    _connection.Close();
                }
            }

            // 开始新连接
            _connection.ConnectionString = connectString;

            _connection.Open();
        }


        /// <summary>
        /// 断开与数据库的连接
        /// </summary>
        public void DisConnect()
        {
            if (this.IsConnected == true)
            {
                // 如果有事务
                _trans = null;

                _connection.Close();
            }
        }


        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns>TRUE: 成功; FALSE: 失败</returns>
        public IDbTransaction BeginTrans()
        {
            // 连接实例是否存在
            assertConnection();

            if (this.IsConnected == false)
            {
                Connect();
            }

            if (_trans != null)
            {
                _trans = null;
            }

            _trans = _connection.BeginTransaction();

            return _trans;
        }


        /// <summary>
        /// 事务提交
        /// </summary>
        public void Commit()
        {
            if (_trans != null)
            {
                _trans.Commit();
                _trans = null;
            }

            this.DisConnect();
        }


        /// <summary>
        /// 事务回滚
        /// </summary>
        public void RollBack()
        {
            if (_trans != null)
            {
                _trans = null;
            }

            this.DisConnect();
        }


        /// <summary>
        /// 查询数据, 只查询第一行的第一列, 最好是返回结果只有一个的
        /// </summary>
        /// <param name="sqlSel">查询语句</param>
        /// <returns>TRUE: 查找; FALSE: 没有查找</returns>
        public bool SelectValue(string sqlSel)
        {
            // 查询数据
            try
            {
                Connect();

                IDbCommand cmd = _connection.CreateCommand();
                cmd.CommandText = sqlSel;

                IDataReader reader = cmd.ExecuteReader();

                try
                {
                    if (reader.Read())
                    {
                        reader.GetValues(_arrSelResult);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            finally
            {
                DisConnect();

                if (Logging) LogFile.WriteLog(sqlSel);
            }
        }


        /// <summary>
        /// 返回单行选择的结果
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public string GetResult(int col)
        {
            if (col < 0 || col >= _MAX_COLS)
            {
                throw new Exception(_STR_INDEX_OUT_RNG);
            }

            if (_arrSelResult[col] == DBNull.Value)
            {
                return string.Empty;
            }
            else
            {
                return _arrSelResult[col].ToString();
            }
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sqlSel">查询语句</param>
        /// <returns>查询结果DataSet</returns>
        public DataSet SelectData(string sqlSel)
        {
            // 准备DataSet
            DataSet dsData = new DataSet();

            SelectData(sqlSel, "Table1", ref dsData, true);

            return dsData;
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sqlSel">查询语句</param>
        /// <returns>查询结果DataSet</returns>
        public DataSet SelectData_NoKey(string sqlSel)
        {
            // 准备DataSet
            DataSet dsData = new DataSet();

            SelectData(sqlSel, "Table1", ref dsData, false);

            return dsData;
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sqlSel">查询语句</param>
        /// <param name="tableName">表名</param>
        /// <returns>查询结果DataSet</returns>
        public DataSet SelectData(string sqlSel, string tableName)
        {
            DataSet dsData = new DataSet();

            SelectData(sqlSel, tableName, ref dsData, true);

            return dsData;
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sqlSel">查询语句</param>
        /// <param name="tableName">表名</param>
        /// <returns>查询结果DataSet</returns>
        public DataSet SelectData_NoKey(string sqlSel, string tableName)
        {
            DataSet dsData = new DataSet();

            SelectData(sqlSel, tableName, ref dsData, false);

            return dsData;
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sqlSel">查询语句</param>
        /// <param name="tableName">表名</param>
        /// <returns>查询结果DataSet</returns>
        public void SelectData(string sqlSel, string tableName, ref DataSet ds)
        {
            SelectData(sqlSel, tableName, ref ds, true);
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sqlSel">查询语句</param>
        /// <param name="tableName">表名</param>
        /// <returns>查询结果DataSet</returns>
        public void SelectData_NoKey(string sqlSel, string tableName, ref DataSet ds)
        {
            SelectData(sqlSel, tableName, ref ds, false);
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sqlSel">查询语句</param>
        /// <param name="tableName">表名</param>
        /// <returns>查询结果DataSet</returns>
        public virtual void SelectData(string sqlSel, string tableName, ref DataSet ds, bool blnWithKey)
        {

        }


        /// <summary>
        /// 更新DB
        /// </summary>
        /// <param name="ds">数据源DataSet</param>
        /// <param name="tableName">要更新的表名</param>
        /// <param name="sqlSel">获取数据源的Sql语句</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>TRUE: 保存成功; FALSE: 保存失败</returns>
        public virtual int Update(ref DataSet ds, string tableName, string sqlSel)
        {
            return 0;
        }


        /// <summary>
        /// 更新DB
        /// </summary>
        /// <param name="dataRows"></param>
        /// <param name="tableName"></param>
        /// <param name="sqlSel"></param>
        public virtual int Update(ref DataRow[] dataRows, string sqlSel)
        {
            return 0;
        }


        /// <summary>
        /// 更新DB
        /// </summary>
        /// <param name="dsChanged"></param>
        public virtual int Update(ref DataSet dsChanged)
        {
            return 0;
        }


        /// <summary>
        /// 执行Sql语句, 没有返回值
        /// </summary>
        /// <param name="sqlCol">sql语句ArrayList</param>
        /// <returns>TRUE: 成功; FALSE: 失败</returns>
        public void ExecuteNoQuery(ref ArrayList sqlCol)
        {
            bool blnInTrans = (_trans != null);

            // 条件检查
            if (sqlCol.Count == 0)
            {
                return;
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

                // 执行Sql语句
                IDbCommand cmd = _connection.CreateCommand();
                cmd.Transaction = _trans;

                try
                {
                    for (int i = 0; i < sqlCol.Count; i++)
                    {
                        cmd.CommandText = (string)sqlCol[i];
                        cmd.ExecuteNonQuery();

                        if (Logging) LogFile.WriteLog(cmd.CommandText);
                    }

                    if (blnInTrans == false)
                    {
                        _trans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    _trans.Rollback();
                    throw new Exception(ex.Message + cmd.CommandText);
                }
            }
            finally
            {
                if (blnInTrans == false)
                {
                    _trans = null;

                    DisConnect();
                }
            }
        }


        /// <summary>
        /// 执行Sql语句, 没有返回值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>TRUE: 成功; FALSE: 失败</returns>
        public void ExecuteNoQuery(string sql)
        {
            bool blnInTrans = (_trans != null);

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

                // 执行Sql语句
                IDbCommand cmd = _connection.CreateCommand();
                cmd.Transaction = _trans;

                try
                {
                    cmd.CommandText = sql;
                    int b= cmd.ExecuteNonQuery();

                    if (blnInTrans == false)
                    {
                        _trans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    _trans.Rollback();
                    throw ex;
                }
            }
            finally
            {
                if (blnInTrans == false)
                {
                    _trans = null;

                    DisConnect();
                }

                if (Logging) LogFile.WriteLog(sql);
            }
        }
        #endregion


        #region 数据库信息获取
        /// <summary>
        /// 获取系统时间
        /// </summary>
        /// <returns></returns>
        public virtual DateTime GetSysDate()
        {
            throw new System.Exception("请重载获取系统日期函数!");
        }
        #endregion


        #region 共通函数
        private void assertConnection()
        {
            if (_connection == null)
            {
                throw new Exception(_STR_NO_CONNECTION);
            }
        }
        #endregion
    }
}
