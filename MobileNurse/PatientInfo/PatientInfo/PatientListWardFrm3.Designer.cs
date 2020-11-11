namespace HISPlus
{
    partial class PatientListWardFrm3
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.ucPatientList1 = new HISPlus.UCPatientList();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuRefresh});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 28);
            // 
            // cmnuRefresh
            // 
            this.cmnuRefresh.Name = "cmnuRefresh";
            this.cmnuRefresh.Size = new System.Drawing.Size(108, 24);
            this.cmnuRefresh.Text = "刷新";
            this.cmnuRefresh.Click += new System.EventHandler(this.cmnuRefresh_Click);
            // 
            // ucPatientList1
            // 
            this.ucPatientList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucPatientList1.DeptCode = "";
            this.ucPatientList1.Location = new System.Drawing.Point(0, 12);
            this.ucPatientList1.Name = "ucPatientList1";
            this.ucPatientList1.ShowOutHospital3Days = false;
            this.ucPatientList1.ShowWaitInpPatient = false;
            this.ucPatientList1.Size = new System.Drawing.Size(201, 516);
            this.ucPatientList1.TabIndex = 6;
            // 
            // PatientListWardFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(213, 659);
            this.Controls.Add(this.ucPatientList1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PatientListWardFrm";
            this.Text = "病区病人列表";
            this.Load += new System.EventHandler(this.PatientListWardFrm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmnuRefresh;
        private UCPatientList ucPatientList1;
    }
}