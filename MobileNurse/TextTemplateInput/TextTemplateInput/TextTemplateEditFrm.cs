using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class TextTemplateEditFrm : Form
    {
        #region 变量
        private const int LEVEL_LEN = 2;                        // 节点分层长度
        protected string _dictId = "16";                     // 字典ID
        private string rootName = string.Empty;             // 根节点名称

        private DbInfo dbAccess = null;
        public DataSet dsDictItem = null;                     // 字典项目
        public DataSet dsDictItemContent = null;                     // 字典项目内容

        public bool TemplateChanged = false;                    // 模板是否发生改变
        #endregion


        public TextTemplateEditFrm()
        {
            InitializeComponent();
        }

        #region 属性
        public string DictId
        {
            get
            {
                return _dictId;
            }
            set
            {
                if (_dictId.Equals(value) == false)
                {
                    _dictId = value;
                }
            }
        }
        #endregion


        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextTemplateInputFrm_Load(object sender, EventArgs e)
        {
            try
            {
                this.timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 在时钟事件中守成初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                this.timer1.Enabled = false;

                initFrmVal();
                initDisp();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvTemplate_KeyUp(object sender, KeyEventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;

                // 条件检查
                if (e.Modifiers != Keys.Control)
                {
                    return;
                }

                e.Handled = true;

                if (trvTemplate.SelectedNode == null)
                {
                    return;
                }

                // 缓存状态
                TreeNode node = trvTemplate.SelectedNode;

                TreeNode parentNode = node.Parent;
                string parentNodeId = (parentNode == null ? string.Empty : parentNode.Tag.ToString());

                // 移动节点 向上
                if (e.KeyCode == Keys.Up)
                {
                    if (TreeViewHelper.NodeMoveUp(ref trvTemplate, ref node) == true)
                    {
                        trvTemplate.SelectedNode = node;
                        btnSaveOrder.Enabled = true;
                    }
                }

                // 移动节点 向下
                if (e.KeyCode == Keys.Down)
                {
                    if (TreeViewHelper.NodeMoveDown(ref trvTemplate, ref node) == true)
                    {
                        trvTemplate.SelectedNode = node;
                        btnSaveOrder.Enabled = true;
                    }
                }
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
        /// 节点选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvTemplate_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                GVars.App.UserInput = false;

                txtTemplateContent.Text = string.Empty;
                btnDelete.Enabled = false;

                if (trvTemplate.SelectedNode == null || trvTemplate.SelectedNode.Parent == null)
                {
                    return;
                }

                string nodeId = trvTemplate.SelectedNode.Tag.ToString();
                txtTemplateTitle.Text = trvTemplate.SelectedNode.Text;

                if (nodeId.Length > 0)
                {
                    string filter = "ITEM_ID = " + SQL.SqlConvert(nodeId);
                    DataRow[] drFind = dsDictItemContent.Tables[0].Select(filter);

                    if (drFind.Length > 0)
                    {
                        txtTemplateContent.Text = drFind[0]["CONTENT"].ToString();

                    }
                }

                btnDelete.Enabled = true;
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
        /// 文本改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTemplateTitle_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 文本改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTemplateContent_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[删除模板]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (GVars.Msg.Show("Q0006", "删除", "当前节点") != DialogResult.Yes)    // 您确认要 {0} {1} 吗?
                {
                    return;
                }

                string itemId = trvTemplate.SelectedNode.Tag.ToString();
                string filter = "DICT_ID = " + SQL.SqlConvert(_dictId)
                                + "AND ITEM_ID = " + SQL.SqlConvert(itemId);

                DataRow[] drFind = dsDictItem.Tables[0].Select(filter);
                for (int i = 0; i < drFind.Length; i++)
                {
                    drFind[i].Delete();
                }

                drFind = dsDictItemContent.Tables[0].Select(filter);
                for (int i = 0; i < drFind.Length; i++)
                {
                    drFind[i].Delete();
                }

                ArrayList arrDataSet = new ArrayList();
                arrDataSet.Add(dsDictItem);
                arrDataSet.Add(dsDictItemContent);

                if (dbAccess.SaveTableData(arrDataSet) == true)
                {
                    dsDictItem.AcceptChanges();
                    dsDictItemContent.AcceptChanges();

                    trvTemplate.SelectedNode.Remove();

                    btnDelete.Enabled = false;

                    GVars.Msg.Show("I0009", "删除");            // {0}成功!
                }
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
            try
            {
                if (chkDisp() == false)
                {
                    GVars.Msg.Show();
                    return;
                }

                if (saveDisp() == true)
                {
                    TemplateChanged = true;

                    btnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 保存排序结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveTreeNodeSerialNo(trvTemplate.Nodes[0]) == true)
                {
                    if (dbAccess.SaveTableData(dsDictItem) == true)
                    {
                        btnSaveOrder.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[退出]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion


        #region 共通函数
        /// <summary>
        /// 初始化窗体变量
        /// </summary>
        private void initFrmVal()
        {
            TemplateChanged = false;

            dbAccess = new DbInfo(GVars.OracleAccess);

            string sql = "SELECT DESCRIPTIONS FROM HIS_DICT WHERE DICT_ID = " + SQL.SqlConvert(_dictId);
            if (dbAccess.SelectValue(sql) == true)
            {
                rootName = dbAccess.GetResult(0);
            }
        }


        /// <summary>
        /// 初始化窗体显示
        /// </summary>
        private void initDisp()
        {
            // 重置树
            resetTree();
        }


        /// <summary>
        /// 重新树
        /// </summary>
        private void resetTree()
        {
            // 清除所有节点
            trvTemplate.Nodes.Clear();          // 清空所有节点

            // 增加根节点           
            TreeNode rootNode = new TreeNode(rootName);
            rootNode.Tag = string.Empty;
            trvTemplate.Nodes.Add(rootNode);

            // 条件检查
            if (dsDictItem.Tables.Count == 0 || dsDictItem.Tables[0].Rows.Count == 0)
            {
                return;
            }

            DataRow[] rows = dsDictItem.Tables[0].Select("PARENT_ID IS NULL OR PARENT_ID = ''", "SERIAL_NO");

            foreach (DataRow dr in rows)
            {
                TreeNode nodeNew = new TreeNode(dr["ITEM_NAME"].ToString());
                nodeNew.Tag = dr["ITEM_ID"].ToString();

                rootNode.Nodes.Add(nodeNew);

                // 增加子节点
                addTreeSubNodes(nodeNew);
            }
        }


        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="parentNode"></param>
        private void addTreeSubNodes(TreeNode parentNode)
        {
            string parentNodeId = parentNode.Tag.ToString();

            string filter = "PARENT_ID = " + SQL.SqlConvert(parentNodeId);
            DataRow[] rows = dsDictItem.Tables[0].Select(filter, "SERIAL_NO");

            foreach (DataRow dr in rows)
            {
                TreeNode nodeNew = new TreeNode(dr["ITEM_NAME"].ToString());
                nodeNew.Tag = dr["ITEM_ID"].ToString();

                parentNode.Nodes.Add(nodeNew);

                // 增加子节点
                addTreeSubNodes(nodeNew);
            }
        }


        private bool chkDisp()
        {
            // 必须输入模板名称
            if (txtTemplateTitle.Text.Trim().Length == 0)
            {
                GVars.Msg.MsgId = "E0012";                              // 必须输入{0}!
                GVars.Msg.MsgContent.Add(groupBox2.Text);
                GVars.Msg.ErrorSrc = txtTemplateTitle;
                return false;
            }

            // 请选择节点
            if (trvTemplate.SelectedNode == null)
            {
                GVars.Msg.MsgId = "I0010";                              // 必须选择{0}!
                GVars.Msg.MsgContent.Add("节点");
                GVars.Msg.ErrorSrc = trvTemplate;
                return false;
            }

            // 输入长度限制
            int maxLength = dsDictItem.Tables[0].Columns["ITEM_NAME"].MaxLength;
            if (maxLength > 0)
            {
                if (maxLength < Coding.GetStrByteLen(txtTemplateTitle.Text.Trim()))
                {
                    GVars.Msg.MsgId = "E0014";                          // {0}输入超长
                    GVars.Msg.MsgContent.Add(groupBox2.Text);
                    GVars.Msg.ErrorSrc = txtTemplateTitle;
                    return false;
                }
            }

            maxLength = dsDictItemContent.Tables[0].Columns["CONTENT"].MaxLength;
            if (maxLength > 0)
            {
                if (maxLength < Coding.GetStrByteLen(txtTemplateContent.Text.TrimEnd()))
                {
                    GVars.Msg.MsgId = "E0014";                          // {0}输入超长
                    GVars.Msg.MsgContent.Add(groupBox4.Text);
                    GVars.Msg.ErrorSrc = txtTemplateContent;
                    return false;
                }
            }

            return true;
        }


        private bool saveDisp()
        {
            string parentItemId = trvTemplate.SelectedNode.Tag.ToString();
            string filter = string.Empty;
            DataRow[] drFind = null;

            // 如果模板名称与选中节点名称相同, 表示是修改选中节点的模板内容
            string title = txtTemplateTitle.Text.Trim();
            string content = txtTemplateContent.Text.TrimEnd();

            if (trvTemplate.SelectedNode.Text.Equals(title) == true && parentItemId.Length > 0)
            {
                filter = "ITEM_ID = " + SQL.SqlConvert(parentItemId);
                drFind = dsDictItemContent.Tables[0].Select(filter);
                if (drFind.Length > 0)
                {
                    drFind[0]["CONTENT"] = content;

                    return dbAccess.SaveTableData(dsDictItemContent);
                }
            }

            // 保存为当前节点的子节点
            string itemId = getNewItemId(parentItemId);
            DataRow drNew = dsDictItem.Tables[0].NewRow();
            drNew["DICT_ID"] = _dictId;
            drNew["ITEM_ID"] = itemId;
            drNew["ITEM_NAME"] = title;
            drNew["PARENT_ID"] = parentItemId;
            drNew["SERIAL_NO"] = trvTemplate.SelectedNode.Nodes.Count.ToString();
            dsDictItem.Tables[0].Rows.Add(drNew);

            filter = "ITEM_ID = " + SQL.SqlConvert(drNew["ITEM_ID"].ToString());
            drFind = dsDictItemContent.Tables[0].Select(filter);
            if (drFind.Length == 0)
            {
                drNew = dsDictItemContent.Tables[0].NewRow();

                drNew["DICT_ID"] = _dictId;
                drNew["ITEM_ID"] = itemId;
                drNew["DEPT_CODE"] = "*";
            }

            drNew["CONTENT"] = content;

            if (drFind.Length == 0)
            {
                dsDictItemContent.Tables[0].Rows.Add(drNew);
            }

            ArrayList arrDataSet = new ArrayList();
            arrDataSet.Add(dsDictItem);
            arrDataSet.Add(dsDictItemContent);
            if (dbAccess.SaveTableData(arrDataSet) == false)
            {
                dsDictItem.RejectChanges();
                dsDictItemContent.RejectChanges();

                return false;
            }

            dsDictItem.AcceptChanges();
            dsDictItemContent.AcceptChanges();

            // 当前树增加新节点
            TreeNode node = new TreeNode(title);
            node.Tag = itemId;
            trvTemplate.SelectedNode.Nodes.Add(node);

            return true;
        }


        /// <summary>
        /// 获取某一节点下的新节点ID
        /// </summary>
        /// <param name="parentItemId"></param>
        /// <returns></returns>
        private string getNewItemId(string parentItemId)
        {
            string filter = string.Empty;
            if (parentItemId.Length == 0)
            {
                filter = "PARENT_ID IS NULL OR PARENT_ID = ''";
            }
            else
            {
                filter = "PARENT_ID = " + SQL.SqlConvert(parentItemId);
            }

            DataRow[] drFind = dsDictItem.Tables[0].Select(filter, "ITEM_ID");

            if (drFind.Length == 0)
            {
                return parentItemId + 1.ToString(new string('0', LEVEL_LEN));
            }
            else
            {
                long itemId = 0;
                long i = 0;
                long parentValue = 0;

                if (parentItemId.Length > 0)
                {
                    parentValue = long.Parse(parentItemId);
                }

                for (int j = 0; j < LEVEL_LEN; j++)
                {
                    parentValue *= 10;
                }

                for (i = 0; i < drFind.Length; i++)
                {
                    if (long.TryParse(drFind[i]["ITEM_ID"].ToString(), out itemId) == true)
                    {
                        if (parentValue + (i + 1) != itemId)
                        {
                            return parentItemId + (i + 1).ToString(new string('0', LEVEL_LEN));
                        }
                    }
                }

                return parentItemId + (i + 1).ToString(new string('0', LEVEL_LEN));
            }

            throw new Exception("获取新节点ID失败");
        }


        /// <summary>
        /// 保存树节点的序列号
        /// </summary>
        /// <returns></returns>
        private bool saveTreeNodeSerialNo(TreeNode parentNode)
        {
            // 条件检查
            if (parentNode == null)
            {
                if (trvTemplate.Nodes.Count == 0)
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
            TreeNode nodeFirst = null;
            if (parentNode == null)
            {
                nodeFirst = trvTemplate.Nodes[0];
            }
            else
            {
                nodeFirst = parentNode.FirstNode;
            }

            // 保存
            TreeNode node = nodeFirst;
            DataRow drEdit = null;
            int index = 0;

            string filter = "ITEM_ID = '{0}'";
            DataRow[] drFind = null;

            do
            {
                drFind = dsDictItem.Tables[0].Select(string.Format(filter, node.Tag.ToString()));
                drEdit = drFind[0];

                if (drEdit["SERIAL_NO"].ToString().Equals(index.ToString()) == false)
                {
                    drEdit["SERIAL_NO"] = index.ToString();
                }

                // 保存子节点
                if (node.Nodes.Count > 0)
                {
                    saveTreeNodeSerialNo(node);
                }

                // 下一个节点
                node = node.NextNode;
                index++;
            } while (node != null);

            return true;
        }
        #endregion

    }
}
