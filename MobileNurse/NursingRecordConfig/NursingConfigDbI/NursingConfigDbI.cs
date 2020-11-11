using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQL = HISPlus.SqlManager;
using System.Data;

namespace HISPlus
{
    public class NursingConfigDbI
    {
        protected DbAccess _connection;
        public NursingConfigDbI(DbAccess conn)
        {
            _connection = conn;
        }
        /// <summary>
        /// 获取护理记录单信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetConfigList(string tableName, string filer)
        {
            string sql = string.Empty;
            if (filer.Trim().Length == 0 || filer == null)
            {
                sql = "SELECT * FROM " + tableName + "";
            }
            else
            {
                sql = "SELECT * FROM " + tableName + " where " + filer;
            }
            return _connection.SelectData(sql, tableName);
        }

        public DataSet GetConfigList(string sql)
        {
            if (sql.Trim().Length > 0)
            {
                return _connection.SelectData(sql);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取ds显示信息
        /// </summary>
        /// <returns></returns>
        public DataSet ReturnConfigList()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select n1.*,n2.descriptions,n2.PARAMETER_ID ");
            sql.Append(" from nursing_record_define n1,NURSING_RECORD_TYPE n2 where n1.type_id=n2.type_id");

            return _connection.SelectData(sql.ToString());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="dsConfig">更改后的ds</param>
        /// <returns></returns>
        public bool SaveConfigList(ref DataSet dsConfig, string tableName)
        {
            string sql = "SELECT * FROM " + tableName + "";

            _connection.Update(ref dsConfig, tableName, sql);

            return true;
        }

        /// <summary>
        /// 获取某个表的列名
        /// </summary>
        /// <param name="tb_name">表名</param>
        /// <returns></returns>
        public DataSet ReturnColName(string tb_name)
        {
            string sql = "select COLUMN_NAME from all_TAB_COLUMNS where table_name=" + SQL.SqlConvert(tb_name);
            return _connection.SelectData(sql);
        }

        /// <summary>
        /// 获取COL_ID的最大值并加1
        /// </summary>
        /// <param name="type_id"></param>
        /// <returns></returns>
        public string ReturnMaxTypeId(string type_id)
        {
            string col_id = string.Empty;
            int int_col = 1000;
            string sql = "select max(col_id) col_id from nursing_record_define where type_id=" + SQL.SqlConvert(type_id);
            DataSet ds = _connection.SelectData(sql);
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["col_id"].ToString().Trim().Trim().Length > 0)
            {
                int_col = Convert.ToInt32(ds.Tables[0].Rows[0]["col_id"].ToString()) + 1;
            }
            else
            {
                int_col = 1;
            }
            col_id = string.Format("{0:00}", int_col);
            return col_id;
        }
        /// <summary>
        /// 获取TYPE_ID的最大值并加1
        /// </summary>
        /// <returns></returns>
        public string ReturnMaxTypeId()
        {
            string typeid = string.Empty;
            int int_type = 1000;
            string sql = "select max(type_id) type_id from nursing_record_type";
            DataSet ds = _connection.SelectData(sql);
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["type_id"].ToString().Trim().Trim().Length > 0)
            {
                int_type = Convert.ToInt32(ds.Tables[0].Rows[0]["type_id"].ToString()) + 1;
            }
            else
            {
                int_type = 1;
            }
            typeid = string.Format("{0:00}", int_type);
            return typeid;
        }


    }
}
