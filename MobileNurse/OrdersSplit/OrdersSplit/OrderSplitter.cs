using System;
using System.Data;
using System.Collections;
using System.Threading;
using System.Text;

using DbType        = HISPlus.SqlManager.DB_TYPE;
using DbFieldType   = HISPlus.SqlManager.FIELD_TYPE;
using SQL           = HISPlus.SqlManager;

namespace HISPlus
{
	/// <summary>
	/// ConvertFunction 的摘要说明。
	/// </summary>
	public class OrderSplitter
	{
        private DateTime        dtNow;
        private DataSet         dsOrders        = null;
        private DataSet         dsOrdersExecute = null;
        private DataSet         dsOrdersExecuteStruct = null;
        private string          nurseName       = "SYSTEM";
        
        private DataSet         dsErr           = new DataSet();
        private string          wardCode        = string.Empty;
        private bool            blnExit         = false;
        
        private Mutex           locker          = new Mutex();                          // 互斥锁        
        private DataSet         dsOrdersOut     = null;
        private DataSet         dsErrOut        = new DataSet();

        private ArrayList       arrSql_Del      = new ArrayList();
        
        // UPD BEGIN BY FUJUN 2009-08-10
        // private OracleAccess    oracleAccess    = null;
        private DbAccess        oracleAccess    = null;
        // UPD END BY FUJUN 2009-08-10
                
		public OrderSplitter()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			
			// 创建表
			DataTable dtNew = new DataTable();
			dsErr.Tables.Add(dtNew);
			
			// 创建该表的字段
			dtNew.Columns.Add("PATIENT_ID",     Type.GetType("System.String"));
			dtNew.Columns.Add("VISIT_ID",       Type.GetType("System.String"));
			dtNew.Columns.Add("ORDER_NO",       Type.GetType("System.String"));
			dtNew.Columns.Add("ORDER_SUB_NO",   Type.GetType("System.String"));
			dtNew.Columns.Add("ERR_DESC",       Type.GetType("System.String"));
			
			// 设置主键
			DataColumn[] dcPrimeKey = new DataColumn[4];
			dcPrimeKey[0] = dtNew.Columns["PATIENT_ID"];
			dcPrimeKey[1] = dtNew.Columns["VISIT_ID"];
			dcPrimeKey[2] = dtNew.Columns["ORDER_NO"];
			dcPrimeKey[3] = dtNew.Columns["ORDER_SUB_NO"];
			
			dtNew.PrimaryKey = dcPrimeKey;

            // 变量赋值
            wardCode = GVars.User.DeptCode;
               
            if ((GVars.IniFile.ReadString("APP", "ORA_NLS_ZHS", "TRUE").ToUpper().Trim().Equals("TRUE")) == false)
            {
                oracleAccess = new OracleAccess();                
            }
            else
            {
                oracleAccess = new OleDbAccess();
            }
                        
            oracleAccess.ConnectionString = GVars.OracleAccess.ConnectionString;
		}
        
        
        /// <summary>
        /// 拆分医嘱
        /// </summary>
        /// <param name="wardCode"></param>
        /// <returns></returns>
        public void SplitOrders()
        {
            bool blnFirst = true;

            while(true)
            {
                try
                {
                    dsErr.Tables[0].Rows.Clear();                       // 不规范的医嘱
                    
                    // 获取当前时间
                    dtNow = GetSysDate();
                    
                    if (blnFirst) LogFile.WriteLog("获取系统日期 " + dtNow.ToLongDateString());

                    // 查找所有医嘱
                    if (blnExit == true) { return; }
                    dsOrders = GetOrdersToday(wardCode);
                  
                    
                    if (blnFirst) LogFile.WriteLog("获取病区所有医嘱 共:" + dsOrders.Tables[0].Rows.Count.ToString() + "条");
                    
                    // 查找今天以后(含今天)医嘱执行单
                    if (blnExit == true) { return; }
                    dsOrdersExecute = GetOrdersExecuteSplitted(wardCode, dtNow.ToString(ComConst.FMT_DATE.SHORT));
                  
                    
                    if (blnFirst) LogFile.WriteLog("获取已拆分医嘱执行单 共:" + dsOrdersExecute.Tables[0].Rows.Count.ToString() + "条");
                    
                    // 进行医嘱拆分(三天内)
                    arrSql_Del.Clear();

                    if (blnFirst) LogFile.WriteLog("进行医嘱拆分 开始:" + DateTime.Now.ToLongDateString());

                    for(int i = 1; i < 4; i++)
                    {
                        if (blnExit == true) { return; }
                        OrdersConvertExecute(i);                        
                    }
                    
                    if (blnFirst) LogFile.WriteLog("进行医嘱拆分 完成:" + DateTime.Now.ToLongDateString());

                    // 进行保存
                    if (blnExit == true) { return; }

                    if (blnFirst) LogFile.WriteLog("保存拆分的医嘱执行单 " + DateTime.Now.ToLongDateString());

                    oracleAccess.Connect();

                    try
                    {
                        // oracleAccess.BeginTrans();
                        
                        oracleAccess.Update(ref dsOrdersExecute, "ORDERS_EXECUTE", "SELECT * FROM ORDERS_EXECUTE");
                        
                        // 删除过期医嘱执行单
                        oracleAccess.ExecuteNoQuery(ref arrSql_Del);
                        
                        // oracleAccess.Commit();
                        
                        if (blnFirst) LogFile.WriteLog("保存拆分的医嘱执行单 完成");
                    }
                    catch (Exception ex)
                    {
                        oracleAccess.RollBack();
                        LogFile.WriteLog("后台拆分线程出错" + ex.Message);        // 异常日志
                    }
                    finally
                    {
                        oracleAccess.DisConnect();
                    }
                    
                    blnFirst = false;

                    try
                    {
                        locker.WaitOne();
                        dsOrdersOut = dsOrders.Copy();
                        dsErrOut    = dsErr.Copy();
                    }
                    finally
                    {
                        locker.ReleaseMutex();
                    }
                    
                    // 体息1分钟
                    for(int i = 0; i < 2 * 60; i++)
                    {
                        if (blnExit == true) { return; }
                        Thread.Sleep(1000);
                    }
                }
                catch(Exception ex)
                {
                    LogFile.WriteLog("后台拆分线程出错" + ex.Message);        // 异常日志
                    
                    // 体息1分钟
                    for(int i = 0; i < 1 * 60; i++)
                    {
                        if (blnExit == true) { return; }
                        Thread.Sleep(1000);
                    }
                }
            }
        }
        
        
        /// <summary>
        /// 查找需要拆分的医嘱
        /// </summary>
        /// <param name="wardCode">病区代码</param>
        /// <returns></returns>
        public DataSet GetOrdersToday(string wardCode)
        {
            string sql = "SELECT ORDERS.* ";
            
            sql += "FROM ";
            sql +=     "ORDERS, ";                                      // 医嘱
            sql +=     "PATS_IN_HOSPITAL ";                             // 在院病人
            
            sql += "WHERE ";
            sql +=     " (ORDERS.ORDER_CLASS = 'A' OR ORDERS.ORDER_CLASS = 'B')";
            sql +=     " AND PATS_IN_HOSPITAL.PATIENT_ID = ORDERS.PATIENT_ID ";
            sql +=     " AND PATS_IN_HOSPITAL.VISIT_ID = ORDERS.VISIT_ID ";
            sql +=     " AND PATS_IN_HOSPITAL.WARD_CODE = " + SQL.SqlConvert(wardCode);
            sql +=     " AND (ORDERS.STOP_DATE_TIME IS NULL OR ORDERS.STOP_DATE_TIME > SYSDATE - 3) ";

            sql += " ORDER BY ";
            sql +=      "ORDERS.PATIENT_ID, ORDERS.VISIT_ID, ORDERS.ORDER_NO, ORDERS.ORDER_SUB_NO";
            
            return oracleAccess.SelectData(sql);
        }
        
        
        /// <summary>
        /// 获取已经拆分了的医嘱
        /// </summary>
        /// <param name="wardCode">病区代码</param>
        /// <param name="conversionDate">拆分时间</param>
        /// <returns></returns>
        public DataSet GetOrdersExecuteSplitted(string wardCode, string conversionDate)
        {
            string sqlSel = "SELECT " + getTableColsList("ORDERS_EXECUTE") + " ";
            
            sqlSel += "FROM ";
            sqlSel +=      "ORDERS_EXECUTE, ";                              // 医嘱执行单
            sqlSel +=      "PATS_IN_HOSPITAL ";                             // 在院病人
            
            sqlSel += "WHERE ";
            sqlSel +=      "CONVERSION_DATE_TIME >= " + SQL.GetOraDbDate_Short(conversionDate);
            sqlSel +=      " AND PATS_IN_HOSPITAL.PATIENT_ID = ORDERS_EXECUTE.PATIENT_ID ";
            sqlSel +=      " AND PATS_IN_HOSPITAL.VISIT_ID = ORDERS_EXECUTE.VISIT_ID ";
            sqlSel +=      " AND PATS_IN_HOSPITAL.WARD_CODE = " + SQL.SqlConvert(wardCode);
            
            DataSet ds = oracleAccess.SelectData(sqlSel, "ORDERS_EXECUTE");
            
            // 设置主键
            DataColumn[] dcPrimary = new DataColumn[6];
            dcPrimary[0] = ds.Tables[0].Columns["PATIENT_ID"];
            dcPrimary[1] = ds.Tables[0].Columns["VISIT_ID"];
            dcPrimary[2] = ds.Tables[0].Columns["ORDER_NO"];
            dcPrimary[3] = ds.Tables[0].Columns["ORDER_SUB_NO"];
            dcPrimary[4] = ds.Tables[0].Columns["CONVERSION_DATE_TIME"];
            dcPrimary[5] = ds.Tables[0].Columns["SCHEDULE_PERFORM_TIME"];
            
            ds.Tables[0].PrimaryKey = dcPrimary;
            
            return ds;
        }
        
        
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
		    string      patientId       = string.Empty;                                         // 病人ID
		    string      visitId         = string.Empty;                                         // 本次就诊序号
		    string      orderNo         = string.Empty;                                         // 医嘱序号
		    string      orderSubNo      = string.Empty;                                         // 子医嘱序号
		    string      orderStatus     = string.Empty;                                         // 医嘱状态
		    DateTime    dtOrderStart;                                                           // 医嘱开始时间
		    string      orderStopDate   = string.Empty;                                         // 医嘱结束时间
		    DateTime    dtOrderStop;                                                            // 医嘱结束时间
		    
		    string      schedule        = string.Empty;                                         // 计划执行时间
		    DateTime    dtSchedule;                                                             // 计划执行时间
		    
		    DataRow     drExecuteNew    = null;                                                 // 新增的医嘱执行单
            string      frequency       = string.Empty;                                         // 医嘱频率描述
            bool        blnOnNeedTime   = false;                                                // 医嘱执行时间为必要时
            
		    string      filter          = string.Empty;                                         // 过滤条件
		    string      filterOra       = string.Empty;
            string      filterOra_Del   = string.Empty;
		    string      filterTemp      = string.Empty;                                         // 临时过滤条件		    
		    DataRow[]   drFind          = null;
            
            string      time            = string.Empty;                                         // 时间字符串
            
            DateTime    dtTreate        = dtNow.AddDays(days - 1);
            
            string      sqlDel          = "DELETE FROM ORDERS_EXECUTE WHERE (IS_EXECUTE IS NULL OR IS_EXECUTE = '0') AND ";
            #endregion
            
            int         counter         = 0;
			foreach(DataRow drOrder in dsOrders.Tables[0].Rows)
			{
			    Thread.Sleep(20);
			    counter++;
			    
			    try
			    {
			        #region 获取当前医嘱的信息
                    patientId     = drOrder["PATIENT_ID"].ToString();                               // 病人ID号
                    visitId       = drOrder["VISIT_ID"].ToString();                                 // 本次就诊序号
                    orderNo       = drOrder["ORDER_NO"].ToString();                                 // 医嘱序号
                    orderSubNo    = drOrder["ORDER_SUB_NO"].ToString();                             // 子医嘱序号
                    orderStatus   = drOrder["ORDER_STATUS"].ToString();                             // 医嘱状态
                    
                    dtOrderStart  = DateTime.Parse(drOrder["START_DATE_TIME"].ToString());          // 医嘱开始日期
                    frequency     = drOrder["FREQUENCY"].ToString().Trim();                         // 频率描述
                    blnOnNeedTime = frequency.Equals("必要时");                                     // 是否为必要时
                    orderStopDate = drOrder["STOP_DATE_TIME"].ToString();                           // 医嘱停止日期
                    
                    string freqIntervalUnit = drOrder["FREQ_INTERVAL_UNIT"].ToString().Trim();	// 时间间隔单位
                    
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
                    filterOra_Del = filter;

                    filter += " AND (CONVERSION_DATE_TIME >= " + SQL.SqlConvert(dtTreate.ToString(ComConst.FMT_DATE.SHORT))
                            + " AND  CONVERSION_DATE_TIME < " + SQL.SqlConvert(dtTreate.AddDays(1).ToString(ComConst.FMT_DATE.SHORT)) + ")";

                    filterOra_Del += " AND (CONVERSION_DATE_TIME >= " + SQL.GetOraDbDate(dtTreate.ToString(ComConst.FMT_DATE.SHORT))
                            + " AND  CONVERSION_DATE_TIME < " + SQL.GetOraDbDate(dtTreate.AddDays(1).ToString(ComConst.FMT_DATE.SHORT)) + ")";
                    #endregion
                    
                    #region 医嘱类型检查
                    // 如果不能确定是临时医嘱还是长期医嘱就不能区分
                    string repeator = drOrder["REPEAT_INDICATOR"].ToString().Trim();
                    if ("1".Equals(repeator) == false && "0".Equals(repeator) == false)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "不能确定是长期医嘱 或是临时医嘱!");
                        continue;
                    }
                    #endregion
                    
                    #region 临时医嘱
                    if ("1".Equals(repeator) == false)
                    {
                        drFind = dsOrdersExecute.Tables[0].Select(filter);

                        // 如果状态不为可执行, 删除以前生成的执行单
                        if ("2".Equals(orderStatus) == false && (dtTreate.Subtract(dtOrderStart)).Days < 3) 
                        {
                            if (days == 1) { arrSql_Del.Add(sqlDel + filterOra_Del); }

                            continue; 
                        }

                        // 如果已经拆分过, 查找下一条医嘱
                        if (drFind.Length > 0) { continue; }

                        // 进行拆分
                        if (dtTreate.Date.CompareTo(dtOrderStart.Date) == 0)
                        {
                            drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                            fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, dtOrderStart.ToString(ComConst.FMT_DATE.LONG));
                            dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                        }

                        continue;
                    }
                    #endregion
                                        
                    #region 不拆分三天前停止的医嘱
                    if ((dtOrderStop.Subtract(dtTreate.AddDays(-3))).Days <= 0)
                    {
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
                            + " AND SCHEDULE_PERFORM_TIME >= " + SQL.GetOraDbDate(dtOrderStop.ToString(ComConst.FMT_DATE.LONG));

                        arrSql_Del.Add(sqlDel + filterTemp);
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
                    
                    // 如果状态不为可执行, 查找下一条医嘱 (长期医嘱的停止必须有停止时间, 可不用状态进行判断)
                    if ("2".Equals(orderStatus) == false) { continue; }
                    #endregion
                    
                    #region 执行频率为 [必要时]
                    if (blnOnNeedTime == true)
                    {
                        schedule = dtTreate.ToString(ComConst.FMT_DATE.SHORT) + ComConst.STR.BLANK + "23:59:00";

                        drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                        fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, schedule);
                        dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                        continue;
                    }
                    #endregion
                    
                    #region 条件检查
                    // 没有时间间隔, 不进行拆分
                    if (freqIntervalUnit.Length == 0) 
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "无时间间隔 !");
                        continue; 
                    }
                    
                    // 时间间隔必须为[日]
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

                    // 获取计划执行时间
                    if (GetPerformSchedule(ref arrSchedule, freqCounter) == false)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[频率次数] 与 [计划执行时间] 不符!");
                        continue;
                    }
                    #endregion
                    
                    #region 处理时间间隔为 [小时] 的医嘱
                    if (freqIntervalUnit.IndexOf("小时") >= 0)
                    {
                        dtSchedule = dtOrderStart;

                        while (dtSchedule.CompareTo(dtTreate.Date) < 0)
                        {
                            dtSchedule = dtSchedule.AddHours(freqInterval);
                        }

                        while (dtSchedule.Day == dtTreate.Day)
                        {
                            if (dtOrderStart <= dtSchedule && (orderStopDate.Length == 0 || dtSchedule <= dtOrderStop))
                            {
                                drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                                fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, dtSchedule.ToString(ComConst.FMT_DATE.LONG));
                                dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                            }

                            dtSchedule = dtSchedule.AddHours(freqInterval);
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

                        if (dtOrderStart <= dtSchedule && (orderStopDate.Length == 0 || dtSchedule <= dtOrderStop))
                        {
                            drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                            fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, dtSchedule.ToString(ComConst.FMT_DATE.LONG));
                            dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
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

                            if (dtOrderStart <= dtSchedule && (orderStopDate.Length == 0 || dtSchedule <= dtOrderStop))
                            {
                                drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                                fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, dtSchedule.ToString(ComConst.FMT_DATE.LONG));
                                dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                            }
                        }

                        continue;
                    }
                    #endregion

                    #region  处理 N天一次 (隔日)
                    if (frequency.IndexOf("隔日") >= 0)
                    {
                        if (freqCounter != 1 || arrSchedule.Count > 1)
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "隔日医嘱 [频率次数] 与 [计划执行时间] 不符!");

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

                            // 有医嘱执行时间, 如何确定从哪一天开始执行
                            drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                            fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, schedule);
                            dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                        }

                        continue;
                    }
                    #endregion
                    
                    #region  处理 N天一次 (N/周)
                    if (frequency.IndexOf("周") >= 0)
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
                                    drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                                    fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, schedule);
                                    dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                                }
                            }
                            catch(Exception ex)
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
                catch(Exception ex)
			    {
    			     throw ex;
			    }
			}
		}
        
		
		/// <summary>
		/// 生成一条医嘱执行单
		/// </summary>
		/// <param name="drExecute">医嘱执行单</param>
		/// <param name="drOrder">医嘱</param>
		/// <param name="nurseName">护士名字</param>
		public void fillOrderExecute(ref DataRow drExecute, DataRow drOrder, DateTime dtConvert, string schedule)
	    {
	        // drExecute["WARD_CODE"] = wardCode;
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
            drExecute["DRUG_BILLING_ATTR"] = drOrder["DRUG_BILLING_ATTR"];
            drExecute["BILLING_ATTR"] = drOrder["BILLING_ATTR"];
            drExecute["CONVERSION_NURSE"] = nurseName;
            drExecute["CONVERSION_DATE_TIME"] = dtConvert;
            
            DateTime dtSchedule = DateTime.Parse(schedule);
            drExecute["ORDERS_PERFORM_SCHEDULE"] = dtSchedule.Hour.ToString();
            drExecute["SCHEDULE_PERFORM_TIME"] = dtSchedule;
		}
        
        
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
			string      sepStr	= "-";						
			string[]    arrPart = performSchedule.Split(sepStr.ToCharArray());
            
            for(int i = 0; i < arrPart.Length; i++)
            {
                arrPart[i] = (arrPart[i].Trim().Length == 1? "0" : string.Empty) + arrPart[i];
                                
                arrSchedule.Add(arrPart[i]);
            }
            
			return arrSchedule;
		}
        
        
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
                        if (dtOrderStart.ToString(ComConst.FMT_DATE.SHORT) .CompareTo(dtTreate.ToString(ComConst.FMT_DATE.SHORT)) < 0)
                        {
                            return true;
                        }
	                }
	            }
	        }
            
            // 如果不确定单双日
            return (dtSchedule.CompareTo(dtOrderStart) > 0);
        }
        
        
        /// <summary>
        /// 获取某一医嘱执行单的任一转换日期
        /// </summary>
        /// <returns></returns>
        public string GetOrderExecuteConvertedDate(string filter)
        {
            string sql = "SELECT CONVERSION_DATE_TIME FROM ORDERS_EXECUTE WHERE " + filter;
                        
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
            
            DataRow drNew = dsErr.Tables[0].NewRow();
            
            drNew["PATIENT_ID"]     = patientId;
            drNew["VISIT_ID"]       = visitId;
            drNew["ORDER_NO"]       = orderNo;
            drNew["ORDER_SUB_NO"]   = orderSubNo;
            drNew["ERR_DESC"]       = errDesc;            
            
            dsErr.Tables[0].Rows.Add(drNew);
        }
        
        
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
		
		
		/// <summary>
		/// 获取处理中的数据
		/// </summary>
		/// <param name="dsParaOrder"></param>
		/// <param name="dsParaErr"></param>
		public void GetData(ref DataSet dsParaOrder, ref DataSet dsParaErr)
		{
		    try
		    {
		        locker.WaitOne();
		        if (dsOrdersOut != null)
		        {
		            dsParaOrder = dsOrdersOut.Copy();
		        }
		        
		        if (dsErrOut != null)
		        {
		            dsParaErr = dsErrOut.Copy();
		        }
		    }
		    finally
		    {
		        locker.ReleaseMutex();
		    }
		}
		
		
		/// <summary>
		/// 是否有错
		/// </summary>
		/// <returns></returns>
		public bool HaveError()
		{
            try
		    {
		        locker.WaitOne();
		        return (dsErrOut != null && dsErrOut.Tables.Count > 0 && dsErrOut.Tables[0].Rows.Count > 0);
		    }
		    finally
		    {
		        locker.ReleaseMutex();
		    }		
		}
		

        /// <summary>
        /// 获取表的字段列表(时间戳类型除外)
        /// </summary>
        /// <returns></returns>
        private string getTableColsList(string tableName)
        {
            tableName = tableName.ToUpper();
            string sql = "SELECT * FROM DBA_TAB_COLUMNS WHERE TABLE_NAME = " + SqlManager.SqlConvert(tableName)
                + "AND OWNER = 'ORDADM'";
            DataSet     ds = null;
            
            switch(tableName)
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
            foreach(DataRow dr in ds.Tables[0].Rows)
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
	}
}
