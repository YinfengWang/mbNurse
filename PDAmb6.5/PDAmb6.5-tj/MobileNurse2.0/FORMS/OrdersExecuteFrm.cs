using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SQL = HISPlus.SqlManager;
using HISPlus.FORMS;
using System.Globalization;
using System.Diagnostics;

namespace HISPlus
{
    public partial class OrdersExecuteFrm : Form
    {
        #region 变量
        /// <summary>
        /// 执行
        /// </summary>
        private const string STR_EXECUTE = "执行";
        /// <summary>
        /// 撤销
        /// </summary>
        private const string STR_CANCEL = "撤消";
        /// <summary>
        /// 完成
        /// </summary>
        private const string STR_FINISH = "完成";

        private PatientNavigator patNavigator = new PatientNavigator();   // 病人导航

        private string sqlSelData = string.Empty;             // 获取数据的SQL语句
        private OrdersDbI ordersDbI = new OrdersDbI();
        private DataSet dsOrdersExecute = null;
        private DataRow[] drOrderExecuteBill = null;

        private int timeRngStart = -6;                       // 医嘱执行单显示时间范围
        private int timeRngEnd = 6;                        // 医嘱执行单显示时间范围

        private string administr_drug = string.Empty;
        private string administr_trans = string.Empty;
        private string administr_infuse = string.Empty;
        private string administr_cure = string.Empty;

        private Color color_NotExecuted = Color.White;
        private Color color_Executed = Color.White;
        private Color color_Finished = Color.White;

        private string waveFile = string.Empty;

        private bool isFinish = false;                    //判断是否已完成

        private Symbol.Audio.Controller audioCtrl = null;                     // 声音控件

        private string start = DateTime.Now.ToString("yyyy-MM-dd 00-00-00");
        private string stop = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");

        private string startnew = string.Empty;
        private string stopnew = string.Empty;

        Stopwatch watch;
        #endregion

        #region  窗体初始化

        public OrdersExecuteFrm()
        {
            InitializeComponent();
        }
        public OrdersExecuteFrm(DateTime datastart, DateTime datastop)
        {
            InitializeComponent();
        }
        #endregion


        #region 窗体事件
        private void OrdersExecuteFrm_Load(object sender, EventArgs e)
        {
            try
            {
                patNavigator.BtnPrePatient = this.btnPrePatient;
                patNavigator.BtnCurrentPatient = this.btnCurrPatient;
                patNavigator.BtnNextPatient = this.btnNextPatient;
                patNavigator.BtnPatientList = this.btnListPatient;

                patNavigator.MenuItemPatient = mnuPatient;

                patNavigator.PatientChanged = new DataChanged(patientChanged);

                this.timer1.Enabled = true;

                // 定位病人
                patNavigator.SetPatientButtons();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 输液单类型切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void lvwType_SelectedIndexChanged(object sender, EventArgs e)comboBox1_SelectedIndexChanged
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (GVars.App.UserInput == false)
                {
                    return;
                }

                //if (lvwType.SelectedIndices.Count == 0)
                if (exceComboBox.SelectedItem.ToString().Length == 0)
                {
                    return;
                }

                showOrdersExecute(sender, e);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 执行单选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwOrderExecuteBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem lv in lvwOrderExecuteBill.Items)
                {
                    if (lv.Selected)
                    {
                        lv.Checked = true;
                    }

                }
                setButtons();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮 [详细信息]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picOrderExecuteDetail_Click(object sender, EventArgs e)
        {
            try
            {
                // 检查
                if (this.lvwOrderExecuteBill.SelectedIndices.Count == 0)
                {
                    return;
                }

                // 获取医嘱
                Cursor.Current = Cursors.WaitCursor;

                ListViewItem item = lvwOrderExecuteBill.Items[lvwOrderExecuteBill.SelectedIndices[0]];

                // 显示医嘱
                OrderDetailForm orderDetail = new OrderDetailForm();
                orderDetail.DrOrder = drOrderExecuteBill[int.Parse(item.Tag.ToString())];
                orderDetail.ShowOrderDetail = false;
                orderDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
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

                GVars.App.UserInput = true;
                showOrdersExecute(new object(), new System.EventArgs());

                // 初始化声音控件
                if (audioCtrl == null)
                {
                    Symbol.Audio.Device device = (Symbol.Audio.Device)Symbol.StandardForms.SelectDevice.Select(
                        Symbol.Audio.Controller.Title, Symbol.Audio.Device.AvailableDevices);

                    if (device != null)
                    {
                        audioCtrl = new Symbol.Audio.StandardAudio(device);
                    }
                }

                timer3.Enabled = true;

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
        /// 设置执行按钮是否可用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setbtnSaveEnable()
        {

            if (exceComboBox.SelectedIndex.ToString().Length <= 0)
                return;
            if (exceComboBox.SelectedIndex.ToString().Equals("2"))
            {
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
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
        /// 提示框单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelMsg_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnMsgOk.Visible == false)
                {
                    setMsgStatus(false, false, 0);
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// MessageBox的OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void btnMsgOk_Click(object sender, EventArgs e)
        {
            btnMsgOk.Enabled = false;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ListViewItem item = lvwOrderExecuteBill.Items[lvwOrderExecuteBill.SelectedIndices[0]];

                int index = int.Parse(item.Tag.ToString());
                int forward = 0;
                if (isFinish)
                {
                    forward = (btnFinish.Text.Equals(STR_FINISH) ? 2 : 1);
                }
                else
                {
                    forward = (btnSave.Text.Equals(STR_EXECUTE) ? 1 : 0);
                }

                //除输液单外 医嘱，可多选执行 2016-07-07 add
                foreach (ListViewItem lv in lvwOrderExecuteBill.Items)
                {
                    if (lv.Checked)
                    {
                        changeOrdersExecuteStatus(int.Parse(lv.Tag.ToString()), forward);
                    }
                    
                }
                 

                //changeOrdersExecuteStatus(index, forward);

                // 刷新显示
                showOrdersExecute(sender, e);

                setMsgStatus(false, false, 0);
            }
            catch (Exception ex)
            {
                //Cursor.Current = Cursors.Default;
                //Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                btnMsgOk.Enabled = true;
            }
        }


        /// <summary>
        /// MessageBox 的Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMsgCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                showOrdersExecute(sender, e);

                setMsgStatus(false, false, 0);
                return;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }


        /// <summary>
        /// 自动执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (panelMsg.Visible == true)
                {
                    timer2.Enabled = false;

                    if (btnMsgOk.Visible == true)
                    {
                        btnMsgOk_Click(sender, e);
                    }
                    else
                    {
                        setMsgStatus(false, false, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 声音播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer3_Tick(object sender, EventArgs e)
        {
            try
            {
                if (panelMsg.Visible == false || btnMsgOk.Visible == true || audioCtrl == null)
                {
                    return;
                }

                if (System.IO.File.Exists(waveFile) == true)
                {
                    audioCtrl.PlayAudio(1500, 2670, waveFile);
                }
            }
            catch
            {
            }
        }


        /// <summary>
        /// 按钮 [执行] 或 [撤销执行]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "执行单与病人匹配, 您确认" + btnSave.Text + "当前医嘱吗?";
                isFinish = false;
                lblMsg.ForeColor = Color.Blue;
                setMsgStatus(true, false, 10);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        /// 按钮 [完成] 或 [撤销完成]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "执行单与病人匹配, 您确认" + btnFinish.Text + "当前医嘱吗?";
                lblMsg.ForeColor = Color.Blue;
                isFinish = true;
                setMsgStatus(true, false, 10);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrdersExecuteFrm_Closed(object sender, EventArgs e)
        {
            try
            {
                if (audioCtrl != null)
                {
                    audioCtrl.Dispose();
                }
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
            try
            {
                //watch = Stopwatch.StartNew();

                // 声音文件
                string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
                waveFile = System.IO.Path.Combine(appPath, "Mismatch.wav");

                // 获取参数
                getParameters();

                // 显示病人医嘱执行单
                if (GVars.Patient.ID != null && GVars.Patient.ID.Length > 0)
                {
                    dsOrdersExecute = ordersDbI.GetOrdersExecuteTime(GVars.Patient.ID, GVars.Patient.VisitId, start, stop);

                    dsOrdersExecute.AcceptChanges();
                }

                //watch.Stop();

                //COMAPP.Function.SystemLog.RecordExpendTime("OrdersExecuteFrm", "initFrmVal/GetOrdersExecuteTime", GVars.ScanReaderBuffer.Text.Trim(), System.DateTime.Now.ToString(), watch.ElapsedMilliseconds.ToString());
            }
            catch (Exception ex)
            {
                GVars.Msg.Show(ex.Message);
                Cursor.Current = Cursors.Default;
                return;
            }

        }


        /// <summary>
        /// 获取参数
        /// </summary>
        private void getParameters()
        {
            // 获取参数
            string val = string.Empty;
            string[] parts = null;

            foreach (DataRow dr in GVars.Parameters.Tables[0].Rows)
            {
                val = dr["PARAMETER_VALUE"].ToString();

                switch (dr["PARAMETER_NAME"].ToString())
                {
                    case "ORDERS_EXECUTE_SHOW_RNG":                     // 执行单显示时间范围

                        parts = val.Split(",".ToCharArray());
                        if (parts.Length > 1)
                        {
                            timeRngStart = int.Parse(parts[0].Trim());
                            timeRngEnd = int.Parse(parts[1].Trim());
                        }
                        break;

                    case "ADMINISTRATION_DRUG":                         // 服药单途径
                        administr_drug = val;
                        break;

                    case "ADMINISTRATION_TRANSFUSE":                    // 输液单途径
                        administr_trans = val;
                        break;

                    case "ADMINISTRATION_INFUSE":                       // 注射单途径
                        administr_infuse = val;
                        break;

                    case "ADMINISTRATION_CURE":                         // 治疗单途径
                        administr_cure = val;
                        break;

                    case "COLOR_NOT_EXECUTED":                          // 未执行的颜色
                        parts = val.Split(",".ToCharArray());
                        if (parts.Length > 2)
                        {
                            color_NotExecuted = Color.FromArgb(int.Parse(parts[0]),
                                                int.Parse(parts[1]), int.Parse(parts[2]));
                        }
                        break;

                    case "COLOR_EXECUTED":                              // 已执行的颜色
                        parts = val.Split(",".ToCharArray());
                        if (parts.Length > 2)
                        {
                            color_Executed = Color.FromArgb(int.Parse(parts[0]),
                                                int.Parse(parts[1]), int.Parse(parts[2]));
                        }
                        break;

                    case "COLOR_FINISHED":                              // 未完成的颜色
                        parts = val.Split(",".ToCharArray());
                        if (parts.Length > 2)
                        {
                            color_Finished = Color.FromArgb(int.Parse(parts[0]),
                                                int.Parse(parts[1]), int.Parse(parts[2]));
                        }
                        break;

                    default:
                        break;
                }
            }
        }


        /// <summary>
        /// 初始化医嘱
        /// </summary>
        private void initDisp()
        {
            // 定位病人
            patNavigator.SetPatientButtons();

            // 类型
            //lvwType.Items[2].Selected = true;
            exceComboBox.SelectedIndex = 2;
        }


        /// <summary>
        /// 根据过滤条件显示医嘱
        /// </summary>
        private void showOrdersExecute(object sender, System.EventArgs e)
        {
            showOrdersExecute(string.Empty, string.Empty);
        }


        /// <summary>
        /// 根据过滤条件显示医嘱 ORDERS_PERFORM_SCHEDULE 
        /// </summary>
        private void showOrdersExecute(string orderNo, string ordersPerfrom)
        {
            string preOrderNo = string.Empty;
            string prePerformShedule = string.Empty;

            string grpChar = new string((char)128, 1);

            // 设置按钮状态
            setButtons();

            // 预处理
            if (GVars.App.UserInput == false)
            {
                return;
            }

            // 显示医嘱执行单
            bool blnStore = GVars.App.UserInput;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                GVars.App.UserInput = false;

                this.lvwOrderExecuteBill.BeginUpdate();

                //清空原来的列表
                this.lvwOrderExecuteBill.Items.Clear();

                if (dsOrdersExecute == null || dsOrdersExecute.Tables.Count == 0)
                {
                    return;
                }

                //if (lvwType.SelectedIndices.Count == 0)
                if (exceComboBox.SelectedItem.ToString().Length == 0)
                {
                    return;
                }

                // 获取执行单类别                
                string sort = "SCHEDULE_PERFORM_TIME DESC, ORDER_NO, ORDER_SUB_NO";

                //  HS 添加  修改   处理  BID 医嘱 问题的  医嘱号 相同     ， 像之前的 是 单纯的 看着患者ID 和 医嘱号 。 现在 加入 了一个
                //  计划执行时间   用来区分这一些东西。正好 。   SCHEDULE_PERFORM_TIME  = " + HISPlus.SqlManager.SqlConvert(schedulePerfromTime) : "");

                // 后来整理 以后 发现    为了能 成功的使用 还是这样在这里  添加  时间  限时 为 最好的。


                //  hs  后来   计划了一下 ， 如果是 添加时间段 来 统一的话 。 那个 实在是不合理。   
                // 后来经过大量的时间验证 。  不能添加 计划执行时间，。但是可以添加 那个    起到唯一表示的  一个字段就行了 。那个就是ORDERS_PERFORM_SCHEDULE



                string filter = (orderNo.Length > 0 ? "ORDER_NO = " + SqlManager.SqlConvert(orderNo) : "");

                filter += (ordersPerfrom.Length > 0 ? "and   ORDERS_PERFORM_SCHEDULE =" + HISPlus.SqlManager.SqlConvert(ordersPerfrom) : "");


                drOrderExecuteBill = dsOrdersExecute.Tables[0].Select(filter, sort);

                // 如果指定了医嘱, 自动设置类别
                //int    billClass= lvwType.Items[lvwType.SelectedIndices[0]].Index;              // 执行单类别
                int billClass = exceComboBox.SelectedIndex;
                
                //2016-07-06 Add 非输液单类型需要多选执行
                if (billClass != 2)                
                    lvwOrderExecuteBill.CheckBoxes = true;                
                else                
                    lvwOrderExecuteBill.CheckBoxes = false;
                


                if (orderNo.Length > 0 && drOrderExecuteBill.Length > 0)
                {
                    DataRow dr = drOrderExecuteBill[0];
                    int temp = getOrderExecuteBillClassCode(dr["ADMINISTRATION"].ToString(), dr["ORDER_CLASS"].ToString());

                    if (temp != billClass)
                    {
                        //lvwType.Items[temp].Selected = true;
                        exceComboBox.SelectedIndex = temp;
                    }

                    billClass = temp;

                    // 自动设置状态
                    string status = drOrderExecuteBill[0]["IS_EXECUTE"].ToString();
                    chkNotExecuted.Checked = (status.Length == 0 || status.Equals("0"));
                    chkExecuted.Checked = (status.Equals("1"));
                    chkFinished.Checked = (status.Equals("2"));
                }

                for (int i = 0; i < drOrderExecuteBill.Length; i++)
                {
                    DataRow dr = drOrderExecuteBill[i];

                    // 执行单类别
                    if (orderNo.Length == 0
                        && billClass != getOrderExecuteBillClassCode(dr["ADMINISTRATION"].ToString(), dr["ORDER_CLASS"].ToString()))
                    {
                        continue;
                    }

                    // 执行状态
                    string isExecute = dr["IS_EXECUTE"].ToString();

                    if ((chkNotExecuted.Checked && (isExecute.Equals("0") || isExecute.Length == 0))
                         || (chkExecuted.Checked && isExecute.Equals("1"))
                         || (chkFinished.Checked && isExecute.Equals("2"))
                       )
                    {

                    }
                    else
                    {
                        continue;
                    }

                    // 显示
                    string strGrp = string.Empty;
                    if (preOrderNo.Equals(dr["ORDER_NO"].ToString()) == false
                        || prePerformShedule.Equals(dr["SCHEDULE_PERFORM_TIME"].ToString()) == false)
                    {
                        strGrp = grpChar;
                        preOrderNo = dr["ORDER_NO"].ToString();
                        prePerformShedule = dr["SCHEDULE_PERFORM_TIME"].ToString();
                    }

                    ListViewItem item = new ListViewItem(strGrp);                               // 医嘱分组

                    item.SubItems.Add(dr["ORDER_TEXT"].ToString());                             // 医嘱内容
                    item.SubItems.Add(ComFunctionApp.GetDosageFormat(dr["DOSAGE"].ToString(),
                        dr["DOSAGE_UNITS"].ToString()));                                        // 剂量

                    if (prePerformShedule.Length > 0)
                    {
                        string schedule = ((DateTime)dr["SCHEDULE_PERFORM_TIME"]).ToString(ComConst.FMT_DATE.LONG);
                        // 21 06:00:00
                        schedule = schedule.Substring(8, (schedule.Length - 8));
                        schedule = schedule.Substring(0, 8);

                        item.SubItems.Add(schedule);                                            // 执行时间
                    }
                    else
                    {
                        item.SubItems.Add(string.Empty);                                        // 执行时间  表达式包含未定义的函数调用 to_date()。
                    }

                    item.SubItems.Add(dr["ORDER_NO"].ToString());
                    item.SubItems.Add(dr["ORDER_SUB_NO"].ToString());

                    item.Tag = i.ToString();

                    item.BackColor = getColor(isExecute);

                    this.lvwOrderExecuteBill.Items.Add(item);

                    item.Selected = (orderNo.Length > 0);
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
                this.lvwOrderExecuteBill.EndUpdate();

                Cursor.Current = Cursors.Default;
            }
        }


        /// <summary>
        /// 重新加载病人医嘱
        /// </summary>
        private void patientChanged()
        {
            Cursor.Current = Cursors.WaitCursor;
            bool blnStore = GVars.App.UserInput;

            try
            {
                watch = Stopwatch.StartNew();

                GVars.App.UserInput = false;


                if (GVars.Patient.ID != null && GVars.Patient.ID.Length > 0)
                {
                    // HS 添加，为   医嘱 添加时间段选择合适 时间段医嘱
                    if (startnew == string.Empty)
                    {
                        dsOrdersExecute = ordersDbI.GetOrdersExecuteTime(GVars.Patient.ID, GVars.Patient.VisitId, start, stop);
                    }
                    else
                    {
                        dsOrdersExecute = ordersDbI.GetOrdersExecuteTime(GVars.Patient.ID, GVars.Patient.VisitId, startnew, stopnew);
                    }
                    dsOrdersExecute.AcceptChanges();
                }

                watch.Stop();

                COMAPP.Function.SystemLog.RecordExpendTime("OrdersExecuteFrm", "patientChanged/GetOrdersExecuteTime", GVars.ScanReaderBuffer.Text.Trim(), System.DateTime.Now.ToString(), watch.ElapsedMilliseconds.ToString());

                // HS  添加实现固定跳转
                patNavigator.SetPatientButtons();

                GVars.App.UserInput = true;
                showOrdersExecute(new object(), new System.EventArgs());
            }
            catch (Exception ex)
            {
                //操作超时,清空当前医嘱列表
                lvwOrderExecuteBill.Items.Clear();
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
        /// 根据状态获取颜色
        /// </summary>
        /// <param name="executeStatus"></param>
        /// <returns></returns>
        private Color getColor(string executeStatus)
        {
            if (executeStatus.Equals("0") || executeStatus.Length == 0)
            {
                return color_NotExecuted;
            }

            if (executeStatus.Equals("1"))
            {
                //if (lvwType.Items[lvwType.SelectedIndices[0]].Index == 1)
                if (exceComboBox.SelectedIndex == 1)
                {
                    return color_Finished;
                }
                else
                {
                    return color_Executed;
                }
            }

            if (executeStatus.Equals("2"))
            {
                return color_Finished;
            }

            return Color.White;
        }


        /// <summary>
        /// 根据给药途径和方法判断某医嘱类别
        /// </summary>
        /// <remarks>注意: 修改时要与Web服务同步</remarks>
        /// <param name="administration">给药途径和方法</param>
        /// <param name="orderClass">医嘱类别</param>
        /// <remarks>
        /// </remarks>
        /// <returns>
        /// 0: 服药单 
        /// 1: 注射单
        /// 2: 输液单
        /// 3: 治疗单
        /// 4: 护理单
        /// 5: 其他(2015-11-16 添加)
        /// </returns>
        private int getOrderExecuteBillClassCode(string administration, string orderClass)
        {


            if (administration.Length >= 1)
            {
                // 服药单
                if (administr_drug.IndexOf(administration) >= 0)
                {
                    return 0;
                }
                // 注射单
                if (administr_infuse.IndexOf(administration) >= 0)
                {
                    return 1;
                }
                // 输液单
                if (administr_trans.IndexOf(administration) >= 0)
                {
                    return 2;
                }
                //治疗单
                if (orderClass.Trim().Equals("E"))
                {
                    return 3;
                }
                //护理单
                if (orderClass.Trim().Equals("H"))
                {
                    return 4;
                }
                //其他(其他单取orders_execute表中的order_class字段值不等于“A”,“E”,“H”的类别的医嘱)  2015-11-16 添加
                if (!orderClass.Trim().Equals("A") && !orderClass.Trim().Equals("E") && !orderClass.Trim().Equals("H"))
                {
                    return 5;
                }

            }

            // 护理单
            else if (orderClass.Trim().Equals("H"))
            {
                return 4;
            }
            //治疗单
            else if (orderClass.Trim().Equals("E"))
            {
                return 3;
            }
            //其他(其他单取orders_execute表中的order_class字段值不等于“A”,“E”,“H”的类别的医嘱)  2015-11-16 添加
            if (!orderClass.Trim().Equals("A") && !orderClass.Trim().Equals("E") && !orderClass.Trim().Equals("H"))
            {
                return 5;
            }
            //其他
            return 5;
        }


        /// <summary>
        /// 设置按钮
        /// </summary>
        private void setButtons()
        {
            btnSave.Enabled = false;
            btnSave.Text = STR_EXECUTE;

            btnFinish.Enabled = false;
            btnFinish.Text = STR_FINISH;

            if (lvwOrderExecuteBill.SelectedIndices.Count == 0)
            {
                return;
            }

            //if (lvwType.SelectedIndices.Count == 0)
            if (exceComboBox.SelectedItem.ToString().Length == 0)
            {
                return;
            }

            //int type = lvwType.Items[lvwType.SelectedIndices[0]].Index;
            int type = exceComboBox.SelectedIndex;

            ListViewItem item = lvwOrderExecuteBill.Items[lvwOrderExecuteBill.SelectedIndices[0]];
            DataRow drCurrent = drOrderExecuteBill[int.Parse(item.Tag.ToString())];

            string executeStatus = drCurrent["IS_EXECUTE"].ToString();
            executeStatus = (executeStatus.Length == 0 ? "0" : executeStatus);

            switch (executeStatus)
            {
                case "0":                       // 未执行 (可执行)
                    //btnSave.Enabled = true;
                    setbtnSaveEnable();
                    break;
                case "1":                       // 已执行 (可撤销执行, 可完成)
                    btnSave.Enabled = drCurrent["EXECUTE_NURSE"].ToString().Equals(GVars.User.Name);
                    btnSave.Text = STR_CANCEL;

                    // 如果是输液单
                    if (type == 2)
                    {
                        btnFinish.Enabled = true;
                    }
                    break;
                case "2":                       // 已完成 (可撤销完成)
                    btnFinish.Enabled = drCurrent["EXECUTE_NURSE"].ToString().Equals(GVars.User.Name);
                    btnFinish.Text = STR_CANCEL;
                    break;
                default:
                    break;
            }
        }

        #region 修改医嘱执行单的状态
        /// <summary>
        ///  修改医嘱执行单的状态
        /// </summary>
        /// <param name="index">记录位置</param>
        /// <param name="forward">状态向前一步</param>
        /// <returns></returns>
        private bool changeOrdersExecuteStatus(int index, int forward)
        {
            //DateTime? dtNow = GVars.GetDateNow();
            //DateTime? dtNull = new DateTime();

            DateTime dtNow = GVars.GetDateNow();

            // 获取记录
            DataRow dr = drOrderExecuteBill[index];

            string patientId = dr["PATIENT_ID"].ToString();
            string visitID = dr["VISIT_ID"].ToString();
            string schedule = ((DateTime)(dr["SCHEDULE_PERFORM_TIME"])).ToString(ComConst.FMT_DATE.LONG);
            string orderNo = dr["ORDER_NO"].ToString();

            string filter = string.Empty;
            filter += "ORDER_NO = " + HISPlus.SqlManager.SqlConvert(orderNo);
            filter += "AND PATIENT_ID = " + HISPlus.SqlManager.SqlConvert(patientId);
            filter += "AND VISIT_ID = " + HISPlus.SqlManager.SqlConvert(visitID);
            filter += "AND SCHEDULE_PERFORM_TIME = " + HISPlus.SqlManager.SqlConvert(schedule);

            DataRow[] drFind = dsOrdersExecute.Tables[0].Select(filter);


            // 更新状态
            for (int i = 0; i < drFind.Length; i++)
            {
                drFind[i]["IS_EXECUTE"] = forward;
                if (forward == 2)
                {
                }
                else
                {
                    drFind[i]["EXECUTE_NURSE"] = (forward.ToString().Equals("0") ? string.Empty : GVars.User.Name);
                    drFind[i]["EXECUTE_DATE_TIME"] = dtNow;
                    //drFind[i]["EXECUTE_DATE_TIME"] = (forward.ToString().Equals("0") ? dtNull : dtNow);
                }
            }

            DataSet ds = dsOrdersExecute.GetChanges();
            if (ordersDbI.SaveOrdersExecute(ref ds, string.Empty) == true)
            {
                dsOrdersExecute.AcceptChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 设置消息框状态
        /// <summary>
        /// 设置消息框状态
        /// </summary>
        private void setMsgStatus(bool blnVisible, bool blnError, int showTimes)
        {
            panelMsg.Visible = blnVisible;
            //lvwType.Enabled = (blnVisible == false);
            exceComboBox.Enabled = (blnVisible == false);
            lvwOrderExecuteBill.Enabled = (blnVisible == false);

            if (blnVisible)
            {
                if (showTimes > 0) timer2.Interval = showTimes * 1000;

                if (blnError)
                {
                    lblMsg.BackColor = Color.Red;
                    lblMsg.ForeColor = Color.White;

                    btnMsgOk.Visible = false;
                    btnMsgCancel.Visible = false;
                }
                else
                {
                    lblMsg.BackColor = Color.LightSkyBlue;
                    lblMsg.ForeColor = Color.Blue;

                    btnMsgOk.Visible = true;
                    btnMsgCancel.Visible = true;
                }

                if (showTimes > 0) timer2.Enabled = true;
            }
        }
        #endregion
        #endregion


        #region 扫描器
        /// <summary>
        /// 扫描器 读取通知 事件的委托程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ScanReader_ReadNotify(object sender, EventArgs e)
        {
            watch = Stopwatch.StartNew();
#if SCANNER
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // 获取病人ID号
                string barcode = GVars.ScanReaderBuffer.Text.Trim();
                if (panelMsg.Visible == true) return;                                             // 如果正显示消息, 不处理扫描事件

                // 如果不包含空格, 表示是病人的腕带
                if (barcode.IndexOf(ComConst.STR.BLANK) < 0 && barcode.IndexOf("T") < 0)
                {
                    if (patNavigator.ScanedPatient(barcode) == false)
                    {
                        lblMsg.Text = GVars.Msg.GetMsg("W00005");                                 // 该病人不存在!
                        setMsgStatus(true, true, -1);
                    }

                    return;
                }
                // 如果扫描的是输液卡
                else
                {
                    if (scanObject2(barcode) == false)
                    {
                        return;
                    }
                }

                // 默认执行
                //if (btnSave.Enabled == true && btnSave.Text.Equals(STR_EXECUTE) == true)    //执行单中输液单功能执行按钮改为无效，执行单中其他功能可以点击执行按钮不需要扫描
                if (btnSave.Text.Equals(STR_EXECUTE) == true)
                {
                    btnSave_Click(sender, e);
                }
                // 对已执行医嘱, 仅提示
                else if (btnSave.Text.Equals(STR_CANCEL) == true)
                {
                    lblMsg.Text = GVars.Msg.GetMsg("I00012");           // 病人与执行单匹配!
                    setMsgStatus(true, true, 5);
                }
                // 默认完成
                if (btnFinish.Enabled == true && btnFinish.Text.Equals(STR_FINISH) == true)
                {
                    btnFinish_Click(sender, e);
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

            watch.Stop();
            string time = watch.ElapsedMilliseconds.ToString();

            //记录扫码所用时间
            COMAPP.Function.SystemLog.RecordExpendTime("OrdersExecuteFrm", "ScanReader_ReadNotify", GVars.ScanReaderBuffer.Text.Trim(), System.DateTime.Now.ToString(), time);
        }


        /// <summary>
        /// 扫描输液卡，添加时间判断
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private bool scanObject2(string barcode)
        {

            string[] parts = barcode.Split("T".ToCharArray());
            string orderDate = string.Empty;

            try
            {
                orderDate = parts[3];
                if (!(parts[3].Equals(GVars.GetDateNow().ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo))))
                {
                    lblMsg.Text = GVars.Msg.GetMsg("E00063");               // 日期不正确!
                    lblMsg.Text += ComConst.STR.CRLF + "条码: " + barcode;

                    lblMsg.ForeColor = Color.Red;
                    setMsgStatus(true, true, -1);
                    return false;

                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = GVars.Msg.GetMsg("E00062");               // 格式不正确!
                lblMsg.Text += ComConst.STR.CRLF + "条码: " + barcode;

                lblMsg.ForeColor = Color.Red;
                setMsgStatus(true, true, -1);
                return false;
            }

            if (scanObject(barcode))
                return true;
            else
                return false;

        }

        /// <summary>
        /// 扫描输液卡
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private bool scanObject(string barcode)
        {
            // 如果不是正确的病人, 提示, 退出
            if (barcode.IndexOf(GVars.Patient.ID) != 0)
            {
                lblMsg.Text = GVars.Msg.GetMsg("I00007");               // 药品与病人不符!
                lblMsg.Text += ComConst.STR.CRLF + "病人ID:" + GVars.Patient.ID;
                lblMsg.Text += ComConst.STR.CRLF + "条码: " + barcode;

                lblMsg.ForeColor = Color.Red;
                setMsgStatus(true, true, -1);
                return false;
            }

            // 定位医嘱
            string orderNo = string.Empty;
            string dsdsdsd = string.Empty;
            string[] parts = barcode.Split("T".ToCharArray());
            if (parts.Length > 1)
            {
                orderNo = parts[1];
                dsdsdsd = parts[2];

            }
            else
            {
                lblMsg.Text = "条码格式不正确";                             // 药品与病人不符!
                lblMsg.Text += ComConst.STR.CRLF + "条码: " + barcode;

                lblMsg.ForeColor = Color.Red;
                setMsgStatus(true, true, -1);
                return false;
            }

            if (orderNo.Length > 0)
            {
                showOrdersExecute(orderNo, dsdsdsd);
                return true;
            }
            else
            {
                lblMsg.Text = "条码格式不正确, 医嘱号为空";                 // 药品与病人不符!
                lblMsg.Text += ComConst.STR.CRLF + "条码: " + barcode;

                lblMsg.ForeColor = Color.Red;
                setMsgStatus(true, true, -1);
                return false;
            }
        }


        /// <summary>
        /// 定位医嘱执行单
        /// </summary>
        /// <param name="orderNo"></param>
        private bool locateOrderExecute(string orderNo)
        {
            if (drOrderExecuteBill == null)
            {
                return false;
            }

            int col_order_no = lvwOrderExecuteBill.Columns.Count - 2;

            for (int i = 0; i < lvwOrderExecuteBill.Items.Count; i++)
            {
                ListViewItem item = lvwOrderExecuteBill.Items[i];

                string orderNoTest = item.SubItems[col_order_no].Text;

                if (orderNoTest.Equals(orderNo) == true)
                {
                    item.Selected = true;
                    lvwOrderExecuteBill.EnsureVisible(i);

                    return true;
                }
            }

            return false;
        }
        #endregion

        #region 确定
        private void finish()
        {
            try
            {
                lblMsg.Text = "执行单与病人匹配, 您确认" + btnFinish.Text + "当前医嘱吗?";

                lblMsg.ForeColor = Color.Blue;
                setMsgStatus(true, false, 10);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion

        #region 提供时间段选择
        private void menuItem1_Click(object sender, EventArgs e)
        {
            SelectTime selectime = new SelectTime();
            DialogResult result = selectime.ShowDialog();
            if (result == DialogResult.OK)
            {
                startnew = "20" + selectime.Start;
                stopnew = "20" + selectime.Stop;
                if (selectime.Start == string.Empty)
                {
                    startnew = string.Empty;
                }

                watch = Stopwatch.StartNew();

                // 显示病人医嘱执行单
                if (GVars.Patient.ID != null && GVars.Patient.ID.Length > 0)
                {
                    if (selectime.Start != string.Empty)
                    {
                        dsOrdersExecute = null;
                        showOrdersExecute(sender, e);
                        dsOrdersExecute = ordersDbI.GetOrdersExecuteTime(GVars.Patient.ID, GVars.Patient.VisitId, startnew, stopnew);
                        dsOrdersExecute.AcceptChanges();
                    }
                    else
                    {
                        dsOrdersExecute = ordersDbI.GetOrdersExecuteTime(GVars.Patient.ID, GVars.Patient.VisitId, start, stop);
                        dsOrdersExecute.AcceptChanges();
                    }

                    dsOrdersExecute.AcceptChanges();
                }

                watch.Stop();

                COMAPP.Function.SystemLog.RecordExpendTime("OrdersExecuteFrm", "menuItem1_Click/GetOrdersExecuteTime", GVars.ScanReaderBuffer.Text.Trim(), System.DateTime.Now.ToString(), watch.ElapsedMilliseconds.ToString());
            }
        }
        #endregion

        private void lvwOrderExecuteBill_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            
            
        }

    }
}