using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class DictItemContentFrm : FormDo
    {
        #region 变量
        protected string _dictId = "07";                     // 字典ID

        protected string _listTitle = string.Empty;             // 项目列表名称
        protected string _contentTitle = string.Empty;             // 内容的名称

        protected bool _deptOwn = true;                     // 是否是科室拥有

        private DataSet dsDictItem = null;                     // 字典项目
        private DataRow[] drItems = null;                     // 字典项目
        private DataSet dsDictItemContent = null;                     // 字典项目的内容

        private DataRow drItemContent = null;                     // 项目内容

        private HISDictDbI dictDbI = null;                     // 数据接口
        #endregion


        public DictItemContentFrm()
        {
            InitializeComponent();

            _id = "00046";
            _guid = "4047FF3A-4F8C-44a8-A4DE-FFBE2935E9EF";
        }


        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DictItemContentFrm_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (_listTitle.Length > 0)groupControl1.Text = _listTitle;
                if (_contentTitle.Length > 0) groupControl2.Text = _contentTitle;

                dictDbI = new HISDictDbI(GVars.OracleAccess);
                dsDictItem = dictDbI.GetDictItem(_dictId);
               
                ucTreeList1.KeyFieldName = "ITEM_ID";
                ucTreeList1.ParentFieldName = "PARENT_ID";
                ucTreeList1.Add( "名称","ITEM_NAME");
               
                if (_deptOwn == true)
                {
                    dsDictItemContent = dictDbI.GetDictItemContent(_dictId, GVars.User.DeptCode);
                }
                else
                {
                    dsDictItemContent = dictDbI.GetDictItemContent(_dictId, string.Empty);
                }

                ucTreeList1.DataSource = dsDictItem.Tables[0];

                showDictItem();

                setControlMaxLength();
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
        /// 显示评估数据
        /// </summary>
        private void showDictItem()
        {
            //trvDictItem.Nodes.Clear();

            //if (dsDictItem == null)
            //{
            //    return;
            //}

            //drItems = dsDictItem.Tables[0].Select(string.Empty, "ITEM_ID");

            //TreeNode nodePre = null;
            //string itemIdPre = string.Empty;

            //for (int i = 0; i < drItems.Length; i++)
            //{
            //    // 创建节点
            //    string itemId = drItems[i]["ITEM_ID"].ToString();
            //    string itemName = drItems[i]["ITEM_NAME"].ToString();

            //    TreeNode node = null;

            //    #region 增加节点
            //    // 如果是第一个节点
            //    if (nodePre == null)
            //    {
            //        node = trvDictItem.Nodes.Add(itemName);
            //    }
            //    else
            //    {
            //        // 如果ID长度相同, 表示是同一层
            //        if (itemIdPre.Length == itemId.Length)
            //        {
            //            if (nodePre.Parent == null)
            //            {
            //                node = trvDictItem.Nodes.Add(itemName);
            //            }
            //            else
            //            {
            //                node = nodePre.Parent.Nodes.Add(itemName);
            //            }
            //        }
            //        // 如果ID长度大于前一个ID, 表示是子项目
            //        else if (itemIdPre.Length < itemId.Length)
            //        {
            //            node = nodePre.Nodes.Add(itemName);
            //        }
            //        // 如果ID小于于前一个ID, 表示父项目的兄弟
            //        else
            //        {
            //            do
            //            {
            //                nodePre = nodePre.Parent;

            //                if (nodePre != null)
            //                {
            //                    itemIdPre = drItems[(int)(nodePre.Tag)]["ITEM_ID"].ToString();
            //                }
            //                else
            //                {
            //                    itemIdPre = string.Empty;
            //                }

            //            } while (nodePre != null && itemId.Length < itemIdPre.Length);

            //            if (nodePre == null || nodePre.Parent == null)
            //            {
            //                node = trvDictItem.Nodes.Add(itemName);
            //            }
            //            else
            //            {
            //                if (nodePre.Parent == null)
            //                {
            //                    node = trvDictItem.Nodes.Add(itemName);
            //                }
            //                else
            //                {
            //                    node = nodePre.Parent.Nodes.Add(itemName);
            //                }
            //            }
            //        }
            //    }
            //    #endregion

            //    node.Tag = i;
            //    nodePre = node;
            //    itemIdPre = itemId;
            //}
        }


        /// <summary>
        /// 字典项目选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvDictItem_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;

                drItemContent = null;

                txtContent.Text = string.Empty;
                txtContent.Enabled = false;
                btnSave.Enabled = false;

                if (e.Node.Nodes.Count > 0)
                {
                    return;
                }

                // 获取项目ID
                //DataRow dr = drItems[(int)(e.Node.Tag)];
                DataRowView dr = ucTreeList1.SelectedRow  ;
                string itemId = dr["ITEM_ID"].ToString();

                // 获取项目内容
                string filter = "ITEM_ID = " + SqlManager.SqlConvert(itemId);
                DataRow[] drFind = dsDictItemContent.Tables[0].Select(filter);

                if (drFind.Length > 0)
                {
                    drItemContent = drFind[0];

                    txtContent.Text = drItemContent["CONTENT"].ToString();
                }

                txtContent.Enabled = true;
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
        /// 项目内容改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtContent_TextChanged(object sender, EventArgs e)
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
        /// 按钮[保存]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool blnAdd = false;

                if (drItemContent == null)
                {
                    blnAdd = true;
                    drItemContent = dsDictItemContent.Tables[0].NewRow();
                }

                //DataRow drItem = drItems[(int)(trvDictItem.SelectedNode.Tag)];
                DataRowView drItem = ucTreeList1.SelectedRow;
                drItemContent["DICT_ID"] = _dictId;
                drItemContent["ITEM_ID"] = drItem["ITEM_ID"].ToString();
                drItemContent["DEPT_CODE"] = GVars.User.DeptCode;
                drItemContent["CONTENT"] = txtContent.Text.Trim();

                if (blnAdd)
                {
                    dsDictItemContent.Tables[0].Rows.Add(drItemContent);
                }

                if (_deptOwn == true)
                {
                    dictDbI.SaveDictItemContent(dsDictItemContent.GetChanges(), _dictId, GVars.User.DeptCode);
                }
                else
                {
                    dictDbI.SaveDictItemContent(dsDictItemContent.GetChanges(), _dictId, string.Empty);
                }

                dsDictItemContent.AcceptChanges();

                btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 设置控件最大长度
        /// </summary>
        private void setControlMaxLength()
        {
            if (dsDictItemContent != null && dsDictItemContent.Tables.Count > 0)
            {
                txtContent.MaxLength = (int)(dsDictItemContent.Tables[0].Columns["CONTENT"].MaxLength / 2);
            }
        }

        private void ucTreeList1_AfterSelect(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;

                drItemContent = null;

                txtContent.Text = string.Empty;
                txtContent.Enabled = false;
                btnSave.Enabled = false;

                if (e.Node.Nodes.Count > 0)
                {
                    return;
                }

                // 获取项目ID
                //DataRow dr = drItems[(int)(e.Node.Tag)];
                DataRowView dr = ucTreeList1.SelectedRow ;
                string itemId = dr["ITEM_ID"].ToString();

                // 获取项目内容
                string filter = "ITEM_ID = " + SqlManager.SqlConvert(itemId);
                DataRow[] drFind = dsDictItemContent.Tables[0].Select(filter);

                if (drFind.Length > 0)
                {
                    drItemContent = drFind[0];

                    txtContent.Text = drItemContent["CONTENT"].ToString();
                }

                txtContent.Enabled = true;
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
    }
}
