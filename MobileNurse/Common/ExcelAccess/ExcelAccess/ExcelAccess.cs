//------------------------------------------------------------------------------------
//
//  ϵͳ����        : ҽԺ��Ϣϵͳ
//  ��ϵͳ����      : ��ͨģ��
//  ��������        : 
//  ����            : ExcelAccess.cs
//  ���ܸ�Ҫ        : Excel������
//  ������          : ����
//  ������          : 2007-01-23
//  �汾            : 1.0.0.0
// 
//------------------< �����ʷ >------------------------------------------------------
//  �������        :
//  �����          :
//  �������        :
//  �汾            :
//------------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Collections;

namespace HISPlus
{
	/// <summary>
	/// ExcelAccess ��ժҪ˵����
	/// </summary>
	public class ExcelAccess
	{
		#region ����
		private Excel.Application	xlApp;										        // ExcelӦ�ó���
		private Excel.Workbook		xlWorkbook;									        // Excel��������Ĭ��ֻ��һ������Open([Template])����
        
		private bool				isVisibledExcel	= false;					        // ��ӡ��Ԥ��ʱ�Ƿ�Ҫ��ʾExcel����
		private string				formCaption		= string.Empty;				        // ��ӡԤ��Excel����ı�����
		
		private Object				oMissing        = System.Reflection.Missing.Value;	// ʵ������������
		#endregion

		
		public ExcelAccess()
		{
		}


		#region ����
		/// <summary>
		/// Excelʵ��
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
		/// Excel��������Ĭ��ֻ��һ������Open([Template])����
		/// </summary>
		public Excel.Workbook Workbooks
		{
			get
			{
				return xlWorkbook;
			}
		}


		/// <summary>
		/// ��ӡ��Ԥ��ʱ�Ƿ�Ҫ��ʾExcel����
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
		/// ��ӡԤ��Excel����ı�����
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
		

		#region �򿪹ر�
		/// <summary>
		/// ��Excel��������Ĭ�ϵ�Workbooks��
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
		/// �������й�����ģ��򿪣����ָ����ģ�岻���ڣ�����Ĭ�ϵĿ�ģ��
		/// </summary>
		/// <param name="templateFileName">����ģ��Ĺ������ļ���</param>
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
		/// �ر�
		/// </summary>
		/// <param name="blnQuit">�Ƿ��˳�</param>
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
		/// Excelʵ���˳�
		/// </summary>
		public void Quit()
		{
			// ���û��ʵ����Excel, ֱ����
			if (xlApp == null)
			{
				return;
			}

			// �ر�WorkBook �� Excelʵ��
            if (xlApp != null)
            {
                xlApp.Workbooks.Close();
                xlWorkbook = null;
			
                xlApp.Quit();
                xlApp = null;
            }

			oMissing = null;
			
			// ǿ���������գ�����ÿ��ʵ����Excel����Excell���̶�һ����
			System.GC.Collect();
		}
		#endregion


		#region ��ӡ��Ԥ��
		/// <summary>
		/// ��ʾExcel
		/// </summary>
		public void ShowExcel()
		{			
			xlApp.Visible = true;
		}
			

		/// <summary>
		/// ��Excel��ӡԤ�������Ҫ��ʾExcel���ڣ�������IsVisibledExcel 
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
        /// ���ô�ӡ��
        /// </summary>
        /// <param name="printerName"></param>
        public void SetActivePrinter(string printerName)
        {
            //���ô�ӡ��ǰ����Ĭ�ϴ�ӡ��
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
        /// �ָ�Ĭ�ϴ�ӡ��
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
		/// ��Excel��ӡ�����Ҫ��ʾExcel���ڣ�������IsVisibledExcel 
		/// </summary>
		public void Print()
		{
			xlApp.Visible = this.isVisibledExcel;	

			Object oMissing = System.Reflection.Missing.Value;  // ʵ������������

			try
			{
             	xlApp.ActiveWorkbook.PrintOut(oMissing,oMissing,oMissing,oMissing,oMissing,oMissing,oMissing,oMissing);	
			}
			catch{}
		}
		#endregion


		#region ���
		/// <summary>
		/// ��档�������ɹ����򷵻�true������������治�ɹ���������Ѵ����ļ�����ѡ���˲��滻Ҳ����false
		/// </summary>
		/// <param name="fileName">��Ҫ������ļ���</param>
		/// <param name="replaceExistsFileName">����ļ����ڣ����滻</param>
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
		

		#region ��Ԫ���ı�
		/// <summary>
		/// �����ı�
		/// </summary>
		/// <param name="row">Excel ���к�</param>
		/// <param name="col">Excel ���к�</param>
		/// <param name="text">�ı�</param>
		public void SetCellText(int row, int col, string text)
		{
            //if (text.IndexOf("\n") == 0)
            //{
            //    text = text.Substring(1);
            //}

			xlApp.Cells[row, col] = text;
		}

		
		/// <summary>
		/// ��ȡ��Ԫ����ı�
		/// </summary>
		/// <param name="row">Excel ���к�</param>
		/// <param name="col">Excel ���к�</param>
		/// <returns>��Ԫ����ı�</returns>
		public string GetCellText(int row, int col)
		{
			return ((Excel.Range)xlApp.Cells[row, col]).Text.ToString();
		}
		#endregion
		
		
        #region ͼƬ
        /// <summary>
        /// ����ͼƬ
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
        /// ����ͼƬ
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
        

        #region ���ò���
        /// <summary>
        /// ���ӷ�ҳ��
        /// </summary>
        /// <param name="row"></param>
        public void AddPageBreak(int row)
        {
            ((Excel.Range)xlApp.Rows[row, Type.Missing]).PageBreak = (int)Excel.XlPageBreak.xlPageBreakManual;
        }


        /// <summary>
        /// ����
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
        /// ���Ƶ�ճ����
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
        /// ��ճ���帴�Ƶ�Excel
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
        /// ��Ԫ��ϲ�
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
        /// ������
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
        /// �Զ������и�
        /// </summary>
        public void AuotFit(int x, int y, int x1, int y1)
        {
            Excel.Range rngSrc = xlApp.get_Range(xlApp.Cells[x, y], xlApp.Cells[x1, y1]);
            rngSrc.EntireRow.AutoFit();
        }
        #endregion
        

        #region ��ͨ����
        /// <summary>
        /// ��ȡExcel �е��к�
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
