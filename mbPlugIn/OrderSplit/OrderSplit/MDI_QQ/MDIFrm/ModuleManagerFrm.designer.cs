using HISPlus.UserControls;

namespace HISPlus
{
    partial class ModuleManagerFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModuleManagerFrm));
            this.grpModuleDetail = new DevExpress.XtraEditors.PanelControl();
            this.btnSelectIcon = new HISPlus.UserControls.UcButton();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.btnDelete = new HISPlus.UserControls.UcButton();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.btnNew = new HISPlus.UserControls.UcButton();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.txtMemo = new HISPlus.UserControls.UcTextArea();
            this.txtIniPath = new HISPlus.UserControls.UcTextBox();
            this.txtModuleName = new HISPlus.UserControls.UcTextBox();
            this.txtModuleId = new HISPlus.UserControls.UcTextBox();
            this.label7 = new DevExpress.XtraEditors.LabelControl();
            this.label5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.grpModule = new DevExpress.XtraEditors.GroupControl();
            this.ucTreeList1 = new HISPlus.UserControls.UcTreeList();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnCancel = new HISPlus.UserControls.UcButton();
            ((System.ComponentModel.ISupportInitialize)(this.grpModuleDetail)).BeginInit();
            this.grpModuleDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpModule)).BeginInit();
            this.grpModule.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpModuleDetail
            // 
            this.grpModuleDetail.Controls.Add(this.btnCancel);
            this.grpModuleDetail.Controls.Add(this.btnSelectIcon);
            this.grpModuleDetail.Controls.Add(this.picIcon);
            this.grpModuleDetail.Controls.Add(this.btnDelete);
            this.grpModuleDetail.Controls.Add(this.btnSave);
            this.grpModuleDetail.Controls.Add(this.btnNew);
            this.grpModuleDetail.Controls.Add(this.chkEnabled);
            this.grpModuleDetail.Controls.Add(this.txtMemo);
            this.grpModuleDetail.Controls.Add(this.txtIniPath);
            this.grpModuleDetail.Controls.Add(this.txtModuleName);
            this.grpModuleDetail.Controls.Add(this.txtModuleId);
            this.grpModuleDetail.Controls.Add(this.label7);
            this.grpModuleDetail.Controls.Add(this.label5);
            this.grpModuleDetail.Controls.Add(this.labelControl1);
            this.grpModuleDetail.Controls.Add(this.label2);
            this.grpModuleDetail.Dock = System.Windows.Forms.DockStyle.Right;
            this.grpModuleDetail.Location = new System.Drawing.Point(680, 0);
            this.grpModuleDetail.Margin = new System.Windows.Forms.Padding(4);
            this.grpModuleDetail.Name = "grpModuleDetail";
            this.grpModuleDetail.Size = new System.Drawing.Size(345, 599);
            this.grpModuleDetail.TabIndex = 3;
            // 
            // btnSelectIcon
            // 
            this.btnSelectIcon.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSelectIcon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectIcon.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectIcon.Image")));
            this.btnSelectIcon.ImageRight = false;
            this.btnSelectIcon.ImageStyle = HISPlus.UserControls.ImageStyle.Show;
            this.btnSelectIcon.Location = new System.Drawing.Point(129, 103);
            this.btnSelectIcon.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectIcon.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnSelectIcon.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnSelectIcon.Name = "btnSelectIcon";
            this.btnSelectIcon.Size = new System.Drawing.Size(80, 30);
            this.btnSelectIcon.TabIndex = 22;
            this.btnSelectIcon.TextValue = "图标";
            this.btnSelectIcon.UseVisualStyleBackColor = true;
            this.btnSelectIcon.Click += new System.EventHandler(this.btnSelectIcon_Click);
            // 
            // picIcon
            // 
            this.picIcon.Location = new System.Drawing.Point(210, 103);
            this.picIcon.Margin = new System.Windows.Forms.Padding(4);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(64, 54);
            this.picIcon.TabIndex = 21;
            this.picIcon.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageRight = false;
            this.btnDelete.ImageStyle = HISPlus.UserControls.ImageStyle.Delete;
            this.btnDelete.Location = new System.Drawing.Point(128, 362);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnDelete.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 30);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.TextValue = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageRight = false;
            this.btnSave.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSave.Location = new System.Drawing.Point(232, 362);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnSave.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 15;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageRight = false;
            this.btnNew.ImageStyle = HISPlus.UserControls.ImageStyle.Add;
            this.btnNew.Location = new System.Drawing.Point(20, 362);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4);
            this.btnNew.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnNew.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(90, 30);
            this.btnNew.TabIndex = 14;
            this.btnNew.TextValue = "新增";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(21, 111);
            this.chkEnabled.Margin = new System.Windows.Forms.Padding(4);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(60, 22);
            this.chkEnabled.TabIndex = 13;
            this.chkEnabled.Text = "启用";
            this.chkEnabled.UseVisualStyleBackColor = true;
            this.chkEnabled.CheckedChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // txtMemo
            // 
            this.txtMemo.HideSelection = true;
            this.txtMemo.Location = new System.Drawing.Point(21, 198);
            this.txtMemo.Margin = new System.Windows.Forms.Padding(4);
            this.txtMemo.MaxCharLength = 0;
            this.txtMemo.MaxLength = 0;
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.PasswordChar = '\0';
            this.txtMemo.ReadOnly = false;
            this.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMemo.SelectedText = "";
            this.txtMemo.SelectionLength = 0;
            this.txtMemo.SelectionStart = 0;
            this.txtMemo.Size = new System.Drawing.Size(308, 155);
            this.txtMemo.TabIndex = 12;
            this.txtMemo.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // txtIniPath
            // 
            this.txtIniPath.Location = new System.Drawing.Point(99, 63);
            this.txtIniPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtIniPath.MaxLength = 0;
            this.txtIniPath.Multiline = false;
            this.txtIniPath.Name = "txtIniPath";
            this.txtIniPath.PasswordChar = '\0';
            this.txtIniPath.ReadOnly = false;
            this.txtIniPath.Size = new System.Drawing.Size(231, 32);
            this.txtIniPath.TabIndex = 11;
            this.txtIniPath.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // txtModuleName
            // 
            this.txtModuleName.Location = new System.Drawing.Point(99, 23);
            this.txtModuleName.Margin = new System.Windows.Forms.Padding(4);
            this.txtModuleName.MaxLength = 0;
            this.txtModuleName.Multiline = false;
            this.txtModuleName.Name = "txtModuleName";
            this.txtModuleName.PasswordChar = '\0';
            this.txtModuleName.ReadOnly = false;
            this.txtModuleName.Size = new System.Drawing.Size(231, 32);
            this.txtModuleName.TabIndex = 8;
            this.txtModuleName.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // txtModuleId
            // 
            this.txtModuleId.Location = new System.Drawing.Point(99, 473);
            this.txtModuleId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtModuleId.MaxLength = 0;
            this.txtModuleId.Multiline = false;
            this.txtModuleId.Name = "txtModuleId";
            this.txtModuleId.PasswordChar = '\0';
            this.txtModuleId.ReadOnly = true;
            this.txtModuleId.Size = new System.Drawing.Size(231, 32);
            this.txtModuleId.TabIndex = 7;
            this.txtModuleId.Visible = false;
            this.txtModuleId.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(20, 175);
            this.label7.Margin = new System.Windows.Forms.Padding(4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 18);
            this.label7.TabIndex = 6;
            this.label7.Text = "备    注";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(21, 63);
            this.label5.Margin = new System.Windows.Forms.Padding(4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 18);
            this.label5.TabIndex = 4;
            this.label5.Text = "配置文件";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(21, 478);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(46, 18);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "模块ID";
            this.labelControl1.Visible = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(21, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "模块名称";
            // 
            // grpModule
            // 
            this.grpModule.Controls.Add(this.ucTreeList1);
            this.grpModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpModule.Location = new System.Drawing.Point(0, 0);
            this.grpModule.Margin = new System.Windows.Forms.Padding(4);
            this.grpModule.Name = "grpModule";
            this.grpModule.Size = new System.Drawing.Size(680, 599);
            this.grpModule.TabIndex = 4;
            this.grpModule.Text = "模块列表";
            // 
            // ucTreeList1
            // 
            this.ucTreeList1.AllowDrag = false;
            this.ucTreeList1.DataSource = null;
            this.ucTreeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTreeList1.DragFlag = false;
            this.ucTreeList1.EnabledColumnName = "Enabled";
            this.ucTreeList1.FocusedNode = null;
            this.ucTreeList1.ImageColumnName = null;
            this.ucTreeList1.ImageList = null;
            this.ucTreeList1.KeyFieldName = "ID";
            this.ucTreeList1.LevelLimit = 0;
            this.ucTreeList1.Location = new System.Drawing.Point(2, 26);
            this.ucTreeList1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucTreeList1.MultiSelect = false;
            this.ucTreeList1.Name = "ucTreeList1";
            this.ucTreeList1.ParentFieldName = "ParentID";
            this.ucTreeList1.RootValue = 0;
            this.ucTreeList1.SelectedNode = null;
            this.ucTreeList1.ShowCheckBoxes = false;
            this.ucTreeList1.Size = new System.Drawing.Size(676, 571);
            this.ucTreeList1.TabIndex = 2;
            this.ucTreeList1.AfterSelect += new DevExpress.XtraTreeList.NodeEventHandler(this.ucTreeList1_AfterSelect);
            this.ucTreeList1.AfterDragNode += new DevExpress.XtraTreeList.NodeEventHandler(this.ucTreeList1_AfterDragNode);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageRight = false;
            this.btnCancel.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnCancel.Location = new System.Drawing.Point(-100, -100);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnCancel.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.TextValue = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ModuleManagerFrm
            // 
            this.AcceptButton = this.btnCancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1025, 599);
            this.Controls.Add(this.grpModule);
            this.Controls.Add(this.grpModuleDetail);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ModuleManagerFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模块管理";
            this.Load += new System.EventHandler(this.ModuleManagerFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpModuleDetail)).EndInit();
            this.grpModuleDetail.ResumeLayout(false);
            this.grpModuleDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpModule)).EndInit();
            this.grpModule.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl grpModuleDetail;
        private System.Windows.Forms.CheckBox chkEnabled;
        private HISPlus.UserControls.UcTextArea txtMemo;
        private HISPlus.UserControls.UcTextBox txtIniPath;
        private HISPlus.UserControls.UcTextBox txtModuleName;
        private DevExpress.XtraEditors.LabelControl label7;
        private DevExpress.XtraEditors.LabelControl label5;
        private DevExpress.XtraEditors.LabelControl label2;
        private DevExpress.XtraEditors.GroupControl grpModule;
        private HISPlus.UserControls.UcButton btnNew;
        private HISPlus.UserControls.UcButton btnSave;
        private HISPlus.UserControls.UcButton btnDelete;
        private UcTreeList ucTreeList1;
        private UserControls.UcTextBox txtModuleId;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private UserControls.UcButton btnSelectIcon;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private UcButton btnCancel;

    }
}