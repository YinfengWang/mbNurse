namespace HISPlus
{
    partial class VitalRecorderFrm
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
            this.mnuCancel = new System.Windows.Forms.MenuItem();
            this.mnuPatient = new System.Windows.Forms.MenuItem();
            this.mnuMemo = new System.Windows.Forms.MenuItem();
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
            this.btnNextTimePoint = new System.Windows.Forms.Button();
            this.btnCurrTimePoint = new System.Windows.Forms.Button();
            this.btnPreTimePoint = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtItemValue0 = new System.Windows.Forms.TextBox();
            this.txtItemName0 = new System.Windows.Forms.TextBox();
            this.txtItemValue1 = new System.Windows.Forms.TextBox();
            this.txtItemName1 = new System.Windows.Forms.TextBox();
            this.txtItemValue2 = new System.Windows.Forms.TextBox();
            this.txtItemName2 = new System.Windows.Forms.TextBox();
            this.txtItemValue3 = new System.Windows.Forms.TextBox();
            this.txtItemName3 = new System.Windows.Forms.TextBox();
            this.txtItemValue4 = new System.Windows.Forms.TextBox();
            this.txtItemName4 = new System.Windows.Forms.TextBox();
            this.txtItemValue5 = new System.Windows.Forms.TextBox();
            this.txtItemName5 = new System.Windows.Forms.TextBox();
            this.txtItemValue6 = new System.Windows.Forms.TextBox();
            this.txtItemName6 = new System.Windows.Forms.TextBox();
            this.txtItemValue7 = new System.Windows.Forms.TextBox();
            this.txtItemName7 = new System.Windows.Forms.TextBox();
            this.txtItemValue8 = new System.Windows.Forms.TextBox();
            this.txtItemName8 = new System.Windows.Forms.TextBox();
            this.btnAddNurseEvent = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.vsBar = new System.Windows.Forms.VScrollBar();
            this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.comboxNurseEventType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuCancel);
            this.mainMenu1.MenuItems.Add(this.mnuPatient);
            this.mainMenu1.MenuItems.Add(this.mnuMemo);
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
            // mnuMemo
            // 
            this.mnuMemo.Text = "备注";
            this.mnuMemo.Click += new System.EventHandler(this.mnuMemo_Click);
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
            this.btnListPatient.Size = new System.Drawing.Size(46, 20);
            this.btnListPatient.TabIndex = 25;
            this.btnListPatient.Text = "列表";
            // 
            // btnNextPatient
            // 
            this.btnNextPatient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnNextPatient.Location = new System.Drawing.Point(176, 248);
            this.btnNextPatient.Name = "btnNextPatient";
            this.btnNextPatient.Size = new System.Drawing.Size(64, 20);
            this.btnNextPatient.TabIndex = 24;
            // 
            // btnCurrPatient
            // 
            this.btnCurrPatient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCurrPatient.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCurrPatient.ForeColor = System.Drawing.Color.Blue;
            this.btnCurrPatient.Location = new System.Drawing.Point(110, 248);
            this.btnCurrPatient.Name = "btnCurrPatient";
            this.btnCurrPatient.Size = new System.Drawing.Size(67, 20);
            this.btnCurrPatient.TabIndex = 23;
            // 
            // btnPrePatient
            // 
            this.btnPrePatient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnPrePatient.Location = new System.Drawing.Point(45, 248);
            this.btnPrePatient.Name = "btnPrePatient";
            this.btnPrePatient.Size = new System.Drawing.Size(66, 20);
            this.btnPrePatient.TabIndex = 22;
            // 
            // btnNextTimePoint
            // 
            this.btnNextTimePoint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnNextTimePoint.Location = new System.Drawing.Point(138, 221);
            this.btnNextTimePoint.Name = "btnNextTimePoint";
            this.btnNextTimePoint.Size = new System.Drawing.Size(39, 28);
            this.btnNextTimePoint.TabIndex = 29;
            this.btnNextTimePoint.Text = " >";
            this.btnNextTimePoint.Click += new System.EventHandler(this.btnNextTimePoint_Click);
            // 
            // btnCurrTimePoint
            // 
            this.btnCurrTimePoint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCurrTimePoint.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCurrTimePoint.ForeColor = System.Drawing.Color.Blue;
            this.btnCurrTimePoint.Location = new System.Drawing.Point(79, 221);
            this.btnCurrTimePoint.Name = "btnCurrTimePoint";
            this.btnCurrTimePoint.Size = new System.Drawing.Size(61, 28);
            this.btnCurrTimePoint.TabIndex = 28;
            this.btnCurrTimePoint.Text = "20:38";
            this.btnCurrTimePoint.Click += new System.EventHandler(this.btnCurrTimePoint_Click);
            // 
            // btnPreTimePoint
            // 
            this.btnPreTimePoint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnPreTimePoint.Location = new System.Drawing.Point(45, 221);
            this.btnPreTimePoint.Name = "btnPreTimePoint";
            this.btnPreTimePoint.Size = new System.Drawing.Size(35, 28);
            this.btnPreTimePoint.TabIndex = 27;
            this.btnPreTimePoint.Text = " <";
            this.btnPreTimePoint.Click += new System.EventHandler(this.btnPreTimePoint_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(176, 221);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(64, 28);
            this.btnSave.TabIndex = 30;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtItemValue0
            // 
            this.txtItemValue0.Location = new System.Drawing.Point(111, 42);
            this.txtItemValue0.Multiline = true;
            this.txtItemValue0.Name = "txtItemValue0";
            this.txtItemValue0.Size = new System.Drawing.Size(117, 21);
            this.txtItemValue0.TabIndex = 33;
            this.txtItemValue0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtItemValue0.TextChanged += new System.EventHandler(this.txtItemValue1_TextChanged);
            this.txtItemValue0.GotFocus += new System.EventHandler(this.txtItemValue1_GotFocus);
            this.txtItemValue0.LostFocus += new System.EventHandler(this.txtItemValue1_LostFocus);
            // 
            // txtItemName0
            // 
            this.txtItemName0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtItemName0.Location = new System.Drawing.Point(0, 42);
            this.txtItemName0.Name = "txtItemName0";
            this.txtItemName0.ReadOnly = true;
            this.txtItemName0.Size = new System.Drawing.Size(113, 21);
            this.txtItemName0.TabIndex = 32;
            this.txtItemName0.GotFocus += new System.EventHandler(this.txtItemName0_GotFocus);
            // 
            // txtItemValue1
            // 
            this.txtItemValue1.Location = new System.Drawing.Point(111, 62);
            this.txtItemValue1.Multiline = true;
            this.txtItemValue1.Name = "txtItemValue1";
            this.txtItemValue1.Size = new System.Drawing.Size(117, 21);
            this.txtItemValue1.TabIndex = 35;
            this.txtItemValue1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtItemValue1.TextChanged += new System.EventHandler(this.txtItemValue1_TextChanged);
            this.txtItemValue1.GotFocus += new System.EventHandler(this.txtItemValue1_GotFocus);
            this.txtItemValue1.LostFocus += new System.EventHandler(this.txtItemValue1_LostFocus);
            // 
            // txtItemName1
            // 
            this.txtItemName1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtItemName1.Location = new System.Drawing.Point(0, 62);
            this.txtItemName1.Name = "txtItemName1";
            this.txtItemName1.ReadOnly = true;
            this.txtItemName1.Size = new System.Drawing.Size(113, 21);
            this.txtItemName1.TabIndex = 34;
            this.txtItemName1.GotFocus += new System.EventHandler(this.txtItemName0_GotFocus);
            // 
            // txtItemValue2
            // 
            this.txtItemValue2.Location = new System.Drawing.Point(111, 82);
            this.txtItemValue2.Multiline = true;
            this.txtItemValue2.Name = "txtItemValue2";
            this.txtItemValue2.Size = new System.Drawing.Size(117, 21);
            this.txtItemValue2.TabIndex = 37;
            this.txtItemValue2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtItemValue2.TextChanged += new System.EventHandler(this.txtItemValue1_TextChanged);
            this.txtItemValue2.GotFocus += new System.EventHandler(this.txtItemValue1_GotFocus);
            this.txtItemValue2.LostFocus += new System.EventHandler(this.txtItemValue1_LostFocus);
            // 
            // txtItemName2
            // 
            this.txtItemName2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtItemName2.Location = new System.Drawing.Point(0, 82);
            this.txtItemName2.Name = "txtItemName2";
            this.txtItemName2.ReadOnly = true;
            this.txtItemName2.Size = new System.Drawing.Size(113, 21);
            this.txtItemName2.TabIndex = 36;
            this.txtItemName2.GotFocus += new System.EventHandler(this.txtItemName0_GotFocus);
            // 
            // txtItemValue3
            // 
            this.txtItemValue3.Location = new System.Drawing.Point(111, 102);
            this.txtItemValue3.Multiline = true;
            this.txtItemValue3.Name = "txtItemValue3";
            this.txtItemValue3.Size = new System.Drawing.Size(117, 21);
            this.txtItemValue3.TabIndex = 39;
            this.txtItemValue3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtItemValue3.TextChanged += new System.EventHandler(this.txtItemValue1_TextChanged);
            this.txtItemValue3.GotFocus += new System.EventHandler(this.txtItemValue1_GotFocus);
            this.txtItemValue3.LostFocus += new System.EventHandler(this.txtItemValue1_LostFocus);
            // 
            // txtItemName3
            // 
            this.txtItemName3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtItemName3.Location = new System.Drawing.Point(0, 102);
            this.txtItemName3.Name = "txtItemName3";
            this.txtItemName3.ReadOnly = true;
            this.txtItemName3.Size = new System.Drawing.Size(113, 21);
            this.txtItemName3.TabIndex = 38;
            this.txtItemName3.GotFocus += new System.EventHandler(this.txtItemName0_GotFocus);
            // 
            // txtItemValue4
            // 
            this.txtItemValue4.Location = new System.Drawing.Point(111, 122);
            this.txtItemValue4.Multiline = true;
            this.txtItemValue4.Name = "txtItemValue4";
            this.txtItemValue4.Size = new System.Drawing.Size(117, 21);
            this.txtItemValue4.TabIndex = 41;
            this.txtItemValue4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtItemValue4.TextChanged += new System.EventHandler(this.txtItemValue1_TextChanged);
            this.txtItemValue4.GotFocus += new System.EventHandler(this.txtItemValue1_GotFocus);
            this.txtItemValue4.LostFocus += new System.EventHandler(this.txtItemValue1_LostFocus);
            // 
            // txtItemName4
            // 
            this.txtItemName4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtItemName4.Location = new System.Drawing.Point(0, 122);
            this.txtItemName4.Name = "txtItemName4";
            this.txtItemName4.ReadOnly = true;
            this.txtItemName4.Size = new System.Drawing.Size(113, 21);
            this.txtItemName4.TabIndex = 40;
            this.txtItemName4.GotFocus += new System.EventHandler(this.txtItemName0_GotFocus);
            // 
            // txtItemValue5
            // 
            this.txtItemValue5.Location = new System.Drawing.Point(111, 142);
            this.txtItemValue5.Multiline = true;
            this.txtItemValue5.Name = "txtItemValue5";
            this.txtItemValue5.Size = new System.Drawing.Size(117, 21);
            this.txtItemValue5.TabIndex = 43;
            this.txtItemValue5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtItemValue5.TextChanged += new System.EventHandler(this.txtItemValue1_TextChanged);
            this.txtItemValue5.GotFocus += new System.EventHandler(this.txtItemValue1_GotFocus);
            this.txtItemValue5.LostFocus += new System.EventHandler(this.txtItemValue1_LostFocus);
            // 
            // txtItemName5
            // 
            this.txtItemName5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtItemName5.Location = new System.Drawing.Point(0, 142);
            this.txtItemName5.Name = "txtItemName5";
            this.txtItemName5.ReadOnly = true;
            this.txtItemName5.Size = new System.Drawing.Size(113, 21);
            this.txtItemName5.TabIndex = 42;
            this.txtItemName5.GotFocus += new System.EventHandler(this.txtItemName0_GotFocus);
            // 
            // txtItemValue6
            // 
            this.txtItemValue6.Location = new System.Drawing.Point(111, 162);
            this.txtItemValue6.Multiline = true;
            this.txtItemValue6.Name = "txtItemValue6";
            this.txtItemValue6.Size = new System.Drawing.Size(117, 21);
            this.txtItemValue6.TabIndex = 45;
            this.txtItemValue6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtItemValue6.TextChanged += new System.EventHandler(this.txtItemValue1_TextChanged);
            this.txtItemValue6.GotFocus += new System.EventHandler(this.txtItemValue1_GotFocus);
            this.txtItemValue6.LostFocus += new System.EventHandler(this.txtItemValue1_LostFocus);
            // 
            // txtItemName6
            // 
            this.txtItemName6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtItemName6.Location = new System.Drawing.Point(0, 162);
            this.txtItemName6.Name = "txtItemName6";
            this.txtItemName6.ReadOnly = true;
            this.txtItemName6.Size = new System.Drawing.Size(113, 21);
            this.txtItemName6.TabIndex = 44;
            this.txtItemName6.GotFocus += new System.EventHandler(this.txtItemName0_GotFocus);
            // 
            // txtItemValue7
            // 
            this.txtItemValue7.Location = new System.Drawing.Point(111, 182);
            this.txtItemValue7.Multiline = true;
            this.txtItemValue7.Name = "txtItemValue7";
            this.txtItemValue7.Size = new System.Drawing.Size(117, 21);
            this.txtItemValue7.TabIndex = 47;
            this.txtItemValue7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtItemValue7.TextChanged += new System.EventHandler(this.txtItemValue1_TextChanged);
            this.txtItemValue7.GotFocus += new System.EventHandler(this.txtItemValue1_GotFocus);
            this.txtItemValue7.LostFocus += new System.EventHandler(this.txtItemValue1_LostFocus);
            // 
            // txtItemName7
            // 
            this.txtItemName7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtItemName7.Location = new System.Drawing.Point(0, 182);
            this.txtItemName7.Name = "txtItemName7";
            this.txtItemName7.ReadOnly = true;
            this.txtItemName7.Size = new System.Drawing.Size(113, 21);
            this.txtItemName7.TabIndex = 46;
            this.txtItemName7.GotFocus += new System.EventHandler(this.txtItemName0_GotFocus);
            // 
            // txtItemValue8
            // 
            this.txtItemValue8.Location = new System.Drawing.Point(111, 201);
            this.txtItemValue8.Multiline = true;
            this.txtItemValue8.Name = "txtItemValue8";
            this.txtItemValue8.Size = new System.Drawing.Size(116, 21);
            this.txtItemValue8.TabIndex = 49;
            this.txtItemValue8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtItemValue8.TextChanged += new System.EventHandler(this.txtItemValue1_TextChanged);
            this.txtItemValue8.GotFocus += new System.EventHandler(this.txtItemValue1_GotFocus);
            this.txtItemValue8.LostFocus += new System.EventHandler(this.txtItemValue1_LostFocus);
            // 
            // txtItemName8
            // 
            this.txtItemName8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtItemName8.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular);
            this.txtItemName8.Location = new System.Drawing.Point(0, 201);
            this.txtItemName8.Name = "txtItemName8";
            this.txtItemName8.ReadOnly = true;
            this.txtItemName8.Size = new System.Drawing.Size(113, 22);
            this.txtItemName8.TabIndex = 48;
            this.txtItemName8.GotFocus += new System.EventHandler(this.txtItemName0_GotFocus);
            // 
            // btnAddNurseEvent
            // 
            this.btnAddNurseEvent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnAddNurseEvent.Location = new System.Drawing.Point(0, 221);
            this.btnAddNurseEvent.Name = "btnAddNurseEvent";
            this.btnAddNurseEvent.Size = new System.Drawing.Size(46, 28);
            this.btnAddNurseEvent.TabIndex = 50;
            this.btnAddNurseEvent.Text = "增加";
            this.btnAddNurseEvent.Click += new System.EventHandler(this.btnAddNurseEvent_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBox1.Location = new System.Drawing.Point(0, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(113, 21);
            this.textBox1.TabIndex = 52;
            this.textBox1.Text = "项目名称";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBox2.Location = new System.Drawing.Point(111, 22);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(117, 21);
            this.textBox2.TabIndex = 53;
            this.textBox2.Text = "值";
            // 
            // vsBar
            // 
            this.vsBar.LargeChange = 1;
            this.vsBar.Location = new System.Drawing.Point(227, 0);
            this.vsBar.Maximum = 10;
            this.vsBar.Name = "vsBar";
            this.vsBar.Size = new System.Drawing.Size(13, 221);
            this.vsBar.TabIndex = 54;
            this.vsBar.ValueChanged += new System.EventHandler(this.vsBar_ValueChanged);
            // 
            // comboxNurseEventType
            // 
            this.comboxNurseEventType.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.comboxNurseEventType.Items.Add("生命体征");
            this.comboxNurseEventType.Items.Add("出量");
            this.comboxNurseEventType.Items.Add("入量");
            this.comboxNurseEventType.Items.Add("特殊项目");
            this.comboxNurseEventType.Items.Add("护理事件");
            this.comboxNurseEventType.Location = new System.Drawing.Point(0, 0);
            this.comboxNurseEventType.Name = "comboxNurseEventType";
            this.comboxNurseEventType.Size = new System.Drawing.Size(228, 24);
            this.comboxNurseEventType.TabIndex = 55;
            this.comboxNurseEventType.SelectedIndexChanged += new System.EventHandler(this.lvwNurseEventType_SelectedIndexChanged);
            // 
            // VitalRecorderFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.txtItemValue8);
            this.Controls.Add(this.txtItemName8);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboxNurseEventType);
            this.Controls.Add(this.vsBar);
            this.Controls.Add(this.btnAddNurseEvent);
            this.Controls.Add(this.txtItemValue7);
            this.Controls.Add(this.txtItemName7);
            this.Controls.Add(this.txtItemValue6);
            this.Controls.Add(this.txtItemName6);
            this.Controls.Add(this.txtItemValue5);
            this.Controls.Add(this.txtItemName5);
            this.Controls.Add(this.txtItemValue4);
            this.Controls.Add(this.txtItemName4);
            this.Controls.Add(this.txtItemValue3);
            this.Controls.Add(this.txtItemName3);
            this.Controls.Add(this.txtItemValue2);
            this.Controls.Add(this.txtItemName2);
            this.Controls.Add(this.txtItemValue1);
            this.Controls.Add(this.txtItemName1);
            this.Controls.Add(this.txtItemValue0);
            this.Controls.Add(this.txtItemName0);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnNextTimePoint);
            this.Controls.Add(this.btnCurrTimePoint);
            this.Controls.Add(this.btnPreTimePoint);
            this.Controls.Add(this.btnListPatient);
            this.Controls.Add(this.btnNextPatient);
            this.Controls.Add(this.btnCurrPatient);
            this.Controls.Add(this.btnPrePatient);
            this.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "VitalRecorderFrm";
            this.Text = "护理信息";
            this.Load += new System.EventHandler(this.VitalRecorderFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnListPatient;
        private System.Windows.Forms.Button btnNextPatient;
        private System.Windows.Forms.Button btnCurrPatient;
        private System.Windows.Forms.Button btnPrePatient;
        private System.Windows.Forms.MenuItem mnuCancel;
        private System.Windows.Forms.MenuItem mnuPatient;
        private System.Windows.Forms.Button btnNextTimePoint;
        private System.Windows.Forms.Button btnCurrTimePoint;
        private System.Windows.Forms.Button btnPreTimePoint;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtItemValue0;
        private System.Windows.Forms.TextBox txtItemName0;
        private System.Windows.Forms.TextBox txtItemValue1;
        private System.Windows.Forms.TextBox txtItemName1;
        private System.Windows.Forms.TextBox txtItemValue2;
        private System.Windows.Forms.TextBox txtItemName2;
        private System.Windows.Forms.TextBox txtItemValue3;
        private System.Windows.Forms.TextBox txtItemName3;
        private System.Windows.Forms.TextBox txtItemValue4;
        private System.Windows.Forms.TextBox txtItemName4;
        private System.Windows.Forms.TextBox txtItemValue5;
        private System.Windows.Forms.TextBox txtItemName5;
        private System.Windows.Forms.TextBox txtItemValue6;
        private System.Windows.Forms.TextBox txtItemName6;
        private System.Windows.Forms.TextBox txtItemValue7;
        private System.Windows.Forms.TextBox txtItemName7;
        private System.Windows.Forms.TextBox txtItemValue8;
        private System.Windows.Forms.TextBox txtItemName8;
        private System.Windows.Forms.Button btnAddNurseEvent;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.VScrollBar vsBar;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
        private System.Windows.Forms.MenuItem mnuMemo;
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
        private System.Windows.Forms.ComboBox comboxNurseEventType;
    }
}