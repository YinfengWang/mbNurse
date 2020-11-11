namespace HISPlus
{
    partial class OrderExecuteQueryOld
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderExecuteQueryOld));
            this.groupBox2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancelExecute = new HISPlus.UserControls.UcButton();
            this.btnExit = new HISPlus.UserControls.UcButton();
            this.btnPrint = new HISPlus.UserControls.UcButton();
            this.btnQuery = new HISPlus.UserControls.UcButton();
            this.dtRngEnd = new System.Windows.Forms.DateTimePicker();
            this.dtRngStart = new System.Windows.Forms.DateTimePicker();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox3 = new DevExpress.XtraEditors.PanelControl();
            this.lvwOrderExecute = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox3)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCancelExecute);
            this.groupBox2.Controls.Add(this.btnExit);
            this.groupBox2.Controls.Add(this.btnPrint);
            this.groupBox2.Controls.Add(this.btnQuery);
            this.groupBox2.Controls.Add(this.dtRngEnd);
            this.groupBox2.Controls.Add(this.dtRngStart);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1207, 72);
            this.groupBox2.TabIndex = 1;
            // 
            // btnCancelExecute
            // 
            this.btnCancelExecute.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelExecute.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelExecute.Image")));
            this.btnCancelExecute.ImageRight = false;
            this.btnCancelExecute.ImageStyle = HISPlus.UserControls.ImageStyle.Remove;
            this.btnCancelExecute.Location = new System.Drawing.Point(509, 18);
            this.btnCancelExecute.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelExecute.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnCancelExecute.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnCancelExecute.Name = "btnCancelExecute";
            this.btnCancelExecute.Size = new System.Drawing.Size(90, 30);
            this.btnCancelExecute.TabIndex = 11;
            this.btnCancelExecute.TextValue = "撤消执行";
            this.btnCancelExecute.UseVisualStyleBackColor = true;
            this.btnCancelExecute.Visible = false;
            this.btnCancelExecute.Click += new System.EventHandler(this.btnCancelExecute_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageRight = false;
            this.btnExit.ImageStyle = HISPlus.UserControls.ImageStyle.Close;
            this.btnExit.Location = new System.Drawing.Point(1079, 18);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnExit.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 30);
            this.btnExit.TabIndex = 10;
            this.btnExit.TextValue = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageRight = false;
            this.btnPrint.ImageStyle = HISPlus.UserControls.ImageStyle.Print;
            this.btnPrint.Location = new System.Drawing.Point(919, 18);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnPrint.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(90, 30);
            this.btnPrint.TabIndex = 9;
            this.btnPrint.TextValue = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Visible = false;
            // 
            // btnQuery
            // 
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.ImageRight = false;
            this.btnQuery.ImageStyle = HISPlus.UserControls.ImageStyle.Find;
            this.btnQuery.Location = new System.Drawing.Point(383, 18);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuery.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnQuery.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(90, 30);
            this.btnQuery.TabIndex = 8;
            this.btnQuery.TextValue = "查询(&Q)";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // dtRngEnd
            // 
            this.dtRngEnd.Location = new System.Drawing.Point(223, 20);
            this.dtRngEnd.Margin = new System.Windows.Forms.Padding(4);
            this.dtRngEnd.Name = "dtRngEnd";
            this.dtRngEnd.Size = new System.Drawing.Size(151, 26);
            this.dtRngEnd.TabIndex = 7;
            // 
            // dtRngStart
            // 
            this.dtRngStart.Location = new System.Drawing.Point(52, 20);
            this.dtRngStart.Margin = new System.Windows.Forms.Padding(4);
            this.dtRngStart.Name = "dtRngStart";
            this.dtRngStart.Size = new System.Drawing.Size(151, 26);
            this.dtRngStart.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(207, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(5, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "-";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "日期";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lvwOrderExecute);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 72);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(1207, 710);
            this.groupBox3.TabIndex = 2;
            // 
            // lvwOrderExecute
            // 
            this.lvwOrderExecute.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader2,
            this.columnHeader3});
            this.lvwOrderExecute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwOrderExecute.FullRowSelect = true;
            this.lvwOrderExecute.GridLines = true;
            this.lvwOrderExecute.Location = new System.Drawing.Point(6, 6);
            this.lvwOrderExecute.Margin = new System.Windows.Forms.Padding(4);
            this.lvwOrderExecute.Name = "lvwOrderExecute";
            this.lvwOrderExecute.Size = new System.Drawing.Size(1195, 698);
            this.lvwOrderExecute.TabIndex = 1;
            this.lvwOrderExecute.UseCompatibleStateImageBehavior = false;
            this.lvwOrderExecute.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "日期";
            this.columnHeader1.Width = 70;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "类别";
            this.columnHeader4.Width = 40;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "长";
            this.columnHeader5.Width = 30;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "子";
            this.columnHeader6.Width = 30;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "医嘱";
            this.columnHeader7.Width = 188;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "剂量";
            this.columnHeader8.Width = 71;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "途径";
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "计划执行时间";
            this.columnHeader11.Width = 131;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "实际执行时间";
            this.columnHeader12.Width = 142;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "延时";
            this.columnHeader2.Width = 0;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "执行护士";
            this.columnHeader3.Width = 71;
            // 
            // OrderExecuteQueryOld
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 782);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OrderExecuteQueryOld";
            this.Text = "医嘱执行单查询";
            this.Load += new System.EventHandler(this.OrderExecuteQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl groupBox2;
        private DevExpress.XtraEditors.PanelControl groupBox3;
        private System.Windows.Forms.DateTimePicker dtRngEnd;
        private System.Windows.Forms.DateTimePicker dtRngStart;
        private DevExpress.XtraEditors.LabelControl label2;
        private DevExpress.XtraEditors.LabelControl label1;
        private HISPlus.UserControls.UcButton btnQuery;
        private HISPlus.UserControls.UcButton btnPrint;
        private HISPlus.UserControls.UcButton btnExit;
        private System.Windows.Forms.ListView lvwOrderExecute;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private HISPlus.UserControls.UcButton btnCancelExecute;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}