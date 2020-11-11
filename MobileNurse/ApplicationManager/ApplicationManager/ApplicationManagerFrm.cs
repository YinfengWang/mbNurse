using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTab;

namespace HISPlus
{
    public partial class ApplicationManagerFrm : FormDo
    {
        #region 窗体变量
        private ApplicationMangerCom com = null;
        private DataSet dsApplicationList = null;

        private Form appInfo = null;                                 // 应用程序信息
        private Form appParams = null;                                 // 应用程序参数
        private Form appModules = null;                                 // 应用程序模块
        #endregion


        public ApplicationManagerFrm()
        {
            InitializeComponent();

            _id = "00002";
            _guid = "6d3811eb-6517-4616-a201-e2ebef532dcc";
        }


        #region 窗体事件
        private void ApplicationManagerFrm_Load(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;

                initFrmVal();
                initDisp();

                GVars.App.UserInput = blnStore;
                ucGridView1_SelectionChanged(sender, e);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }


        private void ApplicationManagerFrm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        #endregion


        #region DataGridView控件 事件

        private void ucGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                // 显示应用程序详细信息              
                DataRow dr = ucGridView1.SelectedRow;

                string appCode = dr["APP_CODE"].ToString();
                string appVersion = dr["APP_VERSION"].ToString();

                ApplicationInfo appDetail = (ApplicationInfo)appInfo;
                appDetail.AppCode = appCode;
                appDetail.AppVersion = appVersion;

                appDetail.RefreshShow();

                // 显示应用程序参数
                ApplicationParams showParamDetail = (ApplicationParams)appParams;
                showParamDetail.AppCode = appCode;

                showParamDetail.RefreshShow();

                //// 显示程序模块
                //ApplicationModuleFrm appModuleTemp = (ApplicationModuleFrm)appModules;
                //appModuleTemp.AppCode = appCode;
                //drSelected.Cells["APP_TITLE"].Value.ToString();
                //appModuleTemp.RefreshShow();
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
        #endregion


        #region 共通函数
        private void initFrmVal()
        {
            DataChanged treateDataChanged = new DataChanged(this.dataChanged);

            com = new ApplicationMangerCom();

            dsApplicationList = com.GetApplicationList();

            // 应用程序详细信息
            appInfo = new ApplicationInfo();
            ApplicationInfo appDetail = (ApplicationInfo)appInfo;
            appDetail.DsApplicationList = dsApplicationList;
            appDetail.NotifyDataChanged = treateDataChanged;

            // 应用程序参数设置
            appParams = new ApplicationParams();

            // 应用程序模块
            appModules = new ApplicationModuleFrm();

            ucGridView1.ShowRowIndicator = true;

            ucGridView1.Add("编码", "APP_CODE",  false);
            ucGridView1.Add("版本号", "APP_VERSION", false);
            ucGridView1.Add("程序", "APP_NAME");
            ucGridView1.Add("程序名称", "APP_TITLE");

            ucGridView1.Init();
        }


        private void initDisp()
        {
            CreateTabPage(appInfo);
            CreateTabPage(appParams);

            //openWindowInPanels(ref appInfo);                            // 应用程序详细信息
            //openWindowInPanels(ref appParams);                          // 应用程序参数设置
            //openWindowInPanels(ref appModules);                         // 应用程序模块

            tabControl_WindowList.SelectedIndex = 0;

            dsApplicationList.Tables[0].DefaultView.Sort = "APP_CODE, APP_VERSION";
            ucGridView1.DataSource = dsApplicationList.Tables[0].DefaultView;
        }


        private void locate(string appCode, string appVersion)
        {
            // 定位
            if (appCode.Length > 0)
            {
                ucGridView1.SelectRow("APP_CODE", appCode);
                //foreach (DataGridViewRow dr in dgvApplication.Rows)
                //{
                //    if (dr.Cells["APP_CODE"].Value.ToString().Equals(appCode) == true
                //        && dr.Cells["APP_VERSION"].Value.ToString().Equals(appVersion) == true)
                //    {
                //        dr.Selected = true;
                //        break;
                //    }
                //}
            }
        }


        private void dataChanged()
        {
            try
            {
                ApplicationInfo appDetail = (ApplicationInfo)appInfo;
                string appCode = appDetail.AppCode;
                string appVersion = appDetail.AppVersion;

                // 重新获取数据
                dsApplicationList = com.GetApplicationList();

                dsApplicationList.Tables[0].DefaultView.Sort = "APP_CODE, APP_VERSION";
                ucGridView1.DataSource = dsApplicationList.Tables[0].DefaultView;

                // 定位当前数据行
                locate(appCode, appVersion);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
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
        ///     创建选项卡页
        /// </summary>
        /// <param name="form"></param>
        public void CreateTabPage(Form form)
        {
            if (form == null) return;
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
                
                var page = new XtraTabPage
                {
                    Text = form.Text,
                    //Image = image,
                    Name = form.GetType() + form.Text
                };

                page.AutoScroll = true;

                xtraTabControl1.TabPages.Add(page);
                xtraTabControl1.SelectedTabPage = page;
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
        /// 在Panels中打开窗口
        /// </summary>
        private void openWindowInPanels(ref Form frm)
        {
            TabPage tabPage = null;

            // 判断窗口是否存在
            for (int i = 0; i < tabControl_WindowList.TabPages.Count; i++)
            {
                tabPage = tabControl_WindowList.TabPages[i];

                if (tabPage.Text.Equals(frm.Text) == true)
                {
                    tabControl_WindowList.SelectTab(i);
                    return;
                }
            }

            // TabControl 标签头 [图标] + 窗口名称
            // imageListPanels.Images.Add(frm.Icon);

            int preCount = tabControl_WindowList.TabPages.Count;
            this.tabControl_WindowList.TabPages.Add(preCount.ToString(), frm.Text);//, imageListPanels.Images.Count -1);

            tabPage = tabControl_WindowList.TabPages[preCount];

            frm.FormBorderStyle = FormBorderStyle.None;
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            frm.Visible = true;

            tabPage.Controls.Add(frm);

            // 显示当前TabPage
            tabControl_WindowList.SelectTab(tabControl_WindowList.TabPages.Count - 1);
        }
        #endregion
    }
}
