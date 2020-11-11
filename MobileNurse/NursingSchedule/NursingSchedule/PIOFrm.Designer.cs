namespace HISPlus
{
    partial class PIOFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PIOFrm));
            this.groupBox1 = new DevExpress.XtraEditors.PanelControl();
            this.dgvNurseDiagnose = new System.Windows.Forms.DataGridView();
            this.ITEM_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESC_SPELL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DICT_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearch = new HISPlus.UserControls.UcTextBox();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new DevExpress.XtraEditors.PanelControl();
            this.txtContent = new HISPlus.UserControls.UcTextArea();
            this.groupBox3 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new HISPlus.UserControls.UcButton();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNurseDiagnose)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.dgvNurseDiagnose);
            this.groupBox1.Controls.Add(this.txtSearch);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(10, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 470);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "护理诊断";
            // 
            // dgvNurseDiagnose
            // 
            this.dgvNurseDiagnose.AllowUserToAddRows = false;
            this.dgvNurseDiagnose.AllowUserToDeleteRows = false;
            this.dgvNurseDiagnose.AllowUserToResizeColumns = false;
            this.dgvNurseDiagnose.AllowUserToResizeRows = false;
            this.dgvNurseDiagnose.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvNurseDiagnose.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNurseDiagnose.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ITEM_NAME,
            this.DESC_SPELL,
            this.DICT_ID,
            this.ITEM_ID});
            this.dgvNurseDiagnose.Location = new System.Drawing.Point(10, 45);
            this.dgvNurseDiagnose.MultiSelect = false;
            this.dgvNurseDiagnose.Name = "dgvNurseDiagnose";
            this.dgvNurseDiagnose.ReadOnly = true;
            this.dgvNurseDiagnose.RowTemplate.Height = 23;
            this.dgvNurseDiagnose.ShowCellErrors = false;
            this.dgvNurseDiagnose.ShowRowErrors = false;
            this.dgvNurseDiagnose.Size = new System.Drawing.Size(239, 416);
            this.dgvNurseDiagnose.TabIndex = 2;
            this.dgvNurseDiagnose.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNurseDiagnose_CellDoubleClick);
            this.dgvNurseDiagnose.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNurseDiagnose_CellClick);
            // 
            // ITEM_NAME
            // 
            this.ITEM_NAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ITEM_NAME.DataPropertyName = "ITEM_NAME";
            this.ITEM_NAME.HeaderText = "护理诊断";
            this.ITEM_NAME.Name = "ITEM_NAME";
            this.ITEM_NAME.ReadOnly = true;
            // 
            // DESC_SPELL
            // 
            this.DESC_SPELL.DataPropertyName = "DESC_SPELL";
            this.DESC_SPELL.HeaderText = "搜索码";
            this.DESC_SPELL.Name = "DESC_SPELL";
            this.DESC_SPELL.ReadOnly = true;
            this.DESC_SPELL.Visible = false;
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
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(53, 18);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(196, 21);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "查询";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtContent);
            this.groupBox2.Location = new System.Drawing.Point(271, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(499, 405);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "护理目标及措施";
            // 
            // txtContent
            // 
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.Location = new System.Drawing.Point(3, 17);
            this.txtContent.MaxLength = 1000;
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtContent.Size = new System.Drawing.Size(493, 385);
            this.txtContent.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Location = new System.Drawing.Point(274, 419);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(493, 59);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(397, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.TextValue = "退出";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(301, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.TextValue = "确定";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // PIOFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 490);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PIOFrm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PIO";
            this.Load += new System.EventHandler(this.PIOFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNurseDiagnose)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl groupBox1;
        private HISPlus.UserControls.UcTextBox txtSearch;
        private DevExpress.XtraEditors.LabelControl label1;
        private System.Windows.Forms.DataGridView dgvNurseDiagnose;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESC_SPELL;
        private System.Windows.Forms.DataGridViewTextBoxColumn DICT_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_ID;
        private DevExpress.XtraEditors.PanelControl groupBox2;
        private HISPlus.UserControls.UcTextArea txtContent;
        private DevExpress.XtraEditors.PanelControl groupBox3;
        private HISPlus.UserControls.UcButton btnCancel;
        private HISPlus.UserControls.UcButton btnSave;
    }
}