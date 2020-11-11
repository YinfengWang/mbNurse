//------------------------------------------------------------------------------------
//
//  ϵͳ����        : ҽ������վ
//  ��ϵͳ����      : ͨ��ģ��
//  ��������        : 
//  ����            : SqlManager.cs
//  ���ܸ�Ҫ        : �����������ݿ�������
//  ������          : ����
//  ������          : 2007-01-18
//  �汾            : 1.0.0.0
// 
//------------------< �����ʷ >------------------------------------------------------
//  �������        :
//  �����          :
//  �������        :
//  �汾            :
//------------------------------------------------------------------------------------

using System;
using System.Text;
using System.Data;

namespace HISPlus
{
	/// <summary>
	/// SqlManager ��ժҪ˵����
	/// </summary>
	public class SqlManager
	{
        public struct STR
        {
            public static readonly string ORA_MATCH_ONE                 = "_";						// ͨ���(��)
		    public static readonly string ORA_MATCH_ALL					= "*";						// ͨ���(ȫ��)
            
		    public static readonly string SYSDATE_ORACLE				= "SYSDATE";                // Oracle DB�е�ϵͳ����
		    public static readonly string SYSDATE_SQL					= "GETDATE()";              // SQL DB�е�ϵͳ����
    		
		    public static readonly string ORA_DATE_FORMAT_LONG			= "YYYY-MM-DD HH24:MI:SS";  // DB�����ڸ�ʽ

		    // SQL
		    public static readonly string WHERE						    = " WHERE ";				// SQL����е�Where�ؼ���
		    public static readonly string AND						    = " AND ";					// SQL����е�AND�ؼ���
		    public static readonly string EQUAL_NULL				    = " IS NULL";				// ����NULL�ַ���
		    public static readonly string NULL						    = "NULL";					// NULL �ؼ���
        }


        #region �������Ͷ���
		/// <summary>
		/// �ֶ�����
		/// </summary>
		public enum FIELD_TYPE
		{
			STR			= 0,												// �ַ�����
            DECIMAL     = 1,                                                // ������
			DATE		= 2,                                                // ������
			SYS_DATE	= 3,                                                // ϵͳ����
			NULL		= 4                                                 // NULL
		}
		
		
		/// <summary>
		/// ���ݿ�����
		/// </summary>
		public enum DB_TYPE
		{
			DB_SQL		= 0,												// SQL ���ݿ�
			DB_ORACLE	= 1                                                 // Oracle ���ݿ� 
		}
		
		
		/// <summary>
		/// �ֶνṹ
		/// </summary>
		public struct DbField 
		{
			public string			FIELD_NAME;								// �ֶ���
			public string			FIELD_FROM;                             // Դ�ֶ�
			public string			FIELD_VALUE;                            // �ֶ�ֵ
			public DB_TYPE			DB_TYPE;								// ���ݿ�����
			public FIELD_TYPE	    FIELD_TYPE;                             // �ֶ�����
		}		
        #endregion
        
		public SqlManager()
		{
		}
		
        
        #region �ֶ����κ���
        /// <summary>
        /// SQL ���ĸ�ʽ��
        /// </summary>
        /// <remarks>
        /// 1: ������ ת��Ϊ ����������(ǰһ�������ű�ʾת��)
        /// 2: ɾ���ַ����Ľ�β�ո�
        /// 3: �ַ�ǰ�����һ��������
        /// 4: �ַ����һ����׺�ո�
        /// </remarks>
        /// <param name="strSection">ָ�����ַ���</param>
        /// <returns>ת�����ַ���</returns>
        static public string SqlConvert(string text)
        {
            text = text.Replace(ComConst.STR.SINGLE_QUOTATION, ComConst.STR.DOUBLE_QUOTATION);			// �����ű�˫����
            text = text.TrimEnd();																// ɾ���ַ���ĩβ�հ��ַ�
            text = ComConst.STR.SINGLE_QUOTATION + text + ComConst.STR.SINGLE_QUOTATION;	            // �ַ�����β�ӵ�����

            return text + ComConst.STR.BLANK;
        }
		
		
        /// <summary>
        /// ��ʽ������
        /// </summary>
        /// <param name="strDate">ָ�����ַ���</param>
        /// <returns>ת������ַ���</returns>
        static public string GetOraDbDate(string date)
        {
            if (date.Trim().Length > 0)
            {
                return "TO_DATE(" + SqlConvert(date) + ComConst.STR.COMMA + SqlConvert(SqlManager.STR.ORA_DATE_FORMAT_LONG) + ")";
            }
            else
            {
                return SqlManager.STR.NULL;
            }
        }
        
        
        /// <summary>
        /// ��ʽ������
        /// </summary>
        /// <param name="date">����</param>
        /// <returns>ת������ַ���</returns>
        static public string GetOraDbDate(DateTime date)
        {
            return "TO_DATE(" + SqlConvert(date.ToString(ComConst.FMT_DATE.LONG)) + ComConst.STR.COMMA + SqlConvert(SqlManager.STR.ORA_DATE_FORMAT_LONG) + ")";
        }


        /// <summary>
        /// ��ʽ������
        /// </summary>
        /// <param name="strDate">ָ�����ַ���</param>
        /// <returns>ת������ַ���</returns>
        static public string GetOraDbDate_Short(string date)
        {
            if (date.Trim().Length > 0)
            {
                return "TO_DATE(" + SqlConvert(date) + ComConst.STR.COMMA + SqlConvert(ComConst.FMT_DATE.SHORT) + ")";
            }
            else
            {
                return SqlManager.STR.NULL;
            }
        }
        

        /// <summary>
        /// ��ʽ������
        /// </summary>
        /// <param name="date">����</param>
        /// <returns>ת������ַ���</returns>
        static public string GetOraDbDate_Short(DateTime date)
        {
            if (date.Equals(DateTime.MinValue) == true)
            {
                return SqlManager.STR.NULL; 
            }
            else
            {
                return "TO_DATE(" + SqlConvert(date.ToString(ComConst.FMT_DATE.SHORT)) + ComConst.STR.COMMA + SqlConvert(ComConst.FMT_DATE.SHORT) + ")";
            }
        }
        
        
        /// <summary>
        /// ��ȡ����ʱ���SQL��ʾ
        /// </summary>
        /// <param name="dtSrc"></param>
        /// <returns></returns>
        public static string GetSqlDbDate(string date)
        {
            if (date.Trim().Length == 0)
            {
                return SqlManager.STR.NULL;
            }
            else
            {
                return SqlConvert(DateTime.Parse(date).ToString(ComConst.FMT_DATE.LONG));
            }
        }
        
        
        /// <summary>
        /// ��ȡ����ʱ���SQL��ʾ
        /// </summary>
        /// <param name="dtSrc"></param>
        /// <returns></returns>
        public static string GetSqlDbDate(DateTime date)
        {
            if (date.Equals(DateTime.MinValue) == true)
            {
                return SqlManager.STR.NULL; 
            }
            else
            {
                return SqlConvert(date.ToString(ComConst.FMT_DATE.LONG));
            }
        }        
        #endregion
        

        #region SQL����ֶ��б����ɺ���
        /// <summary>
        /// ��ȡ�ֶ��� ���ó� �ֶ�ֵ ���ַ���
        /// </summary>
        /// <returns>�ַ���</returns>
        static public string getFieldValuePairSet(DbField[] arrDbField, int count)
        {
            StringBuilder sql = new StringBuilder();
			
            for(int i = 0; i < count; i++)
            {
                // ��Ϊ��һ��Ԫ��ʱ�ֶ���ǰ�Ӷ���
                if(i > 0)
                {
                    sql.Append(ComConst.STR.COMMA);
                }
				
                sql.Append(arrDbField[i].FIELD_NAME + ComConst.STR.EQUAL);
				
                switch(arrDbField[i].FIELD_TYPE)
                {
                    case FIELD_TYPE.DATE:                             // �����������
                        if (arrDbField[i].FIELD_VALUE.Trim().Length > 0)
                        {
                            if (arrDbField[i].DB_TYPE == DB_TYPE.DB_ORACLE)
                            {
                                sql.Append(GetOraDbDate(arrDbField[i].FIELD_VALUE));
                            }
                            else
                            {
                                sql.Append(SqlConvert(arrDbField[i].FIELD_VALUE));
                            }
                        }
                        else
                        {
                            sql.Append(SqlManager.STR.NULL);
                        }

                        break;
					    
                    case FIELD_TYPE.DECIMAL:                         // ���������
                        if (arrDbField[i].FIELD_VALUE.Trim().Length > 0)
                        {
                            sql.Append(SqlConvert(arrDbField[i].FIELD_VALUE));
                        }
                        else
                        { 
                            sql.Append(SqlManager.STR.NULL);
                        }

                        break;

                    case FIELD_TYPE.SYS_DATE:                         // �����ϵͳ����
                        // Oracle DBϵͳ����
                        if(arrDbField[i].DB_TYPE == DB_TYPE.DB_ORACLE)
                        {
                            sql.Append(SqlManager.STR.SYSDATE_ORACLE);
                        }
                        // SQL DBϵͳ����
                        else
                        {
                            sql.Append(SqlManager.STR.SYSDATE_SQL);
                        }
						
                        break;
					
                    case FIELD_TYPE.STR:                              // ������ַ�����
                        sql.Append(SqlConvert(arrDbField[i].FIELD_VALUE));

                        break;
                    
                    case FIELD_TYPE.NULL:                             // �����NULLֵ
                        sql.Append(SqlManager.STR.NULL);
                        break;
                }
            }

            return sql.ToString();
        }


        /// <summary>
        /// ��ȡ�ֶ��� ���� �ֶ�ֵ �жϵ��ַ���
        /// </summary>
        /// <returns>�ַ���</returns>
        static public string getFieldValuePairAssert(DbField[] arrDbField, int count)
        {
            StringBuilder sql = new StringBuilder();
			
            for(int i = 0; i < count; i++)
            {
                // ��Ϊ��һ��Ԫ��ʱ�ֶ���ǰ�Ӷ���
                if(i > 0)
                {
                    sql.Append(SqlManager.STR.AND);
                }
				
                sql.Append(arrDbField[i].FIELD_NAME);
				
                switch(arrDbField[i].FIELD_TYPE)
                {
                    case FIELD_TYPE.DATE:                             // �����������
                        if (arrDbField[i].FIELD_VALUE.Trim().Length > 0)
                        {
                            if (arrDbField[i].DB_TYPE == DB_TYPE.DB_ORACLE)
                            {
                                sql.Append(ComConst.STR.EQUAL + GetOraDbDate(arrDbField[i].FIELD_VALUE));
                            }
                            else
                            {
                                sql.Append(ComConst.STR.EQUAL + SqlConvert(arrDbField[i].FIELD_VALUE));
                            }
                        }
                        else
                        { 
                            sql.Append(SqlManager.STR.EQUAL_NULL);
                        }

                        break;
					
                    case FIELD_TYPE.DECIMAL:                         // �����������
                        if (arrDbField[i].FIELD_VALUE.Trim().Length > 0)
                        {
                            sql.Append(ComConst.STR.EQUAL + SqlConvert(arrDbField[i].FIELD_VALUE));
                        }
                        else
                        { 
                            sql.Append(SqlManager.STR.EQUAL_NULL);
                        }

                        break;

                    case FIELD_TYPE.SYS_DATE:                         // �����ϵͳ����
                        if(arrDbField[i].DB_TYPE == DB_TYPE.DB_ORACLE)
                        {
                            sql.Append(ComConst.STR.EQUAL + SqlManager.STR.SYSDATE_ORACLE);
                        }
                        else
                        {
                            sql.Append(ComConst.STR.EQUAL + SqlManager.STR.SYSDATE_SQL);
                        }
						
                        break;
					
                    case FIELD_TYPE.STR:                              // ������ַ�����
                        sql.Append(ComConst.STR.EQUAL + SqlConvert(arrDbField[i].FIELD_VALUE));
                        break;
        
                    case FIELD_TYPE.NULL:                             // �����NULLֵ
                        sql.Append(SqlManager.STR.EQUAL_NULL);
                        break;
                }
            }
			
            return sql.ToString();
        }


        /// <summary>
        /// ��ȡ�ֶ��б��ַ���, �ֶ�֮���Զ��ŷָ�
        /// </summary>
        /// <returns>�ֶ��б��ַ���</returns>
        static public string getFieldValueList(DbField[] arrDbField, int count)
        {
            StringBuilder sql = new StringBuilder();

            for(int i = 0; i < count; i++)
            {
                // ��Ϊ��һ��Ԫ��ʱ�ֶ���ǰ�Ӷ���
                if(i > 0)
                {
                    sql.Append(ComConst.STR.COMMA);
                }
				
                switch(arrDbField[i].FIELD_TYPE)
                {
                    case FIELD_TYPE.DATE:							// ������
                        if (arrDbField[i].FIELD_VALUE.Trim().Length > 0)
                        {
                            if (arrDbField[i].DB_TYPE == DB_TYPE.DB_ORACLE)
                            {
                                sql.Append(GetOraDbDate(arrDbField[i].FIELD_VALUE));
                            }
                            else
                            {
                                sql.Append(SqlConvert(arrDbField[i].FIELD_VALUE));
                            }
                        }
                        else
                        {
                            sql.Append(SqlManager.STR.NULL);
                        }

                        break;
					
                    case FIELD_TYPE.DECIMAL:                         // �����������
                        if (arrDbField[i].FIELD_VALUE.Trim().Length > 0)
                        {
                            sql.Append(SqlConvert(arrDbField[i].FIELD_VALUE));
                        }
                        else
                        {
                            sql.Append(SqlManager.STR.NULL);
                        }

                        break;

                    case FIELD_TYPE.SYS_DATE:						// ϵͳ����
                        if(arrDbField[i].DB_TYPE==DB_TYPE.DB_ORACLE)
                        {
                            sql.Append(SqlManager.STR.SYSDATE_ORACLE);
                        }
                        else
                        {
                            sql.Append(SqlManager.STR.SYSDATE_SQL);
                        }

                        break;
					
                    case FIELD_TYPE.STR:								// �ַ�����
                        sql.Append(SqlConvert(arrDbField[i].FIELD_VALUE));
                        break;

                    case FIELD_TYPE.NULL:							// NULLֵ
                        sql.Append(SqlManager.STR.NULL);
                        break;
                }
            }
			
            return sql.ToString();
        }


        /// <summary>
        /// ��ȡ�ֶ��������б��ַ���, �ֶ�֮���Զ��ŷָ�
        /// </summary>
        /// <param name="arrDbField"></param>
        /// <param name="count"></param>
        /// <returns>�ֶ������б��ַ���</returns>
        static public string getFieldNameList(DbField[] arrDbField, int count)
        {
            StringBuilder sql = new StringBuilder();

            for(int i = 0; i < count; i++)
            {
                //��Ϊ��һ��Ԫ��ʱ�ֶ���ǰ�Ӷ���
                if(i > 0)
                {
                    sql.Append(ComConst.STR.COMMA);
                }
				
                sql.Append(arrDbField[i].FIELD_NAME);
            }
			
            return sql.ToString();
        }

        
        /// <summary>
        /// ��ȡ�ֶ��������б��ַ���, �ֶ�֮���Զ��ŷָ�
        /// </summary>
        /// <param name="arrDbField"></param>
        /// <param name="count"></param>
        /// <returns>�ֶ������б��ַ���</returns>
        static public string getFieldNameFromList(DbField[] arrDbField, int count)
        {
            StringBuilder sql = new StringBuilder();

            for(int i = 0; i < count; i++)
            {
                //��Ϊ��һ��Ԫ��ʱ�ֶ���ǰ�Ӷ���
                if(i > 0)
                {
                    sql.Append(ComConst.STR.COMMA);
                }
				
                sql.Append(arrDbField[i].FIELD_FROM);
            }
			
            return sql.ToString();
        }

        #endregion
        

        #region SQL�������
		/// <summary>
		/// ��ȡDbField����ֵ
		/// </summary>
		/// <param name="fieldName">�ֶ���</param>
		/// <param name="fieldFrom">Դ�ֶ�</param>
		/// <param name="fieldValue">�ֶ�ֵ</param>
		/// <param name="typeValue">���ݿ�����</param>
		/// <param name="fieldType">�ֶ�����</param>
		/// <returns>DbField����ֵ</returns>
		static public DbField GetDbField(string fieldName, string fieldFrom, string fieldValue, 
			DB_TYPE dbType, FIELD_TYPE fieldType)
		{
			DbField dbField = new DbField();
			
			dbField.FIELD_NAME	= fieldName;
			dbField.FIELD_FROM	= fieldFrom;
			dbField.FIELD_VALUE = fieldValue;
			dbField.DB_TYPE		= dbType;
			dbField.FIELD_TYPE	= fieldType;
			
			return dbField;
		}


        /// <summary>
        /// ��ȡDbField����ֵ(���Oracle���ݿ�)
        /// </summary>
        /// <param name="fieldName">�ֶ���</param>
        /// <param name="fieldValue">�ֶ�ֵ</param>
        /// <param name="fieldType">�ֶ�����</param>
        /// <returns>DbField����ֵ</returns>
        static public DbField GetDbField_Ora(string fieldName, string fieldValue, FIELD_TYPE fieldType)
        {
            DbField dbField	= new DbField();

            dbField.FIELD_NAME	= fieldName;
            dbField.FIELD_FROM	= string.Empty;
            dbField.FIELD_VALUE = fieldValue;
            dbField.DB_TYPE		= DB_TYPE.DB_ORACLE;
            dbField.FIELD_TYPE	= fieldType;

            return dbField;
        }


        /// <summary>
        /// ��ȡDbField����ֵ(���SqlServer���ݿ�)
        /// </summary>
        /// <param name="fieldName">�ֶ���</param>
        /// <param name="fieldValue">�ֶ�ֵ</param>
        /// <param name="fieldType">�ֶ�����</param>
        /// <returns>DbField����ֵ</returns>
        static public DbField GetDbField_Sql(string fieldName, string fieldValue, FIELD_TYPE fieldType)
        {
            DbField dbField	= new DbField();

            dbField.FIELD_NAME	= fieldName;
            dbField.FIELD_FROM	= string.Empty;
            dbField.FIELD_VALUE = fieldValue;
            dbField.DB_TYPE		= DB_TYPE.DB_SQL;
            dbField.FIELD_TYPE	= fieldType;

            return dbField;
        }


        /// <summary>
        /// ��ȡDbField����ֵ(���SqlServer���ݿ�)
        /// </summary>
        /// <param name="fieldName">�ֶ���</param>
        /// <param name="fieldValue">�ֶ�ֵ</param>
        /// <param name="fieldType">�ֶ�����</param>
        /// <returns>DbField����ֵ</returns>
        static public DbField GetDbField_Sql(string fieldName, string fieldValue)
        {
            DbField dbField	= new DbField();

            dbField.FIELD_NAME	= fieldName;
            dbField.FIELD_FROM	= string.Empty;
            dbField.FIELD_VALUE = fieldValue;
            dbField.DB_TYPE		= DB_TYPE.DB_SQL;
            dbField.FIELD_TYPE	= FIELD_TYPE.STR;

            return dbField;
        }


        /// <summary>
        /// ��ȡDbField����ֵ(���SqlServer���ݿ�)
        /// </summary>
        /// <param name="dc">DataColumn</param>
        /// <param name="fieldValue">�ֶ�ֵ</param>
        /// <returns>DbField����ֵ</returns>
        static public DbField GetDbField_Sql(DataColumn dc, string fieldValue)
        { 
            DbField dbField	= new DbField();

            dbField.FIELD_NAME	= dc.ColumnName;
            dbField.FIELD_FROM	= string.Empty;

            dbField.FIELD_VALUE = fieldValue;

            if (dc.DataType == System.Type.GetType("System.DateTime"))
            {
                if (fieldValue.Trim().Length > 0)
                {
                    dbField.FIELD_VALUE = DateTime.Parse(fieldValue).ToString(ComConst.FMT_DATE.LONG);
                }
            }
            
            dbField.DB_TYPE		= DB_TYPE.DB_SQL;
            dbField.FIELD_TYPE	= GetDataColumnFieldType(dc.DataType);
            
            return dbField;
        }
        

        /// <summary>
        /// ��ȡINSERT SQL���
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="arrDbField">�����ֶε�����</param>
        /// <param name="count">������ЧԪ�ظ���</param>
        /// <returns>INSERT SQL���</returns>
        static public string GetSqlInsert(string tableName, DbField[] arrDbField, int count)
        {
            string sql = string.Empty;

            sql += "INSERT INTO " + tableName + " ( " + getFieldNameList(arrDbField, count) + ") ";
            sql += "VALUES (" + getFieldValueList(arrDbField, count) + ")";
			
            return sql;
        }


        /// <summary>
        /// ��ȡDELETE SQL���
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="arrDbField">�����ֶε�����</param>
        /// <param name="count">������ЧԪ�ظ���</param>
        /// <returns>DELETE SQL���</returns>
        static public string GetSqlDelete(string tableName, DbField[] arrDbField, int count)
        {
            string sql = "DELETE FROM " + tableName;
			
            // ��������ɾ��������
            if(count > 0)
            {
                sql += SqlManager.STR.WHERE + getFieldValuePairAssert(arrDbField, count);
            }
			
            return sql;
        }
			
		
        /// <summary>
        /// ��ȡUPDATE SQL���
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="arrDbField">�����ֶε�����</param>
        /// <param name="count">������ЧԪ�ظ���</param>
        /// <param name="where">WHERE���</param>
        /// <returns>UPDATE SQL���</returns>
        static public string GetSqlUpdate(string tableName, DbField[] arrDbField, int count, string where)
        {
            return "UPDATE " + tableName + " SET " + getFieldValuePairSet(arrDbField, count) + getSQLWhere(where);
        }
		
		
        /// <summary>
        /// ��ȡINSERT FROM SQL���,
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="tableFromName">Դ��</param>
        /// <param name="arrDbField">�����ֶε�����</param>
        /// <param name="count">������ЧԪ�ظ���</param>
        /// <param name="where">Where���</param>
        /// <returns>INSERT FROM SQL���</returns>
        static public string GetSqlInsertFrom(string tableName, string tableFromName, DbField[] arrDbField,
            int count, string where) 
        {
            string strSQL = "INSERT INTO " + tableName + " ("  + getFieldNameList(arrDbField, count) + ") ";
            strSQL += "SELECT ";
			
            // ���ָ��Դ�ֶ�
            if (arrDbField[0].FIELD_FROM.Trim().Length > 0)
            {
                strSQL += getFieldNameFromList(arrDbField, count) + " FROM " + tableFromName + getSQLWhere(where); 
            }
            else
            {
                strSQL += getFieldValueList(arrDbField, count);
            }
			
            return strSQL;
        }

        #endregion
        

        #region WHERE�Ӿ䴦��
		/// <summary>
		/// ȷ��Where�������WHERE�ؼ�����ǰ���ո�
		/// </summary>
		/// <param name="where">where����ַ���</param>
		/// <returns>����ǰ���ո���WHERE�ؼ��ֵ�Where����ַ���</returns>
		static public string getSQLWhere(string where)
		{
			// WHERE ��䴦�� (ȷ����WHERE�ؼ���)
			where = where.Trim();
			
			if(where.Length > 0)
			{
				// �ж�WHERE ������Ƿ��ַ���"WHERE", ���������WHERE, �������ӿո�
				if(where.IndexOf(SqlManager.STR.WHERE.TrimStart()) == 0)
				{
					where = ComConst.STR.BLANK + where;
				}
				else
				{
					where = SqlManager.STR.WHERE + where;
				}
			}
			
			return where;
		}

        
        /// <summary>
        /// �����µ�Where����
        /// </summary>
        /// <param name="where"></param>
        /// <returns>�����������������Where���</returns>
        static public string GetConditionPre(string where)
        {
            if (where.Trim().Length == 0)
            {
                return SqlManager.STR.WHERE;
            }
            else
            {
                return SqlManager.STR.AND;
            }
        }
        #endregion


        #region ����
        /// <summary>
        /// ��ȡDataColumn.Type ��Ӧ�� FIELD_TYPE
        /// </summary>
        /// <param name="dcType">DataColumn.Type</param>
        /// <returns></returns>
        public static FIELD_TYPE GetDataColumnFieldType(Type dcType)
        { 
            if (dcType == System.Type.GetType("System.DateTime"))
            {
                return FIELD_TYPE.DATE;
            }
            else if (dcType == System.Type.GetType("System.Decimal")
                || dcType == System.Type.GetType("System.Double"))
            {
                return FIELD_TYPE.DECIMAL;
            }
            else
            {
                return FIELD_TYPE.STR;
            }
        }     
        #endregion
    }
}