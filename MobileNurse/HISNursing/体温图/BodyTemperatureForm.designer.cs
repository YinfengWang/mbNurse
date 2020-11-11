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
            this.grpBodyTemper = new System.Windows.Forms.GroupBox();
            this.grpFunction = new System.Windows.Forms.GroupBox();
            this.grpTimeInterval = new System.Windows.Forms.GroupBox();
            this.rdoFourHour = new System.Windows.Forms.RadioButton();
            this.rdoTwoHour = new System.Windows.Forms.RadioButton();
            this.rdoOneHour = new System.Windows.Forms.RadioButton();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.btnPrePage = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panelBodyTemper = new System.Windows.Forms.Panel();
            this.sbHorizontal = new System.Windows.Forms.HScrollBar();
            this.sbVertical = new System.Windows.Forms.VScrollBar();
            this.panelTitle0 = new System.Windows.Forms.Panel();
            this.lblDateInterval = new System.Windows.Forms.Label();
            this.lblOperedDays = new System.Windows.Forms.Label();
            this.lblInpDays = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.panelAppend0 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBreath = new System.Windows.Forms.Label();
            this.panelLegend = new System.Windows.Forms.Panel();
            this.lblPulseTop = new System.Windows.Forms.Label();
            this.lblTemperTop = new System.Windows.Forms.Label();
            this.lblArmpitTemper = new System.Windows.Forms.Label();
            this.lblAnusTemper = new System.Windows.Forms.Label();
            this.lblMouseTemper = new System.Windows.Forms.Label();
            this.lblHeartRate = new System.Windows.Forms.Label();
            this.panelTitle = new System.Windows.Forms.Panel();
            this.panelDrawPic = new System.Windows.Forms.Panel();
            this.panelAppend = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblAge = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblInpDate = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblArchiveNo = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblBedRoom = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBedLabel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.grpBodyTemper.SuspendLayout();
            this.grpFunction.SuspendLayout();
            this.grpTimeInterval.SuspendLayout();
            this.panelBodyTemper.SuspendLayout();
            this.panelTitle0.SuspendLayout();
            this.panelAppend0.SuspendLayout();
            this.panelLegend.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBodyTemper
            // 
            this.grpBodyTemper.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBodyTemper.Controls.Add(this.grpFunction);
            this.grpBodyTemper.Controls.Add(this.panelBodyTemper);
            this.grpBodyTemper.Location = new System.Drawing.Point(4, 28);
            this.grpBodyTemper.Name = "grpBodyTemper";
            this.grpBodyTemper.Size = new System.Drawing.Size(847, 478);
            this.grpBodyTemper.TabIndex = 1;
            this.grpBodyTemper.TabStop = false;
            // 
            // grpFunction
            // 
            this.grpFunction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFunction.Controls.Add(this.btnRefresh);
            this.grpFunction.Controls.Add(this.btnPrint);
            this.grpFunction.Controls.Add(this.grpTimeInterval);
            this.grpFunction.Controls.Add(this.btnNextPage);
            this.grpFunction.Controls.Add(this.btnPrePage);
            this.grpFunction.Controls.Add(this.btnExit);
            this.grpFunction.Location = new System.Drawing.Point(4, 437);
            this.grpFunction.Name = "grpFunction";
            this.grpFunction.Size = new System.Drawing.Size(837, 39);
            this.grpFunction.TabIndex = 47;
            this.grpFunction.TabStop = false;
            // 
            // grpTimeInterval
            // 
            this.grpTimeInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpTimeInterval.Controls.Add(this.rdoFourHour);
            this.grpTimeInterval.Controls.Add(this.rdoTwoHour);
            this.grpTimeInterval.Controls.Add(this.rdoOneHour);
            this.grpTimeInterval.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpTimeInterval.Location = new System.Drawing.Point(191, -2);
            this.grpTimeInterval.Name = "grpTimeInterval";
            this.grpTimeInterval.Size = new System.Drawing.Size(244, 36);
            this.grpTimeInterval.TabIndex = 70;
            this.grpTimeInterval.TabStop = false;
            this.grpTimeInterval.Text = "时间间隔";
            this.grpTimeInterval.Visible = false;
            // 
            // rdoFourHour
            // 
            this.rdoFourHour.Checked = true;
            this.rdoFourHour.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoFourHour.Location = new System.Drawing.Point(170, 12);
            this.rdoFourHour.Name = "rdoFourHour";
            this.rdoFourHour.Size = new System.Drawing.Size(65, 22);
            this.rdoFourHour.TabIndex = 2;
            this.rdoFourHour.TabStop = true;
            this.rdoFourHour.Text = "4小时";
            // 
            // rdoTwoHour
            // 
            this.rdoTwoHour.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoTwoHour.Location = new System.Drawing.Point(90, 12);
            this.rdoTwoHour.Name = "rdoTwoHour";
            this.rdoTwoHour.Size = new System.Drawing.Size(66, 22);
            this.rdoTwoHour.TabIndex = 1;
            this.rdoTwoHour.Text = "2小时";
            // 
            // rdoOneHour
            // 
            this.rdoOneHour.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoOneHour.Location = new System.Drawing.Point(10, 12);
            this.rdoOneHour.Name = "rdoOneHour";
            this.rdoOneHour.Size = new System.Drawing.Size(65, 22);
            this.rdoOneHour.TabIndex = 0;
            this.rdoOneHour.Text = "1小时";
            // 
            // btnNextPage
            // 
            this.btnNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNextPage.Location = new System.Drawing.Point(104, 14);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(75, 23);
            this.btnNextPage.TabIndex = 4;
            this.btnNextPage.Text = "后一页";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnPrePage
            // 
            this.btnPrePage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrePage.Location = new System.Drawing.Point(11, 14);
            this.btnPrePage.Name = "btnPrePage";
            this.btnPrePage.Size = new System.Drawing.Size(75, 23);
            this.btnPrePage.TabIndex = 3;
            this.btnPrePage.Text = "前一页";
            this.btnPrePage.UseVisualStyleBackColor = true;
            this.btnPrePage.Click += new System.EventHandler(this.btnPrePage_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(621, 13);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "刷新(&R)";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(752, 6);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "退出(&X)";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(753, 13);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "打印(&P)";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // panelBodyTemper
            // 
            this.panelBodyTemper.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBodyTemper.Controls.Add(this.sbHorizontal);
            this.panelBodyTemper.Controls.Add(this.sbVertical);
            this.panelBodyTemper.Controls.Add(this.panelTitle0);
            this.panelBodyTemper.Controls.Add(this.panelAppend0);
            this.panelBodyTemper.Controls.Add(this.panelLegend);
            this.panelBodyTemper.Controls.Add(this.panelTitle);
            this.panelBodyTemper.Controls.Add(this.panelDrawPic);
            this.panelBodyTemper.Controls.Add(this.panelAppend);
            this.panelBodyTemper.Location = new System.Drawing.Point(4, 11);
            this.panelBodyTemper.Name = "panelBodyTemper";
            this.panelBodyTemper.Size = new System.Drawing.Size(839, 428);
            this.panelBodyTemper.TabIndex = 45;
            // 
            // sbHorizontal
            // 
            this.sbHorizontal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sbHorizontal.LargeChange = 4;
            this.sbHorizontal.Location = new System.Drawing.Point(0, 409);
            this.sbHorizontal.Maximum = 200;
            this.sbHorizontal.Name = "sbHorizontal";
            this.sbHorizontal.Size = new System.Drawing.Size(821, 18);
            this.sbHorizontal.TabIndex = 49;
            // 
            // sbVertical
            // 
            this.sbVertical.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sbVertical.LargeChange = 4;
            this.sbVertical.Location = new System.Drawing.Point(819, 0);
            this.sbVertical.Name = "sbVertical";
            this.sbVertical.Size = new System.Drawing.Size(18, 410);
            this.sbVertical.TabIndex = 48;
            // 
            // panelTitle0
            // 
            this.panelTitle0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTitle0.Controls.Add(this.lblDateInterval);
            this.panelTitle0.Controls.Add(this.lblOperedDays);
            this.panelTitle0.Controls.Add(this.lblInpDays);
            this.panelTitle0.Controls.Add(this.lblDate);
            this.panelTitle0.Location = new System.Drawing.Point(0, 0);
            this.panelTitle0.Name = "panelTitle0";
            this.panelTitle0.Size = new System.Drawing.Size(86, 83);
            this.panelTitle0.TabIndex = 46;
            this.panelTitle0.Visible = false;
            // 
            // lblDateInterval
            // 
            this.lblDateInterval.BackColor = System.Drawing.SystemColors.Control;
            this.lblDateInterval.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDateInterval.Location = new System.Drawing.Point(-1, 61);
            this.lblDateInterval.Name = "lblDateInterval";
            this.lblDateInterval.Size = new System.Drawing.Size(86, 18);
            this.lblDateInterval.TabIndex = 29;
            this.lblDateInterval.Text = "时       间";
            this.lblDateInterval.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDateInterval.Visible = false;
            // 
            // lblOperedDays
            // 
            this.lblOperedDays.BackColor = System.Drawing.SystemColors.Control;
            this.lblOperedDays.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblOperedDays.Location = new System.Drawing.Point(-1, 46);
            this.lblOperedDays.Name = "lblOperedDays";
            this.lblOperedDays.Size = new System.Drawing.Size(86, 14);
            this.lblOperedDays.TabIndex = 28;
            this.lblOperedDays.Text = "手术产后日数";
            this.lblOperedDays.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOperedDays.Visible = false;
            // 
            // lblInpDays
            // 
            this.lblInpDays.BackColor = System.Drawing.SystemColors.Control;
            this.lblInpDays.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInpDays.Location = new System.Drawing.Point(-1, 26);
            this.lblInpDays.Name = "lblInpDays";
            this.lblInpDays.Size = new System.Drawing.Size(86, 14);
            this.lblInpDays.TabIndex = 27;
            this.lblInpDays.Text = "住 院 日 数";
            this.lblInpDays.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInpDays.Visible = false;
            // 
            // lblDate
            // 
            this.lblDate.BackColor = System.Drawing.SystemColors.Control;
            this.lblDate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDate.Location = new System.Drawing.Point(-1, 6);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(86, 14);
            this.lblDate.TabIndex = 26;
            this.lblDate.Text = "日       期";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDate.Visible = false;
            // 
            // panelAppend0
            // 
            this.panelAppend0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAppend0.Controls.Add(this.label1);
            this.panelAppend0.Controls.Add(this.lblBreath);
            this.panelAppend0.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelAppend0.Location = new System.Drawing.Point(3, 265);
            this.panelAppend0.Name = "panelAppend0";
            this.panelAppend0.Size = new System.Drawing.Size(83, 55);
            this.panelAppend0.TabIndex = 47;
            this.panelAppend0.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(8, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "大便次数";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Visible = false;
            // 
            // lblBreath
            // 
            this.lblBreath.BackColor = System.Drawing.SystemColors.Control;
            this.lblBreath.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBreath.Location = new System.Drawing.Point(8, 4);
            this.lblBreath.Name = "lblBreath";
            this.lblBreath.Size = new System.Drawing.Size(70, 13);
            this.lblBreath.TabIndex = 30;
            this.lblBreath.Text = "呼     吸";
            this.lblBreath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBreath.Visible = false;
            // 
            // panelLegend
            // 
            this.panelLegend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLegend.Controls.Add(this.lblPulseTop);
            this.panelLegend.Controls.Add(this.lblTemperTop);
            this.panelLegend.Controls.Add(this.lblArmpitTemper);
            this.panelLegend.Controls.Add(this.lblAnusTemper);
            this.panelLegend.Controls.Add(this.lblMouseTemper);
            this.panelLegend.Controls.Add(this.lblHeartRate);
            this.panelLegend.Location = new System.Drawing.Point(4, 105);
            this.panelLegend.Name = "panelLegend";
            this.panelLegend.Size = new System.Drawing.Size(82, 154);
            this.panelLegend.TabIndex = 45;
            this.panelLegend.Visible = false;
            // 
            // lblPulseTop
            // 
            this.lblPulseTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPulseTop.BackColor = System.Drawing.SystemColors.Control;
            this.lblPulseTop.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPulseTop.Location = new System.Drawing.Point(8, 6);
            this.lblPulseTop.Name = "lblPulseTop";
            this.lblPulseTop.Size = new System.Drawing.Size(36, 30);
            this.lblPulseTop.TabIndex = 32;
            this.lblPulseTop.Text = "脉搏 次/分";
            this.lblPulseTop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPulseTop.Visible = false;
            // 
            // lblTemperTop
            // 
            this.lblTemperTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTemperTop.BackColor = System.Drawing.SystemColors.Control;
            this.lblTemperTop.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTemperTop.Location = new System.Drawing.Point(48, 6);
            this.lblTemperTop.Name = "lblTemperTop";
            this.lblTemperTop.Size = new System.Drawing.Size(30, 30);
            this.lblTemperTop.TabIndex = 31;
            this.lblTemperTop.Text = "体温 ℃";
            this.lblTemperTop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTemperTop.Visible = false;
            // 
            // lblArmpitTemper
            // 
            this.lblArmpitTemper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblArmpitTemper.BackColor = System.Drawing.SystemColors.Control;
            this.lblArmpitTemper.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblArmpitTemper.Location = new System.Drawing.Point(4, 114);
            this.lblArmpitTemper.Name = "lblArmpitTemper";
            this.lblArmpitTemper.Size = new System.Drawing.Size(30, 14);
            this.lblArmpitTemper.TabIndex = 30;
            this.lblArmpitTemper.Text = "腋表";
            this.lblArmpitTemper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblArmpitTemper.Visible = false;
            // 
            // lblAnusTemper
            // 
            this.lblAnusTemper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAnusTemper.BackColor = System.Drawing.SystemColors.Control;
            this.lblAnusTemper.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAnusTemper.Location = new System.Drawing.Point(4, 92);
            this.lblAnusTemper.Name = "lblAnusTemper";
            this.lblAnusTemper.Size = new System.Drawing.Size(30, 14);
            this.lblAnusTemper.TabIndex = 29;
            this.lblAnusTemper.Text = "肛表";
            this.lblAnusTemper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAnusTemper.Visible = false;
            // 
            // lblMouseTemper
            // 
            this.lblMouseTemper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMouseTemper.BackColor = System.Drawing.SystemColors.Control;
            this.lblMouseTemper.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMouseTemper.Location = new System.Drawing.Point(4, 70);
            this.lblMouseTemper.Name = "lblMouseTemper";
            this.lblMouseTemper.Size = new System.Drawing.Size(30, 14);
            this.lblMouseTemper.TabIndex = 28;
            this.lblMouseTemper.Text = "口表";
            this.lblMouseTemper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMouseTemper.Visible = false;
            // 
            // lblHeartRate
            // 
            this.lblHeartRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHeartRate.BackColor = System.Drawing.SystemColors.Control;
            this.lblHeartRate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblHeartRate.Location = new System.Drawing.Point(4, 48);
            this.lblHeartRate.Name = "lblHeartRate";
            this.lblHeartRate.Size = new System.Drawing.Size(30, 14);
            this.lblHeartRate.TabIndex = 27;
            this.lblHeartRate.Text = "心率";
            this.lblHeartRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHeartRate.Visible = false;
            // 
            // panelTitle
            // 
            this.panelTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTitle.Location = new System.Drawing.Point(128, 4);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(200, 83);
            this.panelTitle.TabIndex = 42;
            this.panelTitle.Visible = false;
            // 
            // panelDrawPic
            // 
            this.panelDrawPic.AllowDrop = true;
            this.panelDrawPic.BackColor = System.Drawing.SystemColors.Control;
            this.panelDrawPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDrawPic.Location = new System.Drawing.Point(128, 100);
            this.panelDrawPic.Name = "panelDrawPic";
            this.panelDrawPic.Size = new System.Drawing.Size(200, 148);
            this.panelDrawPic.TabIndex = 43;
            this.panelDrawPic.Visible = false;
            // 
            // panelAppend
            // 
            this.panelAppend.AllowDrop = true;
            this.panelAppend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAppend.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelAppend.Location = new System.Drawing.Point(130, 256);
            this.panelAppend.Name = "panelAppend";
            this.panelAppend.Size = new System.Drawing.Size(200, 81);
            this.panelAppend.TabIndex = 44;
            this.panelAppend.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblAge);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblInpDate);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.lblArchiveNo);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.lblBedRoom);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lblGender);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblPatientName);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtBedLabel);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(4, -4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(847, 36);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            // 
            // lblAge
            // 
            this.lblAge.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAge.ForeColor = System.Drawing.Color.Blue;
            this.lblAge.Location = new System.Drawing.Point(301, 17);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(41, 12);
            this.lblAge.TabIndex = 33;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 32;
            this.label2.Text = "年龄";
            // 
            // lblInpDate
            // 
            this.lblInpDate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInpDate.ForeColor = System.Drawing.Color.Blue;
            this.lblInpDate.Location = new System.Drawing.Point(412, 17);
            this.lblInpDate.Name = "lblInpDate";
            this.lblInpDate.Size = new System.Drawing.Size(70, 12);
            this.lblInpDate.TabIndex = 31;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(357, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 30;
            this.label14.Text = "入院日期";
            // 
            // lblArchiveNo
            // 
            this.lblArchiveNo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblArchiveNo.ForeColor = System.Drawing.Color.Blue;
            this.lblArchiveNo.Location = new System.Drawing.Point(621, 17);
            this.lblArchiveNo.Name = "lblArchiveNo";
            this.lblArchiveNo.Size = new System.Drawing.Size(99, 12);
            this.lblArchiveNo.TabIndex = 29;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(580, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 28;
            this.label12.Text = "病案号";
            // 
            // lblBedRoom
            // 
            this.lblBedRoom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBedRoom.ForeColor = System.Drawing.Color.Blue;
            this.lblBedRoom.Location = new System.Drawing.Point(517, 17);
            this.lblBedRoom.Name = "lblBedRoom";
            this.lblBedRoom.Size = new System.Drawing.Size(48, 12);
            this.lblBedRoom.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(487, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 26;
            this.label10.Text = "病室";
            // 
            // lblGender
            // 
            this.lblGender.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGender.ForeColor = System.Drawing.Color.Blue;
            this.lblGender.Location = new System.Drawing.Point(234, 17);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(16, 12);
            this.lblGender.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(204, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 24;
            this.label8.Text = "性别";
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientName.ForeColor = System.Drawing.Color.Blue;
            this.lblPatientName.Location = new System.Drawing.Point(154, 17);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(0, 12);
            this.lblPatientName.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(119, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "姓名";
            // 
            // txtBedLabel
            // 
            this.txtBedLabel.Location = new System.Drawing.Point(35, 13);
            this.txtBedLabel.Name = "txtBedLabel";
            this.txtBedLabel.Size = new System.Drawing.Size(66, 21);
            this.txtBedLabel.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "床号";
            // 
            // BodyTemperatureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 507);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpBodyTemper);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BodyTemperatureForm";
            this.Text = "体温记录";
            this.grpBodyTemper.ResumeLayout(false);
            this.grpFunction.ResumeLayout(false);
            this.grpTimeInterval.ResumeLayout(false);
            this.panelBodyTemper.ResumeLayout(false);
            this.panelTitle0.ResumeLayout(false);
            this.panelAppend0.ResumeLayout(false);
            this.panelLegend.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBodyTemper;
        private System.Windows.Forms.Panel panelBodyTemper;
        private System.Windows.Forms.HScrollBar sbHorizontal;
        private System.Windows.Forms.VScrollBar sbVertical;
        private System.Windows.Forms.Panel panelTitle0;
        private System.Windows.Forms.Label lblDateInterval;
        private System.Windows.Forms.Label lblOperedDays;
        private System.Windows.Forms.Label lblInpDays;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Panel panelAppend0;
        private System.Windows.Forms.Label lblBreath;
        private System.Windows.Forms.Panel panelLegend;
        private System.Windows.Forms.Label lblPulseTop;
        private System.Windows.Forms.Label lblTemperTop;
        private System.Windows.Forms.Label lblArmpitTemper;
        private System.Windows.Forms.Label lblAnusTemper;
        private System.Windows.Forms.Label lblMouseTemper;
        private System.Windows.Forms.Label lblHeartRate;
        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Panel panelDrawPic;
        private System.Windows.Forms.Panel panelAppend;
        private System.Windows.Forms.GroupBox grpFunction;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnPrePage;
        private System.Windows.Forms.GroupBox grpTimeInterval;
        private System.Windows.Forms.RadioButton rdoFourHour;
        private System.Windows.Forms.RadioButton rdoTwoHour;
        private System.Windows.Forms.RadioButton rdoOneHour;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblInpDate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblArchiveNo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblBedRoom;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBedLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
    }
}