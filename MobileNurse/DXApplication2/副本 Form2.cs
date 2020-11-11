using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using System.Reflection;
using System.IO;
using System.Collections;
using HISPlus;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;

namespace DXApplication2
{
    public partial class Form2 : RibbonForm
    {
        #region 变量

        /// <summary>
        /// 存放权限的nodeid和Button
        /// </summary>
        private readonly Hashtable _rightsControl = new Hashtable(100);                    // 权限控制

        /// <summary>
        /// 菜单数据
        /// </summary>
        readonly DataSet _dsMenu = new DataSet();

        /// <summary>
        /// 菜单按钮索引（用于标明图片位置）
        /// </summary>
        int _buttonImageIndex;

        /// <summary>
        /// 菜单位置
        /// true: 在上边；false：在左边
        /// </summary>
        private bool IsTop = false;

        #endregion

        public Form2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 动态创建功能区页面
        /// </summary>
        /// <param name="parentId">父级节点（顶级节点为0）</param>
        /// <param name="id">当前节点编号</param>
        /// <param name="title">节点标题</param>
        private void CreatePageVertical(string parentId, string id, string title)
        {
            this.mailGroup.Caption = "Mail";
            this.mailGroup.Expanded = true;
            this.mailGroup.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.inboxItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.outboxItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.draftsItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.trashItem)});
            this.mailGroup.LargeImageIndex = 0;
            this.mailGroup.Name = "mailGroup";

            this.navBarControl.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.mailGroup,
            this.organizerGroup});
            
            if (parentId != "0")
                return;
            RibbonPage page = new RibbonPage(title);
            ribbonControl.Pages.Add(page);

            RibbonPageGroup pageGroup = new RibbonPageGroup(title)
            {
                AllowTextClipping = false,
                ShowCaptionButton = false,
                Text = null,
                Name = id
            };
            page.Groups.Add(pageGroup);

        }

        /// <summary>
        /// 动态创建功能区页面
        /// </summary>
        /// <param name="parentId">父级节点（顶级节点为0）</param>
        /// <param name="id">当前节点编号</param>
        /// <param name="title">节点标题</param>
        private void CreatePage(string parentId, string id, string title)
        {

            if (parentId != "0")
                return;
            RibbonPage page = new RibbonPage(title);
            ribbonControl.Pages.Add(page);

            RibbonPageGroup pageGroup = new RibbonPageGroup(title)
            {
                AllowTextClipping = false,
                ShowCaptionButton = false,
                Text = null,
                Name = id
            };
            page.Groups.Add(pageGroup);
        }

        /// <summary>
        /// 动态创建功能区按钮
        /// </summary>
        /// <param name="parentId">父级节点（顶级节点为0）</param>
        /// <param name="nodeId">当前节点编号</param>
        /// <param name="functionName">按钮功能标题</param>
        /// <param name="imageFile">图片路径</param>
        /// <param name="form">按钮绑定窗体</param>
        private void CreateBarButton(string parentId, string nodeId, string functionName, string imageFile, Form form)
        {
            if (parentId == "0")
                return;
            RibbonPageGroup pageGroup = null;
            foreach (RibbonPage tempPage in ribbonControl.Pages)
            {
                foreach (RibbonPageGroup tempPageGroup in
                    tempPage.Groups.Cast<RibbonPageGroup>()
                    .Where(tempPageGroup => tempPageGroup.Name == parentId))
                {
                    pageGroup = tempPageGroup;
                    break;
                }
                if (pageGroup != null)
                    break;
            }
            if (pageGroup == null)
                return;
            // throw new Exception("未找到" + parentId);

            BarButtonItem button = new BarButtonItem { Caption = functionName, Name = functionName };

            if (form != null)
            {
                button.Tag = form;
                button.ItemClick += button_ItemClick;
            }
            ribbonImageCollectionLarge.Images.Add(Image.FromFile(GetIconFile(imageFile)));
            button.Id = _buttonImageIndex;
            button.ImageIndex = _buttonImageIndex;
            button.LargeImageIndex = _buttonImageIndex;
            button.ButtonStyle = BarButtonStyle.Default;
            //button.DropDownControl = this.popupMenu1;                             

            pageGroup.ItemLinks.Add(button);

            // 快速访问工具栏
            //this.ribbonControl.Toolbar.ItemLinks.Add(button);

            _buttonImageIndex++;
            if (!_rightsControl.ContainsKey(nodeId))
                _rightsControl.Add(nodeId, button);
        }

        void button_ItemClick(object sender, ItemClickEventArgs e)
        {
            BarButtonItem button = e.Item as BarButtonItem;

            if (button != null)
            {
                Form form = button.Tag as Form;
                if (form != null)
                    CreateTabPage(form, ribbonImageCollectionLarge.Images[button.LargeImageIndex]);
            }
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
        private void CreateTabPage(Form form, Image image)
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
                //form.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                // form.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                form.FormClosed += TabPage_FormClosed;

                form.TopLevel = false;
                XtraTabPage page = new XtraTabPage
                {
                    Text = form.Text,
                    Image = image,
                    Name = form.GetType() + form.Text
                };
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
        private static void TabPageClose(XtraTabPage tabPage)
        {
            if (tabPage != null)
            {
                tabPage.PageVisible = false;
                // this.xtraTabControl1.TabPages.Remove(tabPage);
                // tabPage.Dispose();
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

        private void Form2_Load(object sender, EventArgs e)
        {
            // 汉化
            // ReSharper disable once ObjectCreationAsStatement
            new ChineseLanguage.Chinese();

            // 快速访问工具栏是否可自定义            
            ribbonControl.Toolbar.ShowCustomizeItem = false;
            ribbonControl.ShowToolbarCustomizeItem = false;
            // 隐藏快速访问工具栏
            ribbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;

            // 最小化功能区
            ribbonControl.Minimized = false;

            //CreateMenu();

            // CreateTabPage(new HISPlus.frmLogin());

            //迭代出所有皮肤样式
            foreach (DevExpress.Skins.SkinContainer skin in DevExpress.Skins.SkinManager.Default.Skins)
            {
                BarButtonItem barBi = new BarButtonItem
                {
                    Tag = skin.SkinName,
                    Name = skin.SkinName,
                    Caption = skin.SkinName
                };
                barBi.ItemClick += ItemClick;
                ribbonPageGroup1.ItemLinks.Add(barBi);
            }

            Login();

            //InitTreeControl();

            //InitGridControl();
            CreateDefaultTabPage(xtraTabPage1, new Form1());
        }

        private DataSet _dsTreeNode;
        private DataSet dsMatch;

        //Create a Banded Grid View including the grindBands and the columns

        /*
                private void ResetTreeNodes()
                {
                    // 清除所有节点
                    treeList1.Nodes.Clear();                               // 清空所有节点

                    // 条件检查
                    if (_dsTreeNode == null || _dsTreeNode.Tables.Count == 0 || _dsTreeNode.Tables[0].Rows.Count == 0)
                    {
                        return;
                    }

                    // 增加根节点
                    DataRow[] rows = _dsTreeNode.Tables[0].Select("PARENT_ID IS NULL OR PARENT_ID = ''", "ITEM_ID ASC");

                    foreach (DataRow dr in rows)
                    {
                        TreeNode nodeNew = new TreeNode(dr["ITEM_NAME"].ToString()) { Tag = dr["ITEM_ID"].ToString() };
                        treeList1.Nodes.Add(nodeNew);

                        // treeList1.Nodes.Add()

                        // 增加子节点
                        AddTreeSubNodes(nodeNew);
                    }
                }
        */

        /*
                private void AddTreeSubNodes(TreeNode parentNode)
                {
                    string parentNodeId = parentNode.Tag.ToString();
                    string filter = "PARENT_ID = " + SqlManager.SqlConvert(parentNodeId);

                    DataRow[] rows = _dsTreeNode.Tables[0].Select(filter, "ITEM_ID ASC");

                    foreach (DataRow dr in rows)
                    {
                        TreeNode nodeNew = new TreeNode(dr["ITEM_NAME"].ToString()) { Tag = dr["ITEM_ID"].ToString() };

                        parentNode.Nodes.Add(nodeNew);

                        // 增加子节点
                        AddTreeSubNodes(nodeNew);
                    }
                }
        */

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

        #region 皮肤单击事件
        private void ItemClick(object sender, ItemClickEventArgs e)
        {

            defaultLookAndFeel1.LookAndFeel.SetSkinStyle(e.Item.Tag.ToString());
            e.Item.Hint = e.Item.Tag.ToString();
        }
        #endregion

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

        private void CreateMenu()
        {
            //rightsControl.Clear();
            //this.ribbonControl.Pages.Clear();

            string mnuFile = GetFileInCurrDir("Menu.xml");
            _dsMenu.ReadXml(mnuFile);
            DataView dv = new DataView { Table = _dsMenu.Tables[0], Sort = "ParentId, SERIAL_NO" };
            _buttonImageIndex = 0;
            foreach (DataRowView drv in dv)
            {
                CreatePage(drv["ParentId"].ToString(), drv["NodeId"].ToString(), drv["NodeName"].ToString());
                CreateBarButton(drv["ParentId"].ToString(), drv["NodeId"].ToString(),
                    drv["NodeName"].ToString(), drv["Icon"].ToString(),
                    GetFormInDll(drv["ADDDLL"].ToString(), drv["ADDRESS"].ToString()));
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
                // return null;
                throw new Exception(("DLL名称: " + dllName + " 类名:" + typeName + " 未找到. ") + ex.Message);
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
    }
}