//------------------------------------------------------------------------------------
//
//  ϵͳ����        : �����ƶ�ҽ��
//  ��ϵͳ����      : ������վ
//  ��������        : 
//  ����            : SettingChangePwdForm.cs
//  ���ܸ�Ҫ        : �޸�����
//  ������          : ����
//  ������          : 2006-05-16
//  �汾            : 1.0.0.0
// 
//------------------< �����ʷ >------------------------------------------------------
//  �������        :
//  �����          :
//  �������        :
//  �汾            :
//------------------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace HISPlus
{
	/// <summary>
	/// SettingChangePwdForm ��ժҪ˵����
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
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
        
        private ChangePwdCom     comLocal            = new ChangePwdCom();
        
		public SettingChangePwdForm()
		{
			//
			// Windows ���������֧���������
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
		/// ������������ʹ�õ���Դ��
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
        
        
		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
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
            this.label2.Text = "ԭ����:";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(112, 96);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 24);
            this.btnExit.TabIndex = 18;
            this.btnExit.Text = "�˳�(&E)";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(24, 96);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(80, 24);
            this.btnModify.TabIndex = 17;
            this.btnModify.Text = "ȷ��(&O)";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "ȷ  ��:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "������:";
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
            this.Text = "�޸�����";
            this.ResumeLayout(false);

        }
		#endregion
        
        
        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SettingChangePwdForm_Load( object sender, EventArgs e )
        {
        }
        
        
        /// <summary>
        /// [ȷ��]��ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModify_Click(object sender, System.EventArgs e)
        {
            try
            {                
                // �ж��������Ƿ�������ȷ
                if (this.txtPwdNewConfirm.Text.Equals(this.txtPwdNew.Text) == false)
                {
                    GVars.Msg.MsgId = "E0008";                          // ������������벻ͬ! ���������롣
                    GVars.Msg.ErrorSrc = this.txtPwdNew;
                    GVars.Msg.Show();
                    return;                
                }
                
                // �ж�ԭ���������Ƿ���ȷ
                //RoleUserSrv.UserManager webSrv = new RoleUserSrv.UserManager();
                //if (webSrv.ChkUserPwd(GVars.UserID, GVars.EnDecrypt.Encrypt(txtPwdOld.Text.Trim())) == false)
                if (comLocal.ChkUserPwd(GVars.User.UserName, txtPwdOld.Text.Trim()) == false)
                {
                    GVars.Msg.MsgId = "E0009";                          // �������!
                    GVars.Msg.ErrorSrc = this.txtPwdOld;
                    GVars.Msg.Show();
                    return;
                }
                
                // �޸�����
                if (comLocal.ChangeUserPwd(GVars.User.UserName, txtPwdNew.Text.Trim()) == true)
                {
                    GVars.Msg.MsgId = "I0002";                          // �����޸ĳɹ�!
                    GVars.Msg.Show();
                    this.Close();
                }
                else
                {
                    GVars.Msg.MsgId = "I0003";                          // �����޸�ʧ��!
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
        /// [�˳�]��ť�����¼�
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
        /// �����KeyPress�¼�
        /// </summary>
        /// <remarks>
        /// ��Ҫ����س����Tab
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingChangePwdForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ����ǻس�, ת����Tab��
            if (e.KeyChar.Equals((char)Keys.Return))
            {
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
                e.Handled = true;
            }
        }
        
        
        /// <summary>
        /// TextBox�ؼ���ȡ�����¼�
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
        /// �����˳��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingChangePwdForm_Closed(object sender, EventArgs e)
        {
            try
            {
//                GVars.Msg.MsgId = "S00001";                              // ��ӭʹ�ñ�ϵͳ!
//                GVars.Msg.Show();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
    }
}
