namespace HISPlus
{
    partial class OrdersExecuteFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrdersExecuteFrm));
            this.picOrderExecuteDetail = new System.Windows.Forms.PictureBox();
            this.lvwOrderExecuteBill = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
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
            this.btnSave = new System.Windows.Forms.Button();
            this.chkNotExecuted = new System.Windows.Forms.CheckBox();
            this.chkExecuted = new System.Windows.Forms.CheckBox();
            this.chkFinished = new System.Windows.Forms.CheckBox();
            this.btnFinish = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer();
            this.btnListPatient = new System.Windows.Forms.Button();
            this.btnNextPatient = new System.Windows.Forms.Button();
            this.btnCurrPatient = new System.Windows.Forms.Button();
            this.btnPrePatient = new System.Windows.Forms.Button();
            this.panelMsg = new System.Windows.Forms.Panel();
            this.btnMsgCancel = new System.Windows.Forms.Button();
            this.btnMsgOk = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer();
            this.timer3 = new System.Windows.Forms.Timer();
            this.exceComboBox = new System.Windows.Forms.ComboBox();
            this.timeButton = new System.Windows.Forms.Button();
            this.panelMsg.SuspendLayout();
            this.SuspendLayout();
            // 
            // picOrderExecuteDetail
            // 
            this.picOrderExecuteDetail.Image = ((System.Drawing.Image)(resources.GetObject("picOrderExecuteDetail.Image")));
            this.picOrderExecuteDetail.Location = new System.Drawing.Point(0, 222);
            this.picOrderExecuteDetail.Name = "picOrderExecuteDetail";
            this.picOrderExecuteDetail.Size = new System.Drawing.Size(22, 20);
            this.picOrderExecuteDetail.Click += new System.EventHandler(this.picOrderExecuteDetail_Click);
            // 
            // lvwOrderExecuteBill
            // 
            this.lvwOrderExecuteBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lvwOrderExecuteBill.Columns.Add(this.columnHeader1);
            this.lvwOrderExecuteBill.Columns.Add(this.columnHeader2);
            this.lvwOrderExecuteBill.Columns.Add(this.columnHeader3);
            this.lvwOrderExecuteBill.Columns.Add(this.columnHeader4);
            this.lvwOrderExecuteBill.Columns.Add(this.columnHeader7);
            this.lvwOrderExecuteBill.Columns.Add(this.columnHeader8);
            this.lvwOrderExecuteBill.FullRowSelect = true;
            this.lvwOrderExecuteBill.Location = new System.Drawing.Point(0, 25);
            this.lvwOrderExecuteBill.Name = "lvwOrderExecuteBill";
            this.lvwOrderExecuteBill.Size = new System.Drawing.Size(240, 192);
            this.lvwOrderExecuteBill.TabIndex = 11;
            this.lvwOrderExecuteBill.View = System.Windows.Forms.View.Details;
            this.lvwOrderExecuteBill.SelectedIndexChanged += new System.EventHandler(this.lvwOrderExecuteBill_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 20;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "医嘱内容";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "剂量";
            this.columnHeader3.Width = 60;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "执行时间";
            this.columnHeader4.Width = 60;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "";
            this.columnHeader7.Width = 0;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "";
            this.columnHeader8.Width = 0;
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
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(88, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(41, 22);
            this.btnSave.TabIndex = 30;
            this.btnSave.Text = "执行";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkNotExecuted
            // 
            this.chkNotExecuted.Checked = true;
            this.chkNotExecuted.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNotExecuted.Location = new System.Drawing.Point(28, 222);
            this.chkNotExecuted.Name = "chkNotExecuted";
            this.chkNotExecuted.Size = new System.Drawing.Size(44, 20);
            this.chkNotExecuted.TabIndex = 34;
            this.chkNotExecuted.Text = "未";
            this.chkNotExecuted.CheckStateChanged += new System.EventHandler(this.showOrdersExecute);
            // 
            // chkExecuted
            // 
            this.chkExecuted.Location = new System.Drawing.Point(100, 223);
            this.chkExecuted.Name = "chkExecuted";
            this.chkExecuted.Size = new System.Drawing.Size(44, 20);
            this.chkExecuted.TabIndex = 35;
            this.chkExecuted.Text = "已";
            this.chkExecuted.CheckStateChanged += new System.EventHandler(this.showOrdersExecute);
            // 
            // chkFinished
            // 
            this.chkFinished.Location = new System.Drawing.Point(172, 223);
            this.chkFinished.Name = "chkFinished";
            this.chkFinished.Size = new System.Drawing.Size(55, 20);
            this.chkFinished.TabIndex = 36;
            this.chkFinished.Text = "完成";
            this.chkFinished.CheckStateChanged += new System.EventHandler(this.showOrdersExecute);
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(136, 2);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(41, 22);
            this.btnFinish.TabIndex = 37;
            this.btnFinish.Text = "完成";
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
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
            this.panelMsg.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panelMsg.Controls.Add(this.btnMsgCancel);
            this.panelMsg.Controls.Add(this.btnMsgOk);
            this.panelMsg.Controls.Add(this.lblMsg);
            this.panelMsg.Location = new System.Drawing.Point(3, 176);
            this.panelMsg.Name = "panelMsg";
            this.panelMsg.Size = new System.Drawing.Size(234, 89);
            this.panelMsg.Visible = false;
            this.panelMsg.Click += new System.EventHandler(this.panelMsg_Click);
            // 
            // btnMsgCancel
            // 
            this.btnMsgCancel.Location = new System.Drawing.Point(129, 69);
            this.btnMsgCancel.Name = "btnMsgCancel";
            this.btnMsgCancel.Size = new System.Drawing.Size(104, 20);
            this.btnMsgCancel.TabIndex = 2;
            this.btnMsgCancel.Text = "取消";
            this.btnMsgCancel.Click += new System.EventHandler(this.btnMsgCancel_Click);
            // 
            // btnMsgOk
            // 
            this.btnMsgOk.Location = new System.Drawing.Point(0, 68);
            this.btnMsgOk.Name = "btnMsgOk";
            this.btnMsgOk.Size = new System.Drawing.Size(112, 20);
            this.btnMsgOk.TabIndex = 1;
            this.btnMsgOk.Text = "确定";
            this.btnMsgOk.Click += new System.EventHandler(this.btnMsgOk_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblMsg.ForeColor = System.Drawing.Color.Blue;
            this.lblMsg.Location = new System.Drawing.Point(3, 5);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(227, 81);
            this.lblMsg.Text = "过敏药物:    青霉素";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // timer2
            // 
            this.timer2.Interval = 12000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Interval = 5000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // exceComboBox
            // 
            this.exceComboBox.Items.Add("服药单");
            this.exceComboBox.Items.Add("注射单");
            this.exceComboBox.Items.Add("输液单");
            this.exceComboBox.Items.Add("治疗单");
            this.exceComboBox.Items.Add("护理单");
            this.exceComboBox.Items.Add("其他");
            this.exceComboBox.Location = new System.Drawing.Point(0, 2);
            this.exceComboBox.Name = "exceComboBox";
            this.exceComboBox.Size = new System.Drawing.Size(82, 22);
            this.exceComboBox.TabIndex = 44;
            this.exceComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // timeButton
            // 
            this.timeButton.Location = new System.Drawing.Point(183, 2);
            this.timeButton.Name = "timeButton";
            this.timeButton.Size = new System.Drawing.Size(57, 22);
            this.timeButton.TabIndex = 47;
            this.timeButton.Text = "时间段";
            this.timeButton.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // OrdersExecuteFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.timeButton);
            this.Controls.Add(this.exceComboBox);
            this.Controls.Add(this.panelMsg);
            this.Controls.Add(this.btnListPatient);
            this.Controls.Add(this.btnNextPatient);
            this.Controls.Add(this.btnCurrPatient);
            this.Controls.Add(this.btnPrePatient);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.chkFinished);
            this.Controls.Add(this.chkExecuted);
            this.Controls.Add(this.chkNotExecuted);
            this.Controls.Add(this.lvwOrderExecuteBill);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.picOrderExecuteDetail);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "OrdersExecuteFrm";
            this.Text = "执行单";
            this.Load += new System.EventHandler(this.OrdersExecuteFrm_Load);
            this.Closed += new System.EventHandler(this.OrdersExecuteFrm_Closed);
            this.panelMsg.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picOrderExecuteDetail;
        private System.Windows.Forms.ListView lvwOrderExecuteBill;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem mnuCancel;
        private System.Windows.Forms.MenuItem mnuPatient;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkNotExecuted;
        private System.Windows.Forms.CheckBox chkExecuted;
        private System.Windows.Forms.CheckBox chkFinished;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnListPatient;
        private System.Windows.Forms.Button btnNextPatient;
        private System.Windows.Forms.Button btnCurrPatient;
        private System.Windows.Forms.Button btnPrePatient;
        private System.Windows.Forms.Panel panelMsg;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnMsgCancel;
        private System.Windows.Forms.Button btnMsgOk;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
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
        private System.Windows.Forms.ComboBox exceComboBox;
        private System.Windows.Forms.Button timeButton;
    }
}