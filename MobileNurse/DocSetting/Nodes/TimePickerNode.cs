using DevExpress.XtraEditors;

namespace HISPlus
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class TimePickerNode : BaseNode
    {
        private readonly TimeEdit _dateTimePicker;

        public TimePickerNode(DesignTemplate container, DocTemplateElement node)
            : base(container, node)
        {

            this._dateTimePicker = new TimeEdit();

            this._dateTimePicker.Font = base.ContentFont;

            this._dateTimePicker.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this._dateTimePicker.Properties.EditMask = "HH:mm";
            this._dateTimePicker.Properties.DisplayFormat.FormatString = "HH:mm";
            this._dateTimePicker.Properties.EditFormat.FormatString = "HH:mm";

            _dateTimePicker.Properties.Appearance.TextOptions.HAlignment
                = DevExpress.Utils.HorzAlignment.Near;
            _dateTimePicker.Properties.Appearance.TextOptions.VAlignment
                = DevExpress.Utils.VertAlignment.Top;

            //this._dateTimePicker.Size = new Size(Math.Min(this._dateTimePicker.PreferredSize.Width, this.Container.LayoutWidth), height);

            if (node.ControlWidth > 0)
                this._dateTimePicker.Width = (int)node.ControlWidth;
            //else
            //    this._dateTimePicker.Width = this._dateTimePicker.Width + 3;
            base.ControlList.Add(this._dateTimePicker);
            this._dateTimePicker.KeyPress += this._dateTimePicker_KeyPress;
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
            base.Container.AddControl(this._dateTimePicker, new Point(location.X + DesignTemplate.SpaceNameControl, location.Y - 3 + (int)NursingDocNode.RowSpacing));
            // + Math.Max(0, (base.ItemHeight - this._dateTimePicker.Height) / 2)
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
                return
                    Convert.ToDateTime(this._dateTimePicker.EditValue)
                        .ToString(this._dateTimePicker.Properties.DisplayFormat.FormatString);
            }
        }

        public override object Value
        {
            get
            {
                return _dateTimePicker.EditValue;
            }
            set
            {
                if (string.IsNullOrEmpty(value as string))
                {
                    this._dateTimePicker.EditValue = GVars.OracleAccess.GetSysDate().TimeOfDay;
                }
                else
                {
                    DateTime d;
                    this._dateTimePicker.EditValue = (DateTime.TryParse(value as string, out d) ? d : GVars.OracleAccess.GetSysDate()).TimeOfDay;
                }
            }
        }
    }
}

