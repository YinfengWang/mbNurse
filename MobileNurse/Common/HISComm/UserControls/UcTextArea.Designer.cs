namespace HISPlus.UserControls
{
    partial class UcTextArea
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtControl = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtControl.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtControl
            // 
            this.txtControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtControl.Location = new System.Drawing.Point(0, 0);
            this.txtControl.Name = "txtControl";
            this.txtControl.Size = new System.Drawing.Size(172, 132);
            this.txtControl.TabIndex = 0;
            this.txtControl.UseOptimizedRendering = true;
            this.txtControl.EditValueChanged += new System.EventHandler(this.txtControl_EditValueChanged);
            // 
            // UcTextArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtControl);
            this.Name = "UcTextArea";
            this.Size = new System.Drawing.Size(172, 132);
            ((System.ComponentModel.ISupportInitialize)(this.txtControl.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion        

        private DevExpress.XtraEditors.MemoEdit txtControl;

    }
}
