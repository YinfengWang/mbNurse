namespace HISPlus
{
    partial class OrderRemindPatientListFrm
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.mnuOK = new System.Windows.Forms.MenuItem();
            this.mnuCancel = new System.Windows.Forms.MenuItem();
            this.lvwPatientList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
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
            this.lvwPatientList.Columns.Add(this.columnHeader1);
            this.lvwPatientList.Columns.Add(this.columnHeader2);
            this.lvwPatientList.Columns.Add(this.columnHeader3);
            this.lvwPatientList.Columns.Add(this.columnHeader4);
            this.lvwPatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwPatientList.FullRowSelect = true;
            this.lvwPatientList.Location = new System.Drawing.Point(0, 0);
            this.lvwPatientList.Name = "lvwPatientList";
            this.lvwPatientList.Size = new System.Drawing.Size(240, 268);
            this.lvwPatientList.TabIndex = 0;
            this.lvwPatientList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "床标";
            this.columnHeader1.Width = 54;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "姓名";
            this.columnHeader2.Width = 70;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "性别";
            this.columnHeader3.Width = 45;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "年龄";
            this.columnHeader4.Width = 55;
            // 
            // OrderRemindPatientListFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lvwPatientList);
            this.Menu = this.mainMenu1;
            this.Name = "OrderRemindPatientListFrm";
            this.Text = "医嘱提醒患者列表";
            this.Load += new System.EventHandler(this.OrderRemindPatientListFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwPatientList;
        private System.Windows.Forms.MenuItem mnuOK;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.MenuItem mnuCancel;
    }
}