namespace HISPlus
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class DateTimePickerNode : BaseNode
    {
        private DateTimePicker _dateTimePicker;

        public DateTimePickerNode(DocContainer container, DocTemplateElement node, string format)
            : base(container, node)
        {
            this._dateTimePicker = new DateTimePicker();
            this._dateTimePicker.Format = DateTimePickerFormat.Custom;
            this._dateTimePicker.CustomFormat = format;
            this._dateTimePicker.Font = base.ContentFont;
            this._dateTimePicker.ShowCheckBox = true;
            this._dateTimePicker.Checked = false;
            if (node.ControlWidth > 0)
                this._dateTimePicker.Width = (int)node.ControlWidth;
            base.ControlList.Add(this._dateTimePicker);
            this._dateTimePicker.KeyPress += new KeyPressEventHandler(this._dateTimePicker_KeyPress);
        }

        private void _dateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                base.Container.SelectNextControl(sender as Control, true, true, true, true);
                e.Handled = true;
            }
        }

        protected override void LayoutBeforeChildNodes(ref Point location)
        {
            base.LayoutBeforeChildNodes(ref location);
            base.Container.AddControl(this._dateTimePicker, new Point(location.X, location.Y + Math.Max(0, (base.ItemHeight - this._dateTimePicker.Height) / 2)));
            location.X += this._dateTimePicker.Width;
        }

        protected override int ItemHeight
        {
            get
            {
                return Math.Max(base.ItemHeight, this._dateTimePicker.Height);
            }
        }

        protected override int PartWidthBeforeChildNodes
        {
            get
            {
                return (base.PartWidthBeforeChildNodes + this._dateTimePicker.Width);
            }
        }

        public override string SelfFormattedValue
        {
            get
            {
                if (this._dateTimePicker.Checked)
                {
                    return this._dateTimePicker.Value.ToString(this._dateTimePicker.CustomFormat);
                }
                return null;
            }
        }

        public override object Value
        {
            get
            {
                if (this._dateTimePicker.Checked)
                {
                    return this._dateTimePicker.Value;
                }
                return null;
            }
            set
            {
                if (string.IsNullOrEmpty(value as string))
                {
                    this._dateTimePicker.Value = DateTime.Now;
                    this._dateTimePicker.Checked = false;
                }
                else
                {
                    this._dateTimePicker.Value = DateTime.Parse(value as string);
                    this._dateTimePicker.Checked = true;
                }
            }
        }
    }
}

