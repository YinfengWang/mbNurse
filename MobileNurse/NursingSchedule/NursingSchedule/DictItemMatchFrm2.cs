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


namespace HISPlus
{
    public partial class DictItemMatchFrm2 : FormDo1
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


        public DictItemMatchFrm2()
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

            InitTreeControl();
        }

        /// <summary>
        /// 初始化TreeList控件
        /// </summary>
        private void InitTreeControl()
        {

            DevExpress.XtraTreeList.Columns.TreeListColumn treeColumn1 =
                new DevExpress.XtraTreeList.Columns.TreeListColumn
                {
                    Caption = @"",
                    FieldName = "ITEM_NAME",
                    Visible = true,
                    Name = "treeColumn1",
                    VisibleIndex = 0
                };
            treeList1.Columns.AddRange(new[] { treeColumn1 });

            // 行指示器【隐藏/显示】
            treeList1.OptionsView.ShowIndicator = false;

            // 被选中的单元格的聚集框 【隐藏/显示】
            treeList1.OptionsView.ShowFocusedFrame = false;

            treeList1.DataSource = dsTreeNode.Tables[0];
            treeList1.KeyFieldName = "ITEM_ID";
            treeList1.ParentFieldName = "PARENT_ID";
        }

        private void initDisp()
        {
            // 初始化程序模块数

            btnAdd.Enabled = false;
            btnDelete.Enabled = false;

            ITEM_ID_MATCH.DisplayMember = "ITEM_NAME";
            ITEM_ID_MATCH.ValueMember = "ITEM_ID";
            ITEM_ID_MATCH.DataSource = dsRespondant.Tables[0].DefaultView;
        }
        #endregion

        private void dgvDest_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dgvDest_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDest.SelectedCells.Count > 0)
                btnDelete.Enabled = true;
        }

        private void dgvDest_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDest.SelectedCells.Count == 0)
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

                if (treeList1.FocusedNode == null)
                {
                    return;
                }

                if (itemSelector.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                DataRowView drv = treeList1.GetDataRecordByNode(treeList1.FocusedNode) as DataRowView;


                // 判断是否已经存在
                string filter = "DICT_ID = '26' AND ITEM_ID = " + SQL.SqlConvert(drv.Row["ITEM_ID"].ToString());
                filter += " AND DICT_ID_MATCH = '20' AND ITEM_ID_MATCH = " + SQL.SqlConvert(itemSelector.ItemId);

                DataRow[] drFind = dsMatch.Tables[0].Select(filter);
                if (drFind.Length > 0)
                {
                    return;
                }

                DataRow drNew = dsMatch.Tables[0].NewRow();

                drv = treeList1.GetDataRecordByNode(treeList1.FocusedNode) as DataRowView;
                if (drv != null)
                    drNew["ITEM_ID"] = drv.Row["ITEM_ID"].ToString();

                drNew["DICT_ID"] = "26";
                drNew["DICT_ID_MATCH"] = "20";
                drNew["ITEM_ID_MATCH"] = itemSelector.ItemId;

                dsMatch.Tables[0].Rows.Add(drNew);

                dgvDest.DataSource = dsMatch.Tables[0].DefaultView;

                dgvDest.ClearSelection();
                // 定位当前行
                foreach (DataGridViewRow dgvRow in dgvDest.Rows)
                {
                    if (dgvRow.Cells["ITEM_ID_MATCH"].Value.ToString().Equals(itemSelector.ItemId) == true)
                    {
                        dgvRow.Selected = true;
                        break;
                    }
                }

                btnSave.Enabled = true;
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
                if (dgvDest.CurrentCell == null)
                {
                    return;
                }

                dgvDest.Rows.Remove(dgvDest.Rows[dgvDest.CurrentCell.RowIndex]);

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

        private void treeList1_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            TreeList node = sender as TreeList;
            if (node != null && e.Node == node.FocusedNode)
            {
                e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
                Rectangle r = new Rectangle
                    (e.EditViewInfo.ContentRect.Left, e.EditViewInfo.ContentRect.Top,
                    Convert.ToInt32(e.Graphics.MeasureString(e.CellText, treeList1.Font).Width + 1),
                    Convert.ToInt32(e.Graphics.MeasureString(e.CellText, treeList1.Font).Height));
                e.Graphics.FillRectangle(SystemBrushes.Highlight, r);
                e.Graphics.DrawString(e.CellText, treeList1.Font, SystemBrushes.HighlightText, r);
                e.Handled = true;
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
                DataRowView drv = treeList1.GetDataRecordByNode(e.Node) as DataRowView;
                // 显示该疾病对应的护理诊断
                //string filter = "ITEM_ID = " + SQL.SqlConvert(e.Node.Tag.ToString());
                string filter = "ITEM_ID = " + SQL.SqlConvert(drv.Row["ITEM_ID"].ToString());
                dsMatch.Tables[0].DefaultView.RowFilter = filter;
                dsMatch.Tables[0].DefaultView.Sort = "ITEM_ID_MATCH";

                dgvDest.DataSource = dsMatch.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
    }
}
