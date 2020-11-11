namespace HISPlus
{
    partial class PatientListFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.mnuOK = new System.Windows.Forms.MenuItem();
            this.mnuCancel = new System.Windows.Forms.MenuItem();
            this.lvwPatientList = new System.Windows.Forms.ListView();
            this.chBedLabel = new System.Windows.Forms.ColumnHeader();
            this.chPatientName = new System.Windows.Forms.ColumnHeader();
            this.chGender = new System.Windows.Forms.ColumnHeader();
            this.chAge = new System.Windows.Forms.ColumnHeader();
            this.chPatientID = new System.Windows.Forms.ColumnHeader();
            this.chVisitID = new System.Windows.Forms.ColumnHeader();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.timer1 = new System.Windows.Forms.Timer();
            this.btnFilter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuOK);
            this.mainMenu1.MenuItems.Add(this.mnuCancel);
            // 
            // mnuOK
            // 
            this.mnuOK.Text = "确定";
            this.mnuOK.Click += new System.EventHandler(this.mnuOK_Click);
            // 
            // mnuCancel
            // 
            this.mnuCancel.Text = "取消";
            this.mnuCancel.Click += new System.EventHandler(this.mnuCancel_Click);
            // 
            // lvwPatientList
            // 
            this.lvwPatientList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lvwPatientList.Columns.Add(this.chBedLabel);
            this.lvwPatientList.Columns.Add(this.chPatientName);
            this.lvwPatientList.Columns.Add(this.chGender);
            this.lvwPatientList.Columns.Add(this.chAge);
            this.lvwPatientList.Columns.Add(this.chPatientID);
            this.lvwPatientList.Columns.Add(this.chVisitID);
            this.lvwPatientList.FullRowSelect = true;
            this.lvwPatientList.Location = new System.Drawing.Point(0, 33);
            this.lvwPatientList.Name = "lvwPatientList";
            this.lvwPatientList.Size = new System.Drawing.Size(240, 235);
            this.lvwPatientList.TabIndex = 8;
            this.lvwPatientList.View = System.Windows.Forms.View.Details;
            // 
            // chBedLabel
            // 
            this.chBedLabel.Text = "床标";
            this.chBedLabel.Width = 46;
            // 
            // chPatientName
            // 
            this.chPatientName.Text = "姓名";
            this.chPatientName.Width = 89;
            // 
            // chGender
            // 
            this.chGender.Text = "性别";
            this.chGender.Width = 37;
            // 
            // chAge
            // 
            this.chAge.Text = "年龄";
            this.chAge.Width = 55;
            // 
            // chPatientID
            // 
            this.chPatientID.Text = "";
            this.chPatientID.Width = 0;
            // 
            // chVisitID
            // 
            this.chVisitID.Text = "";
            this.chVisitID.Width = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtSearch.Location = new System.Drawing.Point(107, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(81, 26);
            this.txtSearch.TabIndex = 9;
            this.txtSearch.GotFocus += new System.EventHandler(this.txtSearch_GotFocus);
            this.txtSearch.LostFocus += new System.EventHandler(this.txtSearch_LostFocus);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(43, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 22);
            this.label1.Text = "床号/姓名";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(190, 5);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(48, 24);
            this.btnQuery.TabIndex = 11;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(1, 5);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(40, 24);
            this.btnFilter.TabIndex = 13;
            this.btnFilter.Text = "过滤";
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // PatientListFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lvwPatientList);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "PatientListFrm";
            this.Text = "病人列表";
            this.Load += new System.EventHandler(this.PatientListFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwPatientList;
        private System.Windows.Forms.ColumnHeader chBedLabel;
        private System.Windows.Forms.ColumnHeader chPatientName;
        private System.Windows.Forms.ColumnHeader chGender;
        private System.Windows.Forms.ColumnHeader chAge;
        private System.Windows.Forms.ColumnHeader chPatientID;
        private System.Windows.Forms.ColumnHeader chVisitID;
        private System.Windows.Forms.MenuItem mnuOK;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnQuery;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.MenuItem mnuCancel;
    }
}