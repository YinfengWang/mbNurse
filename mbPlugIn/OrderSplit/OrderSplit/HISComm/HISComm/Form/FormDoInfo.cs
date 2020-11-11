using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class FormDoInfo : Form
    {
        /// <summary>
        /// 窗体信息
        /// </summary>
        public FormDoInfo()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormDoInfo_Load(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        public void ShowFormInfo(string id, string version, string guid, string right, string userRight)
        {
            string content = "窗 体 ID : " + id + ComConst.STR.CRLF
                           + "版 本 号 : " + version + ComConst.STR.CRLF
                           + "GUID     : " + guid + ComConst.STR.CRLF
                           + "用户权限 : " + userRight + ComConst.STR.CRLF
                           + "权    限 : " + right;

            this.txtContent.Text = content;
        }


        private void btnOk_Click(object sender, EventArgs e)
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
    }
}
