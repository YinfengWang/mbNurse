namespace HISPlus
{
    partial class OrderRemindFrm
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
            this.mnuCancel = new System.Windows.Forms.MenuItem();
            this.mnuPatient = new System.Windows.Forms.MenuItem();
            this.lvwOrderRemindList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.btnNextPatient = new System.Windows.Forms.Button();
            this.btnCurrPatient = new System.Windows.Forms.Button();
            this.btnPrePatient = new System.Windows.Forms.Button();
            this.btnListPatient = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuCancel);
            this.mainMenu1.MenuItems.Add(this.mnuPatient);
            // 
            // mnuCancel
            // 
            this.mnuCancel.Text = "返回";
            this.mnuCancel.Click += new System.EventHandler(this.mnuCancel_Click);
            // 
            // mnuPatient
            // 
            this.mnuPatient.Text = "当前病人";
            // 
            // lvwOrderRemindList
            // 
            this.lvwOrderRemindList.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lvwOrderRemindList.Columns.Add(this.columnHeader1);
            this.lvwOrderRemindList.Columns.Add(this.columnHeader2);
            this.lvwOrderRemindList.Columns.Add(this.columnHeader3);
            this.lvwOrderRemindList.Columns.Add(this.columnHeader4);
            this.lvwOrderRemindList.Columns.Add(this.columnHeader5);
            this.lvwOrderRemindList.Columns.Add(this.columnHeader6);
            this.lvwOrderRemindList.Columns.Add(this.columnHeader7);
            this.lvwOrderRemindList.FullRowSelect = true;
            this.lvwOrderRemindList.Location = new System.Drawing.Point(0, 0);
            this.lvwOrderRemindList.Name = "lvwOrderRemindList";
            this.lvwOrderRemindList.Size = new System.Drawing.Size(240, 248);
            this.lvwOrderRemindList.TabIndex = 4;
            this.lvwOrderRemindList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 25;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "医嘱内容";
            this.columnHeader2.Width = 125;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "剂量";
            this.columnHeader3.Width = 50;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "途径";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "开始时间";
            this.columnHeader5.Width = 110;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "结束时间";
            this.columnHeader6.Width = 110;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "医生";
            this.columnHeader7.Width = 50;
            // 
            // btnNextPatient
            // 
            this.btnNextPatient.Location = new System.Drawing.Point(175, 248);
            this.btnNextPatient.Name = "btnNextPatient";
            this.btnNextPatient.Size = new System.Drawing.Size(65, 20);
            this.btnNextPatient.TabIndex = 28;
            this.btnNextPatient.Click += new System.EventHandler(this.btnNextPatient_Click);
            // 
            // btnCurrPatient
            // 
            this.btnCurrPatient.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCurrPatient.ForeColor = System.Drawing.Color.Blue;
            this.btnCurrPatient.Location = new System.Drawing.Point(110, 248);
            this.btnCurrPatient.Name = "btnCurrPatient";
            this.btnCurrPatient.Size = new System.Drawing.Size(65, 20);
            this.btnCurrPatient.TabIndex = 27;
            // 
            // btnPrePatient
            // 
            this.btnPrePatient.Location = new System.Drawing.Point(45, 248);
            this.btnPrePatient.Name = "btnPrePatient";
            this.btnPrePatient.Size = new System.Drawing.Size(65, 20);
            this.btnPrePatient.TabIndex = 26;
            this.btnPrePatient.Click += new System.EventHandler(this.btnPrePatient_Click);
            // 
            // btnListPatient
            // 
            this.btnListPatient.Location = new System.Drawing.Point(0, 248);
            this.btnListPatient.Name = "btnListPatient";
            this.btnListPatient.Size = new System.Drawing.Size(45, 20);
            this.btnListPatient.TabIndex = 29;
            this.btnListPatient.Text = "列表";
            this.btnListPatient.Click += new System.EventHandler(this.btnListPatient_Click);
            // 
            // OrderRemindFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnListPatient);
            this.Controls.Add(this.btnNextPatient);
            this.Controls.Add(this.btnCurrPatient);
            this.Controls.Add(this.btnPrePatient);
            this.Controls.Add(this.lvwOrderRemindList);
            this.Menu = this.mainMenu1;
            this.Name = "OrderRemindFrm";
            this.Text = "医嘱提醒内容";
            this.Load += new System.EventHandler(this.OrderRemindfrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwOrderRemindList;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.MenuItem mnuCancel;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnNextPatient;
        private System.Windows.Forms.Button btnCurrPatient;
        private System.Windows.Forms.Button btnPrePatient;
        private System.Windows.Forms.MenuItem mnuPatient;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btnListPatient;

    }
}