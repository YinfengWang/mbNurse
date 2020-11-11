namespace HISPlus
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class TextBoxNode : BaseNode
    {
        /// <summary>
        /// 文本框
        /// </summary>
        protected readonly TextBox TxtValue;
        //private VariousDataSourcePanel _variousDataSourcePanel;

        public TextBoxNode(DocContainer container, DocTemplateElement node, bool multiLine) : base(container, node)
        {
            this.TxtValue = new TextBox {Font = ContentFont, Multiline = multiLine};

            if(node.ControlWidth>0)
            this.TxtValue.Width = (int)node.ControlWidth;
            if (node.ControlHeight > 0)
                this.TxtValue.Height = (int)node.ControlHeight;

            if ((node.DataType == (int)Enums.DataType.浮点数) || (node.DataType == (int)Enums.DataType.整数))
            {
                this.TxtValue.TextAlign = HorizontalAlignment.Right;
            }
            base.ControlList.Add(this.TxtValue);
            this.TxtValue.Validated += new EventHandler(this._txtValue_Validated);
            this.TxtValue.Enter += new EventHandler(this._txtValue_Enter);
            this.TxtValue.Leave += new EventHandler(this._txtValue_Leave);
            this.TxtValue.KeyPress += new KeyPressEventHandler(this._txtValue_KeyPress);
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
                base.Container.SelectNextControl(sender as Control, true, true, true, true);
                e.Handled = true;
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
            base.Container.AddControl(this.TxtValue, new Point(location.X, location.Y + Math.Max(0, (base.ItemHeight - this.TxtValue.Height) / 2)));
            location.X += this.TxtValue.Width;
        }

        protected override int ItemHeight
        {
            get
            {
                return Math.Max(base.ItemHeight, this.TxtValue.Height);
            }
        }

        protected override int PartWidthBeforeChildNodes
        {
            get
            {                
                return base.PartWidthBeforeChildNodes + this.TxtValue.Width;
            }
        }

        public override string SelfFormattedValue
        {
            get
            {
                return this.TxtValue.Text;
            }
        }

        public override object Value
        {
            get
            {
                if (!string.IsNullOrEmpty(this.TxtValue.Text))
                {
                    return this.TxtValue.Text;
                }
                return null;
            }
            set
            {
                if (value == null)
                {
                    this.TxtValue.Text = null;
                }
                else
                {
                    this.TxtValue.Text = value.ToString();
                }
            }
        }
    }
}

