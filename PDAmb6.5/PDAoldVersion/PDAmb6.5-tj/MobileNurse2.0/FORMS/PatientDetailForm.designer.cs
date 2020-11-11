namespace HISPlus
{
    partial class PatientDetailForm
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
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.txtBoxName = new System.Windows.Forms.TextBox();
            this.txtBoxSex = new System.Windows.Forms.TextBox();
            this.txtBoxDoctorInCharge = new System.Windows.Forms.TextBox();
            this.txtBoxAge = new System.Windows.Forms.TextBox();
            this.txtBoxInpNo = new System.Windows.Forms.TextBox();
            this.txtBoxDeptName = new System.Windows.Forms.TextBox();
            this.txtBoxBedNo = new System.Windows.Forms.TextBox();
            this.txtBoxPatientId = new System.Windows.Forms.TextBox();
            this.txtBoxAllergyDrug = new System.Windows.Forms.TextBox();
            this.txtBoxNurseClass = new System.Windows.Forms.TextBox();
            this.txtBoxDiagnose = new System.Windows.Forms.TextBox();
            this.txtBoxStatus = new System.Windows.Forms.TextBox();
            this.txtBoxInpDate = new System.Windows.Forms.TextBox();
            this.txtBoxVisitId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "返回";
            this.menuItem1.Click += new System.EventHandler(this.Close);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBox1.ForeColor = System.Drawing.Color.Blue;
            this.textBox1.Location = new System.Drawing.Point(128, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(41, 23);
            this.textBox1.TabIndex = 58;
            this.textBox1.Text = "姓名";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBox2.ForeColor = System.Drawing.Color.Blue;
            this.textBox2.Location = new System.Drawing.Point(128, 23);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(41, 23);
            this.textBox2.TabIndex = 59;
            this.textBox2.Text = "年龄";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBox3.ForeColor = System.Drawing.Color.Blue;
            this.textBox3.Location = new System.Drawing.Point(-2, 67);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(60, 23);
            this.textBox3.TabIndex = 60;
            this.textBox3.Text = "主管医生";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBox4.ForeColor = System.Drawing.Color.Blue;
            this.textBox4.Location = new System.Drawing.Point(-2, 45);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(61, 23);
            this.textBox4.TabIndex = 61;
            this.textBox4.Text = "住院号";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBox5.ForeColor = System.Drawing.Color.Blue;
            this.textBox5.Location = new System.Drawing.Point(128, 45);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(41, 23);
            this.textBox5.TabIndex = 62;
            this.textBox5.Text = "床位号";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBox6.ForeColor = System.Drawing.Color.Blue;
            this.textBox6.Location = new System.Drawing.Point(-2, 89);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(60, 23);
            this.textBox6.TabIndex = 63;
            this.textBox6.Text = "所属科室";
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBox7.ForeColor = System.Drawing.Color.Blue;
            this.textBox7.Location = new System.Drawing.Point(-2, 111);
            this.textBox7.Multiline = true;
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(60, 23);
            this.textBox7.TabIndex = 64;
            this.textBox7.Text = "过敏药物";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBox8.ForeColor = System.Drawing.Color.Blue;
            this.textBox8.Location = new System.Drawing.Point(-1, 0);
            this.textBox8.Multiline = true;
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(61, 23);
            this.textBox8.TabIndex = 65;
            this.textBox8.Text = "病人标识";
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBox9.ForeColor = System.Drawing.Color.Blue;
            this.textBox9.Location = new System.Drawing.Point(-4, 133);
            this.textBox9.Multiline = true;
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(62, 23);
            this.textBox9.TabIndex = 66;
            this.textBox9.Text = "护理等级";
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBox10.ForeColor = System.Drawing.Color.Blue;
            this.textBox10.Location = new System.Drawing.Point(-2, 155);
            this.textBox10.Multiline = true;
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(60, 23);
            this.textBox10.TabIndex = 67;
            this.textBox10.Text = "入院日期";
            this.textBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox11
            // 
            this.textBox11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBox11.ForeColor = System.Drawing.Color.Blue;
            this.textBox11.Location = new System.Drawing.Point(-5, 177);
            this.textBox11.Multiline = true;
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.Size = new System.Drawing.Size(63, 23);
            this.textBox11.TabIndex = 68;
            this.textBox11.Text = "病情";
            this.textBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox12
            // 
            this.textBox12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBox12.ForeColor = System.Drawing.Color.Blue;
            this.textBox12.Location = new System.Drawing.Point(-2, 199);
            this.textBox12.Multiline = true;
            this.textBox12.Name = "textBox12";
            this.textBox12.ReadOnly = true;
            this.textBox12.Size = new System.Drawing.Size(60, 69);
            this.textBox12.TabIndex = 69;
            this.textBox12.Text = "主要诊断";
            this.textBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox13
            // 
            this.textBox13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBox13.ForeColor = System.Drawing.Color.Blue;
            this.textBox13.Location = new System.Drawing.Point(128, 67);
            this.textBox13.Multiline = true;
            this.textBox13.Name = "textBox13";
            this.textBox13.ReadOnly = true;
            this.textBox13.Size = new System.Drawing.Size(41, 23);
            this.textBox13.TabIndex = 70;
            this.textBox13.Text = "序号";
            this.textBox13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox14
            // 
            this.textBox14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBox14.ForeColor = System.Drawing.Color.Blue;
            this.textBox14.Location = new System.Drawing.Point(-1, 23);
            this.textBox14.Multiline = true;
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new System.Drawing.Size(61, 23);
            this.textBox14.TabIndex = 71;
            this.textBox14.Text = "性别";
            this.textBox14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBoxName
            // 
            this.txtBoxName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtBoxName.ForeColor = System.Drawing.Color.Blue;
            this.txtBoxName.Location = new System.Drawing.Point(168, 0);
            this.txtBoxName.Multiline = true;
            this.txtBoxName.Name = "txtBoxName";
            this.txtBoxName.ReadOnly = true;
            this.txtBoxName.Size = new System.Drawing.Size(72, 23);
            this.txtBoxName.TabIndex = 72;
            this.txtBoxName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBoxSex
            // 
            this.txtBoxSex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtBoxSex.ForeColor = System.Drawing.Color.Blue;
            this.txtBoxSex.Location = new System.Drawing.Point(57, 23);
            this.txtBoxSex.Multiline = true;
            this.txtBoxSex.Name = "txtBoxSex";
            this.txtBoxSex.ReadOnly = true;
            this.txtBoxSex.Size = new System.Drawing.Size(72, 23);
            this.txtBoxSex.TabIndex = 73;
            this.txtBoxSex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBoxDoctorInCharge
            // 
            this.txtBoxDoctorInCharge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtBoxDoctorInCharge.ForeColor = System.Drawing.Color.Blue;
            this.txtBoxDoctorInCharge.Location = new System.Drawing.Point(57, 67);
            this.txtBoxDoctorInCharge.Multiline = true;
            this.txtBoxDoctorInCharge.Name = "txtBoxDoctorInCharge";
            this.txtBoxDoctorInCharge.ReadOnly = true;
            this.txtBoxDoctorInCharge.Size = new System.Drawing.Size(72, 23);
            this.txtBoxDoctorInCharge.TabIndex = 74;
            this.txtBoxDoctorInCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBoxAge
            // 
            this.txtBoxAge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtBoxAge.ForeColor = System.Drawing.Color.Blue;
            this.txtBoxAge.Location = new System.Drawing.Point(168, 23);
            this.txtBoxAge.Multiline = true;
            this.txtBoxAge.Name = "txtBoxAge";
            this.txtBoxAge.ReadOnly = true;
            this.txtBoxAge.Size = new System.Drawing.Size(72, 23);
            this.txtBoxAge.TabIndex = 75;
            this.txtBoxAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBoxInpNo
            // 
            this.txtBoxInpNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtBoxInpNo.ForeColor = System.Drawing.Color.Blue;
            this.txtBoxInpNo.Location = new System.Drawing.Point(57, 45);
            this.txtBoxInpNo.Multiline = true;
            this.txtBoxInpNo.Name = "txtBoxInpNo";
            this.txtBoxInpNo.ReadOnly = true;
            this.txtBoxInpNo.Size = new System.Drawing.Size(72, 23);
            this.txtBoxInpNo.TabIndex = 76;
            this.txtBoxInpNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBoxDeptName
            // 
            this.txtBoxDeptName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtBoxDeptName.ForeColor = System.Drawing.Color.Blue;
            this.txtBoxDeptName.HideSelection = false;
            this.txtBoxDeptName.Location = new System.Drawing.Point(57, 89);
            this.txtBoxDeptName.Multiline = true;
            this.txtBoxDeptName.Name = "txtBoxDeptName";
            this.txtBoxDeptName.ReadOnly = true;
            this.txtBoxDeptName.Size = new System.Drawing.Size(183, 23);
            this.txtBoxDeptName.TabIndex = 77;
            this.txtBoxDeptName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBoxBedNo
            // 
            this.txtBoxBedNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtBoxBedNo.ForeColor = System.Drawing.Color.Blue;
            this.txtBoxBedNo.Location = new System.Drawing.Point(168, 45);
            this.txtBoxBedNo.Multiline = true;
            this.txtBoxBedNo.Name = "txtBoxBedNo";
            this.txtBoxBedNo.ReadOnly = true;
            this.txtBoxBedNo.Size = new System.Drawing.Size(72, 23);
            this.txtBoxBedNo.TabIndex = 78;
            this.txtBoxBedNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBoxPatientId
            // 
            this.txtBoxPatientId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtBoxPatientId.ForeColor = System.Drawing.Color.Blue;
            this.txtBoxPatientId.Location = new System.Drawing.Point(57, 0);
            this.txtBoxPatientId.Multiline = true;
            this.txtBoxPatientId.Name = "txtBoxPatientId";
            this.txtBoxPatientId.ReadOnly = true;
            this.txtBoxPatientId.Size = new System.Drawing.Size(72, 23);
            this.txtBoxPatientId.TabIndex = 79;
            this.txtBoxPatientId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBoxAllergyDrug
            // 
            this.txtBoxAllergyDrug.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtBoxAllergyDrug.ForeColor = System.Drawing.Color.Red;
            this.txtBoxAllergyDrug.Location = new System.Drawing.Point(57, 111);
            this.txtBoxAllergyDrug.Multiline = true;
            this.txtBoxAllergyDrug.Name = "txtBoxAllergyDrug";
            this.txtBoxAllergyDrug.ReadOnly = true;
            this.txtBoxAllergyDrug.Size = new System.Drawing.Size(183, 23);
            this.txtBoxAllergyDrug.TabIndex = 80;
            this.txtBoxAllergyDrug.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBoxNurseClass
            // 
            this.txtBoxNurseClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtBoxNurseClass.ForeColor = System.Drawing.Color.Blue;
            this.txtBoxNurseClass.Location = new System.Drawing.Point(57, 133);
            this.txtBoxNurseClass.Multiline = true;
            this.txtBoxNurseClass.Name = "txtBoxNurseClass";
            this.txtBoxNurseClass.ReadOnly = true;
            this.txtBoxNurseClass.Size = new System.Drawing.Size(183, 23);
            this.txtBoxNurseClass.TabIndex = 81;
            this.txtBoxNurseClass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBoxDiagnose
            // 
            this.txtBoxDiagnose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtBoxDiagnose.ForeColor = System.Drawing.Color.Blue;
            this.txtBoxDiagnose.Location = new System.Drawing.Point(57, 199);
            this.txtBoxDiagnose.Multiline = true;
            this.txtBoxDiagnose.Name = "txtBoxDiagnose";
            this.txtBoxDiagnose.ReadOnly = true;
            this.txtBoxDiagnose.Size = new System.Drawing.Size(183, 69);
            this.txtBoxDiagnose.TabIndex = 82;
            this.txtBoxDiagnose.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBoxStatus
            // 
            this.txtBoxStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtBoxStatus.ForeColor = System.Drawing.Color.Blue;
            this.txtBoxStatus.Location = new System.Drawing.Point(57, 177);
            this.txtBoxStatus.Multiline = true;
            this.txtBoxStatus.Name = "txtBoxStatus";
            this.txtBoxStatus.ReadOnly = true;
            this.txtBoxStatus.Size = new System.Drawing.Size(183, 23);
            this.txtBoxStatus.TabIndex = 83;
            this.txtBoxStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBoxInpDate
            // 
            this.txtBoxInpDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtBoxInpDate.ForeColor = System.Drawing.Color.Blue;
            this.txtBoxInpDate.Location = new System.Drawing.Point(57, 155);
            this.txtBoxInpDate.Multiline = true;
            this.txtBoxInpDate.Name = "txtBoxInpDate";
            this.txtBoxInpDate.ReadOnly = true;
            this.txtBoxInpDate.Size = new System.Drawing.Size(183, 23);
            this.txtBoxInpDate.TabIndex = 84;
            this.txtBoxInpDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBoxVisitId
            // 
            this.txtBoxVisitId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtBoxVisitId.ForeColor = System.Drawing.Color.Blue;
            this.txtBoxVisitId.Location = new System.Drawing.Point(168, 67);
            this.txtBoxVisitId.Multiline = true;
            this.txtBoxVisitId.Name = "txtBoxVisitId";
            this.txtBoxVisitId.ReadOnly = true;
            this.txtBoxVisitId.Size = new System.Drawing.Size(72, 23);
            this.txtBoxVisitId.TabIndex = 85;
            this.txtBoxVisitId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PatientDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.txtBoxInpDate);
            this.Controls.Add(this.txtBoxVisitId);
            this.Controls.Add(this.txtBoxStatus);
            this.Controls.Add(this.txtBoxNurseClass);
            this.Controls.Add(this.txtBoxAllergyDrug);
            this.Controls.Add(this.txtBoxPatientId);
            this.Controls.Add(this.txtBoxDeptName);
            this.Controls.Add(this.txtBoxBedNo);
            this.Controls.Add(this.txtBoxInpNo);
            this.Controls.Add(this.txtBoxDoctorInCharge);
            this.Controls.Add(this.txtBoxAge);
            this.Controls.Add(this.txtBoxSex);
            this.Controls.Add(this.txtBoxName);
            this.Controls.Add(this.textBox14);
            this.Controls.Add(this.textBox13);
            this.Controls.Add(this.textBox12);
            this.Controls.Add(this.textBox11);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtBoxDiagnose);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "PatientDetailForm";
            this.Text = "病人详细信息";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TextBox txtBoxName;
        private System.Windows.Forms.TextBox txtBoxSex;
        private System.Windows.Forms.TextBox txtBoxDoctorInCharge;
        private System.Windows.Forms.TextBox txtBoxAge;
        private System.Windows.Forms.TextBox txtBoxInpNo;
        private System.Windows.Forms.TextBox txtBoxDeptName;
        private System.Windows.Forms.TextBox txtBoxBedNo;
        private System.Windows.Forms.TextBox txtBoxPatientId;
        private System.Windows.Forms.TextBox txtBoxAllergyDrug;
        private System.Windows.Forms.TextBox txtBoxNurseClass;
        private System.Windows.Forms.TextBox txtBoxDiagnose;
        private System.Windows.Forms.TextBox txtBoxStatus;
        private System.Windows.Forms.TextBox txtBoxInpDate;
        private System.Windows.Forms.TextBox txtBoxVisitId;
        private System.Windows.Forms.MenuItem menuItem1;
    }
}