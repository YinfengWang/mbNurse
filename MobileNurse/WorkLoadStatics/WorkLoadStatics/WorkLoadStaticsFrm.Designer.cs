namespace HISPlus
{
    partial class WorkLoadStaticsFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkLoadStaticsFrm));
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.btnQuery = new HISPlus.UserControls.UcButton();
            this.btnPrint = new HISPlus.UserControls.UcButton();
            this.dtpBegin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.ucCmbChildType = new System.Windows.Forms.ComboBox();
            this.cmbWardCode = new System.Windows.Forms.ComboBox();
            this.ucCmbType = new System.Windows.Forms.ComboBox();
            this.lblChild = new DevExpress.XtraEditors.LabelControl();
            this.lblType = new DevExpress.XtraEditors.LabelControl();
            this.lblWardCode = new DevExpress.XtraEditors.LabelControl();
            this.gCtlStatice = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gCtlStatice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(610, 41);
            this.dtpEnd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(131, 22);
            this.dtpEnd.TabIndex = 4;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.ImageRight = false;
            this.btnQuery.ImageStyle = HISPlus.UserControls.ImageStyle.Find;
            this.btnQuery.Location = new System.Drawing.Point(752, 41);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnQuery.MaximumSize = new System.Drawing.Size(88, 23);
            this.btnQuery.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(72, 23);
            this.btnQuery.TabIndex = 5;
            this.btnQuery.TextValue = "查询(&Q)";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPrint.Enabled = false;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageRight = false;
            this.btnPrint.ImageStyle = HISPlus.UserControls.ImageStyle.Print;
            this.btnPrint.Location = new System.Drawing.Point(828, 41);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPrint.MaximumSize = new System.Drawing.Size(88, 23);
            this.btnPrint.MinimumSize = new System.Drawing.Size(52, 23);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(60, 23);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.TextValue = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dtpBegin
            // 
            this.dtpBegin.Location = new System.Drawing.Point(475, 41);
            this.dtpBegin.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(131, 22);
            this.dtpBegin.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(443, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "日期:";
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.ucCmbChildType);
            this.groupControl1.Controls.Add(this.cmbWardCode);
            this.groupControl1.Controls.Add(this.ucCmbType);
            this.groupControl1.Controls.Add(this.lblChild);
            this.groupControl1.Controls.Add(this.btnPrint);
            this.groupControl1.Controls.Add(this.dtpEnd);
            this.groupControl1.Controls.Add(this.lblType);
            this.groupControl1.Controls.Add(this.btnQuery);
            this.groupControl1.Controls.Add(this.lblWardCode);
            this.groupControl1.Controls.Add(this.dtpBegin);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Location = new System.Drawing.Point(6, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(964, 79);
            this.groupControl1.TabIndex = 21;
            this.groupControl1.Text = "检索条件";
            // 
            // ucCmbChildType
            // 
            this.ucCmbChildType.FormattingEnabled = true;
            this.ucCmbChildType.Location = new System.Drawing.Point(323, 41);
            this.ucCmbChildType.Name = "ucCmbChildType";
            this.ucCmbChildType.Size = new System.Drawing.Size(113, 22);
            this.ucCmbChildType.TabIndex = 23;
            // 
            // cmbWardCode
            // 
            this.cmbWardCode.DropDownHeight = 100;
            this.cmbWardCode.FormattingEnabled = true;
            this.cmbWardCode.IntegralHeight = false;
            this.cmbWardCode.Location = new System.Drawing.Point(41, 41);
            this.cmbWardCode.MaxDropDownItems = 50;
            this.cmbWardCode.Name = "cmbWardCode";
            this.cmbWardCode.Size = new System.Drawing.Size(121, 22);
            this.cmbWardCode.TabIndex = 22;
            // 
            // ucCmbType
            // 
            this.ucCmbType.FormattingEnabled = true;
            this.ucCmbType.Location = new System.Drawing.Point(204, 41);
            this.ucCmbType.Name = "ucCmbType";
            this.ucCmbType.Size = new System.Drawing.Size(80, 22);
            this.ucCmbType.TabIndex = 21;
            this.ucCmbType.SelectedIndexChanged += new System.EventHandler(this.ucCmbType_SelectedIndexChanged);
            // 
            // lblChild
            // 
            this.lblChild.Location = new System.Drawing.Point(289, 44);
            this.lblChild.Name = "lblChild";
            this.lblChild.Size = new System.Drawing.Size(28, 14);
            this.lblChild.TabIndex = 0;
            this.lblChild.Text = "子类:";
            // 
            // lblType
            // 
            this.lblType.Location = new System.Drawing.Point(170, 43);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(28, 14);
            this.lblType.TabIndex = 0;
            this.lblType.Text = "分类:";
            // 
            // lblWardCode
            // 
            this.lblWardCode.Location = new System.Drawing.Point(7, 43);
            this.lblWardCode.Name = "lblWardCode";
            this.lblWardCode.Size = new System.Drawing.Size(28, 14);
            this.lblWardCode.TabIndex = 0;
            this.lblWardCode.Text = "病区:";
            // 
            // gCtlStatice
            // 
            this.gCtlStatice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gCtlStatice.Location = new System.Drawing.Point(6, 97);
            this.gCtlStatice.MainView = this.gridView1;
            this.gCtlStatice.Name = "gCtlStatice";
            this.gCtlStatice.Size = new System.Drawing.Size(964, 335);
            this.gCtlStatice.TabIndex = 0;
            this.gCtlStatice.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gCtlStatice;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            // 
            // WorkLoadStaticsFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 435);
            this.Controls.Add(this.gCtlStatice);
            this.Controls.Add(this.groupControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "WorkLoadStaticsFrm";
            this.Text = "工作量统计";
            this.Load += new System.EventHandler(this.WorkLoadStaticsFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gCtlStatice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpEnd;
        private HISPlus.UserControls.UcButton btnQuery;
        private HISPlus.UserControls.UcButton btnPrint;
        private System.Windows.Forms.DateTimePicker dtpBegin;
        private DevExpress.XtraEditors.LabelControl label1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl lblType;
        private DevExpress.XtraEditors.LabelControl lblWardCode;
        private DevExpress.XtraEditors.LabelControl lblChild;
        private DevExpress.XtraGrid.GridControl gCtlStatice;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ComboBox ucCmbType;
        private System.Windows.Forms.ComboBox cmbWardCode;
        private System.Windows.Forms.ComboBox ucCmbChildType;
    }
}