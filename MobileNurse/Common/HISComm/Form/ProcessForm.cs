using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class ProcessForm : FormDo
    {
        /// <summary>
        /// 设置提示信息
        /// </summary>
        public string MessageInfo
        {
            set
            {
                lblMsg.Text = value;                
            }
        }

        /// <summary>
        /// 设置进度条显示值
        /// </summary>
        public int ProcessValue
        {
            set { this.progressBarControl1.EditValue = value; }
        }

        ///// <summary>
        ///// 设置进度条样式
        ///// </summary>
        //public ProgressBarStyle ProcessStyle
        //{
        //    set { this.progressBar1.Style = value; }
        //}

        public ProcessForm()
        {
            InitializeComponent();

            progressBarControl1.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            progressBarControl1.Properties.Minimum = 0;
            progressBarControl1.Properties.Maximum = 100;
            progressBarControl1.Properties.Step = 1;
            progressBarControl1.Properties.ShowTitle = true;
            progressBarControl1.Properties.NullText = @"正在执行中，请稍等。。";
        }

        private void progressBarControl1_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            //e.DisplayText = _msg + @"，已完成：" + e.Value;
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

        }
    }
}
