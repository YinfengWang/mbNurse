namespace PACSMonitor
{
    partial class frmConfig
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfig));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtHtmlPath = new System.Windows.Forms.TextBox();
            this.btnHtmlPath = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.btnForeverOK = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnLogPath = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtLogPath = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblLogRefresh = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.radioHand = new System.Windows.Forms.RadioButton();
            this.radioTimer = new System.Windows.Forms.RadioButton();
            this.radioAuto = new System.Windows.Forms.RadioButton();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(18, 278);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(323, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "* 保存则只应用于当前进程,当服务重启后,会还原到原配置.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(18, 255);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "* 永久保存指的是写入配置文件.";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.txtHtmlPath);
            this.groupBox3.Controls.Add(this.btnHtmlPath);
            this.groupBox3.Location = new System.Drawing.Point(9, 10);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(494, 66);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "生成路径";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(-2, 80);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(494, 66);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "生成路径";
            // 
            // txtHtmlPath
            // 
            this.txtHtmlPath.Location = new System.Drawing.Point(11, 28);
            this.txtHtmlPath.Name = "txtHtmlPath";
            this.txtHtmlPath.Size = new System.Drawing.Size(386, 21);
            this.txtHtmlPath.TabIndex = 4;
            this.txtHtmlPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMonitorPath_KeyPress);
            // 
            // btnHtmlPath
            // 
            this.btnHtmlPath.Location = new System.Drawing.Point(410, 25);
            this.btnHtmlPath.Name = "btnHtmlPath";
            this.btnHtmlPath.Size = new System.Drawing.Size(75, 23);
            this.btnHtmlPath.TabIndex = 5;
            this.btnHtmlPath.Text = "浏览";
            this.btnHtmlPath.UseVisualStyleBackColor = true;
            this.btnHtmlPath.Click += new System.EventHandler(this.browseHtmlPath_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(418, 378);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 7;
            this.cmdCancel.Text = "取消(&C)";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // btnForeverOK
            // 
            this.btnForeverOK.Location = new System.Drawing.Point(242, 378);
            this.btnForeverOK.Name = "btnForeverOK";
            this.btnForeverOK.Size = new System.Drawing.Size(75, 23);
            this.btnForeverOK.TabIndex = 7;
            this.btnForeverOK.Text = "永久保存(&S)";
            this.btnForeverOK.UseVisualStyleBackColor = true;
            this.btnForeverOK.Click += new System.EventHandler(this.btnForeverOK_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(330, 378);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(75, 23);
            this.cmdOk.TabIndex = 7;
            this.cmdOk.Text = "保存(&O)";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnLogPath
            // 
            this.btnLogPath.Location = new System.Drawing.Point(410, 25);
            this.btnLogPath.Name = "btnLogPath";
            this.btnLogPath.Size = new System.Drawing.Size(75, 23);
            this.btnLogPath.TabIndex = 5;
            this.btnLogPath.Text = "浏览";
            this.btnLogPath.UseVisualStyleBackColor = true;
            this.btnLogPath.Click += new System.EventHandler(this.btnLogPath_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(-2, 80);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox5.Size = new System.Drawing.Size(494, 66);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "生成路径";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.txtLogPath);
            this.groupBox4.Controls.Add(this.btnLogPath);
            this.groupBox4.Location = new System.Drawing.Point(9, 86);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox4.Size = new System.Drawing.Size(494, 66);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "日志路径";
            // 
            // txtLogPath
            // 
            this.txtLogPath.Location = new System.Drawing.Point(11, 25);
            this.txtLogPath.Name = "txtLogPath";
            this.txtLogPath.Size = new System.Drawing.Size(386, 21);
            this.txtLogPath.TabIndex = 4;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblLogRefresh);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.numericUpDown1);
            this.groupBox6.Controls.Add(this.radioHand);
            this.groupBox6.Controls.Add(this.radioTimer);
            this.groupBox6.Controls.Add(this.radioAuto);
            this.groupBox6.Location = new System.Drawing.Point(9, 169);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox6.Size = new System.Drawing.Size(494, 84);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "日志刷新设置";
            // 
            // lblLogRefresh
            // 
            this.lblLogRefresh.AutoSize = true;
            this.lblLogRefresh.Location = new System.Drawing.Point(9, 58);
            this.lblLogRefresh.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLogRefresh.Name = "lblLogRefresh";
            this.lblLogRefresh.Size = new System.Drawing.Size(281, 12);
            this.lblLogRefresh.TabIndex = 16;
            this.lblLogRefresh.Text = "自动刷新：当监控目录发生变更时，自动刷新日志。";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(308, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "定时(s)：";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(371, 25);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(46, 21);
            this.numericUpDown1.TabIndex = 14;
            this.numericUpDown1.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // radioHand
            // 
            this.radioHand.AutoSize = true;
            this.radioHand.Location = new System.Drawing.Point(100, 29);
            this.radioHand.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioHand.Name = "radioHand";
            this.radioHand.Size = new System.Drawing.Size(71, 16);
            this.radioHand.TabIndex = 13;
            this.radioHand.TabStop = true;
            this.radioHand.Text = "手动刷新";
            this.radioHand.UseVisualStyleBackColor = true;
            this.radioHand.CheckedChanged += new System.EventHandler(this.radioHand_CheckedChanged);
            // 
            // radioTimer
            // 
            this.radioTimer.AutoSize = true;
            this.radioTimer.Location = new System.Drawing.Point(195, 29);
            this.radioTimer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioTimer.Name = "radioTimer";
            this.radioTimer.Size = new System.Drawing.Size(71, 16);
            this.radioTimer.TabIndex = 13;
            this.radioTimer.TabStop = true;
            this.radioTimer.Text = "定时刷新";
            this.radioTimer.UseVisualStyleBackColor = true;
            this.radioTimer.CheckedChanged += new System.EventHandler(this.radioTimer_CheckedChanged);
            // 
            // radioAuto
            // 
            this.radioAuto.AutoSize = true;
            this.radioAuto.Location = new System.Drawing.Point(11, 29);
            this.radioAuto.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioAuto.Name = "radioAuto";
            this.radioAuto.Size = new System.Drawing.Size(71, 16);
            this.radioAuto.TabIndex = 13;
            this.radioAuto.TabStop = true;
            this.radioAuto.Text = "自动刷新";
            this.radioAuto.UseVisualStyleBackColor = true;
            this.radioAuto.CheckedChanged += new System.EventHandler(this.radioAuto_CheckedChanged);
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 418);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.btnForeverOK);
            this.Controls.Add(this.cmdOk);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfig";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "移动医生站PACS辅助程序-配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConfig_FormClosing);
            this.Load += new System.EventHandler(this.dlgConfig_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtHtmlPath;
        private System.Windows.Forms.Button btnHtmlPath;
        private System.Windows.Forms.Button btnForeverOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtLogPath;
        private System.Windows.Forms.Button btnLogPath;
        private System.Windows.Forms.RadioButton radioAuto;
        private System.Windows.Forms.RadioButton radioTimer;
        private System.Windows.Forms.RadioButton radioHand;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label lblLogRefresh;
    }
}