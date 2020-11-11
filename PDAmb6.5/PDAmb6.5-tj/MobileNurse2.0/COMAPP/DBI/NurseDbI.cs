using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SQL = HISPlus.SqlManager;
using System.IO;

namespace HISPlus
{
    public class NurseDbI
    {
        private DataWebSrv.DataWebSrv webSync       = new DataWebSrv.DataWebSrv();
        
        public NurseDbI()
        {
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
            string configFile = Path.Combine(appPath, "Config.xml");
            DataSet dsConfig = new DataSet("Config");
            dsConfig.ReadXml(configFile, XmlReadMode.ReadSchema);
            DataRow dr = dsConfig.Tables[0].Rows[0];
            int days = Convert.ToInt32(dr["PDACLIENTTIMEOUT"].ToString());
            webSync.Url = UrlIp.ChangeIpInUrl(webSync.Url, GVars.App.ServerIp);
            webSync.Timeout = days * 1000;// webSync.GetPdaClientTimeout() * 1000;
        }
        
        
        /// <summary>
        /// 获取交班记录
        /// </summary>
        /// <returns></returns>
        public string GetNurseWorkHandoffRec(string patientId, string visitId)
        {
            //return webSync.GetShiftWorkRec(patientId, visitId);
            return string.Empty;
        }
        
        
        /// <summary>
        /// 保存交班记录
        /// </summary>
        /// <returns></returns>
        public bool SaveNurseWorkHandoffRec(ref DataSet dsChanged, string patientId, string visitId)
        {
            //string sql = "SELECT * FROM EXCHANGEWORK "
            //            + "WHERE PATIENT_ID = " + SQL.SqlConvert(patientId)
            //            +       "AND VISIT_ID = " + SQL.SqlConvert(visitId)
            //            + "ORDER BY "
            //            +       "RECORD_TIME ";
                        
            GVars.sqlceLocal.Update(ref dsChanged);
            return true;
        }

        public bool SaveSpeciment(DataSet dsChanged)
        {
            return webSync.SaveSpeciment(dsChanged);
        }
    }
}
