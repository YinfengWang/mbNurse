using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Handler;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Nodes.Operations;

namespace HISPlus.UserControls
{
    public partial class UcTreeList : UserControl
    {
        public UcTreeList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 在选定树节点后发生
        /// </summary>
        public event NodeEventHandler AfterSelect;

        public event NodeEventHandler AfterCheckNode;

        public event NodeEventHandler AfterDragNode;

        public new event DragEventHandler DragDrop;

        private string _enabledColumnName = "Enabled";

        /// <summary>
        /// 是否显示根节点,即使根节点为空.
        /// </summary>
        [DefaultValue(true)]
        public bool ShowRoot
        {
            set { treeList1.OptionsView.ShowRoot = value; }
        }

        /// <summary>
        /// 是否可用列的列名
        /// </summary>
        public string EnabledColumnName
        {
            get { return _enabledColumnName; }
            set { _enabledColumnName = value; }
        }

        [Browsable(false)]
        public object DataSource
        {
            get { return treeList1.DataSource; }
            set
            {
                treeList1.DataSource = value;

                treeList1.OptionsSelection.MultiSelect = MultiSelect;
                //treeList1.OptionsView.ShowCheckBoxes = MultiSelect;

                if (value != null && !string.IsNullOrEmpty(ImageColumnName))
                    this.SetNodeImage(ImageColumnName);
            }
        }

        [Browsable(false)]
        public TreeListNodes Nodes
        {
            get { return treeList1.Nodes; }
        }

        [Browsable(false)]
        public List<TreeListNode> CheckedNodes
        {
            get { return treeList1.GetAllCheckedNodes(); }
        }

        /// <summary>
        /// 选中行
        /// </summary>
        [Browsable(false)]
        public DataRowView SelectedRow
        {
            get
            {
                TreeListNode node = treeList1.FocusedNode;
                if (node == null)
                    return null;
                return treeList1.GetDataRecordByNode(node) as DataRowView;
            }
        }

        /// <summary>
        /// 选中行
        /// </summary>
        [Browsable(false)]
        public object SelectRowData
        {
            get
            {
                TreeListNode node = treeList1.FocusedNode;
                if (node == null)
                    return null;
                return treeList1.GetDataRecordByNode(node);
            }
        }

        /// <summary>
        /// 选中行集合
        /// </summary>
        [Browsable(false)]
        public TreeListMultiSelection Selection
        {
            get
            {
                return treeList1.Selection;
            }
        }

        [Browsable(false)]
        public TreeListNode SelectedNode
        {
            get { return treeList1.FocusedNode; }
            set { treeList1.FocusedNode = value; }
        }


        [Browsable(false)]
        public TreeListNode FocusedNode
        {
            get { return treeList1.FocusedNode; }
            set { treeList1.FocusedNode = value; }
        }

        /// <summary>
        /// 当前选中节点主键Id
        /// </summary>
        [Browsable(false)]
        public object SelectId
        {
            get { return treeList1.FocusedNode.GetValue(KeyFieldName); }
        }

        /// <summary>
        /// 当前选中节点主键Id(string类型)
        /// </summary>
        [Browsable(false)]
        public string SelectIds
        {
            get { return treeList1.FocusedNode.GetValue(KeyFieldName).ToString(); }
        }

        [Browsable(false)]
        public string ParentFieldName
        {
            get { return treeList1.ParentFieldName; }
            set { treeList1.ParentFieldName = value; }
        }

        [Browsable(false)]
        public string KeyFieldName
        {
            get { return treeList1.KeyFieldName; }
            set { treeList1.KeyFieldName = value; }
        }

        [DefaultValue(true)]
        public bool ShowHeader
        {
            get { return treeList1.OptionsView.ShowColumns; }
            set { treeList1.OptionsView.ShowColumns = value; }
        }

        /// <summary>
        /// 根节点值
        /// </summary>
        [Browsable(false)]
        public object RootValue
        {
            get { return treeList1.RootValue; }
            set { treeList1.RootValue = value; }
        }

        [Browsable(false)]
        public ImageList ImageList
        {
            get { return treeList1.SelectImageList as ImageList; }
            set
            {
                treeList1.SelectImageList = value;
            }
        }

        /// <summary>
        /// 是否显示复选框
        /// </summary>
        public bool ShowCheckBoxes
        {
            get
            {
                return treeList1.OptionsView.ShowCheckBoxes;
            }
            set
            {
                treeList1.OptionsView.ShowCheckBoxes = value;
            }
        }

        /// <summary>
        /// 图片地址列。设置该列名,数据在初始化时,加载图片。
        /// </summary>
        public string ImageColumnName { get; set; }

        /// <summary>
        /// 根据图片地址列的配置,加载图片
        /// </summary>
        private ImageList _imageLists;

        /// <summary>
        /// 是否开启拖拽
        /// </summary>
        public bool AllowDrag
        {
            get { return treeList1.OptionsBehavior.DragNodes; }
            set
            {
                treeList1.OptionsBehavior.DragNodes = value;
            }
        }

        private bool _dragFlag = false;

        /// <summary>
        /// 拖拽标识(节点拖拽后,添加拖拽标识,防止重复操作)
        /// </summary>
        public bool DragFlag
        {
            get { return _dragFlag; }
            set { _dragFlag = value; }
        }

        /// <summary>
        /// 是否接受拖放
        /// </summary>
        public override bool AllowDrop
        {
            get { return treeList1.AllowDrop; }
            set { treeList1.AllowDrop = value; }
        }

        /// <summary>
        /// 节点级数限制(用于拖拽操作)
        /// </summary>
        public int LevelLimit { get; set; }

        /// <summary>
        /// 是否可多选
        /// </summary>
        public bool MultiSelect { get; set; }

        private void UcTreeList_Load(object sender, EventArgs e)
        {
            //            //在ButtonEdit的ButtonClick事件中执行这个Operation：
            //private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
            //{
            //    var operation = new FilterNodeOperation(buttonEdit1.EditValue != null ? buttonEdit1.EditValue.ToString() : "");
            //    treeListMaster.NodesIterator.DoOperation(operation);
            //}

            //// 行指示器【隐藏/显示】
            treeList1.OptionsView.ShowIndicator = false;
            //// 被选中的单元格的聚集框 【隐藏/显示】
            treeList1.OptionsView.ShowFocusedFrame = true;
            //// 双击展开节点
            treeList1.OptionsBehavior.AllowExpandOnDblClick = true;
            
            // 是否可编辑
            treeList1.OptionsBehavior.Editable = false;

            //treeList1.OptionsBehavior.MoveOnEdit = false;
            //treeList1.OptionsBehavior.ShowEditorOnMouseUp = false;

            treeList1.OptionsView.ShowHorzLines = true;
            treeList1.OptionsView.ShowVertLines = true;

            // 行标题居中
            treeList1.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;

            treeList1.OptionsView.ShowButtons = true;
            treeList1.TreeLineStyle = LineStyle.Percent50;

            treeList1.OptionsSelection.EnableAppearanceFocusedRow = true;
            treeList1.OptionsSelection.EnableAppearanceFocusedCell = false;

            treeList1.OptionsView.ShowIndentAsRowStyle = true;

            treeList1.OptionsFilter.AllowFilterEditor = false;
            treeList1.OptionsFind.AllowFindPanel = false;            

            treeList1.Appearance.SelectedRow.BackColor = Color.FromArgb(30, 0, 0, 240);

            treeList1.Appearance.HideSelectionRow.BackColor = Color.FromArgb(60, 0, 0, 240);
            treeList1.Appearance.FocusedRow.BackColor = Color.FromArgb(60, 0, 0, 240);
            treeList1.Appearance.FocusedCell.BackColor = Color.FromArgb(60, 0, 0, 240);

            treeList1.OptionsView.ShowFocusedFrame = false;

            //treeList1.DragExpandDelay
            //treeList1.Nodes[0].

            //imagelist = new ImageList();
            //imagelist.Images.Add(Image.FromFile(@"E:\图片素材\2.jpg"));
            //treeList1.SelectImageList = imagelist;
            //treeList1.Nodes[0].ImageIndex = 0;     

            treeList1.MoveFirst();
        }

        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="caption">标题</param>
        /// <param name="visible">是否可见</param>
        public void Add(string caption, string fieldName, bool visible = true)
        {
            if (treeList1.Columns.ColumnByFieldName(fieldName) != null)
                return;
            TreeListColumn col = treeList1.Columns.AddField(fieldName);
            col.Caption = caption;
            col.Visible = visible;
            col.VisibleIndex = treeList1.Columns.Count;
            col.OptionsColumn.AllowSort = false;
            treeList1.Columns[fieldName].Visible = visible;
        }

        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="caption">标题</param>
        /// <param name="columnEdit"></param>
        public void Add(string caption, string fieldName, RepositoryItem columnEdit)
        {
            if (treeList1.Columns.ColumnByFieldName(fieldName) != null)
                return;
            TreeListColumn col = treeList1.Columns.AddField(fieldName);
            col.Caption = caption;
            col.Visible = true;
            col.VisibleIndex = treeList1.Columns.Count;
            col.OptionsColumn.AllowSort = false;
            if (columnEdit is RepositoryItemCheckEdit)
            {
                (columnEdit as RepositoryItemCheckEdit).NullStyle = StyleIndeterminate.Unchecked;
            }
            col.ColumnEdit = columnEdit;

            treeList1.Columns[fieldName].Visible = true;
        }

        /// <summary>
        /// 将列添加到集合中
        /// </summary>
        /// <param name="caption">列标题单元格的标题文本。</param>
        /// <param name="fieldName">绑定的数据库列的名称。</param>
        /// <param name="visible">是否可见</param>
        /// <param name="item">自定义数据关联项</param>
        public void Add(string caption, string fieldName, bool visible, bool isCheckBox)
        {
            if (treeList1.Columns.ColumnByFieldName(fieldName) != null)
                return;
            TreeListColumn col = treeList1.Columns.AddField(fieldName);
            col.Caption = caption;
            col.Visible = visible;
            col.VisibleIndex = treeList1.Columns.Count;
            col.OptionsColumn.AllowSort = false;
            if (isCheckBox)
            {
                RepositoryItemImageComboBox c = new RepositoryItemImageComboBox();
                c.Items.Add(new ImageComboBoxItem(@"可用", (byte)1, 1));
                c.Items.Add(new ImageComboBoxItem(@"不可用", (byte)0, 0));

                ImageList imageList = new ImageList();
                imageList.Images.Add(Properties.Resources.cancel_16x16);
                imageList.Images.Add(Properties.Resources.apply_16x16);

                c.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                c.GlyphAlignment = HorzAlignment.Center;

                c.SmallImages = imageList;
                c.LargeImages = imageList;
                col.ColumnEdit = c;

                col.OptionsColumn.AllowFocus = false;
            }

            treeList1.Columns[fieldName].Visible = visible;
        }

        /// <summary>
        /// 追加节点
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="parentNodeId"></param>
        public TreeListNode AppendNode(object[] objects, int parentNodeId)
        {
            return treeList1.AppendNode(objects, parentNodeId);
        }

        /// <summary>
        /// 追加节点
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="parentNode"></param>
        public TreeListNode AppendNode(object[] objects, TreeListNode parentNode)
        {
            return treeList1.AppendNode(objects, parentNode);
        }

        private void treeList1_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            //if (e.Node != treeList1.FocusedNode) return;

            object enabled = e.Node.GetValue(_enabledColumnName);
            if (enabled == null || enabled == DBNull.Value) return;
            if (1 != (byte)enabled)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Strikeout);
                e.Appearance.ForeColor =
                    CommonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                        .Colors.GetColor("DisabledText");
            }
            return;
            //DataRow dr = treeList1.GetDataRecordByNode(e.Node) as DataRow;
            //if (dr == null)
            //{
            //    object obj = treeList1.GetDataRecordByNode(e.Node);
            //    Type t = obj.GetType();
            //    foreach (PropertyInfo pi in t.GetProperties())
            //    {
            //        if (ColumnCheckBoxNames.Contains(pi.Name))
            //        {
            //            //用pi.GetValue获得值
            //            object value1 = pi.GetValue(obj, null);
            //            if (value1 == null) return;

            //            if (value1.ToString() != "1")
            //            {
            //                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Strikeout);
            //                e.Appearance.ForeColor =
            //                    CommonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default)
            //                        .Colors.GetColor("DisabledText");
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    foreach (string name in ColumnCheckBoxNames)
            //    {
            //        if (dr[name] == null) return;
            //        if (dr[name].ToString() != "1")
            //        {
            //            e.Appearance.Font = new Font(AppearanceObject.DefaultFont, FontStyle.Strikeout);
            //            e.Appearance.ForeColor =
            //                CommonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default).Colors.GetColor("DisabledText");
            //        }
            //    }
            //}

            return;
            if (e.Column.ColumnEdit != null) return;

            e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);

            Rectangle r = new Rectangle(e.EditViewInfo.ContentRect.Left,
                e.EditViewInfo.ContentRect.Top,
                //Convert.ToInt32(e.Graphics.MeasureString(e.CellText, treeList1.Font).Width + 1),
                //treeList1.Width,
                e.EditViewInfo.ContentRect.Width,
                e.EditViewInfo.ContentRect.Height
                //Convert.ToInt32(e.Graphics.MeasureString(e.CellText, treeList1.Font).Height)
            );

            e.Graphics.FillRectangle(SystemBrushes.Highlight, r);
            e.Graphics.DrawString(e.CellText, treeList1.Font, SystemBrushes.HighlightText, r);

            e.Handled = true;
        }

        private void treeList1_AfterFocusNode(object sender, NodeEventArgs e)
        {
            if (AfterSelect != null)
                AfterSelect(sender, e);
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="node"></param>
        public void Delete(TreeListNode node)
        {
            treeList1.DeleteNode(node);
        }

        public void DeleteSelectedNodes()
        {
            treeList1.DeleteSelectedNodes();
        }

        public void BeginUnboundLoad()
        {
            treeList1.BeginUnboundLoad();
        }
        public void EndUnboundLoad()
        {
            treeList1.EndUnboundLoad();
        }

        private void treeList1_AfterCheckNode(object sender, NodeEventArgs e)
        {
            if (AfterCheckNode != null)
                AfterCheckNode(sender, e);

            CheckedChildNodes(e.Node, e.Node.CheckState);
            CheckedParentNodes(e.Node, e.Node.CheckState);
        }

        public void CheckedChildNodes(TreeListNode node, CheckState check)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].CheckState = check;
                CheckedChildNodes(node.Nodes[i], check);
            }
        }
        public void CheckedParentNodes(TreeListNode node, CheckState check)
        {
            if (node.ParentNode != null)
            {
                bool b = false;
                CheckState state;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    state = (CheckState)node.ParentNode.Nodes[i].CheckState;
                    if (!check.Equals(state))
                    {
                        b = !b;
                        break;
                    }
                }
                node.ParentNode.CheckState = b ? CheckState.Indeterminate : check;
                CheckedParentNodes(node.ParentNode, check);
            }
        }

        private void treeList1_BeforeCheckNode(object sender, CheckNodeEventArgs e)
        {
            e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
        }

        /// <summary>
        /// 根据节点返回数据记录
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public object GetDataRecordByNode(TreeListNode node)
        {
            return treeList1.GetDataRecordByNode(node);
        }

        private void GetCheckedID(TreeListNode parentNode)
        {
            if (parentNode.Nodes.Count == 0)
            {
                return; //递归终止
            }

            foreach (TreeListNode node in parentNode.Nodes)
            {
                if (node.CheckState == CheckState.Checked)
                {
                    DataRowView drv = treeList1.GetDataRecordByNode(node) as DataRowView; //关键代码
                    if (drv != null)
                    {
                        int GroupID = (int)drv["GroupID"];
                        // lstCheckedOfficeID.Add(GroupID);
                    }
                }
            }
        }

        /// <summary>
        /// 清空节点
        /// </summary>
        public void ClearNodes()
        {
            treeList1.ClearNodes();
        }


        /// <summary>
        /// 取消所有选中项
        /// </summary>
        public void UncheckAll()
        {
            treeList1.UncheckAll();
        }

        /// <summary>
        /// 根据主键ID查询节点
        /// </summary>
        /// <param name="keyId"></param>
        /// <returns></returns>
        public TreeListNode FindNodeByKeyId(object keyId)
        {
            return treeList1.FindNodeByKeyID(keyId);
        }

        /// <summary>
        /// 设置节点图片
        /// </summary>
        /// <param name="imageColumn"></param>
        private void SetNodeImage(string imageColumn)
        {
            if (string.IsNullOrEmpty(imageColumn)) return;

            if (_imageLists == null)
                _imageLists = new ImageList();

            if (_imageLists.Images.Count != treeList1.AllNodesCount && _imageLists.Images.Count > 0)
                _imageLists.Images.Clear();

            SetChildNodeImage(imageColumn, treeList1.Nodes);

            treeList1.SelectImageList = _imageLists;
        }

        /// <summary>
        /// 递归设置节点图片
        /// </summary>
        /// <param name="imageColumn"></param>
        /// <param name="parentNode"></param>
        private void SetChildNodeImage(string imageColumn, TreeListNodes parentNode)
        {
            foreach (TreeListNode node in parentNode)
            {
                //if (node.GetValue("Name").ToString() == "护理评估")
                //{

                //}
                //node.SelectImageIndex =
                node.SelectImageIndex = -1;
                node.ImageIndex = -1;

                string path = node.GetValue(imageColumn) as string;
                if (string.IsNullOrEmpty(path)) continue;

                Image image = GetImageFile(path);
                if (image != null)
                {
                    _imageLists.Images.Add(image);
                    node.SelectImageIndex = _imageLists.Images.Count - 1;
                    node.ImageIndex = _imageLists.Images.Count - 1;
                }

                SetChildNodeImage(imageColumn, node.Nodes);
            }
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private Image GetImageFile(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;
            int pos = path.LastIndexOf(Path.DirectorySeparatorChar);
            if (pos > 0)
            {
                path = path.Substring(pos + 1, (path.Length - pos) - 1);
            }
            path = Path.Combine(Path.Combine(Path.Combine(Application.StartupPath, "Resource"), "ICON"), path);
            if (File.Exists(path))
                return Image.FromFile(path);
            return null;
        }

        /// <summary>
        /// 刷新数据源
        /// </summary>
        public void RefreshData()
        {
            treeList1.RefreshDataSource();

            if (!string.IsNullOrEmpty(ImageColumnName))
                this.SetNodeImage(ImageColumnName);
        }

        public void RefreshNode()
        {
            treeList1.RefreshNode(treeList1.FocusedNode);
        }

        /// <summary>
        /// 拖拽后鼠标松开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                TreeListNode node = GetDragNode(e.Data);
                if (node == null) return;

                TreeList list = (TreeList)sender;


                if (DragDrop != null)
                    DragDrop(sender, e);
                else if (list != node.TreeList)
                {
                    TreeListHitInfo info = list.CalcHitInfo(list.PointToClient(new Point(e.X, e.Y)));
                    InsertBrush(list, node, info.Node == null ? -1 : info.Node.Id);
                }

                if (LevelLimit == 0) return;

                TreeListNode dragNode, targetNode;
                TreeList tl = sender as TreeList;
                if (tl != null)
                {
                    dragNode = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;

                    if (dragNode == null)
                        return;

                    Point pt = tl.PointToClient(new Point(e.X, e.Y));
                    targetNode = tl.CalcHitInfo(pt).Node;
                    if (targetNode == null)
                        return;
                }
                else
                    return;
                e.Effect = DragDropEffects.Move;

                // 通过反射获取拖拽方向: 前/后/某节点的子节点
                PropertyInfo pi = typeof(TreeList).GetProperty("Handler", BindingFlags.Instance | BindingFlags.NonPublic);
                TreeListHandler handler = (TreeListHandler)pi.GetValue(tl, null);
                FieldInfo fi2 = typeof(TreeListHandler).GetField("fStateData", BindingFlags.Instance | BindingFlags.NonPublic);
                StateData stateData = (StateData)fi2.GetValue(handler);
                FieldInfo fi = typeof(DragScrollInfo).GetField("dragInsertDirection", BindingFlags.Instance | BindingFlags.NonPublic);
                DragInsertDirection direction = (DragInsertDirection)fi.GetValue(stateData.DragInfo);

                switch (direction)
                {
                    case DragInsertDirection.None:
                        if (targetNode.Level >= LevelLimit - 1)
                        {
                            e.Effect = DragDropEffects.None;
                            XtraMessageBox.Show("仅能设置为二级节点。");
                        }
                        break;
                    case DragInsertDirection.Before: /*...*/ break;
                    case DragInsertDirection.After: /*...*/ break;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void InsertBrush(TreeList list, TreeListNode node, int parent)
        {
            ArrayList data = new ArrayList();
            //data.Add(node.GetValue(KeyFieldName));
            foreach (TreeListColumn column in node.TreeList.Columns)
            {
                data.Add(node[column]);
            }
            parent = list.AppendNode(data.ToArray(), parent).Id;

            if (node.TreeList.Parent is UcTreeList)
                if ((node.TreeList.Parent as UcTreeList).DragFlag)
                    node.SetValue(_enabledColumnName, 0);

            if (node.HasChildren)
                foreach (TreeListNode n in node.Nodes)
                    InsertBrush(list, n, parent);
        }

        /// <summary>
        /// 获取拖拽对象
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private TreeListNode GetDragNode(IDataObject data)
        {
            return (TreeListNode)data.GetData(typeof(TreeListNode));
        }

        private void treeList1_DragEnter(object sender, DragEventArgs e)
        {
            TreeList list = (TreeList)sender;
            TreeListNode node = GetDragNode(e.Data);
            if (node == null) return;

            if (node.TreeList != list)
            {
                e.Effect = DragDropEffects.Move;
            }
            //return;

            //if (ColumnCheckBoxNames == null) return;
            object enabled = node.GetValue("Enabled");
            if (enabled == null) return;
            if (1 != (byte)enabled)
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// 首项选中
        /// </summary>
        public void SelectFirst()
        {
            treeList1.MoveFirst();
        }

        /// <summary>
        /// 根据字段及字段值选中行
        /// </summary>
        public void SelectRow(object id)
        {
            FindNode(id, treeList1.Nodes);
        }

        /// <summary>
        /// 返回节点行集或实体
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public object GetRow(TreeListNode node)
        {
            return treeList1.GetDataRecordByNode(node);
            //treeList1
        }

        private void FindNode(object id, TreeListNodes nodes)
        {
            foreach (TreeListNode node in nodes)
            {
                if (node.GetValue(KeyFieldName) == id)
                {
                    node.Selected = true;
                    return;
                }
                if (node.HasChildren)
                    FindNode(id, node.Nodes);
            }
        }

        /// <summary>
        /// 展开节点
        /// </summary>
        public void ExpandAll()
        {
            treeList1.ExpandAll();
        }

        private void treeList1_AfterDragNode(object sender, NodeEventArgs e)
        {
            if (AfterDragNode != null)
                AfterDragNode(sender, e);
        }
    }

    class FilterNodeOperation : TreeListOperation
    {
        string pattern;


        public FilterNodeOperation(string _pattern)
        {
            pattern = _pattern;
        }


        public override void Execute(TreeListNode node)
        {
            if (NodeContainsPattern(node, pattern))
            {
                node.Visible = true;

                //if (node.ParentNode != null)
                //    node.ParentNode.Visible = true;

                //必须要递归查找其父节点全部设置为可见
                var pNode = node.ParentNode;
                while (pNode != null)
                {
                    pNode.Visible = true;
                    pNode = pNode.ParentNode;
                }
            }
            else
                node.Visible = false;
        }


        bool NodeContainsPattern(TreeListNode node, string pattern)
        {
            foreach (TreeListColumn col in node.TreeList.VisibleColumns)
            {
                if (node.GetDisplayText(col).Contains(pattern))
                    return true;
            }
            return false;
        }
    }

}
