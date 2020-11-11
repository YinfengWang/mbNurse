namespace HISPlus
{
    partial class PIOListFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PIOListFrm));
            this.groupBox2 = new DevExpress.XtraEditors.PanelControl();
            this.dgvNurseDiagnose = new System.Windows.Forms.DataGridView();
            this.ITEM_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_VALUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.START_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STOP_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.START_NURSE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STOP_NURSE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DICT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CREATE_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new HISPlus.UserControls.UcButton();
            this.btnStop = new HISPlus.UserControls.UcButton();
            this.btnStart = new HISPlus.UserControls.UcButton();
            this.btnDelete = new HISPlus.UserControls.UcButton();
            this.btnAdd = new HISPlus.UserControls.UcButton();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNurseDiagnose)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvNurseDiagnose);
            this.groupBox2.Location = new System.Drawing.Point(13, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(1176, 585);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "护理诊断及目标措施";
            // 
            // dgvNurseDiagnose
            // 
            this.dgvNurseDiagnose.AllowUserToAddRows = false;
            this.dgvNurseDiagnose.AllowUserToDeleteRows = false;
            this.dgvNurseDiagnose.AllowUserToOrderColumns = true;
            this.dgvNurseDiagnose.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvNurseDiagnose.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNurseDiagnose.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ITEM_NAME,
            this.ITEM_VALUE,
            this.START_DATE,
            this.STOP_DATE,
            this.STATUS,
            this.START_NURSE,
            this.STOP_NURSE,
            this.DICT_ID,
            this.ITEM_ID,
            this.CREATE_DATE});
            this.dgvNurseDiagnose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNurseDiagnose.Location = new System.Drawing.Point(4, 22);
            this.dgvNurseDiagnose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvNurseDiagnose.MultiSelect = false;
            this.dgvNurseDiagnose.Name = "dgvNurseDiagnose";
            this.dgvNurseDiagnose.ReadOnly = true;
            this.dgvNurseDiagnose.RowTemplate.Height = 23;
            this.dgvNurseDiagnose.ShowCellErrors = false;
            this.dgvNurseDiagnose.ShowRowErrors = false;
            this.dgvNurseDiagnose.Size = new System.Drawing.Size(1168, 559);
            this.dgvNurseDiagnose.TabIndex = 3;
            this.dgvNurseDiagnose.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNurseDiagnose_RowEnter);
            this.dgvNurseDiagnose.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNurseDiagnose_RowLeave);
            this.dgvNurseDiagnose.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvNurseDiagnose_RowPostPaint);
            // 
            // ITEM_NAME
            // 
            this.ITEM_NAME.DataPropertyName = "ITEM_NAME";
            this.ITEM_NAME.HeaderText = "护理诊断";
            this.ITEM_NAME.Name = "ITEM_NAME";
            this.ITEM_NAME.ReadOnly = true;
            // 
            // ITEM_VALUE
            // 
            this.ITEM_VALUE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ITEM_VALUE.DataPropertyName = "ITEM_VALUE";
            this.ITEM_VALUE.HeaderText = "目标及措施";
            this.ITEM_VALUE.Name = "ITEM_VALUE";
            this.ITEM_VALUE.ReadOnly = true;
            // 
            // START_DATE
            // 
            this.START_DATE.DataPropertyName = "START_DATE";
            dataGridViewCellStyle1.Format = "g";
            dataGridViewCellStyle1.NullValue = null;
            this.START_DATE.DefaultCellStyle = dataGridViewCellStyle1;
            this.START_DATE.HeaderText = "开始时间";
            this.START_DATE.Name = "START_DATE";
            this.START_DATE.ReadOnly = true;
            // 
            // STOP_DATE
            // 
            this.STOP_DATE.DataPropertyName = "STOP_DATE";
            dataGridViewCellStyle2.Format = "g";
            dataGridViewCellStyle2.NullValue = null;
            this.STOP_DATE.DefaultCellStyle = dataGridViewCellStyle2;
            this.STOP_DATE.HeaderText = "结束时间";
            this.STOP_DATE.Name = "STOP_DATE";
            this.STOP_DATE.ReadOnly = true;
            // 
            // STATUS
            // 
            this.STATUS.HeaderText = "状态";
            this.STATUS.Name = "STATUS";
            this.STATUS.ReadOnly = true;
            this.STATUS.Width = 40;
            // 
            // START_NURSE
            // 
            this.START_NURSE.DataPropertyName = "START_NURSE";
            this.START_NURSE.HeaderText = "开始护士";
            this.START_NURSE.Name = "START_NURSE";
            this.START_NURSE.ReadOnly = true;
            this.START_NURSE.Width = 80;
            // 
            // STOP_NURSE
            // 
            this.STOP_NURSE.DataPropertyName = "STOP_NURSE";
            this.STOP_NURSE.HeaderText = "结束护士";
            this.STOP_NURSE.Name = "STOP_NURSE";
            this.STOP_NURSE.ReadOnly = true;
            this.STOP_NURSE.Width = 80;
            // 
            // DICT_ID
            // 
            this.DICT_ID.DataPropertyName = "DICT_ID";
            this.DICT_ID.HeaderText = "DICT_ID";
            this.DICT_ID.Name = "DICT_ID";
            this.DICT_ID.ReadOnly = true;
            this.DICT_ID.Visible = false;
            // 
            // ITEM_ID
            // 
            this.ITEM_ID.DataPropertyName = "ITEM_ID";
            this.ITEM_ID.HeaderText = "ITEM_ID";
            this.ITEM_ID.Name = "ITEM_ID";
            this.ITEM_ID.ReadOnly = true;
            this.ITEM_ID.Visible = false;
            // 
            // CREATE_DATE
            // 
            this.CREATE_DATE.DataPropertyName = "CREATE_DATE";
            this.CREATE_DATE.HeaderText = "CREATE_DATE";
            this.CREATE_DATE.Name = "CREATE_DATE";
            this.CREATE_DATE.ReadOnly = true;
            this.CREATE_DATE.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnExit);
            this.groupBox3.Controls.Add(this.btnStop);
            this.groupBox3.Controls.Add(this.btnStart);
            this.groupBox3.Controls.Add(this.btnDelete);
            this.groupBox3.Controls.Add(this.btnAdd);
            this.groupBox3.Location = new System.Drawing.Point(13, 604);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(1176, 66);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(1048, 20);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 38);
            this.btnExit.TabIndex = 4;
            this.btnExit.TextValue = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(920, 21);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(120, 38);
            this.btnStop.TabIndex = 3;
            this.btnStop.TextValue = "结束(&E)";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(792, 21);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(120, 38);
            this.btnStart.TabIndex = 2;
            this.btnStart.TextValue = "开始(&S)";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(136, 20);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 38);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.TextValue = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(8, 20);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 38);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.TextValue = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // PIOListFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 685);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "PIOListFrm";
            this.Text = "护理诊断及目标措施";
            this.Load += new System.EventHandler(this.PIOListFrm_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNurseDiagnose)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl groupBox2;
        private DevExpress.XtraEditors.PanelControl groupBox3;
        private HISPlus.UserControls.UcButton btnAdd;
        private HISPlus.UserControls.UcButton btnDelete;
        private HISPlus.UserControls.UcButton btnStart;
        private HISPlus.UserControls.UcButton btnStop;
        private System.Windows.Forms.DataGridView dgvNurseDiagnose;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_VALUE;
        private System.Windows.Forms.DataGridViewTextBoxColumn START_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn STOP_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn START_NURSE;
        private System.Windows.Forms.DataGridViewTextBoxColumn STOP_NURSE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DICT_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CREATE_DATE;
        private HISPlus.UserControls.UcButton btnExit;
    }
}