//------------------------------------------------------------------------------------
//
//  系统名称        : 无线移动医疗
//  子系统名称      : 护理工作站

//  对象类型        : 
//  类名            : SqlServerCe.cs
//  功能概要        : SqlServerCe数据库的创建
//  作成者          : 付军
//  作成日          : 2007-05-28
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        : 2009-02-05
//  变更者          : 付军
//  变更内容        : 删除本地旧数据的时机, 选择在第一遍下载完成后
//  版本            : 1.0.0.1
//------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Data.SqlServerCe;
using System.Threading;

using SQL = HISPlus.SqlManager;
using HISPlus.COMAPP.Function;

namespace HISPlus
{
    /// <summary>
    /// SqlServerCe 的摘要说明。

    /// </summary>
    public class SqlCeSync
    {
        #region 变量
        public const string PDA_TABLE_LIST          = "PDA_TABLE_LIST";
        public const string TABLE_TOMBSTONE         = "_TOMBSTONE";
        
        public const string COL_UPDATE_DATE_TIME    = "UPD_DATE_TIME";      // 本地表中跟踪变化时间的列
        
        protected bool       _exit                  = false;                // 是否退出
        private Thread threadDownload               = null;
        private Thread threadUpload                 = null;
        
        private const string ACTION_INSERT          = "1";
        private const string ACTION_UPDATE          = "2";
        private const string ACTION_DELETE          = "3";
        
        private string dbFile                       = "pda_nursing.sdf";    // 数据库文件的名称
        private string password                     = "1234";               // 数据库密码
        
        private string connStr                      = string.Empty;         // 连接字符串
        
        private Mutex       _locker                 = new Mutex();          // 互斥锁
        
        private SqlceAccess sqlCeAccess             = new SqlceAccess();        
        
        protected string _wardCode                  = string.Empty;
        protected string _serverIp                  = string.Empty;
        private DataWebSrv.DataWebSrv webSync       = new DataWebSrv.DataWebSrv();
        private UserWebSrv.UserWebSrv userWeb       = new HISPlus.UserWebSrv.UserWebSrv();

        private bool        serverTimeGot           = false;
        private DateTime    serverDateTime          = DateTime.MinValue;
        private long        tickCount = 0;
        
        private DataSet     dsLog                   = null;
        
        private DateTime    lastUpdateTime          = DateTime.MinValue;    // 最后更新时间
        private DateTime    lastUploadTimeBegin     = DateTime.MinValue;    // 最后上传时间
        #endregion
        
        public SqlCeSync()
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
            dbFile = System.IO.Path.Combine(appPath, dbFile);
            
            connStr = "Data Source = " + dbFile + ";";// + @"; Password ='" + password + "';";
            // connStr = "Data Source = " + dbFile + @"; Password ='" + password + "';";
            
            sqlCeAccess.ConnectionString = connStr;            
        }
        
        
        #region 属性
        /// <summary>
        /// 退出上传与下载线程
        /// </summary>
        public bool Exit
        {
            get
            {
                try
                {
                    _locker.WaitOne();
                    
                    return _exit;
                }
                finally
                {
                    _locker.ReleaseMutex();
                }
            }
            
            set
            {
                try
                {
                    _locker.WaitOne();
                    
                    _exit = true;
                }
                finally
                {
                    _locker.ReleaseMutex();
                }
            }
        }
        
        
        /// <summary>
        /// 属性[数据库文件名]
        /// </summary>
        public string DbFile
        {
            get
            {
                return dbFile;
            }
        }
        
        
        /// <summary>
        /// 属性[密码]
        /// </summary>
        public string Password
        {
            get
            {
                return password;
            }
        }
        
        
        /// <summary>
        /// 属性[连接字符串]
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return connStr;
            }
        }
        
        
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public string ServerIp
        {
            get { return _serverIp; }
            set 
            {
                _serverIp = value;
                webSync.Url = UrlIp.ChangeIpInUrl(webSync.Url, _serverIp);
                userWeb.Url = UrlIp.ChangeIpInUrl(userWeb.Url, _serverIp);
            }
        }
        
        
        /// <summary>
        /// 病区代码
        /// </summary>
        public string WardCode
        {
            get { return _wardCode; }
            set { _wardCode = value;}
        }


        /// <summary>
        /// 获取系统时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetSysDate()
        {
            if (serverTimeGot == true)
            {
                DateTime dtCalc = serverDateTime.AddMilliseconds(Environment.TickCount - tickCount);
                if (Math.Abs(dtCalc.Subtract(DateTime.Now).TotalMilliseconds) > 1000)
                {
                    getServerSysDate();

                    dtCalc = serverDateTime.AddMilliseconds(Environment.TickCount - tickCount);
                    if (Math.Abs(dtCalc.Subtract(DateTime.Now).TotalMilliseconds) > 1000)
                    {
                        if (GVars.Demo == true)
                        {
                            return DateTime.Now;
                        }
                        else
                        {
                            throw new Exception("PDA时间与服务器时间不一致, 自动修复失败! 请重登录程序!");
                        }
                    }
                }

                return DateTime.Now;
            }
            else
            {
                if (GVars.Demo == true)
                {
                    return DateTime.Now;
                }
                else
                {
                    getServerSysDate();
                    
                    if (serverTimeGot == true)
                    {
                        return serverDateTime;
                    }
                    else
                    {    
                        throw new Exception("尚未获取系统时间, 请稍等或退出程序重新登录!");
                    }
                }
            }            
        }


        /// <summary>
        /// 获取系统时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetSysDate(DateTime dtDefault)
        {
            try
            {
                return GetSysDate();
            }
            catch
            {
                return dtDefault;
            }            
        }
        #endregion
        
        
        #region  本地数据访问方法
        /// <summary>
		/// 查询数据
		/// </summary>
		/// <param name="sqlSel">查询语句</param>
		/// <returns>查询结果DataSet</returns>
		public DataSet SelectData(string sqlSel)
		{
		    try
		    {
		        _locker.WaitOne();
		        
		        return sqlCeAccess.SelectData(sqlSel);
		    }
		    finally
		    {
		        _locker.ReleaseMutex();
		    }
		}
		
		
        /// <summary>
		/// 查询数据
		/// </summary>
		/// <param name="sqlSel">查询语句</param>
		/// <param name="tableName">表名</param>
		/// <returns>查询结果DataSet</returns>
		public DataSet SelectData(string sqlSel, string tableName)
		{
		    try
		    {
		        _locker.WaitOne();
		        
		        return sqlCeAccess.SelectData(sqlSel, tableName);
		    }
		    finally
		    {
		        _locker.ReleaseMutex();
		    }
		}
		
		
        /// <summary>
		/// 查询数据
		/// </summary>
		/// <param name="sqlSel">查询语句</param>
		/// <param name="tableName">表名</param>
		/// <returns>查询结果DataSet</returns>
		public DataSet SelectData_NoKey(string sqlSel, string tableName)
		{
		    try
		    {
		        _locker.WaitOne();
		        
		        return sqlCeAccess.SelectData_NoKey(sqlSel, tableName);
		    }
		    finally
		    {
		        _locker.ReleaseMutex();
		    }
		}
        
		
        /// <summary>
		/// 查询数据
		/// </summary>
		/// <param name="sqlSel">查询语句</param>
		/// <param name="tableName">表名</param>
		/// <returns>查询结果DataSet</returns>
		public void SelectData(string sqlSel, string tableName, ref DataSet ds, bool blnWithKey)
		{
		    try
		    {
		        _locker.WaitOne();
		        
		        sqlCeAccess.SelectData(sqlSel, tableName, ref ds, blnWithKey);
		    }
		    finally
		    {
		        _locker.ReleaseMutex();
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
		public void Update(ref DataSet ds, string tableName, string sqlSel)
		{
		    try
		    {
		        _locker.WaitOne();
		     
		        sqlCeAccess.Update(ref ds, tableName, sqlSel);
		    }
		    finally
		    {
		        _locker.ReleaseMutex();

                lastUpdateTime = GetSysDate(lastUpdateTime);
		    }
		}
        
        
        /// <summary>
        /// 更新DB
        /// </summary>
        /// <param name="dataRows"></param>
        /// <param name="tableName"></param>
        /// <param name="sqlSel"></param>
        public void Update(ref DataRow[] dataRows, string sqlSel)
        { 
            try
		    {
		        _locker.WaitOne();
		     
		        sqlCeAccess.Update(ref dataRows, sqlSel);
		    }
		    finally
		    {
		        _locker.ReleaseMutex();

                lastUpdateTime = GetSysDate(lastUpdateTime);
		    }
        }
        
        
        /// <summary>
        /// 仅根据DataSet 更新数据表
        /// </summary>
        /// <param name="dsChanged"></param>
        public void Update(ref DataSet dsChanged)
        {
            try
		    {
		        _locker.WaitOne();
		     
		        sqlCeAccess.Update(ref dsChanged);
		    }
		    finally
		    {
		        _locker.ReleaseMutex();

                lastUpdateTime = GetSysDate(lastUpdateTime);
		    }
        }        
        
        
		/// <summary>
		/// 执行Sql语句, 没有返回值
		/// </summary>
		/// <param name="sqlCol">sql语句ArrayList</param>
		/// <returns>TRUE: 成功; FALSE: 失败</returns>
		public void ExecuteNoQuery(ref ArrayList sqlCol)
		{
		    try
		    {
		        _locker.WaitOne();
		     
		        sqlCeAccess.ExecuteNoQuery(ref sqlCol);
		    }
		    finally
		    {
		        _locker.ReleaseMutex();

                lastUpdateTime = GetSysDate(lastUpdateTime);
		    }
		}


		/// <summary>
		/// 执行Sql语句, 没有返回值
		/// </summary>
		/// <param name="sql">sql语句</param>
		/// <returns>TRUE: 成功; FALSE: 失败</returns>
		public int ExecuteNoQuery(string sql)
		{
            try
		    {
		        _locker.WaitOne();
		        
		        return sqlCeAccess.ExecuteNoQuery(sql);
		    }
		    finally
		    {
		        _locker.ReleaseMutex();

                lastUpdateTime = GetSysDate(lastUpdateTime);
		    }
		}
        #endregion
        
        
        #region 线程控制
        /// <summary>
        /// 开始下载
        /// </summary>
        public void StartDownLoad()
        {
            // return; // TEST
            
            threadDownload = new Thread(new ThreadStart(downloadData));
            threadDownload.Priority = System.Threading.ThreadPriority.BelowNormal;
            threadDownload.IsBackground = true;
            threadDownload.Start();
        }
        
        
        /// <summary>
        /// 开始上传
        /// </summary>
        public void StartUpLoad()
        {
            threadUpload = new Thread(new ThreadStart(uploadData));
            threadUpload.Priority = System.Threading.ThreadPriority.BelowNormal;
            threadUpload.IsBackground = true;
            threadUpload.Start();
        }
        
               
        /// <summary>
        /// 下载数据
        /// </summary>
        private void downloadData()
        {
            int c = 0;
            int counter = 0;
            string maxDate = string.Empty;
                        
            while(true)
            {
                //HISPlus.COMAPP.Function.SystemLog.SetTestLog("downloadData", DateTime.Now.ToString());

                // 获取系统时间
                getServerSysDate();
                
                // 更新同步记录表
                if (counter % 200 == 1)
                {
                    updateSyncTable(ref dsLog);
                }
                
                if (++counter >= int.MaxValue) counter = 0;
                
                for(c = 0; c < 10; c++)
                {
                    if (Exit) return;
                    Thread.Sleep(1000);
                }
                
                string tableName    = string.Empty;
                string filter       = string.Empty;
                
                try
                {
                    // 查找哪些表需要下载
                    string sql = "SELECT * FROM PDA_TABLE_LIST WHERE SYNC_MODE > '0'";
                    DataSet ds = SelectData(sql);
                    
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        // 休息10秒
                        for (c = 0; c <  10; c++)
                        {
                            if (Exit) return;
                            Thread.Sleep(1000);
                        }
                        
                        string lastDownDBTS = string.Empty;
                        
                        if (dr["LAST_DOWN_SCN"] != null && dr["LAST_DOWN_SCN"].ToString().Length > 0)
                        {
                            lastDownDBTS = dr["LAST_DOWN_SCN"].ToString();
                        }
                        
                        maxDate = getMax(maxDate, lastDownDBTS);
                        
                        tableName = dr["TABLE_NAME"].ToString();
                        filter    = getFilter(dr["FILTER_CONDITION"].ToString());
                        
                        // 获取服务器上更新的数据
                        DataSet dsChanged = null;
                        try
                        {
                            IAsyncResult ar = webSync.BeginGetDataChanged(tableName, lastDownDBTS, filter, null, null);
                            
                            int waitCount = 0;      // 最长等10秒
                            while(ar.IsCompleted == false && waitCount++ < 10 * 60)
                            {
                                Thread.Sleep(100);
                                if (_exit) return;                            
                            }
                            
                            if (ar.IsCompleted == false) 
                            {
                                addLog(2, "下载失败 或 超时 10秒");
                                continue;
                            }
                            
                            dsChanged = webSync.EndGetDataChanged(ar);
                            
                            if (dsChanged == null || dsChanged.Tables.Count == 0 || dsChanged.Tables[0].Rows.Count == 0) continue;
                            
                            addLog(2, DateTime.Now.ToString() + " 开始下载: TableName: " + tableName 
                                        + " LAST_DOWN_SCN: " + lastDownDBTS
                                        + " FILTER: " + filter);
                            addLog(2, "下载数据量: " + dsChanged.Tables[0].Rows.Count);
                            
                            // 更新本地的数据
                            saveDownloadInLocal(ref dsChanged, tableName, ref lastDownDBTS, ref dsLog);
                            
                            // 更新PDA_TABLE_LIST表
                            if (lastDownDBTS.Length > 0)
                            {
                                sql = "UPDATE " + PDA_TABLE_LIST 
                                   + " SET  LAST_DOWN_SCN = " + SqlManager.SqlConvert(lastDownDBTS)
                                   + " WHERE TABLE_NAME = " + SqlManager.SqlConvert(tableName);
                                
                                ExecuteNoQuery(sql);

                                addLog(2, "更新本地标识: LAST_DOWN_SCN = " + lastDownDBTS);
                                
                                for(c = 0; c < 30; c++)
                                {
                                    if (Exit) return;
                                    Thread.Sleep(1000);
                                }
                                
                                maxDate = getMax(maxDate, lastDownDBTS);
                            }
                        }
                        catch(Exception ex)
                        {
                            addLog(2, "失败 " + ex.Message);
                            
                            for(c = 0; c < 30; c++)
                            {
                                if (Exit) return;
                                Thread.Sleep(1000);
                            }
                            
                            continue;                            
                        }
                        
                        // 休息10秒
                        for (c = 0; c < 10; c++)
                        {
                            if (Exit) return;
                            Thread.Sleep(1000);
                        }
                    }
                    
                    if ((counter % 200 == 1) && maxDate.Length > 0)                    
                    {
                        DeleteOutOfDate();
                    }
                    
                    if (counter % 200 == 1)
                    {
                        DeleteOutOfPatient();
                        
                        // 删除医嘱记录
                        sql = "DELETE FROM ORDERS_M ";
                        ExecuteNoQuery(sql);
                    }
                }
                catch(Exception ex)
                {
                    addLog(2, "失败 " + ex.Message);
                    
                    for(c = 0; c < 30; c++)
                    {
                        if (Exit) return;
                        Thread.Sleep(1000);
                    }
                }
                
                for(c = 0; c < 300; c++)
                {
                    if (Exit) return;
                    Thread.Sleep(1000);
                }
            }
        }
        
        
        /// <summary>
        /// 把下载的数据保存到本地
        /// </summary>
        /// <param name="dsDown"></param>
        /// <param name="tableName"></param>
        private bool saveDownloadInLocal2(ref DataSet dsDown, string tableName, ref string lastDownDBTS, ref DataSet dsLog)
        {
            string sqlSel = "SELECT * FROM " + tableName;
            DataSet dsLocalEmpty = SelectData(sqlSel + " WHERE (1 = 2)", tableName);
            
            DataRow[] drFind = dsDown.Tables[0].Select(string.Empty, "CREATE_TIMESTAMP, UPDATE_TIMESTAMP");
            
            lastDownDBTS = string.Empty;
            int counter = 0;
            for(int i = 0; i < drFind.Length; i++)
            {
                if ((++counter -1 ) % 50 == 49)
                {
                    addLog(2, "本地已保存 " + counter.ToString());
                }
                
                DataRow dr = drFind[i];
                
                string sql = sqlSel + " WHERE (1 = 2)";
                
                DataSet dsLocal = dsLocalEmpty.Clone();
                    
                #region 获取本地对应记录
                string filter = COL_UPDATE_DATE_TIME + " IS NULL ";
                for(int k = 0; k < dsLocalEmpty.Tables[0].PrimaryKey.Length; k++)
                {
                    filter += " AND ";
                    
                    DataColumn dc = dsLocalEmpty.Tables[0].PrimaryKey[k];
                    if (dc.DataType == System.Type.GetType("System.DateTime"))
                    {
                        DateTime dt = (DateTime)(dr[dc.ColumnName]);
                        
                        filter += dc.ColumnName + " = " + SqlManager.SqlConvert(dt.ToString(ComConst.FMT_DATE.LONG));
                    }
                    else
                    {
                        filter += dc.ColumnName + " = " + SqlManager.SqlConvert(dr[dc.ColumnName].ToString());
                    }
                }
                
                sql = sqlSel + " WHERE " + filter;
                dsLocal = SelectData(sql, tableName);
                #endregion
                
                // 如果存在记录
                if (dsLocal.Tables[0].Rows.Count > 0)
                {
                    #region 更新与删除
                    DataRow drEdit = dsLocal.Tables[0].Rows[0];
                    
                    // 如果是删除
                    if (dr["ACTION"].ToString().Equals(ACTION_DELETE))
                    {
                        dsLocal.Tables[0].Rows[0].Delete();
                    }
                    // 如果是更新
                    else
                    {
                        foreach(DataColumn dc in dsLocal.Tables[0].Columns)
                        {
                            if (dc.ColumnName.Equals(COL_UPDATE_DATE_TIME) == false)
                            {
                                drEdit[dc.ColumnName] = dr[dc.ColumnName];
                            }
                        }
                    }
                    
                    // 更Db
                    try
                    {
                        Update(ref dsLocal, tableName, sql);
                    }
                    catch(Exception ex)
                    {
                        addLog(2, "更新本地数据 失败 表: " + tableName + " 原因: " + ex.Message);
                        return false;
                    }
                    #endregion
                }
                // 如果不存在
                else if (dr["ACTION"].ToString().Equals(ACTION_DELETE) == false)
                {
                    #region 如果是插入
                    DataRow drNew = dsLocal.Tables[0].NewRow();
                    
                    foreach(DataColumn dc in dsLocal.Tables[0].Columns)
                    {
                        if (dc.ColumnName.Equals(COL_UPDATE_DATE_TIME) == false)
                        {
                            drNew[dc.ColumnName] = dr[dc.ColumnName];
                        }
                    }
                    
                    dsLocal.Tables[0].Rows.Add(drNew);
                    
                    try
                    {
                        Update(ref dsLocal, tableName, sql);
                        dsLocal.AcceptChanges();
                    }
                    catch(Exception ex)
                    {
                        addLog(2, "更新本地数据 失败 表: " + tableName + " 原因: " + ex.Message);
                        return false;
                    }
                    #endregion
                }
                
                #region 获取最大时间
                if (dr["DBTS"] != null && dr["DBTS"].ToString().Length > 0)              
                {
                    string dbts = dr["DBTS"].ToString();
                    
                    if (lastDownDBTS.CompareTo(dbts) < 0)
                    {
                        lastDownDBTS = dbts;
                    }
                }
                #endregion                
                
                if (Exit) return true;
            }
            
            return true;
        }        

        
        /// <summary>
        /// 把下载的数据保存到本地
        /// </summary>
        /// <param name="dsDown"></param>
        /// <param name="tableName"></param>
        private bool saveDownloadInLocal(ref DataSet dsDown, string tableName, ref string lastDownDBTS, ref DataSet dsLog)
        {
            string sqlSel = "SELECT * FROM " + tableName;
            DataSet dsLocalEmpty = SelectData(sqlSel + " WHERE (1 = 2)", tableName);
            
            DataRow[] drFind = dsDown.Tables[0].Select(string.Empty, "CREATE_TIMESTAMP, UPDATE_TIMESTAMP");
            
            lastDownDBTS = string.Empty;
            int counter = 0;
            for(int i = 0; i < drFind.Length; i++)
            {
                if ((++counter -1 ) % 50 == 49)
                {
                    addLog(2, "本地已保存 " + counter.ToString());
                }
                
                DataRow dr = drFind[i];
                
                // 获取主键过滤条件
                string filter = COL_UPDATE_DATE_TIME + " IS NULL ";
                for(int k = 0; k < dsLocalEmpty.Tables[0].PrimaryKey.Length; k++)
                {
                    filter += " AND ";
                    
                    DataColumn dc = dsLocalEmpty.Tables[0].PrimaryKey[k];
                    if (dc.DataType == System.Type.GetType("System.DateTime"))
                    {
                        DateTime dt = (DateTime)(dr[dc.ColumnName]);
                        
                        filter += dc.ColumnName + " = " + SQL.SqlConvert(dt.ToString(ComConst.FMT_DATE.LONG));
                    }
                    else
                    {
                        filter += dc.ColumnName + " = " + SQL.SqlConvert(dr[dc.ColumnName].ToString());
                    }
                }
                
                string sql = string.Empty;
                
                // 删除
                if (ACTION_DELETE.Equals(dr["ACTION"].ToString()))
                {
                    sql = "DELETE FROM " + tableName + " WHERE " + filter;
                    ExecuteNoQuery(sql);
                }
                
                // 对于更新, 如果返回更新记录数为0, 改变为INSERT再试一次
                if (ACTION_UPDATE.Equals(dr["ACTION"].ToString()))
                {
                    string valuePair = string.Empty;
                        
                    foreach(DataColumn dc in dsLocalEmpty.Tables[0].Columns)
                    {
                        if (dc.ColumnName.Equals(COL_UPDATE_DATE_TIME) == true)
                        {
                            continue;
                        }
                        
                        if (valuePair.Length > 0) valuePair += ",";
                        
                        if (dr[dc.ColumnName] == DBNull.Value)
                        {
                            valuePair += dc.ColumnName + " = NULL ";
                            continue;
                        }
                        
                        if (dc.DataType == System.Type.GetType("System.DateTime"))
                        {
                            DateTime dt = (DateTime)(dr[dc.ColumnName]);
                            valuePair += dc.ColumnName + " = " + SQL.SqlConvert(dt.ToString(ComConst.FMT_DATE.LONG));
                        }
                        else
                        {
                            valuePair += dc.ColumnName + " = " + SQL.SqlConvert(dr[dc.ColumnName].ToString());
                        }
                    }
                    
                    sql = "UPDATE " + tableName + " SET " + valuePair + " WHERE " + filter;
                    
                    // 如果更新失败, 原因可能是 COL_UPDATE_DATE_TIME 值不为空, 或者无这条记录
                    // 在下一步偿试插入这条记录, 如果插入失败, 表示是 COL_UPDATE_DATE_TIME 不为空, 不进行处理
                    if(ExecuteNoQuery(sql) == 0)
                    {
                        dr["ACTION"] = ACTION_INSERT;
                    }                    
                }
                
                if (ACTION_INSERT.Equals(dr["ACTION"].ToString()))
                {
                    string colList   = string.Empty;
                    string valueList = string.Empty;
                    foreach(DataColumn dc in dsLocalEmpty.Tables[0].Columns)
                    {
                        if (dc.ColumnName.Equals(COL_UPDATE_DATE_TIME) == true)
                        {
                            continue;
                        }
                        
                        if (valueList.Length > 0) valueList += ",";
                        if (colList.Length > 0) colList += ",";
                        
                        colList += dc.ColumnName;
                        if (dr[dc.ColumnName] == DBNull.Value)
                        {
                            valueList += "NULL";
                            continue;
                        }
                        
                        if (dc.DataType == System.Type.GetType("System.DateTime"))
                        {
                            DateTime dt = (DateTime)(dr[dc.ColumnName]);
                            valueList += SQL.SqlConvert(dt.ToString(ComConst.FMT_DATE.LONG));
                        }
                        else
                        {
                            valueList += SQL.SqlConvert(dr[dc.ColumnName].ToString());
                        }
                    }
                    
                    sql = "INSERT " + tableName + " ( " + colList + " ) VALUES (" + valueList + ") ";
                    
                    // 忽略插入失败的错误
                    try
                    {
                        ExecuteNoQuery(sql);
                    }
                    catch(Exception ex)
                    {
                        addLog(2, "更新本地记录失败 " + ex.Message);
                    }
                }
                                
                #region 获取最大时间
                if (dr["DBTS"] != null && dr["DBTS"].ToString().Length > 0)              
                {
                    string dbts = dr["DBTS"].ToString();
                    
                    if (lastDownDBTS.CompareTo(dbts) < 0)
                    {
                        lastDownDBTS = dbts;
                    }
                }
                #endregion                
                
                if (Exit) return true;
            }
            
            return true;
        }        
        
        
        /// <summary>
        /// 上传数据
        /// </summary>
        private void uploadData()
        {
            // 查找哪些表需要上传
            int     c   = 0;
            string  sql = string.Empty;
            DataSet ds  = null;
                        
            while(true)
            {
                for(c = 0; c < 30; c++)
                {
                    if (Exit) return;
                    Thread.Sleep(100);
                }

                if (lastUploadTimeBegin.CompareTo(lastUpdateTime) > 0) continue;
                
                try
                {
                    sql = "SELECT * FROM PDA_TABLE_LIST WHERE SYNC_MODE = '2'";
                    ds = SelectData(sql);
                }
                catch(Exception ex)
                {
                    addLog(1, "获取本地表列表失败 " + ex.Message);
                    continue;
                }
                
                if (ds == null || ds.Tables.Count == 0)
                {
                    continue;
                }
                
                string tableName = string.Empty;
                bool allUploaded = true;
                
                foreach(DataRow dr in ds.Tables[0].Rows)
                {   
                    tableName = dr["TABLE_NAME"].ToString();
                    
                    // 获取本地更新的数据
                    DataSet dsChanged = null;
                    
                    try
                    {
                        dsChanged = getDataChanged(tableName);
                    }
                    catch(Exception ex)
                    {
                        addLog(1, "获取" + tableName + "变更数据失败 " + ex.Message);
                        allUploaded = false;
                        continue;
                    }
                    
                    if (dsChanged == null || dsChanged.Tables.Count == 0 || dsChanged.Tables[0].Rows.Count == 0)
                    {
                        Thread.Sleep(200);
                        continue;
                    }
                    else
                    {
                        // dsChanged.WriteXml(tableName + ".Xml", XmlWriteMode.WriteSchema);
                    }
                    
                    //dsChanged.WriteXml(tableName + "_up.xml", XmlWriteMode.WriteSchema);
                    addLog(1, "获取表" + tableName + " 变更 " + dsChanged.Tables[0].Rows.Count.ToString() + " 条");
                    
                    // 更新远程服务器的数据
                    DateTime dtLastUpload = DataType.DateTime_Null();
                    try
                    {

                        //SystemLog.SetTestLog(tableName, SystemLog.ConvertDataSetToXML(dsChanged));
                        IAsyncResult ar = webSync.BeginApplyDataChange(dsChanged, tableName, null, null);
                        
                        int waitCount = 0;      // 最长等10秒
                        while(ar.IsCompleted == false && waitCount++ < 10 * 60)
                        {
                            Thread.Sleep(100);
                            if (Exit) return;                            
                        }
                        
                        if (ar.IsCompleted == false) continue;
                        
                        dtLastUpload = webSync.EndApplyDataChange(ar);
                    }
                    catch(Exception ex)
                    {
                        addLog(1, "上传" + tableName + "变更数据失败 " + ex.Message);
                        allUploaded = false;
                        SystemLog.OutputExceptionLog("上传" + tableName + "异常:" + ex.Message);
                        continue;
                    }

                    addLog(1, "上传表" + tableName + " " + dsChanged.Tables[0].Rows.Count.ToString() + " 条");
                    
                    try
                    {
                        if (DataType.DateTime_IsNull(ref dtLastUpload) == false)
                        {
                            string lastUploadTime = dtLastUpload.ToString(ComConst.FMT_DATE.LONG);
                            
                            // 更新PDA_TABLE_LIST表
                            sql = "UPDATE " + PDA_TABLE_LIST 
                               + " SET  LAST_UP_DATE_TIME = " + SqlManager.SqlConvert(lastUploadTime)
                               + " WHERE TABLE_NAME = " + SqlManager.SqlConvert(tableName);
                            
                            ExecuteNoQuery(sql);
                            
                            // 更新表的UPD_DATE_TIME字段
                            lastUploadTime = dtLastUpload.AddSeconds(1).ToString(ComConst.FMT_DATE.LONG);                    
                            string filter = COL_UPDATE_DATE_TIME 
                                           + " < CAST(" + SqlManager.SqlConvert(lastUploadTime) + " AS DATETIME)";
                            
                            sql = "UPDATE " + tableName + " SET " + COL_UPDATE_DATE_TIME + " = NULL"
                                 + " WHERE " + filter;
                            ExecuteNoQuery(sql);
                            
                            // 删除Tombstone表的内容
                            sql = "DELETE FROM " + tableName + TABLE_TOMBSTONE
                                + " WHERE " + filter;
                            ExecuteNoQuery(sql);

                            addLog(1, "更新本地 表" + tableName + " 状态成功!");
                            
                            Thread.Sleep(1 * 1000);
                        }
                    }
                    catch(Exception ex)
                    {
                        addLog(1, "更新" + tableName + "本地状态失败 " + ex.Message);
                        allUploaded = false;
                        continue;
                    }
                                    
                    Thread.Sleep(300);
                }
                
                try
                {
                    if (allUploaded == true) lastUploadTimeBegin = GetSysDate(lastUploadTimeBegin);
                }
                catch{}
                
                for(c = 0; c < 100; c++)
                {
                    if (Exit) return;
                    Thread.Sleep(100);
                }
            }
        }
        
        
        /// <summary>
        /// 获取本地数据的更新
        /// </summary>
        /// <returns></returns>
        private DataSet getDataChanged(string tableName)
        {   
            string sql = "SELECT " + tableName + ".*, '2' ACTION FROM " + tableName + " WHERE " + COL_UPDATE_DATE_TIME + " IS NOT NULL"
                + " UNION "
                + "SELECT " + tableName + TABLE_TOMBSTONE + ".*, '3' ACTION FROM " + tableName + TABLE_TOMBSTONE;
                
            return SelectData_NoKey(sql, tableName);
        }        
        #endregion
        
        
        #region 表结构更新
        /// <summary>
        /// 本地数据检查
        /// </summary>
        /// <returns></returns>
        public bool CheckDatabase()
        {
            // 查看表结构最新更新时间
            DateTime dtLastUpdate = getTableStrcutLastUpdateDateTime();
            
            // 更新表结构
            updateDatabaseStruct(dtLastUpdate);
            
            return true;
        }
        
        
        /// <summary>
        /// 获取表结构最后更新时间
        /// </summary>
        /// <returns></returns>
        private DateTime getTableStrcutLastUpdateDateTime()
        {
            DateTime dtMax = DataType.DateTime_Null();
            
            // 获取最后更新日期
            string sql = "SELECT "
                            + "MAX(CREATE_DATE) MAX_CREATE_DATE "
                         + "FROM " + PDA_TABLE_LIST;
            
            try
            {             
                DataSet ds = sqlCeAccess.SelectData(sql);
                
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    if (dr["MAX_CREATE_DATE"] != null)
                    {
                        dtMax = (DateTime)dr["MAX_CREATE_DATE"];
                    }                    
                }
            }
            catch(Exception ex)
            {
                addLog(0, "获取表结构最后更新时间 " + ex.Message);
            }
            
            return dtMax;
        }
        
        
        /// <summary>
        /// 创建数据库

        /// </summary>
        /// <returns></returns>
        private bool updateDatabaseStruct(DateTime dtLastUpdate)
        {
            // 获取更新了的PDA数据表列表            
            DataSet dsTableList = webSync.GetPdaDbTableList(dtLastUpdate);
            
            if (dsTableList == null || dsTableList.Tables.Count == 0 || dsTableList.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            
            // 如果索引表结构发生变化, 整个本地库重建
            if (dsTableList == null || dsTableList.Tables.Count == 0)
            {
                return true;
            }
            
            DataRow[] drFind = dsTableList.Tables[0].Select("TABLE_NAME = " + SqlManager.SqlConvert(PDA_TABLE_LIST));
            
            if (drFind.Length > 0)
            {
                if (System.IO.File.Exists(dbFile) == true)
                {
                    System.IO.File.Delete(dbFile);
                }
            }
            
            // 判断本地数据库文件是否存在
            if (System.IO.File.Exists(dbFile) == false)
            {
                // 如果数据库不存在, 创建它            
                // string connectString = connStr + "Encrypt = TRUE;";
                string connectString = connStr;
                SqlCeEngine engine = new SqlCeEngine(connectString);
                engine.CreateDatabase();
            }
            
            // 创建PDA本地数据表
            ArrayList arrSql = new ArrayList();
            foreach(DataRow dr in dsTableList.Tables[0].Rows)
            {
                string tableName = dr["TABLE_NAME"].ToString();
                string script    = dr["CREATE_SCRIPT"].ToString().Replace(ComConst.STR.CRLF, string.Empty).ToUpper();
                
                // 删除以前的表
                try
                {
                    string sql   = "DROP TABLE " + tableName;
                    sqlCeAccess.ExecuteNoQuery(sql);
                    
                    sql = "DROP TABLE " + tableName + TABLE_TOMBSTONE;
                    sqlCeAccess.ExecuteNoQuery(sql);
                }
                catch(Exception ex)
                {
                    addLog(0, ex.Message);
                }
                
                // 创建表
                arrSql.Clear();
                arrSql.Add(script);
                
                // 如果需要双向同步
                string syncMode = dr["SYNC_MODE"].ToString();
                if (dr["SYNC_MODE"].ToString().Equals("2"))
                {
                    // 增加变化跟踪表
                    arrSql.Add(getScript_CreateTableTombstone(tableName, script));                        
                }
                
                sqlCeAccess.ExecuteNoQuery(ref arrSql);
            }
            
            // 更新表 [PDA_TABLE_LIST]
            updateTableList(ref dsTableList);
            
            return true;
        }
        
        
        /// <summary>
        /// 获取创建跟踪表的SCRIPT语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="script"></param>
        /// <remarks>仅去掉正常表的主键约束</remarks>
        /// <returns></returns>
        string getScript_CreateTableTombstone(string tableName, string script)
        {
            //CREATE TABLE SYNC_APP_CONFIG ( 
            //    PARAMETER_ID NUMERIC(8,0),
            //    PARAMETER_NAME NVARCHAR(40),
            //    PARAMETER_VALUE NVARCHAR(200),
            //    CONSTRAINT PK_APP_CONFIG PRIMARY KEY 
            //    (PARAMETER_ID)
            //)
            
            // SCRIPT 中替换表名
            tableName += TABLE_TOMBSTONE;
            
            int pos0 = script.IndexOf(" TABLE ");
            int pos1 = script.IndexOf(" (");
            
            if (pos0 < 1 || pos1 < 1 || pos0 > pos1)
            {
                throw new Exception(tableName + "表结构定义不正确!");
            }
            
            script = script.Substring(0, pos0) + " TABLE " + tableName + script.Substring(pos1, script.Length - pos1);
            
            // SCRIPT 中增加同步列
            pos0 = script.IndexOf("CONSTRAINT ");
            if (pos0 < 1)
            {
                throw new Exception(tableName + "表结构定义不正确!");
            }
            
            script = script.Substring(0, pos0).Trim();
            if (script.EndsWith(",") == true)
            {
                script = script.Substring(0, script.Length - 1);
            }
            
            script += ")";
            
            return script;
        }
        
        
        /// <summary>
        /// 更新本地库中的TableList表
        /// </summary>
        /// <returns></returns>
        private bool updateTableList(ref DataSet ds)
        {
            string sql = "SELECT * FROM " + PDA_TABLE_LIST;          
            DataSet dsLocal = sqlCeAccess.SelectData(sql, PDA_TABLE_LIST);
            
            string filter = "TABLE_NAME = '{0}'";
            
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                DataRow[] drFind = dsLocal.Tables[0].Select(string.Format(filter, dr["TABLE_NAME"].ToString()));
                
                DataRow drEdit = null;
                if (drFind.Length == 0)
                {
                    drEdit = dsLocal.Tables[0].NewRow();
                }
                else
                {
                    drEdit = drFind[0];
                }
                
                drEdit["APP_CODE"]         = dr["APP_CODE"];
                drEdit["TABLE_NAME"]       = dr["TABLE_NAME"];
                drEdit["FILTER_CONDITION"] = dr["FILTER_CONDITION"];
                
                if (dr["UPDATE_DATE"] == null)
                {
                    drEdit["CREATE_DATE"] = dr["CREATE_DATE"];
                }
                else
                {
                    drEdit["CREATE_DATE"] = dr["UPDATE_DATE"];
                }
                
                drEdit["SYNC_MODE"]     = dr["SYNC_MODE"];
                drEdit["LAST_DOWN_SCN"] = null;
                
                if (drFind.Length == 0)
                {
                    dsLocal.Tables[0].Rows.Add(drEdit);
                }
            }
            
            sqlCeAccess.Update(ref dsLocal, PDA_TABLE_LIST, sql);
            
            return true;
        }
        
        
        /// <summary>
        /// 更新同步记录信息
        /// </summary>
        /// <returns></returns>
        private bool updateSyncTable(ref DataSet dsLog)
        {
            try
            {
                // 获取服务器数据
                IAsyncResult ar = webSync.BeginGetDataChanged("PDA_DB_TABLE", string.Empty, string.Empty, null, null);
                
                int waitCount = 0;      // 最长等10秒
                while(ar.IsCompleted == false && waitCount++ < 10 * 60)
                {
                    Thread.Sleep(100);
                    if (_exit) return false;                            
                }
                
                if (ar.IsCompleted == false) 
                {
                    addLog(2, "下载失败 或 超时 10秒");
                    return false;
                }
                
                DataSet dsChanged = webSync.EndGetDataChanged(ar);
                
                // 保存到本地
                if (dsChanged == null || dsChanged.Tables.Count == 0)
                {
                    return false;
                }
                
                ArrayList arrSql = new ArrayList();
                foreach(DataRow dr in dsChanged.Tables[0].Rows)
                {
                    string sql = "UPDATE PDA_TABLE_LIST SET "
                                +   "FILTER_CONDITION = " + SqlManager.SqlConvert(dr["FILTER_CONDITION"].ToString())
                                +   ",SYNC_MODE = " + SqlManager.SqlConvert(dr["SYNC_MODE"].ToString())
                                + "WHERE "
                                +   "APP_CODE = "  + SqlManager.SqlConvert(dr["APP_CODE"].ToString())
                                +   "AND TABLE_NAME = " + SqlManager.SqlConvert(dr["TABLE_NAME"].ToString());
                    arrSql.Add(sql);
                }
                
                ExecuteNoQuery(ref arrSql);
                
                return true;
            }
            catch(Exception ex)
            {
                addLog(2, "失败 " + ex.Message);
                return false;
            }
        }
        #endregion
        
        
        #region 共通函数
        public bool DeleteRows(string tableName, string filter)
        {
            string sql = "SELECT * FROM " + tableName + " WHERE " + filter;
            DataSet ds = sqlCeAccess.SelectData(sql, tableName);
            
            DataSet dsDel = new DataSet();
            dsDel.Tables.Add(ds.Tables[0].Clone());
                
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow drEdit = ds.Tables[0].Rows[i];
                DataRow drNew = dsDel.Tables[0].NewRow();
                
                foreach(DataColumn dc in ds.Tables[0].Columns)
                {
                    if (dc.ColumnName.Equals(COL_UPDATE_DATE_TIME) == true)
                    {
                        drNew[dc.ColumnName] = DateTime.Now;
                    }
                    else
                    {
                        drNew[dc.ColumnName] = drEdit[dc.ColumnName];
                    }
                }
                
                dsDel.Tables[0].Rows.Add(drNew);
                
                ds.Tables[0].Rows[0].Delete();                
            }
            
            // 删除表中的数据
            sqlCeAccess.Update(ref ds, tableName, sql);
            
            // 保存删除的数据
            tableName += TABLE_TOMBSTONE;
            sql = "SELECT * FROM " + tableName + " WHERE (1 = 2)";
            dsDel.Tables[0].TableName = tableName;
            sqlCeAccess.Update(ref dsDel, tableName, sql);
            
            return true;
        }
        
        
        public string getFilter(string filter)
        {   
            filter = filter.Replace("{WARD_CODE}", "'" + _wardCode + "'");
                        
            return filter;
        }
        
        
        public bool createLogDataSet(string tableName)
        {
            if (dsLog == null)
            {
                if (System.IO.File.Exists("Download.xml") == true)
                {
                    try
                    {
                        dsLog = new DataSet("Download");
                        dsLog.ReadXml("Download.xml");
                    }
                    catch
                    {}
                }
                
                if (dsLog == null)
                { 
                    dsLog = new DataSet("Download");
                }
            }
            
            if (dsLog.Tables.Contains(tableName) == false)
            {
                dsLog.Tables.Add(tableName);
                dsLog.Tables[tableName].Columns.Add("Log", Type.GetType("System.String"));
            }
            
            return true;
        }


        public void addLog(int normalUpDown, string info)
        {
            string tableName = string.Empty;
            switch (normalUpDown)
            {
                case 0:
                    tableName = "Normal";
                    break;
                case 1:
                    tableName = "Up";
                    break;
                case 2:
                    tableName = "Down";
                    break;
                default:
                    return;
            }
            
            try
            {
                createLogDataSet(tableName);
                
                DataRow drError = dsLog.Tables[tableName].NewRow();
                drError[0] = info;
                dsLog.Tables[tableName].Rows.Add(drError);
                
                dsLog.WriteXml(dsLog.DataSetName + ".xml");
            }
            catch
            {}
            
            // 删除过多的log
            if (dsLog != null && dsLog.Tables.Contains(tableName) && dsLog.Tables[tableName].Rows.Count > 200)
            {
                for(int i = 20; i >= 0; i--)
                {
                    dsLog.Tables[tableName].Rows[i].Delete();
                }
                
                dsLog.AcceptChanges();
            }
        }
        
        
        /// <summary>
        /// 删除过期数据(10天前的数据)
        /// </summary>
        public void DeleteOutOfDate()
        {          
            string[] tables = {"EXCHANGEWORK",      "VITAL_SIGNS_REC", "ORDERS_EXECUTE", "PAT_EVALUATION_M"};
            string sql = "DELETE FROM {0} WHERE ";
            
            for(int i = 0; i < tables.Length; i++)
            {
                string tableName    = tables[i];
                string filter       = string.Empty;
                string colCompare   = string.Empty;
                int daysExist       = 10;
                
                try
                {
                    switch(tableName)
                    {
                        case "EXCHANGEWORK":                                // 交接班
                            colCompare = "RECORD_TIME";
                            break;
                        case "VITAL_SIGNS_REC":                             // 生命体征
                            daysExist = 3;
                            colCompare = "RECORDING_DATE";
                            break;
                        case "ORDERS_EXECUTE":                              // 医嘱执行单
                            colCompare = "SCHEDULE_PERFORM_TIME";
                            daysExist = 3;
                            break;
                        case "PAT_EVALUATION_M":                            // 评估单 每日
                            daysExist = 3;
                            filter = "DICT_ID = '01' AND ";
                            colCompare = "RECORD_DATE";
                            break;
                        default:
                            break;
                    }
                    
                    if (colCompare.Length == 0)
                    {
                        continue;
                    }

                    filter += colCompare + " < " + SqlManager.SqlConvert(serverDateTime.AddDays(-1 * daysExist).Date.ToString(ComConst.FMT_DATE.LONG));
                    
                    ExecuteNoQuery(string.Format(sql, tableName) + filter);
                    
                    Thread.Sleep(1 * 1000);
                }
                catch(Exception ex)
                {
                    addLog(2, ex.Message);
                    Thread.Sleep(2 * 1000);
                }
            }
        }
        
        
        /// <summary>
        /// 删除出院病人数据
        /// </summary>
        public void DeleteOutOfPatient()
        {
            // 查找哪些表需要下载
            string sql = "SELECT * FROM PDA_TABLE_LIST WHERE SYNC_MODE = '2'";
            DataSet ds = SelectData(sql);
            
            string tableName = string.Empty;
            
            // 获取现在病人列表
            sql = "SELECT PATIENT_ID FROM PATIENT_INFO ORDER BY PATIENT_ID";
            DataSet dsPatient = SelectData(sql, "PATIENT_INFO");
            
            foreach(DataRow drT in ds.Tables[0].Rows)
            {   
                try
                {
                    tableName = drT["TABLE_NAME"].ToString();
                    
                    // 获取数据表中存在的病人
                    sql = "SELECT DISTINCT PATIENT_ID FROM " + tableName + " ORDER BY PATIENT_ID";
                    DataSet dsPatient1 = SelectData(sql, tableName);
                    
                    // 比较
                    int idx  = 0;
                    int idx1 = 0;
                    int compare = 0;
                    
                    DataRow dr = null;
                    DataRow dr1 = null;
                    
                    sql = "DELETE FROM " + tableName + " WHERE PATIENT_ID = '{0}'";
                    
                    while(idx < dsPatient.Tables[0].Rows.Count 
                        && idx1 < dsPatient1.Tables[0].Rows.Count)
                    {
                        Thread.Sleep(10);
                        
                        dr = dsPatient.Tables[0].Rows[idx];
                        dr1 = dsPatient1.Tables[0].Rows[idx1];
                        
                        compare = dr["PATIENT_ID"].ToString().CompareTo(dr1["PATIENT_ID"].ToString());
                        
                        if (compare < 0)
                        {
                            idx++;
                            continue;
                        }
                        
                        if (compare == 0)
                        {
                            idx++;
                            idx1++;
                            continue;
                        }
                        
                        idx1++;
                        ExecuteNoQuery(string.Format(sql, dr1["PATIENT_ID"].ToString()));
                        Thread.Sleep(100);
                    }
                    
                    for(; idx1 < dsPatient1.Tables[0].Rows.Count; idx1++)
                    {
                        dr1 = dsPatient1.Tables[0].Rows[idx1];
                        ExecuteNoQuery(string.Format(sql, dr1["PATIENT_ID"].ToString()));
                        Thread.Sleep(100);
                    }
                    
                    Thread.Sleep(1 * 1000);
                    
                    // 删除_TombStone中的数据
                    sql = "DELETE FROM " + tableName + TABLE_TOMBSTONE 
                        + " WHERE " + COL_UPDATE_DATE_TIME + " < " + SqlManager.SqlConvert(DateTime.Now.AddDays(-10).ToString(ComConst.FMT_DATE.SHORT));
                    
                    ExecuteNoQuery(sql);
                }
                catch(Exception ex)
                {
                    addLog(2, ex.Message);
                    Thread.Sleep(2 * 1000);
                }
                
                Thread.Sleep(300);
            }            
        }        
        
        
        /// <summary>
        /// 获取系统时间
        /// </summary>
        private void getServerSysDate()
        {
            if (GVars.Demo == true)
            {
                serverTimeGot = true;
                return;
            }
            
            // 获取系统时间
            try
            {
                IAsyncResult ar = userWeb.BeginGetSysDate(null, null);
                
                int count = 0;
                while (ar.IsCompleted == false && count++ < 20)
                {
                    Thread.Sleep(100);
                }
                
                if (ar.IsCompleted)
                {
                    // setLocalSysDate(userWeb.EndGetSysDate(ar));
                    serverDateTime = userWeb.EndGetSysDate(ar);
                    tickCount      = Environment.TickCount;
                    CeAPI.SetLocalDateTime(serverDateTime);
                    
                    serverTimeGot = true;
                    //dateTimeLastGot = userWeb.EndGetSysDate(ar);
                    //tickCount = Environment.TickCount;
                }
            }
            catch(Exception ex)
            {
                addLog(0, ex.Message);
            }
        }
        
        
        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <returns></returns>
        private string getMax(string str1, string str2)
        {
            str2 = str2.Trim();
                        
            // 如果是日期字符串
            if (str2.Contains("-") || str2.Contains(":") || str2.Contains(" ") || str2.Contains("\\") || str2.Contains("/")
                || str1.Contains("-") || str1.Contains(":") || str1.Contains(" ") || str1.Contains("\\") || str1.Contains("/"))
            {
                if (str1.CompareTo(str2) < 0) return str2;
                return str1;
            }
            // 如果是数字
            else
            {
                long val1 = 0;
                long val2 = 0;
                if (str1.Length > 0) val1 = long.Parse(str1);
                if (str2.Length > 0) val2 = long.Parse(str2);
                
                if (val1 < val2) return str2;
                return str1;
            }
        }
        #endregion


    }
}
