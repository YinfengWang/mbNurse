//------------------------------------------------------------------------------------
//
//  系统名称        : 无线移动医疗
//  子系统名称      : 护理工作站(PDA)
//  对象类型        : 
//  类名            : ChangePwd.cs
//  功能概要        : 修改密码
//  作成者          : 付军
//  作成日          : 2007-07-24
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

namespace HISPlus
{
    public partial class ChangePwd : Form
    {
        private LoginCom com = new LoginCom();

        public ChangePwd()
        {
            InitializeComponent();
            
            this.txtPwdOld.GotFocus += new EventHandler(textBox_GotFocus);
            this.txtPwd.GotFocus += new EventHandler(textBox_GotFocus);
        }

        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangePwd_Load(object sender, EventArgs e)
        {
            try
            {
                initDisp();                                             // 初始化显示
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 初始化显示
        /// </summary>
        private void initDisp()
        {
            lblAppName.Text     = GVars.App.Title;                  // 应用程序的名称
            lblCopyRight.Text   = GVars.App.CopyRight;              // 版权
            lblVersion.Text     = GVars.App.Version;                // 版本
        }
        
        
        /// <summary>
        /// [登录]按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangePwd_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                // 检查界面输入
                if (chkDisp() == false)
                {
                    Cursor.Current = Cursors.Default;
                    GVars.Msg.Show();
                    return;
                }
                
                // 修改密码
                if (changePwd(GVars.User.UserName, txtPwdOld.Text.Trim(), txtPwd.Text.Trim()) == false)
                {
                    Cursor.Current = Cursors.Default;
                    GVars.Msg.Show();
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    GVars.Msg.MsgId = "I00001";                             // 密码修改成功!
                    GVars.Msg.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
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
        /// 文本输入框获取焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_GotFocus(object sender, System.EventArgs e)
        {
            this.inputPanel1.Enabled = true;
        }
        
        
        /// <summary>
        /// 检查界面输入
        /// </summary>
        /// <returns>TRUE: 通过; FALSE: 有误</returns>
        private bool chkDisp()
        {
            // 必须输入新密码
            if (txtPwd.Text.Trim().Length == 0)
            {
                GVars.Msg.ErrorSrc = txtPwd;
                GVars.Msg.MsgId = "E00057";                             // 请输入{0}!
                GVars.Msg.MsgContent.Add(lblNewPwd.Text);
                return false;
            }
            
            // 新密码两次输入正确
            if (txtPwd.Text.Trim().Equals(txtPwd2.Text.Trim()) == false)
            {
                GVars.Msg.ErrorSrc = txtPwd;
                GVars.Msg.MsgId = "E00008";                             // 两次输入的密码不同! 请重新输入。
                return false;
            }
            
            // 密码必须用字母开头
            //string first = txtPwd.Text.Trim().Substring(0, 1);
            //if (DataType.IsNumber(first) == true)
            //{
            //    GVars.Msg.ErrorSrc = txtPwd;
            //    GVars.Msg.MsgId = "E00058";                             // 密码必须以字母开头!
            //    return false;
            //}
            
            return true;
        }
        
        
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="oldPwd">旧密码</param>
        /// <param name="newPwd">新密码</param>
        private bool changePwd(string userName, string oldPwd, string newPwd)
        {
            // 检查用户名/密码是否正确
            if (com.ChkUserPwd(userName, oldPwd) == false)
            {
                GVars.Msg.ErrorSrc = this.txtPwdOld;
                return false;
            }
            
            // 修改密码
            if (com.ChangeUserPwd(userName, newPwd) == false)
            {
                GVars.Msg.ErrorSrc = this.txtPwd;                
                return false;
            }
            
            return true;
        }        
    }
}