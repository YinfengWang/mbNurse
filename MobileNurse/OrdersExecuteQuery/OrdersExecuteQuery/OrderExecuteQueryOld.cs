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
    
    public partial class OrderExecuteQueryOld : FormDo,IBasePatient
    {
        #region ����
        private PatientDbI patientCom;                                  // ���˹�ͨ
        
        private DataSet dsPatient = null;                               // ������Ϣ
        private DataSet dsOrderExecute = null;                          // ҽ��ִ�е�
        
        private string patientId = string.Empty;                        // ���˱�ʶ��
        private string visitId = string.Empty;                          // סԺ��ʶ
        
        private bool    blnSuperUser= false;                            // �����û�
        #endregion


        public OrderExecuteQueryOld()
        {
            _id     = "00018";
            _guid   = "1E1EDFF1-8E58-45cd-85BD-70B20C3B94D1";
            
            InitializeComponent();
        }
        
        
        #region �����¼�
        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderExecuteQuery_Load(object sender, EventArgs e)
        {
            try
            {
                patientCom = new PatientDbI(GVars.OracleAccess);
                
                // blnSuperUser = userDbI.ChkSuperUser(GVars.User.ID, RIGHT_ID);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
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

                dsOrderExecute = getOrderExecute(patientId, visitId, dtRngStart.Value, dtRngEnd.Value);
                showOrderExecute();
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
        /// ��ť[����ִ��]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelExecute_Click(object sender, EventArgs e)
        {
            try
            {
                // �������
                if (lvwOrderExecute.SelectedIndices.Count == 0)
                {
                    MessageBox.Show("��ѡ��Ҫ����ִ�е�ҽ��!");
                    return;
                }

                // �����û�Ȩ��
                int selectedIndex = lvwOrderExecute.SelectedIndices[0];
                ListViewItem item = lvwOrderExecute.Items[selectedIndex];
                
                if (blnSuperUser == false
                    && item.SubItems[lvwOrderExecute.Columns.Count - 1].Text.Equals(GVars.User.Name) == false)
                {
                    MessageBox.Show("�������г�����ҽ��ִ�е���Ȩ��! ����Ϊִ���˻�����޸�Ȩ��!");
                    return;
                }

                // �û�ȷ��
                if (MessageBox.Show("��ȷ��Ҫ������ǰҽ��ִ����?", "��ȷ��", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }

                // ִ�г���
                DataRow dr = dsOrderExecute.Tables[0].Rows[lvwOrderExecute.SelectedIndices[0]];

                string orderNo = dr["ORDER_NO"].ToString();
                DateTime dtPerformSchedule = (DateTime)(dr["SCHEDULE_PERFORM_TIME"]);

                if (cancelOrdersExecute(patientId, visitId, orderNo, dtPerformSchedule) == true)
                {
                    MessageBox.Show("�����ɹ�!");

                    // ���½�����ʾ
                    btnQuery_Click(sender, e);

                    if (selectedIndex >= lvwOrderExecute.Items.Count)
                    {
                        selectedIndex = lvwOrderExecute.Items.Count - 1;
                    }

                    if (selectedIndex >= 0)
                    {
                        lvwOrderExecute.Items[selectedIndex].Selected = true;
                        lvwOrderExecute.Items[selectedIndex].EnsureVisible();
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
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
        /// ��ȡ���˵�ҽ��ִ�е�
        /// </summary>
        /// <returns></returns>
        private DataSet getOrderExecute_Old(string patientId, string visitId, DateTime dtStart, DateTime dtEnd)
        {
            string sqlDateRng = "( TO_DATE(ORDERS_EXECUTE.CONVERSION_DATE_TIME) >= "
                                + "TO_DATE('" + dtStart.ToString(ComConst.FMT_DATE.SHORT) + "', 'yyyy-MM-dd') "
                                + "AND TO_DATE(ORDERS_EXECUTE.CONVERSION_DATE_TIME) <= "
                                + "TO_DATE('" + dtEnd.ToString(ComConst.FMT_DATE.SHORT) + "', 'yyyy-MM-dd') "
                                + ") ";

            string sql = string.Empty;

            sql += "SELECT BED_REC.BED_LABEL, ";                                                        // �����
            sql +=     "ORDERS_EXECUTE.CONVERSION_DATE_TIME, ";                                         // ת������
            sql +=     "ORDERS_EXECUTE.PATIENT_ID, ";                                                   // ���˱�ʶ��
            sql +=     "PAT_MASTER_INDEX.NAME, ";                                                       // ����
            sql +=     "PAT_MASTER_INDEX.SEX, ";                                                        // �Ա�
            sql +=     "ORDERS_EXECUTE.ORDER_TEXT, ";                                                   // ҽ������
            sql +=     "ORDERS_EXECUTE.ORDER_CLASS, ";                                                  // ҽ�����

            sql +=     "(SELECT CLINIC_ITEM_CLASS_DICT.CLASS_NAME FROM CLINIC_ITEM_CLASS_DICT ";
            sql +=     " WHERE CLINIC_ITEM_CLASS_DICT.CLASS_CODE = ORDERS_EXECUTE.ORDER_CLASS) ORDER_CLASS_NAME, ";

            sql +=     "ORDERS_EXECUTE.REPEAT_INDICATOR, ";                                             // ����/��ʱ
            sql +=     "ORDERS_EXECUTE.ORDER_SUB_NO, ";                                                 // ҽ�������
            sql +=     "ORDERS_EXECUTE.ORDER_NO, ";                                                     // ҽ�����
            sql +=     "ORDERS_EXECUTE.DOSAGE, ";                                                       // ҩƷһ��ʹ�ü���
            sql +=     "ORDERS_EXECUTE.DOSAGE_UNITS, ";                                                 // ������λ
            sql +=     "ORDERS_EXECUTE.ORDERS_PERFORM_SCHEDULE, ";                                      // ҽ��Ĭ��ִ��ʱ��
            sql +=     "ORDERS_EXECUTE.SCHEDULE_PERFORM_TIME, ";                                        // ҽ��Ĭ��ִ��ʱ��
            sql +=     "ORDERS_EXECUTE.ADMINISTRATION,";                                                // ;��
            sql +=     "ORDERS_EXECUTE.EXECUTE_DATE_TIME, ";                                            // ʵ��ִ��ʱ��
            sql +=     "ORDERS_EXECUTE.EXECUTE_NURSE, ";                                                // ִ�л�ʿ
            sql +=     "ORDERS_EXECUTE.IS_EXECUTE ";                                                    // �Ƿ�ִ��
            sql += "FROM ORDERS_EXECUTE, ";                                                             // ҽ��ִ�б�
            sql +=     "PATS_IN_HOSPITAL, ";                                                            // ��Ժ���˼�¼
            sql +=     "BED_REC, ";                                                                     // ��λ��¼
            sql +=     "PAT_MASTER_INDEX ";                                                             // ����������
            sql += "WHERE ";
            sql +=     "ORDERS_EXECUTE.IS_EXECUTE >= '1' ";                                             // ִ��
            sql +=     "AND PATS_IN_HOSPITAL.BED_NO = BED_REC.BED_NO ";                                 // ����
            sql +=     "AND PATS_IN_HOSPITAL.WARD_CODE = BED_REC.WARD_CODE ";                           // ���ڲ�������
            
            sql +=     "AND ORDERS_EXECUTE.PATIENT_ID = " + SqlConvert.SqlConvert(patientId);           // ���˱�ʶ��
            sql +=     "AND ORDERS_EXECUTE.VISIT_ID = " + SqlConvert.SqlConvert(visitId);               // ���˱���סԺ��ʶ

            sql +=     "AND ORDERS_EXECUTE.PATIENT_ID = PATS_IN_HOSPITAL.PATIENT_ID ";                  // ���˱�ʶ��
            sql +=     "AND ORDERS_EXECUTE.VISIT_ID = PATS_IN_HOSPITAL.VISIT_ID ";                      // ���˱���סԺ��ʶ
            sql +=     "AND PATS_IN_HOSPITAL.PATIENT_ID = PAT_MASTER_INDEX.PATIENT_ID ";                // ���˱�ʶ��
            sql +=     "AND PATS_IN_HOSPITAL.WARD_CODE = " + SqlConvert.SqlConvert(GVars.User.DeptCode); // ���ڲ�������
            sql +=     "AND " + sqlDateRng;
            sql += "ORDER BY ";
            sql +=     "ORDERS_EXECUTE.CONVERSION_DATE_TIME, ";                                         // ת������
            sql +=     "ORDERS_EXECUTE.SCHEDULE_PERFORM_TIME, ";                                        // ҽ��Ĭ��ִ��ʱ��
            sql +=     "ORDERS_EXECUTE.ORDER_NO, ";                                                     // ҽ�����
            sql +=     "ORDERS_EXECUTE.ORDER_SUB_NO ";                                                  // ҽ�������
            
            return GVars.OracleAccess.SelectData(sql);
        }


        /// <summary>
        /// ��ȡ���˵�ҽ��ִ�е�
        /// </summary>
        /// <returns></returns>
        private DataSet getOrderExecute(string patientId, string visitId, DateTime dtStart, DateTime dtEnd)
        {
            string sqlDateRng = "( TO_DATE(ORDERS_EXECUTE.CONVERSION_DATE_TIME) >= "
                                + "TO_DATE('" + dtStart.ToString(ComConst.FMT_DATE.SHORT) + "', 'yyyy-MM-dd') "
                                + "AND TO_DATE(ORDERS_EXECUTE.CONVERSION_DATE_TIME) <= "
                                + "TO_DATE('" + dtEnd.ToString(ComConst.FMT_DATE.SHORT) + "', 'yyyy-MM-dd') "
                                + ") ";

            string sql = string.Empty;

            sql += "SELECT PATIENT_INFO.BED_LABEL, ";                                                        // �����
            sql +=     "ORDERS_EXECUTE.CONVERSION_DATE_TIME, ";                                         // ת������
            sql +=     "ORDERS_EXECUTE.PATIENT_ID, ";                                                   // ���˱�ʶ��
            sql +=     "PATIENT_INFO.NAME, ";                                                       // ����
            sql +=     "PATIENT_INFO.SEX, ";                                                        // �Ա�
            sql +=     "ORDERS_EXECUTE.ORDER_TEXT, ";                                                   // ҽ������
            sql +=     "ORDERS_EXECUTE.ORDER_CLASS, ";                                                  // ҽ�����

            sql +=     "(SELECT CLINIC_ITEM_CLASS_DICT.CLASS_NAME FROM CLINIC_ITEM_CLASS_DICT ";
            sql +=     " WHERE CLINIC_ITEM_CLASS_DICT.CLASS_CODE = ORDERS_EXECUTE.ORDER_CLASS) ORDER_CLASS_NAME, ";

            sql +=     "ORDERS_EXECUTE.REPEAT_INDICATOR, ";                                             // ����/��ʱ
            sql +=     "ORDERS_EXECUTE.ORDER_SUB_NO, ";                                                 // ҽ�������
            sql +=     "ORDERS_EXECUTE.ORDER_NO, ";                                                     // ҽ�����
            sql +=     "ORDERS_EXECUTE.DOSAGE, ";                                                       // ҩƷһ��ʹ�ü���
            sql +=     "ORDERS_EXECUTE.DOSAGE_UNITS, ";                                                 // ������λ
            sql +=     "ORDERS_EXECUTE.ORDERS_PERFORM_SCHEDULE, ";                                      // ҽ��Ĭ��ִ��ʱ��
            sql +=     "ORDERS_EXECUTE.SCHEDULE_PERFORM_TIME, ";                                        // ҽ��Ĭ��ִ��ʱ��
            sql +=     "ORDERS_EXECUTE.ADMINISTRATION,";                                                // ;��
            sql +=     "ORDERS_EXECUTE.EXECUTE_DATE_TIME, ";                                            // ʵ��ִ��ʱ��
            sql +=     "ORDERS_EXECUTE.EXECUTE_NURSE, ";                                                // ִ�л�ʿ
            sql +=     "ORDERS_EXECUTE.IS_EXECUTE ";                                                    // �Ƿ�ִ��
            sql += "FROM ORDERS_EXECUTE, ";                                                             // ҽ��ִ�б�
            sql +=     "PATIENT_INFO ";                                                                 // ��Ժ���˼�¼
            sql += "WHERE ";
            sql +=     "ORDERS_EXECUTE.IS_EXECUTE >= '1' ";                                             // ִ��

            sql +=     "AND ORDERS_EXECUTE.PATIENT_ID = " + SqlConvert.SqlConvert(patientId);           // ���˱�ʶ��
            sql +=     "AND ORDERS_EXECUTE.VISIT_ID = " + SqlConvert.SqlConvert(visitId);               // ���˱���סԺ��ʶ

            sql +=     "AND ORDERS_EXECUTE.PATIENT_ID = PATIENT_INFO.PATIENT_ID ";                  // ���˱�ʶ��
            sql +=     "AND ORDERS_EXECUTE.VISIT_ID = PATIENT_INFO.VISIT_ID ";                      // ���˱���סԺ��ʶ
            sql +=     "AND PATIENT_INFO.WARD_CODE = " + SqlConvert.SqlConvert(GVars.User.DeptCode); // ���ڲ�������
            sql +=     "AND " + sqlDateRng;
            sql += "ORDER BY ";
            sql +=     "ORDERS_EXECUTE.CONVERSION_DATE_TIME, ";                                         // ת������
            sql +=     "ORDERS_EXECUTE.SCHEDULE_PERFORM_TIME, ";                                        // ҽ��Ĭ��ִ��ʱ��
            sql +=     "ORDERS_EXECUTE.ORDER_NO, ";                                                     // ҽ�����
            sql +=     "ORDERS_EXECUTE.ORDER_SUB_NO ";                                                  // ҽ�������

            return GVars.OracleAccess.SelectData(sql);
        }
        
        
        
        /// <summary>
        /// ��ʾҽ��ִ�м�¼��
        /// </summary>
        private void showOrderExecute()

        {
            string orderDay = string.Empty;
            string orderClass = string.Empty;

            string schedule = string.Empty;
            string execute = string.Empty;

            try
            {
                lvwOrderExecute.BeginUpdate();

                lvwOrderExecute.Items.Clear();

                if (dsOrderExecute == null || dsOrderExecute.Tables.Count == 0)
                {
                    return;
                }

                foreach (DataRow dr in dsOrderExecute.Tables[0].Rows)
                {
                    ListViewItem item = null;
                    DateTime dtConversion = DateTime.Parse(dr["CONVERSION_DATE_TIME"].ToString());

                    if (orderDay.Equals(dtConversion.ToString(ComConst.FMT_DATE.SHORT)) == false)
                    {
                        orderDay = dtConversion.ToString(ComConst.FMT_DATE.SHORT);
                        orderClass = dr["ORDER_CLASS_NAME"].ToString();                         // ���

                        item = new ListViewItem(orderDay);                                      // ����
                        item.SubItems.Add(dr["ORDER_CLASS_NAME"].ToString());                   // ���
                    }
                    else
                    {
                        item = new ListViewItem(string.Empty);                                  // �����
                        
                        if (orderClass.Equals(dr["ORDER_CLASS_NAME"].ToString()) == false)
                        {
                            orderClass = dr["ORDER_CLASS_NAME"].ToString();
                            item.SubItems.Add(dr["ORDER_CLASS_NAME"].ToString());               // ���
                        }
                        else
                        { 
                            item.SubItems.Add(string.Empty);                                    // ���
                        }
                    }

                    item.SubItems.Add("1".Equals(dr["REPEAT_INDICATOR"].ToString())? "��" : "��");              // ����/��ʱ
                    item.SubItems.Add(dr["ORDER_SUB_NO"].ToString());                           // ҽ�������
                    item.SubItems.Add(dr["ORDER_TEXT"].ToString());                             // ҽ��
                    item.SubItems.Add(dr["DOSAGE"].ToString() + dr["DOSAGE_UNITS"].ToString()); // ���� + ������λ
                    item.SubItems.Add(dr["ADMINISTRATION"].ToString());                         // ;��

                    schedule = dr["SCHEDULE_PERFORM_TIME"].ToString();
                    item.SubItems.Add(schedule);                                                // �ƻ�ִ��ʱ��
                    
                    if (dr["EXECUTE_DATE_TIME"].ToString().Length > 0)
                    {
                        execute = DataType.GetDateTimeLong(dr["EXECUTE_DATE_TIME"].ToString());
                        
                        item.SubItems.Add(execute);                                                 // ʵ��ִ��ʱ��

                        TimeSpan tmSpan = DateTime.Parse(execute).Subtract(DateTime.Parse(schedule));
                        item.SubItems.Add(tmSpan.TotalHours.ToString("0.00"));                      // ��ʱ                        
                    }
                    else
                    {
                        item.SubItems.Add(string.Empty);                                        // ʵ��ִ��ʱ��
                        item.SubItems.Add(string.Empty);                                        // ��ʱ
                    }                    
                    
                    item.SubItems.Add(dr["EXECUTE_NURSE"].ToString());                          // ִ�л�ʿ
                    
                    lvwOrderExecute.Items.Add(item);
                }
            }
            finally
            {
                lvwOrderExecute.EndUpdate();
            }
        }


        /// <summary>
        /// ����ҽ��ִ��
        /// </summary>
        /// <returns></returns>
        private bool cancelOrdersExecute(string patientId, string visitId, string orderNo, DateTime ordersPerformShedule)
        {
            string sql = "UPDATE ORDERS_EXECUTE SET IS_EXECUTE = NULL, EXECUTE_NURSE = NULL, EXECUTE_DATE_TIME = NULL "
                       + "WHERE "
                            + "PATIENT_ID = " + SqlConvert.SqlConvert(patientId)
                            + "AND VISIT_ID = " + SqlConvert.SqlConvert(visitId)
                            + "AND ORDER_NO = " + SqlConvert.SqlConvert(orderNo)
                            + "AND SCHEDULE_PERFORM_TIME = " + SqlConvert.GetOraDbDate(ordersPerformShedule);

            GVars.OracleAccess.ExecuteNoQuery(sql);

            return true;
        }
        #endregion

        public void Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
            // ��ȡ������Ϣ
            dsPatient = patientCom.GetPatientInfo_FromID(e.PatientId);
            if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("�����ڸò���");
                return;
            }

            patientId =e.PatientId;
            visitId = e.VisitId;

            // ����ҵ�������Ϣ
            btnQuery_Click(sender, e);
        }

        void IBasePatient.Patient_ListRefresh(object sender, PatientEventArgs e)
        {
            
        }
    }
}