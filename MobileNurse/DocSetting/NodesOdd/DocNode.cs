using System.Collections;

namespace HISPlus
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    /// <summary>
    /// 节点基类
    /// </summary>
    public abstract class BaseNode : IDocNodeParent, IDisposable
    {
        protected DocEditor _container;
        protected List<Control> _controlList;
        private bool _enabled;

        private Label _lblName;
        private Label _lblPost;
        private Label _lblPrev;
        /// <summary>
        /// 控件偏移量
        /// </summary>
        private readonly int _controlOffset;

        protected DocTemplateElement _docNode;
        private bool _visible;

        public event EventHandler ValueChanged;

        private bool _newRow;

        /// <summary>
        /// 是否是新行
        /// </summary>
        public bool NewRow
        {
            get { return _newRow; }
            set { _newRow = value; }
        }

        public BaseNode(DocEditor container, DocTemplateElement docNode)
        {
            this._container = container;
            this._docNode = docNode;
            this.ChildNodes = new DocNodeCollection(this);
            this.PreFix = docNode.ControlPrefix;
            this.PostFix = docNode.ControlSuffix;
            this.NameFont = this.GetFont(docNode.ControlFont);
            this.ContentFont = this.GetFont(docNode.ControlFont);
            this.ChildrenIndent = (int)docNode.ChildrenIndent;

            this._controlOffset = (int)docNode.ControlOffset;
            this._lblName = new Label();
            this._lblName.Text = docNode.DisplayName;
            this._lblName.Font = this.NameFont;
            this._lblName.TextAlign = ContentAlignment.MiddleLeft;
            this._lblPrev = new Label();
            this._lblPrev.Text = this.PreFix;
            this._lblPrev.Font = this.ContentFont;
            this._lblPrev.TextAlign = ContentAlignment.MiddleLeft;
            this._lblPost = new Label();
            this._lblPost.Text = this.PostFix;
            this._lblPost.Font = this.ContentFont;
            this._lblPost.TextAlign = ContentAlignment.MiddleLeft;
            int height = Math.Max(container.DefaultRowHeight, Math.Max(this._lblName.PreferredHeight, Math.Max(this._lblPrev.PreferredHeight, this._lblPost.PreferredHeight)));
            this._lblName.Size = new Size(this._lblName.PreferredWidth, height);
            this._lblPrev.Size = new Size(this._lblPrev.PreferredWidth, height);
            this._lblPost.Size = new Size(this._lblPost.PreferredWidth, height);
            this._controlList = new List<Control>();
            this._controlList.Add(this._lblName);
            this._controlList.Add(this._lblPrev);
            this._controlList.Add(this._lblPost);

            IDocTemplateElement idoc = new DocTemplateElementDAL();

            _newRow = _docNode.NewLine == 1;

            this.ChildNodess = idoc.FindByProperty("ParentElementId", docNode.Id);

            if (this.ChildNodess != null)
            {
                foreach (DocTemplateElement node in this.ChildNodess)
                {
                    BaseNode item = NewDocNode(this._container, node);
                    this.ChildNodes.Add(item);
                }
            }
            switch ((int)this._docNode.DocControlStatus.Id)
            {
                case (int)Enums.ControlStatus.可用:

                    this._visible = true;
                    this._enabled = true;
                    return;
                case (int)Enums.ControlStatus.不可用:

                    this._visible = false;
                    this._enabled = false;
                    return;
                //case DocTemplateElementStatus.Enable:
                //    this._visible = true;
                //    this._enabled = true;
                //    return;

                //case DocTemplateElementStatus.Hide:
                //    this._visible = false;
                //    this._enabled = true;
                //    return;

                //case DocTemplateElementStatus.Disable:
                //    this._visible = true;
                //    this._enabled = false;
                //    return;
            }
        }

        public virtual void Dispose()
        {
            foreach (Control control in this._controlList)
            {
                control.Dispose();
            }
        }

        private Font GetFont(string text)
        {
            if (string.IsNullOrEmpty(text))
                return new Font("宋体", 15);
            string[] strArray = text.Split(new char[] { ',' });
            string familyName = strArray[0].Trim();
            string s = strArray[1].Trim().TrimEnd(new char[] { 'p', 't' });
            if (strArray.Length > 2)
            {
                int startIndex = (strArray[0].Length + strArray[1].Length) + 2;
                return new Font(familyName, float.Parse(s), (FontStyle)Enum.Parse(typeof(FontStyle), text.Substring(startIndex, text.Length - startIndex)));
            }
            return new Font(familyName, float.Parse(s));
        }

        private int GetLayoutWidthCurrentRow(Point prevNodeEndLocation)
        {
            Point point = this._container.GetNextLocation(prevNodeEndLocation, 0, this._docNode.NewLine == 1, this._controlOffset);
            int num = (point.X + this.PartWidthBeforeChildNodes) - prevNodeEndLocation.X;
            if (!NewRow && (this.ChildNodes.Count > 0))
            {
                foreach (BaseNode node in this.ChildNodes)
                {
                    num += node.GetLayoutWidthCurrentRow(new Point(point.X + num, point.Y));
                }
                return num;
            }
            if (this.ChildNodes.Count == 0)
            {
                num += this.GetPartWidthSumAfterChildNodes();
            }
            return num;
        }

        private int GetPartWidthSumAfterChildNodes()
        {
            int partWidthAfterChildNodes = this.PartWidthAfterChildNodes;
            if (this.Parent is BaseNode)
            {
                partWidthAfterChildNodes += (this.Parent as BaseNode).GetPartWidthSumAfterChildNodes();
            }
            return partWidthAfterChildNodes;
        }

        public void Layout()
        {
            this.Layout(false);
        }

        public void Layout(bool relocate)
        {
            //if (!relocate && (this._docNode.formRecord != null))
            //{
            //    if (this._docNode.control.dataType == DocTemplateElementDataType.String)
            //    {
            //        this.Value = this._docNode.formRecord.stringValue;
            //    }
            //    else
            //    {
            //        this.Value = this._docNode.formRecord.numberValue;
            //    }
            //}

            this.Location = this._container.GetNextLocation(this.PrevNodeEndLocation, this.PrevNodeRowHeight, this.NewRow, this._controlOffset);
            if (this.NewRow)
            {
                this.Location = new Point(this.Location.X + this.RowIndent, this.Location.Y);
            }
            foreach (Control control in this._controlList)
            {
                control.Visible = this.Visible;
                control.Enabled = this.Enabled;
            }
            Point location = this.Location;
            location.Offset(this._container.DisplayRectangle.Location);
            this.RowHeight = this.NewRow ? this.ItemHeight : Math.Max(this.PrevNodeRowHeight, this.ItemHeight);
            this.LayoutBeforeChildNodes(ref location);
            if (this.ChildNodes.Count > 0)
            {
                foreach (BaseNode node in this.ChildNodes)
                {
                    node.Layout(relocate);
                }
                location = this.ChildNodes[this.ChildNodes.Count - 1].EndLocation;
                location.Offset(this._container.DisplayRectangle.Location);
            }
            else
            {
                location = new Point(this.Location.X + this.PartWidthBeforeChildNodes, this.Location.Y);
                location.Offset(this._container.DisplayRectangle.Location);
            }
            this.LayoutAfterChildNodes(ref location);
        }

        protected virtual void LayoutAfterChildNodes(ref Point location)
        {
            this.LayoutPostfix(ref location);
        }

        protected virtual void LayoutBeforeChildNodes(ref Point location)
        {
            this.LayoutName(ref location, this.Visible);
            this.LayoutPrefix(ref location);
        }

        protected void LayoutName(ref Point location, bool visible)
        {
            this._lblName.Visible = visible;
            this._container.AddControl(this._lblName, location);
            location.X += this._lblName.Width;
        }

        protected void LayoutPostfix(ref Point location)
        {
            this._container.AddControl(this._lblPost, location);
            location.X += this._lblPost.Width;
        }

        protected void LayoutPrefix(ref Point location)
        {
            this._container.AddControl(this._lblPrev, location);
            location.X += this._lblPrev.Width;
        }

        public static BaseNode NewDocNode(DocEditor container, DocTemplateElement nursingDocNode)
        {
            switch ((int)nursingDocNode.DocControlTemplate.DocControlType.Id)
            {
                case (int)Enums.ControlType.Label:
                    return new StaticTextNode(container, nursingDocNode);

                case (int)Enums.ControlType.TextBox:
                    //if ( nursingDocNode.DocTemplate.TemplateTypeId != 2)
                    //{
                    //    if (nursingDocNode. == DocTemplateElementType.Scale)
                    //    {
                    //        return new ScaleTextBoxNode(container, nursingDocNode);
                    //    }
                    //    if (nursingDocNode.nodeType == DocTemplateElementType.User)
                    //    {
                    //        return new UserTextBoxNode(container, nursingDocNode);
                    //    }
                    //    if (nursingDocNode.nodeType == DocTemplateElementType.Signature)
                    //    {
                    //        return new SignatureTextBoxNode(container, nursingDocNode);
                    //    }
                    return new TextBoxNode(container, nursingDocNode, false);

                //return new TotalScoreTextBoxNode(container, nursingDocNode);

                case (int)Enums.ControlType.TextArea:
                    return new TextBoxNode(container, nursingDocNode, true);

                case (int)Enums.ControlType.SelectSingle:
                    return new CheckListNode(container, nursingDocNode, true);

                //case DocTemplateElementControlType.DropDownBox:
                //    return new ComboBoxNode(container, nursingDocNode);

                //case DocTemplateElementControlType.DateTimePicker:
                //    return new DateTimePickerNode(container, nursingDocNode, "yyyy-MM-dd HH:mm");

                //case DocTemplateElementControlType.DatePicker:
                //    return new DateTimePickerNode(container, nursingDocNode, "yyyy-MM-dd");

                //case DocTemplateElementControlType.TimePicker:
                //    return new DateTimePickerNode(container, nursingDocNode, "HH:mm");

                //case DocTemplateElementControlType.CheckOption:
                //    return new CheckOptionNode(container, nursingDocNode);

                //case DocTemplateElementControlType.Radio:
                //    return new CheckListNode(container, nursingDocNode, false);

                //case DocTemplateElementControlType.ListBox:
                //    return new ListDocNode(container, nursingDocNode);
            }
            return null;
        }

        protected void OnValueChanged(object sender, EventArgs e)
        {
            //if ((this._docNode.nodeActions != null) && (this._docNode.nodeActions.Length > 0))
            //{
            //    foreach (DocTemplateElementAction action in this._docNode.nodeActions)
            //    {
            //        bool flag = (((action.nodeOperator == DocTemplateElementOperator.Equals) && (this.Value != null)) && (this.Value.ToString() == action.conditionValue)) || ((action.nodeOperator == DocTemplateElementOperator.NotEqualTo) && ((this.Value.ToString() != action.conditionValue) || (this.Value == null)));
            //        BaseNode node = this._container.FindNode(action.targetNodeId);
            //        if (node != null)
            //        {
            //            switch (action.targetNodeStatus)
            //            {
            //                case DocTemplateElementStatus.Show:
            //                    node.Visible = flag;
            //                    break;

            //                case DocTemplateElementStatus.Hide:
            //                    node.Visible = !flag;
            //                    break;

            //                case DocTemplateElementStatus.Enable:
            //                    node.Enabled = flag;
            //                    break;

            //                case DocTemplateElementStatus.Disable:
            //                    node.Enabled = !flag;
            //                    break;
            //            }
            //        }
            //    }
            //}
            //if (this.ValueChanged != null)
            //{
            //    this.ValueChanged(this, new EventArgs());
            //}
        }

        public void RelocateChildNodes(int startChildIndex)
        {
            this._container.SuspendLayout();
            try
            {
                Point endLocation;
                foreach (Control control in this._controlList)
                {
                    control.Visible = this.Visible;
                    control.Enabled = this.Enabled;
                }
                for (int i = startChildIndex; i < this.ChildNodes.Count; i++)
                {
                    //this.ChildNodes[i].Layout(true);
                }
                if (this.ChildNodes.Count > 0)
                {
                    endLocation = this.ChildNodes[this.ChildNodes.Count - 1].EndLocation;
                }
                else
                {
                    endLocation = new Point(this.Location.X + this.PartWidthBeforeChildNodes, this.Location.Y);
                }
                this.LayoutAfterChildNodes(ref endLocation);
                this.Parent.RelocateChildNodes(this.Index + 1);
            }
            finally
            {
                this._container.ResumeLayout();
            }
        }

        public virtual void Remove()
        {
            while (this.ChildNodes.Count > 0)
            {
                this.ChildNodes[0].Remove();
            }
            this.Parent.ChildNodes.Remove(this);
            foreach (Control control in this._controlList)
            {
                if (this._container.Controls.Contains(control))
                {
                    this._container.Controls.Remove(control);
                }
            }
        }

        private IList<DocTemplateElement> ChildNodess { get; set; }

        public DocNodeCollection ChildNodes { get; private set; }

        /// <summary>
        /// 子项缩进
        /// </summary>
        public int ChildrenIndent { get; private set; }

        protected Font ContentFont { get; private set; }

        public virtual bool Enabled
        {
            get
            {
                if ((this.Parent is BaseNode) && !(this.Parent as BaseNode).Enabled)
                {
                    return false;
                }
                return this._enabled;
            }
            set
            {
                bool enabled = this.Enabled;
                this._enabled = value;
                if (enabled != this.Enabled)
                {
                    this.Parent.RelocateChildNodes(this.Index);
                }
            }
        }

        private Point EndLocation
        {
            get
            {
                if (this.LastVisibleChildNode == null)
                {
                    return new Point((this.Location.X + this.PartWidthBeforeChildNodes) + this.PartWidthAfterChildNodes, this.Location.Y);
                }
                return new Point(this.LastVisibleChildNode.EndLocation.X + this.PartWidthAfterChildNodes, this.LastVisibleChildNode.EndLocation.Y);
            }
        }

        public string FormattedValue
        {
            get
            {
                //if (!this.Visible || !this.Enabled)
                //{
                //    return string.Empty;
                //}
                //string str = string.Empty;
                //foreach (BaseNode node in this.ChildNodes)
                //{
                //    if (!string.IsNullOrEmpty(node.FormattedValue))
                //    {
                //        if (!string.IsNullOrEmpty(str))
                //        {
                //            str = str + " ";
                //        }
                //        str = str + node.FormattedValue;
                //    }
                //}
                //if (!this.HasFormattedValue && string.IsNullOrEmpty(str))
                //{
                //    return null;
                //}
                //return (this._docNode + this.SelfFormattedValue + str + this._docNode.strNext);
                return "123";
            }
        }

        protected virtual bool HasFormattedValue
        {
            get
            {
                return !string.IsNullOrEmpty(this.SelfFormattedValue);
            }
        }

        public bool HasValue { get; protected set; }

        public int Index
        {
            get
            {
                return this.Parent.ChildNodes.IndexOf(this);
            }
        }

        public bool IsInList
        {
            get
            {
                //return ((this is ListDocNode) || ((this.Parent is BaseNode) && (this.Parent as BaseNode).IsInList));
                return false;
            }
        }

        protected virtual int ItemHeight
        {
            get
            {
                return this._lblName.Height;
            }
        }

        private BaseNode LastVisibleChildNode
        {
            get
            {
                for (int i = this.ChildNodes.Count - 1; i >= 0; i--)
                {
                    if (this.ChildNodes[i].Visible)
                    {
                        return this.ChildNodes[i];
                    }
                }
                return null;
            }
        }

        private int LastVisibleChildRowHeight
        {
            get
            {
                if (this.LastVisibleChildNode != null)
                {
                    return this.LastVisibleChildNode.LastVisibleChildRowHeight;
                }
                return this.RowHeight;
            }
        }

        private Point Location { get; set; }

        protected Font NameFont { get; private set; }

        protected int NameWidth
        {
            get
            {
                return this._lblName.Width;
            }
        }

        public DocTemplateElement NursingDocNode
        {
            get
            {
                return this._docNode;
            }
        }

        public IDocNodeParent Parent { get; set; }

        protected virtual int PartWidthAfterChildNodes
        {
            get
            {
                return this.PostfixWidth;
            }
        }

        protected virtual int PartWidthBeforeChildNodes
        {
            get
            {
                return (this.NameWidth + this.PrefixWidth);
            }
        }

        public string PostFix { get; private set; }

        protected int PostfixWidth
        {
            get
            {
                return this._lblPost.Width;
            }
        }

        private int PostWidth
        {
            get
            {
                int width = this._lblPost.Width;
                if (this.Parent is BaseNode)
                {
                    width += (this.Parent as BaseNode).PostWidth;
                }
                return width;
            }
        }

        public string PreFix { get; private set; }

        protected int PrefixWidth
        {
            get
            {
                return this._lblPrev.Width;
            }
        }

        private Point PrevNodeEndLocation
        {
            get
            {
                if (this.PrevVisibleNode == null)
                {
                    return new Point(this._container.Padding.Left, this._container.Padding.Top);
                }
                bool flag = false;
                for (BaseNode node = this; node.Parent is BaseNode; node = node.Parent as BaseNode)
                {
                    if (this.Parent.Equals(this.PrevVisibleNode))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    return this.PrevVisibleNode.EndLocation;
                }
                return new Point(this.PrevVisibleNode.Location.X + this.PrevVisibleNode.PartWidthBeforeChildNodes, this.PrevVisibleNode.Location.Y);
            }
        }

        private int PrevNodeRowHeight
        {
            get
            {
                if (this.PrevVisibleNode == null)
                {
                    return 0;
                }
                bool flag = false;
                for (BaseNode node = this; node.Parent is BaseNode; node = node.Parent as BaseNode)
                {
                    if (this.Parent.Equals(this.PrevVisibleNode))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    return this.PrevVisibleNode.LastVisibleChildRowHeight;
                }
                return this.PrevVisibleNode.RowHeight;
            }
        }

        private BaseNode PrevVisibleNode
        {
            get
            {
                if (this.Index == 0)
                {
                    if (!(this.Parent is BaseNode))
                    {
                        return null;
                    }
                    if ((this.Parent as BaseNode).Visible)
                    {
                        return (this.Parent as BaseNode);
                    }
                    return (this.Parent as BaseNode).PrevVisibleNode;
                }
                if (this.Parent.ChildNodes[this.Index - 1].Visible)
                {
                    return this.Parent.ChildNodes[this.Index - 1];
                }
                return this.Parent.ChildNodes[this.Index - 1].PrevVisibleNode;
            }
        }

        private int RowHeight { get; set; }

        public int RowIndent
        {
            get
            {
                if (this.Parent is DocEditor)
                {
                    return 0;
                }
                return ((this.Parent as BaseNode).RowIndent + (this.Parent as BaseNode).ChildrenIndent);
            }
        }

        public abstract string SelfFormattedValue { get; }

        public abstract object Value { get; set; }

        public bool Visible
        {
            get
            {
                if ((this.Parent is BaseNode) && !(this.Parent as BaseNode).Visible)
                {
                    return false;
                }
                return this._visible;
            }
            set
            {
                bool visible = this.Visible;
                if (this._visible != value)
                {
                    this._visible = value;
                    if (visible != this.Visible)
                    {
                        this._container.SuspendLayout();
                        try
                        {
                            this.Parent.RelocateChildNodes(this.Index);
                        }
                        finally
                        {
                            this._container.ResumeLayout();
                        }
                    }
                }
            }
        }
    }
}

