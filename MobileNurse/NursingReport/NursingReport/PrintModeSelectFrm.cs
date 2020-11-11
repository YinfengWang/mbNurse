using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class PrintModeSelectFrm : Form
    {
        #region 变量

        /// <summary>
        /// 是否为续打
        /// </summary>
        public bool PrintContinue = false;


        /// <summary>
        /// 是否为重打
        /// </summary>
        public bool Reprint = false;


        /// <summary>
        /// 是否为页码打印
        /// </summary>
        public bool PrintPageNo = false;

        /// <summary>
        /// 是否为首次打印
        /// </summary>
        public bool PrintFirst = false;
        public bool PrintDate = false;
        public string startDate = "";
        public string endDate = "";

        public int PageNo = 0;
        public int ReprintPageNo = 0;
        #endregion


        private DataSet dsPageInfo = null;
        public DataSet DsPageInfo
        {
            set { dsPageInfo = value; }
        }
        public PrintModeSelectFrm()
        {



            InitializeComponent();
            //txtDateStart.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //txtDateStart.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            //txtDateStart.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            //txtDateStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //txtDateStart.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;

            //txtEndDate.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            //txtEndDate.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            //txtEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //txtEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;

            txtDateStart.Enabled = false;
            txtEndDate.Enabled = false;
            //txtDateStart.Properties.DisplayFormat.FormatString = "G";
            //txtEndDate.Properties.DisplayFormat.FormatString = "G";
            //txtDateStart.Properties.EditFormat.FormatString="G";  
            //txtEndDate.Properties.EditFormat.FormatString="G";  
        }


        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintModeSelectFrm_Load(object sender, EventArgs e)
        {
            if (dsPageInfo.Tables[0].Rows.Count > 0)
            {
                //rdoFirstPrint.Enabled = true;

                rdoFirstPrint.Checked = true;
                //rdoContinuePrint.Checked = false;

                //txtDateStart.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //txtDateStart.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
                //txtDateStart.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
                //txtEndDate.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
                //txtEndDate.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;

                //txtDateStart.Properties.DisplayFormat.FormatString = "G";
                //txtEndDate.Properties.DisplayFormat.FormatString = "G";
            }
            else
            {
                //rdoFirstPrint.Enabled = true;
                //rdoFirstPrint.Checked = true;
                //rdoContinuePrint.Enabled = false;
                //rdoPageNoPrint.Enabled = false;
                //rdoReprint.Enabled = false;
            }
        }


        /// <summary>
        /// 按钮[取消]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        /// <summary>
        /// 按钮[确定]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                PageNo = (int)(numPageNo.Value);
                ReprintPageNo = (int)(numReprintPageNo.Value);
                //PrintPageNo = rdoPageNoPrint.Checked;
                PrintFirst = rdoFirstPrint.Checked;
                PrintDate = radDate.Checked;
                startDate = txtDateStart.Value.ToString();
                endDate = txtEndDate.Value.ToString();
                //PrintContinue = rdoContinuePrint.Checked;
                //Reprint = rdoReprint.Checked;

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void rdoPageNoPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPageNoPrint.Checked)
            {
                numPageNo.Enabled = true;
            }
            else
            {
                numPageNo.Enabled = false;
            }
        }

        private void rdoReprint_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoReprint.Checked)
            {
                numReprintPageNo.Enabled = true;
            }
            else
            {
                numReprintPageNo.Enabled = false;
            }
        }

        private void radDate_CheckedChanged(object sender, EventArgs e)
        {
            if (radDate.Checked)
            {
                txtDateStart.Enabled = true;
                txtEndDate.Enabled = true;
            }
            else
            {
                txtDateStart.Enabled = false;
                txtEndDate.Enabled = false;
            }
        }
    }
}
