using System.Drawing.Drawing2D;
using DevExpress.XtraEditors;

namespace HISPlus
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// 复选框
    /// </summary>
    internal sealed class GridNode1 : BaseNode
    {
        /// <summary>
        /// 后缀(Label)
        /// </summary>
        public readonly WebBrowser _webBrowser;

        public GridNode1(DesignTemplate container, DocTemplateElement nursingDocNode)
            : base(container, nursingDocNode)
        {
            base.HasValue = false;
            _webBrowser = new WebBrowser();

            _webBrowser.SendToBack();

            if (ControlWidth > 0)
            {
                _webBrowser.Width = ControlWidth;
            }
            if (ControlHeight > 0)
            {
                _webBrowser.Height = ControlHeight;
            }

            //webBrowser1.Width = 800;
            //webBrowser1.Height = 800;

            //设置一个对COM可见的对象(上面已将该类设置对COM可见)
            //this.webBrowser1.ObjectForScripting = this;
            //禁止显示拖放到webBrowser上的文件
            _webBrowser.AllowWebBrowserDrop = false;
            //禁止使用IE浏览器快捷键
            _webBrowser.WebBrowserShortcutsEnabled = false;
            //禁止显示右键菜单
            _webBrowser.IsWebBrowserContextMenuEnabled = false;
            _webBrowser.ScrollBarsEnabled = false;

            //禁止显示脚本错误
            //this.webBrowser1.ScriptErrorsSuppressed = true;

            //webBrowser1.Url = new Uri(@"E:\SVN\mobilenurse\MobileNurse\Html\1.html"); 
            _webBrowser.Url = new Uri(@"D:\文件\635624599784816800.files\sheet002.html");
            //webBrowser1.Url = new Uri("E:\\PICC护理记录表.xls");
            _webBrowser.DocumentCompleted += _webBrowser_DocumentCompleted;
            _webBrowser.Show();

            //_checkEdit.ClientRectangle.Width
            ControlList.Add(this._webBrowser);

        }

        void _webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //MessageBox.Show(_ctl.Document.Body.ScrollRectangle.Width.ToString());
            //_ctl.Width = _ctl.Document.Body.OffsetRectangle.Width;
            //_ctl.Height = _ctl.Document.Body.ScrollRectangle.Height;
        }

        protected override void LayoutBeforeChildNodes(ref Point location)
        {
            LayoutPrefix(ref location);
            Container.AddControl(this._webBrowser, new Point(location.X, location.Y - 6 + (int)NursingDocNode.RowSpacing));
            location.X += this._webBrowser.Width;
        }

        public override string SelfFormattedValue
        {
            get
            {
                return string.Empty;
            }
        }

        public override object Value
        {
            get
            {
                return null;
            }
            set
            {
            }
        }
    }
}

