using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SqlConvert = HISPlus.SqlManager;

namespace HISPlus
{
    class VitalSignRec_WardCom
    {
        #region ����
        public VitalSignRec_Ward_Ds DsVitalSignRec = null;
        #endregion

        public VitalSignRec_WardCom()
        { 
        }


        /// <summary>
        /// ��ȡ�������������۲쵥
        /// </summary>
        /// <param name="dtDay"></param>
        /// <param name="wardCode"></param>
        /// <returns></returns>
        public void GetVitalSignRec_Ward(DateTime dtDay, string wardCode)
        { 
            DsVitalSignRec = new VitalSignRec_Ward_Ds();

            // ��ȡ�����б�
            DataSet ds = getPatientList(wardCode);
            if (ds != null && ds.Tables.Count > 0)
            {
                DsVitalSignRec.Tables[0].Merge(ds.Tables[0], true, MissingSchemaAction.Error);
                DsVitalSignRec.AcceptChanges();
            }
            
            // ��ȡʱ���²����б�
            ds = getPatient_FilterOper(wardCode);
            if (ds != null && ds.Tables.Count > 0)
            {
                DsVitalSignRec.Tables[0].Merge(ds.Tables[0], true, MissingSchemaAction.Error);

                DataRow[] drFind = DsVitalSignRec.Tables[0].Select(string.Empty, string.Empty, DataViewRowState.ModifiedCurrent);

                for (int i = 0; i < drFind.Length; i++)
                {
                    drFind[i]["FREQUENCY"] = "T";
                }
            }

            // ��ȡ���������������ļ�¼����
            ds = getPatient_NurseRec(dtDay, wardCode);
            mergeNurseRec(ref DsVitalSignRec, ref ds);

            // ���ؽ��
            return;
        }


        /// <summary>
        /// ��ȡ�����������˵��б�
        /// </summary>
        /// <param name="wardCode"></param>
        /// <returns></returns>
        private DataSet getPatientList(string wardCode)
        { 
            string sql = string.Empty;
            //sql += "SELECT PATS_IN_HOSPITAL.PATIENT_ID, ";
            //sql +=     "PATS_IN_HOSPITAL.VISIT_ID, ";
            //sql +=     "(SELECT BED_REC.BED_LABEL FROM BED_REC WHERE BED_REC.WARD_CODE = PATS_IN_HOSPITAL.WARD_CODE AND BED_REC.BED_NO = PATS_IN_HOSPITAL.BED_NO) BED_LABEL, ";
            //sql +=     "(SELECT PAT_MASTER_INDEX.NAME FROM PAT_MASTER_INDEX WHERE PAT_MASTER_INDEX.PATIENT_ID = PATS_IN_HOSPITAL.PATIENT_ID) NAME ";
            //sql += "FROM PATS_IN_HOSPITAL ";
            //sql += "WHERE PATS_IN_HOSPITAL.WARD_CODE = " + SqlConvert.SqlConvert(wardCode);
            //sql += "ORDER BY PATS_IN_HOSPITAL.BED_NO ";

            sql += "SELECT PATIENT_ID, VISIT_ID, NAME, BED_LABEL ";
            sql += "FROM PATIENT_INFO ";
            sql += "WHERE WARD_CODE = " + SqlConvert.SqlConvert(wardCode);
            sql += "ORDER BY BED_NO ";
            
            DataSet ds = GVars.OracleAccess.SelectData_NoKey(sql);
            
            // ��������
            if (ds != null && ds.Tables.Count > 0)
            {
                DataColumn[] dcPrimary = new DataColumn[2];
                dcPrimary[0] = ds.Tables[0].Columns[0];
                dcPrimary[1] = ds.Tables[0].Columns[1];

                ds.Tables[0].PrimaryKey = dcPrimary;
            }

            return ds;
        }


        /// <summary>
        /// ��ȡ����������Ϣ�Ĳ�����Ϣ
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        private DataSet getPatient_FilterOper(string wardCode)
        {
            string sql = string.Empty;

            //sql += "SELECT DISTINCT VITAL_SIGNS_REC.PATIENT_ID, ";                              // ���˱�ʶ��
            //sql +=      "VITAL_SIGNS_REC.VISIT_ID ";                                            // ���˱���סԺ��ʶ
            //sql += "FROM VITAL_SIGNS_REC, ";                                                    // ������֢��¼
            //sql +=      "PATS_IN_HOSPITAL ";                                                    // ��Ժ���˼�¼
            //sql += "WHERE (VITAL_SIGNS = '����' OR VITAL_SIGNS = '����') ";
            //sql +=      "AND TO_DATE(TIME_POINT) > TO_DATE(SYSDATE) - 4 ";
            //sql +=      "AND VITAL_SIGNS_REC.PATIENT_ID = PATS_IN_HOSPITAL.PATIENT_ID ";        // ���˱�ʶ��
            //sql +=      "AND VITAL_SIGNS_REC.VISIT_ID = PATS_IN_HOSPITAL.VISIT_ID ";            // ���˱���סԺ��ʶ
            //sql +=      "AND PATS_IN_HOSPITAL.WARD_CODE = " + SqlConvert.SqlConvert(wardCode);  // ���ڲ�������

            sql += "SELECT DISTINCT VITAL_SIGNS_REC.PATIENT_ID, ";                              // ���˱�ʶ��
            sql +=      "VITAL_SIGNS_REC.VISIT_ID ";                                            // ���˱���סԺ��ʶ
            sql += "FROM VITAL_SIGNS_REC, ";                                                    // ������֢��¼
            sql +=      "PATIENT_INFO ";                                                        // ��Ժ���˼�¼
            sql += "WHERE (VITAL_SIGNS = '����' OR VITAL_SIGNS = '����') ";
            sql +=      "AND TO_DATE(TIME_POINT) > TO_DATE(SYSDATE) - 4 ";
            sql +=      "AND VITAL_SIGNS_REC.PATIENT_ID = PATIENT_INFO.PATIENT_ID ";            // ���˱�ʶ��
            sql +=      "AND VITAL_SIGNS_REC.VISIT_ID = PATIENT_INFO.VISIT_ID ";                // ���˱���סԺ��ʶ
            sql +=      "AND PATIENT_INFO.WARD_CODE = " + SqlConvert.SqlConvert(wardCode);      // ���ڲ�������

            
            DataSet ds = GVars.OracleAccess.SelectData_NoKey(sql);

            // ��������
            if (ds != null && ds.Tables.Count > 0)
            {
                DataColumn[] dcPrimary = new DataColumn[2];
                dcPrimary[0] = ds.Tables[0].Columns[0];
                dcPrimary[1] = ds.Tables[0].Columns[1];

                ds.Tables[0].PrimaryKey = dcPrimary;
            }

            return ds;
        }

        
        /// <summary>
        /// ��ȡ�������¼�������¼
        /// </summary>
        /// <param name="dtDay">����</param>
        /// <param name="wardCode">��������</param>
        /// <returns></returns>
        private DataSet getPatient_NurseRec(DateTime dtDay, string wardCode)
        {
            string sql = string.Empty;

            sql += "SELECT PATIENT_ID, ";                               // ���˱�ʶ��
            sql +=     "VISIT_ID, ";                                    // ���˱���סԺ��ʶ
            sql +=     "TIME_POINT, ";                                  // ʱ���
            sql +=     "VITAL_SIGNS, ";                                 // ��¼��Ŀ
            sql +=     "VITAL_SIGNS_CVALUES ";                          // ��Ŀ��ֵ
            
            sql += "FROM ";
            sql +=     "VITAL_SIGNS_REC ";                              // ������֢��¼

            sql += "WHERE WARD_CODE = " + SqlConvert.SqlConvert(wardCode);
            sql +=     "AND TO_DATE(TIME_POINT) = " + SqlConvert.GetOraDbDate_Short(dtDay);
            
            sql += "ORDER BY ";
            sql +=     "PATIENT_ID, VISIT_ID, TIME_POINT";

            return GVars.OracleAccess.SelectData_NoKey(sql);
        }


        /// <summary>
        /// �ϲ���������
        /// </summary>
        /// <returns></returns>
        private bool mergeNurseRec(ref VitalSignRec_Ward_Ds DsVitalSignRec, ref DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0)
            {
                return true;
            }

            string patientId    = string.Empty;
            string visitId      = string.Empty;
            string vitalSigns   = string.Empty;
            string vitalVal     = string.Empty;
            DateTime dtTimePoint;

            string filterPat    = "PATIENT_ID = '{0}' AND VISIT_ID = '{1}'";

            DataRow drPatient   = null;
            string  fieldName   = string.Empty;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dtTimePoint = (DateTime)dr["TIME_POINT"];
                vitalSigns  = dr["VITAL_SIGNS"].ToString();
                vitalVal    = dr["VITAL_SIGNS_CVALUES"].ToString();

                if (patientId.Equals(dr["PATIENT_ID"].ToString()) == false)
                {
                    // ���Ҳ���
                    DataRow[] drFind = DsVitalSignRec.Tables[0].Select(string.Format(filterPat, 
                        dr["PATIENT_ID"].ToString(), dr["VISIT_ID"].ToString()));

                    if (drFind.Length == 0)
                    {
                        continue;
                    }

                    patientId   = dr["PATIENT_ID"].ToString();                
                    visitId     = dr["VISIT_ID"].ToString();
                    
                    drPatient = drFind[0];
                }

                if (drPatient == null) { continue; }
                
                // �����ֶ�
                fieldName = string.Empty;

                if (vitalSigns.IndexOf("����") >= 0) { fieldName = "T"; }
                if (vitalSigns.IndexOf("����") >= 0) { fieldName = "P"; }
                if (vitalSigns.IndexOf("����") >= 0) { fieldName = "P"; }
                if (vitalSigns.IndexOf("���") >= 0) { fieldName = "STOOL"; }
                if (vitalSigns.IndexOf("С��") >= 0) { fieldName = "PEE"; }
                if (vitalSigns.IndexOf("����") >= 0) { fieldName = "PEE"; }

                if (fieldName.Length == 0)
                {
                    continue;
                }

                // ����Ǵ�С��
                if (fieldName.Length == 1)
                {
                    int intHour = dtTimePoint.Hour + 1;
                    // ȷ��ʱ���
                    if (0 < intHour && intHour <= 4)
                    {
                        fieldName += "2";
                    }
                    else if (4 < intHour && intHour <= 8)
                    {
                        fieldName += "6";
                    }
                    else if (8 < intHour && intHour <= 12)
                    {
                        fieldName += "10";
                    }
                    else if (12 < intHour && intHour <= 16)
                    {
                        fieldName += "14";
                    }
                    else if (16 < intHour && intHour <= 20)
                    {
                        fieldName += "18";
                    }
                    else if (20 < intHour && intHour <= 24)
                    {
                        fieldName += "22";
                    }
                    else
                    {
                        continue;
                    }
                }
                
                drPatient[fieldName] = vitalVal;
            }

            return true;
        }
    }
}
