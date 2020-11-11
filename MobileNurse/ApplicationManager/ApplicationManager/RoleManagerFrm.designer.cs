namespace HISPlus
{
    partial class RoleManagerFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoleManagerFrm));
            this.groupBox1 = new DevExpress.XtraEditors.PanelControl();
            this.ucGridView1 = new HISPlus.UserControls.UcGridView();
            this.btnDelete = new HISPlus.UserControls.UcButton();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.btnNew = new HISPlus.UserControls.UcButton();
            this.txtRole = new HISPlus.UserControls.UcTextBox();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new DevExpress.XtraEditors.PanelControl();
            this.ucGridView2 = new HISPlus.UserControls.UcGridView();
            this.txtApp = new HISPlus.UserControls.UcTextBox();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox3 = new DevExpress.XtraEditors.PanelControl();
            this.ucTreeList1 = new HISPlus.UserControls.UcTreeList();
            this.btnSaveRights = new HISPlus.UserControls.UcButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox3)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.ucGridView1);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.txtRole);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 718);
            this.groupBox1.TabIndex = 4;
            // 
            // ucGridView1
            // 
            this.ucGridView1.AllowDeleteRows = false;
            this.ucGridView1.AllowEdit = false;
            this.ucGridView1.AllowSort = false;
            this.ucGridView1.DataSource = null;
            this.ucGridView1.Location = new System.Drawing.Point(8, 46);
            this.ucGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucGridView1.Name = "ucGridView1";
            this.ucGridView1.ShowRowIndicator = false;
            this.ucGridView1.Size = new System.Drawing.Size(251, 601);
            this.ucGridView1.TabIndex = 10;
            this.ucGridView1.SelectionChanged += new System.EventHandler(this.ucGridView1_SelectionChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageRight = false;
            this.btnDelete.ImageStyle = HISPlus.UserControls.ImageStyle.Delete;
            this.btnDelete.Location = new System.Drawing.Point(96, 666);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnDelete.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 30);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.TextValue = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageRight = false;
            this.btnSave.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSave.Location = new System.Drawing.Point(184, 666);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnSave.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 8;
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
            this.btnNew.Location = new System.Drawing.Point(8, 666);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4);
            this.btnNew.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnNew.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 30);
            this.btnNew.TabIndex = 7;
            this.btnNew.TextValue = "新增";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // txtRole
            // 
            this.txtRole.Location = new System.Drawing.Point(84, 16);
            this.txtRole.Margin = new System.Windows.Forms.Padding(4);
            this.txtRole.MaxLength = 0;
            this.txtRole.Multiline = false;
            this.txtRole.Name = "txtRole";
            this.txtRole.PasswordChar = '\0';
            this.txtRole.ReadOnly = false;
            this.txtRole.Size = new System.Drawing.Size(175, 32);
            this.txtRole.TabIndex = 5;
            this.txtRole.TextChanged += new System.EventHandler(this.txtRole_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "角色名称";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.ucGridView2);
            this.groupBox2.Controls.Add(this.txtApp);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(291, 9);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(408, 718);
            this.groupBox2.TabIndex = 5;
            // 
            // ucGridView2
            // 
            this.ucGridView2.AllowDeleteRows = false;
            this.ucGridView2.AllowEdit = false;
            this.ucGridView2.AllowSort = false;
            this.ucGridView2.DataSource = null;
            this.ucGridView2.Location = new System.Drawing.Point(14, 46);
            this.ucGridView2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucGridView2.Name = "ucGridView2";
            this.ucGridView2.ShowRowIndicator = false;
            this.ucGridView2.Size = new System.Drawing.Size(389, 650);
            this.ucGridView2.TabIndex = 11;
            this.ucGridView2.SelectionChanged += new System.EventHandler(this.ucGridView2_SelectionChanged);
            // 
            // txtApp
            // 
            this.txtApp.Enabled = false;
            this.txtApp.Location = new System.Drawing.Point(86, 16);
            this.txtApp.Margin = new System.Windows.Forms.Padding(4);
            this.txtApp.MaxLength = 0;
            this.txtApp.Multiline = false;
            this.txtApp.Name = "txtApp";
            this.txtApp.PasswordChar = '\0';
            this.txtApp.ReadOnly = false;
            this.txtApp.Size = new System.Drawing.Size(316, 32);
            this.txtApp.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "程序名称";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.ucTreeList1);
            this.groupBox3.Controls.Add(this.btnSaveRights);
            this.groupBox3.Location = new System.Drawing.Point(707, 6);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(358, 718);
            this.groupBox3.TabIndex = 6;
            // 
            // ucTreeList1
            // 
            this.ucTreeList1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucTreeList1.DataSource = null;
            this.ucTreeList1.ImageList = null;
            this.ucTreeList1.KeyFieldName = "ID";
            this.ucTreeList1.Location = new System.Drawing.Point(21, 19);
            this.ucTreeList1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucTreeList1.Name = "ucTreeList1";
            this.ucTreeList1.ParentFieldName = "ParentID";
            this.ucTreeList1.RootValue = 0;
            this.ucTreeList1.SelectedNode = null;
            this.ucTreeList1.ShowCheckBoxes = false;
            this.ucTreeList1.ShowHeader = true;
            this.ucTreeList1.Size = new System.Drawing.Size(318, 631);
            this.ucTreeList1.TabIndex = 10;
this.ucTreeList1.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.ucTreeList1_AfterCheckNode);
            // 
            // btnSaveRights
            // 
            this.btnSaveRights.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveRights.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSaveRights.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveRights.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveRights.Image")));
            this.btnSaveRights.ImageRight = false;
            this.btnSaveRights.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSaveRights.Location = new System.Drawing.Point(271, 669);
            this.btnSaveRights.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveRights.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnSaveRights.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnSaveRights.Name = "btnSaveRights";
            this.btnSaveRights.Size = new System.Drawing.Size(80, 30);
            this.btnSaveRights.TabIndex = 9;
            this.btnSaveRights.TextValue = "保存";
            this.btnSaveRights.UseVisualStyleBackColor = true;
            this.btnSaveRights.Click += new System.EventHandler(this.btnSaveRights_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // RoleManagerFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 742);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RoleManagerFrm";
            this.Text = "角色管理";
            this.Load += new System.EventHandler(this.RoleManagerFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl groupBox1;
        private HISPlus.UserControls.UcTextBox txtRole;
        private DevExpress.XtraEditors.LabelControl label1;
        private DevExpress.XtraEditors.PanelControl groupBox2;
        private DevExpress.XtraEditors.PanelControl groupBox3;
        private HISPlus.UserControls.UcButton btnSave;
        private HISPlus.UserControls.UcButton btnNew;
        private HISPlus.UserControls.UcButton btnDelete;
        private System.Windows.Forms.ImageList imageList1;
        private HISPlus.UserControls.UcButton btnSaveRights;
        private HISPlus.UserControls.UcTextBox txtApp;
        private DevExpress.XtraEditors.LabelControl label2;
        private UserControls.UcGridView ucGridView1;
        private UserControls.UcGridView ucGridView2;
        private UserControls.UcTreeList ucTreeList1;


    }
}