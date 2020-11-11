using DevExpress.XtraEditors;

namespace HISPlus
{
    partial class UserManagerFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserManagerFrm));
            this.gvDept = new HISPlus.UserControls.UcGridView();
            this.gvUser = new HISPlus.UserControls.UcGridView();
            this.ucTreeList1 = new HISPlus.UserControls.UcTreeList();
            this.groupBox4 = new DevExpress.XtraEditors.PanelControl();
            this.btnClearPwd = new HISPlus.UserControls.UcButton();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox4)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvDept
            // 
            this.gvDept.AllowAddRows = false;
            this.gvDept.AllowDeleteRows = false;
            this.gvDept.AllowEdit = false;
            this.gvDept.AllowSort = false;
            this.gvDept.ColumnAutoWidth = true;
            this.gvDept.ColumnsEvenOldRowColor = null;
            this.gvDept.DataSource = null;
            this.gvDept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvDept.Location = new System.Drawing.Point(2, 26);
            this.gvDept.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gvDept.Name = "gvDept";
            this.gvDept.ShowRowIndicator = false;
            this.gvDept.Size = new System.Drawing.Size(332, 623);
            this.gvDept.TabIndex = 2;
            // 
            // gvUser
            // 
            this.gvUser.AllowAddRows = false;
            this.gvUser.AllowDeleteRows = false;
            this.gvUser.AllowEdit = false;
            this.gvUser.AllowSort = false;
            this.gvUser.ColumnAutoWidth = true;
            this.gvUser.ColumnsEvenOldRowColor = null;
            this.gvUser.DataSource = null;
            this.gvUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvUser.Location = new System.Drawing.Point(2, 26);
            this.gvUser.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gvUser.Name = "gvUser";
            this.gvUser.ShowRowIndicator = false;
            this.gvUser.Size = new System.Drawing.Size(309, 623);
            this.gvUser.TabIndex = 2;
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
            this.ucTreeList1.Size = new System.Drawing.Size(382, 622);
            this.ucTreeList1.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnClearPwd);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.Location = new System.Drawing.Point(12, 664);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(1046, 70);
            this.groupBox4.TabIndex = 3;
            // 
            // btnClearPwd
            // 
            this.btnClearPwd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearPwd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClearPwd.Enabled = false;
            this.btnClearPwd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPwd.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPwd.Image")));
            this.btnClearPwd.ImageRight = false;
            this.btnClearPwd.ImageStyle = HISPlus.UserControls.ImageStyle.Refresh;
            this.btnClearPwd.Location = new System.Drawing.Point(815, 27);
            this.btnClearPwd.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearPwd.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnClearPwd.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnClearPwd.Name = "btnClearPwd";
            this.btnClearPwd.Size = new System.Drawing.Size(90, 30);
            this.btnClearPwd.TabIndex = 1;
            this.btnClearPwd.TextValue = "重置密码";
            this.btnClearPwd.UseVisualStyleBackColor = true;
            this.btnClearPwd.Click += new System.EventHandler(this.btnClearPwd_Click);
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
            this.btnSave.Location = new System.Drawing.Point(938, 27);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnSave.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gvDept);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(336, 651);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "科室列表";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gvUser);
            this.groupControl2.Location = new System.Drawing.Point(354, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(313, 651);
            this.groupControl2.TabIndex = 3;
            this.groupControl2.Text = "用户列表";
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.ucTreeList1);
            this.groupControl3.Location = new System.Drawing.Point(673, 13);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(386, 650);
            this.groupControl3.TabIndex = 4;
            this.groupControl3.Text = "角色列表";
            // 
            // UserManagerFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 742);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.groupBox4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserManagerFrm";
            this.Text = "用户管理";
            this.Load += new System.EventHandler(this.UserManagerFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox4)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PanelControl groupBox4;
        private HISPlus.UserControls.UcButton btnSave;
        private HISPlus.UserControls.UcButton btnClearPwd;
        private UserControls.UcGridView gvDept;
        private UserControls.UcGridView gvUser;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private UserControls.UcTreeList ucTreeList1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl3;
    }
}