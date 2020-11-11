namespace HISPlus
{
    partial class SettingFrm
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.mnuCancel = new System.Windows.Forms.MenuItem();
            this.btnSet = new System.Windows.Forms.Button();
            this.txtWardCode = new System.Windows.Forms.TextBox();
            this.txtServerIp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClearLocalDb = new System.Windows.Forms.Button();
            this.btnUpdDbStruct = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuCancel);
            // 
            // mnuCancel
            // 
            this.mnuCancel.Text = "返回";
            this.mnuCancel.Click += new System.EventHandler(this.mnuCancel_Click);
            // 
            // btnSet
            // 
            this.btnSet.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnSet.Enabled = false;
            this.btnSet.Location = new System.Drawing.Point(144, 109);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(69, 27);
            this.btnSet.TabIndex = 17;
            this.btnSet.Text = "保存";
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // txtWardCode
            // 
            this.txtWardCode.Location = new System.Drawing.Point(83, 46);
            this.txtWardCode.Name = "txtWardCode";
            this.txtWardCode.Size = new System.Drawing.Size(130, 21);
            this.txtWardCode.TabIndex = 16;
            this.txtWardCode.TextChanged += new System.EventHandler(this.txtServerIp_TextChanged);
            // 
            // txtServerIp
            // 
            this.txtServerIp.Location = new System.Drawing.Point(83, 12);
            this.txtServerIp.Name = "txtServerIp";
            this.txtServerIp.Size = new System.Drawing.Size(130, 21);
            this.txtServerIp.TabIndex = 15;
            this.txtServerIp.TextChanged += new System.EventHandler(this.txtServerIp_TextChanged);
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(18, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.Text = "病区代码";
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(18, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.Text = "服务器IP";
            // 
            // btnClearLocalDb
            // 
            this.btnClearLocalDb.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnClearLocalDb.Location = new System.Drawing.Point(129, 202);
            this.btnClearLocalDb.Name = "btnClearLocalDb";
            this.btnClearLocalDb.Size = new System.Drawing.Size(108, 27);
            this.btnClearLocalDb.TabIndex = 20;
            this.btnClearLocalDb.Text = "清除本地数据";
            this.btnClearLocalDb.Click += new System.EventHandler(this.btnClearLocalDb_Click);
            // 
            // btnUpdDbStruct
            // 
            this.btnUpdDbStruct.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnUpdDbStruct.Location = new System.Drawing.Point(3, 202);
            this.btnUpdDbStruct.Name = "btnUpdDbStruct";
            this.btnUpdDbStruct.Size = new System.Drawing.Size(104, 27);
            this.btnUpdDbStruct.TabIndex = 23;
            this.btnUpdDbStruct.Text = "更新本地表结构";
            this.btnUpdDbStruct.Click += new System.EventHandler(this.btnUpdDbStruct_Click);
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(18, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 20);
            this.label3.Text = "版 本 号";
            this.label3.Visible = false;
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(83, 116);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(100, 20);
            this.lblVersion.Visible = false;
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(83, 81);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(130, 21);
            this.txtVersion.TabIndex = 27;
            this.txtVersion.TextChanged += new System.EventHandler(this.txtServerIp_TextChanged);
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(18, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 20);
            this.label4.Text = "版  本 号";
            // 
            // SettingFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnUpdDbStruct);
            this.Controls.Add(this.btnClearLocalDb);
            this.Controls.Add(this.txtWardCode);
            this.Controls.Add(this.txtServerIp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "SettingFrm";
            this.Text = "设置";
            this.Load += new System.EventHandler(this.SettingFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.TextBox txtWardCode;
        private System.Windows.Forms.TextBox txtServerIp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuItem mnuCancel;
        private System.Windows.Forms.Button btnClearLocalDb;
        private System.Windows.Forms.Button btnUpdDbStruct;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label label4;
    }
}