namespace HISPlus
{
    partial class LabTestFrm
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
            this.mnuNavigator = new System.Windows.Forms.MenuItem();
            this.mnuNavigator_Orders = new System.Windows.Forms.MenuItem();
            this.mnuNavigator_OrdersExecute = new System.Windows.Forms.MenuItem();
            this.mnuNavigator_EvalDay = new System.Windows.Forms.MenuItem();
            this.mnuNavigator_EvalInp = new System.Windows.Forms.MenuItem();
            this.mnuNavigator_Nurse = new System.Windows.Forms.MenuItem();
            this.mnuNavigator_Speciment = new System.Windows.Forms.MenuItem();
            this.mnuNavigator_Exam = new System.Windows.Forms.MenuItem();
            this.mnuNavigator_Lab = new System.Windows.Forms.MenuItem();
            this.mnuNavigator_ShiftWork = new System.Windows.Forms.MenuItem();
            this.mnuNavigator_HealthEdu = new System.Windows.Forms.MenuItem();
            this.mnuNavigator_Operator = new System.Windows.Forms.MenuItem();
            this.lvwLab = new System.Windows.Forms.ListView();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.lvwResult = new System.Windows.Forms.ListView();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.timer1 = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuCancel);
            this.mainMenu1.MenuItems.Add(this.mnuPatient);
            this.mainMenu1.MenuItems.Add(this.mnuNavigator);
            // 
            // mnuCancel
            // 
            this.mnuCancel.Text = "返回";
            this.mnuCancel.Click += new System.EventHandler(this.mnuCancel_Click);
            // 
            // mnuPatient
            // 
            this.mnuPatient.Text = "当前病人";
            this.mnuPatient.Click += new System.EventHandler(this.mnuPatient_Click);
            // 
            // mnuNavigator
            // 
            this.mnuNavigator.MenuItems.Add(this.mnuNavigator_Orders);
            this.mnuNavigator.MenuItems.Add(this.mnuNavigator_OrdersExecute);
            this.mnuNavigator.MenuItems.Add(this.mnuNavigator_EvalDay);
            this.mnuNavigator.MenuItems.Add(this.mnuNavigator_EvalInp);
            this.mnuNavigator.MenuItems.Add(this.mnuNavigator_Nurse);
            this.mnuNavigator.MenuItems.Add(this.mnuNavigator_Speciment);
            this.mnuNavigator.MenuItems.Add(this.mnuNavigator_Exam);
            this.mnuNavigator.MenuItems.Add(this.mnuNavigator_Lab);
            this.mnuNavigator.MenuItems.Add(this.mnuNavigator_ShiftWork);
            this.mnuNavigator.MenuItems.Add(this.mnuNavigator_HealthEdu);
            this.mnuNavigator.MenuItems.Add(this.mnuNavigator_Operator);
            this.mnuNavigator.Text = "功能导航";
            // 
            // mnuNavigator_Orders
            // 
            this.mnuNavigator_Orders.Text = "医嘱";
            // 
            // mnuNavigator_OrdersExecute
            // 
            this.mnuNavigator_OrdersExecute.Text = "执行单";
            // 
            // mnuNavigator_EvalDay
            // 
            this.mnuNavigator_EvalDay.Text = "每日评估";
            // 
            // mnuNavigator_EvalInp
            // 
            this.mnuNavigator_EvalInp.Text = "入院评估";
            // 
            // mnuNavigator_Nurse
            // 
            this.mnuNavigator_Nurse.Text = "护理";
            // 
            // mnuNavigator_Speciment
            // 
            this.mnuNavigator_Speciment.Text = "标本管理";
            // 
            // mnuNavigator_Exam
            // 
            this.mnuNavigator_Exam.Text = "检查";
            // 
            // mnuNavigator_Lab
            // 
            this.mnuNavigator_Lab.Text = "检验";
            // 
            // mnuNavigator_ShiftWork
            // 
            this.mnuNavigator_ShiftWork.Text = "交接班";
            // 
            // mnuNavigator_HealthEdu
            // 
            this.mnuNavigator_HealthEdu.Text = "健康教育";
            // 
            // mnuNavigator_Operator
            // 
            this.mnuNavigator_Operator.Text = "手术";
            // 
            // lvwLab
            // 
            this.lvwLab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lvwLab.Columns.Add(this.columnHeader5);
            this.lvwLab.Columns.Add(this.columnHeader9);
            this.lvwLab.Columns.Add(this.columnHeader10);
            this.lvwLab.FullRowSelect = true;
            this.lvwLab.Location = new System.Drawing.Point(1, 1);
            this.lvwLab.Name = "lvwLab";
            this.lvwLab.Size = new System.Drawing.Size(238, 103);
            this.lvwLab.TabIndex = 31;
            this.lvwLab.View = System.Windows.Forms.View.Details;
            this.lvwLab.SelectedIndexChanged += new System.EventHandler(this.lvwLab_SelectedIndexChanged);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "标本";
            this.columnHeader5.Width = 70;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "临床诊断";
            this.columnHeader9.Width = 100;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "日期";
            this.columnHeader10.Width = 160;
            // 
            // lvwResult
            // 
            this.lvwResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lvwResult.Columns.Add(this.columnHeader6);
            this.lvwResult.Columns.Add(this.columnHeader7);
            this.lvwResult.Columns.Add(this.columnHeader1);
            this.lvwResult.FullRowSelect = true;
            this.lvwResult.Location = new System.Drawing.Point(1, 105);
            this.lvwResult.Name = "lvwResult";
            this.lvwResult.Size = new System.Drawing.Size(238, 162);
            this.lvwResult.TabIndex = 32;
            this.lvwResult.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "项目名称";
            this.columnHeader6.Width = 120;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "结果";
            this.columnHeader7.Width = 60;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "标准值";
            this.columnHeader1.Width = 160;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // LabTestFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.lvwResult);
            this.Controls.Add(this.lvwLab);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "LabTestFrm";
            this.Text = "检验结果";
            this.Load += new System.EventHandler(this.LabTestFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuCancel;
        private System.Windows.Forms.ListView lvwLab;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ListView lvwResult;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuItem mnuPatient;
        private System.Windows.Forms.MenuItem mnuNavigator;
        private System.Windows.Forms.MenuItem mnuNavigator_Orders;
        private System.Windows.Forms.MenuItem mnuNavigator_OrdersExecute;
        private System.Windows.Forms.MenuItem mnuNavigator_EvalDay;
        private System.Windows.Forms.MenuItem mnuNavigator_EvalInp;
        private System.Windows.Forms.MenuItem mnuNavigator_Nurse;
        private System.Windows.Forms.MenuItem mnuNavigator_Speciment;
        private System.Windows.Forms.MenuItem mnuNavigator_Exam;
        private System.Windows.Forms.MenuItem mnuNavigator_Lab;
        private System.Windows.Forms.MenuItem mnuNavigator_ShiftWork;
        private System.Windows.Forms.MenuItem mnuNavigator_HealthEdu;
        private System.Windows.Forms.MenuItem mnuNavigator_Operator;
    }
}