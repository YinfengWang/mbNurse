using System;
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
    public partial class NursingRecNormal : Form
    {
        #region �������
        //private NursingRecDocumentCom com = new NursingRecNormalCom();
        
        private PatientDbI              patientCom;                                             // ����
        
        private DataSet                 dsPatient   = null;
        private string                  patientId   = string.Empty;
        private string                  visitId     = string.Empty;
        private bool                    toselect    = false;
        //private HISPlus.ExcelAccess excelAccess = null;                 // Excel��ӡ���ʽӿ�
        private int linenumber = 20;

        #endregion
        
        public NursingRecNormal()
        {
            InitializeComponent();

            this.txtBedLabel.KeyDown += new KeyEventHandler( txtBedLabel_KeyDown );
            this.txtBedLabel.GotFocus += new EventHandler(imeCtrl_GotFocus);
        }
        
        
        #region �����¼�
        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NursingRecNormal_Load( object sender, EventArgs e )
        {
            try
            {
                initFrmVal();
                initDisp();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// �����ı��س�����ѯ������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtBedLabel_KeyDown( object sender, KeyEventArgs e )
        {
            try
            {
                
                this.Cursor = Cursors.WaitCursor;

                // �������
                if (e.KeyCode != Keys.Return)
                {
                    return;
                }

                // ��ȡ��ѯ����
                if (txtBedLabel.Text.Trim().Length == 0)
                {
                    return;
                }

                // ��ս���
                initDisp();

                // ��ȡ������Ϣ
                dsPatient = patientCom.GetInpPatientInfo_FromBedLabel(txtBedLabel.Text.Trim(), GVars.User.DeptCode);
                if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
                {
                    GVars.Msg.Show("W00005");                           // �ò��˲�����!	
                    return;
                }

                // ��ʾ������Ϣ
                showPatientInfo();
                
                PatientGridView.DataSource = PatientData(patientId, visitId).Tables[0].DefaultView;
                toselect = false;

            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                
            }
        }
        
        
        /// <summary>
        /// ��ť[��ѯ]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click( object sender, EventArgs e )
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (patientId.Length == 0)
                {
                    GVars.Msg.Show("E00011");                                                   // ����ȷ������!
                    return;
                }

                // ��ȡ����
                //com.Now = GVars.OracleAccess.GetSysDate();                                                 // ��ȡ���ڵ�ʱ��
                PatientGridView.DataSource = PatientData(patientId, visitId, dtRngStart.Value, dtRngEnd.Value).Tables[0].DefaultView;           // ��ȡ����                

                // ��ʾ����
                //showNursingRec(string.Empty);
                toselect = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        
        
        /// <summary>
        /// ��ť[ɾ��]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click( object sender, EventArgs e )
        {
            if (txtBedLabel.Text.Trim().Length == 0)
            {
                return;
            }
            try
            {
                string sql = "DELETE FROM NURSING_RECORD_NORMAL WHERE ";
                sql += "PATIENT_ID = '" + patientId + "'";
                sql += "AND VISIT_ID = '" + visitId + "'";
                sql += "AND NURSING_DATE = " + SQL.GetOraDbDate(this.PatientGridView[1, PatientGridView.CurrentCell.RowIndex].Value.ToString());
                GVars.OracleAccess.ExecuteNoQuery(sql);
                if (toselect)
                {
                    PatientGridView.DataSource = PatientData(patientId, visitId, dtRngStart.Value, dtRngEnd.Value).Tables[0].DefaultView;
                }
                if (!toselect)
                {
                    PatientGridView.DataSource = PatientData(patientId, visitId).Tables[0].DefaultView;
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        
        /// <summary>
        /// ��ť[��ӡ]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContinuePrint_Click( object sender, EventArgs e )
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // ��ȡ��Ҫ��ӡ�ļ�¼
                //string filter = string.Empty;
                DataTable dtl;
                DataRow[] drData = null;
                if (toselect)
                {
                    dtl = PatientData(patientId, visitId, dtRngStart.Value, dtRngEnd.Value).Tables[0];
                    drData = dtl.Select();
                }
                if (!toselect)
                {
                    dtl = PatientData(patientId, visitId).Tables[0];
                    drData = dtl.Select();
                }
                //��ӡ������ַ�������
                string str = null;
                StreamReader sr = new StreamReader(@"Template\�����¼ģ��(��ͨ).txt", Encoding.Default);
                str = sr.ReadToEnd();
                sr.Close();
                string[] htmtxt = str.Split(new char[] { '��' });
                string htmtxt1 = htmtxt[0].ToString();
                string htmtxt2 = htmtxt[1].ToString().Replace("[#����]", lblName.Text).Replace("[#�Ʊ�]", lblDeptName.Text).Replace("[#����]", txtBedLabel.Text).Replace("[#סԺ��]", patientId).Replace("[#�ٴ����]", lblDiagnose.Text);
                string htmtxt3 = htmtxt[2].ToString();

                string htmtxt4 = null;
                if (drData.Length > 0)
                {
                    for (int i = 0; i < drData.Length; i++)
                    {
                        DateTime dtm = DateTime.Parse(drData[i][0].ToString());

                        linenumber = int.Parse(GVars.IniFile.ReadString("LINENUMBER", "NursingRecNormal", string.Empty));

                        TextDesc DET = new TextDesc();
                        string desc = DET.TextEditDesc(drData[i][1].ToString(), linenumber);
                        string[] descs = desc.Replace("\r\n", "��").Split(new char[] { '��' });
                        for (int j = 0; j < descs.Length; j++)
                        {
                            if (j == 0)
                            {
                                htmtxt4 += htmtxt[3].ToString().Replace("[#����]", dtm.ToShortDateString()).Replace("[#ʱ��]", dtm.ToShortTimeString()).Replace("[#����]", descs[j].ToString());
                            }
                            else 
                            {
                                htmtxt4 += htmtxt[3].ToString().Replace("[#����]", " ").Replace("[#ʱ��]", " ").Replace("[#����]", descs[j].ToString());
                            }
                        }
                    }
                }
                else 
                {
                    htmtxt4 = htmtxt[3].ToString();
                }
                string htmtxt5 = htmtxt[4].ToString();

                string txtValue = htmtxt1 + htmtxt2 + htmtxt3 + htmtxt4 + htmtxt5;
                FormPrint fmp = new FormPrint(txtValue);
                fmp.ShowDialog();

              

            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        /// <summary>
        /// ��ť[�˳�]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click( object sender, EventArgs e )
        {
            try
            {
                this.Close();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// ȷ��Ϊ������뷨
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void imeCtrl_GotFocus(object sender, EventArgs e)
        {
            IME.ChangeControlIme(this.ActiveControl.Handle);
        }
        #endregion
        
        
        #region ��ͨ����
        /// <summary>
        /// ��ʼ���������
        /// </summary>
        private void initFrmVal()
        {
            patientCom = new PatientDbI(GVars.OracleAccess);
            
            //com.CreateDataTable();
        }
        

        /// <summary>
        /// ��ʼ��������ʾ
        /// </summary>
        private void initDisp()
        {
            
            // ���������Ϣ
            lblName.Text    = string.Empty;
            lblGender.Text  = string.Empty;
            lblAge.Text     = string.Empty;
            lblDeptName.Text = string.Empty;
            lblDocNo.Text   = string.Empty;
            lblDiagnose.Text = string.Empty;

            // ��ťλ��
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        private DataSet PatientData(string patientId, string visitId)
        {
            // ��ȡ�����¼
            string sql = string.Empty;

            sql += "SELECT NURSING_DATE,NURSING_STATE_ILL,USER_NAME, '' test ";
            sql += " FROM ";
            sql += "NURSING_RECORD_NORMAL ";
            sql += "WHERE ";
            sql += "PATIENT_ID = " + SQL.SqlConvert(patientId);
            sql += "AND VISIT_ID = " + SQL.SqlConvert(visitId);
            sql += "ORDER BY ";
            sql += " NURSING_DATE ASC";

            DataSet ds = GVars.OracleAccess.SelectData(sql);
            return ds;
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        private DataSet PatientData(string patientId, string visitId, DateTime dtStart, DateTime dtEnd)
        {
            // ��ȡ�����¼
            string sql = string.Empty;

            sql += "SELECT NURSING_DATE,NURSING_STATE_ILL,USER_NAME";
            sql += "  FROM ";
            sql += "NURSING_RECORD_NORMAL ";
            sql += "WHERE ";
            sql += "PATIENT_ID = " + SQL.SqlConvert(patientId);
            sql += "AND VISIT_ID = " + SQL.SqlConvert(visitId);

            sql += "AND NURSING_DATE >= " + SQL.GetOraDbDate(dtStart.ToString());
            sql += "AND NURSING_DATE < " + SQL.GetOraDbDate(dtEnd.ToString());

            sql += "ORDER BY ";
            sql += " NURSING_DATE ASC";

            DataSet ds = GVars.OracleAccess.SelectData(sql);
            return ds;
        }


        /// <summary>
        /// ��ʾ���˵Ļ�����Ϣ
        /// </summary>
        private void showPatientInfo()
        { 
            patientId   = string.Empty;
            visitId     = string.Empty;
            
            // ���û�������˳�
            if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
            {
                GVars.Msg.Show("W00005");                               // �ò��˲�����!
                return;
            }
            
            // ��ʾ���˵Ļ�����Ϣ
            DataRow dr = dsPatient.Tables[0].Rows[0];
            PersonCls person = new PersonCls();

            string age = dr["DATE_OF_BIRTH"].ToString();
            if (age.Length > 0)
            {
                age = PersonCls.GetAge(DateTime.Parse(age), GVars.OracleAccess.GetSysDate());
            }
            
            this.lblName.Text       = dr["NAME"].ToString();            // ��������
            this.lblGender.Text     = dr["SEX"].ToString();             // �����Ա�
            this.lblAge.Text        = age;                              // ����
            this.lblDeptName.Text   = dr["DEPT_NAME"].ToString();       // �Ʊ�
            this.lblDocNo.Text      = dr["INP_NO"].ToString();          // סԺ��
            this.lblDiagnose.Text   = dr["DIAGNOSIS"].ToString();       // ���
            
            patientId   = dr["PATIENT_ID"].ToString();
            visitId     = dr["VISIT_ID"].ToString();
        }
        
        
        /// <summary>
        /// ��ʾ�����¼
        /// </summary>
        /// <param name="timePoint"></param>
        /// <param name="rowStart"></param>
        private void showNursingRec(string timePoint)
        {
            //PatientData(patientId, visitId, "", "");
        }
        
        
        /// <summary>
        /// ��ʾ��������
        /// </summary>
        private void showNursingRec(DataRow[] drArray, int rowStart)
        {
 
        }
        
        

        #endregion

        private void PatientGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string desc = string.Empty;
            if (PatientGridView.Columns[e.ColumnIndex] == Column4 && txtBedLabel.Text != "")   // Column4 ����� ButtonColumn ���Q
            {
                FormTextEditor frmShow = new FormTextEditor("");
                //frmShow.Desc = desc.Replace(ComConst.STR.TAB, string.Empty);
                frmShow.ShowDialog();
                PatientGridView.Rows[e.RowIndex].Cells["Column2"].Value += frmShow.Desc;
                if (PatientGridView.Rows[e.RowIndex].Cells["Column1"].Value.ToString().Length < 1)
                {
                    string sql = "insert into NURSING_RECORD_NORMAL(DEPT_CODE,PATIENT_ID,VISIT_ID,NURSING_DATE,NURSING_STATE_ILL,DB_USER,USER_NAME)";
                    sql = sql + "values( ";
                    sql = sql + "'" + GVars.User.DeptCode + "'";
                    sql = sql + ",'" + patientId + "'";
                    sql = sql + ",'" + visitId + "'";
                    sql = sql + "," + SQL.GetOraDbDate(DateTime.Now);
                    sql = sql + ",'" + PatientGridView.Rows[e.RowIndex].Cells["Column2"].Value + "'";
                    sql = sql + ",'" + GVars.User.ID + "'";
                    sql = sql + ",'" + GVars.User.Name + "'";
                    sql = sql + ")";
                    GVars.OracleAccess.ExecuteNoQuery(sql);
                    PatientGridView.Rows[e.RowIndex].Cells["Column1"].Value = DateTime.Now;
                    PatientGridView.Rows[e.RowIndex].Cells["Column3"].Value = GVars.User.Name;
                    this.PatientGridView.Rows[e.RowIndex].Cells["Column2"].Selected = true;
                    this.PatientGridView.CurrentCell = this.PatientGridView.Rows[e.RowIndex].Cells["Column2"];
                    
                }
                else if (PatientGridView.Rows[e.RowIndex].Cells["Column1"].Value != null)
                {
                    string sql = "UPDATE NURSING_RECORD_NORMAL ";
                    sql += "SET ";
                    sql += "NURSING_STATE_ILL = '" + PatientGridView.Rows[e.RowIndex].Cells["Column2"].Value + "'";
                    sql += "WHERE ";
                    sql += "PATIENT_ID = '" + patientId + "'";
                    sql += "AND VISIT_ID = '" + visitId + "'";
                    sql += "AND NURSING_DATE = " + SQL.GetOraDbDate(PatientGridView.Rows[e.RowIndex].Cells["Column1"].Value.ToString());
                    GVars.OracleAccess.ExecuteNoQuery(sql);
                }
                

            }
        }

        private void PatientGridView_CancelRowEdit(object sender, QuestionEventArgs e)
        {
            //PatientGridView.CurrentCell.

        }

        private void PatientGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            PatientGridView.Rows[e.RowIndex].ErrorText = "";
            if (PatientGridView.Columns[e.ColumnIndex].DataPropertyName == "NURSING_STATE_ILL")
            {
                if (e.FormattedValue == null)
                {
                    PatientGridView.Rows[e.RowIndex].ErrorText = "��¼���ݲ���Ϊ�գ�";
                }

            }
 
        }

        private void PatientGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (txtBedLabel.Text.Trim().Length == 0)
            {
                return;
            }
            PatientGridView.Rows[e.RowIndex].ErrorText = "";
            if (PatientGridView.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString().Length < 1)
            {
                string sql = "insert into NURSING_RECORD_NORMAL(DEPT_CODE,PATIENT_ID,VISIT_ID,NURSING_DATE,NURSING_STATE_ILL,DB_USER,USER_NAME)";
                sql = sql + "values( ";
                sql = sql + "'" + GVars.User.DeptCode + "'";
                sql = sql + ",'" + patientId + "'";
                sql = sql + ",'" + visitId + "'";
                sql = sql + "," + SQL.GetOraDbDate(DateTime.Now);
                sql = sql + ",'" + PatientGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value + "'";
                sql = sql + ",'" + GVars.User.ID + "'";
                sql = sql + ",'" + GVars.User.Name + "'";
                sql = sql + ")";
                GVars.OracleAccess.ExecuteNoQuery(sql);
                PatientGridView.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value = DateTime.Now;
                PatientGridView.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = GVars.User.Name;
            }
            else if (PatientGridView.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value != null)
            {
                string sql = "UPDATE NURSING_RECORD_NORMAL ";
                sql += "SET ";
                sql += "NURSING_STATE_ILL = '" + PatientGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value + "'";
                sql += "WHERE ";
                sql += "PATIENT_ID = '" + patientId + "'";
                sql += "AND VISIT_ID = '" + visitId + "'";
                sql += "AND NURSING_DATE = " + SQL.GetOraDbDate(PatientGridView.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString());
                GVars.OracleAccess.ExecuteNoQuery(sql);
            }
            
        }

        private void PatientGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //����ÿһ��
            int i = 0;
            foreach (DataGridViewRow dgv in PatientGridView.Rows)
            {

                //ȡ�ض��е�ֵ����������INDEX
                if (i == 0)
                {
                    dgv.DefaultCellStyle.ForeColor = Color.Blue;
                }
                else if (i == 1)
                {
                    dgv.DefaultCellStyle.ForeColor = Color.Red;
                }
                if (i == 0)
                {
                    i = 1;
                }
                else
                {
                    i = 0;
                }

            }

        }


    }
}