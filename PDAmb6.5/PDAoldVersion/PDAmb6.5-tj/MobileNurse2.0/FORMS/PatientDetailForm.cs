//------------------------------------------------------------------------------------
//
//  系统名称        : 无线移动医疗
//  子系统名称      : 护理工作站(PDA)
//  对象类型        : 
//  类名            : PatientDetailForm.cs
//  功能概要        : 病人详细信息
//  作成者          : 付军
//  作成日          : 2007-05-30
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        :
//  变更者          :
//  变更内容        :
//  版本            :
//------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class PatientDetailForm : Form
    {
        public PatientDetailForm()
        {
            InitializeComponent();

            this.Load += new EventHandler(PatientDetailForm_Load);
            
            //this.Click +=new EventHandler(Close);
        }
        
        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientDetailForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                initDisp();                                             // 初始化病人详细信息
                showPatientDetail();                                    // 显示病人详细信息
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }       
        
        
        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void Close(object sender, System.EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion
        
        
        #region 共通函数
        /// <summary>
        /// 初始化病人详细信息
        /// </summary>
        /// <remarks>所有文本框的内容清空</remarks>
        private void initDisp()
        {
            txtBoxName.Text           = string.Empty;                     // 姓名
            txtBoxSex.Text = string.Empty;                     // 性别
            txtBoxAge.Text = string.Empty;                     // 年龄
            txtBoxDoctorInCharge.Text = string.Empty;                     // 主管医生
            txtBoxInpNo.Text = string.Empty;                     // 住院号
            txtBoxVisitId.Text = string.Empty;                     // 住院标识
            txtBoxBedNo.Text = string.Empty;                     // 床位号
            txtBoxDeptName.Text = string.Empty;                     // 所属科室
            txtBoxNurseClass.Text = string.Empty;                     // 护理等级
            txtBoxPatientId.Text = string.Empty;                     // 病人标识
            txtBoxStatus.Text = string.Empty;                     // 入院病情
            txtBoxDiagnose.Text = string.Empty;                     // 主要诊断
            txtBoxAllergyDrug.Text = string.Empty;                     // 过敏药物
        }

        
        /// <summary>
        /// 显示病人详细信息
        /// </summary>
        private void showPatientDetail()
        {
            // 获取系统日期
            DateTime dtNow = GVars.GetDateNow();
            
            // 显示病人的信息
            DataRow curRow         = GVars.DsPatient.Tables[0].Rows[GVars.PatIndex];

            txtBoxName.Text = curRow["NAME"].ToString();                                           // 姓名
            txtBoxSex.Text = curRow["SEX"].ToString();                                            // 性别
            
            string birthday = curRow["DATE_OF_BIRTH"].ToString();
            
            if (birthday.Length > 0)
            {
                txtBoxAge.Text = PersonCls.GetAge((DateTime)(curRow["DATE_OF_BIRTH"]), dtNow);      // 年龄
            }
            else
            {
                txtBoxAge.Text = string.Empty;
            }

            txtBoxPatientId.Text = curRow["PATIENT_ID"].ToString();                                     // 病人标识号
            txtBoxInpNo.Text = curRow["INP_NO"].ToString();                                         // 住院号
            txtBoxVisitId.Text = curRow["VISIT_ID"].ToString();                                       // 住院标识
            txtBoxBedNo.Text = curRow["BED_LABEL"].ToString();                                      // 床位号
            txtBoxDeptName.Text = curRow["DEPT_NAME"].ToString();                                      // 所在科室
            txtBoxNurseClass.Text = curRow["NURSING_CLASS_NAME"].ToString();                             // 护理等级
            txtBoxDoctorInCharge.Text = curRow["DOCTOR_IN_CHARGE"].ToString();                               // 主管医生
            txtBoxDiagnose.Text = curRow["DIAGNOSIS"].ToString();                                      // 主要诊断
            txtBoxStatus.Text = curRow["PATIENT_STATUS_NAME"].ToString();                            // 入院病情
            
            if (curRow["ADMISSION_DATE_TIME"].ToString().Length > 0)
            {
                txtBoxInpDate.Text = ((DateTime)curRow["ADMISSION_DATE_TIME"]).ToString(ComConst.FMT_DATE.LONG); // 入院日期
            }

            txtBoxAllergyDrug.Text = curRow["ALERGY_DRUGS"].ToString();                                   // 过敏药物
        }
        #endregion        
    }
}