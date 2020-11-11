// ***********************************************************************
// Assembly         : NursingTransferRecord
// Author           : LPD
// Created          : 09-28-2015
//
// Last Modified By : LPD
// Last Modified On : 09-28-2015
// ***********************************************************************
// <copyright file="NursingTransferRecordFrm.cs" company="陕西天健源达信息科技有限公司">
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
using System.Data.OracleClient;
using DevExpress.XtraPrinting;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

/// <summary>
/// The HISPlus namespace.
/// </summary>
namespace HISPlus
{
    /// <summary>
    /// Class NursingTransferRecordFrm.
    /// </summary>
    public partial class NursingTransferRecordFrm : FormDo, IBasePatient
    {

        private DbAccess oracleAccess = null;

        DataSet dsTransferRecode = new DataSet();
        /// <summary>
        /// Initializes a new instance of the <see cref="NursingTransferRecordFrm"/> class.
        /// </summary>
        public NursingTransferRecordFrm()
        {
            InitializeComponent();
            _id = "00075";
            _guid = "55972058-E5D0-4a30-BD94-99D42388101M";

        }

        /// <summary>
        /// 设置GridView样式
        /// </summary>
        /// <returns>System.String.</returns>
        public void SetGridViewStyle()
        {
            gViewNursingRecode.OptionsView.ShowGroupPanel = false;   //去掉默认字
            gViewNursingRecode.IndicatorWidth = 40;
            gViewNursingRecode.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;  //取消选中时的虚拟框
            gViewNursingRecode.OptionsCustomization.AllowColumnMoving = false;  //禁止列头移动
            gViewNursingRecode.OptionsBehavior.Editable = true;      //单元格不可编辑
            gViewNursingRecode.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect; //选择行，不选择单元格
            gViewNursingRecode.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gViewNursingRecode.Appearance.OddRow.BackColor = Color.White;  // 设置奇数行颜色 // 默认也是白色 可以省略 
            this.gViewNursingRecode.OptionsView.EnableAppearanceOddRow = true;   // 使能 // 和和上面绑定 同时使用有效 
            this.gViewNursingRecode.Appearance.EvenRow.BackColor = Color.WhiteSmoke; // 设置偶数行颜色 
            this.gViewNursingRecode.OptionsView.EnableAppearanceEvenRow = true;   // 使能 // 和和上面绑定 同时使用有效
            gViewNursingRecode.OptionsMenu.EnableColumnMenu = false;
            gViewNursingRecode.OptionsMenu.EnableFooterMenu = false;
            gViewNursingRecode.OptionsMenu.EnableGroupPanelMenu = false;

            gViewRecode.OptionsView.ShowGroupPanel = false;   //去掉默认字
            gViewRecode.IndicatorWidth = 40;
            gViewRecode.OptionsCustomization.AllowColumnMoving = false;  //禁止列头移动
            gViewRecode.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;  //取消选中时的虚拟框
            gViewRecode.OptionsBehavior.Editable = false;      //单元格不可编辑
            gViewRecode.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect; //选择行，不选择单元格
            gViewRecode.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gViewRecode.Appearance.OddRow.BackColor = Color.White;  // 设置奇数行颜色 // 默认也是白色 可以省略 
            this.gViewRecode.OptionsView.EnableAppearanceOddRow = true;   // 使能 // 和和上面绑定 同时使用有效 
            this.gViewRecode.Appearance.EvenRow.BackColor = Color.WhiteSmoke; // 设置偶数行颜色 
            this.gViewRecode.OptionsView.EnableAppearanceEvenRow = true;   // 使能 // 和和上面绑定 同时使用有效
            gViewRecode.OptionsView.RowAutoHeight = true;   //自动行高


            //gViewRecode.OptionsCustomization.AllowRowSizing = true;
            //gViewRecode.LayoutChanged();

            //设置日期控件，显示为长日期模式
            repositoryItemDateEdit1.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            repositoryItemDateEdit1.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NursingTransferRecordFrm_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:01");
            dateTimePicker2.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            SetGridViewStyle();
            BindGridData("");

        }

        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gViewNursingRecode_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <returns></returns>
        private DataTable GetTransferRecodeList(string strWhere)
        {
            //string stransferRecode = string.Empty;
            //if (!string.IsNullOrEmpty(strWhere))
            //{
            //    stransferRecode = "SELECT  * FROM NURSING_TRANSFER_RECORD WHERE WARD_CODE='" + GVars.User.DeptCode + "' AND IS_ABOLISH ='F' AND TO_CHAR(EXECUTE_TIME,'YYYY-MM-DD')='" + Convert.ToDateTime(strWhere).ToString("yyyy-MM-dd") + "' ORDER BY EXECUTE_TIME DESC";
            //}
            //else
            //{
            //    stransferRecode = "SELECT  * FROM NURSING_TRANSFER_RECORD WHERE WARD_CODE='" + GVars.User.DeptCode + "' AND IS_ABOLISH ='F' AND TO_CHAR(EXECUTE_TIME,'YYYY-MM-DD')='" + DateTime.Now.ToString("yyyy-MM-dd") + "' ORDER BY EXECUTE_TIME DESC";
            //}
            string transferRecode = "SELECT * FROM NURSING_TRANSFER_RECORD WHERE WARD_CODE='" + GVars.User.DeptCode + "' AND IS_ABOLISH ='F' AND EXECUTE_TIME>TO_DATE('" + dateTimePicker1.Value.ToString() + "','YYYY-MM-DD HH24:MI:SS'） AND EXECUTE_TIME<TO_DATE('" + dateTimePicker2.Value.ToString() + "','YYYY-MM-DD HH24:MI:SS')";

            dsTransferRecode = GVars.OracleAccess.SelectData(transferRecode, "dtTransferRecode");

            if (dsTransferRecode != null)
                return dsTransferRecode.Tables[0];
            else
                return null;
        }

        /// <summary>
        /// 绑定默认数据
        /// </summary>
        private void BindGridData(string strWhere)
        {
            gridControlRecode.DataSource = GetTransferRecodeList(strWhere);

        }

        /// <summary>
        /// 添加新行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddRecode_Click(object sender, EventArgs e)
        {
            //this.gViewRecode.AddNewRow();
            //btnSave.Enabled = true;
            AddNursingRecode addForm = new AddNursingRecode("add", this, null);
            addForm.Owner = this;
            addForm.ShowDialog();
            addForm.StartPosition = FormStartPosition.CenterScreen;
            addForm.MaximizeBox = false;
        }

        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gViewRecode_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gViewRecode_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //默认值,当前时间
            this.gViewRecode.SetRowCellValue(e.RowHandle, "EXECUTE_TIME", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            gViewRecode.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //DataTable dtTransferRecode = this.gridControlRecode.DataSource as DataTable;

        //DataTable dtChange = dtTransferRecode.GetChanges();
        //if (dtTransferRecode != null && dtTransferRecode.Rows.Count > 0)
        //{
        //    for (int i = 0; i < dtTransferRecode.Rows.Count; i++)
        //    {
        //        if (CheckEmptyValue(dtTransferRecode.Rows[i]))
        //        {
        //            if (string.IsNullOrEmpty(dtTransferRecode.Rows[i]["ward_code"].ToString()))
        //            {
        //                dtTransferRecode.Rows[i]["WARD_CODE"] = GVars.User.DeptCode;
        //            }
        //        }
        //        else
        //        {
        //            return;
        //        }

        //    }
        //}


        //int result = GVars.OracleAccess.Update(ref dsTransferRecode, "dtTransferRecode", "SELECT * FROM NURSING_TRANSFER_RECORD WHERE WARD_CODE='" + GVars.User.DeptCode + "' ");
        //dsTransferRecode.AcceptChanges();
        //if (result > 0)
        //{
        //    MessageBox.Show("保存成功", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    btnSave.Enabled = false;
        //}

        //}

        /// <summary>
        /// 检查交班记录书写是否合法,true:验证通过   false:验证失败
        /// </summary>
        private bool CheckEmptyValue(DataRow dr)
        {
            bool _flag = true;//
            if (dr != null)
            {
                if (string.IsNullOrEmpty(dr["TRANSFERER"].ToString()))
                {
                    MessageBox.Show("交班者不能为空", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _flag = false;
                }
                else if (string.IsNullOrEmpty(dr["RECEIVEER"].ToString()))
                {
                    MessageBox.Show("接班者不能为空", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _flag = false; ;
                }
            }
            return _flag;
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        public void Refresh_Method()
        {
            BindGridData("");
        }

        /// <summary>
        /// 数据源为空时，隐藏右键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            //数据源为空时，禁止编辑
            if (gridControlRecode.DataSource == null || ((DataTable)gridControlRecode.DataSource).Rows.Count == 0)
            {
                contextMenuStrip1.Items[0].Enabled = false;
                contextMenuStrip1.Items[2].Enabled = false;
            }
        }

        /// <summary>
        /// 作废操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolAbolish_Click(object sender, EventArgs e)
        {
            DialogResult isAbolish = MessageBox.Show("确定作废吗?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (isAbolish == System.Windows.Forms.DialogResult.Yes)
            {
                //更新IS_ABOLISH字段
                string rguid = gViewRecode.GetFocusedRowCellValue("RGUID").ToString();
                string updSql = @"UPDATE NURSING_TRANSFER_RECORD
                                   SET IS_ABOLISH = 'T'
                                   WHERE RGUID = '" + rguid + "'";
                GVars.OracleAccess.ExecuteNoQuery(updSql);

                //刷新数据
                Refresh_Method();
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintSettingController melc = new PrintSettingController(this.gridControlRecode);
            //string strStartDate = Convert.ToDateTime(dataStart.Text).ToString("yyyy-MM-dd");
            //string strEndDate = Convert.ToDateTime(dataEnd.Text).ToString("yyyy-MM-dd");
            //页眉
            melc.PrintHeader = GVars.User.DeptName + dateTimePicker1.Text + "交班报告"; ;

            //页脚
            melc.PrintFooter = "时间:" + DateTime.Now.ToString() + "     制表人:" + GVars.User.Name;

            //横纵向  false:默认的竖向打印   true:横向打印
            melc.LandScape = true;

            //纸型
            melc.PaperKind = System.Drawing.Printing.PaperKind.A4;
            //加载页面设置信息
            melc.LoadPageSetting();
            melc.Preview();
        }

        private void gViewRecode_CalcRowHeight(object sender, DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //    e.RowHeight = (int)gViewRecode.GetDataRow(e.RowHandle)["TRANSFER_CONTENT"];
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolEdit_Click(object sender, EventArgs e)
        {
            NursingTransferEntity nefEntity = new NursingTransferEntity();


            nefEntity.RGUID = gViewRecode.GetFocusedRowCellValue("RGUID").ToString();
            nefEntity.PATIENT_ID = gViewRecode.GetFocusedRowCellValue("PATIENT_ID").ToString();
            nefEntity.VISIT_ID = gViewRecode.GetFocusedRowCellValue("VISIT_ID").ToString();
            nefEntity.WARD_CODE = gViewRecode.GetFocusedRowCellValue("WARD_CODE").ToString();
            nefEntity.BED_NO = gViewRecode.GetFocusedRowCellValue("BED_NO").ToString();

            nefEntity.NAME = gViewRecode.GetFocusedRowCellValue("NAME").ToString();
            nefEntity.DIAGNOSIS_NAME = gViewRecode.GetFocusedRowCellValue("DIAGNOSIS_NAME").ToString();
            nefEntity.DYNAMIC_SITUATION = gViewRecode.GetFocusedRowCellValue("DYNAMIC_SITUATION").ToString();
            nefEntity.TRANSFER_CONTENT = gViewRecode.GetFocusedRowCellValue("TRANSFER_CONTENT").ToString();
            nefEntity.TRANSFERER = gViewRecode.GetFocusedRowCellValue("TRANSFERER").ToString();

            nefEntity.RECEIVEER = gViewRecode.GetFocusedRowCellValue("RECEIVEER").ToString();
            nefEntity.EXECUTE_TIME = gViewRecode.GetFocusedRowCellValue("EXECUTE_TIME").ToString();
            nefEntity.IS_ABOLISH = gViewRecode.GetFocusedRowCellValue("IS_ABOLISH").ToString();
            nefEntity.NURSE_CLASS = gViewRecode.GetFocusedRowCellValue("NURSE_CLASS").ToString();
            nefEntity.NURSE_EVENT = gViewRecode.GetFocusedRowCellValue("NURSE_EVENT").ToString();

            AddNursingRecode editForm = new AddNursingRecode("edit", this, nefEntity);
            editForm.ShowDialog();
        }

        private void gridControlRecode_DoubleClick(object sender, EventArgs e)
        {
            NursingTransferEntity nefEntity = new NursingTransferEntity();


            nefEntity.RGUID = gViewRecode.GetFocusedRowCellValue("RGUID").ToString();
            nefEntity.PATIENT_ID = gViewRecode.GetFocusedRowCellValue("PATIENT_ID").ToString();
            nefEntity.VISIT_ID = gViewRecode.GetFocusedRowCellValue("VISIT_ID").ToString();
            nefEntity.WARD_CODE = gViewRecode.GetFocusedRowCellValue("WARD_CODE").ToString();
            nefEntity.BED_NO = gViewRecode.GetFocusedRowCellValue("BED_NO").ToString();

            nefEntity.NAME = gViewRecode.GetFocusedRowCellValue("NAME").ToString();
            nefEntity.DIAGNOSIS_NAME = gViewRecode.GetFocusedRowCellValue("DIAGNOSIS_NAME").ToString();
            nefEntity.DYNAMIC_SITUATION = gViewRecode.GetFocusedRowCellValue("DYNAMIC_SITUATION").ToString();
            nefEntity.TRANSFER_CONTENT = gViewRecode.GetFocusedRowCellValue("TRANSFER_CONTENT").ToString();
            nefEntity.TRANSFERER = gViewRecode.GetFocusedRowCellValue("TRANSFERER").ToString();

            nefEntity.RECEIVEER = gViewRecode.GetFocusedRowCellValue("RECEIVEER").ToString();
            nefEntity.EXECUTE_TIME = gViewRecode.GetFocusedRowCellValue("EXECUTE_TIME").ToString();
            nefEntity.IS_ABOLISH = gViewRecode.GetFocusedRowCellValue("IS_ABOLISH").ToString();
            nefEntity.NURSE_CLASS = gViewRecode.GetFocusedRowCellValue("NURSE_CLASS").ToString();
            nefEntity.NURSE_EVENT = gViewRecode.GetFocusedRowCellValue("NURSE_EVENT").ToString();

            AddNursingRecode editForm = new AddNursingRecode("edit", this, nefEntity);
            editForm.ShowDialog();
        }

        void IBasePatient.Patient_SelectionChanged(object sender, PatientEventArgs e)
        {

        }


        void IBasePatient.Patient_ListRefresh(object sender, PatientEventArgs e)
        {

        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string strDateValue = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd HH:mm:ss");
            string endDateValue = dateTimePicker2.Value.Date.ToString("yyyy-MM-dd HH:mm:ss");
            BindGridData(strDateValue);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            gridControl1.DataSource = GetTransRecode();
        }

        private DataTable GetTransRecode()
        {
            DataTable dtNewTable = new DataTable("statistics");

            #region DataColumn
            DataColumn dcbc = dtNewTable.Columns.Add("班次", Type.GetType("System.String"));
            DataColumn dcyy = dtNewTable.Columns.Add("原有", Type.GetType("System.String"));
            DataColumn dcxr = dtNewTable.Columns.Add("新入", Type.GetType("System.String"));
            DataColumn dccy = dtNewTable.Columns.Add("出院", Type.GetType("System.String"));
            DataColumn dczr = dtNewTable.Columns.Add("转入", Type.GetType("System.String"));
            DataColumn dczc = dtNewTable.Columns.Add("转出", Type.GetType("System.String"));
            DataColumn dcsw = dtNewTable.Columns.Add("死亡", Type.GetType("System.String"));
            DataColumn dcxy = dtNewTable.Columns.Add("现有", Type.GetType("System.String"));
            DataColumn dcss = dtNewTable.Columns.Add("手术", Type.GetType("System.String"));
            DataColumn dcfm = dtNewTable.Columns.Add("分娩", Type.GetType("System.String"));
            DataColumn dcwz = dtNewTable.Columns.Add("危重", Type.GetType("System.String"));
            #endregion

            DataRow drZb = dtNewTable.NewRow();//主班
            DataRow drxyb = dtNewTable.NewRow(); //小夜班
            DataRow drdyb = dtNewTable.NewRow(); //大夜班
            dtNewTable.Rows.Add(drZb); dtNewTable.Rows.Add(drxyb); dtNewTable.Rows.Add(drdyb);


            #region  SQL
            string strZbStart = dateTimePicker1.Value.Date.AddDays(-1).ToString("yyyy-MM-dd") + " 08:00:00";//主班开始时间
            string strZbEnd = dateTimePicker1.Value.Date.AddDays(-1).ToString("yyyy-MM-dd") + " 14:00:00"; //主班结束时间

            string strXybStart = dateTimePicker1.Value.Date.AddDays(-1).ToString("yyyy-MM-dd") + " 14:00:00"; //小夜班开始时间
            string strXybEnd = dateTimePicker1.Value.Date.AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00";   //小夜班结束时间

            string strDybStart = dateTimePicker1.Value.Date.AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00";  //大夜班开始时间
            string strDybEnd = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + " 08:00:00";                //大夜班结束时间

            //主班
            string strSqlZb = @"select '主班'主班,p.patient_status_chg_name 状态,count(1)人次 from mobile.adt_log a  " +
                             " ,mobile.PATIENT_STATUS_CHG_DICT p   " +
                             " where a.ward_code='" + GVars.User.DeptCode + "'  " +
                             " and a.action = p.patient_status_chg_code  " +
                             " and a.log_date_time>=to_date('" + strZbStart + "','yyyy-MM-dd HH24:mi:ss')    " +
                             " and a.log_date_time<to_date('" + strZbEnd + "','yyyy-MM-dd HH24:mi:ss')  " +
                             " group by p.patient_status_chg_name";
            string strSqlXyb = @"select '小夜班'小夜班,p.patient_status_chg_name 状态,count(1)人次 from mobile.adt_log b   " +
                             "   ,mobile.PATIENT_STATUS_CHG_DICT p    " +
                             "   where b.ward_code='" + GVars.User.DeptCode + "'   " +
                             "   and b.action=p.patient_status_chg_code   " +
                             "   and b.log_date_time>=to_date('" + strXybStart + "','yyyy-MM-dd HH24:mi:ss')     " +
                             "   and b.log_date_time<=to_date('" + strXybEnd + "','yyyy-MM-dd HH24:mi:ss')   " +
                             "   group by p.patient_status_chg_name";
            string strSqlDyb = @"select '大夜班'大夜班,p.patient_status_chg_name 状态,count(1)人次 from mobile.adt_log c  " +
                             " ,mobile.PATIENT_STATUS_CHG_DICT p   " +
                             "  where c.ward_code='" + GVars.User.DeptCode + "'   " +
                             " and c.action=p.patient_status_chg_code   " +
                             " and c.log_date_time>to_date('" + strDybStart + "','yyyy-MM-dd HH24:mi:ss')     " +
                             " and c.log_date_time<to_date('" + strDybEnd + "','yyyy-MM-dd HH24:mi:ss')   " +
                             " group by p.patient_status_chg_name";
            DataSet dszb = GVars.OracleAccess.SelectData(strSqlZb);
            DataSet dsXyb = GVars.OracleAccess.SelectData(strSqlXyb);
            DataSet dsDyb = GVars.OracleAccess.SelectData(strSqlDyb);
            #endregion

            if (dszb != null)
            {
                DataTable dtZb = dszb.Tables[0];
                for (int m = 0; m < dtNewTable.Columns.Count; m++)
                {
                    if (m == 0)
                    {
                        dtNewTable.Rows[0][m] = "主班(8-14)";
                        continue;
                    }
                    if (dtZb.Rows.Count == 0)
                    {
                        dtNewTable.Rows[0][m] = "0";
                        continue;
                    }
                    for (int n = 0; n < dtZb.Rows.Count; n++)
                    {
                        if (dtNewTable.Columns[m].ColumnName == dtZb.Rows[n]["状态"].ToString())
                        {
                            dtNewTable.Rows[0][m] = dtZb.Rows[n]["人次"].ToString();
                            break;
                        }
                        else
                        {
                            dtNewTable.Rows[0][m] = "0";
                        }
                    }

                }
            }

            if (dsXyb != null)
            {
                DataTable dtXyb = dsXyb.Tables[0];
                for (int m = 0; m < dtNewTable.Columns.Count; m++)
                {
                    if (m == 0)
                    {
                        dtNewTable.Rows[1][m] = "小夜班(14-20)";
                        continue;
                    }
                    if (dtXyb.Rows.Count == 0)
                    {
                        dtNewTable.Rows[1][m] = "0";
                        continue;
                    }
                    for (int n = 0; n < dtXyb.Rows.Count; n++)
                    {
                        if (dtNewTable.Columns[m].ColumnName == dtXyb.Rows[n]["状态"].ToString())
                        {
                            dtNewTable.Rows[1][m] = dtXyb.Rows[n]["人次"].ToString();
                            break;
                        }
                        else
                        {
                            dtNewTable.Rows[1][m] = "0";
                        }
                    }

                }
            }

            if (dsDyb != null)
            {
                DataTable dtDyb = dsDyb.Tables[0];
                for (int m = 0; m < dtNewTable.Columns.Count; m++)
                {
                    if (m == 0)
                    {
                        dtNewTable.Rows[2][m] = "大夜班(20-8)";
                        continue;
                    }

                    if (dtDyb.Rows.Count == 0)
                    {
                        dtNewTable.Rows[2][m] = "0";
                        continue;
                    }

                    for (int n = 0; n < dtDyb.Rows.Count; n++)
                    {
                        if (dtNewTable.Columns[m].ColumnName == dtDyb.Rows[n]["状态"].ToString())
                        {
                            dtNewTable.Rows[2][m] = dtDyb.Rows[n]["人次"].ToString();
                            break;
                        }
                        else
                        {
                            dtNewTable.Rows[2][m] = "0";
                        }
                    }


                }
            }
            return dtNewTable;
        }

        /// <summary>
        /// 护士手动输入的统计数据保存到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dtRecodeStatic = gridControl1.DataSource as DataTable;

        }
    }

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