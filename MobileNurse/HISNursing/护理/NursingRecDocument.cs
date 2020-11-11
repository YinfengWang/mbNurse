using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class NursingRecDocument : Form
    {
        #region �������
        private NursingRecDocumentCom   com         = new NursingRecDocumentCom();
        
        private PatientDbI              patientCom;                                             // ����
        
        private DataSet                 dsPatient   = null;
        private string                  patientId   = string.Empty;
        private string                  visitId     = string.Empty;
        
        private DataRow[]               drShow      = null;
        
        private int                     BLANK_ROWS  = 40;
        private int                     COL_DESC    = 12;
        #endregion
        
        public NursingRecDocument()
        {
            InitializeComponent();

            this.txtBedLabel.KeyDown += new KeyEventHandler( txtBedLabel_KeyDown );
            this.txtBedLabel.GotFocus += new EventHandler(imeCtrl_GotFocus);
            this.grdTitle.EnterCell += new EventHandler( grdTitle_EnterCell );
                        
            this.grdTitle.DblClick += new EventHandler( grdTitle_DblClick );
        }
        
        
        #region �����¼�
        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NursingRecDocument_Load( object sender, EventArgs e )
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
                com.SetDescWidth(grdTitle.get_ColWidth(COL_DESC) / ComConst.VAL.TWIPS_PER_PIXEL + 12);
                com.Now = GVars.OracleAccess.GetSysDate();                                                 // ��ȡ���ڵ�ʱ��
                com.SelectData(patientId, visitId, dtRngStart.Value, dtRngEnd.Value);           // ��ȡ����                
                
                // ��ʾ����
                showNursingRec(string.Empty);
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
        /// ���뵥Ԫ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void grdTitle_EnterCell( object sender, EventArgs e )
        {
        }
        
        
        /// <summary>
        /// ˫������Ա༭����۲켰�����ʩ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void grdTitle_DblClick( object sender, EventArgs e )
        {
            try
            {
                DateTime    dtTimePoint = GVars.OracleAccess.GetSysDate();
                string      timeStr     = string.Empty;
                int         rowStart    = grdTitle.FixedRows;                               // ĳһ��ʱ���¼�ڱ���еĿ�ʼ��
                string      desc        = string.Empty;
                DataRow     drEdit      = null;
                
                string      filterTime  = "(TIME_POINT >= '{0}' AND TIME_POINT <= '{1}')";  // ��ѯ����
                string      timePoint   = string.Empty;
                string      timePointEnd= string.Empty;
                
                if (drShow == null) { return; }
                
                // ��ȡ��¼
                if (grdTitle.Row >= grdTitle.FixedRows && grdTitle.Row < grdTitle.Rows)
                {
                    timeStr = grdTitle.get_TextMatrix(grdTitle.Row, grdTitle.Cols - 1);
                    
                    for(int i = grdTitle.Row; i >= grdTitle.FixedRows; i--)
                    {
                        if (timeStr.Equals(grdTitle.get_TextMatrix(i, grdTitle.Cols - 1)) == false)
                        {
                            rowStart = i + 1;
                            break;
                        }
                    }
                    
                    if (timeStr.Length > 0)
                    {
                        dtTimePoint     = DateTime.Parse(timeStr);
                        timePoint       = dtTimePoint.ToString(ComConst.FMT_DATE.LONG_MINUTE);
                        timePointEnd    = timePoint + ":59";
                        
                        DataRow[] drFind = com.DsShow.Tables[0].Select(string.Format(filterTime, timePoint, timePointEnd));
                        
                        if (drFind.Length > 0)
                        {
                            drEdit = drFind[0];
                            desc = drEdit["OBSERVE_NURSE"].ToString();
                        }
                    }
                }
                
                // �༭��¼
                TextEditor frmShow = new TextEditor();
                
                frmShow.TimePoint = dtTimePoint.ToString(ComConst.FMT_DATE.LONG);
                frmShow.Desc      = desc.Replace(ComConst.STR.TAB, string.Empty);
                
                frmShow.ShowDialog();
                
                // �����¼
                if (frmShow.ArrDesc != null)
                {
                    desc = string.Empty;
                    
                    for(int i = 0; i < frmShow.ArrDesc.Length;i ++)
                    {
                        desc += frmShow.ArrDesc[i] + ComConst.STR.TAB;
                    }
                    
                    desc = desc.TrimEnd();
                    
                    // ���Ҽ�¼
                    dtTimePoint     = DateTime.Parse(frmShow.TimePoint);
                    timePoint       = dtTimePoint.ToString(ComConst.FMT_DATE.LONG_MINUTE);
                    timePointEnd    = timePoint + ":59";
                    
                    DataRow[] drFind = com.DsShow.Tables[0].Select(string.Format(filterTime, timePoint, timePointEnd));
                    
                    if (drFind.Length > 0)
                    {
                        drEdit = drFind[0];
                        
                        drEdit["OBSERVE_NURSE"] = desc;
                        drEdit["ROWS_DESC"]     = frmShow.Lines.ToString();
                        
                        // �ж��Ƿ���ɾ��
                        if (desc.Trim().Length == 0
                            && drEdit["TEMPERATURE"].ToString().Length == 0
                            && drEdit["PULSE"].ToString().Length == 0
                            && drEdit["BREATH"].ToString().Length == 0
                            && drEdit["BLOOD_PRESSURE"].ToString().Length == 0
                            && drEdit["COL1"].ToString().Length == 0
                            && drEdit["COL2"].ToString().Length == 0
                            && drEdit["ITEM_OUT"].ToString().Length == 0
                            && drEdit["ITEM_IN"].ToString().Length == 0
                        )
                        {
                            drEdit.Delete();                                
                        }
                    }
                    else
                    {
                        drEdit = com.DsShow.Tables[0].NewRow();
                        
                        drEdit["PATIENT_ID"]     = patientId;
                        drEdit["VISIT_ID"]       = visitId;
                        drEdit["TIME_POINT"]     = timePoint;
                        drEdit["OBSERVE_NURSE"]  = desc;
                        drEdit["SUM_FLG"]        = "0";
                        drEdit["SHOW_ORDER"]     = "0";
                        drEdit["ROWS_COUNT_PRE"] = "0";
                        drEdit["ROWS_COUNT"]     = "1";
                        drEdit["ROWS_DESC"]      = frmShow.Lines.ToString();
                        
                        com.DsShow.Tables[0].Rows.Add(drEdit);
                    }
                    
                    // ��ť״̬
                    this.btnSave.Enabled = true;
                }
                
                // ˢ����ʾ
                showNursingRec(frmShow.TimePoint);
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
        private void btnSave_Click( object sender, EventArgs e )
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (patientId.Length == 0)
                {
                    GVars.Msg.Show("E00011");                           // E00011	����ȷ������!

                    return;
                }

                if (com.SaveData() == true)
                {
                    GVars.Msg.Show("I00004", "����");                   // {0}�ɹ�!
                    
                    this.btnSave.Enabled = false;
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
        /// ��ť[����]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContinuePrint_Click( object sender, EventArgs e )
        {
            
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
            
            com.CreateDataTable();
        }
        

        /// <summary>
        /// ��ʼ��������ʾ
        /// </summary>
        private void initDisp()
        {
            grdTitle.RowHeightMin = 250;
            grdTitle.set_RowHeight(0, 600);
            
            grdTitle.set_TextMatrix(0, 0, "  ��" + ComConst.STR.CRLF + ComConst.STR.CRLF + "  ��");
            grdTitle.set_TextMatrix(0, 1, "  ʱ" + ComConst.STR.CRLF + ComConst.STR.CRLF + "  ��");
            grdTitle.set_TextMatrix(0, 2, " ��" + ComConst.STR.CRLF + ComConst.STR.CRLF + " ��");
            grdTitle.set_TextMatrix(0, 3, "��" + ComConst.STR.CRLF + ComConst.STR.CRLF + "��");
            grdTitle.set_TextMatrix(0, 4, "��" + ComConst.STR.CRLF + ComConst.STR.CRLF + "��");
            grdTitle.set_TextMatrix(0, 5, "  Ѫ" + ComConst.STR.CRLF + ComConst.STR.CRLF + "  ѹ");
            
            // ����������
            grdTitle.Rows = grdTitle.FixedRows;
            grdTitle.Rows = grdTitle.FixedRows + BLANK_ROWS;
            
            grdTitle.Row = 0;
            grdTitle.Col = COL_DESC;
            grdTitle.CellAlignment = 4;
            
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
            DataRow[] drShow2 = null;
            
            int     rowStart = 0;
            
            // ���û��ȷ��ʱ���, ��ʾȫ������
            if (timePoint.Length == 0)
            {
                rowStart = grdTitle.FixedRows;
                drShow = com.DsShow.Tables[0].Select(string.Empty, "TIME_POINT ASC, SHOW_ORDER ASC");
                drShow2 = drShow;
            }
            // ���ָ��ʱ���, ��ʾ��ʱ��㼰�Ժ������
            else
            {
                // ��ȡ��ʱ�����ǰ��������ռ����
                string filter = "TIME_POINT < " + HISPlus.SqlManager.SqlConvert(timePoint);
                drShow2 = com.DsShow.Tables[0].Select(filter, "TIME_POINT ASC, SHOW_ORDER ASC");
                rowStart = grdTitle.FixedRows + getShowRows(drShow2);
                
                filter = "TIME_POINT >= " + HISPlus.SqlManager.SqlConvert(timePoint);
                drShow2 = com.DsShow.Tables[0].Select(filter, "TIME_POINT ASC, SHOW_ORDER ASC");
            }
            
            // ��������
            int rows = getShowRows(drShow2);
            
            // ��ձ��
            grdTitle.Rows   = rowStart;
            grdTitle.Rows   = rowStart + rows + BLANK_ROWS;
            
            // ��ʾ����
            showNursingRec(drShow2, rowStart);
            
            // ��λ��ǰ��
            grdTitle.Row = rowStart;
        }
        
        
        /// <summary>
        /// ��ʾ��������
        /// </summary>
        private void showNursingRec(DataRow[] drArray, int rowStart)
        {
            string strDate  = string.Empty;
            string strTime  = string.Empty;
            DateTime timePoint;
            
            int     row     = rowStart;
            int     col     = grdTitle.FixedCols;
            string  desc    = string.Empty;
            string  timeStr = string.Empty;
            bool    blnSum  = false;
            
            int     rowDesc = 0;
            string[] arrDesc = null;
            
            // ��ȡǰһ��¼��ʱ��
            if (rowStart > grdTitle.FixedRows)
            {
                string strValue = grdTitle.get_TextMatrix(rowStart - 1, grdTitle.Cols - 1);
                if (strValue.Length > 0)
                {
                    timePoint = DateTime.Parse(strValue);
                    
                    strDate = timePoint.ToString("MM.dd");
                    strTime = timePoint.ToString(ComConst.FMT_DATE.TIME_SHORT);
                }
            }
            
            // ��ʾ����
            for(int i = 0; i < drArray.Length; i++)
            {
                DataRow dr = drArray[i];
                
                timePoint = DateTime.Parse(dr["TIME_POINT"].ToString());
                
                blnSum = ("1".Equals(dr["SUM_FLG"].ToString()));
                
                // ���ʱ���仯��, ������ûд��, д�겡��
                if ((strDate.Equals(timePoint.ToString("MM.dd")) == false
                    || strTime.Equals(timePoint.ToString(ComConst.FMT_DATE.TIME_SHORT)) == false)
                    && (arrDesc != null && rowDesc < arrDesc.Length))
                {
                    for(int j = rowDesc; j < arrDesc.Length; j++)
                    {
                        grdTitle.set_TextMatrix(row, grdTitle.Cols - 1, timeStr);               // ʱ���
                        grdTitle.set_TextMatrix(row++, COL_DESC, arrDesc[rowDesc++]);           // ����۲�
                    }
                }
                
                // �����С��
                if (blnSum == true)
                {
                    desc    = dr["OBSERVE_NURSE"].ToString();
                    arrDesc = desc.Split(ComConst.STR.TAB.ToCharArray());
                    rowDesc = 0;

                    col = 0;
                    grdTitle.set_TextMatrix(row, col++, strDate);                               // ����
                     
                    for (int j = 0; j < arrDesc.Length; j++)
                    {
                        col = COL_DESC;

                        if (rowDesc < arrDesc.Length)
                        {
                            grdTitle.set_TextMatrix(row, col++, arrDesc[rowDesc++]);            // ����۲�
                        }
                        else
                        {
                            grdTitle.set_TextMatrix(row, col++, string.Empty);                  // ����۲�
                        }

                        grdTitle.set_TextMatrix(row, col++, dr["NURSE"].ToString());            // ��ʿ

                        // ʱ���
                        timeStr = dr["TIME_POINT"].ToString();
                        grdTitle.set_TextMatrix(row, col++, timeStr);

                        row++;
                    }

                    continue;
                }
                
                // ������ڱ���
                if (strDate.Equals(timePoint.ToString("MM.dd")) == false)
                {
                    col = 0;
                    
                    desc    = dr["OBSERVE_NURSE"].ToString();
                    arrDesc = desc.Split(ComConst.STR.TAB.ToCharArray());
                    rowDesc = 0;
                    
                    strDate = timePoint.ToString("MM.dd");
                    strTime = timePoint.ToString(ComConst.FMT_DATE.TIME_SHORT);
                    
                    grdTitle.set_TextMatrix(row, col++, strDate);                       // ����
                    grdTitle.set_TextMatrix(row, col++, strTime);                       // ʱ��
                }
                // ���ʱ�����
                else if (strTime.Equals(timePoint.ToString(ComConst.FMT_DATE.TIME_SHORT)) == false)
                {
                    col = 1;
                    
                    desc    = dr["OBSERVE_NURSE"].ToString();
                    arrDesc = desc.Split(ComConst.STR.TAB.ToCharArray());
                    rowDesc = 0;
                    
                    strTime = timePoint.ToString(ComConst.FMT_DATE.TIME_SHORT);
                    
                    grdTitle.set_TextMatrix(row, col++, strTime);                       // ʱ��
                }
                // ͬһʱ������������
                else
                {
                    col = 2;
                }
                                
                grdTitle.set_TextMatrix(row, col++, dr["TEMPERATURE"].ToString());      // ����
                grdTitle.set_TextMatrix(row, col++, dr["PULSE"].ToString());            // ����
                grdTitle.set_TextMatrix(row, col++, dr["BREATH"].ToString());           // ����
                grdTitle.set_TextMatrix(row, col++, dr["BLOOD_PRESSURE"].ToString());   // Ѫѹ
                col++;                                                                  // ��1
                col++;                                                                  // ��2
                grdTitle.set_TextMatrix(row, col++, dr["ITEM_IN"].ToString());          // ������Ŀ
                grdTitle.set_TextMatrix(row, col++, dr["ITEM_IN_AMOUNT"].ToString());   // ����ֵ
                grdTitle.set_TextMatrix(row, col++, dr["ITEM_OUT"].ToString());         // ������Ŀ
                grdTitle.set_TextMatrix(row, col++, dr["ITEM_OUT_AMOUNT"].ToString());  // ����ֵ
                
                if (rowDesc < arrDesc.Length)
                {
                    grdTitle.set_TextMatrix(row, col++, arrDesc[rowDesc++]);            // ����۲�
                }
                else
                {
                    grdTitle.set_TextMatrix(row, col++, string.Empty);                  // ����۲�
                }
                
                grdTitle.set_TextMatrix(row, col++, dr["NURSE"].ToString());            // ��ʿ
                
                // ʱ���
                timeStr = dr["TIME_POINT"].ToString();
                grdTitle.set_TextMatrix(row, col++, timeStr);
                
                row++;                
            }
            
            // �������ûд��, д�겡��
            if (arrDesc != null && rowDesc < arrDesc.Length)
            {
                for(int j = rowDesc; j < arrDesc.Length; j++)
                {
                    grdTitle.set_TextMatrix(row, grdTitle.Cols - 1, timeStr);           // ʱ���
                    grdTitle.set_TextMatrix(row, COL_DESC, arrDesc[rowDesc++]);         // ����۲�
                    row++;
                }
            }
        }
        
        
        /// <summary>
        /// ��ȡ��ʾ������, ���ѽ�����浽��¼��
        /// </summary>
        /// <returns></returns>
        private int getShowRows(DataRow[] drArray)
        {
            int allRows     = 0;                    // ������
            int descRows    = 0;                    // ����������ռ����
            int rows        = 0;                    // ĳһ��ʱ��������
            
            string timePoint = string.Empty;
            DataRow drRec   = null;
            
            for(int i = 0; i < drArray.Length; i++)
            {
                DataRow dr = drArray[i];                                
                
                if (timePoint.Equals(dr["TIME_POINT"].ToString()) == false)
                {
                    rows = (rows > descRows ? rows : descRows);
                    allRows += rows;
                    
                    if (drRec != null && rows != int.Parse(drRec["ROWS_COUNT"].ToString()))
                    {
                        drRec["ROWS_COUNT"] = rows;
                    }
                    
                    descRows = int.Parse(dr["ROWS_DESC"].ToString());                    
                    rows    = 1;
                    
                    timePoint = dr["TIME_POINT"].ToString();
                    drRec = dr;
                }
                else
                {
                    rows++;
                }
            }
            
            if (drArray.Length > 0)
            {
                rows = (rows > descRows ? rows : descRows);
                allRows += rows;
                                
                if (rows != int.Parse(drRec["ROWS_COUNT"].ToString()))
                {
                    drRec["ROWS_COUNT"] = rows;
                }
            }
            
            return allRows;            
        }
        #endregion

        private void btnReprint_Click(object sender, EventArgs e)
        {

        }

        private void grdTitle_Enter(object sender, EventArgs e)
        {

        }
    }
}