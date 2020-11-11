using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HISPlus.UserControls
{
    /// <summary>
    /// 多行文本框
    /// </summary>
    public partial class UcTextArea : UserControl, IBaseTextBox
    {
        public UcTextArea()
        {
            InitializeComponent();

            txtControl.KeyPress += txtControl_KeyPress;
            txtControl.EditValueChanging += txtControl_EditValueChanging;
        }

        //public string EditValue
        //{
        //    get { return _editValue; }
        //    set { _editValue = value; }
        //}

        //private string _editValue;

        public bool Multiline
        {
            get { return !this.txtControl.Properties.AutoHeight; }
            set { this.txtControl.Properties.AutoHeight = !value; }
        }

        /// <summary>
        /// 最大长度
        /// </summary>
        public int MaxLength
        {
            get { return this.txtControl.Properties.MaxLength; }
            set { this.txtControl.Properties.MaxLength = value; }
        }

        private int _maxCharLength;

        /// <summary>
        /// 最大长度[最大字符长度,一个汉字为两个字符长度]
        /// </summary>
        public int MaxCharLength
        {
            get { return _maxCharLength; }
            set
            {
                _maxCharLength = value;

            }
        }

        void txtControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_maxCharLength > 0)
            {
                if (Coding.GetStrByteLen(txtControl.Text) > _maxCharLength)
                    e.Handled = false;
            }
        }

        void txtControl_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (_maxCharLength > 0)
            {
                if (Coding.GetStrByteLen(txtControl.Text) > _maxCharLength)
                    e.Cancel = true;
            }
        }

        public ScrollBars ScrollBars
        {
            get { return txtControl.Properties.ScrollBars; }
            set
            {
                txtControl.Properties.ScrollBars = value;
            }
        }

        public char PasswordChar
        {
            get { return this.txtControl.Properties.PasswordChar; }
            set { this.txtControl.Properties.PasswordChar = value; }
        }

        public bool ReadOnly
        {
            get { return txtControl.Properties.ReadOnly; }
            set { txtControl.Properties.ReadOnly = value; }
        }

        public int SelectionStart
        {
            get { return txtControl.SelectionStart; }
            set { txtControl.SelectionStart = value; }
        }

        public int SelectionLength
        {
            get { return txtControl.SelectionLength; }
            set { txtControl.SelectionLength = value; }
        }

        public string SelectedText
        {
            get { return txtControl.SelectedText; }
            set { txtControl.SelectedText = value; }
        }

        public bool HideSelection
        {
            get { return txtControl.Properties.HideSelection; }
            set { txtControl.Properties.HideSelection = value; }
        }

        [Browsable(true)]
        public override string Text
        {
            get { return this.txtControl.Text; }
            set { this.txtControl.Text = value; }
        }

        public new System.IntPtr Handle
        {
            get { return this.txtControl.Handle; }
        }


        /// <summary>
        /// 文本变更事件
        /// </summary>
        public new event EventHandler TextChanged;

        private void txtControl_EditValueChanged(object sender, EventArgs e)
        {            
            if (TextChanged != null)
                TextChanged(sender, e);
        }
    }
}
