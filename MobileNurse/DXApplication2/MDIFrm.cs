using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AutoUpdate.AuotUpdate;
using CommonEntity;
using DevExpress.Skins;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.XtraNavBar.ViewInfo;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

using HISPlus;
using IniFile = AutoUpdate.IniFile;

namespace DXApplication2
{
    public partial class MdiFrm : FormDo
    {
        #region 变量

        /// <summary>
        ///     左侧导航菜单显示的是大图还是小图
        /// </summary>
        private const bool IsLargeImage = true;

        // 自动更新有关
        private bool _exit;
        private bool _isRestart;
        private string _updateExe = string.Empty;
        private string _updFlagFile = string.Empty;

        /// <summary>
        /// 当前系统时间
        /// </summary>
        private DateTime _currentTime;

        /// <summary>
        ///     页面集合
        /// </summary>
        private readonly Dictionary<string, XtraTabPage> _dictionaryPages = new Dictionary<string, XtraTabPage>();

        private readonly Form _homePage;

        /// <summary>
        ///     菜单集合
        /// </summary>
        private IList<MenuManager> _listMenu;

        private PatientListWardFrm _patientListFrm;

        /// <summary>
        ///     设置窗体Logo
        /// </summary>
        public Image Logo { private get; set; }

        /// <summary>
        ///     入院日期
        /// </summary>
        public DateTime InpDateTime
        {
            get { return _patientListFrm.InpDateTime; }
        }

        /// <summary>
        ///     病人ID
        /// </summary>
        public string PatientId
        {
            get { return _patientListFrm.PatientId; }
        }


        /// <summary>
        ///     病人姓名
        /// </summary>
        public string PatientName
        {
            get { return _patientListFrm.PatientName; }
        }

        /// <summary>
        ///     住院次数
        /// </summary>
        public string VisitId
        {
            get { return _patientListFrm.VisitId; }
        }

        /// <summary>
        ///     科室
        /// </summary>
        public string DeptCode
        {
            get { return _patientListFrm.DeptCode; }
        }

        /// <summary>
        ///     获取当前的主框架界面实例
        /// </summary>
        /// <returns></returns>
        public static MdiFrm GetInstance()
        {
            return Application.OpenForms["MdiFrm"] as MdiFrm;
        }

        /// <summary>
        /// 获取病人列表，引用病人列表窗体中的DataSet
        /// </summary>
        /// <returns></returns>
        public DataSet GetPatientlist()
        {
            return _patientListFrm.DsPatient;
        }
        /// <summary>
        /// 主框架右侧病人列表宽度
        /// </summary>
        public new int Right
        {
            get
            {
                return navBarControl2.Width +
                    (navBarControl2.NavPaneForm == null ? 0 : navBarControl2.NavPaneForm.Width);
            }
        }

        #endregion

        public MdiFrm()
        {
            InitializeComponent();
            //SkinHelper.InitSkinGallery(rgbiSkins);

            defaultLookAndFeel1.LookAndFeel.SetSkinStyle("Liquid Sky");
            //defaultLookAndFeel1.LookAndFeel.SetSkinStyle("Office 2007 Green");
        }

        public MdiFrm(Form homePage)
        {
            InitializeComponent();
            //SkinHelper.InitSkinGallery(rgbiSkins);

            defaultLookAndFeel1.LookAndFeel.SetSkinStyle("Liquid Sky");
            //defaultLookAndFeel1.LookAndFeel.SetSkinStyle("Office 2007 Green");
            _homePage = homePage;
        }


        /// <summary>
        ///     根据Form找选项卡
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        private XtraTabPage GetTabPageByForm(Form form)
        {
            return form == null
                ? null
                : xtraTabControl1.TabPages.FirstOrDefault(p => p.Name == form.GetType() + form.Text);
        }

        /// <summary>
        ///     根据病人信息找选项卡
        /// </summary>
        /// <returns></returns>
        private XtraTabPage GetTabPageByPatient(string caption)
        {
            return xtraTabControl1.TabPages.FirstOrDefault(p => p.Name == caption);
        }

        /// <summary>
        ///     根据Form信息和病人信息找选项卡
        /// </summary>
        /// <returns></returns>
        private XtraTabPage GetTabPageByForm(Form form, XtraTabPage tabpage)
        {
            foreach (Control ctl in tabpage.Controls)
            {
                if (ctl is XtraTabControl)
                {
                    if (form == null) return null;
                    XtraTabPage page =
                        (ctl as XtraTabControl).TabPages.FirstOrDefault(
                            p => p.Name == form.GetType() + form.Text);
                    if (page == null)
                    {
                        page = new XtraTabPage
                        {
                            Text = form.Text,
                            //Image = image,
                            Name = form.GetType() + form.Text
                        };

                        page.Controls.Add(form);
                        (ctl as XtraTabControl).TabPages.Add(page);
                    }
                    return page;
                }
            }
            return null;
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
        ///     创建选项卡页
        /// </summary>
        /// <param name="form"></param>
        /// <param name="model"></param>
        /// <param name="level">菜单级别，默认为1级菜单。2级菜单可实现2级选项卡</param>
        public void CreateTabPage(Form form, MenuManager model, int level = 1)
        {


            if (form == null) return;

            if (level == 0)
            {
                //navBarControl2.();
                navBarControl2.OptionsNavPane.NavPaneState = NavPaneState.Collapsed;
            }
            else
            {
                navBarControl2.OptionsNavPane.NavPaneState = NavPaneState.Expanded;
            }

            Form form1 = form;
            string key;
            if (level == 2)
                key = GVars.Patient.ID + form1.GetType() + form1.Text;
            else
                key = level.ToString() + form1.GetType() + form1.Text;

            KeyValuePair<string, XtraTabPage> pair = _dictionaryPages.FirstOrDefault(p => p.Key.Equals(key));

            //XtraTabPage xtp = GetTabPageByForm(form);
            if (pair.Value != null)
            {
                if (pair.Value.IsDisposed)
                    _dictionaryPages.Remove(key);
                else
                {
                    pair.Value.PageVisible = true;
                    pair.Value.Show();
                    pair.Value.Parent.Show();
                    pair.Value.Parent.Parent.Show();
                    return;
                }
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

                // form.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                form.FormClosed += TabPage_FormClosed;

                if (level == 2)
                {
                    #region level 等于 2
                    XtraTabPage page = GetTabPageByPatient(GetPatientName());

                    XtraTabPage page2;
                    if (page == null)
                    {
                        #region 没有找到页面，则创建
                        page = new XtraTabPage
                        {
                            Text = GetPatientName(),
                            //Image = image,
                            Name = GetPatientName()
                        };
                        page.AutoScroll = true;
                        page.Tag = _patientListFrm.PatientId;

                        var tabControl = new XtraTabControl();

                        tabControl.CloseButtonClick += xtraTabControl1_CloseButtonClick;
                        tabControl.DoubleClick += xtraTabControl1_DoubleClick;
                        tabControl.ClosePageButtonShowMode = ClosePageButtonShowMode.InActiveTabPageHeader;

                        page.Controls.Add(tabControl);

                        tabControl.Dock = DockStyle.Fill;

                        xtraTabControl1.TabPages.Add(page);

                        page2 = new XtraTabPage
                        {
                            Text = form.Text,
                            //Image = image,
                            Name = form.GetType() + form.Text
                        };

                        page2.Controls.Add(form);

                        tabControl.TabPages.Add(page2);

                        form.Show();
                        page2.Show();

                        page.PageVisible = true;
                        page.Show();
                        xtraTabControl1.SelectedTabPage = page;

                        _dictionaryPages.Add(key, page2);
                        #endregion
                    }
                    else
                    {
                        page2 = GetTabPageByForm(form, page);
                        page2.PageVisible = true;
                        form.Show();
                        page2.Show();
                        xtraTabControl1.SelectedTabPage = page;
                    }
                    #endregion
                }
                else
                {
                    #region level 等于 1 或 0
                    var page = new XtraTabPage
                    {
                        Text = form.Text,
                        //Image = image,
                        Name = form.GetType() + form.Text
                    };

                    page.AutoScroll = true;

                    page.Tag = model;

                    xtraTabControl1.TabPages.Add(page);
                    xtraTabControl1.SelectedTabPage = page;
                    page.Controls.Add(form);
                    form.Show();
                    page.Show();

                    // 模拟当前选中项点击事件
                    //navBarControl2.SelectedLink.PerformClick();

                    PatientSelectionChanged();

                    _dictionaryPages.Add(key, page);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private string GetPatientName()
        {
            return _patientListFrm.PatientName;
            #region 注释
            if (navBarControl2.SelectedLink == null) return null;
            string caption = navBarControl2.SelectedLink.Item.Caption;
            if (string.IsNullOrEmpty(caption)) return null;
            string[] strs = caption.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (strs.Length == 2)
                return strs[1];
            return caption;
            #endregion
        }

        /// <summary>
        ///     关闭选项卡页面
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
        ///     关闭选项卡页面
        /// </summary>
        /// <param name="tabPage"></param>
        private void TabPageClose(XtraTabPage tabPage)
        {
            if (tabPage == null) return;
            if (tabPage.Tag == null)
            {
                if (tabPage.Parent.Parent is XtraTabPage)
                    if (tabPage.Parent is XtraTabControl)
                    {
                        var tabcontrol = (tabPage.Parent as XtraTabControl);
                        if (tabcontrol.TabPages.Count == 1)
                        {
                            var t =
                                (tabPage.Parent.Parent as XtraTabPage);
                            //if (!t.IsDisposed)
                            //    t.Dispose();
                            tabcontrol.Dispose();
                            xtraTabControl1.TabPages.Remove(t);
                        }
                    }
            }
            tabPage.PageVisible = false;
            xtraTabControl1.TabPages.Remove(tabPage);
            tabPage.Dispose();
        }

        /// <summary>
        ///     选项卡关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabPage_FormClosed(object sender, FormClosedEventArgs e)
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
                base.LoadingShow();

                if (!File.Exists(@"Resource\ICON\MdiLogo.png")) return;

                Image img = Image.FromFile(@"Resource\ICON\MdiLogo.png");

                this.pictureEdit1.EditValue = img;

                //pictureEdit1.Width = img.Width;

                //SkinHelper.InitSkinPopupMenu(popupMenu1);

                SkinHelper.InitSkinPopupMenu(popupMenu2);

                //迭代出所有皮肤样式
                foreach (LinkPersistInfo item in popupMenu2.LinksPersistInfo)
                {
                    if (item.Link.Caption.Contains("2010") ||
                        item.Link.Caption.Contains("皮肤"))
                    {
                        BarSubItem subItem = item.Item as BarSubItem;
                        if (subItem == null)
                        {
                            if (item.Item is BarButtonItem)
                            {
                                (item.Item as BarButtonItem).Visibility = BarItemVisibility.Always;
                            }
                            continue;
                        }

                        if (subItem.LinksPersistInfo.Count == 1)
                            subItem.LinksPersistInfo[0].Visible = true;

                        foreach (LinkPersistInfo linkChild in subItem.LinksPersistInfo)
                        {
                            if (linkChild.Link.Caption.Contains("天空")
                                || linkChild.Link.Caption.Contains("南瓜")
                                || linkChild.Link.Caption.Contains("海洋")
                                || linkChild.Link.Caption.Contains("春季")
                                || linkChild.Link.Caption.Contains("液态")
                                )
                            {
                                linkChild.Link.Visible = true;
                            }
                            else
                                linkChild.Link.Visible = false;
                        }
                    }
                    else
                    {
                        BarSubItem subItem = item.Item as BarSubItem;
                        if (subItem == null)
                        {
                            if (item.Item is BarButtonItem)
                            {
                                (item.Item as BarButtonItem).Visibility = BarItemVisibility.Never;
                            }
                        }
                    }
                }
                #region 注释
                ////迭代出所有皮肤样式
                //foreach (LinkPersistInfo item in popupMenu2.LinksPersistInfo)
                //{
                //    if (item.Link.Caption == @"Liquid Sky" || item.Link.Caption.Contains("2010") || item.Link.Caption.Contains("Theme") ||
                //        item.Link.Caption.Contains("Skin"))
                //    {
                //        BarSubItem subItem = item.Item as BarSubItem;
                //        if (subItem == null)
                //        {
                //            if (item.Item is BarButtonItem)
                //            {
                //                (item.Item as BarButtonItem).Visibility = BarItemVisibility.Always;
                //            }
                //            continue;
                //        }

                //        if (subItem.LinksPersistInfo.Count == 1)
                //            subItem.LinksPersistInfo[0].Visible = true;

                //        foreach (LinkPersistInfo linkChild in subItem.LinksPersistInfo)
                //        {
                //            if (linkChild.Link.Caption == "Liquid Sky"
                //                || linkChild.Link.Caption.Contains("2010")
                //                || linkChild.Link.Caption.Contains("Office")
                //                || linkChild.Link.Caption.Contains("Xmas")
                //                || linkChild.Link.Caption.Contains("Valentine")
                //                || linkChild.Link.Caption.Contains("Twins")
                //                || linkChild.Link.Caption.Contains("iMaginary")
                //                || linkChild.Link.Caption.Contains("Coffee")
                //                || linkChild.Link.Caption.Contains("Oceans")
                //                || linkChild.Link.Caption.Contains("Stardust")
                //                || linkChild.Link.Caption.EndsWith("Blue")
                //                || linkChild.Link.Caption.Contains("Black")
                //                || linkChild.Link.Caption.Contains("Spring")
                //                )
                //            {
                //                linkChild.Link.Visible = true;
                //            }
                //            else
                //                linkChild.Link.Visible = false;
                //        }
                //    }
                //    else
                //    {
                //        BarSubItem subItem = item.Item as BarSubItem;
                //        if (subItem == null)
                //        {
                //            if (item.Item is BarButtonItem)
                //            {
                //                (item.Item as BarButtonItem).Visibility = BarItemVisibility.Never;
                //            }
                //        }
                //    }
                //}
                #endregion
                bar3.Visible = false;
                barEditItem1.Visibility = BarItemVisibility.Never;
                barEditItem1.AutoFillWidth = true;
                //barEditItem1.Width = (int)(this.Width * 0.96);

                // 另一种类似水晶的选择样式
                //navBarControl1.LookAndFeel.UseDefaultLookAndFeel = false;
                //navBarControl1.SkinExplorerBarViewScrollStyle = SkinExplorerBarViewScrollStyle.ScrollBar;

                //
                //                navBarControl2.LookAndFeel.UseDefaultLookAndFeel = false;
                //                navBarControl2.SkinExplorerBarViewScrollStyle = SkinExplorerBarViewScrollStyle.ScrollBar;

                //迭代出所有皮肤样式
                foreach (SkinContainer skin in SkinManager.Default.Skins)
                {
                    //comboBoxEdit1.Properties.Items.Add(skin.SkinName);
                }

                InitMenu();

                //Form form = GetFormInDll("DocSetting.dll", "HISPlus.ControlSetting", null);
                //CreateDefaultTabPage(xtraTabPage1, form);  

                //CreateDefaultTabPage(xtraTabPage1, new PatientCard());

                InitPatients();

                lblDeptName.Text = GVars.User.DeptName;

                //lblUserName.Location = new Point(lblDeptName.Location.X + lblDeptName.Width + 20, lblUserName.Location.Y);

                lblUserName.Text = GVars.User.Name;

                _currentTime = GVars.OracleAccess.GetSysDate();
                lblTime.Text = _currentTime.ToString(ComConst.FMT_DATE.LONG);

                if (!DesignMode)
                    timer1.Enabled = true;

                ucButton1.Visible = false;
                btnCard_Click(null, null);
                if (_homePage != null)
                    CreateTabPage(_homePage, new MenuManager(), 0);

                //LPD 2016-06-08
                //navBarControl2.OptionsNavPane.NavPaneState = NavPaneState.Expanded;


                // 开始检查自动更新
                Thread thread = new Thread(new ThreadStart(checkNeedUpdate));
                //LB20110622添加
                thread.IsBackground = true;
                thread.Priority = ThreadPriority.BelowNormal;
                //LB20110622添加结束
                thread.Start();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                base.LoadingClose();
            }
        }

        /// <summary>
        ///     获取DLL路径
        /// </summary>
        /// <returns></returns>
        private static string getDllPath()
        {
            string dllFile = Assembly.GetExecutingAssembly().CodeBase;
            int length = dllFile.LastIndexOf("/", StringComparison.Ordinal);
            return dllFile.Substring(8, length - 8);
        }

        public static Form GetFormInDll(string dllName, string typeName, params object[] args)
        {
            if (string.IsNullOrEmpty(typeName))
                return null;
            Form form;
            try
            {
                var genericInstance = Activator.CreateInstance(Assembly.LoadFrom(dllName).GetType(typeName), args) as Form;
                form = genericInstance;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
                // throw new Exception(("DLL名称: " + dllName + " 类名:" + typeName + " 未找到. ") + ex.Message);
                return null;
            }
            return form;
        }

        public static Image GetImageFile(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;
            int pos = path.LastIndexOf(Path.DirectorySeparatorChar);
            if (pos > 0)
            {
                path = path.Substring(pos + 1, (path.Length - pos) - 1);
            }
            path = Path.Combine(Path.Combine(Path.Combine(getDllPath(), "Resource"), "ICON"), path);
            if (File.Exists(path))
                return Image.FromFile(path);
            return null;
        }

        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            try
            {
                var a = (ClosePageButtonEventArgs)e;

                TabPageClose((XtraTabPage)a.Page);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        ///     加载导航菜单
        /// </summary>
        /// <returns>成功/失败</returns>
        private void InitMenu()
        {
            // 从Menu.xml中读取菜单信息并写入数据库
            // 在MenuManager.hbm.xml中设置主键ID的生成方式为手动写入:<generator class="assigned" />
            // 数据写入完成后,记得把ID生成方式还原

            //_dsMenu.ReadXml("Menu.xml");
            //IList<MenuManager> list = new List<MenuManager>();
            //DataView dv = new DataView { Table = _dsMenu.Tables[0], Sort = "ParentId, SERIAL_NO" };
            //foreach (DataRowView drv in dv)
            //{
            //    MenuManager m = new MenuManager();
            //    m.Id = drv["NodeId"].ToString();
            //    m.ParentId = Convert.ToDecimal(drv["ParentId"]);
            //    m.IconPath = drv["Icon"].ToString();
            //    m.Name = drv["NodeName"].ToString();
            //    m.Assembly = drv["ADDDLL"].ToString();
            //    m.FormName = drv["ADDRESS"].ToString();
            //    m.ModuleCode = drv["MODULE_CODE"].ToString();
            //    m.OpenMode = drv["WIN_OPEN_MODE"].ToString();
            //    m.SortId = drv["SERIAL_NO"] == null || drv["SERIAL_NO"] == DBNull.Value
            //        ? 0 : Convert.ToDecimal(drv["SERIAL_NO"]);

            //    list.Add(m);
            //}

            //new MenuManagerDAL().Save(list);
            _listMenu = EntityOper.GetInstance().FindByProperty<MenuManager>("Enabled", (byte)1);
            _listMenu = _listMenu.OrderBy(p => p.ParentId).ThenBy(p => p.SortId).ToList();

            const string filter = "MODULE_CODE = '{0}'";
            bool itemEnabled = false;
            foreach (MenuManager model in _listMenu)
            {
                DataRow[] drFind = GVars.User.Rights.Tables[0].Select(string.Format(filter, model.ModuleCode));

                itemEnabled = drFind.Length > 0;

                if (model.ParentId == "0")
                {
                    //父级菜单
                    var nbg = new NavBarGroup
                    {
                        Caption = model.Name,
                        Name = model.Id,
                        Expanded = true,
                        GroupStyle = IsLargeImage
                            ? NavBarGroupStyle.LargeIconsText
                            : NavBarGroupStyle.SmallIconsText,
                        Tag = model,
                    };

                    if (IsLargeImage)
                    // 按钮图片                       
                    {
                        nbg.LargeImage = GetImageFile(model.IconPath);
                        nbg.GroupCaptionUseImage = NavBarImage.Large;
                    }
                    else
                    {
                        nbg.SmallImage = GetImageFile(model.IconPath);
                        nbg.GroupCaptionUseImage = NavBarImage.Small;
                    }

                    nbg.ItemChanged += nbg_ItemChanged;


                    if (model.Name == "护理评估")
                        CreateTreeList(nbg);
                    navBarControl1.Groups.Add(nbg);
                }
                else
                {
                    //子级菜单
                    var item = new NavBarItem
                    {
                        Caption = model.Name,
                        Name = model.Assembly,
                        Hint = model.Name,
                        Enabled = itemEnabled,
                        Visible = itemEnabled,
                        Tag = model,
                    };

                    item.LinkClicked += NavBarItem_Click; //赋予单击事件
                    if (IsLargeImage)
                        // 按钮图片                       
                        item.LargeImage = GetImageFile(model.IconPath);
                    else
                        item.SmallImage = GetImageFile(model.IconPath);

                    foreach (NavBarGroup grp in navBarControl1.Groups)
                    {
                        if (grp.Name == model.ParentId.ToString())
                        {
                            grp.ItemLinks.Add(item);
                            break;
                        }
                    }
                    navBarControl1.Items.Add(item);
                }
            }

            // 如果没有任何权限，就隐藏分组
            foreach (NavBarGroup group in navBarControl1.Groups)
            {
                if (group.VisibleItemLinks.Count == 0 && group.GroupStyle != NavBarGroupStyle.ControlContainer)
                    group.Visible = false;
            }
        }

        private void nbg_ItemChanged(object sender, EventArgs e)
        {

            var item = sender as NavBarGroup;
            if (item == null) return;
            return;
            var model = item.Tag as MenuManager;
            if (model == null) return;
            if (model.ConnectionPatient == 0)
            {
                navBarControl2.Hide();
            }
            else
            {
                navBarControl2.Show();
            }
        }

        ///// <summary>
        /////     护理文书创建导航菜单树
        ///// </summary>
        ///// <param name="group"></param>
        //private void CreateTreeList(NavBarGroup group)
        //{
        //    group.GroupStyle = NavBarGroupStyle.ControlContainer;

        //    IList<DocTemplateClass> listTemplateClass = new DocTemplateClassDAL().LoadAll();
        //    IList<DocTemplate> listElements = new DocTemplateDAL().LoadAll();

        //    IList<DocTemplateDept> listDepts = new DocTemplateDeptDAL().FindByProperty("DeptCode", GVars.User.DeptCode);

        //    listElements =
        //        listElements.Where(p => p.IsGlobal == 1 || listDepts.Any(q => q.DocTemplate.Id == p.Id)).ToList();

        //    if (listElements.Count == 0) return;

        //    var treeElement = new TreeList();

        //    treeElement.Columns.AddRange(new[]
        //    {
        //        new TreeListColumn
        //        {
        //            FieldName = "Id",
        //            Visible = false,
        //            Name = "treeColumn1"
        //        },
        //        new TreeListColumn
        //        {
        //            Caption = @"元素列表",
        //            FieldName = "Name",
        //            Visible = true,
        //            Name = "treeColumn2",
        //            VisibleIndex = 1,
        //        }
        //    });

        //    //// 行指示器【隐藏/显示】
        //    treeElement.OptionsView.ShowIndicator = false;
        //    //// 被选中的单元格的聚集框 【隐藏/显示】
        //    treeElement.OptionsView.ShowFocusedFrame = true;
        //    //// 双击展开节点
        //    treeElement.OptionsBehavior.AllowExpandOnDblClick = true;

        //    treeElement.OptionsBehavior.Editable = false;

        //    treeElement.OptionsView.ShowHorzLines = true;
        //    treeElement.OptionsView.ShowVertLines = true;
        //    treeElement.OptionsView.ShowRoot = true;
        //    treeElement.OptionsView.ShowButtons = true;
        //    treeElement.TreeLineStyle = LineStyle.Dark;

        //    //treeElement.KeyFieldName = "Id";
        //    //treeElement.ParentFieldName = "ParentId";

        //    //treeElement.DataSource = listElements;
        //    treeElement.Dock = DockStyle.Fill;
        //    treeElement.OptionsView.ShowColumns = false;
        //    //treeElement.CustomDrawNodeCell += treeElement_CustomDrawNodeCell;
        //    treeElement.BeginUnboundLoad();
        //    foreach (DocTemplateClass templateClass in listTemplateClass)
        //    {
        //        //TreeListNode node= treeElement.AppendNode(new Tree() { Id = templateClass.Id, Name = templateClass.Name, ParentId = templateClass.ParentId }, (int)templateClass.ParentId);
        //        //node.SetValue(treeElement.Columns[1], templateClass.Name);
        //        TreeListNode node;
        //        if (templateClass.ParentId == 0)
        //            node = treeElement.AppendNode(new object[] { templateClass.Id, templateClass.Name, 0 }, null);
        //        else
        //        {
        //            node =
        //                treeElement.AppendNode(
        //                    new object[] { templateClass.Id, templateClass.Name, templateClass.ParentId },
        //                    (int)templateClass.ParentId);
        //        }
        //        DocTemplateClass @class = templateClass;

        //        IList<DocTemplate> docTemplates = listElements.Where(p => p.DocTemplateClass.Id == @class.Id).ToList();
        //        if (!docTemplates.Any())
        //            treeElement.DeleteNode(node);

        //        foreach (DocTemplate template in docTemplates)
        //        {
        //            TreeListNode nodeChild =
        //                treeElement.AppendNode(
        //                    new object[]
        //                    {
        //                        template.DocTemplateClass.Id*100 + template.Id, template.TemplateName,
        //                        template.DocTemplateClass.Id
        //                    }, node);

        //            nodeChild.Tag = template.Id;
        //        }
        //    }

        //    treeElement.MouseClick += treeElement_MouseClick;

        //    treeElement.EndUnboundLoad();

        //    treeElement.ExpandAll();


        //    var groupContainer = new NavBarGroupControlContainer();
        //    group.ControlContainer = groupContainer;
        //    groupContainer.Height = 500;
        //    groupContainer.Controls.Add(treeElement);
        //    navBarControl1.Controls.Add(groupContainer);
        //}

        ///// <summary>
        /////     护理文书创建导航菜单树
        ///// </summary>
        ///// <param name="group"></param>
        //private void CreateTreeList(NavBarGroup group)
        //{
        //    group.GroupStyle = NavBarGroupStyle.ControlContainer;

        //    IList<DocTemplateClass> listTemplateClass = new DocTemplateClassDAL().LoadAll();
        //    IList<DocTemplate> listElements = new DocTemplateDAL().LoadAll();

        //    IList<DocTemplateDept> listDepts = new DocTemplateDeptDAL().FindByProperty("DeptCode", GVars.User.DeptCode);

        //    listElements =
        //        listElements.Where(p => p.IsGlobal == 1 || listDepts.Any(q => q.DocTemplate.Id == p.Id)).ToList();

        //    if (listElements.Count == 0) return;

        //    UcTreeList ucTreeList1 = new UcTreeList();            
        //    ucTreeList1.ShowHeader = false;
        //    ucTreeList1.Add("Id", "Id", false);
        //    ucTreeList1.Add("模板列表", "Name");

        //    foreach (DocTemplateClass templateClass in listTemplateClass)
        //    {
        //        //TreeListNode node= treeElement.AppendNode(new Tree() { Id = templateClass.Id, Name = templateClass.Name, ParentId = templateClass.ParentId }, (int)templateClass.ParentId);
        //        //node.SetValue(treeElement.Columns[1], templateClass.Name);
        //        TreeListNode node;
        //        if (templateClass.ParentId == 0)
        //            node =ucTreeList1.AppendNode(new object[] { templateClass.Id, templateClass.Name, 0 }, null);
        //        else
        //        {
        //            node =
        //                ucTreeList1.AppendNode(
        //                    new object[] { templateClass.Id, templateClass.Name, templateClass.ParentId },
        //                    (int)templateClass.ParentId);
        //        }
        //        DocTemplateClass @class = templateClass;

        //        IList<DocTemplate> docTemplates = listElements.Where(p => p.DocTemplateClass.Id == @class.Id).ToList();
        //        if (!docTemplates.Any())
        //            ucTreeList1.DeleteNode(node);

        //        foreach (DocTemplate template in docTemplates)
        //        {
        //            TreeListNode nodeChild =
        //                ucTreeList1.AppendNode(
        //                    new object[]
        //                    {
        //                        template.DocTemplateClass.Id*100 + template.Id, template.TemplateName,
        //                        template.DocTemplateClass.Id
        //                    }, node);

        //            nodeChild.Tag = template.Id;
        //        }
        //    }

        //    ucTreeList1.ExpandAll();
        //    ucTreeList1.AfterSelect+=new NodeEventHandler(ucTreeList1_AfterSelect);

        //    var groupContainer = new NavBarGroupControlContainer();
        //    group.ControlContainer = groupContainer;
        //    groupContainer.Height = 500;
        //    ucTreeList1.Dock = DockStyle.Fill;
        //    ucTreeList1.Visible = true;
        //    groupContainer.Controls.Add(ucTreeList1);
        //    ucTreeList1.Dock = DockStyle.Fill;
        //    ucTreeList1.Visible = true;
        //    navBarControl1.Controls.Add(groupContainer);
        //}

        /// <summary>
        ///     护理文书创建导航菜单树
        /// </summary>
        /// <param name="group"></param>
        private void CreateTreeList(NavBarGroup group)
        {
            group.GroupStyle = NavBarGroupStyle.ControlContainer;

            LeftDocModule form = new LeftDocModule
            {
                FormBorderStyle = FormBorderStyle.None,
                TopLevel = false,
                Dock = DockStyle.Fill,
                Visible = true,
            };

            form.LinkClicked += LeftDocModule_LinkClicked;

            var groupContainer = new NavBarGroupControlContainer();
            group.ControlContainer = groupContainer;
            groupContainer.Height = 500;
            groupContainer.Controls.Add(form);
            form.Show();
            navBarControl1.Controls.Add(groupContainer);
        }

        public void CreateDocForm(decimal templateId, string templateName)
        {
            Form form = GetFormInDll("DocSetting.dll", "HISPlus.DesignTemplate", new object[] { templateId, templateName, true });
            //CreateTabPage(new DesignTemplate((decimal)node.Tag, node.GetValue("Name").ToString()), null, 2);
            CreateTabPage(form, null, 2);
        }

        void LeftDocModule_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                base.LoadingShow();

                CreateDocForm(Convert.ToDecimal(e.Link.Item.Tag), e.Link.Item.Name);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                base.LoadingClose();
            }
        }


        void ucTreeList1_AfterSelect(object sender, NodeEventArgs e)
        {
            try
            {
                TreeListNode node = e.Node;

                if (node == null || node.HasChildren) return;

                base.LoadingShow();

                Form form = GetFormInDll("DocSetting.dll", "HISPlus.DesignTemplate", new object[] { (decimal)node.Tag, node.GetValue("Name").ToString(), true });
                //CreateTabPage(new DesignTemplate((decimal)node.Tag, node.GetValue("Name").ToString()), null, 2);
                CreateTabPage(form, null, 2);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                base.LoadingClose();
            }
        }

        /// <summary>
        ///     高亮选中项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeElement_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            var treeElement = sender as TreeList;
            if (treeElement == null) return;
            if (e.Node != treeElement.FocusedNode) return;

            e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);

            var r = new Rectangle(e.EditViewInfo.ContentRect.Left,
                e.EditViewInfo.ContentRect.Top,
                Convert.ToInt32(e.Graphics.MeasureString(e.CellText, treeElement.Font).Width + 1),
                Convert.ToInt32(e.Graphics.MeasureString(e.CellText, treeElement.Font).Height));

            e.Graphics.FillRectangle(SystemBrushes.Highlight, r);

            e.Graphics.DrawString(e.CellText, treeElement.Font, SystemBrushes.HighlightText, r);

            e.Handled = true;
        }

        void DoWithProcess(Action<int> percent)
        {
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(100);
                percent(i);
            }
        }

        /// <summary>
        ///     菜单单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBarItem_Click(object sender, EventArgs e)
        {


            var item = sender as NavBarItem;
            try
            {
                base.LoadingShow();

                if (item == null) return;
                var model = item.Tag as MenuManager;
                if (model == null) return;
                if (model.ConnectionPatient == 0)
                {
                    //navBarControl2.();
                    navBarControl2.OptionsNavPane.NavPaneState = NavPaneState.Collapsed;
                }
                else
                {
                    navBarControl2.OptionsNavPane.NavPaneState = NavPaneState.Expanded;
                }

                Form form = GetFormInDll(model.Assembly, model.FormName);
                if (form != null)
                {
                    CreateTabPage(form, model, model.ConnectionPatient);
                }
                else
                {
                    XtraMessageBox.Show("窗口数据配置错误,请联系系统管理员！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                base.LoadingClose();
                //navBarControl2.OptionsNavPane.NavPaneState = NavPaneState.Expanded;
            }
        }

        /// <summary>
        ///     获取字符串长度，一个汉字算两个字节
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetLength(string str)
        {
            if (str.Length == 0) return 0;
            var ascii = new ASCIIEncoding();
            int tempLen = 0;
            byte[] s = ascii.GetBytes(str);
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }
            }
            return tempLen;
        }


        private void InitPatients()
        {


            var nbg = new NavBarGroup
            {
                Caption = @"病人列表",
                Expanded = true,

            };
            // //LPD 2016-06-08
            //navBarControl2.OptionsNavPane.NavPaneState = NavPaneState.Expanded;

            //nbg.GroupClientHeight = 500;           
            nbg.GroupStyle = NavBarGroupStyle.ControlContainer;

            _patientListFrm = new PatientListWardFrm
            {
                FormBorderStyle = FormBorderStyle.None,
                TopLevel = false,
                Dock = DockStyle.Fill,
                Visible = true,
                DeptCode = GVars.User.DeptCode
            };
            _patientListFrm.PatientChanged += patientListFrm_PatientChanged;
            _patientListFrm.PatientRefresh += patientListFrm_PatientRefresh;//2015.11.25 add
            _patientListFrm.Show();

            var groupContainer = new NavBarGroupControlContainer();
            nbg.ControlContainer = groupContainer;
            //groupContainer.Height = 500;            
            groupContainer.Controls.Add(_patientListFrm);
            nbg.NavigationPaneVisible = false;
            navBarControl2.Controls.Add(groupContainer);
            navBarControl2.Groups.Add(nbg);
            navBarControl2.ActiveGroup = nbg;



            #region 注释
            //nbgPatient.GroupStyle = NavBarGroupStyle.ControlContainer;

            //PatientListWardFrm patientListFrm = new PatientListWardFrm();
            //patientListFrm.FormBorderStyle = FormBorderStyle.None;
            //patientListFrm.TopLevel = false;
            //_patientListFrm.Dock = DockStyle.Fill;
            //patientListFrm.Visible = true;
            //patientListFrm.DeptCode = GVars.User.DeptCode;
            //patientListFrm.Show();

            //NavBarGroupControlContainer groupContainer = new NavBarGroupControlContainer();
            //nbgPatient.ControlContainer = groupContainer;
            ////groupContainer.Height = 500;
            //groupContainer.Controls.Add(patientListFrm);

            //navBarControl2.Controls.Add(groupContainer);
            #endregion

            return;
            #region 注释
            //DataSet DsPatient = new PatientDbI(GVars.OracleAccess).GetWardPatientList(GVars.User.DeptCode);

            ////nbgPatient.AllowGlyphSkinning = DefaultBoolean.True;
            ////nbgPatient.GroupStyle = NavBarGroupStyle.SmallIconsText;

            ////navBarControl2.ShowLinkHint = true;
            //int i = 0;
            //foreach (DataRow dr in DsPatient.Tables[0].Rows)
            //{
            //    //子级菜单
            //    var item = new NavBarItem
            //    {
            //        Caption =
            //            string.Format("{0}{1}",
            //                dr["BED_LABEL"].ToString()
            //                    .PadRight(
            //                        8 - GetLength(dr["BED_LABEL"].ToString()) + dr["BED_LABEL"].ToString().Length, ' '),
            //                dr["NAME"].ToString()),
            //    };
            //    item.Hint = item.Caption;
            //    item.Tag = dr["PATIENT_ID"] + "," + dr["VISIT_ID"];
            //    item.SmallImageIndex = i;
            //    i++;

            //    //item.SmallImage = IsLargeImage
            //    //    ? navBarControl1.Items[i % navBarControl1.Items.Count].LargeImage
            //    //    : navBarControl1.Items[i % navBarControl1.Items.Count].SmallImage;

            //    // nbgPatient.ItemLinks.Add(item);
            //}

            //// 默认选中第一项
            //// if (nbgPatient.ItemLinks.Count > 0)
            //{
            //    //  navBarControl2.SelectedLink = nbgPatient.ItemLinks[0];

            //    string str = navBarControl2.SelectedLink.Item.Tag.ToString();
            //    string[] strs = str.Split(',');

            //    GVars.Patient.ID = strs[0];
            //    GVars.Patient.VisitId = strs[1];
            //}
            #endregion
        }

        private void patientListFrm_PatientChanged(object sender, PatientEventArgs e)
        {
            //patientListFrm.VisitId
            PatientSelectionChanged();
        }

        //2015.11.25 add
        private void patientListFrm_PatientRefresh(object sender, PatientEventArgs e)
        {
            //切换科室时，刷新界面显示
            lblDeptName.Text = GVars.User.DeptName;

            XtraTabPage page = xtraTabControl1.SelectedTabPage;
            if (page == null) return;
            foreach (Control ctl in page.Controls)
            {
                if (ctl is Form)
                {
                    if (ctl is PatientCard)
                        (ctl as PatientCard).PatientListRefresh(null, null);
                    if (ctl is IBasePatient)//2015.12.04增加，应该和PatientCard整合在一起
                        (ctl as IBasePatient).Patient_ListRefresh(sender, e);
                }
            }
        }
        private void navBarControl2_CustomDrawLink(object sender, CustomDrawNavBarElementEventArgs e)
        {
            var linkInfo = e.ObjectInfo as NavLinkInfoArgs;

            if (linkInfo != null && linkInfo.Link.Item.SmallImageIndex % 2 != 0)
            {
                e.Graphics.FillRectangle(Brushes.AliceBlue, e.RealBounds);
            }

            return;
            // If a link is not hot tracked or pressed it is drawn in the normal way.
            if (e.ObjectInfo.State == ObjectState.Hot || e.ObjectInfo.State == ObjectState.Pressed)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void navBarControl2_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            PatientSelectionChanged(e.Link);

            //XtraTabPage page = GetTabPageByPatient(navBarControl2.SelectedLink.Item.Caption);

            //if (page == null) return;
            //page.PageVisible = true;
            //page.Show();
        }

        /// <summary>
        ///     定位病人
        /// </summary>
        public void LocatePatient(string patientId)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;

                _patientListFrm.LocatePatient(patientId);
                //foreach (NavBarItemLink item in nbgPatient.ItemLinks)
                //{
                //    if (!item.Item.Tag.ToString().StartsWith(patientId + ",")) continue;
                //    navBarControl2.SelectedLink = item;
                //    break;
                //}
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 是否可以刷新，用于切换科室调用
        /// </summary>
        /// <returns></returns>
        public bool CanRefresh()
        {
            foreach (XtraTabPage page in xtraTabControl1.TabPages)
            {
                foreach (Control ctl in page.Controls)
                {
                    if (ctl is Form)
                        if (!(ctl is PatientCard))
                            return false;
                }
            }
            return true;
        }

        private void PatientSelectionChanged(NavBarItemLink link)
        {
            XtraTabPage page = xtraTabControl1.SelectedTabPage;

            if (page == null) return;

            if (link == null) return;

            string str = link.Item.Tag.ToString();
            string[] strs = str.Split(',');

            GVars.Patient.ID = strs[0];
            GVars.Patient.VisitId = strs[1];
            GVars.Patient.STATE = strs[2];

            foreach (Control ctl in page.Controls)
            {
                if (ctl is Form)
                    if (ctl is IBasePatient)
                        (ctl as IBasePatient).Patient_SelectionChanged(null,
                            new PatientEventArgs(strs[0], strs[1], strs[2]));
            }
        }

        private void PatientSelectionChanged()
        {
            XtraTabPage page = xtraTabControl1.SelectedTabPage;

            if (page == null) return;

            GVars.Patient.ID = _patientListFrm.PatientId;
            GVars.Patient.VisitId = _patientListFrm.VisitId;
            GVars.Patient.STATE = _patientListFrm.STATE;

            foreach (Control ctl in page.Controls)
            {
                if (ctl is Form)
                    if (ctl is IBasePatient)
                        (ctl as IBasePatient).Patient_SelectionChanged(null,
                            new PatientEventArgs(GVars.Patient.ID, GVars.Patient.VisitId, GVars.Patient.STATE));
                #region//2015.12.19
                if (ctl is XtraTabControl)
                {
                    //临时，提高效率
                    if (_patientListFrm.PatientId == page.Tag.ToString())
                        continue;
                    page.Text = GetPatientName();
                    page.Name = GetPatientName();
                    page.Tag = _patientListFrm.PatientId;
                    foreach (XtraTabPage page1 in (ctl as XtraTabControl).TabPages)
                    {
                        foreach (Control ctl1 in page1.Controls)
                        {
                            if (ctl1 is IBasePatient)
                                (ctl1 as IBasePatient).Patient_SelectionChanged(null,
                                new PatientEventArgs(GVars.Patient.ID, GVars.Patient.VisitId, GVars.Patient.STATE));
                        }
                    }
                }
                #endregion
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == null) return;
            var model = xtraTabControl1.SelectedTabPage.Tag as MenuManager;
            if (model == null)
            {
                if (xtraTabControl1.SelectedTabPage.Tag != null)
                    LocatePatient(xtraTabControl1.SelectedTabPage.Tag.ToString());
                navBarControl2.Show();
            }
            else if (model.ConnectionPatient == 0)
            {
                navBarControl2.OptionsNavPane.NavPaneState = NavPaneState.Collapsed;
            }
            else
            {
                navBarControl2.OptionsNavPane.NavPaneState = NavPaneState.Expanded;
            }

            PatientSelectionChanged();
            //PatientSelectionChanged(navBarControl2.SelectedLink);
        }

        private void navBarControl1_MouseEnter(object sender, EventArgs e)
        {
            var navBarControl = sender as NavBarControl;
            if (navBarControl != null) navBarControl.Focus();
            //navBarControl1.Focus();
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //defaultLookAndFeel1.LookAndFeel.SetSkinStyle(comboBoxEdit1.SelectedText);
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("确认注销吗?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _isRestart = true;
                base.Close();
                if (!base.Visible)
                {
                    Application.Restart();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            try
            {
                _currentTime = _currentTime.AddSeconds(1);
                lblTime.Text = _currentTime.ToString(ComConst.FMT_DATE.LONG);

                // 检查是否需要自动升级
                if (File.Exists(_updFlagFile) == true)
                {
                    timer1.Enabled = false;

                    MessageBox.Show("程序有更新, 需要升级! 请保存输入, 并退出!");
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
                timer1.Stop();
            }
        }

        private void repositoryItemProgressBar1_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            e.DisplayText = _inforMessage + @",已完成：" + e.Value;
        }

        private void btnSkin_Click(object sender, EventArgs e)
        {
            popupMenu2.ShowPopup(Control.MousePosition);
        }

        private void btnCard_Click(object sender, EventArgs e)
        {
            Form form = new PatientCard();
            MenuManager model = new MenuManager();

            CreateTabPage(form, model, 0);
        }

        private void ucButton1_Click(object sender, EventArgs e)
        {
            NurseSubmit f = new NurseSubmit();
            f.WindowState = FormWindowState.Maximized;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }

        #region 自动升级
        /// <summary>
        /// 检查是否需要升级
        /// </summary>
        private void checkNeedUpdate()
        {
            HISPlus.LogFile.WriteLog("checkNeedUpdate：" + DateTime.Now.ToString());
            try
            {
                //LB20110627添加，为了让检查打印线程快点执行,检查自动更新先暂停
                for (int i = 0; i < 10; i++)
                {
                    if (_exit) return;
                    Thread.Sleep(1000);
                }
                //LB20110627添加结束


                AutoUpdateWebSrv autoUpdSrv = new AutoUpdateWebSrv();
                IniFile ini = new IniFile(Path.Combine(Application.StartupPath, "AutoUpdate.ini"));

                string updFileName = Path.Combine(Application.StartupPath, "UpdFileList.xml");
                string appCode = ini.ReadString("SETTING", "APP_CODE", "").Trim();
                string serverIp = ini.ReadString("SETTING", "SERVER_IP", "").Trim();

                _updFlagFile = Path.Combine(Application.StartupPath, "UpdateFlag");
                _updateExe = ini.ReadString("SETTING", "EXE", "").Trim();

                if (_updateExe.Length > 0)
                {
                    _updateExe = Path.Combine(Application.StartupPath, _updateExe);
                }

                if (appCode.Length == 0 || serverIp.Length == 0)
                {
                    return;
                }

                autoUpdSrv.Url = ChangeIpInUrl(autoUpdSrv.Url, serverIp);
                DataSet ds = null;
                bool blnNeedUpdate = false;

                int _checkUpdateCount = 0;
                while (_checkUpdateCount < 5)
                {
                    //测试
                    HISPlus.LogFile.WriteLog("Web-CheckUpdated：" + DateTime.Now.ToString() + " " + _checkUpdateCount);

                    for (int i = 0; i < 10; i++)
                    {
                        if (_exit) return;
                        Thread.Sleep(1000);
                    }

                    try
                    {
                        if (File.Exists(updFileName) == false)
                        {
                            blnNeedUpdate = autoUpdSrv.CheckUpdated(appCode, null);

                        }
                        else
                        {
                            ds = new DataSet();
                            ds.ReadXml(updFileName, XmlReadMode.ReadSchema);
                            blnNeedUpdate = autoUpdSrv.CheckUpdated(appCode, ds);
                        }
                        if (blnNeedUpdate && File.Exists(_updFlagFile) == false)
                        {
                            File.Create(_updFlagFile);
                        }
                    }
                    catch (Exception ex)
                    {
                        //检测更新失败
                    }
                    finally
                    {
                        autoUpdSrv.Dispose();
                        _checkUpdateCount++;
                    }
                }
            }
            catch (Exception ex)
            { }
        }


        /// <summary>
        /// 改变Url中的主机地址
        /// </summary>
        /// <returns>新的URL</returns>
        static public string ChangeIpInUrl(string url, string newIp)
        {
            int pos = url.IndexOf("//");
            string header = url.Substring(0, pos + 2);

            pos = url.IndexOf("/", pos + 2);
            string tail = url.Substring(pos + 1);

            return header + newIp + "/" + tail;
        }
        #endregion

        private void MdiFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //for (int i = frmsName.Count - 1; i >= 0; i--)
            //{
            //    Form frm = frmsBackground[(String)frmsName[i]] as Form;

            //    if (frm == null) continue;
            //    frm.Close();
            //}

            _exit = true;

            if (File.Exists(_updFlagFile) == true && File.Exists(_updateExe) == true)
            {
                System.Diagnostics.Process.Start(_updateExe);
            }
        }

        private void MdiFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (GVars.App.QuestionExit == true && GVars.App.Verified == true)
                {
                    if (_isRestart)
                    {
                        _isRestart = false;
                        e.Cancel = true;
                        return;
                    }

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

    }
}