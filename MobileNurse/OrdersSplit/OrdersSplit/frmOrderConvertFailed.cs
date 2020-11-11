using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using SqlConvert = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class frmOrderConvertFailed : FormDo,IBasePatient
    {

        #region ����
        //private static OrderSplitter  orderConvertor  = new OrderSplitter();  // ҽ�������
        //private static Thread   threadSplitOrder;                             // �������ݵ��߳�

        //private DataSet         dsOrders = null;
        private DataSet dsErr = null;
        #endregion


        public frmOrderConvertFailed()
        {
            _id = "00017";
            _guid = "3F226D3E-B7BE-4952-9991-ADDA0FA6A96F";

            InitializeComponent();
        }


        #region �����¼�
        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmOrderConvertFailed_Load(object sender, EventArgs e)
        {
            try
            {
                dgvResult.AutoGenerateColumns = false;

                showSplitFailedOrder();
                
                ucGridView1.ShowRowIndicator = true;
                ucGridView1.Add("����", "WARD_CODE");
                ucGridView1.Add("����ID", "PATIENT_ID");
                ucGridView1.Add("VISITID", "VISIT_ID", false);
                ucGridView1.Add("����", "NAME");
                ucGridView1.Add("����", "BED_NO");
                ucGridView1.Add("ҽ�����", "ORDER_NO");
                ucGridView1.Add("�����", "ORDER_SUB_NO");
                DataTable dt = new DataTable();
                dt.Columns.Add("VALUE");
                dt.Columns.Add("TEXT");
                dt.Rows.Add(new object[] { 1, "��" });
                dt.Rows.Add(new object[] { 0, "��" });
                ucGridView1.Add("��", "REPEAT_INDICATOR", dt, "VALUE", "TEXT");
                ucGridView1.Add("ҽ������", "ORDER_TEXT");
                ucGridView1.Add("ִ��Ƶ��", "FREQUENCY");
                ucGridView1.Add("Ƶ��", "FREQ_COUNTER");
                ucGridView1.Add("Ƶ�ʼ��", "FREQ_INTERVAL");
                ucGridView1.Add("��λ", "FREQ_INTERVAL_UNIT");
                ucGridView1.Add("ִ��ʱ��", "PERFORM_SCHEDULE");
                ucGridView1.Add("����������", "SPLIT_MEMO");
                ucGridView1.Add("��ʼʱ��", "START_DATE_TIME");
                ucGridView1.Add("����ʱ��", "STOP_DATE_TIME");
                ucGridView1.MultiSelect = true;

                ucGridView1.Init();


                //threadSplitOrder = new Thread(new ThreadStart(orderConvertor.SplitOrders));
                //threadSplitOrder.IsBackground = true;
                //threadSplitOrder.Priority = ThreadPriority.BelowNormal;

                //threadSplitOrder.Start();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// ����ر��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOrderConvertFailed_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //orderConvertor.Exit();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void dgvResult_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }


        private void dgvResult_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dgvResult.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
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
        /// ��ť[ҽ�����]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                showSplitFailedOrder();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion


        #region ��ͨ����
        /// <summary>
        /// ��ʾ���ʧ�ܵ�ҽ��
        /// </summary>
        private void showSplitFailedOrder()
        {
            // ��ȡ���ʧ�ܵ�ҽ��
            string sql = string.Empty;

            #region  HS ���ע�ͣ������׼ȷѽ
            //sql = "SELECT ORDERS.*, "
            //        +    "ORDERS_M.SPLIT_MEMO, "
            //        +    "PAT_MASTER_INDEX.NAME, "
            //        +    "PAT_MASTER_INDEX.SEX, "
            //        +    "PAT_MASTER_INDEX.DATE_OF_BIRTH, "
            //        +    "PATS_IN_HOSPITAL.WARD_CODE, "
            //        +    "PATS_IN_HOSPITAL.BED_NO "
            //     + "FROM ORDERS_M, "
            //        +    "ORDERS, "
            //        +    "PAT_MASTER_INDEX, "
            //        +    "PATS_IN_HOSPITAL "
            //    + "WHERE ORDERS_M.PATIENT_ID = ORDERS.PATIENT_ID "
            //        +    "AND ORDERS_M.VISIT_ID = ORDERS.VISIT_ID "
            //        +    "AND ORDERS_M.ORDER_NO = ORDERS.ORDER_NO "
            //        +    "AND ORDERS_M.ORDER_SUB_NO = ORDERS.ORDER_SUB_NO "
            //        +    "AND ORDERS_M.PATIENT_ID = PAT_MASTER_INDEX.PATIENT_ID "
            //        +    "AND ORDERS_M.PATIENT_ID = PATS_IN_HOSPITAL.PATIENT_ID "
            //        +    "AND ORDERS_M.VISIT_ID = PATS_IN_HOSPITAL.VISIT_ID ";
            #endregion

            sql = "SELECT PATIENT_INFO.NAME, "
                       + "PATIENT_INFO.BED_NO, "
                       + "ORDERS_M.* "
                       + "FROM ORDERS_M, "
                       + "PATIENT_INFO "
                       + "WHERE SPLIT_MEMO IS NOT NULL "
                       + "AND ORDERS_M.PATIENT_ID = PATIENT_INFO.PATIENT_ID "
                       + "AND ORDERS_M.VISIT_ID = PATIENT_INFO.VISIT_ID ";

            if (GVars.User.DeptCode.Length > 0)
            {
                sql += "AND patient_info.WARD_CODE = " + SqlManager.SqlConvert(GVars.User.DeptCode);
            }

            dsErr = GVars.OracleAccess.SelectData(sql);

            // ��ʾ���ʧ��ҽ��
            dsErr.Tables[0].DefaultView.Sort = "WARD_CODE, PATIENT_ID, VISIT_ID, ORDER_NO, ORDER_SUB_NO";
            dgvResult.DataSource = dsErr.Tables[0].DefaultView;
            ucGridView1.DataSource = dsErr.Tables[0].DefaultView;
        }


        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        private string getPatientName(string patientId)
        {
            string sql = "SELECT NAME FROM PAT_MASTER_INDEX WHERE PATIENT_ID = " + SqlConvert.SqlConvert(patientId);

            if (GVars.OracleAccess.SelectValue(sql) == true)
            {
                return GVars.OracleAccess.GetResult(0);
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// ��ȡ���˵Ĵ����
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="vistId"></param>
        /// <returns></returns>
        private string getPatientBedLabel(string patientId, string visitId)
        {
            string sql = string.Empty;

            sql += "SELECT BED_REC.BED_LABEL ";                                 // �����
            sql += "FROM PATS_IN_HOSPITAL, ";                                   // ��Ժ���˼�¼
            sql += "BED_REC ";                                              // ��λ��¼
            sql += "WHERE PATS_IN_HOSPITAL.WARD_CODE = BED_REC.WARD_CODE ";     // ���ڲ�������
            sql += "AND PATS_IN_HOSPITAL.BED_NO = BED_REC.BED_NO ";         // ����
            sql += "AND PATS_IN_HOSPITAL.PATIENT_ID = " + SqlConvert.SqlConvert(patientId);
            sql += "AND PATS_IN_HOSPITAL.VISIT_ID = " + SqlConvert.SqlConvert(visitId);

            if (GVars.OracleAccess.SelectValue(sql) == true)
            {
                return GVars.OracleAccess.GetResult(0);
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        #region HS ��� �������Ҫ������ �ڴ���Һ����ʱ��Ϊ��һĿ��Ȼ�ķ���ҩ�Ƶ�ҽ���Ƿ�������
        private void btnclassA_Click(object sender, EventArgs e)
        {
            // ��ȡ���ʧ�ܵ�ҽ�� ҩ����
            string sql = string.Empty;
            sql = "SELECT PATIENT_INFO.NAME, "
                    + "PATIENT_INFO.BED_NO, "
                    + "ORDERS_M.* "
                    + "FROM ORDERS_M, "
                    + "PATIENT_INFO "
                    + "WHERE SPLIT_MEMO IS NOT NULL "
                    + "AND ORDERS_M.ORDER_CLASS ='A'"
                    + "AND ORDERS_M.PATIENT_ID = PATIENT_INFO.PATIENT_ID "
                    + "AND ORDERS_M.VISIT_ID = PATIENT_INFO.VISIT_ID ";

            if (GVars.User.DeptCode.Length > 0)
            {
                sql += "AND patient_info.WARD_CODE = " + SqlManager.SqlConvert(GVars.User.DeptCode);
            }

            dsErr = GVars.OracleAccess.SelectData(sql);

            // ��ʾ���ʧ��ҽ��
            dsErr.Tables[0].DefaultView.Sort = "WARD_CODE, PATIENT_ID, VISIT_ID, ORDER_NO, ORDER_SUB_NO";
            dgvResult.DataSource = dsErr.Tables[0].DefaultView;
            ucGridView1.DataSource = dsErr.Tables[0].DefaultView;
        }

        private void btnClass_Click(object sender, EventArgs e)
        {

            // ��ȡ���ʧ�ܵ�ҽ��  ��ҩ����
            string sql = string.Empty;
            sql = "SELECT PATIENT_INFO.NAME, "
                    + "PATIENT_INFO.BED_NO, "
                    + "ORDERS_M.* "
                    + "FROM ORDERS_M, "
                    + "PATIENT_INFO "
                    + "WHERE SPLIT_MEMO IS NOT NULL "
                    + "AND ORDERS_M.ORDER_CLASS !='A'"
                    + "AND ORDERS_M.PATIENT_ID = PATIENT_INFO.PATIENT_ID "
                    + "AND ORDERS_M.VISIT_ID = PATIENT_INFO.VISIT_ID ";

            if (GVars.User.DeptCode.Length > 0)
            {
                sql += "AND patient_info.WARD_CODE = " + SqlManager.SqlConvert(GVars.User.DeptCode);
            }

            dsErr = GVars.OracleAccess.SelectData(sql);

            // ��ʾ���ʧ��ҽ��
            dsErr.Tables[0].DefaultView.Sort = "WARD_CODE, PATIENT_ID, VISIT_ID, ORDER_NO, ORDER_SUB_NO";
            dgvResult.DataSource = dsErr.Tables[0].DefaultView;
            ucGridView1.DataSource = dsErr.Tables[0].DefaultView;
        }
        #endregion

        public void Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
            ucGridView1.SelectRow("PATIENT_ID", e.PatientId);     
        }

        void IBasePatient.Patient_ListRefresh(object sender, PatientEventArgs e)
        {
             
        }
    }
}