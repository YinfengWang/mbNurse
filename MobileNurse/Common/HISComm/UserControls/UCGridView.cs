using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Linq;

namespace HISPlus.UserControls
{
    public partial class UcGridView : UserControl
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
        public bool EnableOddEvenRow { get; set; }

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

        /// <summary>
        /// 列集合
        /// </summary>
        public GridColumnCollection Columns
        {
            get { return gvDefault.Columns; }
        }

        /// <summary>
        /// 单元格选中模式.默认为整行选中.
        /// </summary>
        public bool CellSelected { private get; set; }

        /// <summary>
        /// 可见列集合
        /// </summary>
        public GridColumnReadOnlyCollection VisibleColumns
        {
            get { return gvDefault.VisibleColumns; }
        }

        public GridColumn FocusedColumn
        {
            get { return gvDefault.FocusedColumn; }
        }

        /// <summary>
        /// 获取或设置一个值，该值指示是否向用户显示添加行的选项。
        /// </summary>
        /// <value>如果向用户显示“添加行”选项，为 true；否则为 false。默认值为 true。</value>
        [Category("自定义属性"), Description("获取或设置一个值，该值指示是否向用户显示添加行的选项。"),]
        [Browsable(false)]
        public bool AllowAddRows { get; set; }

        private bool _allowDeleteRows;
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
            set
            {
                _allowEdit = value;

                gvDefault.OptionsBehavior.Editable = value;
                //this.gvDefault.OptionsBehavior.ReadOnly = !value;
            }
        }

        /// <summary>
        /// 获取或设置一个值，该值指示是否允许通过点击列标题对列进行排序。
        /// </summary>
        /// <value>如果用户可以对列进行排序，为 true；否则为 false。默认值为 false。</value>
        [Category("自定义属性"), Description("获取或设置一个值，该值指示是否允许用户对 UCGridView 进行编辑操作。"), Browsable(true)]
        public bool AllowSort { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否允许用户一次选择 UCGridView 的多个单元格、行或列。
        /// </summary>
        /// <value>如果允许多选，为 true；否则为 false。默认值为 false。</value>
        [Category("自定义属性")]
        [Description("获取或设置一个值，该值指示是否允许用户一次选择 UCGridView 的多个单元格、行或列。")]
        [Browsable(true)]
        public bool MultiSelect
        {
            private get { return gvDefault.IsMultiSelect; }
            set { gvDefault.OptionsSelection.MultiSelect = value; }
        }

        /// <summary>
        /// 是否启用奇偶行交替颜色
        /// </summary>
        /// <value>如果启用，为 true；否则为 false。默认值为 false。</value>      
        [Category("自定义属性"), Description("是否显示检索框"), DefaultValue(false)]
        public bool ShowFindPanel { get; set; }

        private object _dataSource;

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

                //当配置数据源后，控件自动初始化
                if (!this.DesignMode && value != null)
                    Init();
            }
        }

        private bool _showHeaders = true;
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
            get { return _showHeaders; }
            set { _showHeaders = value; }
        }

        private bool _columnAutoWidth = true;
        /// <summary>
        /// 是否自动调整列宽[默认为是]
        /// </summary>
        public bool ColumnAutoWidth
        {
            get { return _columnAutoWidth; }
            set { _columnAutoWidth = value; }
        }

        public GridViewAppearances Appearance
        {
            get { return gvDefault.Appearance; }
        }

        /// <summary>
        /// 选中总行数
        /// </summary>
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
        /// 选中的单元格
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public GridCell[] SelectedCells
        {
            get { return gvDefault.GetSelectedCells(); }
        }

        /// <summary>
        /// 控件
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public GridControl GridControl
        {
            get { return gcDefault; }
        }

        /// <summary>
        /// 获取选中项
        /// </summary>        
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public int[] SelectedRows
        {
            get
            {
                return gvDefault.GetSelectedRows();
            }
        }

        /// <summary>
        /// 总行数
        /// </summary>
        public int RowCount
        {
            get
            {
                return gvDefault.RowCount;
            }
        }

        /// <summary>
        /// 选中行索引
        /// </summary>        
        public int FocusedRowHandle
        {
            get { return gvDefault.FocusedRowHandle; }
            set
            {
                if (value >= 0 && value < gvDefault.RowCount)
                    gvDefault.FocusedRowHandle = value;
            }
        }



        /// <summary>
        /// 是否自动生成列.默认为自动生成,如果手动添加列时,会自动置为false
        /// </summary>
        protected bool IsAutoGenerateColumns = true;

        /// <summary>
        /// 是否显示行指示器
        /// </summary>
        public bool ShowRowIndicator { get; set; }

        private Dictionary<GridColumn, string> UniqueColumns;

        //private string UniqueString;

        #endregion 公共变量/属性

        #region 委托/事件

        /// <summary>
        /// 在 Grid 上，当前所选内容更改时发生
        /// </summary>
        public event EventHandler SelectionChanged;

        /// <summary>
        /// 在双击控件时发生.
        /// </summary>
        public new event EventHandler DoubleClick;

        /// <summary>
        /// 在 Grid 上释放键时发生
        /// </summary>
        public event KeyEventHandler KeyUpOnGrid;

        /// <summary>
        /// 在 Grid 上，数据源更改时发生
        /// </summary>
        public event EventHandler DataSourceChanged;

        public event CancelEventHandler ShowingEditor;

        /// <summary>
        /// 单元格值更改时发生
        /// </summary>
        public event CellValueChangedEventHandler CellValueChanged;

        /// <summary>
        /// 绑定数据源时发生，初始化自定义列数据
        /// </summary>
        public event CustomColumnDataEventHandler CustomColumnData;

        /// <summary>
        /// 按钮按下事件
        /// </summary>
        public new event MouseEventHandler MouseDown;

        public event InitNewRowEventHandler InitNewRow;

        public event RowStyleEventHandler RowStyle;
		public event RowStyleEventHandler RowStyle2;//2016.01.26 增加
        public event RowCellStyleEventHandler RowCellStyle;
        /// <summary>
        /// 复选框列的列名集合，以逗号分隔。
        /// 为了使不可用的列以删除线和灰色标识。
        /// </summary>
        private List<string> _columnCheckBoxNames;

        ///// <summary>
        ///// 以卡片形式作数据展示
        ///// </summary>
        //private bool ShowCard;

        /// <summary>
        /// 获取或设置与此控件关联的 System.Windows.Forms.ContextMenuStrip。        
        /// </summary>
        public override ContextMenuStrip ContextMenuStrip
        {
            get { return gcDefault.ContextMenuStrip; }
            set { gcDefault.ContextMenuStrip = value; }
        }


        #endregion

        public UcGridView()
        {
            AllowAddRows = false;
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

            //边框
            gvDefault.Appearance.HorzLine.BackColor = Color.FromArgb(135, 182, 236);
            gvDefault.Appearance.VertLine.BackColor = Color.FromArgb(135, 182, 236);

            if (EnableOddEvenRow)
            {
                //设置奇偶行不同颜色
                //gvDefault.Appearance.OddRow.BackColor = Color.BurlyWood;  // 设置奇数行颜色 // 默认是白色
                //gvDefault.OptionsView.EnableAppearanceOddRow = true;   // 使能 // 和和上面绑定 同时使用有效 
                //gvDefault.Appearance.EvenRow.BackColor = Color.CadetBlue; // 偶数行背景色
                gvDefault.Appearance.EvenRow.BackColor = Color.AliceBlue;
                gvDefault.OptionsView.EnableAppearanceEvenRow = true;
            }

            gvDefault.Appearance.SelectedRow.BackColor = Color.FromArgb(30, 0, 0, 240);
            //gvDefault.Appearance.HideSelectionRow.BackColor = Color.FromArgb(60, 0, 0, 240);            
            //gvDefault.Appearance.FocusedCell.BackColor = Color.FromArgb(60, 0, 0, 240);
            gvDefault.Appearance.FocusedRow.BackColor = Color.FromArgb(60, 0, 0, 240);

            //如果主从表中，没有找到从表内容也要显示(默认是不显示的)
            gvDefault.OptionsDetail.AllowZoomDetail = false;
            gvDefault.OptionsDetail.EnableMasterViewMode = false;
            gvDefault.OptionsDetail.SmartDetailExpand = false;
            gvDefault.OptionsDetail.ShowDetailTabs = false;
            gvDefault.OptionsDetail.AllowExpandEmptyDetails = false;

            gvDefault.FixedLineWidth = 1;
            // 是否可编辑
            //gvDefault.OptionsBehavior.Editable = _allowEdit;
            //this.gvDefault.OptionsBehavior.ReadOnly = !_allowEdit;
            gvDefault.OptionsBehavior.Editable = _allowEdit;
            this.gvDefault.OptionsBehavior.ReadOnly = !_allowEdit;

            gvDefault.OptionsBehavior.AllowAddRows = AllowAddRows ? DefaultBoolean.True : DefaultBoolean.False;

            if (_allowEdit)
            {
                gvDefault.OptionsBehavior.EditorShowMode = EditorShowMode.Click;
                gvDefault.OptionsBehavior.EditingMode = GridEditingMode.Inplace;
            }
            // 标题行过滤
            gvDefault.OptionsCustomization.AllowFilter = AllowSort;
            //// 是否允许列排序
            gvDefault.OptionsCustomization.AllowSort = AllowSort;
            //列移动【允许】
            gvDefault.OptionsCustomization.AllowColumnMoving = false;
            //改变列宽【允许】
            gvDefault.OptionsCustomization.AllowColumnResizing = true;

            //gvDefault.OptionsCustomization.AllowRowSizing = true;
            //gvDefault.OptionsBehavior.KeepFocusedRowOnUpdate = true;  
            //自动列高
            gvDefault.OptionsView.RowAutoHeight = true;
            // 行标题居中
            gvDefault.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            // 内容居中
            //gvDefault.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;


            // 是否显示列标题右键菜单
            gvDefault.OptionsMenu.EnableColumnMenu = false;

            //gvDefault.OptionsFilter.AllowFilterEditor = true;
            gvDefault.OptionsFind.AllowFindPanel = ShowFindPanel;
            gvDefault.OptionsFind.AlwaysVisible = ShowFindPanel;
            gvDefault.OptionsFind.ClearFindOnClose = false;
            gvDefault.OptionsFind.ShowFindButton = true;
            gvDefault.OptionsFind.ShowClearButton = false;
            //gvDefault.OptionsFind.SearchInPreview = false;
            //gvDefault.OptionsFind.HighlightFindResults = true;
            //gvDefault.OptionsFind.FindMode = FindMode.Always;

            //设置列是否可排序
            //gvDefault.Columns["ID"].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
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
                this.gvDefault.CustomDrawRowIndicator += gvDefault_CustomDrawRowIndicator;
            }

            //列标题栏【显示】
            gvDefault.OptionsView.ShowColumnHeaders = _showHeaders;
            //设置自动列宽（这样的话表格下方可能会出现滚动条或者未铺满）
            gvDefault.OptionsView.ColumnAutoWidth = _columnAutoWidth;

            //gvDefault.OptionsView.ShowAutoFilterRow = true;
            //gvDefault.Appearance.HeaderPanel
            //是否自动合并单元格【慎用】
            //gvDefault.OptionsView.AllowCellMerge = true;

            //gvDefault.OptionsSelection.EnableAppearanceFocusedCell = false; //设置单元格不能选择（如果不设置，则点击到的单元格在整行选择情况下的背景色不变）

            //gvDefault.OptionsSelection.EnableAppearanceFocusedRow = true; //禁止选择行

            //gvDefault.OptionsSelection.MultiSelect = this.MultiSelect; //设置可多选
            gvDefault.OptionsSelection.EnableAppearanceHideSelection = false;

            //多选行还是多选单元格（一般选RowSelect）
            //if (_allowEdit)
            //    gvDefault.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            //else
            if (this.CellSelected)
                gvDefault.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            else
                gvDefault.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;

            gvDefault.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DefaultBoolean.True;

            // 被选中后鼠标点击的单元格颜色不变
            gvDefault.OptionsSelection.EnableAppearanceFocusedCell = true;
            gvDefault.OptionsSelection.EnableAppearanceFocusedRow = true;
            gvDefault.OptionsSelection.EnableAppearanceHideSelection = false;
            //gvDefault.OptionsSelection.InvertSelection = true;

            //取消点击后的虚线框
            gvDefault.FocusRectStyle = DrawFocusRectStyle.None;

            //清除当前选择
            //在选中列时后，可配置在选中列以外的地方点击时候会清除当前的选择。14以后才有此功能
            //gvDefault.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = true; 

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

            //if (ShowCard)
            //{
            //    this.gcDefault.ViewCollection.AddRange(new BaseView[] {
            //this.gvDefault});

            //    this.gvDefault.OptionsBehavior.AllowFixedGroups = DefaultBoolean.True;
            //    this.gvDefault.OptionsBehavior.AllowPixelScrolling = DefaultBoolean.True;
            //    this.gvDefault.OptionsBehavior.AutoExpandAllGroups = true;
            //    this.gvDefault.OptionsBehavior.Editable = false;
            //    this.gvDefault.OptionsDetail.EnableMasterViewMode = false;
            //    this.gvDefault.OptionsFind.AlwaysVisible = true;
            //    this.gvDefault.OptionsPrint.PrintHorzLines = false;
            //    this.gvDefault.OptionsPrint.PrintVertLines = false;
            //    this.gvDefault.OptionsView.ShowGroupedColumns = true;
            //    this.gvDefault.OptionsView.ShowGroupPanel = false;
            //    this.gvDefault.OptionsView.ShowIndicator = false;
            //    this.gvDefault.OptionsView.ShowVerticalLines = DefaultBoolean.False;
            //}

            if (gvDefault.SelectedRowsCount > 0)
                if (SelectionChanged != null)
                    SelectionChanged(null, null);

            //if (_showAllChecked)
            //{
            //    this.gvDefault.Click += this.gvDefault_Click;
            //    this.gvDefault.CustomDrawColumnHeader += this.gvDefault_CustomDrawColumnHeader;
            //}

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
                //this.gvDefault.IndicatorWidth = (e.Info.DisplayText.Length) * 20;
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

            if (CellValueChanged != null)
                gvDefault.CellValueChanged += gvDefault_CellValueChanged;

            if (CustomColumnData != null)
                gvDefault.CustomUnboundColumnData += gvDefault_CustomUnboundColumnData;

            if (_allowEdit)
            {
                gvDefault.EditFormShowing += gvDefault_EditFormShowing;

            }

            if (ShowingEditor != null)
            {
                gvDefault.ShowingEditor += gvDefault_ShowingEditor;
            }

            //if (DoubleClick != null)
            //{
            //    gvDefault.DoubleClick += new EventHandler(gvDefault_DoubleClick);
            //}

            if (InitNewRow != null)
                gvDefault.InitNewRow += gvDefault_InitNewRow;

            if (RowStyle != null)
                gvDefault.RowStyle += new RowStyleEventHandler(gvDefault_RowStyle);
            if (RowStyle2 != null)
                gvDefault.RowStyle += new RowStyleEventHandler(gvDefault_RowStyle);
        }

        void gvDefault_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            InitNewRow(sender, e);
        }

        void gvDefault_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            CellValueChanged(sender, e);
        }

        void gvDefault_ShowingEditor(object sender, CancelEventArgs e)
        {
            ShowingEditor(sender, e);

        }

        void gvDefault_EditFormShowing(object sender, EditFormShowingEventArgs e)
        {

        }

        public void ShowToolTip(string content)
        {
            Point mousePoint = Control.MousePosition;
            toolTipController1.ShowHint(content, "提示", mousePoint);
        }

        private void gvDefault_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (gvDefault.SelectedRowsCount > 0 && gvDefault.FocusedRowHandle >= 0)
                SelectionChanged(sender, e);
        }

        private void gvDefault_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (gvDefault.SelectedRowsCount > 0 && gvDefault.FocusedRowHandle >= 0)
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
        /// <param name="status"></param>
        public void Add(string caption, string fieldName, ColumnStatus status)
        {
            this.Add(caption, fieldName, 50, status);
        }

        /// <summary>
        /// 将列添加到集合中
        /// </summary>
        /// <param name="caption">列标题单元格的标题文本。</param>
        /// <param name="fieldName">绑定的数据库列的名称。</param>
        /// <param name="width">列宽度</param>
        public void Add(string caption, string fieldName, int width)
        {
            this.NewColumn(caption, fieldName, width);
        }

        /// <summary>
        /// 将列添加到集合中
        /// </summary>
        /// <param name="caption">列标题单元格的标题文本。</param>
        /// <param name="fieldName">绑定的数据库列的名称。</param>
        /// <param name="width">列宽度</param>
        /// <param name="status"></param>
        public void Add(string caption, string fieldName, int width, ColumnStatus status)
        {
            this.Add(caption, fieldName, null, width, status);
        }

        /// <summary>
        /// 将列添加到集合中
        /// </summary>
        /// <param name="caption">列标题单元格的标题文本。</param>
        /// <param name="fieldName">绑定的数据库列的名称。</param>
        /// <param name="stringFormat">格式化字符串,一般为日期</param>
        /// <param name="unique">是否唯一</param>
        /// <param name="status"></param>
        public void Add(string caption, string fieldName, string stringFormat, ColumnStatus status = ColumnStatus.None)
        {
            this.Add(caption, fieldName, stringFormat, 50, status);
        }

        /// <summary>
        /// 将列添加到集合中
        /// </summary>
        /// <param name="caption">列标题单元格的标题文本。</param>
        /// <param name="fieldName">绑定的数据库列的名称。</param>
        /// <param name="stringFormat">格式化字符串,一般为日期</param>
        /// <param name="width">列宽</param>
        /// <param name="status"></param>
        public void Add(string caption, string fieldName, string stringFormat, int width, ColumnStatus status = ColumnStatus.None)
        {
            GridColumn col = NewColumn(caption, fieldName, width);

            if ((status & ColumnStatus.WordWrap) == ColumnStatus.WordWrap)
            {
                gvDefault.OptionsView.RowAutoHeight = true;
                RepositoryItemMemoEdit ritem = new RepositoryItemMemoEdit { ScrollBars = ScrollBars.None };
                col.ColumnEdit = ritem;
                ritem.DoubleClick += ritem_DoubleClick;
                col.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
            }
            if ((status & ColumnStatus.Fixed) == ColumnStatus.Fixed)
            {
                col.Fixed = FixedStyle.Left;
            }
            if ((status & ColumnStatus.Center) == ColumnStatus.Center)
            {
                col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }

            // 是否可编辑
            if ((status & ColumnStatus.AllowEdit) == ColumnStatus.AllowEdit)
                col.OptionsColumn.AllowEdit = true;
            if ((status & ColumnStatus.ReadOnly) == ColumnStatus.ReadOnly)
            {
                col.OptionsColumn.ReadOnly = true;
                col.OptionsColumn.AllowEdit = false;
            }

            if ((status & ColumnStatus.Unique) == ColumnStatus.Unique)
            {
                if (UniqueColumns == null)
                    UniqueColumns = new Dictionary<GridColumn, string>();

                UniqueColumns.Add(col, string.Empty);
            }

            if (!string.IsNullOrEmpty(stringFormat))
            {
                RepositoryItemDateEdit date = new RepositoryItemDateEdit();

                date.Mask.EditMask = stringFormat;
                date.EditFormat.FormatString = stringFormat;
                date.DisplayFormat.FormatString = stringFormat;
                col.DisplayFormat.FormatType = FormatType.DateTime;
                col.ColumnEdit = date;
                col.DisplayFormat.FormatString = stringFormat;
                date.AllowMouseWheel = false;
            }
        }

        /// <summary>
        /// 将列添加到集合中
        /// </summary>
        /// <param name="caption">列标题单元格的标题文本。</param>
        /// <param name="fieldName">绑定的数据库列的名称。</param>
        /// <param name="visible">是否可见</param>
        public void Add(string caption, string fieldName, bool visible = true)
        {
            this.NewColumn(caption, fieldName).Visible = visible;
        }

        /// <summary>
        /// 添加新列.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="fieldName"></param>
        /// <param name="width"></param>
        /// <param name="minWidth"></param>
        /// <returns></returns>
        protected GridColumn NewColumn(string caption, string fieldName, int width = 50, int minWidth = 30)
        {
            if (IsAutoGenerateColumns)
            {
                this.ClearColumn();
                IsAutoGenerateColumns = false;
            }

            GridColumn col = new GridColumn
            {
                Caption = caption,
                FieldName = fieldName,
                MinWidth = minWidth,
                Width = width,
                VisibleIndex = gvDefault.Columns.Count,
            };

            if (string.IsNullOrEmpty(caption))
                col.OptionsColumn.ShowCaption = false;

            col.OptionsColumn.AllowEdit = _allowEdit;
            col.OptionsColumn.ReadOnly = !_allowEdit;

            gvDefault.Columns.Add(col);

            return col;
        }

        /// <summary>
        /// 将列添加到集合中
        /// </summary>
        /// <param name="caption">列标题单元格的标题文本。</param>
        /// <param name="fieldName">绑定的数据库列的名称。</param>
        /// <param name="dataSource"></param>
        /// <param name="displayName"></param>
        /// <param name="width"></param>
        /// <param name="minWidth"></param>
        /// <param name="valueName"></param>
        public void Add(string caption, string fieldName, object dataSource, string valueName, string displayName, int width = 50, int minWidth = 30)
        {
            GridColumn col = NewColumn(caption, fieldName, width, minWidth);

            if (dataSource != null)
            {
                RepositoryItemLookUpEdit l = new RepositoryItemLookUpEdit
                {
                    DataSource = dataSource,
                    ValueMember = valueName,
                    DisplayMember = displayName,
                    NullText = string.Empty
                };
                l.ShowDropDown = ShowDropDown.SingleClick;
                l.Columns.Add(new LookUpColumnInfo(displayName));
                l.ShowHeader = false;
                l.ShowFooter = false;
                col.ColumnEdit = l;
            }
        }

        /// <summary>
        /// 将列添加到集合中
        /// </summary>
        /// <param name="caption">列标题单元格的标题文本。</param>
        /// <param name="fieldName">绑定的数据库列的名称。</param>
        /// <param name="visible">指示该列是否可见。默认为可见。</param>
        /// <param name="type">数据类型</param>
        /// <param name="width"></param>
        /// <param name="minWidth"></param>
        /// <param name="format">格式化字符串</param>
        public void Add(string caption, string fieldName, Type type)
        {
            GridColumn col = NewColumn(caption, fieldName);

            RepositoryItemImageComboBox c = new RepositoryItemImageComboBox();

            if (type == typeof(bool))
            {
                c.Items.Add(new ImageComboBoxItem(@"可用", true, 1));
                c.Items.Add(new ImageComboBoxItem(@"不可用", false, 0));
            }
            else
            {
                c.Items.Add(new ImageComboBoxItem(@"可用", Convert.ChangeType(1, type), 1));
                c.Items.Add(new ImageComboBoxItem(@"不可用", Convert.ChangeType(0, type), 0));
            }

            ImageList imageList = new ImageList();
            imageList.Images.Add(Properties.Resources.cancel_16x16);
            imageList.Images.Add(Properties.Resources.apply_16x16);

            c.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            c.GlyphAlignment = HorzAlignment.Center;
            c.SmallImages = imageList;
            c.LargeImages = imageList;
            col.ColumnEdit = c;

            col.OptionsColumn.AllowFocus = false;
            col.OptionsColumn.AllowSize = false;
            col.OptionsColumn.FixedWidth = true;
            col.OptionsColumn.AllowEdit = _allowEdit;
            //col.OptionsColumn.ShowCaption = false;

            if (_columnCheckBoxNames == null)
                _columnCheckBoxNames = new List<string>();
            _columnCheckBoxNames.Add(fieldName);

            if (type != null)
                switch (type.Name)
                {
                    case "Int32":
                        col.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                        break;
                    case "Decimal":
                        col.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
                        break;
                    case "Boolean":
                        col.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
                        break;
                    default:
                        col.UnboundType = DevExpress.Data.UnboundColumnType.String;
                        break;
                }
        }

        void ritem_DoubleClick(object sender, EventArgs e)
        {
            if (DoubleClick != null)
                DoubleClick(sender, e);
        }

        #endregion

        /// <summary>
        /// 选中行
        /// </summary>
        public void SelectRow(int rowHandle)
        {
            gvDefault.SelectRow(rowHandle);
        }

        /// <summary>
        /// 选中行
        /// </summary>
        public void AddNewRow()
        {

            gvDefault.AddNewRow();
        }

        /// <summary>
        /// 根据字段及字段值选中行
        /// </summary>
        public void SelectRow(string fieldName, object value)
        {
            //if (gvDefault.FocusedRowHandle > -1
            //    && gvDefault.GetRowCellValue(gvDefault.FocusedRowHandle, fieldName).Equals(value))
            //    return;

            gvDefault.ClearSelection();
            bool focusFirst = true;
            for (int rowHandle = 0; rowHandle < gvDefault.RowCount; rowHandle++)
            {
                for (int j = 0; j < gvDefault.Columns.Count; j++)
                {
                    if (gvDefault.GetRowCellValue(rowHandle, fieldName).Equals(value))
                    {
                        if (focusFirst)
                        {
                            focusFirst = false;
                            gvDefault.FocusedRowHandle = rowHandle;
                        }
                        gvDefault.SelectRow(rowHandle);
                        if (this.MultiSelect)
                            break;
                        return;
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
        /// 获取单元格值
        /// </summary>
        /// <param name="rowHandle">行号</param>
        /// <param name="fieldName">字段</param>
        /// <returns></returns>
        public object GetRowCellValue(int rowHandle, string fieldName)
        {
            return gvDefault.GetRowCellValue(rowHandle, fieldName);
        }

        /// <summary>
        /// 获取单元格值
        /// </summary>
        /// <param name="rowHandle">行号</param>
        /// <param name="column">字段</param>
        /// <returns></returns>
        public object GetRowCellValue(int rowHandle, GridColumn column)
        {
            return gvDefault.GetRowCellValue(rowHandle, column);
        }

        /// <summary>
        /// 获取选中行单元格值
        /// </summary>
        /// <param name="fieldName">字段</param>
        /// <returns></returns>
        public object GetFocusedRowCellValue(string fieldName)
        {
            return gvDefault.GetFocusedRowCellValue(fieldName);
        }

        /// <summary>
        /// 获取选中行单元格值
        /// </summary>
        /// <returns></returns>
        public object GetFocusedRowCellValue(GridColumn column)
        {
            return gvDefault.GetFocusedRowCellValue(column);
        }

        /// <summary>
        /// 获取数据源行索引
        /// </summary>
        /// <returns></returns>
        public int GetDataSourceRowIndex(int rowHandle)
        {
            return gvDefault.GetDataSourceRowIndex(rowHandle);
        }

        /// <summary>
        /// 设置单元格值
        /// </summary>
        /// <param name="rowHandle">行号</param>
        /// <param name="fieldName">字段</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void SetRowCellValue(int rowHandle, string fieldName, object value)
        {
            gvDefault.SetRowCellValue(rowHandle, fieldName, value);
        }

        /// <summary>
        /// 设置单元格值
        /// </summary>
        /// <param name="rowHandle">行号</param>
        /// <param name="column">字段</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void SetRowCellValue(int rowHandle, GridColumn column, object value)
        {
            gvDefault.SetRowCellValue(rowHandle, column, value);
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
        /// 获取行
        /// </summary>
        /// <returns></returns>
        public object GetRow(int rowHandle)
        {
            return gvDefault.GetRow(rowHandle);
        }


        /// <summary>
        /// 隐藏滚动条
        /// </summary>
        /// <returns></returns>
        public void HideScroll()
        {
            gvDefault.HorzScrollVisibility = ScrollVisibility.Never;
            gvDefault.VertScrollVisibility = ScrollVisibility.Never;
        }

        /// <summary>
        /// 当前选择行
        /// </summary>
        public DataRow SelectedRow
        {
            get { return gvDefault.GetDataRow(gvDefault.FocusedRowHandle); }
        }

        private string _columnsEvenOldRowColor;

        protected Dictionary<GridColumn, string> DicColumnsEvenOldRowColor;

        protected readonly Color[] RowColors = { Color.White, Color.BlanchedAlmond };//2016.01.26 原AliceBlue

        protected Dictionary<int, Color> DicRowColors;

        /// <summary>
        /// 设定一列或多列，根据列值的不同实现奇偶色交替
        /// </summary>
        public string ColumnsEvenOldRowColor
        {
            get { return _columnsEvenOldRowColor; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    DicColumnsEvenOldRowColor = new Dictionary<GridColumn, string>();

                    string[] strColumns = value.Split(new[] { ',' });
                    if (strColumns.Length == 0) return;

                    foreach (string columnName in strColumns)
                    {
                        DicColumnsEvenOldRowColor.Add(gvDefault.Columns.ColumnByFieldName(columnName), string.Empty);
                    }
                    DicRowColors = new Dictionary<int, Color>();
                }
                _columnsEvenOldRowColor = value;
            }
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
            if (gvDefault.SelectedRowsCount == 0) return;
            gvDefault.DeleteRow(gvDefault.FocusedRowHandle);
            if (!CellSelected) return;
            if (gvDefault.GetSelectedCells().Length > 0)
                gvDefault.FocusedRowHandle = gvDefault.GetSelectedCells()[0].RowHandle;
        }

        /// <summary>
        /// 删除项
        /// </summary>
        public void DeleteRow(int rowHandle)
        {
            gvDefault.DeleteRow(rowHandle);
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

            if (UniqueColumns != null)
            {
                if (e.RowHandle != 0 && UniqueColumns.ContainsKey(e.Column))
                {
                    string prevText = gvDefault.GetRowCellDisplayText(e.RowHandle - 1, e.Column);
                    GridColumn gcFirst = UniqueColumns.First().Key;

                    if (gcFirst == e.Column)
                    {
                        if (prevText == e.DisplayText)
                            e.DisplayText = string.Empty;
                    }
                    else
                    {
                        //if (gvDefault.GetRowCellDisplayText(e.RowHandle, gcFirst) == string.Empty)
                        if (gvDefault.GetRowCellDisplayText(e.RowHandle, gcFirst) ==
                            gvDefault.GetRowCellDisplayText(e.RowHandle - 1, gcFirst))
                            if (prevText == e.DisplayText)
                                e.DisplayText = string.Empty;
                    }
                }
            }

            if (_columnCheckBoxNames == null) return;
            DataRow dr = gvDefault.GetDataRow(e.RowHandle);
            if (dr == null)
            {
                object obj = gvDefault.GetRow(e.RowHandle);
                Type t = obj.GetType();
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    if (_columnCheckBoxNames.Contains(pi.Name))
                    {
                        //用pi.GetValue获得值
                        object value1 = pi.GetValue(obj, null);
                        if (value1 == null) return;

                        if (value1.ToString() != "1" && value1.ToString() != "True")
                        {
                            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Strikeout);
                            e.Appearance.ForeColor =
                                CommonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                                    .Colors.GetColor(CommonColors.DisabledText);
                        }
                    }
                }
            }
            else
            {
                foreach (string name in _columnCheckBoxNames)
                {
                    if (dr[name] == null) return;
                    if (dr[name].ToString() != "1" && dr[name].ToString() != "True")
                    {
                        e.Appearance.Font = new Font(AppearanceObject.DefaultFont, FontStyle.Strikeout);
                        e.Appearance.ForeColor =
                            CommonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default).Colors.GetColor(CommonColors.DisabledText);
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
            Point pt = gvDefault.GridControl.PointToClient(MousePosition);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvDefault_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {

                if (RowStyle2 != null && e.RowHandle >= 0 && DicRowColors == null)
                    RowStyle2(sender, e);

                if (e.RowHandle < 0) return;
                if (DicRowColors == null) return;

                if (DicColumnsEvenOldRowColor != null)
                {
                    if (DicRowColors.ContainsKey(e.RowHandle))
                        e.Appearance.BackColor = DicRowColors[e.RowHandle];
                }
                #region 2015.12.04
                if (RowStyle != null)
                    RowStyle(sender, e);
                #endregion

                //if (e.RowHandle == 0)
                //{
                //    if (!_dicRowColors.ContainsKey(e.RowHandle))
                //        _dicRowColors.Add(e.RowHandle, _rowColors[0]);
                //    return;
                //}
                //if (_dicColumnsEvenOldRowColor != null)
                //{
                //    int i = 0;
                //    bool isEqual = true;
                //    foreach (GridColumn gc in _dicColumnsEvenOldRowColor.Keys)
                //    {
                //        if (gvDefault.GetRowCellValue(e.RowHandle, gc) != null && gvDefault.GetRowCellValue(e.RowHandle - 1, gc) != null)
                //        {
                //            if (gvDefault.GetRowCellValue(e.RowHandle, gc).ToString() ==
                //                gvDefault.GetRowCellValue(e.RowHandle - 1, gc).ToString())
                //                continue;
                //        }
                //        isEqual = false;
                //        break;
                //    }

                //    Color bk = _dicRowColors[e.RowHandle - 1];

                //    if (isEqual)
                //    {
                //        if (!_dicRowColors.ContainsKey(e.RowHandle))
                //            _dicRowColors.Add(e.RowHandle, bk);
                //    }
                //    else
                //    {
                //        bk = _rowColors[0] == bk ? _rowColors[1] : _rowColors[0];
                //        if (!_dicRowColors.ContainsKey(e.RowHandle))
                //            _dicRowColors.Add(e.RowHandle, bk);
                //    }

                //    e.Appearance.BackColor = bk;
                //}
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void gcDefault_DataSourceChanged(object sender, EventArgs e)
        {
            if (gvDefault.RowCount == 0) return;

            if (DataSourceChanged != null)
                DataSourceChanged(sender, e);

            this.gvDefault.IndicatorWidth = gvDefault.DataRowCount > 9 ? 32 : 18;

            if (DicRowColors == null) return;

            DicRowColors.Clear();

            for (int rowHandle = 0; rowHandle < gvDefault.RowCount; rowHandle++)
            {
                if (rowHandle == 0)
                {
                    if (!DicRowColors.ContainsKey(rowHandle))
                        DicRowColors.Add(rowHandle, RowColors[0]);
                    continue;
                }
                if (DicColumnsEvenOldRowColor != null)
                {
                    bool isEqual = true;
                    foreach (GridColumn gc in DicColumnsEvenOldRowColor.Keys)
                    {
                        if (gvDefault.GetRowCellValue(rowHandle, gc) != null && gvDefault.GetRowCellValue(rowHandle - 1, gc) != null)
                        {
                            if (gvDefault.GetRowCellValue(rowHandle, gc).ToString() ==
                                gvDefault.GetRowCellValue(rowHandle - 1, gc).ToString())
                                continue;
                        }
                        isEqual = false;
                        break;
                    }
                    Color bk = DicRowColors[rowHandle - 1];

                    if (isEqual)
                    {
                        if (!DicRowColors.ContainsKey(rowHandle))
                            DicRowColors.Add(rowHandle, bk);
                    }
                    else
                    {
                        bk = RowColors[0] == bk ? RowColors[1] : RowColors[0];
                        if (!DicRowColors.ContainsKey(rowHandle))
                            DicRowColors.Add(rowHandle, bk);
                    }
                }
            }
        }

        private void gvDefault_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            bool b = true;
            if (this.FindForm() is IGridCellChecked)
            {

                IGridCellChecked i = this.FindForm() as IGridCellChecked;
                b = i.GridCellChecked(e.RowHandle, e.Column);
            }
            if (b)
                if (_allowEdit && e.Column.OptionsColumn.AllowEdit)
                    // 状态列[可用|不可用]单击即可更改
                    if (e.Column.ColumnEdit != null)
                    {
                        RepositoryItemImageComboBox r = e.Column.ColumnEdit as RepositoryItemImageComboBox;
                        if (r != null)
                        {
                            gvDefault.SetRowCellValue(e.RowHandle, e.Column,
                                e.CellValue.ToString() == r.Items[0].Value.ToString() ? r.Items[1].Value : r.Items[0].Value);
                            // e.CellValue 
                        }
                    }

            // 触发双击事件
            if (e.Button == MouseButtons.Left && e.RowHandle >= 0 && e.Clicks == 2)
            {
                if (DoubleClick != null)
                    DoubleClick(sender, e);
            }
        }

        private void gvDefault_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //try
            //{
            //    RowCellStyle(sender, e);
            //}
            //catch (Exception ex)
            //{
            //    throw (ex);
            //}
        }
    }

    /// <summary>
    /// 列状态
    /// </summary>
    [Flags]
    public enum ColumnStatus
    {
        /// <summary>
        /// 无状态
        /// </summary>
        None = 0,

        /// <summary>
        /// 是否可见[因为如果设置特殊属性,肯定是可见的,所以不再使用此属性]
        /// </summary>
        //Visible = 1,

        /// <summary>
        /// 是否允许编辑
        /// </summary>
        AllowEdit = 2,

        /// <summary>
        /// 是否唯一.即只显示一次,重复值自动隐藏
        /// </summary>
        Unique = 4,

        /// <summary>
        /// 是否固定.[左固定]
        /// </summary>
        Fixed = 8,

        /// <summary>
        /// 是否自动换行
        /// </summary>
        WordWrap = 16,

        /// <summary>
        /// 是否居中显示
        /// </summary>
        Center = 32,

        /// <summary>
        /// 是否只读.设置该属性时,则无法编辑.
        /// </summary>
        ReadOnly = 64,
    }
}
