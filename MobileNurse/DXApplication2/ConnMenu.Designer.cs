namespace DXApplication2
{
    partial class ConnMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnMenu));
            this.btnDelete = new HISPlus.UserControls.UcButton();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.trlUser = new HISPlus.UserControls.UcTreeList();
            this.trlMenus = new HISPlus.UserControls.UcTreeList();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageRight = false;
            this.btnDelete.ImageStyle = HISPlus.UserControls.ImageStyle.Delete;
            this.btnDelete.Location = new System.Drawing.Point(614, 35);
            this.btnDelete.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnDelete.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.TextValue = "删除";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageRight = false;
            this.btnSave.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSave.Location = new System.Drawing.Point(614, 489);
            this.btnSave.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnSave.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 13;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // trlUser
            // 
            this.trlUser.AllowDrag = false;
            this.trlUser.DataSource = null;
            this.trlUser.DragFlag = false;
            this.trlUser.EnabledColumnName = "Enabled";
            this.trlUser.ImageColumnName = null;
            this.trlUser.ImageList = null;
            this.trlUser.KeyFieldName = "ID";
            this.trlUser.LevelLimit = 0;
            this.trlUser.Location = new System.Drawing.Point(297, 14);
            this.trlUser.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.trlUser.Name = "trlUser";
            this.trlUser.ParentFieldName = "ParentID";
            this.trlUser.RootValue = 0;
            this.trlUser.SelectedNode = null;
            this.trlUser.ShowCheckBoxes = false;
            this.trlUser.ShowHeader = false;
            this.trlUser.Size = new System.Drawing.Size(260, 527);
            this.trlUser.TabIndex = 12;
            this.trlUser.AfterDragNode += new DevExpress.XtraTreeList.NodeEventHandler(this.trlUser_AfterDragNode);
            this.trlUser.DragDrop += new System.Windows.Forms.DragEventHandler(this.trlUser_DragDrop);
            // 
            // trlMenus
            // 
            this.trlMenus.AllowDrag = false;
            this.trlMenus.DataSource = null;
            this.trlMenus.DragFlag = false;
            this.trlMenus.EnabledColumnName = "Enabled";
            this.trlMenus.ImageColumnName = null;
            this.trlMenus.ImageList = null;
            this.trlMenus.KeyFieldName = "ID";
            this.trlMenus.LevelLimit = 0;
            this.trlMenus.Location = new System.Drawing.Point(12, 14);
            this.trlMenus.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.trlMenus.Name = "trlMenus";
            this.trlMenus.ParentFieldName = "ParentID";
            this.trlMenus.RootValue = 0;
            this.trlMenus.SelectedNode = null;
            this.trlMenus.ShowCheckBoxes = false;
            this.trlMenus.ShowHeader = false;
            this.trlMenus.Size = new System.Drawing.Size(260, 527);
            this.trlMenus.TabIndex = 11;
            // 
            // ConnMenu
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 555);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.trlUser);
            this.Controls.Add(this.trlMenus);
            this.Name = "ConnMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "关联菜单配置";
            this.Load += new System.EventHandler(this.ConnMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private HISPlus.UserControls.UcTreeList trlMenus;
        private HISPlus.UserControls.UcTreeList trlUser;
        private HISPlus.UserControls.UcButton btnSave;
        private HISPlus.UserControls.UcButton btnDelete;
    }
}