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
using System.Drawing;
using System.Collections;

namespace HISPlus
{
	/// <summary>
	/// ExcelAccess 的摘要说明。
	/// </summary>
	public class ExcelAccess
	{
		#region 变量
		private Excel.Application	xlApp;										        // Excel应用程序
		private Excel.Workbook		xlWorkbook;									        // Excel工作薄，默认只有一个，用Open([Template])创建
        
		private bool				isVisibledExcel	= false;					        // 打印或预览时是否还要显示Excel窗体
		private string				formCaption		= string.Empty;				        // 打印预览Excel窗体的标题栏
		
		private Object				oMissing        = System.Reflection.Missing.Value;	// 实例化参数对象
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

        private string defaultPrinter;
        /// <summary>
        /// 设置打印机
        /// </summary>
        /// <param name="printerName"></param>
        public void SetActivePrinter(string printerName)
        {
            //设置打印机前保存默认打印机
            defaultPrinter = this.xlApp.ActivePrinter;
            try
            {
                this.xlApp.ActivePrinter = printerName;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 恢复默认打印机
        /// </summary>
        public void RestoreDefaultPrinter()
        {
            if (!string.IsNullOrEmpty(defaultPrinter) && !this.xlApp.ActivePrinter.Equals(defaultPrinter))
                this.xlApp.ActivePrinter = defaultPrinter;
        }

        public string GetActivePrinter()
        {
            return xlApp.ActivePrinter;
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

			xlApp.Cells[row, col] = text;
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
		
		
        #region 图片
        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="pic"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void InsertPicture(string fileName, float left, float top, float width, float height)
        {
            Excel.Worksheet ws = xlApp.ActiveSheet as Excel.Worksheet;
            ws.Shapes.AddPicture(fileName, Office.MsoTriState.msoTrue, Office.MsoTriState.msoTrue, left, top, width, height);
        }


        /// <summary>
        /// 插入图片
        /// </summary>
        public void InsertPicture(string fileName, int row, int col)
        {
            Excel.Range rng = xlApp.get_Range(xlApp.Cells[1, 1], xlApp.Cells[row - 1, col - 1]);
            Image bmp = Bitmap.FromFile(fileName);

            Excel.Worksheet ws = xlApp.ActiveSheet as Excel.Worksheet;
            float left = float.Parse(rng.Width.ToString());
            float top = float.Parse(rng.Height.ToString());
            float width = (float)(bmp.Width);
            float height = (float)(bmp.Height);
            
            ws.Shapes.AddOLEObject(oMissing, fileName, true, false, oMissing, oMissing, oMissing, left, top, width, height);
            //ws.Shapes.AddPicture(fileName, Office.MsoTriState.msoTrue, Office.MsoTriState.msoTrue, left, top, width, height);
            
            bmp.Dispose();
        }
        
        
        public void InsertPicture(int row, int col, string fileName)
        {
            Excel.Worksheet ws  = xlApp.ActiveSheet as Excel.Worksheet;
            Excel.Pictures pics = (Excel.Pictures)((Excel.Worksheet)(xlApp.ActiveSheet)).Pictures(oMissing);
            Excel.Picture  pic  = pics.Insert(fileName, oMissing);

            Excel.Range rng = (Excel.Range)(ws.Cells[row, col]);
            pic.Left = float.Parse(rng.Left.ToString());
            pic.Top  = float.Parse(rng.Top.ToString());
        }
        #endregion
        

        #region 常用操作
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
        /// 复制到粘贴板
        /// </summary>
        /// <param name="rowSrc"></param>
        /// <param name="colSrc"></param>
        /// <param name="rowDest"></param>
        /// <param name="colDest"></param>
        public void RangeCopy(int x, int y, int x1, int y1)
        {
            Excel.Range rngSrc = xlApp.get_Range(xlApp.Cells[x, y], xlApp.Cells[x1, y1]);
            rngSrc.Select();
            rngSrc.Copy(Type.Missing);
        }


        /// <summary>
        /// 从粘贴板复制到Excel
        /// </summary>
        /// <param name="rowSrc"></param>
        /// <param name="colSrc"></param>
        /// <param name="rowDest"></param>
        /// <param name="colDest"></param>
        public void RangePaste(int x, int y, int x1, int y1)
        {
            Excel.Range rngSrc = xlApp.get_Range(xlApp.Cells[x, y], xlApp.Cells[x1, y1]);
            rngSrc.Select();
            
            ((Excel.Worksheet)xlApp.ActiveSheet).Paste(Type.Missing, Type.Missing);
            rngSrc.Copy(Type.Missing);
        }
        
        
        /// <summary>
        /// 单元格合并
        /// </summary>
        public void RangMerge(int x, int y, int x1, int y1)
        {
            Excel.Range rngSrc = xlApp.get_Range(xlApp.Cells[x, y], xlApp.Cells[x, y]);
            object horizon  = rngSrc.HorizontalAlignment;
            object verital  = rngSrc.VerticalAlignment;
            object wrap     = rngSrc.WrapText;
            
            rngSrc = xlApp.get_Range(xlApp.Cells[x, y], xlApp.Cells[x1, y1]);
            rngSrc.Merge(Type.Missing);
            
            rngSrc.WrapText = wrap;
            rngSrc.VerticalAlignment    = verital;
            rngSrc.HorizontalAlignment  = horizon;
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
        
        
        /// <summary>
        /// 自动调整行高
        /// </summary>
        public void AuotFit(int x, int y, int x1, int y1)
        {
            Excel.Range rngSrc = xlApp.get_Range(xlApp.Cells[x, y], xlApp.Cells[x1, y1]);
            rngSrc.EntireRow.AutoFit();
        }
        #endregion
        

        #region 共通函数
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
		    
		    if (col == 0)
		    {   
		        col = 1;
		    }
		    
		    return col;
		}
        #endregion
    }
}
