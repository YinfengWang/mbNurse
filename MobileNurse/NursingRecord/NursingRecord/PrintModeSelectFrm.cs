using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class PrintModeSelectFrm : Form
    {
        #region 变量
        public bool PrintSame       = true;                 // 是否原样重打
        public int  PageStart       = 0;
        public int  PageEnd         = 0;
        public int  FirstPage       = 0;
        public DateTime FirstDate   = DateTime.MinValue;
        public bool RePrintAll      = false;                // 全部重新打印
        #endregion
        
        
        public PrintModeSelectFrm()
        {
            InitializeComponent();
        }
        
        
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintModeSelectFrm_Load(object sender, EventArgs e)
        {
            try
            {
                printMode_CheckedChanged(sender, e);
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        private void printMode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                numPageStart.Enabled = rdoSamePrint.Checked;
                numPageEnd.Enabled   = rdoSamePrint.Checked;
                
                rdoStartPage.Enabled = rdoRePrint.Checked;
                rdoStartDate.Enabled = rdoRePrint.Checked;
                rdoReprintAll.Enabled= rdoRePrint.Checked;
                
                numFirstPage.Enabled = rdoRePrint.Checked && rdoStartPage.Checked;
                
                dtFirstDate.Enabled  = rdoRePrint.Checked && rdoStartDate.Checked;
                dtFirstTime.Enabled  = rdoRePrint.Checked && rdoStartDate.Checked;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[取消]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
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
                // 输入检查
                if (numPageStart.Enabled == true)
                {
                    if (numPageStart.Value > numPageEnd.Value)
                    {
                        numPageStart.Focus();
                        return;
                    }
                }
                
                // 获取输入
                PrintSame = rdoSamePrint.Checked;
                PageStart = (int)(numPageStart.Value);
                PageEnd   = (int)(numPageEnd.Value);
                FirstPage = (rdoStartPage.Checked? (int)(numFirstPage.Value): -1);
                FirstDate = dtFirstDate.Value.Date.AddHours(dtFirstTime.Value.Hour).AddMinutes(dtFirstTime.Value.Minute).AddSeconds(dtFirstTime.Value.Second);
                
                RePrintAll = rdoReprintAll.Enabled && rdoReprintAll.Checked;
                
                this.DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }               
    }
}
