using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace HISPlus
{
    partial class NurseScheduleDayFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NurseScheduleDayFrm));
            this.groupBox1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClear = new HISPlus.UserControls.UcButton();
            this.btnPrint = new HISPlus.UserControls.UcButton();
            this.btnSave = new HISPlus.UserControls.UcButton();
            this.btnCopy = new HISPlus.UserControls.UcButton();
            this.btnNextWeek = new HISPlus.UserControls.UcButton();
            this.btnPreWeek = new HISPlus.UserControls.UcButton();
            this.dtPicker = new System.Windows.Forms.DateTimePicker();
            this.label3 = new DevExpress.XtraEditors.LabelControl();
            this.cmbDept = new HISPlus.UserControls.UcComboBox();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.grpSchedule = new DevExpress.XtraEditors.PanelControl();
            this.ucGridView1 = new HISPlus.UserControls.UcGridView();
            this.lblMemo = new DevExpress.XtraEditors.LabelControl();
            this.lblWeekDay0 = new DevExpress.XtraEditors.LabelControl();
            this.lblDate0 = new DevExpress.XtraEditors.LabelControl();
            this.label5 = new DevExpress.XtraEditors.LabelControl();
            this.label4 = new DevExpress.XtraEditors.LabelControl();
            this.lblTopLeft = new DevExpress.XtraEditors.LabelControl();
            this.cmnuNurseDuty = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpSchedule)).BeginInit();
            this.grpSchedule.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnCopy);
            this.groupBox1.Controls.Add(this.btnNextWeek);
            this.groupBox1.Controls.Add(this.btnPreWeek);
            this.groupBox1.Controls.Add(this.dtPicker);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbDept);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1139, 69);
            this.groupBox1.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.Enabled = false;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageRight = false;
            this.btnClear.ImageStyle = HISPlus.UserControls.ImageStyle.Clear;
            this.btnClear.Location = new System.Drawing.Point(951, 19);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnClear.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 30);
            this.btnClear.TabIndex = 9;
            this.btnClear.TextValue = "清空(&C)";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPrint.Enabled = false;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageRight = false;
            this.btnPrint.ImageStyle = HISPlus.UserControls.ImageStyle.Print;
            this.btnPrint.Location = new System.Drawing.Point(866, 19);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnPrint.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(80, 30);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.TextValue = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageRight = false;
            this.btnSave.ImageStyle = HISPlus.UserControls.ImageStyle.Save;
            this.btnSave.Location = new System.Drawing.Point(781, 19);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnSave.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 7;
            this.btnSave.TextValue = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageRight = false;
            this.btnCopy.ImageStyle = HISPlus.UserControls.ImageStyle.Copy;
            this.btnCopy.Location = new System.Drawing.Point(628, 19);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(4);
            this.btnCopy.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnCopy.MinimumSize = new System.Drawing.Size(120, 30);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(120, 30);
            this.btnCopy.TabIndex = 6;
            this.btnCopy.TextValue = "复制上周数据";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnNextWeek
            // 
            this.btnNextWeek.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNextWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextWeek.Image = ((System.Drawing.Image)(resources.GetObject("btnNextWeek.Image")));
            this.btnNextWeek.ImageRight = true;
            this.btnNextWeek.ImageStyle = HISPlus.UserControls.ImageStyle.Forward;
            this.btnNextWeek.Location = new System.Drawing.Point(524, 19);
            this.btnNextWeek.Margin = new System.Windows.Forms.Padding(4);
            this.btnNextWeek.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnNextWeek.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnNextWeek.Name = "btnNextWeek";
            this.btnNextWeek.Size = new System.Drawing.Size(80, 30);
            this.btnNextWeek.TabIndex = 5;
            this.btnNextWeek.TextValue = "下周";
            this.btnNextWeek.UseVisualStyleBackColor = true;
            this.btnNextWeek.Click += new System.EventHandler(this.btnNextWeek_Click);
            // 
            // btnPreWeek
            // 
            this.btnPreWeek.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPreWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreWeek.Image = ((System.Drawing.Image)(resources.GetObject("btnPreWeek.Image")));
            this.btnPreWeek.ImageRight = false;
            this.btnPreWeek.ImageStyle = HISPlus.UserControls.ImageStyle.Backward;
            this.btnPreWeek.Location = new System.Drawing.Point(438, 19);
            this.btnPreWeek.Margin = new System.Windows.Forms.Padding(4);
            this.btnPreWeek.MaximumSize = new System.Drawing.Size(100, 30);
            this.btnPreWeek.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnPreWeek.Name = "btnPreWeek";
            this.btnPreWeek.Size = new System.Drawing.Size(80, 30);
            this.btnPreWeek.TabIndex = 4;
            this.btnPreWeek.TextValue = "上周";
            this.btnPreWeek.UseVisualStyleBackColor = true;
            this.btnPreWeek.Click += new System.EventHandler(this.btnPreWeek_Click);
            // 
            // dtPicker
            // 
            this.dtPicker.Location = new System.Drawing.Point(281, 21);
            this.dtPicker.Margin = new System.Windows.Forms.Padding(4);
            this.dtPicker.Name = "dtPicker";
            this.dtPicker.Size = new System.Drawing.Size(148, 22);
            this.dtPicker.TabIndex = 3;
            this.dtPicker.ValueChanged += new System.EventHandler(this.dtPicker_ValueChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(233, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "日期";
            // 
            // cmbDept
            // 
            this.cmbDept.DataSource = null;
            this.cmbDept.DisplayMember = null;
            this.cmbDept.DropDownHeight = 0;
            this.cmbDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDept.DropDownWidth = 0;
            this.cmbDept.DroppedDown = false;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.IntegralHeight = true;
            this.cmbDept.Location = new System.Drawing.Point(62, 22);
            this.cmbDept.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDept.MaxDropDownItems = 0;
            this.cmbDept.MaximumSize = new System.Drawing.Size(1000, 24);
            this.cmbDept.MinimumSize = new System.Drawing.Size(40, 24);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.SelectedIndex = -1;
            this.cmbDept.SelectedValue = null;
            this.cmbDept.Size = new System.Drawing.Size(163, 24);
            this.cmbDept.TabIndex = 1;
            this.cmbDept.ValueMember = null;
            this.cmbDept.SelectedIndexChanged += new System.EventHandler(this.cmbDept_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(22, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "科室";
            // 
            // grpSchedule
            // 
            this.grpSchedule.Controls.Add(this.ucGridView1);
            this.grpSchedule.Controls.Add(this.lblMemo);
            this.grpSchedule.Controls.Add(this.lblWeekDay0);
            this.grpSchedule.Controls.Add(this.lblDate0);
            this.grpSchedule.Controls.Add(this.label5);
            this.grpSchedule.Controls.Add(this.label4);
            this.grpSchedule.Controls.Add(this.lblTopLeft);
            this.grpSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSchedule.Location = new System.Drawing.Point(0, 69);
            this.grpSchedule.Margin = new System.Windows.Forms.Padding(4);
            this.grpSchedule.Name = "grpSchedule";
            this.grpSchedule.Padding = new System.Windows.Forms.Padding(4);
            this.grpSchedule.Size = new System.Drawing.Size(1139, 672);
            this.grpSchedule.TabIndex = 1;
            // 
            // ucGridView1
            // 
            this.ucGridView1.AllowAddRows = false;
            this.ucGridView1.AllowDeleteRows = false;
            this.ucGridView1.AllowEdit = false;
            this.ucGridView1.AllowSort = false;
            this.ucGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.ucGridView1.ColumnAutoWidth = true;
            this.ucGridView1.ColumnsEvenOldRowColor = null;
            this.ucGridView1.DataSource = null;
            this.ucGridView1.Location = new System.Drawing.Point(13, 72);
            this.ucGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucGridView1.Name = "ucGridView1";
            this.ucGridView1.ShowRowIndicator = false;
            this.ucGridView1.Size = new System.Drawing.Size(447, 587);
            this.ucGridView1.TabIndex = 22;
            // 
            // lblMemo
            // 
            this.lblMemo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblMemo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMemo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblMemo.Location = new System.Drawing.Point(219, 16);
            this.lblMemo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMemo.Name = "lblMemo";
            this.lblMemo.Size = new System.Drawing.Size(241, 57);
            this.lblMemo.TabIndex = 21;
            this.lblMemo.Text = "备    注";
            // 
            // lblWeekDay0
            // 
            this.lblWeekDay0.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblWeekDay0.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblWeekDay0.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblWeekDay0.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblWeekDay0.Location = new System.Drawing.Point(99, 44);
            this.lblWeekDay0.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWeekDay0.Name = "lblWeekDay0";
            this.lblWeekDay0.Size = new System.Drawing.Size(121, 29);
            this.lblWeekDay0.TabIndex = 6;
            this.lblWeekDay0.Text = "一";
            // 
            // lblDate0
            // 
            this.lblDate0.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblDate0.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDate0.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblDate0.Location = new System.Drawing.Point(99, 16);
            this.lblDate0.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDate0.Name = "lblDate0";
            this.lblDate0.Size = new System.Drawing.Size(121, 29);
            this.lblDate0.TabIndex = 5;
            this.lblDate0.Text = "05月04";
            // 
            // label5
            // 
            this.label5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.label5.Location = new System.Drawing.Point(22, 50);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 18);
            this.label5.TabIndex = 4;
            this.label5.Text = "护士";
            // 
            // label4
            // 
            this.label4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.label4.Location = new System.Drawing.Point(59, 22);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "时间";
            // 
            // lblTopLeft
            // 
            this.lblTopLeft.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTopLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblTopLeft.Location = new System.Drawing.Point(13, 16);
            this.lblTopLeft.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTopLeft.Name = "lblTopLeft";
            this.lblTopLeft.Size = new System.Drawing.Size(88, 57);
            this.lblTopLeft.TabIndex = 2;
            // 
            // cmnuNurseDuty
            // 
            this.cmnuNurseDuty.Name = "cmnuNurseDuty";
            this.cmnuNurseDuty.ShowImageMargin = false;
            this.cmnuNurseDuty.Size = new System.Drawing.Size(128, 26);
            this.cmnuNurseDuty.Opening += new System.ComponentModel.CancelEventHandler(this.cmnuNurseDuty_Opening);
            // 
            // NurseScheduleDayFrm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1139, 741);
            this.Controls.Add(this.grpSchedule);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NurseScheduleDayFrm";
            this.Text = "护士排班";
            this.Load += new System.EventHandler(this.NurseScheduleFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpSchedule)).EndInit();
            this.grpSchedule.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PanelControl groupBox1;
        private LabelControl label2;
        private HISPlus.UserControls.UcComboBox cmbDept;
        private LabelControl label3;
        private System.Windows.Forms.DateTimePicker dtPicker;
        private HISPlus.UserControls.UcButton btnPreWeek;
        private HISPlus.UserControls.UcButton btnNextWeek;
        private HISPlus.UserControls.UcButton btnCopy;
        private HISPlus.UserControls.UcButton btnSave;
        private PanelControl  grpSchedule;
        private LabelControl lblTopLeft;
        private LabelControl lblDate0;
        private LabelControl lblWeekDay0;
        private LabelControl lblMemo;
        private System.Windows.Forms.ContextMenuStrip cmnuNurseDuty;
        private HISPlus.UserControls.UcButton btnPrint;
        private HISPlus.UserControls.UcButton btnClear;
        private UserControls.UcGridView ucGridView1;
        private LabelControl label5;
        private LabelControl label4;
    }
}