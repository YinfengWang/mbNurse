using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace HISPlus
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// 单选框
    /// </summary>
    public class RadioButtonNode : BaseNode
    {
        private readonly RadioButton _checkEdit;
        /// <summary>
        /// 后缀(Label)
        /// </summary>
        private readonly LabelControl _lblPostFix;

        public RadioButtonNode(DesignTemplate container, DocTemplateElement nursingDocNode)
            : base(container, nursingDocNode)
        {
            _checkEdit = new RadioButton { Font = ContentFont, Text = nursingDocNode.DisplayName, ForeColor = Color.Blue };
            _checkEdit.AutoCheck = true;
            _checkEdit.TextAlign = ContentAlignment.MiddleLeft;
            _checkEdit.AutoSize = false;
            // this._checkEdit.PreferredSize.Width
            this._checkEdit.Size = new Size(Math.Max(this._checkEdit.PreferredSize.Width, ControlWidth), this.ItemHeight);


            ControlList.Add(this._checkEdit);
            if (nursingDocNode.Score > 0)
            {
                _lblPostFix = new LabelControl();
                _lblPostFix.Font = ContentFont;
                _lblPostFix.Text = string.Format("({0}分)", nursingDocNode.Score);//2015.11.09 del
                _lblPostFix.Appearance.TextOptions.HAlignment
             = DevExpress.Utils.HorzAlignment.Near;
                _lblPostFix.Appearance.TextOptions.VAlignment
                    = DevExpress.Utils.VertAlignment.Center;
                _lblPostFix.Size = new Size(this._lblPostFix.PreferredSize.Width, this.ItemHeight);
                ControlList.Add(this._lblPostFix);//2015.11.09 del
            }

            this._checkEdit.CheckedChanged += this._checkbox_CheckedChanged;
            this._checkEdit.KeyPress += this._checkbox_KeyPress;
        }

        private void _checkbox_CheckedChanged(object sender, EventArgs e)
        {
            OnValueChanged(this, e);
        }

        private void _checkbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Container.SelectNextControl(sender as Control, true, true, true, true);
                e.Handled = true;
            }
        }

        protected override void LayoutBeforeChildNodes(ref Point location)
        {
            LayoutPrefix(ref location);
            Container.AddControl(this._checkEdit, new Point(location.X, location.Y + (int)NursingDocNode.RowSpacing));
            location.X += this._checkEdit.Width;
            if (!IsInGrid) //2015.11.09 debug 表格内暂不绘制后缀
            if (_lblPostFix != null)
            {
                Container.AddControl(this._lblPostFix, new Point(location.X, location.Y +3+ (int)NursingDocNode.RowSpacing));
                location.X += this._lblPostFix.Width;
            }
        }

        public RadioButton CheckBox
        {
            get
            {
                return this._checkEdit;
            }
        }

        protected virtual bool HasFormattedValue
        {
            get
            {
                return this._checkEdit.Checked;
            }
        }

        protected override sealed int ItemHeight
        {
            get
            {
                return Math.Max(base.ItemHeight, this._checkEdit.PreferredSize.Height);
            }
        }

        protected override int PartWidthBeforeChildNodes
        {
            get
            {
                return (PostfixWidth + this._checkEdit.Width + (this._lblPostFix == null ? 0 : this._lblPostFix.Width));
            }
        }

        public override string SelfFormattedValue
        {
            get
            {
                return string.Empty;
            }
        }

        public override object Value
        {
            get
            {
                if (this._checkEdit.Checked)
                {
                    return 1;
                }
                return null;
            }
            set
            {
                this._checkEdit.Checked = (value != null) && value.ToString().Equals("1");
            }
        }
    }
}

