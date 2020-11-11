namespace HISPlus
{
    partial class PatientListWardFrm1{
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbWardList = new System.Windows.Forms.ComboBox();
            this.BED_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BED_LABEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VISIT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PATIENT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPatient = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatient)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuRefresh});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 28);
            // 
            // cmnuRefresh
            // 
            this.cmnuRefresh.Name = "cmnuRefresh";
            this.cmnuRefresh.Size = new System.Drawing.Size(108, 24);
            this.cmnuRefresh.Text = "刷新";
            this.cmnuRefresh.Click += new System.EventHandler(this.cmnuRefresh_Click);
            // 
            // cmbWardList
            // 
            this.cmbWardList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWardList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWardList.FormattingEnabled = true;
            this.cmbWardList.Location = new System.Drawing.Point(3, 626);
            this.cmbWardList.Margin = new System.Windows.Forms.Padding(4);
            this.cmbWardList.MaxDropDownItems = 20;
            this.cmbWardList.Name = "cmbWardList";
            this.cmbWardList.Size = new System.Drawing.Size(173, 23);
            this.cmbWardList.TabIndex = 5;
            this.cmbWardList.SelectedValueChanged += new System.EventHandler(this.cmbWardList_SelectedValueChanged);
            // 
            // BED_NO
            // 
            this.BED_NO.HeaderText = "床号";
            this.BED_NO.Name = "BED_NO";
            this.BED_NO.ReadOnly = true;
            this.BED_NO.Visible = false;
            // 
            // NAME
            // 
            this.NAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NAME.DataPropertyName = "NAME";
            this.NAME.HeaderText = "姓名";
            this.NAME.Name = "NAME";
            this.NAME.ReadOnly = true;
            // 
            // BED_LABEL
            // 
            this.BED_LABEL.DataPropertyName = "BED_LABEL";
            this.BED_LABEL.Frozen = true;
            this.BED_LABEL.HeaderText = "床标";
            this.BED_LABEL.Name = "BED_LABEL";
            this.BED_LABEL.ReadOnly = true;
            this.BED_LABEL.Width = 50;
            // 
            // VISIT_ID
            // 
            this.VISIT_ID.DataPropertyName = "VISIT_ID";
            this.VISIT_ID.Frozen = true;
            this.VISIT_ID.HeaderText = "就诊序号";
            this.VISIT_ID.Name = "VISIT_ID";
            this.VISIT_ID.ReadOnly = true;
            this.VISIT_ID.Visible = false;
            // 
            // PATIENT_ID
            // 
            this.PATIENT_ID.DataPropertyName = "PATIENT_ID";
            this.PATIENT_ID.Frozen = true;
            this.PATIENT_ID.HeaderText = "病人ID";
            this.PATIENT_ID.Name = "PATIENT_ID";
            this.PATIENT_ID.ReadOnly = true;
            this.PATIENT_ID.Visible = false;
            // 
            // Column1
            // 
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            this.Column1.Width = 5;
            // 
            // dgvPatient
            // 
            this.dgvPatient.AllowUserToAddRows = false;
            this.dgvPatient.AllowUserToDeleteRows = false;
            this.dgvPatient.AllowUserToResizeRows = false;
            this.dgvPatient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPatient.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvPatient.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.PATIENT_ID,
            this.VISIT_ID,
            this.BED_LABEL,
            this.NAME,
            this.BED_NO});
            this.dgvPatient.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvPatient.Location = new System.Drawing.Point(0, 0);
            this.dgvPatient.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPatient.MultiSelect = false;
            this.dgvPatient.Name = "dgvPatient";
            this.dgvPatient.ReadOnly = true;
            this.dgvPatient.RowHeadersVisible = false;
            this.dgvPatient.RowTemplate.Height = 23;
            this.dgvPatient.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPatient.Size = new System.Drawing.Size(180, 619);
            this.dgvPatient.TabIndex = 4;
            this.dgvPatient.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_RowPostPaint);
            this.dgvPatient.SelectionChanged += new System.EventHandler(this.dgvPatient_SelectionChanged);
            this.dgvPatient.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvPatient_KeyUp);
            // 
            // PatientListWardFrm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 659);
            this.Controls.Add(this.cmbWardList);
            this.Controls.Add(this.dgvPatient);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PatientListWardFrm1";
            this.Text = "病区病人列表";
            this.Load += new System.EventHandler(this.PatientListWardFrm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatient)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbWardList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmnuRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn BED_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn BED_LABEL;
        private System.Windows.Forms.DataGridViewTextBoxColumn VISIT_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PATIENT_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridView dgvPatient;
    }
}