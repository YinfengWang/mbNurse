using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HISPlus.COMAPP.Function;

namespace HISPlus
{
    public partial class ErrInfo : Form
    {
        public ErrInfo()
        {
            InitializeComponent();
        }
        
        private void ErrInfo_Load( object sender, EventArgs e )
        {
            Cursor.Current = Cursors.Default;
        }
        
        
        /// <summary>
        /// ���ô�����Ϣ
        /// </summary>
        /// <param name="strErrMsg">������Ϣ</param>
        public void SetErrMsg(string strErrMsg)
        {
            txtErrMsg.Text = strErrMsg;
            SystemLog.OutputExceptionLog(strErrMsg);
        }

        private void btnOK_Click( object sender, EventArgs e )
        {
            this.Close();
        }
    }
}