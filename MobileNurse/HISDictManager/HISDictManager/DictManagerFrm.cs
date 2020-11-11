using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using HISPlus.UserControls;

namespace HISPlus
{
    public partial class DictManagerFrm : FormDo
    {
        #region 变量
        private const string RIGHT_ITEM = "1";                        // 维护项目的权限
        private const string RIGHT_DICT = "2";                        // 维护字典的权限

        private HISDictDbI dictDbI = null;

        private DataSet dsDictGroup = null;                         // 字典分组
        private DataSet dsDict = null;                         // 字典表
        private DataSet dsDictItem = null;                         // 字典项目

        private DataRow drDictGroup = null;                         // 当前字典分组记录
        private DataRow drDict = null;                         // 当前字典

        private TextBox txtControl = null;                         // 字典项目当前编辑框

        protected string _groupId = string.Empty;                 // 分组编码
        #endregion


        public DictManagerFrm()
        {
            InitializeComponent();

            _id = "00044";
            _guid = "99D23DCB-3028-4099-8F17-8850F1839038";
        }


        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DictManagerFrm_Load(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            try
            {
                GVars.App.UserInput = false;

                initFrmVal();
                InitControl();
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

        private void InitControl()
        {
            // 权限设置 如果没有维护项目的权限            
            ucGridView1.AllowAddRows = _userRight.IndexOf(RIGHT_ITEM) >= 0;
            ucGridView1.AllowEdit = _userRight.IndexOf(RIGHT_ITEM) >= 0;

            ucGridView1.Add("DICT_ID", "DICT_ID", false);
            ucGridView1.Add("ID", "ITEM_ID");
            ucGridView1.Add("项目名称", "ITEM_NAME");
            ucGridView1.Add("父节点", "PARENT_ID");
            ucGridView1.Add("标识", "FLAG");
            ucGridView1.Add("搜索码", "DESC_SPELL");

            ucGridView1.SelectionChanged += ucGridView1_SelectionChanged;
            ucGridView1.InitNewRow += ucGridView1_InitNewRow;
            ucGridView1.CellValueChanged += ucGridView1_CellValueChanged;
            ucGridView1.Init();

            ucTreeList1.ContextMenuStrip = this.mnuContext_DictGroup;

            if (_groupId.Length > 0)
            {
                ucTreeList1.ContextMenuStrip = null;
            }

            btnSaveDict.Enabled = false;

            // 权限设置 如果没有维护字典的权限
            if (_userRight.IndexOf(RIGHT_DICT) < 0)
            {
                ucTreeList1.ContextMenuStrip = null;
            }

            ucTreeList1.KeyFieldName = "NODE_ID";
            ucTreeList1.ParentFieldName = "PARENT_NODE_ID";
            ucTreeList1.Add("字典列表", "NODE_TITLE");
            ucTreeList1.Add("PARENT_NODE_ID", "PARENT_NODE_ID", false);
            ucTreeList1.Add("NODE_ID", "NODE_ID", false);

            //dsDictGroup.Tables[0].Columns.Add("PARENT_NODE_ID1", typeof(decimal));
            //foreach (DataRow dr in dsDictGroup.Tables[0].Rows)
            //{
            //    if (dr["PARENT_NODE_ID"] != DBNull.Value)
            //        dr["PARENT_NODE_ID1"] = dr["PARENT_NODE_ID"];
            //    else
            //        dr["PARENT_NODE_ID1"] = 0;
            //}
            ////dsDictGroup.Tables[0].Columns.Remove("PARENT_NODE_ID");
            ////dsDictGroup.Tables[0].Columns["PARENT_NODE_ID1"].ColumnName = "PARENT_NODE_ID";
            ucTreeList1.DataSource = dsDictGroup.Tables[0];

            btnSaveItem.Enabled = false;
            btnDeleteItem.Enabled = false;
            btnAdd.Enabled = false;
        }

        void ucGridView1_SelectionChanged(object sender, EventArgs e)
        {
            btnDeleteItem.Enabled = (_userRight.IndexOf(RIGHT_ITEM) >= 0);
            btnAdd.Enabled = btnDeleteItem.Enabled;
        }

        void ucGridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            btnSaveItem.Enabled = true;

            if (e.Column.FieldName == "ITEM_NAME")
            {
                string text = e.Value.ToString();
                // string text = dgvDictItem.SelectedCells[0].Value.ToString();

                if (text.Trim().Length > 0)
                {
                    text = Coding.GetChineseSpell(text.Trim());
                }

                if (text.Length > 6)
                {
                    text = text.Substring(0, 6);
                }
                ucGridView1.SetRowCellValue(e.RowHandle, "DESC_SPELL", text);
            }
        }

        void ucGridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            if (dsDictItem == null) return;
            if (drDict != null)
                ucGridView1.SetRowCellValue(e.RowHandle, "DICT_ID", drDict["DICT_ID"]);
        }

        void ucTreeList1_AfterSelect(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                GVars.App.UserInput = false;

                this.Cursor = Cursors.WaitCursor;

                // 显示节点基本信息
                showNodeInfo(e.Node.GetValue("NODE_ID").ToString());

                // 获取字典项目
                dsDictItem = dictDbI.GetDictItem(txtDictId.Text.Trim());

                // 显示字典项目
                if (dsDictItem == null)
                {
                    ucGridView1.DataSource = null;
                }
                else
                {
                    ucGridView1.DataSource = dsDictItem.Tables[0].DefaultView;
                }

                btnSaveItem.Enabled = false;
                //btnDeleteItem.Enabled = false;
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


        /// <summary>
        /// 按钮[保存]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveDict_Click(object sender, EventArgs e)
        {
            try
            {
                // 检查输入
                if (chkDisp_DictInfo() == false)
                {
                    return;
                }

                // 保存到缓存
                bool blnNewNode = false;

                if (drDict == null && drDictGroup == null)
                {
                    DataRow drNew = dsDict.Tables[0].NewRow();
                    drNew["DICT_ID"] = txtDictId.Text.Trim();
                    dsDict.Tables[0].Rows.Add(drNew);
                    drDict = drNew;

                    drNew = dsDictGroup.Tables[0].NewRow();
                    drNew["NODE_ID"] = txtNodeId.Text.Trim();
                    drNew["DICT_ID"] = txtDictId.Text.Trim();
                    drNew["PARENT_NODE_ID"] = ucTreeList1.SelectIds;    // 父节点ID
                    drNew["NODE_TITLE"] = txtNodeName.Text.Trim();      // 字典名称

                    dsDictGroup.Tables[0].Rows.Add(drNew);

                    drDictGroup = drNew;

                    blnNewNode = true;
                }

                if (drDict != null)
                {
                    drDict["DESCRIPTIONS"] = txtDictName.Text.Trim();  // 字典名称

                    if (txtMemoInt.Text.Trim().Length > 0)
                    {
                        drDict["STANDBYINT"] = txtMemoInt.Text.Trim();   // 备注1
                    }
                    else
                    {
                        drDict["STANDBYINT"] = DBNull.Value;             // 备注1
                    }

                    drDict["STANDBYVARCHAR"] = txtMemoStr.Text.Trim();   // 备注2
                }

                drDictGroup["NODE_TITLE"] = txtNodeName.Text.Trim();  // 字典名称

                // 保存到DB
                if (drDict != null)
                {
                    dictDbI.SaveDictInfo(dsDict.GetChanges());
                }

                dictDbI.SaveDictGroupInfo(dsDictGroup.GetChanges());

                dsDict.AcceptChanges();
                dsDictGroup.AcceptChanges();

                // 刷新显示
                if (blnNewNode == false)
                {
                    if (ucTreeList1.SelectedNode != null)
                    {
                        //ucTreeList1.SelectedNode.Text1 = txtNodeName.Text1.Trim();
                    }
                }
                else
                {
                    //TreeListNode node = ucTreeList1.SelectedNode.Nodes.Add(txtNodeId.Text1, txtNodeName.Text1.Trim());
                    //node.ImageIndex = 1;
                    ////node.SelectedImageIndex = 1;
                    //node.Tag = txtNodeId.Text1.Trim();

                    //node.Selected = true;
                    //ucTreeList1.SelectedNode = node;
                }

                txtDictId.Properties.ReadOnly = true;
                btnSaveDict.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 节点信息改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDictInfo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                btnSaveDict.Enabled = true;
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
        private void btnSaveItem_Click(object sender, EventArgs e)
        {
            if (drDict != null)
            {
                dictDbI.SaveDictItem(dsDictItem.GetChanges(), drDict["DICT_ID"].ToString());

                dsDictItem.AcceptChanges();
                btnSaveItem.Enabled = false;
            }
        }


        /// <summary>
        /// 按钮[删除]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GVars.Msg.Show("Q0006", btnDeleteItem.Text, "项目") != DialogResult.Yes) // 您确认要 {0} {1} 吗?
                {
                    return;
                }

                ucGridView1.DeleteSelectRow();

                //for (int i = dgvDictItem.SelectedRows.Count - 1; i >= 0; i--)
                //{
                //    dgvDictItem.Rows.Remove(dgvDictItem.SelectedRows[i]);
                //}

                btnSaveItem.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion

        #region 菜单
        /// <summary>
        /// 菜单[新增分组]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuGroup_Add_Click(object sender, EventArgs e)
        {
            try
            {
                // 判断是不是增加顶级分组
                bool topLevel = ucTreeList1.SelectedNode.Level == 0;

                if (topLevel)
                {
                    topLevel = (MessageBox.Show("添加顶级分组吗?", "分组类别", MessageBoxButtons.YesNo) == DialogResult.Yes);
                }

                // 增加分组
                if (topLevel)
                {
                    addNewDictGroup(null);
                }
                else
                {
                    // 获取父分组节点
                    string filter = "NODE_ID = " + SqlManager.SqlConvert(ucTreeList1.SelectIds);
                    DataRow[] drFind = dsDictGroup.Tables[0].Select(filter);

                    TreeListNode nodeParent = ucTreeList1.SelectedNode;

                    if (drFind.Length > 0)
                    {
                        if (drFind[0]["DICT_ID"].ToString().Length > 0)
                        {
                            nodeParent = nodeParent.ParentNode;
                        }
                    }

                    // 增加分组
                    addNewDictGroup(nodeParent);
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuGroup_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (GVars.Msg.Show("Q0006", "删除", "当前分组") != DialogResult.Yes) // 您确认要 {0} {1} 吗?
                {
                    return;
                }

                // 删除缓存
                deleteSubNodesCache(ucTreeList1.SelectIds);

                // 在DB中删除
                dictDbI.SaveDictGroupInfo(dsDictGroup.GetChanges());
                dsDictGroup.AcceptChanges();

                // 删除当前节点
                //DeleteSubNodes(ucTreeList1.SelectedNode);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 新增字典
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuDict_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (getDictId(ucTreeList1.SelectIds).Length > 0)
                {
                    ucTreeList1.SelectedNode = ucTreeList1.SelectedNode.ParentNode;
                }

                txtNodeName.Enabled = true;

                txtDictId.Properties.ReadOnly = false;
                txtDictId.Enabled = true;
                txtDictName.Enabled = true;
                txtMemoInt.Enabled = true;
                txtMemoStr.Enabled = true;

                txtNodeId.Text = getNewNodeId().ToString();
                txtNodeName.Text = string.Empty;
                txtNodeName.Focus();

                drDict = null;
                drDictGroup = null;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuDict_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (GVars.Msg.Show("Q0006", "删除", "当前字典") != DialogResult.Yes) // 您确认要 {0} {1} 吗?
                {
                    return;
                }

                string dictId = getDictId(ucTreeList1.SelectIds);
                if (dictId.Trim().Length == 0)
                {
                    return;
                }

                // 删除DB中对应的数据
                dictDbI.DeleteDict(dictId);

                // 重新生成树
                DictManagerFrm_Load(sender, e);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 菜单[移除字典]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuDict_Remove_Click(object sender, EventArgs e)
        {
            try
            {
                if (GVars.Msg.Show("Q0006", "移除", "当前字典") != DialogResult.Yes) // 您确认要 {0} {1} 吗?
                {
                    return;
                }

                string dictId = getDictId(ucTreeList1.SelectIds);
                if (dictId.Trim().Length == 0)
                {
                    return;
                }

                // 删除DB中对应的数据
                dictDbI.RemoveDictFromGroup(ucTreeList1.SelectIds);

                // 刷新树
                //ucTreeList1.SelectedNode.Remove();
                ucTreeList1.DeleteSelectedNodes();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 菜单[添加字典]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuDict_Append_Click(object sender, EventArgs e)
        {
            try
            {
                DictSearchFrm searchFrm = new DictSearchFrm();
                if (searchFrm.ShowDialog() == DialogResult.OK)
                {
                    // 树上增加一个节点
                    string nodeId = getNewNodeId().ToString();
                    string parentNodeId = string.Empty;

                    TreeListNode node = ucTreeList1.SelectedNode;

                    string dictId = getDictId(ucTreeList1.SelectIds);
                    if (dictId.Trim().Length > 0)
                    {
                        node = node.ParentNode;
                        parentNodeId = node.GetValue(ucTreeList1.KeyFieldName).ToString();
                    }
                    else
                        parentNodeId = ucTreeList1.SelectIds;

                    node = node.Nodes.Add(nodeId, searchFrm.DictName);

                    //node.SelectedImageIndex = 1;
                    node.ImageIndex = 1;

                    node.Tag = nodeId;
                    node.SetValue("NODE_ID", nodeId);
                    node.SetValue("NODE_TITLE", searchFrm.DictName);
                    node.SetValue("PARENT_NODE_ID", parentNodeId);
                    node.SetValue("DICT_ID", searchFrm.DictId);

                    // 缓存中增加一条记录
                    DataRow drNew = dsDictGroup.Tables[0].NewRow();
                    drNew["NODE_ID"] = nodeId;
                    drNew["NODE_TITLE"] = searchFrm.DictName;
                    drNew["PARENT_NODE_ID"] = parentNodeId;
                    drNew["DICT_ID"] = searchFrm.DictId;

                    //dsDictGroup.Tables[0].Rows.Add(drNew);

                    // 保存到数据库中
                    dictDbI.SaveDictGroupInfo(dsDictGroup.GetChanges());
                    dsDictGroup.AcceptChanges();

                    // 选中当前字典
                    ucTreeList1.SelectedNode = node;
                }
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
            // 获取配置参数
            getParameters();

            dictDbI = new HISDictDbI(GVars.OracleAccess);

            dsDictGroup = dictDbI.GetDictGroup();
            dsDict = dictDbI.GetDictList();

            _userRight = GVars.User.GetUserFrmRight(_id);
        }

        /// <summary>
        /// 显示字典基本信息
        /// </summary>
        /// <param name="dictId"></param>
        private void showNodeInfo(string nodeId)
        {
            try
            {
                drDict = null;

                // 初始化显示
                txtNodeId.Text = string.Empty;                         // 节点ID
                txtNodeName.Text = string.Empty;                         // 节点名称

                txtDictId.Text = string.Empty;                         // 字典ID
                txtDictName.Text = string.Empty;                         // 字典名称
                txtMemoInt.Text = string.Empty;                         // 备注1
                txtMemoStr.Text = string.Empty;                         // 备注2

                txtNodeName.Enabled = (nodeId.Length > 0);                  // 节点名称            
                txtDictName.Enabled = false;                                // 字典名称
                txtMemoInt.Enabled = false;                                // 备注1
                txtMemoStr.Enabled = false;                                // 备注2

                // 获取字典ID
                string filter = "NODE_ID = " + SqlManager.SqlConvert(nodeId);
                DataRow[] drFind = dsDictGroup.Tables[0].Select(filter);

                string dictId = string.Empty;
                drDictGroup = null;

                if (drFind.Length > 0)
                {
                    drDictGroup = drFind[0];
                    dictId = drDictGroup["DICT_ID"].ToString();
                }

                // 显示节点信息
                if (drDictGroup != null)
                {
                    txtNodeId.Text = nodeId;                               // 节点ID
                    txtNodeName.Text = drDictGroup["NODE_TITLE"].ToString(); // 节点名称
                }

                // 获取字典信息
                if (dictId.Trim().Length == 0)
                {
                    btnAdd.Enabled = btnDeleteItem.Enabled = false;
                    return;
                }

                filter = "DICT_ID = " + SqlManager.SqlConvert(dictId);
                drFind = dsDict.Tables[0].Select(filter);

                if (drFind.Length == 0)
                {
                    btnAdd.Enabled = btnDeleteItem.Enabled = false;
                    return;
                }

                btnAdd.Enabled = btnDeleteItem.Enabled = true;

                // 显示字典信息
                drDict = drFind[0];
                txtDictId.Text = drDict["DICT_ID"].ToString();         // 字典ID
                txtDictName.Text = drDict["DESCRIPTIONS"].ToString();    // 字典名称
                txtMemoInt.Text = drDict["STANDBYINT"].ToString();      // 备注1
                txtMemoStr.Text = drDict["STANDBYVARCHAR"].ToString();  // 备注2

                txtDictName.Enabled = true;                                 // 字典名称
                txtMemoInt.Enabled = true;                                 // 备注1
                txtMemoStr.Enabled = true;                                 // 备注2

                
            }
            finally
            {
                // 权限设置
                bool right_edit_dict = (_userRight.IndexOf(RIGHT_DICT) >= 0);

                txtNodeId.Enabled = right_edit_dict;                   // 节点ID
                txtNodeName.Enabled = right_edit_dict;                   // 节点名称

                txtDictId.Enabled = right_edit_dict;                   // 字典ID
                txtDictName.Enabled = right_edit_dict;                   // 字典名称
                txtMemoInt.Enabled = right_edit_dict;                   // 备注1
                txtMemoStr.Enabled = right_edit_dict;                   // 备注2            
            }
        }


        /// <summary>
        /// 检查节点信息
        /// </summary>
        /// <returns></returns>
        private bool chkDisp_DictInfo()
        {
            if (txtNodeId.Text.Trim().Length == 0)
            {
                txtNodeId.Focus();
                return false;
            }

            if (txtNodeName.Text.Trim().Length == 0)
            {
                txtNodeName.Focus();
                return false;
            }

            if (drDict == null && drDictGroup == null)
            {
                if (txtDictId.Text.Trim().Length == 0)
                {
                    txtDictId.Focus();
                    return false;
                }
            }

            if (txtDictId.Text.Trim().Length != 0 && txtDictName.Text.Trim().Length == 0)
            {
                txtDictName.Focus();
                return false;
            }

            if (txtMemoInt.Text.Trim().Length > 0)
            {
                int val = 0;
                if (int.TryParse(txtMemoInt.Text.Trim(), out val) == false)
                {
                    txtMemoInt.Focus();
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// 获取新的节点ID
        /// </summary>
        /// <returns></returns>
        private int getNewNodeId()
        {
            DataRow[] drFind = dsDictGroup.Tables[0].Select(string.Empty, "NODE_ID DESC");

            if (drFind.Length > 0)
            {
                return int.Parse(drFind[0]["NODE_ID"].ToString()) + 1;
            }
            else
            {
                return 1;
            }
        }


        /// <summary>
        /// 增加新的顶级分组
        /// </summary>
        private void addNewDictGroup(TreeListNode nodeParent)
        {
            TreeListNode node = null;

            // 获取新的节点ID
            string nodeIdNew = getNewNodeId().ToString();

            // 字典树中增加相应的节点
            if (nodeParent == null)
            {
                //ucTreeList1.Nodes.Add()
                node = ucTreeList1.Nodes.Add(nodeIdNew, "新分组");
            }
            else
            {
                node = nodeParent.Nodes.Add(nodeIdNew, "新分组");
            }

            node.ImageIndex = 0;
            //node.SelectedImageIndex = 0;

            node.Tag = nodeIdNew;

            // 保存到数据库中
            drDictGroup = dsDictGroup.Tables[0].NewRow();
            drDictGroup["NODE_ID"] = nodeIdNew;
            drDictGroup["NODE_TITLE"] = "新分组";

            if (nodeParent != null)
            {
                drDictGroup["PARENT_NODE_ID"] = nodeParent.GetValue("PARENT_NODE_ID");
            }

            //dsDictGroup.Tables[0].Rows.Add(drDictGroup);

            dictDbI.SaveDictGroupInfo(dsDictGroup.GetChanges());
            dsDictGroup.AcceptChanges();

            // 选中当前节点
            ucTreeList1.SelectedNode = node;
        }


        /// <summary>
        /// 删除缓存中所有子节点
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="isChlid">是否为子节点</param>
        private void deleteSubNodesCache(string nodeId, bool isChlid = false)
        {
            string filter = "PARENT_NODE_ID = " + SqlManager.SqlConvert(nodeId);
            DataRow[] drFind = dsDictGroup.Tables[0].Select(filter);

            if (drFind.Length > 0)
            {
                for (int i = 0; i < drFind.Length; i++)
                {
                    deleteSubNodesCache(drFind[i]["NODE_ID"].ToString(), true);

                    drFind[i].Delete();
                }
            }

            if (isChlid) return;
            filter = "NODE_ID = " + SqlManager.SqlConvert(nodeId);
            drFind = dsDictGroup.Tables[0].Select(filter);
            if (drFind.Length > 0)
            {
                drFind[0].Delete();
            }
        }


        /// <summary>
        /// 删除所有子节点
        /// </summary>
        /// <param name="parentNode"></param>
        private void DeleteSubNodes(TreeListNode parentNode)
        {
            foreach (TreeListNode node in parentNode.Nodes)
            {
                //DeleteSubNodes(node);
                //node.Remove();
                ucTreeList1.Delete(node);
            }

            //parentNode.Remove();
            ucTreeList1.Delete(parentNode);
        }


        /// <summary>
        /// 获取字典ID
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        private string getDictId(string nodeId)
        {
            string filter = "NODE_ID = " + SqlManager.SqlConvert(nodeId);
            DataRow[] drFind = dsDictGroup.Tables[0].Select(filter);

            if (drFind.Length > 0)
            {
                return drFind[0]["DICT_ID"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 获取配置参数
        /// </summary>
        private void getParameters()
        {
            if (this.Tag != null)
            {
                _groupId = this.Tag.ToString();
            }
        }
        #endregion

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ucGridView1.AddNewRow();
                return;
                if (dsDictItem == null) return;
                DataRow drNew = dsDictItem.Tables[0].NewRow();
                if (drDict != null)
                    drNew["DICT_ID"] = drDict["DICT_ID"].ToString();

                dsDictItem.Tables[0].Rows.Add(drNew);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }

        }

    }
}
