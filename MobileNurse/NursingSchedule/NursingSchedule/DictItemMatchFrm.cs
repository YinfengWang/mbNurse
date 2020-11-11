using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using SQL = HISPlus.SqlManager;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;


namespace HISPlus
{
    public partial class DictItemMatchFrm : FormDo1
    {

        #region 窗体变量
        public string ItemId = string.Empty;             // 选中的项目ID
        public string ItemName = string.Empty;             // 选中的项目名称

        private DataSet dsTreeNode = null;
        private DataSet dsMatch = null;
        private DataSet dsRespondant = null;

        private DictItemSelectFrm itemSelector = null;
        private TreeNode nodePre = null;
        #endregion


        public DictItemMatchFrm()
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

                ucGridView1.Add("护理诊断", "ITEM_ID_MATCH", dsRespondant.Tables[0], "ITEM_ID", "ITEM_NAME");
                ucGridView1.DataSource = dsMatch.Tables[0].DefaultView;
                ucGridView1.SelectionChanged += new EventHandler(ucGridView1_SelectionChanged);
                ucGridView1.Init();
            }
            catch (Exception ex)
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

            ucTreeList1.Add("", "ITEM_ID", false);
            ucTreeList1.Add("疾病", "ITEM_NAME");
            ucTreeList1.KeyFieldName = "ITEM_ID";
            ucTreeList1.ParentFieldName = "PARENT_ID";
            ucTreeList1.AfterSelect += treeList1_AfterFocusNode;
            ucTreeList1.DataSource = dsTreeNode.Tables[0];
        }

        private void initDisp()
        {
            // 初始化程序模块数

            btnAdd.Enabled = false;
            btnDelete.Enabled = false;            
        }

        void ucGridView1_SelectionChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = ucGridView1.SelectRowsCount > 0;
        }
        #endregion

        /// <summary>
        /// 铵钮[新增]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucTreeList1.SelectedNode== null)
                {
                    return;
                }
                if (itemSelector.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                DataRowView drv = ucTreeList1.GetDataRecordByNode(ucTreeList1.FocusedNode) as DataRowView;

                // 判断是否已经存在
                string filter = "DICT_ID = '26' AND ITEM_ID = " + SQL.SqlConvert(drv.Row["ITEM_ID"].ToString());
                filter += " AND DICT_ID_MATCH = '20' AND ITEM_ID_MATCH = " + SQL.SqlConvert(itemSelector.ItemId);

                DataRow[] drFind = dsMatch.Tables[0].Select(filter);
                if (drFind.Length > 0)
                {
                    ucGridView1.SelectRow("ITEM_ID_MATCH", itemSelector.ItemId);                    
                    return;
                }

                DataRow drNew = dsMatch.Tables[0].NewRow();

                drv = ucTreeList1.GetDataRecordByNode(ucTreeList1.FocusedNode) as DataRowView;
                if (drv != null)
                    drNew["ITEM_ID"] = drv.Row["ITEM_ID"].ToString();

                drNew["DICT_ID"] = "26";
                drNew["DICT_ID_MATCH"] = "20";
                drNew["ITEM_ID_MATCH"] = itemSelector.ItemId;

                dsMatch.Tables[0].Rows.Add(drNew);

                ucGridView1.DataSource = dsMatch.Tables[0].DefaultView;

                ucGridView1.SelectRow("ITEM_ID_MATCH", itemSelector.ItemId);

                btnSave.Enabled = true;
                btnDelete.Enabled = true;
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
                if (ucGridView1.SelectRowsCount == 0)
                {
                    return;
                }

                ucGridView1.DeleteSelectRow();

                btnSave.Enabled = true;
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
                GVars.OracleAccess.Update(ref dsMatch);
                dsMatch.AcceptChanges();

                btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void treeList1_AfterFocusNode(object sender, NodeEventArgs e)
        {
            try
            {
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
                btnDelete.Enabled = true;
                DataRowView drv =ucTreeList1.GetDataRecordByNode(e.Node) as DataRowView;
                // 显示该疾病对应的护理诊断
                //string filter = "ITEM_ID = " + SQL.SqlConvert(e.Node.Tag.ToString());
                string filter = "ITEM_ID = " + SQL.SqlConvert(drv.Row["ITEM_ID"].ToString());
                dsMatch.Tables[0].DefaultView.RowFilter = filter;
                dsMatch.Tables[0].DefaultView.Sort = "ITEM_ID_MATCH";
                                
                ucGridView1.DataSource = dsMatch.Tables[0].DefaultView;
                //ucGridView1.Show();

            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
     

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            btnDelete.Enabled =ucGridView1.SelectRowsCount > 0;
        }
    }
}
