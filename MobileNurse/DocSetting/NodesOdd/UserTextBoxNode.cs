namespace HISPlus
{   
    using System;
    using System.Windows.Forms;

    public class UserTextBoxNode : TextBoxNode
    {
        public UserTextBoxNode(DocContainer container, DocTemplateElement node) : base(container, node, false)
        {
            base.TxtValue.KeyPress += new KeyPressEventHandler(this._txtValue_KeyPress);
        }

        private void _txtValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (!string.IsNullOrEmpty(base.TxtValue.Text))
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        //WSUser userByCode = WSController.GetUserByCode(base._txtValue.Text);
                        //if (userByCode != null)
                        //{
                        //    base._txtValue.Text = userByCode.userName;
                        //}
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
                e.Handled = true;
            }
        }
    }
}

