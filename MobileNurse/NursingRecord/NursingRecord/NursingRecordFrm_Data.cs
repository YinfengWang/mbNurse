using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    partial class NursingRecordFrm : FormDo
    {
        #region 变量定义
        private const int           INTERVAL_SPAN   = 4;                // 最多相隔4分钟
        private const string        ORDERS_EXECUTE  = "+";              // 表示从医嘱执行单取值
        
        private DbInfo              dbAccess        = null;   
        private PatientDbI          patientDbI      = null;             // 病人信息接口        
        
        private DataSet             dsPatMaster     = null;             // 病人主记录
        private DataSet             dsPatVisit      = null;             // 病人就诊记录
        private DataSet             dsPatInpInfo    = null;             // 病人在院记录
        private DataSet             dsPatAdtLog     = null;             // 病人转科记录
        private DataSet             dsPatDiagnosis  = null;             // 病人诊断记录
        #endregion
        
        
        #region 共通函数
        /// <summary>
        /// 初始化窗体变量
        /// </summary>
        private void initFrmVal()
        {
            if (GVars.OracleAccess == null) return;

            patientDbI  = new PatientDbI(GVars.OracleAccess);
            dbAccess    = new DbInfo(GVars.OracleAccess);

            // 获取参数
            _userRight = GVars.User.GetUserFrmRight(_id);
            
            // 获取页面定义
            dsRecordDefine = dbAccess.GetTableData("NURSING_RECORD_DEFINE", "TYPE_ID = " + SQL.SqlConvert(_typeId));            
        }
        
        
        /// <summary>
        /// 获取参数
        /// </summary>
        private int getPageRows()
        {
            int rows = 23;

            string filter = "APP_CODE = " + SqlManager.SqlConvert(GVars.App.Right);
            DataSet ds = dbAccess.GetTableData("APP_CONFIG", filter);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string paramName    = dr["PARAMETER_NAME"].ToString().Trim();
                string paramVal     = dr["PARAMETER_VALUE"].ToString().Trim();

                switch (paramName)
                {
                    case "PRINT_PAGE_ROWS":         // 一般护理记录单 打印页行数
                        if (_typeId.Equals("01") == true)
                        {
                            int.TryParse(paramVal, out rows);
                        }

                        break;

                    case "PRINT_PAGE_ROWS_WZ":      // 危重护理记录单 打印行数
                        if (_typeId.Equals("02") == true)
                        {
                            int.TryParse(paramVal, out rows);
                        }

                        break;
                }
            }

            return rows;
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

                codeList.Add(dr["VITAL_CODE"].ToString(), dr["STORE_COL_NAME"].ToString());
            }

            return codeList;
        }
        
        
        /// <summary>
        /// 获取列宽
        /// </summary>
        /// <returns></returns>
        private int getColWidth(string colName)
        {
            foreach(DataRow dr in dsRecordDefine.Tables[0].Rows)
            {
                if (dr["STORE_COL_NAME"].ToString().Equals(colName) == true)
                {
                    return int.Parse(dr["COL_WIDTH"].ToString());
                }
            }
            
            return -1;
        }
        
        
        /// <summary>
        /// 是多行
        /// </summary>
        /// <returns></returns>
        private bool isMultiLine(string colName)
        {
            foreach(DataRow dr in dsRecordDefine.Tables[0].Rows)
            {
                if (dr["STORE_COL_NAME"].ToString().Equals(colName) == true)
                {
                    return dr["MULTI_LINE"].ToString().Equals("1");
                }
            }
            
            return false;
        }
        
                
        /// <summary>
        /// 获取总共打印行数
        /// </summary>
        /// <returns></returns>
        private int getLastPagePrintedRows(ref int lastPageIndex)
        {
            int printedRows = -1;
            string filter = "PATIENT_ID = " + SQL.SqlConvert(patientId)
                        + "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                        + "AND TYPE_ID = " + SQL.SqlConvert(_typeId);
            
            // 获取最后的页码
            string sql = "SELECT NVL(MAX(PAGE_INDEX),0) FROM NURSING_RECORD_PAGE_INFO WHERE " + filter;
            dbAccess.SelectValue(sql);
            lastPageIndex = int.Parse(dbAccess.GetResult(0));

            if (lastPageIndex == 0)             // 如果还没有开始打印
            {
                return 0;
            }
            
            // 获取最后页码打印的行数
            sql = "SELECT PRINTED_ROWS FROM NURSING_RECORD_PAGE_INFO "
                 + "WHERE PAGE_INDEX = " + SQL.SqlConvert(lastPageIndex.ToString()) 
                 +   "AND " + filter;
            
            dbAccess.SelectValue(sql);
            int.TryParse(dbAccess.GetResult(0), out printedRows);
            return printedRows;                 // 最后一页已打印行数
        }
        
        
        /// <summary>
        /// 获取最后的打印日期
        /// </summary>
        /// <returns></returns>
        private DateTime getLastPrintedDateTime()
        {
            string filter = "PATIENT_ID = " + SQL.SqlConvert(patientId)
                        + "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                        + "AND TYPE_ID = " + SQL.SqlConvert(_typeId)
                        + "AND PRINT_FLG = '1' ";
                        
            string sql = "SELECT MAX(RECORD_DATE) RECORD_DATE FROM NURSING_RECORD_CONTENT WHERE " + filter;
            DataSet ds = dbAccess.GetData(sql, "NURSING_RECORD_CONTENT");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][COL_RECORD_DATE] != DBNull.Value)
                {
                    return (DateTime)(ds.Tables[0].Rows[0][COL_RECORD_DATE]);
                }
            }
            
            return DateTime.MinValue;
        }
        
        
        /// <summary>
        /// 从生命体征中生成护理记录
        /// </summary>
        /// <returns></returns>
        private bool addNewNursingRecord_VitalSigns(DateTime dtStart)
        {
            bool        dataGot     = false;                        // 是否获取到新的护理记录
            string      vitalCode   = string.Empty;                 // 生命体征代码
            string      colName     = string.Empty;                 // 列名
            bool        hasData     = false;                        // 是否有数据
            
            DateTime    dtCurrent   = dtStart;                      // 当前时间
            int         idx_rec     = 0;                            // 记录索引
            DataRow     drEdit      = null;                         // 当前编辑记录
            
            // 获取生命体征数据
            string filter = "PATIENT_ID = " + SQL.SqlConvert(patientId)
                  + "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                  + "AND TIME_POINT > " + SQL.GetOraDbDate(dtStart);
            DataSet ds = dbAccess.GetTableData("VITAL_SIGNS_REC", filter);
            DataRow[] drFind = ds.Tables[0].Select(string.Empty, "TIME_POINT");
            
            // 从生命体征数据中生成护理记录
            while(idx_rec < drFind.Length)
            {
                vitalCode = drFind[idx_rec]["VITAL_CODE"].ToString();
                
                // 如果不是需要的生命体征
                if (vitalCodeList.Contains(vitalCode) == false || vitalCode.Equals(ORDERS_EXECUTE))
                {
                    idx_rec++;
                    continue;
                }
                
                // 获取对应的存储字段名称与当前时间点
                colName     = vitalCodeList[vitalCode].ToString();
                dtCurrent   = (DateTime)(drFind[idx_rec]["TIME_POINT"]);
                dtCurrent   = dtCurrent.AddSeconds(dtCurrent.Second);
                
                // 如果是当前记录的第一个数据
                if (hasData == false)
                {
                    drEdit = dsRecordContent.Tables[0].NewRow();
                    drEdit["ROW_INDEX"]     = 1;
                    drEdit["PATIENT_ID"]    = patientId;
                    drEdit["VISIT_ID"]      = visitId;
                    drEdit["TYPE_ID"]       = _typeId;
                    drEdit[COL_RECORD_DATE] = dtCurrent;
                    drEdit[colName]         = drFind[idx_rec]["VITAL_SIGNS_CVALUES"].ToString();
                    
                    if (colSign.Length > 0)
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
            int         idx_rec         = 0;                            // 记录索引
            DataRow     drEdit          = null;                         // 当前编辑记录
            int         row_Index       = 0;                            // 同一个时间中行的序号
            string      orderNoPre      = string.Empty;                 // 前一个医嘱号
            
            // 获取各项列名
            string      colName_Text    = string.Empty;                 // 列名 内容
            string      colName_Amount  = string.Empty;                 // 列名 数量
            string      colName_Group   = string.Empty;                 // 列名 分组
            
            if (getColName_OrdersExecute(ref colName_Text, ref colName_Amount, ref colName_Group) == false)
            {
                throw new Exception("护理记录单定义表中关于医嘱执行单的配置有误!");
            }
            
            // 获取医嘱执行单
            string filter = "PATIENT_ID = " + SQL.SqlConvert(patientId)  + "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                        + "AND (ORDER_CLASS = 'A' OR ORDER_CLASS = 'B') AND DOSAGE_UNITS = 'ml' "
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
                DateTime dtRecEnd   = dtExecute.AddMinutes(INTERVAL_SPAN / 2);
                
                filter = "PATIENT_ID = " + SQL.SqlConvert(patientId)  + "AND VISIT_ID = " + SQL.SqlConvert(visitId)
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

                    drEdit["ROW_INDEX"]     = row_Index;
                    drEdit["PATIENT_ID"]    = patientId;
                    drEdit["VISIT_ID"]      = visitId;
                    drEdit["TYPE_ID"]       = _typeId;
                    drEdit[COL_RECORD_DATE] = dtExecute;
                    
                    if (colSign.Length > 0)
                    {
                        drEdit[colSign] = GVars.User.Name;
                    }
                    
                    dtStart = (DateTime)(drEdit[COL_RECORD_DATE]);
                    
                    dsRecordContent.Tables[0].Rows.Add(drEdit);
                }
                
                drEdit[colName_Text]    = drFind[idx_rec]["ORDER_TEXT"].ToString();
                drEdit[colName_Amount]  = drFind[idx_rec]["DOSAGE"].ToString();
                if (colName_Group.Length > 0) drEdit[colName_Group]   = string.Empty;   // 分组信息

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
                        drEdit[colName_Group]   = GROUP_BEGIN;                          // 分组信息
                    }
                    
                    // 新增其它记录
                    drEdit = dsRecordContent.Tables[0].NewRow();

                    drEdit["ROW_INDEX"]     = (++row_Index);
                    drEdit["PATIENT_ID"]    = patientId;
                    drEdit["VISIT_ID"]      = visitId;
                    drEdit["TYPE_ID"]       = _typeId;
                    drEdit[COL_RECORD_DATE] = dtTemp;
                    
                    if (colSign.Length > 0)
                    {
                        drEdit[colSign] = GVars.User.Name;
                    }
                    
                    drEdit[colName_Text]    = drFind[idx_rec]["ORDER_TEXT"].ToString();
                    drEdit[colName_Amount]  = drFind[idx_rec]["DOSAGE"].ToString();
                    if (colName_Group.Length > 0) drEdit[colName_Group]   = GROUP_IN;   // 分组信息
                    dsRecordContent.Tables[0].Rows.Add(drEdit);
                    
                    orderNoPre = drFind[idx_rec]["ORDER_NO"].ToString();
                    
                    idx_rec++;
                }
                
                if (colName_Group.Length > 0 && drEdit[colName_Group].ToString().Equals(GROUP_IN)) 
                {
                    drEdit[colName_Group]   = GROUP_END;                                // 分组信息
                }
            }
            
            return (drFind.Length > 0);
        }
        
        
        /// <summary>
        /// 获取最近护理记录时间
        /// </summary>
        /// <returns></returns>
        private DateTime getLastNursingRecordDateTime()
        {
            // 获取时间开始点
            DateTime dtStart = DateTime.MinValue;
            DataSet ds       = null;
            
            string filter = string.Empty;
            DataRow[] drFind = dsRecordContent.Tables[0].Select(filter, "RECORD_DATE DESC");
            if (drFind.Length > 0)
            {
                dtStart = (DateTime)drFind[0][COL_RECORD_DATE];
            }
            
            // 数据库中的最新时间点
            filter = "PATIENT_ID = " + SQL.SqlConvert(patientId)
                        + "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                        + "AND TYPE_ID = " + SQL.SqlConvert(_typeId)
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
        /// 获取医嘱执行单对应的列名
        /// </summary>
        /// <returns></returns>
        private bool getColName_OrdersExecute(ref string colNameOrderText, ref string colNameAmount, ref string colNameGroup)
        {
            colNameOrderText = vitalCodeList[ORDERS_EXECUTE].ToString();
            DataRow[] drFind = dsRecordDefine.Tables[0].Select(string.Empty, "SERIAL_NO");
            
            bool blnFind = false;
            
            for(int i = 0; i < drFind.Length; i++)
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
        #endregion
        
        
        #region 数据交互
        /// <summary>
        /// 获取护理记录
        /// </summary>
        /// <returns></returns>
        private DataSet getNursingRecord(string typeId, string patientId, string visitId, DateTime dtBegin, DateTime dtEnd)
        {
            // 获取记录
            string filter = "TYPE_ID = " + SQL.SqlConvert(typeId) 
                            + " AND PATIENT_ID = " + SQL.SqlConvert(patientId) 
                            +  " AND VISIT_ID = " + SQL.SqlConvert(visitId) 
                            + " AND RECORD_DATE >= " + SQL.GetOraDbDate_Short(dtBegin.Date)
                            + " AND RECORD_DATE < " + SQL.GetOraDbDate_Short(dtEnd.Date.AddDays(1));
            return dbAccess.GetTableData("NURSING_RECORD_CONTENT", filter);
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
        /// 获取页面信息
        /// </summary>
        /// <returns></returns>
        private DataSet getPageInfo()
        {
            // 获取页面起始日期
            string filter = "TYPE_ID = " + SQL.SqlConvert(_typeId) 
                        + " AND PATIENT_ID = " + SQL.SqlConvert(patientId) 
                        +  " AND VISIT_ID = " + SQL.SqlConvert(visitId);

            return dbAccess.GetTableData("NURSING_RECORD_PAGE_INFO", filter);
        }

        
        /// <summary>
        /// 获取病人历史信息
        /// </summary>
        private void getPatientHistoryInfo()
        {
            string filter = "PATIENT_ID = " + SQL.SqlConvert(patientId);
            dsPatMaster = dbAccess.GetTableData("PAT_MASTER_INDEX", filter);        // 病人主记录

            filter += "AND VISIT_ID = " + SQL.SqlConvert(visitId);
            dsPatVisit      = dbAccess.GetTableData("PAT_VISIT", filter);           // 病人就诊记录
            dsPatInpInfo    = dbAccess.GetTableData("PATS_IN_HOSPITAL", filter);    // 病人在院记录
            dsPatAdtLog     = dbAccess.GetTableData("ADT_LOG", filter);             // 病人转科记录
            dsPatDiagnosis  = dbAccess.GetTableData("DIAGNOSIS", filter);           // 病人诊断记录
        }                
        #endregion
    }
}
