//  HS  2012 �� ����·���������׳��� ֮  ҩ��ҽ�����   ֻ�� ��  ҽ�� ���� ΪA  ��ġ�

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
    /// ConvertFunction ��ժҪ˵����
    /// </summary>
    public class OrderSplitter
    {
        #region ����
        private int instanceCount = 0;                            // ʵ����
        private DateTime dtNow;
        private DataSet dsOrders = null;
        private DataSet dsOrdersExecute = null;
        private DataSet dsOrdersExecuteStruct = null;
        private string nurseName = "SYSTEM";
        private DataSet dsErr = new DataSet();
        private string wardCode = string.Empty;
        private bool blnExit = false;
        private Mutex locker = new Mutex();                          // ������        
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

        #region ����
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
        /// �����������
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

        #region ��������
        public OrderSplitter()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //

            // ������
            DataTable dtNew = new DataTable();
            dsErr.Tables.Add(dtNew);

            // �����ñ���ֶ�
            dtNew.Columns.Add("PATIENT_ID", Type.GetType("System.String"));
            dtNew.Columns.Add("VISIT_ID", Type.GetType("System.String"));
            dtNew.Columns.Add("ORDER_NO", Type.GetType("System.String"));
            dtNew.Columns.Add("ORDER_SUB_NO", Type.GetType("System.String"));
            dtNew.Columns.Add("ERR_DESC", Type.GetType("System.String"));
            dtNew.Columns.Add("ORDER_CODE", Type.GetType("System.String"));

            // ��������
            DataColumn[] dcPrimeKey = new DataColumn[4];
            dcPrimeKey[0] = dtNew.Columns["PATIENT_ID"];
            dcPrimeKey[1] = dtNew.Columns["VISIT_ID"];
            dcPrimeKey[2] = dtNew.Columns["ORDER_NO"];
            dcPrimeKey[3] = dtNew.Columns["ORDER_SUB_NO"];
            // dcPrimeKey[4] = dtNew.Columns["ORDER_CODE"];

            dtNew.PrimaryKey = dcPrimeKey;

            // ������ֵ
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

        #region ����ͬ����Ϣ
        /// <summary>
        /// ����ͬ����Ϣ
        /// </summary>
        /// <returns></returns>
        public bool loadSyncInfo()
        {
            syncInfos.Clear();

            // �ж��ļ��Ƿ����
            string dllName = this.GetType().Module.Name.Replace(".dll", "");
            string strNum = dllName.Substring(dllName.Length - 1);
            string fileName = Path.Combine(Path.Combine(Application.StartupPath, "SQL"), "List" + strNum + ".INI");
            if (File.Exists(fileName) == false)
            {
                throw new Exception(fileName + "������!");
            }

            // ��ȡ�ļ�����
            StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("gb2312"));
            string content = sr.ReadToEnd();
            sr.Close();

            content = content.Replace("\n", ComConst.STR.BLANK);

            // �����ļ�
            string[] parts = content.Split('\r');
            string line = string.Empty;
            SyncInfo syncItem = null;
            for (int i = 0; i < parts.Length; i++)
            {
                line = parts[i].Trim();

                // ע��
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

        #region ��ͨ����

        #region ���ҽ��
        /// <summary>
        /// ���ҽ��
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
                    //LB20110712���,����ͬ����Ϣ
                    loadSyncInfo();
                    //LB20110712��ӽ���

                    dsErr.Tables[0].Rows.Clear();

                    // �����ַ���
                    setHisNlsLang();


                    // ��ȡ��ǰʱ��
                    dtNow = GetSysDate();
                    TraceInfo = "��ȡϵͳ����: " + dtNow.ToString(ComConst.FMT_DATE.LONG);

                    // ��������ҽ��
                    if (blnExit == true) { return; }
                    dsOrders = GetOrdersToday(wardCode);
                    TraceInfo = "��ȡ��������ҽ�� ��:" + dsOrders.Tables[0].Rows.Count.ToString() + "��";

                    // ���ҽ����Ժ�(������)ҽ��ִ�е�

                    if (blnExit == true) { return; }
                    dsOrdersExecute = GetOrdersExecuteSplitted(wardCode, dtNow.ToString(ComConst.FMT_DATE.SHORT));
                    TraceInfo = "��ȡ�Ѳ��ҽ��ִ�е� ��:" + dsOrdersExecute.Tables[0].Rows.Count.ToString() + "��";

                    // ����ҽ�����(������)
                    //
                    int splitCount = Convert.ToInt32(GVars.IniFile.ReadString("APP", "SPLIT_DATES", "2"));
                    for (int i = 1; i <= splitCount; i++)
                    {
                        // ���в��
                        TraceInfo = "��ʼ����ҽ�����ݲ�ֳ��� �� " + i.ToString() + "�����ݲ��...";
                        dtBegin = DateTime.Now;

                        if (blnExit == true) { return; }
                        OrdersConvertExecute(i);

                        // ���б���
                        if (blnExit == true) { return; }
                        saveSplitResult();

                        // ������ʧ�ܵ�ԭ��
                        if (i == 1)
                        {
                            saveSplitError();
                        }

                        // ȷ��һ�������Ĳ��ѭ����ʱ���� 1����(60��)����
                        while (DateTime.Now.Subtract(dtBegin).TotalSeconds <= 20)
                        {
                            if (blnExit == true) { return; }
                            Thread.Sleep(100);
                        }
                    }

                    // ɾ������ҽ��(����orders_m�Ѿ����Ϊorders_execute�ļ�¼�����ν����������ݺ�orders_m�Խӵ�ʱ��,�����в����¿��ĺ�ִ�е�ҽ������orders_m����ɾ�������ݽ��뱸�ݱ�orders_m_tombstone��)
                    //��ϸ�����������˵��
                    if (counter++ % 10 == 1)
                    {
                        DeleteOrdersExecuteInvalid();
                    }

                    //���ȫԺ��ʱ��ORDERS_EXECUTE_TOMBSTONE���������ܴܺ󣬱���ɾ������45�����ǰ������
                    if (counter % 30 == 1)
                    {
                        //����ɾ����Ϣ
                        loadDeleteInfos();
                        //ִ��ɾ�����
                        for (int i = 0; i < deleteInfos.Count; i++)
                        {
                            DeleteInfo delinfo = deleteInfos[i];
                            oracleAccess.ExecuteNoQuery(delinfo.Sql);
                            TraceInfo = DateTime.Now.ToString(ComConst.FMT_DATE.LONG) + delinfo.Msg;
                            Thread.Sleep(1000);
                        }
                    }

                    TraceInfo = " HS    -- һ��ѭ������ ���� ��һ��ѭ�� ����!";

                    // ��Ϣ10��
                    for (int j = 0; j < 10 * 10; j++)
                    {
                        if (blnExit == true) { return; }
                        Thread.Sleep(100);
                    }
                }
                catch (Exception ex)
                {
                    TraceInfo = "��̨����̳߳���" + ex.Message;        // �쳣��־                    
                }
            }
        }
        #endregion

        #region ������Ҫɾ������Ϣ
        /// <summary>
        /// ������Ҫɾ������Ϣ
        /// </summary>
        /// <returns></returns>
        public bool loadDeleteInfos()
        {
            deleteInfos.Clear();
            // �ж��ļ��Ƿ����
            string fileName = Path.Combine(Path.Combine(Application.StartupPath, "SQL"), "DeleteInfo.sql");

            if (File.Exists(fileName) == false)
            {
                throw new Exception(fileName + "������!");
            }

            // ��ȡ�ļ�����
            StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("gb2312"));
            string content = sr.ReadToEnd();
            sr.Close();

            content = content.Replace("\n", ComConst.STR.BLANK);

            // �����ļ�
            string[] parts = content.Split('\r');
            string line = string.Empty;
            DeleteInfo delItem = null;
            for (int i = 0; i < parts.Length; i++)
            {
                line = parts[i].Trim();

                // ע��
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

        #region ������Ҫ��ֵ�ҽ��
        /// <summary>
        /// ������Ҫ��ֵ�ҽ��
        /// </summary>
        /// <param name="wardCode">��������</param>
        /// <returns></returns>
        public DataSet GetOrdersToday(string wardCode)
        {
            string sql = "SELECT * FROM ORDERS_M WHERE ORDER_CLASS = 'A' ";
            SyncInfo syncInfo = syncInfos[0];
            sql += " and ward_code in (" + syncInfo.Filter + ")";
            return oracleAccess.SelectData(sql);
        }
        #endregion

        #region ��ȡ�Ѿ�����˵�ҽ��
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
            sqlSel += "ORDERS_EXECUTE, ";                              // ҽ��ִ�е�
            sqlSel += "PATIENT_INFO ";                             // ��Ժ����

            sqlSel += "WHERE ";
            sqlSel += "ORDERS_EXECUTE.CONVERSION_DATE_TIME >= " + SQL.GetOraDbDate_Short(conversionDate);
            sqlSel += " AND PATIENT_INFO.PATIENT_ID = ORDERS_EXECUTE.PATIENT_ID ";
            sqlSel += " AND PATIENT_INFO.VISIT_ID = ORDERS_EXECUTE.VISIT_ID  and  ORDER_CLASS = 'A'  ";
            SyncInfo syncInfo = syncInfos[0];
            sqlSel += " and PATIENT_INFO.ward_code in (" + syncInfo.Filter + ")";
            DataSet ds = oracleAccess.SelectData(sqlSel, "ORDERS_EXECUTE");
            // ��������
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

        #region  ���ҽ��������ҽ��ִ�е�
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
            #region �м��������
            string patientId = string.Empty;                                         // ����ID
            string visitId = string.Empty;                                         // ���ξ������
            string orderNo = string.Empty;                                         // ҽ�����
            string orderSubNo = string.Empty;                                         // ��ҽ�����
            string orderStatus = string.Empty;                                         // ҽ��״̬
            DateTime dtOrderStart;                                                           // ҽ����ʼʱ��
            string orderStopDate = string.Empty;                                         // ҽ������ʱ��
            DateTime dtOrderStop;                                                            // ҽ������ʱ��

            string schedule = string.Empty;                                         // �ƻ�ִ��ʱ��
            DateTime dtSchedule;                                                             // �ƻ�ִ��ʱ��

            DataRow drExecuteNew = null;                                                 // ������ҽ��ִ�е�
            string frequency = string.Empty;                                         // ҽ��Ƶ������
            bool blnOnNeedTime = false;                                                // ҽ��ִ��ʱ��Ϊ��Ҫʱ

            string filter = string.Empty;                                         // ��������
            string filterOra = string.Empty;
            string filterTemp = string.Empty;                                         // ��ʱ��������		    
            DataRow[] drFind = null;

            string time = string.Empty;                                         // ʱ���ַ���

            DateTime dtTreate = dtNow.AddDays(days - 1);
            #endregion

            int counter = 0;

            DataRow[] drOrders = dsOrders.Tables[0].Select("", "PATIENT_ID, VISIT_ID, ORDER_NO, ORDER_SUB_NO");

            for (int c = 0; c < drOrders.Length; c++)
            {
                DataRow drOrder = drOrders[c];

                counter++;
                if (counter % 1000 == 500) TraceInfo = "�Ѳ�� " + counter + "��";

                if (blnExit) return;

                try
                {
                    #region ��ȡ��ǰҽ������Ϣ
                    patientId = drOrder["PATIENT_ID"].ToString();                               // ����ID��
                    visitId = drOrder["VISIT_ID"].ToString();                                 // ���ξ������
                    orderNo = drOrder["ORDER_NO"].ToString();                                 // ҽ�����
                    orderSubNo = drOrder["ORDER_SUB_NO"].ToString();                             // ��ҽ�����
                    orderStatus = drOrder["ORDER_STATUS"].ToString();                             // ҽ��״̬

                    dtOrderStart = DateTime.Parse(drOrder["START_DATE_TIME"].ToString());          // ҽ����ʼ����
                    frequency = drOrder["FREQUENCY"].ToString().Trim();                         // Ƶ������
                    blnOnNeedTime = frequency.Equals("��Ҫʱ");                                     // �Ƿ�Ϊ��Ҫʱ
                    orderStopDate = drOrder["STOP_DATE_TIME"].ToString();                           // ҽ��ֹͣ����

                    string freqIntervalUnit = drOrder["FREQ_INTERVAL_UNIT"].ToString().Trim();   	// ʱ������λ

                    if (orderStopDate.Length > 0)
                    {
                        dtOrderStop = DateTime.Parse(orderStopDate);
                    }
                    else
                    {
                        dtOrderStop = dtTreate.AddDays(1);
                    }
                    #endregion

                    #region ��������Ԥ��
                    filter = "PATIENT_ID = " + SQL.SqlConvert(patientId)
                            + " AND VISIT_ID = " + SQL.SqlConvert(visitId)
                            + " AND ORDER_NO = " + SQL.SqlConvert(orderNo)
                            + " AND ORDER_SUB_NO = " + SQL.SqlConvert(orderSubNo);

                    filterOra = filter;

                    filter += " AND (CONVERSION_DATE_TIME >= " + SQL.SqlConvert(dtTreate.ToString(ComConst.FMT_DATE.SHORT))
                            + " AND  CONVERSION_DATE_TIME < " + SQL.SqlConvert(dtTreate.AddDays(1).ToString(ComConst.FMT_DATE.SHORT)) + ")";
                    #endregion

                    #region ���ҽ��״̬��Ϊ��ִ�� (2), ɾ��δִ��ҽ��ִ�е�
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

                    #region ҽ�����ͼ��
                    // �������ȷ������ʱҽ�����ǳ���ҽ���Ͳ�������

                    string repeator = drOrder["REPEAT_INDICATOR"].ToString().Trim();
                    if ("1".Equals(repeator) == false && "0".Equals(repeator) == false)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "����ȷ���ǳ���ҽ�� ������ʱҽ��!");
                        continue;
                    }
                    else if ("1".Equals(repeator) == true && frequency.Length == 0)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "����ҽ����Ƶ�β���Ϊ��!");
                        continue;
                    }

                    #endregion

                    #region ��ʱҽ���۷�
                    if ("1".Equals(repeator) == false)
                    {
                        drFind = dsOrdersExecute.Tables[0].Select(filter);

                        // ����Ѿ���ֹ�, ������һ��ҽ��
                        if (drFind.Length > 0) { continue; }

                        // ���в��
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

                    #region �����ֹͣʱ��, ɾ������ֹͣʱ���ҽ��ִ�е�
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

                    #region ���������ǰֹͣ��ҽ��
                    if ((dtOrderStop.Subtract(dtTreate.AddDays(-3))).Days <= 0)
                    {
                        continue;
                    }
                    #endregion

                    #region �������
                    // ��������ҽ��ִ�е�����, �����в��
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

                    // ҽ��״̬������2����6������ʵ��û�е�ʱ����ǰ����ҽ������Ч
                    //// ���״̬��Ϊ��ִ��, ������һ��ҽ��  ��ֹͣʱ��  ������������߸� ��������
                    //if ("26".IndexOf(orderStatus) < 0) { continue; }
                    #endregion

                    #region ִ��Ƶ��Ϊ [��Ҫʱ]
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

                    #region �������

                    // ���  Ƶ��Ϊ�� ��������ҽ������ �������
                    if (frequency.Length == 0)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "Ƶ��Ϊ�� !");
                        continue;
                    }
                    // û��ʱ����, �����в��
                    if (freqIntervalUnit.Length == 0)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "��ʱ���� !");
                        continue;
                    }

                    // ʱ��������Ϊ[��],[Сʱ],[��]
                    if (freqIntervalUnit.IndexOf("��") < 0
                        && freqIntervalUnit.IndexOf("Сʱ") < 0
                        && freqIntervalUnit.IndexOf("��") < 0)
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
                    #endregion

                    #region ��ȡִ�мƻ�

                    int freqInterval = int.Parse(drOrder["FREQ_INTERVAL"].ToString());  // ִ��Ƶ�ʵļ������
                    int freqCounter = int.Parse(drOrder["FREQ_COUNTER"].ToString());   // ִ��Ƶ�ʵĴ�������
                    string performSchedule = drOrder["PERFORM_SCHEDULE"].ToString();           // ��ʿִ��ʱ��

                    ArrayList arrSchedule = getOrderPerformSchedule(performSchedule);

                    // �ƻ�ִ��ʱ�䲻��Ϊ��

                    if (freqCounter >= 1 && performSchedule.Length == 0)
                    {
                        addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "�ƻ�ִ��ʱ�䲻��Ϊ�գ�");
                        continue;
                    }


                    // ��ȡ�ƻ�ִ��ʱ��, ���ʱ������Ϊ[Сʱ]
                    if (freqIntervalUnit.Equals("��"))
                    {
                        if (GetPerformSchedule(ref arrSchedule, freqCounter) == false)
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[Ƶ�ʴ���] �� [�ƻ�ִ��ʱ��] ����!");
                            continue;
                        }
                    }

                    if (freqIntervalUnit.Equals("Сʱ"))
                    {
                        if (ComConst.VAL.HOURS_PER_DAY / freqInterval != arrSchedule.Count)
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[Ƶ�ʴ���] �� [�ƻ�ִ��ʱ��] ����!");
                            continue;
                        }
                    }
                    #endregion

                    #region ����ʱ����Ϊ [1/XСʱ] ��ҽ��
                    if (freqIntervalUnit.IndexOf("Сʱ") >= 0)
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

                    #region ����һ��һ��
                    if (freqIntervalUnit.IndexOf("��") >= 0 && freqInterval > 0 && freqInterval == freqCounter)
                    {
                        time = (string)arrSchedule[0];

                        if (DataType.IsTime(ref time) == false)
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "[�ƻ�ִ��ʱ��] ����!");
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

                    #region ���� һ��N��
                    if (freqIntervalUnit.IndexOf("��") >= 0 && freqInterval == 1)
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

                    #region  ���� N��һ�� (����)
                    if (frequency.IndexOf("Qod") >= 0 || frequency.IndexOf("����") >= 0)
                    {
                        if (freqCounter != 1 || arrSchedule.Count > 1)
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "����ҽ�� [Ƶ�ʴ���] �� [�ƻ�ִ��ʱ��] ����!");

                            continue;
                        }

                        if (!performSchedule.Contains(":"))
                        {
                            addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "������Ч��ʱ���ʽ");

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

                            // ȷ��ʱ�䷶Χ
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

                    #region  ���� N��һ�� (N/��)
                    if (freqIntervalUnit.IndexOf("��") >= 0)
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

                    // ���ܴ�������, ����
                    addErrOrder(days, patientId, visitId, orderNo, orderSubNo, "��ҽ��������������!");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region ɾ�����ϵ�ҽ��
        /// <summary>
        /// ɾ�����ϵ�ҽ����orders_m��ļ�¼ɾ�����Ժ����orders_m_tombstone,��orders_m_tombstone�����м�¼ʱ˵����
        /// ������ҽ���Ѿ��ı��ˣ��ڽӿڶԽӵ�ʱ����ԭ��orders_m�����е������е��Ѿ�������Ч״̬�ˣ�����orders_m��ɾ����
        /// ������ORDERS_M_DELETE������ת�ƽ�orders_m_tombstone��
        /// �����Ǽ��orders_m_tombstone�����ݱ��еļ�¼�Ƿ��Ѿ�������ORDERS_EXECUTE������Ѿ������ˣ�ɾ����
        /// </summary>
        public void DeleteOrdersExecuteInvalid()
        {
            string sql = string.Empty;
            sql += "delete from ORDERS_EXECUTE E where (E.CONVERSION_DATE_TIME,E.Patient_Id,E.Visit_Id,E.Order_No,E.Order_Sub_No,E.ORDERS_PERFORM_SCHEDULE) in ";
            sql += "(SELECT E.CONVERSION_DATE_TIME,E.Patient_Id,E.Visit_Id,E.Order_No,E.Order_Sub_No,E.ORDERS_PERFORM_SCHEDULE ";
            sql += "FROM ORDERS_EXECUTE E,orders_m_tombstone O ";
            sql += "WHERE E.CONVERSION_DATE_TIME > TO_DATE(SYSDATE - 3) ";
            //LB20110703�߷��꣬���ԤԼ��Ժ
            sql += "AND E.SCHEDULE_PERFORM_TIME>O.stop_date_time ";
            //LB20110703�߷����޸Ľ���
            //sql += "AND O.START_DATE_TIME > TO_DATE(SYSDATE - 3) ";
            sql += "AND (E.IS_EXECUTE IS NULL OR E.IS_EXECUTE = '0') ";
            sql += "AND E.PATIENT_ID = O.PATIENT_ID AND E.VISIT_ID = O.VISIT_ID ";
            sql += "AND E.ORDER_NO = O.ORDER_NO AND E.ORDER_SUB_NO = O.ORDER_SUB_NO)";
            oracleAccess.ExecuteNoQuery(sql);
            TraceInfo = "ɾ������ҽ��ִ�е������ɹ�!";
        }

        public void DeleteOrdersExeTombstone(int days)
        {
            string sql = string.Empty;
            sql += "delete from ORDERS_EXECUTE_TOMBSTONE E where E.CREATE_TIMESTAMP<TO_DATE(SYSDATE-" + days.ToString() + ")";
            oracleAccess.ExecuteNoQuery(sql);
            TraceInfo = "ɾ��ҽ��ִ�е����ݱ�������ݲ����ɹ�!";
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

            //��ʾ��Ϣ
            TraceInfo = "����Ѳ��,��������ʾ��ִ���ҽ�� " + count.ToString() + " ��!";

            if (count > 0)
            {
                RowCountChanged = count;
                TraceInfo = "�������: " + dtBegin.ToString(ComConst.FMT_DATE.LONG) + " ��: " + rowCount.ToString() + "��";
            }
        }

        #endregion

        #region ����һ��ҽ��ִ�е�
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
            drExecute["CONVERSION_NURSE"] = nurseName;
            drExecute["CONVERSION_DATE_TIME"] = dtConvert;
            DateTime dtSchedule = DateTime.Parse(schedule);
            drExecute["ORDERS_PERFORM_SCHEDULE"] = dtSchedule.Hour.ToString();
            drExecute["SCHEDULE_PERFORM_TIME"] = dtSchedule;
        }
        #endregion

        #region ��ȡҽ��ִ�мƻ�
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
            performSchedule = performSchedule.Replace(@";", "-");

            // ���Ϊ���ַ���, ֱ���˳�
            if (performSchedule.Trim().Length == 0)
            {
                return arrSchedule;
            }

            // ���÷ָ��ַ���
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

        #region �������ǲ������õĸ���
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
                        if (dtOrderStart.ToString(ComConst.FMT_DATE.SHORT).CompareTo(dtTreate.ToString(ComConst.FMT_DATE.SHORT)) < 0)
                        {
                            return true;
                        }
                    }
                }
            }

            // �����ȷ����˫��
            return (dtSchedule.CompareTo(dtOrderStart) > 0);
        }
        #endregion

        #region ��ȡĳһҽ��ִ�е�����һת������
        /// <summary>
        /// ��ȡĳһҽ��ִ�е�����һת������
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

        #region ���ɼƻ�ִ��ʱ��
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
        #endregion

        #region ���治�淶��ҽ��
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

        #region ��ȡϵͳ����
        /// <summary>
        /// ��ȡϵͳ����
        /// </summary>
        /// <returns>ϵͳ��ǰ����</returns>
        public DateTime GetSysDate()
        {
            if (oracleAccess.SelectValue("SELECT SYSDATE FROM DUAL") == true)
            {
                return DateTime.Parse(oracleAccess.GetResult(0));
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
        #endregion

        #region ��ȡ����ֶ��б�(ʱ������ͳ���
        /// <summary>
        /// ��ȡ����ֶ��б�(ʱ������ͳ���)
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

            // ��ȡ�б�
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

        #region �����ֽ��
        /// <summary>
        /// �����ֽ��
        /// </summary>
        /// <returns></returns>
        private bool saveSplitResult()
        {
            try
            {
                TraceInfo = "��ʼ׼�������ֵ�ҽ��ִ�е�...";

                DataSet ds = dsOrdersExecute.GetChanges();
                if (ds != null && ds.Tables.Count > 0)
                {
                    TraceInfo = "һ��Ҫ �����ֵ�ҽ��ִ�е�" + ds.Tables[0].Rows.Count.ToString() + "��  ���!";

                    RowCountChanged = ds.Tables[0].Rows.Count;
                    TraceInfo = "�������: " + dtBegin.ToString(ComConst.FMT_DATE.LONG) + " ��: " + rowCount.ToString() + "��";

                    oracleAccess.Update(ref ds, "ORDERS_EXECUTE", "SELECT * FROM ORDERS_EXECUTE  where ORDER_CLASS = 'A' ");
                    dsOrdersExecute.AcceptChanges();
                }

                TraceInfo = "�����ֵ�ҽ��ִ�е�   �ܱ仯   ���!";

                return true;
            }
            catch (Exception ex)
            {
                TraceInfo = "��̨����̳߳���" + ex.Message;    // �쳣��־
                return false;
            }
        }
        #endregion

        #region ������ʧ��ԭ��
        /// <summary>
        /// ������ʧ��ԭ��
        /// </summary>
        private void saveSplitError()
        {
            if (dsErr == null || dsErr.Tables.Count == 0 || dsErr.Tables[0].Rows.Count == 0)
            {
                return;
            }

            // ��ѯԭʼ����
            string sql = "SELECT * FROM ORDERS_M ";
            if (wardCode.Length > 0)
            {
                sql += " WHERE WARD_CODE = " + SQL.SqlConvert(wardCode);
            }

            DataSet dsSrc = oracleAccess.SelectData(sql, "ORDERS_M");
            dsSrc.AcceptChanges();


            // ��������
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

            // ������״̬
            foreach (DataRow dr in dsSrc.Tables[0].Rows)
            {
                if (dr["SPLIT_MEMO"].ToString().Trim().Length == 0) dr.AcceptChanges();
            }
            //orders_m����ɹ���ֵļ�¼�ᱻɾ�������´κ�Դ���ݽ��бȽϵ�ʱ����û�о��ֹ����ˣ�����orders_m_tombstore�������ݷǳ���
            //Ӧ�øĳ�ֹͣ�˵�ҽ����ɾ���������һֱ��ɾ���������������Ϣ�ǳ���--(����ע��)
            //// ɾ������δ����ļ�¼(ԭ����ע��)
            //drFind = dsSrc.Tables[0].Select(string.Empty, string.Empty, DataViewRowState.Unchanged);
            //for(int i = drFind.Length - 1; i >= 0; i--)
            //{
            //    drFind[i].Delete();
            //}

            oracleAccess.Update(ref dsSrc);

            RowCountChanged = dsSrc.Tables[0].Select("", "", DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedCurrent).Length;

            TraceInfo = "�������: " + dtBegin.ToString(ComConst.FMT_DATE.LONG) + " ��: " + rowCount.ToString() + "��";
        }

        #endregion
        #endregion

        #region Oracle�ַ�������
        private string oldNlslang = string.Empty;


        /// <summary>
        /// �����ַ���
        /// </summary>
        private void setHisNlsLang()
        {
            // ��ͬ�ַ����Ĵ���
            string nlsLangKey = GVars.IniFile.ReadString("DATABASE", "ORA_NLS_LANG", string.Empty);

            if (nlsLangKey.Length > 0)
            {

                for (int i = 0; i < 6; i++)                 // �������ҽԺ Ҫ�����ִ�ӲŻ�ɹ���������
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

                        Thread.Sleep(30 * 1000);        // ��Ϣ30��
                    }
                    finally
                    {
                        oracleNlsLang_Restore(nlsLangKey, oldNlslang);
                    }
                }

                throw new Exception("�����ַ���ʧ��!");
            }
        }


        /// <summary>
        /// �޸�Oracle�ַ���ΪӢ��
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
        /// �޸�Oracle�ַ���Ϊ����
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
        /// ��ԭOracle�ַ���
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
