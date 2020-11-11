namespace HISPlus
{
    partial class OrdersExecuteFrm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpFunction = new DevExpress.XtraEditors.PanelControl();
            this.dtRngEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.btnPrint = new HISPlus.UserControls.UcButton();
            this.btnExit = new HISPlus.UserControls.UcButton();
            this.dtRngStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery = new HISPlus.UserControls.UcButton();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.grpMain = new DevExpress.XtraEditors.PanelControl();
            this.lblCount = new DevExpress.XtraEditors.LabelControl();
            this.chkExecuted = new System.Windows.Forms.CheckBox();
            this.chkNotExecuted = new System.Windows.Forms.CheckBox();
            this.lblInfo = new DevExpress.XtraEditors.LabelControl();
            this.chkLongTerm = new System.Windows.Forms.CheckBox();
            this.chkTempTerm = new System.Windows.Forms.CheckBox();
            this.dgvOrdersExecute = new System.Windows.Forms.DataGridView();
            this.PATIENT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SELECTION = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BED_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PATIENT_NAME = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.VISIT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDER_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDER_NO_SHOW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDER_SUB_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REPEAT_INDICATOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDER_TEXT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDER_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOSAGE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOSAGE_UNITS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FREQUENCY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ADMINISTRATION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SCHEDULE_PERFORM_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EXECUTE_DATE_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EXECUTE_NURSE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmnuOrdersExecute = new System.Windows.Forms.ContextMenuStrip();
            this.cmnuOrdersExecute_Execute = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuOrdersExecute_CancelExecute = new System.Windows.Forms.ToolStripMenuItem();
            this.grpAdmin = new DevExpress.XtraEditors.PanelControl();
            this.lvwAdministration = new System.Windows.Forms.ListView();
            this.grpFunction.SuspendLayout();
            this.grpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdersExecute)).BeginInit();
            this.cmnuOrdersExecute.SuspendLayout();
            this.grpAdmin.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpFunction
            // 
            this.grpFunction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFunction.Controls.Add(this.chkAll);
            this.grpFunction.Controls.Add(this.dtRngEnd);
            this.grpFunction.Controls.Add(this.label2);
            this.grpFunction.Controls.Add(this.btnPrint);
            this.grpFunction.Controls.Add(this.btnExit);
            this.grpFunction.Controls.Add(this.dtRngStart);
            this.grpFunction.Controls.Add(this.label1);
            this.grpFunction.Controls.Add(this.btnQuery);
            this.grpFunction.Location = new System.Drawing.Point(13, 664);
            this.grpFunction.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpFunction.Name = "grpFunction";
            this.grpFunction.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpFunction.Size = new System.Drawing.Size(1102, 69);
            this.grpFunction.TabIndex = 2;
            this.grpFunction.TabStop = false;
            // 
            // dtRngEnd
            // 
            this.dtRngEnd.CustomFormat = "yy-MM-dd HH:mm";
            this.dtRngEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtRngEnd.Location = new System.Drawing.Point(338, 24);
            this.dtRngEnd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtRngEnd.Name = "dtRngEnd";
            this.dtRngEnd.Size = new System.Drawing.Size(144, 26);
            this.dtRngEnd.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(316, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "-";
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Enabled = false;
            this.btnPrint.Location = new System.Drawing.Point(823, 15);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(120, 45);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.TextValue = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(974, 20);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 45);
            this.btnExit.TabIndex = 4;
            this.btnExit.TextValue = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dtRngStart
            // 
            this.dtRngStart.CustomFormat = "yy-MM-dd HH:mm";
            this.dtRngStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtRngStart.Location = new System.Drawing.Point(164, 24);
            this.dtRngStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtRngStart.Name = "dtRngStart";
            this.dtRngStart.Size = new System.Drawing.Size(143, 26);
            this.dtRngStart.TabIndex = 1;
            this.dtRngStart.ValueChanged += new System.EventHandler(this.dtRngStart_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(117, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "日期";
            // 
            // btnQuery
            // 
            this.btnQuery.Enabled = false;
            this.btnQuery.Location = new System.Drawing.Point(504, 15);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(120, 45);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.TextValue = "查询(&Q)";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(23, 26);
            this.chkAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(60, 22);
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
            this.grpMain.Controls.Add(this.lblCount);
            this.grpMain.Controls.Add(this.chkExecuted);
            this.grpMain.Controls.Add(this.chkNotExecuted);
            this.grpMain.Controls.Add(this.lblInfo);
            this.grpMain.Controls.Add(this.chkLongTerm);
            this.grpMain.Controls.Add(this.chkTempTerm);
            this.grpMain.Controls.Add(this.dgvOrdersExecute);
            this.grpMain.Location = new System.Drawing.Point(13, 13);
            this.grpMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpMain.Name = "grpMain";
            this.grpMain.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpMain.Size = new System.Drawing.Size(1102, 579);
            this.grpMain.TabIndex = 2;
            this.grpMain.TabStop = false;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.ForeColor = System.Drawing.Color.Blue;
            this.lblCount.Location = new System.Drawing.Point(515, 2);
            this.lblCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 18);
            this.lblCount.TabIndex = 9;
            // 
            // chkExecuted
            // 
            this.chkExecuted.AutoSize = true;
            this.chkExecuted.Location = new System.Drawing.Point(96, -2);
            this.chkExecuted.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkExecuted.Name = "chkExecuted";
            this.chkExecuted.Size = new System.Drawing.Size(75, 22);
            this.chkExecuted.TabIndex = 8;
            this.chkExecuted.Text = "已执行";
            this.chkExecuted.UseVisualStyleBackColor = true;
            this.chkExecuted.CheckedChanged += new System.EventHandler(this.chkLongTerm_CheckedChanged);
            // 
            // chkNotExecuted
            // 
            this.chkNotExecuted.AutoSize = true;
            this.chkNotExecuted.Checked = true;
            this.chkNotExecuted.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNotExecuted.Location = new System.Drawing.Point(8, -2);
            this.chkNotExecuted.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkNotExecuted.Name = "chkNotExecuted";
            this.chkNotExecuted.Size = new System.Drawing.Size(75, 22);
            this.chkNotExecuted.TabIndex = 7;
            this.chkNotExecuted.Text = "未执行";
            this.chkNotExecuted.UseVisualStyleBackColor = true;
            this.chkNotExecuted.CheckedChanged += new System.EventHandler(this.chkLongTerm_CheckedChanged);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(279, 2);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 18);
            this.lblInfo.TabIndex = 6;
            // 
            // chkLongTerm
            // 
            this.chkLongTerm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLongTerm.AutoSize = true;
            this.chkLongTerm.Checked = true;
            this.chkLongTerm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLongTerm.Location = new System.Drawing.Point(951, -2);
            this.chkLongTerm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkLongTerm.Name = "chkLongTerm";
            this.chkLongTerm.Size = new System.Drawing.Size(60, 22);
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
            this.chkTempTerm.Location = new System.Drawing.Point(1035, -2);
            this.chkTempTerm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkTempTerm.Name = "chkTempTerm";
            this.chkTempTerm.Size = new System.Drawing.Size(60, 22);
            this.chkTempTerm.TabIndex = 2;
            this.chkTempTerm.Text = "临时";
            this.chkTempTerm.UseVisualStyleBackColor = true;
            this.chkTempTerm.CheckedChanged += new System.EventHandler(this.chkLongTerm_CheckedChanged);
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9F);
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
            this.ORDER_NO_SHOW,
            this.ORDER_SUB_NO,
            this.REPEAT_INDICATOR,
            this.ORDER_TEXT,
            this.ORDER_CODE,
            this.DOSAGE,
            this.DOSAGE_UNITS,
            this.FREQUENCY,
            this.ADMINISTRATION,
            this.SCHEDULE_PERFORM_TIME,
            this.EXECUTE_DATE_TIME,
            this.EXECUTE_NURSE});
            this.dgvOrdersExecute.ContextMenuStrip = this.cmnuOrdersExecute;
            this.dgvOrdersExecute.Location = new System.Drawing.Point(4, 32);
            this.dgvOrdersExecute.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvOrdersExecute.MultiSelect = false;
            this.dgvOrdersExecute.Name = "dgvOrdersExecute";
            this.dgvOrdersExecute.RowHeadersVisible = false;
            this.dgvOrdersExecute.RowTemplate.Height = 23;
            this.dgvOrdersExecute.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdersExecute.Size = new System.Drawing.Size(1094, 543);
            this.dgvOrdersExecute.TabIndex = 3;
            this.dgvOrdersExecute.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdersExecute_CellEnter);
            this.dgvOrdersExecute.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrdersExecute_CellLeave);
            this.dgvOrdersExecute.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvOrdersExecute_DataBindingComplete);
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
            this.SELECTION.Visible = false;
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
            this.VISIT_ID.DataPropertyName = "VISIT_ID";
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
            this.ORDER_NO.Width = 35;
            // 
            // ORDER_NO_SHOW
            // 
            this.ORDER_NO_SHOW.HeaderText = "";
            this.ORDER_NO_SHOW.Name = "ORDER_NO_SHOW";
            this.ORDER_NO_SHOW.Width = 30;
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
            this.ORDER_TEXT.DataPropertyName = "ORDER_TEXT";
            this.ORDER_TEXT.HeaderText = "医嘱正文";
            this.ORDER_TEXT.Name = "ORDER_TEXT";
            this.ORDER_TEXT.ReadOnly = true;
            this.ORDER_TEXT.Width = 194;
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
            // EXECUTE_DATE_TIME
            // 
            this.EXECUTE_DATE_TIME.DataPropertyName = "EXECUTE_DATE_TIME";
            dataGridViewCellStyle4.Format = "g";
            dataGridViewCellStyle4.NullValue = null;
            this.EXECUTE_DATE_TIME.DefaultCellStyle = dataGridViewCellStyle4;
            this.EXECUTE_DATE_TIME.HeaderText = "实际执行时间";
            this.EXECUTE_DATE_TIME.Name = "EXECUTE_DATE_TIME";
            this.EXECUTE_DATE_TIME.ReadOnly = true;
            this.EXECUTE_DATE_TIME.Width = 110;
            // 
            // EXECUTE_NURSE
            // 
            this.EXECUTE_NURSE.DataPropertyName = "EXECUTE_NURSE";
            this.EXECUTE_NURSE.HeaderText = "执行人";
            this.EXECUTE_NURSE.Name = "EXECUTE_NURSE";
            this.EXECUTE_NURSE.ReadOnly = true;
            this.EXECUTE_NURSE.Width = 70;
            // 
            // cmnuOrdersExecute
            // 
            this.cmnuOrdersExecute.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuOrdersExecute_Execute,
            this.cmnuOrdersExecute_CancelExecute});
            this.cmnuOrdersExecute.Name = "cmnuOrdersExecute";
            this.cmnuOrdersExecute.Size = new System.Drawing.Size(139, 52);
            // 
            // cmnuOrdersExecute_Execute
            // 
            this.cmnuOrdersExecute_Execute.Name = "cmnuOrdersExecute_Execute";
            this.cmnuOrdersExecute_Execute.Size = new System.Drawing.Size(138, 24);
            this.cmnuOrdersExecute_Execute.Text = "执行";
            this.cmnuOrdersExecute_Execute.Click += new System.EventHandler(this.cmnuOrdersExecute_Execute_Click);
            // 
            // cmnuOrdersExecute_CancelExecute
            // 
            this.cmnuOrdersExecute_CancelExecute.Name = "cmnuOrdersExecute_CancelExecute";
            this.cmnuOrdersExecute_CancelExecute.Size = new System.Drawing.Size(138, 24);
            this.cmnuOrdersExecute_CancelExecute.Text = "撤销执行";
            this.cmnuOrdersExecute_CancelExecute.Click += new System.EventHandler(this.cmnuOrdersExecute_CancelExecute_Click);
            // 
            // grpAdmin
            // 
            this.grpAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAdmin.Controls.Add(this.lvwAdministration);
            this.grpAdmin.Location = new System.Drawing.Point(13, 592);
            this.grpAdmin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpAdmin.Name = "grpAdmin";
            this.grpAdmin.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpAdmin.Size = new System.Drawing.Size(1102, 78);
            this.grpAdmin.TabIndex = 3;
            this.grpAdmin.TabStop = false;
            // 
            // lvwAdministration
            // 
            this.lvwAdministration.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwAdministration.CheckBoxes = true;
            this.lvwAdministration.Location = new System.Drawing.Point(4, 9);
            this.lvwAdministration.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lvwAdministration.Name = "lvwAdministration";
            this.lvwAdministration.Size = new System.Drawing.Size(1091, 58);
            this.lvwAdministration.TabIndex = 1;
            this.lvwAdministration.UseCompatibleStateImageBehavior = false;
            this.lvwAdministration.View = System.Windows.Forms.View.SmallIcon;
            // 
            // OrdersExecuteFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 752);
            this.Controls.Add(this.grpMain);
            this.Controls.Add(this.grpAdmin);
            this.Controls.Add(this.grpFunction);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "OrdersExecuteFrm";
            this.Text = "医嘱执行单";
            this.Load += new System.EventHandler(this.OrdersExecuteBilFrm_Load);
            this.grpFunction.ResumeLayout(false);
            this.grpFunction.PerformLayout();
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdersExecute)).EndInit();
            this.cmnuOrdersExecute.ResumeLayout(false);
            this.grpAdmin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl grpFunction;
        private HISPlus.UserControls.UcButton btnExit;
        private System.Windows.Forms.DateTimePicker dtRngStart;
        private DevExpress.XtraEditors.LabelControl label1;
        private DevExpress.XtraEditors.PanelControl grpMain;
        private HISPlus.UserControls.UcButton btnPrint;
        private System.Windows.Forms.DataGridView dgvOrdersExecute;
        private HISPlus.UserControls.UcButton btnQuery;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.CheckBox chkLongTerm;
        private System.Windows.Forms.CheckBox chkTempTerm;
        private DevExpress.XtraEditors.LabelControl label2;
        private DevExpress.XtraEditors.LabelControl lblInfo;
        private System.Windows.Forms.DateTimePicker dtRngEnd;
        private DevExpress.XtraEditors.PanelControl grpAdmin;
        private System.Windows.Forms.ListView lvwAdministration;
        private System.Windows.Forms.CheckBox chkNotExecuted;
        private System.Windows.Forms.CheckBox chkExecuted;
        private DevExpress.XtraEditors.LabelControl lblCount;
        private System.Windows.Forms.ContextMenuStrip cmnuOrdersExecute;
        private System.Windows.Forms.ToolStripMenuItem cmnuOrdersExecute_Execute;
        private System.Windows.Forms.ToolStripMenuItem cmnuOrdersExecute_CancelExecute;
        private System.Windows.Forms.DataGridViewTextBoxColumn PATIENT_ID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SELECTION;
        private System.Windows.Forms.DataGridViewTextBoxColumn BED_NO;
        private System.Windows.Forms.DataGridViewComboBoxColumn PATIENT_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn VISIT_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_NO_SHOW;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_SUB_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn REPEAT_INDICATOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_TEXT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOSAGE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOSAGE_UNITS;
        private System.Windows.Forms.DataGridViewTextBoxColumn FREQUENCY;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADMINISTRATION;
        private System.Windows.Forms.DataGridViewTextBoxColumn SCHEDULE_PERFORM_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn EXECUTE_DATE_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn EXECUTE_NURSE;
    }
}