using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HISPlus;
using System.Configuration;
using SQL = HISPlus.SqlManager;
using System.Collections;

namespace BodyTemp
{
    public class OrderSplit
    {
        #region 字段和属性

        private static DbAccess oracleAccess = null;
        private DataSet dsOrders = null;
        private DataSet dsOrdersExecute = null;
        private List<BodyTemp.MyComm.SyncInfo> syncInfos = new List<BodyTemp.MyComm.SyncInfo>();
        private BodyTemp.MyComm.SyncInfo syncInfo;
        private string nurseName = "SYSTEM";
        private bool isPharmacologic;
        public static DbAccess OracleAccess
        {
            get
            {
                if (oracleAccess == null)
                {
                    try
                    {
                        oracleAccess = new OracleAccess();
                        string connStr = ConfigurationManager.AppSettings["StrConnMobile"].ToString();
                        connStr = new EnDecrypt().Decrypt(connStr);
                        oracleAccess.ConnectionString = connStr;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                return oracleAccess;
            }
        }
        /// <summary>
        /// 药疗非药疗
        /// </summary>
        public bool IsPharmacologic
        {
            get { return this.isPharmacologic; }
            set { this.isPharmacologic = value; }
        }
        /// <summary>
        /// 将要拆分的医嘱
        /// </summary>
        public DataSet DsOrders
        {
            get { return this.dsOrders; }
            set { this.dsOrders = value; }
        }
        /// <summary>
        /// 已拆分的医嘱
        /// </summary>
        public DataSet DsOrdersExecute
        {
            get { return this.dsOrdersExecute; }
            set { this.dsOrdersExecute = value; }
        }
        /// <summary>
        /// 拆分相关配置信息
        /// </summary>
        public List<BodyTemp.MyComm.SyncInfo> SyncInfos
        {
            get { return this.syncInfos; }
            set
            {
                this.syncInfos = value;
                this.syncInfo = this.syncInfos[0];
            }
        }

        private DataSet dsErr = new DataSet();
        /// <summary>
        /// 拆分错误医嘱行
        /// </summary>
        public DataSet ErrorDs
        {
            get { return this.dsErr; }
            set { this.dsErr = value; }
        }
        /// <summary>
        /// 拆分时间
        /// </summary>
        public DateTime OperationTime { get; set; }

        #endregion

        #region 获取系统日期
        /// <summary>
        /// 获取系统日期
        /// </summary>
        /// <returns>系统当前日期</returns>
        public static DateTime GetSysDate()
        {
            if (OracleAccess.SelectValue("SELECT SYSDATE FROM DUAL") == true)
            {
                return DateTime.Parse(OracleAccess.GetResult(0));
            }
            else
            {
                throw new Exception("获取系统日期出错!");
            }
        }
        #endregion

        #region 拆分医嘱
        /// <summary>
        /// 拆分单条医嘱
        /// </summary>
        /// <param name="orderDs"></param>
        /// <returns></returns>
        public DataSet SplitMedicalOrders(DataSet orderDs)
        {
            if (orderDs == null || orderDs.Tables.Count == 0 || orderDs.Tables[0].Rows.Count == 0)
            {
                throw new Exception("要拆分的医嘱数据不正确！");
            }
            orderDs.Tables[0].TableName = "orders_m";
            this.dsOrders = orderDs;
            DataRow row = orderDs.Tables[0].Rows[0];
            this.isPharmacologic = row["ORDER_CLASS"].ToString().Trim() == "A";
            string wardCode = row["WARD_CODE"].ToString().Trim();
            this.syncInfos.Clear();
            syncInfo = new BodyTemp.MyComm.SyncInfo() { Filter = "'" + wardCode + "'", Comment = isPharmacologic ? "有效医嘱" : "有效非药疗", SrcSqlFile = row["ORDER_CLASS"].ToString().Trim(), FnPreLog = "" };
            this.syncInfos.Add(syncInfo);
            this.dsErr.Tables.Add(CreateErrorDt());
            OperationTime = GetSysDate();
            this.dsOrdersExecute = GetOrdersExecuteSplitted(OperationTime.ToString(ComConst.FMT_DATE.SHORT), row["PATIENT_ID"].ToString(), row["VISIT_ID"].ToString(), row["ORDER_NO"].ToString(), row["ORDER_SUB_NO"].ToString());


            int splitCount = Convert.ToInt32(ConfigurationManager.AppSettings["SPLIT_DATES"] ?? "2");
            for (int i = 1; i <= splitCount; i++)
            {
                SplitMedicalOrders(i, SplitSourceType.SplitSourceTypeNurseClient);
            }
            //保存拆分结果
            saveSplitResult();
            saveOrdersResult();
            return dsErr;
        }

        /// <summary>
        /// 拆分方法
        /// </summary>
        /// <param name="days"></param>
        /// <param name="splitSourceType">拆分数据来源类型默认为服务器端拆分程序</param>
        public void SplitMedicalOrders(int days, SplitSourceType splitSourceType = SplitSourceType.SplitSourceTypeServer)
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

            DateTime dtTreate = OperationTime.AddDays(days - 1);

            string logLine = string.Empty;
            #endregion

            //int counter = 0;

            //需要拆分的医嘱
            DataRow[] drOrders = dsOrders.Tables[0].Select("", "PATIENT_ID, VISIT_ID, ORDER_NO, ORDER_SUB_NO");

            for (int c = 0; c < drOrders.Length; c++)
            {
                #region 输出日志
                logLine = BodyTemp.MyComm.SysConst.MsgOptDev.LOG + "----------" + syncInfos[0].FnPreLog + "----------" + BodyTemp.MyComm.SysConst.STR.CRLF +
                    "第" + c.ToString() + "行" + BodyTemp.MyComm.SysConst.STR.VERTICAL_LINE;
                #endregion

                DataRow drOrder = drOrders[c];

                //counter++;
                //if (counter % 1000 == 500)
                //{
                //    TraceInfo = SysConst.MsgOptDev.SCREEN + "已拆分 " + counter + "条";
                //}

                //if (blnExit)
                //{
                //    return;
                //}

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
                    //2015.11.23 日志
                    logLine += BodyTemp.MyComm.SysConst.MsgOptDev.LOG +
                        c.ToString() + BodyTemp.MyComm.SysConst.STR.VERTICAL_LINE +
                        "pid:" + patientId + BodyTemp.MyComm.SysConst.STR.VERTICAL_LINE + "vid:" + visitId + BodyTemp.MyComm.SysConst.STR.VERTICAL_LINE +
                       "OrderNo:" + orderNo + BodyTemp.MyComm.SysConst.STR.UnderLine + orderSubNo + BodyTemp.MyComm.SysConst.STR.VERTICAL_LINE + frequency +
                       BodyTemp.MyComm.SysConst.STR.CRLF;

                    if (orderStopDate.Length > 0)
                    {
                        dtOrderStop = DateTime.Parse(orderStopDate);
                    }
                    else
                    {
                        dtOrderStop = dtTreate.AddDays(1);
                    }
                    #endregion

                    #region 医嘱开始时间检查：（1）开始日期大于执行日期，不拆分，不写split_stauts
                    if (isPharmacologic)
                    {
                        if (dtOrderStart.Date > dtTreate.Date)
                        {
                            logLine += "开始时间大于执行时间-返回";
                            continue;
                        }
                    }
                    #endregion

                    #region 医嘱类型检查
                    // 如果不能确定是临时医嘱还是长期医嘱就不能区分

                    //长期医嘱标志
                    string repeator = drOrder["REPEAT_INDICATOR"].ToString().Trim();

                    //不是长医嘱，也不是临时医嘱
                    if ("1".Equals(repeator) == false && "0".Equals(repeator) == false)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "不能确定是长期医嘱 或是临时医嘱!");
                        drOrder["SPLIT_MEMO"] = "不能确定是长期医嘱 或是临时医嘱!";
                        drOrder["SPLIT_STAUTS"] = "2";
                        drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                        logLine += "不能确定是长期医嘱 或是临时医嘱-返回";
                        continue;
                    }
                    //频次为空  del
                    else if (isPharmacologic && "1".Equals(repeator) == true && frequency.Length == 0)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "长期医嘱，频次不能为空!");
                        drOrder["SPLIT_MEMO"] = "长期医嘱，频次不能为空!";
                        drOrder["SPLIT_STAUTS"] = "2";
                        drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                        logLine += "长期医嘱，频次不能为空-返回";
                        continue;
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

                    #region 临时医嘱折分
                    if ("1".Equals(repeator) == false)
                    {
                        //按照条件在ORDERS_EXECUTE表中查找数据
                        drFind = dsOrdersExecute.Tables[0].Select(filter);
                        logLine += "临时医嘱：检查已拆分，条件：" + BodyTemp.MyComm.SysConst.STR.RIGHT_ARROW + filter + BodyTemp.MyComm.SysConst.STR.CRLF + BodyTemp.MyComm.SysConst.STR.RIGHT_ARROW;
                        // 如果已经拆分过, 查找下一条医嘱
                        if (drFind.Length > 0)
                        {
                            logLine += "有重复-返回";
                            continue;
                        }

                        // 进行拆分
                        if (dtTreate.Date.CompareTo(dtOrderStart.Date) == 0)
                        {
                            try
                            {
                                drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                                drOrder["SPLIT_STAUTS"] = "1";
                                drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                                fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, dtOrderStart.ToString(ComConst.FMT_DATE.LONG), "[867]临时医嘱", splitSourceType);
                                dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                                logLine += "成功" + BodyTemp.MyComm.SysConst.STR.CRLF;
                            }
                            catch (Exception ex)
                            {
                                addErrOrder(days, patientId, visitId, orderNo, orderSubNo, ex.Message);
                                drOrder["SPLIT_MEMO"] = ex.Message;
                                drOrder["SPLIT_STAUTS"] = "2";
                                drOrder["CONVERSION_DATE_TIME"] = dtTreate;
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
                        logLine += "删除超停止时间的执行单,共" + drFind.Length.ToString() + "条，条件:" + BodyTemp.MyComm.SysConst.STR.RIGHT_ARROW + filterTemp + BodyTemp.MyComm.SysConst.STR.CRLF;
                        for (int j = 0; j < drFind.Length; j++)
                        {
                            drFind[j].Delete();
                        }
                    }
                    #endregion

                    #region 不拆分三天前停止的医嘱
                    if ((dtOrderStop.Subtract(dtTreate.AddDays(-3))).Days <= 0)
                    {
                        drOrder["SPLIT_MEMO"] = "不拆分三天前停止的医嘱";
                        drOrder["SPLIT_STAUTS"] = "2";
                        drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                        logLine += "不拆分三天前停止的医嘱-返回";
                        continue;
                    }
                    #endregion

                    #region 条件检查
                    // 如果当天的医嘱执行单存在, 不进行拆分
                    logLine += "长期医嘱，检查已拆分，条件：" + BodyTemp.MyComm.SysConst.STR.CRLF;
                    if (blnOnNeedTime == true)
                    {
                        drFind = dsOrdersExecute.Tables[0].Select(filter + " AND (IS_EXECUTE IS NULL OR IS_EXECUTE <> '1')");
                        logLine += filter + " AND (IS_EXECUTE IS NULL OR IS_EXECUTE <> '1')" + BodyTemp.MyComm.SysConst.STR.CRLF;
                    }
                    else
                    {
                        drFind = dsOrdersExecute.Tables[0].Select(filter);
                        logLine += filter + BodyTemp.MyComm.SysConst.STR.CRLF;
                    }

                    // 如果已经拆分过, 查找下一条医嘱
                    if (drFind.Length > 0)
                    {
                        drOrder["SPLIT_STAUTS"] = "1";
                        drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                        logLine += "找到-返回";
                        continue;
                    }

                    // 医嘱状态不等于2或者6，但其实在没有到时间以前，该医嘱还有效
                    //// 如果状态不为可执行, 查找下一条医嘱  。停止时间  可能是明天或者更 ····
                    //if ("26".IndexOf(orderStatus) < 0) { continue; }
                    #endregion

                    #region 频次为空的长期医嘱 2015.11.18 从长期医嘱复制，只保留PRN条件，见return改为continue
                    //从SplitTheMedicationOrders复制，并增加已拆分校验，删除  
                    if ((string.IsNullOrEmpty(frequency) || frequency == "PRN"))
                    {
                        if (!isPharmacologic)
                        {
                            #region 2015.11.17 增加已拆分校验 2015.11.24重复校验，屏蔽
                            ////按照条件在ORDERS_EXECUTE表中查找数据
                            drFind = dsOrdersExecute.Tables[0].Select(filter);

                            // 如果已经拆分过, 查找下一条医嘱
                            if (drFind.Length > 0)
                            { //2015.11.17
                                continue;
                            }
                            #endregion
                        }
                        //根据频次判断，如果没有频次，则直接复制到ORDERS_EXECUTE表
                        //1：
                        try
                        {
                            logLine += "分支：" + "PRN";
                            drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                            drOrder["SPLIT_STAUTS"] = "1";
                            drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                            fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, dtOrderStart.ToString(ComConst.FMT_DATE.LONG), "[964]长期医嘱PRN", splitSourceType);
                            drExecuteNew["SCHEDULE_PERFORM_TIME"] = new DateTime(dtTreate.Year, dtTreate.Month, dtTreate.Day,
                                dtOrderStart.Hour, dtOrderStart.Minute, dtOrderStart.Second);//2015.11.15 没有频次的非药疗医嘱，执行时间为当天
                            dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                        }
                        catch (Exception ex)
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, ex.Message);
                            drOrder["SPLIT_MEMO"] = ex.Message;
                            drOrder["SPLIT_STAUTS"] = "2";
                            drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                            continue;
                        }
                        continue; //2015.11.17
                    }
                    #endregion

                    #region 执行频率为 [必要时]
                    if (blnOnNeedTime == true)
                    {
                        //计划执行时间
                        schedule = dtTreate.ToString(ComConst.FMT_DATE.SHORT) + ComConst.STR.BLANK + "23:59:00";

                        try
                        {
                            logLine += "分支：" + "必要时";
                            drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                            drOrder["SPLIT_STAUTS"] = "1";
                            drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                            fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, schedule, "[993]长期医嘱必要时", splitSourceType);
                            dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                        }
                        catch (Exception ex)
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, ex.Message);
                            drOrder["SPLIT_MEMO"] = ex.Message;
                            drOrder["SPLIT_STAUTS"] = "2";
                            drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                        }
                        continue;
                    }
                    #endregion

                    #region 条件检查

                    // 如果  频率为空 ，则这条医嘱放弃 ，不拆分 del
                    if (frequency.Length == 0)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "频次为空 !");
                        drOrder["SPLIT_MEMO"] = "频次为空 !";
                        drOrder["SPLIT_STAUTS"] = "2";
                        drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                        logLine += "频次为空-返回";
                        continue;
                    }
                    // 没有时间间隔, 不进行拆分 del                   
                    if (isPharmacologic && freqIntervalUnit.Length == 0)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "无时间间隔 !");
                        drOrder["SPLIT_MEMO"] = "无时间间隔 !";
                        drOrder["SPLIT_STAUTS"] = "2";
                        drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                        logLine += "无时间间隔-返回";
                        continue;
                    }

                    // 时间间隔必须为[日],[小时],[周]
                    if (freqIntervalUnit.IndexOf("日") < 0
                        && freqIntervalUnit.IndexOf("小时") < 0
                        && freqIntervalUnit.IndexOf("周") < 0)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[医嘱频率] 的间隔单位必须是 [日]、[小时]或[周] !");
                        drOrder["SPLIT_MEMO"] = "[医嘱频率] 的间隔单位必须是 [日]、[小时]或[周] !";
                        drOrder["SPLIT_STAUTS"] = "2";
                        drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                        logLine += "[医嘱频率] 的间隔单位必须是 [日]、[小时]或[周]-返回";
                        continue;
                    }

                    if (DataType.IsNumber(drOrder["FREQ_INTERVAL"].ToString()) == false)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[频率间隔] 必须为正整数!");
                        drOrder["SPLIT_MEMO"] = "[频率间隔] 必须为正整数!";
                        drOrder["SPLIT_STAUTS"] = "2";
                        drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                        logLine += "[频率间隔] 必须为正整数-返回";
                        continue;
                    }

                    if (DataType.IsNumber(drOrder["FREQ_COUNTER"].ToString()) == false)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[频率次数] 必须为正整数!");
                        drOrder["SPLIT_MEMO"] = "[频率次数] 必须为正整数!";
                        drOrder["SPLIT_STAUTS"] = "2";
                        drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                        logLine += "[频率次数] 必须为正整数-返回";
                        continue;
                    }
                    #endregion

                    #region 获取执行计划

                    int freqInterval = int.Parse(drOrder["FREQ_INTERVAL"].ToString());  // 执行频率的间隔部分
                    int freqCounter = int.Parse(drOrder["FREQ_COUNTER"].ToString());   // 执行频率的次数部分
                    string performSchedule = drOrder["PERFORM_SCHEDULE"].ToString();           // 护士执行时间

                    if (!isPharmacologic && freqCounter >= 1 && performSchedule.Length == 0)
                    {
                        if (frequency == "QD")
                            performSchedule = Convert.ToDateTime(drOrder["START_DATE_TIME"].ToString()).ToString("HH:mm");
                        else
                            try
                            {
                                performSchedule = System.Configuration.ConfigurationManager.AppSettings[frequency].ToString();
                            }
                            catch (Exception ex)
                            {
                                string msg = "频率:'" + frequency + "'未配置";
                                addErrOrder(days, patientId, visitId, orderNo, orderSubNo, msg);
                                drOrder["SPLIT_MEMO"] = msg;
                                drOrder["SPLIT_STAUTS"] = "2";
                                drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                                return;
                            }
                    }

                    ArrayList arrSchedule = getOrderPerformSchedule(performSchedule);

                    if (isPharmacologic)
                    {
                        // 计划执行时间不能为空  del

                        if (freqCounter >= 1 && performSchedule.Length == 0)
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "计划执行时间不能为空！");
                            drOrder["SPLIT_MEMO"] = "计划执行时间不能为空！";
                            drOrder["SPLIT_STAUTS"] = "2";
                            drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                            logLine += "计划执行时间不能为空-返回";
                            continue;
                        }


                        // 获取计划执行时间, 如果时间间隔不为[小时]
                        if (freqIntervalUnit.Equals("日"))
                        {
                            if (GetPerformSchedule(ref arrSchedule, freqCounter) == false)
                            {
                                addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[频率次数] 与 [计划执行时间] 不符!");
                                drOrder["SPLIT_MEMO"] = "[频率次数] 与 [计划执行时间] 不符!";
                                drOrder["SPLIT_STAUTS"] = "2";
                                drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                                logLine += "[频率次数] 与 [计划执行时间] 不符-返回";
                                continue;
                            }
                        }

                        if (freqIntervalUnit.Equals("小时"))
                        {
                            if (ComConst.VAL.HOURS_PER_DAY / freqInterval != arrSchedule.Count)
                            {
                                addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[频率次数] 与 [计划执行时间] 不符!");
                                drOrder["SPLIT_MEMO"] = "[频率次数] 与 [计划执行时间] 不符!";
                                drOrder["SPLIT_STAUTS"] = "2";
                                drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                                logLine += "[频率次数] 与 [计划执行时间] 不符-返回";
                                continue;
                            }
                        }
                    }
                    #endregion

                    #region 处理时间间隔为 [1/X小时] 的医嘱
                    if (freqIntervalUnit.IndexOf("小时") >= 0)
                    {
                        logLine += "分支-[1/X小时] ";
                        int hours = 0;
                        for (int i = 0; i < arrSchedule.Count; i++)
                        {
                            if (int.TryParse(arrSchedule[i].ToString(), out hours) == false)
                            {
                                addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[计划执行时间] 有误!");
                                drOrder["SPLIT_MEMO"] = "[计划执行时间] 有误!";
                                drOrder["SPLIT_STAUTS"] = "2";
                                drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                                continue;
                            }

                            dtSchedule = dtTreate.Date.AddHours(hours);
                            if (dtOrderStart < dtSchedule.AddMinutes(1) && (orderStopDate.Length == 0 || dtSchedule <= dtOrderStop))
                            {
                                try
                                {
                                    drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                                    drOrder["SPLIT_STAUTS"] = "1";
                                    drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                                    fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, dtSchedule.ToString(ComConst.FMT_DATE.LONG), "[1133]长期医嘱1次/X小时", splitSourceType);
                                    dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                                }
                                catch (Exception ex)
                                {
                                    addErrOrder(days, patientId, visitId, orderNo, orderSubNo, ex.Message);
                                    drOrder["SPLIT_MEMO"] = ex.Message;
                                    drOrder["SPLIT_STAUTS"] = "2";
                                    drOrder["CONVERSION_DATE_TIME"] = dtTreate;
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
                        logLine += "分支-一天一次 日";
                        time = (string)arrSchedule[0];

                        if (DataType.IsTime(ref time) == false)
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[计划执行时间] 有误!");
                            drOrder["SPLIT_MEMO"] = "[计划执行时间] 有误!";
                            drOrder["SPLIT_STAUTS"] = "2";
                            drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                            continue;
                        }

                        dtSchedule = DateTime.Parse(dtTreate.ToString(ComConst.FMT_DATE.SHORT) + ComConst.STR.BLANK + time);

                        if (/*dtOrderStart < dtSchedule.AddMinutes(1) && */(orderStopDate.Length == 0 || dtSchedule <= dtOrderStop))//2015.11.16
                        {
                            try
                            {
                                drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                                drOrder["SPLIT_STAUTS"] = "1";
                                drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                                fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, dtSchedule.ToString(ComConst.FMT_DATE.LONG), "[1175]长期医嘱1天1次", splitSourceType);
                                dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                            }
                            catch (Exception ex)
                            {
                                addErrOrder(days, patientId, visitId, orderNo, orderSubNo, ex.Message);
                                drOrder["SPLIT_MEMO"] = ex.Message;
                                drOrder["SPLIT_STAUTS"] = "2";
                                drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                            }
                        }

                        continue;
                    }
                    #endregion

                    #region 处理 一天N次
                    if (freqIntervalUnit.IndexOf("日") >= 0 && freqInterval == 1)
                    {
                        logLine += "分支-一天N次 日";
                        if (isPharmacologic && freqCounter != arrSchedule.Count)
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[频率次数] 与 [计划执行时间] 不符!");
                            drOrder["SPLIT_MEMO"] = "[频率次数] 与 [计划执行时间] 不符!";
                            drOrder["SPLIT_STAUTS"] = "2";
                            drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                            continue;
                        }

                        for (int i = 0; i < arrSchedule.Count; i++)
                        {
                            time = (string)arrSchedule[i];
                            if (DataType.IsTime(ref time) == false)
                            {
                                addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[计划执行时间] 有误!");
                                drOrder["SPLIT_MEMO"] = "[计划执行时间] 有误!";
                                drOrder["SPLIT_STAUTS"] = "2";
                                drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                                break;
                            }

                            dtSchedule = DateTime.Parse(dtTreate.ToString(ComConst.FMT_DATE.SHORT) + ComConst.STR.BLANK + time);

                            if (/*dtOrderStart < dtSchedule.AddMinutes(1) &&*/ (orderStopDate.Length == 0 || dtSchedule <= dtOrderStop))//2015.11.16 需求拆分全天，暂时屏蔽，应增加配置参数
                            {
                                try
                                {
                                    drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                                    drOrder["SPLIT_STAUTS"] = "1";
                                    drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                                    fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, dtSchedule.ToString(ComConst.FMT_DATE.LONG), "[1225]长期医嘱1天N次", splitSourceType);
                                    dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                                }
                                catch (Exception ex)
                                {
                                    addErrOrder(days, patientId, visitId, orderNo, orderSubNo, ex.Message);
                                    drOrder["SPLIT_MEMO"] = ex.Message;
                                    drOrder["SPLIT_STAUTS"] = "2";
                                    drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                                }
                            }
                        }

                        continue;
                    }
                    #endregion

                    #region  处理 N天一次 (隔日)
                    //if (frequency.IndexOf("Qod") >= 0 || frequency.IndexOf("隔日") >= 0)
                    if (frequency.ToUpper().IndexOf("QOD") >= 0 || frequency.IndexOf("隔日") >= 0)
                    {
                        logLine += "分支-N天一次 QOD";
                        if (freqCounter != 1 || arrSchedule.Count > 1)
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "隔日医嘱 [频率次数] 与 [计划执行时间] 不符!");
                            drOrder["SPLIT_MEMO"] = "隔日医嘱 [频率次数] 与 [计划执行时间] 不符!";
                            drOrder["SPLIT_STAUTS"] = "2";
                            drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                            continue;
                        }

                        if (!performSchedule.Contains(":"))
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "不是有效的时间格式");
                            drOrder["SPLIT_MEMO"] = "不是有效的时间格式";
                            drOrder["SPLIT_STAUTS"] = "2";
                            drOrder["CONVERSION_DATE_TIME"] = dtTreate;
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
                                    drOrder["SPLIT_STAUTS"] = "1";
                                    drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                                    fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, schedule, "[1286]长期医嘱隔日", splitSourceType);
                                    dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                                }
                                catch (Exception ex)
                                {
                                    addErrOrder(days, patientId, visitId, orderNo, orderSubNo, ex.Message);
                                    drOrder["SPLIT_MEMO"] = ex.Message;
                                    drOrder["SPLIT_STAUTS"] = "2";
                                    drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                                }
                            }
                        }

                        continue;
                    }
                    #endregion

                    #region  处理 N天一次 (N/周)
                    if (freqIntervalUnit.IndexOf("周") >= 0)
                    {
                        logLine += "分支-N天一次 周";
                        if (arrSchedule.Count != freqCounter)
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "周医嘱 [频率次数] 与 [计划执行时间] 不符!");
                            drOrder["SPLIT_MEMO"] = "周医嘱 [频率次数] 与 [计划执行时间] 不符!";
                            drOrder["SPLIT_STAUTS"] = "2";
                            drOrder["CONVERSION_DATE_TIME"] = dtTreate;
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
                                            drOrder["SPLIT_STAUTS"] = "1";
                                            drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                                            fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, schedule, "[1335]长期医嘱N天一次", splitSourceType);
                                            dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                                        }
                                        catch (Exception ex)
                                        {
                                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, ex.Message);
                                            drOrder["SPLIT_MEMO"] = ex.Message;
                                            drOrder["SPLIT_STAUTS"] = "2";
                                            drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                addErrOrder(days, patientId, visitId, orderNo, orderSubNo, ex.Message);
                                drOrder["SPLIT_MEMO"] = ex.Message;
                                drOrder["SPLIT_STAUTS"] = "2";
                                drOrder["CONVERSION_DATE_TIME"] = dtTreate;
                            }
                        }

                        continue;
                    }
                    #endregion

                    // 不能处理的情况, 报错
                    addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "该医嘱不满足拆分条件!");
                    logLine += "该医嘱不满足拆分条件!";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

        public static DataTable CreateErrorDt()
        {
            DataTable dtNew = new DataTable();

            // 创建该表的字段
            // 创建该表的字段
            dtNew.Columns.Add("PATIENT_ID", Type.GetType("System.String"));
            dtNew.Columns.Add("VISIT_ID", Type.GetType("System.String"));
            dtNew.Columns.Add("ORDER_NO", Type.GetType("System.String"));
            dtNew.Columns.Add("ORDER_SUB_NO", Type.GetType("System.String"));
            dtNew.Columns.Add("ERR_DESC", Type.GetType("System.String"));

            // 设置主键
            DataColumn[] dcPrimeKey = new DataColumn[4];

            dcPrimeKey[0] = dtNew.Columns["PATIENT_ID"];
            dcPrimeKey[1] = dtNew.Columns["VISIT_ID"];
            dcPrimeKey[2] = dtNew.Columns["ORDER_NO"];
            dcPrimeKey[3] = dtNew.Columns["ORDER_SUB_NO"];

            dtNew.PrimaryKey = dcPrimeKey;

            return dtNew;
        }

        #region 查找需要拆分的医嘱

        /// <summary>
        /// 测试方法：查找需要拆分的医嘱（测试查询一条数据）
        /// 2015-11-11
        /// 修改
        /// </summary>
        /// <param name="wardCode">病区代码</param>
        /// <returns></returns>
        public DataSet GetOrdersToday()
        {
            string strSql = "SELECT * FROM ORDERS_M WHERE ORDER_CLASS = 'A' ";
            string conversionDate = GetSysDate().AddDays(3 - 1).ToString(ComConst.FMT_DATE.SHORT);
            #region 状态为空或今天未拆分过 2015.11.13
            strSql += " AND WARD_CODE ='1001N' AND ( SPLIT_STAUTS IS  NULL OR (";
            strSql += " SPLIT_STAUTS IS NOT NULL AND CONVERSION_DATE_TIME < " + SQL.GetOraDbDate_Short(conversionDate);
            strSql += " )) ";
            strSql += " AND PATIENT_ID='78787878' AND VISIT_ID='1'";
            //Console.WriteLine("药疗:" + strSql);//2015.11.15调试
            #endregion
            return OracleAccess.SelectData(strSql, "ORDERS_M");
        }

        /// <summary>
        /// 查找需要拆分的医嘱
        /// 2015-11-11
        /// 修改
        /// </summary>
        /// <param name="wardCode">病区代码</param>
        /// <returns></returns>
        public DataSet GetOrdersToday(string conversionDate)
        {
            BodyTemp.MyComm.SyncInfo syncInfo = syncInfos[0];
            string strSql = string.Empty;
            //判断药疗还是非药疗
            if (syncInfo.Comment.Contains("有效医嘱"))
            {
                strSql = "SELECT * FROM ORDERS_M WHERE ORDER_CLASS = 'A' ";
            }
            else if (syncInfo.Comment.Contains("有效非药疗"))
            {
                strSql = "SELECT * FROM ORDERS_M WHERE ORDER_CLASS IN(" + syncInfo.SrcSqlFile + ")";
            }
            #region 状态为空或今天未拆分过 2015.11.13
            strSql += " AND WARD_CODE IN (" + syncInfo.Filter + ") AND ( SPLIT_STAUTS IS  NULL OR (";
            strSql += " SPLIT_STAUTS IS NOT NULL AND CONVERSION_DATE_TIME < " + SQL.GetOraDbDate_Short(conversionDate);
            strSql += " )) ";
            #endregion
            return OracleAccess.SelectData(strSql, "ORDERS_M");
        }
        #endregion

        #region 获取已经拆分了的医嘱
        /// <summary>
        /// 获取已经拆分了的医嘱
        /// </summary>
        /// <param name="wardCode">病区代码</param>
        /// <param name="conversionDate">拆分时间</param>
        /// <returns></returns>
        public DataSet GetOrdersExecuteSplitted(string conversionDate)
        {
            return GetOrdersExecuteSplitted(conversionDate, null, null, null, null);
        }
        /// <summary>
        /// 获取已经拆分了的医嘱,单条医嘱拆分
        /// </summary>
        /// <param name="wardCode">病区代码</param>
        /// <param name="conversionDate">拆分时间</param>
        /// <returns></returns>
        public DataSet GetOrdersExecuteSplitted(string conversionDate, string patientId, string visitId, string orderId, string subOrderId)
        {
            string sqlSel = "SELECT ORDERS_EXECUTE.* ";
            sqlSel += "FROM ";
            sqlSel += "ORDERS_EXECUTE, ";                              // 医嘱执行单
            sqlSel += "PATIENT_INFO ";                             // 在院病人

            sqlSel += "WHERE ";
            sqlSel += "ORDERS_EXECUTE.CONVERSION_DATE_TIME >= " + SQL.GetOraDbDate_Short(conversionDate);
            sqlSel += " AND PATIENT_INFO.PATIENT_ID = ORDERS_EXECUTE.PATIENT_ID ";
            sqlSel += " AND PATIENT_INFO.VISIT_ID = ORDERS_EXECUTE.VISIT_ID     ";
            BodyTemp.MyComm.SyncInfo syncInfo = syncInfos[0];

            if (syncInfo.Comment.Contains("有效医嘱"))
                sqlSel += " and ORDER_CLASS = 'A'";
            else if (syncInfo.Comment.Contains("有效非药疗"))
                sqlSel += " and ORDER_CLASS IN (" + syncInfo.SrcSqlFile + ")";

            sqlSel += " and PATIENT_INFO.ward_code in (" + syncInfo.Filter + ")";
            if (!string.IsNullOrEmpty(patientId) && !string.IsNullOrEmpty(visitId) && !string.IsNullOrEmpty(orderId) && !string.IsNullOrEmpty(subOrderId))
            {
                sqlSel += " AND ORDERS_EXECUTE.PATIENT_ID='" + patientId + "' AND ORDERS_EXECUTE.VISIT_ID='" + visitId + "' AND ORDERS_EXECUTE.ORDER_NO='" + orderId + "' AND ORDERS_EXECUTE.ORDER_SUB_NO='" + subOrderId + "'";
            }
            DataSet ds = OracleAccess.SelectData(sqlSel, "ORDERS_EXECUTE");
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

        #region 保存拆分结果

        /// <summary>
        /// 保存拆分结果
        /// </summary>
        public void saveSplitResult()
        {
            DataSet ds = dsOrdersExecute.GetChanges();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                OracleAccess.Update(ref ds, "ORDERS_EXECUTE", "select conversion_date_time,  patient_id,  visit_id   ,  order_no    ,  order_sub_no ,  orders_perform_schedule,  repeat_indicator,order_class,order_text ,order_code,dosage,dosage_units,administration,  duration, duration_units,frequency ,freq_counter,freq_interval,freq_interval_unit, freq_detail, perform_schedule,perform_result,conversion_nurse,execute_date_time, execute_nurse,  drug_billing_attr,billing_attr,costs,is_execute,charge,drug_date_time,schedule_perform_time,  print_flag,ward_code,nurse,processing_nurse,memo,update_timestamp,create_timestamp from mobile.orders_execute where ORDER_CLASS = 'A'");
                dsOrdersExecute.AcceptChanges();
            }
        }

        /// <summary>
        /// 保存Orders_m变化
        /// </summary>
        public void saveOrdersResult()
        {
            DataSet changes = this.dsOrders.GetChanges();
            if (changes != null && changes.Tables.Count > 0 && changes.Tables[0].Rows.Count > 0)
            {
                if (!isPharmacologic)
                    OracleAccess.Update(ref changes, "ORDERS_M", "SELECT * FROM ORDERS_M WHERE ORDER_CLASS IN(" + syncInfo.SrcSqlFile + ")");
                else
                    OracleAccess.Update(ref changes, "ORDERS_M", "SELECT * FROM ORDERS_M WHERE ORDER_CLASS='A' ");

                this.dsOrders.AcceptChanges();
            }
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
            // drNew["ORDERS_PERFORM_SCHEDULE"] = DateTime.Parse(DateTime .Now .ToString(ComConst.FMT_DATE.LONG));

            dsErr.Tables[0].Rows.Add(drNew);
        }
        #endregion

        #region 生成一条医嘱执行单

        /// <summary>
        /// 拆分数据来源
        /// </summary>
        public enum SplitSourceType
        {
            /// <summary>
            /// 来源为服务器端拆分程序
            /// </summary>
            SplitSourceTypeServer,
            /// <summary>
            /// 来源为护士端拆分程序
            /// </summary>
            SplitSourceTypeNurseClient
        }

        /// <summary>
        /// 生成一条医嘱执行单
        /// </summary>
        /// <param name="drExecute">新行</param>
        /// <param name="drOrder">遍历Orders_M时的dr</param>
        /// <param name="dtConvert">拆分时间</param>
        /// <param name="schedule">计划执行时间</param>
        private void fillOrderExecute(ref DataRow drExecute, DataRow drOrder, DateTime dtConvert, string schedule, string memo, SplitSourceType splitSource)
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
            drExecute["WARD_CODE"] = drOrder["WARD_CODE"];
            drExecute["NURSE"] = drOrder["NURSE"];
            drExecute["CONVERSION_NURSE"] = nurseName;
            drExecute["CONVERSION_DATE_TIME"] = dtConvert;
            DateTime dtSchedule = DateTime.Parse(schedule);
            drExecute["ORDERS_PERFORM_SCHEDULE"] = dtSchedule.Hour.ToString();
            drExecute["SCHEDULE_PERFORM_TIME"] = dtSchedule;
            if (splitSource == SplitSourceType.SplitSourceTypeNurseClient)
                memo = "[NurceClient]" + memo;
            drExecute["MEMO"] = memo;
            drExecute["CREATE_TIMESTAMP"] = GetSysDate();
            //drExecute["SPLIT_STAUTS"] = drOrder["SPLIT_STAUTS"];
        }
        #endregion

        #region 获取医嘱执行计划
        /// <summary>
        /// 获取医嘱执行计划
        /// </summary>
        /// <param name="performSchedule">医嘱执行计划字符串</param>
        /// <returns>拆分后的ArrayList</returns>
        private ArrayList getOrderPerformSchedule(string performSchedule)
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

        #region 生成计划执行时间
        /// <summary>
        /// 生成计划执行时间
        /// </summary>
        /// <param name="arrSchedule">指定的计划时间</param>
        /// <param name="freqCounter">执行次数/天</param>
        /// <returns></returns>
        private bool GetPerformSchedule(ref ArrayList arrSchedule, int freqCounter)
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

        #region 检查今天是不是正好的隔日
        /// <summary>
        /// 检查今天是不是正好的隔日
        /// </summary>
        /// <returns></returns>
        private bool ChkRightTertian(DateTime dtTreate, DateTime dtOrderStart, string performSchedule, string freqDetail, string orderFilter)
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
        private string GetOrderExecuteConvertedDate(string filter)
        {
            string sql = "SELECT CONVERSION_DATE_TIME FROM ORDERS_EXECUTE WHERE " + filter + " AND ROWNUM = 1";

            if (OracleAccess.SelectValue(sql) == true)
            {
                return OracleAccess.GetResult(0);
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
    }
}
