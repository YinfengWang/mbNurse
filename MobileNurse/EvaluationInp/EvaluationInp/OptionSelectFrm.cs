using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class OptionSelectFrm : FormDo
    {
        #region 变量
        public string      SelectItems   = string.Empty;             

        protected string   _patientId    = string.Empty;             
        protected string   _visitId      = string.Empty;             

        protected string   _dictId       = string.Empty;                // 字典ID
        protected string   _parentItemId = string.Empty;                // 父节点ID

        protected DataSet  _dsItemDict   = null;                        // 评估项目
        protected DataSet  _dsEvalRec    = null;                        // 评估记录
        protected DateTime _dtRec        = DataType.DateTime_Null();    // 记录日期
        
        private bool       multiSelect  = true;
        #endregion
        
        public OptionSelectFrm()
        {
            InitializeComponent();
        }
        
        
        #region 属性
        public string PatientId
        {
            get { return _patientId; }
            set { _patientId = value; }
        }
        
        
        public string VisitId
        {
            get { return _visitId; }
            set { _visitId = value; }
        }
        
        
        public string DictId
        {
            get { return _dictId; }
            set { _dictId = value; }
        }
        
        
        public DateTime DateTimeRec
        {
            get { return _dtRec; }
            set { _dtRec = value; }
        }
        
        public string ParentItemId
        {
            get { return _parentItemId;}
            set { _parentItemId = value;}
        }
        
        
        public DataSet DsItemDict
        {
            get { return  _dsItemDict; }
            set { _dsItemDict = value; }
        }
        
        
        public DataSet DsEvalRec
        {
            get { return _dsEvalRec; }
            set { _dsEvalRec = value;}
        }
        #endregion
        
        
        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionSelectFrm_Load(object sender, EventArgs e)
        {
            try
            {
                showItemList();
                showEvaluationRec();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 项目选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwItems_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                if (e.Item.Checked == false) return;
                
                if (multiSelect == false)
                {                
                    foreach(ListViewItem item in lvwItems.Items)
                    {
                        if (item.Equals(e.Item) == false)
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
                SelectItems = string.Empty;
                
                if (saveEvaluationRec() == false)
                {
                    GVars.Msg.Show("E0005", "保存");            // {0}失败!
                    return;
                }
                
                bool blnFirst = true;
                foreach(ListViewItem item in lvwItems.Items)
                {                    
                    if (item.Checked == true)
                    {
                        if (blnFirst == false) SelectItems += ",";
                        SelectItems += item.SubItems[1].Text;                    
                        blnFirst = false;
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
            multiSelect = true;
            
            if (_dsItemDict == null || _dsItemDict.Tables.Count == 0)
            {
                return;
            }
            
            string filter = "ITEM_ID LIKE '" + _parentItemId + "%' AND ITEM_ID <> " + SqlManager.SqlConvert(_parentItemId);
            DataRow[] drFind = _dsItemDict.Tables[0].Select(filter, "ITEM_ID");
            
            lvwItems.Items.Clear();
            for(int i = 0; i < drFind.Length; i++)
            {
                ListViewItem item = lvwItems.Items.Add((i + 1).ToString());
                item.SubItems.Add(drFind[i]["ITEM_NAME"].ToString());
                
                if (drFind[i]["FLAG"].ToString().Equals("1") == true)
                {
                    multiSelect = false;
                }
                
                item.Tag = drFind[i]["ITEM_ID"].ToString();
            }
        }
        
        
        /// <summary>
        /// 显示病人评估
        /// </summary>
        private void showEvaluationRec()
        {
            if (_dsEvalRec == null || _dsEvalRec.Tables.Count == 0)
            {
                return;
            }
            
            string filter = "ITEM_ID LIKE '" + _parentItemId + "%' AND ITEM_ID <> " + SqlManager.SqlConvert(_parentItemId);
            DataRow[] drFind = _dsEvalRec.Tables[0].Select(filter, "ITEM_ID");
            
            int idx_rec = 0;
            
            for(int i = 0; i < lvwItems.Items.Count; i++)
            {
                ListViewItem item = lvwItems.Items[i];
                string itemId = item.Tag.ToString();
                
                if (idx_rec >= drFind.Length)
                {
                    break;
                }
                
                int compare = itemId.CompareTo(drFind[idx_rec]["ITEM_ID"].ToString());
                
                while(compare > 0 && idx_rec < drFind.Length - 1)
                {
                    idx_rec++;
                    compare = itemId.CompareTo(drFind[idx_rec]["ITEM_ID"].ToString());
                }
                
                if (compare == 0)
                {
                    item.Checked = true;
                    idx_rec++;
                }
            }
        }        
        
        
        /// <summary>
        /// 保存护理评估结果
        /// </summary>
        private bool saveEvaluationRec()
        {
            if (_dsEvalRec == null || _dsEvalRec.Tables.Count == 0)
            {
                return false;
            }
            
            DataRow[] drFind = null;
            
            for(int i = 0; i < lvwItems.Items.Count; i++)
            {
                ListViewItem item = lvwItems.Items[i];
                string itemId = item.Tag.ToString();
                string itemName = item.SubItems[1].Text;
                
                drFind = _dsEvalRec.Tables[0].Select("ITEM_ID = " + SqlManager.SqlConvert(itemId));
                DataRow drEdit = null;
                
                if (item.Checked == true)
                {
                    if (drFind.Length == 0)
                    {
                        drEdit = _dsEvalRec.Tables[0].NewRow();
                        
                        drEdit["PATIENT_ID"]    = _patientId;
                        drEdit["VISIT_ID"]      = _visitId;
                        drEdit["DICT_ID"]       = _dictId; 
                        drEdit["ITEM_ID"]       = itemId;
                        drEdit["ITEM_NAME"]     = itemName;
                        drEdit["DEPT_CODE"]     = GVars.User.DeptCode; 
                        drEdit["RECORD_DATE"]   = _dtRec;
                        
                        _dsEvalRec.Tables[0].Rows.Add(drEdit);
                    }
                }
                else
                {
                    if (drFind.Length > 0)
                    {
                        drFind[0].Delete();
                    }
                }
            }
            
            return true;
        }
        #endregion
    }
}
