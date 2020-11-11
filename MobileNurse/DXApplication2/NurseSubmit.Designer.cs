namespace DXApplication2
{
    partial class NurseSubmit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NurseSubmit));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.ucButton1 = new HISPlus.UserControls.UcButton();
            this.btnRefresh = new HISPlus.UserControls.UcButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(12, 69);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(721, 428);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // ucButton1
            // 
            this.ucButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ucButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ucButton1.Image = null;
            this.ucButton1.ImageRight = false;
            this.ucButton1.ImageStyle = HISPlus.UserControls.ImageStyle.None;
            this.ucButton1.Location = new System.Drawing.Point(130, 22);
            this.ucButton1.MaximumSize = new System.Drawing.Size(90, 30);
            this.ucButton1.MinimumSize = new System.Drawing.Size(60, 30);
            this.ucButton1.Name = "ucButton1";
            this.ucButton1.Size = new System.Drawing.Size(90, 30);
            this.ucButton1.TabIndex = 1;
            this.ucButton1.TextValue = "测试";
            this.ucButton1.UseVisualStyleBackColor = false;
            this.ucButton1.Click += new System.EventHandler(this.ucButton1_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Image = null;
            this.btnRefresh.ImageRight = false;
            this.btnRefresh.ImageStyle = HISPlus.UserControls.ImageStyle.None;
            this.btnRefresh.Location = new System.Drawing.Point(266, 22);
            this.btnRefresh.MaximumSize = new System.Drawing.Size(90, 30);
            this.btnRefresh.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 30);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.TextValue = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // NurseSubmit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 509);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.ucButton1);
            this.Controls.Add(this.webBrowser1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NurseSubmit";
            this.Text = "XtraForm1";
            this.Load += new System.EventHandler(this.XtraForm1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private HISPlus.UserControls.UcButton ucButton1;
        private HISPlus.UserControls.UcButton btnRefresh;
        private System.Windows.Forms.Timer timer1;
    }
}