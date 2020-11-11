using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using SqlManager = HISPlus.SqlManager;

namespace HISPlus
{    
    class BodyTemperatureCom
    {        
        public static readonly string T_VITAL_SIGNS_REC         = "VITAL_SIGNS_REC";                // ��: ����������¼
        public static readonly string T_OPERATION_REC           = "OPERATION";                      // ��: ������¼
        public static readonly string T_NURSING_CLASS_DICT      = "NURSE_TEMPERATURE_CLASS_DICT";   // ��: ���������ֵ� 
        public static readonly string T_NURSING_ITEM            = "NURSE_ITEM_DICT";                // ��: ������Ŀ�ֵ�
                
        public DataSet          DsData  = new DataSet();                // ����
        
        /// <summary>
        /// ��ȡ�������¼�¼
        /// </summary>
        /// <param name="patientId">���˱�ʶ��</param>
        /// <param name="visitId">���ξ������</param>
        /// <returns>TRUE: �ɹ�; FALSE: ʧ��</returns>
        public bool GetVitalSignsRec(string patientId, string visitId)
        {
            // ��ȡ�����¼�
            string sql = string.Empty;
            
            sql = "SELECT * FROM VITAL_SIGNS_REC ";
            sql += "WHERE PATIENT_ID = " + SqlManager.SqlConvert(patientId);
            sql += "AND VISIT_ID = " + SqlManager.SqlConvert(visitId);
            
            GVars.OracleAccess.SelectData(sql, T_VITAL_SIGNS_REC, ref DsData);
            
            // ��ȡת�Ƽ�¼
            getTransferRec(patientId, visitId);
            
            // ��ȡ���²�����¼
            getTemperatureRec(patientId, visitId);

            return true;
        }
        
        
        /// <summary>
        /// ��ȡ������¼
        /// </summary>
        /// <param name="patientId">���˱�ʶ��</param>
        /// <param name="visitId">���ξ������</param>
        /// <returns>TRUE: �ɹ�; FALSE: ʧ��</returns>
        public bool GetOperationRec(string patientId, string visitId)
        {
            string sql = string.Empty;
            
            sql = "SELECT * FROM OPERATION ";
            sql += "WHERE PATIENT_ID = " + HISPlus.SqlManager.SqlConvert(patientId);
            sql += "AND VISIT_ID = " + HISPlus.SqlManager.SqlConvert(visitId);
            sql += "ORDER BY OPERATING_DATE DESC";
            
            GVars.OracleAccess.SelectData(sql, T_OPERATION_REC, ref DsData);
            return true;
        }        
        
        
        /// <summary>
        /// ��ȡ����ת�Ƽ�¼
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>        
        private bool getTransferRec(string patientId, string visitId)
        {
            string TRANS_DEPT = "ת��";
            
            // ��ȡת�Ƽ�¼
            string sql = string.Empty;
            
            sql += "SELECT ";
            sql +=     "ADMISSION_DATE_TIME, ";
            sql +=     "(SELECT DEPT_NAME FROM DEPT_DICT WHERE DEPT_DICT.DEPT_CODE = DEPT_STAYED) DEPT_IN, ";
            sql +=     "TRANSFER.DISCHARGE_DATE_TIME, ";
            sql +=     "(SELECT DEPT_NAME FROM DEPT_DICT WHERE DEPT_DICT.DEPT_CODE = DEPT_TRANSFERED_TO) DEPT_OUT, ";
            sql +=     "DOCTOR_IN_CHARGE ";
            sql += "FROM TRANSFER ";
            sql += "WHERE PATIENT_ID = " + SqlManager.SqlConvert(patientId);
            sql +=     "AND VISIT_ID = " + SqlManager.SqlConvert(visitId);
            sql +=     "AND DEPT_TRANSFERED_TO IS NOT NULL ";
            
            DataSet dsTransfer = GVars.OracleAccess.SelectData(sql);
            
            // ��ת���¼����浽�����¼���
            // ��ȡת���¼��Ĵ���
            string vitalCode = string.Empty;
            sql = "SELECT VITAL_CODE FROM NURSE_TEMPERATURE_ITEM_DICT WHERE VITAL_SIGNS = " + SqlManager.SqlConvert(TRANS_DEPT);
            if (GVars.SqlserverAccess.SelectValue(sql) == true)
            {
                vitalCode = GVars.SqlserverAccess.GetResult(0);
            }
            
            if (vitalCode.Length == 0)
            {
                return false;
            }
            
            // ɾ��ԭ����ת���¼�
            DataRow[] drFind = DsData.Tables[T_VITAL_SIGNS_REC].Select("VITAL_SIGNS = " + SqlManager.SqlConvert(TRANS_DEPT));
            for(int i = drFind.Length - 1; i >= 0; i--)
            {
                drFind[i].Delete();
            }
            
            DsData.Tables[T_VITAL_SIGNS_REC].AcceptChanges();
            
            // �����µ�ת���¼�
            foreach(DataRow dr in dsTransfer.Tables[0].Rows)
            {
                DataRow drNew = DsData.Tables[T_VITAL_SIGNS_REC].NewRow();
                
                drNew["PATIENT_ID"]         = patientId;
                drNew["VISIT_ID"]           = visitId;
                drNew["RECORDING_DATE"]     = ((DateTime)dr["DISCHARGE_DATE_TIME"]).Date;
                drNew["TIME_POINT"]         = dr["DISCHARGE_DATE_TIME"];
                drNew["VITAL_CODE"]         = vitalCode;
                drNew["VITAL_SIGNS"]        = TRANS_DEPT;
                drNew["CLASS_CODE"]         = "C";
                drNew["VITAL_SIGNS_CVALUES"]= dr["DEPT_IN"].ToString() + "��" + dr["DEPT_OUT"].ToString();
                drNew["WARD_CODE"]          = GVars.User.DeptCode;
                drNew["NURSE"]              = dr["DOCTOR_IN_CHARGE"].ToString();
                
                DsData.Tables[T_VITAL_SIGNS_REC].Rows.Add(drNew);
            }
            
            return true;
        }
        
                
        /// <summary>
        /// ��ȡ�������²����¼�
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>        
        private bool getTemperatureRec(string patientId, string visitId)
        {
            string ITEM_NAME = "����";

            // ��ȡת�Ƽ�¼
            StringBuilder sb = new StringBuilder(); 
            sb.Append("SELECT TIME_POINT, NURSE "                                                ); // ʱ���
            sb.Append("FROM VITAL_SIGNS_REC "                                                    ); // ������֢��¼
            sb.Append("WHERE PATIENT_ID = " + SqlManager.SqlConvert(patientId)                   ); // ���˱�ʶ��
            sb.Append(    "AND VISIT_ID = " + SqlManager.SqlConvert(visitId)                     ); // ���˱���סԺ��ʶ
            sb.Append(    "AND VITAL_SIGNS LIKE '%����' "                                        );
            sb.Append(    "AND VITAL_SIGNS_CVALUES <= '35' "                                     );
            
            DataSet ds = GVars.OracleAccess.SelectData(sb.ToString());
            
            // ɾ��ԭ����ת���¼�
            DataRow[] drFind = DsData.Tables[T_VITAL_SIGNS_REC].Select("VITAL_SIGNS = " + SqlManager.SqlConvert(ITEM_NAME));
            for(int i = drFind.Length - 1; i >= 0; i--)
            {
                drFind[i].Delete();
            }
            
            DsData.Tables[T_VITAL_SIGNS_REC].AcceptChanges();
            
            DateTime dtPre      = DateTime.Now;
            bool     blnFirst   = true;
            bool     blnAdd     = false;

            // �����µ�ת���¼�
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                DateTime dtNow = (DateTime)dr["TIME_POINT"];
                
                blnAdd = true;

                if (blnFirst)
                {
                    dtPre = dtNow;                    
                }
                else
                {
                    if (dtPre.Year == dtNow.Year
                        && dtPre.Month == dtNow.Month
                        && dtPre.Day == dtNow.Day
                        && dtPre.Hour / 2 == dtNow.Hour / 2)
                    {
                        blnAdd = false;
                    }
                    else
                    {
                        dtPre = dtNow;
                    }
                }

                blnFirst = false;

                if (blnAdd)
                {
                    DataRow drNew = DsData.Tables[T_VITAL_SIGNS_REC].NewRow();

                    drNew["PATIENT_ID"] = patientId;
                    drNew["VISIT_ID"] = visitId;
                    drNew["RECORDING_DATE"] = ((DateTime)dr["TIME_POINT"]).Date;
                    drNew["TIME_POINT"] = dr["TIME_POINT"];
                    drNew["VITAL_CODE"] = "-1";
                    drNew["VITAL_SIGNS"] = ITEM_NAME;
                    drNew["CLASS_CODE"] = "C";
                    drNew["VITAL_SIGNS_CVALUES"] = ITEM_NAME;
                    drNew["WARD_CODE"] = GVars.User.DeptCode;
                    drNew["NURSE"] = dr["NURSE"].ToString();

                    DsData.Tables[T_VITAL_SIGNS_REC].Rows.Add(drNew);
                }
            }
            
            return true;
        }
        
                
        /// <summary>
		/// ��ȡ��������
		/// </summary>
		/// <returns>TRUE: �ɹ�; FALSE: ʧ��</returns>
		public bool GetNursingItemClass()
		{
			string sql = "SELECT * FROM NURSE_TEMPERATURE_CLASS_DICT";
			
			GVars.OracleAccess.SelectData(sql, T_NURSING_CLASS_DICT, ref DsData);
			return true;
		}
		
		
		/// <summary>
		/// ��ȡ������Ŀ�ֵ�
		/// </summary>
		/// <returns></returns>
		public bool GetNursingItem()
		{
		    string sql = "SELECT * FROM NURSE_TEMPERATURE_ITEM_DICT WHERE WARD_CODE = " + SqlManager.SqlConvert(GVars.User.DeptCode);
			
			GVars.SqlserverAccess.SelectData(sql, T_NURSING_ITEM, ref DsData);
			return true;
		}
		
		
		/// <summary>
		/// ��ȡϵͳ����
		/// </summary>
		/// <returns></returns>
		public DateTime GetSysDate()
		{
		    if (GVars.OracleAccess.SelectValue("SELECT SYSDATE FROM DUAL") == true)
		    {
		        return DateTime.Parse(GVars.OracleAccess.GetResult(0));
		    }
		    else
		    {
		        throw new Exception("��ȡϵͳ���ڴ���!");
		    }
		}
		
        
        /// <summary>
        /// ���ݺ��� (�Ի������ݱ�����һ��������, ���������и�ֵ)
        /// </summary>
        /// <returns></returns>
        public bool ReTreatNursingRec()
        {            
            // ��������
            string colAttribute = "ATTRIBUTE";
            
            DataTable dtDest = DsData.Tables[T_VITAL_SIGNS_REC];
            if (dtDest.Columns.IndexOf(colAttribute) < 0)
            {
                dtDest.Columns.Add(colAttribute, Type.GetType("System.String"));
            }
            
            // Ϊ�����и�ֵ
            DataTable dtDict = DsData.Tables[T_NURSING_ITEM];
            
            string vitalCode = string.Empty;
            string attribute = string.Empty;
            DataRow[] drData = DsData.Tables[T_VITAL_SIGNS_REC].Select(string.Empty, "VITAL_CODE");
            DataRow[] drFind = null;
            
            for(int i = 0; i < drData.Length; i++)
            {
                // ��ȡ����ֵ
                if (vitalCode.Equals(drData[i]["VITAL_CODE"].ToString()) == false)
                {
                    vitalCode = drData[i]["VITAL_CODE"].ToString();
                    
                    drFind = dtDict.Select("VITAL_CODE = " + SqlManager.SqlConvert(vitalCode));
                    if (drFind.Length > 0)
                    {
                        attribute = drFind[0][colAttribute].ToString();
                    }
                    else
                    {
                        attribute = "0";
                    }
                }
                
                if (drData[i]["VITAL_SIGNS"].ToString().Equals("����") == true)
                {
                    attribute = "4";
                }

                // ��������ֵ
                drData[i][colAttribute] = attribute;
            }
            
            return true;            
        }        
        
        
        /// <summary>
        /// ��ȡ���µ����ӵ���Ŀ
        /// </summary>
        /// <returns></returns>
        public string GetTemperatureAppendItem()
        {
            string sql = string.Empty;
            sql = "SELECT PARAMETER_VALUE FROM APP_CONFIG WHERE DEPT_CODE = " + SqlManager.SqlConvert(GVars.User.DeptCode);
            sql += "AND PARAMETER_NAME = 'TEMPERATURE_APPEND_ITEM'";
            
            if (GVars.SqlserverAccess.SelectValue(sql) == true)
            {
                return GVars.SqlserverAccess.GetResult(0);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
