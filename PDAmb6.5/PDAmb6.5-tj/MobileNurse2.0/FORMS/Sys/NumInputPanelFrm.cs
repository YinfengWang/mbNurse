using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HISPlus
{
    public partial class NumInputPanelFrm : Form
    {
        #region ����
        private string _result          = string.Empty;
        #endregion
        
        
        #region ����
        /// <summary>
        /// ������ַ���
        /// </summary>
        public string TextInput
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
            }
        }
        
        
        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        public string TextTips
        {
            get
            {
                return lblTips.Text;
            }
            set
            {
                lblTips.Text = value;
            }
        }
        #endregion
        
        
        public NumInputPanelFrm()
        {
            InitializeComponent();
        }


        #region �����¼�
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>                    
        private void SettingFrm_Load(object sender, EventArgs e)
        {
            try
            {
                txtInput.Text = _result;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        
        /// <summary>
        /// �˵� [����]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCancel_Click(object sender, EventArgs e)
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
        /// ��ť[����]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (btn == null) return;
                
                txtInput.Text += btn.Text;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// ��ť[ɾ��]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtInput.Text.Length == 0)
                {
                    return;
                }
                
                txtInput.Text = txtInput.Text.Remove(txtInput.Text.Length - 1, 1);
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// �˵� [ˢ��]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuOk_Click(object sender, EventArgs e)
        {
            try
            {
                _result = txtInput.Text.Trim();
                
                this.DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }        
        #endregion
    }
}