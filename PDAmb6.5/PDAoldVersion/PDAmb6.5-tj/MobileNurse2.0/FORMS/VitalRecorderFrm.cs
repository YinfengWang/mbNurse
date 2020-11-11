using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    /// <summary>
    /// 窗体加载时: 获取 [项目列表] dsNurseItemDict
    ///             创建显示缓存表  dtStoreShow
    /// 病人切换时: 获取 [护理记录] dsVitalSignsRec
    /// 选择类型时: 获取显示缓存表  loadInMidTable_Rec()
    ///                             loadInMidTable_Value()
    ///             获取时间点列表  getTimePointList()
    ///             显示值          showVitalSignsRec()
    /// 滚动栏滚动: 显示值          showVitalSignsRec()
    /// 更改时间点: 
    /// </summary>
    public partial class VitalRecorderFrm : Form
    {
        #region 变量
        private const int MAX_SHOW_ROWS = 9;
        private const int TYPE_NURSE_EVENT = 4;

        private PatientNavigator patNavigator = new PatientNavigator();   // 病人导航

        private VitalSignsDbI vitalSignsDbI = new VitalSignsDbI();
        private DataSet dsNurseItemDict = null;                     // 护理项目字典
        private DataSet dsVitalSignsRec = null;                     // 病人的生命体征
        private DataRow[] drEvents = null;

        private ArrayList arrayTime = new ArrayList();
        private DataTable dtStoreShow = null;

        private int nurseType = 0;
        private int nurseEventRecPos = -1;
        private DateTime dtTimePoint = DataType.DateTime_Null();

        private NumInputPanelFrm numInput = null;                     // 数字输入界面
        #endregion


        public VitalRecorderFrm()
        {
            InitializeComponent();
        }


        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VitalRecorderFrm_Load(object sender, EventArgs e)
        {
            try
            {
                patNavigator.BtnPrePatient = this.btnPrePatient;
                patNavigator.BtnCurrentPatient = this.btnCurrPatient;
                patNavigator.BtnNextPatient = this.btnNextPatient;
                patNavigator.BtnPatientList = this.btnListPatient;

                patNavigator.MenuItemPatient = mnuPatient;

                patNavigator.PatientChanged = new DataChanged(reloadVitalSignsRec);

                this.timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 时钟事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;
                timer1.Enabled = false;

                initFrmVal();

                initDisp();

                // 菜单控制
                foreach (MenuItem mnu in mnuNavigator.MenuItems)
                {
                    if (mnu.Text.IndexOf(this.Text) >= 0)
                    {
                        mnu.Enabled = false;
                    }
                    else
                    {
                        mnu.Click += new EventHandler(mnuNavigator_Func_Click);
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
                Cursor.Current = Cursors.Default;
            }
        }


        /// <summary>
        /// 菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuNavigator_Func_Click(object sender, EventArgs e)
        {
            try
            {
                MenuItem mnu = sender as MenuItem;
                if (mnu == null) return;

                this.Tag = mnu.Text;

                this.Close();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }



        /// <summary>
        /// 类型更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwNurseEventType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                GVars.App.UserInput = false;

                // 获取类型
                //if (lvwNurseEventType.SelectedIndices.Count == 0)
                if (comboxNurseEventType.SelectedItem.ToString().Length == 0)
                {
                    return;
                }
                string item = comboxNurseEventType.SelectedItem.ToString();
                //ListViewItem item = lvwNurseEventType.Items[lvwNurseEventType.SelectedIndices[0]];
                if (item.Length == 0)
                {
                    return;
                }
                else
                {
                    nurseType = getNurseTypeAttributeValue(item);
                }

                // 如果没有指定病人, 退出
                if (GVars.Patient.ID.Length == 0)
                {
                    return;
                }

                // 按钮 [增加护理事件] 的状态
                btnAddNurseEvent.Enabled = (nurseType == TYPE_NURSE_EVENT);

                if (nurseType != TYPE_NURSE_EVENT)
                {
                    // 加载中间表记录
                    loadInMidTable_Rec(nurseType);

                    // 获取时间点列表
                    getTimePointList();

                    // 显示数据
                    dtTimePoint = DataType.DateTime_Null();
                    if (arrayTime.Count > 0)
                    {
                        dtTimePoint = (DateTime)(arrayTime[arrayTime.Count - 1]);
                        setTimePointButtons();
                    }

                    // 加载中间表的值
                    loadInMidTable_Value(nurseType, dtTimePoint);

                    // 显示
                    showVitalSignsRec();
                }
                else
                {
                    showNurseEventRec();

                    setTimePointButtons();
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
                Cursor.Current = Cursors.Default;
            }
        }


        private void txtItemName0_GotFocus(object sender, EventArgs e)
        {
            try
            {
                if (nurseType == TYPE_NURSE_EVENT)
                {
                    TextBox txtFocu = getFocusedTextBox();

                    btnAddNurseEvent.Enabled = true;

                    if (txtFocu != null && txtFocu.Text.Length > 0)
                    {
                        btnAddNurseEvent.Text = "删除";

                        nurseEventRecPos = int.Parse(txtFocu.Tag.ToString());

                        btnAddNurseEvent.Enabled =
                            drEvents[nurseEventRecPos]["NURSE"].ToString().Equals(GVars.User.Name);
                    }
                    else
                    {
                        nurseEventRecPos = -1;
                        btnAddNurseEvent.Text = "增加";
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void txtItemValue1_GotFocus(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                GVars.App.UserInput = false;

                // inputPanel1.Enabled = true;
                TextBox txtInput = sender as TextBox;
                if (txtInput == null) return;

                mnuMemo.Enabled = (txtInput.Text.Trim().Length > 0);

                // 设置数字输入界面
                numInput.TextInput = txtInput.Text;

                // 获取提示信息
                numInput.TextTips = GVars.Patient.Name;
                int txtIndex = getFocusedTextBoxIndex();
                if (txtIndex >= 0)
                {
                    numInput.TextTips = GVars.Patient.Name + "    " + getCell(txtIndex, 0).Text;
                }

                // 弹出数字输入界面
                if (numInput.ShowDialog() == DialogResult.OK)
                {
                    txtInput.Text = numInput.TextInput;
                    btnSave.Enabled = true;
                }
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


        private void txtItemValue1_LostFocus(object sender, EventArgs e)
        {
            inputPanel1.Enabled = false;
            mnuMemo.Enabled = false;

            if (btnSave.Enabled == true)
            {
                saveInMidDispData();
            }
        }


        private void txtItemValue1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                this.btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void vsBar_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                if (nurseType == TYPE_NURSE_EVENT)
                {
                    showNurseEventRec();
                }
                else
                {
                    showVitalSignsRec();
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮 [增加]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNurseEvent_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAddNurseEvent.Text.Equals("增加") == true)
                {
                    AddNurseEventFrm showFrm = new AddNurseEventFrm();

                    showFrm.DsNurseEvent = dsNurseItemDict;
                    showFrm.DsNurseRec = dsVitalSignsRec;

                    if (showFrm.ShowDialog() == DialogResult.OK)
                    {
                        showNurseEventRec();
                    }
                }
                else
                {
                    DataRow[] drFind = dsVitalSignsRec.Tables[0].Select("ATTRIBUTE = '4'", "TIME_POINT");

                    if (0 <= nurseEventRecPos && nurseEventRecPos < drFind.Length)
                    {
                        GVars.Msg.MsgId = "Q00004";                     // 您确认{0}吗?");
                        GVars.Msg.MsgContent.Add("删除当前护理事件");

                        if (GVars.Msg.Show() != DialogResult.Yes)
                        {
                            return;
                        }

                        drFind[nurseEventRecPos].Delete();

                        showNurseEventRec();

                        btnSave.Enabled = true;
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }


        /// <summary>
        /// 按钮 [保存]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 对于更新 重新设置更新时间
                DateTime dtNow = GVars.GetDateNow();
                resetModifyDateTime(dtNow);

                // 去掉属性[ATTRIBUTE]
                DataSet dsChanged = dsVitalSignsRec.Copy();
                dsChanged.Tables[0].Columns.Remove("ATTRIBUTE");

                DataSet dsDel = ComFunctionApp.GetDataSetDeleted(ref dsChanged, dtNow);

                // 保存
                vitalSignsDbI.SaveVitalSignsRecord(ref dsChanged, GVars.Patient.ID, GVars.Patient.VisitId, dtTimePoint);

                // 保存删除的记录
                vitalSignsDbI.SaveVitalSignsRecordDel(ref dsDel);

                // 接受更新
                dsVitalSignsRec.AcceptChanges();

                btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 菜单[备注]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuMemo_Click(object sender, EventArgs e)
        {
            try
            {
                int txtIndex = getFocusedTextBoxIndex();

                if (txtIndex >= 0)
                {
                    TextBox txtEdit = getCell(txtIndex, 1);

                    VitalRecordMemo frmShow = new VitalRecordMemo();
                    frmShow.Editable = txtEdit.Enabled;
                    frmShow.Memo = txtEdit.Tag.ToString();

                    if (frmShow.ShowDialog() == DialogResult.OK)
                    {
                        txtEdit.Tag = frmShow.Memo;

                        btnSave.Enabled = (txtEdit.Tag.ToString().Equals(frmShow.Memo));
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 菜单 [返回]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCancel_Click(object sender, EventArgs e)
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
        #endregion


        #region 共通函数
        /// <summary>
        /// 初始化医嘱数据
        /// </summary>
        private void initFrmVal()
        {
            // 创建中间表
            createMidTable();

            dsNurseItemDict = vitalSignsDbI.GetNurseItemDict();

            if (GVars.Patient.ID != null && GVars.Patient.ID.Length > 0)
            {
                dsVitalSignsRec = vitalSignsDbI.GetVitalSignsRecord(GVars.Patient.ID, GVars.Patient.VisitId, GVars.GetDateNow());
                dsVitalSignsRec.AcceptChanges();
            }

            // 数字输入界面
            numInput = new NumInputPanelFrm();
        }


        /// <summary>
        /// 初始化医嘱
        /// </summary>
        private void initDisp()
        {
            mnuMemo.Enabled = false;

            // 定位病人
            patNavigator.SetPatientButtons();

            // 显示病人生命体征
            btnCurrTimePoint.Text = string.Empty;

            //if (lvwNurseEventType.SelectedIndices.Count == 0)
            //{
            //    lvwNurseEventType.Items[0].Selected = true;
            //}
            if (comboxNurseEventType.SelectedIndex == -1)
            {
                comboxNurseEventType.SelectedIndex = 0;
            }
            else
            {
                if (nurseType == TYPE_NURSE_EVENT)
                {
                    showNurseEventRec();
                }
                else
                {
                    // 获取时间点
                    getTimePointList();

                    // 显示数据
                    DateTime dtTimePoint = DataType.DateTime_Null();
                    if (arrayTime.Count > 0)
                    {
                        dtTimePoint = (DateTime)(arrayTime[arrayTime.Count - 1]);
                        setTimePointButtons();
                    }

                    // 加载中间表的值
                    loadInMidTable_Value(nurseType, dtTimePoint);

                    // 显示
                    showVitalSignsRec();
                }

            }

            this.btnSave.Enabled = false;
        }


        /// <summary>
        /// 根据过滤条件显示医嘱
        /// 当 符合7N+1条件时,默认一些值为'/' ,N代表周期,从1开始
        /// 2015-12-15 新需求
        /// </summary>
        private void showVitalSignsRec()
        {
            int dataIndex = vsBar.Value;

            #region 记录患者如可的天数 2015-12-15 Add

            //记录患者入科的天数
            int getAdmWardDateTimeCount = 0;
            DateTime today = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));  //当前日期
            DateTime admWardDateTime = Convert.ToDateTime(Convert.ToDateTime(GVars.DsPatient.Tables[0].Select(" PATIENT_ID='" + GVars.Patient.ID + "' AND VISIT_ID='" + GVars.Patient.VisitId + "'")[0]["ADM_WARD_DATE_TIME"]).ToString("yyyy-MM-dd"));  //入科日期
            TimeSpan tSpan = today.Subtract(admWardDateTime);   //时间差
            getAdmWardDateTimeCount = tSpan.Days;               //入科天数
            #endregion


            if (dtStoreShow.Rows.Count > MAX_SHOW_ROWS - 1)
            {
                vsBar.Maximum = dtStoreShow.Rows.Count - MAX_SHOW_ROWS + 1;
            }
            else
            {
                vsBar.Maximum = 0;
            }

            TextBox txtValue;

            int i = 0;
            for (; i < MAX_SHOW_ROWS; i++)
            {
                if (dataIndex < dtStoreShow.Rows.Count)
                {
                    DataRow dr = dtStoreShow.Rows[dataIndex];

                    // 项目名称
                    string itemUnit = dr["UNIT"].ToString().Trim();
                    if (itemUnit.Length > 0)
                    {
                        getCell(i, 0).Text = dr["VITAL_SIGNS"].ToString() + " (" + itemUnit + ")";
                    }
                    else
                    {
                        getCell(i, 0).Text = dr["VITAL_SIGNS"].ToString();
                    }

                    // 项目值
                    txtValue = getCell(i, 1);

                    
                    // 2015-12-16 在此作出验证,如果是7N+1天，给默认值'/' Add
                    //getCell(i, 0).Text = ""
                    string defaultValue = "血压 (mmHg),大便 (次/日),小便 (次/日),身高 (cm),体重 (Kg),尿量 (ml),过敏试验1,过敏试验2";
                    if (getAdmWardDateTimeCount % 7 == 1 && getAdmWardDateTimeCount > 7 && defaultValue.Split(',').Contains(getCell(i, 0).Text))
                    {
                        txtValue.Text = "/";
                    }
                    else
                    {
                        txtValue.Text = dr["VITAL_SIGNS_CVALUES"].ToString();
                    }

                    //txtValue.Text = dr["VITAL_SIGNS_CVALUES"].ToString();
                    txtValue.Tag = dr["MEMO"].ToString();

                    string nurse = dr["NURSE"].ToString();
                    txtValue.Enabled = (nurse.Length == 0 || nurse.Equals(GVars.User.Name));
                }
                else
                {
                    break;
                }

                dataIndex++;
            }

            for (; i < MAX_SHOW_ROWS; i++)
            {
                getCell(i, 0).Text = string.Empty;
                getCell(i, 1).Text = string.Empty;

                getCell(i, 1).Enabled = false;
            }
        }


        /// <summary>
        /// 根据过滤条件显示医嘱
        /// </summary>
        private void showNurseEventRec()
        {
            int dataIndex = vsBar.Value;

            drEvents = dsVitalSignsRec.Tables[0].Select("ATTRIBUTE = '4'", "TIME_POINT");

            if (drEvents.Length > MAX_SHOW_ROWS - 1)
            {
                vsBar.Maximum = drEvents.Length - MAX_SHOW_ROWS + 1;
            }
            else
            {
                vsBar.Maximum = 0;
            }

            int i = 0;
            for (; i < MAX_SHOW_ROWS; i++)
            {
                if (dataIndex < drEvents.Length)
                {
                    DataRow dr = drEvents[dataIndex];

                    // 项目名称
                    getCell(i, 0).Text = dr["VITAL_SIGNS"].ToString();
                    getCell(i, 0).Tag = dataIndex.ToString();

                    // 项目值
                    DateTime dtHappen = (DateTime)(dr["TIME_POINT"]);
                    TextBox textEdit = getCell(i, 1);

                    textEdit.Text = dtHappen.ToString("yy-MM-dd HH:mm");
                    textEdit.Enabled = false;
                }
                else
                {
                    break;
                }

                dataIndex++;
            }

            for (; i < MAX_SHOW_ROWS; i++)
            {
                getCell(i, 0).Text = string.Empty;
                getCell(i, 1).Text = string.Empty;

                getCell(i, 1).Enabled = false;
            }
        }


        /// <summary>
        /// 获取编辑单元格
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private TextBox getCell(int row, int col)
        {
            if (row < 0 && row >= MAX_SHOW_ROWS)
            {
                return null;
            }

            if (col < 0 && col >= 2)
            {
                return null;
            }

            switch (row)
            {
                case 0:
                    if (col == 0) { return txtItemName0; }
                    if (col == 1) { return txtItemValue0; }
                    break;
                case 1:
                    if (col == 0) { return txtItemName1; }
                    if (col == 1) { return txtItemValue1; }
                    break;
                case 2:
                    if (col == 0) { return txtItemName2; }
                    if (col == 1) { return txtItemValue2; }
                    break;
                case 3:
                    if (col == 0) { return txtItemName3; }
                    if (col == 1) { return txtItemValue3; }
                    break;
                case 4:
                    if (col == 0) { return txtItemName4; }
                    if (col == 1) { return txtItemValue4; }
                    break;
                case 5:
                    if (col == 0) { return txtItemName5; }
                    if (col == 1) { return txtItemValue5; }
                    break;
                case 6:
                    if (col == 0) { return txtItemName6; }
                    if (col == 1) { return txtItemValue6; }
                    break;
                case 7:
                    if (col == 0) { return txtItemName7; }
                    if (col == 1) { return txtItemValue7; }
                    break;
                case 8:
                    if (col == 0) { return txtItemName8; }
                    if (col == 1) { return txtItemValue8; }
                    break;
                default:
                    return null;
            }

            return null;
        }


        private TextBox getFocusedTextBox()
        {
            for (int row = 0; row < MAX_SHOW_ROWS; row++)
            {
                if (getCell(row, 0).Focused) return getCell(row, 0);
                if (getCell(row, 1).Focused) return getCell(row, 1);
            }

            return null;
        }


        private int getFocusedTextBoxIndex()
        {
            for (int row = 0; row < MAX_SHOW_ROWS; row++)
            {
                if (getCell(row, 0).Focused) return row;
                if (getCell(row, 1).Focused) return row;
            }

            return -1;
        }


        /// <summary>
        /// 获取护理类型名称对应的代码
        /// </summary>
        /// <returns></returns>
        private int getNurseTypeAttributeValue(string type)
        {
            if (type.IndexOf("生命体征") >= 0)
            {
                return 0;
            }
            else if (type.IndexOf("入量") >= 0)
            {
                return 1;
            }
            else if (type.IndexOf("出量") >= 0)
            {
                return 2;
            }
            else if (type.IndexOf("特殊项目") >= 0)
            {
                return 3;
            }
            else if (type.IndexOf("护理事件") >= 0)
            {
                return 4;
            }
            else
            {
                return -1;
            }
        }


        /// <summary>
        /// 获取时间列表
        /// </summary>
        private void getTimePointList()
        {
            arrayTime.Clear();

            // 获取一天的数据
            DateTime dtNow = GVars.GetDateNow();
            string filter = "ATTRIBUTE = '" + nurseType.ToString() + "' AND "
                    + "(TIME_POINT >= " + SqlManager.SqlConvert(dtNow.Date.ToString(ComConst.FMT_DATE.LONG))
                    + " AND TIME_POINT < " + SqlManager.SqlConvert(dtNow.Date.AddDays(1).ToString(ComConst.FMT_DATE.LONG))
                    + ") ";

            // 获取时间列表
            DataRow[] drFind = dsVitalSignsRec.Tables[0].Select(filter, "TIME_POINT");

            DateTime dtPre = DataType.DateTime_Null();
            for (int i = 0; i < drFind.Length; i++)
            {
                DateTime dtCurr = (DateTime)(drFind[i]["TIME_POINT"]);

                if (i == 0)
                {
                    arrayTime.Add(dtCurr);
                    dtPre = dtCurr.AddSeconds(-1 * dtCurr.Second);
                }
                else
                {
                    TimeSpan tSpan = dtCurr.Subtract(dtPre);
                    if (tSpan.TotalMinutes > 1)
                    {
                        arrayTime.Add(dtCurr);
                        dtPre = dtCurr.AddSeconds(-1 * dtCurr.Second);
                    }
                }
            }
        }


        /// <summary>
        /// 重新加载病人医嘱
        /// </summary>
        private void reloadVitalSignsRec()
        {
            Cursor.Current = Cursors.WaitCursor;
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;

                dsVitalSignsRec = vitalSignsDbI.GetVitalSignsRecord(GVars.Patient.ID, GVars.Patient.VisitId, GVars.GetDateNow());
                dsVitalSignsRec.AcceptChanges();

                initDisp();
            }
            finally
            {
                GVars.App.UserInput = blnStore;
                Cursor.Current = Cursors.Default;
            }
        }
        #endregion


        #region 中间缓存处理
        /// <summary>
        /// 创建中间表
        /// </summary>
        private void createMidTable()
        {
            dtStoreShow = new DataTable();

            dtStoreShow.Columns.Add("VITAL_SIGNS", Type.GetType("System.String"));
            dtStoreShow.Columns.Add("UNIT", Type.GetType("System.String"));
            dtStoreShow.Columns.Add("WARD_CODE", Type.GetType("System.String"));
            dtStoreShow.Columns.Add("CLASS_CODE", Type.GetType("System.String"));
            dtStoreShow.Columns.Add("VITAL_CODE", Type.GetType("System.String"));
            dtStoreShow.Columns.Add("ATTRIBUTE", Type.GetType("System.String"));
            dtStoreShow.Columns.Add("VITAL_SIGNS_CVALUES", Type.GetType("System.String"));
            dtStoreShow.Columns.Add("NURSE", Type.GetType("System.String"));
            dtStoreShow.Columns.Add("TIME_POINT", Type.GetType("System.DateTime"));
            dtStoreShow.Columns.Add("VALUE_TYPE", Type.GetType("System.String"));
            dtStoreShow.Columns.Add("VALUE_SCOPE", Type.GetType("System.String"));
            dtStoreShow.Columns.Add("MEMO", Type.GetType("System.String"));
        }


        /// <summary>
        /// 把记录往中间表加载
        /// </summary>
        private void loadInMidTable_Rec(int type)
        {
            string filter = "ATTRIBUTE = " + SqlManager.SqlConvert(type.ToString());
            DataRow[] drFind = dsNurseItemDict.Tables[0].Select(filter, "SHOW_ORDER");

            dtStoreShow.Rows.Clear();
            for (int i = 0; i < drFind.Length; i++)
            {
                DataRow dr = drFind[i];
                DataRow drNew = dtStoreShow.NewRow();

                drNew["VITAL_SIGNS"] = dr["VITAL_SIGNS"];
                drNew["UNIT"] = dr["UNIT"];
                drNew["WARD_CODE"] = dr["WARD_CODE"];
                drNew["CLASS_CODE"] = dr["CLASS_CODE"];
                drNew["ATTRIBUTE"] = dr["ATTRIBUTE"];
                drNew["VITAL_CODE"] = dr["VITAL_CODE"];
                drNew["VALUE_TYPE"] = dr["VALUE_TYPE"];
                drNew["VALUE_SCOPE"] = dr["VALUE_SCOPE"];

                dtStoreShow.Rows.Add(drNew);
            }

            if (drFind.Length > MAX_SHOW_ROWS - 3)
            {
                vsBar.Maximum = drFind.Length - MAX_SHOW_ROWS - 3;
            }
            else
            {
                vsBar.Maximum = 0;
            }

            vsBar.Value = 0;
        }


        /// <summary>
        /// 把记录的值往中间表加载
        /// </summary>
        private void loadInMidTable_Value(int type, DateTime dtTimePoint)
        {
            string filter = "ATTRIBUTE = " + SqlManager.SqlConvert(type.ToString())
                          + " AND TIME_POINT >= " + SqlManager.SqlConvert(dtTimePoint.ToString("yyyy-MM-dd HH:mm"))
                          + " AND TIME_POINT < " + SqlManager.SqlConvert(dtTimePoint.AddMinutes(1).ToString("yyyy-MM-dd HH:mm"));
            DataRow[] drFind = dsVitalSignsRec.Tables[0].Select(filter);

            // 清除旧数据
            foreach (DataRow dr in dtStoreShow.Rows)
            {
                dr["VITAL_SIGNS_CVALUES"] = string.Empty;
                dr["NURSE"] = string.Empty;
                dr["MEMO"] = string.Empty;
            }

            // 赋现在的值
            for (int i = 0; i < drFind.Length; i++)
            {
                filter = "VITAL_CODE = " + SqlManager.SqlConvert(drFind[i]["VITAL_CODE"].ToString());
                DataRow[] drFindIn = dtStoreShow.Select(filter);

                if (drFindIn.Length > 0)
                {
                    drFindIn[0]["VITAL_SIGNS_CVALUES"] = drFind[i]["VITAL_SIGNS_CVALUES"];
                    drFindIn[0]["NURSE"] = drFind[i]["NURSE"];
                    drFindIn[0]["TIME_POINT"] = drFind[i]["TIME_POINT"];
                    drFindIn[0]["MEMO"] = drFind[i]["MEMO"];
                }
            }
        }


        /// <summary>
        /// 保存到缓存
        /// </summary>
        /// <returns></returns>
        private bool saveInMidDispData()
        {
            DateTime dtCurrent = GVars.GetDateNow();

            if (DataType.DateTime_IsNull(ref dtTimePoint) == true)
            {
                dtTimePoint = dtCurrent;
            }

            int dataIndex = vsBar.Value - 1;
            TextBox txtValue;

            for (int i = 0; i < MAX_SHOW_ROWS; i++)
            {
                dataIndex++;

                if (dataIndex < dtStoreShow.Rows.Count)
                {
                    DataRow dr = dtStoreShow.Rows[dataIndex];

                    txtValue = getCell(i, 1);
                    string textValue = txtValue.Text.Trim();

                    if (dr["VITAL_SIGNS_CVALUES"].ToString().Equals(textValue) == false
                        || dr["MEMO"].ToString().Equals(txtValue.Tag.ToString()) == false)
                    {
                        // 保存到显示缓存
                        dr["VITAL_SIGNS_CVALUES"] = textValue;
                        dr["NURSE"] = GVars.User.Name;
                        dr["MEMO"] = txtValue.Tag.ToString();
                        dr["TIME_POINT"] = dtTimePoint;

                        // 存于整体缓存 dsVitalSignsRec
                        string filter = "VITAL_CODE = " + SqlManager.SqlConvert(dr["VITAL_CODE"].ToString())
                                        + "AND TIME_POINT >= " + SqlManager.SqlConvert(dtTimePoint.ToString("yyyy-MM-dd HH:mm"))
                                        + "AND TIME_POINT < " + SqlManager.SqlConvert(dtTimePoint.AddMinutes(1).ToString("yyyy-MM-dd HH:mm"));

                        DataRow[] drFind = dsVitalSignsRec.Tables[0].Select(filter);

                        // 如果是删除
                        if (textValue.Length == 0)
                        {
                            if (drFind.Length > 0)
                            {
                                drFind[0].Delete();
                            }

                            continue;
                        }

                        // 如果是增加或修改
                        DataRow drEdit = null;
                        if (drFind.Length > 0)
                        {
                            drEdit = drFind[0];

                            for (int c = 0; c < dsVitalSignsRec.Tables[0].Columns.Count; c++)
                            {
                                dsVitalSignsRec.Tables[0].Columns[c].ReadOnly = false;
                            }
                        }
                        else
                        {
                            drEdit = dsVitalSignsRec.Tables[0].NewRow();
                        }

                        drEdit["PATIENT_ID"] = GVars.Patient.ID;
                        drEdit["VISIT_ID"] = GVars.Patient.VisitId;
                        drEdit["TIME_POINT"] = dtTimePoint;
                        drEdit["VITAL_CODE"] = dr["VITAL_CODE"];
                        drEdit["ATTRIBUTE"] = dr["ATTRIBUTE"];
                        drEdit["RECORDING_DATE"] = dtTimePoint.Date;
                        drEdit["VITAL_SIGNS"] = dr["VITAL_SIGNS"];
                        drEdit["UNITS"] = dr["UNIT"];
                        drEdit["CLASS_CODE"] = dr["CLASS_CODE"];
                        drEdit["VITAL_SIGNS_CVALUES"] = dr["VITAL_SIGNS_CVALUES"];
                        drEdit["NURSE"] = dr["NURSE"];
                        drEdit["WARD_CODE"] = dr["WARD_CODE"];
                        drEdit["MEMO"] = dr["MEMO"];
                        drEdit["UPD_DATE_TIME"] = dtCurrent;

                        if (drFind.Length == 0)
                        {
                            dsVitalSignsRec.Tables[0].Rows.Add(drEdit);
                        }
                    }
                }
            }

            getTimePointList();
            setTimePointButtons();

            return true;
        }


        /// <summary>
        /// 对于更新项目, 重新设置更新时间
        /// </summary>
        private void resetModifyDateTime(DateTime dtNow)
        {
            for (int i = 0; i < dsVitalSignsRec.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dsVitalSignsRec.Tables[0].Rows[i];
                if (dr.RowState == DataRowState.Modified)
                {
                    dr["UPD_DATE_TIME"] = dtNow;
                }
            }
        }
        #endregion


        #region 时间点切换
        /// <summary>
        /// 设置病人导航按钮
        /// </summary>
        public void setTimePointButtons()
        {
            btnCurrTimePoint.Text = string.Empty;
            btnPreTimePoint.Text = "<";
            btnNextTimePoint.Text = ">";

            btnCurrTimePoint.Enabled = false;
            btnPreTimePoint.Enabled = false;
            btnNextTimePoint.Enabled = false;
            btnAddNurseEvent.Enabled = false;

            if (nurseType == TYPE_NURSE_EVENT)
            {
                btnAddNurseEvent.Enabled = true;
                return;
            }

            // 当前时间点
            btnCurrTimePoint.Enabled = true;
            if (DataType.DateTime_IsNull(ref dtTimePoint) == false)
            {
                btnCurrTimePoint.Text = dtTimePoint.ToString("HH:mm");
            }

            // 后面时间点
            if (btnCurrTimePoint.Text.Length > 0
                && nurseType != 3)
            {
                btnNextTimePoint.Enabled = true;
            }

            // 前面时间点
            btnPreTimePoint.Enabled = false;

            for (int i = 0; i < arrayTime.Count; i++)
            {
                DateTime dtCurrent = (DateTime)(arrayTime[i]);
                if (dtTimePoint.CompareTo(dtCurrent) > 0)
                {
                    btnPreTimePoint.Enabled = true;
                    break;
                }
            }
        }


        private void btnNextTimePoint_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dtSelected = DataType.DateTime_Null();

                for (int i = 0; i < arrayTime.Count; i++)
                {
                    DateTime dtCurrent = (DateTime)(arrayTime[i]);
                    if (dtTimePoint.CompareTo(dtCurrent) < 0)
                    {
                        dtSelected = dtCurrent;

                        break;
                    }
                }

                if (DataType.DateTime_IsNull(ref dtSelected) == true)
                {
                    dtSelected = GVars.GetDateNow();
                }

                dtTimePoint = dtSelected;

                // 加载中间表的值
                loadInMidTable_Value(nurseType, dtTimePoint);

                // 显示
                showVitalSignsRec();

                // 设置按钮                
                setTimePointButtons();

            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void btnPreTimePoint_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dtSelected = DataType.DateTime_Null();

                for (int i = arrayTime.Count - 1; i >= 0; i--)
                {
                    DateTime dtCurrent = (DateTime)(arrayTime[i]);
                    if (dtTimePoint.CompareTo(dtCurrent) > 0)
                    {
                        dtSelected = dtCurrent;
                        break;
                    }
                }

                if (DataType.DateTime_IsNull(ref dtSelected) == true)
                {
                    return;
                }

                dtTimePoint = dtSelected;

                // 加载中间表的值
                loadInMidTable_Value(nurseType, dtTimePoint);

                // 显示
                showVitalSignsRec();

                // 设置按钮                
                setTimePointButtons();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 当前时间点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCurrTimePoint_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnCurrTimePoint.Text.Length == 0)
                {
                    return;
                }

                // 判断有无数据
                if (dtStoreShow != null && dtStoreShow.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtStoreShow.Rows)
                    {
                        if (dr["VITAL_SIGNS_CVALUES"].ToString().Length > 0)
                        {
                            GVars.Msg.Show("I00010");       // 该时间点已有数据, 不能修改时间!
                            return;
                        }
                    }
                }

                // 修改时间点
                DateTimePickerFrm timeSelectFrm = new DateTimePickerFrm();
                timeSelectFrm.CurrentDateTime = dtTimePoint;
                if (timeSelectFrm.ShowDialog() == DialogResult.Yes)
                {
                    dtTimePoint = timeSelectFrm.CurrentDateTime;
                    setTimePointButtons();
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion


        #region 扫描器
        /// <summary>
        /// 扫描器 读取通知 事件的委托程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ScanReader_ReadNotify(object sender, EventArgs e)
        {
#if SCANNER
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // 获取病人ID号
                string barcode = GVars.ScanReaderBuffer.Text.Trim();

                // 如果不包含空格, 表示是病人的腕带
                if (barcode.IndexOf(ComConst.STR.BLANK) < 0 && barcode.IndexOf("T") < 0)
                {
                    if (patNavigator.ScanedPatient(barcode) == false) GVars.Msg.Show("W00005");   // 该病人不存在!

                    return;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                GVars.ScanReader.Actions.Read(GVars.ScanReaderBuffer);  // 再次开始等待扫描
            }
#endif
        }
        #endregion

    }
}