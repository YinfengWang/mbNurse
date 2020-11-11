using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

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
        //protected readonly TextEdit TxtValue;
        //private VariousDataSourcePanel _variousDataSourcePanel;        

        /// <summary>
        /// 后缀(Label)
        /// </summary>
        private readonly LabelControl _lblPostFix;
        public readonly TextEdit TxtControl;

        public TextBoxNode(DesignTemplate container, DocTemplateElement node)
            : base(container, node)
        {
            TxtControl = node.Score > 0 ? new SpinEdit() : new TextEdit();

            this.TxtControl.Tag = this;
            TxtControl.Font = ContentFont;

            if (ControlWidth > 0)
            {
                TxtControl.Width = ControlWidth;
            }
            if (ControlHeight > 0)
            {
                this.TxtControl.Height = ControlHeight;
            }

            if ((node.DataType == (int)Enums.DataType.浮点数) || (node.DataType == (int)Enums.DataType.整数))
            {
                //this.TxtValue.TextAlign = HorizontalAlignment.Right;
            }

            //TxtValue.Properties.Appearance.TextOptions.HAlignment
            //    = DevExpress.Utils.HorzAlignment.Near;
            //TxtValue.Properties.Appearance.TextOptions.VAlignment
            //    = DevExpress.Utils.VertAlignment.Top;

            ControlList.Add(this.TxtControl);

            if (node.Score > 0)
            {
                SpinEdit edit = TxtControl as SpinEdit;

                if (edit != null)
                {
                    edit.Properties.MinValue = 0;
                    edit.Properties.MaxValue = (decimal)node.Score;
                    edit.Properties.MaxLength = 3;
                    //edit.Properties.SpinStyle = SpinStyles.Vertical;                
                    edit.Properties.IsFloatValue = true;
                    //上下箭头步长
                    edit.Properties.Increment = 1;
                    //edit.TextChanged += edit_TextChanged;
                    edit.KeyUp += edit_KeyUp;
                }
                if (ShowScore)
                {
                    _lblPostFix = new LabelControl();
                    _lblPostFix.Name = "_lblPostFix";
                    _lblPostFix.Font = ContentFont;
                    _lblPostFix.Text = string.Format("({0}分)", node.Score);//2015.11.09 del
                    _lblPostFix.Appearance.TextOptions.HAlignment
                        = DevExpress.Utils.HorzAlignment.Near;
                    _lblPostFix.Appearance.TextOptions.VAlignment
                        = DevExpress.Utils.VertAlignment.Bottom;
                    _lblPostFix.Size = new Size(this._lblPostFix.PreferredSize.Width, this.TxtControl.Height);
                    ControlList.Add(this._lblPostFix);//2015.11.09 del
                }
            }

            this.TxtControl.Validated += this._txtValue_Validated;
            this.TxtControl.Enter += this._txtValue_Enter;
            this.TxtControl.Leave += this._txtValue_Leave;
            this.TxtControl.KeyPress += this._txtValue_KeyPress;
            //if ((node.variousDataSource != null) && (node.variousDataSource.Length > 0))
            //{
            //    this._variousDataSourcePanel = new VariousDataSourcePanel(this._txtValue, node.variousDataSource, base._container.Patient);
            //}
        }

        void edit_KeyUp(object sender, KeyEventArgs e)
        {
            SpinEdit edit = sender as SpinEdit;
            if (edit == null || edit.Value <= edit.Properties.MaxValue) return;
            edit.Value = edit.Properties.MaxValue;
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
                e.KeyChar = (char)Keys.Tab;
                //Container.SelectNextControl(sender as Control, true, true, true, true);
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
            //base.Container.AddControl(this.TxtValue, new Point(location.X, location.Y + Math.Max(0, (base.ItemHeight - this.TxtValue.Height) / 2)));
            // 因为文本框比Label标签要高，所以为了对齐，此处高度-3
            if (IsInGrid)
            {
                this.Container.AddControl(this.TxtControl,
                    new Point(GridLocation.X + base.PartWidthBeforeChildNodes + (int)this.NursingDocNode.ControlOffset,
                        GridLocation.Y - 3 + (int)NursingDocNode.RowSpacing));
            }
            else
            {
                base.Container.AddControl(this.TxtControl,
                    new Point(location.X + DesignTemplate.SpaceNameControl,
                        location.Y - 3 + (int)NursingDocNode.RowSpacing));
                location.X += this.TxtControl.Width + DesignTemplate.SpaceNameControl;
                //location.Y+=(int)NursingDocNode.RowSpacing;
            }
            //base.Container.AddControl(this.txtControl, new Point(location.X, location.Y - 3));
            //location.X += this.txtControl.Width;
            if (!IsInGrid) //2015.11.09 debug 表格内暂不绘制后缀
            if (_lblPostFix != null)
            {
                Container.AddControl(this._lblPostFix, new Point(location.X, location.Y + (int)NursingDocNode.RowSpacing));
                location.X += this._lblPostFix.Width;
            }
            //base.Container.AddControl(this.TxtValue, new Point(location.X , location.Y ));
            //location.X += this.TxtValue.Width ;
        }

        protected override int ItemHeight
        {
            get
            {
                return Math.Max(base.ItemHeight, this.TxtControl.Height);
            }
        }

        protected override int PartWidthBeforeChildNodes
        {
            get
            {
                return base.PartWidthBeforeChildNodes + this.TxtControl.Width + (this._lblPostFix == null ? 0 : this._lblPostFix.Width);
            }
        }

        public override string SelfFormattedValue
        {
            get
            {
                return this.TxtControl.Text;
            }
        }

        public override object Value
        {
            get
            {
                if (!string.IsNullOrEmpty(this.TxtControl.Text))
                {
                    return this.TxtControl.Text;
                }
                return null;
            }
            set
            {
                if (value == null)
                {
                    this.TxtControl.Text = null;
                }
                else
                {
                    this.TxtControl.Text = value.ToString();
                }
            }
        }
    }
}

