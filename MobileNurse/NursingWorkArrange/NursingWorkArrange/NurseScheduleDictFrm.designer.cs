namespace HISPlus
{
    partial class NurseScheduleDictFrm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvScheduleDict = new System.Windows.Forms.DataGridView();
            this.grpFunc = new System.Windows.Forms.GroupBox();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.btnDelete = new HISPlus.UserControls.UcButton();
            this.DEPT_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SCHEDULE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.START_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.END_TIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SERIAL_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScheduleDict)).BeginInit();
            this.grpFunc.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvScheduleDict);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(762, 379);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // dgvScheduleDict
            // 
            this.dgvScheduleDict.AllowUserToDeleteRows = false;
            this.dgvScheduleDict.AllowUserToOrderColumns = true;
            this.dgvScheduleDict.AllowUserToResizeRows = false;
            this.dgvScheduleDict.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvScheduleDict.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvScheduleDict.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScheduleDict.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DEPT_CODE,
            this.SCHEDULE,
            this.START_TIME,
            this.END_TIME,
            this.SERIAL_NO});
            this.dgvScheduleDict.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvScheduleDict.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvScheduleDict.Location = new System.Drawing.Point(3, 17);
            this.dgvScheduleDict.Name = "dgvScheduleDict";
            this.dgvScheduleDict.RowTemplate.Height = 23;
            this.dgvScheduleDict.Size = new System.Drawing.Size(756, 359);
            this.dgvScheduleDict.TabIndex = 1;
            this.dgvScheduleDict.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvScheduleDict_CellBeginEdit);
            this.dgvScheduleDict.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvScheduleDict_CellEndEdit);
            this.dgvScheduleDict.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvScheduleDict_DefaultValuesNeeded);
            this.dgvScheduleDict.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvScheduleDict_DataError);
            // 
            // grpFunc
            // 
            this.grpFunc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFunc.Controls.Add(this.btnSave);
            this.grpFunc.Controls.Add(this.btnDelete);
            this.grpFunc.Location = new System.Drawing.Point(12, 397);
            this.grpFunc.Name = "grpFunc";
            this.grpFunc.Size = new System.Drawing.Size(759, 55);
            this.grpFunc.TabIndex = 2;
            this.grpFunc.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(663, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保  存(&S)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(535, 19);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 30);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "删  除(&D)";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // DEPT_CODE
            // 
            this.DEPT_CODE.DataPropertyName = "DEPT_CODE";
            this.DEPT_CODE.HeaderText = "护理单元";
            this.DEPT_CODE.Name = "DEPT_CODE";
            this.DEPT_CODE.Visible = false;
            // 
            // SCHEDULE
            // 
            this.SCHEDULE.DataPropertyName = "SCHEDULE";
            this.SCHEDULE.HeaderText = "班次";
            this.SCHEDULE.Name = "SCHEDULE";
            this.SCHEDULE.Width = 80;
            // 
            // START_TIME
            // 
            this.START_TIME.DataPropertyName = "START_TIME";
            dataGridViewCellStyle2.Format = "t";
            dataGridViewCellStyle2.NullValue = null;
            this.START_TIME.DefaultCellStyle = dataGridViewCellStyle2;
            this.START_TIME.HeaderText = "开始时间";
            this.START_TIME.Name = "START_TIME";
            this.START_TIME.Width = 60;
            // 
            // END_TIME
            // 
            this.END_TIME.DataPropertyName = "END_TIME";
            dataGridViewCellStyle3.Format = "t";
            dataGridViewCellStyle3.NullValue = null;
            this.END_TIME.DefaultCellStyle = dataGridViewCellStyle3;
            this.END_TIME.HeaderText = "结束时间";
            this.END_TIME.Name = "END_TIME";
            this.END_TIME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.END_TIME.Width = 60;
            // 
            // SERIAL_NO
            // 
            this.SERIAL_NO.DataPropertyName = "SERIAL_NO";
            this.SERIAL_NO.HeaderText = "排序";
            this.SERIAL_NO.Name = "SERIAL_NO";
            this.SERIAL_NO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SERIAL_NO.Width = 40;
            // 
            // NurseScheduleDictFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 464);
            this.Controls.Add(this.grpFunc);
            this.Controls.Add(this.groupBox1);
            this.Name = "NurseScheduleDictFrm";
            this.Text = "护士排班班次定义";
            this.Load += new System.EventHandler(this.NurseScheduleDictFrm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvScheduleDict)).EndInit();
            this.grpFunc.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvScheduleDict;
        private System.Windows.Forms.GroupBox grpFunc;
        private HISPlus.UserControls.UcButton btnSave;
        private HISPlus.UserControls.UcButton btnDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn DEPT_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SCHEDULE;
        private System.Windows.Forms.DataGridViewTextBoxColumn START_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn END_TIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn SERIAL_NO;
    }
}