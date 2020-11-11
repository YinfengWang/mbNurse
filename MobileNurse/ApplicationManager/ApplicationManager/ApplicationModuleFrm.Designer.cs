namespace HISPlus
{
    partial class ApplicationModuleFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationModuleFrm));
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.groupBox1 = new DevExpress.XtraEditors.PanelControl();
            this.dgvModule = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MODULE_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MODULE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MODULE_VERSION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DLL_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FORM_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MEMO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FORM_GUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new DevExpress.XtraEditors.PanelControl();
            this.chkBackGround = new System.Windows.Forms.CheckBox();
            this.btnSelectIcon = new HISPlus.UserControls.UcButton();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.btnNew = new HISPlus.UserControls.UcButton();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.btnDelete = new HISPlus.UserControls.UcButton();
            this.btnSearch = new HISPlus.UserControls.UcButton();
            this.txtModuleName = new HISPlus.UserControls.UcTextBox();
            this.label4 = new DevExpress.XtraEditors.LabelControl();
            this.txtModuleCode = new HISPlus.UserControls.UcTextBox();
            this.label3 = new DevExpress.XtraEditors.LabelControl();
            this.txtNodeTitle = new HISPlus.UserControls.UcTextBox();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.txtNodeId = new HISPlus.UserControls.UcTextBox();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnUp = new HISPlus.UserControls.UcButton();
            this.btnDown = new HISPlus.UserControls.UcButton();
            this.btnDownload = new HISPlus.UserControls.UcButton();
            this.ucTreeList1 = new HISPlus.UserControls.UcTreeList();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvModule);
            this.groupBox1.Location = new System.Drawing.Point(553, 241);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 466);
            this.groupBox1.TabIndex = 1;
            // 
            // dgvModule
            // 
            this.dgvModule.AllowUserToAddRows = false;
            this.dgvModule.AllowUserToDeleteRows = false;
            this.dgvModule.AllowUserToResizeRows = false;
            this.dgvModule.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvModule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.MODULE_CODE,
            this.MODULE_NAME,
            this.MODULE_VERSION,
            this.DLL_NAME,
            this.FORM_TYPE,
            this.Column5,
            this.MEMO,
            this.FORM_GUID});
            this.dgvModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvModule.Location = new System.Drawing.Point(2, 2);
            this.dgvModule.Margin = new System.Windows.Forms.Padding(4);
            this.dgvModule.MultiSelect = false;
            this.dgvModule.Name = "dgvModule";
            this.dgvModule.ReadOnly = true;
            this.dgvModule.RowHeadersVisible = false;
            this.dgvModule.RowTemplate.Height = 23;
            this.dgvModule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvModule.Size = new System.Drawing.Size(440, 462);
            this.dgvModule.TabIndex = 1;
            this.dgvModule.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvModule_RowPostPaint);
            this.dgvModule.DoubleClick += new System.EventHandler(this.dgvModule_DoubleClick);
            // 
            // Column1
            // 
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 36;
            // 
            // MODULE_CODE
            // 
            this.MODULE_CODE.DataPropertyName = "MODULE_CODE";
            this.MODULE_CODE.Frozen = true;
            this.MODULE_CODE.HeaderText = "模块编码";
            this.MODULE_CODE.Name = "MODULE_CODE";
            this.MODULE_CODE.ReadOnly = true;
            this.MODULE_CODE.Width = 60;
            // 
            // MODULE_NAME
            // 
            this.MODULE_NAME.DataPropertyName = "MODULE_NAME";
            this.MODULE_NAME.Frozen = true;
            this.MODULE_NAME.HeaderText = "模块名称";
            this.MODULE_NAME.Name = "MODULE_NAME";
            this.MODULE_NAME.ReadOnly = true;
            this.MODULE_NAME.Width = 120;
            // 
            // MODULE_VERSION
            // 
            this.MODULE_VERSION.DataPropertyName = "MODULE_VERSION";
            this.MODULE_VERSION.Frozen = true;
            this.MODULE_VERSION.HeaderText = "版本号";
            this.MODULE_VERSION.Name = "MODULE_VERSION";
            this.MODULE_VERSION.ReadOnly = true;
            this.MODULE_VERSION.Width = 50;
            // 
            // DLL_NAME
            // 
            this.DLL_NAME.DataPropertyName = "DLL_NAME";
            this.DLL_NAME.Frozen = true;
            this.DLL_NAME.HeaderText = "动态库名称";
            this.DLL_NAME.Name = "DLL_NAME";
            this.DLL_NAME.ReadOnly = true;
            this.DLL_NAME.Width = 120;
            // 
            // FORM_TYPE
            // 
            this.FORM_TYPE.DataPropertyName = "FORM_TYPE";
            this.FORM_TYPE.Frozen = true;
            this.FORM_TYPE.HeaderText = "类名";
            this.FORM_TYPE.Name = "FORM_TYPE";
            this.FORM_TYPE.ReadOnly = true;
            this.FORM_TYPE.Width = 160;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "ENABLED";
            this.Column5.FalseValue = "0";
            this.Column5.Frozen = true;
            this.Column5.HeaderText = "启用";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.TrueValue = "1";
            this.Column5.Width = 36;
            // 
            // MEMO
            // 
            this.MEMO.DataPropertyName = "MEMO";
            this.MEMO.Frozen = true;
            this.MEMO.HeaderText = "备注";
            this.MEMO.Name = "MEMO";
            this.MEMO.ReadOnly = true;
            this.MEMO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MEMO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FORM_GUID
            // 
            this.FORM_GUID.DataPropertyName = "FORM_GUID";
            this.FORM_GUID.Frozen = true;
            this.FORM_GUID.HeaderText = "GUID";
            this.FORM_GUID.Name = "FORM_GUID";
            this.FORM_GUID.ReadOnly = true;
            this.FORM_GUID.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkBackGround);
            this.groupBox2.Controls.Add(this.btnSelectIcon);
            this.groupBox2.Controls.Add(this.picIcon);
            this.groupBox2.Controls.Add(this.btnNew);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.txtModuleName);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtModuleCode);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtNodeTitle);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtNodeId);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(553, 8);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(444, 225);
            this.groupBox2.TabIndex = 2;
            // 
            // chkBackGround
            // 
            this.chkBackGround.AutoSize = true;
            this.chkBackGround.Location = new System.Drawing.Point(335, 121);
            this.chkBackGround.Margin = new System.Windows.Forms.Padding(4);
            this.chkBackGround.Name = "chkBackGround";
            this.chkBackGround.Size = new System.Drawing.Size(90, 22);
            this.chkBackGround.TabIndex = 18;
            this.chkBackGround.Text = "后台运行";
            this.chkBackGround.UseVisualStyleBackColor = true;
            // 
            // btnSelectIcon
            // 
            this.btnSelectIcon.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSelectIcon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectIcon.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectIcon.Image")));
            this.btnSelectIcon.ImageRight = false;
            this.btnSelectIcon.ImageStyle = HISPlus.UserControls.ImageStyle.Show;
            this.btnSelectIcon.Location = new System.Drawing.Point(15, 120);
            this.btnSelectIcon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelectIcon.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnSelectIcon.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnSelectIcon.Name = "btnSelectIcon";
            this.btnSelectIcon.Size = new System.Drawing.Size(80, 30);
            this.btnSelectIcon.TabIndex = 17;
            this.btnSelectIcon.TextValue = "图标";
            this.btnSelectIcon.UseVisualStyleBackColor = true;
            this.btnSelectIcon.Click += new System.EventHandler(this.btnSelectIcon_Click);
            // 
            // picIcon
            // 
            this.picIcon.Location = new System.Drawing.Point(96, 120);
            this.picIcon.Margin = new System.Windows.Forms.Padding(4);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(64, 54);
            this.picIcon.TabIndex = 16;
            this.picIcon.TabStop = false;
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageRight = false;
            this.btnNew.ImageStyle = HISPlus.UserControls.ImageStyle.Add;
            this.btnNew.Location = new System.Drawing.Point(8, 182);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNew.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnNew.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(90, 30);
            this.btnNew.TabIndex = 14;
            this.btnNew.TextValue = "新增";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageRight = false;
            this.btnSave.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSave.Location = new System.Drawing.Point(328, 182);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnSave.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 13;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageRight = false;
            this.btnDelete.ImageStyle = HISPlus.UserControls.ImageStyle.Delete;
            this.btnDelete.Location = new System.Drawing.Point(223, 182);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDelete.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnDelete.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 30);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.TextValue = "删除节点";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageRight = false;
            this.btnSearch.ImageStyle = HISPlus.UserControls.ImageStyle.Find;
            this.btnSearch.Location = new System.Drawing.Point(115, 182);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearch.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnSearch.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 30);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.TextValue = "查找模块";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtModuleName
            // 
            this.txtModuleName.Location = new System.Drawing.Point(279, 80);
            this.txtModuleName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtModuleName.MaxLength = 0;
            this.txtModuleName.Multiline = false;
            this.txtModuleName.Name = "txtModuleName";
            this.txtModuleName.PasswordChar = '\0';
            this.txtModuleName.ReadOnly = false;
            this.txtModuleName.Size = new System.Drawing.Size(144, 32);
            this.txtModuleName.TabIndex = 8;
            this.txtModuleName.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(201, 86);
            this.label4.Margin = new System.Windows.Forms.Padding(4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "模块名称";
            // 
            // txtModuleCode
            // 
            this.txtModuleCode.Location = new System.Drawing.Point(87, 80);
            this.txtModuleCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtModuleCode.MaxLength = 0;
            this.txtModuleCode.Multiline = false;
            this.txtModuleCode.Name = "txtModuleCode";
            this.txtModuleCode.PasswordChar = '\0';
            this.txtModuleCode.ReadOnly = false;
            this.txtModuleCode.Size = new System.Drawing.Size(93, 32);
            this.txtModuleCode.TabIndex = 6;
            this.txtModuleCode.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 86);
            this.label3.Margin = new System.Windows.Forms.Padding(4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "模块编码";
            // 
            // txtNodeTitle
            // 
            this.txtNodeTitle.Location = new System.Drawing.Point(279, 30);
            this.txtNodeTitle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNodeTitle.MaxLength = 0;
            this.txtNodeTitle.Multiline = false;
            this.txtNodeTitle.Name = "txtNodeTitle";
            this.txtNodeTitle.PasswordChar = '\0';
            this.txtNodeTitle.ReadOnly = false;
            this.txtNodeTitle.Size = new System.Drawing.Size(144, 32);
            this.txtNodeTitle.TabIndex = 3;
            this.txtNodeTitle.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(201, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "节点名称";
            // 
            // txtNodeId
            // 
            this.txtNodeId.Enabled = false;
            this.txtNodeId.Location = new System.Drawing.Point(87, 30);
            this.txtNodeId.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNodeId.MaxLength = 0;
            this.txtNodeId.Multiline = false;
            this.txtNodeId.Name = "txtNodeId";
            this.txtNodeId.PasswordChar = '\0';
            this.txtNodeId.ReadOnly = false;
            this.txtNodeId.Size = new System.Drawing.Size(93, 32);
            this.txtNodeId.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "节  点ID";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.ImageRight = false;
            this.btnUp.ImageStyle = HISPlus.UserControls.ImageStyle.Previous;
            this.btnUp.Location = new System.Drawing.Point(16, 668);
            this.btnUp.Margin = new System.Windows.Forms.Padding(4);
            this.btnUp.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnUp.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(90, 30);
            this.btnUp.TabIndex = 14;
            this.btnUp.TextValue = "上移";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Visible = false;
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDown.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.ImageRight = false;
            this.btnDown.ImageStyle = HISPlus.UserControls.ImageStyle.Next;
            this.btnDown.Location = new System.Drawing.Point(136, 668);
            this.btnDown.Margin = new System.Windows.Forms.Padding(4);
            this.btnDown.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnDown.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(90, 30);
            this.btnDown.TabIndex = 15;
            this.btnDown.TextValue = "下移";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Visible = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDownload.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.Image = ((System.Drawing.Image)(resources.GetObject("btnDownload.Image")));
            this.btnDownload.ImageRight = false;
            this.btnDownload.ImageStyle = HISPlus.UserControls.ImageStyle.Download;
            this.btnDownload.Location = new System.Drawing.Point(445, 668);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(4);
            this.btnDownload.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnDownload.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(90, 30);
            this.btnDownload.TabIndex = 16;
            this.btnDownload.TextValue = "下载";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // ucTreeList1
            // 
            this.ucTreeList1.AllowDrag = false;
            this.ucTreeList1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ucTreeList1.DataSource = null;
            this.ucTreeList1.ImageColumnName = null;
            this.ucTreeList1.ImageList = null;
            this.ucTreeList1.KeyFieldName = "ID";
            this.ucTreeList1.Location = new System.Drawing.Point(12, 13);
            this.ucTreeList1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucTreeList1.Name = "ucTreeList1";
            this.ucTreeList1.ParentFieldName = "ParentID";
            this.ucTreeList1.RootValue = 0;
            this.ucTreeList1.SelectedNode = null;
            this.ucTreeList1.ShowCheckBoxes = false;
            this.ucTreeList1.ShowHeader = true;
            this.ucTreeList1.Size = new System.Drawing.Size(523, 632);
            this.ucTreeList1.TabIndex = 17;
            this.ucTreeList1.AfterSelect += new DevExpress.XtraTreeList.NodeEventHandler(this.ucTreeList1_AfterSelect);
            // 
            // ApplicationModuleFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 724);
            this.Controls.Add(this.ucTreeList1);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ApplicationModuleFrm";
            this.Text = "应用程序模块管理";
            this.Load += new System.EventHandler(this.ApplicationModuleFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvModule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl groupBox1;
        private DevExpress.XtraEditors.PanelControl groupBox2;
        private DevExpress.XtraEditors.LabelControl label1;
        private HISPlus.UserControls.UcTextBox txtNodeId;
        private HISPlus.UserControls.UcTextBox txtNodeTitle;
        private DevExpress.XtraEditors.LabelControl label2;
        private HISPlus.UserControls.UcTextBox txtModuleName;
        private DevExpress.XtraEditors.LabelControl label4;
        private HISPlus.UserControls.UcTextBox txtModuleCode;
        private DevExpress.XtraEditors.LabelControl label3;
        private HISPlus.UserControls.UcButton btnSearch;
        private HISPlus.UserControls.UcButton btnSave;
        private HISPlus.UserControls.UcButton btnDelete;
        private System.Windows.Forms.DataGridView dgvModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MODULE_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn MODULE_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn MODULE_VERSION;
        private System.Windows.Forms.DataGridViewTextBoxColumn DLL_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn FORM_TYPE;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn MEMO;
        private System.Windows.Forms.DataGridViewTextBoxColumn FORM_GUID;
        private HISPlus.UserControls.UcButton btnNew;
        private System.Windows.Forms.PictureBox picIcon;
        private HISPlus.UserControls.UcButton btnSelectIcon;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox chkBackGround;
        private HISPlus.UserControls.UcButton btnUp;
        private HISPlus.UserControls.UcButton btnDown;
        private HISPlus.UserControls.UcButton btnDownload;
        private UserControls.UcTreeList ucTreeList1;
    }
}