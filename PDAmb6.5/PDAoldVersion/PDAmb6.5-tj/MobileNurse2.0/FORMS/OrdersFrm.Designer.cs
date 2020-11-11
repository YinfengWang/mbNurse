namespace HISPlus
{
    partial class OrdersFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrdersFrm));
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
            this.btnListPatient = new System.Windows.Forms.Button();
            this.btnNextPatient = new System.Windows.Forms.Button();
            this.btnCurrPatient = new System.Windows.Forms.Button();
            this.btnPrePatient = new System.Windows.Forms.Button();
            this.lvwOrders = new System.Windows.Forms.ListView();
            this.chOrderGrp = new System.Windows.Forms.ColumnHeader();
            this.chOrderText = new System.Windows.Forms.ColumnHeader();
            this.chDosage = new System.Windows.Forms.ColumnHeader();
            this.chPerformSchedule = new System.Windows.Forms.ColumnHeader();
            this.chOrderDate = new System.Windows.Forms.ColumnHeader();
            this.chOrderDoctor = new System.Windows.Forms.ColumnHeader();
            this.chOrderNo = new System.Windows.Forms.ColumnHeader();
            this.chOrderSubNo = new System.Windows.Forms.ColumnHeader();
            this.picOrderDetail = new System.Windows.Forms.PictureBox();
            this.cmbOrderTerm = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbOrderClass = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer();
            this.panelMsg = new System.Windows.Forms.Panel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.panelMsg.SuspendLayout();
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
            this.mnuPatient.Enabled = false;
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
            // btnListPatient
            // 
            this.btnListPatient.Location = new System.Drawing.Point(0, 248);
            this.btnListPatient.Name = "btnListPatient";
            this.btnListPatient.Size = new System.Drawing.Size(45, 20);
            this.btnListPatient.TabIndex = 25;
            this.btnListPatient.Text = "列表";
            // 
            // btnNextPatient
            // 
            this.btnNextPatient.Location = new System.Drawing.Point(175, 248);
            this.btnNextPatient.Name = "btnNextPatient";
            this.btnNextPatient.Size = new System.Drawing.Size(65, 20);
            this.btnNextPatient.TabIndex = 24;
            // 
            // btnCurrPatient
            // 
            this.btnCurrPatient.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCurrPatient.ForeColor = System.Drawing.Color.Blue;
            this.btnCurrPatient.Location = new System.Drawing.Point(109, 248);
            this.btnCurrPatient.Name = "btnCurrPatient";
            this.btnCurrPatient.Size = new System.Drawing.Size(67, 20);
            this.btnCurrPatient.TabIndex = 23;
            // 
            // btnPrePatient
            // 
            this.btnPrePatient.Location = new System.Drawing.Point(44, 248);
            this.btnPrePatient.Name = "btnPrePatient";
            this.btnPrePatient.Size = new System.Drawing.Size(66, 20);
            this.btnPrePatient.TabIndex = 22;
            // 
            // lvwOrders
            // 
            this.lvwOrders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lvwOrders.Columns.Add(this.chOrderGrp);
            this.lvwOrders.Columns.Add(this.chOrderText);
            this.lvwOrders.Columns.Add(this.chDosage);
            this.lvwOrders.Columns.Add(this.chPerformSchedule);
            this.lvwOrders.Columns.Add(this.chOrderDate);
            this.lvwOrders.Columns.Add(this.chOrderDoctor);
            this.lvwOrders.Columns.Add(this.chOrderNo);
            this.lvwOrders.Columns.Add(this.chOrderSubNo);
            this.lvwOrders.FullRowSelect = true;
            this.lvwOrders.Location = new System.Drawing.Point(0, 0);
            this.lvwOrders.Name = "lvwOrders";
            this.lvwOrders.Size = new System.Drawing.Size(240, 221);
            this.lvwOrders.TabIndex = 26;
            this.lvwOrders.View = System.Windows.Forms.View.Details;
            // 
            // chOrderGrp
            // 
            this.chOrderGrp.Text = "";
            this.chOrderGrp.Width = 20;
            // 
            // chOrderText
            // 
            this.chOrderText.Text = "医嘱内容";
            this.chOrderText.Width = 120;
            // 
            // chDosage
            // 
            this.chDosage.Text = "剂量";
            this.chDosage.Width = 60;
            // 
            // chPerformSchedule
            // 
            this.chPerformSchedule.Text = "执行时间";
            this.chPerformSchedule.Width = 60;
            // 
            // chOrderDate
            // 
            this.chOrderDate.Text = "开始日期";
            this.chOrderDate.Width = 120;
            // 
            // chOrderDoctor
            // 
            this.chOrderDoctor.Text = "医生";
            this.chOrderDoctor.Width = 60;
            // 
            // chOrderNo
            // 
            this.chOrderNo.Text = "";
            this.chOrderNo.Width = 0;
            // 
            // chOrderSubNo
            // 
            this.chOrderSubNo.Text = "";
            this.chOrderSubNo.Width = 0;
            // 
            // picOrderDetail
            // 
            this.picOrderDetail.Image = ((System.Drawing.Image)(resources.GetObject("picOrderDetail.Image")));
            this.picOrderDetail.Location = new System.Drawing.Point(2, 225);
            this.picOrderDetail.Name = "picOrderDetail";
            this.picOrderDetail.Size = new System.Drawing.Size(22, 20);
            this.picOrderDetail.Click += new System.EventHandler(this.picOrderDetail_Click);
            // 
            // cmbOrderTerm
            // 
            this.cmbOrderTerm.Items.Add("");
            this.cmbOrderTerm.Items.Add("长期");
            this.cmbOrderTerm.Items.Add("临时");
            this.cmbOrderTerm.Location = new System.Drawing.Point(171, 224);
            this.cmbOrderTerm.Name = "cmbOrderTerm";
            this.cmbOrderTerm.Size = new System.Drawing.Size(68, 22);
            this.cmbOrderTerm.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(136, 227);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 18);
            this.label2.Text = "长/临";
            // 
            // cmbOrderClass
            // 
            this.cmbOrderClass.Location = new System.Drawing.Point(58, 224);
            this.cmbOrderClass.Name = "cmbOrderClass";
            this.cmbOrderClass.Size = new System.Drawing.Size(70, 22);
            this.cmbOrderClass.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(29, 227);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 20);
            this.label1.Text = "类别";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panelMsg
            // 
            this.panelMsg.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panelMsg.Controls.Add(this.lblMsg);
            this.panelMsg.Location = new System.Drawing.Point(25, 90);
            this.panelMsg.Name = "panelMsg";
            this.panelMsg.Size = new System.Drawing.Size(190, 89);
            this.panelMsg.Visible = false;
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular);
            this.lblMsg.ForeColor = System.Drawing.Color.Blue;
            this.lblMsg.Location = new System.Drawing.Point(3, 10);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(176, 68);
            this.lblMsg.Text = "过敏药物:    青霉素";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // OrdersFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.panelMsg);
            this.Controls.Add(this.picOrderDetail);
            this.Controls.Add(this.cmbOrderTerm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbOrderClass);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvwOrders);
            this.Controls.Add(this.btnListPatient);
            this.Controls.Add(this.btnNextPatient);
            this.Controls.Add(this.btnCurrPatient);
            this.Controls.Add(this.btnPrePatient);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "OrdersFrm";
            this.Text = "医嘱单";
            this.Load += new System.EventHandler(this.OrdersFrm_Load);
            this.Closed += new System.EventHandler(this.OrdersFrm_Closed);
            this.panelMsg.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnListPatient;
        private System.Windows.Forms.Button btnNextPatient;
        private System.Windows.Forms.Button btnCurrPatient;
        private System.Windows.Forms.Button btnPrePatient;
        private System.Windows.Forms.ListView lvwOrders;
        private System.Windows.Forms.ColumnHeader chOrderGrp;
        private System.Windows.Forms.ColumnHeader chOrderText;
        private System.Windows.Forms.ColumnHeader chDosage;
        private System.Windows.Forms.ColumnHeader chPerformSchedule;
        private System.Windows.Forms.ColumnHeader chOrderDate;
        private System.Windows.Forms.ColumnHeader chOrderDoctor;
        private System.Windows.Forms.ColumnHeader chOrderNo;
        private System.Windows.Forms.ColumnHeader chOrderSubNo;
        private System.Windows.Forms.PictureBox picOrderDetail;
        private System.Windows.Forms.ComboBox cmbOrderTerm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbOrderClass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuItem mnuCancel;
        private System.Windows.Forms.MenuItem mnuPatient;
        private System.Windows.Forms.Panel panelMsg;
        private System.Windows.Forms.Label lblMsg;
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