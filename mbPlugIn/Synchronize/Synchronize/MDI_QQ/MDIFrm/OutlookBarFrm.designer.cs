namespace HISPlus
{
    partial class OutlookBarFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutlookBarFrm));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.xpPanelGroup1 = new BSE.Windows.Forms.XPanderPanelList();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "16Info.ico");
            this.imageList1.Images.SetKeyName(1, "eye.ICO");
            // 
            // xpPanelGroup1
            // 
            this.xpPanelGroup1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(168)))), ((int)(((byte)(231)))));
            this.xpPanelGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xpPanelGroup1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.xpPanelGroup1.ForeColor = System.Drawing.Color.Blue;
            this.xpPanelGroup1.GradientBackground = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(168)))), ((int)(((byte)(231)))));
            this.xpPanelGroup1.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.xpPanelGroup1.Location = new System.Drawing.Point(0, 0);
            this.xpPanelGroup1.Name = "xpPanelGroup1";
            this.xpPanelGroup1.Padding = new System.Windows.Forms.Padding(3);
            this.xpPanelGroup1.PanelStyle = BSE.Windows.Forms.PanelStyle.Aqua;
            this.xpPanelGroup1.Size = new System.Drawing.Size(181, 423);
            this.xpPanelGroup1.TabIndex = 0;
            this.xpPanelGroup1.Text = "xPanderPanelList1";
            // 
            // OutlookBarFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(181, 423);
            this.ControlBox = false;
            this.Controls.Add(this.xpPanelGroup1);
            this.Name = "OutlookBarFrm";
            this.Text = "OutlookBarFrm";
            this.Load += new System.EventHandler(this.OutlookBarFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private BSE.Windows.Forms.XPanderPanelList xpPanelGroup1;
    }
}