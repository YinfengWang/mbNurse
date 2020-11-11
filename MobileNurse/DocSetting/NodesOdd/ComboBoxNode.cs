namespace HISPlus
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class ComboBoxNode : BaseNode
    {
        private ComboBox _comboBox;

        public ComboBoxNode(DocContainer container, DocTemplateElement node) : base(container, node)
        {
            this._comboBox = new ComboBox();
            this._comboBox.Font = base.ContentFont;
            this._comboBox.DropDownStyle = ComboBoxStyle.DropDown;
            this._comboBox.Width =(int) node.ControlWidth;
            //if (!string.IsNullOrEmpty(node.control.enumValues))
            //{
            //    string[] items = node.control.enumValues.Split(new char[] { ',' });
            //    this._comboBox.Items.AddRange(items);
            //}
            base.ControlList.Add(this._comboBox);
            this._comboBox.SelectedIndexChanged += new EventHandler(this._comboBox_SelectedIndexChanged);
            this._comboBox.KeyPress += new KeyPressEventHandler(this._comboBox_KeyPress);
        }

        private void _comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                base.Container.SelectNextControl(sender as Control, true, true, true, true);
                e.Handled = true;
            }
        }

        private void _comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.OnValueChanged(this, e);
        }

        protected override void LayoutBeforeChildNodes(ref Point location)
        {
            base.LayoutBeforeChildNodes(ref location);
            base.Container.AddControl(this._comboBox, new Point(location.X, location.Y + Math.Max(0, (base.ItemHeight - this._comboBox.Height) / 2)));
            location.X += this._comboBox.Width;
        }

        protected override int ItemHeight
        {
            get
            {
                return Math.Max(base.ItemHeight, this._comboBox.Height);
            }
        }

        protected override int PartWidthBeforeChildNodes
        {
            get
            {
                return (base.PartWidthBeforeChildNodes + this._comboBox.Width);
            }
        }

        public override string SelfFormattedValue
        {
            get
            {
                return this._comboBox.Text;
            }
        }

        public override object Value
        {
            get
            {
                return this._comboBox.Text;
            }
            set
            {
                this._comboBox.Text = value as string;
            }
        }
    }
}

