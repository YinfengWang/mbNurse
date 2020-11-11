using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class PIOSettingFrm : FormDo1
    {
        #region 变量
        private DbInfo          dbDbI               = null;             // 字典接口
        private DataSet         dsItemRelation      = null;             // 关系
        private DataSet         dsDictItem          = null;             // 护理诊断记录
        private DataSet         dsDictItemContent   = null;             // 字典项目内容
        
        private DataSet         dsDiagnosis         = null;             // 护理诊断
        private DataSet         dsDiagnosis2        = null;             // 护理诊断
        private DataView        dvDiagnosis         = null;
        private DataSet         dsObjective         = null;             // 护理目标
        private DataSet         dsObjective2        = null;             // 护理目标
        private DataSet         dsMeasure           = null;             // 护理措施
        private DataSet         dsMeasureType       = null;             // 护理措施类型
        
        private DataRow         drMeasure           = null;
        
        private string          nursingDiagnosis    = string.Empty;
        private string          nursingObjective    = string.Empty;
        private string          nursingMeasureType  = string.Empty;
        private string          nursingMeasure      = string.Empty;
        
        public string           NursingDiagnosis    = string.Empty;
        public string           NursingObjective    = string.Empty;
        public ArrayList        NursingMeasureType  = new ArrayList();
        public ArrayList        NursingMeasure      = new ArrayList();
        public ArrayList        NursingMeasureContent = new ArrayList();
        
        public bool             SelectionMode       = false;            // 是否是选择模式
        #endregion
        
        
        public PIOSettingFrm()
        {
            InitializeComponent();
            
            _id     = "00056";
            _guid   = "11D53EF9-6CAB-4c49-A47F-CEA377698C29";
        }
        
        
        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PIOSettingFrm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                GVars.App.UserInput = false;
                
                initFrmVal();                
                initDisp();
                initDisp_SelectionMode();
                
                refreshShow();
            }
            catch(Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
                this.Cursor = Cursors.Default;
            }
        }
        
        
        /// <summary>
        /// 护理措施类别选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbMeasureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbMeasureType.SelectedIndex >= 0)
                {
                    string dictId = (22 + cmbMeasureType.SelectedIndex).ToString();
                    dsMeasure.Tables[0].DefaultView.RowFilter = "DICT_ID = " + SQL.SqlConvert(dictId);
                }
                else
                {
                    dsMeasure.Tables[0].DefaultView.RowFilter = "DICT_ID = '-1'";
                }
                
                cmbMeasure.DataSource = dsMeasure.Tables[0].DefaultView;            
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        private void dgvNursingDiagnosis_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
        
        
        private void dgvNursingDiagnosis_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                dgvNursingObjective.Rows.Clear();
                dgvNursingMeasure.DataSource = null;
                
                getCurrentRecordId();
                
                if (nursingDiagnosis.Length > 0)
                {
                    showDgvObjective(nursingDiagnosis);
                }
                
                getCurrentRecordId();
                if (nursingDiagnosis.Length > 0 && nursingObjective.Length > 0)
                {
                    showDgvMeasure(nursingDiagnosis, nursingObjective);
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        
        private void dgvNursingObjective_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                dgvNursingMeasure.DataSource = null;
                
                getCurrentRecordId();
                
                if (nursingDiagnosis.Length > 0 && nursingObjective.Length > 0)
                {
                    showDgvMeasure(nursingDiagnosis, nursingObjective);
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void dgvNursingMeasure_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach(DataGridViewRow dgvRow in dgvNursingMeasure.Rows)
            {
                string measureTypeId = dgvRow.Cells["MEASURE_DICT_ID"].Value.ToString();
                string measureId   = dgvRow.Cells["MEASURE_ID"].Value.ToString();
                
                string measureType = string.Empty;
                switch(measureTypeId)
                {
                    case "22":
                        measureType = "观察";
                        break;
                    case "23":
                        measureType = "治疗";
                        break;
                    case "24":
                        measureType = "教育";
                        break;
                    default:
                        measureType = string.Empty;
                        break;
                }
                
                string filter = "DICT_ID = " + SQL.SqlConvert(measureTypeId)
                        + "AND ITEM_ID = " + SQL.SqlConvert(measureId);
                DataRow[] drFind = dsDictItem.Tables[0].Select(filter);
                if (drFind.Length > 0)
                {
                    dgvRow.Cells["NURSING_MEASURE_TYPE"].Value = measureType;
                    dgvRow.Cells["NURSING_MEASURE"].Value = drFind[0]["ITEM_NAME"].ToString();
                }
            }
        }


        private void dgvNursingMeasure_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                getCurrentRecordId();
                
                // 查找措施内容
                showMeasureContent(nursingMeasureType, nursingMeasure);
                
                btnDelete.Enabled = (nursingDiagnosis.Length > 0 && nursingObjective.Length > 0 
                                        && nursingMeasureType.Length > 0 && nursingMeasure.Length > 0);
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }        
        
        
        private void dgvNursingObjective_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        
        private void dgvNursingMeasure_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
        
        
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dsItemRelation.Tables[0].DefaultView.RowFilter = getDiagnosiFilter(txtFilter.Text);
                refreshShow();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }            
        }
        
        
        private void txtDiagnosis_TextChanged(object sender, EventArgs e)
        {
            dsDiagnosis.Tables[0].DefaultView.RowFilter = "DICT_ID = '20' AND DESC_SPELL LIKE '" + txtDiagnosis.Text.Trim() +  "%'";
            cmbDiagnosis.DataSource = dsDiagnosis.Tables[0].DefaultView;
            cmbDiagnosis.DroppedDown = true;            
        }
        
        
        private void txtOjective_TextChanged(object sender, EventArgs e)
        {
            dsObjective.Tables[0].DefaultView.RowFilter = "DICT_ID = '21' AND DESC_SPELL LIKE '" + txtOjective.Text.Trim() +  "%'";
            cmbOjective.DataSource = dsObjective.Tables[0].DefaultView;
            cmbOjective.DroppedDown = true;
        }
        
        
        private void txtMeasureSearch_TextChanged(object sender, EventArgs e)
        {
            string filter = string.Empty;
            if (cmbMeasureType.SelectedIndex >= 0)
            {
                string dictId = (22 + cmbMeasureType.SelectedIndex).ToString();
                filter = "DICT_ID = " + SQL.SqlConvert(dictId);
            }
            else
            {
                filter = "DICT_ID = '-1'";
            }
            
            dsMeasure.Tables[0].DefaultView.RowFilter = filter + " AND DESC_SPELL LIKE '" + txtMeasureSearch.Text.Trim() +  "%'";
            cmbMeasure.DataSource = dsMeasure.Tables[0].DefaultView;
            cmbMeasure.DroppedDown = true;
        }
        
        
        private void txtDiagnosis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                cmbDiagnosis.Focus();
            }
        }

        private void txtOjective_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                cmbOjective.Focus();
            }
        }

        private void txtMeasureSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                cmbMeasure.Focus();
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
                // 条件检查
                if (cmbDiagnosis.SelectedIndex < 0)
                {
                    cmbDiagnosis.Focus();
                    return;
                }
                
                if (cmbOjective.SelectedIndex < 0)
                {
                    cmbOjective.Focus();
                    return;
                }
                
                if (cmbMeasureType.SelectedIndex < 0)
                {
                    cmbMeasureType.Focus();
                    return;
                }
                
                if (cmbMeasure.SelectedIndex < 0)
                {
                    return;
                }
                
                // 检查记录是否已经存在
                string filter = "DIAGNOSIS_ID = " + SQL.SqlConvert(cmbDiagnosis.SelectedValue.ToString())
                                + "AND OBJECTIVE_ID = " + SQL.SqlConvert(cmbOjective.SelectedValue.ToString())
                                + "AND MEASURE_DICT_ID = " + SQL.SqlConvert((cmbMeasureType.SelectedIndex + 22).ToString())
                                + "AND MEASURE_ID = " + SQL.SqlConvert(cmbMeasure.SelectedValue.ToString());
                DataRow[] drFind = dsItemRelation.Tables[0].Select(filter);
                if (drFind.Length > 0)
                {
                    return;
                }
                
                DataRow drNew = dsItemRelation.Tables[0].NewRow();
                
                drNew["DIAGNOSIS_ID"] = cmbDiagnosis.SelectedValue.ToString();
                drNew["OBJECTIVE_ID"] = cmbOjective.SelectedValue.ToString();
                drNew["MEASURE_DICT_ID"] = (cmbMeasureType.SelectedIndex + 22).ToString();
                drNew["MEASURE_ID"] = cmbMeasure.SelectedValue.ToString();
                
                dsItemRelation.Tables[0].Rows.Add(drNew);
                
                // 刷新显示
                dsItemRelation.Tables[0].DefaultView.RowFilter = string.Empty;
                refreshShow();
                
                // 定位
                dgvLoacate(dgvNursingDiagnosis, "NURSING_DIAG_NAME", drNew["DIAGNOSIS_ID"].ToString());
                dgvLoacate(dgvNursingObjective, "NURSING_OBJECTIVE", drNew["OBJECTIVE_ID"].ToString());
                dgvLoacate(dgvNursingMeasure, "MEASURE_DICT_ID", "MEASURE_ID", drNew["MEASURE_DICT_ID"].ToString(), drNew["MEASURE_ID"].ToString());
                
                showMeasureContent(drNew["MEASURE_DICT_ID"].ToString(), drNew["MEASURE_ID"].ToString());
                
                btnSave.Enabled = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }           
        

        private void txtMeasure_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                if (drMeasure != null)
                {
                    drMeasure["CONTENT"] = txtMeasure.Text.TrimEnd();
                    
                    btnSave.Enabled = true;
                }
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
                if (GVars.Msg.Show("Q0006", "删除", "当前项目") != DialogResult.Yes)        // 您确认要 {0} {1} 吗?
                {
                    return;
                }

                // 删除
                string filter = "DIAGNOSIS_ID = " + SQL.SqlConvert(nursingDiagnosis)
                                + "AND OBJECTIVE_ID = " + SQL.SqlConvert(nursingObjective)
                                + "AND MEASURE_DICT_ID = " + SQL.SqlConvert(nursingMeasureType)
                                + "AND MEASURE_ID = " + SQL.SqlConvert(nursingMeasure);
                DataRow[] drFind = dsItemRelation.Tables[0].Select(filter);
                if (drFind.Length > 0)
                {
                    drFind[0].Delete();
                }
                          
                // 更新显示
                dsItemRelation.Tables[0].DefaultView.RowFilter = string.Empty;
                refreshShow();
                
                btnSave.Enabled = true;
            }
            catch(Exception ex)
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
                if (SelectionMode == false)
                {
                    if (dbDbI.SaveData(null, new object[]{dsItemRelation, dsDictItemContent}) == true)
                    {
                        dsDictItemContent.AcceptChanges();
                        dsItemRelation.AcceptChanges();
                        
                        btnSave.Enabled = false;
                    }
                }
                else
                {
                    getCurrentRecordText();
                    this.DialogResult = DialogResult.OK;
                }
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
            try
            {   
                this.DialogResult = DialogResult.Cancel;
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
            dbDbI = new DbInfo(GVars.OracleAccess);
            
            string filter = "DICT_ID IN ('20', '21', '22', '23', '24')";
            dsDictItem = dbDbI.GetTableData("HIS_DICT_ITEM", filter);
            
            dsDiagnosis = dsDictItem.Copy();
            dsDiagnosis2 = dsDiagnosis.Copy();
            
            dsDiagnosis.Tables[0].DefaultView.RowFilter = "DICT_ID = '20'";
            dsDiagnosis.Tables[0].DefaultView.Sort = "ITEM_ID";
            
            dsDiagnosis2.Tables[0].DefaultView.RowFilter = "DICT_ID = '20'";
            dsDiagnosis2.Tables[0].DefaultView.Sort = "ITEM_ID";
            dvDiagnosis = new DataView(dsDiagnosis.Tables[0]);
            
            dsObjective = dsDictItem.Copy();
            dsObjective2 = dsObjective.Copy();
            
            dsObjective.Tables[0].DefaultView.RowFilter = "DICT_ID = '21'";
            dsObjective.Tables[0].DefaultView.Sort = "ITEM_ID";
            
            dsObjective2.Tables[0].DefaultView.RowFilter = "DICT_ID = '21'";
            dsObjective2.Tables[0].DefaultView.Sort = "ITEM_ID";
            
            dsMeasure   = dsDictItem.Copy();
            
            dsDictItemContent = dbDbI.GetTableData("HIS_DICT_ITEM_CONTENT", filter);
            dsItemRelation  = dbDbI.GetTableData("NURSING_SCHEDULE_ITEM_DICT", "");
        }
        
        
        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {   
            dgvNursingDiagnosis.AutoGenerateColumns    = false;
            dgvNursingObjective.AutoGenerateColumns = false;
            dgvNursingMeasure.AutoGenerateColumns   = false;
            
            NURSING_DIAG_NAME.DisplayMember = "ITEM_NAME";
            NURSING_DIAG_NAME.ValueMember   = "ITEM_ID";
            NURSING_DIAG_NAME.DataSource    = dsDiagnosis2.Tables[0].DefaultView;
            
            NURSING_OBJECTIVE.DisplayMember = "ITEM_NAME";
            NURSING_OBJECTIVE.ValueMember   = "ITEM_ID";
            NURSING_OBJECTIVE.DataSource    = dsObjective2.Tables[0].DefaultView;
            
            // 护理诊断
            cmbDiagnosis.DisplayMember = "ITEM_NAME";
            cmbDiagnosis.ValueMember   = "ITEM_ID";
            cmbDiagnosis.DataSource = dsDiagnosis.Tables[0].DefaultView;
                        
            // 护理目标
            cmbOjective.DisplayMember = "ITEM_NAME";
            cmbOjective.ValueMember   = "ITEM_ID";
            cmbOjective.DataSource = dsObjective.Tables[0].DefaultView;
            
            // 护理措施
            cmbMeasure.DisplayMember = "ITEM_NAME";
            cmbMeasure.ValueMember   = "ITEM_ID";
            dsMeasure.Tables[0].DefaultView.Sort = "ITEM_ID";
            
            if (cmbMeasureType.SelectedIndex >= 0)
            {
                string dictId = (22 + cmbMeasureType.SelectedIndex).ToString();
                dsMeasure.Tables[0].DefaultView.RowFilter = "DICT_ID = " + SQL.SqlConvert(dictId);
            }
            else
            {
                dsMeasure.Tables[0].DefaultView.RowFilter = "DICT_ID = '-1'";
            }
            
            cmbMeasure.DataSource = dsMeasure.Tables[0].DefaultView;
        }


        private void initDisp_SelectionMode()
        {
            if (SelectionMode == true)
            {
                grpAdd.Visible = false;
                
                int offSet = (grpButton.Left - grpAdd.Left);
                this.Width = this.Width - offSet;
                
                groupBox1.Left -= offSet;
                groupBox2.Left -= offSet;
                groupBox3.Left -= offSet;
                groupBox3.Width += offSet;
                groupBox5.Left -= offSet;
                groupBox5.Width += offSet;
                
                grpButton.Left -= offSet;
                grpButton.Width += offSet;
                
                btnSave.TextValue = "选择(&S)";
                btnDelete.Visible = false;
                
                txtMeasure.ReadOnly = false;
                btnSave.Enabled = true;
            }            
        }
        
        
        private void refreshShow()
        {
            dgvNursingDiagnosis.Rows.Clear();
            dgvNursingObjective.Rows.Clear();
            dgvNursingMeasure.DataSource = null;
            
            showDgvDiagnosis();
            getCurrentRecordId();
            
            if (nursingDiagnosis.Length > 0)
            {
                showDgvObjective(nursingDiagnosis);
            }
            
            getCurrentRecordId();
            if (nursingDiagnosis.Length > 0 && nursingObjective.Length > 0)
            {
                showDgvMeasure(nursingDiagnosis, nursingObjective);
            }
        }
        
        
        private void showDgvDiagnosis()
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                GVars.App.UserInput = false;
                
                dsItemRelation.Tables[0].DefaultView.Sort = "DIAGNOSIS_ID";
                string itemId = string.Empty;
                string itemName = string.Empty;
                
                foreach(DataRowView drv in dsItemRelation.Tables[0].DefaultView)
                {
                    if (drv["DIAGNOSIS_ID"].ToString().Equals(itemId) == false)
                    {
                        itemId = drv["DIAGNOSIS_ID"].ToString();
                        
                        int row = dgvNursingDiagnosis.Rows.Add();
                        DataGridViewRow dgvRow = dgvNursingDiagnosis.Rows[row];
                        
                        dgvRow.Cells["NURSING_DIAG_NAME"].Value = itemId;
                    }
                }
                
                //DataRow[] drFind = dsItemRelation.Tables[0].Select(string.Empty, "DIAGNOSIS_ID");
                
                //string itemId = string.Empty;
                //string itemName = string.Empty;
                //for(int i = 0; i < drFind.Length; i++)
                //{
                //    if (drFind[i]["DIAGNOSIS_ID"].ToString().Equals(itemId) == false)
                //    {
                //        itemId = drFind[i]["DIAGNOSIS_ID"].ToString();
                        
                //        int row = dgvNursingDiagnosis.Rows.Add();
                //        DataGridViewRow dgvRow = dgvNursingDiagnosis.Rows[row];
                        
                //        dgvRow.Cells["NURSING_DIAG_NAME"].Value = itemId;
                //    }
                //}
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }
        
        
        private void showDgvObjective(string diagnoseId)
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                GVars.App.UserInput = false;
                
                string filter = "DIAGNOSIS_ID = " + SQL.SqlConvert(diagnoseId);
                DataRow[] drFind = dsItemRelation.Tables[0].Select(filter, "OBJECTIVE_ID");
                
                string itemId = string.Empty;
                string itemName = string.Empty;
                for(int i = 0; i < drFind.Length; i++)
                {
                    if (drFind[i]["OBJECTIVE_ID"].ToString().Equals(itemId) == false)
                    {
                        itemId = drFind[i]["OBJECTIVE_ID"].ToString();
                        int row = dgvNursingObjective.Rows.Add();
                        DataGridViewRow dgvRow = dgvNursingObjective.Rows[row];
                        
                        dgvRow.Cells["NURSING_OBJECTIVE"].Value = itemId;
                    }
                }
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }
        
        
        private void showDgvMeasure(string diagnoseId, string objectiveId)
        {
            string filter = "DIAGNOSIS_ID = " + SQL.SqlConvert(diagnoseId)
                            + "AND OBJECTIVE_ID = " + SQL.SqlConvert(objectiveId);
            
            dsItemRelation.Tables[0].DefaultView.RowFilter = filter;
            dsItemRelation.Tables[0].DefaultView.Sort = "MEASURE_DICT_ID, MEASURE_ID";
            
            dgvNursingMeasure.DataSource = dsItemRelation.Tables[0].DefaultView;
        }
        
        
        private void dgvLoacate(DataGridView dgv, string colName, string targetValue)
        {
            foreach(DataGridViewRow dgvRow in dgv.Rows)
            {
                if (dgvRow.Cells[colName].Value.ToString().Equals(targetValue) == true)
                {
                    dgvRow.Selected = true;
                    return;
                }
            }
        }
        
        
        private void dgvLoacate(DataGridView dgv, string colName0, string colName1, string targetValue0, string targetValue1)
        {
            foreach(DataGridViewRow dgvRow in dgv.Rows)
            {
                if (dgvRow.Cells[colName0].Value.ToString().Equals(targetValue0) == true
                    && dgvRow.Cells[colName1].Value.ToString().Equals(targetValue1) == true)
                {
                    dgvRow.Selected = true;
                    return;
                }
            }
        }
        
        
        private void showMeasureContent(string measureType, string measureId)
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                GVars.App.UserInput = false;
                txtMeasure.Text = string.Empty;
                
                if (measureType.Length == 0 || measureId.Length == 0)
                {
                    return;
                }
                
                // 查找措施内容
                string filter = "DICT_ID = " + SQL.SqlConvert(measureType)
                                + "AND ITEM_ID = " + SQL.SqlConvert(measureId);
                DataRow[] drFind = dsDictItemContent.Tables[0].Select(filter);
                if (drFind.Length > 0)
                {
                    drMeasure = drFind[0];
                }
                else
                {
                    drMeasure = dsDictItemContent.Tables[0].NewRow();
                    
                    drMeasure["DICT_ID"]  = measureType;
                    drMeasure["ITEM_ID"]  = measureId;
                    drMeasure["DEPT_CODE"]  = "*";
                    
                    dsDictItemContent.Tables[0].Rows.Add(drMeasure);
                }
                
                if (drMeasure != null)
                {
                    txtMeasure.Text = drMeasure["CONTENT"].ToString();
                }
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }
        
        
        private string getMeasureContent(string measureType, string measureId)
        {
            if (measureType.Length == 0 || measureId.Length == 0)
            {
                return string.Empty;
            }
            
            // 查找措施内容
            string filter = "DICT_ID = " + SQL.SqlConvert(measureType)
                            + "AND ITEM_ID = " + SQL.SqlConvert(measureId);
            DataRow[] drFind = dsDictItemContent.Tables[0].Select(filter);
            if (drFind.Length > 0)
            {
                return drFind[0]["CONTENT"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        
        
        private void getCurrentRecordId()
        {
            nursingDiagnosis    = string.Empty;
            nursingObjective    = string.Empty;
            nursingMeasureType  = string.Empty;
            nursingMeasure      = string.Empty;
            
            DataGridViewRow dgvRow = null;
            if (dgvNursingDiagnosis.SelectedRows.Count > 0)
            {
                dgvRow = dgvNursingDiagnosis.SelectedRows[0];
                if (dgvRow.Cells["NURSING_DIAG_NAME"].Value != null)
                {
                    nursingDiagnosis = dgvRow.Cells["NURSING_DIAG_NAME"].Value.ToString();
                }
            }
                            
            if (dgvNursingObjective.SelectedRows.Count > 0)
            {
                dgvRow = dgvNursingObjective.SelectedRows[0];
                if (dgvRow.Cells["NURSING_OBJECTIVE"].Value != null)
                {
                    nursingObjective = dgvRow.Cells["NURSING_OBJECTIVE"].Value.ToString();
                }
            }
            
            
            if (dgvNursingMeasure.SelectedRows.Count > 0)
            {
                dgvRow = dgvNursingMeasure.SelectedRows[0];
                
                if (dgvRow.Cells["MEASURE_DICT_ID"].Value != null)
                {
                    nursingMeasureType = dgvRow.Cells["MEASURE_DICT_ID"].Value.ToString();
                }
                
                if (dgvRow.Cells["MEASURE_ID"].Value != null)
                {
                    nursingMeasure = dgvRow.Cells["MEASURE_ID"].Value.ToString();
                }
            }            
        }
        
        
        private void getCurrentRecordText()
        {
            NursingDiagnosis    = string.Empty;
            NursingObjective    = string.Empty;
            
            NursingMeasureType.Clear();
            NursingMeasure.Clear();
            NursingMeasureContent.Clear();
            
            DataGridViewRow dgvRow = null;
            if (dgvNursingDiagnosis.SelectedRows.Count > 0)
            {
                dgvRow = dgvNursingDiagnosis.SelectedRows[0];
                if (dgvRow.Cells["NURSING_DIAG_NAME"].Value != null)
                {
                    NursingDiagnosis = dgvRow.Cells["NURSING_DIAG_NAME"].FormattedValue.ToString();
                }
            }
                            
            if (dgvNursingObjective.SelectedRows.Count > 0)
            {
                dgvRow = dgvNursingObjective.SelectedRows[0];
                if (dgvRow.Cells["NURSING_OBJECTIVE"].Value != null)
                {
                    NursingObjective = dgvRow.Cells["NURSING_OBJECTIVE"].FormattedValue.ToString();
                }
            }
            
            
            if (dgvNursingMeasure.SelectedRows.Count > 0)
            {
                for(int i = 0; i < dgvNursingMeasure.Rows.Count; i++)
                {
                    dgvRow = dgvNursingMeasure.Rows[i];
                    if (dgvRow.Selected == false)
                    {
                        continue;
                    }
                    
                    NursingMeasureType.Add(dgvRow.Cells["NURSING_MEASURE_TYPE"].FormattedValue.ToString());
                    NursingMeasure.Add(dgvRow.Cells["NURSING_MEASURE"].FormattedValue.ToString());
                    NursingMeasureContent.Add(getMeasureContent(dgvRow.Cells["MEASURE_DICT_ID"].Value.ToString(),
                                        dgvRow.Cells["MEASURE_ID"].Value.ToString()));                    
                }
            }
        }
        
        
        private string getDiagnosiFilter(string spell)
        {
            // 查找第一匹配项
            string filter = string.Empty;
            
            spell = spell.Trim();
            if (spell.Length > 0)
            {
                filter = "DESC_SPELL LIKE '" + spell.ToUpper() + "%'";
            }
            else
            {
                return string.Empty;
            }
            
            dvDiagnosis.RowFilter = filter;
            
            ArrayList arrItem = new ArrayList();
            foreach(DataRowView drv in dvDiagnosis)
            {
                if (arrItem.Contains(drv["ITEM_ID"].ToString()) == false)
                {
                    arrItem.Add(drv["ITEM_ID"].ToString());
                }
            }
            
            if (arrItem.Count == 0)
            {
                return "(1 = 2)";
            }
            
            string itemList = string.Empty;
            for(int i = 0; i < arrItem.Count; i++)
            {
                if (itemList.Length > 0)
                {
                    itemList += ", ";
                }
                
                itemList += SQL.SqlConvert(arrItem[i].ToString());            
            }
            
            filter = "DIAGNOSIS_ID IN (" + itemList + ")";
            return filter;
        }
        #endregion
    }
}
