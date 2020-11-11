namespace HISPlus
{
    public class SignatureTextBoxNode : TextBoxNode
    {
        public SignatureTextBoxNode(DocContainer container, DocTemplateElement node) : base(container, node, false)
        {
            //base._txtValue.Text = (Global.User == null) ? string.Empty : Global.User.userName;
            //base.TxtValue.ReadOnly = true;
        }
    }
}

