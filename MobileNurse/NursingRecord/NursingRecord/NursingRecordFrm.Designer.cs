namespace HISPlus
{
    partial class NursingRecordFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NursingRecordFrm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpSeach = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new HISPlus.UserControls.UcButton();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.dtSearchEnd = new HISPlus.UserControls.UcDatePicker();
            this.btnDelete = new HISPlus.UserControls.UcButton();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.btnRePrint = new HISPlus.UserControls.UcButton();
            this.btnPrintContinue = new HISPlus.UserControls.UcButton();
            this.btnQuery = new HISPlus.UserControls.UcButton();
            this.dtSearchStart = new System.Windows.Forms.DateTimePicker();
            this.label3 = new DevExpress.XtraEditors.LabelControl();
            this.timer1 = new System.Windows.Forms.Timer();
            this.grpWorkSpace = new DevExpress.XtraEditors.PanelControl();
            this.pnlHeaderHolder = new DevExpress.XtraEditors.PanelControl();
            this.panelTitle = new DevExpress.XtraEditors.PanelControl();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.contextMnu_VitalSigns = new System.Windows.Forms.ContextMenuStrip();
            this.mnuVitalSigns_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVitalSigns_InsertPageBreak = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVitalSigns_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuVitalSigns_AddRec = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVitalSigns_AddLine = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVitalSigns_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVitalSigns_Sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuVitalSigns_Group = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.grpSeach)).BeginInit();
            this.grpSeach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpWorkSpace)).BeginInit();
            this.grpWorkSpace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeaderHolder)).BeginInit();
            this.pnlHeaderHolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.contextMnu_VitalSigns.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSeach
            // 
            this.grpSeach.Controls.Add(this.btnExit);
            this.grpSeach.Controls.Add(this.label1);
            this.grpSeach.Controls.Add(this.dtSearchEnd);
            this.grpSeach.Controls.Add(this.btnDelete);
            this.grpSeach.Controls.Add(this.btnSave);
            this.grpSeach.Controls.Add(this.btnRePrint);
            this.grpSeach.Controls.Add(this.btnPrintContinue);
            this.grpSeach.Controls.Add(this.btnQuery);
            this.grpSeach.Controls.Add(this.dtSearchStart);
            this.grpSeach.Controls.Add(this.label3);
            this.grpSeach.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpSeach.Location = new System.Drawing.Point(0, 754);
            this.grpSeach.Margin = new System.Windows.Forms.Padding(4);
            this.grpSeach.Name = "grpSeach";
            this.grpSeach.Padding = new System.Windows.Forms.Padding(4);
            this.grpSeach.Size = new System.Drawing.Size(1256, 76);
            this.grpSeach.TabIndex = 6;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageStyle = HISPlus.UserControls.ImageStyle.Close;
            this.btnExit.Location = new System.Drawing.Point(1151, 24);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnExit.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 30);
            this.btnExit.TabIndex = 11;
            this.btnExit.TextValue = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(196, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(5, 18);
            this.label1.TabIndex = 10;
            this.label1.Text = "-";
            // 
            // dtSearchEnd
            // 
            this.dtSearchEnd.Location = new System.Drawing.Point(212, 26);
            this.dtSearchEnd.Margin = new System.Windows.Forms.Padding(4);
            this.dtSearchEnd.Name = "dtSearchEnd";
            this.dtSearchEnd.Size = new System.Drawing.Size(141, 26);
            this.dtSearchEnd.TabIndex = 9;
            // 
            // btnDelete
            // 
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageStyle = HISPlus.UserControls.ImageStyle.Delete;
            this.btnDelete.Location = new System.Drawing.Point(463, 24);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnDelete.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 30);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.TextValue = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSave.Location = new System.Drawing.Point(844, 24);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnSave.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 6;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRePrint
            // 
            this.btnRePrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRePrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRePrint.Image = ((System.Drawing.Image)(resources.GetObject("btnRePrint.Image")));
            this.btnRePrint.ImageStyle = HISPlus.UserControls.ImageStyle.Refresh;
            this.btnRePrint.Location = new System.Drawing.Point(945, 24);
            this.btnRePrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnRePrint.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnRePrint.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnRePrint.Name = "btnRePrint";
            this.btnRePrint.Size = new System.Drawing.Size(90, 30);
            this.btnRePrint.TabIndex = 4;
            this.btnRePrint.TextValue = "重打";
            this.btnRePrint.UseVisualStyleBackColor = true;
            this.btnRePrint.Click += new System.EventHandler(this.btnRePrint_Click);
            // 
            // btnPrintContinue
            // 
            this.btnPrintContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintContinue.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPrintContinue.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintContinue.Image")));
            this.btnPrintContinue.ImageStyle = HISPlus.UserControls.ImageStyle.Apply;
            this.btnPrintContinue.Location = new System.Drawing.Point(1048, 24);
            this.btnPrintContinue.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrintContinue.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnPrintContinue.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnPrintContinue.Name = "btnPrintContinue";
            this.btnPrintContinue.Size = new System.Drawing.Size(90, 30);
            this.btnPrintContinue.TabIndex = 5;
            this.btnPrintContinue.TextValue = "续打";
            this.btnPrintContinue.UseVisualStyleBackColor = true;
            this.btnPrintContinue.Click += new System.EventHandler(this.btnPrintContinue_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.ImageStyle = HISPlus.UserControls.ImageStyle.Find;
            this.btnQuery.Location = new System.Drawing.Point(360, 24);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuery.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnQuery.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(90, 30);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.TextValue = "查询(&Q)";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // dtSearchStart
            // 
            this.dtSearchStart.Location = new System.Drawing.Point(48, 26);
            this.dtSearchStart.Margin = new System.Windows.Forms.Padding(4);
            this.dtSearchStart.Name = "dtSearchStart";
            this.dtSearchStart.Size = new System.Drawing.Size(141, 26);
            this.dtSearchStart.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "日期:";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // grpWorkSpace
            // 
            this.grpWorkSpace.Controls.Add(this.pnlHeaderHolder);
            this.grpWorkSpace.Controls.Add(this.dgvData);
            this.grpWorkSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpWorkSpace.Location = new System.Drawing.Point(0, 0);
            this.grpWorkSpace.Margin = new System.Windows.Forms.Padding(4);
            this.grpWorkSpace.Name = "grpWorkSpace";
            this.grpWorkSpace.Padding = new System.Windows.Forms.Padding(4);
            this.grpWorkSpace.Size = new System.Drawing.Size(1256, 754);
            this.grpWorkSpace.TabIndex = 8;
            // 
            // pnlHeaderHolder
            // 
            this.pnlHeaderHolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHeaderHolder.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlHeaderHolder.Controls.Add(this.panelTitle);
            this.pnlHeaderHolder.Location = new System.Drawing.Point(4, 12);
            this.pnlHeaderHolder.Margin = new System.Windows.Forms.Padding(4);
            this.pnlHeaderHolder.Name = "pnlHeaderHolder";
            this.pnlHeaderHolder.Size = new System.Drawing.Size(1251, 69);
            this.pnlHeaderHolder.TabIndex = 2;
            // 
            // panelTitle
            // 
            this.panelTitle.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.panelTitle.Appearance.Options.UseBackColor = true;
            this.panelTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Margin = new System.Windows.Forms.Padding(4);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(920, 69);
            this.panelTitle.TabIndex = 1;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeColumns = false;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.ColumnHeadersVisible = false;
            this.dgvData.ContextMenuStrip = this.contextMnu_VitalSigns;
            this.dgvData.Location = new System.Drawing.Point(4, 81);
            this.dgvData.Margin = new System.Windows.Forms.Padding(4);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersVisible = false;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(1242, 668);
            this.dgvData.TabIndex = 1;
            this.dgvData.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvData_CellBeginEdit);
            this.dgvData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellDoubleClick);
            this.dgvData.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellEndEdit);
            this.dgvData.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellEnter);
            this.dgvData.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvData_DataBindingComplete);
            this.dgvData.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvData_DefaultValuesNeeded);
            this.dgvData.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvData_RowPostPaint);
            this.dgvData.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvData_Scroll);
            this.dgvData.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvData_KeyUp);
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
            this.mnuVitalSigns_Group});
            this.contextMnu_VitalSigns.Name = "contextMnu_VitalSigns";
            this.contextMnu_VitalSigns.Size = new System.Drawing.Size(190, 160);
            // 
            // mnuVitalSigns_Import
            // 
            this.mnuVitalSigns_Import.Name = "mnuVitalSigns_Import";
            this.mnuVitalSigns_Import.Size = new System.Drawing.Size(189, 24);
            this.mnuVitalSigns_Import.Text = "导入生命体征";
            this.mnuVitalSigns_Import.Click += new System.EventHandler(this.mnuVitalSigns_Import_Click);
            // 
            // mnuVitalSigns_InsertPageBreak
            // 
            this.mnuVitalSigns_InsertPageBreak.Name = "mnuVitalSigns_InsertPageBreak";
            this.mnuVitalSigns_InsertPageBreak.Size = new System.Drawing.Size(189, 24);
            this.mnuVitalSigns_InsertPageBreak.Text = "插入/撤消分页符";
            this.mnuVitalSigns_InsertPageBreak.Visible = false;
            this.mnuVitalSigns_InsertPageBreak.Click += new System.EventHandler(this.mnuVitalSigns_InsertPageBreak_Click);
            // 
            // mnuVitalSigns_Sep1
            // 
            this.mnuVitalSigns_Sep1.Name = "mnuVitalSigns_Sep1";
            this.mnuVitalSigns_Sep1.Size = new System.Drawing.Size(186, 6);
            // 
            // mnuVitalSigns_AddRec
            // 
            this.mnuVitalSigns_AddRec.Name = "mnuVitalSigns_AddRec";
            this.mnuVitalSigns_AddRec.Size = new System.Drawing.Size(189, 24);
            this.mnuVitalSigns_AddRec.Text = "插入新记录";
            this.mnuVitalSigns_AddRec.Click += new System.EventHandler(this.mnuVitalSigns_AddRec_Click);
            // 
            // mnuVitalSigns_AddLine
            // 
            this.mnuVitalSigns_AddLine.Name = "mnuVitalSigns_AddLine";
            this.mnuVitalSigns_AddLine.Size = new System.Drawing.Size(189, 24);
            this.mnuVitalSigns_AddLine.Text = "追加新行";
            this.mnuVitalSigns_AddLine.Click += new System.EventHandler(this.mnuVitalSigns_AddLine_Click);
            // 
            // mnuVitalSigns_Delete
            // 
            this.mnuVitalSigns_Delete.Name = "mnuVitalSigns_Delete";
            this.mnuVitalSigns_Delete.Size = new System.Drawing.Size(189, 24);
            this.mnuVitalSigns_Delete.Text = "删除";
            this.mnuVitalSigns_Delete.Click += new System.EventHandler(this.mnuVitalSigns_Delete_Click);
            // 
            // mnuVitalSigns_Sep2
            // 
            this.mnuVitalSigns_Sep2.Name = "mnuVitalSigns_Sep2";
            this.mnuVitalSigns_Sep2.Size = new System.Drawing.Size(186, 6);
            // 
            // mnuVitalSigns_Group
            // 
            this.mnuVitalSigns_Group.Name = "mnuVitalSigns_Group";
            this.mnuVitalSigns_Group.Size = new System.Drawing.Size(189, 24);
            this.mnuVitalSigns_Group.Text = "分组";
            this.mnuVitalSigns_Group.Click += new System.EventHandler(this.mnuVitalSigns_Group_Click);
            // 
            // NursingRecordFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1256, 830);
            this.Controls.Add(this.grpWorkSpace);
            this.Controls.Add(this.grpSeach);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NursingRecordFrm";
            this.Text = "护理记录单";
            this.Load += new System.EventHandler(this.NursingRecordFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpSeach)).EndInit();
            this.grpSeach.ResumeLayout(false);
            this.grpSeach.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpWorkSpace)).EndInit();
            this.grpWorkSpace.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeaderHolder)).EndInit();
            this.pnlHeaderHolder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.contextMnu_VitalSigns.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl grpSeach;
        private HISPlus.UserControls.UcButton btnRePrint;
        private HISPlus.UserControls.UcButton btnPrintContinue;
        private HISPlus.UserControls.UcButton btnQuery;
        private System.Windows.Forms.DateTimePicker dtSearchStart;
        private DevExpress.XtraEditors.LabelControl label3;
        private HISPlus.UserControls.UcButton btnSave;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.PanelControl grpWorkSpace;
        private System.Windows.Forms.DataGridView dgvData;
        private HISPlus.UserControls.UcButton btnDelete;
        private System.Windows.Forms.ContextMenuStrip contextMnu_VitalSigns;
        private System.Windows.Forms.ToolStripMenuItem mnuVitalSigns_Import;
        private DevExpress.XtraEditors.LabelControl label1;
        private HISPlus.UserControls.UcDatePicker dtSearchEnd;
        private System.Windows.Forms.ToolStripMenuItem mnuVitalSigns_InsertPageBreak;
        private DevExpress.XtraEditors.PanelControl pnlHeaderHolder;
        private DevExpress.XtraEditors.PanelControl panelTitle;
        private HISPlus.UserControls.UcButton btnExit;
        private System.Windows.Forms.ToolStripSeparator mnuVitalSigns_Sep1;
        private System.Windows.Forms.ToolStripMenuItem mnuVitalSigns_AddRec;
        private System.Windows.Forms.ToolStripMenuItem mnuVitalSigns_AddLine;
        private System.Windows.Forms.ToolStripMenuItem mnuVitalSigns_Delete;
        private System.Windows.Forms.ToolStripSeparator mnuVitalSigns_Sep2;
        private System.Windows.Forms.ToolStripMenuItem mnuVitalSigns_Group;
    }
}