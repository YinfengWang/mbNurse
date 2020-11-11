namespace HISPlus
{
    partial class EvaluationResultViewFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EvaluationResultViewFrm));
            this.grpPatientEdu = new DevExpress.XtraEditors.GroupControl();
            this.txtContent = new HISPlus.UserControls.UcTextArea();
            this.groupBox2 = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new HISPlus.UserControls.UcButton();
            this.dtRngEnd = new System.Windows.Forms.DateTimePicker();
            this.label13 = new DevExpress.XtraEditors.LabelControl();
            this.dtRngStart = new System.Windows.Forms.DateTimePicker();
            this.label11 = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery = new HISPlus.UserControls.UcButton();
            this.btnPrint = new HISPlus.UserControls.UcButton();
            this.grpPatientEdu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPatientEdu
            // 
            this.grpPatientEdu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPatientEdu.Controls.Add(this.txtContent);
            this.grpPatientEdu.Location = new System.Drawing.Point(13, 13);
            this.grpPatientEdu.Margin = new System.Windows.Forms.Padding(4);
            this.grpPatientEdu.Name = "grpPatientEdu";
            this.grpPatientEdu.Padding = new System.Windows.Forms.Padding(4);
            this.grpPatientEdu.Size = new System.Drawing.Size(1030, 657);
            this.grpPatientEdu.TabIndex = 1;
            this.grpPatientEdu.TabStop = false;
            this.grpPatientEdu.Text = "评估内容";
            // 
            // txtContent
            // 
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.Location = new System.Drawing.Point(4, 23);
            this.txtContent.Margin = new System.Windows.Forms.Padding(4);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.ReadOnly = true;
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContent.Size = new System.Drawing.Size(1022, 630);
            this.txtContent.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnExit);
            this.groupBox2.Controls.Add(this.dtRngEnd);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.dtRngStart);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.btnQuery);
            this.groupBox2.Controls.Add(this.btnPrint);
            this.groupBox2.Location = new System.Drawing.Point(271, 680);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(772, 74);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(647, 20);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 45);
            this.btnExit.TabIndex = 6;
            this.btnExit.TextValue = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dtRngEnd
            // 
            this.dtRngEnd.Location = new System.Drawing.Point(219, 27);
            this.dtRngEnd.Margin = new System.Windows.Forms.Padding(4);
            this.dtRngEnd.Name = "dtRngEnd";
            this.dtRngEnd.Size = new System.Drawing.Size(147, 26);
            this.dtRngEnd.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(203, 33);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(13, 18);
            this.label13.TabIndex = 2;
            this.label13.Text = "-";
            // 
            // dtRngStart
            // 
            this.dtRngStart.Location = new System.Drawing.Point(49, 27);
            this.dtRngStart.Margin = new System.Windows.Forms.Padding(4);
            this.dtRngStart.Name = "dtRngStart";
            this.dtRngStart.Size = new System.Drawing.Size(147, 26);
            this.dtRngStart.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 33);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 18);
            this.label11.TabIndex = 0;
            this.label11.Text = "日期";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(391, 20);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(120, 45);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.TextValue = "查询(&Q)";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(519, 20);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(120, 45);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.TextValue = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // EvaluationResultViewFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 771);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpPatientEdu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "EvaluationResultViewFrm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "评估结果查看";
            this.Load += new System.EventHandler(this.EvaluationResultViewFrmec_Load);
            this.grpPatientEdu.ResumeLayout(false);
            this.grpPatientEdu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpPatientEdu;
        private DevExpress.XtraEditors.PanelControl groupBox2;
        private HISPlus.UserControls.UcButton btnQuery;
        private System.Windows.Forms.DateTimePicker dtRngEnd;
        private DevExpress.XtraEditors.LabelControl label13;
        private System.Windows.Forms.DateTimePicker dtRngStart;
        private DevExpress.XtraEditors.LabelControl label11;
        private HISPlus.UserControls.UcButton btnPrint;
        private HISPlus.UserControls.UcTextArea txtContent;
        private HISPlus.UserControls.UcButton btnExit;
    }
}