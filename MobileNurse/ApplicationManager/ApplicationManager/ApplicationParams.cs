using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HISPlus.UserControls;

namespace HISPlus
{
    public partial class ApplicationParams : FormDo
    {
        #region 窗体变量
        public string AppCode = string.Empty;

        private string paramId = string.Empty;
        private ApplicationMangerCom com = null;
        private DataSet dsParams = null;
        #endregion

        public ApplicationParams()
        {
            InitializeComponent();

            _id = "00004";
            _guid = "a141a1e9-8030-4e9a-af9b-68f7d8fc66a5";
        }


        #region 窗体事件
        private void ApplicationParams_Load(object sender, EventArgs e)
        {
            try
            {
                initFrmVal();
                initDisp();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void dgvParameters_SelectionChanged(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                GVars.App.UserInput = false;    // 为了避免 触发文本框编辑事件

                // 初始化 应用程序参数信息的显示
                initDisp_ParamDetail();


                DataRow drParam = ucGridView1.SelectedRow;
                showParamDetail(drParam);

                this.btnSave.Enabled = false;
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


        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                paramId = string.Empty;

                initDisp_ParamDetail();

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
                if (txtParamName.Text.Trim().Length == 0)
                {
                    txtParamName.Focus();
                    return;
                }

                // 询问
                if (GVars.Msg.Show("Q0004", "参数") != DialogResult.Yes)       // 您确认要删除当前{0}吗?
                {
                    return;
                }

                // 删除
                initDisp_ParamDetail();

                if (paramId.Length > 0)
                {
                    DataRow[] drFind = dsParams.Tables[0].Select("PARAMETER_ID = " + paramId);

                    if (drFind.Length > 0)
                    {
                        drFind[0].Delete();

                        com.SaveApplicationParams(ref dsParams, AppCode);
                        paramId = string.Empty;

                        // 刷新显示
                        dsParams = com.GetApplicationParams(AppCode);
                        dsParams.Tables[0].DefaultView.Sort = "APP_CODE, DEPT_CODE, USER_ID, PARAMETER_CLASS,PARAMETER_CLASS_SUB, PARAMETER_NAME";
                        ucGridView1.DataSource = dsParams.Tables[0].DefaultView;
                    }
                }

                this.btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;

                if (chkDisp() == false)
                {
                    return;
                }

                if (paramId.Length == 0)
                {
                    paramId = com.GetNewParamId();
                }

                string paramIdTemp = paramId;

                // 保存数据到缓存
                saveDisp();

                // 保存数扰到DB中
                DataSet dsChanged = dsParams.GetChanges();
                com.SaveApplicationParams(ref dsChanged, AppCode);

                // 刷新显示
                GVars.App.UserInput = blnStore;

                dsParams = com.GetApplicationParams(AppCode);
                dsParams.Tables[0].DefaultView.Sort = "APP_CODE, DEPT_CODE, USER_ID, PARAMETER_CLASS,PARAMETER_CLASS_SUB, PARAMETER_NAME";
                ucGridView1.DataSource = dsParams.Tables[0].DefaultView;

                // 定位当前行
                paramId = paramIdTemp;
                locate(paramId);

                this.btnSave.Enabled = false;
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
        #endregion


        #region 接口
        public void RefreshShow()
        {
            // 初始化界面
            dsParams = com.GetApplicationParams(AppCode);
            initDisp();

            // 定位记录
            if (paramId.Length > 0)
            {
                string filter = "PARAMETER_ID = " + paramId;

                DataRow[] drParams = dsParams.Tables[0].Select(filter);

                // 显示应用程序详细信息
                if (drParams.Length > 0)
                {
                    DataRow drParam = drParams[0];

                    showParamDetail(drParam);
                }
            }

            this.btnSave.Enabled = false;
        }
        #endregion


        #region 共通函数
        private void initFrmVal()
        {
            com = new ApplicationMangerCom();

            dsParams = com.GetApplicationParams(AppCode);

            ucGridView1.ShowRowIndicator = true;
            ucGridView1.Add("参数ID", "PARAMETER_ID", false);
            ucGridView1.Add("程序代码", "APP_CODE", false);

            ucGridView1.Add("参数类别", "PARAMETER_CLASS");
            ucGridView1.Add("子类", "PARAMETER_CLASS_SUB");
            ucGridView1.Add("参数名(中文)", "PARAMETER_NAME_CN", true);
            ucGridView1.Add("参数名", "PARAMETER_NAME");
            ucGridView1.Add("参数值", "PARAMETER_VALUE");
            //ucGridView1.Add("取值范围", "PARAMETER_SCOPE");
            ucGridView1.Add("是否可用", "ENABLED", typeof(decimal));

            ucGridView1.Add("科室", "DEPT_CODE");
            ucGridView1.Add("用户", "USER_ID");
            //ucGridView1.Add("备注", "MEMO");
            //ucGridView1.Init();
        }


        private void initDisp()
        {
            initDisp_ParamDetail();

            dsParams.Tables[0].DefaultView.Sort = "APP_CODE, DEPT_CODE, USER_ID, PARAMETER_CLASS,PARAMETER_CLASS_SUB, PARAMETER_NAME";
            ucGridView1.DataSource = dsParams.Tables[0].DefaultView;
        }


        private void initDisp_ParamDetail()
        {
            txtAppCode.Text = AppCode;      // 程序代码
            //txtDeptCode.Text1        = string.Empty; // 科室代码
            //txtUserId.Text1          = string.Empty; // 用户ID
            //txtParamClass.Text      = string.Empty; // 参数类别
            //txtParamClassSub.Text = string.Empty;                  // 参数子类别
            txtParamNameCn.Text = string.Empty; // 参数中文名
            txtParamName.Text = string.Empty; // 参数名
            txtParamValue.Text = string.Empty; // 参数值
            txtParamValueRng.Text = string.Empty; // 取值范围
            chkEnabled.Checked = true;         // 启用
            txtMemo.Text = string.Empty; // 备用
        }


        private void showParamDetail(DataRow drApp)
        {
            paramId = drApp["PARAMETER_ID"].ToString();

            txtAppCode.Text = drApp["APP_CODE"].ToString();                             // 程序代码
            txtDeptCode.Text = drApp["DEPT_CODE"].ToString();                            // 科室代码
            txtUserId.Text = drApp["USER_ID"].ToString();                              // 用户ID
            txtParamClass.Text = drApp["PARAMETER_CLASS"].ToString();                      // 参数类别
            txtParamClassSub.Text = drApp["PARAMETER_CLASS_SUB"].ToString();                  // 参数子类别
            txtParamNameCn.Text = drApp["PARAMETER_NAME_CN"].ToString();                    // 参数中文名
            txtParamName.Text = drApp["PARAMETER_NAME"].ToString();                       // 参数名
            txtParamValue.Text = drApp["PARAMETER_VALUE"].ToString();                      // 参数值
            txtParamValueRng.Text = drApp["PARAMETER_SCOPE"].ToString();                      // 取值范围
            chkEnabled.Checked = (drApp["ENABLED"].ToString().Equals("1"));                // 启用
            txtMemo.Text = drApp["MEMO"].ToString();                                 // 备用
        }


        private bool chkDisp()
        {
            if (txtParamName.Text.Trim().Length == 0)
            {
                txtParamName.Focus();
                return false;
            }

            txtAppCode.Text = txtAppCode.Text.Trim();
            if (txtAppCode.Text.Length > 0 && txtAppCode.Text.Equals(AppCode) == false)
            {
                txtAppCode.Focus();
                return false;
            }

            return true;
        }


        private bool saveDisp()
        {
            // 获取当前日期
            DateTime dtNow = GVars.OracleAccess.GetSysDate();

            // 查找记录
            DataRow[] drFind = dsParams.Tables[0].Select("PARAMETER_ID = " + paramId);

            DataRow drEdit = null;
            if (drFind.Length == 0)
            {
                drEdit = dsParams.Tables[0].NewRow();
            }
            else
            {
                drEdit = drFind[0];
            }

            // 保存数据            
            drEdit["PARAMETER_ID"] = paramId;                      // 参数ID号
            drEdit["APP_CODE"] = txtAppCode.Text.Trim();       // 程序代码            
            drEdit["DEPT_CODE"] = txtDeptCode.Text.Trim();      // 科室代码
            drEdit["USER_ID"] = txtUserId.Text.Trim();        // 用户ID
            drEdit["PARAMETER_CLASS"] = txtParamClass.Text.Trim();    // 参数类别
            drEdit["PARAMETER_CLASS_SUB"] = txtParamClassSub.Text.Trim();    // 参数子类别
            drEdit["PARAMETER_NAME_CN"] = txtParamNameCn.Text.Trim();   // 参数中文名
            drEdit["PARAMETER_NAME"] = txtParamName.Text.Trim();     // 参数名
            drEdit["PARAMETER_VALUE"] = txtParamValue.Text.Trim();    // 参数值
            drEdit["PARAMETER_SCOPE"] = txtParamValueRng.Text.Trim(); // 取值范围
            drEdit["ENABLED"] = (chkEnabled.Checked ? "1" : "0");         // 启用
            drEdit["MEMO"] = txtMemo.Text.Trim();          // 备用

            if (drFind.Length == 0)
            {
                dsParams.Tables[0].Rows.Add(drEdit);
            }

            return true;
        }


        private void locate(string paramId)
        {
            ucGridView1.SelectRow("PARAMETER_ID", paramId);
        }
        #endregion
    }
}
