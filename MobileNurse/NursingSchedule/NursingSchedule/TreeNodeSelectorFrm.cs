using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class TreeNodeSelectorFrm : FormDo
    {

        #region 窗体变量
        public string ItemId = string.Empty;             // 选中的项目ID
        public string ItemName = string.Empty;             // 选中的项目名称

        public ArrayList ItemIdArr = new ArrayList();
        public ArrayList ItemNameArr = new ArrayList();

        private DataSet dsTreeNode = null;

        private DataSet dsMatch = null;

        #endregion


        public TreeNodeSelectorFrm()
        {
            InitializeComponent();


            InitControl();

        }

        private void TreeNodeSelectorFrm_Load(object sender, EventArgs e)
        {
            try
            {
                InitData();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        #region 共通函数

        private void InitControl()
        {
            ucTreeList1.Add("项目名称", "ITEM_NAME");
            ucTreeList1.KeyFieldName = "ITEM_ID";
            ucTreeList1.ParentFieldName = "PARENT_ID";
            ucTreeList1.AfterSelect += ucTreeList1_AfterSelect;

            ucGridView1.MultiSelect = true;
            ucGridView1.ShowFindPanel = true;
            ucGridView1.ColumnAutoWidth = false;
            ucGridView1.Add("ID", "ITEM_ID", false);
            ucGridView1.Add("护理诊断", "ITEM_NAME", (int)(ucGridView1.Width * 1));
            ucGridView1.Add("拼写", "DESC_SPELL", 10);
            ucGridView1.DoubleClick += ucGridView1_DoubleClick;

            ucGridView1.Init();
            ucGridView1.HideScroll();
        }

        void ucGridView1_DoubleClick(object sender, EventArgs e)
        {
            btnOk.PerformClick();
        }

        void ucTreeList1_AfterSelect(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            try
            {
                if (e.Node.Nodes.Count > 0)
                {
                    dsMatch.Tables[0].DefaultView.RowFilter = "(1 = 2)";
                    return;
                }

                //string filter = "ITEM_ID like  " + SQL.SqlConvert(e.Node.GetValue("ITEM_ID").ToString() + "%");
                string filter = "ITEM_ID =  " + SQL.SqlConvert(e.Node.GetValue("ITEM_ID").ToString());
                dsMatch.Tables[0].DefaultView.RowFilter = filter;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void InitData()
        {
            if (dsTreeNode == null)
            {
                string sql = string.Empty;

                sql = "SELECT * FROM HIS_DICT_ITEM WHERE DICT_ID = '26'";
                dsTreeNode = GVars.OracleAccess.SelectData(sql, "HIS_DICT_ITEM");

                //sql = "SELECT * FROM HIS_DICT_ITEM_MATCH WHERE DICT_ID = '26' ";
                //dsMatch = GVars.OracleAccess.SelectData(sql, "HIS_DICT_ITEM_MATCH");

                //sql = "SELECT * FROM HIS_DICT_ITEM WHERE DICT_ID = '20'";
                //dsRespondant = GVars.OracleAccess.SelectData(sql, "HIS_DICT_ITEM");

                sql = @"SELECT M.ITEM_ID,
                               M.ITEM_ID_MATCH,
                               D.ITEM_NAME,
                               D.DESC_SPELL
                        FROM   HIS_DICT_ITEM_MATCH M
                        JOIN   HIS_DICT_ITEM D
                        ON     M.ITEM_ID_MATCH = D.ITEM_ID
                        WHERE  M.DICT_ID = '26'
                               AND D.DICT_ID = '20'";

                dsMatch = GVars.OracleAccess.SelectData(sql, "HIS_DICT_ITEM");
            }

            dsTreeNode.Tables[0].DefaultView.Sort = "PARENT_ID,ITEM_ID";
            ucTreeList1.DataSource = dsTreeNode.Tables[0].DefaultView;
            //ucTreeList1.RefreshData();

            dsMatch.Tables[0].DefaultView.RowFilter = string.Empty;
            ucGridView1.DataSource = dsMatch.Tables[0].DefaultView;
            //ucGridView1.RefreshData();            
        }

        #endregion


        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                ItemIdArr.Clear();
                ItemNameArr.Clear();

                if (ucGridView1.SelectRowsCount == 0)
                {
                    return;
                }

                foreach (int rowHandle in ucGridView1.SelectedRows)
                {
                    ItemIdArr.Add(ucGridView1.GetRowCellValue(rowHandle, "ITEM_ID_MATCH").ToString());
                    ItemNameArr.Add(ucGridView1.GetRowCellValue(rowHandle, "ITEM_NAME").ToString());
                }

                ItemId = ItemIdArr[0].ToString();
                ItemName = ItemNameArr[0].ToString();

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
    }
}
