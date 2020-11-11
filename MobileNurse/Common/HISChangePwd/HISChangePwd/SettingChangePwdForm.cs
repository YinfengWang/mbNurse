//------------------------------------------------------------------------------------
//
//  系统名称        : 无线移动医疗
//  子系统名称      : 护理工作站
//  对象类型        : 
//  类名            : SettingChangePwdForm.cs
//  功能概要        : 修改密码
//  作成者          : 付军
//  作成日          : 2006-05-16
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
	/// SettingChangePwdForm 的摘要说明。
	/// </summary>
	public class SettingChangePwdForm : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPwdOld;
        private System.Windows.Forms.TextBox txtPwdNewConfirm;
        private System.Windows.Forms.TextBox txtPwdNew;
        private System.Windows.Forms.Button btnModify;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
        
        private ChangePwdCom     comLocal            = new ChangePwdCom();
        
		public SettingChangePwdForm()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

            this.Load += new EventHandler( SettingChangePwdForm_Load );
            
			this.KeyPress += new KeyPressEventHandler(SettingChangePwdForm_KeyPress);
            this.Closed += new EventHandler(SettingChangePwdForm_Closed);
            
            this.txtPwdOld.GotFocus += new EventHandler(textBox_GotFocus);
            this.txtPwdNew.GotFocus += new EventHandler(textBox_GotFocus);
            this.txtPwdNewConfirm.GotFocus += new EventHandler(textBox_GotFocus);
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SettingChangePwdForm));
            this.txtPwdOld = new System.Windows.Forms.TextBox();
            this.txtPwdNewConfirm = new System.Windows.Forms.TextBox();
            this.txtPwdNew = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPwdOld
            // 
            this.txtPwdOld.Location = new System.Drawing.Point(72, 16);
            this.txtPwdOld.Name = "txtPwdOld";
            this.txtPwdOld.PasswordChar = '*';
            this.txtPwdOld.Size = new System.Drawing.Size(120, 21);
            this.txtPwdOld.TabIndex = 14;
            this.txtPwdOld.Text = "";
            // 
            // txtPwdNewConfirm
            // 
            this.txtPwdNewConfirm.Location = new System.Drawing.Point(72, 64);
            this.txtPwdNewConfirm.Name = "txtPwdNewConfirm";
            this.txtPwdNewConfirm.PasswordChar = '*';
            this.txtPwdNewConfirm.Size = new System.Drawing.Size(120, 21);
            this.txtPwdNewConfirm.TabIndex = 16;
            this.txtPwdNewConfirm.Text = "";
            // 
            // txtPwdNew
            // 
            this.txtPwdNew.Location = new System.Drawing.Point(72, 40);
            this.txtPwdNew.Name = "txtPwdNew";
            this.txtPwdNew.PasswordChar = '*';
            this.txtPwdNew.Size = new System.Drawing.Size(120, 21);
            this.txtPwdNew.TabIndex = 15;
            this.txtPwdNew.Text = "";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 21;
            this.label2.Text = "原密码:";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(112, 96);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 24);
            this.btnExit.TabIndex = 18;
            this.btnExit.Text = "退出(&E)";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(24, 96);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(80, 24);
            this.btnModify.TabIndex = 17;
            this.btnModify.Text = "确认(&O)";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "确  认:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "新密码:";
            // 
            // SettingChangePwdForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(216, 134);
            this.Controls.Add(this.txtPwdOld);
            this.Controls.Add(this.txtPwdNewConfirm);
            this.Controls.Add(this.txtPwdNew);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "SettingChangePwdForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "修改密码";
            this.ResumeLayout(false);

        }
		#endregion
        
        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SettingChangePwdForm_Load( object sender, EventArgs e )
        {
        }
        
        
        /// <summary>
        /// [确认]按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModify_Click(object sender, System.EventArgs e)
        {
            try
            {                
                // 判断新密码是否输入正确
                if (this.txtPwdNewConfirm.Text.Equals(this.txtPwdNew.Text) == false)
                {
                    GVars.Msg.MsgId = "E0008";                          // 两次输入的密码不同! 请重新输入。
                    GVars.Msg.ErrorSrc = this.txtPwdNew;
                    GVars.Msg.Show();
                    return;                
                }
                
                // 判断原来的密码是否正确
                //RoleUserSrv.UserManager webSrv = new RoleUserSrv.UserManager();
                //if (webSrv.ChkUserPwd(GVars.UserID, GVars.EnDecrypt.Encrypt(txtPwdOld.Text.Trim())) == false)
                if (comLocal.ChkUserPwd(GVars.User.UserName, txtPwdOld.Text.Trim()) == false)
                {
                    GVars.Msg.MsgId = "E0009";                          // 密码错误!
                    GVars.Msg.ErrorSrc = this.txtPwdOld;
                    GVars.Msg.Show();
                    return;
                }
                
                // 修改密码
                if (comLocal.ChangeUserPwd(GVars.User.UserName, txtPwdNew.Text.Trim()) == true)
                {
                    GVars.Msg.MsgId = "I0002";                          // 密码修改成功!
                    GVars.Msg.Show();
                    this.Close();
                }
                else
                {
                    GVars.Msg.MsgId = "I0003";                          // 密码修改失败!
                    GVars.Msg.ErrorSrc = this.txtPwdNew;
                    GVars.Msg.Show();
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
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
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
        private void SettingChangePwdForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 如果是回车, 转换成Tab键
            if (e.KeyChar.Equals((char)Keys.Return))
            {
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
                e.Handled = true;
            }
        }
        
        
        /// <summary>
        /// TextBox控件获取焦点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_GotFocus(object sender, EventArgs e)
        {
            ((System.Windows.Forms.TextBox)this.ActiveControl).SelectionStart = 0;
            ((System.Windows.Forms.TextBox)this.ActiveControl).SelectionLength = this.ActiveControl.Text.Length;

            IME.ChangeControlIme(this.ActiveControl.Handle);
        }
        
        
        /// <summary>
        /// 窗体退出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingChangePwdForm_Closed(object sender, EventArgs e)
        {
            try
            {
//                GVars.Msg.MsgId = "S00001";                              // 欢迎使用本系统!
//                GVars.Msg.Show();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
    }
}
