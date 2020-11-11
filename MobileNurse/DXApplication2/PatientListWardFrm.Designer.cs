namespace DXApplication2
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.cmenuQueryPat = new System.Windows.Forms.ToolStripMenuItem();
            this.BED_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BED_LABEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VISIT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PATIENT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.cmbDeptList = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuRefresh,
            this.cmenuQueryPat});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // cmnuRefresh
            // 
            this.cmnuRefresh.Name = "cmnuRefresh";
            this.cmnuRefresh.Size = new System.Drawing.Size(124, 22);
            this.cmnuRefresh.Text = "刷新列表";
            this.cmnuRefresh.Click += new System.EventHandler(this.cmnuRefresh_Click);
            // 
            // cmenuQueryPat
            // 
            this.cmenuQueryPat.Name = "cmenuQueryPat";
            this.cmenuQueryPat.Size = new System.Drawing.Size(124, 22);
            this.cmenuQueryPat.Text = "查询病人";
            this.cmenuQueryPat.Click += new System.EventHandler(this.cmenuQueryPat_Click);
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
            // panelControl1
            // 
            this.panelControl1.ContextMenuStrip = this.contextMenuStrip1;
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
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(194, 85);
            this.panelControl1.TabIndex = 9;
            // 
            // lblPatientId
            // 
            this.lblPatientId.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblPatientId.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPatientId.Location = new System.Drawing.Point(53, 10);
            this.lblPatientId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblPatientId.Name = "lblPatientId";
            this.lblPatientId.Size = new System.Drawing.Size(24, 14);
            this.lblPatientId.TabIndex = 67;
            this.lblPatientId.Text = "床号";
            this.lblPatientId.ToolTip = "双击复制";
            this.lblPatientId.DoubleClick += new System.EventHandler(this.lblPatientId_DoubleClick);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(8, 10);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(36, 14);
            this.labelControl6.TabIndex = 66;
            this.labelControl6.Text = "病人ID";
            // 
            // lblBedNo
            // 
            this.lblBedNo.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblBedNo.Location = new System.Drawing.Point(147, 10);
            this.lblBedNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblBedNo.Name = "lblBedNo";
            this.lblBedNo.Size = new System.Drawing.Size(24, 14);
            this.lblBedNo.TabIndex = 65;
            this.lblBedNo.Text = "床号";
            // 
            // lblInpDate
            // 
            this.lblInpDate.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblInpDate.Location = new System.Drawing.Point(66, 55);
            this.lblInpDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblInpDate.Name = "lblInpDate";
            this.lblInpDate.Size = new System.Drawing.Size(48, 14);
            this.lblInpDate.TabIndex = 65;
            this.lblInpDate.Text = "入院日期";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(8, 55);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 65;
            this.labelControl5.Text = "入院日期";
            // 
            // lblSex
            // 
            this.lblSex.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblSex.Location = new System.Drawing.Point(147, 33);
            this.lblSex.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(24, 14);
            this.lblSex.TabIndex = 65;
            this.lblSex.Text = "性别";
            // 
            // lblAge
            // 
            this.lblAge.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblAge.Location = new System.Drawing.Point(53, 33);
            this.lblAge.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(24, 14);
            this.lblAge.TabIndex = 65;
            this.lblAge.Text = "年龄";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(8, 33);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 14);
            this.labelControl4.TabIndex = 65;
            this.labelControl4.Text = "年龄";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(117, 33);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 14);
            this.labelControl3.TabIndex = 65;
            this.labelControl3.Text = "性别";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(117, 10);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 65;
            this.labelControl1.Text = "床号";
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
            this.ucGridView1.Location = new System.Drawing.Point(2, 2);
            this.ucGridView1.Margin = new System.Windows.Forms.Padding(38, 26, 38, 26);
            this.ucGridView1.Name = "ucGridView1";
            this.ucGridView1.ShowRowIndicator = false;
            this.ucGridView1.Size = new System.Drawing.Size(190, 501);
            this.ucGridView1.TabIndex = 8;
            this.ucGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ucGridView1_MouseDown);
            // 
            // popupMenu
            // 
            this.popupMenu.Manager = this.barManager1;
            this.popupMenu.Name = "popupMenu";
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.MaxItemId = 0;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(194, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 619);
            this.barDockControlBottom.Size = new System.Drawing.Size(194, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 619);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(194, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 619);
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.ucGridView1);
            this.panelControl2.Location = new System.Drawing.Point(0, 90);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(194, 505);
            this.panelControl2.TabIndex = 14;
            // 
            // cmbDeptList
            // 
            this.cmbDeptList.BackColor = System.Drawing.Color.Thistle;
            this.cmbDeptList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cmbDeptList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeptList.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cmbDeptList.FormattingEnabled = true;
            this.cmbDeptList.Location = new System.Drawing.Point(0, 592);
            this.cmbDeptList.Name = "cmbDeptList";
            this.cmbDeptList.Size = new System.Drawing.Size(194, 27);
            this.cmbDeptList.TabIndex = 9;
            this.cmbDeptList.SelectedIndexChanged += new System.EventHandler(this.cmbDeptList_SelectedIndexChanged);
            // 
            // PatientListWardFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 619);
            this.Controls.Add(this.cmbDeptList);
            this.Controls.Add(this.panelControl2);
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
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
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
        private HISPlus.UserControls.UcGridView ucGridView1;
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
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.ToolStripMenuItem cmenuQueryPat;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.ComboBox cmbDeptList;
    }
}