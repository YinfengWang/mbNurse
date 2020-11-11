//------------------------------------------------------------------------------------
//
//  系统名称        : 医院信息系统
//  子系统名称      : 通用模块
//  对象类型        : 
//  类名            : frmConfig.cs
//  功能概要        : INI文件配置
//  作成者          : 付军
//  作成日          : 2007-05-23
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        :
//  变更者          :
//  变更内容        :
//  版本            :
//------------------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace HISPlus
{
	/// <summary>
	/// frmConfig 的摘要说明。
	/// </summary>
	public class frmConfig : FormDo
	{
        private System.Windows.Forms.TabControl tabControl1;
        private HISPlus.UserControls.UcButton btnCancel;
        private HISPlus.UserControls.UcButton btnOK;
        private System.Windows.Forms.TabPage tabPage_Env;
        private DevExpress.XtraEditors.GroupControl grpConnect;
        private DevExpress.XtraEditors.LabelControl label1;
        private HISPlus.UserControls.UcTextBox txtServer;
        private DevExpress.XtraEditors.LabelControl label2;
        private HISPlus.UserControls.UcTextBox txtPwd;
        private DevExpress.XtraEditors.LabelControl label3;
        private HISPlus.UserControls.UcTextBox txtUserID;
        private HISPlus.UserControls.UcTextBox txtDatabase;
        private DevExpress.XtraEditors.LabelControl label5;
        private DevExpress.XtraEditors.GroupControl groupBox1;
        private HISPlus.UserControls.UcTextBox txtOra_Pwd;
        private DevExpress.XtraEditors.LabelControl label7;
        private HISPlus.UserControls.UcTextBox txtOra_UserId;
        private DevExpress.XtraEditors.LabelControl label8;
        private HISPlus.UserControls.UcTextBox txtOra_DataSource;
        private DevExpress.XtraEditors.LabelControl label9;
        private DevExpress.XtraEditors.GroupControl groupBox2;
        private CheckBox chkOraNlsZhs;
        private CheckBox chkApp3_3;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmConfig()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
            			
		}


		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfig));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_Env = new System.Windows.Forms.TabPage();
            this.groupBox2 = new DevExpress.XtraEditors.GroupControl();
            this.chkOraNlsZhs = new System.Windows.Forms.CheckBox();
            this.chkApp3_3 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new DevExpress.XtraEditors.GroupControl();
            this.txtOra_Pwd = new HISPlus.UserControls.UcTextBox();
            this.label7 = new DevExpress.XtraEditors.LabelControl();
            this.txtOra_UserId = new HISPlus.UserControls.UcTextBox();
            this.label8 = new DevExpress.XtraEditors.LabelControl();
            this.txtOra_DataSource = new HISPlus.UserControls.UcTextBox();
            this.label9 = new DevExpress.XtraEditors.LabelControl();
            this.grpConnect = new DevExpress.XtraEditors.GroupControl();
            this.txtDatabase = new HISPlus.UserControls.UcTextBox();
            this.label5 = new DevExpress.XtraEditors.LabelControl();
            this.txtPwd = new HISPlus.UserControls.UcTextBox();
            this.label3 = new DevExpress.XtraEditors.LabelControl();
            this.txtUserID = new HISPlus.UserControls.UcTextBox();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.txtServer = new HISPlus.UserControls.UcTextBox();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new HISPlus.UserControls.UcButton();
            this.btnOK = new HISPlus.UserControls.UcButton();
            this.tabControl1.SuspendLayout();
            this.tabPage_Env.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpConnect.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage_Env);
            this.tabControl1.Location = new System.Drawing.Point(3, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(678, 281);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage_Env
            // 
            this.tabPage_Env.Controls.Add(this.groupBox2);
            this.tabPage_Env.Controls.Add(this.groupBox1);
            this.tabPage_Env.Controls.Add(this.grpConnect);
            this.tabPage_Env.Location = new System.Drawing.Point(4, 25);
            this.tabPage_Env.Name = "tabPage_Env";
            this.tabPage_Env.Size = new System.Drawing.Size(670, 252);
            this.tabPage_Env.TabIndex = 0;
            this.tabPage_Env.Text = "环境设置";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkOraNlsZhs);
            this.groupBox2.Controls.Add(this.chkApp3_3);
            this.groupBox2.Location = new System.Drawing.Point(4, 80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(660, 67);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "环境";
            // 
            // chkOraNlsZhs
            // 
            this.chkOraNlsZhs.AutoSize = true;
            this.chkOraNlsZhs.Location = new System.Drawing.Point(328, 26);
            this.chkOraNlsZhs.Name = "chkOraNlsZhs";
            this.chkOraNlsZhs.Size = new System.Drawing.Size(212, 19);
            this.chkOraNlsZhs.TabIndex = 5;
            this.chkOraNlsZhs.Text = "Oracle客户端用中文字符集";
            this.chkOraNlsZhs.UseVisualStyleBackColor = true;
            // 
            // chkApp3_3
            // 
            this.chkApp3_3.AutoSize = true;
            this.chkApp3_3.Location = new System.Drawing.Point(103, 26);
            this.chkApp3_3.Name = "chkApp3_3";
            this.chkApp3_3.Size = new System.Drawing.Size(145, 19);
            this.chkApp3_3.TabIndex = 4;
            this.chkApp3_3.Text = "HIS 3.3版或以上";
            this.chkApp3_3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtOra_Pwd);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtOra_UserId);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtOra_DataSource);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(660, 68);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库连接(ORACLE)";
            // 
            // txtOra_Pwd
            // 
            this.txtOra_Pwd.Location = new System.Drawing.Point(519, 21);
            this.txtOra_Pwd.MaxLength = 20;
            this.txtOra_Pwd.Name = "txtOra_Pwd";
            this.txtOra_Pwd.PasswordChar = '*';
            this.txtOra_Pwd.Size = new System.Drawing.Size(129, 25);
            this.txtOra_Pwd.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(472, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 21);
            this.label7.TabIndex = 4;
            this.label7.Text = "密码";
            // 
            // txtOra_UserId
            // 
            this.txtOra_UserId.Location = new System.Drawing.Point(328, 21);
            this.txtOra_UserId.MaxLength = 20;
            this.txtOra_UserId.Name = "txtOra_UserId";
            this.txtOra_UserId.Size = new System.Drawing.Size(104, 25);
            this.txtOra_UserId.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(249, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 21);
            this.label8.TabIndex = 2;
            this.label8.Text = "用户名";
            // 
            // txtOra_DataSource
            // 
            this.txtOra_DataSource.Location = new System.Drawing.Point(103, 23);
            this.txtOra_DataSource.MaxLength = 20;
            this.txtOra_DataSource.Name = "txtOra_DataSource";
            this.txtOra_DataSource.Size = new System.Drawing.Size(130, 25);
            this.txtOra_DataSource.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(1, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 22);
            this.label9.TabIndex = 0;
            this.label9.Text = "Data Source";
            this.label9.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            // 
            // grpConnect
            // 
            this.grpConnect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpConnect.Controls.Add(this.txtDatabase);
            this.grpConnect.Controls.Add(this.label5);
            this.grpConnect.Controls.Add(this.txtPwd);
            this.grpConnect.Controls.Add(this.label3);
            this.grpConnect.Controls.Add(this.txtUserID);
            this.grpConnect.Controls.Add(this.label2);
            this.grpConnect.Controls.Add(this.txtServer);
            this.grpConnect.Controls.Add(this.label1);
            this.grpConnect.Location = new System.Drawing.Point(4, 154);
            this.grpConnect.Name = "grpConnect";
            this.grpConnect.Size = new System.Drawing.Size(660, 102);
            this.grpConnect.TabIndex = 0;
            this.grpConnect.TabStop = false;
            this.grpConnect.Text = "数据库连接(SQLSERVER)";
            this.grpConnect.Visible = false;
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(329, 23);
            this.txtDatabase.MaxLength = 20;
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(104, 25);
            this.txtDatabase.TabIndex = 1;
            this.txtDatabase.Visible = false;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(249, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 21);
            this.label5.TabIndex = 6;
            this.label5.Text = "数据库";
            this.label5.Visible = false;
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(329, 62);
            this.txtPwd.MaxLength = 20;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(104, 25);
            this.txtPwd.TabIndex = 3;
            this.txtPwd.Visible = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(249, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "密码";
            this.label3.Visible = false;
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(103, 62);
            this.txtUserID.MaxLength = 20;
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(130, 25);
            this.txtUserID.TabIndex = 2;
            this.txtUserID.Visible = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(29, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "用户名";
            this.label2.Visible = false;
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(103, 18);
            this.txtServer.MaxLength = 20;
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(130, 25);
            this.txtServer.TabIndex = 0;
            this.txtServer.Visible = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(29, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器";
            this.label1.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(580, 298);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.TextValue = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(468, 298);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 30);
            this.btnOK.TabIndex = 1;
            this.btnOK.TextValue = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmConfig
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(684, 334);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfig";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "配置文件设置";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_Env.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpConnect.ResumeLayout(false);
            this.grpConnect.PerformLayout();
            this.ResumeLayout(false);

        }
		#endregion


        #region 窗体变量
        public bool IsOracleOleConnect = false;                         // 是否生成针对OleDb的Oracle连接字符串
        #endregion


        #region 窗体事件
        private void frmConfig_Load(object sender, System.EventArgs e)
        {
            try
            {
                iniDisp();
            }
            catch(Exception ex)
            {                
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[退出]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[确定]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            try
            {
                saveConfig();
                
                this.Close();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion
        

        #region 共通函数
        /// <summary>
        /// 初始化界面显示
        /// </summary>
        /// <returns></returns>
        private bool iniDisp()
        {
            // 数据库连接
            // initDisp_SqlConnect();
            initDisp_OraConnect();

            // Oracle客户端采用中文字符集
            chkOraNlsZhs.Checked = (GVars.IniFile.ReadString("APP", "ORA_NLS_ZHS", "TRUE").ToUpper().Equals("TRUE"));

            // HIS为3.3版本
            chkApp3_3.Checked = (GVars.IniFile.ReadString("APP", "APP3.3", "TRUE").ToUpper().Equals("TRUE"));

            return true;
        }


        /// <summary>
        /// 初始化[SQLSERVER数据库连接字符串]
        /// </summary>
        private void initDisp_SqlConnect()
        { 
            try
            {
                string connStr = GVars.IniFile.ReadString("DATABASE", "SQL_CONNECT", string.Empty);
                connStr = GVars.EnDecryptor.Decrypt(connStr);
                
                txtServer.Text      = ConnectionStringHelper.GetParameter(connStr, "SERVER");
                txtDatabase.Text    = ConnectionStringHelper.GetParameter(connStr, "DATABASE");
                txtUserID.Text      = ConnectionStringHelper.GetParameter(connStr, "UID");
                txtPwd.Text         = ConnectionStringHelper.GetParameter(connStr, "PWD");
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        

        /// <summary>
        /// 初始化[Oracle数据库连接字符串]
        /// </summary>
        private void initDisp_OraConnect()
        { 
            try
            {
                string connStr = GVars.IniFile.ReadString("DATABASE", "ORA_CONNECT", string.Empty);
                connStr = GVars.EnDecryptor.Decrypt(connStr);

                txtOra_DataSource.Text  = ConnectionStringHelper.GetParameter(connStr, "DATA SOURCE");;
                txtOra_UserId.Text      = ConnectionStringHelper.GetParameter(connStr, "USER ID");;
                txtOra_Pwd.Text         = ConnectionStringHelper.GetParameter(connStr, "PASSWORD");;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <returns></returns>
        private bool saveConfig()
        {
            string connectStr = string.Empty;

            //// 数据库连接[SQLSERVER]
            
            //connectStr = ConnectionStringHelper.GetSQLServerConnectionString(
            //                            txtServer.Text1.Trim(), txtDatabase.Text1.Trim(), 
            //                            txtUserID.Text1.Trim(), txtPwd.Text1.Trim());
            
            //connectStr = GVars.EnDecryptor.Encrypt(connectStr);
            
            //GVars.IniFile.WriteString("DATABASE", "SQL_CONNECT", connectStr);
            
            // 数据库连接[ORACLE]
            connectStr = ConnectionStringHelper.GetOracleConnectionString(
                                        txtOra_DataSource.Text.Trim(), txtOra_UserId.Text.Trim(),
                                        txtOra_Pwd.Text.Trim());
            
            connectStr = GVars.EnDecryptor.Encrypt(connectStr);
            
            GVars.IniFile.WriteString("DATABASE", "ORA_CONNECT", connectStr);
            
            // Oracle客户端采用中文字符集
            GVars.IniFile.WriteString("APP", "ORA_NLS_ZHS", (chkOraNlsZhs.Checked ? "TRUE": "FALSE"));

            // HIS为3.3版本
            GVars.IniFile.WriteString("APP", "APP3.3", (chkApp3_3.Checked ? "TRUE": "FALSE"));

            return true;
        }
        #endregion
	}
}
