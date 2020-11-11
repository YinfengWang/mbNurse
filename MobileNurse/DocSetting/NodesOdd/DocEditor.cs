namespace HISPlus
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class DocEditor : ContainerControl, IDocNodeParent
    {
        private const int _columnWidth = 60;
        private DocNodeCollection _docNodes;
        //private WSDocPatientForm _form;
        private int _layoutWidth = 800;
        private int _pageIndex;
        private const int _rowHeight = 0x1b;
        private const int _spaceBetweenRows = 5;
        private DocTemplate _template;
        private Dictionary<string, BaseNode> nodeDict = new Dictionary<string, BaseNode>();

        public DocEditor()
        {
            this._docNodes = new DocNodeCollection(this);
            this.AutoScroll = true;
            this.DefaultRowHeight = 0x1b;
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

        private BaseNode FindNode(int id, DocNodeCollection nodes)
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

        internal Point GetNextLocation(Point currentLocation, int rowHeight, bool isNewRow, int customOffset)
        {
            if (isNewRow)
                return new Point(base.Padding.Left, currentLocation.Y + rowHeight);
            else
                return new Point((((int)Math.Ceiling((double)(((float)(currentLocation.X - base.Padding.Left)) / 60f))) * 60) + base.Padding.Left, currentLocation.Y);

            return new Point(currentLocation.X + customOffset, currentLocation.Y);
        }

        private void LayoutForm(DocTemplate template)
        {
            if ((template != null) && (template.DocTemplateElements != null))
            {
                foreach (DocTemplateElement node in template.DocTemplateElements)
                {
                    BaseNode item = BaseNode.NewDocNode(this, node);
                    if (item != null)
                    {
                        this._docNodes.Add(item);
                    }
                }
            }
            Dictionary<int, BaseNode> dict = new Dictionary<int, BaseNode>();
            this.SetNodeDict(this._docNodes, dict);
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                base.SuspendLayout();
                base.VerticalScroll.Value = 0;
                base.HorizontalScroll.Value = 0;
                foreach (Control control in base.Controls)
                {
                    control.Dispose();
                }
                base.Controls.Clear();
                Point point = new Point(base.Padding.Left, base.Padding.Top);
                int y = point.Y;
                foreach (BaseNode node3 in this._docNodes)
                {
                    //if ((node3 is TotalScoreTextBoxNode) && dict.ContainsKey(node3.NursingDocNode.referenceCode))
                    //{
                    //    (node3 as TotalScoreTextBoxNode).SetReferenceNode(dict[node3.NursingDocNode.referenceCode]);
                    //}
                    node3.Layout();
                }
            }
            finally
            {
                base.ResumeLayout();
                Cursor.Current = Cursors.Default;
            }
        }


        public void NewForm(DocTemplate template)
        {
            this._docNodes.Clear();
            this._template = template;
            this.LayoutForm(template);
        }

        public void NewForm(int templateId)
        {
            Cursor.Current = Cursors.Default;
            try
            {
                this._docNodes.Clear();
                this._template = new DocTemplateDAL().Load(templateId);
                //this._form = null;
                this.LayoutForm(this._template);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public void Print(string printerName, bool isPreview)
        {
            this._pageIndex = 0;
            PrintDocument document = new PrintDocument
            {
                DocumentName = this._template.TemplateName,
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
                    //this.ChildNodes[i].Layout(true);
                }
            }
            finally
            {
                base.ResumeLayout();
            }
        }

        public bool SaveForm(DateTime formTime)
        {
            //if ((this._template == null) || (this._patient == null))
            //{
            //    return false;
            //}
            //WSDocPatientForm form = new WSDocPatientForm
            //{
            //    template = this._template,
            //    patientId = this._patient.id,
            //    series = this._patient.series,
            //    patientName = this._patient.patient.name,
            //    mrn = this._patient.mrn,
            //    deptCode = Global.Ward.code,
            //    wardCode = Global.Ward.code,
            //    createdBy = Global.User.userCode,
            //    createdName = Global.User.userName,
            //    formTime = formTime,
            //    formTimeSpecified = true,
            //    lastUpdatedBy = Global.User.userCode
            //};
            //double totalScore = 0.0;
            //form.template.docNodes = this.GetDocTemplateElements(this._docNodes, ref totalScore);
            //form.totalScore = totalScore;
            //if (this._form != null)
            //{
            //    form.id = this._form.id;
            //}
            //this._form = WSController.SaveAndGetDocForm(form);
            //return (this._form != null);
            return false;
        }

        private void SetNodeDict(DocNodeCollection nodes, Dictionary<int, BaseNode> dict)
        {
            //foreach (BaseNode node in nodes)
            //{
            //    if (!dict.ContainsKey(node.NursingDocNode.code))
            //    {
            //        dict.Add(node.NursingDocNode.code, node);
            //    }
            //    this.SetNodeDict(node.ChildNodes, dict);
            //}
        }

        [Browsable(false)]
        public DocNodeCollection ChildNodes
        {
            get
            {
                return this._docNodes;
            }
        }

        [Browsable(false)]
        public int DefaultRowHeight { get; private set; }

        //[Browsable(false)]
        //public WSDocPatientForm DocForm
        //{
        //    get
        //    {
        //        return this._form;
        //    }
        //}

        public string DocFormText
        {
            get
            {
                string str = string.Empty;
                foreach (BaseNode node in this.DocNodes)
                {
                    str = str + node.FormattedValue;
                }
                return str;
            }
        }

        [Browsable(false)]
        public DocNodeCollection DocNodes
        {
            get
            {
                return this._docNodes;
            }
        }

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

        //[Browsable(false)]
        //public WSInpatientVisit Patient
        //{
        //    get
        //    {
        //        return this._patient;
        //    }
        //    set
        //    {
        //        this._patient = value;
        //    }
        //}

        //[Browsable(false)]
        //public WSDocFormTemplate Template
        //{
        //    get
        //    {
        //        return this._template;
        //    }
        //}
    }
}

