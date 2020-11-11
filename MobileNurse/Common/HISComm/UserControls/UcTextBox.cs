using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HISPlus.UserControls
{
    [DefaultEvent("TextChanged")]
    public partial class UcTextBox : UserControl, IBaseTextBox
    {
        public UcTextBox()
        {
            InitializeComponent();
        }

        //public string EditValue
        //{
        //    get { return _editValue; }
        //    set { _editValue = value; }
        //}

        //private string _editValue;

        public bool Multiline
        {
            get { return !this.textEdit1.Properties.AutoHeight; }
            set { this.textEdit1.Properties.AutoHeight = !value; }
        }

        public int MaxLength
        {
            get { return this.textEdit1.Properties.MaxLength; }
            set { this.textEdit1.Properties.MaxLength = value; }
        }

        public char PasswordChar
        {            
            get { return this.textEdit1.Properties.PasswordChar; }
            set { this.textEdit1.Properties.PasswordChar = value; }
        }

        public bool ReadOnly
        {
            get { return textEdit1.Properties.ReadOnly; }
            set { textEdit1.Properties.ReadOnly = value; }
        }

        [Browsable(true)]
        public override string Text
        {
            get { return this.textEdit1.Text; }
            set { this.textEdit1.Text = value; }
        }

        /// <summary>
        /// 文本变更事件
        /// </summary>
        [Browsable(true)]
        public new event EventHandler TextChanged;

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (TextChanged != null)
                TextChanged(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new bool Validate()
        {
            //textEdit1.DrawBorder(AppDomain.me);
            return false;
        }

        protected override void WndProc(ref  System.Windows.Forms.Message m)
        {
            try
            {
                base.WndProc(ref m);
                //if (m.Msg == WM_PAINT)
                {
                    using (Graphics g = Graphics.FromHwnd(this.Handle))
                    {
                        Rectangle r = new Rectangle();
                        r.Width = this.Width;
                        r.Height = this.Height;
                        textEdit1.DrawBorder(ref m, textEdit1.Width, textEdit1.Height);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

//        #region ITzhtecControl 成员
//        private ButtonBase btn;
//        ////// 获取或设置验证控件的按钮
//        ///[Description("获取或设置验证控件的按钮"), Category("验证"), DefaultValue(true)]
//        public ButtonBase Button
//        {
//            get
//            {
//                return btn;
//            }
//            set
//            {
//                if (!DesignMode && hasCreate)
//                {
//                    if (value == null) btn.RemoveControl(this);
//                    else btn.AddControl(this);
//                }
//                btn = value;
//            }
//        }

//        private string _regExp = string.Empty;
//        ////// 获取或设置用于验证控件值的正则表达式
//        ///[Description("获取或设置用于验证控件值的正则表达式"), Category("验证"),
// DefaultValue("")]
//        public string RegexExpression
//        {
//            get { return _regExp; }
//            set { _regExp = value; }
//        }

//        private bool _allEmpty = false;
//        ////// 获取或设置是否允许空值
//        ///[Description("获取或设置是否允许空值"), Category("验证"), DefaultValue(true)]
//        public bool AllowEmpty
//        {
//            get { return _allEmpty; }
//            set { _allEmpty = value; }
//        }

//        private bool _removeSpace = false;
//        ////// 获取或设置验证的时候是否除去头尾空格
//        ///[Description("获取或设置验证的时候是否除去头尾空格"), Category("验证"),
// DefaultValue(false)]
//        public bool RemoveSpace
//        {
//            get { return _removeSpace; }
//            set { _removeSpace = value; }
//        }

//        private string _empMsg = string.Empty;
//        ////// 获取或设置当控件的值为空的时候显示的信息
//        ///[Description("获取或设置当控件的值为空的时候显示的信息"), Category("验证"),
// DefaultValue("")]
//        public string EmptyMessage
//        {
//            get { return _empMsg; }
//            set { _empMsg = value; }
//        }

//        private string _errMsg = string.Empty;
//        ////// 获取或设置当不满足正则表达式结果的时候显示的错误信息
//        ///[Description("获取或设置当不满足正则表达式结果的时候显示的错误信息"), Category
//("验证"), DefaultValue("")]
//        public string ErrorMessage
//        {
//            get { return _errMsg; }
//            set { _errMsg = value; }
//        }

//        public event CustomerValidatedHandler CustomerValidated;

//        public void SelectAll()
//        {
//            base.SelectAll();
//        }
//        #endregion

//        private bool hasCreate = false;
//        protected override void OnCreateControl()
//        {
//            base.OnCreateControl();
//            if (btn != null) btn.AddControl(this);
//            hasCreate = true;
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (btn != null) btn.RemoveControl(this);
//            base.Dispose(disposing);
//        }
    }

    /// <summary>  
    /// 第二种方法  
    /// </summary>  
    class NewTextBox : DevExpress.XtraEditors.TextEdit
    {
        private Color borderColor = Color.Red;   // 设置默认的边框颜色  
        private static int WM_NCPAINT = 0x0085;    // WM_NCPAINT message  
        private static int WM_ERASEBKGND = 0x0014; // WM_ERASEBKGND message  
        private static int WM_PAINT = 0x000F;      // WM_PAINT message  
        [DllImport("user32.dll")]
        static extern IntPtr GetDCEx(IntPtr hwnd, IntPtr hrgnclip, uint fdwOptions);
        //释放DC  
        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hwnd, IntPtr hDc);
        /// <summary>  
        /// 重绘边框的方法  
        /// </summary>  
        /// <param name="message"></param>  
        /// <param name="width"></param>  
        /// <param name="height"></param>  
        public void DrawBorder(ref System.Windows.Forms.Message message, int width, int height)
        {
            if (message.Msg == WM_NCPAINT || message.Msg == WM_ERASEBKGND ||
                message.Msg == WM_PAINT)
            {
                IntPtr hdc = GetDCEx(message.HWnd, (IntPtr)1, 1 | 0x0020);

                if (hdc != IntPtr.Zero)
                {
                    Graphics graphics = Graphics.FromHdc(hdc);
                    Rectangle rectangle = new Rectangle(0, 0, width, height);
                    ControlPaint.DrawBorder(graphics, rectangle,
                                 borderColor, ButtonBorderStyle.Solid);

                    message.Result = (IntPtr)1;
                    ReleaseDC(message.HWnd, hdc);
                }
            }
        }
    }
}
