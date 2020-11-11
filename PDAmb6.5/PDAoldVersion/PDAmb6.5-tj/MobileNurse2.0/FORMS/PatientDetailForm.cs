//------------------------------------------------------------------------------------
//
//  ϵͳ����        : �����ƶ�ҽ��
//  ��ϵͳ����      : ������վ(PDA)
//  ��������        : 
//  ����            : PatientDetailForm.cs
//  ���ܸ�Ҫ        : ������ϸ��Ϣ
//  ������          : ����
//  ������          : 2007-05-30
//  �汾            : 1.0.0.0
// 
//------------------< �����ʷ >------------------------------------------------------
//  �������        :
//  �����          :
//  �������        :
//  �汾            :
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
        
        #region �����¼�
        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientDetailForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                initDisp();                                             // ��ʼ��������ϸ��Ϣ
                showPatientDetail();                                    // ��ʾ������ϸ��Ϣ
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
        /// �ر��¼�
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
        
        
        #region ��ͨ����
        /// <summary>
        /// ��ʼ��������ϸ��Ϣ
        /// </summary>
        /// <remarks>�����ı�����������</remarks>
        private void initDisp()
        {
            txtBoxName.Text           = string.Empty;                     // ����
            txtBoxSex.Text = string.Empty;                     // �Ա�
            txtBoxAge.Text = string.Empty;                     // ����
            txtBoxDoctorInCharge.Text = string.Empty;                     // ����ҽ��
            txtBoxInpNo.Text = string.Empty;                     // סԺ��
            txtBoxVisitId.Text = string.Empty;                     // סԺ��ʶ
            txtBoxBedNo.Text = string.Empty;                     // ��λ��
            txtBoxDeptName.Text = string.Empty;                     // ��������
            txtBoxNurseClass.Text = string.Empty;                     // ����ȼ�
            txtBoxPatientId.Text = string.Empty;                     // ���˱�ʶ
            txtBoxStatus.Text = string.Empty;                     // ��Ժ����
            txtBoxDiagnose.Text = string.Empty;                     // ��Ҫ���
            txtBoxAllergyDrug.Text = string.Empty;                     // ����ҩ��
        }

        
        /// <summary>
        /// ��ʾ������ϸ��Ϣ
        /// </summary>
        private void showPatientDetail()
        {
            // ��ȡϵͳ����
            DateTime dtNow = GVars.GetDateNow();
            
            // ��ʾ���˵���Ϣ
            DataRow curRow         = GVars.DsPatient.Tables[0].Rows[GVars.PatIndex];

            txtBoxName.Text = curRow["NAME"].ToString();                                           // ����
            txtBoxSex.Text = curRow["SEX"].ToString();                                            // �Ա�
            
            string birthday = curRow["DATE_OF_BIRTH"].ToString();
            
            if (birthday.Length > 0)
            {
                txtBoxAge.Text = PersonCls.GetAge((DateTime)(curRow["DATE_OF_BIRTH"]), dtNow);      // ����
            }
            else
            {
                txtBoxAge.Text = string.Empty;
            }

            txtBoxPatientId.Text = curRow["PATIENT_ID"].ToString();                                     // ���˱�ʶ��
            txtBoxInpNo.Text = curRow["INP_NO"].ToString();                                         // סԺ��
            txtBoxVisitId.Text = curRow["VISIT_ID"].ToString();                                       // סԺ��ʶ
            txtBoxBedNo.Text = curRow["BED_LABEL"].ToString();                                      // ��λ��
            txtBoxDeptName.Text = curRow["DEPT_NAME"].ToString();                                      // ���ڿ���
            txtBoxNurseClass.Text = curRow["NURSING_CLASS_NAME"].ToString();                             // ����ȼ�
            txtBoxDoctorInCharge.Text = curRow["DOCTOR_IN_CHARGE"].ToString();                               // ����ҽ��
            txtBoxDiagnose.Text = curRow["DIAGNOSIS"].ToString();                                      // ��Ҫ���
            txtBoxStatus.Text = curRow["PATIENT_STATUS_NAME"].ToString();                            // ��Ժ����
            
            if (curRow["ADMISSION_DATE_TIME"].ToString().Length > 0)
            {
                txtBoxInpDate.Text = ((DateTime)curRow["ADMISSION_DATE_TIME"]).ToString(ComConst.FMT_DATE.LONG); // ��Ժ����
            }

            txtBoxAllergyDrug.Text = curRow["ALERGY_DRUGS"].ToString();                                   // ����ҩ��
        }
        #endregion        
    }
}