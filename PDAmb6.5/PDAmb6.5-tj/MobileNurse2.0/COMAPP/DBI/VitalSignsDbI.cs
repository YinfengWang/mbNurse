using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public class VitalSignsDbI
    {
        public VitalSignsDbI()
        {
        }
        
        
        /// <summary>
        /// 获取护理项目字典
        /// </summary>
        /// <returns></returns>
        public DataSet GetNurseItemDict()
        {
            string sql = "SELECT * FROM NURSING_ITEM_DICT "
                        + "WHERE WARD_CODE = " + SQL.SqlConvert(GVars.App.DeptCode);
            
            return GVars.sqlceLocal.SelectData(sql);
        }
        
        
        /// <summary>
        /// 获取病人的护理记录
        /// </summary>
        /// <returns></returns>
        public DataSet GetVitalSignsRecord(string patientId, string visitId, DateTime dtNow)
        {
            string filter = "TIME_POINT >= " + SQL.SqlConvert(dtNow.Date.ToString(ComConst.FMT_DATE.LONG))
                            + " AND TIME_POINT < " + SQL.SqlConvert(dtNow.Date.AddDays(1).ToString(ComConst.FMT_DATE.LONG));
            
            string sql = "SELECT VITAL_SIGNS_REC.*, NURSING_ITEM_DICT.ATTRIBUTE "
                        + " FROM VITAL_SIGNS_REC, NURSING_ITEM_DICT "
                        + " WHERE PATIENT_ID = " + SQL.SqlConvert(patientId)
                        +       " AND VISIT_ID = " + SQL.SqlConvert(visitId)
                        +       " AND VITAL_SIGNS_REC.VITAL_CODE = NURSING_ITEM_DICT.VITAL_CODE "
                        +       " AND VITAL_SIGNS_REC.WARD_CODE = NURSING_ITEM_DICT.WARD_CODE "
                        +       " AND " + filter;
            
            DataSet ds = GVars.sqlceLocal.SelectData(sql, "VITAL_SIGNS_REC");
            
            // 设置主键
            DataColumn[] dcPrimary = new DataColumn[4];
            dcPrimary[0] = ds.Tables[0].Columns["PATIENT_ID"];
            dcPrimary[1] = ds.Tables[0].Columns["VISIT_ID"];
            dcPrimary[2] = ds.Tables[0].Columns["TIME_POINT"];
            dcPrimary[3] = ds.Tables[0].Columns["VITAL_CODE"];
            
            ds.Tables[0].PrimaryKey = dcPrimary;
            
            return ds;
        }
        
        
        /// <summary>
        /// 保存病人的护理记录
        /// </summary>
        /// <returns></returns>
        public bool SaveVitalSignsRecord(ref DataSet dsChanged, string patientId, string visitId, DateTime dtNow)
        {
            GVars.sqlceLocal.Update(ref dsChanged);
            
            return true;                        
        }
        
        
        /// <summary>
        /// 保存病人的护理记录
        /// </summary>
        /// <returns></returns>
        public bool SaveVitalSignsRecordDel(ref DataSet dsDel)
        {
            string sql = "SELECT * "
                        + " FROM VITAL_SIGNS_REC_TOMBSTONE "
                        + " WHERE (1 = 2) ";
            
            GVars.sqlceLocal.Update(ref dsDel, "VITAL_SIGNS_REC_TOMBSTONE", sql);
            return true;
        }        
    }
}
