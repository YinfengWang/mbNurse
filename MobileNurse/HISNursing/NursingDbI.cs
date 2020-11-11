using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public class NursingDbI2
    {
        public static readonly string T_NURSE_ITEM_DICT = "NURSE_TEMPERATURE_ITEM_DICT";

        protected DbAccess _oraConnect;
        protected DbAccess _sqlConnect;
        
        public NursingDbI2(DbAccess oraConnect, DbAccess sqlConnect)
        {
            _oraConnect = oraConnect;
            _sqlConnect = sqlConnect;
        }
        

        /// <summary>
        /// ��ȡ������Ŀ�������
        /// </summary>
        /// <returns></returns>
        public int GetNursingItemAttribute(string vitalCode)
        {
            string sql = "SELECT ATTRIBUTE FROM " + T_NURSE_ITEM_DICT + " WHERE VITAL_CODE = " + SQL.SqlConvert(vitalCode);
            
            if (_sqlConnect.SelectValue(sql) == true)
            {
                return int.Parse(_sqlConnect.GetResult(0));
            }
            
            return -1;
        }
        
        
        /// <summary>
        /// ��ȡ���˵Ļ����¼
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>		
		public DataSet GetNursingData(string patientId, string visitId, DateTime dtStart, DateTime dtEnd, string vitalSigns)
		{
		    string sql = string.Empty;
		    
		    dtEnd = dtEnd.AddDays(1);
		    
	        sql += "SELECT VITAL_SIGNS_REC.PATIENT_ID, ";                                               // ���˱�ʶ��
            sql +=     "VITAL_SIGNS_REC.VISIT_ID, ";                                                    // ���˱���סԺ��ʶ
            sql +=     "VITAL_SIGNS_REC.RECORDING_DATE, ";                                              // ��¼����
            sql +=     "VITAL_SIGNS_REC.TIME_POINT, ";                                                  // ʱ���
            sql +=     "VITAL_SIGNS_REC.VITAL_SIGNS, ";                                                 // ��¼��Ŀ
            sql +=     "VITAL_SIGNS_REC.UNITS, ";                                                       // ��Ŀ��λ
            sql +=     "VITAL_SIGNS_REC.CLASS_CODE, ";                                                  // ������
            sql +=     "VITAL_SIGNS_REC.VITAL_CODE, ";                                                  // ��Ŀ����
            sql +=     "VITAL_SIGNS_REC.VITAL_SIGNS_CVALUES, ";                                         // ��Ŀ��ֵ
            sql +=     "VITAL_SIGNS_REC.WARD_CODE, ";
            sql +=     "VITAL_SIGNS_REC.NURSE ";
            sql += "FROM VITAL_SIGNS_REC ";                                                             // ������֢��¼
            sql += "WHERE PATIENT_ID = " + SQL.SqlConvert(patientId);
            sql +=     "AND VISIT_ID = " + SQL.SqlConvert(visitId);
            sql +=     "AND (RECORDING_DATE >= " + SQL.GetOraDbDate(dtStart.ToString(ComConst.FMT_DATE.SHORT));
            sql +=          "AND RECORDING_DATE < " + SQL.GetOraDbDate(dtEnd.ToString(ComConst.FMT_DATE.SHORT));
            sql +=          ") ";
            if (vitalSigns.Length > 0)
            {
                sql += "AND VITAL_SIGNS_REC.VITAL_SIGNS = " + SQL.SqlConvert(vitalSigns);
            }
            sql += "ORDER BY ";
            sql +=     "TIME_POINT ASC";
                        
            return _oraConnect.SelectData(sql);
		}
		
		
		/// <summary>
		/// ��ȡ������Ŀ�ֵ�
		/// </summary>
		/// <returns></returns>
		public DataSet GetNursingItemDict(string wardCode)
		{
		    string sql = "SELECT * FROM " + T_NURSE_ITEM_DICT;

            if (wardCode.Trim().Length > 0)
            {
                sql += " WHERE DEPT_CODE = " + SQL.SqlConvert(wardCode);
            }

            sql += " ORDER BY CLASS_CODE, VITAL_CODE";

		    return _oraConnect.SelectData(sql, T_NURSE_ITEM_DICT);
		}


        /// <summary>
		/// ��ȡ������Ŀ�ֵ�
		/// </summary>
		/// <returns></returns>
		public DataSet GetNursingItemDict_Sql(string wardCode, int attribute)
		{
		    string sql = "SELECT * FROM " + T_NURSE_ITEM_DICT;

            if (wardCode.Trim().Length > 0)
            {
                sql += " WHERE WARD_CODE = " + SQL.SqlConvert(wardCode);

                if (attribute > -1)
                {
                    sql += " AND ATTRIBUTE = " + SQL.SqlConvert(attribute.ToString());
                }
            }

            sql += " ORDER BY CLASS_CODE, VITAL_CODE";

		    return _sqlConnect.SelectData(sql, T_NURSE_ITEM_DICT);
		}
    }
}
