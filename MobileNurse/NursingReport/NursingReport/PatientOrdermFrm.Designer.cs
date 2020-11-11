namespace HISPlus
{
    partial class PatientOrdermFrm
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
            this.groupOrders = new DevExpress.XtraEditors.GroupControl();
            this.gControlOrdersm = new DevExpress.XtraGrid.GridControl();
            this.gViewOrdersm = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.PATIENT_ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ORDER_TEXT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DOSAGE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.check = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSure = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupOrders)).BeginInit();
            this.groupOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gControlOrdersm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gViewOrdersm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupOrders
            // 
            this.groupOrders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupOrders.Controls.Add(this.gControlOrdersm);
            this.groupOrders.Location = new System.Drawing.Point(1, 3);
            this.groupOrders.Name = "groupOrders";
            this.groupOrders.Size = new System.Drawing.Size(524, 254);
            this.groupOrders.TabIndex = 0;
            this.groupOrders.Text = "医嘱信息";
            // 
            // gControlOrdersm
            // 
            this.gControlOrdersm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gControlOrdersm.Location = new System.Drawing.Point(2, 22);
            this.gControlOrdersm.MainView = this.gViewOrdersm;
            this.gControlOrdersm.Name = "gControlOrdersm";
            this.gControlOrdersm.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gControlOrdersm.Size = new System.Drawing.Size(520, 230);
            this.gControlOrdersm.TabIndex = 0;
            this.gControlOrdersm.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gViewOrdersm});
            this.gControlOrdersm.DoubleClick += new System.EventHandler(this.gControlOrdersm_DoubleClick);
            // 
            // gViewOrdersm
            // 
            this.gViewOrdersm.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.PATIENT_ID,
            this.check,
            this.ORDER_TEXT,
            this.DOSAGE});
            this.gViewOrdersm.GridControl = this.gControlOrdersm;
            this.gViewOrdersm.Name = "gViewOrdersm";
            this.gViewOrdersm.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gViewOrdersm_CustomDrawRowIndicator);
            // 
            // PATIENT_ID
            // 
            this.PATIENT_ID.Caption = "病人ID";
            this.PATIENT_ID.FieldName = "PATIENT_ID";
            this.PATIENT_ID.Name = "PATIENT_ID";
            this.PATIENT_ID.Width = 80;
            // 
            // ORDER_TEXT
            // 
            this.ORDER_TEXT.Caption = "医嘱内容";
            this.ORDER_TEXT.FieldName = "ORDER_TEXT";
            this.ORDER_TEXT.Name = "ORDER_TEXT";
            this.ORDER_TEXT.Visible = true;
            this.ORDER_TEXT.VisibleIndex = 0;
            this.ORDER_TEXT.Width = 315;
            // 
            // DOSAGE
            // 
            this.DOSAGE.Caption = "剂量";
            this.DOSAGE.FieldName = "DOSAGE";
            this.DOSAGE.Name = "DOSAGE";
            this.DOSAGE.Visible = true;
            this.DOSAGE.VisibleIndex = 1;
            this.DOSAGE.Width = 107;
            // 
            // check
            // 
            this.check.Caption = "选择";
            this.check.ColumnEdit = this.repositoryItemCheckEdit1;
            this.check.FieldName = "check";
            this.check.Name = "check";
            this.check.Visible = true;
            this.check.VisibleIndex = 2;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Caption = "Check";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Controls.Add(this.btnSure);
            this.panelControl1.Location = new System.Drawing.Point(1, 260);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(524, 37);
            this.panelControl1.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(456, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSure
            // 
            this.btnSure.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSure.Location = new System.Drawing.Point(389, 7);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(60, 23);
            this.btnSure.TabIndex = 0;
            this.btnSure.Text = "确定";
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // PatientOrdermFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 299);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.groupOrders);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PatientOrdermFrm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "患者医嘱";
            this.Load += new System.EventHandler(this.PatientOrdermFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupOrders)).EndInit();
            this.groupOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gControlOrdersm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gViewOrdersm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupOrders;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gControlOrdersm;
        private DevExpress.XtraGrid.Views.Grid.GridView gViewOrdersm;
        private DevExpress.XtraEditors.SimpleButton btnSure;
        private DevExpress.XtraGrid.Columns.GridColumn PATIENT_ID;
        private DevExpress.XtraGrid.Columns.GridColumn ORDER_TEXT;
        private DevExpress.XtraGrid.Columns.GridColumn DOSAGE;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraGrid.Columns.GridColumn check;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
    }
}