using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SQL = HISPlus.SqlManager;
using System.Diagnostics;
using System.IO;

namespace HISPlus
{
    public class OrdersDbI
    {
        private DataWebSrv.DataWebSrv webSync = new DataWebSrv.DataWebSrv();
         
        Stopwatch watch;

        public OrdersDbI()
        {

            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
            string configFile = Path.Combine(appPath, "Config.xml");
            DataSet dsConfig = new DataSet("Config");
            dsConfig.ReadXml(configFile, XmlReadMode.ReadSchema);
            DataRow dr = dsConfig.Tables[0].Rows[0];
            int days = Convert.ToInt32(dr["PDACLIENTTIMEOUT"].ToString());

            webSync.Url = UrlIp.ChangeIpInUrl(webSync.Url, GVars.App.ServerIp);

            webSync.Timeout = days * 1000; //;webSync.GetPdaClientTimeout() * 1000
            
        }


        /// <summary>
        /// 获取病人医嘱
        /// </summary>
        /// <returns></returns>
        public DataSet GetOrders(string patientId, string visitId, DateTime dtNow)
        {
            return webSync.GetOrders(patientId, visitId);
        }


        /// <summary>
        /// 获取病人的医嘱执行单
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public DataSet GetOrdersExecute(string patientId, string visitId, DateTime dtNow, int hoursPre, int hoursNext, ref string sqlSel)
        {
            return webSync.GetOrdersExecute(patientId, visitId);
        }

        /// <summary>
        /// 获取病人的医嘱执行单
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public DataSet GetOrdersExecuteTime(string patientId, string visitId, string start, string stop)
        {
            watch = Stopwatch.StartNew();

            DataSet dsGetOrdersExecute = webSync.GetOrdersExecuteTime(patientId, visitId, start, stop);

            watch.Stop();

            COMAPP.Function.SystemLog.RecordExpendTime("OrdersDbi", "OrdersDbi/GetOrdersExecuteTime", GVars.ScanReaderBuffer.Text.Trim(), System.DateTime.Now.ToString(), watch.ElapsedMilliseconds.ToString());

            return dsGetOrdersExecute;
        }

        /// <summary>
        /// 保存病人的医嘱执行单
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public bool SaveOrdersExecute(ref DataSet dsChanged, string sql)
        {
            return webSync.SaveData(dsChanged);
        }
    }
}
