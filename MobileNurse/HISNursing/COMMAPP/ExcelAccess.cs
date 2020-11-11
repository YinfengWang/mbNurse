//------------------------------------------------------------------------------------
//
//  系统名称        : 医院信息系统
//  子系统名称      : 共通模块
//  对象类型        : 
//  类名            : ExcelAccess.cs
//  功能概要        : Excel访问类
//  作成者          : 付军
//  作成日          : 2007-01-23
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        :
//  变更者          :
//  变更内容        :
//  版本            :
//------------------------------------------------------------------------------------

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;

namespace HISPlus
{
	/// <summary>
	/// ExcelAccess 的摘要说明。
	/// </summary>
	public class ExcelAccess
	{
        #region 自定义
        /// <summary>
        /// 占位符类型
        /// </summary>
        public enum PlaceHolderType
        {
            NONE    = 0,                // 不是占位符
            SINGLE  = 1,                // 单目占位符
            MORE    = 2,                // 多目占位符
            END     = 3                 // 多目占位符结束
        }

        
        /// <summary>
        /// 占位符位置
        /// </summary>
        public struct PlaceHolderPos
        {
            public int     col;         // 列数
            public int     dataRow;     // 表的第几行
            public string  tableName;   // 表名
            public string  fieldName;   // 字段名
            public bool    deleted;     // 是否删除
        }
        #endregion


		#region 变量
		private Excel.Application	xlApp;										// Excel应用程序
		private Excel.Workbook		xlWorkbook;									// Excel工作薄，默认只有一个，用Open([Template])创建

		private bool				isVisibledExcel	= false;					// 打印或预览时是否还要显示Excel窗体
		private string				formCaption		= string.Empty;				// 打印预览Excel窗体的标题栏
		
		private Object				oMissing = System.Reflection.Missing.Value;	// 实例化参数对象
        private DataTable dtNursingDataNew;                                     // 分页截取后的护理记录表
        
        public Patient_Info PatientInfo = new Patient_Info();
        public Print_Info PrintInfo = new Print_Info();
       
		#endregion


        #region 结构体
        public struct Patient_Info
        {
            public string Name;
            public string Gender;
            public string Age;
            public string DeptName;
            public string BedNo;
            public string Date;
            public string Diagnosis;
        }

        public struct Print_Info
        {
            public DataRow[] DataRowList;
            public int RowNum;
            public int PageNum;
            public bool IsShow;
            public bool Pagination; 
        }
        #endregion
		
		public ExcelAccess()
		{
		}


		#region 属性
		/// <summary>
		/// Excel实例
		/// </summary>
		public Excel.Application ExcelApp
		{
			get
			{
				return xlApp;
			}
			set
			{
				xlApp = value;
			}
		}


		/// <summary>
		/// Excel工作薄，默认只有一个，用Open([Template])创建
		/// </summary>
		public Excel.Workbook Workbooks
		{
			get
			{
				return xlWorkbook;
			}
		}


		/// <summary>
		/// 打印或预览时是否还要显示Excel窗体
		/// </summary>
		public bool IsVisibledExcel
		{
			get
			{
				return isVisibledExcel;
			}
			set
			{
				isVisibledExcel = value;
			}		
		}


		/// <summary>
		/// 打印预览Excel窗体的标题栏
		/// </summary>
		public string FormCaption
		{
			get
			{
				return formCaption;
			}
			set
			{
				formCaption = value;
			}
		}


        /// <summary>
        /// 分页截取后的护理记录表
        /// </summary>
        public DataTable DTNursingDataNew
        {
            get
            {
                return dtNursingDataNew;
            }
            set
            {
                dtNursingDataNew = value;
            }
        }
		#endregion
		

		#region 打开关闭
		/// <summary>
		/// 打开Excel，并建立默认的Workbooks。
		/// </summary>
		/// <returns></returns>
		public void Open()
		{	
			if (xlApp == null)
			{
				xlApp = new Excel.ApplicationClass();
				xlApp.DisplayAlerts = false;
			}

			xlWorkbook = xlApp.Workbooks.Add(oMissing);
		}

		
		/// <summary>
		/// 根据现有工作薄模板打开，如果指定的模板不存在，则用默认的空模板
		/// </summary>
		/// <param name="templateFileName">用作模板的工作薄文件名</param>
		public void Open(string templateFileName)
		{	
			if (System.IO.File.Exists(templateFileName))
			{
				if (xlApp == null)
				{
					xlApp = new Excel.ApplicationClass();
					xlApp.DisplayAlerts = false;
				}

				xlWorkbook = xlApp.Workbooks.Add(templateFileName);	
			}
			else
			{
				Open();
			}
		}

		
		/// <summary>
		/// 关闭
		/// </summary>
		/// <param name="blnQuit">是否退出</param>
		public void Close(bool blnQuit)
		{		
			if (xlApp != null)
			{
				xlApp.Workbooks.Close();

                if (blnQuit == false)
                {
                    xlApp.Visible = false;
                    return;
                }
                
				xlWorkbook = null;

                xlApp.Quit();
                xlApp = null;
			}
		}

		
		/// <summary>
		/// Excel实例退出
		/// </summary>
		public void Quit()
		{
			// 如果没有实例化Excel, 直接退
			if (xlApp == null)
			{
				return;
			}

			// 关闭WorkBook 与 Excel实例
            if (xlApp != null)
            {
                xlApp.Workbooks.Close();
                xlWorkbook = null;
			
                xlApp.Quit();
                xlApp = null;
            }

			oMissing = null;
			
			// 强制垃圾回收，否则每次实例化Excel，则Excell进程多一个。
			System.GC.Collect();
		}
		#endregion


		#region 打印及预览
		/// <summary>
		/// 显示Excel
		/// </summary>
		public void ShowExcel()
		{			
			xlApp.Visible = true;
		}
			

		/// <summary>
		/// 用Excel打印预览，如果要显示Excel窗口，请设置IsVisibledExcel 
		/// </summary>
		public void PrintPreview()
		{			
			xlApp.Caption = FormCaption;
			xlApp.Visible = true;

			try
			{	
				xlApp.ActiveWorkbook.PrintPreview(oMissing);
			}
			catch{}
			
			xlApp.Visible = this.isVisibledExcel;		
		}


		/// <summary>
		/// 用Excel打印，如果要显示Excel窗口，请设置IsVisibledExcel 
		/// </summary>
		public void Print()
		{
			xlApp.Visible = this.isVisibledExcel;	

			Object oMissing = System.Reflection.Missing.Value;  // 实例化参数对象

			try
			{
				xlApp.ActiveWorkbook.PrintOut(oMissing,oMissing,oMissing,oMissing,oMissing,oMissing,oMissing,oMissing);	
			}
			catch{}
		}
		#endregion


		#region 另存
		/// <summary>
		/// 另存。如果保存成功，则返回true，否则，如果保存不成功或者如果已存在文件但是选择了不替换也返回false
		/// </summary>
		/// <param name="fileName">将要保存的文件名</param>
		/// <param name="replaceExistsFileName">如果文件存在，则替换</param>
		public bool SaveAs(string fileName,bool replaceExistsFileName)
		{
			bool blnReturn = false;

			if (System.IO.File.Exists(fileName))
			{
				if (replaceExistsFileName)
				{
					System.IO.File.Delete(fileName);
					blnReturn = true;
				}
			}

			try
			{				
				xlApp.ActiveWorkbook.SaveCopyAs(fileName);
				blnReturn = true;
			}
			catch
			{
				blnReturn = false;
			}
			
			return blnReturn;
		}
		#endregion
		

		#region 单元格文本
		/// <summary>
		/// 设置文本
		/// </summary>
		/// <param name="row">Excel 的行号</param>
		/// <param name="col">Excel 的列号</param>
		/// <param name="text">文本</param>
		public void SetCellText(int row, int col, string text)
		{
            //if (text.IndexOf("\n") == 0)
            //{
            //    text = text.Substring(1);
            //}

			xlApp.Cells[row, col] = text.Trim();
		}

		
		/// <summary>
		/// 获取单元格的文本
		/// </summary>
		/// <param name="row">Excel 的行号</param>
		/// <param name="col">Excel 的列号</param>
		/// <returns>单元格的文本</returns>
		public string GetCellText(int row, int col)
		{
			return ((Excel.Range)xlApp.Cells[row, col]).Text.ToString();
		}
		#endregion
        

        #region 常用操作
        /// <summary>
        /// Excel有效范围
        /// </summary>
        /// <param name="startRow">开始行</param>
        /// <param name="endRow">结束行</param>
        /// <param name="startCol">开始列</param>
        /// <param name="endCol">结束列</param>
        /// <returns>TRUE: 成功; FALSE: 失败</returns>
        public bool GetExcelValRange(ref int startRow, ref int endRow, ref int startCol, ref int endCol)
        {
            string region       = GetCellText(1, 1);
            string[] arrRange   = null;
            string[] arrVal     = null;
            try
            {
                // 获取行列的范围
                if (region.IndexOf(ComConst.STR.SLASH) > 0)
                {
                    arrRange = region.Split(ComConst.STR.SLASH.ToCharArray());
                }
                else if (region.IndexOf(ComConst.STR.BACKSLASH) > 0)
                {
                    arrRange = region.Split(ComConst.STR.BACKSLASH.ToCharArray());
                }

                if (arrRange.Length <= 1)
                {
                    return false;
                }
                
                // 获取行列的值
                string rowExp = arrRange[0];
                string colExp = arrRange[1];
                
                arrVal = rowExp.Split("-".ToCharArray());
                if (arrVal.Length <= 1)
                {
                    return false;
                }
                
                startRow = int.Parse(arrVal[0]);
                endRow = int.Parse(arrVal[1]);

                arrVal = colExp.Split("-".ToCharArray());
                if (arrVal.Length <= 1)
                {
                    return false;
                }
                
                startCol = int.Parse(arrVal[0]);
                endCol = int.Parse(arrVal[1]);                
                
                SetCellText(1, 1, string.Empty);

                return true;
            }
            catch
            {
                return false;
            }
        }

        
        /// <summary>
        /// 判断是不是占位符
        /// </summary>
        /// <param name="text">Excel文本中的字符</param>
        /// <returns>TRUE: 是; FALSE: 不是</returns>
        public PlaceHolderType GetPlaceHolderType(string cellText)
        {
            // 预处理
            cellText = cellText.Trim();

            // 必须大于2个字符
            if (cellText.Length < 3)
            {
                return PlaceHolderType.NONE;
            }
                        
            // 模式 [*]
            if (cellText.StartsWith(ComConst.STR.SQUARE_BRACKET_L) && cellText.EndsWith(ComConst.STR.SQUARE_BRACKET_R))
            {
                return PlaceHolderType.SINGLE;
            }
            
            // 模式 {*}
            if (cellText.StartsWith(ComConst.STR.BRACE_LEFT) && cellText.EndsWith(ComConst.STR.BRACE_RIGHT))
            {
                if (cellText.StartsWith(ComConst.STR.BRACE_LEFT + ComConst.STR.BACKSLASH) ||
                    cellText.StartsWith(ComConst.STR.BRACE_LEFT + ComConst.STR.SLASH))
                {
                    return PlaceHolderType.END;
                }
                else
                {
                    return PlaceHolderType.MORE;
                }
            }
            
            return PlaceHolderType.NONE;
        }

        
        /// <summary>
        /// 获取字段名
        /// </summary>
        /// <param name="cellText">Excel单元格文本</param>
        /// <returns>字段名</returns>
        public string[] GetTableFieldName(string cellText)
        {
            cellText = cellText.Trim();
            cellText = cellText.Substring(1, cellText.Length - 2);

            if (cellText.StartsWith(ComConst.STR.BACKSLASH) || cellText.StartsWith(ComConst.STR.SLASH))
            {
                cellText = cellText.Remove(0, 1);
            }
            
            return cellText.Split(ComConst.STR.POINT.ToCharArray());
        }


        /// <summary>
        /// 增加分页符
        /// </summary>
        /// <param name="row"></param>
        public void AddPageBreak(int row)
        {
            ((Excel.Range)xlApp.Rows[row, Type.Missing]).PageBreak = (int)Excel.XlPageBreak.xlPageBreakManual;
        }


        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="rowSrc"></param>
        /// <param name="colSrc"></param>
        /// <param name="rowDest"></param>
        /// <param name="colDest"></param>
        public void RangeCopy(int srcX0,  int srxY0,  int srcX1,  int srcY1,
                              int destX0, int destY0, int destX1, int destY1)
        { 
            Excel.Range rngSrc = xlApp.get_Range(xlApp.Cells[srcX0, srxY0], xlApp.Cells[srcX1, srcY1]);
            Excel.Range rngDest = xlApp.get_Range(xlApp.Cells[destX0, destY0], xlApp.Cells[destX1, destY1]);

            rngSrc.Copy(rngDest);
        }


        /// <summary>
        /// 复制行
        /// </summary>
        /// <param name="srcRow0"></param>
        /// <param name="srcRow1"></param>
        /// <param name="destRow"></param>
        public void RowsCopy(int srcRow0, int srcRow1, int destRow)
        {            
            Excel.Worksheet xlSheet = (Excel.Worksheet)(xlApp.Worksheets.get_Item(1));

            for (int row = srcRow0; row <= srcRow1; row++)
            {
                Excel.Range rngSrc = (Excel.Range)(xlSheet.Rows.get_Item(row, Type.Missing));
                Excel.Range rngDest = (Excel.Range)(xlSheet.Rows.get_Item(destRow + row - srcRow0, Type.Missing));

                rngSrc.Copy(rngDest);
            }
        }


        public void RowsCopy(int srcRow0, int srcRow1, int destRow, string maxCol)
        {            
            Excel.Worksheet xlSheet = (Excel.Worksheet)(xlApp.Worksheets.get_Item(1));

            Excel.Range rngSrc;
            Excel.Range rngDest;
            
            for (int row = srcRow0; row <= srcRow1; row++)
            {
                rngSrc = (Excel.Range)(xlSheet.Rows.get_Item(row, Type.Missing));
                rngDest = (Excel.Range)(xlSheet.Rows.get_Item(destRow + row - srcRow0, Type.Missing));

                rngSrc.Copy(rngDest);
            }

            rngSrc = (Excel.Range)(xlSheet.get_Range("A" + srcRow0.ToString() + ":" + maxCol + srcRow1.ToString(), Type.Missing));
            rngDest = (Excel.Range)(xlSheet.get_Range("A" + destRow.ToString() + ":" + maxCol + (destRow + srcRow1 - srcRow0).ToString(), Type.Missing));

            rngSrc.Copy(rngDest);
        }
        #endregion
        

        #region 打印模板
        public bool PrintTemplate(DataSet dsData)
        {
            // 获取自定义表格区域
            int rowNum = 0;
            int colNum = 0;
            int rowStart = 0;
            int colStart = 0;
            if (GetExcelValRange(ref rowStart, ref rowNum, ref colStart, ref colNum) == false)
            {
                return false;
            }
            
            string tableName            = string.Empty;                 // 表名
            string fieldName            = string.Empty;                 // 字段名
            string fieldValue           = string.Empty;
            string[] tableField         = null;
            PlaceHolderPos[] arrPlace   = new PlaceHolderPos[100];
            int placeCount              = 0;

            ArrayList arrPlaceHolderPos = new ArrayList();
            int row = 0;

            for(row = rowStart; row <= rowNum; row++)
            {
                for(int col = colStart; col <= colNum; col++)
                {
                    string cellText = GetCellText(row, col);
                    
                    // 空字符串, 不处理
                    if (cellText.Trim().Length == 0)
                    {
                        // 判断是不是属于多位占位符中的某一列
                        for(int i = 0; i < placeCount; i++)
                        {
                            if (arrPlace[i].col == col && arrPlace[i].deleted == false)
                            {
                                DataTable dt = dsData.Tables[arrPlace[i].tableName];

                                if (arrPlace[i].dataRow < dt.Rows.Count)
                                {
                                    fieldValue = dt.Rows[arrPlace[i].dataRow][arrPlace[i].fieldName].ToString();
                                    SetCellText(row, col, fieldValue);

                                    arrPlace[i].dataRow++;
                                }
                                else
                                {
                                    arrPlace[i].deleted = true;
                                }

                                break;
                            }
                        }

                        continue;
                    }
                    
                    // 如果不是占位符不处理
                    switch(GetPlaceHolderType(cellText))
                    {
                        case PlaceHolderType.NONE:  // 如果不是占位符
                            continue;

                        case PlaceHolderType.SINGLE:// 单目占位符
                            tableField = GetTableFieldName(cellText);
                            tableName = tableField[0];
                            fieldName = tableField[1];
                            
                            fieldValue = dsData.Tables[tableName].Rows[0][fieldName].ToString();
                            SetCellText(row, col, fieldValue);
                            break;

                        case PlaceHolderType.MORE:  // 多目占位符
                            tableField = GetTableFieldName(cellText);
                            tableName = tableField[0];
                            fieldName = tableField[1];
                            
                            if (dsData.Tables[tableName].Rows.Count > 0)
                            {
                                fieldValue = dsData.Tables[tableName].Rows[0][fieldName].ToString();
                                SetCellText(row, col, fieldValue);
                                
                                // 记录位置
                                placeCount++;
                                arrPlace[placeCount - 1].deleted   = false;
                                arrPlace[placeCount - 1].col       = col;
                                arrPlace[placeCount - 1].tableName = tableName;
                                arrPlace[placeCount - 1].fieldName = fieldName;
                                arrPlace[placeCount - 1].dataRow   = 1;
                            }
                            
                            break;
                            
                        case PlaceHolderType.END: // 占位符结束
                            SetCellText(row, col, string.Empty);
                            
                            // 判断是不是属于多位占位符中的某一列
                            for(int i = 0; i < placeCount; i++)
                            {
                                if (arrPlace[i].col == col)
                                {
                                    if (arrPlace[i].deleted == false)
                                    {
                                        DataTable dt = dsData.Tables[arrPlace[i].tableName];

                                        if (arrPlace[i].dataRow < dt.Rows.Count)
                                        {
                                            fieldValue = dt.Rows[arrPlace[i].dataRow][arrPlace[i].fieldName].ToString();
                                            SetCellText(row, col, fieldValue);
                                        }
                                        else
                                        {
                                            SetCellText(row, col, string.Empty);
                                        }

                                        arrPlace[i].deleted = true;
                                    }
                                    else
                                    {
                                        SetCellText(row, col, string.Empty);
                                    }
                                    
                                    break;
                                }
                            }
                            
                            break;
                            
                        default:
                            break;
                    }
                }
            } 

            // 列表数据处理
            for(int i = 0; i < placeCount; i++)
            {
                if (arrPlace[i].deleted == false)
                {
                    DataTable dt = dsData.Tables[arrPlace[i].tableName];
                    
                    int startRow = row;
                    while (arrPlace[i].dataRow < dt.Rows.Count)
                    {
                        fieldValue = dt.Rows[arrPlace[i].dataRow][arrPlace[i].fieldName].ToString();
                        SetCellText(startRow++, arrPlace[i].col, fieldValue);
                        
                        arrPlace[i].dataRow++;
                    }
                }
            }

            return true;
        }


        /// <summary>
        /// 病人基本信息的录入
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="sex"></param>
        /// <param name="age"></param>
        /// <param name="dept"></param>
        /// <param name="bedNo"></param>
        /// <param name="date"></param>
        /// <param name="diagnosis"></param>
        /// <returns></returns>
        public void printPatientInfo(Patient_Info patientInfo)
        {
            SetCellText(5, 3, patientInfo.Name);
            SetCellText(5, 9, patientInfo.Gender);
            SetCellText(5, 13, patientInfo.Age);
            SetCellText(5, 17, patientInfo.DeptName);
            SetCellText(5, 23, patientInfo.BedNo);
            SetCellText(5, 27, patientInfo.Date);
            SetCellText(6, 4, patientInfo.Diagnosis);
        }


        /// <summary>
        /// 护理数据的录入
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public Print_Info printNursingData(string filter, DataRow[] drList, Graphics grfx, ref DataSet ds, int row, int page, Patient_Info patientInfo, bool isFirst)
        {
            Print_Info printInfo = new Print_Info();

            if (row == 10)
            {
                printPatientInfo(patientInfo);
            }

            for (int i = 0; i < drList.Length; i++)
            {
                int sqlRow = -1;

                DataRow dr = drList[i];
                string timePoint = dr["TIME_POINT"].ToString().Trim();

                if (i == 0)
                {
                    sqlRow = getSqlRow(timePoint, ref ds, row, page, dr, isFirst);
                }
                else
                {
                    sqlRow = getSqlRow(timePoint, ref ds, row, page, dr, true);
                }

                //判断病情观察需要占用几行
                string strValue = dr["NURSING"].ToString();
                System.Drawing.Font printFont = new Font("Arial", 11);
                float widthUnit = grfx.MeasureString("医", printFont).Width;
                float widthAll = 0.0F;
                int NURSING_WIDTH = 370;   // 像素
                int numPerRow = (int)(NURSING_WIDTH / widthUnit);
                string[] strSplit = strValue.Split('\r');

                for (int k = 0; k < strSplit.Length; k++)
                {
                    strSplit[k] = MergeBlank(strSplit[k]);
                    widthAll = strSplit[k].Length * widthUnit; //grfx.MeasureString(strSplit[k], printFont).Width;
                    int rowNumer = 0;

                    if (widthAll > NURSING_WIDTH)
                    {
                        rowNumer = (int)(widthAll / NURSING_WIDTH);
                        float mode = widthAll % NURSING_WIDTH;
                        if (mode > 0)
                        {
                            rowNumer += 1;
                        }
                    }
                    else
                    {
                        rowNumer += 1;
                    }

                    string str = string.Empty;

                    for (int j = 0; j < rowNumer; j++)
                    {
                        if ((row + j) > 32)
                        {
                            // 分页
                            row = 10;
                            page += 1;
                            str = getCutStr(strSplit[k].Substring(numPerRow * j), strSplit, k + 1);
                            DataRow[] drNew = getCutDataRow(ref dtNursingDataNew, filter, str, i); 
                            printInfo.DataRowList = drNew;
                            printInfo.RowNum = row;
                            printInfo.PageNum = page;
                            printInfo.IsShow = false;
                            printInfo.Pagination = true;
                            return printInfo;
                        }

                        if (j < rowNumer - 1)
                        {
                            str = strSplit[k].Substring(numPerRow * j, numPerRow);
                        }
                        else
                        {
                            str = strSplit[k].Substring(numPerRow * j); 
                        }

                        SetCellText(row + j, 19, str);
                    }

                    row += rowNumer;
                }

                if (sqlRow >= 0)
                {
                    DataRow dr1 = ds.Tables[0].Rows[sqlRow];
                    dr1["END_ROW"] = Convert.ToString(row - 1);
                    dr1["END_PAGE"] = Convert.ToString(page);
                }

                if (row > 32)         // 分页
                {
                    row = 10;
                    page += 1;
                    string str = drList[i + 1]["NURSING"].ToString(); 
                    DataRow[] drNew = getCutDataRow(ref dtNursingDataNew, filter, str, i + 1); 
                    printInfo.DataRowList = drNew;
                    printInfo.RowNum = row;
                    printInfo.PageNum = page;
                    printInfo.IsShow = true;
                    printInfo.Pagination = true;
                    return printInfo;
                }
            }

            printInfo.Pagination = false;
            return printInfo;
        }


        /// <summary>
        /// 获取病情观察记录的行号
        /// </summary>
        /// <param name="timePoint"></param>
        /// <param name="ds"></param>
        /// <param name="row"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private int getSqlRow(string timePoint, ref DataSet ds, int row, int page, DataRow dr, bool isFirst)
        {
            int sqlRow = -1;

            // 判断sql中护理记录是否和该条记录一致,若一致记录下占用的起始行行号
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow drSql = ds.Tables[0].Rows[i];
                string timePointStr = drSql["TIME_POINT"].ToString().Trim();

                if (timePointStr.Equals(timePoint) == true)
                {
                    sqlRow = i;

                    if (isFirst == true)
                    {
                        drSql["START_ROW"] = Convert.ToString(row);
                        drSql["START_PAGE"] = Convert.ToString(page);

                        // 显示日期等信息(除了病情观察)
                        SetCellText(row, 1, DataType.GetMD(dr["RECORDING_DATE"].ToString()));
                        SetCellText(row, 2, DataType.GetShortTime(dr["TIME_POINT"].ToString()));
                        SetCellText(row, 4, dr["TEMPERATURE"].ToString());
                        SetCellText(row, 5, dr["PULSE"].ToString());
                        SetCellText(row, 6, dr["BREATH"].ToString());
                        SetCellText(row, 7, dr["BLOOD"].ToString());
                        SetCellText(row, 11, dr["PROJECT"].ToString());
                        SetCellText(row, 13, dr["REAL_IN"].ToString());
                        SetCellText(row, 15, dr["PEE"].ToString());
                        SetCellText(row, 16, dr["STOOL"].ToString());
                        SetCellText(row, 29, dr["SIGN"].ToString());
                    }
                    
                    break;
                }
            }

            return sqlRow;
        }


        /// <summary>
        /// 获取截取的字符串
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="strSplit"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        private string getCutStr(string strValue, string[] strSplit, int start)
        {
            for (int i = start; i < strSplit.Length; i++)
            {
                strValue += ComConst.STR.CRLF + strSplit[i];
            }

            return strValue;
        }


        /// <summary>
        /// 获取截取后重组的DataRow集合
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="drList"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        private DataRow[] getCutDataRow(ref DataTable dt, string filter, string strValue, int start)
        {
            DataRow[] dr = dt.Select(filter);

            for (int i = 0; i < start; i++)
            {
                dr[i].Delete();
            }

            dr[start]["NURSING"] = strValue;
            dt.AcceptChanges(); 

            return dt.Select(filter);
        }
        #endregion


        #region 共通函数
        /// <summary>
        /// 将给定字符串中的连续多个空白字符合并,并且去掉换行符'\n'
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string MergeBlank(string strValue)
        {
            int index = strValue.IndexOf('\n');
            if (index >= 0)
            {
                strValue = strValue.Substring(index + 1);
            }

            string mergeBlankStr = string.Empty;
            string[] strBlank = strValue.Split(' ');
            mergeBlankStr = strBlank[0];

            for (int i = 1; i < strBlank.Length; i++)
            {
                if (strBlank[i].Length > 0)
                {
                    mergeBlankStr += ComConst.STR.BLANK + strBlank[i];
                }
            }

            return mergeBlankStr;
        }  


        /// <summary>
        /// 读取Excel配置的ini文件的一行
        /// </summary>
        /// <param name="line"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
		public static string GetConfigParts(string line, ref int row, ref int col)
		{
		    string[] arrParts = line.Split(ComConst.STR.BLANK.ToCharArray());
		    
		    string pos = string.Empty;
		    string fieldName = string.Empty;
		    
		    row = 0;
		    col = 0;
		    for(int i = 0; i < arrParts.Length; i++)
		    {
		        if (arrParts[i].Trim().Length > 0)
		        {
		            if (pos.Length == 0) 
		            { 
		                pos = arrParts[i]; 
                    }
                    else
                    {
                        fieldName = arrParts[i];
                        break;
                    }
		        }    
		    }
		    
		    // 获取行列
		    arrParts = pos.Split(":".ToCharArray());
		    if (arrParts.Length <= 1)
		    {
		        return string.Empty;
		    }
		    
		    row = int.Parse(arrParts[0]);   // 行号
		    col = GetCol(arrParts[1]);      // 列号
		    
		    return fieldName;
		}

        
        /// <summary>
        /// 获取Excel 中的列号
        /// </summary>
        /// <param name="colString"></param>
        /// <returns></returns>
		public static int GetCol(string colString)
		{
            int col = 0;
		    for(int i = 0; i < colString.Length; i++)
		    {
                switch(colString.Substring(i, 1).ToUpper())
                {
                    case "A": col += 1; break;
                    case "B": col += 2; break;
                    case "C": col += 3; break;
                    case "D": col += 4; break;
                    case "E": col += 5; break;
                    case "F": col += 6; break;
                    case "G": col += 7; break;
                    case "H": col += 8; break;
                    case "I": col += 9; break;
                    case "J": col += 10; break;
                    case "K": col += 11; break;
                    case "L": col += 12; break;
                    case "M": col += 13; break;
                    case "N": col += 14; break;
                    case "O": col += 15; break;
                    case "P": col += 16; break;
                    case "Q": col += 17; break;
                    case "R": col += 18; break;
                    case "S": col += 19; break;
                    case "T": col += 20; break;
                    case "U": col += 21; break;
                    case "V": col += 22; break;
                    case "W": col += 23; break;
                    case "X": col += 24; break;
                    case "Y": col += 25; break;
                    case "Z": col += 26; break;
                }
                
                if (colString.Length - 1 > i)
                {
                    col *= (colString.Length - 1 - i) * 26;
                }
		    }	
		    
		    return col;	    
		}
        #endregion
    }
}
