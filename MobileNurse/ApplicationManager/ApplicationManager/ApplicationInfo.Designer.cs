namespace HISPlus
{
    partial class ApplicationInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationInfo));
            this.groupBox2 = new DevExpress.XtraEditors.PanelControl();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.txtAppCode = new HISPlus.UserControls.UcTextBox();
            this.label5 = new DevExpress.XtraEditors.LabelControl();
            this.txtModifyDateTime = new HISPlus.UserControls.UcTextBox();
            this.txtCreateDateTime = new HISPlus.UserControls.UcTextBox();
            this.txtMemo = new HISPlus.UserControls.UcTextArea();
            this.txtGrants = new HISPlus.UserControls.UcTextBox();
            this.txtVersion = new HISPlus.UserControls.UcTextBox();
            this.txtAppTitle = new HISPlus.UserControls.UcTextBox();
            this.txtAppName = new HISPlus.UserControls.UcTextBox();
            this.label8 = new DevExpress.XtraEditors.LabelControl();
            this.label7 = new DevExpress.XtraEditors.LabelControl();
            this.label6 = new DevExpress.XtraEditors.LabelControl();
            this.label4 = new DevExpress.XtraEditors.LabelControl();
            this.label3 = new DevExpress.XtraEditors.LabelControl();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new DevExpress.XtraEditors.PanelControl();
            this.btnDelete = new HISPlus.UserControls.UcButton();
            this.btnNewApplication = new HISPlus.UserControls.UcButton();
            this.btnSave = new HISPlus.UserControls.UcButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkEnabled);
            this.groupBox2.Controls.Add(this.txtAppCode);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtModifyDateTime);
            this.groupBox2.Controls.Add(this.txtCreateDateTime);
            this.groupBox2.Controls.Add(this.txtMemo);
            this.groupBox2.Controls.Add(this.txtGrants);
            this.groupBox2.Controls.Add(this.txtVersion);
            this.groupBox2.Controls.Add(this.txtAppTitle);
            this.groupBox2.Controls.Add(this.txtAppName);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(13, 6);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(752, 591);
            this.groupBox2.TabIndex = 39;
            // 
            // chkEnabled
            // 
            this.chkEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.BackColor = System.Drawing.Color.Transparent;
            this.chkEnabled.Location = new System.Drawing.Point(84, 560);
            this.chkEnabled.Margin = new System.Windows.Forms.Padding(4);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(90, 22);
            this.chkEnabled.TabIndex = 50;
            this.chkEnabled.Text = "是否启用";
            this.chkEnabled.UseVisualStyleBackColor = false;
            this.chkEnabled.CheckedChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // txtAppCode
            // 
            this.txtAppCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAppCode.Location = new System.Drawing.Point(84, 21);
            this.txtAppCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtAppCode.MaxLength = 0;
            this.txtAppCode.Multiline = false;
            this.txtAppCode.Name = "txtAppCode";
            this.txtAppCode.PasswordChar = '\0';
            this.txtAppCode.ReadOnly = false;
            this.txtAppCode.Size = new System.Drawing.Size(660, 32);
            this.txtAppCode.TabIndex = 35;
            this.txtAppCode.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // label5
            // 
            this.label5.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(5, 24);
            this.label5.Margin = new System.Windows.Forms.Padding(4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 18);
            this.label5.TabIndex = 34;
            this.label5.Text = "程序代码";
            // 
            // txtModifyDateTime
            // 
            this.txtModifyDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtModifyDateTime.Enabled = false;
            this.txtModifyDateTime.Location = new System.Drawing.Point(84, 321);
            this.txtModifyDateTime.Margin = new System.Windows.Forms.Padding(4);
            this.txtModifyDateTime.MaxLength = 0;
            this.txtModifyDateTime.Multiline = false;
            this.txtModifyDateTime.Name = "txtModifyDateTime";
            this.txtModifyDateTime.PasswordChar = '\0';
            this.txtModifyDateTime.ReadOnly = false;
            this.txtModifyDateTime.Size = new System.Drawing.Size(660, 32);
            this.txtModifyDateTime.TabIndex = 47;
            this.txtModifyDateTime.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // txtCreateDateTime
            // 
            this.txtCreateDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCreateDateTime.Enabled = false;
            this.txtCreateDateTime.Location = new System.Drawing.Point(84, 271);
            this.txtCreateDateTime.Margin = new System.Windows.Forms.Padding(4);
            this.txtCreateDateTime.MaxLength = 0;
            this.txtCreateDateTime.Multiline = false;
            this.txtCreateDateTime.Name = "txtCreateDateTime";
            this.txtCreateDateTime.PasswordChar = '\0';
            this.txtCreateDateTime.ReadOnly = false;
            this.txtCreateDateTime.Size = new System.Drawing.Size(660, 32);
            this.txtCreateDateTime.TabIndex = 45;
            this.txtCreateDateTime.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // txtMemo
            // 
            this.txtMemo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMemo.HideSelection = true;
            this.txtMemo.Location = new System.Drawing.Point(84, 368);
            this.txtMemo.Margin = new System.Windows.Forms.Padding(4);
            this.txtMemo.MaxLength = 0;
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.PasswordChar = '\0';
            this.txtMemo.ReadOnly = false;
            this.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMemo.SelectedText = "";
            this.txtMemo.SelectionLength = 0;
            this.txtMemo.SelectionStart = 0;
            this.txtMemo.Size = new System.Drawing.Size(660, 182);
            this.txtMemo.TabIndex = 49;
            this.txtMemo.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // txtGrants
            // 
            this.txtGrants.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGrants.Location = new System.Drawing.Point(84, 221);
            this.txtGrants.Margin = new System.Windows.Forms.Padding(4);
            this.txtGrants.MaxLength = 0;
            this.txtGrants.Multiline = false;
            this.txtGrants.Name = "txtGrants";
            this.txtGrants.PasswordChar = '\0';
            this.txtGrants.ReadOnly = false;
            this.txtGrants.Size = new System.Drawing.Size(660, 32);
            this.txtGrants.TabIndex = 43;
            this.txtGrants.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // txtVersion
            // 
            this.txtVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVersion.Location = new System.Drawing.Point(84, 171);
            this.txtVersion.Margin = new System.Windows.Forms.Padding(4);
            this.txtVersion.MaxLength = 0;
            this.txtVersion.Multiline = false;
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.PasswordChar = '\0';
            this.txtVersion.ReadOnly = false;
            this.txtVersion.Size = new System.Drawing.Size(660, 32);
            this.txtVersion.TabIndex = 41;
            this.txtVersion.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // txtAppTitle
            // 
            this.txtAppTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAppTitle.Location = new System.Drawing.Point(84, 121);
            this.txtAppTitle.Margin = new System.Windows.Forms.Padding(4);
            this.txtAppTitle.MaxLength = 0;
            this.txtAppTitle.Multiline = false;
            this.txtAppTitle.Name = "txtAppTitle";
            this.txtAppTitle.PasswordChar = '\0';
            this.txtAppTitle.ReadOnly = false;
            this.txtAppTitle.Size = new System.Drawing.Size(660, 32);
            this.txtAppTitle.TabIndex = 39;
            this.txtAppTitle.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // txtAppName
            // 
            this.txtAppName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAppName.Location = new System.Drawing.Point(84, 71);
            this.txtAppName.Margin = new System.Windows.Forms.Padding(4);
            this.txtAppName.MaxLength = 0;
            this.txtAppName.Multiline = false;
            this.txtAppName.Name = "txtAppName";
            this.txtAppName.PasswordChar = '\0';
            this.txtAppName.ReadOnly = false;
            this.txtAppName.Size = new System.Drawing.Size(660, 32);
            this.txtAppName.TabIndex = 37;
            this.txtAppName.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // label8
            // 
            this.label8.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(5, 324);
            this.label8.Margin = new System.Windows.Forms.Padding(4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 18);
            this.label8.TabIndex = 46;
            this.label8.Text = "修改时间";
            // 
            // label7
            // 
            this.label7.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(5, 274);
            this.label7.Margin = new System.Windows.Forms.Padding(4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 18);
            this.label7.TabIndex = 44;
            this.label7.Text = "创建时间";
            // 
            // label6
            // 
            this.label6.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(5, 372);
            this.label6.Margin = new System.Windows.Forms.Padding(4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 18);
            this.label6.TabIndex = 48;
            this.label6.Text = "备     注";
            // 
            // label4
            // 
            this.label4.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(5, 224);
            this.label4.Margin = new System.Windows.Forms.Padding(4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 18);
            this.label4.TabIndex = 42;
            this.label4.Text = "授 权 码";
            // 
            // label3
            // 
            this.label3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(5, 174);
            this.label3.Margin = new System.Windows.Forms.Padding(4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 18);
            this.label3.TabIndex = 40;
            this.label3.Text = "版 本 号";
            // 
            // label2
            // 
            this.label2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(5, 124);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 18);
            this.label2.TabIndex = 38;
            this.label2.Text = "程序标题";
            // 
            // label1
            // 
            this.label1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(5, 74);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 18);
            this.label1.TabIndex = 36;
            this.label1.Text = "程序名称";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnNewApplication);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Location = new System.Drawing.Point(13, 606);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(752, 70);
            this.groupBox1.TabIndex = 40;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageRight = false;
            this.btnDelete.ImageStyle = HISPlus.UserControls.ImageStyle.Delete;
            this.btnDelete.Location = new System.Drawing.Point(492, 27);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnDelete.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 30);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.TextValue = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNewApplication
            // 
            this.btnNewApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewApplication.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNewApplication.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewApplication.Image = ((System.Drawing.Image)(resources.GetObject("btnNewApplication.Image")));
            this.btnNewApplication.ImageRight = false;
            this.btnNewApplication.ImageStyle = HISPlus.UserControls.ImageStyle.Add;
            this.btnNewApplication.Location = new System.Drawing.Point(340, 27);
            this.btnNewApplication.Margin = new System.Windows.Forms.Padding(4);
            this.btnNewApplication.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnNewApplication.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnNewApplication.Name = "btnNewApplication";
            this.btnNewApplication.Size = new System.Drawing.Size(90, 30);
            this.btnNewApplication.TabIndex = 5;
            this.btnNewApplication.TextValue = "新增";
            this.btnNewApplication.UseVisualStyleBackColor = true;
            this.btnNewApplication.Click += new System.EventHandler(this.btnNewApplication_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageRight = false;
            this.btnSave.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSave.Location = new System.Drawing.Point(644, 27);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnSave.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 7;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 680);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ApplicationInfo";
            this.Text = "应用程序信息";
            this.Load += new System.EventHandler(this.ApplicationInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl groupBox2;
        private System.Windows.Forms.CheckBox chkEnabled;
        private HISPlus.UserControls.UcTextBox txtAppCode;
        private DevExpress.XtraEditors.LabelControl label5;
        private HISPlus.UserControls.UcTextBox txtModifyDateTime;
        private HISPlus.UserControls.UcTextBox txtCreateDateTime;
        private HISPlus.UserControls.UcTextArea txtMemo;
        private HISPlus.UserControls.UcTextBox txtGrants;
        private HISPlus.UserControls.UcTextBox txtVersion;
        private HISPlus.UserControls.UcTextBox txtAppTitle;
        private HISPlus.UserControls.UcTextBox txtAppName;
        private DevExpress.XtraEditors.LabelControl label8;
        private DevExpress.XtraEditors.LabelControl label7;
        private DevExpress.XtraEditors.LabelControl label6;
        private DevExpress.XtraEditors.LabelControl label4;
        private DevExpress.XtraEditors.LabelControl label3;
        private DevExpress.XtraEditors.LabelControl label2;
        private DevExpress.XtraEditors.LabelControl label1;
        private DevExpress.XtraEditors.PanelControl groupBox1;
        private HISPlus.UserControls.UcButton btnDelete;
        private HISPlus.UserControls.UcButton btnNewApplication;
        private HISPlus.UserControls.UcButton btnSave;
    }
}