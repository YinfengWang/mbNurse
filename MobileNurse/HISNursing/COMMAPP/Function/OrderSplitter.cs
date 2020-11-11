using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Collections;
using System.Threading;

using DbType        = HISPlus.SqlManager.DB_TYPE;
using DbFieldType   = HISPlus.SqlManager.FIELD_TYPE;
using SQL           = HISPlus.SqlManager;

namespace HISPlus
{
	/// <summary>
	/// ConvertFunction ��ժҪ˵����
	/// </summary>
	public class OrderSplitter
	{
        private DateTime        dtNow;
        private DataSet         dsOrders        = null;
        private DataSet         dsOrdersExecute = null;
        private string          nurseName       = "SYSTEM";
        
        private DataSet         dsErr           = new DataSet();
        public string           WardCode        = string.Empty;
        private bool            blnExit         = false;
        
        private Mutex           locker          = new Mutex();                          // ������        
        private DataSet         dsOrdersOut     = null;
        private DataSet         dsErrOut        = new DataSet();

        private ArrayList       arrSql_Del      = new ArrayList();

        public DbAccess         OracleAccess    = null;
        
		public OrderSplitter()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			
			// ������
			DataTable dtNew = new DataTable();
			dsErr.Tables.Add(dtNew);
			
			// �����ñ���ֶ�
			dtNew.Columns.Add("PATIENT_ID",     Type.GetType("System.String"));
			dtNew.Columns.Add("VISIT_ID",       Type.GetType("System.String"));
			dtNew.Columns.Add("ORDER_NO",       Type.GetType("System.String"));
			dtNew.Columns.Add("ORDER_SUB_NO",   Type.GetType("System.String"));
			dtNew.Columns.Add("ERR_DESC",       Type.GetType("System.String"));
			
			// ��������
			DataColumn[] dcPrimeKey = new DataColumn[4];
			dcPrimeKey[0] = dtNew.Columns["PATIENT_ID"];
			dcPrimeKey[1] = dtNew.Columns["VISIT_ID"];
			dcPrimeKey[2] = dtNew.Columns["ORDER_NO"];
			dcPrimeKey[3] = dtNew.Columns["ORDER_SUB_NO"];
			
			dtNew.PrimaryKey = dcPrimeKey;
		}
        
        
        /// <summary>
        /// ���ҽ��
        /// </summary>
        /// <param name="wardCode"></param>
        /// <returns></returns>
        public void SplitOrders()
        {
            while(true)
            {
                try
                {
                    dsErr.Tables[0].Rows.Clear();                       // ���淶��ҽ��
                    
                    // ��ȡ��ǰʱ��
                    dtNow = GetSysDate();
                    
                    // ��������ҽ��
                    if (blnExit == true) { return; }
                    dsOrders = GetOrdersToday(WardCode);
                    
                    // ���ҽ����Ժ�(������)ҽ��ִ�е�
                    if (blnExit == true) { return; }
                    dsOrdersExecute = GetOrdersExecuteSplitted(WardCode, dtNow.ToString(ComConst.FMT_DATE.SHORT));
                    
                    // ����ҽ�����(������)
                    arrSql_Del.Clear();

                    for(int i = 1; i < 4; i++)
                    {
                        if (blnExit == true) { return; }
                        OrdersConvertExecute(i);                        
                    }
                    
                    // ���б���
                    if (blnExit == true) { return; }

                    OracleAccess.Connect();

                    try
                    {
                        OracleAccess.BeginTrans();

                        OracleAccess.Update(ref dsOrdersExecute, "ORDERS_EXECUTE", "SELECT * FROM ORDERS_EXECUTE");

                        // ɾ������ҽ��ִ�е�
                        OracleAccess.ExecuteNoQuery(ref arrSql_Del);

                        OracleAccess.Commit();
                    }
                    catch (Exception ex)
                    {
                        OracleAccess.RollBack();
                        LogFile.WriteLog("��̨����̳߳���" + ex.Message);        // �쳣��־
                    }
                    finally
                    {
                        OracleAccess.DisConnect();
                    }

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
                    
                    // ��Ϣ1����
                    for(int i = 0; i < 2 * 60; i++)
                    {
                        if (blnExit == true) { return; }
                        Thread.Sleep(1000);
                    }
                }
                catch(Exception ex)
                {
                    LogFile.WriteLog("��̨����̳߳���" + ex.Message);        // �쳣��־
                    
                    // ��Ϣ1����
                    for(int i = 0; i < 1 * 60; i++)
                    {
                        if (blnExit == true) { return; }
                        Thread.Sleep(1000);
                    }
                }
            }
        }
        
        
        /// <summary>
        /// ������Ҫ��ֵ�ҽ��
        /// </summary>
        /// <param name="wardCode">��������</param>
        /// <returns></returns>
        public DataSet GetOrdersToday(string wardCode)
        {
            string sql = "SELECT ORDERS.* ";
            
            sql += "FROM ";
            sql +=     "ORDERS, ";                                      // ҽ��
            sql +=     "PATS_IN_HOSPITAL ";                             // ��Ժ����
            
            sql += "WHERE ";
            sql +=     " (ORDERS.ORDER_CLASS = 'A' OR ORDERS.ORDER_CLASS = 'B')";
            sql +=     " AND PATS_IN_HOSPITAL.PATIENT_ID = ORDERS.PATIENT_ID ";
            sql +=     " AND PATS_IN_HOSPITAL.VISIT_ID = ORDERS.VISIT_ID ";
            sql +=     " AND PATS_IN_HOSPITAL.WARD_CODE = " + SQL.SqlConvert(wardCode);
            sql +=     " AND (ORDERS.STOP_DATE_TIME IS NULL OR ORDERS.STOP_DATE_TIME > SYSDATE - 3) ";

            sql += " ORDER BY ";
            sql +=      "ORDERS.PATIENT_ID, ORDERS.VISIT_ID, ORDERS.ORDER_NO, ORDERS.ORDER_SUB_NO";
            
            return OracleAccess.SelectData(sql);
        }
        
        
        /// <summary>
        /// ��ȡ�Ѿ�����˵�ҽ��
        /// </summary>
        /// <param name="wardCode">��������</param>
        /// <param name="conversionDate">���ʱ��</param>
        /// <returns></returns>
        public DataSet GetOrdersExecuteSplitted(string wardCode, string conversionDate)
        {
            string sqlSel = "SELECT ORDERS_EXECUTE.* ";
            
            sqlSel += "FROM ";
            sqlSel +=      "ORDERS_EXECUTE, ";                              // ҽ��ִ�е�
            sqlSel +=      "PATS_IN_HOSPITAL ";                             // ��Ժ����
            
            sqlSel += "WHERE ";
            sqlSel +=      "CONVERSION_DATE_TIME >= " + SQL.GetOraDbDate_Short(conversionDate);
            sqlSel +=      " AND PATS_IN_HOSPITAL.PATIENT_ID = ORDERS_EXECUTE.PATIENT_ID ";
            sqlSel +=      " AND PATS_IN_HOSPITAL.VISIT_ID = ORDERS_EXECUTE.VISIT_ID ";
            sqlSel +=      " AND PATS_IN_HOSPITAL.WARD_CODE = " + SQL.SqlConvert(wardCode);
            
            DataSet ds = OracleAccess.SelectData(sqlSel, "ORDERS_EXECUTE");
            
            // ��������
            DataColumn[] dcPrimary = new DataColumn[6];
            dcPrimary[0] = ds.Tables[0].Columns["PATIENT_ID"];
            dcPrimary[1] = ds.Tables[0].Columns["VISIT_ID"];
            dcPrimary[2] = ds.Tables[0].Columns["ORDER_NO"];
            dcPrimary[3] = ds.Tables[0].Columns["ORDER_SUB_NO"];
            dcPrimary[4] = ds.Tables[0].Columns["CONVERSION_DATE_TIME"];
            dcPrimary[5] = ds.Tables[0].Columns["ORDERS_PERFORM_SCHEDULE"];
            
            ds.Tables[0].PrimaryKey = dcPrimary;
            
            return ds;
        }
        
        
		/// <summary>
		/// ���ҽ��������ҽ��ִ�е�
		/// </summary>
        /// <remarks>
        ///  ����: 1. ���ڳ���ҽ��, ���û��ʱ���������в��
        /// </remarks>
		/// <param name="dsOrders">ҽ��</param>
        /// <param name="dsOrdersExecute">ҽ��ִ�е�</param>
		/// <param name="nurseName">��ʿ����</param>
		public void OrdersConvertExecute(int days)
		{   
		    string      patientId       = string.Empty;                                         // ����ID
		    string      visitId         = string.Empty;                                         // ���ξ������
		    string      orderNo         = string.Empty;                                         // ҽ�����
		    string      orderSubNo      = string.Empty;                                         // ��ҽ�����
		    string      orderStatus     = string.Empty;                                         // ҽ��״̬
		    DateTime    dtOrderStart;                                                           // ҽ����ʼʱ��
		    string      orderStopDate   = string.Empty;                                         // ҽ������ʱ��
		    DateTime    dtOrderStop;                                                            // ҽ������ʱ��
		    
		    string      schedule        = string.Empty;                                         // �ƻ�ִ��ʱ��
		    DateTime    dtSchedule;                                                             // �ƻ�ִ��ʱ��
		    
		    DataRow     drExecuteNew    = null;                                                 // ������ҽ��ִ�е�
            string      frequency       = string.Empty;                                         // ҽ��Ƶ������
            bool        blnOnNeedTime   = false;                                                // ҽ��ִ��ʱ��Ϊ��Ҫʱ
            
		    string      filter          = string.Empty;                                         // ��������
		    string      filterOra       = string.Empty;
            string      filterOra_Del   = string.Empty;
		    string      filterTemp      = string.Empty;                                         // ��ʱ��������		    
		    DataRow[]   drFind          = null;
            
            string      time            = string.Empty;                                         // ʱ���ַ���
            
            DateTime    dtTreate        = dtNow.AddDays(days - 1);
            
            string      sqlDel          = "DELETE FROM ORDERS_EXECUTE WHERE (IS_EXECUTE IS NULL OR IS_EXECUTE = '0') AND ";

			foreach(DataRow drOrder in dsOrders.Tables[0].Rows)
			{               
                patientId     = drOrder["PATIENT_ID"].ToString();                               // ����ID��
                visitId       = drOrder["VISIT_ID"].ToString();                                 // ���ξ������
                orderNo       = drOrder["ORDER_NO"].ToString();                                 // ҽ�����
                orderSubNo    = drOrder["ORDER_SUB_NO"].ToString();                             // ��ҽ�����
                orderStatus   = drOrder["ORDER_STATUS"].ToString();                             // ҽ��״̬

                dtOrderStart  = DateTime.Parse(drOrder["START_DATE_TIME"].ToString());          // ҽ����ʼ����
                frequency     = drOrder["FREQUENCY"].ToString().Trim();                         // Ƶ������
                blnOnNeedTime = frequency.Equals("��Ҫʱ");                                     // �Ƿ�Ϊ��Ҫʱ
                orderStopDate = drOrder["STOP_DATE_TIME"].ToString();                           // ҽ��ֹͣ����

                if (orderStopDate.Length > 0)
                {
                    dtOrderStop = DateTime.Parse(orderStopDate);
                }
                else
                {
                    dtOrderStop = dtTreate.AddDays(1);
                }

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
                
                // �������ȷ������ʱҽ�����ǳ���ҽ���Ͳ�������
                string repeator = drOrder["REPEAT_INDICATOR"].ToString().Trim();
                if ("1".Equals(repeator) == false && "0".Equals(repeator) == false)
                {
                    addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "����ȷ���ǳ���ҽ�� ������ʱҽ��!");
                    continue;
                }

                // ---------------------- ��ʱҽ�� ------------------------------------
                if ("1".Equals(repeator) == false)
                {
                    drFind = dsOrdersExecute.Tables[0].Select(filter);

                    // ���״̬��Ϊ��ִ��, ɾ����ǰ���ɵ�ִ�е�
                    if ("2".Equals(orderStatus) == false && (dtTreate.Subtract(dtOrderStart)).Days < 3) 
                    {
                        if (days == 1) { arrSql_Del.Add(sqlDel + filterOra_Del); }

                        continue; 
                    }

                    // ����Ѿ���ֹ�, ������һ��ҽ��
                    if (drFind.Length > 0) { continue; }

                    // ���в��
                    if (dtTreate.Date.CompareTo(dtOrderStart.Date) == 0)
                    {
                        drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                        fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, dtOrderStart.ToString(ComConst.FMT_DATE.LONG));
                        dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                    }

                    continue;
                }

                // ---------------------- ����ҽ�� ------------------------------------
                if ((dtOrderStop.Subtract(dtTreate.AddDays(-3))).Days <= 0)
                {
                    continue;
                }

                // �����ֹͣʱ��, ɾ������ֹͣʱ���ҽ��ִ�е�
                if (orderStopDate.Length > 0 && days == 1)
                {
                    filterTemp = "PATIENT_ID = " + SQL.SqlConvert(patientId)
                        + " AND VISIT_ID = " + SQL.SqlConvert(visitId)
                        + " AND ORDER_NO = " + SQL.SqlConvert(orderNo)
                        + " AND ORDER_SUB_NO = " + SQL.SqlConvert(orderSubNo)
                        + " AND ORDERS_PERFORM_SCHEDULE >= " + SQL.GetOraDbDate(dtOrderStop.ToString(ComConst.FMT_DATE.LONG));

                    arrSql_Del.Add(sqlDel + filterTemp);
                }

                // ���Ҹ�ҽ��ִ�е��Ƿ����
                if (blnOnNeedTime == true)
                {
                    drFind = dsOrdersExecute.Tables[0].Select(filter + " AND (IS_EXECUTE IS NULL OR IS_EXECUTE <> '1')");
                }
                else
                {
                    drFind = dsOrdersExecute.Tables[0].Select(filter);
                }

                // ����Ѿ���ֹ�, ������һ��ҽ��
                if (drFind.Length > 0) { continue; }

                // ���״̬��Ϊ��ִ��, ������һ��ҽ�� (����ҽ����ֹͣ������ֹͣʱ��, �ɲ���״̬�����ж�)
                if ("2".Equals(orderStatus) == false) { continue; }

                // �������
                string freqIntervalUnit = drOrder["FREQ_INTERVAL_UNIT"].ToString().Trim();	// ʱ������λ

                // ִ��Ƶ��Ϊ [��Ҫʱ]
                if (blnOnNeedTime == true)
                {
                    schedule = dtTreate.ToString(ComConst.FMT_DATE.SHORT) + ComConst.STR.BLANK + "23:59:00";

                    drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                    fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, schedule);
                    dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                    continue;
                }

                // û��ʱ����, �����в��
                if (freqIntervalUnit.Length == 0) 
                {
                    addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "��ʱ���� !");
                    continue; 
                }

                // ʱ��������Ϊ[��]
                if (freqIntervalUnit.IndexOf("��") < 0 && freqIntervalUnit.IndexOf("Сʱ") < 0)
                {
                    addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[ҽ��Ƶ��] �ļ����λ������ [��]��[Сʱ] !");
                    continue;
                }

                if (DataType.IsNumber(drOrder["FREQ_INTERVAL"].ToString()) == false)
                {
                    addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[Ƶ�ʼ��] ����Ϊ������!");
                    continue;
                }

                if (DataType.IsNumber(drOrder["FREQ_COUNTER"].ToString()) == false)
                {
                    addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[Ƶ�ʴ���] ����Ϊ������!");

                    continue;
                }

                // ���г���ҽ�����
                int freqInterval = int.Parse(drOrder["FREQ_INTERVAL"].ToString());  // ִ��Ƶ�ʵļ������
                int freqCounter = int.Parse(drOrder["FREQ_COUNTER"].ToString());   // ִ��Ƶ�ʵĴ�������
                string performSchedule = drOrder["PERFORM_SCHEDULE"].ToString();           // ��ʿִ��ʱ��

                ArrayList arrSchedule = getOrderPerformSchedule(performSchedule);

                // ��ȡ�ƻ�ִ��ʱ��
                if (GetPerformSchedule(ref arrSchedule, freqCounter) == false)
                {
                    addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[Ƶ�ʴ���] �� [�ƻ�ִ��ʱ��] ����!");
                    continue;
                }

                // ����ʱ����Ϊ [Сʱ] ��ҽ��
                if (freqIntervalUnit.IndexOf("Сʱ") >= 0)
                {
                    dtSchedule = dtOrderStart;

                    while (dtSchedule.CompareTo(dtTreate.Date) < 0)
                    {
                        dtSchedule.AddHours(freqInterval);
                    }

                    while (dtSchedule.Day == dtTreate.Day)
                    {
                        if (dtOrderStart <= dtSchedule && (orderStopDate.Length == 0 || dtSchedule <= dtOrderStop))
                        {
                            drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                            fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, dtSchedule.ToString(ComConst.FMT_DATE.LONG));
                            dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                        }

                        dtSchedule.AddHours(freqInterval);
                    }

                    continue;
                }

                // ����ʱ���Ϊ [��] ��ҽ��
                // ����һ��һ��
                if (freqInterval > 0 && freqInterval == freqCounter)
                {
                    time = (string)arrSchedule[0];
                    
                    if (DataType.IsTime(ref time) == false)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[�ƻ�ִ��ʱ��] ����!");
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

                // ���� һ��N��
                if (freqInterval == 1)
                {
                    if (freqCounter != arrSchedule.Count)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[Ƶ�ʴ���] �� [�ƻ�ִ��ʱ��] ����!");

                        continue;
                    }

                    for (int i = 0; i < arrSchedule.Count; i++)
                    {
                        time = (string)arrSchedule[i];
                        if (DataType.IsTime(ref time) == false)
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[�ƻ�ִ��ʱ��] ����!");
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

                //  ���� N��һ�� (����)
                if (frequency.IndexOf("����") >= 0)
                {
                    if (freqCounter != 1 || arrSchedule.Count > 1)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "����ҽ�� [Ƶ�ʴ���] �� [�ƻ�ִ��ʱ��] ����!");

                        continue;
                    }

                    // �����ǡ���ĸ���
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

                        // ��ҽ��ִ��ʱ��, ���ȷ������һ�쿪ʼִ��
                        drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                        fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, schedule);
                        dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                    }

                    continue;
                }

                //  ���� N��һ�� (N/��)
                if (frequency.IndexOf("��") >= 0)
                {
                    if (arrSchedule.Count != freqCounter)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "��ҽ�� [Ƶ�ʴ���] �� [�ƻ�ִ��ʱ��] ����!");

                        continue;
                    }

                    int dayOfWeek = (int)dtTreate.DayOfWeek;
                    dayOfWeek = (dayOfWeek == 0 ? 7 : dayOfWeek);

                    for (int i = 0; i < arrSchedule.Count; i++)
                    {
                        if (int.Parse((string)arrSchedule[i]) == dayOfWeek)
                        {
                            schedule = dtTreate.ToString(ComConst.FMT_DATE.SHORT) + ComConst.STR.BLANK + "09:00:00";
                            drExecuteNew = dsOrdersExecute.Tables[0].NewRow();
                            fillOrderExecute(ref drExecuteNew, drOrder, dtTreate, schedule);
                            dsOrdersExecute.Tables[0].Rows.Add(drExecuteNew);
                        }
                    }

                    continue;
                }

                // ���ܴ�������, ����
                addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "��ҽ��������������!");
			}
		}
        
		
		/// <summary>
		/// ����һ��ҽ��ִ�е�
		/// </summary>
		/// <param name="drExecute">ҽ��ִ�е�</param>
		/// <param name="drOrder">ҽ��</param>
		/// <param name="nurseName">��ʿ����</param>
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
            drExecute["DRUG_BILLING_ATTR"] = drOrder["DRUG_BILLING_ATTR"];
            drExecute["BILLING_ATTR"] = drOrder["BILLING_ATTR"];
            drExecute["CONVERSION_NURSE"] = nurseName;
            drExecute["CONVERSION_DATE_TIME"] = dtConvert;
            drExecute["ORDERS_PERFORM_SCHEDULE"] = schedule;
		}
        
        
		/// <summary>
		/// ��ȡҽ��ִ�мƻ�
		/// </summary>
		/// <param name="performSchedule">ҽ��ִ�мƻ��ַ���</param>
		/// <returns>��ֺ��ArrayList</returns>
		public ArrayList getOrderPerformSchedule(string performSchedule)
		{
		    ArrayList arrSchedule = new ArrayList();
		    
            // Ԥ����: ����ո�
            performSchedule = performSchedule.Replace(ComConst.STR.BLANK, string.Empty);

            // Ԥ����: "/" "\" -> "-"
            performSchedule = performSchedule.Replace("/", "-");
            performSchedule = performSchedule.Replace(@"\", "-");

		    // ���Ϊ���ַ���, ֱ���˳�
			if (performSchedule.Trim().Length == 0)
			{
			    return arrSchedule;
			}
            
            // ���÷ָ��ַ���
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
        /// �������ǲ������õĸ���
        /// </summary>
        /// <returns></returns>
        public bool ChkRightTertian(DateTime dtTreate, DateTime dtOrderStart, string performSchedule, string freqDetail, string orderFilter)
        {
		    // ȷ����ǰ�Ƿ����ɹ�ִ�е�
		    string convertedDate = GetOrderExecuteConvertedDate(orderFilter);						    
		    
		    // �����ǰ���ɹ�ִ�е�
		    if (convertedDate.Length > 0)
		    {
		        DateTime dtConverted = DateTime.Parse(convertedDate);
		        dtConverted = DateTime.Parse(dtConverted.ToString(ComConst.FMT_DATE.SHORT));
		        
		        TimeSpan tSpan = dtTreate.Subtract(dtConverted);
		        return (tSpan.Days % 2 == 0);
		    }
            
		    // �����ǰû�����ɹ�ִ�е�
            DateTime dtSchedule = DateTime.Parse(dtTreate.ToString(ComConst.FMT_DATE.SHORT) + ComConst.STR.BLANK + performSchedule);
            
	        if (freqDetail.Length > 0)
	        {
	            int pos = freqDetail.LastIndexOf("��");
	            
	            if (pos > 0)
	            {
	                if ((("��".Equals(freqDetail.Substring(pos - 1, 1))) && (dtTreate.Day % 2 == 0))
	                 || (("˫".Equals(freqDetail.Substring(pos - 1, 1))) && (dtTreate.Day % 2 == 1)))
                    {
                        return false;
                    }
	                
	                if ((("��".Equals(freqDetail.Substring(pos - 1, 1))) && (dtTreate.Day % 2 == 1))
	                    || ("˫".Equals(freqDetail.Substring(pos - 1, 1))) && (dtTreate.Day % 2 == 0))
	                {
                        // �����ʼ����С�ڽ���
                        if (dtOrderStart.ToString(ComConst.FMT_DATE.SHORT) .CompareTo(dtTreate.ToString(ComConst.FMT_DATE.SHORT)) < 0)
                        {
                            return true;
                        }
	                }
	            }
	        }
            
            // �����ȷ����˫��
            return (dtSchedule.CompareTo(dtOrderStart) > 0);
        }
        
        
        /// <summary>
        /// ��ȡĳһҽ��ִ�е�����һת������
        /// </summary>
        /// <returns></returns>
        public string GetOrderExecuteConvertedDate(string filter)
        {
            string sql = "SELECT CONVERSION_DATE_TIME FROM ORDERS_EXECUTE WHERE " + filter;
                        
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
        

        /// <summary>
        /// ���ɼƻ�ִ��ʱ��
        /// </summary>
        /// <param name="arrSchedule">ָ���ļƻ�ʱ��</param>
        /// <param name="freqCounter">ִ�д���/��</param>
        /// <returns></returns>
        public bool GetPerformSchedule(ref ArrayList arrSchedule, int freqCounter)
        {
            // Ԥ����
            if (arrSchedule == null)            
            {
                arrSchedule = new ArrayList();
            }

            // ���û��ָ���ƻ�ִ��ʱ��, Ĭ�ϴ�9�㿪ʼִ��
            if (arrSchedule.Count == 0)
            {
                arrSchedule.Add("9:00");
            }

            // ����ƻ�ʱ��ȫ��ָ��, ֱ���˳�;
            if (arrSchedule.Count == freqCounter)
            {
                return true;
            }

            // ����ƻ�ʱ���ָ����ʼʱ��
            if (arrSchedule.Count == 1 && freqCounter > 1)
            {
                // ��ȡʱ����
                int interval = ComConst.VAL.HOURS_PER_DAY / freqCounter;

                // ��ȡ��ʼָ����ʱ��
                string tmStart = (string)arrSchedule[0];
                if (DataType.IsTime(ref tmStart) == false)
                {
                    return false;
                }

                DateTime dtStart = DateTime.Parse(DateTime.Now.ToString(ComConst.FMT_DATE.SHORT) + ComConst.STR.BLANK + tmStart);

                // ��������ļƻ�ִ��ʱ��
                for (int i = 1; i < freqCounter; i++)
                {
                    dtStart = dtStart.AddHours(interval);

                    arrSchedule.Add(dtStart.ToString(ComConst.FMT_DATE.TIME_SHORT));
                }

                return true;
            }
            
            // �������
            return false;
        }

        
        /// <summary>
        /// ���治�淶��ҽ��
        /// </summary>
        /// <param name="days">ֻ��days == 1��ִ��</param>
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
		/// ��ȡϵͳ����
		/// </summary>
		/// <returns>ϵͳ��ǰ����</returns>
		public DateTime GetSysDate()
		{
            if (OracleAccess.SelectValue("SELECT SYSDATE FROM DUAL") == true)
            {
                return DateTime.Parse(OracleAccess.GetResult(0));
            }
            else
            {
                throw new Exception("��ȡϵͳ���ڳ���!");
            }
		}		
		
		
		public void Exit()
		{
		    blnExit = true;
		}
		
		
		/// <summary>
		/// ��ȡ�����е�����
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
		/// �Ƿ��д�
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
	}
}
