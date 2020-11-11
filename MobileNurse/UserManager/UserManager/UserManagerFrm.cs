using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;

namespace HISPlus
{
    public partial class UserManagerFrm : FormDo
    {

        #region 窗体变量
        private UserManagerCom _com = null;

        private DataSet dsRoles = null;
        private DataSet dsNursingUnit = null;
        private string deptCode = string.Empty;

        private DataSet dsNurse = null;
        private string nurseId = string.Empty;

        private DataSet dsUserRoles = null;
        #endregion


        public UserManagerFrm()
        {
            InitializeComponent();

            _id = "00009";
            _guid = "556b58fd-32b0-402d-850c-1a0c0b99e6cf";

            gvDept.SelectionChanged += gvDept_SelectionChanged;
            gvUser.SelectionChanged += gvUser_SelectionChanged;
        }


        #region 窗体事件
        private void UserManagerFrm_Load(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;

                initFrmVal();
                initDisp();
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


        /// <summary>
        /// 按钮[保存]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // 保存用户信息
                SaveUserInfo();

                // 保存用户权限
                SaveUserRoles();

                this.btnSave.Enabled = false;
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


        #region DataGridView
        private void dgv_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            dgv.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void dgvRole_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false || e.ColumnIndex != 1)
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
        #endregion


        #region 共通函数
        private void initFrmVal()
        {
            _com = new UserManagerCom();

            dsRoles = _com.GetRoleDict();

            if (!dsRoles.Tables[0].Columns.Contains("CHK_SELECT"))
            {
                dsRoles.Tables[0].Columns.Add("CHK_SELECT", typeof(bool), "False");
            }

            dsNursingUnit = GVars.OracleAccess.SelectData("SELECT * FROM DEPT_DICT"); // com.GetNursingUnits();
        }


        private void initDisp()
        {
            // 护理单元
            dsNursingUnit.Tables[0].DefaultView.Sort = "DEPT_CODE";

            // 角色列表
            dsRoles.Tables[0].DefaultView.Sort = "ROLE_NAME";

            gvDept.ShowRowIndicator = true;
            gvDept.Add("科室代码", "DEPT_CODE");
            gvDept.Add("科室名称", "DEPT_NAME");

            gvDept.Init();

            gvDept.DataSource = dsNursingUnit.Tables[0].DefaultView;

            gvUser.ShowRowIndicator = true;
            gvUser.Add("USER_ID", "ID", false);
            gvUser.Add("输入码", "USER_NAME");
            gvUser.Add("姓名", "NAME");

            gvUser.Init();

            ucTreeList1.ShowCheckBoxes = true;
            ucTreeList1.ShowRoot = false;

            //ucTreeList1.Add("ROLE_ID", "ROLE_ID");
            ucTreeList1.Add("角色名称", "ROLE_NAME");
            ucTreeList1.Add("说明", "MEMO");
            //ucTreeList1.Add("选择", "CHK_SELECT");

            ucTreeList1.KeyFieldName = "ROLE_ID";
            ucTreeList1.ParentFieldName = null;
            ucTreeList1.DataSource = dsRoles.Tables[0].DefaultView;

            ucTreeList1.AfterCheckNode += ucTreeList1_AfterCheckNode;

            // 界面控件
            this.btnSave.Enabled = false;
        }

        void ucTreeList1_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (gvUser.SelectedRow != null)
                btnSave.Enabled = true;
        }

        void gvUser_SelectionChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;

                btnClearPwd.Enabled = false;

                if (gvUser.SelectedRow == null)
                {
                    ucTreeList1.UncheckAll();
                }
                else
                {
                    // 获取用户代码
                    nurseId = gvUser.SelectedRow["ID"].ToString();

                    // 查找用户具有的角色
                    dsUserRoles = _com.GetUserRoles(nurseId);

                    // 显示用户角色
                    markUserRole();
                }
                btnSave.Enabled = false;
                btnClearPwd.Enabled = true;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                GVars.App.UserInput = blnStore;
            }
        }

        void gvDept_SelectionChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // 获取科室代码
                deptCode = gvDept.SelectedRow["DEPT_CODE"].ToString();

                // 查找该病区护士
                dsNurse = _com.GetNursesInDept(deptCode);

                dsNurse.Tables[0].DefaultView.Sort = "NAME";

                gvUser.DataSource = dsNurse.Tables[0].DefaultView;

                if (gvUser.SelectedRow == null)
                {
                    ucTreeList1.UncheckAll();
                    btnClearPwd.Enabled = false;
                }
                else
                {
                    gvUser_SelectionChanged(sender, e);
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


        /// <summary>
        /// 标识用户具有的角色
        /// </summary>
        private void markUserRole()
        {
            //foreach (DataGridViewRow dgvRow in dgvRole.Rows)
            //{
            //    dgvRow.Cells["CHK_SELECT"].Value = 0;

            //    string roleId = dgvRow.Cells["ROLE_ID"].Value.ToString();

            //    foreach (DataRow dr in dsUserRoles.Tables[0].Rows)
            //    {
            //        if (roleId.Equals(dr["ROLE_ID"].ToString()) == true)
            //        {
            //            dgvRow.Cells["CHK_SELECT"].Value = 1;
            //            break;
            //        }
            //    }
            //}

            //for (int i = 0; i < gvRole.RowCount; i++)
            //{
            //    gvRole.SetRowCellValue(i, "CHK_SELECT", false);

            //    string roleId = gvRole.GetRowCellValue(i, "ROLE_ID").ToString();

            //    //foreach (DataRow dr in dsUserRoles.Tables[0].Rows)
            //    {
            //      //  if (roleId.Equals(dr["ROLE_ID"].ToString()) == true)
            //        {
            //            gvRole.SetRowCellValue(i, "CHK_SELECT", true);
            //            break;
            //        }
            //    }
            //}

            ucTreeList1.UncheckAll();

            foreach (TreeListNode node in ucTreeList1.Nodes)
            {
                string roleId = node.GetValue("ROLE_ID").ToString();
                foreach (DataRow dr in dsUserRoles.Tables[0].Rows)
                {
                    if (roleId.Equals(dr["ROLE_ID"].ToString()) == true)
                    {
                        node.Checked = true;
                        break;
                    }
                }
            }
        }


        private bool SaveUserInfo()
        {
            DataRow[] drFind = dsNurse.Tables[0].Select("ID = " + SqlManager.SqlConvert(nurseId));

            if (drFind.Length > 0)
            {
                DataRow dr = drFind[0];

                _com.SaveUserInfo(nurseId, nurseId, dr["NAME"].ToString(),
                    dr["PASSWORD"].ToString(), dr["USER_NAME"].ToString());

                return true;
            }
            else
            {
                return true;
            }
        }


        private bool SaveUserRoles()
        {
            // 删除以前的角色
            foreach (DataRow dr in dsUserRoles.Tables[0].Rows)
            {
                dr.Delete();
            }

            DateTime dtNow = GVars.OracleAccess.GetSysDate();

            // 增加选中的角色
            foreach (TreeListNode node in ucTreeList1.CheckedNodes)
            {
                DataRow drNew = dsUserRoles.Tables[0].NewRow();

                drNew["USER_ID"] = nurseId;
                drNew["ROLE_ID"] = node.GetValue("ROLE_ID").ToString();
                drNew["CREATE_DATE"] = dtNow;
                drNew["UPDATE_DATE"] = dtNow;

                dsUserRoles.Tables[0].Rows.Add(drNew);
            }

            return _com.SaveUserRoles(ref dsUserRoles, nurseId);
        }
        #endregion


        /// <summary>
        /// 按钮[清除密码]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearPwd_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dgvRow = gvUser.SelectedRow;
                nurseId = dgvRow["USER_NAME"].ToString();

                if (_com.ResetPwd(nurseId) == true)
                {
                    GVars.Msg.Show("I0009", "重置密码");            // {0}成功!
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
    }
}
