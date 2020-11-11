using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using HISPlus;

namespace HISPlus
{
    public class DocumentDbI
    {
        #region 变量
        protected DbAccess _connection = null;             // 数据库连接

        private DataSet dsItemDict = null;             // 评估字典
        #endregion

        public DocumentDbI(DbAccess connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// 获取模板类型列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetTemplateClass()
        {
            const string sql = @"SELECT * FROM DOC_TEMPLATE_CLASS ORDER BY ID";
            DataSet ds = _connection.SelectData(sql);

            if (ds != null)
                return ds.Tables[0];
            return null;
        }

        /// <summary>
        /// 获取模板类型列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetTemplateType()
        {
            const string sql = @"SELECT * FROM DOC_TEMPLATE_TYPE ORDER BY ID";
            DataSet ds = _connection.SelectData(sql);
            if (ds != null)
                return ds.Tables[0];
            return null;
        }

        /// <summary>
        /// 根据模板ID获取模板元素
        /// </summary>
        /// <returns></returns>
        public DataTable GetTemplateElement(decimal templateId)
        {
            string sql = @"SELECT * FROM DOC_TEMPLATE_ELEMENT 
                WHERE TEMPLATE_ID={0} ORDER BY PARENT_ID,SORT_ID";
            sql = string.Format(sql, templateId);

            DataSet ds = _connection.SelectData(sql, "DOC_TEMPLATE_ELEMENT");

            if (ds != null)
                return ds.Tables[0];
            return null;
        }

        /// <summary>
        /// 根据模板ID获取模板元素
        /// </summary>
        /// <returns></returns>
        public DataTable UpdateTemplateElement(decimal templateId, DataTable dt)
        {
            //            string sql = @"SELECT * FROM DOC_TEMPLATE_ELEMENT 
            //                WHERE TEMPLATE_ID={0} ORDER BY PARENT_ID,SORT_ID";
            //            sql = string.Format(sql, templateId);

            DataSet ds = dt.DataSet;

            _connection.Update(ref ds);

            if (ds != null)
                return ds.Tables[0];
            return null;
        }
        /// <summary>
        /// 获取模板元素下一序列值
        /// </summary>
        /// <returns></returns>
        public int GetTemplateElementId()
        {
            const string sql = @"SELECT SEQ_ID.NEXTVAL as value FROM DUAL";
            DataSet ds = _connection.SelectData(sql);
            if (ds != null) return (int)ds.Tables[0].Rows[0][0];
            return 1;
        }

        /// <summary>
        /// 获取所有控件模板数据
        /// </summary>        
        /// <returns></returns>
        public DataTable GetAllControlTemplate()
        {
            string sql = @"SELECT * FROM DOC_CONTROL_TEMPLATE WHERE IS_ENABLED=1";

            DataSet ds = _connection.SelectData(sql);

            if (ds != null)
                return ds.Tables[0];
            return null;
        }

        /// <summary>
        /// 获取控件状态
        /// </summary>        
        /// <returns></returns>
        public DataTable GetAllControlStatus()
        {
            string sql = @"SELECT * FROM DOC_CONTROL_STATUS WHERE IS_ENABLED=1";

            DataSet ds = _connection.SelectData(sql);

            if (ds != null)
                return ds.Tables[0];
            return null;
        }

        /// <summary>
        /// 获取频次列表
        /// </summary>        
        /// <returns></returns>
        public DataTable GetAllFreq()
        {
            const string sql = @"SELECT SERIAL_NO,FREQ_DESC FROM PERFORM_FREQ_DICT";//2015.10.30 COMM.PERFORM_FREQ_DICT";

            DataSet ds = _connection.SelectData(sql);

            if (ds != null)
                return ds.Tables[0];
            return null;
        }

        /// <summary>
        /// 获取所有控件模板数据
        /// </summary>        
        /// <returns></returns>
        public List<DocControlTemplate> GetAllControlTemplateList()
        {
            string sql = @"SELECT * FROM DOC_CONTROL_TEMPLATE";

            DataSet ds = _connection.SelectData(sql);
            if (ds != null && ds.Tables.Count > 0)
                return EntityConvert.ConvertToList<DocControlTemplate>(ds.Tables[0]);
            return null;
        }

        /// <summary>
        /// 获取所有控件类型
        /// </summary>        
        /// <returns></returns>
        public DataSet GetAllControlType()
        {
            string sql = "SELECT * FROM DOC_CONTROL_TYPE ";

            return _connection.SelectData(sql);
        }

        //public DocTemplate GetTemplate(int templateId)
        //{
        //    string sql = @"SELECT * FROM DOC_TEMPLATE WHERE TEMPLATE_ID={0}";
        //    sql = string.Format(sql, templateId);
        //    DataSet ds = _connection.SelectData(sql);
        //    if (ds != null && ds.Tables.Count > 0)
        //        if (ds.Tables[0].Rows.Count == 0)
        //            return new DocTemplate();
        //    return EntityConvert.ConvertToList<DocTemplate>(ds.Tables[0])[0];
        //    return null;
        //}

        //public bool UpdateTemplate(DocTemplate entity)
        //{
        //    DataTable dt = EntityConvert.ConvertToDataTable<DocTemplate>(new List<DocTemplate> { entity });

        //    DataRow dr = dt.NewRow();
        //    DataRow[] drs = { dr };

        //    string sql = @"SELECT * FROM DOC_TEMPLATE WHERE TEMPLATE_ID={0}";
        //    sql = string.Format(sql, entity.TemplateId);
        //    DataSet ds = _connection.SelectData(sql);
        //    ds.Tables.RemoveAt(0);
        //    ds.Tables.Add(dt);

        //    _connection.Update(ref ds);

        //    return true;
        //}

        public DataTable GetTemplate(decimal templateId)
        {
            string sql = @"SELECT * FROM DOC_TEMPLATE WHERE TEMPLATE_ID={0}";
            sql = string.Format(sql, templateId);
            DataSet ds = _connection.SelectData(sql, "DOC_TEMPLATE");
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public bool UpdateTemplate(DataTable dt)
        {
            DataSet ds = dt.DataSet;

            _connection.Update(ref ds);

            return true;
        }

        /// <summary>
        /// 获取所有控件类型
        /// </summary>        
        /// <returns></returns>
        public List<DocControlType> GetAllControlTypeList()
        {
            string sql = "SELECT * FROM DOC_CONTROL_TYPE ";

            DataSet ds = _connection.SelectData(sql);
            if (ds != null && ds.Tables.Count > 0)
                return EntityConvert.ConvertToList<DocControlType>(ds.Tables[0]);
            return null;
        }
    }
}
