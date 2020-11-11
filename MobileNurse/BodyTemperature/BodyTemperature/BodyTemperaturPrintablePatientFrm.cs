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
    public partial class BodyTemperaturPrintablePatientFrm : Form
    {
        
        public string Bed_Label = string.Empty;
        public string Patient_Id = string.Empty;

        private DataSet dsPatient = null;
        
        public BodyTemperaturPrintablePatientFrm()
        {
            InitializeComponent();
        }

        private void initDisp(bool isOut3Days)
        {
            try
            {
                //string sql = "select patient_id, visit_id, min(time_point) min_time_point, "
                //        + "(SELECT PAT_MASTER_INDEX.NAME FROM PAT_MASTER_INDEX WHERE PAT_MASTER_INDEX.PATIENT_ID = VITAL_SIGNS_REC.PATIENT_ID ) NAME,"
                //        + "(SELECT MIN(LOG_DATE_TIME) FROM ADT_LOG WHERE PATIENT_ID = VITAL_SIGNS_REC.PATIENT_ID AND VISIT_ID = VITAL_SIGNS_REC.VISIT_ID) ADMISSION_DATE_TIME,"
                //        + "(SELECT BED_REC.BED_LABEL FROM BED_REC ,PATS_IN_HOSPITAL "
                //        +   "WHERE PATS_IN_HOSPITAL.WARD_CODE = BED_REC.WARD_CODE AND PATS_IN_HOSPITAL.BED_NO = BED_REC.BED_NO "
                //        +    " AND PATS_IN_HOSPITAL.PATIENT_ID = VITAL_SIGNS_REC.PATIENT_ID AND PATS_IN_HOSPITAL.VISIT_ID = VITAL_SIGNS_REC.VISIT_ID) BED_LABEL "
                //        +  "from vital_signs_rec ";

                //sql += "WHERE WARD_CODE = " + SqlManager.SqlConvert(GVars.User.DeptCode); 
                //sql += "group by patient_id, visit_id";

                StringBuilder sb = new StringBuilder();
                //sb.Append("SELECT PATS_IN_HOSPITAL.PATIENT_ID, ");
                //sb.Append("PATS_IN_HOSPITAL.VISIT_ID, ");
                //sb.Append("MIN(VITAL_SIGNS_REC.TIME_POINT) MIN_TIME_POINT, ");

                //sb.Append("(SELECT PAT_MASTER_INDEX.NAME ");
                //sb.Append("FROM PAT_MASTER_INDEX ");
                //sb.Append("WHERE PAT_MASTER_INDEX.PATIENT_ID = PATS_IN_HOSPITAL.PATIENT_ID) NAME, ");

                //sb.Append("(SELECT MIN(LOG_DATE_TIME) ");
                //sb.Append("FROM ADT_LOG ");
                //sb.Append("WHERE PATIENT_ID = PATS_IN_HOSPITAL.PATIENT_ID AND ");
                //sb.Append("VISIT_ID = PATS_IN_HOSPITAL.VISIT_ID) ADMISSION_DATE_TIME, ");

                //sb.Append("(SELECT BED_LABEL ");
                //sb.Append("FROM BED_REC ");
                //sb.Append("WHERE PATS_IN_HOSPITAL.WARD_CODE = BED_REC.WARD_CODE AND ");
                //sb.Append("PATS_IN_HOSPITAL.BED_NO = BED_REC.BED_NO ) BED_LABEL ");
                //sb.Append("FROM PATS_IN_HOSPITAL, VITAL_SIGNS_REC ");
                //sb.Append("WHERE PATS_IN_HOSPITAL.WARD_CODE = " + SqlManager.SqlConvert(GVars.User.DeptCode));
                //sb.Append("AND PATS_IN_HOSPITAL.PATIENT_ID = VITAL_SIGNS_REC.PATIENT_ID ");
                //sb.Append("AND PATS_IN_HOSPITAL.VISIT_ID = VITAL_SIGNS_REC.VISIT_ID ");
                //sb.Append("GROUP BY PATS_IN_HOSPITAL.PATIENT_ID, PATS_IN_HOSPITAL.VISIT_ID, ");
                //sb.Append("PATS_IN_HOSPITAL.BED_NO, PATS_IN_HOSPITAL.WARD_CODE ");
                //sb.Append(" select * from ");
                //sb.Append(" (");
                sb.Append(" SELECT A.PATIENT_ID,A.VISIT_ID,MIN(TIME_POINT) MIN_TIME_POINT,B.name,B.ADMISSION_DATE_TIME,B.bed_label");
                sb.Append(" FROM VITAL_SIGNS_REC A, patient_info B");
                sb.Append(" WHERE (B.WARD_CODE = " + SqlManager.SqlConvert(GVars.User.DeptCode));
                if (isOut3Days == true)
                {
                    sb.Append(" or B.WARD_CODE is null)");
                }
                else
                {
                    sb.Append(" and 1=1)");
                }
                sb.Append(" AND A.PATIENT_ID = B.PATIENT_ID");
                sb.Append(" AND A.VISIT_ID = B.VISIT_ID");
                sb.Append(" GROUP BY A.PATIENT_ID, A.VISIT_ID,B.name,B.ADMISSION_DATE_TIME,B.bed_label");
                sb.Append(" order by length(B.bed_label) asc,B.bed_label asc");

                dsPatient = GVars.OracleAccess.SelectData(sb.ToString());
                DateTime dtNow = GVars.OracleAccess.GetSysDate();

                //清空数据
                listView1.Items.Clear();
                // 显示数据
                int rowIndex = -1;
                foreach (DataRow dr in dsPatient.Tables[0].Rows)
                {
                    rowIndex++;

                    if (dr["min_time_point"].ToString().Length == 0)
                    {
                        continue;
                    }

                    //LB20110614如果是已经出院了的病人，直接添加上去
                    if (dr["BED_LABEL"].ToString().Length == 0)
                    {
                        ListViewItem item = listView1.Items.Add(dr["BED_LABEL"].ToString());
                        item.SubItems.Add(dr["NAME"].ToString());
                        item.SubItems.Add(dr["PATIENT_ID"].ToString());
                        item.Tag = rowIndex;
                        continue;
                    }
                    //LB20110614结束

                    DateTime dtMin = (DateTime)dr["min_time_point"];
                    TimeSpan tspan = dtNow.Date.Subtract(dtMin.Date);
                    if (tspan.TotalDays > 0 && tspan.TotalDays % ComConst.VAL.DAYS_PER_WEEK == 0)
                    {
                        ListViewItem item = listView1.Items.Add(dr["BED_LABEL"].ToString());
                        item.SubItems.Add(dr["NAME"].ToString());
                        item.SubItems.Add(dr["PATIENT_ID"].ToString());
                        item.Tag = rowIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        private void BodyTemperaturPrintablePatientFrm_Load(object sender, EventArgs e)
        {
            initDisp(false);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedIndices.Count == 0)
                {
                    return;
                }
                
                Bed_Label = listView1.SelectedItems[0].SubItems[0].Text;
                Patient_Id = listView1.SelectedItems[0].SubItems[2].Text;

                DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
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
            catch(Exception ex)
            {   
                Error.ErrProc(ex);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            initDisp(checkBox1.Checked);
        }
    }
}
