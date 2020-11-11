using System.Collections;
using System.Data;
using System.Linq;
using DocSetting.Annotations;

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
        /// <summary>
        /// 控件容器
        /// </summary>
        protected readonly DocContainer Container;

        /// <summary>
        /// 控件列表
        /// </summary>
        protected readonly List<Control> ControlList;

        private bool _enabled;

        /// <summary>
        /// 显示名(Label)
        /// </summary>
        private readonly Label _lblName;

        /// <summary>
        /// 后缀(Label)
        /// </summary>
        private readonly Label _lblPostFix;

        /// <summary>
        /// 前缀(Label)
        /// </summary>
        private readonly Label _lblPreFix;

        /// <summary>
        /// 控件偏移量
        /// </summary>
        private readonly int _controlOffset;

        private readonly DocTemplateElement _docNode;

        private bool _visible;

        public event EventHandler ValueChanged;

        /// <summary>
        /// 是否是新行
        /// </summary>
        private bool NewRow { get; set; }

        protected BaseNode(DocContainer container, DocTemplateElement docNode)
        {
            this.Container = container;
            this._docNode = docNode;
            this.ChildNodes = new DocNodeCollection(this);
            this.PreFix = docNode.ControlPrefix;
            this.PostFix = docNode.ControlSuffix;
            this.NameFont = DocCommon.FontFromString(docNode.ControlFont);
            this.ContentFont = DocCommon.FontFromString(docNode.ControlFont);
            this.ChildrenIndent = (int)docNode.ChildrenIndent;

            this._controlOffset = (int)docNode.ControlOffset;

            this._lblName = new Label
            {
                Text = docNode.DisplayName,
                Font = this.NameFont,
                TextAlign = ContentAlignment.MiddleLeft
            };

            this._lblPreFix = new Label
            {
                Text = this.PreFix,
                Font = this.ContentFont,
                TextAlign = ContentAlignment.MiddleLeft
            };

            this._lblPostFix = new Label
            {
                Text = this.PostFix,
                Font = this.ContentFont,
                TextAlign = ContentAlignment.MiddleLeft
            };

            int height = Math.Max(container.DefaultRowHeight, Math.Max(this._lblName.PreferredHeight, Math.Max(this._lblPreFix.PreferredHeight, this._lblPostFix.PreferredHeight)));

            this._lblName.Size = new Size(this._lblName.PreferredWidth, height);
            this._lblPreFix.Size = new Size(this._lblPreFix.PreferredWidth, height);
            this._lblPostFix.Size = new Size(this._lblPostFix.PreferredWidth, height);

            this.ControlList = new List<Control> { this._lblName, this._lblPreFix, this._lblPostFix };

            NewRow = _docNode.NewLine == 1;

            List<DocTemplateElement> list = Container.ListTemplateElements.FindAll(p => p.ParentId == docNode.Id);

            foreach (BaseNode item in list.Select(node => NewDocNode(this.Container, node)))
            {
                this.ChildNodes.Add(item);
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
            foreach (Control control in this.ControlList)
            {
                control.Dispose();
            }
        }


        private int GetLayoutWidthCurrentRow(Point prevNodeEndLocation)
        {
            Point point = this.Container.GetNextLocation(prevNodeEndLocation, 0, this._docNode.NewLine == 1, this._controlOffset);
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
                num += this.PostfixTotalWidth;
            }
            return num;
        }

        /// <summary>
        /// 后缀总宽度
        /// </summary>
        private int PostfixTotalWidth
        {
            get
            {
                int width = this.PostfixWidth;
                if (this.Parent is BaseNode)
                {
                    width += (this.Parent as BaseNode).PostfixWidth;
                }
                return width;
            }
        }

        public void Layout(bool relocate = false)
        {
            if (!relocate &&_docNode.RelationType == (decimal)Enums.RelationType.病人信息)
            {
                DataSet DsPatient = new PatientDbI(GVars.OracleAccess).GetWardPatientList(GVars.User.DeptCode);
                DataRow[] drs = DsPatient.Tables[0].Select("PATIENT_ID=" + GVars.Patient.ID);
                if (drs.Length > 0)
                {
                    DataRow dr = drs[0];
                    switch ((Enums.RelationPatientItem)_docNode.RelationCode)
                    {
                        case Enums.RelationPatientItem.病人Id:
                            this.Value = dr["PATIENT_ID"];
                            break;
                        case Enums.RelationPatientItem.姓名:
                            this.Value = dr["NAME"];
                            break;
                        case Enums.RelationPatientItem.性别:
                            this.Value = dr["SEX"];
                            break;
                        case Enums.RelationPatientItem.住院号:
                            this.Value = dr["INP_NO"];
                            break;
                        case Enums.RelationPatientItem.住院次数:
                            this.Value = dr["VISIT_ID"];
                            break;
                    }
                }
            }

            if (!relocate && this.Container.ListNursingRecords != null
                && this.Container.ListNursingRecords.Count > 0)
            {
                DocNursingRecord record = this.Container.ListNursingRecords.FirstOrDefault(p => p.DocElementId == this._docNode.Id);
                if (record != null)
                    if (this._docNode.DataType == (int)Enums.DataType.字符串)
                    {
                        this.Value = record.StringValue;
                    }
                    else
                    {
                        this.Value = record.NumberValue;
                    }
            }
           

            int layoutWidthCurrentRow = this.GetLayoutWidthCurrentRow(this.PrevNodeEndLocation);
            if ((this.PrevNodeEndLocation.X + layoutWidthCurrentRow) >
                (this.Container.LayoutWidth - this.Container.Padding.Right))
                NewRow = true;

            this.Location = this.Container.GetNextLocation(this.PrevNodeEndLocation, this.PrevNodeRowHeight, this.NewRow, this._controlOffset);
            if (this.NewRow)
            {
                this.Location = new Point(this.Location.X + this.RowIndent, this.Location.Y);
            }
            foreach (Control control in this.ControlList)
            {
                control.Visible = this.Visible;
                control.Enabled = this.Enabled;
            }

            //this.Value = this.Location.ToString();

            Point location = this.Location;
            location.Offset(this.Container.DisplayRectangle.Location);
            this.RowHeight = this.NewRow ? this.ItemHeight : Math.Max(this.PrevNodeRowHeight, this.ItemHeight);
            this.LayoutBeforeChildNodes(ref location);

            if (this.ChildNodes.Count > 0)
            {
                foreach (BaseNode node in this.ChildNodes)
                {
                    node.Layout(relocate);
                }
                location = this.ChildNodes[this.ChildNodes.Count - 1].EndLocation;
                location.Offset(this.Container.DisplayRectangle.Location);
            }
            else
            {
                location = new Point(this.Location.X + this.PartWidthBeforeChildNodes, this.Location.Y);
                location.Offset(this.Container.DisplayRectangle.Location);
            }
            this.LayoutAfterChildNodes(ref location);
        }

        protected void LayoutAfterChildNodes(ref Point location)
        {
            this.LayoutPostfix(ref location);
        }

        protected virtual void LayoutBeforeChildNodes(ref Point location)
        {
            this.LayoutName(ref location, this.Visible);
            this.LayoutPrefix(ref location);
        }

        private void LayoutName(ref Point location, bool visible)
        {
            this._lblName.Visible = visible;
            this.Container.AddControl(this._lblName, location);
            location.X += this._lblName.Width;
        }

        private void LayoutPostfix(ref Point location)
        {
            this.Container.AddControl(this._lblPostFix, location);
            location.X += this._lblPostFix.Width;
        }

        protected void LayoutPrefix(ref Point location)
        {
            this.Container.AddControl(this._lblPreFix, location);
            location.X += this._lblPreFix.Width;
        }

        public static BaseNode NewDocNode(DocContainer container, DocTemplateElement docNode)
        {
            switch ((int)docNode.DocControlTemplate.DocControlType.Id)
            {
                case (int)Enums.ControlType.Label:
                    return new StaticTextNode(container, docNode);

                case (int)Enums.ControlType.TextBox:
                    if (docNode.Score > 0)
                        return new ScaleTextBoxNode(container, docNode);
                    if (docNode.DocTemplate.DocTemplateType.Id == 2)
                        return new TotalScoreTextBoxNode(container, docNode);
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
                    return new TextBoxNode(container, docNode, false);

                //return new TotalScoreTextBoxNode(container, nursingDocNode);

                case (int)Enums.ControlType.TextArea:
                    return new TextBoxNode(container, docNode, true);

                case (int)Enums.ControlType.SelectSingle:
                    return new CheckListNode(container, docNode, false);
                case (int)Enums.ControlType.SelectMul:
                    return new CheckListNode(container, docNode, true);
                case (int)Enums.ControlType.CheckBox:
                    return new CheckBoxNode(container, docNode);

                //case DocTemplateElementControlType.DropDownBox:
                //    return new ComboBoxNode(container, nursingDocNode);

                case (int)Enums.ControlType.DateTime:
                    return new DateTimePickerNode(container, docNode, "yyyy-MM-dd HH:mm");

                case (int)Enums.ControlType.Date:
                    return new DateTimePickerNode(container, docNode, "yyyy-MM-dd");

                case (int)Enums.ControlType.Time:
                    return new DateTimePickerNode(container, docNode, "HH:mm");

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
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, new EventArgs());
            }
        }

        public void RelocateChildNodes(int startChildIndex)
        {
            this.Container.SuspendLayout();
            try
            {
                Point endLocation;
                foreach (Control control in this.ControlList)
                {
                    control.Visible = this.Visible;
                    control.Enabled = this.Enabled;
                }
                for (int i = startChildIndex; i < this.ChildNodes.Count; i++)
                {
                    this.ChildNodes[i].Layout(true);
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
                this.Container.ResumeLayout();
            }
        }

        public virtual void Remove()
        {
            while (this.ChildNodes.Count > 0)
            {
                this.ChildNodes[0].Remove();
            }
            this.Parent.ChildNodes.Remove(this);
            foreach (Control control in this.ControlList)
            {
                if (this.Container.Controls.Contains(control))
                {
                    this.Container.Controls.Remove(control);
                }
            }
        }

        public DocNodeCollection ChildNodes { get; private set; }

        /// <summary>
        /// 子项缩进
        /// </summary>
        public int ChildrenIndent { get; private set; }

        /// <summary>
        /// 字体
        /// </summary>
        protected Font ContentFont { get; private set; }

        public bool Enabled
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

        /// <summary>
        /// 结束位置
        /// </summary>
        private Point EndLocation
        {
            get
            {
                if (this.LastVisibleChildNode == null)
                {
                    return new Point(this.Location.X + this.PartWidthBeforeChildNodes + this.PostfixWidth, this.Location.Y);
                }
                return new Point(this.LastVisibleChildNode.EndLocation.X + this.PostfixWidth, this.LastVisibleChildNode.EndLocation.Y);
            }
        }

        public string FormattedValue
        {
            get
            {
                if (!this.Visible || !this.Enabled)
                {
                    return string.Empty;
                }
                string str = string.Empty;
                foreach (BaseNode node in this.ChildNodes)
                {
                    if (!string.IsNullOrEmpty(node.FormattedValue))
                    {
                        if (!string.IsNullOrEmpty(str))
                        {
                            str = str + " ";
                        }
                        str = str + node.FormattedValue;
                    }
                }
                if (!this.HasFormattedValue && string.IsNullOrEmpty(str))
                {
                    return null;
                }
                //return (this._docNode + this.SelfFormattedValue + str + this._docNode.strNext);
                return (this.SelfFormattedValue + str);
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

        protected bool HasValue { [UsedImplicitly] private get; set; }

        private int Index
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

        /// <summary>
        /// 节点高度
        /// </summary>
        protected virtual int ItemHeight
        {
            get
            {
                return this._lblName.Height;
            }
        }

        /// <summary>
        /// 最后一个可见子控件
        /// </summary>
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

        /// <summary>
        /// 最后可见子控件的高度
        /// </summary>
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

        /// <summary>
        /// 当前位置
        /// </summary>
        private Point Location { get; set; }

        /// <summary>
        /// 文本字体
        /// </summary>
        protected Font NameFont { get; private set; }

        /// <summary>
        /// 文本宽度
        /// </summary>
        protected int NameWidth
        {
            get
            {
                return this._lblName.Width;
            }
        }

        /// <summary>
        /// 当前元素节点
        /// </summary>
        public DocTemplateElement NursingDocNode
        {
            get
            {
                return this._docNode;
            }
        }

        public IDocNodeParent Parent { get; set; }

        /// <summary>
        /// 后缀宽度.节点后宽度
        /// </summary>
        protected virtual int PartWidthAfterChildNodes
        {
            get
            {
                return this.PostfixWidth;
            }
        }

        /// <summary>
        /// 文本+前缀宽度.节点前宽度
        /// </summary>
        protected virtual int PartWidthBeforeChildNodes
        {
            get
            {
                //if ((this.Parent is BaseNode))
                //    return (this.Parent as BaseNode).RowIndent + (this.NameWidth + this.PrefixWidth);
                //return (this.NameWidth + this.PrefixWidth);

                return (this.NameWidth + this.PrefixWidth);
            }
        }

        /// <summary>
        /// 后缀
        /// </summary>
        private string PostFix { get; set; }

        /// <summary>
        /// 后缀宽度
        /// </summary>
        protected int PostfixWidth
        {
            get
            {
                return this._lblPostFix.Width;
            }
        }


        /// <summary>
        /// 前缀
        /// </summary>
        public string PreFix { get; private set; }

        /// <summary>
        /// 前缀宽度
        /// </summary>
        protected int PrefixWidth
        {
            get
            {
                return this._lblPreFix.Width;
            }
        }

        /// <summary>
        /// 前一节点结束位置
        /// </summary>
        private Point PrevNodeEndLocation
        {
            get
            {
                // 如果前一节点为空,表示是第一个节点
                if (this.PrevVisibleNode == null)
                {
                    return new Point(this.Container.Padding.Left, this.Container.Padding.Top);
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

        /// <summary>
        /// 前一可见节点
        /// </summary>
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

        /// <summary>
        /// 行高
        /// </summary>
        private int RowHeight { get; set; }

        /// <summary>
        /// 行缩进
        /// </summary>
        public int RowIndent
        {
            get
            {
                if (this.Parent is DocContainer)
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
                        this.Container.SuspendLayout();
                        try
                        {
                            this.Parent.RelocateChildNodes(this.Index);
                        }
                        finally
                        {
                            this.Container.ResumeLayout();
                        }
                    }
                }
            }
        }
    }
}

