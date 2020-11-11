namespace HISPlus
{
    partial class SpecimentFrm
    {
        /// <summary>
        /// 必需的设计器变量。

        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.lvwSpeciment = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
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
            this.timer1 = new System.Windows.Forms.Timer();
            this.btnListPatient = new System.Windows.Forms.Button();
            this.btnNextPatient = new System.Windows.Forms.Button();
            this.btnCurrPatient = new System.Windows.Forms.Button();
            this.btnPrePatient = new System.Windows.Forms.Button();
            this.panelMsg = new System.Windows.Forms.Panel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.chkWait = new System.Windows.Forms.CheckBox();
            this.chkGathered = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer();
            this.panelMsg.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwSpeciment
            // 
            this.lvwSpeciment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lvwSpeciment.Columns.Add(this.columnHeader2);
            this.lvwSpeciment.Columns.Add(this.columnHeader3);
            this.lvwSpeciment.Columns.Add(this.columnHeader4);
            this.lvwSpeciment.Columns.Add(this.columnHeader7);
            this.lvwSpeciment.FullRowSelect = true;
            this.lvwSpeciment.Location = new System.Drawing.Point(0, 3);
            this.lvwSpeciment.Name = "lvwSpeciment";
            this.lvwSpeciment.Size = new System.Drawing.Size(240, 219);
            this.lvwSpeciment.TabIndex = 11;
            this.lvwSpeciment.View = System.Windows.Forms.View.Details;
            this.lvwSpeciment.SelectedIndexChanged += new System.EventHandler(this.lvwSpeciment_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "标本";
            this.columnHeader2.Width = 60;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "检查号";
            this.columnHeader3.Width = 128;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "采集人";
            this.columnHeader4.Width = 50;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "采集时间";
            this.columnHeader7.Width = 120;
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
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnListPatient
            // 
            this.btnListPatient.Location = new System.Drawing.Point(1, 248);
            this.btnListPatient.Name = "btnListPatient";
            this.btnListPatient.Size = new System.Drawing.Size(45, 20);
            this.btnListPatient.TabIndex = 42;
            this.btnListPatient.Text = "列表";
            // 
            // btnNextPatient
            // 
            this.btnNextPatient.Location = new System.Drawing.Point(176, 248);
            this.btnNextPatient.Name = "btnNextPatient";
            this.btnNextPatient.Size = new System.Drawing.Size(64, 20);
            this.btnNextPatient.TabIndex = 41;
            // 
            // btnCurrPatient
            // 
            this.btnCurrPatient.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCurrPatient.ForeColor = System.Drawing.Color.Blue;
            this.btnCurrPatient.Location = new System.Drawing.Point(110, 248);
            this.btnCurrPatient.Name = "btnCurrPatient";
            this.btnCurrPatient.Size = new System.Drawing.Size(67, 20);
            this.btnCurrPatient.TabIndex = 40;
            // 
            // btnPrePatient
            // 
            this.btnPrePatient.Location = new System.Drawing.Point(45, 248);
            this.btnPrePatient.Name = "btnPrePatient";
            this.btnPrePatient.Size = new System.Drawing.Size(66, 20);
            this.btnPrePatient.TabIndex = 39;
            // 
            // panelMsg
            // 
            this.panelMsg.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panelMsg.Controls.Add(this.lblMsg);
            this.panelMsg.Location = new System.Drawing.Point(1, 159);
            this.panelMsg.Name = "panelMsg";
            this.panelMsg.Size = new System.Drawing.Size(239, 89);
            this.panelMsg.Visible = false;
            this.panelMsg.Click += new System.EventHandler(this.panelMsg_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.Color.Red;
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular);
            this.lblMsg.ForeColor = System.Drawing.Color.White;
            this.lblMsg.Location = new System.Drawing.Point(10, 12);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(220, 68);
            this.lblMsg.Text = "病人与标本容器不匹配";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chkWait
            // 
            this.chkWait.Checked = true;
            this.chkWait.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWait.Location = new System.Drawing.Point(1, 225);
            this.chkWait.Name = "chkWait";
            this.chkWait.Size = new System.Drawing.Size(55, 20);
            this.chkWait.TabIndex = 43;
            this.chkWait.Text = "未采";
            this.chkWait.CheckStateChanged += new System.EventHandler(this.showSpecimentList);
            // 
            // chkGathered
            // 
            this.chkGathered.Location = new System.Drawing.Point(57, 226);
            this.chkGathered.Name = "chkGathered";
            this.chkGathered.Size = new System.Drawing.Size(58, 20);
            this.chkGathered.TabIndex = 44;
            this.chkGathered.Text = "已采";
            this.chkGathered.CheckStateChanged += new System.EventHandler(this.showSpecimentList);
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(175, 226);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 20);
            this.btnCancel.TabIndex = 45;
            this.btnCancel.Text = "撤销";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 5000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // SpecimentFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.panelMsg);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chkGathered);
            this.Controls.Add(this.chkWait);
            this.Controls.Add(this.btnListPatient);
            this.Controls.Add(this.btnNextPatient);
            this.Controls.Add(this.btnCurrPatient);
            this.Controls.Add(this.btnPrePatient);
            this.Controls.Add(this.lvwSpeciment);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "SpecimentFrm";
            this.Text = "标本管理";
            this.Load += new System.EventHandler(this.OrdersExecuteFrm_Load);
            this.Closed += new System.EventHandler(this.SpecimentFrm_Closed);
            this.panelMsg.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwSpeciment;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem mnuCancel;
        private System.Windows.Forms.MenuItem mnuPatient;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnListPatient;
        private System.Windows.Forms.Button btnNextPatient;
        private System.Windows.Forms.Button btnCurrPatient;
        private System.Windows.Forms.Button btnPrePatient;
        private System.Windows.Forms.Panel panelMsg;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.CheckBox chkWait;
        private System.Windows.Forms.CheckBox chkGathered;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Timer timer2;
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