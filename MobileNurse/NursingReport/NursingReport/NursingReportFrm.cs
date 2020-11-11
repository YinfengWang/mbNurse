using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using CommPrinter;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DXApplication2;
using HISPlus.UserControls;
using System.Configuration;
using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class NursingReportFrm : FormDo, IBasePatient
    {

        #region 变量定义

        private const string COL_RECORD_DATE = "RECORD_DATE";
        private const string COL_ROW_INDEX = "ROW_INDEX";

        private bool _isNewLine = false;   //是否新增的行
        private bool _isDel = false;       //是否删除行

        private const string GROUP_BEGIN = "╗";
        private const string GROUP_IN = "║";
        private const string GROUP_END = "╝";

        /// <summary>
        /// 护理记录单类别编号
        /// </summary>
        private string typeId = null;
        /// <summary>
        /// 签名列
        /// </summary>
        private string colSign = null;                    //签名列

        /// <summary>
        /// 护理记录单定义
        /// </summary>
        private DataSet dsRecordDefine = null;            //护理记录单定义
        /// <summary>
        /// 护理记录内容
        /// </summary>
        private DataSet dsRecordContent = null;           //护理记录内容
        /// <summary>
        /// 护理记录类别
        /// </summary>
        private DataSet dsReocrdType = null;              //护理记录类别
        /// <summary>
        /// 病人信息
        /// </summary>
        private DataSet dsPatient = null;                 //病人信息
        private DataSet dsPageInfo = null;

        private string patientId = GVars.Patient.ID;          // 病人ID号
        private string visitId = GVars.Patient.VisitId;            // 本次就诊序号

        /// <summary>
        /// 对应生命体征代码列表
        /// </summary>
        private Hashtable vitalCodeList = null;           //对应生命体征代码列表

        private Hashtable htPrintSkipFlg = null;
        /// <summary>
        /// 选项选择窗体
        /// </summary>
        private OptionSelectionFrm optionsSelect = null;             // 选项选择窗体
        /// <summary>
        /// 模板输入
        /// </summary>
        private TextTemplateInputFrm templateInput = null;             // 模板输入

        private List<PrimaryKeyItem> deleteRecordList = new List<PrimaryKeyItem>();

        #endregion
        struct PrimaryKeyItem
        {
            public string patient_id;
            public string visit_id;
            public string type_id;
            public string record_date;
        }

        public NursingReportFrm()
        {
            InitializeComponent();
            _id = "00080";
            _guid = "61563FC3-47B6-4a42-9A3E-4D455949BAD2";

            ucGridView1.ShowingEditor += ucGridView1_ShowingEditor;

        }

        void ucGridView1_ShowingEditor(object sender, CancelEventArgs e)
        {

            DataRow row = this.ucGridView1.GetSelectDataRow();
            if (row != null)
            {
                if (row["NURSE_SYS_NAME"].ToString() != GVars.User.Name)
                {
                    e.Cancel = true;
                }

            }

            //if (ucGridView1.FocusedColumn.ReadOnly)
            //    ucGridView1.ShowToolTip("已结束或无权限,无法编辑!");
            //string value = ucGridView1.SelectedRow["END_DATE"] as string;
            //if (string.IsNullOrEmpty(value)) return;

            //if (string.IsNullOrEmpty(_userRight))
            //    _userRight = GVars.User.GetUserFrmRight(_id);

            //if (string.IsNullOrEmpty(_userRight) || _userRight.IndexOf(RIGHT_MODIFY) < 0)
            //{
            //    e.Cancel = true;
            //    ucGridView1.ShowToolTip("已结束或无权限,无法编辑!");
            //}
        }

        private void NursingReport_Load(object sender, EventArgs e)
        {
            try
            {
                InitDisp();
                InitFromVal();
                InitFrom_Data();


                lblBedNo.Text = "床号:" + dsPatient.Tables[0].Select(" PATIENT_ID ='" + patientId + "' AND VISIT_ID='" + visitId + "'")[0]["BED_LABEL"].ToString();
                lblName.Text = "姓名:" + dsPatient.Tables[0].Select(" PATIENT_ID ='" + patientId + "' AND VISIT_ID='" + visitId + "'")[0]["NAME"].ToString();
                //lbl_ADMISSION_DATE_TIME.Text = "入院日期:" + dsPatient.Tables[0].Select(" PATIENT_ID ='" + patientId + "' AND VISIT_ID='" + visitId + "'")[0]["ADMISSION_DATE_TIME"].ToString();
                string admission_date_time = dsPatient.Tables[0].Select(" PATIENT_ID ='" + patientId + "' AND VISIT_ID='" + visitId + "'")[0]["ADMISSION_DATE_TIME"].ToString();
                if (!string.IsNullOrEmpty(admission_date_time))
                    lbl_ADMISSION_DATE_TIME.Text = "入院日期:" + Convert.ToDateTime(admission_date_time).ToString("yyyy-MM-dd");

                #region MyRegion
                //DataGridView dgvModule = dgvData;
                //DataGridViewCellStyle style = new DataGridViewCellStyle();
                //DataGridViewCellStyle style2 = new DataGridViewCellStyle();
                //DataGridViewCellStyle style3 = new DataGridViewCellStyle();
                ////style.BackColor = Color.FromArgb(0xf3, 0xf9, 0xff);
                //style.BackColor = this.BackColor;
                //dgvModule.AlternatingRowsDefaultCellStyle = style;
                ////dgvModule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //dgvModule.BackgroundColor = Color.White;
                //dgvModule.BorderStyle = BorderStyle.Fixed3D;
                //style2.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //style2.BackColor = SystemColors.Control;
                //style2.Font = new Font("宋体", 9f);
                //style2.ForeColor = SystemColors.WindowText;
                //style2.SelectionBackColor = SystemColors.Highlight;
                //style2.SelectionForeColor = SystemColors.HighlightText;
                //style2.WrapMode = DataGridViewTriState.True;
                //dgvModule.ColumnHeadersDefaultCellStyle = style2;
                ////dgvModule.ColumnHeadersHeight = 0x18;
                //dgvModule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                //style3.Alignment = DataGridViewContentAlignment.MiddleLeft;
                //style3.BackColor = SystemColors.Window;
                //style3.Font = new Font("宋体", 9f);
                //style3.ForeColor = Color.FromArgb(0x37, 0x47, 0x60);
                //style3.SelectionBackColor = SystemColors.Highlight;
                //style3.SelectionForeColor = SystemColors.HighlightText;
                //style3.WrapMode = DataGridViewTriState.False;
                //dgvModule.DefaultCellStyle = style3;
                ////dgvModule.EnableHeadersVisualStyles = false;                    
                //dgvModule.RowHeadersWidth = 15;
                //dgvModule.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                //dgvModule.RowTemplate.Height = 20;
                #endregion

                ucGridView1.Columns["CHECK_FLAG"].OptionsColumn.AllowEdit = false;
                ucGridView1.Columns["NURSE_SYS_NAME"].OptionsColumn.AllowEdit = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void InitDisp()
        {
            ////病人信息
            //patientInfoFrm = new PatientSearchFrm();
            //patientInfoFrm.PatientChanged += new PatientChangedEventHandler(patientInfoFrm_PatientChanged);
            //patientInfoFrm.FormBorderStyle = FormBorderStyle.None;
            //patientInfoFrm.TopLevel = false;
            //patientInfoFrm.Dock = DockStyle.Fill;
            //patientInfoFrm.BackColor = this.BackColor;
            //patientInfoFrm.Show();
            //grpPatientInfo.Controls.Add(patientInfoFrm);          

            vitalCodeList = new Hashtable();
            htPrintSkipFlg = new Hashtable();
            templateInput = new TextTemplateInputFrm();

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
        /// 初始化界面显示数据
        /// </summary>
        private void InitFrom_Data()
        {
            dsReocrdType = getNursingRecordType();
            dsReocrdType.Tables[0].DefaultView.Sort = "SHUNXU"; //排序  2015-11-25
            cmbNursingRecord.DisplayMember = "DESCRIPTIONS";
            cmbNursingRecord.ValueMember = "TYPE_ID";
            cmbNursingRecord.DataSource = dsReocrdType.Tables[0].DefaultView;

            cmbNursingRecord.SelectedIndex = 0;

            htPrintSkipFlg = getPrintSkipFlg();
        }

        /// <summary>
        /// 获取护理打印分页标识
        /// </summary>
        /// <returns></returns>
        private Hashtable getPrintSkipFlg()
        {
            Hashtable ht = new Hashtable();
            DataSet ds = getNursingPrintSkipFlg(GVars.User.DeptCode);
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ht.Add(dr["VITAL_CODE"], dr["VITAL_SIGNS"]);
                }
            }
            return ht;
        }


        /// <summary>
        /// 初始化表格显示
        /// </summary>
        private void InitDisp_DataGrid()
        {

            DataRow drRec = null;
            DataGridViewTextBoxColumn dgvColumn = null;

            ucGridView1.MultiSelect = true;
            ucGridView1.AllowEdit = true;
            ucGridView1.CellSelected = true;

            ucGridView1.ClearColumn();
            ucGridView1.ColumnAutoWidth = false;
            //ucGridView1.AutoSize = true;
            ucGridView1.Add("PATIENT_ID", "PATIENT_ID", false);
            ucGridView1.Add("VISIT_ID", "VISIT_ID", false);
            ucGridView1.Add("TYPE_ID", "TYPE_ID", false);
            ucGridView1.Add("ROW_INDEX", "ROW_INDEX", false);
            ucGridView1.Add("RECORD_DATE", "RECORD_DATE", false);

            DataRow[] drDefine = dsRecordDefine.Tables[0].Select(string.Empty, "SERIAL_NO");
            //sucGridView1.Columns[1].OptionsColumn.AllowEdit = false;
            DataGridViewCellStyle dgvCellStyle = new DataGridViewCellStyle();
            dgvCellStyle.Format = "g";
            dgvCellStyle.NullValue = null;



            int colWidth = 0;

            for (int i = 0; i < drDefine.Length; i++)
            {
                drRec = drDefine[i];

                int.TryParse(drRec["COL_WIDTH"].ToString(), out colWidth);

                if (colWidth == 0)
                {
                    ucGridView1.Add(drRec["COL_TITLE"].ToString(), drRec["STORE_COL_NAME"].ToString(), false);
                }

                if (drRec["STORE_COL_NAME"].ToString().Equals(COL_RECORD_DATE))
                {
                    ucGridView1.Add(drRec["COL_TITLE"].ToString(), drRec["STORE_COL_NAME"].ToString(), ComConst.FMT_DATE.LONG_MINUTE, colWidth,
                        drRec["SIGN_FLAG"].ToString().Equals("1") ? ColumnStatus.None : ColumnStatus.WordWrap);
                }
                else
                {
                    ucGridView1.Add(drRec["COL_TITLE"].ToString(), drRec["STORE_COL_NAME"].ToString(), colWidth,
                        drRec["SIGN_FLAG"].ToString().Equals("1") ? ColumnStatus.None : ColumnStatus.WordWrap);

                }

                if (drRec["SIGN_FLAG"].ToString().Equals("1"))
                {
                    colSign = drRec["STORE_COL_NAME"].ToString();
                }
                //dgvColumn.Tag = drRec["DICT_ID"].ToString();
            }

            ucGridView1.CellValueChanged += ucGridView1_CellValueChanged;
            //ucGridView1.DoubleClick += ucGridView1_DoubleClick;
            //ucGridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(ucGridView1_RowStyle);
            ucGridView1.ContextMenuStrip = contextMnu_VitalSigns;
            ucGridView1.Init();

            // 初始选择窗体
            initOptionSelectionFrm();

        }

        /// <summary>
        /// 根据审核状态，显示颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucGridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {

                DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                DataTable dt = new DataTable();
                if (ucGridView1.DataSource as DataView != null)
                {
                    dt = (ucGridView1.DataSource as DataView).ToTable();
                }
                //DataTable dt = (ucGridView1.DataSource as DataView).ToTable();
                if (e.RowHandle >= 0 && dt.Rows.Count > 0)
                {
                    string category = view.GetRowCellDisplayText(e.RowHandle, view.Columns[view.Columns.Count - 1]);//view.Columns.Count-1   CHECK_FLAG

                    string strRcl = string.Empty;
                    if (typeId == "27")
                    {
                        //strRcl = view.GetRowCellDisplayText(e.RowHandle, view.Columns["COL4"]);
                        //label4.Text += strRcl;
                        //if ("小结,总结".Split(',').Contains(strRcl))
                        //{
                        //    e.Appearance.BackColor = Color.Red;
                        //}
                    }

                    if (category == "未审核" || category == "待审核")
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 255, 255);
                    }
                    if (category == "审核未通过")
                    {
                        e.Appearance.BackColor = Color.FromArgb(188, 143, 143);
                    }
                    if (category == "审核通过")
                    {
                        e.Appearance.BackColor = Color.FromArgb(176, 224, 230);
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        void ucGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataRow row = this.ucGridView1.GetSelectDataRow();
                if (row != null)
                {
                    if (row["NURSE_SYS_NAME"].ToString() != GVars.User.Name)
                    {
                        return;
                    }

                }

                if ((typeId == "27" && ucGridView1.FocusedColumn.FieldName == "COL4")
                    || (typeId == "22" && ucGridView1.FocusedColumn.FieldName == "COL23")
                    || (typeId == "31" && ucGridView1.FocusedColumn.FieldName == "COL12"))
                {
                    string patName = dsPatient.Tables[0].Select(" PATIENT_ID ='" + patientId + "' AND VISIT_ID='" + visitId + "'")[0]["NAME"].ToString();
                    PatientOrdermFrm pof = new PatientOrdermFrm(patientId, visitId, patName);
                    pof.ShowDialog();
                    string drValue = pof.resultValue;
                    if (!string.IsNullOrEmpty(drValue))
                    {
                        ucGridView1.SetRowCellValue(ucGridView1.FocusedRowHandle, ucGridView1.FocusedColumn,
                                drValue);

                    }
                }
                //else if (typeId == "22" && ucGridView1.FocusedColumn.FieldName == "COL23")
                //{
                //    string patName = dsPatient.Tables[0].Select(" PATIENT_ID ='" + patientId + "' AND VISIT_ID='" + visitId + "'")[0]["NAME"].ToString();
                //    PatientOrdermFrm pof = new PatientOrdermFrm(patientId, visitId, patName);
                //    pof.ShowDialog();
                //    string drValue = pof.resultValue;
                //    if (!string.IsNullOrEmpty(drValue))
                //    {
                //        ucGridView1.SetRowCellValue(ucGridView1.FocusedRowHandle, ucGridView1.FocusedColumn,
                //                drValue);

                //    }
                //}
                //else if (typeId == "31" && ucGridView1.FocusedColumn.FieldName == "COL12")
                //{
                //    string patName = dsPatient.Tables[0].Select(" PATIENT_ID ='" + patientId + "' AND VISIT_ID='" + visitId + "'")[0]["NAME"].ToString();
                //    PatientOrdermFrm pof = new PatientOrdermFrm(patientId, visitId, patName);
                //    pof.ShowDialog();
                //    string drValue = pof.resultValue;
                //    if (!string.IsNullOrEmpty(drValue))
                //    {
                //        ucGridView1.SetRowCellValue(ucGridView1.FocusedRowHandle, ucGridView1.FocusedColumn,
                //                drValue);

                //    }
                //}
                DataRow[] drs =
                    dsRecordDefine.Tables[0].Select("STORE_COL_NAME=" + SQL.SqlConvert(ucGridView1.FocusedColumn.FieldName));

                if (drs.Length == 0) return;

                string dictId = string.Empty;
                if (drs[0]["DICT_ID"] != null)
                    dictId = drs[0]["DICT_ID"].ToString();
                if (string.IsNullOrEmpty(dictId)) return;

                string selectMode = string.Empty;
                bool multiSelect = false;
                string colTitle = string.Empty;

                // 判断选择模式
                DataRow[] drFind =
                    dsRecordDefine.Tables[0].Select("STORE_COL_NAME = " + SQL.SqlConvert(ucGridView1.FocusedColumn.FieldName));
                if (drFind.Length > 0)
                {
                    multiSelect = drFind[0]["MULTI_VALUE"].ToString().Equals("1");
                    selectMode = drFind[0]["SELECTED_VALUE"].ToString();
                    colTitle = drFind[0]["COL_TITLE"].ToString();
                }

                // 模式处理                    
                switch (selectMode)
                {
                    case "0": // 单选
                    case "1": // 多选
                        optionsSelect.DictId = dictId;
                        optionsSelect.MultiSelect = multiSelect;
                        optionsSelect.StoreCode = selectMode.Equals("0");
                        optionsSelect.SelectedItems = string.Empty;

                        if (ucGridView1.GetFocusedRowCellValue(ucGridView1.FocusedColumn) != null)
                        {
                            optionsSelect.SelectedItems = ucGridView1.GetFocusedRowCellValue(ucGridView1.FocusedColumn).ToString();
                        }
                        optionsSelect.frmText = colTitle;
                        if (optionsSelect.ShowDialog() == DialogResult.OK)
                        {
                            ucGridView1.SetRowCellValue(ucGridView1.FocusedRowHandle, ucGridView1.FocusedColumn,
                                optionsSelect.SelectedItems);
                            btnSave.Enabled = true;
                        }
                        break;

                    case "2": // 模板输入
                        templateInput.DictId = dictId;
                        templateInput.MaxLength = dsRecordContent.Tables[0].Columns[ucGridView1.FocusedColumn.FieldName].MaxLength;
                        templateInput.TextEdit = string.Empty;
                        templateInput.ColWidth = ucGridView1.Columns[ucGridView1.FocusedColumn.FieldName].Width;

                        if (ucGridView1.GetFocusedRowCellValue(ucGridView1.FocusedColumn) != null)
                        {
                            if (templateInput.MaxLength >= 0)
                            {
                                templateInput.TextEdit = getRecordContent(ucGridView1.FocusedRowHandle, ucGridView1.FocusedColumn);
                            }

                        }

                        if (templateInput.ShowDialog() == DialogResult.OK)
                        {
                            //dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = templateInput.TextEdit;
                            //acceptTemplateInputText(ucGridView1.FocusedRowHandle, ucGridView1.FocusedColumn, templateInput.Lines);

                            //此版本需修改,上线前暂时提交 2015-11-19
                            ucGridView1.SetRowCellValue(ucGridView1.FocusedRowHandle, ucGridView1.FocusedColumn,
                                templateInput.TextEdit);
                            //acceptDataGridNewRow();
                            btnSave.Enabled = true;
                        }
                        break;
                }


            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }

        }

        void ucGridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            btnSave.Enabled = true;
            if (typeId == "27")
            {

                if (Convert.ToDateTime(ucGridView1.GetSelectCellValue("RECORD_DATE")).ToString("yyyy-MM-dd HH:mm").Contains("07:00") && !ucGridView1.GetSelectCellValue("COL4").ToString().Contains("总结"))
                {
                    ucGridView1.SetRowCellValue(ucGridView1.FocusedRowHandle, "COL4",
                    "总结");
                }
                else if (Convert.ToDateTime(ucGridView1.GetSelectCellValue("RECORD_DATE")).ToString("yyyy-MM-dd HH:mm").Contains("19:00") && !ucGridView1.GetSelectCellValue("COL4").ToString().Contains("小结"))
                {

                    ucGridView1.SetRowCellValue(ucGridView1.FocusedRowHandle, "COL4",
                     "小结");
                }
                else if (
                     !Convert.ToDateTime(ucGridView1.GetSelectCellValue("RECORD_DATE")).ToString("yyyy-MM-dd HH:mm").Contains("07:00")
                    && !Convert.ToDateTime(ucGridView1.GetSelectCellValue("RECORD_DATE")).ToString("yyyy-MM-dd HH:mm").Contains("19:00")
                    && (ucGridView1.GetSelectCellValue("COL4").ToString().Contains("小结") || ucGridView1.GetSelectCellValue("COL4").ToString().Contains("总结")))
                {
                    //"小结,总结".Split(',').Contains(ucGridView1.GetSelectCellValue("COL4").ToString())
                    //&& !string.IsNullOrEmpty(ucGridView1.GetSelectCellValue("COL4").ToString())
                    ucGridView1.SetRowCellValue(ucGridView1.FocusedRowHandle, "COL4",
                     "");
                }



            }
        }

        void IBasePatient.Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
            if (e.PatientId.Equals(patientId))
                return;
            GVars.Patient.ID = e.PatientId;
            GVars.Patient.VisitId = e.VisitId;

            patientId = e.PatientId;
            visitId = e.VisitId;

            btnQuery_Click(null, null);

            lblBedNo.Text = "床号:" + dsPatient.Tables[0].Select(" PATIENT_ID ='" + patientId + "' AND VISIT_ID='" + visitId + "'")[0]["BED_LABEL"].ToString();
            lblName.Text = "姓名:" + dsPatient.Tables[0].Select(" PATIENT_ID ='" + patientId + "' AND VISIT_ID='" + visitId + "'")[0]["NAME"].ToString();
            string admission_date_time = dsPatient.Tables[0].Select(" PATIENT_ID ='" + patientId + "' AND VISIT_ID='" + visitId + "'")[0]["ADMISSION_DATE_TIME"].ToString();
            if (!string.IsNullOrEmpty(admission_date_time))
                lbl_ADMISSION_DATE_TIME.Text = "入院日期:" + Convert.ToDateTime(admission_date_time).ToString("yyyy-MM-dd");
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                base.LoadingShow();

                if (typeId == null)
                {
                    return;
                }
                dsPatient = getPatientInfo(patientId, visitId);

                dsRecordContent = getNursingRecord(typeId, patientId, visitId, dtSearchStart.Value, dtSearchEnd.Value);
                //select * from mobile.nursing_record_define b where b.type_id='28'

                //add
                dsRecordContent = ConvertShowValue(dsRecordContent);
                // UPDATE 2014年6月20日
                //原为: dsRecordContent.Tables[0].DefaultView.Sort = COL_RECORD_DATE;
                dsRecordContent.Tables[0].DefaultView.Sort = COL_RECORD_DATE + "," + COL_ROW_INDEX;
                ucGridView1.DataSource = dsRecordContent.Tables[0].DefaultView;
                btnSave.Enabled = false;



            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                base.LoadingClose();

            }
        }

        /// <summary>
        /// 选择护理评估单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbNursingRecord_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                base.LoadingShow();

                if (cmbNursingRecord.SelectedIndex < 0)
                {
                    return;
                }
                for (int i = 0; i < dsReocrdType.Tables[0].Rows.Count; i++)
                {
                    if (dsReocrdType.Tables[0].Rows[i]["DESCRIPTIONS"].ToString().Trim() == cmbNursingRecord.Text.Trim())
                    {
                        template = cmbNursingRecord.Text.Trim();
                        typeId = dsReocrdType.Tables[0].Rows[i]["TYPE_ID"].ToString().Trim();
                        rowCount = Convert.ToInt32(dsReocrdType.Tables[0].Rows[i]["ROW_COUNT"]);
                        break;
                    }
                }

                dsRecordDefine = getNrusingReocrdDefine(typeId);
                InitDisp_DataGrid();
                vitalCodeList = getVitalCodeList();
                btnQuery_Click(null, null);

                DataTable dt = (ucGridView1.DataSource as DataView).ToTable();
                if (dt != null && dt.Rows.Count > 0)
                {
                    ucGridView1.Columns["CHECK_FLAG"].OptionsColumn.AllowEdit = false;
                    ucGridView1.Columns["NURSE_SYS_NAME"].OptionsColumn.AllowEdit = false;
                }
               
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                base.LoadingClose();

            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (GVars.Patient.STATE == HISPlus.PAT_INHOS_STATE.OUT)
                {

                    btnSave.Enabled = false;
                }

                if (_isNewLine)
                {
                    if (dsRecordContent != null && dsRecordContent.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsRecordContent.Tables[0].Rows.Count; i++)
                        {

                            dsRecordContent.Tables[0].Rows[i]["CHECK_FLAG"] = ConvertAssessState(dsRecordContent.Tables[0].Rows[i]["CHECK_FLAG"].ToString());
                            if (dsRecordContent.Tables[0].Rows[i]["CHECK_FLAG"].ToString() == "1")
                            {
                                dsRecordContent.Tables[0].Rows[i]["CHECK_FLAG"] = "3";
                            }
                        }
                    }
                }
                else
                {
                    if (dsRecordContent != null && dsRecordContent.Tables[0].Rows.Count > 0 && !_isDel)
                    {

                        for (int i = 0; i < dsRecordContent.Tables[0].Rows.Count; i++)
                        {
                            if (ConvertAssessState(dsRecordContent.Tables[0].Rows[i]["CHECK_FLAG"].ToString()) == "1")
                            {
                                //第一次审核未通过时，再次保存，则为待审核状态
                                dsRecordContent.Tables[0].Rows[i]["CHECK_FLAG"] = "3";
                            }
                        }
                    }
                }

                //验证时间是否重复(护理工作交班报告(科室)不做验证)
                if (dsRecordContent != null && dsRecordContent.Tables[0].Rows.Count > 0 && !_isDel && typeId != "28")
                {
                    List<string> sDate = new List<string>();
                    foreach (DataRow dr in dsRecordContent.Tables[0].Rows)
                    {
                        sDate.Add(Convert.ToDateTime(dr["RECORD_DATE"]).ToString("yyyy-MM-dd HH:mm"));
                    }
                    string[] arrDate = sDate.ToArray();
                    string[] newArrDate = arrDate.GroupBy(p => p).Select(p => p.Key).ToArray();
                    if (arrDate.Length != newArrDate.Length)
                    {
                        MessageBox.Show("日期重复,请修改后再保存！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string admission_date_time = dsPatient.Tables[0].Select(" PATIENT_ID ='" + patientId + "' AND VISIT_ID='" + visitId + "'")[0]["ADMISSION_DATE_TIME"].ToString();


                    for (int i = 0; i < newArrDate.Length; i++)
                    {
                        if (Convert.ToDateTime(newArrDate[i]) < Convert.ToDateTime(admission_date_time))
                        {
                            MessageBox.Show("存在小于入院日期的记录：【" + Convert.ToDateTime(newArrDate[i]).ToString("yyyy-MM-dd HH:mm") + "】，请修改后再保存！",
                                "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                }

                dbAccess.SaveTableData(dsRecordContent);

                if (deleteRecordList.Count > 0)
                {
                    dsPageInfo = getPageInfoFromType();
                    DataRow[] drPageInfo = dsPageInfo.Tables[0].Select();
                    foreach (PrimaryKeyItem item in deleteRecordList)
                    {
                        foreach (DataRow dr in drPageInfo)
                        {
                            if (item.patient_id.Trim() == dr["PATIENT_ID"].ToString().Trim()
                                && item.visit_id.Trim() == dr["VISIT_ID"].ToString().Trim()
                                && item.type_id.Trim() == dr["TYPE_ID"].ToString().Trim()
                                && item.record_date.Trim() == dr["RECORD_DATE"].ToString().Trim())
                            {
                                dr.Delete();
                            }
                        }
                    }
                    dbAccess.SaveTableData(dsPageInfo);
                    deleteRecordList.Clear();
                }
                btnSave.Enabled = false;
                btnQuery_Click(null, null);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void btnRePrint_Click(object sender, EventArgs e)
        {
            try
            {
                //string text = "出 入 量 记 录 单";
                //DataGridViewPrinter printer = new DataGridViewPrinter(this.dgvData)
                //{
                //    Margin = new Margins(50, 50, 50, 50),
                //    //                    PVisit = this._pvisit
                //};
                //printer.PageHeaders.Add(new DataGridViewPrinter.Header(text, new Font("宋体", 14f, FontStyle.Bold), 30));
                //printer.Print(null);

                //return;

                dsPageInfo = getPageInfoFromPatient(typeId);
                PrintModeSelectFrm reprintModeFrm = new PrintModeSelectFrm();


                reprintModeFrm.DsPageInfo = dsPageInfo;
                if (reprintModeFrm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                if (reprintModeFrm.PrintFirst)
                {
                    LogFile.WriteLog("首次打印!");
                    if (!printFirst())
                    {
                        GVars.Msg.Show();
                        return;
                    }

                }
                else if (reprintModeFrm.PrintContinue)
                {
                    LogFile.WriteLog("续打!");
                    if (!printContinue())
                    {
                        GVars.Msg.Show();
                        return;
                    }
                }
                else if (reprintModeFrm.PrintPageNo)
                {
                    LogFile.WriteLog("指定页码打印!");
                    if (!printPageNo(reprintModeFrm.PageNo))
                    {
                        GVars.Msg.Show();
                        return;
                    }
                }
                else if (reprintModeFrm.PrintDate)
                {
                    LogFile.WriteLog("指定时间打印!");
                    if (!PrintData(reprintModeFrm.startDate, reprintModeFrm.endDate))
                    {
                        GVars.Msg.Show();
                        return;
                    }

                }
                else if (reprintModeFrm.Reprint)
                {
                    dsPageInfo = getPageInfoFromPatient(typeId);
                    LogFile.WriteLog("重打!");
                    printContinue(false);//重打时因为不能打印新增的记录所以先续打一下（不打印出来），该处传的参数无实际意义lzh改
                    if (!reprint(reprintModeFrm.ReprintPageNo))
                    {
                        GVars.Msg.Show();
                        return;
                    }
                }
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
                //if (dgvData.CurrentRow == null)
                //{
                //    return;
                //}

                // 查找对应的数据
                int rowIndex = -1;
                string filter = getFilterFromDgvRow(ucGridView1.FocusedRowHandle, ref rowIndex);

                DataRow[] drFind = dsRecordContent.Tables[0].Select(filter, COL_ROW_INDEX);
                if (drFind.Length == 0) return;

                // 修改行后记录的ROW_INDEX
                for (int i = drFind.Length - 1; i >= 0; i--)
                {
                    if (int.Parse(drFind[i][COL_ROW_INDEX].ToString()) <= rowIndex) break;
                    drFind[i][COL_ROW_INDEX] = int.Parse(drFind[i][COL_ROW_INDEX].ToString()) + 1;
                }

                // 在该行插入新记录
                addNewLineInRec(ucGridView1.FocusedRowHandle, rowIndex + 1, string.Empty, string.Empty);

                // 设置按钮状态
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        /// 在一条记录中增加新行
        /// </summary>
        /// <returns></returns>
        private bool addNewLineInRec(int dgvRowBased, int rowIndexInRec, string colName, string newValue)
        {
            // 插入新记录
            DataColumn dc = null;
            DataRow drNew = dsRecordContent.Tables[0].NewRow();

            // 插入主键
            for (int i = 0; i < dsRecordContent.Tables[0].PrimaryKey.Length; i++)
            {
                dc = dsRecordContent.Tables[0].PrimaryKey[i];

                drNew[dc.ColumnName] = ucGridView1.GetRowCellValue(dgvRowBased, dc.ColumnName);
            }

            if (colName.Length > 0)
            {
                drNew[colName] = newValue;
            }

            // 行序号
            drNew[COL_ROW_INDEX] = rowIndexInRec;

            // 记录人
            if (colSign != null && colSign.Length > 0)
            {
                drNew[colSign] = GVars.User.Name;
            }

            // 保存记录
            dsRecordContent.Tables[0].Rows.Add(drNew);

            return true;
        }

        private void mnuVitalSigns_AddRec_Click(object sender, EventArgs e)
        {
            try
            {
                //出院病人不能插入新纪录
                //if (GVars.Patient.STATE == HISPlus.PAT_INHOS_STATE.OUT)
                //{
                //    btnSave.Enabled = false;
                //    MessageBox.Show("出院病人不能操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                _isNewLine = true;
                _isDel = false;
                DataRow drNew = dsRecordContent.Tables[0].NewRow();
                drNew["PATIENT_ID"] = patientId;
                drNew["VISIT_ID"] = visitId;
                drNew["TYPE_ID"] = typeId;
                drNew["RECORD_DATE"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                drNew["CHECK_FLAG"] = "未审核";
                drNew["ROW_INDEX"] = 1;

                if (colSign != null && colSign.Length > 0)
                {
                    drNew[colSign] = GVars.User.Name;
                }
                dsRecordContent.Tables[0].Rows.Add(drNew);
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void dgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void mnuVitalSigns_Import_Click(object sender, EventArgs e)
        {
            try
            {
                if (vitalCodeList.Count == 0)
                {
                    return;
                }

                DateTime dtStart = getLastNursingRecordDateTime();  // 获取时间开始点

                bool imported = addNewNursingRecord_VitalSigns(dtStart);
                if (addNewNursingRecord_OrdersExecute(dtStart) == true) imported = true;

                if (imported)
                {
                    ucGridView1.DataSource = dsRecordContent.Tables[0].DefaultView;

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
        /// 删除空白行
        /// </summary>
        private void deleteEmptyRow()
        {
            bool isEmpty = false;

            for (int i = ucGridView1.RowCount - 1; i >= 0; i--)
            {

                DataRowView dr = dsRecordContent.Tables[0].DefaultView[ucGridView1.GetDataSourceRowIndex(i)];
                if (dr.IsNew) continue;

                isEmpty = true;
                foreach (GridColumn dgvCol in ucGridView1.VisibleColumns)
                {
                    if (ucGridView1.GetRowCellValue(i, dgvCol) != null
                     && ucGridView1.GetRowCellValue(i, dgvCol).ToString().Length > 0
                     && dgvCol.ReadOnly == false
                     && dgvCol.FieldName.Equals("RECORD_DATE") == false)
                    {
                        isEmpty = false;
                        break;
                    }
                }

                if (isEmpty)
                {
                    ucGridView1.DeleteRow(i);
                }
            }
        }

        /// <summary>
        /// 获取当前行的过滤条件, 并获取当前行的位置
        /// </summary>
        /// <param name="dgvRowIndex"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private string getFilterFromDgvRow(int dgvRowIndex, ref int rowIndexInRec)
        {
            rowIndexInRec = -1;

            DataColumn dc = null;
            string filter = string.Empty;

            for (int i = 0; i < dsRecordContent.Tables[0].PrimaryKey.Length; i++)
            {
                dc = dsRecordContent.Tables[0].PrimaryKey[i];
                if (dc.ColumnName.ToUpper().Equals(COL_ROW_INDEX) == true)
                {
                    rowIndexInRec = int.Parse(ucGridView1.GetRowCellValue(dgvRowIndex, COL_ROW_INDEX).ToString());
                    continue;
                }

                if (filter.Length > 0) filter += " AND ";
                if (dc.DataType == typeof(DateTime))
                {
                    DateTime dtTemp = (DateTime)(ucGridView1.GetRowCellValue(dgvRowIndex, dc.ColumnName));

                    filter += "(" + dc.ColumnName + " >= " + SQL.SqlConvert(dtTemp.ToString(ComConst.FMT_DATE.LONG));
                    filter += "AND " + dc.ColumnName + " < " + SQL.SqlConvert(dtTemp.AddSeconds(1).ToString(ComConst.FMT_DATE.LONG));
                    filter += ") "; ;
                }
                else
                {
                    filter += dc.ColumnName + " = ";
                    filter += SQL.SqlConvert(ucGridView1.GetRowCellValue(dgvRowIndex, dc.ColumnName).ToString());
                }
            }

            return filter;
        }

        /// <summary>
        /// 菜单[删除]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuVitalSigns_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                //出院病人不能插入新纪录
                //if (GVars.Patient.STATE == HISPlus.PAT_INHOS_STATE.OUT)
                //{
                //    btnSave.Enabled = false;
                //    MessageBox.Show("出院病人不能操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                _isNewLine = false;
                _isDel = true;
                // 条件检查
                if (ucGridView1.SelectRowsCount == 0)
                {
                    return;
                }

                //if (dgvData.CurrentRow.ReadOnly == true)
                //{
                //    GVars.Msg.Show("I0013", "删除");                    // 您没有{0}权限!
                //    return;
                //}

                // 确认是否要删除
                if (GVars.Msg.Show("Q0005") != DialogResult.Yes)        // 您确认要删除当前记录吗?
                {
                    return;
                }

                //记录删除的护理记录
                PrimaryKeyItem item = new PrimaryKeyItem();
                item.patient_id = ucGridView1.SelectedRow["PATIENT_ID"].ToString();
                item.visit_id = ucGridView1.SelectedRow["VISIT_ID"].ToString();
                item.type_id = ucGridView1.SelectedRow["TYPE_ID"].ToString();
                item.record_date = ucGridView1.SelectedRow["RECORD_DATE"].ToString();
                deleteRecordList.Add(item);

                ucGridView1.DeleteSelectRow();
                //ucGridView1.SelectRow()
                //删除
                //dgvData.Rows.Remove(dgvData.CurrentRow);

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

                if (ucGridView1.SelectRowsCount == 0) return;

                // 查找选择单元格的范围
                foreach (GridCell cell in ucGridView1.SelectedCells)
                {
                    if (cell.Column.AbsoluteIndex > colLast) colLast = cell.Column.AbsoluteIndex;
                    if (cell.RowHandle > row1) row1 = cell.RowHandle;
                    if (cell.RowHandle < row0 || row0 < 0) row0 = cell.RowHandle;
                }


                if (row0 == row1) return;

                // 查找分组符号应该放在哪一列
                int colGroup = colLast;
                for (int i = colLast + 1; i < ucGridView1.Columns.Count; i++)
                {
                    if (ucGridView1.Columns[i].Visible == false) continue;
                    if (ucGridView1.Columns[i].ReadOnly == true) continue;
                    colGroup = i;
                    break;
                }

                if (colGroup == colLast) return;

                // 设置分组符号
                ucGridView1.SetRowCellValue(row0, ucGridView1.Columns[colGroup], GROUP_BEGIN);

                for (int i = row0 + 1; i < row1; i++)
                {
                    ucGridView1.SetRowCellValue(i, ucGridView1.Columns[colGroup], GROUP_IN);
                }
                ucGridView1.SetRowCellValue(row1, ucGridView1.Columns[colGroup], GROUP_END);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void mnuVitalSigns_Sum_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            int selectCellCount = ucGridView1.SelectedCells.Length;
            double sum = 0;

            foreach (GridCell cell in ucGridView1.SelectedCells)
            {
                try
                {
                    string strValue = "0";
                    if (ucGridView1.GetRowCellValue(cell.RowHandle, cell.Column) == null || ucGridView1.GetRowCellValue(cell.RowHandle, cell.Column).ToString().Length == 0)
                    {
                        strValue = "0";
                    }
                    else
                    {
                        strValue = ucGridView1.GetRowCellValue(cell.RowHandle, cell.Column).ToString();
                    }
                    double cellValue = double.Parse(strValue);
                    sum += cellValue;
                }
                catch
                {
                    GVars.Msg.MsgId = "E";
                    GVars.Msg.MsgContent.Add("你选择的单元格包含了非数字类型,复制求和失败！");
                    GVars.Msg.Show();
                    break;
                }
            }
            Clipboard.SetText(sum.ToString(), TextDataFormat.Text);
        }

        private void mnuVitalSigns_InsertPageBreak_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 获取记录内容, 如果一个记录写在多个行上.
        /// </summary>
        /// <returns></returns>
        private string getRecordContent(int row, GridColumn column)
        {
            // 获取记录开始行
            string dateStr = ucGridView1.GetRowCellValue(row, COL_RECORD_DATE).ToString();

            int rowStart = 0;
            for (int i = row; i >= 0; i--)
            {
                if (dateStr.Equals(ucGridView1.GetRowCellValue(i, COL_RECORD_DATE).ToString()) == false)
                {
                    break;
                }

                rowStart = i;
            }

            // 获取记录内容
            string cellValue = string.Empty;
            for (int i = rowStart; i < ucGridView1.RowCount; i++)
            {
                if (dateStr.Equals(ucGridView1.GetRowCellValue(i, COL_RECORD_DATE).ToString()) == false)
                {
                    break;
                }

                if (cellValue.Length > 0)
                    cellValue += ComConst.STR.CRLF;

                cellValue += ucGridView1.GetRowCellValue(i, column).ToString();
            }

            return cellValue;
        }

        /// <summary>
        /// 接受模板输入
        /// </summary>
        /// <returns></returns>
        private bool acceptTemplateInputText(int cellRowIndex, GridColumn column, string[] lines)
        {
            // 查找主键
            int rowInRec = 0;
            string filter = getFilterFromDgvRow(cellRowIndex, ref rowInRec);

            // 查找记录
            DataRow[] drFind = dsRecordContent.Tables[0].Select(filter, COL_ROW_INDEX);

            // 保存结果
            string colName = column.FieldName;
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
                foreach (GridColumn dgvCol in ucGridView1.VisibleColumns)
                {
                    if (dgvCol.Visible == false || dgvCol.Name.Equals(COL_RECORD_DATE) == true || dgvCol.Name.Equals(colSign) == true) continue;
                    if (dgvCol == column) continue;

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

        /// <summary>
        /// 接受新行数据
        /// </summary>
        private void acceptDataGridNewRow()
        {
            //int row =ucGridView1.FocusedRowHandle;
            //int col = dgvData.CurrentCell.ColumnIndex;

            //if (dgvData.CurrentRow.IsNewRow == true)
            //{
            //    DataRow drNew = dsRecordContent.Tables[0].NewRow();
            //    foreach (GridColumn dgvCol in ucGridView1.Columns)
            //    {
            //        if (dgvCol.FieldName != null && dgvCol.FieldName.Length > 0)
            //        {
            //            drNew[dgvCol.FieldName] =ucGridView1.GetRowCellValue(row,dgvCol.FieldName) ;
            //        }
            //    }

            //    dgvData.AllowUserToAddRows = false;

            //    if (dsRecordContent.Tables[0].Rows.Count == 0)
            //    {
            //        dsRecordContent.Tables[0].Rows.Add(drNew);
            //    }

            //    try
            //    {
            //        dgvData.AllowUserToAddRows = true;
            //    }
            //    catch
            //    {
            //        dgvData.Rows[dgvData.Rows.Count - 1].Cells[COL_RECORD_DATE].Value = DateTime.Now.ToString(ComConst.FMT_DATE.LONG);
            //    }

            //    // 重定位当前行
            //    dgvData.CurrentCell = dgvData.Rows[row].Cells[col];
            //}
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void auditPass_Click(object sender, EventArgs e)
        {
            //DataRow dr = ucGridView1.SelectedRow;


            AuditAssess("2");

        }

        /// <summary>
        /// 审核未通过
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void auditNoPass_Click(object sender, EventArgs e)
        {
            AuditAssess("1");
        }

        /// <summary>
        /// 审核 1：未通过 2：通过
        /// </summary>
        /// <param name="_flag">1：未通过 2：通过</param>
        private void AuditAssess(string _flag)
        {
            ArrayList alst = new ArrayList();
            foreach (int rowHandle in ucGridView1.SelectedRows)
            {
                string strPatient_id = ucGridView1.GetRowCellValue(rowHandle, "PATIENT_ID").ToString(); // dr["PATIENT_ID"].ToString();
                string strVisit_id = ucGridView1.GetRowCellValue(rowHandle, "VISIT_ID").ToString(); //dr["VISIT_ID"].ToString();
                string strType_id = ucGridView1.GetRowCellValue(rowHandle, "TYPE_ID").ToString(); //dr["TYPE_ID"].ToString();
                string strRecord_date = ucGridView1.GetRowCellValue(rowHandle, "RECORD_DATE").ToString();// dr["RECORD_DATE"].ToString();
                string strRow_index = ucGridView1.GetRowCellValue(rowHandle, "ROW_INDEX").ToString(); //dr["ROW_INDEX"].ToString();
                string updSql = "UPDATE NURSING_RECORD_CONTENT  set CHECK_FLAG='" + _flag + "' WHERE PATIENT_ID='" + strPatient_id + "' and  VISIT_ID='" + strVisit_id + "' and TYPE_ID='" + strType_id + "' and RECORD_DATE=to_date('" + strRecord_date + "','yyyy-MM-dd HH24:mi:ss') and ROW_INDEX='" + strRow_index + "'";

                alst.Add(updSql);

                //SelectedItems += (StoreCode ? ucGridView1.GetRowCellValue(rowHandle, "ITEM_ID").ToString() : ucGridView1.GetRowCellValue(rowHandle, "ITEM_NAME"));
            }

            GVars.OracleAccess.ExecuteNoQuery(ref alst);

            //刷新数据
            dsRecordContent = getNursingRecord(typeId, patientId, visitId, dtSearchStart.Value, dtSearchEnd.Value);
            dsRecordContent.Tables[0].DefaultView.Sort = COL_RECORD_DATE + "," + COL_ROW_INDEX;
            ucGridView1.DataSource = dsRecordContent.Tables[0].DefaultView;
            btnSave.Enabled = false;
        }


        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMnu_VitalSigns_Opening(object sender, CancelEventArgs e)
        {
            if (typeId == "28")
            {
                contextMnu_VitalSigns.Items["auditPass"].Visible = false;
                contextMnu_VitalSigns.Items["auditNoPass"].Visible = false;
                contextMnu_VitalSigns.Items["toolStripMenuItem2"].Visible = false;
            }
            else
            {
                contextMnu_VitalSigns.Items["auditPass"].Visible = true;
                contextMnu_VitalSigns.Items["auditNoPass"].Visible = true;
                contextMnu_VitalSigns.Items["toolStripMenuItem2"].Visible = true;
            }

            DataTable dtSource = (ucGridView1.DataSource as DataView).ToTable();
            if (dtSource == null || dtSource.Rows.Count <= 0)
            {

                contextMnu_VitalSigns.Items["mnuVitalSigns_Delete"].Enabled = false;
                contextMnu_VitalSigns.Items["mnuVitalSigns_AddLine"].Enabled = false;//
                contextMnu_VitalSigns.Items["auditPass"].Enabled = false;
                contextMnu_VitalSigns.Items["mnuVitalSigns_Group"].Enabled = false;
                contextMnu_VitalSigns.Items["mnuVitalSigns_Sum"].Enabled = false;
                contextMnu_VitalSigns.Items["auditNoPass"].Enabled = false;
                contextMnu_VitalSigns.Items["insNursingTransfer"].Enabled = false;
                return;
            }
            else
            {
                contextMnu_VitalSigns.Items["mnuVitalSigns_Delete"].Enabled = true;
                contextMnu_VitalSigns.Items["mnuVitalSigns_AddLine"].Enabled = true;//
                contextMnu_VitalSigns.Items["auditPass"].Enabled = true;
                contextMnu_VitalSigns.Items["mnuVitalSigns_Group"].Enabled = true;
                contextMnu_VitalSigns.Items["mnuVitalSigns_Sum"].Enabled = true;
                contextMnu_VitalSigns.Items["auditNoPass"].Enabled = true;
                contextMnu_VitalSigns.Items["insNursingTransfer"].Enabled = true;
            }

            //获取APP_CONFIG中科室审核权限的集合
            string strValue = string.Empty;
            DataSet dsCheckRole = GVars.OracleAccess.SelectData("SELECT PARAMETER_VALUE FROM MOBILE.APP_CONFIG  WHERE PARAMETER_NAME='CHECK_ROLE'");
            if (dsCheckRole != null && dsCheckRole.Tables[0].Rows.Count > 0)
            {
                //获取到的字符串
                strValue = dsCheckRole.Tables[0].Rows[0][0].ToString();
                string[] arrRoleValue = strValue.Split('|');
                if (arrRoleValue.Length > 0)
                {
                    for (int i = 0; i < arrRoleValue.Length; i++)
                    {
                        //找到当前登录的科室对应的护士权限编号
                        if (arrRoleValue[i].Split('-')[0].ToString() == GVars.User.DeptCode)
                        {

                            if (GVars.User.Name == ucGridView1.SelectedRow["NURSE_SYS_NAME"].ToString() && !arrRoleValue[i].Contains(GVars.User.UserName))
                            {
                                //当前记录是登陆者所写
                                contextMnu_VitalSigns.Items[5].Visible = true;
                                contextMnu_VitalSigns.Items[9].Visible = false;
                                contextMnu_VitalSigns.Items[10].Visible = false;
                                contextMnu_VitalSigns.Items[11].Visible = false;
                                break;
                            }

                            //当前登录者,不在权限列表中，则隐藏审核菜单
                            if (!arrRoleValue[i].Contains(GVars.User.UserName))
                            {
                                contextMnu_VitalSigns.Items[5].Visible = false;
                                contextMnu_VitalSigns.Items[9].Visible = false;
                                contextMnu_VitalSigns.Items[10].Visible = false;
                                contextMnu_VitalSigns.Items[11].Visible = false;
                                break;
                            }

                        }


                    }

                }
            }
        }

        /// <summary>
        /// 自定义护理记录单模板列值转换
        /// LPD
        /// 2015-11-20 
        /// Add
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private DataSet ConvertShowValue(DataSet dsRecord_content)
        {
            //获取NURSING_RECORD_DEFINE表，某个模板中(typeId)，列是"取模板(2)"的列集合,返回DataSet
            string getDefineValue = @"SELECT * FROM MOBILE.NURSING_RECORD_DEFINE B WHERE B.TYPE_ID='" + typeId + "' AND B.SELECTED_VALUE ='2' ";
            DataSet dsDefine = GVars.OracleAccess.SelectData(getDefineValue);

            //记录取到的值
            string defineValue = string.Empty;
            if (dsDefine != null && dsDefine.Tables[0].Rows.Count > 0)
            {
                DataTable dtDefine = dsDefine.Tables[0];
                for (int i = 0; i < dtDefine.Rows.Count; i++)
                {
                    defineValue += "COL" + Convert.ToInt32(dtDefine.Rows[i]["COL_ID"].ToString()).ToString() + ",";
                }

                defineValue = defineValue.Length > 0 ? defineValue.Substring(0, defineValue.Length - 1) : defineValue;
            }
            string[] arrdefineValue = defineValue.Split(',');

            //记录进入转换的次数，当需循环的列等于_count时Break，去掉没必要的循环
            int _count = 0;

            //遍历原始数据源的行
            for (int n = 0; n < dsRecord_content.Tables[0].Rows.Count; n++)
            {
                //遍历数据源的列，找出需转换的列（如：COL5，COL6，COL7）
                for (int k = 0; k < dsRecord_content.Tables[0].Columns.Count; k++)
                {
                    if (arrdefineValue.Contains(dsRecord_content.Tables[0].Columns[k].ColumnName))
                    {
                        dsRecord_content.Tables[0].Rows[n][k] = ConvertLfToCrlf(dsRecord_content.Tables[0].Rows[n][k].ToString());
                        _count++;
                    }
                    if (arrdefineValue.Length == _count)
                    {
                        //当符合条件的列次数等于_count时break，去掉本条数据没必要的循环，进入下一行转换
                        _count = 0;
                        break;
                    }
                }
            }



            return dsRecord_content;
        }

        /// <summary>
        /// 功能：换行转回车+换行
        /// 创建：2015.11.19
        /// </summary>
        /// <param name="sSour"></param>
        /// <returns>转化后的字符串</returns>
        private string ConvertLfToCrlf(string sSour)
        {
            string sDesc = string.Empty;

            if (!string.IsNullOrEmpty(sSour))
            {

                if (sSour[0] == '\n')
                    sDesc += '\r';
                sDesc += sSour[0];
                for (int i = 1; i < sSour.Length; i++)
                {
                    if (sSour[i] == '\n')
                    {
                        if (sSour[i - 1] != '\r')
                            sDesc += "\r";
                    }
                    sDesc += sSour[i];
                }
                return sDesc;
            }
            else
            {
                return sDesc;
            }
        }

        /// <summary>
        /// 转换状态
        /// </summary>
        /// <param name="strAssess"></param>
        /// <returns></returns>
        private string ConvertAssessState(string strAssess)
        {
            string assessValue = string.Empty;
            switch (strAssess)
            {
                case "未审核":
                    assessValue = "0";
                    break;
                case "审核未通过":
                    assessValue = "1";
                    break;
                case "审核通过":
                    assessValue = "2";
                    break;
                case "待审核":
                    assessValue = "3";
                    break;
                default:
                    assessValue = "0";
                    break;
            }
            return assessValue;
        }

        /// <summary>
        /// 插入交班报告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void insNursingTransfer_Click(object sender, EventArgs e)
        {
            //获取NURSING_RECORD_DEFINE表，某个模板中(typeId)，列是"取模板(2)"的列集合,返回DataSet
            string getDefineValue = @"SELECT * FROM MOBILE.NURSING_RECORD_DEFINE B WHERE B.TYPE_ID='" + typeId + "' AND B.SELECTED_VALUE ='2' ";
            DataSet dsDefine = GVars.OracleAccess.SelectData(getDefineValue);

            //记录取到的值
            string defineValue = string.Empty;
            if (dsDefine != null && dsDefine.Tables[0].Rows.Count > 0)
            {
                DataTable dtDefine = dsDefine.Tables[0];
                for (int i = 0; i < dtDefine.Rows.Count; i++)
                {
                    defineValue += "COL" + Convert.ToInt32(dtDefine.Rows[i]["COL_ID"].ToString()).ToString() + ",";
                }

                defineValue = defineValue.Length > 0 ? defineValue.Substring(0, defineValue.Length - 1) : defineValue;
            }
            string[] arrdefineValue = defineValue.Split(',');


            //遍历选中的数据
            foreach (int rowHandle in ucGridView1.SelectedRows)
            {
                //获取选中的具体值
                string rGuid = System.Guid.NewGuid().ToString("N").ToUpper();   //GUID
                string strPatient_id = ucGridView1.GetRowCellValue(rowHandle, "PATIENT_ID").ToString(); // dr["PATIENT_ID"].ToString();       
                string strVisit_id = ucGridView1.GetRowCellValue(rowHandle, "VISIT_ID").ToString(); //dr["VISIT_ID"].ToString();
                string ward_code = GVars.User.DeptCode;   //科室代码
                string bedNo = dsPatient.Tables[0].Select(" PATIENT_ID ='" + patientId + "' AND VISIT_ID='" + visitId + "'")[0]["BED_LABEL"].ToString();
                string name = dsPatient.Tables[0].Select(" PATIENT_ID ='" + patientId + "' AND VISIT_ID='" + visitId + "'")[0]["NAME"].ToString();
                string diagnosisName = dsPatient.Tables[0].Select(" PATIENT_ID ='" + patientId + "' AND VISIT_ID='" + visitId + "'")[0]["DIAGNOSIS"].ToString();
                string dynamic_situation = string.Empty;
                string strNurseClass = GetPatientNurseClass(strPatient_id, strVisit_id);


                string transfer_content = string.Empty;//defineValue

                //住院患者护理实施记录单
                if (typeId == "26")
                {
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL2").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL3").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL4").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL5").ToString() + " ";

                }

                //赤峰市医院神经内科病重(病危)患者护理记录
                if (typeId == "22")
                {
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL2").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL3").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL4").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL5").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL6").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL7").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL8").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL9").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL10").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL11").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL12").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL13").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL14").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL15").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL16").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL17").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL18").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL19").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL20").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL21").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL22").ToString() + " ";

                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL23").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL24").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL25").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL26").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL27").ToString() + " ";
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, "COL28").ToString() + " ";
                }
                transfer_content = transfer_content.Replace(" ", "").Length == 0 ? "" : transfer_content;
                if (arrdefineValue.Length == 1)
                    transfer_content += ucGridView1.GetRowCellValue(rowHandle, defineValue).ToString();

                string receiveer = "";
                string transferer = ucGridView1.GetRowCellValue(rowHandle, "NURSE_SYS_NAME").ToString();//NURSE_SYS_NAME
                string executeTime = DateTime.Now.ToString();//ucGridView1.GetRowCellValue(rowHandle, "RECORD_DATE").ToString();
                string insSql = @"INSERT INTO NURSING_TRANSFER_RECORD  " +
                               "  (RGUID,WARD_CODE, BED_NO, NAME, DIAGNOSIS_NAME, DYNAMIC_SITUATION,  " +
                               "   TRANSFER_CONTENT, TRANSFERER, RECEIVEER, EXECUTE_TIME,IS_ABOLISH,PATIENT_ID,VISIT_ID,NURSE_CLASS)  " +
                               "  VALUES  " +
                               "  ('" + rGuid + "','" + ward_code + "', '" + bedNo + "', '" + name + "', '" + diagnosisName + "', '" + dynamic_situation + "',  " +
                               "   '" + transfer_content + "', '" + transferer + "', '" + receiveer + "', TO_DATE('" + executeTime + "','YYYY-MM-DD HH24:MI:SS'),'F','" + strPatient_id + "','" + strVisit_id + "','" + strNurseClass + "')";

                GVars.OracleAccess.ExecuteNoQuery(insSql);
            }

            MessageBox.Show("插入成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        void IBasePatient.Patient_ListRefresh(object sender, PatientEventArgs e)
        {

        }

        /// <summary>
        /// 获取用户护理级别
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="visidID"></param>
        /// <returns></returns>
        public string GetPatientNurseClass(string patientID, string visidID)
        {
            string strResultValue = string.Empty;
            string strSql = "SELECT A.NURSING_CLASS_NAME FROM PATIENT_INFO A WHERE A.WARD_CODE='" + GVars.User.DeptCode + "' AND A.PATIENT_ID='" + patientID + "' AND A.VISIT_ID='" + visidID + "'";
            DataSet dsNurseClass = GVars.OracleAccess.SelectData(strSql);
            if (dsNurseClass != null && dsNurseClass.Tables[0].Rows.Count > 0)
            {
                strResultValue = dsNurseClass.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                string strSqlTombstone = "SELECT NURSING_CLASS_NAME FROM PATIENT_INFO_TOMBSTONE WHERE WARD_CODE='" + GVars.User.DeptCode + "' AND  PATIENT_ID = '" + patientID + "' AND VISIT_ID = '" + visidID + "'  ";
                DataSet dsNurseClassTombstone = GVars.OracleAccess.SelectData(strSqlTombstone);
                strResultValue = dsNurseClassTombstone.Tables[0].Rows[0][0].ToString();
            }
            return strResultValue;
        }
    }

}
