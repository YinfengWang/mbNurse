namespace HISPlus
{
    partial class PIORecFrmOld
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PIORecFrm));
            this.grpSeach = new DevExpress.XtraEditors.PanelControl();
            this.btnStop = new HISPlus.UserControls.UcButton();
            this.btnDelete = new HISPlus.UserControls.UcButton();
            this.btnNew = new HISPlus.UserControls.UcButton();
            this.btnExit = new HISPlus.UserControls.UcButton();
            this.btnPrint = new HISPlus.UserControls.UcButton();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.grpPatientList = new DevExpress.XtraEditors.PanelControl();
            this.grpPatientInfo = new DevExpress.XtraEditors.PanelControl();
            this.grpHolder = new DevExpress.XtraEditors.PanelControl();
            this.dgvNursingSchedule = new System.Windows.Forms.DataGridView();
            this.SERIAL_NO_SHOW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PATIENT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VISIT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DIAGNOSIS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJECTIVE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MEASURE_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MEASURE_TITLE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MEASURE_CONTENT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BEGIN_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BEGIN_OPERATOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.END_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.END_OPERATOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CREATE_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CREATOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SERIAL_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpSeach.SuspendLayout();
            this.grpHolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNursingSchedule)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSeach
            // 
            this.grpSeach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSeach.Controls.Add(this.btnStop);
            this.grpSeach.Controls.Add(this.btnDelete);
            this.grpSeach.Controls.Add(this.btnNew);
            this.grpSeach.Controls.Add(this.btnExit);
            this.grpSeach.Controls.Add(this.btnPrint);
            this.grpSeach.Controls.Add(this.btnSave);
            this.grpSeach.Location = new System.Drawing.Point(202, 465);
            this.grpSeach.Name = "grpSeach";
            this.grpSeach.Size = new System.Drawing.Size(660, 56);
            this.grpSeach.TabIndex = 3;
            this.grpSeach.TabStop = false;
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(276, 18);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(90, 30);
            this.btnStop.TabIndex = 10;
            this.btnStop.TextValue = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(102, 18);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 30);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.TextValue = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNew
            // 
            this.btnNew.Enabled = false;
            this.btnNew.Location = new System.Drawing.Point(6, 18);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(90, 30);
            this.btnNew.TabIndex = 7;
            this.btnNew.TextValue = "新增";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(564, 18);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 30);
            this.btnExit.TabIndex = 6;
            this.btnExit.TextValue = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Enabled = false;
            this.btnPrint.Location = new System.Drawing.Point(468, 18);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(90, 30);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.TextValue = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(372, 18);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 3;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // grpPatientList
            // 
            this.grpPatientList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grpPatientList.Location = new System.Drawing.Point(9, 6);
            this.grpPatientList.Name = "grpPatientList";
            this.grpPatientList.Size = new System.Drawing.Size(187, 515);
            this.grpPatientList.TabIndex = 0;
            this.grpPatientList.TabStop = false;
            // 
            // grpPatientInfo
            // 
            this.grpPatientInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPatientInfo.Location = new System.Drawing.Point(202, 6);
            this.grpPatientInfo.Name = "grpPatientInfo";
            this.grpPatientInfo.Size = new System.Drawing.Size(660, 50);
            this.grpPatientInfo.TabIndex = 1;
            this.grpPatientInfo.TabStop = false;
            // 
            // grpHolder
            // 
            this.grpHolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpHolder.Controls.Add(this.dgvNursingSchedule);
            this.grpHolder.Location = new System.Drawing.Point(202, 62);
            this.grpHolder.Name = "grpHolder";
            this.grpHolder.Size = new System.Drawing.Size(660, 397);
            this.grpHolder.TabIndex = 4;
            this.grpHolder.TabStop = false;
            // 
            // dgvNursingSchedule
            // 
            this.dgvNursingSchedule.AllowUserToAddRows = false;
            this.dgvNursingSchedule.AllowUserToDeleteRows = false;
            this.dgvNursingSchedule.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.dgvNursingSchedule.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNursingSchedule.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvNursingSchedule.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNursingSchedule.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvNursingSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNursingSchedule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SERIAL_NO_SHOW,
            this.PATIENT_ID,
            this.VISIT_ID,
            this.DIAGNOSIS,
            this.OBJECTIVE,
            this.MEASURE_TYPE,
            this.MEASURE_TITLE,
            this.MEASURE_CONTENT,
            this.BEGIN_DATE,
            this.BEGIN_OPERATOR,
            this.END_DATE,
            this.END_OPERATOR,
            this.CREATE_DATE,
            this.CREATOR,
            this.SERIAL_NO});
            this.dgvNursingSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNursingSchedule.Location = new System.Drawing.Point(3, 17);
            this.dgvNursingSchedule.MultiSelect = false;
            this.dgvNursingSchedule.Name = "dgvNursingSchedule";
            this.dgvNursingSchedule.RowHeadersVisible = false;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNursingSchedule.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvNursingSchedule.RowTemplate.Height = 23;
            this.dgvNursingSchedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNursingSchedule.Size = new System.Drawing.Size(654, 377);
            this.dgvNursingSchedule.TabIndex = 2;
            this.dgvNursingSchedule.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvNursingSchedule_RowPostPaint);
            this.dgvNursingSchedule.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvNursingSchedule_DataError);
            this.dgvNursingSchedule.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvNursingSchedule_DataBindingComplete);
            this.dgvNursingSchedule.SelectionChanged += new System.EventHandler(this.dgvNursingSchedule_SelectionChanged);
            // 
            // SERIAL_NO_SHOW
            // 
            this.SERIAL_NO_SHOW.HeaderText = "序号";
            this.SERIAL_NO_SHOW.Name = "SERIAL_NO_SHOW";
            this.SERIAL_NO_SHOW.ReadOnly = true;
            this.SERIAL_NO_SHOW.Width = 40;
            // 
            // PATIENT_ID
            // 
            this.PATIENT_ID.DataPropertyName = "PATIENT_ID";
            this.PATIENT_ID.HeaderText = "PATIENT_ID";
            this.PATIENT_ID.Name = "PATIENT_ID";
            this.PATIENT_ID.ReadOnly = true;
            this.PATIENT_ID.Visible = false;
            // 
            // VISIT_ID
            // 
            this.VISIT_ID.DataPropertyName = "VISIT_ID";
            this.VISIT_ID.HeaderText = "VISIT_ID";
            this.VISIT_ID.Name = "VISIT_ID";
            this.VISIT_ID.ReadOnly = true;
            this.VISIT_ID.Visible = false;
            // 
            // DIAGNOSIS
            // 
            this.DIAGNOSIS.DataPropertyName = "DIAGNOSIS";
            this.DIAGNOSIS.HeaderText = "护理诊断";
            this.DIAGNOSIS.Name = "DIAGNOSIS";
            this.DIAGNOSIS.ReadOnly = true;
            this.DIAGNOSIS.Width = 120;
            // 
            // OBJECTIVE
            // 
            this.OBJECTIVE.DataPropertyName = "OBJECTIVE";
            this.OBJECTIVE.HeaderText = "预期目标";
            this.OBJECTIVE.Name = "OBJECTIVE";
            this.OBJECTIVE.ReadOnly = true;
            this.OBJECTIVE.Width = 120;
            // 
            // MEASURE_TYPE
            // 
            this.MEASURE_TYPE.DataPropertyName = "MEASURE_TYPE";
            this.MEASURE_TYPE.HeaderText = "类型";
            this.MEASURE_TYPE.Name = "MEASURE_TYPE";
            this.MEASURE_TYPE.ReadOnly = true;
            this.MEASURE_TYPE.Width = 40;
            // 
            // MEASURE_TITLE
            // 
            this.MEASURE_TITLE.DataPropertyName = "MEASURE_TITLE";
            this.MEASURE_TITLE.HeaderText = "措施";
            this.MEASURE_TITLE.Name = "MEASURE_TITLE";
            this.MEASURE_TITLE.Width = 240;
            // 
            // MEASURE_CONTENT
            // 
            this.MEASURE_CONTENT.DataPropertyName = "MEASURE_CONTENT";
            this.MEASURE_CONTENT.HeaderText = "备注";
            this.MEASURE_CONTENT.Name = "MEASURE_CONTENT";
            // 
            // BEGIN_DATE
            // 
            this.BEGIN_DATE.DataPropertyName = "BEGIN_DATE";
            dataGridViewCellStyle3.Format = "g";
            dataGridViewCellStyle3.NullValue = null;
            this.BEGIN_DATE.DefaultCellStyle = dataGridViewCellStyle3;
            this.BEGIN_DATE.HeaderText = "开始日期";
            this.BEGIN_DATE.Name = "BEGIN_DATE";
            this.BEGIN_DATE.ReadOnly = true;
            // 
            // BEGIN_OPERATOR
            // 
            this.BEGIN_OPERATOR.DataPropertyName = "BEGIN_OPERATOR";
            this.BEGIN_OPERATOR.HeaderText = "开始护士";
            this.BEGIN_OPERATOR.Name = "BEGIN_OPERATOR";
            this.BEGIN_OPERATOR.ReadOnly = true;
            this.BEGIN_OPERATOR.Width = 80;
            // 
            // END_DATE
            // 
            this.END_DATE.DataPropertyName = "END_DATE";
            dataGridViewCellStyle4.Format = "g";
            dataGridViewCellStyle4.NullValue = null;
            this.END_DATE.DefaultCellStyle = dataGridViewCellStyle4;
            this.END_DATE.HeaderText = "结束日期";
            this.END_DATE.Name = "END_DATE";
            this.END_DATE.ReadOnly = true;
            // 
            // END_OPERATOR
            // 
            this.END_OPERATOR.DataPropertyName = "END_OPERATOR";
            this.END_OPERATOR.HeaderText = "结束护士";
            this.END_OPERATOR.Name = "END_OPERATOR";
            this.END_OPERATOR.ReadOnly = true;
            this.END_OPERATOR.Width = 80;
            // 
            // CREATE_DATE
            // 
            this.CREATE_DATE.DataPropertyName = "CREATE_DATE";
            this.CREATE_DATE.HeaderText = "CREATE_DATE";
            this.CREATE_DATE.Name = "CREATE_DATE";
            this.CREATE_DATE.ReadOnly = true;
            this.CREATE_DATE.Visible = false;
            // 
            // CREATOR
            // 
            this.CREATOR.DataPropertyName = "CREATOR";
            this.CREATOR.HeaderText = "CREATOR";
            this.CREATOR.Name = "CREATOR";
            this.CREATOR.ReadOnly = true;
            this.CREATOR.Visible = false;
            // 
            // SERIAL_NO
            // 
            this.SERIAL_NO.DataPropertyName = "SERIAL_NO";
            this.SERIAL_NO.HeaderText = "SERIAL_NO";
            this.SERIAL_NO.Name = "SERIAL_NO";
            this.SERIAL_NO.ReadOnly = true;
            this.SERIAL_NO.Visible = false;
            // 
            // PIORecFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 532);
            this.Controls.Add(this.grpHolder);
            this.Controls.Add(this.grpPatientInfo);
            this.Controls.Add(this.grpPatientList);
            this.Controls.Add(this.grpSeach);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PIORecFrm";
            this.Text = "护理措施";
            this.Load += new System.EventHandler(this.EvaluationEverydayFrm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EvaluationEverydayFrm_FormClosed);
            this.grpSeach.ResumeLayout(false);
            this.grpHolder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNursingSchedule)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl grpSeach;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.PanelControl grpPatientList;
        private DevExpress.XtraEditors.PanelControl grpPatientInfo;
        private HISPlus.UserControls.UcButton btnPrint;
        private HISPlus.UserControls.UcButton btnSave;
        private HISPlus.UserControls.UcButton btnExit;
        private DevExpress.XtraEditors.PanelControl grpHolder;
        private HISPlus.UserControls.UcButton btnNew;
        private System.Windows.Forms.DataGridView dgvNursingSchedule;
        private HISPlus.UserControls.UcButton btnDelete;
        private HISPlus.UserControls.UcButton btnStop;
        private System.Windows.Forms.DataGridViewTextBoxColumn SERIAL_NO_SHOW;
        private System.Windows.Forms.DataGridViewTextBoxColumn PATIENT_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn VISIT_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DIAGNOSIS;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJECTIVE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MEASURE_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MEASURE_TITLE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MEASURE_CONTENT;
        private System.Windows.Forms.DataGridViewTextBoxColumn BEGIN_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn BEGIN_OPERATOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn END_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn END_OPERATOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn CREATE_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CREATOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn SERIAL_NO;
    }
}