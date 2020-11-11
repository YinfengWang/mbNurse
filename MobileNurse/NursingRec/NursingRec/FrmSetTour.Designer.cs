namespace HISPlus
{
    partial class FrmSetTour
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetTour));
            this.dataGridVCou = new System.Windows.Forms.DataGridView();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.btnDelete = new HISPlus.UserControls.UcButton();
            this.button1 = new HISPlus.UserControls.UcButton();
            this.bntServe = new HISPlus.UserControls.UcButton();
            this.txtCount = new HISPlus.UserControls.UcTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVCou)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridVCou
            // 
            this.dataGridVCou.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridVCou.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridVCou.Location = new System.Drawing.Point(11, 10);
            this.dataGridVCou.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridVCou.MultiSelect = false;
            this.dataGridVCou.Name = "dataGridVCou";
            this.dataGridVCou.ReadOnly = true;
            this.dataGridVCou.RowHeadersVisible = false;
            this.dataGridVCou.RowTemplate.Height = 23;
            this.dataGridVCou.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridVCou.Size = new System.Drawing.Size(368, 220);
            this.dataGridVCou.TabIndex = 5;
            this.dataGridVCou.SelectionChanged += new System.EventHandler(this.dataGridVCou_SelectionChanged);
            // 
            // label2
            // 
            this.label2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(11, 311);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "输入字数不能大于10个！";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 236);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "添加巡视内容：";
            // 
            // btnDelete
            // 
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageRight = false;
            this.btnDelete.ImageStyle = HISPlus.UserControls.ImageStyle.Delete;
            this.btnDelete.Location = new System.Drawing.Point(249, 306);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDelete.MaximumSize = new System.Drawing.Size(79, 23);
            this.btnDelete.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(62, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.TextValue = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageRight = false;
            this.button1.ImageStyle = HISPlus.UserControls.ImageStyle.Close;
            this.button1.Location = new System.Drawing.Point(318, 306);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.button1.MaximumSize = new System.Drawing.Size(70, 23);
            this.button1.MinimumSize = new System.Drawing.Size(70, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 23);
            this.button1.TabIndex = 3;
            this.button1.TextValue = "退出";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bntServe
            // 
            this.bntServe.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bntServe.Enabled = false;
            this.bntServe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntServe.Image = ((System.Drawing.Image)(resources.GetObject("bntServe.Image")));
            this.bntServe.ImageRight = false;
            this.bntServe.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.bntServe.Location = new System.Drawing.Point(171, 306);
            this.bntServe.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.bntServe.MaximumSize = new System.Drawing.Size(70, 23);
            this.bntServe.MinimumSize = new System.Drawing.Size(70, 23);
            this.bntServe.Name = "bntServe";
            this.bntServe.Size = new System.Drawing.Size(70, 23);
            this.bntServe.TabIndex = 2;
            this.bntServe.TextValue = "保存";
            this.bntServe.UseVisualStyleBackColor = true;
            this.bntServe.Click += new System.EventHandler(this.bntServe_Click);
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(110, 236);
            this.txtCount.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtCount.MaxLength = 0;
            this.txtCount.Multiline = true;
            this.txtCount.Name = "txtCount";
            this.txtCount.PasswordChar = '\0';
            this.txtCount.ReadOnly = false;
            this.txtCount.Size = new System.Drawing.Size(269, 54);
            this.txtCount.TabIndex = 1;
            this.txtCount.TextChanged += new System.EventHandler(this.txtCount_TextChanged);
            // 
            // FrmSetTour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 340);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dataGridVCou);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bntServe);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSetTour";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "巡视内容设置";
            this.Load += new System.EventHandler(this.FrmSetTour_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridVCou)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl label1;
        private HISPlus.UserControls.UcTextBox txtCount;
        private HISPlus.UserControls.UcButton bntServe;
        private HISPlus.UserControls.UcButton button1;
        private DevExpress.XtraEditors.LabelControl label2;
        private System.Windows.Forms.DataGridView dataGridVCou;
        private UserControls.UcButton btnDelete;
    }
}