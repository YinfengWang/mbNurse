namespace HISPlus
{
    partial class TextTemplateInputFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextTemplateInputFrm));
            this.groupBox1 = new DevExpress.XtraEditors.PanelControl();
            this.trvTemplate = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new DevExpress.XtraEditors.PanelControl();
            this.txtTemplateContent = new HISPlus.UserControls.UcTextArea();
            this.groupBox3 = new DevExpress.XtraEditors.PanelControl();
            this.btnEditTemplate = new HISPlus.UserControls.UcButton();
            this.btnOk = new HISPlus.UserControls.UcButton();
            this.btnCancel = new HISPlus.UserControls.UcButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox4 = new DevExpress.XtraEditors.PanelControl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtEdit = new HISPlus.UserControls.UcTextArea();
            this.btnInsert = new HISPlus.UserControls.UcButton();
            this.lblTextCount = new DevExpress.XtraEditors.LabelControl();
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
            this.groupBox1.Location = new System.Drawing.Point(14, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 474);
            this.groupBox1.TabIndex = 0;
            // 
            // trvTemplate
            // 
            this.trvTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvTemplate.HideSelection = false;
            this.trvTemplate.ImageIndex = 0;
            this.trvTemplate.ImageList = this.imageList1;
            this.trvTemplate.Location = new System.Drawing.Point(2, 2);
            this.trvTemplate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trvTemplate.Name = "trvTemplate";
            this.trvTemplate.SelectedImageIndex = 0;
            this.trvTemplate.Size = new System.Drawing.Size(266, 470);
            this.trvTemplate.TabIndex = 0;
            this.trvTemplate.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvTemplate_AfterSelect);
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
            this.groupBox2.Controls.Add(this.txtTemplateContent);
            this.groupBox2.Location = new System.Drawing.Point(290, 14);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(401, 176);
            this.groupBox2.TabIndex = 1;
            // 
            // txtTemplateContent
            // 
            this.txtTemplateContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTemplateContent.HideSelection = false;
            this.txtTemplateContent.Location = new System.Drawing.Point(2, 2);
            this.txtTemplateContent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTemplateContent.MaxCharLength = 0;
            this.txtTemplateContent.MaxLength = 0;
            this.txtTemplateContent.Multiline = true;
            this.txtTemplateContent.Name = "txtTemplateContent";
            this.txtTemplateContent.PasswordChar = '\0';
            this.txtTemplateContent.ReadOnly = false;
            this.txtTemplateContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTemplateContent.SelectedText = "";
            this.txtTemplateContent.SelectionLength = 0;
            this.txtTemplateContent.SelectionStart = 0;
            this.txtTemplateContent.Size = new System.Drawing.Size(397, 172);
            this.txtTemplateContent.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnEditTemplate);
            this.groupBox3.Controls.Add(this.btnOk);
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Location = new System.Drawing.Point(14, 496);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(677, 59);
            this.groupBox3.TabIndex = 2;
            // 
            // btnEditTemplate
            // 
            this.btnEditTemplate.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnEditTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditTemplate.Image = ((System.Drawing.Image)(resources.GetObject("btnEditTemplate.Image")));
            this.btnEditTemplate.ImageRight = false;
            this.btnEditTemplate.ImageStyle = HISPlus.UserControls.ImageStyle.Edit;
            this.btnEditTemplate.Location = new System.Drawing.Point(18, 17);
            this.btnEditTemplate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnEditTemplate.MaximumSize = new System.Drawing.Size(79, 23);
            this.btnEditTemplate.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnEditTemplate.Name = "btnEditTemplate";
            this.btnEditTemplate.Size = new System.Drawing.Size(79, 23);
            this.btnEditTemplate.TabIndex = 3;
            this.btnEditTemplate.TextValue = "编辑模板";
            this.btnEditTemplate.UseVisualStyleBackColor = true;
            this.btnEditTemplate.Click += new System.EventHandler(this.btnEditTemplate_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.ImageRight = false;
            this.btnOk.ImageStyle = HISPlus.UserControls.ImageStyle.Apply;
            this.btnOk.Location = new System.Drawing.Point(466, 17);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOk.MaximumSize = new System.Drawing.Size(79, 23);
            this.btnOk.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(79, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.TextValue = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageRight = false;
            this.btnCancel.ImageStyle = HISPlus.UserControls.ImageStyle.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(565, 17);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.MaximumSize = new System.Drawing.Size(79, 23);
            this.btnCancel.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(79, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.TextValue = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.groupBox4.Controls.Add(this.textBox1);
            this.groupBox4.Controls.Add(this.txtEdit);
            this.groupBox4.Location = new System.Drawing.Point(290, 226);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(401, 260);
            this.groupBox4.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(4, 124);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(375, 132);
            this.textBox1.TabIndex = 1;
            this.textBox1.Visible = false;
            // 
            // txtEdit
            // 
            this.txtEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEdit.HideSelection = false;
            this.txtEdit.Location = new System.Drawing.Point(2, 2);
            this.txtEdit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtEdit.MaxCharLength = 0;
            this.txtEdit.MaxLength = 0;
            this.txtEdit.Multiline = true;
            this.txtEdit.Name = "txtEdit";
            this.txtEdit.PasswordChar = '\0';
            this.txtEdit.ReadOnly = false;
            this.txtEdit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEdit.SelectedText = "";
            this.txtEdit.SelectionLength = 0;
            this.txtEdit.SelectionStart = 0;
            this.txtEdit.Size = new System.Drawing.Size(397, 256);
            this.txtEdit.TabIndex = 0;
            // 
            // btnInsert
            // 
            this.btnInsert.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnInsert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsert.Image = ((System.Drawing.Image)(resources.GetObject("btnInsert.Image")));
            this.btnInsert.ImageRight = false;
            this.btnInsert.ImageStyle = HISPlus.UserControls.ImageStyle.Insert;
            this.btnInsert.Location = new System.Drawing.Point(362, 198);
            this.btnInsert.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnInsert.MaximumSize = new System.Drawing.Size(79, 23);
            this.btnInsert.MinimumSize = new System.Drawing.Size(70, 23);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(79, 23);
            this.btnInsert.TabIndex = 4;
            this.btnInsert.TextValue = "插入";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // lblTextCount
            // 
            this.lblTextCount.Location = new System.Drawing.Point(549, 206);
            this.lblTextCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblTextCount.Name = "lblTextCount";
            this.lblTextCount.Size = new System.Drawing.Size(70, 14);
            this.lblTextCount.TabIndex = 5;
            this.lblTextCount.Text = "labelControl1";
            this.lblTextCount.Visible = false;
            // 
            // TextTemplateInputFrm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(705, 569);
            this.Controls.Add(this.lblTextCount);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextTemplateInputFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文本模板输入";
            this.Load += new System.EventHandler(this.TextTemplateInputFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox4)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl groupBox1;
        private DevExpress.XtraEditors.PanelControl groupBox2;
        private DevExpress.XtraEditors.PanelControl groupBox3;
        private HISPlus.UserControls.UcButton btnOk;
        private HISPlus.UserControls.UcButton btnCancel;
        private HISPlus.UserControls.UcButton btnEditTemplate;
        private System.Windows.Forms.TreeView trvTemplate;
        private HISPlus.UserControls.UcTextArea txtTemplateContent;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.PanelControl groupBox4;
        private HISPlus.UserControls.UcTextArea txtEdit;
        private HISPlus.UserControls.UcButton btnInsert;
        private DevExpress.XtraEditors.LabelControl lblTextCount;
        private System.Windows.Forms.TextBox textBox1;
    }
}