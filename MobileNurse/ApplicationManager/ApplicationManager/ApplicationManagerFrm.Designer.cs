namespace HISPlus
{
    partial class ApplicationManagerFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationManagerFrm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grpApplication = new DevExpress.XtraEditors.PanelControl();
            this.ucGridView1 = new HISPlus.UserControls.UcGridView();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabControl_WindowList = new System.Windows.Forms.TabControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpApplication)).BeginInit();
            this.grpApplication.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grpApplication);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.xtraTabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl_WindowList);
            this.splitContainer1.Size = new System.Drawing.Size(1124, 812);
            this.splitContainer1.SplitterDistance = 245;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 5;
            // 
            // grpApplication
            // 
            this.grpApplication.Controls.Add(this.ucGridView1);
            this.grpApplication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpApplication.Location = new System.Drawing.Point(0, 0);
            this.grpApplication.Margin = new System.Windows.Forms.Padding(4);
            this.grpApplication.Name = "grpApplication";
            this.grpApplication.Size = new System.Drawing.Size(245, 812);
            this.grpApplication.TabIndex = 1;
            // 
            // ucGridView1
            // 
            this.ucGridView1.AllowDeleteRows = false;
            this.ucGridView1.AllowEdit = false;
            this.ucGridView1.AllowSort = false;
            this.ucGridView1.DataSource = null;
            this.ucGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGridView1.Location = new System.Drawing.Point(2, 2);
            this.ucGridView1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);            
            this.ucGridView1.Name = "ucGridView1";
            this.ucGridView1.ShowRowIndicator = false;
            this.ucGridView1.Size = new System.Drawing.Size(241, 808);
            this.ucGridView1.TabIndex = 11;
            this.ucGridView1.SelectionChanged += new System.EventHandler(this.ucGridView1_SelectionChanged);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.Size = new System.Drawing.Size(874, 812);
            this.xtraTabControl1.TabIndex = 3;
            // 
            // tabControl_WindowList
            // 
            this.tabControl_WindowList.Location = new System.Drawing.Point(49, 371);
            this.tabControl_WindowList.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl_WindowList.Name = "tabControl_WindowList";
            this.tabControl_WindowList.SelectedIndex = 0;
            this.tabControl_WindowList.Size = new System.Drawing.Size(825, 441);
            this.tabControl_WindowList.TabIndex = 2;
            // 
            // ApplicationManagerFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 812);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ApplicationManagerFrm";
            this.Text = "应用程序管理";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ApplicationManagerFrm_FormClosed);
            this.Load += new System.EventHandler(this.ApplicationManagerFrm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpApplication)).EndInit();
            this.grpApplication.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.PanelControl grpApplication;
        private System.Windows.Forms.TabControl tabControl_WindowList;
        private UserControls.UcGridView ucGridView1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
    }
}