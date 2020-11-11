namespace HISPlus
{
    partial class ApplicationParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationParams));
            this.panel = new DevExpress.XtraEditors.PanelControl();
            this.chkEnabled = new DevExpress.XtraEditors.CheckEdit();
            this.txtParamClassSub = new HISPlus.UserControls.UcTextBox();
            this.label10 = new DevExpress.XtraEditors.LabelControl();
            this.txtParamValueRng = new HISPlus.UserControls.UcTextBox();
            this.label8 = new DevExpress.XtraEditors.LabelControl();
            this.btnDelete = new HISPlus.UserControls.UcButton();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.btnNew = new HISPlus.UserControls.UcButton();
            this.txtParamValue = new HISPlus.UserControls.UcTextArea();
            this.label7 = new DevExpress.XtraEditors.LabelControl();
            this.txtMemo = new HISPlus.UserControls.UcTextArea();
            this.label6 = new DevExpress.XtraEditors.LabelControl();
            this.txtParamName = new HISPlus.UserControls.UcTextBox();
            this.label5 = new DevExpress.XtraEditors.LabelControl();
            this.txtParamNameCn = new HISPlus.UserControls.UcTextBox();
            this.label4 = new DevExpress.XtraEditors.LabelControl();
            this.txtParamClass = new HISPlus.UserControls.UcTextBox();
            this.label3 = new DevExpress.XtraEditors.LabelControl();
            this.txtUserId = new HISPlus.UserControls.UcTextBox();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDeptCode = new HISPlus.UserControls.UcTextBox();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.txtAppCode = new HISPlus.UserControls.UcTextBox();
            this.label9 = new DevExpress.XtraEditors.LabelControl();
            this.ucGridView1 = new HISPlus.UserControls.UcGridView();
            ((System.ComponentModel.ISupportInitialize)(this.panel)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnabled.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel.Controls.Add(this.chkEnabled);
            this.panel.Controls.Add(this.txtParamClassSub);
            this.panel.Controls.Add(this.label10);
            this.panel.Controls.Add(this.txtParamValueRng);
            this.panel.Controls.Add(this.label8);
            this.panel.Controls.Add(this.btnDelete);
            this.panel.Controls.Add(this.btnSave);
            this.panel.Controls.Add(this.btnNew);
            this.panel.Controls.Add(this.txtParamValue);
            this.panel.Controls.Add(this.label7);
            this.panel.Controls.Add(this.txtMemo);
            this.panel.Controls.Add(this.label6);
            this.panel.Controls.Add(this.txtParamName);
            this.panel.Controls.Add(this.label5);
            this.panel.Controls.Add(this.txtParamNameCn);
            this.panel.Controls.Add(this.label4);
            this.panel.Controls.Add(this.txtParamClass);
            this.panel.Controls.Add(this.label3);
            this.panel.Controls.Add(this.txtUserId);
            this.panel.Controls.Add(this.label2);
            this.panel.Controls.Add(this.txtDeptCode);
            this.panel.Controls.Add(this.label1);
            this.panel.Controls.Add(this.txtAppCode);
            this.panel.Controls.Add(this.label9);
            this.panel.Location = new System.Drawing.Point(15, 10);
            this.panel.Margin = new System.Windows.Forms.Padding(4);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(349, 764);
            this.panel.TabIndex = 0;
            // 
            // chkEnabled
            // 
            this.chkEnabled.Location = new System.Drawing.Point(101, 446);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Properties.Caption = "启用";
            this.chkEnabled.Size = new System.Drawing.Size(75, 22);
            this.chkEnabled.TabIndex = 23;
            this.chkEnabled.CheckedChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // txtParamClassSub
            // 
            this.txtParamClassSub.Location = new System.Drawing.Point(103, 206);
            this.txtParamClassSub.Margin = new System.Windows.Forms.Padding(4);
            this.txtParamClassSub.MaxLength = 0;
            this.txtParamClassSub.Multiline = false;
            this.txtParamClassSub.Name = "txtParamClassSub";
            this.txtParamClassSub.PasswordChar = '\0';
            this.txtParamClassSub.ReadOnly = false;
            this.txtParamClassSub.Size = new System.Drawing.Size(233, 32);
            this.txtParamClassSub.TabIndex = 4;
            this.txtParamClassSub.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(50, 210);
            this.label10.Margin = new System.Windows.Forms.Padding(4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 18);
            this.label10.TabIndex = 22;
            this.label10.Text = "子类";
            // 
            // txtParamValueRng
            // 
            this.txtParamValueRng.Location = new System.Drawing.Point(103, 398);
            this.txtParamValueRng.Margin = new System.Windows.Forms.Padding(4);
            this.txtParamValueRng.MaxLength = 0;
            this.txtParamValueRng.Multiline = false;
            this.txtParamValueRng.Name = "txtParamValueRng";
            this.txtParamValueRng.PasswordChar = '\0';
            this.txtParamValueRng.ReadOnly = false;
            this.txtParamValueRng.Size = new System.Drawing.Size(233, 32);
            this.txtParamValueRng.TabIndex = 8;
            this.txtParamValueRng.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(20, 401);
            this.label8.Margin = new System.Windows.Forms.Padding(4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 18);
            this.label8.TabIndex = 14;
            this.label8.Text = "取值范围";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageRight = false;
            this.btnDelete.ImageStyle = HISPlus.UserControls.ImageStyle.Delete;
            this.btnDelete.Location = new System.Drawing.Point(127, 720);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnDelete.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 30);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.TextValue = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageRight = false;
            this.btnSave.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSave.Location = new System.Drawing.Point(236, 720);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnSave.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 13;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageRight = false;
            this.btnNew.ImageStyle = HISPlus.UserControls.ImageStyle.Add;
            this.btnNew.Location = new System.Drawing.Point(19, 720);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4);
            this.btnNew.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnNew.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(90, 30);
            this.btnNew.TabIndex = 11;
            this.btnNew.TextValue = "新增";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // txtParamValue
            // 
            this.txtParamValue.HideSelection = true;
            this.txtParamValue.Location = new System.Drawing.Point(103, 327);
            this.txtParamValue.Margin = new System.Windows.Forms.Padding(4);
            this.txtParamValue.MaxLength = 0;
            this.txtParamValue.Multiline = true;
            this.txtParamValue.Name = "txtParamValue";
            this.txtParamValue.PasswordChar = '\0';
            this.txtParamValue.ReadOnly = false;
            this.txtParamValue.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtParamValue.SelectedText = "";
            this.txtParamValue.SelectionLength = 0;
            this.txtParamValue.SelectionStart = 0;
            this.txtParamValue.Size = new System.Drawing.Size(233, 60);
            this.txtParamValue.TabIndex = 7;
            this.txtParamValue.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(35, 332);
            this.label7.Margin = new System.Windows.Forms.Padding(4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 18);
            this.label7.TabIndex = 12;
            this.label7.Text = "参数值";
            // 
            // txtMemo
            // 
            this.txtMemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMemo.HideSelection = true;
            this.txtMemo.Location = new System.Drawing.Point(16, 477);
            this.txtMemo.Margin = new System.Windows.Forms.Padding(4);
            this.txtMemo.MaxLength = 0;
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.PasswordChar = '\0';
            this.txtMemo.ReadOnly = false;
            this.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMemo.SelectedText = "";
            this.txtMemo.SelectionLength = 0;
            this.txtMemo.SelectionStart = 0;
            this.txtMemo.Size = new System.Drawing.Size(320, 208);
            this.txtMemo.TabIndex = 10;
            this.txtMemo.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 450);
            this.label6.Margin = new System.Windows.Forms.Padding(4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 18);
            this.label6.TabIndex = 16;
            this.label6.Text = "备    注";
            // 
            // txtParamName
            // 
            this.txtParamName.Location = new System.Drawing.Point(103, 286);
            this.txtParamName.Margin = new System.Windows.Forms.Padding(4);
            this.txtParamName.MaxLength = 0;
            this.txtParamName.Multiline = false;
            this.txtParamName.Name = "txtParamName";
            this.txtParamName.PasswordChar = '\0';
            this.txtParamName.ReadOnly = false;
            this.txtParamName.Size = new System.Drawing.Size(233, 32);
            this.txtParamName.TabIndex = 6;
            this.txtParamName.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(35, 291);
            this.label5.Margin = new System.Windows.Forms.Padding(4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 18);
            this.label5.TabIndex = 10;
            this.label5.Text = "参数名";
            // 
            // txtParamNameCn
            // 
            this.txtParamNameCn.Location = new System.Drawing.Point(103, 246);
            this.txtParamNameCn.Margin = new System.Windows.Forms.Padding(4);
            this.txtParamNameCn.MaxLength = 0;
            this.txtParamNameCn.Multiline = false;
            this.txtParamNameCn.Name = "txtParamNameCn";
            this.txtParamNameCn.PasswordChar = '\0';
            this.txtParamNameCn.ReadOnly = false;
            this.txtParamNameCn.Size = new System.Drawing.Size(233, 32);
            this.txtParamNameCn.TabIndex = 5;
            this.txtParamNameCn.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 250);
            this.label4.Margin = new System.Windows.Forms.Padding(4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "参数名(中)";
            // 
            // txtParamClass
            // 
            this.txtParamClass.Location = new System.Drawing.Point(103, 158);
            this.txtParamClass.Margin = new System.Windows.Forms.Padding(4);
            this.txtParamClass.MaxLength = 0;
            this.txtParamClass.Multiline = false;
            this.txtParamClass.Name = "txtParamClass";
            this.txtParamClass.PasswordChar = '\0';
            this.txtParamClass.ReadOnly = false;
            this.txtParamClass.Size = new System.Drawing.Size(233, 32);
            this.txtParamClass.TabIndex = 3;
            this.txtParamClass.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(20, 162);
            this.label3.Margin = new System.Windows.Forms.Padding(4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "参数类别";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(103, 116);
            this.txtUserId.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserId.MaxLength = 0;
            this.txtUserId.Multiline = false;
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.PasswordChar = '\0';
            this.txtUserId.ReadOnly = false;
            this.txtUserId.Size = new System.Drawing.Size(233, 32);
            this.txtUserId.TabIndex = 2;
            this.txtUserId.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(34, 120);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "用户ID";
            // 
            // txtDeptCode
            // 
            this.txtDeptCode.Location = new System.Drawing.Point(103, 72);
            this.txtDeptCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtDeptCode.MaxLength = 0;
            this.txtDeptCode.Multiline = false;
            this.txtDeptCode.Name = "txtDeptCode";
            this.txtDeptCode.PasswordChar = '\0';
            this.txtDeptCode.ReadOnly = false;
            this.txtDeptCode.Size = new System.Drawing.Size(233, 32);
            this.txtDeptCode.TabIndex = 1;
            this.txtDeptCode.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 76);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "科室代码";
            // 
            // txtAppCode
            // 
            this.txtAppCode.Location = new System.Drawing.Point(103, 32);
            this.txtAppCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtAppCode.MaxLength = 0;
            this.txtAppCode.Multiline = false;
            this.txtAppCode.Name = "txtAppCode";
            this.txtAppCode.PasswordChar = '\0';
            this.txtAppCode.ReadOnly = false;
            this.txtAppCode.Size = new System.Drawing.Size(233, 32);
            this.txtAppCode.TabIndex = 0;
            this.txtAppCode.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(20, 36);
            this.label9.Margin = new System.Windows.Forms.Padding(4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "程序代码";
            // 
            // ucGridView1
            // 
            this.ucGridView1.AllowDeleteRows = false;
            this.ucGridView1.AllowEdit = false;
            this.ucGridView1.AllowSort = false;
            this.ucGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucGridView1.DataSource = null;
            this.ucGridView1.Location = new System.Drawing.Point(371, 13);
            this.ucGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucGridView1.Name = "ucGridView1";
            this.ucGridView1.ShowRowIndicator = false;
            this.ucGridView1.Size = new System.Drawing.Size(705, 760);
            this.ucGridView1.TabIndex = 2;
            this.ucGridView1.SelectionChanged += new System.EventHandler(this.dgvParameters_SelectionChanged);
            // 
            // ApplicationParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 786);
            this.Controls.Add(this.ucGridView1);
            this.Controls.Add(this.panel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ApplicationParams";
            this.Text = "应用程序参数";
            this.Load += new System.EventHandler(this.ApplicationParams_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnabled.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel;
        private HISPlus.UserControls.UcTextBox txtAppCode;
        private DevExpress.XtraEditors.LabelControl label9;
        private HISPlus.UserControls.UcTextBox txtDeptCode;
        private DevExpress.XtraEditors.LabelControl label1;
        private HISPlus.UserControls.UcTextBox txtUserId;
        private DevExpress.XtraEditors.LabelControl label2;
        private HISPlus.UserControls.UcTextBox txtParamNameCn;
        private DevExpress.XtraEditors.LabelControl label4;
        private HISPlus.UserControls.UcTextBox txtParamClass;
        private DevExpress.XtraEditors.LabelControl label3;
        private HISPlus.UserControls.UcTextBox txtParamName;
        private DevExpress.XtraEditors.LabelControl label5;
        private HISPlus.UserControls.UcTextArea txtMemo;
        private DevExpress.XtraEditors.LabelControl label6;
        private HISPlus.UserControls.UcTextArea txtParamValue;
        private DevExpress.XtraEditors.LabelControl label7;
        private HISPlus.UserControls.UcButton btnDelete;
        private HISPlus.UserControls.UcButton btnSave;
        private HISPlus.UserControls.UcButton btnNew;
        private HISPlus.UserControls.UcTextBox txtParamValueRng;
        private DevExpress.XtraEditors.LabelControl label8;
        private HISPlus.UserControls.UcTextBox txtParamClassSub;
        private DevExpress.XtraEditors.LabelControl label10;
        private UserControls.UcGridView ucGridView1;
        private DevExpress.XtraEditors.CheckEdit chkEnabled;
    }
}