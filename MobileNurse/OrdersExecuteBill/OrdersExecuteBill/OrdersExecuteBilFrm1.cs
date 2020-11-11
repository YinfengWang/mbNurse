using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class OrdersExecuteBilFrm1 : FormDo
    {
        #region 窗体变量
        private PatientListWardFrm  patientListFrm  = null;                     // 病人列表
        private PatientSearchFrm    patientInfoFrm  = null;                     // 病人信息

        private DataSet             dsPatient       = null;                     // 病人信息
        private string              patientId       = string.Empty;             // 病人ID号
        private string              visitId         = string.Empty;             // 本次就诊序号
        
        private string              adminList_Trans = string.Empty;             // 输液途径
        private string              adminList_Trans_s= string.Empty;            // 输液途径(选中)
        private DataSet             dsOrdersExecute = null;                     // 医嘱执行单
        
        private ExcelConfigData     excelConfig     = new ExcelConfigData();    // Excel 打印配置
        private ExcelAccess         excelAccess     = new ExcelAccess();        // Excel 接口
        private string              _template       = "输液单";                 // 模板文件

        private DotNetBarcode       qrBarcode       = null;
        private List<ExcelItem>     arrPics         = null;                     // 待插入图片列表
        #endregion
        
        
        public OrdersExecuteBilFrm1()
        {
            InitializeComponent();

            _id     = "00060";
            _guid   = "DC972CAF-89A2-4c85-82B5-CEC40DF5D9C9";
        }
        
        
        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrdersExecuteBilFrm_Load(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                GVars.App.UserInput = false;
                
                initFrmVal();
                showAdministrationList();
                
                initDisp();
                initTimeSelector();
                
                GVars.App.UserInput = true;
                btnQuery_Click(null, null);
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }

        
        void patientListFrm_PatientChanged(object sender, PatientEventArgs e)
        {
            patientInfoFrm.ShowPatientInfo(e.PatientId, e.VisitId);

            GVars.Patient.ID = e.PatientId;
            GVars.Patient.VisitId = e.VisitId;

            patientId = GVars.Patient.ID;
            visitId = GVars.Patient.VisitId;
        }


        void patientInfoFrm_PatientChanged(object sender, PatientEventArgs e)
        {
            patientListFrm.LocatePatient(e.PatientId);

            GVars.Patient.ID = e.PatientId;
            GVars.Patient.VisitId = e.VisitId;

            patientId = GVars.Patient.ID;
            visitId = GVars.Patient.VisitId;
            
            btnQuery.Enabled = (patientId.Length > 0);
            
            // 如果是单病人, 查询该病人的执行单
            if (chkAll.Checked == false)
            {
                btnQuery_Click(null, null);
            }
            // 如果是多病人, 定位该病人
            else
            {
                locatePatientInOrdersExecute();
            }
        }


        /// <summary>
        /// 日期改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtRngStart_ValueChanged(object sender, EventArgs e)
        {
            //btnQuery_Click(sender, e);
            //initTimeSelector();
        }
        

        /// <summary>
        /// 按钮[查询]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                if (btnQuery.Enabled == false)
                {
                    return;
                }
                
                this.Cursor = Cursors.WaitCursor;
                
                DateTime dtBegin = dtRngStart.Value.Date.Add(dtpTime0.Value.TimeOfDay);
                DateTime dtStop  = dtRngStart.Value.Date.Add(dtpTime1.Value.TimeOfDay);
                
                // 获取病人执行单
                if (chkAll.Checked == true)
                {
                    dsOrdersExecute = getOrdersExecute(dtBegin, dtStop, patientListFrm.DeptCode, string.Empty, string.Empty);
                }
                else
                {
                    dsOrdersExecute = getOrdersExecute(dtBegin, dtStop, string.Empty, patientId, visitId);
                }
                
                dsPatient       = patientListFrm.DsPatient.Copy();
                
                // 显示病人执行单
                dsOrdersExecute.Tables[0].DefaultView.Sort = "BED_NO, ORDER_NO, SCHEDULE_PERFORM_TIME, ORDER_SUB_NO";
                dsOrdersExecute.Tables[0].DefaultView.RowFilter = getAdminList_Trans("ADMINISTRATION");
                string filter = getFilterTerm();
                if (filter.Length > 0)
                {
                    dsOrdersExecute.Tables[0].DefaultView.RowFilter += " AND " + filter;
                }
                
                dgvOrdersExecute.Columns["PATIENT_NAME"].Visible = (chkAll.Checked == true);
                dgvOrdersExecute.Columns["BED_NO"].Visible = (chkAll.Checked == true);
                
                PATIENT_NAME.DisplayMember  = "NAME";
                PATIENT_NAME.ValueMember    = "PATIENT_ID";
                PATIENT_NAME.DataSource     = dsPatient.Tables[0].DefaultView;
                
                dgvOrdersExecute.AutoGenerateColumns = false;
                dgvOrdersExecute.DataSource = dsOrdersExecute.Tables[0].DefaultView;
                
                // 打印按钮状态控制
                btnPrint.Enabled = (dgvOrdersExecute.Rows.Count > 0);
                
                // 检查拆分结果
                if (chkSplitError(patientListFrm.DeptCode) == false)
                {
                    lblInfo.Text = "医嘱拆分有错误!";
                }
                else
                {
                    lblInfo.Text = string.Empty;
                }
                
                this.Cursor = Cursors.Default;
            }
            catch(Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 时间过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void billFilter_Time(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false) return;
                if (dsOrdersExecute == null || dsOrdersExecute.Tables.Count == 0) return;

                this.Cursor = Cursors.WaitCursor;

                dsOrdersExecute.Tables[0].DefaultView.Sort = "BED_NO, ORDER_NO, SCHEDULE_PERFORM_TIME, ORDER_SUB_NO";
                dsOrdersExecute.Tables[0].DefaultView.RowFilter = getAdminList_Trans("ADMINISTRATION");
                string filter = getFilterTerm();
                if (filter.Length > 0)
                {
                    dsOrdersExecute.Tables[0].DefaultView.RowFilter += " AND " + filter;
                }

                //DateTime dtStart = dtRngStart.Value.Date.AddHours(dtpTime0.Value.Hour).AddMinutes(dtpTime0.Value.Minute).AddSeconds(dtpTime0.Value.Second);
                //DateTime dtStop  = dtRngStart.Value.Date.AddHours(dtpTime1.Value.Hour).AddMinutes(dtpTime1.Value.Minute).AddSeconds(dtpTime1.Value.Second);
                //filter = "SCHEDULE_PERFORM_TIME >= " + SQL.SqlConvert(dtStart.ToString(ComConst.FMT_DATE.LONG))
                //        + " AND SCHEDULE_PERFORM_TIME <= " + SQL.SqlConvert(dtStop.ToString(ComConst.FMT_DATE.LONG));
                //dsOrdersExecute.Tables[0].DefaultView.RowFilter += " AND " + filter;

                dgvOrdersExecute.DataSource = dsOrdersExecute.Tables[0].DefaultView;

                // 打印按钮状态控制
                btnPrint.Enabled = (dgvOrdersExecute.Rows.Count > 0);

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
        }

        
        /// <summary>
        /// 输液途径选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwAdministration_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            billFilter_Time(sender, null);
        }
        

        /// <summary>
        /// 不同组按不同颜色显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvOrdersExecute_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                string key1 = string.Empty;
                string key2 = string.Empty;
                string key3 = string.Empty;

                Color color1 = Color.White;
                Color color2 = Color.Cyan;
                Color colorBk = Color.White;

                int selector = 1;

                DataGridViewRow dgvRow = null;
                for (int i = 0; i < dgvOrdersExecute.Rows.Count; i++)
                {
                    dgvRow = dgvOrdersExecute.Rows[i];

                    // 判断是否换组
                    if (key1.Equals(dgvRow.Cells["PATIENT_ID"].FormattedValue.ToString()) == false
                        || key2.Equals(dgvRow.Cells["ORDER_NO"].FormattedValue.ToString()) == false
                        || key3.Equals(dgvRow.Cells["SCHEDULE_PERFORM_TIME"].FormattedValue.ToString()) == false)
                    {
                        key1 = dgvRow.Cells["PATIENT_ID"].FormattedValue.ToString();
                        key2 = dgvRow.Cells["ORDER_NO"].FormattedValue.ToString();
                        key3 = dgvRow.Cells["SCHEDULE_PERFORM_TIME"].FormattedValue.ToString();

                        selector = (selector + 1) % 2;
                        colorBk = (selector == 0 ? color1 : color2);
                    }

                    // 设置背景色
                    foreach (DataGridViewColumn dgvCol in dgvOrdersExecute.Columns)
                    {
                        dgvRow.Cells[dgvCol.Name].Style.BackColor = colorBk;
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        

        /// <summary>
        /// 单元格单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvOrdersExecute_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false) return;
                if (e.ColumnIndex != SELECTION.Index) return;

                DataGridViewCell dgvCell = dgvOrdersExecute.CurrentCell;
                
                Color colorSel = dgvOrdersExecute.CurrentCell.Style.BackColor;
                int value = (dgvOrdersExecute.CurrentCell.FormattedValue.ToString().ToUpper().Equals("TRUE")? 1: 0);
                
                for (int i = e.RowIndex - 1; i >= 0; i--)
                {
                    if (colorSel.Equals(dgvOrdersExecute.Rows[i].Cells[e.ColumnIndex].Style.BackColor) == false)
                    {
                        break;
                    }

                    dgvOrdersExecute.Rows[i].Cells[SELECTION.Index].Value = (value == 0? 1: 0);
                }
                
                for (int i = e.RowIndex + 1; i < dgvOrdersExecute.Rows.Count; i++)
                {
                    if (colorSel.Equals(dgvOrdersExecute.Rows[i].Cells[e.ColumnIndex].Style.BackColor) == false)
                    {
                        break;
                    }

                    dgvOrdersExecute.Rows[i].Cells[SELECTION.Index].Value = (value == 0? 1: 0);
                }
                
                dgvOrdersExecute.CurrentCell = null;
                dgvOrdersExecute.CurrentCell = dgvCell;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 单元格双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvOrdersExecute_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvOrdersExecute_CellContentClick(sender, e);
        }
        
        
        /// <summary>
        /// CHECKBOX 全部病人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //patientListFrm.Enabled = (chkAll.Checked == false);
                grpPatientInfo.Visible = (chkAll.Checked == false);
                
                if (chkAll.Checked == true)
                {
                    grpMain.Top = grpPatientInfo.Top;
                }
                else
                {
                    grpMain.Top = grpPatientInfo.Top + grpPatientInfo.Height + 6;
                }

                grpMain.Height = grpAdmin.Top - grpMain.Top;
                
                //chkSelectAll.Top = grpMain.Top;
                
                // 查询
                btnQuery_Click(sender, e);
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        
        /// <summary>
        /// CHECKBOX 选择所有医嘱执行单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                GVars.App.UserInput = false;
                
                int result = (chkSelectAll.Checked == true? 1: 0);
                
                this.Cursor = Cursors.WaitCursor;
                
                foreach(DataRow dr in dsOrdersExecute.Tables[0].Rows)
                {
                    dr["SELECTION"] = result;
                }
                
                dgvOrdersExecute.DataSource = dsOrdersExecute.Tables[0].DefaultView;
                
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }


        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkLongTerm_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                dsOrdersExecute.Tables[0].DefaultView.RowFilter = getAdminList_Trans("ADMINISTRATION");
                string filter = getFilterTerm();
                if (filter.Length > 0)
                {
                    dsOrdersExecute.Tables[0].DefaultView.RowFilter += " AND " + filter;
                }
                
                dgvOrdersExecute.DataSource = dsOrdersExecute.Tables[0].DefaultView;

                this.Cursor = Cursors.Default;
            }
            catch(Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
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
                if (excelConfig.ConfigSections.Count == 0)
                {
                    loadExcelPrintConfig(_template);
                }

                if (excelConfig.ConfigSections.Count > 0)
                {
                    excelTemplatePrint();
                }
                
                // 确认打印成功
                if (GVars.Msg.Show("Q0009", "打印") != DialogResult.Yes)
                {
                    return;
                }
                
                // 保存打印结果
                savePrintResult();
            }
            catch (Exception ex)
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
            try
            {
                this.Close();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion
        
        
        #region 共通函数
        /// <summary>
        /// 初始化窗体变量
        /// </summary>
        private void initFrmVal()
        {
            _userRight  = GVars.User.GetUserFrmRight(_id);
            
            getAdminList_Trans();
            
            // 删除本地图片
            deleteOldBarcodeBmpFile();
        }


        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {
            // 医嘱执行单
            dgvOrdersExecute.AutoGenerateColumns = false;
            
            // 加载病人列表
            patientListFrm = new PatientListWardFrm();
            patientListFrm.DeptCode = GVars.User.DeptCode;

            patientListFrm.FormBorderStyle = FormBorderStyle.None;
            patientListFrm.TopLevel = false;
            patientListFrm.Dock = DockStyle.Fill;
            patientListFrm.Visible = true;

            grpPatientList.Controls.Add(patientListFrm);

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

            DataGridViewCheckBoxColumn chkSel = (DataGridViewCheckBoxColumn)(SELECTION);

            // 禁用排序
            foreach (DataGridViewColumn dc in dgvOrdersExecute.Columns)
            {
                dc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        
        
        /// <summary>
        /// 获取输液单的途径过滤条件
        /// </summary>
        /// <returns></returns>
        private string getAdminList_Trans(string colName)
        {
            string adminList = string.Empty;
            ListViewItem item = null;
            for(int i = 0; i < lvwAdministration.Items.Count; i++)
            {
                item = lvwAdministration.Items[i];
                if (item.Checked == true)
                {
                    if (adminList.Length > 0) adminList += ",";
                    adminList += "'" + item.Text.Trim() + "'";
                }
            }
            
            if (adminList.Length > 0)
            {
                return colName + " IN (" + adminList + ") ";
            }
            else
            {
                return "(1 = 2) ";
            }
        }
        
        
        /// <summary>
        /// 获取长期与临时的过滤条件
        /// </summary>
        /// <returns></returns>
        private string getFilterTerm()
        {
            // 如果全选
            if (chkLongTerm.Checked == true && chkTempTerm.Checked == true && chkPrinted.Checked == true && chkNotPrinted.Checked == true)
            {
                return string.Empty;
            }
            
            // 长期/临时全不选
            if (chkLongTerm.Checked == false && chkTempTerm.Checked == false)
            {
                return "(1 = 2) ";
            }
            
            // 打印/未打印 全不选
            if (chkPrinted.Checked == false && chkNotPrinted.Checked == false)
            {
                return "(1 = 2) ";
            }
            
            string filter = string.Empty;

            // 如果长期/临时全选
            if (chkLongTerm.Checked == true && chkTempTerm.Checked == true)
            {
            }
            else
            {
                if (chkLongTerm.Checked == true)
                {
                    filter = "REPEAT_INDICATOR = '1' ";
                }
                else
                {
                    filter = "(REPEAT_INDICATOR IS NULL OR REPEAT_INDICATOR <> '1') ";
                }
            }

            // 打印/未打印 全选
            if (chkPrinted.Checked == true && chkNotPrinted.Checked == true)
            {
                
            }
            else
            {
                if (filter.Length > 0) filter += " AND ";
                
                if (chkPrinted.Checked == true)
                {
                    filter += "PRINT_FLAG = '1' ";
                }
                else
                {
                    filter += "(PRINT_FLAG IS NULL OR PRINT_FLAG <> '1') ";
                }
            }
            
            return filter;
        }
        
        
        /// <summary>
        /// 在执行单列表中定位当前病人
        /// </summary>
        private void locatePatientInOrdersExecute()
        {
            DataGridViewRow dgvRow = null;
            for(int i = 0; i < dgvOrdersExecute.Rows.Count; i++)
            {
                dgvRow = dgvOrdersExecute.Rows[i];
                
                if (dgvRow.Cells["PATIENT_ID"].Value.ToString().Equals(patientId) == true)
                {
                    dgvRow.Selected = true;
                    dgvOrdersExecute.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
            }
        }
        
        
        /// <summary>
        /// 初始化时间选择控件
        /// </summary>
        private void initTimeSelector()
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                GVars.App.UserInput = false;
                dtpTime0.Value = DateTime.Now.Date;
                dtpTime1.Value = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }
        
        
        /// <summary>
        /// 加载途径列表
        /// </summary>
        private void showAdministrationList()
        {
            // 加载所有列表
            string[] splitChars = {",", ";", ":"};
            string   splitChar  = ",";
            for(int i = 0; i < splitChars.Length; i++)
            {
                if (adminList_Trans.Contains(splitChars[i]) == true)
                {
                    splitChar = splitChars[i];
                    break;
                }
            }
            
            string[] parts = adminList_Trans.Split(splitChar.ToCharArray());
            
            lvwAdministration.Items.Clear();
            for(int i = 0; i < parts.Length; i++)
            {
                ListViewItem item = new ListViewItem(parts[i]);                
                lvwAdministration.Items.Add(item);
            }     
            //LB20110707,解决用药途径显示不美观问题
            lvwAdministration.Alignment = ListViewAlignment.Default;
            // 加载上次选中的列表
            loadUserSelection();
        }
        
        
        /// <summary>
        /// 加载用户选择
        /// </summary>
        private void loadUserSelection()
        {
            // 加载所有列表
            string[] splitChars = { ",", ";", ":" };
            string   splitChar  = ",";
            for (int i = 0; i < splitChars.Length; i++)
            {
                if (adminList_Trans_s.Contains(splitChars[i]) == true)
                {
                    splitChar = splitChars[i];
                    break;
                }
            }

            string[] parts = adminList_Trans_s.Split(splitChar.ToCharArray());
            
            // 加载选中
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                GVars.App.UserInput = false;
                
                for (int i = 0; i < lvwAdministration.Items.Count; i++)
                {
                    if (parts.Contains<string>(lvwAdministration.Items[i].Text) == true)
                    {
                        lvwAdministration.Items[i].Checked = true;
                    }
                }
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }
        #endregion
        
        
        #region 打印有关
        /// <summary>
        /// 加载Excel打印配置文件
        /// </summary>
        /// <param name="templateFileName"></param>
        private void loadExcelPrintConfig(string templateFileName)
        {
            excelConfig.Clear();
            
            // 读取配置文件
            string iniFile = Path.Combine(Application.StartupPath, "Template\\" + templateFileName + ".ini");
            if (File.Exists(iniFile) == true)
            {
                StreamReader sr = new StreamReader(iniFile);
                
                try
                {
                    string configInfo = sr.ReadToEnd();
                    excelConfig.ParseConfigInfo(configInfo);
                }
                finally
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }
        
        
        /// <summary>
        /// 用Excel模板打印
        /// </summary>
        /// <remarks>比较适合套打、格式、统计分析报表、图形分析、自定义打印</remarks>
        private void excelTemplatePrint()
        {
            string strExcelTemplateFile = Path.Combine(Application.StartupPath, "Template\\" + _template + ".xls");
            
            excelAccess.Open(strExcelTemplateFile);         // 打开模板文件
            excelAccess.IsVisibledExcel = true;
            excelAccess.FormCaption = string.Empty;
            
            excelTemplatePrintFormat();                     // 设置页面格式
            excelTemplatePrintData();                       // 输出数据
                        
            //excel.Print();				                // 打印
            excelAccess.PrintPreview();			            // 预览
            
            excelAccess.Close(false);				        // 关闭并释放			
        }

        
        /// <summary>
        /// 向Excel中设置各单元项格式
        /// </summary>
        private void excelTemplatePrintFormat()
        {
            int unit_index = 0;
            int row_start = 0;
            int col_start = 0;
            
            // 查找印数据
            string filter = "SELECTION = '1'";
            if (dsOrdersExecute.Tables[0].DefaultView.RowFilter.Length > 0)
            {
                filter += " AND " + dsOrdersExecute.Tables[0].DefaultView.RowFilter;
            }
            
            DataRow[] drFind = dsOrdersExecute.Tables[0].Select(filter, dsOrdersExecute.Tables[0].DefaultView.Sort);
            string[] keys = { "", "", "", "" };
            
            // 复制模板单元格
            excelAccess.RangeCopy(excelConfig.PageInfo.Start_Row, excelConfig.PageInfo.Start_Col,
                                  excelConfig.PageInfo.Start_Row + excelConfig.PageInfo.Unit_Rows,
                                  excelConfig.PageInfo.Start_Col + excelConfig.PageInfo.Unit_Cols);

            // 进行数据输出
            for (int i = 0; i < drFind.Length; i++)
            {
                DataRow dr = drFind[i];
                
                // 如果是换分组
                if (keys[0].Equals(dr["PATIENT_ID"].ToString()) == false
                    || keys[1].Equals(dr["ORDER_NO"].ToString()) == false
                    || keys[2].Equals(dr["SCHEDULE_PERFORM_TIME"].ToString()) == false)
                {
                    keys[0] = dr["PATIENT_ID"].ToString();
                    keys[1] = dr["ORDER_NO"].ToString();
                    keys[2] = dr["SCHEDULE_PERFORM_TIME"].ToString();

                    unit_index++;         // 打印单元增加
                    
                    // 如果是换行
                    if (unit_index > excelConfig.PageInfo.Unit_Count)
                    {
                        unit_index = 1;
                        col_start = 0;
                        row_start += excelConfig.PageInfo.Unit_Rows;
                    }
                    
                    if (unit_index > 1)
                    {
                        col_start += excelConfig.PageInfo.Unit_Cols;
                    }
                                        
                    // 复制单元格格式
                    if (i > 0)
                    {
                        int dest_x0 = row_start + excelConfig.PageInfo.Start_Row;
                        int dest_y0 = col_start + excelConfig.PageInfo.Start_Col;
                        int dest_x1 = dest_x0 + excelConfig.PageInfo.Unit_Rows;
                        int dest_y1 = dest_y0 + excelConfig.PageInfo.Unit_Cols;

                        excelAccess.RangePaste(dest_x0, dest_y0, dest_x1, dest_y1);
                    }
                }
            }
        }
        
        
        /// <summary>
        /// 向Excel中输出数据
        /// </summary>
        private void excelTemplatePrintData()
        {
            if (arrPics != null) arrPics.Clear();               // 清除待插入图片
            arrPics = new List<ExcelItem>();

            //------------------------------------------------------------------
            
            int unit_index = 0;
            int unit_PrintedRows = 0;
            int row_start = 0;
            int col_start = 0;
            
            // 查找印数据
            string filter = "SELECTION = '1'";
            if (dsOrdersExecute.Tables[0].DefaultView.RowFilter.Length > 0)
            {
                filter += " AND " + dsOrdersExecute.Tables[0].DefaultView.RowFilter;
            }

            DataRow[] drFind = dsOrdersExecute.Tables[0].Select(filter, dsOrdersExecute.Tables[0].DefaultView.Sort);
            string[] keys = { "", "", "", "" };
            
            // 进行数据输出
            for (int i = 0; i < drFind.Length; i++)
            {
                DataRow dr = drFind[i];

                // 如果是换分组
                if (keys[0].Equals(dr["PATIENT_ID"].ToString()) == false
                    || keys[1].Equals(dr["ORDER_NO"].ToString()) == false
                    || keys[2].Equals(dr["SCHEDULE_PERFORM_TIME"].ToString()) == false)
                {
                    keys[0] = dr["PATIENT_ID"].ToString();
                    keys[1] = dr["ORDER_NO"].ToString();
                    keys[2] = dr["SCHEDULE_PERFORM_TIME"].ToString();

                    unit_index++;         // 打印单元增加

                    // 如果是换行
                    if (unit_index > excelConfig.PageInfo.Unit_Count)
                    {
                        unit_index = 1;
                        col_start = 0;
                        row_start += excelConfig.PageInfo.Unit_Rows;
                    }

                    if (unit_index > 1)
                    {
                        col_start += excelConfig.PageInfo.Unit_Cols;
                    }

                    unit_PrintedRows = 0;
                    
                    #region 输出单行项目
                    // 输出单行项目
                    for (int k = 0; k < excelConfig.ConfigSections.Count; k++)
                    {
                        ExcelConfigSection configSection = excelConfig.ConfigSections[k];
                        if (configSection.MultiRows == true) continue;

                        // 确定数据源
                        DataRow drData = null;
                        switch (configSection.TableName)
                        {
                            case "PATIENT_INFO":
                                DataRow[] drPatients = dsPatient.Tables[0].Select("PATIENT_ID = " + SQL.SqlConvert(keys[0]));
                                if (drPatients.Length > 0)
                                {
                                    drData = drPatients[0];
                                }
                                else
                                {
                                    continue;
                                }

                                break;

                            case "ORDERS_EXECUTE":
                                drData = dr;
                                break;
                        }

                        // 输出数据
                        for (int n = 0; n < configSection.ConfigItems.Count; n++)
                        {
                            ExcelItem excelItem = configSection.ConfigItems[n];
                            setExcelCellText(drData, excelItem.ItemId, row_start + excelItem.Row, col_start + excelItem.Col, excelItem.CheckValue);
                        }
                    }
                    #endregion
                }

                // 输出多行项目
                for (int k = 0; k < excelConfig.ConfigSections.Count; k++)
                {
                    ExcelConfigSection configSection = excelConfig.ConfigSections[k];
                    if (configSection.MultiRows == false) continue;
                    if (configSection.Max_Rows > 0 && unit_PrintedRows > configSection.Max_Rows - 1) continue;
                    
                    // 输出数据
                    for (int n = 0; n < configSection.ConfigItems.Count; n++)
                    {
                        ExcelItem excelItem = configSection.ConfigItems[n];
                        setExcelCellText(dr, excelItem.ItemId, row_start + excelItem.Row + unit_PrintedRows, col_start + excelItem.Col, excelItem.CheckValue);
                    }
                }
                
                unit_PrintedRows++;
            }

            //------------------------------------------------------------------
            for (int i = 0; i < arrPics.Count; i++)
            {
                excelAccess.InsertPicture(arrPics[i].Row, arrPics[i].Col, arrPics[i].ItemId);
                Thread.Sleep(100);
            }
        }
        
        
        /// <summary>
        /// 向单元格赋值
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="colName"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void setExcelCellText(DataRow dr, string colName, int row, int col, string tagInfo)
        {
            // 如果是条码
            if (colName.StartsWith(ExcelConfigData.STR_BARCODE) == true)
            {
                if (tagInfo.Trim().Length == 0) return;

                // 获取条码值
                string barCode = string.Empty;
                string[] parts = tagInfo.Split("+".ToCharArray());
                for (int i = 0; i < parts.Length; i++)
                {
                    string colNameIn = parts[i].Trim();

                    if (colNameIn.StartsWith(@"'") && colNameIn.EndsWith(@"'") && colNameIn.Length > 2)
                    {
                        barCode += colNameIn.Substring(1, colNameIn.Length - 2);
                        continue;
                    }

                    if (dr.Table.Columns.Contains(colNameIn) == false) return;
                    if (dr[colNameIn] == DBNull.Value) return;

                    barCode += dr[colNameIn].ToString();
                }

                // 生成条码
                int pixelSize = 0;
                int.TryParse(colName.Substring(ExcelConfigData.STR_BARCODE.Length), out pixelSize);
                if (pixelSize < 1) pixelSize = 2;

                string fileName = createBarcodeBmpFile(barCode, pixelSize);

                ExcelItem item = new ExcelItem();
                item.Row = row;
                item.Col = col;
                item.ItemId = fileName;
                arrPics.Add(item);

                return;
            }
            
            // 如果不存在这个字段
            if (dr.Table.Columns.Contains(colName) == false) 
            {
                return;
            }
            
            // 输出该字段值
            if (dr[colName] == DBNull.Value)
            {
                excelAccess.SetCellText(row, col, string.Empty);
            }
            else if (dr.Table.Columns[colName].DataType == typeof(DateTime))
            {
                excelAccess.SetCellText(row, col, ((DateTime)(dr[colName])).ToString(ComConst.FMT_DATE.LONG));
            }
            else
            {
                excelAccess.SetCellText(row, col, dr[colName].ToString());
            }
        }
        
        
        /// <summary>
        /// 创建条码文件
        /// </summary> <returns></returns>
        private string createBarcodeBmpFile(string barcode, int pixelSize)
        {
            if (qrBarcode == null)
            {
                qrBarcode = new DotNetBarcode(DotNetBarcode.Types.QRCode);
                qrBarcode.QRQuitZone = pixelSize;
            }

            string path =  Path.Combine(Application.StartupPath, "Data");
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            string fileName = string.Empty;

            if (pixelSize == 2)
            {
                fileName = Path.Combine(path, barcode + ".bmp");
            }
            else
            {
                fileName = Path.Combine(path, barcode + "_" + pixelSize.ToString() + ".bmp");
            }
            
            qrBarcode.QRSave(barcode, fileName, pixelSize);

            return fileName;
        }


        /// <summary>
        /// 删除以前的条码文件
        /// </summary>
        /// <returns></returns>
        private void deleteOldBarcodeBmpFile()
        {
            string path =  Path.Combine(Application.StartupPath, "Data");
            if (Directory.Exists(path) == false)
            {
                return;
            }
            
            string[] files = Directory.GetFiles(path);
            for(int i = files.Length - 1; i >= 0; i--)
            {
                File.SetAttributes(files[i], FileAttributes.Normal);
                File.Delete(files[i]);
            }
        }
        #endregion
        
        
        #region 数据交互
        /// <summary>
        /// 获取医嘱执行单
        /// </summary>
        /// <returns></returns>
        private DataSet getOrdersExecute(DateTime dtBegin, DateTime dtStop, string wardCode, string patientId, string visitId)
        {
            string filter = "ORDERS_EXECUTE.SCHEDULE_PERFORM_TIME >= " + SQL.GetOraDbDate(dtBegin)
                        + "AND ORDERS_EXECUTE.SCHEDULE_PERFORM_TIME <= " + SQL.GetOraDbDate(dtStop);

            if (patientId.Length > 0)
            {
                filter += "AND ORDERS_EXECUTE.PATIENT_ID = " + SQL.SqlConvert(patientId)
                        + "AND ORDERS_EXECUTE.VISIT_ID = " + SQL.SqlConvert(visitId);
            }
            else
            {
                if (wardCode.Length > 0)
                {
                    filter += "AND ORDERS_EXECUTE.PATIENT_ID IN (SELECT PATIENT_ID FROM PATIENT_INFO "
                            + "WHERE WARD_CODE = " + SQL.SqlConvert(wardCode) + " OR DEPT_CODE = " + SQL.SqlConvert(wardCode) + ") ";
                }
            }

            //20110326LB修改的
            string sql = "SELECT 1 SELECTION, ORDERS_EXECUTE.DOSAGE, ORDERS_EXECUTE.DOSAGE_UNITS, ORDERS_EXECUTE.* , patient_info.BED_NO "
                        + "FROM ORDERS_EXECUTE , patient_info "
                        + "WHERE ORDERS_EXECUTE.PATIENT_ID = patient_info.PATIENT_ID "
                        + "AND ORDERS_EXECUTE.VISIT_ID = patient_info.VISIT_ID "
                        + "AND " + filter;
            return GVars.OracleAccess.SelectData(sql, "ORDERS_EXECUTE");
        }
        
        
        /// <summary>
        /// 获取输液途径
        /// </summary>
        private string getAdminList_Trans()
        {
            string sql = "SELECT PARAMETER_VALUE FROM APP_CONFIG WHERE PARAMETER_NAME = 'ADMINISTRATION_TRANSFUSE'";
            if (GVars.OracleAccess.SelectValue(sql) == true)
            {
                adminList_Trans = GVars.OracleAccess.GetResult(0);
            }

            sql = "SELECT PARAMETER_VALUE FROM APP_CONFIG WHERE PARAMETER_NAME = 'ADMINISTRATION_TRANSFUSE_SEL'";
            if (GVars.OracleAccess.SelectValue(sql) == true)
            {
                adminList_Trans_s = GVars.OracleAccess.GetResult(0);
            }
            
            return string.Empty;
        }
        
               
        /// <summary>
        /// 保存打印结果
        /// </summary>
        /// <returns></returns>
        private bool savePrintResult()
        {
            if (dsOrdersExecute == null || dsOrdersExecute.Tables.Count == 0)
            {
                return true;
            }
            
            dsOrdersExecute.AcceptChanges();
            
            DataRow[] drFind = dsOrdersExecute.Tables[0].Select("SELECTION = '1'");
            for(int i = 0; i < drFind.Length; i++)
            {
                drFind[i]["PRINT_FLAG"] = "1";
            }
            
            DataSet ds = dsOrdersExecute.GetChanges();
            
            GVars.OracleAccess.Update(ref ds);
            dsOrdersExecute.AcceptChanges();
            return true;
        }
        
        
        /// <summary>
        /// 检查医嘱拆分中是否有错误
        /// </summary>
        /// <returns></returns>
        private bool chkSplitError(string wardCode  )
        {
            string sql = string.Empty;

            #region 这个 也失效了
            //sql = "SELECT ORDERS_M.* FROM ORDERS_M, PATS_IN_HOSPITAL "
            //    + "WHERE PATS_IN_HOSPITAL.WARD_CODE = " + SQL.SqlConvert(wardCode)
            //    +       "AND ORDERS_M.PATIENT_ID = PATS_IN_HOSPITAL.PATIENT_ID "
            //    +       "AND ORDERS_M.VISIT_ID = PATS_IN_HOSPITAL.VISIT_ID";
            #endregion 

            sql = "SELECT PATIENT_INFO.NAME, "
                       + "PATIENT_INFO.BED_NO, "
                       + "ORDERS_M.* "
                       + "FROM ORDERS_M, "
                       + "PATIENT_INFO "
                       + "WHERE SPLIT_MEMO IS NOT NULL "
                       + "AND ORDERS_M.PATIENT_ID =PATIENT_INFO.PATIENT_ID "
                       + "AND ORDERS_M.VISIT_ID = PATIENT_INFO.VISIT_ID "  
                       + "AND patient_info.WARD_CODE = " + SQL.SqlConvert(wardCode);

        
            DataSet ds = GVars.OracleAccess.SelectData(sql, "ORDERS_M");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
