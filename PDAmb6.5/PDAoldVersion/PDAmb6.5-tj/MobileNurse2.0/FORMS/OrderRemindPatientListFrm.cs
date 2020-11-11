using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class OrderRemindPatientListFrm : Form
    {

        private DataSet dsPatientInfoList = null;

        private string patientBedLabel = null;

        public string PatientBedLabel
        {
            get { return patientBedLabel; }
        }

        public DataSet DsPatientInfoList
        {
            set { dsPatientInfoList = value; }
        }


        public OrderRemindPatientListFrm()
        {
            InitializeComponent();
        }

        private void OrderRemindPatientListFrm_Load(object sender, EventArgs e)
        {
            ShowPatientInfo();
        }

        private void ShowPatientInfo()
        {
            if (dsPatientInfoList == null)
            {
                return;
            }
            DataTable dt=dsPatientInfoList.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(dt.Rows[i]["BED_LABEL"].ToString());
                item.SubItems.Add(dt.Rows[i]["NAME"].ToString());
                item.SubItems.Add(dt.Rows[i]["SEX"].ToString());
                item.SubItems.Add(PersonCls.GetAge((DateTime)(dt.Rows[i]["DATE_OF_BIRTH"]), GVars.GetDateNow()));
                lvwPatientList.Items.Add(item);
            }
        }

        private void mnuOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwPatientList.SelectedIndices.Count == 0)
                {
                    GVars.Msg.Show("E00060", "病人");               // "请选择{0}!"
                    return;
                }
                patientBedLabel = lvwPatientList.Items[lvwPatientList.SelectedIndices[0]].Text;
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void mnuCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}