using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;

using SQL = HISPlus.SqlManager;
using System.IO;

namespace HISPlus
{
    public class PatientDbI
    {


        private PatientWebSrv.PatientWebSrv patientWebSrv = new PatientWebSrv.PatientWebSrv();


        public PatientDbI()
        {
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
            string configFile = Path.Combine(appPath, "Config.xml");
            DataSet dsConfig = new DataSet("Config");
            dsConfig.ReadXml(configFile, XmlReadMode.ReadSchema);
            DataRow dr = dsConfig.Tables[0].Rows[0];
            int days = Convert.ToInt32(dr["PDACLIENTTIMEOUT"].ToString());
            patientWebSrv.Timeout = days * 1000; //webSync.GetPdaClientTimeout() * 1000; ;
        }


        /// <summary>
        /// 设置服务器IP地址
        /// </summary>
        public void SetServerIp(string serverIp)
        {
            patientWebSrv.Url = UrlIp.ChangeIpInUrl(patientWebSrv.Url, serverIp);
        }


        ///// <summary>
        ///// 获取病人列表
        ///// </summary>
        ///// <returns></returns>
        //public DataSet GetPatientList()
        //{
        //    string sql = "SELECT * FROM PATIENT_INFO ORDER BY BED_NO";

        //    return GVars.sqlceLocal.SelectData(sql);
        //}


        /// <summary>
        /// 获取病人的过滤条件
        /// </summary>
        /// <returns></returns>        
        public DataSet GetPatFilterName()
        {
            string sql = "SELECT * FROM PAT_FILTER_NAME "
                        + "WHERE DEPT_CODE =" + SqlManager.SqlConvert(GVars.User.DeptCode)
                        + "ORDER BY SHOW_ORDER";

            return GVars.sqlceLocal.SelectData(sql);
        }


        /// <summary>
        /// 按过滤条件查找病人
        /// </summary>
        /// <returns></returns>
        public DataSet GetPatientInfo_Filter(string itemList)
        {
            if (GVars.Demo == true)
            {
                string sql = "SELECT * FROM PATIENT_INFO ORDER BY BED_NO";

                return GVars.sqlceLocal.SelectData(sql);
            }

            return patientWebSrv.GetPatientInfo_Filter(GVars.App.DeptCode, itemList);
        }
    }
}
