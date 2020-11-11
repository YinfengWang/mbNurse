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
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using System.Resources;
using DevExpress.XtraEditors;
//using AutoUpdate.AuotUpdate;

namespace HISPlus
{
    /// <summary>
    /// LoginForm ��ժҪ˵����
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
            // Windows ���������֧���������
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
        /// ������������ʹ�õ���Դ��
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


        #region Windows ������������ɵĴ���
        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
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
            this.lblAppName.Appearance.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAppName.Appearance.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblAppName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblAppName.Location = new System.Drawing.Point(12, 94);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(102, 16);
            this.lblAppName.TabIndex = 17;
            this.lblAppName.Text = "Ӧ�ó�������";
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
            this.lblVersion.Text = "�汾��";
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
            this.lblCopyRight.Text = "��Ȩ��Ϣ";
            this.lblCopyRight.Visible = false;
            // 
            // lblExit
            // 
            this.lblExit.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblExit.Appearance.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.lblLogin.Appearance.Font = new System.Drawing.Font("����", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.txtName.Properties.Appearance.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.txtPwd.Properties.Appearance.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.Text = "��¼";
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


        #region ���弶����
        private frmLoginCom comLocal = new frmLoginCom();

        private string userName = string.Empty;                             // �û���
        private string password = string.Empty;                             // ����
        private int attempts = 0;                                        // ���Ե�¼����
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
                //lblLogin.Cursor

                initVal();						// ��ʼ������
                initDisp();                     // ��ʼ����ʾ

                //  ������û���������, ����ֱ�����û��������¼
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
        /// �����ϵ������Ҽ�, ���������ļ����ô���
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
        /// ����ر��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Closed(object sender, EventArgs e)
        {
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
                this.Close();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        /// [�˳�]��ť�����¼�
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
        /// �ı����ý���, ȫѡ��
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


        #region ��ͨ����
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void initVal()
        {
            GVars.App.Verified = false;
        }


        /// <summary>
        /// ��ʼ����ʾ
        /// </summary>
        private void initDisp()
        {
            this.lblAppName.Text = GVars.App.Title;              // Ӧ�ó��������
            this.lblCopyRight.Text = GVars.App.CopyRight;          // ��Ȩ
            this.lblVersion.Text = GVars.App.Version;            // �汾

            if (GVars.User.UserName.Length == 0)
            {
                this.txtName.Text = GVars.IniFile.ReadString("LOGIN", "USER", string.Empty);  // Ĭ��Ϊ�ϴε�¼�ɹ����û�
            }
            else
            {
                this.txtName.Text = GVars.User.UserName;
            }

            this.txtPwd.Text = GVars.User.PWD;

            this.txtName.Focus();							    // Ĭ���������������
        }


        /// <summary>
        /// ϵͳ��¼
        /// </summary>
        private void loginSys()
        {
            bool blnResult = false;

            // ���ر�������
            comLocal.LoadAppSetting_Local();

            // �������Ȩ!!!
            // �����Ȩ
            if (comLocal.CheckAuthorization() == false)
            {
                GVars.Msg.Show();
                return;
            }

            // ����û���/�����Ƿ���ȷ
            GVars.App.Verified = false;

            try
            {
                if (comLocal.ChkUserPwd(userName, password) == false)
                {
                    attempts++;

                    GVars.Msg.ErrorSrc = this.txtName;
                    GVars.Msg.MsgId = "ID003";                  // ������û���/���롣���������룡

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

            // ������Դ�����������, ֱ���˳�
            if (blnResult == false)
            {
                if (attempts == 3)
                {
                    GVars.Msg.Show("ID004");                            // ���ε�¼ʧ�ܣ��˳���¼
                    this.Close();
                }
            }
            // ���ͨ����֤, �˳�
            else
            {
                this.Close();                                           // �˳���¼����

                // �ж�����Ȩ��
                if (comLocal.GetUserInfo(userName) == false)
                {
                    GVars.Msg.Show("ED006");                            // ����û��ʹ�ñ�ϵͳ��Ȩ��!
                }
                else
                {
                    GVars.App.Verified = true;
                    GVars.User.UserName = txtName.Text.Trim();

                    // ��¼���ʹ�õ��û���
                    GVars.IniFile.WriteString("LOGIN", "USER", userName);
                }
            }
        }


        /// <summary>
        /// ���������벢��ȡ
        /// </summary>
        /// <returns></returns>
        private bool chk_Get_Disp()
        {
            if (this.txtName.Text.Trim().Length == 0)
            {
                GVars.Msg.MsgId = "ID001";                              // �û���Ϊ�ա�/r/n�������û������ȡȷ�ϰ�ť��
                GVars.Msg.ErrorSrc = txtName;
                return false;
            }

            //if (this.txtPwd.Text1.Trim().Length == 0)
            //{
            //    GVars.Msg.MsgId = "ID002";                              // "����Ϊ�ա�/r/n�����������ȡȷ�ϰ�ť��
            //    GVars.Msg.ErrorSrc = txtPwd;
            //    return false;
            //}

            userName = this.txtName.Text.Trim().ToUpper();           // �û���
            password = this.txtPwd.Text.Trim().ToUpper();               // ����

            return true;
        }
        #endregion


        #region   �����˶�

        private Point MousePos;        //��¼���ָ�������
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
