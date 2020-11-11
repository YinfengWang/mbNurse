using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class ExamReportForm : Form
    {
        public DataRow DrShow = null;                                   // Ҫ��ʾ������

        public ExamReportForm()
        {
            InitializeComponent();
            
            this.Click += new EventHandler(Close);
        }
        
        
        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExamReportForm_Load(object sender, EventArgs e)
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
        
        
        /// <summary>
        /// ��ʼ����ʾ
        /// </summary>
        private void initDisp()
        {
            // ������
            lblExamClass.Text      = DrShow["EXAM_CLASS"].ToString();

            // �������
            lblExamSubClass.Text   = DrShow["EXAM_SUB_CLASS"].ToString();

            // ���״̬
            lblExamStatus.Text     = DrShow["EXAM_RESULT_STATUS_NAME"].ToString();

            // ������
            lblProposer.Text      += DrShow["REQ_PHYSICIAN"].ToString();

            // ��������
            if (DrShow["REQ_DATE_TIME"].ToString().Length > 0)
            {
                lblRequestDate.Text += DateTime.Parse(DrShow["REQ_DATE_TIME"].ToString()).ToString(ComConst.FMT_DATE.LONG);
            }
            else
            {
                lblRequestDate.Text += string.Empty;
            }

            // ������
            lblReporter.Text      += DrShow["REPORTER"].ToString();

            // ��������
            if (DrShow["REPORT_DATE_TIME"].ToString().Length > 0)
            {
                lblReportDate.Text += DateTime.Parse(DrShow["REPORT_DATE_TIME"].ToString()).ToString(ComConst.FMT_DATE.LONG);
            }
            else
            {
                lblReportDate.Text += string.Empty;
            }

            // ��������
            this.lblIndictor.Visible = (ComConst.VAL.YES.Equals(DrShow["IS_ABNORMAL"].ToString()) == true);         // �쳣ָʾ

            this.txtReport.Text = string.Empty;
            if (DrShow["EXAM_PARA"].ToString().Length > 0)
            {
                this.txtReport.Text += "[������]" + ComConst.STR.CRLF
                    + DrShow["EXAM_PARA"].ToString() + ComConst.STR.CRLF;
            }
            
            if (DrShow["DESCRIPTION"].ToString().Length > 0)
            {
                this.txtReport.Text += ComConst.STR.CRLF + "[�������]" + ComConst.STR.CRLF
                    + DrShow["DESCRIPTION"].ToString() + ComConst.STR.CRLF;
            }

            if (DrShow["IMPRESSION"].ToString().Length > 0)
            {
                this.txtReport.Text += ComConst.STR.CRLF + "[ӡ��]" + ComConst.STR.CRLF
                    + DrShow["IMPRESSION"].ToString() + ComConst.STR.CRLF;
            }
            
            if (DrShow["RECOMMENDATION"].ToString().Length > 0)
            {
                this.txtReport.Text += ComConst.STR.CRLF + "[����]" + ComConst.STR.CRLF
                    + DrShow["RECOMMENDATION"].ToString() + ComConst.STR.CRLF;
            }
            
            this.txtReport.Text = txtReport.Text.TrimEnd();
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