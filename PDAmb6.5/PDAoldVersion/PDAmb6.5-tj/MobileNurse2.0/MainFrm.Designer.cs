namespace HISPlus
{
    partial class MainFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.mnuLock = new System.Windows.Forms.MenuItem();
            this.mnuLogin = new System.Windows.Forms.MenuItem();
            this.mnuExit = new System.Windows.Forms.MenuItem();
            this.mnuSep1 = new System.Windows.Forms.MenuItem();
            this.mnuChangePwd = new System.Windows.Forms.MenuItem();
            this.mnuSep2 = new System.Windows.Forms.MenuItem();
            this.mnuSetting = new System.Windows.Forms.MenuItem();
            this.mnuSysStatus = new System.Windows.Forms.MenuItem();
            this.mnuPatient = new System.Windows.Forms.MenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.picBackground = new System.Windows.Forms.PictureBox();
            this.btnPrePatient = new System.Windows.Forms.Button();
            this.btnCurrPatient = new System.Windows.Forms.Button();
            this.btnNextPatient = new System.Windows.Forms.Button();
            this.btnListPatient = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.timer2 = new System.Windows.Forms.Timer();
            this.timer3 = new System.Windows.Forms.Timer();
            this.TLastMinute = new System.Windows.Forms.Timer();
            this.picPgd = new System.Windows.Forms.PictureBox();
            this.contextPgd = new System.Windows.Forms.ContextMenu();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuLock);
            this.mainMenu1.MenuItems.Add(this.mnuPatient);
            // 
            // mnuLock
            // 
            this.mnuLock.MenuItems.Add(this.mnuLogin);
            this.mnuLock.MenuItems.Add(this.mnuExit);
            this.mnuLock.MenuItems.Add(this.mnuSep1);
            this.mnuLock.MenuItems.Add(this.mnuChangePwd);
            this.mnuLock.MenuItems.Add(this.mnuSep2);
            this.mnuLock.MenuItems.Add(this.mnuSetting);
            this.mnuLock.MenuItems.Add(this.mnuSysStatus);
            this.mnuLock.Text = "系统";
            // 
            // mnuLogin
            // 
            this.mnuLogin.Text = "登录";
            this.mnuLogin.Click += new System.EventHandler(this.mnuLogin_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Text = "注销";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuSep1
            // 
            this.mnuSep1.Text = "-";
            // 
            // mnuChangePwd
            // 
            this.mnuChangePwd.Text = "修改密码";
            this.mnuChangePwd.Click += new System.EventHandler(this.mnuChangePwd_Click);
            // 
            // mnuSep2
            // 
            this.mnuSep2.Text = "-";
            // 
            // mnuSetting
            // 
            this.mnuSetting.Text = "系统设置";
            this.mnuSetting.Click += new System.EventHandler(this.mnuSetting_Click);
            // 
            // mnuSysStatus
            // 
            this.mnuSysStatus.Text = "系统状态";
            this.mnuSysStatus.Click += new System.EventHandler(this.mnuSysStatus_Click);
            // 
            // mnuPatient
            // 
            this.mnuPatient.Enabled = false;
            this.mnuPatient.Text = "当前病人";
            this.imageList1.Images.Clear();
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            // 
            // picBackground
            // 
            this.picBackground.Image = ((System.Drawing.Image)(resources.GetObject("picBackground.Image")));
            this.picBackground.Location = new System.Drawing.Point(0, 0);
            this.picBackground.Name = "picBackground";
            this.picBackground.Size = new System.Drawing.Size(240, 248);
            this.picBackground.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBackground_MouseUp);
            // 
            // btnPrePatient
            // 
            this.btnPrePatient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnPrePatient.Location = new System.Drawing.Point(45, 248);
            this.btnPrePatient.Name = "btnPrePatient";
            this.btnPrePatient.Size = new System.Drawing.Size(66, 20);
            this.btnPrePatient.TabIndex = 2;
            // 
            // btnCurrPatient
            // 
            this.btnCurrPatient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCurrPatient.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCurrPatient.ForeColor = System.Drawing.Color.Blue;
            this.btnCurrPatient.Location = new System.Drawing.Point(110, 248);
            this.btnCurrPatient.Name = "btnCurrPatient";
            this.btnCurrPatient.Size = new System.Drawing.Size(67, 20);
            this.btnCurrPatient.TabIndex = 3;
            // 
            // btnNextPatient
            // 
            this.btnNextPatient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnNextPatient.Location = new System.Drawing.Point(176, 248);
            this.btnNextPatient.Name = "btnNextPatient";
            this.btnNextPatient.Size = new System.Drawing.Size(64, 20);
            this.btnNextPatient.TabIndex = 4;
            // 
            // btnListPatient
            // 
            this.btnListPatient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnListPatient.Location = new System.Drawing.Point(1, 248);
            this.btnListPatient.Name = "btnListPatient";
            this.btnListPatient.Size = new System.Drawing.Size(45, 20);
            this.btnListPatient.TabIndex = 21;
            this.btnListPatient.Text = "列表";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 200;
            // 
            // timer3
            // 
            this.timer3.Enabled = true;
            this.timer3.Interval = 5000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // TLastMinute
            // 
            this.TLastMinute.Enabled = true;
            this.TLastMinute.Interval = 1000;
            this.TLastMinute.Tick += new System.EventHandler(this.TLastMinute_Tick);
            // 
            // picPgd
            // 
            this.picPgd.BackColor = System.Drawing.Color.Transparent;
            this.picPgd.ContextMenu = this.contextPgd;
            this.picPgd.Image = ((System.Drawing.Image)(resources.GetObject("picPgd.Image")));
            this.picPgd.Location = new System.Drawing.Point(184, 174);
            this.picPgd.Name = "picPgd";
            this.picPgd.Size = new System.Drawing.Size(46, 50);
            this.picPgd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.picPgd);
            this.Controls.Add(this.btnListPatient);
            this.Controls.Add(this.btnNextPatient);
            this.Controls.Add(this.btnCurrPatient);
            this.Controls.Add(this.btnPrePatient);
            this.Controls.Add(this.picBackground);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "MainFrm";
            this.Text = "移动护理";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.Closed += new System.EventHandler(this.MainFrm_Closed);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainFrm_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuLock;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox picBackground;
        private System.Windows.Forms.Button btnPrePatient;
        private System.Windows.Forms.Button btnCurrPatient;
        private System.Windows.Forms.Button btnNextPatient;
        private System.Windows.Forms.MenuItem mnuLogin;
        private System.Windows.Forms.Button btnListPatient;
        private System.Windows.Forms.MenuItem mnuChangePwd;
        private System.Windows.Forms.MenuItem mnuSep2;
        private System.Windows.Forms.MenuItem mnuExit;
        private System.Windows.Forms.MenuItem mnuSep1;
        private System.Windows.Forms.MenuItem mnuSysStatus;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuItem mnuPatient;
        private System.Windows.Forms.MenuItem mnuSetting;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Timer TLastMinute;
        private System.Windows.Forms.PictureBox picPgd;
        private System.Windows.Forms.ContextMenu contextPgd;

    }
}

