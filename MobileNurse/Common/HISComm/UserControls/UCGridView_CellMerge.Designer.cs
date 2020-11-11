namespace HISPlus.UserControls
{
    partial class UCGridView_CellMerge
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
            this.gcDefault = new DevExpress.XtraGrid.GridControl();
            this.gvDefault = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gcDefault)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDefault)).BeginInit();
            this.SuspendLayout();
            // 
            // gcDefault
            // 
            this.gcDefault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDefault.Location = new System.Drawing.Point(0, 0);
            this.gcDefault.MainView = this.gvDefault;
            this.gcDefault.Name = "gcDefault";
            this.gcDefault.Size = new System.Drawing.Size(500, 300);
            this.gcDefault.TabIndex = 1;
            this.gcDefault.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDefault});
            // 
            // gvDefault
            // 
            this.gvDefault.GridControl = this.gcDefault;
            this.gvDefault.Name = "gvDefault";
            this.gvDefault.OptionsView.ShowGroupPanel = false;
            this.gvDefault.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvDefault_CustomDrawCell);
            this.gvDefault.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.gvDefault_CustomDrawGroupRow);
            this.gvDefault.CustomDrawEmptyForeground += new DevExpress.XtraGrid.Views.Base.CustomDrawEventHandler(this.gvDefault_CustomDrawEmptyForeground);
            this.gvDefault.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvDefault_MouseDown);
            // 
            // UcGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcDefault);
            this.Name = "UcGridView";
            this.Size = new System.Drawing.Size(500, 300);
            this.Load += new System.EventHandler(this.UCGridView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcDefault)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDefault)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcDefault;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDefault;

    }
}
