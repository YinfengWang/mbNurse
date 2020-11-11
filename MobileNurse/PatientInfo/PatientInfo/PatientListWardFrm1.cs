using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class PatientListWardFrm1 : Form
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


        public PatientListWardFrm1()
        {
            InitializeComponent();
        }


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
        private void PatientListWardFrm_Load(object sender, EventArgs e)
        {
            try
            {
                ReloadData();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 病区改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbWardList_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                if (cmbWardList.SelectedValue != null)
                {
                    _deptCode = cmbWardList.SelectedValue.ToString();
                }

                DsPatient = patientDbI.GetWardPatientList(_deptCode);
                DsPatient.Tables[0].DefaultView.Sort = "BED_NO";
                dgvPatient.DataSource = DsPatient.Tables[0].DefaultView;

                if (dgvPatient.SelectedRows.Count > 0)
                {
                    dgvPatient_SelectionChanged(sender, e);
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
        /// 病人选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPatient_SelectionChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (dgvPatient.SelectedRows.Count == 0)
                {
                    return;
                }

                DataGridViewRow dgvRow = dgvPatient.SelectedRows[0];

                string patientId = dgvRow.Cells["PATIENT_ID"].Value.ToString();
                if (patientId.Equals(_patientId) == false)
                {
                    _patientId = dgvRow.Cells["PATIENT_ID"].Value.ToString();
                    _visitId = dgvRow.Cells["VISIT_ID"].Value.ToString();

                    if (PatientChanged != null)
                    {
                        PatientChanged(this, new PatientEventArgs(_patientId, _visitId));
                    }
                }
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
        /// 病人列表按键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPatient_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F5)
                {
                    cmbWardList_SelectedValueChanged(sender, e);
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 菜单[刷新]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmnuRefresh_Click(object sender, EventArgs e)
        {
            cmbWardList_SelectedValueChanged(sender, e);
        }
        #endregion

        #region 共通函数
        private void initFrmVal()
        {
            patientDbI = new PatientDbI(GVars.OracleAccess);
            hospitalDbI = new HospitalDbI(GVars.OracleAccess);

            //dsPatient = patientDbI.GetWardPatientList(_deptCode);
            DsWardList = hospitalDbI.Get_WardList_Nurse();

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
            dgvPatient.AutoGenerateColumns = false;

            cmbWardList.DisplayMember = "DEPT_NAME";
            cmbWardList.ValueMember = "DEPT_CODE";
            cmbWardList.DataSource = DsWardList.Tables[0].DefaultView;

            string filter = string.Empty;
            if (_deptCode.Length > 0)
            {
                filter = "DEPT_CODE = " + SqlManager.SqlConvert(_deptCode);
                cmbWardList.Enabled = false;
            }
            else
            {
                filter = "DEPT_CODE = " + SqlManager.SqlConvert("-1");
                cmbWardList.Enabled = true;
            }

            if (DsWardList.Tables[0].Select(filter).Length > 0)
            {
                cmbWardList.SelectedValue = _deptCode;
            }
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

                foreach (DataGridViewRow dgvRow in dgvPatient.Rows)
                {
                    if (dgvRow.Cells["PATIENT_ID"].Value.ToString().Equals(patientId) == true)
                    {
                        dgvRow.Selected = true;
                        break;
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
                cmbWardList_SelectedValueChanged(null, null);
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
            cmbWardList_SelectedValueChanged(null, null);
        }
        #endregion

    }
}
