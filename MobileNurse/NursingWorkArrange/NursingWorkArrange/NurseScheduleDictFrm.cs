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
    public partial class NurseScheduleDictFrm : FormDo
    {
        #region 变量
        private DataSet     dsScheduleDict  = null;             // 班次定义
                
        private string      cellValue       = string.Empty;     // 单元格的值
        #endregion
        
        
        public NurseScheduleDictFrm()
        {
            InitializeComponent();
            
            _id     = "00055";
            _guid   = "3CB3D723-077C-44b9-81E5-A046CAB73A08";
        }
        
        
        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NurseScheduleDictFrm_Load(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                GVars.App.UserInput = false;
                
                initFrmVal();
                initDisp();
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
        /// 新行默行值赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvScheduleDict_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["DEPT_CODE"].Value = GVars.User.DeptCode;
        }
        
        
        /// <summary>
        /// 单元格开始编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvScheduleDict_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvScheduleDict.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
            {
                cellValue = string.Empty;                
            }
            else
            {
                cellValue = dgvScheduleDict.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
        }
        
        
        /// <summary>
        /// 单元格结束编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvScheduleDict_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string tempValue = string.Empty;
            
            if (dgvScheduleDict.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                tempValue = dgvScheduleDict.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
            
            if (cellValue.Equals(tempValue) == false && GVars.App.UserInput == true)
            {
                btnSave.Enabled = true;
            }
        }
        
        
        /// <summary>
        /// 错误处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvScheduleDict_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
        
        
        /// <summary>
        /// 按钮[删除]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvScheduleDict.CurrentRow != null)
                {
                    dgvScheduleDict.Rows.Remove(dgvScheduleDict.CurrentRow);
                    
                    btnSave.Enabled = true;
                }
            }
            catch(Exception ex)
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
                if (dsScheduleDict.HasChanges() == true)
                {
                    GVars.OracleAccess.Update(ref dsScheduleDict);
                    dsScheduleDict.AcceptChanges();
                    
                    string sql = "SELECT * FROM SHIFT_EXCHANGE_DICT";
                    dsScheduleDict  = GVars.OracleAccess.SelectData(sql, "SHIFT_EXCHANGE_DICT");       // 班次定义
                    dsScheduleDict.Tables[0].DefaultView.Sort = "SERIAL_NO";
                }
                
                btnSave.Enabled = false;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion


        #region 共通函数
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void initFrmVal()
        {
            string filter   = "DEPT_CODE = " + SQL.SqlConvert(GVars.User.DeptCode);
            dsScheduleDict  = GVars.OracleAccess.SelectData("SELECT * FROM NURSE_SCHEDULE_DICT WHERE " + filter);               // 班次定义            
        }
        
        
        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {
            // 班次定义
            dgvScheduleDict.AutoGenerateColumns = false;
            dsScheduleDict.Tables[0].DefaultView.Sort = "SERIAL_NO";
            dgvScheduleDict.DataSource = dsScheduleDict.Tables[0].DefaultView;
        }        
        #endregion
    }
}
