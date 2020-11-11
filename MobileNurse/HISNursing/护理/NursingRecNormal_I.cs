namespace HISPlus
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class NursingRecNormal_I : FormDo
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private IContainer components = null;
        private DataRow dr;
        private DataSet ds;
        private DataSet dsPatient = null;
        private GroupBox groupBox1;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
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
        private Label label31;
        private Label label32;
        private Label label33;
        private Label label34;
        private Label label35;
        private Label label36;
        private Label label37;
        private Label label38;
        private Label label39;
        private Label label4;
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
        private Label label5;
        private Label label50;
        private Label label51;
        private Label label52;
        private Label label53;
        private Label label54;
        private Label label55;
        private Label label56;
        private Label label57;
        private Label label58;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lblAge;
        private Label lblDeptName;
        private Label lblDiagnose;
        private Label lblDocNo;
        private Label lblGender;
        private Label lblName;
        private Panel panel1;
        private PatientDbI patientCom;
        private string patientId = string.Empty;
        private string s09 = string.Empty;
        private string s10 = string.Empty;
        private string s17 = string.Empty;
        private string s19 = string.Empty;
        private string s21 = string.Empty;
        private string s23 = string.Empty;
        private string s25 = string.Empty;
        private string s29 = string.Empty;
        private string s31 = string.Empty;
        private string s33 = string.Empty;
        private string s40 = string.Empty;
        private string s41 = string.Empty;
        private string s42 = string.Empty;
        private string s43 = string.Empty;
        private string s44 = string.Empty;
        private string s46 = string.Empty;
        private string s48 = string.Empty;
        private string s50 = string.Empty;
        private string s52 = string.Empty;
        private string sql = string.Empty;
        private GroupBox t;
        private TextBox t01;
        private HISPlus.UserControls.UcComboBox t02;
        private TextBox t03;
        private HISPlus.UserControls.UcComboBox t04;
        private TextBox t05;
        private TextBox t06;
        private TextBox t07;
        private MaskedTextBox t08;
        private CheckBox t09_1;
        private CheckBox t09_2;
        private CheckBox t10_1;
        private CheckBox t10_2;
        private CheckBox t10_3;
        private CheckBox t10_4;
        private CheckBox t10_5;
        private TextBox t11;
        private TextBox t12;
        private TextBox t13;
        private MaskedTextBox t14;
        private MaskedTextBox t15;
        private TextBox t16;
        private CheckBox t17_1;
        private CheckBox t17_2;
        private CheckBox t17_3;
        private CheckBox t17_4;
        private CheckBox t17_5;
        private TextBox t18;
        private CheckBox t19_1;
        private CheckBox t19_2;
        private CheckBox t19_3;
        private CheckBox t19_4;
        private CheckBox t19_5;
        private TextBox t20;
        private CheckBox t21_1;
        private CheckBox t21_2;
        private TextBox t22;
        private CheckBox t23_1;
        private CheckBox t23_2;
        private TextBox t24;
        private CheckBox t25_1;
        private CheckBox t25_2;
        private CheckBox t25_3;
        private TextBox t26;
        private TextBox t27;
        private TextBox t28;
        private CheckBox t29_1;
        private CheckBox t29_2;
        private TextBox t30;
        private CheckBox t31_1;
        private CheckBox t31_2;
        private CheckBox t31_3;
        private CheckBox t31_4;
        private TextBox t32;
        private CheckBox t33_1;
        private CheckBox t33_2;
        private CheckBox t33_3;
        private CheckBox t33_4;
        private TextBox t34;
        private HISPlus.UserControls.UcComboBox t35;
        private HISPlus.UserControls.UcComboBox t36;
        private HISPlus.UserControls.UcComboBox t37;
        private TextBox t38;
        private HISPlus.UserControls.UcComboBox t39;
        private CheckBox t40_1;
        private CheckBox t40_2;
        private CheckBox t41_1;
        private CheckBox t41_2;
        private CheckBox t42_1;
        private CheckBox t42_2;
        private CheckBox t43_1;
        private CheckBox t43_2;
        private CheckBox t44_1;
        private CheckBox t44_2;
        private TextBox t45;
        private CheckBox t46_1;
        private CheckBox t46_2;
        private TextBox t47;
        private CheckBox t48_1;
        private CheckBox t48_2;
        private CheckBox t48_3;
        private CheckBox t48_4;
        private TextBox t49;
        private CheckBox t50_1;
        private CheckBox t50_2;
        private CheckBox t50_3;
        private TextBox t51;
        private CheckBox t52_1;
        private CheckBox t52_2;
        private CheckBox t52_3;
        private CheckBox t52_4;
        private TextBox t53;
        private TextBox txtBedLabel;
        private string visitId = string.Empty;

        public NursingRecNormal_I()
        {
            base._id = "00032";
            base._guid = "C3F5DCF2-A401-4b35-A922-EF858B22852C";
            this.InitializeComponent();
            this.txtBedLabel.KeyDown += new KeyEventHandler(this.txtBedLabel_KeyDown);
            this.txtBedLabel.GotFocus += new EventHandler(this.imeCtrl_GotFocus);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.patientId.Length == 0)
            {
                GVars.Msg.Show("无记录");
            }
            else
            {
                this.ds = this.PatientData(this.patientId, this.visitId);
                this.GetControls(base.Controls);
                GVars.OracleAccess.Update(ref this.ds, "t1", this.sql);
            }
        }

        private void CheckControl(Control ct, string Rstr, string Xstr)
        {
            string name = ct.GetType().Name;
            if (((name != null) && (name == "CheckBox")) && ((((CheckBox) ct).Text.Trim() == Rstr) && ((CheckBox) ct).Name.Contains(Xstr)))
            {
                ((CheckBox) ct).Checked = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void GetControl(Control.ControlCollection ctc, string Rstr, string Xstr)
        {
            foreach (Control control in ctc)
            {
                this.CheckControl(control, Rstr, Xstr);
                if (control.HasChildren)
                {
                    this.GetControl(control.Controls, Rstr, Xstr);
                }
            }
        }

        private void GetControls(Control.ControlCollection ctc)
        {
            foreach (Control control in ctc)
            {
                this.TextControl(control);
                if (control.HasChildren)
                {
                    this.GetControls(control.Controls);
                }
            }
        }

        private void imeCtrl_GotFocus(object sender, EventArgs e)
        {
            IME.ChangeControlIme(base.ActiveControl.Handle);
        }

        private void initDisp()
        {
            this.lblName.Text = string.Empty;
            this.lblGender.Text = string.Empty;
            this.lblAge.Text = string.Empty;
            this.lblDeptName.Text = string.Empty;
            this.lblDocNo.Text = string.Empty;
            this.lblDiagnose.Text = string.Empty;
        }

        private void initFrmVal()
        {
            this.patientCom = new PatientDbI(GVars.OracleAccess);
        }

        private void InitializeComponent()
        {
            this.panel1 = new Panel();
            this.t = new GroupBox();
            this.t13 = new TextBox();
            this.t47 = new TextBox();
            this.t45 = new TextBox();
            this.t37 = new HISPlus.UserControls.UcComboBox();
            this.t36 = new HISPlus.UserControls.UcComboBox();
            this.t39 = new HISPlus.UserControls.UcComboBox();
            this.t35 = new HISPlus.UserControls.UcComboBox();
            this.t24 = new TextBox();
            this.t30 = new TextBox();
            this.t53 = new TextBox();
            this.t51 = new TextBox();
            this.t22 = new TextBox();
            this.t28 = new TextBox();
            this.t20 = new TextBox();
            this.t49 = new TextBox();
            this.t34 = new TextBox();
            this.t32 = new TextBox();
            this.t18 = new TextBox();
            this.t04 = new HISPlus.UserControls.UcComboBox();
            this.t16 = new TextBox();
            this.t27 = new TextBox();
            this.t38 = new TextBox();
            this.t15 = new MaskedTextBox();
            this.t14 = new MaskedTextBox();
            this.label29 = new Label();
            this.label27 = new Label();
            this.label26 = new Label();
            this.label25 = new Label();
            this.label24 = new Label();
            this.label23 = new Label();
            this.label22 = new Label();
            this.label34 = new Label();
            this.label40 = new Label();
            this.label32 = new Label();
            this.label52 = new Label();
            this.label51 = new Label();
            this.label50 = new Label();
            this.label55 = new Label();
            this.label54 = new Label();
            this.label49 = new Label();
            this.label33 = new Label();
            this.label39 = new Label();
            this.label58 = new Label();
            this.label57 = new Label();
            this.label31 = new Label();
            this.label38 = new Label();
            this.label37 = new Label();
            this.label36 = new Label();
            this.label35 = new Label();
            this.label30 = new Label();
            this.label47 = new Label();
            this.label46 = new Label();
            this.label45 = new Label();
            this.label44 = new Label();
            this.label43 = new Label();
            this.label53 = new Label();
            this.label48 = new Label();
            this.label56 = new Label();
            this.label42 = new Label();
            this.label41 = new Label();
            this.label28 = new Label();
            this.label21 = new Label();
            this.t12 = new TextBox();
            this.t11 = new TextBox();
            this.label20 = new Label();
            this.t25_3 = new CheckBox();
            this.t48_4 = new CheckBox();
            this.label19 = new Label();
            this.t33_4 = new CheckBox();
            this.t19_5 = new CheckBox();
            this.t52_4 = new CheckBox();
            this.t50_3 = new CheckBox();
            this.t31_4 = new CheckBox();
            this.label18 = new Label();
            this.t17_5 = new CheckBox();
            this.t19_4 = new CheckBox();
            this.t10_5 = new CheckBox();
            this.t17_4 = new CheckBox();
            this.t19_3 = new CheckBox();
            this.t23_2 = new CheckBox();
            this.t29_2 = new CheckBox();
            this.t48_3 = new CheckBox();
            this.t10_4 = new CheckBox();
            this.t52_3 = new CheckBox();
            this.t52_2 = new CheckBox();
            this.t50_2 = new CheckBox();
            this.t33_3 = new CheckBox();
            this.t21_2 = new CheckBox();
            this.t31_3 = new CheckBox();
            this.t25_2 = new CheckBox();
            this.t17_3 = new CheckBox();
            this.t19_2 = new CheckBox();
            this.t23_1 = new CheckBox();
            this.t29_1 = new CheckBox();
            this.t48_2 = new CheckBox();
            this.t10_3 = new CheckBox();
            this.t52_1 = new CheckBox();
            this.t50_1 = new CheckBox();
            this.t33_2 = new CheckBox();
            this.t21_1 = new CheckBox();
            this.t31_2 = new CheckBox();
            this.t41_2 = new CheckBox();
            this.t25_1 = new CheckBox();
            this.t46_2 = new CheckBox();
            this.t44_2 = new CheckBox();
            this.t40_2 = new CheckBox();
            this.t48_1 = new CheckBox();
            this.t17_2 = new CheckBox();
            this.t33_1 = new CheckBox();
            this.t19_1 = new CheckBox();
            this.t41_1 = new CheckBox();
            this.t46_1 = new CheckBox();
            this.t44_1 = new CheckBox();
            this.t31_1 = new CheckBox();
            this.t40_1 = new CheckBox();
            this.t43_2 = new CheckBox();
            this.t10_2 = new CheckBox();
            this.t43_1 = new CheckBox();
            this.t42_2 = new CheckBox();
            this.t42_1 = new CheckBox();
            this.t17_1 = new CheckBox();
            this.t10_1 = new CheckBox();
            this.t09_2 = new CheckBox();
            this.t09_1 = new CheckBox();
            this.label17 = new Label();
            this.label16 = new Label();
            this.t08 = new MaskedTextBox();
            this.t07 = new TextBox();
            this.label15 = new Label();
            this.label14 = new Label();
            this.t06 = new TextBox();
            this.t05 = new TextBox();
            this.t03 = new TextBox();
            this.t02 = new HISPlus.UserControls.UcComboBox();
            this.t26 = new TextBox();
            this.t01 = new TextBox();
            this.label13 = new Label();
            this.label12 = new Label();
            this.label11 = new Label();
            this.label10 = new Label();
            this.label9 = new Label();
            this.label8 = new Label();
            this.groupBox1 = new GroupBox();
            this.lblDiagnose = new Label();
            this.label2 = new Label();
            this.lblDocNo = new Label();
            this.label7 = new Label();
            this.lblDeptName = new Label();
            this.label5 = new Label();
            this.lblAge = new Label();
            this.label4 = new Label();
            this.lblGender = new Label();
            this.label3 = new Label();
            this.lblName = new Label();
            this.label6 = new Label();
            this.txtBedLabel = new TextBox();
            this.label1 = new Label();
            this.button1 = new Button();
            this.button2 = new Button();
            this.button3 = new Button();
            this.panel1.SuspendLayout();
            this.t.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = SystemColors.Control;
            this.panel1.BorderStyle = BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.t);
            this.panel1.Location = new Point(12, 0x33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x2d1, 0x195);
            this.panel1.TabIndex = 0;
            this.t.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.t.BackColor = SystemColors.ControlLightLight;
            this.t.Controls.Add(this.t13);
            this.t.Controls.Add(this.t47);
            this.t.Controls.Add(this.t45);
            this.t.Controls.Add(this.t37);
            this.t.Controls.Add(this.t36);
            this.t.Controls.Add(this.t39);
            this.t.Controls.Add(this.t35);
            this.t.Controls.Add(this.t24);
            this.t.Controls.Add(this.t30);
            this.t.Controls.Add(this.t53);
            this.t.Controls.Add(this.t51);
            this.t.Controls.Add(this.t22);
            this.t.Controls.Add(this.t28);
            this.t.Controls.Add(this.t20);
            this.t.Controls.Add(this.t49);
            this.t.Controls.Add(this.t34);
            this.t.Controls.Add(this.t32);
            this.t.Controls.Add(this.t18);
            this.t.Controls.Add(this.t04);
            this.t.Controls.Add(this.t16);
            this.t.Controls.Add(this.t27);
            this.t.Controls.Add(this.t38);
            this.t.Controls.Add(this.t15);
            this.t.Controls.Add(this.t14);
            this.t.Controls.Add(this.label29);
            this.t.Controls.Add(this.label27);
            this.t.Controls.Add(this.label26);
            this.t.Controls.Add(this.label25);
            this.t.Controls.Add(this.label24);
            this.t.Controls.Add(this.label23);
            this.t.Controls.Add(this.label22);
            this.t.Controls.Add(this.label34);
            this.t.Controls.Add(this.label40);
            this.t.Controls.Add(this.label32);
            this.t.Controls.Add(this.label52);
            this.t.Controls.Add(this.label51);
            this.t.Controls.Add(this.label50);
            this.t.Controls.Add(this.label55);
            this.t.Controls.Add(this.label54);
            this.t.Controls.Add(this.label49);
            this.t.Controls.Add(this.label33);
            this.t.Controls.Add(this.label39);
            this.t.Controls.Add(this.label58);
            this.t.Controls.Add(this.label57);
            this.t.Controls.Add(this.label31);
            this.t.Controls.Add(this.label38);
            this.t.Controls.Add(this.label37);
            this.t.Controls.Add(this.label36);
            this.t.Controls.Add(this.label35);
            this.t.Controls.Add(this.label30);
            this.t.Controls.Add(this.label47);
            this.t.Controls.Add(this.label46);
            this.t.Controls.Add(this.label45);
            this.t.Controls.Add(this.label44);
            this.t.Controls.Add(this.label43);
            this.t.Controls.Add(this.label53);
            this.t.Controls.Add(this.label48);
            this.t.Controls.Add(this.label56);
            this.t.Controls.Add(this.label42);
            this.t.Controls.Add(this.label41);
            this.t.Controls.Add(this.label28);
            this.t.Controls.Add(this.label21);
            this.t.Controls.Add(this.t12);
            this.t.Controls.Add(this.t11);
            this.t.Controls.Add(this.label20);
            this.t.Controls.Add(this.t25_3);
            this.t.Controls.Add(this.t48_4);
            this.t.Controls.Add(this.label19);
            this.t.Controls.Add(this.t33_4);
            this.t.Controls.Add(this.t19_5);
            this.t.Controls.Add(this.t52_4);
            this.t.Controls.Add(this.t50_3);
            this.t.Controls.Add(this.t31_4);
            this.t.Controls.Add(this.label18);
            this.t.Controls.Add(this.t17_5);
            this.t.Controls.Add(this.t19_4);
            this.t.Controls.Add(this.t10_5);
            this.t.Controls.Add(this.t17_4);
            this.t.Controls.Add(this.t19_3);
            this.t.Controls.Add(this.t23_2);
            this.t.Controls.Add(this.t29_2);
            this.t.Controls.Add(this.t48_3);
            this.t.Controls.Add(this.t10_4);
            this.t.Controls.Add(this.t52_3);
            this.t.Controls.Add(this.t52_2);
            this.t.Controls.Add(this.t50_2);
            this.t.Controls.Add(this.t33_3);
            this.t.Controls.Add(this.t21_2);
            this.t.Controls.Add(this.t31_3);
            this.t.Controls.Add(this.t25_2);
            this.t.Controls.Add(this.t17_3);
            this.t.Controls.Add(this.t19_2);
            this.t.Controls.Add(this.t23_1);
            this.t.Controls.Add(this.t29_1);
            this.t.Controls.Add(this.t48_2);
            this.t.Controls.Add(this.t10_3);
            this.t.Controls.Add(this.t52_1);
            this.t.Controls.Add(this.t50_1);
            this.t.Controls.Add(this.t33_2);
            this.t.Controls.Add(this.t21_1);
            this.t.Controls.Add(this.t31_2);
            this.t.Controls.Add(this.t41_2);
            this.t.Controls.Add(this.t25_1);
            this.t.Controls.Add(this.t46_2);
            this.t.Controls.Add(this.t44_2);
            this.t.Controls.Add(this.t40_2);
            this.t.Controls.Add(this.t48_1);
            this.t.Controls.Add(this.t17_2);
            this.t.Controls.Add(this.t33_1);
            this.t.Controls.Add(this.t19_1);
            this.t.Controls.Add(this.t41_1);
            this.t.Controls.Add(this.t46_1);
            this.t.Controls.Add(this.t44_1);
            this.t.Controls.Add(this.t31_1);
            this.t.Controls.Add(this.t40_1);
            this.t.Controls.Add(this.t43_2);
            this.t.Controls.Add(this.t10_2);
            this.t.Controls.Add(this.t43_1);
            this.t.Controls.Add(this.t42_2);
            this.t.Controls.Add(this.t42_1);
            this.t.Controls.Add(this.t17_1);
            this.t.Controls.Add(this.t10_1);
            this.t.Controls.Add(this.t09_2);
            this.t.Controls.Add(this.t09_1);
            this.t.Controls.Add(this.label17);
            this.t.Controls.Add(this.label16);
            this.t.Controls.Add(this.t08);
            this.t.Controls.Add(this.t07);
            this.t.Controls.Add(this.label15);
            this.t.Controls.Add(this.label14);
            this.t.Controls.Add(this.t06);
            this.t.Controls.Add(this.t05);
            this.t.Controls.Add(this.t03);
            this.t.Controls.Add(this.t02);
            this.t.Controls.Add(this.t26);
            this.t.Controls.Add(this.t01);
            this.t.Controls.Add(this.label13);
            this.t.Controls.Add(this.label12);
            this.t.Controls.Add(this.label11);
            this.t.Controls.Add(this.label10);
            this.t.Controls.Add(this.label9);
            this.t.Controls.Add(this.label8);
            this.t.Location = new Point(3, 0);
            this.t.Name = "t";
            this.t.Size = new Size(710, 0x28b);
            this.t.TabIndex = 0;
            this.t.TabStop = false;
            this.t13.Location = new Point(0x53, 0xca);
            this.t13.Name = "t13";
            this.t13.Size = new Size(0x27, 0x15);
            this.t13.TabIndex = 0x1c;
            this.t47.Location = new Point(0xf4, 0x21b);
            this.t47.Name = "t47";
            this.t47.Size = new Size(0x177, 0x15);
            this.t47.TabIndex = 0x1b;
            this.t45.Location = new Point(0xf4, 0x203);
            this.t45.Name = "t45";
            this.t45.Size = new Size(0x177, 0x15);
            this.t45.TabIndex = 0x1b;
            this.t37.FormattingEnabled = true;
            this.t37.Location = new Point(0x17e, 0x1b2);
            this.t37.Name = "t37";
            this.t37.Size = new Size(0x22, 20);
            this.t37.TabIndex = 0x1a;
            this.t36.FormattingEnabled = true;
            this.t36.Location = new Point(0x112, 0x1b2);
            this.t36.Name = "t36";
            this.t36.Size = new Size(0x39, 20);
            this.t36.TabIndex = 0x1a;
            this.t39.FormattingEnabled = true;
            this.t39.Location = new Point(0x232, 0x1b2);
            this.t39.Name = "t39";
            this.t39.Size = new Size(0x39, 20);
            this.t39.TabIndex = 0x1a;
            this.t35.FormattingEnabled = true;
            this.t35.Location = new Point(0xa6, 0x1b2);
            this.t35.Name = "t35";
            this.t35.Size = new Size(0x39, 20);
            this.t35.TabIndex = 0x1a;
            this.t24.Location = new Point(300, 0x132);
            this.t24.Name = "t24";
            this.t24.Size = new Size(0x13f, 0x15);
            this.t24.TabIndex = 0x19;
            this.t30.Location = new Point(300, 0x164);
            this.t30.Name = "t30";
            this.t30.Size = new Size(0x13f, 0x15);
            this.t30.TabIndex = 0x19;
            this.t53.Location = new Point(0x18b, 0x263);
            this.t53.Name = "t53";
            this.t53.Size = new Size(0xe0, 0x15);
            this.t53.TabIndex = 0x19;
            this.t51.Location = new Point(0x18b, 0x24b);
            this.t51.Name = "t51";
            this.t51.Size = new Size(0xe0, 0x15);
            this.t51.TabIndex = 0x19;
            this.t22.Location = new Point(300, 0x119);
            this.t22.Name = "t22";
            this.t22.Size = new Size(0x13f, 0x15);
            this.t22.TabIndex = 0x19;
            this.t28.Location = new Point(0x1df, 0x14b);
            this.t28.Name = "t28";
            this.t28.Size = new Size(140, 0x15);
            this.t28.TabIndex = 0x19;
            this.t20.Location = new Point(0x189, 0x100);
            this.t20.Name = "t20";
            this.t20.Size = new Size(0xe2, 0x15);
            this.t20.TabIndex = 0x19;
            this.t49.Location = new Point(0x18b, 0x233);
            this.t49.Name = "t49";
            this.t49.Size = new Size(0xe0, 0x15);
            this.t49.TabIndex = 0x19;
            this.t34.Location = new Point(0x189, 0x196);
            this.t34.Name = "t34";
            this.t34.Size = new Size(0xe2, 0x15);
            this.t34.TabIndex = 0x19;
            this.t32.Location = new Point(0x189, 0x17d);
            this.t32.Name = "t32";
            this.t32.Size = new Size(0xe2, 0x15);
            this.t32.TabIndex = 0x19;
            this.t18.Location = new Point(0x189, 0xe7);
            this.t18.Name = "t18";
            this.t18.Size = new Size(0xe2, 0x15);
            this.t18.TabIndex = 0x19;
            this.t04.FormattingEnabled = true;
            this.t04.Location = new Point(0x15f, 0x12);
            this.t04.Name = "t04";
            this.t04.Size = new Size(0x69, 20);
            this.t04.TabIndex = 0x18;
            this.t16.Location = new Point(0x1de, 0xca);
            this.t16.Name = "t16";
            this.t16.Size = new Size(0x4c, 0x15);
            this.t16.TabIndex = 0x17;
            this.t27.Location = new Point(0x166, 0x14b);
            this.t27.Name = "t27";
            this.t27.Size = new Size(0x24, 0x15);
            this.t27.TabIndex = 0x16;
            this.t38.Location = new Point(0x1bb, 0x1b1);
            this.t38.Name = "t38";
            this.t38.Size = new Size(0x20, 0x15);
            this.t38.TabIndex = 0x16;
            this.t15.Location = new Point(0x158, 0xcb);
            this.t15.Mask = "999";
            this.t15.Name = "t15";
            this.t15.Size = new Size(0x23, 0x15);
            this.t15.TabIndex = 0x15;
            this.t14.Location = new Point(0xde, 0xcb);
            this.t14.Mask = "999";
            this.t14.Name = "t14";
            this.t14.Size = new Size(0x23, 0x15);
            this.t14.TabIndex = 0x15;
            this.label29.AutoSize = true;
            this.label29.Location = new Point(560, 0xd4);
            this.label29.Name = "label29";
            this.label29.Size = new Size(0x1d, 12);
            this.label29.TabIndex = 20;
            this.label29.Text = "mmHg";
            this.label27.AutoSize = true;
            this.label27.Location = new Point(0x1bb, 0xd3);
            this.label27.Name = "label27";
            this.label27.Size = new Size(0x1d, 12);
            this.label27.TabIndex = 20;
            this.label27.Text = "血压";
            this.label26.AutoSize = true;
            this.label26.Location = new Point(0x17d, 0xd3);
            this.label26.Name = "label26";
            this.label26.Size = new Size(0x23, 12);
            this.label26.TabIndex = 20;
            this.label26.Text = "次/分";
            this.label25.AutoSize = true;
            this.label25.Location = new Point(0x13b, 0xd3);
            this.label25.Name = "label25";
            this.label25.Size = new Size(0x1d, 12);
            this.label25.TabIndex = 20;
            this.label25.Text = "呼吸";
            this.label24.AutoSize = true;
            this.label24.Location = new Point(260, 0xd3);
            this.label24.Name = "label24";
            this.label24.Size = new Size(0x23, 12);
            this.label24.TabIndex = 20;
            this.label24.Text = "次/分";
            this.label23.AutoSize = true;
            this.label23.Location = new Point(0xa4, 0xd3);
            this.label23.Name = "label23";
            this.label23.Size = new Size(0x3b, 12);
            this.label23.TabIndex = 20;
            this.label23.Text = "脉搏/心率";
            this.label22.AutoSize = true;
            this.label22.Location = new Point(120, 0xd3);
            this.label22.Name = "label22";
            this.label22.Size = new Size(0x11, 12);
            this.label22.TabIndex = 20;
            this.label22.Text = "℃";
            this.label34.AutoSize = true;
            this.label34.Location = new Point(0xec, 0x13a);
            this.label34.Name = "label34";
            this.label34.Size = new Size(0x41, 12);
            this.label34.TabIndex = 20;
            this.label34.Text = "异常的描述";
            this.label40.AutoSize = true;
            this.label40.Location = new Point(0xec, 0x16c);
            this.label40.Name = "label40";
            this.label40.Size = new Size(0x41, 12);
            this.label40.TabIndex = 20;
            this.label40.Text = "异常的描述";
            this.label32.AutoSize = true;
            this.label32.Location = new Point(0xec, 0x121);
            this.label32.Name = "label32";
            this.label32.Size = new Size(0x41, 12);
            this.label32.TabIndex = 20;
            this.label32.Text = "异常的描述";
            this.label52.AutoSize = true;
            this.label52.Location = new Point(0x129, 0x1ef);
            this.label52.Name = "label52";
            this.label52.Size = new Size(0x1d, 12);
            this.label52.TabIndex = 20;
            this.label52.Text = "睡眠";
            this.label51.AutoSize = true;
            this.label51.Location = new Point(0x7d, 0x1ed);
            this.label51.Name = "label51";
            this.label51.Size = new Size(0x1d, 12);
            this.label51.TabIndex = 20;
            this.label51.Text = "饮食";
            this.label50.AutoSize = true;
            this.label50.Location = new Point(0x129, 0x1d9);
            this.label50.Name = "label50";
            this.label50.Size = new Size(0x1d, 12);
            this.label50.TabIndex = 20;
            this.label50.Text = "饮酒";
            this.label55.AutoSize = true;
            this.label55.Location = new Point(0x7e, 0x220);
            this.label55.Name = "label55";
            this.label55.Size = new Size(0x1d, 12);
            this.label55.TabIndex = 20;
            this.label55.Text = "食物";
            this.label54.AutoSize = true;
            this.label54.Location = new Point(0x7e, 520);
            this.label54.Name = "label54";
            this.label54.Size = new Size(0x1d, 12);
            this.label54.TabIndex = 20;
            this.label54.Text = "药物";
            this.label49.AutoSize = true;
            this.label49.Location = new Point(0x7e, 0x1d8);
            this.label49.Name = "label49";
            this.label49.Size = new Size(0x1d, 12);
            this.label49.TabIndex = 20;
            this.label49.Text = "吸烟";
            this.label33.AutoSize = true;
            this.label33.Location = new Point(0x35, 0x13a);
            this.label33.Name = "label33";
            this.label33.Size = new Size(0x23, 12);
            this.label33.TabIndex = 20;
            this.label33.Text = "听力:";
            this.label39.AutoSize = true;
            this.label39.Location = new Point(0x35, 0x16c);
            this.label39.Name = "label39";
            this.label39.Size = new Size(0x3b, 12);
            this.label39.TabIndex = 20;
            this.label39.Text = "运动能力:";
            this.label58.AutoSize = true;
            this.label58.Location = new Point(0x35, 0x267);
            this.label58.Name = "label58";
            this.label58.Size = new Size(0x23, 12);
            this.label58.TabIndex = 20;
            this.label58.Text = "饮食:";
            this.label57.AutoSize = true;
            this.label57.Location = new Point(0x35, 0x24f);
            this.label57.Name = "label57";
            this.label57.Size = new Size(0x23, 12);
            this.label57.TabIndex = 20;
            this.label57.Text = "卧位:";
            this.label31.AutoSize = true;
            this.label31.Location = new Point(0x35, 0x121);
            this.label31.Name = "label31";
            this.label31.Size = new Size(0x3b, 12);
            this.label31.TabIndex = 20;
            this.label31.Text = "视力情况:";
            this.label38.AutoSize = true;
            this.label38.Location = new Point(0x189, 340);
            this.label38.Name = "label38";
            this.label38.Size = new Size(0x1d, 12);
            this.label38.TabIndex = 20;
            this.label38.Text = "公分";
            this.label37.AutoSize = true;
            this.label37.Location = new Point(0x14c, 340);
            this.label37.Name = "label37";
            this.label37.Size = new Size(0x1d, 12);
            this.label37.TabIndex = 20;
            this.label37.Text = "大小";
            this.label36.AutoSize = true;
            this.label36.Location = new Point(0xec, 0x151);
            this.label36.Name = "label36";
            this.label36.Size = new Size(0x1d, 12);
            this.label36.TabIndex = 20;
            this.label36.Text = "部位";
            this.label35.AutoSize = true;
            this.label35.Location = new Point(0x35, 340);
            this.label35.Name = "label35";
            this.label35.Size = new Size(0x23, 12);
            this.label35.TabIndex = 20;
            this.label35.Text = "皮肤:";
            this.label30.AutoSize = true;
            this.label30.Location = new Point(0x35, 0x109);
            this.label30.Name = "label30";
            this.label30.Size = new Size(0x3b, 12);
            this.label30.TabIndex = 20;
            this.label30.Text = "意识情况:";
            this.label47.AutoSize = true;
            this.label47.Location = new Point(0x1da, 0x1b9);
            this.label47.Name = "label47";
            this.label47.Size = new Size(0x59, 12);
            this.label47.TabIndex = 20;
            this.label47.Text = "医疗费支付形式";
            this.label46.AutoSize = true;
            this.label46.Location = new Point(0x1a0, 0x1b9);
            this.label46.Name = "label46";
            this.label46.Size = new Size(0x1d, 12);
            this.label46.TabIndex = 20;
            this.label46.Text = "子女";
            this.label45.AutoSize = true;
            this.label45.Location = new Point(0x14b, 0x1ba);
            this.label45.Name = "label45";
            this.label45.Size = new Size(0x35, 12);
            this.label45.TabIndex = 20;
            this.label45.Text = "婚姻状况";
            this.label44.AutoSize = true;
            this.label44.Location = new Point(0xe0, 0x1b9);
            this.label44.Name = "label44";
            this.label44.Size = new Size(0x35, 12);
            this.label44.TabIndex = 20;
            this.label44.Text = "文化程度";
            this.label43.AutoSize = true;
            this.label43.Location = new Point(0x35, 0x1b9);
            this.label43.Name = "label43";
            this.label43.Size = new Size(0x71, 12);
            this.label43.TabIndex = 20;
            this.label43.Text = "家庭社会情况: 职业";
            this.label53.AutoSize = true;
            this.label53.Location = new Point(0x35, 0x207);
            this.label53.Name = "label53";
            this.label53.Size = new Size(0x2f, 12);
            this.label53.TabIndex = 20;
            this.label53.Text = "过敏史:";
            this.label48.AutoSize = true;
            this.label48.Location = new Point(0x35, 0x1d7);
            this.label48.Name = "label48";
            this.label48.Size = new Size(0x3b, 12);
            this.label48.TabIndex = 20;
            this.label48.Text = "生活习惯:";
            this.label56.AutoSize = true;
            this.label56.Location = new Point(0x35, 0x239);
            this.label56.Name = "label56";
            this.label56.Size = new Size(0x3b, 12);
            this.label56.TabIndex = 20;
            this.label56.Text = "护理级别:";
            this.label42.AutoSize = true;
            this.label42.Location = new Point(0x35, 0x19c);
            this.label42.Name = "label42";
            this.label42.Size = new Size(0x3b, 12);
            this.label42.TabIndex = 20;
            this.label42.Text = "自理能力:";
            this.label41.AutoSize = true;
            this.label41.Location = new Point(0x35, 390);
            this.label41.Name = "label41";
            this.label41.Size = new Size(0x3b, 12);
            this.label41.TabIndex = 20;
            this.label41.Text = "语言能力:";
            this.label28.AutoSize = true;
            this.label28.Location = new Point(0x35, 240);
            this.label28.Name = "label28";
            this.label28.Size = new Size(0x3b, 12);
            this.label28.TabIndex = 20;
            this.label28.Text = "精神状态:";
            this.label21.AutoSize = true;
            this.label21.Location = new Point(0x35, 0xd3);
            this.label21.Name = "label21";
            this.label21.Size = new Size(0x1d, 12);
            this.label21.TabIndex = 20;
            this.label21.Text = "体温";
            this.t12.Location = new Point(0x53, 0x85);
            this.t12.Multiline = true;
            this.t12.Name = "t12";
            this.t12.Size = new Size(0x250, 50);
            this.t12.TabIndex = 0x13;
            this.t11.Location = new Point(0x53, 0x6c);
            this.t11.Name = "t11";
            this.t11.Size = new Size(0x250, 0x15);
            this.t11.TabIndex = 0x13;
            this.label20.AutoSize = true;
            this.label20.Font = new Font("宋体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label20.Location = new Point(12, 0xbb);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x35, 14);
            this.label20.TabIndex = 0x12;
            this.label20.Text = "查体: ";
            this.t25_3.AutoSize = true;
            this.t25_3.Location = new Point(0x1b2, 0x151);
            this.t25_3.Name = "t25_3";
            this.t25_3.Size = new Size(0x30, 0x10);
            this.t25_3.TabIndex = 0x11;
            this.t25_3.Text = "其他";
            this.t25_3.UseVisualStyleBackColor = true;
            this.t48_4.AutoSize = true;
            this.t48_4.Location = new Point(0x15a, 0x238);
            this.t48_4.Name = "t48_4";
            this.t48_4.Size = new Size(0x30, 0x10);
            this.t48_4.TabIndex = 0x11;
            this.t48_4.Text = "其他";
            this.t48_4.UseVisualStyleBackColor = true;
            this.t48_4.CheckedChanged += new EventHandler(this.t48_4_CheckedChanged);
            this.label19.AutoSize = true;
            this.label19.Location = new Point(6, 0x8e);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0x47, 12);
            this.label19.TabIndex = 0x12;
            this.label19.Text = "病史与主诉:";
            this.t33_4.AutoSize = true;
            this.t33_4.Location = new Point(0x15a, 0x19b);
            this.t33_4.Name = "t33_4";
            this.t33_4.Size = new Size(0x30, 0x10);
            this.t33_4.TabIndex = 0x11;
            this.t33_4.Text = "其他";
            this.t33_4.UseVisualStyleBackColor = true;
            this.t33_4.CheckedChanged += new EventHandler(this.t33_4_CheckedChanged);
            this.t19_5.AutoSize = true;
            this.t19_5.Location = new Point(0x15a, 0x105);
            this.t19_5.Name = "t19_5";
            this.t19_5.Size = new Size(0x30, 0x10);
            this.t19_5.TabIndex = 0x11;
            this.t19_5.Text = "其他";
            this.t19_5.UseVisualStyleBackColor = true;
            this.t19_5.CheckedChanged += new EventHandler(this.t19_5_CheckedChanged);
            this.t52_4.AutoSize = true;
            this.t52_4.Location = new Point(0x15a, 0x266);
            this.t52_4.Name = "t52_4";
            this.t52_4.Size = new Size(0x30, 0x10);
            this.t52_4.TabIndex = 0x11;
            this.t52_4.Text = "其他";
            this.t52_4.UseVisualStyleBackColor = true;
            this.t52_4.CheckedChanged += new EventHandler(this.t52_4_CheckedChanged);
            this.t50_3.AutoSize = true;
            this.t50_3.Location = new Point(0x15a, 590);
            this.t50_3.Name = "t50_3";
            this.t50_3.Size = new Size(0x30, 0x10);
            this.t50_3.TabIndex = 0x11;
            this.t50_3.Text = "其他";
            this.t50_3.UseVisualStyleBackColor = true;
            this.t50_3.CheckedChanged += new EventHandler(this.t50_3_CheckedChanged);
            this.t31_4.AutoSize = true;
            this.t31_4.Location = new Point(0x15a, 0x182);
            this.t31_4.Name = "t31_4";
            this.t31_4.Size = new Size(0x30, 0x10);
            this.t31_4.TabIndex = 0x11;
            this.t31_4.Text = "其他";
            this.t31_4.UseVisualStyleBackColor = true;
            this.t31_4.CheckedChanged += new EventHandler(this.t31_4_CheckedChanged);
            this.label18.AutoSize = true;
            this.label18.Location = new Point(6, 0x74);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x47, 12);
            this.label18.TabIndex = 0x12;
            this.label18.Text = "病史与主诉:";
            this.t17_5.AutoSize = true;
            this.t17_5.Location = new Point(0x15a, 0xec);
            this.t17_5.Name = "t17_5";
            this.t17_5.Size = new Size(0x30, 0x10);
            this.t17_5.TabIndex = 0x11;
            this.t17_5.Text = "其他";
            this.t17_5.UseVisualStyleBackColor = true;
            this.t17_5.CheckedChanged += new EventHandler(this.t17_5_CheckedChanged);
            this.t19_4.AutoSize = true;
            this.t19_4.Location = new Point(0x124, 0x105);
            this.t19_4.Name = "t19_4";
            this.t19_4.Size = new Size(0x30, 0x10);
            this.t19_4.TabIndex = 0x11;
            this.t19_4.Text = "昏迷";
            this.t19_4.UseVisualStyleBackColor = true;
            this.t19_4.CheckedChanged += new EventHandler(this.t19_4_CheckedChanged);
            this.t10_5.AutoSize = true;
            this.t10_5.Location = new Point(0x239, 0x58);
            this.t10_5.Name = "t10_5";
            this.t10_5.Size = new Size(0x30, 0x10);
            this.t10_5.TabIndex = 0x11;
            this.t10_5.Text = "平车";
            this.t10_5.UseVisualStyleBackColor = true;
            this.t10_5.CheckedChanged += new EventHandler(this.t10_5_CheckedChanged);
            this.t17_4.AutoSize = true;
            this.t17_4.Location = new Point(0x124, 0xec);
            this.t17_4.Name = "t17_4";
            this.t17_4.Size = new Size(0x30, 0x10);
            this.t17_4.TabIndex = 0x11;
            this.t17_4.Text = "恐惧";
            this.t17_4.UseVisualStyleBackColor = true;
            this.t17_4.CheckedChanged += new EventHandler(this.t17_4_CheckedChanged);
            this.t19_3.AutoSize = true;
            this.t19_3.Location = new Point(0xee, 0x105);
            this.t19_3.Name = "t19_3";
            this.t19_3.Size = new Size(0x30, 0x10);
            this.t19_3.TabIndex = 0x11;
            this.t19_3.Text = "昏睡";
            this.t19_3.UseVisualStyleBackColor = true;
            this.t19_3.CheckedChanged += new EventHandler(this.t19_3_CheckedChanged);
            this.t23_2.AutoSize = true;
            this.t23_2.Location = new Point(0xb8, 0x138);
            this.t23_2.Name = "t23_2";
            this.t23_2.Size = new Size(0x36, 0x10);
            this.t23_2.TabIndex = 0x11;
            this.t23_2.Text = "异常:";
            this.t23_2.UseVisualStyleBackColor = true;
            this.t23_2.CheckedChanged += new EventHandler(this.t23_2_CheckedChanged);
            this.t29_2.AutoSize = true;
            this.t29_2.Location = new Point(0xb8, 0x16a);
            this.t29_2.Name = "t29_2";
            this.t29_2.Size = new Size(0x36, 0x10);
            this.t29_2.TabIndex = 0x11;
            this.t29_2.Text = "异常:";
            this.t29_2.UseVisualStyleBackColor = true;
            this.t29_2.CheckedChanged += new EventHandler(this.t29_2_CheckedChanged);
            this.t48_3.AutoSize = true;
            this.t48_3.Location = new Point(0x110, 0x238);
            this.t48_3.Name = "t48_3";
            this.t48_3.Size = new Size(0x48, 0x10);
            this.t48_3.TabIndex = 0x11;
            this.t48_3.Text = "三级护理";
            this.t48_3.UseVisualStyleBackColor = true;
            this.t48_3.CheckedChanged += new EventHandler(this.t48_3_CheckedChanged);
            this.t10_4.AutoSize = true;
            this.t10_4.Location = new Point(0x203, 0x58);
            this.t10_4.Name = "t10_4";
            this.t10_4.Size = new Size(0x30, 0x10);
            this.t10_4.TabIndex = 0x11;
            this.t10_4.Text = "轮椅";
            this.t10_4.UseVisualStyleBackColor = true;
            this.t10_4.CheckedChanged += new EventHandler(this.t10_4_CheckedChanged);
            this.t52_3.AutoSize = true;
            this.t52_3.Location = new Point(0x10f, 0x267);
            this.t52_3.Name = "t52_3";
            this.t52_3.Size = new Size(0x30, 0x10);
            this.t52_3.TabIndex = 0x11;
            this.t52_3.Text = "流食";
            this.t52_3.UseVisualStyleBackColor = true;
            this.t52_3.CheckedChanged += new EventHandler(this.t52_3_CheckedChanged);
            this.t52_2.AutoSize = true;
            this.t52_2.Location = new Point(0xca, 0x267);
            this.t52_2.Name = "t52_2";
            this.t52_2.Size = new Size(60, 0x10);
            this.t52_2.TabIndex = 0x11;
            this.t52_2.Text = "半流食";
            this.t52_2.UseVisualStyleBackColor = true;
            this.t52_2.CheckedChanged += new EventHandler(this.t52_2_CheckedChanged);
            this.t50_2.AutoSize = true;
            this.t50_2.Location = new Point(0xca, 0x24f);
            this.t50_2.Name = "t50_2";
            this.t50_2.Size = new Size(60, 0x10);
            this.t50_2.TabIndex = 0x11;
            this.t50_2.Text = "半卧位";
            this.t50_2.UseVisualStyleBackColor = true;
            this.t50_2.CheckedChanged += new EventHandler(this.t50_2_CheckedChanged);
            this.t33_3.AutoSize = true;
            this.t33_3.Location = new Point(0x110, 0x19b);
            this.t33_3.Name = "t33_3";
            this.t33_3.Size = new Size(0x48, 0x10);
            this.t33_3.TabIndex = 0x11;
            this.t33_3.Text = "完全依赖";
            this.t33_3.UseVisualStyleBackColor = true;
            this.t33_3.CheckedChanged += new EventHandler(this.t33_3_CheckedChanged);
            this.t21_2.AutoSize = true;
            this.t21_2.Location = new Point(0xb8, 0x11f);
            this.t21_2.Name = "t21_2";
            this.t21_2.Size = new Size(0x36, 0x10);
            this.t21_2.TabIndex = 0x11;
            this.t21_2.Text = "异常:";
            this.t21_2.UseVisualStyleBackColor = true;
            this.t21_2.CheckedChanged += new EventHandler(this.t21_2_CheckedChanged);
            this.t31_3.AutoSize = true;
            this.t31_3.Location = new Point(0x110, 0x182);
            this.t31_3.Name = "t31_3";
            this.t31_3.Size = new Size(0x30, 0x10);
            this.t31_3.TabIndex = 0x11;
            this.t31_3.Text = "失语";
            this.t31_3.UseVisualStyleBackColor = true;
            this.t31_3.CheckedChanged += new EventHandler(this.t31_3_CheckedChanged);
            this.t25_2.AutoSize = true;
            this.t25_2.Location = new Point(0xb8, 0x150);
            this.t25_2.Name = "t25_2";
            this.t25_2.Size = new Size(0x36, 0x10);
            this.t25_2.TabIndex = 0x11;
            this.t25_2.Text = "破损:";
            this.t25_2.UseVisualStyleBackColor = true;
            this.t25_2.CheckedChanged += new EventHandler(this.t25_2_CheckedChanged);
            this.t17_3.AutoSize = true;
            this.t17_3.Location = new Point(0xee, 0xec);
            this.t17_3.Name = "t17_3";
            this.t17_3.Size = new Size(0x30, 0x10);
            this.t17_3.TabIndex = 0x11;
            this.t17_3.Text = "紧张";
            this.t17_3.UseVisualStyleBackColor = true;
            this.t17_3.CheckedChanged += new EventHandler(this.t17_3_CheckedChanged);
            this.t19_2.AutoSize = true;
            this.t19_2.Location = new Point(0xb8, 0x105);
            this.t19_2.Name = "t19_2";
            this.t19_2.Size = new Size(0x30, 0x10);
            this.t19_2.TabIndex = 0x11;
            this.t19_2.Text = "嗜睡";
            this.t19_2.UseVisualStyleBackColor = true;
            this.t19_2.CheckedChanged += new EventHandler(this.t19_2_CheckedChanged);
            this.t23_1.AutoSize = true;
            this.t23_1.Location = new Point(130, 0x138);
            this.t23_1.Name = "t23_1";
            this.t23_1.Size = new Size(0x30, 0x10);
            this.t23_1.TabIndex = 0x11;
            this.t23_1.Text = "正常";
            this.t23_1.UseVisualStyleBackColor = true;
            this.t23_1.CheckedChanged += new EventHandler(this.t23_1_CheckedChanged);
            this.t29_1.AutoSize = true;
            this.t29_1.Location = new Point(130, 0x16a);
            this.t29_1.Name = "t29_1";
            this.t29_1.Size = new Size(0x30, 0x10);
            this.t29_1.TabIndex = 0x11;
            this.t29_1.Text = "正常";
            this.t29_1.UseVisualStyleBackColor = true;
            this.t29_1.CheckedChanged += new EventHandler(this.t29_1_CheckedChanged);
            this.t48_2.AutoSize = true;
            this.t48_2.Location = new Point(0xca, 0x238);
            this.t48_2.Name = "t48_2";
            this.t48_2.Size = new Size(0x48, 0x10);
            this.t48_2.TabIndex = 0x11;
            this.t48_2.Text = "二级护理";
            this.t48_2.UseVisualStyleBackColor = true;
            this.t48_2.CheckedChanged += new EventHandler(this.t48_2_CheckedChanged);
            this.t10_3.AutoSize = true;
            this.t10_3.Location = new Point(0x1cd, 0x58);
            this.t10_3.Name = "t10_3";
            this.t10_3.Size = new Size(0x30, 0x10);
            this.t10_3.TabIndex = 0x11;
            this.t10_3.Text = "步行";
            this.t10_3.UseVisualStyleBackColor = true;
            this.t10_3.CheckedChanged += new EventHandler(this.t10_3_CheckedChanged);
            this.t52_1.AutoSize = true;
            this.t52_1.Location = new Point(130, 0x267);
            this.t52_1.Name = "t52_1";
            this.t52_1.Size = new Size(0x30, 0x10);
            this.t52_1.TabIndex = 0x11;
            this.t52_1.Text = "普食";
            this.t52_1.UseVisualStyleBackColor = true;
            this.t52_1.CheckedChanged += new EventHandler(this.t52_1_CheckedChanged);
            this.t50_1.AutoSize = true;
            this.t50_1.Location = new Point(130, 0x24f);
            this.t50_1.Name = "t50_1";
            this.t50_1.Size = new Size(60, 0x10);
            this.t50_1.TabIndex = 0x11;
            this.t50_1.Text = "平卧位";
            this.t50_1.UseVisualStyleBackColor = true;
            this.t50_1.CheckedChanged += new EventHandler(this.t50_1_CheckedChanged);
            this.t33_2.AutoSize = true;
            this.t33_2.Location = new Point(0xca, 0x19b);
            this.t33_2.Name = "t33_2";
            this.t33_2.Size = new Size(0x48, 0x10);
            this.t33_2.TabIndex = 0x11;
            this.t33_2.Text = "部分自理";
            this.t33_2.UseVisualStyleBackColor = true;
            this.t33_2.CheckedChanged += new EventHandler(this.t33_2_CheckedChanged);
            this.t21_1.AutoSize = true;
            this.t21_1.Location = new Point(130, 0x11f);
            this.t21_1.Name = "t21_1";
            this.t21_1.Size = new Size(0x30, 0x10);
            this.t21_1.TabIndex = 0x11;
            this.t21_1.Text = "正常";
            this.t21_1.UseVisualStyleBackColor = true;
            this.t21_1.CheckedChanged += new EventHandler(this.t21_1_CheckedChanged);
            this.t31_2.AutoSize = true;
            this.t31_2.Location = new Point(0xca, 0x182);
            this.t31_2.Name = "t31_2";
            this.t31_2.Size = new Size(0x48, 0x10);
            this.t31_2.TabIndex = 0x11;
            this.t31_2.Text = "含糊不清";
            this.t31_2.UseVisualStyleBackColor = true;
            this.t31_2.CheckedChanged += new EventHandler(this.t31_2_CheckedChanged);
            this.t41_2.AutoSize = true;
            this.t41_2.Location = new Point(0x180, 0x1d7);
            this.t41_2.Name = "t41_2";
            this.t41_2.Size = new Size(0x24, 0x10);
            this.t41_2.TabIndex = 0x11;
            this.t41_2.Text = "有";
            this.t41_2.UseVisualStyleBackColor = true;
            this.t41_2.CheckedChanged += new EventHandler(this.t41_2_CheckedChanged);
            this.t25_1.AutoSize = true;
            this.t25_1.Location = new Point(130, 0x150);
            this.t25_1.Name = "t25_1";
            this.t25_1.Size = new Size(0x30, 0x10);
            this.t25_1.TabIndex = 0x11;
            this.t25_1.Text = "完整";
            this.t25_1.UseVisualStyleBackColor = true;
            this.t25_1.CheckedChanged += new EventHandler(this.t25_1_CheckedChanged);
            this.t46_2.AutoSize = true;
            this.t46_2.Location = new Point(0xd4, 0x220);
            this.t46_2.Name = "t46_2";
            this.t46_2.Size = new Size(0x24, 0x10);
            this.t46_2.TabIndex = 0x11;
            this.t46_2.Text = "有";
            this.t46_2.UseVisualStyleBackColor = true;
            this.t46_2.CheckedChanged += new EventHandler(this.t46_2_CheckedChanged);
            this.t44_2.AutoSize = true;
            this.t44_2.Location = new Point(0xd4, 520);
            this.t44_2.Name = "t44_2";
            this.t44_2.Size = new Size(0x24, 0x10);
            this.t44_2.TabIndex = 0x11;
            this.t44_2.Text = "有";
            this.t44_2.UseVisualStyleBackColor = true;
            this.t44_2.CheckedChanged += new EventHandler(this.t44_2_CheckedChanged);
            this.t40_2.AutoSize = true;
            this.t40_2.Location = new Point(0xd4, 0x1d8);
            this.t40_2.Name = "t40_2";
            this.t40_2.Size = new Size(0x24, 0x10);
            this.t40_2.TabIndex = 0x11;
            this.t40_2.Text = "有";
            this.t40_2.UseVisualStyleBackColor = true;
            this.t40_2.CheckedChanged += new EventHandler(this.t40_2_CheckedChanged);
            this.t48_1.AutoSize = true;
            this.t48_1.Location = new Point(130, 0x238);
            this.t48_1.Name = "t48_1";
            this.t48_1.Size = new Size(0x48, 0x10);
            this.t48_1.TabIndex = 0x11;
            this.t48_1.Text = "一级护理";
            this.t48_1.UseVisualStyleBackColor = true;
            this.t48_1.CheckedChanged += new EventHandler(this.t48_1_CheckedChanged);
            this.t17_2.AutoSize = true;
            this.t17_2.Location = new Point(0xb8, 0xec);
            this.t17_2.Name = "t17_2";
            this.t17_2.Size = new Size(0x30, 0x10);
            this.t17_2.TabIndex = 0x11;
            this.t17_2.Text = "焦虑";
            this.t17_2.UseVisualStyleBackColor = true;
            this.t17_2.CheckedChanged += new EventHandler(this.t17_2_CheckedChanged);
            this.t33_1.AutoSize = true;
            this.t33_1.Location = new Point(130, 0x19b);
            this.t33_1.Name = "t33_1";
            this.t33_1.Size = new Size(0x48, 0x10);
            this.t33_1.TabIndex = 0x11;
            this.t33_1.Text = "完全自理";
            this.t33_1.UseVisualStyleBackColor = true;
            this.t33_1.CheckedChanged += new EventHandler(this.t33_1_CheckedChanged);
            this.t19_1.AutoSize = true;
            this.t19_1.Location = new Point(130, 0x105);
            this.t19_1.Name = "t19_1";
            this.t19_1.Size = new Size(0x30, 0x10);
            this.t19_1.TabIndex = 0x11;
            this.t19_1.Text = "清醒";
            this.t19_1.UseVisualStyleBackColor = true;
            this.t19_1.CheckedChanged += new EventHandler(this.t19_1_CheckedChanged);
            this.t41_1.AutoSize = true;
            this.t41_1.Location = new Point(0x152, 0x1d7);
            this.t41_1.Name = "t41_1";
            this.t41_1.Size = new Size(0x24, 0x10);
            this.t41_1.TabIndex = 0x11;
            this.t41_1.Text = "无";
            this.t41_1.UseVisualStyleBackColor = true;
            this.t41_1.CheckedChanged += new EventHandler(this.t41_1_CheckedChanged);
            this.t46_1.AutoSize = true;
            this.t46_1.Location = new Point(0xa6, 0x21f);
            this.t46_1.Name = "t46_1";
            this.t46_1.Size = new Size(0x24, 0x10);
            this.t46_1.TabIndex = 0x11;
            this.t46_1.Text = "无";
            this.t46_1.UseVisualStyleBackColor = true;
            this.t46_1.CheckedChanged += new EventHandler(this.t46_1_CheckedChanged);
            this.t44_1.AutoSize = true;
            this.t44_1.Location = new Point(0xa6, 0x207);
            this.t44_1.Name = "t44_1";
            this.t44_1.Size = new Size(0x24, 0x10);
            this.t44_1.TabIndex = 0x11;
            this.t44_1.Text = "无";
            this.t44_1.UseVisualStyleBackColor = true;
            this.t44_1.CheckedChanged += new EventHandler(this.t44_1_CheckedChanged);
            this.t31_1.AutoSize = true;
            this.t31_1.Location = new Point(130, 0x182);
            this.t31_1.Name = "t31_1";
            this.t31_1.Size = new Size(0x30, 0x10);
            this.t31_1.TabIndex = 0x11;
            this.t31_1.Text = "正常";
            this.t31_1.UseVisualStyleBackColor = true;
            this.t31_1.CheckedChanged += new EventHandler(this.t31_1_CheckedChanged);
            this.t40_1.AutoSize = true;
            this.t40_1.Location = new Point(0xa6, 0x1d7);
            this.t40_1.Name = "t40_1";
            this.t40_1.Size = new Size(0x24, 0x10);
            this.t40_1.TabIndex = 0x11;
            this.t40_1.Text = "无";
            this.t40_1.UseVisualStyleBackColor = true;
            this.t40_1.CheckedChanged += new EventHandler(this.t40_1_CheckedChanged);
            this.t43_2.AutoSize = true;
            this.t43_2.Location = new Point(0x180, 0x1ef);
            this.t43_2.Name = "t43_2";
            this.t43_2.Size = new Size(60, 0x10);
            this.t43_2.TabIndex = 0x11;
            this.t43_2.Text = "不正常";
            this.t43_2.UseVisualStyleBackColor = true;
            this.t43_2.CheckedChanged += new EventHandler(this.t43_2_CheckedChanged);
            this.t10_2.AutoSize = true;
            this.t10_2.Location = new Point(0x195, 0x58);
            this.t10_2.Name = "t10_2";
            this.t10_2.Size = new Size(0x30, 0x10);
            this.t10_2.TabIndex = 0x11;
            this.t10_2.Text = "急诊";
            this.t10_2.UseVisualStyleBackColor = true;
            this.t10_2.CheckedChanged += new EventHandler(this.t10_2_CheckedChanged);
            this.t43_1.AutoSize = true;
            this.t43_1.Location = new Point(0x152, 0x1ef);
            this.t43_1.Name = "t43_1";
            this.t43_1.Size = new Size(0x30, 0x10);
            this.t43_1.TabIndex = 0x11;
            this.t43_1.Text = "正常";
            this.t43_1.UseVisualStyleBackColor = true;
            this.t43_1.CheckedChanged += new EventHandler(this.t43_1_CheckedChanged);
            this.t42_2.AutoSize = true;
            this.t42_2.Location = new Point(0xd4, 0x1ed);
            this.t42_2.Name = "t42_2";
            this.t42_2.Size = new Size(60, 0x10);
            this.t42_2.TabIndex = 0x11;
            this.t42_2.Text = "不正常";
            this.t42_2.UseVisualStyleBackColor = true;
            this.t42_2.CheckedChanged += new EventHandler(this.t42_2_CheckedChanged);
            this.t42_1.AutoSize = true;
            this.t42_1.Location = new Point(0xa6, 0x1ed);
            this.t42_1.Name = "t42_1";
            this.t42_1.Size = new Size(0x30, 0x10);
            this.t42_1.TabIndex = 0x11;
            this.t42_1.Text = "正常";
            this.t42_1.UseVisualStyleBackColor = true;
            this.t42_1.CheckedChanged += new EventHandler(this.t42_1_CheckedChanged);
            this.t17_1.AutoSize = true;
            this.t17_1.Location = new Point(130, 0xec);
            this.t17_1.Name = "t17_1";
            this.t17_1.Size = new Size(0x30, 0x10);
            this.t17_1.TabIndex = 0x11;
            this.t17_1.Text = "正常";
            this.t17_1.UseVisualStyleBackColor = true;
            this.t17_1.CheckedChanged += new EventHandler(this.t17_1_CheckedChanged);
            this.t10_1.AutoSize = true;
            this.t10_1.Location = new Point(0x15f, 0x58);
            this.t10_1.Name = "t10_1";
            this.t10_1.Size = new Size(0x30, 0x10);
            this.t10_1.TabIndex = 0x11;
            this.t10_1.Text = "门诊";
            this.t10_1.UseVisualStyleBackColor = true;
            this.t10_1.CheckedChanged += new EventHandler(this.t10_1_CheckedChanged);
            this.t09_2.AutoSize = true;
            this.t09_2.Location = new Point(0x90, 0x56);
            this.t09_2.Name = "t09_2";
            this.t09_2.Size = new Size(0x30, 0x10);
            this.t09_2.TabIndex = 0x11;
            this.t09_2.Text = "一般";
            this.t09_2.UseVisualStyleBackColor = true;
            this.t09_2.CheckedChanged += new EventHandler(this.t09_2_CheckedChanged);
            this.t09_1.AutoSize = true;
            this.t09_1.Location = new Point(90, 0x56);
            this.t09_1.Name = "t09_1";
            this.t09_1.Size = new Size(0x30, 0x10);
            this.t09_1.TabIndex = 0x11;
            this.t09_1.Text = "危重";
            this.t09_1.UseVisualStyleBackColor = true;
            this.t09_1.CheckedChanged += new EventHandler(this.t09_1_CheckedChanged);
            this.label17.AutoSize = true;
            this.label17.Location = new Point(0x10b, 0x59);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x3b, 12);
            this.label17.TabIndex = 0x10;
            this.label17.Text = "入院方式:";
            this.label16.AutoSize = true;
            this.label16.Location = new Point(6, 0x59);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x3b, 12);
            this.label16.TabIndex = 0x10;
            this.label16.Text = "入院病情:";
            this.t08.Location = new Point(0x203, 0x31);
            this.t08.Mask = "0000年90月90日 90时00分";
            this.t08.Name = "t08";
            this.t08.Size = new Size(160, 0x15);
            this.t08.TabIndex = 15;
            this.t08.ValidatingType = typeof(DateTime);
            this.t07.Location = new Point(0x41, 0x31);
            this.t07.Name = "t07";
            this.t07.Size = new Size(0x175, 0x15);
            this.t07.TabIndex = 14;
            this.label15.AutoSize = true;
            this.label15.Location = new Point(0x1c7, 0x3a);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x3b, 12);
            this.label15.TabIndex = 13;
            this.label15.Text = "入院时间:";
            this.label14.AutoSize = true;
            this.label14.Location = new Point(6, 0x3a);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x3b, 12);
            this.label14.TabIndex = 12;
            this.label14.Text = "入院诊断:";
            this.t06.Location = new Point(0x255, 15);
            this.t06.Name = "t06";
            this.t06.Size = new Size(0x5c, 0x15);
            this.t06.TabIndex = 11;
            this.t05.Location = new Point(0x1ef, 15);
            this.t05.Name = "t05";
            this.t05.Size = new Size(0x31, 0x15);
            this.t05.TabIndex = 10;
            this.t03.Location = new Point(0xf8, 0x11);
            this.t03.Name = "t03";
            this.t03.Size = new Size(0x27, 0x15);
            this.t03.TabIndex = 8;
            this.t02.FormattingEnabled = true;
            this.t02.Location = new Point(0x99, 0x11);
            this.t02.Name = "t02";
            this.t02.Size = new Size(0x2f, 20);
            this.t02.TabIndex = 7;
            this.t26.Location = new Point(0x109, 0x14b);
            this.t26.Name = "t26";
            this.t26.Size = new Size(0x3f, 0x15);
            this.t26.TabIndex = 6;
            this.t01.Location = new Point(0x2a, 0x10);
            this.t01.Name = "t01";
            this.t01.Size = new Size(0x3f, 0x15);
            this.t01.TabIndex = 6;
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0x228, 0x19);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x2f, 12);
            this.label13.TabIndex = 5;
            this.label13.Text = "病案号:";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0x1ce, 0x19);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x23, 12);
            this.label12.TabIndex = 4;
            this.label12.Text = "床号:";
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0x123, 0x19);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x3b, 12);
            this.label11.TabIndex = 3;
            this.label11.Text = "岁  科别:";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0xd5, 0x19);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x23, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "年龄:";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x76, 0x19);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x23, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "性别:";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(6, 0x19);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x23, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "姓名:";
            this.groupBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.lblDiagnose);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblDocNo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblDeptName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblAge);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblGender);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtBedLabel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x2d1, 0x2b);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.lblDiagnose.AutoSize = true;
            this.lblDiagnose.ForeColor = Color.Blue;
            this.lblDiagnose.Location = new Point(0x270, 0x11);
            this.lblDiagnose.Name = "lblDiagnose";
            this.lblDiagnose.Size = new Size(0x35, 12);
            this.lblDiagnose.TabIndex = 0x12;
            this.lblDiagnose.Text = "张三李四";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x24d, 0x11);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 0x11;
            this.label2.Text = "诊断";
            this.lblDocNo.AutoSize = true;
            this.lblDocNo.ForeColor = Color.Blue;
            this.lblDocNo.Location = new Point(0x20d, 0x11);
            this.lblDocNo.Name = "lblDocNo";
            this.lblDocNo.Size = new Size(0x3b, 12);
            this.lblDocNo.TabIndex = 0x10;
            this.lblDocNo.Text = "JX0000001";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x1de, 0x11);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x29, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "病案号";
            this.lblDeptName.AutoSize = true;
            this.lblDeptName.ForeColor = Color.Blue;
            this.lblDeptName.Location = new Point(0x195, 0x11);
            this.lblDeptName.Name = "lblDeptName";
            this.lblDeptName.Size = new Size(0x35, 12);
            this.lblDeptName.TabIndex = 14;
            this.lblDeptName.Text = "消化内科";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(370, 0x11);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "科别";
            this.lblAge.AutoSize = true;
            this.lblAge.ForeColor = Color.Blue;
            this.lblAge.Location = new Point(0x137, 0x11);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new Size(0x2f, 12);
            this.lblAge.TabIndex = 12;
            this.lblAge.Text = "12岁5月";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x114, 0x11);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x1d, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "年龄";
            this.lblGender.AutoSize = true;
            this.lblGender.ForeColor = Color.Blue;
            this.lblGender.Location = new Point(0xf3, 0x11);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new Size(0x11, 12);
            this.lblGender.TabIndex = 10;
            this.lblGender.Text = "男";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0xd0, 0x11);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "性别";
            this.lblName.AutoSize = true;
            this.lblName.ForeColor = Color.Blue;
            this.lblName.Location = new Point(0x95, 0x11);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(0x35, 12);
            this.lblName.TabIndex = 8;
            this.lblName.Text = "张三李四";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x72, 0x11);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "姓名";
            this.txtBedLabel.Location = new Point(0x29, 14);
            this.txtBedLabel.Name = "txtBedLabel";
            this.txtBedLabel.Size = new Size(0x36, 0x15);
            this.txtBedLabel.TabIndex = 2;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(6, 0x11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "床标";
            this.button1.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.button1.Location = new Point(0x1b7, 0x1d0);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x4b, 0x17);
            this.button1.TabIndex = 2;
            this.button1.Text = "打印";
            this.button1.UseVisualStyleBackColor = true;
            this.button2.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.button2.Location = new Point(0x23b, 0x1d0);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x4b, 0x17);
            this.button2.TabIndex = 2;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.button3.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.button3.Location = new Point(0x292, 0x1d1);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x4b, 0x17);
            this.button3.TabIndex = 2;
            this.button3.Text = "删除";
            this.button3.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2e9, 0x1ed);
            base.Controls.Add(this.button3);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.panel1);
            base.Name = "NursingRecNormal_I";
            this.Text = "NursingRecNormal_I";
            base.Load += new EventHandler(this.NursingRecNormal_I_Load);
            this.panel1.ResumeLayout(false);
            this.t.ResumeLayout(false);
            this.t.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void NursingRecNormal_I_Load(object sender, EventArgs e)
        {
            try
            {
                this.initFrmVal();
                this.initDisp();
            }
            catch (Exception exception)
            {
                Error.ErrProc(exception);
            }
        }

        private DataSet PatientData(string patientId, string visitId)
        {
            this.sql = string.Empty;
            this.sql = this.sql + "SELECT *";
            this.sql = this.sql + " FROM ";
            this.sql = this.sql + "PAT_EVALUATION_NEW ";
            this.sql = this.sql + "WHERE ";
            this.sql = this.sql + "PATIENT_ID = " + SqlManager.SqlConvert(patientId);
            this.sql = this.sql + " AND VISIT_ID = " + SqlManager.SqlConvert(visitId);
            this.sql = this.sql + " AND CREATE_TYPE = '1'";
            this.sql = this.sql + " AND DEPT_CODE = '" + GVars.User.DeptCode + "'";
            this.ds = GVars.OracleAccess.SelectData(this.sql, "t1");
            return this.ds;
        }

        private void SaveInfo(string Xstr, string Rstr)
        {
            if (this.ds.Tables["t1"].Select("DICTITEM_ID='" + Xstr + "'").Length > 0)
            {
                this.ds.Tables["t1"].Rows[0]["PATIENT_ID"] = this.patientId;
                this.ds.Tables["t1"].Rows[0]["VISIT_ID"] = int.Parse(this.visitId);
                this.ds.Tables["t1"].Rows[0]["DICTITEM_ID"] = Xstr;
                this.ds.Tables["t1"].Rows[0]["CREATE_TYPE"] = "1";
                this.ds.Tables["t1"].Rows[0]["DEPT_CODE"] = GVars.User.DeptCode;
                this.ds.Tables["t1"].Rows[0]["CONTENTS"] = Rstr;
            }
            else
            {
                this.dr = this.ds.Tables["t1"].NewRow();
                this.dr[0] = this.patientId;
                this.dr[1] = int.Parse(this.visitId);
                this.dr[2] = Xstr;
                this.dr[4] = "1";
                this.dr[5] = GVars.User.DeptCode;
                this.dr[3] = Rstr;
                this.ds.Tables["t1"].Rows.Add(this.dr);
            }
        }

        private void showNursingRec(string timePoint)
        {
        }

        private void showNursingRec(DataRow[] drArray, int rowStart)
        {
        }

        private void showPatientInfo()
        {
            this.patientId = string.Empty;
            this.visitId = string.Empty;
            if (((this.dsPatient == null) || (this.dsPatient.Tables.Count == 0)) || (this.dsPatient.Tables[0].Rows.Count == 0))
            {
                GVars.Msg.Show("W00005");
            }
            else
            {
                DataRow row = this.dsPatient.Tables[0].Rows[0];
                PersonCls cls = new PersonCls();
                string s = row["DATE_OF_BIRTH"].ToString();
                if (s.Length > 0)
                {
                    s = PersonCls.GetAge(DateTime.Parse(s), GVars.OracleAccess.GetSysDate());
                }
                this.lblName.Text = row["NAME"].ToString();
                this.lblGender.Text = row["SEX"].ToString();
                this.lblAge.Text = s;
                this.lblDeptName.Text = row["DEPT_NAME"].ToString();
                this.lblDocNo.Text = row["INP_NO"].ToString();
                this.lblDiagnose.Text = row["DIAGNOSIS"].ToString();
                this.patientId = row["PATIENT_ID"].ToString();
                this.visitId = row["VISIT_ID"].ToString();
            }
        }

        private void t09_1_CheckedChanged(object sender, EventArgs e)
        {
            this.t09_2.Checked = false;
            this.s09 = "危重";
            this.t09_1.Checked = true;
        }

        private void t09_2_CheckedChanged(object sender, EventArgs e)
        {
            this.t09_1.Checked = false;
            this.s09 = "一般";
            this.t09_2.Checked = true;
        }

        private void t10_1_CheckedChanged(object sender, EventArgs e)
        {
            this.t10_2.Checked = false;
            this.t10_3.Checked = false;
            this.t10_4.Checked = false;
            this.t10_5.Checked = false;
            this.s10 = "门诊";
            this.t10_1.Checked = true;
        }

        private void t10_2_CheckedChanged(object sender, EventArgs e)
        {
            this.t10_1.Checked = false;
            this.t10_3.Checked = false;
            this.t10_4.Checked = false;
            this.t10_5.Checked = false;
            this.s10 = "急诊";
            this.t10_2.Checked = true;
        }

        private void t10_3_CheckedChanged(object sender, EventArgs e)
        {
            this.t10_2.Checked = false;
            this.t10_1.Checked = false;
            this.t10_4.Checked = false;
            this.t10_5.Checked = false;
            this.s10 = "步行";
            this.t10_3.Checked = true;
        }

        private void t10_4_CheckedChanged(object sender, EventArgs e)
        {
            this.t10_2.Checked = false;
            this.t10_3.Checked = false;
            this.t10_1.Checked = false;
            this.t10_5.Checked = false;
            this.s10 = "轮椅";
            this.t10_4.Checked = true;
        }

        private void t10_5_CheckedChanged(object sender, EventArgs e)
        {
            this.t10_1.Checked = false;
            this.t10_2.Checked = false;
            this.t10_3.Checked = false;
            this.t10_4.Checked = false;
            this.s10 = "平车";
            this.t10_5.Checked = true;
        }

        private void t17_1_CheckedChanged(object sender, EventArgs e)
        {
            this.t17_5.Checked = false;
            this.t17_2.Checked = false;
            this.t17_3.Checked = false;
            this.t17_4.Checked = false;
            this.s17 = "正常";
            this.t17_1.Checked = true;
        }

        private void t17_2_CheckedChanged(object sender, EventArgs e)
        {
            this.t17_1.Checked = false;
            this.t17_5.Checked = false;
            this.t17_3.Checked = false;
            this.t17_4.Checked = false;
            this.s17 = "焦虑";
            this.t17_2.Checked = true;
        }

        private void t17_3_CheckedChanged(object sender, EventArgs e)
        {
            this.t17_1.Checked = false;
            this.t17_2.Checked = false;
            this.t17_5.Checked = false;
            this.t17_4.Checked = false;
            this.s17 = "紧张";
            this.t17_3.Checked = true;
        }

        private void t17_4_CheckedChanged(object sender, EventArgs e)
        {
            this.t17_1.Checked = false;
            this.t17_2.Checked = false;
            this.t17_3.Checked = false;
            this.t17_5.Checked = false;
            this.s17 = "恐惧";
            this.t17_4.Checked = true;
        }

        private void t17_5_CheckedChanged(object sender, EventArgs e)
        {
            this.t17_1.Checked = false;
            this.t17_2.Checked = false;
            this.t17_3.Checked = false;
            this.t17_4.Checked = false;
            this.s17 = "其他";
            this.t17_5.Checked = true;
        }

        private void t19_1_CheckedChanged(object sender, EventArgs e)
        {
            this.t19_5.Checked = false;
            this.t19_2.Checked = false;
            this.t19_3.Checked = false;
            this.t19_4.Checked = false;
            this.s19 = "清醒";
            this.t19_1.Checked = true;
        }

        private void t19_2_CheckedChanged(object sender, EventArgs e)
        {
            this.t19_1.Checked = false;
            this.t19_5.Checked = false;
            this.t19_3.Checked = false;
            this.t19_4.Checked = false;
            this.s19 = "嗜睡";
            this.t19_2.Checked = true;
        }

        private void t19_3_CheckedChanged(object sender, EventArgs e)
        {
            this.t19_1.Checked = false;
            this.t19_2.Checked = false;
            this.t19_5.Checked = false;
            this.t19_4.Checked = false;
            this.s19 = "昏睡";
            this.t19_3.Checked = true;
        }

        private void t19_4_CheckedChanged(object sender, EventArgs e)
        {
            this.t19_1.Checked = false;
            this.t19_2.Checked = false;
            this.t19_3.Checked = false;
            this.t19_5.Checked = false;
            this.s19 = "昏迷";
            this.t19_4.Checked = true;
        }

        private void t19_5_CheckedChanged(object sender, EventArgs e)
        {
            this.t19_1.Checked = false;
            this.t19_2.Checked = false;
            this.t19_3.Checked = false;
            this.t19_4.Checked = false;
            this.s19 = "其他";
            this.t19_5.Checked = true;
        }

        private void t21_1_CheckedChanged(object sender, EventArgs e)
        {
            this.t21_2.Checked = false;
            this.s21 = "正常";
            this.t21_1.Checked = true;
        }

        private void t21_2_CheckedChanged(object sender, EventArgs e)
        {
            this.t21_1.Checked = false;
            this.s21 = "异常";
            this.t21_2.Checked = true;
        }

        private void t23_1_CheckedChanged(object sender, EventArgs e)
        {
            this.t23_2.Checked = false;
            this.s23 = "正常";
            this.t23_1.Checked = true;
        }

        private void t23_2_CheckedChanged(object sender, EventArgs e)
        {
            this.t23_1.Checked = false;
            this.s23 = "异常";
            this.t23_2.Checked = true;
        }

        private void t25_1_CheckedChanged(object sender, EventArgs e)
        {
            this.t25_2.Checked = false;
            this.s25 = "完整";
            this.t25_1.Checked = true;
        }

        private void t25_2_CheckedChanged(object sender, EventArgs e)
        {
            this.t25_1.Checked = false;
            this.s25 = "破损";
            this.t25_2.Checked = true;
        }

        private void t29_1_CheckedChanged(object sender, EventArgs e)
        {
            this.t29_2.Checked = false;
            this.s29 = "正常";
            this.t29_1.Checked = true;
        }

        private void t29_2_CheckedChanged(object sender, EventArgs e)
        {
            this.t29_1.Checked = false;
            this.s29 = "异常";
            this.t29_2.Checked = true;
        }

        private void t31_1_CheckedChanged(object sender, EventArgs e)
        {
            this.t31_4.Checked = false;
            this.t31_2.Checked = false;
            this.t31_3.Checked = false;
            this.s31 = "正常";
            this.t31_1.Checked = true;
        }

        private void t31_2_CheckedChanged(object sender, EventArgs e)
        {
            this.t31_1.Checked = false;
            this.t31_4.Checked = false;
            this.t31_3.Checked = false;
            this.s31 = "含糊不清";
            this.t31_2.Checked = true;
        }

        private void t31_3_CheckedChanged(object sender, EventArgs e)
        {
            this.t31_1.Checked = false;
            this.t31_2.Checked = false;
            this.t31_4.Checked = false;
            this.s31 = "失语";
            this.t31_3.Checked = true;
        }

        private void t31_4_CheckedChanged(object sender, EventArgs e)
        {
            this.t31_1.Checked = false;
            this.t31_2.Checked = false;
            this.t31_3.Checked = false;
            this.s31 = "其他";
            this.t31_4.Checked = true;
        }

        private void t33_1_CheckedChanged(object sender, EventArgs e)
        {
            this.t33_4.Checked = false;
            this.t33_2.Checked = false;
            this.t33_3.Checked = false;
            this.s33 = "完全自理";
            this.t33_1.Checked = true;
        }

        private void t33_2_CheckedChanged(object sender, EventArgs e)
        {
            this.t33_1.Checked = false;
            this.t33_4.Checked = false;
            this.t33_3.Checked = false;
            this.s33 = "部分自理";
            this.t33_2.Checked = true;
        }

        private void t33_3_CheckedChanged(object sender, EventArgs e)
        {
            this.t33_1.Checked = false;
            this.t33_2.Checked = false;
            this.t33_4.Checked = false;
            this.s33 = "完全依赖";
            this.t33_3.Checked = true;
        }

        private void t33_4_CheckedChanged(object sender, EventArgs e)
        {
            this.t33_1.Checked = false;
            this.t33_2.Checked = false;
            this.t33_3.Checked = false;
            this.s33 = "其他";
            this.t33_4.Checked = true;
        }

        private void t40_1_CheckedChanged(object sender, EventArgs e)
        {
            this.t40_2.Checked = false;
            this.s40 = "无";
            this.t40_1.Checked = true;
        }

        private void t40_2_CheckedChanged(object sender, EventArgs e)
        {
            this.t40_1.Checked = false;
            this.s40 = "有";
            this.t40_2.Checked = true;
        }

        private void t41_1_CheckedChanged(object sender, EventArgs e)
        {
            this.t41_2.Checked = false;
            this.s41 = "无";
            this.t41_1.Checked = true;
        }

        private void t41_2_CheckedChanged(object sender, EventArgs e)
        {
            this.t41_1.Checked = false;
            this.s41 = "有";
            this.t41_2.Checked = true;
        }

        private void t42_1_CheckedChanged(object sender, EventArgs e)
        {
            this.t42_2.Checked = false;
            this.s42 = "正常";
            this.t42_1.Checked = true;
        }

        private void t42_2_CheckedChanged(object sender, EventArgs e)
        {
            this.t42_1.Checked = false;
            this.s42 = "不正常";
            this.t42_2.Checked = true;
        }

        private void t43_1_CheckedChanged(object sender, EventArgs e)
        {
            this.t43_2.Checked = false;
            this.s43 = "正常";
            this.t43_1.Checked = true;
        }

        private void t43_2_CheckedChanged(object sender, EventArgs e)
        {
            this.t43_1.Checked = false;
            this.s43 = "不正常";
            this.t43_2.Checked = true;
        }

        private void t44_1_CheckedChanged(object sender, EventArgs e)
        {
            this.t44_2.Checked = false;
            this.s44 = "无";
            this.t44_1.Checked = true;
        }

        private void t44_2_CheckedChanged(object sender, EventArgs e)
        {
            this.t44_1.Checked = false;
            this.s44 = "有";
            this.t44_2.Checked = true;
        }

        private void t46_1_CheckedChanged(object sender, EventArgs e)
        {
            this.t46_2.Checked = false;
            this.s46 = "无";
            this.t46_1.Checked = true;
        }

        private void t46_2_CheckedChanged(object sender, EventArgs e)
        {
            this.t46_1.Checked = false;
            this.s46 = "有";
            this.t46_2.Checked = true;
        }

        private void t48_1_CheckedChanged(object sender, EventArgs e)
        {
            this.t48_4.Checked = false;
            this.t48_2.Checked = false;
            this.t48_3.Checked = false;
            this.s48 = "一级护理";
            this.t48_1.Checked = true;
        }

        private void t48_2_CheckedChanged(object sender, EventArgs e)
        {
            this.t48_1.Checked = false;
            this.t48_4.Checked = false;
            this.t48_3.Checked = false;
            this.s48 = "二级护理";
            this.t48_2.Checked = true;
        }

        private void t48_3_CheckedChanged(object sender, EventArgs e)
        {
            this.t48_1.Checked = false;
            this.t48_2.Checked = false;
            this.t48_4.Checked = false;
            this.s48 = "三级护理";
            this.t48_3.Checked = true;
        }

        private void t48_4_CheckedChanged(object sender, EventArgs e)
        {
            this.t48_1.Checked = false;
            this.t48_2.Checked = false;
            this.t48_3.Checked = false;
            this.s48 = "其他";
            this.t48_4.Checked = true;
        }

        private void t50_1_CheckedChanged(object sender, EventArgs e)
        {
            this.t50_3.Checked = false;
            this.t50_2.Checked = false;
            this.s50 = "平卧位";
            this.t50_1.Checked = true;
        }

        private void t50_2_CheckedChanged(object sender, EventArgs e)
        {
            this.t50_1.Checked = false;
            this.t50_3.Checked = false;
            this.s50 = "半卧位";
            this.t50_2.Checked = true;
        }

        private void t50_3_CheckedChanged(object sender, EventArgs e)
        {
            this.t50_1.Checked = false;
            this.t50_2.Checked = false;
            this.s50 = "其他";
            this.t50_3.Checked = true;
        }

        private void t52_1_CheckedChanged(object sender, EventArgs e)
        {
            this.t52_4.Checked = false;
            this.t52_2.Checked = false;
            this.t52_3.Checked = false;
            this.s52 = "普食";
            this.t52_1.Checked = true;
        }

        private void t52_2_CheckedChanged(object sender, EventArgs e)
        {
            this.t52_1.Checked = false;
            this.t52_4.Checked = false;
            this.t52_3.Checked = false;
            this.s52 = "半流食";
            this.t52_2.Checked = true;
        }

        private void t52_3_CheckedChanged(object sender, EventArgs e)
        {
            this.t52_1.Checked = false;
            this.t52_2.Checked = false;
            this.t52_4.Checked = false;
            this.s52 = "流食";
            this.t52_3.Checked = true;
        }

        private void t52_4_CheckedChanged(object sender, EventArgs e)
        {
            this.t52_1.Checked = false;
            this.t52_2.Checked = false;
            this.t52_3.Checked = false;
            this.s52 = "其他";
            this.t52_4.Checked = true;
        }

        private void TextControl(Control ct)
        {
            string name = ct.GetType().Name;
            if (name != null)
            {
                if (!(name == "TextBox"))
                {
                    if (name == "MaskedTextBox")
                    {
                        this.SaveInfo(ct.Name.Replace("t", "").Trim(), ct.Text);
                    }
                    else if (name == "ComboBox")
                    {
                        this.SaveInfo(ct.Name.Replace("t", "").Trim(), ((ComboBox) ct).Text);
                    }
                }
                else if (ct.Name.Trim() != "txtBedLabel")
                {
                    this.SaveInfo(ct.Name.Replace("t", "").Trim(), ct.Text);
                }
            }
        }

        private void txtBedLabel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if ((e.KeyCode == Keys.Enter) && (this.txtBedLabel.Text.Trim().Length != 0))
                {
                    this.initDisp();
                    this.dsPatient = this.patientCom.GetInpPatientInfo_FromBedLabel(this.txtBedLabel.Text.Trim(), GVars.User.DeptCode);
                    if (((this.dsPatient == null) || (this.dsPatient.Tables.Count == 0)) || (this.dsPatient.Tables[0].Rows.Count == 0))
                    {
                        GVars.Msg.Show("W00005");
                    }
                    else
                    {
                        this.showPatientInfo();
                    }
                }
            }
            catch (Exception exception)
            {
                Error.ErrProc(exception);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}

