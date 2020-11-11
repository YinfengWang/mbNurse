using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using SqlConvert = HISPlus.SqlManager;
using System.IO;
using OrderSplitHelper;

namespace HISPlus
{
    public partial class frmOrderConvertFailed : FormDo
    {
        
        #region 变量
        private OrderSplitter   orderSplitter      = new OrderSplitter();      // 医嘱拆分器

        private Thread          threadSplitOrder;                               // 加载数据的线程

        private DataSet         dsErr               = null;

        
        #endregion        

        public frmOrderConvertFailed()
        {
            _id     = "00017";
            _guid   = "3F226D3E-B7BE-4952-9991-ADDA0FA6A96F";
            
            InitializeComponent();
        }

        public frmOrderConvertFailed(string path)
        {
            _id = "00017";
            _guid = "3F226D3E-B7BE-4952-9991-ADDA0FA6A96F";

            InitializeComponent();

            orderSplitter.IniPath = path;
        }
        
        
        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmOrderConvertFailed_Load( object sender, EventArgs e )
        {
            try
            {
                dgvResult.AutoGenerateColumns = false;

                threadSplitOrder = new Thread(new ThreadStart(orderSplitter.SplitOrders));
                threadSplitOrder.IsBackground = true;
                threadSplitOrder.Priority = ThreadPriority.BelowNormal;
                threadSplitOrder.Name = "SplitOrders";
                threadSplitOrder.Start();
                timer2.Enabled = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOrderConvertFailed_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                orderSplitter.Exit();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        private void dgvResult_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dgvResult.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }
        
        
        private void dgvResult_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
                
        
        /// <summary>
        /// 按钮[关闭]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click( object sender, EventArgs e )
        {
            try
            {
                this.Hide();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[医嘱拆分]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click( object sender, EventArgs e )
        {
            try
            {
                showSplitFailedOrder(orderSplitter.syncInfos[0].Filter);
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        /// 拆分日志刷新时钟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;

                txtTrace.Clear();
                txtTrace.Text = orderSplitter.TraceInfo;       // 变化日志
                //CommFunc.LogClass.WriteLog(txtTrace.Text);  //2015.11.23 写日志
            }
            catch
            {
            }
            finally
            {
                timer2.Enabled = true;
            }
        }
        #endregion
        
        
        #region 共通函数
        /// <summary>
        /// 显示拆分失败的医嘱
        /// </summary>
        /// <param name="wardCodeStr">病区代码字符串，配置文件中形如 filter:'1001N','100102N','1009N'</param>
        private void showSplitFailedOrder(string wardCodeStr)
        {
            try
            {
                // 获取拆分失败的医嘱

                string sql = "SELECT PATIENT_INFO.NAME, "
                          + "PATIENT_INFO.BED_NO, "
                          + "ORDERS_M.* "
                          + "FROM ORDERS_M, "
                          + "PATIENT_INFO "
                          + "WHERE SPLIT_MEMO IS NOT NULL "
                          + "AND ORDERS_M.PATIENT_ID = PATIENT_INFO.PATIENT_ID "
                          + "AND ORDERS_M.VISIT_ID = PATIENT_INFO.VISIT_ID ";
                         //+ "AND ORDERS_M.ORDER_CLASS ='A'";

                // HS   添加修改 显示  当前配置科室 错误医嘱
                  sql += " AND ORDERS_M.WARD_CODE in (" + wardCodeStr + ")";
                  
                dsErr = GVars.OracleMobile.SelectData(sql);
                
                // 显示拆分失败医嘱
                dsErr.Tables[0].DefaultView.Sort = "WARD_CODE, PATIENT_ID, VISIT_ID, ORDER_NO, ORDER_SUB_NO";
                dgvResult.DataSource = dsErr.Tables[0].DefaultView;                
            }
            finally
            {
            }    
        }
        #endregion
    }
}