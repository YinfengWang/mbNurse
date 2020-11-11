namespace HISPlus
{
    partial class WristBandPrintFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WristBandPrintFrm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnPrint = new HISPlus.UserControls.UcButton();
            this.grpPatientInfo = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblAdmissionDate = new DevExpress.XtraEditors.LabelControl();
            this.panelControl8 = new DevExpress.XtraEditors.PanelControl();
            this.lblBedLabel = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl7 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.lblDeptName = new DevExpress.XtraEditors.LabelControl();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.lblPatientID = new DevExpress.XtraEditors.LabelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblSex = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.dgvPatient = new System.Windows.Forms.DataGridView();
            this.COL_NAME_CH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COL_VALUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grpPatientInfo)).BeginInit();
            this.grpPatientInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatient)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPrint.Enabled = false;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageRight = false;
            this.btnPrint.ImageStyle = HISPlus.UserControls.ImageStyle.Print;
            this.btnPrint.Location = new System.Drawing.Point(257, 16);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPrint.MaximumSize = new System.Drawing.Size(88, 23);
            this.btnPrint.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(88, 23);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.TextValue = "打印";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // grpPatientInfo
            // 
            this.grpPatientInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPatientInfo.Controls.Add(this.panelControl2);
            this.grpPatientInfo.Controls.Add(this.panelControl1);
            this.grpPatientInfo.Controls.Add(this.dgvPatient);
            this.grpPatientInfo.Location = new System.Drawing.Point(11, 14);
            this.grpPatientInfo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpPatientInfo.Name = "grpPatientInfo";
            this.grpPatientInfo.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpPatientInfo.Size = new System.Drawing.Size(830, 468);
            this.grpPatientInfo.TabIndex = 1;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnPrint);
            this.panelControl2.Location = new System.Drawing.Point(17, 247);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(366, 53);
            this.panelControl2.TabIndex = 3;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblAdmissionDate);
            this.panelControl1.Controls.Add(this.panelControl8);
            this.panelControl1.Controls.Add(this.lblBedLabel);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.panelControl7);
            this.panelControl1.Controls.Add(this.labelControl12);
            this.panelControl1.Controls.Add(this.lblDeptName);
            this.panelControl1.Controls.Add(this.panelControl6);
            this.panelControl1.Controls.Add(this.labelControl10);
            this.panelControl1.Controls.Add(this.lblPatientID);
            this.panelControl1.Controls.Add(this.panelControl4);
            this.panelControl1.Controls.Add(this.labelControl8);
            this.panelControl1.Controls.Add(this.panelControl3);
            this.panelControl1.Controls.Add(this.lblName);
            this.panelControl1.Controls.Add(this.lblSex);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Location = new System.Drawing.Point(17, 21);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(366, 213);
            this.panelControl1.TabIndex = 2;
            // 
            // lblAdmissionDate
            // 
            this.lblAdmissionDate.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblAdmissionDate.Location = new System.Drawing.Point(180, 189);
            this.lblAdmissionDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblAdmissionDate.Name = "lblAdmissionDate";
            this.lblAdmissionDate.Size = new System.Drawing.Size(0, 14);
            this.lblAdmissionDate.TabIndex = 8;
            // 
            // panelControl8
            // 
            this.panelControl8.Location = new System.Drawing.Point(18, 174);
            this.panelControl8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl8.Name = "panelControl8";
            this.panelControl8.Size = new System.Drawing.Size(326, 2);
            this.panelControl8.TabIndex = 12;
            // 
            // lblBedLabel
            // 
            this.lblBedLabel.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblBedLabel.Location = new System.Drawing.Point(180, 156);
            this.lblBedLabel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblBedLabel.Name = "lblBedLabel";
            this.lblBedLabel.Size = new System.Drawing.Size(0, 14);
            this.lblBedLabel.TabIndex = 11;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(18, 189);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(48, 14);
            this.labelControl6.TabIndex = 7;
            this.labelControl6.Text = "入院日期";
            // 
            // panelControl7
            // 
            this.panelControl7.Location = new System.Drawing.Point(18, 145);
            this.panelControl7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl7.Name = "panelControl7";
            this.panelControl7.Size = new System.Drawing.Size(326, 2);
            this.panelControl7.TabIndex = 12;
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(18, 156);
            this.labelControl12.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(24, 14);
            this.labelControl12.TabIndex = 10;
            this.labelControl12.Text = "床号";
            // 
            // lblDeptName
            // 
            this.lblDeptName.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblDeptName.Location = new System.Drawing.Point(180, 126);
            this.lblDeptName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblDeptName.Name = "lblDeptName";
            this.lblDeptName.Size = new System.Drawing.Size(0, 14);
            this.lblDeptName.TabIndex = 11;
            // 
            // panelControl6
            // 
            this.panelControl6.Location = new System.Drawing.Point(18, 109);
            this.panelControl6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Size = new System.Drawing.Size(326, 2);
            this.panelControl6.TabIndex = 12;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(18, 126);
            this.labelControl10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(48, 14);
            this.labelControl10.TabIndex = 10;
            this.labelControl10.Text = "所在科室";
            // 
            // lblPatientID
            // 
            this.lblPatientID.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblPatientID.Location = new System.Drawing.Point(180, 90);
            this.lblPatientID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblPatientID.Name = "lblPatientID";
            this.lblPatientID.Size = new System.Drawing.Size(0, 14);
            this.lblPatientID.TabIndex = 11;
            // 
            // panelControl4
            // 
            this.panelControl4.Location = new System.Drawing.Point(18, 75);
            this.panelControl4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(326, 2);
            this.panelControl4.TabIndex = 6;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(18, 90);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(60, 14);
            this.labelControl8.TabIndex = 10;
            this.labelControl8.Text = "病人标识号";
            // 
            // panelControl3
            // 
            this.panelControl3.Location = new System.Drawing.Point(18, 40);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(326, 2);
            this.panelControl3.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblName.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblName.Location = new System.Drawing.Point(180, 21);
            this.lblName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 14);
            this.lblName.TabIndex = 2;
            // 
            // lblSex
            // 
            this.lblSex.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblSex.Location = new System.Drawing.Point(180, 56);
            this.lblSex.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(0, 14);
            this.lblSex.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(18, 21);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "姓名";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(18, 56);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "性别";
            // 
            // dgvPatient
            // 
            this.dgvPatient.AllowUserToAddRows = false;
            this.dgvPatient.AllowUserToDeleteRows = false;
            this.dgvPatient.AllowUserToResizeRows = false;
            this.dgvPatient.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPatient.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPatient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPatient.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.COL_NAME_CH,
            this.COL_VALUE});
            this.dgvPatient.Location = new System.Drawing.Point(9, 331);
            this.dgvPatient.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvPatient.MultiSelect = false;
            this.dgvPatient.Name = "dgvPatient";
            this.dgvPatient.ReadOnly = true;
            this.dgvPatient.RowHeadersVisible = false;
            this.dgvPatient.RowTemplate.Height = 23;
            this.dgvPatient.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPatient.Size = new System.Drawing.Size(408, 107);
            this.dgvPatient.TabIndex = 0;
            this.dgvPatient.Visible = false;
            // 
            // COL_NAME_CH
            // 
            this.COL_NAME_CH.DataPropertyName = "COL_NAME_CH";
            this.COL_NAME_CH.HeaderText = "属性";
            this.COL_NAME_CH.Name = "COL_NAME_CH";
            this.COL_NAME_CH.ReadOnly = true;
            this.COL_NAME_CH.Width = 80;
            // 
            // COL_VALUE
            // 
            this.COL_VALUE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.COL_VALUE.DataPropertyName = "COL_VALUE";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.COL_VALUE.DefaultCellStyle = dataGridViewCellStyle2;
            this.COL_VALUE.HeaderText = "值";
            this.COL_VALUE.Name = "COL_VALUE";
            this.COL_VALUE.ReadOnly = true;
            // 
            // WristBandPrintFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 557);
            this.Controls.Add(this.grpPatientInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "WristBandPrintFrm";
            this.Text = "腕带打印";
            this.Load += new System.EventHandler(this.WristBandPrintFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpPatientInfo)).EndInit();
            this.grpPatientInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatient)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private HISPlus.UserControls.UcButton btnPrint;
        private DevExpress.XtraEditors.PanelControl grpPatientInfo;
        private System.Windows.Forms.DataGridView dgvPatient;
        private System.Windows.Forms.DataGridViewTextBoxColumn COL_NAME_CH;
        private System.Windows.Forms.DataGridViewTextBoxColumn COL_VALUE;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.LabelControl lblSex;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl lblAdmissionDate;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.PanelControl panelControl6;
        private DevExpress.XtraEditors.LabelControl lblPatientID;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.PanelControl panelControl8;
        private DevExpress.XtraEditors.LabelControl lblBedLabel;
        private DevExpress.XtraEditors.PanelControl panelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl lblDeptName;
        private DevExpress.XtraEditors.LabelControl labelControl10;
    }
}