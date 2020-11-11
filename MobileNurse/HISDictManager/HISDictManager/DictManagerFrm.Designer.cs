using DevExpress.XtraEditors;

namespace HISPlus
{
    partial class DictManagerFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DictManagerFrm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ucTreeList1 = new HISPlus.UserControls.UcTreeList();
            this.groupBox4 = new DevExpress.XtraEditors.GroupControl();
            this.ucGridView1 = new HISPlus.UserControls.UcGridView();
            this.btnAdd = new HISPlus.UserControls.UcButton();
            this.btnSaveItem = new HISPlus.UserControls.UcButton();
            this.btnDeleteItem = new HISPlus.UserControls.UcButton();
            this.groupBox2 = new DevExpress.XtraEditors.GroupControl();
            this.txtNodeName = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new DevExpress.XtraEditors.LabelControl();
            this.txtNodeId = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new DevExpress.XtraEditors.LabelControl();
            this.txtMemoInt = new DevExpress.XtraEditors.TextEdit();
            this.btnSaveDict = new HISPlus.UserControls.UcButton();
            this.txtMemoStr = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new DevExpress.XtraEditors.LabelControl();
            this.label3 = new DevExpress.XtraEditors.LabelControl();
            this.txtDictName = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDictId = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.mnuContext_DictGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuDict_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDict_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDict_Append = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDict_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuGroup_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGroup_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox4)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNodeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNodeId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemoInt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemoStr.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDictName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDictId.Properties)).BeginInit();
            this.mnuContext_DictGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ucTreeList1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1069, 746);
            this.splitContainer1.SplitterDistance = 304;
            this.splitContainer1.SplitterWidth = 11;
            this.splitContainer1.TabIndex = 15;
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
            this.ucTreeList1.Location = new System.Drawing.Point(0, 0);
            this.ucTreeList1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucTreeList1.MultiSelect = false;
            this.ucTreeList1.Name = "ucTreeList1";
            this.ucTreeList1.ParentFieldName = "ParentID";
            this.ucTreeList1.RootValue = 0;
            this.ucTreeList1.SelectedNode = null;
            this.ucTreeList1.ShowCheckBoxes = false;
            this.ucTreeList1.Size = new System.Drawing.Size(304, 746);
            this.ucTreeList1.TabIndex = 2;
            this.ucTreeList1.AfterSelect += new DevExpress.XtraTreeList.NodeEventHandler(this.ucTreeList1_AfterSelect);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ucGridView1);
            this.groupBox4.Controls.Add(this.btnAdd);
            this.groupBox4.Controls.Add(this.btnSaveItem);
            this.groupBox4.Controls.Add(this.btnDeleteItem);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 158);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(754, 588);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.Text = "字典项目";
            // 
            // ucGridView1
            // 
            this.ucGridView1.AllowAddRows = false;
            this.ucGridView1.AllowDeleteRows = false;
            this.ucGridView1.AllowEdit = false;
            this.ucGridView1.AllowSort = false;
            this.ucGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucGridView1.ColumnAutoWidth = true;
            this.ucGridView1.ColumnsEvenOldRowColor = null;
            this.ucGridView1.DataSource = null;
            this.ucGridView1.Location = new System.Drawing.Point(11, 34);
            this.ucGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucGridView1.Name = "ucGridView1";
            this.ucGridView1.ShowRowIndicator = false;
            this.ucGridView1.Size = new System.Drawing.Size(731, 479);
            this.ucGridView1.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageRight = false;
            this.btnAdd.ImageStyle = HISPlus.UserControls.ImageStyle.Insert;
            this.btnAdd.Location = new System.Drawing.Point(421, 534);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnAdd.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 30);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.TextValue = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSaveItem
            // 
            this.btnSaveItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveItem.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSaveItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveItem.Image")));
            this.btnSaveItem.ImageRight = false;
            this.btnSaveItem.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSaveItem.Location = new System.Drawing.Point(644, 534);
            this.btnSaveItem.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveItem.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnSaveItem.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnSaveItem.Name = "btnSaveItem";
            this.btnSaveItem.Size = new System.Drawing.Size(90, 30);
            this.btnSaveItem.TabIndex = 1;
            this.btnSaveItem.TextValue = "保存";
            this.btnSaveItem.UseVisualStyleBackColor = true;
            this.btnSaveItem.Click += new System.EventHandler(this.btnSaveItem_Click);
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteItem.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDeleteItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteItem.Image")));
            this.btnDeleteItem.ImageRight = false;
            this.btnDeleteItem.ImageStyle = HISPlus.UserControls.ImageStyle.Delete;
            this.btnDeleteItem.Location = new System.Drawing.Point(533, 534);
            this.btnDeleteItem.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteItem.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnDeleteItem.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Size = new System.Drawing.Size(90, 30);
            this.btnDeleteItem.TabIndex = 0;
            this.btnDeleteItem.TextValue = "删除";
            this.btnDeleteItem.UseVisualStyleBackColor = true;
            this.btnDeleteItem.Click += new System.EventHandler(this.btnDeleteItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtNodeName);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtNodeId);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtMemoInt);
            this.groupBox2.Controls.Add(this.btnSaveDict);
            this.groupBox2.Controls.Add(this.txtMemoStr);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtDictName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtDictId);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(754, 158);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.Text = "字典信息";
            // 
            // txtNodeName
            // 
            this.txtNodeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNodeName.Location = new System.Drawing.Point(295, 38);
            this.txtNodeName.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtNodeName.Name = "txtNodeName";
            this.txtNodeName.Properties.MaxLength = 50;
            this.txtNodeName.Size = new System.Drawing.Size(361, 24);
            this.txtNodeName.TabIndex = 3;
            this.txtNodeName.TextChanged += new System.EventHandler(this.txtDictInfo_TextChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(214, 42);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 18);
            this.label5.TabIndex = 2;
            this.label5.Text = "节点名称";
            // 
            // txtNodeId
            // 
            this.txtNodeId.Location = new System.Drawing.Point(76, 38);
            this.txtNodeId.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtNodeId.Name = "txtNodeId";
            this.txtNodeId.Properties.MaxLength = 2;
            this.txtNodeId.Properties.ReadOnly = true;
            this.txtNodeId.Size = new System.Drawing.Size(129, 24);
            this.txtNodeId.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(13, 42);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 18);
            this.label6.TabIndex = 0;
            this.label6.Text = "节点ID";
            // 
            // txtMemoInt
            // 
            this.txtMemoInt.Location = new System.Drawing.Point(76, 119);
            this.txtMemoInt.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtMemoInt.Name = "txtMemoInt";
            this.txtMemoInt.Properties.MaxLength = 2;
            this.txtMemoInt.Size = new System.Drawing.Size(129, 24);
            this.txtMemoInt.TabIndex = 9;
            this.txtMemoInt.TextChanged += new System.EventHandler(this.txtDictInfo_TextChanged);
            // 
            // btnSaveDict
            // 
            this.btnSaveDict.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveDict.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSaveDict.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveDict.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveDict.Image")));
            this.btnSaveDict.ImageRight = false;
            this.btnSaveDict.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSaveDict.Location = new System.Drawing.Point(667, 38);
            this.btnSaveDict.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveDict.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnSaveDict.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnSaveDict.Name = "btnSaveDict";
            this.btnSaveDict.Size = new System.Drawing.Size(70, 30);
            this.btnSaveDict.TabIndex = 12;
            this.btnSaveDict.TextValue = "保存";
            this.btnSaveDict.UseVisualStyleBackColor = true;
            this.btnSaveDict.Click += new System.EventHandler(this.btnSaveDict_Click);
            // 
            // txtMemoStr
            // 
            this.txtMemoStr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMemoStr.Location = new System.Drawing.Point(295, 120);
            this.txtMemoStr.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtMemoStr.Name = "txtMemoStr";
            this.txtMemoStr.Properties.MaxLength = 100;
            this.txtMemoStr.Size = new System.Drawing.Size(361, 24);
            this.txtMemoStr.TabIndex = 11;
            this.txtMemoStr.TextChanged += new System.EventHandler(this.txtDictInfo_TextChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(229, 125);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 18);
            this.label4.TabIndex = 10;
            this.label4.Text = "备注2";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 125);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "备注1";
            // 
            // txtDictName
            // 
            this.txtDictName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDictName.Location = new System.Drawing.Point(295, 78);
            this.txtDictName.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtDictName.Name = "txtDictName";
            this.txtDictName.Properties.MaxLength = 50;
            this.txtDictName.Size = new System.Drawing.Size(361, 24);
            this.txtDictName.TabIndex = 7;
            this.txtDictName.TextChanged += new System.EventHandler(this.txtDictInfo_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(214, 83);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "字典名称";
            // 
            // txtDictId
            // 
            this.txtDictId.Location = new System.Drawing.Point(76, 78);
            this.txtDictId.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtDictId.Name = "txtDictId";
            this.txtDictId.Properties.MaxLength = 2;
            this.txtDictId.Properties.ReadOnly = true;
            this.txtDictId.Size = new System.Drawing.Size(129, 24);
            this.txtDictId.TabIndex = 5;
            this.txtDictId.TextChanged += new System.EventHandler(this.txtDictInfo_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 83);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "字典ID";
            // 
            // mnuContext_DictGroup
            // 
            this.mnuContext_DictGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDict_Add,
            this.mnuDict_Delete,
            this.mnuSep1,
            this.mnuDict_Append,
            this.mnuDict_Remove,
            this.mnuSep2,
            this.mnuGroup_Add,
            this.mnuGroup_Delete});
            this.mnuContext_DictGroup.Name = "contextMenuStrip1";
            this.mnuContext_DictGroup.Size = new System.Drawing.Size(139, 160);
            // 
            // mnuDict_Add
            // 
            this.mnuDict_Add.Name = "mnuDict_Add";
            this.mnuDict_Add.Size = new System.Drawing.Size(138, 24);
            this.mnuDict_Add.Text = "新增字典";
            this.mnuDict_Add.Click += new System.EventHandler(this.mnuDict_Add_Click);
            // 
            // mnuDict_Delete
            // 
            this.mnuDict_Delete.Name = "mnuDict_Delete";
            this.mnuDict_Delete.Size = new System.Drawing.Size(138, 24);
            this.mnuDict_Delete.Text = "删除字典";
            this.mnuDict_Delete.Click += new System.EventHandler(this.mnuDict_Delete_Click);
            // 
            // mnuSep1
            // 
            this.mnuSep1.Name = "mnuSep1";
            this.mnuSep1.Size = new System.Drawing.Size(135, 6);
            // 
            // mnuDict_Append
            // 
            this.mnuDict_Append.Name = "mnuDict_Append";
            this.mnuDict_Append.Size = new System.Drawing.Size(138, 24);
            this.mnuDict_Append.Text = "添加字典";
            this.mnuDict_Append.Click += new System.EventHandler(this.mnuDict_Append_Click);
            // 
            // mnuDict_Remove
            // 
            this.mnuDict_Remove.Name = "mnuDict_Remove";
            this.mnuDict_Remove.Size = new System.Drawing.Size(138, 24);
            this.mnuDict_Remove.Text = "移除字典";
            this.mnuDict_Remove.Click += new System.EventHandler(this.mnuDict_Remove_Click);
            // 
            // mnuSep2
            // 
            this.mnuSep2.Name = "mnuSep2";
            this.mnuSep2.Size = new System.Drawing.Size(135, 6);
            // 
            // mnuGroup_Add
            // 
            this.mnuGroup_Add.Name = "mnuGroup_Add";
            this.mnuGroup_Add.Size = new System.Drawing.Size(138, 24);
            this.mnuGroup_Add.Text = "新增分组";
            this.mnuGroup_Add.Click += new System.EventHandler(this.mnuGroup_Add_Click);
            // 
            // mnuGroup_Delete
            // 
            this.mnuGroup_Delete.Name = "mnuGroup_Delete";
            this.mnuGroup_Delete.Size = new System.Drawing.Size(138, 24);
            this.mnuGroup_Delete.Text = "删除分组";
            this.mnuGroup_Delete.Click += new System.EventHandler(this.mnuGroup_Delete_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Folder.ico");
            this.imageList1.Images.SetKeyName(1, "INDEX.ICO");
            // 
            // DictManagerFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 746);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DictManagerFrm";
            this.ShowInTaskbar = false;
            this.Text = "字典管理";
            this.Load += new System.EventHandler(this.DictManagerFrm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox4)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNodeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNodeId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemoInt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemoStr.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDictName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDictId.Properties)).EndInit();
            this.mnuContext_DictGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private GroupControl groupBox2;
        private TextEdit txtMemoStr;
        private LabelControl label4;
        private LabelControl label3;
        private TextEdit txtDictName;
        private LabelControl label2;
        private TextEdit txtDictId;
        private LabelControl label1;
        private GroupControl groupBox4;
        private System.Windows.Forms.ContextMenuStrip mnuContext_DictGroup;
        private System.Windows.Forms.ToolStripMenuItem mnuDict_Add;
        private System.Windows.Forms.ToolStripMenuItem mnuDict_Remove;
        private System.Windows.Forms.ToolStripMenuItem mnuDict_Delete;
        private System.Windows.Forms.ToolStripSeparator mnuSep1;
        private System.Windows.Forms.ToolStripMenuItem mnuGroup_Add;
        private System.Windows.Forms.ToolStripMenuItem mnuGroup_Delete;
        private HISPlus.UserControls.UcButton btnSaveDict;
        private HISPlus.UserControls.UcButton btnSaveItem;
        private HISPlus.UserControls.UcButton btnDeleteItem;
        private System.Windows.Forms.ImageList imageList1;
        private TextEdit txtMemoInt;
        private TextEdit txtNodeName;
        private LabelControl label5;
        private TextEdit txtNodeId;
        private LabelControl label6;
        private System.Windows.Forms.ToolStripMenuItem mnuDict_Append;
        private System.Windows.Forms.ToolStripSeparator mnuSep2;
        private UserControls.UcTreeList ucTreeList1;
        private UserControls.UcGridView ucGridView1;
        private UserControls.UcButton btnAdd;

    }
}