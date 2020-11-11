namespace HISPlus
{
    partial class HealthEduDoFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.mnuCancel = new System.Windows.Forms.MenuItem();
            this.mnuOk = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbEduObject = new System.Windows.Forms.ComboBox();
            this.cmbPrecondition = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbEduMethod = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbMasterDegree = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbEduClog = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuCancel);
            this.mainMenu1.MenuItems.Add(this.mnuOk);
            // 
            // mnuCancel
            // 
            this.mnuCancel.Text = "返回";
            this.mnuCancel.Click += new System.EventHandler(this.mnuCancel_Click);
            // 
            // mnuOk
            // 
            this.mnuOk.Text = "确定";
            this.mnuOk.Click += new System.EventHandler(this.mnuOk_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.Text = "教育对象";
            // 
            // cmbEduObject
            // 
            this.cmbEduObject.Items.Add("病人");
            this.cmbEduObject.Items.Add("家属");
            this.cmbEduObject.Location = new System.Drawing.Point(64, 6);
            this.cmbEduObject.Name = "cmbEduObject";
            this.cmbEduObject.Size = new System.Drawing.Size(173, 22);
            this.cmbEduObject.TabIndex = 1;
            this.cmbEduObject.TextChanged += new System.EventHandler(this.content_TextChanged);
            // 
            // cmbPrecondition
            // 
            this.cmbPrecondition.Location = new System.Drawing.Point(64, 34);
            this.cmbPrecondition.Name = "cmbPrecondition";
            this.cmbPrecondition.Size = new System.Drawing.Size(173, 22);
            this.cmbPrecondition.TabIndex = 4;
            this.cmbPrecondition.TextChanged += new System.EventHandler(this.content_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.Text = "准备情况";
            // 
            // cmbEduMethod
            // 
            this.cmbEduMethod.Location = new System.Drawing.Point(64, 62);
            this.cmbEduMethod.Name = "cmbEduMethod";
            this.cmbEduMethod.Size = new System.Drawing.Size(173, 22);
            this.cmbEduMethod.TabIndex = 7;
            this.cmbEduMethod.TextChanged += new System.EventHandler(this.content_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(5, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.Text = "教育方法";
            // 
            // cmbMasterDegree
            // 
            this.cmbMasterDegree.Location = new System.Drawing.Point(64, 90);
            this.cmbMasterDegree.Name = "cmbMasterDegree";
            this.cmbMasterDegree.Size = new System.Drawing.Size(173, 22);
            this.cmbMasterDegree.TabIndex = 10;
            this.cmbMasterDegree.TextChanged += new System.EventHandler(this.content_TextChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(5, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 20);
            this.label4.Text = "掌握情况";
            // 
            // cmbEduClog
            // 
            this.cmbEduClog.Location = new System.Drawing.Point(64, 118);
            this.cmbEduClog.Name = "cmbEduClog";
            this.cmbEduClog.Size = new System.Drawing.Size(173, 22);
            this.cmbEduClog.TabIndex = 13;
            this.cmbEduClog.TextChanged += new System.EventHandler(this.content_TextChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(5, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 20);
            this.label5.Text = "学习障碍";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(5, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 20);
            this.label6.Text = "备注";
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(64, 147);
            this.txtMemo.MaxLength = 122;
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMemo.Size = new System.Drawing.Size(173, 118);
            this.txtMemo.TabIndex = 17;
            this.txtMemo.TextChanged += new System.EventHandler(this.content_TextChanged);
            // 
            // HealthEduDoFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbEduClog);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbMasterDegree);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbEduMethod);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbPrecondition);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbEduObject);
            this.Controls.Add(this.label1);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "HealthEduDoFrm";
            this.Text = "健康教育";
            this.Load += new System.EventHandler(this.HealthEduDoFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuOk;
        private System.Windows.Forms.MenuItem mnuCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbEduObject;
        private System.Windows.Forms.ComboBox cmbPrecondition;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbEduMethod;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbMasterDegree;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbEduClog;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMemo;
    }
}