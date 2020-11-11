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
using System.Data;
using System.Drawing;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;

namespace HISPlus
{
	/// <summary>
	/// ExcelAccess ��ժҪ˵����
	/// </summary>
	public class ExcelAccess
	{
        #region �Զ���
        /// <summary>
        /// ռλ������
        /// </summary>
        public enum PlaceHolderType
        {
            NONE    = 0,                // ����ռλ��
            SINGLE  = 1,                // ��Ŀռλ��
            MORE    = 2,                // ��Ŀռλ��
            END     = 3                 // ��Ŀռλ������
        }

        
        /// <summary>
        /// ռλ��λ��
        /// </summary>
        public struct PlaceHolderPos
        {
            public int     col;         // ����
            public int     dataRow;     // ��ĵڼ���
            public string  tableName;   // ����
            public string  fieldName;   // �ֶ���
            public bool    deleted;     // �Ƿ�ɾ��
        }
        #endregion


		#region ����
		private Excel.Application	xlApp;										// ExcelӦ�ó���
		private Excel.Workbook		xlWorkbook;									// Excel��������Ĭ��ֻ��һ������Open([Template])����

		private bool				isVisibledExcel	= false;					// ��ӡ��Ԥ��ʱ�Ƿ�Ҫ��ʾExcel����
		private string				formCaption		= string.Empty;				// ��ӡԤ��Excel����ı�����
		
		private Object				oMissing = System.Reflection.Missing.Value;	// ʵ������������
        private DataTable dtNursingDataNew;                                     // ��ҳ��ȡ��Ļ����¼��
        
        public Patient_Info PatientInfo = new Patient_Info();
        public Print_Info PrintInfo = new Print_Info();
       
		#endregion


        #region �ṹ��
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


        /// <summary>
        /// ��ҳ��ȡ��Ļ����¼��
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

			xlApp.Cells[row, col] = text.Trim();
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
        

        #region ���ò���
        /// <summary>
        /// Excel��Ч��Χ
        /// </summary>
        /// <param name="startRow">��ʼ��</param>
        /// <param name="endRow">������</param>
        /// <param name="startCol">��ʼ��</param>
        /// <param name="endCol">������</param>
        /// <returns>TRUE: �ɹ�; FALSE: ʧ��</returns>
        public bool GetExcelValRange(ref int startRow, ref int endRow, ref int startCol, ref int endCol)
        {
            string region       = GetCellText(1, 1);
            string[] arrRange   = null;
            string[] arrVal     = null;
            try
            {
                // ��ȡ���еķ�Χ
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
                
                // ��ȡ���е�ֵ
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
        /// �ж��ǲ���ռλ��
        /// </summary>
        /// <param name="text">Excel�ı��е��ַ�</param>
        /// <returns>TRUE: ��; FALSE: ����</returns>
        public PlaceHolderType GetPlaceHolderType(string cellText)
        {
            // Ԥ����
            cellText = cellText.Trim();

            // �������2���ַ�
            if (cellText.Length < 3)
            {
                return PlaceHolderType.NONE;
            }
                        
            // ģʽ [*]
            if (cellText.StartsWith(ComConst.STR.SQUARE_BRACKET_L) && cellText.EndsWith(ComConst.STR.SQUARE_BRACKET_R))
            {
                return PlaceHolderType.SINGLE;
            }
            
            // ģʽ {*}
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
        /// ��ȡ�ֶ���
        /// </summary>
        /// <param name="cellText">Excel��Ԫ���ı�</param>
        /// <returns>�ֶ���</returns>
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
        #endregion
        

        #region ��ӡģ��
        public bool PrintTemplate(DataSet dsData)
        {
            // ��ȡ�Զ���������
            int rowNum = 0;
            int colNum = 0;
            int rowStart = 0;
            int colStart = 0;
            if (GetExcelValRange(ref rowStart, ref rowNum, ref colStart, ref colNum) == false)
            {
                return false;
            }
            
            string tableName            = string.Empty;                 // ����
            string fieldName            = string.Empty;                 // �ֶ���
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
                    
                    // ���ַ���, ������
                    if (cellText.Trim().Length == 0)
                    {
                        // �ж��ǲ������ڶ�λռλ���е�ĳһ��
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
                    
                    // �������ռλ��������
                    switch(GetPlaceHolderType(cellText))
                    {
                        case PlaceHolderType.NONE:  // �������ռλ��
                            continue;

                        case PlaceHolderType.SINGLE:// ��Ŀռλ��
                            tableField = GetTableFieldName(cellText);
                            tableName = tableField[0];
                            fieldName = tableField[1];
                            
                            fieldValue = dsData.Tables[tableName].Rows[0][fieldName].ToString();
                            SetCellText(row, col, fieldValue);
                            break;

                        case PlaceHolderType.MORE:  // ��Ŀռλ��
                            tableField = GetTableFieldName(cellText);
                            tableName = tableField[0];
                            fieldName = tableField[1];
                            
                            if (dsData.Tables[tableName].Rows.Count > 0)
                            {
                                fieldValue = dsData.Tables[tableName].Rows[0][fieldName].ToString();
                                SetCellText(row, col, fieldValue);
                                
                                // ��¼λ��
                                placeCount++;
                                arrPlace[placeCount - 1].deleted   = false;
                                arrPlace[placeCount - 1].col       = col;
                                arrPlace[placeCount - 1].tableName = tableName;
                                arrPlace[placeCount - 1].fieldName = fieldName;
                                arrPlace[placeCount - 1].dataRow   = 1;
                            }
                            
                            break;
                            
                        case PlaceHolderType.END: // ռλ������
                            SetCellText(row, col, string.Empty);
                            
                            // �ж��ǲ������ڶ�λռλ���е�ĳһ��
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

            // �б����ݴ���
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
        /// ���˻�����Ϣ��¼��
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
        /// �������ݵ�¼��
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

                //�жϲ���۲���Ҫռ�ü���
                string strValue = dr["NURSING"].ToString();
                System.Drawing.Font printFont = new Font("Arial", 11);
                float widthUnit = grfx.MeasureString("ҽ", printFont).Width;
                float widthAll = 0.0F;
                int NURSING_WIDTH = 370;   // ����
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
                            // ��ҳ
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

                if (row > 32)         // ��ҳ
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
        /// ��ȡ����۲��¼���к�
        /// </summary>
        /// <param name="timePoint"></param>
        /// <param name="ds"></param>
        /// <param name="row"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private int getSqlRow(string timePoint, ref DataSet ds, int row, int page, DataRow dr, bool isFirst)
        {
            int sqlRow = -1;

            // �ж�sql�л����¼�Ƿ�͸�����¼һ��,��һ�¼�¼��ռ�õ���ʼ���к�
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

                        // ��ʾ���ڵ���Ϣ(���˲���۲�)
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
        /// ��ȡ��ȡ���ַ���
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
        /// ��ȡ��ȡ�������DataRow����
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


        #region ��ͨ����
        /// <summary>
        /// �������ַ����е���������հ��ַ��ϲ�,����ȥ�����з�'\n'
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
        /// ��ȡExcel���õ�ini�ļ���һ��
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
		    
		    // ��ȡ����
		    arrParts = pos.Split(":".ToCharArray());
		    if (arrParts.Length <= 1)
		    {
		        return string.Empty;
		    }
		    
		    row = int.Parse(arrParts[0]);   // �к�
		    col = GetCol(arrParts[1]);      // �к�
		    
		    return fieldName;
		}

        
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
		    
		    return col;	    
		}
        #endregion
    }
}
