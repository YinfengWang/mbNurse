namespace HISPlus
{
    partial class DictItemMatchFrm
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
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.grpSrc = new DevExpress.XtraEditors.PanelControl();
            this.grpDest = new DevExpress.XtraEditors.PanelControl();
            this.ucTreeList1 = new HISPlus.UserControls.UcTreeList();
            this.ucGridView1 = new HISPlus.UserControls.UcGridView();
            this.btnDelete = new HISPlus.UserControls.UcButton();
            this.btnAdd = new HISPlus.UserControls.UcButton();
            this.groupBox2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.grpSrc)).BeginInit();
            this.grpSrc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpDest)).BeginInit();
            this.grpDest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageRight = false;
            this.btnSave.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSave.Location = new System.Drawing.Point(27, 137);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.MaximumSize = new System.Drawing.Size(90, 28);
            this.btnSave.MinimumSize = new System.Drawing.Size(60, 28);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 28);
            this.btnSave.TabIndex = 0;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpSrc
            // 
            this.grpSrc.Controls.Add(this.ucTreeList1);
            this.grpSrc.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpSrc.Location = new System.Drawing.Point(0, 0);
            this.grpSrc.Margin = new System.Windows.Forms.Padding(4);
            this.grpSrc.Name = "grpSrc";
            this.grpSrc.Padding = new System.Windows.Forms.Padding(4);
            this.grpSrc.Size = new System.Drawing.Size(300, 619);
            this.grpSrc.TabIndex = 4;
            // 
            // grpDest
            // 
            this.grpDest.Controls.Add(this.ucGridView1);
            this.grpDest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDest.Location = new System.Drawing.Point(300, 0);
            this.grpDest.Margin = new System.Windows.Forms.Padding(4);
            this.grpDest.Name = "grpDest";
            this.grpDest.Padding = new System.Windows.Forms.Padding(4);
            this.grpDest.Size = new System.Drawing.Size(793, 619);
            this.grpDest.TabIndex = 5;
            // 
            // ucTreeList1
            // 
            this.ucTreeList1.AllowDrag = false;
            this.ucTreeList1.DataSource = null;
            this.ucTreeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTreeList1.DragFlag = false;
            this.ucTreeList1.EnabledColumnName = "Enabled";
            this.ucTreeList1.ImageColumnName = null;
            this.ucTreeList1.ImageList = null;
            this.ucTreeList1.KeyFieldName = "ID";
            this.ucTreeList1.LevelLimit = 0;
            this.ucTreeList1.Location = new System.Drawing.Point(6, 6);
            this.ucTreeList1.MultiSelect = false;
            this.ucTreeList1.Name = "ucTreeList1";
            this.ucTreeList1.ParentFieldName = "ParentID";
            this.ucTreeList1.RootValue = 0;
            this.ucTreeList1.SelectedNode = null;
            this.ucTreeList1.ShowCheckBoxes = false;
            this.ucTreeList1.ShowHeader = true;
            this.ucTreeList1.Size = new System.Drawing.Size(288, 607);
            this.ucTreeList1.TabIndex = 9;
            // 
            // ucGridView1
            // 
            this.ucGridView1.AllowDeleteRows = false;
            this.ucGridView1.AllowEdit = false;
            this.ucGridView1.AllowSort = false;
            this.ucGridView1.ColumnAutoWidth = true;
            this.ucGridView1.ColumnsEvenOldRowColor = null;
            this.ucGridView1.DataSource = null;
            this.ucGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGridView1.Location = new System.Drawing.Point(6, 6);
            this.ucGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucGridView1.Name = "ucGridView1";
            this.ucGridView1.ShowRowIndicator = false;
            this.ucGridView1.Size = new System.Drawing.Size(781, 607);
            this.ucGridView1.TabIndex = 8;
            // 
            // btnDelete
            // 
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageRight = false;
            this.btnDelete.ImageStyle = HISPlus.UserControls.ImageStyle.Delete;
            this.btnDelete.Location = new System.Drawing.Point(27, 78);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.MaximumSize = new System.Drawing.Size(90, 28);
            this.btnDelete.MinimumSize = new System.Drawing.Size(60, 28);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 28);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.TextValue = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAdd.Enabled = false;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageRight = false;
            this.btnAdd.ImageStyle = HISPlus.UserControls.ImageStyle.Add;
            this.btnAdd.Location = new System.Drawing.Point(27, 20);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.MaximumSize = new System.Drawing.Size(90, 28);
            this.btnAdd.MinimumSize = new System.Drawing.Size(60, 28);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 28);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.TextValue = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(1093, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(152, 619);
            this.groupBox2.TabIndex = 6;
            // 
            // DictItemMatchFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 619);
            this.Controls.Add(this.grpDest);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpSrc);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DictItemMatchFrm";
            this.Text = "字典项目对照";
            this.Load += new System.EventHandler(this.DictItemMatchFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpSrc)).EndInit();
            this.grpSrc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpDest)).EndInit();
            this.grpDest.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HISPlus.UserControls.UcButton btnSave;
        private DevExpress.XtraEditors.PanelControl grpSrc;
        private DevExpress.XtraEditors.PanelControl grpDest;
        private HISPlus.UserControls.UcButton btnDelete;
        private HISPlus.UserControls.UcButton btnAdd;
        private DevExpress.XtraEditors.PanelControl groupBox2;
        private UserControls.UcGridView ucGridView1;
        private UserControls.UcTreeList ucTreeList1;
    }
}