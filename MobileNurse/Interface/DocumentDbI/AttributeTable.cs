using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HISPlus
{
    /// <summary>
    /// 实体表属性
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Class, Inherited = false, AllowMultiple = false), Serializable]
    public class AttributeTable : Attribute
    {
        //保存表名的字段
        private string _tableName;
        /// <summary>
        /// 映射的表名(表的全名:模式名.表名)
        /// </summary>
        public string TableName
        {
            set
            {
                this._tableName = value;
            }
            get
            {
                return this._tableName;
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public AttributeTable()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableName"></param>
        public AttributeTable(string tableName)
        {
            this._tableName = tableName;
        }


    }
}
