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
            this.numReprintPageNo = new System.Windows.Forms.NumericUpDown();
            this.rdoReprint = new System.Windows.Forms.RadioButton();
            this.numPageNo = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.radDate = new System.Windows.Forms.RadioButton();
            this.rdoContinuePrint = new System.Windows.Forms.RadioButton();
            this.rdoPageNoPrint = new System.Windows.Forms.RadioButton();
            this.rdoFirstPrint = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new DevExpress.XtraEditors.PanelControl();
            this.btnOk = new HISPlus.UserControls.UcButton();
            this.btnCancel = new HISPlus.UserControls.UcButton();
            this.txtDateStart = new System.Windows.Forms.DateTimePicker();
            this.txtEndDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.numReprintPageNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPageNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox3)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // numReprintPageNo
            // 
            this.numReprintPageNo.Enabled = false;
            this.numReprintPageNo.Location = new System.Drawing.Point(252, 51);
            this.numReprintPageNo.Name = "numReprintPageNo";
            this.numReprintPageNo.Size = new System.Drawing.Size(55, 21);
            this.numReprintPageNo.TabIndex = 8;
            this.numReprintPageNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numReprintPageNo.Visible = false;
            // 
            // rdoReprint
            // 
            this.rdoReprint.AutoSize = true;
            this.rdoReprint.Location = new System.Drawing.Point(177, 54);
            this.rdoReprint.Name = "rdoReprint";
            this.rdoReprint.Size = new System.Drawing.Size(71, 16);
            this.rdoReprint.TabIndex = 7;
            this.rdoReprint.Text = "重    打";
            this.rdoReprint.UseVisualStyleBackColor = true;
            this.rdoReprint.Visible = false;
            // 
            // numPageNo
            // 
            this.numPageNo.Enabled = false;
            this.numPageNo.Location = new System.Drawing.Point(254, 32);
            this.numPageNo.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numPageNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPageNo.Name = "numPageNo";
            this.numPageNo.Size = new System.Drawing.Size(55, 21);
            this.numPageNo.TabIndex = 3;
            this.numPageNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPageNo.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtEndDate);
            this.groupBox1.Controls.Add(this.txtDateStart);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.radDate);
            this.groupBox1.Controls.Add(this.numReprintPageNo);
            this.groupBox1.Controls.Add(this.rdoReprint);
            this.groupBox1.Controls.Add(this.rdoContinuePrint);
            this.groupBox1.Controls.Add(this.numPageNo);
            this.groupBox1.Controls.Add(this.rdoPageNoPrint);
            this.groupBox1.Controls.Add(this.rdoFirstPrint);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 162);
            this.groupBox1.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(174, 118);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(8, 14);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "--";
            // 
            // radDate
            // 
            this.radDate.AutoSize = true;
            this.radDate.Location = new System.Drawing.Point(23, 74);
            this.radDate.Name = "radDate";
            this.radDate.Size = new System.Drawing.Size(59, 16);
            this.radDate.TabIndex = 10;
            this.radDate.TabStop = true;
            this.radDate.Text = "时间段";
            this.radDate.UseVisualStyleBackColor = true;
            this.radDate.CheckedChanged += new System.EventHandler(this.radDate_CheckedChanged);
            // 
            // rdoContinuePrint
            // 
            this.rdoContinuePrint.AutoSize = true;
            this.rdoContinuePrint.Location = new System.Drawing.Point(177, 16);
            this.rdoContinuePrint.Name = "rdoContinuePrint";
            this.rdoContinuePrint.Size = new System.Drawing.Size(71, 16);
            this.rdoContinuePrint.TabIndex = 4;
            this.rdoContinuePrint.Text = "续    打";
            this.rdoContinuePrint.UseVisualStyleBackColor = true;
            this.rdoContinuePrint.Visible = false;
            // 
            // rdoPageNoPrint
            // 
            this.rdoPageNoPrint.AutoSize = true;
            this.rdoPageNoPrint.Location = new System.Drawing.Point(177, 35);
            this.rdoPageNoPrint.Name = "rdoPageNoPrint";
            this.rdoPageNoPrint.Size = new System.Drawing.Size(71, 16);
            this.rdoPageNoPrint.TabIndex = 2;
            this.rdoPageNoPrint.Text = "页码打印";
            this.rdoPageNoPrint.UseVisualStyleBackColor = true;
            this.rdoPageNoPrint.Visible = false;
            // 
            // rdoFirstPrint
            // 
            this.rdoFirstPrint.AutoSize = true;
            this.rdoFirstPrint.Checked = true;
            this.rdoFirstPrint.Location = new System.Drawing.Point(23, 34);
            this.rdoFirstPrint.Name = "rdoFirstPrint";
            this.rdoFirstPrint.Size = new System.Drawing.Size(71, 16);
            this.rdoFirstPrint.TabIndex = 1;
            this.rdoFirstPrint.TabStop = true;
            this.rdoFirstPrint.Text = "全部打印";
            this.rdoFirstPrint.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnOk);
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Location = new System.Drawing.Point(4, 170);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 60);
            this.groupBox3.TabIndex = 4;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Image = null;
            this.btnOk.ImageRight = false;
            this.btnOk.ImageStyle = HISPlus.UserControls.ImageStyle.None;
            this.btnOk.Location = new System.Drawing.Point(125, 16);
            this.btnOk.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnOk.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 30);
            this.btnOk.TabIndex = 2;
            this.btnOk.TextValue = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = null;
            this.btnCancel.ImageRight = false;
            this.btnCancel.ImageStyle = HISPlus.UserControls.ImageStyle.None;
            this.btnCancel.Location = new System.Drawing.Point(221, 16);
            this.btnCancel.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnCancel.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.TextValue = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtDateStart
            // 
            this.txtDateStart.Location = new System.Drawing.Point(23, 116);
            this.txtDateStart.Name = "txtDateStart";
            this.txtDateStart.Size = new System.Drawing.Size(145, 21);
            this.txtDateStart.TabIndex = 14;
            // 
            // txtEndDate
            // 
            this.txtEndDate.Location = new System.Drawing.Point(188, 116);
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.Size = new System.Drawing.Size(145, 21);
            this.txtEndDate.TabIndex = 15;
            // 
            // PrintModeSelectFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 235);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintModeSelectFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印选择";
            this.Load += new System.EventHandler(this.PrintModeSelectFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numReprintPageNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPageNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        
        private System.Windows.Forms.NumericUpDown numReprintPageNo;
        private System.Windows.Forms.RadioButton rdoReprint;
        private System.Windows.Forms.NumericUpDown numPageNo;
        private DevExpress.XtraEditors.PanelControl groupBox1;
        private System.Windows.Forms.RadioButton rdoContinuePrint;
        private System.Windows.Forms.RadioButton rdoPageNoPrint;
        private System.Windows.Forms.RadioButton rdoFirstPrint;
        private DevExpress.XtraEditors.PanelControl groupBox3;
        private UserControls.UcButton btnOk;
        private UserControls.UcButton btnCancel;
        private System.Windows.Forms.RadioButton radDate;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.DateTimePicker txtEndDate;
        private System.Windows.Forms.DateTimePicker txtDateStart;
    }
}