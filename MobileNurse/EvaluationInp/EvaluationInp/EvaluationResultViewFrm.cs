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
        #region 窗体变量
        private const string        ITEM_RECORDER       = "RECORDER";               // 记录人
        private const string        ITEM_RECORD_TIME    = "RECORD_TIME";            // 记录时间
        protected string            _dict_id            = "01";                     // 字典ID
        
        private EvaluationDbI       evaluationDbI       = null;                     // 评估接口
        private PatientDbI          patientDbI          = null;                     // 病人接口
        
        private DataSet             dsItemDict          = null;                     // 项目字典
        private DataSet             dsEvalRec           = null;                     // 评估记录
        
        private DataSet             dsPatient           = null;                     // 病人信息
        private string              patientId           = string.Empty;             // 病人ID号
        private string              visitId             = string.Empty;             // 本次就诊序号
        
        private ExcelAccess         excelAccess         = new ExcelAccess();
        private List<ExcelItem>     arrExcelItem        = new List<ExcelItem>();
        private string              _template           = "评估结果";
        
        private int                 printPageWidth      = 600;                     // 打印页面宽度
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
        
        
        #region 窗体事件
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
            
            //// 获取护理记录
            //dsPatient = patientDbI.GetInpPatientInfo_FromID(patientId, visitId);
            
            //// 显示评估记录
            //showEvaluationData();
            
            GVars.Patient.ID        = e.PatientId;
            GVars.Patient.VisitId   = e.VisitId;
            
            patientId = GVars.Patient.ID;
            visitId   = GVars.Patient.VisitId;

            btnQuery_Click(null, null);  
        }
                
        /// <summary>
        /// 窗体的KeyPress事件
        /// </summary>
        /// <remarks>
        /// 主要处理回车变成Tab
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EvaluationResultViewFrm_KeyPress( object sender, KeyPressEventArgs e )
        {
            try
            {
                // 如果是回车, 转换成Tab键
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
        /// 按钮[查询]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click( object sender, EventArgs e )
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnQuery.Enabled = false;
                
                // 获取护理记录
                dsPatient = patientDbI.GetInpPatientInfo_FromID(patientId, visitId);
                
                // 显示评估记录
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
        /// 按钮[打印]
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
        /// 确保为半角输入法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void imeCtrl_GotFocus(object sender, EventArgs e)
        {
            IME.ChangeControlIme(this.ActiveControl.Handle);
        }
        
        
        /// <summary>
        /// 按钮[退出]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
        #endregion
        
        
        #region 共通函数
        /// <summary>
        /// 初始化窗体变量
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
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {          
            // 清空记录列表
            txtContent.Text = string.Empty;
                        
            // 测试文本框
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
        /// 获取评估记录
        /// </summary>
        /// <returns></returns>
        private string getEvaluationData()
        {
            dsEvalRec = evaluationDbI.GetEvaluationRec(patientId, visitId, _dict_id, dtRngStart.Value.Date, dtRngEnd.Value.Date.AddDays(1));
            
            DataRow[] drFind = dsEvalRec.Tables[0].Select(string.Empty, "RECORD_DATE, ITEM_ID");
            
            string str_tab      = "    ";
            string str_line     = new string('-', 80);
            string recordTag    = string.Empty;                            // 记录人与记录时间
            string content      = string.Empty;                            // 内容
            string itemIdGrpWNL = string.Empty;                            // 分组ID
            string itemIdGrp    = string.Empty;                            // 分组ID
            string itemIdPre    = string.Empty;                            // 前一个ID号
            bool   valPre       = false;                                   // 前一个项目是否有值
            
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
                
                // 如果日期变更
                //if (i > 0 && dtRecord.Date.CompareTo(dtRecordPre.Date) != 0)
                if (i > 0 && dtRecord.CompareTo(dtRecordPre) != 0)
                {
                    if (recordTag.Length > 0)
                    {
                        sb.Append(str_line);
                        
                        if (sb.Length > 0) sb.Append(ComConst.STR.CRLF);
                        
                        sb.Append(recordTag + ComConst.STR.CRLF);           // 记录人与记录时间
                        sb.Append(content.TrimStart() + ComConst.STR.CRLF); // 记录内容
                    }
                    
                    recordTag    = string.Empty;
                    content      = string.Empty;
                    
                    itemIdGrp    = string.Empty;
                    itemIdGrpWNL = string.Empty;
                    itemIdPre    = string.Empty;
                    
                    valPre       = false;
                    
                    dtRecordPre = dtRecord;
                }
                
                // 获取内容
                string itemId    = dr["ITEM_ID"].ToString();
                string itemName  = dr["ITEM_NAME"].ToString().ToUpper();
                string itemValue = dr["ITEM_VALUE"].ToString().Trim();
                
                switch(itemName)
                {
                    case ITEM_RECORD_TIME:                              // 记录时间
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
                        
                    case ITEM_RECORDER:                                 // 记录人
                        recordTag += itemValue;
                        break;
                    
                    case "WNL":                                         // 正常
                        itemIdGrpWNL = itemId.Substring(0, itemId.Length / 2);                        
                        itemIdGrp    = string.Empty;
                        
                        break;
                        
                    default:                                            // 其它
                        if (itemIdGrpWNL.Length > 0 && itemId.StartsWith(itemIdGrpWNL))
                        {
                            break;
                        }
                        
                        // 如果分组变更
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
                sb.Append(ComConst.STR.CRLF);                           // 记录人与记录时间
                sb.Append(recordTag + ComConst.STR.CRLF);               // 记录人与记录时间
                sb.Append(content.TrimStart());                         // 记录内容
            }
            
            string result = sb.ToString();
            string blank4 = new string(' ', 4);
            string blank5 = new string(' ', 5);
            result = result.Replace(blank5 + ":", blank4);
            result = result.Replace(blank5, blank4); 
            
            return result;
        }
        
        
        /// <summary>
        /// 显示病人的护理记录
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
        /// 获取分组名称
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
        
        
        #region 打印
        /// <summary>
		/// 用Excel模板打印，比较适合套打、格式、统计分析报表、图形分析、自定义打印
		/// </summary>
		/// <remarks>用Excel打印，步骤为：打开、写数据、打印预览、关闭</remarks>
		private void ExcelTemplatePrint()
		{
		    // 加载excel节点
            loadExcelItem(_template);
            
			string strExcelTemplateFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Template\\" + _template + ".xls");
            
			excelAccess.Open(strExcelTemplateFile);				//用模板文件
			excelAccess.IsVisibledExcel = true;
			excelAccess.FormCaption = string.Empty;	
		    
            string  itemId      = string.Empty;
            string  checkValue  = string.Empty;
            
            int     startCol    = 0;
            int     startRow    = 0;
            
            // 输出固定记录
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
            
			//excel.Print();				           //打印
			excelAccess.PrintPreview();			       //预览
            
			excelAccess.Close(false);				   //关闭并释放			
		}
		
		
        private void loadExcelItem(string templateFileName)
        {
            arrExcelItem.Clear();
            
		    // 读取配置文件
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
                    
                    arrExcelItem.Add(excelItem);
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
		private bool getParts(string line, ref int row, ref int col, ref string itemid, ref string checkValue)
		{
		    line = line.Replace(ComConst.STR.BLANK, string.Empty);
		    string[] arrParts = line.Split(ComConst.STR.COMMA.ToCharArray());
		    
		    if (arrParts.Length < 3) return false;
		    
		    itemid      = arrParts[1];
		    checkValue  = arrParts[2];
		    
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
		/// 获取配置文件的一行
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
		    
		    // 年龄单独处理
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
		    
		    // 其它病人基本信息
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