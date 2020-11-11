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
using System.Diagnostics;
using System.Configuration;
using OrderSplitHelper;
using System.Linq;
namespace HISPlus
{

    /// <summary>
    /// ConvertFunction ��ժҪ˵����
    /// </summary>
    public class OrderSplitter
    {
        #region ����
        // private int             instanceCount   = 0;                            // ʵ����
        private DateTime dtNow;
        private DataSet dsOrders = null;
        private DataSet dsOrdersExecute = null;
        private DataSet dsOrdersExecuteStruct = null;
        private DataSet dsErr = new DataSet();
        private string wardCode = string.Empty;
        private bool blnExit = false;
        private Mutex locker = new Mutex();                          // ������        
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
        /// �����ļ�·��
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

        #region ����
        public string TraceInfo
        {
            get
            {
                try
                {
                    //����һ��������
                    locker.WaitOne();

                    StringBuilder sb = new StringBuilder();
                    object[] objects = stTraceInfo.ToArray();
                    string line = string.Empty; 
                    string outDev = string.Empty;
                    for (int i = objects.Length - 1; i >= 0; i--)
                    {
                        line = objects[i].ToString();
                        //2015.11.23 ���ڴ���ı༭�ؼ�.text = TraceInfo����ˣ��ڴ˴��������������
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
                    //�ͷ�
                    locker.ReleaseMutex();
                }
            }
            set
            {
                try
                {
                    locker.WaitOne();
                    //stTraceInfo.Push(DateTime.Now.ToLongTimeString() + ": " +value);

                    //2015.11.24 ����Ϣ������У��˴�Ӧ�Ż������豸�����־����
                    stTraceInfo.Enqueue ( value.Substring(0,SysConst.MsgOptDev.SCREEN.Length) + " " +
                        DateTime.Now.ToLongTimeString() + ": " +
                        value.Substring(SysConst.MsgOptDev.SCREEN.Length));
                    //stTraceInfo.Enqueue(DateTime.Now.ToLongTimeString() + ": " + value);
                    while (stTraceInfo.Count > 50)
                    {
                        //stTraceInfo.Pop();
                        //stTraceInfo.Dequeue();
                        #region ��Ļ���д��־ 2015.11.23
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
            Init();
        }

        private void Init()
        {// ������
            
            dsErr.Tables.Add(OrderSplit.CreateErrorDt());

            // ������ֵ
            wardCode = GVars.User.DeptCode;

            orderSplit = new OrderSplit();
            this.oracleAccess = orderSplit.OracleAccess;

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

            // �ж��ļ��Ƿ����
            string fileName;
            if (string.IsNullOrEmpty(IniPath))
                fileName = Path.Combine(Path.Combine(Application.StartupPath, "SQL"), "List" + strNum + ".INI");
            else
                fileName = Path.Combine(Application.StartupPath, IniPath);

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
                //2015.11.23 ��־�ļ���ǰ׺�����Ϊ�գ���д��־
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

        #region ��ͨ����

        #region ���ҽ��
        /// <summary>
        /// ���ҽ��
        /// </summary>
        /// <param name="wardCode"></param>
        /// <returns></returns>
        public void SplitOrders()
        {

            while (true)
            {
                try
                {
                    // �����ַ���
                    setHisNlsLang();

                    #region Ԥ����
                    //��¼��ǰ��ֵ���"ҩ��"����"��ҩ��",Ĭ����ҩ�ơ�
                    bool isPharmacologic = true;
                    DateTime dtBegin = DateTime.Now;
                    long counter = 0;

                    //����ͬ����Ϣ
                    loadSyncInfo();

                    // ��ȡ��ǰʱ��
                    dtNow = orderSplit.GetSysDate();
                    TraceInfo = SysConst.MsgOptDev.ALL + "============" + syncInfos[0].FnPreLog + "============" + SysConst.STR.CRLF
                        + "��ȡϵͳ����: " + dtNow.ToString(ComConst.FMT_DATE.LONG);
                    dsErr.Tables[0].Rows.Clear();
                    orderSplit.SyncInfos = syncInfos;
                    orderSplit.ErrorDs = dsErr;
                    
                    int splitCount = Convert.ToInt32(ConfigurationManager.AppSettings["SPLIT_DATES"] ?? "2");
                    SyncInfo syncInfo = syncInfos[0];
                    //���������ļ�������isPharmacologic,  True:ҩ��   False:��ҩ��  2015-11-11 add
                    isPharmacologic = syncInfo.Comment.Contains("��Чҽ��");



                    #endregion
                    // ��������ҽ��
                    if (blnExit == true)
                    {
                        return;
                    }

                    
                    //������Ҫ��ֵ�ҽ��
                    dsOrders = orderSplit.GetOrdersToday(dtNow.AddDays(splitCount - 1).ToString(ComConst.FMT_DATE.SHORT),isPharmacologic,syncInfo.SrcSqlFile,syncInfo.Filter);//2015.11.15 �����������

                    
                    TraceInfo = SysConst.MsgOptDev.ALL + "��ȡ��������" + GetYzType(syncInfo.Comment) + "��:" + dsOrders.Tables[0].Rows.Count.ToString() + "��";



                    // ���ҽ����Ժ�(������)ҽ��ִ�е�0
                    if (blnExit == true)
                    {
                        return;
                    }

                    //��ȡ�Ѿ�����˵�ҽ��
                    dsOrdersExecute = orderSplit.GetOrdersExecuteSplitted(dtNow.ToString(ComConst.FMT_DATE.SHORT),isPharmacologic,syncInfo.SrcSqlFile,syncInfo.Filter);
                    TraceInfo = SysConst.MsgOptDev.ALL + "��ȡ�����Ѳ��" + GetYzType(syncInfo.Comment) + "ִ�е� ��:" + dsOrdersExecute.Tables[0].Rows.Count.ToString() + "��";

                    
                    orderSplit.IsPharmacologic = isPharmacologic;
                    orderSplit.DsOrders = dsOrders;
                    orderSplit.DsOrdersExecute = dsOrdersExecute;
                    orderSplit.OperationTime = dtNow;
                    

                    #region 2015.11.24��־��ʱ
                    string logLine = string.Empty;
                    logLine = "=====================================================" + SysConst.STR.CRLF;
                    logLine += "��ʼ��֡���������" + syncInfos[0].Filter + ";��Ҫ���:" + dsOrders.Tables[0].Rows.Count.ToString() + ";�Ѳ��:" + dsOrdersExecute.Tables[0].Rows.Count + SysConst.STR.CRLF;
                    if (!string.IsNullOrEmpty(syncInfos[0].FnPreLog))
                        LogClass.WriteLog(logLine, SysConst.LogFnType.YMDH, syncInfos[0].FnPreLog);
                    if (dsOrders.Tables[0].Rows.Count == 1)//����
                    {
                        //��Ҫ��ֵ�ҽ��
                        DataRow[] drOrders = dsOrders.Tables[0].Select("", "PATIENT_ID, VISIT_ID, ORDER_NO, ORDER_SUB_NO");
                        DataRow dr = drOrders[0];
                        LogClass.WriteLog(dr[0] + "|" + dr[1] + "|" + dr[2] + "|" + dr[3], SysConst.LogFnType.YMDH, syncInfos[0].FnPreLog);
                    }
                    #endregion
                    //���������ļ�����������ѭ��
                    for (int i = 1; i <= splitCount; i++)
                    {
                        // ���в��
                        TraceInfo = SysConst.MsgOptDev.ALL + "��ʼ����" + GetYzType(syncInfo.Comment) + "��ֳ��� �� " + i.ToString() + "�����ݲ��...";
                        dtBegin = DateTime.Now;

                        #region 2015.11.24��־��ʱ
                        string logLinetmp = "----------------" + SysConst.STR.CRLF;
                        logLinetmp += "��ֵ�" + i.ToString() + "��";
                        if (!string.IsNullOrEmpty(syncInfos[0].FnPreLog))
                            LogClass.WriteLog(logLinetmp, SysConst.LogFnType.YMDH, syncInfos[0].FnPreLog);
                        #endregion

                        if (blnExit == true)
                        {
                            return;
                        }
                        
                        //jyq 2015.12.18 �ϲ�ҩ�Ʒ�ҩ�Ʋ��ҽ������
                        orderSplit.SplitMedicalOrders(i);

                        // ���б���
                        if (blnExit == true)
                        {
                            return;
                        }

                        //�����ֽ������ֽ������ɹ������order_m�и������ݡ�
                        bool isSaved = saveSplitResult();
                        if (isSaved)
                        {
                            saveOrdersResult(isPharmacologic);
                        }

                        // ������ʧ�ܵ�ԭ��
                        //if (i == 1)
                        //{
                            //saveSplitError();
                        //}

                        // ȷ��һ�������Ĳ��ѭ����ʱ���� 1����(60��)����
                        while (DateTime.Now.Subtract(dtBegin).TotalSeconds <= 20)
                        {
                            if (blnExit == true) { return; }
                            Thread.Sleep(100);
                        }
                    }

                    // ɾ������ҽ��(����orders_m�Ѿ����Ϊorders_execute�ļ�¼�����ν����������ݺ�orders_m�Խӵ�ʱ��,�����в����¿��ĺ�ִ�е�ҽ������
                    // orders_m����ɾ�������ݽ��뱸�ݱ�orders_m_tombstone��)
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
                            TraceInfo = SysConst.MsgOptDev.SCREEN + DateTime.Now.ToString(ComConst.FMT_DATE.LONG) + delinfo.Msg;
                            Thread.Sleep(1000);
                        }
                    }

                    TraceInfo = SysConst.MsgOptDev.SCREEN + " HS    -- һ��ѭ������ ���� ��һ��ѭ�� ����!";

                    // ��Ϣ10��
                    for (int j = 0; j < 10 * 10; j++)
                    {
                        if (blnExit == true) { return; }
                        Thread.Sleep(100);
                    }
                }
                catch (Exception ex)
                {
                    TraceInfo = SysConst.MsgOptDev.SCREEN + "��̨����̳߳���" + ex.Message;        // �쳣��־  

                    if (!string.IsNullOrEmpty(syncInfos[0].FnPreLog))
                        LogClass.WriteLog(GVars.User.UserName + ex.Message + ex.StackTrace, SysConst.LogFnType.YMDH, syncInfos[0].FnPreLog);
                    //LogFile.WriteLog(GVars.User.UserName + ex.Message + ex.StackTrace);//2015.11.23 del
                }
                finally
                {

                    #region 2015.11.24��־��ʱ
                    string logLine = SysConst.STR.CRLF + "��һ��ѭ����ֽ�����" + SysConst.STR.CRLF;
                    if (!string.IsNullOrEmpty(syncInfos[0].FnPreLog))
                        LogClass.WriteLog(logLine, SysConst.LogFnType.YMDH, syncInfos[0].FnPreLog);
                    #endregion
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
            TraceInfo = SysConst.MsgOptDev.SCREEN + "ɾ������ҽ��ִ�е������ɹ�!";
        }

        public void DeleteOrdersExeTombstone(int days)
        {
            string sql = string.Empty;
            sql += "delete from ORDERS_EXECUTE_TOMBSTONE E where E.CREATE_TIMESTAMP<TO_DATE(SYSDATE-" + days.ToString() + ")";
            oracleAccess.ExecuteNoQuery(sql);
            TraceInfo = SysConst.MsgOptDev.SCREEN + "ɾ��ҽ��ִ�е����ݱ�������ݲ����ɹ�!";
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
            TraceInfo = SysConst.MsgOptDev.SCREEN + "����Ѳ��,��������ʾ��ִ���ҽ�� " + count.ToString() + " ��!";

            if (count > 0)
            {
                RowCountChanged = count;
                TraceInfo = SysConst.MsgOptDev.SCREEN + "�������: " + dtBegin.ToString(ComConst.FMT_DATE.LONG) + " ��: " + rowCount.ToString() + "��";
            }
        }

        #endregion

        public void Exit()
        {
            blnExit = true;
        }

        #region ��ȡ����ֶ��б�(ʱ������ͳ���
        /// <summary>
        /// ��ȡ����ֶ��б�(ʱ������ͳ���)
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
            bool result = true;
            try
            {                
                DataSet ds = dsOrdersExecute.GetChanges();
                if (ds != null && ds.Tables.Count > 0)
                {
                    TraceInfo = SysConst.MsgOptDev.SCREEN + "��ʼ׼�������ֵ�ҽ��ִ�е�...";
                    string str = string.Join(",", ds.Tables[0].Select("", "", DataViewRowState.Added).Select(r => string.Format("('{0}',{1},{2},{3},to_date('{4}','yyyy-mm-dd hh24:mi:ss'))", r["patient_id"], r["visit_id"], r["order_no"], r["order_sub_no"], r["schedule_perform_time"])).ToArray());
                    string str1 = string.Join(",", ds.Tables[0].Select("", "", DataViewRowState.Added).Select(r => string.Format("('{0}',{1},{2},{3})", r["patient_id"], r["visit_id"], r["order_no"], r["order_sub_no"])).ToArray());
                    TraceInfo = SysConst.MsgOptDev.SCREEN + "һ��Ҫ �����ֵ�ҽ��ִ�е�" + ds.Tables[0].Rows.Count.ToString() + "��  ���!";

                    RowCountChanged = ds.Tables[0].Rows.Count;
                    TraceInfo = SysConst.MsgOptDev.SCREEN + "�������: " + dtBegin.ToString(ComConst.FMT_DATE.LONG) + " ��: " + rowCount.ToString() + "��";

                    TraceInfo = SysConst.MsgOptDev.SCREEN + "�����ֵ�ҽ��ִ�е�   �ܱ仯   ���!";
                    result = orderSplit.saveSplitResult();
                }
            }
            catch (Exception ex)
            {
                TraceInfo = SysConst.MsgOptDev.SCREEN + "��̨����̳߳���" + ex.Message;    // �쳣��־
                result = false;
            }
            return result;
        }

        /// <summary>
        /// ����Orders_m�仯
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
                    this.TraceInfo = SysConst.MsgOptDev.SCREEN + "�����ֵ�ҽ��...";
                    this.TraceInfo = SysConst.MsgOptDev.SCREEN + "�����ֵ�ҽ��" + changes.Tables[0].Rows.Count.ToString() + " ���!";
                    this.RowCountChanged = changes.Tables[0].Rows.Count;
                    this.TraceInfo = SysConst.MsgOptDev.SCREEN + "�����ֵ�ҽ�� ���!";
                    result = orderSplit.saveOrdersResult();
                }
            }
            catch (Exception ex)
            {
                this.TraceInfo = SysConst.MsgOptDev.SCREEN + "��̨����̳߳���" + ex.Message;
                result = false;
            }
            return result;
        }

        #endregion

        #region ������ʧ��ԭ��
        /// <summary>
        /// ������ʧ��ԭ��,�˷����Ѿ����ڣ�������ע���ˡ�
        /// </summary>
        [Obsolete]
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

            TraceInfo = SysConst.MsgOptDev.SCREEN + "�������: " + dtBegin.ToString(ComConst.FMT_DATE.LONG) + " ��: " + rowCount.ToString() + "��";
        }

        #endregion

        #region �ж���ҩ��ҽ�����Ƿ�ҩ��ҽ��

        /// <summary>
        /// �ж���ҩ��ҽ�����Ƿ�ҩ��ҽ��
        /// 2015-11-11
        /// add
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        private string GetYzType(string comment)
        {
            string typeValue = string.Empty;
            typeValue = comment.Contains("��ҩ��") ? "��ҩ��ҽ��" : "ҩ��ҽ��";
            return typeValue;
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
                        TraceInfo = SysConst.MsgOptDev.SCREEN + ex.Message;

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
