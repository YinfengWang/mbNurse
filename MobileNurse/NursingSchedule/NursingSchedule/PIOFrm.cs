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
    public partial class PIOFrm : FormDo1
    {
        #region 变量
        private const string             RIGHT_EDIT = "2";              // 编辑别人录入记录的权限
        private const string             _dictId    = "08";             // 护理诊断字典
        
        protected string                _itemId     = string.Empty;     // 项目代码
        protected string                _itemName   = string.Empty;     // 项目名称
        protected string                _itemValue  = string.Empty;     // 项目内容
        
        private string                   patientId  = string.Empty;     // 病人ID号
        private string                   visitId    = string.Empty;     // 本次就诊序号
        
        private HISDictDbI               dictDbI    = null;             // 字典接口
        private DataSet                  dsDictItem = null;             // 护理诊断记录
        #endregion
        
        
        public PIOFrm()
        {
            InitializeComponent();
            
            _id     = "00050";
            _guid   = "4E048BF7-BFD7-4ffa-9075-CD38E2EFB4B0";
        }
        
        
        #region 属性
        public string ItemId
        {
            get { return _itemId; }
            set { _itemId = value;}
        }
        
        
        public string ItemName
        {
            get { return _itemName; }
        }
        
        
        public string ItemValue
        {
            get { return _itemValue;}
        }
        #endregion
        
        
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PIOFrm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            
            try
            {
                initFrmVal();                
                initDisp();
            }
            catch(Exception ex)
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
        /// 初始化窗体变量
        /// </summary>
        private void initFrmVal()
        {
            dictDbI     = new HISDictDbI(GVars.OracleAccess);
            dsDictItem  = dictDbI.GetDictItem(_dictId);
            
            _userRight = GVars.User.GetUserFrmRight(_id);
        }
                
        
        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {   
            setControlMaxLength();
            
            // 健康教育记录
            dgvNurseDiagnose.AutoGenerateColumns = false;
            
            showDictItem(txtSearch.Text.Trim());                        
        }        
        
        
        /// <summary>
        /// 显示诊断列表
        /// </summary>
        private void showDictItem(string filter)
        {
            if (filter.Trim().Length > 0)
            {
                dsDictItem.Tables[0].DefaultView.RowFilter = "DESC_SPELL LIKE '" + filter.Trim() + "%'";
            }
            else
            {
                dsDictItem.Tables[0].DefaultView.RowFilter = string.Empty;
            }
            
            dgvNurseDiagnose.DataSource = dsDictItem.Tables[0].DefaultView;
        }
        
        
        /// <summary>
        /// 搜索事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                showDictItem(txtSearch.Text.Trim());
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 护理诊断选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvNurseDiagnose_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                
                _itemId = dgvNurseDiagnose.Rows[e.RowIndex].Cells["ITEM_ID"].Value.ToString();
                _itemName = dgvNurseDiagnose.Rows[e.RowIndex].Cells["ITEM_NAME"].Value.ToString();
                
                // 获取护理措施及日标
                _itemValue = dictDbI.GetDictItemContent(_dictId, _itemId, string.Empty);
                
                txtContent.Text = _itemValue;
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_itemId.Trim().Length == 0 || _itemValue.Trim().Length == 0)
                {
                    return;
                }
                
                _itemValue = txtContent.Text.Trim();
                
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

        
        private void dgvNurseDiagnose_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvNurseDiagnose_CellDoubleClick(sender, e);
        }
        
        
        private void setControlMaxLength()
        {
            if (dsDictItem != null && dsDictItem.Tables.Count > 0)
            {
                txtContent.MaxLength = 1000;
            }
        }
    }
}
