using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    
    public partial class ApplicationInfo : FormDo
    {
        #region 窗体变量
        public DataSet  DsApplicationList   = null;

        public string   AppCode             = string.Empty;
        public string   AppVersion          = string.Empty;
        
        public DataChanged NotifyDataChanged = null;
        
        private ApplicationMangerCom com    = null;
        #endregion


        #region 窗体事件
        public ApplicationInfo()
        {
            InitializeComponent();

            _id     = "00003";
            _guid   = "4be5e15d-4990-49bd-97d8-6b5fd8f04fa6";
        }


        private void ApplicationInfo_Load(object sender, EventArgs e)
        {
            try
            {
                com = new ApplicationMangerCom();

                RefreshShow();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

                
        private void txtItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                this.btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void btnNewApplication_Click(object sender, EventArgs e)
        {
            try
            {
                initDisp();

                this.txtAppCode.Focus();
                
                this.btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // 条件检查
                if (this.txtAppCode.Text.Trim().Length == 0)
                {
                    txtAppCode.Focus();
                    return;
                }

                // 询问
                if (GVars.Msg.Show("Q00004", "应用程序") != DialogResult.Yes)       // 您确认要删除当前{0}吗?
                {
                    return;
                }

                // 删除
                string filter = "APP_CODE = " + SqlManager.SqlConvert(txtAppCode.Text.Trim())
                              + "AND APP_VERSION = " + SqlManager.SqlConvert(txtVersion.Text.Trim());

                DataRow[] drFind = DsApplicationList.Tables[0].Select(filter);

                if (drFind.Length > 0)
                {
                    drFind[0].Delete();

                    com.SaveApplicationList(ref DsApplicationList);

                    // 刷新显示
                    DsApplicationList = com.GetApplicationList();

                    // 中间变量
                    AppCode = string.Empty;
                    AppVersion = string.Empty;
                                        
                    // 通知主窗体, 数据改变
                    NotifyDataChanged();
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkDisp() == false)
                {
                    return;
                }
                
                // 保存数据到缓存
                saveDisp();
                
                // 保存数扰到DB中
                com.SaveApplicationList(ref DsApplicationList);

                // 刷新显示
                DsApplicationList = com.GetApplicationList();
                
                // 设置中间变量
                AppCode = txtAppCode.Text.Trim();
                AppVersion = txtVersion.Text.Trim();

                // 通知主窗体, 数据改变
                NotifyDataChanged();

                this.btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion


        #region 接口
        public void RefreshShow()
        {
            // 初始化界面
            initDisp();

            // 定位记录
            string filter = "APP_CODE = " + SqlManager.SqlConvert(AppCode)
                      + "AND APP_VERSION = " + SqlManager.SqlConvert(AppVersion);
            
            DataRow[] drApps = DsApplicationList.Tables[0].Select(filter);

            // 显示应用程序详细信息
            if (drApps.Length > 0)
            {
                DataRow drApp = drApps[0];

                showApplicationInfo(drApp);
            }

            this.btnSave.Enabled = false;
        }
        #endregion


        #region 共通函数
        private void initDisp()
        {
            this.txtAppCode.Text        = string.Empty;         // 程序代码
            this.txtAppName.Text        = string.Empty;         // 程序名称
            this.txtAppTitle.Text       = string.Empty;         // 程序标题
            this.txtVersion.Text        = string.Empty;         // 版本
            this.txtGrants.Text         = string.Empty;         // 授权
            
            this.chkEnabled.Checked     = true;                 // 是否启用

            this.txtCreateDateTime.Text = string.Empty;         // 创建时间
            this.txtModifyDateTime.Text = string.Empty;         // 更新时间

            this.txtMemo.Text           = string.Empty;         // 备注
        }


        private void showApplicationInfo(DataRow drApp)
        {
            this.txtAppCode.Text        = drApp["APP_CODE"].ToString();                         // 程序代码
            this.txtAppName.Text        = drApp["APP_NAME"].ToString();                         // 程序
            this.txtAppTitle.Text       = drApp["APP_TITLE"].ToString();                        // 程序名称
            this.txtVersion.Text        = drApp["APP_VERSION"].ToString();                      // 版本
            this.txtGrants.Text         = drApp["GRANTS"].ToString();                           // 授权
            
            this.chkEnabled.Checked     = drApp["ENABLED"].ToString().Equals("1");              // 是否启用

            this.txtCreateDateTime.Text = drApp["CREATE_DATE"].ToString();                      // 创建时间
            this.txtModifyDateTime.Text = drApp["UPDATE_DATE"].ToString();                      // 更新时间

            this.txtMemo.Text           = drApp["MEMO"].ToString();                             // 备注
        }


        private bool chkDisp()
        {
            if (this.txtAppCode.Text.Trim().Length == 0)
            {
                txtAppCode.Focus();
                return false;
            }

            if (this.txtAppName.Text.Trim().Length == 0)
            {
                txtAppName.Focus();
                return false;
            }

            if (this.txtAppTitle.Text.Trim().Length == 0)
            {
                txtAppTitle.Focus();
                return false;
            }

            return true;
        }


        private bool saveDisp()
        {
            // 获取当前日期
            DateTime dtNow = GVars.OracleAccess.GetSysDate();

            // 查找记录
            string filter = "APP_CODE = " + SqlManager.SqlConvert(txtAppCode.Text.Trim())
                          + "AND APP_VERSION = " + SqlManager.SqlConvert(txtVersion.Text.Trim());

            DataRow[] drFind = DsApplicationList.Tables[0].Select(filter);

            DataRow drEdit = null;
            if (drFind.Length == 0)
            {
                drEdit = DsApplicationList.Tables[0].NewRow();
            }
            else
            {
                drEdit = drFind[0];
            }

            // 保存数据
            drEdit["APP_CODE"]      = this.txtAppCode.Text.Trim();                              // 程序代码
            drEdit["APP_NAME"]      = this.txtAppName.Text.Trim();                              // 程序名称
            drEdit["APP_TITLE"]     = this.txtAppTitle.Text.Trim();                             // 称序标题
            drEdit["APP_VERSION"]   = this.txtVersion.Text.Trim();                              // 版本
            drEdit["GRANTS"]        = this.txtGrants.Text.Trim();                               // 授权码
            drEdit["ENABLED"]       = (this.chkEnabled.Checked? "1": "0");;                     // 是否启用
            drEdit["MEMO"]          = this.txtMemo.Text.Trim();                                 // 备注

            if (drFind.Length == 0)
            {
                drEdit["CREATE_DATE"]   = dtNow;                                                // 创建时间
                DsApplicationList.Tables[0].Rows.Add(drEdit);
            }

            drEdit["UPDATE_DATE"]       = dtNow;                                                // 更新时间
            
            return true;
        }
        #endregion
    }
}
