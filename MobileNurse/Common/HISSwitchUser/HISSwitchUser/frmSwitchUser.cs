//------------------------------------------------------------------------------------
//
//  系统名称        : 医院信息系统
//  子系统名称      : 通用模块
//  对象类型        : 
//  类名            : frmLogin.cs
//  功能概要        : 登录界面
//  作成者          : 付军
//  作成日          : 2007-01-22
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        :
//  变更者          :
//  变更内容        :
//  版本            :
//------------------------------------------------------------------------------------

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace HISPlus
{
	/// <summary>
	/// LoginForm 的摘要说明。
	/// </summary>
    public class frmSwitchUser : System.Windows.Forms.Form
    {
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblCopyRight;
        private System.Windows.Forms.Label lblExit;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox txtInputNo;     
        
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;
        
        public frmSwitchUser()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler( frmSwitchUser_FormClosing );            
            this.Closed += new EventHandler(LoginForm_Closed);
            this.MouseDown += new MouseEventHandler(frmLogin_MouseDown);
            this.lblAppName.MouseDown += new MouseEventHandler(frmLogin_MouseDown);
            this.lblVersion.MouseDown += new MouseEventHandler(frmLogin_MouseDown);
            this.lblCopyRight.MouseDown += new MouseEventHandler(frmLogin_MouseDown);
            
            this.txtInputNo.GotFocus += new EventHandler(textBox_GotFocus);
            this.txtPwd.GotFocus     += new EventHandler(textBox_GotFocus);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSwitchUser));
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.txtInputNo = new System.Windows.Forms.TextBox();
            this.lblAppName = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblCopyRight = new System.Windows.Forms.Label();
            this.lblExit = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPwd
            // 
            this.txtPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPwd.Location = new System.Drawing.Point(155, 248);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(103, 21);
            this.txtPwd.TabIndex = 2;
            // 
            // txtInputNo
            // 
            this.txtInputNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInputNo.Location = new System.Drawing.Point(154, 218);
            this.txtInputNo.Name = "txtInputNo";
            this.txtInputNo.Size = new System.Drawing.Size(104, 21);
            this.txtInputNo.TabIndex = 0;
            // 
            // lblAppName
            // 
            this.lblAppName.BackColor = System.Drawing.Color.Transparent;
            this.lblAppName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAppName.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblAppName.Location = new System.Drawing.Point(168, 156);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(264, 20);
            this.lblAppName.TabIndex = 17;
            this.lblAppName.Text = "应用程序名称";
            this.lblAppName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.ForeColor = System.Drawing.Color.Blue;
            this.lblVersion.Location = new System.Drawing.Point(272, 184);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(160, 16);
            this.lblVersion.TabIndex = 18;
            this.lblVersion.Text = "版本号";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCopyRight
            // 
            this.lblCopyRight.BackColor = System.Drawing.SystemColors.Highlight;
            this.lblCopyRight.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCopyRight.Location = new System.Drawing.Point(0, 284);
            this.lblCopyRight.Name = "lblCopyRight";
            this.lblCopyRight.Size = new System.Drawing.Size(440, 23);
            this.lblCopyRight.TabIndex = 19;
            this.lblCopyRight.Text = "版权信息";
            this.lblCopyRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblExit
            // 
            this.lblExit.BackColor = System.Drawing.Color.Transparent;
            this.lblExit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblExit.Location = new System.Drawing.Point(289, 247);
            this.lblExit.Name = "lblExit";
            this.lblExit.Size = new System.Drawing.Size(72, 22);
            this.lblExit.TabIndex = 20;
            this.lblExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblExit.Click += new System.EventHandler(this.lblExit_Click);
            // 
            // lblLogin
            // 
            this.lblLogin.BackColor = System.Drawing.Color.Transparent;
            this.lblLogin.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLogin.Location = new System.Drawing.Point(291, 218);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(72, 22);
            this.lblLogin.TabIndex = 21;
            this.lblLogin.Click += new System.EventHandler(this.lblLogin_Click);
            // 
            // frmSwitchUser
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(440, 308);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.lblExit);
            this.Controls.Add(this.lblCopyRight);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblAppName);
            this.Controls.Add(this.txtInputNo);
            this.Controls.Add(this.txtPwd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmSwitchUser";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LoginForm_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        

        #region 窗体级变量
        private frmLoginCom     comLocal            = new frmLoginCom();

        private string          userName            = string.Empty;                             // 用户名
        private string          password            = string.Empty;                             // 密码
        private int             attempts            = 0;                                        // 偿试登录次数
        #endregion
        

        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Load(object sender, System.EventArgs e)
        {
            try 
            {
				initVal();						// 初始化变量
                initDisp();                     // 初始化显示
            }
            catch (Exception ex)
            {
				Error.ErrProc(ex);
            }
        }

        
        /// <summary>
        /// 窗体上点击鼠标右键, 弹出配置文件配置窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        
        /// <summary>
        /// 窗体关闭前事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmSwitchUser_FormClosing( object sender, FormClosingEventArgs e )
        {
            try
            {
                if (GVars.App.QuestionExit && GVars.App.Verified == false
                && GVars.Msg.Show("Q0003") == DialogResult.No)            // 您确认要退出本系统吗? 
                {
                    e.Cancel = true;
                }
                else
                {
                    GVars.App.QuestionExit = false;
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Closed(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.Verified == false)
                {
                    Application.Exit();
                }
                else
                {
                    GVars.App.QuestionExit = true;
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        
        /// <summary>
        /// [退出]按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblExit_Click(object sender, System.EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        
        /// <summary>
        /// [登录]按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblLogin_Click(object sender, System.EventArgs e)
        {
            try 
            {
                lblLogin.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
				
                // 检查并获取界面输入
                if (chk_Get_Disp() == false)
                {
                    GVars.Msg.Show();
                    return;
                }
                
                // 登录系统
                loginSys();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                lblLogin.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }
        
        
        /// <summary>
        /// 窗体的KeyPress事件
        /// </summary>
        /// <remarks>
        /// 主要处理回车变成Tab
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // 如果是回车, 转换成Tab键
                if (e.KeyChar.Equals((char)Keys.Return))
                {
                    // 如果是在密码位置上, 相当于确认
                    if (this.ActiveControl == this.txtPwd)
                    {
                        lblLogin_Click(new object(), new System.EventArgs());
                    }
                    else
                    {
                        this.SelectNextControl(this.ActiveControl, true, true, true, true);
                    }
                    
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        
        /// <summary>
        /// 文本框获得焦点, 全选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_GotFocus(object sender, System.EventArgs e)
        {
            TextBox textBox         = (TextBox)sender;
            textBox.SelectionStart  = 0;
            textBox.SelectionLength = textBox.Text.Length;

            IME.ChangeControlIme(this.ActiveControl.Handle);
        }
        #endregion
        

		#region 共通函数
		/// <summary>
		/// 初始化变量
		/// </summary>
		private void initVal()
		{
			GVars.App.Verified  = false;
		}
        
        
        /// <summary>
        /// 初始化显示
        /// </summary>
        private void initDisp()
        {
            this.lblAppName.Text    = GVars.App.Title;              // 应用程序的名称
            this.lblCopyRight.Text  = GVars.App.CopyRight;          // 版权
            this.lblVersion.Text    = GVars.App.Version;            // 版本
			
            this.txtInputNo.Text    = GVars.IniFile.ReadString("LOGIN", "USER", string.Empty);  // 默认为上次登录成功的用户
            
			this.txtInputNo.Focus();									// 默认输入框在输入码
		}
		
        
        /// <summary>
        /// 系统登录
        /// </summary>
        private void loginSys()
        {
            bool blnResult = false;
            
            // 检查用户名/口令是否正确
            try
            {
                if (comLocal.ChkUserPwd(userName, password) == false)
                {
                    attempts++;
                    
                    GVars.Msg.ErrorSrc	    = this.txtInputNo;
                    GVars.Msg.MsgId		    = "ID003";                  // 错误的用户名/密码。请重新输入！
                    
                    GVars.Msg.Show();
                }
                else
                {
                    blnResult = true;
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            
            // 如果偿试次数超过三次, 直接退出
            if (blnResult == false)
            {
                if (attempts == 3)
                {
                    GVars.Msg.Show("ID004");                            // 三次登录失败，退出登录
                    DialogResult = DialogResult.Cancel;
                }
            }
            // 如果通过验证, 退出
            else
            {
                // 判断有无权限
                if (comLocal.GetUserInfo(userName) == false)
                {
                    // GVars.Msg.Show("E00056");                           // 您还没有使用本系统的权限!
                }                
                else
                {
                    GVars.App.Verified  = true;
                    GVars.User.UserName     = txtInputNo.Text.Trim();

                    // 记录最近使用的用户名
                    GVars.IniFile.WriteString("LOGIN", "USER", userName);
                }
                
                DialogResult = DialogResult.OK;                         // 退出登录窗体
            }
        }
        
        
        /// <summary>
        /// 检查界面输入并获取
        /// </summary>
        /// <returns></returns>
        private bool chk_Get_Disp()
        {
            if (this.txtInputNo.Text.Trim().Length == 0)
            {
                GVars.Msg.MsgId = "ID001";                              // 用户名为空。/r/n请输入用户名后点取确认按钮。
                GVars.Msg.ErrorSrc = txtInputNo;
                return false;
            }
            
            if (this.txtPwd.Text.Trim().Length == 0)
            {
                GVars.Msg.MsgId = "ID002";                              // "口令为空。/r/n请输入口令后点取确认按钮。
                GVars.Msg.ErrorSrc = txtPwd;
                return false;
            }
            
            userName = this.txtInputNo.Text.Trim().ToUpper();           // 用户名
            password = this.txtPwd.Text.Trim().ToUpper();               // 密码
            
            return true;
        }                      
        #endregion
    }
}
