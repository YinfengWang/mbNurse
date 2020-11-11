using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
namespace HISPlus
{
    partial class XunShiFrm
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
            this.btnListPatient = new System.Windows.Forms.Button();
            this.btnNextPatient = new System.Windows.Forms.Button();
            this.btnCurrPatient = new System.Windows.Forms.Button();
            this.btnPrePatient = new System.Windows.Forms.Button();
            this.lvwOrders = new System.Windows.Forms.ListView();
            this.chOrderGrp = new System.Windows.Forms.ColumnHeader();
            this.chOrderText = new System.Windows.Forms.ColumnHeader();
            this.chPerformSchedule = new System.Windows.Forms.ColumnHeader();
            this.chContent = new System.Windows.Forms.ColumnHeader();
            this.timer1 = new System.Windows.Forms.Timer();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panelMsg = new System.Windows.Forms.Panel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnMsgCancel = new System.Windows.Forms.Button();
            this.btnMsgOk = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer();
            this.cmbMemoClass = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
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
            this.btnListPatient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnListPatient.Location = new System.Drawing.Point(0, 248);
            this.btnListPatient.Name = "btnListPatient";
            this.btnListPatient.Size = new System.Drawing.Size(45, 20);
            this.btnListPatient.TabIndex = 25;
            this.btnListPatient.Text = "列表";
            // 
            // btnNextPatient
            // 
            this.btnNextPatient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnNextPatient.Location = new System.Drawing.Point(175, 248);
            this.btnNextPatient.Name = "btnNextPatient";
            this.btnNextPatient.Size = new System.Drawing.Size(65, 20);
            this.btnNextPatient.TabIndex = 24;
            // 
            // btnCurrPatient
            // 
            this.btnCurrPatient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCurrPatient.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCurrPatient.ForeColor = System.Drawing.Color.Blue;
            this.btnCurrPatient.Location = new System.Drawing.Point(109, 248);
            this.btnCurrPatient.Name = "btnCurrPatient";
            this.btnCurrPatient.Size = new System.Drawing.Size(67, 20);
            this.btnCurrPatient.TabIndex = 23;
            // 
            // btnPrePatient
            // 
            this.btnPrePatient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
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
            this.lvwOrders.Columns.Add(this.chPerformSchedule);
            this.lvwOrders.Columns.Add(this.chContent);
            this.lvwOrders.FullRowSelect = true;
            this.lvwOrders.Location = new System.Drawing.Point(0, 0);
            this.lvwOrders.Name = "lvwOrders";
            this.lvwOrders.Size = new System.Drawing.Size(240, 221);
            this.lvwOrders.TabIndex = 26;
            this.lvwOrders.View = System.Windows.Forms.View.Details;
            // 
            // chOrderGrp
            // 
            this.chOrderGrp.Text = "序号";
            this.chOrderGrp.Width = 41;
            // 
            // chOrderText
            // 
            this.chOrderText.Text = "执行护士";
            this.chOrderText.Width = 67;
            // 
            // chPerformSchedule
            // 
            this.chPerformSchedule.Text = "执行时间";
            this.chPerformSchedule.Width = 80;
            // 
            // chContent
            // 
            this.chContent.Text = "备注";
            this.chContent.Width = 65;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnAdd.Location = new System.Drawing.Point(175, 222);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(65, 25);
            this.btnAdd.TabIndex = 46;
            this.btnAdd.Text = "新增";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panelMsg
            // 
            this.panelMsg.BackColor = System.Drawing.Color.White;
            this.panelMsg.Controls.Add(this.lblMsg);
            this.panelMsg.Controls.Add(this.btnMsgCancel);
            this.panelMsg.Controls.Add(this.btnMsgOk);
            this.panelMsg.Location = new System.Drawing.Point(3, 99);
            this.panelMsg.Name = "panelMsg";
            this.panelMsg.Size = new System.Drawing.Size(234, 100);
            this.panelMsg.Visible = false;
            this.panelMsg.Click += new System.EventHandler(this.panelMsg_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblMsg.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblMsg.Location = new System.Drawing.Point(4, 2);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(227, 77);
            this.lblMsg.Text = "信息提示";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnMsgCancel
            // 
            this.btnMsgCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMsgCancel.Location = new System.Drawing.Point(129, 79);
            this.btnMsgCancel.Name = "btnMsgCancel";
            this.btnMsgCancel.Size = new System.Drawing.Size(104, 20);
            this.btnMsgCancel.TabIndex = 2;
            this.btnMsgCancel.Text = "取消";
            this.btnMsgCancel.Click += new System.EventHandler(this.btnMsgCancel_Click);
            // 
            // btnMsgOk
            // 
            this.btnMsgOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMsgOk.Location = new System.Drawing.Point(0, 79);
            this.btnMsgOk.Name = "btnMsgOk";
            this.btnMsgOk.Size = new System.Drawing.Size(112, 20);
            this.btnMsgOk.TabIndex = 1;
            this.btnMsgOk.Text = "新增";
            this.btnMsgOk.Click += new System.EventHandler(this.btnMsgOk_Click);
            // 
            // timer2
            // 
            this.timer2.Interval = 12000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // cmbMemoClass
            // 
            this.cmbMemoClass.Location = new System.Drawing.Point(37, 222);
            this.cmbMemoClass.Name = "cmbMemoClass";
            this.cmbMemoClass.Size = new System.Drawing.Size(64, 22);
            this.cmbMemoClass.TabIndex = 48;
            this.cmbMemoClass.SelectedIndexChanged += new System.EventHandler(this.cmbMemoClass_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 222);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 20);
            this.label1.Text = "备注";
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(103, 222);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(70, 21);
            this.txtMemo.TabIndex = 50;
            this.txtMemo.Visible = false;
            // 
            // XunShiFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.panelMsg);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lvwOrders);
            this.Controls.Add(this.btnListPatient);
            this.Controls.Add(this.btnNextPatient);
            this.Controls.Add(this.btnCurrPatient);
            this.Controls.Add(this.btnPrePatient);
            this.Controls.Add(this.cmbMemoClass);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "XunShiFrm";
            this.Text = "护理巡视";
            this.Load += new System.EventHandler(this.XunShiFrm_Load);
            this.Closed += new System.EventHandler(this.XunShiFrm_Load);
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
        private System.Windows.Forms.ColumnHeader chPerformSchedule;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuItem mnuCancel;
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
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panelMsg;
        private System.Windows.Forms.Button btnMsgCancel;
        private System.Windows.Forms.Button btnMsgOk;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ComboBox cmbMemoClass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.ColumnHeader chContent;
    }
}