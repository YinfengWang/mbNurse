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
using DXApplication2;
using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class OrdersExecuteFrm : FormDo,IBasePatient
    {
        #region 窗体变量
        private const  string       RIGHT_EXECUTE   = "1";                      // 医嘱执行
        private const  string       RIGHT_MODIFY    = "2";                      // 修改
        
        private DataSet             dsPatient       = null;                     // 病人信息
        private string              patientId       = string.Empty;             // 病人ID号
        private string              visitId         = string.Empty;             // 本次就诊序号
        
        private string              adminList_Trans = string.Empty;             // 输液途径
        private string              adminList_Trans_s= string.Empty;            // 输液途径(选中)
        private DataSet             dsOrdersExecute = null;                     // 医嘱执行单
        
        private ExcelConfigData     excelConfig     = new ExcelConfigData();    // Excel 打印配置
        private ExcelAccess         excelAccess     = new ExcelAccess();        // Excel 接口
        private string              _template       = "医嘱执行查询";           // 模板文件
        private DataSet             dsPrint         = null;                     // 打印信息
        
        private DateTimePickerFrm   dtpSelector     = new DateTimePickerFrm();  // 时间选择
        #endregion


        public OrdersExecuteFrm()
        {
            InitializeComponent();

            _id     = "00072";
            _guid   = "BB34977D-87DC-4896-83DD-698BFA2BEE8D";
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
                
                DateTime dtBegin = dtRngStart.Value;
                DateTime dtStop  = dtRngEnd.Value;
                
                // 获取病人执行单
                if (chkAll.Checked == true)
                {
                    dsOrdersExecute = getOrdersExecute(dtBegin, dtStop, MdiFrm.GetInstance().DeptCode, string.Empty, string.Empty);
                }
                else
                {
                    dsOrdersExecute = getOrdersExecute(dtBegin, dtStop, string.Empty, patientId, visitId);
                }

                dsPatient   = new PatientDbI(GVars.OracleAccess).GetWardPatientList(MdiFrm.GetInstance().DeptCode);
                
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
                
                lblInfo.Text = string.Empty;
                
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

                Color color1  = Color.White;
                Color color2  = Color.Cyan;
                Color colorBk = Color.FromArgb(200,200,200);
                
                int counter = 0;
                DataGridViewRow dgvRow = null;
                for (int i = 0; i < dgvOrdersExecute.Rows.Count; i++)
                {
                    dgvRow = dgvOrdersExecute.Rows[i];

                    // 判断是否换组
                    if (key1.Equals(dgvRow.Cells["PATIENT_ID"].FormattedValue.ToString()) == false
                        || key2.Equals(dgvRow.Cells["ORDER_NO"].FormattedValue.ToString()) == false
                        || key3.Equals(dgvRow.Cells["SCHEDULE_PERFORM_TIME"].FormattedValue.ToString()) == false)
                    {
                        counter++;
                        key1 = dgvRow.Cells["PATIENT_ID"].FormattedValue.ToString();
                        key2 = dgvRow.Cells["ORDER_NO"].FormattedValue.ToString();
                        key3 = dgvRow.Cells["SCHEDULE_PERFORM_TIME"].FormattedValue.ToString();

                        dgvRow.Cells["ORDER_NO_SHOW"].Value = "☞";
                    }

                    if (dgvRow.Cells["EXECUTE_NURSE"].FormattedValue.ToString().Length != 0)
                    {
                        foreach (DataGridViewColumn dgvColor in dgvOrdersExecute.Columns)
                        {
                            dgvRow.Cells[dgvColor.Name].Style.BackColor = colorBk;
                        }
                    }
                }
                
                lblCount.Text = counter + "条";
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
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
                //grpPatientInfo.Visible = (chkAll.Checked == false);
                
                //if (chkAll.Checked == true)
                //{
                //    grpMain.Top = grpPatientInfo.Top;
                //}
                //else
                //{
                //    grpMain.Top = grpPatientInfo.Top + grpPatientInfo.Height + 6;
                //}

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

        
        private void dgvOrdersExecute_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (_userRight.Contains(RIGHT_EXECUTE) == false && _userRight.Contains(RIGHT_MODIFY) == false)
                {
                    return;
                }

                dgvOrdersExecute.ContextMenuStrip = cmnuOrdersExecute;

                string nurse = dgvOrdersExecute.Rows[e.RowIndex].Cells["EXECUTE_NURSE"].Value.ToString();
                cmnuOrdersExecute_Execute.Enabled = (nurse.Length == 0);
                cmnuOrdersExecute_CancelExecute.Enabled = (nurse.Equals(GVars.User.Name) || _userRight.Contains(RIGHT_MODIFY));
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void dgvOrdersExecute_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvOrdersExecute.ContextMenuStrip = null;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        
        /// <summary>
        /// 菜单[执行]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmnuOrdersExecute_Execute_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOrdersExecute.CurrentRow == null)
                {
                    return;
                }

                if (dtpSelector.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                // 修改数据库
                string patientId    = dgvOrdersExecute.CurrentRow.Cells["PATIENT_ID"].Value.ToString();
                string visitId      = dgvOrdersExecute.CurrentRow.Cells["VISIT_ID"].Value.ToString();
                string orderNo      = dgvOrdersExecute.CurrentRow.Cells["ORDER_NO"].Value.ToString();
                DateTime dtSchedule = (DateTime)(dgvOrdersExecute.CurrentRow.Cells["SCHEDULE_PERFORM_TIME"].Value);

                string sql = "UPDATE ORDERS_EXECUTE "
                           + "SET IS_EXECUTE = '1', EXECUTE_NURSE = " + SQL.SqlConvert(GVars.User.Name)
                           +     ",EXECUTE_DATE_TIME = " + SQL.GetOraDbDate(dtpSelector.DateTimeSet)
                           + "WHERE "
                           +      "PATIENT_ID = " + SQL.SqlConvert(patientId)
                           +      "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                           +      "AND ORDER_NO = " + SQL.SqlConvert(orderNo)
                           +      "AND SCHEDULE_PERFORM_TIME = " + SQL.GetOraDbDate(dtSchedule);
                GVars.OracleAccess.ExecuteNoQuery(sql);

                // 缓存修改
                string filter = "PATIENT_ID = " + SQL.SqlConvert(patientId)
                           +      "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                           +      "AND ORDER_NO = " + SQL.SqlConvert(orderNo)
                           +      "AND SCHEDULE_PERFORM_TIME = " + SQL.GetSqlDbDate(dtSchedule);

                DataRow[] drFind = dsOrdersExecute.Tables[0].Select(filter);
                for (int i = 0; i < drFind.Length; i++)
                {
                    drFind[i]["IS_EXECUTE"] = "1";
                    drFind[i]["EXECUTE_NURSE"] = GVars.User.Name;
                    drFind[i]["EXECUTE_DATE_TIME"] = dtpSelector.DateTimeSet;
                }

                // 状态变更
                if (dgvOrdersExecute.CurrentCell == null) return;
                dgvOrdersExecute_CellEnter(sender, new DataGridViewCellEventArgs(dgvOrdersExecute.CurrentCell.ColumnIndex, dgvOrdersExecute.CurrentCell.RowIndex));
            }
            catch (Exception ex)
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
        /// 菜单[撤销执行]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmnuOrdersExecute_CancelExecute_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOrdersExecute.CurrentRow == null)
                {
                    return;
                }

                if (GVars.Msg.Show("Q0006", "撤销执行", "当前医嘱执行单") != DialogResult.Yes)           // 您确认要 {0} {1} 吗?
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                // 修改数据库
                string patientId    = dgvOrdersExecute.CurrentRow.Cells["PATIENT_ID"].Value.ToString();
                string visitId      = dgvOrdersExecute.CurrentRow.Cells["VISIT_ID"].Value.ToString();
                string orderNo      = dgvOrdersExecute.CurrentRow.Cells["ORDER_NO"].Value.ToString();
                DateTime dtSchedule = (DateTime)(dgvOrdersExecute.CurrentRow.Cells["SCHEDULE_PERFORM_TIME"].Value);

                string sql = "UPDATE ORDERS_EXECUTE "
                           + "SET IS_EXECUTE = NULL, EXECUTE_NURSE = NULL,EXECUTE_DATE_TIME = NULL "
                           + "WHERE "
                           +      "PATIENT_ID = " + SQL.SqlConvert(patientId)
                           +      "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                           +      "AND ORDER_NO = " + SQL.SqlConvert(orderNo)
                           +      "AND SCHEDULE_PERFORM_TIME = " + SQL.GetOraDbDate(dtSchedule);
                GVars.OracleAccess.ExecuteNoQuery(sql);

                // 缓存修改
                string filter = "PATIENT_ID = " + SQL.SqlConvert(patientId)
                           +      "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                           +      "AND ORDER_NO = " + SQL.SqlConvert(orderNo)
                           +      "AND SCHEDULE_PERFORM_TIME = " + SQL.GetSqlDbDate(dtSchedule);

                DataRow[] drFind = dsOrdersExecute.Tables[0].Select(filter);
                for (int i = 0; i < drFind.Length; i++)
                {
                    drFind[i]["IS_EXECUTE"] = DBNull.Value;
                    drFind[i]["EXECUTE_NURSE"] = DBNull.Value;
                    drFind[i]["EXECUTE_DATE_TIME"] = DBNull.Value;
                }

                // 状态变更
                if (dgvOrdersExecute.CurrentCell == null) return;
                dgvOrdersExecute_CellEnter(sender, new DataGridViewCellEventArgs(dgvOrdersExecute.CurrentCell.ColumnIndex, dgvOrdersExecute.CurrentCell.RowIndex));
            }
            catch (Exception ex)
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
        /// 按钮[打印]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                getPrintInfo();
                
                if (excelConfig.ConfigSections.Count == 0)
                {
                    loadExcelPrintConfig(_template);
                }

                if (excelConfig.ConfigSections.Count > 0)
                {
                    excelTemplatePrint();
                }
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
        }


        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {
            if (_userRight.Contains(RIGHT_EXECUTE) == false && _userRight.Contains(RIGHT_MODIFY) == false)
            {
                dgvOrdersExecute.ContextMenuStrip = null;
            }
            
            // 医嘱执行单
            dgvOrdersExecute.AutoGenerateColumns = false;
            
            //// 加载病人列表
            //patientListFrm = new PatientListWardFrm();
            //patientListFrm.DeptCode = MdiFrm.GetInstance().DeptCode;

            //patientListFrm.FormBorderStyle = FormBorderStyle.None;
            //patientListFrm.TopLevel = false;
            //patientListFrm.Dock = DockStyle.Fill;
            //patientListFrm.Visible = true;

            //grpPatientList.Controls.Add(patientListFrm);

            //patientListFrm.PatientChanged += new PatientChangedEventHandler(patientListFrm_PatientChanged);
            //patientListFrm.BackColor = this.BackColor;

            //// 病人信息
            //patientInfoFrm = new PatientSearchFrm();
            //patientInfoFrm.FormBorderStyle = FormBorderStyle.None;
            //patientInfoFrm.TopLevel = false;
            //grpPatientInfo.Controls.Add(patientInfoFrm);
            //patientInfoFrm.Dock = DockStyle.Fill;
            //patientInfoFrm.Show();
            //patientInfoFrm.PatientChanged += new PatientChangedEventHandler(patientInfoFrm_PatientChanged);
            //patientInfoFrm.BackColor = this.BackColor;

            //if (patientListFrm.PatientId.Length > 0)
            //{
            //    patientListFrm_PatientChanged(null, new PatientEventArgs(patientListFrm.PatientId, patientListFrm.VisitId));
            //}
            
            // 禁用排序
            foreach(DataGridViewColumn dc in dgvOrdersExecute.Columns)
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
            if (chkLongTerm.Checked == true && chkTempTerm.Checked == true && chkNotExecuted.Checked == true && chkExecuted.Checked == true)
            {
                return string.Empty;
            }
            
            // 长期/临时全不选
            if (chkLongTerm.Checked == false && chkTempTerm.Checked == false)
            {
                return "(1 = 2) ";
            }
            
            // 未执行/已执行
            if (chkExecuted.Checked == false && chkNotExecuted.Checked == false)
            {
                return "(1 = 2)";
            }
            
            string filter = string.Empty;

            // 如果长期/临时全选
            if (chkLongTerm.Checked == true && chkTempTerm.Checked == true)
            {
            }
            else
            {
                if (filter.Length > 0) filter += " AND ";
                
                if (chkLongTerm.Checked == true)
                {
                    filter = "REPEAT_INDICATOR = '1' ";
                }
                else
                {
                    filter = "REPEAT_INDICATOR <> '1' ";
                }
            }
            
            // 未执行
            if (chkNotExecuted.Checked == true && chkExecuted.Checked == true)
            {

            }
            else
            {
                if (filter.Length > 0) filter += " AND ";

                if (chkExecuted.Checked == true)
                {
                    filter += "NOT (IS_EXECUTE IS NULL OR IS_EXECUTE = '0') ";
                }
                else
                {
                    filter += "(IS_EXECUTE IS NULL OR IS_EXECUTE = '0') ";
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
            
            excelTemplatePrintData();                       // 输出数据
                        
            //excel.Print();				                // 打印
            excelAccess.PrintPreview();			            // 预览
            
            excelAccess.Close(false);				        // 关闭并释放			
        }

        
        /// <summary>
        /// 向Excel中输出数据
        /// </summary>
        private void excelTemplatePrintData()
        {
            // 单行输出
            for (int k = 0; k < excelConfig.ConfigSections.Count; k++)
            {
                ExcelConfigSection configSection = excelConfig.ConfigSections[k];
                if (configSection.TableName.Equals("PRINT_INFO") == false) continue;
                
                DataRow dr = dsPrint.Tables[0].Rows[0];
                
                // 输出数据
                for (int n = 0; n < configSection.ConfigItems.Count; n++)
                {
                    ExcelItem excelItem = configSection.ConfigItems[n];
                    setExcelCellText(dr, excelItem.ItemId, excelItem.Row, excelItem.Col, excelItem.CheckValue);
                }
            }

            DataRow[] drFind = dsOrdersExecute.Tables[0].Select(dsOrdersExecute.Tables[0].DefaultView.RowFilter, dsOrdersExecute.Tables[0].DefaultView.Sort);
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

                    // 输出多行项目
                    for (int k = 0; k < excelConfig.ConfigSections.Count; k++)
                    {
                        ExcelConfigSection configSection = excelConfig.ConfigSections[k];
                        if (configSection.MultiRows == false) continue;

                        // 输出数据
                        for (int n = 0; n < configSection.ConfigItems.Count; n++)
                        {
                            ExcelItem excelItem = configSection.ConfigItems[n];
                            setExcelCellText(dr, excelItem.ItemId, i + excelItem.Row, excelItem.Col, excelItem.CheckValue);
                        }
                    }
                    
                    continue;
                }
                
                // 输出多行项目
                for (int k = 0; k < excelConfig.ConfigSections.Count; k++)
                {
                    ExcelConfigSection configSection = excelConfig.ConfigSections[k];
                    if (configSection.MultiRows == false) continue;
                                        
                    // 输出数据
                    for (int n = 3; n < configSection.ConfigItems.Count; n++)
                    {
                        ExcelItem excelItem = configSection.ConfigItems[n];
                        setExcelCellText(dr, excelItem.ItemId, i + excelItem.Row, excelItem.Col, excelItem.CheckValue);
                    }
                }
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
        /// 获取打印信息
        /// </summary>
        /// <returns></returns>
        private void getPrintInfo()
        {
            if (dsPrint == null) dsPrint = new DataSet();
            if (dsPrint.Tables.Count == 0)
            {
                DataTable dt = dsPrint.Tables.Add("PRINT_INFO");
                dt.Columns.Add("USER_NAME", typeof(string));
                dt.Columns.Add("DEPT_CODE", typeof(string));
                dt.Columns.Add("DEPT_NAME", typeof(string));
                dt.Columns.Add("DATE_BEGIN", typeof(DateTime));
                dt.Columns.Add("DATE_END", typeof(DateTime));
            }

            DataRow drEdit = null;
            if (dsPrint.Tables[0].Rows.Count == 0)
            {
                drEdit = dsPrint.Tables[0].NewRow();
            }
            else
            {
                drEdit = dsPrint.Tables[0].Rows[0];
            }

            string deptName = string.Empty;
            string sql = "SELECT DEPT_NAME FROM DEPT_DICT WHERE DEPT_CODE = " + SQL.SqlConvert(MdiFrm.GetInstance().DeptCode);// patientListFrm.DeptCode
            if (GVars.OracleAccess.SelectValue(sql) == true) deptName = GVars.OracleAccess.GetResult(0);
            
            drEdit["USER_NAME"]  = GVars.User.Name;
            drEdit["DEPT_CODE"] = MdiFrm.GetInstance().DeptCode;// patientListFrm.DeptCode;
            drEdit["DEPT_NAME"]  = deptName;
            drEdit["DATE_BEGIN"] = dtRngStart.Value;
            drEdit["DATE_END"]   = dtRngEnd.Value;

            if (dsPrint.Tables[0].Rows.Count == 0)
            {
                dsPrint.Tables[0].Rows.Add(drEdit);
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
                    filter += "AND (PATS_IN_HOSPITAL.WARD_CODE = " + SQL.SqlConvert(wardCode)
                            + " OR PATS_IN_HOSPITAL.DEPT_CODE = " + SQL.SqlConvert(wardCode) + ") ";
                }            
            }

            string sql = "SELECT 1 SELECTION, ORDERS.DOSAGE, ORDERS.DOSAGE_UNITS, ORDERS_EXECUTE.* , PATS_IN_HOSPITAL.BED_NO, PAT_MASTER_INDEX.NAME "
                        + "FROM ORDERS_EXECUTE , PATS_IN_HOSPITAL, ORDERS, PAT_MASTER_INDEX  "
                        + "WHERE ORDERS_EXECUTE.PATIENT_ID = PATS_IN_HOSPITAL.PATIENT_ID "
                        +       "AND ORDERS_EXECUTE.VISIT_ID = PATS_IN_HOSPITAL.VISIT_ID "
                        +       "AND ORDERS_EXECUTE.PATIENT_ID = ORDERS.PATIENT_ID "
                        +       "AND ORDERS_EXECUTE.VISIT_ID = ORDERS.VISIT_ID " 
                        +       "AND ORDERS_EXECUTE.ORDER_NO = ORDERS.ORDER_NO "
                        +       "AND ORDERS_EXECUTE.ORDER_SUB_NO = ORDERS.ORDER_SUB_NO "
                        +       "AND (ORDERS.STOP_DATE_TIME IS NULL OR ORDERS.STOP_DATE_TIME >= ORDERS_EXECUTE.SCHEDULE_PERFORM_TIME) "
                        +       "AND " + filter
                        +       "AND PAT_MASTER_INDEX.PATIENT_ID = ORDERS_EXECUTE.PATIENT_ID ";
            //LogFile.WriteLog(sql);
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
        #endregion

        public void Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
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

        void IBasePatient.Patient_ListRefresh(object sender, PatientEventArgs e)
        {
             
        }
    }
}
