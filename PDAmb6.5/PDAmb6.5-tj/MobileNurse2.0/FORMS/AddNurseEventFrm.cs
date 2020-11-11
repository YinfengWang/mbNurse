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
    public partial class AddNurseEventFrm : Form
    {
        public DataSet DsNurseEvent = null;
        public DataSet DsNurseRec   = null;
        
        private DataRow[] drShows   = null;
        
        public AddNurseEventFrm()
        {
            InitializeComponent();
        }
        
        
        private void mnuCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        
        private void AddNurseEventFrm_Load(object sender, EventArgs e)
        {
            try
            {
                string filter = "ATTRIBUTE = '4'";
                drShows = DsNurseEvent.Tables[0].Select(filter, "SHOW_ORDER");
                
                for(int i = 0; i < drShows.Length; i++)
                {
                    ListViewItem item = new ListViewItem((i + 1).ToString());
                    
                    item.SubItems.Add(drShows[i]["VITAL_SIGNS"].ToString());
                    item.Tag = drShows[i]["VITAL_CODE"].ToString();
                    
                    lvwNurseEvent.Items.Add(item);
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwNurseEvent.SelectedIndices.Count == 0)
                {
                    return;
                }
                
                DataRow drItem = drShows[lvwNurseEvent.SelectedIndices[0]];
                DateTime dtNow = GVars.GetDateNow();
                DataRow drNew = DsNurseRec.Tables[0].NewRow();
                
                DateTime dtEvent = dtNow.Date;
                
                dtEvent = dtEvent.AddHours(dateTimePicker1.Value.Hour);
                dtEvent = dtEvent.AddMinutes(dateTimePicker1.Value.Minute);
                
                if (dtEvent.CompareTo(dtNow) > 0)
                {
                    GVars.Msg.Show("I00009");                           // , "护理事件时间不能大于当前时间!");
                    return;
                }
                
                drNew["PATIENT_ID"]            = GVars.Patient.ID;
                drNew["VISIT_ID"]              = GVars.Patient.VisitId;
                drNew["TIME_POINT"]            = dtEvent;
                drNew["VITAL_CODE"]            = drItem["VITAL_CODE"];
                drNew["ATTRIBUTE"]             = drItem["ATTRIBUTE"];
                drNew["RECORDING_DATE"]        = dtEvent.Date;
                drNew["VITAL_SIGNS"]           = drItem["VITAL_SIGNS"];
                drNew["VITAL_SIGNS_CVALUES"]   = txtValue.Text.Trim();
                drNew["UNITS"]                 = drItem["UNIT"];
                drNew["CLASS_CODE"]            = drItem["CLASS_CODE"];
                drNew["NURSE"]                 = GVars.User.Name;
                drNew["WARD_CODE"]             = drItem["WARD_CODE"];
                drNew["UPD_DATE_TIME"]         = GVars.GetDateNow();
                
                DsNurseRec.Tables[0].Rows.Add(drNew);
                
                this.DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
    }
}