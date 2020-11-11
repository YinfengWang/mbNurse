namespace HISPlus
{
    partial class BodyTemperatureFormBak
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
            this.btnHistory = new HISPlus.UserControls.UcButton();
            this.btnPrintablePatient = new HISPlus.UserControls.UcButton();
            this.btnRefresh = new HISPlus.UserControls.UcButton();
            this.btnPrint = new HISPlus.UserControls.UcButton();
            this.btnNextPage = new HISPlus.UserControls.UcButton();
            this.btnPrePage = new HISPlus.UserControls.UcButton();
            this.panelBodyTemper = new DevExpress.XtraEditors.PanelControl();
            this.picTop = new System.Windows.Forms.PictureBox();
            this.picLeftTop = new System.Windows.Forms.PictureBox();
            this.sbVertical = new System.Windows.Forms.VScrollBar();
            this.picLeft = new System.Windows.Forms.PictureBox();
            this.picMain = new System.Windows.Forms.PictureBox();
            this.picBottom = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.grpFunction)).BeginInit();
            this.grpFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelBodyTemper)).BeginInit();
            this.panelBodyTemper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLeftTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBottom)).BeginInit();
            this.SuspendLayout();
            // 
            // grpFunction
            // 
            this.grpFunction.Controls.Add(this.btnHistory);
            this.grpFunction.Controls.Add(this.btnPrintablePatient);
            this.grpFunction.Controls.Add(this.btnRefresh);
            this.grpFunction.Controls.Add(this.btnPrint);
            this.grpFunction.Controls.Add(this.btnNextPage);
            this.grpFunction.Controls.Add(this.btnPrePage);
            this.grpFunction.Dock = System.Windows.Forms.DockStyle.Right;
            this.grpFunction.Location = new System.Drawing.Point(935, 0);
            this.grpFunction.Margin = new System.Windows.Forms.Padding(4);
            this.grpFunction.Name = "grpFunction";
            this.grpFunction.Padding = new System.Windows.Forms.Padding(4);
            this.grpFunction.Size = new System.Drawing.Size(248, 843);
            this.grpFunction.TabIndex = 47;
            // 
            // btnHistory
            // 
            this.btnHistory.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.Image = ((System.Drawing.Image)(resources.GetObject("btnHistory.Image")));
            this.btnHistory.ImageRight = false;
            this.btnHistory.ImageStyle = HISPlus.UserControls.ImageStyle.HistoryItem;
            this.btnHistory.Location = new System.Drawing.Point(26, 214);
            this.btnHistory.Margin = new System.Windows.Forms.Padding(4);
            this.btnHistory.MaximumSize = new System.Drawing.Size(120, 30);
            this.btnHistory.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(120, 30);
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
            this.btnPrintablePatient.Location = new System.Drawing.Point(26, 158);
            this.btnPrintablePatient.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrintablePatient.MaximumSize = new System.Drawing.Size(150, 30);
            this.btnPrintablePatient.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnPrintablePatient.Name = "btnPrintablePatient";
            this.btnPrintablePatient.Size = new System.Drawing.Size(150, 30);
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
            this.btnRefresh.Location = new System.Drawing.Point(26, 343);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnRefresh.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.TextValue = "刷新";
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
            this.btnPrint.Location = new System.Drawing.Point(26, 401);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnPrint.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 30);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.TextValue = "打印";
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
            this.btnNextPage.Location = new System.Drawing.Point(26, 87);
            this.btnNextPage.Margin = new System.Windows.Forms.Padding(4);
            this.btnNextPage.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnNextPage.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(100, 30);
            this.btnNextPage.TabIndex = 4;
            this.btnNextPage.TextValue = "后一页";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnPrePage
            // 
            this.btnPrePage.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPrePage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrePage.Image = ((System.Drawing.Image)(resources.GetObject("btnPrePage.Image")));
            this.btnPrePage.ImageRight = false;
            this.btnPrePage.ImageStyle = HISPlus.UserControls.ImageStyle.Backward;
            this.btnPrePage.Location = new System.Drawing.Point(26, 30);
            this.btnPrePage.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrePage.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnPrePage.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnPrePage.Name = "btnPrePage";
            this.btnPrePage.Size = new System.Drawing.Size(100, 30);
            this.btnPrePage.TabIndex = 3;
            this.btnPrePage.TextValue = "前一页";
            this.btnPrePage.UseVisualStyleBackColor = true;
            this.btnPrePage.Click += new System.EventHandler(this.btnPrePage_Click);
            // 
            // panelBodyTemper
            // 
            this.panelBodyTemper.AllowTouchScroll = true;
            this.panelBodyTemper.Controls.Add(this.picTop);
            this.panelBodyTemper.Controls.Add(this.picLeftTop);
            this.panelBodyTemper.Controls.Add(this.sbVertical);
            this.panelBodyTemper.Controls.Add(this.picLeft);
            this.panelBodyTemper.Controls.Add(this.picMain);
            this.panelBodyTemper.Controls.Add(this.picBottom);
            this.panelBodyTemper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBodyTemper.Location = new System.Drawing.Point(0, 0);
            this.panelBodyTemper.Margin = new System.Windows.Forms.Padding(4);
            this.panelBodyTemper.Name = "panelBodyTemper";
            this.panelBodyTemper.Size = new System.Drawing.Size(935, 843);
            this.panelBodyTemper.TabIndex = 45;
            // 
            // picTop
            // 
            this.picTop.Image = ((System.Drawing.Image)(resources.GetObject("picTop.Image")));
            this.picTop.Location = new System.Drawing.Point(132, 0);
            this.picTop.Margin = new System.Windows.Forms.Padding(4);
            this.picTop.Name = "picTop";
            this.picTop.Size = new System.Drawing.Size(545, 90);
            this.picTop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picTop.TabIndex = 52;
            this.picTop.TabStop = false;
            this.picTop.Tag = "Template\\体温图\\TopPart.jpg";
            // 
            // picLeftTop
            // 
            this.picLeftTop.Image = ((System.Drawing.Image)(resources.GetObject("picLeftTop.Image")));
            this.picLeftTop.Location = new System.Drawing.Point(15, 0);
            this.picLeftTop.Margin = new System.Windows.Forms.Padding(4);
            this.picLeftTop.Name = "picLeftTop";
            this.picLeftTop.Size = new System.Drawing.Size(89, 90);
            this.picLeftTop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picLeftTop.TabIndex = 51;
            this.picLeftTop.TabStop = false;
            this.picLeftTop.Tag = "Template\\体温图\\TopLeftPart.jpg";
            // 
            // sbVertical
            // 
            this.sbVertical.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sbVertical.LargeChange = 4;
            this.sbVertical.Location = new System.Drawing.Point(887, 147);
            this.sbVertical.Name = "sbVertical";
            this.sbVertical.Size = new System.Drawing.Size(18, 634);
            this.sbVertical.TabIndex = 48;
            this.sbVertical.ValueChanged += new System.EventHandler(this.sbVertical_ValueChanged);
            // 
            // picLeft
            // 
            this.picLeft.Image = ((System.Drawing.Image)(resources.GetObject("picLeft.Image")));
            this.picLeft.Location = new System.Drawing.Point(15, 135);
            this.picLeft.Margin = new System.Windows.Forms.Padding(4);
            this.picLeft.Name = "picLeft";
            this.picLeft.Size = new System.Drawing.Size(89, 555);
            this.picLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picLeft.TabIndex = 53;
            this.picLeft.TabStop = false;
            this.picLeft.Tag = "Template\\体温图\\LeftPart.jpg";
            // 
            // picMain
            // 
            this.picMain.Image = ((System.Drawing.Image)(resources.GetObject("picMain.Image")));
            this.picMain.Location = new System.Drawing.Point(132, 135);
            this.picMain.Margin = new System.Windows.Forms.Padding(4);
            this.picMain.Name = "picMain";
            this.picMain.Size = new System.Drawing.Size(545, 555);
            this.picMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picMain.TabIndex = 57;
            this.picMain.TabStop = false;
            this.picMain.Tag = "Template\\体温图\\CenterPart.jpg";
            this.picMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMain_MouseDown);
            // 
            // picBottom
            // 
            this.picBottom.Image = ((System.Drawing.Image)(resources.GetObject("picBottom.Image")));
            this.picBottom.Location = new System.Drawing.Point(159, 234);
            this.picBottom.Margin = new System.Windows.Forms.Padding(4);
            this.picBottom.Name = "picBottom";
            this.picBottom.Size = new System.Drawing.Size(636, 170);
            this.picBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picBottom.TabIndex = 56;
            this.picBottom.TabStop = false;
            this.picBottom.Tag = "Template\\体温图\\BottomPart.jpg";
            // 
            // BodyTemperatureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 843);
            this.Controls.Add(this.panelBodyTemper);
            this.Controls.Add(this.grpFunction);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BodyTemperatureForm";
            this.Text = "体温记录";
            this.Resize += new System.EventHandler(this.BodyTemperatureForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.grpFunction)).EndInit();
            this.grpFunction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelBodyTemper)).EndInit();
            this.panelBodyTemper.ResumeLayout(false);
            this.panelBodyTemper.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLeftTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBottom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelBodyTemper;
        private System.Windows.Forms.VScrollBar sbVertical;
        private DevExpress.XtraEditors.PanelControl grpFunction;
        private HISPlus.UserControls.UcButton btnPrint;
        private HISPlus.UserControls.UcButton btnRefresh;
        private HISPlus.UserControls.UcButton btnNextPage;
        private HISPlus.UserControls.UcButton btnPrePage;
        private System.Windows.Forms.PictureBox picLeftTop;
        private System.Windows.Forms.PictureBox picTop;
        private System.Windows.Forms.PictureBox picLeft;
        private System.Windows.Forms.PictureBox picBottom;
        private System.Windows.Forms.PictureBox picMain;
        private HISPlus.UserControls.UcButton btnPrintablePatient;
        private HISPlus.UserControls.UcButton btnHistory;
    }
}