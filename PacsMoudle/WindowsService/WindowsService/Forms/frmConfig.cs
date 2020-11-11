using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PACSMonitor
{
    public partial class frmConfig : Form//BaseForm
    {
        public frmConfig()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }    

        private void dlgConfig_Load(object sender, EventArgs e)
        {
            //设置其闪烁样式
            //BlinkIfDifferentError 当图标已经显示并且为控件设置了新的错误字符串时闪烁。 
            //AlwaysBlink 总是闪烁。 
            //NeverBlink 错误图标从不闪烁。 
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            //错误图标的闪烁速率（以毫秒为单位）。默认为 250 毫秒
            errorProvider1.BlinkRate = 1000;

            txtHtmlPath.Text = Common.PacsSavePath;
            txtLogPath.Text = Common.LogPath;
           
            CheckPath(txtHtmlPath);
            CheckPath(txtLogPath);

            radioAuto.Checked = Common.LogRefresh.IsAuto;
            radioHand.Checked = Common.LogRefresh.IsHand;
            radioTimer.Checked = Common.LogRefresh.IsTimer;
            numericUpDown1.Value = Common.LogRefresh.TimerSecond;
        }

        /// <summary>
        /// 检查控件路径是否存在
        /// </summary>
        private void CheckPath(TextBox txtbox)
        {
            if (!Directory.Exists(txtbox.Text))
            {
                if (string.IsNullOrWhiteSpace(txtbox.Text))
                    txtbox.BackColor = Color.Red;
                else
                    txtbox.ForeColor = Color.Red;

                errorProvider1.SetError(txtbox, "无效路径!");
            }
            else
            {
                txtbox.BackColor = System.Drawing.SystemColors.Window;
                txtbox.ForeColor = System.Drawing.SystemColors.WindowText;
                errorProvider1.SetError(txtbox, string.Empty);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void browseHtmlPath_Click(object sender, EventArgs e)
        {
            using (System.Windows.Forms.FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    txtHtmlPath.Text = dlg.SelectedPath;
                    CheckPath(txtHtmlPath);
                }
            }
        }

        private void btnForeverOK_Click(object sender, EventArgs e)
        {
            if (CloseingCheck())
            {
                Common.updateSeeting("PacsSavePath", txtHtmlPath.Text);
                Common.updateSeeting("logPath", txtLogPath.Text);

                Common.PacsSavePath = txtHtmlPath.Text;
                Common.LogPath = txtLogPath.Text;

                Common.updateSeeting("IsAuto", radioAuto.Checked.ToString());
                Common.updateSeeting("IsHand", radioHand.Checked.ToString());
                Common.updateSeeting("IsTimer", radioTimer.Checked.ToString());
                Common.updateSeeting("TimerSecond", numericUpDown1.Value.ToString());

                Common.LogRefresh.IsAuto = radioAuto.Checked;
                Common.LogRefresh.IsHand = radioHand.Checked;
                Common.LogRefresh.IsTimer = radioTimer.Checked;
                Common.LogRefresh.TimerSecond = (int)numericUpDown1.Value;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (CloseingCheck())
            {
                Common.PacsSavePath = txtHtmlPath.Text;
                Common.LogPath = txtLogPath.Text;

                Common.LogRefresh.IsAuto = radioAuto.Checked;
                Common.LogRefresh.IsHand = radioHand.Checked;
                Common.LogRefresh.IsTimer = radioTimer.Checked;
                Common.LogRefresh.TimerSecond = (int)numericUpDown1.Value;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// 读取控件异常信息
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        private string ControlError(TextBox txt)
        {
            string msg = "无效路径: ";
            string errormsg = errorProvider1.GetError(txt);
            if (!string.IsNullOrEmpty(errormsg))
                return msg + txt.Text + Environment.NewLine;
            return string.Empty;
        }

        /// <summary>
        /// 关闭前数据校验
        /// </summary>
        private bool CloseingCheck()
        {         
            CheckPath(txtHtmlPath);
            CheckPath(txtLogPath);

            string errormsg = string.Empty;
            errormsg = ControlError(txtHtmlPath) + ControlError(txtLogPath);
            if (!string.IsNullOrEmpty(errormsg))
            {
                MessageBox.Show(errormsg);
                return false;
            }
            return true;
        }

        private void frmConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Directory.Exists(Common.PacsSavePath) ||
                !Directory.Exists(Common.LogPath))
            {
                e.Cancel = true;
                MessageBox.Show(
                    (Directory.Exists(Common.PacsSavePath) ? string.Empty : ("无效生成目录: " + Common.PacsSavePath +
                    Environment.NewLine + Environment.NewLine))
                    + (Directory.Exists(Common.LogPath) ? string.Empty : ("无效日志目录: " + Common.LogPath)),
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!CloseingCheck())
            {
                e.Cancel = true;
            }
        }

        private void txtMonitorPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = true;
            //return;
        }

        private void btnLogPath_Click(object sender, EventArgs e)
        {
            using (System.Windows.Forms.FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    txtLogPath.Text = dlg.SelectedPath;
                    CheckPath(txtLogPath);
                }
            }
        }

        private void radioAuto_CheckedChanged(object sender, EventArgs e)
        {
            lblLogRefresh.Text = "自动刷新：当监控目录发生变更时，自动刷新日志。";
        }

        private void radioHand_CheckedChanged(object sender, EventArgs e)
        {
            lblLogRefresh.Text = "手动刷新：点击刷新按钮才会加载最新日志。";
        }

        private void radioTimer_CheckedChanged(object sender, EventArgs e)
        {
            lblLogRefresh.Text = "定时刷新：按设定间隔时间刷新日志。";
        }

    }

    /// <summary>
    /// 日志刷新操作类
    /// </summary>
    public class LogRefresh
    {
        private bool _IsAuto;
        /// <summary>
        /// 自动刷新
        /// </summary>
        public bool IsAuto
        {
            get { return _IsAuto; }
            set { _IsAuto = value; }
        }

        private bool _IsHand;
        /// <summary>
        /// 手动刷新
        /// </summary>
        public bool IsHand
        {
            get { return _IsHand; }
            set { _IsHand = value; }
        }

        private bool _IsTimer;
        /// <summary>
        /// 定时刷新
        /// </summary>
        public bool IsTimer
        {
            get { return _IsTimer; }
            set { _IsTimer = value; }
        }

        private int _TimerSecond;
        /// <summary>
        /// 定时秒数，仅在设定为定时刷新时有效
        /// </summary>
        public int TimerSecond
        {
            get { return _TimerSecond; }
            set { _TimerSecond = value; }
        }
    }

}