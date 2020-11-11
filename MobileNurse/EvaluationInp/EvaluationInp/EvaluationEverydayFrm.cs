using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraEditors;
using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class EvaluationEverydayFrm : FormDo, IBasePatient
    {
        #region 变量
        private const string RIGHT_VIEW = "0";                      //  查看权限
        private const string RIGHT_EDIT = "1";                      //  录入权限
        private const string RIGHT_MODIFY = "2";                      //  编辑别人录入记录的权限

        private const string ITEM_RECORDER = "RECORDER";               // 记录人
        private const string ITEM_RECORD_TIME = "RECORD_TIME";         // 记录时间
        private const string SEP_CHAR = ":";

        private const int SCROLL_STEP = 20;

        protected string _dict_id = "01";                     // 字典ID
        //原来的
        //protected bool        _moreTimes    = true;                     // 是否多次评估
        //LB20110708解决每日评估不能保存问题
        protected bool _moreTimes = false;                     // 是否多次评估
        protected string _template = "每日评估表";             // 模板文件

        private PatientDbI patientDbI = null;                     // 病人信息接口
        private DataSet dsPatient = null;                     // 病人信息
        private string patientId = string.Empty;             // 病人ID号
        private string visitId = string.Empty;             // 本次就诊序号

        private EvaluationDbI evaluationDbI;      // 评估接口

        /// <summary>
        /// 所有控件列表
        /// </summary>
        private ArrayList arrControls = new ArrayList();

        private DataSet dsItemDict = null;                     // 项目字典
        private DataSet dsEvalRec = null;                     // 评估记录

        private TextBox txtEdit = null;                     // 当前编辑文本框

        private int maxHeight = 0;                        // 编辑区域最大高度

        private string recorder = string.Empty;             // 记录人

        private ExcelAccess excelAccess = new ExcelAccess();
        private List<ExcelItem> arrExcelItem = new List<ExcelItem>();
        private int printCols = 7;

        private List<CalcItem> arrCalc = new List<CalcItem>();
        /// <summary>
        /// 实际控件
        /// </summary>
        private ArrayList arrCtrl = new ArrayList();

        private DateTime dtEvalTime = DateTime.Now;             // 评估时间
        private DataSet dsParentNode = null;

        private DataSet dsAppConfig = null;                     // 参数
        #endregion


        struct ExcelItem
        {
            public int Row;
            public int Col;
            public string ItemId;
            public string CheckValue;
        }


        struct CalcItem
        {
            public string sumId;
            public string subId;
        }


        public EvaluationEverydayFrm()
        {
            InitializeComponent();

            _id = "00038";
            _guid = "CC118166-670D-4cec-84E4-EDE789DD611E";

            _right = string.Empty;
        }


        #region 属性
        public string DictId
        {
            get { return _dict_id; }
            set { _dict_id = value; }
        }


        public bool MoreTimes
        {
            get { return _moreTimes; }
            set { _moreTimes = value; }
        }


        public string TemplateFile
        {
            get { return _template; }
            set { _template = value; }
        }
        #endregion


        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EvaluationEverydayFrm_Load(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                btnPrint.Enabled = false;
                btnMultiPrint.Enabled = false;
                if (GVars.OracleAccess != null)
                    evaluationDbI = new EvaluationDbI(GVars.OracleAccess);

                patientDbI = new PatientDbI(GVars.OracleAccess);

                dtDay.Enabled = _moreTimes;
                //grpPatientList.Height = this.Height - 19;
                //grpSeach.Top = grpPatientList.Height + grpPatientList.Top - grpSeach.Height;
                panel1.Height = grpSeach.Top - panel1.Top - 10;

                if (_dict_id.Equals("01") == false)
                {
                    btnPrint.TextValue = "打印";
                    btnMultiPrint.Visible = false;
                    //btnSave.Left  = btnMultiPrint.Left;
                }

                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 窗体退出前事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EvaluationEverydayFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (btnSave.Enabled == true)
                {
                    if (GVars.Msg.Show("Q0007") != DialogResult.Yes) // 记录已改变, 要保存吗?
                    {
                        return;
                    }
                    else
                    {
                        btnSave_Click(sender, new EventArgs());
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        /// 时钟消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (evaluationDbI == null) return;

            bool blnStore = GVars.App.UserInput;

            this.Cursor = Cursors.WaitCursor;

            try
            {
                GVars.App.UserInput = false;

               
                // 获取动态控件               
                getControlsInfo();

                // 获取字典
                dsItemDict = evaluationDbI.GetEvaluationDictItem(_dict_id);
                //dsItemDict.WriteXml(@"d:\dsItemDict.xml", XmlWriteMode.WriteSchema);

                // 获取参数
                string sql = "SELECT * FROM APP_CONFIG WHERE PARAMETER_NAME LIKE 'EI_" + _dict_id + "_%'";
                dsAppConfig = GVars.OracleAccess.SelectData(sql, "APP_CONFIG");

                _userRight = GVars.User.GetUserFrmRight(_id);

                createControls();

                // 重新布局窗体
                layoutDisp();

                // 加载合计配置
                loadCalcItems();

                //// 查询
                //if (patientListFrm.PatientId.Length > 0)
                //{
                //    patientListFrm_PatientChanged(null, new PatientEventArgs(patientListFrm.PatientId, patientListFrm.VisitId));
                //}
                Patient_SelectionChanged(null, new PatientEventArgs(GVars.Patient.ID, GVars.Patient.VisitId));                
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                timer1.Stop();
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                timer1.Enabled = false;
                timer1.Stop();
                GVars.App.UserInput = blnStore;
            }
        }

        public void Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
            GVars.Patient.ID = e.PatientId;
            GVars.Patient.VisitId = e.VisitId;

            patientId = GVars.Patient.ID;
            visitId = GVars.Patient.VisitId;

            btnSave.Enabled = false;

            if (_moreTimes == false)
            {
                btnQuery_Click(null, null);
            }
            else
            {
                refreshEvalTimes();
                
                if (cmbTime.Items.Count > 0)
                {
                    cmbTime.SelectedIndex = 0;
                }
                else
                {
                    btnQuery_Click(sender, e);
                }
            }
        }

        /// <summary>
        /// 日期改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtDay_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbTime.Visible == true)
                {
                    refreshEvalTimes();
                }
                else
                {
                    btnQuery_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 时间选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnQuery_Click(sender, e);
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
            bool blnStore = GVars.App.UserInput;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                GVars.App.UserInput = false;

                recorder = string.Empty;

                // 清除评估记录
                clearEvaluationRec();

                // 获取评估记录 每日评估
                if (dtDay.Enabled == true)
                {
                    //dsEvalRec = evaluationDbI.GetEvaluationRec(patientId, visitId, _dict_id, dtDay.Value.Date);
                    dtEvalTime = DateTime.Parse(dtDay.Value.Date.ToString(ComConst.FMT_DATE.SHORT + " " + cmbTime.Text));
                    dsEvalRec = evaluationDbI.GetEvaluationRec(patientId, visitId, _dict_id, dtEvalTime);
                }
                // 入院评估
                else
                {
                    dsEvalRec = evaluationDbI.GetEvaluationRec(patientId, visitId, _dict_id);
                }

                //dsEvalRec.WriteXml(@"d:\dsEvalRec.xml", XmlWriteMode.WriteSchema);

                // 获取病人信息
                dsPatient = patientDbI.GetInpPatientInfo_FromID(patientId, visitId);
                if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
                {
                    dsPatient = patientDbI.GetPatientInfo_FromID(patientId);
                }
                //dsPatient.WriteXml(@"d:\dsPatient.xml", XmlWriteMode.WriteSchema);

                // 获取记录人
                recorder = getRecorder();
                if (dtDay.Enabled == false)
                {
                    dtDay.Value = getRecordTime();
                    dtEvalTime = dtDay.Value;
                }

                // 按钮状态
                btnSave.Enabled = false;
                btnPrint.Enabled = true;
                btnMultiPrint.Enabled = true;
                btnDelete.Enabled = (cmbTime.SelectedIndex >= 0) && (recorder.Length == 0 || recorder.Equals(GVars.User.Name) || _right.IndexOf(RIGHT_MODIFY) >= 0);

                // 显示评估记录
                showEvaluationRec();

                if (btnSave.Enabled == true && patientId.Length > 0)
                {
                    btnSave_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                GVars.App.UserInput = blnStore;
            }
        }


        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false) return;

                RadioButton radio = (RadioButton)sender;

                if (radio.Checked == true)
                {
                    if (radio.Text.Equals("WNL"))
                    {
                        Control parent = radio.Parent;
                        if (parent != null)
                        {
                            foreach (Control a in parent.Controls)
                            {
                                if (a != radio)
                                {
                                    if (a.GetType().Name.Equals("RadioButton"))
                                    {
                                        ((RadioButton)a).Checked = false;
                                    }
                                    else
                                    {
                                        a.Enabled = false;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Control parent = radio.Parent;
                        if (parent != null)
                        {
                            foreach (Control a in parent.Controls)
                            {
                                if (a != radio)
                                {
                                    if (a.GetType().Name.Equals("RadioButton"))
                                    {
                                        ((RadioButton)a).Checked = false;
                                    }
                                    else
                                    {
                                        a.Enabled = true;
                                    }
                                }
                            }
                        }
                    }
                }

                btnSave.Enabled = chkSaveEnabled();
                //btnSave.Enabled = (patientId.Length > 0);// && (recorder.Length == 0 || GVars.User.Name.Equals(recorder));
                //btnSave.Enabled = btnSave.Enabled  && (_userRight.IndexOf(RIGHT_EDIT) >= 0 || _userRight.IndexOf(RIGHT_MODIFY) >= 0); 
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void checkBox_Click(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false) return;

                CheckBox a = (CheckBox)sender;

                if (a.Tag != null)
                {
                    OptionSelectTreeFrm selectFrm = new OptionSelectTreeFrm();

                    selectFrm.DictId = _dict_id;
                    selectFrm.PatientId = patientId;
                    selectFrm.VisitId = visitId;
                    selectFrm.DateTimeRec = dtEvalTime;// dtDay.Value.Date;                
                    selectFrm.ParentItemId = a.Tag.ToString();
                    selectFrm.DsItemDict = dsItemDict;
                    selectFrm.DsEvalRec = dsEvalRec;
                    selectFrm.DateTimeRec = dtEvalTime;

                    if (selectFrm.ShowDialog() == DialogResult.OK)
                    {
                        string[] parts = a.Text.Split(":".ToCharArray());

                        a.Text = parts[0];

                        if (selectFrm.SelectItems.Length > 0)
                        {
                            a.Text += ": " + selectFrm.SelectItems;
                            a.Checked = true;
                        }
                        else
                        {
                            a.Checked = false;
                        }

                        sprideResult(a.Tag.ToString());         // 波及其它控件

                        btnSave.Enabled = chkSaveEnabled();
                        //btnSave.Enabled = (patientId.Length > 0);// && (recorder.Length == 0 || GVars.User.Name.Equals(recorder));
                        //btnSave.Enabled = btnSave.Enabled  && (_userRight.IndexOf(RIGHT_EDIT) >= 0 || _userRight.IndexOf(RIGHT_MODIFY) >= 0); 
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void textBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                btnSave.Enabled = chkSaveEnabled();
                //btnSave.Enabled = (patientId.Length > 0);// && (recorder.Length == 0 || GVars.User.Name.Equals(recorder));
                //btnSave.Enabled = btnSave.Enabled  && (_userRight.IndexOf(RIGHT_EDIT) >= 0 || _userRight.IndexOf(RIGHT_MODIFY) >= 0); 
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false) return;

                grpEvaluation.Top = -1 * vScrollBar1.Value * SCROLL_STEP;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        ///  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // delete
                if (GVars.Msg.Show("Q0006", "删除", "当前评估") != DialogResult.Yes)
                {
                    return;
                }

                //dtEvalTime = DateTime.Parse(dtDay.Value.Date.ToString(ComConst.FMT_DATE.SHORT + " " + cmbTime.Text1));

                string sql = "DELETE FROM PAT_EVALUATION_M "
                            + "WHERE DICT_ID = " + SQL.SqlConvert(_dict_id)
                            + "AND PATIENT_ID = " + SQL.SqlConvert(patientId)
                            + "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                            + "AND RECORD_DATE = " + SQL.GetOraDbDate(dtEvalTime);
                GVars.OracleAccess.ExecuteNoQuery(sql);

                // delete
                cmbTime.Items.RemoveAt(cmbTime.SelectedIndex);

                // refresh
                btnQuery_Click(null, null);
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
        /// 按钮[保存]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                saveEvaluationDisp();
                retreatSaveResult();

                if (dtDay.Enabled == true)
                {
                    //evaluationDbI.SaveEvaluationRec(ref dsEvalRec, patientId, visitId, _dict_id, dtDay.Value.Date);
                    //DateTime dtEval = DateTime.Parse(dtDay.Value.Date.ToString(ComConst.FMT_DATE.SHORT + " " + cmbTime.Text1));
                    evaluationDbI.SaveEvaluationRec(ref dsEvalRec, patientId, visitId, _dict_id, dtEvalTime);
                }
                else
                {
                    evaluationDbI.SaveEvaluationRec(ref dsEvalRec, patientId, visitId, _dict_id);
                }

                btnSave.Enabled = false;
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
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                //dsEvalRec.WriteXml(@"d:\dsEvalRec.xml", XmlWriteMode.WriteSchema);
                //dsEvalRec.ReadXml(@"d:\dsEvalRec.xml", XmlReadMode.ReadSchema);

                ExcelTemplatePrint();
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
        /// 多日打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void multiPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DataSet dsRec = evaluationDbI.GetEvaluationRec(patientId, visitId, _dict_id,
                                dtDay.Value.Date, dtDay.Value.Date.AddDays(30));

                //dsRec.WriteXml(@"d:\dsRec2.xml", XmlWriteMode.WriteSchema);

                //DataSet dsRec = new DataSet();
                //dsRec.ReadXml(@"d:\dsRec2.xml", XmlReadMode.ReadSchema);

                ExcelTemplatePrint2(ref dsRec);
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
        /// 按钮[退出]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


        #region 共通函数
        /// <summary>
        /// 获取控件信息
        /// </summary>
        private void getControlsInfo()
        {
            if (evaluationDbI == null) return;

            DataSet ds = evaluationDbI.GetEvaluationImplement(_dict_id);
            //ds.WriteXml(@"d:\evalControl.xml", XmlWriteMode.WriteSchema);

            EvaluationUserControl control;
            string val = string.Empty;
            string[] parts = null;

            int x = 0;
            int y = 0;

            maxHeight = grpEvaluation.Height;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                control = new EvaluationUserControl();
                control.TYPE = dr["CONTROL_TYPE"].ToString();
                control.TEXT = dr["CONTROL_TEXT"].ToString();
                control.ID = dr["ITEM_ID"].ToString();
                control.PARENT = dr["PARENT_ID"].ToString();
                control.RNG = dr["CONTROL_RNG"].ToString();

                control.CHILDREN = (dr["EXIST_CHILD"].ToString().Trim().Equals("1"));

                val = dr["LOCATION"].ToString();
                parts = val.Split(",".ToCharArray());
                if (parts.Length > 1)
                {
                    if (int.TryParse(parts[0].Trim(), out x) == false) continue;
                    if (int.TryParse(parts[1].Trim(), out y) == false) continue;
                }

                control.LOCATION.X = x;
                control.LOCATION.Y = y;

                val = dr["CONTROL_SIZE"].ToString();
                parts = val.Split(",".ToCharArray());
                if (parts.Length > 1)
                {
                    if (int.TryParse(parts[0].Trim(), out x) == false) continue;
                    if (int.TryParse(parts[1].Trim(), out y) == false) continue;
                }

                control.SIZE.Width = x;
                control.SIZE.Height = y;

                arrControls.Add(control);

                if (control.LOCATION.Y + control.SIZE.Height > maxHeight)
                {
                    maxHeight = control.LOCATION.Y + control.SIZE.Height;
                }
            }
        }


        /// <summary>
        /// 获取GroupBox
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private PanelControl getGroupBox(string id)
        {
            foreach (Control a in grpEvaluation.Controls)
            {
                if (a.Name.Equals("GROUPBOX" + id) == true)
                {
                    return (PanelControl)a;
                }
            }

            return null;
        }


        /// <summary>
        /// 创建控件
        /// </summary>
        private void createControls()
        {
            arrCtrl.Clear();

            EvaluationUserControl control = null;
            for (int i = 0; i < arrControls.Count; i++)
            {
                control = (EvaluationUserControl)arrControls[i];

                Control a = null;
                PanelControl g = grpEvaluation;

                switch (control.TYPE)
                {
                    case "GROUPBOX": a = new GroupBox(); break;
                    case "TEXTBOX":
                        a = new TextBox();
                        ((TextBox)a).Multiline = true;

                        a.TextChanged += new EventHandler(textBox_TextChanged);
                        break;

                    case "LABEL": a = new Label(); break;

                    case "DATETIME":
                        a = new TextBox();
                        a.TextChanged += new EventHandler(textBox_TextChanged);
                        ((TextBox)a).ReadOnly = true;
                        break;

                    case "RADIO":
                        a = new RadioButton();
                        ((RadioButton)a).CheckedChanged += new EventHandler(radio_CheckedChanged);
                        break;

                    case "CHECKBOX":
                        a = new CheckBox();
                        ((CheckBox)a).AutoCheck = !control.CHILDREN;
                        ((CheckBox)a).AutoSize = true;

                        if (control.CHILDREN == true)
                        {
                            ((CheckBox)a).Click += new EventHandler(checkBox_Click);
                        }

                        ((CheckBox)a).CheckedChanged += new EventHandler(textBox_TextChanged);
                        ((CheckBox)a).CheckedChanged += new EventHandler(chk_CheckedChanged);
                        break;

                    default: continue;
                }

                if (control.PARENT.Length > 0 && getGroupBox(control.PARENT) != null)
                {
                    g = getGroupBox(control.PARENT);
                }

                a.Name = control.TYPE + control.ID;
                a.Text = control.TEXT;
                a.Location = control.LOCATION;
                a.Size = control.SIZE;
                a.Tag = control.ID;

                control.EditCtrl = a;

                g.Controls.Add(a);
                arrCtrl.Add(a);
            }
        }


        /// <summary>
        /// 重新布局窗体
        /// </summary>
        private void layoutDisp()
        {
            grpEvaluation.Height = vScrollBar1.Height;

            // 设置滚动条
            maxHeight += 200;

            if (vScrollBar1.Height > maxHeight)
            {
                vScrollBar1.Enabled = false;
            }
            else
            {
                vScrollBar1.Enabled = true;

                vScrollBar1.Maximum = (int)((maxHeight - vScrollBar1.Height) / SCROLL_STEP) + 1;

                grpEvaluation.Height = maxHeight;
            }

            // 界面控制
            cmbTime.Visible = _moreTimes;

            btnDelete.Visible = _moreTimes;
            btnDelete.Enabled = false;
        }


        /// <summary>
        /// 清除评估记录
        /// </summary>
        private void clearEvaluationRec()
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;

                for (int i = 0; i < arrControls.Count; i++)
                {
                    EvaluationUserControl userControl = (EvaluationUserControl)arrControls[i];

                    switch (userControl.TYPE)
                    {
                        case "GROUPBOX":
                            break;
                        case "TEXTBOX":
                            ((TextBox)userControl.EditCtrl).Text = string.Empty;
                            break;
                        case "LABEL":
                            break;
                        case "DATETIME":
                            ((TextBox)userControl.EditCtrl).Text = string.Empty;
                            break;
                        case "RADIO":
                            ((RadioButton)userControl.EditCtrl).Checked = false;
                            break;
                        case "CHECKBOX":
                            if (userControl.CHILDREN)
                            {
                                userControl.EditCtrl.Text = userControl.TEXT;
                            }
                            ((CheckBox)userControl.EditCtrl).Checked = false;
                            break;

                        default: continue;
                    }
                }
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }


        /// <summary>
        /// 显示评估记录
        /// </summary>
        private void showEvaluationRec()
        {
            int idx_ctrl = 0;
            int idx_rec = 0;

            int compare = 0;           // 0: 相等; 1:大于 -1:小于
            string itemValue = string.Empty;

            EvaluationUserControl userControl = null;

            bool blnChanged = false;

            // 已记录值
            idx_ctrl = 0;
            while (idx_rec < dsEvalRec.Tables[0].Rows.Count && idx_ctrl < arrControls.Count)
            {
                userControl = (EvaluationUserControl)arrControls[idx_ctrl];
                DataRow drRec = dsEvalRec.Tables[0].Rows[idx_rec];
                string itemId = drRec["ITEM_ID"].ToString();

                itemValue = drRec["ITEM_VALUE"].ToString();

                compare = itemId.CompareTo(userControl.ID);
                if (compare == 0)
                {
                    switch (userControl.TYPE)
                    {
                        case "GROUPBOX":
                            break;
                        case "TEXTBOX":
                            ((TextBox)userControl.EditCtrl).Text = itemValue;
                            break;
                        case "LABEL":
                            break;
                        case "DATETIME":
                            ((TextBox)userControl.EditCtrl).Text = itemValue;
                            break;
                        case "RADIO":
                            ((RadioButton)userControl.EditCtrl).Checked = true;
                            break;
                        case "CHECKBOX":
                            if (userControl.CHILDREN)
                            {
                                userControl.EditCtrl.Text = userControl.TEXT + getChildrenList(itemId);
                            }

                            ((CheckBox)userControl.EditCtrl).Checked = true;
                            break;

                        default: break;
                    }

                    idx_ctrl++;
                }

                // 查找下一个匹配
                if (idx_ctrl < arrControls.Count)
                {
                    userControl = (EvaluationUserControl)arrControls[idx_ctrl];

                    while (itemId.CompareTo(userControl.ID) > 0)
                    {
                        idx_ctrl++;
                        if (idx_ctrl >= arrControls.Count) break;

                        userControl = (EvaluationUserControl)arrControls[idx_ctrl];
                    }
                }

                compare = itemId.CompareTo(userControl.ID);
                if (compare == 0)
                {
                    continue;
                }
                else if (compare < 0)
                {
                    idx_rec++;
                }
                else
                {
                    idx_ctrl++;
                }
            }

            // 自动取值控件
            idx_ctrl = 0;
            while (idx_ctrl < arrControls.Count)
            {
                userControl = (EvaluationUserControl)arrControls[idx_ctrl];

                if (userControl.EditCtrl.Text.Trim().Length == 0
                    && userControl.RNG.Length > 0
                    && userControl.TYPE.Equals("TEXTBOX"))
                {
                    if (getVariableValue(userControl.RNG, ref itemValue) == true)
                    {
                        if (userControl.EditCtrl.Text.Equals(itemValue) == false)
                        {
                            userControl.EditCtrl.Text = itemValue;
                            blnChanged = true;
                        }
                    }
                }

                idx_ctrl++;
            }

            // 自动计算合计
            if (autoCalc() == true)
            {
                blnChanged = true;
            }

            if (blnChanged == true)
            {
                btnSave.Enabled = chkSaveEnabled();
                //btnSave.Enabled = true;
                //btnSave.Enabled = btnSave.Enabled  && (_userRight.IndexOf(RIGHT_EDIT) >= 0 || _userRight.IndexOf(RIGHT_MODIFY) >= 0); 
            }
        }


        /// <summary>
        /// 获取子项
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        private string getChildrenList(string itemId)
        {
            string filter = "ITEM_ID LIKE '" + itemId + "%' AND ITEM_ID <> '" + itemId + "'";
            DataRow[] drFind = dsEvalRec.Tables[0].Select(filter);

            string val = string.Empty;

            for (int i = 0; i < drFind.Length; i++)
            {
                if (i > 0) val += ComConst.STR.COMMA;
                val += drFind[i]["ITEM_NAME"].ToString();
            }

            if (val.Length > 0) val = ComConst.STR.COLON + val;

            return val;
        }


        /// <summary>
        /// 删除子项
        /// </summary>
        /// <param name="itemId"></param>
        private void delChildren(string itemId)
        {
            string filter = "ITEM_ID LIKE '" + itemId + "%' AND ITEM_ID <> '" + itemId + "'";
            DataRow[] drFind = dsEvalRec.Tables[0].Select(filter);

            string val = string.Empty;

            for (int i = 0; i < drFind.Length; i++)
            {
                drFind[i].Delete();
            }
        }


        /// <summary>
        /// 保存界面上的数据
        /// </summary>
        /// <returns></returns>
        private bool saveEvaluationDisp()
        {
            //DateTime    dtNow   = dtDay.Value;
            //if (cmbTime.Visible == true && cmbTime.Enabled == true && cmbTime.SelectedIndex >= 0)
            //{
            //    dtNow = DateTime.Parse(dtNow.ToString(ComConst.FMT_DATE.SHORT) + " " + cmbTime.Text1);
            //}

            DataRow drPre = null;                 // 前一条记录
            DataRow[] drRecs = dsEvalRec.Tables[0].Select(string.Empty, "ITEM_ID");
            int idx_rec = 0;

            EvaluationUserControl userControl = null;
            for (int i = 0; i < arrControls.Count; i++)
            {
                // 获取控件
                userControl = (EvaluationUserControl)arrControls[i];

                // 查找记录
                DataRow drEdit = null;
                string itemId = string.Empty;
                bool recExist = false;

                while (idx_rec < drRecs.Length)
                {
                    if (drRecs[idx_rec].RowState == DataRowState.Deleted
                        || drRecs[idx_rec].RowState == DataRowState.Detached)
                    {
                        idx_rec++;
                        continue;
                    }

                    itemId = drRecs[idx_rec]["ITEM_ID"].ToString();
                    if (userControl.ID.CompareTo(itemId) == 0) break;

                    if (itemId.CompareTo(userControl.ID) < 0)
                    {
                        idx_rec++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (idx_rec < drRecs.Length && userControl.ID.CompareTo(itemId) == 0)
                {
                    drEdit = drRecs[idx_rec];
                    recExist = true;
                }

                // 如果当前控件选中
                if (isControlSelected(ref userControl) == true)
                {
                    if (drPre == null || drPre.RowState == DataRowState.Deleted || drPre.RowState == DataRowState.Detached)
                    {
                        drPre = null;
                    }

                    // 如果记录存在
                    if (drPre != null && drPre["ITEM_ID"].ToString().Equals(userControl.ID) == true)
                    {
                        recExist = true;
                        drEdit = drPre;
                    }

                    if (recExist == false)
                    {
                        drEdit = dsEvalRec.Tables[0].NewRow();

                        drEdit["PATIENT_ID"] = patientId;
                        drEdit["VISIT_ID"] = visitId;
                        drEdit["DICT_ID"] = _dict_id;
                        drEdit["ITEM_ID"] = userControl.ID;
                        drEdit["ITEM_NAME"] = userControl.TEXT;
                        drEdit["DEPT_CODE"] = GVars.User.DeptCode;
                        drEdit["RECORD_DATE"] = dtEvalTime;   //dtNow.Date;
                    }

                    if (userControl.TYPE.Equals("TEXTBOX") || userControl.TYPE.Equals("DATETIME"))
                    {
                        drEdit["ITEM_VALUE"] = ((TextBox)userControl.EditCtrl).Text.Trim();
                    }

                    if (recExist == false)
                    {
                        dsEvalRec.Tables[0].Rows.Add(drEdit);
                    }

                    drPre = drEdit;
                }
                // 如果当前控件没被选中
                else
                {
                    if (userControl.TYPE.Equals("GROUPBOX") || userControl.TYPE.Equals("LABEL"))
                    {

                    }
                    else if (recExist == true)
                    {
                        if (userControl.TYPE.Equals("TEXTBOX") || userControl.TYPE.Equals("DATETIME"))
                        {
                            //drEdit["ITEM_VALUE"] = string.Empty;
                            drEdit.Delete();
                        }
                        else
                        {
                            delChildren(drEdit["ITEM_ID"].ToString());
                            drEdit.Delete();
                        }
                    }
                }
            }

            // 保存记录人
            saveRecorder(dtEvalTime);

            // 保存记录时间
            saveRecordTime(dtEvalTime);

            return true;
        }


        /// <summary>
        /// 控件是否被选中
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private bool isControlSelected(ref EvaluationUserControl control)
        {
            switch (control.TYPE)
            {
                case "GROUPBOX":
                case "LABEL":
                    break;
                case "TEXTBOX":
                case "DATETIME":
                    txtEdit = ((TextBox)control.EditCtrl);
                    return (txtEdit.Enabled && txtEdit.Text.Trim().Length > 0);

                case "RADIO":
                    return (control.EditCtrl.Enabled && ((RadioButton)control.EditCtrl).Checked);

                case "CHECKBOX":
                    return (control.EditCtrl.Enabled && ((CheckBox)control.EditCtrl).Checked);

                default:
                    break;
            }

            return false;
        }


        /// <summary>
        /// 用Excel模板打印，比较适合套打、格式、统计分析报表、图形分析、自定义打印
        /// </summary>
        /// <remarks>用Excel打印，步骤为：打开、写数据、打印预览、关闭</remarks>
        private void ExcelTemplatePrint()
        {
            // 加载excel节点
            loadExcelItem(_template);

            string strExcelTemplateFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Template\\" + _template + ".xls");

            excelAccess.Open(strExcelTemplateFile);				//用模板文件
            excelAccess.IsVisibledExcel = true;
            excelAccess.FormCaption = string.Empty;

            int row = 0;
            int col = 0;
            string itemId = string.Empty;
            string checkValue = string.Empty;

            DataRow[] drFind = dsEvalRec.Tables[0].Select(string.Empty, "ITEM_ID");
            DataRow drRec = null;

            // 输出固定记录
            ExcelItem excelItem;
            for (int i = 0; i < arrExcelItem.Count; i++)
            {
                excelItem = arrExcelItem[i];

                if (getVariableValue(excelItem.ItemId, ref checkValue) == true)
                {
                    excelAccess.SetCellText(excelItem.Row, excelItem.Col, checkValue);
                    continue;
                }

                if (excelItem.ItemId.Equals("RECORD_DATE") == true && drFind.Length > 0)
                {
                    drRec = drFind[0];
                    excelAccess.SetCellText(excelItem.Row, excelItem.Col,
                            ((DateTime)(drRec["RECORD_DATE"])).ToString(ComConst.FMT_DATE.LONG));
                    continue;
                }
            }

            // 其它记录            
            for (int i = 0; i < drFind.Length; i++)
            {
                drRec = drFind[i];

                itemId = drRec["ITEM_ID"].ToString();
                while (getItemAttr(itemId, ref row, ref col, ref checkValue) == true)
                {
                    if (checkValue.Length > 0)
                    {
                        excelAccess.SetCellText(row, col, checkValue);
                    }
                    else
                    {
                        excelAccess.SetCellText(row, col, drRec["ITEM_VALUE"].ToString());
                    }
                }
            }

            //excel.Print();				           //打印
            excelAccess.PrintPreview();			       //预览

            excelAccess.Close(false);				   //关闭并释放			
        }


        /// <summary>
        /// 用Excel模板打印，比较适合套打、格式、统计分析报表、图形分析、自定义打印
        /// </summary>
        /// <remarks>用Excel打印，步骤为：打开、写数据、打印预览、关闭</remarks>
        private void ExcelTemplatePrint2(ref DataSet ds)
        {
            string templateFileName = "每日评估表2";
            string strExcelTemplateFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Template\\" + templateFileName + ".xls");

            // 加载excel节点
            loadExcelItem(templateFileName);

            excelAccess.Open(strExcelTemplateFile);				// 用模板文件
            excelAccess.IsVisibledExcel = true;
            excelAccess.FormCaption = string.Empty;

            DataRow[] drFind = ds.Tables[0].Select(string.Empty, "RECORD_DATE, ITEM_ID");
            DataRow drRec = null;
            int idx_rec = 0;

            DateTime dtRecord = DateTime.MinValue;
            int row = 0;
            int col = 0;
            string itemId = string.Empty;
            string checkValue = string.Empty;

            // 输出固定记录
            ExcelItem excelItem;
            for (int i = 0; i < arrExcelItem.Count; i++)
            {
                excelItem = arrExcelItem[i];

                if (getVariableValue(excelItem.ItemId, ref checkValue) == true)
                {
                    excelAccess.SetCellText(excelItem.Row, excelItem.Col, checkValue);
                }
            }

            // 输出其它记录
            int counter = -1;
            while (counter < printCols && idx_rec < drFind.Length)
            {
                drRec = drFind[idx_rec];

                // 如果是改天
                if (dtRecord.Equals((DateTime)drRec["RECORD_DATE"]) == false)
                {
                    counter++;

                    // 日期
                    dtRecord = (DateTime)drRec["RECORD_DATE"];
                    if (getItemAttr("RECORD_DATE", ref row, ref col, ref checkValue) == true)
                    {
                        excelAccess.SetCellText(row, col + counter, dtRecord.ToString(ComConst.FMT_DATE.SHORT));
                    }

                    continue;
                }
                else
                {
                    itemId = drRec["ITEM_ID"].ToString();
                    if (getItemAttr(itemId, ref row, ref col, ref checkValue) == true)
                    {
                        if (checkValue.Length > 0)
                        {
                            excelAccess.SetCellText(row, col + counter, checkValue);
                        }
                        else
                        {
                            // 取存的值
                            string filter = "RECORD_DATE = " + SqlManager.SqlConvert(dtRecord.ToString(ComConst.FMT_DATE.SHORT))
                                        + "AND ITEM_ID LIKE '" + itemId + "%' AND LEN(ITEM_ID) > " + itemId.Length.ToString();
                            DataRow[] drResult = ds.Tables[0].Select(filter);
                            string content = string.Empty;
                            for (int j = 0; j < drResult.Length; j++)
                            {
                                if (content.Length > 0)
                                {
                                    content += ",";
                                }

                                content += drResult[j]["ITEM_NAME"].ToString();
                            }

                            if (content.Length == 0)
                            {
                                content = drRec["ITEM_VALUE"].ToString();
                            }

                            excelAccess.SetCellText(row, col + counter, content);
                        }
                    }

                    idx_rec++;
                }
            }

            //excel.Print();				           // 打印
            excelAccess.PrintPreview();			       // 预览

            excelAccess.Close(false);				   // 关闭并释放			
        }


        /// <summary>
        /// 获取配置文件的一行
        /// </summary>
        /// <param name="line"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>				
        private bool getParts(string line, ref int row, ref int col, ref string itemid, ref string checkValue)
        {
            line = line.Replace(ComConst.STR.BLANK, string.Empty);
            string[] arrParts = line.Split(ComConst.STR.COMMA.ToCharArray());

            if (arrParts.Length < 3) return false;

            itemid = arrParts[1];
            checkValue = arrParts[2];

            // 获取行列
            arrParts = arrParts[0].Split(":".ToCharArray());
            if (arrParts.Length <= 1)
            {
                return false;
            }

            // 行号
            if (int.TryParse(arrParts[0], out row) == false)
            {
                return false;
            }

            // 列号
            col = ExcelAccess.GetCol(arrParts[1]);

            return true;
        }


        /// <summary>
        /// 获取配置文件的一行
        /// </summary>
        /// <param name="line"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>				
        private bool getVariableValue(string variable, ref string variableValue)
        {
            if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
            {
                return false;
            }

            // 年龄单独处理
            switch (variable)
            {
                case "AGE":
                    if (dsPatient.Tables[0].Rows[0]["DATE_OF_BIRTH"].ToString().Length > 0)
                    {
                        DateTime dt = (DateTime)dsPatient.Tables[0].Rows[0]["DATE_OF_BIRTH"];

                        variableValue = PersonCls.GetAgeYear(dt, GVars.OracleAccess.GetSysDate()).ToString();
                        return true;
                    }

                    return false;
                case ITEM_RECORDER:
                    variableValue = getRecorder();// GVars.User.Name;
                    return true;
                case ITEM_RECORD_TIME:
                    variableValue = getRecordTime().ToString(ComConst.FMT_DATE.LONG); // DateTime.Now.ToString(ComConst.FMT_DATE.LONG);
                    return true;
                default:
                    break;
            }

            // 其它病人基本信息
            if (dsPatient.Tables[0].Columns.Contains(variable) == true)
            {
                variableValue = dsPatient.Tables[0].Rows[0][variable].ToString();
                return true;
            }

            // 护理信息
            variableValue = getFirstVitalRecordValue(variable);

            return (variableValue.Length > 0);
        }


        /// <summary>
        /// 获取当前记录的记录人
        /// </summary>
        /// <returns></returns>
        private string getRecorder()
        {
            if (dsEvalRec == null || dsEvalRec.Tables.Count == 0)
            {
                return string.Empty;
            }

            DataRow[] drFind = dsEvalRec.Tables[0].Select("ITEM_NAME = " + SqlManager.SqlConvert(ITEM_RECORDER));
            if (drFind.Length > 0)
            {
                return drFind[0]["ITEM_VALUE"].ToString();
            }

            return string.Empty;
        }


        /// <summary>
        /// 获取当前记录的记录时间
        /// </summary>
        /// <returns></returns>
        private DateTime getRecordTime()
        {
            DateTime dtRecord = GVars.OracleAccess.GetSysDate();

            if (dsEvalRec == null || dsEvalRec.Tables.Count == 0)
            {
                return dtRecord;
            }


            DataRow[] drFind = dsEvalRec.Tables[0].Select("ITEM_NAME = " + SqlManager.SqlConvert(ITEM_RECORD_TIME));
            if (drFind.Length > 0)
            {
                DateTime.TryParse(drFind[0]["ITEM_VALUE"].ToString(), out dtRecord);
            }

            return dtRecord;
        }


        /// <summary>
        /// 获取当前记录的记录人
        /// </summary>
        /// <returns></returns>
        private bool saveRecorder(DateTime dtNow)
        {
            if (dsEvalRec == null || dsEvalRec.Tables.Count == 0)
            {
                return false;
            }

            string filter = "ITEM_NAME = " + SqlManager.SqlConvert(ITEM_RECORDER);
            DataRow[] drFind = dsEvalRec.Tables[0].Select(filter);
            DataRow[] drItems = dsItemDict.Tables[0].Select(filter);

            if (drFind.Length > 0)
            {
                return true;
            }

            if (drItems.Length == 0)
            {
                return false;
            }

            DataRow drNew = dsEvalRec.Tables[0].NewRow();

            drNew["PATIENT_ID"] = patientId;
            drNew["VISIT_ID"] = visitId;
            drNew["DICT_ID"] = _dict_id;
            drNew["ITEM_ID"] = drItems[0]["ITEM_ID"].ToString();
            drNew["ITEM_NAME"] = drItems[0]["ITEM_NAME"].ToString();
            drNew["ITEM_VALUE"] = (recorder.Length == 0 ? GVars.User.Name : recorder);
            drNew["DEPT_CODE"] = GVars.User.DeptCode;
            drNew["RECORD_DATE"] = dtNow;

            dsEvalRec.Tables[0].Rows.Add(drNew);

            return true;
        }


        /// <summary>
        /// 获取当前记录的记录时间
        /// </summary>
        /// <returns></returns>
        private bool saveRecordTime(DateTime dtNow)
        {
            if (dsEvalRec == null || dsEvalRec.Tables.Count == 0)
            {
                return false;
            }

            string filter = "ITEM_NAME = " + SqlManager.SqlConvert(ITEM_RECORD_TIME);
            DataRow[] drFind = dsEvalRec.Tables[0].Select(filter);
            DataRow[] drItems = dsItemDict.Tables[0].Select(filter);

            if (drFind.Length > 0)
            {
                return true;
            }

            if (drItems.Length == 0)
            {
                return false;
            }

            DataRow drNew = dsEvalRec.Tables[0].NewRow();

            drNew["PATIENT_ID"] = patientId;
            drNew["VISIT_ID"] = visitId;
            drNew["DICT_ID"] = _dict_id;
            drNew["ITEM_ID"] = drItems[0]["ITEM_ID"].ToString();
            drNew["ITEM_NAME"] = drItems[0]["ITEM_NAME"].ToString();
            drNew["ITEM_VALUE"] = dtNow.ToString(ComConst.FMT_DATE.LONG);
            drNew["DEPT_CODE"] = GVars.User.DeptCode;
            drNew["RECORD_DATE"] = dtNow;

            dsEvalRec.Tables[0].Rows.Add(drNew);

            return true;
        }


        /// <summary>
        /// 获取第一条指定的护理项目记录
        /// </summary>
        /// <returns></returns>
        private string getFirstVitalRecordValue(string vitalCode)
        {
            string sql = "SELECT * FROM VITAL_SIGNS_REC "
                        + "WHERE PATIENT_ID = " + SQL.SqlConvert(patientId)
                        + "AND VISIT_ID = " + SQL.SqlConvert(visitId)
                        + "AND VITAL_CODE = " + SQL.SqlConvert(vitalCode);
            DataSet ds = GVars.OracleAccess.SelectData(sql);
            DataRow[] drFind = ds.Tables[0].Select(string.Empty, "TIME_POINT");
            if (drFind.Length > 0)
            {
                return drFind[0]["VITAL_SIGNS_CVALUES"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 获取某一天评估时间列表
        /// </summary>
        /// <returns></returns>
        private void refreshEvalTimes()
        {
            // 清空界面
            cmbTime.Items.Clear();

            // 获取数据
            string sql = "SELECT DISTINCT RECORD_DATE "
                       + "FROM PAT_EVALUATION_M "
                       + "WHERE PATIENT_ID = " + SQL.SqlConvert(patientId)
                       + "AND DICT_ID = " + SQL.SqlConvert(_dict_id)
                       + "AND TO_DATE(RECORD_DATE) = " + SQL.GetOraDbDate_Short(dtDay.Value);

            DataSet ds = GVars.OracleAccess.SelectData(sql, "RECORD_DATE");

            // 显示数据            
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                cmbTime.Items.Add(((DateTime)(dr["RECORD_DATE"])).ToString(ComConst.FMT_DATE.TIME));
            }
        }


        /// <summary>
        /// 处理保存结果
        /// </summary>
        /// <remarks>删除没有子节点的父节点</remarks>
        private void retreatSaveResult()
        {
            // 查找父节点
            if (dsParentNode == null)
            {
                string sql = "SELECT DISTINCT PARENT_ID FROM PAT_EVALUATION_IMPLEMENT";
                dsParentNode = GVars.OracleAccess.SelectData(sql, "PARENT_NODES");
            }

            // 处理父节点
            string filter = string.Empty;
            foreach (DataRow dr in dsParentNode.Tables[0].Rows)
            {
                if (dr["PARENT_ID"] == DBNull.Value)
                {
                    continue;
                }

                // 查找父节点是否存在
                string parentItemId = dr["PARENT_ID"].ToString();
                filter = "ITEM_ID = " + SQL.SqlConvert(parentItemId);
                DataRow[] drFind = dsEvalRec.Tables[0].Select(filter);
                if (drFind.Length == 0)
                {
                    continue;
                }

                DataRow drParent = drFind[0];

                // 查找该父节点的子节点是否存在
                filter = "ITEM_ID LIKE '" + parentItemId + "%'";
                drFind = dsEvalRec.Tables[0].Select(filter, "ITEM_ID");

                // 如果子节点不存在, 删除父节点
                if (drFind.Length <= 1)
                {
                    drParent.Delete();
                }
            }
        }


        /// <summary>
        /// 检查[保存]按钮是否可用
        /// </summary>
        /// <returns></returns>
        private bool chkSaveEnabled()
        {
            return (patientId.Length > 0) && (_userRight.IndexOf(RIGHT_EDIT) >= 0 || _userRight.IndexOf(RIGHT_MODIFY) >= 0)
                 && (cmbTime.Visible == false || (cmbTime.Visible == true && cmbTime.SelectedIndex >= 0));
        }


        /// <summary>
        /// 传播结果
        /// </summary>
        private void sprideResult(string itemId)
        {
            if (itemId.Length == 0)
            {
                return;
            }

            // 判断有无相关内容
            string paraName = "EI_" + _dict_id + "_" + itemId;
            string filter = "PARAMETER_NAME = " + SqlManager.SqlConvert(paraName);
            DataRow[] drFind = dsAppConfig.Tables[0].Select(filter);
            if (drFind.Length == 0)
            {
                return;
            }

            // 派生值
            while (drFind.Length > 0)
            {
                EvaluationUserControl controlDest = new EvaluationUserControl();
                EvaluationUserControl controlSrc = new EvaluationUserControl();
                int i = 0;

                // 源节点的值
                for (i = 0; i < arrControls.Count; i++)
                {
                    controlSrc = (EvaluationUserControl)(arrControls[i]);
                    if (controlSrc.TYPE.Equals("LABEL") == true) continue;

                    if (controlSrc.ID.Equals(itemId) == true) break;
                }
                if (i >= arrControls.Count) break;

                string srvVal = controlSrc.EditCtrl.Text;

                // 查找目标节点
                string itemId_dest = drFind[0]["PARAMETER_VALUE"].ToString().Trim();
                string resultRng = drFind[0]["PARAMETER_SCOPE"].ToString().Trim();

                for (i = 0; i < arrControls.Count; i++)
                {
                    controlDest = (EvaluationUserControl)(arrControls[i]);
                    if (controlDest.TYPE.Equals("LABEL") == true) continue;

                    if (controlDest.ID.Equals(itemId_dest) == true) break;
                }
                if (i >= arrControls.Count) break;

                // 目标节点赋值
                switch (drFind[0]["MEMO"].ToString().Trim().ToUpper())
                {
                    case "SUM":         // 合计总分
                        string sumVal = getSumVal(itemId);
                        controlDest.EditCtrl.Text = string.Empty;
                        if (sumVal.Length > 0)
                        {
                            controlDest.EditCtrl.Text = sumVal;
                        }

                        break;
                    case "EVAL":        // 评估结果
                        controlDest.EditCtrl.Text = string.Empty;
                        if (srvVal.Length > 0)
                        {
                            controlDest.EditCtrl.Text = getValDesc(srvVal, resultRng);
                        }

                        break;
                    case "EV_DATE":     // 评估日期
                        controlDest.EditCtrl.Text = string.Empty;
                        if (srvVal.Length > 0)
                        {
                            controlDest.EditCtrl.Text = GVars.OracleAccess.GetSysDate().ToString(ComConst.FMT_DATE.LONG);
                        }

                        break;
                    case "EV_RECORDER": // 评估人
                        controlDest.EditCtrl.Text = string.Empty;
                        if (srvVal.Length > 0)
                        {
                            controlDest.EditCtrl.Text = GVars.User.Name;
                        }

                        break;
                    default:
                        break;
                }

                // 查找下一个影响到的节点
                itemId = itemId_dest;

                paraName = "EI_" + _dict_id + "_" + itemId;
                filter = "PARAMETER_NAME = " + SqlManager.SqlConvert(paraName);
                drFind = dsAppConfig.Tables[0].Select(filter);
            }
        }


        /// <summary>
        /// 获取合计值
        /// </summary>
        /// <returns></returns>
        private string getSumVal(string itemId)
        {
            int sum = 0;
            EvaluationUserControl control;
            for (int i = 0; i < arrControls.Count; i++)
            {
                control = (EvaluationUserControl)(arrControls[i]);
                if (control.ID.Length == itemId.Length)
                {
                    sum += getIntValInText(control.EditCtrl.Text);
                }
            }

            if (sum > 0) return sum.ToString();
            return string.Empty;
        }


        /// <summary>
        /// 获取字符串中的数值
        /// </summary>
        /// <returns></returns>
        private int getIntValInText(string str)
        {
            int val = 0;
            string valStr = string.Empty;

            for (int i = 0; i < str.Length; i++)
            {
                if ("0123456789".Contains(str.Substring(i, 1)) == true)
                {
                    valStr += str.Substring(i, 1);
                }
                else if (valStr.Length > 0)
                {
                    break;
                }
            }

            if (valStr.Length > 0)
            {
                val = int.Parse(valStr);
            }

            return val;
        }


        /// <summary>
        /// 获取值描述
        /// </summary>
        /// <returns></returns>
        private string getValDesc(string srcVal, string scope)
        {
            if (srcVal.Length == 0)
            {
                return string.Empty;
            }

            int valTemp = int.Parse(srcVal);

            // 特重型(3-5); 重型(6-8);中型(9-12);轻型(13-15)
            string[] parts = scope.Split(")".ToCharArray());
            for (int i = 0; i < parts.Length; i++)
            {
                string[] partsIn = parts[i].Split("(".ToCharArray());
                if (partsIn.Length != 2) continue;

                string[] partsVal = partsIn[1].Split("-".ToCharArray());
                if (partsVal.Length != 2) continue;

                if (int.Parse(partsVal[0]) <= valTemp && valTemp <= int.Parse(partsVal[1]))
                {
                    return partsIn[0].Trim();
                }
            }

            return string.Empty;
        }
        #endregion


        #region 打印有关
        private void loadExcelItem(string templateFileName)
        {
            arrExcelItem.Clear();

            // 读取配置文件
            string iniFile = Path.Combine(Application.StartupPath, "Template\\" + templateFileName + ".ini");
            if (System.IO.File.Exists(iniFile) == true)
            {
                StreamReader sr = new StreamReader(iniFile);

                string line = string.Empty;
                int row = 0;
                int col = 0;
                string itemId = string.Empty;
                string checkValue = string.Empty;

                while ((line = sr.ReadLine()) != null)
                {
                    // 获取配置
                    if (getParts(line, ref row, ref col, ref itemId, ref checkValue) == false)
                    {
                        getPageConfig(line);

                        continue;
                    }

                    ExcelItem excelItem = new ExcelItem();
                    excelItem.Row = row;
                    excelItem.Col = col;
                    excelItem.ItemId = itemId;
                    excelItem.CheckValue = checkValue;

                    arrExcelItem.Add(excelItem);
                }

                sr.Close();
                sr.Dispose();
            }
        }


        private bool getItemAttr(string itemId, ref int row, ref int col, ref string checkValue)
        {
            ExcelItem excelItem;
            for (int i = 0; i < arrExcelItem.Count; i++)
            {
                excelItem = arrExcelItem[i];
                if (excelItem.ItemId.Equals(itemId) == true)
                {
                    row = excelItem.Row;
                    col = excelItem.Col;
                    checkValue = excelItem.CheckValue;

                    arrExcelItem.Remove(excelItem);
                    return true;
                }
            }

            return false;
        }


        private bool getPageConfig(string line)
        {
            line = line.Trim().ToUpper();
            string[] parts = line.Split("=".ToCharArray());

            if (parts.Length == 2 && parts[0].Trim().Equals("PRINT_COLS") == true)
            {
                int.TryParse(parts[1], out printCols);
            }

            return true;
        }
        #endregion


        #region 计算合计
        /// <summary>
        /// 加载合计项目
        /// </summary>
        private void loadCalcItems()
        {
            // 获取配置文件名
            string fileName = _template + "Calc.ini";
            fileName = System.IO.Path.Combine(Application.StartupPath, "Template\\" + fileName);
            if (File.Exists(fileName) == false) return;

            // 加载文件内容
            arrCalc.Clear();

            if (System.IO.File.Exists(fileName) == true)
            {
                StreamReader sr = new StreamReader(fileName);
                string line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Trim().Length == 0) continue;              // 如果是空行

                    // 必须为 a = c + d 格式
                    string[] parts = line.Split("=".ToCharArray());
                    if (parts.Length < 2) continue;
                    if (parts[0].Trim().Length == 0 || parts[1].Trim().Length == 0) continue;

                    // 保存 合计项
                    string sumId = parts[0].Trim();

                    parts = parts[1].Split("+".ToCharArray());
                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (parts[i].Trim().Length == 0) continue;

                        CalcItem item = new CalcItem();
                        item.sumId = sumId;
                        item.subId = parts[i].Trim();

                        arrCalc.Add(item);
                    }
                }

                sr.Close();
                sr.Dispose();
            }
        }


        /// <summary>
        /// 算分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void chk_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (GVars.App.UserInput == false) return;

                CheckBox chk = sender as CheckBox;
                if (chk == null || chk.Tag == null) return;

                // 判断是否需要合计
                string itemId = chk.Tag.ToString();
                string sumItemId = string.Empty;
                ArrayList arrSubItem = new ArrayList();

                CalcItem calcItem;
                for (int i = 0; i < arrCalc.Count; i++)
                {
                    calcItem = arrCalc[i];

                    if (calcItem.subId.Equals(itemId) == true)
                    {
                        sumItemId = calcItem.sumId;
                        break;
                    }
                }

                if (sumItemId.Length == 0) return;

                for (int i = 0; i < arrCalc.Count; i++)
                {
                    calcItem = arrCalc[i];

                    if (calcItem.sumId.Equals(sumItemId) == true)
                    {
                        arrSubItem.Add(calcItem.subId);
                    }
                }

                // 查找合计结果显示控件
                float sumValue = 0F;
                float subValue = 0F;
                Control ctrlSum = null;
                CheckBox chkCtrl = null;
                foreach (Control ctrl in arrCtrl)
                {
                    if (ctrl.Tag == null) continue;
                    if (ctrl.Tag.ToString().Equals(sumItemId) == true)
                    {
                        if (ctrl.GetType().Equals(typeof(TextBox)) == false)
                        {
                            continue;
                        }

                        ctrlSum = ctrl;
                        continue;
                    }

                    // 计算合计值
                    if (arrSubItem.Contains(ctrl.Tag.ToString()) == true)
                    {
                        chkCtrl = ctrl as CheckBox;
                        if (chkCtrl == null || chkCtrl.Checked == false) continue;

                        string[] parts = ctrl.Text.Split(" ".ToCharArray());
                        if (parts.Length < 2) continue;

                        if (float.TryParse(parts[0], out subValue) == true)
                        {
                            sumValue += subValue;
                        }
                    }
                }

                // 进行合计
                if (ctrlSum == null) return;
                ctrlSum.Text = sumValue.ToString();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        /// <summary>
        /// 自动计算
        /// </summary>
        private bool autoCalc()
        {
            bool blnChanged = false;

            // 查找合计结果显示控件
            Control ctrlSum = null;

            string itemId = string.Empty;
            CalcItem calcItem;
            foreach (Control ctrl in arrCtrl)
            {
                if (ctrl.Tag == null) continue;

                // 检查是否是文本控件
                if (ctrl.GetType().Equals(typeof(TextBox)) == false)
                {
                    continue;
                }

                // 检查是否是合计显示项
                ctrlSum = null;
                itemId = ctrl.Tag.ToString();
                for (int i = 0; i < arrCalc.Count; i++)
                {
                    calcItem = arrCalc[i];
                    if (calcItem.sumId.Equals(itemId) == true)
                    {
                        ctrlSum = ctrl;
                        break;
                    }
                }

                if (ctrlSum == null) continue; ;

                // 计算合计
                string sumValue = calcSum(ctrlSum.Tag.ToString());
                if (ctrlSum.Text.Equals(sumValue) == false && sumValue.Equals("0") == false)
                {
                    ctrlSum.Text = sumValue;
                    blnChanged = true;
                }
            }

            return blnChanged;
        }


        /// <summary>
        /// 计算合计值
        /// </summary>
        /// <returns></returns>
        private string calcSum(string sumItemId)
        {
            if (sumItemId.Length == 0) return string.Empty;

            // 获取该合计是由哪些项组成
            ArrayList arrSubItem = new ArrayList();

            CalcItem calcItem;
            for (int i = 0; i < arrCalc.Count; i++)
            {
                calcItem = arrCalc[i];

                if (calcItem.sumId.Equals(sumItemId) == true)
                {
                    arrSubItem.Add(calcItem.subId);
                }
            }

            // 查找合计结果显示控件
            float sumValue = 0F;
            float subValue = 0F;
            CheckBox chkCtrl = null;
            foreach (Control ctrl in arrCtrl)
            {
                if (ctrl.Tag == null) continue;
                if (ctrl.GetType().Equals(typeof(CheckBox)) == false)
                {
                    continue;
                }

                // 计算合计值
                if (arrSubItem.Contains(ctrl.Tag.ToString()) == true)
                {
                    chkCtrl = ctrl as CheckBox;
                    if (chkCtrl == null || chkCtrl.Checked == false) continue;

                    string[] parts = ctrl.Text.Split(" ".ToCharArray());
                    if (parts.Length < 2) continue;

                    if (float.TryParse(parts[0], out subValue) == true)
                    {
                        sumValue += subValue;
                    }
                }
            }

            return sumValue.ToString();
        }
        #endregion


        void IBasePatient.Patient_ListRefresh(object sender, PatientEventArgs e)
        {
             
        }
    }
}
