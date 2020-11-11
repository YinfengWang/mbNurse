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
    public partial class OptionSelectionFrm : FormDo
    {
        #region 变量
        /// <summary>
        /// 字典集合
        /// </summary>
        public DataSet DsItemDict = null;                 // 字典集合
        /// <summary>
        /// 字典ID
        /// </summary>
        public string DictId = string.Empty;         // 字典ID
        /// <summary>
        /// 是否多选
        /// </summary>
        public bool MultiSelect = true;                 // 是否多选
        /// <summary>
        /// 选中的项目
        /// </summary>
        public string SelectedItems = string.Empty;         // 选中的项目
        /// <summary>
        /// 是否保存代码
        /// </summary>
        public bool StoreCode = true;                 // 是否保存代码
        public string frmText = string.Empty;//LB20110427新增，区别当前选择的项目类型（比如出量项目、入量项目）
        #endregion

        public OptionSelectionFrm()
        {
            InitializeComponent();

            ucGridView1.Add("代码", "ITEM_ID");
            ucGridView1.Add("名称", "ITEM_NAME");
            ucGridView1.DoubleClick += ucGridView1_DoubleClick;

            ucGridView1.Init();
        }

        void ucGridView1_DoubleClick(object sender, EventArgs e)
        {
            btnOk.PerformClick();
        }


        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionSelectFrm_Load(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;

                ucGridView1.MultiSelect = MultiSelect;

                if (SelectedItems.EndsWith(ComConst.STR.COMMA) == false)
                {
                    SelectedItems += ComConst.STR.COMMA;
                }

                //LB20110427新增，区别当前选择的项目类型（比如出量项目、入量项目）
                if (frmText.Length > 0)
                {
                    this.Text = frmText;
                }
                //结束
                showItemList();

                foreach (string str in SelectedItems.Split(Convert.ToChar(ComConst.STR.COMMA)))
                {
                    ucGridView1.SelectRow(StoreCode ? "ITEM_ID" : "ITEM_NAME", str);
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
        /// 按钮[确定]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                SelectedItems = string.Empty;

                bool blnFirst = true;

                foreach (int rowHandle in ucGridView1.SelectedRows)
                {
                    if (blnFirst == false) SelectedItems += ComConst.STR.COMMA;
                    blnFirst = false;

                    SelectedItems += (StoreCode ? ucGridView1.GetRowCellValue(rowHandle, "ITEM_ID").ToString() : ucGridView1.GetRowCellValue(rowHandle, "ITEM_NAME"));
                }
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[取消]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion


        #region 共通函数
        /// <summary>
        /// 显示项目
        /// </summary>
        private void showItemList()
        {
            if (DsItemDict == null || DsItemDict.Tables.Count == 0)
            {
                return;
            }

            string filter = "DICT_ID = " + SQL.SqlConvert(DictId);

            DsItemDict.Tables[0].DefaultView.RowFilter = filter;

            ucGridView1.DataSource = DsItemDict.Tables[0].DefaultView;

        }
        #endregion
    }
}
