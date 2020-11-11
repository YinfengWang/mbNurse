using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace HISPlus.UserControls
{
    [DefaultEvent("Click")]
    public partial class UcButton : UserControl, IButtonControl
    {
        public UcButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 单击事件
        /// </summary>
        public new event EventHandler Click;
        // Add implementation to the IButtonControl.DialogResult property.
        public DialogResult DialogResult
        {
            get;
            set;
        }

        // Add implementation to the IButtonControl.NotifyDefault method.
        public void NotifyDefault(bool value)
        {
            this.simpleButton1.NotifyDefault(value);            
        }

        // Add implementation to the IButtonControl.PerformClick method.
        public void PerformClick()
        {
            if (this.CanSelect)
            {
                this.simpleButton1.PerformClick();
            }
        }

        public override string Text
        {
            set { this.simpleButton1.Text = value; }
        }

        /// <summary>
        /// 值
        /// </summary>
        [BrowsableAttribute(true)]
        public string TextValue
        {
            get { return this.simpleButton1.Text; }
            set { this.simpleButton1.Text = value; }
        }

        public FlatStyle FlatStyle { get; set; }

        public bool UseVisualStyleBackColor { get; set; }

        public Image Image
        {
            get { return this.simpleButton1.Image; }
            set { this.simpleButton1.Image = value; }
        }

        [Description("图片")]
        [Category("其他")]
        [BrowsableAttribute(true)]
        [TypeConverter(typeof(ImageStyle))]
        public ImageStyle ImageStyle
        {
            get { return this._imageStyle; }
            set
            {
                this._imageStyle = value;
                if (this._imageStyle == ImageStyle.None)
                    return;
                ResourceManager temp = new ResourceManager("HISPlus.Properties.Resources", Assembly.GetExecutingAssembly());

                object obj = temp.GetObject(string.Format("{0}_16x16", value.ToString().ToLower()),
                    System.Globalization.CultureInfo.CurrentCulture) ??
                             temp.GetObject(string.Format("{0}_16x16", value.ToString()),
                        System.Globalization.CultureInfo.CurrentCulture);
                this.simpleButton1.Image = obj as Bitmap;

                SetText();
            }
        }

        private ImageStyle _imageStyle;

        private bool _imageRight = false;

        /// <summary>
        /// 图片在右边
        /// </summary>
        public bool ImageRight
        {
            get { return _imageRight; }
            set
            {
                _imageRight = value;
                if (_imageRight)
                    simpleButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
                else
                    simpleButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Click != null)
                Click(sender, e);
        }

        /// <summary>
        /// 写Text文本
        /// </summary>
        private void SetText()
        {
            //return;
            //if (simpleButton1.Text.Contains("&")) return;

            //switch (_imageStyle)
            //{
            //    case ImageStyle.Add:
            //        simpleButton1.Text += "(&I)";
            //        break;
            //    case ImageStyle.Cancel:
            //        simpleButton1.Text += "(&C)";
            //        break;
            //    case ImageStyle.Save:
            //        simpleButton1.Text += "(&S)";
            //        break;
            //    case ImageStyle.Delete:
            //        simpleButton1.Text += "(&D)";
            //        break;

            //}
        }

        private void UcButton_Load(object sender, EventArgs e)
        {
            SetText();
        }
    }

    public enum ImageStyle
    {
        /// <summary>
        /// 空
        /// </summary>
        None,
        Add,
        Apply,
        Backward,
        Cancel,
        Clear,
        Close,
        Copy,
        Cut,
        Delete,
        Download,
        Edit,
        Find,
        Forward,
        Hide,
        HistoryItem,
        Insert,
        Mail,
        New,
        Next,
        Open,
        Paste,
        Preview,
        Previous,
        Print,
        Properties,
        Refresh,
        Remove,
        Save,
        SelectAll,
        Show,
        WeekView,
        Zoom,
    }
}
