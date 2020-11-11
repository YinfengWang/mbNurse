namespace HISPlus
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;
    using System.Linq;

    public sealed class DocContainer1 : ContainerControl, IDocNodeParent1
    {
        /// <summary>
        /// 列宽
        /// </summary>
        private const int ColumnWidth = 40;
        //private WSDocPatientForm _form;
        private int _layoutWidth = 800;
        private int _pageIndex;
        private const int RowHeight = 28;
        private const int SpaceBetweenRows = 5;

        /// <summary>
        /// 模板元素List
        /// </summary>
        public List<DocTemplateElement> ListTemplateElements;

        /// <summary>
        /// 文书记录值List
        /// </summary>
        public IList<DocNursingRecord> ListNursingRecords;

        /// <summary>
        /// 文书护理信息
        /// </summary>
        public DocNursing Nursing;
        public int LayoutWidth
        {
            get
            {
                return this._layoutWidth;
            }
            set
            {
                this._layoutWidth = value;
            }
        }

        /// <summary>
        /// 文书模板Id
        /// </summary>
        private decimal TemplateId;

        public DocContainer1()
        {
            this.ChildNodes = new DocNodeCollection1(this);            
        }

        public void AddControl(Control control, Point location)
        {
            if (!base.Controls.Contains(control))
            {
                base.Controls.Add(control);
            }
            control.Location = location;
        }

        internal BaseNode FindNode(int id)
        {
            return this.FindNode(id, this.ChildNodes);
        }

        private BaseNode FindNode(int id, DocNodeCollection1 nodes)
        {
            //foreach (BaseNode node in nodes)
            //{
            //    if (node.i == id)
            //    {
            //        return node;
            //    }
            //    if (node.ChildNodes.Count > 0)
            //    {
            //        BaseNode node2 = this.FindNode(id, node.ChildNodes);
            //        if (node2 != null)
            //        {
            //            return node2;
            //        }
            //    }
            //}
            return null;
        }

        /// <summary>
        /// 获取下一坐标点
        /// </summary>
        /// <param name="currentLocation">当前位置</param>
        /// <param name="rowHeight">行高</param>
        /// <param name="isNewRow">是否新行</param>
        /// <param name="customOffset">自定义偏移量</param>
        /// <returns></returns>
        internal Point GetNextLocation(Point currentLocation, int rowHeight, bool isNewRow, int customOffset)
        {
            if (isNewRow)
                return new Point(base.Padding.Left, currentLocation.Y + rowHeight);
            else
                //return new Point((((int)Math.Ceiling(((float)(currentLocation.X - Padding.Left)) / ColumnWidth)) * ColumnWidth) + Padding.Left, currentLocation.Y);
                return new Point((((int)Math.Ceiling((double)(((float)(currentLocation.X - base.Padding.Left)) / 60f))) * 60) + base.Padding.Left, currentLocation.Y);
            return new Point((((int)Math.Ceiling((double)(((float)(currentLocation.X - base.Padding.Left)) / 10f))) * 10) + base.Padding.Left + 10, currentLocation.Y);

            return new Point(currentLocation.X + customOffset, currentLocation.Y);
        }

        private void LayoutForm(IList<DocTemplateElement> ilist)
        {
            List<DocTemplateElement> list = ilist.OrderBy(p => p.ParentId).ThenBy(p => p.SortId).ToList();

            this.ListTemplateElements = list;

            foreach (DocTemplateElement node in list)
            {
                if (node.ParentId != 0) continue;

                //BaseNode1 item = BaseNode1.NewDocNode(this, node);

                //if (item != null)
                //{
                //    this.ChildNodes.Add(item);

                //    item.Layout();
                //}
            }

            SetChecked(this.ChildNodes);

            //Dictionary<int, BaseNode> dict = new Dictionary<int, BaseNode>();
            ////this.SetNodeDict(this._docNodes, dict);
            //Cursor.Current = Cursors.WaitCursor;
            //try
            //{
            //    base.SuspendLayout();
            //    base.VerticalScroll.Value = 0;
            //    base.HorizontalScroll.Value = 0;
            //    foreach (Control control in base.Controls)
            //    {
            //        control.Dispose();
            //    }
            //    base.Controls.Clear();
            //    Point point = new Point(base.Padding.Left, base.Padding.Top);
            //    int y = point.Y;
            //    foreach (BaseNode node3 in this.DocNodes)
            //    {
            //        if ((node3 is TotalScoreTextBoxNode) && dict.ContainsKey(node3.NursingDocNode.referenceCode))
            //        {
            //            (node3 as TotalScoreTextBoxNode).SetReferenceNode(dict[node3.NursingDocNode.referenceCode]);
            //        }
            //        node3.Layout();
            //    }
            //}
            //finally
            //{
            //    base.ResumeLayout();
            //    Cursor.Current = Cursors.Default;
            //}
        }

        private void SetChecked(IEnumerable<BaseNode1> nodes)
        {

            foreach (BaseNode1 node in nodes)
            {
                if ((node is TotalScoreTextBoxNode))
                {
                    (node as TotalScoreTextBoxNode1).SetReferenceNode(this.ChildNodes);
                }
                SetChecked(node.ChildNodes);
            }
        }

        public void NewForm(decimal templateId, decimal nursingId = 0)
        {
            Cursor.Current = Cursors.Default;
            try
            {
                this.ChildNodes.Clear();

                this.TemplateId = templateId;

                IList<DocTemplateElement> ilist =
                    new DocTemplateElementDAL().FindByProperty("DocTemplate.Id", templateId);
                if (nursingId > 0)
                {
                    DocNursing nursing = new DocNursingDAL().Get(nursingId);

                    this.Nursing = nursing;
                    //IList<DocNursingRecord> nursingRecords =
                    //    new DocNursingRecordDAL().FindByProperty(new[] { "DocNursing.Id", "DocNursing.PatientId", "DocNursing.VisitNo" }, new object[] { nursingId, Convert.ToDecimal(GVars.Patient.ID), GVars.Patient.VisitId });
                    IList<DocNursingRecord> nursingRecords =
                       new DocNursingRecordDAL().FindByProperty(new[] { "DocNursing.Id" }, new object[] { nursingId });


                    this.ListNursingRecords = nursingRecords;
                }
                this.LayoutForm(ilist);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void Save(DocNodeCollection1 nodes, ref double totalScore)
        {
            List<BaseNode1> list = new List<BaseNode1>();
            foreach (BaseNode1 node in nodes)
            {
                DocNursingRecord record = ListNursingRecords.FirstOrDefault(p => p.DocElementId == node.NursingDocNode.Id);

                if (((node.Value == null) && !node.IsInList) && (!(node is ScaleTextBoxNode1)))
                {
                    record = null;
                }
                else
                {
                    if (record == null)
                    {
                        record = new DocNursingRecord();
                        record.DocElementId = node.NursingDocNode.Id;
                        record.DocNursing = this.Nursing;
                        ListNursingRecords.Add(record);
                    }
                    if (node.Value != null)
                    {
                        if (node.NursingDocNode.DataType == (int)Enums.DataType.字符串)
                        {
                            record.StringValue = node.Value.ToString();
                        }
                        else
                        {
                            record.NumberValue = Convert.ToDecimal(node.Value);
                        }
                    }
                }
                if (((node is CheckBoxNode) && (node.Value != null)) && (node.Value.ToString() == "1"))
                {
                    totalScore += node.NursingDocNode.Score;
                }
                this.Save(node.ChildNodes, ref totalScore);
            }
        }

        public void SaveNursing()
        {
            if (this.Nursing == null)
            {
                this.Nursing = new DocNursing
                {
                    PatientId = "01403141",
                    VisitNo = "1",
                    WardCode = "1026N",
                    TemplateId = this.TemplateId,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CreateUser = GVars.User.UserName,
                    UpdateUser = GVars.User.UserName
                };
            }

            double totalScore = 0.0;
            Save(this.ChildNodes, ref totalScore);
            Nursing.TotalScore = totalScore;

            new DocNursingDAL().SaveOrUpdate(Nursing);
            new DocNursingRecordDAL().SaveOrUpdate(ListNursingRecords);

            this.FindForm().Close();
        }

        public void Print(string printerName, bool isPreview)
        {
            this._pageIndex = 0;
            PrintDocument document = new PrintDocument
            {
                //DocumentName = this._template.TemplateName,
                PrintController = new StandardPrintController()
            };
            document.DefaultPageSettings.Margins = new Margins(base.Padding.Left, base.Padding.Right, base.Padding.Top, base.Padding.Bottom);
            document.DefaultPageSettings.PrinterSettings.Duplex = Duplex.Vertical;
            document.DefaultPageSettings.Landscape = false;
            document.PrintPage += new PrintPageEventHandler(this.printDoc_PrintPage);
            if (!isPreview)
            {
                document.Print();
            }
            else
            {
                PrintPreviewDialog dialog = new PrintPreviewDialog
                {
                    Document = document,
                    ShowIcon = false
                };
                dialog.PrintPreviewControl.Zoom = 1.0;
                dialog.ShowDialog();
            }
        }

        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            this.PrintPage(e.Graphics, e.MarginBounds, this._pageIndex, 1f);
            if (((int)((((float)base.Height) / 0.96f) / ((float)e.MarginBounds.Height))) > this._pageIndex)
            {
                e.HasMorePages = true;
                this._pageIndex++;
            }
            else
            {
                this._pageIndex = 0;
            }
        }

        public void PrintPage(Graphics g, Rectangle rectPrintBound, int pageIndex, float verticalScaleFactor)
        {
            float num = 0.96f;
            foreach (Control control in base.Controls)
            {
                if (!control.Visible)
                {
                    continue;
                }
                int x = ((int)(((float)(control.Left + base.HorizontalScroll.Value)) / num)) + rectPrintBound.Left;
                int y = (((int)((((float)(control.Top + base.VerticalScroll.Value)) / num) * verticalScaleFactor)) - (rectPrintBound.Height * pageIndex)) + rectPrintBound.Top;
                int width = (int)Math.Ceiling((double)(((float)control.Width) / num));
                int height = (int)Math.Ceiling((double)(((float)control.Height) / num));
                if ((y >= rectPrintBound.Top) && (y < rectPrintBound.Bottom))
                {
                    StringFormat format = new StringFormat();
                    if (control is Label)
                    {
                        Label label = control as Label;
                        switch (label.TextAlign)
                        {
                            case ContentAlignment.TopLeft:
                            case ContentAlignment.TopCenter:
                            case ContentAlignment.TopRight:
                                format.LineAlignment = StringAlignment.Near;
                                break;

                            case ContentAlignment.MiddleLeft:
                            case ContentAlignment.MiddleCenter:
                            case ContentAlignment.MiddleRight:
                                format.LineAlignment = StringAlignment.Center;
                                break;

                            case ContentAlignment.BottomCenter:
                            case ContentAlignment.BottomRight:
                            case ContentAlignment.BottomLeft:
                                format.LineAlignment = StringAlignment.Far;
                                break;
                        }
                        switch (label.TextAlign)
                        {
                            case ContentAlignment.TopLeft:
                            case ContentAlignment.MiddleLeft:
                            case ContentAlignment.BottomLeft:
                                format.Alignment = StringAlignment.Near;
                                break;

                            case ContentAlignment.TopCenter:
                            case ContentAlignment.MiddleCenter:
                            case ContentAlignment.BottomCenter:
                                format.Alignment = StringAlignment.Center;
                                break;

                            case ContentAlignment.TopRight:
                            case ContentAlignment.BottomRight:
                            case ContentAlignment.MiddleRight:
                                format.Alignment = StringAlignment.Far;
                                break;
                        }
                        g.DrawString(label.Text, label.Font, new SolidBrush(label.ForeColor), new Rectangle(x, y, width + 10, height), format);
                        continue;
                    }
                    if (control is TextBox)
                    {
                        TextBox box = control as TextBox;
                        switch (box.TextAlign)
                        {
                            case HorizontalAlignment.Left:
                                format.Alignment = StringAlignment.Near;
                                break;

                            case HorizontalAlignment.Right:
                                format.Alignment = StringAlignment.Far;
                                break;

                            case HorizontalAlignment.Center:
                                format.Alignment = StringAlignment.Center;
                                break;
                        }
                        format.LineAlignment = StringAlignment.Center;
                        g.DrawString(box.Text, box.Font, new SolidBrush(box.ForeColor), new Rectangle(x, y, width, height), format);
                        continue;
                    }
                    if (control is ComboBox)
                    {
                        ComboBox box2 = control as ComboBox;
                        format.Alignment = StringAlignment.Near;
                        format.LineAlignment = StringAlignment.Center;
                        g.DrawString(box2.Text, box2.Font, new SolidBrush(box2.ForeColor), new Rectangle(x, y, width, height), format);
                        continue;
                    }
                    if (control is DateTimePicker)
                    {
                        DateTimePicker picker = control as DateTimePicker;
                        format.Alignment = StringAlignment.Near;
                        format.LineAlignment = StringAlignment.Center;
                        g.DrawString(picker.Text, picker.Font, Brushes.Black, new Rectangle(x, y, width, height), format);
                        continue;
                    }
                    if (control is CheckBox)
                    {
                        CheckBox box3 = control as CheckBox;
                        switch (box3.TextAlign)
                        {
                            case ContentAlignment.TopLeft:
                            case ContentAlignment.TopCenter:
                            case ContentAlignment.TopRight:
                                format.LineAlignment = StringAlignment.Near;
                                break;

                            case ContentAlignment.MiddleLeft:
                            case ContentAlignment.MiddleCenter:
                            case ContentAlignment.MiddleRight:
                                format.LineAlignment = StringAlignment.Center;
                                break;

                            case ContentAlignment.BottomCenter:
                            case ContentAlignment.BottomRight:
                            case ContentAlignment.BottomLeft:
                                format.LineAlignment = StringAlignment.Far;
                                break;
                        }
                        switch (box3.TextAlign)
                        {
                            case ContentAlignment.TopLeft:
                            case ContentAlignment.MiddleLeft:
                            case ContentAlignment.BottomLeft:
                                format.Alignment = StringAlignment.Near;
                                break;

                            case ContentAlignment.TopCenter:
                            case ContentAlignment.MiddleCenter:
                            case ContentAlignment.BottomCenter:
                                format.Alignment = StringAlignment.Center;
                                break;

                            case ContentAlignment.TopRight:
                            case ContentAlignment.BottomRight:
                            case ContentAlignment.MiddleRight:
                                format.Alignment = StringAlignment.Far;
                                break;
                        }
                        g.DrawString("□" + box3.Text, box3.Font, new SolidBrush(box3.ForeColor), new Rectangle(x, y, width, height), format);
                        if (box3.Checked)
                        {
                            g.DrawString("√", new Font("楷体_GB2312", 9f, FontStyle.Bold), new SolidBrush(box3.ForeColor), new Rectangle(x, y, width, height), format);
                        }
                    }
                }
            }
        }

        public void RelocateChildNodes(int startChildIndex)
        {
            base.SuspendLayout();
            try
            {
                for (int i = startChildIndex; i < this.ChildNodes.Count; i++)
                {
                    this.ChildNodes[i].Layout(true);
                }
            }
            finally
            {
                base.ResumeLayout();
            }
        }

        [Browsable(false)]
        public DocNodeCollection1 ChildNodes { get; private set; }
    }
}

