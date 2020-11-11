namespace BodyTemp
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lstSyncRecodeList = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolOper = new System.Windows.Forms.ToolStripMenuItem();
            this.toolClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolAutoUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.myIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.myMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolShowFrm = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolHide = new System.Windows.Forms.ToolStripMenuItem();
            this.panelRecode = new DevExpress.XtraEditors.PanelControl();
            this.xtraTabControl2 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.lstSpiltRecodeList = new System.Windows.Forms.ListBox();
            this.panelSuccess = new DevExpress.XtraEditors.PanelControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageSyncSuccess = new DevExpress.XtraTab.XtraTabPage();
            this.lstSyncSuccess = new System.Windows.Forms.ListBox();
            this.xtraTabPageSyncFail = new DevExpress.XtraTab.XtraTabPage();
            this.lstSyncFail = new System.Windows.Forms.ListBox();
            this.xtraTabPageSplitSuccess = new DevExpress.XtraTab.XtraTabPage();
            this.lstSplitSuccess = new System.Windows.Forms.ListBox();
            this.xtraTabPageSplitFalit = new DevExpress.XtraTab.XtraTabPage();
            this.lstSplitFail = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.myMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelRecode)).BeginInit();
            this.panelRecode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).BeginInit();
            this.xtraTabControl2.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
            this.xtraTabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelSuccess)).BeginInit();
            this.panelSuccess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPageSyncSuccess.SuspendLayout();
            this.xtraTabPageSyncFail.SuspendLayout();
            this.xtraTabPageSplitSuccess.SuspendLayout();
            this.xtraTabPageSplitFalit.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstSyncRecodeList
            // 
            this.lstSyncRecodeList.BackColor = System.Drawing.Color.Gainsboro;
            this.lstSyncRecodeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSyncRecodeList.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lstSyncRecodeList.FormattingEnabled = true;
            this.lstSyncRecodeList.ItemHeight = 14;
            this.lstSyncRecodeList.Location = new System.Drawing.Point(0, 0);
            this.lstSyncRecodeList.Name = "lstSyncRecodeList";
            this.lstSyncRecodeList.Size = new System.Drawing.Size(962, 282);
            this.lstSyncRecodeList.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOper,
            this.toolSetting});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(977, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolOper
            // 
            this.toolOper.Checked = true;
            this.toolOper.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolOper.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolClear,
            this.toolExit});
            this.toolOper.Name = "toolOper";
            this.toolOper.Size = new System.Drawing.Size(62, 21);
            this.toolOper.Text = "操作(&O)";
            // 
            // toolClear
            // 
            this.toolClear.Image = global::BodyTemp.Properties.Resources.edit_clear_2_128px_539679_easyicon_net;
            this.toolClear.Name = "toolClear";
            this.toolClear.Size = new System.Drawing.Size(116, 22);
            this.toolClear.Text = "清空(&C)";
            this.toolClear.Click += new System.EventHandler(this.toolClear_Click);
            // 
            // toolExit
            // 
            this.toolExit.Enabled = false;
            this.toolExit.Image = global::BodyTemp.Properties.Resources.exit_128px_1097422_easyicon_net;
            this.toolExit.Name = "toolExit";
            this.toolExit.Size = new System.Drawing.Size(116, 22);
            this.toolExit.Text = "退出(&E)";
            this.toolExit.Click += new System.EventHandler(this.toolExit_Click);
            // 
            // toolSetting
            // 
            this.toolSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAutoUpdate});
            this.toolSetting.Name = "toolSetting";
            this.toolSetting.Size = new System.Drawing.Size(59, 21);
            this.toolSetting.Text = "设置(&S)";
            // 
            // toolAutoUpdate
            // 
            this.toolAutoUpdate.Image = global::BodyTemp.Properties.Resources.update;
            this.toolAutoUpdate.Name = "toolAutoUpdate";
            this.toolAutoUpdate.Size = new System.Drawing.Size(165, 22);
            this.toolAutoUpdate.Text = "上传更新文件(&U)";
            this.toolAutoUpdate.Click += new System.EventHandler(this.toolAutoUpdate_Click);
            // 
            // myIcon
            // 
            this.myIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.myIcon.BalloonTipTitle = "提示";
            this.myIcon.ContextMenuStrip = this.myMenu;
            this.myIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("myIcon.Icon")));
            this.myIcon.Text = "同步/拆分辅助程序";
            this.myIcon.Visible = true;
            this.myIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.myIcon_MouseClick);
            // 
            // myMenu
            // 
            this.myMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolShowFrm,
            this.ToolHide});
            this.myMenu.Name = "myMenu";
            this.myMenu.Size = new System.Drawing.Size(101, 48);
            // 
            // toolShowFrm
            // 
            this.toolShowFrm.Image = ((System.Drawing.Image)(resources.GetObject("toolShowFrm.Image")));
            this.toolShowFrm.Name = "toolShowFrm";
            this.toolShowFrm.Size = new System.Drawing.Size(100, 22);
            this.toolShowFrm.Text = "显示";
            this.toolShowFrm.Click += new System.EventHandler(this.toolShowFrm_Click);
            // 
            // ToolHide
            // 
            this.ToolHide.Image = ((System.Drawing.Image)(resources.GetObject("ToolHide.Image")));
            this.ToolHide.Name = "ToolHide";
            this.ToolHide.Size = new System.Drawing.Size(100, 22);
            this.ToolHide.Text = "隐藏";
            this.ToolHide.Click += new System.EventHandler(this.ToolHide_Click);
            // 
            // panelRecode
            // 
            this.panelRecode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRecode.Controls.Add(this.xtraTabControl2);
            this.panelRecode.Location = new System.Drawing.Point(2, 29);
            this.panelRecode.Name = "panelRecode";
            this.panelRecode.Size = new System.Drawing.Size(972, 315);
            this.panelRecode.TabIndex = 6;
            // 
            // xtraTabControl2
            // 
            this.xtraTabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl2.Location = new System.Drawing.Point(2, 2);
            this.xtraTabControl2.Name = "xtraTabControl2";
            this.xtraTabControl2.SelectedTabPage = this.xtraTabPage3;
            this.xtraTabControl2.Size = new System.Drawing.Size(968, 311);
            this.xtraTabControl2.TabIndex = 1;
            this.xtraTabControl2.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage3,
            this.xtraTabPage4});
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.lstSyncRecodeList);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(962, 282);
            this.xtraTabPage3.Text = "同步记录";
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.lstSpiltRecodeList);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(962, 282);
            this.xtraTabPage4.Text = "拆分记录";
            // 
            // lstSpiltRecodeList
            // 
            this.lstSpiltRecodeList.BackColor = System.Drawing.Color.Gainsboro;
            this.lstSpiltRecodeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSpiltRecodeList.FormattingEnabled = true;
            this.lstSpiltRecodeList.ItemHeight = 14;
            this.lstSpiltRecodeList.Location = new System.Drawing.Point(0, 0);
            this.lstSpiltRecodeList.Name = "lstSpiltRecodeList";
            this.lstSpiltRecodeList.Size = new System.Drawing.Size(962, 282);
            this.lstSpiltRecodeList.TabIndex = 0;
            // 
            // panelSuccess
            // 
            this.panelSuccess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSuccess.Controls.Add(this.xtraTabControl1);
            this.panelSuccess.Location = new System.Drawing.Point(2, 344);
            this.panelSuccess.Name = "panelSuccess";
            this.panelSuccess.Size = new System.Drawing.Size(972, 159);
            this.panelSuccess.TabIndex = 7;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(2, 2);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPageSyncSuccess;
            this.xtraTabControl1.Size = new System.Drawing.Size(968, 155);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageSyncSuccess,
            this.xtraTabPageSyncFail,
            this.xtraTabPageSplitSuccess,
            this.xtraTabPageSplitFalit});
            // 
            // xtraTabPageSyncSuccess
            // 
            this.xtraTabPageSyncSuccess.Controls.Add(this.lstSyncSuccess);
            this.xtraTabPageSyncSuccess.Name = "xtraTabPageSyncSuccess";
            this.xtraTabPageSyncSuccess.Size = new System.Drawing.Size(962, 126);
            this.xtraTabPageSyncSuccess.Text = "同步成功";
            // 
            // lstSyncSuccess
            // 
            this.lstSyncSuccess.BackColor = System.Drawing.Color.Gainsboro;
            this.lstSyncSuccess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSyncSuccess.ForeColor = System.Drawing.Color.Green;
            this.lstSyncSuccess.FormattingEnabled = true;
            this.lstSyncSuccess.ItemHeight = 14;
            this.lstSyncSuccess.Location = new System.Drawing.Point(0, 0);
            this.lstSyncSuccess.Name = "lstSyncSuccess";
            this.lstSyncSuccess.Size = new System.Drawing.Size(962, 126);
            this.lstSyncSuccess.TabIndex = 0;
            // 
            // xtraTabPageSyncFail
            // 
            this.xtraTabPageSyncFail.Controls.Add(this.lstSyncFail);
            this.xtraTabPageSyncFail.Name = "xtraTabPageSyncFail";
            this.xtraTabPageSyncFail.Size = new System.Drawing.Size(962, 126);
            this.xtraTabPageSyncFail.Text = "同步失败";
            // 
            // lstSyncFail
            // 
            this.lstSyncFail.BackColor = System.Drawing.Color.Gainsboro;
            this.lstSyncFail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSyncFail.ForeColor = System.Drawing.Color.Red;
            this.lstSyncFail.FormattingEnabled = true;
            this.lstSyncFail.ItemHeight = 14;
            this.lstSyncFail.Location = new System.Drawing.Point(0, 0);
            this.lstSyncFail.Name = "lstSyncFail";
            this.lstSyncFail.Size = new System.Drawing.Size(962, 126);
            this.lstSyncFail.TabIndex = 0;
            // 
            // xtraTabPageSplitSuccess
            // 
            this.xtraTabPageSplitSuccess.Controls.Add(this.lstSplitSuccess);
            this.xtraTabPageSplitSuccess.Name = "xtraTabPageSplitSuccess";
            this.xtraTabPageSplitSuccess.Size = new System.Drawing.Size(962, 126);
            this.xtraTabPageSplitSuccess.Text = "拆分成功";
            // 
            // lstSplitSuccess
            // 
            this.lstSplitSuccess.BackColor = System.Drawing.Color.Gainsboro;
            this.lstSplitSuccess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSplitSuccess.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lstSplitSuccess.FormattingEnabled = true;
            this.lstSplitSuccess.ItemHeight = 14;
            this.lstSplitSuccess.Location = new System.Drawing.Point(0, 0);
            this.lstSplitSuccess.Name = "lstSplitSuccess";
            this.lstSplitSuccess.Size = new System.Drawing.Size(962, 126);
            this.lstSplitSuccess.TabIndex = 0;
            // 
            // xtraTabPageSplitFalit
            // 
            this.xtraTabPageSplitFalit.Controls.Add(this.lstSplitFail);
            this.xtraTabPageSplitFalit.Name = "xtraTabPageSplitFalit";
            this.xtraTabPageSplitFalit.Size = new System.Drawing.Size(962, 126);
            this.xtraTabPageSplitFalit.Text = "拆分失败";
            // 
            // lstSplitFail
            // 
            this.lstSplitFail.BackColor = System.Drawing.Color.Gainsboro;
            this.lstSplitFail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSplitFail.ForeColor = System.Drawing.Color.Red;
            this.lstSplitFail.FormattingEnabled = true;
            this.lstSplitFail.ItemHeight = 14;
            this.lstSplitFail.Location = new System.Drawing.Point(0, 0);
            this.lstSplitFail.Name = "lstSplitFail";
            this.lstSplitFail.Size = new System.Drawing.Size(962, 126);
            this.lstSplitFail.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 506);
            this.Controls.Add(this.panelSuccess);
            this.Controls.Add(this.panelRecode);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "同步/拆分辅助程序";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.myMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelRecode)).EndInit();
            this.panelRecode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).EndInit();
            this.xtraTabControl2.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            this.xtraTabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelSuccess)).EndInit();
            this.panelSuccess.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPageSyncSuccess.ResumeLayout(false);
            this.xtraTabPageSyncFail.ResumeLayout(false);
            this.xtraTabPageSplitSuccess.ResumeLayout(false);
            this.xtraTabPageSplitFalit.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstSyncRecodeList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolOper;
        private System.Windows.Forms.ToolStripMenuItem toolClear;
        private System.Windows.Forms.NotifyIcon myIcon;
        private System.Windows.Forms.ContextMenuStrip myMenu;
        private DevExpress.XtraEditors.PanelControl panelRecode;
        private System.Windows.Forms.ToolStripMenuItem toolExit;
        private DevExpress.XtraEditors.PanelControl panelSuccess;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageSyncSuccess;
        private System.Windows.Forms.ListBox lstSyncSuccess;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageSyncFail;
        private System.Windows.Forms.ListBox lstSyncFail;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private System.Windows.Forms.ListBox lstSpiltRecodeList;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageSplitSuccess;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageSplitFalit;
        private System.Windows.Forms.ListBox lstSplitSuccess;
        private System.Windows.Forms.ListBox lstSplitFail;
        private System.Windows.Forms.ToolStripMenuItem toolSetting;
        private System.Windows.Forms.ToolStripMenuItem toolShowFrm;
        private System.Windows.Forms.ToolStripMenuItem ToolHide;
        private System.Windows.Forms.ToolStripMenuItem toolAutoUpdate;

    }
}

