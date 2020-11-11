using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HISPlus;

namespace DXApplication2
{
    /// <summary>
    /// 护理上报
    /// </summary>
    public partial class NurseSubmit : FormDo
    {
        public NurseSubmit()
        {
            InitializeComponent();
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
            try
            {
                webBrowser1.Hide();

                //设置一个对COM可见的对象(上面已将该类设置对COM可见)
                //this.webBrowser1.ObjectForScripting = this;
                //禁止显示拖放到webBrowser上的文件
                this.webBrowser1.AllowWebBrowserDrop = false;
                //禁止使用IE浏览器快捷键
                this.webBrowser1.WebBrowserShortcutsEnabled = false;
                //禁止显示右键菜单
                this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
                //禁止显示脚本错误
                //this.webBrowser1.ScriptErrorsSuppressed = true;

                //webBrowser1.Url = new Uri(@"E:\SVN\mobilenurse\MobileNurse\Html\1.html"); 
                webBrowser1.Url = new Uri(@"http://192.168.1.36:8090/runnurseqcmain/app/nurseqcmain/main.html?page=1");
                webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;
                webBrowser1.Show();
                timer1.Start();
                return;
                // 调用页面的js方法
                //object oSum = webBrowser1.Document.InvokeScript("CreateTable", new object[] { 4, 7 });
                //int sum = Convert.ToInt32(oSum);

                // 

                HtmlDocument htmlDoc = webBrowser1.Document;
                HtmlElement btnElement = htmlDoc.All["btnClose"];
                if (btnElement != null)
                {
                    btnElement.Click += btnElement_Click;
                    // 两者等价
                    //btnElement.AttachEventHandler("onclick", new EventHandler(btnElement2_Click)); 
                }

                // 自动填值并自动提交
                HtmlElement btnSubmit = webBrowser1.Document.All["submitbutton"];
                HtmlElement tbUserid = webBrowser1.Document.All["username"];
                HtmlElement tbPasswd = webBrowser1.Document.All["password"];

                if (tbUserid == null || tbPasswd == null || btnSubmit == null)
                    return;

                tbUserid.SetAttribute("value", "smalldust");
                tbPasswd.SetAttribute("value", "12345678");

                btnSubmit.InvokeMember("click");


                // 这是form的提交方法
                HtmlElement formLogin = webBrowser1.Document.Forms["loginForm"];
                //…… 
                formLogin.InvokeMember("submit");
                //本文之所以没有推荐这种方法，是因为现在的网页，很多都在submit按钮上添加onclick事件，以对提交的内容做最基本的验证。如果直接使用form的submit方法，这些验证代码就得不到执行，有可能会引起错误。
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        void btnElement_Click(object sender, HtmlElementEventArgs e)
        {
            MessageBox.Show("关闭");
        }


        void btnElement2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("关闭");
        }

        private void ucButton1_Click(object sender, EventArgs e)
        {
            // 调用页面的js方法
            object oSum = webBrowser1.Document.InvokeScript("setColor");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            SetBackColor();
        }

        private void SetBackColor()
        {
            //int time = 1;
            //while (webBrowser1.Document == null || time > 100)
            //{
            //    Thread.SpinWait(time * 100);
            //    time++;
            //}
            if (webBrowser1.Document != null)
                webBrowser1.Document.BackColor = this.BackColor;
            webBrowser1.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            webBrowser1.Hide();
            //webBrowser1.Visible = false;
            webBrowser1.Refresh();
            //SetBackColor();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (webBrowser1.Document != null)
                if (webBrowser1.Document.BackColor != this.BackColor)
                {
                    //XtraMessageBox.Show(webBrowser1.Document.BackColor + "---error");
                    webBrowser1.Document.BackColor = this.BackColor;
                    //XtraMessageBox.Show(webBrowser1.Document.BackColor + "---error---this:"+this.BackColor);
                    if (webBrowser1.Document.BackColor != this.BackColor)
                    {

                        //(" $('body').css('background-color','"+s+"')");
                        Color color = this.BackColor;
                        string s = string.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
                        webBrowser1.Document.InvokeScript("setColor", new object[] { s });
                    }
                }
        }
    }
}