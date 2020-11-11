using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DXApplication2;

namespace HISPlus
{
    public partial class PIORecViewFrm : FormDo1,IBasePatient
    {
        #region 窗体变量
        protected string            _dictId             = "08";
        
        private PatientDbI          patientDbI          = null;
        
        private DataSet             dsPatient           = null;                     // 病人信息
        private string              patientId           = string.Empty;             // 病人ID号
        private string              visitId             = string.Empty;             // 本次就诊序号
        
        private HISDictDbI          dictDbI             = null;                     // 字典接口
        private DataSet             dsRec               = null;                     // 护理记录
        #endregion
        
        
        public PIORecViewFrm()
        {
            InitializeComponent();
            
            _id     = "00052";
            _guid   = "D9D4998D-407F-4855-9792-B02DDAF49995";
        }
        
        
        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PIORecViewFrm_Load(object sender, EventArgs e)
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


        void IBasePatient.Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
            //patientId   = e.PatientId;
            //visitId     = e.VisitId;
            
            //// 获取护理记录
            //dsPatient = patientDbI.GetInpPatientInfo_FromID(patientId, visitId);
            //dsRec     = dictDbI.GetPIORec(patientId, visitId, _dictId, dtRngStart.Value, dtRngEnd.Value);
            
            //// 显示护理记录
            //showPIORec();
            
            GVars.Patient.ID        = e.PatientId;
            GVars.Patient.VisitId   = e.VisitId;
            
            patientId = GVars.Patient.ID;
            visitId   = GVars.Patient.VisitId;

            btnQuery_Click(null, null);
        }
        
        /// <summary>
        /// 按钮[查询]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                dsRec = dictDbI.GetPIORec(patientId, visitId, _dictId, dtRngStart.Value, dtRngEnd.Value);
                
                // 显示护理记录
                showPIORec();
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
                
        #endregion
        
        
        #region 共通函数
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
        }


        /// <summary>
        /// 显示病人的护理PIO记录
        /// </summary>
        private void showPIORec()
        {
            txtContent.Text = string.Empty;
            
            DataRow[] drFind = dsRec.Tables[0].Select("START_DATE IS NOT NULL", "START_DATE, STOP_DATE");
            
            StringBuilder sb = new StringBuilder();
            DateTime dtPre = DataType.DateTime_Null();
            for(int i = 0; i < drFind.Length; i++)
            {
                DataRow dr = drFind[i];
                DateTime dtCurr = (DateTime)dr["START_DATE"];
                
                if (dtCurr.Date.Equals(dtPre.Date) == false)
                {
                    // ------------------------------------
                    if (sb.Length > 0)
                    {
                        sb.Append(ComConst.STR.CRLF);                        
                    }
                    
                    sb.Append(new string('-', 80) + ComConst.STR.CRLF);
                }
                
                dtPre = dtCurr;
                
                // 日    期: 2008-12-12
                sb.Append("日    期: " + dtCurr.Date.ToString(ComConst.FMT_DATE.SHORT) + ComConst.STR.CRLF);
                
                // 开始时间: 2008-12-12 12:12 开始护士: 张三    结束时间: 2008-12-13 14:11 结束护士: 李四
                sb.Append("开始时间: " + dtCurr.ToString(ComConst.FMT_DATE.LONG_MINUTE));
                sb.Append(" 开始护士: " + dr["START_NURSE"].ToString());
                sb.Append(new string(' ', 4));
                if (dr["STOP_DATE"] != DBNull.Value)
                {
                    DateTime dtStop = (DateTime)dr["STOP_DATE"];
                    sb.Append("结束时间: " + dtStop.ToString(ComConst.FMT_DATE.LONG_MINUTE));
                    sb.Append(" 结束护士: " + dr["STOP_NURSE"].ToString());
                }
                
                sb.Append(ComConst.STR.CRLF);
                
                // 护理诊断: 不知道
                sb.Append("护理诊断: " + dr["ITEM_NAME"].ToString() + ComConst.STR.CRLF);
                
                // 目标及措施:
                //     test
                sb.Append("目标及措施: " + ComConst.STR.CRLF);
                sb.Append(new string(' ', 4) + dr["ITEM_VALUE"].ToString() + ComConst.STR.CRLF + ComConst.STR.CRLF);            
            }
            
            txtContent.Text = sb.ToString();
        }
        #endregion


        void IBasePatient.Patient_ListRefresh(object sender, PatientEventArgs e)
        {
            
        }
    }
}
