using System;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace HISPlus
{
    /// <summary>
    /// 分隔线
    /// </summary>
    public class SplitLineNode : BaseNode
    {
        private readonly PanelControl _line;
        public SplitLineNode(DesignTemplate container, DocTemplateElement node)
            : base(container, node)
        {
            base.HasValue = false;

            _line = new PanelControl();
            _line.Height = 2;
            _line.Width = Container.LayoutWidth;
            _line.BorderStyle = BorderStyles.Style3D;
            //if (isHorizontal)
            //{
            //    line.Height = 2;
            //    line.Width = container.LayoutWidth;
            //}
            //else
            //{
            //    line.Height = container.Height;
            //    line.Width = 2;
            //}

            ControlList.Add(this._line);
        }

        protected override void LayoutBeforeChildNodes(ref Point location)
        {
            LayoutPrefix(ref location);
            //Container.AddControl(this._line, new Point(location.X, location.Y + (DesignTemplate.DefaultRowHeight / 2)));
            Container.AddControl(this._line, new Point(this.Container.Border, location.Y + (DesignTemplate.DefaultRowHeight / 2)));

            location.X += this._line.Width;
        }

        protected override int ItemHeight
        {
            get
            {
                return Math.Max(base.ItemHeight, this._line.Height);
            }
        }

        protected override int PartWidthBeforeChildNodes
        {
            get
            {
                return (PostfixWidth + this._line.Width);
            }
        }

        public override void Dispose()
        {
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
                return null;
            }
            set
            {
            }
        }
    }
}


