//------------------------------------------------------------------------------------
//
//  系统名称        : 医生工作站
//  子系统名称      : 通用模块
//  对象类型        : 
//  类名            : SqlManager.cs
//  功能概要        : 用于描述数据库语句的类
//  作成者          : 付军
//  作成日          : 2007-01-18
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        :
//  变更者          :
//  变更内容        :
//  版本            :
//------------------------------------------------------------------------------------

using System;
using System.Text;
using System.Data;

namespace HISPlus
{
	/// <summary>
	/// SqlManager 的摘要说明。
	/// </summary>
	public class SqlManager
	{
        public struct STR
        {
            public static readonly string ORA_MATCH_ONE                 = "_";						// 通配符(单)
		    public static readonly string ORA_MATCH_ALL					= "*";						// 通配符(全部)
            
		    public static readonly string SYSDATE_ORACLE				= "SYSDATE";                // Oracle DB中的系统日期
		    public static readonly string SYSDATE_SQL					= "GETDATE()";              // SQL DB中的系统日期
    		
		    public static readonly string ORA_DATE_FORMAT_LONG			= "YYYY-MM-DD HH24:MI:SS";  // DB中日期格式

		    // SQL
		    public static readonly string WHERE						    = " WHERE ";				// SQL语句中的Where关键字
		    public static readonly string AND						    = " AND ";					// SQL语句中的AND关键字
		    public static readonly string EQUAL_NULL				    = " IS NULL";				// 等于NULL字符串
		    public static readonly string NULL						    = "NULL";					// NULL 关键字
        }


        #region 数据类型定义
		/// <summary>
		/// 字段类型
		/// </summary>
		public enum FIELD_TYPE
		{
			STR			= 0,												// 字符串型
            DECIMAL     = 1,                                                // 数字型
			DATE		= 2,                                                // 日期型
			SYS_DATE	= 3,                                                // 系统日期
			NULL		= 4                                                 // NULL
		}
		
		
		/// <summary>
		/// 数据库类型
		/// </summary>
		public enum DB_TYPE
		{
			DB_SQL		= 0,												// SQL 数据库
			DB_ORACLE	= 1                                                 // Oracle 数据库 
		}
		
		
		/// <summary>
		/// 字段结构
		/// </summary>
		public struct DbField 
		{
			public string			FIELD_NAME;								// 字段名
			public string			FIELD_FROM;                             // 源字段
			public string			FIELD_VALUE;                            // 字段值
			public DB_TYPE			DB_TYPE;								// 数据库类型
			public FIELD_TYPE	    FIELD_TYPE;                             // 字段类型
		}		
        #endregion
        
		public SqlManager()
		{
		}
		
        
        #region 字段修饰函数
        /// <summary>
        /// SQL 语句的格式化
        /// </summary>
        /// <remarks>
        /// 1: 单引号 转换为 两个单引号(前一个单引号表示转义)
        /// 2: 删除字符串的结尾空格
        /// 3: 字符前后各加一个单引号
        /// 4: 字符后加一个后缀空格
        /// </remarks>
        /// <param name="strSection">指定的字符串</param>
        /// <returns>转换的字符串</returns>
        static public string SqlConvert(string text)
        {
            text = text.Replace(ComConst.STR.SINGLE_QUOTATION, ComConst.STR.DOUBLE_QUOTATION);			// 单引号变双引号
            text = text.TrimEnd();																// 删除字符串末尾空白字符
            text = ComConst.STR.SINGLE_QUOTATION + text + ComConst.STR.SINGLE_QUOTATION;	            // 字符串首尾加单引号

            return text + ComConst.STR.BLANK;
        }
		
		
        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="strDate">指定的字符串</param>
        /// <returns>转换后的字符串</returns>
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
        /// 格式化日期
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>转换后的字符串</returns>
        static public string GetOraDbDate(DateTime date)
        {
            return "TO_DATE(" + SqlConvert(date.ToString(ComConst.FMT_DATE.LONG)) + ComConst.STR.COMMA + SqlConvert(SqlManager.STR.ORA_DATE_FORMAT_LONG) + ")";
        }


        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="strDate">指定的字符串</param>
        /// <returns>转换后的字符串</returns>
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
        /// 格式化日期
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>转换后的字符串</returns>
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
        /// 获取日期时间的SQL表示
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
        /// 获取日期时间的SQL表示
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
        

        #region SQL语句字段列表生成函数
        /// <summary>
        /// 获取字段名 设置成 字段值 的字符串
        /// </summary>
        /// <returns>字符串</returns>
        static public string getFieldValuePairSet(DbField[] arrDbField, int count)
        {
            StringBuilder sql = new StringBuilder();
			
            for(int i = 0; i < count; i++)
            {
                // 不为第一个元素时字段名前加逗号
                if(i > 0)
                {
                    sql.Append(ComConst.STR.COMMA);
                }
				
                sql.Append(arrDbField[i].FIELD_NAME + ComConst.STR.EQUAL);
				
                switch(arrDbField[i].FIELD_TYPE)
                {
                    case FIELD_TYPE.DATE:                             // 如果是日期型
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
					    
                    case FIELD_TYPE.DECIMAL:                         // 如果是数字
                        if (arrDbField[i].FIELD_VALUE.Trim().Length > 0)
                        {
                            sql.Append(SqlConvert(arrDbField[i].FIELD_VALUE));
                        }
                        else
                        { 
                            sql.Append(SqlManager.STR.NULL);
                        }

                        break;

                    case FIELD_TYPE.SYS_DATE:                         // 如果是系统日期
                        // Oracle DB系统日期
                        if(arrDbField[i].DB_TYPE == DB_TYPE.DB_ORACLE)
                        {
                            sql.Append(SqlManager.STR.SYSDATE_ORACLE);
                        }
                        // SQL DB系统日期
                        else
                        {
                            sql.Append(SqlManager.STR.SYSDATE_SQL);
                        }
						
                        break;
					
                    case FIELD_TYPE.STR:                              // 如果是字符串型
                        sql.Append(SqlConvert(arrDbField[i].FIELD_VALUE));

                        break;
                    
                    case FIELD_TYPE.NULL:                             // 如果是NULL值
                        sql.Append(SqlManager.STR.NULL);
                        break;
                }
            }

            return sql.ToString();
        }


        /// <summary>
        /// 获取字段名 等于 字段值 判断的字符串
        /// </summary>
        /// <returns>字符串</returns>
        static public string getFieldValuePairAssert(DbField[] arrDbField, int count)
        {
            StringBuilder sql = new StringBuilder();
			
            for(int i = 0; i < count; i++)
            {
                // 不为第一个元素时字段名前加逗号
                if(i > 0)
                {
                    sql.Append(SqlManager.STR.AND);
                }
				
                sql.Append(arrDbField[i].FIELD_NAME);
				
                switch(arrDbField[i].FIELD_TYPE)
                {
                    case FIELD_TYPE.DATE:                             // 如果是日期型
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
					
                    case FIELD_TYPE.DECIMAL:                         // 如果是数字型
                        if (arrDbField[i].FIELD_VALUE.Trim().Length > 0)
                        {
                            sql.Append(ComConst.STR.EQUAL + SqlConvert(arrDbField[i].FIELD_VALUE));
                        }
                        else
                        { 
                            sql.Append(SqlManager.STR.EQUAL_NULL);
                        }

                        break;

                    case FIELD_TYPE.SYS_DATE:                         // 如果是系统日期
                        if(arrDbField[i].DB_TYPE == DB_TYPE.DB_ORACLE)
                        {
                            sql.Append(ComConst.STR.EQUAL + SqlManager.STR.SYSDATE_ORACLE);
                        }
                        else
                        {
                            sql.Append(ComConst.STR.EQUAL + SqlManager.STR.SYSDATE_SQL);
                        }
						
                        break;
					
                    case FIELD_TYPE.STR:                              // 如果是字符串型
                        sql.Append(ComConst.STR.EQUAL + SqlConvert(arrDbField[i].FIELD_VALUE));
                        break;
        
                    case FIELD_TYPE.NULL:                             // 如果是NULL值
                        sql.Append(SqlManager.STR.EQUAL_NULL);
                        break;
                }
            }
			
            return sql.ToString();
        }


        /// <summary>
        /// 获取字段列表字符串, 字段之间以逗号分隔
        /// </summary>
        /// <returns>字段列表字符串</returns>
        static public string getFieldValueList(DbField[] arrDbField, int count)
        {
            StringBuilder sql = new StringBuilder();

            for(int i = 0; i < count; i++)
            {
                // 不为第一个元素时字段名前加逗号
                if(i > 0)
                {
                    sql.Append(ComConst.STR.COMMA);
                }
				
                switch(arrDbField[i].FIELD_TYPE)
                {
                    case FIELD_TYPE.DATE:							// 日期型
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
					
                    case FIELD_TYPE.DECIMAL:                         // 如果是数字型
                        if (arrDbField[i].FIELD_VALUE.Trim().Length > 0)
                        {
                            sql.Append(SqlConvert(arrDbField[i].FIELD_VALUE));
                        }
                        else
                        {
                            sql.Append(SqlManager.STR.NULL);
                        }

                        break;

                    case FIELD_TYPE.SYS_DATE:						// 系统日期
                        if(arrDbField[i].DB_TYPE==DB_TYPE.DB_ORACLE)
                        {
                            sql.Append(SqlManager.STR.SYSDATE_ORACLE);
                        }
                        else
                        {
                            sql.Append(SqlManager.STR.SYSDATE_SQL);
                        }

                        break;
					
                    case FIELD_TYPE.STR:								// 字符串型
                        sql.Append(SqlConvert(arrDbField[i].FIELD_VALUE));
                        break;

                    case FIELD_TYPE.NULL:							// NULL值
                        sql.Append(SqlManager.STR.NULL);
                        break;
                }
            }
			
            return sql.ToString();
        }


        /// <summary>
        /// 获取字段名名称列表字符串, 字段之间以逗号分隔
        /// </summary>
        /// <param name="arrDbField"></param>
        /// <param name="count"></param>
        /// <returns>字段名称列表字符串</returns>
        static public string getFieldNameList(DbField[] arrDbField, int count)
        {
            StringBuilder sql = new StringBuilder();

            for(int i = 0; i < count; i++)
            {
                //不为第一个元素时字段名前加逗号
                if(i > 0)
                {
                    sql.Append(ComConst.STR.COMMA);
                }
				
                sql.Append(arrDbField[i].FIELD_NAME);
            }
			
            return sql.ToString();
        }

        
        /// <summary>
        /// 获取字段名名称列表字符串, 字段之间以逗号分隔
        /// </summary>
        /// <param name="arrDbField"></param>
        /// <param name="count"></param>
        /// <returns>字段名称列表字符串</returns>
        static public string getFieldNameFromList(DbField[] arrDbField, int count)
        {
            StringBuilder sql = new StringBuilder();

            for(int i = 0; i < count; i++)
            {
                //不为第一个元素时字段名前加逗号
                if(i > 0)
                {
                    sql.Append(ComConst.STR.COMMA);
                }
				
                sql.Append(arrDbField[i].FIELD_FROM);
            }
			
            return sql.ToString();
        }

        #endregion
        

        #region SQL语句生成
		/// <summary>
		/// 获取DbField类型值
		/// </summary>
		/// <param name="fieldName">字段名</param>
		/// <param name="fieldFrom">源字段</param>
		/// <param name="fieldValue">字段值</param>
		/// <param name="typeValue">数据库类型</param>
		/// <param name="fieldType">字段类型</param>
		/// <returns>DbField类型值</returns>
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
        /// 获取DbField类型值(针对Oracle数据库)
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="fieldType">字段类型</param>
        /// <returns>DbField类型值</returns>
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
        /// 获取DbField类型值(针对SqlServer数据库)
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="fieldType">字段类型</param>
        /// <returns>DbField类型值</returns>
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
        /// 获取DbField类型值(针对SqlServer数据库)
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="fieldType">字段类型</param>
        /// <returns>DbField类型值</returns>
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
        /// 获取DbField类型值(针对SqlServer数据库)
        /// </summary>
        /// <param name="dc">DataColumn</param>
        /// <param name="fieldValue">字段值</param>
        /// <returns>DbField类型值</returns>
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
        /// 获取INSERT SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="arrDbField">表征字段的数组</param>
        /// <param name="count">数组有效元素个数</param>
        /// <returns>INSERT SQL语句</returns>
        static public string GetSqlInsert(string tableName, DbField[] arrDbField, int count)
        {
            string sql = string.Empty;

            sql += "INSERT INTO " + tableName + " ( " + getFieldNameList(arrDbField, count) + ") ";
            sql += "VALUES (" + getFieldValueList(arrDbField, count) + ")";
			
            return sql;
        }


        /// <summary>
        /// 获取DELETE SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="arrDbField">表征字段的数组</param>
        /// <param name="count">数组有效元素个数</param>
        /// <returns>DELETE SQL语句</returns>
        static public string GetSqlDelete(string tableName, DbField[] arrDbField, int count)
        {
            string sql = "DELETE FROM " + tableName;
			
            // 有条件的删除表的语句
            if(count > 0)
            {
                sql += SqlManager.STR.WHERE + getFieldValuePairAssert(arrDbField, count);
            }
			
            return sql;
        }
			
		
        /// <summary>
        /// 获取UPDATE SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="arrDbField">表征字段的数组</param>
        /// <param name="count">数组有效元素个数</param>
        /// <param name="where">WHERE语句</param>
        /// <returns>UPDATE SQL语句</returns>
        static public string GetSqlUpdate(string tableName, DbField[] arrDbField, int count, string where)
        {
            return "UPDATE " + tableName + " SET " + getFieldValuePairSet(arrDbField, count) + getSQLWhere(where);
        }
		
		
        /// <summary>
        /// 获取INSERT FROM SQL语句,
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="tableFromName">源表</param>
        /// <param name="arrDbField">表征字段的数组</param>
        /// <param name="count">数组有效元素个数</param>
        /// <param name="where">Where语句</param>
        /// <returns>INSERT FROM SQL语句</returns>
        static public string GetSqlInsertFrom(string tableName, string tableFromName, DbField[] arrDbField,
            int count, string where) 
        {
            string strSQL = "INSERT INTO " + tableName + " ("  + getFieldNameList(arrDbField, count) + ") ";
            strSQL += "SELECT ";
			
            // 如果指定源字段
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
        

        #region WHERE子句处理
		/// <summary>
		/// 确定Where语句中有WHERE关键字与前导空格
		/// </summary>
		/// <param name="where">where语句字符串</param>
		/// <returns>包含前导空格与WHERE关键字的Where语句字符串</returns>
		static public string getSQLWhere(string where)
		{
			// WHERE 语句处理 (确保有WHERE关键字)
			where = where.Trim();
			
			if(where.Length > 0)
			{
				// 判断WHERE 语句中是否含字符串"WHERE", 不含则添加WHERE, 含则增加空格
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
        /// 增加新的Where条件
        /// </summary>
        /// <param name="where"></param>
        /// <returns>返回增加新条件后的Where语句</returns>
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


        #region 其它
        /// <summary>
        /// 获取DataColumn.Type 对应的 FIELD_TYPE
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