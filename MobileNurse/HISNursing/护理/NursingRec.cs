using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SqlConvert = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class NursingRec : Form
    {
        #region �������
        private const string RIGHT_ID = "020204";                       // ��������Ҫ��Ȩ��

        private PatientDbI  patientCom;                                 // ����
        private NursingDbI  nursingCom;
        
        private DataSet dsPatient   = null;                             // ������Ϣ
        private DataSet dsNursing   = null;                             // ������Ϣ
        private DataSet dsNursingItemDict = null;                       // ������Ŀ�ֵ�
        private DataRow[] drNursingItem   = null;
        
        private string patientId    = string.Empty;                     // ����ID��
        private string visitId      = string.Empty;                     // ���ξ������
        
        private bool    blnRights   = false;                            // �Ƿ�����޸�Ȩ��
        private bool    blnSuperUser= false;                            // �����û�
        #endregion


        public NursingRec()
        {
            InitializeComponent();

            this.KeyPress += new KeyPressEventHandler( NursingRec_KeyPress );

            this.txtBedLabel.GotFocus += new EventHandler(imeCtrl_GotFocus);
            this.txtBedLabel.KeyDown += new KeyEventHandler( txtBedLabel_KeyDown );

            this.txtValue.GotFocus += new EventHandler(imeCtrl_GotFocus);
            this.cmbClass.GotFocus += new EventHandler(imeCtrl_GotFocus);
            this.cmbItem.GotFocus += new EventHandler(imeCtrl_GotFocus);
        }

        
        #region �����¼�
        private void NursingRec_Load( object sender, EventArgs e )
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
        /// �����KeyPress�¼�
        /// </summary>
        /// <remarks>
        /// ��Ҫ����س����Tab
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void NursingRec_KeyPress( object sender, KeyPressEventArgs e )
        {
            try
            {
                // ����ǻس�, ת����Tab��
                if (e.KeyChar.Equals((char)Keys.Return))
                {
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                    
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
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

                // ��ѯ���˻����¼
                btnQuery_Click(sender, e);
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
                btnQuery.Enabled = false;
                
                // ��ȡ�����¼
                dsNursing = nursingCom.GetNursingData(patientId, visitId, dtRngStart.Value, dtRngEnd.Value, cmbItem_Filter.Text);
                
                // ��ʾ�����¼
                showNursingData();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                btnQuery.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }
        
        
        /// <summary>
        /// �����¼�б�ѡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwNursingRec_SelectedIndexChanged( object sender, EventArgs e )
        {
            try
            {
                if (lvwNursingRec.SelectedIndices.Count == 0)
                {
                    return;
                }
                
                // ��ȡ��ǰ��¼
                DataRow dr = dsNursing.Tables[0].Rows[lvwNursingRec.SelectedIndices[0]];
                
                // ��ʾ��ǰ��¼
                DateTime dtRec = (DateTime)dr["TIME_POINT"];
                dtPickerDate.Value = dtRec;                             // ����
                dtPickerTime.Value = dtRec;                             // ʱ��
                
                cmbClass.SelectedIndex = nursingCom.GetNursingItemAttribute(dr["VITAL_CODE"].ToString());
                
                cmbItem.Text = dr["VITAL_SIGNS"].ToString();            // ��Ŀ����
                txtValue.Text = dr["VITAL_SIGNS_CVALUES"].ToString();   // ֵ
                lblUnit.Text = "(" + dr["UNITS"].ToString() + ")";      // ��λ
                
                lblUnit.Visible = (lblUnit.Text.Length > 2);
                
                blnRights = (blnSuperUser || GVars.User.Name.Equals(dr["NURSE"].ToString()));
                                
                // ����Ȩ�����ý���״̬
                this.btnSave.Enabled = blnRights;
                this.btnDelete.Enabled = blnRights;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// �������ı��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbClass_SelectedIndexChanged( object sender, EventArgs e )
        {
            try
            {                
                cmbItem.Items.Clear();
                
                drNursingItem = dsNursingItemDict.Tables[0].Select("ATTRIBUTE = " + SqlConvert.SqlConvert(cmbClass.SelectedIndex.ToString()));
                
                for(int i = 0; i < drNursingItem.Length; i++)
                {
                    DataRow dr = drNursingItem[i];
                    
                    cmbItem.Items.Add(dr["VITAL_SIGNS"].ToString());                
                }
                                
                cmbItem.Text = string.Empty;
                
                txtValue.Enabled = true;
                if (cmbClass.SelectedIndex == cmbClass.Items.Count - 1)
                {
                    txtValue.Enabled = false;
                    txtValue.Text   = string.Empty;
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
                
        private void cmbClass_Filter_SelectedIndexChanged( object sender, EventArgs e )
        {
            try
            {                
                cmbItem_Filter.Items.Clear();
                
                if (cmbClass_Filter.SelectedIndex < 1)
                {
                    return;
                }
                
                string attribute = (cmbClass_Filter.SelectedIndex - 1).ToString();
                drNursingItem = dsNursingItemDict.Tables[0].Select("ATTRIBUTE = " + SqlConvert.SqlConvert(attribute));
                                                
                for(int i = 0; i < drNursingItem.Length; i++)
                {
                    DataRow dr = drNursingItem[i];
                    
                    cmbItem_Filter.Items.Add(dr["VITAL_SIGNS"].ToString());                
                }
                
                cmbItem_Filter.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        
        /// <summary>
        /// ������Ŀ�ı��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbItem_SelectedIndexChanged( object sender, EventArgs e )
        {
            try
            {
                if (cmbItem.SelectedIndex > -1)
                {
                    string unit = drNursingItem[cmbItem.SelectedIndex]["UNIT"].ToString();
                                                        
                    lblUnit.Text = "(" + unit + ")";
                    lblUnit.Visible = (unit.Length > 0);
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// ��ť[����]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click( object sender, EventArgs e )
        {
            try
            {
                this.cmbItem.SelectedIndex = -1;
                this.txtValue.Text = string.Empty;
                this.lblUnit.Visible = false;
                
                // ����Ȩ�����ý���״̬
                blnRights = true;
                
                this.btnSave.Enabled = blnRights;
                this.btnDelete.Enabled = blnRights;
                
                this.dtPickerTime.Focus();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// ��ť[ɾ��]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click( object sender, EventArgs e )
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnDelete.Enabled = false;
                
                // �������
                if (chkDisp() == false)
                {
                    GVars.Msg.Show();
                    return;
                }
                
                // ȷ��ɾ��
                if (GVars.Msg.Show("Q00019") != DialogResult.Yes)
                {
                    return;
                }
                   
                // ��������
                deleteRec();
                
                // ������ʾ����
                btnQuery_Click(sender, e);
                
                // �������
                this.cmbItem.SelectedIndex = -1;
                this.txtValue.Text = string.Empty;
                this.lblUnit.Visible = false;
            }            
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                btnDelete.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        
        /// <summary>
        /// ��ť[����]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click( object sender, EventArgs e )
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                // �������
                if (chkDisp() == false)
                {
                    GVars.Msg.Show();
                    return;
                }
                
                // ��������
                saveData();
                
                // ������ʾ����
                btnQuery_Click(sender, e);
                
                // ��ʾ
                // GVars.Msg.Show("I00004", "����");   // {0}�ɹ�!
                this.btnSave.Enabled = false;

                // �������
                this.cmbItem.SelectedIndex = -1;
                this.txtValue.Text = string.Empty;
                this.lblUnit.Visible = false;
                
                this.dtPickerTime.Focus();
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
            nursingCom = new NursingDbI(GVars.OracleAccess, GVars.SqlserverAccess);
            
            dsNursingItemDict = nursingCom.GetNursingItemDict_Sql(GVars.User.DeptCode, -1); // ������Ŀ�ֵ�

            blnSuperUser = GVars.UserDbI.ChkSuperUser(GVars.User.ID, RIGHT_ID);
        }
        
        
        /// <summary>
        /// ��ʼ��������ʾ
        /// </summary>
        private void initDisp()
        {
            // ��ղ�����Ϣ
            this.lblPatientID.Text = string.Empty;                      // ���˱�ʶ��
            this.lblVisitID.Text = string.Empty;                        // ����סԺ��ʶ
            this.lblPatientName.Text = string.Empty;                    // ��������
            this.lblGender.Text = string.Empty;                         // �����Ա�
            this.lblDeptName.Text = string.Empty;                       // �Ʊ�
            this.lblInpNo.Text = string.Empty;                          // סԺ��
            this.lblInpDate.Text = string.Empty;                        // ��Ժ����
            
            // �����Ŀ�б�
            lvwNursingRec.Items.Clear();
            
            // �����Ŀ����
            this.cmbItem.SelectedIndex = -1;
            this.txtValue.Text = string.Empty;
            this.lblUnit.Visible = false;
        }
        
        
        /// <summary>
        /// ��ʾ���˵Ļ�����Ϣ
        /// </summary>
        private void showPatientInfo()
        { 
            // ��ս���
            this.txtBedLabel.Text = string.Empty;                       // ���˴����
            this.lblPatientID.Text = string.Empty;                      // ���˱�ʶ��
            this.lblVisitID.Text = string.Empty;                        // ����סԺ��ʶ
            this.lblPatientName.Text = string.Empty;                    // ��������
            this.lblGender.Text = string.Empty;                         // �����Ա�
            this.lblDeptName.Text = string.Empty;                       // �Ʊ�
            this.lblInpNo.Text = string.Empty;                          // סԺ��
            this.lblInpDate.Text = string.Empty;                        // ��Ժ����
            
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
            
            this.txtBedLabel.Text = dr["BED_LABEL"].ToString();                                 // ���˴����
            this.lblPatientID.Text = dr["PATIENT_ID"].ToString();                               // ���˱�ʶ��
            this.lblVisitID.Text = dr["VISIT_ID"].ToString();                                   // ����סԺ��ʶ
            this.lblPatientName.Text = dr["NAME"].ToString();                                   // ��������
            this.lblGender.Text = dr["SEX"].ToString();                                         // �����Ա�
            this.lblDeptName.Text = dr["DEPT_NAME"].ToString();                                 // �Ʊ�
            this.lblInpNo.Text = dr["INP_NO"].ToString();                                       // סԺ��
            this.lblInpDate.Text = DataType.GetDateTimeShort(dr["ADMISSION_DATE_TIME"].ToString());   // ��Ժ����
            
            patientId   = dr["PATIENT_ID"].ToString();
            visitId     = dr["VISIT_ID"].ToString();
        }


        /// <summary>
        /// ��ʾ���˵Ļ����¼
        /// </summary>
        private void showNursingData()
        {
            try
            {
                lvwNursingRec.BeginUpdate();
                lvwNursingRec.Items.Clear();
                
                if (dsNursing == null || dsNursing.Tables.Count == 0 || dsNursing.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                
                ListViewItem    item    = null;
                DateTime        dtRec;
                
                string          recDate0 = string.Empty;
                string          recTime0 = string.Empty;
                string          recDate = string.Empty;
                string          recTime = string.Empty;
                
                foreach(DataRow dr in dsNursing.Tables[0].Rows)
                {
                    dtRec   = (DateTime)dr["TIME_POINT"];
                    
                    recDate = dtRec.ToString(ComConst.FMT_DATE.SHORT);                    
                    recTime = dtRec.ToString(ComConst.FMT_DATE.TIME_SHORT);
                    
                    if (recDate0.Equals(recDate) == true)
                    {
                        item = new ListViewItem();                                              // ����
                        
                        if (recTime0.Equals(recTime) == true)
                        {
                            item.SubItems.Add(string.Empty);
                        }
                        else
                        {
                            item.SubItems.Add(recTime);
                        }
                    }
                    else
                    {
                        item = new ListViewItem(recDate);                                       // ����
                        item.SubItems.Add(recTime);                                             // ʱ��                        
                    }
                    
                    recTime0 = recTime;
                    recDate0 = recDate;
                    
                    // item.SubItems.Add(dr["CLASS_NAME"].ToString().Substring(0, 2));             // ���
                    item.SubItems.Add(dr["VITAL_SIGNS"].ToString());                            // ����
                    item.SubItems.Add(dr["VITAL_SIGNS_CVALUES"].ToString());                    // ֵ
                    item.SubItems.Add(dr["UNITS"].ToString());                                  // ��λ
                    item.SubItems.Add(dr["NURSE"].ToString());                                  // ��¼��
                    
                    lvwNursingRec.Items.Add(item);
                }
            }
            finally
            {
                if (lvwNursingRec.Items.Count > 1)
                {
                    lvwNursingRec.TopItem = lvwNursingRec.Items[lvwNursingRec.Items.Count - 1];
                }
                
                lvwNursingRec.EndUpdate();
            }
        }        
        
        
        /// <summary>
        /// �����������Ƿ�����
        /// </summary>
        /// <returns></returns>
        private bool chkDisp()
        {
            if (patientId.Length == 0)
            {
                GVars.Msg.MsgId = "E00011";     // E00011	����ȷ������!
                GVars.Msg.ErrorSrc = txtBedLabel;

                return false;
            }

            if (this.cmbClass.SelectedIndex == -1)
            {
                GVars.Msg.MsgId = "E00004";     // ��������{0}!
                GVars.Msg.MsgContent.Add("���");
                GVars.Msg.ErrorSrc = cmbClass;
                
                return false;
            }
            
            if (this.cmbItem.SelectedIndex == -1)
            {
                GVars.Msg.MsgId = "E00004";     // ��������{0}!
                GVars.Msg.MsgContent.Add("��Ŀ");
                GVars.Msg.ErrorSrc = cmbItem;            
                return false;
            }
            
            if (this.txtValue.Text.Trim().Length == 0 && this.txtValue.Enabled == true)
            {
                GVars.Msg.MsgId = "E00004";     // ��������{0}!
                GVars.Msg.MsgContent.Add("ֵ");
                GVars.Msg.ErrorSrc = txtValue;
                
                return false;
            }

            // ʱ�䲻�ܴ��ڵ�ǰϵͳʱ��
            DateTime dt = dtPickerDate.Value.Date;
            DateTime dtTime = dtPickerTime.Value;

            dt = dt.AddHours(dtTime.Hour).AddMinutes(dtTime.Minute).AddSeconds(dtTime.Second);

            if (dt.CompareTo(GVars.OracleAccess.GetSysDate()) > 0)
            {
                GVars.Msg.MsgId = "E00057";     // ���ڲ��ܴ��ڵ�ǰϵͳ����!
                GVars.Msg.ErrorSrc = dtPickerDate;

                return false;
            }

            return true;
        }
        
        
        /// <summary>
        /// ��������
        /// </summary>
        private void saveData()
        {
            SqlConvert.DbField[] arr = new SqlConvert.DbField[11];
            
            string tableName = "VITAL_SIGNS_REC";
            string recDate = dtPickerDate.Value.ToString(ComConst.FMT_DATE.SHORT);
            string recTime = dtPickerTime.Value.ToString(ComConst.FMT_DATE.TIME);
            DataRow drItem = drNursingItem[cmbItem.SelectedIndex];
                        
            int i = 0;
            arr[i++] = SqlConvert.GetDbField_Ora("PATIENT_ID", patientId, SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("VISIT_ID", visitId, SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("CLASS_CODE", drItem["CLASS_CODE"].ToString(), SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("VITAL_CODE", drItem["VITAL_CODE"].ToString(), SqlManager.FIELD_TYPE.STR);            
            arr[i++] = SqlConvert.GetDbField_Ora("RECORDING_DATE", recDate, SqlManager.FIELD_TYPE.DATE);
            arr[i++] = SqlConvert.GetDbField_Ora("TIME_POINT", recDate + ComConst.STR.BLANK + recTime, SqlManager.FIELD_TYPE.DATE);
            
            arr[i++] = SqlConvert.GetDbField_Ora("VITAL_SIGNS", cmbItem.Text, SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("UNITS", drItem["UNIT"].ToString(), SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("VITAL_SIGNS_CVALUES", txtValue.Text.Trim(), SqlManager.FIELD_TYPE.STR);            
            arr[i++] = SqlConvert.GetDbField_Ora("WARD_CODE", GVars.User.DeptCode, SqlManager.FIELD_TYPE.STR);            
            arr[i++] = SqlConvert.GetDbField_Ora("NURSE", GVars.User.Name, SqlManager.FIELD_TYPE.STR);
            
            // �жϼ�¼�Ƿ����
            string where = SqlConvert.getFieldValuePairAssert(arr, 6);
            string sql = "SELECT PATIENT_ID FROM " + tableName + SqlConvert.getSQLWhere(where);
            
            // �����¼����
            if (GVars.OracleAccess.SelectValue(sql) == true)
            {
                sql = SqlConvert.GetSqlUpdate(tableName, arr, 11, where);
            }
            // �����¼������
            else
            {
                sql = SqlConvert.GetSqlInsert(tableName, arr, 11);
            }
            
            GVars.OracleAccess.ExecuteNoQuery(sql);
        }
        
                    
        /// <summary>
        /// ��������
        /// </summary>
        private void deleteRec()
        {
            SqlConvert.DbField[] arr = new SqlConvert.DbField[6];
            
            string tableName = "VITAL_SIGNS_REC";
            string recDate = dtPickerDate.Value.ToString(ComConst.FMT_DATE.SHORT);
            string recTime = dtPickerTime.Value.ToString(ComConst.FMT_DATE.TIME);
            DataRow drItem = drNursingItem[cmbItem.SelectedIndex];
                        
            int i = 0;
            arr[i++] = SqlConvert.GetDbField_Ora("PATIENT_ID", patientId, SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("VISIT_ID", visitId, SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("CLASS_CODE", drItem["CLASS_CODE"].ToString(), SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("VITAL_CODE", drItem["VITAL_CODE"].ToString(), SqlManager.FIELD_TYPE.STR);            
            arr[i++] = SqlConvert.GetDbField_Ora("RECORDING_DATE", recDate, SqlManager.FIELD_TYPE.DATE);
            arr[i++] = SqlConvert.GetDbField_Ora("TIME_POINT", recDate + ComConst.STR.BLANK + recTime, SqlManager.FIELD_TYPE.DATE);
            
            // �жϼ�¼�Ƿ����
            string where = SqlConvert.getFieldValuePairAssert(arr, 6);
            string sql = "SELECT PATIENT_ID FROM " + tableName + SqlConvert.getSQLWhere(where);
            
            // �����¼����
            if (GVars.OracleAccess.SelectValue(sql) == true)
            {
                sql = "DELETE FROM " + tableName + SqlConvert.getSQLWhere(where);
                GVars.OracleAccess.ExecuteNoQuery(sql);
                
                GVars.Msg.Show("I00004", "ɾ��");     // {0}�ɹ�!
            }
            // �����¼������
            else
            {
                GVars.Msg.Show("E00055", "ɾ��");     // {0}ʧ��!
            }
        }
        #endregion
    }
}