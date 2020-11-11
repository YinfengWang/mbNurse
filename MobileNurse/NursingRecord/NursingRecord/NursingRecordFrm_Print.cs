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
        private const string        STR_START_POS   = "START_POS";
                
        private ExcelAccess         excelAccess     = new ExcelAccess();
                
        private List<ExcelItem>     arrSingle       = new List<ExcelItem>();
        private List<ExcelItem>     arrRepeat       = new List<ExcelItem>();

        private int                 pageRows        = 23;                       // 一页多少行
        private Hashtable           hasPrintInfo    = new Hashtable();          // 打印信息
        
        private PrintModeSelectFrm  reprintModeFrm  = new PrintModeSelectFrm(); // 重打模式选择
        #endregion
        
        
        struct ExcelItem
        {
            public int    Row;
            public int    Col;
            public string ItemId;
            public string CheckValue;
        }
        
        
        #region 打印
        /// <summary>
        /// 全部打印
        /// </summary>
        private bool printWhole()
        {
            // 加载excel节点
            loadExcelItem(_template + _typeId);
            
            // 获取已打印的行数
            int lastPrintedPage = 0;
            int printedRows = 0;
            
            // 获取最后打印时间点            
            DateTime dtLastPrinted = DateTime.MinValue;
            
            // 获取要打印的记录
            string filter = "PATIENT_ID = " + SQL.SqlConvert(patientId)
                        + "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                        + "AND TYPE_ID = " + SQL.SqlConvert(_typeId);
            //            + "AND RECORD_DATE > " + SQL.GetOraDbDate(dtLastPrinted);
            DataSet dsWaitPrint = dbAccess.GetTableData("NURSING_RECORD_CONTENT", filter);
            
            DataRow[] drWaitPrint = dsWaitPrint.Tables[0].Select(string.Empty, "RECORD_DATE ASC");
            if (drWaitPrint.Length == 0)
            {
                GVars.Msg.MsgId = "I0011";                              // 没有{0}!
                GVars.Msg.MsgContent.Add("记录");
                return false;
            }
            
            // 进行打印
            int idx_rec       = 0;
            
            // 如果未满页, 进行继打
            if (printedRows > 0)
            {
                excelOpen(printedRows);                
                ExcelTemplatePrint(ref drWaitPrint, ref idx_rec, ref printedRows);
                excelPrint();
            }
            
            while(idx_rec < drWaitPrint.Length)
            {
                lastPrintedPage++;
                printedRows = 0;
                
                excelOpen(printedRows);
                DateTime dtTimePoint = (DateTime)(drWaitPrint[idx_rec][COL_RECORD_DATE]);
                excelPrintFixedInfo(dtTimePoint, lastPrintedPage);                              // 输出固定项目
                ExcelTemplatePrint(ref drWaitPrint, ref idx_rec, ref printedRows);
                excelPrint();
            }
            
            return true;
        }

                
        /// <summary>
        /// 续打
        /// </summary>
        private bool printContinue(ref DataSet dsWaitPrint)
        {
            // 加载excel节点
            loadExcelItem(_template + _typeId);
            
            // 获取最后打印的页码, 在最后一页上已打印的行数
            int lastPrintedPage = 0;
            int printedRows = getLastPagePrintedRows(ref lastPrintedPage);
            
            // 获取最后打印时间点            
            DateTime dtLastPrinted = getLastPrintedDateTime();
            
            // 获取要打印的记录
            string filter = "PATIENT_ID = " + SQL.SqlConvert(patientId)
                        + "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                        + "AND TYPE_ID = " + SQL.SqlConvert(_typeId)
                        + "AND RECORD_DATE > " + SQL.GetOraDbDate(dtLastPrinted);
            dsWaitPrint = dbAccess.GetTableData("NURSING_RECORD_CONTENT", filter);
            // dsWaitPrint.WriteXml(@"d:\dsWaitPrint.xml", XmlWriteMode.WriteSchema);
            //dsWaitPrint.ReadXml(@"d:\dsWaitPrint.xml", XmlReadMode.ReadSchema);

            DataRow[] drWaitPrint = dsWaitPrint.Tables[0].Select(string.Empty, "RECORD_DATE ASC, ROW_INDEX ASC");
            if (drWaitPrint.Length == 0)
            {
                GVars.Msg.MsgId = "I0011";                                                          // 没有{0}!
                GVars.Msg.MsgContent.Add("续打记录");
                return false;
            }
            
            // 进行打印
            int idx_rec       = 0;
            
            // 如果未满页, 进行继打
            if (printedRows > 0)
            {
                excelOpen(printedRows);
                int idx_rec_first = idx_rec;
                ExcelTemplatePrint(ref drWaitPrint, ref idx_rec, ref printedRows);
                excelPrint();
                savePageInfo(lastPrintedPage, ref drWaitPrint, idx_rec_first, printedRows);         // 保存页面信息
            }
            
            while(idx_rec < drWaitPrint.Length)
            {
                lastPrintedPage++;
                printedRows = 0;
                
                excelOpen(printedRows);
                DateTime dtTimePoint = (DateTime)(drWaitPrint[idx_rec][COL_RECORD_DATE]);
                excelPrintFixedInfo(dtTimePoint, lastPrintedPage);                                  // 输出固定项目
                
                int idx_rec_first = idx_rec;
                ExcelTemplatePrint(ref drWaitPrint, ref idx_rec, ref printedRows);
                excelPrint();
                
                savePageInfo(lastPrintedPage, ref drWaitPrint, idx_rec_first, printedRows);         // 保存页面信息
            }
            
            return true;
        }
        
        
        /// <summary>
        /// 指定页码原样重打
        /// </summary>
        /// <returns></returns>
        private bool reprint(int pageStart, int pageEnd)
        {
            // 加载excel节点
            loadExcelItem(_template + _typeId);
            LogFile.WriteLog("加载excel节点");
            
            // 获取页面信息
            string filter = "TYPE_ID = " + SQL.SqlConvert(_typeId) 
                          + "AND PAGE_INDEX >= " + SQL.SqlConvert(pageStart.ToString()) 
                          + "AND PAGE_INDEX <= " + SQL.SqlConvert((pageEnd + 1).ToString());
            
            DataSet dsPageInfo = dbAccess.GetTableData("NURSING_RECORD_PAGE_INFO", filter);
            DataRow[] drPageInfos = dsPageInfo.Tables[0].Select(string.Empty, "PAGE_INDEX");
            LogFile.WriteLog("获取页面信息" + drPageInfos.Length.ToString());
            if (drPageInfos.Length == 0)
            {
                GVars.Msg.MsgId = "I0012";      // 指定的页面不存在!
                return false;
            }
            
            // 获取护理记录
            DateTime dtStart = (DateTime)(drPageInfos[0]["FIRST_REC_DATE"]);
            DateTime dtEnd   = (DateTime)(drPageInfos[drPageInfos.Length - 1]["FIRST_REC_DATE"]);
            
            int pageCount = drPageInfos.Length;
            int endPageIndex = int.Parse(drPageInfos[drPageInfos.Length - 1]["PAGE_INDEX"].ToString());
            
            // 如果没有到最后一页
            if (endPageIndex == pageEnd + 1)
            {
                pageCount = drPageInfos.Length - 1;
            }
            // 如果到了最后一页
            else
            {
                dtEnd = DateTime.MaxValue;
            }
            
            filter = "PATIENT_ID = " + SQL.SqlConvert(patientId) + "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                    + "AND PRINT_FLG = '1' AND TYPE_ID = " + SQL.SqlConvert(_typeId)
                    + "AND RECORD_DATE >= " + SQL.GetOraDbDate(dtStart) + "AND RECORD_DATE <= " + SQL.GetOraDbDate(dtEnd);
            
            DataSet dsNuringRec = dbAccess.GetTableData("NURSING_RECORD_CONTENT", filter);
            DataRow[] drNursingRec = dsNuringRec.Tables[0].Select(string.Empty, "RECORD_DATE");
            //dsPageInfo.WriteXml(@"d:\dsPageInfo.xml", XmlWriteMode.WriteSchema);
            //dsNuringRec.WriteXml(@"d:\dsNuringRec.xml", XmlWriteMode.WriteSchema);
            
            // 打印
            int idx_rec_first = 0;
            int idx_rec       = 0;
            int printedRows   = 0;
            
            DataRow drPageInfo = null;
            for(int i = 0; i < pageCount; i++)
            {
                drPageInfo = drPageInfos[i];
                                
                // 定位记录
                dtStart         = (DateTime)(drPageInfo["FIRST_REC_DATE"]);
                idx_rec_first   = int.Parse(drPageInfo["FIRST_REC_ROWS"].ToString());
                
                while(idx_rec < drNursingRec.Length)
                {
                    int compare = dtStart.CompareTo((DateTime)(drNursingRec[idx_rec][COL_RECORD_DATE]));
                    
                    if (compare == 0)
                    {
                        if (idx_rec_first >= int.Parse(drNursingRec[idx_rec]["ROW_INDEX"].ToString()))
                        {
                            break;
                        }
                    }
                    
                    if (compare < 0) 
                    {
                        idx_rec_first = 1;
                        break;
                    }
                    
                    idx_rec++;
                }
                
                if(idx_rec >= drNursingRec.Length)
                {
                    break;
                }
                
                // 打印
                printedRows = 0;
                excelOpen(printedRows);
                
                int pageIndex = int.Parse(drPageInfo["PAGE_INDEX"].ToString());
                excelPrintFixedInfo(ref drPageInfos, i);
                
                ExcelTemplatePrint(ref drNursingRec, ref idx_rec, ref printedRows);
                excelPrint();
            }
            
            return true;            
        }
        
        
        /// <summary>
        /// 指定开始页码, 重新打印
        /// </summary>
        /// <param name="pageStart"></param>
        /// <returns></returns>
        private bool reprint(int pageStart, ref DataSet dsWaitPrint)
        {
            // 加载excel节点
            loadExcelItem(_template + _typeId);
            
            // 获取页面信息
            string filter = "PAGE_INDEX = " + SQL.SqlConvert(pageStart.ToString());
            DataSet ds = dbAccess.GetTableData("NURSING_RECORD_PAGE_INFO", filter);
            DataRow[] drPageInfos = ds.Tables[0].Select(string.Empty, "PAGE_INDEX");
            if (drPageInfos.Length == 0)
            {
                GVars.Msg.MsgId = "I0012";      // 指定的页面不存在!
                return false;
            }
            
            // 获取护理记录
            DateTime dtStart = (DateTime)(drPageInfos[0]["FIRST_REC_DATE"]);
            
            filter = "RECORD_DATE >= " + SQL.GetOraDbDate(dtStart);
            DataSet dsNuringRec = dbAccess.GetTableData("NURSING_RECORD_CONTENT", filter);
            DataRow[] drNursingRec = dsNuringRec.Tables[0].Select(string.Empty, "RECORD_DATE");
            
            // 打印
            int idx_rec_first = 0;
            int idx_rec       = 0;
            int printedRows   = 0;
            
            int rows = int.Parse(drNursingRec[idx_rec]["ROW_COUNT"].ToString());
            int lastPrintedPage = pageStart - 1;
            
            while(idx_rec < drNursingRec.Length)
            {
                lastPrintedPage++;
                printedRows = 0;
                
                excelOpen(printedRows);
                
                DateTime dtTimePoint = (DateTime)(drNursingRec[idx_rec][COL_RECORD_DATE]);
                excelPrintFixedInfo(dtTimePoint, lastPrintedPage);                              // 输出固定项目
                
                idx_rec_first = idx_rec;
                ExcelTemplatePrint(ref drNursingRec, ref idx_rec, ref printedRows);
                excelPrint();

                savePageInfo(lastPrintedPage, ref drNursingRec, idx_rec_first, printedRows);    // 保存页面信息
            }
            
            return true;
        }
        
        
        /// <summary>
        /// 指定开始时间, 重新打印
        /// </summary>
        /// <param name="dtStart"></param>
        /// <returns></returns>
        private bool reprint(DateTime dtStart, ref DataSet dsWaitPrint)
        {
            // 获取页面信息
            string filter = "FIRST_REC_DATE >= " + SQL.GetOraDbDate(dtStart);
            DataSet dsPageInfo = dbAccess.GetTableData("NURSING_RECORD_PAGE_INFO", filter);
            DataRow[] drPageInfos = dsPageInfo.Tables[0].Select(string.Empty, "PAGE_INDEX");
            if (drPageInfos.Length == 0)
            {
                GVars.Msg.MsgId = "I0012";      // 指定的页面不存在!
                return false;
            }
            
            int pageStart = int.Parse(drPageInfos[0]["PAGE_INDEX"].ToString());
            return reprint(pageStart, ref dsWaitPrint);
        }
        
        
		/// <summary>
		/// 打开Excel
		/// </summary>
		/// <param name="printedRows"></param>
		private void excelOpen(int printedRows)
		{
            // 开始打印
            string fileName = "Template\\" + _template + _typeId + (printedRows == 0? "": "~" ) + ".xls";
			string strExcelTemplateFile = Path.Combine(Application.StartupPath, fileName);
            
			excelAccess.Open(strExcelTemplateFile);
			excelAccess.IsVisibledExcel = true;
			excelAccess.FormCaption = string.Empty;	

		}
		
		
		/// <summary>
		/// 打印Excel
		/// </summary>
		private void excelPrint()
		{
            excelAccess.Print();                    //打印
            //excelAccess.PrintPreview();		    //预览            
			excelAccess.Close(false);		    //关闭并释放		    
		}
		
		
		/// <summary>
		/// 输出固定项目
		/// </summary>
		private void excelPrintFixedInfo(DateTime dtTimePoint, int pageIndex)
		{
            string variableValue = string.Empty;
            for(int i = 0; i < arrSingle.Count; i++)
            {
                if (getVariableValue(arrSingle[i].ItemId, ref variableValue, dtTimePoint, pageIndex) == true)
                {
                    excelAccess.SetCellText(arrSingle[i].Row, arrSingle[i].Col, variableValue);
                    continue;
                }
            }
		}


        /// <summary>
        /// 输出固定项目
        /// </summary>
        private void excelPrintFixedInfo(ref DataRow[] drPageInfo, int pageIndex)
        {
            if (pageIndex >= drPageInfo.Length)
            {
                return;
            }
            
            DataRow drPage = drPageInfo[pageIndex];
            
            string variableValue = string.Empty;
            for (int i = 0; i < arrSingle.Count; i++)
            {
                if (drPage.Table.Columns.Contains(arrSingle[i].ItemId) == false) continue;
                
                excelAccess.SetCellText(arrSingle[i].Row, arrSingle[i].Col, drPage[arrSingle[i].ItemId].ToString());
            }
        }
		
		
        /// <summary>
		/// 用Excel模板打印，比较适合套打、格式、统计分析报表、图形分析、自定义打印
		/// </summary>
		/// <remarks>用Excel打印，步骤为：打开、写数据、打印预览、关闭</remarks>
		private bool ExcelTemplatePrint(ref DataRow[] drWaitPrint, ref int idx_rec, ref int printedRows)
		{
		    int startRow = getGridStartRow();   // 模板中表格部份起始行
            
            if (startRow <= 0)
            {
                return false;
            }

            pageRows     = getPageRows();       // 获取一页有多少行
            
            // 页面打印
            DataRow drRec   = null;             // 当前记录
            
            while(printedRows < pageRows && idx_rec < drWaitPrint.Length)                
	        {
	            drRec = drWaitPrint[idx_rec];
	            
                drRec["PRINT_FLG"] = "1";       // 打印标识	            
	            
                // 按列 输出一行
                for(int c = 1; c < arrRepeat.Count; c++)
                {
                    string colName = arrRepeat[c].ItemId;
                    excelAccess.SetCellText(startRow + printedRows, arrRepeat[c].Col, drRec[colName].ToString());
	            }
                
                printedRows++;                  // excel对应的行                
                idx_rec++;
	        }
	        
	        return true;
		}
        
		
        /// <summary>
        /// 加载打印配置文件
        /// </summary>
        /// <param name="templateFileName"></param>
        private void loadExcelItem(string templateFileName)
        {
            arrSingle.Clear();
            arrRepeat.Clear();
            
            bool blnSep = false;
            
		    // 读取配置文件
		    string iniFile = System.IO.Path.Combine(Application.StartupPath, "Template\\" + templateFileName + ".ini");
		    if (System.IO.File.Exists(iniFile) == true)
		    {
	            StreamReader sr = new StreamReader(iniFile);
		        
	            string  line        = string.Empty;
	            int     row         = 0;
	            int     col         = 0;
	            string  itemId      = string.Empty;
	            string  checkValue  = string.Empty;
		        
	            while((line = sr.ReadLine()) != null)
	            {
	                // 获取配置
	                if (getParts(line, ref row, ref col, ref itemId, ref checkValue) == false)
	                {
	                    continue;
	                }
                    
                    ExcelItem excelItem = new ExcelItem();
                    excelItem.Row       = row;
                    excelItem.Col        = col;
                    excelItem.ItemId     = itemId;
                    excelItem.CheckValue = checkValue;
                    
                    blnSep = (blnSep? blnSep: excelItem.ItemId.Equals(STR_START_POS));
                    
                    if (blnSep == false)
                    {
                        arrSingle.Add(excelItem);
                    }
                    else
                    {
                        arrRepeat.Add(excelItem);
                    }
                }
                
	            sr.Close();
	            sr.Dispose();
		    }
        }        
        
        
		/// <summary>
		/// 获取配置文件的一行
		/// </summary>
		/// <param name="line"></param>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <returns></returns>				
		private bool getParts(string line, ref int row, ref int col, ref string itemId, ref string checkValue)
		{
		    line = line.Replace(ComConst.STR.BLANK, string.Empty);
		    string[] arrParts = line.Split(ComConst.STR.COMMA.ToCharArray());
		    
		    if (arrParts.Length < 3) return false;
		    
		    itemId = arrParts[1];
            checkValue = arrParts[2];
            
		    // 获取行列
		    arrParts = arrParts[0].Split(":".ToCharArray());
		    if (arrParts.Length <= 1)
		    {
		        return false;
		    }
		    
		    // 行号
		    if (int.TryParse(arrParts[0], out row) == false)
		    {
		        return false;
		    }
		    
		    // 列号
		    col = ExcelAccess.GetCol(arrParts[1]);      
		    
		    return true;
		}		        		
				
        
		/// <summary>
		/// 获取配置文件的一行
		/// </summary>
		/// <param name="line"></param>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <returns></returns>				
		private bool getVariableValue(string variable, ref string variableValue, DateTime dtTimePoint, int pageIndex)
		{
		    string filter = string.Empty;
		    DataRow[] drFind = null;
		    
		    // 年龄单独处理
		    switch(variable)
		    {
		        case "AGE":             // 年龄
		            if (dsPatient.Tables[0].Rows[0]["DATE_OF_BIRTH"].ToString().Length > 0)
		            {
		                DateTime dt = (DateTime)dsPatient.Tables[0].Rows[0]["DATE_OF_BIRTH"];
    		            
		                variableValue = PersonCls.GetAgeYear(dt, GVars.OracleAccess.GetSysDate()).ToString();
		                hasPrintInfo[variable] = variableValue;
		                return true;
		            }
		            
		            return false;
		            
		        case "PAGE_INDEX":      // 页码
		            variableValue = pageIndex.ToString();
                    hasPrintInfo[variable] = variableValue;
		            return true;
                
                
                
                case "RECORD_DATE":     // 打印日期
                    variableValue = dtTimePoint.ToString(ComConst.FMT_DATE.LONG);
                    hasPrintInfo[variable] = dtTimePoint;
                    return true;
                    
                default:
                    break;
		    }
		    
		    // 其它病人基本信息
		    if (dsPatient.Tables[0].Columns.Contains(variable) == true)
		    {
		        variableValue = dsPatient.Tables[0].Rows[0][variable].ToString();
                hasPrintInfo[variable] = variableValue;
		        return true;
		    }
		    
		    return false;
		}				
		
		
		/// <summary>
		/// 获取表格起始行
		/// </summary>
		/// <returns></returns>
		private int getGridStartRow()
		{
		    for(int i = 0; i < arrRepeat.Count; i++)
		    {
		        if (arrRepeat[i].ItemId.Equals(STR_START_POS) == true)
		        {
		            return arrRepeat[i].Row;
		        }
		    }
		    
		    return -1;
		}
		
        		
		/// <summary>
		/// 保存页面信息
		/// </summary>
		/// <returns></returns>
        private bool savePageInfo(int pageIndex, ref DataRow[] drWaitPrint, int idx_rec, int printedRows)
		{
		    // 查找页面是否存在
		    string filter = "PAGE_INDEX = " + SQL.SqlConvert(pageIndex.ToString());
		    DataRow[] drFind = dsPageInfo.Tables[0].Select(filter);
		    
		    DataRow drEdit = null;
		    if (drFind.Length == 0)
		    {
		        drEdit = dsPageInfo.Tables[0].NewRow();

                DataRow drNursingRecord = drWaitPrint[idx_rec];
		        drEdit["PATIENT_ID"]    = drNursingRecord["PATIENT_ID"].ToString();     // 病人标识号
		        drEdit["VISIT_ID"]      = drNursingRecord["VISIT_ID"].ToString();       // 本次住院标识
		        drEdit["TYPE_ID"]       = drNursingRecord["TYPE_ID"].ToString();        // 护理记录单ID
		        drEdit["PAGE_INDEX"]    = pageIndex.ToString();                         // 页码
                drEdit["FIRST_REC_DATE"]= drNursingRecord[COL_RECORD_DATE];             // 记录日期
                drEdit["FIRST_REC_ROWS"]= drNursingRecord["ROW_INDEX"].ToString();      // 指定记录的行号
                
                // 打印信息
                drEdit["NAME"]          = (hasPrintInfo.Contains("NAME")? hasPrintInfo["NAME"].ToString(): "");
                drEdit["SEX"]           = (hasPrintInfo.Contains("SEX")? hasPrintInfo["SEX"].ToString(): "");
                drEdit["AGE"]           = (hasPrintInfo.Contains("AGE")? hasPrintInfo["AGE"].ToString(): "");
                drEdit["DEPT_NAME"]     = (hasPrintInfo.Contains("DEPT_NAME")? hasPrintInfo["DEPT_NAME"].ToString(): "");
                drEdit["WARD_NAME"]     = (hasPrintInfo.Contains("WARD_NAME")? hasPrintInfo["WARD_NAME"].ToString(): "");
                drEdit["BED_NO"]        = (hasPrintInfo.Contains("BED_NO")? hasPrintInfo["BED_NO"].ToString(): "");
                drEdit["INP_NO"]        = (hasPrintInfo.Contains("INP_NO")? hasPrintInfo["INP_NO"].ToString(): "");
                drEdit["DIAGNOSIS"]     = (hasPrintInfo.Contains("DIAGNOSIS")? hasPrintInfo["DIAGNOSIS"].ToString(): "");
                drEdit["RECORD_DATE"]    = (hasPrintInfo.Contains("RECORD_DATE")? (DateTime)(hasPrintInfo["RECORD_DATE"]): DateTime.Now);
                drEdit["PRINT_NURSE"]   = (hasPrintInfo.Contains("PRINT_NURSE")? hasPrintInfo["PRINT_NURSE"].ToString(): "");
		    }
		    else
		    {
		        drEdit = drFind[0];
		    }
		    
		    drEdit["PRINTED_ROWS"]   = printedRows;                                     // 已打印行数
                        
		    if (drFind.Length == 0)
		    {
		        dsPageInfo.Tables[0].Rows.Add(drEdit);
		    }
		    
		    return true;
		}
		#endregion
    }
}
