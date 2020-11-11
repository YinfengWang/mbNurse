using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class OrdersFrm : Form
    {
        #region 变量
        private const int COL_ORDER_NO = 6;                             // 医嘱序号
        private const int COL_ORDER_SUB_NO = 7;                         // 子医嘱序号
        
        private PatientNavigator patNavigator = new PatientNavigator(); // 病人导航
        
        private OrdersDbI ordersDbI = new OrdersDbI();
        private DataSet   dsOrders  = null;
        #endregion
        
        public OrdersFrm()
        {
            InitializeComponent();
            
            this.cmbOrderClass.SelectedIndexChanged += new EventHandler(showOrders);
            this.cmbOrderTerm.SelectedIndexChanged += new EventHandler(showOrders);
        }
        
        
        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrdersFrm_Load(object sender, EventArgs e)
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
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
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
            catch(Exception ex)
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
                foreach(MenuItem mnu in mnuNavigator.MenuItems)
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
        /// [医嘱]详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picOrderDetail_Click(object sender, EventArgs e)
        {
            try
            {
                // 检查
                if (this.lvwOrders.SelectedIndices.Count == 0)
                {
                    return;
                }
                
                // 获取医嘱
                Cursor.Current = Cursors.WaitCursor;

                ListViewItem item = this.lvwOrders.Items[this.lvwOrders.SelectedIndices[0]];
                
                string filter = "ORDER_NO = " + SqlManager.SqlConvert(item.SubItems[COL_ORDER_NO].Text);
                filter += " AND ORDER_SUB_NO = " + SqlManager.SqlConvert(item.SubItems[COL_ORDER_SUB_NO].Text);
                
                DataRow[] drFind = dsOrders.Tables[0].Select(filter);
                
                if (drFind.Length == 0)
                {
                    return;
                }
                
                // 显示医嘱
                OrderDetailForm orderDetail = new OrderDetailForm();
                orderDetail.DrOrder = drFind[0];
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
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrdersFrm_Closed(object sender, EventArgs e)
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
                dsOrders = ordersDbI.GetOrders(GVars.Patient.ID, GVars.Patient.VisitId, GVars.GetDateNow());
            }
        }
        
        
        /// <summary>
        /// 初始化医嘱
        /// </summary>
        private void initDisp()
        {   
            // 定位病人
            patNavigator.SetPatientButtons();
            
            // 长期/临时
            this.cmbOrderTerm.SelectedIndex = 0;
            
            // 医嘱类别
            this.cmbOrderClass.Items.Clear();
            this.cmbOrderClass.Items.Add(string.Empty);                             // 空白,表示所有医嘱
            
            if (dsOrders == null || dsOrders.Tables.Count == 0)
            {
                return;
            }
            
            foreach(DataRow dr in dsOrders.Tables[0].Rows)
            {
                string orderClass = dr["ORDER_CLASS_NAME"].ToString();
                if (cmbOrderClass.Items.Contains(orderClass) == false)
                {
                    this.cmbOrderClass.Items.Add(orderClass);                       // 医嘱类别
                }                
            }
            
            this.cmbOrderClass.SelectedIndex = 0;
        }

        
        /// <summary>
        /// 根据过滤条件显示医嘱
        /// </summary>
        private void showOrders(object sender, System.EventArgs e)
        {
            const   int LONG_TERM   = 1;                                                        // 长期
            const   int SHORT_TERM  = 2;                                                        // 临时
            
            bool     blnPassed      = false;
            string   orderClass     = string.Empty;
            DateTime dtNow          = DateTime.Now;

            string   preOrderNo     = string.Empty;
            string  grpChar         = new string((char)128, 1);
            
            if (GVars.App.UserInput == false)
            {
                return;
            }
            
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                orderClass = cmbOrderClass.Text;
                
                // 清空原来的医嘱列表
                this.lvwOrders.BeginUpdate();
                this.lvwOrders.Items.Clear();
                
                if (dsOrders == null || dsOrders.Tables.Count == 0)
                {
                    return;
                }
                
                // 获取 当前日期
                dtNow = GVars.GetDateNow();
                string strDtNow = dtNow.ToString(ComConst.FMT_DATE.SHORT);
                
                DataRow[] drFind = dsOrders.Tables[0].Select(string.Empty, "ORDER_NO, ORDER_SUB_NO");
                
                for (int i = 0; i < drFind.Length; i++)
                {
                    DataRow dr = drFind[i];
                    
                    // -------------------- 过滤 ---------------------------
                    // 医嘱类别过滤
                    blnPassed = ((orderClass.Trim().Length == 0)
                        || dr["ORDER_CLASS_NAME"].ToString().Equals(orderClass));
                    
                    // 长期医嘱
                    if (blnPassed == true && cmbOrderTerm.SelectedIndex == LONG_TERM)
                    {
                        blnPassed = (dr["REPEAT_INDICATOR"].ToString().Equals("1") == true);
                    }
                    
                    // 临时医嘱
                    if (blnPassed == true && cmbOrderTerm.SelectedIndex == SHORT_TERM)
                    {
                        blnPassed = (dr["REPEAT_INDICATOR"].ToString().Equals("1") == false);
                    }
                    
                    if (blnPassed == false)
                    {
                        continue;
                    }

                    // -------------------- 显示 ---------------------------
                    // 如果是同一组
                    string strGrp = string.Empty;
                    if (preOrderNo.Equals(dr["ORDER_NO"].ToString()) == false)
                    {
                        strGrp      = grpChar;
                        preOrderNo  = dr["ORDER_NO"].ToString();
                    }
                    
                    ListViewItem item = new ListViewItem(strGrp);                                           // 组
                    
                    item.SubItems.Add(dr["ORDER_TEXT"].ToString());                                         // 医嘱内容
                    item.SubItems.Add(ComFunctionApp.GetDosageFormat(dr["DOSAGE"].ToString(),
                        dr["DOSAGE_UNITS"].ToString()));                                                    // 剂量
                    item.SubItems.Add(dr["PERFORM_SCHEDULE"].ToString());                                   // 执行时间
                    
                    string startDate = string.Empty;
                    if (dr["START_DATE_TIME"].ToString().Length > 0)
                    {
                        startDate = ComFunctionApp.GetDateFormat_Order((DateTime)dr["START_DATE_TIME"]);
                    }
                    
                    item.SubItems.Add(startDate);                                                           // 开始时间
                    item.SubItems.Add(dr["DOCTOR"].ToString());                                             // 医嘱执行次数
                    
                    item.SubItems.Add(dr["ORDER_NO"].ToString());
                    item.SubItems.Add(dr["ORDER_SUB_NO"].ToString());
                    
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
        /// 重新加载病人医嘱
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
                showOrders(new object(), new System.EventArgs());
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
            #if SCANNER
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                // 获取病人ID号
                string barcode = GVars.ScanReaderBuffer.Text.Trim();
                
                // 如果不包含空格, 表示是病人的腕带
                if (barcode.IndexOf( ComConst.STR.BLANK) < 0 && barcode.IndexOf("T") < 0)
                {
                    if (patNavigator.ScanedPatient(barcode) == false) GVars.Msg.Show("W00005");   // 该病人不存在!
                                        
                    return;
                }                
                // 如果扫描的是输液卡
                else
                {
                    scanObject(barcode);
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
                lblMsg.Text = GVars.Msg.GetMsg("I00007");               // 不属于当前的病人!
                lblMsg.ForeColor = Color.Red;
                panelMsg.Visible = true;
                return false;
            }
            
            // 如果是正确的病人
            lblMsg.Text = GVars.Msg.GetMsg("I00006");                   // 正确的病人!
            lblMsg.ForeColor = Color.Blue;
            panelMsg.Visible = true;
            
            // 定位医嘱
            string orderNo = string.Empty;
            string[] parts = barcode.Split("T".ToCharArray());
            if (parts.Length > 1)
            {
                orderNo = parts[1];
            }
            
            if (orderNo.Length > 0)
            {
                locateOrderExecute(orderNo);
            }        
            
            return true;
        }
        
        
        /// <summary>
        /// 定位医嘱执行单
        /// </summary>
        /// <param name="orderNo"></param>
        private bool locateOrderExecute(string orderNo)
        {
            int col_order_no = lvwOrders.Columns.Count - 2;
            
            for (int i = 0; i < lvwOrders.Items.Count; i++)
            {
                ListViewItem item = lvwOrders.Items[i];
                
                string orderNoTest = item.SubItems[col_order_no].Text;
                
                if (orderNoTest.Equals(orderNo) == true)
                {
                    item.Selected = true;
                    lvwOrders.EnsureVisible(i);
                    
                    return true;
                }
            }
            
            return false;
        }        
        #endregion
    }
}