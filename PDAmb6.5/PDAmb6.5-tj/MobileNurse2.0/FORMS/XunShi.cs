using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HISPlus.COMAPP.DBI;
using System.Diagnostics;

namespace HISPlus
{
    public partial class XunShiFrm : Form
    {
        #region 变量
        private const int COL_ORDER_NO = 6;                             // 医嘱序号
        private const int COL_ORDER_SUB_NO = 7;                         // 子医嘱序号

        private PatientNavigator patNavigator = new PatientNavigator(); // 病人导航

        private XunShiDbI xunShiDbI = new XunShiDbI();
        private DataSet dsXunShis = null;

        private bool _isDefaultXunShi = true;  //默认记住上次巡视内容

        private DataSet dsXunShiSet = null;

        private DateTime dtXunShi = DateTime.MinValue;
        private string xunShiContent = string.Empty;

        Stopwatch watchXunShi;
        #endregion

        #region   初始化
        public XunShiFrm()
        {
            InitializeComponent();
            setBtnAddStatus(false);
        }
        #endregion


        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XunShiFrm_Load(object sender, EventArgs e)
        {
            try
            {
                patNavigator.BtnPrePatient = this.btnPrePatient;
                patNavigator.BtnCurrentPatient = this.btnCurrPatient;
                patNavigator.BtnNextPatient = this.btnNextPatient;
                patNavigator.BtnPatientList = this.btnListPatient;

                patNavigator.MenuItemPatient = mnuPatient;

                patNavigator.PatientChanged = new DataChanged(reloadOrders);

                this.timer1.Enabled = true;

                //cmbMemoClass.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        #region 设置添加按钮属性
        private void setBtnAddStatus(Boolean isVisible)
        {
            btnAdd.Visible = isVisible;
        }

        #endregion

        /// <summary>
        /// 菜单[返回]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Tag = ComConst.VAL.NO;
                this.Close();
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
            try
            {
                timer1.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                reloadOrders();

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
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XunShiFrm_Closed(object sender, EventArgs e)
        {
            if (this.Tag == null)
            {
                this.Tag = ComConst.VAL.NO;
            }
        }
        #endregion


        #region 共通函数
        /// <summary>
        /// 初始化医嘱数据
        /// </summary>
        private void initFrmVal()
        {
            if (GVars.Patient.ID != null && GVars.Patient.ID.Length > 0)
            {
                dsXunShis = xunShiDbI.GetXunShis(GVars.Patient.ID, GVars.Patient.VisitId, GVars.App.DeptCode);
            }

            //  显示  自定义  巡视 列表
            dsXunShiSet = xunShiDbI.GetXunShiClass(GVars.App.DeptCode);
            this.cmbMemoClass.Items.Clear();

            if (dsXunShiSet == null || dsXunShiSet.Tables.Count == 0)
            {
                return;
            }
            foreach (DataRow dr in dsXunShiSet.Tables[0].Rows)
            {
                string xunshiset = dr["xunshiContent"].ToString();

                this.cmbMemoClass.Items.Add(xunshiset);

            }
            //cmbMemoClass.SelectedIndex = 0;
            cmbMemoClass.SelectedIndex = GVars._DefauleXunShiMemo;
        }


        /// <summary>
        /// 初始化医嘱
        /// </summary>
        private void initDisp()
        {
            // 定位病人
            patNavigator.SetPatientButtons();

            if (dsXunShis == null || dsXunShis.Tables.Count == 0)
            {
                return;
            }
        }


        /// <summary>
        /// 根据过滤条件显示医嘱
        /// </summary>
        private void showXunShis(object sender, System.EventArgs e)
        {

            DateTime dtNow = DateTime.Now;

            if (GVars.App.UserInput == false)
            {
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                //orderClass = cmbOrderClass.Text;

                // 清空原来的列表
                this.lvwOrders.BeginUpdate();
                this.lvwOrders.Items.Clear();

                if (dsXunShis == null || dsXunShis.Tables.Count == 0)
                {
                    return;
                }

                // 获取 当前日期
                dtNow = GVars.GetDateNow();
                string strDtNow = dtNow.ToString(ComConst.FMT_DATE.SHORT);

                DataRow[] drFind = dsXunShis.Tables[0].Select(string.Empty, "EXECUTE_DATE desc");

                for (int i = 0; i < drFind.Length; i++)
                {
                    DataRow dr = drFind[i];


                    ListViewItem item = new ListViewItem((i + 1).ToString());                                           // 组

                    item.SubItems.Add(dr["NURSE"].ToString());
                    string startDate = string.Empty;
                    if (dr["EXECUTE_DATE"].ToString().Length > 0)
                    {
                        startDate = ComFunctionApp.GetDateFormat_Order((DateTime)dr["EXECUTE_DATE"]);
                    }

                    item.SubItems.Add(startDate);  // 巡视时间
                    if (dr["content"] == null)//增加备注
                    {
                        item.SubItems.Add("");
                    }
                    else
                    {
                        item.SubItems.Add(dr["content"].ToString());
                    }
                    this.lvwOrders.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                this.lvwOrders.EndUpdate();
            }
        }


        /// <summary>
        /// 重新加载巡视
        /// </summary>
        private void reloadOrders()
        {
            Cursor.Current = Cursors.WaitCursor;
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;

                initFrmVal();
                initDisp();

                GVars.App.UserInput = true;
                showXunShis(new object(), new System.EventArgs());
            }
            finally
            {
                GVars.App.UserInput = blnStore;
                Cursor.Current = Cursors.Default;
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
            watchXunShi = Stopwatch.StartNew();
#if SCANNER
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // 获取病人ID号
                string barcode = GVars.ScanReaderBuffer.Text.Trim();

                // 如果不包含空格, 表示是病人的腕带
                if (barcode.IndexOf(ComConst.STR.BLANK) < 0 && barcode.IndexOf("T") < 0)
                {
                    //if (patNavigator.ScanedPatient(barcode) == false) GVars.Msg.Show("W00005");   // 该病人不存在!

                    //return;
                    if (GVars.Patient.ID.Equals(barcode))
                    {
                        btnAdd_Click(null, null);
                        btnMsgOk.Enabled = true;
                        return;
                    }

                    if (patNavigator.ScanedPatient(barcode) == false)
                    {
                        GVars.Msg.Show("W00005");   // 该病人不存在!
                        return;
                    }
                    btnAdd_Click(null, null);
                    btnMsgOk.Enabled = true;
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


            watchXunShi.Stop();
            string time = watchXunShi.ElapsedMilliseconds.ToString();

            //记录扫码所用时间
            COMAPP.Function.SystemLog.RecordExpendTime("XunShi", "ScanReader_ReadNotify", GVars.Patient.Name, System.DateTime.Now.ToString(), time);
        }



        #endregion

        #region  添加 巡视 内容
        private void btnAdd_Click(object sender, EventArgs e)
        {
            dtXunShi = GVars.GetDateNow();
            //if (cmbMemoClass.SelectedIndex == 1) //未在病房
            //    xunShiContent = cmbMemoClass.Text.Trim();
            //else if (cmbMemoClass.SelectedIndex == 2) //其他
            //{
            //    xunShiContent = txtMemo.Text.Trim();
            //    if (xunShiContent.Length == 0)
            //    {
            //        lblMsg.Text = "选择其他必须录入具体内容";
            //        lblMsg.BackColor = Color.Red;
            //        setMsgStatus(true, true, -1);
            //        return;
            //    }
            //}
            //else if (cmbMemoClass.SelectedIndex == 0)
            //{
            //    xunShiContent = "";
            //}
            cmbMemoClass.SelectedIndex = GVars._DefauleXunShiMemo;
            xunShiContent = cmbMemoClass.Text.Trim();
            lblMsg.Text = "当前巡视的护士是:" + GVars.User.Name + ";\n" + "时间是:" + dtXunShi.ToString();
            //lblMsg.Text += ";备注：" + xunShiContent;
            lblMsg.Text += "\r\n 确认添加？";
            lblMsg.ForeColor = Color.Yellow;
            setMsgStatus(true, false, 10);
        }


        /// <summary>
        /// MessageBox的OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void btnMsgOk_Click(object sender, EventArgs e)
        {
            try
            {
                btnMsgOk.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;

                SaveXunShi();
                // 刷新显示
                showXunShis(new object(), new System.EventArgs());
                //reloadOrders();
                setMsgStatus(false, false, 0);
            }
            catch (Exception ex)
            {
                setMsgStatus(false, false, 0);
                Cursor.Current = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                btnMsgOk.Enabled = true;
                Cursor.Current = Cursors.Default;

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
                // 刷新显示
                //reloadOrders();
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
        /// 设置消息框状态
        /// </summary>
        private void setMsgStatus(bool blnVisible, bool blnError, int showTimes)
        {
            panelMsg.Visible = blnVisible;
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
                    lblMsg.BackColor = Color.White;
                    lblMsg.ForeColor = Color.Black;

                    btnMsgOk.Visible = true;
                    btnMsgCancel.Visible = true;
                }

                if (showTimes > 0) timer2.Enabled = true;
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
                //if (panelMsg.Visible == true)
                //{
                //    timer2.Enabled = false;

                //    if (btnMsgOk.Visible == true)
                //    {
                //        btnMsgOk_Click(sender, e);
                //    }
                //    else
                //    {
                //        setMsgStatus(false, false, 0);
                //    }
                //}
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        ///  修改医嘱执行单的状态
        /// </summary>
        /// <param name="index">记录位置</param>
        /// <param name="forward">状态向前一步</param>
        /// <returns></returns>
        private bool SaveXunShi()
        {
            DataRow dr = dsXunShis.Tables[0].NewRow();
            xunShiContent = cmbMemoClass.SelectedItem.ToString();
            dr["ward_code"] = GVars.App.DeptCode;
            dr["PATIENT_ID"] = GVars.Patient.ID;
            dr["VISIT_ID"] = GVars.Patient.VisitId;
            dr["NURSE"] = GVars.User.Name;
            dr["EXECUTE_DATE"] = GVars.GetDateNow();
            dr["content"] = xunShiContent; //xunShiContent;
            dsXunShis.Tables[0].Rows.Add(dr);



            DataSet ds = dsXunShis.GetChanges();

            //test
            //HISPlus.COMAPP.Function.SystemLog.SetTestLog("SaveXunShi",HISPlus.COMAPP.Function.SystemLog.ConvertDataSetToXML(ds));

            if (xunShiDbI.SaveXunShi(ref ds, string.Empty) == true)
            {
                dsXunShis.AcceptChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool InsXunShi()
        {
            string ward_code = GVars.App.DeptCode;
            string PATIENT_ID = GVars.Patient.ID;
            string VISIT_ID = GVars.Patient.VisitId;
            string NURSE = GVars.User.Name;
            string EXECUTE_DATE = GVars.GetDateNow().ToString();
            string content = cmbMemoClass.SelectedItem.ToString(); //xunShiContent;

            if (true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void cmbMemoClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMemo.Text = "";
            if (cmbMemoClass.SelectedIndex == 3)
            {
                txtMemo.Visible = true;
            }
            else
            {
                txtMemo.Visible = false;
            }

            //进行验证 T:记住当前选择,下次直接选择该项  F:默认选择第一个
            if (_isDefaultXunShi)
            {
                GVars._DefauleXunShiMemo = cmbMemoClass.SelectedIndex;
            }
        }

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
        #endregion
    }
}