using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public class NursingLianXiDbI
    {
        protected DbAccess _oraConnect;

        public NursingLianXiDbI(DbAccess oraConnect)
        {
            _oraConnect = oraConnect;
        }

        /// <summary>
        ///获取病人信息的护理记录
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="?"></param>
        /// <param name="?"></param>
        /// <param name="?"></param>
        /// <param name="?"></param>
        public DataSet GetNursingData(string patientId, string visitId, DateTime dtRngStart, DateTime dtRngEnd, string vitalSigns)
       {
           string sql = string.Empty;

           dtRngEnd = dtRngEnd.AddDays(1);

           sql += "SELECT VITAL_SIGNS_REC.PATIENT_ID, ";                                               // 病人标识号
           sql += "VITAL_SIGNS_REC.VISIT_ID, ";                                                    // 病人本次住院标识
           sql += "VITAL_SIGNS_REC.RECORDING_DATE, ";                                              // 记录日期
           sql += "VITAL_SIGNS_REC.TIME_POINT, ";                                                  // 时间点
           sql += "VITAL_SIGNS_REC.VITAL_SIGNS, ";                                                 // 记录项目
           sql += "VITAL_SIGNS_REC.UNITS, ";                                                       // 项目单位
           sql += "VITAL_SIGNS_REC.CLASS_CODE, ";                                                  // 类别代码
           sql += "VITAL_SIGNS_REC.VITAL_CODE, ";                                                  // 项目代码
           sql += "VITAL_SIGNS_REC.VITAL_SIGNS_CVALUES, ";                                         // 项目数值
           sql += "VITAL_SIGNS_REC.WARD_CODE, ";                                                   // 病区代码
           sql += "VITAL_SIGNS_REC.NURSE, ";                                                       // 护士
           sql += "VITAL_SIGNS_REC.MEMO ";                                                         // 备注
           sql += "FROM VITAL_SIGNS_REC ";                                                             // 病人体症记录
           sql += "WHERE PATIENT_ID = " + SQL.SqlConvert(patientId);
           sql += "AND VISIT_ID = " + SQL.SqlConvert(visitId);
           sql += "AND (TIME_POINT >= " + SQL.GetOraDbDate(dtRngStart.ToString(ComConst.FMT_DATE.SHORT));
           sql += "AND TIME_POINT < " + SQL.GetOraDbDate(dtRngEnd.ToString(ComConst.FMT_DATE.SHORT));
           sql += ") ";
           if (vitalSigns.Length > 0)
           {
               sql += "AND VITAL_SIGNS_REC.VITAL_SIGNS = " + SQL.SqlConvert(vitalSigns);
           }
           sql += "ORDER BY ";
           sql += "TIME_POINT ASC";

           return _oraConnect.SelectData(sql);
       }

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
    }
}
