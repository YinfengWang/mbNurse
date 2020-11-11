using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors.Controls;

namespace HISPlus
{
    public partial class UCPatientList : UserControl
    {

        #region 变量
        public event PatientChangedEventHandler PatientChanged;                              // 定义事件

        protected string _deptCode = string.Empty;

        protected string _patientId = string.Empty;
        protected string _visitId = string.Empty;
        protected bool _showWaitInpPatient = false;                                        // 是否显示待入科病人
        protected bool _showOutHospital3Days = false;

        private PatientDbI patientDbI = null;
        private HospitalDbI hospitalDbI = null;
        public DataSet DsPatient = null;
        public DataSet DsWardList = null;
        #endregion

        #region 属性

        public string DeptCode
        {
            get { return _deptCode; }
            set { _deptCode = value; }
        }


        public string PatientId
        {
            get { return _patientId; }
        }


        public string VisitId
        {
            get { return _visitId; }
        }


        /// <summary>
        /// 属性[显示待入科病人]
        /// </summary>
        public bool ShowWaitInpPatient
        {
            get
            {
                return _showWaitInpPatient;
            }
            set
            {
                _showWaitInpPatient = value;
            }
        }

        /// <summary>
        /// 属性[显示三天之内的病人]
        /// </summary>
        public bool ShowOutHospital3Days
        {
            get
            {
                return _showOutHospital3Days;
            }
            set
            {
                _showOutHospital3Days = value;
            }
        }

        #endregion


        #region 窗体事件

        public UCPatientList()
        {
            InitializeComponent();
        }

        public void Init()
        {
            ReloadData();
        }

        private void UCPatientList_Load(object sender, EventArgs e)
        {            
            //try
            //{
            //    ReloadData();
            //}
            //catch (Exception ex)
            //{
            //    Error.ErrProc(ex);
            //}
        }

        /// <summary>
        /// 病区改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                if (lookUpEdit1.EditValue != null && !string.IsNullOrEmpty(lookUpEdit1.EditValue.ToString()))
                {
                    _deptCode = lookUpEdit1.EditValue.ToString();
                }

                DsPatient = patientDbI.GetWardPatientList(_deptCode);
                DsPatient.Tables[0].DefaultView.Sort = "BED_NO";
                // if (DsPatient != null)
                gridControl1.DataSource = DsPatient.Tables[0].DefaultView;

                if (gridView1.SelectedRowsCount > 0)
                {
                    gridView1_FocusedRowChanged(null, null);
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
        /// 病人列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            dgv.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        /// <summary>
        /// 菜单[刷新]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmnuRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }
        #endregion


        #region 共通函数
        private void initFrmVal()
        {
            // if (GVars.OracleAccess == null) return;

            patientDbI = new PatientDbI(GVars.OracleAccess);
            hospitalDbI = new HospitalDbI(GVars.OracleAccess);
            //dsPatient = patientDbI.GetWardPatientList(_deptCode);
            try
            {
                DsWardList = hospitalDbI.Get_WardList_Nurse();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //if (DsWardList == null || DsWardList.Tables.Count == 0 || DsWardList.Tables[0].Rows.Count == 0)
            //    return;

            if (_showWaitInpPatient == true)
            {
                DataRow drNew = DsWardList.Tables[0].NewRow();

                drNew["DEPT_CODE"] = "-1";
                drNew["DEPT_NAME"] = "待入科";

                DsWardList.Tables[0].Rows.Add(drNew);
            }

            //if (_showOutHospital3Days == true)
            //{
            //    DataRow drNew = DsWardList.Tables[0].NewRow();

            //    drNew["DEPT_CODE"] = "-2";
            //    drNew["DEPT_NAME"] = "已出院(三天)";

            //    DsWardList.Tables[0].Rows.Add(drNew);
            //}
        }
        
        private void initDisp()
        {
            if (DsWardList == null) return;

            lookUpEdit1.Properties.ValueMember = "DEPT_CODE";// 实际要用的字段;   //相当于editvalue
            lookUpEdit1.Properties.DisplayMember = "DEPT_NAME"; //要显示的字段;    //相当于text
            lookUpEdit1.Properties.DataSource = DsWardList.Tables[0].DefaultView;

            LookUpColumnInfo col = new LookUpColumnInfo("DEPT_NAME", "选择科室");       // 定义列信息 对应的字段名称及字段表头即Caption                     
            lookUpEdit1.Properties.Columns.Add(col);                  // 向 LookUpEdit 中添加列

            string filter = string.Empty;
            if (_deptCode.Length > 0){
                filter = "DEPT_CODE = " + SqlManager.SqlConvert(_deptCode);
                lookUpEdit1.Enabled = false;
            }
            else
            {
                filter = "DEPT_CODE = " + SqlManager.SqlConvert("-1");
                lookUpEdit1.Enabled = true;
            }
            if (DsWardList.Tables[0].Select(filter).Length > 0)
            {                
                lookUpEdit1.EditValue = _deptCode;
            }
            else if (_deptCode.Length > 0)
                lookUpEdit1.Properties.NullText = @"无效科室";
            else
                // 选中第一项
                lookUpEdit1.ItemIndex =0;
        }
        #endregion


        #region 接口
        /// <summary>
        /// 定位病人
        /// </summary>
        public void locatePatient(string patientId)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;
                if (patientId.Equals(_patientId) == true)
                {
                    return;
                }

                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    for (int j = 0; j < gridView1.Columns.Count; j++)
                    {
                        if (gridView1.GetRowCellValue(i, "PATIENT_ID").Equals(patientId) == true)
                        {
                            gridView1.FocusedRowHandle = i;
                            gridView1.SelectRow(i);
                            break;
                        }
                    }
                }
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }


        /// <summary>
        /// 重新加载数据
        /// </summary>
        public void ReloadData()
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;

                initFrmVal();

                initDisp();

                GVars.App.UserInput = true;
                lookUpEdit1_EditValueChanged(null, null);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }


        /// <summary>
        /// 重新加载病人列表
        /// </summary>
        public void ReloadPatientList()
        {
            lookUpEdit1_EditValueChanged(null, null);
        }
        #endregion

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (gridView1.SelectedRowsCount == 0)
                {
                    return;
                }
                string patientId = Convert.ToString(gridView1.GetFocusedRowCellValue("PATIENT_ID"));
                if (!patientId.Equals(_patientId))
                {
                    _patientId = patientId;
                    _visitId = Convert.ToString(gridView1.GetFocusedRowCellValue("VISIT_ID"));
                    if (PatientChanged != null)
                    {
                        PatientChanged(this, new PatientEventArgs(_patientId, _visitId));
                    }                
                }
            }
            catch (Exception ex){
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public override void Refresh()
        {
            lookUpEdit1_EditValueChanged(null, null);
        }

        /// <summary>
        /// 病人列表按键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F5)
                {
                    Refresh();
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
    }
}
