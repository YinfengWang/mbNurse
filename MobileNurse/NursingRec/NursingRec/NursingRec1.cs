using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HISPlus.Controller;
using HISPlus.Models;
using HISPlus.UserControls;
using SqlConvert = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class NursingRec1 : FormDo, IBasePatient, INursingRec
    {
        #region �������
        private NursingDbI nursingCom;

        private DataSet dsNursing = null;                     // ������Ϣ
        private DataRow[] drNursing = null;
        private DataSet dsNursingItemDict = null;                     // ������Ŀ�ֵ�
        private DataRow[] drNursingItem = null;
        private string vitalCodePreInput = string.Empty;             // ������ǰ¼�����Ŀ����

        private string patientId = string.Empty;             // ����ID��
        private string visitId = string.Empty;             // ���ξ������
        private bool blnAdd = false;

        private DateTime dtTimePoint = DataType.DateTime_Null();

        private const string RIGHT_EDIT = "2";                      //  �༭����¼���¼��Ȩ��

        private bool IsBind = false;
        /// <summary>
        /// ������
        /// </summary>
        private NursingRecControllor Controllor { get; set; }

        #endregion


        public NursingRec1()
        {
            _id = "00015";
            _guid = "FBA89860-BE5C-4c9e-962A-502768A48FEF";

            _right = @"0:�鿴
                        1:¼��
                        2:�޸�";

            InitializeComponent();

            this.KeyPress += new KeyPressEventHandler(NursingRec_KeyPress);

            this.txtValue.GotFocus += new EventHandler(imeCtrl_GotFocus);
            this.cmbClass.GotFocus += new EventHandler(imeCtrl_GotFocus);
            this.cmbItem.GotFocus += new EventHandler(imeCtrl_GotFocus);

            this.Controllor = new NursingRecControllor(this);


        }


        #region �����¼�
        private void NursingRec_Load(object sender, EventArgs e)
        {
            try
            {
                initFrmVal();
                initDisp();

                return;

            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void Bind()
        {
            this.SetBinding(txtValue, this.Controllor.CurrentModel, "VitalSignsCvalues");
            this.SetBinding(dtPickerDate, "Value", Controllor.CurrentModel, "Id.TimePoint");
            this.SetBinding(dtPickerTime, "Value", Controllor.CurrentModel, "Id.TimePoint");

            this.SetBinding(cmbItem, "SelectedValue", Controllor.CurrentModel, "VitalSigns");
            this.SetBinding(lblUnit, "Text", Controllor.CurrentModel, "Units");

            this.SetBinding(txtMemo, "Text", Controllor.CurrentModel, "Memo");
           
            //if (!IsBind)
            //{
            //    IsBind = true;

            //    //txtValue.DataBindings.Add("Text",Model,"VitalSignsCvalues",false,DataSourceUpdateMode.Never);
            //    txtValue.DataBindings.Add("Text", this.Controllor.CurrentModel, "VitalSignsCvalues");
            //    dtPickerDate.DataBindings.Add("Value", Controllor.CurrentModel, "Id.TimePoint");
            //    dtPickerTime.DataBindings.Add("Value", Controllor.CurrentModel, "Id.TimePoint");

            //    cmbItem.DataBindings.Add("SelectedValue", Controllor.CurrentModel, "VitalSigns");
            //    lblUnit.DataBindings.Add("Text", Controllor.CurrentModel, "Units");

            //    txtMemo.DataBindings.Add("Text", Controllor.CurrentModel, "Memo");
            //}

        }

        /// <summary>
        /// �����KeyPress�¼�
        /// </summary>
        /// <remarks>
        /// ��Ҫ����س����Tab
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void NursingRec_KeyPress(object sender, KeyPressEventArgs e)
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

        void IBasePatient.Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
            if (PatientChanged != null)
                PatientChanged(sender, new PatientQueryArgs(dtRngStart.Value, dtRngEnd.Value, cmbItem_Filter.Text));

            patientId = e.PatientId;
            visitId = e.VisitId;

            //// ��ȡ�����¼
            //dsNursing = nursingCom.GetNursingData(patientId, visitId, dtRngStart.Value, dtRngEnd.Value, cmbItem_Filter.Text);

            //// ��ʾ�����¼
            //showNursingData();
        }


        /// <summary>
        /// ��ť[��ѯ]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnQuery.Enabled = false;

                if (PatientChanged != null)
                    PatientChanged(sender, new PatientQueryArgs(dtRngStart.Value, dtRngEnd.Value, cmbItem_Filter.Text));

                // ��ȡ�����¼
                //dsNursing = nursingCom.GetNursingData(patientId, visitId, dtRngStart.Value, dtRngEnd.Value, cmbItem_Filter.Text);

                // ��ʾ�����¼
                //showNursingData();
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


        /// <summary>
        /// �������ı��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbItem.Items.Clear();

                drNursingItem = dsNursingItemDict.Tables[0].Select(
                                "ATTRIBUTE = " + SqlConvert.SqlConvert(cmbClass.SelectedIndex.ToString()),
                                "SHOW_ORDER");

                for (int i = 0; i < drNursingItem.Length; i++)
                {
                    DataRow dr = drNursingItem[i];

                    cmbItem.Items.Add(dr["VITAL_SIGNS"].ToString());
                }

                //cmbItem.Text1 = string.Empty;

                txtValue.Enabled = true;
                txtValue.Focus();
                //if (cmbClass.SelectedIndex == cmbClass.Items.Count - 1)
                //{
                //    txtValue.Enabled = false;
                //    txtValue.Text1   = string.Empty;
                //}
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void cmbClass_Filter_SelectedIndexChanged(object sender, EventArgs e)
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

                for (int i = 0; i < drNursingItem.Length; i++)
                {
                    DataRow dr = drNursingItem[i];

                    cmbItem_Filter.Items.Add(dr["VITAL_SIGNS"].ToString());
                }

                cmbItem_Filter.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// ������Ŀ�ı��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbItem.SelectedIndex > -1)
                {
                    string unit = drNursingItem[cmbItem.SelectedIndex]["UNIT"].ToString();

                    lblUnit.Text = "(" + unit + ")";
                    lblUnit.Visible = (unit.Length > 0);

                    this.btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// ֵ�ı��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = true;
        }


        /// <summary>
        /// ��ť[����]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                dtTimePoint = DataType.DateTime_Null();

                initDisp_Edit(true);
                blnAdd = true;

                this.cmbItem.SelectedIndex = -1;
                this.txtValue.Text = string.Empty;
                this.lblUnit.Visible = false;
                this.txtMemo.Text = string.Empty;

                // ����Ȩ�����ý���״̬
                this.btnSave.Enabled = true;
                this.btnDelete.Enabled = false;

                this.dtPickerTime.Focus();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// ��ť[ɾ��]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
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

                // ȷ��ɾ�� ��ȷ��Ҫɾ����ǰ��¼��?
                if (GVars.Msg.Show("Q0005") != DialogResult.Yes)
                {
                    return;
                }

                // ��������
                deleteRec();

                dtTimePoint = DataType.DateTime_Null();

                // ������ʾ����
                btnQuery_Click(sender, e);

                // �������
                this.cmbItem.SelectedIndex = -1;
                this.txtValue.Text = string.Empty;
                this.lblUnit.Visible = false;
                this.txtMemo.Text = string.Empty;

                initDisp_Edit(false);
            }
            catch (Exception ex)
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
        private void btnSave_Click(object sender, EventArgs e)
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
                if (saveData() == false)
                {
                    GVars.Msg.Show();
                    return;
                }

                // ������ʾ����
                initDisp_Edit(false);

                btnQuery_Click(sender, e);

                // ��ʾ
                // GVars.Msg.Show("I00004", "����");   // {0}�ɹ�!
                this.btnSave.Enabled = false;

                // �������
                //this.cmbItem.SelectedIndex = -1;
                //this.txtValue.Text1 = string.Empty;
                //this.lblUnit.Visible = false;

                this.dtPickerTime.Focus();
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
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
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
            nursingCom = new NursingDbI(GVars.OracleAccess);

            dsNursingItemDict = nursingCom.GetNursingItemDictMobile(GVars.User.DeptCode); // ������Ŀ�ֵ�

            getParameters();

            _userRight = GVars.User.GetUserFrmRight(_id);

            GVars.App.Now = GVars.OracleAccess.GetSysDate();
            // blnSuperUser = userCom.ChkSuperUser(GVars.User.ID, RIGHT_ID);
        }


        /// <summary>
        /// ��ʼ��������ʾ
        /// </summary>
        private void initDisp()
        {
            this.cmbClass_Filter.Items.AddRange(new object[] {
            "",
            "��������",
            "����",
            "����",
            "��������",
            "�����¼�"});

            this.cmbClass.Items.AddRange(new object[] {
            "��������",
            "����",
            "����",
            "��������",
            "�����¼�"});

            // �����Ŀ����
            this.cmbItem.SelectedIndex = -1;
            this.txtValue.Text = string.Empty;
            this.lblUnit.Visible = false;

            this.txtMemo.Text = string.Empty;

            initDisp_Edit(false);

            dtPickerDate.Value = GVars.App.Now;
            dtPickerTime.Value = GVars.App.Now;

            //ucGridView1.Add("����", "TIME_POINT", ComConst.FMT_DATE.SHORT, ColumnStatus.Unique);
            //ucGridView1.Add("ʱ��", "TIME_POINT", ComConst.FMT_DATE.TIME_SHORT, ColumnStatus.Unique);
            //ucGridView1.Add("����", "VITAL_SIGNS");
            //ucGridView1.Add("ֵ", "VITAL_SIGNS_CVALUES");
            //ucGridView1.Add("��λ", "UNITS");
            //ucGridView1.Add("��¼��", "NURSE");
            //ucGridView1.Add("��ע", "MEMO"); 

            ucGridView1.Add("����", "Id.TimePoint", ComConst.FMT_DATE.SHORT, ColumnStatus.Unique);
            ucGridView1.Add("ʱ��", "Id.TimePoint", ComConst.FMT_DATE.TIME_SHORT, ColumnStatus.Unique);
            ucGridView1.Add("����", "VitalSigns");
            ucGridView1.Add("ֵ", "VitalSignsCvalues");
            ucGridView1.Add("��λ", "Units");
            ucGridView1.Add("��¼��", "Nurse");
            ucGridView1.Add("��ע", "Memo");
            ucGridView1.Init();
        }


        private void initDisp_Edit(bool editable)
        {
            this.dtPickerDate.Enabled = editable;
            this.dtPickerTime.Enabled = editable;
            this.cmbClass.Enabled = editable;
            this.cmbItem.Enabled = editable;
            this.txtValue.Enabled = editable;
        }


        /// <summary>
        /// ��ʾ���˵Ļ����¼
        /// </summary>
        private void showNursingData()
        {
            ucGridView1.DataSource = dsNursing.Tables[0];
        }


        /// <summary>
        ///  �����������Ƿ�����
        /// </summary>
        /// <returns></returns>
        private bool chkDisp()
        {
            if (this.cmbClass.SelectedIndex == -1)
            {
                GVars.Msg.MsgId = "E0012";     // ��������{0}!
                GVars.Msg.MsgContent.Add("���");
                GVars.Msg.ErrorSrc = cmbClass;

                return false;
            }

            if (this.cmbItem.SelectedIndex == -1)
            {
                GVars.Msg.MsgId = "E0012";     // ��������{0}!
                GVars.Msg.MsgContent.Add("��Ŀ");
                GVars.Msg.ErrorSrc = cmbItem;
                return false;
            }

            if (this.cmbClass.SelectedIndex < cmbClass.Items.Count - 1 &&
                this.txtValue.Text.Trim().Length == 0 && this.txtValue.Enabled == true)
            {
                GVars.Msg.MsgId = "E0012";     // ��������{0}!
                GVars.Msg.MsgContent.Add("ֵ");
                GVars.Msg.ErrorSrc = txtValue;

                return false;
            }

            // ʱ�䲻�ܴ��ڵ�ǰϵͳʱ��
            DateTime dt = dtPickerDate.Value.Date;
            DateTime dtTime = dtPickerTime.Value;

            dt = dt.AddHours(dtTime.Hour).AddMinutes(dtTime.Minute).AddSeconds(dtTime.Second);

            DataRow drItem = drNursingItem[cmbItem.SelectedIndex];

            if (vitalCodePreInput.IndexOf(drItem["VITAL_CODE"].ToString()) >= 0)
            //if (cmbItem.Text1.IndexOf("��Ժ") >= 0 || cmbItem.Text1.IndexOf("����") >= 0)
            {
                return true;
            }

            GVars.App.Now = GVars.OracleAccess.GetSysDate();

            if (dt.CompareTo(GVars.App.Now) > 0)
            {
                GVars.Msg.MsgId = "E0011";              // ���ڲ��ܴ��ڵ�ǰϵͳ����!
                GVars.Msg.ErrorSrc = dtPickerDate;

                return false;
            }

            return true;
        }


        /// <summary>
        /// ��������
        /// </summary>
        private bool saveData()
        {
            const int FIELD_COUNT = 12;
            const int KEY_COUNT = 6;

            SqlConvert.DbField[] arr = new SqlConvert.DbField[FIELD_COUNT];

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
            arr[i++] = SqlConvert.GetDbField_Ora("MEMO", txtMemo.Text.Trim(), SqlManager.FIELD_TYPE.STR);

            // �жϼ�¼�Ƿ����
            string where = SqlConvert.getFieldValuePairAssert(arr, KEY_COUNT);
            string sql = "SELECT PATIENT_ID FROM " + tableName + SqlConvert.getSQLWhere(where);

            // �����¼����
            //�����¼�Ѿ����ڣ���Ҫ������LB��
            if (GVars.OracleAccess.SelectValue(sql) == true && blnAdd)
            {
                GVars.Msg.MsgId = "E0013";              // �����ļ�¼�Ѿ�����, ���Ҫ�޸��Ѵ��ڵļ�¼,������ɾ��������.
                return false;
            }


            //Ϊ�޸�(LB)
            if (blnAdd == false)
            {
                arr[5] = SqlConvert.GetDbField_Ora("TIME_POINT", dtTimePoint.ToString(ComConst.FMT_DATE.LONG), SqlManager.FIELD_TYPE.DATE);
                where = SqlConvert.getFieldValuePairAssert(arr, KEY_COUNT);

                arr[5] = SqlConvert.GetDbField_Ora("TIME_POINT", recDate + ComConst.STR.BLANK + recTime, SqlManager.FIELD_TYPE.DATE);
                sql = SqlConvert.GetSqlUpdate(tableName, arr, FIELD_COUNT, where);
            }
            // �����¼������
            else
            {
                sql = SqlConvert.GetSqlInsert(tableName, arr, FIELD_COUNT);
            }

            GVars.OracleAccess.ExecuteNoQuery(sql);

            return true;
        }


        /// <summary>
        /// ��������
        /// </summary>
        private void deleteRec()
        {
            SqlConvert.DbField[] arr = new SqlConvert.DbField[5];

            string tableName = "VITAL_SIGNS_REC";
            string recDate = dtPickerDate.Value.ToString(ComConst.FMT_DATE.SHORT);
            string recTime = dtPickerTime.Value.ToString(ComConst.FMT_DATE.TIME);
            DataRow drItem = drNursingItem[cmbItem.SelectedIndex];

            int i = 0;
            arr[i++] = SqlConvert.GetDbField_Ora("PATIENT_ID", patientId, SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("VISIT_ID", visitId, SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("CLASS_CODE", drItem["CLASS_CODE"].ToString(), SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("VITAL_CODE", drItem["VITAL_CODE"].ToString(), SqlManager.FIELD_TYPE.STR);
            //arr[i++] = SqlConvert.GetDbField_Ora("RECORDING_DATE", recDate, SqlManager.FIELD_TYPE.DATE);
            arr[i++] = SqlConvert.GetDbField_Ora("TIME_POINT", recDate + ComConst.STR.BLANK + recTime, SqlManager.FIELD_TYPE.DATE);

            // �жϼ�¼�Ƿ����
            string where = SqlConvert.getFieldValuePairAssert(arr, 5);
            string sql = "SELECT PATIENT_ID FROM " + tableName + SqlConvert.getSQLWhere(where);

            // �����¼����
            if (GVars.OracleAccess.SelectValue(sql) == true)
            {
                sql = "DELETE FROM " + tableName + SqlConvert.getSQLWhere(where);
                GVars.OracleAccess.ExecuteNoQuery(sql);

                GVars.Msg.Show("I0009", "ɾ��");     // {0}�ɹ�!
            }
            // �����¼������
            else
            {
                MessageBox.Show(sql);
                // GVars.Msg.Show("E0005", "ɾ��" + ComConst.STR.CRLF + sql);     // {0}ʧ��!
            }
        }


        /// <summary>
        /// ��ȡ����
        /// </summary>
        private void getParameters()
        {
            if (GVars.App.AppParameters == null)
            {
                GVars.App.ReloadParameters();
            }

            vitalCodePreInput = GVars.App.GetParameters(GVars.User.DeptCode, GVars.User.ID, "VITAL_CODE_PRE_INPUT");
        }
        #endregion

        private void ucGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (ucGridView1.SelectRowsCount == 0)
                {
                    return;
                }

                this.Controllor.SetCurrentModel(ucGridView1.GetSelectRow());

                cmbClass.SelectedIndex = nursingCom.GetNursingItemAttribute(this.Controllor.CurrentModel.VitalSigns, GVars.User.DeptCode);

                lblUnit.Visible = (lblUnit.Text.Length > 2);

                bool enabled = (_userRight.IndexOf(RIGHT_EDIT) >= 0 || GVars.User.Name.Equals(Controllor.CurrentModel.Nurse));

                Bind();

                // ����Ȩ�����ý���״̬
                initDisp_Edit(false);

                blnAdd = false;

                this.dtPickerTime.Enabled = enabled;
                this.txtValue.Enabled = enabled;
                this.btnSave.Enabled = enabled;
                this.btnDelete.Enabled = enabled;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        public event EventHandler<PatientQueryArgs> PatientChanged;
        public void BindData(IEnumerable<VitalSignsRec> dataSource)
        {
            ucGridView1.DataSource = dataSource;
        }


        void IBasePatient.Patient_ListRefresh(object sender, PatientEventArgs e)
        {
            
        }
    }
}