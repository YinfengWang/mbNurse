using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HISPlus
{
    public partial class GridAdd : DesignTemplate
    {
        //public GridAdd()
        //{
        //    InitializeComponent();
        //}

        public List<DocTemplateElement> listElements;

        private PickBox pb = new PickBox();

        private void GridAdd_Load(object sender, EventArgs e)
        {
            //设置一个对COM可见的对象(上面已将该类设置对COM可见)
            //this.webBrowser1.ObjectForScripting = this;
            //禁止显示拖放到webBrowser上的文件
            webBrowser1.AllowWebBrowserDrop = false;
            //禁止使用IE浏览器快捷键
            webBrowser1.WebBrowserShortcutsEnabled = false;
            //禁止显示右键菜单
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
            webBrowser1.ScrollBarsEnabled = false;

            //禁止显示脚本错误
            //this.webBrowser1.ScriptErrorsSuppressed = true;

            //webBrowser1.Url = new Uri(@"E:\SVN\mobilenurse\MobileNurse\Html\1.html"); 
            webBrowser1.Url = new Uri(@"D:\文件\635624599784816800.files\sheet002.html");
            //webBrowser1.Url = new Uri("E:\\PICC护理记录表.xls");
            //webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;
            webBrowser1.Show();

            foreach (Control c in this.Controls)
            {
                if (c is WebBrowser)
                    continue;
                pb.WireControl(c);
            }

            if (listElements.Count > 0)
                LayoutForm(listElements);
        }
    }
}