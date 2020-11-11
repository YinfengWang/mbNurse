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
using System.Diagnostics;
using System.Configuration;
using OrderSplitHelper;
using System.Linq;
namespace HISPlus
{

    /// <summary>
    /// ConvertFunction 的摘要说明。
    /// </summary>
    public class OrderSplitter
    {
        #region 变量
        // private int             instanceCount   = 0;                            // 实例数
        private DateTime dtNow;
        private DataSet dsOrders = null;
        private DataSet dsOrdersExecute = null;
        private DataSet dsOrdersExecuteStruct = null;
        private DataSet dsErr = new DataSet();
        private string wardCode = string.Empty;
        private bool blnExit = false;
        private Mutex locker = new Mutex();                          // 互斥锁        
        private ArrayList arrSql_Del = new ArrayList();

        private List<DeleteInfo> deleteInfos = new List<DeleteInfo>();
        private Queue stTraceInfo = new Queue(210);
        private Queue stLogFile = new Queue(210);
        private DbAccess oracleAccess = null;
        private long rowCount = 0;
        private DateTime dtBegin = DateTime.MinValue;

        //public static List<SyncInfo> syncInfos = new List<SyncInfo>();
        public List<SyncInfo> syncInfos = new List<SyncInfo>();
        private OrderSplit orderSplit;

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public string IniPath;
        #endregion

        #region
        private class DeleteInfo
        {
            public string Msg;
            public string Sql;
        }

        #endregion

        #region 属性
        public string TraceInfo
        {
            get
            {
                try
                {
                    //申请一个互斥锁
                    locker.WaitOne();

                    StringBuilder sb = new StringBuilder();
                    object[] objects = stTraceInfo.ToArray();
                    string line = string.Empty; 
                    string outDev = string.Empty;
                    for (int i = objects.Length - 1; i >= 0; i--)
                    {
                        line = objects[i].ToString();
                        //2015.11.23 由于窗体的编辑控件.text = TraceInfo，因此，在此处即是输出到窗体
                        outDev = line.Substring(0,SysConst.MsgOptDev.SCREEN.Length);
                        if (outDev == SysConst.MsgOptDev.SCREEN || outDev == SysConst.MsgOptDev.ALL)
                        {
                            sb.Append(objects[i].ToString());
                            sb.Append(ComConst.STR.CRLF);
                        }
                    }

                    return sb.ToString();
                }
                finally
                {
                    //释放
                    locker.ReleaseMutex();
                }
            }
            set
            {
                try
                {
                    locker.WaitOne();
                    //stTraceInfo.Push(DateTime.Now.ToLongTimeString() + ": " +value);

                    //2015.11.24 将信息加入队列，此处应优化，如设备输出标志长度
                    stTraceInfo.Enqueue ( value.Substring(0,SysConst.MsgOptDev.SCREEN.Length) + " " +
                        DateTime.Now.ToLongTimeString() + ": " +
                        value.Substring(SysConst.MsgOptDev.SCREEN.Length));
                    //stTraceInfo.Enqueue(DateTime.Now.ToLongTimeString() + ": " + value);
                    while (stTraceInfo.Count > 50)
                    {
                        //stTraceInfo.Pop();
                        //stTraceInfo.Dequeue();
                        #region 屏幕输出写日志 2015.11.23
                        //if (!string.IsNullOrEmpty(syncInfos[0].FnPreLog))
                        //{
                        //    string line = string.Empty;
                        //    string outDev = string.Empty;
                        //    StringBuilder sb = new StringBuilder();
                        //    object[] objects = stTraceInfo.ToArray();
                        //    for (int i = 0; i < objects.Length ; i++)
                        //    {
                        //        line = objects[i].ToString();
                        //        outDev = line.Substring(0, SysConst.MsgOptDev.SCREEN.Length);
                        //        if (outDev == SysConst.MsgOptDev.LOG || outDev == SysConst.MsgOptDev.ALL)
                        //        {
                        //            sb.Append(objects[i].ToString());
                        //            sb.Append(ComConst.STR.CRLF);
                        //        }
                        //    }
                        //    CommFunc.LogClass.WriteLog(sb.ToString(), SysConst.LogFnType.YMDH,syncInfos[0].FnPreLog);
                        //}
                        #endregion
                        stTraceInfo.Clear();
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
            Init();
        }

        private void Init()
        {// 创建表
            
            dsErr.Tables.Add(OrderSplit.CreateErrorDt());

            // 变量赋值
            wardCode = GVars.User.DeptCode;

            orderSplit = new OrderSplit();
            this.oracleAccess = orderSplit.OracleAccess;

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

            // 判断文件是否存在
            string fileName;
            if (string.IsNullOrEmpty(IniPath))
                fileName = Path.Combine(Path.Combine(Application.StartupPath, "SQL"), "List" + strNum + ".INI");
            else
                fileName = Path.Combine(Application.StartupPath, IniPath);

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
                //2015.11.23 日志文件名前缀，如果为空，则不写日志
                if (line.StartsWith("fnprelog:") == true)
                {
                    syncItem.FnPreLog = line.Substring(9).Trim();
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

            while (true)
            {
                try
                {
                    // 处理字符集
                    setHisNlsLang();

                    #region 预处理
                    //记录当前拆分的是"药疗"还是"非药疗",默认是药疗。
                    bool isPharmacologic = true;
                    DateTime dtBegin = DateTime.Now;
                    long counter = 0;

                    //加载同步信息
                    loadSyncInfo();

                    // 获取当前时间
                    dtNow = orderSplit.GetSysDate();
                    TraceInfo = SysConst.MsgOptDev.ALL + "============" + syncInfos[0].FnPreLog + "============" + SysConst.STR.CRLF
                        + "获取系统日期: " + dtNow.ToString(ComConst.FMT_DATE.LONG);
                    dsErr.Tables[0].Rows.Clear();
                    orderSplit.SyncInfos = syncInfos;
                    orderSplit.ErrorDs = dsErr;
                    
                    int splitCount = Convert.ToInt32(ConfigurationManager.AppSettings["SPLIT_DATES"] ?? "2");
                    SyncInfo syncInfo = syncInfos[0];
                    //根据配置文件，设置isPharmacologic,  True:药疗   False:非药疗  2015-11-11 add
                    isPharmacologic = syncInfo.Comment.Contains("有效医嘱");



                    #endregion
                    // 查找所有医嘱
                    if (blnExit == true)
                    {
                        return;
                    }

                    
                    //查找需要拆分的医嘱
                    dsOrders = orderSplit.GetOrdersToday(dtNow.AddDays(splitCount - 1).ToString(ComConst.FMT_DATE.SHORT),isPharmacologic,syncInfo.SrcSqlFile,syncInfo.Filter);//2015.11.15 增加日期入参

                    
                    TraceInfo = SysConst.MsgOptDev.ALL + "获取病区所有" + GetYzType(syncInfo.Comment) + "共:" + dsOrders.Tables[0].Rows.Count.ToString() + "条";



                    // 查找今天以后(含今天)医嘱执行单0
                    if (blnExit == true)
                    {
                        return;
                    }

                    //获取已经拆分了的医嘱
                    dsOrdersExecute = orderSplit.GetOrdersExecuteSplitted(dtNow.ToString(ComConst.FMT_DATE.SHORT),isPharmacologic,syncInfo.SrcSqlFile,syncInfo.Filter);
                    TraceInfo = SysConst.MsgOptDev.ALL + "获取病区已拆分" + GetYzType(syncInfo.Comment) + "执行单 共:" + dsOrdersExecute.Tables[0].Rows.Count.ToString() + "条";

                    
                    orderSplit.IsPharmacologic = isPharmacologic;
                    orderSplit.DsOrders = dsOrders;
                    orderSplit.DsOrdersExecute = dsOrdersExecute;
                    orderSplit.OperationTime = dtNow;
                    

                    #region 2015.11.24日志临时
                    string logLine = string.Empty;
                    logLine = "=====================================================" + SysConst.STR.CRLF;
                    logLine += "开始拆分——病区：" + syncInfos[0].Filter + ";需要拆分:" + dsOrders.Tables[0].Rows.Count.ToString() + ";已拆分:" + dsOrdersExecute.Tables[0].Rows.Count + SysConst.STR.CRLF;
                    if (!string.IsNullOrEmpty(syncInfos[0].FnPreLog))
                        LogClass.WriteLog(logLine, SysConst.LogFnType.YMDH, syncInfos[0].FnPreLog);
                    if (dsOrders.Tables[0].Rows.Count == 1)//测试
                    {
                        //需要拆分的医嘱
                        DataRow[] drOrders = dsOrders.Tables[0].Select("", "PATIENT_ID, VISIT_ID, ORDER_NO, ORDER_SUB_NO");
                        DataRow dr = drOrders[0];
                        LogClass.WriteLog(dr[0] + "|" + dr[1] + "|" + dr[2] + "|" + dr[3], SysConst.LogFnType.YMDH, syncInfos[0].FnPreLog);
                    }
                    #endregion
                    //根据配置文件的天数进行循环
                    for (int i = 1; i <= splitCount; i++)
                    {
                        // 进行拆分
                        TraceInfo = SysConst.MsgOptDev.ALL + "开始进行" + GetYzType(syncInfo.Comment) + "拆分程序 第 " + i.ToString() + "天数据拆分...";
                        dtBegin = DateTime.Now;

                        #region 2015.11.24日志临时
                        string logLinetmp = "----------------" + SysConst.STR.CRLF;
                        logLinetmp += "拆分第" + i.ToString() + "天";
                        if (!string.IsNullOrEmpty(syncInfos[0].FnPreLog))
                            LogClass.WriteLog(logLinetmp, SysConst.LogFnType.YMDH, syncInfos[0].FnPreLog);
                        #endregion

                        if (blnExit == true)
                        {
                            return;
                        }
                        
                        //jyq 2015.12.18 合并药疗非药疗拆分医嘱方法
                        orderSplit.SplitMedicalOrders(i);

                        // 进行保存
                        if (blnExit == true)
                        {
                            return;
                        }

                        //保存拆分结果，拆分结果保存成功后才向order_m中更新数据。
                        bool isSaved = saveSplitResult();
                        if (isSaved)
                        {
                            saveOrdersResult(isPharmacologic);
                        }

                        // 保存拆分失败的原因
                        //if (i == 1)
                        //{
                            //saveSplitError();
                        //}

                        // 确保一次完整的拆分循环的时间在 1分钟(60秒)以上
                        while (DateTime.Now.Subtract(dtBegin).TotalSeconds <= 20)
                        {
                            if (blnExit == true) { return; }
                            Thread.Sleep(100);
                        }
                    }

                    // 删除作废医嘱(包括orders_m已经拆分为orders_execute的记录，本次进行上游数据和orders_m对接的时候,发现有不是新开的和执行的医嘱单，
                    // orders_m将被删除，数据进入备份表orders_m_tombstone表，)
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
                            TraceInfo = SysConst.MsgOptDev.SCREEN + DateTime.Now.ToString(ComConst.FMT_DATE.LONG) + delinfo.Msg;
                            Thread.Sleep(1000);
                        }
                    }

                    TraceInfo = SysConst.MsgOptDev.SCREEN + " HS    -- 一个循环结束 进行 下一个循环 ！！!";

                    // 体息10秒
                    for (int j = 0; j < 10 * 10; j++)
                    {
                        if (blnExit == true) { return; }
                        Thread.Sleep(100);
                    }
                }
                catch (Exception ex)
                {
                    TraceInfo = SysConst.MsgOptDev.SCREEN + "后台拆分线程出错" + ex.Message;        // 异常日志  

                    if (!string.IsNullOrEmpty(syncInfos[0].FnPreLog))
                        LogClass.WriteLog(GVars.User.UserName + ex.Message + ex.StackTrace, SysConst.LogFnType.YMDH, syncInfos[0].FnPreLog);
                    //LogFile.WriteLog(GVars.User.UserName + ex.Message + ex.StackTrace);//2015.11.23 del
                }
                finally
                {

                    #region 2015.11.24日志临时
                    string logLine = SysConst.STR.CRLF + "【一轮循环拆分结束】" + SysConst.STR.CRLF;
                    if (!string.IsNullOrEmpty(syncInfos[0].FnPreLog))
                        LogClass.WriteLog(logLine, SysConst.LogFnType.YMDH, syncInfos[0].FnPreLog);
                    #endregion
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
            TraceInfo = SysConst.MsgOptDev.SCREEN + "删除作废医嘱执行单操作成功!";
        }

        public void DeleteOrdersExeTombstone(int days)
        {
            string sql = string.Empty;
            sql += "delete from ORDERS_EXECUTE_TOMBSTONE E where E.CREATE_TIMESTAMP<TO_DATE(SYSDATE-" + days.ToString() + ")";
            oracleAccess.ExecuteNoQuery(sql);
            TraceInfo = SysConst.MsgOptDev.SCREEN + "删除医嘱执行单备份表过期数据操作成功!";
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
            TraceInfo = SysConst.MsgOptDev.SCREEN + "清除已拆分,而继续显示拆分错误医嘱 " + count.ToString() + " 条!";

            if (count > 0)
            {
                RowCountChanged = count;
                TraceInfo = SysConst.MsgOptDev.SCREEN + "变更数据: " + dtBegin.ToString(ComConst.FMT_DATE.LONG) + " 共: " + rowCount.ToString() + "条";
            }
        }

        #endregion

        public void Exit()
        {
            blnExit = true;
        }

        #region 获取表的字段列表(时间戳类型除外
        /// <summary>
        /// 获取表的字段列表(时间戳类型除外)
        /// </summary>
        /// <returns></returns>
        private string getTableColsList(string tableName)
        {
            return tableName + ".*";

            // tableName = tableName.ToUpper();
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
            bool result = true;
            try
            {                
                DataSet ds = dsOrdersExecute.GetChanges();
                if (ds != null && ds.Tables.Count > 0)
                {
                    TraceInfo = SysConst.MsgOptDev.SCREEN + "开始准备保存拆分的医嘱执行单...";
                    string str = string.Join(",", ds.Tables[0].Select("", "", DataViewRowState.Added).Select(r => string.Format("('{0}',{1},{2},{3},to_date('{4}','yyyy-mm-dd hh24:mi:ss'))", r["patient_id"], r["visit_id"], r["order_no"], r["order_sub_no"], r["schedule_perform_time"])).ToArray());
                    string str1 = string.Join(",", ds.Tables[0].Select("", "", DataViewRowState.Added).Select(r => string.Format("('{0}',{1},{2},{3})", r["patient_id"], r["visit_id"], r["order_no"], r["order_sub_no"])).ToArray());
                    TraceInfo = SysConst.MsgOptDev.SCREEN + "一共要 保存拆分的医嘱执行单" + ds.Tables[0].Rows.Count.ToString() + "条  完成!";

                    RowCountChanged = ds.Tables[0].Rows.Count;
                    TraceInfo = SysConst.MsgOptDev.SCREEN + "变更数据: " + dtBegin.ToString(ComConst.FMT_DATE.LONG) + " 共: " + rowCount.ToString() + "条";

                    TraceInfo = SysConst.MsgOptDev.SCREEN + "保存拆分的医嘱执行单   总变化   完成!";
                    result = orderSplit.saveSplitResult();
                }
            }
            catch (Exception ex)
            {
                TraceInfo = SysConst.MsgOptDev.SCREEN + "后台拆分线程出错" + ex.Message;    // 异常日志
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 保存Orders_m变化
        /// </summary>
        /// <returns></returns>
        private bool saveOrdersResult(bool isPharmacologic)
        {
            bool result = true;
            SyncInfo syncInfo = syncInfos[0];
            try
            {
                DataSet changes = this.dsOrders.GetChanges();
                if (changes != null && changes.Tables.Count > 0)
                {
                    this.TraceInfo = SysConst.MsgOptDev.SCREEN + "保存拆分的医嘱...";
                    this.TraceInfo = SysConst.MsgOptDev.SCREEN + "保存拆分的医嘱" + changes.Tables[0].Rows.Count.ToString() + " 完成!";
                    this.RowCountChanged = changes.Tables[0].Rows.Count;
                    this.TraceInfo = SysConst.MsgOptDev.SCREEN + "保存拆分的医嘱 完成!";
                    result = orderSplit.saveOrdersResult();
                }
            }
            catch (Exception ex)
            {
                this.TraceInfo = SysConst.MsgOptDev.SCREEN + "后台拆分线程出错" + ex.Message;
                result = false;
            }
            return result;
        }

        #endregion

        #region 保存拆分失败原因
        /// <summary>
        /// 保存拆分失败原因,此方法已经过期，代码中注释了。
        /// </summary>
        [Obsolete]
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

            TraceInfo = SysConst.MsgOptDev.SCREEN + "变更数据: " + dtBegin.ToString(ComConst.FMT_DATE.LONG) + " 共: " + rowCount.ToString() + "条";
        }

        #endregion

        #region 判断是药疗医嘱还是非药疗医嘱

        /// <summary>
        /// 判断是药疗医嘱还是非药疗医嘱
        /// 2015-11-11
        /// add
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        private string GetYzType(string comment)
        {
            string typeValue = string.Empty;
            typeValue = comment.Contains("非药疗") ? "非药疗医嘱" : "药疗医嘱";
            return typeValue;
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
                        TraceInfo = SysConst.MsgOptDev.SCREEN + ex.Message;

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
