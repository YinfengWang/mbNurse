//------------------------------------------------------------------------------------
//  ϵͳ����        : ҽԺ��Ϣϵͳ
//  ��ϵͳ����      : ͨ��ģ��
//  ��������        : 
//  ����            : ErrDspForm.cs
//  ���ܸ�Ҫ        : ������ʾ�Ի���
//  ������          : ����
//  ������          : 2007-01-19
//  �汾            : 1.0.0.0
// 
//------------------< �����ʷ >------------------------------------------------------
//  �������        :
//  �����          :
//  �������        :
//  �汾            :
//------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HISPlus
{
    /// <summary>
    /// ErrDspForm ��ժҪ˵����
    /// </summary>
    public class ErrDspForm : FormDo
    {
        private DevExpress.XtraEditors.MemoEdit txtErrMsg;
        private UserControls.UcButton btnClose;
        private UserControls.UcButton btnCopyToClipboard;
        //private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevExpress.XtraEditors.GroupControl groupPanel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblTimer;
        private Timer timer1;

        /// <summary>
        /// ����ʱ���Զ��رո�ҳ��
        /// </summary>
        private int SecondCount = 5;
        private IContainer components;

        public ErrDspForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        #region Windows ������������ɵĴ���
        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrDspForm));
            this.txtErrMsg = new DevExpress.XtraEditors.MemoEdit();
            this.btnClose = new HISPlus.UserControls.UcButton();
            this.btnCopyToClipboard = new HISPlus.UserControls.UcButton();
            this.groupPanel1 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblTimer = new DevExpress.XtraEditors.LabelControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtErrMsg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupPanel1)).BeginInit();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtErrMsg
            // 
            this.txtErrMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtErrMsg.Location = new System.Drawing.Point(2, 26);
            this.txtErrMsg.Name = "txtErrMsg";
            this.txtErrMsg.Size = new System.Drawing.Size(501, 295);
            this.txtErrMsg.TabIndex = 4;
            this.txtErrMsg.UseOptimizedRendering = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;            
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageRight = false;
            this.btnClose.ImageStyle = HISPlus.UserControls.ImageStyle.Close;
            this.btnClose.Location = new System.Drawing.Point(402, 14);
            this.btnClose.MaximumSize = new System.Drawing.Size(80, 30);
            this.btnClose.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 30);
            this.btnClose.TabIndex = 3;
            this.btnClose.TextValue = "�ر�";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCopyToClipboard
            // 
            this.btnCopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyToClipboard.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCopyToClipboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyToClipboard.Image = ((System.Drawing.Image)(resources.GetObject("btnCopyToClipboard.Image")));
            this.btnCopyToClipboard.ImageRight = false;
            this.btnCopyToClipboard.ImageStyle = HISPlus.UserControls.ImageStyle.Copy;
            this.btnCopyToClipboard.Location = new System.Drawing.Point(300, 14);
            this.btnCopyToClipboard.MaximumSize = new System.Drawing.Size(80, 30);
            this.btnCopyToClipboard.MinimumSize = new System.Drawing.Size(60, 30);
            this.btnCopyToClipboard.Name = "btnCopyToClipboard";
            this.btnCopyToClipboard.Size = new System.Drawing.Size(80, 30);
            this.btnCopyToClipboard.TabIndex = 2;
            this.btnCopyToClipboard.TextValue = "����";
            this.btnCopyToClipboard.UseVisualStyleBackColor = false;
            this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.Controls.Add(this.txtErrMsg);
            this.groupPanel1.Location = new System.Drawing.Point(12, 12);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(505, 323);
            this.groupPanel1.TabIndex = 5;
            this.groupPanel1.Text = "�쳣��Ϣ";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblTimer);
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btnCopyToClipboard);
            this.panelControl1.Location = new System.Drawing.Point(12, 341);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(505, 60);
            this.panelControl1.TabIndex = 6;
            // 
            // lblTimer
            // 
            this.lblTimer.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblTimer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTimer.Location = new System.Drawing.Point(17, 21);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(108, 18);
            this.lblTimer.TabIndex = 4;
            this.lblTimer.Text = "5 ����Զ��ر�.";
            this.lblTimer.ToolTip = "���ȡ��[�Զ��ر�]\r\n";
            this.lblTimer.Click += new System.EventHandler(this.lblTimer_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ErrDspForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(529, 407);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.groupPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrDspForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ϵͳ�쳣";
            this.Load += new System.EventHandler(this.ErrDspForm_Load);
            this.VisibleChanged += new System.EventHandler(this.ErrDspForm_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.txtErrMsg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupPanel1)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// ���ô�����Ϣ
        /// </summary>
        /// <param name="strErrMsg"></param>
        public void setErrMsg(string strErrMsg)
        {
            txtErrMsg.Text = strErrMsg;
        }

        /// <summary>
        /// [�ر�]��ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Hide();
        }


        /// <summary>
        /// ���ı������ݿ�������������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyToClipboard_Click(object sender, System.EventArgs e)
        {
            Clipboard.SetDataObject(this.txtErrMsg.Text, true);
        }

        private void lblTimer_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();

            lblTimer.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (SecondCount <= 0)
            {
                timer1.Enabled = false;
                timer1.Stop();
                
                this.Hide();
            }
            else
            {
                this.lblTimer.Text = SecondCount+" ����Զ��ر�.";

                SecondCount--;
            }
        }

        private void ErrDspForm_Load(object sender, EventArgs e)
        {
            timer1.Start();

            base.LoadingClose();
        }

        private void ErrDspForm_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                timer1.Enabled = false;
                timer1.Stop();

                this.setErrMsg(null);
            }
        }
    }
}
