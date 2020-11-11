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
            // 检验项目
            lblLabItem.Text = DrShow["ITEM_NAME"].ToString();

            // 检验目的
            lblTestCause.Text = DrShow["TEST_CAUSE"].ToString();

            // 标本
            lblSpecimen.Text  = DrShow["SPECIMEN"].ToString();

            // 标本采集日期时间
            string val = DrShow["SPCM_RECEIVED_DATE_TIME"].ToString();
            if (val.Length > 0)
            {
                lblSpcmReceivedDate.Text = DateTime.Parse(val).ToString(ComConst.FMT_DATE.LONG);
            }
            else
            {
                lblSpcmReceivedDate.Text = string.Empty;
            }

            // 临床诊断
            lblClinicDiag.Text = DrShow["RELEVANT_CLINIC_DIAG"].ToString();     
           
            // 标本说明
            lblNotes.Text = DrShow["NOTES_FOR_SPCM"].ToString();

            // 申请日期及时间
            val = DrShow["REQUESTED_DATE_TIME"].ToString();
            if (val.Length > 0)
            {
                lblRequestDateTime.Text = DateTime.Parse(val).ToString(ComConst.FMT_DATE.LONG);
            }
            else
            {
                lblRequestDateTime.Text = string.Empty;
            }
            
            // 申请医生
            lblOrderProvider.Text = DrShow["ORDERING_PROVIDER"].ToString();

            // 报告日期及时间
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
        /// 窗体单击时退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Close(object sender, EventArgs e)
        {
            this.Close();
        }                
    }
}