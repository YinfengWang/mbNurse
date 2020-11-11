namespace HISPlus
{
    partial class PatientFilterFrm
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
            this.lvwFilter = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnSetVal = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvwFilter
            // 
            this.lvwFilter.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lvwFilter.CheckBoxes = true;
            this.lvwFilter.Columns.Add(this.columnHeader2);
            this.lvwFilter.Columns.Add(this.columnHeader3);
            this.lvwFilter.FullRowSelect = true;
            this.lvwFilter.Location = new System.Drawing.Point(3, 3);
            this.lvwFilter.Name = "lvwFilter";
            this.lvwFilter.Size = new System.Drawing.Size(234, 235);
            this.lvwFilter.TabIndex = 0;
            this.lvwFilter.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "条件";
            this.columnHeader2.Width = 130;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "值";
            this.columnHeader3.Width = 100;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(165, 244);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(72, 20);
            this.btnExit.TabIndex = 14;
            this.btnExit.Text = "取消";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(6, 244);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 20);
            this.btnOk.TabIndex = 13;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnSetVal
            // 
            this.btnSetVal.Location = new System.Drawing.Point(84, 244);
            this.btnSetVal.Name = "btnSetVal";
            this.btnSetVal.Size = new System.Drawing.Size(72, 20);
            this.btnSetVal.TabIndex = 15;
            this.btnSetVal.Text = "设置";
            this.btnSetVal.Click += new System.EventHandler(this.btnSetVal_Click);
            // 
            // PatientFilterFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.btnSetVal);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lvwFilter);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "PatientFilterFrm";
            this.Text = "病人过滤";
            this.Load += new System.EventHandler(this.PatientFilterFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwFilter;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnSetVal;
    }
}