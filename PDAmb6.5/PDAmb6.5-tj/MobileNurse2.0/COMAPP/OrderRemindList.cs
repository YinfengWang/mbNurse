using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Sql = HISPlus.SqlManager;

namespace HISPlus
{
    class OrderRemindList
    {
      
        public OrderRemindList()
        {
            GVars.HISDataSrv.Url = UrlIp.ChangeIpInUrl(GVars.HISDataSrv.Url, GVars.App.ServerIp);
        }

        /// <summary>
        /// 获取科室新开医嘱提醒的患者列表
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public DataSet GetPatientInfoList(string deptCode)
        {
            return GVars.HISDataSrv.GetOrderRemindPatientList(deptCode);
        }

        /// <summary>
        /// 获取科室新开医嘱提醒的患者医嘱信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="bedLabel"></param>
        /// <returns></returns>
        public DataSet GetOrderRemindInfo(string deptCode, string bedLabel)
        {
            return GVars.HISDataSrv.GetOrderRemindInfo(deptCode, bedLabel);
        }
    }
}
