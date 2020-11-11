using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HISPlus
{
    /// <summary>
    /// 实体列属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false), Serializable]
    public class AttributeColumn : Attribute
    {

        #region 构造函数
        public AttributeColumn()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="isPrimary">是否为主键</param>
        public AttributeColumn(string columnName)
            : this()
        {
            this._columnName = columnName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="isCanNullOrEmpty">是否可为空</param> 
        /// <param name="maxLength">最大长度(仅用于字符串)</param>
        public AttributeColumn(string columnName, bool isCanNullOrEmpty, int maxLength)
            : this()
        {
            this._columnName = columnName;
            this._isCanNullOrEmpty = isCanNullOrEmpty;
            this._maxLength = maxLength;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isTemp">是否为临时列</param>     
        /// <param name="columnName">列名</param>
        public AttributeColumn(bool isTemp, string columnName)
            : this()
        {
            this._isTemp = isTemp;
            this._columnName = columnName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="isCanNullOrEmpty">是否可为空</param>
        /// <param name="isPrimaryKey">是否为主键</param>     
        public AttributeColumn(string columnName, bool isCanNullOrEmpty, bool isPrimaryKey)
            : this()
        {
            this._columnName = columnName;
            this._isPrimaryKey = isPrimaryKey;
            this._isCanNullOrEmpty = isCanNullOrEmpty;
        }

        // ColumnName = "GERM_CODE",ColType = "VARCHAR2",CanNullOrEmpty = false,MaxLength= 10,IsPrimaryKey = true)]
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="isCanNullOrEmpty">是否可为空</param>
        /// <param name="isPrimaryKey">是否为主键</param>     
        public AttributeColumn(string columnName, string colType, bool isCanNullOrEmpty, bool isPrimaryKey)
            : this()
        {
            this._columnName = columnName;
            this._isPrimaryKey = isPrimaryKey;
            this._colType = colType;
            this._isCanNullOrEmpty = isCanNullOrEmpty;
        }
        #endregion

        #region 自定义特性
        private string _columnName;
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName
        {
            set
            {
                this._columnName = value;
            }
            get
            {
                return this._columnName;
            }
        }

        private bool _isPrimaryKey = false;
        /// <summary>
        /// 是否为主键
        /// </summary>
        public bool IsPrimaryKey
        {
            set
            {
                this._isPrimaryKey = value;
            }
            get
            {
                return this._isPrimaryKey;
            }
        }

        private bool _isCanNullOrEmpty = true;
        /// <summary>
        /// 是否可为Null
        /// </summary>
        public bool CanNullOrEmpty
        {
            get { return _isCanNullOrEmpty; }
            set { _isCanNullOrEmpty = value; }
        }

        private bool _isTemp = false;
        /// <summary>
        /// 是否为临时列(只进行转换操作,不保存到数据库)
        /// </summary>
        public bool IsTemp
        {
            get { return _isTemp; }
            set { _isTemp = value; }
        }

        private int _maxLength = 0;
        /// <summary>
        /// 最大长度(仅用于字符串)
        /// </summary>
        public int MaxLength
        {
            get { return _maxLength; }
            set { _maxLength = value; }
        }

        private string _colType;
        /// <summary>
        /// 列类型
        /// </summary>
        public string ColType
        {
            get { return _colType; }
            set { _colType = value; }
        }
        #endregion

    }
}
