namespace HISPlus
{
    partial class PIOSettingFrm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new DevExpress.XtraEditors.PanelControl();
            this.label5 = new DevExpress.XtraEditors.LabelControl();
            this.txtFilter = new HISPlus.UserControls.UcTextBox();
            this.dgvNursingDiagnosis = new System.Windows.Forms.DataGridView();
            this.NURSING_DIAG_NAME = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.grpButton = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new HISPlus.UserControls.UcButton();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.btnDelete = new HISPlus.UserControls.UcButton();
            this.groupBox5 = new DevExpress.XtraEditors.PanelControl();
            this.txtMeasure = new HISPlus.UserControls.UcTextArea();
            this.grpAdd = new DevExpress.XtraEditors.PanelControl();
            this.txtMeasureSearch = new HISPlus.UserControls.UcTextBox();
            this.txtOjective = new HISPlus.UserControls.UcTextBox();
            this.txtDiagnosis = new HISPlus.UserControls.UcTextBox();
            this.label4 = new DevExpress.XtraEditors.LabelControl();
            this.btnNew = new HISPlus.UserControls.UcButton();
            this.cmbMeasureType = new HISPlus.UserControls.UcComboBox();
            this.cmbMeasure = new HISPlus.UserControls.UcComboBox();
            this.label3 = new DevExpress.XtraEditors.LabelControl();
            this.cmbOjective = new HISPlus.UserControls.UcComboBox();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbDiagnosis = new HISPlus.UserControls.UcComboBox();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.dgvNursingMeasure = new System.Windows.Forms.DataGridView();
            this.NURSING_MEASURE_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NURSING_MEASURE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MEASURE_DICT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MEASURE_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new DevExpress.XtraEditors.PanelControl();
            this.dgvNursingObjective = new System.Windows.Forms.DataGridView();
            this.NURSING_OBJECTIVE = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.groupBox2 = new DevExpress.XtraEditors.PanelControl();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNursingDiagnosis)).BeginInit();
            this.grpButton.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.grpAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNursingMeasure)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNursingObjective)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtFilter);
            this.groupBox1.Controls.Add(this.dgvNursingDiagnosis);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(228, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 276);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "护理诊断";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "检索";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(41, 20);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(156, 21);
            this.txtFilter.TabIndex = 1;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // dgvNursingDiagnosis
            // 
            this.dgvNursingDiagnosis.AllowUserToAddRows = false;
            this.dgvNursingDiagnosis.AllowUserToDeleteRows = false;
            this.dgvNursingDiagnosis.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.dgvNursingDiagnosis.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNursingDiagnosis.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNursingDiagnosis.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNursingDiagnosis.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvNursingDiagnosis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNursingDiagnosis.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NURSING_DIAG_NAME});
            this.dgvNursingDiagnosis.Location = new System.Drawing.Point(3, 42);
            this.dgvNursingDiagnosis.MultiSelect = false;
            this.dgvNursingDiagnosis.Name = "dgvNursingDiagnosis";
            this.dgvNursingDiagnosis.ReadOnly = true;
            this.dgvNursingDiagnosis.RowHeadersVisible = false;
            this.dgvNursingDiagnosis.RowTemplate.Height = 23;
            this.dgvNursingDiagnosis.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNursingDiagnosis.Size = new System.Drawing.Size(194, 231);
            this.dgvNursingDiagnosis.TabIndex = 2;
            this.dgvNursingDiagnosis.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvNursingDiagnosis_DataError);
            this.dgvNursingDiagnosis.SelectionChanged += new System.EventHandler(this.dgvNursingDiagnosis_SelectionChanged);
            // 
            // NURSING_DIAG_NAME
            // 
            this.NURSING_DIAG_NAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NURSING_DIAG_NAME.DataPropertyName = "ITEM_NAME";
            this.NURSING_DIAG_NAME.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.NURSING_DIAG_NAME.HeaderText = "护理诊断";
            this.NURSING_DIAG_NAME.Name = "NURSING_DIAG_NAME";
            this.NURSING_DIAG_NAME.ReadOnly = true;
            this.NURSING_DIAG_NAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NURSING_DIAG_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // grpButton
            // 
            this.grpButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpButton.Controls.Add(this.btnExit);
            this.grpButton.Controls.Add(this.btnSave);
            this.grpButton.Controls.Add(this.btnDelete);
            this.grpButton.Location = new System.Drawing.Point(228, 456);
            this.grpButton.Name = "grpButton";
            this.grpButton.Size = new System.Drawing.Size(616, 53);
            this.grpButton.TabIndex = 5;
            this.grpButton.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(520, 16);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 30);
            this.btnExit.TabIndex = 2;
            this.btnExit.TextValue = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(424, 17);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 1;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(8, 17);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 30);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.TextValue = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.txtMeasure);
            this.groupBox5.Location = new System.Drawing.Point(228, 292);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(616, 158);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "备注";
            // 
            // txtMeasure
            // 
            this.txtMeasure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMeasure.Location = new System.Drawing.Point(3, 17);
            this.txtMeasure.Multiline = true;
            this.txtMeasure.Name = "txtMeasure";
            this.txtMeasure.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMeasure.Size = new System.Drawing.Size(610, 138);
            this.txtMeasure.TabIndex = 0;
            this.txtMeasure.TextChanged += new System.EventHandler(this.txtMeasure_TextChanged);
            // 
            // grpAdd
            // 
            this.grpAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grpAdd.Controls.Add(this.txtMeasureSearch);
            this.grpAdd.Controls.Add(this.txtOjective);
            this.grpAdd.Controls.Add(this.txtDiagnosis);
            this.grpAdd.Controls.Add(this.label4);
            this.grpAdd.Controls.Add(this.btnNew);
            this.grpAdd.Controls.Add(this.cmbMeasureType);
            this.grpAdd.Controls.Add(this.cmbMeasure);
            this.grpAdd.Controls.Add(this.label3);
            this.grpAdd.Controls.Add(this.cmbOjective);
            this.grpAdd.Controls.Add(this.label2);
            this.grpAdd.Controls.Add(this.cmbDiagnosis);
            this.grpAdd.Controls.Add(this.label1);
            this.grpAdd.Location = new System.Drawing.Point(12, 10);
            this.grpAdd.Name = "grpAdd";
            this.grpAdd.Size = new System.Drawing.Size(200, 499);
            this.grpAdd.TabIndex = 0;
            this.grpAdd.TabStop = false;
            // 
            // txtMeasureSearch
            // 
            this.txtMeasureSearch.Location = new System.Drawing.Point(73, 196);
            this.txtMeasureSearch.Name = "txtMeasureSearch";
            this.txtMeasureSearch.Size = new System.Drawing.Size(121, 21);
            this.txtMeasureSearch.TabIndex = 9;
            this.txtMeasureSearch.TextChanged += new System.EventHandler(this.txtMeasureSearch_TextChanged);
            this.txtMeasureSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMeasureSearch_KeyDown);
            // 
            // txtOjective
            // 
            this.txtOjective.Location = new System.Drawing.Point(73, 81);
            this.txtOjective.Name = "txtOjective";
            this.txtOjective.Size = new System.Drawing.Size(121, 21);
            this.txtOjective.TabIndex = 4;
            this.txtOjective.TextChanged += new System.EventHandler(this.txtOjective_TextChanged);
            this.txtOjective.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOjective_KeyDown);
            // 
            // txtDiagnosis
            // 
            this.txtDiagnosis.Location = new System.Drawing.Point(73, 20);
            this.txtDiagnosis.Name = "txtDiagnosis";
            this.txtDiagnosis.Size = new System.Drawing.Size(121, 21);
            this.txtDiagnosis.TabIndex = 1;
            this.txtDiagnosis.TextChanged += new System.EventHandler(this.txtDiagnosis_TextChanged);
            this.txtDiagnosis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDiagnosis_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 202);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "措施名称";
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(104, 258);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(90, 30);
            this.btnNew.TabIndex = 11;
            this.btnNew.TextValue = "新增";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // cmbMeasureType
            // 
            this.cmbMeasureType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMeasureType.FormattingEnabled = true;
            this.cmbMeasureType.Items.AddRange(new object[] {
            "观察",
            "治疗",
            "教育"});
            this.cmbMeasureType.Location = new System.Drawing.Point(73, 148);
            this.cmbMeasureType.Name = "cmbMeasureType";
            this.cmbMeasureType.Size = new System.Drawing.Size(121, 20);
            this.cmbMeasureType.TabIndex = 7;
            this.cmbMeasureType.SelectedIndexChanged += new System.EventHandler(this.cmbMeasureType_SelectedIndexChanged);
            // 
            // cmbMeasure
            // 
            this.cmbMeasure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMeasure.FormattingEnabled = true;
            this.cmbMeasure.Location = new System.Drawing.Point(73, 218);
            this.cmbMeasure.MaxDropDownItems = 24;
            this.cmbMeasure.Name = "cmbMeasure";
            this.cmbMeasure.Size = new System.Drawing.Size(121, 20);
            this.cmbMeasure.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "措施类型";
            // 
            // cmbOjective
            // 
            this.cmbOjective.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOjective.FormattingEnabled = true;
            this.cmbOjective.Location = new System.Drawing.Point(73, 103);
            this.cmbOjective.MaxDropDownItems = 24;
            this.cmbOjective.Name = "cmbOjective";
            this.cmbOjective.Size = new System.Drawing.Size(121, 20);
            this.cmbOjective.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "预期目标";
            // 
            // cmbDiagnosis
            // 
            this.cmbDiagnosis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiagnosis.FormattingEnabled = true;
            this.cmbDiagnosis.Location = new System.Drawing.Point(73, 43);
            this.cmbDiagnosis.MaxDropDownItems = 24;
            this.cmbDiagnosis.Name = "cmbDiagnosis";
            this.cmbDiagnosis.Size = new System.Drawing.Size(121, 20);
            this.cmbDiagnosis.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "护理诊断";
            // 
            // dgvNursingMeasure
            // 
            this.dgvNursingMeasure.AllowUserToAddRows = false;
            this.dgvNursingMeasure.AllowUserToDeleteRows = false;
            this.dgvNursingMeasure.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.dgvNursingMeasure.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvNursingMeasure.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvNursingMeasure.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNursingMeasure.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvNursingMeasure.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNursingMeasure.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NURSING_MEASURE_TYPE,
            this.NURSING_MEASURE,
            this.MEASURE_DICT_ID,
            this.MEASURE_ID});
            this.dgvNursingMeasure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNursingMeasure.Location = new System.Drawing.Point(3, 17);
            this.dgvNursingMeasure.Name = "dgvNursingMeasure";
            this.dgvNursingMeasure.ReadOnly = true;
            this.dgvNursingMeasure.RowHeadersVisible = false;
            this.dgvNursingMeasure.RowTemplate.Height = 23;
            this.dgvNursingMeasure.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNursingMeasure.Size = new System.Drawing.Size(206, 256);
            this.dgvNursingMeasure.TabIndex = 0;
            this.dgvNursingMeasure.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvNursingMeasure_DataError);
            this.dgvNursingMeasure.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvNursingMeasure_DataBindingComplete);
            this.dgvNursingMeasure.SelectionChanged += new System.EventHandler(this.dgvNursingMeasure_SelectionChanged);
            // 
            // NURSING_MEASURE_TYPE
            // 
            this.NURSING_MEASURE_TYPE.HeaderText = "类别";
            this.NURSING_MEASURE_TYPE.Name = "NURSING_MEASURE_TYPE";
            this.NURSING_MEASURE_TYPE.ReadOnly = true;
            this.NURSING_MEASURE_TYPE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NURSING_MEASURE_TYPE.Width = 70;
            // 
            // NURSING_MEASURE
            // 
            this.NURSING_MEASURE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NURSING_MEASURE.HeaderText = "标题";
            this.NURSING_MEASURE.Name = "NURSING_MEASURE";
            this.NURSING_MEASURE.ReadOnly = true;
            // 
            // MEASURE_DICT_ID
            // 
            this.MEASURE_DICT_ID.DataPropertyName = "MEASURE_DICT_ID";
            this.MEASURE_DICT_ID.HeaderText = "MEASURE_DICT_ID";
            this.MEASURE_DICT_ID.Name = "MEASURE_DICT_ID";
            this.MEASURE_DICT_ID.ReadOnly = true;
            this.MEASURE_DICT_ID.Visible = false;
            // 
            // MEASURE_ID
            // 
            this.MEASURE_ID.DataPropertyName = "MEASURE_ID";
            this.MEASURE_ID.HeaderText = "MEASURE_ID";
            this.MEASURE_ID.Name = "MEASURE_ID";
            this.MEASURE_ID.ReadOnly = true;
            this.MEASURE_ID.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dgvNursingMeasure);
            this.groupBox3.Location = new System.Drawing.Point(626, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(212, 276);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "护理措施";
            // 
            // dgvNursingObjective
            // 
            this.dgvNursingObjective.AllowUserToAddRows = false;
            this.dgvNursingObjective.AllowUserToDeleteRows = false;
            this.dgvNursingObjective.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.dgvNursingObjective.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvNursingObjective.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNursingObjective.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvNursingObjective.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNursingObjective.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NURSING_OBJECTIVE});
            this.dgvNursingObjective.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNursingObjective.Location = new System.Drawing.Point(3, 17);
            this.dgvNursingObjective.MultiSelect = false;
            this.dgvNursingObjective.Name = "dgvNursingObjective";
            this.dgvNursingObjective.ReadOnly = true;
            this.dgvNursingObjective.RowHeadersVisible = false;
            this.dgvNursingObjective.RowTemplate.Height = 23;
            this.dgvNursingObjective.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNursingObjective.Size = new System.Drawing.Size(180, 256);
            this.dgvNursingObjective.TabIndex = 0;
            this.dgvNursingObjective.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvNursingObjective_DataError);
            this.dgvNursingObjective.SelectionChanged += new System.EventHandler(this.dgvNursingObjective_SelectionChanged);
            // 
            // NURSING_OBJECTIVE
            // 
            this.NURSING_OBJECTIVE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NURSING_OBJECTIVE.DataPropertyName = "ITEM_NAME";
            this.NURSING_OBJECTIVE.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.NURSING_OBJECTIVE.HeaderText = "护理预期目标";
            this.NURSING_OBJECTIVE.Name = "NURSING_OBJECTIVE";
            this.NURSING_OBJECTIVE.ReadOnly = true;
            this.NURSING_OBJECTIVE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NURSING_OBJECTIVE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvNursingObjective);
            this.groupBox2.Location = new System.Drawing.Point(434, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(186, 276);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "护理目标";
            // 
            // PIOSettingFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 521);
            this.Controls.Add(this.grpAdd);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.grpButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "PIOSettingFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "护理诊断设置";
            this.Load += new System.EventHandler(this.PIOSettingFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNursingDiagnosis)).EndInit();
            this.grpButton.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.grpAdd.ResumeLayout(false);
            this.grpAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNursingMeasure)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNursingObjective)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl groupBox1;
        private DevExpress.XtraEditors.PanelControl grpButton;
        private HISPlus.UserControls.UcButton btnExit;
        private HISPlus.UserControls.UcButton btnSave;
        private HISPlus.UserControls.UcButton btnDelete;
        private System.Windows.Forms.DataGridView dgvNursingDiagnosis;
        private DevExpress.XtraEditors.PanelControl groupBox5;
        private HISPlus.UserControls.UcTextArea txtMeasure;
        private DevExpress.XtraEditors.PanelControl grpAdd;
        private HISPlus.UserControls.UcComboBox cmbMeasure;
        private DevExpress.XtraEditors.LabelControl label3;
        private HISPlus.UserControls.UcComboBox cmbOjective;
        private DevExpress.XtraEditors.LabelControl label2;
        private HISPlus.UserControls.UcComboBox cmbDiagnosis;
        private DevExpress.XtraEditors.LabelControl label1;
        private HISPlus.UserControls.UcComboBox cmbMeasureType;
        private DevExpress.XtraEditors.LabelControl label4;
        private HISPlus.UserControls.UcButton btnNew;
        private System.Windows.Forms.DataGridView dgvNursingMeasure;
        private DevExpress.XtraEditors.PanelControl groupBox3;
        private System.Windows.Forms.DataGridView dgvNursingObjective;
        private DevExpress.XtraEditors.PanelControl groupBox2;
        private System.Windows.Forms.DataGridViewComboBoxColumn NURSING_DIAG_NAME;
        private System.Windows.Forms.DataGridViewComboBoxColumn NURSING_OBJECTIVE;
        private System.Windows.Forms.DataGridViewTextBoxColumn NURSING_MEASURE_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn NURSING_MEASURE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MEASURE_DICT_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn MEASURE_ID;
        private DevExpress.XtraEditors.LabelControl label5;
        private HISPlus.UserControls.UcTextBox txtFilter;
        private HISPlus.UserControls.UcTextBox txtDiagnosis;
        private HISPlus.UserControls.UcTextBox txtMeasureSearch;
        private HISPlus.UserControls.UcTextBox txtOjective;
    }
}