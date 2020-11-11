using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace HISPlus.UserControls
{
    public class UcCheckGridView : UcGridView
    {
        private CheckEdit _checkAll;

        /// <summary>
        /// 复选框列的列名(包含全选按钮)
        /// </summary>
        private string _allCheckedName;

        protected ArrayList selection;

        /// <summary>
        /// 在 Grid 上，首列复选框更改时发生
        /// </summary>
        public event EventHandler CheckedChanged;

        private bool _userInput = true;

        public UcCheckGridView()
        {
            selection = new ArrayList();
        }

        /// <summary>
        /// 将列添加到集合中。（仅用于创建首列且包含有全选按钮时）
        /// </summary>
        /// <param name="caption">列标题单元格的标题文本。</param>
        /// <param name="fieldName">绑定的数据库列的名称。</param>
        /// <param name="showAllChecks"></param>
        public void AddCheckBoxColumn(string fieldName, string caption = "选择", bool showAllChecks = true)
        {
            GridView view = gvDefault;

            _checkAll = new CheckEdit { Checked = true, BackColor = Color.Transparent, Text = string.Empty };
            _checkAll.CheckStateChanged += checkAll_CheckStateChanged;
            // 允许三态
            _checkAll.Properties.AllowGrayed = true;

            RepositoryItemCheckEdit checkEdit =
                (RepositoryItemCheckEdit)view.GridControl.RepositoryItems.Add("CheckEdit");
            checkEdit.NullStyle = StyleIndeterminate.Unchecked;

            GridColumn checkColumn = base.NewColumn(caption, _allCheckedName);

            checkColumn.OptionsColumn.AllowSort = DefaultBoolean.False;
            checkColumn.VisibleIndex = int.MaxValue;
            checkColumn.FieldName = _allCheckedName;
            //_checkColumn.Caption = @"选择";
            checkColumn.OptionsColumn.ShowCaption = false;
            checkColumn.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            checkColumn.ColumnEdit = checkEdit;

            checkColumn.Caption = caption;
            checkColumn.FieldName = fieldName;
            checkColumn.Width = 30;
            checkColumn.Visible = true;
            checkColumn.GroupIndex = -1;

            _allCheckedName = fieldName;

            checkColumn.OptionsColumn.AllowEdit = true;
            checkColumn.OptionsColumn.ReadOnly = false;
            checkColumn.OptionsColumn.AllowSize = true;
            checkColumn.OptionsColumn.ShowCaption = false;

            gvDefault.Columns.Add(checkColumn);
            gvDefault.Columns[fieldName].Visible = true;

            if (showAllChecks)
            {
                //checkEdit.EditValueChanged += Edit_EditValueChanged;
                view.CustomDrawColumnHeader += View_CustomDrawColumnHeader;
                gcDefault.DataSourceChanged += view_DataSourceChanged;
            }

            this.gvDefault.DoubleClick += this.gvDefault_DoubleClick;
            this.gvDefault.RowCellClick += gvDefault_RowCellClick;
        }

        void view_DataSourceChanged(object sender, EventArgs e)
        {
            if (gvDefault.RowCount == 0) return;

            _checkAll.Checked = true;
            //bool b = bool.Parse(gvDefault.GetRowCellValue(gvDefault.FocusedRowHandle, _allCheckedName).ToString());
            //CheckEquals(gvDefault.FocusedRowHandle, b);
        }

        void gvDefault_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            // 状态列[可用|不可用]单击即可更改
            if (e.Column.FieldName == _allCheckedName)
            {
                bool blnStore = _userInput;
                try
                {
                    _userInput = false;

                    bool b = bool.Parse(gvDefault.GetRowCellValue(e.RowHandle, _allCheckedName).ToString());
                    gvDefault.SetRowCellValue(e.RowHandle, _allCheckedName, !b);

                    CheckEquals(gvDefault.FocusedRowHandle, !b);

                    if (CheckedChanged != null)
                        CheckedChanged(sender, e);
                }
                catch (Exception ex)
                {
                    Error.ErrProc(ex);
                }
                finally
                {
                    _userInput = blnStore;
                }
            }

        }

        private void gvDefault_DoubleClick(object sender, EventArgs e)
        {
            bool blnStore = _userInput;
            try
            {
                _userInput = false;
                Point pt = gvDefault.GridControl.PointToClient(MousePosition);
                GridHitInfo info = gvDefault.CalcHitInfo(pt);
                if (info.InRow && info.Column != null)
                {
                    bool b = bool.Parse(gvDefault.GetRowCellValue(info.RowHandle, _allCheckedName).ToString());
                    gvDefault.SetRowCellValue(info.RowHandle, _allCheckedName, !b);
                    CheckEquals(info.RowHandle, !b);
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                _userInput = blnStore;
            }
        }

        void checkAll_CheckStateChanged(object sender, EventArgs e)
        {
            if (_userInput == false)
            {
                return;
            }
            if (_checkAll.CheckState == CheckState.Indeterminate)
            {
                _checkAll.Checked = false;
                return;
            }
           
            //Point pt = gvDefault.GridControl.PointToClient(Control.MousePosition);
            //GridHitInfo info = gvDefault.CalcHitInfo(pt);
            //if (info.Column.FieldName == _allCheckedName)
            //{
            //    if (info.InColumn)
            //    {
            //        if (SelectedCount == gvDefault.DataRowCount)
            //            ClearSelection();
            //        else
            //            SelectAll();
            //    }
            //    if (info.InRowCell)
            //    {
            //        InvertRowSelection(info.RowHandle);
            //    }
            //}
            //if (info.InRow && gvDefault.IsGroupRow(info.RowHandle) && info.HitTest != GridHitTest.RowGroupButton)
            //{
            //    InvertRowSelection(info.RowHandle);
            //}
            //return;
            //if (SelectedCount == gvDefault.DataRowCount)
            //    ClearSelection();
            //else
            //    SelectAll();

            SelectAll();
        }

        private void DrawCheckBox(Rectangle r)
        {
            gcDefault.Controls.Add(_checkAll);
            _checkAll.Location = new Point(r.Location.X + r.Width / 2 - 10, r.Location.Y + 2);
            _checkAll.BringToFront();
        }

        private void View_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column.FieldName != _allCheckedName) return;

            e.Info.InnerElements.Clear();
            e.Painter.DrawObject(e.Info);
            DrawCheckBox(e.Bounds);
            e.Handled = true;
        }

        private void SelectAll()
        {
            this.Cursor = Cursors.WaitCursor;
            for (int i = 0; i < gvDefault.RowCount; i++)
            {
                gvDefault.SetRowCellValue(i, _allCheckedName, _checkAll.Checked);
            }
            this.Cursor = Cursors.Default;
        }

        //public void SelectAll()
        //{
        //    selection.Clear();
        //    // fast (won't work if the grid is filtered)
        //    var source = gvDefault.DataSource as ICollection;
        //    if (source != null)
        //        selection.AddRange(source);
        //    else
        //        // slow:
        //        for (int i = 0; i < gvDefault.DataRowCount; i++)
        //            selection.Add(gvDefault.GetRow(i));
        //    Invalidate();
        //}

        //public void SelectRow(int rowHandle, bool select)
        //{
        //    SelectRow(rowHandle, select, true);
        //}
        //void SelectRow(int rowHandle, bool select, bool invalidate)
        //{
        //    if (IsRowSelected(rowHandle) == select) return;
        //    object row = gvDefault.GetRow(rowHandle);
        //    if (select)
        //        selection.Add(row);
        //    else
        //        selection.Remove(row);
        //    if (invalidate)
        //    {
        //        Invalidate();
        //    }
        //}

        //public void InvertRowSelection(int rowHandle)
        //{
        //    if (gvDefault.IsDataRow(rowHandle))
        //    {
        //        SelectRow(rowHandle, !IsRowSelected(rowHandle));
        //    }
        //}
        //public bool IsGroupRowSelected(int rowHandle)
        //{
        //    for (int i = 0; i < gvDefault.GetChildRowCount(rowHandle); i++)
        //    {
        //        int row = gvDefault.GetChildRowHandle(rowHandle, i);
        //        if (gvDefault.IsGroupRow(row))
        //        {
        //            if (!IsGroupRowSelected(row)) return false;
        //        }
        //        else
        //            if (!IsRowSelected(row)) return false;
        //    }
        //    return true;
        //}
        //public bool IsRowSelected(int rowHandle)
        //{
        //    if (gvDefault.IsGroupRow(rowHandle))
        //        return IsGroupRowSelected(rowHandle);

        //    object row = gvDefault.GetRow(rowHandle);
        //    return GetSelectedIndex(row) != -1;
        //}

        private void CheckEquals(int rowHandle, bool check)
        {
            if (!check)
                _checkAll.Checked = false;

            if (DicColumnsEvenOldRowColor != null && DicRowColors != null)
            {
                int i = rowHandle;

                Color color = DicRowColors[i];

                while (i > 0 && DicRowColors[i - 1] == color)
                {
                    //SelectRow(i - 1, check);
                    gvDefault.SetRowCellValue(i - 1, _allCheckedName, check);
                    i--;
                }

                i = rowHandle;
                while (i < DicRowColors.Count - 1 && DicRowColors[i + 1] == color)
                {
                    //SelectRow(i + 1, check);
                    gvDefault.SetRowCellValue(i + 1, _allCheckedName, check);
                    i++;
                }
            }

            if (check)
            {
                _checkAll.CheckState = CheckState.Indeterminate;
                for (int i = 0; i < gvDefault.RowCount; i++)
                {
                    if (i == rowHandle) continue;
                    object o = gvDefault.GetRowCellValue(i, _allCheckedName);
                    bool b;
                    bool.TryParse(o.ToString(), out b);

                    if (!b) return;
                }

                _checkAll.CheckState = CheckState.Checked;
            }
            else
            {
                for (int i = 0; i < gvDefault.RowCount; i++)
                {
                    if (i == rowHandle) continue;
                    object o = gvDefault.GetRowCellValue(i, _allCheckedName);
                    bool b;
                    bool.TryParse(o.ToString(), out b);

                    if (b)
                    {
                        _checkAll.CheckState = CheckState.Indeterminate;
                        return;
                    }
                }
            }
        }

        private void Edit_EditValueChanged(object sender, EventArgs e)
        {
            bool blnStore = _userInput;
            try
            {
                _userInput = false;

                CheckEdit check = sender as CheckEdit;
                if (check == null) return;

                if (CheckedChanged != null)
                    CheckedChanged(sender, e);

                //if (check.Checked)
                //{
                //    gvDefault.Appearance.SelectedRow.BackColor = Color.FromArgb(30, 0, 0, 240); gvDefault.Appearance.FocusedRow.BackColor = Color.FromArgb(60, 0, 0, 240);
                //}
                //else
                //{
                //    gvDefault.Appearance.SelectedRow.Reset();
                //    gvDefault.Appearance.FocusedRow.Reset();
                //}

                CheckEquals(gvDefault.FocusedRowHandle, check.Checked);

                return;

                if (!check.Checked)
                    _checkAll.Checked = false;

                if (DicColumnsEvenOldRowColor != null && DicRowColors != null)
                {
                    int i = gvDefault.FocusedRowHandle;

                    Color color = DicRowColors[i];

                    while (i > 0 && DicRowColors[i - 1] == color)
                    {
                        gvDefault.SetRowCellValue(i - 1, _allCheckedName, check.Checked);
                        i--;
                    }

                    i = gvDefault.FocusedRowHandle;
                    while (i < DicRowColors.Count - 1 && DicRowColors[i + 1] == color)
                    {
                        gvDefault.SetRowCellValue(i + 1, _allCheckedName, check.Checked);
                        i++;
                    }
                }

                if (check.Checked)
                {
                    _checkAll.CheckState = CheckState.Indeterminate;
                    for (int i = 0; i < gvDefault.RowCount; i++)
                    {
                        if (i == gvDefault.FocusedRowHandle) continue;
                        object o = gvDefault.GetRowCellValue(i, _allCheckedName);
                        bool b;
                        bool.TryParse(o.ToString(), out b);

                        if (!b) return;
                    }

                    _checkAll.CheckState = CheckState.Checked;
                }
                else
                {
                    for (int i = 0; i < gvDefault.RowCount; i++)
                    {
                        if (i == gvDefault.FocusedRowHandle) continue;
                        object o = gvDefault.GetRowCellValue(i, _allCheckedName);
                        bool b;
                        bool.TryParse(o.ToString(), out b);

                        if (b)
                        {
                            _checkAll.CheckState = CheckState.Indeterminate;
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                _userInput = blnStore;
            }
        }

        //public int SelectedCount { get { return selection.Count; } }
        public int CheckedCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < gvDefault.RowCount; i++)
                {
                    object o = gvDefault.GetRowCellValue(i, _allCheckedName);
                    bool b;
                    bool.TryParse(o.ToString(), out b);
                    if (b) count++;

                }
                return count;
            }
        }

        private object GetSelectedRow(int index)
        {
            return selection[index];
        }

        //private int GetSelectedIndex(object row)
        //{
        //    return selection.IndexOf(row);
        //}

        //private new void ClearSelection()
        //{
        //    selection.Clear();
        //    Invalidate();
        //}

        //new void Invalidate()
        //{
        //    gvDefault.CloseEditor();
        //    gvDefault.BeginUpdate();
        //    gvDefault.EndUpdate();
        //}
    }
}
