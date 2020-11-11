using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

using SqlManager = HISPlus.SqlManager;

namespace HISPlus
{    
    class BodyTemperatureCom
    {        
        public static readonly string T_VITAL_SIGNS_REC         = "VITAL_SIGNS_REC";                // 表: 生命体征记录
        public static readonly string T_OPERATION_REC           = "OPERATION";                      // 表: 手术记录
        public static readonly string T_NURSING_CLASS_DICT      = "NURSE_TEMPERATURE_CLASS_DICT";   // 表: 护理类型字典 
        public static readonly string T_NURSING_ITEM            = "NURSE_ITEM_DICT";                // 表: 护理项目字典
                
        public DataSet          DsData  = new DataSet();                // 数据
        
        /// <summary>
        /// 获取生命体温记录
        /// </summary>
        /// <param name="patientId">病人标识号</param>
        /// <param name="visitId">本次就诊序号</param>
        /// <returns>TRUE: 成功; FALSE: 失败</returns>
        public bool GetVitalSignsRec(string patientId, string visitId)
        {
            // 获取护理事件
            string sql = string.Empty;
            
            sql = "SELECT * FROM VITAL_SIGNS_REC ";
            sql += "WHERE PATIENT_ID = " + SqlManager.SqlConvert(patientId);
            sql += "AND VISIT_ID = " + SqlManager.SqlConvert(visitId);
            
            GVars.OracleAccess.SelectData(sql, T_VITAL_SIGNS_REC, ref DsData);
            
            // 获取转科记录
            getTransferRec(patientId, visitId);
            
            return true;
        }
        
        
        /// <summary>
        /// 获取手术记录
        /// </summary>
        /// <param name="patientId">病人标识号</param>
        /// <param name="visitId">本次就诊序号</param>
        /// <returns>TRUE: 成功; FALSE: 失败</returns>
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
        /// 获取病人转科记录
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>        
        private bool getTransferRec(string patientId, string visitId)
        {
            string TRANS_DEPT = "转科";
            
            // 获取转科记录
            string sql = string.Empty;
            
            sql += "SELECT ";
            sql +=     "TRANSFER.ADMISSION_DATE_TIME, ";
            sql +=     "(SELECT DEPT_DICT.DEPT_NAME FROM DEPT_DICT WHERE DEPT_DICT.DEPT_CODE = TRANSFER.DEPT_STAYED) DEPT_IN, ";
            sql +=     "TRANSFER.DISCHARGE_DATE_TIME, ";
            sql +=     "(SELECT DEPT_DICT.DEPT_NAME FROM DEPT_DICT WHERE DEPT_DICT.DEPT_CODE = TRANSFER.DEPT_TRANSFERED_TO) DEPT_OUT, ";
            sql +=     "TRANSFER.DOCTOR_IN_CHARGE ";
            sql += "FROM TRANSFER, ";
            sql +=     "PATS_IN_HOSPITAL ";
            sql += "WHERE TRANSFER.PATIENT_ID = " + SqlManager.SqlConvert(patientId);
            sql +=     "AND TRANSFER.VISIT_ID = " + SqlManager.SqlConvert(visitId);
            sql +=     "AND TRANSFER.PATIENT_ID = PATS_IN_HOSPITAL.PATIENT_ID ";
            sql +=     "AND TRANSFER.VISIT_ID = PATS_IN_HOSPITAL.VISIT_ID ";
            sql +=     "AND TRANSFER.DEPT_TRANSFERED_TO IS NOT NULL ";
            
            DataSet dsTransfer = GVars.OracleAccess.SelectData(sql);
            
            // 把转科事件保存到护理事件中
            // 获取转科事件的代码
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
            
            // 删除原来的转科事件
            DataRow[] drFind = DsData.Tables[T_VITAL_SIGNS_REC].Select("VITAL_SIGNS = " + SqlManager.SqlConvert(TRANS_DEPT));
            for(int i = drFind.Length - 1; i >= 0; i--)
            {
                drFind[i].Delete();
            }
            
            DsData.Tables[T_VITAL_SIGNS_REC].AcceptChanges();
            
            // 增加新的转科事件
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
                drNew["VITAL_SIGNS_CVALUES"]= dr["DEPT_OUT"].ToString();
                drNew["WARD_CODE"]          = GVars.User.DeptCode;
                drNew["NURSE"]              = dr["DOCTOR_IN_CHARGE"].ToString();
                
                DsData.Tables[T_VITAL_SIGNS_REC].Rows.Add(drNew);
            }
            
            return true;
        }
        
        
        /// <summary>
		/// 获取护理类型
		/// </summary>
		/// <returns>TRUE: 成功; FALSE: 失败</returns>
		public bool GetNursingItemClass()
		{
			string sql = "SELECT * FROM NURSE_TEMPERATURE_CLASS_DICT";
			
			GVars.OracleAccess.SelectData(sql, T_NURSING_CLASS_DICT, ref DsData);
			return true;
		}
		
		
		/// <summary>
		/// 获取护理项目字典
		/// </summary>
		/// <returns></returns>
		public bool GetNursingItem()
		{
		    string sql = "SELECT * FROM NURSE_TEMPERATURE_ITEM_DICT WHERE WARD_CODE = " + SqlManager.SqlConvert(GVars.User.DeptCode);
			
			GVars.SqlserverAccess.SelectData(sql, T_NURSING_ITEM, ref DsData);
			return true;
		}
		
		
		/// <summary>
		/// 获取系统日期
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
		        throw new Exception("获取系统日期错误!");
		    }
		}
		
        
        /// <summary>
        /// 数据后处理 (对护理数据表增加一个属性列, 并对它进行赋值)
        /// </summary>
        /// <returns></returns>
        public bool ReTreatNursingRec()
        {            
            // 创建新列
            string colAttribute = "ATTRIBUTE";
            
            DataTable dtDest = DsData.Tables[T_VITAL_SIGNS_REC];
            if (dtDest.Columns.IndexOf(colAttribute) < 0)
            {
                dtDest.Columns.Add(colAttribute, Type.GetType("System.String"));
            }
            
            // 为该新列赋值
            DataTable dtDict = DsData.Tables[T_NURSING_ITEM];
            
            string vitalCode = string.Empty;
            string attribute = string.Empty;
            DataRow[] drData = DsData.Tables[T_VITAL_SIGNS_REC].Select(string.Empty, "VITAL_CODE");
            DataRow[] drFind = null;
            
            for(int i = 0; i < drData.Length; i++)
            {
                // 获取属性值
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
                
                // 设置属性值
                drData[i][colAttribute] = attribute;
            }
            
            return true;            
        }        
        
        
        /// <summary>
        /// 获取体温单附加的项目
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
