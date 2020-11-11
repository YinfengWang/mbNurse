using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace HISPlus
{
    public class PatientNavigator
    {
        #region 变量
        protected Button _btnPatientList;
        protected Button _btnPrePatient;
        protected Button _btnCurrentPatient;
        protected Button _btnNextPatient;
        //protected Button _btnListOrder;
        
        protected MenuItem _mnuPatient;

        //protected MenuItem _mnuRemind;
        
        protected DataChanged _patientChanged;
        #endregion
        
        
        public PatientNavigator()
        {
        }
        
        
        #region 属性
        public Button BtnPatientList
        {
            get { return _btnPatientList; }
            set 
            { 
                _btnPatientList = value;
                
                if (_btnPatientList != null)
                {
                    _btnPatientList.Click += new EventHandler(btnListPatient_Click);
                }
            }
        }
        
        
        public Button BtnPrePatient
        {
            get { return _btnPrePatient; }
            set 
            { 
                _btnPrePatient = value;
                
                if (_btnPrePatient != null)
                {
                    _btnPrePatient.Click += new EventHandler(btnPrePatient_Click);
                }
            }
        }
        
        
        public Button BtnCurrentPatient
        {
            get { return _btnCurrentPatient; }
            set 
            { 
                _btnCurrentPatient = value;
                
                if (_btnCurrentPatient != null)
                {
                    _btnCurrentPatient.Click += new EventHandler(btnCurrPatient_Click);
                }
            }
        }
        
        
        public Button BtnNextPatient
        {
            get { return _btnNextPatient;}
            set 
            { 
                _btnNextPatient = value;
                
                if (_btnNextPatient != null)
                {
                    _btnNextPatient.Click += new EventHandler(btnNextPatient_Click);
                }
            }
        }
        
        
        public MenuItem MenuItemPatient
        {
            get { return _mnuPatient; }
            set 
            { 
                _mnuPatient = value;
                
                if (_mnuPatient != null)
                {
                    _mnuPatient.Click += new EventHandler(btnCurrPatient_Click);
                }
            }
        }

        //public MenuItem MenuItemOrder
        //{
        //    get { return _mnuRemind; }
        //    set
        //    {
        //        _mnuRemind = value;
        //        if (_mnuRemind != null)
        //        {
        //            _mnuRemind.Click += new EventHandler(btnListOrder_Click);
        //        }
        //    }
        //}

        /// <summary>
        /// 按钮【医嘱提醒】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnListOrder_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        _btnListOrder.Enabled = false;

        //        OrderRemindFrm orderDetail = new OrderRemindFrm();
        //        orderDetail.ShowDialog();
        //    }
        //    catch (Exception ex)
        //    {

        //        Error.ErrProc(ex);
        //    }
        //    finally
        //    {
        //        _btnListOrder.Enabled = true;
        //    }
        //}
        
        
        public DataChanged PatientChanged
        {
            get { return _patientChanged; }
            set { _patientChanged = value;}
        }
        #endregion
        
        
        #region 事件
        /// <summary>
        /// 按钮 [前一个病人]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrePatient_Click(object sender, EventArgs e)
        {
            try
            {
                GVars.PatIndex--;
                DataRow dr = GVars.DsPatient.Tables[0].Rows[GVars.PatIndex];
                
                GVars.Patient.ID      = dr["PATIENT_ID"].ToString();
                GVars.Patient.VisitId = dr["VISIT_ID"].ToString();
                GVars.Patient.Name    = dr["NAME"].ToString();
                
                SetPatientButtons();
                
                if (_patientChanged != null)
                {
                    _patientChanged();
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮 [当前病人]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCurrPatient_Click(object sender, EventArgs e)
        {
            try
            {
                _btnCurrentPatient.Enabled = false;
                
                PatientDetailForm patDetail = new PatientDetailForm();
                patDetail.ShowDialog();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                _btnCurrentPatient.Enabled = true;
            }
        }
        
        
        /// <summary>
        /// 下一个病人事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextPatient_Click(object sender, EventArgs e)
        {
            try
            {
                GVars.PatIndex++;
                
                DataRow dr = GVars.DsPatient.Tables[0].Rows[GVars.PatIndex];
                
                GVars.Patient.ID      = dr["PATIENT_ID"].ToString();
                GVars.Patient.VisitId = dr["VISIT_ID"].ToString();
                GVars.Patient.Name    = dr["NAME"].ToString();
                
                SetPatientButtons();
                
                if (_patientChanged != null)
                {
                    _patientChanged();
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[列表]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnListPatient_Click(object sender, EventArgs e)
        {
            try
            {
                _btnPatientList.Enabled = false;
                
                PatientListFrm patListFrm = new PatientListFrm();
                //if (patListFrm.ShowDialog() == DialogResult.OK)
                //{
                    patListFrm.ShowDialog();
                    ResetPatIndex(GVars.Patient.ID);
                    SetPatientButtons();
                    
                    if (_patientChanged != null)
                    {
                        _patientChanged();
                    }
                //}
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                _btnPatientList.Enabled = true;
            }
        }        
        #endregion
        
        
        #region 共通函数
        /// <summary>
        /// 获取病人的位置
        /// </summary>
        public bool ResetPatIndex(string patientId)
        {
            if (GVars.DsPatient == null || GVars.DsPatient.Tables.Count == 0 
                || GVars.DsPatient.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            
            for(int i = 0; i < GVars.DsPatient.Tables[0].Rows.Count; i++)
            {
                DataRow dr = GVars.DsPatient.Tables[0].Rows[i];
                
                if (dr["PATIENT_ID"].ToString().Equals(patientId) == true)
                {
                    GVars.PatIndex = i;
                    return true;
                }                 
            }
            
            return false;
        }  
        
        
        /// <summary>
        /// 设置病人导航按钮
        /// </summary>
        public void SetPatientButtons()
        {
            _btnCurrentPatient.Text = "空";            
            _btnPrePatient.Text = " <";
            _btnNextPatient.Text = "> ";
            
            _btnCurrentPatient.Enabled = false;
            _btnPrePatient.Enabled = false;
            _btnNextPatient.Enabled = false;
            
            if (_mnuPatient != null)
            {
                _mnuPatient.Enabled = false;
                _mnuPatient.Text = "无当前病人";
            }
            
            if (GVars.DsPatient == null || GVars.DsPatient.Tables.Count == 0 
               || GVars.DsPatient.Tables[0].Rows.Count == 0)
            {
                return;
            }
            
            if (GVars.PatIndex < 0)
            {
                GVars.PatIndex = 0;
            }
            
            DataTable dtPat = GVars.DsPatient.Tables[0];
            
            // 当前病人
            if (GVars.PatIndex >= 0 && GVars.PatIndex < dtPat.Rows.Count)
            {
                _btnCurrentPatient.Enabled = true;
                _btnCurrentPatient.Text = dtPat.Rows[GVars.PatIndex]["BED_LABEL"].ToString() + "床";
                
                if (_mnuPatient != null)
                {
                    _mnuPatient.Enabled = true;
                    _mnuPatient.Text = dtPat.Rows[GVars.PatIndex]["NAME"].ToString();
                }
            }
            
            // 如果有后面的病人
            if (GVars.PatIndex < dtPat.Rows.Count - 1)
            {
                _btnNextPatient.Enabled = true;
                _btnNextPatient.Text += dtPat.Rows[GVars.PatIndex + 1]["BED_LABEL"].ToString() + "床";
            }
            
            // 如果有前面的病人
            if (0 < GVars.PatIndex)
            {
                _btnPrePatient.Enabled = true;
                _btnPrePatient.Text = dtPat.Rows[GVars.PatIndex - 1]["BED_LABEL"].ToString() + "床" + _btnPrePatient.Text;
            }            
        }        
                      
        
	    /// <summary>
        /// 扫描到病人
        /// </summary>
        /// <returns></returns>
        public bool ScanedPatient(string barcode)
        {
            // 如果与当前病人相同, 直接退出
            if (barcode.Equals(GVars.Patient.ID) == true)
            {
                return true;
            }
            
            // 如果病人不存在, 提示
            if (ResetPatIndex(barcode) == false)
            {
                GVars.DsPatient = GVars.PatDbI.GetPatientInfo_Filter("全部");   // 重新获取病人列表
                if (ResetPatIndex(barcode) == false)
                {
                    return false;
                }
            }
            
            // 如果病人存在
            GVars.Patient.ID      = GVars.DsPatient.Tables[0].Rows[GVars.PatIndex]["PATIENT_ID"].ToString();
            GVars.Patient.VisitId = GVars.DsPatient.Tables[0].Rows[GVars.PatIndex]["VISIT_ID"].ToString();
            GVars.Patient.Name    = GVars.DsPatient.Tables[0].Rows[GVars.PatIndex]["NAME"].ToString();
            
            SetPatientButtons();
            
            // 显示病人详细信息
            //PatientDetailForm patDetail = new PatientDetailForm();
            //patDetail.ShowDialog();
            
            // 触发病人改变事件
            if (_patientChanged != null)
            {
                _patientChanged();
            }
            
            return true;
        }        
        #endregion
    }
}
