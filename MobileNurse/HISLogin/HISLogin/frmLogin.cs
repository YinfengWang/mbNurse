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
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using System.Resources;
using DevExpress.XtraEditors;
//using AutoUpdate.AuotUpdate;

namespace HISPlus
{
    /// <summary>
    /// LoginForm 的摘要说明。
    /// </summary>
    public class frmLogin : FormDo1
    {
        private DevExpress.XtraEditors.LabelControl lblAppName;
        private DevExpress.XtraEditors.LabelControl lblVersion;
        private DevExpress.XtraEditors.LabelControl lblCopyRight;
        private DevExpress.XtraEditors.LabelControl lblExit;
        private DevExpress.XtraEditors.LabelControl lblLogin;
        private TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtPwd;
        private IContainer components;
        //private DateTime _currentTime;
        //private string _updateExe = string.Empty;
        //private string _updFlagFile = string.Empty;

        public frmLogin()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            _id = "00010";
            _guid = "3533543a-26c7-4752-ba3e-ee14069c8667";
            //_currentTime = GVars.OracleAccess.GetSysDate();

            this.Closed += new EventHandler(LoginForm_Closed);
            this.MouseDown += new MouseEventHandler(frmLogin_MouseDown);
            this.lblAppName.MouseDown += new MouseEventHandler(frmLogin_MouseDown);
            this.lblVersion.MouseDown += new MouseEventHandler(frmLogin_MouseDown);
            this.lblCopyRight.MouseDown += new MouseEventHandler(frmLogin_MouseDown);

            this.txtName.GotFocus += new EventHandler(textBox_GotFocus);
            this.txtPwd.GotFocus += new EventHandler(textBox_GotFocus);

            //checkNeedUpdate();
        }

        


        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.lblAppName = new DevExpress.XtraEditors.LabelControl();
            this.lblVersion = new DevExpress.XtraEditors.LabelControl();
            this.lblCopyRight = new DevExpress.XtraEditors.LabelControl();
            this.lblExit = new DevExpress.XtraEditors.LabelControl();
            this.lblLogin = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtPwd = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAppName
            // 
            this.lblAppName.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblAppName.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAppName.Appearance.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblAppName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblAppName.Location = new System.Drawing.Point(12, 94);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(102, 16);
            this.lblAppName.TabIndex = 17;
            this.lblAppName.Text = "应用程序名称";
            this.lblAppName.Visible = false;
            // 
            // lblVersion
            // 
            this.lblVersion.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblVersion.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblVersion.Location = new System.Drawing.Point(329, 98);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(36, 14);
            this.lblVersion.TabIndex = 18;
            this.lblVersion.Text = "版本号";
            this.lblVersion.Visible = false;
            // 
            // lblCopyRight
            // 
            this.lblCopyRight.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyRight.Appearance.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCopyRight.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblCopyRight.Location = new System.Drawing.Point(3, 278);
            this.lblCopyRight.Name = "lblCopyRight";
            this.lblCopyRight.Size = new System.Drawing.Size(48, 14);
            this.lblCopyRight.TabIndex = 19;
            this.lblCopyRight.Text = "版权信息";
            this.lblCopyRight.Visible = false;
            // 
            // lblExit
            // 
            this.lblExit.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblExit.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblExit.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblExit.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblExit.Location = new System.Drawing.Point(235, 242);
            this.lblExit.Name = "lblExit";
            this.lblExit.Size = new System.Drawing.Size(94, 37);
            this.lblExit.TabIndex = 20;
            this.lblExit.Click += new System.EventHandler(this.lblExit_Click);
            // 
            // lblLogin
            // 
            this.lblLogin.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblLogin.Appearance.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLogin.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLogin.Location = new System.Drawing.Point(124, 243);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(93, 37);
            this.lblLogin.TabIndex = 21;
            this.lblLogin.Click += new System.EventHandler(this.lblLogin_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(201, 148);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.txtName.Properties.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Properties.Appearance.Options.UseBackColor = true;
            this.txtName.Properties.Appearance.Options.UseFont = true;
            this.txtName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtName.Size = new System.Drawing.Size(149, 20);
            this.txtName.TabIndex = 23;
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(201, 199);
            this.txtPwd.Margin = new System.Windows.Forms.Padding(4);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.txtPwd.Properties.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPwd.Properties.Appearance.Options.UseBackColor = true;
            this.txtPwd.Properties.Appearance.Options.UseFont = true;
            this.txtPwd.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtPwd.Properties.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(149, 20);
            this.txtPwd.TabIndex = 24;
            // 
            // frmLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(442, 310);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.lblExit);
            this.Controls.Add(this.lblCopyRight);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblAppName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LoginForm_KeyPress);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmLogin_MouseDown_1);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmLogin_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmLogin_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPwd.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        #region 窗体级变量
        private frmLoginCom comLocal = new frmLoginCom();

        private string userName = string.Empty;                             // 用户名
        private string password = string.Empty;                             // 密码
        private int attempts = 0;                                        // 偿试登录次数
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
                //lblLogin.Cursor

                initVal();						// 初始化变量
                initDisp();                     // 初始化显示

                //  如果有用户名与密码, 偿试直接用用户与密码登录
                if (GVars.User.UserName.Length > 0 && GVars.User.PWD.Length > 0)
                {
                    lblLogin_Click(sender, e);
                }
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
                if (e.Button == MouseButtons.Right)
                {
                    string dllName = GVars.IniFile.ReadString("CONFIG_SETTING", "DLL_NAME", string.Empty);
                    string frmName = GVars.IniFile.ReadString("CONFIG_SETTING", "FORM_NAME", string.Empty);

                    if (dllName.Length > 0 && frmName.Length > 0)
                    {
                        dllName = Path.Combine(Application.StartupPath, dllName);
                        Form configFrm = DllOperator.GetFormInDll(dllName, frmName);
                        configFrm.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
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
                this.Close();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        /// [退出]按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
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
            catch (Exception ex)
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
                    if (this.ActiveControl == this.txtPwd || this.ActiveControl.Parent == this.txtPwd)
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
            TextEdit textBox = (TextEdit)sender;
            textBox.SelectionStart = 0;
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
            GVars.App.Verified = false;
        }


        /// <summary>
        /// 初始化显示
        /// </summary>
        private void initDisp()
        {
            this.lblAppName.Text = GVars.App.Title;              // 应用程序的名称
            this.lblCopyRight.Text = GVars.App.CopyRight;          // 版权
            this.lblVersion.Text = GVars.App.Version;            // 版本

            if (GVars.User.UserName.Length == 0)
            {
                this.txtName.Text = GVars.IniFile.ReadString("LOGIN", "USER", string.Empty);  // 默认为上次登录成功的用户
            }
            else
            {
                this.txtName.Text = GVars.User.UserName;
            }

            this.txtPwd.Text = GVars.User.PWD;

            this.txtName.Focus();							    // 默认输入框在输入码
        }


        /// <summary>
        /// 系统登录
        /// </summary>
        private void loginSys()
        {
            bool blnResult = false;

            // 加载本地设置
            comLocal.LoadAppSetting_Local();

            // 不检查授权!!!
            // 检查授权
            if (comLocal.CheckAuthorization() == false)
            {
                GVars.Msg.Show();
                return;
            }

            // 检查用户名/口令是否正确
            GVars.App.Verified = false;

            try
            {
                if (comLocal.ChkUserPwd(userName, password) == false)
                {
                    attempts++;

                    GVars.Msg.ErrorSrc = this.txtName;
                    GVars.Msg.MsgId = "ID003";                  // 错误的用户名/密码。请重新输入！

                    GVars.Msg.Show();
                }
                else
                {
                    GVars.User.PWD = password;
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }

            // 如果偿试次数超过三次, 直接退出
            if (blnResult == false)
            {
                if (attempts == 3)
                {
                    GVars.Msg.Show("ID004");                            // 三次登录失败，退出登录
                    this.Close();
                }
            }
            // 如果通过验证, 退出
            else
            {
                this.Close();                                           // 退出登录窗体

                // 判断有无权限
                if (comLocal.GetUserInfo(userName) == false)
                {
                    GVars.Msg.Show("ED006");                            // 您还没有使用本系统的权限!
                }
                else
                {
                    GVars.App.Verified = true;
                    GVars.User.UserName = txtName.Text.Trim();

                    // 记录最近使用的用户名
                    GVars.IniFile.WriteString("LOGIN", "USER", userName);
                }
            }
        }


        /// <summary>
        /// 检查界面输入并获取
        /// </summary>
        /// <returns></returns>
        private bool chk_Get_Disp()
        {
            if (this.txtName.Text.Trim().Length == 0)
            {
                GVars.Msg.MsgId = "ID001";                              // 用户名为空。/r/n请输入用户名后点取确认按钮。
                GVars.Msg.ErrorSrc = txtName;
                return false;
            }

            //if (this.txtPwd.Text1.Trim().Length == 0)
            //{
            //    GVars.Msg.MsgId = "ID002";                              // "口令为空。/r/n请输入口令后点取确认按钮。
            //    GVars.Msg.ErrorSrc = txtPwd;
            //    return false;
            //}

            userName = this.txtName.Text.Trim().ToUpper();           // 用户名
            password = this.txtPwd.Text.Trim().ToUpper();               // 密码

            return true;
        }
        #endregion


        #region   窗体运动

        private Point MousePos;        //记录鼠标指针的坐标
        private bool bMouseDown = false;

        private void frmLogin_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MousePos.X = -e.X - SystemInformation.FrameBorderSize.Width;
                MousePos.Y = -e.Y - SystemInformation.CaptionHeight - SystemInformation.FrameBorderSize.Height;
                bMouseDown = true;
            }
        }


        private void frmLogin_MouseMove(object sender, MouseEventArgs e)
        {
            if (bMouseDown)
            {
                Point CurrentPos = Control.MousePosition;
                CurrentPos.Offset(MousePos.X, MousePos.Y);
                Location = CurrentPos;
            }
        }
        private void frmLogin_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                bMouseDown = false;
            }
        }

        #endregion

        //private void checkNeedUpdate()
        //{
        //    try
        //    {

        //        AutoUpdateWebSrv autoUpdSrv = new AutoUpdateWebSrv();
        //        IniFile ini = new IniFile(Path.Combine(Application.StartupPath, "AutoUpdate.ini"));

        //        string updFileName = Path.Combine(Application.StartupPath, "UpdFileList.xml");
        //        string appCode = ini.ReadString("SETTING", "APP_CODE", "").Trim();
        //        string serverIp = ini.ReadString("SETTING", "SERVER_IP", "").Trim();

        //        _updFlagFile = Path.Combine(Application.StartupPath, "UpdateFlag");
        //        _updateExe = ini.ReadString("SETTING", "EXE", "").Trim();

        //        if (_updateExe.Length > 0)
        //        {
        //            _updateExe = Path.Combine(Application.StartupPath, _updateExe);
        //        }

        //        if (appCode.Length == 0 || serverIp.Length == 0)
        //        {
        //            return;
        //        }

        //        autoUpdSrv.Url = ChangeIpInUrl(autoUpdSrv.Url, serverIp);
        //        DataSet ds = null;
        //        bool blnNeedUpdate = false;



        //        //for (int i = 0; i < 10; i++)
        //        //{
        //        //    if (_exit) return;
        //        //    Thread.Sleep(1000);

        //        //}

        //        try
        //        {
        //            if (File.Exists(updFileName) == false)
        //            {
        //                blnNeedUpdate = autoUpdSrv.CheckUpdated(appCode, null);
        //            }
        //            else
        //            {
        //                ds = new DataSet();
        //                ds.ReadXml(updFileName, XmlReadMode.ReadSchema);
        //                blnNeedUpdate = autoUpdSrv.CheckUpdated(appCode, ds);
        //            }

        //            MessageBox.Show(ds.Tables[0].Rows.Count.ToString() + blnNeedUpdate.ToString());
        //            if (blnNeedUpdate && File.Exists(_updFlagFile) == false)
        //            {
        //                File.Create(_updFlagFile);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //        }

        //    }

        //    catch (Exception ex)
        //    { }
        //    finally
        //    {

        //    }
        //}

        //static public string ChangeIpInUrl(string url, string newIp)
        //{
        //    int pos = url.IndexOf("//");
        //    string header = url.Substring(0, pos + 2);

        //    pos = url.IndexOf("/", pos + 2);
        //    string tail = url.Substring(pos + 1);

        //    return header + newIp + "/" + tail;
        //}
    }
}
