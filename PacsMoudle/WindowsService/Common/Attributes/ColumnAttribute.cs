#region 模块说明
/// <summary> 
/// 模块编号： Common.Attributes.ColumnAttribute
/// 作    用： 定义列属性，用来标识实体中属性对应表中的列
/// 作    者： 陆峰
/// 编写日期： 2011-09-02
/// </summary>
/*----------------------------------------------------------*/
/// <summary> 
/// Log编 号： <Log编号,从1开始一次增加> 
/// 修改描述： <对此修改的描述> 
/// 作    者： 
/// 修改日期：  
/// </summary>
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false), Serializable]
    public class ColumnAttribute : Attribute
    {
        private string _columnName;
        private bool _isPrimary;
        /// <summary>
        /// 主键
        /// </summary>
        public static bool IsPrimary=false;
        /// <summary>
        /// 列名
        /// </summary>
        public virtual string ColumnName
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

        /// <summary>
        /// 是否为主键
        /// </summary>
        public virtual bool IsPrimaryKey
        {
            set
            {
                this._isPrimary = value;
            }
            get
            {
                return this._isPrimary;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ColumnAttribute()
        {

        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="isPrimary">是否为主键</param>
        public ColumnAttribute(string columnName)
            : this()
        {
            this._columnName = columnName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="isPrimary">是否为主键</param>
        public ColumnAttribute(string columnName, bool isPrimary)
            : this()
        {
            this._columnName = columnName;
            this._isPrimary = isPrimary;
        }
    }
}
