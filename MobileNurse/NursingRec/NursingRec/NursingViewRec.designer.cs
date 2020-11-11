namespace HISPlus
{
    partial class NursingViewRec
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NursingViewRec));
            this.grpPatientEdu = new DevExpress.XtraEditors.GroupControl();
            this.ucGridView1 = new HISPlus.UserControls.UcCheckGridView();
            this.lvwNursingRec = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new DevExpress.XtraEditors.PanelControl();
            this.btnPrint = new HISPlus.UserControls.UcButton();
            this.dtRngEnd = new System.Windows.Forms.DateTimePicker();
            this.label13 = new DevExpress.XtraEditors.LabelControl();
            this.dtRngStart = new System.Windows.Forms.DateTimePicker();
            this.label11 = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery = new HISPlus.UserControls.UcButton();
            ((System.ComponentModel.ISupportInitialize)(this.grpPatientEdu)).BeginInit();
            this.grpPatientEdu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPatientEdu
            // 
            this.grpPatientEdu.Controls.Add(this.ucGridView1);
            this.grpPatientEdu.Controls.Add(this.lvwNursingRec);
            this.grpPatientEdu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPatientEdu.Location = new System.Drawing.Point(0, 74);
            this.grpPatientEdu.Margin = new System.Windows.Forms.Padding(4);
            this.grpPatientEdu.Name = "grpPatientEdu";
            this.grpPatientEdu.Padding = new System.Windows.Forms.Padding(4);
            this.grpPatientEdu.Size = new System.Drawing.Size(1135, 742);
            this.grpPatientEdu.TabIndex = 1;
            this.grpPatientEdu.Text = "护理记录列表";
            // 
            // ucGridView1
            // 
            this.ucGridView1.AllowDeleteRows = false;
            this.ucGridView1.AllowEdit = false;
            this.ucGridView1.AllowSort = false;
            this.ucGridView1.ColumnsEvenOldRowColor = null;
            this.ucGridView1.DataSource = null;
            this.ucGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGridView1.Location = new System.Drawing.Point(6, 30);
            this.ucGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucGridView1.MultiSelect = false;
            this.ucGridView1.Name = "ucGridView1";
            this.ucGridView1.ShowRowIndicator = false;
            this.ucGridView1.Size = new System.Drawing.Size(1123, 706);
            this.ucGridView1.TabIndex = 1;
            // 
            // lvwNursingRec
            // 
            this.lvwNursingRec.CheckBoxes = true;
            this.lvwNursingRec.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader7,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader3});
            this.lvwNursingRec.FullRowSelect = true;
            this.lvwNursingRec.GridLines = true;
            this.lvwNursingRec.Location = new System.Drawing.Point(6, 30);
            this.lvwNursingRec.Margin = new System.Windows.Forms.Padding(4);
            this.lvwNursingRec.Name = "lvwNursingRec";
            this.lvwNursingRec.Size = new System.Drawing.Size(1123, 240);
            this.lvwNursingRec.TabIndex = 0;
            this.lvwNursingRec.UseCompatibleStateImageBehavior = false;
            this.lvwNursingRec.View = System.Windows.Forms.View.Details;
            this.lvwNursingRec.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvwNursingRec_ItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "日期";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "时间";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "名称";
            this.columnHeader7.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "值";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "单位";
            this.columnHeader5.Width = 80;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "记录人";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "备注";
            this.columnHeader3.Width = 360;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnPrint);
            this.groupBox2.Controls.Add(this.dtRngEnd);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.dtRngStart);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.btnQuery);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1135, 74);
            this.groupBox2.TabIndex = 2;
            // 
            // btnPrint
            // 
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageRight = false;
            this.btnPrint.ImageStyle = HISPlus.UserControls.ImageStyle.Print;
            this.btnPrint.Location = new System.Drawing.Point(518, 20);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnPrint.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(90, 30);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.TextValue = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dtRngEnd
            // 
            this.dtRngEnd.Location = new System.Drawing.Point(219, 22);
            this.dtRngEnd.Margin = new System.Windows.Forms.Padding(4);
            this.dtRngEnd.Name = "dtRngEnd";
            this.dtRngEnd.Size = new System.Drawing.Size(147, 26);
            this.dtRngEnd.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(203, 26);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(5, 18);
            this.label13.TabIndex = 2;
            this.label13.Text = "-";
            // 
            // dtRngStart
            // 
            this.dtRngStart.Location = new System.Drawing.Point(49, 22);
            this.dtRngStart.Margin = new System.Windows.Forms.Padding(4);
            this.dtRngStart.Name = "dtRngStart";
            this.dtRngStart.Size = new System.Drawing.Size(147, 26);
            this.dtRngStart.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(8, 26);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 18);
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
            this.btnQuery.Location = new System.Drawing.Point(391, 20);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuery.MaximumSize = new System.Drawing.Size(180, 30);
            this.btnQuery.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(90, 30);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.TextValue = "查询(&Q)";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // NursingViewRec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 816);
            this.Controls.Add(this.grpPatientEdu);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NursingViewRec";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "血压脉搏观察表";
            this.Load += new System.EventHandler(this.NursingRec_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpPatientEdu)).EndInit();
            this.grpPatientEdu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpPatientEdu;
        private System.Windows.Forms.ListView lvwNursingRec;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private DevExpress.XtraEditors.PanelControl groupBox2;
        private HISPlus.UserControls.UcButton btnQuery;
        private System.Windows.Forms.DateTimePicker dtRngEnd;
        private DevExpress.XtraEditors.LabelControl label13;
        private System.Windows.Forms.DateTimePicker dtRngStart;
        private DevExpress.XtraEditors.LabelControl label11;
        private HISPlus.UserControls.UcButton btnPrint;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private UserControls.UcCheckGridView ucGridView1;
    }
}