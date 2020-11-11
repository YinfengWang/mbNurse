namespace HISPlus
{
    partial class NurseScheduleFrm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.btnCopy = new HISPlus.UserControls.UcButton();
            this.btnNextWeek = new HISPlus.UserControls.UcButton();
            this.btnPreWeek = new HISPlus.UserControls.UcButton();
            this.dtPicker = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDept = new HISPlus.UserControls.UcComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpSchedule = new System.Windows.Forms.GroupBox();
            this.lblMemo = new System.Windows.Forms.Label();
            this.dgvSchedule = new System.Windows.Forms.DataGridView();
            this.lblWeekDay0 = new System.Windows.Forms.Label();
            this.lblDate0 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTopLeft = new System.Windows.Forms.Label();
            this.cmnuNurseDuty = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox1.SuspendLayout();
            this.grpSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnCopy);
            this.groupBox1.Controls.Add(this.btnNextWeek);
            this.groupBox1.Controls.Add(this.btnPreWeek);
            this.groupBox1.Controls.Add(this.dtPicker);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbDept);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(830, 38);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(606, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(514, 12);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(86, 23);
            this.btnCopy.TabIndex = 7;
            this.btnCopy.Text = "复制上周数据";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnNextWeek
            // 
            this.btnNextWeek.Location = new System.Drawing.Point(433, 12);
            this.btnNextWeek.Name = "btnNextWeek";
            this.btnNextWeek.Size = new System.Drawing.Size(75, 23);
            this.btnNextWeek.TabIndex = 6;
            this.btnNextWeek.Text = "下周";
            this.btnNextWeek.UseVisualStyleBackColor = true;
            this.btnNextWeek.Click += new System.EventHandler(this.btnNextWeek_Click);
            // 
            // btnPreWeek
            // 
            this.btnPreWeek.Location = new System.Drawing.Point(352, 12);
            this.btnPreWeek.Name = "btnPreWeek";
            this.btnPreWeek.Size = new System.Drawing.Size(75, 23);
            this.btnPreWeek.TabIndex = 5;
            this.btnPreWeek.Text = "上周";
            this.btnPreWeek.UseVisualStyleBackColor = true;
            this.btnPreWeek.Click += new System.EventHandler(this.btnPreWeek_Click);
            // 
            // dtPicker
            // 
            this.dtPicker.Location = new System.Drawing.Point(234, 13);
            this.dtPicker.Name = "dtPicker";
            this.dtPicker.Size = new System.Drawing.Size(112, 21);
            this.dtPicker.TabIndex = 4;
            this.dtPicker.ValueChanged += new System.EventHandler(this.dtPicker_ValueChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(198, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "日期";
            // 
            // cmbDept
            // 
            this.cmbDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.Location = new System.Drawing.Point(36, 13);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.Size = new System.Drawing.Size(135, 20);
            this.cmbDept.TabIndex = 2;
            this.cmbDept.SelectedIndexChanged += new System.EventHandler(this.cmbDept_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "科室";
            // 
            // grpSchedule
            // 
            this.grpSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSchedule.Controls.Add(this.lblMemo);
            this.grpSchedule.Controls.Add(this.dgvSchedule);
            this.grpSchedule.Controls.Add(this.lblWeekDay0);
            this.grpSchedule.Controls.Add(this.lblDate0);
            this.grpSchedule.Controls.Add(this.label5);
            this.grpSchedule.Controls.Add(this.label4);
            this.grpSchedule.Controls.Add(this.lblTopLeft);
            this.grpSchedule.Location = new System.Drawing.Point(12, 39);
            this.grpSchedule.Name = "grpSchedule";
            this.grpSchedule.Size = new System.Drawing.Size(830, 479);
            this.grpSchedule.TabIndex = 2;
            this.grpSchedule.TabStop = false;
            // 
            // lblMemo
            // 
            this.lblMemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(228)))), ((int)(((byte)(246)))));
            this.lblMemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMemo.Location = new System.Drawing.Point(163, 17);
            this.lblMemo.Name = "lblMemo";
            this.lblMemo.Size = new System.Drawing.Size(181, 58);
            this.lblMemo.TabIndex = 21;
            this.lblMemo.Text = "备    注";
            this.lblMemo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvSchedule
            // 
            this.dgvSchedule.AllowUserToAddRows = false;
            this.dgvSchedule.AllowUserToDeleteRows = false;
            this.dgvSchedule.AllowUserToOrderColumns = true;
            this.dgvSchedule.AllowUserToResizeColumns = false;
            this.dgvSchedule.AllowUserToResizeRows = false;
            this.dgvSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvSchedule.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchedule.ColumnHeadersVisible = false;
            this.dgvSchedule.Location = new System.Drawing.Point(8, 74);
            this.dgvSchedule.Name = "dgvSchedule";
            this.dgvSchedule.RowHeadersVisible = false;
            this.dgvSchedule.RowTemplate.Height = 23;
            this.dgvSchedule.Size = new System.Drawing.Size(336, 399);
            this.dgvSchedule.TabIndex = 20;
            this.dgvSchedule.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvSchedule_CellBeginEdit);
            this.dgvSchedule.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSchedule_CellDoubleClick);
            this.dgvSchedule.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSchedule_CellEndEdit);
            this.dgvSchedule.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvSchedule_DataError);
            // 
            // lblWeekDay0
            // 
            this.lblWeekDay0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWeekDay0.Location = new System.Drawing.Point(73, 36);
            this.lblWeekDay0.Name = "lblWeekDay0";
            this.lblWeekDay0.Size = new System.Drawing.Size(91, 20);
            this.lblWeekDay0.TabIndex = 6;
            this.lblWeekDay0.Text = "一";
            this.lblWeekDay0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDate0
            // 
            this.lblDate0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDate0.Location = new System.Drawing.Point(73, 17);
            this.lblDate0.Name = "lblDate0";
            this.lblDate0.Size = new System.Drawing.Size(91, 20);
            this.lblDate0.TabIndex = 5;
            this.lblDate0.Text = "05月04";
            this.lblDate0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "护士";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "时间";
            // 
            // lblTopLeft
            // 
            this.lblTopLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTopLeft.Location = new System.Drawing.Point(8, 17);
            this.lblTopLeft.Name = "lblTopLeft";
            this.lblTopLeft.Size = new System.Drawing.Size(66, 58);
            this.lblTopLeft.TabIndex = 2;
            // 
            // cmnuNurseDuty
            // 
            this.cmnuNurseDuty.Name = "cmnuNurseDuty";
            this.cmnuNurseDuty.Size = new System.Drawing.Size(61, 4);
            // 
            // NurseScheduleFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 530);
            this.Controls.Add(this.grpSchedule);
            this.Controls.Add(this.groupBox1);
            this.Name = "NurseScheduleFrm";
            this.Text = "护士排班";
            this.Load += new System.EventHandler(this.NurseScheduleFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.grpSchedule.ResumeLayout(false);
            this.grpSchedule.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private HISPlus.UserControls.UcComboBox cmbDept;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtPicker;
        private HISPlus.UserControls.UcButton btnPreWeek;
        private HISPlus.UserControls.UcButton btnNextWeek;
        private HISPlus.UserControls.UcButton btnCopy;
        private HISPlus.UserControls.UcButton btnSave;
        private System.Windows.Forms.GroupBox grpSchedule;
        private System.Windows.Forms.Label lblTopLeft;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDate0;
        private System.Windows.Forms.Label lblWeekDay0;
        private System.Windows.Forms.DataGridView dgvSchedule;
        private System.Windows.Forms.Label lblMemo;
        private System.Windows.Forms.ContextMenuStrip cmnuNurseDuty;
    }
}