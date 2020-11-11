using DevExpress.XtraEditors;

namespace HISPlus
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class TextAreaNode : BaseNode
    {
        /// <summary>
        /// 文本框
        /// </summary>
        private readonly MemoEdit _txtValue;
        //private VariousDataSourcePanel _variousDataSourcePanel;

        public TextAreaNode(DesignTemplate container, DocTemplateElement node)
            : base(container, node)
        {
            this._txtValue = new MemoEdit { Font = ContentFont };

            if (ControlWidth > 0)
                this._txtValue.Width = ControlWidth;
            if (ControlHeight > 0)
                this._txtValue.Height = ControlHeight;

            if ((node.DataType == (int)Enums.DataType.浮点数) || (node.DataType == (int)Enums.DataType.整数))
            {
                //this.TxtValue.TextAlign = HorizontalAlignment.Right;
            }
            ControlList.Add(this._txtValue);
            //_txtValue
            this._txtValue.Validated += this._txtValue_Validated;
            this._txtValue.Enter += this._txtValue_Enter;
            this._txtValue.Leave += this._txtValue_Leave;
            //this._txtValue.KeyPress += this._txtValue_KeyPress;
            //if ((node.variousDataSource != null) && (node.variousDataSource.Length > 0))
            //{
            //    this._variousDataSourcePanel = new VariousDataSourcePanel(this._txtValue, node.variousDataSource, base._container.Patient);
            //}
        }

        private void _txtValue_Enter(object sender, EventArgs e)
        {
          

            //if (this._variousDataSourcePanel != null)
            //{
            //if (!base._container.Controls.Contains(this._variousDataSourcePanel))
            //{
            //    base._container.Controls.Add(this._variousDataSourcePanel);
            //}
            //    this._variousDataSourcePanel.Location = new Point(this._txtValue.Left, ((this._txtValue.Top - this._txtValue.Margin.Top) - this._variousDataSourcePanel.Margin.Bottom) - this._variousDataSourcePanel.Height);
            //    if (this._variousDataSourcePanel.Top < 0)
            //    {
            //        this._variousDataSourcePanel.Top = (this._txtValue.Bottom + this._txtValue.Margin.Bottom) + this._variousDataSourcePanel.Margin.Top;
            //    }
            //    this._variousDataSourcePanel.Visible = true;
            //    this._variousDataSourcePanel.BringToFront();
            //}
        }

        private void _txtValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this._txtValue.Text = this._txtValue.Text.Insert(_txtValue.SelectionStart, Environment.NewLine);

                //this._txtValue.SelectionStart = this._txtValue.Text.Length;

                //Container.SelectNextControl(sender as Control, true, true, true, true);
                //e.Handled = true;
            }
        }

        private void _txtValue_Leave(object sender, EventArgs e)
        {
            //if ((this._variousDataSourcePanel != null) && !this._variousDataSourcePanel.Focused)
            //{
            //    this._variousDataSourcePanel.Visible = false;
            //}
        }

        private void _txtValue_Validated(object sender, EventArgs e)
        {
            base.OnValueChanged(this, e);
        }

        protected override void LayoutBeforeChildNodes(ref Point location)
        {
            base.LayoutBeforeChildNodes(ref location);
            //base.Container.AddControl(this._txtValue, new Point(location.X, location.Y + Math.Max(0, (base.ItemHeight - this._txtValue.Height) / 2)));
            base.Container.AddControl(this._txtValue, new Point(location.X, location.Y-3 + (int)NursingDocNode.RowSpacing));
            location.X += this._txtValue.Width;
            //location.Y += 3;
        }

        protected override int ItemHeight
        {
            get
            {
                return Math.Max(base.ItemHeight, this._txtValue.Height)+5;
            }
        }

        protected override int PartWidthBeforeChildNodes
        {
            get
            {
                return base.PartWidthBeforeChildNodes + this._txtValue.Width;
            }
        }

        public override string SelfFormattedValue
        {
            get
            {
                return this._txtValue.Text;
            }
        }

        public override object Value
        {
            get
            {
                return string.IsNullOrEmpty(this._txtValue.Text) ? null : this._txtValue.Text;
            }
            set
            {
                this._txtValue.Text = value == null ? null : value.ToString();
            }
        }
    }
}

