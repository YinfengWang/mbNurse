namespace HISPlus
{
    partial class NurseItemManagerFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NurseItemManagerFrm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvDept = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DEPT_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DEPT_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.txtValRng = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbValType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtShowOrder = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbProperty = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVitalSigns = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtVitalCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtClassCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvNursingItem = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLASS_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VITAL_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VITAL_SIGNS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UNIT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ATTRIBUTE = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SHOW_ORDER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALUE_TYPE = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.VALUE_SCOPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ENABLED = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MEMO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDept)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNursingItem)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.dgvDept);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(214, 493);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "护理单元列表";
            // 
            // dgvDept
            // 
            this.dgvDept.AllowUserToAddRows = false;
            this.dgvDept.AllowUserToDeleteRows = false;
            this.dgvDept.AllowUserToResizeRows = false;
            this.dgvDept.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDept.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.DEPT_CODE,
            this.DEPT_NAME});
            this.dgvDept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDept.Location = new System.Drawing.Point(3, 17);
            this.dgvDept.MultiSelect = false;
            this.dgvDept.Name = "dgvDept";
            this.dgvDept.ReadOnly = true;
            this.dgvDept.RowHeadersVisible = false;
            this.dgvDept.RowTemplate.Height = 23;
            this.dgvDept.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDept.Size = new System.Drawing.Size(208, 473);
            this.dgvDept.TabIndex = 2;
            this.dgvDept.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_RowPostPaint);
            this.dgvDept.SelectionChanged += new System.EventHandler(this.dgvDept_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 36;
            // 
            // DEPT_CODE
            // 
            this.DEPT_CODE.DataPropertyName = "DEPT_CODE";
            this.DEPT_CODE.Frozen = true;
            this.DEPT_CODE.HeaderText = "科室代码";
            this.DEPT_CODE.Name = "DEPT_CODE";
            this.DEPT_CODE.ReadOnly = true;
            this.DEPT_CODE.Width = 60;
            // 
            // DEPT_NAME
            // 
            this.DEPT_NAME.DataPropertyName = "DEPT_NAME";
            this.DEPT_NAME.Frozen = true;
            this.DEPT_NAME.HeaderText = "科室名称";
            this.DEPT_NAME.Name = "DEPT_NAME";
            this.DEPT_NAME.ReadOnly = true;
            this.DEPT_NAME.Width = 140;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.txtMemo);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.chkEnabled);
            this.groupBox2.Controls.Add(this.txtValRng);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cmbValType);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtShowOrder);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cmbProperty);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtUnit);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtVitalSigns);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtVitalCode);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtClassCode);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(232, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(564, 150);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "项目设置";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(479, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 123);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtMemo
            // 
            this.txtMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMemo.Location = new System.Drawing.Point(184, 117);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(289, 21);
            this.txtMemo.TabIndex = 18;
            this.txtMemo.TextChanged += new System.EventHandler(this.item_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(150, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "备注";
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(53, 119);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(48, 16);
            this.chkEnabled.TabIndex = 16;
            this.chkEnabled.Text = "启用";
            this.chkEnabled.UseVisualStyleBackColor = true;
            this.chkEnabled.CheckedChanged += new System.EventHandler(this.item_TextChanged);
            // 
            // txtValRng
            // 
            this.txtValRng.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValRng.Location = new System.Drawing.Point(184, 84);
            this.txtValRng.Name = "txtValRng";
            this.txtValRng.Size = new System.Drawing.Size(289, 21);
            this.txtValRng.TabIndex = 15;
            this.txtValRng.TextChanged += new System.EventHandler(this.item_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(128, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "取值范围";
            // 
            // cmbValType
            // 
            this.cmbValType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbValType.FormattingEnabled = true;
            this.cmbValType.Items.AddRange(new object[] {
            "0:字符型",
            "1:数值型"});
            this.cmbValType.Location = new System.Drawing.Point(53, 85);
            this.cmbValType.Name = "cmbValType";
            this.cmbValType.Size = new System.Drawing.Size(66, 20);
            this.cmbValType.TabIndex = 13;
            this.cmbValType.SelectedIndexChanged += new System.EventHandler(this.item_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "值类型";
            // 
            // txtShowOrder
            // 
            this.txtShowOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShowOrder.Location = new System.Drawing.Point(360, 51);
            this.txtShowOrder.Name = "txtShowOrder";
            this.txtShowOrder.Size = new System.Drawing.Size(113, 21);
            this.txtShowOrder.TabIndex = 11;
            this.txtShowOrder.TextChanged += new System.EventHandler(this.item_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(301, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "显示顺序";
            // 
            // cmbProperty
            // 
            this.cmbProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProperty.FormattingEnabled = true;
            this.cmbProperty.Items.AddRange(new object[] {
            "0:生命体征",
            "1:入量",
            "2:出量",
            "3:其它计量 ",
            "4.护理事件"});
            this.cmbProperty.Location = new System.Drawing.Point(184, 51);
            this.cmbProperty.Name = "cmbProperty";
            this.cmbProperty.Size = new System.Drawing.Size(95, 20);
            this.cmbProperty.TabIndex = 9;
            this.cmbProperty.SelectedIndexChanged += new System.EventHandler(this.item_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(150, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "属性";
            // 
            // txtUnit
            // 
            this.txtUnit.Enabled = false;
            this.txtUnit.Location = new System.Drawing.Point(53, 51);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(66, 21);
            this.txtUnit.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "单位";
            // 
            // txtVitalSigns
            // 
            this.txtVitalSigns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVitalSigns.Enabled = false;
            this.txtVitalSigns.Location = new System.Drawing.Point(360, 17);
            this.txtVitalSigns.Name = "txtVitalSigns";
            this.txtVitalSigns.Size = new System.Drawing.Size(113, 21);
            this.txtVitalSigns.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(301, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "项目名称";
            // 
            // txtVitalCode
            // 
            this.txtVitalCode.Enabled = false;
            this.txtVitalCode.Location = new System.Drawing.Point(184, 17);
            this.txtVitalCode.Name = "txtVitalCode";
            this.txtVitalCode.Size = new System.Drawing.Size(80, 21);
            this.txtVitalCode.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(150, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "代码";
            // 
            // txtClassCode
            // 
            this.txtClassCode.Enabled = false;
            this.txtClassCode.Location = new System.Drawing.Point(53, 18);
            this.txtClassCode.Name = "txtClassCode";
            this.txtClassCode.Size = new System.Drawing.Size(66, 21);
            this.txtClassCode.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "类别";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dgvNursingItem);
            this.groupBox3.Location = new System.Drawing.Point(232, 168);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(564, 334);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "项目列表";
            // 
            // dgvNursingItem
            // 
            this.dgvNursingItem.AllowUserToAddRows = false;
            this.dgvNursingItem.AllowUserToDeleteRows = false;
            this.dgvNursingItem.AllowUserToResizeRows = false;
            this.dgvNursingItem.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvNursingItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.CLASS_CODE,
            this.VITAL_CODE,
            this.VITAL_SIGNS,
            this.UNIT,
            this.ATTRIBUTE,
            this.SHOW_ORDER,
            this.VALUE_TYPE,
            this.VALUE_SCOPE,
            this.ENABLED,
            this.MEMO});
            this.dgvNursingItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNursingItem.Location = new System.Drawing.Point(3, 17);
            this.dgvNursingItem.MultiSelect = false;
            this.dgvNursingItem.Name = "dgvNursingItem";
            this.dgvNursingItem.ReadOnly = true;
            this.dgvNursingItem.RowHeadersVisible = false;
            this.dgvNursingItem.RowTemplate.Height = 23;
            this.dgvNursingItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNursingItem.Size = new System.Drawing.Size(558, 314);
            this.dgvNursingItem.TabIndex = 3;
            this.dgvNursingItem.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_RowPostPaint);
            this.dgvNursingItem.SelectionChanged += new System.EventHandler(this.dgvNursingItem_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "序号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 36;
            // 
            // CLASS_CODE
            // 
            this.CLASS_CODE.DataPropertyName = "CLASS_CODE";
            this.CLASS_CODE.Frozen = true;
            this.CLASS_CODE.HeaderText = "类别";
            this.CLASS_CODE.Name = "CLASS_CODE";
            this.CLASS_CODE.ReadOnly = true;
            this.CLASS_CODE.Width = 40;
            // 
            // VITAL_CODE
            // 
            this.VITAL_CODE.DataPropertyName = "VITAL_CODE";
            this.VITAL_CODE.Frozen = true;
            this.VITAL_CODE.HeaderText = "代码";
            this.VITAL_CODE.Name = "VITAL_CODE";
            this.VITAL_CODE.ReadOnly = true;
            this.VITAL_CODE.Width = 60;
            // 
            // VITAL_SIGNS
            // 
            this.VITAL_SIGNS.DataPropertyName = "VITAL_SIGNS";
            this.VITAL_SIGNS.HeaderText = "项目名称";
            this.VITAL_SIGNS.Name = "VITAL_SIGNS";
            this.VITAL_SIGNS.ReadOnly = true;
            // 
            // UNIT
            // 
            this.UNIT.DataPropertyName = "UNIT";
            this.UNIT.HeaderText = "单位";
            this.UNIT.Name = "UNIT";
            this.UNIT.ReadOnly = true;
            this.UNIT.Width = 60;
            // 
            // ATTRIBUTE
            // 
            this.ATTRIBUTE.DataPropertyName = "ATTRIBUTE";
            this.ATTRIBUTE.HeaderText = "属性";
            this.ATTRIBUTE.Name = "ATTRIBUTE";
            this.ATTRIBUTE.ReadOnly = true;
            this.ATTRIBUTE.Width = 80;
            // 
            // SHOW_ORDER
            // 
            this.SHOW_ORDER.DataPropertyName = "SHOW_ORDER";
            this.SHOW_ORDER.HeaderText = "显示顺序";
            this.SHOW_ORDER.Name = "SHOW_ORDER";
            this.SHOW_ORDER.ReadOnly = true;
            this.SHOW_ORDER.Width = 60;
            // 
            // VALUE_TYPE
            // 
            this.VALUE_TYPE.DataPropertyName = "VALUE_TYPE";
            this.VALUE_TYPE.HeaderText = "值类型";
            this.VALUE_TYPE.Items.AddRange(new object[] {
            "0:字符型",
            "1:数值型"});
            this.VALUE_TYPE.Name = "VALUE_TYPE";
            this.VALUE_TYPE.ReadOnly = true;
            this.VALUE_TYPE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.VALUE_TYPE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.VALUE_TYPE.Width = 80;
            // 
            // VALUE_SCOPE
            // 
            this.VALUE_SCOPE.DataPropertyName = "VALUE_SCOPE";
            this.VALUE_SCOPE.HeaderText = "取值范围";
            this.VALUE_SCOPE.Name = "VALUE_SCOPE";
            this.VALUE_SCOPE.ReadOnly = true;
            // 
            // ENABLED
            // 
            this.ENABLED.DataPropertyName = "ENABLED";
            this.ENABLED.FalseValue = "0";
            this.ENABLED.HeaderText = "启用";
            this.ENABLED.Name = "ENABLED";
            this.ENABLED.ReadOnly = true;
            this.ENABLED.TrueValue = "1";
            this.ENABLED.Width = 40;
            // 
            // MEMO
            // 
            this.MEMO.DataPropertyName = "MEMO";
            this.MEMO.HeaderText = "备注";
            this.MEMO.Name = "MEMO";
            this.MEMO.ReadOnly = true;
            // 
            // NurseItemManagerFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 517);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NurseItemManagerFrm";
            this.Text = "护理项目设置";
            this.Load += new System.EventHandler(this.NurseItemManagerFrm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDept)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNursingItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvDept;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DEPT_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DEPT_NAME;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvNursingItem;
        private System.Windows.Forms.TextBox txtVitalSigns;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtVitalCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtClassCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbProperty;
        private System.Windows.Forms.TextBox txtShowOrder;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbValType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtValRng;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLASS_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn VITAL_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn VITAL_SIGNS;
        private System.Windows.Forms.DataGridViewTextBoxColumn UNIT;
        private System.Windows.Forms.DataGridViewComboBoxColumn ATTRIBUTE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SHOW_ORDER;
        private System.Windows.Forms.DataGridViewComboBoxColumn VALUE_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALUE_SCOPE;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ENABLED;
        private System.Windows.Forms.DataGridViewTextBoxColumn MEMO;
    }
}