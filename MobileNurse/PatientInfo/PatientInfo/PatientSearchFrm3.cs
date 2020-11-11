using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace HISPlus
{
    public partial class PatientSearchFrm3 : FormDo
    {
        #region 变量
        public event    PatientChangedEventHandler PatientChanged;                              // 定义事件

        protected string _patientId     = string.Empty;
        protected string _visitId       = string.Empty;
        protected DateTime _dtInp       = DataType.DateTime_Null();

        private PatientDbI patientDbi   = null;
        #endregion


        #region 属性
        public string PatientId
        {
            get { return _patientId;}
        }


        public string VisitId
        {
            get { return _visitId; }
        }


        public DateTime DtInp
        {
            get { return _dtInp;}
        }
        #endregion


        public PatientSearchFrm3()
        {
            InitializeComponent();
        }


        #region 窗体事件
        private void PatientSearchFrm_Load(object sender, EventArgs e)
        {
            try
            {                

                initFrmVal();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        public void txtBedLabel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // 条件检查
                if (e.KeyCode != Keys.Return)
                {
                    return;
                }
                
                // 获取查询条件
                if (txtBedLabel.Text.Trim().Length == 0)
                {
                    return;
                }

                // 获取病人信息
                DataSet dsPatient = patientDbi.GetInpPatientInfo_FromBedLabel(txtBedLabel.Text.Trim(), GVars.User.DeptCode);

                if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
                {
                    dsPatient = patientDbi.GetPatientInfo_FromID(txtBedLabel.Text.Trim());
                }

                // 显示病人信息
                if (showPatientInfo(ref dsPatient) == false)
                {
                    GVars.Msg.Show("W0005");                            // 该病人不存在!
                    return;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion


        #region 共通函数
        private void initFrmVal()
        {
            patientDbi = new PatientDbI(GVars.OracleAccess);
        }

                
        /// <summary>
        /// 显示病人的基本信息
        /// </summary>
        private bool showPatientInfo(ref DataSet dsPatient)
        { 
            string patientId = _patientId;                              // 保存PatientId
            
            // 清空界面
            this.lblPatientName.Text = string.Empty;                    // 病人姓名
            this.lblGender.Text     = string.Empty;                     // 病人性别
            this.lblAge.Text        = string.Empty;                     // 年龄
            this.lblInpDate.Text    = string.Empty;                     // 入院日期
            this.lblBedRoom.Text    = string.Empty;                     // 病室
            this.lblArchiveNo.Text  = string.Empty;                     // 病案号
            
            // 如果没有数据退出
            if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            
            // 显示病人的基本信息
            DataRow dr = dsPatient.Tables[0].Rows[0];
            
            this.lblPatientName.Text = dr["NAME"].ToString();           // 病人姓名
            this.lblGender.Text = dr["SEX"].ToString();                 // 病人性别

            DateTime dtNow = GVars.OracleAccess.GetSysDate();

            if (dr["DATE_OF_BIRTH"].ToString().Length > 0)
            {
                this.lblAge.Text = PersonCls.GetAge((DateTime)dr["DATE_OF_BIRTH"], dtNow);
            }
            else
            {
                this.lblAge.Text = string.Empty;
            }

            if (dr["ADMISSION_DATE_TIME"].ToString().Length > 0)
            {
                this.lblInpDate.Text = DataType.GetDateTimeShort(dr["ADMISSION_DATE_TIME"].ToString());   // 入院日期
                _dtInp      = (DateTime)dr["ADMISSION_DATE_TIME"];
            }
            else
            {
                _dtInp      = DataType.DateTime_Null();
            }
            
            this.lblArchiveNo.Text  = dr["INP_NO"].ToString();          // 病案号
            this.lblBedRoom.Text    = dr["ROOM_NO"].ToString();         // 房间号
            
            _patientId  = dr["PATIENT_ID"].ToString();
            _visitId    = dr["VISIT_ID"].ToString();
            
            // diagnose    = dr["DIAGNOSIS"].ToString();                // 诊断
            
            // 如果病人ID发生变化
            //if (patientId.Equals(_patientId) == false)
            //{
                if (PatientChanged != null)
                {
                    PatientChanged(this, new PatientEventArgs(_patientId, _visitId));
                }
            //}
            
            return true;
        }
        #endregion
        
        
        #region 接口
        public void InputBedlabel(string bedLabel)
        {
            txtBedLabel.Text = bedLabel;
            
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                // 获取查询条件
                if (txtBedLabel.Text.Trim().Length == 0)
                {
                    return;
                }

                // 获取病人信息
                DataSet dsPatient = patientDbi.GetInpPatientInfo_FromBedLabel(txtBedLabel.Text.Trim(), GVars.User.DeptCode);

                if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
                {
                    dsPatient = patientDbi.GetPatientInfo_FromID(txtBedLabel.Text.Trim());
                }

                // 显示病人信息
                if (showPatientInfo(ref dsPatient) == false)
                {
                    GVars.Msg.Show("W0005");                            // 该病人不存在!
                    return;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        
        /// <summary>
        /// 显示病人详细信息
        /// </summary>
        public void ShowPatientInfo(string patientId, string visitId)
        {
            // 获取病人信息
            if (patientId.Equals(_patientId) == true && visitId.Equals(_visitId) == true)
            {
                return;
            }
            
            DataSet dsPatient = patientDbi.GetInpPatientInfo_FromID(patientId, visitId);
            
            // 显示病人信息
            if (showPatientInfo(ref dsPatient) == false)
            {
                GVars.Msg.Show("W0005");                           // 该病人不存在!
                return;
            }
            else
            {
                txtBedLabel.Text = dsPatient.Tables[0].Rows[0]["BED_LABEL"].ToString();
            }
        }
        #endregion
    }
}
