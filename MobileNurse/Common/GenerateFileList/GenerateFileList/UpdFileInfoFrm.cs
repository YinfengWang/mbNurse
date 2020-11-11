using HISPlus;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GenerateFileList
{
    /// <summary>
    /// 更新上传文件的xml信息,即更新下载列表.
    /// </summary>
    public class UpdFileInfoFrm : Form
    {
        private Button btnFolder;
        private Button btnStart;
        private string COL_EXIST_FLAG = "EXIST_FLAG";
        private IContainer components = null;
        private string doFile = string.Empty;
        private DataSet dsUpdFile = null;
        private FolderBrowserDialog folderBrowserDialog1;
        private Label label1;
        private TextBox txtFolder;
        private string updFileName = "UpdFileList.xml";

        public UpdFileInfoFrm()
        {
            this.InitializeComponent();
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtFolder.Text = this.folderBrowserDialog1.SelectedPath;
            }
            this.btnStart.Enabled = this.btnFolder.Text.Length > 0;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                this.updFileListInfo();
                MessageBox.Show("更新完成");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + this.doFile);
            }
        }

        private DataSet createUpdFileStruct()
        {
            DataSet set = new DataSet();
            set.Tables.Add("UpdFileList");
            set.Tables[0].Columns.Add("FILE_NAME", typeof(string));
            set.Tables[0].Columns.Add("VERSION", typeof(decimal));
            set.Tables[0].Columns.Add("MODIFY_DATE", typeof(DateTime));
            return set;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(68, 62);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(90, 30);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "文件夹";
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(68, 6);
            this.txtFolder.Multiline = true;
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.ReadOnly = true;
            this.txtFolder.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFolder.Size = new System.Drawing.Size(393, 50);
            this.txtFolder.TabIndex = 2;
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(467, 6);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(31, 50);
            this.btnFolder.TabIndex = 3;
            this.btnFolder.Text = "...";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // UpdFileInfoFrm
            // 
            this.ClientSize = new System.Drawing.Size(510, 132);
            this.Controls.Add(this.btnFolder);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.Name = "UpdFileInfoFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更新同步文件";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void updFileListInfo()
        {
            if (!Directory.Exists(this.txtFolder.Text))
            {
                MessageBox.Show("指定的文件夹不存在!");
            }
            else
            {
                string path = Path.Combine(this.txtFolder.Text, this.updFileName);
                if (!File.Exists(path))
                {
                    this.dsUpdFile = this.createUpdFileStruct();
                    this.dsUpdFile.WriteXml(path, XmlWriteMode.WriteSchema);
                }
                this.dsUpdFile = new DataSet();
                this.dsUpdFile.ReadXml(path, XmlReadMode.ReadSchema);
                this.dsUpdFile.Tables[0].Columns.Add(this.COL_EXIST_FLAG, typeof(string));
                this.updFileListInfo(this.txtFolder.Text, this.txtFolder.Text);
                DataRow[] rowArray = this.dsUpdFile.Tables[0].Select(this.COL_EXIST_FLAG + " IS NULL");
                for (int i = 0; i < rowArray.Length; i++)
                {
                    rowArray[i].Delete();
                }
                this.dsUpdFile.Tables[0].Columns.Remove(this.COL_EXIST_FLAG);
                this.dsUpdFile.WriteXml(path, XmlWriteMode.WriteSchema);
            }
        }

        private bool updFileListInfo(string rootPath, string path)
        {
            int num;
            DirectoryInfo info = new DirectoryInfo(path);
            string str = string.Empty;
            if (!rootPath.Equals(path))
            {
                str = path.Substring(rootPath.Length + 1, (path.Length - rootPath.Length) - 1);
            }
            FileInfo[] files = info.GetFiles();
            for (num = 0; num < files.Length; num++)
            {
                FileInfo info2 = files[num];
                if ((info2.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                {
                    string text = (str.Length > 0) ? Path.Combine(str, info2.Name) : info2.Name;
                    if (!text.Equals(this.updFileName))
                    {
                        this.doFile = text;
                        string filterExpression = "FILE_NAME = " + SqlManager.SqlConvert(text);
                        DataRow[] rowArray = this.dsUpdFile.Tables[0].Select(filterExpression);
                        DataRow row = null;
                        if (rowArray.Length > 0)
                        {
                            row = rowArray[0];
                        }
                        else
                        {
                            row = this.dsUpdFile.Tables[0].NewRow();
                            row["FILE_NAME"] = text;
                        }
                        if (row["MODIFY_DATE"] == DBNull.Value)
                        {
                            row["VERSION"] = 1;
                            row["MODIFY_DATE"] = info2.LastWriteTime;
                        }
                        else
                        {
                            DateTime time = (DateTime)row["MODIFY_DATE"];
                            if (!time.Equals(info2.LastWriteTime))
                            {
                                row["MODIFY_DATE"] = info2.LastWriteTime;
                                row["VERSION"] = (int)(((decimal)row["VERSION"]) + 1);
                            }
                        }
                        row[this.COL_EXIST_FLAG] = "1";
                        if (rowArray.Length == 0)
                        {
                            this.dsUpdFile.Tables[0].Rows.Add(row);
                        }
                    }
                }
            }
            DirectoryInfo[] directories = info.GetDirectories();
            for (num = 0; num < directories.Length; num++)
            {
                this.updFileListInfo(rootPath, directories[num].FullName);
            }
            return true;
        }
    }
}

