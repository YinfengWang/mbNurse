using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraNavBar;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraBars;
using System.Reflection;
using System.IO;
using System.Collections;
using DevExpress.XtraBars.Helpers;
using TJ.CHSIS;

namespace HISPlus
{
    public partial class MDIFrm1 : FormDo
    {
        #region 变量

        /// <summary>
        /// 存放权限的nodeid和Button
        /// </summary>
        private readonly Hashtable _rightsControl = new Hashtable(100);                    // 权限控制

        private TabControlTJ _tabControlWindowList = new TabControlTJ();
        private readonly Hashtable frmsBackground = new Hashtable(3);                     // 后台窗体列表
        private readonly ArrayList frmsName = new ArrayList(3);                     // 后台窗体名列表

        private string _hideNavigator = string.Empty;                       // 是否隐藏主窗体的导航栏

        private const string BACKGROUND_FRM = "background";

        private string _defaultWndDllName = string.Empty;                       // 默认打开窗体
        private string _defaultWndName = string.Empty;                       // 默认打开窗体
        private ResourceManager rm = null;                                 // 资源管理器

        /// <summary>
        /// 菜单数据
        /// </summary>
        readonly DataSet _dsMenu = new DataSet();

        MenuStrip menuStrip_Function = new MenuStrip();

        private string wardCodes = string.Empty;

        private const string DllName = "OrdersSplit1.dll";
        private const string FormName = "HISPlus.frmOrderConvertFailed";

        #endregion

        public MDIFrm1()
        {
            InitializeComponent();
            SkinHelper.InitSkinGallery(rgbiSkins);
            defaultLookAndFeel1.LookAndFeel.SetSkinStyle("Office 2007 Green");

            this.FormClosing += MDIMainForm_FormClosing;
            this.FormClosed += MDIMainForm_FormClosed;

            _id = "0007";
            _version = "1.0";
            _guid = "5cd33f94-e88d-4053-abef-720ae07f1bf3";
            _right = string.Empty;
        }

        /// <summary>
        /// 根据Form找选项卡
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        private XtraTabPage GetTabPageByForm(Form form)
        {
            return form == null ? null : xtraTabControl1.TabPages.FirstOrDefault(p => p.Name == form.GetType() + form.Text);
        }

        private void CreateDefaultTabPage(XtraTabPage page, Form form)
        {
            if (form == null) return;

            XtraTabPage xtp = GetTabPageByForm(form);

            if (xtp != null)
            {
                xtp.PageVisible = true;
                xtp.Show();
                return;
            }
            try
            {
                if (form.IsDisposed)
                {
                    form = Activator.CreateInstance(form.GetType()) as Form;
                }
                if (form == null) return;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;

                //form.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;                
                // form.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                form.FormClosed += TabPage_FormClosed;

                form.TopLevel = false;
                page.Text = form.Text;
                page.Name = form.GetType() + form.Text;
                xtraTabControl1.TabPages.Add(page);
                page.Controls.Add(form);
                form.Show();
                page.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 创建选项卡页
        /// </summary>
        /// <param name="form"></param>
        /// <param name="image"></param>
        private void CreateTabPage(Form form, string tag, Image image)
        {
            if (form == null) return;

            XtraTabPage xtp = GetTabPageByForm(form);

            if (xtp != null)
            {
                xtp.PageVisible = true;
                xtp.Show();
                return;
            }

            try
            {
                if (form.IsDisposed)
                {
                    form = Activator.CreateInstance(form.GetType()) as Form;
                }
                if (form == null) return;
                form.FormBorderStyle = FormBorderStyle.None;
                form.TopLevel = false;
                form.Dock = DockStyle.Fill;
                form.AutoScroll = true;
                form.Visible = true;
                form.Tag = tag;

                //form.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;                                             
                // form.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                form.FormClosed += TabPage_FormClosed;

                XtraTabPage page = new XtraTabPage
                {
                    Text = form.Text,
                    Image = image,
                    Name = form.GetType() + form.Text
                };

                //this.xtraScrollableControl1.Controls.Add(page);
                xtraTabControl1.TabPages.Add(page);
                page.AutoScroll = true;

                //PanelControl panel = new PanelControl();                
                //panel.Dock = DockStyle.Fill;
                //panel.Location = new System.Drawing.Point(0, 0);
                ////panel.Size = new System.Drawing.Size(1093, 435);
                //panel.TabIndex = 999;
                //panel.Controls.Add(form);
                //page.Controls.Add(panel);

                page.Controls.Add(form);
                form.Show();
                page.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 关闭选项卡页面
        /// </summary>
        /// <param name="form"></param>
        private void TabPageClose(Form form)
        {
            if (form == null) return;
            if (form.IsDisposed) return;
            XtraTabPage tabPage = GetTabPageByForm(form);

            TabPageClose(tabPage);
        }

        /// <summary>
        /// 关闭选项卡页面
        /// </summary>
        /// <param name="tabPage"></param>
        private void TabPageClose(XtraTabPage tabPage)
        {
            if (tabPage != null)
            {
                tabPage.PageVisible = false;
                xtraTabControl1.TabPages.Remove(tabPage);
                tabPage.Dispose();
            }
        }

        /// <summary>
        /// 选项卡关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TabPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            TabPageClose(sender as Form);
        }

        private void xtraTabControl1_DoubleClick(object sender, EventArgs e)
        {
            XtraTabHitInfo tabHitInfo =
                ((XtraTabControl)sender).CalcHitInfo(((XtraTabControl)sender).PointToClient(MousePosition));

            TabPageClose(tabHitInfo.Page);
        }

        private void MdiFrm_Load(object sender, EventArgs e)
        {
            try
            {
                //// 快速访问工具栏是否可自定义            
                //ribbonControl.Toolbar.ShowCustomizeItem = false;
                ////ribbonControl.ShowToolbarCustomizeItem = false;
                //// 隐藏快速访问工具栏
                //ribbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;

                //// 最小化功能区
                //ribbonControl.Minimized = true;

                // Login();
                // 加载菜单
                CreateMenu();

                //Init();
                //分析病区代码是否有重复，如果有重复抛出异常
                isRightWardCodes();
                initFrmVal();
                initDisp();

                startDefaultWnd();

                createlblMenuFromMenu();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 打开默认窗体
        /// </summary>
        private void startDefaultWnd()
        {
            // 条件判断
            if (_dsMenu == null || _defaultWndName.Length == 0)
            {
                return;
            }
            return;
            // 查找节点ID
            string filter = "Address = " + SqlManager.SqlConvert(_defaultWndName)
                            + "AND ADDDLL = " + SqlManager.SqlConvert(_defaultWndDllName);

            DataRow[] drFind = _dsMenu.Tables[0].Select(filter);
            if (drFind.Length == 0)
            {
                return;
            }

            string nodeId = drFind[0]["NodeId"].ToString();

            // 查找菜单 并模拟点击

            ToolStripMenuItem mnu = (ToolStripMenuItem)_rightsControl[nodeId];
            if (mnu.Enabled == true)
            {
                Menu_Click(mnu, null);
            }
        }

        private void isRightWardCodes()
        {
            string[] wardCodesShuZu = wardCodes.Split(',');
            for (int i = 0; i < wardCodesShuZu.Length; i++)
            {
                for (int j = i + 1; j < wardCodesShuZu.Length; j++)
                {
                    if (wardCodesShuZu[i] == wardCodesShuZu[j])
                    {
                        throw new Exception("list.ini文件中存在重复的病区代码:" + wardCodesShuZu[i]);
                    }
                }
            }
        }

        /// <summary>
        /// 初始化窗体变量
        /// </summary>
        private void initFrmVal()
        {
            if (this.Tag != null)
            {
                string[] parts = this.Tag.ToString().Split(ComConst.STR.COMMA.ToCharArray());
                if (parts.Length == 3)
                {
                    _hideNavigator = parts[0].Trim();
                    _defaultWndName = parts[1].Trim();
                    _defaultWndDllName = parts[2].Trim();
                }
            }

            rm = new ResourceManager("TJ.CHSIS.ResPic", Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// 初始化窗体显示
        /// </summary>
        private void initDisp()
        {
            this.Text = GVars.App.Title;

            lblUser.Text = GVars.User.Name;
            lblDeptName.Text = GVars.User.DeptName;
            lblDate.Text = DateTime.Now.ToString(ComConst.FMT_DATE.LONG_MINUTE);

            // 定位
            //lblDeptName.Left = lblTitleDate.Left - lblDeptName.Width - 20;
            //lblTitleDept.Left = lblDeptName.Left - lblTitleDept.Width;

            //lblUser.Left = lblTitleDept.Left - lblUser.Width - 20;
            //lblTitleUser0.Left = lblUser.Left - lblTitleUser0.Width;

            // 窗体最大化
            if (GVars.App.MaxMdiFrm)
            {
                this.WindowState = FormWindowState.Maximized;
            }


            menuStrip_Function.Visible = false;

            // 导航栏

            if (_hideNavigator.Equals("1") == true)
            {
                //lblAction_Click(null, null);
                navBarControl1.Show();
            }
        }


        private void Login()
        {
            // 获取登录窗体
            string appConfigFile = "Application.xml";
            appConfigFile = Path.Combine(Application.StartupPath, appConfigFile);
            if (File.Exists(appConfigFile))
            {
                DataSet dsConfig = new DataSet();
                dsConfig.ReadXml(appConfigFile, XmlReadMode.ReadSchema);

                if (dsConfig.Tables.Count > 0)
                {
                    DataRow[] drFind = dsConfig.Tables[0].Select("PropertyName = 'LOGIN'");
                    if (drFind.Length > 0)
                    {
                        string loginFrmName = drFind[0]["PropertyValue"].ToString();
                        string loginDllName = drFind[0]["Comment"].ToString();

                        // 显示登录窗体
                        Form loginFrm = DllOperator.GetFormInDll(loginDllName, loginFrmName);
                        if (loginFrm != null)
                        {
                            GVars.App.Verified = false;
                            string userName = GVars.User.Name;

                            GVars.User.UserName = string.Empty;
                            GVars.User.PWD = string.Empty;

                            loginFrm.Text = @"请重新登录";
                            loginFrm.ShowDialog();

                            if (GVars.App.Verified == false)
                            {
                                Close();
                            }
                            else
                            {
                                if (userName == null || userName.Equals(GVars.User.Name) == false)
                                {
                                    // 关闭所有Tab页
                                    //lblCloseWindows_Click(null, null);

                                    // 重新加载菜单
                                    CreateMenu();
                                    //initFrmVal();
                                    //initDisp();
                                    Init();
                                }
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 个性化界面(主要指界面)
        /// </summary>
        private void Init()
        {
            foreach (DataRow dr in _dsMenu.Tables[0].Rows)
            {
                try
                {
                    ((BarButtonItem)_rightsControl[dr["NodeId"].ToString()]).Enabled = false;
                }
                catch
                {
                    //throw new Exception("Menu.xml中 节点:" + dr["NodeId"] + "没有找到!");
                }
            }

            //string filter = "RIGHT_ID = '{0}'";
            const string filter = "MODULE_CODE = '{0}'";
            foreach (DataRow dr in _dsMenu.Tables[0].Rows)
            {
                string nodeId = dr["NodeId"].ToString();

                DataRow[] drFind = GVars.User.Rights.Tables[0].Select(string.Format(filter, nodeId));

                if (drFind.Length > 0 && _rightsControl.Contains(nodeId))
                {
                    ((BarButtonItem)_rightsControl[nodeId]).Enabled = true;

                    // 查找父节点
                    drFind = _dsMenu.Tables[0].Select("NodeId = " + SqlManager.SqlConvert(nodeId));

                    if (drFind.Length > 0)
                    {
                        nodeId = drFind[0]["ParentId"].ToString();
                        if (nodeId.Equals("0")) continue;
                        if (_rightsControl[nodeId] != null)
                        {
                            ((BarButtonItem)_rightsControl[nodeId]).Enabled = true;

                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取DLL路径
        /// </summary>
        /// <returns></returns>
        private string getDllPath()
        {
            string dllFile = Assembly.GetExecutingAssembly().CodeBase;
            int length = dllFile.LastIndexOf("/", StringComparison.Ordinal);
            return dllFile.Substring(8, length - 8);
        }

        /// <summary>
        /// 获取文件完整路径
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetFileInCurrDir(string fileName)
        {
            fileName = Path.Combine(getDllPath(), fileName);
            return fileName;
        }

        /// <summary>
        /// 获取当前dll所在目录的文件名
        /// </summary>
        /// <returns></returns>
        private string getFileInCurrDir(string fileName)
        {
            fileName = Path.Combine(getDllPath(), fileName);

            return fileName;
        }

        /// <summary>
        /// 从Menu菜单中创建OutlookBar的主菜单按钮
        /// </summary>        
        private void createlblMenuFromMenu()
        {
            if (menuStrip_Function.Items.Count > 0)
            {
                ToolStripItem mnuItem = menuStrip_Function.Items[0];
                navBarGroup1.Caption = mnuItem.Text;
            }

            navBarGroup1.GroupStyle = NavBarGroupStyle.LargeIconsText;
            navBarGroup1.GroupCaptionUseImage = DevExpress.XtraNavBar.NavBarImage.Large;
        }


        //private void createlblSubMenuFromMenu(ref XPanderPanel xpPanel, ref ToolStripItem mnuItem)
        //{
        //    // 创建一个包含的Panel
        //    xpPanel.BackColor = Color.FromArgb(221, 239, 249);

        //    // 创建ListView
        //    ListView lvwMenu = new ListView();
        //    xpPanel.Controls.Add(lvwMenu);

        //    lvwMenu.BackColor = Color.FromArgb(242, 247, 253);
        //    lvwMenu.BorderStyle = BorderStyle.None;
        //    lvwMenu.FullRowSelect = true;
        //    lvwMenu.HideSelection = true;
        //    lvwMenu.LabelWrap = false;
        //    lvwMenu.LargeImageList = this.imageList1;
        //    lvwMenu.SmallImageList = this.imageList1;
        //    lvwMenu.UseCompatibleStateImageBehavior = false;
        //    lvwMenu.Click += new EventHandler(lblSubMenu_Click);

        //    // 加载ListViewItem
        //    lvwMenu.AutoArrange = true;
        //    lvwMenu.Width = 96;

        //    ToolStripMenuItem mnuParent = (ToolStripMenuItem)mnuItem;
        //    ListViewItem item = null;

        //    for (int i = 0; i < mnuParent.DropDownItems.Count; i++)
        //    {
        //        ToolStripItem mnuSubItem = mnuParent.DropDownItems[i];

        //        if (mnuSubItem.Enabled == false)
        //        {
        //            continue;
        //        }

        //        if (mnuSubItem.Image != null)
        //        {

        //            item = lvwMenu.Items.Add(mnuSubItem.Text, imageList1.Images.Count - 1);
        //        }
        //        else
        //        {
        //            item = lvwMenu.Items.Add(mnuSubItem.Text);
        //        }

        //        item.ForeColor = Color.Blue;
        //        item.Tag = mnuSubItem;
        //    }

        //    lvwMenu.AutoArrange = false;
        //    lvwMenu.Location = new Point(6, xpPanel.CaptionHeight);
        //    lvwMenu.Width = xpPanel.Width - 12;
        //    lvwMenu.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
        //}      

        void item_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                NavBarItem lvwMenu = sender as NavBarItem;

                if (lvwMenu != null)
                {
                    ToolStripItem mnuItem = (ToolStripItem)(lvwMenu.Tag);
                    mnuItem.PerformClick();
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void CreateMenu()
        {
            // 加载 菜单设置            
            string mnuFile = getFileInCurrDir("Menu.xml");
            _dsMenu.Clear();
            _rightsControl.Clear();
            navBarControl1.Items.Clear();
            navBarControl1.Groups.Clear();

            _dsMenu.ReadXml(mnuFile);

            // 加载 菜单
            DataView dv = new DataView();
            dv.Table = _dsMenu.Tables[0];
            dv.Sort = "ParentId, SERIAL_NO";
            dv.RowFilter = "ParentId = '0'";                            // 主菜单

            foreach (DataRowView drv in dv)
            {
                if (drv["Enabled"] != DBNull.Value && drv["Enabled"].ToString() == "True")
                CreateMenuItem(drv["NodeName"].ToString(), 
                           drv["NodeId"].ToString(), drv["Icon"].ToString());
            }
        }


        /// <summary>
        /// 获取图标文件的绝对路径
        /// </summary>
        /// <param name="iconFile"></param>
        /// <returns></returns>
        private string getIconFile(string iconFile)
        {
            int pos = iconFile.LastIndexOf(Path.DirectorySeparatorChar);
            if (pos > 0)
            {
                iconFile = iconFile.Substring(pos + 1, iconFile.Length - pos - 1);
            }

            string filePath = Path.Combine(getDllPath(), "Resource");
            filePath = Path.Combine(filePath, "ICON");
            iconFile = Path.Combine(filePath, iconFile);

            return iconFile;
        }

        public string GetFilter(string fileName)
        {
            string strResult = string.Empty;
            // 获取文件内容
            StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("gb2312"));
            string content = sr.ReadToEnd();
            sr.Close();

            content = content.Replace("\n", ComConst.STR.BLANK);

            // 解析文件
            string[] parts = content.Split('\r');
            string line = string.Empty;
            for (int i = 0; i < parts.Length; i++)
            {
                line = parts[i].Trim();

                // 注释
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    continue;
                }

                // src:  
                if (line.StartsWith("src:") == true)
                {
                    continue;
                }

                // dest:
                if (line.StartsWith("dest:") == true)
                {
                    continue;
                }
                //filter:
                if (line.StartsWith("filter:") == true)
                {
                    strResult = line.Substring(7).Trim();
                    continue;
                }
            }
            return strResult;
        }

        /// <summary>
        /// 创建菜单项
        /// </summary>
        /// <param name="drv"></param>
        /// <param name="ParentId"></param>
        private void CreateMenuItem(string mnuName, string nodeId, string iconFile)
        {
            ToolStripMenuItem topMenu = new ToolStripMenuItem(mnuName, null, null, nodeId);
            topMenu.Tag = nodeId;

            if (iconFile.Length > 0)
            {
                iconFile = getIconFile(iconFile);

                if (File.Exists(iconFile) == true)
                {
                    topMenu.Image = Image.FromFile(iconFile);
                }

                ((ToolStripDropDownMenu)(topMenu.DropDown)).ShowImageMargin = true;
            }
            else
            {
                topMenu.Image = null;
                ((ToolStripDropDownMenu)(topMenu.DropDown)).ShowImageMargin = false;
            }

            menuStrip_Function.Items.Add(topMenu);
            _rightsControl.Add(nodeId, topMenu);

            // 查找子菜单

            DataView dv = new DataView();
            dv.Table = _dsMenu.Tables[0];

            dv.Sort = "ParentId, SERIAL_NO";
            dv.RowFilter = "ParentId = " + SqlManager.SqlConvert(nodeId);

            //父级菜单
            var nbg = new NavBarGroup
            {
                Caption = mnuName,
                Name = nodeId,
                Expanded = true,
                GroupStyle = NavBarGroupStyle.LargeIconsText,
            };


            // 按钮图片                       
            iconFile = getIconFile(iconFile);

            if (File.Exists(iconFile) == true)
            {
                nbg.LargeImage = Image.FromFile(iconFile);
            }
            nbg.GroupCaptionUseImage = NavBarImage.Large;
            navBarControl1.Groups.Add(nbg);

            foreach (DataRowView drv in dv)
            {
                //LB20110712,根据sql文件夹下面配置的list.ini的名称来判断是否显示该菜单
                string Num = drv["NodeName"].ToString().Substring(drv["NodeName"].ToString().Length - 1);
                int i = -1;
                if (int.TryParse(Num, out i))//是数字的话
                {
                    // 判断文件是否存在，以确定该菜单是否显示
                    string fileName = Path.Combine(Application.StartupPath, drv["IniPath"].ToString());

                    // 判断文件是否存在，以确定该菜单是否显示
                    //string fileName = Path.Combine(Path.Combine(Application.StartupPath, "SQL"), "List" + i.ToString() + ".INI");
                    if (File.Exists(fileName) == false)
                    {
                        continue;//不创建该菜单
                    }
                    else//分析科室条件中，科室是否重复，如果重复将会导致程序运行错误
                    {
                        string result = GetFilter(fileName);
                        if (result.Trim().Length == 0)
                        {
                            throw new Exception("sql文件夹下" + "List" + i.ToString() + ".INI中filter病区条件为空！");
                        }
                        else
                        {
                            //防止加载两次，比如同步1和拆分1，只累加一次
                            if (wardCodes.IndexOf(result) < 0)
                            {
                                wardCodes += result;
                                wardCodes += ",";
                            }
                        }
                    }
                }
                //LB20110712添加结束

                ToolStripMenuItem subMenu = new ToolStripMenuItem(drv["NodeName"].ToString(), null,
                    new EventHandler(Menu_Click));

                ((ToolStripDropDownMenu)(subMenu.DropDown)).ShowCheckMargin = true;

                iconFile = drv["Icon"].ToString();
                if (iconFile.Length > 0)
                {
                    iconFile = getIconFile(iconFile);

                    if (File.Exists(iconFile) == true)
                    {
                        subMenu.Image = Image.FromFile(iconFile);
                    }

                    ((ToolStripDropDownMenu)(subMenu.DropDown)).ShowImageMargin = true;
                }
                else
                {
                    subMenu.Image = null;
                    ((ToolStripDropDownMenu)(subMenu.DropDown)).ShowImageMargin = false;
                }

                subMenu.Tag = drv["NodeId"].ToString();

                topMenu.DropDownItems.Add(subMenu);
                _rightsControl.Add(subMenu.Tag.ToString(), subMenu);

                CreateItem(nbg, subMenu.Text, subMenu.Image, subMenu, drv["MEMO"].ToString());

                // 如果是后台窗体
                if (drv["Background"].ToString().Equals("1") == true)
                {
                    startWindowBackground(subMenu);
                }
            }
        }

        /// <summary>
        /// 添加左侧列表
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="image"></param>
        /// <param name="tag"></param>
        /// <param name="hint">鼠标经过的提示文本</param>
        private void CreateItem(NavBarGroup group, string caption, Image image, ToolStripMenuItem tag, string hint)
        {
            NavBarItem item = new NavBarItem { Caption = caption, LargeImage = image, SmallImage = image, Tag = tag };
            //item
            item.LinkClicked += item_LinkClicked;
            item.Hint = string.IsNullOrEmpty(hint) ? caption : hint;

            navBarControl1.Items.Add(item);
            group.ItemLinks.Add(item);
        }

        /// <summary>
        /// 菜单 处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem mnuItem = ((ToolStripMenuItem)sender);

            string nodeId = mnuItem.Tag.ToString();

            DataView dv = new DataView();
            dv.Table = _dsMenu.Tables[0];
            dv.RowFilter = "NodeId= " + SqlManager.SqlConvert(nodeId);

            try
            {
                // 条件检查

                if (dv.Count == 0)
                {
                    return;
                }

                DataRowView drv = dv[0];

                string dllName = GetFileInCurrDir(DllName);
                string frmName = FormName;
                string parameter = string.Empty;

                if (dv.Table.Columns.Contains("PARAMETER") == true)
                {
                    parameter = drv["PARAMETER"].ToString();
                }

                if (dllName.Length == 0 || frmName.Length == 0)
                {
                    return;
                }

                // 获取窗体
                Form frmShow = DllOperator.GetFormInDll(dllName, frmName, drv["IniPath"].ToString());
                //frmShow.BackColor = Color.FromArgb(244, 249, 253);
                frmShow.Tag = parameter;

                //if (frmShow is FormDo)
                //{
                //    FormDo frmShowDo = (FormDo)frmShow;
                    //frmShowDo.SetWinBackColor();

                    //// 检查ID号是否相同

                    //if (nodeId.Equals(frmShowDo.ID) == false)
                    //{
                    //    GVars.Msg.MsgId= "E0010";                       // 窗体ID为: {0}, Menu.xml文件中定义为 {1}, 请修改Menu.xml文件!
                    //    GVars.Msg.MsgContent.Add(frmShowDo.ID);
                    //    GVars.Msg.MsgContent.Add(nodeId);
                    //    GVars.Msg.Show();
                    //    return;
                    //}
                //}

                frmShow.Text = mnuItem.Text;

                switch (drv["WIN_OPEN_MODE"].ToString())
                {
                    case "1":
                        frmShow.ShowDialog();
                        break;
                    case "2":
                        frmShow.Show();
                        break;
                    default:
                        if (frmsBackground.Contains(frmName) == true)
                        {
                            frmShow = (Form)(frmsBackground[frmName]);

                            openWindowInPanels(ref frmShow, BACKGROUND_FRM, mnuItem.Image);
                            CreateTabPage(frmShow, BACKGROUND_FRM, mnuItem.Image);
                        }
                        else
                        {
                            CreateTabPage(frmShow, string.Empty, mnuItem.Image);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 在Panels中打开窗口
        /// </summary>
        private void openWindowInPanels(ref Form frm, string tag, Image iconImage)
        {
            CreateTabPage(frm, tag, iconImage);
        }

        /// <summary>
        /// 后台启动窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startWindowBackground(ToolStripMenuItem mnuItem)
        {
            DataView dv = new DataView();
            dv.Table = _dsMenu.Tables[0];
            dv.RowFilter = "NodeId= " + SqlManager.SqlConvert(mnuItem.Tag.ToString());

            try
            {
                if (dv.Count > 0)
                {
                    DataRowView drv = dv[0];

                    string dllName = DllName;
                    string frmName = FormName;

                    if (dllName.Length > 0 && frmName.Length > 0)
                    {
                        if (frmsBackground.Contains(frmName) == false)
                        {
                            Form frmShow = DllOperator.GetFormInDll(dllName, frmName);

                            frmShow.Text = mnuItem.Text;

                            // frmShow.Visible = false;
                            frmShow.Show();
                            frmShow.Visible = false;

                            // 保存
                            frmsBackground.Add(frmName, frmShow);
                            frmsName.Add(frmName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private static Form GetFormInDll(string dllName, string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
                return null;
            Form form;
            try
            {
                Form genericInstance = Activator.CreateInstance(Assembly.LoadFrom(dllName).GetType(typeName)) as Form;
                form = genericInstance;
            }
            catch (Exception ex)
            {
                // throw new Exception(("DLL名称: " + dllName + " 类名:" + typeName + " 未找到. ") + ex.Message);
                return null;

            }
            return form;
        }

        private string GetIconFile(string iconFile)
        {
            int pos = iconFile.LastIndexOf(Path.DirectorySeparatorChar);
            if (pos > 0)
            {
                iconFile = iconFile.Substring(pos + 1, (iconFile.Length - pos) - 1);
            }
            iconFile = Path.Combine(Path.Combine(Path.Combine(getDllPath(), "Resource"), "ICON"), iconFile);
            return iconFile;
        }

        private void iNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            Login();
        }

        private void iExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Application.ExitThread();
        }

        private void xtraTabControl1_CloseButtonClick_1(object sender, EventArgs e)
        {
            ClosePageButtonEventArgs a = (ClosePageButtonEventArgs)e;

            TabPageClose((XtraTabPage)a.Page);
        }

        private void lblCloseWindows_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                //foreach (XtraTabPage page in xtraTabControl1.TabPages)
                //    TabPageClose(page);

                for (int i = xtraTabControl1.TabPages.Count-1; i >= 0; i--)
                {
                    TabPageClose(xtraTabControl1.TabPages[i]);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void lblHelp_Click(object sender, EventArgs e)
        {

        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        /// 窗体关闭通知事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MDIMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (GVars.App.QuestionExit == true)
                {
                    if (GVars.Msg.Show("Q0003") == DialogResult.No)    // 您确认要退出本系统吗? 
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        e.Cancel = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MDIMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //for (int i = 0; i < frmsName.Count; i++)
                //{
                //    Form frm = (Form)(frmsBackground[(String)frmsName[i]]);
                //    frm.Close();
                //}

                for (int i = frmsName.Count - 1; i >= 0; i--)
                {
                    Form frm = frmsBackground[(String)frmsName[i]] as Form;

                    if (frm == null) continue;
                    frm.Close();
                }

                // 激活门户程序

                //GVars.App.Show();
                // activateSelectApp();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void lblSetting_Click(object sender, EventArgs e)
        {
            ModuleManagerFrm frm = new ModuleManagerFrm();
            if (frm.ShowDialog() != DialogResult.Cancel)
                CreateMenu();
        }        
    }
}