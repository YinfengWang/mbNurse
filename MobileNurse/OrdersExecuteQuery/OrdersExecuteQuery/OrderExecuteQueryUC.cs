using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HISPlus
{
    public partial class OrderExecuteQueryUC : FormDo, IBasePatient
    {
        private DataSet dsPatient = null;                     // 病人信息
        private string patientId = string.Empty;             // 病人ID号
        private string visitId = string.Empty;             // 本次就诊序号

        public OrderExecuteQueryUC()
        {
            _id = "00018";
            _guid = "1E1EDFF1-8E58-45cd-85BD-70B20C3B94D1";
            InitializeComponent();
            dataStart.Text = dataEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
            cmb_type.SelectedIndex = 0;
        }

        private void OrderExecuteQueryUC_Load(object sender, EventArgs e)
        {
            GVars.App.UserInput = false;

            initFrmVal();
            LoadAdministrationTransfuse();
            initDisp();
            
            ucGridView1.DataSource = GetGridDate("");
            GVars.App.UserInput = true;
        }

        /// <summary>
        /// 输液单途径
        /// </summary>
        private void LoadAdministrationTransfuse()
        {
            DataTable dtAdministrationTransfuse = new DataTable();
            string defaultSelect = string.Empty;

            //输注单途径SQL
            string strSqlAdministrationTransfuse = @"SELECT * FROM MOBILE.APP_CONFIG B WHERE B.PARAMETER_CLASS='执行单'
                                        AND B.PARAMETER_NAME='ADMINISTRATION_TRANSFUSE'";

            //默认选中
            string strSqlAdministrationTransfuseSel = @"SELECT B.PARAMETER_VALUE FROM MOBILE.APP_CONFIG B WHERE B.PARAMETER_CLASS='执行单'
                                        AND B.PARAMETER_NAME='ADMINISTRATION_TRANSFUSE_SEL'";

            //输注单途径数据集
            DataSet dsAdministrationTransfuse = GVars.OracleAccess.SelectData(strSqlAdministrationTransfuse);
            //默认选中途径数据集
            DataSet dsAdministrationTransfuseSel = GVars.OracleAccess.SelectData(strSqlAdministrationTransfuseSel);


            if (dsAdministrationTransfuse != null && dsAdministrationTransfuse.Tables[0].Rows.Count > 0)
            {
                dtAdministrationTransfuse = dsAdministrationTransfuse.Tables[0];
            }

            if (dsAdministrationTransfuseSel != null && dsAdministrationTransfuseSel.Tables[0].Rows.Count > 0)
            {
                defaultSelect = dsAdministrationTransfuseSel.Tables[0].Rows[0][0].ToString();
            }

            //默认选中途径字符串
            string getAdministration = dtAdministrationTransfuse.Rows[0]["PARAMETER_VALUE"].ToString();
            for (int i = 0; i < getAdministration.Split(',').Length; i++)
            {
                if (defaultSelect.Split(',').Contains(getAdministration.Split(',')[i]))
                {
                    //默认选中输注单途径
                    chkListTj.Items.Add(getAdministration.Split(',')[i], true);
                }
                else
                {
                    //默认不选中
                    chkListTj.Items.Add(getAdministration.Split(',')[i], false);
                }
            }
        }



        /// <summary>
        /// 初始化窗体变量
        /// </summary>
        private void initFrmVal()
        {
            _userRight = GVars.User.GetUserFrmRight(_id);
            dsPatient = new PatientDbI(GVars.OracleAccess).GetWardPatientList(GVars.User.DeptCode);

        }

        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {
            //ucGridView1.ShowRowIndicator = true;
            ucGridView1.MultiSelect = true;
            ucGridView1.AddCheckBoxColumn("CHECKED");
            ucGridView1.Add("病人ID", "PATIENT_ID", false);
            ucGridView1.Add("住院编号", "VISSIT_NO", false);
            ucGridView1.Add("医嘱号", "ORDER_NO", false);
            ucGridView1.Add("医嘱子序号", "ORDER_SUB_NO", false);

            ucGridView1.Add("床号", "BED_NO", 20);
            ucGridView1.Add("姓名", "NAME", 40);
            ucGridView1.Add("长/临", "REPEAT_INDICATOR", false);
            ucGridView1.Add("医嘱正文", "ORDER_TEXT", 130);

            ucGridView1.Add("频次", "FREQUENCY", 20);
            ucGridView1.Add("途径", "ADMINISTRATION", 40);
            ucGridView1.Add("转抄/校对护士", "NURSE", 40);
            ucGridView1.Add("执行时间", "SCHEDULE_PERFORM_TIME", ComConst.FMT_DATE.LONG_MINUTE, 60);
            ucGridView1.Add("执行护士", "EXECUTE_NURSE", 40);

            ucGridView1.ColumnsEvenOldRowColor = "PATIENT_ID,ORDER_NO,SCHEDULE_PERFORM_TIME";
            ucGridView1.Init();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string strSql = string.Empty;
                if (chkCq.Checked && !chkLs.Checked)
                {
                    //长医
                    strSql += " AND B.REPEAT_INDICATOR=1";
                }
                else if (!chkCq.Checked && chkLs.Checked)
                {
                    //临医
                    strSql += " AND B.REPEAT_INDICATOR=0";
                }

                strSql += "  AND P.WARD_CODE='" + GVars.User.DeptCode + "'";

                DataTable dtDataSource = GetGridDate(strSql);

                if (!dtDataSource.Columns.Contains("CHECKED"))
                {
                    dtDataSource.Columns.Add("CHECKED", typeof(bool));
                }

                foreach (DataRow dr in dtDataSource.Rows)
                {
                    dr["CHECKED"] = true;
                }

                // 显示病人执行单
                dtDataSource.DefaultView.Sort = "BED_NO, ORDER_NO, SCHEDULE_PERFORM_TIME, ORDER_SUB_NO";
                ucGridView1.DataSource = dtDataSource.DefaultView;
                // 打印按钮状态控制
                btnPrint.Enabled = (ucGridView1.RowCount > 0);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private DataTable GetGridDate(string sqlFiltrate)
        {
            try
            {
                //取出当前科室数据
                string sqlStr = @"SELECT 1 SELECTION,P.PATIENT_ID,P.BED_NO,P.NAME,B.ORDER_NO,B.ORDER_SUB_NO,B.SCHEDULE_PERFORM_TIME,
                                CASE B.REPEAT_INDICATOR WHEN 1 THEN '长医' WHEN 0 THEN '临医' END CLY,
                                B.ORDER_TEXT,B.FREQUENCY,B.ADMINISTRATION,B.NURSE,
                                B.EXECUTE_DATE_TIME,B.EXECUTE_NURSE FROM 
                                ORDERS_EXECUTE B
                                INNER JOIN PATIENT_INFO P
                                ON B.PATIENT_ID = P.PATIENT_ID ";



                if (!chk_QK.Checked && !string.IsNullOrEmpty(GVars.Patient.ID))
                    sqlStr += " AND P.PATIENT_ID='" + GVars.Patient.ID + "'";

                if (!string.IsNullOrEmpty(sqlFiltrate))
                    sqlStr += sqlFiltrate;

                if (!string.IsNullOrEmpty(dataStart.Text) && !string.IsNullOrEmpty(dataEnd.Text))
                {
                    //00:00:00 -- 25:59:59 秒
                    string strStartDate = Convert.ToDateTime(dataStart.Text).ToString("yyyy-MM-dd") + " 00:00:00";
                    string strEndDate = Convert.ToDateTime(dataEnd.Text).AddDays(1).ToString("yyyy-MM-dd") + " 00:00:00";
                    sqlStr += " AND B.SCHEDULE_PERFORM_TIME >= TO_DATE('" + strStartDate + "','YYYY-MM-DD HH24:MI:SS')   " +
                              "  AND B.SCHEDULE_PERFORM_TIME< TO_DATE('" + strEndDate + "','YYYY-MM-DD HH24:MI:SS')";
                }
                if (cmb_type.SelectedItem.ToString() == "药疗")
                    sqlStr += " AND B.ORDER_CLASS='A'";
                else if (cmb_type.SelectedItem.ToString() == "非药疗")
                    sqlStr += " AND B.ORDER_CLASS<>'A'";

                //记录选中的途径，拼接成字符串
                string strCheck = string.Empty;
                for (int i = 0; i < chkListTj.Items.Count; i++)
                {
                    if (chkListTj.Items[i].CheckState == CheckState.Checked)
                    {
                        strCheck += "'" + chkListTj.Items[i].Value + "'" + ",";
                    }
                }
                strCheck = strCheck.Length > 0 ? strCheck.Substring(0, strCheck.Length - 1) : strCheck;

                if (cmb_type.SelectedItem.ToString() != "非药疗")
                {
                    if (!string.IsNullOrEmpty(strCheck))
                        sqlStr += " AND B.ADMINISTRATION IN(" + strCheck + ")";
                }


                sqlStr += "  ORDER BY P.BED_NO,B.ORDER_NO,B.SCHEDULE_PERFORM_TIME,B.ORDER_SUB_NO";
                DataSet ds = GVars.OracleAccess.SelectData_NoKey(sqlStr);

                DataColumn dc = new DataColumn("check", typeof(bool));
                ds.Tables[0].Columns.Add(dc);
                //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //{
                //    ds.Tables[0].Rows[i]["check"] = "True";
                //}
                return ds.Tables[0];


            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// 切换病人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IBasePatient.Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
            GVars.Patient.ID = e.PatientId;
            GVars.Patient.VisitId = e.VisitId;
            //GVars.Patient.BedNo = e.

            patientId = GVars.Patient.ID;
            visitId = GVars.Patient.VisitId;
            //bed_No = GVars.Patient.BedNo;
            DataTable dt = new DataTable();
            if (chk_QK.CheckState == CheckState.Checked)
            {
                dt = GetGridDate(" AND P.WARD_CODE='" + GVars.User.DeptCode + "'");

                ucGridView1.DataSource = dt;

                ucGridView1.SelectRow("PATIENT_ID", patientId);
            }
            else
            {
                dt = GetGridDate(" AND P.PATIENT_ID='" + patientId + "' AND P.WARD_CODE='" + GVars.User.DeptCode + "'");
                ucGridView1.DataSource = dt;
            }
        }

        /// <summary>
        /// 全科
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chk_QK_CheckedChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DataTable dt = new DataTable();

            dt = GetGridDate(" AND P.WARD_CODE='" + GVars.User.DeptCode + "'");

            ucGridView1.DataSource = dt;

            this.Cursor = Cursors.Default;
        }

        private void chkCq_CheckedChanged(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }

        private void chkLs_CheckedChanged(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt = ucGridView1.DataSource as DataTable;
            if (ucGridView1 == null || ucGridView1.RowCount <= 0)
            {
                MessageBox.Show("无数据,不允许打印");
                return;
            }


            ////打印选中行
            //ucGridView1.DataSource = dt;


            //PrintSettingController melc = new PrintSettingController(dt);
            //string strStartDate = Convert.ToDateTime(dataStart.Text).ToString("yyyy-MM-dd");
            //string strEndDate = Convert.ToDateTime(dataEnd.Text).ToString("yyyy-MM-dd");
            ////页眉
            //melc.PrintHeader = GVars.User.DeptName + strStartDate + " 至 " + strEndDate + "医嘱执行单"; ;

            ////页脚
            //melc.PrintFooter = "时间:" + DateTime.Now.ToString() + "     制表人:" + GVars.User.Name;

            ////横纵向  false:默认的竖向打印   true:横向打印
            //melc.LandScape = false;

            ////纸型
            //melc.PaperKind = System.Drawing.Printing.PaperKind.A4;
            ////加载页面设置信息
            //melc.LoadPageSetting();
            //melc.Preview();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            DataTable dt = ucGridView1.DataSource as DataTable;
            try
            {
                GVars.App.UserInput = false;

                int result = (chkSelectAll.Checked == true ? 1 : 0);

                this.Cursor = Cursors.WaitCursor;

                foreach (DataRow dr in dt.Rows)
                {
                    dr["SELECTION"] = result;
                }

                ucGridView1.DataSource = dt.DefaultView;

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }




        void IBasePatient.Patient_ListRefresh(object sender, PatientEventArgs e)
        {
            
        }
    }
}