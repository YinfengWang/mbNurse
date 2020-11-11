using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class OptionSelectionFrm1 : Form
    {
        #region 变量
        public DataSet      DsItemDict      = null;                 // 字典集合
        public string       DictId          = string.Empty;         // 字典ID
        public bool         MultiSelect     = true;                 // 是否多选
        public string       SelectedItems   = string.Empty;         // 选中的项目
        public bool         StoreCode       = true;                 // 是否保存代码
        #endregion
        
        public OptionSelectionFrm1()
        {
            InitializeComponent();
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
                
                showItemList();
                showSelectedItems();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }
        
        
        /// <summary>
        /// 项目选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwItems_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                GVars.App.UserInput = false;
                
                if (e.Item.Checked == false) return;
                
                if (MultiSelect == false)
                {                
                    foreach(ListViewItem item in lvwItems.Items)
                    {
                        if (item == null)
                        {
                            continue;
                        }
                        
                        if (item.Equals(e.Item) == false && item.Checked == true)
                        {
                            item.Checked = false;
                        }
                    }
                }
            }
            catch(Exception ex)
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
                foreach(ListViewItem item in lvwItems.Items)
                {   
                    if (item.Checked == true)
                    {
                        if (blnFirst == false) SelectedItems += ComConst.STR.COMMA;
                        blnFirst = false;
                        
                        SelectedItems += (StoreCode? item.Tag.ToString(): item.SubItems[1].Text);
                    }
                }
                
                DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
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
            catch(Exception ex)
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
            DataRow[] drFind = DsItemDict.Tables[0].Select(filter, "ITEM_ID");
            
            lvwItems.Items.Clear();
            for(int i = 0; i < drFind.Length; i++)
            {
                ListViewItem item = lvwItems.Items.Add(drFind[i]["ITEM_ID"].ToString());
                item.SubItems.Add(drFind[i]["ITEM_NAME"].ToString());                
                item.Tag = drFind[i]["ITEM_ID"].ToString();                                
            }
        }
        
        
        /// <summary>
        /// 显示病人评估
        /// </summary>
        private void showSelectedItems()
        {
            if (SelectedItems.EndsWith(ComConst.STR.COMMA) == false)
            {
                SelectedItems += ComConst.STR.COMMA;
            }
            
            foreach(ListViewItem item in lvwItems.Items)
            {
                string itemId = item.Tag.ToString() + ComConst.STR.COMMA;
                string itemName = item.SubItems[1].Text + ComConst.STR.COMMA;
                
                if (StoreCode == true)
                {
                    if (SelectedItems.IndexOf(itemId) >= 0)
                    {
                        item.Checked = true;                    
                    }
                }
                else
                {
                    if (SelectedItems.IndexOf(itemName) >= 0)
                    {
                        item.Checked = true;                    
                    }                    
                }
            }
        }
        #endregion
    }
}
