using System;
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
    public partial class DictItemMatchFrm1 : FormDo1
    {
                
        #region 窗体变量
        public string   ItemId          = string.Empty;             // 选中的项目ID
        public string   ItemName        = string.Empty;             // 选中的项目名称
        
        private DataSet  dsTreeNode     = null;
        private DataSet  dsMatch        = null;
        private DataSet  dsRespondant   = null;
        
        private DictItemSelectFrm itemSelector = null;
        private TreeNode nodePre        = null;
        #endregion
        

        public DictItemMatchFrm1()
        {
            InitializeComponent();
            
            _id = "00059";
            _guid = "23E11CB2-ED4D-4587-9396-44B7D53123ED";
        }
        
        
        private void DictItemMatchFrm_Load(object sender, EventArgs e)
        {
            try
            {
                initFrmVal();
                initDisp();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        #region 共通函数
        private void initFrmVal()
        {
            string sql = "SELECT * FROM HIS_DICT_ITEM WHERE DICT_ID = '26'";
            dsTreeNode = GVars.OracleAccess.SelectData(sql, "HIS_DICT_ITEM");

            sql = "SELECT * FROM HIS_DICT_ITEM_MATCH WHERE DICT_ID = '26'order by ITEM_ID ASC ";
            dsMatch = GVars.OracleAccess.SelectData(sql, "HIS_DICT_ITEM_MATCH");
            
            sql = "SELECT * FROM HIS_DICT_ITEM WHERE DICT_ID = '20'";
            dsRespondant = GVars.OracleAccess.SelectData(sql, "HIS_DICT_ITEM");
            
            itemSelector = new DictItemSelectFrm();
            itemSelector.MultiSelect = false;
            itemSelector.DictId = "20";
        }
        

        private void initDisp()
        {
            // 初始化程序模块数
            resetTreeNodes();
            
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            
            ITEM_ID_MATCH.DisplayMember = "ITEM_NAME";
            ITEM_ID_MATCH.ValueMember = "ITEM_ID";
            ITEM_ID_MATCH.DataSource    = dsRespondant.Tables[0].DefaultView;
        }
        
        
        private void resetTreeNodes()
        {
            // 清除所有节点
            trvSrc.Nodes.Clear();                               // 清空所有节点
            
            // 条件检查
			if (dsTreeNode == null || dsTreeNode.Tables.Count == 0 || dsTreeNode.Tables[0].Rows.Count == 0)
			{
				return;
			}
            
            // 增加根节点
            DataRow[] rows = dsTreeNode.Tables[0].Select("PARENT_ID IS NULL OR PARENT_ID = ''", "ITEM_ID ASC");
                        
            foreach (DataRow dr in rows)
            {
                TreeNode nodeNew = new TreeNode(dr["ITEM_NAME"].ToString());
                nodeNew.Tag = dr["ITEM_ID"].ToString();
                trvSrc.Nodes.Add(nodeNew);
                
                // 增加子节点
                addTreeSubNodes(nodeNew);
            }
        }
        

        private void addTreeSubNodes(TreeNode parentNode)
        {
            string parentNodeId = parentNode.Tag.ToString();
            string filter = "PARENT_ID = " + SQL.SqlConvert(parentNodeId);
            
            DataRow[] rows = dsTreeNode.Tables[0].Select(filter, "ITEM_ID ASC");
            
            foreach (DataRow dr in rows)
            {
                TreeNode nodeNew = new TreeNode(dr["ITEM_NAME"].ToString());
                nodeNew.Tag = dr["ITEM_ID"].ToString();
                
                parentNode.Nodes.Add(nodeNew);
                
                // 增加子节点
                addTreeSubNodes(nodeNew);
            }
        }
        #endregion

        
        private void trvSrc_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {   
                // 当前节点颜色设置
                e.Node.BackColor = Color.Blue;
                e.Node.ForeColor = Color.White;
                nodePre = e.Node;
                
                // 对于非叶子节点, 退出
                if (e.Node.Nodes.Count > 0)
                {
                    btnAdd.Enabled = false;
                    btnDelete.Enabled = false;
                    
                    dsMatch.Tables[0].DefaultView.RowFilter = "(1 = 2)";
                    return;
                }
                
                // 对于叶子节点, 显示对应项目
                btnAdd.Enabled = true;
                
                // 显示该疾病对应的护理诊断
                string filter = "ITEM_ID = " + SQL.SqlConvert(e.Node.Tag.ToString());
                dsMatch.Tables[0].DefaultView.RowFilter = filter;
                dsMatch.Tables[0].DefaultView.Sort = "ITEM_ID_MATCH";
                
                dgvDest.DataSource = dsMatch.Tables[0].DefaultView;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void dgvDest_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
        
        private void dgvDest_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            btnDelete.Enabled = true;
        }

        private void dgvDest_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            btnDelete.Enabled = false;
        }
        
        
        /// <summary>
        /// 铵钮[新增]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (trvSrc.SelectedNode == null)
                {
                    return;
                }
                
                if (itemSelector.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                
                // 判断是否已经存在
                string filter = "DICT_ID = '26' AND ITEM_ID = " + SQL.SqlConvert(trvSrc.SelectedNode.Tag.ToString());
                filter += " AND DICT_ID_MATCH = '20' AND ITEM_ID_MATCH = " + SQL.SqlConvert(itemSelector.ItemId);
                
                DataRow[] drFind = dsMatch.Tables[0].Select(filter);
                if (drFind.Length > 0)
                {
                    return;
                }
                
                DataRow drNew = dsMatch.Tables[0].NewRow();
                
                drNew["DICT_ID"]        = "26";
                drNew["ITEM_ID"]        = trvSrc.SelectedNode.Tag.ToString();
                drNew["DICT_ID_MATCH"]  = "20";
                drNew["ITEM_ID_MATCH"]  = itemSelector.ItemId;
                
                dsMatch.Tables[0].Rows.Add(drNew);
                
                dgvDest.DataSource = dsMatch.Tables[0].DefaultView;
                
                // 定位当前行
                foreach(DataGridViewRow dgvRow in dgvDest.Rows)
                {
                    if (dgvRow.Cells["ITEM_ID_MATCH"].Value.ToString().Equals(itemSelector.ItemId) == true)
                    {
                        dgvRow.Selected = true;
                        break;
                    }
                }
                
                btnSave.Enabled = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDest.CurrentCell == null)
                {
                    return;
                }
                
                dgvDest.Rows.Remove(dgvDest.Rows[dgvDest.CurrentCell.RowIndex]);
                
                btnSave.Enabled = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                GVars.OracleAccess.Update(ref dsMatch);
                dsMatch.AcceptChanges();
                
                btnSave.Enabled = false;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        
        private void trvSrc_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (nodePre != null)
            {
                nodePre.BackColor = SystemColors.Control;
                nodePre.ForeColor = Color.Black;
            }
        }        
    }
}
