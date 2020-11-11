namespace HISPlus
{
    partial class OptionSelectTreeFrm
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
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.trvItems = new System.Windows.Forms.TreeView();
            this.dtInputTime = new System.Windows.Forms.DateTimePicker();
            this.txtValue = new HISPlus.UserControls.UcTextArea();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dtInputDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new DevExpress.XtraEditors.PanelControl();
            this.btnOk = new HISPlus.UserControls.UcButton();
            this.btnCancel = new HISPlus.UserControls.UcButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(282, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(34, 39);
            this.btnSave.TabIndex = 41;
            this.btnSave.TextValue = "确定";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // trvItems
            // 
            this.trvItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trvItems.CheckBoxes = true;
            this.trvItems.Location = new System.Drawing.Point(12, 54);
            this.trvItems.Name = "trvItems";
            this.trvItems.Size = new System.Drawing.Size(302, 232);
            this.trvItems.TabIndex = 42;
            this.trvItems.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvItems_AfterCheck);
            this.trvItems.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvItems_AfterSelect);
            this.trvItems.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvItems_BeforeCheck);
            // 
            // dtInputTime
            // 
            this.dtInputTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtInputTime.Location = new System.Drawing.Point(128, 18);
            this.dtInputTime.Name = "dtInputTime";
            this.dtInputTime.ShowUpDown = true;
            this.dtInputTime.Size = new System.Drawing.Size(86, 21);
            this.dtInputTime.TabIndex = 43;
            this.dtInputTime.ValueChanged += new System.EventHandler(this.txtValue_TextChanged);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(12, 9);
            this.txtValue.Multiline = true;
            this.txtValue.Name = "txtValue";
            this.txtValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtValue.Size = new System.Drawing.Size(264, 39);
            this.txtValue.TabIndex = 40;
            this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dtInputDate
            // 
            this.dtInputDate.Location = new System.Drawing.Point(12, 18);
            this.dtInputDate.Name = "dtInputDate";
            this.dtInputDate.Size = new System.Drawing.Size(116, 21);
            this.dtInputDate.TabIndex = 44;
            this.dtInputDate.ValueChanged += new System.EventHandler(this.txtValue_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnOk);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Location = new System.Drawing.Point(12, 292);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 57);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(107, 17);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 30);
            this.btnOk.TabIndex = 4;
            this.btnOk.TextValue = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(203, 17);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.TextValue = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // OptionSelectTreeFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 358);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtInputDate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.trvItems);
            this.Controls.Add(this.dtInputTime);
            this.Controls.Add(this.txtValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "OptionSelectTreeFrm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "项目选择";
            this.Load += new System.EventHandler(this.OptionSelectFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HISPlus.UserControls.UcButton btnSave;
        private System.Windows.Forms.TreeView trvItems;
        private System.Windows.Forms.DateTimePicker dtInputTime;
        private HISPlus.UserControls.UcTextArea txtValue;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DateTimePicker dtInputDate;
        private DevExpress.XtraEditors.PanelControl groupBox1;
        private HISPlus.UserControls.UcButton btnOk;
        private HISPlus.UserControls.UcButton btnCancel;

    }
}