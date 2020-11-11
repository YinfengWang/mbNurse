namespace HISPlus
{
    partial class NursingRec
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NursingRec));
            this.ucGridView1 = new HISPlus.UserControls.UcGridView();
            this.groupBox2 = new DevExpress.XtraEditors.PanelControl();
            this.cmbItem_Filter = new HISPlus.UserControls.UcComboBox();
            this.label17 = new DevExpress.XtraEditors.LabelControl();
            this.cmbClass_Filter = new HISPlus.UserControls.UcComboBox();
            this.label16 = new DevExpress.XtraEditors.LabelControl();
            this.dtRngEnd = new System.Windows.Forms.DateTimePicker();
            this.label13 = new DevExpress.XtraEditors.LabelControl();
            this.dtRngStart = new System.Windows.Forms.DateTimePicker();
            this.label11 = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery = new HISPlus.UserControls.UcButton();
            this.groupBox3 = new DevExpress.XtraEditors.PanelControl();
            this.groupBox5 = new DevExpress.XtraEditors.PanelControl();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.txtMemo = new HISPlus.UserControls.UcTextArea();
            this.cmbClass = new HISPlus.UserControls.UcComboBox();
            this.label15 = new DevExpress.XtraEditors.LabelControl();
            this.label9 = new DevExpress.XtraEditors.LabelControl();
            this.txtValue = new HISPlus.UserControls.UcTextBox();
            this.lblUnit = new DevExpress.XtraEditors.LabelControl();
            this.cmbItem = new HISPlus.UserControls.UcComboBox();
            this.label7 = new DevExpress.XtraEditors.LabelControl();
            this.dtPickerTime = new System.Windows.Forms.DateTimePicker();
            this.label5 = new DevExpress.XtraEditors.LabelControl();
            this.dtPickerDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox4 = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.btnDelete = new HISPlus.UserControls.UcButton();
            this.btnAdd = new HISPlus.UserControls.UcButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox3)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox5)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox4)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucGridView1
            // 
            this.ucGridView1.AllowAddRows = false;
            this.ucGridView1.AllowDeleteRows = false;
            this.ucGridView1.AllowEdit = false;
            this.ucGridView1.AllowSort = false;
            this.ucGridView1.ColumnAutoWidth = true;
            this.ucGridView1.ColumnsEvenOldRowColor = null;
            this.ucGridView1.DataSource = null;
            this.ucGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGridView1.Location = new System.Drawing.Point(0, 56);
            this.ucGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.ucGridView1.Name = "ucGridView1";
            this.ucGridView1.ShowRowIndicator = false;
            this.ucGridView1.Size = new System.Drawing.Size(829, 514);
            this.ucGridView1.TabIndex = 1;
            this.ucGridView1.SelectionChanged += new System.EventHandler(this.ucGridView1_SelectionChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbItem_Filter);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.cmbClass_Filter);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.dtRngEnd);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.dtRngStart);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.btnQuery);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(1078, 56);
            this.groupBox2.TabIndex = 2;
            // 
            // cmbItem_Filter
            // 
            this.cmbItem_Filter.DataSource = null;
            this.cmbItem_Filter.DisplayMember = null;
            this.cmbItem_Filter.DropDownHeight = 0;
            this.cmbItem_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItem_Filter.DropDownWidth = 0;
            this.cmbItem_Filter.DroppedDown = false;
            this.cmbItem_Filter.FormattingEnabled = true;
            this.cmbItem_Filter.IntegralHeight = true;
            this.cmbItem_Filter.Location = new System.Drawing.Point(508, 20);
            this.cmbItem_Filter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbItem_Filter.MaxDropDownItems = 22;
            this.cmbItem_Filter.MaximumSize = new System.Drawing.Size(656, 15);
            this.cmbItem_Filter.MinimumSize = new System.Drawing.Size(26, 20);
            this.cmbItem_Filter.Name = "cmbItem_Filter";
            this.cmbItem_Filter.SelectedIndex = -1;
            this.cmbItem_Filter.SelectedValue = null;
            this.cmbItem_Filter.Size = new System.Drawing.Size(88, 20);
            this.cmbItem_Filter.TabIndex = 9;
            this.cmbItem_Filter.ValueMember = null;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(479, 24);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(24, 14);
            this.label17.TabIndex = 8;
            this.label17.Text = "项目";
            // 
            // cmbClass_Filter
            // 
            this.cmbClass_Filter.DataSource = null;
            this.cmbClass_Filter.DisplayMember = null;
            this.cmbClass_Filter.DropDownHeight = 0;
            this.cmbClass_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClass_Filter.DropDownWidth = 0;
            this.cmbClass_Filter.DroppedDown = false;
            this.cmbClass_Filter.FormattingEnabled = true;
            this.cmbClass_Filter.IntegralHeight = true;
            this.cmbClass_Filter.Location = new System.Drawing.Point(365, 21);
            this.cmbClass_Filter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbClass_Filter.MaxDropDownItems = 22;
            this.cmbClass_Filter.MaximumSize = new System.Drawing.Size(656, 15);
            this.cmbClass_Filter.MinimumSize = new System.Drawing.Size(26, 20);
            this.cmbClass_Filter.Name = "cmbClass_Filter";
            this.cmbClass_Filter.SelectedIndex = -1;
            this.cmbClass_Filter.SelectedValue = null;
            this.cmbClass_Filter.Size = new System.Drawing.Size(102, 20);
            this.cmbClass_Filter.TabIndex = 7;
            this.cmbClass_Filter.ValueMember = null;
            this.cmbClass_Filter.SelectedIndexChanged += new System.EventHandler(this.cmbClass_Filter_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(334, 24);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(24, 14);
            this.label16.TabIndex = 6;
            this.label16.Text = "类别";
            // 
            // dtRngEnd
            // 
            this.dtRngEnd.Location = new System.Drawing.Point(192, 21);
            this.dtRngEnd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dtRngEnd.Name = "dtRngEnd";
            this.dtRngEnd.Size = new System.Drawing.Size(129, 22);
            this.dtRngEnd.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(178, 26);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(4, 14);
            this.label13.TabIndex = 2;
            this.label13.Text = "-";
            // 
            // dtRngStart
            // 
            this.dtRngStart.Location = new System.Drawing.Point(39, 21);
            this.dtRngStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dtRngStart.Name = "dtRngStart";
            this.dtRngStart.Size = new System.Drawing.Size(129, 22);
            this.dtRngStart.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(7, 24);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 14);
            this.label11.TabIndex = 0;
            this.label11.Text = "日期";
            // 
            // btnQuery
            // 
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.ImageRight = false;
            this.btnQuery.ImageStyle = HISPlus.UserControls.ImageStyle.Find;
            this.btnQuery.Location = new System.Drawing.Point(620, 18);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnQuery.MaximumSize = new System.Drawing.Size(79, 23);
            this.btnQuery.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(79, 23);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.TextValue = "查询(&Q)";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox3.Location = new System.Drawing.Point(829, 56);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Size = new System.Drawing.Size(249, 514);
            this.groupBox3.TabIndex = 3;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.txtMemo);
            this.groupBox5.Controls.Add(this.cmbClass);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.txtValue);
            this.groupBox5.Controls.Add(this.lblUnit);
            this.groupBox5.Controls.Add(this.cmbItem);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.dtPickerTime);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.dtPickerDate);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Location = new System.Drawing.Point(7, 8);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox5.Size = new System.Drawing.Size(242, 333);
            this.groupBox5.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.label1.Location = new System.Drawing.Point(25, 200);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 14);
            this.label1.TabIndex = 11;
            this.label1.Text = "备注";
            // 
            // txtMemo
            // 
            this.txtMemo.HideSelection = true;
            this.txtMemo.Location = new System.Drawing.Point(60, 196);
            this.txtMemo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtMemo.MaxCharLength = 0;
            this.txtMemo.MaxLength = 40;
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.PasswordChar = '\0';
            this.txtMemo.ReadOnly = false;
            this.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMemo.SelectedText = "";
            this.txtMemo.SelectionLength = 0;
            this.txtMemo.SelectionStart = 0;
            this.txtMemo.Size = new System.Drawing.Size(129, 95);
            this.txtMemo.TabIndex = 12;
            // 
            // cmbClass
            // 
            this.cmbClass.DataSource = null;
            this.cmbClass.DisplayMember = null;
            this.cmbClass.DropDownHeight = 0;
            this.cmbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClass.DropDownWidth = 0;
            this.cmbClass.DroppedDown = false;
            this.cmbClass.FormattingEnabled = true;
            this.cmbClass.IntegralHeight = true;
            this.cmbClass.Location = new System.Drawing.Point(60, 85);
            this.cmbClass.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbClass.MaxDropDownItems = 22;
            this.cmbClass.MaximumSize = new System.Drawing.Size(656, 20);
            this.cmbClass.MinimumSize = new System.Drawing.Size(26, 15);
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.SelectedIndex = -1;
            this.cmbClass.SelectedValue = null;
            this.cmbClass.Size = new System.Drawing.Size(129, 20);
            this.cmbClass.TabIndex = 5;
            this.cmbClass.ValueMember = null;
            this.cmbClass.SelectedIndexChanged += new System.EventHandler(this.cmbClass_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(25, 89);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(24, 14);
            this.label15.TabIndex = 4;
            this.label15.Text = "类别";
            // 
            // label9
            // 
            this.label9.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.label9.Location = new System.Drawing.Point(25, 163);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(12, 14);
            this.label9.TabIndex = 8;
            this.label9.Text = "值";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(60, 159);
            this.txtValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtValue.MaxLength = 0;
            this.txtValue.Multiline = false;
            this.txtValue.Name = "txtValue";
            this.txtValue.PasswordChar = '\0';
            this.txtValue.ReadOnly = false;
            this.txtValue.Size = new System.Drawing.Size(129, 20);
            this.txtValue.TabIndex = 9;
            this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
            // 
            // lblUnit
            // 
            this.lblUnit.Location = new System.Drawing.Point(193, 163);
            this.lblUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(22, 14);
            this.lblUnit.TabIndex = 10;
            this.lblUnit.Text = "(ml)";
            this.lblUnit.Visible = false;
            // 
            // cmbItem
            // 
            this.cmbItem.DataSource = null;
            this.cmbItem.DisplayMember = null;
            this.cmbItem.DropDownHeight = 0;
            this.cmbItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItem.DropDownWidth = 0;
            this.cmbItem.DroppedDown = false;
            this.cmbItem.FormattingEnabled = true;
            this.cmbItem.IntegralHeight = true;
            this.cmbItem.Location = new System.Drawing.Point(60, 121);
            this.cmbItem.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbItem.MaxDropDownItems = 22;
            this.cmbItem.MaximumSize = new System.Drawing.Size(656, 20);
            this.cmbItem.MinimumSize = new System.Drawing.Size(26, 15);
            this.cmbItem.Name = "cmbItem";
            this.cmbItem.SelectedIndex = -1;
            this.cmbItem.SelectedValue = null;
            this.cmbItem.Size = new System.Drawing.Size(129, 20);
            this.cmbItem.TabIndex = 7;
            this.cmbItem.ValueMember = null;
            this.cmbItem.SelectedIndexChanged += new System.EventHandler(this.cmbItem_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(25, 124);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 14);
            this.label7.TabIndex = 6;
            this.label7.Text = "项目";
            // 
            // dtPickerTime
            // 
            this.dtPickerTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtPickerTime.Location = new System.Drawing.Point(60, 49);
            this.dtPickerTime.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dtPickerTime.Name = "dtPickerTime";
            this.dtPickerTime.ShowUpDown = true;
            this.dtPickerTime.Size = new System.Drawing.Size(129, 22);
            this.dtPickerTime.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(25, 54);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 14);
            this.label5.TabIndex = 2;
            this.label5.Text = "时间";
            // 
            // dtPickerDate
            // 
            this.dtPickerDate.Location = new System.Drawing.Point(60, 14);
            this.dtPickerDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dtPickerDate.Name = "dtPickerDate";
            this.dtPickerDate.Size = new System.Drawing.Size(129, 22);
            this.dtPickerDate.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(25, 19);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "日期";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.Controls.Add(this.btnDelete);
            this.groupBox4.Controls.Add(this.btnAdd);
            this.groupBox4.Location = new System.Drawing.Point(7, 347);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox4.Size = new System.Drawing.Size(242, 55);
            this.groupBox4.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageRight = false;
            this.btnSave.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSave.Location = new System.Drawing.Point(158, 16);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.MaximumSize = new System.Drawing.Size(79, 23);
            this.btnSave.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(62, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageRight = false;
            this.btnDelete.ImageStyle = HISPlus.UserControls.ImageStyle.Delete;
            this.btnDelete.Location = new System.Drawing.Point(84, 16);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDelete.MaximumSize = new System.Drawing.Size(79, 23);
            this.btnDelete.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(62, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.TextValue = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageRight = false;
            this.btnAdd.ImageStyle = HISPlus.UserControls.ImageStyle.Insert;
            this.btnAdd.Location = new System.Drawing.Point(10, 16);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAdd.MaximumSize = new System.Drawing.Size(79, 23);
            this.btnAdd.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(62, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.TextValue = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // NursingRec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 570);
            this.Controls.Add(this.ucGridView1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "NursingRec";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "护理记录";
            this.Load += new System.EventHandler(this.NursingRec_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox5)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox4)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl groupBox2;
        private HISPlus.UserControls.UcButton btnQuery;
        private DevExpress.XtraEditors.PanelControl groupBox3;
        private DevExpress.XtraEditors.PanelControl groupBox5;
        private DevExpress.XtraEditors.LabelControl label9;
        private HISPlus.UserControls.UcTextBox txtValue;
        private DevExpress.XtraEditors.LabelControl lblUnit;
        private HISPlus.UserControls.UcComboBox cmbItem;
        private DevExpress.XtraEditors.LabelControl label7;
        private System.Windows.Forms.DateTimePicker dtPickerTime;
        private DevExpress.XtraEditors.LabelControl label5;
        private System.Windows.Forms.DateTimePicker dtPickerDate;
        private DevExpress.XtraEditors.LabelControl label4;
        private DevExpress.XtraEditors.PanelControl groupBox4;
        private HISPlus.UserControls.UcButton btnSave;
        private HISPlus.UserControls.UcButton btnDelete;
        private HISPlus.UserControls.UcButton btnAdd;
        private System.Windows.Forms.DateTimePicker dtRngEnd;
        private DevExpress.XtraEditors.LabelControl label13;
        private System.Windows.Forms.DateTimePicker dtRngStart;
        private DevExpress.XtraEditors.LabelControl label11;
        private HISPlus.UserControls.UcComboBox cmbClass;
        private DevExpress.XtraEditors.LabelControl label15;
        private HISPlus.UserControls.UcComboBox cmbItem_Filter;
        private DevExpress.XtraEditors.LabelControl label17;
        private HISPlus.UserControls.UcComboBox cmbClass_Filter;
        private DevExpress.XtraEditors.LabelControl label16;
        private DevExpress.XtraEditors.LabelControl label1;
        private HISPlus.UserControls.UcTextArea txtMemo;
        private UserControls.UcGridView ucGridView1;
    }
}