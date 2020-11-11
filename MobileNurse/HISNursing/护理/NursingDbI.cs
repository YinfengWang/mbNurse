namespace HISPlus
{
    using System;
    using System.Data;

    public class NursingDbI
    {
        protected DbAccess _oraConnect;
        protected DbAccess _sqlConnect;
        public static readonly string T_NURSE_ITEM_DICT = "NURSE_TEMPERATURE_ITEM_DICT";

        public NursingDbI(DbAccess oraConnect, DbAccess sqlConnect)
        {
            this._oraConnect = oraConnect;
            this._sqlConnect = sqlConnect;
        }

        public DataSet GetNursingData(string patientId, string visitId, DateTime dtStart, DateTime dtEnd, string vitalSigns)
        {
            string sqlSel = string.Empty;
            dtEnd = dtEnd.AddDays(1.0);
            sqlSel = ((((((((((sqlSel + "SELECT VITAL_SIGNS_REC.PATIENT_ID, " + "VITAL_SIGNS_REC.VISIT_ID, ") + "VITAL_SIGNS_REC.RECORDING_DATE, " + "VITAL_SIGNS_REC.TIME_POINT, ") + "VITAL_SIGNS_REC.VITAL_SIGNS, " + "VITAL_SIGNS_REC.UNITS, ") + "VITAL_SIGNS_REC.CLASS_CODE, " + "VITAL_SIGNS_REC.VITAL_CODE, ") + "VITAL_SIGNS_REC.VITAL_SIGNS_CVALUES, " + "VITAL_SIGNS_REC.WARD_CODE, ") + "VITAL_SIGNS_REC.NURSE " + "FROM VITAL_SIGNS_REC ") + "WHERE PATIENT_ID = " + SqlManager.SqlConvert(patientId)) + "AND VISIT_ID = " + SqlManager.SqlConvert(visitId)) + "AND (RECORDING_DATE >= " + SqlManager.GetOraDbDate(dtStart.ToString(ComConst.FMT_DATE.SHORT))) + "AND RECORDING_DATE < " + SqlManager.GetOraDbDate(dtEnd.ToString(ComConst.FMT_DATE.SHORT))) + ") ";
            if (vitalSigns.Length > 0)
            {
                sqlSel = sqlSel + "AND VITAL_SIGNS_REC.VITAL_SIGNS = " + SqlManager.SqlConvert(vitalSigns);
            }
            sqlSel = sqlSel + "ORDER BY " + "TIME_POINT ASC";
            return this._oraConnect.SelectData(sqlSel);
        }

        public int GetNursingItemAttribute(string vitalCode)
        {
            string sqlSel = "SELECT ATTRIBUTE FROM " + T_NURSE_ITEM_DICT + " WHERE VITAL_CODE = " + SqlManager.SqlConvert(vitalCode);
            if (this._sqlConnect.SelectValue(sqlSel))
            {
                return int.Parse(this._sqlConnect.GetResult(0));
            }
            return -1;
        }

        public DataSet GetNursingItemDict(string wardCode)
        {
            string sqlSel = "SELECT * FROM " + T_NURSE_ITEM_DICT;
            if (wardCode.Trim().Length > 0)
            {
                sqlSel = sqlSel + " WHERE DEPT_CODE = " + SqlManager.SqlConvert(wardCode);
            }
            sqlSel = sqlSel + " ORDER BY CLASS_CODE, VITAL_CODE";
            return this._oraConnect.SelectData(sqlSel, T_NURSE_ITEM_DICT);
        }

        public DataSet GetNursingItemDict_Sql(string wardCode, int attribute)
        {
            string sqlSel = "SELECT * FROM " + T_NURSE_ITEM_DICT;
            if (wardCode.Trim().Length > 0)
            {
                sqlSel = sqlSel + " WHERE WARD_CODE = " + SqlManager.SqlConvert(wardCode);
                if (attribute > -1)
                {
                    sqlSel = sqlSel + " AND ATTRIBUTE = " + SqlManager.SqlConvert(attribute.ToString());
                }
            }
            sqlSel = sqlSel + " ORDER BY CLASS_CODE, VITAL_CODE";
            return this._sqlConnect.SelectData(sqlSel, T_NURSE_ITEM_DICT);
        }
    }
}

