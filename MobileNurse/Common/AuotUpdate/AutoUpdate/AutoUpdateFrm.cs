using AutoUpdate.AuotUpdate;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace AutoUpdate
{
    public class AutoUpdateFrm : Form
    {
        private bool _exit = false;
        private int _fileCount = 0;
        private int _fileIndex = 0;
        private Mutex _locker = new Mutex();
        private bool blnFinished = false;
        private Button btnStart;
        private IContainer components = null;
        private string errMsg = string.Empty;
        private IniFile iniFile = new IniFile();
        private Label lblResult;
        private string logInfo = string.Empty;
        private ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private string updFileName = "UpdFileList.xml";
        private AutoUpdateWebSrv webSrv = new AutoUpdateWebSrv();

        public AutoUpdateFrm()
        {
            this.InitializeComponent();
            this.iniFile.FileName = Path.Combine(Application.StartupPath, this.iniFile.FileName);
            lblResult.Text = "";
        }

        private void AutoUpdate()
        {
            try
            {
                this.logInfo = "获取配置信息";

                //001
                string appCode = this.iniFile.ReadString("SETTING", "APP_CODE", "001");

                //获取程序运行的目录
                string startupPath = Application.StartupPath;

                //服务器IP
                string newIp = this.iniFile.ReadString("SETTING", "SERVER_IP", "");
                this.logInfo = "应用配置信息, 更改服务IP地址";

                //WebService地址
                this.webSrv.Url = ChangeIpInUrl(this.webSrv.Url, newIp);
                this.logInfo = "获取服务器端同步文件...";

                //记录本地的UpdFileList.xml文件的数据集
                DataSet dsLocalUpdFileList = new DataSet();

                //将服务器的UpdFileList.xml文件全部读取出来放到serverFileList中                
                DataSet dsServerUpdFileList = this.webSrv.GetServerFileList(appCode);

                //获取文件个数
                this.FileCount = dsServerUpdFileList.Tables[0].Rows.Count;
                this.logInfo = "获取服务器端同步文件完成!";
                this.FileIndex = 0;

                //本地UpdFileList.xml文件
                string path = Path.Combine(Application.StartupPath, this.updFileName);

                //
                if (!File.Exists(path))
                {
                    for (int i = 0; i < dsServerUpdFileList.Tables[0].Rows.Count; i++)
                    {
                        DataRow row = dsServerUpdFileList.Tables[0].Rows[i];
                        if (this.ExitThread)
                        {
                            return;
                        }
                        Thread.Sleep(10);
                        this.FileIndex++;
                        if (!this.downLoadFile(appCode, startupPath, row["FILE_NAME"].ToString()))
                        {
                            row.Delete();
                        }
                    }
                }
                else
                {
                    //将本地的UpdFileList.xml加载到DataSet中
                    dsLocalUpdFileList.ReadXml(path, XmlReadMode.ReadSchema);
                    if (!this.downLoadChanged(appCode, startupPath, dsServerUpdFileList, dsLocalUpdFileList))
                    {
                        return;
                    }
                }
                dsServerUpdFileList.AcceptChanges();
                dsServerUpdFileList.WriteXml(path, XmlWriteMode.WriteSchema);
                this.FileIndex = this.FileCount;
                string str5 = Path.Combine(Application.StartupPath, "UpdateFlag");
                if (File.Exists(str5))
                {
                    File.Delete(str5);
                }
                this.logInfo = "完成";
                this.blnFinished = true;
            }
            catch (Exception exception)
            {
                this.logInfo = exception.Message;
            }
        }

        private void AutoUpdateFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.ExitThread = true;
                string str = this.iniFile.ReadString("SETTING", "EXE_AFTER_AUTOUPDATE", "").Trim();
                if (str.Length != 0)
                {
                    str = Path.Combine(Application.StartupPath, str);
                    if (File.Exists(str))
                    {
                        Process.Start(str);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.btnStart.Enabled = false;
            this.timer1.Enabled = true;
            this.ExitThread = false;
            new Thread(new ThreadStart(this.AutoUpdate)).Start();
            webSrv.Dispose();
            
        }

        public static string ChangeIpInUrl(string url, string newIp)
        {
            int index = url.IndexOf("//");
            string str = url.Substring(0, index + 2);
            index = url.IndexOf("/", (int)(index + 2));
            string str2 = url.Substring(index + 1);
            return (str + newIp + "/" + str2);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 下载更新的文件
        /// </summary>
        /// <param name="appCode">应用程序编号</param>
        /// <param name="localPath">本地位置</param>
        /// <param name="dsSrc">服务器文件列表</param>
        /// <param name="dsDest">本地文件列表</param>
        /// <returns></returns>
        private bool downLoadChanged(string appCode, string localPath, DataSet dsServerUpdFileList, DataSet dsLocalUpdFileList)
        {
            DataRow[] rowArrayServer = dsServerUpdFileList.Tables[0].Select(string.Empty, "FILE_NAME");
            DataRow[] rowArrayLocal = dsLocalUpdFileList.Tables[0].Select(string.Empty, "FILE_NAME");
            int index = 0;
            int num2 = 0;
            int num3 = 0;
            while ((index < rowArrayServer.Length) && (num2 < rowArrayLocal.Length))
            {
                if (this.ExitThread)
                {
                    return false;
                }
                Thread.Sleep(10);
                this.FileIndex++;
                num3 = rowArrayServer[index]["FILE_NAME"].ToString().CompareTo(rowArrayLocal[num2]["FILE_NAME"].ToString());
                // 比较文件名
                if (num3 == 0)
                {
                    // 比较文件版本  服务器版本与本地版本不一致且
                    if (!rowArrayServer[index]["VERSION"].ToString().Equals(rowArrayLocal[num2]["VERSION"].ToString()) && 
                        !this.downLoadFile(appCode, localPath, rowArrayLocal[index]["FILE_NAME"].ToString()))
                    {
                        rowArrayServer[index].Delete();
                    }
                    num2++;
                    index++;
                }
                else if (num3 < 0)
                {
                    if (!this.downLoadFile(appCode, localPath, rowArrayServer[index]["FILE_NAME"].ToString()))
                    {
                        rowArrayServer[index].Delete();
                    }
                    index++;
                }
                else
                {
                    num2++;
                }
            }
            for (int i = index; i < rowArrayServer.Length; i++)
            {
                if (this.ExitThread)
                {
                    return false;
                }
                Thread.Sleep(10);
                this.FileIndex++;
                if (!this.downLoadFile(appCode, localPath, rowArrayServer[i]["FILE_NAME"].ToString()))
                {
                    rowArrayServer[index].Delete();
                }
            }
            return true;
        }

        /// <summary>
        /// 下载服务器文件. 本地文件已存在时,就删除. 如果是主程序.exe文件,则在下载时命名为new_主程序.exe,并在操作完成后启动DeleteOldFile.exe做下一步处理.
        /// </summary>
        /// <param name="appCode">应用程序编号</param>
        /// <param name="localPath">本地路径.即应用程序根目录</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        private bool downLoadFile(string appCode, string localPath, string fileName)
        {
            try
            {
                byte[] serverFile = this.webSrv.GetServerFile(appCode, fileName);
                if (serverFile.Length < 2)
                {
                    return false;
                }
                MemoryStream stream = new MemoryStream(serverFile);
                string path = Path.Combine(localPath, fileName);
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                }
                if (File.Exists(fileName))
                {
                    File.SetAttributes(fileName, FileAttributes.Normal);
                }

                // 如果不是主程序,就删除
                if (!fileName.ToUpper().Equals(Application.ProductName.ToUpper() + ".EXE"))
                {
                    fileName = Path.Combine(localPath, fileName);
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                }
                else
                {
                    fileName = "new_" + fileName;
                    fileName = Path.Combine(localPath, fileName);
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                }
                FileStream stream2 = new FileStream(fileName, FileMode.OpenOrCreate);
                stream.WriteTo(stream2);
                stream.Close();
                stream2.Close();
                stream = null;
                stream2 = null;
                return true;
            }
            catch (Exception exception)
            {
                this.errMsg = exception.Message;
                return false;
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnStart = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblResult = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(90, 30);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(108, 15);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(392, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(106, 49);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(59, 12);
            this.lblResult.TabIndex = 2;
            this.lblResult.Text = "lblResult";
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // AutoUpdateFrm
            // 
            this.ClientSize = new System.Drawing.Size(512, 70);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnStart);
            this.Name = "AutoUpdateFrm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "程序更新";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AutoUpdateFrm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Maximum = this.FileCount;
            this.progressBar1.Value = (this.FileIndex <= this.FileCount) ? this.FileIndex : this.FileCount;
            if (this.ExitThread)
            {
                this.timer1.Enabled = false;
                this.progressBar1.Value = 0;
                this.FileIndex = 0;
                this.FileCount = 0;
                this.btnStart.Enabled = true;
            }
            if (this.blnFinished)
            {
                this.timer1.Enabled = false;
                this.progressBar1.Value = 0;
                if (this.errMsg.Length > 0)
                {
                    MessageBox.Show(this.errMsg);
                }
                else
                {
                    MessageBox.Show("更新完成");
                }
                base.Close();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                this.lblResult.Text = this.logInfo;
            }
            catch
            {
            }
        }

        public bool ExitThread
        {
            get
            {
                bool flag;
                try
                {
                    this._locker.WaitOne();
                    flag = this._exit;
                }
                finally
                {
                    this._locker.ReleaseMutex();
                }
                return flag;
            }
            set
            {
                try
                {
                    this._locker.WaitOne();
                    this._exit = value;
                }
                finally
                {
                    this._locker.ReleaseMutex();
                }
            }
        }

        public int FileCount
        {
            get
            {
                int num;
                try
                {
                    this._locker.WaitOne();
                    num = this._fileCount;
                }
                finally
                {
                    this._locker.ReleaseMutex();
                }
                return num;
            }
            set
            {
                try
                {
                    this._locker.WaitOne();
                    this._fileCount = value;
                }
                finally
                {
                    this._locker.ReleaseMutex();
                }
            }
        }

        public int FileIndex
        {
            get
            {
                int num;
                try
                {
                    this._locker.WaitOne();
                    num = this._fileIndex;
                }
                finally
                {
                    this._locker.ReleaseMutex();
                }
                return num;
            }
            set
            {
                try
                {
                    this._locker.WaitOne();
                    this._fileIndex = value;
                }
                finally
                {
                    this._locker.ReleaseMutex();
                }
            }
        }
    }
}

