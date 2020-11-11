namespace HISPlus
{
    partial class NurseRecordFrm
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
            this.mnuPatient = new System.Windows.Forms.MenuItem();
            this.txtNurseRecord = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNextTimePoint = new System.Windows.Forms.Button();
            this.btnCurrTimePoint = new System.Windows.Forms.Button();
            this.btnPreTimePoint = new System.Windows.Forms.Button();
            this.btnTemplate = new System.Windows.Forms.Button();
            this.btnListPatient = new System.Windows.Forms.Button();
            this.btnNextPatient = new System.Windows.Forms.Button();
            this.btnCurrPatient = new System.Windows.Forms.Button();
            this.btnPrePatient = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuCancel);
            this.mainMenu1.MenuItems.Add(this.mnuPatient);
            // 
            // mnuCancel
            // 
            this.mnuCancel.Text = "返回";
            // 
            // mnuPatient
            // 
            this.mnuPatient.Text = "当前病人";
            // 
            // txtNurseRecord
            // 
            this.txtNurseRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtNurseRecord.Location = new System.Drawing.Point(0, 0);
            this.txtNurseRecord.Multiline = true;
            this.txtNurseRecord.Name = "txtNurseRecord";
            this.txtNurseRecord.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNurseRecord.Size = new System.Drawing.Size(240, 219);
            this.txtNurseRecord.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(176, 218);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(64, 31);
            this.btnSave.TabIndex = 54;
            this.btnSave.Text = "保存";
            // 
            // btnNextTimePoint
            // 
            this.btnNextTimePoint.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnNextTimePoint.Location = new System.Drawing.Point(137, 218);
            this.btnNextTimePoint.Name = "btnNextTimePoint";
            this.btnNextTimePoint.Size = new System.Drawing.Size(40, 31);
            this.btnNextTimePoint.TabIndex = 53;
            this.btnNextTimePoint.Text = " >";
            // 
            // btnCurrTimePoint
            // 
            this.btnCurrTimePoint.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnCurrTimePoint.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCurrTimePoint.ForeColor = System.Drawing.Color.Blue;
            this.btnCurrTimePoint.Location = new System.Drawing.Point(84, 218);
            this.btnCurrTimePoint.Name = "btnCurrTimePoint";
            this.btnCurrTimePoint.Size = new System.Drawing.Size(54, 31);
            this.btnCurrTimePoint.TabIndex = 52;
            this.btnCurrTimePoint.Text = "20:38";
            // 
            // btnPreTimePoint
            // 
            this.btnPreTimePoint.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnPreTimePoint.Location = new System.Drawing.Point(45, 218);
            this.btnPreTimePoint.Name = "btnPreTimePoint";
            this.btnPreTimePoint.Size = new System.Drawing.Size(40, 31);
            this.btnPreTimePoint.TabIndex = 51;
            this.btnPreTimePoint.Text = " <";
            // 
            // btnTemplate
            // 
            this.btnTemplate.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnTemplate.Location = new System.Drawing.Point(0, 218);
            this.btnTemplate.Name = "btnTemplate";
            this.btnTemplate.Size = new System.Drawing.Size(46, 31);
            this.btnTemplate.TabIndex = 56;
            this.btnTemplate.Text = "模板";
            // 
            // btnListPatient
            // 
            this.btnListPatient.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnListPatient.Location = new System.Drawing.Point(0, 248);
            this.btnListPatient.Name = "btnListPatient";
            this.btnListPatient.Size = new System.Drawing.Size(46, 20);
            this.btnListPatient.TabIndex = 60;
            this.btnListPatient.Text = "列表";
            // 
            // btnNextPatient
            // 
            this.btnNextPatient.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnNextPatient.Location = new System.Drawing.Point(176, 248);
            this.btnNextPatient.Name = "btnNextPatient";
            this.btnNextPatient.Size = new System.Drawing.Size(64, 20);
            this.btnNextPatient.TabIndex = 59;
            // 
            // btnCurrPatient
            // 
            this.btnCurrPatient.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCurrPatient.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCurrPatient.ForeColor = System.Drawing.Color.White;
            this.btnCurrPatient.Location = new System.Drawing.Point(110, 248);
            this.btnCurrPatient.Name = "btnCurrPatient";
            this.btnCurrPatient.Size = new System.Drawing.Size(67, 20);
            this.btnCurrPatient.TabIndex = 58;
            // 
            // btnPrePatient
            // 
            this.btnPrePatient.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPrePatient.Location = new System.Drawing.Point(45, 248);
            this.btnPrePatient.Name = "btnPrePatient";
            this.btnPrePatient.Size = new System.Drawing.Size(66, 20);
            this.btnPrePatient.TabIndex = 57;
            // 
            // NurseRecordFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.btnListPatient);
            this.Controls.Add(this.btnNextPatient);
            this.Controls.Add(this.btnCurrPatient);
            this.Controls.Add(this.btnPrePatient);
            this.Controls.Add(this.btnTemplate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnNextTimePoint);
            this.Controls.Add(this.btnCurrTimePoint);
            this.Controls.Add(this.btnPreTimePoint);
            this.Controls.Add(this.txtNurseRecord);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "NurseRecordFrm";
            this.Text = "护理记录";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuCancel;
        private System.Windows.Forms.MenuItem mnuPatient;
        private System.Windows.Forms.TextBox txtNurseRecord;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNextTimePoint;
        private System.Windows.Forms.Button btnCurrTimePoint;
        private System.Windows.Forms.Button btnPreTimePoint;
        private System.Windows.Forms.Button btnTemplate;
        private System.Windows.Forms.Button btnListPatient;
        private System.Windows.Forms.Button btnNextPatient;
        private System.Windows.Forms.Button btnCurrPatient;
        private System.Windows.Forms.Button btnPrePatient;
    }
}