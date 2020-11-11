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
        #region ����
        public DataRow  DrOrder         = null;
        public bool     ShowOrderDetail = true;     // �Ƿ���ʾҽ����ϸ��Ϣ, ���ʾ��ҽ��ִ�е���ϸ��Ϣ
        #endregion
        
        
        public OrderDetailForm()
        {
            InitializeComponent();

            this.Click += new EventHandler(Close);
        }

        
        /// <summary>
        /// ��������¼�
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
        /// ���嵥��ʱ�˳�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// ��ʼ��������ʾ
        /// </summary>
        private void initDisp()
        {
            if (DrOrder["ORDER_SUB_NO"].ToString().Equals("1") == true)
            {
                this.txtOrderType.Text = "��ҽ��";                                              // ҽ������
            }
            else
            {
                this.txtOrderType.Text = "��ҽ��";                                              // ҽ������
            }
            
            this.txtOrderText.Text = DrOrder["ORDER_TEXT"].ToString();                          // ҽ������
            this.txtDosage.Text = DrOrder["DOSAGE"].ToString() + DrOrder["DOSAGE_UNITS"].ToString(); // ����
            this.txtAdministration.Text = DrOrder["ADMINISTRATION"].ToString();                 // ;��
            
            if (ShowOrderDetail == true)
            {
                this.txtSchedule.Text = DrOrder["PERFORM_SCHEDULE"].ToString();                 // �ƻ�
            }
            else
            { 
                this.txtSchedule.Text = DrOrder["SCHEDULE_PERFORM_TIME"].ToString();            // �ƻ�
            }
            
            this.txtFrequency.Text = DrOrder["FREQUENCY"].ToString();                           // Ƶ��

            if (ShowOrderDetail == true)
            {
                this.txtStartDateTime.Text = DrOrder["START_DATE_TIME"].ToString();             // ��ʼʱ��
                this.txtStopDateTime.Text = DrOrder["STOP_DATE_TIME"].ToString();               // ֹͣʱ��
            }
            else
            { 
                this.txtStartDateTime.Visible = false;                                          // ��ʼʱ��
                this.txtStopDateTime.Visible = false;;                                          // ֹͣʱ��
            }
            
            if (ShowOrderDetail == true)
            {
                this.txtDoctor.Text = DrOrder["DOCTOR"].ToString();                             // ҽ��
            }
            else
            {
                this.txtDoctor.Text = string.Empty;                                             // ҽ��
            }

            this.txtOrderNo.Text    = DrOrder["ORDER_NO"].ToString();                           // ҽ����
        }

    }
}