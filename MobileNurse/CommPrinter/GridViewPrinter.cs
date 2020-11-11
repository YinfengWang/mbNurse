using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using CommonEntity;
using DevExpress.XtraGrid.Views.Grid;

namespace CommPrinter
{
    public class GridViewPrinter
    {
        private int _columnHeaderPrintHeight;
        private List<int> _columnsPrintWidth;
        private int _currPrintRow;
        private int _currPrintTableFoot;
        private GridView _dataGridView;
        private int _dataGridViewPrintLeft;
        private System.Drawing.Printing.Duplex _duplex = System.Drawing.Printing.Duplex.Vertical;
        private List<int> _firstRowIndexForPages = new List<int>();
        private bool _isPrintTail = true;
        private bool _landscape;
        private Margins _margin = new Margins(10, 10, 10, 10);
        private List<Header> _pageHeaders;
        private int _pageNo;
        private int _patientInfoHeight = 0x2d;
        private bool _printPageNo = true;
        private bool _printPrintTime;
        private bool _printWard;
        private CommonEntity.PatientInfo _pvisit;
        private Rectangle _rectBody;
        private Rectangle _rectPrintBound;
        private List<Header> _tableFoots;
        private int _tailHeight = 0x2d;

        public GridViewPrinter(GridView dataGridView)
        {
            this._dataGridView = dataGridView;
            this._pageHeaders = new List<Header>();
            this._tableFoots = new List<Header>();
            this._columnsPrintWidth = new List<int>();
            this.DefinedStringsInHeader = new List<DefineDrawingString>();
        }

        protected virtual void CountPageNos(Graphics g)
        {
            int top = this._rectPrintBound.Top;
            foreach (Header header in this._pageHeaders)
            {
                top += header.Height;
            }
            if (this._pvisit != null)
            {
                top += this._patientInfoHeight;
            }
            this._rectBody = new Rectangle(this._rectPrintBound.Left, top, this._rectPrintBound.Width, (this._rectPrintBound.Bottom - top) - this._tailHeight);
            int num2 = 0;
            foreach (DataGridViewColumn column in this._dataGridView.Columns)
            {
                if (column.Visible)
                {
                    num2 += column.Width;
                }
            }
            int width = this._rectBody.Width;
            foreach (DataGridViewColumn column2 in this._dataGridView.Columns)
            {
                if (!column2.Visible)
                {
                    this._columnsPrintWidth.Add(0);
                }
                else
                {
                    int num4 = (column2.Width * width) / num2;
                    this._columnsPrintWidth.Add(num4);
                }
            }
            this._columnHeaderPrintHeight = 0;
            foreach (DataGridViewColumn column3 in this._dataGridView.Columns)
            {
                if (!column3.Visible)
                {
                    this._columnsPrintWidth.Add(0);
                }
                else
                {
                    int num5 = (column3.Width * width) / num2;
                    this._columnsPrintWidth.Add(num5);
                    int num6 = (int) Math.Ceiling((double) g.MeasureString(column3.HeaderText, new Font(this._dataGridView.Appearance.HeaderPanel.Font, FontStyle.Bold), num5).Height);
                    this._columnHeaderPrintHeight = Math.Max(this._columnHeaderPrintHeight, num6);
                }
            }
            this._columnHeaderPrintHeight += 15;
            int item = 0;
            this._firstRowIndexForPages.Clear();
        Label_03DA:
            while (item < this._dataGridView.RowCount)
            {
                this._firstRowIndexForPages.Add(item);
                int num8 = this._rectBody.Top + this._columnHeaderPrintHeight;
                while (item < this._dataGridView.RowCount)
                {
                    int num9 = 0;
                    for (int i = 0; i < this._dataGridView.Columns.Count; i++)
                    {
                        if (this._dataGridView.Columns[i].Visible)
                        {
                            Font font = this._dataGridView.[i, item].Style.Font;
                            if (font == null)
                            {
                                font = this._dataGridView.Rows[item].DefaultCellStyle.Font;
                            }
                            if (font == null)
                            {
                                font = this._dataGridView.Columns[i].DefaultCellStyle.Font;
                            }
                            if (font == null)
                            {
                                font = this._dataGridView.DefaultCellStyle.Font;
                            }
                            DataGridViewCell cell = this._dataGridView[i, item];
                            string formattedValue = cell.FormattedValue as string;
                            int num11 = (int) Math.Ceiling((double) g.MeasureString(formattedValue, font, this._columnsPrintWidth[i]).Height);
                            num9 = Math.Max(num9, num11);
                        }
                    }
                    if (num9 > 0)
                    {
                        num9 += 5;
                    }
                    num9 = Math.Max(num9, this._dataGridView.RowTemplate.Height);
                    if ((num8 + num9) >= this._rectBody.Bottom)
                    {
                        goto Label_03DA;
                    }
                    num8 += num9;
                    item++;
                }
            }
        }

        private void FormatRows(Graphics g)
        {
            int num = 0;
            foreach (DataGridViewColumn column in this._dataGridView.Columns)
            {
                if (column.Visible)
                {
                    num += column.Width;
                }
            }
            int width = this._rectBody.Width;
            this._columnHeaderPrintHeight = 0;
            foreach (DataGridViewColumn column2 in this._dataGridView.Columns)
            {
                if (!column2.Visible)
                {
                    this._columnsPrintWidth.Add(0);
                }
                else
                {
                    int item = (column2.Width * width) / num;
                    this._columnsPrintWidth.Add(item);
                    int num4 = (int) Math.Ceiling((double) g.MeasureString(column2.HeaderText, new Font(this._dataGridView.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold), item).Height);
                    this._columnHeaderPrintHeight = Math.Max(this._columnHeaderPrintHeight, num4);
                }
            }
            this._columnHeaderPrintHeight += 15;
            if (this._columnsPrintWidth.Count != 0)
            {
                DataGridView view = new DataGridView {
                    DefaultCellStyle = this._dataGridView.DefaultCellStyle,
                    AllowUserToAddRows = false
                };
                foreach (DataGridViewColumn column3 in this._dataGridView.Columns)
                {
                    int num5 = view.Columns.Add("new" + column3.Name, column3.HeaderText);
                    view.Columns[num5].DefaultCellStyle = column3.DefaultCellStyle;
                    view.Columns[num5].Visible = column3.Visible;
                    view.Columns[num5].Width = column3.Width;
                }
                foreach (DataGridViewRow row in (IEnumerable) this._dataGridView.Rows)
                {
                    int count = view.Rows.Count;
                    for (int i = 0; i < this._dataGridView.Columns.Count; i++)
                    {
                        if (this._dataGridView.Columns[i].Visible)
                        {
                            Font font = this._dataGridView[i, this._currPrintRow].Style.Font;
                            if (font == null)
                            {
                                font = this._dataGridView.Rows[this._currPrintRow].DefaultCellStyle.Font;
                            }
                            if (font == null)
                            {
                                font = this._dataGridView.Columns[i].DefaultCellStyle.Font;
                            }
                            if (font == null)
                            {
                                font = this._dataGridView.DefaultCellStyle.Font;
                            }
                            DataGridViewCell cell = row.Cells[i];
                            string formattedValue = cell.FormattedValue as string;
                            float num8 = 0f;
                            int num9 = 0;
                            int startIndex = 0;
                            float num11 = 0f;
                            for (int j = 0; j < formattedValue.Length; j++)
                            {
                                float height = g.MeasureString(formattedValue.Substring(0, j + 1), font, this._columnsPrintWidth[i]).Height;
                                if ((height > 0f) && (num11 == 0f))
                                {
                                    num11 = height;
                                }
                                if ((height - num8) > (num11 / 2f))
                                {
                                    if (num8 != 0f)
                                    {
                                        if (view.Rows.Count <= (count + num9))
                                        {
                                            int num14 = view.Rows.Add();
                                            view.Rows[num14].DefaultCellStyle = row.DefaultCellStyle;
                                        }
                                        view[i, count + num9].Value = formattedValue.Substring(startIndex, j - startIndex);
                                        view[i, count + num9].Style = row.Cells[i].Style;
                                        num9++;
                                        startIndex = j;
                                    }
                                    num8 = height;
                                }
                            }
                            if (view.Rows.Count <= (count + num9))
                            {
                                int num15 = view.Rows.Add();
                                view.Rows[num15].DefaultCellStyle = row.DefaultCellStyle;
                            }
                            view[i, count + num9].Value = formattedValue.Substring(startIndex, formattedValue.Length - startIndex);
                            view[i, count + num9].Style = row.Cells[i].Style;
                        }
                    }
                }
                this._dataGridView = view;
            }
        }

        private void frmPreview_EndPrint(object sender, EventArgs e)
        {
        }

        public void Print(string printer)
        {
            this.Print(printer, false);
        }

        public void Print(string printer, bool isPreview)
        {
            this._currPrintRow = 0;
            this._currPrintTableFoot = 0;
            this._pageNo = 1;
            PrintDocument document = new PrintDocument {
                DocumentName = this._dataGridView.Name
            };
            document.PrinterSettings.PrinterName = printer;
            document.PrintController = new StandardPrintController();
            document.DefaultPageSettings.Margins = this._margin;
            document.DefaultPageSettings.Landscape = this._landscape;
            document.DefaultPageSettings.Margins = this._margin;
            document.PrintPage += new PrintPageEventHandler(this.printDoc_PrintPage);
            if (!isPreview)
            {
                document.Print();
            }
            else
            {
                PrintPreviewDialog dialog = new PrintPreviewDialog {
                    StartPosition = FormStartPosition.CenterParent,
                    Document = document,
                    ShowIcon = false
                };
                dialog.PrintPreviewControl.Zoom = 1.0;
                dialog.Width = 800;
                dialog.Height = 600;
                dialog.ShowDialog();
            }
        }

        private void PrintColumnsHeader(Graphics g)
        {
            int num = 0;
            foreach (DataGridViewColumn column in this._dataGridView.Columns)
            {
                if (column.Visible)
                {
                    num += column.Width;
                }
            }
            int width = this._rectBody.Width;
            this._columnHeaderPrintHeight = 0;
            foreach (DataGridViewColumn column2 in this._dataGridView.Columns)
            {
                if (!column2.Visible)
                {
                    this._columnsPrintWidth.Add(0);
                }
                else
                {
                    int item = (column2.Width * width) / num;
                    this._columnsPrintWidth.Add(item);
                    int num4 = (int) Math.Ceiling((double) g.MeasureString(column2.HeaderText, new Font(this._dataGridView.ColumnHeadersDefaultCellStyle.Font, FontStyle.Regular), item).Height);
                    this._columnHeaderPrintHeight = Math.Max(this._columnHeaderPrintHeight, num4);
                }
            }
            this._columnHeaderPrintHeight += 15;
            this._dataGridViewPrintLeft = this._rectBody.Left;
            using (Brush brush = new SolidBrush(Color.Black))
            {
                using (Pen pen = new Pen(brush))
                {
                    using (Font font = new Font(this._dataGridView.ColumnHeadersDefaultCellStyle.Font, FontStyle.Regular))
                    {
                        using (StringFormat format = new StringFormat())
                        {
                            format.Alignment = StringAlignment.Center;
                            format.LineAlignment = StringAlignment.Center;
                            int x = this._dataGridViewPrintLeft;
                            for (int i = 0; i < this._dataGridView.Columns.Count; i++)
                            {
                                if (this._dataGridView.Columns[i].Visible)
                                {
                                    Rectangle rect = new Rectangle(x, this._rectBody.Top, this._columnsPrintWidth[i], this._columnHeaderPrintHeight);
                                    g.DrawRectangle(pen, rect);
                                    g.DrawString(this._dataGridView.Columns[i].HeaderText, font, brush, rect, format);
                                    x += this._columnsPrintWidth[i];
                                }
                            }
                        }
                    }
                }
            }
        }

        private void PrintDefineStrings(List<DefineDrawingString> strs, Graphics g)
        {
            foreach (DefineDrawingString str in strs)
            {
                g.DrawString(str.StringText, str.StringFont, str.StringBrush, str.StringPosition);
                if (str.WithLine)
                {
                    SizeF ef = g.MeasureString(str.StringText, str.StringFont);
                    int num = (int) Math.Ceiling((double) ef.Width);
                    int num2 = (int) Math.Ceiling((double) ef.Height);
                    g.DrawLine(new Pen(Color.Black), new PointF(str.StringPosition.X, str.StringPosition.Y + num2), new PointF(str.StringPosition.X + num, str.StringPosition.Y + num2));
                }
            }
        }

        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            this._rectPrintBound = e.MarginBounds;
            if (this._currPrintRow == 0)
            {
                this.CountPageNos(e.Graphics);
            }
            PrintDocument document = sender as PrintDocument;
            if (document.PrinterSettings.PrintRange == PrintRange.SomePages)
            {
                if ((document.PrinterSettings.FromPage > 0) && (this._pageNo < document.PrinterSettings.FromPage))
                {
                    this._pageNo = document.PrinterSettings.FromPage;
                }
                if ((document.PrinterSettings.ToPage > 0) && (this._pageNo > document.PrinterSettings.ToPage))
                {
                    e.Cancel = true;
                    this._currPrintRow = 0;
                    this._currPrintTableFoot = 0;
                    this._pageNo = 1;
                    return;
                }
            }
            this.PrintHeaders(e.Graphics);
            this.PrintColumnsHeader(e.Graphics);
            if (this._pageNo > this._firstRowIndexForPages.Count)
            {
                e.Cancel = true;
                this._currPrintRow = 0;
                this._currPrintTableFoot = 0;
                this._pageNo = 1;
            }
            else
            {
                int y = this._rectBody.Top + this._columnHeaderPrintHeight;
                this.PrintRows(e.Graphics, ref y, this._pageNo);
                y += 10;
                this.PrintTableFoots(e.Graphics, y);
                this.PrintTail(e.Graphics);
                if (((document.PrinterSettings.PrintRange == PrintRange.SomePages) && (document.PrinterSettings.ToPage > 0)) && (this._pageNo >= document.PrinterSettings.ToPage))
                {
                    this._currPrintRow = 0;
                    this._currPrintTableFoot = 0;
                    this._pageNo = 1;
                }
                else if ((this._currPrintRow < this._dataGridView.Rows.Count) || (this._currPrintTableFoot < this._tableFoots.Count))
                {
                    e.HasMorePages = true;
                    this._pageNo++;
                }
                else
                {
                    this._currPrintRow = 0;
                    this._currPrintTableFoot = 0;
                    this._pageNo = 1;
                }
            }
        }

        private void PrintHeaders(Graphics g)
        {
            int top = this._rectPrintBound.Top;
            using (StringFormat format = new StringFormat())
            {
                using (Pen pen = new Pen(Color.Black))
                {
                    using (Brush brush = new SolidBrush(Color.Black))
                    {
                        format.Alignment = StringAlignment.Center;
                        format.LineAlignment = StringAlignment.Center;
                        foreach (Header header in this._pageHeaders)
                        {
                            Rectangle layoutRectangle = new Rectangle(this._rectPrintBound.Left, top, this._rectPrintBound.Width, header.Height);
                            format.Alignment = header.Alignment;
                            format.LineAlignment = header.LineAlignment;
                            g.DrawString(header.Text, header.Font, brush, layoutRectangle, format);
                            top += header.Height;
                        }
                        this.PrintDefineStrings(this.DefinedStringsInHeader, g);
                        Font font = new Font("宋体", 11f);
                        if (this._printWard)
                        {
                            int height = 30;
                            int width = 80;
                            int num4 = 160;
                            int left = this._rectPrintBound.Left;
                            Rectangle rectangle2 = new Rectangle(left, top, width, height);
                            format.Alignment = StringAlignment.Near;
                            format.LineAlignment = StringAlignment.Center;
                            g.DrawString("科室名称:", font, Brushes.Black, rectangle2, format);
                            left += width;
                            rectangle2 = new Rectangle(left, top, num4, height);
                            g.DrawString(HISPlus.GVars.User.DeptName, font, Brushes.Black, rectangle2, format);
                            g.DrawLine(Pens.Black, rectangle2.Left, rectangle2.Bottom - 10, rectangle2.Right, rectangle2.Bottom - 10);
                            top += height;
                        }
                        if (this._pvisit != null)
                        {
                            int num6 = 40;
                            int num7 = 120;
                            int num8 = 60;
                            int num9 = 190;
                            int num10 = 60;
                            int num11 = 40;
                            int num12 = 80;
                            int num13 = 120;
                            int num14 = ((((((num6 + num7) + num8) + num9) + num10) + num11) + num12) + num13;
                            int x = ((this._rectPrintBound.Width - num14) / 2) + this._rectPrintBound.Left;
                            Rectangle rectangle3 = new Rectangle(x, top, num6, this._patientInfoHeight);
                            g.DrawString("姓名", font, brush, rectangle3, format);
                            x += num6;
                            rectangle3 = new Rectangle(x, top, num7, this._patientInfoHeight);
                            g.DrawString(this._pvisit.Name, font, brush, rectangle3, format);
                            g.DrawLine(pen, rectangle3.Left, rectangle3.Bottom - 12, rectangle3.Right, rectangle3.Bottom - 12);
                            x += num7;
                            rectangle3 = new Rectangle(x, top, num8, this._patientInfoHeight);
                            g.DrawString("科室", font, brush, rectangle3, format);
                            x += num8;
                            
                            rectangle3 = new Rectangle(x, top, num9, this._patientInfoHeight);
                            g.DrawString(this._pvisit.DeptName, font, brush, rectangle3, format);
                            g.DrawLine(pen, rectangle3.Left, rectangle3.Bottom - 12, rectangle3.Right, rectangle3.Bottom - 12);
                            x += num9;
                            rectangle3 = new Rectangle(x, top, num10, this._patientInfoHeight);
                            g.DrawString(" 床号", font, brush, rectangle3, format);
                            x += num10;
                            rectangle3 = new Rectangle(x, top, num11, this._patientInfoHeight);
                            g.DrawString(this._pvisit.BedNo.ToString(), font, brush, rectangle3, format);
                            g.DrawLine(pen, rectangle3.Left, rectangle3.Bottom - 12, rectangle3.Right, rectangle3.Bottom - 12);
                            x += num11;
                            rectangle3 = new Rectangle(x, top, num12, this._patientInfoHeight);
                            g.DrawString("  住院号", font, brush, rectangle3, format);
                            x += num12;
                            rectangle3 = new Rectangle(x, top, num13, this._patientInfoHeight);
                            g.DrawString(this._pvisit.InpNo, font, brush, rectangle3, format);
                            g.DrawLine(pen, rectangle3.Left, rectangle3.Bottom - 12, rectangle3.Right, rectangle3.Bottom - 12);
                            x += num13;
                            top += this._patientInfoHeight;
                        }
                    }
                }
            }
            this._rectBody = new Rectangle(this._rectPrintBound.Left, top, this._rectPrintBound.Width, (this._rectPrintBound.Bottom - top) - this._tailHeight);
        }

        private void PrintRows(Graphics g, ref int y)
        {
            while (this._currPrintRow < this._dataGridView.Rows.Count)
            {
                int num = 0;
                for (int i = 0; i < this._dataGridView.Columns.Count; i++)
                {
                    if (this._dataGridView.Columns[i].Visible)
                    {
                        Font font = this._dataGridView[i, this._currPrintRow].Style.Font;
                        if (font == null)
                        {
                            font = this._dataGridView.Rows[this._currPrintRow].DefaultCellStyle.Font;
                        }
                        if (font == null)
                        {
                            font = this._dataGridView.Columns[i].DefaultCellStyle.Font;
                        }
                        if (font == null)
                        {
                            font = this._dataGridView.DefaultCellStyle.Font;
                        }
                        DataGridViewCell cell = this._dataGridView[i, this._currPrintRow];
                        string formattedValue = cell.FormattedValue as string;
                        int num3 = (int) Math.Ceiling((double) g.MeasureString(formattedValue, font, this._columnsPrintWidth[i]).Height);
                        num = Math.Max(num, num3);
                    }
                }
                if (num > 0)
                {
                    num += 5;
                }
                num = Math.Max(num, this._dataGridView.RowTemplate.Height);
                if ((y + num) >= this._rectBody.Bottom)
                {
                    return;
                }
                using (Brush brush = new SolidBrush(Color.Black))
                {
                    using (Pen pen = new Pen(brush))
                    {
                        using (StringFormat format = new StringFormat())
                        {
                            format.Alignment = StringAlignment.Center;
                            format.LineAlignment = StringAlignment.Center;
                            int x = this._dataGridViewPrintLeft;
                            for (int j = 0; j < this._dataGridView.Columns.Count; j++)
                            {
                                if (!this._dataGridView.Columns[j].Visible)
                                {
                                    continue;
                                }
                                DataGridViewContentAlignment alignment = this._dataGridView[j, this._currPrintRow].Style.Alignment;
                                if (alignment == DataGridViewContentAlignment.NotSet)
                                {
                                    alignment = this._dataGridView.Columns[j].DefaultCellStyle.Alignment;
                                }
                                if (alignment == DataGridViewContentAlignment.NotSet)
                                {
                                    alignment = this._dataGridView.DefaultCellStyle.Alignment;
                                }
                                DataGridViewContentAlignment alignment2 = alignment;
                                if (alignment2 <= DataGridViewContentAlignment.MiddleCenter)
                                {
                                    switch (alignment2)
                                    {
                                        case DataGridViewContentAlignment.TopLeft:
                                        case DataGridViewContentAlignment.TopCenter:
                                        case DataGridViewContentAlignment.TopRight:
                                            format.LineAlignment = StringAlignment.Near;
                                            goto Label_0260;

                                        case (DataGridViewContentAlignment.TopCenter | DataGridViewContentAlignment.TopLeft):
                                            goto Label_0260;

                                        case DataGridViewContentAlignment.MiddleLeft:
                                        case DataGridViewContentAlignment.MiddleCenter:
                                            goto Label_024E;
                                    }
                                    goto Label_0260;
                                }
                                if (alignment2 <= DataGridViewContentAlignment.BottomLeft)
                                {
                                    switch (alignment2)
                                    {
                                        case DataGridViewContentAlignment.MiddleRight:
                                            goto Label_024E;

                                        case DataGridViewContentAlignment.BottomLeft:
                                            goto Label_0244;
                                    }
                                    goto Label_0260;
                                }
                                if ((alignment2 != DataGridViewContentAlignment.BottomCenter) && (alignment2 != DataGridViewContentAlignment.BottomRight))
                                {
                                    goto Label_0260;
                                }
                            Label_0244:
                                format.LineAlignment = StringAlignment.Far;
                                goto Label_0260;
                            Label_024E:
                                format.LineAlignment = StringAlignment.Center;
                            Label_0260:
                                switch (alignment)
                                {
                                    case DataGridViewContentAlignment.TopLeft:
                                    case DataGridViewContentAlignment.MiddleLeft:
                                    case DataGridViewContentAlignment.BottomLeft:
                                        format.Alignment = StringAlignment.Near;
                                        break;

                                    case DataGridViewContentAlignment.TopCenter:
                                    case DataGridViewContentAlignment.MiddleCenter:
                                    case DataGridViewContentAlignment.BottomCenter:
                                        format.Alignment = StringAlignment.Center;
                                        break;

                                    case DataGridViewContentAlignment.TopRight:
                                    case DataGridViewContentAlignment.BottomRight:
                                    case DataGridViewContentAlignment.MiddleRight:
                                        format.Alignment = StringAlignment.Far;
                                        break;
                                }
                                Font font2 = this._dataGridView[j, this._currPrintRow].Style.Font;
                                if (font2 == null)
                                {
                                    font2 = this._dataGridView.Rows[this._currPrintRow].DefaultCellStyle.Font;
                                }
                                if (font2 == null)
                                {
                                    font2 = this._dataGridView.Columns[j].DefaultCellStyle.Font;
                                }
                                if (font2 == null)
                                {
                                    font2 = this._dataGridView.DefaultCellStyle.Font;
                                }
                                DataGridViewCell cell2 = this._dataGridView[j, this._currPrintRow];
                                string s = cell2.FormattedValue as string;
                                Rectangle rect = new Rectangle(x, y, this._columnsPrintWidth[j], num);
                                g.DrawRectangle(pen, rect);
                                g.DrawString(s, font2, brush, rect, format);
                                x += this._columnsPrintWidth[j];
                            }
                        }
                    }
                }
                y += num;
                this._currPrintRow++;
            }
        }

        private void PrintRows(Graphics g, ref int y, int pageNo)
        {
            if (this._firstRowIndexForPages.Count == 0)
            {
                this._currPrintRow = 0;
            }
            else
            {
                this._currPrintRow = this._firstRowIndexForPages[pageNo - 1];
            }
            while ((this._currPrintRow < this._dataGridView.Rows.Count) && ((this._firstRowIndexForPages.Count <= pageNo) || ((this._firstRowIndexForPages.Count > pageNo) && (this._currPrintRow < this._firstRowIndexForPages[pageNo]))))
            {
                int num = 0;
                for (int i = 0; i < this._dataGridView.Columns.Count; i++)
                {
                    if (this._dataGridView.Columns[i].Visible)
                    {
                        Font font = this._dataGridView[i, this._currPrintRow].Style.Font;
                        if (font == null)
                        {
                            font = this._dataGridView.Rows[this._currPrintRow].DefaultCellStyle.Font;
                        }
                        if (font == null)
                        {
                            font = this._dataGridView.Columns[i].DefaultCellStyle.Font;
                        }
                        if (font == null)
                        {
                            font = this._dataGridView.DefaultCellStyle.Font;
                        }
                        DataGridViewCell cell = this._dataGridView[i, this._currPrintRow];
                        string formattedValue = cell.FormattedValue as string;
                        int num3 = (int) Math.Ceiling((double) g.MeasureString(formattedValue, font, this._columnsPrintWidth[i]).Height);
                        num = Math.Max(num, num3);
                    }
                }
                if (num > 0)
                {
                    num += 5;
                }
                num = Math.Max(num, this._dataGridView.RowTemplate.Height);
                using (Brush brush = new SolidBrush(Color.Black))
                {
                    using (Pen pen = new Pen(brush))
                    {
                        using (StringFormat format = new StringFormat())
                        {
                            format.Alignment = StringAlignment.Center;
                            format.LineAlignment = StringAlignment.Center;
                            int x = this._dataGridViewPrintLeft;
                            for (int j = 0; j < this._dataGridView.Columns.Count; j++)
                            {
                                if (!this._dataGridView.Columns[j].Visible)
                                {
                                    continue;
                                }
                                DataGridViewContentAlignment alignment = this._dataGridView[j, this._currPrintRow].Style.Alignment;
                                if (alignment == DataGridViewContentAlignment.NotSet)
                                {
                                    alignment = this._dataGridView.Columns[j].DefaultCellStyle.Alignment;
                                }
                                if (alignment == DataGridViewContentAlignment.NotSet)
                                {
                                    alignment = this._dataGridView.DefaultCellStyle.Alignment;
                                }
                                DataGridViewContentAlignment alignment2 = alignment;
                                if (alignment2 <= DataGridViewContentAlignment.MiddleCenter)
                                {
                                    switch (alignment2)
                                    {
                                        case DataGridViewContentAlignment.TopLeft:
                                        case DataGridViewContentAlignment.TopCenter:
                                        case DataGridViewContentAlignment.TopRight:
                                            format.LineAlignment = StringAlignment.Near;
                                            goto Label_027B;

                                        case (DataGridViewContentAlignment.TopCenter | DataGridViewContentAlignment.TopLeft):
                                            goto Label_027B;

                                        case DataGridViewContentAlignment.MiddleLeft:
                                        case DataGridViewContentAlignment.MiddleCenter:
                                            goto Label_0269;
                                    }
                                    goto Label_027B;
                                }
                                if (alignment2 <= DataGridViewContentAlignment.BottomLeft)
                                {
                                    switch (alignment2)
                                    {
                                        case DataGridViewContentAlignment.MiddleRight:
                                            goto Label_0269;

                                        case DataGridViewContentAlignment.BottomLeft:
                                            goto Label_025F;
                                    }
                                    goto Label_027B;
                                }
                                if ((alignment2 != DataGridViewContentAlignment.BottomCenter) && (alignment2 != DataGridViewContentAlignment.BottomRight))
                                {
                                    goto Label_027B;
                                }
                            Label_025F:
                                format.LineAlignment = StringAlignment.Far;
                                goto Label_027B;
                            Label_0269:
                                format.LineAlignment = StringAlignment.Center;
                            Label_027B:
                                switch (alignment)
                                {
                                    case DataGridViewContentAlignment.TopLeft:
                                    case DataGridViewContentAlignment.MiddleLeft:
                                    case DataGridViewContentAlignment.BottomLeft:
                                        format.Alignment = StringAlignment.Near;
                                        break;

                                    case DataGridViewContentAlignment.TopCenter:
                                    case DataGridViewContentAlignment.MiddleCenter:
                                    case DataGridViewContentAlignment.BottomCenter:
                                        format.Alignment = StringAlignment.Center;
                                        break;

                                    case DataGridViewContentAlignment.TopRight:
                                    case DataGridViewContentAlignment.BottomRight:
                                    case DataGridViewContentAlignment.MiddleRight:
                                        format.Alignment = StringAlignment.Far;
                                        break;
                                }
                                Font font2 = this._dataGridView[j, this._currPrintRow].Style.Font;
                                if (font2 == null)
                                {
                                    font2 = this._dataGridView.Rows[this._currPrintRow].DefaultCellStyle.Font;
                                }
                                if (font2 == null)
                                {
                                    font2 = this._dataGridView.Columns[j].DefaultCellStyle.Font;
                                }
                                if (font2 == null)
                                {
                                    font2 = this._dataGridView.DefaultCellStyle.Font;
                                }
                                DataGridViewCell cell2 = this._dataGridView[j, this._currPrintRow];
                                string s = cell2.FormattedValue as string;
                                Rectangle destRect = new Rectangle(x, y, this._columnsPrintWidth[j], num);
                                if (this._dataGridView.Columns[j].Name.ToLower().IndexOf("colsignature") >= 0)
                                {
                                    //if (cell2.Tag != null)
                                    //{
                                    //    string key = cell2.Tag.ToString();
                                    //    if (Global.SignatureList.ContainsKey(key))
                                    //    {
                                    //        WSUserImage[] imageArray = (WSUserImage[]) Global.SignatureList[key];
                                    //        if ((imageArray != null) && (imageArray.Length > 0))
                                    //        {
                                    //            WSUserImage image = imageArray[0];
                                    //            if ((image.signature != null) && (image.signature.Length > 0))
                                    //            {
                                    //                MemoryStream stream = new MemoryStream(image.signature);
                                    //                Global.DrawSignature(destRect, Image.FromStream(stream), g, StringAlignment.Center, StringAlignment.Center);
                                    //            }
                                    //            else
                                    //            {
                                    //                g.DrawString(s, font2, brush, destRect, format);
                                    //            }
                                    //        }
                                    //        else
                                    //        {
                                    //            g.DrawString(s, font2, brush, destRect, format);
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        WSUserImage[] signature = WSController.GetSignature(key);
                                    //        Global.SignatureList.Add(key, signature);
                                    //        if ((signature != null) && (signature.Length > 0))
                                    //        {
                                    //            WSUserImage image2 = signature[0];
                                    //            if ((image2.signature != null) && (image2.signature.Length > 0))
                                    //            {
                                    //                MemoryStream stream2 = new MemoryStream(image2.signature);
                                    //                Image image3 = Image.FromStream(stream2);
                                    //                Global.DrawSignature(destRect, image3, g, StringAlignment.Center, StringAlignment.Center);
                                    //            }
                                    //        }
                                    //        else
                                    //        {
                                    //            g.DrawString(s, font2, brush, destRect, format);
                                    //        }
                                    //    }
                                    //}
                                }
                                else
                                {
                                    g.DrawString(s, font2, brush, destRect, format);
                                }
                                g.DrawRectangle(pen, destRect);
                                x += this._columnsPrintWidth[j];
                            }
                        }
                    }
                }
                y += num;
                this._currPrintRow++;
            }
        }

        private void PrintTableFoots(Graphics g, int y)
        {
            StringFormat format = new StringFormat();
            while (this._currPrintTableFoot < this._tableFoots.Count)
            {
                if ((y + this._tableFoots[this._currPrintTableFoot].Height) > this._rectBody.Bottom)
                {
                    break;
                }
                Rectangle layoutRectangle = new Rectangle(this._rectBody.Left, y, this._rectBody.Width, this._tableFoots[this._currPrintTableFoot].Height);
                format.Alignment = this._tableFoots[this._currPrintTableFoot].Alignment;
                format.LineAlignment = this._tableFoots[this._currPrintTableFoot].LineAlignment;
                g.DrawString(this._tableFoots[this._currPrintTableFoot].Text, this._tableFoots[this._currPrintTableFoot].Font, Brushes.Black, layoutRectangle, format);
                y += this._tableFoots[this._currPrintTableFoot].Height;
                this._currPrintTableFoot++;
            }
        }

        private void PrintTail(Graphics g)
        {
            using (StringFormat format = new StringFormat())
            {
                using (StringFormat format2 = new StringFormat())
                {
                    using (new Pen(Color.Black))
                    {
                        using (Brush brush = new SolidBrush(Color.Black))
                        {
                            format.Alignment = StringAlignment.Center;
                            format.LineAlignment = StringAlignment.Center;
                            format2.Alignment = StringAlignment.Far;
                            format2.LineAlignment = StringAlignment.Center;
                            Rectangle layoutRectangle = new Rectangle(this._rectPrintBound.Left, this._rectBody.Bottom, this._rectPrintBound.Width, this._tailHeight);
                            if (this._printPageNo)
                            {
                                g.DrawString("第" + this._pageNo.ToString() + "页", new Font("Arial Unicode MS", 11f), brush, layoutRectangle, format);
                            }
                        }
                    }
                }
            }
        }

        public MemoryStream[] PrintToImages(Size size, float sizeOfNomal)
        {
            List<MemoryStream> list = new List<MemoryStream>();
            if (this._pvisit != null)
            {
                bool flag = true;
                while (flag)
                {
                    Bitmap image = new Bitmap(size.Width, size.Height);
                    Graphics g = Graphics.FromImage(image);
                    g.Clear(Color.White);
                    g.DrawImage(image, 0, 0);
                    g.ScaleTransform(sizeOfNomal, sizeOfNomal);
                    this._rectPrintBound = new Rectangle(20, 20, image.Width - 40, image.Height - 40);
                    if (this._currPrintRow == 0)
                    {
                        this.CountPageNos(g);
                    }
                    this.PrintHeaders(g);
                    this.PrintColumnsHeader(g);
                    if (this._pageNo > this._firstRowIndexForPages.Count)
                    {
                        this._currPrintRow = 0;
                        this._currPrintTableFoot = 0;
                        this._pageNo = 1;
                    }
                    else
                    {
                        if (this._pageNo == 0)
                        {
                            this._pageNo = 1;
                        }
                        int y = this._rectBody.Top + this._columnHeaderPrintHeight;
                        this.PrintRows(g, ref y, this._pageNo);
                        y += 10;
                        this.PrintTableFoots(g, y);
                        this.PrintTail(g);
                        if ((this._currPrintRow < this._dataGridView.Rows.Count) || (this._currPrintTableFoot < this._tableFoots.Count))
                        {
                            this._pageNo++;
                        }
                        else
                        {
                            this._currPrintRow = 0;
                            this._currPrintTableFoot = 0;
                            this._pageNo = 1;
                            flag = false;
                        }
                        MemoryStream stream = new MemoryStream();
                        image.Save(stream, ImageFormat.Bmp);
                        g.Dispose();
                        image.Dispose();
                        list.Add(stream);
                    }
                }
            }
            return list.ToArray();
        }

        public List<DefineDrawingString> DefinedStringsInHeader { get; set; }

        public System.Drawing.Printing.Duplex Duplex
        {
            get
            {
                return this._duplex;
            }
            set
            {
                this._duplex = value;
            }
        }

        public bool IsPrintTail
        {
            get
            {
                return this._isPrintTail;
            }
            set
            {
                this._isPrintTail = value;
            }
        }

        public bool Landscape
        {
            get
            {
                return this._landscape;
            }
            set
            {
                this._landscape = value;
            }
        }

        public Margins Margin
        {
            get
            {
                return this._margin;
            }
            set
            {
                this._margin = value;
            }
        }

        public List<Header> PageHeaders
        {
            get
            {
                return this._pageHeaders;
            }
        }

        public bool PrintPageNo
        {
            get
            {
                return this._printPageNo;
            }
            set
            {
                this._printPageNo = false;
            }
        }

        public bool PrintPrintTime
        {
            get
            {
                return this._printPrintTime;
            }
            set
            {
                this._printPrintTime = value;
            }
        }

        public bool PrintWard
        {
            get
            {
                return this._printWard;
            }
            set
            {
                this._printWard = value;
            }
        }

        public PatientInfo PVisit
        {
            get
            {
                return this._pvisit;
            }
            set
            {
                this._pvisit = value;
            }
        }

        public List<Header> TableFoots
        {
            get
            {
                return this._tableFoots;
            }
        }

        public class DefineDrawingString
        {
            public DefineDrawingString()
            {
            }

            public DefineDrawingString(string strText, Font font, Brush brush, PointF point, bool withLine) : this()
            {
                this.StringText = strText;
                this.StringFont = font;
                this.StringBrush = brush;
                this.StringPosition = point;
                this.WithLine = withLine;
            }

            public Brush StringBrush { get; set; }

            public Font StringFont { get; set; }

            public PointF StringPosition { get; set; }

            public string StringText { get; set; }

            public bool WithLine { get; set; }
        }

        public class Header
        {
            private StringAlignment _alignment = StringAlignment.Center;
            private System.Drawing.Font _font;
            private int _height;
            private StringAlignment _lineAlignment = StringAlignment.Center;
            private string _text;

            public Header(string text, System.Drawing.Font font, int height)
            {
                this._text = text;
                this._font = font;
                this._height = height;
            }

            public StringAlignment Alignment
            {
                get
                {
                    return this._alignment;
                }
                set
                {
                    this._alignment = value;
                }
            }

            public System.Drawing.Font Font
            {
                get
                {
                    return this._font;
                }
            }

            public int Height
            {
                get
                {
                    return this._height;
                }
            }

            public StringAlignment LineAlignment
            {
                get
                {
                    return this._lineAlignment;
                }
                set
                {
                    this._lineAlignment = value;
                }
            }

            public string Text
            {
                get
                {
                    return this._text;
                }
            }
        }
    }
}

