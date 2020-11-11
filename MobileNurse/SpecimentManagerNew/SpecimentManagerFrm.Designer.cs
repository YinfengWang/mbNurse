namespace HISPlus
{
    partial class SpecimentManagerFrm
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.chk_ys = new DevExpress.XtraEditors.CheckEdit();
            this.chk_yc = new DevExpress.XtraEditors.CheckEdit();
            this.chk_wc = new DevExpress.XtraEditors.CheckEdit();
            this.chk_qk = new DevExpress.XtraEditors.CheckEdit();
            this.gControlSpecimentData = new DevExpress.XtraGrid.GridControl();
            this.gViewSpecimentData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_ys.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_yc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_wc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_qk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gControlSpecimentData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gViewSpecimentData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.chk_ys);
            this.panelControl1.Controls.Add(this.chk_yc);
            this.panelControl1.Controls.Add(this.chk_wc);
            this.panelControl1.Controls.Add(this.chk_qk);
            this.panelControl1.Location = new System.Drawing.Point(2, 1);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(883, 44);
            this.panelControl1.TabIndex = 0;
            // 
            // chk_ys
            // 
            this.chk_ys.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_ys.EditValue = true;
            this.chk_ys.Location = new System.Drawing.Point(800, 12);
            this.chk_ys.Name = "chk_ys";
            this.chk_ys.Properties.Caption = "已送";
            this.chk_ys.Size = new System.Drawing.Size(54, 19);
            this.chk_ys.TabIndex = 1;
            // 
            // chk_yc
            // 
            this.chk_yc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_yc.EditValue = true;
            this.chk_yc.Location = new System.Drawing.Point(740, 12);
            this.chk_yc.Name = "chk_yc";
            this.chk_yc.Properties.Caption = "已采";
            this.chk_yc.Size = new System.Drawing.Size(54, 19);
            this.chk_yc.TabIndex = 1;
            // 
            // chk_wc
            // 
            this.chk_wc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_wc.EditValue = true;
            this.chk_wc.Location = new System.Drawing.Point(680, 12);
            this.chk_wc.Name = "chk_wc";
            this.chk_wc.Properties.Caption = "未采";
            this.chk_wc.Size = new System.Drawing.Size(54, 19);
            this.chk_wc.TabIndex = 1;
            // 
            // chk_qk
            // 
            this.chk_qk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.chk_qk.Location = new System.Drawing.Point(11, 12);
            this.chk_qk.Name = "chk_qk";
            this.chk_qk.Properties.Caption = "全科";
            this.chk_qk.Size = new System.Drawing.Size(53, 19);
            this.chk_qk.TabIndex = 0;
            // 
            // gControlSpecimentData
            // 
            this.gControlSpecimentData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gControlSpecimentData.Location = new System.Drawing.Point(2, 2);
            this.gControlSpecimentData.MainView = this.gViewSpecimentData;
            this.gControlSpecimentData.Name = "gControlSpecimentData";
            this.gControlSpecimentData.Size = new System.Drawing.Size(879, 386);
            this.gControlSpecimentData.TabIndex = 0;
            this.gControlSpecimentData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gViewSpecimentData});
            // 
            // gViewSpecimentData
            // 
            this.gViewSpecimentData.GridControl = this.gControlSpecimentData;
            this.gViewSpecimentData.Name = "gViewSpecimentData";
            this.gViewSpecimentData.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gViewSpecimentData_CustomDrawRowIndicator);
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.gControlSpecimentData);
            this.panelControl2.Location = new System.Drawing.Point(2, 51);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(883, 390);
            this.panelControl2.TabIndex = 2;
            // 
            // SpecimentManagerFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 462);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "SpecimentManagerFrm";
            this.ShowIcon = false;
            this.Text = "标本管理";
            this.Load += new System.EventHandler(this.SpecimentManagerFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chk_ys.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_yc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_wc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_qk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gControlSpecimentData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gViewSpecimentData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckEdit chk_qk;
        private DevExpress.XtraEditors.CheckEdit chk_ys;
        private DevExpress.XtraEditors.CheckEdit chk_yc;
        private DevExpress.XtraEditors.CheckEdit chk_wc;
        private DevExpress.XtraGrid.GridControl gControlSpecimentData;
        private DevExpress.XtraGrid.Views.Grid.GridView gViewSpecimentData;
        private DevExpress.XtraEditors.PanelControl panelControl2;
    }
}