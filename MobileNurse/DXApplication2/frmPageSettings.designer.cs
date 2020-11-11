namespace RDLCReport
{
    partial class frmPageSettings
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
            this.cmb表单 = new System.Windows.Forms.ComboBox();
            this.grpForm = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btn添加表单 = new System.Windows.Forms.Button();
            this.txt表单高度 = new System.Windows.Forms.TextBox();
            this.txt表单宽度 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt表单 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpOrientation = new System.Windows.Forms.GroupBox();
            this.rad纵向 = new System.Windows.Forms.RadioButton();
            this.rad横向 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt底部边距 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt顶部边距 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt右边距 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt左边距 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn状态 = new System.Windows.Forms.Button();
            this.btn默认打印机 = new System.Windows.Forms.Button();
            this.cmb打印机 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btn取消 = new System.Windows.Forms.Button();
            this.btn确定 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.epError = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpForm.SuspendLayout();
            this.grpOrientation.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epError)).BeginInit();
            this.SuspendLayout();
            // 
            // cmb表单
            // 
            this.cmb表单.FormattingEnabled = true;
            this.cmb表单.Location = new System.Drawing.Point(75, 20);
            this.cmb表单.Name = "cmb表单";
            this.cmb表单.Size = new System.Drawing.Size(198, 20);
            this.cmb表单.TabIndex = 2;
            this.cmb表单.SelectedValueChanged += new System.EventHandler(this.cmb表单_SelectedValueChanged);
            // 
            // grpForm
            // 
            this.grpForm.Controls.Add(this.btnDelete);
            this.grpForm.Controls.Add(this.btn添加表单);
            this.grpForm.Controls.Add(this.txt表单高度);
            this.grpForm.Controls.Add(this.txt表单宽度);
            this.grpForm.Controls.Add(this.label5);
            this.grpForm.Controls.Add(this.label4);
            this.grpForm.Controls.Add(this.label3);
            this.grpForm.Controls.Add(this.txt表单);
            this.grpForm.Controls.Add(this.label2);
            this.grpForm.Controls.Add(this.cmb表单);
            this.grpForm.Controls.Add(this.label1);
            this.grpForm.Location = new System.Drawing.Point(12, 96);
            this.grpForm.Name = "grpForm";
            this.grpForm.Size = new System.Drawing.Size(363, 111);
            this.grpForm.TabIndex = 3;
            this.grpForm.TabStop = false;
            this.grpForm.Text = "纸张";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(279, 79);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "删除(&D)";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btn添加表单
            // 
            this.btn添加表单.Location = new System.Drawing.Point(279, 49);
            this.btn添加表单.Name = "btn添加表单";
            this.btn添加表单.Size = new System.Drawing.Size(75, 23);
            this.btn添加表单.TabIndex = 10;
            this.btn添加表单.Text = "添加(&A)";
            this.btn添加表单.UseVisualStyleBackColor = true;
            this.btn添加表单.Click += new System.EventHandler(this.btn添加表单_Click);
            // 
            // txt表单高度
            // 
            this.txt表单高度.Location = new System.Drawing.Point(223, 80);
            this.txt表单高度.Name = "txt表单高度";
            this.txt表单高度.Size = new System.Drawing.Size(50, 21);
            this.txt表单高度.TabIndex = 9;
            // 
            // txt表单宽度
            // 
            this.txt表单宽度.Location = new System.Drawing.Point(116, 80);
            this.txt表单宽度.Name = "txt表单宽度";
            this.txt表单宽度.Size = new System.Drawing.Size(50, 21);
            this.txt表单宽度.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(182, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "高度：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(75, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "宽度：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "纸张大小：";
            // 
            // txt表单
            // 
            this.txt表单.Location = new System.Drawing.Point(75, 50);
            this.txt表单.Name = "txt表单";
            this.txt表单.Size = new System.Drawing.Size(198, 21);
            this.txt表单.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "表 格 名：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "格    式：";
            // 
            // grpOrientation
            // 
            this.grpOrientation.Controls.Add(this.rad纵向);
            this.grpOrientation.Controls.Add(this.rad横向);
            this.grpOrientation.Location = new System.Drawing.Point(12, 213);
            this.grpOrientation.Name = "grpOrientation";
            this.grpOrientation.Size = new System.Drawing.Size(363, 48);
            this.grpOrientation.TabIndex = 4;
            this.grpOrientation.TabStop = false;
            this.grpOrientation.Text = "方向：";
            // 
            // rad纵向
            // 
            this.rad纵向.AutoSize = true;
            this.rad纵向.Location = new System.Drawing.Point(184, 20);
            this.rad纵向.Name = "rad纵向";
            this.rad纵向.Size = new System.Drawing.Size(65, 16);
            this.rad纵向.TabIndex = 1;
            this.rad纵向.TabStop = true;
            this.rad纵向.Text = "纵向(&V)";
            this.rad纵向.UseVisualStyleBackColor = true;
            // 
            // rad横向
            // 
            this.rad横向.AutoSize = true;
            this.rad横向.Checked = true;
            this.rad横向.Location = new System.Drawing.Point(80, 20);
            this.rad横向.Name = "rad横向";
            this.rad横向.Size = new System.Drawing.Size(65, 16);
            this.rad横向.TabIndex = 0;
            this.rad横向.TabStop = true;
            this.rad横向.Text = "横向(&H)";
            this.rad横向.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt底部边距);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txt顶部边距);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txt右边距);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txt左边距);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(12, 267);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(363, 48);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印边距：";
            // 
            // txt底部边距
            // 
            this.txt底部边距.Location = new System.Drawing.Point(314, 20);
            this.txt底部边距.Name = "txt底部边距";
            this.txt底部边距.Size = new System.Drawing.Size(33, 21);
            this.txt底部边距.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(273, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "底部：";
            // 
            // txt顶部边距
            // 
            this.txt顶部边距.Location = new System.Drawing.Point(222, 20);
            this.txt顶部边距.Name = "txt顶部边距";
            this.txt顶部边距.Size = new System.Drawing.Size(33, 21);
            this.txt顶部边距.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(181, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "顶部：";
            // 
            // txt右边距
            // 
            this.txt右边距.Location = new System.Drawing.Point(129, 20);
            this.txt右边距.Name = "txt右边距";
            this.txt右边距.Size = new System.Drawing.Size(33, 21);
            this.txt右边距.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(100, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "右：";
            // 
            // txt左边距
            // 
            this.txt左边距.Location = new System.Drawing.Point(49, 20);
            this.txt左边距.Name = "txt左边距";
            this.txt左边距.Size = new System.Drawing.Size(33, 21);
            this.txt左边距.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "左：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn状态);
            this.groupBox2.Controls.Add(this.btn默认打印机);
            this.groupBox2.Controls.Add(this.cmb打印机);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(12, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(363, 82);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "打印机";
            // 
            // btn状态
            // 
            this.btn状态.Location = new System.Drawing.Point(197, 48);
            this.btn状态.Name = "btn状态";
            this.btn状态.Size = new System.Drawing.Size(75, 23);
            this.btn状态.TabIndex = 15;
            this.btn状态.Text = "状态(&U)";
            this.btn状态.UseVisualStyleBackColor = true;
            this.btn状态.Click += new System.EventHandler(this.btn状态_Click);
            // 
            // btn默认打印机
            // 
            this.btn默认打印机.Location = new System.Drawing.Point(279, 48);
            this.btn默认打印机.Name = "btn默认打印机";
            this.btn默认打印机.Size = new System.Drawing.Size(75, 23);
            this.btn默认打印机.TabIndex = 14;
            this.btn默认打印机.Text = "默认(&E)";
            this.btn默认打印机.UseVisualStyleBackColor = true;
            this.btn默认打印机.Click += new System.EventHandler(this.btn默认打印机_Click);
            // 
            // cmb打印机
            // 
            this.cmb打印机.FormattingEnabled = true;
            this.cmb打印机.Location = new System.Drawing.Point(75, 22);
            this.cmb打印机.Name = "cmb打印机";
            this.cmb打印机.Size = new System.Drawing.Size(279, 20);
            this.cmb打印机.TabIndex = 3;
            this.cmb打印机.SelectedValueChanged += new System.EventHandler(this.cmb打印机_SelectedValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "打 印 机：";
            // 
            // btn取消
            // 
            this.btn取消.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn取消.Location = new System.Drawing.Point(300, 332);
            this.btn取消.Name = "btn取消";
            this.btn取消.Size = new System.Drawing.Size(75, 23);
            this.btn取消.TabIndex = 15;
            this.btn取消.Text = "取消(&C)";
            this.btn取消.UseVisualStyleBackColor = true;
            this.btn取消.Click += new System.EventHandler(this.btn取消_Click);
            // 
            // btn确定
            // 
            this.btn确定.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn确定.Location = new System.Drawing.Point(219, 332);
            this.btn确定.Name = "btn确定";
            this.btn确定.Size = new System.Drawing.Size(75, 23);
            this.btn确定.TabIndex = 16;
            this.btn确定.Text = "确定(&O)";
            this.btn确定.UseVisualStyleBackColor = true;
            this.btn确定.Click += new System.EventHandler(this.btn确定_Click);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 337);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(197, 12);
            this.label11.TabIndex = 17;
            this.label11.Text = "注：以上长度均以厘米(cm)为单位。";
            // 
            // epError
            // 
            this.epError.ContainerControl = this;
            // 
            // frmPageSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 369);
            this.Controls.Add(this.btn确定);
            this.Controls.Add(this.btn取消);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpOrientation);
            this.Controls.Add(this.grpForm);
            this.Controls.Add(this.label11);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmPageSettings";
            this.Text = "页面设置";
            this.Load += new System.EventHandler(this.frmPageSettings_Load);
            this.grpForm.ResumeLayout(false);
            this.grpForm.PerformLayout();
            this.grpOrientation.ResumeLayout(false);
            this.grpOrientation.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb表单;
        private System.Windows.Forms.GroupBox grpForm;
        private System.Windows.Forms.TextBox txt表单;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt表单高度;
        private System.Windows.Forms.TextBox txt表单宽度;
        private System.Windows.Forms.Button btn添加表单;
        private System.Windows.Forms.GroupBox grpOrientation;
        private System.Windows.Forms.RadioButton rad纵向;
        private System.Windows.Forms.RadioButton rad横向;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt底部边距;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt顶部边距;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt右边距;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt左边距;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmb打印机;
        private System.Windows.Forms.Button btn默认打印机;
        private System.Windows.Forms.Button btn取消;
        private System.Windows.Forms.Button btn确定;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btn状态;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ErrorProvider epError;


    }
}