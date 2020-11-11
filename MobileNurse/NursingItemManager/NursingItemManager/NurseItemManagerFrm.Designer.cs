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
            this.groupBox2 = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.txtMemo = new HISPlus.UserControls.UcTextBox();
            this.label9 = new DevExpress.XtraEditors.LabelControl();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.txtValRng = new HISPlus.UserControls.UcTextBox();
            this.label8 = new DevExpress.XtraEditors.LabelControl();
            this.cmbValType = new HISPlus.UserControls.UcComboBox();
            this.label7 = new DevExpress.XtraEditors.LabelControl();
            this.txtShowOrder = new HISPlus.UserControls.UcTextBox();
            this.label6 = new DevExpress.XtraEditors.LabelControl();
            this.cmbProperty = new HISPlus.UserControls.UcComboBox();
            this.label5 = new DevExpress.XtraEditors.LabelControl();
            this.txtUnit = new HISPlus.UserControls.UcTextBox();
            this.label4 = new DevExpress.XtraEditors.LabelControl();
            this.txtVitalSigns = new HISPlus.UserControls.UcTextBox();
            this.label3 = new DevExpress.XtraEditors.LabelControl();
            this.txtVitalCode = new HISPlus.UserControls.UcTextBox();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.txtClassCode = new HISPlus.UserControls.UcTextBox();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.ucGridView1 = new HISPlus.UserControls.UcGridView();
            this.ucGridView2 = new HISPlus.UserControls.UcGridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
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
            this.groupBox2.Location = new System.Drawing.Point(309, 18);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(752, 225);
            this.groupBox2.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageRight = false;
            this.btnSave.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSave.Location = new System.Drawing.Point(650, 21);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnSave.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 19;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtMemo
            // 
            this.txtMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMemo.Location = new System.Drawing.Point(245, 176);
            this.txtMemo.Margin = new System.Windows.Forms.Padding(4);
            this.txtMemo.MaxLength = 0;
            this.txtMemo.Multiline = false;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.PasswordChar = '\0';
            this.txtMemo.ReadOnly = false;
            this.txtMemo.Size = new System.Drawing.Size(385, 24);
            this.txtMemo.TabIndex = 18;
            this.txtMemo.TextChanged += new System.EventHandler(this.item_TextChanged);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(200, 180);
            this.label9.Margin = new System.Windows.Forms.Padding(4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 18);
            this.label9.TabIndex = 17;
            this.label9.Text = "备注";
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(71, 178);
            this.chkEnabled.Margin = new System.Windows.Forms.Padding(4);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(60, 22);
            this.chkEnabled.TabIndex = 16;
            this.chkEnabled.Text = "启用";
            this.chkEnabled.UseVisualStyleBackColor = true;
            this.chkEnabled.CheckedChanged += new System.EventHandler(this.item_TextChanged);
            // 
            // txtValRng
            // 
            this.txtValRng.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValRng.Location = new System.Drawing.Point(245, 126);
            this.txtValRng.Margin = new System.Windows.Forms.Padding(4);
            this.txtValRng.MaxLength = 0;
            this.txtValRng.Multiline = false;
            this.txtValRng.Name = "txtValRng";
            this.txtValRng.PasswordChar = '\0';
            this.txtValRng.ReadOnly = false;
            this.txtValRng.Size = new System.Drawing.Size(385, 24);
            this.txtValRng.TabIndex = 15;
            this.txtValRng.TextChanged += new System.EventHandler(this.item_TextChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(171, 132);
            this.label8.Margin = new System.Windows.Forms.Padding(4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 18);
            this.label8.TabIndex = 14;
            this.label8.Text = "取值范围";
            // 
            // cmbValType
            // 
            this.cmbValType.DataSource = null;
            this.cmbValType.DisplayMember = null;
            this.cmbValType.DropDownHeight = 0;
            this.cmbValType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbValType.DropDownWidth = 0;
            this.cmbValType.DroppedDown = false;
            this.cmbValType.FormattingEnabled = true;
            this.cmbValType.IntegralHeight = true;
            this.cmbValType.Location = new System.Drawing.Point(71, 128);
            this.cmbValType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbValType.MaxDropDownItems = 0;
            this.cmbValType.MaximumSize = new System.Drawing.Size(1000, 24);
            this.cmbValType.MinimumSize = new System.Drawing.Size(40, 24);
            this.cmbValType.Name = "cmbValType";
            this.cmbValType.SelectedIndex = -1;
            this.cmbValType.SelectedValue = null;
            this.cmbValType.Size = new System.Drawing.Size(87, 24);
            this.cmbValType.TabIndex = 13;
            this.cmbValType.ValueMember = null;
            this.cmbValType.SelectedIndexChanged += new System.EventHandler(this.item_TextChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 132);
            this.label7.Margin = new System.Windows.Forms.Padding(4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 18);
            this.label7.TabIndex = 12;
            this.label7.Text = "值类型";
            // 
            // txtShowOrder
            // 
            this.txtShowOrder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShowOrder.Location = new System.Drawing.Point(480, 76);
            this.txtShowOrder.Margin = new System.Windows.Forms.Padding(4);
            this.txtShowOrder.MaxLength = 0;
            this.txtShowOrder.Multiline = false;
            this.txtShowOrder.Name = "txtShowOrder";
            this.txtShowOrder.PasswordChar = '\0';
            this.txtShowOrder.ReadOnly = false;
            this.txtShowOrder.Size = new System.Drawing.Size(151, 24);
            this.txtShowOrder.TabIndex = 11;
            this.txtShowOrder.TextChanged += new System.EventHandler(this.item_TextChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(401, 81);
            this.label6.Margin = new System.Windows.Forms.Padding(4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 18);
            this.label6.TabIndex = 10;
            this.label6.Text = "显示顺序";
            // 
            // cmbProperty
            // 
            this.cmbProperty.DataSource = null;
            this.cmbProperty.DisplayMember = null;
            this.cmbProperty.DropDownHeight = 0;
            this.cmbProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProperty.DropDownWidth = 0;
            this.cmbProperty.DroppedDown = false;
            this.cmbProperty.FormattingEnabled = true;
            this.cmbProperty.IntegralHeight = true;
            this.cmbProperty.Location = new System.Drawing.Point(245, 76);
            this.cmbProperty.Margin = new System.Windows.Forms.Padding(4);
            this.cmbProperty.MaxDropDownItems = 0;
            this.cmbProperty.MaximumSize = new System.Drawing.Size(1000, 24);
            this.cmbProperty.MinimumSize = new System.Drawing.Size(40, 24);
            this.cmbProperty.Name = "cmbProperty";
            this.cmbProperty.SelectedIndex = -1;
            this.cmbProperty.SelectedValue = null;
            this.cmbProperty.Size = new System.Drawing.Size(125, 24);
            this.cmbProperty.TabIndex = 9;
            this.cmbProperty.ValueMember = null;
            this.cmbProperty.SelectedIndexChanged += new System.EventHandler(this.item_TextChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(200, 81);
            this.label5.Margin = new System.Windows.Forms.Padding(4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 18);
            this.label5.TabIndex = 8;
            this.label5.Text = "属性";
            // 
            // txtUnit
            // 
            this.txtUnit.Enabled = false;
            this.txtUnit.Location = new System.Drawing.Point(71, 76);
            this.txtUnit.Margin = new System.Windows.Forms.Padding(4);
            this.txtUnit.MaxLength = 0;
            this.txtUnit.Multiline = false;
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.PasswordChar = '\0';
            this.txtUnit.ReadOnly = false;
            this.txtUnit.Size = new System.Drawing.Size(88, 24);
            this.txtUnit.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(24, 81);
            this.label4.Margin = new System.Windows.Forms.Padding(4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "单位";
            // 
            // txtVitalSigns
            // 
            this.txtVitalSigns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVitalSigns.Enabled = false;
            this.txtVitalSigns.Location = new System.Drawing.Point(480, 26);
            this.txtVitalSigns.Margin = new System.Windows.Forms.Padding(4);
            this.txtVitalSigns.MaxLength = 0;
            this.txtVitalSigns.Multiline = false;
            this.txtVitalSigns.Name = "txtVitalSigns";
            this.txtVitalSigns.PasswordChar = '\0';
            this.txtVitalSigns.ReadOnly = false;
            this.txtVitalSigns.Size = new System.Drawing.Size(151, 24);
            this.txtVitalSigns.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(401, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "项目名称";
            // 
            // txtVitalCode
            // 
            this.txtVitalCode.Enabled = false;
            this.txtVitalCode.Location = new System.Drawing.Point(245, 26);
            this.txtVitalCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtVitalCode.MaxLength = 0;
            this.txtVitalCode.Multiline = false;
            this.txtVitalCode.Name = "txtVitalCode";
            this.txtVitalCode.PasswordChar = '\0';
            this.txtVitalCode.ReadOnly = false;
            this.txtVitalCode.Size = new System.Drawing.Size(107, 24);
            this.txtVitalCode.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(200, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "代码";
            // 
            // txtClassCode
            // 
            this.txtClassCode.Enabled = false;
            this.txtClassCode.Location = new System.Drawing.Point(71, 27);
            this.txtClassCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtClassCode.MaxLength = 0;
            this.txtClassCode.Multiline = false;
            this.txtClassCode.Name = "txtClassCode";
            this.txtClassCode.PasswordChar = '\0';
            this.txtClassCode.ReadOnly = false;
            this.txtClassCode.Size = new System.Drawing.Size(88, 24);
            this.txtClassCode.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "类别";
            // 
            // ucGridView1
            // 
            this.ucGridView1.AllowDeleteRows = false;
            this.ucGridView1.AllowEdit = false;
            this.ucGridView1.AllowSort = false;
            this.ucGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ucGridView1.ColumnAutoWidth = true;
            this.ucGridView1.ColumnsEvenOldRowColor = null;
            this.ucGridView1.DataSource = null;
            this.ucGridView1.Location = new System.Drawing.Point(12, 13);
            this.ucGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucGridView1.Name = "ucGridView1";
            this.ucGridView1.ShowRowIndicator = false;
            this.ucGridView1.Size = new System.Drawing.Size(290, 750);
            this.ucGridView1.TabIndex = 4;
            // 
            // ucGridView2
            // 
            this.ucGridView2.AllowDeleteRows = false;
            this.ucGridView2.AllowEdit = false;
            this.ucGridView2.AllowSort = false;
            this.ucGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucGridView2.ColumnAutoWidth = true;
            this.ucGridView2.ColumnsEvenOldRowColor = null;
            this.ucGridView2.DataSource = null;
            this.ucGridView2.Location = new System.Drawing.Point(309, 252);
            this.ucGridView2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ucGridView2.Name = "ucGridView2";
            this.ucGridView2.ShowRowIndicator = false;
            this.ucGridView2.Size = new System.Drawing.Size(752, 511);
            this.ucGridView2.TabIndex = 4;
            // 
            // NurseItemManagerFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 776);
            this.Controls.Add(this.ucGridView2);
            this.Controls.Add(this.ucGridView1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NurseItemManagerFrm";
            this.Text = "护理项目设置";
            this.Load += new System.EventHandler(this.NurseItemManagerFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl groupBox2;
        private HISPlus.UserControls.UcTextBox txtVitalSigns;
        private DevExpress.XtraEditors.LabelControl label3;
        private HISPlus.UserControls.UcTextBox txtVitalCode;
        private DevExpress.XtraEditors.LabelControl label2;
        private HISPlus.UserControls.UcTextBox txtClassCode;
        private DevExpress.XtraEditors.LabelControl label1;
        private HISPlus.UserControls.UcTextBox txtUnit;
        private DevExpress.XtraEditors.LabelControl label4;
        private DevExpress.XtraEditors.LabelControl label5;
        private HISPlus.UserControls.UcComboBox cmbProperty;
        private HISPlus.UserControls.UcTextBox txtShowOrder;
        private DevExpress.XtraEditors.LabelControl label6;
        private HISPlus.UserControls.UcComboBox cmbValType;
        private DevExpress.XtraEditors.LabelControl label7;
        private HISPlus.UserControls.UcTextBox txtValRng;
        private DevExpress.XtraEditors.LabelControl label8;
        private System.Windows.Forms.CheckBox chkEnabled;
        private HISPlus.UserControls.UcTextBox txtMemo;
        private DevExpress.XtraEditors.LabelControl label9;
        private HISPlus.UserControls.UcButton btnSave;
        private UserControls.UcGridView ucGridView1;
        private UserControls.UcGridView ucGridView2;
    }
}