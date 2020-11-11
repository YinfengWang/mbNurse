namespace HISPlus
{
    partial class NursingReportFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NursingReportFrm));
            this.contextMnu_VitalSigns = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuVitalSigns_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVitalSigns_InsertPageBreak = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVitalSigns_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuVitalSigns_AddRec = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVitalSigns_AddLine = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVitalSigns_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVitalSigns_Sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuVitalSigns_Group = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVitalSigns_Sum = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.auditPass = new System.Windows.Forms.ToolStripMenuItem();
            this.auditNoPass = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.insNursingTransfer = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery = new HISPlus.UserControls.UcButton();
            this.dtSearchStart = new System.Windows.Forms.DateTimePicker();
            this.btnRePrint = new HISPlus.UserControls.UcButton();
            this.label3 = new DevExpress.XtraEditors.LabelControl();
            this.grpSeach = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblBedNo = new System.Windows.Forms.Label();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.lbl_ADMISSION_DATE_TIME = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.cmbNursingRecord = new HISPlus.UserControls.UcComboBox();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.dtSearchEnd = new System.Windows.Forms.DateTimePicker();
            this.ucGridView1 = new HISPlus.UserControls.UcGridView();
            this.contextMnu_VitalSigns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpSeach)).BeginInit();
            this.grpSeach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMnu_VitalSigns
            // 
            this.contextMnu_VitalSigns.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuVitalSigns_Import,
            this.mnuVitalSigns_InsertPageBreak,
            this.mnuVitalSigns_Sep1,
            this.mnuVitalSigns_AddRec,
            this.mnuVitalSigns_AddLine,
            this.mnuVitalSigns_Delete,
            this.mnuVitalSigns_Sep2,
            this.mnuVitalSigns_Group,
            this.mnuVitalSigns_Sum,
            this.toolStripMenuItem1,
            this.auditPass,
            this.auditNoPass,
            this.toolStripMenuItem2,
            this.insNursingTransfer});
            this.contextMnu_VitalSigns.Name = "contextMnu_VitalSigns";
            this.contextMnu_VitalSigns.ShowImageMargin = false;
            this.contextMnu_VitalSigns.Size = new System.Drawing.Size(160, 248);
            this.contextMnu_VitalSigns.Opening += new System.ComponentModel.CancelEventHandler(this.contextMnu_VitalSigns_Opening);
            // 
            // mnuVitalSigns_Import
            // 
            this.mnuVitalSigns_Import.Name = "mnuVitalSigns_Import";
            this.mnuVitalSigns_Import.Size = new System.Drawing.Size(159, 22);
            this.mnuVitalSigns_Import.Text = "导入生命体征及输液";
            this.mnuVitalSigns_Import.Click += new System.EventHandler(this.mnuVitalSigns_Import_Click);
            // 
            // mnuVitalSigns_InsertPageBreak
            // 
            this.mnuVitalSigns_InsertPageBreak.Name = "mnuVitalSigns_InsertPageBreak";
            this.mnuVitalSigns_InsertPageBreak.Size = new System.Drawing.Size(159, 22);
            this.mnuVitalSigns_InsertPageBreak.Text = "插入/撤消分页符";
            this.mnuVitalSigns_InsertPageBreak.Visible = false;
            // 
            // mnuVitalSigns_Sep1
            // 
            this.mnuVitalSigns_Sep1.Name = "mnuVitalSigns_Sep1";
            this.mnuVitalSigns_Sep1.Size = new System.Drawing.Size(156, 6);
            // 
            // mnuVitalSigns_AddRec
            // 
            this.mnuVitalSigns_AddRec.Name = "mnuVitalSigns_AddRec";
            this.mnuVitalSigns_AddRec.Size = new System.Drawing.Size(159, 22);
            this.mnuVitalSigns_AddRec.Text = "插入新记录";
            this.mnuVitalSigns_AddRec.Click += new System.EventHandler(this.mnuVitalSigns_AddRec_Click);
            // 
            // mnuVitalSigns_AddLine
            // 
            this.mnuVitalSigns_AddLine.Name = "mnuVitalSigns_AddLine";
            this.mnuVitalSigns_AddLine.Size = new System.Drawing.Size(159, 22);
            this.mnuVitalSigns_AddLine.Text = "追加新行";
            this.mnuVitalSigns_AddLine.Click += new System.EventHandler(this.mnuVitalSigns_AddLine_Click);
            // 
            // mnuVitalSigns_Delete
            // 
            this.mnuVitalSigns_Delete.Name = "mnuVitalSigns_Delete";
            this.mnuVitalSigns_Delete.Size = new System.Drawing.Size(159, 22);
            this.mnuVitalSigns_Delete.Text = "删除";
            this.mnuVitalSigns_Delete.Click += new System.EventHandler(this.mnuVitalSigns_Delete_Click);
            // 
            // mnuVitalSigns_Sep2
            // 
            this.mnuVitalSigns_Sep2.Name = "mnuVitalSigns_Sep2";
            this.mnuVitalSigns_Sep2.Size = new System.Drawing.Size(156, 6);
            // 
            // mnuVitalSigns_Group
            // 
            this.mnuVitalSigns_Group.Name = "mnuVitalSigns_Group";
            this.mnuVitalSigns_Group.Size = new System.Drawing.Size(159, 22);
            this.mnuVitalSigns_Group.Text = "分组";
            this.mnuVitalSigns_Group.Click += new System.EventHandler(this.mnuVitalSigns_Group_Click);
            // 
            // mnuVitalSigns_Sum
            // 
            this.mnuVitalSigns_Sum.Name = "mnuVitalSigns_Sum";
            this.mnuVitalSigns_Sum.Size = new System.Drawing.Size(159, 22);
            this.mnuVitalSigns_Sum.Text = "复制求和";
            this.mnuVitalSigns_Sum.Click += new System.EventHandler(this.mnuVitalSigns_Sum_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(156, 6);
            // 
            // auditPass
            // 
            this.auditPass.Name = "auditPass";
            this.auditPass.Size = new System.Drawing.Size(159, 22);
            this.auditPass.Text = "审核通过";
            this.auditPass.Click += new System.EventHandler(this.auditPass_Click);
            // 
            // auditNoPass
            // 
            this.auditNoPass.Name = "auditNoPass";
            this.auditNoPass.Size = new System.Drawing.Size(159, 22);
            this.auditNoPass.Text = "审核未通过";
            this.auditNoPass.Click += new System.EventHandler(this.auditNoPass_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(156, 6);
            // 
            // insNursingTransfer
            // 
            this.insNursingTransfer.Name = "insNursingTransfer";
            this.insNursingTransfer.Size = new System.Drawing.Size(159, 22);
            this.insNursingTransfer.Text = "插入交班报告";
            this.insNursingTransfer.Click += new System.EventHandler(this.insNursingTransfer_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(319, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 14);
            this.label2.TabIndex = 19;
            this.label2.Text = "记录单：";
            // 
            // btnQuery
            // 
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.ImageRight = false;
            this.btnQuery.ImageStyle = HISPlus.UserControls.ImageStyle.Find;
            this.btnQuery.Location = new System.Drawing.Point(319, 64);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnQuery.MaximumSize = new System.Drawing.Size(79, 60);
            this.btnQuery.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(52, 26);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.TextValue = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // dtSearchStart
            // 
            this.dtSearchStart.Location = new System.Drawing.Point(48, 33);
            this.dtSearchStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dtSearchStart.Name = "dtSearchStart";
            this.dtSearchStart.Size = new System.Drawing.Size(111, 22);
            this.dtSearchStart.TabIndex = 1;
            // 
            // btnRePrint
            // 
            this.btnRePrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRePrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRePrint.Image = ((System.Drawing.Image)(resources.GetObject("btnRePrint.Image")));
            this.btnRePrint.ImageRight = false;
            this.btnRePrint.ImageStyle = HISPlus.UserControls.ImageStyle.Print;
            this.btnRePrint.Location = new System.Drawing.Point(379, 64);
            this.btnRePrint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRePrint.MaximumSize = new System.Drawing.Size(79, 50);
            this.btnRePrint.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnRePrint.Name = "btnRePrint";
            this.btnRePrint.Size = new System.Drawing.Size(54, 26);
            this.btnRePrint.TabIndex = 4;
            this.btnRePrint.TextValue = "打印";
            this.btnRePrint.UseVisualStyleBackColor = true;
            this.btnRePrint.Click += new System.EventHandler(this.btnRePrint_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 36);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "日期:";
            // 
            // grpSeach
            // 
            this.grpSeach.Controls.Add(this.groupControl1);
            this.grpSeach.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpSeach.Location = new System.Drawing.Point(0, 0);
            this.grpSeach.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpSeach.Name = "grpSeach";
            this.grpSeach.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpSeach.Size = new System.Drawing.Size(1088, 99);
            this.grpSeach.TabIndex = 17;
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.lblBedNo);
            this.groupControl1.Controls.Add(this.btnSave);
            this.groupControl1.Controls.Add(this.lbl_ADMISSION_DATE_TIME);
            this.groupControl1.Controls.Add(this.btnQuery);
            this.groupControl1.Controls.Add(this.lblName);
            this.groupControl1.Controls.Add(this.cmbNursingRecord);
            this.groupControl1.Controls.Add(this.dtSearchStart);
            this.groupControl1.Controls.Add(this.btnRePrint);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.dtSearchEnd);
            this.groupControl1.Location = new System.Drawing.Point(3, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1085, 95);
            this.groupControl1.TabIndex = 18;
            this.groupControl1.Text = "操作";
            // 
            // lblBedNo
            // 
            this.lblBedNo.AutoSize = true;
            this.lblBedNo.BackColor = System.Drawing.Color.Transparent;
            this.lblBedNo.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblBedNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblBedNo.Location = new System.Drawing.Point(13, 68);
            this.lblBedNo.Name = "lblBedNo";
            this.lblBedNo.Size = new System.Drawing.Size(35, 14);
            this.lblBedNo.TabIndex = 0;
            this.lblBedNo.Text = "床号:";
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageRight = false;
            this.btnSave.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSave.Location = new System.Drawing.Point(441, 64);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.MaximumSize = new System.Drawing.Size(79, 50);
            this.btnSave.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(54, 26);
            this.btnSave.TabIndex = 6;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lbl_ADMISSION_DATE_TIME
            // 
            this.lbl_ADMISSION_DATE_TIME.AutoSize = true;
            this.lbl_ADMISSION_DATE_TIME.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbl_ADMISSION_DATE_TIME.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lbl_ADMISSION_DATE_TIME.Location = new System.Drawing.Point(166, 68);
            this.lbl_ADMISSION_DATE_TIME.Name = "lbl_ADMISSION_DATE_TIME";
            this.lbl_ADMISSION_DATE_TIME.Size = new System.Drawing.Size(59, 14);
            this.lbl_ADMISSION_DATE_TIME.TabIndex = 1;
            this.lbl_ADMISSION_DATE_TIME.Text = "入院日期:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblName.Location = new System.Drawing.Point(82, 68);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 14);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "姓名:";
            // 
            // cmbNursingRecord
            // 
            this.cmbNursingRecord.DataSource = null;
            this.cmbNursingRecord.DisplayMember = null;
            this.cmbNursingRecord.DropDownHeight = 0;
            this.cmbNursingRecord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNursingRecord.DropDownWidth = 0;
            this.cmbNursingRecord.DroppedDown = false;
            this.cmbNursingRecord.FormattingEnabled = true;
            this.cmbNursingRecord.IntegralHeight = true;
            this.cmbNursingRecord.Location = new System.Drawing.Point(371, 32);
            this.cmbNursingRecord.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbNursingRecord.MaxDropDownItems = 0;
            this.cmbNursingRecord.MaximumSize = new System.Drawing.Size(875, 19);
            this.cmbNursingRecord.MinimumSize = new System.Drawing.Size(35, 19);
            this.cmbNursingRecord.Name = "cmbNursingRecord";
            this.cmbNursingRecord.SelectedIndex = -1;
            this.cmbNursingRecord.SelectedValue = null;
            this.cmbNursingRecord.Size = new System.Drawing.Size(277, 19);
            this.cmbNursingRecord.TabIndex = 20;
            this.cmbNursingRecord.ValueMember = null;
            this.cmbNursingRecord.SelectedIndexChanged += new System.EventHandler(this.cmbNursingRecord_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(165, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(4, 14);
            this.label1.TabIndex = 10;
            this.label1.Text = "-";
            // 
            // dtSearchEnd
            // 
            this.dtSearchEnd.Location = new System.Drawing.Point(173, 33);
            this.dtSearchEnd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dtSearchEnd.Name = "dtSearchEnd";
            this.dtSearchEnd.Size = new System.Drawing.Size(111, 22);
            this.dtSearchEnd.TabIndex = 9;
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
            this.ucGridView1.Location = new System.Drawing.Point(0, 99);
            this.ucGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucGridView1.Name = "ucGridView1";
            this.ucGridView1.ShowRowIndicator = false;
            this.ucGridView1.Size = new System.Drawing.Size(1088, 477);
            this.ucGridView1.TabIndex = 2;
            this.ucGridView1.DoubleClick += new System.EventHandler(this.ucGridView1_DoubleClick);
            this.ucGridView1.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.ucGridView1_ShowingEditor);
            this.ucGridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.ucGridView1_CellValueChanged);
            this.ucGridView1.RowStyle2 += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.ucGridView1_RowStyle);
            // 
            // NursingReportFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 576);
            this.Controls.Add(this.ucGridView1);
            this.Controls.Add(this.grpSeach);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "NursingReportFrm";
            this.Text = "自定义护理记录单";
            this.Load += new System.EventHandler(this.NursingReport_Load);
            this.contextMnu_VitalSigns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpSeach)).EndInit();
            this.grpSeach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMnu_VitalSigns;
        private System.Windows.Forms.ToolStripMenuItem mnuVitalSigns_Import;
        private System.Windows.Forms.ToolStripMenuItem mnuVitalSigns_InsertPageBreak;
        private System.Windows.Forms.ToolStripSeparator mnuVitalSigns_Sep1;
        private System.Windows.Forms.ToolStripMenuItem mnuVitalSigns_AddRec;
        private System.Windows.Forms.ToolStripMenuItem mnuVitalSigns_AddLine;
        private System.Windows.Forms.ToolStripMenuItem mnuVitalSigns_Delete;
        private System.Windows.Forms.ToolStripSeparator mnuVitalSigns_Sep2;
        private System.Windows.Forms.ToolStripMenuItem mnuVitalSigns_Group;
        private System.Windows.Forms.ToolStripMenuItem mnuVitalSigns_Sum;
        private DevExpress.XtraEditors.LabelControl label2;
        private HISPlus.UserControls.UcButton btnQuery;
        private System.Windows.Forms.DateTimePicker dtSearchStart;
        private HISPlus.UserControls.UcButton btnRePrint;
        private DevExpress.XtraEditors.LabelControl label3;
        private DevExpress.XtraEditors.PanelControl grpSeach;
        private DevExpress.XtraEditors.LabelControl label1;
        private System.Windows.Forms.DateTimePicker dtSearchEnd;
        private HISPlus.UserControls.UcButton btnSave;
        private HISPlus.UserControls.UcComboBox cmbNursingRecord;
        private UserControls.UcGridView ucGridView1;
        private System.Windows.Forms.ToolStripMenuItem auditPass;
        private System.Windows.Forms.ToolStripMenuItem auditNoPass;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblBedNo;
        private System.Windows.Forms.ToolStripMenuItem insNursingTransfer;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.Label lbl_ADMISSION_DATE_TIME;
        private DevExpress.XtraEditors.GroupControl groupControl1;
    }
}