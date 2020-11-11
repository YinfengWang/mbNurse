namespace GenerateFileList
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class SyncFileInDirFrm : Form
    {
        private Button btnAnalyze;
        private Button btnExit;
        private Button btnStart;
        private IContainer components = null;
        private DataGridView dgvFile;
        private DataSet dsFile = null;
        private DataGridViewTextBoxColumn FILE_NAME;
        private FolderBrowserDialog folderBrowserDialog1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox grpFilter;
        private Label label1;
        private Label label2;
        private DataGridViewTextBoxColumn LAST_MODIFY_DATE;
        private DataGridViewTextBoxColumn SERIAL_NO;
        private TextBox txtFileType;
        private TextBox txtFolder;

        public SyncFileInDirFrm()
        {
            this.InitializeComponent();
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            if (this.txtFileType.Text.Trim().Length == 0)
            {
                this.txtFileType.Focus();
            }
            else if (this.txtFolder.Text.Trim().Length == 0)
            {
                this.txtFolder.Focus();
            }
            else
            {
                if (this.dsFile == null)
                {
                    this.dsFile = this.createDataSet();
                }
                else
                {
                    this.dsFile.Clear();
                }
                try
                {
                    this.readFileInfo(this.txtFolder.Text, this.txtFolder.Text, this.txtFileType.Text.Trim());
                    this.treateResult();
                    this.dgvFile.DataSource = this.dsFile.Tables[0].DefaultView;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private DataSet createDataSet()
        {
            DataSet set = new DataSet();
            set.Tables.Add("FILE_INFO");
            DataTable table = set.Tables[0];
            table.Columns.Add("FILE_NAME", typeof(string));
            table.Columns.Add("LAST_MODIFY_DATE", typeof(DateTime));
            table.Columns.Add("FILE_NAME_FULL", typeof(string));
            return set;
        }

        private void dgvFile_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgvFile.Rows[e.RowIndex].Cells["SERIAL_NO"].Value = (e.RowIndex + 1).ToString();
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFileType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvFile = new System.Windows.Forms.DataGridView();
            this.SERIAL_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FILE_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LAST_MODIFY_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.grpFilter.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFile)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpFilter
            // 
            this.grpFilter.Controls.Add(this.txtFolder);
            this.grpFilter.Controls.Add(this.label2);
            this.grpFilter.Controls.Add(this.txtFileType);
            this.grpFilter.Controls.Add(this.label1);
            this.grpFilter.Location = new System.Drawing.Point(12, 12);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new System.Drawing.Size(660, 86);
            this.grpFilter.TabIndex = 0;
            this.grpFilter.TabStop = false;
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(65, 43);
            this.txtFolder.Multiline = true;
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.ReadOnly = true;
            this.txtFolder.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFolder.Size = new System.Drawing.Size(589, 37);
            this.txtFolder.TabIndex = 3;
            this.txtFolder.DoubleClick += new System.EventHandler(this.txtFolder_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "文 件 夹";
            // 
            // txtFileType
            // 
            this.txtFileType.Location = new System.Drawing.Point(65, 14);
            this.txtFileType.Name = "txtFileType";
            this.txtFileType.Size = new System.Drawing.Size(85, 25);
            this.txtFileType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "文件类型";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvFile);
            this.groupBox1.Location = new System.Drawing.Point(12, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(660, 224);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文件列表";
            // 
            // dgvFile
            // 
            this.dgvFile.AllowUserToAddRows = false;
            this.dgvFile.AllowUserToDeleteRows = false;
            this.dgvFile.AllowUserToOrderColumns = true;
            this.dgvFile.AllowUserToResizeColumns = false;
            this.dgvFile.AllowUserToResizeRows = false;
            this.dgvFile.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFile.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SERIAL_NO,
            this.FILE_NAME,
            this.LAST_MODIFY_DATE});
            this.dgvFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFile.Location = new System.Drawing.Point(3, 21);
            this.dgvFile.Name = "dgvFile";
            this.dgvFile.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFile.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFile.RowTemplate.Height = 23;
            this.dgvFile.Size = new System.Drawing.Size(654, 200);
            this.dgvFile.TabIndex = 0;
            this.dgvFile.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvFile_RowPostPaint);
            // 
            // SERIAL_NO
            // 
            this.SERIAL_NO.HeaderText = "序号";
            this.SERIAL_NO.Name = "SERIAL_NO";
            this.SERIAL_NO.ReadOnly = true;
            this.SERIAL_NO.Width = 40;
            // 
            // FILE_NAME
            // 
            this.FILE_NAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FILE_NAME.DataPropertyName = "FILE_NAME_FULL";
            this.FILE_NAME.HeaderText = "文件名";
            this.FILE_NAME.Name = "FILE_NAME";
            this.FILE_NAME.ReadOnly = true;
            // 
            // LAST_MODIFY_DATE
            // 
            this.LAST_MODIFY_DATE.DataPropertyName = "LAST_MODIFY_DATE";
            dataGridViewCellStyle2.Format = "G";
            dataGridViewCellStyle2.NullValue = null;
            this.LAST_MODIFY_DATE.DefaultCellStyle = dataGridViewCellStyle2;
            this.LAST_MODIFY_DATE.HeaderText = "更新时间";
            this.LAST_MODIFY_DATE.Name = "LAST_MODIFY_DATE";
            this.LAST_MODIFY_DATE.ReadOnly = true;
            this.LAST_MODIFY_DATE.Width = 160;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAnalyze);
            this.groupBox2.Controls.Add(this.btnExit);
            this.groupBox2.Controls.Add(this.btnStart);
            this.groupBox2.Location = new System.Drawing.Point(12, 334);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(660, 62);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(8, 20);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(90, 30);
            this.btnAnalyze.TabIndex = 3;
            this.btnAnalyze.Text = "分析(&A)";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // btnExit
            // 
            this.btnExit.Enabled = false;
            this.btnExit.Location = new System.Drawing.Point(564, 20);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 30);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "退出(&E)";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(468, 20);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(90, 30);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "处理(&S)";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // SyncFileInDirFrm
            // 
            this.ClientSize = new System.Drawing.Size(684, 408);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpFilter);
            this.Name = "SyncFileInDirFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件处理";
            this.Load += new System.EventHandler(this.SyncFileInDirFrm_Load);
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFile)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private bool readFileInfo(string rootPath, string path, string fileType)
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
                if (((info2.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden) && info2.FullName.EndsWith(fileType))
                {
                    DataRow row = this.dsFile.Tables[0].NewRow();
                    row["FILE_NAME"] = info2.Name;
                    row["FILE_NAME_FULL"] = info2.FullName.Substring(path.Length + 1);
                    row["LAST_MODIFY_DATE"] = info2.LastWriteTime;
                    this.dsFile.Tables[0].Rows.Add(row);
                }
            }
            DirectoryInfo[] directories = info.GetDirectories();
            for (num = 0; num < directories.Length; num++)
            {
                this.readFileInfo(rootPath, directories[num].FullName, fileType);
            }
            return true;
        }

        private void SyncFileInDirFrm_Load(object sender, EventArgs e)
        {
            this.dgvFile.AutoGenerateColumns = false;
        }

        private void treateResult()
        {
            int num4;
            DataRow[] rowArray = this.dsFile.Tables[0].Select(string.Empty, "LAST_MODIFY_DATE DESC");
            string str = string.Empty;
            DateTime minValue = DateTime.MinValue;
            DateTime time2 = DateTime.MinValue;
            int index = -1;
            int num2 = -1;
            for (int i = 0; i < rowArray.Length; i++)
            {
                DataRow row = rowArray[i];
                if (!row["FILE_NAME"].ToString().Equals(str))
                {
                    if (index > -1)
                    {
                        if (index == num2)
                        {
                            rowArray[index].Delete();
                        }
                        else if (time2.CompareTo(minValue) != 0)
                        {
                            num4 = index + 1;
                            while (num4 <= num2)
                            {
                                rowArray[num4].Delete();
                                num4++;
                            }
                        }
                    }
                    time2 = (DateTime) row["LAST_MODIFY_DATE"];
                    minValue = (DateTime) row["LAST_MODIFY_DATE"];
                    index = i;
                    num2 = i;
                }
                else
                {
                    minValue = (DateTime) row["LAST_MODIFY_DATE"];
                    num2 = i;
                }
            }
            if (index > -1)
            {
                if (index == num2)
                {
                    rowArray[index].Delete();
                }
                else if (time2.CompareTo(minValue) != 0)
                {
                    for (num4 = index + 1; num4 <= num2; num4++)
                    {
                        rowArray[num4].Delete();
                    }
                }
            }
        }

        private void txtFolder_DoubleClick(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtFolder.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }
    }
}

