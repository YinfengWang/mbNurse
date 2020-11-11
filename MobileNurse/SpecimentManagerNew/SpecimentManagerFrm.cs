// ***********************************************************************
// Assembly         : SpecimentManagerNew
// Author           : LPD
// Created          : 11-13-2015
//
// Last Modified By : LPD
// Last Modified On : 11-13-2015
// ***********************************************************************
// <copyright file="SpecimentManagerFrm.cs" company="陕西天健源达信息科技有限公司">
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

/// <summary>
/// The SpecimentManagerNew namespace.
/// </summary>
namespace HISPlus
{
    /// <summary>
    /// Class SpecimentManagerFrm.
    /// </summary>
    public partial class SpecimentManagerFrm : FormDo, IBasePatient
    {
        private string patientId = string.Empty;                        // 病人标识号
        private string visitId = string.Empty;                          // 住院标识
        private string bed_No = string.Empty;  //床号
        private DataSet dsPatient = null;                     // 病人信息

        DataSet dsSpecimentManager = new DataSet();

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecimentManagerFrm"/> class.
        /// </summary>
        public SpecimentManagerFrm()
        {
            _id = "00063";
            _guid = "1E1EDFF1-8E58-45cd-85BD-70B20C3B33EE";
            InitializeComponent();
        }


        /// <summary>
        /// Handles the Load event of the SpecimentManagerFrm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SpecimentManagerFrm_Load(object sender, EventArgs e)
        {
            SetGridViewStyle();
            BindGridData();
        }

        /// <summary>
        /// 设置GridView样式
        /// </summary>
        /// <returns>System.String.</returns>
        public void SetGridViewStyle()
        {

            gViewSpecimentData.OptionsView.ShowGroupPanel = false;   //去掉默认字
            gViewSpecimentData.IndicatorWidth = 30;
            gViewSpecimentData.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;  //取消选中时的虚拟框
            gViewSpecimentData.OptionsCustomization.AllowColumnMoving = true;  //禁止列头移动
            gViewSpecimentData.OptionsBehavior.Editable = true;      //单元格不可编辑
            gViewSpecimentData.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect; //选择行，不选择单元格
            gViewSpecimentData.OptionsSelection.EnableAppearanceFocusedCell = false;
            gViewSpecimentData.Appearance.OddRow.BackColor = Color.White;  // 设置奇数行颜色 // 默认也是白色 可以省略 
            gViewSpecimentData.OptionsView.EnableAppearanceOddRow = true;   // 使能 // 和和上面绑定 同时使用有效 
            gViewSpecimentData.Appearance.EvenRow.BackColor = Color.WhiteSmoke; // 设置偶数行颜色 
            gViewSpecimentData.OptionsView.EnableAppearanceEvenRow = true;   // 使能 // 和和上面绑定 同时使用有效
            gViewSpecimentData.OptionsMenu.EnableColumnMenu = false;
            gViewSpecimentData.OptionsMenu.EnableFooterMenu = false;
            gViewSpecimentData.OptionsMenu.EnableGroupPanelMenu = false;
            gViewSpecimentData.OptionsBehavior.Editable = true;

        }

        /// <summary>
        /// 选择列表病人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IBasePatient.Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
            //GVars.Patient.ID = e.PatientId;
            //GVars.Patient.VisitId = e.VisitId;
            ////GVars.Patient.BedNo = e.

            //patientId = GVars.Patient.ID;
            //visitId = GVars.Patient.VisitId;
           

            
        }

        /// <summary>
        /// 行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gViewSpecimentData_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 绑定默认数据
        /// </summary>
        private void BindGridData()
        {
            gControlSpecimentData.DataSource = GetSpecimentManagerList();

        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <returns></returns>
        private DataTable GetSpecimentManagerList()
        {
            string specimentManagerSql = @"SELECT A.* FROM MOBILE.VITAL_SIGNS_REC A,MOBILE.PATIENT_INFO B
                                                WHERE A.PATIENT_ID=B.PATIENT_ID
                                                AND A.WARD_CODE='"+GVars.User.DeptCode+"'";
            dsSpecimentManager = GVars.OracleAccess.SelectData(specimentManagerSql, "dtTransferRecode");

            if (dsSpecimentManager != null)
                return dsSpecimentManager.Tables[0];
            else
                return null;
        }


        void IBasePatient.Patient_ListRefresh(object sender, PatientEventArgs e)
        {
            
        }
    }
}