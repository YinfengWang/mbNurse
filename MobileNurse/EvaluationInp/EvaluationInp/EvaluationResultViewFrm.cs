using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DXApplication2;
using SqlConvert = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class EvaluationResultViewFrm : FormDo,IBasePatient
    {
        #region �������
        private const string        ITEM_RECORDER       = "RECORDER";               // ��¼��
        private const string        ITEM_RECORD_TIME    = "RECORD_TIME";            // ��¼ʱ��
        protected string            _dict_id            = "01";                     // �ֵ�ID
        
        private EvaluationDbI       evaluationDbI       = null;                     // �����ӿ�
        private PatientDbI          patientDbI          = null;                     // ���˽ӿ�
        
        private DataSet             dsItemDict          = null;                     // ��Ŀ�ֵ�
        private DataSet             dsEvalRec           = null;                     // ������¼
        
        private DataSet             dsPatient           = null;                     // ������Ϣ
        private string              patientId           = string.Empty;             // ����ID��
        private string              visitId             = string.Empty;             // ���ξ������
        
        private ExcelAccess         excelAccess         = new ExcelAccess();
        private List<ExcelItem>     arrExcelItem        = new List<ExcelItem>();
        private string              _template           = "�������";
        
        private int                 printPageWidth      = 600;                     // ��ӡҳ����
        private TextBox             txtMeasure          = new TextBox();
        #endregion
        
        
        struct ExcelItem
        {
            public int    Row;
            public int    Col;
            public string ItemId;
            public string CheckValue;
        }
        
        
        public EvaluationResultViewFrm()
        {
            _id     = "00048";
            _guid   = "2484147B-CA0E-4497-A122-13E089B307D4";
            
            InitializeComponent();

            this.KeyPress += new KeyPressEventHandler( EvaluationResultViewFrm_KeyPress );
        }
        
        
        #region �����¼�
        private void EvaluationResultViewFrmec_Load( object sender, EventArgs e )
        {
            try
            {
                initFrmVal();
                initDisp();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        void IBasePatient.Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
            //patientId   = e.PatientId;
            //visitId     = e.VisitId;
            
            //// ��ȡ�����¼
            //dsPatient = patientDbI.GetInpPatientInfo_FromID(patientId, visitId);
            
            //// ��ʾ������¼
            //showEvaluationData();
            
            GVars.Patient.ID        = e.PatientId;
            GVars.Patient.VisitId   = e.VisitId;
            
            patientId = GVars.Patient.ID;
            visitId   = GVars.Patient.VisitId;

            btnQuery_Click(null, null);  
        }
                
        /// <summary>
        /// �����KeyPress�¼�
        /// </summary>
        /// <remarks>
        /// ��Ҫ����س����Tab
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EvaluationResultViewFrm_KeyPress( object sender, KeyPressEventArgs e )
        {
            try
            {
                // ����ǻس�, ת����Tab��
                if (e.KeyChar.Equals((char)Keys.Return))
                {
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                    
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
               
        
        /// <summary>
        /// ��ť[��ѯ]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click( object sender, EventArgs e )
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnQuery.Enabled = false;
                
                // ��ȡ�����¼
                dsPatient = patientDbI.GetInpPatientInfo_FromID(patientId, visitId);
                
                // ��ʾ������¼
                showEvaluationData();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                btnQuery.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }
        
        
        /// <summary>
        /// ��ť[��ӡ]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ExcelTemplatePrint();
            }
            catch(Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        

        /// <summary>
        /// ȷ��Ϊ������뷨
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void imeCtrl_GotFocus(object sender, EventArgs e)
        {
            IME.ChangeControlIme(this.ActiveControl.Handle);
        }
        
        
        /// <summary>
        /// ��ť[�˳�]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
        #endregion
        
        
        #region ��ͨ����
        /// <summary>
        /// ��ʼ���������
        /// </summary>
        private void initFrmVal()
        {
            patientDbI = new PatientDbI(GVars.OracleAccess);
            evaluationDbI = new EvaluationDbI(GVars.OracleAccess);
            
            dsItemDict = evaluationDbI.GetEvaluationDictItem(_dict_id);
            
            GVars.App.ReloadParameters();
            string result = GVars.App.GetParameters(GVars.User.DeptCode, GVars.User.ID, "PRINT_PAGE_WIDTH");
            int.TryParse(result, out printPageWidth);
        }   
        
        
        /// <summary>
        /// ��ʼ��������ʾ
        /// </summary>
        private void initDisp()
        {          
            // ��ռ�¼�б�
            txtContent.Text = string.Empty;
                        
            // �����ı���
            // txtContent.Width = printPageWidth;

            this.Controls.Add(txtMeasure);
            txtMeasure.Left = -10000;
            txtMeasure.Visible = true;
            txtMeasure.Multiline = true;
            txtMeasure.Width = printPageWidth;
            txtMeasure.ScrollBars = ScrollBars.Vertical;
            txtMeasure.Font = txtContent.Font;
        }

        
        /// <summary>
        /// ��ȡ������¼
        /// </summary>
        /// <returns></returns>
        private string getEvaluationData()
        {
            dsEvalRec = evaluationDbI.GetEvaluationRec(patientId, visitId, _dict_id, dtRngStart.Value.Date, dtRngEnd.Value.Date.AddDays(1));
            
            DataRow[] drFind = dsEvalRec.Tables[0].Select(string.Empty, "RECORD_DATE, ITEM_ID");
            
            string str_tab      = "    ";
            string str_line     = new string('-', 80);
            string recordTag    = string.Empty;                            // ��¼�����¼ʱ��
            string content      = string.Empty;                            // ����
            string itemIdGrpWNL = string.Empty;                            // ����ID
            string itemIdGrp    = string.Empty;                            // ����ID
            string itemIdPre    = string.Empty;                            // ǰһ��ID��
            bool   valPre       = false;                                   // ǰһ����Ŀ�Ƿ���ֵ
            
            StringBuilder sb = new StringBuilder();
            
            DateTime dtRecordA   = DataType.DateTime_Null();
            DateTime dtRecordPre = DataType.DateTime_Null();
            
            for(int i = 0; i < drFind.Length; i++)
            {
                DataRow dr = drFind[i];
                
                DateTime dtRecord = (DateTime)dr["RECORD_DATE"];
                
                if (i == 0)
                {
                    dtRecordPre = dtRecord;
                }
                
                // ������ڱ��
                //if (i > 0 && dtRecord.Date.CompareTo(dtRecordPre.Date) != 0)
                if (i > 0 && dtRecord.CompareTo(dtRecordPre) != 0)
                {
                    if (recordTag.Length > 0)
                    {
                        sb.Append(str_line);
                        
                        if (sb.Length > 0) sb.Append(ComConst.STR.CRLF);
                        
                        sb.Append(recordTag + ComConst.STR.CRLF);           // ��¼�����¼ʱ��
                        sb.Append(content.TrimStart() + ComConst.STR.CRLF); // ��¼����
                    }
                    
                    recordTag    = string.Empty;
                    content      = string.Empty;
                    
                    itemIdGrp    = string.Empty;
                    itemIdGrpWNL = string.Empty;
                    itemIdPre    = string.Empty;
                    
                    valPre       = false;
                    
                    dtRecordPre = dtRecord;
                }
                
                // ��ȡ����
                string itemId    = dr["ITEM_ID"].ToString();
                string itemName  = dr["ITEM_NAME"].ToString().ToUpper();
                string itemValue = dr["ITEM_VALUE"].ToString().Trim();
                
                switch(itemName)
                {
                    case ITEM_RECORD_TIME:                              // ��¼ʱ��
                        if (DateTime.TryParse(itemValue, out dtRecordA) == true)
                        {
                            if (recordTag.Length > 0)
                            {
                                recordTag = dtRecordA.ToString(ComConst.FMT_DATE.LONG_MINUTE) + ComConst.STR.BLANK + recordTag;
                            }
                            else
                            {
                                recordTag += dtRecordA.ToString(ComConst.FMT_DATE.LONG_MINUTE);
                            }
                        }
                        
                        break;
                        
                    case ITEM_RECORDER:                                 // ��¼��
                        recordTag += itemValue;
                        break;
                    
                    case "WNL":                                         // ����
                        itemIdGrpWNL = itemId.Substring(0, itemId.Length / 2);                        
                        itemIdGrp    = string.Empty;
                        
                        break;
                        
                    default:                                            // ����
                        if (itemIdGrpWNL.Length > 0 && itemId.StartsWith(itemIdGrpWNL))
                        {
                            break;
                        }
                        
                        // ���������
                        if (itemIdGrp.Length == 0 || itemId.StartsWith(itemIdGrp) == false)
                        {
                            valPre    = false;
                            itemIdPre = string.Empty;
                            itemIdGrp = itemId.Substring(0, 2);
                            content += ComConst.STR.CRLF + getGroupName(itemId) + ComConst.STR.CRLF + str_tab;
                        }
                        
                        if (itemName.Equals("EXCEPTION") == false && itemName.Trim().Length > 0)
                        {
                            if (itemId.Length > 2)
                            {
                                if (itemIdPre.Length > 0)
                                {
                                    if (itemIdPre.Length < itemId.Length && valPre == false)
                                    {
                                        if (content.EndsWith(ComConst.STR.CRLF + str_tab) == false)
                                        {
                                            content += ComConst.STR.COLON;
                                        }
                                    }
                                    
                                    if (itemIdPre.Length == itemId.Length)
                                    {
                                        content += ComConst.STR.COMMA;
                                    }
                                }
                                
                                content += (content.EndsWith(ComConst.STR.COLON)? string.Empty: ComConst.STR.BLANK) + itemName; 
                                
                                if (itemValue.Trim().Length > 0)
                                {
                                    content += (itemName.Length > 0 ? ComConst.STR.COLON : ComConst.STR.BLANK) + itemValue; 
                                    valPre = true;
                                }
                                else
                                {
                                    valPre = false;
                                }
                            }                            
                        }
                        else
                        {
                            content += itemValue; 
                        }
                        
                        break;
                }
                
                if (itemId.Length > 2)
                {
                    itemIdPre = itemId;
                }
                else
                {
                    itemIdPre = string.Empty;
                }
            }
            
            if (content.Length > 0)
            {
                sb.Append(str_line);
                sb.Append(ComConst.STR.CRLF);                           // ��¼�����¼ʱ��
                sb.Append(recordTag + ComConst.STR.CRLF);               // ��¼�����¼ʱ��
                sb.Append(content.TrimStart());                         // ��¼����
            }
            
            string result = sb.ToString();
            string blank4 = new string(' ', 4);
            string blank5 = new string(' ', 5);
            result = result.Replace(blank5 + ":", blank4);
            result = result.Replace(blank5, blank4); 
            
            return result;
        }
        
        
        /// <summary>
        /// ��ʾ���˵Ļ����¼
        /// </summary>
        private void showEvaluationData()
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                GVars.App.UserInput = false;
                this.txtContent.Text = getEvaluationData().Trim();
            }
            finally
            {               
                GVars.App.UserInput = blnStore;
            }
        }        
        
        
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        private string getGroupName(string itemId)
        {
            string filter = "ITEM_ID = " + SqlManager.SqlConvert(itemId.Substring(0, 2));
            DataRow[] drFind = dsItemDict.Tables[0].Select(filter);
            
            if (drFind.Length > 0)            
            {
                return drFind[0]["ITEM_NAME"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion        
        
        
        #region ��ӡ
        /// <summary>
		/// ��Excelģ���ӡ���Ƚ��ʺ��״򡢸�ʽ��ͳ�Ʒ�������ͼ�η������Զ����ӡ
		/// </summary>
		/// <remarks>��Excel��ӡ������Ϊ���򿪡�д���ݡ���ӡԤ�����ر�</remarks>
		private void ExcelTemplatePrint()
		{
		    // ����excel�ڵ�
            loadExcelItem(_template);
            
			string strExcelTemplateFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Template\\" + _template + ".xls");
            
			excelAccess.Open(strExcelTemplateFile);				//��ģ���ļ�
			excelAccess.IsVisibledExcel = true;
			excelAccess.FormCaption = string.Empty;	
		    
            string  itemId      = string.Empty;
            string  checkValue  = string.Empty;
            
            int     startCol    = 0;
            int     startRow    = 0;
            
            // ����̶���¼
            ExcelItem excelItem;
            for(int i = 0; i < arrExcelItem.Count; i++)
            {
                excelItem = arrExcelItem[i];
                
                if (getVariableValue(excelItem.ItemId, ref checkValue) == true)
                {
                    excelAccess.SetCellText(excelItem.Row, excelItem.Col, checkValue);
                    continue;
                }
                
                if (excelItem.ItemId.Equals("START_POS") == true)
                {
                    startCol = excelItem.Col;
                    startRow = excelItem.Row;
                }
            }
            
            
            if (startCol > 0 && startRow > 0)
            {
                txtMeasure.Text = txtContent.Text;

                int lineCount = Win32API.SendMessageA((int)(txtMeasure.Handle), Win32API.EM_GETLINECOUNT, 0, 0);
                for(int i = 0; i < lineCount; i++)
                {
                    string line = getLine(txtMeasure, i);
                    
                    excelAccess.SetCellText(startRow + i, startCol, line);
                }
            }
            
			//excel.Print();				           //��ӡ
			excelAccess.PrintPreview();			       //Ԥ��
            
			excelAccess.Close(false);				   //�رղ��ͷ�			
		}
		
		
        private void loadExcelItem(string templateFileName)
        {
            arrExcelItem.Clear();
            
		    // ��ȡ�����ļ�
		    string iniFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Template\\" + templateFileName + ".ini");
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
	                // ��ȡ����
	                if (getParts(line, ref row, ref col, ref itemId, ref checkValue) == false)
	                {
	                    continue;
	                }
                    
                    ExcelItem excelItem = new ExcelItem();
                    excelItem.Row       = row;
                    excelItem.Col        = col;
                    excelItem.ItemId     = itemId;
                    excelItem.CheckValue = checkValue;
                    
                    arrExcelItem.Add(excelItem);
                }
                
	            sr.Close();
	            sr.Dispose();
		    }
        }
        
        
		/// <summary>
		/// ��ȡ�����ļ���һ��
		/// </summary>
		/// <param name="line"></param>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <returns></returns>				
		private bool getParts(string line, ref int row, ref int col, ref string itemid, ref string checkValue)
		{
		    line = line.Replace(ComConst.STR.BLANK, string.Empty);
		    string[] arrParts = line.Split(ComConst.STR.COMMA.ToCharArray());
		    
		    if (arrParts.Length < 3) return false;
		    
		    itemid      = arrParts[1];
		    checkValue  = arrParts[2];
		    
		    // ��ȡ����
		    arrParts = arrParts[0].Split(":".ToCharArray());
		    if (arrParts.Length <= 1)
		    {
		        return false;
		    }
		    
		    // �к�
		    if (int.TryParse(arrParts[0], out row) == false)
		    {
		        return false;
		    }
		    
		    // �к�
		    col = ExcelAccess.GetCol(arrParts[1]);      
		    
		    return true;
		}
        
        
        private bool getItemAttr(string itemId, ref int row, ref int col, ref string checkValue)
        {
            ExcelItem excelItem;
            for(int i = 0; i < arrExcelItem.Count; i++)
            {
                excelItem = arrExcelItem[i];
                if (excelItem.ItemId.Equals(itemId) == true)
                {
                    row = excelItem.Row;
                    col = excelItem.Col;
                    checkValue = excelItem.CheckValue;
                    
                    arrExcelItem.Remove(excelItem);
                    return true;
                }
            }
            
            return false;
        }
        
                		
		/// <summary>
		/// ��ȡ�����ļ���һ��
		/// </summary>
		/// <param name="line"></param>
		/// <param name="row"></param>
		/// <param name="col"></param>
		/// <returns></returns>				
		private bool getVariableValue(string variable, ref string variableValue)
		{
		    if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
		    {
		        return false;
		    }   
		    
		    // ���䵥������
		    switch(variable)
		    {
		        case "AGE":
		            if (dsPatient.Tables[0].Rows[0]["DATE_OF_BIRTH"].ToString().Length > 0)
		            {
		                DateTime dt = (DateTime)dsPatient.Tables[0].Rows[0]["DATE_OF_BIRTH"];
    		            
		                variableValue = PersonCls.GetAgeYear(dt, GVars.OracleAccess.GetSysDate()).ToString();
		                return true;
		            }
		            
		            return false;
                case "PRINT_DATE":
                    variableValue = GVars.OracleAccess.GetSysDate().ToString(ComConst.FMT_DATE.LONG);
                    return true;
                    
                case "PRINT_NURSE":
                    variableValue = GVars.User.Name;
                    return true;
                
                case "CONTENT":
                    variableValue = txtContent.Text;
                    return true;
                    
		        case ITEM_RECORDER:
		            variableValue = GVars.User.Name;
		            return true;
		        case ITEM_RECORD_TIME:
		            variableValue = DateTime.Now.ToString(ComConst.FMT_DATE.LONG);
		            return true;    
                default:
                    break;
		    }
		    
		    // �������˻�����Ϣ
		    if (dsPatient.Tables[0].Columns.Contains(variable) == true)
		    {
		        variableValue = dsPatient.Tables[0].Rows[0][variable].ToString();
		        return true;
		    }
		    
		    return false;
		}
		
		
		private string getLine(TextBoxBase textBox, int index)
		{
		    StringBuilder buff = new StringBuilder("\uffff".PadRight(0xffff));
            int count = Win32API.SendMessage((int)(textBox.Handle), Win32API.EM_GETLINE, index, buff);
		    return buff.ToString(0, count);
		}
        #endregion


        void IBasePatient.Patient_ListRefresh(object sender, PatientEventArgs e)
        {
            
        }
    }
}