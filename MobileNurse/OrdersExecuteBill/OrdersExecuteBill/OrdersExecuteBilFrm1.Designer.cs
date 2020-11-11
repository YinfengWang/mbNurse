namespace HISPlus
{
    partial class OrdersExecuteBilFrm1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpPatientInfo = new DevExpress.XtraEditors.PanelControl();
            this.grpFunction = new DevExpress.XtraEditors.PanelControl();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.dtpTime1 = new System.Windows.Forms.DateTimePicker();
            this.dtpTime0 = new System.Windows.Forms.DateTimePicker();
            this.btnPrint = new HISPlus.UserControls.UcButton();
            this.btnExit = new HISPlus.UserControls.UcButton();
            this.dtRngStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery = new HISPlus.UserControls.UcButton();
            this.grpPatientList = new DevExpress.XtraEditors.PanelControl();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.grpMain = new DevExpress.XtraEditors.PanelControl();
            this.lblInfo = new DevExpress.XtraEditors.LabelControl();
            this.chkPrinted = new System.Windows.Forms.CheckBox();
            this.chkNotPrinted = new System.Windows.Forms.CheckBox();
            this.chkLongTerm = new System.Windows.Forms.CheckBox();
            this.chkTempTerm = new System.Windows.Forms.CheckBox();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.dgvOrdersExecute = new System.Windows.Forms.DataGridView();
            this.PATIENT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SELECTION = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BED_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PATIENT_NAME = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.VISIT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDER_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDER_SUB_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REPEAT_INDICATOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDER_TEXT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDER_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOSAGE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOSAGE_UNITS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FREQUENCY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ADMINISTRATION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SCHEDULE_PERFORM_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpAdmin = new DevExpress.XtraEditors.PanelControl();
            this.lvwAdministration = new System.Windows.Forms.ListView();
            this.grpFunction.SuspendLayout();
            this.grpPatientList.SuspendLayout();
            this.grpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdersExecute)).BeginInit();
            this.grpAdmin.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPatientInfo
            // 
            this.grpPatientInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPatientInfo.Location = new System.Drawing.Point(213, 12);
            this.grpPatientInfo.Name = "grpPatientInfo";
            this.grpPatientInfo.Size = new System.Drawing.Size(623, 50);
            this.grpPatientInfo.TabIndex = 1;
            this.grpPatientInfo.TabStop = false;
            // 
            // grpFunction
            // 
            this.grpFunction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFunction.Controls.Add(this.label2);
            this.grpFunction.Controls.Add(this.dtpTime1);
            this.grpFunction.Controls.Add(this.dtpTime0);
            this.grpFunction.Controls.Add(this.btnPrint);
            this.grpFunction.Controls.Add(this.btnExit);
            this.grpFunction.Controls.Add(this.dtRngStart);
            this.grpFunction.Controls.Add(this.label1);
            this.grpFunction.Controls.Add(this.btnQuery);
            this.grpFunction.Location = new System.Drawing.Point(213, 443);
            this.grpFunction.Name = "grpFunction";
            this.grpFunction.Size = new System.Drawing.Size(623, 46);
            this.grpFunction.TabIndex = 2;
            this.grpFunction.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(253, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "-";
            // 
            // dtpTime1
            // 
            this.dtpTime1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime1.Location = new System.Drawing.Point(267, 18);
            this.dtpTime1.Name = "dtpTime1";
            this.dtpTime1.ShowUpDown = true;
            this.dtpTime1.Size = new System.Drawing.Size(80, 21);
            this.dtpTime1.TabIndex = 6;
            // 
            // dtpTime0
            // 
            this.dtpTime0.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime0.Location = new System.Drawing.Point(170, 18);
            this.dtpTime0.Name = "dtpTime0";
            this.dtpTime0.ShowUpDown = true;
            this.dtpTime0.Size = new System.Drawing.Size(80, 21);
            this.dtpTime0.TabIndex = 5;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Enabled = false;
            this.btnPrint.Location = new System.Drawing.Point(431, 13);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(90, 30);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.TextValue = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(527, 13);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 30);
            this.btnExit.TabIndex = 4;
            this.btnExit.TextValue = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dtRngStart
            // 
            this.dtRngStart.Location = new System.Drawing.Point(41, 18);
            this.dtRngStart.Name = "dtRngStart";
            this.dtRngStart.Size = new System.Drawing.Size(113, 21);
            this.dtRngStart.TabIndex = 1;
            this.dtRngStart.ValueChanged += new System.EventHandler(this.dtRngStart_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "日期";
            // 
            // btnQuery
            // 
            this.btnQuery.Enabled = false;
            this.btnQuery.Location = new System.Drawing.Point(355, 13);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(90, 30);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.TextValue = "查询(&Q)";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // grpPatientList
            // 
            this.grpPatientList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpPatientList.Controls.Add(this.chkAll);
            this.grpPatientList.Location = new System.Drawing.Point(12, 12);
            this.grpPatientList.Name = "grpPatientList";
            this.grpPatientList.Size = new System.Drawing.Size(195, 477);
            this.grpPatientList.TabIndex = 0;
            this.grpPatientList.TabStop = false;
            this.grpPatientList.Text = "病人列表";
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(143, 0);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(48, 16);
            this.chkAll.TabIndex = 0;
            this.chkAll.Text = "全部";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // grpMain
            // 
            this.grpMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMain.Controls.Add(this.lblInfo);
            this.grpMain.Controls.Add(this.chkPrinted);
            this.grpMain.Controls.Add(this.chkNotPrinted);
            this.grpMain.Controls.Add(this.chkLongTerm);
            this.grpMain.Controls.Add(this.chkTempTerm);
            this.grpMain.Controls.Add(this.chkSelectAll);
            this.grpMain.Controls.Add(this.dgvOrdersExecute);
            this.grpMain.Location = new System.Drawing.Point(213, 68);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(623, 327);
            this.grpMain.TabIndex = 2;
            this.grpMain.TabStop = false;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(209, 1);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 12);
            this.lblInfo.TabIndex = 6;
            // 
            // chkPrinted
            // 
            this.chkPrinted.AutoSize = true;
            this.chkPrinted.Checked = true;
            this.chkPrinted.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrinted.Location = new System.Drawing.Point(134, 0);
            this.chkPrinted.Name = "chkPrinted";
            this.chkPrinted.Size = new System.Drawing.Size(60, 16);
            this.chkPrinted.TabIndex = 5;
            this.chkPrinted.Text = "已打印";
            this.chkPrinted.UseVisualStyleBackColor = true;
            this.chkPrinted.CheckedChanged += new System.EventHandler(this.chkLongTerm_CheckedChanged);
            // 
            // chkNotPrinted
            // 
            this.chkNotPrinted.AutoSize = true;
            this.chkNotPrinted.Checked = true;
            this.chkNotPrinted.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNotPrinted.Location = new System.Drawing.Point(68, 0);
            this.chkNotPrinted.Name = "chkNotPrinted";
            this.chkNotPrinted.Size = new System.Drawing.Size(60, 16);
            this.chkNotPrinted.TabIndex = 4;
            this.chkNotPrinted.Text = "未打印";
            this.chkNotPrinted.UseVisualStyleBackColor = true;
            this.chkNotPrinted.CheckedChanged += new System.EventHandler(this.chkLongTerm_CheckedChanged);
            // 
            // chkLongTerm
            // 
            this.chkLongTerm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLongTerm.AutoSize = true;
            this.chkLongTerm.Checked = true;
            this.chkLongTerm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLongTerm.Location = new System.Drawing.Point(507, 0);
            this.chkLongTerm.Name = "chkLongTerm";
            this.chkLongTerm.Size = new System.Drawing.Size(48, 16);
            this.chkLongTerm.TabIndex = 1;
            this.chkLongTerm.Text = "长期";
            this.chkLongTerm.UseVisualStyleBackColor = true;
            this.chkLongTerm.CheckedChanged += new System.EventHandler(this.chkLongTerm_CheckedChanged);
            // 
            // chkTempTerm
            // 
            this.chkTempTerm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTempTerm.AutoSize = true;
            this.chkTempTerm.Checked = true;
            this.chkTempTerm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTempTerm.Location = new System.Drawing.Point(570, 0);
            this.chkTempTerm.Name = "chkTempTerm";
            this.chkTempTerm.Size = new System.Drawing.Size(48, 16);
            this.chkTempTerm.TabIndex = 2;
            this.chkTempTerm.Text = "临时";
            this.chkTempTerm.UseVisualStyleBackColor = true;
            this.chkTempTerm.CheckedChanged += new System.EventHandler(this.chkLongTerm_CheckedChanged);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Checked = true;
            this.chkSelectAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelectAll.Location = new System.Drawing.Point(3, 0);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(48, 16);
            this.chkSelectAll.TabIndex = 0;
            this.chkSelectAll.Text = "全选";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // dgvOrdersExecute
            // 
            this.dgvOrdersExecute.AllowUserToAddRows = false;
            this.dgvOrdersExecute.AllowUserToDeleteRows = false;
            this.dgvOrdersExecute.AllowUserToResizeRows = false;
            this.dgvOrdersExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOrdersExecute.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOrdersExecute.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvOrdersExecute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdersExecute.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PATIENT_ID,
            this.SELECTION,
            this.BED_NO,
            this.PATIENT_NAME,
            this.VISIT_ID,
            this.ORDER_NO,
            this.ORDER_SUB_NO,
            this.REPEAT_INDICATOR,
            this.ORDER_TEXT,
            this.ORDER_CODE,
            this.DOSAGE,
            this.DOSAGE_UNITS,
            this.FREQUENCY,
            this.ADMINISTRATION,
            this.SCHEDULE_PERFORM_TIME});
            this.dgvOrdersExecute.Location = new System.Drawing.Point(3, 22);
            this.dgvOrdersExecute.MultiSelect = false;
            this.dgvOrdersExecute.Name = "dgvOrdersExecute";
            this.dgvOrdersExecute.RowHeadersVisible = false;
            this.dgvOrdersExecute.RowTemplate.Height = 23;
            this.dgvOrdersExecute.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdersExecute.Size = new System.Drawing.Size(617, 302);
            this.dgvOrdersExecute.TabIndex = 3;
            this.dgvOrdersExecute.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdersExecute_CellContentDoubleClick);
            this.dgvOrdersExecute.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvOrdersExecute_DataBindingComplete);
            this.dgvOrdersExecute.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdersExecute_CellContentClick);
            // 
            // PATIENT_ID
            // 
            this.PATIENT_ID.DataPropertyName = "PATIENT_ID";
            this.PATIENT_ID.HeaderText = "病人标识号";
            this.PATIENT_ID.Name = "PATIENT_ID";
            this.PATIENT_ID.ReadOnly = true;
            this.PATIENT_ID.Visible = false;
            this.PATIENT_ID.Width = 80;
            // 
            // SELECTION
            // 
            this.SELECTION.DataPropertyName = "SELECTION";
            this.SELECTION.FalseValue = "0";
            this.SELECTION.HeaderText = "";
            this.SELECTION.Name = "SELECTION";
            this.SELECTION.TrueValue = "1";
            this.SELECTION.Width = 30;
            // 
            // BED_NO
            // 
            this.BED_NO.DataPropertyName = "BED_NO";
            this.BED_NO.HeaderText = "床号";
            this.BED_NO.Name = "BED_NO";
            this.BED_NO.Width = 60;
            // 
            // PATIENT_NAME
            // 
            this.PATIENT_NAME.DataPropertyName = "PATIENT_ID";
            this.PATIENT_NAME.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.PATIENT_NAME.HeaderText = "姓名";
            this.PATIENT_NAME.Name = "PATIENT_NAME";
            this.PATIENT_NAME.ReadOnly = true;
            this.PATIENT_NAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PATIENT_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.PATIENT_NAME.Width = 70;
            // 
            // VISIT_ID
            // 
            this.VISIT_ID.HeaderText = "VISIT_ID";
            this.VISIT_ID.Name = "VISIT_ID";
            this.VISIT_ID.ReadOnly = true;
            this.VISIT_ID.Visible = false;
            // 
            // ORDER_NO
            // 
            this.ORDER_NO.DataPropertyName = "ORDER_NO";
            this.ORDER_NO.HeaderText = "医嘱号";
            this.ORDER_NO.Name = "ORDER_NO";
            this.ORDER_NO.ReadOnly = true;
            this.ORDER_NO.Visible = false;
            this.ORDER_NO.Width = 50;
            // 
            // ORDER_SUB_NO
            // 
            this.ORDER_SUB_NO.DataPropertyName = "ORDER_SUB_NO";
            this.ORDER_SUB_NO.HeaderText = "医嘱子序号";
            this.ORDER_SUB_NO.Name = "ORDER_SUB_NO";
            this.ORDER_SUB_NO.ReadOnly = true;
            this.ORDER_SUB_NO.Visible = false;
            // 
            // REPEAT_INDICATOR
            // 
            this.REPEAT_INDICATOR.DataPropertyName = "REPEAT_INDICATOR";
            this.REPEAT_INDICATOR.HeaderText = "长/临";
            this.REPEAT_INDICATOR.Name = "REPEAT_INDICATOR";
            this.REPEAT_INDICATOR.ReadOnly = true;
            this.REPEAT_INDICATOR.Visible = false;
            this.REPEAT_INDICATOR.Width = 50;
            // 
            // ORDER_TEXT
            // 
            this.ORDER_TEXT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ORDER_TEXT.DataPropertyName = "ORDER_TEXT";
            this.ORDER_TEXT.HeaderText = "医嘱正文";
            this.ORDER_TEXT.Name = "ORDER_TEXT";
            this.ORDER_TEXT.ReadOnly = true;
            // 
            // ORDER_CODE
            // 
            this.ORDER_CODE.DataPropertyName = "ORDER_CODE";
            this.ORDER_CODE.HeaderText = "医嘱代码";
            this.ORDER_CODE.Name = "ORDER_CODE";
            this.ORDER_CODE.ReadOnly = true;
            this.ORDER_CODE.Visible = false;
            // 
            // DOSAGE
            // 
            this.DOSAGE.DataPropertyName = "DOSAGE";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DOSAGE.DefaultCellStyle = dataGridViewCellStyle2;
            this.DOSAGE.HeaderText = "剂量";
            this.DOSAGE.Name = "DOSAGE";
            this.DOSAGE.ReadOnly = true;
            this.DOSAGE.Width = 60;
            // 
            // DOSAGE_UNITS
            // 
            this.DOSAGE_UNITS.DataPropertyName = "DOSAGE_UNITS";
            this.DOSAGE_UNITS.HeaderText = "单位";
            this.DOSAGE_UNITS.Name = "DOSAGE_UNITS";
            this.DOSAGE_UNITS.ReadOnly = true;
            this.DOSAGE_UNITS.Width = 40;
            // 
            // FREQUENCY
            // 
            this.FREQUENCY.DataPropertyName = "FREQUENCY";
            this.FREQUENCY.HeaderText = "频次";
            this.FREQUENCY.Name = "FREQUENCY";
            this.FREQUENCY.ReadOnly = true;
            this.FREQUENCY.Width = 60;
            // 
            // ADMINISTRATION
            // 
            this.ADMINISTRATION.DataPropertyName = "ADMINISTRATION";
            this.ADMINISTRATION.HeaderText = "途径";
            this.ADMINISTRATION.Name = "ADMINISTRATION";
            this.ADMINISTRATION.ReadOnly = true;
            this.ADMINISTRATION.Width = 90;
            // 
            // SCHEDULE_PERFORM_TIME
            // 
            this.SCHEDULE_PERFORM_TIME.DataPropertyName = "SCHEDULE_PERFORM_TIME";
            dataGridViewCellStyle3.Format = "g";
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SCHEDULE_PERFORM_TIME.DefaultCellStyle = dataGridViewCellStyle3;
            this.SCHEDULE_PERFORM_TIME.HeaderText = "计划执行时间";
            this.SCHEDULE_PERFORM_TIME.Name = "SCHEDULE_PERFORM_TIME";
            this.SCHEDULE_PERFORM_TIME.ReadOnly = true;
            this.SCHEDULE_PERFORM_TIME.Width = 110;
            // 
            // grpAdmin
            // 
            this.grpAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAdmin.Controls.Add(this.lvwAdministration);
            this.grpAdmin.Location = new System.Drawing.Point(213, 395);
            this.grpAdmin.Name = "grpAdmin";
            this.grpAdmin.Size = new System.Drawing.Size(623, 52);
            this.grpAdmin.TabIndex = 3;
            this.grpAdmin.TabStop = false;
            // 
            // lvwAdministration
            // 
            this.lvwAdministration.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwAdministration.CheckBoxes = true;
            this.lvwAdministration.Location = new System.Drawing.Point(3, 9);
            this.lvwAdministration.Name = "lvwAdministration";
            this.lvwAdministration.Size = new System.Drawing.Size(616, 40);
            this.lvwAdministration.TabIndex = 0;
            this.lvwAdministration.UseCompatibleStateImageBehavior = false;
            this.lvwAdministration.View = System.Windows.Forms.View.SmallIcon;
            this.lvwAdministration.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvwAdministration_ItemChecked);
            // 
            // OrdersExecuteBilFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 501);
            this.Controls.Add(this.grpMain);
            this.Controls.Add(this.grpAdmin);
            this.Controls.Add(this.grpPatientInfo);
            this.Controls.Add(this.grpFunction);
            this.Controls.Add(this.grpPatientList);
            this.Name = "OrdersExecuteBilFrm";
            this.Text = "医嘱执行单";
            this.Load += new System.EventHandler(this.OrdersExecuteBilFrm_Load);
            this.grpFunction.ResumeLayout(false);
            this.grpFunction.PerformLayout();
            this.grpPatientList.ResumeLayout(false);
            this.grpPatientList.PerformLayout();
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdersExecute)).EndInit();
            this.grpAdmin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl grpPatientInfo;
        private DevExpress.XtraEditors.PanelControl grpFunction;
        private HISPlus.UserControls.UcButton btnExit;
        private System.Windows.Forms.DateTimePicker dtRngStart;
        private DevExpress.XtraEditors.LabelControl label1;
        private DevExpress.XtraEditors.PanelControl grpPatientList;
        private DevExpress.XtraEditors.PanelControl grpMain;
        private HISPlus.UserControls.UcButton btnPrint;
        private System.Windows.Forms.DataGridView dgvOrdersExecute;
        private HISPlus.UserControls.UcButton btnQuery;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.CheckBox chkLongTerm;
        private System.Windows.Forms.CheckBox chkTempTerm;
        private DevExpress.XtraEditors.LabelControl label2;
        private System.Windows.Forms.DateTimePicker dtpTime1;
        private System.Windows.Forms.DateTimePicker dtpTime0;
        private DevExpress.XtraEditors.PanelControl grpAdmin;
        private System.Windows.Forms.ListView lvwAdministration;
        private System.Windows.Forms.CheckBox chkNotPrinted;
        private System.Windows.Forms.CheckBox chkPrinted;
        private DevExpress.XtraEditors.LabelControl lblInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PATIENT_ID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SELECTION;
        private System.Windows.Forms.DataGridViewTextBoxColumn BED_NO;
        private System.Windows.Forms.DataGridViewComboBoxColumn PATIENT_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn VISIT_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_SUB_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn REPEAT_INDICATOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_TEXT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOSAGE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOSAGE_UNITS;
        private System.Windows.Forms.DataGridViewTextBoxColumn FREQUENCY;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADMINISTRATION;
        private System.Windows.Forms.DataGridViewTextBoxColumn SCHEDULE_PERFORM_TIME;
    }
}