using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class NursingRecordFrm : FormDo, IBasePatient
    {
        #region 变量定义
        private const string RIGHT_EDIT = "2";              //  编辑别人录入记录的权限

        private const string COL_RECORD_DATE = "RECORD_DATE";
        private const string COL_ROW_INDEX = "ROW_INDEX";

        private const string GROUP_BEGIN = "╗";
        private const string GROUP_IN = "║";
        private const string GROUP_END = "╝";

        private string patientId = string.Empty;     // 病人ID号
        private string visitId = string.Empty;     // 本次就诊序号

        private DataSet dsPatient = null;             // 病人信息

        private DataSet dsRecordDefine = null;             // 护理记录单定义
        private DataSet dsRecordContent = null;             // 护理记录
        private DataSet dsPageInfo = null;             // 打印页面信息

        protected string _typeId = "01";
        private string colSign = string.Empty;     // 签名列
        private Hashtable vitalCodeList = null;             // 对应生命体征代码列表
        private TextBox txtMeasure = new TextBox();

        private string cellValue = string.Empty;     // 单元格的值

        private string _template = "护理记录单";     // 模板
        private TextTemplateInputFrm templateInput = null;             // 模板输入

        private  OptionSelectionFrm optionsSelect = null;             // 选项选择窗体

        private DataGridViewCellStyle dtCellStyle = new DataGridViewCellStyle();
        #endregion


        public NursingRecordFrm()
        {
            InitializeComponent();

            _id = "00012";
            _guid = "9FE144E0-16F7-4b94-88DE-E7862FEB2DA9";
        }


        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NursingRecordFrm_Load(object sender, EventArgs e)
        {
            try
            {
                initDisp();


                // 初始化变量
                initFrmVal();

                timer1.Enabled = true;
            }
            catch (Exception ex)
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
                timer1.Enabled = false;

                // 获取页面定义
                initDisp_dataGrid();
                vitalCodeList = getVitalCodeList();

                // 显示病人信息                
                //patientChanged();

                // 初始选择窗体
                initOptionSelectionFrm();

                templateInput = new TextTemplateInputFrm();

                Patient_SelectionChanged(null, new PatientEventArgs(GVars.Patient.ID, GVars.Patient.VisitId));
            }
            catch (Exception ex)
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

        public void Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
            //if (patientInfoFrm == null) return;

            //patientInfoFrm.ShowPatientInfo(e.PatientId, e.VisitId);

            GVars.Patient.ID = e.PatientId;
            GVars.Patient.VisitId = e.VisitId;

            patientId = GVars.Patient.ID;
            visitId = GVars.Patient.VisitId;

            btnQuery_Click(null, null);
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

                // 获取记录
                dsRecordContent = getNursingRecord(_typeId, patientId, visitId, dtSearchStart.Value, dtSearchEnd.Value);

                // 获取病人信息
                dsPatient = getPatientInfo(patientId, visitId);

                // 显示记录
                dsRecordContent.Tables[0].DefaultView.Sort = COL_RECORD_DATE;
                dgvData.DataSource = dsRecordContent.Tables[0].DefaultView;

                // 打印页面信息
                dsPageInfo = getPageInfo();

                // 获取病人历史信息
                //getPatientHistoryInfo();

                // 界面控制
                btnSave.Enabled = false;
                btnPrintContinue.Enabled = true;
                btnRePrint.Enabled = true;
            }
            catch (Exception ex)
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
        /// 按钮[删除]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // 条件检查
                if (dgvData.CurrentRow == null)
                {
                    return;
                }

                if (dgvData.CurrentRow.ReadOnly == true)
                {
                    GVars.Msg.Show("I0013", "删除");                    // 您没有{0}权限!
                    return;
                }

                // 确认是否要删除
                if (GVars.Msg.Show("Q0005") != DialogResult.Yes)        // 您确认要删除当前记录吗?
                {
                    return;
                }

                // 删除
                dgvData.Rows.Remove(dgvData.CurrentRow);

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
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

                // 把表格数据提交到缓存中
                dgvData.CurrentCell = null;

                // 保存缓存数据到db中
                dbAccess.SaveTableData(dsRecordContent);

                btnSave.Enabled = false;

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[重打]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRePrint_Click(object sender, EventArgs e)
        {
            try
            {
                // LB20110627,打印机条件检查
                //string dllName = this.GetType().Module.Name.Replace(".dll", "");
                //string Result = string.Empty;
                //GVars.PrinterInfos.SetDefaultPrinter(dllName, out Result);
                //if (Result.Length > 0)
                //{
                //    GVars.Msg.MsgId = "E";
                //    GVars.Msg.MsgContent.Add(Result);
                //    GVars.Msg.Show();
                //    return;
                //}
                //LB20110627结束

                if (reprintModeFrm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                dsPageInfo = getPageInfo();
                if (reprintModeFrm.PrintSame == true)
                {
                    LogFile.WriteLog("原样重打");
                    reprint(reprintModeFrm.PageStart, reprintModeFrm.PageEnd);
                }
                else
                {
                    DataSet dsRecord = new DataSet();

                    // 全部重打
                    if (reprintModeFrm.RePrintAll == true)
                    {
                        if (printWhole() == false)
                        {
                            GVars.Msg.Show();
                        }

                        return;
                    }
                    // 指定首页页码
                    else if (reprintModeFrm.FirstPage > 0)
                    {
                        if (reprint(reprintModeFrm.FirstPage, ref dsRecord) == false)
                        {
                            dsPageInfo.RejectChanges();
                            GVars.Msg.Show();
                        }
                    }
                    // 指定首页日期
                    else
                    {
                        if (reprint(reprintModeFrm.FirstDate, ref dsRecord) == false)
                        {
                            dsPageInfo.RejectChanges();
                            GVars.Msg.Show();
                        }
                    }

                    if (GVars.Msg.Show("Q0009", "重打") == DialogResult.Yes)    // {0}成功了吗?
                    {
                        this.Cursor = Cursors.WaitCursor;

                        dbAccess.SaveData(null, new object[] { dsRecord, dsPageInfo });

                        btnQuery_Click(sender, e);
                        this.Cursor = Cursors.Default;
                    }

                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[继打]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintContinue_Click(object sender, EventArgs e)
        {
            try
            {
                // LB20110627,打印机条件检查
                //string dllName = this.GetType().Module.Name.Replace(".dll", "");
                //string Result = string.Empty;
                //GVars.PrinterInfos.SetDefaultPrinter(dllName, out Result);
                //if (Result.Length > 0)
                //{
                //    GVars.Msg.MsgId = "E";
                //    GVars.Msg.MsgContent.Add(Result);
                //    GVars.Msg.Show();
                //    return;
                //}
                //LB20110627结束

                DataSet dsRecord = new DataSet();
                dsPageInfo = getPageInfo();
                //dsPageInfo.WriteXml(@"d:\dsPageInfo.xml", XmlWriteMode.WriteSchema);
                //dsPageInfo.ReadXml(@"d:\dsPageInfo.xml", XmlReadMode.ReadSchema);

                if (printContinue(ref dsRecord) == true)
                {
                    if (GVars.Msg.Show("Q0009", "续打") == DialogResult.Yes)    // {0}成功了吗?
                    {
                        this.Cursor = Cursors.WaitCursor;

                        dbAccess.SaveData(null, new object[] { dsRecord, dsPageInfo });

                        btnQuery_Click(sender, e);
                        this.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    GVars.Msg.Show();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
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


        #region 数据录入
        /// <summary>
        /// 画行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
        }


        /// <summary>
        /// 数据绑定完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                // 设置权限
                if (colSign.Length > 0)
                {
                    foreach (DataGridViewRow dgvRow in dgvData.Rows)
                    {
                        if (dgvRow.Cells[colSign].Value != null)
                        {
                            string editNurse = dgvRow.Cells[colSign].Value.ToString();
                            dgvRow.ReadOnly = !(_userRight.IndexOf(RIGHT_EDIT) >= 0 || editNurse.Equals(GVars.User.Name));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 滚动条滚动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvData_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                // 如果是水平滚动
                if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                {
                    panelTitle.Left = -1 * e.NewValue;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 自动增加默认值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvData_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells["PATIENT_ID"].Value = patientId;
                e.Row.Cells["VISIT_ID"].Value = visitId;
                e.Row.Cells["TYPE_ID"].Value = _typeId;
                e.Row.Cells[COL_RECORD_DATE].Value = DateTime.Now.ToString(ComConst.FMT_DATE.LONG);

                if (colSign.Length > 0)
                {
                    e.Row.Cells[colSign].Value = GVars.User.Name;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void dgvData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
            {
                cellValue = string.Empty;
            }
            else
            {
                cellValue = dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
        }


        private void dgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string tempValue = string.Empty;

            if (dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                tempValue = dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }

            if (cellValue.Equals(tempValue) == false && GVars.App.UserInput == true)
            {
                btnSave.Enabled = true;
            }
        }


        /// <summary>
        /// 表格双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewColumn dgvCol = dgvData.Columns[e.ColumnIndex];

                // 如果是选择输入
                if (dgvCol.Tag != null && dgvCol.Tag.ToString().Length > 0 && dgvData.Rows[e.RowIndex].ReadOnly == false)
                {
                    string dictId = dgvCol.Tag.ToString();
                    string selectMode = string.Empty;
                    bool multiSelect = false;

                    // 判断选择模式
                    DataRow[] drFind = dsRecordDefine.Tables[0].Select("STORE_COL_NAME = " + SQL.SqlConvert(dgvCol.Name));
                    if (drFind.Length > 0)
                    {
                        multiSelect = drFind[0]["MULTI_VALUE"].ToString().Equals("1");
                        selectMode = drFind[0]["SELECTED_VALUE"].ToString();
                    }

                    // 模式处理                    
                    switch (selectMode)
                    {
                        case "0":       // 单选
                        case "1":       // 多选
                            optionsSelect.DictId = dictId;
                            optionsSelect.MultiSelect = multiSelect;
                            optionsSelect.StoreCode = selectMode.Equals("0");
                            optionsSelect.SelectedItems = string.Empty;

                            if (dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                            {
                                optionsSelect.SelectedItems = dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                            }

                            if (optionsSelect.ShowDialog() == DialogResult.OK)
                            {
                                dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = optionsSelect.SelectedItems;
                                btnSave.Enabled = true;
                            }
                            break;

                        case "2":       // 模板输入
                            templateInput.DictId = dictId;
                            templateInput.MaxLength = dsRecordContent.Tables[0].Columns[dgvCol.Name].MaxLength;
                            templateInput.TextEdit = string.Empty;
                            templateInput.ColWidth = dgvData.Columns[e.ColumnIndex].Width;

                            if (dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                            {
                                templateInput.TextEdit = getRecordContent(e.RowIndex, e.ColumnIndex);
                            }

                            if (templateInput.ShowDialog() == DialogResult.OK)
                            {
                                //dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = templateInput.TextEdit;
                                acceptTemplateInputText(e.RowIndex, e.ColumnIndex, templateInput.Lines);

                                acceptDataGridNewRow();
                                btnSave.Enabled = true;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 进入单元格事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvData_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                mnuVitalSigns_Delete.Enabled = (dgvData.Rows[e.RowIndex].ReadOnly == false);
                mnuVitalSigns_AddLine.Enabled = mnuVitalSigns_Delete.Enabled;
                mnuVitalSigns_Group.Enabled = mnuVitalSigns_Delete.Enabled;
                //bool isNewRow = dgvData.Rows[e.RowIndex].IsNewRow;
                //mnuVitalSigns_Import.Visible = isNewRow;
                //mnuVitalSigns_InsertPageBreak.Visible = !isNewRow && dgvData.Columns.Contains(COL_PAGE_BREAK);                
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 菜单[导入生命体征]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuVitalSigns_Import_Click(object sender, EventArgs e)
        {
            try
            {
                if (vitalCodeList.Count == 0)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                DateTime dtStart = getLastNursingRecordDateTime();  // 获取时间开始点

                bool imported = addNewNursingRecord_VitalSigns(dtStart);
                if (addNewNursingRecord_OrdersExecute(dtStart) == true) imported = true;

                if (imported)
                {
                    dgvData.DataSource = dsRecordContent.Tables[0].DefaultView;
                    deleteEmptyRow();

                    btnSave.Enabled = true;
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 插入分页符
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuVitalSigns_InsertPageBreak_Click(object sender, EventArgs e)
        {
            try
            {
                //if (dgvData.CurrentRow == null)
                //{
                //    return;
                //}

                //// 撤消分页符
                //if (dgvData.CurrentRow.Cells[COL_PAGE_BREAK].Value.ToString().Equals("1") == true)
                //{
                //    dgvData.CurrentRow.Cells[COL_PAGE_BREAK].Value = "0";
                //    dgvData.CurrentRow.Cells[COL_RECORD_DATE].Style.BackColor = dgvData.RowsDefaultCellStyle.BackColor;
                //}
                //// 增加分页符
                //else
                //{
                //    dgvData.CurrentRow.Cells[COL_PAGE_BREAK].Value = "1";
                //    dgvData.CurrentRow.Cells[COL_RECORD_DATE].Style.BackColor = Color.Yellow;
                //}

                //dgvData.CurrentCell = null;

                //btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 菜单[删除]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuVitalSigns_Delete_Click(object sender, EventArgs e)
        {
            btnDelete_Click(sender, e);
        }


        /// <summary>
        /// 菜单[增加新记录]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuVitalSigns_AddRec_Click(object sender, EventArgs e)
        {
            try
            {
                // 插入新记录
                DataRow drNew = dsRecordContent.Tables[0].NewRow();

                drNew["PATIENT_ID"] = patientId;
                drNew["VISIT_ID"] = visitId;
                drNew["TYPE_ID"] = _typeId;
                drNew["RECORD_DATE"] = DateTime.Now;
                drNew["ROW_INDEX"] = 1;

                if (colSign.Length > 0)
                {
                    drNew[colSign] = GVars.User.Name;           // 记录人
                }

                dsRecordContent.Tables[0].Rows.Add(drNew);

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 菜单[增加新行]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuVitalSigns_AddLine_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvData.CurrentRow == null)
                {
                    return;
                }

                // 查找对应的数据
                int rowIndex = -1;
                string filter = getFilterFromDgvRow(dgvData.CurrentRow.Index, ref rowIndex);

                DataRow[] drFind = dsRecordContent.Tables[0].Select(filter, COL_ROW_INDEX);
                if (drFind.Length == 0) return;

                // 修改行后记录的ROW_INDEX
                for (int i = drFind.Length - 1; i >= 0; i--)
                {
                    if (int.Parse(drFind[i][COL_ROW_INDEX].ToString()) <= rowIndex) break;
                    drFind[i][COL_ROW_INDEX] = int.Parse(drFind[i][COL_ROW_INDEX].ToString()) + 1;
                }

                // 在该行插入新记录
                addNewLineInRec(dgvData.CurrentCell.RowIndex, rowIndex + 1, string.Empty, string.Empty);

                // 设置按钮状态
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 菜单[分组]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuVitalSigns_Group_Click(object sender, EventArgs e)
        {
            try
            {
                int colLast = -1;
                int row0 = -1;
                int row1 = -1;

                if (dgvData.SelectedCells.Count == 0) return;

                // 查找选择单元格的范围
                for (int i = 0; i < dgvData.SelectedCells.Count; i++)
                {
                    DataGridViewCell dgvCell = dgvData.SelectedCells[i];

                    if (dgvCell.ColumnIndex > colLast) colLast = dgvCell.ColumnIndex;
                    if (dgvCell.RowIndex > row1) row1 = dgvCell.RowIndex;
                    if (dgvCell.RowIndex < row0 || row0 < 0) row0 = dgvCell.RowIndex;
                }

                if (row0 == row1) return;

                // 查找分组符号应该放在哪一列
                int colGroup = colLast;
                for (int i = colLast + 1; i < dgvData.Columns.Count; i++)
                {
                    if (dgvData.Columns[i].Visible == false) continue;
                    if (dgvData.Columns[i].ReadOnly == true) continue;
                    colGroup = i;
                    break;
                }

                if (colGroup == colLast) return;

                // 设置分组符号
                dgvData.Rows[row0].Cells[colGroup].Value = GROUP_BEGIN;
                for (int i = row0 + 1; i < row1; i++)
                {
                    dgvData.Rows[i].Cells[colGroup].Value = GROUP_IN;
                }
                dgvData.Rows[row1].Cells[colGroup].Value = GROUP_END;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void dgvData_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode != Keys.Delete) return;

                for (int i = 0; i < dgvData.SelectedCells.Count; i++)
                {
                    DataGridViewCell dgvCell = dgvData.SelectedCells[i];
                    if (dgvCell.ReadOnly == true) continue;
                    if (dgvData.Columns[dgvCell.ColumnIndex].Name.ToUpper().Equals(COL_RECORD_DATE) == true) continue;

                    dgvCell.Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion


        #region 共通函数
        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {
            btnSave.Enabled = false;
            btnPrintContinue.Enabled = false;
            btnRePrint.Enabled = false;

            //grpPatientList.Height = this.Height - 19;
            //grpSeach.Top = grpPatientList.Height + grpPatientList.Top - grpSeach.Height;
            grpWorkSpace.Height = grpSeach.Top - grpWorkSpace.Top - 10;

            //grpPatientInfo.Width = this.Width - grpPatientInfo.Left - grpPatientList.Left;
            //grpWorkSpace.Width = grpPatientInfo.Width;
            //grpSeach.Width = grpPatientInfo.Width;
        }


        /// <summary>
        /// 初始化表格显示
        /// </summary>
        private void initDisp_dataGrid()
        {
            // 加表格标题
            loadGridTitle();

            // 加载表格列
            DataRow[] drFind = dsRecordDefine.Tables[0].Select(string.Empty, "SERIAL_NO");

            dgvData.AutoGenerateColumns = false;
            dgvData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            DataGridViewColumn dgvCol = null;
            DataRow drRec = null;

            //dgvData.ColumnHeadersVisible = true;

            int colWidth = 0;

            // 主键列
            dgvCol = new DataGridViewTextBoxColumn();
            dgvCol.Name = "PATIENT_ID";
            dgvCol.DataPropertyName = "PATIENT_ID";
            dgvCol.Visible = false;
            dgvData.Columns.Add(dgvCol);
            dgvData.Columns["PATIENT_ID"].Visible = false;


            dgvCol = new DataGridViewTextBoxColumn();
            dgvCol.Name = "VISIT_ID";
            dgvCol.DataPropertyName = "VISIT_ID";
            dgvCol.Visible = false;
            dgvData.Columns.Add(dgvCol);
            dgvData.Columns["VISIT_ID"].Visible = false;

            dgvCol = new DataGridViewTextBoxColumn();
            dgvCol.Name = "TYPE_ID";
            dgvCol.DataPropertyName = "TYPE_ID";
            dgvData.Columns.Add(dgvCol);
            dgvCol.Visible = false;
            dgvData.Columns["TYPE_ID"].Visible = false;

            dgvCol = new DataGridViewTextBoxColumn();
            dgvCol.Name = "ROW_INDEX";
            dgvCol.DataPropertyName = "ROW_INDEX";
            dgvCol.Visible = false;
            dgvData.Columns.Add(dgvCol);
            dgvData.Columns["ROW_INDEX"].Visible = false;

            dtCellStyle.Format = "g";
            dtCellStyle.NullValue = null;

            // 数据列
            for (int i = 0; i < drFind.Length; i++)
            {
                drRec = drFind[i];
                dgvCol = new DataGridViewTextBoxColumn();

                dgvCol.Name = drRec["STORE_COL_NAME"].ToString();
                dgvCol.DataPropertyName = drRec["STORE_COL_NAME"].ToString();

                if (dgvCol.Name.Equals(COL_RECORD_DATE) == true)
                {
                    dgvCol.DefaultCellStyle = dtCellStyle;
                }

                if (int.TryParse(drRec["COL_WIDTH"].ToString(), out colWidth) == true)
                {
                    dgvCol.Width = colWidth;
                    dgvCol.Visible = (colWidth > 0);
                }
                else
                {
                    dgvCol.Visible = false;
                }

                if (drRec["SIGN_FLAG"].ToString().Equals("1") == true)
                {
                    dgvCol.ReadOnly = true;
                    colSign = drRec["STORE_COL_NAME"].ToString();
                }

                //if (drRec["DICT_ID"].ToString().Length > 0)
                //{
                //    dgvCol.ReadOnly = true;
                //}

                dgvCol.Tag = drRec["DICT_ID"].ToString();
                if (dgvCol.Tag != null && dgvCol.Tag.ToString().Length > 0)
                {
                    dgvCol.ReadOnly = true;
                }

                dgvData.Columns.Add(dgvCol);
            }
        }


        /// <summary>
        /// 初始化选项选择窗体
        /// </summary>
        private void initOptionSelectionFrm()
        {
            if (optionsSelect == null)
            {
                optionsSelect = new OptionSelectionFrm();
            }

            string dictIdList = string.Empty;
            foreach (DataRow dr in dsRecordDefine.Tables[0].Rows)
            {
                string dictId = dr["DICT_ID"].ToString();
                if (dictId.Length > 0)
                {
                    dictIdList += (dictIdList.Length > 0 ? ComConst.STR.COMMA : string.Empty);
                    dictIdList += SQL.SqlConvert(dictId);
                }
            }

            if (dictIdList.Length > 0)
            {
                dictIdList = " (" + dictIdList + ")";
                optionsSelect.DsItemDict = dbAccess.GetTableData("HIS_DICT_ITEM", "DICT_ID IN " + dictIdList);
            }
        }


        /// <summary>
        /// 获取行数
        /// </summary>
        /// <param name="text"></param>
        /// <param name="width"></param>
        /// <param name="font"></param>
        /// <returns></returns>       
        private int getLines(string text, int width)
        {
            if (txtMeasure.Tag == null)
            {
                txtMeasure.Multiline = true;
                txtMeasure.Font = dgvData.RowsDefaultCellStyle.Font;

                txtMeasure.Tag = "INIT";
            }

            txtMeasure.Width = width;
            txtMeasure.Text = text;

            return txtMeasure.GetLineFromCharIndex(text.Length) + 1;
        }


        /// <summary>
        /// 获取某一行的字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="width"></param>
        /// <param name="idx_line"></param>
        /// <returns></returns>
        private string getLineInText(string text, int width, int idx_line)
        {
            if (width <= 0)
            {
                return string.Empty;
            }

            if (txtMeasure.Tag == null)
            {
                txtMeasure.Multiline = true;
                txtMeasure.Font = dgvData.RowsDefaultCellStyle.Font;

                txtMeasure.Tag = "INIT";
            }

            txtMeasure.Width = width;
            txtMeasure.Text = text;

            int pos0 = txtMeasure.GetFirstCharIndexFromLine(idx_line);
            int pos1 = txtMeasure.GetFirstCharIndexFromLine(idx_line + 1);
            if (pos0 >= 0 && (pos1 > pos0 || pos1 == -1))
            {
                if (pos1 == -1)
                {
                    return text.Substring(pos0);
                }
                else
                {
                    return text.Substring(pos0, pos1 - pos0);
                }
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 加载护理记录单表头
        /// </summary>
        private void loadGridTitle()
        {
            panelTitle.BackgroundImage = null;

            string imgFile = System.IO.Path.Combine(Application.StartupPath, "Template\\" + _template + _typeId + ".bmp");
            if (File.Exists(imgFile) == true)
            {

                /*
                 * 原为 panelTitle.BackgroundImage    
                 * 改成了 panelTitle.Appearance.Image
                 */
                panelTitle.Appearance.Image = Image.FromFile(imgFile);
                //panelTitle.BackgroundImageLayout = ImageLayout.Zoom;
                panelTitle.Left = 0;
                panelTitle.Width = panelTitle.Appearance.Image.Width + 2;
                panelTitle.Height = panelTitle.Appearance.Image.Height;
                pnlHeaderHolder.Height = panelTitle.Height;

                int offsetAdd = pnlHeaderHolder.Top + pnlHeaderHolder.Height - dgvData.Top;
                dgvData.Top = pnlHeaderHolder.Top + pnlHeaderHolder.Height;
                dgvData.Height = dgvData.Height - offsetAdd;
                //dgvData.Width     = panelTitle.BackgroundImage.Width;
            }
        }


        /// <summary>
        /// 接受新行数据
        /// </summary>
        private void acceptDataGridNewRow()
        {
            int row = dgvData.CurrentCell.RowIndex;
            int col = dgvData.CurrentCell.ColumnIndex;

            if (dgvData.CurrentRow.IsNewRow == true)
            {
                DataRow drNew = dsRecordContent.Tables[0].NewRow();
                foreach (DataGridViewColumn dgvCol in dgvData.Columns)
                {
                    if (dgvCol.DataPropertyName != null && dgvCol.DataPropertyName.Length > 0)
                    {
                        drNew[dgvCol.DataPropertyName] = dgvData.CurrentRow.Cells[dgvCol.Name].Value;
                    }
                }

                dgvData.AllowUserToAddRows = false;

                if (dsRecordContent.Tables[0].Rows.Count == 0)
                {
                    dsRecordContent.Tables[0].Rows.Add(drNew);
                }

                try
                {
                    dgvData.AllowUserToAddRows = true;
                }
                catch
                {
                    dgvData.Rows[dgvData.Rows.Count - 1].Cells[COL_RECORD_DATE].Value = DateTime.Now.ToString(ComConst.FMT_DATE.LONG);
                }

                // 重定位当前行
                dgvData.CurrentCell = dgvData.Rows[row].Cells[col];
            }
        }


        /// <summary>
        /// 删除空白行
        /// </summary>
        private void deleteEmptyRow()
        {
            bool isEmpty = false;

            DataGridViewRow dgvRow = null;
            for (int i = dgvData.Rows.Count - 1; i >= 0; i--)
            {
                dgvRow = dgvData.Rows[i];

                if (dgvRow.IsNewRow) continue;

                isEmpty = true;
                foreach (DataGridViewColumn dgvCol in dgvData.Columns)
                {
                    if (dgvRow.Cells[dgvCol.Name].Visible == true
                     && dgvRow.Cells[dgvCol.Name].Value != null
                     && dgvRow.Cells[dgvCol.Name].Value.ToString().Length > 0
                     && dgvCol.ReadOnly == false
                     && dgvCol.Name.Equals("RECORD_DATE") == false)
                    {
                        isEmpty = false;
                        break;
                    }
                }

                if (isEmpty)
                {
                    dgvData.Rows.RemoveAt(i);
                }
            }
        }


        /// <summary>
        /// 获取记录内容, 如果一个记录写在多个行上.
        /// </summary>
        /// <returns></returns>
        private string getRecordContent(int row, int col)
        {
            // 获取记录开始行
            string dateStr = dgvData.Rows[row].Cells[COL_RECORD_DATE].FormattedValue.ToString();

            int rowStart = 0;
            for (int i = row; i >= 0; i--)
            {
                if (dateStr.Equals(dgvData.Rows[i].Cells[COL_RECORD_DATE].FormattedValue.ToString()) == false)
                {
                    break;
                }

                rowStart = i;
            }

            // 获取记录内容
            string cellValue = string.Empty;
            for (int i = rowStart; i < dgvData.Rows.Count; i++)
            {
                if (dateStr.Equals(dgvData.Rows[i].Cells[COL_RECORD_DATE].FormattedValue.ToString()) == false)
                {
                    break;
                }

                if (cellValue.Length > 0) cellValue += ComConst.STR.CRLF;
                cellValue += dgvData.Rows[i].Cells[col].Value.ToString();
            }

            return cellValue;
        }
        #endregion


        #region 表格数据交互
        /// <summary>
        /// 获取当前行的过滤条件, 并获取当前行的位置
        /// </summary>
        /// <param name="dgvRowIndex"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private string getFilterFromDgvRow(int dgvRowIndex, ref int rowIndexInRec)
        {
            rowIndexInRec = -1;

            DataGridViewRow dgvRow = dgvData.Rows[dgvRowIndex];

            DataColumn dc = null;
            string filter = string.Empty;

            for (int i = 0; i < dsRecordContent.Tables[0].PrimaryKey.Length; i++)
            {
                dc = dsRecordContent.Tables[0].PrimaryKey[i];
                if (dc.ColumnName.ToUpper().Equals(COL_ROW_INDEX) == true)
                {
                    rowIndexInRec = int.Parse(dgvRow.Cells[COL_ROW_INDEX].Value.ToString());
                    continue;
                }

                if (filter.Length > 0) filter += " AND ";
                if (dc.DataType == typeof(DateTime))
                {
                    DateTime dtTemp = (DateTime)(dgvRow.Cells[dc.ColumnName].Value);

                    filter += "(" + dc.ColumnName + " >= " + SQL.SqlConvert(dtTemp.ToString(ComConst.FMT_DATE.LONG));
                    filter += "AND " + dc.ColumnName + " < " + SQL.SqlConvert(dtTemp.AddSeconds(1).ToString(ComConst.FMT_DATE.LONG));
                    filter += ") "; ;
                }
                else
                {
                    filter += dc.ColumnName + " = ";
                    filter += SQL.SqlConvert(dgvRow.Cells[dc.ColumnName].Value.ToString());
                }
            }

            return filter;
        }


        /// <summary>
        /// 在一条记录中增加新行
        /// </summary>
        /// <returns></returns>
        private bool addNewLineInRec(int dgvRowBased, int rowIndexInRec, string colName, string newValue)
        {
            DataGridViewRow dgvRow = dgvData.Rows[dgvRowBased];

            // 插入新记录
            DataColumn dc = null;
            DataRow drNew = dsRecordContent.Tables[0].NewRow();

            // 插入主键
            for (int i = 0; i < dsRecordContent.Tables[0].PrimaryKey.Length; i++)
            {
                dc = dsRecordContent.Tables[0].PrimaryKey[i];

                drNew[dc.ColumnName] = dgvRow.Cells[dc.ColumnName].Value;
            }

            if (colName.Length > 0)
            {
                drNew[colName] = newValue;
            }

            // 行序号
            drNew[COL_ROW_INDEX] = rowIndexInRec;

            // 记录人
            if (colSign.Length > 0)
            {
                drNew[colSign] = GVars.User.Name;
            }

            // 保存记录
            dsRecordContent.Tables[0].Rows.Add(drNew);

            return true;
        }


        /// <summary>
        /// 接受模板输入
        /// </summary>
        /// <returns></returns>
        private bool acceptTemplateInputText(int cellRowIndex, int cellColIndex, string[] lines)
        {
            // 查找主键
            int rowInRec = 0;
            string filter = getFilterFromDgvRow(cellRowIndex, ref rowInRec);

            // 查找记录
            DataRow[] drFind = dsRecordContent.Tables[0].Select(filter, COL_ROW_INDEX);

            // 保存结果
            string colName = dgvData.Columns[cellColIndex].Name;
            int i = 0;
            for (i = 0; i < drFind.Length && i < lines.Length; i++)
            {
                drFind[i][colName] = lines[i].TrimEnd();
            }

            // 清空其它记录
            for (; i < drFind.Length; i++)
            {
                drFind[i][colName] = string.Empty;
            }

            // 增加新行
            while (i < lines.Length)
            {
                addNewLineInRec(cellRowIndex, (i + 1), colName, lines[i].TrimEnd());
                i++;
            }

            // 删除多余的空白行
            DataRow dr = null;
            bool isEmpty = true;
            for (int j = drFind.Length - 1; j >= i; j--)
            {
                dr = drFind[j];
                isEmpty = true;

                // 判断是不是空白行
                foreach (DataGridViewColumn dgvCol in dgvData.Columns)
                {
                    if (dgvCol.Visible == false || dgvCol.Name.Equals(COL_RECORD_DATE) == true || dgvCol.Name.Equals(colSign) == true) continue;
                    if (dgvCol.Index == cellColIndex) continue;

                    if (dr[dgvCol.Name] != DBNull.Value && dr[dgvCol.Name].ToString().Length > 0)
                    {
                        isEmpty = false;
                        break;
                    }
                }

                // 如果是空白行, 删除
                if (isEmpty) dr.Delete();
            }

            return true;
        }
        #endregion

        void IBasePatient.Patient_ListRefresh(object sender, PatientEventArgs e)
        {
             
        }
    }
}
