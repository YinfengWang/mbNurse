﻿//HS 2012 年 整理

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace HISPlus
{
    public class ServerCreateSyncInfo
    {
        #region 变量
        protected string _wardCode = string.Empty;

        private bool _blnExit = false;

        private DbAccess _oraHis = null;
        private DbAccess _connMobile = null;

        //private Stack           stTraceInfo = new Stack(210);
        private Queue stTraceInfo = new Queue(210);

        private Mutex locker = new Mutex();          // 互斥锁

        private List<SyncInfo> syncInfos = new List<SyncInfo>();
        private string pathData = string.Empty;        // 临时存储数据的文件夹

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public string IniPath;
        #endregion

        #region  构造函数谅解
        private class SyncInfo
        {
            public string Comment;
            public string SrcSqlFile;
            public string DestSql;
            public string TableName;
            public string Filter;
        }


        public ServerCreateSyncInfo()
        {
            _oraHis = new OleDbAccess();
            _connMobile = new OracleAccess();

            _oraHis.ConnectionString = GVars.OracleHis.ConnectionString;
            _connMobile.ConnectionString = GVars.OracleMobile.ConnectionString;
        }
        #endregion


        #region 属性
        public string WardCode
        {
            get { return _wardCode; }
            set { _wardCode = value; }
        }


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
                    stTraceInfo.Enqueue(value);
                    while (stTraceInfo.Count > 200)
                    {
                        stTraceInfo.Dequeue();
                    }
                }
                finally
                {
                    locker.ReleaseMutex();
                }
            }
        }
        #endregion

        /// <summary>
        /// 执行任务
        /// </summary>
        public void SqlTask()
        {
            try
            {
                FileInfo fileInfo = new FileInfo(Path.Combine(Application.StartupPath, this.IniPath));
                string sql = string.Empty;
                if (fileInfo.Exists)
                {
                    StreamReader reader = fileInfo.OpenText();
                    sql = reader.ReadToEnd();
                    if (!string.IsNullOrEmpty(sql))
                    {
                        string[] sqls = sql.Split(new char[] { ';', '；' });
                        ArrayList array = new ArrayList(sqls);
                        array.Remove("");
                        while (true)
                        {
                            _connMobile.ExecuteNoQuery(ref array);
                            TraceInfo = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 已同步";
                            //十分钟执行一次
                            Thread.Sleep(10 * 60 * 1000);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                TraceInfo = e.Message;
            }
        }


        #region 接口
        public void Trace()
        {
            try
            {
                // 设置字符集
                setHisNlsLang();
                setMobileNlsLang();

                // 加载同步信息
                loadSyncInfo();

                // 加载其它配置信息
                loadConfigInfo();
            }
            catch (Exception ex)
            {
                TraceInfo = ex.Message;
                return;
            }

            while (_blnExit == false)
            {
                try
                {
                    Thread.Sleep(1000);

                    for (int i = 0; i < syncInfos.Count; i++)
                    {
                        SyncInfo syncInfo = syncInfos[i];

                        TraceInfo = DateTime.Now.ToString(ComConst.FMT_DATE.LONG) + syncInfo.Comment;

                        //SyncData(getSqlContent(syncInfo.SrcSqlFile), syncInfo.DestSql, syncInfo.TableName, syncInfo.Filter);
                        CreateSyncInfo(getSqlContent(syncInfo.SrcSqlFile), syncInfo.DestSql, syncInfo.TableName,syncInfo.Filter);

                        if (_blnExit) return;

                        Thread.Sleep(1000);
                    }

                    // 休息3秒
                    for (int i = 0; i < 3; i++)
                    {
                        if (_blnExit) return;
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception ex)
                {
                    TraceInfo = ex.Message;

                    // 休息5秒
                    for (int i = 0; i < 5; i++)
                    {
                        if (_blnExit) return;
                        Thread.Sleep(1000);
                    }
                }
            }
        }


        public void Exit()
        {
            _blnExit = true;
        }
        #endregion


        #region 共通函数

        /// <summary>
        /// 数据同步
        /// </summary>
        /// <param name="sqlSrc"></param>
        /// <param name="sqlDest"></param>
        /// <param name="tableNameDest"></param>
        /// <param name="filter"></param>
        private void SyncData(string sqlSrc, string sqlDest, string tableNameDest, string filter = "")
        {
            if (!string.IsNullOrEmpty(filter))
            {
                sqlSrc = sqlSrc + " and ward_code in (" + filter + ")";
                sqlDest = sqlDest + " and ward_code in (" + filter + ")";
            }

            DataSet dsSrc = _oraHis.SelectData_NoKey(sqlSrc,tableNameDest);
            DataSet dsDest = _connMobile.SelectData(sqlDest,tableNameDest);
            DataSet dsDestModify = dsDest.Copy();

            if (pathData.Length > 0)
            {
                try
                {
                    dsSrc.WriteXml(Path.Combine(pathData, tableNameDest) + ".xml", XmlWriteMode.WriteSchema);
                }
                catch
                { }
            }

            TraceInfo = "源 数 据: " + dsSrc.Tables[0].Rows.Count + " 条";
            TraceInfo = "目标数据: " + dsDest.Tables[0].Rows.Count + " 条";

            /*
             * 根据医嘱表主键比较两个数据集：
             * 1，求交集：逐行比较源数据行和目标数据行，如果有变化，用源数据更新目标数据。
             * 2，源减目标如果有数据：向目标插入差集。
             * 3，目标减源如果有数据：从目标数据集中删除差集。
             */
            var querySrc = dsSrc.Tables[0].AsEnumerable();
            var queryDest = dsDest.Tables[0].AsEnumerable();

            //1，求交集：逐行比较源数据行和目标数据行，如果有变化，用源数据更新目标数据。
            string[] primaryKeys = dsDest.Tables[0].PrimaryKey.Select(c => c.ColumnName).ToArray();
            DataRowComparer primaryKeyComparer = new DataRowComparer(primaryKeys);
            var intersectSet = querySrc.Intersect(queryDest, primaryKeyComparer);

            DataColumn[] columns = new DataColumn[dsDest.Tables[0].Columns.Count];
            dsDest.Tables[0].Columns.CopyTo(columns,0);
            string[] columnNames = columns.Select(c=>c.ColumnName).ToArray();
            DataRowComparer columnsComparer = new DataRowComparer(columnNames);

            foreach (var row in intersectSet)
            {
                string filterStr = GetFilter(primaryKeys, row);
                DataRow rowSrc = dsSrc.Tables[0].Select(filterStr)[0];
                DataRow rowDest = dsDestModify.Tables[0].Select(filterStr)[0];
                //如果源数据行与目标数据行不一致，则用源数据行更新目标数据行
                if (!columnsComparer.Equals(rowSrc, rowDest))
                {
                    ModifyRow(rowSrc, rowDest, columnNames);
                }
            }

            //2，源减目标如果有数据：向目标插入差集。
            var srcExceptDestSet = querySrc.Except(queryDest, primaryKeyComparer);
            foreach (DataRow row in srcExceptDestSet)
            {
                DataRow newRow = dsDestModify.Tables[0].NewRow();
                foreach (DataColumn column in newRow.Table.Columns)
                {
                    if (row.Table.Columns.Contains(column.ColumnName) && column.ColumnName.IndexOf("_TIMESTAMP") < 0)
                        newRow[column.ColumnName] = row[column.ColumnName];
                }
                dsDestModify.Tables[0].Rows.Add(newRow);
            }

            //3，目标减源如果有数据：从目标数据集中删除差集。
            var destExceptSrcSet = queryDest.Except(querySrc, primaryKeyComparer);
            foreach (DataRow row in destExceptSrcSet)
            {
                string filterStr = GetFilter(primaryKeys, row);
                DataRow destRow = dsDestModify.Tables[0].Select(filterStr)[0];
                destRow.Delete();
            }

            //保存数据
            if (dsDestModify.HasChanges() == true)
            {
                DataSet modifiedDestDs = dsDestModify.GetChanges();

                TraceInfo = "数据变更: " + modifiedDestDs.Tables[0].Rows.Count.ToString() + "条";

                if (tableNameDest.ToUpper() == "ORDERS_M")
                {
                    string str = string.Join(",", dsDestModify.Tables[0].Select("", "", DataViewRowState.Added).AsEnumerable().Select(r => string.Format("('{0}','{1}','{2}','{3}')", r["patient_id"], r["visit_id"], r["order_no"], r["order_sub_no"])).ToArray());
                    if (!string.IsNullOrEmpty(str))
                    {
                        string sql = @"select * from orders_m t where (t.patient_id,t.visit_id,t.order_no,t.order_sub_no) in (" + str + ")";
                        DataSet repeatDs = _connMobile.SelectData(sql);
                        if (repeatDs != null && repeatDs.Tables.Count > 0 && repeatDs.Tables[0].Rows.Count > 0)
                        {
                            List<string> list = new List<string>();
                            foreach (DataRow row in repeatDs.Tables[0].Rows)
                            {
                                list.Add(string.Format("('{0}',{1},{2},{3})", row["patient_id"].ToString(), row["visit_id"].ToString(), row["order_no"].ToString(), row["order_sub_no"].ToString()));
                                DataRow[] rows = dsDestModify.Tables[0].Select(string.Format("patient_id='{0}' and  visit_id={1} and order_no={2} and order_sub_no={3}", row["patient_id"].ToString(), row["visit_id"].ToString(), row["order_no"].ToString(), row["order_sub_no"].ToString()));
                                if (rows.Length > 0)
                                    dsDestModify.Tables[0].Rows.Remove(rows[0]);
                            }
                            string errorMsg = "主键冲突：(patient_id,visit_id,order_no,order_sub_no) = " + string.Join(",", list.ToArray());
                            LogFile.WriteLog(errorMsg);
                            TraceInfo = errorMsg;
                        }
                    }
                }
                _connMobile.Update(ref dsDestModify, tableNameDest, sqlDest);
            }
            else
            {
                TraceInfo = "数据变更: 0 条";
            }
        }

        private static string GetFilter(string[] primaryKeys, DataRow row)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string str in primaryKeys)
            {
                sb.AppendFormat("{0}='{1}' AND ", str, row[str]);
            }
            string filterStr = sb.ToString().TrimEnd(" AND ".ToCharArray());
            return filterStr;
        }

        /// <summary>
        /// 用源数据行更新新数据行
        /// </summary>
        /// <param name="rowSrc"></param>
        /// <param name="rowDest"></param>
        private void ModifyRow(DataRow rowSrc, DataRow rowDest,string[] columnNames)
        {
            if (rowSrc == null || rowDest == null)
            {
                throw new ArgumentNullException();
            }
            foreach (string columnName in columnNames)
            {
                if (columnName.IndexOf("_TIMESTAMP") > -1)
                    continue;
                if (rowSrc.Table.Columns.Contains(columnName)&&rowDest.Table.Columns.Contains(columnName))
                {
                    if (rowSrc[columnName].GetType() == typeof(DateTime) && rowDest[columnName].GetType() == typeof(DateTime))
                    {
                        DateTime time1 = Convert.ToDateTime(rowSrc[columnName]);
                        DateTime time2 = Convert.ToDateTime(rowDest[columnName]);
                        if (DateTime.Compare(time1, time2) != 0)
                        {
                            rowDest[columnName] = rowSrc[columnName];
                        }
                    }
                    else if (rowSrc[columnName].GetType() == typeof(decimal) && rowDest[columnName].GetType() == typeof(decimal))
                    {
                        decimal d1 = Convert.ToDecimal(rowSrc[columnName]);
                        decimal d2 = Convert.ToDecimal(rowDest[columnName]);
                        if (decimal.Compare(d1, d2) != 0)
                        {
                            rowDest[columnName] = rowSrc[columnName];
                        }
                    }
                    else
                    {
                        if (!rowSrc[columnName].ToString().Equals(rowDest[columnName].ToString()))
                        {
                            rowDest[columnName] = rowSrc[columnName];
                        }
                    }
                }
            }
        }

        private DataSet CreateSyncInfo(string sqlSrc, string sqlDest, string tableNameDest, string filter="")
        {
            // 获取数据

            if (!string.IsNullOrEmpty(filter))
            {
                sqlSrc = sqlSrc + " and ward_code in (" + filter + ")";
                sqlDest = sqlDest + " and ward_code in (" + filter + ")";
            }

            DataSet dsSrc = _oraHis.SelectData_NoKey(sqlSrc);              
            DataSet dsDest = _connMobile.SelectData(sqlDest);
            DataSet dsDestModify = dsDest.Copy();

            if (pathData.Length > 0)
            {
                try
                {
                    dsSrc.WriteXml(Path.Combine(pathData, tableNameDest) + ".xml", XmlWriteMode.WriteSchema);
                }
                catch
                { }
            }


            #region 获取排序规则
            //string sort = string.Empty;
            DataColumn[] dcPrimarys = dsDest.Tables[0].PrimaryKey; 
            //for (int i = 0; i < dcPrimarys.Length; i++)
            //{
            //    if (sort.Length > 0)
            //    {
            //        sort += ",";
            //    }
            //    sort += dcPrimarys[i].ColumnName;
            //}

            string sort = string.Join(",", dsDest.Tables[0].PrimaryKey.Select(c => c.ColumnName).ToArray());
            #endregion

            #region 进行排序

            DataRow[] drSrcs = dsSrc.Tables[0].Select(string.Empty, sort);
            DataRow[] drDests = dsDest.Tables[0].Select(string.Empty, sort);
            DataRow[] drDestsM = dsDestModify.Tables[0].Select(string.Empty, sort);

            TraceInfo = "源 数 据: " + drSrcs.Length.ToString() + " 条";
            TraceInfo = "目标数据: " + drDests.Length.ToString() + " 条";

            #endregion

            // 进行比较
            int indexSrc = 0;
            int indexDest = 0;

            float srcFloat = 0;
            float destFloat = 0;

            long counter = 0;
            while (indexSrc < drSrcs.Length && indexDest < drDests.Length)
            {
                counter++;
                if (counter % 1000 == 500) TraceInfo = "已比较 " + counter + "条";
                Thread.Sleep(10);
                if (_blnExit) return null;

                DataRow drSrc = drSrcs[indexSrc];
                DataRow drDest = drDests[indexDest];

                #region 比较大小
                int compare = 0;
                for (int j = 0; j < dcPrimarys.Length; j++)
                {
                    DataColumn dc = dcPrimarys[j];

                    string valSrc = drSrc[dc.ColumnName].ToString();
                    string valDest = drDest[dc.ColumnName].ToString();

                    // 日期类型处理
                    if (dc.DataType == Type.GetType("System.DateTime"))
                    {
                        if (valSrc.Length > 0 && valDest.Length > 0)
                        {
                            DateTime dtSrc = (DateTime)drSrc[dc.ColumnName];
                            DateTime dtDest = (DateTime)drDest[dc.ColumnName];

                            compare = dtSrc.CompareTo(dtDest);
                        }

                        if (compare != 0) break;
                        continue;
                    }

                    // 数字类型处理
                    if (dc.DataType == Type.GetType("System.Decimal"))
                    {
                        srcFloat = float.Parse(drSrc[dc.ColumnName].ToString());
                        destFloat = float.Parse(drDest[dc.ColumnName].ToString());

                        if (srcFloat > destFloat)
                        {
                            compare = 1;
                        }

                        if (srcFloat < destFloat)
                        {
                            compare = -1;
                        }

                        if (compare != 0) break;

                        continue;
                    }

                    // 字符串处理
                    compare = valSrc.CompareTo(valDest);

                    if (compare != 0) break;
                }
                #endregion


                //  HS  提示   这里程序的 流程是  所谓的 源就是  HIS的数据来源 。目标则是  我们库的数据。
                //三种情况 。当  源数据 小于 目标数据量的时候 对不起。 目标数据流 将被删除。一直到数据量和 源数据量相同
                //  当 源数据量 大于  目标数据链的额时候  这时候  要做的就是 把 源 数据 快素的保存到  目标数据表里面、
                //当  两个数据量相同的时候 才可以进行 处理。对比，查看 、2012年6月8号瓦房店椰海宾馆。

                #region 如果源小, 插入, 源前行一步
                if (compare < 0)
                {
                    DataRow drNew = dsDestModify.Tables[0].NewRow();

                    //foreach(DataColumn dc in dsDestModify.Tables[0].Columns)
                    foreach (DataColumn dc in dsSrc.Tables[0].Columns)
                    {
                        if (dc.ColumnName.ToUpper().IndexOf("_TIMESTAMP") < 0)
                        {
                            drNew[dc.ColumnName] = drSrc[dc.ColumnName];
                        }
                    }
                    dsDestModify.Tables[0].Rows.Add(drNew);

                    indexSrc++;
                }
                #endregion

                #region 如果源大, 删除目标; 源不动, 目标前行一步
                if (compare > 0)
                {
                    drDestsM[indexDest].Delete();

                    indexDest++;
                    continue;
                }
                #endregion

                #region 如果相同, 比较是否有变更, 源与目标都前行一步
                // 如果相同, 比较是否有变更, 源与目标都前行一步
                if (compare == 0)
                {
                    #region 比较两条记录是否相同
                    compare = 0;
                    //foreach(DataColumn dc in dsDestModify.Tables[0].Columns)
                    foreach (DataColumn dc in dsSrc.Tables[0].Columns)
                    {
                        if (dc.ColumnName.ToUpper().IndexOf("_TIMESTAMP") >= 0)
                        {
                            continue;
                        }

                        string valSrc = drSrc[dc.ColumnName].ToString();
                        string valDest = drDest[dc.ColumnName].ToString();

                        // 日期类型处理
                        if (dc.DataType == Type.GetType("System.DateTime"))
                        {
                            if (valSrc.Length > 0 && valDest.Length > 0)
                            {
                                DateTime dtSrc = (DateTime)drSrc[dc.ColumnName];
                                DateTime dtDest = (DateTime)drDest[dc.ColumnName];

                                compare = dtSrc.CompareTo(dtDest);

                                if (compare != 0) break;

                                continue;
                            }
                        }

                        // 数字类型处理
                        if (dc.DataType == Type.GetType("System.Decimal"))
                        {
                            srcFloat = 0;
                            destFloat = 0;

                            float.TryParse(drSrc[dc.ColumnName].ToString(), out srcFloat);
                            float.TryParse(drDest[dc.ColumnName].ToString(), out destFloat);
                            //srcFloat = float.Parse(drSrc[dc.ColumnName].ToString());
                            //destFloat = float.Parse(drDest[dc.ColumnName].ToString());

                            if (srcFloat > destFloat)
                            {
                                compare = 1;
                            }

                            if (srcFloat < destFloat)
                            {
                                compare = -1;
                            }

                            if (compare != 0) break;

                            continue;
                        }

                        if (compare == 0)
                        {
                            compare = valSrc.CompareTo(valDest);
                        }

                        if (compare != 0)
                        {
                            break;
                        }
                    }
                    #endregion

                    #region 如果两条记录不相同, 更新目标记录
                    if (compare != 0)
                    {
                        DataRow drEdit = drDestsM[indexDest];

                        //foreach(DataColumn dc in dsDestModify.Tables[0].Columns)
                        foreach (DataColumn dc in dsSrc.Tables[0].Columns)
                        {
                            if (dc.ColumnName.ToUpper().IndexOf("_TIMESTAMP") < 0)
                            {
                                // 不更新主键
                                bool blnPrim = false;
                                for (int k = 0; k < dcPrimarys.Length; k++)
                                {
                                    if (dcPrimarys[k].ColumnName.Equals(dc.ColumnName) == true)
                                    {
                                        blnPrim = true;
                                        break;
                                    }
                                }

                                if (blnPrim == false)
                                {
                                    drEdit[dc.ColumnName] = drSrc[dc.ColumnName];
                                }
                            }
                        }
                    }
                    #endregion

                    indexSrc++;
                    indexDest++;
                }
                #endregion
            }

            #region 剩余部份处理
            // 如果源剩余, 增加源的内容
            for (int k = indexSrc; k < drSrcs.Length; k++)
            {
                if (_blnExit) return null;

                DataRow drSrc = drSrcs[k];
                DataRow drNew = dsDestModify.Tables[0].NewRow();

                //foreach(DataColumn dc in dsDestModify.Tables[0].Columns)
                foreach (DataColumn dc in dsSrc.Tables[0].Columns)
                {
                    if (dc.ColumnName.ToUpper().IndexOf("_TIMESTAMP") < 0)
                    {
                        drNew[dc.ColumnName] = drSrc[dc.ColumnName];
                    }
                }
                dsDestModify.Tables[0].Rows.Add(drNew);
            }

            // 如果目标剩余, 删除目标的内容
            for (int k = indexDest; k < drDests.Length; k++)
            {
                if (_blnExit) return null;
                drDestsM[k].Delete();
            }
            #endregion

            // 保存
            if (dsDestModify.HasChanges() == true)
            {
                TraceInfo = "数据变更: " + dsDestModify.GetChanges().Tables[0].Rows.Count.ToString() + "条";

                DataSet dsChanged = dsDestModify.GetChanges().Copy();

                dsDestModify.Tables[0].TableName = tableNameDest;


                if (tableNameDest.ToUpper() == "ORDERS_M")
                {
                    string str = string.Join(",", dsDestModify.Tables[0].Select("", "", DataViewRowState.Added).AsEnumerable().Select(r => string.Format("('{0}','{1}','{2}','{3}')", r["patient_id"], r["visit_id"], r["order_no"], r["order_sub_no"])).ToArray());
                    if (!string.IsNullOrEmpty(str))
                    {
                        string sql = @"select * from orders_m t where (t.patient_id,t.visit_id,t.order_no,t.order_sub_no) in (" + str + ")";
                        DataSet repeatDs = _connMobile.SelectData(sql);
                        if (repeatDs != null && repeatDs.Tables.Count > 0 && repeatDs.Tables[0].Rows.Count > 0)
                        {
                            List<string> list = new List<string>();
                            foreach (DataRow row in repeatDs.Tables[0].Rows)
                            {
                                list.Add(string.Format("('{0}',{1},{2},{3})", row["patient_id"].ToString(), row["visit_id"].ToString(), row["order_no"].ToString(), row["order_sub_no"].ToString()));
                                DataRow[] rows = dsDestModify.Tables[0].Select(string.Format("patient_id='{0}' and  visit_id={1} and order_no={2} and order_sub_no={3}", row["patient_id"].ToString(), row["visit_id"].ToString(), row["order_no"].ToString(), row["order_sub_no"].ToString()));
                                if (rows.Length > 0)
                                    dsDestModify.Tables[0].Rows.Remove(rows[0]);
                            }
                            string errorMsg = "主键冲突：(patient_id,visit_id,order_no,order_sub_no) = " + string.Join(",", list.ToArray());
                            LogFile.WriteLog(errorMsg);
                           // TraceInfo = errorMsg; 只写日志 不显示
                        }
                    }
                }

                _connMobile.Update(ref dsDestModify);

                return dsChanged;
            }
            else
            {
                TraceInfo = "数据变更: 0 条";
                return null;
            }
        }


        /// <summary>
        /// 获取文件内容, 去掉文件中的注释
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string getSqlContent(string fileName)
        {
            // 条件检查
            if (fileName.IndexOf(".") < 0)
            {
                fileName += ".sql";
            }

            fileName = Path.Combine(Path.Combine(Application.StartupPath, "SQL"), fileName);

            if (File.Exists(fileName) == false)
            {
                throw new Exception(fileName + "不存在!");
            }

            // 获取文件内容
            StreamReader sr = new StreamReader(fileName);
            string content = sr.ReadToEnd();
            sr.Close();

            content = content.Replace("\r", string.Empty);
            content = content.Replace("\n", ComConst.STR.BLANK);

            // 截除注释
            int pos = content.IndexOf(@"//");
            if (pos >= 0)
            {
                content = content.Substring(0, pos);
            }

            // 替换病人列表参数数
            if (content.IndexOf("{PATIENT_LIST}") > 0 && pathData.Length > 0)
            {
                try
                {
                    fileName = Path.Combine(pathData, "PATIENT_INFO") + ".xml";
                    if (File.Exists(fileName) == false) return content;

                    DataSet ds = new DataSet();
                    ds.ReadXml(fileName, XmlReadMode.ReadSchema);
                    if (ds.Tables.Count == 0) return content;

                    StringBuilder sb = new StringBuilder();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (sb.Length == 0)
                            sb.Append("'");
                        else
                            sb.Append("','");

                        sb.Append(dr["PATIENT_ID"].ToString().Trim());
                    }

                    if (sb.Length > 0) sb.Append("'");

                    content = content.Replace("{PATIENT_LIST}", sb.ToString());
                }
                catch
                {
                    return content;
                }
            }

            return content;
        }


        /// <summary>
        /// 加载同步信息
        /// </summary>
        /// <returns></returns>
        public bool loadSyncInfo()
        {
            syncInfos.Clear();

            // 判断文件是否存在
            string fileName;
            if (string.IsNullOrEmpty(IniPath))
                fileName = Path.Combine(Path.Combine(Application.StartupPath, "SQL"), "SyncTableList.INI");
            else
                fileName = Path.Combine(Application.StartupPath, IniPath);

            if (File.Exists(fileName) == false)
            {
                throw new Exception(fileName + "不存在!");
            }

            // 获取文件内容
            StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("gb2312"));
            string content = sr.ReadToEnd();
            sr.Close();

            content = content.Replace("\n", ComConst.STR.BLANK);

            // 解析文件
            string[] parts = content.Split('\r');
            string line = string.Empty;
            SyncInfo syncItem = null;
            for (int i = 0; i < parts.Length; i++)
            {
                line = parts[i].Trim();

                // 注释
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
                    syncItem.Comment += syncItem.Filter;
                    continue;
                }
            }

            if (syncItem != null) syncInfos.Add(syncItem);

            return (syncInfos.Count > 0);
        }


        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        public bool loadConfigInfo()
        {
            try
            {
                pathData = GVars.IniFile.ReadString("DATA", "LOCATION", string.Empty).Trim();
                if (pathData.Length > 0 && Directory.Exists(pathData) == false)
                {
                    Directory.CreateDirectory(pathData);
                }
            }
            catch
            {
                pathData = string.Empty;
            }

            return true;
        }
        #endregion


        #region Oracle字符集处理
        private string oldNlslang = string.Empty;


        /// <summary>
        /// 设置字符集
        /// </summary>
        private void setMobileNlsLang()
        {
            // 不同字符集的处理
            string nlsLangKey = GVars.IniFile.ReadString("DATABASE", "ORA_NLS_LANG", string.Empty);

            if (nlsLangKey.Length > 0)
            {
                //_connMobile.KeepConnection = true;          // 保持数据库连接

                for (int i = 0; i < 6; i++)              // 针对西京医院 要多次连执接才会成功的情况设计
                {
                    try
                    {
                        oldNlslang = oracleNlsLang_Zh(nlsLangKey);
                        Application.DoEvents();
                        //_connMobile.SelectValue("SELECT SYSDATE FROM DUAL");
                        DataSet ds = _connMobile.SelectData("SELECT DEPT_NAME FROM DEPT_DICT WHERE ROWNUM = 1");
                        TraceInfo = ds.Tables[0].Rows[0]["DEPT_NAME"].ToString();
                        return;
                    }
                    catch (Exception ex)
                    {
                        TraceInfo = ex.Message;

                        Thread.Sleep(5 * 1000);        // 休息5秒
                    }
                    finally
                    {
                        oracleNlsLang_Restore(nlsLangKey, oldNlslang);
                    }
                }

                throw new Exception("设置字符集失败!");
            }
        }



        /// <summary>
        /// 设置字符集
        /// </summary>
        private void setHisNlsLang()
        {
            // 不同字符集的处理
            string nlsLangKey = GVars.IniFile.ReadString("DATABASE", "ORA_NLS_LANG", string.Empty);

            if (nlsLangKey.Length > 0)
            {
                //_oraHis.KeepConnection = true;          // 保持数据库连接

                for (int i = 0; i < 6; i++)              // 针对西京医院 要多次连执接才会成功的情况设计
                {
                    try
                    {
                        oldNlslang = oracleNlsLang_En(nlsLangKey);
                        Application.DoEvents();
                        DataSet ds = _oraHis.SelectData("SELECT DEPT_NAME FROM DEPT_DICT WHERE ROWNUM = 1");
                        //_oraHis.SelectValue("SELECT SYSDATE FROM DUAL");
                        TraceInfo = ds.Tables[0].Rows[0]["DEPT_NAME"].ToString();
                        return;
                    }
                    catch (Exception ex)
                    {
                        TraceInfo = ex.Message;

                        Thread.Sleep(5 * 1000);        // 休息5秒
                    }
                    finally
                    {
                        oracleNlsLang_Restore(nlsLangKey, oldNlslang);
                    }
                }

                throw new Exception("设置字符集失败!");
            }
        }


        /// <summary>
        /// 修改Oracle字符集为英文
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
        /// 修改Oracle字符集为中文
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
        /// 还原Oracle字符集
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
