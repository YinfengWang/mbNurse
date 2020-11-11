//------------------------------------------------------------------------------------
//
//  ϵͳ����        : ҽԺ��Ϣϵͳ
//  ��ϵͳ����      : ͨ��ģ��
//  ��������        : 
//  ����            : frmLogin.cs
//  ���ܸ�Ҫ        : ��¼����
//  ������          : ����
//  ������          : 2007-01-22
//  �汾            : 1.0.0.0
// 
//------------------< �����ʷ >------------------------------------------------------
//  �������        :
//  �����          :
//  �������        :
//  �汾            :
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
	/// LoginForm ��ժҪ˵����
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
        /// ����������������
        /// </summary>
        private System.ComponentModel.Container components = null;
        
        public frmSwitchUser()
        {
            //
            // Windows ���������֧���������
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
            this.lblAppName.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAppName.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblAppName.Location = new System.Drawing.Point(168, 156);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(264, 20);
            this.lblAppName.TabIndex = 17;
            this.lblAppName.Text = "Ӧ�ó�������";
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
            this.lblVersion.Text = "�汾��";
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
            this.lblCopyRight.Text = "��Ȩ��Ϣ";
            this.lblCopyRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblExit
            // 
            this.lblExit.BackColor = System.Drawing.Color.Transparent;
            this.lblExit.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.lblLogin.Font = new System.Drawing.Font("����", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.Text = "��¼";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LoginForm_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        

        #region ���弶����
        private frmLoginCom     comLocal            = new frmLoginCom();

        private string          userName            = string.Empty;                             // �û���
        private string          password            = string.Empty;                             // ����
        private int             attempts            = 0;                                        // ���Ե�¼����
        #endregion
        

        #region �����¼�
        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Load(object sender, System.EventArgs e)
        {
            try 
            {
				initVal();						// ��ʼ������
                initDisp();                     // ��ʼ����ʾ
            }
            catch (Exception ex)
            {
				Error.ErrProc(ex);
            }
        }

        
        /// <summary>
        /// �����ϵ������Ҽ�, ���������ļ����ô���
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
        /// ����ر�ǰ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmSwitchUser_FormClosing( object sender, FormClosingEventArgs e )
        {
            try
            {
                if (GVars.App.QuestionExit && GVars.App.Verified == false
                && GVars.Msg.Show("Q0003") == DialogResult.No)            // ��ȷ��Ҫ�˳���ϵͳ��? 
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
        /// ����ر��¼�
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
        /// [�˳�]��ť�����¼�
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
        /// [��¼]��ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblLogin_Click(object sender, System.EventArgs e)
        {
            try 
            {
                lblLogin.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
				
                // ��鲢��ȡ��������
                if (chk_Get_Disp() == false)
                {
                    GVars.Msg.Show();
                    return;
                }
                
                // ��¼ϵͳ
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
        /// �����KeyPress�¼�
        /// </summary>
        /// <remarks>
        /// ��Ҫ����س����Tab
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // ����ǻس�, ת����Tab��
                if (e.KeyChar.Equals((char)Keys.Return))
                {
                    // �����������λ����, �൱��ȷ��
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
        /// �ı����ý���, ȫѡ��
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
        

		#region ��ͨ����
		/// <summary>
		/// ��ʼ������
		/// </summary>
		private void initVal()
		{
			GVars.App.Verified  = false;
		}
        
        
        /// <summary>
        /// ��ʼ����ʾ
        /// </summary>
        private void initDisp()
        {
            this.lblAppName.Text    = GVars.App.Title;              // Ӧ�ó��������
            this.lblCopyRight.Text  = GVars.App.CopyRight;          // ��Ȩ
            this.lblVersion.Text    = GVars.App.Version;            // �汾
			
            this.txtInputNo.Text    = GVars.IniFile.ReadString("LOGIN", "USER", string.Empty);  // Ĭ��Ϊ�ϴε�¼�ɹ����û�
            
			this.txtInputNo.Focus();									// Ĭ���������������
		}
		
        
        /// <summary>
        /// ϵͳ��¼
        /// </summary>
        private void loginSys()
        {
            bool blnResult = false;
            
            // ����û���/�����Ƿ���ȷ
            try
            {
                if (comLocal.ChkUserPwd(userName, password) == false)
                {
                    attempts++;
                    
                    GVars.Msg.ErrorSrc	    = this.txtInputNo;
                    GVars.Msg.MsgId		    = "ID003";                  // ������û���/���롣���������룡
                    
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
            
            // ������Դ�����������, ֱ���˳�
            if (blnResult == false)
            {
                if (attempts == 3)
                {
                    GVars.Msg.Show("ID004");                            // ���ε�¼ʧ�ܣ��˳���¼
                    DialogResult = DialogResult.Cancel;
                }
            }
            // ���ͨ����֤, �˳�
            else
            {
                // �ж�����Ȩ��
                if (comLocal.GetUserInfo(userName) == false)
                {
                    // GVars.Msg.Show("E00056");                           // ����û��ʹ�ñ�ϵͳ��Ȩ��!
                }                
                else
                {
                    GVars.App.Verified  = true;
                    GVars.User.UserName     = txtInputNo.Text.Trim();

                    // ��¼���ʹ�õ��û���
                    GVars.IniFile.WriteString("LOGIN", "USER", userName);
                }
                
                DialogResult = DialogResult.OK;                         // �˳���¼����
            }
        }
        
        
        /// <summary>
        /// ���������벢��ȡ
        /// </summary>
        /// <returns></returns>
        private bool chk_Get_Disp()
        {
            if (this.txtInputNo.Text.Trim().Length == 0)
            {
                GVars.Msg.MsgId = "ID001";                              // �û���Ϊ�ա�/r/n�������û������ȡȷ�ϰ�ť��
                GVars.Msg.ErrorSrc = txtInputNo;
                return false;
            }
            
            if (this.txtPwd.Text.Trim().Length == 0)
            {
                GVars.Msg.MsgId = "ID002";                              // "����Ϊ�ա�/r/n�����������ȡȷ�ϰ�ť��
                GVars.Msg.ErrorSrc = txtPwd;
                return false;
            }
            
            userName = this.txtInputNo.Text.Trim().ToUpper();           // �û���
            password = this.txtPwd.Text.Trim().ToUpper();               // ����
            
            return true;
        }                      
        #endregion
    }
}
