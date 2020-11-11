namespace HISPlus
{
    partial class BodyTemperatureForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BodyTemperatureForm));
            this.grpFunction = new DevExpress.XtraEditors.PanelControl();
            this.txtPageIndex = new System.Windows.Forms.TextBox();
            this.btnHistory = new HISPlus.UserControls.UcButton();
            this.btnPrintablePatient = new HISPlus.UserControls.UcButton();
            this.btnRefresh = new HISPlus.UserControls.UcButton();
            this.btnPrint = new HISPlus.UserControls.UcButton();
            this.btnNextPage = new HISPlus.UserControls.UcButton();
            this.btnFirst = new HISPlus.UserControls.UcButton();
            this.btnPrePage = new HISPlus.UserControls.UcButton();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grpFunction)).BeginInit();
            this.grpFunction.SuspendLayout();
            this.xtraScrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grpFunction
            // 
            this.grpFunction.Controls.Add(this.txtPageIndex);
            this.grpFunction.Controls.Add(this.btnHistory);
            this.grpFunction.Controls.Add(this.btnPrintablePatient);
            this.grpFunction.Controls.Add(this.btnRefresh);
            this.grpFunction.Controls.Add(this.btnPrint);
            this.grpFunction.Controls.Add(this.btnNextPage);
            this.grpFunction.Controls.Add(this.btnFirst);
            this.grpFunction.Controls.Add(this.btnPrePage);
            this.grpFunction.Dock = System.Windows.Forms.DockStyle.Right;
            this.grpFunction.Location = new System.Drawing.Point(867, 0);
            this.grpFunction.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpFunction.Name = "grpFunction";
            this.grpFunction.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpFunction.Size = new System.Drawing.Size(168, 608);
            this.grpFunction.TabIndex = 47;
            // 
            // txtPageIndex
            // 
            this.txtPageIndex.Location = new System.Drawing.Point(74, -2);
            this.txtPageIndex.Name = "txtPageIndex";
            this.txtPageIndex.Size = new System.Drawing.Size(36, 22);
            this.txtPageIndex.TabIndex = 7;
            this.txtPageIndex.Visible = false;
            // 
            // btnHistory
            // 
            this.btnHistory.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.Image = ((System.Drawing.Image)(resources.GetObject("btnHistory.Image")));
            this.btnHistory.ImageRight = false;
            this.btnHistory.ImageStyle = HISPlus.UserControls.ImageStyle.HistoryItem;
            this.btnHistory.Location = new System.Drawing.Point(23, 212);
            this.btnHistory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnHistory.MaximumSize = new System.Drawing.Size(150, 23);
            this.btnHistory.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(131, 23);
            this.btnHistory.TabIndex = 6;
            this.btnHistory.TextValue = "该病人历史体温图";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnPrintablePatient
            // 
            this.btnPrintablePatient.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPrintablePatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintablePatient.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintablePatient.Image")));
            this.btnPrintablePatient.ImageRight = false;
            this.btnPrintablePatient.ImageStyle = HISPlus.UserControls.ImageStyle.WeekView;
            this.btnPrintablePatient.Location = new System.Drawing.Point(23, 180);
            this.btnPrintablePatient.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPrintablePatient.MaximumSize = new System.Drawing.Size(131, 23);
            this.btnPrintablePatient.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnPrintablePatient.Name = "btnPrintablePatient";
            this.btnPrintablePatient.Size = new System.Drawing.Size(131, 23);
            this.btnPrintablePatient.TabIndex = 5;
            this.btnPrintablePatient.TextValue = "整周及已出院病人";
            this.btnPrintablePatient.UseVisualStyleBackColor = true;
            this.btnPrintablePatient.Click += new System.EventHandler(this.btnPrintablePatient_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageRight = false;
            this.btnRefresh.ImageStyle = HISPlus.UserControls.ImageStyle.Refresh;
            this.btnRefresh.Location = new System.Drawing.Point(22, 116);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRefresh.MaximumSize = new System.Drawing.Size(88, 23);
            this.btnRefresh.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(88, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.TextValue = "刷  新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageRight = false;
            this.btnPrint.ImageStyle = HISPlus.UserControls.ImageStyle.Print;
            this.btnPrint.Location = new System.Drawing.Point(22, 146);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPrint.MaximumSize = new System.Drawing.Size(88, 23);
            this.btnPrint.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(88, 23);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.TextValue = "打  印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextPage.Image = ((System.Drawing.Image)(resources.GetObject("btnNextPage.Image")));
            this.btnNextPage.ImageRight = false;
            this.btnNextPage.ImageStyle = HISPlus.UserControls.ImageStyle.Forward;
            this.btnNextPage.Location = new System.Drawing.Point(23, 84);
            this.btnNextPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnNextPage.MaximumSize = new System.Drawing.Size(88, 23);
            this.btnNextPage.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(88, 23);
            this.btnNextPage.TabIndex = 4;
            this.btnNextPage.TextValue = "后一页";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.Image")));
            this.btnFirst.ImageRight = false;
            this.btnFirst.ImageStyle = HISPlus.UserControls.ImageStyle.Backward;
            this.btnFirst.Location = new System.Drawing.Point(23, 25);
            this.btnFirst.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFirst.MaximumSize = new System.Drawing.Size(88, 23);
            this.btnFirst.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(88, 23);
            this.btnFirst.TabIndex = 3;
            this.btnFirst.TextValue = "第一页";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrePage
            // 
            this.btnPrePage.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPrePage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrePage.Image = ((System.Drawing.Image)(resources.GetObject("btnPrePage.Image")));
            this.btnPrePage.ImageRight = false;
            this.btnPrePage.ImageStyle = HISPlus.UserControls.ImageStyle.Backward;
            this.btnPrePage.Location = new System.Drawing.Point(23, 54);
            this.btnPrePage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPrePage.MaximumSize = new System.Drawing.Size(88, 23);
            this.btnPrePage.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnPrePage.Name = "btnPrePage";
            this.btnPrePage.Size = new System.Drawing.Size(88, 23);
            this.btnPrePage.TabIndex = 3;
            this.btnPrePage.TextValue = "前一页";
            this.btnPrePage.UseVisualStyleBackColor = true;
            this.btnPrePage.Click += new System.EventHandler(this.btnPrePage_Click);
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Controls.Add(this.pictureEdit1);
            this.xtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(867, 608);
            this.xtraScrollableControl1.TabIndex = 48;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Location = new System.Drawing.Point(46, 9);
            this.pictureEdit1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Size = new System.Drawing.Size(638, 576);
            this.pictureEdit1.TabIndex = 0;
            // 
            // BodyTemperatureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 608);
            this.Controls.Add(this.xtraScrollableControl1);
            this.Controls.Add(this.grpFunction);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "BodyTemperatureForm";
            this.Text = "体温记录";
            ((System.ComponentModel.ISupportInitialize)(this.grpFunction)).EndInit();
            this.grpFunction.ResumeLayout(false);
            this.grpFunction.PerformLayout();
            this.xtraScrollableControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl grpFunction;
        private HISPlus.UserControls.UcButton btnPrint;
        private HISPlus.UserControls.UcButton btnRefresh;
        private HISPlus.UserControls.UcButton btnNextPage;
        private HISPlus.UserControls.UcButton btnPrePage;
        private HISPlus.UserControls.UcButton btnPrintablePatient;
        private HISPlus.UserControls.UcButton btnHistory;
        private UserControls.UcButton btnFirst;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private System.Windows.Forms.TextBox txtPageIndex;
    }
}