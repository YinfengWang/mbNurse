using System;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Text;
using SQL = HISPlus.SqlManager;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

/// <summary>
///DataWebSrv 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class DataWebSrv : System.Web.Services.WebService
{

    private const string COL_UPDATE_DATE_TIME = "UPD_DATE_TIME";  // 本地表中跟踪变化时间的列

    private const string ACTION_INSERT = "1";
    private const string ACTION_UPDATE = "2";
    private const string ACTION_DELETE = "3";
    private HISPlus.OracleAccess oraAccess = new HISPlus.OracleAccess();

    Stopwatch watch;
    public DataWebSrv()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
        oraAccess.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString;
    }


    /// <summary>
    /// 获取PDA 本地库的哪些表表结构发生变化
    /// </summary>
    /// <param name="dtLastUpdate"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetPdaDbTableList(DateTime dtLastUpdate)
    {
        //Application["GetPdaDbTableList"] = (int)Application["GetPdaDbTableList"] + 1;
        string sqlSelCols = "SELECT "
                                + "APP_CODE, TABLE_NAME, FILTER_CONDITION, "
                                + "CREATE_SCRIPT, SYNC_MODE, CREATE_DATE, "
                                + "UPDATE_DATE "
                          + "FROM PDA_DB_TABLE ";

        string sql = sqlSelCols
                   + "WHERE "
                   + "APP_CODE = '002'"
                   + " AND (CREATE_DATE > " + SQL.GetOraDbDate(dtLastUpdate)
                   + " OR UPDATE_DATE > " + SQL.GetOraDbDate(dtLastUpdate)
                   + ")";

        DataSet ds = oraAccess.SelectData(sql, "PDA_DB_TABLE");
        string logJsonValue = string.Empty;
        if (ds == null || ds.Tables.Count == 0)
        {
            #region 日志


            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //    logJsonValue = OperationLog.DataTableConvertJson.Dataset2Json(ds);
            ////日志
            //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(), "dtLastUpdate:" + dtLastUpdate, logJsonValue);

            #endregion

            return ds;
        }

        // 查找 PDA_TABLE_LIST 表是否更新
        DataRow[] drFind = ds.Tables[0].Select("TABLE_NAME = 'PDA_TABLE_LIST'");

        // 如果 PDA_TABLE_LIST 表结构更新, 全部PDA数据表结构都要更新
        if (drFind.Length > 0)
        {
            sql = sqlSelCols;
            ds = oraAccess.SelectData(sql, "PDA_DB_TABLE");
        }

        #region 日志

        //string logJsonValue = string.Empty;
        //if (ds != null && ds.Tables[0].Rows.Count > 0)
        //    logJsonValue = OperationLog.DataTableConvertJson.Dataset2Json(ds);

        ////日志
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(), "dtLastUpdate:" + dtLastUpdate, logJsonValue);

        #endregion

        //OperationLog.OutputApplicationLog("GetPdaDbTableList", (int)Application["GetPdaDbTableList"]);

        return ds;
    }


    /// <summary>
    /// 获取自上次下载以来更新的数据
    /// </summary>
    /// <param name="lastDownScn"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetDataChanged(string tableName, string lastDownDBTS, string filter)
    {
        #region MyRegion

        //开始计时
        watch = Stopwatch.StartNew();
        Application.Lock();
        Application["GetDataChanged"] = (int)Application["GetDataChanged"] + 1;
        string DBTS_FMT = "YYYY-MM-DD HH24:MI:SS.ff";
        string defaultTimeStamp = "TO_TIMESTAMP('1970-01-01 01:01:01', '" + DBTS_FMT + "')";
        string colUpdTimestamp = "TO_CHAR("
                                    + "GREATEST("
                                      + "NVL(CREATE_TIMESTAMP, " + defaultTimeStamp + "),"
                                      + "NVL(UPDATE_TIMESTAMP, " + defaultTimeStamp + ") "
                                      + "), "
                                      + "'" + DBTS_FMT + "'"
                                      + ") DBTS";

        string sql = string.Empty;
        string tableTombstone = tableName + "_Tombstone";
        bool existTombStone = IsExistTombstone(tableName);

        if (lastDownDBTS.Trim().Length > 0)
        {

            if (filter.Trim().Length > 0)
            {
                filter = " AND " + filter;
            }

            string tsFilter = "TO_TIMESTAMP('" + lastDownDBTS + "', '" + DBTS_FMT + "')";

            // 查找插入的记录
            sql = "SELECT " + tableName + ".*, '1' ACTION " + "," + colUpdTimestamp + " FROM " + tableName
                + " WHERE CREATE_TIMESTAMP > " + tsFilter + filter;
            sql += " UNION ";

            // 查找更新的记录
            sql += "SELECT " + tableName + ".*, '2' ACTION " + "," + colUpdTimestamp + " FROM " + tableName
                + " WHERE UPDATE_TIMESTAMP > " + tsFilter
                    + " AND CREATE_TIMESTAMP <= " + tsFilter + filter;

            /*2016-03-04*/
            if (existTombStone == true)
            {
                sql += " UNION ";

                // 查找删除的记录
                sql += "SELECT " + tableTombstone + ".*, '3' ACTIOIN " + "," + colUpdTimestamp + " FROM " + tableTombstone
                    + " WHERE UPDATE_TIMESTAMP > " + tsFilter + filter;
            }
        }
        else
        {
            if (filter.Trim().Length > 0)
            {
                filter = " WHERE " + filter;
            }

            // 查找插入的记录
            sql = "SELECT " + tableName + ".*, '1' ACTION " + "," + colUpdTimestamp
                + " FROM " + tableName + filter;

            if (existTombStone == true)
            {
                sql += " UNION ";

                // 查找删除的记录
                sql += "SELECT " + tableTombstone + ".*, '3' ACTIOIN " + "," + colUpdTimestamp
                    + " FROM " + tableTombstone + filter;
            }
        }

        //refreshVitalSignRec(tableName);

        //DataSet DsChanged = oraAccess.SelectData_NoKey(sql, tableName);


        //结束计时
        watch.Stop();
        #region 日志

        //日志
        //string logJsonValue = string.Empty;
        //if (DsChanged != null && DsChanged.Tables[0].Rows.Count > 0)
        //    logJsonValue = OperationLog.DataTableConvertJson.Dataset2Json(DsChanged);
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
        //    "tableName:" + tableName + "\r\n" + "lastDownDBTS:" + lastDownDBTS + "\r\n" + "filter:" + filter, logJsonValue);
        #endregion

        string time = (Convert.ToDouble(watch.ElapsedMilliseconds) / 1000).ToString("f2") + "秒";
        Application.UnLock();

        string isOpenLog = System.Configuration.ConfigurationManager.AppSettings["isOpenLog"].ToString();

        if (isOpenLog == "T")
        {
            OperationLog.OutputApplicationLogTime("GetDataChanged", (int)Application["GetDataChanged"], time, sql, Context.Request.ServerVariables["REMOTE_HOST"]);
        }


        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
        //   "tableName:" + tableName + "\r\n" + "lastDownDBTS:" + lastDownDBTS + "\r\n" + "filter:" + filter, time);
        //return DsChanged;

        return null;

        #endregion
    }


    /// <summary>
    /// 获取自上次下载以来更新的数据
    /// </summary>
    /// <param name="lastDownScn"></param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetDataChanged2(string tableName, string lastDownDBTS, string filter)
    {
        //Application["GetDataChanged2"] = (int)Application["GetDataChanged2"] + 1;
        string DBTS_FMT = "YYYY-MM-DD HH24:MI:SS";
        string defaultTimeStamp = "TO_DATE('1970-01-01 01:01', '" + DBTS_FMT + "')";
        string colUpdTimestamp = "TO_CHAR("
                                    + "GREATEST("
                                      + "NVL(CREATE_TIMESTAMP, " + defaultTimeStamp + "),"
                                      + "NVL(UPDATE_TIMESTAMP, " + defaultTimeStamp + ") "
                                      + "), "
                                      + "'" + DBTS_FMT + "'"
                                      + ") DBTS";

        string sql = string.Empty;
        string tableTombstone = tableName + "_Tombstone";
        bool existTombStone = IsExistTombstone(tableName);

        if (lastDownDBTS.Trim().Length > 0)
        {
            if (filter.Trim().Length > 0)
            {
                filter = " AND " + filter;
            }

            string tsFilter = "TO_DATE('" + lastDownDBTS + "', '" + DBTS_FMT + "')";

            // 查找插入的记录
            sql = "SELECT " + tableName + ".*, '1' ACTION " + "," + colUpdTimestamp + " FROM " + tableName
                + " WHERE CREATE_TIMESTAMP > " + tsFilter + filter;
            sql += " UNION ";

            // 查找更新的记录
            sql += "SELECT " + tableName + ".*, '2' ACTION " + "," + colUpdTimestamp + " FROM " + tableName
                + " WHERE UPDATE_TIMESTAMP > " + tsFilter
                    + " AND CREATE_TIMESTAMP <= " + tsFilter + filter;

            if (existTombStone == true)
            {
                sql += " UNION ";

                // 查找删除的记录
                sql += "SELECT " + tableTombstone + ".*, '3' ACTIOIN " + "," + colUpdTimestamp + " FROM " + tableTombstone
                    + " WHERE UPDATE_TIMESTAMP > " + tsFilter + filter;
            }
        }
        else
        {
            if (filter.Trim().Length > 0)
            {
                filter = " WHERE " + filter;
            }

            // 查找插入的记录
            sql = "SELECT " + tableName + ".*, '1' ACTION " + "," + colUpdTimestamp
                + " FROM " + tableName + filter;

            if (existTombStone == true)
            {
                sql += " UNION ";

                // 查找删除的记录
                sql += "SELECT " + tableTombstone + ".*, '3' ACTIOIN " + "," + colUpdTimestamp
                    + " FROM " + tableTombstone + filter;
            }
        }
        DataSet DsChanged2 = oraAccess.SelectData_NoKey(sql, tableName);

        //日志
        //string logJsonValue = string.Empty;
        //if (DsChanged2 != null && DsChanged2.Tables[0].Rows.Count > 0)
        //    logJsonValue = OperationLog.DataTableConvertJson.Dataset2Json(DsChanged2);
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
        //    "tableName:" + tableName + "\r\n" + "lastDownDBTS:" + lastDownDBTS + "\r\n" + "filter:" + filter, logJsonValue);

        //OperationLog.OutputApplicationLog("GetDataChanged2", (int)Application["GetDataChanged2"]);

        return DsChanged2;
    }


    /// <summary>
    /// 上传本地的更新的数据
    /// </summary>
    /// <param name="dsChanged"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    [WebMethod]
    public DateTime ApplyDataChange(DataSet dsChanged, string tableName)
    {
        //Application["ApplyDataChange"] = (int)Application["ApplyDataChange"] + 1;

        string sort = COL_UPDATE_DATE_TIME + " ASC, ACTION DESC";
        DataRow[] drFind = dsChanged.Tables[0].Select(string.Empty, sort);

        string sqlSel = "SELECT * FROM " + tableName + " WHERE (1 = 2)";
        DataSet dsLocalEmpty = oraAccess.SelectData(sqlSel, tableName);

        DateTime dtMax = HISPlus.DataType.DateTime_Null();

        for (int i = 0; i < drFind.Length; i++)
        {
            DataRow dr = drFind[i];

            #region 获取主键过滤条件
            string filter = string.Empty;
            DataColumn[] dcPrimary = getTablePrimaryKeys(tableName);

            if (dcPrimary == null) dcPrimary = dsLocalEmpty.Tables[0].PrimaryKey;

            for (int k = 0; k < dcPrimary.Length; k++)
            {
                DataColumn dc = dcPrimary[k];

                if (dsChanged.Tables[0].Columns.Contains(dc.ColumnName) == false)
                {
                    continue;
                }

                if (filter.Length > 0) filter += " AND ";

                if (dc.DataType == System.Type.GetType("System.DateTime"))
                {
                    DateTime dt = (DateTime)(dr[dc.ColumnName]);

                    filter += dc.ColumnName + " = " + SQL.GetOraDbDate(dt);
                }
                else
                {
                    filter += dc.ColumnName + " = " + SQL.SqlConvert(dr[dc.ColumnName].ToString());
                }
            }
            #endregion

            // 记录最大时间点
            DateTime dtUpdate = (DateTime)dr[COL_UPDATE_DATE_TIME];

            if (dtMax.CompareTo(dtUpdate) < 0)
            {
                dtMax = dtUpdate;
            }

            sqlSel = "SELECT * FROM " + tableName + " WHERE " + filter;
            DataSet dsLocal = oraAccess.SelectData(sqlSel, tableName);
            bool blnAdd = false;

            // 如果是更新
            if (dr["ACTION"].ToString().Equals(ACTION_UPDATE) == true)
            {
                DataRow drEdit = null;

                if (dsLocal != null && dsLocal.Tables.Count > 0 && dsLocal.Tables[0].Rows.Count > 0)
                {
                    drEdit = dsLocal.Tables[0].Rows[0];

                    #region 检查能否更新
                    DateTime dtCur;

                    if (drEdit["CREATE_TIMESTAMP"] != null && drEdit["CREATE_TIMESTAMP"].ToString().Length > 0)
                    {
                        dtCur = (DateTime)drEdit["CREATE_TIMESTAMP"];

                        if (dtUpdate.CompareTo(dtCur) < 0)
                        {
                            continue;
                        }
                    }

                    if (drEdit["UPDATE_TIMESTAMP"] != null && drEdit["UPDATE_TIMESTAMP"].ToString().Length > 0)
                    {
                        dtCur = (DateTime)drEdit["UPDATE_TIMESTAMP"];

                        if (dtUpdate.CompareTo(dtCur) < 0)
                        {
                            continue;
                        }
                    }
                    #endregion
                }
                else
                {
                    drEdit = dsLocal.Tables[0].NewRow();
                    blnAdd = true;
                }

                // 赋值
                foreach (DataColumn dc in dsChanged.Tables[0].Columns)
                {
                    if (dc.ColumnName.Equals(COL_UPDATE_DATE_TIME) == false
                        && dc.ColumnName.Equals("ACTION") == false)
                    {
                        drEdit[dc.ColumnName] = dr[dc.ColumnName];
                    }
                }

                // 保存
                if (blnAdd)
                {
                    dsLocal.Tables[0].Rows.Add(drEdit);
                }

                try
                {
                    oraAccess.Update(ref dsLocal, tableName, sqlSel);
                }
                catch (Exception ex)
                {
                    outputError(ex, dsLocal, "I_");
                }
            }

            // 如果是删除
            if (dr["ACTION"].ToString().Equals(ACTION_DELETE) == true)
            {
                if (dsLocal.Tables[0].Rows.Count > 0)
                {
                    dsLocal.Tables[0].Rows[0].Delete();

                    try
                    {
                        oraAccess.Update(ref dsLocal, tableName, sqlSel);
                    }
                    catch (Exception ex)
                    {
                        outputError(ex, dsLocal, "D_");
                    }
                }
            }
        }

        #region 日志
        //string logJsonValue = string.Empty;

        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
        //    "dsChanged:" + OperationLog.DataTableConvertJson.Dataset2Json(dsChanged) + "\r\n" + "tableName:" + tableName, dtMax.ToString());
        #endregion

        //OperationLog.OutputApplicationLog("ApplyDataChange", (int)Application["ApplyDataChange"]);

        return dtMax;
    }


    /// <summary>
    /// 上传本地的更新的数据
    /// </summary>
    /// <param name="dsChanged"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    [WebMethod]
    public DateTime ApplyDataChange2()
    {
        DataSet ds = new DataSet();
        ds.ReadXml(@"d:\VITAL_SIGNS_REC.xml", XmlReadMode.ReadSchema);
        return ApplyDataChange(ds, "VITAL_SIGNS_REC");
    }


    /// <summary>
    /// 获取病人的医嘱
    /// <param name="patientId">病人ID</param>
    /// <param name="visitId">住院次数</param>
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetOrders(string patientId, string visitId)
    {

        //Application.Lock();

        //Application["GetOrders"] = (int)Application["GetOrders"] + 1;

        string sql = @"SELECT WARD_CODE, PATIENT_ID, VISIT_ID, ORDER_NO, ORDER_SUB_NO, REPEAT_INDICATOR, ORDER_CLASS, ORDER_CLASS_NAME, 
                        ORDER_STATUS, ORDER_STATUS_NAME, ORDER_TEXT, DOSAGE, DOSAGE_UNITS, DURATION, DURATION_UNITS, START_DATE_TIME, 
                        STOP_DATE_TIME, FREQUENCY, FREQ_COUNTER, PERFORM_SCHEDULE, PERFORM_RESULT,DOCTOR, 
                        STOP_DOCTOR, NURSE, STOP_NURSE, UPDATE_TIMESTAMP,PROCESSING_NURSE FROM ORDERS_M "
                    + "WHERE nvl(stop_date_time,sysdate+999)>to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd') and PATIENT_ID = " + SQL.SqlConvert(patientId)
                    + "AND VISIT_ID = " + SQL.SqlConvert(visitId);

        DataSet dsOrders = oraAccess.SelectData(sql);

        #region 日志

        //string logValue = string.Empty;
        //if (dsOrders != null && dsOrders.Tables[0].Rows.Count > 0)
        //    logValue = OperationLog.DataTableConvertJson.Dataset2Json(dsOrders);

        ////日志
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(), "patientId:" + patientId + "\r\n" + "visitId:" + visitId, logValue);

        #endregion
        //Application.UnLock();
        //OperationLog.OutputApplicationLog("GetOrders", (int)Application["GetOrders"]);

        return dsOrders;
    }



    #region   读取科室 自定义的 巡视内容
    /// <summary>
    /// 获取科室内对应的巡视内容
    /// </summary>
    /// <param name="deptCode"></param>
    /// <returns></returns>

    [WebMethod]
    public DataSet getDeptCodeCount(string deptCode)
    {
        //Application["getDeptCodeCount"] = (int)Application["getDeptCodeCount"] + 1;

        string sql = "SELECT WARD_CODE,XUNSHICONTENT "
                            + "FROM  CUSTOMCONTENT "
                            + " WHERE  WARD_CODE =" + SQL.SqlConvert(deptCode);


        DataSet DsDeptCodeCount = oraAccess.SelectData(sql, "customcontent");

        #region 日志

        //日志
        //string logJosnValue = string.Empty;
        //if (DsDeptCodeCount != null && DsDeptCodeCount.Tables[0].Rows.Count > 0)
        //    logJosnValue = OperationLog.DataTableConvertJson.Dataset2Json(DsDeptCodeCount);
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(), "deptCode:" + deptCode, logJosnValue);

        #endregion

        //OperationLog.OutputApplicationLog("getDeptCodeCount", (int)Application["getDeptCodeCount"]);

        return DsDeptCodeCount;
    }

    #endregion


    /// <summary>
    /// 获取病人的医嘱执行单
    /// </summary>
    /// <param name="patientId">病人ID</param>
    /// <param name="visitId">住院次数</param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetOrdersExecute(string patientId, string visitId)
    {
        //Application["GetOrdersExecute"] = (int)Application["GetOrdersExecute"] + 1;

        string sql = "SELECT E.* "
                    + "FROM ORDERS_EXECUTE E "
                    + "WHERE "
                        + "E.PATIENT_ID = " + SQL.SqlConvert(patientId)
                        + "AND E.VISIT_ID = " + SQL.SqlConvert(visitId)
                        + "AND (E.SCHEDULE_PERFORM_TIME > SYSDATE - 6/12 AND E.SCHEDULE_PERFORM_TIME < SYSDATE + 6/12) ";
        DataSet dsOrderExecute = oraAccess.SelectData(sql, "ORDERS_EXECUTE");

        #region 日志

        //string logValue = string.Empty;
        //if (dsOrderExecute != null && dsOrderExecute.Tables[0].Rows.Count > 0)
        //    logValue = OperationLog.DataTableConvertJson.Dataset2Json(dsOrderExecute);
        ////日志
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(), "\r\n" + "patientId:" + patientId + "\r\n" + "visitId:" + visitId, logValue);

        #endregion

       // OperationLog.OutputApplicationLog("GetOrdersExecute", (int)Application["GetOrdersExecute"]);

        return dsOrderExecute;
    }


    /// <summary>
    /// 获取病人的医嘱执行单按照时间段
    /// 2015-11-15 修改 SCHEDULE_PERFORM_TIME 时间大于等于开始时间
    /// </summary>
    /// <param name="patientId">病人ID</param>
    /// <param name="start">开始日期</param>
    /// <param name="stop">结束日期</param>
    /// <param name="visitId">住院次数</param>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetOrdersExecuteTime(string patientId, string visitId, string start, string stop)
    {
        //Application["GetOrdersExecuteTime"] = (int)Application["GetOrdersExecuteTime"] + 1;

        watch = Stopwatch.StartNew();
        string sql = @"SELECT E.* "
                    + "FROM ORDERS_EXECUTE E "
                    + "WHERE "
                        + "E.PATIENT_ID = " + SQL.SqlConvert(patientId)
                        + "AND E.VISIT_ID = " + SQL.SqlConvert(visitId)
                        + "AND E.SCHEDULE_PERFORM_TIME >= to_date(" + SQL.SqlConvert(start) + ", 'YYYY-MM-DD HH24:MI:SS') AND E.SCHEDULE_PERFORM_TIME <=to_date(" + SQL.SqlConvert(stop) + ", 'YYYY-MM-DD HH24:MI:SS') ";


        

        //长庆油田职工医院
        //【改变临时医嘱显示时间范围，接近于0点之前来的患者，使他们的临时医嘱在过了0点之后也可以执行。】
        //string sql = "SELECT E.*      " +
        //               "       FROM ORDERS_EXECUTE E     " +
        //               "      WHERE E.PATIENT_ID =" + SQL.SqlConvert(patientId) + "  " +
        //               "        AND E.VISIT_ID = " + SQL.SqlConvert(visitId) + "     " +
        //               "        AND E.SCHEDULE_PERFORM_TIME >=     " +
        //               "            to_date(" + SQL.SqlConvert(start) + ", 'YYYY-MM-DD HH24:MI:SS')     " +
        //               "        AND E.SCHEDULE_PERFORM_TIME <=     " +
        //               "            to_date(" + SQL.SqlConvert(stop) + ", 'YYYY-MM-DD HH24:MI:SS')              " +
        //               "     AND E.REPEAT_INDICATOR=1     " +

        //               "     union all  " +

        //               "  SELECT F.*     " +
        //               "   FROM ORDERS_EXECUTE F     " +
        //               "  WHERE F.PATIENT_ID = " + SQL.SqlConvert(patientId) + "     " +
        //               "    AND F.VISIT_ID =" + SQL.SqlConvert(visitId) + "     " +
        //               "    AND F.SCHEDULE_PERFORM_TIME <=     " +    //<schedule_perform_time+24
        //               "        F.schedule_perform_time + 1     " +
        //               "    AND F.SCHEDULE_PERFORM_TIME >=     " +    //schedule_perform_time> 当前时间减去24小时
        //               "        sysdate - 1      " +
        //               "    AND F.REPEAT_INDICATOR = 0     ";

        
        DataSet DsOrdersExecuteTime = oraAccess.SelectData(sql, "ORDERS_EXECUTE");
        watch.Stop();

        #region 日志

        //string logValue = string.Empty;
        string time = (Convert.ToDouble(watch.ElapsedMilliseconds) / 1000).ToString("f2") + "秒";
        //if (DsOrdersExecuteTime != null && DsOrdersExecuteTime.Tables[0].Rows.Count > 0)
        //    logValue = OperationLog.DataTableConvertJson.Dataset2Json(DsOrdersExecuteTime);
        //日志
        OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
            "patientId:" + patientId + "\r\n" + "visitId:" + visitId + "\r\n" + "start:" + start + "\r\n" + "stop:" + stop, time);

        #endregion


        //OperationLog.OutputApplicationLogTime("GetOrdersExecuteTime", 1,"1",sql,"1");

        return DsOrdersExecuteTime;
    }



    /// <summary>
    /// 压缩二进制数据
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public byte[] Compress(byte[] data)
    {
        MemoryStream ms = new MemoryStream();
        Stream zipStream = null;
        using (zipStream = new GZipStream(ms, CompressionMode.Compress, true))
        {
            zipStream.Write(data, 0, data.Length);
            zipStream.Close();
        }
        ms.Position = 0;
        byte[] compressedData = new byte[ms.Length];
        ms.Read(compressedData, 0, int.Parse(ms.Length.ToString()));
        return compressedData;
    }

    /// <summary>
    /// 获取病人的医嘱执行单
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public bool SaveData(DataSet ds)
    {
        //Application["SaveData"] = (int)Application["SaveData"] + 1;

        oraAccess.Update(ref ds);

        //OperationLog.OutputApplicationLog("SaveData", (int)Application["SaveData"]);
        return true;
    }


    /// <summary>
    /// 获取病人的巡视记录
    /// <param name="patientId">病人ID</param>
    /// <param name="visitId">住院次数</param>
    /// <param name="wardCode">护理单元ID</param>
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public DataSet GetXunShis(string patientId, string visitId, string wardCode)
    {
        //Application["GetXunShis"] = (int)Application["GetXunShis"] + 1;

        watch = Stopwatch.StartNew();
        StringBuilder sb = new StringBuilder();
        sb.Append(" SELECT E.* FROM XUNSHI E ");
        sb.Append(" WHERE E.PATIENT_ID = " + SQL.SqlConvert(patientId));
        sb.Append(" AND E.VISIT_ID = " + SQL.SqlConvert(visitId));
        sb.Append(" AND E.WARD_CODE = " + SQL.SqlConvert(wardCode));
        sb.Append(" AND E.EXECUTE_DATE > SYSDATE - 1");

        DataSet DsXunShi = oraAccess.SelectData(sb.ToString(), "XUNSHI");

        watch.Stop();

        #region 日志


        string time = (Convert.ToDouble(watch.ElapsedMilliseconds) / 1000).ToString("f2") + "秒";
        //string logJsonValue = string.Empty;
        //if (DsXunShi != null && DsXunShi.Tables[0].Rows.Count > 0)
        //    logJsonValue = OperationLog.DataTableConvertJson.Dataset2Json(DsXunShi);

        ////日志
        OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(), "patientId:" + patientId + "\r\n" +
            "visitId:" + visitId + "\r\n" + "start", time);

        //OperationLog.OutputApplicationLog("GetXunShis", (int)Application["GetXunShis"]);

        #endregion

        return DsXunShi;
    }


    /// <summary>
    /// 获取表的主键
    /// </summary>
    /// <returns></returns>
    private DataColumn[] getTablePrimaryKeys(string tableName)
    {
        //Application["getTablePrimaryKeys"] = (int)Application["getTablePrimaryKeys"] + 1;

        tableName = tableName.ToUpper();

        if (tableName.Equals("ORDERS_EXECUTE") == true)
        {
            DataColumn[] dcPrimary = new DataColumn[5];

            dcPrimary[0] = new DataColumn("PATIENT_ID", Type.GetType("System.String"));
            dcPrimary[1] = new DataColumn("VISIT_ID", Type.GetType("System.String"));
            dcPrimary[2] = new DataColumn("ORDER_NO", Type.GetType("System.String"));
            dcPrimary[3] = new DataColumn("ORDER_SUB_NO", Type.GetType("System.String"));
            dcPrimary[4] = new DataColumn("SCHEDULE_PERFORM_TIME", Type.GetType("System.DateTime"));

            //OperationLog.OutputApplicationLog("getTablePrimaryKeys", (int)Application["getTablePrimaryKeys"]);

            return dcPrimary;
        }

        return null;
    }


    /// <summary>
    /// 判断是否存在删除表
    /// </summary>
    /// <returns></returns>
    private bool IsExistTombstone(string tableName)
    {
        //Application["IsExistTombstone"] = (int)Application["IsExistTombstone"] + 1;

        tableName = tableName.ToUpper();

        string sql = "SELECT * FROM ALL_TABLES WHERE UPPER(TABLE_NAME) = " + SQL.SqlConvert(tableName + "_TOMBSTONE");


        //OperationLog.OutputApplicationLog("IsExistTombstone", (int)Application["IsExistTombstone"]);

        return oraAccess.SelectValue(sql);
    }


    private bool refreshVitalSignRec(string tableName)
    {
        //Application["refreshVitalSignRec"] = (int)Application["refreshVitalSignRec"] + 1;

        if (tableName.Trim().ToUpper().Equals("VITAL_SIGNS_REC") == false)
        {
            return true;
        }

        try
        {
            string sql = "DELETE FROM VITAL_SIGNS_REC_TOMBSTONE WHERE RECORDING_DATE < SYSDATE - 3";
            oraAccess.ExecuteNoQuery(sql);

            sql = "update VITAL_SIGNS_REC_TOMBSTONE "
                    + "set vital_code = (SELECT VITAL_CODE FROM NURSING_ITEM_DICT WHERE VITAL_SIGNS = VITAL_SIGNS_REC_TOMBSTONE.VITAL_SIGNS and rownum = 1),"
                    + "class_code = (SELECT class_code FROM NURSING_ITEM_DICT WHERE VITAL_SIGNS = VITAL_SIGNS_REC_TOMBSTONE.VITAL_SIGNS and rownum = 1) "
                    + "where vital_code is null or vital_code = '' or class_code is null or class_code = ''";
            oraAccess.ExecuteNoQuery(sql);


            //OperationLog.OutputApplicationLog("refreshVitalSignRec", (int)Application["refreshVitalSignRec"]);

            return true;
        }
        catch
        {
            return false;
        }
    }


    private void outputError(Exception ex, DataSet ds, string action)
    {
        //Application["outputError"] = (int)Application["outputError"] + 1;


        try
        {
            if (ds == null || ds.Tables.Count == 0) return;

            DataSet dsIn = ds.Copy();
            if (dsIn.Tables[0].Select(string.Empty, string.Empty, DataViewRowState.Deleted).Length > 0) dsIn.RejectChanges();

            dsIn.Tables[0].Columns.Add("ERROR_DESC", typeof(string));

            if (dsIn.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsIn.Tables[0].Rows[0];
                dr["ERROR_DESC"] = ex.Message;
            }
            else
            {
                dsIn.Tables[0].PrimaryKey = null;
                DataRow drNew = dsIn.Tables[0].NewRow();
                drNew["ERROR_DESC"] = ex.Message;
                dsIn.Tables[0].Rows.Add(drNew);
            }

            string path = HttpContext.Current.Server.MapPath("APP_DATA");
            string fileName = action + Guid.NewGuid().ToString();
            dsIn.WriteXml(System.IO.Path.Combine(path, fileName), XmlWriteMode.WriteSchema);
        }
        catch
        { }
        finally
        {
            //OperationLog.OutputApplicationLog("outputError", (int)Application["outputError"]);
        }
    }

    /// <summary>
    /// 获取PDA客户端超时时间
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public int GetPdaClientTimeout()
    {
        //Application["GetPdaClientTimeout"] = (int)Application["GetPdaClientTimeout"] + 1;

        int _resultValue = 10;
        string pdaTimeout = string.Empty;
        try
        {
            pdaTimeout = "SELECT A.PARAMETER_VALUE FROM APP_CONFIG A    WHERE A.PARAMETER_NAME='PDA_TIMEOUT'";
            DataSet DsPdaClientTimeout = oraAccess.SelectData(pdaTimeout, "APP_CONFIG");
            if (DsPdaClientTimeout != null && DsPdaClientTimeout.Tables[0].Rows.Count > 0)
            {
                _resultValue = Convert.ToInt32(DsPdaClientTimeout.Tables[0].Rows[0][0]);
            }
            else
            {
                _resultValue = 10;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            _resultValue = 10;
        }

        //OperationLog.OutputApplicationLog("GetPdaClientTimeout", (int)Application["GetPdaClientTimeout"]);

        return _resultValue;
    }

    /// <summary>
    /// 保存到Mobile的SPECIMENT_FLOW_REC表  
    /// </summary>
    /// <param name="dsChanged"></param>
    /// <returns></returns>
    [WebMethod]
    public bool SaveSpeciment(DataSet dsChanged)
    {
        //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
        //    "dsChanged:" + OperationLog.DataTableConvertJson.Dataset2Json(dsChanged), true.ToString());

        try
        {
            if (dsChanged == null || dsChanged.Tables.Count == 0 || dsChanged.Tables[0].Rows.Count == 0)
            {
                //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
                //    "dsChanged:" + OperationLog.DataTableConvertJson.Dataset2Json(dsChanged), true.ToString());
                return true;
            }

            // 删除
            DataRow[] drFind = dsChanged.Tables[0].Select(string.Empty, string.Empty, DataViewRowState.ModifiedCurrent | DataViewRowState.ModifiedOriginal);
            for (int i = 0; i < drFind.Length; i++)
            {
                string sql = "DELETE FROM SPECIMENT_FLOW_REC WHERE TEST_NO = " + SQL.SqlConvert(drFind[i]["TEST_NO"].ToString());
                oraAccess.ExecuteNoQuery(sql);
            }

            if (drFind.Length > 0)
            {
                //OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
                //    "dsChanged:" + OperationLog.DataTableConvertJson.Dataset2Json(dsChanged), true.ToString());
                return true;
            }

            // 插入HIS
            int insMobileCount = oraAccess.Update(ref dsChanged, "SPECIMENT_FLOW_REC", "SELECT * FROM SPECIMENT_FLOW_REC");


            //日志
            OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
           "insMobileCount:" + insMobileCount, true.ToString());
        }
        catch (Exception ex)
        {
            OperationLog.OutputOperationLog(HttpContext.Current.Request.Url.ToString(), OperationLog.getMethodName(),
            "异常:" + ex.Message, false.ToString());
            return false;
        }




        return true;
    }

    /// <summary>
    /// 插入巡视内容
    /// </summary>
    /// <param name="ward_code">病区代码</param>
    /// <param name="patient_id">病人ID</param>
    /// <param name="visit_id">住院次数</param>
    /// <param name="nurse">巡视护士</param>
    /// <param name="execute_date">时间</param>
    /// <param name="content">内容</param>
    /// <returns></returns>
    public bool InsXunShi(string ward_code, string patient_id, string visit_id, string nurse, string execute_date, string content)
    {
        try
        {
            string sql = "";
            oraAccess.ExecuteNoQuery(sql);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
}

