using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class OrderDetailForm : Form
    {
        #region 变量
        public DataRow  DrOrder         = null;
        public bool     ShowOrderDetail = true;     // 是否显示医嘱详细信息, 否表示是医嘱执行单详细信息
        #endregion
        
        
        public OrderDetailForm()
        {
            InitializeComponent();

            this.Click += new EventHandler(Close);
        }

        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderDetailForm_Load(object sender, EventArgs e)
        {
            try
            {
                initDisp();
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
        /// 窗体单击时退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {
            if (DrOrder["ORDER_SUB_NO"].ToString().Equals("1") == true)
            {
                this.txtOrderType.Text = "主医嘱";                                              // 医嘱类型
            }
            else
            {
                this.txtOrderType.Text = "子医嘱";                                              // 医嘱类型
            }
            
            this.txtOrderText.Text = DrOrder["ORDER_TEXT"].ToString();                          // 医嘱内容
            this.txtDosage.Text = DrOrder["DOSAGE"].ToString() + DrOrder["DOSAGE_UNITS"].ToString(); // 剂量
            this.txtAdministration.Text = DrOrder["ADMINISTRATION"].ToString();                 // 途径
            
            if (ShowOrderDetail == true)
            {
                this.txtSchedule.Text = DrOrder["PERFORM_SCHEDULE"].ToString();                 // 计划
            }
            else
            { 
                this.txtSchedule.Text = DrOrder["SCHEDULE_PERFORM_TIME"].ToString();            // 计划
            }
            
            this.txtFrequency.Text = DrOrder["FREQUENCY"].ToString();                           // 频次

            if (ShowOrderDetail == true)
            {
                this.txtStartDateTime.Text = DrOrder["START_DATE_TIME"].ToString();             // 开始时间
                this.txtStopDateTime.Text = DrOrder["STOP_DATE_TIME"].ToString();               // 停止时间
            }
            else
            { 
                this.txtStartDateTime.Visible = false;                                          // 开始时间
                this.txtStopDateTime.Visible = false;;                                          // 停止时间
            }
            
            if (ShowOrderDetail == true)
            {
                this.txtDoctor.Text = DrOrder["DOCTOR"].ToString();                             // 医生
            }
            else
            {
                this.txtDoctor.Text = string.Empty;                                             // 医生
            }

            this.txtOrderNo.Text    = DrOrder["ORDER_NO"].ToString();                           // 医嘱号
        }

    }
}