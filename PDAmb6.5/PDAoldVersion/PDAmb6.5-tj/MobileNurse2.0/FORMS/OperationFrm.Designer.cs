namespace HISPlus
{
    partial class OperationFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperationFrm));
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
            this.picOperationDetail = new System.Windows.Forms.PictureBox();
            this.rdOperationWait = new System.Windows.Forms.RadioButton();
            this.rdOperationDone = new System.Windows.Forms.RadioButton();
            this.lvwOperation = new System.Windows.Forms.ListView();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.btnListPatient = new System.Windows.Forms.Button();
            this.btnNextPatient = new System.Windows.Forms.Button();
            this.btnCurrPatient = new System.Windows.Forms.Button();
            this.btnPrePatient = new System.Windows.Forms.Button();
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
            // picOperationDetail
            // 
            this.picOperationDetail.Image = ((System.Drawing.Image)(resources.GetObject("picOperationDetail.Image")));
            this.picOperationDetail.Location = new System.Drawing.Point(215, 226);
            this.picOperationDetail.Name = "picOperationDetail";
            this.picOperationDetail.Size = new System.Drawing.Size(22, 20);
            this.picOperationDetail.Visible = false;
            // 
            // rdOperationWait
            // 
            this.rdOperationWait.Checked = true;
            this.rdOperationWait.Location = new System.Drawing.Point(79, 226);
            this.rdOperationWait.Name = "rdOperationWait";
            this.rdOperationWait.Size = new System.Drawing.Size(70, 20);
            this.rdOperationWait.TabIndex = 33;
            this.rdOperationWait.Text = "未完成";
            // 
            // rdOperationDone
            // 
            this.rdOperationDone.Location = new System.Drawing.Point(3, 226);
            this.rdOperationDone.Name = "rdOperationDone";
            this.rdOperationDone.Size = new System.Drawing.Size(70, 20);
            this.rdOperationDone.TabIndex = 32;
            this.rdOperationDone.Text = "已完成";
            // 
            // lvwOperation
            // 
            this.lvwOperation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lvwOperation.CheckBoxes = true;
            this.lvwOperation.Columns.Add(this.columnHeader11);
            this.lvwOperation.Columns.Add(this.columnHeader17);
            this.lvwOperation.Columns.Add(this.columnHeader12);
            this.lvwOperation.Columns.Add(this.columnHeader14);
            this.lvwOperation.FullRowSelect = true;
            this.lvwOperation.Location = new System.Drawing.Point(0, 0);
            this.lvwOperation.Name = "lvwOperation";
            this.lvwOperation.Size = new System.Drawing.Size(240, 220);
            this.lvwOperation.TabIndex = 31;
            this.lvwOperation.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "手术名称";
            this.columnHeader11.Width = 100;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "部位";
            this.columnHeader17.Width = 60;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "手术时间";
            this.columnHeader12.Width = 80;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "手术医生";
            this.columnHeader14.Width = 120;
            // 
            // btnListPatient
            // 
            this.btnListPatient.Location = new System.Drawing.Point(1, 248);
            this.btnListPatient.Name = "btnListPatient";
            this.btnListPatient.Size = new System.Drawing.Size(45, 20);
            this.btnListPatient.TabIndex = 37;
            this.btnListPatient.Text = "列表";
            // 
            // btnNextPatient
            // 
            this.btnNextPatient.Location = new System.Drawing.Point(176, 248);
            this.btnNextPatient.Name = "btnNextPatient";
            this.btnNextPatient.Size = new System.Drawing.Size(64, 20);
            this.btnNextPatient.TabIndex = 36;
            // 
            // btnCurrPatient
            // 
            this.btnCurrPatient.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCurrPatient.ForeColor = System.Drawing.Color.Blue;
            this.btnCurrPatient.Location = new System.Drawing.Point(110, 248);
            this.btnCurrPatient.Name = "btnCurrPatient";
            this.btnCurrPatient.Size = new System.Drawing.Size(67, 20);
            this.btnCurrPatient.TabIndex = 35;
            // 
            // btnPrePatient
            // 
            this.btnPrePatient.Location = new System.Drawing.Point(45, 248);
            this.btnPrePatient.Name = "btnPrePatient";
            this.btnPrePatient.Size = new System.Drawing.Size(66, 20);
            this.btnPrePatient.TabIndex = 34;
            // 
            // OperationFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.btnListPatient);
            this.Controls.Add(this.btnNextPatient);
            this.Controls.Add(this.btnCurrPatient);
            this.Controls.Add(this.btnPrePatient);
            this.Controls.Add(this.picOperationDetail);
            this.Controls.Add(this.rdOperationWait);
            this.Controls.Add(this.rdOperationDone);
            this.Controls.Add(this.lvwOperation);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "OperationFrm";
            this.Text = "手术核对";
            this.Load += new System.EventHandler(this.OperationFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuCancel;
        private System.Windows.Forms.MenuItem mnuPatient;
        private System.Windows.Forms.PictureBox picOperationDetail;
        private System.Windows.Forms.RadioButton rdOperationWait;
        private System.Windows.Forms.RadioButton rdOperationDone;
        private System.Windows.Forms.ListView lvwOperation;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.Button btnListPatient;
        private System.Windows.Forms.Button btnNextPatient;
        private System.Windows.Forms.Button btnCurrPatient;
        private System.Windows.Forms.Button btnPrePatient;
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