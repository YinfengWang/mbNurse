namespace HISPlus
{
    partial class DictItemContentFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DictItemContentFrm));
            this.ucTreeList1 = new HISPlus.UserControls.UcTreeList();
            this.txtContent = new HISPlus.UserControls.UcTextArea();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
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
            this.ucTreeList1.Location = new System.Drawing.Point(2, 22);
            this.ucTreeList1.MultiSelect = false;
            this.ucTreeList1.Name = "ucTreeList1";
            this.ucTreeList1.ParentFieldName = "ParentID";
            this.ucTreeList1.RootValue = 0;
            this.ucTreeList1.SelectedNode = null;
            this.ucTreeList1.ShowCheckBoxes = false;
            this.ucTreeList1.Size = new System.Drawing.Size(274, 586);
            this.ucTreeList1.TabIndex = 1;
            this.ucTreeList1.AfterSelect += new DevExpress.XtraTreeList.NodeEventHandler(this.ucTreeList1_AfterSelect);
            // 
            // txtContent
            // 
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.HideSelection = true;
            this.txtContent.Location = new System.Drawing.Point(2, 22);
            this.txtContent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtContent.MaxCharLength = 0;
            this.txtContent.MaxLength = 1000;
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.PasswordChar = '\0';
            this.txtContent.ReadOnly = false;
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContent.SelectedText = "";
            this.txtContent.SelectionLength = 0;
            this.txtContent.SelectionStart = 0;
            this.txtContent.Size = new System.Drawing.Size(584, 499);
            this.txtContent.TabIndex = 0;
            this.txtContent.TextChanged += new System.EventHandler(this.txtContent_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageRight = false;
            this.btnSave.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSave.Location = new System.Drawing.Point(471, 8);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.MaximumSize = new System.Drawing.Size(79, 23);
            this.btnSave.MinimumSize = new System.Drawing.Size(70, 23);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(79, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupControl1.Controls.Add(this.ucTreeList1);
            this.groupControl1.Location = new System.Drawing.Point(10, 9);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(278, 610);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "项目列表";
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.txtContent);
            this.groupControl2.Location = new System.Drawing.Point(294, 9);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(588, 523);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = "项目内容";
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Location = new System.Drawing.Point(294, 535);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(588, 40);
            this.panelControl1.TabIndex = 3;
            // 
            // DictItemContentFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 576);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "DictItemContentFrm";
            this.ShowInTaskbar = false;
            this.Text = "字典项目内容管理";
            this.Load += new System.EventHandler(this.DictItemContentFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HISPlus.UserControls.UcButton btnSave;
        public HISPlus.UserControls.UcTextArea txtContent;
        private UserControls.UcTreeList ucTreeList1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}