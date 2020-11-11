using DevExpress.XtraEditors;

namespace HISPlus
{
    using System;

    public class CheckListNode : BaseNode
    {
        public readonly bool MultiChecked;

        public CheckListNode(DesignTemplate container, DocTemplateElement nursingDocNode, bool multiChecked)
            : base(container, nursingDocNode)
        {
            base.HasValue = false;
            this.MultiChecked = multiChecked;
            if (this.MultiChecked) return;
            foreach (BaseNode node in base.ChildNodes)
            {
                if (node is CheckBoxNode)
                {
                    (node as CheckBoxNode).ParentNode = this;
                    (node as CheckBoxNode).CheckBox.CheckedChanged += this.CheckBox_CheckedChanged;
                }
            }
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit box = sender as CheckEdit;
            if (box != null && box.Checked)
            {
                foreach (BaseNode node in base.ChildNodes)
                {
                    if ((node is CheckBoxNode) && !(node as CheckBoxNode).CheckBox.Equals(box))
                    {
                        (node as CheckBoxNode).CheckBox.Checked = false;
                    }
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

