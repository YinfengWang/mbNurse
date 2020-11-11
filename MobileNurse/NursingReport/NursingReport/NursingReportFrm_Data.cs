using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    partial class NursingReportFrm : FormDo
    {
        private const int INTERVAL_SPAN = 4;           // 最多相隔4分钟
        private const string ORDERS_EXECUTE = "+";      // 表示从医嘱执行单取值

        private DbInfo dbAccess = null;
        private PatientDbI patientDbI = null;

       

        /// <summary>
        /// 初始化窗体变量
        /// </summary>
        private void InitFromVal()
        {
            dbAccess = new DbInfo(GVars.OracleAccess);
            patientDbI = new PatientDbI(GVars.OracleAccess);
        }


        private DataSet getNrusingReocrdDefine(string typeId)
        {
            return dbAccess.GetTableData("NURSING_RECORD_DEFINE", "TYPE_ID = " + SQL.SqlConvert(typeId));
        }

        /// <summary>
        /// 获取护理记录类别
        /// </summary>
        /// <returns></returns>
        private DataSet getNursingRecordType()
        {
            StringBuilder filter = new StringBuilder();
            filter.Append("DEPT_CODE LIKE '%" + GVars.User.DeptCode + "%'");
            filter.Append(" OR DEPT_CODE IS NULL");
            filter.Append(" AND TYPE_ID NOT IN('01','02','03','04','05','06','07')");
            return dbAccess.GetTableData("NURSING_RECORD_TYPE", filter.ToString());
        }


        /// <summary>
        /// 获取护理打印分页标识
        /// </summary>
        /// <param name="wardCode"></param>
        /// <returns></returns>
        private DataSet getNursingPrintSkipFlg(string wardCode)
        {
            string filter = "WARD_CODE=" + SQL.SqlConvert(wardCode);
            return dbAccess.GetTableData("NURSING_RECORD_PRINT_SKIP_FLG", filter);
        }

        ///<summary>
        /// 获取护理记录
        /// </summary>
        ///<returns></returns>
        private DataSet getNursingRecord(string typeId, string patientId, string visitId, DateTime dtBegin, DateTime dtEnd)
        {
            DataSet ds = new DataSet();
            //获取记录
            StringBuilder filter = new StringBuilder();
            filter.Append("TYPE_ID = " + SQL.SqlConvert(typeId));
            filter.Append(" AND PATIENT_ID = " + SQL.SqlConvert(patientId));
            filter.Append(" AND VISIT_ID = " + SQL.SqlConvert(visitId));
            filter.Append(" AND RECORD_DATE >= " + SQL.GetOraDbDate_Short(dtBegin.Date));
            filter.Append(" AND RECORD_DATE < " + SQL.GetOraDbDate_Short(dtEnd.Date.AddDays(1)));

            string strSql = @"SELECT PATIENT_ID, VISIT_ID, TYPE_ID, RECORD_DATE, COL1, COL2, COL3, COL4, COL5, COL6, COL7, 
                                COL8, COL9, COL10, COL11, COL12, COL13, COL14, COL15, COL16, COL17, COL18, COL19, COL20, ROW_COUNT, 
                                PRINT_FLG, PAGE_BREAK, ROW_INDEX, COL33, COL36, COL23, COL21, COL24, COL25, COL40, COL22, NURSE_SYS_NAME, 
                                COL26, COL27, COL28, COL29, COL30, COL31, 
                                COL32, COL34, COL35, COL37, COL38, COL39, COL41, 
                                DECODE(CHECK_FLAG,'0','未审核','1','审核未通过','2','审核通过','3','待审核','未审核') AS CHECK_FLAG 
                                FROM NURSING_RECORD_CONTENT";
            if (filter.ToString().Trim().Length > 0)
            {
                strSql += " WHERE " + filter;
            }
            return dbAccess.GetTableDataBySQL(strSql, "NURSING_RECORD_CONTENT");
            //DataTable dt = dbHelp.GetData(strSql);
            //ds.Tables.Add(dt);
           // return ds;
        }

        private DataSet getNursingRecordPageInfo(string typeId, int pageStart, int pageEnd)
        {
            StringBuilder filter = new StringBuilder();
            filter.Append("TYPE_ID = " + SQL.SqlConvert(typeId));
            filter.Append(" AND PAGE_INDEX >= " + SQL.SqlConvert(pageStart.ToString()));
            filter.Append(" AND PAGE_INDEX <= " + SQL.SqlConvert(pageEnd.ToString()));
            return dbAccess.GetTableData("NURSING_RECORD_PAGE_INFO", filter.ToString());

        }

        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <returns></returns>
        private DataSet getPatientInfo(string patientId, string visitId)
        {
            DataSet ds = patientDbI.GetInpPatientInfo_FromID(patientId, visitId);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                ds = patientDbI.GetPatientInfo_FromID(patientId);
            }

            return ds;
        }

        /// <summary>
        /// 获取最近护理记录时间
        /// </summary>
        /// <returns></returns>
        private DateTime getLastNursingRecordDateTime()
        {
            // 获取时间开始点
            DateTime dtStart = DateTime.MinValue;
            DataSet ds = null;

            string filter = string.Empty;
            DataRow[] drFind = dsRecordContent.Tables[0].Select(filter, "RECORD_DATE DESC");
            if (drFind.Length > 0)
            {
                dtStart = (DateTime)drFind[0][COL_RECORD_DATE];
            }

            // 数据库中的最新时间点
            filter = "PATIENT_ID = " + SQL.SqlConvert(patientId)
                        + "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                        + "AND TYPE_ID = " + SQL.SqlConvert(typeId)
                        + "AND RECORD_DATE > " + SQL.GetOraDbDate(dtStart);
            string sql = "SELECT MAX(RECORD_DATE) FROM NURSING_RECORD_CONTENT WHERE " + filter;

            ds = dbAccess.GetData(sql, "NURSING_RECORD_CONTENT");

            if (ds.Tables[0].Rows[0][0] != DBNull.Value)
            {
                dtStart = (DateTime)(ds.Tables[0].Rows[0][0]);
            }

            return dtStart;
        }


        /// <summary>
        /// 从生命体征中生成护理记录
        /// </summary>
        /// <returns></returns>
        private bool addNewNursingRecord_VitalSigns(DateTime dtStart)
        {
            bool dataGot = false;                        // 是否获取到新的护理记录
            string vitalCode = string.Empty;                 // 生命体征代码
            string colName = string.Empty;                 // 列名
            bool hasData = false;                        // 是否有数据

            DateTime dtCurrent = dtStart;                      // 当前时间
            int idx_rec = 0;                            // 记录索引
            DataRow drEdit = null;                         // 当前编辑记录

            // 获取生命体征数据
            string filter = "PATIENT_ID = " + SQL.SqlConvert(patientId)
                  + "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                  + "AND TIME_POINT > " + SQL.GetOraDbDate(dtStart);
            DataSet ds = dbAccess.GetTableData("VITAL_SIGNS_REC", filter);
            DataRow[] drFind = ds.Tables[0].Select(string.Empty, "TIME_POINT");

            // 从生命体征数据中生成护理记录
            while (idx_rec < drFind.Length)
            {
                vitalCode = drFind[idx_rec]["VITAL_CODE"].ToString();

                // 如果不是需要的生命体征
                if (vitalCodeList.Contains(vitalCode) == false || vitalCode.Equals(ORDERS_EXECUTE))
                {
                    idx_rec++;
                    continue;
                }

                // 获取对应的存储字段名称与当前时间点
                colName = vitalCodeList[vitalCode].ToString();
                dtCurrent = (DateTime)(drFind[idx_rec]["TIME_POINT"]);
                dtCurrent = dtCurrent.AddSeconds(dtCurrent.Second);

                // 如果是当前记录的第一个数据
                if (hasData == false)
                {
                    drEdit = dsRecordContent.Tables[0].NewRow();
                    drEdit["ROW_INDEX"] = 1;
                    drEdit["PATIENT_ID"] = patientId;
                    drEdit["VISIT_ID"] = visitId;
                    drEdit["TYPE_ID"] = typeId;
                    drEdit[COL_RECORD_DATE] = dtCurrent;
                    drEdit[colName] = drFind[idx_rec]["VITAL_SIGNS_CVALUES"].ToString();

                    if (colSign != null && colSign.Length > 0)
                    {
                        drEdit[colSign] = GVars.User.Name;
                    }

                    dtStart = (DateTime)(drEdit[COL_RECORD_DATE]);

                    idx_rec++;
                    hasData = true;
                    continue;
                }

                // 如果不是当前记录的第一个数据 或者时间跨度小于5分钟
                if (drEdit[colName].ToString().Length == 0 && dtCurrent.CompareTo(dtStart.AddMinutes(INTERVAL_SPAN)) < 0)
                {
                    drEdit[colName] = drFind[idx_rec]["VITAL_SIGNS_CVALUES"].ToString();
                    idx_rec++;
                }
                // 如果需要新增一行护理记录
                else
                {
                    if (hasData == true)
                    {
                        dsRecordContent.Tables[0].Rows.Add(drEdit);
                        dataGot = true;
                    }

                    hasData = false;
                }
            }

            if (hasData == true)
            {
                dsRecordContent.Tables[0].Rows.Add(drEdit);
                dataGot = true;
            }

            return dataGot;
        }

        /// <summary>
        /// 从医嘱执行单中生成护理记录
        /// </summary>
        /// <returns></returns>
        private bool addNewNursingRecord_OrdersExecute(DateTime dtStart)
        {
            // 条件检查
            if (vitalCodeList.Contains(ORDERS_EXECUTE) == false) return false;

            // 正常处理
            int idx_rec = 0;                            // 记录索引
            DataRow drEdit = null;                         // 当前编辑记录
            int row_Index = 0;                            // 同一个时间中行的序号
            string orderNoPre = string.Empty;                 // 前一个医嘱号

            // 获取各项列名
            string colName_Text = string.Empty;                 // 列名 内容
            string colName_Amount = string.Empty;                 // 列名 数量
            string colName_Group = string.Empty;                 // 列名 分组

            if (getColName_OrdersExecute(ref colName_Text, ref colName_Amount, ref colName_Group) == false)
            {
                throw new Exception("护理记录单定义表中关于医嘱执行单的配置有误!");
            }

            // 获取医嘱执行单
            string filter = "PATIENT_ID = " + SQL.SqlConvert(patientId) + "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                        + "AND (ORDER_CLASS = 'A' OR ORDER_CLASS = '1') AND RTRIM(DOSAGE_UNITS) = 'ml' "
                        + "AND EXECUTE_DATE_TIME > " + SQL.GetOraDbDate(dtStart);
            DataSet ds = dbAccess.GetTableData("ORDERS_EXECUTE", filter);
            DataRow[] drFind = ds.Tables[0].Select(string.Empty, "EXECUTE_DATE_TIME, ORDER_NO, ORDER_SUB_NO");

            // 从医嘱执行单中生成护理记录
            while (idx_rec < drFind.Length)
            {
                // 获取医嘱执行时间
                string orderNo = drFind[idx_rec]["ORDER_NO"].ToString();                // 医嘱号
                DateTime dtExecute = (DateTime)(drFind[idx_rec]["EXECUTE_DATE_TIME"]);  // 医嘱执行时间
                dtExecute = dtExecute.AddSeconds(dtExecute.Second * -1);                // 去掉秒

                // 查找该时间点附近有没有护理记录(前后两分钟)
                drEdit = null;

                DateTime dtRecBegin = dtExecute.AddMinutes(INTERVAL_SPAN / 2 * -1);
                DateTime dtRecEnd = dtExecute.AddMinutes(INTERVAL_SPAN / 2);

                filter = "PATIENT_ID = " + SQL.SqlConvert(patientId) + "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                        + "AND " + COL_RECORD_DATE + " >= " + SQL.SqlConvert(dtRecBegin.ToString(ComConst.FMT_DATE.LONG))
                        + "AND " + COL_RECORD_DATE + " <= " + SQL.SqlConvert(dtRecEnd.ToString(ComConst.FMT_DATE.LONG));
                DataRow[] drNursingRec = dsRecordContent.Tables[0].Select(filter, COL_RECORD_DATE + " DESC, ROW_INDEX DESC");

                if (drNursingRec.Length > 0)
                {
                    drEdit = drNursingRec[0];
                }

                // 查找最大行号
                row_Index = 1;
                if (drEdit != null)
                {
                    int.TryParse(drEdit["ROW_INDEX"].ToString(), out row_Index);
                    row_Index++;

                    // 如果医嘱号不相同
                    if (orderNoPre.Length > 0 && orderNoPre.Equals(orderNo) == false)
                    {
                        drEdit = null;
                    }
                }

                // 生成护理记录
                if (drEdit == null)
                {
                    drEdit = dsRecordContent.Tables[0].NewRow();

                    drEdit["ROW_INDEX"] = row_Index;
                    drEdit["PATIENT_ID"] = patientId;
                    drEdit["VISIT_ID"] = visitId;
                    drEdit["TYPE_ID"] = typeId;
                    drEdit[COL_RECORD_DATE] = dtExecute;

                    if (colSign != null && colSign.Length > 0)
                    {
                        drEdit[colSign] = GVars.User.Name;
                    }

                    dtStart = (DateTime)(drEdit[COL_RECORD_DATE]);

                    dsRecordContent.Tables[0].Rows.Add(drEdit);
                }

                drEdit[colName_Text] = drFind[idx_rec]["ORDER_TEXT"].ToString();
                drEdit[colName_Amount] = drFind[idx_rec]["DOSAGE"].ToString();
                if (colName_Group.Length > 0) drEdit[colName_Group] = string.Empty;   // 分组信息

                orderNoPre = drFind[idx_rec]["ORDER_NO"].ToString();
                idx_rec++;

                // 一组医嘱中其它记录的处理
                while (idx_rec < drFind.Length)
                {
                    DateTime dtTemp = (DateTime)(drFind[idx_rec]["EXECUTE_DATE_TIME"]);
                    dtTemp = dtTemp.AddSeconds(dtTemp.Second * -1);                // 去掉秒
                    if (dtExecute.CompareTo(dtTemp) != 0)
                    {
                        break;
                    }

                    if (orderNo.Equals(drFind[idx_rec]["ORDER_NO"].ToString()) == false)
                    {
                        break;
                    }

                    // 前一记录的分组信息补充
                    if (colName_Group.Length > 0 && drEdit[colName_Group].ToString().Length == 0)
                    {
                        drEdit[colName_Group] = GROUP_BEGIN;                          // 分组信息
                    }

                    // 新增其它记录
                    drEdit = dsRecordContent.Tables[0].NewRow();

                    drEdit["ROW_INDEX"] = (++row_Index);
                    drEdit["PATIENT_ID"] = patientId;
                    drEdit["VISIT_ID"] = visitId;
                    drEdit["TYPE_ID"] = typeId;
                    drEdit[COL_RECORD_DATE] = dtTemp;

                    if (colSign != null && colSign.Length > 0)
                    {
                        drEdit[colSign] = GVars.User.Name;
                    }

                    drEdit[colName_Text] = drFind[idx_rec]["ORDER_TEXT"].ToString();
                    drEdit[colName_Amount] = drFind[idx_rec]["DOSAGE"].ToString();
                    if (colName_Group.Length > 0) drEdit[colName_Group] = GROUP_IN;   // 分组信息
                    dsRecordContent.Tables[0].Rows.Add(drEdit);

                    orderNoPre = drFind[idx_rec]["ORDER_NO"].ToString();

                    idx_rec++;
                }

                if (colName_Group.Length > 0 && drEdit[colName_Group].ToString().Equals(GROUP_IN))
                {
                    drEdit[colName_Group] = GROUP_END;                                // 分组信息
                }
            }

            return (drFind.Length > 0);
        }

        /// <summary>
        /// 获取医嘱执行单对应的列名
        /// </summary>
        /// <returns></returns>
        private bool getColName_OrdersExecute(ref string colNameOrderText, ref string colNameAmount, ref string colNameGroup)
        {
            colNameOrderText = vitalCodeList[ORDERS_EXECUTE].ToString();
            DataRow[] drFind = dsRecordDefine.Tables[0].Select(string.Empty, "SERIAL_NO");

            bool blnFind = false;

            for (int i = 0; i < drFind.Length; i++)
            {
                DataRow dr = drFind[i];

                if (blnFind == false)
                {
                    blnFind = dr["STORE_COL_NAME"].ToString().Equals(colNameOrderText);
                    continue;
                }

                if (colNameAmount.Length > 0)
                {
                    colNameGroup = dr["STORE_COL_NAME"].ToString();
                    return true;
                }
                colNameAmount = dr["STORE_COL_NAME"].ToString();
            }
            return (colNameOrderText.Length > 0 && colNameAmount.Length > 0);
        }

        /// <summary>
        /// 获取需要提取的生命体征代码
        /// </summary>
        /// <returns></returns>
        private Hashtable getVitalCodeList()
        {
            Hashtable codeList = new Hashtable();
            foreach (DataRow dr in dsRecordDefine.Tables[0].Rows)
            {
                if (dr["VITAL_CODE"].ToString().Length == 0)
                {
                    continue;
                }
                if (!codeList.Contains(dr["VITAL_CODE"].ToString()))
                    codeList.Add(dr["VITAL_CODE"].ToString(), dr["STORE_COL_NAME"].ToString());
            }
            return codeList;
        }

        /// <summary>
        /// 根据类型获取页面打印记录
        /// </summary>
        /// <returns></returns>
        private DataSet getPageInfoFromType()
        {
            // 获取页面起始日期
            StringBuilder filter = new StringBuilder();
            filter.Append("TYPE_ID = " + SQL.SqlConvert(typeId));
            filter.Append(" AND PATIENT_ID = " + SQL.SqlConvert(patientId));
            filter.Append(" AND VISIT_ID = " + SQL.SqlConvert(visitId));
            return dbAccess.GetTableData("NURSING_RECORD_PRINT_INFO", filter.ToString());
        }

        /// <summary>
        /// 根据患者获取页面打印记录
        /// </summary>
        /// <returns></returns>
        private DataSet getPageInfoFromPatient(string type_Id = null)
        {
            StringBuilder filter = new StringBuilder();
            filter.Append("PATIENT_ID=" + SQL.SqlConvert(patientId));
            filter.Append(" AND VISIT_ID=" + SQL.SqlConvert(visitId));
            if (!string.IsNullOrEmpty(type_Id))
                filter.Append(" AND TYPE_ID=" + SQL.SqlConvert(type_Id));
            return dbAccess.GetTableData("NURSING_RECORD_PRINT_INFO", filter.ToString());
        }
    }
}
