//  HS  2012 年 护理路径程序配套程序 之  药疗医嘱拆分   只拆 分  医嘱 类型 为A  类的。

using System;
using System.Data;
using System.Collections;
using System.Threading;
using System.Text;
using Microsoft.Win32;

using DbType = HISPlus.SqlManager.DB_TYPE;
using DbFieldType = HISPlus.SqlManager.FIELD_TYPE;
using SQL = HISPlus.SqlManager;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace HISPlus
{

    /// <summary>
    /// ConvertFunction 的摘要说明。
    /// </summary>
    public class OrderSplitter
    {
        #region 变量
        private int instanceCount = 0;                            // 实例数
        private DateTime dtNow;
        private DataSet dsOrders = null;
        private DataSet dsOrdersExecute = null;
        private DataSet dsOrdersExecuteStruct = null;
        private string nurseName = "SYSTEM";
        private DataSet dsErr = new DataSet();
        private string wardCode = string.Empty;
        private bool blnExit = false;
        private Mutex locker = new Mutex();                          // 互斥锁        
        private ArrayList arrSql_Del = new ArrayList();

        private List<DeleteInfo> deleteInfos = new List<DeleteInfo>();
        private Queue stTraceInfo = new Queue(210);
        private DbAccess oracleAccess = null;
        private long rowCount = 0;
        private DateTime dtBegin = DateTime.MinValue;

        public static List<SyncInfo> syncInfos = new List<SyncInfo>();
        #endregion

        #region
        private class DeleteInfo
        {
            public string Msg;
            public string Sql;
        }

        public class SyncInfo
        {
            public string Comment;
            public string SrcSqlFile;
            public string DestSql;
            public string Filter;
            public string TableName;
        }

        #endregion

        #region 属性
        public string TraceInfo
        {
            get
            {
                try
                {
                    locker.WaitOne();

                    StringBuilder sb = new StringBuilder();
                    object[] objects = stTraceInfo.ToArray();
                    for (int i = objects.Length - 1; i >= 0; i--)
                    {
                        sb.Append(objects[i].ToString());
                        sb.Append(ComConst.STR.CRLF);
                    }

                    return sb.ToString();
                }
                finally
                {
                    locker.ReleaseMutex();
                }
            }
            set
            {
                try
                {
                    locker.WaitOne();
                    //stTraceInfo.Push(DateTime.Now.ToLongTimeString() + ": " +value);
                    stTraceInfo.Enqueue(DateTime.Now.ToLongTimeString() + ": " + value);
                    while (stTraceInfo.Count > 200)
                    {
                        //stTraceInfo.Pop();
                        stTraceInfo.Dequeue();
                    }
                }
                finally
                {
                    locker.ReleaseMutex();
                }
            }
        }


        /// <summary>
        /// 变更数据条数
        /// </summary>
        public long RowCountChanged
        {
            get
            {
                try
                {
                    locker.WaitOne();

                    return rowCount;
                }
                finally
                {
                    locker.ReleaseMutex();
                }
            }
            set
            {
                try
                {
                    locker.WaitOne();

                    rowCount += value;

                    if (dtBegin.Equals(DateTime.MinValue) == true)
                    {
                        dtBegin = dtNow;
                    }
                }
                finally
                {
                    locker.ReleaseMutex();
                }
            }
        }
        #endregion

        #region 构造属性
        public OrderSplitter()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //

            // 创建表
            DataTable dtNew = new DataTable();
            dsErr.Tables.Add(dtNew);

            // 创建该表的字段
            dtNew.Columns.Add("PATIENT_ID", Type.GetType("System.String"));
            dtNew.Columns.Add("VISIT_ID", Type.GetType("System.String"));
            dtNew.Columns.Add("ORDER_NO", Type.GetType("System.String"));
            dtNew.Columns.Add("ORDER_SUB_NO", Type.GetType("System.String"));
            dtNew.Columns.Add("ERR_DESC", Type.GetType("System.String"));
            dtNew.Columns.Add("ORDER_CODE", Type.GetType("System.String"));

            // 设置主键
            DataColumn[] dcPrimeKey = new DataColumn[4];
            dcPrimeKey[0] = dtNew.Columns["PATIENT_ID"];
            dcPrimeKey[1] = dtNew.Columns["VISIT_ID"];
            dcPrimeKey[2] = dtNew.Columns["ORDER_NO"];
            dcPrimeKey[3] = dtNew.Columns["ORDER_SUB_NO"];
            // dcPrimeKey[4] = dtNew.Columns["ORDER_CODE"];

            dtNew.PrimaryKey = dcPrimeKey;

            // 变量赋值
            wardCode = GVars.User.DeptCode;

            if ((GVars.IniFile.ReadString("APP", "ORA_NLS_ZHS", "TRUE").ToUpper().Trim().Equals("TRUE")) == true)
            {
                oracleAccess = new OracleAccess();
            }
            else
            {
                oracleAccess = new OleDbAccess();
            }

            oracleAccess.ConnectionString = GVars.OracleMobile.ConnectionString;
        }
        #endregion

        #region 加载同步信息
        /// <summary>
        /// 加载同步信息
        /// </summary>
        /// <returns></returns>
        public bool loadSyncInfo()
        {
            syncInfos.Clear();

            // 判断文件是否存在
            string dllName = this.GetType().Module.Name.Replace(".dll", "");
            string strNum = dllName.Substring(dllName.Length - 1);
            string fileName = Path.Combine(Path.Combine(Application.StartupPath, "SQL"), "List" + strNum + ".INI");
            if (File.Exists(fileName) == false)
            {
                throw new Exception(fileName + "不存在!");
            }

            // 获取文件内容
            StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("gb2312"));
            string content = sr.ReadToEnd();
            sr.Close();

            content = content.Replace("\n", ComConst.STR.BLANK);

            // 解析文件
            string[] parts = content.Split('\r');
            string line = string.Empty;
            SyncInfo syncItem = null;
            for (int i = 0; i < parts.Length; i++)
            {
                line = parts[i].Trim();

                // 注释
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    if (syncItem != null) syncInfos.Add(syncItem);
                    syncItem = null;

                    line = line.Substring(1, line.Length - 2);
                    string[] parts2 = line.Split(",".ToCharArray());
                    if (parts2.Length == 2)
                    {
                        syncItem = new SyncInfo();
                        syncItem.Comment = parts2[0].Trim();
                        syncItem.TableName = parts2[1].Trim();
                    }

                    continue;
                }

                if (syncItem == null)
                {
                    continue;
                }

                // src:  
                if (line.StartsWith("src:") == true)
                {
                    syncItem.SrcSqlFile = line.Substring(4).Trim();
                    continue;
                }

                // dest:
                if (line.StartsWith("dest:") == true)
                {
                    syncItem.DestSql = line.Substring(5).Trim();
                    continue;
                }
                //filter:
                if (line.StartsWith("filter:") == true)
                {
                    syncItem.Filter = line.Substring(7).Trim();
                    continue;
                }
            }

            if (syncItem != null) syncInfos.Add(syncItem);

            return (syncInfos.Count > 0);
        }


        #endregion

        #region 共通函数

        #region 拆分医嘱
        /// <summary>
        /// 拆分医嘱
        /// </summary>
        /// <param name="wardCode"></param>
        /// <returns></returns>
        public void SplitOrders()
        {
            DateTime dtBegin = DateTime.Now;
            long counter = 0;

            while (true)
            {
                try
                {
                    //LB20110712添加,加载同步信息
                    loadSyncInfo();
                    //LB20110712添加结束

                    dsErr.Tables[0].Rows.Clear();

                    // 处理字符集
                    setHisNlsLang();


                    // 获取当前时间
                    dtNow = GetSysDate();
                    TraceInfo = "获取系统日期: " + dtNow.ToString(ComConst.FMT_DATE.LONG);

                    // 查找所有医嘱
                    if (blnExit == true) { return; }
                    dsOrders = GetOrdersToday(wardCode);
                    TraceInfo = "获取病区所有医嘱 共:" + dsOrders.Tables[0].Rows.Count.ToString() + "条";

                    // 查找今天以后(含今天)医嘱执行单

                    if (blnExit == true) { return; }
                    dsOrdersExecute = GetOrdersExecuteSplitted(wardCode, dtNow.ToString(ComConst.FMT_DATE.SHORT));
                    TraceInfo = "获取已拆分医嘱执行单 共:" + dsOrdersExecute.Tables[0].Rows.Count.ToString() + "条";

                    // 进行医嘱拆分(二天内)
                    //
                    int splitCount = Convert.ToInt32(GVars.IniFile.ReadString("APP", "SPLIT_DATES", "2"));
                    for (int i = 1; i <= splitCount; i++)
                    {
                        // 进行拆分
                        TraceInfo = "开始进行医嘱数据拆分程序 第 " + i.ToString() + "天数据拆分...";
                        dtBegin = DateTime.Now;

                        if (blnExit == true) { return; }
                        OrdersConvertExecute(i);

                        // 进行保存
                        if (blnExit == true) { return; }
                        saveSplitResult();

                        // 保存拆分失败的原因
                        if (i == 1)
                        {
                            saveSplitError();
                        }

                        // 确保一次完整的拆分循环的时间在 1分钟(60秒)以上
                        while (DateTime.Now.Subtract(dtBegin).TotalSeconds <= 20)
                        {
                            if (blnExit == true) { return; }
                            Thread.Sleep(100);
                        }
                    }

                    // 删除作废医嘱(包括orders_m已经拆分为orders_execute的记录，本次进行上游数据和orders_m对接的时候,发现有不是新开的和执行的医嘱单，orders_m将被删除，数据进入备份表orders_m_tombstone表，)
                    //详细见方法里面的说明
                    if (counter++ % 10 == 1)
                    {
                        DeleteOrdersExecuteInvalid();
                    }

                    //拆分全院的时候ORDERS_EXECUTE_TOMBSTONE数据量可能很大，必须删除超过45天的以前的数据
                    if (counter % 30 == 1)
                    {
                        //加载删除信息
                        loadDeleteInfos();
                        //执行删除语句
                        for (int i = 0; i < deleteInfos.Count; i++)
                        {
                            DeleteInfo delinfo = deleteInfos[i];
                            oracleAccess.ExecuteNoQuery(delinfo.Sql);
                            TraceInfo = DateTime.Now.ToString(ComConst.FMT_DATE.LONG) + delinfo.Msg;
                            Thread.Sleep(1000);
                        }
                    }

                    TraceInfo = " HS    -- 一个循环结束 进行 下一个循环 ！！!";

                    // 体息10秒
                    for (int j = 0; j < 10 * 10; j++)
                    {
                        if (blnExit == true) { return; }
                        Thread.Sleep(100);
                    }
                }
                catch (Exception ex)
                {
                    TraceInfo = "后台拆分线程出错" + ex.Message;        // 异常日志                    
                }
            }
        }
        #endregion

        #region 加载需要删除的信息
        /// <summary>
        /// 加载需要删除的信息
        /// </summary>
        /// <returns></returns>
        public bool loadDeleteInfos()
        {
            deleteInfos.Clear();
            // 判断文件是否存在
            string fileName = Path.Combine(Path.Combine(Application.StartupPath, "SQL"), "DeleteInfo.sql");

            if (File.Exists(fileName) == false)
            {
                throw new Exception(fileName + "不存在!");
            }

            // 获取文件内容
            StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("gb2312"));
            string content = sr.ReadToEnd();
            sr.Close();

            content = content.Replace("\n", ComConst.STR.BLANK);

            // 解析文件
            string[] parts = content.Split('\r');
            string line = string.Empty;
            DeleteInfo delItem = null;
            for (int i = 0; i < parts.Length; i++)
            {
                line = parts[i].Trim();

                // 注释
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    if (delItem != null) deleteInfos.Add(delItem);
                    delItem = null;

                    line = line.Substring(1, line.Length - 2);
                    string[] parts2 = line.Split(",".ToCharArray());
                    if (parts2.Length == 2)
                    {
                        delItem = new DeleteInfo();
                        delItem.Msg = parts2[0].Trim();
                        delItem.Sql = parts2[1].Trim();
                    }

                    continue;
                }

                if (delItem == null)
                {
                    continue;
                }
            }

            if (delItem != null) deleteInfos.Add(delItem);

            return (deleteInfos.Count > 0);
        }

        #endregion

        #region 查找需要拆分的医嘱
        /// <summary>
        /// 查找需要拆分的医嘱
        /// </summary>
        /// <param name="wardCode">病区代码</param>
        /// <returns></returns>
        public DataSet GetOrdersToday(string wardCode)
        {
            string sql = "SELECT * FROM ORDERS_M WHERE ORDER_CLASS = 'A' ";
            SyncInfo syncInfo = syncInfos[0];
            sql += " and ward_code in (" + syncInfo.Filter + ")";
            return oracleAccess.SelectData(sql);
        }
        #endregion

        #region 获取已经拆分了的医嘱
        /// <summary>
        /// 获取已经拆分了的医嘱
        /// </summary>
        /// <param name="wardCode">病区代码</param>
        /// <param name="conversionDate">拆分时间</param>
        /// <returns></returns>
        public DataSet GetOrdersExecuteSplitted(string wardCode, string conversionDate)
        {
            string sqlSel = "SELECT ORDERS_EXECUTE.* ";
            sqlSel += "FROM ";
            sqlSel += "ORDERS_EXECUTE, ";                              // 医嘱执行单
            sqlSel += "PATIENT_INFO ";                             // 在院病人

            sqlSel += "WHERE ";
            sqlSel += "ORDERS_EXECUTE.CONVERSION_DATE_TIME >= " + SQL.GetOraDbDate_Short(conversionDate);
            sqlSel += " AND PATIENT_INFO.PATIENT_ID = ORDERS_EXECUTE.PATIENT_ID ";
            sqlSel += " AND PATIENT_INFO.VISIT_ID = ORDERS_EXECUTE.VISIT_ID  and  ORDER_CLASS = 'A'  ";
            SyncInfo syncInfo = syncInfos[0];
            sqlSel += " and PATIENT_INFO.ward_code in (" + syncInfo.Filter + ")";
            DataSet ds = oracleAccess.SelectData(sqlSel, "ORDERS_EXECUTE");
            // 设置主键
            DataColumn[] dcPrimary = new DataColumn[6];
            dcPrimary[0] = ds.Tables[0].Columns["PATIENT_ID"];
            dcPrimary[1] = ds.Tables[0].Columns["VISIT_ID"];
            dcPrimary[2] = ds.Tables[0].Columns["ORDER_NO"];
            dcPrimary[3] = ds.Tables[0].Columns["ORDER_SUB_NO"];
            dcPrimary[4] = ds.Tables[0].Columns["CONVERSION_DATE_TIME"];
            dcPrimary[5] = ds.Tables[0].Columns["SCHEDULE_PERFORM_TIME"];
            //dcPrimary[6] = ds.Tables[0].Columns["ORDER_CODE"];

            ds.Tables[0].PrimaryKey = dcPrimary;

            return ds;
        }
        #endregion

        #region  拆分医嘱以生成医嘱执行单
        /// <summary>
        /// 拆分医嘱以生成医嘱执行单
        /// </summary>
        /// <remarks>
        ///  例外: 1. 对于长期医嘱, 如果没有时间间隔不进行拆分
        /// </remarks>
        /// <param name="dsOrders">医嘱</param>
        /// <param name="dsOrdersExecute">医嘱执行单</param>
        /// <param name="nurseName">护士名称</param>
        public void OrdersConvertExecute(int days)
        {
            #region 中间变量定义
            string patientId = string.Empty;                                         // 病人ID
            string visitId = string.Empty;                                         // 本次就诊序号
            string orderNo = string.Empty;                                         // 医嘱序号
            string orderSubNo = string.Empty;                                         // 子医嘱序号
            string orderStatus = string.Empty;                                         // 医嘱状态
            DateTime dtOrderStart;                                                           // 医嘱开始时间
            string orderStopDate = string.Empty;                                         // 医嘱结束时间
            DateTime dtOrderStop;                                                            // 医嘱结束时间

            string schedule = string.Empty;                                         // 计划执行时间
            DateTime dtSchedule;                                                             // 计划执行时间

            DataRow drExecuteNew = null;                                                 // 新增的医嘱执行单
            string frequency = string.Empty;                                         // 医嘱频率描述
            bool blnOnNeedTime = false;                                                // 医嘱执行时间为必要时

            string filter = string.Empty;                                         // 过滤条件
            string filterOra = string.Empty;
            string filterTemp = string.Empty;                                         // 临时过滤条件		    
            DataRow[] drFind = null;

            string time = string.Empty;                                         // 时间字符串

            DateTime dtTreate = dtNow.AddDays(days - 1);
            #endregion

            int counter = 0;

            DataRow[] drOrders = dsOrders.Tables[0].Select("", "PATIENT_ID, VISIT_ID, ORDER_NO, ORDER_SUB_NO");

            for (int c = 0; c < drOrders.Length; c++)
            {
                DataRow drOrder = drOrders[c];

                counter++;
                if (counter % 1000 == 500) TraceInfo = "已拆分 " + counter + "条";

                if (blnExit) return;

                try
                {
                    #region 获取当前医嘱的信息
                    patientId = drOrder["PATIENT_ID"].ToString();                               // 病人ID号
                    visitId = drOrder["VISIT_ID"].ToString();                                 // 本次就诊序号
                    orderNo = drOrder["ORDER_NO"].ToString();                                 // 医嘱序号
                    orderSubNo = drOrder["ORDER_SUB_NO"].ToString();                             // 子医嘱序号
                    orderStatus = drOrder["ORDER_STATUS"].ToString();                             // 医嘱状态

                    dtOrderStart = DateTime.Parse(drOrder["START_DATE_TIME"].ToString());          // 医嘱开始日期
                    frequency = drOrder["FREQUENCY"].ToString().Trim();                         // 频率描述
                    blnOnNeedTime = frequency.Equals("必要时");                                     // 是否为必要时
                    orderStopDate = drOrder["STOP_DATE_TIME"].ToString();                           // 医嘱停止日期

                    string freqIntervalUnit = drOrder["FREQ_INTERVAL_UNIT"].ToString().Trim();   	// 时间间隔单位

                    if (orderStopDate.Length > 0)
                    {
                        dtOrderStop = DateTime.Parse(orderStopDate);
                    }
                    else
                    {
                        dtOrderStop = dtTreate.AddDays(1);
                    }
                    #endregion

                    #region 过滤条件预备
                    filter = "PATIENT_ID = " + SQL.SqlConvert(patientId)
                            + " AND VISIT_ID = " + SQL.SqlConvert(visitId)
                            + " AND ORDER_NO = " + SQL.SqlConvert(orderNo)
                            + " AND ORDER_SUB_NO = " + SQL.SqlConvert(orderSubNo);

                    filterOra = filter;

                    filter += " AND (CONVERSION_DATE_TIME >= " + SQL.SqlConvert(dtTreate.ToString(ComConst.FMT_DATE.SHORT))
                            + " AND  CONVERSION_DATE_TIME < " + SQL.SqlConvert(dtTreate.AddDays(1).ToString(ComConst.FMT_DATE.SHORT)) + ")";
                    #endregion

                    #region 如果医嘱状态不为可执行 (2), 删除未执行医嘱执行单
                    //if ("26".IndexOf(orderStatus) < 0)
                    //{
                    //    filterTemp = "PATIENT_ID = " + SQL.SqlConvert(patientId)
                    //        + " AND VISIT_ID = " + SQL.SqlConvert(visitId)
                    //        + " AND ORDER_NO = " + SQL.SqlConvert(orderNo)
                    //        + " AND ORDER_SUB_NO = " + SQL.SqlConvert(orderSubNo)                            
                    //        + " AND (IS_EXECUTE IS NULL OR IS_EXECUTE = '0') ";

                    //    drFind = dsOrdersExecute.Tables[0].Select(filterTemp);
                    //    for (int j = 0; j < drFind.Length; j++)
                    //    {
                    //        drFind[j].Delete();
                    //    }

                    //    continue;
                    //}
                    #endregion

                    #region 医嘱类型检查
                    // 如果不能确定是临时医嘱还是长期医嘱就不能区分

                    string repeator = drOrder["REPEAT_INDICATOR"].ToString().Trim();
                    if ("1".Equals(repeator) == false && "0".Equals(repeator) == false)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "不能确定是长期医嘱 或是临时医嘱!");
                        continue;
                    }
                    else if ("1".Equals(repeator) == true && frequency.Length == 0)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "长期医嘱，频次不能为空!");
                        continue;
                    }

                    #endregion

                    #region 临时医嘱折分
                    if ("1".Equals(repeator) == false)
                    {
                        drFind = dsOrdersExecute.Tables[0].Select(filter);

                        // 如果已经拆分过, 查找下一条医嘱
                        if (drFind.Length > 0) { continue; }

                        // 进行拆分
                        if (dtTreate.Date.CompareTo(dtOrderStart.Date) == 0)
                        {
                            try
                            {
                                drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                                fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, dtOrderStart.ToString(ComConst.FMT_DATE.LONG));
                                dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                            }
                            catch (Exception ex)
                            {
                                addErrOrder(days, patientId, visitId, orderNo, orderSubNo, ex.Message);
                            }
                        }

                        continue;
                    }
                    #endregion

                    #region 如果有停止时间, 删除超除停止时间的医嘱执行单
                    if (orderStopDate.Length > 0 && days == 1)
                    {
                        filterTemp = "PATIENT_ID = " + SQL.SqlConvert(patientId)
                            + " AND VISIT_ID = " + SQL.SqlConvert(visitId)
                            + " AND ORDER_NO = " + SQL.SqlConvert(orderNo)
                            + " AND ORDER_SUB_NO = " + SQL.SqlConvert(orderSubNo)
                            + " AND SCHEDULE_PERFORM_TIME >= " + SQL.SqlConvert(dtOrderStop.ToString(ComConst.FMT_DATE.LONG));

                        drFind = dsOrdersExecute.Tables[0].Select(filterTemp);
                        for (int j = 0; j < drFind.Length; j++)
                        {
                            drFind[j].Delete();
                        }
                    }
                    #endregion

                    #region 不拆分三天前停止的医嘱
                    if ((dtOrderStop.Subtract(dtTreate.AddDays(-3))).Days <= 0)
                    {
                        continue;
                    }
                    #endregion

                    #region 条件检查
                    // 如果当天的医嘱执行单存在, 不进行拆分
                    if (blnOnNeedTime == true)
                    {
                        drFind = dsOrdersExecute.Tables[0].Select(filter + " AND (IS_EXECUTE IS NULL OR IS_EXECUTE <> '1')");
                    }
                    else
                    {
                        drFind = dsOrdersExecute.Tables[0].Select(filter);
                    }

                    // 如果已经拆分过, 查找下一条医嘱
                    if (drFind.Length > 0) { continue; }

                    // 医嘱状态不等于2或者6，但其实在没有到时间以前，该医嘱还有效
                    //// 如果状态不为可执行, 查找下一条医嘱  。停止时间  可能是明天或者更 ····
                    //if ("26".IndexOf(orderStatus) < 0) { continue; }
                    #endregion

                    #region 执行频率为 [必要时]
                    if (blnOnNeedTime == true)
                    {
                        schedule = dtTreate.ToString(ComConst.FMT_DATE.SHORT) + ComConst.STR.BLANK + "23:59:00";

                        try
                        {
                            drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                            fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, schedule);
                            dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                        }
                        catch (Exception ex)
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, ex.Message);
                        }
                        continue;
                    }
                    #endregion

                    #region 条件检查

                    // 如果  频率为空 ，则这条医嘱放弃 ，不拆分
                    if (frequency.Length == 0)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "频次为空 !");
                        continue;
                    }
                    // 没有时间间隔, 不进行拆分
                    if (freqIntervalUnit.Length == 0)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "无时间间隔 !");
                        continue;
                    }

                    // 时间间隔必须为[日],[小时],[周]
                    if (freqIntervalUnit.IndexOf("日") < 0
                        && freqIntervalUnit.IndexOf("小时") < 0
                        && freqIntervalUnit.IndexOf("周") < 0)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[医嘱频率] 的间隔单位必须是 [日]或[小时] !");
                        continue;
                    }

                    if (DataType.IsNumber(drOrder["FREQ_INTERVAL"].ToString()) == false)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[频率间隔] 必须为正整数!");
                        continue;
                    }

                    if (DataType.IsNumber(drOrder["FREQ_COUNTER"].ToString()) == false)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[频率次数] 必须为正整数!");

                        continue;
                    }
                    #endregion

                    #region 获取执行计划

                    int freqInterval = int.Parse(drOrder["FREQ_INTERVAL"].ToString());  // 执行频率的间隔部分
                    int freqCounter = int.Parse(drOrder["FREQ_COUNTER"].ToString());   // 执行频率的次数部分
                    string performSchedule = drOrder["PERFORM_SCHEDULE"].ToString();           // 护士执行时间

                    ArrayList arrSchedule = getOrderPerformSchedule(performSchedule);

                    // 计划执行时间不能为空

                    if (freqCounter >= 1 && performSchedule.Length == 0)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "计划执行时间不能为空！");
                        continue;
                    }


                    // 获取计划执行时间, 如果时间间隔不为[小时]
                    if (freqIntervalUnit.Equals("日"))
                    {
                        if (GetPerformSchedule(ref arrSchedule, freqCounter) == false)
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[频率次数] 与 [计划执行时间] 不符!");
                            continue;
                        }
                    }

                    if (freqIntervalUnit.Equals("小时"))
                    {
                        if (ComConst.VAL.HOURS_PER_DAY / freqInterval != arrSchedule.Count)
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[频率次数] 与 [计划执行时间] 不符!");
                            continue;
                        }
                    }
                    #endregion

                    #region 处理时间间隔为 [1/X小时] 的医嘱
                    if (freqIntervalUnit.IndexOf("小时") >= 0)
                    {
                        int hours = 0;
                        for (int i = 0; i < arrSchedule.Count; i++)
                        {
                            if (int.TryParse(arrSchedule[i].ToString(), out hours) == false)
                            {
                                continue;
                            }

                            dtSchedule = dtTreate.Date.AddHours(hours);
                            if (dtOrderStart < dtSchedule.AddMinutes(1) && (orderStopDate.Length == 0 || dtSchedule <= dtOrderStop))
                            {
                                try
                                {
                                    drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                                    fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, dtSchedule.ToString(ComConst.FMT_DATE.LONG));
                                    dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                                }
                                catch (Exception ex)
                                {
                                    addErrOrder(days, patientId, visitId, orderNo, orderSubNo, ex.Message);
                                    continue;
                                }
                            }
                        }

                        continue;
                    }
                    #endregion

                    #region 处理一天一次
                    if (freqIntervalUnit.IndexOf("日") >= 0 && freqInterval > 0 && freqInterval == freqCounter)
                    {
                        time = (string)arrSchedule[0];

                        if (DataType.IsTime(ref time) == false)
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[计划执行时间] 有误!");
                            continue;
                        }

                        dtSchedule = DateTime.Parse(dtTreate.ToString(ComConst.FMT_DATE.SHORT) + ComConst.STR.BLANK + time);

                        if (dtOrderStart < dtSchedule.AddMinutes(1) && (orderStopDate.Length == 0 || dtSchedule <= dtOrderStop))
                        {
                            try
                            {
                                drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                                fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, dtSchedule.ToString(ComConst.FMT_DATE.LONG));
                                dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                            }
                            catch (Exception ex)
                            {
                                addErrOrder(days, patientId, visitId, orderNo, orderSubNo, ex.Message);
                            }
                        }

                        continue;
                    }
                    #endregion

                    #region 处理 一天N次
                    if (freqIntervalUnit.IndexOf("日") >= 0 && freqInterval == 1)
                    {
                        if (freqCounter != arrSchedule.Count)
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[频率次数] 与 [计划执行时间] 不符!");

                            continue;
                        }

                        for (int i = 0; i < arrSchedule.Count; i++)
                        {
                            time = (string)arrSchedule[i];
                            if (DataType.IsTime(ref time) == false)
                            {
                                addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[计划执行时间] 有误!");
                                break;
                            }

                            dtSchedule = DateTime.Parse(dtTreate.ToString(ComConst.FMT_DATE.SHORT) + ComConst.STR.BLANK + time);

                            if (dtOrderStart < dtSchedule.AddMinutes(1) && (orderStopDate.Length == 0 || dtSchedule <= dtOrderStop))
                            {
                                try
                                {
                                    drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                                    fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, dtSchedule.ToString(ComConst.FMT_DATE.LONG));
                                    dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                                }
                                catch (Exception ex)
                                {
                                    addErrOrder(days, patientId, visitId, orderNo, orderSubNo, ex.Message);
                                }
                            }
                        }

                        continue;
                    }
                    #endregion

                    #region  处理 N天一次 (隔日)
                    if (frequency.IndexOf("Qod") >= 0 || frequency.IndexOf("隔日") >= 0)
                    {
                        if (freqCounter != 1 || arrSchedule.Count > 1)
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "隔日医嘱 [频率次数] 与 [计划执行时间] 不符!");

                            continue;
                        }

                        if (!performSchedule.Contains(":"))
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "不是有效的时间格式");

                            continue;
                        }

                        // 如果是恰当的隔日
                        if (ChkRightTertian(dtTreate, dtOrderStart, performSchedule, drOrder["FREQ_DETAIL"].ToString(), filterOra) == true)
                        {
                            if (performSchedule.Length > 0)
                            {
                                schedule = dtTreate.ToString(ComConst.FMT_DATE.SHORT) + ComConst.STR.BLANK + performSchedule;
                            }
                            else
                            {
                                schedule = dtTreate.ToString(ComConst.FMT_DATE.SHORT) + ComConst.STR.BLANK + "09:00:00";
                            }

                            // 确定时间范围
                            dtSchedule = DateTime.Parse(schedule);
                            if (dtOrderStart < dtSchedule.AddMinutes(1) && (orderStopDate.Length == 0 || dtSchedule <= dtOrderStop))
                            {
                                try
                                {
                                    drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                                    fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, schedule);
                                    dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                                }
                                catch (Exception ex)
                                {
                                    addErrOrder(days, patientId, visitId, orderNo, orderSubNo, ex.Message);
                                }
                            }
                        }

                        continue;
                    }
                    #endregion

                    #region  处理 N天一次 (N/周)
                    if (freqIntervalUnit.IndexOf("周") >= 0)
                    {
                        if (arrSchedule.Count != freqCounter)
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "周医嘱 [频率次数] 与 [计划执行时间] 不符!");

                            continue;
                        }

                        int dayOfWeek = (int)dtTreate.DayOfWeek;
                        dayOfWeek = (dayOfWeek == 0 ? 7 : dayOfWeek);

                        for (int i = 0; i < arrSchedule.Count; i++)
                        {
                            try
                            {
                                if (int.Parse((string)arrSchedule[i]) == dayOfWeek)
                                {
                                    schedule = dtTreate.ToString(ComConst.FMT_DATE.SHORT) + ComConst.STR.BLANK + "09:00:00";
                                    dtSchedule = DateTime.Parse(schedule);

                                    if (dtOrderStart < dtSchedule.AddMinutes(1) && (orderStopDate.Length == 0 || dtSchedule <= dtOrderStop))
                                    {
                                        try
                                        {
                                            drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                                            fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, schedule);
                                            dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                                        }
                                        catch (Exception ex)
                                        {
                                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, ex.Message);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                addErrOrder(days, patientId, visitId, orderNo, orderSubNo, ex.Message);
                            }
                        }

                        continue;
                    }
                    #endregion

                    // 不能处理的情况, 报错
                    addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "该医嘱不满足拆分条件!");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region 删除作废的医嘱
        /// <summary>
        /// 删除作废的医嘱（orders_m表的记录删除了以后进入orders_m_tombstone,当orders_m_tombstone表中有记录时说明，
        /// 杭创的医嘱已经改变了，在接口对接的时候发现原来orders_m里面有的数据有的已经不是有效状态了，于是orders_m被删除，
        /// 触发器ORDERS_M_DELETE把数据转移进orders_m_tombstone）
        /// 这里是检查orders_m_tombstone（备份表）中的记录是否已经生成了ORDERS_EXECUTE，如果已经生成了，删除掉
        /// </summary>
        public void DeleteOrdersExecuteInvalid()
        {
            string sql = string.Empty;
            sql += "delete from ORDERS_EXECUTE E where (E.CONVERSION_DATE_TIME,E.Patient_Id,E.Visit_Id,E.Order_No,E.Order_Sub_No,E.ORDERS_PERFORM_SCHEDULE) in ";
            sql += "(SELECT E.CONVERSION_DATE_TIME,E.Patient_Id,E.Visit_Id,E.Order_No,E.Order_Sub_No,E.ORDERS_PERFORM_SCHEDULE ";
            sql += "FROM ORDERS_EXECUTE E,orders_m_tombstone O ";
            sql += "WHERE E.CONVERSION_DATE_TIME > TO_DATE(SYSDATE - 3) ";
            //LB20110703瓦房店，解决预约出院
            sql += "AND E.SCHEDULE_PERFORM_TIME>O.stop_date_time ";
            //LB20110703瓦房店修改结束
            //sql += "AND O.START_DATE_TIME > TO_DATE(SYSDATE - 3) ";
            sql += "AND (E.IS_EXECUTE IS NULL OR E.IS_EXECUTE = '0') ";
            sql += "AND E.PATIENT_ID = O.PATIENT_ID AND E.VISIT_ID = O.VISIT_ID ";
            sql += "AND E.ORDER_NO = O.ORDER_NO AND E.ORDER_SUB_NO = O.ORDER_SUB_NO)";
            oracleAccess.ExecuteNoQuery(sql);
            TraceInfo = "删除作废医嘱执行单操作成功!";
        }

        public void DeleteOrdersExeTombstone(int days)
        {
            string sql = string.Empty;
            sql += "delete from ORDERS_EXECUTE_TOMBSTONE E where E.CREATE_TIMESTAMP<TO_DATE(SYSDATE-" + days.ToString() + ")";
            oracleAccess.ExecuteNoQuery(sql);
            TraceInfo = "删除医嘱执行单备份表过期数据操作成功!";
        }

        public void clearSucess_Split_Memo()
        {
            int count = 0;
            string sql = string.Empty;
            sql = "select m.* from orders_m  m where m.split_memo is not null and order_sub_no  in (select distinct(order_sub_no) from orders_execute)";
            DataSet ds = oracleAccess.SelectData(sql, "orders_m");

            count = ds.Tables[0].Rows.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                ds.Tables[0].Rows[i]["split_memo"] = null;
            }
            oracleAccess.Update(ref ds);

            //提示信息
            TraceInfo = "清除已拆分,而继续显示拆分错误医嘱 " + count.ToString() + " 条!";

            if (count > 0)
            {
                RowCountChanged = count;
                TraceInfo = "变更数据: " + dtBegin.ToString(ComConst.FMT_DATE.LONG) + " 共: " + rowCount.ToString() + "条";
            }
        }

        #endregion

        #region 生成一条医嘱执行单
        /// <summary>
        /// 生成一条医嘱执行单
        /// </summary>
        /// <param name="drExecute">医嘱执行单</param>
        /// <param name="drOrder">医嘱</param>
        /// <param name="nurseName">护士名字</param>
        public void fillOrderExecute(ref DataRow drExecute, DataRow drOrder, DateTime dtConvert, string schedule)
        {

            drExecute["PATIENT_ID"] = drOrder["PATIENT_ID"];
            drExecute["VISIT_ID"] = drOrder["VISIT_ID"];
            drExecute["ORDER_NO"] = drOrder["ORDER_NO"];
            drExecute["ORDER_SUB_NO"] = drOrder["ORDER_SUB_NO"];
            drExecute["REPEAT_INDICATOR"] = drOrder["REPEAT_INDICATOR"];
            drExecute["ORDER_CLASS"] = drOrder["ORDER_CLASS"];
            drExecute["ORDER_TEXT"] = drOrder["ORDER_TEXT"];
            drExecute["ORDER_CODE"] = drOrder["ORDER_CODE"];
            drExecute["DOSAGE"] = drOrder["DOSAGE"];
            drExecute["DOSAGE_UNITS"] = drOrder["DOSAGE_UNITS"];
            drExecute["ADMINISTRATION"] = drOrder["ADMINISTRATION"];
            drExecute["DURATION"] = drOrder["DURATION"];
            drExecute["DURATION_UNITS"] = drOrder["DURATION_UNITS"];
            drExecute["FREQUENCY"] = drOrder["FREQUENCY"];
            drExecute["FREQ_COUNTER"] = drOrder["FREQ_COUNTER"];
            drExecute["FREQ_INTERVAL"] = drOrder["FREQ_INTERVAL"];
            drExecute["FREQ_INTERVAL_UNIT"] = drOrder["FREQ_INTERVAL_UNIT"];
            drExecute["FREQ_DETAIL"] = drOrder["FREQ_DETAIL"];
            drExecute["PERFORM_SCHEDULE"] = drOrder["PERFORM_SCHEDULE"];
            drExecute["PERFORM_RESULT"] = drOrder["PERFORM_RESULT"];
            drExecute["CONVERSION_NURSE"] = nurseName;
            drExecute["CONVERSION_DATE_TIME"] = dtConvert;
            DateTime dtSchedule = DateTime.Parse(schedule);
            drExecute["ORDERS_PERFORM_SCHEDULE"] = dtSchedule.Hour.ToString();
            drExecute["SCHEDULE_PERFORM_TIME"] = dtSchedule;
        }
        #endregion

        #region 获取医嘱执行计划
        /// <summary>
        /// 获取医嘱执行计划
        /// </summary>
        /// <param name="performSchedule">医嘱执行计划字符串</param>
        /// <returns>拆分后的ArrayList</returns>
        public ArrayList getOrderPerformSchedule(string performSchedule)
        {
            ArrayList arrSchedule = new ArrayList();

            // 预处理: 清除空格
            performSchedule = performSchedule.Replace(ComConst.STR.BLANK, string.Empty);

            // 预处理: "/" "\" -> "-"
            performSchedule = performSchedule.Replace("/", "-");
            performSchedule = performSchedule.Replace(@"\", "-");
            performSchedule = performSchedule.Replace(@";", "-");

            // 如果为空字符串, 直接退出
            if (performSchedule.Trim().Length == 0)
            {
                return arrSchedule;
            }

            // 设置分隔字符串
            string sepStr = "-";
            string[] arrPart = performSchedule.Split(sepStr.ToCharArray());

            for (int i = 0; i < arrPart.Length; i++)
            {
                arrPart[i] = (arrPart[i].Trim().Length == 1 ? "0" : string.Empty) + arrPart[i];

                arrSchedule.Add(arrPart[i]);
            }

            return arrSchedule;
        }
        #endregion

        #region 检查今天是不是正好的隔日
        /// <summary>
        /// 检查今天是不是正好的隔日
        /// </summary>
        /// <returns></returns>
        public bool ChkRightTertian(DateTime dtTreate, DateTime dtOrderStart, string performSchedule, string freqDetail, string orderFilter)
        {
            // 确定以前是否生成过执行单
            string convertedDate = GetOrderExecuteConvertedDate(orderFilter);

            // 如果以前生成过执行单
            if (convertedDate.Length > 0)
            {
                DateTime dtConverted = DateTime.Parse(convertedDate);
                dtConverted = DateTime.Parse(dtConverted.ToString(ComConst.FMT_DATE.SHORT));

                TimeSpan tSpan = dtTreate.Subtract(dtConverted);
                return (tSpan.Days % 2 == 0);
            }

            // 如果以前没有生成过执行单
            DateTime dtSchedule = DateTime.Parse(dtTreate.ToString(ComConst.FMT_DATE.SHORT) + ComConst.STR.BLANK + performSchedule);

            if (freqDetail.Length > 0)
            {
                int pos = freqDetail.LastIndexOf("日");

                if (pos > 0)
                {
                    if ((("单".Equals(freqDetail.Substring(pos - 1, 1))) && (dtTreate.Day % 2 == 0))
                     || (("双".Equals(freqDetail.Substring(pos - 1, 1))) && (dtTreate.Day % 2 == 1)))
                    {
                        return false;
                    }

                    if ((("单".Equals(freqDetail.Substring(pos - 1, 1))) && (dtTreate.Day % 2 == 1))
                        || ("双".Equals(freqDetail.Substring(pos - 1, 1))) && (dtTreate.Day % 2 == 0))
                    {
                        // 如果开始日期小于今天
                        if (dtOrderStart.ToString(ComConst.FMT_DATE.SHORT).CompareTo(dtTreate.ToString(ComConst.FMT_DATE.SHORT)) < 0)
                        {
                            return true;
                        }
                    }
                }
            }

            // 如果不确定单双日
            return (dtSchedule.CompareTo(dtOrderStart) > 0);
        }
        #endregion

        #region 获取某一医嘱执行单的任一转换日期
        /// <summary>
        /// 获取某一医嘱执行单的任一转换日期
        /// </summary>
        /// <returns></returns>
        public string GetOrderExecuteConvertedDate(string filter)
        {
            string sql = "SELECT CONVERSION_DATE_TIME FROM ORDERS_EXECUTE WHERE " + filter + " AND ROWNUM = 1";

            if (oracleAccess.SelectValue(sql) == true)
            {
                return oracleAccess.GetResult(0);
            }
            else
            {
                DataRow[] drFind = dsOrdersExecute.Tables[0].Select(filter);

                if (drFind.Length > 0)
                {
                    return drFind[0]["CONVERSION_DATE_TIME"].ToString();
                }
            }

            return string.Empty;
        }
        #endregion

        #region 生成计划执行时间
        /// <summary>
        /// 生成计划执行时间
        /// </summary>
        /// <param name="arrSchedule">指定的计划时间</param>
        /// <param name="freqCounter">执行次数/天</param>
        /// <returns></returns>
        public bool GetPerformSchedule(ref ArrayList arrSchedule, int freqCounter)
        {
            // 预处理
            if (arrSchedule == null)
            {
                arrSchedule = new ArrayList();
            }

            // 如果没有指定计划执行时间, 默认从9点开始执行
            if (arrSchedule.Count == 0)
            {
                arrSchedule.Add("9:00");
            }

            // 如果计划时间全部指定, 直接退出;
            if (arrSchedule.Count == freqCounter)
            {
                return true;
            }

            // 如果计划时间仅指定开始时间
            if (arrSchedule.Count == 1 && freqCounter > 1)
            {
                // 获取时间间隔
                int interval = ComConst.VAL.HOURS_PER_DAY / freqCounter;

                // 获取开始指定的时间
                string tmStart = (string)arrSchedule[0];
                if (DataType.IsTime(ref tmStart) == false)
                {
                    return false;
                }

                DateTime dtStart = DateTime.Parse(DateTime.Now.ToString(ComConst.FMT_DATE.SHORT) + ComConst.STR.BLANK + tmStart);

                // 生成其余的计划执行时间
                for (int i = 1; i < freqCounter; i++)
                {
                    dtStart = dtStart.AddHours(interval);

                    arrSchedule.Add(dtStart.ToString(ComConst.FMT_DATE.TIME_SHORT));
                }

                return true;
            }

            // 其它情况
            return false;
        }
        #endregion

        #region 保存不规范的医嘱
        /// <summary>
        /// 保存不规范的医嘱
        /// </summary>
        /// <param name="days">只有days == 1才执行</param>
        private void addErrOrder(int days, string patientId, string visitId, string orderNo, string orderSubNo, string errDesc)
        {
            if (days != 1)
            {
                return;
            }
            DataRow[] drFind = dsErr.Tables[0].Select("patient_id=" + SQL.SqlConvert(patientId) + " and visit_id=" + SQL.SqlConvert(visitId) + " and order_No=" + SQL.SqlConvert(orderNo) + " and order_sub_no=" + SQL.SqlConvert(orderSubNo), string.Empty);
            if (drFind.Length > 0)
            {
                return;
            }


            DataRow drNew = dsErr.Tables[0].NewRow();

            drNew["PATIENT_ID"] = patientId;
            drNew["VISIT_ID"] = visitId;
            drNew["ORDER_NO"] = orderNo;
            drNew["ORDER_SUB_NO"] = orderSubNo;
            drNew["ERR_DESC"] = errDesc;

            dsErr.Tables[0].Rows.Add(drNew);
        }
        #endregion

        #region 获取系统日期
        /// <summary>
        /// 获取系统日期
        /// </summary>
        /// <returns>系统当前日期</returns>
        public DateTime GetSysDate()
        {
            if (oracleAccess.SelectValue("SELECT SYSDATE FROM DUAL") == true)
            {
                return DateTime.Parse(oracleAccess.GetResult(0));
            }
            else
            {
                throw new Exception("获取系统日期出错!");
            }
        }


        public void Exit()
        {
            blnExit = true;
        }
        #endregion

        #region 获取表的字段列表(时间戳类型除外
        /// <summary>
        /// 获取表的字段列表(时间戳类型除外)
        /// </summary>
        /// <returns></returns>
        private string getTableColsList(string tableName)
        {
            return tableName + ".*";

            tableName = tableName.ToUpper();
            string sql = "SELECT * FROM DBA_TAB_COLUMNS WHERE TABLE_NAME = " + SqlManager.SqlConvert(tableName)
                + "AND OWNER = 'ORDADM'";
            DataSet ds = null;

            switch (tableName)
            {
                case "ORDERS_EXECUTE":
                    if (dsOrdersExecuteStruct == null)
                    {
                        dsOrdersExecuteStruct = oracleAccess.SelectData_NoKey(sql, tableName);
                    }

                    ds = dsOrdersExecuteStruct;

                    break;

                default:
                    return string.Empty;
            }

            // 获取列表
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["DATA_TYPE"].ToString().IndexOf("TIMESTAMP") < 0)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(", ");
                    }

                    sb.Append(tableName + "." + dr["COLUMN_NAME"].ToString());
                }
            }

            if (sb.ToString().Length > 0)
            {
                return sb.ToString();
            }
            else
            {
                return tableName + ".*";
            }
        }
        #endregion

        #region 保存拆分结果
        /// <summary>
        /// 保存拆分结果
        /// </summary>
        /// <returns></returns>
        private bool saveSplitResult()
        {
            try
            {
                TraceInfo = "开始准备保存拆分的医嘱执行单...";

                DataSet ds = dsOrdersExecute.GetChanges();
                if (ds != null && ds.Tables.Count > 0)
                {
                    TraceInfo = "一共要 保存拆分的医嘱执行单" + ds.Tables[0].Rows.Count.ToString() + "条  完成!";

                    RowCountChanged = ds.Tables[0].Rows.Count;
                    TraceInfo = "变更数据: " + dtBegin.ToString(ComConst.FMT_DATE.LONG) + " 共: " + rowCount.ToString() + "条";

                    oracleAccess.Update(ref ds, "ORDERS_EXECUTE", "SELECT * FROM ORDERS_EXECUTE  where ORDER_CLASS = 'A' ");
                    dsOrdersExecute.AcceptChanges();
                }

                TraceInfo = "保存拆分的医嘱执行单   总变化   完成!";

                return true;
            }
            catch (Exception ex)
            {
                TraceInfo = "后台拆分线程出错" + ex.Message;    // 异常日志
                return false;
            }
        }
        #endregion

        #region 保存拆分失败原因
        /// <summary>
        /// 保存拆分失败原因
        /// </summary>
        private void saveSplitError()
        {
            if (dsErr == null || dsErr.Tables.Count == 0 || dsErr.Tables[0].Rows.Count == 0)
            {
                return;
            }

            // 查询原始数据
            string sql = "SELECT * FROM ORDERS_M ";
            if (wardCode.Length > 0)
            {
                sql += " WHERE WARD_CODE = " + SQL.SqlConvert(wardCode);
            }

            DataSet dsSrc = oracleAccess.SelectData(sql, "ORDERS_M");
            dsSrc.AcceptChanges();


            // 更新数据
            string filter = "PATIENT_ID = '{0}' AND VISIT_ID = '{1}' AND ORDER_NO = '{2}' AND ORDER_SUB_NO = '{3}'  ";
            DataRow[] drFind = null;
            DataRow drEdit = null;
            foreach (DataRow dr in dsErr.Tables[0].Rows)
            {
                drFind = dsSrc.Tables[0].Select(string.Format(filter, new string[] { dr["PATIENT_ID"].ToString(), dr["VISIT_ID"].ToString(), dr["ORDER_NO"].ToString(), dr["ORDER_SUB_NO"].ToString() }));
                if (drFind.Length == 0)
                {
                    drEdit = dsSrc.Tables[0].NewRow();

                    drEdit["PATIENT_ID"] = dr["PATIENT_ID"];
                    drEdit["VISIT_ID"] = dr["VISIT_ID"];
                    drEdit["ORDER_NO"] = dr["ORDER_NO"];
                    drEdit["ORDER_SUB_NO"] = dr["ORDER_SUB_NO"];
                }
                else
                {
                    drEdit = drFind[0];
                }

                if (drEdit["SPLIT_MEMO"].ToString().Equals(dr["ERR_DESC"].ToString()) == false)
                {
                    drEdit["SPLIT_MEMO"] = dr["ERR_DESC"].ToString();
                }

                if (drFind.Length == 0)
                {
                    dsSrc.Tables[0].Rows.Add(drEdit);
                }
            }

            // 设置行状态
            foreach (DataRow dr in dsSrc.Tables[0].Rows)
            {
                if (dr["SPLIT_MEMO"].ToString().Trim().Length == 0) dr.AcceptChanges();
            }
            //orders_m里面成功拆分的记录会被删除掉，下次和源数据进行比较的时候发现没有就又过来了，导致orders_m_tombstore里面数据非常大
            //应该改成停止了的医嘱就删除掉，如果一直不删除，导致里面的信息非常大--(刘波注释)
            //// 删除所有未变更的记录(原来的注释)
            //drFind = dsSrc.Tables[0].Select(string.Empty, string.Empty, DataViewRowState.Unchanged);
            //for(int i = drFind.Length - 1; i >= 0; i--)
            //{
            //    drFind[i].Delete();
            //}

            oracleAccess.Update(ref dsSrc);

            RowCountChanged = dsSrc.Tables[0].Select("", "", DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedCurrent).Length;

            TraceInfo = "变更数据: " + dtBegin.ToString(ComConst.FMT_DATE.LONG) + " 共: " + rowCount.ToString() + "条";
        }

        #endregion
        #endregion

        #region Oracle字符集处理
        private string oldNlslang = string.Empty;


        /// <summary>
        /// 设置字符集
        /// </summary>
        private void setHisNlsLang()
        {
            // 不同字符集的处理
            string nlsLangKey = GVars.IniFile.ReadString("DATABASE", "ORA_NLS_LANG", string.Empty);

            if (nlsLangKey.Length > 0)
            {

                for (int i = 0; i < 6; i++)                 // 针对西京医院 要多次连执接才会成功的情况设计
                {
                    try
                    {
                        oldNlslang = oracleNlsLang_Zh(nlsLangKey);
                        oracleAccess.SelectValue("SELECT SYSDATE FROM DUAL");
                        return;
                    }
                    catch (Exception ex)
                    {
                        TraceInfo = ex.Message;

                        Thread.Sleep(30 * 1000);        // 休息30秒
                    }
                    finally
                    {
                        oracleNlsLang_Restore(nlsLangKey, oldNlslang);
                    }
                }

                throw new Exception("设置字符集失败!");
            }
        }


        /// <summary>
        /// 修改Oracle字符集为英文
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string oracleNlsLang_En(string key)
        {
            string preValue = Registry.GetValue(key, "NLS_LANG", null).ToString();

            if (preValue.Length > 0)
            {
                Registry.SetValue(key, "NLS_LANG", "AMERICAN_AMERICA.US7ASCII");
            }

            return preValue;
        }


        /// <summary>
        /// 修改Oracle字符集为中文
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string oracleNlsLang_Zh(string key)
        {
            string preValue = Registry.GetValue(key, "NLS_LANG", null).ToString();

            if (preValue.Length > 0)
            {
                Registry.SetValue(key, "NLS_LANG", "SIMPLIFIED CHINESE_CHINA.ZHS16GBK");
            }

            return preValue;
        }


        /// <summary>
        /// 还原Oracle字符集
        /// </summary>
        /// <param name="key"></param>
        /// <param name="oldValue"></param>
        /// <returns></returns>
        private bool oracleNlsLang_Restore(string key, string oldValue)
        {
            if (oldValue.Length == 0)
            {
                return true;
            }

            Registry.SetValue(key, "NLS_LANG", oldValue);

            return true;
        }
        #endregion
    }
}
