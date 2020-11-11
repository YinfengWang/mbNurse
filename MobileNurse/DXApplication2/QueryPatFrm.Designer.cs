namespace DXApplication2
{
    partial class QueryPatFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryPatFrm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.chkAll = new DevExpress.XtraEditors.CheckEdit();
            this.ucGridView1 = new HISPlus.UserControls.UcCheckGridView();
            this.btnOk = new HISPlus.UserControls.UcButton();
            this.btnQuery = new HISPlus.UserControls.UcButton();
            this.txtPatientid = new HISPlus.UserControls.UcTextBox();
            this.cbxPatInHosState = new HISPlus.UserControls.UcComboBox();
            this.dtStart = new HISPlus.UserControls.UcDatePicker();
            this.dtEnd = new HISPlus.UserControls.UcDatePicker();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "病人ID：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "时间";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(221, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "至";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(221, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 14);
            this.label3.TabIndex = 12;
            this.label3.Text = "状态：";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.chkAll);
            this.panelControl1.Controls.Add(this.txtPatientid);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.cbxPatInHosState);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.dtStart);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.dtEnd);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(475, 101);
            this.panelControl1.TabIndex = 14;
            // 
            // chkAll
            // 
            this.chkAll.Location = new System.Drawing.Point(416, 26);
            this.chkAll.Name = "chkAll";
            this.chkAll.Properties.Caption = "全院";
            this.chkAll.Size = new System.Drawing.Size(59, 19);
            this.chkAll.TabIndex = 13;
            // 
            // ucGridView1
            // 
            this.ucGridView1.AllowAddRows = false;
            this.ucGridView1.AllowDeleteRows = false;
            this.ucGridView1.AllowEdit = false;
            this.ucGridView1.AllowSort = false;
            this.ucGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucGridView1.ColumnAutoWidth = true;
            this.ucGridView1.ColumnsEvenOldRowColor = null;
            this.ucGridView1.DataSource = null;
            this.ucGridView1.Location = new System.Drawing.Point(12, 120);
            this.ucGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.ucGridView1.Name = "ucGridView1";
            this.ucGridView1.ShowRowIndicator = false;
            this.ucGridView1.Size = new System.Drawing.Size(599, 270);
            this.ucGridView1.TabIndex = 16;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Image = null;
            this.btnOk.ImageRight = false;
            this.btnOk.ImageStyle = HISPlus.UserControls.ImageStyle.None;
            this.btnOk.Location = new System.Drawing.Point(509, 69);
            this.btnOk.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnOk.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 30);
            this.btnOk.TabIndex = 13;
            this.btnOk.TextValue = "确定";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.Image = null;
            this.btnQuery.ImageRight = false;
            this.btnQuery.ImageStyle = HISPlus.UserControls.ImageStyle.None;
            this.btnQuery.Location = new System.Drawing.Point(509, 22);
            this.btnQuery.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnQuery.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(90, 30);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.TextValue = "查找";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtPatientid
            // 
            this.txtPatientid.Location = new System.Drawing.Point(68, 24);
            this.txtPatientid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPatientid.MaxLength = 0;
            this.txtPatientid.Multiline = false;
            this.txtPatientid.Name = "txtPatientid";
            this.txtPatientid.PasswordChar = '\0';
            this.txtPatientid.ReadOnly = false;
            this.txtPatientid.Size = new System.Drawing.Size(131, 19);
            this.txtPatientid.TabIndex = 2;
            // 
            // cbxPatInHosState
            // 
            this.cbxPatInHosState.AutoScroll = true;
            this.cbxPatInHosState.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cbxPatInHosState.DataSource = null;
            this.cbxPatInHosState.DisplayMember = null;
            this.cbxPatInHosState.DropDownHeight = 0;
            this.cbxPatInHosState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbxPatInHosState.DropDownWidth = 0;
            this.cbxPatInHosState.DroppedDown = false;
            this.cbxPatInHosState.FormattingEnabled = false;
            this.cbxPatInHosState.IntegralHeight = true;
            this.cbxPatInHosState.Location = new System.Drawing.Point(282, 24);
            this.cbxPatInHosState.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbxPatInHosState.MaxDropDownItems = 0;
            this.cbxPatInHosState.MaximumSize = new System.Drawing.Size(875, 22);
            this.cbxPatInHosState.MinimumSize = new System.Drawing.Size(35, 22);
            this.cbxPatInHosState.Name = "cbxPatInHosState";
            this.cbxPatInHosState.SelectedIndex = -1;
            this.cbxPatInHosState.SelectedValue = null;
            this.cbxPatInHosState.Size = new System.Drawing.Size(128, 22);
            this.cbxPatInHosState.TabIndex = 11;
            this.cbxPatInHosState.ValueMember = null;
            this.cbxPatInHosState.SelectedIndexChanged += new System.EventHandler(this.cbxPatInHosState_SelectedIndexChanged);
            // 
            // dtStart
            // 
            this.dtStart.Location = new System.Drawing.Point(69, 66);
            this.dtStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(130, 19);
            this.dtStart.TabIndex = 3;
            this.dtStart.Value = new System.DateTime(2016, 1, 27, 23, 48, 57, 165);
            // 
            // dtEnd
            // 
            this.dtEnd.Location = new System.Drawing.Point(282, 67);
            this.dtEnd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(128, 19);
            this.dtEnd.TabIndex = 9;
            this.dtEnd.Value = new System.DateTime(2016, 1, 27, 23, 48, 57, 174);
            // 
            // QueryPatFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 401);
            this.Controls.Add(this.ucGridView1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QueryPatFrm";
            this.Text = "查询出院病人";
            this.Load += new System.EventHandler(this.QueryPatFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private HISPlus.UserControls.UcButton btnQuery;
        private HISPlus.UserControls.UcTextBox txtPatientid;
        private HISPlus.UserControls.UcDatePicker dtStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private HISPlus.UserControls.UcDatePicker dtEnd;
        private HISPlus.UserControls.UcComboBox cbxPatInHosState;
        private System.Windows.Forms.Label label3;
        private HISPlus.UserControls.UcButton btnOk;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private HISPlus.UserControls.UcCheckGridView ucGridView1;
        private DevExpress.XtraEditors.CheckEdit chkAll;
    }
}