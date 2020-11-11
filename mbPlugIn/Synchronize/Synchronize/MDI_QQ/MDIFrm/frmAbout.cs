//------------------------------------------------------------------------------------
//
//  系统名称        : 医院信息系统
//  子系统名称      : 共通模块
//  对象类型        : 
//  类名            : frmAbout.cs
//  功能概要        : 窗体[关于]
//  作成者          : 付军
//  作成日          : 2007-01-22
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        :
//  变更者          :
//  变更内容        :
//  版本            :
//------------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace HISPlus
{
	/// <summary>
	/// About 的摘要说明。
	/// </summary>
	public class frmAbout : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblCopyRight;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
        private System.Windows.Forms.GroupBox grpSep;

        public string Title     = string.Empty;                         // 应用程序名称
        public string CopyRight = string.Empty;
        private Label lblWarning;                         // 版权
        public string Version   = string.Empty;                         // 版本

		public frmAbout()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.btnOk = new System.Windows.Forms.Button();
            this.lblAppName = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblCopyRight = new System.Windows.Forms.Label();
            this.grpSep = new System.Windows.Forms.GroupBox();
            this.lblWarning = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(306, 242);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblAppName
            // 
            this.lblAppName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAppName.BackColor = System.Drawing.Color.Transparent;
            this.lblAppName.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAppName.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblAppName.Location = new System.Drawing.Point(2, 86);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(384, 28);
            this.lblAppName.TabIndex = 18;
            this.lblAppName.Text = "应用程序名称";
            this.lblAppName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.ForeColor = System.Drawing.Color.Blue;
            this.lblVersion.Location = new System.Drawing.Point(40, 134);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(264, 16);
            this.lblVersion.TabIndex = 19;
            this.lblVersion.Text = "版本号";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCopyRight
            // 
            this.lblCopyRight.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyRight.ForeColor = System.Drawing.Color.Blue;
            this.lblCopyRight.Location = new System.Drawing.Point(40, 150);
            this.lblCopyRight.Name = "lblCopyRight";
            this.lblCopyRight.Size = new System.Drawing.Size(320, 24);
            this.lblCopyRight.TabIndex = 20;
            this.lblCopyRight.Text = "版权信息";
            this.lblCopyRight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpSep
            // 
            this.grpSep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSep.Location = new System.Drawing.Point(10, 216);
            this.grpSep.Name = "grpSep";
            this.grpSep.Size = new System.Drawing.Size(370, 8);
            this.grpSep.TabIndex = 21;
            this.grpSep.TabStop = false;
            // 
            // lblWarning
            // 
            this.lblWarning.Location = new System.Drawing.Point(10, 230);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(292, 68);
            this.lblWarning.TabIndex = 22;
            // 
            // frmAbout
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(388, 302);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.grpSep);
            this.Controls.Add(this.lblCopyRight);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblAppName);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "关于";
            this.Load += new System.EventHandler(this.About_Load);
            this.ResumeLayout(false);

        }
		#endregion


        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void About_Load(object sender, System.EventArgs e)
        {
            this.Text              += Title;                            // 窗口标题
            this.lblAppName.Text    = Title;                            // 应用程序的名称
            this.lblCopyRight.Text  = CopyRight;                        // 版权
            this.lblVersion.Text    = Version;                          // 版本
        }


        /// <summary>
        /// [确定]按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
	}
}
