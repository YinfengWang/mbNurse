#region 模块说明
/// <summary> 
/// 模块编号： Common.Attributes.TableAttribute
/// 作    用： 定义表属性，用来标识实体中属性对应的表
/// 作    者： 陆峰
/// 编写日期： 2011-10-20
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
    [AttributeUsageAttribute(AttributeTargets.Class, Inherited = false, AllowMultiple = false), Serializable]
    public class TableAttribute:Attribute
    {
        //保存表名的字段
        private string _tableName;
        /// <summary>
        /// 构造函数
        /// </summary>
        public TableAttribute()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableName"></param>
        public TableAttribute(string tableName)
        {
            this._tableName = tableName;
        }

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
    }
}
