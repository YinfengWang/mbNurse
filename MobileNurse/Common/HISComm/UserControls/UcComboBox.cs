using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.Utils.Drawing.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace HISPlus.UserControls
{
    public partial class UcComboBox : UserControl
    {
        public UcComboBox()
        {
            InitializeComponent();
        }

        public event EventHandler SelectedIndexChanged;

        private object _dataSource;

        private List<string> _listDataSource;

        public object DataSource
        {
            get { return _dataSource; }
            set
            {
                _dataSource = value;
                lookUpEdit1.Properties.DataSource = value;
            }
        }

        public object SelectedValue
        {
            get { return lookUpEdit1.EditValue; }
            set
            {
                lookUpEdit1.EditValue = value;
            }
        }

        public int SelectedIndex
        {
            get { return lookUpEdit1.ItemIndex; }
            set
            {
                lookUpEdit1.ItemIndex = value;
                if (value == -1)
                    lookUpEdit1.EditValue = null;
            }
        }

        ///// <summary>
        ///// 是否允许录入在数据源没有的数据.即手动录入
        ///// </summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //public bool AllowNullInput
        //{
        //    set
        //    {
        //        if (value)
        //        {
        //            SetNullInput();
        //        }
        //    }
        //}

        /// <summary>
        /// 数据源必须为通过Add或AddRange方法添加
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string this[int index]
        {
            get
            {
                if (_listDataSource != null)
                    return _listDataSource[index];

                return string.Empty;
            }
            set
            {
                if (_listDataSource != null)
                    _listDataSource[index] = value;
            }
        }

        public object SelectedItem
        {
            get { return lookUpEdit1.SelectedText; }
        }


        /// <summary>
        /// 文本值。禁止set！
        /// </summary>
        [ReadOnly(true)]
        public override string Text
        {
            get
            {
                return lookUpEdit1.Text;
            }
            //private
            set
            {
                //lookUpEdit1.EditValue = value;

                //if (!string.IsNullOrEmpty(value) && (lookUpEdit1.Text1 == string.Empty || (lookUpEdit1.OldEditValue != null && lookUpEdit1.Text1 == lookUpEdit1.OldEditValue.ToString())))
                {
                    lookUpEdit1.Text = value;
                    lookUpEdit1.RefreshEditValue();
                    lookUpEdit1.Properties.ForceInitialize();
                }

                //if (lookUpEdit1.Properties.IsFilterLookUp)
                //    lookUpEdit1.Properties.ShowPopupShadow = false;
                //lookUpEdit1.RefreshEditValue();
                //lookUpEdit1.Properties.ForceInitialize();
            }
        }

        public int Count
        {
            get { return _listDataSource != null ? _listDataSource.Count : lookUpEdit1.Properties.LinkCount; }
        }

        public ComboBoxStyle DropDownStyle
        {
            get;
            set;
        }

        /// <summary>
        /// 改写Items属性
        /// </summary>
        public UcComboBox Items
        {
            get { return this; }
        }

        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                if (value == null) return; _displayMember = value;
                LookUpColumnInfo col = new LookUpColumnInfo(value, "Name");
                lookUpEdit1.Properties.Columns.Add(col);
                lookUpEdit1.Properties.DisplayMember = value;
            }
        }

        private string _displayMember;

        private string _valueMember;

        public int MaxDropDownItems { get; set; }

        public int DropDownHeight
        {
            get { return lookUpEdit1.Properties.DropDownItemHeight; }
            set
            {
                //lookUpEdit1.Properties.DropDownItemHeight = value;
            }
        }

        public bool IntegralHeight
        {
            get { return lookUpEdit1.Properties.AutoHeight; }
            set { lookUpEdit1.Properties.AutoHeight = value; }
        }

        public int DropDownWidth
        {
            get { return lookUpEdit1.Properties.PopupWidth; }
            set { lookUpEdit1.Properties.PopupWidth = value; }
        }

        public AutoCompleteSource AutoCompleteSource
        {
            set { }
        }

        public bool DroppedDown
        {
            get { return lookUpEdit1.IsPopupOpen; }
            set
            {
                if (value)
                    lookUpEdit1.ShowPopup();
                else
                    lookUpEdit1.ClosePopup();
            }
        }

        public string ValueMember
        {
            get { return _valueMember; }
            set
            {
                if (value == null) return;
                _valueMember = value;
                lookUpEdit1.Properties.ValueMember = value;

                LookUpColumnInfo col = new LookUpColumnInfo(value, "Id");
                col.Visible = false;
                lookUpEdit1.Properties.Columns.Add(col);
            }
        }

        public bool FormattingEnabled
        {
            get { return _formattingEnabled; }
            set { _formattingEnabled = value; }
        }

        private bool _formattingEnabled = false;

        private void UcComboBox_Load(object sender, EventArgs e)
        {
            //    // 选中第一项
            //    control.ItemIndex = 0;

            //    control.Properties.Columns.Add(new LookUpColumnInfo("NAME", "名称"));
            //    control.Properties.ShowDropDown = ShowDropDown.SingleClick;
            //    control.Properties.ShowHeader = false;
            //    control.Properties.DataSource = Enums.EnumToDicionary(enumType);
            //    control.Properties.ValueMember = "Key";
            //    control.Properties.DisplayMember = "Value";

            lookUpEdit1.Properties.NullText = string.Empty;
            lookUpEdit1.Properties.ShowHeader = false;

            lookUpEdit1.Properties.AllowFocused = true;
            // 自动检索
            lookUpEdit1.Properties.ImmediatePopup = false;

            lookUpEdit1.Properties.ShowPopupShadow = true;

            //lookUpEdit1.Properties.
            lookUpEdit1.Properties.SearchMode = SearchMode.AutoFilter;

            lookUpEdit1.Properties.AllowNullInput = DefaultBoolean.True;

            // 底部有一个红叉，去掉
            lookUpEdit1.Properties.ShowFooter = false;

            //lookUpEdit1.Properties.
            // 选中第一项
            //lookUpEdit1.ItemIndex = 0;
            //自适应宽度
            //lookUpEdit1.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(sender, e);
        }

        public void Clear()
        {
            _listDataSource = null;
            lookUpEdit1.Properties.DataSource = null;
        }

        public void Add(string value)
        {
            if (_listDataSource == null)
                _listDataSource = new List<string>();
            _listDataSource.Add(value);

            lookUpEdit1.Properties.DataSource = _listDataSource;
            //lookUpEdit1.ClosePopup();
        }

        //public void AddRange(IEnumerable<string> values)
        //{
        //    if (listDataSource == null)
        //        listDataSource = new List<string>();
        //    listDataSource.AddRange(values);

        //    lookUpEdit1.Properties.DataSource = listDataSource;
        //}

        public void AddRange(IEnumerable<object> values)
        {
            if (_listDataSource == null)
                _listDataSource = new List<string>();
            foreach (object obj in values)
            {
                if (obj is string)
                    _listDataSource.Add(obj as string);
            }

            lookUpEdit1.Properties.DataSource = _listDataSource;
        }

        public void RemoveAt(int index)
        {
            if (_listDataSource != null)
                _listDataSource.RemoveAt(index);
            else
                XtraMessageBox.Show("没有实现RemoveAt方法");
        }

        //public void SetNullInput()
        //{
        //    lookUpEdit1.Properties.TextEditStyle = TextEditStyles.Standard;
        //    lookUpEdit1.ProcessNewValue += new ProcessNewValueEventHandler(lookUpEdit1_ProcessNewValue);
        //}

        //void lookUpEdit1_ProcessNewValue(object sender, ProcessNewValueEventArgs e)
        //{
        //    try
        //    {
        //        string strName = lookUpEdit1.Text.Trim();
        //        if (!string.IsNullOrEmpty(strName))
        //        {
        //            //if (Utils.IsSqlLanguage(strName))
        //            //{
        //            //    MSG.ShowMsgInfo("输入的病种里含有非法字符！", GlbConstant.Information);
        //            //    return;
        //            //}
        //            DataTable dataTable = _dataSource as DataTable;
        //            if (dataTable == null)
        //            {
        //                dataTable = new DataTable();
        //                dataTable.Columns.Add("Id");
        //                dataTable.Columns.Add("Name");
        //                this.ValueMember = "Id";
        //                this.DisplayMember = "Name";
        //                _dataSource = dataTable;
        //                lookUpEdit1.Properties.DataSource = dataTable;
        //            }
        //            DataRow dr = dataTable.NewRow();
        //            dr[0] = strName;
        //            dr[1] = strName;
        //            dataTable.Rows.Add(dr);
        //            lookUpEdit1.Properties.DropDownRows = dataTable.Rows.Count;
        //            e.Handled = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Error.ErrProc(ex);
        //    }
        //}
    }
}
