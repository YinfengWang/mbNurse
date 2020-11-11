using DevExpress.XtraEditors;

namespace HISPlus
{
    /// <summary>
    /// 签名文本框
    /// </summary>
    public class SignTextBoxNode : TextBoxNode
    {
        public SignTextBoxNode(DesignTemplate container, DocTemplateElement node)
            : base(container, node)
        {
            base.TxtControl.Text = GVars.User.Name;
            if(TxtControl != null)
                TxtControl.Properties.ReadOnly = true;
            //base.txtControl.Properties.ReadOnly = true;
        }
    }
}

