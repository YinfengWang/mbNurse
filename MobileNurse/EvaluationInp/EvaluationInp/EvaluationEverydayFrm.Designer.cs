namespace HISPlus
{
    partial class EvaluationEverydayFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EvaluationEverydayFrm));
            this.grpSeach = new DevExpress.XtraEditors.PanelControl();
            this.btnDelete = new HISPlus.UserControls.UcButton();
            this.cmbTime = new HISPlus.UserControls.UcComboBox();
            this.btnExit = new HISPlus.UserControls.UcButton();
            this.btnMultiPrint = new HISPlus.UserControls.UcButton();
            this.btnPrint = new HISPlus.UserControls.UcButton();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.dtDay = new System.Windows.Forms.DateTimePicker();
            this.label3 = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery = new HISPlus.UserControls.UcButton();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.grpEvaluation = new DevExpress.XtraEditors.PanelControl();
            this.timer1 = new System.Windows.Forms.Timer();
            this.grpSeach.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSeach
            // 
            this.grpSeach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSeach.Controls.Add(this.btnDelete);
            this.grpSeach.Controls.Add(this.cmbTime);
            this.grpSeach.Controls.Add(this.btnExit);
            this.grpSeach.Controls.Add(this.btnMultiPrint);
            this.grpSeach.Controls.Add(this.btnPrint);
            this.grpSeach.Controls.Add(this.btnSave);
            this.grpSeach.Controls.Add(this.dtDay);
            this.grpSeach.Controls.Add(this.label3);
            this.grpSeach.Controls.Add(this.btnQuery);
            this.grpSeach.Location = new System.Drawing.Point(13, 700);
            this.grpSeach.Margin = new System.Windows.Forms.Padding(4);
            this.grpSeach.Name = "grpSeach";
            this.grpSeach.Padding = new System.Windows.Forms.Padding(4);
            this.grpSeach.Size = new System.Drawing.Size(1140, 84);
            this.grpSeach.TabIndex = 3;
            this.grpSeach.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(500, 27);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 45);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.TextValue = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cmbTime
            // 
            this.cmbTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTime.FormattingEnabled = true;
            this.cmbTime.Location = new System.Drawing.Point(207, 36);
            this.cmbTime.Margin = new System.Windows.Forms.Padding(4);
            this.cmbTime.Name = "cmbTime";
            this.cmbTime.Size = new System.Drawing.Size(93, 26);
            this.cmbTime.TabIndex = 5;
            this.cmbTime.SelectedIndexChanged += new System.EventHandler(this.cmbTime_SelectedIndexChanged);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(1012, 27);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 45);
            this.btnExit.TabIndex = 1;
            this.btnExit.TextValue = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnMultiPrint
            // 
            this.btnMultiPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMultiPrint.Location = new System.Drawing.Point(884, 26);
            this.btnMultiPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnMultiPrint.Name = "btnMultiPrint";
            this.btnMultiPrint.Size = new System.Drawing.Size(120, 45);
            this.btnMultiPrint.TabIndex = 8;
            this.btnMultiPrint.TextValue = "多日打印";
            this.btnMultiPrint.UseVisualStyleBackColor = true;
            this.btnMultiPrint.Visible = false;
            this.btnMultiPrint.Click += new System.EventHandler(this.multiPrint_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(756, 26);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(120, 45);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.TextValue = "单日打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(628, 27);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 45);
            this.btnSave.TabIndex = 7;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dtDay
            // 
            this.dtDay.Location = new System.Drawing.Point(56, 34);
            this.dtDay.Margin = new System.Windows.Forms.Padding(4);
            this.dtDay.Name = "dtDay";
            this.dtDay.Size = new System.Drawing.Size(141, 26);
            this.dtDay.TabIndex = 4;
            this.dtDay.ValueChanged += new System.EventHandler(this.dtDay_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 40);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "日期:";
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuery.Location = new System.Drawing.Point(357, 26);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(120, 45);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.TextValue = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Visible = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            //this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.vScrollBar1);
            this.panel1.Controls.Add(this.grpEvaluation);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1139, 672);
            this.panel1.TabIndex = 2;            
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar1.Location = new System.Drawing.Point(1109, 14);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 650);
            this.vScrollBar1.TabIndex = 1;
            this.vScrollBar1.ValueChanged += new System.EventHandler(this.vScrollBar1_ValueChanged);
            // 
            // grpEvaluation
            // 
            this.grpEvaluation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpEvaluation.Location = new System.Drawing.Point(4, 4);
            this.grpEvaluation.Margin = new System.Windows.Forms.Padding(4);
            this.grpEvaluation.Name = "grpEvaluation";
            this.grpEvaluation.Padding = new System.Windows.Forms.Padding(4);
            this.grpEvaluation.Size = new System.Drawing.Size(1100, 506);
            this.grpEvaluation.TabIndex = 0;
            this.grpEvaluation.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // EvaluationEverydayFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 800);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grpSeach);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "EvaluationEverydayFrm";
            this.Text = "每日评估单";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EvaluationEverydayFrm_FormClosing);
            this.Load += new System.EventHandler(this.EvaluationEverydayFrm_Load);
            this.grpSeach.ResumeLayout(false);
            this.grpSeach.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl grpSeach;
        private DevExpress.XtraEditors.LabelControl label3;
        private System.Windows.Forms.DateTimePicker dtDay;
        private HISPlus.UserControls.UcButton btnQuery;
        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.PanelControl grpEvaluation;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.Timer timer1;
        private HISPlus.UserControls.UcButton btnPrint;
        private HISPlus.UserControls.UcButton btnSave;
        private HISPlus.UserControls.UcButton btnMultiPrint;
        private HISPlus.UserControls.UcButton btnExit;
        private HISPlus.UserControls.UcComboBox cmbTime;
        private HISPlus.UserControls.UcButton btnDelete;
    }
}