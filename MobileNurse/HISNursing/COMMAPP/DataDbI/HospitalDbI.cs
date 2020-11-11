using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public class HospitalDbI
    {
        #region ����
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
        /// ��ȡ����Ԫ�б�
        /// </summary>
        /// <returns></returns>
        public DataSet Get_WardList_Nurse()
        { 
            // ����Ԫ
            string sql = "SELECT * FROM DEPT_DICT WHERE CLINIC_ATTR = '2'";
            return _connection.SelectData(sql, "DEPT_DICT");
        }


        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="deptCode">���Ҵ���</param>
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
        /// ��ȡ���Ҵ���
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
    }
}
