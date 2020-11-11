using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class PIORecFrmOld : FormDo1
    {
        #region 变量
        private PatientListWardFrm  patientListFrm  = null;             // 病人列表
        private PatientSearchFrm    patientInfoFrm  = null;             // 病人信息
        
        protected string      _template     = "护理计划表";             // 模板文件
        
        private PatientDbI    patientDbI    = null;                     // 病人信息接口
        private DataSet       dsPatient     = null;                     // 病人信息
        private string        patientId     = string.Empty;             // 病人ID号
        private string        visitId       = string.Empty;             // 本次就诊序号
        
        private ExcelAccess   excelAccess   = new ExcelAccess();
        private List<ExcelItem> arrExcelItem  = new List<ExcelItem>();
        private int           printCols     = 7;
        
        private DbInfo          dbDbI       = null;
        private DataSet         dsScheduleRec   = null;
        
        private PIOSettingFrm   pioSelection    = null;
        #endregion
        
        
        struct ExcelItem
        {
            public int    Row;
            public int    Col;
            public string ItemId;
            public string CheckValue;
        }
        
        
        public PIORecFrmOld()
        {
            InitializeComponent();
            
            _id     = "00058";
            _guid   = "171F3931-6910-4da8-8625-D4123765F076";
        }
        
        
        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EvaluationEverydayFrm_Load(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled         = false;
                btnPrint.Enabled        = false;
                
                timer1.Enabled = true;                
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EvaluationEverydayFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {                
                grpPatientList.Controls.Remove(patientListFrm);
                patientListFrm.Close();
                patientListFrm.Dispose();
                
                grpPatientInfo.Controls.Remove(patientInfoFrm);
                patientInfoFrm.Close();
                patientInfoFrm.Dispose();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 时钟消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            
            this.Cursor = Cursors.WaitCursor;
            
            try
            {
                GVars.App.UserInput = false;
                
                dbDbI = new DbInfo(GVars.OracleAccess);
                patientDbI = new PatientDbI(GVars.OracleAccess);
                
                dgvNursingSchedule.AutoGenerateColumns = false;
                
                pioSelection = new PIOSettingFrm();
                pioSelection.SelectionMode = true;
                pioSelection.Text = "护理计划选择";
                
                // 增加病人列表
                patientListFrm = new PatientListWardFrm();
                patientListFrm.DeptCode = GVars.User.DeptCode;
                patientListFrm.FormBorderStyle = FormBorderStyle.None;
                patientListFrm.TopLevel = false;
                grpPatientList.Controls.Add(patientListFrm);
                patientListFrm.Dock = DockStyle.Fill;
                patientListFrm.Show();
                patientListFrm.PatientChanged += new PatientChangedEventHandler(patientListFrm_PatientChanged);
                patientListFrm.BackColor = this.BackColor;
                
                // 病人信息
                patientInfoFrm = new PatientSearchFrm();
                patientInfoFrm.FormBorderStyle = FormBorderStyle.None;
                patientInfoFrm.TopLevel = false;
                grpPatientInfo.Controls.Add(patientInfoFrm);
                patientInfoFrm.Dock = DockStyle.Fill;
                patientInfoFrm.Show();
                patientInfoFrm.PatientChanged += new PatientChangedEventHandler(patientInfoFrm_PatientChanged);
                patientInfoFrm.BackColor = this.BackColor;
                
                if (patientListFrm.PatientId.Length > 0)
                {
                    patientListFrm_PatientChanged(null, new PatientEventArgs(patientListFrm.PatientId, patientListFrm.VisitId));
                }
                //patientChanged();
            }
            catch(Exception ex)
            {
                timer1.Enabled = false;
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                timer1.Enabled = false;
                GVars.App.UserInput = blnStore;
            }
        }


        void patientInfoFrm_PatientChanged(object sender, PatientEventArgs e)
        {
            patientListFrm.LocatePatient(e.PatientId);
            
            GVars.Patient.ID        = e.PatientId;
            GVars.Patient.VisitId   = e.VisitId;
            
            patientId = GVars.Patient.ID;
            visitId   = GVars.Patient.VisitId;
            
            btnQuery_Click(null, null);
        }

        
        void patientListFrm_PatientChanged(object sender, PatientEventArgs e)
        {
            patientInfoFrm.ShowPatientInfo(e.PatientId, e.VisitId);
            
            GVars.Patient.ID        = e.PatientId;
            GVars.Patient.VisitId   = e.VisitId;
            
            patientId = GVars.Patient.ID;
            visitId   = GVars.Patient.VisitId;
        }
        
        
        private void dgvNursingSchedule_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                dgvNursingSchedule.Rows[e.RowIndex].Cells["SERIAL_NO_SHOW"].Value = (e.RowIndex + 1).ToString();
            }
            catch
            {}
        }
            
            
        private void dgvNursingSchedule_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                btnDelete.Enabled = dgvNursingSchedule.SelectedRows.Count > 0;
                btnStop.Enabled   = dgvNursingSchedule.SelectedRows.Count > 0;
                
                if (btnStop.Enabled == true)
                {
                    DataGridViewRow dgvRow = dgvNursingSchedule.SelectedRows[0];
                    
                    if (dgvRow.Cells["END_DATE"].Value == null || dgvRow.Cells["END_DATE"].Value.ToString().Length == 0)
                    {
                        btnStop.TextValue = "停止";
                    }
                    else
                    {
                        btnStop.TextValue = "取消停止";
                    }                    
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        private void dgvNursingSchedule_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach(DataGridViewRow dgvRow in dgvNursingSchedule.Rows)
                {
                    if (dgvRow.Cells["END_DATE"].Value != null && dgvRow.Cells["END_DATE"].Value.ToString().Length > 0)
                    {
                        dgvRow.ReadOnly = true;
                    }
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        private void dgvNursingSchedule_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
        
        
        /// <summary>
        /// 按钮[查询]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                this.Cursor = Cursors.WaitCursor;
                getScheduleRec(patientId, visitId);
                
                btnNew.Enabled      = patientId.Length > 0;
                btnSave.Enabled     = false;
                btnPrint.Enabled    = patientId.Length > 0;
            }
            catch(Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                GVars.App.UserInput = blnStore;
            }
        }
        
        
        /// <summary>
        /// 按钮[保存]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                if (dbDbI.SaveData(null, new object[]{dsScheduleRec}) == true)
                {
                    dsScheduleRec.AcceptChanges();
                    
                    btnSave.Enabled = false;
                }
                
                btnSave.Enabled = false;
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
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {            
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                if (dsScheduleRec == null || dsScheduleRec.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                
                dsPatient = patientDbI.GetInpPatientInfo_FromID(GVars.Patient.ID, GVars.Patient.VisitId);
                
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
        /// 当前病人改变事件
        /// </summary>
        private void patientChanged()
        {
            try
            {
                patientListFrm.LocatePatient(GVars.Patient.ID);
                patientInfoFrm.ShowPatientInfo(GVars.Patient.ID, GVars.Patient.VisitId);
                
                patientId = GVars.Patient.ID;
                visitId   = GVars.Patient.VisitId;
                
                btnQuery_Click(null, null);
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[新增]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (pioSelection.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                
                // 插入到当前行后面 序号变更
                string serialNo = "1";
                int serialNum   = 1;
                if (dgvNursingSchedule.SelectedRows.Count > 0)
                {
                    serialNo = dgvNursingSchedule.SelectedRows[0].Cells["SERIAL_NO"].Value.ToString();
                    string filter = "SERIAL_NO > " + SQL.SqlConvert(serialNo);
                    DataRow[] drFind = dsScheduleRec.Tables[0].Select(filter);
                    
                    for(int i = 0; i < drFind.Length; i++)
                    {
                        drFind[i]["SERIAL_NO"] = int.Parse(drFind[i]["SERIAL_NO"].ToString()) + 1;
                    }
                    
                    serialNum = int.Parse(serialNo) + 1;
                }
                else
                {
                    DataRow[] drFind = dsScheduleRec.Tables[0].Select(string.Empty, "SERIAL_NO DESC");
                    if (drFind.Length > 0)
                    {
                        serialNo = drFind[0]["SERIAL_NO"].ToString();
                        serialNum = int.Parse(serialNo) + 1;
                    }
                }
                
                DateTime dtNow = dbDbI.GetSysDate();
                
                for(int i = 0; i < pioSelection.NursingMeasure.Count; i++)
                {
                    DataRow drNew = dsScheduleRec.Tables[0].NewRow();
                    
                    drNew["PATIENT_ID"]     = patientId;
                    drNew["VISIT_ID"]       = visitId;
                    drNew["DIAGNOSIS"]      = pioSelection.NursingDiagnosis;
                    drNew["OBJECTIVE"]      = pioSelection.NursingObjective;
                    drNew["MEASURE_TYPE"]   = pioSelection.NursingMeasureType[i].ToString();
                    drNew["MEASURE_TITLE"]  = pioSelection.NursingMeasure[i].ToString();
                    drNew["MEASURE_CONTENT"]= pioSelection.NursingMeasureContent[i].ToString();
                    drNew["SERIAL_NO"]      = serialNum.ToString();
                    drNew["CREATE_DATE"]    = dtNow;
                    drNew["CREATOR"]        = GVars.User.Name;
                    drNew["BEGIN_DATE"]     = dtNow;
                    drNew["BEGIN_OPERATOR"] = GVars.User.Name;
                    
                    serialNum++;
                    dsScheduleRec.Tables[0].Rows.Add(drNew);
                }
                
                // 按钮状态
                btnSave.Enabled = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        
        /// <summary>
        /// 按钮[删除]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // 确认删除
                if (GVars.Msg.Show("Q0006", "删除", "当前记录") != DialogResult.Yes)        // 您确认要 {0} {1} 吗?
                {
                    return;
                }
                
                // 删除
                dgvNursingSchedule.Rows.Remove(dgvNursingSchedule.SelectedRows[0]);
                                          
                btnSave.Enabled = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }        
        
        
        /// <summary>
        /// 按钮[停止]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow dgvRow = dgvNursingSchedule.SelectedRows[0];
                if (dgvRow.Cells["END_DATE"].Value == null || dgvRow.Cells["END_DATE"].Value.ToString().Length == 0)
                {
                    dgvRow.Cells["END_DATE"].Value = dbDbI.GetSysDate();
                    dgvRow.Cells["END_OPERATOR"].Value = GVars.User.Name;
                    
                    dgvRow.ReadOnly = true;
                }
                else
                {
                    dgvRow.Cells["END_DATE"].Value = DBNull.Value;
                    dgvRow.Cells["END_OPERATOR"].Value = string.Empty;
                    dgvRow.ReadOnly = false;
                }
                
                if (dgvRow.Cells["END_DATE"].Value == null || dgvRow.Cells["END_DATE"].Value.ToString().Length == 0)
                {
                    btnStop.TextValue = "停止";
                }
                else
                {
                    btnStop.TextValue = "取消停止";
                }
                
                btnSave.Enabled = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
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
		    
            int     row         = 0;
            int     col         = 0;
            string  itemId      = string.Empty;
            string  checkValue  = string.Empty;
            
            DataRow[] drFind    = null; //dsEvalRec.Tables[0].Select(string.Empty, "ITEM_ID");
            DataRow drRec       = null;
            
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
            }
            
            // 输出其它记录
            int idx_rec = 0;
            
            foreach (DataGridViewRow dr in dgvNursingSchedule.Rows)
            {
                foreach(DataGridViewColumn dc in dgvNursingSchedule.Columns)
                {
                    if (getItemAttr(dc.DataPropertyName, ref row, ref col, ref checkValue) == true)
                    {
                        if (dr.Cells[dc.DataPropertyName].Value != null)
                        {
                            excelAccess.SetCellText(row + idx_rec, col, dr.Cells[dc.DataPropertyName].Value.ToString());
                        }
                    }
                }
                
                idx_rec++;               
            }
		    
			//excel.Print();				           //打印
			excelAccess.PrintPreview();			       //预览
            
			excelAccess.Close(false);				   //关闭并释放			
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
		
		
		/// <summary>
		/// 获取护理计划
		/// </summary>
		/// <returns></returns>
		private void getScheduleRec(string patientId, string visitId)
		{
		    string filter = "PATIENT_ID = " + SQL.SqlConvert(patientId)
		                    + "AND VISIT_ID = " + SQL.SqlConvert(visitId);
		    dsScheduleRec = dbDbI.GetTableData("NURSING_SCHEDULE_REC", filter);
		    
		    dsScheduleRec.Tables[0].DefaultView.Sort = "SERIAL_NO";
		    dgvNursingSchedule.DataSource = dsScheduleRec.Tables[0].DefaultView;
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
        #endregion        
    }
}
