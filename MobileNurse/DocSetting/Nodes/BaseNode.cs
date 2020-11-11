using System.Data;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace HISPlus
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// 节点基类
    /// </summary>
    public abstract class BaseNode : IDocNodeParent, IDisposable
    {
        /// <summary>
        /// 控件容器
        /// </summary>
        protected readonly DesignTemplate Container;

        /// <summary>
        /// 控件列表
        /// </summary>
        protected readonly List<Control> ControlList;

        private bool _enabled;

        /// <summary>
        /// 显示名(Label)
        /// </summary>
        public readonly LabelControl _lblName;

        /// <summary>
        /// 后缀(Label)
        /// </summary>
        private readonly LabelControl _lblPostFix;

        /// <summary>
        /// 前缀(Label)
        /// </summary>
        private readonly LabelControl _lblPreFix;

        /// <summary>
        /// 控件偏移量
        /// </summary>
        private readonly int _controlOffset;

        private readonly DocTemplateElement _docNode;

        private bool _visible;

        public event EventHandler ValueChanged;

        /// <summary>
        /// 控件宽度.(如果元素为默认值0时,就取控件模板默认值)
        /// </summary>
        public int ControlWidth = 0;

        /// <summary>
        /// 控件高度.(如果元素为默认值0时,就取控件模板默认值)
        /// </summary>
        public int ControlHeight = 0;

        /// <summary>
        /// 是否是新行
        /// </summary>
        public bool NewRow { get; set; }

        /// <summary>
        /// 题目最高得分。如果为0则表示非评分文书或非评分节点
        /// </summary>
        public float MaxScore;

        protected const bool ShowScore = true;

        protected BaseNode(DesignTemplate container, DocTemplateElement docNode)
        {
            this.Container = container;
            this._docNode = docNode;
            this.ChildNodes = new DocNodeCollection1(this);
            this.PreFix = docNode.ControlPrefix;
            this.PostFix = docNode.ControlSuffix;
            this.ControlWidth = docNode.ControlWidth == 0
                ? (int)docNode.DocControlTemplate.ControlWidth
                : (int)docNode.ControlWidth;

            this.ControlHeight = docNode.ControlHeight == 0
               ? (int)docNode.DocControlTemplate.ControlHeight
               : (int)docNode.ControlHeight;

            this.NameFont = DocCommon.FontFromString(docNode.ControlFont ?? docNode.DocControlTemplate.ControlFont);
            this.ContentFont = DocCommon.FontFromString(docNode.ControlFont ?? docNode.DocControlTemplate.ControlFont);
            this.ChildrenIndent = (int)docNode.ChildrenIndent;

            this._controlOffset = (int)(docNode.ControlOffset == 0 ? docNode.DocControlTemplate.ControlOffset : docNode.ControlOffset);

            this._lblName = new LabelControl();
            _lblName.Name = "_lblName";
            _lblName.Text = docNode.DisplayName;
            _lblName.Font = this.NameFont;

            this._lblName.Tag = this;

            _lblName.Appearance.TextOptions.HAlignment
                = DevExpress.Utils.HorzAlignment.Near;
            _lblName.Appearance.TextOptions.VAlignment
                = DevExpress.Utils.VertAlignment.Bottom;

            this._lblPreFix = new LabelControl
            {
                Text = this.PreFix,
                Font = this.ContentFont,
                Name = "_lblPreFix"
            };
            _lblPreFix.Appearance.TextOptions.HAlignment
               = DevExpress.Utils.HorzAlignment.Center;
            _lblPreFix.Appearance.TextOptions.VAlignment
                = DevExpress.Utils.VertAlignment.Bottom;

            //if (node.DisplayName.Contains("入院诊断"))
            //{
            //}

            this._lblPostFix = new LabelControl
            {
                Text = this.PostFix,
                Font = this.ContentFont,
                Name = "_lblPostFix"
                //TextAlign = ContentAlignment.MiddleLeft
            };

            if (ShowScore)
                if (this is RadioButtonNode || this is CheckBoxNode)
                {
                    MaxScore = GetChildNodeScores(docNode.ParentId);
                    //if (MaxScore > 0 && string.IsNullOrEmpty(_lblPreFix.Text))
                    //{
                    //    _lblPreFix.Text = string.Format("({0}分)", this.NursingDocNode.Score);
                    //}
                }
                else if (this is CheckListNode)
                {
                    MaxScore = GetChildNodeScores(docNode.Id);
                    if (MaxScore > 0 && string.IsNullOrEmpty(_lblPreFix.Text))
                    {
                        _lblPreFix.Text = string.Format("({0}分)", MaxScore);//2015.11.09 del
                    }
                }

            _lblPostFix.Appearance.TextOptions.HAlignment
               = DevExpress.Utils.HorzAlignment.Near;
            _lblPostFix.Appearance.TextOptions.VAlignment
                = DevExpress.Utils.VertAlignment.Bottom;

            // 配置行高属性
            int height = Math.Max(DesignTemplate.DefaultRowHeight, Math.Max(this._lblName.PreferredSize.Height, Math.Max(this._lblPreFix.PreferredSize.Height, this._lblPostFix.PreferredSize.Height)));

            this._lblName.Size = new Size(Math.Min(this._lblName.PreferredSize.Width, this.Container.LayoutWidth), height);
            if (this._lblName.Width == this.Container.LayoutWidth)
                this._lblName.AutoSizeMode = LabelAutoSizeMode.Vertical;

            if (ControlHeight > 0)
            {
                //_lblName.AutoSize = false;
                this._lblName.Height = ControlHeight;
                //_lblName.BorderStyle = BorderStyles.Style3D;
            }
            if (ControlWidth > 0)
            {
                this._lblName.Width = ControlWidth;
            }

            #region 测试debug，修正名称控件的宽高 2015.10.10
            //int iFixHeight = 0, iFixWidth = 10;
            //this._lblName.Width += iFixWidth;
            //this._lblName.Height += iFixHeight;
            //this._lblName.BorderStyle = BorderStyles.Office2003;
            #endregion
            //this._lblPostFix.AutoSizeMode = 
            //    this._lblPreFix.AutoSizeMode = 
            //    this._lblName.AutoSizeMode = LabelAutoSizeMode.Vertical;

            this._lblPreFix.Size = new Size(this._lblPreFix.PreferredSize.Width, height);
            this._lblPostFix.Size = new Size(this._lblPostFix.PreferredSize.Width, height);

            this.ControlList = new List<Control> { this._lblName };
            if (this._lblPreFix.Width > 0 && !string.IsNullOrEmpty(_lblPreFix.Text))
                ControlList.Add(_lblPreFix);
            if (this._lblPostFix.Width > 0 && !string.IsNullOrEmpty(_lblPostFix.Text))
                ControlList.Add(_lblPostFix);

            NewRow = _docNode.NewLine == 1;

            List<DocTemplateElement> list = Container.ListTemplateElements.FindAll(p => p.ParentId == docNode.Id);

            foreach (BaseNode item in list.Select(node => NewDocNode(this.Container, node)))
            {
                this.ChildNodes.Add(item);
            }

            switch ((int)this._docNode.ControlStatusId)
            {
                case (int)Enums.ControlStatus.可用:

                    this._visible = true;
                    this._enabled = true;
                    return;
                case (int)Enums.ControlStatus.不可用:

                    this._visible = true;
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


        /// <summary>
        /// 获取下一坐标点
        /// </summary>
        /// <param name="currentLocation">当前位置</param>
        /// <param name="rowHeight">行高</param>
        /// <param name="isNewRow">是否新行</param>
        /// <param name="customOffset">自定义偏移量</param>
        /// <returns></returns>
        private Point GetNextLocation(Point currentLocation, int rowHeight, bool isNewRow, int customOffset)
        {
            if (isNewRow)
                return new Point(Container.ParentControl.Padding.Left + customOffset, currentLocation.Y + rowHeight);
            return new Point((((int)Math.Ceiling((currentLocation.X - Container.ParentControl.Padding.Left) / (float)Container.ColumnWidth)) * Container.ColumnWidth) + Container.ParentControl.Padding.Left + customOffset, currentLocation.Y);
        }

        private int GetLayoutWidthCurrentRow(Point prevNodeEndLocation)
        {
            Point point = this.GetNextLocation(prevNodeEndLocation, 0, this._docNode.NewLine == 1, this._controlOffset);
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

        public void Layout()
        {
            this.Layout(false);
        }

        public void Layout(bool relocate)
        {
            if (!relocate && _docNode.RelationType == (decimal)Enums.RelationType.病人信息)
            {
                if (!string.IsNullOrEmpty(this.Container.PatientId))
                {
                    //DataSet dsPatient = new PatientDbI(GVars.OracleAccess).GetWardPatientList(GVars.User.DeptCode);
                    //DataRow[] drs = dsPatient.Tables[0].Select("PATIENT_ID=" + SqlManager.SqlConvert(this.Container.PatientId));
                    //DataSet ds = new PatientDbI(GVars.OracleAccess).GetPatientInfo(this.Container.PatientId);
                    DataSet ds = new PatientDbI(GVars.OracleAccess).GetInpPatientInfo_FromID(this.Container.PatientId,this.Container.VisitId);
                    //if (drs.Length > 0)
                    if(ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        //if(dr[
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
                            case Enums.RelationPatientItem.生日:
                                this.Value = dr["DATE_OF_BIRTH"];
                                break;
                            case Enums.RelationPatientItem.年龄:
                                var o = dr["DATE_OF_BIRTH"];
                                if (o != null)
                                    this.Value = PersonCls.GetAge((DateTime)o, GVars.OracleAccess.GetSysDate());
                                break;
                            case Enums.RelationPatientItem.诊断:
                                this.Value = dr["DIAGNOSIS"];
                                break;
                            case Enums.RelationPatientItem.护理等级:
                                this.Value = dr["NURSING_CLASS_NAME"];
                                break;
                            case Enums.RelationPatientItem.主治医生:
                                this.Value = dr["DOCTOR_IN_CHARGE"];
                                break;
                            case Enums.RelationPatientItem.床号:
                                this.Value = dr["BED_NO"];
                                break;
                            case Enums.RelationPatientItem.床标号:
                                this.Value = dr["BED_LABEL"];
                                break;
                            case Enums.RelationPatientItem.病情状态:
                                this.Value = dr["PATIENT_STATUS_NAME"];
                                break;
                            case Enums.RelationPatientItem.入院时间:
                                this.Value = dr["ADMISSION_DATE_TIME"];
                                break;
                            case Enums.RelationPatientItem.所在科室:
                                //this.Value = dr["DEPT_NAME"];
                                this.Value = GVars.Patient.DeptName;//2016.02.27 以病人当前状态为准
                                break;
                        }
                    }
                }
            }
            else if (!relocate && this.Container.docNursingRecordDs != null && this.Container.docNursingRecordDs.Tables.Count > 0
                     && this.Container.docNursingRecordDs.Tables[0].Rows.Count > 0)
            {
                DataRow[] rows = this.Container.docNursingRecordDs.Tables[0].Select("DOC_ELEMENT_ID='" + this._docNode.Id + "'");
                if (rows.Length > 0)
                    if (this._docNode.DataType == (int)Enums.DataType.字符串)
                    {
                        this.Value = rows[0]["STRING_VALUE"].ToString();
                    }
                    else
                    {
                        this.Value = Convert.ToDecimal(rows[0]["NUMBER_VALUE"]);
                    }
            }

            int layoutWidthCurrentRow = this.GetLayoutWidthCurrentRow(this.PrevNodeEndLocation);


            if ((this.PrevNodeEndLocation.X + layoutWidthCurrentRow) >=
              this.Container.LayoutWidth)
                NewRow = true;

            //if (this._docNode.RowSpacing < 0)
            //{
            //    NewRow = false;
            //}

            this.Location = this.GetNextLocation(this.PrevNodeEndLocation, this.PrevNodeRowHeight, this.NewRow,
                this._controlOffset);

            if (this.NewRow)
            {
                this.Location = new Point(this.Location.X + this.RowIndent, this.Location.Y);
            }
            //else if (this._docNode.RowSpacing < 0)
            //{
            //    this.Location = new Point((int)_docNode.ControlOffset + this.RowIndent, this.Location.Y);
            //}
            foreach (Control control in this.ControlList)
            {
                control.Visible = this.Visible;//2015.11.09 del
                control.Enabled = this.Enabled;
            }

            //this.Value = this.Location.ToString();

            Point location = this.Location;
            location.Offset(this.Container.ParentControl.DisplayRectangle.Location);
            this.RowHeight = this.NewRow ? this.ItemHeight : Math.Max(this.PrevNodeRowHeight, this.ItemHeight);
            this.LayoutBeforeChildNodes(ref location);

            if (this is GridNode)
                GridLocation = new Point(this.Location.X - Container.Border, this.Location.Y - Container.Border);
            #region
            //GridLocation = new Point(this.Location.X, this.Location.Y);

            //if (this is CheckListNode && MaxScore > 0)
            //{
            //    this.LayoutAfterChildNodes(ref location);
            //    if (this.ChildNodes.Count > 0)
            //    {
            //        foreach (BaseNode node in this.ChildNodes)
            //        {
            //            node.Layout(relocate);
            //        }
            //        location = this.ChildNodes[this.ChildNodes.Count - 1].EndLocation;
            //        location.Offset(this.Container.ParentControl.DisplayRectangle.Location);
            //    }
            //    else
            //    {
            //        location = new Point(location.X + this.PartWidthBeforeChildNodes + this.PartWidthAfterChildNodes, this.Location.Y);
            //        location.Offset(this.Container.ParentControl.DisplayRectangle.Location);
            //    }
            //}
            //else
            //{
            //    if (this.ChildNodes.Count > 0)
            //    {
            //        foreach (BaseNode node in this.ChildNodes)
            //        {
            //            node.Layout(relocate);
            //        }
            //        location = this.ChildNodes[this.ChildNodes.Count - 1].EndLocation;
            //        location.Offset(this.Container.ParentControl.DisplayRectangle.Location);
            //    }
            //    else
            //    {
            //        location = new Point(this.Location.X + this.PartWidthBeforeChildNodes + (MaxScore > 0 ? this.PartWidthAfterChildNodes : 0), this.Location.Y);

            //        location.Offset(this.Container.ParentControl.DisplayRectangle.Location);
            //    }
            //    this.LayoutAfterChildNodes(ref location);
            //}
            #endregion
            if (this.ChildNodes.Count > 0)
            {
                foreach (BaseNode node in this.ChildNodes)
                {
                    node.Layout(relocate);
                }

                //if (this is GridNode)
                //{
                //    GridNode gridNode = this as GridNode;
                //    if (gridNode != null)
                //    {
                //        // 把所有后面的绘制向下平移
                //        location =
                //            new Point(GridLocation.X + gridNode._ctl.Width, GridLocation.Y
                //                + 9 + (int)gridNode.NursingDocNode.RowSpacing);
                //        this.Location = location;
                //    }
                //}
                //else
                location = this.ChildNodes[this.ChildNodes.Count - 1].EndLocation;

                location.Offset(this.Container.ParentControl.DisplayRectangle.Location);
            }
            else
            {
                location =
                    new Point(
                        this.Location.X + this.PartWidthBeforeChildNodes, this.Location.Y);

                location.Offset(this.Container.ParentControl.DisplayRectangle.Location);
            }
            this.LayoutAfterChildNodes(ref location);
        }

        /// <summary>
        /// 在绘制子节点后，绘制后缀
        /// </summary>
        /// <param name="location"></param>
        protected virtual void LayoutAfterChildNodes(ref Point location)
        {
            this.LayoutPostfix(ref location);
        }

        /// <summary>
        /// 在绘制子节点前，绘制前缀和名称
        /// </summary>
        /// <param name="location"></param>
        protected virtual void LayoutBeforeChildNodes(ref Point location)
        {
            //+ (int)NursingDocNode.RowSpacing
            this.LayoutName(ref location, this.Visible);
            this.LayoutPrefix(ref location);
            //this.LayoutPostfix(ref location);
        }

        /// <summary>
        /// 获取子节点最高分数
        /// </summary>
        protected float GetChildNodeScores(decimal parentId)
        {
            //return Container.ListTemplateElements.Where(p => p.ParentId == parentId).Select(model => model.Score).Concat(new float[] {0}).Max();
            IEnumerable<DocTemplateElement> list = Container.ListTemplateElements.Where(p => p.ParentId == parentId).ToList();
            if (list.Any())
                return list.Max(p => p.Score);
            return -1;
        }

        /// <summary>
        /// 绘制名称
        /// </summary>
        /// <param name="location"></param>
        /// <param name="visible"></param>
        private void LayoutName(ref Point location, bool visible)
        {
            if (this is StaticTextNode)
            {
                // 根节点自动居中
                if (this.NursingDocNode.ParentId == 0 && this.NursingDocNode.SortId == 1)
                {
                    this.Container.AddControl(this._lblName,
                        new Point((Container.ParentControl.Width - _lblName.Width) / 2,
                            location.Y + (int)NursingDocNode.RowSpacing));
                    location.X += this._lblName.Width;
                    return;
                }
            }
            this._lblName.Visible = visible;
            if (IsInGrid)
            {
                this.Container.AddControl(this._lblName,
                    new Point(GridLocation.X + (int)this.NursingDocNode.ControlOffset,
                        GridLocation.Y + (int)NursingDocNode.RowSpacing));
            }
            else
            {
                this.Container.AddControl(this._lblName,
                    new Point(location.X, location.Y + (int)NursingDocNode.RowSpacing));
                location.X += this._lblName.Width;
                //location.Y += (int)NursingDocNode.RowSpacing;
            }
        }

        /// <summary>
        /// 绘制后缀
        /// </summary>
        /// <param name="location"></param>
        private void LayoutPostfix(ref Point location)
        {
            if (this._lblPostFix.Width > 0 && !string.IsNullOrEmpty(_lblPostFix.Text))
            {
                #region //2015.11.09 debug
                if (IsInGrid)
                {
                    this.Container.AddControl(this._lblName,
                        new Point(GridLocation.X + (int)this.NursingDocNode.ControlOffset,
                            GridLocation.Y + (int)NursingDocNode.RowSpacing));
                }
                #endregion
                else
                {
                    this.Container.AddControl(this._lblPostFix, new Point(location.X, location.Y + (int)NursingDocNode.RowSpacing));
                    location.X += this._lblPostFix.Width;
                }
            }
        }

        /// <summary>
        /// 绘制前缀
        /// </summary>
        /// <param name="location"></param>
        protected void LayoutPrefix(ref Point location)
        {
            if (this._lblPreFix.Width > 0 && !string.IsNullOrEmpty(_lblPreFix.Text))
            {
                if (IsInGrid)
                {
                    this.Container.AddControl(this._lblName,
                        new Point(GridLocation.X + (int)this.NursingDocNode.ControlOffset,
                            GridLocation.Y + (int)NursingDocNode.RowSpacing));
                }
                else
                {
                    this.Container.AddControl(this._lblPreFix,
                        new Point(location.X, location.Y + (int)NursingDocNode.RowSpacing));
                    location.X += this._lblPreFix.Width;
                }
            }
        }

        public static BaseNode NewDocNode(DesignTemplate container, DocTemplateElement node)
        {
            if ((Enums.RelationType)node.RelationType == Enums.RelationType.分隔线)
            {
                return new SplitLineNode(container, node);
            }
            if ((Enums.RelationType)node.RelationType == Enums.RelationType.表格)
            {
                return new GridNode(container, node);
            }
            switch ((int)node.DocControlTemplate.DocControlType.Id)
            {
                case (int)Enums.ControlType.Label:
                    return new StaticTextNode(container, node);

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
                    if ((Enums.RelationType)node.RelationType == Enums.RelationType.签名)
                        return new SignTextBoxNode(container, node);
                    if ((Enums.RelationType)node.RelationType == Enums.RelationType.总分)
                        return new TotalScoreTextBoxNode(container, node);

                    return new TextBoxNode(container, node);

                case (int)Enums.ControlType.TextArea:
                    return new TextAreaNode(container, node);

                case (int)Enums.ControlType.SelectSingle:
                    return new CheckListNode(container, node, false);
                case (int)Enums.ControlType.SelectMul:
                    return new CheckListNode(container, node, true);
                case (int)Enums.ControlType.CheckBox:
                    return new CheckBoxNode(container, node);
                case (int)Enums.ControlType.RadioButton:
                    return new RadioButtonNode(container, node);

                //case DocTemplateElementControlType.DropDownBox:
                //    return new HISPlus.UserControls.UcComboBoxNode(container, nursingDocNode);

                case (int)Enums.ControlType.DateTime:
                    return new DateTimePickerNode(container, node);

                case (int)Enums.ControlType.Date:
                    return new DatePickerNode(container, node);

                case (int)Enums.ControlType.Time:
                    return new TimePickerNode(container, node);

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
                    endLocation = new Point(this.Location.X + this.PartWidthBeforeChildNodes - 20, this.Location.Y);
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

        public DocNodeCollection1 ChildNodes { get; private set; }

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
                //if (IsInGrid)
                //    return null;
                if (this is GridNode)
                {
                    GridNode gridNode = this as GridNode;
                    if (gridNode != null)
                        return new Point(GridLocation.X + gridNode._ctl.Width, GridLocation.Y
                                 + gridNode._ctl.Height + 3 + (int)gridNode.NursingDocNode.RowSpacing);
                }

                if (this.LastVisibleChildNode == null)
                {
                    return new Point(this.Location.X + this.PartWidthBeforeChildNodes + this.PostfixWidth, this.Location.Y + (int)this.NursingDocNode.RowSpacing);
                }
                return new Point(this.LastVisibleChildNode.EndLocation.X + this.PostfixWidth, this.LastVisibleChildNode.EndLocation.Y);
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

        protected bool HasValue { get; set; }

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
        /// 是否是表格或在表格中
        /// </summary>
        public bool IsInGrid
        {
            get
            {
                if (this._docNode.RelationType == (decimal)Enums.RelationType.表格)
                    return true;
                if (this.Parent is BaseNode)
                {
                    return (this.Parent as BaseNode).IsInGrid;
                }
                return false;
            }
        }

        private Point _gridLocation;

        /// <summary>
        /// Grid位置.网格位置,如果没有网格,该值为NULL
        /// </summary>
        protected Point GridLocation
        {
            get
            {
                if (this._gridLocation == Point.Empty)
                {
                    BaseNode baseNode = this.Parent as BaseNode;
                    if (baseNode != null) return baseNode.GridLocation;
                }
                else return this._gridLocation;

                return Point.Empty;
            }
            private set { _gridLocation = value; }
        }

        /// <summary>
        /// 默认行高
        /// </summary>
        protected int DefaultRowHeight = 28;

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

        ///// <summary>
        ///// Grid位置.网格位置,如果没有网格,该值为NULL
        ///// </summary>
        //private Point GridLocation { get; set; }


        /// <summary>
        /// 文本字体
        /// </summary>
        private Font NameFont { get; set; }

        /// <summary>
        /// 文本宽度
        /// </summary>
        private int NameWidth
        {
            get
            {
                return this._lblName.Width + DesignTemplate.SpaceNameControl;
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
                return (this.NameWidth + this.PrefixWidth);

                //return (this.NameWidth + this.PrefixWidth+this.PostfixWidth);
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
                    return new Point(this.Container.Border, this.Container.Border);
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

        public int PrevNodeRowHeight
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
                if (flag)
                {
                    //if (this.PrevVisibleNode.RowHeight > this.DefaultRowHeight)
                    //    return this.PrevVisibleNode.RowHeight + 3;
                    return this.PrevVisibleNode.RowHeight;
                }
                return this.PrevVisibleNode.LastVisibleChildRowHeight;
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
                if (this.Parent is DesignTemplate)
                {
                    return 0;
                }
                BaseNode node = this.Parent as BaseNode;
                if (node != null)
                    return (node.RowIndent + node.ChildrenIndent);
                return 0;
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

