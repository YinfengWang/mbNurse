using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraTreeList.Nodes;

namespace HISPlus
{
    public partial class RoleManagerFrm : FormDo
    {
        #region 窗体变量
        private ApplicationMangerCom _com;

        private DataSet _dsRole;
        private DataSet _dsApp;
        private DataSet _dsRoleRights;

        /// <summary>
        /// 当前选中的角色Id
        /// </summary>
        private string _roleId = string.Empty;
        private string _roleTitle = string.Empty;

        private string _appCode = string.Empty;
        private string _appName = string.Empty;

        /// <summary>
        /// 菜单集合
        /// </summary>
        private IList<MenuManager> _listMenus;
        #endregion


        public RoleManagerFrm()
        {
            InitializeComponent();

            _id = "00008";
            _guid = "c0af52eb-a253-40cf-a9a6-c6faf4e9523a";
        }


        private void RoleManagerFrm_Load(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;

                InitFrmVal();
                InitDisp();

                // 触发事件
                GVars.App.UserInput = blnStore;
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
        /// 按钮[新增]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtRole.Text = string.Empty;
                this.txtRole.Focus();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[删除]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucGridView1.SelectRowsCount == 0 || _roleId.Length == 0)
                {
                    return;
                }

                if (GVars.Msg.Show("Q0004", "角色") != DialogResult.Yes)
                {
                    return;
                }

                string filter = "ROLE_ID = " + SqlManager.SqlConvert(_roleId);
                DataRow[] drFind = _dsRole.Tables[0].Select(filter);

                if (drFind.Length > 0)
                {
                    drFind[0].Delete();

                    _com.SaveRoleDict(ref _dsRole);

                    // 刷新显示
                    _dsRole = _com.GetRoleDict();
                    _dsRole.Tables[0].DefaultView.Sort = "ROLE_NAME";
                    ucGridView1.DeleteSelectRow();
                    ucGridView1.DataSource = _dsRole.Tables[0].DefaultView;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 角色改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRole_TextChanged(object sender, EventArgs e)
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
                if (txtRole.Text.Trim().Length == 0)
                {
                    txtRole.Focus();
                    return;
                }

                // 判断角色名称是否存在
                string roleTitle = txtRole.Text.Trim();

                if (_dsRole.Tables[0].Select("ROLE_NAME ='" + roleTitle + "'").Length > 0)
                    return;
                //foreach (DataGridViewRow dgvRow in  dgvRole.Rows)
                //{
                //    if (dgvRow.Cells["ROLE_NAME"].Value.ToString().Equals(roleTitle) == true)
                //    {
                //        return;
                //    }
                //}

                // 新增角色
                DataRow[] drFind = _dsRole.Tables[0].Select(string.Empty, "ROLE_ID DESC");
                string newRoleId = "001";

                if (drFind.Length > 0)
                {
                    newRoleId = (int.Parse(drFind[0]["ROLE_ID"].ToString()) + 1).ToString("000");
                }

                DateTime dtNow = GVars.OracleAccess.GetSysDate();

                DataRow drNew = _dsRole.Tables[0].NewRow();

                drNew["ROLE_ID"] = newRoleId;
                drNew["ROLE_NAME"] = roleTitle;
                drNew["ENABLED"] = "1";
                drNew["CREATE_DATE"] = dtNow;
                drNew["UPDATE_DATE"] = dtNow;

                _dsRole.Tables[0].Rows.Add(drNew);

                // 保存
                _com.SaveRoleDict(ref _dsRole);

                ucGridView1.SelectRow("ROLE_ID", newRoleId);

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

        /// <summary>
        /// 按钮[保存], 保存角色权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveRights_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (_roleId.Length == 0)
                {
                    GVars.Msg.ErrorSrc = txtRole;
                    GVars.Msg.MsgId = "I0007";                      // 请先确定角色!
                    GVars.Msg.Show();

                    return;
                }

                // 保存角色权限到缓存
                DateTime dtNow = GVars.OracleAccess.GetSysDate();      // 获取当前日期
                SaveDispRoleRight(null, dtNow);

                // 保到DB
                _com.SaveRoleRights(ref _dsRoleRights, _roleId);

                this.btnSaveRights.Enabled = false;
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

        #region 共通函数
        private void InitFrmVal()
        {
            _com = new ApplicationMangerCom();

            _dsRole = _com.GetRoleDict();
            _dsApp = _com.GetApplicationList();
        }


        private void InitDisp()
        {
            ucTreeList1.ShowCheckBoxes = true;
            ucTreeList1.ImageList = imageList1;         
            ucTreeList1.Add("", "Id", false);
            ucTreeList1.Add("模块", "Name");
            ucTreeList1.Add("ModuleCode", "ModuleCode",false);

            // 角色列表
            //dgvRole.AutoGenerateColumns = false;
            //dsRole.Tables[0].DefaultView.Sort = "ROLE_NAME";
            //dgvRole.DataSource = dsRole.Tables[0].DefaultView;

            ucGridView1.ShowRowIndicator = true;
            ucGridView1.Add("角色名称", "ROLE_NAME");
            ucGridView1.DataSource = _dsRole.Tables[0].DefaultView;
            ucGridView1.Init();

            // 应用程序列表
            _dsApp.Tables[0].DefaultView.Sort = "APP_CODE, APP_VERSION";
            ucGridView2.ShowRowIndicator = true;
            ucGridView2.Add("程序", "APP_NAME");
            ucGridView2.Add("程序名称", "APP_TITLE");
            ucGridView2.Add("编码", "APP_CODE");
            ucGridView2.Add("版本", "APP_VERSION");
            ucGridView2.DataSource = _dsApp.Tables[0].DefaultView;
            ucGridView2.Init();

            // 界面控制
            this.btnSave.Enabled = false;
        }

        private void MarkModuleRight(TreeListNode parentNode)
        {
            // 遍历所有节点
            if (parentNode == null)
            {
                foreach (TreeListNode node in ucTreeList1.Nodes)
                {
                    MarkOneNodeRight(node, _roleId);
                    MarkModuleRight(node);
                }
            }
            else
            {
                foreach (TreeListNode node in parentNode.Nodes)
                {
                    MarkOneNodeRight(node, _roleId);
                    MarkModuleRight(node);
                }
            }
        }

        private void MarkOneNodeRight(TreeListNode node, string roleId)
        {
            string nodeTag = node.Tag.ToString();
            string moduleCode = node.GetValue("ModuleCode").ToString();
            string right = string.Empty;

            string[] parts = nodeTag.Split(ComConst.STR.BLANK.ToCharArray());
            if (parts.Length < 2)
            {
                throw new Exception("saveOneNodeRight() 错误! 原因是 节点Tab赋值不符合要求!");
            }

            //moduleCode = parts[1].Trim();

            if (moduleCode.Length == 0)
            {
                return;
            }

            if (parts.Length > 2)
            {
                right = parts[2].Trim();
            }

            // 查找记录
            string filter = "ROLE_ID = " + SqlManager.SqlConvert(roleId)
                          + " AND MODULE_CODE = " + SqlManager.SqlConvert(moduleCode);

            DataRow[] drFind = _dsRoleRights.Tables[0].Select(filter);

            if (drFind.Length == 0)
            {
                return;
            }

            if (right.Length > 0)
            {
                string rightList = drFind[0]["MODULE_RIGHT"].ToString();
                if (rightList.IndexOf(right) >= 0)
                {
                    node.Checked = true;
                }
            }
            else
            {
                node.Checked = true;
            }
            //ucTreeList1.CheckedParentNodes(node, node.Checked ? CheckState.Checked : CheckState.Unchecked);
            ucTreeList1.CheckedParentNodes(node, CheckState.Checked);
        }

        private void SaveDispRoleRight(TreeListNode parentNode, DateTime dtNow)
        {
            // 遍历所有节点
            if (parentNode == null)
            {
                foreach (TreeListNode node in ucTreeList1.Nodes)
                {
                    SaveOneNodeRight(_roleId, node.Tag.ToString(), dtNow, node.Checked);
                    SaveDispRoleRight(node, dtNow);
                }
            }
            else
            {
                foreach (TreeListNode node in parentNode.Nodes)
                {
                    SaveOneNodeRight(_roleId, node.Tag.ToString(), dtNow, node.Checked);
                    SaveDispRoleRight(node, dtNow);
                }
            }
        }


        private void SaveOneNodeRight(string roleId, string nodeId, DateTime dtNow, bool endow)
        {
            string right = string.Empty;

            string[] parts = nodeId.Split(ComConst.STR.BLANK.ToCharArray());
            if (parts.Length < 2)
            {
                throw new Exception("saveOneNodeRight() 错误! 原因是 节点Tab赋值不符合要求!");
            }

            string moduleCode = parts[1].Trim();

            if (moduleCode.Length == 0)
            {
                return;
            }

            if (parts.Length > 2)
            {
                right = parts[2].Trim();
            }

            // 查找记录
            string filter = "ROLE_ID = " + SqlManager.SqlConvert(roleId)
                          + " AND MODULE_CODE = " + SqlManager.SqlConvert(moduleCode);

            DataRow[] drFind = _dsRoleRights.Tables[0].Select(filter);

            // 如果是撤消权限
            if (endow == false)
            {
                if (drFind.Length > 0 && parts.Length < 3)
                {
                    drFind[0].Delete();
                }

                return;
            }

            // 如果是赋予权限
            DataRow drEdit;
            if (drFind.Length == 0)
            {
                drEdit = _dsRoleRights.Tables[0].NewRow();
            }
            else
            {
                drEdit = drFind[0];
            }

            // 保存数据
            drEdit["ROLE_ID"] = roleId;               // 角色ID
            drEdit["MODULE_CODE"] = moduleCode;           // 模块编码
            // 权限
            if (parts.Length > 2)
            {
                string moduleRight = drEdit["MODULE_RIGHT"].ToString();

                if (moduleRight.Length == 0)
                {
                    drEdit["MODULE_RIGHT"] = right;
                }
                else
                {
                    drEdit["MODULE_RIGHT"] = moduleRight + ComConst.STR.COMMA + right;
                }
            }
            else
            {
                drEdit["MODULE_RIGHT"] = string.Empty;
            }

            if (drFind.Length == 0)
            {
                drEdit["CREATE_DATE"] = dtNow;            // 创建时间
                _dsRoleRights.Tables[0].Rows.Add(drEdit);
            }

            drEdit["UPDATE_DATE"] = dtNow;            // 更新时间
        }
        #endregion


        #region 权限树
        private void ResetTreeAppModule()
        {
            // 清除所有节点
            ucTreeList1.ClearNodes();
            imageList1.Images.Clear();

            if (_listMenus.Count == 0)
                return;

            ucTreeList1.BeginUnboundLoad();
            IList<MenuManager> list = _listMenus.Where(p => p.ParentId == "0").OrderBy(p => p.SortId).ToList();
            foreach (MenuManager root in list)
            {
                TreeListNode node = ucTreeList1.AppendNode(new object[] { root.Id, root.Name, root.ModuleCode }, null);

                if (AddImageInList(root.IconPath))
                {
                    node.SelectImageIndex = imageList1.Images.Count - 1;
                    node.ImageIndex = imageList1.Images.Count - 1;
                }

                node.Tag = root.Id + ComConst.STR.BLANK + root.ModuleCode;
                AddSubNodes(node);
            }
            ucTreeList1.EndUnboundLoad();
        }

        private void AddSubNodes(TreeListNode parentNode)
        {
            string parentNodeId = Convert.ToString(parentNode.GetValue("Id"));

            IList<MenuManager> list = _listMenus.Where(p => p.ParentId == parentNodeId).OrderBy(p => p.SortId).ToList();
            foreach (MenuManager root in list)
            {
                TreeListNode node = ucTreeList1.AppendNode(new object[] { root.Id, root.Name, root.ModuleCode }, parentNode);

                node.Tag = root.Id + ComConst.STR.BLANK + root.ModuleCode;

                if (AddImageInList(root.IconPath))
                {
                    node.SelectImageIndex = imageList1.Images.Count - 1;
                    node.ImageIndex = imageList1.Images.Count - 1;
                }

                // 增加子节点
                if (root.ModuleCode.Length == 0)
                {
                    AddSubNodes(node);
                }
                // 增加权限节点
                else
                {
                    if (string.IsNullOrEmpty(root.Rights))
                    {
                        continue;
                    }
                    // 添加节点                    
                    string[] parts = root.Rights.Split(ComConst.STR.CRLF.ToCharArray());

                    for (int i = 0; i < parts.Length; i++)
                    {
                        string detailRight = parts[i].Trim().Replace("：", ":");                        // 全角转半角

                        if (detailRight.Length == 0)
                        {
                            continue;
                        }

                        string[] detailParts = detailRight.Split(":".ToCharArray());
                        if (detailParts.Length < 2)
                        {
                            continue;
                        }

                        TreeListNode nodeChild = ucTreeList1.AppendNode(new object[] { root.Id + ComConst.STR.BLANK + root.ModuleCode + ComConst.STR.BLANK + detailParts[0], detailParts[1], root.ModuleCode }, node);

                        nodeChild.Tag = node.Tag + ComConst.STR.BLANK + detailParts[0];
                    }
                }
            }
        }

        private bool AddImageInList(string iconFile)
        {
            if (string.IsNullOrEmpty(iconFile))
            {
                return false;
            }

            string fileName = Path.Combine(Application.StartupPath, iconFile);
            if (File.Exists(fileName))
            {
                imageList1.Images.Add(Image.FromFile(fileName));

                return true;
            }
            return false;
        }
        #endregion

        private void ucGridView1_SelectionChanged(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // 角色 初始化
                _roleId = string.Empty;
                _roleTitle = string.Empty;
                txtRole.Text = string.Empty;

                // 应用程序初始化
                _appCode = string.Empty;
                _appName = string.Empty;
                txtApp.Text = string.Empty;

                // 条件检查
                if (ucGridView1.SelectRowsCount == 0)
                {
                    return;
                }

                // 显示相关信息
                // DataGridViewRow dgvRow = dgvRole.SelectedRows[0];
                _roleId = ucGridView1.SelectedRow["ROLE_ID"].ToString();
                _roleTitle = ucGridView1.SelectedRow["ROLE_NAME"].ToString();

                this.txtRole.Text = _roleTitle;

                // 显示角色拥有的应用程序列表
                _dsRoleRights = _com.GetRoleRights(_roleId);

                GVars.App.UserInput = blnStore;
                ucGridView2_SelectionChanged(sender, e);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
                this.Cursor = Cursors.Default;
            }
        }

        private void ucGridView2_SelectionChanged(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            this.Cursor = Cursors.WaitCursor;

            try
            {
                GVars.App.UserInput = false;

                // 初始化
                _appCode = string.Empty;
                _appName = string.Empty;

                // 条件检查
                if (ucGridView2.SelectRowsCount == 0)
                {
                    return;
                }

                // 显示相关信息
                _appCode = ucGridView2.SelectedRow["APP_CODE"].ToString();
                _appName = ucGridView2.SelectedRow["APP_TITLE"].ToString();

                this.txtApp.Text = _appName;

                _listMenus = _com.GetMenus(_appCode);
                _listMenus = _listMenus.Where(p => p.Enabled == (byte)1).ToList();

                ResetTreeAppModule();

                // 显示用户权限
                MarkModuleRight(null);

                // 界面控制
                btnSaveRights.Enabled = false;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
                this.Cursor = Cursors.Default;
            }
        }

        private void ucTreeList1_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                btnSaveRights.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
    }
}
