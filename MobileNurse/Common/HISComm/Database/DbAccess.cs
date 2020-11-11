//------------------------------------------------------------------------------------
//  ����            : DbAccess.cs
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
using System.Data.Common;
using System.Collections;
using System.IO;

namespace HISPlus
{
    /// <summary>
    /// OracleAccess ��ժҪ˵����
    /// </summary>
    public class DbAccess
    {
        #region ����
        protected const int _MAX_COLS = 10;                                   // ����ѡ��ʱĬ�����ѡ10��

        public bool Logging = false;                                // �Ƿ�Log SQL

        protected string _connectStr = string.Empty;                         // �����ַ���
        protected IDbConnection _connection = null;                                 // ���ݿ����Ӷ���        		
        protected IDbTransaction _trans = null;

        protected object[] _arrSelResult = new object[_MAX_COLS];                // Ĭ��Ϊ10���ֶ�

        private readonly string _STR_NO_CONNECTION = "δָ������";
        private readonly string _STR_INDEX_OUT_RNG = "����������Χ";
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


        #region ����
        /// <summary>
        /// �����ݿ������
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
        /// ����
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
        /// �Ƿ������ݿ�����
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
        /// �����ַ���
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


        #region �ӿ�
        /// <summary>
        /// Oracle���ݿ������
        /// </summary>
        /// <returns>��������</returns>
        public void Connect()
        {
            // ����ʵ���Ƿ����
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
        /// Oracle���ݿ������
        /// </summary>
        /// <param name="connectString">�����ַ���</param>
        /// <returns>��������</returns>
        public void Connect(string connectString)
        {
            // ����ʵ���Ƿ����
            assertConnection();

            // ����Ѿ�����
            if (this.IsConnected == true)
            {
                // ��������ַ�����ָ���������ַ�����ͬ, ������
                if (_connection.ConnectionString.Equals(connectString) == true)
                {
                    return;
                }
                else
                {
                    // ���������
                    if (_trans != null)
                    {
                        _trans = null;
                    }

                    // �ر�����
                    _connection.Close();
                }
            }

            // ��ʼ������
            _connection.ConnectionString = connectString;

            _connection.Open();
        }


        /// <summary>
        /// �Ͽ������ݿ������
        /// </summary>
        public void DisConnect()
        {
            if (this.IsConnected == true)
            {
                // ���������
                _trans = null;

                _connection.Close();
            }
        }


        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <returns>TRUE: �ɹ�; FALSE: ʧ��</returns>
        public IDbTransaction BeginTrans()
        {
            // ����ʵ���Ƿ����
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
        /// �����ύ
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
        /// ����ع�
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
        /// ��ѯ����, ֻ��ѯ��һ�еĵ�һ��, ����Ƿ��ؽ��ֻ��һ����
        /// </summary>
        /// <param name="sqlSel">��ѯ���</param>
        /// <returns>TRUE: ����; FALSE: û�в���</returns>
        public bool SelectValue(string sqlSel)
        {
            // ��ѯ����
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
        /// ���ص���ѡ��Ľ��
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
        /// ��ѯ����
        /// </summary>
        /// <param name="sqlSel">��ѯ���</param>
        /// <returns>��ѯ���DataSet</returns>
        public DataSet SelectData(string sqlSel)
        {
            // ׼��DataSet
            DataSet dsData = new DataSet();

            SelectData(sqlSel, "Table1", ref dsData, true);

            return dsData;
        }


        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="sqlSel">��ѯ���</param>
        /// <returns>��ѯ���DataSet</returns>
        public DataSet SelectData_NoKey(string sqlSel)
        {
            // ׼��DataSet
            DataSet dsData = new DataSet();

            SelectData(sqlSel, "Table1", ref dsData, false);

            return dsData;
        }


        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="sqlSel">��ѯ���</param>
        /// <param name="tableName">����</param>
        /// <returns>��ѯ���DataSet</returns>
        public DataSet SelectData(string sqlSel, string tableName)
        {
            DataSet dsData = new DataSet();

            SelectData(sqlSel, tableName, ref dsData, true);

            return dsData;
        }


        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="sqlSel">��ѯ���</param>
        /// <param name="tableName">����</param>
        /// <returns>��ѯ���DataSet</returns>
        public DataSet SelectData_NoKey(string sqlSel, string tableName)
        {
            DataSet dsData = new DataSet();

            SelectData(sqlSel, tableName, ref dsData, false);

            return dsData;
        }


        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="sqlSel">��ѯ���</param>
        /// <param name="tableName">����</param>
        /// <returns>��ѯ���DataSet</returns>
        public void SelectData(string sqlSel, string tableName, ref DataSet ds)
        {
            SelectData(sqlSel, tableName, ref ds, true);
        }


        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="sqlSel">��ѯ���</param>
        /// <param name="tableName">����</param>
        /// <returns>��ѯ���DataSet</returns>
        public void SelectData_NoKey(string sqlSel, string tableName, ref DataSet ds)
        {
            SelectData(sqlSel, tableName, ref ds, false);
        }


        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="sqlSel">��ѯ���</param>
        /// <param name="tableName">����</param>
        /// <returns>��ѯ���DataSet</returns>
        public virtual void SelectData(string sqlSel, string tableName, ref DataSet ds, bool blnWithKey)
        {

        }


        /// <summary>
        /// ����DB
        /// </summary>
        /// <param name="ds">����ԴDataSet</param>
        /// <param name="tableName">Ҫ���µı���</param>
        /// <param name="sqlSel">��ȡ����Դ��Sql���</param>
        /// <param name="conn">���ݿ�����</param>
        /// <returns>TRUE: ����ɹ�; FALSE: ����ʧ��</returns>
        public virtual int Update(ref DataSet ds, string tableName, string sqlSel)
        {
            return 0;
        }


        /// <summary>
        /// ����DB
        /// </summary>
        /// <param name="dataRows"></param>
        /// <param name="tableName"></param>
        /// <param name="sqlSel"></param>
        public virtual int Update(ref DataRow[] dataRows, string sqlSel)
        {
            return 0;
        }


        /// <summary>
        /// ����DB
        /// </summary>
        /// <param name="dsChanged"></param>
        public virtual int Update(ref DataSet dsChanged)
        {
            return 0;
        }


        /// <summary>
        /// ִ��Sql���, û�з���ֵ
        /// </summary>
        /// <param name="sqlCol">sql���ArrayList</param>
        /// <returns>TRUE: �ɹ�; FALSE: ʧ��</returns>
        public void ExecuteNoQuery(ref ArrayList sqlCol)
        {
            bool blnInTrans = (_trans != null);

            // �������
            if (sqlCol.Count == 0)
            {
                return;
            }

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

                // ִ��Sql���
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
        /// ִ��Sql���, û�з���ֵ
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <returns>TRUE: �ɹ�; FALSE: ʧ��</returns>
        public void ExecuteNoQuery(string sql)
        {
            bool blnInTrans = (_trans != null);

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

                // ִ��Sql���
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


        #region ���ݿ���Ϣ��ȡ
        /// <summary>
        /// ��ȡϵͳʱ��
        /// </summary>
        /// <returns></returns>
        public virtual DateTime GetSysDate()
        {
            throw new System.Exception("�����ػ�ȡϵͳ���ں���!");
        }
        #endregion


        #region ��ͨ����
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
