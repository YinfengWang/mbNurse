using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace HISPlus.UserControls
{
    public partial class UCGridView_CellMerge : UserControl
    {
        #region 私有变量

        /// <summary>
        /// 初次加载标志位。当首次加载后，置为 true
        /// </summary>
        private bool _firstLoad = true;

        #endregion 私有变量

        #region 公共变量/属性

        /// <summary>
        /// 是否启用奇偶行交替颜色
        /// </summary>
        /// <value>如果启用，为 true；否则为 false。默认值为 false。</value>      
        [Category("自定义属性"), Description("是否启用奇偶行交替颜色"), DefaultValue(false)]
        private bool EnableOddEvenRow { get; set; }

        private bool _enableRowSelect = true;

        /// <summary>
        /// 获取或设置一个值，该值指示是否<br>按行</br>选择 UCGridView 的单元格。
        /// </summary>                                
        /// <value>如果按行选择 UCGridView，为 true；否则为 false。默认值为 true。</value>          
        [Category("自定义属性")]
        [Description("获取或设置一个值，该值指示是否按行选择 UCGridView 的单元格。")]
        [DefaultValue(true)]
        public bool EnableRowSelect
        {
            get { return _enableRowSelect; }
            set { _enableRowSelect = value; }
        }

        //private bool _AutoGenerateColumns = false;
        ///// <summary>
        /////  获取或设置一个值，该值指示在设置 UCGridView.DataSource 或 UCGridView.DataMember属性时是否自动创建列。
        ///// </summary>
        ///// <value>如果应自动创建列，为 true；否则为 false。默认值为 true。</value>
        //[Category("自定义属性")]
        //[Description("获取或设置一个值，该值指示在设置 UCGridView.DataSource 或 UCGridView.DataMember属性时是否自动创建列。")]
        //public bool AutoGenerateColumns
        //{
        //    get { return _AutoGenerateColumns; }
        //    set { _AutoGenerateColumns = value; }
        //}

        /// <summary>
        /// 获取或设置一个值，该值指示是否向用户显示添加行的选项。
        /// </summary>
        /// <value>如果向用户显示“添加行”选项，为 true；否则为 false。默认值为 true。</value>
        [Category("自定义属性"), Description("获取或设置一个值，该值指示是否向用户显示添加行的选项。"),]
        private bool AllowAddRows { get; set; }

        private bool _allowDeleteRows = false;

        /// <summary>
        /// 获取或设置一个值，该值指示是否允许用户从 UCGridView 中删除行。
        /// </summary>
        /// <value>如果用户可以删除行，为 true；否则为 false。默认值为 true。</value>
        [Category("自定义属性")]
        [Description("获取或设置一个值，该值指示是否允许用户从 UCGridView 中删除行。")]
        [Browsable(true)]
        public bool AllowDeleteRows
        {
            get { return _allowDeleteRows; }
            set { _allowDeleteRows = value; }
        }

        private bool _allowEdit = false;

        /// <summary>
        /// 获取或设置一个值，该值指示是否允许用户对 UCGridView 进行编辑操作。
        /// </summary>
        [Category("自定义属性")]
        [Description("获取或设置一个值，该值指示是否允许用户对 UCGridView 进行编辑操作。")]
        [Browsable(true)]
        public bool AllowEdit
        {
            get { return _allowEdit; }
            set { _allowEdit = value; }
        }

        /// <summary>
        /// 获取或设置一个值，该值指示是否允许通过手动对列重新定位。
        /// </summary>
        /// <value>如果用户可以移动列，为 true；否则为 false。默认值为 false。</value>
        [Category("自定义属性"), Description("获取或设置一个值，该值指示是否允许通过手动对列重新定位。"), Browsable(true)]
        public bool AllowMoving { private get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否允许通过点击列标题对列进行排序。
        /// </summary>
        /// <value>如果用户可以对列进行排序，为 true；否则为 false。默认值为 false。</value>
        [Category("自定义属性"), Description("获取或设置一个值，该值指示是否允许用户对 UCGridView 进行编辑操作。"), Browsable(true)]
        public bool AllowSort { get; set; }

        /// <summary>
        /// 是否合并列
        /// </summary>
        public bool AllowMergeColumn { get; set; }


        private bool _MultiSelect = false;
        /// <summary>
        /// 获取或设置一个值，该值指示是否允许用户一次选择 UCGridView 的多个单元格、行或列。
        /// </summary>
        /// <value>如果允许多选，为 true；否则为 false。默认值为 false。</value>
        [Category("自定义属性")]
        [Description("获取或设置一个值，该值指示是否允许用户一次选择 UCGridView 的多个单元格、行或列。")]
        [Browsable(true)]
        public bool MultiSelect
        {
            get { return _MultiSelect; }
            set { _MultiSelect = value; }
        }

        /// <summary>
        /// 是否启用奇偶行交替颜色
        /// </summary>
        /// <value>如果启用，为 true；否则为 false。默认值为 false。</value>      
        [Category("自定义属性"), Description("是否显示检索框"), DefaultValue(false)]
        public bool ShowFindPanel { get; set; }

        private object _dataSource;
        //当配置数据源后，控件自动初始化
        /// <summary>
        /// 数据源。
        /// </summary>             
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public object DataSource
        {
            get { return _dataSource; }
            set
            {
                _dataSource = value;

                gcDefault.DataSource = this.DataSource;
                //gvDefault.RefreshData();

                if (!this.DesignMode && value != null)
                    Init();
            }
        }

        private bool _ShowHeaders = true;
        /// <summary>
        /// 获取或设置一个值，该值指示是否显示标题栏。
        /// </summary>        
        [Category("自定义属性")]
        [Description("获取或设置一个值，该值指示是否显示标题栏。")]
        [DefaultValue(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public bool ShowHeaders
        {
            get { return _ShowHeaders; }
            set { _ShowHeaders = value; }
        }

        /// <summary>
        /// 选中总行数
        /// </summary>
        [Description("选中总行数")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public int SelectRowsCount
        {
            get
            {
                return gvDefault.SelectedRowsCount;
            }
        }

        /// <summary>
        /// 显示全选按钮（在首列为复选框时）
        /// </summary>
        private bool _showAllChecked;

        /// <summary>
        /// 复选框列的列名(包含全选按钮)
        /// </summary>
        private string _allCheckedName;

        /// <summary>
        /// 是否自动生成列.默认为自动生成,如果手动添加列时,会自动
        /// </summary>
        private bool _isAutoGenerateColumns = true;

        /// <summary>
        /// 是否显示行指示器
        /// </summary>
        public bool ShowRowIndicator { get; set; }

        #endregion 公共变量/属性

        #region 委托/事件

        /// <summary>
        /// 在 Grid 上，当前所选内容更改时发生
        /// </summary>
        public event EventHandler SelectionChanged;

        /// <summary>
        /// 在 Grid 上释放键时发生
        /// </summary>
        public event KeyEventHandler KeyUpOnGrid;

        /// <summary>
        /// 绑定数据源时发生，初始化自定义列数据
        /// </summary>
        public event CustomColumnDataEventHandler CustomColumnData;

        /// <summary>
        /// 按钮按下事件
        /// </summary>
        public new event MouseEventHandler MouseDown;

        /// <summary>
        /// 复选框列的列名集合，以逗号分隔。
        /// 为了使不可用的列以删除线和灰色标识。
        /// </summary>
        private List<string> ColumnCheckBoxNames;

        /// <summary>
        /// 以卡片形式作数据展示
        /// </summary>
        public bool ShowCard;

        #endregion

        public UCGridView_CellMerge()
        {
            AllowAddRows = false;
            AllowMoving = false;
            AllowSort = false;
            ShowFindPanel = false;
            InitializeComponent();
        }

        #region 事件

        private void UCGridView_Load(object sender, EventArgs e)
        {
            //gvDefault.Columns.Clear();            
            //if (!this.DesignMode)
            //    Init();


        }

        #endregion

        #region 方法

        public void ClearColumn()
        {
            gvDefault.Columns.Clear();
        }

        /// <summary>
        /// 初始化控件属性。
        /// 窗体初始化顺序：        
        ///     UserControl.构造方法 -->        
        ///     引用窗体.构造方法 -->
        ///     UserControl.Load -->
        ///     引用窗体.Load 。      
        /// </summary>
        public void Init()
        {
            //gcDefault.DataSource = this.DataSource;

            if (gvDefault.Columns.Count == 0)
            {
                gvDefault.PopulateColumns();
            }

            if (_firstLoad)
            {
                _firstLoad = false;
                InitEvent();
            }
            else return;

            if (EnableOddEvenRow)
            {
                //设置奇偶行不同颜色
                //gvDefault.Appearance.OddRow.BackColor = Color.BurlyWood;  // 设置奇数行颜色 // 默认是白色
                //gvDefault.OptionsView.EnableAppearanceOddRow = true;   // 使能 // 和和上面绑定 同时使用有效 
                //gvDefault.Appearance.EvenRow.BackColor = Color.CadetBlue; // 偶数行背景色
                gvDefault.Appearance.EvenRow.BackColor = Color.AliceBlue;

                gvDefault.OptionsView.EnableAppearanceEvenRow = true;
            }

            //如果主从表中，没有找到从表内容也要显示(默认是不显示的)
            gvDefault.OptionsDetail.AllowZoomDetail = false;
            gvDefault.OptionsDetail.EnableMasterViewMode = false;
            gvDefault.OptionsDetail.SmartDetailExpand = false;
            gvDefault.OptionsDetail.ShowDetailTabs = false;
            gvDefault.OptionsDetail.AllowExpandEmptyDetails = false;

            // 是否可编辑
            //gvDefault.OptionsBehavior.Editable = _allowEdit;
            //this.gvDefault.OptionsBehavior.ReadOnly = !_allowEdit;
            gvDefault.OptionsBehavior.Editable = true;
            this.gvDefault.OptionsBehavior.ReadOnly = false;


            if (_allowEdit)
            {
                gvDefault.OptionsBehavior.EditorShowMode = EditorShowMode.MouseUp;
                gvDefault.OptionsBehavior.EditingMode = GridEditingMode.Inplace;
            }
            // 标题行过滤
            gvDefault.OptionsCustomization.AllowFilter = AllowSort;
            //// 是否允许列排序
            gvDefault.OptionsCustomization.AllowSort = AllowSort;
            //列移动【允许】
            gvDefault.OptionsCustomization.AllowColumnMoving = true;
            //改变列宽【允许】
            gvDefault.OptionsCustomization.AllowColumnResizing = true;

            // 行标题居中
            gvDefault.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            // 内容居中
            //gvDefault.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //gvDefault.OptionsView.AllowCellMerge = true;

            // 是否显示列标题右键菜单
            gvDefault.OptionsMenu.EnableColumnMenu = false;
            // 被选中后鼠标点击的单元格颜色不变
            gvDefault.OptionsSelection.EnableAppearanceFocusedCell = false;

            //gvDefault.OptionsFilter.AllowFilterEditor = true;

            gvDefault.OptionsFind.AllowFindPanel = ShowFindPanel;
            gvDefault.OptionsFind.AlwaysVisible = ShowFindPanel;
            gvDefault.OptionsFind.ClearFindOnClose = false;
            gvDefault.OptionsFind.ShowFindButton = false;
            gvDefault.OptionsFind.ShowClearButton = false;
            gvDefault.OptionsFind.SearchInPreview = false;

            //取消点击后的虚线框
            gvDefault.FocusRectStyle = DrawFocusRectStyle.RowFocus;

            //设置列是否可排序
            //gvDefault.Columns["ID"].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            //gvDefault.OptionsBehavior.ReadOnly = true;//设置控件单元格只读（不能被修改）


            //控件绑定数据源的时候，如果是对象只需要绑定DataSouse即可；
            //如果是DataTable，后面需要添加函数gvDefault.PopulateColumns()

            //去掉表格上方“Drag a column header ……”
            gvDefault.OptionsView.ShowGroupPanel = false;

            //是否显示最左边一列空白列
            gvDefault.OptionsView.ShowIndicator = ShowRowIndicator;

            // 显示行号
            if (ShowRowIndicator)
            {
                // 宽度
                this.gvDefault.IndicatorWidth = 30;
                if (gvDefault.DataRowCount > 9)
                    this.gvDefault.IndicatorWidth = 50;
                else if (gvDefault.DataRowCount > 99)
                    this.gvDefault.IndicatorWidth = 80;

                this.gvDefault.CustomDrawRowIndicator += gvDefault_CustomDrawRowIndicator;
            }

            //列标题栏【显示】
            gvDefault.OptionsView.ShowColumnHeaders = this.ShowHeaders;
            //设置自动列宽（这样的话表格下方可能会出现滚动条或者未铺满）
            gvDefault.OptionsView.ColumnAutoWidth = true;

            //gvDefault.OptionsView.ShowAutoFilterRow = true;

            //是否自动合并单元格【慎用】
            //gvDefault.OptionsView.AllowCellMerge = true;

            //gvDefault.OptionsSelection.EnableAppearanceFocusedCell = false; //设置单元格不能选择（如果不设置，则点击到的单元格在整行选择情况下的背景色不变）

            //gvDefault.OptionsSelection.EnableAppearanceFocusedRow = true; //禁止选择行

            gvDefault.OptionsSelection.MultiSelect = this.MultiSelect; //设置可多选
            //gvDefault.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;//多选行还是多选单元格（一般选RowSelect）

            //SelectedRow & FocusedRow(以下两项要同时设置)：设置选中行背景色

            //BackColor :MediumSlateBlue //背景色
            //ForeColor : White //前景色（字体颜色）

            //HeaderPanel：设置标题行颜色            
            //仅设置以上两项无法改变标题行的颜色，还需要设置控件的LookAndFeel
            //选中GridControl，在属性中找到LookAndFeel并展开，
            //Style设为UltraFlat，UseDefualtLookAndFeel设为false。

            //OddRow EvenRow：设置奇数行、偶数行颜色
            //gvDefault.Appearance.OddRow.BackColor

            // RowHeight //行高
            //ColumnPanelRowHeight //标题行的行高
            //FocusRectStyle = None; //取消点击后的虚线框

            // 自动展开分组
            gvDefault.OptionsBehavior.AutoExpandAllGroups = true;
            //gvDefault.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.True;
            //gvDefault.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            //gvDefault.OptionsBehavior.AutoExpandAllGroups = true;

            //gvDefault.OptionsPrint.PrintHorzLines = false;
            //gvDefault.OptionsPrint.PrintVertLines = false;
            //gvDefault.OptionsView.ShowGroupPanel = false;
            gvDefault.OptionsView.ShowVerticalLines = DefaultBoolean.True;

            if (ShowCard)
            {
                this.gcDefault.ViewCollection.AddRange(new BaseView[] {
            this.gvDefault});



                this.gvDefault.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.True;
                this.gvDefault.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
                this.gvDefault.OptionsBehavior.AutoExpandAllGroups = true;
                this.gvDefault.OptionsBehavior.Editable = false;
                this.gvDefault.OptionsDetail.EnableMasterViewMode = false;
                this.gvDefault.OptionsFind.AlwaysVisible = true;
                this.gvDefault.OptionsPrint.PrintHorzLines = false;
                this.gvDefault.OptionsPrint.PrintVertLines = false;
                this.gvDefault.OptionsView.ShowGroupedColumns = true;
                this.gvDefault.OptionsView.ShowGroupPanel = false;
                this.gvDefault.OptionsView.ShowIndicator = false;
                this.gvDefault.OptionsView.ShowVerticalLines = DefaultBoolean.False;
            }

            if (gvDefault.SelectedRowsCount > 0)
                if (SelectionChanged != null)
                    SelectionChanged(null, null);           
        }

        /// <summary>
        /// 行序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gvDefault_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();

            }
        }

        /// <summary>
        /// 初始化事件
        /// </summary>
        private void InitEvent()
        {
            if (SelectionChanged != null)
                if (this.MultiSelect)
                    gvDefault.SelectionChanged += gvDefault_SelectionChanged;
                else // if (this.EnableRowSelect)
                    gvDefault.FocusedRowChanged += gvDefault_FocusedRowChanged;

            if (KeyUpOnGrid != null)
                gvDefault.KeyUp += gvDefault_KeyUp;

            if (CustomColumnData != null)
                gvDefault.CustomUnboundColumnData += gvDefault_CustomUnboundColumnData;

            
            //if (AllowMergeColumn)
            //    this.gvDefault.CellMerge += gvDefault_CellMerge;
            //this.gvDefault.CustomDrawColumnHeader += gvDefault_CustomDrawColumnHeader;

            //this.gvDefault.Click += gvDefault_Click;
        }

        void gvDefault_CellMerge(object sender, CellMergeEventArgs e)
        {
            //if (e.Column.FieldName == "Order Date")
            if (e.Column.OptionsColumn.AllowMerge != DefaultBoolean.True)
                return;

            //GridView view = sender as GridView;
            if (e.CellValue1 != null && e.CellValue2 != null)
            {
                DateTime val1;
                DateTime.TryParse(e.CellValue1.ToString(), out val1);

                DateTime val2;
                DateTime.TryParse(e.CellValue2.ToString(), out val2);

                if (e.Column.DisplayFormat.FormatString == ComConst.FMT_DATE.SHORT_CN)
                    e.Merge = val1.Date == val2.Date;
                //else if (e.Column.DisplayFormat.FormatString == ComConst.FMT_DATE.TIME_SHORT)
                else
                    e.Merge = val1.Date == val2.Date
                        && val1.TimeOfDay == val2.TimeOfDay;



                e.Handled = true;
            }
        }

        private void gvDefault_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (gvDefault.SelectedRowsCount > 0)
                SelectionChanged(sender, e);
        }

        private void gvDefault_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (gvDefault.SelectedRowsCount > 0)
                SelectionChanged(sender, e);
        }

        private void gvDefault_KeyUp(object sender, KeyEventArgs e)
        {
            KeyUpOnGrid(sender, e);
        }

        private void gvDefault_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            //GridView View = sender as GridView;

            //if (e.IsGetData)
            //{
            //    string itemId = (e.Row as DataRowView)["ITEM_ID_MATCH"].ToString();

            //    string filter = "ITEM_ID = " + SQL.SqlConvert(itemId);
            //    DataRow[] dr = dsRespondant.Tables[0].Select(filter);
            //    if (dr.Length > 0)
            //        e.Value = dr[0]["ITEM_NAME"];
            //}

            CustomColumnData(sender, e);
        }

        #endregion

        #region 公共方法

        #region 添加列

        /// <summary>
        /// 将列添加到集合中
        /// </summary>
        /// <param name="caption">列标题单元格的标题文本。</param>
        /// <param name="fieldName">绑定的数据库列的名称。</param>
        /// <param name="width">列宽度</param>
        /// <param name="visible">指示该列是否可见。默认为可见。</param>
        public void Add(string caption, string fieldName, int width, bool visible = true)
        {
            this.Add(caption, fieldName, visible, null, -1, width);
        }

        /// <summary>
        /// 将列添加到集合中
        /// </summary>
        /// <param name="caption">列标题单元格的标题文本。</param>
        /// <param name="fieldName">绑定的数据库列的名称。</param>
        /// <param name="stringFormat"></param>
        public void Add(string caption, string fieldName, string stringFormat)
        {
            this.Add(caption, fieldName, true, null, -1, 50, 30, stringFormat);
        }

        /// <summary>
        /// 将列添加到集合中
        /// </summary>
        /// <param name="caption">列标题单元格的标题文本。</param>
        /// <param name="fieldName">绑定的数据库列的名称。</param>
        /// <param name="visible">是否可见</param>
        /// <param name="groupIndex">分组索引,从0开始</param>
        /// <param name="stringFormat">自定义格式化</param>
        public void Add(string caption, string fieldName, bool visible = true, int groupIndex = -1, string stringFormat = "")
        {
            this.Add(caption, fieldName, visible, null, groupIndex, 50, 30, stringFormat);
        }

        ///// <summary>
        ///// 将列添加到集合中
        ///// </summary>
        ///// <param name="caption">列标题单元格的标题文本。</param>
        ///// <param name="fieldName">绑定的数据库列的名称。</param>
        ///// <param name="groupIndex">分组索引,从0开始</param>
        //public void Add(string caption, string fieldName, int groupIndex)
        //{
        //    this.Add(caption, fieldName, true, null, groupIndex);
        //}


        /// <summary>
        /// 将列添加到集合中
        /// </summary>
        /// <param name="caption">列标题单元格的标题文本。</param>
        /// <param name="fieldName">绑定的数据库列的名称。</param>        
        /// <param name="item">自定义数据关联项</param>
        public void Add(string caption, string fieldName, RepositoryItem item)
        {
            this.Add(caption, fieldName, true, item);
        }

        /// <summary>
        /// 将列添加到集合中
        /// </summary>
        /// <param name="caption">列标题单元格的标题文本。</param>
        /// <param name="fieldName">绑定的数据库列的名称。</param>
        /// <param name="visible">指示该列是否可见。默认为可见。</param>
        /// <param name="columnEdit"></param>
        /// <param name="groupIndex">分级索引,从0开始</param>
        /// <param name="width"></param>
        /// <param name="minWidth"></param>
        /// <param name="format">格式化字符串</param>
        private void Add(string caption, string fieldName, bool visible
            , RepositoryItem columnEdit, int groupIndex = -1, int width = 50, int minWidth = 30, string format = null)
        {
            if (_isAutoGenerateColumns)
            {
                this.ClearColumn();
                _isAutoGenerateColumns = false;
            }

            GridColumn col = new GridColumn
            {
                Caption = caption,
                FieldName = fieldName,
                MinWidth = minWidth,
                Width = width,
                Visible = visible,
                VisibleIndex = gvDefault.Columns.Count,
                ColumnEdit = columnEdit,
                //GroupIndex = groupIndex               
            };

            if (columnEdit is RepositoryItemCheckEdit)
            {
                (columnEdit as RepositoryItemCheckEdit).NullStyle = StyleIndeterminate.Unchecked;

                if (ColumnCheckBoxNames == null)
                    ColumnCheckBoxNames = new List<string>();
                ColumnCheckBoxNames.Add(fieldName);
            }

            col.OptionsColumn.AllowEdit = _allowEdit;
            col.OptionsColumn.ReadOnly = !_allowEdit;

            if (groupIndex > -1 && !string.IsNullOrEmpty(format))
            {
                col.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.DisplayText;
                
                col.GroupFormat.FormatType = FormatType.DateTime;
                col.GroupFormat.FormatString = format;
            }
            else if (!string.IsNullOrEmpty(format))
                col.DisplayFormat.FormatString = format;

            col.OptionsColumn.AllowMerge = groupIndex >= 0 ? DefaultBoolean.True : DefaultBoolean.False;
            if (col.OptionsColumn.AllowMerge == DefaultBoolean.True)
            {
                AllowMergeColumn = true;
                col.DisplayFormat.FormatString = format;
                col.DisplayFormat.FormatType = FormatType.DateTime;
            }

            if (string.IsNullOrEmpty(fieldName))
                col.UnboundType = DevExpress.Data.UnboundColumnType.String;

            gvDefault.Columns.Add(col);
            gvDefault.Columns[fieldName].Visible = visible;
        }

        /// <summary>
        /// 将列添加到集合中。（仅用于创建首列且包含有全选按钮时）
        /// </summary>
        /// <param name="caption">列标题单元格的标题文本。</param>
        /// <param name="fieldName">绑定的数据库列的名称。</param>
        /// <param name="showAllChecks"></param>
        public void AddCheckBoxColumn(string fieldName, string caption = "选择", bool showAllChecks = true)
        {
            if (_isAutoGenerateColumns)
            {
                this.ClearColumn();
                _isAutoGenerateColumns = false;
            }

            RepositoryItemCheckEdit check = new RepositoryItemCheckEdit { NullStyle = StyleIndeterminate.InactiveChecked };
            check.AllowFocused = true;
            //check.CheckStyle = CheckStyles.Style1;            
            //check.QueryCheckStateByValue += check_QueryCheckStateByValue;
            GridColumn col = new GridColumn
            {
                Caption = caption,
                FieldName = fieldName,
                //MinWidth = 30,
                Width = 30,
                Visible = true,
                VisibleIndex = gvDefault.Columns.Count,
                ColumnEdit = check,
                GroupIndex = -1
            };

            _showAllChecked = showAllChecks;
            _allCheckedName = fieldName;

            col.OptionsColumn.AllowEdit = true;
            col.OptionsColumn.AllowSize = true;
            col.OptionsColumn.ShowCaption = false;

            if (string.IsNullOrEmpty(fieldName))
                col.UnboundType = DevExpress.Data.UnboundColumnType.String;
            //col.OptionsColumn.ShowCaption = false;

            gvDefault.Columns.Add(col);
            gvDefault.Columns[fieldName].Visible = true;
            //if (showAllChecks)
            //{
            //    this.gvDefault.Click += gvDefault_Click;
            //}
        }

        void check_QueryCheckStateByValue(object sender, QueryCheckStateByValueEventArgs e)
        {
            string val = e.Value != null ? e.Value.ToString() : "True";
            switch (val)
            {
                case "True":
                    e.CheckState = CheckState.Unchecked;
                    break;
                default:
                    e.CheckState = CheckState.Checked;
                    break;
            }
            //switch (val)
            //{
            //    case "True":
            //        e.CheckState = CheckState.Checked;
            //        break;
            //    case "False":
            //        e.CheckState = CheckState.Unchecked;
            //        break;
            //    case "Yes":
            //        goto case "True";
            //    case "No":
            //        goto case "False";
            //    case "1":
            //        goto case "True";
            //    case "0":
            //        goto case "False";
            //    default:
            //        e.CheckState = CheckState.Checked;
            //        break;
            //}
            e.Handled = true;
        }

        /// <summary>
        /// 将列添加到集合中
        /// </summary>
        /// <param name="caption">列标题单元格的标题文本。</param>
        /// <param name="fieldName">绑定的数据库列的名称。</param>
        /// <param name="dataSource">组合框数据源</param>
        /// <param name="valueMember">在组合框中下拉列表的选项对应的值的属性或列。</param>
        /// <param name="displayMember">在组合框中显示的字符串的属性或列。</param>
        /// <param name="visible">指示该列是否可见。默认为可见。</param>
        public void Add(string caption, string fieldName,
            object dataSource, string valueMember, string displayMember, bool visible = true)
        {
            if (dataSource != null)
            {
                RepositoryItemImageComboBox c = new RepositoryItemImageComboBox();
                DataSet dataSet = dataSource as DataSet;
                if (dataSet != null)
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        ImageComboBoxItem i = new ImageComboBoxItem(dr[displayMember].ToString(), dr[valueMember]);
                        c.Items.Add(i);
                    }
                this.Add(caption, fieldName, visible, c);
            }
        }

        #endregion

        /// <summary>
        /// 根据字段及字段值选中行
        /// </summary>
        public void SelectRow(string fieldName, object value)
        {
            if (gvDefault.FocusedRowHandle > -1
                && gvDefault.GetRowCellValue(gvDefault.FocusedRowHandle, fieldName).Equals(value))
                return;
            for (int i = 0; i < gvDefault.RowCount; i++)
            {
                for (int j = 0; j < gvDefault.Columns.Count; j++)
                {
                    if (gvDefault.GetRowCellValue(i, fieldName).Equals(value))
                    {
                        gvDefault.FocusedRowHandle = i;
                        gvDefault.SelectRow(i);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 首行选中
        /// </summary>
        public void SelectFirstRow()
        {
            gvDefault.FocusedRowHandle = 0;
            gvDefault.SelectRow(0);
        }

        /// <summary>
        /// 获取选中行单元格值
        /// </summary>
        /// <param name="fieldName">字段</param>
        /// <returns></returns>
        public object GetSelectCellValue(string fieldName)
        {
            return gvDefault.GetFocusedRowCellValue(fieldName);
        }

        /// <summary>
        /// 获取选中行(可能是实体对象)
        /// </summary>
        /// <returns></returns>
        public object GetSelectRow()
        {
            return gvDefault.GetRow(gvDefault.FocusedRowHandle);
        }

        /// <summary>
        /// 当前选择行
        /// </summary>
        public DataRow SelectedRow
        {
            get { return gvDefault.GetDataRow(gvDefault.FocusedRowHandle); }
        }

        /// <summary>
        /// 获取选中行(数据源是DataTable)
        /// </summary>
        /// <returns></returns>
        public DataRow GetSelectDataRow()
        {
            return gvDefault.GetDataRow(gvDefault.FocusedRowHandle);
        }

        /// <summary>
        /// 清除选中项
        /// </summary>
        public void ClearSelection()
        {
            gvDefault.ClearSelection();
        }

        /// <summary>
        /// 删除选中项
        /// </summary>
        public void DeleteSelectRow()
        {
            gvDefault.DeleteRow(gvDefault.FocusedRowHandle);
        }

        /// <summary>
        /// 展开所有分组
        /// </summary>
        public void ExpandGroups()
        {
            gvDefault.ExpandAllGroups();
        }

        #endregion

        private void gvDefault_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            GridGroupRowInfo gridGroupRowInfo = e.Info as GridGroupRowInfo;
            if (gridGroupRowInfo != null)
            {
                if (gridGroupRowInfo.Level == 1)
                    if (gridGroupRowInfo.Column.GroupFormat.FormatString == ComConst.FMT_DATE.TIME_SHORT)
                    {
                        DateTime d;
                        DateTime.TryParse(gridGroupRowInfo.EditValue.ToString(), out d);
                        gridGroupRowInfo.GroupText = d.ToString(gridGroupRowInfo.Column.GroupFormat.FormatString);
                    }
                //gridGroupRowInfo.GroupText = "第" + (e.RowHandle).ToString() + "行 " + gridGroupRowInfo.EditValue.ToString();
            }
        }

        public void RefreshData()
        {
            gvDefault.RefreshData();
        }

        private void gvDefault_CustomDrawEmptyForeground(object sender, CustomDrawEventArgs e)
        {
            //方法一（此方法为GridView设置了数据源绑定时，可用） 
            //ColumnView columnView = sender as ColumnView;
            //BindingSource bindingSource = this.gvDefault.DataSource as BindingSource;
            //if (bindingSource.Count == 0)
            //{
            //    string str = "没有查询到你所想要的数据!";
            //    Font f = new Font("宋体", 10, FontStyle.Bold);
            //    Rectangle r = new Rectangle(e.Bounds.Top + 5, e.Bounds.Left + 5, e.Bounds.Right - 5, e.Bounds.Height - 5);
            //    e.Graphics.DrawString(str, f, Brushes.Black, r);
            //}
            ////方法二（此方法为GridView没有设置数据源绑定时，使用，一般使用此种方法）  
            //if (this._flag)
            {
                if (this.gvDefault.RowCount == 0)
                {
                    const string str = "没有查询到你所想要的数据!";
                    Font f = new Font("宋体", 10, FontStyle.Bold);
                    Rectangle r = new Rectangle(e.Bounds.Left + 5, e.Bounds.Top + 5, e.Bounds.Width - 5, e.Bounds.Height - 5);
                    e.Graphics.DrawString(str, f, Brushes.Black, r);
                }
            }
        }

        private void gvDefault_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            //MessageBox.Show("123");
            if (e.RowHandle < 0) return;

            //if (gvDefault.GetDataRow(e.RowHandle) == null) return;
            //if (gvDefault.GetDataRow(e.RowHandle)["BED_LABEL"].ToString().Contains("1"))
            //   {
            ////该行数据的该列的值为1时,其背景色为gray
            //   e.Appearance.BackColor = Color.Gray;
            //   }
            //   else
            //   {
            //   e.Appearance.BackColor = Color.Blue;
            //   }

            //if (e.RowHandle == gvDefault.FocusedRowHandle)
            //{
            //    e.Appearance.ForeColor = Color.White;
            //    e.Appearance.BackColor = Color.RoyalBlue;
            //}

            if (ColumnCheckBoxNames == null) return;
            DataRow dr = gvDefault.GetDataRow(e.RowHandle);
            if (dr == null)
            {
                object obj = gvDefault.GetRow(e.RowHandle);
                Type t = obj.GetType();
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    if (ColumnCheckBoxNames.Contains(pi.Name))
                    {
                        //用pi.GetValue获得值
                        object value1 = pi.GetValue(obj, null);
                        if (value1 == null) return;

                        if (value1.ToString() != "1")
                        {
                            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Strikeout);
                            e.Appearance.ForeColor =
                                CommonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                                    .Colors.GetColor("DisabledText");
                        }
                    }
                }
            }
            else
            {
                foreach (string name in ColumnCheckBoxNames)
                {
                    if (dr[name] == null) return;
                    if (dr[name].ToString() != "1")
                    {
                        e.Appearance.Font = new Font(AppearanceObject.DefaultFont, FontStyle.Strikeout);
                        e.Appearance.ForeColor =
                            CommonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default).Colors.GetColor("DisabledText");
                    }
                }
            }

            //if (currentTask == null) return;
            //if (currentTask.Status == TaskStatus.Completed)
            //{
            //    e.Appearance.Font = FontResources.StrikeoutFont;
            //    e.Appearance.ForeColor = ColorHelper.DisabledTextColor;
            //}
            //if (currentTask.Status == TaskStatus.Deferred)
            //    e.Appearance.ForeColor = ColorHelper.DisabledTextColor;
            //if (currentTask.Status == TaskStatus.WaitingOnSomeoneElse)
            //    e.Appearance.ForeColor = ColorHelper.WarningColor;
            //if (currentTask.Priority == 2 && currentTask.Status != TaskStatus.Completed)
            //    e.Appearance.Font = FontResources.BoldFont;
            //if (currentTask.Overdue)
            //    e.Appearance.ForeColor = ColorHelper.CriticalColor;
        }

        /// <summary>
        /// 获取鼠标点击处的行号
        /// </summary>
        /// <returns></returns>
        public int GetMouseRowHandle()
        {
            Point pt = gvDefault.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo gv = gvDefault.CalcHitInfo(pt);

            if (gv.InRow)
            {
                return gv.RowHandle;
            }
            return -1;
        }

        private void gvDefault_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseDown != null)
                MouseDown(sender, e);
        }
    }

    //#region 委托事件：

    //// 病人改变事件类型定义
    //public delegate void UC_SelectRowChangedHandler(object sender, SelectRowChanged e);

    //// 病人改变事件参数
    //public class SelectRowChanged : EventArgs
    //{
    //    protected string _patientId = string.Empty;
    //    protected string _visitId = string.Empty;

    //    public SelectRowChanged(string patientId, string visitId)
    //    {
    //        _patientId = patientId;
    //        _visitId = visitId;
    //    }


    //    /// <summary>
    //    /// 属性[病人ID]
    //    /// </summary>
    //    public string PatientId
    //    {
    //        get
    //        {
    //            return _patientId;
    //        }
    //    }


    //    /// <summary>
    //    /// 属性[就诊序号]
    //    /// </summary>
    //    public string VisitId
    //    {
    //        get
    //        {
    //            return _visitId;
    //        }
    //    }
    //}
    //#endregion
}
