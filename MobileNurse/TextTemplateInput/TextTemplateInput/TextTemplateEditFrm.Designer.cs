namespace HISPlus
{
    partial class TextTemplateEditFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextTemplateEditFrm));
            this.groupBox1 = new DevExpress.XtraEditors.PanelControl();
            this.trvTemplate = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.groupBox2 = new DevExpress.XtraEditors.PanelControl();
            this.txtTemplateTitle = new HISPlus.UserControls.UcTextArea();
            this.groupBox3 = new DevExpress.XtraEditors.PanelControl();
            this.btnSaveOrder = new HISPlus.UserControls.UcButton();
            this.btnDelete = new HISPlus.UserControls.UcButton();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.btnExit = new HISPlus.UserControls.UcButton();
            this.timer1 = new System.Windows.Forms.Timer();
            this.groupBox4 = new DevExpress.XtraEditors.PanelControl();
            this.txtTemplateContent = new HISPlus.UserControls.UcTextArea();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox3)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox4)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.trvTemplate);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 509);
            this.groupBox1.TabIndex = 0;
            // 
            // trvTemplate
            // 
            this.trvTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvTemplate.HideSelection = false;
            this.trvTemplate.ImageIndex = 0;
            this.trvTemplate.ImageList = this.imageList1;
            this.trvTemplate.Location = new System.Drawing.Point(2, 2);
            this.trvTemplate.Margin = new System.Windows.Forms.Padding(4);
            this.trvTemplate.Name = "trvTemplate";
            this.trvTemplate.SelectedImageIndex = 0;
            this.trvTemplate.Size = new System.Drawing.Size(304, 505);
            this.trvTemplate.TabIndex = 0;
            this.trvTemplate.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvTemplate_AfterSelect);
            this.trvTemplate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.trvTemplate_KeyUp);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Orders.ICO");
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtTemplateTitle);
            this.groupBox2.Location = new System.Drawing.Point(332, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(580, 78);
            this.groupBox2.TabIndex = 1;
            // 
            // txtTemplateTitle
            // 
            this.txtTemplateTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTemplateTitle.HideSelection = true;
            this.txtTemplateTitle.Location = new System.Drawing.Point(2, 2);
            this.txtTemplateTitle.Margin = new System.Windows.Forms.Padding(4);
            this.txtTemplateTitle.MaxLength = 0;
            this.txtTemplateTitle.Multiline = true;
            this.txtTemplateTitle.Name = "txtTemplateTitle";
            this.txtTemplateTitle.PasswordChar = '\0';
            this.txtTemplateTitle.ReadOnly = false;
            this.txtTemplateTitle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTemplateTitle.SelectedText = "";
            this.txtTemplateTitle.SelectionLength = 0;
            this.txtTemplateTitle.SelectionStart = 0;
            this.txtTemplateTitle.Size = new System.Drawing.Size(576, 74);
            this.txtTemplateTitle.TabIndex = 0;
            this.txtTemplateTitle.TextChanged += new System.EventHandler(this.txtTemplateTitle_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnSaveOrder);
            this.groupBox3.Controls.Add(this.btnDelete);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Controls.Add(this.btnExit);
            this.groupBox3.Location = new System.Drawing.Point(16, 531);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(896, 64);
            this.groupBox3.TabIndex = 2;
            // 
            // btnSaveOrder
            // 
            this.btnSaveOrder.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSaveOrder.Enabled = false;
            this.btnSaveOrder.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveOrder.Image")));
            this.btnSaveOrder.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSaveOrder.Location = new System.Drawing.Point(20, 20);
            this.btnSaveOrder.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveOrder.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnSaveOrder.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnSaveOrder.Name = "btnSaveOrder";
            this.btnSaveOrder.Size = new System.Drawing.Size(100, 30);
            this.btnSaveOrder.TabIndex = 6;
            this.btnSaveOrder.TextValue = "保存排序";
            this.btnSaveOrder.UseVisualStyleBackColor = true;
            this.btnSaveOrder.Click += new System.EventHandler(this.btnSaveOrder_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageStyle = HISPlus.UserControls.ImageStyle.Delete;
            this.btnDelete.Location = new System.Drawing.Point(186, 19);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnDelete.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.TextValue = "删除模板";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Enabled = false;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSave.Location = new System.Drawing.Point(640, 19);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnSave.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 1;
            this.btnSave.TextValue = "保存(&O)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageStyle = HISPlus.UserControls.ImageStyle.Close;
            this.btnExit.Location = new System.Drawing.Point(768, 19);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnExit.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 30);
            this.btnExit.TabIndex = 0;
            this.btnExit.TextValue = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.txtTemplateContent);
            this.groupBox4.Location = new System.Drawing.Point(332, 100);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(580, 420);
            this.groupBox4.TabIndex = 3;
            // 
            // txtTemplateContent
            // 
            this.txtTemplateContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTemplateContent.HideSelection = true;
            this.txtTemplateContent.Location = new System.Drawing.Point(2, 2);
            this.txtTemplateContent.Margin = new System.Windows.Forms.Padding(4);
            this.txtTemplateContent.MaxLength = 0;
            this.txtTemplateContent.Multiline = true;
            this.txtTemplateContent.Name = "txtTemplateContent";
            this.txtTemplateContent.PasswordChar = '\0';
            this.txtTemplateContent.ReadOnly = false;
            this.txtTemplateContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTemplateContent.SelectedText = "";
            this.txtTemplateContent.SelectionLength = 0;
            this.txtTemplateContent.SelectionStart = 0;
            this.txtTemplateContent.Size = new System.Drawing.Size(576, 416);
            this.txtTemplateContent.TabIndex = 0;
            this.txtTemplateContent.TextChanged += new System.EventHandler(this.txtTemplateContent_TextChanged);
            // 
            // TextTemplateEditFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 610);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextTemplateEditFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "保存模板";
            this.Load += new System.EventHandler(this.TextTemplateInputFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox4)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl groupBox1;
        private DevExpress.XtraEditors.PanelControl groupBox2;
        private DevExpress.XtraEditors.PanelControl groupBox3;
        private HISPlus.UserControls.UcButton btnSave;
        private HISPlus.UserControls.UcButton btnExit;
        private System.Windows.Forms.TreeView trvTemplate;
        private HISPlus.UserControls.UcTextArea txtTemplateTitle;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.PanelControl groupBox4;
        private HISPlus.UserControls.UcTextArea txtTemplateContent;
        private HISPlus.UserControls.UcButton btnSaveOrder;
        private HISPlus.UserControls.UcButton btnDelete;
    }
}