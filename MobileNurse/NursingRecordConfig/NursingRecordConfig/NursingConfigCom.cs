using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQL = HISPlus.SqlManager;
using System.Data;

namespace HISPlus
{
    public class NursingConfigCom
    {
        private NursingConfigDbI configrDbI;

        public NursingConfigCom()
        {
            configrDbI = new NursingConfigDbI(GVars.OracleAccess);
        }

        /// <summary>
        /// 获取对应表的数据集
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public DataSet GetConfigList(string tableName, string filter)
        {
            return configrDbI.GetConfigList(tableName, filter);
        }

        public DataSet GetConfigList(string sql)
        {
            return configrDbI.GetConfigList(sql);
        }

        /// <summary>
        /// 修改或新增数据库
        /// </summary>
        /// <param name="dsConfig">更新后的数据</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public bool SaveConfigList(ref DataSet dsConfig, string tableName)
        {
            return configrDbI.SaveConfigList(ref dsConfig, tableName);
        }

        /// <summary>
        /// 显示联合表的数据
        /// </summary>
        /// <returns></returns>
        public DataSet ReturnConfigList()
        {
            return configrDbI.ReturnConfigList();
        }
        /// <summary>
        /// 获取某个表的列
        /// </summary>
        /// <param name="tb_name">表名</param>
        /// <returns></returns>
        public DataSet ReturnColName(string tbName)
        {
            return configrDbI.ReturnColName(tbName);
        }
        /// <summary>
        /// 获取type_ID的最大值并加1
        /// </summary>
        /// <returns></returns>
        public string ReturnMaxTypeId()
        {
            return configrDbI.ReturnMaxTypeId();
        }
        /// <summary>
        /// 获取某个记录单中COL_ID的最大值并加1
        /// </summary>
        /// <param name="type_id">记录单号</param>
        /// <returns>当没有时返回01</returns>
        public string ReturnMaxTypeId(string typeId)
        {
            return configrDbI.ReturnMaxTypeId(typeId);
        }

    }
}
