namespace HISPlus
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class FormNuresPersonnel : FormDo
    {
        private HISPlus.UserControls.UcButton btnAdd;
        private HISPlus.UserControls.UcButton btnSave;
        private HISPlus.UserControls.UcButton btnDel;
        private HISPlus.UserControls.UcComboBox comboBox1;
        private IContainer components = null;
        private MaskedTextBox DATE_OF_BIRTH;
        private MaskedTextBox DATE_OF_DUTY;
        private MaskedTextBox DATE_OF_TITLE;
        private HISPlus.UserControls.UcComboBox DEGREE_1;
        private HISPlus.UserControls.UcComboBox DEGREE_2;
        private HISPlus.UserControls.UcComboBox DEPT_CODE;
        private HISPlus.UserControls.UcComboBox DUTY;
        private HISPlus.UserControls.UcComboBox EDUCATION_TYPE_1;
        private HISPlus.UserControls.UcComboBox EDUCATION_TYPE_2;
        private HISPlus.UserControls.UcComboBox EDUCATIONAL_LEVEL_1;
        private HISPlus.UserControls.UcComboBox EDUCATIONAL_LEVEL_2;
        private MaskedTextBox ENTER_DATE;
        private MaskedTextBox GRADUATE_DATE_1;
        private MaskedTextBox GRADUATE_DATE_2;
        private TextBox GRADUATE_FROM_1;
        private TextBox GRADUATE_FROM_2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label18;
        private Label label19;
        private Label label2;
        private Label label20;
        private Label label21;
        private Label label22;
        private Label label23;
        private Label label24;
        private Label label25;
        private Label label26;
        private Label label27;
        private Label label28;
        private Label label29;
        private Label label3;
        private Label label30;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private ListBox listBox1;
        private TextBox NAME;
        private TextBox NURSE_AGE;
        private TextBox NURSE_ID;
        private HISPlus.UserControls.UcComboBox NURSE_TYPE;
        private TextBox REG_DATE;
        private TextBox REGISTER_NO;
        private HISPlus.UserControls.UcComboBox SEX;
        private MaskedTextBox START_DATE_OF_WORK;
        private HISPlus.UserControls.UcComboBox TITLE;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private GroupBox groupBox3;
        private HISPlus.UserControls.UcComboBox comboBox2;
        private HISPlus.UserControls.UcComboBox comboBox3;
        private HISPlus.UserControls.UcComboBox comboBox4;
        private HISPlus.UserControls.UcComboBox comboBox5;
        private HISPlus.UserControls.UcComboBox comboBox6;
        private HISPlus.UserControls.UcComboBox comboBox7;
        private HISPlus.UserControls.UcComboBox comboBox8;
        private HISPlus.UserControls.UcComboBox comboBox9;
        private HISPlus.UserControls.UcComboBox comboBox10;
        private MaskedTextBox maskedTextBox1;
        private MaskedTextBox maskedTextBox2;
        private MaskedTextBox maskedTextBox3;
        private MaskedTextBox maskedTextBox4;
        private MaskedTextBox maskedTextBox5;
        private MaskedTextBox maskedTextBox6;
        private MaskedTextBox maskedTextBox7;
        private HISPlus.UserControls.UcComboBox comboBox11;
        private TextBox textBox1;
        private Label label12;
        private Label label17;
        private TextBox textBox2;
        private Label label31;
        private Label label32;
        private TextBox textBox3;
        private Label label33;
        private Label label34;
        private Label label35;
        private Label label36;
        private Label label37;
        private Label label38;
        private TextBox textBox4;
        private Label label39;
        private Label label40;
        private Label label41;
        private Label label42;
        private Label label43;
        private Label label44;
        private Label label45;
        private Label label46;
        private Label label47;
        private Label label48;
        private Label label49;
        private TextBox textBox5;
        private TextBox textBox6;
        private Label label50;
        private Label label51;
        private TextBox textBox7;
        private HISPlus.UserControls.UcComboBox comboBox12;
        private Label label52;
        private Label label53;
        private HISPlus.UserControls.UcComboBox comboBox13;
        private Label label54;
        private HISPlus.UserControls.UcComboBox WORKING_SATUS;

        public FormNuresPersonnel()
        {
            base._id = "00030";
            base._guid = "E3EAC78E-2AAC-410a-9C63-77FA6378329C";
            this.InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listBox1.DataSource = this.GetListInfo();
            this.listBox1.DisplayMember = "NAME";
            this.listBox1.ValueMember = "NURSE_ID";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FormNuresPersonnel_Load(object sender, EventArgs e)
        {
            try
            {
                this.comboBox1.DataSource = this.GetComboInfo("NURSING_DEPT_DICT", "DEPT_NAME", "DEPT_CODE");
                this.comboBox1.DisplayMember = "DEPT_NAME";
                this.comboBox1.ValueMember = "DEPT_CODE";
                this.comboBox1.SelectedValue = GVars.User.DeptCode;

                //this.comboBox13.AllowNullInput = true;

                this.listBox1.DataSource = this.GetListInfo();
                this.listBox1.DisplayMember = "NAME";
                this.listBox1.ValueMember = "NURSE_ID";
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private DataTable GetComboInfo(string TableName, string TextName, string ValueName)
        {
            string str2 = string.Empty;
            string sqlSel = (str2 + "SELECT " + TextName + "," + ValueName) + " FROM " + TableName;
            return GVars.OracleAccess.SelectData(sqlSel).Tables[0];
        }

        private DataTable GetListInfo()
        {
            string sqlSel = ((string.Empty + "SELECT NAME,NURSE_ID" + " FROM ") + "NURSE_PERSONNEL_INFO " + "WHERE ") + "DEPT_CODE = '" + this.comboBox1.SelectedValue.ToString() + "'";
            return GVars.OracleAccess.SelectData(sqlSel).Tables[0];
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNuresPersonnel));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new HISPlus.UserControls.UcComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.EDUCATION_TYPE_2 = new HISPlus.UserControls.UcComboBox();
            this.EDUCATION_TYPE_1 = new HISPlus.UserControls.UcComboBox();
            this.DEGREE_2 = new HISPlus.UserControls.UcComboBox();
            this.EDUCATIONAL_LEVEL_2 = new HISPlus.UserControls.UcComboBox();
            this.WORKING_SATUS = new HISPlus.UserControls.UcComboBox();
            this.DEGREE_1 = new HISPlus.UserControls.UcComboBox();
            this.EDUCATIONAL_LEVEL_1 = new HISPlus.UserControls.UcComboBox();
            this.DUTY = new HISPlus.UserControls.UcComboBox();
            this.TITLE = new HISPlus.UserControls.UcComboBox();
            this.GRADUATE_DATE_1 = new System.Windows.Forms.MaskedTextBox();
            this.ENTER_DATE = new System.Windows.Forms.MaskedTextBox();
            this.GRADUATE_DATE_2 = new System.Windows.Forms.MaskedTextBox();
            this.START_DATE_OF_WORK = new System.Windows.Forms.MaskedTextBox();
            this.DATE_OF_DUTY = new System.Windows.Forms.MaskedTextBox();
            this.DATE_OF_TITLE = new System.Windows.Forms.MaskedTextBox();
            this.DATE_OF_BIRTH = new System.Windows.Forms.MaskedTextBox();
            this.SEX = new HISPlus.UserControls.UcComboBox();
            this.REG_DATE = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.NURSE_AGE = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.GRADUATE_FROM_2 = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.GRADUATE_FROM_1 = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.NAME = new System.Windows.Forms.TextBox();
            this.REGISTER_NO = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.NURSE_ID = new System.Windows.Forms.TextBox();
            this.NURSE_TYPE = new HISPlus.UserControls.UcComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.DEPT_CODE = new HISPlus.UserControls.UcComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAdd = new HISPlus.UserControls.UcButton();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.btnDel = new HISPlus.UserControls.UcButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBox2 = new HISPlus.UserControls.UcComboBox();
            this.comboBox3 = new HISPlus.UserControls.UcComboBox();
            this.comboBox4 = new HISPlus.UserControls.UcComboBox();
            this.comboBox5 = new HISPlus.UserControls.UcComboBox();
            this.comboBox6 = new HISPlus.UserControls.UcComboBox();
            this.comboBox7 = new HISPlus.UserControls.UcComboBox();
            this.comboBox8 = new HISPlus.UserControls.UcComboBox();
            this.comboBox9 = new HISPlus.UserControls.UcComboBox();
            this.comboBox10 = new HISPlus.UserControls.UcComboBox();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox3 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox4 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox5 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox6 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox7 = new System.Windows.Forms.MaskedTextBox();
            this.comboBox11 = new HISPlus.UserControls.UcComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.comboBox12 = new HISPlus.UserControls.UcComboBox();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.comboBox13 = new HISPlus.UserControls.UcComboBox();
            this.label54 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 593);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "人员列表";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "护士名单";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Location = new System.Drawing.Point(10, 99);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(219, 480);
            this.listBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "科室\\单元";
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = null;
            this.comboBox1.DisplayMember = null;
            this.comboBox1.DropDownHeight = 0;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboBox1.DropDownWidth = 0;
            this.comboBox1.DroppedDown = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.IntegralHeight = true;
            this.comboBox1.Location = new System.Drawing.Point(92, 34);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox1.MaxDropDownItems = 0;
            this.comboBox1.MaximumSize = new System.Drawing.Size(750, 19);
            this.comboBox1.MinimumSize = new System.Drawing.Size(30, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.SelectedIndex = -1;
            this.comboBox1.SelectedValue = null;
            this.comboBox1.Size = new System.Drawing.Size(142, 19);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.ValueMember = null;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.EDUCATION_TYPE_2);
            this.groupBox2.Controls.Add(this.EDUCATION_TYPE_1);
            this.groupBox2.Controls.Add(this.DEGREE_2);
            this.groupBox2.Controls.Add(this.EDUCATIONAL_LEVEL_2);
            this.groupBox2.Controls.Add(this.WORKING_SATUS);
            this.groupBox2.Controls.Add(this.DEGREE_1);
            this.groupBox2.Controls.Add(this.EDUCATIONAL_LEVEL_1);
            this.groupBox2.Controls.Add(this.DUTY);
            this.groupBox2.Controls.Add(this.TITLE);
            this.groupBox2.Controls.Add(this.GRADUATE_DATE_1);
            this.groupBox2.Controls.Add(this.ENTER_DATE);
            this.groupBox2.Controls.Add(this.GRADUATE_DATE_2);
            this.groupBox2.Controls.Add(this.START_DATE_OF_WORK);
            this.groupBox2.Controls.Add(this.DATE_OF_DUTY);
            this.groupBox2.Controls.Add(this.DATE_OF_TITLE);
            this.groupBox2.Controls.Add(this.DATE_OF_BIRTH);
            this.groupBox2.Controls.Add(this.SEX);
            this.groupBox2.Controls.Add(this.REG_DATE);
            this.groupBox2.Controls.Add(this.label30);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.NURSE_AGE);
            this.groupBox2.Controls.Add(this.label29);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.GRADUATE_FROM_2);
            this.groupBox2.Controls.Add(this.label28);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label27);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.GRADUATE_FROM_1);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.NAME);
            this.groupBox2.Controls.Add(this.REGISTER_NO);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.NURSE_ID);
            this.groupBox2.Controls.Add(this.NURSE_TYPE);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.DEPT_CODE);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(248, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(628, 543);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "护士基本信息";
            // 
            // EDUCATION_TYPE_2
            // 
            this.EDUCATION_TYPE_2.DataSource = null;
            this.EDUCATION_TYPE_2.DisplayMember = null;
            this.EDUCATION_TYPE_2.DropDownHeight = 0;
            this.EDUCATION_TYPE_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.EDUCATION_TYPE_2.DropDownWidth = 0;
            this.EDUCATION_TYPE_2.DroppedDown = false;
            this.EDUCATION_TYPE_2.FormattingEnabled = true;
            this.EDUCATION_TYPE_2.IntegralHeight = true;
            this.EDUCATION_TYPE_2.Location = new System.Drawing.Point(388, 384);
            this.EDUCATION_TYPE_2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.EDUCATION_TYPE_2.MaxDropDownItems = 0;
            this.EDUCATION_TYPE_2.MaximumSize = new System.Drawing.Size(750, 19);
            this.EDUCATION_TYPE_2.MinimumSize = new System.Drawing.Size(30, 19);
            this.EDUCATION_TYPE_2.Name = "EDUCATION_TYPE_2";
            this.EDUCATION_TYPE_2.SelectedIndex = -1;
            this.EDUCATION_TYPE_2.SelectedValue = null;
            this.EDUCATION_TYPE_2.Size = new System.Drawing.Size(180, 19);
            this.EDUCATION_TYPE_2.TabIndex = 42;
            this.EDUCATION_TYPE_2.ValueMember = null;
            // 
            // EDUCATION_TYPE_1
            // 
            this.EDUCATION_TYPE_1.DataSource = null;
            this.EDUCATION_TYPE_1.DisplayMember = null;
            this.EDUCATION_TYPE_1.DropDownHeight = 0;
            this.EDUCATION_TYPE_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.EDUCATION_TYPE_1.DropDownWidth = 0;
            this.EDUCATION_TYPE_1.DroppedDown = false;
            this.EDUCATION_TYPE_1.FormattingEnabled = true;
            this.EDUCATION_TYPE_1.IntegralHeight = true;
            this.EDUCATION_TYPE_1.Location = new System.Drawing.Point(95, 316);
            this.EDUCATION_TYPE_1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.EDUCATION_TYPE_1.MaxDropDownItems = 0;
            this.EDUCATION_TYPE_1.MaximumSize = new System.Drawing.Size(750, 19);
            this.EDUCATION_TYPE_1.MinimumSize = new System.Drawing.Size(30, 19);
            this.EDUCATION_TYPE_1.Name = "EDUCATION_TYPE_1";
            this.EDUCATION_TYPE_1.SelectedIndex = -1;
            this.EDUCATION_TYPE_1.SelectedValue = null;
            this.EDUCATION_TYPE_1.Size = new System.Drawing.Size(180, 19);
            this.EDUCATION_TYPE_1.TabIndex = 41;
            this.EDUCATION_TYPE_1.ValueMember = null;
            // 
            // DEGREE_2
            // 
            this.DEGREE_2.DataSource = null;
            this.DEGREE_2.DisplayMember = null;
            this.DEGREE_2.DropDownHeight = 0;
            this.DEGREE_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.DEGREE_2.DropDownWidth = 0;
            this.DEGREE_2.DroppedDown = false;
            this.DEGREE_2.FormattingEnabled = true;
            this.DEGREE_2.IntegralHeight = true;
            this.DEGREE_2.Location = new System.Drawing.Point(95, 351);
            this.DEGREE_2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DEGREE_2.MaxDropDownItems = 0;
            this.DEGREE_2.MaximumSize = new System.Drawing.Size(750, 19);
            this.DEGREE_2.MinimumSize = new System.Drawing.Size(30, 19);
            this.DEGREE_2.Name = "DEGREE_2";
            this.DEGREE_2.SelectedIndex = -1;
            this.DEGREE_2.SelectedValue = null;
            this.DEGREE_2.Size = new System.Drawing.Size(180, 19);
            this.DEGREE_2.TabIndex = 40;
            this.DEGREE_2.ValueMember = null;
            // 
            // EDUCATIONAL_LEVEL_2
            // 
            this.EDUCATIONAL_LEVEL_2.DataSource = null;
            this.EDUCATIONAL_LEVEL_2.DisplayMember = null;
            this.EDUCATIONAL_LEVEL_2.DropDownHeight = 0;
            this.EDUCATIONAL_LEVEL_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.EDUCATIONAL_LEVEL_2.DropDownWidth = 0;
            this.EDUCATIONAL_LEVEL_2.DroppedDown = false;
            this.EDUCATIONAL_LEVEL_2.FormattingEnabled = true;
            this.EDUCATIONAL_LEVEL_2.IntegralHeight = true;
            this.EDUCATIONAL_LEVEL_2.Location = new System.Drawing.Point(387, 314);
            this.EDUCATIONAL_LEVEL_2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.EDUCATIONAL_LEVEL_2.MaxDropDownItems = 0;
            this.EDUCATIONAL_LEVEL_2.MaximumSize = new System.Drawing.Size(750, 19);
            this.EDUCATIONAL_LEVEL_2.MinimumSize = new System.Drawing.Size(30, 19);
            this.EDUCATIONAL_LEVEL_2.Name = "EDUCATIONAL_LEVEL_2";
            this.EDUCATIONAL_LEVEL_2.SelectedIndex = -1;
            this.EDUCATIONAL_LEVEL_2.SelectedValue = null;
            this.EDUCATIONAL_LEVEL_2.Size = new System.Drawing.Size(180, 19);
            this.EDUCATIONAL_LEVEL_2.TabIndex = 39;
            this.EDUCATIONAL_LEVEL_2.ValueMember = null;
            // 
            // WORKING_SATUS
            // 
            this.WORKING_SATUS.DataSource = null;
            this.WORKING_SATUS.DisplayMember = null;
            this.WORKING_SATUS.DropDownHeight = 0;
            this.WORKING_SATUS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.WORKING_SATUS.DropDownWidth = 0;
            this.WORKING_SATUS.DroppedDown = false;
            this.WORKING_SATUS.FormattingEnabled = true;
            this.WORKING_SATUS.IntegralHeight = true;
            this.WORKING_SATUS.Location = new System.Drawing.Point(387, 515);
            this.WORKING_SATUS.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.WORKING_SATUS.MaxDropDownItems = 0;
            this.WORKING_SATUS.MaximumSize = new System.Drawing.Size(750, 19);
            this.WORKING_SATUS.MinimumSize = new System.Drawing.Size(30, 19);
            this.WORKING_SATUS.Name = "WORKING_SATUS";
            this.WORKING_SATUS.SelectedIndex = -1;
            this.WORKING_SATUS.SelectedValue = null;
            this.WORKING_SATUS.Size = new System.Drawing.Size(180, 19);
            this.WORKING_SATUS.TabIndex = 38;
            this.WORKING_SATUS.ValueMember = null;
            // 
            // DEGREE_1
            // 
            this.DEGREE_1.DataSource = null;
            this.DEGREE_1.DisplayMember = null;
            this.DEGREE_1.DropDownHeight = 0;
            this.DEGREE_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.DEGREE_1.DropDownWidth = 0;
            this.DEGREE_1.DroppedDown = false;
            this.DEGREE_1.FormattingEnabled = true;
            this.DEGREE_1.IntegralHeight = true;
            this.DEGREE_1.Location = new System.Drawing.Point(388, 244);
            this.DEGREE_1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DEGREE_1.MaxDropDownItems = 0;
            this.DEGREE_1.MaximumSize = new System.Drawing.Size(750, 19);
            this.DEGREE_1.MinimumSize = new System.Drawing.Size(30, 19);
            this.DEGREE_1.Name = "DEGREE_1";
            this.DEGREE_1.SelectedIndex = -1;
            this.DEGREE_1.SelectedValue = null;
            this.DEGREE_1.Size = new System.Drawing.Size(180, 19);
            this.DEGREE_1.TabIndex = 37;
            this.DEGREE_1.ValueMember = null;
            // 
            // EDUCATIONAL_LEVEL_1
            // 
            this.EDUCATIONAL_LEVEL_1.DataSource = null;
            this.EDUCATIONAL_LEVEL_1.DisplayMember = null;
            this.EDUCATIONAL_LEVEL_1.DropDownHeight = 0;
            this.EDUCATIONAL_LEVEL_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.EDUCATIONAL_LEVEL_1.DropDownWidth = 0;
            this.EDUCATIONAL_LEVEL_1.DroppedDown = false;
            this.EDUCATIONAL_LEVEL_1.FormattingEnabled = true;
            this.EDUCATIONAL_LEVEL_1.IntegralHeight = true;
            this.EDUCATIONAL_LEVEL_1.Location = new System.Drawing.Point(95, 246);
            this.EDUCATIONAL_LEVEL_1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.EDUCATIONAL_LEVEL_1.MaxDropDownItems = 0;
            this.EDUCATIONAL_LEVEL_1.MaximumSize = new System.Drawing.Size(750, 19);
            this.EDUCATIONAL_LEVEL_1.MinimumSize = new System.Drawing.Size(30, 19);
            this.EDUCATIONAL_LEVEL_1.Name = "EDUCATIONAL_LEVEL_1";
            this.EDUCATIONAL_LEVEL_1.SelectedIndex = -1;
            this.EDUCATIONAL_LEVEL_1.SelectedValue = null;
            this.EDUCATIONAL_LEVEL_1.Size = new System.Drawing.Size(180, 19);
            this.EDUCATIONAL_LEVEL_1.TabIndex = 36;
            this.EDUCATIONAL_LEVEL_1.ValueMember = null;
            // 
            // DUTY
            // 
            this.DUTY.DataSource = null;
            this.DUTY.DisplayMember = null;
            this.DUTY.DropDownHeight = 0;
            this.DUTY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.DUTY.DropDownWidth = 0;
            this.DUTY.DroppedDown = false;
            this.DUTY.FormattingEnabled = true;
            this.DUTY.IntegralHeight = true;
            this.DUTY.Location = new System.Drawing.Point(388, 174);
            this.DUTY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DUTY.MaxDropDownItems = 0;
            this.DUTY.MaximumSize = new System.Drawing.Size(750, 19);
            this.DUTY.MinimumSize = new System.Drawing.Size(30, 19);
            this.DUTY.Name = "DUTY";
            this.DUTY.SelectedIndex = -1;
            this.DUTY.SelectedValue = null;
            this.DUTY.Size = new System.Drawing.Size(180, 19);
            this.DUTY.TabIndex = 35;
            this.DUTY.ValueMember = null;
            // 
            // TITLE
            // 
            this.TITLE.DataSource = null;
            this.TITLE.DisplayMember = null;
            this.TITLE.DropDownHeight = 0;
            this.TITLE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.TITLE.DropDownWidth = 0;
            this.TITLE.DroppedDown = false;
            this.TITLE.FormattingEnabled = true;
            this.TITLE.IntegralHeight = true;
            this.TITLE.Location = new System.Drawing.Point(388, 139);
            this.TITLE.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TITLE.MaxDropDownItems = 0;
            this.TITLE.MaximumSize = new System.Drawing.Size(750, 19);
            this.TITLE.MinimumSize = new System.Drawing.Size(30, 19);
            this.TITLE.Name = "TITLE";
            this.TITLE.SelectedIndex = -1;
            this.TITLE.SelectedValue = null;
            this.TITLE.Size = new System.Drawing.Size(180, 19);
            this.TITLE.TabIndex = 34;
            this.TITLE.ValueMember = null;
            // 
            // GRADUATE_DATE_1
            // 
            this.GRADUATE_DATE_1.Location = new System.Drawing.Point(95, 281);
            this.GRADUATE_DATE_1.Mask = "0000-00-00";
            this.GRADUATE_DATE_1.Name = "GRADUATE_DATE_1";
            this.GRADUATE_DATE_1.Size = new System.Drawing.Size(180, 22);
            this.GRADUATE_DATE_1.TabIndex = 33;
            this.GRADUATE_DATE_1.ValidatingType = typeof(System.DateTime);
            // 
            // ENTER_DATE
            // 
            this.ENTER_DATE.Location = new System.Drawing.Point(387, 419);
            this.ENTER_DATE.Mask = "0000-00-00";
            this.ENTER_DATE.Name = "ENTER_DATE";
            this.ENTER_DATE.Size = new System.Drawing.Size(180, 22);
            this.ENTER_DATE.TabIndex = 32;
            this.ENTER_DATE.ValidatingType = typeof(System.DateTime);
            // 
            // GRADUATE_DATE_2
            // 
            this.GRADUATE_DATE_2.Location = new System.Drawing.Point(388, 349);
            this.GRADUATE_DATE_2.Mask = "0000-00-00";
            this.GRADUATE_DATE_2.Name = "GRADUATE_DATE_2";
            this.GRADUATE_DATE_2.Size = new System.Drawing.Size(180, 22);
            this.GRADUATE_DATE_2.TabIndex = 31;
            this.GRADUATE_DATE_2.ValidatingType = typeof(System.DateTime);
            // 
            // START_DATE_OF_WORK
            // 
            this.START_DATE_OF_WORK.Location = new System.Drawing.Point(388, 209);
            this.START_DATE_OF_WORK.Mask = "0000-00-00";
            this.START_DATE_OF_WORK.Name = "START_DATE_OF_WORK";
            this.START_DATE_OF_WORK.Size = new System.Drawing.Size(180, 22);
            this.START_DATE_OF_WORK.TabIndex = 30;
            this.START_DATE_OF_WORK.ValidatingType = typeof(System.DateTime);
            // 
            // DATE_OF_DUTY
            // 
            this.DATE_OF_DUTY.Location = new System.Drawing.Point(95, 211);
            this.DATE_OF_DUTY.Mask = "0000-00-00";
            this.DATE_OF_DUTY.Name = "DATE_OF_DUTY";
            this.DATE_OF_DUTY.Size = new System.Drawing.Size(180, 22);
            this.DATE_OF_DUTY.TabIndex = 29;
            this.DATE_OF_DUTY.ValidatingType = typeof(System.DateTime);
            // 
            // DATE_OF_TITLE
            // 
            this.DATE_OF_TITLE.Location = new System.Drawing.Point(95, 176);
            this.DATE_OF_TITLE.Mask = "0000-00-00";
            this.DATE_OF_TITLE.Name = "DATE_OF_TITLE";
            this.DATE_OF_TITLE.Size = new System.Drawing.Size(180, 22);
            this.DATE_OF_TITLE.TabIndex = 28;
            this.DATE_OF_TITLE.ValidatingType = typeof(System.DateTime);
            // 
            // DATE_OF_BIRTH
            // 
            this.DATE_OF_BIRTH.Location = new System.Drawing.Point(388, 104);
            this.DATE_OF_BIRTH.Mask = "0000-00-00";
            this.DATE_OF_BIRTH.Name = "DATE_OF_BIRTH";
            this.DATE_OF_BIRTH.Size = new System.Drawing.Size(180, 22);
            this.DATE_OF_BIRTH.TabIndex = 27;
            this.DATE_OF_BIRTH.ValidatingType = typeof(System.DateTime);
            // 
            // SEX
            // 
            this.SEX.DataSource = null;
            this.SEX.DisplayMember = null;
            this.SEX.DropDownHeight = 0;
            this.SEX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.SEX.DropDownWidth = 0;
            this.SEX.DroppedDown = false;
            this.SEX.FormattingEnabled = true;
            this.SEX.IntegralHeight = true;
            this.SEX.Location = new System.Drawing.Point(95, 141);
            this.SEX.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SEX.MaxDropDownItems = 0;
            this.SEX.MaximumSize = new System.Drawing.Size(750, 19);
            this.SEX.MinimumSize = new System.Drawing.Size(30, 19);
            this.SEX.Name = "SEX";
            this.SEX.SelectedIndex = -1;
            this.SEX.SelectedValue = null;
            this.SEX.Size = new System.Drawing.Size(180, 19);
            this.SEX.TabIndex = 26;
            this.SEX.ValueMember = null;
            // 
            // REG_DATE
            // 
            this.REG_DATE.Location = new System.Drawing.Point(95, 517);
            this.REG_DATE.Name = "REG_DATE";
            this.REG_DATE.Size = new System.Drawing.Size(180, 22);
            this.REG_DATE.TabIndex = 25;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(304, 520);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(55, 14);
            this.label30.TabIndex = 24;
            this.label30.Text = "在位情况";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(18, 522);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(55, 14);
            this.label13.TabIndex = 24;
            this.label13.Text = "注册年号";
            // 
            // NURSE_AGE
            // 
            this.NURSE_AGE.Location = new System.Drawing.Point(95, 421);
            this.NURSE_AGE.Name = "NURSE_AGE";
            this.NURSE_AGE.Size = new System.Drawing.Size(180, 22);
            this.NURSE_AGE.TabIndex = 23;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(304, 424);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(55, 14);
            this.label29.TabIndex = 22;
            this.label29.Text = "来院时间";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(18, 426);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 14);
            this.label14.TabIndex = 22;
            this.label14.Text = "护龄";
            // 
            // GRADUATE_FROM_2
            // 
            this.GRADUATE_FROM_2.Location = new System.Drawing.Point(95, 386);
            this.GRADUATE_FROM_2.Name = "GRADUATE_FROM_2";
            this.GRADUATE_FROM_2.Size = new System.Drawing.Size(180, 22);
            this.GRADUATE_FROM_2.TabIndex = 21;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(304, 389);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(55, 14);
            this.label28.TabIndex = 20;
            this.label28.Text = "教育形式";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(18, 391);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(55, 14);
            this.label15.TabIndex = 20;
            this.label15.Text = "毕业院校";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(304, 354);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(55, 14);
            this.label27.TabIndex = 18;
            this.label27.Text = "毕业时间";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(18, 356);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(55, 14);
            this.label16.TabIndex = 18;
            this.label16.Text = "最高学位";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(304, 319);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(55, 14);
            this.label26.TabIndex = 16;
            this.label26.Text = "最高学历";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(18, 321);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 14);
            this.label11.TabIndex = 16;
            this.label11.Text = "教育形式";
            // 
            // GRADUATE_FROM_1
            // 
            this.GRADUATE_FROM_1.Location = new System.Drawing.Point(388, 279);
            this.GRADUATE_FROM_1.Name = "GRADUATE_FROM_1";
            this.GRADUATE_FROM_1.Size = new System.Drawing.Size(180, 22);
            this.GRADUATE_FROM_1.TabIndex = 15;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(304, 284);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(55, 14);
            this.label25.TabIndex = 14;
            this.label25.Text = "毕业院校";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 286);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 14);
            this.label10.TabIndex = 14;
            this.label10.Text = "毕业时间";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(304, 249);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(55, 14);
            this.label24.TabIndex = 12;
            this.label24.Text = "第一学位";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 251);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 14);
            this.label9.TabIndex = 12;
            this.label9.Text = "第一学历";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(304, 214);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(55, 14);
            this.label23.TabIndex = 10;
            this.label23.Text = "工作时间";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 216);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 14);
            this.label8.TabIndex = 10;
            this.label8.Text = "职务时间";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(304, 179);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(31, 14);
            this.label22.TabIndex = 8;
            this.label22.Text = "职务";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 181);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 14);
            this.label7.TabIndex = 8;
            this.label7.Text = "职称时间";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(304, 144);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(31, 14);
            this.label21.TabIndex = 6;
            this.label21.Text = "职称";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 14);
            this.label6.TabIndex = 6;
            this.label6.Text = "性别";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(304, 109);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(55, 14);
            this.label20.TabIndex = 4;
            this.label20.Text = "出生日期";
            // 
            // NAME
            // 
            this.NAME.Location = new System.Drawing.Point(95, 106);
            this.NAME.Name = "NAME";
            this.NAME.Size = new System.Drawing.Size(180, 22);
            this.NAME.TabIndex = 5;
            // 
            // REGISTER_NO
            // 
            this.REGISTER_NO.Location = new System.Drawing.Point(388, 69);
            this.REGISTER_NO.Name = "REGISTER_NO";
            this.REGISTER_NO.Size = new System.Drawing.Size(180, 22);
            this.REGISTER_NO.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "姓名";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(304, 74);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(55, 14);
            this.label19.TabIndex = 2;
            this.label19.Text = "注册号码";
            // 
            // NURSE_ID
            // 
            this.NURSE_ID.Location = new System.Drawing.Point(95, 71);
            this.NURSE_ID.Name = "NURSE_ID";
            this.NURSE_ID.Size = new System.Drawing.Size(180, 22);
            this.NURSE_ID.TabIndex = 3;
            // 
            // NURSE_TYPE
            // 
            this.NURSE_TYPE.DataSource = null;
            this.NURSE_TYPE.DisplayMember = null;
            this.NURSE_TYPE.DropDownHeight = 0;
            this.NURSE_TYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.NURSE_TYPE.DropDownWidth = 0;
            this.NURSE_TYPE.DroppedDown = false;
            this.NURSE_TYPE.FormattingEnabled = true;
            this.NURSE_TYPE.IntegralHeight = true;
            this.NURSE_TYPE.Location = new System.Drawing.Point(388, 34);
            this.NURSE_TYPE.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.NURSE_TYPE.MaxDropDownItems = 0;
            this.NURSE_TYPE.MaximumSize = new System.Drawing.Size(750, 19);
            this.NURSE_TYPE.MinimumSize = new System.Drawing.Size(30, 19);
            this.NURSE_TYPE.Name = "NURSE_TYPE";
            this.NURSE_TYPE.SelectedIndex = -1;
            this.NURSE_TYPE.SelectedValue = null;
            this.NURSE_TYPE.Size = new System.Drawing.Size(180, 19);
            this.NURSE_TYPE.TabIndex = 1;
            this.NURSE_TYPE.ValueMember = null;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "护士ID";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(304, 39);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(55, 14);
            this.label18.TabIndex = 0;
            this.label18.Text = "护士类别";
            // 
            // DEPT_CODE
            // 
            this.DEPT_CODE.DataSource = null;
            this.DEPT_CODE.DisplayMember = null;
            this.DEPT_CODE.DropDownHeight = 0;
            this.DEPT_CODE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.DEPT_CODE.DropDownWidth = 0;
            this.DEPT_CODE.DroppedDown = false;
            this.DEPT_CODE.FormattingEnabled = true;
            this.DEPT_CODE.IntegralHeight = true;
            this.DEPT_CODE.Location = new System.Drawing.Point(95, 36);
            this.DEPT_CODE.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DEPT_CODE.MaxDropDownItems = 0;
            this.DEPT_CODE.MaximumSize = new System.Drawing.Size(750, 19);
            this.DEPT_CODE.MinimumSize = new System.Drawing.Size(30, 19);
            this.DEPT_CODE.Name = "DEPT_CODE";
            this.DEPT_CODE.SelectedIndex = -1;
            this.DEPT_CODE.SelectedValue = null;
            this.DEPT_CODE.Size = new System.Drawing.Size(180, 19);
            this.DEPT_CODE.TabIndex = 1;
            this.DEPT_CODE.ValueMember = null;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "护理单元";
            // 
            // btnAdd
            // 
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageRight = false;
            this.btnAdd.ImageStyle = HISPlus.UserControls.ImageStyle.Add;
            this.btnAdd.Location = new System.Drawing.Point(21, 5);
            this.btnAdd.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnAdd.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 30);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.TextValue = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageRight = false;
            this.btnSave.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSave.Location = new System.Drawing.Point(287, 6);
            this.btnSave.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnSave.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 3;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnDel
            // 
            this.btnDel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDel.Image = ((System.Drawing.Image)(resources.GetObject("btnDel.Image")));
            this.btnDel.ImageRight = false;
            this.btnDel.ImageStyle = HISPlus.UserControls.ImageStyle.Delete;
            this.btnDel.Location = new System.Drawing.Point(386, 5);
            this.btnDel.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnDel.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 30);
            this.btnDel.TabIndex = 4;
            this.btnDel.TextValue = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnAdd);
            this.panelControl1.Controls.Add(this.btnDel);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Location = new System.Drawing.Point(248, 549);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(628, 44);
            this.panelControl1.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBox2);
            this.groupBox3.Controls.Add(this.comboBox3);
            this.groupBox3.Controls.Add(this.comboBox4);
            this.groupBox3.Controls.Add(this.comboBox5);
            this.groupBox3.Controls.Add(this.comboBox6);
            this.groupBox3.Controls.Add(this.comboBox7);
            this.groupBox3.Controls.Add(this.comboBox8);
            this.groupBox3.Controls.Add(this.comboBox9);
            this.groupBox3.Controls.Add(this.comboBox10);
            this.groupBox3.Controls.Add(this.maskedTextBox1);
            this.groupBox3.Controls.Add(this.maskedTextBox2);
            this.groupBox3.Controls.Add(this.maskedTextBox3);
            this.groupBox3.Controls.Add(this.maskedTextBox4);
            this.groupBox3.Controls.Add(this.maskedTextBox5);
            this.groupBox3.Controls.Add(this.maskedTextBox6);
            this.groupBox3.Controls.Add(this.maskedTextBox7);
            this.groupBox3.Controls.Add(this.comboBox11);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Controls.Add(this.label31);
            this.groupBox3.Controls.Add(this.label32);
            this.groupBox3.Controls.Add(this.textBox3);
            this.groupBox3.Controls.Add(this.label33);
            this.groupBox3.Controls.Add(this.label34);
            this.groupBox3.Controls.Add(this.label35);
            this.groupBox3.Controls.Add(this.label36);
            this.groupBox3.Controls.Add(this.label37);
            this.groupBox3.Controls.Add(this.label38);
            this.groupBox3.Controls.Add(this.textBox4);
            this.groupBox3.Controls.Add(this.label39);
            this.groupBox3.Controls.Add(this.label40);
            this.groupBox3.Controls.Add(this.label41);
            this.groupBox3.Controls.Add(this.label42);
            this.groupBox3.Controls.Add(this.label43);
            this.groupBox3.Controls.Add(this.label44);
            this.groupBox3.Controls.Add(this.label45);
            this.groupBox3.Controls.Add(this.label46);
            this.groupBox3.Controls.Add(this.label47);
            this.groupBox3.Controls.Add(this.label48);
            this.groupBox3.Controls.Add(this.label49);
            this.groupBox3.Controls.Add(this.textBox5);
            this.groupBox3.Controls.Add(this.textBox6);
            this.groupBox3.Controls.Add(this.label50);
            this.groupBox3.Controls.Add(this.label51);
            this.groupBox3.Controls.Add(this.textBox7);
            this.groupBox3.Controls.Add(this.comboBox12);
            this.groupBox3.Controls.Add(this.label52);
            this.groupBox3.Controls.Add(this.label53);
            this.groupBox3.Controls.Add(this.comboBox13);
            this.groupBox3.Controls.Add(this.label54);
            this.groupBox3.Location = new System.Drawing.Point(248, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(628, 543);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "护士基本信息";
            // 
            // comboBox2
            // 
            this.comboBox2.DataSource = null;
            this.comboBox2.DisplayMember = null;
            this.comboBox2.DropDownHeight = 0;
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboBox2.DropDownWidth = 0;
            this.comboBox2.DroppedDown = false;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.IntegralHeight = true;
            this.comboBox2.Location = new System.Drawing.Point(388, 434);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox2.MaxDropDownItems = 0;
            this.comboBox2.MaximumSize = new System.Drawing.Size(750, 19);
            this.comboBox2.MinimumSize = new System.Drawing.Size(30, 19);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.SelectedIndex = -1;
            this.comboBox2.SelectedValue = null;
            this.comboBox2.Size = new System.Drawing.Size(180, 19);
            this.comboBox2.TabIndex = 42;
            this.comboBox2.ValueMember = null;
            // 
            // comboBox3
            // 
            this.comboBox3.DataSource = null;
            this.comboBox3.DisplayMember = null;
            this.comboBox3.DropDownHeight = 0;
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboBox3.DropDownWidth = 0;
            this.comboBox3.DroppedDown = false;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.IntegralHeight = true;
            this.comboBox3.Location = new System.Drawing.Point(95, 356);
            this.comboBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox3.MaxDropDownItems = 0;
            this.comboBox3.MaximumSize = new System.Drawing.Size(750, 19);
            this.comboBox3.MinimumSize = new System.Drawing.Size(30, 19);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.SelectedIndex = -1;
            this.comboBox3.SelectedValue = null;
            this.comboBox3.Size = new System.Drawing.Size(180, 19);
            this.comboBox3.TabIndex = 41;
            this.comboBox3.ValueMember = null;
            // 
            // comboBox4
            // 
            this.comboBox4.DataSource = null;
            this.comboBox4.DisplayMember = null;
            this.comboBox4.DropDownHeight = 0;
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboBox4.DropDownWidth = 0;
            this.comboBox4.DroppedDown = false;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.IntegralHeight = true;
            this.comboBox4.Location = new System.Drawing.Point(95, 396);
            this.comboBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox4.MaxDropDownItems = 0;
            this.comboBox4.MaximumSize = new System.Drawing.Size(750, 19);
            this.comboBox4.MinimumSize = new System.Drawing.Size(30, 19);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.SelectedIndex = -1;
            this.comboBox4.SelectedValue = null;
            this.comboBox4.Size = new System.Drawing.Size(180, 19);
            this.comboBox4.TabIndex = 40;
            this.comboBox4.ValueMember = null;
            // 
            // comboBox5
            // 
            this.comboBox5.DataSource = null;
            this.comboBox5.DisplayMember = null;
            this.comboBox5.DropDownHeight = 0;
            this.comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboBox5.DropDownWidth = 0;
            this.comboBox5.DroppedDown = false;
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.IntegralHeight = true;
            this.comboBox5.Location = new System.Drawing.Point(387, 354);
            this.comboBox5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox5.MaxDropDownItems = 0;
            this.comboBox5.MaximumSize = new System.Drawing.Size(750, 19);
            this.comboBox5.MinimumSize = new System.Drawing.Size(30, 19);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.SelectedIndex = -1;
            this.comboBox5.SelectedValue = null;
            this.comboBox5.Size = new System.Drawing.Size(180, 19);
            this.comboBox5.TabIndex = 39;
            this.comboBox5.ValueMember = null;
            // 
            // comboBox6
            // 
            this.comboBox6.DataSource = null;
            this.comboBox6.DisplayMember = null;
            this.comboBox6.DropDownHeight = 0;
            this.comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboBox6.DropDownWidth = 0;
            this.comboBox6.DroppedDown = false;
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.IntegralHeight = true;
            this.comboBox6.Location = new System.Drawing.Point(387, 514);
            this.comboBox6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox6.MaxDropDownItems = 0;
            this.comboBox6.MaximumSize = new System.Drawing.Size(750, 19);
            this.comboBox6.MinimumSize = new System.Drawing.Size(30, 19);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.SelectedIndex = -1;
            this.comboBox6.SelectedValue = null;
            this.comboBox6.Size = new System.Drawing.Size(180, 19);
            this.comboBox6.TabIndex = 38;
            this.comboBox6.ValueMember = null;
            // 
            // comboBox7
            // 
            this.comboBox7.DataSource = null;
            this.comboBox7.DisplayMember = null;
            this.comboBox7.DropDownHeight = 0;
            this.comboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboBox7.DropDownWidth = 0;
            this.comboBox7.DroppedDown = false;
            this.comboBox7.FormattingEnabled = true;
            this.comboBox7.IntegralHeight = true;
            this.comboBox7.Location = new System.Drawing.Point(388, 274);
            this.comboBox7.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox7.MaxDropDownItems = 0;
            this.comboBox7.MaximumSize = new System.Drawing.Size(750, 19);
            this.comboBox7.MinimumSize = new System.Drawing.Size(30, 19);
            this.comboBox7.Name = "comboBox7";
            this.comboBox7.SelectedIndex = -1;
            this.comboBox7.SelectedValue = null;
            this.comboBox7.Size = new System.Drawing.Size(180, 19);
            this.comboBox7.TabIndex = 37;
            this.comboBox7.ValueMember = null;
            // 
            // comboBox8
            // 
            this.comboBox8.DataSource = null;
            this.comboBox8.DisplayMember = null;
            this.comboBox8.DropDownHeight = 0;
            this.comboBox8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboBox8.DropDownWidth = 0;
            this.comboBox8.DroppedDown = false;
            this.comboBox8.FormattingEnabled = true;
            this.comboBox8.IntegralHeight = true;
            this.comboBox8.Location = new System.Drawing.Point(95, 276);
            this.comboBox8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox8.MaxDropDownItems = 0;
            this.comboBox8.MaximumSize = new System.Drawing.Size(750, 19);
            this.comboBox8.MinimumSize = new System.Drawing.Size(30, 19);
            this.comboBox8.Name = "comboBox8";
            this.comboBox8.SelectedIndex = -1;
            this.comboBox8.SelectedValue = null;
            this.comboBox8.Size = new System.Drawing.Size(180, 19);
            this.comboBox8.TabIndex = 36;
            this.comboBox8.ValueMember = null;
            // 
            // comboBox9
            // 
            this.comboBox9.DataSource = null;
            this.comboBox9.DisplayMember = null;
            this.comboBox9.DropDownHeight = 0;
            this.comboBox9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboBox9.DropDownWidth = 0;
            this.comboBox9.DroppedDown = false;
            this.comboBox9.FormattingEnabled = true;
            this.comboBox9.IntegralHeight = true;
            this.comboBox9.Location = new System.Drawing.Point(388, 194);
            this.comboBox9.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox9.MaxDropDownItems = 0;
            this.comboBox9.MaximumSize = new System.Drawing.Size(750, 19);
            this.comboBox9.MinimumSize = new System.Drawing.Size(30, 19);
            this.comboBox9.Name = "comboBox9";
            this.comboBox9.SelectedIndex = -1;
            this.comboBox9.SelectedValue = null;
            this.comboBox9.Size = new System.Drawing.Size(180, 19);
            this.comboBox9.TabIndex = 35;
            this.comboBox9.ValueMember = null;
            // 
            // comboBox10
            // 
            this.comboBox10.DataSource = null;
            this.comboBox10.DisplayMember = null;
            this.comboBox10.DropDownHeight = 0;
            this.comboBox10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboBox10.DropDownWidth = 0;
            this.comboBox10.DroppedDown = false;
            this.comboBox10.FormattingEnabled = true;
            this.comboBox10.IntegralHeight = true;
            this.comboBox10.Location = new System.Drawing.Point(388, 154);
            this.comboBox10.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox10.MaxDropDownItems = 0;
            this.comboBox10.MaximumSize = new System.Drawing.Size(750, 19);
            this.comboBox10.MinimumSize = new System.Drawing.Size(30, 19);
            this.comboBox10.Name = "comboBox10";
            this.comboBox10.SelectedIndex = -1;
            this.comboBox10.SelectedValue = null;
            this.comboBox10.Size = new System.Drawing.Size(180, 19);
            this.comboBox10.TabIndex = 34;
            this.comboBox10.ValueMember = null;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(95, 316);
            this.maskedTextBox1.Mask = "0000-00-00";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(180, 22);
            this.maskedTextBox1.TabIndex = 33;
            this.maskedTextBox1.ValidatingType = typeof(System.DateTime);
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.Location = new System.Drawing.Point(387, 474);
            this.maskedTextBox2.Mask = "0000-00-00";
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.Size = new System.Drawing.Size(180, 22);
            this.maskedTextBox2.TabIndex = 32;
            this.maskedTextBox2.ValidatingType = typeof(System.DateTime);
            // 
            // maskedTextBox3
            // 
            this.maskedTextBox3.Location = new System.Drawing.Point(388, 394);
            this.maskedTextBox3.Mask = "0000-00-00";
            this.maskedTextBox3.Name = "maskedTextBox3";
            this.maskedTextBox3.Size = new System.Drawing.Size(180, 22);
            this.maskedTextBox3.TabIndex = 31;
            this.maskedTextBox3.ValidatingType = typeof(System.DateTime);
            // 
            // maskedTextBox4
            // 
            this.maskedTextBox4.Location = new System.Drawing.Point(388, 234);
            this.maskedTextBox4.Mask = "0000-00-00";
            this.maskedTextBox4.Name = "maskedTextBox4";
            this.maskedTextBox4.Size = new System.Drawing.Size(180, 22);
            this.maskedTextBox4.TabIndex = 30;
            this.maskedTextBox4.ValidatingType = typeof(System.DateTime);
            // 
            // maskedTextBox5
            // 
            this.maskedTextBox5.Location = new System.Drawing.Point(95, 236);
            this.maskedTextBox5.Mask = "0000-00-00";
            this.maskedTextBox5.Name = "maskedTextBox5";
            this.maskedTextBox5.Size = new System.Drawing.Size(180, 22);
            this.maskedTextBox5.TabIndex = 29;
            this.maskedTextBox5.ValidatingType = typeof(System.DateTime);
            // 
            // maskedTextBox6
            // 
            this.maskedTextBox6.Location = new System.Drawing.Point(95, 196);
            this.maskedTextBox6.Mask = "0000-00-00";
            this.maskedTextBox6.Name = "maskedTextBox6";
            this.maskedTextBox6.Size = new System.Drawing.Size(180, 22);
            this.maskedTextBox6.TabIndex = 28;
            this.maskedTextBox6.ValidatingType = typeof(System.DateTime);
            // 
            // maskedTextBox7
            // 
            this.maskedTextBox7.Location = new System.Drawing.Point(388, 114);
            this.maskedTextBox7.Mask = "0000-00-00";
            this.maskedTextBox7.Name = "maskedTextBox7";
            this.maskedTextBox7.Size = new System.Drawing.Size(180, 22);
            this.maskedTextBox7.TabIndex = 27;
            this.maskedTextBox7.ValidatingType = typeof(System.DateTime);
            // 
            // comboBox11
            // 
            this.comboBox11.DataSource = null;
            this.comboBox11.DisplayMember = null;
            this.comboBox11.DropDownHeight = 0;
            this.comboBox11.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboBox11.DropDownWidth = 0;
            this.comboBox11.DroppedDown = false;
            this.comboBox11.FormattingEnabled = true;
            this.comboBox11.IntegralHeight = true;
            this.comboBox11.Location = new System.Drawing.Point(95, 156);
            this.comboBox11.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox11.MaxDropDownItems = 0;
            this.comboBox11.MaximumSize = new System.Drawing.Size(750, 19);
            this.comboBox11.MinimumSize = new System.Drawing.Size(30, 19);
            this.comboBox11.Name = "comboBox11";
            this.comboBox11.SelectedIndex = -1;
            this.comboBox11.SelectedValue = null;
            this.comboBox11.Size = new System.Drawing.Size(180, 19);
            this.comboBox11.TabIndex = 26;
            this.comboBox11.ValueMember = null;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(95, 516);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(180, 22);
            this.textBox1.TabIndex = 25;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(304, 519);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 14);
            this.label12.TabIndex = 24;
            this.label12.Text = "在位情况";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(18, 521);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(55, 14);
            this.label17.TabIndex = 24;
            this.label17.Text = "注册年号";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(95, 476);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(180, 22);
            this.textBox2.TabIndex = 23;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(304, 479);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(55, 14);
            this.label31.TabIndex = 22;
            this.label31.Text = "来院时间";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(18, 481);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(31, 14);
            this.label32.TabIndex = 22;
            this.label32.Text = "护龄";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(95, 436);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(180, 22);
            this.textBox3.TabIndex = 21;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(304, 439);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(55, 14);
            this.label33.TabIndex = 20;
            this.label33.Text = "教育形式";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(18, 441);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(55, 14);
            this.label34.TabIndex = 20;
            this.label34.Text = "毕业院校";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(304, 399);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(55, 14);
            this.label35.TabIndex = 18;
            this.label35.Text = "毕业时间";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(18, 401);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(55, 14);
            this.label36.TabIndex = 18;
            this.label36.Text = "最高学位";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(304, 359);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(55, 14);
            this.label37.TabIndex = 16;
            this.label37.Text = "最高学历";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(18, 361);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(55, 14);
            this.label38.TabIndex = 16;
            this.label38.Text = "教育形式";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(388, 314);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(180, 22);
            this.textBox4.TabIndex = 15;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(304, 319);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(55, 14);
            this.label39.TabIndex = 14;
            this.label39.Text = "毕业院校";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(18, 321);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(55, 14);
            this.label40.TabIndex = 14;
            this.label40.Text = "毕业时间";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(304, 279);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(55, 14);
            this.label41.TabIndex = 12;
            this.label41.Text = "第一学位";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(18, 281);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(55, 14);
            this.label42.TabIndex = 12;
            this.label42.Text = "第一学历";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(304, 239);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(55, 14);
            this.label43.TabIndex = 10;
            this.label43.Text = "工作时间";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(18, 241);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(55, 14);
            this.label44.TabIndex = 10;
            this.label44.Text = "职务时间";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(304, 199);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(31, 14);
            this.label45.TabIndex = 8;
            this.label45.Text = "职务";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(18, 201);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(55, 14);
            this.label46.TabIndex = 8;
            this.label46.Text = "职称时间";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(304, 159);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(31, 14);
            this.label47.TabIndex = 6;
            this.label47.Text = "职称";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(18, 161);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(31, 14);
            this.label48.TabIndex = 6;
            this.label48.Text = "性别";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(304, 119);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(55, 14);
            this.label49.TabIndex = 4;
            this.label49.Text = "出生日期";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(95, 116);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(180, 22);
            this.textBox5.TabIndex = 5;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(388, 74);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(180, 22);
            this.textBox6.TabIndex = 3;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(18, 121);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(31, 14);
            this.label50.TabIndex = 4;
            this.label50.Text = "姓名";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(304, 79);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(55, 14);
            this.label51.TabIndex = 2;
            this.label51.Text = "注册号码";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(95, 76);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(180, 22);
            this.textBox7.TabIndex = 3;
            // 
            // comboBox12
            // 
            this.comboBox12.DataSource = null;
            this.comboBox12.DisplayMember = null;
            this.comboBox12.DropDownHeight = 0;
            this.comboBox12.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboBox12.DropDownWidth = 0;
            this.comboBox12.DroppedDown = false;
            this.comboBox12.FormattingEnabled = true;
            this.comboBox12.IntegralHeight = true;
            this.comboBox12.Location = new System.Drawing.Point(388, 34);
            this.comboBox12.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox12.MaxDropDownItems = 0;
            this.comboBox12.MaximumSize = new System.Drawing.Size(750, 19);
            this.comboBox12.MinimumSize = new System.Drawing.Size(30, 19);
            this.comboBox12.Name = "comboBox12";
            this.comboBox12.SelectedIndex = -1;
            this.comboBox12.SelectedValue = null;
            this.comboBox12.Size = new System.Drawing.Size(180, 19);
            this.comboBox12.TabIndex = 1;
            this.comboBox12.ValueMember = null;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(18, 81);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(43, 14);
            this.label52.TabIndex = 2;
            this.label52.Text = "护士ID";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(304, 39);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(55, 14);
            this.label53.TabIndex = 0;
            this.label53.Text = "护士类别";
            // 
            // comboBox13
            // 
            this.comboBox13.DataSource = null;
            this.comboBox13.DisplayMember = null;
            this.comboBox13.DropDownHeight = 0;
            this.comboBox13.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboBox13.DropDownWidth = 0;
            this.comboBox13.DroppedDown = false;
            this.comboBox13.FormattingEnabled = true;
            this.comboBox13.IntegralHeight = true;
            this.comboBox13.Location = new System.Drawing.Point(95, 36);
            this.comboBox13.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox13.MaxDropDownItems = 0;
            this.comboBox13.MaximumSize = new System.Drawing.Size(750, 19);
            this.comboBox13.MinimumSize = new System.Drawing.Size(30, 19);
            this.comboBox13.Name = "comboBox13";
            this.comboBox13.SelectedIndex = -1;
            this.comboBox13.SelectedValue = null;
            this.comboBox13.Size = new System.Drawing.Size(180, 19);
            this.comboBox13.TabIndex = 1;
            this.comboBox13.ValueMember = null;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(18, 41);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(55, 14);
            this.label54.TabIndex = 0;
            this.label54.Text = "护理单元";
            // 
            // FormNuresPersonnel
            // 
            this.ClientSize = new System.Drawing.Size(906, 605);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormNuresPersonnel";
            this.Text = "人员管理";
            this.Load += new System.EventHandler(this.FormNuresPersonnel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        private void SevaData()
        {
        }
    }
}

