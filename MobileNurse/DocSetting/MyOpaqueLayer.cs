using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace HISPlus
{
    /// <summary>
    /// 自定义控件:半透明控件
    /// </summary>
    /* 
     * [ToolboxBitmap(typeof(MyOpaqueLayer))]
     * 用于指定当把你做好的自定义控件添加到工具栏时，工具栏显示的图标。
     * 正确写法应该是
     * [ToolboxBitmap(typeof(XXXXControl),"xxx.bmp")]
     * 其中XXXXControl是你的自定义控件，"xxx.bmp"是你要用的图标名称。
    */
    [ToolboxBitmap(typeof(MyOpaqueLayer))]
    public class MyOpaqueLayer : Control
    {
        private bool _transparentBg = true;//是否使用透明
        private int _alpha = 125;//设置透明度

        private readonly Container _components = new Container();

        public MyOpaqueLayer()
            : this(125)
        {
        }

        public MyOpaqueLayer(int alpha)
        {
            SetStyle(ControlStyles.Opaque, true);
            base.CreateControl();

            this._alpha = alpha;          
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_components != null)
                {
                    _components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 自定义绘制窗体
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Pen labelBorderPen;
            SolidBrush labelBackColorBrush;

            if (_transparentBg)
            {
                //Color drawColor = Color.FromArgb(this._alpha, this.BackColor);
                Color drawColor = Color.FromArgb(this._alpha, Color.Red);
                labelBorderPen = new Pen(drawColor, 0);
                labelBackColorBrush = new SolidBrush(drawColor);
            }
            else
            {
                labelBorderPen = new Pen(this.BackColor, 0);
                labelBackColorBrush = new SolidBrush(this.BackColor);
            }
            base.OnPaint(e);
            float vlblControlWidth = this.Size.Width;
            float vlblControlHeight = this.Size.Height;
            e.Graphics.DrawRectangle(labelBorderPen, 0, 0, vlblControlWidth, vlblControlHeight);
            e.Graphics.FillRectangle(labelBackColorBrush, 0, 0, vlblControlWidth, vlblControlHeight);
        }


        protected override CreateParams CreateParams//v1.10 
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //0x20;  // 开启 WS_EX_TRANSPARENT,使控件支持透明
                return cp;
            }
        }

        /*
         * [Category("myOpaqueLayer"), Description("是否使用透明,默认为True")]
         * 一般用于说明你自定义控件的属性（Property）。
         * Category用于说明该属性属于哪个分类，Description自然就是该属性的含义解释。
         */
        [Category("MyOpaqueLayer"), Description("是否使用透明,默认为True")]
        public bool TransparentBG
        {
            get
            {
                return _transparentBg;
            }
            set
            {
                _transparentBg = value;
                this.Invalidate();
            }
        }

        [Category("MyOpaqueLayer"), Description("设置透明度")]
        public int Alpha
        {
            get
            {
                return _alpha;
            }
            set
            {
                _alpha = value;
                this.Invalidate();
            }
        }
    }


}
