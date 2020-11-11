namespace HISPlus
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class TotalScoreTextBoxNode : TextBoxNode
    {
        private readonly List<BaseNode> _scoreOptionNodes;

        public TotalScoreTextBoxNode(DocContainer container, DocTemplateElement node)
            : base(container, node, false)
        {
            this._scoreOptionNodes = new List<BaseNode>();
            //base.TxtValue.ReadOnly = true;
            base.TxtValue.BackColor = SystemColors.Window;

            //this._scoreOptionNodes.Clear();
            //this.AddScoreOptionNodes(container.ChildNodes);
        }

        private void AddScoreOptionNodes(IEnumerable<BaseNode> nodes)
        {
            foreach (BaseNode node in nodes)
            {
                if (node is CheckBoxNode)
                {
                    node.ValueChanged += new EventHandler(this.ScoreNode_ValueChanged);
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
            }
            base.TxtValue.Text = num.ToString();
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

