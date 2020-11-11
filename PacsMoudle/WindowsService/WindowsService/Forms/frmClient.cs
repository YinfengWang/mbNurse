using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Threading;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using Dicom;
using Dicom.Imaging;

namespace PACSMonitor
{
    /// <summary>
    /// 文件系统监视服务客户端主窗体
    /// </summary>
    public partial class frmClient : BaseForm
    {
        /// <summary>
        /// PACS文件映射盘符下的目录
        /// </summary>
        private string PACSRootPath = Common.GetConfigValue("PACSRemotePath");

        /// <summary>
        /// 初始化对象
        /// </summary>
        public frmClient()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化对象. 由注册表注册后,开机自启动,所以直接最小化
        /// </summary>
        public frmClient(string args)
        {
            InitializeComponent();

            FormHide();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            if (!Directory.Exists(Common.PacsSavePath))
                Directory.CreateDirectory(Common.PacsSavePath);

            if (!Directory.Exists(Common.LogPath))
                Directory.CreateDirectory(Common.LogPath);
        }

        /// <summary>
        /// 窗体加载事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmViewLog_Load(object sender, EventArgs e)
        {            
            try
            {
                try
                {
                    string programName = Path.GetFileName(Application.ExecutablePath);
                    string registryPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
                    string value = this.GetType().Assembly.Location + " -s";
                    if ((Registry.LocalMachine.OpenSubKey(registryPath).GetValue(programName) as string) != value)
                    {
                        RegistryKey key = Registry.LocalMachine.CreateSubKey(registryPath);
                        key.SetValue(programName, value);
                        key.Close();
                    }
                }
                catch (Exception exp)
                {
                    if (exp is UnauthorizedAccessException)
                        MessageBox.Show("权限不足! " + Environment.NewLine
                            + exp.Message.ToString() + Environment.NewLine + "开机自启动设置失败!",
                            "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show(exp.Message.ToString(), "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // [线程间操作无效: 从不是创建控件“textBox1”的线程访问它]异常的解决
                // 把所有的控件合法性线程检查全部禁止。
                //System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;

                this.ShowIcon = true;
                this.ShowInTaskbar = true;

                notifyIcon1.Visible = true;
                notifyIcon1.Text = "移动医生站PACS辅助程序";

                //设置窗体图标
                notifyIcon1.Icon = CommonIcon;
                
                notifyIcon1.ContextMenuStrip = contextMenuStrip1;

                System.Windows.Forms.ToolStripMenuItem Monitor = new System.Windows.Forms.ToolStripMenuItem();
                System.Windows.Forms.ToolStripMenuItem Exit = new System.Windows.Forms.ToolStripMenuItem();
                // 
                // contextMenuStrip1
                // 
                contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                    Monitor,
                    new System.Windows.Forms.ToolStripSeparator(),
                    Exit});

                contextMenuStrip1.Name = "contextMenuStrip1";
                contextMenuStrip1.Size = new System.Drawing.Size(167, 48);
                // 
                // 实时监控
                //                 
                Monitor.Size = new System.Drawing.Size(166, 22);
                Monitor.Text = "实时监控";
                Monitor.Click += new EventHandler(Monitor_Click);
                // 
                // 退出
                //                 
                Exit.Size = new System.Drawing.Size(166, 22);
                Exit.Text = "退出";
                Exit.Click += new EventHandler(Exit_Click);

                richTextBox1.HideSelection = false;
            }
            catch (Exception ex)
            {
                myTimer.Enabled = false;
                MessageBox.Show(this, ex.Message, "系统错误");
            }
            this.btnRefresh_Click(null, null);

            //PACSInit();

            // 初始化数据
            InitData();

            // 定时刷新日志 
            myTimer.Interval = 1000 * 10;
            //myTimer.Start();

            Thread t = new Thread(new ThreadStart(() => new Dicom()));
            t.IsBackground = true;
            t.Start();

            //string dicomPath = @"F:\PacsFile\？主\2046195\1.2.840.113698.330601.19980101004154.0.1";

            //int Last1 = dicomPath.LastIndexOf(@"\");            

            //int Last2 = dicomPath.Substring(0, Last1).LastIndexOf(@"\");            

            //int Last3 = dicomPath.Substring(0, Last2).LastIndexOf(@"\");

            //MessageBox.Show(dicomPath.Substring(0, Last3));
            //MessageBox.Show(dicomPath.Substring(Last1 + 1));

            //string[] files = Directory.GetFileSystemEntries(dicomPath.Substring(0, Last3),
            //    dicomPath.Substring(Last1+1),SearchOption.AllDirectories);
            //MessageBox.Show(files.Length.ToString());
            //MessageBox.Show(files[0]);


            //DirectoryInfo d = new DirectoryInfo(@"F:\2046195");

            //foreach (FileInfo f in d.GetFiles())
            //{
            //    try
            //    {
            //        MessageBox.Show(f.FullName);

            //        DicomFile dicomFile = DicomFile.Open(f.FullName);
            //        DicomDataset dcmDataSet = dicomFile.Dataset;
            //        var dcmImage = new DicomImage(dcmDataSet);//可以增加第二个参数来指定获取第几帧
            //        using (Image image = dcmImage.RenderImage())
            //        {
            //            image.Save(f.FullName + "_123", System.Drawing.Imaging.ImageFormat.Png);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //        continue;
            //    }
            //}

            //F:\PacsFile\1.2.840.113698.330601.19980101004205.0.6
            //F:\PacsFile\1375507444\1375507446_0.DCM
            //DicomImage dicomImage = new DicomImage(@"F:\PacsFile\1.2.840.113698.330601.19980101004205.0.6");
            //DicomImage dicomImage = new DicomImage(@"F:\PacsFile\1375507444\1375507446_0.DCM");
            //using (Image image = dicomImage.RenderImage())
            //{
            //    image.Save(@"F:\PacsFile\123.png", System.Drawing.Imaging.ImageFormat.Png);
            //}
        }

        void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.ExitThread();
        }

        void Monitor_Click(object sender, EventArgs e)
        {
            FormShow();
        }

        /// <summary>
        /// 日志数据绑定
        /// </summary>
        public void Bind()
        {
            //return;
            try
            {
                new Thread(() =>
                {
                    BeginInvoke(new Action(() =>
                    {
                        if (File.Exists(Common.LogName))
                        {
                            //FileStream fs = new FileStream(Common.LogName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            //StreamReader sr = new StreamReader(fs, System.Text.Encoding.GetEncoding("gb2312"));

                            //richTextBox1.Text =
                            //   "生成目录: " + Common.PacsSavePath + Environment.NewLine +
                            //    sr.ReadToEnd();
                            //sr.Dispose();
                            //fs.Dispose();

                            richTextBox1.Text =
                                 "生成目录: " + Common.PacsSavePath + Environment.NewLine +
                                  File.ReadAllText(Common.LogName,
                                   System.Text.Encoding.GetEncoding("gb2312"));

                            richTextBox1.Select(
                                richTextBox1.Text.Length, this.richTextBox1.Text.Length);
                            //选择RichTextBox内容的最后一个字节。
                            this.richTextBox1.ScrollToCaret();
                            //将RichTextBox的滚动条移动到上一步所设定的位置。
                        }
                        else
                            richTextBox1.Text = string.Empty;

                    }));
                    Thread.Sleep(200);
                }).Start();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Bind方法异常!" + ex.Message);
            }
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Bind();
        }

        /// <summary>
        /// 显示模态窗口
        /// </summary>
        /// <param name="frm"></param>
        private DialogResult showDialog(Form frm)
        {
            using (frm)
            {
                return frm.ShowDialog(this);
            }
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                File.Copy(Common.LogName, Common.LogPath + DateTime.Now.ToString("yyyyMMdd") + "_PACS_" + DateTime.Now.ToString("hhmm") + ".log");
                File.Delete(Common.LogName);
                richTextBox1.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 系统配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfig_Click(object sender, EventArgs e)
        {
            new frmConfig().Show();

            DialogResult dr = showDialog(new frmConfig());
            //if (dr == DialogResult.OK)
            //{
            //    //RefreshSetting();
            //}
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                this,
                @"    天健源达-移动医生站PACS辅助程序。",
                "关于本程序");
        }

        public void Disply(string strInput, Color fontColor)
        {
            int p1 = richTextBox1.TextLength;  //取出未添加时的字符串长度。 
            richTextBox1.AppendText(strInput + "\n");  //保留每行的所有颜色。 //  rtb.Text += strInput + "\n";  //添加时，仅当前行有颜色。 
            int p2 = strInput.Length;  //取出要添加的文本的长度 
            richTextBox1.Select(p1, p2);        //选中要添加的文本 
            richTextBox1.SelectionColor = fontColor;  //设置要添加的文本的字体色 

            richTextBox1.Focus();
        }

        /// <summary>
        /// 根据文件路径读取文件内容。当文件不存在时，每等待0.1s后再尝试1次，共3次，若仍然失败则异常
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        private string ReadAgain(string path)
        {
            int againCount = 3;
        tryAgain:
            {
                try
                {
                    using (StreamReader sr = new StreamReader(path, Encoding.Default, false))
                    {
                        return sr.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    if (againCount > 0)
                    {
                        Thread.Sleep(100);
                        againCount--;
                        goto tryAgain;
                    }
                    else
                        throw new Exception(" StreamReader 打开失败! " + ex.Message); ;
                }
            }
        }

        /// <summary>
        /// 写监控日志.    (当设定自动更新时,刷新Log窗口)
        /// </summary>
        /// <param name="msg"></param>
        private void WriteLog(string msg)
        {
            Common.WriteLog(msg);

            msg = DateTime.Now.ToLocalTime() + Environment.NewLine + msg + Environment.NewLine;
            if (Common.LogRefresh.IsAuto)
            {
                new Thread(() =>
                {
                    BeginInvoke(new Action(() =>
                    {
                        if (msg.StartsWith("E"))
                            Disply(msg, Color.Red);
                        else
                            Disply(msg, Color.Blue);
                    }));
                }).Start();
            }
        }

        /// <summary>
        /// 关闭打开的子窗口(由 Esc 执行)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void tmp_btnClose_Click(object sender, EventArgs e)
        {
            FormHide();
        }

        private void frmClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            FormHide();
        }

        /// <summary>
        /// 打开菜单单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            FormHide();
        }

        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmClient_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                FormHide();
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
                FormShow();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
                FormShow();
        }

        void textBox1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = true;
            return;
        }

        /// <summary>
        /// 显示窗口
        /// </summary>
        private void FormShow()
        {
            // 显示窗口
            this.Visible = true;
            // 将窗口正常化
            this.WindowState = FormWindowState.Normal;
            // 在任务栏显示
            this.ShowInTaskbar = true;
            this.ShowIcon = true;
            // 激活窗口，使窗口获得焦点
            this.Activate();
        }

        /// <summary>
        /// 隐藏窗口
        /// </summary>
        private void FormHide()
        {
            // 将窗口最小化
            this.WindowState = FormWindowState.Minimized;
            // 隐藏窗口
            this.Visible = false;
            // 不在任务栏显示
            this.ShowInTaskbar = false;
            // 托盘显示
            notifyIcon1.Visible = true;
        }

        private void myTimer_Tick(object sender, EventArgs e)
        {
            Bind();
            try
            {
                //myTimer.Enabled = false;
                //new Dicom();
                //myTimer.Start();

                //Thread t = new Thread(new ThreadStart(() => new Dicom()));
                //t.IsBackground = true;
                //t.Start();
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }

        /// <summary>
        /// 自定义生成
        /// </summary>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            CustomGenerate c = new CustomGenerate();
            c.Show();
        }
    }

}