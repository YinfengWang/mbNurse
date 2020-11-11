namespace HISPlus
{
    partial class frmOrderConvertFailed
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderConvertFailed));
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.SERIAL_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WARD_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PATIENT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VISIT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BED_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDER_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDER_SUB_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REPEAT_INDICATOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORDER_TEXT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FREQUENCY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FREQ_COUNTER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FREQ_INTERVAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FREQ_INTERVAL_UNIT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PERFORM_SCHEDULE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SPLIT_MEMO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.START_DATE_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STOP_DATE_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTrace = new System.Windows.Forms.TextBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(666, 444);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 30);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "关闭(&C)";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Location = new System.Drawing.Point(12, 444);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 30);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 60000;
            //this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToOrderColumns = true;
            this.dgvResult.AllowUserToResizeRows = false;
            this.dgvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResult.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SERIAL_NO,
            this.WARD_CODE,
            this.PATIENT_ID,
            this.VISIT_ID,
            this.NAME,
            this.BED_NO,
            this.ORDER_NO,
            this.ORDER_SUB_NO,
            this.REPEAT_INDICATOR,
            this.ORDER_TEXT,
            this.FREQUENCY,
            this.FREQ_COUNTER,
            this.FREQ_INTERVAL,
            this.FREQ_INTERVAL_UNIT,
            this.PERFORM_SCHEDULE,
            this.SPLIT_MEMO,
            this.START_DATE_TIME,
            this.STOP_DATE_TIME});
            this.dgvResult.Location = new System.Drawing.Point(12, 12);
            this.dgvResult.MultiSelect = false;
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.RowTemplate.Height = 23;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.Size = new System.Drawing.Size(744, 262);
            this.dgvResult.TabIndex = 3;
            this.dgvResult.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvResult_RowPostPaint);
            this.dgvResult.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvResult_DataError);
            // 
            // SERIAL_NO
            // 
            this.SERIAL_NO.HeaderText = "序号";
            this.SERIAL_NO.Name = "SERIAL_NO";
            this.SERIAL_NO.ReadOnly = true;
            this.SERIAL_NO.Width = 40;
            // 
            // WARD_CODE
            // 
            this.WARD_CODE.DataPropertyName = "WARD_CODE";
            this.WARD_CODE.HeaderText = "病区";
            this.WARD_CODE.Name = "WARD_CODE";
            this.WARD_CODE.ReadOnly = true;
            this.WARD_CODE.Width = 60;
            // 
            // PATIENT_ID
            // 
            this.PATIENT_ID.DataPropertyName = "PATIENT_ID";
            this.PATIENT_ID.HeaderText = "病人ID";
            this.PATIENT_ID.Name = "PATIENT_ID";
            this.PATIENT_ID.ReadOnly = true;
            this.PATIENT_ID.Width = 80;
            // 
            // VISIT_ID
            // 
            this.VISIT_ID.DataPropertyName = "VISIT_ID";
            this.VISIT_ID.HeaderText = "VISIT_ID";
            this.VISIT_ID.Name = "VISIT_ID";
            this.VISIT_ID.ReadOnly = true;
            this.VISIT_ID.Visible = false;
            // 
            // NAME
            // 
            this.NAME.DataPropertyName = "NAME";
            this.NAME.HeaderText = "姓名";
            this.NAME.Name = "NAME";
            this.NAME.ReadOnly = true;
            this.NAME.Width = 80;
            // 
            // BED_NO
            // 
            this.BED_NO.DataPropertyName = "BED_NO";
            this.BED_NO.HeaderText = "床号";
            this.BED_NO.Name = "BED_NO";
            this.BED_NO.ReadOnly = true;
            this.BED_NO.Width = 60;
            // 
            // ORDER_NO
            // 
            this.ORDER_NO.DataPropertyName = "ORDER_NO";
            this.ORDER_NO.HeaderText = "医嘱序号";
            this.ORDER_NO.Name = "ORDER_NO";
            this.ORDER_NO.ReadOnly = true;
            this.ORDER_NO.Width = 60;
            // 
            // ORDER_SUB_NO
            // 
            this.ORDER_SUB_NO.DataPropertyName = "ORDER_SUB_NO";
            this.ORDER_SUB_NO.HeaderText = "子序号";
            this.ORDER_SUB_NO.Name = "ORDER_SUB_NO";
            this.ORDER_SUB_NO.ReadOnly = true;
            this.ORDER_SUB_NO.Width = 50;
            // 
            // REPEAT_INDICATOR
            // 
            this.REPEAT_INDICATOR.DataPropertyName = "REPEAT_INDICATOR";
            this.REPEAT_INDICATOR.HeaderText = "长/临";
            this.REPEAT_INDICATOR.Name = "REPEAT_INDICATOR";
            this.REPEAT_INDICATOR.ReadOnly = true;
            this.REPEAT_INDICATOR.Width = 50;
            // 
            // ORDER_TEXT
            // 
            this.ORDER_TEXT.DataPropertyName = "ORDER_TEXT";
            this.ORDER_TEXT.HeaderText = "医嘱正文";
            this.ORDER_TEXT.Name = "ORDER_TEXT";
            this.ORDER_TEXT.ReadOnly = true;
            this.ORDER_TEXT.Width = 160;
            // 
            // FREQUENCY
            // 
            this.FREQUENCY.DataPropertyName = "FREQUENCY";
            this.FREQUENCY.HeaderText = "执行频率";
            this.FREQUENCY.Name = "FREQUENCY";
            this.FREQUENCY.ReadOnly = true;
            this.FREQUENCY.Width = 70;
            // 
            // FREQ_COUNTER
            // 
            this.FREQ_COUNTER.DataPropertyName = "FREQ_COUNTER";
            this.FREQ_COUNTER.HeaderText = "频率次数";
            this.FREQ_COUNTER.Name = "FREQ_COUNTER";
            this.FREQ_COUNTER.ReadOnly = true;
            this.FREQ_COUNTER.Width = 70;
            // 
            // FREQ_INTERVAL
            // 
            this.FREQ_INTERVAL.DataPropertyName = "FREQ_INTERVAL";
            this.FREQ_INTERVAL.HeaderText = "频率间隔";
            this.FREQ_INTERVAL.Name = "FREQ_INTERVAL";
            this.FREQ_INTERVAL.ReadOnly = true;
            this.FREQ_INTERVAL.Width = 70;
            // 
            // FREQ_INTERVAL_UNIT
            // 
            this.FREQ_INTERVAL_UNIT.DataPropertyName = "FREQ_INTERVAL_UNIT";
            this.FREQ_INTERVAL_UNIT.HeaderText = "单位";
            this.FREQ_INTERVAL_UNIT.Name = "FREQ_INTERVAL_UNIT";
            this.FREQ_INTERVAL_UNIT.ReadOnly = true;
            this.FREQ_INTERVAL_UNIT.Width = 40;
            // 
            // PERFORM_SCHEDULE
            // 
            this.PERFORM_SCHEDULE.DataPropertyName = "PERFORM_SCHEDULE";
            this.PERFORM_SCHEDULE.HeaderText = "执行时间";
            this.PERFORM_SCHEDULE.Name = "PERFORM_SCHEDULE";
            this.PERFORM_SCHEDULE.ReadOnly = true;
            this.PERFORM_SCHEDULE.Width = 80;
            // 
            // SPLIT_MEMO
            // 
            this.SPLIT_MEMO.DataPropertyName = "SPLIT_MEMO";
            this.SPLIT_MEMO.HeaderText = "拆分情况描述";
            this.SPLIT_MEMO.Name = "SPLIT_MEMO";
            this.SPLIT_MEMO.ReadOnly = true;
            this.SPLIT_MEMO.Width = 380;
            // 
            // START_DATE_TIME
            // 
            this.START_DATE_TIME.DataPropertyName = "START_DATE_TIME";
            dataGridViewCellStyle8.Format = "g";
            dataGridViewCellStyle8.NullValue = null;
            this.START_DATE_TIME.DefaultCellStyle = dataGridViewCellStyle8;
            this.START_DATE_TIME.HeaderText = "开始时间";
            this.START_DATE_TIME.Name = "START_DATE_TIME";
            this.START_DATE_TIME.ReadOnly = true;
            this.START_DATE_TIME.Width = 120;
            // 
            // STOP_DATE_TIME
            // 
            this.STOP_DATE_TIME.DataPropertyName = "STOP_DATE_TIME";
            dataGridViewCellStyle9.Format = "g";
            dataGridViewCellStyle9.NullValue = null;
            this.STOP_DATE_TIME.DefaultCellStyle = dataGridViewCellStyle9;
            this.STOP_DATE_TIME.HeaderText = "结束时间";
            this.STOP_DATE_TIME.Name = "STOP_DATE_TIME";
            this.STOP_DATE_TIME.ReadOnly = true;
            this.STOP_DATE_TIME.Width = 120;
            // 
            // txtTrace
            // 
            this.txtTrace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTrace.Location = new System.Drawing.Point(12, 280);
            this.txtTrace.Multiline = true;
            this.txtTrace.Name = "txtTrace";
            this.txtTrace.ReadOnly = true;
            this.txtTrace.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTrace.Size = new System.Drawing.Size(744, 158);
            this.txtTrace.TabIndex = 4;
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 2000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // frmOrderConvertFailed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 479);
            this.Controls.Add(this.txtTrace);
            this.Controls.Add(this.dgvResult);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnExit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmOrderConvertFailed";
            this.Text = "拆分未成功医嘱";
            this.Load += new System.EventHandler(this.frmOrderConvertFailed_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmOrderConvertFailed_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOrderConvertFailed_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.TextBox txtTrace;
        private System.Windows.Forms.DataGridViewTextBoxColumn SERIAL_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn WARD_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PATIENT_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn VISIT_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn BED_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_SUB_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn REPEAT_INDICATOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORDER_TEXT;
        private System.Windows.Forms.DataGridViewTextBoxColumn FREQUENCY;
        private System.Windows.Forms.DataGridViewTextBoxColumn FREQ_COUNTER;
        private System.Windows.Forms.DataGridViewTextBoxColumn FREQ_INTERVAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn FREQ_INTERVAL_UNIT;
        private System.Windows.Forms.DataGridViewTextBoxColumn PERFORM_SCHEDULE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPLIT_MEMO;
        private System.Windows.Forms.DataGridViewTextBoxColumn START_DATE_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn STOP_DATE_TIME;
        private System.Windows.Forms.Timer timer2;
    }
}