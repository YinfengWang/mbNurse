using System;
using System.Data;
using System.Windows.Forms;
using HISPlus.UserControls;
using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class OrderNotExecuted : FormDo, IBasePatient
    {
        #region 窗体变量
        public DataSet dsOrderExecute = null;                           // 医嘱执行单
        #endregion

        public OrderNotExecuted()
        {
            _id = "00019";
            _guid = "CE91973F-311E-470e-80A0-6D84D7228FC8";

            InitializeComponent();
        }


        /// <summary>
        /// 窗体加载事件
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
            ucGridView1.Add("床号", "BED_LABEL", 30, ColumnStatus.Unique);
            ucGridView1.Add("姓名", "NAME", 50, ColumnStatus.Unique);
            ucGridView1.Add("性别", "SEX", 30, ColumnStatus.Unique);
            ucGridView1.Add("类别", "ORDER_CLASS_NAME", 30, ColumnStatus.Unique);
            DataTable dt = new DataTable();
            dt.Columns.Add("VALUE");
            dt.Columns.Add("TEXT");
            dt.Rows.Add(new object[] { 1, "长" });
            dt.Rows.Add(new object[] { 0, "临" });
            ucGridView1.Add("长", "REPEAT_INDICATOR", dt, "VALUE", "TEXT", 30);
            ucGridView1.Add("主", "ORDER_NO",30);
            ucGridView1.Add("子", "ORDER_SUB_NO", 30);
            ucGridView1.Add("医嘱", "ORDER_TEXT", 200);
            ucGridView1.Add("剂量", "DOSAGE", 50);
            ucGridView1.Add("单位", "DOSAGE_UNITS", 30);
            ucGridView1.Add("时间点", "SCHEDULE_PERFORM_TIME", ComConst.FMT_DATE.LONG, 120);

            ucGridView1.Init();
        }

        /// <summary>
        /// 获取未执行的医嘱
        /// </summary>
        /// <param name="dt">要查询的日期</param>
        /// <returns></returns>
        private DataSet getOrderNotExecuted_Old(DateTime dtStart, DateTime dtEnd)
        {
            string sql = string.Empty;

            sql += "SELECT BED_REC.BED_LABEL, ";                                                        // 床标号
            sql += "ORDERS_EXECUTE.PATIENT_ID, ";                                                   // 病人标识号
            sql += "PAT_MASTER_INDEX.NAME, ";                                                       // 姓名
            sql += "PAT_MASTER_INDEX.SEX, ";                                                        // 性别
            sql += "ORDERS_EXECUTE.ORDER_TEXT, ";                                                   // 医嘱正文
            sql += "ORDERS_EXECUTE.ORDER_CLASS, ";                                                  // 医嘱类别

            sql += "(SELECT CLINIC_ITEM_CLASS_DICT.CLASS_NAME FROM CLINIC_ITEM_CLASS_DICT ";
            sql += " WHERE CLINIC_ITEM_CLASS_DICT.CLASS_CODE = ORDERS_EXECUTE.ORDER_CLASS) ORDER_CLASS_NAME, ";

            sql += "ORDERS_EXECUTE.REPEAT_INDICATOR, ";                                             // 长期/临时
            sql += "ORDERS_EXECUTE.ORDER_NO, ";                                                     // 医嘱序号
            sql += "ORDERS_EXECUTE.ORDER_SUB_NO, ";                                                 // 医嘱子序号
            sql += "ORDERS_EXECUTE.DOSAGE, ";                                                       // 药品一次使用剂量
            sql += "ORDERS_EXECUTE.DOSAGE_UNITS, ";                                                 // 剂量单位
            sql += "ORDERS_EXECUTE.ORDERS_PERFORM_SCHEDULE, ";                                      // 医嘱默认执行时间
            sql += "ORDERS_EXECUTE.SCHEDULE_PERFORM_TIME, ";                                        // 医嘱默认执行时间
            sql += "ORDERS_EXECUTE.IS_EXECUTE ";                                                    // 是否执行
            sql += "FROM ORDERS_EXECUTE, ";                                                             // 医嘱执行表
            sql += "PATS_IN_HOSPITAL, ";                                                            // 在院病人记录
            sql += "BED_REC, ";                                                                     // 床位记录
            sql += "PAT_MASTER_INDEX ";                                                             // 病人主索引
            sql += "WHERE ";
            sql += "(ORDERS_EXECUTE.IS_EXECUTE IS NULL OR ORDERS_EXECUTE.IS_EXECUTE = '0') ";       // 未执行
            sql += "AND PATS_IN_HOSPITAL.BED_NO = BED_REC.BED_NO ";                                 // 床号
            sql += "AND PATS_IN_HOSPITAL.WARD_CODE = BED_REC.WARD_CODE ";                           // 所在病房代码
            sql += "AND ORDERS_EXECUTE.PATIENT_ID = PATS_IN_HOSPITAL.PATIENT_ID ";                  // 病人标识号
            sql += "AND ORDERS_EXECUTE.VISIT_ID = PATS_IN_HOSPITAL.VISIT_ID ";                      // 病人本次住院标识
            sql += "AND PATS_IN_HOSPITAL.PATIENT_ID = PAT_MASTER_INDEX.PATIENT_ID ";                // 病人标识号
            sql += "AND PATS_IN_HOSPITAL.WARD_CODE = " + SQL.SqlConvert(GVars.User.DeptCode);       // 所在病房代码
            sql += "AND TO_DATE(ORDERS_EXECUTE.CONVERSION_DATE_TIME) >= " + SQL.GetOraDbDate_Short(dtStart);
            sql += "AND TO_DATE(ORDERS_EXECUTE.CONVERSION_DATE_TIME) <= " + SQL.GetOraDbDate_Short(dtEnd);
            sql += "ORDER BY ";
            sql += "BED_REC.BED_NO, ";                                                              // 床号
            sql += "SCHEDULE_PERFORM_TIME, ";                                                       // 计划执行时间
            sql += "ORDERS_EXECUTE.ORDER_NO, ";                                                     // 医嘱序号
            sql += "ORDERS_EXECUTE.ORDER_SUB_NO ";                                                  // 医嘱子序号

            return GVars.OracleAccess.SelectData(sql);
        }



        /// <summary>
        /// 获取未执行的医嘱
        /// </summary>
        /// <param name="dt">要查询的日期</param>
        /// <returns></returns>
        private DataSet getOrderNotExecuted(DateTime dtStart, DateTime dtEnd)
        {
            string sql = string.Empty;

            sql += "SELECT PATIENT_INFO.BED_LABEL, ";                                                        // 床标号
            sql += "ORDERS_EXECUTE.PATIENT_ID, ";                                                   // 病人标识号
            sql += "PATIENT_INFO.NAME, ";                                                       // 姓名
            sql += "PATIENT_INFO.SEX, ";                                                        // 性别
            sql += "ORDERS_EXECUTE.ORDER_TEXT, ";                                                   // 医嘱正文
            sql += "ORDERS_EXECUTE.ORDER_CLASS, ";                                                  // 医嘱类别

            sql += "(SELECT CLINIC_ITEM_CLASS_DICT.CLASS_NAME FROM CLINIC_ITEM_CLASS_DICT ";
            sql += " WHERE CLINIC_ITEM_CLASS_DICT.CLASS_CODE = ORDERS_EXECUTE.ORDER_CLASS) ORDER_CLASS_NAME, ";

            sql += "ORDERS_EXECUTE.REPEAT_INDICATOR, ";                                             // 长期/临时
            sql += "ORDERS_EXECUTE.ORDER_NO, ";                                                     // 医嘱序号
            sql += "ORDERS_EXECUTE.ORDER_SUB_NO, ";                                                 // 医嘱子序号
            sql += "ORDERS_EXECUTE.DOSAGE, ";                                                       // 药品一次使用剂量
            sql += "ORDERS_EXECUTE.DOSAGE_UNITS, ";                                                 // 剂量单位
            sql += "ORDERS_EXECUTE.ORDERS_PERFORM_SCHEDULE, ";                                      // 医嘱默认执行时间
            sql += "ORDERS_EXECUTE.SCHEDULE_PERFORM_TIME, ";                                        // 医嘱默认执行时间
            sql += "ORDERS_EXECUTE.IS_EXECUTE ";                                                    // 是否执行
            sql += "FROM ORDERS_EXECUTE, ";                                                             // 医嘱执行表
            sql += "PATIENT_INFO ";                                                                 // 在院病人记录
            sql += "WHERE ";
            sql += "(ORDERS_EXECUTE.IS_EXECUTE IS NULL OR ORDERS_EXECUTE.IS_EXECUTE = '0') ";       // 未执行
            sql += "AND ORDERS_EXECUTE.PATIENT_ID = PATIENT_INFO.PATIENT_ID ";                  // 病人标识号
            sql += "AND ORDERS_EXECUTE.VISIT_ID = PATIENT_INFO.VISIT_ID ";                      // 病人本次住院标识
            sql += "AND PATIENT_INFO.WARD_CODE = " + SQL.SqlConvert(GVars.User.DeptCode);       // 所在病房代码
            sql += "AND TO_DATE(ORDERS_EXECUTE.CONVERSION_DATE_TIME) >= " + SQL.GetOraDbDate_Short(dtStart);
            sql += "AND TO_DATE(ORDERS_EXECUTE.CONVERSION_DATE_TIME) <= " + SQL.GetOraDbDate_Short(dtEnd);
            sql += "ORDER BY ";
            sql += "PATIENT_INFO.BED_NO, ";                                                         // 床号
            sql += "SCHEDULE_PERFORM_TIME, ";                                                       // 计划执行时间
            sql += "ORDERS_EXECUTE.ORDER_NO, ";                                                     // 医嘱序号
            sql += "ORDERS_EXECUTE.ORDER_SUB_NO ";                                                  // 医嘱子序号

            return GVars.OracleAccess.SelectData(sql);
        }

        /// <summary>
        /// 按钮[查询]
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