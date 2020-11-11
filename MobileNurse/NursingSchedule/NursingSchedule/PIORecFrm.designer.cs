namespace HISPlus
{
    partial class PIORecFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PIORecFrm));
            this.grpSeach = new DevExpress.XtraEditors.PanelControl();
            this.btnStop = new HISPlus.UserControls.UcButton();
            this.btnDelete = new HISPlus.UserControls.UcButton();
            this.btnNew = new HISPlus.UserControls.UcButton();
            this.btnPrint = new HISPlus.UserControls.UcButton();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.ucGridView1 = new HISPlus.UserControls.UcGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grpSeach)).BeginInit();
            this.grpSeach.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSeach
            // 
            this.grpSeach.Controls.Add(this.btnStop);
            this.grpSeach.Controls.Add(this.btnDelete);
            this.grpSeach.Controls.Add(this.btnNew);
            this.grpSeach.Controls.Add(this.btnPrint);
            this.grpSeach.Controls.Add(this.btnSave);
            this.grpSeach.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpSeach.Location = new System.Drawing.Point(0, 0);
            this.grpSeach.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpSeach.Name = "grpSeach";
            this.grpSeach.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpSeach.Size = new System.Drawing.Size(1022, 45);
            this.grpSeach.TabIndex = 3;
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnStop.Enabled = false;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.ImageRight = false;
            this.btnStop.ImageStyle = HISPlus.UserControls.ImageStyle.SelectAll;
            this.btnStop.Location = new System.Drawing.Point(531, 10);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnStop.MaximumSize = new System.Drawing.Size(79, 23);
            this.btnStop.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(79, 23);
            this.btnStop.TabIndex = 10;
            this.btnStop.TextValue = "停止";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageRight = false;
            this.btnDelete.ImageStyle = HISPlus.UserControls.ImageStyle.Delete;
            this.btnDelete.Location = new System.Drawing.Point(727, 10);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDelete.MaximumSize = new System.Drawing.Size(79, 23);
            this.btnDelete.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(79, 23);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.TextValue = "删除";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNew.Enabled = false;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageRight = false;
            this.btnNew.ImageStyle = HISPlus.UserControls.ImageStyle.Add;
            this.btnNew.Location = new System.Drawing.Point(631, 10);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnNew.MaximumSize = new System.Drawing.Size(79, 23);
            this.btnNew.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(79, 23);
            this.btnNew.TabIndex = 7;
            this.btnNew.TextValue = "新增";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPrint.Enabled = false;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Image = null;
            this.btnPrint.ImageRight = false;
            this.btnPrint.ImageStyle = HISPlus.UserControls.ImageStyle.Add;
            this.btnPrint.Location = new System.Drawing.Point(901, 10);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPrint.MaximumSize = new System.Drawing.Size(88, 23);
            this.btnPrint.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(88, 23);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.TextValue = "打印";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageRight = false;
            this.btnSave.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSave.Location = new System.Drawing.Point(814, 10);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.MaximumSize = new System.Drawing.Size(79, 23);
            this.btnSave.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(79, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ucGridView1
            // 
            this.ucGridView1.AllowAddRows = false;
            this.ucGridView1.AllowDeleteRows = false;
            this.ucGridView1.AllowEdit = false;
            this.ucGridView1.AllowSort = false;
            this.ucGridView1.ColumnAutoWidth = true;
            this.ucGridView1.ColumnsEvenOldRowColor = null;
            this.ucGridView1.DataSource = null;
            this.ucGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGridView1.Location = new System.Drawing.Point(0, 45);
            this.ucGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.ucGridView1.Name = "ucGridView1";
            this.ucGridView1.ShowRowIndicator = false;
            this.ucGridView1.Size = new System.Drawing.Size(1022, 525);
            this.ucGridView1.TabIndex = 3;
            // 
            // PIORecFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 570);
            this.Controls.Add(this.ucGridView1);
            this.Controls.Add(this.grpSeach);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "PIORecFrm";
            this.Text = "护理计划";
            this.Load += new System.EventHandler(this.EvaluationEverydayFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpSeach)).EndInit();
            this.grpSeach.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl grpSeach;
        private HISPlus.UserControls.UcButton btnPrint;
        private HISPlus.UserControls.UcButton btnSave;
        private HISPlus.UserControls.UcButton btnNew;
        private HISPlus.UserControls.UcButton btnDelete;
        private HISPlus.UserControls.UcButton btnStop;
        private UserControls.UcGridView ucGridView1;
    }
}