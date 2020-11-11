// ***********************************************************************
// Assembly         : OrdersExecuteQuery
// Author           : LPD
// Created          : 10-24-2015
//
// Last Modified By : LPD
// Last Modified On : 10-26-2015
// ***********************************************************************
// <copyright file="OrderExecuteQueryNew.cs" company="陕西天健源达信息科技有限公司">
//     Copyright (c) 陕西天健源达信息科技有限公司. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.Grid;

/// <summary>
/// The HISPlus namespace.
/// </summary>
namespace HISPlus
{
    /// <summary>
    /// Class OrderExecuteQueryNew.
    /// </summary>
    public partial class OrderExecuteQueryNew : FormDo, IBasePatient
    {
        private string patientId = string.Empty;                        // 病人标识号
        private string visitId = string.Empty;                          // 住院标识
        private string bed_No = string.Empty;  //床号
        private DataSet dsPatient = null;                     // 病人信息




        /// <summary>
        /// Initializes a new instance of the <see cref="OrderExecuteQueryNew"/> class.
        /// </summary>
        public OrderExecuteQueryNew()
        {
            _id = "00018";
            _guid = "1E1EDFF1-8E58-45cd-85BD-70B20C3B94D1";
            InitializeComponent();
            dataStart.Text = DateTime.Now.ToString("yyyy-MM-dd 00:00");
            dataEnd.Text = DateTime.Now.ToString("yyyy-MM-dd 23:59");
            cmb_type.SelectedIndex = 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSearch_Click(object sender, EventArgs e)
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

            gControlData.DataSource = dtDataSource;

        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private DataTable GetGridDate(string sqlFiltrate)
        {
            try
            {
                //取出当前科室数据
                string sqlStr = @"SELECT P.PATIENT_ID,P.BED_NO,P.NAME,B.ORDER_NO,B.ORDER_SUB_NO,B.SCHEDULE_PERFORM_TIME,
                                CASE B.REPEAT_INDICATOR WHEN 1 THEN '长医' WHEN 0 THEN '临医' END CLY,
                                B.ORDER_TEXT,B.FREQUENCY,B.ADMINISTRATION,B.NURSE,
                                B.EXECUTE_DATE_TIME,B.EXECUTE_NURSE,B.DOSAGE, B.DOSAGE_UNITS FROM 
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
                    //string strStartDate = Convert.ToDateTime(dataStart.Text).ToString("yyyy-MM-dd") + " 00:00:00";
                    //string strEndDate = Convert.ToDateTime(dataEnd.Text).AddDays(1).ToString("yyyy-MM-dd") + " 00:00:00";

                    string strStartDate = Convert.ToDateTime(dataStart.Text).ToString("yyyy-MM-dd HH:mm");
                    string strEndDate = Convert.ToDateTime(dataEnd.Text).ToString("yyyy-MM-dd HH:mm");

                    sqlStr += " AND B.SCHEDULE_PERFORM_TIME >= TO_DATE('" + strStartDate + "','yyyy-MM-dd HH24:mi')   " +
                               "  AND B.SCHEDULE_PERFORM_TIME< TO_DATE('" + strEndDate + "','yyyy-MM-dd HH24:mi')";
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
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ds.Tables[0].Rows[i]["check"] = "True";
                }
                return ds.Tables[0];


            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderExecuteQueryNew_Load(object sender, EventArgs e)
        {
            initFrmVal();

            SetGridViewStyle();

            LoadAdministrationTransfuse();


            gControlData.DataSource = GetGridDate("");
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
        /// 设置GridView样式
        /// </summary>
        /// <returns>System.String.</returns>
        public void SetGridViewStyle()
        {

            DevExpress.XtraGrid.Views.Grid.GridViewAppearances Appearance1 = new DevExpress.XtraGrid.Views.Grid.GridViewAppearances(gViewData);


            gViewData.OptionsView.ShowGroupPanel = false;   //去掉默认字
            gViewData.IndicatorWidth = 35;
            gViewData.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;  //取消选中时的虚拟框
            gViewData.OptionsCustomization.AllowColumnMoving = false;  //禁止列头移动
            //gViewData.OptionsBehavior.Editable = false;      //单元格不可编辑
            gViewData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect; //选择行，不选择单元格
            gViewData.OptionsSelection.EnableAppearanceFocusedCell = false;
            //this.gViewData.Appearance.OddRow.BackColor = Color.White;  // 设置奇数行颜色 // 默认也是白色 可以省略 
            //this.gViewData.OptionsView.EnableAppearanceOddRow = true;   // 使能 // 和和上面绑定 同时使用有效 
            //this.gViewData.Appearance.EvenRow.BackColor = Color.WhiteSmoke; // 设置偶数行颜色 
            //this.gViewData.OptionsView.EnableAppearanceEvenRow = true;   // 使能 // 和和上面绑定 同时使用有效
            gViewData.OptionsMenu.EnableColumnMenu = false;
            gViewData.OptionsMenu.EnableFooterMenu = false;
            gViewData.OptionsMenu.EnableGroupPanelMenu = false;
            gViewData.OptionsBehavior.Editable = true;
            gViewData.OptionsCustomization.AllowSort = false;   //禁用列头排序
            gViewData.OptionsCustomization.AllowFilter = false; //禁用筛选
            gViewData.OptionsSelection.MultiSelect = true;      //允许选择多行

            //gViewData.OptionsCustomization.AllowGroup = true;     //是否允许分组
            //gViewData.OptionsView.ShowGroupedColumns = true;     //显示分组的列
            //gViewData.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "ORDER_NO");  //添加分组1，如果不是count，则名称必须与字段名对应            
            //gViewData.Columns["ORDER_NO"].GroupIndex = 0;  //设置默认分组列
            //gViewData.Appearance.GroupRow.BackColor = Appearance1.GroupRow.BackColor;
            ////分组row颜色
            //Appearance1.GroupRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(232)))), ((int)(((byte)(243)))));
            //Appearance1.GroupRow.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(232)))), ((int)(((byte)(243)))));
            ////空白区域颜色
            //Appearance1.Empty.BackColor = System.Drawing.Color.LightYellow;
        }

        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gViewData_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
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
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {

            DataTable dt = gControlData.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0)
            {
                MessageBox.Show("无数据,不允许打印");
                return;
            }

            string value = string.Empty;
            int Selected = 0;      //记录选中人员

            //GridControl 数据源
            DataTable dtControlData = gControlData.DataSource as DataTable;
            //
            DataTable dtNewData = dtControlData.Clone();
            #region MyRegion
            dtNewData.Columns.Remove("check");
            dtNewData.Columns["BED_NO"].ColumnName = "BED_NOP";
            dtNewData.Columns["NAME"].ColumnName = "NAMEP";
            dtNewData.Columns["CLY"].ColumnName = "CLYP";
            dtNewData.Columns["ORDER_TEXT"].ColumnName = "ORDER_TEXTP";
            dtNewData.Columns["FREQUENCY"].ColumnName = "FREQUENCYP";
            dtNewData.Columns["ADMINISTRATION"].ColumnName = "ADMINISTRATIONP";
            dtNewData.Columns["NURSE"].ColumnName = "NURSEP";
            dtNewData.Columns["EXECUTE_DATE_TIME"].ColumnName = "EXECUTE_DATE_TIMEP";
            dtNewData.Columns["EXECUTE_NURSE"].ColumnName = "EXECUTE_NURSEP";
            #endregion

            for (int i = 0; i < gViewData.RowCount; i++)
            {
                value = gViewData.GetDataRow(i)["check"].ToString();
                if (value == "True")
                {
                    DataRow dr = dtNewData.NewRow();
                    dr["BED_NOP"] = gViewData.GetRowCellValue(i, "BED_NO");
                    dr["NAMEP"] = gViewData.GetRowCellValue(i, "NAME");
                    dr["CLYP"] = gViewData.GetRowCellValue(i, "CLY");
                    dr["ORDER_TEXTP"] = gViewData.GetRowCellValue(i, "ORDER_TEXT");
                    dr["FREQUENCYP"] = gViewData.GetRowCellValue(i, "FREQUENCY");
                    dr["ADMINISTRATIONP"] = gViewData.GetRowCellValue(i, "ADMINISTRATION");
                    dr["NURSEP"] = gViewData.GetRowCellValue(i, "NURSE");
                    dr["EXECUTE_DATE_TIMEP"] = gViewData.GetRowCellValue(i, "EXECUTE_DATE_TIME");
                    dr["EXECUTE_NURSEP"] = gViewData.GetRowCellValue(i, "EXECUTE_NURSE");
                    Selected++;

                    dtNewData.Rows.Add(dr);
                }
            }

            if (Selected > 0)
            {
                //打印选中行
                gControlPrint.DataSource = dtNewData;


                PrintSettingController melc = new PrintSettingController(this.gControlPrint);
                string strStartDate = Convert.ToDateTime(dataStart.Text).ToString("yyyy-MM-dd");
                string strEndDate = Convert.ToDateTime(dataEnd.Text).ToString("yyyy-MM-dd");
                //页眉
                melc.PrintHeader = GVars.User.DeptName + strStartDate + " 至 " + strEndDate + "医嘱执行单"; ;

                //页脚
                melc.PrintFooter = "时间:" + DateTime.Now.ToString() + "     制表人:" + GVars.User.Name;

                //横纵向  false:默认的竖向打印   true:横向打印
                melc.LandScape = false;

                //纸型
                melc.PaperKind = System.Drawing.Printing.PaperKind.A4;
                //加载页面设置信息
                melc.LoadPageSetting();
                melc.Preview();
            }
            else
            {
                if (Selected <= 0)
                {
                    MessageBox.Show("未选择数据,不允许打印.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //
                DataTable dtPrintALlData = dtControlData.Copy();
                dtPrintALlData.Columns.Remove("check");
                dtPrintALlData.Columns["BED_NO"].ColumnName = "BED_NOP";
                dtPrintALlData.Columns["NAME"].ColumnName = "NAMEP";
                dtPrintALlData.Columns["CLY"].ColumnName = "CLYP";
                dtPrintALlData.Columns["ORDER_TEXT"].ColumnName = "ORDER_TEXTP";
                dtPrintALlData.Columns["FREQUENCY"].ColumnName = "FREQUENCYP";
                dtPrintALlData.Columns["ADMINISTRATION"].ColumnName = "ADMINISTRATIONP";
                dtPrintALlData.Columns["NURSE"].ColumnName = "NURSEP";
                dtPrintALlData.Columns["EXECUTE_DATE_TIME"].ColumnName = "EXECUTE_DATE_TIMEP";
                dtPrintALlData.Columns["EXECUTE_NURSE"].ColumnName = "EXECUTE_NURSEP";
                gControlPrint.DataSource = dtPrintALlData;
                //打印全部                
                PrintSettingController psc = new PrintSettingController(this.gControlPrint);

                string strStartDate = Convert.ToDateTime(dataStart.Text).ToString("yyyy-MM-dd");
                string strEndDate = Convert.ToDateTime(dataEnd.Text).ToString("yyyy-MM-dd");
                //页眉
                psc.PrintHeader = GVars.User.DeptName + strStartDate + " 至 " + strEndDate + "医嘱执行单"; ;

                //页脚
                psc.PrintFooter = "时间:" + DateTime.Now.ToString() + "     制表人:" + GVars.User.Name;

                //横纵向  false:默认的竖向打印   true:横向打印
                psc.LandScape = false;

                //纸型
                psc.PaperKind = System.Drawing.Printing.PaperKind.A4;
                //加载页面设置信息
                psc.LoadPageSetting();
                psc.Preview();
            }


        }

        /// <summary>
        /// 选择全科
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chk_QK_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt = GetGridDate(" AND P.WARD_CODE='" + GVars.User.DeptCode + "'");

            gControlData.DataSource = dt;

        }


        /// <summary>
        /// 选择列表病人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IBasePatient.Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
            //出院病人不能插入新纪录
            if (GVars.Patient.STATE == HISPlus.PAT_INHOS_STATE.OUT)
            {
                MessageBox.Show("出院病人不能操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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

                gControlData.DataSource = dt;

                SelectRow("PATIENT_ID", patientId);
            }
            else
            {
                dt = GetGridDate(" AND P.PATIENT_ID='" + patientId + "' AND P.WARD_CODE='" + GVars.User.DeptCode + "'");
                gControlData.DataSource = dt;
            }
        }

        public void SelectRow(string fieldName, object value)
        {
            //if (gvDefault.FocusedRowHandle > -1
            //    && gvDefault.GetRowCellValue(gvDefault.FocusedRowHandle, fieldName).Equals(value))
            //    return;

            gViewData.ClearSelection();
            bool focusFirst = true;
            for (int rowHandle = 0; rowHandle < gViewData.RowCount; rowHandle++)
            {
                for (int j = 0; j < gViewData.Columns.Count; j++)
                {
                    if (gViewData.GetRowCellValue(rowHandle, fieldName).Equals(value))
                    {
                        if (focusFirst)
                        {
                            focusFirst = false;
                            gViewData.FocusedRowHandle = rowHandle;
                        }
                        gViewData.SelectRow(rowHandle);
                        if (gViewData.IsMultiSelect)
                            break;
                        return;
                    }
                }
            }
        }


        Color lastColor = new Color();
        private void gViewData_RowStyle(object sender, RowStyleEventArgs e)
        {
            //#region Demo

            //string lastOrderNoValue = string.Empty;
            //string lastOrderSubNo = string.Empty;
            //string nowOrderNoValue = string.Empty;
            //string nowOrderSubNo = string.Empty;
            //string lastSchedulePerformTime = string.Empty;
            //string nowSchedulePerformTime = string.Empty;
            ////Color lastColor = Color.AliceBlue;  //上一行的行颜色
            ////Color newColor = Color.White;  //新行颜色
            ////Color nowColor;   //

            ////遍历数据源的行
            //if (e.RowHandle != -1)
            //{
            //    if (e.RowHandle == 0)
            //    {
            //        lastOrderNoValue = gViewData.GetRowCellValue(e.RowHandle, "ORDER_NO").ToString();  //12
            //        lastOrderSubNo = gViewData.GetRowCellValue(e.RowHandle, "ORDER_SUB_NO").ToString();  //12
            //        nowOrderNoValue = gViewData.GetRowCellValue(e.RowHandle, "ORDER_NO").ToString();  //12
            //        nowOrderSubNo = gViewData.GetRowCellValue(e.RowHandle, "ORDER_SUB_NO").ToString();  //12
            //        lastSchedulePerformTime = gViewData.GetRowCellValue(e.RowHandle, "SCHEDULE_PERFORM_TIME").ToString();
            //        nowSchedulePerformTime = gViewData.GetRowCellValue(e.RowHandle, "SCHEDULE_PERFORM_TIME").ToString();
            //    }
            //    else
            //    {
            //        lastOrderNoValue = gViewData.GetRowCellValue(e.RowHandle - 1, "ORDER_NO").ToString();  //12
            //        lastOrderSubNo = gViewData.GetRowCellValue(e.RowHandle - 1, "ORDER_SUB_NO").ToString();  //12
            //        lastSchedulePerformTime = gViewData.GetRowCellValue(e.RowHandle - 1, "SCHEDULE_PERFORM_TIME").ToString();
            //        nowOrderNoValue = gViewData.GetRowCellValue(e.RowHandle, "ORDER_NO").ToString();  //12
            //        nowOrderSubNo = gViewData.GetRowCellValue(e.RowHandle, "ORDER_SUB_NO").ToString();  //12                    
            //        nowSchedulePerformTime = gViewData.GetRowCellValue(e.RowHandle, "SCHEDULE_PERFORM_TIME").ToString();
            //    }

            //    //与上条数据ORDER_NO一致，ORDER_SUB_NO一致，改变颜色
            //    //与上条数据ORDER_NO一致，ORDER_SUB_NO不一致，与上条数据颜色相同
            //    //与上条数据ORDER_NO不一致，与上条颜色不同

            //    if (lastOrderNoValue == nowOrderNoValue && lastOrderSubNo == nowOrderSubNo)
            //    {
            //        if (e.RowHandle == 0)
            //        {
            //            e.Appearance.BackColor = Color.White;
            //        }
            //        else
            //        {
            //            e.Appearance.BackColor = (lastColor == Color.White) ? Color.AliceBlue : Color.White;
            //        }

            //    }
            //    else if (lastOrderNoValue == nowOrderNoValue && lastOrderSubNo != nowOrderSubNo && lastSchedulePerformTime == nowSchedulePerformTime)
            //    {
            //        //与上条数据颜色相同
            //        if (e.RowHandle == 0)
            //        {
            //            e.Appearance.BackColor = Color.White;
            //        }
            //        else
            //        {
            //            e.Appearance.BackColor = lastColor;
            //        }
            //    }

            //    else if (lastOrderNoValue == nowOrderNoValue && lastOrderSubNo != nowOrderSubNo && lastSchedulePerformTime != nowSchedulePerformTime)
            //    {
            //        //与上条数据颜色不同
            //        if (e.RowHandle == 0)
            //        {
            //            e.Appearance.BackColor = Color.White;
            //        }
            //        else
            //        {
            //            e.Appearance.BackColor = (lastColor == Color.White) ? Color.AliceBlue : Color.White;
            //        }
            //    }
            //    else if (lastOrderNoValue != nowOrderNoValue)
            //    {
            //        //与上条颜色不同
            //        if (e.RowHandle == 0)
            //        {
            //            e.Appearance.BackColor = Color.White;
            //        }
            //        else
            //        {
            //            e.Appearance.BackColor = (lastColor == Color.White) ? Color.AliceBlue : Color.White;
            //        }
            //    }
            //    lastColor = e.Appearance.BackColor;
            //}

            //#endregion


        }

        //长医
        private void chkCq_CheckedChanged(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }

        /// <summary>
        /// 临医
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkLs_CheckedChanged(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.CheckState == CheckState.Checked)
            {
                //取消选中状态
                DataTable dt = gControlData.DataSource as DataTable;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["CHECK"] = "true";
                }
                gControlData.DataSource = dt;
            }
            else
            {
                DataTable dt = gControlData.DataSource as DataTable;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["CHECK"] = "false";
                }
                gControlData.DataSource = dt;
            }



        }


        /// <summary>
        /// 右键刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IBasePatient.Patient_ListRefresh(object sender, PatientEventArgs e)
        {
            btnSearch_Click(null, null);
        }

        private void cmb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }


    }

    /// <summary>
    /// 打印类
    /// </summary>
    public class PrintSettingController
    {
        PrintingSystem ps = null;
        string formName = null;
        PrintableComponentLink link = null;

        string _PrintHeader = null;
        /// <summary>
        /// 打印时的标题
        /// </summary>
        public string PrintHeader
        {
            set
            {
                _PrintHeader = value;
            }
        }

        string _PrintFooter = null;
        /// <summary>
        /// 打印时页脚

        /// </summary>
        public string PrintFooter
        {
            set
            {
                _PrintFooter = value;
            }
        }

        bool _landScape;
        /// <summary>
        /// 页面横纵向
        /// false:默认的竖向打印   true:横向打印
        /// </summary>
        public bool LandScape
        {
            set { _landScape = value; }
        }

        System.Drawing.Printing.PaperKind _paperKind;
        /// <summary>
        /// 纸型
        /// </summary>
        public System.Drawing.Printing.PaperKind PaperKind
        {
            set { _paperKind = value; }
        }

        /// <summary>
        /// 打印控制器

        /// </summary>
        /// <param name="control">要打印的部件</param>
        public PrintSettingController(IPrintable control)
        {
            if (control == null) return;
            Control c = (Control)control;
            formName = c.FindForm().GetType().FullName + "." + c.Name;
            ps = new DevExpress.XtraPrinting.PrintingSystem();
            link = new DevExpress.XtraPrinting.PrintableComponentLink(ps);
            ps.Links.Add(link);
            link.Component = control;
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        public void Preview()
        {
            try
            {
                //if (DevExpress.Xpf.Printing.PrintHelper.IsPrintingAvailable)
                //{
                Cursor.Current = Cursors.AppStarting;
                if (_PrintHeader != null)
                {
                    PageHeaderFooter phf = link.PageHeaderFooter as PageHeaderFooter;

                    //设置页眉
                    phf.Header.Content.Clear();
                    phf.Header.Content.AddRange(new string[] { "", _PrintHeader, "" });
                    phf.Header.Font = new System.Drawing.Font("宋体", 14, System.Drawing.FontStyle.Bold);
                    phf.Header.LineAlignment = BrickAlignment.Center;

                    //设置页脚
                    phf.Footer.Content.Clear();
                    phf.Footer.Content.AddRange(new string[] { "", "", _PrintFooter });
                    phf.Footer.Font = new System.Drawing.Font("宋体", 9, System.Drawing.FontStyle.Regular);
                    phf.Footer.LineAlignment = BrickAlignment.Center;

                }
                link.PaperKind = ps.PageSettings.PaperKind;
                link.Margins = ps.PageSettings.Margins;
                link.Landscape = ps.PageSettings.Landscape;
                link.CreateDocument();

                //汉化
                //DevExpress.XtraPrinting.Localization.PreviewLocalizer.Active = new Dxperience.LocalizationCHS.DxperienceXtraPrintingLocalizationCHS();
                ps.PreviewFormEx.Show();

                //}
                //else
                //{
                //    Cursor.Current = Cursors.Default;
                //    MessageBox.Show("打印机不可用 ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            try
            {
                //if (DevExpress.XtraPrinting.PrintHelper.IsPrintingAvailable)
                //{
                if (_PrintHeader != null)
                {
                    PageHeaderFooter phf = link.PageHeaderFooter as PageHeaderFooter;

                    //设置页眉
                    phf.Header.Content.Clear();
                    phf.Header.Content.AddRange(new string[] { "", _PrintHeader, "" });
                    phf.Header.Font = new System.Drawing.Font("宋体", 14, System.Drawing.FontStyle.Bold);
                    phf.Header.LineAlignment = BrickAlignment.Center;

                    //设置页脚
                    phf.Footer.Content.Clear();
                    phf.Footer.Content.AddRange(new string[] { "", "", _PrintFooter });
                    phf.Footer.Font = new System.Drawing.Font("宋体", 9, System.Drawing.FontStyle.Regular);
                    phf.Footer.LineAlignment = BrickAlignment.Center;

                }
                link.PaperKind = ps.PageSettings.PaperKind;
                link.Margins = ps.PageSettings.Margins;
                link.Landscape = ps.PageSettings.Landscape;
                link.CreateDocument();
                link.CreateDocument();
                //汉化
                // DevExpress.XtraPrinting.Localization.PreviewLocalizer.Active = new Dxperience.LocalizationCHS.DxperienceXtraPrintingLocalizationCHS();
                ps.Print();
                //}
                //else
                //{
                //    Cursor.Current = Cursors.Default;
                //    MessageBox.Show("打印机不可用 ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {

            }
            finally
            {
            }
        }

        //获取页面设置信息
        public void LoadPageSetting()
        {

            System.Drawing.Printing.Margins margins = new System.Drawing.Printing.Margins(60, 60, 60, 60);
            ps.PageSettings.Assign(margins, _paperKind, _landScape);

        }
    }
}