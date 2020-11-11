namespace HISPlus
{
    partial class NursingConfigFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NursingConfigFrm));
            this.groupBox1 = new DevExpress.XtraEditors.PanelControl();
            this.cbxDeptCode = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.ucGvType = new HISPlus.UserControls.UcGridView();
            this.cbxVital = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.label19 = new DevExpress.XtraEditors.LabelControl();
            this.label17 = new DevExpress.XtraEditors.LabelControl();
            this.txtRowCount = new HISPlus.UserControls.UcTextBox();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.btnDelete = new HISPlus.UserControls.UcButton();
            this.btnNew = new HISPlus.UserControls.UcButton();
            this.txtModuleName = new HISPlus.UserControls.UcTextBox();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.label3 = new DevExpress.XtraEditors.LabelControl();
            this.label4 = new DevExpress.XtraEditors.LabelControl();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.txtModuleCode = new HISPlus.UserControls.UcTextBox();
            this.cb_SIGN_FLAG = new HISPlus.UserControls.UcComboBox();
            this.cb_MULTI_LINE = new HISPlus.UserControls.UcComboBox();
            this.cb_MULTI_VALUE = new HISPlus.UserControls.UcComboBox();
            this.cb_selectValue = new HISPlus.UserControls.UcComboBox();
            this.cb_storeCol = new HISPlus.UserControls.UcComboBox();
            this.txtserial_no = new HISPlus.UserControls.UcTextBox();
            this.label14 = new DevExpress.XtraEditors.LabelControl();
            this.label15 = new DevExpress.XtraEditors.LabelControl();
            this.label16 = new DevExpress.XtraEditors.LabelControl();
            this.txt_width = new HISPlus.UserControls.UcTextBox();
            this.label11 = new DevExpress.XtraEditors.LabelControl();
            this.label12 = new DevExpress.XtraEditors.LabelControl();
            this.label13 = new DevExpress.XtraEditors.LabelControl();
            this.label9 = new DevExpress.XtraEditors.LabelControl();
            this.label10 = new DevExpress.XtraEditors.LabelControl();
            this.txtVital_code = new HISPlus.UserControls.UcTextBox();
            this.label7 = new DevExpress.XtraEditors.LabelControl();
            this.cbType = new HISPlus.UserControls.UcComboBox();
            this.label6 = new DevExpress.XtraEditors.LabelControl();
            this.txtTitle = new HISPlus.UserControls.UcTextBox();
            this.txtRecordType = new HISPlus.UserControls.UcTextBox();
            this.label5 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox3 = new DevExpress.XtraEditors.PanelControl();
            this.cbDictCode = new HISPlus.UserControls.UcComboBox();
            this.txtNumber = new HISPlus.UserControls.UcTextBox();
            this.label8 = new DevExpress.XtraEditors.LabelControl();
            this.bt_saveConfig = new HISPlus.UserControls.UcButton();
            this.bt_delConfig = new HISPlus.UserControls.UcButton();
            this.bt_addConfig = new HISPlus.UserControls.UcButton();
            this.ucGridView1 = new HISPlus.UserControls.UcGridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDeptCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxVital)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox3)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.cbxDeptCode);
            this.groupBox1.Controls.Add(this.ucGvType);
            this.groupBox1.Controls.Add(this.cbxVital);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtRowCount);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.txtModuleName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(13, 20);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 756);
            this.groupBox1.TabIndex = 2;
            // 
            // cbxDeptCode
            // 
            this.cbxDeptCode.Location = new System.Drawing.Point(21, 277);
            this.cbxDeptCode.Name = "cbxDeptCode";
            this.cbxDeptCode.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.cbxDeptCode.Size = new System.Drawing.Size(289, 139);
            this.cbxDeptCode.TabIndex = 22;
            this.cbxDeptCode.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.cbxDeptCode_ItemCheck);
            // 
            // ucGvType
            // 
            this.ucGvType.AllowAddRows = false;
            this.ucGvType.AllowDeleteRows = false;
            this.ucGvType.AllowEdit = false;
            this.ucGvType.AllowSort = false;
            this.ucGvType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucGvType.ColumnAutoWidth = true;
            this.ucGvType.ColumnsEvenOldRowColor = null;
            this.ucGvType.DataSource = null;
            this.ucGvType.Location = new System.Drawing.Point(21, 481);
            this.ucGvType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucGvType.Name = "ucGvType";
            this.ucGvType.ShowRowIndicator = false;
            this.ucGvType.Size = new System.Drawing.Size(289, 269);
            this.ucGvType.TabIndex = 6;
            this.ucGvType.SelectionChanged += new System.EventHandler(this.ucGvType_SelectionChanged);
            // 
            // cbxVital
            // 
            this.cbxVital.Location = new System.Drawing.Point(21, 125);
            this.cbxVital.Name = "cbxVital";
            this.cbxVital.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.cbxVital.Size = new System.Drawing.Size(289, 121);
            this.cbxVital.TabIndex = 21;
            this.cbxVital.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.cbxVital_ItemCheck);
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(20, 100);
            this.label19.Margin = new System.Windows.Forms.Padding(4);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(60, 18);
            this.label19.TabIndex = 55;
            this.label19.Text = "换页事件";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(20, 252);
            this.label17.Margin = new System.Windows.Forms.Padding(4);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(60, 18);
            this.label17.TabIndex = 51;
            this.label17.Text = "科室名称";
            // 
            // txtRowCount
            // 
            this.txtRowCount.Location = new System.Drawing.Point(97, 63);
            this.txtRowCount.Margin = new System.Windows.Forms.Padding(4);
            this.txtRowCount.MaxLength = 0;
            this.txtRowCount.Multiline = false;
            this.txtRowCount.Name = "txtRowCount";
            this.txtRowCount.PasswordChar = '\0';
            this.txtRowCount.ReadOnly = false;
            this.txtRowCount.Size = new System.Drawing.Size(213, 24);
            this.txtRowCount.TabIndex = 48;
            this.txtRowCount.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageRight = false;
            this.btnSave.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSave.Location = new System.Drawing.Point(230, 440);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnSave.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 2;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageRight = false;
            this.btnDelete.ImageStyle = HISPlus.UserControls.ImageStyle.Delete;
            this.btnDelete.Location = new System.Drawing.Point(126, 440);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnDelete.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 30);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.TextValue = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNew
            // 
            this.btnNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageRight = false;
            this.btnNew.ImageStyle = HISPlus.UserControls.ImageStyle.Add;
            this.btnNew.Location = new System.Drawing.Point(21, 440);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4);
            this.btnNew.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnNew.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 30);
            this.btnNew.TabIndex = 0;
            this.btnNew.TextValue = "新增";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // txtModuleName
            // 
            this.txtModuleName.Location = new System.Drawing.Point(97, 27);
            this.txtModuleName.Margin = new System.Windows.Forms.Padding(4);
            this.txtModuleName.MaxLength = 0;
            this.txtModuleName.Multiline = false;
            this.txtModuleName.Name = "txtModuleName";
            this.txtModuleName.PasswordChar = '\0';
            this.txtModuleName.ReadOnly = false;
            this.txtModuleName.Size = new System.Drawing.Size(213, 24);
            this.txtModuleName.TabIndex = 18;
            this.txtModuleName.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(21, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 18);
            this.label2.TabIndex = 13;
            this.label2.Text = "模版名称";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(20, 116);
            this.label3.Margin = new System.Windows.Forms.Padding(4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 18);
            this.label3.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(19, 68);
            this.label4.Margin = new System.Windows.Forms.Padding(4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 18);
            this.label4.TabIndex = 15;
            this.label4.Text = "每页行数";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 613);
            this.label1.Margin = new System.Windows.Forms.Padding(4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 18);
            this.label1.TabIndex = 12;
            this.label1.Text = "模版编码";
            this.label1.Visible = false;
            // 
            // txtModuleCode
            // 
            this.txtModuleCode.Enabled = false;
            this.txtModuleCode.Location = new System.Drawing.Point(96, 613);
            this.txtModuleCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtModuleCode.MaxLength = 0;
            this.txtModuleCode.Multiline = false;
            this.txtModuleCode.Name = "txtModuleCode";
            this.txtModuleCode.PasswordChar = '\0';
            this.txtModuleCode.ReadOnly = false;
            this.txtModuleCode.Size = new System.Drawing.Size(202, 24);
            this.txtModuleCode.TabIndex = 17;
            this.txtModuleCode.Visible = false;
            this.txtModuleCode.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // cb_SIGN_FLAG
            // 
            this.cb_SIGN_FLAG.DataSource = null;
            this.cb_SIGN_FLAG.DisplayMember = null;
            this.cb_SIGN_FLAG.DropDownHeight = 0;
            this.cb_SIGN_FLAG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_SIGN_FLAG.DropDownWidth = 0;
            this.cb_SIGN_FLAG.DroppedDown = false;
            this.cb_SIGN_FLAG.FormattingEnabled = true;
            this.cb_SIGN_FLAG.IntegralHeight = true;
            this.cb_SIGN_FLAG.Location = new System.Drawing.Point(96, 396);
            this.cb_SIGN_FLAG.Margin = new System.Windows.Forms.Padding(4);
            this.cb_SIGN_FLAG.MaxDropDownItems = 0;
            this.cb_SIGN_FLAG.MaximumSize = new System.Drawing.Size(1000, 24);
            this.cb_SIGN_FLAG.MinimumSize = new System.Drawing.Size(40, 24);
            this.cb_SIGN_FLAG.Name = "cb_SIGN_FLAG";
            this.cb_SIGN_FLAG.SelectedIndex = -1;
            this.cb_SIGN_FLAG.SelectedValue = null;
            this.cb_SIGN_FLAG.Size = new System.Drawing.Size(202, 24);
            this.cb_SIGN_FLAG.TabIndex = 47;
            this.cb_SIGN_FLAG.ValueMember = null;
            this.cb_SIGN_FLAG.SelectedIndexChanged += new System.EventHandler(this.txtDifin_TextChanged);
            this.cb_SIGN_FLAG.TextChanged += new System.EventHandler(this.txtDifin_TextChanged);
            // 
            // cb_MULTI_LINE
            // 
            this.cb_MULTI_LINE.DataSource = null;
            this.cb_MULTI_LINE.DisplayMember = null;
            this.cb_MULTI_LINE.DropDownHeight = 0;
            this.cb_MULTI_LINE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_MULTI_LINE.DropDownWidth = 0;
            this.cb_MULTI_LINE.DroppedDown = false;
            this.cb_MULTI_LINE.FormattingEnabled = true;
            this.cb_MULTI_LINE.IntegralHeight = true;
            this.cb_MULTI_LINE.Location = new System.Drawing.Point(96, 355);
            this.cb_MULTI_LINE.Margin = new System.Windows.Forms.Padding(4);
            this.cb_MULTI_LINE.MaxDropDownItems = 0;
            this.cb_MULTI_LINE.MaximumSize = new System.Drawing.Size(1000, 24);
            this.cb_MULTI_LINE.MinimumSize = new System.Drawing.Size(40, 24);
            this.cb_MULTI_LINE.Name = "cb_MULTI_LINE";
            this.cb_MULTI_LINE.SelectedIndex = -1;
            this.cb_MULTI_LINE.SelectedValue = null;
            this.cb_MULTI_LINE.Size = new System.Drawing.Size(202, 24);
            this.cb_MULTI_LINE.TabIndex = 46;
            this.cb_MULTI_LINE.ValueMember = null;
            this.cb_MULTI_LINE.SelectedIndexChanged += new System.EventHandler(this.txtDifin_TextChanged);
            this.cb_MULTI_LINE.TextChanged += new System.EventHandler(this.txtDifin_TextChanged);
            // 
            // cb_MULTI_VALUE
            // 
            this.cb_MULTI_VALUE.DataSource = null;
            this.cb_MULTI_VALUE.DisplayMember = null;
            this.cb_MULTI_VALUE.DropDownHeight = 0;
            this.cb_MULTI_VALUE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_MULTI_VALUE.DropDownWidth = 0;
            this.cb_MULTI_VALUE.DroppedDown = false;
            this.cb_MULTI_VALUE.FormattingEnabled = true;
            this.cb_MULTI_VALUE.IntegralHeight = true;
            this.cb_MULTI_VALUE.Location = new System.Drawing.Point(96, 273);
            this.cb_MULTI_VALUE.Margin = new System.Windows.Forms.Padding(4);
            this.cb_MULTI_VALUE.MaxDropDownItems = 0;
            this.cb_MULTI_VALUE.MaximumSize = new System.Drawing.Size(1000, 24);
            this.cb_MULTI_VALUE.MinimumSize = new System.Drawing.Size(40, 24);
            this.cb_MULTI_VALUE.Name = "cb_MULTI_VALUE";
            this.cb_MULTI_VALUE.SelectedIndex = -1;
            this.cb_MULTI_VALUE.SelectedValue = null;
            this.cb_MULTI_VALUE.Size = new System.Drawing.Size(202, 24);
            this.cb_MULTI_VALUE.TabIndex = 45;
            this.cb_MULTI_VALUE.ValueMember = null;
            this.cb_MULTI_VALUE.SelectedIndexChanged += new System.EventHandler(this.txtDifin_TextChanged);
            this.cb_MULTI_VALUE.TextChanged += new System.EventHandler(this.txtDifin_TextChanged);
            // 
            // cb_selectValue
            // 
            this.cb_selectValue.DataSource = null;
            this.cb_selectValue.DisplayMember = null;
            this.cb_selectValue.DropDownHeight = 0;
            this.cb_selectValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_selectValue.DropDownWidth = 0;
            this.cb_selectValue.DroppedDown = false;
            this.cb_selectValue.FormattingEnabled = true;
            this.cb_selectValue.IntegralHeight = true;
            this.cb_selectValue.Location = new System.Drawing.Point(96, 232);
            this.cb_selectValue.Margin = new System.Windows.Forms.Padding(4);
            this.cb_selectValue.MaxDropDownItems = 0;
            this.cb_selectValue.MaximumSize = new System.Drawing.Size(1000, 24);
            this.cb_selectValue.MinimumSize = new System.Drawing.Size(40, 24);
            this.cb_selectValue.Name = "cb_selectValue";
            this.cb_selectValue.SelectedIndex = -1;
            this.cb_selectValue.SelectedValue = null;
            this.cb_selectValue.Size = new System.Drawing.Size(202, 24);
            this.cb_selectValue.TabIndex = 44;
            this.cb_selectValue.ValueMember = null;
            this.cb_selectValue.SelectedIndexChanged += new System.EventHandler(this.txtDifin_TextChanged);
            this.cb_selectValue.TextChanged += new System.EventHandler(this.txtDifin_TextChanged);
            // 
            // cb_storeCol
            // 
            this.cb_storeCol.DataSource = null;
            this.cb_storeCol.DisplayMember = null;
            this.cb_storeCol.DropDownHeight = 0;
            this.cb_storeCol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_storeCol.DropDownWidth = 0;
            this.cb_storeCol.DroppedDown = false;
            this.cb_storeCol.FormattingEnabled = true;
            this.cb_storeCol.IntegralHeight = false;
            this.cb_storeCol.Location = new System.Drawing.Point(96, 478);
            this.cb_storeCol.Margin = new System.Windows.Forms.Padding(4);
            this.cb_storeCol.MaxDropDownItems = 0;
            this.cb_storeCol.MaximumSize = new System.Drawing.Size(1000, 24);
            this.cb_storeCol.MinimumSize = new System.Drawing.Size(40, 24);
            this.cb_storeCol.Name = "cb_storeCol";
            this.cb_storeCol.SelectedIndex = -1;
            this.cb_storeCol.SelectedValue = null;
            this.cb_storeCol.Size = new System.Drawing.Size(202, 24);
            this.cb_storeCol.TabIndex = 43;
            this.cb_storeCol.ValueMember = null;
            this.cb_storeCol.SelectedIndexChanged += new System.EventHandler(this.txtDifin_TextChanged);
            this.cb_storeCol.TextChanged += new System.EventHandler(this.txtDifin_TextChanged);
            // 
            // txtserial_no
            // 
            this.txtserial_no.Location = new System.Drawing.Point(96, 437);
            this.txtserial_no.Margin = new System.Windows.Forms.Padding(4);
            this.txtserial_no.MaxLength = 0;
            this.txtserial_no.Multiline = false;
            this.txtserial_no.Name = "txtserial_no";
            this.txtserial_no.PasswordChar = '\0';
            this.txtserial_no.ReadOnly = false;
            this.txtserial_no.Size = new System.Drawing.Size(202, 24);
            this.txtserial_no.TabIndex = 42;
            this.txtserial_no.TextChanged += new System.EventHandler(this.txtDifin_TextChanged);
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(20, 399);
            this.label14.Margin = new System.Windows.Forms.Padding(4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(60, 18);
            this.label14.TabIndex = 38;
            this.label14.Text = "是否签名";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(20, 440);
            this.label15.Margin = new System.Windows.Forms.Padding(4);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(60, 18);
            this.label15.TabIndex = 39;
            this.label15.Text = "显示顺序";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(20, 481);
            this.label16.Margin = new System.Windows.Forms.Padding(4);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 18);
            this.label16.TabIndex = 40;
            this.label16.Text = "数据来源";
            // 
            // txt_width
            // 
            this.txt_width.Location = new System.Drawing.Point(96, 314);
            this.txt_width.Margin = new System.Windows.Forms.Padding(4);
            this.txt_width.MaxLength = 0;
            this.txt_width.Multiline = false;
            this.txt_width.Name = "txt_width";
            this.txt_width.PasswordChar = '\0';
            this.txt_width.ReadOnly = false;
            this.txt_width.Size = new System.Drawing.Size(202, 24);
            this.txt_width.TabIndex = 36;
            this.txt_width.TextChanged += new System.EventHandler(this.txtDifin_TextChanged);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(20, 276);
            this.label11.Margin = new System.Windows.Forms.Padding(4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 18);
            this.label11.TabIndex = 32;
            this.label11.Text = "是否多选";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(20, 317);
            this.label12.Margin = new System.Windows.Forms.Padding(4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 18);
            this.label12.TabIndex = 33;
            this.label12.Text = "列    宽";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(20, 358);
            this.label13.Margin = new System.Windows.Forms.Padding(4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 18);
            this.label13.TabIndex = 34;
            this.label13.Text = "是否多行";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(20, 194);
            this.label9.Margin = new System.Windows.Forms.Padding(4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 18);
            this.label9.TabIndex = 27;
            this.label9.Text = "对应字典";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(20, 235);
            this.label10.Margin = new System.Windows.Forms.Padding(4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 18);
            this.label10.TabIndex = 28;
            this.label10.Text = "字典取值";
            // 
            // txtVital_code
            // 
            this.txtVital_code.Location = new System.Drawing.Point(96, 150);
            this.txtVital_code.Margin = new System.Windows.Forms.Padding(4);
            this.txtVital_code.MaxLength = 0;
            this.txtVital_code.Multiline = false;
            this.txtVital_code.Name = "txtVital_code";
            this.txtVital_code.PasswordChar = '\0';
            this.txtVital_code.ReadOnly = false;
            this.txtVital_code.Size = new System.Drawing.Size(202, 24);
            this.txtVital_code.TabIndex = 25;
            this.txtVital_code.TextChanged += new System.EventHandler(this.txtDifin_TextChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(20, 153);
            this.label7.Margin = new System.Windows.Forms.Padding(4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 18);
            this.label7.TabIndex = 24;
            this.label7.Text = "参    数";
            // 
            // cbType
            // 
            this.cbType.DataSource = null;
            this.cbType.DisplayMember = null;
            this.cbType.DropDownHeight = 0;
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.DropDownWidth = 0;
            this.cbType.DroppedDown = false;
            this.cbType.FormattingEnabled = true;
            this.cbType.IntegralHeight = true;
            this.cbType.Location = new System.Drawing.Point(96, 109);
            this.cbType.Margin = new System.Windows.Forms.Padding(4);
            this.cbType.MaxDropDownItems = 0;
            this.cbType.MaximumSize = new System.Drawing.Size(1000, 24);
            this.cbType.MinimumSize = new System.Drawing.Size(40, 24);
            this.cbType.Name = "cbType";
            this.cbType.SelectedIndex = -1;
            this.cbType.SelectedValue = null;
            this.cbType.Size = new System.Drawing.Size(202, 24);
            this.cbType.TabIndex = 0;
            this.cbType.ValueMember = null;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.txtDifin_TextChanged);
            this.cbType.TextChanged += new System.EventHandler(this.txtDifin_TextChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(20, 112);
            this.label6.Margin = new System.Windows.Forms.Padding(4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 18);
            this.label6.TabIndex = 22;
            this.label6.Text = "类    型";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(96, 68);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(4);
            this.txtTitle.MaxLength = 0;
            this.txtTitle.Multiline = false;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.PasswordChar = '\0';
            this.txtTitle.ReadOnly = false;
            this.txtTitle.Size = new System.Drawing.Size(202, 24);
            this.txtTitle.TabIndex = 21;
            this.txtTitle.TextChanged += new System.EventHandler(this.txtDifin_TextChanged);
            // 
            // txtRecordType
            // 
            this.txtRecordType.Enabled = false;
            this.txtRecordType.Location = new System.Drawing.Point(96, 27);
            this.txtRecordType.Margin = new System.Windows.Forms.Padding(4);
            this.txtRecordType.MaxLength = 0;
            this.txtRecordType.Multiline = false;
            this.txtRecordType.Name = "txtRecordType";
            this.txtRecordType.PasswordChar = '\0';
            this.txtRecordType.ReadOnly = false;
            this.txtRecordType.Size = new System.Drawing.Size(202, 24);
            this.txtRecordType.TabIndex = 20;
            this.txtRecordType.TextChanged += new System.EventHandler(this.txtItem_TextChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(20, 71);
            this.label5.Margin = new System.Windows.Forms.Padding(4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 18);
            this.label5.TabIndex = 16;
            this.label5.Text = "标    题";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.cbDictCode);
            this.groupBox3.Controls.Add(this.txtNumber);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.bt_saveConfig);
            this.groupBox3.Controls.Add(this.bt_delConfig);
            this.groupBox3.Controls.Add(this.bt_addConfig);
            this.groupBox3.Controls.Add(this.txtRecordType);
            this.groupBox3.Controls.Add(this.cb_SIGN_FLAG);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtModuleCode);
            this.groupBox3.Controls.Add(this.cb_MULTI_LINE);
            this.groupBox3.Controls.Add(this.txtTitle);
            this.groupBox3.Controls.Add(this.cb_MULTI_VALUE);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cb_selectValue);
            this.groupBox3.Controls.Add(this.cbType);
            this.groupBox3.Controls.Add(this.cb_storeCol);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtserial_no);
            this.groupBox3.Controls.Add(this.txtVital_code);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.txt_width);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Location = new System.Drawing.Point(347, 20);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(318, 756);
            this.groupBox3.TabIndex = 4;
            // 
            // cbDictCode
            // 
            this.cbDictCode.DataSource = null;
            this.cbDictCode.DisplayMember = null;
            this.cbDictCode.DropDownHeight = 0;
            this.cbDictCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDictCode.DropDownWidth = 160;
            this.cbDictCode.DroppedDown = false;
            this.cbDictCode.FormattingEnabled = true;
            this.cbDictCode.IntegralHeight = false;
            this.cbDictCode.Location = new System.Drawing.Point(96, 191);
            this.cbDictCode.Margin = new System.Windows.Forms.Padding(4);
            this.cbDictCode.MaxDropDownItems = 0;
            this.cbDictCode.MaximumSize = new System.Drawing.Size(1000, 24);
            this.cbDictCode.MinimumSize = new System.Drawing.Size(40, 24);
            this.cbDictCode.Name = "cbDictCode";
            this.cbDictCode.SelectedIndex = -1;
            this.cbDictCode.SelectedValue = null;
            this.cbDictCode.Size = new System.Drawing.Size(202, 24);
            this.cbDictCode.TabIndex = 53;
            this.cbDictCode.ValueMember = null;
            this.cbDictCode.SelectedIndexChanged += new System.EventHandler(this.txtDifin_TextChanged);
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(95, 569);
            this.txtNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtNumber.MaxLength = 0;
            this.txtNumber.Multiline = false;
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.PasswordChar = '\0';
            this.txtNumber.ReadOnly = false;
            this.txtNumber.Size = new System.Drawing.Size(203, 24);
            this.txtNumber.TabIndex = 52;
            this.txtNumber.Visible = false;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(20, 30);
            this.label8.Margin = new System.Windows.Forms.Padding(4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 18);
            this.label8.TabIndex = 51;
            this.label8.Text = "护理类型";
            // 
            // bt_saveConfig
            // 
            this.bt_saveConfig.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bt_saveConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_saveConfig.Image = ((System.Drawing.Image)(resources.GetObject("bt_saveConfig.Image")));
            this.bt_saveConfig.ImageRight = false;
            this.bt_saveConfig.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.bt_saveConfig.Location = new System.Drawing.Point(218, 531);
            this.bt_saveConfig.Margin = new System.Windows.Forms.Padding(4);
            this.bt_saveConfig.MaximumSize = new System.Drawing.Size(100, 30);
            this.bt_saveConfig.MinimumSize = new System.Drawing.Size(80, 30);
            this.bt_saveConfig.Name = "bt_saveConfig";
            this.bt_saveConfig.Size = new System.Drawing.Size(80, 30);
            this.bt_saveConfig.TabIndex = 50;
            this.bt_saveConfig.TextValue = "保存";
            this.bt_saveConfig.UseVisualStyleBackColor = true;
            this.bt_saveConfig.Click += new System.EventHandler(this.bt_saveConfig_Click);
            // 
            // bt_delConfig
            // 
            this.bt_delConfig.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bt_delConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_delConfig.Image = ((System.Drawing.Image)(resources.GetObject("bt_delConfig.Image")));
            this.bt_delConfig.ImageRight = false;
            this.bt_delConfig.ImageStyle = HISPlus.UserControls.ImageStyle.Delete;
            this.bt_delConfig.Location = new System.Drawing.Point(119, 531);
            this.bt_delConfig.Margin = new System.Windows.Forms.Padding(4);
            this.bt_delConfig.MaximumSize = new System.Drawing.Size(100, 30);
            this.bt_delConfig.MinimumSize = new System.Drawing.Size(80, 30);
            this.bt_delConfig.Name = "bt_delConfig";
            this.bt_delConfig.Size = new System.Drawing.Size(80, 30);
            this.bt_delConfig.TabIndex = 49;
            this.bt_delConfig.TextValue = "删除";
            this.bt_delConfig.UseVisualStyleBackColor = true;
            this.bt_delConfig.Click += new System.EventHandler(this.bt_delConfig_Click);
            // 
            // bt_addConfig
            // 
            this.bt_addConfig.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bt_addConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_addConfig.Image = ((System.Drawing.Image)(resources.GetObject("bt_addConfig.Image")));
            this.bt_addConfig.ImageRight = false;
            this.bt_addConfig.ImageStyle = HISPlus.UserControls.ImageStyle.Add;
            this.bt_addConfig.Location = new System.Drawing.Point(20, 531);
            this.bt_addConfig.Margin = new System.Windows.Forms.Padding(4);
            this.bt_addConfig.MaximumSize = new System.Drawing.Size(100, 30);
            this.bt_addConfig.MinimumSize = new System.Drawing.Size(80, 30);
            this.bt_addConfig.Name = "bt_addConfig";
            this.bt_addConfig.Size = new System.Drawing.Size(80, 30);
            this.bt_addConfig.TabIndex = 48;
            this.bt_addConfig.TextValue = "新增";
            this.bt_addConfig.UseVisualStyleBackColor = true;
            this.bt_addConfig.Click += new System.EventHandler(this.bt_addConfig_Click);
            // 
            // ucGridView1
            // 
            this.ucGridView1.AllowAddRows = false;
            this.ucGridView1.AllowDeleteRows = false;
            this.ucGridView1.AllowEdit = false;
            this.ucGridView1.AllowSort = false;
            this.ucGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucGridView1.ColumnAutoWidth = true;
            this.ucGridView1.ColumnsEvenOldRowColor = null;
            this.ucGridView1.DataSource = null;
            this.ucGridView1.Location = new System.Drawing.Point(672, 20);
            this.ucGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucGridView1.Name = "ucGridView1";
            this.ucGridView1.ShowRowIndicator = false;
            this.ucGridView1.Size = new System.Drawing.Size(560, 756);
            this.ucGridView1.TabIndex = 5;
            // 
            // NursingConfigFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 789);
            this.Controls.Add(this.ucGridView1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NursingConfigFrm";
            this.Text = "护理记录单配置";
            this.Load += new System.EventHandler(this.NursingConfigFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDeptCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxVital)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl groupBox1;
        private HISPlus.UserControls.UcComboBox cb_storeCol;
        private HISPlus.UserControls.UcTextBox txtserial_no;
        private DevExpress.XtraEditors.LabelControl label14;
        private DevExpress.XtraEditors.LabelControl label15;
        private DevExpress.XtraEditors.LabelControl label16;
        private HISPlus.UserControls.UcTextBox txt_width;
        private DevExpress.XtraEditors.LabelControl label11;
        private DevExpress.XtraEditors.LabelControl label12;
        private DevExpress.XtraEditors.LabelControl label13;
        private DevExpress.XtraEditors.LabelControl label9;
        private DevExpress.XtraEditors.LabelControl label10;
        private HISPlus.UserControls.UcTextBox txtVital_code;
        private DevExpress.XtraEditors.LabelControl label7;
        private HISPlus.UserControls.UcComboBox cbType;
        private DevExpress.XtraEditors.LabelControl label6;
        private HISPlus.UserControls.UcTextBox txtTitle;
        private HISPlus.UserControls.UcButton btnSave;
        private HISPlus.UserControls.UcTextBox txtRecordType;
        private HISPlus.UserControls.UcButton btnDelete;
        private HISPlus.UserControls.UcButton btnNew;
        private HISPlus.UserControls.UcTextBox txtModuleName;
        private DevExpress.XtraEditors.LabelControl label1;
        private HISPlus.UserControls.UcTextBox txtModuleCode;
        private DevExpress.XtraEditors.LabelControl label2;
        private DevExpress.XtraEditors.LabelControl label5;
        private DevExpress.XtraEditors.LabelControl label3;
        private DevExpress.XtraEditors.LabelControl label4;
        private HISPlus.UserControls.UcComboBox cb_SIGN_FLAG;
        private HISPlus.UserControls.UcComboBox cb_MULTI_LINE;
        private HISPlus.UserControls.UcComboBox cb_MULTI_VALUE;
        private HISPlus.UserControls.UcComboBox cb_selectValue;
        private HISPlus.UserControls.UcTextBox txtRowCount;
        private DevExpress.XtraEditors.PanelControl groupBox3;
        private HISPlus.UserControls.UcButton bt_saveConfig;
        private HISPlus.UserControls.UcButton bt_delConfig;
        private HISPlus.UserControls.UcButton bt_addConfig;
        private DevExpress.XtraEditors.LabelControl label8;
        private HISPlus.UserControls.UcTextBox txtNumber;
        private DevExpress.XtraEditors.LabelControl label17;
        private HISPlus.UserControls.UcComboBox cbDictCode;
        private DevExpress.XtraEditors.LabelControl label19;
        private UserControls.UcGridView ucGridView1;
        private UserControls.UcGridView ucGvType;
        private DevExpress.XtraEditors.CheckedListBoxControl cbxVital;
        private DevExpress.XtraEditors.CheckedListBoxControl cbxDeptCode;
    }
}