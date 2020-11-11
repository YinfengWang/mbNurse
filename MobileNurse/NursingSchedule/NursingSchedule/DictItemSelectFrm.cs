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
    public partial class DictItemSelectFrm : FormDo
    {
        #region 变量
        public DataSet DsDictItem = null;                     // 字典项目

        public string DictId = string.Empty;             // 指定的字典
        public string ItemId = string.Empty;             // 选中的项目ID
        public string ItemName = string.Empty;             // 选中的项目名称

        public bool MultiSelect = false;
        public string ParentNodeId = string.Empty;
        public bool ResultContainSerialNo = false;

        private readonly string _dictIdPre = string.Empty;
        private readonly string _parentNodeId = string.Empty;
        #endregion


        public DictItemSelectFrm()
        {
            InitializeComponent();

            ucGridView1.ShowFindPanel = true;
            ucGridView1.Add("编码", "ITEM_ID", 8);
            ucGridView1.Add("项目名称", "ITEM_NAME",100);
            ucGridView1.DoubleClick += ucGridView1_DoubleClick;
            ucGridView1.Init();
        }

        private void DictItemSelectFrm_Load(object sender, EventArgs e)
        {
            try
            {
               
                ucGridView1.MultiSelect = MultiSelect;
                
                // 显示字典
                string filter = "DICT_ID = " + SQL.SqlConvert(DictId);
                if (ParentNodeId.Length > 0)
                {
                    filter += "AND PARENT_ID = " + SQL.SqlConvert(ParentNodeId);
                }

                if (_dictIdPre.Equals(DictId) == false || _parentNodeId.Equals(ParentNodeId) == false)
                {
                    string sql = "SELECT * FROM HIS_DICT_ITEM WHERE " + filter;
                    DsDictItem = GVars.OracleAccess.SelectData(sql, "HIS_DICT_ITEM");

                    DsDictItem.Tables[0].DefaultView.Sort = "ITEM_ID";
                    //DsDictItem.Tables[0].DefaultView.RowFilter = filter;
                    ucGridView1.DataSource = DsDictItem.Tables[0].DefaultView;
                }

                if (ItemId.Length > 0)
                    ucGridView1.SelectRow("ITEM_ID", ItemId);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        void ucGridView1_DoubleClick(object sender, EventArgs e)
        {
            btnOk.PerformClick();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                ItemId = ucGridView1.SelectedRow["ITEM_ID"].ToString();
                ItemName = ucGridView1.SelectedRow["ITEM_NAME"].ToString();

                if (ucGridView1.SelectRowsCount > 1)
                {
                    int index = 1;
                    ItemName = string.Empty;

                    foreach (int i in ucGridView1.SelectedRows)
                    {
                        if (ItemName.Length > 0)
                        {
                            ItemName += ComConst.STR.CRLF;
                        }

                        if (ResultContainSerialNo)
                        {
                            ItemName += index + ".";
                            index++;
                        }

                        ItemName += ucGridView1.GetRowCellValue(i, "ITEM_NAME").ToString();
                    }
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
