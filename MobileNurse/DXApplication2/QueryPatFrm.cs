using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HISPlus;
using HISPlus.UserControls;
namespace DXApplication2
{
    public partial class QueryPatFrm : DevExpress.XtraEditors.XtraForm
    {
        #region 窗体变量
        private PatientDbI patientDbi;
        private DataSet dsPatient = null;
        public DataRow[] drPatient = null;//返回选择的病人
        #endregion

        public QueryPatFrm()
        {
            InitializeComponent();
            
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string deptCode = string.Empty;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnQuery.Enabled = false;

                //当没有选择全院时，查询本护理单元病人
                if (!chkAll.Checked)
                    deptCode = GVars.User.DeptCode;

                dsPatient = patientDbi.GetWardPatientList(deptCode, txtPatientid.Text,
                    dtStart.Value, dtEnd.Value.Date.AddDays(1), cbxPatInHosState.Text);
                dsPatient.Tables[0].Columns.Add("STATE", System.Type.GetType("System.String"));
                ucGridView1.DataSource = dsPatient.Tables[0].DefaultView;

                if (!dsPatient.Tables[0].Columns.Contains("CHECKED"))
                {
                    dsPatient.Tables[0].Columns.Add("CHECKED", typeof(bool));
                }

                foreach (DataRow dr in dsPatient.Tables[0].Rows)
                {
                    dr["CHECKED"] = true;
                    dr["STATE"] = cbxPatInHosState.Text;
                    if (cbxPatInHosState.Text == PAT_INHOS_STATE.TRANSFER)
                    {
                        dr["BED_NO"] = 0;
                        dr["BED_LABEL"] = "";
                    }
                }

            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                btnQuery.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private void QueryPatFrm_Load(object sender, EventArgs e)
        {
            try
            {
                InitFrm();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //DataSet dsDesc;
            //DataRow drDesc;
            if (dsPatient == null || dsPatient.Tables.Count == 0)
            {
                this.Close();
                return;
            }
            //dsDesc = dsPatient.Clone();//复制DataSet框架
            dsPatient.AcceptChanges();

            drPatient = dsPatient.Tables[0].Select("CHECKED = 'True'");
            if (drPatient.Length > 0)
                this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void InitFrm()
        {
            DateTime dt;
            dt = GVars.OracleAccess.GetSysDate().AddMonths(-1);
            dtStart.Value  = new DateTime(dt.Year, dt.Month, 1);

            //2016.02.27 临时
            if (GVars.User.DeptCode != "2070")
                chkAll.Enabled = false;

            patientDbi = new PatientDbI(GVars.OracleAccess);
            if (cbxPatInHosState.Count <= 0)
            {
                //2016.02.27 临时
                if (GVars.User.DeptCode == "2070") cbxPatInHosState.Add(PAT_INHOS_STATE.IN);
                
                cbxPatInHosState.Add(PAT_INHOS_STATE.OUT);
                cbxPatInHosState.Add(PAT_INHOS_STATE.TRANSFER);
                //cbxPatInHosState.Add(PAT_INHOS_STATE.WAIT);
                cbxPatInHosState.Text = PAT_INHOS_STATE.OUT;
            }
            if (ucGridView1.Columns.Count <= 0)
            {
                ucGridView1.AllowEdit = true;
                ucGridView1.AddCheckBoxColumn("CHECKED");
                ucGridView1.Add("床号", "BED_NO", 30);
                ucGridView1.Add("床标", "BED_LABEL", 30);
                ucGridView1.Add("姓名", "NAME");
                ucGridView1.Add("性别", "SEX");
                ucGridView1.Add("住院号", "INP_NO");
                ucGridView1.Add("入院日期", "ADMISSION_DATE_TIME");
                ucGridView1.Init();
            }
        }

        private void cbxPatInHosState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(dsPatient!=null)
                dsPatient.Clear();
        }
    }
}