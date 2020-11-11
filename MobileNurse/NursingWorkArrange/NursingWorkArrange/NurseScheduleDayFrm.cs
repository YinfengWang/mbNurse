using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using HISPlus.UserControls;
using SQL = HISPlus.SqlManager;
using System.Collections;

namespace HISPlus
{
    public partial class NurseScheduleDayFrm : FormDo
    {
        #region 变量
        private const string STR_ALL_DAY = "全天";
        private const string RIGHT_EDIT = "1";                  // 录入

        private int TIMES_PER_DAY = 4;                    // 一天几次
        private int SECTION_LEN = 81;                   // 班次显示长度

        private string deptCode = string.Empty;         // 科室代码
        private DataSet dsWardList = null;                 // 病区列表        
        private DataSet dsScheduleSrc = null;                 // 排班数据
        private DataSet dsScheduleMemo = null;                 // 排班备注
        private DataSet dsScheduleDisp = null;                 // 班次
        private DataSet dsNurse = null;                 // 护士
        private DataSet dsNurseDuty = null;                 // 护士职责

        private DateTime dtWeekStart = DataType.DateTime_Null();

        private bool _isNewLine = false;   //是否新增的行
        private bool _isDel = false;       //是否删除行

        private List<LabelControl> arrLabelDate = new List<LabelControl>();    // 显示日期的标签
        private string cellValue = string.Empty;         // 单元格的值        
        #endregion


        public NurseScheduleDayFrm()
        {
            InitializeComponent();

            _id = "00074";
            _guid = "64C06336-670E-4f99-91AF-817696C7FD87";
        }


        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NurseScheduleFrm_Load(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            try
            {
                GVars.App.UserInput = false;

                initFrmVal();

                createContextMenu();

                initScheduleHeader();
                refreshHeaderDate();
                initScheduleDisp();

                initScheduleStruct();

                refreshShow();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }


        /// <summary>
        /// 病区选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false) return;

                this.Cursor = Cursors.WaitCursor;

                deptCode = cmbDept.SelectedValue.ToString();

                // 获取护士列表
                string sql = "SELECT ID PERSON_ID, NAME FROM STAFF_DICT "
                           + "WHERE DEPT_CODE = " + SQL.SqlConvert(deptCode)
                           + "AND JOB = '护士'";
                dsNurse = GVars.OracleAccess.SelectData(sql);

                // 显示排班记录
                refreshShow();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[上周]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreWeek_Click(object sender, EventArgs e)
        {
            dtPicker.Value = dtPicker.Value.AddDays(-1 * ComConst.VAL.DAYS_PER_WEEK);
        }


        /// <summary>
        /// 按钮[下周]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextWeek_Click(object sender, EventArgs e)
        {
            dtPicker.Value = dtPicker.Value.AddDays(ComConst.VAL.DAYS_PER_WEEK);
        }


        /// <summary>
        /// 日期选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtPicker_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                // 判断周开始日期是否改变
                DateTime dtWeekStartTemp = dtPicker.Value.Date;

                while (dtWeekStartTemp.DayOfWeek != DayOfWeek.Monday)
                {
                    dtWeekStartTemp = dtWeekStartTemp.AddDays(-1);
                }

                if (Math.Abs((dtWeekStart.Subtract(dtWeekStartTemp)).Days) < 2)
                {
                    return;
                }

                dtWeekStart = dtWeekStartTemp;

                // 如果改变, 刷新显示
                refreshShow();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[复制上周数据]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                // 界面上显示上周数据
                initScheduleRec();
                loadScheduleData(dtWeekStart.AddDays(-1 * ComConst.VAL.DAYS_PER_WEEK));
                ucGridView1.DataSource = dsScheduleDisp.Tables[0].DefaultView;
                // 内存中获取本周数据
                DateTime dtNext = dtWeekStart.AddDays(ComConst.VAL.DAYS_PER_WEEK);

                string filter = "DEPT_CODE = " + SQL.SqlConvert(deptCode)
                                + "AND SCHEDULE_DATE >= " + SQL.GetOraDbDate_Short(dtWeekStart)
                                + "AND SCHEDULE_DATE < " + SQL.GetOraDbDate_Short(dtNext);

                dsScheduleSrc = GVars.OracleAccess.SelectData("SELECT * FROM NURSE_SCHEDULE_REC WHERE " + filter, "NURSE_SCHEDULE_REC");
                dsScheduleMemo = GVars.OracleAccess.SelectData("SELECT * FROM NURSE_SCHEDULE_MEMO WHERE " + filter, "NURSE_SCHEDULE_MEMO");

                // 按钮状态
                btnSave.Enabled = _userRight.Contains(RIGHT_EDIT);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[保存]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DataSet dsNewSource = dsScheduleDisp;
                if (dsNewSource != null && dsNewSource.Tables.Count > 0 && dsNewSource.Tables[0].Rows.Count > 0)
                {
                    DataTable dtNewSource = dsNewSource.Tables[0];
                    var nlist = dtNewSource.AsEnumerable()
                        .Select(t => t.Field<string>("NAME"))
                        .ToList();

                    foreach (string nname in nlist)
                    {
                        if (string.IsNullOrEmpty(nname))
                        {
                            MessageBox.Show("存在空值，请修改后再保存！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                saveScheduleDisp();


                GVars.OracleAccess.BeginTrans();
                GVars.OracleAccess.Update(ref dsScheduleSrc);
                GVars.OracleAccess.Update(ref dsScheduleMemo);
                GVars.OracleAccess.Commit();

                btnSave.Enabled = false;

                // 重新获取数据
                DateTime dtNext = dtWeekStart.AddDays(ComConst.VAL.DAYS_PER_WEEK);

                string filter = "DEPT_CODE = " + SQL.SqlConvert(deptCode)
                        + "AND SCHEDULE_DATE >= " + SQL.GetOraDbDate_Short(dtWeekStart)
                        + "AND SCHEDULE_DATE < " + SQL.GetOraDbDate_Short(dtNext);

                dsScheduleSrc = GVars.OracleAccess.SelectData("SELECT * FROM NURSE_SCHEDULE_REC WHERE " + filter, "NURSE_SCHEDULE_REC");
                dsScheduleMemo = GVars.OracleAccess.SelectData("SELECT * FROM NURSE_SCHEDULE_MEMO WHERE " + filter, "NURSE_SCHEDULE_MEMO");
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


        /// <summary>
        /// 按钮[打印]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // 获取打印信息
                getDayList();
                getPrintInfo();

                if (excelConfig.ConfigSections.Count == 0)
                {
                    loadExcelPrintConfig(_template);
                }

                if (excelConfig.ConfigSections.Count > 0)
                {
                    excelTemplatePrint();
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[清空]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                //for (int rowHandle = 0; rowHandle < ucGridView1.RowCount; rowHandle++)
                //{
                //    foreach (GridColumn col in ucGridView1.VisibleColumns)
                //    {
                //        if (col.VisibleIndex > 0)
                //            ucGridView1.SetRowCellValue(rowHandle, col, string.Empty);
                //    }
                //}

                for (int rowHandle = 0; rowHandle < ucGridView1.RowCount; rowHandle++)
                {
                    for (int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
                    {
                        for (int j = 0; j < TIMES_PER_DAY; j++)
                        {
                            string colName = getScheduleColumnName(i, j);
                            ucGridView1.SetRowCellValue(rowHandle, colName, string.Empty);
                        }
                    }
                }

                btnSave.Enabled = _userRight.Contains(RIGHT_EDIT);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucGridView1.SelectRowsCount == 0) return;

                ToolStripItem mnuItem = (ToolStripItem)(sender);
                ucGridView1.SetRowCellValue(ucGridView1.FocusedRowHandle, ucGridView1.FocusedColumn, mnuItem.Text);
                btnSave.Enabled = _userRight.Contains(RIGHT_EDIT);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion


        #region 表格事件
        /// <summary>
        /// 排班单元格双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSchedule_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvSchedule.Columns[e.ColumnIndex].Tag != null)
            //{
            //    DataGridViewCell dgvCell = dgvSchedule.Rows[e.RowIndex].Cells[e.ColumnIndex];

            //    if (dgvCell.Value.ToString().Length == 0)
            //    {
            //        dgvCell.Value = dgvSchedule.Columns[e.ColumnIndex].Tag.ToString();
            //    }
            //    else
            //    {
            //        dgvCell.Value = string.Empty;
            //    }

            //    btnSave.Enabled = true;
            //}
        }


        /// <summary>
        /// 排班单元格数据错误处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSchedule_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion

        #region 共通函数
        /// <summary>
        /// 初始化窗体变量
        /// </summary>
        private void initFrmVal()
        {
            deptCode = GVars.User.DeptCode;

            // 病区
            //CJ20110815 针对瓦房店中心医院his表结构不同
            //string sql = "SELECT * FROM DEPT_DICT WHERE CLINIC_ATTR = '2' AND OUTP_OR_INP = '1'";

            // 2014年11月20日
            // hcc
            // CLINIC_ATTR = 2 表示 护理单元
            // 查询自 DEPT_CLINIC_ATTR_DICT表.

            string sql = "SELECT * FROM DEPT_DICT WHERE CLINIC_ATTR='2'";
            dsWardList = GVars.OracleAccess.SelectData(sql, "DEPT_DICT");

            TIMES_PER_DAY = 1;

            // 护士
            sql = "SELECT ID PERSON_ID, NAME FROM STAFF_DICT "
                + "WHERE DEPT_CODE = " + SQL.SqlConvert(deptCode)
                + "AND JOB = '护士'";
            dsNurse = GVars.OracleAccess.SelectData(sql);

            // 护士职责
            // 全天,夜班,白班
            sql = "SELECT * FROM HIS_DICT_ITEM WHERE DICT_ID = '28'";
            dsNurseDuty = GVars.OracleAccess.SelectData(sql);

            // 当前周开始时间
            dtWeekStart = dtPicker.Value.Date;

            while (dtWeekStart.DayOfWeek != DayOfWeek.Monday)
            {
                dtWeekStart = dtWeekStart.AddDays(-1);
            }

            // 用户权限
            _userRight = GVars.User.GetUserFrmRight(_id);
        }


        /// <summary>
        /// 初始化排班表头
        /// </summary>
        private void initScheduleHeader()
        {
            LabelControl lblPreDate = lblDate0;
            LabelControl lblPreWeekDay = lblWeekDay0;

            arrLabelDate.Add(lblDate0);

            lblPreDate.Width = (SECTION_LEN - 1) * TIMES_PER_DAY + 1;
            lblPreWeekDay.Width = lblPreDate.Width;

            // grpSchedule
            LabelControl lbl = null;

            for (int i = 1; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {
                // 日期
                lbl = createLabel();
                arrLabelDate.Add(lbl);
                grpSchedule.Controls.Add(lbl);

                lbl.Width = lblPreDate.Width;
                lbl.Height = lblPreDate.Height;
                lbl.Left = lblPreDate.Left + lblPreDate.Width - 1;
                lbl.Top = lblPreDate.Top;

                lblPreDate = lbl;

                // 星期
                lbl = createLabel();
                lbl.Text = GetDayOfWeekCaptionNum(i);
                grpSchedule.Controls.Add(lbl);

                lbl.Width = lblPreWeekDay.Width;
                lbl.Height = lblPreWeekDay.Height;
                lbl.Left = lblPreWeekDay.Left + lblPreWeekDay.Width - 1;
                lbl.Top = lblPreWeekDay.Top;

                lblPreWeekDay = lbl;
            }

            // 备注
            lblMemo.Left = lbl.Left + lbl.Width - 1;

            //btnSave.Left = lblMemo.Left + lblMemo.Width - btnSave.Width;
        }


        /// <summary>
        /// 初始化排班表格
        /// </summary>
        private void initScheduleDisp()
        {
            btnCopy.Enabled = _userRight.Contains(RIGHT_EDIT);
            btnClear.Enabled = _userRight.Contains(RIGHT_EDIT);

            // 科室列表
            cmbDept.DisplayMember = "DEPT_NAME";
            cmbDept.ValueMember = "DEPT_CODE";
            dsWardList.Tables[0].DefaultView.Sort = "DEPT_CODE";
            cmbDept.DataSource = dsWardList.Tables[0].DefaultView;

            if (deptCode.Length > 0)
            {
                cmbDept.SelectedValue = deptCode;
                cmbDept.Enabled = false;
            }

            // 排班
            ucGridView1.Width = lblMemo.Left + lblMemo.Width - lblTopLeft.Left;

            // 权限
            if (_userRight.Contains(RIGHT_EDIT) == true)
            {
                ucGridView1.ContextMenuStrip = cmnuNurseDuty;
            }
            else
            {
                ucGridView1.ContextMenuStrip = null;
            }

            ucGridView1.AllowEdit = _userRight.Contains(RIGHT_EDIT);
            ucGridView1.ShowHeaders = false;
            ucGridView1.Add("ID", "PERSON_ID", false);
            ucGridView1.Add("护士", "NAME", lblTopLeft.Width - 2);
            // 班次
            for (int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {
                for (int j = 0; j < TIMES_PER_DAY; j++)
                {
                    string colName = getScheduleColumnName(i, j);
                    ucGridView1.Add(colName, colName, SECTION_LEN, ColumnStatus.Center | ColumnStatus.AllowEdit);
                }
            }

            ucGridView1.Add("备注", "MEMO", lblMemo.Width - 2);

            ucGridView1.Add("SERIAL_NO", "SERIAL_NO", false);

            ucGridView1.Init();
        }


        /// <summary>
        /// 创建Label
        /// </summary>
        /// <returns></returns>
        private LabelControl createLabel()
        {
            LabelControl lbl = new LabelControl();
            //lbl.BorderStyle = BorderStyle.FixedSingle;
            //lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            lbl.BorderStyle = BorderStyles.Simple;
            lbl.AutoSizeMode = LabelAutoSizeMode.None;
            //lbl.BackColor = ucGridView1.BackColor;
            //lbl.Appearance.BorderColor = CommonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default)
            //                        .Colors.GetColor(GridSkins.SkinHeader);

            return lbl;
        }


        /// <summary>
        /// 创建菜单
        /// </summary>
        private void createContextMenu()
        {
            DataRow[] drFind = dsNurseDuty.Tables[0].Select("", "SERIAL_NO");
            for (int i = 0; i < drFind.Length; i++)
            {
                ToolStripItem mnuItem = cmnuNurseDuty.Items.Add(drFind[i]["ITEM_NAME"].ToString());
                mnuItem.Click += new EventHandler(mnuItem_Click);
            }
            //添加临时员工
            ToolStripItem lineStripItem = cmnuNurseDuty.Items.Add("-");
            ToolStripItem temporaryStripItem = cmnuNurseDuty.Items.Add("添加临时员工");
            temporaryStripItem.Name = "Add";
            temporaryStripItem.Click += new EventHandler(temporaryStripItem_Click);
            //删除临时员工
            ToolStripItem tempDel = cmnuNurseDuty.Items.Add("删除临时员工");
            tempDel.Name = "Del";
            tempDel.Click += new EventHandler(tempDel_Click);
        }

        /// <summary>
        /// 删除临时员工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tempDel_Click(object sender, EventArgs e)
        {
            _isNewLine = false;
            _isDel = true;
            // 条件检查
            if (ucGridView1.SelectRowsCount == 0)
                return;

            // 确认是否要删除
            if (GVars.Msg.Show("Q0005") != DialogResult.Yes)        // 您确认要删除当前记录吗?
            {
                return;
            }

            //记录删除前PERSON_ID，NAME
            string person_id = ucGridView1.SelectedRow["PERSON_ID"].ToString();
            string nurse_name = ucGridView1.SelectedRow["NAME"].ToString();

            //记录删除的护理记录
            ucGridView1.DeleteSelectRow();
            btnSave.Enabled = true;

            dsScheduleDisp.AcceptChanges();

            

            DelTemporary(person_id);
        }

        /// <summary>
        /// 添加临时员工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void temporaryStripItem_Click(object sender, EventArgs e)
        {
            _isNewLine = true;
            _isDel = false;

            string personId = "T" + new Random().Next(0, 10000).ToString("0000");
            DataRow drNew = dsScheduleDisp.Tables[0].NewRow();

            drNew["PERSON_ID"] = personId;
            drNew["SERIAL_NO"] = "0";

            dsScheduleDisp.Tables[0].Rows.Add(drNew);
            btnSave.Enabled = true;
        }


        /// <summary>
        /// 获以日期
        /// </summary>
        /// <param name="weekDay">星期序号 1: 星期1 7:星期天</param>
        /// <returns></returns>
        private void refreshHeaderDate()
        {
            DateTime dtNow = dtPicker.Value.Date;
            while (dtNow.DayOfWeek != DayOfWeek.Monday)
            {
                dtNow = dtNow.AddDays(-1);
            }

            for (int i = 0; i < arrLabelDate.Count; i++)
            {
                arrLabelDate[i].Text = dtNow.Month.ToString("00月") + dtNow.Day.ToString("00日");
                dtNow = dtNow.AddDays(1);
            }
        }


        /// <summary>
        /// 获取班次列名
        /// </summary>
        /// <param name="dayOfWeek">星期几</param>
        /// <param name="schedule">班次序号</param>
        /// <returns>班次列名</returns>
        private string getScheduleColumnName(int dayOfWeek, int schedule)
        {
            return STR_ALL_DAY + dayOfWeek.ToString();
            //return drScheduleDict[schedule]["SHIFT_NAME"].ToString() + dayOfWeek.ToString();
        }


        /// <summary>
        /// 获取班次列名
        /// </summary>
        /// <param name="dayOfWeek">星期几</param>
        /// <param name="scheduleName">班次名</param>
        /// <returns>班次列名</returns>
        private string getScheduleColumnName(int dayOfWeek, string scheduleName)
        {
            return STR_ALL_DAY + dayOfWeek.ToString();
            //return scheduleName + dayOfWeek.ToString();
        }


        /// <summary>
        /// 初始化班次结构
        /// </summary>
        private void initScheduleStruct()
        {
            dsScheduleDisp = new DataSet();
            dsScheduleDisp.Tables.Add("SHIFT_NAME");

            // 护士
            DataTable dt = dsScheduleDisp.Tables[0];
            dt.Columns.Add("PERSON_ID", typeof(string));
            dt.Columns.Add("NAME", typeof(string));

            // 班次
            for (int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {
                for (int j = 0; j < TIMES_PER_DAY; j++)
                {
                    dt.Columns.Add(getScheduleColumnName(i, j), typeof(string));
                }
            }

            // 备注
            dt.Columns.Add("MEMO", typeof(string));
            dt.Columns.Add("SERIAL_NO", typeof(decimal));

            // 设置主键
            DataColumn[] pk = new DataColumn[1];
            pk[0] = dt.Columns["PERSON_ID"];

            dt.PrimaryKey = pk;
        }


        /// <summary>
        /// 初始化排班记录
        /// </summary>
        private void initScheduleRec()
        {
            dsScheduleDisp.Tables[0].Clear();

            // 如果是一周前的排班, 不能增加新护士排班记录, 只能修改已有护士的排班记录.
            if (dtWeekStart.AddDays(ComConst.VAL.DAYS_PER_WEEK) < DateTime.Now)
            {
                return;
            }

            // 加载现有护士
            dsScheduleDisp.Tables[0].Clear();

            foreach (DataRow dr in dsNurse.Tables[0].Rows)
            {
                DataRow drNew = dsScheduleDisp.Tables[0].NewRow();

                drNew["PERSON_ID"] = dr["PERSON_ID"].ToString();
                drNew["NAME"] = dr["NAME"].ToString();
                drNew["SERIAL_NO"] = "0";

                dsScheduleDisp.Tables[0].Rows.Add(drNew);
            }
        }


        /// <summary>
        /// 加载排班数据
        /// </summary>
        private void loadScheduleData(DateTime dtWeekStartPara)
        {
            // 查找排班记录
            DateTime dtNext = dtWeekStartPara.AddDays(ComConst.VAL.DAYS_PER_WEEK);

            string filter = "DEPT_CODE = " + SQL.SqlConvert(deptCode)
                            + "AND SCHEDULE = " + SQL.SqlConvert(STR_ALL_DAY)
                            + "AND SCHEDULE_DATE >= " + SQL.GetOraDbDate_Short(dtWeekStartPara)
                            + "AND SCHEDULE_DATE < " + SQL.GetOraDbDate_Short(dtNext);

            dsScheduleSrc = GVars.OracleAccess.SelectData("SELECT * FROM NURSE_SCHEDULE_REC WHERE " + filter, "NURSE_SCHEDULE_REC");        // 排班表

            filter = "DEPT_CODE = " + SQL.SqlConvert(deptCode)
                            + "AND SCHEDULE_DATE >= " + SQL.GetOraDbDate_Short(dtWeekStartPara)
                            + "AND SCHEDULE_DATE < " + SQL.GetOraDbDate_Short(dtNext);
            dsScheduleMemo = GVars.OracleAccess.SelectData("SELECT * FROM NURSE_SCHEDULE_MEMO WHERE " + filter, "NURSE_SCHEDULE_MEMO");     // 排班备注

            // 备注最大长度限制
            dsScheduleDisp.Tables[0].Columns["MEMO"].MaxLength = dsScheduleMemo.Tables[0].Columns["MEMO"].MaxLength;

            // 保存排班记录
            DataRow[] drFind = null;
            DataRow drEdit = null;
            string filterFmt = "PERSON_ID = '{0}'";

            foreach (DataRow dr in dsScheduleSrc.Tables[0].Rows)
            {
                string personId = dr["PERSON_ID"].ToString();                                   // 人员ID
                DateTime dt = ((DateTime)dr["SCHEDULE_DATE"]).Date;                         // 日期
                string schedule = dr["SCHEDULE"].ToString();                                    // 班次
                string duty = dr["NURSE_DUTY"].ToString();                                  // 职责

                // 查找人员
                drFind = dsScheduleDisp.Tables[0].Select(string.Format(filterFmt, personId), string.Empty);

                if (drFind.Length == 0)
                {
                    drEdit = dsScheduleDisp.Tables[0].NewRow();
                    drEdit["PERSON_ID"] = personId;
                    drEdit["NAME"] = dr["NURSE_NAME"].ToString();
                }
                else
                {
                    drEdit = drFind[0];
                }

                // 获取字段名称
                string colName = getScheduleColumnName(dt.Subtract(dtWeekStartPara).Days, schedule);

                // 赋值
                if (dsScheduleDisp.Tables[0].Columns.Contains(colName) == true)
                {
                    drEdit[colName] = duty;//schedule;
                }
                else
                {
                    continue;
                }

                if (drFind.Length == 0)
                {
                    dsScheduleDisp.Tables[0].Rows.Add(drEdit);
                }
            }

            // 备注
            foreach (DataRow dr in dsScheduleMemo.Tables[0].Rows)
            {
                string personId = dr["PERSON_ID"].ToString();                                   // 人员ID
                DateTime dt = ((DateTime)dr["SCHEDULE_DATE"]).Date;                         // 日期
                string memo = dr["MEMO"].ToString();                                        // 备注

                // 查找人员
                drFind = dsScheduleDisp.Tables[0].Select(string.Format(filterFmt, personId), string.Empty);

                if (drFind.Length == 0)
                {
                    drEdit = dsScheduleDisp.Tables[0].NewRow();
                    drEdit["PERSON_ID"] = personId;
                }
                else
                {
                    drEdit = drFind[0];
                }

                // 赋值
                drEdit["MEMO"] = memo;
                drEdit["SERIAL_NO"] = dr["SERIAL_NO"];

                if (drFind.Length == 0)
                {
                    dsScheduleDisp.Tables[0].Rows.Add(drEdit);
                }
            }

            dsScheduleDisp.AcceptChanges();
        }


        /// <summary>
        /// 保存到缓存中
        /// </summary>
        /// <returns></returns>
        private bool saveScheduleDisp()
        {
            // 条件检查 不能处于复制上击数据的情况
            //DataSet dsChanged = dsScheduleDisp.GetChanges();
            //if (dsChanged == null)
            //{
            //    return true;
            //}

            // 正常处理
            string filterFmt = "PERSON_ID = '{0}' AND SCHEDULE_DATE = '{1}' AND SCHEDULE = '{2}'";
            DataRow[] drFind = null;

            DataTable dtSrc = dsScheduleSrc.Tables[0];

            foreach (DataRow dr in dsScheduleDisp.Tables[0].Rows)
            {
                string personId = dr["PERSON_ID"].ToString();                                   // 护士ID号

                // 班次信息保存                
                for (int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
                {
                    DateTime dtCurr = dtWeekStart.AddDays(i);                                   // 班次日期

                    for (int j = 0; j < TIMES_PER_DAY; j++)
                    {
                        string scheduleName = STR_ALL_DAY; //drScheduleDict[j]["SHIFT_NAME"].ToString();    // 班次名称
                        string colName = getScheduleColumnName(i, scheduleName);        // 显示DataSet的列名
                        string scheduleValue = dr[colName].ToString();                        // 班次的值

                        drFind = dtSrc.Select(string.Format(filterFmt, personId, dtCurr, scheduleName), string.Empty);
                        if (drFind.Length > 0)
                        {
                            //if (scheduleValue.Length == 0)
                            //{
                            //    drFind[0].Delete();
                            //}
                            //else
                            //{
                            drFind[0]["SCHEDULE"] = scheduleName;
                            drFind[0]["NURSE_DUTY"] = scheduleValue;
                            //}
                        }

                        else /*if (scheduleValue.Length > 0)*/
                        {
                            DataRow drNew = dtSrc.NewRow();

                            drNew["DEPT_CODE"] = deptCode;
                            drNew["PERSON_ID"] = personId;
                            drNew["NURSE_NAME"] = dr["NAME"].ToString();
                            drNew["SCHEDULE_DATE"] = dtCurr;
                            drNew["SCHEDULE"] = scheduleName;
                            drNew["NURSE_DUTY"] = scheduleValue;

                            dtSrc.Rows.Add(drNew);
                        }
                    }
                }

                // 排班备注保存
                string memo = dr["MEMO"].ToString().Trim();

                string filter = "PERSON_ID = " + SQL.SqlConvert(personId)
                            + " AND SCHEDULE_DATE = " + SQL.SqlConvert(dtWeekStart.ToString(ComConst.FMT_DATE.LONG));
                drFind = dsScheduleMemo.Tables[0].Select(filter);
                if (drFind.Length > 0)
                {
                    //if (memo.Length == 0)
                    //{
                    //    drFind[0].Delete();
                    //}
                    //else
                    //{
                    drFind[0]["MEMO"] = memo;
                    drFind[0]["SERIAL_NO"] = dr["SERIAL_NO"];
                    //}
                }
                else /*if (memo.Length > 0)*/
                {
                    DataRow drNew = dsScheduleMemo.Tables[0].NewRow();

                    drNew["DEPT_CODE"] = deptCode;
                    drNew["PERSON_ID"] = personId;
                    drNew["SCHEDULE_DATE"] = dtWeekStart;
                    drNew["MEMO"] = memo;
                    drNew["SERIAL_NO"] = dr["SERIAL_NO"];

                    dsScheduleMemo.Tables[0].Rows.Add(drNew);
                }
            }

            return true;
        }


        /// <summary>
        /// 刷新显示
        /// </summary>
        private void refreshShow()
        {
            initScheduleRec();
            loadScheduleData(dtWeekStart);

            refreshHeaderDate();

            dsScheduleDisp.Tables[0].DefaultView.Sort = "SERIAL_NO";
            ucGridView1.DataSource = dsScheduleDisp.Tables[0].DefaultView;

            btnPrint.Enabled = (ucGridView1.RowCount > 0);
            btnSave.Enabled = false;
        }


        /// <summary>
        /// 获取星期的大写数字表示
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string GetDayOfWeekCaptionNum(int num)
        {
            if (num < 0 || 6 < num) { return string.Empty; }

            string[] arrNum = { "一", "二", "三", "四", "五", "六", "日" };
            string romaNum = string.Empty;

            string numStr = num.ToString();

            for (int i = 0; i < numStr.Length; i++)
            {
                romaNum += arrNum[int.Parse(numStr.Substring(i, 1))];
            }

            return romaNum;
        }
        #endregion

        /// <summary>
        /// 右键时 根据人员PERSON_ID判断是否可以删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmnuNurseDuty_Opening(object sender, CancelEventArgs e)
        {
            if (ucGridView1.SelectRowsCount == 0)
            {
                cmnuNurseDuty.Items["Add"].Visible = false;
                cmnuNurseDuty.Items["Del"].Visible = false;
            }

            string personId = ucGridView1.SelectedRow["PERSON_ID"].ToString();

            //PERSON_ID不已T开头的人员，不能删除
            if (!personId.StartsWith("T"))
            {
                cmnuNurseDuty.Items["Del"].Enabled = false;
            }
            else
            {
                cmnuNurseDuty.Items["Del"].Enabled = true;
            }
        }

        /// <summary>
        /// 删除临时员工
        /// </summary>
        private void DelTemporary(string personId)
        {
            ArrayList alist = new ArrayList();
            //DEPT_CODE
            string sqlRec = " DELETE NURSE_SCHEDULE_REC WHERE DEPT_CODE = '" + GVars.User.DeptCode + "' AND PERSON_ID =  '" + personId + "'";
            string sqlMemo = " DELETE NURSE_SCHEDULE_MEMO WHERE DEPT_CODE = '" + GVars.User.DeptCode + "'   AND PERSON_ID = '" + personId + "' ";
            alist.Add(sqlRec); alist.Add(sqlMemo);
            GVars.OracleAccess.ExecuteNoQuery(ref alist);
        }


    }
}
