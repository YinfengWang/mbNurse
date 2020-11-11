namespace HISPlus
{
    partial class ExamReportForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.mnuCancel = new System.Windows.Forms.MenuItem();
            this.mnuPatient = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblExamSubClass = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblExamStatus = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtReport = new System.Windows.Forms.TextBox();
            this.lblProposer = new System.Windows.Forms.Label();
            this.lblReporter = new System.Windows.Forms.Label();
            this.lblReportDate = new System.Windows.Forms.Label();
            this.lblRequestDate = new System.Windows.Forms.Label();
            this.lblExamClass = new System.Windows.Forms.Label();
            this.lblIndictor = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuCancel);
            this.mainMenu1.MenuItems.Add(this.mnuPatient);
            // 
            // mnuCancel
            // 
            this.mnuCancel.Text = "返回";
            // 
            // mnuPatient
            // 
            this.mnuPatient.Text = "当前病人";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 20);
            this.label1.Text = "类别:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(120, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 20);
            this.label2.Text = "子类:";
            // 
            // lblExamSubClass
            // 
            this.lblExamSubClass.Location = new System.Drawing.Point(154, 8);
            this.lblExamSubClass.Name = "lblExamSubClass";
            this.lblExamSubClass.Size = new System.Drawing.Size(83, 20);
            this.lblExamSubClass.Text = "检查子类:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 20);
            this.label5.Text = "状态:";
            // 
            // lblExamStatus
            // 
            this.lblExamStatus.Location = new System.Drawing.Point(46, 29);
            this.lblExamStatus.Name = "lblExamStatus";
            this.lblExamStatus.Size = new System.Drawing.Size(68, 20);
            this.lblExamStatus.Text = "检查状态:";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 20);
            this.label7.Text = "检查报告:";
            // 
            // txtReport
            // 
            this.txtReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtReport.Location = new System.Drawing.Point(3, 91);
            this.txtReport.Multiline = true;
            this.txtReport.Name = "txtReport";
            this.txtReport.ReadOnly = true;
            this.txtReport.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReport.Size = new System.Drawing.Size(234, 149);
            this.txtReport.TabIndex = 13;
            // 
            // lblProposer
            // 
            this.lblProposer.Location = new System.Drawing.Point(3, 51);
            this.lblProposer.Name = "lblProposer";
            this.lblProposer.Size = new System.Drawing.Size(111, 20);
            this.lblProposer.Text = "申请人:";
            // 
            // lblReporter
            // 
            this.lblReporter.Location = new System.Drawing.Point(3, 245);
            this.lblReporter.Name = "lblReporter";
            this.lblReporter.Size = new System.Drawing.Size(111, 20);
            this.lblReporter.Text = "报告人:";
            // 
            // lblReportDate
            // 
            this.lblReportDate.Location = new System.Drawing.Point(126, 245);
            this.lblReportDate.Name = "lblReportDate";
            this.lblReportDate.Size = new System.Drawing.Size(111, 20);
            this.lblReportDate.Text = "日期:";
            // 
            // lblRequestDate
            // 
            this.lblRequestDate.Location = new System.Drawing.Point(120, 51);
            this.lblRequestDate.Name = "lblRequestDate";
            this.lblRequestDate.Size = new System.Drawing.Size(111, 20);
            this.lblRequestDate.Text = "日期:";
            // 
            // lblExamClass
            // 
            this.lblExamClass.Location = new System.Drawing.Point(46, 8);
            this.lblExamClass.Name = "lblExamClass";
            this.lblExamClass.Size = new System.Drawing.Size(68, 20);
            this.lblExamClass.Text = "检查子类:";
            // 
            // lblIndictor
            // 
            this.lblIndictor.ForeColor = System.Drawing.Color.Red;
            this.lblIndictor.Location = new System.Drawing.Point(65, 75);
            this.lblIndictor.Name = "lblIndictor";
            this.lblIndictor.Size = new System.Drawing.Size(22, 20);
            this.lblIndictor.Text = "★";
            this.lblIndictor.Visible = false;
            // 
            // ExamReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.lblRequestDate);
            this.Controls.Add(this.lblReportDate);
            this.Controls.Add(this.lblReporter);
            this.Controls.Add(this.lblProposer);
            this.Controls.Add(this.txtReport);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblExamStatus);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblExamSubClass);
            this.Controls.Add(this.lblExamClass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblIndictor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "ExamReportForm";
            this.Text = "检查信息";
            this.Load += new System.EventHandler(this.ExamReportForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblExamSubClass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblExamStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtReport;
        private System.Windows.Forms.Label lblProposer;
        private System.Windows.Forms.Label lblReporter;
        private System.Windows.Forms.Label lblReportDate;
        private System.Windows.Forms.Label lblRequestDate;
        private System.Windows.Forms.Label lblExamClass;
        private System.Windows.Forms.Label lblIndictor;
        private System.Windows.Forms.MenuItem mnuCancel;
        private System.Windows.Forms.MenuItem mnuPatient;
    }
}