namespace HISPlus
{
    public class StaticTextNode : BaseNode
    {
        public StaticTextNode(DocContainer container, DocTemplateElement node) : base(container, node)
        {
            base.HasValue = false;
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

