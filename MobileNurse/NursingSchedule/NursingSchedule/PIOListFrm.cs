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
    public partial class PIOListFrm : FormDo1,IBasePatient
    {
        #region 窗体变量
        private const string        RIGHT_WRITE         = "1";                      // 录入权限
        private const string        RIGHT_MODIFY        = "2";                      // 修改权限
        private const string        STR_START           = "开始(&S)";
        private const string        STR_START_CANCEL    = "撤销开始";
        private const string        STR_STOP            = "结束(&E)";
        private const string        STR_STOP_CANCEL     = "撤销结束";
        
        protected string            _dictId             = "08";
               
        
        private PatientDbI          patientDbI          = null;
        
        private DataSet             dsPatient           = null;                     // 病人信息
        private string              patientId           = string.Empty;             // 病人ID号
        private string              visitId             = string.Empty;             // 本次就诊序号
        
        private HISDictDbI          dictDbI             = null;                     // 字典接口
        private DataSet             dsRec               = null;                     // 护理记录
        #endregion
        
        
        public PIOListFrm()
        {
            InitializeComponent();
            
            _id     = "00051";
            _guid   = "BDD37544-0981-430a-AFA3-74DA7DE41C1A";
        }
        
        
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PIOListFrm_Load(object sender, EventArgs e)
        {
            try
            {
                initFrmVal();
                initDisp();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 初始化窗体变量
        /// </summary>
        private void initFrmVal()
        {
            patientDbI  = new PatientDbI(GVars.OracleAccess);
            dictDbI     = new HISDictDbI(GVars.OracleAccess);
            
            _userRight  = GVars.User.GetUserFrmRight(_id);
        }
        
        
        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {            
            dgvNurseDiagnose.AutoGenerateColumns = false;
            
            // 选中第一个病人
            //patientChanged();
        }

        void IBasePatient.Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
            patientId   = e.PatientId;
            visitId     = e.VisitId;
            
            // 获取护理记录
            dsPatient = patientDbI.GetInpPatientInfo_FromID(patientId, visitId);
            dsRec     = dictDbI.GetPIORec(patientId, visitId, _dictId);
            
            // 显示护理记录
            showPIORec();            
        }

        
        /// <summary>
        /// 按钮[新增]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                PIOFrm pio = new PIOFrm();
                if (pio.ShowDialog() == DialogResult.OK)
                {
                    DataRow dr = dsRec.Tables[0].NewRow();
                    
                    dr["PATIENT_ID"]    = patientId;
                    dr["VISIT_ID"]      = visitId;
                    dr["DICT_ID"]       = _dictId;
                    dr["ITEM_ID"]       = pio.ItemId;
                    dr["ITEM_NAME"]     = pio.ItemName;
                    dr["ITEM_VALUE"]    = pio.ItemValue;
                    dr["DEPT_CODE"]     = GVars.User.DeptCode;
                    dr["CREATE_DATE"]   = GVars.OracleAccess.GetSysDate();
                    
                    dsRec.Tables[0].Rows.Add(dr);
                    
                    dictDbI.SavePIORec(dsRec.GetChanges());
                    dsRec.AcceptChanges();
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 显示病人的护理PIO记录
        /// </summary>
        private void showPIORec()
        {
            dsRec.Tables[0].DefaultView.Sort = "START_DATE, STOP_DATE";            
            dgvNurseDiagnose.DataSource = dsRec.Tables[0].DefaultView;
            
            setButtonStatus((dgvNurseDiagnose.CurrentRow == null? -1: dgvNurseDiagnose.CurrentRow.Index));
        }
        
        
        /// <summary>
        /// 病人变更
        /// </summary>
        private void changePatient()
        {
            
        }
        
        
        private void dgvNurseDiagnose_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                setButtonStatus(e.RowIndex);
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        private void dgvNurseDiagnose_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        
        
        private void setButtonStatus(int rowIndex)
        {
            btnStart.Enabled = false;
            btnStop.Enabled  = false;
            btnDelete.Enabled = false;
            
            if (rowIndex == -1) return;
            
            btnDelete.Enabled = true;
            
            DataGridViewRow dgvRow = dgvNurseDiagnose.Rows[rowIndex];
            
            if (dgvRow.Cells["STOP_DATE"].Value.ToString().Length > 0)
            {
                btnStart.TextValue   = STR_START_CANCEL;
                btnStart.Enabled= false;
                
                btnStop.TextValue    = STR_STOP_CANCEL;
                btnStop.Enabled = true;
            }
            else
            {
                if (dgvRow.Cells["START_DATE"].Value.ToString().Length > 0)
                {
                    btnStart.TextValue   = STR_START_CANCEL;
                    btnStart.Enabled= true;
                    
                    btnStop.TextValue    = STR_STOP;
                    btnStop.Enabled = true;
                }
                else
                {
                    btnStart.TextValue   = STR_START;
                    btnStart.Enabled = true;
                    
                    btnStop.TextValue    = STR_STOP;
                    btnStop.Enabled = false;
                }
            }
            
            string nurseStart = dgvRow.Cells["START_NURSE"].Value.ToString();
            string nurseStop = dgvRow.Cells["STOP_NURSE"].Value.ToString();
            
            // 录入权限设置
            btnAdd.Enabled = btnAdd.Enabled && (_userRight.IndexOf(RIGHT_WRITE) >= 0);
            
            if (btnStart.TextValue.Equals(STR_START) == true)
            {
                btnStart.Enabled = btnStart.Enabled && (_userRight.IndexOf(RIGHT_WRITE) >= 0);
            }
            
            if (btnStop.TextValue.Equals(STR_STOP) == true)
            {
                btnStop.Enabled = btnStop.Enabled && (_userRight.IndexOf(RIGHT_WRITE) >= 0);
            }
            
            // 撤销权限设置
            btnDelete.Enabled =  btnDelete.Enabled && (_userRight.IndexOf(RIGHT_WRITE) >= 0) 
                                    && (_userRight.IndexOf(RIGHT_MODIFY) >= 0);
            
            if (btnStart.TextValue.Equals(STR_START_CANCEL) == true)
            {
                btnStart.Enabled = btnStart.Enabled && (nurseStart.Equals(GVars.User.Name) || _userRight.IndexOf(RIGHT_MODIFY) >= 0);
            }
            
            if (btnStop.TextValue.Equals(STR_STOP_CANCEL) == true)
            {
                btnStop.Enabled = btnStop.Enabled && (nurseStop.Equals(GVars.User.Name) || _userRight.IndexOf(RIGHT_MODIFY) >= 0);
            }
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
                if (GVars.Msg.Show("Q0004") != DialogResult.Yes)        // 您确认要删除当前记录吗?
                {
                    return;
                }
                
                if (dgvNurseDiagnose.CurrentRow == null)
                {
                    return;
                }
                
                DataRow dr = getCurrentRow();
                if (dr == null)
                {
                    return;
                }
                
                dr.Delete();
                dictDbI.SavePIORec(dsRec.GetChanges());
                dsRec.AcceptChanges();
                
                btnDelete.Enabled = false;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        private DataRow getCurrentRow()
        {
            if (dgvNurseDiagnose.CurrentRow == null)
            {
                return null;
            }
            
            DataGridViewRow dgvRow = dgvNurseDiagnose.CurrentRow;
            string itemId       = dgvRow.Cells["ITEM_ID"].Value.ToString();
            DateTime dtCreate   = (DateTime)(dgvRow.Cells["CREATE_DATE"].Value);
            
            string filter = "DICT_ID = " + SqlManager.SqlConvert(_dictId)
                        + "AND ITEM_ID = " + SqlManager.SqlConvert(itemId)
                        + "AND CREATE_DATE = " + SqlManager.SqlConvert(dtCreate.ToString(ComConst.FMT_DATE.LONG));
            DataRow[] drFind = dsRec.Tables[0].Select(filter);
            if (drFind.Length > 0)
            {
                return drFind[0];
            }
            else
            {
                return null;
            }
        }
        
        
        /// <summary>
        /// 按钮[开始]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNurseDiagnose.CurrentRow == null)
                {
                    return;
                }
                
                DataRow dr = getCurrentRow();
                if (dr == null) return;
                
                // 如果是开始
                if (btnStart.TextValue.Equals(STR_START))
                {
                    dr["START_DATE"] = GVars.OracleAccess.GetSysDate();
                    dr["START_NURSE"] = GVars.User.Name;
                    
                    dgvNurseDiagnose.CurrentRow.Cells["STATUS"].Value = "开始";
                }
                else
                {
                    dr["START_DATE"] = DBNull.Value;
                    dr["START_NURSE"] = string.Empty;
                    
                    dgvNurseDiagnose.CurrentRow.Cells["STATUS"].Value = string.Empty;
                }
                
                dictDbI.SavePIORec(dsRec.GetChanges());
                dsRec.AcceptChanges();
                
                setButtonStatus((dgvNurseDiagnose.CurrentRow == null? -1: dgvNurseDiagnose.CurrentRow.Index));
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNurseDiagnose.CurrentRow == null)
                {
                    return;
                }
                
                DataRow dr = getCurrentRow();
                if (dr == null) return;
                
                // 如果是开始
                if (btnStop.TextValue.Equals(STR_STOP))
                {
                    dr["STOP_DATE"]     = GVars.OracleAccess.GetSysDate();
                    dr["STOP_NURSE"]    = GVars.User.Name;
                    
                    dgvNurseDiagnose.CurrentRow.Cells["STATUS"].Value = "结束";
                }
                else
                {
                    dr["STOP_DATE"]     = DBNull.Value;
                    dr["STOP_NURSE"]    = string.Empty;
                    
                    dgvNurseDiagnose.CurrentRow.Cells["STATUS"].Value = "开始";
                }
                
                dictDbI.SavePIORec(dsRec.GetChanges());
                dsRec.AcceptChanges();
                
                setButtonStatus((dgvNurseDiagnose.CurrentRow == null? -1: dgvNurseDiagnose.CurrentRow.Index));
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        private void dgvNurseDiagnose_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                DataGridViewRow dgvRow = dgvNurseDiagnose.Rows[e.RowIndex];
                
                if (dgvRow.Cells["START_DATE"].Value.ToString().Length > 0)
                {
                    if (dgvRow.Cells["STOP_DATE"].Value.ToString().Length > 0)
                    {
                        dgvRow.Cells["STATUS"].Value = "结束";
                    }
                    else
                    {
                        dgvRow.Cells["STATUS"].Value = "开始";
                    }
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        void IBasePatient.Patient_ListRefresh(object sender, PatientEventArgs e)
        {
            
        }
    }
}
