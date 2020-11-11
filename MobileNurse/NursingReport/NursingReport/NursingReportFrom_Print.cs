using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    partial class NursingReportFrm : FormDo
    {
        struct ExcelItem
        {
            public int Row;
            public int Col;
            public string ItemId;
            public string ItemValue;
        }

        private const string START_POS = "START_POS";

        //private int parameterId = 0;

        private int rowCount = 0;

        /// <summary>
        /// 选择的模板名称
        /// </summary>
        private string template = null;

        private ArrayList alTimePoint = new ArrayList();

        private ExcelAccess excelAccess = new ExcelAccess();

        private List<ExcelItem> arrSingle = new List<ExcelItem>();

        private List<ExcelItem> arrRepeat = new List<ExcelItem>();

        /// <summary>
        /// 首次打印
        /// </summary>
        /// <returns></returns>
        private bool printFirst()
        {
            StringBuilder filter = new StringBuilder();
            filter.Append("PATIENT_ID=" + SQL.SqlConvert(patientId));
            filter.Append(" AND VISIT_ID=" + SQL.SqlConvert(visitId));
            filter.Append(" AND TYPE_ID=" + SQL.SqlConvert(typeId));

            DataSet dsWaitPrint = dbAccess.GetTableData("NURSING_RECORD_CONTENT", filter.ToString());
            DataRow[] drWaitPrint = dsWaitPrint.Tables[0].Select(string.Empty, "RECORD_DATE ASC");
            if (drWaitPrint.Length == 0)
            {
                GVars.Msg.MsgId = "I0011";
                return false;
            }
            else
            {
                int currentIndex = 0;//当前
                int currentPage = 0;
                int currentRow = 0;
                bool isHave = loadExcelItem(template);
                if (!isHave)
                {
                    GVars.Msg.MsgId = "I0013";
                    return false;
                }
                alTimePoint = getSkipFlgTimePoint();
                while (currentIndex < drWaitPrint.Length)
                {
                    currentPage++;
                    currentRow = 0;
                    excelOpen(template);
                    excelPrintFixedInfo(currentPage);
                    bool isSuccess = false;
                    if (getItemNum() > 1)
                    {
                        isSuccess = excelTemplatePrint_Col(ref drWaitPrint, ref currentIndex, ref currentRow, currentPage);
                    }
                    else
                    {
                        isSuccess = excelTemplatePrint(ref drWaitPrint, ref currentIndex, ref currentRow, currentPage);
                    }
                    if (isSuccess)
                    {
                        excelPrint();
                        dbAccess.SaveData(null, new object[] { dsWaitPrint, dsPageInfo });
                    }
                    else
                    {
                        excelClose();
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 获取ini文件中开始位置的数目
        /// </summary>
        /// <returns></returns>
        private int getItemNum()
        {
            int num = 0;
            for (int i = 0; i < arrRepeat.Count; i++)
            {
                if (arrRepeat[i].ItemId.Equals(START_POS))
                {
                    num++;
                }
            }
            return num;
        }

        /// <summary>
        /// 根据日期打印
        /// </summary>
        /// <returns></returns>
        private bool PrintData(string txtStartDate, string txtEndDate)
        {
            StringBuilder filter = new StringBuilder();
            filter.Append("PATIENT_ID=" + SQL.SqlConvert(patientId));
            filter.Append(" AND VISIT_ID=" + SQL.SqlConvert(visitId));
            filter.Append(" AND TYPE_ID=" + SQL.SqlConvert(typeId));
            if (!string.IsNullOrEmpty(txtStartDate) && !string.IsNullOrEmpty(txtEndDate))
            {
                
                filter.Append(@" AND RECORD_DATE  BETWEEN 
                    TO_DATE('" + txtStartDate + "','YYYY-MM-DD HH24:MI:SS') AND TO_DATE('" + txtEndDate + "','YYYY-MM-DD HH24:MI:SS')");
            }
            DataSet dsWaitPrint = dbAccess.GetTableData("NURSING_RECORD_CONTENT", filter.ToString());
            DataRow[] drWaitPrint = dsWaitPrint.Tables[0].Select(string.Empty, "RECORD_DATE ASC");
            if (drWaitPrint.Length == 0)
            {
                GVars.Msg.MsgId = "I0011";
                return false;
            }
            else
            {
                int currentIndex = 0;//当前
                int currentPage = 0;
                int currentRow = 0;
                bool isHave = loadExcelItem(template);
                if (!isHave)
                {
                    GVars.Msg.MsgId = "I0013";
                    return false;
                }
                alTimePoint = getSkipFlgTimePoint();
                while (currentIndex < drWaitPrint.Length)
                {
                    currentPage++;
                    currentRow = 0;
                    excelOpen(template);
                    excelPrintFixedInfo(currentPage);
                    bool isSuccess = false;
                    if (getItemNum() > 1)
                    {
                        isSuccess = excelTemplatePrint_Col(ref drWaitPrint, ref currentIndex, ref currentRow, currentPage);
                    }
                    else
                    {
                        isSuccess = excelTemplatePrint(ref drWaitPrint, ref currentIndex, ref currentRow, currentPage);
                    }
                    if (isSuccess)
                    {
                        excelPrint();
                        dbAccess.SaveData(null, new object[] { dsWaitPrint, dsPageInfo });
                    }
                    else
                    {
                        excelClose();
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 指定页码打印
        /// </summary>
        /// <param name="startPage"></param>
        /// <param name="endPage"></param>
        /// <returns></returns>
        private bool printPageNo(int pageNo)
        {
            StringBuilder sql = new StringBuilder();
            StringBuilder sql_page = new StringBuilder();
            sql_page.Append("select max(page_no) page_no from NURSING_RECORD_PRINT_INFO");
            sql_page.Append(" where PATIENT_ID=" + SQL.SqlConvert(patientId));
            sql_page.Append(" and VISIT_ID=" + SQL.SqlConvert(visitId));
            sql_page.Append(" and type_id=" + SQL.SqlConvert(typeId));
            DataSet ds = dbAccess.GetData(sql_page.ToString(), "NURSING_RECORD_PRINT_INFO");
            if (pageNo.ToString().Equals(ds.Tables[0].Rows[0]["page_no"].ToString()))
            {
                sql.Append("select * from ");
                sql.Append(" (select a.*,b.page_no,b.row_no from NURSING_RECORD_CONTENT a left join NURSING_RECORD_PRINT_INFO b on A.PATIENT_ID = B.PATIENT_ID AND A.VISIT_ID = B.VISIT_ID ");
                sql.Append(" AND A.TYPE_ID=B.TYPE_ID AND A.RECORD_DATE=B.RECORD_DATE AND A.ROW_INDEX=B.ROW_INDEX) tt ");
                sql.Append(" where tt.PATIENT_ID=" + SQL.SqlConvert(patientId));
                sql.Append(" and tt.VISIT_ID=" + SQL.SqlConvert(visitId));
                sql.Append(" and tt.type_id=" + SQL.SqlConvert(typeId));
                sql.Append(" and (tt.page_no=" + SQL.SqlConvert(pageNo.ToString()) + " or tt.print_flg is null)");
                sql.Append("  ORDER BY tt.PAGE_NO,tt.ROW_NO");
            }
            else
            {
                sql.Append("SELECT A.*,B.PAGE_NO,B.ROW_NO");
                sql.Append(" FROM NURSING_RECORD_CONTENT A, NURSING_RECORD_PRINT_INFO B");
                sql.Append(" WHERE A.PATIENT_ID = B.PATIENT_ID");
                sql.Append(" AND A.VISIT_ID = B.VISIT_ID");
                sql.Append(" AND A.TYPE_ID=B.TYPE_ID");
                sql.Append(" AND A.RECORD_DATE=B.RECORD_DATE");
                sql.Append(" AND A.ROW_INDEX=B.ROW_INDEX");
                sql.Append(" AND B.PATIENT_ID=" + SQL.SqlConvert(patientId));
                sql.Append(" AND B.VISIT_ID=" + SQL.SqlConvert(visitId));
                sql.Append(" AND B.PAGE_NO>=" + SQL.SqlConvert(pageNo.ToString()));
                sql.Append(" AND B.PAGE_NO<=" + SQL.SqlConvert(pageNo.ToString()));
                sql.Append(" ORDER BY B.PAGE_NO,B.ROW_NO");
            }
            DataSet dsWaitPrint = dbAccess.GetData(sql.ToString(), "NURSING_RECORD_CONTENT");
            DataRow[] drWaitPrint = dsWaitPrint.Tables[0].Select(string.Empty, "ROW_NO ASC");
            if (drWaitPrint.Length == 0)
            {
                GVars.Msg.MsgId = "I0011";
                return false;
            }
            else
            {
                //选择当前指定页码的护理类型
                string fileName = null;
                DataRow[] dr = dsReocrdType.Tables[0].Select();
                for (int i = 0; i < dr.Length; i++)
                {
                    if (dr[i]["TYPE_ID"].ToString().Trim() == drWaitPrint[0]["TYPE_ID"].ToString().Trim())
                    {
                        fileName = dr[i]["DESCRIPTIONS"].ToString();
                        break;
                    }
                }
                bool isHave = loadExcelItem(fileName);
                if (!isHave)
                {
                    GVars.Msg.MsgId = "I0013";
                    return false;
                }
                excelOpen(fileName);
                excelPrintFixedInfo(pageNo);

                if (getItemNum() > 1)
                {
                    excelTemplatePrint_Col(ref drWaitPrint);
                }
                else
                {
                    excelTemplatePrint(ref drWaitPrint);
                }
                excelPrint();
            }
            return true;
        }

        /// <summary>
        /// 指定页码重打
        /// </summary>
        /// <param name="reprintPageNo"></param>
        /// <returns></returns>
        private bool reprint(int reprintPageNo)
        {
            StringBuilder filter = new StringBuilder();
            filter.Append("PATIENT_ID=" + SQL.SqlConvert(patientId));
            filter.Append(" AND VISIT_ID=" + SQL.SqlConvert(visitId));
            filter.Append(" AND PAGE_NO>=" + SQL.SqlConvert(reprintPageNo.ToString()));
            filter.Append(" ORDER BY RECORD_DATE,PAGE_NO,ROW_NO");
            DataSet dsPrintRecord = dbAccess.GetTableData("NURSING_RECORD_PRINT_INFO", filter.ToString());
            if (dsPrintRecord.Tables[0].Rows.Count < 0)
            {
                return false;
            }

            StringBuilder AddFilter = new StringBuilder();
            AddFilter.Append("PATIENT_ID=" + SQL.SqlConvert(patientId));
            AddFilter.Append(" AND VISIT_ID=" + SQL.SqlConvert(visitId));
            AddFilter.Append(" AND PRINT_FLG IS NULL");
            AddFilter.Append(" AND RECORD_DATE>=" + SQL.GetOraDbDate(dsPrintRecord.Tables[0].Rows[0]["RECORD_DATE"].ToString().Trim()));
            AddFilter.Append(" AND RECORD_DATE<=" + SQL.GetOraDbDate(dsPrintRecord.Tables[0].Rows[dsPrintRecord.Tables[0].Rows.Count - 1]["RECORD_DATE"].ToString().Trim()));
            DataSet dsAddRecord = dbAccess.GetTableData("NURSING_RECORD_CONTENT", AddFilter.ToString());
            DataRow[] drAddReocrd = dsAddRecord.Tables[0].Select();
            foreach (DataRow dr in drAddReocrd)
            {
                dr["PRINT_FLG"] = 1;
                DataRow tempDr = dsPrintRecord.Tables[0].NewRow();
                tempDr["PATIENT_ID"] = dr["PATIENT_ID"];
                tempDr["VISIT_ID"] = dr["VISIT_ID"];
                tempDr["TYPE_ID"] = dr["TYPE_ID"];
                tempDr["RECORD_DATE"] = dr["RECORD_DATE"];
                tempDr["ROW_INDEX"] = dr["ROW_INDEX"];
                dsPrintRecord.Tables[0].Rows.Add(tempDr);
            }

            DataRow[] drPrintRecord = dsPrintRecord.Tables[0].Select(string.Empty, "RECORD_DATE,PAGE_NO,ROW_NO,ROW_INDEX");
            string tempTypeId = drPrintRecord[0]["TYPE_ID"].ToString().Trim();
            int tempPageNo = reprintPageNo;
            int pageRows = getPageRowsFromParameter(tempTypeId);
            int currentRowIndex = 1;
            DateTime dtRecordDate = DateTime.MinValue;
            DateTime dtTimePoint = DateTime.MinValue;
            bool isSkip = false;
            alTimePoint = getSkipFlgTimePoint();
            foreach (DataRow dr in drPrintRecord)
            {
                if (dr["TYPE_ID"].ToString().Trim() == tempTypeId)
                {
                    dtRecordDate = Convert.ToDateTime(dr["RECORD_DATE"]);
                    for (int i = 0; i < alTimePoint.Count; i++)
                    {
                        dtTimePoint = (DateTime)alTimePoint[i];
                        if (DateTime.Compare(dtRecordDate, dtTimePoint) > 0)
                        {
                            alTimePoint.Remove(alTimePoint[i]);
                            isSkip = true;
                            break;
                        }
                    }
                    if (currentRowIndex > pageRows || isSkip == true)
                    {
                        tempPageNo++;
                        currentRowIndex = 1;
                    }
                }
                else if (dr["TYPE_ID"].ToString().Trim() != tempTypeId)
                {
                    tempTypeId = dr["TYPE_ID"].ToString().Trim();
                    pageRows = getPageRowsFromParameter(tempTypeId);
                    tempPageNo++;
                    currentRowIndex = 1;
                }
                dr["PAGE_NO"] = tempPageNo;
                dr["ROW_NO"] = currentRowIndex;
                currentRowIndex++;
                isSkip = false;
            }

            if (dbAccess.SaveTablesData(dsPrintRecord, dsAddRecord, null))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT DISTINCT A.PAGE_NO FROM NURSING_RECORD_PRINT_INFO A");
                sql.Append(" WHERE A.PATIENT_ID=" + SQL.SqlConvert(patientId));
                sql.Append(" AND A.VISIT_ID=" + SQL.SqlConvert(visitId));
                sql.Append(" AND A.PAGE_NO>=" + SQL.SqlConvert(reprintPageNo.ToString()));
                DataSet dsPrintPage = dbAccess.GetData(sql.ToString(), "NURSING_RECORD_PRINT_INFO");
                DataRow[] drPrintPage = dsPrintPage.Tables[0].Select(string.Empty, "PAGE_NO");
                foreach (DataRow dr in drPrintPage)
                {
                    printPageNo(Convert.ToInt32(dr[0]));
                }
            }
            return true;
        }

        /// <summary>
        /// 续打
        /// </summary>
        /// <returns></returns>
        private bool printContinue()
        {
            StringBuilder filter = new StringBuilder();
            filter.Append("PATIENT_ID=" + SQL.SqlConvert(patientId));
            filter.Append(" AND VISIT_ID=" + SQL.SqlConvert(visitId));
            filter.Append(" AND TYPE_ID=" + SQL.SqlConvert(typeId));
            filter.Append(" AND PRINT_FLG IS NULL");
            DataSet dsWaitPrint = dbAccess.GetTableData("NURSING_RECORD_CONTENT", filter.ToString());
            DataRow[] drWaitPrint = dsWaitPrint.Tables[0].Select(string.Empty, "RECORD_DATE ASC");
            if (drWaitPrint.Length == 0)
            {
                GVars.Msg.MsgId = "I0011";
                return false;
            }
            else
            {
                int currentIndex = 0;
                int currentPage = 0;
                int currentRow = 0;
                int pageRows = rowCount;
                bool isHave = false;
                string fileName = template;
                alTimePoint = getSkipFlgTimePointOfRecordDate();
                getLastPrintedPageInfo(ref currentPage, ref currentRow);
                while (currentIndex < drWaitPrint.Length)
                {
                    if (currentRow >= pageRows || currentIndex > 0 || currentRow == 0)
                    {
                        fileName = template;
                    }
                    else
                    {
                        fileName = template + "~";
                    }
                    isHave = loadExcelItem(fileName);
                    if (!isHave)
                    {
                        GVars.Msg.MsgId = "I0013";
                        return false;
                    }
                    excelOpen(fileName);
                    excelPrintFixedInfo(currentPage);
                    bool isSuccess = false;
                    if (getItemNum() > 1)
                    {
                        isSuccess = excelTemplatePrint_Col(ref drWaitPrint, ref currentIndex, ref currentRow, currentPage);
                    }
                    else
                    {
                        isSuccess = excelTemplatePrint(ref drWaitPrint, ref currentIndex, ref currentRow, currentPage);
                    }
                    if (isSuccess)
                    {
                        excelPrint();
                        dbAccess.SaveData(null, new object[] { dsWaitPrint, dsPageInfo });
                    }
                    else
                    {
                        excelClose();
                    }
                    currentRow = 0;
                    currentPage++;
                }
            }
            return true;
        }

        private bool printContinue(bool flag)
        {
            StringBuilder filter = new StringBuilder();
            filter.Append("PATIENT_ID=" + SQL.SqlConvert(patientId));
            filter.Append(" AND VISIT_ID=" + SQL.SqlConvert(visitId));
            filter.Append(" AND TYPE_ID=" + SQL.SqlConvert(typeId));
            filter.Append(" AND PRINT_FLG IS NULL");
            DataSet dsWaitPrint = dbAccess.GetTableData("NURSING_RECORD_CONTENT", filter.ToString());
            DataRow[] drWaitPrint = dsWaitPrint.Tables[0].Select(string.Empty, "RECORD_DATE ASC");
            if (drWaitPrint.Length == 0)
            {
                return false;
            }
            else
            {
                int currentIndex = 0;
                int currentPage = 0;
                int currentRow = 0;
                int pageRows = rowCount;
                bool isHave = false;
                string fileName = template;
                alTimePoint = getSkipFlgTimePointOfRecordDate();
                getLastPrintedPageInfo(ref currentPage, ref currentRow);
                while (currentIndex < drWaitPrint.Length)
                {
                    if (currentRow >= pageRows || currentIndex > 0 || currentRow == 0)
                    {
                        fileName = template;
                    }
                    else
                    {
                        fileName = template + "~";
                    }
                    isHave = loadExcelItem(fileName);
                    if (!isHave)
                    {
                        GVars.Msg.MsgId = "I0013";
                        return false;
                    }
                    excelOpen(fileName);
                    excelPrintFixedInfo(currentPage);
                    bool isSuccess = false;
                    if (getItemNum() > 1)
                    {
                        isSuccess = excelTemplatePrint_Col(ref drWaitPrint, ref currentIndex, ref currentRow, currentPage);
                    }
                    else
                    {
                        isSuccess = excelTemplatePrint(ref drWaitPrint, ref currentIndex, ref currentRow, currentPage);
                    }
                    if (isSuccess)
                    {
                        // excelPrint();
                        dbAccess.SaveData(null, new object[] { dsWaitPrint, dsPageInfo });
                    }
                    else
                    {
                        excelClose();
                    }
                    currentRow = 0;
                    currentPage++;
                }
            }
            return true;
        }

        /// <summary>
        /// 加载打印ini文件
        /// </summary>
        /// <param name="templateFileName"></param>
        private bool loadExcelItem(string templateFileName)
        {
            arrSingle.Clear();
            arrRepeat.Clear();
            string iniFile = Path.Combine(Application.StartupPath, "Template\\" + templateFileName + ".ini");
            if (File.Exists(iniFile))
            {
                int row = 0;
                int col = 0;
                bool startPos = false;
                string itemId = string.Empty;
                string itemValue = string.Empty;
                string line = string.Empty;
                StreamReader sr = new StreamReader(iniFile);
                while ((line = sr.ReadLine()) != null)
                {
                    if (!getParts(line, ref row, ref col, ref itemId, ref itemValue))
                    {
                        continue;
                    }
                    ExcelItem excelItem = new ExcelItem();
                    excelItem.Row = row;
                    excelItem.Col = col;
                    excelItem.ItemId = itemId;
                    excelItem.ItemValue = itemValue;
                    startPos = (startPos ? startPos : excelItem.ItemId.Equals(START_POS));
                    if (startPos)
                    {
                        arrRepeat.Add(excelItem);
                    }
                    else
                    {
                        arrSingle.Add(excelItem);
                    }
                }
                sr.Close();
                sr.Dispose();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取配置文件的一行
        /// </summary>
        /// <param name="line"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="itemId"></param>
        /// <param name="itemValue"></param>
        /// <returns></returns>
        private bool getParts(string line, ref int row, ref int col, ref string itemId, ref string itemValue)
        {
            line = line.Replace(ComConst.STR.BLANK, string.Empty);
            string[] arrParts = line.Split(ComConst.STR.COMMA.ToCharArray());
            if (arrParts.Length < 3)
            {
                return false;
            }
            itemId = arrParts[1];
            itemValue = arrParts[2];
            arrParts = arrParts[0].Split(":".ToCharArray());
            if (arrParts.Length <= 1)
            {
                return false;
            }

            if (!int.TryParse(arrParts[0], out row))
            {
                return false;
            }
            col = ExcelAccess.GetCol(arrParts[1]);
            return true;
        }

        /// <summary>
        /// 打开Excel
        /// </summary>
        /// <param name="fileName">文件名称</param>
        private void excelOpen(string fileName)
        {
            fileName = "Template\\" + fileName + ".xls";
            string excelTemplateFile = Path.Combine(Application.StartupPath, fileName);
            excelAccess.Open(excelTemplateFile);
            excelAccess.IsVisibledExcel = true;
            excelAccess.FormCaption = string.Empty;
        }

        /// <summary>
        /// 关闭Excel
        /// </summary>
        private void excelClose()
        {
            excelAccess.Close(false);
        }


        /// <summary>
        /// 输出模板中的固定项
        /// </summary>
        private void excelPrintFixedInfo(int pageNo)
        {
            string variableValue = string.Empty;
            for (int i = 0; i < arrSingle.Count; i++)
            {
                if (getVariableValue(arrSingle[i].ItemId, ref variableValue, pageNo))
                {
                    excelAccess.SetCellText(arrSingle[i].Row, arrSingle[i].Col, variableValue);
                }
            }
        }

        /// <summary>
        /// 获取配置文件中项目的值
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="varibleValue"></param>
        /// <returns></returns>
        private bool getVariableValue(string variable, ref string varibleValue, int pageNo)
        {
            switch (variable)
            {
                case "PAGE_INDEX":
                    varibleValue = pageNo.ToString();
                    return true;
            }

            if (dsPatient.Tables[0].Columns.Contains(variable))
            {
                varibleValue = dsPatient.Tables[0].Rows[0][variable].ToString();
            }
            if (dsRecordContent.Tables[0].Columns.Contains(variable))
            {
                varibleValue = dsRecordContent.Tables[0].Rows[0][variable].ToString();
            }
            return true;
        }

        /// <summary>
        /// Excel模板打印
        /// </summary>
        /// <param name="drWaitPrint"></param>
        /// <param name="currentIndex"></param>
        /// <param name="currentRow"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        private bool excelTemplatePrint(ref DataRow[] drWaitPrint, ref int currentIndex, ref int currentRow, int currentPage)
        {
            //模板中表格部分起始行
            int startRow = 0;
            for (int i = 0; i < arrRepeat.Count; i++)
            {
                if (arrRepeat[i].ItemId.Equals(START_POS))
                {
                    startRow = arrRepeat[i].Row;
                    break;
                }
            }
            if (startRow <= 0)
            {
                return false;
            }
            int pageRows = rowCount;
            bool isHaveData = false;
            DataRow drRec = null;
            DateTime dtRecordDate = DateTime.MinValue;
            DateTime dtTimePoint = DateTime.MinValue;
            while (currentRow < pageRows && currentIndex < drWaitPrint.Length)
            {
                drRec = drWaitPrint[currentIndex];

                //判断是否换页
                dtRecordDate = Convert.ToDateTime(drRec["RECORD_DATE"].ToString());
                for (int i = 0; i < alTimePoint.Count; i++)
                {
                    dtTimePoint = (DateTime)alTimePoint[i];
                    if (DateTime.Compare(dtRecordDate, dtTimePoint) > 0)
                    {
                        alTimePoint.Remove(alTimePoint[i]);
                        if (currentIndex <= 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }

                for (int j = 1; j < arrRepeat.Count; j++)
                {
                    excelAccess.SetCellText(startRow + currentRow, arrRepeat[j].Col, drRec[arrRepeat[j].ItemId].ToString());
                    isHaveData = true;
                }
                savePrintInfo(drRec, currentPage, currentRow + 1);
                currentRow++;
                currentIndex++;
            }
            if (!isHaveData)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Excel模板打印
        /// </summary>
        /// <param name="drWaitPrint"></param>
        /// <param name="currentIndex"></param>
        /// <param name="currentRow"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        private bool excelTemplatePrint_Col(ref DataRow[] drWaitPrint, ref int currentIndex, ref int currentRow, int currentPage)
        {
            int key = 0;
            Hashtable hashTable = new Hashtable();
            ArrayList arraylist = null;
            for (int i = 0; i < arrRepeat.Count; i++)
            {
                if (arrRepeat[i].ItemId.Equals(START_POS))
                {
                    key++;
                    arraylist = new ArrayList();
                    hashTable.Add(key, arraylist);
                }
                else
                {
                    arraylist.Add(arrRepeat[i]);
                }
            }

            //模板中表格部分起始行
            int pageRows = rowCount;
            bool isHaveData = false;
            DataRow drRec = null;
            DateTime dtRecordDate = DateTime.MinValue;
            DateTime dtTimePoint = DateTime.MinValue;
            while (currentRow < pageRows && currentIndex < drWaitPrint.Length)
            {
                drRec = drWaitPrint[currentIndex];

                //判断是否换页
                dtRecordDate = Convert.ToDateTime(drRec["RECORD_DATE"].ToString());
                for (int i = 0; i < alTimePoint.Count; i++)
                {
                    dtTimePoint = (DateTime)alTimePoint[i];
                    if (DateTime.Compare(dtRecordDate, dtTimePoint) > 0)
                    {
                        alTimePoint.Remove(alTimePoint[i]);
                        if (currentIndex <= 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }

                foreach (DictionaryEntry ht in hashTable)
                {
                    if (ht.Key.ToString() == (currentRow + 1).ToString())
                    {
                        string strValue = string.Empty;
                        ArrayList al = (ArrayList)ht.Value;
                        for (int j = 0; j < al.Count; j++)
                        {
                            ExcelItem ei = (ExcelItem)al[j];
                            switch (ei.ItemId)
                            {
                                case "MONTH":
                                    {
                                        strValue = DateTime.Parse(drRec[COL_RECORD_DATE].ToString()).Month.ToString();
                                        break;
                                    }
                                case "DAY":
                                    {
                                        strValue = DateTime.Parse(drRec[COL_RECORD_DATE].ToString()).Day.ToString();
                                        break;
                                    }
                                case "HOUR":
                                    {
                                        strValue = DateTime.Parse(drRec[COL_RECORD_DATE].ToString()).Hour.ToString();
                                        break;
                                    }
                                default:
                                    {
                                        strValue = drRec[ei.ItemId].ToString();
                                        break;
                                    }
                            }
                            excelAccess.SetCellText(ei.Row, ei.Col, strValue);
                        }
                        isHaveData = true;
                        break;
                    }
                }
                savePrintInfo(drRec, currentPage, currentRow + 1);
                currentRow++;
                currentIndex++;
            }
            if (!isHaveData)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// Excel模板打印
        /// </summary>
        /// <param name="drWaitPrint"></param>
        /// <returns></returns>
        private bool excelTemplatePrint(ref DataRow[] drWaitPrint)
        {
            //模板中表格部分起始行
            int startRow = 0;
            for (int i = 0; i < arrRepeat.Count; i++)
            {
                if (arrRepeat[i].ItemId.Equals(START_POS))
                {
                    startRow = arrRepeat[i].Row;
                    break;
                }
            }
            if (startRow <= 0)
            {
                return false;
            }

            int currentIndex = 0;
            DataRow drRec = null;
            while (currentIndex < drWaitPrint.Length)
            {
                drRec = drWaitPrint[currentIndex];
                for (int j = 1; j < arrRepeat.Count; j++)
                {
                    excelAccess.SetCellText(startRow + Convert.ToInt32(drRec["ROW_NO"].ToString()) - 1, arrRepeat[j].Col, drRec[arrRepeat[j].ItemId].ToString());
                }
                currentIndex++;
            }
            return true;
        }

        /// <summary>
        /// Excel模板打印
        /// </summary>
        /// <param name="drWaitPrint"></param>
        /// <returns></returns>
        private bool excelTemplatePrint_Col(ref DataRow[] drWaitPrint)
        {
            int key = 0;
            Hashtable hashTable = new Hashtable();
            ArrayList arraylist = null;
            for (int i = 0; i < arrRepeat.Count; i++)
            {
                if (arrRepeat[i].ItemId.Equals(START_POS))
                {
                    key++;
                    arraylist = new ArrayList();
                    hashTable.Add(key, arraylist);
                }
                else
                {
                    arraylist.Add(arrRepeat[i]);
                }
            }

            int currentIndex = 0;
            DataRow drRec = null;
            while (currentIndex < drWaitPrint.Length)
            {
                drRec = drWaitPrint[currentIndex];
                foreach (DictionaryEntry ht in hashTable)
                {
                    if (ht.Key.ToString() == (currentIndex + 1).ToString())
                    {
                        string strValue = string.Empty;
                        ArrayList al = (ArrayList)ht.Value;
                        for (int j = 0; j < al.Count; j++)
                        {
                            ExcelItem ei = (ExcelItem)al[j];
                            switch (ei.ItemId)
                            {
                                case "MONTH":
                                    {
                                        strValue = DateTime.Parse(drRec[COL_RECORD_DATE].ToString()).Month.ToString();
                                        break;
                                    }
                                case "DAY":
                                    {
                                        strValue = DateTime.Parse(drRec[COL_RECORD_DATE].ToString()).Day.ToString();
                                        break;
                                    }
                                case "HOUR":
                                    {
                                        strValue = DateTime.Parse(drRec[COL_RECORD_DATE].ToString()).Hour.ToString();
                                        break;
                                    }
                                default:
                                    {
                                        strValue = drRec[ei.ItemId].ToString();
                                        break;
                                    }
                            }
                            excelAccess.SetCellText(ei.Row, ei.Col, strValue);
                        }
                    }
                }
                currentIndex++;
            }
            return true;
        }



        private int getPageRowsFromParameter(string typeId)
        {
            string sql = "SELECT ROW_COUNT FROM NURSING_RECORD_TYPE WHERE TYPE_ID=" + SQL.SqlConvert(typeId);
            if (dbAccess.SelectValue(sql))
            {
                return Convert.ToInt32(dbAccess.GetResult(0));
            }
            return 0;
        }

        //// <summary>
        ////从配置获取打印页面的参数
        //// </summary>
        //// <returns></returns>
        //private int getPageRowsFromParameter()
        //{
        //    string sql = "SELECT PARAMETER_VALUE FROM APP_CONFIG WHERE PARAMETER_ID=" + SQL.SqlConvert(parameterId.ToString());
        //    if (dbAccess.SelectValue(sql))
        //    {
        //        return Convert.ToInt32(dbAccess.GetResult(0));
        //    }
        //    return 0;
        //}

        //private int getPageRowsFromParameter(string typeId)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    sql.Append("SELECT DISTINCT A.PARAMETER_VALUE FROM ");
        //    sql.Append("APP_CONFIG A, NURSING_RECORD_TYPE B WHERE");
        //    sql.Append(" A.PARAMETER_ID=B.PARAMETER_ID");
        //    sql.Append(" AND B.TYPE_ID=" + SQL.SqlConvert(typeId));
        //    if (dbAccess.SelectValue(sql.ToString()))
        //    {
        //        return Convert.ToInt32(dbAccess.GetResult(0));
        //    }
        //    return 0;
        //}

        /// <summary>
        /// Excel打印
        /// </summary>
        private void excelPrint()
        {
            excelAccess.PrintPreview();
            excelAccess.Close(false);
        }

        /// <summary>
        /// 保存打印信息
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="currentPage"></param>
        /// <param name="currentRow"></param>
        private void savePrintInfo(DataRow dr, int currentPage, int currentRow)
        {
            dr["PRINT_FLG"] = "1";
            StringBuilder filter = new StringBuilder();
            filter.Append("PATIENT_ID=" + SQL.SqlConvert(dr["PATIENT_ID"].ToString()));
            filter.Append(" AND VISIT_ID=" + SQL.SqlConvert(dr["VISIT_ID"].ToString()));
            filter.Append(" AND TYPE_ID=" + SQL.SqlConvert(dr["TYPE_ID"].ToString()));
            filter.Append(" AND RECORD_DATE=" + SQL.SqlConvert(dr["RECORD_DATE"].ToString()));
            filter.Append(" AND ROW_INDEX=" + SQL.SqlConvert(dr["ROW_INDEX"].ToString()));
            DataRow[] drFind = dsPageInfo.Tables[0].Select(filter.ToString());
            DataRow drEdit = null;
            if (drFind.Length == 0)
            {
                drEdit = dsPageInfo.Tables[0].NewRow();
                drEdit["PATIENT_ID"] = dr["PATIENT_ID"].ToString();
                drEdit["VISIT_ID"] = dr["VISIT_ID"].ToString();
                drEdit["TYPE_ID"] = dr["TYPE_ID"].ToString();
                drEdit["RECORD_DATE"] = dr["RECORD_DATE"].ToString();
                drEdit["ROW_INDEX"] = dr["ROW_INDEX"].ToString();
                drEdit["PAGE_NO"] = currentPage;
                drEdit["ROW_NO"] = currentRow;
                dsPageInfo.Tables[0].Rows.Add(drEdit);
            }
            else
            {
                drEdit = drFind[0];
                drEdit["PAGE_NO"] = currentPage;
                drEdit["ROW_NO"] = currentRow;
            }
        }

        /// <summary>
        /// 获取当前打印页的信息
        /// </summary>
        /// <param name="lastPageNo"></param>
        /// <param name="lastRowNo"></param>
        private void getLastPrintedPageInfo(ref int lastPageNo, ref int lastRowNo)
        {
            int temPageNo = 0;
            string tempTypeId = string.Empty;
            string sql = "SELECT DISTINCT TYPE_ID,PAGE_NO FROM NURSING_RECORD_PRINT_INFO WHERE PAGE_NO=(SELECT MAX(PAGE_NO) FROM NURSING_RECORD_PRINT_INFO)";
            if (dbAccess.SelectValue(sql))
            {
                tempTypeId = dbAccess.GetResult(0).ToString();
                temPageNo = int.Parse(dbAccess.GetResult(1));
            }

            StringBuilder filter = new StringBuilder();
            filter.Append("PATIENT_ID=" + SQL.SqlConvert(patientId));
            filter.Append(" AND VISIT_ID=" + SQL.SqlConvert(visitId));
            filter.Append(" AND TYPE_ID=" + SQL.SqlConvert(typeId));
            sql = "SELECT NVL(MAX(PAGE_NO),0) FROM NURSING_RECORD_PRINT_INFO WHERE " + filter;
            if (dbAccess.SelectValue(sql))
            {
                lastPageNo = int.Parse(dbAccess.GetResult(0));

            }
            filter.Append(" AND PAGE_NO=" + SQL.SqlConvert(lastPageNo.ToString()));
            sql = "SELECT NVL(MAX(ROW_NO),0) FROM NURSING_RECORD_PRINT_INFO WHERE " + filter;
            if (dbAccess.SelectValue(sql))
            {
                lastRowNo = int.Parse(dbAccess.GetResult(0));
            }
            if (!tempTypeId.Equals(typeId))
            {
                lastPageNo = temPageNo + 1;
                lastRowNo = 0;
            }
        }

        /// <summary>
        /// 获取分页的时间点
        /// </summary>
        /// <returns></returns>
        private ArrayList getSkipFlgTimePoint()
        {
            ArrayList al = new ArrayList();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT A.TIME_POINT");
            sql.Append(" FROM VITAL_SIGNS_REC A,NURSING_RECORD_PRINT_SKIP_FLG B");
            sql.Append(" WHERE A.VITAL_CODE=B.VITAL_CODE");
            sql.Append(" AND A.VITAL_SIGNS=B.VITAL_SIGNS");
            sql.Append(" AND A.WARD_CODE=B.WARD_CODE");
            sql.Append(" AND A.PATIENT_ID=" + SQL.SqlConvert(patientId));
            sql.Append(" AND A.VISIT_ID=" + SQL.SqlConvert(visitId));
            sql.Append(" AND A.WARD_CODE=" + SQL.SqlConvert(GVars.User.DeptCode));
            sql.Append(" ORDER BY A.TIME_POINT");
            DataSet dsTimePoint = dbAccess.GetData(sql.ToString(), "VITAL_SIGNS_REC");
            DataRow[] drTimePoint = dsTimePoint.Tables[0].Select(string.Empty, "TIME_POINT");
            foreach (DataRow dr in drTimePoint)
            {
                al.Add(dr["TIME_POINT"]);
            }
            return al;
        }

        /// <summary>
        /// 根据护理时间获取时间点
        /// </summary>
        /// <param name="timePoint"></param>
        /// <returns></returns>
        private ArrayList getSkipFlgTimePointOfRecordDate()
        {
            ArrayList al = new ArrayList();
            DateTime timePoint = DateTime.MinValue;

            StringBuilder filter = new StringBuilder();
            filter.Append("PATIENT_ID=" + SQL.SqlConvert(patientId));
            filter.Append(" AND VISIT_ID=" + SQL.SqlConvert(visitId));
            filter.Append(" AND PRINT_FLG IS NOT NULL");
            string strSql = "SELECT MAX(RECORD_DATE) FROM NURSING_RECORD_CONTENT WHERE " + filter.ToString();
            dbAccess.SelectValue(strSql);
            if (dbAccess.GetResult(0) != string.Empty)
            {
                timePoint = Convert.ToDateTime(dbAccess.GetResult(0));
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT A.TIME_POINT");
                sql.Append(" FROM VITAL_SIGNS_REC A,NURSING_RECORD_PRINT_SKIP_FLG B");
                sql.Append(" WHERE A.VITAL_CODE=B.VITAL_CODE");
                sql.Append(" AND A.VITAL_SIGNS=B.VITAL_SIGNS");
                sql.Append(" AND A.WARD_CODE=B.WARD_CODE");
                sql.Append(" AND A.PATIENT_ID=" + SQL.SqlConvert(patientId));
                sql.Append(" AND A.VISIT_ID=" + SQL.SqlConvert(visitId));
                sql.Append(" AND A.WARD_CODE=" + SQL.SqlConvert(GVars.User.DeptCode));
                sql.Append(" AND A.TIME_POINT>" + SQL.GetOraDbDate(timePoint));
                sql.Append(" ORDER BY A.TIME_POINT");
                DataSet dsTimePoint = dbAccess.GetData(sql.ToString(), "VITAL_SIGNS_REC");
                DataRow[] drTimePoint = dsTimePoint.Tables[0].Select(string.Empty, "TIME_POINT");
                foreach (DataRow dr in drTimePoint)
                {
                    al.Add(dr["TIME_POINT"]);
                }
            }
            return al;
        }
    }
}
