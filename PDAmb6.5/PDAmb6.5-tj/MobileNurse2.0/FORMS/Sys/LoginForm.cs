//------------------------------------------------------------------------------------
//
//  ϵͳ����        : �����ƶ�ҽ��
//  ��ϵͳ����      : ������վ(PDA)
//  ��������        : 
//  ����            : LoginForm.cs
//  ���ܸ�Ҫ        : ��¼����
//  ������          : ����
//  ������          : 2007-05-30
//  �汾            : 1.0.0.0
// 
//------------------< �����ʷ >------------------------------------------------------
//  �������        :
//  �����          :
//  �������        :
//  �汾            :
//------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

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
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Load(object sender, EventArgs e)
        {

            try
            {
                GVars.App.Verified = false;
                picBackground.SizeMode = PictureBoxSizeMode.StretchImage;

                initDisp();                                             // ��ʼ����ʾ
                string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
                string configFile = Path.Combine(appPath, "DefaultUser.txt");

                if (File.Exists(configFile))
                {
                    //�������Ĭ���˻����ļ�

                    this.txtName.GotFocus -= new EventHandler(textBox_GotFocus);
                    this.txtPwd.GotFocus -= new EventHandler(textBox_GotFocus);

                    using (StreamReader sr = new StreamReader(configFile, Encoding.Default))
                    {
                        string strUserInfo = sr.ReadToEnd();
                        if (!string.IsNullOrEmpty(strUserInfo))
                        {
                            //�˺�,�������
                            //��ʾ

                            string[] arrUserInfo = strUserInfo.Split("\r\n".ToCharArray());

                            txtName.Text = arrUserInfo[0];
                            txtPwd.Text = arrUserInfo[2];
                        }
                        else
                        {

                        }
                    }

                }
                else
                {
                    //�������ļ����򴴽�
                    File.Create(configFile).Close();

                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }


        /// <summary>
        /// �ı�������ȡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_GotFocus(object sender, System.EventArgs e)
        {
            this.inputPanel1.Enabled = true;
        }


        /// <summary>
        /// ��¼���˳�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void picBackground_MouseUp(object sender, MouseEventArgs e)
        {
            //btnLogin_Click(sender, e);
            //return;

            // ����ǵ�����¼  ������ͬ�� �ֱ��� �� ��ť ����
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

            // ����ǵ����˳�
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
        }


        /// <summary>
        /// [��¼]��ť�����¼�
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
                //��ǰ��¼���û�д���ļ�
                InsDefaultUset();
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

            //��¼ɨ������ʱ��
            COMAPP.Function.SystemLog.RecordExpendTime("LoginForm", "Login", GVars.User.Name, System.DateTime.Now.ToString(), time);
        }

        /// <summary>
        /// ��ǰ��¼���û�д���ļ�
        /// </summary>
        private void InsDefaultUset()
        {
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
            string configFile = Path.Combine(appPath, "DefaultUser.txt");
            if (File.Exists(configFile))
            {
                string strUserInfo = txtName.Text.Trim() + "\r\n" + txtPwd.Text.Trim();

                using (StreamWriter sw = new StreamWriter(configFile))
                {
                    sw.Write(strUserInfo);
                    sw.Close();
                    sw.Dispose();
                }


            }
        }

        /// <summary>
        /// [�˳�]��ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// ��ʼ����ʾ
        /// </summary>
        private void initDisp()
        {
            // ��Ϣ��ʾ
            this.Text = GVars.App.Title;

            lblAppName.Text = GVars.App.Title;                  // Ӧ�ó��������
            lblCopyRight.Text = GVars.App.CopyRight;              // ��Ȩ
            lblVersion.Text = GVars.App.Version;                // �汾
        }


        /// <summary>
        /// ��¼ϵͳ
        /// </summary>
        private void loginSys()
        {
            Cursor.Current = Cursors.WaitCursor;

            // ����û���/����
            if (com.ChkUserPwd(txtName.Text, txtPwd.Text) == false)
            {
                GVars.Msg.ErrorSrc = this.txtName;

                GVars.Msg.Show();
                return;
            }

            // ��ȡ�û���Ϣ
            GVars.User.Name = txtName.Text.ToUpper().Trim();

            if (GVars.User.Name.Equals("ADMIN") == false
                && com.GetUserInfo(txtName.Text, txtPwd.Text) == false)
            {
                GVars.Msg.ErrorSrc = this.txtName;
                GVars.Msg.Show();
            }
            else
            {
                this.Close();                                           // �˳���¼����

                GVars.App.Verified = true;                              // ��¼��¼�ɹ�
            }
        }
    }
}