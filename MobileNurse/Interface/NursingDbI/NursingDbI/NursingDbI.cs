using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public class NursingDbI
    {
        protected DbAccess _oraConnect;

        public NursingDbI(DbAccess oraConnect)
        {
            _oraConnect = oraConnect;
        }


        /// <summary>
        /// ��ȡ������Ŀ�������
        /// </summary>
        /// <returns></returns>
        public int GetNursingItemAttribute(string vitalCode, string wardCode)
        {
            string sql = "SELECT ATTRIBUTE FROM NURSING_ITEM_DICT "
                        + "WHERE VITAL_CODE = " + SQL.SqlConvert(vitalCode)
                        + "AND WARD_CODE = " + SQL.SqlConvert(wardCode);

            if (_oraConnect.SelectValue(sql) == true)
            {
                return int.Parse(_oraConnect.GetResult(0));
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
            sql += "VITAL_SIGNS_REC.VISIT_ID, ";                                                    // ���˱���סԺ��ʶ
            sql += "VITAL_SIGNS_REC.RECORDING_DATE, ";                                              // ��¼����
            sql += "VITAL_SIGNS_REC.TIME_POINT, ";                                                  // ʱ���
            sql += "VITAL_SIGNS_REC.VITAL_SIGNS, ";                                                 // ��¼��Ŀ
            sql += "VITAL_SIGNS_REC.UNITS, ";                                                       // ��Ŀ��λ
            sql += "VITAL_SIGNS_REC.CLASS_CODE, ";                                                  // ������
            sql += "VITAL_SIGNS_REC.VITAL_CODE, ";                                                  // ��Ŀ����
            sql += "VITAL_SIGNS_REC.VITAL_SIGNS_CVALUES, ";                                         // ��Ŀ��ֵ
            sql += "VITAL_SIGNS_REC.WARD_CODE, ";                                                   // ��������
            sql += "VITAL_SIGNS_REC.NURSE, ";                                                       // ��ʿ
            sql += "VITAL_SIGNS_REC.MEMO ";                                                         // ��ע
            sql += "FROM VITAL_SIGNS_REC ";                                                             // ������֢��¼
            sql += "WHERE PATIENT_ID = " + SQL.SqlConvert(patientId);
            sql += "AND VISIT_ID = " + SQL.SqlConvert(visitId);
            sql += "AND (TIME_POINT >= " + SQL.GetOraDbDate(dtStart.ToString(ComConst.FMT_DATE.SHORT));
            sql += "AND TIME_POINT < " + SQL.GetOraDbDate(dtEnd.ToString(ComConst.FMT_DATE.SHORT));
            sql += ") ";
            if (vitalSigns.Length > 0)
            {
                sql += "AND VITAL_SIGNS_REC.VITAL_SIGNS = " + SQL.SqlConvert(vitalSigns);
            }
            sql += "ORDER BY ";
            sql += "TIME_POINT ASC";

            return _oraConnect.SelectData(sql, "VITAL_SIGNS_REC");
        }

        /// <summary>
        /// ��ȡ���˵Ļ����¼
        /// </summary>        
        /// <returns></returns>		
        public bool SaveNursingData(ref DataSet dsNursing, string patientId, string visitId)
        {
            string sql = string.Empty;

            sql += "SELECT VITAL_SIGNS_REC.PATIENT_ID, ";                                               // ���˱�ʶ��
            sql += "VITAL_SIGNS_REC.VISIT_ID, ";                                                    // ���˱���סԺ��ʶ
            sql += "VITAL_SIGNS_REC.RECORDING_DATE, ";                                              // ��¼����
            sql += "VITAL_SIGNS_REC.TIME_POINT, ";                                                  // ʱ���
            sql += "VITAL_SIGNS_REC.VITAL_SIGNS, ";                                                 // ��¼��Ŀ
            sql += "VITAL_SIGNS_REC.UNITS, ";                                                       // ��Ŀ��λ
            sql += "VITAL_SIGNS_REC.CLASS_CODE, ";                                                  // ������
            sql += "VITAL_SIGNS_REC.VITAL_CODE, ";                                                  // ��Ŀ����
            sql += "VITAL_SIGNS_REC.VITAL_SIGNS_CVALUES, ";                                         // ��Ŀ��ֵ
            sql += "VITAL_SIGNS_REC.WARD_CODE, ";                                                   // ��������
            sql += "VITAL_SIGNS_REC.NURSE, ";                                                       // ��ʿ
            sql += "VITAL_SIGNS_REC.MEMO ";                                                         // ��ע
            sql += "FROM VITAL_SIGNS_REC ";                                                             // ������֢��¼
            sql += "WHERE PATIENT_ID = " + SQL.SqlConvert(patientId);
            sql += "AND VISIT_ID = " + SQL.SqlConvert(visitId);


            _oraConnect.Update(ref dsNursing, "VITAL_SIGNS_REC", sql);

            return true;
        }

        /// <summary>
        /// ��ȡ���˵Ļ����¼
        /// </summary>
        /// <returns></returns>		
        public DataSet GetNursingColors()
        {
            string sql = string.Empty;

            sql += "SELECT A.SERIAL_NO,A.NURSING_CLASS_CODE,A.NURSING_CLASS_NAME,A.SHOW_COLOR FROM NURSING_CLASS_DICT A ORDER BY A.SERIAL_NO";
            return _oraConnect.SelectData(sql);
        }

        public void DelNursingIetm(string patient_id, string visit_id, string recording_date, string time_point, string class_code, string vital_code)
        {
            string delSql = @"DELETE VITAL_SIGNS_REC    " +
                              "   WHERE PATIENT_ID = '" + patient_id + "'   " +
                              "     AND VISIT_ID = '" + visit_id + "'   " +
                              "     AND RECORDING_DATE = to_date('" + recording_date + "','yyyy-MM-dd')   " +
                              "     AND TIME_POINT = to_date('" + time_point + "','yyyy-MM-dd HH24:mi:ss')   " +
                              "     AND CLASS_CODE = '" + class_code + "'   " +
                              "     AND VITAL_CODE = '" + vital_code + "'";
            _oraConnect.ExecuteNoQuery(delSql);
        }

        /// <summary>
        /// ��ȡ���˵Ļ����¼
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>		
        public DataSet GetNursingItemsData(string patientId, string visitId, DateTime dtStart, DateTime dtEnd, string vitalCodeList)
        {
            string sql = string.Empty;

            dtEnd = dtEnd.AddDays(1);

            sql += "SELECT  VITAL_SIGNS_REC.PATIENT_ID, ";                                               // ���˱�ʶ��
            sql += "VITAL_SIGNS_REC.VISIT_ID, ";                                                    // ���˱���סԺ��ʶ
            sql += "VITAL_SIGNS_REC.RECORDING_DATE, ";                                              // ��¼����
            sql += "VITAL_SIGNS_REC.TIME_POINT," +
                       "VITAL_SIGNS_REC.TIME_POINT as  TIME_POINT1,";                                                  // ʱ���
            sql += "VITAL_SIGNS_REC.VITAL_SIGNS, ";                                                 // ��¼��Ŀ
            sql += "VITAL_SIGNS_REC.UNITS, ";                                                       // ��Ŀ��λ
            sql += "VITAL_SIGNS_REC.CLASS_CODE, ";                                                  // ������
            sql += "VITAL_SIGNS_REC.VITAL_CODE, ";                                                  // ��Ŀ����VITAL_SIGNS_CVALUES || '/' ||  units   VITAL_SIGNS_CVALUES
            //  sql +=     "VITAL_SIGNS_REC.VITAL_SIGNS_CVALUES, ";                                         // ��Ŀ��ֵ
            sql += "VITAL_SIGNS_REC.VITAL_SIGNS_CVALUES || '/' ||  VITAL_SIGNS_REC.units  VITAL_SIGNS_CVALUES, ";                                         // ��Ŀ��ֵ
            sql += "VITAL_SIGNS_REC.WARD_CODE, ";                                                   // ��������
            sql += "VITAL_SIGNS_REC.MEMO, ";                                                        // ��ע
            sql += "VITAL_SIGNS_REC.NURSE ";                                                        // ��ʿ
            sql += "FROM VITAL_SIGNS_REC ";                                                             // ������֢��¼
            sql += "WHERE PATIENT_ID = " + SQL.SqlConvert(patientId);
            sql += "AND VISIT_ID = " + SQL.SqlConvert(visitId);
            sql += "AND (RECORDING_DATE >= " + SQL.GetOraDbDate(dtStart.ToString(ComConst.FMT_DATE.SHORT));
            sql += "AND RECORDING_DATE < " + SQL.GetOraDbDate(dtEnd.ToString(ComConst.FMT_DATE.SHORT));
            sql += ") ";
            if (vitalCodeList.Length > 0)
            {
                sql += "AND VITAL_SIGNS_REC.VITAL_CODE IN (" + vitalCodeList + ") ";
            }
            sql += "ORDER BY ";
            sql += "TIME_POINT ASC";

            return _oraConnect.SelectData(sql);
        }


        /// <summary>
        /// ��ȡ������Ŀ�ֵ�(HIS��)
        /// </summary>
        /// <returns></returns>
        public DataSet GetNursingItemDict(string wardCode)
        {
            string sql = "SELECT "
                            + "CLASS_CODE, "
                            + "VITAL_CODE,"
                            + "VITAL_SIGNS,"
                            + "UNIT,"
                            + "DEPT_CODE, "
                            + "0 AS ENABLED "
                        + "FROM NURSE_TEMPERATURE_ITEM_DICT ";

            if (wardCode.Trim().Length > 0)
            {
                sql += " WHERE DEPT_CODE = " + SQL.SqlConvert(wardCode);
            }

            return _oraConnect.SelectData(sql, "NURSE_TEMPERATURE_ITEM_DICT");
        }


        /// <summary>
        /// ��ȡ������Ŀ�ֵ� (�ƶ�������)
        /// </summary>
        /// <returns></returns>
        public DataSet GetNursingItemDictMobile(string wardCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT WARD_CODE DEPT_CODE, ");
            sb.Append("CLASS_CODE, ");
            sb.Append("VITAL_CODE, ");
            sb.Append("VITAL_SIGNS, ");
            sb.Append("UNIT, ");
            sb.Append("ATTRIBUTE, ");
            sb.Append("SHOW_ORDER, ");
            sb.Append("VALUE_TYPE, ");
            sb.Append("VALUE_SCOPE, ");
            sb.Append("PROPORTION, ");
            sb.Append("ENABLED, ");
            sb.Append("MEMO ");
            sb.Append("FROM NURSING_ITEM_DICT ");
            sb.Append("WHERE ");
            sb.Append("WARD_CODE = " + SqlManager.SqlConvert(wardCode));
            // sb.Append(    "AND ENABLED = '1' "                                                   );

            string sql = sb.ToString();

            return _oraConnect.SelectData(sql, "NURSING_ITEM_DICT");
        }


        public bool SaveNursingItemDictMobile(ref DataSet dsNurseItemDict, string wardCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT WARD_CODE DEPT_CODE, ");
            sb.Append("CLASS_CODE, ");
            sb.Append("VITAL_CODE, ");
            sb.Append("VITAL_SIGNS, ");
            sb.Append("UNIT, ");
            sb.Append("ATTRIBUTE, ");
            sb.Append("SHOW_ORDER, ");
            sb.Append("VALUE_TYPE, ");
            sb.Append("VALUE_SCOPE, ");
            sb.Append("PROPORTION, ");
            sb.Append("ENABLED, ");
            sb.Append("MEMO  ");
            sb.Append("FROM NURSING_ITEM_DICT ");
            sb.Append("WHERE ");
            sb.Append("WARD_CODE = " + SqlManager.SqlConvert(wardCode));

            string sql = sb.ToString();

            _oraConnect.Update(ref dsNurseItemDict, "NURSING_ITEM_DICT", sql);

            return true;
        }


    }
}
