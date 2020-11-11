//------------------------------------------------------------------------------------
//
//  系统名称        : 无线移动医疗
//  子系统名称      : 护理工作站(PDA)
//  对象类型        : 
//  类名            : LoginForm.cs
//  功能概要        : 登录界面
//  作成者          : 付军
//  作成日          : 2007-05-30
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        :
//  变更者          :
//  变更内容        :
//  版本            :
//------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace HISPlus
{
    public partial class LoginForm : Form
    {
        private LoginCom com = new LoginCom();

        Stopwatch watchLogin;
        public LoginForm()
        {
            InitializeComponent();

            this.txtName.GotFocus += new EventHandler(textBox_GotFocus);
            this.txtPwd.GotFocus += new EventHandler(textBox_GotFocus);
            
            this.picBackground.MouseUp += new MouseEventHandler(picBackground_MouseUp);
        }

        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Load(object sender, EventArgs e)
        {
             
            try
            {
                GVars.App.Verified = false;
                picBackground.SizeMode = PictureBoxSizeMode.StretchImage;

                initDisp();                                             // 初始化显示
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        

        /// <summary>
        /// 文本输入框获取焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_GotFocus(object sender, System.EventArgs e)
        {
            this.inputPanel1.Enabled = true;
        }
        
        
        /// <summary>
        /// 登录或退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void picBackground_MouseUp(object sender, MouseEventArgs e)
        {
            if (btnLogin.Bounds.Contains(e.X, e.Y))
            {
                btnLogin_Click(sender, e);
                return;
            }

            if (btnExit.Bounds.Contains(e.X, e.Y))
            {
                btnExit_Click(sender, e);
                return;
            }

            #region old method
            /*
             //btnLogin_Click(sender, e);
            //return;

            // 如果是单击登录  两个不同的 分辨率 的 按钮 坐标
            RectangleF rectLogin = new RectangleF(67.0F, 320.0F, 348.0F, 42.0F);
           //RectangleF rectLogin = new RectangleF(70.0F, 177.0F, 240.0F, 268.0F); 
           //  RectangleF rectLogin = new RectangleF(25.0F, 168.0F, 80.0F, 20.0F); 
            if (rectLogin.X < e.X && e.X < rectLogin.X + rectLogin.Width)
            {
                if (rectLogin.Y < e.Y && e.Y < rectLogin.Y + rectLogin.Height)
                {
                    btnLogin_Click(sender, e);
                    return;
                }
            }

            // 如果是单击退出
            RectangleF rectExit = new RectangleF(69.0F, 379.0F, 348.0F, 42.0F);
           //RectangleF rectExit = new RectangleF(165.0F, 177.0F, 240.0F, 268.0F);
            // RectangleF rectExit = new RectangleF(115.0F, 168.0F, 80.0F, 20.0F);
            if (rectExit.X < e.X && e.X < rectExit.X + rectExit.Width)
            {
                if (rectExit.Y < e.Y && e.Y < rectExit.Y + rectExit.Height)
                {
                    btnExit_Click(sender, e);
                    return;
                }
            }
             */
            #endregion
        }

        
        /// <summary>
        /// [登录]按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            watchLogin = Stopwatch.StartNew();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                loginSys();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;

                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            watchLogin.Stop();
            string time = watchLogin.ElapsedMilliseconds.ToString();

            //记录扫码所用时间
            COMAPP.Function.SystemLog.RecordExpendTime("LoginForm", "Login", GVars.User.Name, System.DateTime.Now.ToString(), time);
        }

        
        /// <summary>
        /// [退出]按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
                
        
        /// <summary>
        /// 初始化显示
        /// </summary>
        private void initDisp()
        {
            // 信息显示
            this.Text = GVars.App.Title;
            
            lblAppName.Text     = GVars.App.Title;                  // 应用程序的名称
            lblCopyRight.Text   = GVars.App.CopyRight;              // 版权
            lblVersion.Text     = GVars.App.Version;                // 版本
        }
        

        /// <summary>
        /// 登录系统
        /// </summary>
        private void loginSys()
        {
            Cursor.Current = Cursors.WaitCursor;

            // 检查用户名/密码
            if (com.ChkUserPwd(txtName.Text, txtPwd.Text) == false)
            {
                GVars.Msg.ErrorSrc = this.txtName;
                
                GVars.Msg.Show();
                return;
            }
            
            // 获取用户信息
            GVars.User.Name = txtName.Text.ToUpper().Trim();
            
            if (GVars.User.Name.Equals("ADMIN") == false
                && com.GetUserInfo(txtName.Text, txtPwd.Text) == false)
            {
                GVars.Msg.ErrorSrc = this.txtName;
                GVars.Msg.Show();
            }
            else
            {
                this.Close();                                           // 退出登录窗体
                
                GVars.App.Verified = true;                              // 记录登录成功
            }
        }
    }
}