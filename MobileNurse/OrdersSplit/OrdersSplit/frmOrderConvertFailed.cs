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

        #region 变量
        //private static OrderSplitter  orderConvertor  = new OrderSplitter();  // 医嘱拆分器
        //private static Thread   threadSplitOrder;                             // 加载数据的线程

        //private DataSet         dsOrders = null;
        private DataSet dsErr = null;
        #endregion


        public frmOrderConvertFailed()
        {
            _id = "00017";
            _guid = "3F226D3E-B7BE-4952-9991-ADDA0FA6A96F";

            InitializeComponent();
        }


        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
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
                ucGridView1.Add("病区", "WARD_CODE");
                ucGridView1.Add("病人ID", "PATIENT_ID");
                ucGridView1.Add("VISITID", "VISIT_ID", false);
                ucGridView1.Add("姓名", "NAME");
                ucGridView1.Add("床号", "BED_NO");
                ucGridView1.Add("医嘱序号", "ORDER_NO");
                ucGridView1.Add("子序号", "ORDER_SUB_NO");
                DataTable dt = new DataTable();
                dt.Columns.Add("VALUE");
                dt.Columns.Add("TEXT");
                dt.Rows.Add(new object[] { 1, "长" });
                dt.Rows.Add(new object[] { 0, "临" });
                ucGridView1.Add("长", "REPEAT_INDICATOR", dt, "VALUE", "TEXT");
                ucGridView1.Add("医嘱正文", "ORDER_TEXT");
                ucGridView1.Add("执行频率", "FREQUENCY");
                ucGridView1.Add("频次", "FREQ_COUNTER");
                ucGridView1.Add("频率间隔", "FREQ_INTERVAL");
                ucGridView1.Add("单位", "FREQ_INTERVAL_UNIT");
                ucGridView1.Add("执行时间", "PERFORM_SCHEDULE");
                ucGridView1.Add("拆分情况描述", "SPLIT_MEMO");
                ucGridView1.Add("开始时间", "START_DATE_TIME");
                ucGridView1.Add("结束时间", "STOP_DATE_TIME");
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
        /// 窗体关闭事件
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
        /// 按钮[关闭]
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
        /// 按钮[医嘱拆分]
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


        #region 共通函数
        /// <summary>
        /// 显示拆分失败的医嘱
        /// </summary>
        private void showSplitFailedOrder()
        {
            // 获取拆分失败的医嘱
            string sql = string.Empty;

            #region  HS 添加注释，这个不准确呀
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

            // 显示拆分失败医嘱
            dsErr.Tables[0].DefaultView.Sort = "WARD_CODE, PATIENT_ID, VISIT_ID, ORDER_NO, ORDER_SUB_NO";
            dgvResult.DataSource = dsErr.Tables[0].DefaultView;
            ucGridView1.DataSource = dsErr.Tables[0].DefaultView;
        }


        /// <summary>
        /// 获取病人名称
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
        /// 获取病人的床标号
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="vistId"></param>
        /// <returns></returns>
        private string getPatientBedLabel(string patientId, string visitId)
        {
            string sql = string.Empty;

            sql += "SELECT BED_REC.BED_LABEL ";                                 // 床标号
            sql += "FROM PATS_IN_HOSPITAL, ";                                   // 在院病人记录
            sql += "BED_REC ";                                              // 床位记录
            sql += "WHERE PATS_IN_HOSPITAL.WARD_CODE = BED_REC.WARD_CODE ";     // 所在病房代码
            sql += "AND PATS_IN_HOSPITAL.BED_NO = BED_REC.BED_NO ";         // 床号
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

        #region HS 添加 这里的主要功能是 在打输液贴的时候为了一目了然的发现药疗的医嘱是否有问题
        private void btnclassA_Click(object sender, EventArgs e)
        {
            // 获取拆分失败的医嘱 药疗类
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

            // 显示拆分失败医嘱
            dsErr.Tables[0].DefaultView.Sort = "WARD_CODE, PATIENT_ID, VISIT_ID, ORDER_NO, ORDER_SUB_NO";
            dgvResult.DataSource = dsErr.Tables[0].DefaultView;
            ucGridView1.DataSource = dsErr.Tables[0].DefaultView;
        }

        private void btnClass_Click(object sender, EventArgs e)
        {

            // 获取拆分失败的医嘱  非药疗类
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

            // 显示拆分失败医嘱
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