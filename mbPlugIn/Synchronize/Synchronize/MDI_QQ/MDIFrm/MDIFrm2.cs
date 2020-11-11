using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Resources;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraNavBar;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
//using TabControlTJ = TJ.CHSIS.TabControlTJ;

namespace HISPlus
{
    public partial class MDIFrm2 : FormDo
    {
        #region 窗体变量
        private string                  BACKGROUND_FRM  = "background";
        
        private TabControlTJ            tabControl_WindowList = new TabControlTJ();
        private Hashtable               frmsBackground  = new Hashtable(3);                     // 后台窗体列表
        private ArrayList               frmsName        = new ArrayList(3);                     // 后台窗体名列表

        
        private Hashtable               rightsControl   = new Hashtable(17);                    // 权限控制
        //private OutlookBarFrm           outlookBarFrm   = new OutlookBarFrm();
        private DataSet                 dsMenu          = null;                                 // 菜单
        
        private string                  defaultWndDllName = string.Empty;                       // 默认打开窗体
        private string                  defaultWndName    = string.Empty;                       // 默认打开窗体
        
        private string                  hideNavigator     = string.Empty;                       // 是否隐藏主窗体的导航栏

        
        private ResourceManager         rm              = null;                                 // 资源管理器

        private string wardCodes = string.Empty;
        #endregion


        public MDIFrm2()
        {
            InitializeComponent();

            _id     = "0007";
            _version= "1.0";
            _guid   = "5cd33f94-e88d-4053-abef-720ae07f1bf3";            
            _right  = string.Empty;

            this.Resize += new EventHandler(MDIFrm_Resize);
            this.FormClosing += new FormClosingEventHandler(MDIMainForm_FormClosing);
            this.FormClosed += new FormClosedEventHandler( MDIMainForm_FormClosed );

            this.splitContainerMain.SplitterMoving += new SplitterCancelEventHandler(splitContainerMain_SplitterMoving);
            
            this.tabControl_WindowList.MouseDoubleClick += new MouseEventHandler(tabControl_WindowList_MouseDoubleClick);
        }
        
        
        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MDIFrm_Load(object sender, EventArgs e)
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

                // 加载控件
                this.splitContainerMain.Panel2.Controls.Add(tabControl_WindowList);
                tabControl_WindowList.Location = new Point(12, 12);
                tabControl_WindowList.Size     = new Size(693, 355);
                tabControl_WindowList.ItemSize = new Size(0, 32);
                tabControl_WindowList.ImageList = imageListPanels;
                tabControl_WindowList.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                
                //// 加载背景图
                //LoadBackgroudPics();
                
                //CreateMenu();
                ////分析病区代码是否有重复，如果有重复抛出异常
                //isRightWardCodes();
                //initFrmVal();
                //initDisp();
                
                //startDefaultWnd();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void isRightWardCodes()
        {
            string[] wardCodesShuZu = wardCodes.Split(',');
            for (int i = 0; i < wardCodesShuZu.Length; i++)
            {
                for(int j=i+1;j<wardCodesShuZu.Length;j++)
                {
                    if (wardCodesShuZu[i] == wardCodesShuZu[j])
                    {
                        throw new Exception("list.ini文件中存在重复的病区代码:"+wardCodesShuZu[i]);
                    }
                }
            }
        }
        /// <summary>
        /// 窗体缩放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MDIFrm_Resize(object sender, EventArgs e)
        {
            // outlookBarFrm.OutlookBarFrm_Resize(sender, e);
        }
        
        
        /// <summary>
        /// 分隔栏移动事件

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void splitContainerMain_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            MDIFrm_Resize(sender, e);
        }


        /// <summary>
        /// Tabcontrol双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tabControl_WindowList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                GVars.App.UserInput = false;
                
                Point pt = new Point(e.X, e.Y);
                
                for (int i = 0; i < tabControl_WindowList.TabCount; i++)
                {
                    Rectangle recTab = tabControl_WindowList.GetTabRect(i);

                    if (recTab.Contains(pt))
                    {
                        TabPage selTab = tabControl_WindowList.SelectedTab;
                        
                        // 删除当前Tab页中的窗体

                        if (selTab.Tag == null || selTab.Tag.ToString().Equals(BACKGROUND_FRM) == false)
                        {
                            foreach(Control ctrl in selTab.Controls)
                            {
                                Form frm = ctrl as Form;
                                if (frm != null)
                                {
                                    frm.Close();
                                }
                            }
                        }
                        
                        // 删除当前Tab页中的窗体                    
                        int selTabIndex = tabControl_WindowList.SelectedIndex;
                        
                        if (selTab.Tag.ToString().Equals(BACKGROUND_FRM) == true)
                        {
                            selTab.Controls.Clear();
                        }
                        
                        // 删除当前Tab页

                        tabControl_WindowList.Controls.Remove(selTab);
                        
                        // 显示下一个Tab页

                        if (selTabIndex > 0)
                        {
                            tabControl_WindowList.SelectTab(selTabIndex - 1);
                        }

                        // 如果没有Tabpage, 显示背景图

                        if (tabControl_WindowList.TabPages.Count == 0)
                        {
                            //tabControl_WindowList.Visible = false;
                            picBackgroud.BringToFront();
                            
                            imageListPanels.Images.Clear();
                        }

                        return;
                    }
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }


        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tabPage_ControlRemoved(object sender, ControlEventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                TabPage selTab = tabControl_WindowList.SelectedTab;
                
                // 删除当前Tab页中的窗体

                if (selTab.Tag == null || selTab.Tag.ToString().Equals(BACKGROUND_FRM) == false)
                {
                    foreach(Control ctrl in selTab.Controls)
                    {
                        Form frm = ctrl as Form;
                        if (frm != null)
                        {
                            frm.Close();
                        }
                    }
                }
                
                // 删除当前Tab页中的窗体                    
                int selTabIndex = tabControl_WindowList.SelectedIndex;
                
                if (selTab.Tag.ToString().Equals(BACKGROUND_FRM) == true)
                {
                    selTab.Controls.RemoveAt(0);
                }
                
                // 删除当前Tab页

                tabControl_WindowList.Controls.Remove(selTab);
                
                // 显示下一个Tab页

                if (selTabIndex > 0)
                {
                    tabControl_WindowList.SelectTab(selTabIndex - 1);
                }

                // 如果没有Tabpage, 显示背景图

                if (tabControl_WindowList.TabPages.Count == 0)
                {
                    //tabControl_WindowList.Visible = false;
                    picBackgroud.BringToFront();
                    
                    imageListPanels.Images.Clear();
                }
                
                // 第一个Tab的文字增加四个空格

                if (tabControl_WindowList.TabCount > 0)
                {
                    tabControl_WindowList.TabPages[0].Text = tabControl_WindowList.TabPages[0].Text.TrimEnd() + "    ";
                }
            }
            catch(Exception ex)
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
        void MDIMainForm_FormClosed( object sender, FormClosedEventArgs e )
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
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 时钟消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick( object sender, EventArgs e )
        {
            try
            {   
                lblDate.Text = DateTime.Now.ToString(ComConst.FMT_DATE.LONG_MINUTE);
            }
            catch
            {
            }
        }
        #endregion
        
        
        #region 工具栏

        private void toolStripBtn_Exit_Click(object sender, EventArgs e)
        {
            ExitToolsStripMenuItem_Click(sender, e);
        }


        private void toolStripBtn_SwitchUser_Click(object sender, EventArgs e)
        {
            mnuSys_SwitchUser_Click(sender, e);
        }

        
        private void toolStripBtn_SwitchOutloolbar_Click(object sender, EventArgs e)
        {
            // 如果是隐藏

            if (splitContainerMain.Tag == null || int.Parse(splitContainerMain.Tag.ToString()) == 0)
            {
                splitContainerMain.Tag = splitContainerMain.SplitterDistance;

                splitContainerMain.SplitterDistance = 0;
            }
            // 如果是显示

            else
            { 
                splitContainerMain.SplitterDistance = int.Parse(splitContainerMain.Tag.ToString());
                splitContainerMain.Tag = 0;
            }
        }
        #endregion
        
        
        #region 菜单事件
        /// <summary>
        /// 菜单[系统] -> [修改密码]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSys_Changepwd_Click(object sender, EventArgs e)
        {
            try
            {
                string dllName = GVars.IniFile.ReadString("CHANGE_PWD", "DLL_NAME", string.Empty);
                string frmName = GVars.IniFile.ReadString("CHANGE_PWD", "FORM_NAME", string.Empty);

                if (dllName.Length > 0 && frmName.Length > 0)
                {
                    Form frmShow = DllOperator.GetFormInDll(dllName, frmName);
                    frmShow.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 菜单[系统] -> [切换用户]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSys_SwitchUser_Click(object sender, EventArgs e)
        {
            try
            {
                // 切换用户
                string dllName = GVars.IniFile.ReadString("SWITCH_USER", "DLL_NAME", string.Empty);
                string frmName = GVars.IniFile.ReadString("SWITCH_USER", "FORM_NAME", string.Empty);

                if (dllName.Length > 0 && frmName.Length > 0)
                {
                    dllName = Path.Combine(Application.StartupPath, dllName);

                    Form frmShow = DllOperator.GetFormInDll(dllName, frmName);
                    frmShow.ShowDialog();
                }

                // 删除当前Tab页

                while (tabControl_WindowList.TabCount > 0)
                {
                    TabPage selTab = tabControl_WindowList.SelectedTab;
                    int selTabIndex = tabControl_WindowList.SelectedIndex;

                    tabControl_WindowList.Controls.Remove(selTab);
                }
                
                //tabControl_WindowList.Visible = false;
                picBackgroud.BringToFront();
                
                // 初始化界面显示

                imageListPanels.Images.Clear();

                initDisp();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 菜单[系统] -> [退出]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
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
        /// 菜单[视图] -> [工具栏]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // toolStrip.Visible = mnuView_Toolbar.Checked;
        }


        /// <summary>
        /// 菜单[视图] -> [状态栏]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // statusStrip.Visible = mnuView_StatusBar.Checked;
        }       

        
        /// <summary>
        /// 菜单[帮助] -> [关于]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuHelp_About_Click(object sender, EventArgs e)
        {
            try
            {
                frmAbout frmAbout = new frmAbout();
                
                frmAbout.Title      = GVars.App.Title;
                frmAbout.CopyRight  = GVars.App.CopyRight;
                frmAbout.Version    = GVars.App.Version;

                frmAbout.ShowDialog();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion
        
        
        #region 共通函数

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
                    hideNavigator       = parts[0].Trim();
                    defaultWndName      = parts[1].Trim();
                    defaultWndDllName   = parts[2].Trim();
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
            
            lblUser.Text        = GVars.User.Name;
            lblDeptName.Text    = GVars.User.DeptName;
            lblDate.Text        = DateTime.Now.ToString(ComConst.FMT_DATE.LONG_MINUTE);
            
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
                     
            createlblMenuFromMenu();

            menuStrip_Function.Visible = false;
            // 导航栏

            if (hideNavigator.Equals("1") == true)
            {
                lblAction_Click(null, null);
            }            
        }
        

        /// <summary>
        /// 在Panels中打开窗口
        /// </summary>
        private void openWindowInPanels(ref Form frm, string tag, Image iconImage)
        { 
            TabPage tabPage = null;

            // 隐藏背景图

            //tabControl_WindowList.Visible = true;
            picBackgroud.SendToBack();
            
            // 判断窗口是否存在
            for(int i = 0; i < tabControl_WindowList.TabPages.Count; i++)
            {
                tabPage = tabControl_WindowList.TabPages[i];

                if (tabPage.Text.Trim().Equals(frm.Text.Trim()) == true)
                {
                    tabControl_WindowList.SelectTab(i);
                    return;
                }
            }

            // TabControl 标签头 [图标] + 窗口名称
            int preCount = tabControl_WindowList.TabPages.Count;
            
            if (iconImage != null)
            {
                imageListPanels.Images.Add(iconImage);
                this.tabControl_WindowList.TabPages.Add(preCount.ToString(), frm.Text, imageListPanels.Images.Count -1);
            }
            else
            {
                this.tabControl_WindowList.TabPages.Add(preCount.ToString(), frm.Text);
            }
            
            tabPage = tabControl_WindowList.TabPages[preCount];
            
            // MOD BEGIN BY FUJUN 2009-01-15
            //DecorateFrm holderFrm = new DecorateFrm(frm);
            
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.TopLevel        = false;
            frm.Dock            = DockStyle.Fill;
            frm.Visible         = true;
                        
            tabPage.Controls.Add(frm);
            
            //holderFrm.FormBorderStyle = FormBorderStyle.None;
            //holderFrm.TopLevel        = false;
            //holderFrm.Dock            = DockStyle.Fill;
            //holderFrm.Visible         = true;
            
            //tabPage.Controls.Add(holderFrm);
            // MOD END BY FUJUN 2009-01-15
            
            tabPage.Tag = tag;

            // 显示当前TabPage
            tabControl_WindowList.SelectTab(tabControl_WindowList.TabPages.Count - 1);

            tabPage.ControlRemoved += new ControlEventHandler(tabPage_ControlRemoved);
            
            // 第一个Tab页增加四个空格

            if (tabControl_WindowList.TabCount > 0)
            {
                tabControl_WindowList.TabPages[0].Text = tabControl_WindowList.TabPages[0].Text.TrimEnd() + "    ";
            }            
        }
        
        
        /// <summary>
        /// 后台启动窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startWindowBackground(ToolStripMenuItem mnuItem)
        {
            DataView dv = new DataView();
            dv.Table = dsMenu.Tables[0];
            dv.RowFilter = "NodeId= " + SqlManager.SqlConvert(mnuItem.Tag.ToString());

            try
            {
                if (dv.Count > 0)
                {
                    DataRowView drv = dv[0];

                    string dllName = drv["AddDll"].ToString();
                    string frmName = drv["Address"].ToString();

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


        /// <summary>
        /// 获取后台窗体的提示消息

        /// </summary>
        /// <returns></returns>
        private string getBackgroundFrmMsg()
        { 
            string msg = string.Empty;

            for (int i = 0; i < frmsName.Count; i++)
            {
                Form frm = (Form)(frmsBackground[(String)frmsName[i]]);
                if (frm.Tag != null && frm.Tag.ToString().Length > 0)
                {
                    msg += frm.Tag.ToString() + ComConst.STR.SEMICOLON;
                }
            }

            return msg;
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
        #endregion
        
        
        #region 共通函数 菜单
        /// <summary>
        /// 创建菜单
        /// </summary>
        private void CreateMenu()
        {
            // 清除 菜单内容
            menuStrip_Function.Items.Clear();
            rightsControl.Clear();
            
            // 加载 菜单设置
            dsMenu = new DataSet();
            string mnuFile = getFileInCurrDir("Menu.xml");            
            dsMenu.ReadXml(mnuFile);
            
            // 加载 菜单
            DataView dv = new DataView();
            dv.Table = dsMenu.Tables[0];
            dv.Sort = "ParentId, SERIAL_NO";
            dv.RowFilter = "ParentId = '0'";                            // 主菜单

            
            foreach (DataRowView drv in dv)
            {
                CreateMenuItem(drv["NodeName"].ToString(), drv["Address"].ToString(),
                           drv["NodeId"].ToString(), drv["Icon"].ToString());
                
            }
            
            // 删除菜单文件
            //File.Delete(mnuFile);
        }
        
        
        /// <summary>
        /// 创建菜单项

        /// </summary>
        /// <param name="drv"></param>
        /// <param name="ParentId"></param>
        private void CreateMenuItem(string mnuName, string typeName, string nodeId, string iconFile)
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
            rightsControl.Add(nodeId, topMenu);

            // 查找子菜单

            DataView dv = new DataView();
            dv.Table = dsMenu.Tables[0];
            
            dv.Sort = "ParentId, SERIAL_NO";
            dv.RowFilter = "ParentId = " + SqlManager.SqlConvert(nodeId);
            
            foreach (DataRowView drv in dv)
            {
                //LB20110712,根据sql文件夹下面配置的list.ini的名称来判断是否显示该菜单
                string Num = drv["NodeName"].ToString().Substring(drv["NodeName"].ToString().Length - 1);
                    int i=-1;
                    if (int.TryParse(Num, out i))//是数字的话
                    {
                        // 判断文件是否存在，以确定该菜单是否显示
                        string fileName = Path.Combine(Path.Combine(Application.StartupPath, "SQL"), "List" + i.ToString() + ".INI");
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

                ToolStripMenuItem subMenu = new ToolStripMenuItem(drv["NodeName"].ToString(), null, new EventHandler(Menu_Click));

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
                rightsControl.Add(subMenu.Tag.ToString(), subMenu);

                // 如果是后台窗体
                if (drv["Background"].ToString().Equals("1") == true)
                {
                    startWindowBackground(subMenu);
                }
            }
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
        /// 菜单 处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem mnuItem = ((ToolStripMenuItem)sender);
            
            string nodeId = mnuItem.Tag.ToString();
            
            DataView dv = new DataView();
            dv.Table = dsMenu.Tables[0];
            dv.RowFilter = "NodeId= " + SqlManager.SqlConvert(nodeId);
            
            try
            {
                // 条件检查

                if (dv.Count == 0)
                {
                    return;
                }
                
                DataRowView drv = dv[0];

                string dllName = getFileInCurrDir(drv["AddDll"].ToString());
                string frmName = drv["Address"].ToString();
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
                Form frmShow = DllOperator.GetFormInDll(dllName, frmName);
                frmShow.BackColor  = Color.FromArgb(244, 249, 253);
                frmShow.Tag = parameter;
                
                if (frmShow is FormDo)
                {
                    FormDo frmShowDo = (FormDo)frmShow;
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
                }

                frmShow.Text = mnuItem.Text;
                
                switch(drv["WIN_OPEN_MODE"].ToString())
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
                        }
                        else
                        {
                            openWindowInPanels(ref frmShow, string.Empty, mnuItem.Image);
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
        /// 获取当前dll的路径

        /// </summary>
        /// <returns></returns>
        private string getDllPath()
        {
            string dllFile = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            int    length  = dllFile.LastIndexOf("/"); 
            return dllFile.Substring(8, length - 8);
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
        #endregion

        
        /// <summary>
        /// 伸缩栏

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblAction_Click(object sender, EventArgs e)
        {
            if (lblAction.Tag.Equals("<<") == true)
            {
                lblAction.Tag = ">>";
                splitContainerMain.Panel1.Tag = splitContainerMain.Panel1.Width;
                splitContainerMain.SplitterDistance = lblAction.Width;
                lblAction.Image = (Image)rm.GetObject("arrowR");  
            }
            else
            {
                lblAction.Tag = "<<";
                splitContainerMain.SplitterDistance = (int)splitContainerMain.Panel1.Tag;
                lblAction.Image = (Image)rm.GetObject("arrowL");  
            }
        }
        
        
        /// <summary>
        /// 打开默认窗体
        /// </summary>
        private void startDefaultWnd()
        {
            // 条件判断
            if (dsMenu == null || defaultWndName.Length == 0)
            {
                return;
            }
            
            // 查找节点ID
            string filter = "Address = " + SqlManager.SqlConvert(defaultWndName)
                            + "AND ADDDLL = " + SqlManager.SqlConvert(defaultWndDllName);
            
            DataRow[] drFind = dsMenu.Tables[0].Select(filter);
            if (drFind.Length == 0)
            {
                return;
            }
            
            string nodeId = drFind[0]["NodeId"].ToString();
            
            // 查找菜单 并模拟点击

            ToolStripMenuItem mnu = (ToolStripMenuItem)rightsControl[nodeId];
            if (mnu.Enabled == true)
            {
                Menu_Click(mnu, null);
            }
        }

        
        /// <summary>
        /// 按钮[退出]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[关闭所有窗体]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblCloseWindows_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                foreach(TabPage selTab in tabControl_WindowList.TabPages)
                {
                    // 删除当前Tab页中的窗体

                    if (selTab.Tag == null || selTab.Tag.ToString().Equals(BACKGROUND_FRM) == false)
                    {
                        foreach(Control ctrl in selTab.Controls)
                        {
                            Form frm = ctrl as Form;
                            if (frm != null)
                            {
                                frm.Close();
                            }
                        }
                    }
                    
                    // 删除当前Tab页中的窗体                    
                    int selTabIndex = tabControl_WindowList.SelectedIndex;
                    
                    if (selTab.Tag.ToString().Equals(BACKGROUND_FRM) == true)
                    {
                        selTab.Controls.RemoveAt(0);
                    }
                    
                    // 删除当前Tab页

                    tabControl_WindowList.Controls.Remove(selTab);
                }
                
                //tabControl_WindowList.Visible = false;
                picBackgroud.BringToFront();
                imageListPanels.Images.Clear();                
            }
            catch(Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        
        /// <summary>
        /// 加载背景图

        /// </summary>
        public void LoadBackgroudPics()
        {
            string fileName = string.Empty;
                        
            // 顶部1
            if (pnlHeader.Tag != null)
            {
                fileName = Path.Combine(getDllPath(), pnlHeader.Tag.ToString());
                if (File.Exists(fileName) == true)
                {
                    pnlHeader.BackgroundImage = Image.FromFile(fileName);
                }
            }
            
            // 顶部2
            if (pnlHeader2.Tag != null)
            {
                fileName = Path.Combine(getDllPath(), pnlHeader2.Tag.ToString());
                if (File.Exists(fileName) == true)
                {
                    pnlHeader2.BackgroundImage = Image.FromFile(fileName);
                }
            }
            
            // 工作区

            if (picBackgroud.Tag != null)
            {
                fileName = Path.Combine(getDllPath(), picBackgroud.Tag.ToString());
                if (File.Exists(fileName) == true)
                {
                    picBackgroud.Image = Image.FromFile(fileName);
                }
            }
            
            // 导航栏

            if (splitContainerMain.Panel1.Tag != null)
            {
                fileName = Path.Combine(getDllPath(), splitContainerMain.Panel1.Tag.ToString());
                if (File.Exists(fileName) == true)
                {
                    splitContainerMain.Panel1.BackgroundImage = Image.FromFile(fileName);
                }
            }
        }

        /// <summary>
        /// 动态创建功能区页面
        /// </summary>
        /// <param name="parentId">父级节点（顶级节点为0）</param>
        /// <param name="id">当前节点编号</param>
        /// <param name="title">节点标题</param>
        /// <param name="imageFile">图标文件名</param>
        private void CreatePage(string parentId, string id, string title, string imageFile)
        {
            if (parentId != "0")
                return;
            RibbonPage page = new RibbonPage(title) { Image = Image.FromFile(GetIconFile(imageFile)) };
            ribbonControl.Pages.Add(page);


            RibbonPageGroup pageGroup = new RibbonPageGroup(title)
            {
                AllowTextClipping = false,
                ShowCaptionButton = false,
                Text = title,
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

            BarButtonItem button = new BarButtonItem(ribbonControl.Manager, functionName) { Caption = functionName, Name = functionName };

            //if (form != null)
            {
                button.Tag = form;
                button.ItemClick += button_ItemClick;
            }
            // 按钮图片
            button.Glyph = Image.FromFile(GetIconFile(imageFile));

            // 按钮风格【下拉框/复选框/按钮】
            button.ButtonStyle = BarButtonStyle.Default;
            // 显示风格【大图标/小图标】
            button.RibbonStyle = RibbonItemStyles.Large;
            button.SmallWithTextWidth = 150;
            pageGroup.ItemLinks.Add(button, true, functionName);

            // 快速访问工具栏
            //this.ribbonControl.Toolbar.ItemLinks.Add(button);

            //if (!_rightsControl.ContainsKey(nodeId))
            //    _rightsControl.Add(nodeId, button);
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
                form.TopLevel = false;
                form.Dock = DockStyle.Fill;
                form.AutoScroll = true;
                form.Visible = true;

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

        /// <summary>
        /// 从Menu菜单中创建OutlookBar的主菜单按钮
        /// </summary>
        /// <param name="mnuStrip"></param>
        private void createlblMenuFromMenu()
        {
            for (int i = 0; i < menuStrip_Function.Items.Count; i++)
            {
                ToolStripItem mnuItem = menuStrip_Function.Items[i];

                if (mnuItem.Enabled == false){
                    continue;
                }

                NavBarItem item = new NavBarItem();

                item.Caption = mnuItem.Text;
                item.LargeImage = mnuItem.Image;

                item.Tag = mnuItem;
                item.LinkClicked += new NavBarLinkEventHandler(item_LinkClicked);

                navBarControl1.Items.Add(item);
            }
        }


        void button_ItemClick(object sender, ItemClickEventArgs e)
        {
            BarButtonItem button = e.Item as BarButtonItem;

            if (button != null)
            {
                Form form = button.Tag as Form;
                if (form != null)
                    CreateTabPage(form, button.Glyph);
            }
        }


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

        private void xtraTabControl1_CloseButtonClick_1(object sender, EventArgs e)
        {
            ClosePageButtonEventArgs a = (ClosePageButtonEventArgs)e;

            TabPageClose((XtraTabPage)a.Page);
        }   
    }
}
