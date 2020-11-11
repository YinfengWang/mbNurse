using DevExpress.XtraEditors;

namespace HISPlus
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class TotalScoreTextBoxNode : TextBoxNode
    {
        private readonly List<BaseNode> _scoreOptionNodes;

        public TotalScoreTextBoxNode(DesignTemplate container, DocTemplateElement node)
            : base(container, node)
        {
            this._scoreOptionNodes = new List<BaseNode>();
            base.TxtControl.Properties.ReadOnly = false;// true -> false
            base.TxtControl.EditValue = "0";//2015.12.15 add
            base.TxtControl.BackColor = SystemColors.Window;

            //this._scoreOptionNodes.Clear();
            //this.AddScoreOptionNodes(container.ChildNodes);
        }

        private void AddScoreOptionNodes(IEnumerable<BaseNode> nodes)
        {
            foreach (BaseNode node in nodes)
            {
                if ((node is CheckBoxNode || node is TextBoxNode) && node.NursingDocNode.Score > 0)
                {
                    node.ValueChanged += this.ScoreNode_ValueChanged;
                    this._scoreOptionNodes.Add(node);
                }
                this.AddScoreOptionNodes(node.ChildNodes);
            }
        }

        private void RefreshTotalScore()
        {
            float num = 0;
            foreach (BaseNode node in this._scoreOptionNodes)
            {
                if (((node is CheckBoxNode) && (node.Value != null)) && (node.Value.ToString() == "1"))
                {
                    num += node.NursingDocNode.Score;
                }
                if (((node is TextBoxNode) && (node.Value != null)))
                {
                    float f = 0;
                    float.TryParse(node.Value.ToString(), out f);
                    num += f;
                }
            }
            base.TxtControl.Text = num.ToString();
        }

        private void ScoreNode_ValueChanged(object sender, EventArgs e)
        {
            this.RefreshTotalScore();
        }

        public void SetReferenceNode(IEnumerable<BaseNode> nodes)
        {
            this._scoreOptionNodes.Clear();
            this.AddScoreOptionNodes(nodes);
        }
    }
}

