namespace HISPlus
{
    partial class OrderExecuteQueryUC
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
            this.panelSearch = new DevExpress.XtraEditors.PanelControl();
            this.chk_QK = new DevExpress.XtraEditors.CheckEdit();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.cmb_type = new System.Windows.Forms.ComboBox();
            this.lbl_Type = new DevExpress.XtraEditors.LabelControl();
            this.lbl_schedule_perform_time = new System.Windows.Forms.Label();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.dataEnd = new DevExpress.XtraEditors.DateEdit();
            this.dataStart = new DevExpress.XtraEditors.DateEdit();
            this.chkLs = new DevExpress.XtraEditors.CheckEdit();
            this.chkCq = new DevExpress.XtraEditors.CheckEdit();
            this.lblLb = new DevExpress.XtraEditors.LabelControl();
            this.panelFiltrate = new DevExpress.XtraEditors.PanelControl();
            this.chkListTj = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.ucGridView1 = new HISPlus.UserControls.UcCheckGridView();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.panelSearch)).BeginInit();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_QK.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataEnd.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataStart.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCq.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelFiltrate)).BeginInit();
            this.panelFiltrate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkListTj)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSearch
            // 
            this.panelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearch.Controls.Add(this.chkSelectAll);
            this.panelSearch.Controls.Add(this.chk_QK);
            this.panelSearch.Controls.Add(this.btnPrint);
            this.panelSearch.Controls.Add(this.cmb_type);
            this.panelSearch.Controls.Add(this.lbl_Type);
            this.panelSearch.Controls.Add(this.lbl_schedule_perform_time);
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Controls.Add(this.dataEnd);
            this.panelSearch.Controls.Add(this.dataStart);
            this.panelSearch.Controls.Add(this.chkLs);
            this.panelSearch.Controls.Add(this.chkCq);
            this.panelSearch.Controls.Add(this.lblLb);
            this.panelSearch.Location = new System.Drawing.Point(0, 5);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(997, 51);
            this.panelSearch.TabIndex = 1;
            // 
            // chk_QK
            // 
            this.chk_QK.Location = new System.Drawing.Point(11, 20);
            this.chk_QK.Name = "chk_QK";
            this.chk_QK.Properties.Caption = "全科";
            this.chk_QK.Size = new System.Drawing.Size(47, 19);
            this.chk_QK.TabIndex = 10;
            this.chk_QK.CheckedChanged += new System.EventHandler(this.chk_QK_CheckedChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(863, 16);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(51, 25);
            this.btnPrint.TabIndex = 9;
            this.btnPrint.Text = "打印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // cmb_type
            // 
            this.cmb_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_type.FormattingEnabled = true;
            this.cmb_type.Items.AddRange(new object[] {
            "全部",
            "药疗",
            "非药疗"});
            this.cmb_type.Location = new System.Drawing.Point(268, 19);
            this.cmb_type.Name = "cmb_type";
            this.cmb_type.Size = new System.Drawing.Size(111, 22);
            this.cmb_type.TabIndex = 8;
            // 
            // lbl_Type
            // 
            this.lbl_Type.Location = new System.Drawing.Point(234, 23);
            this.lbl_Type.Name = "lbl_Type";
            this.lbl_Type.Size = new System.Drawing.Size(28, 14);
            this.lbl_Type.TabIndex = 7;
            this.lbl_Type.Text = "分类:";
            // 
            // lbl_schedule_perform_time
            // 
            this.lbl_schedule_perform_time.AutoSize = true;
            this.lbl_schedule_perform_time.Location = new System.Drawing.Point(415, 21);
            this.lbl_schedule_perform_time.Name = "lbl_schedule_perform_time";
            this.lbl_schedule_perform_time.Size = new System.Drawing.Size(83, 14);
            this.lbl_schedule_perform_time.TabIndex = 6;
            this.lbl_schedule_perform_time.Text = "计划执行时间:";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(808, 16);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(51, 25);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dataEnd
            // 
            this.dataEnd.EditValue = null;
            this.dataEnd.Location = new System.Drawing.Point(658, 19);
            this.dataEnd.Name = "dataEnd";
            this.dataEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dataEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dataEnd.Size = new System.Drawing.Size(145, 20);
            this.dataEnd.TabIndex = 4;
            // 
            // dataStart
            // 
            this.dataStart.EditValue = null;
            this.dataStart.Location = new System.Drawing.Point(505, 19);
            this.dataStart.Name = "dataStart";
            this.dataStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dataStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dataStart.Size = new System.Drawing.Size(145, 20);
            this.dataStart.TabIndex = 3;
            // 
            // chkLs
            // 
            this.chkLs.EditValue = true;
            this.chkLs.Location = new System.Drawing.Point(166, 20);
            this.chkLs.Name = "chkLs";
            this.chkLs.Properties.Caption = "临时";
            this.chkLs.Size = new System.Drawing.Size(53, 19);
            this.chkLs.TabIndex = 2;
            this.chkLs.CheckedChanged += new System.EventHandler(this.chkLs_CheckedChanged);
            // 
            // chkCq
            // 
            this.chkCq.EditValue = true;
            this.chkCq.Location = new System.Drawing.Point(113, 20);
            this.chkCq.Name = "chkCq";
            this.chkCq.Properties.Caption = "长期";
            this.chkCq.Size = new System.Drawing.Size(58, 19);
            this.chkCq.TabIndex = 1;
            this.chkCq.CheckedChanged += new System.EventHandler(this.chkCq_CheckedChanged);
            // 
            // lblLb
            // 
            this.lblLb.Location = new System.Drawing.Point(79, 22);
            this.lblLb.Name = "lblLb";
            this.lblLb.Size = new System.Drawing.Size(36, 14);
            this.lblLb.TabIndex = 0;
            this.lblLb.Text = "类别：";
            // 
            // panelFiltrate
            // 
            this.panelFiltrate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFiltrate.Controls.Add(this.chkListTj);
            this.panelFiltrate.Location = new System.Drawing.Point(0, 388);
            this.panelFiltrate.Name = "panelFiltrate";
            this.panelFiltrate.Size = new System.Drawing.Size(997, 89);
            this.panelFiltrate.TabIndex = 3;
            // 
            // chkListTj
            // 
            this.chkListTj.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkListTj.Location = new System.Drawing.Point(2, 2);
            this.chkListTj.MultiColumn = true;
            this.chkListTj.Name = "chkListTj";
            this.chkListTj.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.chkListTj.Size = new System.Drawing.Size(993, 85);
            this.chkListTj.TabIndex = 0;
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
            this.ucGridView1.Location = new System.Drawing.Point(0, 61);
            this.ucGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.ucGridView1.Name = "ucGridView1";
            this.ucGridView1.ShowRowIndicator = false;
            this.ucGridView1.Size = new System.Drawing.Size(995, 324);
            this.ucGridView1.TabIndex = 4;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Checked = true;
            this.chkSelectAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelectAll.Location = new System.Drawing.Point(939, 20);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(50, 18);
            this.chkSelectAll.TabIndex = 11;
            this.chkSelectAll.Text = "全选";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.Visible = false;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // OrderExecuteQueryUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 477);
            this.Controls.Add(this.ucGridView1);
            this.Controls.Add(this.panelFiltrate);
            this.Controls.Add(this.panelSearch);
            this.Name = "OrderExecuteQueryUC";
            this.ShowIcon = false;
            this.Text = "医嘱执行单查询";
            this.Load += new System.EventHandler(this.OrderExecuteQueryUC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelSearch)).EndInit();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_QK.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataEnd.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataStart.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCq.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelFiltrate)).EndInit();
            this.panelFiltrate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkListTj)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelSearch;
        private DevExpress.XtraEditors.CheckEdit chk_QK;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private System.Windows.Forms.ComboBox cmb_type;
        private DevExpress.XtraEditors.LabelControl lbl_Type;
        private System.Windows.Forms.Label lbl_schedule_perform_time;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.DateEdit dataEnd;
        private DevExpress.XtraEditors.DateEdit dataStart;
        private DevExpress.XtraEditors.CheckEdit chkLs;
        private DevExpress.XtraEditors.CheckEdit chkCq;
        private DevExpress.XtraEditors.LabelControl lblLb;
        private DevExpress.XtraEditors.PanelControl panelFiltrate;
        private DevExpress.XtraEditors.CheckedListBoxControl chkListTj;
        private UserControls.UcCheckGridView ucGridView1;
        private System.Windows.Forms.CheckBox chkSelectAll;
    }
}