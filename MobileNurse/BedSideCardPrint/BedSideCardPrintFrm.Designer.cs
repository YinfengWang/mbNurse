namespace HISPlus
{
    partial class BedSideCardPrintFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BedSideCardPrintFrm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpPatientInfo = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnPrint = new HISPlus.UserControls.UcButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblAdmissionDate = new DevExpress.XtraEditors.LabelControl();
            this.panelControl8 = new DevExpress.XtraEditors.PanelControl();
            this.lblHlLevel = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl7 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.lblDeptName = new DevExpress.XtraEditors.LabelControl();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblSex = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.dgvPatient = new System.Windows.Forms.DataGridView();
            this.COL_NAME_CH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COL_VALUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAge = new DevExpress.XtraEditors.LabelControl();
            this.label2 = new System.Windows.Forms.Label();
            this.lblBed = new DevExpress.XtraEditors.LabelControl();
            this.lblZd = new System.Windows.Forms.Label();
            this.lblZdName = new DevExpress.XtraEditors.LabelControl();
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
            // grpPatientInfo
            // 
            this.grpPatientInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPatientInfo.Controls.Add(this.panelControl2);
            this.grpPatientInfo.Controls.Add(this.panelControl1);
            this.grpPatientInfo.Controls.Add(this.dgvPatient);
            this.grpPatientInfo.Location = new System.Drawing.Point(2, 6);
            this.grpPatientInfo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpPatientInfo.Name = "grpPatientInfo";
            this.grpPatientInfo.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpPatientInfo.Size = new System.Drawing.Size(809, 453);
            this.grpPatientInfo.TabIndex = 2;
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
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblZd);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.lblAdmissionDate);
            this.panelControl1.Controls.Add(this.panelControl8);
            this.panelControl1.Controls.Add(this.lblZdName);
            this.panelControl1.Controls.Add(this.lblHlLevel);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.panelControl7);
            this.panelControl1.Controls.Add(this.labelControl12);
            this.panelControl1.Controls.Add(this.lblDeptName);
            this.panelControl1.Controls.Add(this.panelControl6);
            this.panelControl1.Controls.Add(this.labelControl10);
            this.panelControl1.Controls.Add(this.panelControl4);
            this.panelControl1.Controls.Add(this.panelControl3);
            this.panelControl1.Controls.Add(this.lblName);
            this.panelControl1.Controls.Add(this.lblBed);
            this.panelControl1.Controls.Add(this.lblAge);
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
            this.lblAdmissionDate.Location = new System.Drawing.Point(81, 91);
            this.lblAdmissionDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblAdmissionDate.Name = "lblAdmissionDate";
            this.lblAdmissionDate.Size = new System.Drawing.Size(48, 14);
            this.lblAdmissionDate.TabIndex = 8;
            this.lblAdmissionDate.Text = "入院日期";
            // 
            // panelControl8
            // 
            this.panelControl8.Location = new System.Drawing.Point(18, 174);
            this.panelControl8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl8.Name = "panelControl8";
            this.panelControl8.Size = new System.Drawing.Size(326, 2);
            this.panelControl8.TabIndex = 12;
            // 
            // lblHlLevel
            // 
            this.lblHlLevel.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblHlLevel.Location = new System.Drawing.Point(81, 188);
            this.lblHlLevel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblHlLevel.Name = "lblHlLevel";
            this.lblHlLevel.Size = new System.Drawing.Size(48, 14);
            this.lblHlLevel.TabIndex = 11;
            this.lblHlLevel.Text = "护理级别";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(18, 91);
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
            this.labelControl12.Location = new System.Drawing.Point(18, 188);
            this.labelControl12.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(48, 14);
            this.labelControl12.TabIndex = 10;
            this.labelControl12.Text = "护理级别";
            // 
            // lblDeptName
            // 
            this.lblDeptName.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblDeptName.Location = new System.Drawing.Point(81, 158);
            this.lblDeptName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblDeptName.Name = "lblDeptName";
            this.lblDeptName.Size = new System.Drawing.Size(48, 14);
            this.lblDeptName.TabIndex = 11;
            this.lblDeptName.Text = "所在科室";
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
            this.labelControl10.Location = new System.Drawing.Point(18, 158);
            this.labelControl10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(48, 14);
            this.labelControl10.TabIndex = 10;
            this.labelControl10.Text = "所在科室";
            // 
            // panelControl4
            // 
            this.panelControl4.Location = new System.Drawing.Point(18, 76);
            this.panelControl4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(326, 2);
            this.panelControl4.TabIndex = 6;
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
            this.lblName.Location = new System.Drawing.Point(53, 21);
            this.lblName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(26, 14);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "姓名";
            // 
            // lblSex
            // 
            this.lblSex.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblSex.Location = new System.Drawing.Point(315, 57);
            this.lblSex.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(24, 14);
            this.lblSex.TabIndex = 5;
            this.lblSex.Text = "性别";
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
            this.labelControl4.Location = new System.Drawing.Point(272, 57);
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
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPatient.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
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
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.COL_VALUE.DefaultCellStyle = dataGridViewCellStyle12;
            this.COL_VALUE.HeaderText = "值";
            this.COL_VALUE.Name = "COL_VALUE";
            this.COL_VALUE.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 14);
            this.label1.TabIndex = 13;
            this.label1.Text = "年龄";
            // 
            // lblAge
            // 
            this.lblAge.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblAge.Location = new System.Drawing.Point(53, 57);
            this.lblAge.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(24, 14);
            this.lblAge.TabIndex = 5;
            this.lblAge.Text = "年龄";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(138, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 14);
            this.label2.TabIndex = 14;
            this.label2.Text = "床号";
            // 
            // lblBed
            // 
            this.lblBed.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblBed.Location = new System.Drawing.Point(175, 57);
            this.lblBed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblBed.Name = "lblBed";
            this.lblBed.Size = new System.Drawing.Size(24, 14);
            this.lblBed.TabIndex = 5;
            this.lblBed.Text = "床号";
            // 
            // lblZd
            // 
            this.lblZd.AutoSize = true;
            this.lblZd.Location = new System.Drawing.Point(18, 126);
            this.lblZd.Name = "lblZd";
            this.lblZd.Size = new System.Drawing.Size(31, 14);
            this.lblZd.TabIndex = 15;
            this.lblZd.Text = "诊断";
            // 
            // lblZdName
            // 
            this.lblZdName.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblZdName.Location = new System.Drawing.Point(81, 126);
            this.lblZdName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblZdName.Name = "lblZdName";
            this.lblZdName.Size = new System.Drawing.Size(24, 14);
            this.lblZdName.TabIndex = 11;
            this.lblZdName.Text = "诊断";
            // 
            // BedSideCardPrintFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 465);
            this.Controls.Add(this.grpPatientInfo);
            this.Name = "BedSideCardPrintFrm";
            this.Text = "打印床头卡";
            this.Load += new System.EventHandler(this.BedSideCardPrintFrm_Load);
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

        private DevExpress.XtraEditors.PanelControl grpPatientInfo;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private UserControls.UcButton btnPrint;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblAdmissionDate;
        private DevExpress.XtraEditors.PanelControl panelControl8;
        private DevExpress.XtraEditors.LabelControl lblHlLevel;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.PanelControl panelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl lblDeptName;
        private DevExpress.XtraEditors.PanelControl panelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl lblSex;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.DataGridView dgvPatient;
        private System.Windows.Forms.DataGridViewTextBoxColumn COL_NAME_CH;
        private System.Windows.Forms.DataGridViewTextBoxColumn COL_VALUE;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LabelControl lblBed;
        private DevExpress.XtraEditors.LabelControl lblAge;
        private System.Windows.Forms.Label lblZd;
        private DevExpress.XtraEditors.LabelControl lblZdName;
    }
}