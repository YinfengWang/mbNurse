namespace HISPlus.FORMS
{
    partial class SelectTime
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
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.DataStartTime = new System.Windows.Forms.DateTimePicker();
            this.DataStopTime = new System.Windows.Forms.DateTimePicker();
            this.butSure = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "返回";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(21, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 29);
            this.label1.Text = "请选择合适的时间段";
            // 
            // DataStartTime
            // 
            this.DataStartTime.CalendarFont = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.DataStartTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.DataStartTime.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.DataStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DataStartTime.Location = new System.Drawing.Point(21, 79);
            this.DataStartTime.Name = "DataStartTime";
            this.DataStartTime.Size = new System.Drawing.Size(199, 30);
            this.DataStartTime.TabIndex = 6;
            // 
            // DataStopTime
            // 
            this.DataStopTime.CalendarFont = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.DataStopTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.DataStopTime.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.DataStopTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DataStopTime.Location = new System.Drawing.Point(21, 148);
            this.DataStopTime.Name = "DataStopTime";
            this.DataStopTime.Size = new System.Drawing.Size(199, 30);
            this.DataStopTime.TabIndex = 7;
            // 
            // butSure
            // 
            this.butSure.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.butSure.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.butSure.Location = new System.Drawing.Point(21, 199);
            this.butSure.Name = "butSure";
            this.butSure.Size = new System.Drawing.Size(71, 35);
            this.butSure.TabIndex = 8;
            this.butSure.Text = "确定";
            this.butSure.Click += new System.EventHandler(this.butSure_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnCancle.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.btnCancle.Location = new System.Drawing.Point(149, 199);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(71, 35);
            this.btnCancle.TabIndex = 10;
            this.btnCancle.Text = "取消";
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(21, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 20);
            this.label2.Text = "开始时间:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(21, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.Text = "结束时间:";
            // 
            // SelectTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.butSure);
            this.Controls.Add(this.DataStopTime);
            this.Controls.Add(this.DataStartTime);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.Menu = this.mainMenu1;
            this.Name = "SelectTime";
            this.Text = "选择时间段";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DataStartTime;
        private System.Windows.Forms.DateTimePicker DataStopTime;
        private System.Windows.Forms.Button butSure;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}