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

namespace HISPlus
{
    public partial class frmOrderConvertFailed : FormDo
    {
        
        #region ����
        private OrderSplitter   orderConvertor      = new OrderSplitter();      // ҽ�������
        private Thread          threadSplitOrder;                               // �������ݵ��߳�

        private DataSet         dsErr               = null;




        
        #endregion        

        public frmOrderConvertFailed()
        {
            _id     = "00017";
            _guid   = "3F226D3E-B7BE-4952-9991-ADDA0FA6A96F";
            
            InitializeComponent();
        }
        
        
        #region �����¼�
        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmOrderConvertFailed_Load( object sender, EventArgs e )
        {
            try
            {
                dgvResult.AutoGenerateColumns = false;
                
                threadSplitOrder = new Thread(new ThreadStart(orderConvertor.SplitOrders));
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
        
        
        void frmOrderConvertFailed_FormClosing( object sender, FormClosingEventArgs e )
        {
            //this.Hide();
            //e.Cancel = true;
        }
        
        
        /// <summary>
        /// ����ر��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOrderConvertFailed_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                orderConvertor.Exit();
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
        /// ��ť[�ر�]
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
        /// ��ť[ҽ�����]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click( object sender, EventArgs e )
        {
            try
            {
                showSplitFailedOrder();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        /// <summary>
        /// �����־ˢ��ʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                timer2.Enabled = false;

                txtTrace.Text = orderConvertor.TraceInfo;       // �仯��־
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
        
        
        #region ��ͨ����
        /// <summary>
        /// ��ʾ���ʧ�ܵ�ҽ��
        /// </summary>
        private void showSplitFailedOrder()
        {
            try
            {
                // ��ȡ���ʧ�ܵ�ҽ��
                
              string   sql = "SELECT PATIENT_INFO.NAME, "
                        +    "PATIENT_INFO.BED_NO, "
                        +    "ORDERS_M.* "
                        + "FROM ORDERS_M, "
                        +    "PATIENT_INFO "
                        + "WHERE SPLIT_MEMO IS NOT NULL "
                        +       "AND ORDERS_M.PATIENT_ID = PATIENT_INFO.PATIENT_ID "
                        +       "AND ORDERS_M.VISIT_ID = PATIENT_INFO.VISIT_ID "
                          + "AND ORDERS_M.ORDER_CLASS ='A'";
               
               // HS   �����ʾ ���ṩ��ѯ  һ�������ڵĴ��� ҽ�� 

                HISPlus.OrderSplitter.SyncInfo syncInfo = OrderSplitter.syncInfos[0];
                 sql += " AND ORDERS_M.WARD_CODE in (" + syncInfo.Filter + ")";
                
                dsErr = GVars.OracleMobile.SelectData(sql);
                
                // ��ʾ���ʧ��ҽ��
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