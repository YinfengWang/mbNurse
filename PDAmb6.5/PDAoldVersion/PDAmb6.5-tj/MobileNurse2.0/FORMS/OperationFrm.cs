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
    public partial class OperationFrm : Form
    {
        #region 变量
        private PatientNavigator patNavigator = new PatientNavigator(); // 病人导航
        #endregion
        
        
        public OperationFrm()
        {
            InitializeComponent();
        }

        private void OperationFrm_Load(object sender, EventArgs e)
        {
            try
            {
                patNavigator.BtnPrePatient = this.btnPrePatient;
                patNavigator.BtnCurrentPatient = this.btnCurrPatient;
                patNavigator.BtnNextPatient = this.btnNextPatient;
                patNavigator.BtnPatientList = this.btnListPatient;
                
                patNavigator.MenuItemPatient = mnuPatient;
                
                // patNavigator.PatientChanged = new DataChanged(reloadOrders);
                
                // this.timer1.Enabled = true;
                
                // 定位病人
                patNavigator.SetPatientButtons();

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
            catch(Exception ex)
            {
                Error.ErrProc(ex);
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
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
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