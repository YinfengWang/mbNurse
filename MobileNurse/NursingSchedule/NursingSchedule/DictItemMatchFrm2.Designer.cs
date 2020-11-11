namespace HISPlus
{
    partial class DictItemMatchFrm2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DictItemMatchFrm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.grpSrc = new DevExpress.XtraEditors.GroupControl();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.grpDest = new DevExpress.XtraEditors.GroupControl();
            this.dgvDest = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DICT_ID_MATCH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_ID_MATCH = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btnDelete = new HISPlus.UserControls.UcButton();
            this.btnAdd = new HISPlus.UserControls.UcButton();
            this.groupBox2 = new DevExpress.XtraEditors.PanelControl();
            this.grpSrc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.grpDest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDest)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(273, 26);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 45);
            this.btnSave.TabIndex = 0;
            this.btnSave.TextValue = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpSrc
            // 
            this.grpSrc.Controls.Add(this.treeList1);
            this.grpSrc.Location = new System.Drawing.Point(16, 15);
            this.grpSrc.Margin = new System.Windows.Forms.Padding(4);
            this.grpSrc.Name = "grpSrc";
            this.grpSrc.Padding = new System.Windows.Forms.Padding(4);
            this.grpSrc.Size = new System.Drawing.Size(300, 591);
            this.grpSrc.TabIndex = 4;
            this.grpSrc.TabStop = false;
            this.grpSrc.Text = "病病";
            // 
            // treeList1
            // 
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(4, 22);
            this.treeList1.Name = "treeList1";
            this.treeList1.BeginUnboundLoad();
            this.treeList1.AppendNode(new object[0], -1);
            this.treeList1.EndUnboundLoad();
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.Padding = new System.Windows.Forms.Padding(3);
            this.treeList1.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.ShowForFocusedRow;
            this.treeList1.Size = new System.Drawing.Size(292, 565);
            this.treeList1.TabIndex = 2;
            this.treeList1.AfterFocusNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeList1_AfterFocusNode);
            this.treeList1.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.treeList1_CustomDrawNodeCell);
            // 
            // grpDest
            // 
            this.grpDest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDest.Controls.Add(this.dgvDest);
            this.grpDest.Location = new System.Drawing.Point(324, 15);
            this.grpDest.Margin = new System.Windows.Forms.Padding(4);
            this.grpDest.Name = "grpDest";
            this.grpDest.Padding = new System.Windows.Forms.Padding(4);
            this.grpDest.Size = new System.Drawing.Size(462, 591);
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
            this.dgvDest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDest.Location = new System.Drawing.Point(4, 22);
            this.dgvDest.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDest.MultiSelect = false;
            this.dgvDest.Name = "dgvDest";
            this.dgvDest.ReadOnly = true;
            this.dgvDest.RowHeadersVisible = false;
            this.dgvDest.RowTemplate.Height = 23;
            this.dgvDest.Size = new System.Drawing.Size(454, 565);
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
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(145, 26);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 45);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.TextValue = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Enabled = false;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(17, 26);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 45);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.TextValue = "新增";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Location = new System.Drawing.Point(820, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(412, 87);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // DictItemMatchFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 619);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpDest);
            this.Controls.Add(this.grpSrc);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DictItemMatchFrm";
            this.Text = "字典项目对照";
            this.Load += new System.EventHandler(this.DictItemMatchFrm_Load);
            this.grpSrc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.grpDest.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDest)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HISPlus.UserControls.UcButton btnSave;
        private DevExpress.XtraEditors.GroupControl grpSrc;
        private DevExpress.XtraEditors.GroupControl grpDest;
        private HISPlus.UserControls.UcButton btnDelete;
        private HISPlus.UserControls.UcButton btnAdd;
        private System.Windows.Forms.DataGridView dgvDest;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DICT_ID_MATCH;
        private System.Windows.Forms.DataGridViewComboBoxColumn ITEM_ID_MATCH;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraEditors.PanelControl groupBox2;
    }
}