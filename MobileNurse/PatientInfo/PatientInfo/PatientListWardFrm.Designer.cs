namespace HISPlus
{
    partial class PatientListWardFrm
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.cmnuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.BED_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BED_LABEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VISIT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PATIENT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblPatientId = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.lblBedNo = new DevExpress.XtraEditors.LabelControl();
            this.lblInpDate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.lblSex = new DevExpress.XtraEditors.LabelControl();
            this.lblAge = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.ucGridView1 = new HISPlus.UserControls.UcGridView();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuRefresh});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 28);
            // 
            // cmnuRefresh
            // 
            this.cmnuRefresh.Name = "cmnuRefresh";
            this.cmnuRefresh.Size = new System.Drawing.Size(108, 24);
            this.cmnuRefresh.Text = "刷新";
            this.cmnuRefresh.Click += new System.EventHandler(this.cmnuRefresh_Click);
            // 
            // BED_NO
            // 
            this.BED_NO.HeaderText = "床号";
            this.BED_NO.Name = "BED_NO";
            this.BED_NO.ReadOnly = true;
            this.BED_NO.Visible = false;
            // 
            // NAME
            // 
            this.NAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NAME.DataPropertyName = "NAME";
            this.NAME.HeaderText = "姓名";
            this.NAME.Name = "NAME";
            this.NAME.ReadOnly = true;
            // 
            // BED_LABEL
            // 
            this.BED_LABEL.DataPropertyName = "BED_LABEL";
            this.BED_LABEL.Frozen = true;
            this.BED_LABEL.HeaderText = "床标";
            this.BED_LABEL.Name = "BED_LABEL";
            this.BED_LABEL.ReadOnly = true;
            this.BED_LABEL.Width = 50;
            // 
            // VISIT_ID
            // 
            this.VISIT_ID.DataPropertyName = "VISIT_ID";
            this.VISIT_ID.Frozen = true;
            this.VISIT_ID.HeaderText = "就诊序号";
            this.VISIT_ID.Name = "VISIT_ID";
            this.VISIT_ID.ReadOnly = true;
            this.VISIT_ID.Visible = false;
            // 
            // PATIENT_ID
            // 
            this.PATIENT_ID.DataPropertyName = "PATIENT_ID";
            this.PATIENT_ID.Frozen = true;
            this.PATIENT_ID.HeaderText = "病人ID";
            this.PATIENT_ID.Name = "PATIENT_ID";
            this.PATIENT_ID.ReadOnly = true;
            this.PATIENT_ID.Visible = false;
            // 
            // Column1
            // 
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            this.Column1.Width = 5;
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lookUpEdit1.Location = new System.Drawing.Point(0, 612);
            this.lookUpEdit1.Margin = new System.Windows.Forms.Padding(3, 12, 3, 2);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Size = new System.Drawing.Size(235, 24);
            this.lookUpEdit1.TabIndex = 7;
            this.lookUpEdit1.EditValueChanged += new System.EventHandler(this.lookUpEdit1_EditValueChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblPatientId);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.lblBedNo);
            this.panelControl1.Controls.Add(this.lblInpDate);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.lblSex);
            this.panelControl1.Controls.Add(this.lblAge);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 49);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(235, 91);
            this.panelControl1.TabIndex = 9;
            // 
            // lblPatientId
            // 
            this.lblPatientId.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblPatientId.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPatientId.Location = new System.Drawing.Point(61, 11);
            this.lblPatientId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblPatientId.Name = "lblPatientId";
            this.lblPatientId.Size = new System.Drawing.Size(30, 18);
            this.lblPatientId.TabIndex = 67;
            this.lblPatientId.Text = "床号";
            this.lblPatientId.ToolTip = "双击复制";
            this.lblPatientId.DoubleClick += new System.EventHandler(this.lblPatientId_DoubleClick);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(9, 11);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(46, 18);
            this.labelControl6.TabIndex = 66;
            this.labelControl6.Text = "病人ID";
            // 
            // lblBedNo
            // 
            this.lblBedNo.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblBedNo.Location = new System.Drawing.Point(168, 11);
            this.lblBedNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblBedNo.Name = "lblBedNo";
            this.lblBedNo.Size = new System.Drawing.Size(30, 18);
            this.lblBedNo.TabIndex = 65;
            this.lblBedNo.Text = "床号";
            // 
            // lblInpDate
            // 
            this.lblInpDate.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblInpDate.Location = new System.Drawing.Point(75, 59);
            this.lblInpDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblInpDate.Name = "lblInpDate";
            this.lblInpDate.Size = new System.Drawing.Size(60, 18);
            this.lblInpDate.TabIndex = 65;
            this.lblInpDate.Text = "入院日期";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(9, 59);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 18);
            this.labelControl5.TabIndex = 65;
            this.labelControl5.Text = "入院日期";
            // 
            // lblSex
            // 
            this.lblSex.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblSex.Location = new System.Drawing.Point(168, 35);
            this.lblSex.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(30, 18);
            this.lblSex.TabIndex = 65;
            this.lblSex.Text = "性别";
            // 
            // lblAge
            // 
            this.lblAge.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblAge.Location = new System.Drawing.Point(61, 35);
            this.lblAge.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(30, 18);
            this.lblAge.TabIndex = 65;
            this.lblAge.Text = "年龄";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(9, 35);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(30, 18);
            this.labelControl4.TabIndex = 65;
            this.labelControl4.Text = "年龄";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(134, 35);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(30, 18);
            this.labelControl3.TabIndex = 65;
            this.labelControl3.Text = "性别";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(134, 11);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(30, 18);
            this.labelControl1.TabIndex = 65;
            this.labelControl1.Text = "床号";
            // 
            // ucGridView1
            // 
            this.ucGridView1.AllowDeleteRows = false;
            this.ucGridView1.AllowEdit = false;
            this.ucGridView1.AllowSort = false;
            this.ucGridView1.DataSource = null;
            this.ucGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGridView1.Location = new System.Drawing.Point(0, 140);
            this.ucGridView1.Margin = new System.Windows.Forms.Padding(45, 28, 45, 28);
            this.ucGridView1.MultiSelect = false;
            this.ucGridView1.Name = "ucGridView1";
            this.ucGridView1.ShowRowIndicator = false;
            this.ucGridView1.Size = new System.Drawing.Size(235, 472);
            this.ucGridView1.TabIndex = 8;
            // 
            // popupMenu
            // 
            this.popupMenu.Manager = this.barManager1;
            this.popupMenu.Name = "popupMenu";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 0;
            this.barManager1.StatusBar = this.bar3;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(235, 49);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 636);
            this.barDockControlBottom.Size = new System.Drawing.Size(235, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 49);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 587);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(235, 49);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 587);
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.Text = "Tools";
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // PatientListWardFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 659);
            this.Controls.Add(this.ucGridView1);
            this.Controls.Add(this.lookUpEdit1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PatientListWardFrm";
            this.Text = "病区病人列表";
            this.Load += new System.EventHandler(this.PatientListWardFrm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion        
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmnuRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn BED_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn BED_LABEL;
        private System.Windows.Forms.DataGridViewTextBoxColumn VISIT_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PATIENT_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private UserControls.UcGridView ucGridView1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblBedNo;
        private DevExpress.XtraEditors.LabelControl lblInpDate;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl lblSex;
        private DevExpress.XtraEditors.LabelControl lblAge;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblPatientId;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}
