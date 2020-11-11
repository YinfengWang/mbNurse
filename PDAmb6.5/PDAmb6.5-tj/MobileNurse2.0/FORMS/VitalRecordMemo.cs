using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class VitalRecordMemo : Form
    {
        #region 变量
        protected string _memo      = string.Empty;
        protected bool   _editable  = false;
        #endregion
        
        public VitalRecordMemo()
        {
            InitializeComponent();
        }
        
        
        #region 属性
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }
        
        
        public bool Editable
        {
            get { return _editable; }
            set { _editable = value; }
        }
        #endregion
        
        
        private void VitalRecordMemo_Load(object sender, EventArgs e)
        {
            try
            {
                txtMemo.Text    = _memo;
                
                txtMemo.Enabled = _editable;
                mnuOk.Enabled   = _editable;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        private void mnuOk_Click(object sender, EventArgs e)
        {
            try
            {
                _memo = txtMemo.Text.Trim();
                DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        
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
    }
}