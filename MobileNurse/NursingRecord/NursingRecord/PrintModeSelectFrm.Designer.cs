namespace HISPlus
{
    partial class PrintModeSelectFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintModeSelectFrm));
            this.groupBox1 = new DevExpress.XtraEditors.PanelControl();
            this.numPageEnd = new System.Windows.Forms.NumericUpDown();
            this.numPageStart = new System.Windows.Forms.NumericUpDown();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new DevExpress.XtraEditors.PanelControl();
            this.rdoReprintAll = new System.Windows.Forms.RadioButton();
            this.numFirstPage = new System.Windows.Forms.NumericUpDown();
            this.dtFirstDate = new System.Windows.Forms.DateTimePicker();
            this.rdoStartDate = new System.Windows.Forms.RadioButton();
            this.dtFirstTime = new System.Windows.Forms.DateTimePicker();
            this.rdoStartPage = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new DevExpress.XtraEditors.PanelControl();
            this.btnOk = new HISPlus.UserControls.UcButton();
            this.btnCancel = new HISPlus.UserControls.UcButton();
            this.rdoSamePrint = new System.Windows.Forms.RadioButton();
            this.rdoRePrint = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPageEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPageStart)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFirstPage)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numPageEnd);
            this.groupBox1.Controls.Add(this.numPageStart);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(334, 63);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "原样重打";
            // 
            // numPageEnd
            // 
            this.numPageEnd.Location = new System.Drawing.Point(203, 27);
            this.numPageEnd.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numPageEnd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPageEnd.Name = "numPageEnd";
            this.numPageEnd.Size = new System.Drawing.Size(55, 21);
            this.numPageEnd.TabIndex = 3;
            this.numPageEnd.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numPageStart
            // 
            this.numPageStart.Location = new System.Drawing.Point(125, 27);
            this.numPageStart.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numPageStart.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPageStart.Name = "numPageStart";
            this.numPageStart.Size = new System.Drawing.Size(55, 21);
            this.numPageStart.TabIndex = 1;
            this.numPageStart.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(186, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "-";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "页码范围";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rdoReprintAll);
            this.groupBox2.Controls.Add(this.numFirstPage);
            this.groupBox2.Controls.Add(this.dtFirstDate);
            this.groupBox2.Controls.Add(this.rdoStartDate);
            this.groupBox2.Controls.Add(this.dtFirstTime);
            this.groupBox2.Controls.Add(this.rdoStartPage);
            this.groupBox2.Location = new System.Drawing.Point(12, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(334, 111);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "重新打印";
            // 
            // rdoReprintAll
            // 
            this.rdoReprintAll.AutoSize = true;
            this.rdoReprintAll.Checked = true;
            this.rdoReprintAll.Location = new System.Drawing.Point(51, 85);
            this.rdoReprintAll.Name = "rdoReprintAll";
            this.rdoReprintAll.Size = new System.Drawing.Size(95, 16);
            this.rdoReprintAll.TabIndex = 5;
            this.rdoReprintAll.TabStop = true;
            this.rdoReprintAll.Text = "全部重新打印";
            this.rdoReprintAll.UseVisualStyleBackColor = true;
            // 
            // numFirstPage
            // 
            this.numFirstPage.Location = new System.Drawing.Point(125, 20);
            this.numFirstPage.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numFirstPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFirstPage.Name = "numFirstPage";
            this.numFirstPage.Size = new System.Drawing.Size(55, 21);
            this.numFirstPage.TabIndex = 1;
            this.numFirstPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFirstPage.Visible = false;
            // 
            // dtFirstDate
            // 
            this.dtFirstDate.Location = new System.Drawing.Point(125, 53);
            this.dtFirstDate.Name = "dtFirstDate";
            this.dtFirstDate.Size = new System.Drawing.Size(116, 21);
            this.dtFirstDate.TabIndex = 3;
            this.dtFirstDate.Visible = false;
            // 
            // rdoStartDate
            // 
            this.rdoStartDate.AutoSize = true;
            this.rdoStartDate.Location = new System.Drawing.Point(51, 55);
            this.rdoStartDate.Name = "rdoStartDate";
            this.rdoStartDate.Size = new System.Drawing.Size(71, 16);
            this.rdoStartDate.TabIndex = 2;
            this.rdoStartDate.Text = "开始日期";
            this.rdoStartDate.UseVisualStyleBackColor = true;
            this.rdoStartDate.Visible = false;
            this.rdoStartDate.CheckedChanged += new System.EventHandler(this.printMode_CheckedChanged);
            // 
            // dtFirstTime
            // 
            this.dtFirstTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtFirstTime.Location = new System.Drawing.Point(247, 53);
            this.dtFirstTime.Name = "dtFirstTime";
            this.dtFirstTime.ShowUpDown = true;
            this.dtFirstTime.Size = new System.Drawing.Size(74, 21);
            this.dtFirstTime.TabIndex = 4;
            this.dtFirstTime.Visible = false;
            // 
            // rdoStartPage
            // 
            this.rdoStartPage.AutoSize = true;
            this.rdoStartPage.Location = new System.Drawing.Point(51, 25);
            this.rdoStartPage.Name = "rdoStartPage";
            this.rdoStartPage.Size = new System.Drawing.Size(71, 16);
            this.rdoStartPage.TabIndex = 0;
            this.rdoStartPage.Text = "开始页码";
            this.rdoStartPage.UseVisualStyleBackColor = true;
            this.rdoStartPage.Visible = false;
            this.rdoStartPage.CheckedChanged += new System.EventHandler(this.printMode_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnOk);
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Location = new System.Drawing.Point(12, 206);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(335, 56);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(143, 20);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 30);
            this.btnOk.TabIndex = 0;
            this.btnOk.TextValue = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(239, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.TextValue = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rdoSamePrint
            // 
            this.rdoSamePrint.AutoSize = true;
            this.rdoSamePrint.Checked = true;
            this.rdoSamePrint.Location = new System.Drawing.Point(19, 9);
            this.rdoSamePrint.Name = "rdoSamePrint";
            this.rdoSamePrint.Size = new System.Drawing.Size(71, 16);
            this.rdoSamePrint.TabIndex = 0;
            this.rdoSamePrint.TabStop = true;
            this.rdoSamePrint.Text = "原样重打";
            this.rdoSamePrint.UseVisualStyleBackColor = true;
            this.rdoSamePrint.CheckedChanged += new System.EventHandler(this.printMode_CheckedChanged);
            // 
            // rdoRePrint
            // 
            this.rdoRePrint.AutoSize = true;
            this.rdoRePrint.Location = new System.Drawing.Point(19, 90);
            this.rdoRePrint.Name = "rdoRePrint";
            this.rdoRePrint.Size = new System.Drawing.Size(71, 16);
            this.rdoRePrint.TabIndex = 1;
            this.rdoRePrint.Text = "重新打印";
            this.rdoRePrint.UseVisualStyleBackColor = true;
            this.rdoRePrint.CheckedChanged += new System.EventHandler(this.printMode_CheckedChanged);
            // 
            // PrintModeSelectFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 270);
            this.Controls.Add(this.rdoRePrint);
            this.Controls.Add(this.rdoSamePrint);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintModeSelectFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重打选择";
            this.Load += new System.EventHandler(this.PrintModeSelectFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPageEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPageStart)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFirstPage)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl groupBox1;
        private DevExpress.XtraEditors.LabelControl label1;
        private DevExpress.XtraEditors.LabelControl label2;
        private DevExpress.XtraEditors.PanelControl groupBox2;
        private System.Windows.Forms.DateTimePicker dtFirstDate;
        private System.Windows.Forms.RadioButton rdoStartDate;
        private System.Windows.Forms.RadioButton rdoStartPage;
        private System.Windows.Forms.DateTimePicker dtFirstTime;
        private DevExpress.XtraEditors.PanelControl groupBox3;
        private HISPlus.UserControls.UcButton btnOk;
        private HISPlus.UserControls.UcButton btnCancel;
        private System.Windows.Forms.NumericUpDown numPageEnd;
        private System.Windows.Forms.NumericUpDown numPageStart;
        private System.Windows.Forms.NumericUpDown numFirstPage;
        private System.Windows.Forms.RadioButton rdoSamePrint;
        private System.Windows.Forms.RadioButton rdoRePrint;
        private System.Windows.Forms.RadioButton rdoReprintAll;
    }
}