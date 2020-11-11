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
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtRngStart
            // 
            this.dtRngStart.Location = new System.Drawing.Point(47, 21);
            this.dtRngStart.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.dtRngStart.Name = "dtRngStart";
            this.dtRngStart.Size = new System.Drawing.Size(147, 26);
            this.dtRngStart.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(6, 25);
            this.label11.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
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
            this.btnQuery.Location = new System.Drawing.Point(407, 18);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnQuery.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnQuery.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(80, 30);
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
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox2.Size = new System.Drawing.Size(1375, 66);
            this.groupBox2.TabIndex = 19;
            // 
            // bntSet
            // 
            this.bntSet.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bntSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntSet.Image = ((System.Drawing.Image)(resources.GetObject("bntSet.Image")));
            this.bntSet.ImageRight = false;
            this.bntSet.ImageStyle = HISPlus.UserControls.ImageStyle.Properties;
            this.bntSet.Location = new System.Drawing.Point(1266, 18);
            this.bntSet.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.bntSet.MaximumSize = new System.Drawing.Size(100, 30);
            this.bntSet.MinimumSize = new System.Drawing.Size(69, 30);
            this.bntSet.Name = "bntSet";
            this.bntSet.Size = new System.Drawing.Size(80, 30);
            this.bntSet.TabIndex = 8;
            this.bntSet.TextValue = "设置";
            this.bntSet.UseVisualStyleBackColor = true;
            this.bntSet.Click += new System.EventHandler(this.bntSet_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageRight = false;
            this.btnPrint.ImageStyle = HISPlus.UserControls.ImageStyle.Print;
            this.btnPrint.Location = new System.Drawing.Point(1144, 18);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnPrint.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnPrint.MinimumSize = new System.Drawing.Size(69, 30);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(80, 30);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.TextValue = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dtRngEnd
            // 
            this.dtRngEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtRngEnd.Location = new System.Drawing.Point(217, 21);
            this.dtRngEnd.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.dtRngEnd.Name = "dtRngEnd";
            this.dtRngEnd.Size = new System.Drawing.Size(147, 26);
            this.dtRngEnd.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(201, 25);
            this.label13.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(5, 18);
            this.label13.TabIndex = 2;
            this.label13.Text = "-";
            // 
            // ucGridView1
            // 
            this.ucGridView1.AllowDeleteRows = false;
            this.ucGridView1.AllowEdit = false;
            this.ucGridView1.AllowSort = false;
            this.ucGridView1.ColumnsEvenOldRowColor = null;
            this.ucGridView1.DataSource = null;
            this.ucGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGridView1.Location = new System.Drawing.Point(0, 66);
            this.ucGridView1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ucGridView1.MultiSelect = false;
            this.ucGridView1.Name = "ucGridView1";
            this.ucGridView1.ShowRowIndicator = false;
            this.ucGridView1.Size = new System.Drawing.Size(1375, 691);
            this.ucGridView1.TabIndex = 1;
            // 
            // NursingTour1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1375, 757);
            this.Controls.Add(this.ucGridView1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "NursingTour1";
            this.Text = "巡视查询";
            this.Load += new System.EventHandler(this.NursingTour_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
    }
}