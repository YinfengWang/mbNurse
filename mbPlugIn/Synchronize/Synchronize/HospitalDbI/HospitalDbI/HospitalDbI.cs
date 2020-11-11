using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public class HospitalDbI
    {
        #region 变量
        protected DbAccess _connection;
        #endregion

        public HospitalDbI(DbAccess conn)
        {
            _connection = conn;
        }


        public string GetHospitalName()
        {
            string sql = "SELECT HOSPITAL FROM HOSPITAL_CONFIG";

            if (_connection.SelectValue(sql) == true)
            {
                return _connection.GetResult(0);
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 获取护理单元列表
        /// </summary>
        /// <returns></returns>
        public DataSet Get_WardList_Nurse()
        { 
            // 护理单元
            string sql = "SELECT * FROM DEPT_DICT WHERE CLINIC_ATTR IN ('0', '2') AND OUTP_OR_INP = '1'";
            return _connection.SelectData(sql, "DEPT_DICT");
        }


        /// <summary>
        /// 获取科室名称
        /// </summary>
        /// <param name="deptCode">科室代码</param>
        /// <returns></returns>
        public string Get_DeptName(string deptCode)
        {
            deptCode = deptCode.Trim();

            string sql = "SELECT DEPT_NAME FROM DEPT_DICT WHERE DEPT_CODE = " + SQL.SqlConvert(deptCode);

            if (_connection.SelectValue(sql) == true)
            {
                return _connection.GetResult(0);
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 获取科室代码
        /// </summary>
        /// <param name="deptName"></param>
        /// <returns></returns>
        public string Get_DeptCode(string deptName)
        { 
            deptName = deptName.Trim();

            string sql = "SELECT DEPT_CODE FROM DEPT_DICT WHERE DEPT_NAME = " + SQL.SqlConvert(deptName);

            if (_connection.SelectValue(sql) == true)
            {
                return _connection.GetResult(0);
            }
            else
            {
                return string.Empty;
            }
        }

		
		/// <summary>
		/// 获取某一护理单元的用户列表
		/// </summary>
		/// <param name="nursingUnitCode"></param>
		/// <returns></returns>
		public DataSet GetNursesInDept(string deptCode)
		{
		    string sql = string.Empty;
		    
		    sql += "SELECT * ";
            sql += "FROM STAFF_DICT ";                                                          // 用户记录
            
            if (deptCode.Trim().Length > 0)
            {
                sql += "WHERE DEPT_CODE = " + SqlManager.SqlConvert(deptCode);                  // 用户单位
            }
            
            return _connection.SelectData(sql, "USER_DICT");
		}
    }
}
