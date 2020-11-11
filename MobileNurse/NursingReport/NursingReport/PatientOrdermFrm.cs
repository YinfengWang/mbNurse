// ***********************************************************************
// Assembly         : NursingReport
// Author           : LPD
// Created          : 12-28-2015
//
// Last Modified By : LPD
// Last Modified On : 12-28-2015
// ***********************************************************************
// <copyright file="PatientOrdermFrm.cs" company="心医国际(西安)">
//     Copyright (c) 心医国际(西安). All rights reserved.
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

/// <summary>
/// The HISPlus namespace.
/// </summary>
namespace HISPlus
{
    /// <summary>
    /// Class PatientOrdermFrm.
    /// </summary>
    public partial class PatientOrdermFrm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 病人ID
        /// </summary>
        public string patinentID = string.Empty;

        /// <summary>
        /// 住院次数
        /// </summary>
        private string visitID = string.Empty;

        public string resultValue { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientOrdermFrm"/> class.
        /// </summary>
        /// <param name="_patinentID">The _patinent unique identifier.</param>
        /// <param name="_visitID">The _visit unique identifier.</param>
        public PatientOrdermFrm(string _patinentID, string _visitID, string _patName)
        {
            InitializeComponent();
            patinentID = _patinentID;
            visitID = _visitID;
            this.Text += ":" + _patName;
        }

        /// <summary>
        /// 设置GridView样式
        /// </summary>
        /// <returns>System.String.</returns>
        public void SetGridViewStyle()
        {
            //gViewOrdersm.OptionsView.ShowGroupPanel = false;   //去掉默认字
            //gViewOrdersm.IndicatorWidth = 30;
            //gViewOrdersm.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;  //取消选中时的虚拟框
            //gViewOrdersm.OptionsCustomization.AllowColumnMoving = false;  //禁止列头移动
            //gViewOrdersm.OptionsBehavior.Editable = false;      //单元格不可编辑
            //gViewOrdersm.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect; //选择行，不选择单元格
            //gViewOrdersm.OptionsSelection.MultiSelect = true;      //允许选择多行
            //gViewOrdersm.OptionsSelection.EnableAppearanceFocusedCell = false;
            //this.gViewOrdersm.Appearance.OddRow.BackColor = Color.White;  // 设置奇数行颜色 // 默认也是白色 可以省略 
            //this.gViewOrdersm.OptionsView.EnableAppearanceOddRow = true;   // 使能 // 和和上面绑定 同时使用有效 
            //this.gViewOrdersm.Appearance.EvenRow.BackColor = Color.WhiteSmoke; // 设置偶数行颜色 
            //this.gViewOrdersm.OptionsView.EnableAppearanceEvenRow = true;   // 使能 // 和和上面绑定 同时使用有效
            //gViewOrdersm.OptionsMenu.EnableColumnMenu = false;
            //gViewOrdersm.OptionsMenu.EnableFooterMenu = false;
            //gViewOrdersm.OptionsMenu.EnableGroupPanelMenu = false;

            gViewOrdersm.OptionsView.ShowGroupPanel = false;   //去掉默认字
            gViewOrdersm.IndicatorWidth = 35;
            gViewOrdersm.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;  //取消选中时的虚拟框
            gViewOrdersm.OptionsCustomization.AllowColumnMoving = false;  //禁止列头移动
            //gViewData.OptionsBehavior.Editable = false;      //单元格不可编辑
            gViewOrdersm.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect; //选择行，不选择单元格
            gViewOrdersm.OptionsSelection.EnableAppearanceFocusedCell = false;
            //this.gViewData.Appearance.OddRow.BackColor = Color.White;  // 设置奇数行颜色 // 默认也是白色 可以省略 
            //this.gViewData.OptionsView.EnableAppearanceOddRow = true;   // 使能 // 和和上面绑定 同时使用有效 
            //this.gViewData.Appearance.EvenRow.BackColor = Color.WhiteSmoke; // 设置偶数行颜色 
            //this.gViewData.OptionsView.EnableAppearanceEvenRow = true;   // 使能 // 和和上面绑定 同时使用有效
            gViewOrdersm.OptionsMenu.EnableColumnMenu = false;
            gViewOrdersm.OptionsMenu.EnableFooterMenu = false;
            gViewOrdersm.OptionsMenu.EnableGroupPanelMenu = false;
            gViewOrdersm.OptionsBehavior.Editable = true;
            gViewOrdersm.OptionsCustomization.AllowSort = false;   //禁用列头排序
            gViewOrdersm.OptionsCustomization.AllowFilter = false; //禁用筛选
            gViewOrdersm.OptionsSelection.MultiSelect = true;      //允许选择多行


        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PatientOrdermFrm_Load(object sender, EventArgs e)
        {
            SetGridViewStyle();

            DataTable dt = dtPatientInfo();

            DataColumn dc = new DataColumn("check", typeof(bool));
            dt.Columns.Add(dc);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["check"] = "False";
            }
            gControlOrdersm.DataSource = dt;
        }


        private DataTable dtPatientInfo()
        {
            string selOrdersm = @"SELECT A.PATIENT_ID,A.ORDER_TEXT,A.DOSAGE||A.DOSAGE_UNITS DOSAGE FROM ORDERS_M A  WHERE A.WARD_CODE='" + GVars.User.DeptCode + "' AND A.PATIENT_ID='" + patinentID + "' AND A.VISIT_ID='" + visitID + "'      " +
                                 "   AND A.ORDER_CLASS='A'       " +
                                 "   ORDER BY A.ORDER_NO DESC,A.ORDER_SUB_NO";
            DataSet dsPatInfo = GVars.OracleAccess.SelectData(selOrdersm);
            if (dsPatInfo != null && dsPatInfo.Tables[0].Rows.Count > 0)
            {
                return dsPatInfo.Tables[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gViewOrdersm_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSure_Click(object sender, EventArgs e)
        {
            DataTable dtControlOrdersm = gControlOrdersm.DataSource as DataTable;
            string value = string.Empty;
            string orderText = string.Empty;
            for (int i = 0; i < dtControlOrdersm.Rows.Count; i++)
            {
                value = gViewOrdersm.GetDataRow(i)["check"].ToString();
                if (value == "True")
                {
                    orderText += gViewOrdersm.GetRowCellValue(i, "ORDER_TEXT").ToString() + " " + gViewOrdersm.GetRowCellValue(i, "DOSAGE").ToString() + "\r\n";//
                }
            }
            orderText = orderText.Length > 0 ? orderText.Substring(0, orderText.Length - 2) : orderText;
            resultValue = orderText;
            this.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 双击,默认确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gControlOrdersm_DoubleClick(object sender, EventArgs e)
        {
            string orderText = gViewOrdersm.GetFocusedRowCellValue("ORDER_TEXT").ToString() + " " + gViewOrdersm.GetFocusedRowCellValue("DOSAGE").ToString();//
            resultValue = orderText;
            this.Close();
        }
    }
}