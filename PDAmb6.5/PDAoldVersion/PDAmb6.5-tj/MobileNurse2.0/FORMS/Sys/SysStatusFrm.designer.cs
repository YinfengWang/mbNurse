namespace HISPlus
{
    partial class SysStatusFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>SysStatusFrm
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.mnuCancel = new System.Windows.Forms.MenuItem();
            this.mnuRefresh = new System.Windows.Forms.MenuItem();
            this.mnuLog_Down = new System.Windows.Forms.MenuItem();
            this.mnuLog_Up = new System.Windows.Forms.MenuItem();
            this.mnuLog_Normal = new System.Windows.Forms.MenuItem();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuCancel);
            this.mainMenu1.MenuItems.Add(this.mnuRefresh);
            // 
            // mnuCancel
            // 
            this.mnuCancel.Text = "返回";
            this.mnuCancel.Click += new System.EventHandler(this.mnuCancel_Click);
            // 
            // mnuRefresh
            // 
            this.mnuRefresh.MenuItems.Add(this.mnuLog_Down);
            this.mnuRefresh.MenuItems.Add(this.mnuLog_Up);
            this.mnuRefresh.MenuItems.Add(this.mnuLog_Normal);
            this.mnuRefresh.Text = "日志";
            this.mnuRefresh.Click += new System.EventHandler(this.mnuRefresh_Click);
            // 
            // mnuLog_Down
            // 
            this.mnuLog_Down.Text = "下载";
            this.mnuLog_Down.Click += new System.EventHandler(this.mnuLog_Down_Click);
            // 
            // mnuLog_Up
            // 
            this.mnuLog_Up.Text = "上传";
            this.mnuLog_Up.Click += new System.EventHandler(this.mnuLog_Up_Click);
            // 
            // mnuLog_Normal
            // 
            this.mnuLog_Normal.Text = "正常";
            this.mnuLog_Normal.Click += new System.EventHandler(this.mnuLog_Normal_Click);
            // 
            // txtResult
            // 
            this.txtResult.BackColor = System.Drawing.SystemColors.Control;
            this.txtResult.Location = new System.Drawing.Point(3, 3);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(237, 262);
            this.txtResult.TabIndex = 0;
            // 
            // SysStatusFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.txtResult);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "SysStatusFrm";
            this.Text = "系统状态";
            this.Load += new System.EventHandler(this.SysStatusFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuCancel;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.MenuItem mnuRefresh;
        private System.Windows.Forms.MenuItem mnuLog_Down;
        private System.Windows.Forms.MenuItem mnuLog_Up;
        private System.Windows.Forms.MenuItem mnuLog_Normal;
    }
}