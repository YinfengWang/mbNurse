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
    public partial class ContentShowFrm : Form
    {
        protected string _content;
        
        public ContentShowFrm()
        {
            InitializeComponent();
        }        
        
        public string Content
        {
            get { return _content; }
            set { _content = value;}
        }
        
        
        /// <summary>
        /// 菜单[返回]
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
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentShowFrm_Load(object sender, EventArgs e)
        {
            try
            {
                this.txtContent.Text = _content;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
    }
}