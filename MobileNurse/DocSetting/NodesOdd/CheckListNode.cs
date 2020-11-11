namespace HISPlus
{
    using System;
    using System.Windows.Forms;

    public class CheckListNode : BaseNode
    {
        public CheckListNode(DocContainer container, DocTemplateElement nursingDocNode, bool multiChecked)
            : base(container, nursingDocNode)
        {
            base.HasValue = false;
            if (!multiChecked)
            {
                foreach (BaseNode node in base.ChildNodes)
                {
                    if (node is CheckBoxNode)
                    {
                        (node as CheckBoxNode).CheckBox.CheckedChanged += this.CheckedChanged;
                    }
                }
            }
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = sender as CheckBox;
            if (box == null || !box.Checked) return;

            foreach (BaseNode node in ChildNodes)
            {
                if ((node is CheckBoxNode) && !(node as CheckBoxNode).CheckBox.Equals(box))
                {
                    (node as CheckBoxNode).CheckBox.Checked = false;
                }
            }
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

