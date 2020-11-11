namespace HISPlus
{
    partial class NursingTour1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NursingTour1));
            this.dtRngStart = new System.Windows.Forms.DateTimePicker();
            this.label11 = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery = new HISPlus.UserControls.UcButton();
            this.groupBox2 = new DevExpress.XtraEditors.PanelControl();
            this.bntSet = new HISPlus.UserControls.UcButton();
            this.btnPrint = new HISPlus.UserControls.UcButton();
            this.dtRngEnd = new System.Windows.Forms.DateTimePicker();
            this.label13 = new DevExpress.XtraEditors.LabelControl();
            this.ucGridView1 = new HISPlus.UserControls.UcGridView();
            this.DeleteStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DelStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.DeleteStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtRngStart
            // 
            this.dtRngStart.Location = new System.Drawing.Point(46, 12);
            this.dtRngStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dtRngStart.Name = "dtRngStart";
            this.dtRngStart.Size = new System.Drawing.Size(129, 22);
            this.dtRngStart.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(10, 15);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 14);
            this.label11.TabIndex = 0;
            this.label11.Text = "日期";
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.ImageRight = false;
            this.btnQuery.ImageStyle = HISPlus.UserControls.ImageStyle.Find;
            this.btnQuery.Location = new System.Drawing.Point(361, 10);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnQuery.MaximumSize = new System.Drawing.Size(88, 23);
            this.btnQuery.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(70, 23);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.TextValue = "查询(&Q)";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bntSet);
            this.groupBox2.Controls.Add(this.btnPrint);
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
            this.groupBox2.Size = new System.Drawing.Size(1192, 45);
            this.groupBox2.TabIndex = 19;
            // 
            // bntSet
            // 
            this.bntSet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.bntSet.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bntSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntSet.Image = ((System.Drawing.Image)(resources.GetObject("bntSet.Image")));
            this.bntSet.ImageRight = false;
            this.bntSet.ImageStyle = HISPlus.UserControls.ImageStyle.Properties;
            this.bntSet.Location = new System.Drawing.Point(517, 10);
            this.bntSet.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bntSet.MaximumSize = new System.Drawing.Size(88, 23);
            this.bntSet.MinimumSize = new System.Drawing.Size(60, 23);
            this.bntSet.Name = "bntSet";
            this.bntSet.Size = new System.Drawing.Size(70, 23);
            this.bntSet.TabIndex = 8;
            this.bntSet.TextValue = "设置";
            this.bntSet.UseVisualStyleBackColor = true;
            this.bntSet.Click += new System.EventHandler(this.bntSet_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageRight = false;
            this.btnPrint.ImageStyle = HISPlus.UserControls.ImageStyle.Print;
            this.btnPrint.Location = new System.Drawing.Point(439, 10);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPrint.MaximumSize = new System.Drawing.Size(88, 23);
            this.btnPrint.MinimumSize = new System.Drawing.Size(60, 23);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(70, 23);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.TextValue = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dtRngEnd
            // 
            this.dtRngEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtRngEnd.Location = new System.Drawing.Point(195, 12);
            this.dtRngEnd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dtRngEnd.Name = "dtRngEnd";
            this.dtRngEnd.Size = new System.Drawing.Size(129, 22);
            this.dtRngEnd.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(181, 15);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(4, 14);
            this.label13.TabIndex = 2;
            this.label13.Text = "-";
            // 
            // ucGridView1
            // 
            this.ucGridView1.AllowAddRows = false;
            this.ucGridView1.AllowDeleteRows = false;
            this.ucGridView1.AllowEdit = false;
            this.ucGridView1.AllowSort = false;
            this.ucGridView1.ColumnAutoWidth = true;
            this.ucGridView1.ColumnsEvenOldRowColor = null;
            this.ucGridView1.ContextMenuStrip = this.DeleteStrip;
            this.ucGridView1.DataSource = null;
            this.ucGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGridView1.Location = new System.Drawing.Point(0, 45);
            this.ucGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucGridView1.Name = "ucGridView1";
            this.ucGridView1.ShowRowIndicator = false;
            this.ucGridView1.Size = new System.Drawing.Size(1192, 531);
            this.ucGridView1.TabIndex = 1;
            // 
            // DeleteStrip
            // 
            this.DeleteStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DelStripMenuItem});
            this.DeleteStrip.Name = "DeleteStrip";
            this.DeleteStrip.Size = new System.Drawing.Size(153, 48);
            this.DeleteStrip.Opening += new System.ComponentModel.CancelEventHandler(this.DeleteStrip_Opening);
            // 
            // DelStripMenuItem
            // 
            this.DelStripMenuItem.Name = "DelStripMenuItem";
            this.DelStripMenuItem.ShowShortcutKeys = false;
            this.DelStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.DelStripMenuItem.Text = "删除";
            this.DelStripMenuItem.Click += new System.EventHandler(this.DelStripMenuItem_Click);
            // 
            // NursingTour1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 576);
            this.Controls.Add(this.ucGridView1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "NursingTour1";
            this.Text = "巡视查询";
            this.Load += new System.EventHandler(this.NursingTour_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.DeleteStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtRngStart;
        private DevExpress.XtraEditors.LabelControl label11;
        private HISPlus.UserControls.UcButton btnQuery;
        private DevExpress.XtraEditors.PanelControl groupBox2;
        private HISPlus.UserControls.UcButton btnPrint;
        private System.Windows.Forms.DateTimePicker dtRngEnd;
        private DevExpress.XtraEditors.LabelControl label13;
        private HISPlus.UserControls.UcButton bntSet;
        private UserControls.UcGridView ucGridView1;
        private System.Windows.Forms.ContextMenuStrip DeleteStrip;
        private System.Windows.Forms.ToolStripMenuItem DelStripMenuItem;
    }
}