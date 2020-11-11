using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class BodyTemperHistory : Form
    {
        public string Patient_Id = string.Empty;
        public string Patient_Name = string.Empty;        
        private DataSet dsPatient = null;

        //输出参数
        public string Visit_Id = string.Empty;
        public string MIN_TIME_POINT = string.Empty;

        public BodyTemperHistory()
        {
            InitializeComponent();
        }

        private void BodyTemperHistory_Load(object sender, EventArgs e)
        {
            initDisp(false);
        }

        private void initDisp(bool isOut3Days)
        {
            try
            {                
                StringBuilder sb = new StringBuilder();
                sb.Append("select patient_id,visit_id,MIN(TIME_POINT) MIN_TIME_POINT,MAX(TIME_POINT) MAX_TIME_POINT from VITAL_SIGNS_REC");
                sb.Append(" where patient_id=" + SqlManager.SqlConvert(this.Patient_Id) + " GROUP by patient_id,visit_id order by visit_id");

                dsPatient = GVars.OracleAccess.SelectData(sb.ToString());

                //清空数据
                listView1.Items.Clear();
                // 显示数据
                int rowIndex = -1;
                foreach (DataRow dr in dsPatient.Tables[0].Rows)
                {
                    rowIndex++;
                    ListViewItem item = listView1.Items.Add(this.Patient_Name);
                    item.SubItems.Add(dr["patient_id"].ToString());
                    item.SubItems.Add(dr["visit_id"].ToString());
                    item.SubItems.Add(dr["MIN_TIME_POINT"].ToString());
                    item.SubItems.Add(dr["MAX_TIME_POINT"].ToString());
                    item.Tag = rowIndex;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedIndices.Count == 0)
                {
                    return;
                }

                Visit_Id = listView1.SelectedItems[0].SubItems[2].Text;
                MIN_TIME_POINT = listView1.SelectedItems[0].SubItems[3].Text;
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
    }
}
