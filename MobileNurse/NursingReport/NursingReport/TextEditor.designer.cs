namespace HISPlus
{
    partial class TextEditor
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( TextEditor ) );
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTemplate = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnEditTemplate = new System.Windows.Forms.Button();
            this.btnSaveTemplate = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.imageList1 = new System.Windows.Forms.ImageList( this.components );
            this.dtPicker = new System.Windows.Forms.DateTimePicker();
            this.trvDirectory = new System.Windows.Forms.TreeView();
            this.txtEdit = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point( 253, 8 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 134, 12 );
            this.label1.TabIndex = 1;
            this.label1.Text = "病情观察及护理措施";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 15, 8 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 29, 12 );
            this.label2.TabIndex = 2;
            this.label2.Text = "模板";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point( 10, 203 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size( 53, 12 );
            this.label3.TabIndex = 4;
            this.label3.Text = "模板内容";
            // 
            // txtTemplate
            // 
            this.txtTemplate.AcceptsReturn = true;
            this.txtTemplate.AllowDrop = true;
            this.txtTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTemplate.HideSelection = false;
            this.txtTemplate.Location = new System.Drawing.Point( 12, 222 );
            this.txtTemplate.Multiline = true;
            this.txtTemplate.Name = "txtTemplate";
            this.txtTemplate.ReadOnly = true;
            this.txtTemplate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTemplate.Size = new System.Drawing.Size( 236, 116 );
            this.txtTemplate.TabIndex = 5;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point( 192, 198 );
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size( 56, 23 );
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "=>";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler( this.btnAdd_Click );
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point( 423, 350 );
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size( 79, 23 );
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "退出(&E)";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler( this.btnExit_Click );
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point( 320, 350 );
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size( 75, 23 );
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler( this.btnOk_Click );
            // 
            // btnEditTemplate
            // 
            this.btnEditTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditTemplate.Location = new System.Drawing.Point( 12, 350 );
            this.btnEditTemplate.Name = "btnEditTemplate";
            this.btnEditTemplate.Size = new System.Drawing.Size( 75, 23 );
            this.btnEditTemplate.TabIndex = 9;
            this.btnEditTemplate.Text = "编辑模板";
            this.btnEditTemplate.UseVisualStyleBackColor = true;
            this.btnEditTemplate.Click += new System.EventHandler( this.btnEditTemplate_Click );
            // 
            // btnSaveTemplate
            // 
            this.btnSaveTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveTemplate.Location = new System.Drawing.Point( 173, 350 );
            this.btnSaveTemplate.Name = "btnSaveTemplate";
            this.btnSaveTemplate.Size = new System.Drawing.Size( 75, 23 );
            this.btnSaveTemplate.TabIndex = 10;
            this.btnSaveTemplate.Text = "保存模板";
            this.btnSaveTemplate.UseVisualStyleBackColor = true;
            this.btnSaveTemplate.Click += new System.EventHandler( this.btnSaveTemplate_Click );
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point( 12, 172 );
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size( 236, 21 );
            this.txtPath.TabIndex = 11;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject( "imageList1.ImageStream" )));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName( 0, "Folder.ico" );
            this.imageList1.Images.SetKeyName( 1, "FOLDEROP.ICO" );
            this.imageList1.Images.SetKeyName( 2, "mrqc.ico" );
            // 
            // dtPicker
            // 
            this.dtPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtPicker.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPicker.Location = new System.Drawing.Point( 380, 2 );
            this.dtPicker.Name = "dtPicker";
            this.dtPicker.Size = new System.Drawing.Size( 121, 21 );
            this.dtPicker.TabIndex = 13;
            // 
            // trvDirectory
            // 
            this.trvDirectory.HideSelection = false;
            this.trvDirectory.Location = new System.Drawing.Point( 12, 24 );
            this.trvDirectory.Name = "trvDirectory";
            this.trvDirectory.Size = new System.Drawing.Size( 236, 142 );
            this.trvDirectory.TabIndex = 14;
            // 
            // txtEdit
            // 
            this.txtEdit.AcceptsReturn = true;
            this.txtEdit.AllowDrop = true;
            this.txtEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEdit.HideSelection = false;
            this.txtEdit.Location = new System.Drawing.Point( 254, 24 );
            this.txtEdit.Multiline = true;
            this.txtEdit.Name = "txtEdit";
            this.txtEdit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEdit.Size = new System.Drawing.Size( 247, 314 );
            this.txtEdit.TabIndex = 15;
            // 
            // TextEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 12F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 514, 385 );
            this.Controls.Add( this.txtEdit );
            this.Controls.Add( this.trvDirectory );
            this.Controls.Add( this.dtPicker );
            this.Controls.Add( this.txtPath );
            this.Controls.Add( this.btnSaveTemplate );
            this.Controls.Add( this.btnEditTemplate );
            this.Controls.Add( this.btnOk );
            this.Controls.Add( this.btnExit );
            this.Controls.Add( this.btnAdd );
            this.Controls.Add( this.txtTemplate );
            this.Controls.Add( this.label3 );
            this.Controls.Add( this.label2 );
            this.Controls.Add( this.label1 );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject( "$this.Icon" )));
            this.MaximizeBox = false;
            this.Name = "TextEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "病情录入";
            this.Load += new System.EventHandler( this.TextEditor_Load );
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTemplate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnEditTemplate;
        private System.Windows.Forms.Button btnSaveTemplate;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.DateTimePicker dtPicker;
        private System.Windows.Forms.TreeView trvDirectory;
        private System.Windows.Forms.TextBox txtEdit;
    }
}