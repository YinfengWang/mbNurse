using System.Drawing;

namespace HISPlus
{
    public class StaticTextNode : BaseNode
    {
        public StaticTextNode(DesignTemplate container, DocTemplateElement node) : base(container, node)
        {
            base.HasValue = false;
        }

        public override void Dispose()
        {
        }

        //protected override void LayoutBeforeChildNodes(ref Point location)
        //{
        //    LayoutPrefix(ref location);
        //    LayoutName(ref location, this.Visible);
        //    Container.AddControl(lbl, location);
        //    location.X += this._checkEdit.Width;
        //    if (_lblPostFix != null)
        //    {
        //        Container.AddControl(this._lblPostFix, new Point(location.X, location.Y + 3));
        //        location.X += this._lblPostFix.Width;
        //    }
        //}

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

