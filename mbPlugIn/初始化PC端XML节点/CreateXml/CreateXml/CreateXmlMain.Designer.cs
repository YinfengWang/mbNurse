namespace CreateXml
{
    partial class CreateXmlMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateXmlMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolClear = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.Toolabout = new System.Windows.Forms.ToolStripMenuItem();
            this.lstShowData = new System.Windows.Forms.ListBox();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripCreate = new System.Windows.Forms.ToolStripButton();
            this.toolStripClearn = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolFile,
            this.ToolHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(634, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolFile
            // 
            this.toolFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOpen,
            this.toolCreate,
            this.toolClear});
            this.toolFile.Name = "toolFile";
            this.toolFile.Size = new System.Drawing.Size(58, 21);
            this.toolFile.Text = "文件(&F)";
            // 
            // toolOpen
            // 
            this.toolOpen.Image = global::CreateXml.Properties.Resources.文件夹;
            this.toolOpen.Name = "toolOpen";
            this.toolOpen.Size = new System.Drawing.Size(118, 22);
            this.toolOpen.Text = "打开(&O)";
            this.toolOpen.Click += new System.EventHandler(this.toolOpen_Click);
            // 
            // toolCreate
            // 
            this.toolCreate.Image = global::CreateXml.Properties.Resources.一键生成;
            this.toolCreate.Name = "toolCreate";
            this.toolCreate.Size = new System.Drawing.Size(118, 22);
            this.toolCreate.Text = "生成(&R)";
            this.toolCreate.Click += new System.EventHandler(this.toolCreate_Click);
            // 
            // toolClear
            // 
            this.toolClear.Image = global::CreateXml.Properties.Resources.清理;
            this.toolClear.Name = "toolClear";
            this.toolClear.Size = new System.Drawing.Size(118, 22);
            this.toolClear.Text = "清理(&C)";
            this.toolClear.Click += new System.EventHandler(this.toolClear_Click);
            // 
            // ToolHelp
            // 
            this.ToolHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Toolabout});
            this.ToolHelp.Name = "ToolHelp";
            this.ToolHelp.Size = new System.Drawing.Size(61, 21);
            this.ToolHelp.Text = "帮助(&H)";
            // 
            // Toolabout
            // 
            this.Toolabout.Name = "Toolabout";
            this.Toolabout.Size = new System.Drawing.Size(152, 22);
            this.Toolabout.Text = "关于(&A)";
            this.Toolabout.Click += new System.EventHandler(this.Toolabout_Click);
            // 
            // lstShowData
            // 
            this.lstShowData.AllowDrop = true;
            this.lstShowData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstShowData.FormattingEnabled = true;
            this.lstShowData.ItemHeight = 14;
            this.lstShowData.Location = new System.Drawing.Point(0, 56);
            this.lstShowData.Name = "lstShowData";
            this.lstShowData.Size = new System.Drawing.Size(634, 214);
            this.lstShowData.TabIndex = 1;
            this.lstShowData.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstShowData_DragDrop);
            this.lstShowData.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstShowData_DragEnter);
            this.lstShowData.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lstShowData_MouseMove);
            // 
            // txtContent
            // 
            this.txtContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContent.Location = new System.Drawing.Point(0, 272);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(634, 244);
            this.txtContent.TabIndex = 2;
            this.txtContent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContent_KeyDown);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripOpen,
            this.toolStripCreate,
            this.toolStripClearn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(634, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripOpen
            // 
            this.toolStripOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripOpen.Image")));
            this.toolStripOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripOpen.Name = "toolStripOpen";
            this.toolStripOpen.Size = new System.Drawing.Size(23, 22);
            this.toolStripOpen.Text = "打开";
            this.toolStripOpen.Click += new System.EventHandler(this.toolOpen_Click);
            // 
            // toolStripCreate
            // 
            this.toolStripCreate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripCreate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripCreate.Image")));
            this.toolStripCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripCreate.Name = "toolStripCreate";
            this.toolStripCreate.Size = new System.Drawing.Size(23, 22);
            this.toolStripCreate.Text = "生成";
            this.toolStripCreate.Click += new System.EventHandler(this.toolCreate_Click);
            // 
            // toolStripClearn
            // 
            this.toolStripClearn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripClearn.Image = ((System.Drawing.Image)(resources.GetObject("toolStripClearn.Image")));
            this.toolStripClearn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripClearn.Name = "toolStripClearn";
            this.toolStripClearn.Size = new System.Drawing.Size(23, 22);
            this.toolStripClearn.Text = "清空";
            this.toolStripClearn.Click += new System.EventHandler(this.toolClear_Click);
            // 
            // CreateXmlMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 519);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.lstShowData);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CreateXmlMain";
            this.Text = "CreateXml";
            this.Load += new System.EventHandler(this.CreateXmlMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolFile;
        private System.Windows.Forms.ToolStripMenuItem toolOpen;
        private System.Windows.Forms.ToolStripMenuItem toolCreate;
        private System.Windows.Forms.ListBox lstShowData;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.ToolStripMenuItem toolClear;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripOpen;
        private System.Windows.Forms.ToolStripButton toolStripCreate;
        private System.Windows.Forms.ToolStripButton toolStripClearn;
        private System.Windows.Forms.ToolStripMenuItem ToolHelp;
        private System.Windows.Forms.ToolStripMenuItem Toolabout;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}

