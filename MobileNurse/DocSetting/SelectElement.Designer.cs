namespace HISPlus
{
    partial class SelectElement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectElement));
            this.ucTreeList1 = new HISPlus.UserControls.UcTreeList();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new HISPlus.UserControls.UcButton();
            this.btnSure = new HISPlus.UserControls.UcButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucTreeList1
            // 
            this.ucTreeList1.AllowDrag = false;
            this.ucTreeList1.DataSource = null;
            this.ucTreeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTreeList1.DragFlag = false;
            this.ucTreeList1.EnabledColumnName = "Enabled";
            this.ucTreeList1.ImageColumnName = null;
            this.ucTreeList1.ImageList = null;
            this.ucTreeList1.KeyFieldName = "ID";
            this.ucTreeList1.LevelLimit = 0;
            this.ucTreeList1.Location = new System.Drawing.Point(0, 0);
            this.ucTreeList1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucTreeList1.MultiSelect = false;
            this.ucTreeList1.Name = "ucTreeList1";
            this.ucTreeList1.ParentFieldName = "ParentID";
            this.ucTreeList1.RootValue = 0;
            this.ucTreeList1.SelectedNode = null;
            this.ucTreeList1.ShowCheckBoxes = false;
            this.ucTreeList1.ShowHeader = true;
            this.ucTreeList1.Size = new System.Drawing.Size(559, 545);
            this.ucTreeList1.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnCancel);
            this.panelControl1.Controls.Add(this.btnSure);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 485);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(559, 60);
            this.panelControl1.TabIndex = 7;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageRight = false;
            this.btnCancel.ImageStyle = HISPlus.UserControls.ImageStyle.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(456, 14);
            this.btnCancel.MaximumSize = new System.Drawing.Size(80, 30);
            this.btnCancel.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.TextValue = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSure
            // 
            this.btnSure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSure.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSure.Image = ((System.Drawing.Image)(resources.GetObject("btnSure.Image")));
            this.btnSure.ImageRight = false;
            this.btnSure.ImageStyle = HISPlus.UserControls.ImageStyle.Apply;
            this.btnSure.Location = new System.Drawing.Point(354, 14);
            this.btnSure.MaximumSize = new System.Drawing.Size(80, 30);
            this.btnSure.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(80, 30);
            this.btnSure.TabIndex = 2;
            this.btnSure.TextValue = "确定";
            this.btnSure.UseVisualStyleBackColor = false;
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // SelectElement
            // 
            this.AcceptButton = this.btnSure;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(559, 545);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ucTreeList1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectElement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择模板元素";
            this.Load += new System.EventHandler(this.SelectElement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HISPlus.UserControls.UcTreeList ucTreeList1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private UserControls.UcButton btnCancel;
        private UserControls.UcButton btnSure;
    }
}