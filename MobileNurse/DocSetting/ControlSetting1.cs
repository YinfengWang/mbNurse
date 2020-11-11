using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using HISPlus;
using DevExpress.XtraEditors.Repository;

namespace HISPlus
{
    public partial class ControlSetting1 : DevExpress.XtraEditors.XtraForm
    {
        private DocumentDbI documentDbI;

        public ControlSetting1()
        {
            InitializeComponent();
        }

        private void ControlSetting_Load(object sender, EventArgs e)
        {
            if (GVars.OracleAccess != null)
                documentDbI = new DocumentDbI(GVars.OracleAccess);

            BindDropDownList();

            //this.Add("模板编号", "CONTROL_TEMPLATE_ID", false);
            //this.Add("模板名称", "CONTROL_TEMPLATE_NAME");

            //RepositoryItemLookUpEdit tempControlType = new RepositoryItemLookUpEdit();
            //tempControlType.DataSource = ddlControlType.Properties.DataSource;
            //tempControlType.ValueMember = "CONTROL_TYPE_ID";
            //tempControlType.DisplayMember = "CONTROL_TYPE_NAME";            
            //this.Add("控件类型", "CONTROL_TYPE_ID", tempControlType);
            ////this.Add("控件类型", "CONTROL_TYPE_NAME");
            //this.Add("控件字体", "CONTROL_FONT");
            //this.Add("控件宽度", "CONTROL_WIDTH");
            //this.Add("控件高度", "CONTROL_HEIGHT");
            //this.Add("控件偏移量", "CONTROL_OFFSET");
            //this.Add("是否可用", "Enabled");
            //this.Add("备注", "REMARK");
            //Init();

            this.Add("模板编号", "ControlTemplateId", false);
            this.Add("模板名称", "ControlTemplateName");

            RepositoryItemLookUpEdit tempControlType = new RepositoryItemLookUpEdit();
            tempControlType.DataSource = ddlControlType.Properties.DataSource;
            tempControlType.ValueMember = "ControlTypeId";
            tempControlType.DisplayMember = "ControlTypeName";
            this.Add("控件类型", "ControlTypeId", tempControlType);
            //this.Add("控件类型", "CONTROL_TYPE_NAME");
            this.Add("控件字体", "ControlFont");
            this.Add("控件宽度", "ControlWidth");
            this.Add("控件高度", "ControlHeight");
            this.Add("控件偏移量", "ControlOffset");
            this.Add("是否可用", "IsEnabled");
            this.Add("备注", "Remark");
            Init();
            //gridView1.Columns["CONTROL_TEMPLATE_ID"].Visible = false;
            Bind();
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void Bind()
        {
            List<DocControlTemplate> list = documentDbI.GetAllControlTemplateList();

            gridControl1.DataSource = list;
        }

        /// <summary>
        /// 绑定下拉列表
        /// </summary>
        private void BindDropDownList()
        {
            // 控件状态的下拉列表
            //DataSet ds = documentDbI.GetAllControlType();
            //ddlControlType.Properties.DataSource = ds.Tables[0].DefaultView;

            //ddlControlType.Properties.ValueMember = "CONTROL_TYPE_ID";
            //ddlControlType.Properties.DisplayMember = "CONTROL_TYPE_NAME";
            //ddlControlType.Properties.Columns.AddRange(new LookUpColumnInfo[]
            //{
            //    new LookUpColumnInfo("CONTROL_TYPE_NAME", "类型"),
            //    new LookUpColumnInfo("REMARK", "说明")
            //});

            ddlControlType.Properties.DataSource = documentDbI.GetAllControlTypeList();

            ddlControlType.Properties.ValueMember = "ControlTypeId";
            ddlControlType.Properties.DisplayMember = "ControlTypeName";
            ddlControlType.Properties.Columns.AddRange(new LookUpColumnInfo[]
            {
                new LookUpColumnInfo("ControlTypeName", "类型"),
                new LookUpColumnInfo("Remark", "说明")
            });
            // 选中第一项
            ddlControlType.ItemIndex = 0;

            txtFont.Properties.ReadOnly = true;
        }

        private void txtFont_Click(object sender, EventArgs e)
        {
            FontDialog f = new FontDialog
            {
                Font = GetFont(txtFont.Text),
                ShowColor = true
            };
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.txtFont.Text = GetFontText(f.Font);
                //this.txtFont.Text = f.Font.Name + ", " + f.Font.Size.ToString() + "pt" + ((f.Font.Style == FontStyle.Regular) ? "" : (", " + f.Font.Style.ToString()));                
                MessageBox.Show(f.Font.ToString());
            }

        }

        private Control CreateControl(Enums.ControlType type)
        {
            BaseControl ctl;
            //switch (type)
            //{
            //case Enums.ControlType.
            //}

            return null;
        }

        private Font GetFont(string text)
        {
            if (string.IsNullOrEmpty(text)) return null;
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

        private string GetFontText(Font font)
        {
            return (font.Name + ", " + font.Size.ToString() + "pt" + ((font.Style == FontStyle.Regular) ? "" : (", " + font.Style.ToString())));
        }

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
            this.Add(caption, fieldName, visible, null, width);
        }

        /// <summary>
        /// 将列添加到集合中
        /// </summary>
        /// <param name="caption">列标题单元格的标题文本。</param>
        /// <param name="fieldName">绑定的数据库列的名称。</param>
        /// <param name="visible">指示该列是否可见。默认为可见。</param>
        public void Add(string caption, string fieldName, bool visible = true)
        {
            this.Add(caption, fieldName, visible, null);
        }

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
        private void Add(string caption, string fieldName, bool visible
            , RepositoryItem columnEdit, int width = 50, int minWidth = 30)
        {
            GridColumn col = new GridColumn();
            col.Caption = caption;
            col.FieldName = fieldName;
            col.MinWidth = minWidth;
            col.Width = width;
            //col.Visible = visible;
            col.VisibleIndex = gridView1.Columns.Count;

            col.ColumnEdit = columnEdit;

            if (string.IsNullOrEmpty(fieldName))
                col.UnboundType = DevExpress.Data.UnboundColumnType.String;

            gridView1.Columns.Add(col);
            gridView1.Columns[fieldName].Visible = visible;
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
                foreach (DataRow dr in (dataSource as DataSet).Tables[0].Rows)
                {
                    ImageComboBoxItem i = new ImageComboBoxItem(dr[displayMember].ToString(), dr[valueMember]);
                    c.Items.Add(i);
                }
                this.Add(caption, fieldName, visible, c);
            }
        }

        #endregion

        public void Init()
        {
            if (gridView1.Columns.Count == 0)
            {
                gridView1.PopulateColumns();
            }
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            // 是否可编辑
            this.gridView1.OptionsBehavior.Editable = false;
            // 标题行过滤
            this.gridView1.OptionsCustomization.AllowFilter = false;
            //// 是否允许列排序
            this.gridView1.OptionsCustomization.AllowSort = true;
            // 是否显示列标题右键菜单
            this.gridView1.OptionsMenu.EnableColumnMenu = false;
            // 是否允许单元格选中
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            //去掉表格上方“Drag a column header ……”
            gridView1.OptionsView.ShowGroupPanel = false;
            //不显示最左边一列空白列
            gridView1.OptionsView.ShowIndicator = false;
            //列标题栏【显示】
            gridView1.OptionsView.ShowColumnHeaders = true;
            //设置自动列宽（这样的话表格下方可能会出现滚动条或者未铺满）
            gridView1.OptionsView.ColumnAutoWidth = true;
            //列移动【允许】
            gridView1.OptionsCustomization.AllowColumnMoving = true;
            //改变列宽【允许】
            gridView1.OptionsCustomization.AllowColumnResizing = true;

            gridView1.OptionsSelection.MultiSelect = true; //设置可多选            
            gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.True;
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                //gridView1.se
                //gridView1.GetDataRow()

                DocControlTemplate model = gridView1.GetRow(gridView1.FocusedRowHandle) as DocControlTemplate;
                if (model == null) return;
                txtFont.Text = model.ControlFont;
                txtTemplateName.Text = model.Name;
                ddlControlType.EditValue = model.DocControlType.Id;

            }
        }

        private void SetTextValue(DocControlTemplate model)
        {
            if (model == null)
                model = new DocControlTemplate();
            txtFont.Text = model.ControlFont;
            txtTemplateName.Text = model.Name;
            ddlControlType.EditValue = model.DocControlType.Id;

        }
    }
}