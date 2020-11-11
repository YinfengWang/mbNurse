using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.Web.Services.Protocols;
using System.IO;

namespace HISPlus.COMAPP.DBI
{
    public class XunShiDbI
    {
        private DataWebSrv.DataWebSrv webSync = new DataWebSrv.DataWebSrv();

        public XunShiDbI()
        {
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
            string configFile = Path.Combine(appPath, "Config.xml");
            DataSet dsConfig = new DataSet("Config");
            dsConfig.ReadXml(configFile, XmlReadMode.ReadSchema);
            DataRow dr = dsConfig.Tables[0].Rows[0];
            int days = Convert.ToInt32(dr["PDACLIENTTIMEOUT"].ToString());

            webSync.Url = UrlIp.ChangeIpInUrl(webSync.Url, GVars.App.ServerIp);

            webSync.Timeout = days * 1000; //webSync.GetPdaClientTimeout() * 1000;
        }





        /// <summary>
        /// 获取病人巡视
        /// </summary>
        /// <returns></returns>
        public DataSet GetXunShis(string patientId, string visitId, string wardCode)
        {
            return webSync.GetXunShis(patientId, visitId, wardCode);
        }

        /// <summary>
        /// 保存病人的医嘱执行单
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public bool SaveXunShi(ref DataSet dsChanged, string sql)
        {
            return webSync.SaveData(dsChanged);
        }
        /// <summary>
        /// 根据科室获取 科室 内部自定义的巡视 内容
        /// </summary>
        /// <param name="wardCode"></param>
        /// <returns></returns>
        public DataSet GetXunShiClass(string wardCode)
        {
            return webSync.getDeptCodeCount(wardCode);
        }
    }
}
