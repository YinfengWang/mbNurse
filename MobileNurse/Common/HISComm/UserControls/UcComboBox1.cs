using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace HISPlus.UserControls
{
    public partial class UcComboBox1 : UserControl
    {
        public UcComboBox1()
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
                var collection = value as ICollection;
                if (collection != null)
                {
                    // fast
                    comboBoxEdit1.Properties.Items.AddRange(collection);
                }
            }
        }

        public object SelectedValue
        {
            get { return comboBoxEdit1.EditValue; }
            set
            {
                comboBoxEdit1.EditValue = value;
            }
        }

        public int SelectedIndex
        {
            get { return comboBoxEdit1.SelectedIndex; }
            set
            {
                comboBoxEdit1.SelectedIndex = value;
            }
        }

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
            get { return comboBoxEdit1.SelectedText; }
        }


        /// <summary>
        /// 文本值。禁止set！
        /// </summary>
        [ReadOnly(true)]
        public override string Text
        {
            get
            {
                return comboBoxEdit1.Text;
            }
            //private
            set
            {
                //comboBoxEdit1.EditValue = value;

                //if (!string.IsNullOrEmpty(value) && (comboBoxEdit1.Text1 == string.Empty || (comboBoxEdit1.OldEditValue != null && comboBoxEdit1.Text1 == comboBoxEdit1.OldEditValue.ToString())))
                {
                    comboBoxEdit1.Text = value;
                    comboBoxEdit1.RefreshEditValue();                   
                }

                //if (comboBoxEdit1.Properties.IsFilterLookUp)
                //    comboBoxEdit1.Properties.ShowPopupShadow = false;
                //comboBoxEdit1.RefreshEditValue();
                //comboBoxEdit1.Properties.ForceInitialize();
            }
        }

        public int Count
        {
            get { return _listDataSource != null ? _listDataSource.Count : comboBoxEdit1.Properties.LinkCount; }
        }

        public ComboBoxStyle DropDownStyle
        {
            get;
            set;
        }

        /// <summary>
        /// 改写Items属性
        /// </summary>
        public UcComboBox1 Items
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
                comboBoxEdit1.Properties.Columns.Add(col);
                comboBoxEdit1.Properties.DisplayMember = value;
            }
        }

        private string _displayMember;

        private string _valueMember;

        public int MaxDropDownItems { get; set; }

        public int DropDownHeight
        {
            get { return comboBoxEdit1.Properties.DropDownItemHeight; }
            set
            {
                //comboBoxEdit1.Properties.DropDownItemHeight = value;
            }
        }

        public bool IntegralHeight
        {
            get { return comboBoxEdit1.Properties.AutoHeight; }
            set { comboBoxEdit1.Properties.AutoHeight = value; }
        }

        public int DropDownWidth
        {
            get { return comboBoxEdit1.Properties.PopupWidth; }
            set { comboBoxEdit1.Properties.PopupWidth = value; }
        }

        public AutoCompleteSource AutoCompleteSource
        {
            set { }
        }

        public bool DroppedDown
        {
            get { return comboBoxEdit1.IsPopupOpen; }
            set
            {
                if (value)
                    comboBoxEdit1.ShowPopup();
                else
                    comboBoxEdit1.ClosePopup();
            }
        }

        public string ValueMember
        {
            get { return _valueMember; }
            set
            {
                if (value == null) return;
                _valueMember = value;
                comboBoxEdit1.Properties.ValueMember = value;

                LookUpColumnInfo col = new LookUpColumnInfo(value, "Id");
                col.Visible = false;
                comboBoxEdit1.Properties.Columns.Add(col);
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

            comboBoxEdit1.Properties.NullText = string.Empty;
            comboBoxEdit1.Properties.ShowHeader = false;

            comboBoxEdit1.Properties.AllowFocused = true;
            // 自动检索
            comboBoxEdit1.Properties.ImmediatePopup = false;

            comboBoxEdit1.Properties.ShowPopupShadow = true;
            //comboBoxEdit1.Properties.TextEditStyle = TextEditStyles.Standard;

            comboBoxEdit1.Properties.SearchMode = SearchMode.AutoComplete;

            comboBoxEdit1.Properties.AllowNullInput = DefaultBoolean.True;

            // 底部有一个红叉，去掉
            comboBoxEdit1.Properties.ShowFooter = false;

            //comboBoxEdit1.Properties.
            // 选中第一项
            //comboBoxEdit1.ItemIndex = 0;
            //自适应宽度
            //comboBoxEdit1.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
        }

        private void comboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(sender, e);
        }

        public void Clear()
        {
            _listDataSource = null;
            comboBoxEdit1.Properties.DataSource = null;
        }

        public void Add(string value)
        {
            if (_listDataSource == null)
                _listDataSource = new List<string>();
            _listDataSource.Add(value);

            comboBoxEdit1.Properties.DataSource = _listDataSource;
            //comboBoxEdit1.ClosePopup();
        }

        //public void AddRange(IEnumerable<string> values)
        //{
        //    if (listDataSource == null)
        //        listDataSource = new List<string>();
        //    listDataSource.AddRange(values);

        //    comboBoxEdit1.Properties.DataSource = listDataSource;
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

            comboBoxEdit1.Properties.DataSource = _listDataSource;
        }

        public void RemoveAt(int index)
        {
            if (_listDataSource != null)
                _listDataSource.RemoveAt(index);
            else
                XtraMessageBox.Show("没有实现RemoveAt方法");
        }
    }
}
