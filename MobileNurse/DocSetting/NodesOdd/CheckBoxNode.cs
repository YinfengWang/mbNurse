namespace HISPlus
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// 复选框
    /// </summary>
    public class CheckBoxNode : BaseNode
    {
        private System.Windows.Forms.CheckBox _checkbox;

        public CheckBoxNode(DocContainer container, DocTemplateElement nursingDocNode)
            : base(container, nursingDocNode)
        {
            this._checkbox = new System.Windows.Forms.CheckBox();
            this._checkbox.Font = base.ContentFont;
            this._checkbox.Text = nursingDocNode.DisplayName;
            this._checkbox.ForeColor = Color.Blue;
            this._checkbox.TextAlign = ContentAlignment.MiddleLeft;
            this._checkbox.Size = new Size(this._checkbox.PreferredSize.Width, this.ItemHeight);
            base.ControlList.Add(this._checkbox);
            this._checkbox.CheckedChanged += new EventHandler(this._checkbox_CheckedChanged);
            this._checkbox.KeyPress += new KeyPressEventHandler(this._checkbox_KeyPress);
        }

        private void _checkbox_CheckedChanged(object sender, EventArgs e)
        {
            base.OnValueChanged(this, e);
        }

        private void _checkbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                base.Container.SelectNextControl(sender as Control, true, true, true, true);
                e.Handled = true;
            }
        }

        protected override void LayoutBeforeChildNodes(ref Point location)
        {
            base.LayoutPrefix(ref location);
            base.Container.AddControl(this._checkbox, location);
            location.X += this._checkbox.Width;
        }

        public System.Windows.Forms.CheckBox CheckBox
        {
            get
            {
                return this._checkbox;
            }
        }

        protected override bool HasFormattedValue
        {
            get
            {
                return this._checkbox.Checked;
            }
        }

        protected override int ItemHeight
        {
            get
            {
                return Math.Max(base.ItemHeight, this._checkbox.PreferredSize.Height);
            }
        }

        protected override int PartWidthBeforeChildNodes
        {
            get
            {
                return (base.PostfixWidth + this._checkbox.Width);
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
                if (this._checkbox.Checked)
                {
                    return 1;
                }
                return null;
            }
            set
            {
                this._checkbox.Checked = (value != null) && value.ToString().Equals("1");
            }
        }
    }
}

