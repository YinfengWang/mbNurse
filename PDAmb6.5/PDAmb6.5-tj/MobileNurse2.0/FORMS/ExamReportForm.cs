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
        public DataRow DrShow = null;                                   // 要显示的数据

        public ExamReportForm()
        {
            InitializeComponent();
            
            this.Click += new EventHandler(Close);
        }
        
        
        /// <summary>
        /// 窗体加载事件
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
        /// 初始化显示
        /// </summary>
        private void initDisp()
        {
            // 检查类别
            lblExamClass.Text      = DrShow["EXAM_CLASS"].ToString();

            // 检查子类
            lblExamSubClass.Text   = DrShow["EXAM_SUB_CLASS"].ToString();

            // 检查状态
            lblExamStatus.Text     = DrShow["EXAM_RESULT_STATUS_NAME"].ToString();

            // 申请人
            lblProposer.Text      += DrShow["REQ_PHYSICIAN"].ToString();

            // 申请日期
            if (DrShow["REQ_DATE_TIME"].ToString().Length > 0)
            {
                lblRequestDate.Text += DateTime.Parse(DrShow["REQ_DATE_TIME"].ToString()).ToString(ComConst.FMT_DATE.LONG);
            }
            else
            {
                lblRequestDate.Text += string.Empty;
            }

            // 报告者
            lblReporter.Text      += DrShow["REPORTER"].ToString();

            // 报告日期
            if (DrShow["REPORT_DATE_TIME"].ToString().Length > 0)
            {
                lblReportDate.Text += DateTime.Parse(DrShow["REPORT_DATE_TIME"].ToString()).ToString(ComConst.FMT_DATE.LONG);
            }
            else
            {
                lblReportDate.Text += string.Empty;
            }

            // 报告内容
            this.lblIndictor.Visible = (ComConst.VAL.YES.Equals(DrShow["IS_ABNORMAL"].ToString()) == true);         // 异常指示

            this.txtReport.Text = string.Empty;
            if (DrShow["EXAM_PARA"].ToString().Length > 0)
            {
                this.txtReport.Text += "[检查参数]" + ComConst.STR.CRLF
                    + DrShow["EXAM_PARA"].ToString() + ComConst.STR.CRLF;
            }
            
            if (DrShow["DESCRIPTION"].ToString().Length > 0)
            {
                this.txtReport.Text += ComConst.STR.CRLF + "[检查所见]" + ComConst.STR.CRLF
                    + DrShow["DESCRIPTION"].ToString() + ComConst.STR.CRLF;
            }

            if (DrShow["IMPRESSION"].ToString().Length > 0)
            {
                this.txtReport.Text += ComConst.STR.CRLF + "[印象]" + ComConst.STR.CRLF
                    + DrShow["IMPRESSION"].ToString() + ComConst.STR.CRLF;
            }
            
            if (DrShow["RECOMMENDATION"].ToString().Length > 0)
            {
                this.txtReport.Text += ComConst.STR.CRLF + "[建议]" + ComConst.STR.CRLF
                    + DrShow["RECOMMENDATION"].ToString() + ComConst.STR.CRLF;
            }
            
            this.txtReport.Text = txtReport.Text.TrimEnd();
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