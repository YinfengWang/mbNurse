//------------------------------------------------------------------------------------
//
//  ϵͳ����        : �����ƶ�ҽ��
//  ��ϵͳ����      : ������վ(PDA)
//  ��������        : 
//  ����            : ChangePwd.cs
//  ���ܸ�Ҫ        : �޸�����
//  ������          : ����
//  ������          : 2007-07-24
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
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangePwd_Load(object sender, EventArgs e)
        {
            try
            {
                initDisp();                                             // ��ʼ����ʾ
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// ��ʼ����ʾ
        /// </summary>
        private void initDisp()
        {
            lblAppName.Text     = GVars.App.Title;                  // Ӧ�ó��������
            lblCopyRight.Text   = GVars.App.CopyRight;              // ��Ȩ
            lblVersion.Text     = GVars.App.Version;                // �汾
        }
        
        
        /// <summary>
        /// [��¼]��ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangePwd_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                // ����������
                if (chkDisp() == false)
                {
                    Cursor.Current = Cursors.Default;
                    GVars.Msg.Show();
                    return;
                }
                
                // �޸�����
                if (changePwd(GVars.User.UserName, txtPwdOld.Text.Trim(), txtPwd.Text.Trim()) == false)
                {
                    Cursor.Current = Cursors.Default;
                    GVars.Msg.Show();
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    GVars.Msg.MsgId = "I00001";                             // �����޸ĳɹ�!
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
        /// [�˳�]��ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
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
        /// ����������
        /// </summary>
        /// <returns>TRUE: ͨ��; FALSE: ����</returns>
        private bool chkDisp()
        {
            // ��������������
            if (txtPwd.Text.Trim().Length == 0)
            {
                GVars.Msg.ErrorSrc = txtPwd;
                GVars.Msg.MsgId = "E00057";                             // ������{0}!
                GVars.Msg.MsgContent.Add(lblNewPwd.Text);
                return false;
            }
            
            // ����������������ȷ
            if (txtPwd.Text.Trim().Equals(txtPwd2.Text.Trim()) == false)
            {
                GVars.Msg.ErrorSrc = txtPwd;
                GVars.Msg.MsgId = "E00008";                             // ������������벻ͬ! ���������롣
                return false;
            }
            
            // �����������ĸ��ͷ
            //string first = txtPwd.Text.Trim().Substring(0, 1);
            //if (DataType.IsNumber(first) == true)
            //{
            //    GVars.Msg.ErrorSrc = txtPwd;
            //    GVars.Msg.MsgId = "E00058";                             // �����������ĸ��ͷ!
            //    return false;
            //}
            
            return true;
        }
        
        
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <param name="userName">�û���</param>
        /// <param name="oldPwd">������</param>
        /// <param name="newPwd">������</param>
        private bool changePwd(string userName, string oldPwd, string newPwd)
        {
            // ����û���/�����Ƿ���ȷ
            if (com.ChkUserPwd(userName, oldPwd) == false)
            {
                GVars.Msg.ErrorSrc = this.txtPwdOld;
                return false;
            }
            
            // �޸�����
            if (com.ChangeUserPwd(userName, newPwd) == false)
            {
                GVars.Msg.ErrorSrc = this.txtPwd;                
                return false;
            }
            
            return true;
        }        
    }
}