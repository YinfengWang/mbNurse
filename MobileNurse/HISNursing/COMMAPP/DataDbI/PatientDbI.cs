using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public class PatientDbI
    {
        protected DbAccess _connection;

        public PatientDbI(DbAccess dbConnect)
        {
            _connection = dbConnect;
        }


        #region ��ȡ������Ϣ
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public virtual DataSet GetPatientInfo(string patientId)
        {
            string sql = "SELECT * FROM PAT_MASTER_INDEX WHERE PATIENT_ID = " + SQL.SqlConvert(patientId.Trim());

            return _connection.SelectData(sql);
        }


        /// <summary>
        /// ��ȡ���˻�����Ϣ
        /// </summary>
        /// <param name="patientId">���˱�ʶ��</param>
        /// <param name="visitId">���ξ������</param>
        /// <returns></returns>
        public virtual DataSet GetInpPatientInfo_FromID(string patientId, string visitId)
        {
            string sql = string.Empty;

            sql = "SELECT ";
            sql +=      "PAT_MASTER_INDEX.NAME, ";                                      // ����
            sql +=      "PAT_MASTER_INDEX.SEX, ";                                       // �Ա�
            sql +=      "PAT_MASTER_INDEX.DATE_OF_BIRTH, ";                             // ��������
            sql +=      "PATS_IN_HOSPITAL.DIAGNOSIS, ";                                 // ��Ҫ���

            sql +=      "PATS_IN_HOSPITAL.VISIT_ID, ";                                  // ����סԺ��ʶ
            sql +=      "PAT_MASTER_INDEX.PATIENT_ID, ";                                // ���˱�ʶ��
            sql +=      "PAT_MASTER_INDEX.INP_NO, ";                                    // סԺ��

            sql +=     "(SELECT NURSING_CLASS_DICT.NURSING_CLASS_NAME ";
            sql +=      "FROM NURSING_CLASS_DICT ";
            sql +=      "WHERE NURSING_CLASS_DICT.NURSING_CLASS_CODE = PATS_IN_HOSPITAL.NURSING_CLASS) ";
            sql +=      "NURSING_CLASS_NAME, ";                                         // ����ȼ�����

            sql +=      "PATS_IN_HOSPITAL.DOCTOR_IN_CHARGE, ";                          // ����ҽ��

            sql +=      "PATS_IN_HOSPITAL.BED_NO, ";								    // ����
            sql +=      "(SELECT BED_REC.BED_LABEL ";
            sql +=       "FROM BED_REC ";
            sql +=       "WHERE BED_REC.BED_NO = PATS_IN_HOSPITAL.BED_NO ";
            sql +=          "AND BED_REC.WARD_CODE = PATS_IN_HOSPITAL.WARD_CODE) ";
            sql +=       "BED_LABEL, ";                                                 // �����

            sql +=      "(SELECT PATIENT_STATUS_DICT.PATIENT_STATUS_NAME ";
            sql +=        "FROM PATIENT_STATUS_DICT ";
            sql +=        "WHERE PATIENT_STATUS_DICT.PATIENT_STATUS_NAME = PATS_IN_HOSPITAL.PATIENT_CONDITION) ";
            sql +=      "PATIENT_STATUS_NAME, ";                                        // ����״̬

            sql +=      "PATS_IN_HOSPITAL.ADMISSION_DATE_TIME, ";                       // ��Ժ���ڼ�ʱ��

            sql +=      "(SELECT DEPT_NAME ";
            sql +=          "FROM DEPT_DICT ";
            sql +=          "WHERE DEPT_CODE = ";
            sql +=              "(CASE WHEN PATS_IN_HOSPITAL.LEND_INDICATOR = 1 ";
            sql +=               "THEN PATS_IN_HOSPITAL.DEPT_CODE_LEND ";
            sql +=               "ELSE PATS_IN_HOSPITAL.DEPT_CODE END)) ";
            sql +=      "DEPT_NAME ";                                                   // ���ڿ���

            sql += "FROM ";
            sql +=      "PATS_IN_HOSPITAL, ";                                           // ��Ժ���˼�¼
            sql +=      "PAT_MASTER_INDEX ";                                            // ����������

            sql += "WHERE ";
            sql +=      "PATS_IN_HOSPITAL.PATIENT_ID = " + SQL.SqlConvert(patientId);
            sql +=      " AND PATS_IN_HOSPITAL.VISIT_ID = " + SQL.SqlConvert(visitId);
            sql +=      " AND PATS_IN_HOSPITAL.PATIENT_ID = PAT_MASTER_INDEX.PATIENT_ID ";

            return _connection.SelectData(sql);
        }
        

        /// <summary>
        /// ��ȡ���˻�����Ϣ
        /// </summary>
        /// <param name="bedLabel">�����</param>
        /// <param name="wardCode">��������</param>
        /// <returns></returns>
        public virtual DataSet GetInpPatientInfo_FromBedLabel(string bedLabel, string wardCode)
        {
            string sql = string.Empty;

            sql = "SELECT ";
            sql+=    "PAT_MASTER_INDEX.NAME, ";                                      // ����
            sql+=    "PAT_MASTER_INDEX.SEX, ";                                       // �Ա�
            sql+=    "PAT_MASTER_INDEX.DATE_OF_BIRTH, ";                             // ��������
            sql+=    "PATS_IN_HOSPITAL.DIAGNOSIS, ";                                 // ��Ҫ���
                        
            sql+=    "PATS_IN_HOSPITAL.VISIT_ID, ";                                  // ����סԺ��ʶ
            sql+=    "PAT_MASTER_INDEX.PATIENT_ID, ";                                // ���˱�ʶ��
            sql+=    "PAT_MASTER_INDEX.INP_NO, ";                                    // סԺ��
            sql+=    "PAT_MASTER_INDEX.CHARGE_TYPE, ";                               // �ѱ�

            sql+=	 "BED_REC.BED_NO, ";								             // ����
            sql+=    "BED_REC.BED_LABEL, ";                                          // �����
            sql+=    "BED_REC.ROOM_NO, ";                                            // �����

            sql+=    "PATS_IN_HOSPITAL.ADMISSION_DATE_TIME, ";                       // ��Ժ���ڼ�ʱ��

            sql+=    "(SELECT DEPT_NAME ";
            sql+=    "FROM DEPT_DICT ";
            sql+=    "WHERE DEPT_CODE = ";
            sql+=        "(CASE WHEN PATS_IN_HOSPITAL.LEND_INDICATOR = 1 ";
            sql+=         "THEN PATS_IN_HOSPITAL.DEPT_CODE_LEND ";
            sql+=         "ELSE PATS_IN_HOSPITAL.DEPT_CODE END)) ";
            sql+=    "DEPT_NAME ";                                                   // ���ڿ���
            
            sql+= "FROM ";
            sql+=    "PATS_IN_HOSPITAL, ";                                           // ��Ժ���˼�¼
            sql+=    "PAT_MASTER_INDEX, ";                                           // ����������
            sql+=    "BED_REC ";                                                     // ��λ��¼

            sql+= "WHERE ";
            sql+=    "(BED_REC.BED_NO = PATS_IN_HOSPITAL.BED_NO ";
            sql+=    "AND BED_REC.WARD_CODE = PATS_IN_HOSPITAL.WARD_CODE) ";
            
            // ��λ��
            sql+=    "AND BED_REC.BED_LABEL = " + SQL.SqlConvert(bedLabel);
            sql+=    "AND BED_REC.WARD_CODE = " + SQL.SqlConvert(wardCode);
            sql+=    "AND PATS_IN_HOSPITAL.PATIENT_ID = PAT_MASTER_INDEX.PATIENT_ID ";

            return _connection.SelectData(sql);
        }


        /// <summary>
        /// ��ȡ���������Ĳ����б�
        /// </summary>
        /// <param name="wardCode"></param>
        /// <returns></returns>
        public virtual DataSet GetWardPatientList(string wardCode)
        { 
            string sql = string.Empty;

            sql = "SELECT ";
            sql +=      "PAT_MASTER_INDEX.NAME, ";                                      // ����
            sql +=      "PAT_MASTER_INDEX.SEX, ";                                       // �Ա�
            sql +=      "PAT_MASTER_INDEX.DATE_OF_BIRTH, ";                             // ��������
            sql +=      "PATS_IN_HOSPITAL.DIAGNOSIS, ";                                 // ��Ҫ���

            sql +=      "PATS_IN_HOSPITAL.VISIT_ID, ";                                  // ����סԺ��ʶ
            sql +=      "PAT_MASTER_INDEX.PATIENT_ID, ";                                // ���˱�ʶ��
            sql +=      "PAT_MASTER_INDEX.INP_NO, ";                                    // סԺ��

            sql +=     "(SELECT NURSING_CLASS_DICT.NURSING_CLASS_NAME ";
            sql +=      "FROM NURSING_CLASS_DICT ";
            sql +=      "WHERE NURSING_CLASS_DICT.NURSING_CLASS_CODE = PATS_IN_HOSPITAL.NURSING_CLASS) ";
            sql +=      "NURSING_CLASS_NAME, ";                                         // ����ȼ�����

            sql +=      "PATS_IN_HOSPITAL.DOCTOR_IN_CHARGE, ";                          // ����ҽ��

            sql +=      "PATS_IN_HOSPITAL.BED_NO, ";								    // ����
            sql +=      "(SELECT BED_REC.BED_LABEL ";
            sql +=       "FROM BED_REC ";
            sql +=       "WHERE BED_REC.BED_NO = PATS_IN_HOSPITAL.BED_NO ";
            sql +=          "AND BED_REC.WARD_CODE = PATS_IN_HOSPITAL.WARD_CODE) ";
            sql +=       "BED_LABEL, ";                                                 // �����

            sql +=      "(SELECT PATIENT_STATUS_DICT.PATIENT_STATUS_NAME ";
            sql +=        "FROM PATIENT_STATUS_DICT ";
            sql +=        "WHERE PATIENT_STATUS_DICT.PATIENT_STATUS_NAME = PATS_IN_HOSPITAL.PATIENT_CONDITION) ";
            sql +=      "PATIENT_STATUS_NAME, ";                                        // ����״̬

            sql +=      "PATS_IN_HOSPITAL.ADMISSION_DATE_TIME, ";                       // ��Ժ���ڼ�ʱ��

            sql +=      "(SELECT DEPT_NAME ";
            sql +=          "FROM DEPT_DICT ";
            sql +=          "WHERE DEPT_CODE = ";
            sql +=              "(CASE WHEN PATS_IN_HOSPITAL.LEND_INDICATOR = 1 ";
            sql +=               "THEN PATS_IN_HOSPITAL.DEPT_CODE_LEND ";
            sql +=               "ELSE PATS_IN_HOSPITAL.DEPT_CODE END)) ";
            sql +=      "DEPT_NAME ";                                                   // ���ڿ���

            sql += "FROM ";
            sql +=      "PATS_IN_HOSPITAL, ";                                           // ��Ժ���˼�¼
            sql +=      "PAT_MASTER_INDEX ";                                            // ����������

            sql += "WHERE ";
            sql +=      "PATS_IN_HOSPITAL.WARD_CODE = " + SQL.SqlConvert(wardCode);
            sql +=      " AND PATS_IN_HOSPITAL.PATIENT_ID = PAT_MASTER_INDEX.PATIENT_ID ";
            
            sql += "ORDER BY ";
            sql +=      "PATS_IN_HOSPITAL.BED_NO ";

            return _connection.SelectData(sql);
        }
        #endregion


        #region ��ȡ������Ϣ
        /// <summary>
        /// ��ȡ���ﲡ�˴����б�
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public DataSet GetOutpPrescList(string patientId)
        { 
            StringBuilder sb = new StringBuilder(); 
            sb.Append("SELECT DISTINCT "                                                         );
            sb.Append(    "OUTP_PRESC.SERIAL_NO, "                                               ); // ����
            sb.Append(    "OUTP_PRESC.PRESC_NO, "                                                ); // �������
            sb.Append(    "OUTP_PRESC.VISIT_DATE, "                                              ); // ��������
            sb.Append(    "OUTP_PRESC.VISIT_NO, "                                                ); // �������
            sb.Append(    "OUTP_ORDERS.ORDERED_BY, "                                             ); // ��������
            sb.Append(    "OUTP_ORDERS.DOCTOR, "                                                 ); // ����ҽ��
            sb.Append(    "OUTP_ORDERS.ORDER_DATE, "                                             ); // ����ʱ��
            sb.Append(    "OUTP_PRESC.CHARGE_INDICATOR "                                         ); // �շѱ�ʶ
            sb.Append("FROM OUTP_ORDERS, "                                                       ); // ����ҽ������¼
            sb.Append(    "OUTP_PRESC "                                                          ); // ����ҽ����ϸ��¼
            sb.Append("WHERE OUTP_PRESC.SERIAL_NO = OUTP_ORDERS.SERIAL_NO "                      ); // ��ˮ��
            sb.Append(    "AND OUTP_ORDERS.PATIENT_ID = " + SQL.SqlConvert(patientId)            ); // ���˱�ʶ��

            return _connection.SelectData(sb.ToString());
        }
        #endregion
    }
}
