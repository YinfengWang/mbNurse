using System;
using System.Data;
using System.Windows.Forms;
using HISPlus.UserControls;
using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class OrderNotExecuted : FormDo, IBasePatient
    {
        #region �������
        public DataSet dsOrderExecute = null;                           // ҽ��ִ�е�
        #endregion

        public OrderNotExecuted()
        {
            _id = "00019";
            _guid = "CE91973F-311E-470e-80A0-6D84D7228FC8";

            InitializeComponent();
        }


        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderNotExecuted_Load(object sender, EventArgs e)
        {
            try
            {
                InitControl();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void InitControl()
        {
            ucGridView1.MultiSelect = true;
            ucGridView1.Add("PATIENT_ID", "PATIENT_ID", false);
            ucGridView1.Add("����", "BED_LABEL", 30, ColumnStatus.Unique);
            ucGridView1.Add("����", "NAME", 50, ColumnStatus.Unique);
            ucGridView1.Add("�Ա�", "SEX", 30, ColumnStatus.Unique);
            ucGridView1.Add("���", "ORDER_CLASS_NAME", 30, ColumnStatus.Unique);
            DataTable dt = new DataTable();
            dt.Columns.Add("VALUE");
            dt.Columns.Add("TEXT");
            dt.Rows.Add(new object[] { 1, "��" });
            dt.Rows.Add(new object[] { 0, "��" });
            ucGridView1.Add("��", "REPEAT_INDICATOR", dt, "VALUE", "TEXT", 30);
            ucGridView1.Add("��", "ORDER_NO",30);
            ucGridView1.Add("��", "ORDER_SUB_NO", 30);
            ucGridView1.Add("ҽ��", "ORDER_TEXT", 200);
            ucGridView1.Add("����", "DOSAGE", 50);
            ucGridView1.Add("��λ", "DOSAGE_UNITS", 30);
            ucGridView1.Add("ʱ���", "SCHEDULE_PERFORM_TIME", ComConst.FMT_DATE.LONG, 120);

            ucGridView1.Init();
        }

        /// <summary>
        /// ��ȡδִ�е�ҽ��
        /// </summary>
        /// <param name="dt">Ҫ��ѯ������</param>
        /// <returns></returns>
        private DataSet getOrderNotExecuted_Old(DateTime dtStart, DateTime dtEnd)
        {
            string sql = string.Empty;

            sql += "SELECT BED_REC.BED_LABEL, ";                                                        // �����
            sql += "ORDERS_EXECUTE.PATIENT_ID, ";                                                   // ���˱�ʶ��
            sql += "PAT_MASTER_INDEX.NAME, ";                                                       // ����
            sql += "PAT_MASTER_INDEX.SEX, ";                                                        // �Ա�
            sql += "ORDERS_EXECUTE.ORDER_TEXT, ";                                                   // ҽ������
            sql += "ORDERS_EXECUTE.ORDER_CLASS, ";                                                  // ҽ�����

            sql += "(SELECT CLINIC_ITEM_CLASS_DICT.CLASS_NAME FROM CLINIC_ITEM_CLASS_DICT ";
            sql += " WHERE CLINIC_ITEM_CLASS_DICT.CLASS_CODE = ORDERS_EXECUTE.ORDER_CLASS) ORDER_CLASS_NAME, ";

            sql += "ORDERS_EXECUTE.REPEAT_INDICATOR, ";                                             // ����/��ʱ
            sql += "ORDERS_EXECUTE.ORDER_NO, ";                                                     // ҽ�����
            sql += "ORDERS_EXECUTE.ORDER_SUB_NO, ";                                                 // ҽ�������
            sql += "ORDERS_EXECUTE.DOSAGE, ";                                                       // ҩƷһ��ʹ�ü���
            sql += "ORDERS_EXECUTE.DOSAGE_UNITS, ";                                                 // ������λ
            sql += "ORDERS_EXECUTE.ORDERS_PERFORM_SCHEDULE, ";                                      // ҽ��Ĭ��ִ��ʱ��
            sql += "ORDERS_EXECUTE.SCHEDULE_PERFORM_TIME, ";                                        // ҽ��Ĭ��ִ��ʱ��
            sql += "ORDERS_EXECUTE.IS_EXECUTE ";                                                    // �Ƿ�ִ��
            sql += "FROM ORDERS_EXECUTE, ";                                                             // ҽ��ִ�б�
            sql += "PATS_IN_HOSPITAL, ";                                                            // ��Ժ���˼�¼
            sql += "BED_REC, ";                                                                     // ��λ��¼
            sql += "PAT_MASTER_INDEX ";                                                             // ����������
            sql += "WHERE ";
            sql += "(ORDERS_EXECUTE.IS_EXECUTE IS NULL OR ORDERS_EXECUTE.IS_EXECUTE = '0') ";       // δִ��
            sql += "AND PATS_IN_HOSPITAL.BED_NO = BED_REC.BED_NO ";                                 // ����
            sql += "AND PATS_IN_HOSPITAL.WARD_CODE = BED_REC.WARD_CODE ";                           // ���ڲ�������
            sql += "AND ORDERS_EXECUTE.PATIENT_ID = PATS_IN_HOSPITAL.PATIENT_ID ";                  // ���˱�ʶ��
            sql += "AND ORDERS_EXECUTE.VISIT_ID = PATS_IN_HOSPITAL.VISIT_ID ";                      // ���˱���סԺ��ʶ
            sql += "AND PATS_IN_HOSPITAL.PATIENT_ID = PAT_MASTER_INDEX.PATIENT_ID ";                // ���˱�ʶ��
            sql += "AND PATS_IN_HOSPITAL.WARD_CODE = " + SQL.SqlConvert(GVars.User.DeptCode);       // ���ڲ�������
            sql += "AND TO_DATE(ORDERS_EXECUTE.CONVERSION_DATE_TIME) >= " + SQL.GetOraDbDate_Short(dtStart);
            sql += "AND TO_DATE(ORDERS_EXECUTE.CONVERSION_DATE_TIME) <= " + SQL.GetOraDbDate_Short(dtEnd);
            sql += "ORDER BY ";
            sql += "BED_REC.BED_NO, ";                                                              // ����
            sql += "SCHEDULE_PERFORM_TIME, ";                                                       // �ƻ�ִ��ʱ��
            sql += "ORDERS_EXECUTE.ORDER_NO, ";                                                     // ҽ�����
            sql += "ORDERS_EXECUTE.ORDER_SUB_NO ";                                                  // ҽ�������

            return GVars.OracleAccess.SelectData(sql);
        }



        /// <summary>
        /// ��ȡδִ�е�ҽ��
        /// </summary>
        /// <param name="dt">Ҫ��ѯ������</param>
        /// <returns></returns>
        private DataSet getOrderNotExecuted(DateTime dtStart, DateTime dtEnd)
        {
            string sql = string.Empty;

            sql += "SELECT PATIENT_INFO.BED_LABEL, ";                                                        // �����
            sql += "ORDERS_EXECUTE.PATIENT_ID, ";                                                   // ���˱�ʶ��
            sql += "PATIENT_INFO.NAME, ";                                                       // ����
            sql += "PATIENT_INFO.SEX, ";                                                        // �Ա�
            sql += "ORDERS_EXECUTE.ORDER_TEXT, ";                                                   // ҽ������
            sql += "ORDERS_EXECUTE.ORDER_CLASS, ";                                                  // ҽ�����

            sql += "(SELECT CLINIC_ITEM_CLASS_DICT.CLASS_NAME FROM CLINIC_ITEM_CLASS_DICT ";
            sql += " WHERE CLINIC_ITEM_CLASS_DICT.CLASS_CODE = ORDERS_EXECUTE.ORDER_CLASS) ORDER_CLASS_NAME, ";

            sql += "ORDERS_EXECUTE.REPEAT_INDICATOR, ";                                             // ����/��ʱ
            sql += "ORDERS_EXECUTE.ORDER_NO, ";                                                     // ҽ�����
            sql += "ORDERS_EXECUTE.ORDER_SUB_NO, ";                                                 // ҽ�������
            sql += "ORDERS_EXECUTE.DOSAGE, ";                                                       // ҩƷһ��ʹ�ü���
            sql += "ORDERS_EXECUTE.DOSAGE_UNITS, ";                                                 // ������λ
            sql += "ORDERS_EXECUTE.ORDERS_PERFORM_SCHEDULE, ";                                      // ҽ��Ĭ��ִ��ʱ��
            sql += "ORDERS_EXECUTE.SCHEDULE_PERFORM_TIME, ";                                        // ҽ��Ĭ��ִ��ʱ��
            sql += "ORDERS_EXECUTE.IS_EXECUTE ";                                                    // �Ƿ�ִ��
            sql += "FROM ORDERS_EXECUTE, ";                                                             // ҽ��ִ�б�
            sql += "PATIENT_INFO ";                                                                 // ��Ժ���˼�¼
            sql += "WHERE ";
            sql += "(ORDERS_EXECUTE.IS_EXECUTE IS NULL OR ORDERS_EXECUTE.IS_EXECUTE = '0') ";       // δִ��
            sql += "AND ORDERS_EXECUTE.PATIENT_ID = PATIENT_INFO.PATIENT_ID ";                  // ���˱�ʶ��
            sql += "AND ORDERS_EXECUTE.VISIT_ID = PATIENT_INFO.VISIT_ID ";                      // ���˱���סԺ��ʶ
            sql += "AND PATIENT_INFO.WARD_CODE = " + SQL.SqlConvert(GVars.User.DeptCode);       // ���ڲ�������
            sql += "AND TO_DATE(ORDERS_EXECUTE.CONVERSION_DATE_TIME) >= " + SQL.GetOraDbDate_Short(dtStart);
            sql += "AND TO_DATE(ORDERS_EXECUTE.CONVERSION_DATE_TIME) <= " + SQL.GetOraDbDate_Short(dtEnd);
            sql += "ORDER BY ";
            sql += "PATIENT_INFO.BED_NO, ";                                                         // ����
            sql += "SCHEDULE_PERFORM_TIME, ";                                                       // �ƻ�ִ��ʱ��
            sql += "ORDERS_EXECUTE.ORDER_NO, ";                                                     // ҽ�����
            sql += "ORDERS_EXECUTE.ORDER_SUB_NO ";                                                  // ҽ�������

            return GVars.OracleAccess.SelectData(sql);
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

                dsOrderExecute = getOrderNotExecuted(dtStart.Value, dtEnd.Value);
                
                if (dsOrderExecute == null || dsOrderExecute.Tables.Count == 0)
                {
                    return;
                }
                ucGridView1.DataSource = dsOrderExecute.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public void Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
            ucGridView1.SelectRow("PATIENT_ID", e.PatientId);
        }

        void IBasePatient.Patient_ListRefresh(object sender, PatientEventArgs e)
        {
            btnQuery_Click(null, null);
        }
    }
}