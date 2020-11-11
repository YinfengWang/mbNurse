namespace HISPlus
{
    partial class DictItemMatchFrm1
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
            this.groupBox2 = new DevExpress.XtraEditors.PanelControl();
            this.button2 = new HISPlus.UserControls.UcButton();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.grpSrc = new DevExpress.XtraEditors.GroupControl();
            this.trvSrc = new System.Windows.Forms.TreeView();
            this.grpDest = new DevExpress.XtraEditors.GroupControl();
            this.dgvDest = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DICT_ID_MATCH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_ID_MATCH = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btnDelete = new HISPlus.UserControls.UcButton();
            this.btnAdd = new HISPlus.UserControls.UcButton();
            this.groupBox2.SuspendLayout();
            this.grpSrc.SuspendLayout();
            this.grpDest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDest)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Location = new System.Drawing.Point(16, 529);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(944, 75);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(816, 25);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 38);
            this.button2.TabIndex = 1;
            this.button2.TextValue = "退出";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(688, 25);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 38);
            this.btnSave.TabIndex = 0;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpSrc
            // 
            this.grpSrc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpSrc.Controls.Add(this.trvSrc);
            this.grpSrc.Location = new System.Drawing.Point(16, 15);
            this.grpSrc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpSrc.Name = "grpSrc";
            this.grpSrc.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpSrc.Size = new System.Drawing.Size(413, 506);
            this.grpSrc.TabIndex = 4;
            this.grpSrc.TabStop = false;
            this.grpSrc.Text = "病病";
            // 
            // trvSrc
            // 
            this.trvSrc.BackColor = System.Drawing.SystemColors.Control;
            this.trvSrc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvSrc.Location = new System.Drawing.Point(4, 22);
            this.trvSrc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trvSrc.Name = "trvSrc";
            this.trvSrc.Size = new System.Drawing.Size(405, 480);
            this.trvSrc.TabIndex = 1;
            this.trvSrc.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvSrc_BeforeSelect);
            this.trvSrc.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvSrc_AfterSelect);
            // 
            // grpDest
            // 
            this.grpDest.Controls.Add(this.dgvDest);
            this.grpDest.Controls.Add(this.btnDelete);
            this.grpDest.Controls.Add(this.btnAdd);
            this.grpDest.Location = new System.Drawing.Point(437, 15);
            this.grpDest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpDest.Name = "grpDest";
            this.grpDest.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpDest.Size = new System.Drawing.Size(523, 506);
            this.grpDest.TabIndex = 5;
            this.grpDest.TabStop = false;
            this.grpDest.Text = "护理诊断";
            // 
            // dgvDest
            // 
            this.dgvDest.AllowUserToAddRows = false;
            this.dgvDest.AllowUserToDeleteRows = false;
            this.dgvDest.AllowUserToResizeColumns = false;
            this.dgvDest.AllowUserToResizeRows = false;
            this.dgvDest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDest.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDest.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDest.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.DICT_ID_MATCH,
            this.ITEM_ID_MATCH});
            this.dgvDest.Location = new System.Drawing.Point(8, 21);
            this.dgvDest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvDest.MultiSelect = false;
            this.dgvDest.Name = "dgvDest";
            this.dgvDest.ReadOnly = true;
            this.dgvDest.RowHeadersVisible = false;
            this.dgvDest.RowTemplate.Height = 23;
            this.dgvDest.Size = new System.Drawing.Size(507, 432);
            this.dgvDest.TabIndex = 4;
            this.dgvDest.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDest_CellEnter);
            this.dgvDest.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDest_CellLeave);
            this.dgvDest.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvDest_DataError);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "DICT_ID";
            this.dataGridViewTextBoxColumn1.HeaderText = "DICT_ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ITEM_ID";
            this.dataGridViewTextBoxColumn2.HeaderText = "ITEM_ID";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // DICT_ID_MATCH
            // 
            this.DICT_ID_MATCH.DataPropertyName = "DICT_ID_MATCH";
            this.DICT_ID_MATCH.HeaderText = "DICT_ID_MATCH";
            this.DICT_ID_MATCH.Name = "DICT_ID_MATCH";
            this.DICT_ID_MATCH.ReadOnly = true;
            this.DICT_ID_MATCH.Visible = false;
            // 
            // ITEM_ID_MATCH
            // 
            this.ITEM_ID_MATCH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ITEM_ID_MATCH.DataPropertyName = "ITEM_ID_MATCH";
            this.ITEM_ID_MATCH.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.ITEM_ID_MATCH.HeaderText = "护理诊断";
            this.ITEM_ID_MATCH.Name = "ITEM_ID_MATCH";
            this.ITEM_ID_MATCH.ReadOnly = true;
            this.ITEM_ID_MATCH.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ITEM_ID_MATCH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(395, 461);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 38);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.TextValue = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(267, 461);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 38);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.TextValue = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // DictItemMatchFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 619);
            this.Controls.Add(this.grpDest);
            this.Controls.Add(this.grpSrc);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DictItemMatchFrm";
            this.Text = "字典项目对照";
            this.Load += new System.EventHandler(this.DictItemMatchFrm_Load);
            this.groupBox2.ResumeLayout(false);
            this.grpSrc.ResumeLayout(false);
            this.grpDest.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl groupBox2;
        private HISPlus.UserControls.UcButton button2;
        private HISPlus.UserControls.UcButton btnSave;
        private DevExpress.XtraEditors.GroupControl grpSrc;
        private DevExpress.XtraEditors.GroupControl grpDest;
        private HISPlus.UserControls.UcButton btnDelete;
        private HISPlus.UserControls.UcButton btnAdd;
        private System.Windows.Forms.DataGridView dgvDest;
        private System.Windows.Forms.TreeView trvSrc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DICT_ID_MATCH;
        private System.Windows.Forms.DataGridViewComboBoxColumn ITEM_ID_MATCH;
    }
}