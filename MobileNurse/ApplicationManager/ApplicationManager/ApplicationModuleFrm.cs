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
    public partial class ApplicationModuleFrm : FormDo
    {

        #region 窗体变量
        public string AppCode = string.Empty;

        private ApplicationMangerCom _com = null;
        private DataSet _dsModule = null;
        private DataSet _dsAppModules = null;

        private bool _updateNodeProp = false;

        /// <summary>
        /// 菜单集合
        /// </summary>
        private IList<MenuManager> _listMenus;
        #endregion


        public ApplicationModuleFrm()
        {
            InitializeComponent();

            _id = "0006";
            _guid = "f86ff899-d134-4aa5-a6d4-4bd5854388fe";
        }


        #region 窗体事件
        private void ApplicationModuleFrm_Load(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            try
            {
                GVars.App.UserInput = false;

                InitFrmVal();
                InitDisp();
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


        private void dgvModule_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // 添加序号列的值
            dgvModule.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }


        private void dgvModule_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvModule.SelectedRows.Count == 0)
                {
                    return;
                }

                DataGridViewRow dgvRow = dgvModule.SelectedRows[0];

                this.txtModuleCode.Text = dgvRow.Cells["MODULE_CODE"].Value.ToString();         // 模块代码
                this.txtModuleName.Text = dgvRow.Cells["MODULE_NAME"].Value.ToString();         // 模块名称

                if (this.txtNodeTitle.Text.Trim().Length == 0)                                  // 节点标题
                {
                    this.txtNodeTitle.Text = this.txtModuleName.Text;
                }
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

        ///// <summary>
        ///// 按钮[上移]
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnUp_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //  条件检查
        //        if (trvApplication.SelectedNode == null)
        //        {
        //            return;
        //        }

        //        TreeListNode node = trvApplication.SelectedNode;

        //        if (node.Parent == null || node.Parent.Parent == null)
        //        {
        //            return;
        //        }

        //        // 节点移动                
        //        TreeListNode nodeNew = (TreeListNode)node.Clone();

        //        // 如果有兄弟节点
        //        TreeListNode preNode = null;
        //        if (node.PrevNode != null)
        //        {
        //            if (node.PrevNode.Nodes.Count > 0)
        //            {
        //                preNode = node.PrevNode.Nodes[0].LastNode;
        //                preNode.Parent.Nodes.Insert(preNode.Index + 1, nodeNew);
        //            }
        //            else
        //            {
        //                preNode = node.PrevNode;
        //                preNode.Parent.Nodes.Insert(preNode.Index, nodeNew);
        //            }
        //        }
        //        else
        //        {
        //            preNode = node.Parent;
        //            preNode.Parent.Nodes.Insert(preNode.Index + 1, nodeNew);
        //        }

        //        node.Remove();

        //        // 保存到数据库中
        //    }
        //    catch (Exception ex)
        //    {
        //        Error.ErrProc(ex);
        //    }
        //}


        /// <summary>
        /// 按钮[下移]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[下载]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string menuFile = Path.Combine(Application.StartupPath, "Menu.xml");
                if (File.Exists(menuFile))
                {
                    string menuFileNew = Path.Combine(Application.StartupPath, Guid.NewGuid() + "Menu.xml");
                    File.Copy(menuFile, menuFileNew, true);
                }

                DataSet ds = _com.GetApplicationMenu(AppCode);
                ds.WriteXml(menuFile, XmlWriteMode.WriteSchema);

                GVars.Msg.Show("I0004");                                // 下载成功!
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[图标]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectIcon_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.DefaultExt = "ico";
                openFileDialog1.InitialDirectory = Application.StartupPath;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog1.FileName.Length > 0 && openFileDialog1.CheckFileExists)
                    {
                        string fileName = openFileDialog1.FileName;
                        int pos = fileName.IndexOf(Application.StartupPath);

                        if (pos >= 0)
                        {
                            fileName = fileName.Substring(Application.StartupPath.Length + 1,
                                fileName.Length - Application.StartupPath.Length - 1);
                        }
                        else
                        {
                            fileName = Path.GetFileName(fileName);
                        }

                        picIcon.Tag = fileName;
                        picIcon.Image = Image.FromFile(openFileDialog1.FileName);

                        this.btnSave.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[清空]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                initDisp_ModuleDetail();

                txtNodeTitle.Focus();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[查找]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string filter = string.Empty;

                // 查询条件
                if (txtModuleCode.Text.Trim().Length > 0)
                {
                    if (filter.Length > 0) filter += " AND ";
                    filter += "MODULE_CODE LIKE '%" + txtModuleCode.Text.Trim() + "%' ";
                }

                if (txtModuleName.Text.Trim().Length > 0)
                {
                    if (filter.Length > 0) filter += " AND ";
                    filter += "MODULE_NAME LIKE '%" + txtModuleName.Text.Trim() + "%' ";
                }

                // 查询
                _dsModule.Tables[0].DefaultView.RowFilter = filter;
                _dsModule.Tables[0].DefaultView.Sort = "MODULE_CODE";
                dgvModule.DataSource = _dsModule.Tables[0].DefaultView;
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
                // 条件检查
                if (txtNodeId.Text.Trim().Length == 0)
                {
                    txtNodeId.Focus();
                    return;
                }

                // 询问
                if (GVars.Msg.Show("Q0004", "删除", "节点") != DialogResult.Yes)
                {
                    return;
                }

                // 执行删除
                string filter = "ID = " + SqlManager.SqlConvert(txtNodeId.Text.Trim());
                DataRow[] drFind = _dsAppModules.Tables[0].Select(filter);
                if (drFind.Length > 0)
                {
                    drFind[0].Delete();

                    _com.SaveApplicationModules(ref _dsAppModules, AppCode);

                    GVars.Msg.Show("I0005");                            // 删除成功!

                    // 删除树节点
                    TreeListNode treeListNode = findNode(null, txtNodeId.Text.Trim());
                    if (treeListNode != null)
                    {
                        ucTreeList1.Delete(treeListNode);
                    }
                }
                else
                {
                    GVars.Msg.Show("I0006");                            // 该节点不存在!
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 为元素排序
        /// </summary>
        private void SortElement(TreeListNodes nodes)
        {
            int i = 1;
            foreach (TreeListNode node in nodes)
            {
                node.SetValue("SortId", i);
                i++;

                if (node.HasChildren)
                    SortElement(node.Nodes);
            }
        }


        /// <summary>
        /// 按钮[保存]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChkDisp() == false)
                {
                    return;
                }

                // 保存数据到缓存
                SaveDisp();

                //SortElement(ucTreeList1.Nodes);

                //saveTreeListNodeSerialNo(null);

                // 保存数扰到DB中
                _com.SaveApplicationModules(ref _dsAppModules, AppCode);

                // 刷新显示
                //if (_updateNodeProp == false)
                //{

                //    AddTreeListNode(ucTreeList1.SelectedNode, txtNodeId.Text.Trim(),
                //        txtNodeTitle.Text.Trim(), picIcon.Tag.ToString());
                //}
                //else
                //{
                //    updateTreeListNode(txtNodeId.Text.Trim(), txtNodeTitle.Text.Trim(), picIcon.Tag.ToString());
                //}

                InitDisp();

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
            try
            {
                InitFrmVal();
                InitDisp();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion


        #region 共通函数
        private void InitFrmVal()
        {
            _com = new ApplicationMangerCom();

            _dsModule = _com.GetModuleList();
            _dsAppModules = _com.GetApplicationModules(AppCode);

            ucTreeList1.Add("名称", "Name");
            ucTreeList1.KeyFieldName = "Id";
            ucTreeList1.ParentFieldName = "ParentId";
            ucTreeList1.ImageColumnName = "IconPath";            
            ucTreeList1.AllowDrag = true;
            ucTreeList1.LevelLimit = 2;
        }


        private void InitDisp()
        {
            // 模块列表
            dgvModule.AutoGenerateColumns = false;
            _dsModule.Tables[0].DefaultView.Sort = "MODULE_CODE";
            dgvModule.DataSource = _dsModule.Tables[0].DefaultView;

            _listMenus = _com.GetMenus(AppCode);

            ucTreeList1.DataSource = _listMenus;

            // 初始化程序模块数
            //ResetTreeAppModule();

        }


        private void initDisp_ModuleDetail()
        {
            this.txtNodeId.Text = string.Empty;         // 节点ID
            this.txtNodeTitle.Text = string.Empty;         // 节点名称
            this.txtModuleCode.Text = string.Empty;         // 模块编码
            this.txtModuleName.Text = string.Empty;         // 模块名称
            this.picIcon.Tag = string.Empty;         // 图标
            this.picIcon.Image = null;
            this.chkBackGround.Checked = false;             // 是否后台运行
        }

        private void ShowModuleInfo(MenuManager model)
        {
            this.txtNodeId.Text = model.Id;       // 节点ID
            this.txtNodeTitle.Text = model.Name;    // 节点名称
            this.txtModuleCode.Text = model.ModuleCode;   // 模块编码

            // 图标
            picIcon.Image = null;
            if (string.IsNullOrEmpty(model.IconPath)) return;

            string fileName = Path.Combine(Application.StartupPath, model.IconPath);
            if (File.Exists(fileName))
            {
                picIcon.Image = Image.FromFile(fileName);
                picIcon.Tag = model.IconPath;
            }

            // 是否后台运行
            //this.chkBackGround.Checked = (drAppModule["BACKGROUND_RUN"].ToString().Equals("1"));
        }

        private bool ChkDisp()
        {
            if (ucTreeList1.SelectedNode == null)
            {
                ucTreeList1.Focus();
                return false;
            }

            if (txtNodeId.Text.Trim().Length == 0)
            {
                _updateNodeProp = false;

                int maxNodeId = 0;

                foreach (DataRow dr in _dsAppModules.Tables[0].Rows)
                {
                    int nodeId;

                    if (int.TryParse(dr["ID"].ToString(), out nodeId))
                    {
                        if (nodeId > maxNodeId) maxNodeId = nodeId;
                    }
                }

                maxNodeId++;

                txtNodeId.Text = maxNodeId.ToString();
            }
            else
            {
                _updateNodeProp = true;
            }

            if (txtNodeId.Text.Trim().Length == 0)
            {
                txtNodeTitle.Focus();
                return false;
            }

            if (txtNodeTitle.Text.Trim().Length == 0)
            {
                txtNodeTitle.Focus();
                return false;
            }

            if (ucTreeList1.Nodes.Count > 0
                && ucTreeList1.SelectedNode == null)
            {
                ucTreeList1.Focus();
                return false;
            }

            return true;
        }


        private void SaveDisp()
        {
            // 获取当前日期
            DateTime dtNow = GVars.OracleAccess.GetSysDate();

            // 获取NodeId
            string parentNodeId;
            string nodeId = txtNodeId.Text.Trim();

            // 如果是新建
            DataRow drEdit = null;

            if (_updateNodeProp == false)
            {
                parentNodeId = ucTreeList1.SelectedNode.GetValue("ParentId").ToString(); ;

                drEdit = _dsAppModules.Tables[0].NewRow();
            }
            else
            {
                parentNodeId = ucTreeList1.SelectedNode.GetValue("ParentId").ToString();

                // 查找记录
                DataRow[] drFind = _dsAppModules.Tables[0].Select("ID = " + SqlManager.SqlConvert(nodeId));

                drEdit = drFind[0];
            }

            // 保存数据
            drEdit["ID"] = nodeId;                           // 节点ID
            drEdit["NAME"] = this.txtNodeTitle.Text.Trim();    // 节点标题

            drEdit["PARENT_ID"] = parentNodeId;                     // 父节点ID

            drEdit["APP_CODE"] = AppCode;                          // 应用程序代码
            drEdit["MODULE_CODE"] = txtModuleCode.Text.Trim();        // 模块代码           
            drEdit["ICON_PATH"] = picIcon.Tag.ToString();           // 图标文件

            drEdit["ENABLED"] = "1";

            if (_updateNodeProp == false)
            {
                drEdit["CREATE_DATE"] = dtNow;                            // 创建时间
                _dsAppModules.Tables[0].Rows.Add(drEdit);
            }

            drEdit["UPDATE_DATE"] = dtNow;                            // 更新时间
        }


        private bool AddTreeListNode(TreeListNode parentNode, string nodeId, string nodeTitle, string iconFile)
        {
            // 删除原来的节点
            foreach (TreeListNode node in ucTreeList1.Nodes)
            {
                if (node.Tag.ToString().Equals(nodeId))
                {
                    ucTreeList1.Nodes.Remove(node);
                    break;
                }

                if (removeNode(node, nodeId) == true)
                {
                    break;
                }
            }

            // 增加新节点
            //TreeListNode TreeListNode = null;

            //if (addImageInList(iconFile) == true)
            //{
            //    TreeListNode = new TreeListNode(nodeTitle, imageList1.Images.Count - 1, imageList1.Images.Count - 1);
            //}
            //else
            //{
            //    TreeListNode = new TreeListNode(nodeTitle);
            //}

            //TreeListNode.Tag = nodeId;

            //if (parentNode == null)
            //{
            //    ucTreeList1.Nodes.Add(TreeListNode);
            //}
            //else
            //{
            //    parentNode.Nodes.Add(TreeListNode);
            //}

            return true;
        }


        private bool removeNode(TreeListNode parentNode, string nodeId)
        {
            foreach (TreeListNode node in parentNode.Nodes)
            {
                if (node.Tag.ToString().Equals(nodeId))
                {
                    parentNode.Nodes.Remove(node);
                    return true;
                }

                if (removeNode(node, nodeId) == true)
                {
                    return true;
                }
            }

            return false;
        }


        private bool updateTreeListNode(string nodeId, string nodeTitle, string iconFile)
        {
            TreeListNode TreeListNode = findNode(null, nodeId);

            //TreeListNode.Text = nodeTitle;

            //if (iconFile.Length > 0)
            //{
            //    if (addImageInList(iconFile) == true)
            //    {
            //        TreeListNode.ImageIndex = imageList1.Images.Count - 1;
            //        TreeListNode.SelectedImageIndex = imageList1.Images.Count - 1;
            //    }
            //    else
            //    {
            //        TreeListNode.ImageIndex = -1;
            //        TreeListNode.SelectedImageIndex = -1;
            //    }
            //}

            return true;
        }


        private TreeListNode findNode(TreeListNode parentNode, string nodeId)
        {
            TreeListNode TreeListNode = null;

            if (parentNode == null)
            {
                foreach (TreeListNode node in ucTreeList1.Nodes)
                {
                    if (node.Tag.ToString().Equals(nodeId) == true)
                    {
                        return node;
                    }

                    TreeListNode = findNode(node, nodeId);
                    if (TreeListNode != null) return TreeListNode;
                }
            }
            else
            {
                foreach (TreeListNode node in parentNode.Nodes)
                {
                    if (node.Tag.ToString().Equals(nodeId) == true)
                    {
                        return node;
                    }

                    TreeListNode = findNode(node, nodeId);
                    if (TreeListNode != null) return TreeListNode;
                }
            }

            return null;
        }

        /// <summary>
        /// 保存树节点的序列号
        /// </summary>
        /// <returns></returns>
        private bool saveTreeListNodeSerialNo(TreeListNode parentNode)
        {
            // 条件检查
            if (parentNode == null)
            {
                if (ucTreeList1.Nodes.Count == 0)
                {
                    return false;
                }
            }
            else
            {
                if (parentNode.Nodes.Count == 0)
                {
                    return false;
                }
            }

            // 获取子节点列表中的第一个节点
            TreeListNode nodeFirst = null;
            if (parentNode == null)
            {
                nodeFirst = ucTreeList1.Nodes[0];
            }
            else
            {
                nodeFirst = parentNode.FirstNode;
            }

            // 保存
            TreeListNode node = nodeFirst;
            DataRow drEdit = null;
            int index = 0;

            string filter = "ID = '{0}'";
            DataRow[] drFind = null;

            do
            {
                if (node.Tag != null && node.Tag.ToString().Length > 0)
                {
                    drFind = _dsAppModules.Tables[0].Select(string.Format(filter, node.Tag.ToString()));
                    drEdit = drFind[0];

                    if (drEdit["SERIAL_NO"].ToString().Equals(index.ToString()) == false)
                    {
                        drEdit["SERIAL_NO"] = index.ToString();
                    }
                }

                // 保存子节点
                if (node.Nodes.Count > 0)
                {
                    saveTreeListNodeSerialNo(node);
                }

                // 下一个节点
                node = node.NextNode;
                index++;
            } while (node != null);

            return true;
        }
        #endregion

        private void ucTreeList1_AfterSelect(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                initDisp_ModuleDetail();

                if (ucTreeList1.SelectRowData == null)
                {
                    return;
                }

                MenuManager model = ucTreeList1.SelectRowData as MenuManager;
                if (model == null) return;

                ShowModuleInfo(model);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
    }
}
