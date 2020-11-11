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
    public partial class frmNurseRecord : Form
    {
        #region �������
        private PatientDbI patientCom;                                  // ���˹�ͨ
        private DataSet dsPatient = null;                               // ������Ϣ
        private string patientId = string.Empty;                        // ���˱�ʶ��
        private string visitId = string.Empty;                          // סԺ��ʶ
        private DataSet dsVitalSignRec = null;                          // ������¼
        private DataSet dsNursingRecord = null;                         // �����¼
        private DataTable dtNursingData = null;                         // ��Ӧ�ڽ���ļ�¼��(����Ļ�ȡ)
        private DataTable dtNursingDataNew = null;                      // ��Ӧ�ڽ���ļ�¼��(������Ļ�ȡ)
        private int ROW_HEIGHT = 0;                                     // �еĳ�ʼ�߶�
        private const int OFF_SET = 30;                                 // ƫ����
        private int selectRow = -1;                                     // ���е��к�
        private HISPlus.ExcelAccess excelAccess = null;                 // Excel��ӡ���ʽӿ�
        private Graphics grfx = null;
        private bool scrollValue = true;                                // ��ֱ������������ֵ
        #endregion

        public frmNurseRecord()
        {
            InitializeComponent();

            this.Load += new EventHandler(frmNurseRecord_Load);
            this.txtBedLabel.KeyDown += new KeyEventHandler(txtBedLabel_KeyDown);
            this.txtBedLabel.GotFocus += new EventHandler(imeCtrl_GotFocus);

            this.grdRecord.EnterCell += new EventHandler(grdRecord_EnterCell);
            this.grdRecord.LeaveCell += new EventHandler(txtData_LostFocus);
            //this.txtData.GotFocus += new EventHandler(txtData_GotFocus);
            this.txtData.LostFocus += new EventHandler(txtData_LostFocus);
            this.txtData.KeyDown += new KeyEventHandler(txtData_KeyDown);
            this.grdRecord.Scroll += new EventHandler(grdRecord_Scroll);
        }


        #region �����¼�
        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmNurseRecord_Load(object sender, EventArgs e)
        {
            try
            {
                grfx = this.CreateGraphics();
                ROW_HEIGHT = (int)(grfx.MeasureString("ҽ", this.grdRecord.Font).Height) * 15;         // ����еĳ�ʼ�߶�
                
                patientCom = new PatientDbI(GVars.OracleAccess);
                
                initDisp();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// ����Żس��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtBedLabel_KeyDown(object sender, KeyEventArgs e)
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

                // ��ȡ������Ϣ
                dsPatient = patientCom.GetInpPatientInfo_FromBedLabel(txtBedLabel.Text.Trim(), GVars.User.DeptCode);
                if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
                {
                    GVars.Msg.Show("W00005");                           // �ò��˲�����!	
                    return;
                }
                
                // ��ʾ������Ϣ
                showPatientInfo();
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
        /// �ɱ༭��textbox�س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtData_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // �������
                if (e.KeyCode != Keys.Return)
                {
                    return;
                }

                // ��ȡ��ѯ����
                if (txtData.Text.Trim().Length == 0)
                {
                    return;
                }

                System.Drawing.Font strFont = new Font("Arial", 11);
                int rowNum = getRowNum(txtData.Text.Trim(), strFont, txtData.Width);
                float heightUnit = grfx.MeasureString("ҽ", strFont).Height;
                float widthUnit = grfx.MeasureString("ҽ", strFont).Width;

                if ((txtData.Height - (rowNum * heightUnit)) < heightUnit)
                {
                    txtData.Height += ROW_HEIGHT / 15;
                    grdRecord.set_RowHeight(selectRow, txtData.Height * 15);
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// �ɱ༭��textbox���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtData_GotFocus(object sender, EventArgs e)
        {
            txtData.Visible = true;
        }


        /// <summary>
        /// �ɱ༭��textboxʧ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtData_LostFocus(object sender, EventArgs e)
        {
            if (txtData.Visible == true)
            {
                int rowNum = getRowNum(txtData.Text.Trim(), this.grdRecord.Font, grdRecord.get_ColWidth(13) / 15);

                grdRecord.set_RowHeight(selectRow, rowNum * ROW_HEIGHT);
                grdRecord.set_TextMatrix(selectRow, 13, txtData.Text);
                txtData.Visible = false;
            }
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

                dsVitalSignRec = getPatientVitalSignRec(patientId, visitId);
                dsNursingRecord = getPatientNursingRecord(patientId, visitId);
                mergeNursingData(ref dtNursingData);
                showPatientVitalSignRec();
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
        /// ��ť[��ӡ](��ӡʱ����ѡ���ѯһ��ļ�¼)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // ��ȡ���ؼ��ϵ�����
                getGrdNursingData(ref dtNursingDataNew);

                // �Ƚ��Ƿ������ݱ仯,���в���δ����,������ʾ
                for (int i = 0; i < dtNursingData.Rows.Count; i++)
                {
                    string nursingData = dtNursingData.Rows[i]["NURSING"].ToString().Trim();
                    string nursingDataNew = dtNursingDataNew.Rows[i]["NURSING"].ToString().Trim();

                    if (nursingData.Equals(nursingDataNew) == false)
                    {
                        GVars.Msg.Show("ID010");         // ���ݻ�δ����,���ȱ�����ٴ�ӡ!
                        return;
                    }
                }

                if (dtRngStart.Value.ToString(ComConst.FMT_DATE.SHORT) != dtRngEnd.Value.ToString(ComConst.FMT_DATE.SHORT))
                {
                    GVars.Msg.Show("ED007");                   // ֻ�ܴ�ӡһ��ļ�¼!
                    return;
                }

                // ��ȡ��Ҫ��ӡ�ļ�¼
                //string filter = string.Empty;
                string filter = "RECORDING_DATE >= " + SqlConvert.SqlConvert(dtRngStart.Value.ToString(ComConst.FMT_DATE.SHORT));
                filter += "AND RECORDING_DATE <= " + SqlConvert.SqlConvert(dtRngEnd.Value.ToString(ComConst.FMT_DATE.SHORT));            
                DataRow[] drData = dtNursingData.Select(filter);

                int row = 10, page = 1;

                // ��ӡ
                if (excelAccess == null)
                {
                    excelAccess = new HISPlus.ExcelAccess();
                }

                setPrintInfo(filter);

                excelTemplatePrint(filter, drData, row, page, true);
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
        /// ��ť[����]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                for (int i = 0; i < grdRecord.Rows; i++)
                {
                    bool isExsit = false;

                    foreach (DataRow dr in dsNursingRecord.Tables[0].Rows)
                    {
                        string recordDate = DataType.GetMD(dr["RECORDING_DATE"].ToString());
                        string timePoint = DataType.GetShortTime(dr["TIME_POINT"].ToString());

                        if (grdRecord.get_TextMatrix(i, 0).Trim().CompareTo(recordDate.Trim()) == 0
                            && grdRecord.get_TextMatrix(i, 1).Trim().CompareTo(timePoint.Trim()) == 0)
                        {
                            isExsit = true;

                            // �޸ļ�¼
                            if (grdRecord.get_TextMatrix(i, 13).Trim().CompareTo(dr["REPRSENTATION"].ToString().Trim()) != 0)
                            {
                                dr["REPRSENTATION"] = grdRecord.get_TextMatrix(i, 13).Trim();
                            }

                            break;
                        }
                    }

                    // ������¼
                    if(isExsit == false)
                    {
                        DataRow drNew = dsNursingRecord.Tables[0].NewRow();
                        drNew["RECORDING_DATE"] = dtNursingData.Rows[i]["RECORDING_DATE"].ToString();
                        drNew["TIME_POINT"] = dtNursingData.Rows[i]["TIME_POINT"].ToString();
                        drNew["PATIENT_ID"] = patientId;
                        drNew["VISIT_ID"] = visitId;
                        drNew["REPRSENTATION"] = grdRecord.get_TextMatrix(i, 13).Trim();
                        drNew["START_ROW"] = dtNursingData.Rows[i]["START_ROW"].ToString();
                        drNew["START_PAGE"] = dtNursingData.Rows[i]["START_PAGE"].ToString();
                        drNew["END_ROW"] = dtNursingData.Rows[i]["END_ROW"].ToString();
                        drNew["END_PAGE"] = dtNursingData.Rows[i]["END_PAGE"].ToString();
                        dsNursingRecord.Tables[0].Rows.Add(drNew);
                    }
                }

                updateNursingRecord();
                mergeNursingData(ref dtNursingData);          // ����Ӧ�ڽ���Ļ����¼�����
                GVars.Msg.Show("ID009");
                btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                GVars.Msg.Show("ED006");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        /// <summary>
        /// ��ť[�ر�]
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
        /// ��ť[����](��ӡʱ����ѡ���ѯһ��ļ�¼)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // ��ȡ���ؼ��ϵ�����
                getGrdNursingData(ref dtNursingDataNew); 

                // �Ƚ��Ƿ������ݱ仯,����,������ʾ(��������)
                for (int i = 0; i < dtNursingData.Rows.Count; i++)
                {
                    string nursingData = dtNursingData.Rows[i]["NURSING"].ToString().Trim();
                    string nursingDataNew = dtNursingDataNew.Rows[i]["NURSING"].ToString().Trim();

                    if (nursingData.Equals(nursingDataNew) == false && i < dtNursingData.Rows.Count - 1)
                    {
                        GVars.Msg.Show("ID011");                // �Ѵ�ӡ���ļ�¼�и���,��������!
                        return;
                    }

                    if (nursingData.Equals(nursingDataNew) == false && i == dtNursingData.Rows.Count - 1)
                    {
                        GVars.Msg.Show("ID010");                // ���ݻ�δ����,���ȱ�����ٴ�ӡ!
                        return;
                    }
                }

                if (dtRngStart.Value.ToString(ComConst.FMT_DATE.SHORT) != dtRngEnd.Value.ToString(ComConst.FMT_DATE.SHORT))
                {
                    GVars.Msg.Show("ED007");                   // ֻ�ܴ�ӡһ��ļ�¼!
                    return;
                }

                // ��ȡ��Ҫ��ӡ�ļ�¼
                string filter = string.Empty;
                filter += "RECORDING_DATE >= " + SqlConvert.SqlConvert(dtRngStart.Value.ToString(ComConst.FMT_DATE.SHORT));
                filter += "AND RECORDING_DATE <= " + SqlConvert.SqlConvert(dtRngEnd.Value.ToString(ComConst.FMT_DATE.SHORT));   
                filter += "AND START_ROW < 10";
                DataRow[] drData = dtNursingData.Select(filter);

                int row = 10, page = 1;

                for (int i = 0; i < dtNursingData.Rows.Count; i++)
                {
                     int rowNew = Convert.ToInt32(dtNursingData.Rows[i]["END_ROW"].ToString());
                     int pageNew = Convert.ToInt32(dtNursingData.Rows[0]["END_PAGE"].ToString());
                    
                    if (rowNew > 0)
                    {
                        row = rowNew + 1;
                        page = pageNew;

                        if (row > 32)
                        {
                            row = 10;
                            page = pageNew + 1;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                // ��ӡ
                if (excelAccess == null)
                {
                    excelAccess = new HISPlus.ExcelAccess();
                }

                setPrintInfo(filter);

                excelTemplatePrint(filter, drData, row, page, true);
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
        /// ���ؼ�EnterCell�¼�
        /// </summary>
        private void grdRecord_EnterCell(object sender, EventArgs e)
        {
            scrollValue = true;      // �Ƿ񴥷��������¼�

            // �ж��û��Ƿ���Ȩ���ļ�¼
            if (GVars.User.Name.CompareTo(grdRecord.get_TextMatrix(grdRecord.Row, 14)) == 0)
            {
                // ��λ�����textbox
                if (grdRecord.Col == 13)
                {
                    int top = 0;
                    for (int i = grdRecord.TopRow; i < grdRecord.Row; i++)
                    {
                        top += grdRecord.get_RowHeight(i);
                    }

                    txtData.Top = grdRecord.Top + top / 15;
                    txtData.Height = grdRecord.get_RowHeight(grdRecord.Row) / 15;

                    // �жϽ�����ʾ�ı��ؼ����һ���Ƿ�����,�������������ε��������¼�
                    if ((txtData.Top + txtData.Height) > (grdRecord.Top + grdRecord.Height / 15))
                    {
                        scrollValue = false;
                    }

                    txtData.Focus();
                    txtData.Visible = true;
                    btnSave.Enabled = true;
                    txtData.Text = grdRecord.get_TextMatrix(grdRecord.Row, grdRecord.Col);
                    selectRow = grdRecord.Row;
                }
            }
        }


        /// <summary>
        /// ���ؼ��Ĺ������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdRecord_Scroll(object sender, EventArgs e)
        {
            if (scrollValue == true)
            {
                txtData.Top = txtDate.Top;
                txtData.Visible = false;
            }
            else
            {
                grdRecord_EnterCell(sender, e);
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


        #region ��������
        /// <summary>
        /// ��ʼ��������ʾ
        /// </summary>
        private void initDisp()
        {
            showPatientInfo();          
            setGrid();
            initDataTable(); 
        }


        /// <summary>
        /// ��ʼ����ṹ
        /// </summary>
        private void initDataTable()
        {
            dtNursingData = new DataTable("NURSING_DATA");

            DataColumn myDataColumn;  
            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.DateTime");
            //myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "RECORDING_DATE";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.DateTime");
            //myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "TIME_POINT";
            dtNursingData.Columns.Add(myDataColumn);

            //myDataColumn = new DataColumn();
            //myDataColumn.DataType = Type.GetType("System.String");
            //myDataColumn.ColumnName = "PATIENT_ID";
            //dtNursingData.Columns.Add(myDataColumn);

            //myDataColumn = new DataColumn();
            //myDataColumn.DataType = Type.GetType("System.Int");
            //myDataColumn.ColumnName = "VISIT_ID";
            //dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "TEMPERATURE";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "PULSE";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "BREATH";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "BLOOD";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "BLANK1";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "BLANK2";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "PROJECT";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "REAL_IN";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "PEE";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "STOOL";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "BLANK3";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "NURSING";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "SIGN";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "START_ROW";
            myDataColumn.DefaultValue = "0";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "START_PAGE";
            myDataColumn.DefaultValue = "1";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "END_ROW";
            myDataColumn.DefaultValue = "0";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "END_PAGE";
            myDataColumn.DefaultValue = "1";
            dtNursingData.Columns.Add(myDataColumn);

            dtNursingData.AcceptChanges();
            dtNursingDataNew = dtNursingData.Clone();
            dtNursingDataNew.AcceptChanges();
        }


        /// <summary>
        /// ��ʾ���˵Ļ�����Ϣ
        /// </summary>
        private void showPatientInfo()
        {
            // ��ս���
            this.txtBedLabel.Text = string.Empty;                       // ���˴����
            this.lblPatientName.Text = string.Empty;                    // ��������
            this.lblGender.Text = string.Empty;                         // �����Ա�
            this.lblAge.Text = string.Empty;                            // ����
            this.lblDeptName.Text = string.Empty;                       // �Ʊ�
            this.lblDate.Text = string.Empty;                        �� // ��¼����
            this.lblDiagnosis.Text = string.Empty;                      // �ٴ����

            // ���û�������˳�
            if (dsPatient == null || dsPatient.Tables.Count == 0)
            {
                return;
            }

            // ��ʾ���˵Ļ�����Ϣ
            DataRow dr = dsPatient.Tables[0].Rows[0];
            
            PersonCls person = new PersonCls();
            if (DataType.IsDateTime(dr["DATE_OF_BIRTH"].ToString()) == true)
            {
                person.Birthday = DateTime.Parse(dr["DATE_OF_BIRTH"].ToString());
            }

            this.txtBedLabel.Text       = dr["BED_LABEL"].ToString();                   // ���˴����
            this.lblPatientName.Text    = dr["NAME"].ToString();                        // ��������
            this.lblGender.Text         = dr["SEX"].ToString();                         // �����Ա�
            this.lblAge.Text            = person.GetAge(DateTime.Now);                  // ����
            this.lblDeptName.Text       = dr["DEPT_NAME"].ToString();                   // �Ʊ�
            this.lblDate.Text           = DataType.GetYMD(DateTime.Now.ToString());     // ��ǰ����
            this.lblDiagnosis.Text      = dr["DIAGNOSIS"].ToString();                   // �ٴ����

            patientId   = dr["PATIENT_ID"].ToString();
            visitId     = dr["VISIT_ID"].ToString();
        }


        /// <summary>
        /// ���ñ��ؼ�
        /// </summary>
        private void setGrid()
        {
            // ���ÿɱ༭��textbox
            txtData.Visible = false;
            txtData.Left = txtNurse.Left;
            txtData.Width = txtNurse.Width;

            // ���ñ���λ��
            grdRecord.Top = txtDate.Bottom;
            grdRecord.Left = txtDate.Left;

            // ���ñ��������и�,�������п�
            grdRecord.Rows = 1;
            grdRecord.Cols = 15;
            grdRecord.set_ColWidth(0, txtDate.Width * 15);
            grdRecord.set_ColWidth(1, txtTime.Width * 15);
            grdRecord.set_ColWidth(2, txtTemp.Width * 15);
            grdRecord.set_ColWidth(3, txtPulse.Width * 15);
            grdRecord.set_ColWidth(4, txtBreath.Width * 15);
            grdRecord.set_ColWidth(5, txtBlood.Width * 15);
            grdRecord.set_ColWidth(6, txtBlank1.Width * 15);
            grdRecord.set_ColWidth(7, txtBlank2.Width * 15);
            grdRecord.set_ColWidth(8, txtProject.Width * 15);
            grdRecord.set_ColWidth(9, txtRealIn.Width * 15);
            grdRecord.set_ColWidth(10, txtPee.Width * 15);
            grdRecord.set_ColWidth(11, txtStool.Width * 15);
            grdRecord.set_ColWidth(12, txtBlank3.Width * 15);
            grdRecord.set_ColWidth(13, txtNurse.Width * 15);
            grdRecord.set_ColWidth(14, txtSign.Width * 15);
        }


        /// <summary>
        /// ��ȡ���˵�������¼
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>
        private DataSet getPatientVitalSignRec(string patientId, string visitId)
        {
            string sql = string.Empty;

            string sqlDateRng = "( TO_DATE(VITAL_SIGNS_REC.RECORDING_DATE) >= "
                                + "TO_DATE('" + dtRngStart.Value.ToString(ComConst.FMT_DATE.SHORT) + "', 'yyyy-MM-dd') "
                                + "AND TO_DATE(VITAL_SIGNS_REC.RECORDING_DATE) <= "
                                + "TO_DATE('" + dtRngEnd.Value.ToString(ComConst.FMT_DATE.SHORT) + "', 'yyyy-MM-dd') "
                                + ") ";

            sql += "SELECT RECORDING_DATE, ";       // ��¼����
            sql += "TIME_POINT, ";                  // ʱ���
            sql += "VITAL_SIGNS, ";                 // ��¼��Ŀ
            sql += "CLASS_CODE, ";                  // ������
            sql += "VITAL_CODE, ";                  // ��Ŀ����
            sql += "VITAL_SIGNS_CVALUES, ";         // ��Ŀ��ֵ
            sql += "NURSE ";
            sql += "FROM VITAL_SIGNS_REC ";             // ������֢��¼
            sql += "WHERE PATIENT_ID = " + SqlConvert.SqlConvert(patientId);        // ���˱�ʶ��
            sql += "AND VISIT_ID = " + SqlConvert.SqlConvert(visitId);
            sql += "AND " + sqlDateRng;
            sql += "AND CLASS_CODE <> 'C' ";
            sql += "AND CLASS_CODE <> 'D' ";
            sql += "ORDER BY RECORDING_DATE, ";         // ��¼����
            sql += "TIME_POINT, ";                  // ʱ���
            sql += "CLASS_CODE, ";                  // ������
            sql += "VITAL_CODE ";                   // ��Ŀ����

            return GVars.OracleAccess.SelectData(sql);
        }


        /// <summary>
        /// ��ȡ����۲켰�����ʩ
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>
        private DataSet getPatientNursingRecord(string patientId, string visitId)
        {
            string sql = string.Empty;

            sql += "SELECT * FROM NURSING_RECORD ";
            sql += "WHERE PATIENT_ID = " + SqlConvert.SqlConvert(patientId);        // ���˱�ʶ��
            sql += "AND VISIT_ID = " + SqlConvert.SqlConvert(visitId);
            sql += "AND RECORDING_DATE >= " + SqlConvert.SqlConvert(dtRngStart.Value.ToString(ComConst.FMT_DATE.SHORT));
            sql += "AND RECORDING_DATE <= " + SqlConvert.SqlConvert(dtRngEnd.Value.ToString(ComConst.FMT_DATE.SHORT));
            sql += "ORDER BY RECORDING_DATE, ";         // ��¼����
            sql += "TIME_POINT ";                       // ʱ���

            return GVars.SqlserverAccess.SelectData(sql, "NURSING_RECORD");
        }


        /// <summary>
        /// ��ʾ���˵�������Ϣ
        /// </summary>
        /// <returns></returns>
        private bool showPatientVitalSignRec()
        {
            if (dsVitalSignRec == null || dsVitalSignRec.Tables.Count == 0 || dsVitalSignRec.Tables[0].Rows.Count == 0)
            {
                return true;
            }
            
            // ��ʾ����
            grdRecord.Rows = dtNursingData.Rows.Count;
       
            int row = 0;
            int nursingWidth = grdRecord.get_ColWidth(13) / 15; 

            foreach (DataRow dr in dtNursingData.Rows)
            {
                grdRecord.set_TextMatrix(row, 0, DataType.GetMD(dr["RECORDING_DATE"].ToString()));
                grdRecord.set_TextMatrix(row, 1, DataType.GetShortTime(dr["TIME_POINT"].ToString()));
                grdRecord.set_TextMatrix(row, 2, dr["TEMPERATURE"].ToString());
                grdRecord.set_TextMatrix(row, 3, dr["PULSE"].ToString());
                grdRecord.set_TextMatrix(row, 4, dr["BREATH"].ToString());
                grdRecord.set_TextMatrix(row, 5, dr["BLOOD"].ToString());
                grdRecord.set_TextMatrix(row, 8, dr["PROJECT"].ToString());
                grdRecord.set_TextMatrix(row, 9, dr["REAL_IN"].ToString());
                grdRecord.set_TextMatrix(row, 10, dr["PEE"].ToString());
                grdRecord.set_TextMatrix(row, 11, dr["STOOL"].ToString());
                grdRecord.set_TextMatrix(row, 13, dr["NURSING"].ToString());
                grdRecord.set_TextMatrix(row, 14, dr["SIGN"].ToString());

                int rowNum = getRowNum(dr["NURSING"].ToString(), this.grdRecord.Font, nursingWidth);
                grdRecord.set_RowHeight(row, rowNum * ROW_HEIGHT + OFF_SET);

                row++;
            }

            lblDate.Text = DataType.GetYMD(dsVitalSignRec.Tables[0].Rows[0]["RECORDING_DATE"].ToString());

            return true;
        }


        /// <summary>
        /// �ϲ���ͬʱ���Ļ�������
        /// </summary>
        /// <param name="dt"></param>
        private void mergeNursingData(ref DataTable dt)
        {
            if (dsVitalSignRec == null || dsVitalSignRec.Tables.Count == 0)
            {
                return;
            }

            dt.Clear();             // ���ԭ�еĽ��滤�����ݼ�¼
            dt.AcceptChanges();

            string timePoint = string.Empty;
            int row = 0;

            for (int i = 0; i < dsVitalSignRec.Tables[0].Rows.Count; i++)
            {
                string timePointStr = dsVitalSignRec.Tables[0].Rows[i]["TIME_POINT"].ToString();

                if (timePoint.Equals(timePointStr) == true)
                {
                    setVitalSignsValue(dtNursingData.Rows[row - 1], dsVitalSignRec.Tables[0].Rows[i]); 
                }
                else               // ����
                {
                    DataRow drNew = dt.NewRow();
                    drNew["RECORDING_DATE"] = dsVitalSignRec.Tables[0].Rows[i]["RECORDING_DATE"].ToString();
                    drNew["TIME_POINT"] = dsVitalSignRec.Tables[0].Rows[i]["TIME_POINT"].ToString();

                    if (setVitalSignsValue(drNew, dsVitalSignRec.Tables[0].Rows[i]) == false)
                    {
                        continue;
                    }
                   
                    foreach (DataRow dr1 in dsNursingRecord.Tables[0].Rows)
                    {
                        if (dr1["TIME_POINT"].ToString().Trim().CompareTo(timePointStr.Trim()) == 0)
                        {
                            drNew["NURSING"] = dr1["REPRSENTATION"].ToString();
                            drNew["START_ROW"] = dr1["START_ROW"].ToString();
                            drNew["START_PAGE"] = dr1["START_PAGE"].ToString();
                            drNew["END_ROW"] = dr1["END_ROW"].ToString();
                            drNew["END_PAGE"] = dr1["END_PAGE"].ToString();
                            break;
                        }
                    }

                    drNew["SIGN"] = dsVitalSignRec.Tables[0].Rows[i]["NURSE"].ToString();
                    dt.Rows.Add(drNew);
                    row++;

                    timePoint = timePointStr;
                }
            }

            dt.AcceptChanges();
        }


        /// <summary>
        /// ������Ŀֵ
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="drSrc"></param>
        /// <returns>true:����Ŀֵ��Ҫ��ʾ�ڽ����� false:����Ŀֵ����Ҫ��ʾ�ڽ�����</returns>
        private bool setVitalSignsValue(DataRow dr, DataRow drSrc)
        {
            bool isShow = true;
            string vitalSigns = drSrc["VITAL_SIGNS"].ToString();

            if (vitalSigns.Length > 2 && String.Compare(vitalSigns.Substring(2), "����") == 0)
            {
                vitalSigns = "����";
            }

            switch (vitalSigns)
            {
                case "����":
                    dr["TEMPERATURE"] = drSrc["VITAL_SIGNS_CVALUES"].ToString();
                    break;

                case "����":
                    dr["PULSE"] = drSrc["VITAL_SIGNS_CVALUES"].ToString();
                    break;

                case "����":
                    dr["BREATH"] = drSrc["VITAL_SIGNS_CVALUES"].ToString();
                    break;

                case "Ѫѹ":
                    dr["BLOOD"] = drSrc["VITAL_SIGNS_CVALUES"].ToString();
                    break;

                case "����Һ��":
                    dr["PROJECT"] = drSrc["VITAL_SIGNS"].ToString();
                    dr["REAL_IN"] = drSrc["VITAL_SIGNS_CVALUES"].ToString();
                    break;

                case "�ų�Һ��":
                    dr["PEE"] = drSrc["VITAL_SIGNS_CVALUES"].ToString();
                    break;

                case "���":
                    dr["STOOL"] = drSrc["VITAL_SIGNS_CVALUES"].ToString();
                    break;

                default:
                    isShow = false;
                    break;
            }

            return isShow;
        }


        /// <summary>
        /// ��ȡ���ؼ��ϵ�����
        /// </summary>
        private void getGrdNursingData(ref DataTable dt)
        {
            dt.Clear();
            dt.AcceptChanges();

            for (int i = 0; i < grdRecord.Rows; i++)
            {
                DataRow drNew = dt.NewRow();
                drNew["NURSING"] = grdRecord.get_TextMatrix(i, 13);
                dt.Rows.Add(drNew);
            }
            dt.AcceptChanges();
        }


        /// <summary>
        /// ��Excelģ���ӡ���Ƚ��ʺ��״򡢸�ʽ��ͳ�Ʒ�������ͼ�η������Զ����ӡ
        /// </summary>
        /// <remarks>��Excel��ӡ������Ϊ���򿪡�д���ݡ���ӡԤ�����ر�</remarks>
        private void excelTemplatePrint(string filter, DataRow[] drData, int row, int page, bool isFirst)
        {
            // �򿪴�ӡģ��
            string excelFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "�����¼��.xls");

            excelAccess.Open(excelFile);	                            // ��ģ���ļ�
            excelAccess.IsVisibledExcel = true;
            excelAccess.FormCaption = string.Empty;

            excelAccess.PrintInfo = excelAccess.printNursingData(filter, drData, grfx, ref dsNursingRecord, row, page, excelAccess.PatientInfo, isFirst);

            // ��ӡ
            excelAccess.PrintPreview();			                        // Ԥ��

            excelAccess.Close(false);	                                // �ر�

            while (excelAccess.PrintInfo.Pagination == true)            // ��ҳ
            {
                excelTemplatePrint(filter, excelAccess.PrintInfo.DataRowList, excelAccess.PrintInfo.RowNum, excelAccess.PrintInfo.PageNum, excelAccess.PrintInfo.IsShow);
            }

            // ����¼���кŵ����ݼ����������ݿ�
            updateNursingRecord();           
        }


        /// <summary>
        /// ����¼���кŵĻ����¼���ݼ�������sql���ݿ�
        /// </summary>
        private void updateNursingRecord()
        {
            // ����¼���кŵ����ݼ����������ݿ�
            string sql = string.Empty;
            sql += "SELECT * FROM NURSING_RECORD ";
            sql += "WHERE PATIENT_ID = " + SqlConvert.SqlConvert(patientId);        // ���˱�ʶ��
            sql += "AND VISIT_ID = " + SqlConvert.SqlConvert(visitId);
            sql += "AND RECORDING_DATE >= " + SqlConvert.SqlConvert(dtRngStart.Value.ToString(ComConst.FMT_DATE.SHORT));
            sql += "AND RECORDING_DATE <= " + SqlConvert.SqlConvert(dtRngEnd.Value.ToString(ComConst.FMT_DATE.SHORT));
            sql += "ORDER BY RECORDING_DATE, ";         // ��¼����
            sql += "TIME_POINT ";                       // ʱ���

            GVars.SqlserverAccess.Update(ref dsNursingRecord, "NURSING_RECORD", sql); 
        }


        /// <summary>
        /// Ҫ��ӡ�Ĳ��˵Ļ�����Ϣ
        /// </summary>
        private void setPrintInfo(string filter)
        {
            DataTable dt = dtNursingData.Copy();
            excelAccess.DTNursingDataNew = dt; 
            excelAccess.PatientInfo.Name = lblPatientName.Text;
            excelAccess.PatientInfo.Gender = lblGender.Text;
            excelAccess.PatientInfo.Age = lblAge.Text;
            excelAccess.PatientInfo.DeptName = lblDeptName.Text;
            excelAccess.PatientInfo.BedNo = txtBedLabel.Text.Trim();
            excelAccess.PatientInfo.Date = lblDate.Text;
            excelAccess.PatientInfo.Diagnosis = lblDiagnosis.Text;
        }


        /// <summary>
        /// �����ַ������ı�������ռ����(���ǵ����е����)
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="strFont"></param>
        /// <param name="colWidth"></param>
        /// <returns></returns>
        private int getRowNum(string strValue, Font strFont, int colWidth)
        {
            string[] strSplit = strValue.Split('\r');
            int rowNum = 0;

            float heightUnit = grfx.MeasureString("ҽ", strFont).Height;
            float widthUnit = grfx.MeasureString("ҽ", strFont).Width;
            float widthAll = 0.0F;
         
            for (int i = 0; i < strSplit.Length; i++)
            {
                widthAll = grfx.MeasureString(strSplit[i], strFont).Width;
                int rowNumer = 0;

                if (widthAll > colWidth)
                {
                    rowNumer = (int)(widthAll / colWidth);
                    float mode = widthAll % colWidth;
                    if (mode > 0)
                    {
                        rowNumer += 1;
                    }
                }
                else
                {
                    rowNumer += 1;
                }

                rowNum += rowNumer;
            }

            return rowNum;
        }
        #endregion
    }
}