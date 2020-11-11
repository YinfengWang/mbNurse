using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HISPlus.UserControls;
using SqlConvert = HISPlus.SqlManager;

namespace HISPlus
{

    public partial class OrderExecuteQuery : FormDo, IBasePatient
    {
        #region 变量
        private PatientDbI patientCom;                                  // 病人共通

        private DataSet dsPatient = null;                               // 病人信息
        private DataSet dsOrderExecute = null;                          // 医嘱执行单

        private string patientId = string.Empty;                        // 病人标识号
        private string visitId = string.Empty;                          // 住院标识

        private bool blnSuperUser = false;                            // 超级用户
        #endregion


        public OrderExecuteQuery()
        {
            _id = "00018";
            _guid = "1E1EDFF1-8E58-45cd-85BD-70B20C3B94D1";

            InitializeComponent();
        }


        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderExecuteQuery_Load(object sender, EventArgs e)
        {
            try
            {
                patientCom = new PatientDbI(GVars.OracleAccess);
                //ucGridView1.ColumnAutoWidth = false;
                ucGridView1.Add("日期", "CONVERSION_DATE_TIME", ComConst.FMT_DATE.SHORT, 90, ColumnStatus.Unique);
                ucGridView1.Add("类别", "ORDER_CLASS_NAME", string.Empty, 50, ColumnStatus.Unique);
                //ucGridView1.Add("日期", "TIME_POINT");
                DataTable dt = new DataTable();
                dt.Columns.Add("VALUE");
                dt.Columns.Add("TEXT");
                dt.Rows.Add(new object[] { 1, "长" });
                dt.Rows.Add(new object[] { 0, "临" });
                ucGridView1.Add("长", "REPEAT_INDICATOR", dt, "VALUE", "TEXT", 15);
                ucGridView1.Add("子", "ORDER_SUB_NO", 15);
                ucGridView1.Add("医嘱", "ORDER_TEXT", 350);
                ucGridView1.Add("剂量", "DOSAGE", 50);
                ucGridView1.Add("剂量单位", "DOSAGE_UNITS", 80);
                ucGridView1.Add("途径", "ADMINISTRATION", 80);
                ucGridView1.Add("计划执行时间", "SCHEDULE_PERFORM_TIME", ComConst.FMT_DATE.LONG, 160);
                ucGridView1.Add("实际执行时间", "EXECUTE_DATE_TIME", ComConst.FMT_DATE.LONG, 160);
                ucGridView1.Add("执行护士", "EXECUTE_NURSE", 80);
                ucGridView1.Add("延时(小时)", "DELAY", 100);

                ucGridView1.Init();
                // blnSuperUser = userDbI.ChkSuperUser(GVars.User.ID, RIGHT_ID);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
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
        /// 按钮[撤消执行]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelExecute_Click(object sender, EventArgs e)
        {
            try
            {
                // 条件检查
                if (ucGridView1.SelectRowsCount == 0)
                {
                    MessageBox.Show("请选择要撤消执行的医嘱!");
                    return;
                }

                DataRow dr = ucGridView1.SelectedRow;
                if (blnSuperUser == false && dr["EXECUTE_NURSE"].ToString() != GVars.User.Name)
                {
                    MessageBox.Show("您不具有撤消该医嘱执行单的权限! 必须为执行人或具有修改权限!");
                    return;
                }
                //// 检用用户权限
                //int selectedIndex = lvwOrderExecute.SelectedIndices[0];
                //ListViewItem item = lvwOrderExecute.Items[selectedIndex];

                //if (blnSuperUser == false
                //    && item.SubItems[lvwOrderExecute.Columns.Count - 1].Text.Equals(GVars.User.Name) == false)
                //{
                //    MessageBox.Show("您不具有撤消该医嘱执行单的权限! 必须为执行人或具有修改权限!");
                //    return;
                //}

                // 用户确认
                if (MessageBox.Show("您确认要撤消当前医嘱执行吗?", "请确认", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }


                string orderNo = dr["ORDER_NO"].ToString();
                DateTime dtPerformSchedule = (DateTime)(dr["SCHEDULE_PERFORM_TIME"]);

                if (cancelOrdersExecute(patientId, visitId, orderNo, dtPerformSchedule) == true)
                {
                    MessageBox.Show("撤消成功!");

                    // 更新界面显示
                    btnQuery_Click(sender, e);

                    //if (selectedIndex >= lvwOrderExecute.Items.Count)
                    //{
                    //    selectedIndex = lvwOrderExecute.Items.Count - 1;
                    //}

                    //if (selectedIndex >= 0)
                    //{
                    //    lvwOrderExecute.Items[selectedIndex].Selected = true;
                    //    lvwOrderExecute.Items[selectedIndex].EnsureVisible();
                    //}
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 确保为半角输入法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void imeCtrl_GotFocus(object sender, EventArgs e)
        {
            IME.ChangeControlIme(this.ActiveControl.Handle);
        }
        #endregion


        #region 共通函数

        /// <summary>
        /// 获取病人的医嘱执行单
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

            sql += "SELECT BED_REC.BED_LABEL, ";                                                        // 床标号
            sql += "ORDERS_EXECUTE.CONVERSION_DATE_TIME, ";                                         // 转换日期
            sql += "ORDERS_EXECUTE.PATIENT_ID, ";                                                   // 病人标识号
            sql += "PAT_MASTER_INDEX.NAME, ";                                                       // 姓名
            sql += "PAT_MASTER_INDEX.SEX, ";                                                        // 性别
            sql += "ORDERS_EXECUTE.ORDER_TEXT, ";                                                   // 医嘱正文
            sql += "ORDERS_EXECUTE.ORDER_CLASS, ";                                                  // 医嘱类别

            sql += "(SELECT CLINIC_ITEM_CLASS_DICT.CLASS_NAME FROM CLINIC_ITEM_CLASS_DICT ";
            sql += " WHERE CLINIC_ITEM_CLASS_DICT.CLASS_CODE = ORDERS_EXECUTE.ORDER_CLASS) ORDER_CLASS_NAME, ";

            sql += "ORDERS_EXECUTE.REPEAT_INDICATOR, ";                                             // 长期/临时
            sql += "ORDERS_EXECUTE.ORDER_SUB_NO, ";                                                 // 医嘱子序号
            sql += "ORDERS_EXECUTE.ORDER_NO, ";                                                     // 医嘱序号
            sql += "ORDERS_EXECUTE.DOSAGE, ";                                                       // 药品一次使用剂量
            sql += "ORDERS_EXECUTE.DOSAGE_UNITS, ";                                                 // 剂量单位
            sql += "ORDERS_EXECUTE.ORDERS_PERFORM_SCHEDULE, ";                                      // 医嘱默认执行时间
            sql += "ORDERS_EXECUTE.SCHEDULE_PERFORM_TIME, ";                                        // 医嘱默认执行时间
            sql += "ORDERS_EXECUTE.ADMINISTRATION,";                                                // 途径
            sql += "ORDERS_EXECUTE.EXECUTE_DATE_TIME, ";                                            // 实际执行时间
            sql += "ORDERS_EXECUTE.EXECUTE_NURSE, ";                                                // 执行护士
            sql += "ORDERS_EXECUTE.IS_EXECUTE ";                                                    // 是否执行
            sql += "FROM ORDERS_EXECUTE, ";                                                             // 医嘱执行表
            sql += "PATS_IN_HOSPITAL, ";                                                            // 在院病人记录
            sql += "BED_REC, ";                                                                     // 床位记录
            sql += "PAT_MASTER_INDEX ";                                                             // 病人主索引
            sql += "WHERE ";
            sql += "ORDERS_EXECUTE.IS_EXECUTE >= '1' ";                                             // 执行
            sql += "AND PATS_IN_HOSPITAL.BED_NO = BED_REC.BED_NO ";                                 // 床号
            sql += "AND PATS_IN_HOSPITAL.WARD_CODE = BED_REC.WARD_CODE ";                           // 所在病房代码

            sql += "AND ORDERS_EXECUTE.PATIENT_ID = " + SqlConvert.SqlConvert(patientId);           // 病人标识号
            sql += "AND ORDERS_EXECUTE.VISIT_ID = " + SqlConvert.SqlConvert(visitId);               // 病人本次住院标识

            sql += "AND ORDERS_EXECUTE.PATIENT_ID = PATS_IN_HOSPITAL.PATIENT_ID ";                  // 病人标识号
            sql += "AND ORDERS_EXECUTE.VISIT_ID = PATS_IN_HOSPITAL.VISIT_ID ";                      // 病人本次住院标识
            sql += "AND PATS_IN_HOSPITAL.PATIENT_ID = PAT_MASTER_INDEX.PATIENT_ID ";                // 病人标识号
            sql += "AND PATS_IN_HOSPITAL.WARD_CODE = " + SqlConvert.SqlConvert(GVars.User.DeptCode); // 所在病房代码
            sql += "AND " + sqlDateRng;
            sql += "ORDER BY ";
            sql += "ORDERS_EXECUTE.CONVERSION_DATE_TIME, ";                                         // 转换日期
            sql += "ORDERS_EXECUTE.SCHEDULE_PERFORM_TIME, ";                                        // 医嘱默认执行时间
            sql += "ORDERS_EXECUTE.ORDER_NO, ";                                                     // 医嘱序号
            sql += "ORDERS_EXECUTE.ORDER_SUB_NO ";                                                  // 医嘱子序号

            return GVars.OracleAccess.SelectData(sql);
        }


        /// <summary>
        /// 获取病人的医嘱执行单
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

            sql += "SELECT PATIENT_INFO.BED_LABEL, ";                                                        // 床标号
            sql += "ORDERS_EXECUTE.CONVERSION_DATE_TIME, ";                                         // 转换日期
            sql += "ORDERS_EXECUTE.PATIENT_ID, ";                                                   // 病人标识号
            sql += "PATIENT_INFO.NAME, ";                                                       // 姓名
            sql += "PATIENT_INFO.SEX, ";                                                        // 性别
            sql += "ORDERS_EXECUTE.ORDER_TEXT, ";                                                   // 医嘱正文
            sql += "ORDERS_EXECUTE.ORDER_CLASS, ";                                                  // 医嘱类别

            sql += "(SELECT CLINIC_ITEM_CLASS_DICT.CLASS_NAME FROM CLINIC_ITEM_CLASS_DICT ";
            sql += " WHERE CLINIC_ITEM_CLASS_DICT.CLASS_CODE = ORDERS_EXECUTE.ORDER_CLASS) ORDER_CLASS_NAME, ";

            sql += "ORDERS_EXECUTE.REPEAT_INDICATOR, ";                                             // 长期/临时
            sql += "ORDERS_EXECUTE.ORDER_SUB_NO, ";                                                 // 医嘱子序号
            sql += "ORDERS_EXECUTE.ORDER_NO, ";                                                     // 医嘱序号
            sql += "ORDERS_EXECUTE.DOSAGE, ";                                                       // 药品一次使用剂量
            sql += "ORDERS_EXECUTE.DOSAGE_UNITS, ";                                                 // 剂量单位
            sql += "ORDERS_EXECUTE.ORDERS_PERFORM_SCHEDULE, ";                                      // 医嘱默认执行时间
            sql += "ORDERS_EXECUTE.SCHEDULE_PERFORM_TIME, ";                                        // 医嘱默认执行时间
            sql += "ORDERS_EXECUTE.ADMINISTRATION,";
            sql += "round(24*(ORDERS_EXECUTE.EXECUTE_DATE_TIME-ORDERS_EXECUTE.SCHEDULE_PERFORM_TIME),2)as Delay,";                                             // 延时
            sql += "ORDERS_EXECUTE.EXECUTE_DATE_TIME, ";                                            // 实际执行时间
            sql += "ORDERS_EXECUTE.EXECUTE_NURSE, ";                                                // 执行护士
            sql += "ORDERS_EXECUTE.IS_EXECUTE ";                                                    // 是否执行
            sql += "FROM ORDERS_EXECUTE, ";                                                             // 医嘱执行表
            sql += "PATIENT_INFO ";                                                                 // 在院病人记录
            sql += "WHERE ";
            sql += "ORDERS_EXECUTE.IS_EXECUTE >= '1' ";                                             // 执行

            sql += "AND ORDERS_EXECUTE.PATIENT_ID = " + SqlConvert.SqlConvert(patientId);           // 病人标识号
            sql += "AND ORDERS_EXECUTE.VISIT_ID = " + SqlConvert.SqlConvert(visitId);               // 病人本次住院标识

            sql += "AND ORDERS_EXECUTE.PATIENT_ID = PATIENT_INFO.PATIENT_ID ";                  // 病人标识号
            sql += "AND ORDERS_EXECUTE.VISIT_ID = PATIENT_INFO.VISIT_ID ";                      // 病人本次住院标识
            sql += "AND PATIENT_INFO.WARD_CODE = " + SqlConvert.SqlConvert(GVars.User.DeptCode); // 所在病房代码
            sql += "AND " + sqlDateRng;
            sql += "ORDER BY ";
            sql += "ORDERS_EXECUTE.CONVERSION_DATE_TIME, ";                                         // 转换日期
            sql += "ORDERS_EXECUTE.SCHEDULE_PERFORM_TIME, ";                                        // 医嘱默认执行时间
            sql += "ORDERS_EXECUTE.ORDER_NO, ";                                                     // 医嘱序号
            sql += "ORDERS_EXECUTE.ORDER_SUB_NO ";                                                  // 医嘱子序号

            return GVars.OracleAccess.SelectData(sql);
        }



        /// <summary>
        /// 显示医嘱执行记录单
        /// </summary>
        private void showOrderExecute()
        {
            string orderDay = string.Empty;
            string orderClass = string.Empty;

            string schedule = string.Empty;
            string execute = string.Empty;

            try
            {
                //lvwOrderExecute.BeginUpdate();

                //lvwOrderExecute.Items.Clear();

                if (dsOrderExecute == null || dsOrderExecute.Tables.Count == 0)
                {
                    return;
                }

                ucGridView1.DataSource = dsOrderExecute.Tables[0];

                //foreach (DataRow dr in dsOrderExecute.Tables[0].Rows)
                //{
                //    ListViewItem item = null;
                //    DateTime dtConversion = DateTime.Parse(dr["CONVERSION_DATE_TIME"].ToString());

                //    if (orderDay.Equals(dtConversion.ToString(ComConst.FMT_DATE.SHORT)) == false)
                //    {
                //        orderDay = dtConversion.ToString(ComConst.FMT_DATE.SHORT);
                //        orderClass = dr["ORDER_CLASS_NAME"].ToString();                         // 类别

                //        item = new ListViewItem(orderDay);                                      // 日期
                //        item.SubItems.Add(dr["ORDER_CLASS_NAME"].ToString());                   // 类别
                //    }
                //    else
                //    {
                //        item = new ListViewItem(string.Empty);                                  // 床标号

                //        if (orderClass.Equals(dr["ORDER_CLASS_NAME"].ToString()) == false)
                //        {
                //            orderClass = dr["ORDER_CLASS_NAME"].ToString();
                //            item.SubItems.Add(dr["ORDER_CLASS_NAME"].ToString());               // 类别
                //        }
                //        else
                //        {
                //            item.SubItems.Add(string.Empty);                                    // 类别
                //        }
                //    }

                //    item.SubItems.Add("1".Equals(dr["REPEAT_INDICATOR"].ToString()) ? "长" : "临");              // 长期/临时
                //    item.SubItems.Add(dr["ORDER_SUB_NO"].ToString());                           // 医嘱子序号
                //    item.SubItems.Add(dr["ORDER_TEXT"].ToString());                             // 医嘱
                //    item.SubItems.Add(dr["DOSAGE"].ToString() + dr["DOSAGE_UNITS"].ToString()); // 剂量 + 剂量单位
                //    item.SubItems.Add(dr["ADMINISTRATION"].ToString());                         // 途径

                //    schedule = dr["SCHEDULE_PERFORM_TIME"].ToString();
                //    item.SubItems.Add(schedule);                                                // 计划执行时间

                //    if (dr["EXECUTE_DATE_TIME"].ToString().Length > 0)
                //    {
                //        execute = DataType.GetDateTimeLong(dr["EXECUTE_DATE_TIME"].ToString());

                //        item.SubItems.Add(execute);                                                 // 实际执行时间

                //        TimeSpan tmSpan = DateTime.Parse(execute).Subtract(DateTime.Parse(schedule));
                //        item.SubItems.Add(tmSpan.TotalHours.ToString("0.00"));                      // 延时                        
                //    }
                //    else
                //    {
                //        item.SubItems.Add(string.Empty);                                        // 实际执行时间
                //        item.SubItems.Add(string.Empty);                                        // 延时
                //    }

                //    item.SubItems.Add(dr["EXECUTE_NURSE"].ToString());                          // 执行护士

                //    lvwOrderExecute.Items.Add(item);
                //}
            }
            finally
            {
                //lvwOrderExecute.EndUpdate();
            }
        }


        /// <summary>
        /// 撤消医嘱执行
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
            // 获取病人信息
            dsPatient = patientCom.GetPatientInfo_FromID(e.PatientId);
            if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("不存在该病人");
                return;
            }

            patientId = e.PatientId;
            visitId = e.VisitId;

            // 如果找到病人信息
            btnQuery_Click(sender, e);
        }

        void IBasePatient.Patient_ListRefresh(object sender, PatientEventArgs e)
        {
            
        }
    }
}