using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class LabReportForm : Form
    {
        public DataRow DrShow = null;

        public LabReportForm()
        {
            InitializeComponent();
            
            this.Click += new EventHandler(Close);
        }


        private void LabReportForm_Load(object sender, EventArgs e)
        {
            try
            {
                initDisp();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void initDisp()
        { 
            // ������Ŀ
            lblLabItem.Text = DrShow["ITEM_NAME"].ToString();

            // ����Ŀ��
            lblTestCause.Text = DrShow["TEST_CAUSE"].ToString();

            // �걾
            lblSpecimen.Text  = DrShow["SPECIMEN"].ToString();

            // �걾�ɼ�����ʱ��
            string val = DrShow["SPCM_RECEIVED_DATE_TIME"].ToString();
            if (val.Length > 0)
            {
                lblSpcmReceivedDate.Text = DateTime.Parse(val).ToString(ComConst.FMT_DATE.LONG);
            }
            else
            {
                lblSpcmReceivedDate.Text = string.Empty;
            }

            // �ٴ����
            lblClinicDiag.Text = DrShow["RELEVANT_CLINIC_DIAG"].ToString();     
           
            // �걾˵��
            lblNotes.Text = DrShow["NOTES_FOR_SPCM"].ToString();

            // �������ڼ�ʱ��
            val = DrShow["REQUESTED_DATE_TIME"].ToString();
            if (val.Length > 0)
            {
                lblRequestDateTime.Text = DateTime.Parse(val).ToString(ComConst.FMT_DATE.LONG);
            }
            else
            {
                lblRequestDateTime.Text = string.Empty;
            }
            
            // ����ҽ��
            lblOrderProvider.Text = DrShow["ORDERING_PROVIDER"].ToString();

            // �������ڼ�ʱ��
            val = DrShow["RESULTS_RPT_DATE_TIME"].ToString();
            if (val.Length > 0)
            {
                lblReportDateTime.Text = DateTime.Parse(val).ToString(ComConst.FMT_DATE.LONG);
            }
            else
            {
                lblReportDateTime.Text = string.Empty;
            }
        }
        
        
        /// <summary>
        /// ���嵥��ʱ�˳�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Close(object sender, EventArgs e)
        {
            this.Close();
        }                
    }
}