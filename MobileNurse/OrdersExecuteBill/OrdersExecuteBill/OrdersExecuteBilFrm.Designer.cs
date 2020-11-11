namespace HISPlus
{
    partial class OrdersExecuteBilFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrdersExecuteBilFrm));
            this.grpFunction = new DevExpress.XtraEditors.PanelControl();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.dtpTime1 = new System.Windows.Forms.DateTimePicker();
            this.lblInfo = new DevExpress.XtraEditors.LabelControl();
            this.dtpTime0 = new System.Windows.Forms.DateTimePicker();
            this.btnPrint = new HISPlus.UserControls.UcButton();
            this.dtRngStart = new System.Windows.Forms.DateTimePicker();
            this.btnQuery = new HISPlus.UserControls.UcButton();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.grpMain = new DevExpress.XtraEditors.PanelControl();
            this.ucGridView1 = new HISPlus.UserControls.UcCheckGridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.chkLongTerm = new System.Windows.Forms.CheckBox();
            this.chkTempTerm = new System.Windows.Forms.CheckBox();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.chkPrinted = new System.Windows.Forms.CheckBox();
            this.chkNotPrinted = new System.Windows.Forms.CheckBox();
            this.grpAdmin = new DevExpress.XtraEditors.PanelControl();
            this.lvwAdministration = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.grpFunction)).BeginInit();
            this.grpFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpMain)).BeginInit();
            this.grpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpAdmin)).BeginInit();
            this.grpAdmin.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpFunction
            // 
            this.grpFunction.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.grpFunction.Appearance.Options.UseBackColor = true;
            this.grpFunction.Controls.Add(this.label2);
            this.grpFunction.Controls.Add(this.dtpTime1);
            this.grpFunction.Controls.Add(this.lblInfo);
            this.grpFunction.Controls.Add(this.dtpTime0);
            this.grpFunction.Controls.Add(this.btnPrint);
            this.grpFunction.Controls.Add(this.dtRngStart);
            this.grpFunction.Controls.Add(this.btnQuery);
            this.grpFunction.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpFunction.Location = new System.Drawing.Point(0, 533);
            this.grpFunction.Name = "grpFunction";
            this.grpFunction.Padding = new System.Windows.Forms.Padding(3);
            this.grpFunction.Size = new System.Drawing.Size(890, 58);
            this.grpFunction.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(229, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(4, 14);
            this.label2.TabIndex = 7;
            this.label2.Text = "-";
            // 
            // dtpTime1
            // 
            this.dtpTime1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime1.Location = new System.Drawing.Point(241, 20);
            this.dtpTime1.Name = "dtpTime1";
            this.dtpTime1.ShowUpDown = true;
            this.dtpTime1.Size = new System.Drawing.Size(85, 22);
            this.dtpTime1.TabIndex = 6;
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfo.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(777, 24);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(84, 14);
            this.lblInfo.TabIndex = 6;
            this.lblInfo.Text = "医嘱拆分有错误";
            this.lblInfo.Visible = false;
            // 
            // dtpTime0
            // 
            this.dtpTime0.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime0.Location = new System.Drawing.Point(142, 20);
            this.dtpTime0.Name = "dtpTime0";
            this.dtpTime0.ShowUpDown = true;
            this.dtpTime0.Size = new System.Drawing.Size(85, 22);
            this.dtpTime0.TabIndex = 5;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPrint.Enabled = false;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageRight = false;
            this.btnPrint.ImageStyle = HISPlus.UserControls.ImageStyle.Print;
            this.btnPrint.Location = new System.Drawing.Point(622, 20);
            this.btnPrint.MaximumSize = new System.Drawing.Size(72, 24);
            this.btnPrint.MinimumSize = new System.Drawing.Size(48, 24);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(72, 24);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.TextValue = "打印";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dtRngStart
            // 
            this.dtRngStart.Location = new System.Drawing.Point(10, 20);
            this.dtRngStart.Name = "dtRngStart";
            this.dtRngStart.Size = new System.Drawing.Size(120, 22);
            this.dtRngStart.TabIndex = 1;
            this.dtRngStart.ValueChanged += new System.EventHandler(this.dtRngStart_ValueChanged);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnQuery.Enabled = false;
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.ImageRight = false;
            this.btnQuery.ImageStyle = HISPlus.UserControls.ImageStyle.Find;
            this.btnQuery.Location = new System.Drawing.Point(474, 20);
            this.btnQuery.MaximumSize = new System.Drawing.Size(72, 24);
            this.btnQuery.MinimumSize = new System.Drawing.Size(48, 24);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(72, 24);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.TextValue = "查询(&Q)";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(14, 6);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(50, 18);
            this.chkAll.TabIndex = 0;
            this.chkAll.Text = "全科";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // grpMain
            // 
            this.grpMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMain.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.grpMain.Appearance.Options.UseBackColor = true;
            this.grpMain.Controls.Add(this.ucGridView1);
            this.grpMain.Controls.Add(this.panelControl1);
            this.grpMain.Location = new System.Drawing.Point(0, 0);
            this.grpMain.Name = "grpMain";
            this.grpMain.Padding = new System.Windows.Forms.Padding(3);
            this.grpMain.Size = new System.Drawing.Size(890, 441);
            this.grpMain.TabIndex = 2;
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
            this.ucGridView1.Location = new System.Drawing.Point(7, 46);
            this.ucGridView1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ucGridView1.Name = "ucGridView1";
            this.ucGridView1.ShowRowIndicator = false;
            this.ucGridView1.Size = new System.Drawing.Size(876, 392);
            this.ucGridView1.TabIndex = 8;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.chkLongTerm);
            this.panelControl1.Controls.Add(this.chkAll);
            this.panelControl1.Controls.Add(this.chkTempTerm);
            this.panelControl1.Controls.Add(this.chkSelectAll);
            this.panelControl1.Controls.Add(this.chkPrinted);
            this.panelControl1.Controls.Add(this.chkNotPrinted);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(5, 5);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(880, 33);
            this.panelControl1.TabIndex = 7;
            // 
            // chkLongTerm
            // 
            this.chkLongTerm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLongTerm.AutoSize = true;
            this.chkLongTerm.Checked = true;
            this.chkLongTerm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLongTerm.Location = new System.Drawing.Point(758, 6);
            this.chkLongTerm.Name = "chkLongTerm";
            this.chkLongTerm.Size = new System.Drawing.Size(50, 18);
            this.chkLongTerm.TabIndex = 1;
            this.chkLongTerm.Text = "长期";
            this.chkLongTerm.UseVisualStyleBackColor = true;
            this.chkLongTerm.CheckedChanged += new System.EventHandler(this.chkLongTerm_CheckedChanged);
            // 
            // chkTempTerm
            // 
            this.chkTempTerm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTempTerm.AutoSize = true;
            this.chkTempTerm.Checked = true;
            this.chkTempTerm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTempTerm.Location = new System.Drawing.Point(825, 6);
            this.chkTempTerm.Name = "chkTempTerm";
            this.chkTempTerm.Size = new System.Drawing.Size(50, 18);
            this.chkTempTerm.TabIndex = 2;
            this.chkTempTerm.Text = "临时";
            this.chkTempTerm.UseVisualStyleBackColor = true;
            this.chkTempTerm.CheckedChanged += new System.EventHandler(this.chkLongTerm_CheckedChanged);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Checked = true;
            this.chkSelectAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelectAll.Location = new System.Drawing.Point(519, 6);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(50, 18);
            this.chkSelectAll.TabIndex = 0;
            this.chkSelectAll.Text = "全选";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.Visible = false;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // chkPrinted
            // 
            this.chkPrinted.AutoSize = true;
            this.chkPrinted.Checked = true;
            this.chkPrinted.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrinted.Location = new System.Drawing.Point(666, 6);
            this.chkPrinted.Name = "chkPrinted";
            this.chkPrinted.Size = new System.Drawing.Size(62, 18);
            this.chkPrinted.TabIndex = 5;
            this.chkPrinted.Text = "已打印";
            this.chkPrinted.UseVisualStyleBackColor = true;
            this.chkPrinted.CheckedChanged += new System.EventHandler(this.chkLongTerm_CheckedChanged);
            // 
            // chkNotPrinted
            // 
            this.chkNotPrinted.AutoSize = true;
            this.chkNotPrinted.Checked = true;
            this.chkNotPrinted.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNotPrinted.Location = new System.Drawing.Point(586, 6);
            this.chkNotPrinted.Name = "chkNotPrinted";
            this.chkNotPrinted.Size = new System.Drawing.Size(62, 18);
            this.chkNotPrinted.TabIndex = 4;
            this.chkNotPrinted.Text = "未打印";
            this.chkNotPrinted.UseVisualStyleBackColor = true;
            this.chkNotPrinted.CheckedChanged += new System.EventHandler(this.chkLongTerm_CheckedChanged);
            // 
            // grpAdmin
            // 
            this.grpAdmin.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.grpAdmin.Appearance.Options.UseBackColor = true;
            this.grpAdmin.Controls.Add(this.lvwAdministration);
            this.grpAdmin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpAdmin.Location = new System.Drawing.Point(0, 439);
            this.grpAdmin.Name = "grpAdmin";
            this.grpAdmin.Padding = new System.Windows.Forms.Padding(3);
            this.grpAdmin.Size = new System.Drawing.Size(890, 94);
            this.grpAdmin.TabIndex = 3;
            // 
            // lvwAdministration
            // 
            this.lvwAdministration.CheckBoxes = true;
            this.lvwAdministration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwAdministration.Location = new System.Drawing.Point(5, 5);
            this.lvwAdministration.Name = "lvwAdministration";
            this.lvwAdministration.Size = new System.Drawing.Size(880, 84);
            this.lvwAdministration.TabIndex = 0;
            this.lvwAdministration.UseCompatibleStateImageBehavior = false;
            this.lvwAdministration.View = System.Windows.Forms.View.SmallIcon;
            this.lvwAdministration.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvwAdministration_ItemChecked);
            // 
            // OrdersExecuteBilFrm
            // 
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(890, 591);
            this.Controls.Add(this.grpMain);
            this.Controls.Add(this.grpAdmin);
            this.Controls.Add(this.grpFunction);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OrdersExecuteBilFrm";
            this.Text = "输液单打印";
            this.Load += new System.EventHandler(this.OrdersExecuteBilFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpFunction)).EndInit();
            this.grpFunction.ResumeLayout(false);
            this.grpFunction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpMain)).EndInit();
            this.grpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpAdmin)).EndInit();
            this.grpAdmin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl grpFunction;
        private System.Windows.Forms.DateTimePicker dtRngStart;
        private DevExpress.XtraEditors.PanelControl grpMain;
        private HISPlus.UserControls.UcButton btnPrint;
        private HISPlus.UserControls.UcButton btnQuery;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.CheckBox chkLongTerm;
        private System.Windows.Forms.CheckBox chkTempTerm;
        private DevExpress.XtraEditors.LabelControl label2;
        private System.Windows.Forms.DateTimePicker dtpTime1;
        private System.Windows.Forms.DateTimePicker dtpTime0;
        private DevExpress.XtraEditors.PanelControl grpAdmin;
        private System.Windows.Forms.ListView lvwAdministration;
        private System.Windows.Forms.CheckBox chkNotPrinted;
        private System.Windows.Forms.CheckBox chkPrinted;
        private DevExpress.XtraEditors.LabelControl lblInfo;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private UserControls.UcCheckGridView ucGridView1;
    }
}