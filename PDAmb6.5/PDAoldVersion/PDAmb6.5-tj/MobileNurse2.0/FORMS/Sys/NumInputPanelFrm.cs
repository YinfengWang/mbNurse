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
        #region 变量
        private string _result          = string.Empty;
        #endregion
        
        
        #region 属性
        /// <summary>
        /// 输入的字符串
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
        /// 提示信息
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


        #region 窗体事件
        /// <summary>
        /// 窗体加载
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
        /// 菜单 [返回]
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
        /// 按钮[输入]
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
        /// 按钮[删除]
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
        /// 菜单 [刷新]
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