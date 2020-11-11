using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SQL=HISPlus.SqlManager;

namespace HISPlus
{
    public partial class NurseScheduleFrm : FormDo
    {
        #region 变量
        private const string    RIGHT_EDIT      = "1";                  // 录入
        
        private int             TIMES_PER_DAY   = 4;                    // 一天几次
        private int             SECTION_LEN     = 31;                   // 班次显示长度
        
        private string          deptCode        = string.Empty;         // 科室代码
        private DataSet         dsWardList      = null;                 // 病区列表
        private DataSet         dsScheduleDict  = null;                 // 班次字典
        private DataRow[]       drScheduleDict  = null;                 // 班次字典记录
        
        private DataSet         dsScheduleSrc   = null;                 // 排班数据
        private DataSet         dsScheduleMemo  = null;                 // 排班备注
        private DataSet         dsScheduleDisp  = null;                 // 班次
        private DataSet         dsNurse         = null;                 // 护士
        private DataSet         dsNurseDuty     = null;                 // 护士职责
        
        private DateTime        dtWeekStart     = DataType.DateTime_Null();
        
        private List<Label>     arrLabelDate    = new List<Label>();    // 显示日期的标签
        private string          cellValue       = string.Empty;         // 单元格的值
        #endregion
        
        
        public NurseScheduleFrm()
        {
            InitializeComponent();
            
            _id     = "00054";
            _guid   = "528A9D62-F61E-46b8-9AA4-C43C9D3E9176";
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
            catch(Exception ex)
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
                           +   "AND JOB = '护士'";
                dsNurse         = GVars.OracleAccess.SelectData(sql);
                
                // 显示排班记录
                refreshShow();

                this.Cursor = Cursors.Default;
            }
            catch(Exception ex)
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
            // 判断周开始日期是否改变
            DateTime dtWeekStartTemp = dtPicker.Value.Date;
            
            while(dtWeekStartTemp.DayOfWeek != DayOfWeek.Monday)
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
                dgvSchedule.DataSource = dsScheduleDisp.Tables[0].DefaultView;
                
                // 内存中获取本周数据
                DateTime dtNext = dtWeekStart.AddDays(ComConst.VAL.DAYS_PER_WEEK);
                
                string filter = "DEPT_CODE = " + SQL.SqlConvert(deptCode)
                                + "AND SCHEDULE_DATE >= " + SQL.GetOraDbDate_Short(dtWeekStart)
                                + "AND SCHEDULE_DATE < " + SQL.GetOraDbDate_Short(dtNext);
                
                dsScheduleSrc = GVars.OracleAccess.SelectData("SELECT * FROM NURSE_SCHEDULE_REC WHERE " + filter);
                dsScheduleMemo = GVars.OracleAccess.SelectData("SELECT * FROM NURSE_SCHEDULE_MEMO WHERE " + filter);
                
                // 按钮状态
                btnSave.Enabled = true;
            }
            catch(Exception ex)
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
            catch(Exception ex)
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
        /// 菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mnuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSchedule.CurrentCell == null) return;
                if (dgvSchedule.Columns[dgvSchedule.CurrentCell.ColumnIndex].Tag == null) return;

                ToolStripItem mnuItem = (ToolStripItem)(sender);
                dgvSchedule.CurrentCell.Value = mnuItem.Text;
                btnSave.Enabled = true;
            }
            catch(Exception ex)
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


        /// <summary>
        /// 单元格开始编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void dgvSchedule_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvSchedule.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
            {
                cellValue = string.Empty;
            }
            else
            {
                cellValue = dgvSchedule.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
        }


        /// <summary>
        /// 单元格结束编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSchedule_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string tempValue = string.Empty;

            if (dgvSchedule.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                tempValue = dgvSchedule.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }

            if (cellValue.Equals(tempValue) == false && GVars.App.UserInput == true)
            {
                btnSave.Enabled = true;
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
            string sql = "SELECT * FROM DEPT_DICT WHERE CLINIC_ATTR = '2' AND OUTP_OR_INP = '1'";
            dsWardList      = GVars.OracleAccess.SelectData(sql, "DEPT_DICT");
            
            // 班次定义
            dsScheduleDict  = GVars.OracleAccess.SelectData("SELECT * FROM SHIFT_EXCHANGE_DICT");   // 班次定义
            drScheduleDict  = dsScheduleDict.Tables[0].Select(string.Empty, "SERIAL_NO");
            
            TIMES_PER_DAY   = dsScheduleDict.Tables[0].Rows.Count;
            
            // 护士
            sql = "SELECT ID PERSON_ID, NAME FROM STAFF_DICT "
                + "WHERE DEPT_CODE = " + SQL.SqlConvert(deptCode)
                +   "AND JOB = '护士'";
            dsNurse         = GVars.OracleAccess.SelectData(sql);
            
            // 护士职责
            sql = "SELECT * FROM HIS_DICT_ITEM WHERE DICT_ID = '28'";
            dsNurseDuty = GVars.OracleAccess.SelectData(sql);
            
            // 当前周开始时间
            dtWeekStart = dtPicker.Value.Date;
            
            while(dtWeekStart.DayOfWeek != DayOfWeek.Monday)
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
            Label lblPreDate    = lblDate0;
            Label lblPreWeekDay = lblWeekDay0;
            
            arrLabelDate.Add(lblDate0);
            
            lblPreDate.Width    = (SECTION_LEN - 1) * TIMES_PER_DAY + 1;
            lblPreWeekDay.Width = lblPreDate.Width;
            
            // grpSchedule
            Label lbl   = null;
            
            // 班次
            for(int j = 0; j < TIMES_PER_DAY; j++)
            {
                lbl         = createLabel();
                lbl.Text    = drScheduleDict[j]["SHIFT_NAME"].ToString();
                grpSchedule.Controls.Add(lbl);
                
                lbl.Width   = SECTION_LEN;
                lbl.Height  = lblPreWeekDay.Height;
                lbl.Left    = lblPreWeekDay.Left + SECTION_LEN * j - j;
                lbl.Top     = lblPreWeekDay.Top + lblPreWeekDay.Height - 1;
            }
            
            for(int i = 1; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {
                // 班次
                for(int j = 0; j < TIMES_PER_DAY; j++)
                {
                    lbl         = createLabel();
                    lbl.Text    = drScheduleDict[j]["SHIFT_NAME"].ToString();
                    grpSchedule.Controls.Add(lbl);
                    
                    lbl.Width   = SECTION_LEN;
                    lbl.Height  = lblPreWeekDay.Height;
                    lbl.Left    = lblPreWeekDay.Left + lblPreWeekDay.Width + SECTION_LEN * j - (j + 1);
                    lbl.Top     = lblPreWeekDay.Top + lblPreWeekDay.Height - 1;
                }
                
                // 日期
                lbl         = createLabel();
                arrLabelDate.Add(lbl);
                grpSchedule.Controls.Add(lbl);
                
                lbl.Width   = lblPreDate.Width;
                lbl.Height  = lblPreDate.Height;
                lbl.Left    = lblPreDate.Left + lblPreDate.Width - 1;
                lbl.Top     = lblPreDate.Top;
                
                lblPreDate = lbl;
                
                // 星期
                lbl         = createLabel();
                lbl.Text    = GetDayOfWeekCaptionNum(i);
                grpSchedule.Controls.Add(lbl);
                
                lbl.Width   = lblPreWeekDay.Width;
                lbl.Height  = lblPreWeekDay.Height;
                lbl.Left    = lblPreWeekDay.Left + lblPreWeekDay.Width - 1;
                lbl.Top     = lblPreWeekDay.Top;
                
                lblPreWeekDay   = lbl;
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
            // 科室列表
            cmbDept.DisplayMember   = "DEPT_NAME";
            cmbDept.ValueMember     = "DEPT_CODE";
            dsWardList.Tables[0].DefaultView.Sort = "DEPT_CODE";
            cmbDept.DataSource      = dsWardList.Tables[0].DefaultView;
            
            if (deptCode.Length > 0)
            {
                cmbDept.SelectedValue   = deptCode;
                cmbDept.Enabled         = false;
            }
            
            // 排班
            dgvSchedule.Width = lblMemo.Left + lblMemo.Width - lblTopLeft.Left;
            
            DataGridViewColumn dgvCol = null;
            
            // 护士名称
            dgvSchedule.Columns.Add("NAME", string.Empty);
            dgvCol = dgvSchedule.Columns[dgvSchedule.Columns.Count - 1];
            dgvCol.Width = lblTopLeft.Width - 2;
            dgvCol.DataPropertyName = "NAME";
            dgvCol.ReadOnly = true;
            
            dgvSchedule.Columns.Add("PERSON_ID", string.Empty);
            dgvCol = dgvSchedule.Columns[dgvSchedule.Columns.Count - 1];
            dgvCol.Visible = false;
            dgvCol.DataPropertyName = "PERSON_ID";
            dgvCol.ReadOnly = true;
            
            // 班次
            for(int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {
                for(int j = 0; j < TIMES_PER_DAY; j++)
                {
                    string colName = getScheduleColumnName(i, j);
                    
                    dgvSchedule.Columns.Add(colName, colName);
                    dgvCol = dgvSchedule.Columns[dgvSchedule.Columns.Count - 1];
                    dgvCol.Width = SECTION_LEN - 1;
                    dgvCol.DataPropertyName = colName;
                    dgvCol.Tag = drScheduleDict[j]["SHIFT_NAME"].ToString();
                    dgvCol.ReadOnly = true;
                }
                
                dgvCol.DefaultCellStyle.BackColor = Color.SkyBlue;
            }
            
            // 备注
            dgvSchedule.Columns.Add("MEMO", string.Empty);
            dgvCol = dgvSchedule.Columns[dgvSchedule.Columns.Count - 1];
            dgvCol.Width = lblMemo.Width - 2;
            dgvCol.DataPropertyName = "MEMO";
            
            // 排序
            dgvSchedule.Columns.Add("SERIAL_NO", string.Empty);
            dgvCol = dgvSchedule.Columns[dgvSchedule.Columns.Count - 1];
            dgvCol.Width = 20;
            dgvCol.DataPropertyName = "SERIAL_NO";
            
            // 权限
            if (_userRight.Contains(RIGHT_EDIT) == true)
            {
                dgvSchedule.ContextMenuStrip = cmnuNurseDuty;
            }
            else
            {
                dgvSchedule.ContextMenuStrip = null;
            }
        }
        
        
        /// <summary>
        /// 创建Label
        /// </summary>
        /// <returns></returns>
        private Label createLabel()
        {
            Label lbl = new Label();
            lbl.BorderStyle = BorderStyle.FixedSingle;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            
            return lbl;
        }
        
        
        /// <summary>
        /// 创建菜单
        /// </summary>
        private void createContextMenu()
        {
            DataRow[] drFind = dsNurseDuty.Tables[0].Select("", "SERIAL_NO");
            for(int i = 0; i < drFind.Length; i++)
            {
                ToolStripItem mnuItem = cmnuNurseDuty.Items.Add(drFind[i]["ITEM_NAME"].ToString());
                mnuItem.Click += new EventHandler(mnuItem_Click);
            }
        }
        
        
        /// <summary>
        /// 获以日期
        /// </summary>
        /// <param name="weekDay">星期序号 1: 星期1 7:星期天</param>
        /// <returns></returns>
        private void refreshHeaderDate()
        {
            DateTime dtNow = dtPicker.Value.Date;
            while(dtNow.DayOfWeek != DayOfWeek.Monday)
            {
                dtNow = dtNow.AddDays(-1);
            }
            
            for(int i = 0; i < arrLabelDate.Count; i++)
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
            return drScheduleDict[schedule]["SHIFT_NAME"].ToString() + dayOfWeek.ToString();
        }
        
        
        /// <summary>
        /// 获取班次列名
        /// </summary>
        /// <param name="dayOfWeek">星期几</param>
        /// <param name="scheduleName">班次名</param>
        /// <returns>班次列名</returns>
        private string getScheduleColumnName(int dayOfWeek, string scheduleName)
        {
            return scheduleName + dayOfWeek.ToString();
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
            for(int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
            {
                for(int j = 0; j < TIMES_PER_DAY; j++)
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
            
            foreach(DataRow dr in dsNurse.Tables[0].Rows)
            {
                DataRow drNew = dsScheduleDisp.Tables[0].NewRow();
                
                drNew["PERSON_ID"] = dr["PERSON_ID"].ToString();
                drNew["NAME"]      = dr["NAME"].ToString();
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
                            + "AND SCHEDULE_DATE >= " + SQL.GetOraDbDate_Short(dtWeekStartPara)
                            + "AND SCHEDULE_DATE < " + SQL.GetOraDbDate_Short(dtNext);

            dsScheduleSrc = GVars.OracleAccess.SelectData("SELECT * FROM NURSE_SCHEDULE_REC WHERE " + filter, "NURSE_SCHEDULE_REC");        // 排班表
            dsScheduleMemo = GVars.OracleAccess.SelectData("SELECT * FROM NURSE_SCHEDULE_MEMO WHERE " + filter, "NURSE_SCHEDULE_MEMO");     // 排班备注
            
            // 备注最大长度限制
            dsScheduleDisp.Tables[0].Columns["MEMO"].MaxLength = dsScheduleMemo.Tables[0].Columns["MEMO"].MaxLength;
            
            // 保存排班记录
            DataRow[] drFind    = null;
            DataRow   drEdit    = null;
            string  filterFmt   = "PERSON_ID = '{0}'";
            
            foreach(DataRow dr in dsScheduleSrc.Tables[0].Rows)
            {
                string personId = dr["PERSON_ID"].ToString();                                   // 人员ID
                DateTime dt     = ((DateTime)dr["SCHEDULE_DATE"]).Date;                         // 日期
                string schedule = dr["SCHEDULE"].ToString();                                    // 班次
                string duty     = dr["NURSE_DUTY"].ToString();                                  // 职责
                
                // 查找人员
                drFind = dsScheduleDisp.Tables[0].Select(string.Format(filterFmt, personId), string.Empty);
                
                if (drFind.Length == 0)
                {
                    drEdit = dsScheduleDisp.Tables[0].NewRow();
                    drEdit["PERSON_ID"] = personId;
                    drEdit["NAME"]      = dr["NURSE_NAME"].ToString();
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
            foreach(DataRow dr in dsScheduleMemo.Tables[0].Rows)
            {
                string personId = dr["PERSON_ID"].ToString();                                   // 人员ID
                DateTime dt     = ((DateTime)dr["SCHEDULE_DATE"]).Date;                         // 日期
                string memo     = dr["MEMO"].ToString();                                        // 备注
                
                // 查找人员
                drFind = dsScheduleDisp.Tables[0].Select(string.Format(filterFmt, personId), string.Empty);
                
                if (drFind.Length == 0)
                {
                    drEdit = dsScheduleDisp.Tables[0].NewRow();
                    drEdit["PERSON_ID"] = personId;
                    drEdit["NAME"]      = dr["NURSE_NAME"].ToString();
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
            string filterFmt    = "PERSON_ID = '{0}' AND SCHEDULE_DATE = '{1}' AND SCHEDULE = '{2}'";
            DataRow[] drFind    = null;
            
            DataTable dtSrc = dsScheduleSrc.Tables[0];
            
            foreach(DataRow dr in dsScheduleDisp.Tables[0].Rows)
            {
                string personId = dr["PERSON_ID"].ToString();                                   // 护士ID号
                
                // 班次信息保存                
                for(int i = 0; i < ComConst.VAL.DAYS_PER_WEEK; i++)
                {
                    DateTime dtCurr = dtWeekStart.AddDays(i);                                   // 班次日期
                    
                    for(int j = 0; j < TIMES_PER_DAY; j++)
                    {
                        string scheduleName    = drScheduleDict[j]["SHIFT_NAME"].ToString();    // 班次名称
                        string colName         = getScheduleColumnName(i, scheduleName);        // 显示DataSet的列名
                        string scheduleValue   = dr[colName].ToString();                        // 班次的值
                        
                        drFind = dtSrc.Select(string.Format(filterFmt, personId, dtCurr, scheduleName), string.Empty);
                        if (drFind.Length > 0)
                        {
                            if (scheduleValue.Length == 0)
                            {
                                drFind[0].Delete();
                            }
                            else
                            {
                                drFind[0]["SCHEDULE"]   = scheduleName;
                                drFind[0]["NURSE_DUTY"] = scheduleValue;
                            }
                        }
                        else if (scheduleValue.Length > 0)
                        {
                            DataRow drNew = dtSrc.NewRow();
                            
                            drNew["DEPT_CODE"]      = deptCode; 
                            drNew["PERSON_ID"]      = personId; 
                            drNew["NURSE_NAME"]     = dr["NAME"].ToString(); 
                            drNew["SCHEDULE_DATE"]  = dtCurr;
                            drNew["SCHEDULE"]       = scheduleName;
                            drNew["NURSE_DUTY"]     = scheduleValue;
                            
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
                            
                    drNew["DEPT_CODE"]      = deptCode; 
                    drNew["PERSON_ID"]      = personId; 
                    drNew["SCHEDULE_DATE"]  = dtWeekStart;
                    drNew["MEMO"]           = memo;
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
            
            dsScheduleDisp.Tables[0].DefaultView.Sort = "SERIAL_NO";
            dgvSchedule.DataSource = dsScheduleDisp.Tables[0].DefaultView;
                        
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
    }
}
