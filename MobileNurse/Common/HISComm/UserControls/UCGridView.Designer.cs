using DevExpress.XtraGrid.Views.Grid;

namespace HISPlus.UserControls
{
    partial class UcGridView
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
            this.components = new System.ComponentModel.Container();
            this.gcDefault = new DevExpress.XtraGrid.GridControl();
            this.gvDefault = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gcDefault)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDefault)).BeginInit();
            this.SuspendLayout();
            // 
            // gcDefault
            // 
            this.gcDefault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDefault.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gcDefault.Location = new System.Drawing.Point(0, 0);
            this.gcDefault.MainView = this.gvDefault;
            this.gcDefault.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gcDefault.Name = "gcDefault";
            this.gcDefault.Size = new System.Drawing.Size(375, 240);
            this.gcDefault.TabIndex = 1;
            this.gcDefault.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDefault});
            this.gcDefault.DataSourceChanged += new System.EventHandler(this.gcDefault_DataSourceChanged);
            // 
            // gvDefault
            // 
            this.gvDefault.GridControl = this.gcDefault;
            this.gvDefault.Name = "gvDefault";
            this.gvDefault.OptionsView.RowAutoHeight = true;
            this.gvDefault.OptionsView.ShowGroupPanel = false;
            this.gvDefault.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvDefault_RowCellClick);
            this.gvDefault.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvDefault_CustomDrawCell);
            this.gvDefault.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.gvDefault_CustomDrawGroupRow);
            this.gvDefault.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvDefault_RowStyle);
            this.gvDefault.CustomDrawEmptyForeground += new DevExpress.XtraGrid.Views.Base.CustomDrawEventHandler(this.gvDefault_CustomDrawEmptyForeground);
            this.gvDefault.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvDefault_MouseDown);
            // 
            // UcGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcDefault);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UcGridView";
            this.Size = new System.Drawing.Size(375, 240);
            this.Load += new System.EventHandler(this.UCGridView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcDefault)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDefault)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraGrid.GridControl gcDefault;
        protected GridView gvDefault;
        private DevExpress.Utils.ToolTipController toolTipController1;

    }
}
