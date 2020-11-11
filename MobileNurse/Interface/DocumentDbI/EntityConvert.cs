using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.ComponentModel;
using System.Collections;
using System.Text.RegularExpressions;
using HISPlus;

namespace HISPlus
{
    /// <summary>
    /// 实体单元，主要包括DataTable和List..T的互转
    /// </summary>
    public class EntityConvert
    {
        /// <summary>
        /// 将DataTable转换为List..T
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="dt">表名</param>
        /// <returns>实体列表</returns>
        public static List<T> ConvertToList<T>(DataTable dt)
        {
            if (dt == null)
                return null;
            List<T> list = new List<T>();
            //定义属性集
            List<PropertyInfo> properies = null;
            string strColumnName = string.Empty;

            foreach (DataRow dr in dt.Rows)
            {
                //使用default关键字，T为空时返回：
                //1、如果T为引用类型，则t=null
                //2、如果T为值类型，则t=0
                //3、如果T为结构类型，则返回所有成员为0或空的结构
                T t = Activator.CreateInstance<T>();
                //取得类中的所有属性
                properies = t.GetType().GetProperties().ToList();

                foreach (DataColumn dc in dt.Columns)
                {
                    string columnName = Regex.Replace(dc.ColumnName.ToLower(), @"(^[a-zA-Z0-9])|([_](\w))",
                                      delegate(Match m) { return m.Value.ToUpper().Replace("_", ""); });

                    PropertyInfo pro = properies.Find(p => p.Name.ToUpper() == columnName.ToUpper());

                    if (pro != null && !(dr[dc.ColumnName] is DBNull))
                        if (pro.PropertyType == typeof(Boolean))
                        {
                            bool b = false;
                            string s = dr[dc.ColumnName].ToString();
                            Boolean.TryParse(dr[dc.ColumnName].ToString(), out b);
                            if (!b && s.Length == 1 && (s.ToUpper() == "Y" || s == "1"))
                                b = true;
                            pro.SetValue(t, b, null);
                        }
                        else
                            pro.SetValue(t, dr[dc.ColumnName], null);

                    //PropertyInfo pro = properies.Find(p => p.Name == dc.ColumnName);

                    //if (pro != null && !(dr[dc.ColumnName] is DBNull))
                    //    pro.SetValue(t, dr[dc.ColumnName], null);                    
                }
                //foreach (PropertyInfo pro in properies)
                //{
                //    object[] objs = pro.GetCustomAttributes(false);
                //    if (objs.Length > 0)
                //        //取得实体属性对应的列名
                //        foreach (object temp in objs)
                //        {
                //            if (temp is AttributeColumn)
                //            {
                //                strColumnName = (temp as AttributeColumn).ColumnName;
                //                //赋值到实体中的属性
                //                if (!dt.Columns.Contains(strColumnName) || dr[strColumnName] == null || dr[strColumnName] == DBNull.Value)
                //                {
                //                    continue;
                //                }
                //                pro.SetValue(t, dr[strColumnName], null);
                //            }
                //        }
                //    else
                //    {
                //        if (dt.Columns.Contains(pro.Name))
                //        {
                //            object obj = dr[pro.Name];
                //            if (obj == DBNull.Value)
                //            {
                //                Type type = pro.PropertyType;
                //                obj = type.IsValueType ? Activator.CreateInstance(type) : null;
                //            }
                //            pro.SetValue(t, obj, null);
                //        }
                //        else
                //        {
                //            Type type = pro.PropertyType;
                //            pro.SetValue(t, type.IsValueType ? Activator.CreateInstance(type) : null, null);
                //        }
                //    }
                //}
                list.Add(t);
            }

            return list;
        }

        /// <summary>
        /// 将DataTable转换为List..T （列名转换：首字母大写，下划线后首字母大写）
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="dt">表名</param>
        /// <returns>实体列表</returns>
        public static List<T> ConvertToListAsColumn<T>(DataTable dt)
        {
            if (dt == null)
                return null;
            List<T> list = new List<T>();
            //使用default关键字，T为空时返回：
            //1、如果T为引用类型，则t=null
            //2、如果T为值类型，则t=0
            //3、如果T为结构类型，则返回所有成员为0或空的结构
            T t = Activator.CreateInstance<T>();

            //定义属性集
            List<PropertyInfo> properies = null;
            string strColumnName = string.Empty;

            foreach (DataRow dr in dt.Rows)
            {
                //取得类中的所有属性
                properies = t.GetType().GetProperties().ToList();

                foreach (DataColumn dc in dt.Columns)
                {
                    string columnName = Regex.Replace(dc.ColumnName.ToLower(), @"(^[a-zA-Z0-9])|([_](\w))",
                        delegate(Match m) { return m.Value.ToUpper().Replace("_", ""); });

                    PropertyInfo pro = properies.Find(p => p.Name == dc.ColumnName);

                    if (pro != null && !(dr[dc.ColumnName] is DBNull))
                        if (pro.PropertyType == typeof(Boolean))
                        {
                            bool b = false;
                            string s = dr[dc.ColumnName].ToString();
                            Boolean.TryParse(dr[dc.ColumnName].ToString(), out b);
                            if (!b && s.Length == 1 && (s.ToUpper() == "Y" || s == "1"))
                                b = true;
                            pro.SetValue(t, b, null);
                        }
                        else
                            pro.SetValue(t, dr[dc.ColumnName], null);
                }
                //foreach (PropertyInfo pro in properies)
                //{
                //    object[] objs = pro.GetCustomAttributes(false);
                //    if (objs.Length > 0)
                //        //取得实体属性对应的列名
                //        foreach (object temp in objs)
                //        {
                //            if (temp is AttributeColumn)
                //            {
                //                strColumnName = (temp as AttributeColumn).ColumnName;
                //                //赋值到实体中的属性
                //                if (!dt.Columns.Contains(strColumnName) || dr[strColumnName] == null || dr[strColumnName] == DBNull.Value)
                //                {
                //                    continue;
                //                }
                //                pro.SetValue(t, dr[strColumnName], null);
                //            }
                //        }
                //    else
                //    {
                //        if (dt.Columns.Contains(pro.Name))
                //        {
                //            object obj = dr[pro.Name];
                //            if (obj == DBNull.Value)
                //            {
                //                Type type = pro.PropertyType;
                //                obj = type.IsValueType ? Activator.CreateInstance(type) : null;
                //            }
                //            pro.SetValue(t, obj, null);
                //        }
                //        else
                //        {
                //            Type type = pro.PropertyType;
                //            pro.SetValue(t, type.IsValueType ? Activator.CreateInstance(type) : null, null);
                //        }
                //    }
                //}
                list.Add(t);
            }

            return list;
        }

        /// <summary>
        /// 将DataTable转换为List..T ,此方法会设定List的DefaultIfEmpty值
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="dt">表名</param>
        /// <returns>实体列表</returns>
        public static List<T> ConvertToListDefault<T>(DataTable dt)
        {
            if (dt == null)
                return null;
            List<T> list = new List<T>();
            //使用default关键字，T为空时返回：
            //1、如果T为引用类型，则t=null
            //2、如果T为值类型，则t=0
            //3、如果T为结构类型，则返回所有成员为0或空的结构
            T t = Activator.CreateInstance<T>();

            //定义属性集
            List<PropertyInfo> properies = null;
            string strColumnName = string.Empty;

            foreach (DataRow dr in dt.Rows)
            {
                //取得类中的所有属性
                properies = t.GetType().GetProperties().ToList();

                foreach (DataColumn dc in dt.Columns)
                {
                    string columnName = Regex.Replace(dc.ColumnName.ToLower(), @"(^[a-zA-Z0-9])|([_](\w))",
                        delegate(Match m) { return m.Value.ToUpper().Replace("_", ""); });

                    PropertyInfo pro = properies.Find(p => p.Name == columnName);

                    if (pro != null && !(dr[dc.ColumnName] is DBNull))
                        pro.SetValue(t, dr[dc.ColumnName], null);
                }
                //foreach (PropertyInfo pro in properies)
                //{
                //    object[] objs = pro.GetCustomAttributes(false);
                //    if (objs.Length > 0)
                //        //取得实体属性对应的列名
                //        foreach (object temp in objs)
                //        {
                //            if (temp is AttributeColumn)
                //            {
                //                strColumnName = (temp as AttributeColumn).ColumnName;
                //                //赋值到实体中的属性
                //                if (!dt.Columns.Contains(strColumnName) || dr[strColumnName] == null || dr[strColumnName] == DBNull.Value)
                //                {
                //                    continue;
                //                }
                //                pro.SetValue(t, dr[strColumnName], null);
                //            }
                //        }
                //    else
                //    {
                //        if (dt.Columns.Contains(pro.Name))
                //        {
                //            object obj = dr[pro.Name];
                //            if (obj == DBNull.Value)
                //            {
                //                Type type = pro.PropertyType;
                //                obj = type.IsValueType ? Activator.CreateInstance(type) : null;
                //            }
                //            pro.SetValue(t, obj, null);
                //        }
                //        else
                //        {
                //            Type type = pro.PropertyType;
                //            pro.SetValue(t, type.IsValueType ? Activator.CreateInstance(type) : null, null);
                //        }
                //    }
                //}
                list.Add(t);
            }

            return list.DefaultIfEmpty(t).ToList();
        }

        /// <summary>
        /// 将List..T转换为DataTable
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="lstT">实体列表</param>
        /// <returns>表</returns>
        public static DataTable ConvertToDataTable<T>(List<T> lstT)
        {
            //获取表名
            Type entityType = typeof(T);
            object[] objs2 = entityType.GetCustomAttributes(typeof(AttributeTable), false);

            string strName = string.Empty;
            if (objs2.Length > 0)
                strName = (objs2[0] as AttributeTable).TableName;
            else
                strName = entityType.Name;

            //定义返回的表
            DataTable table = new DataTable(strName);

            //定义属性集
            PropertyInfo[] properies = null;
            string strColumnName = string.Empty;

            //使用default关键字，T为空时返回：
            //1、如果T为引用类型，则t=null
            //2、如果T为值类型，则t=0
            //3、如果T为结构类型，则返回所有成员为0或空的结构
            T t = default(T);

            //实例化T
            t = Activator.CreateInstance<T>();
            //取得类中的所有属性
            properies = t.GetType().GetProperties();

            Hashtable ht = new Hashtable();

            //为表添加列
            foreach (PropertyInfo pro in properies)
            {
                object[] objs = pro.GetCustomAttributes(false);
                if (objs.Length > 0)
                    //取得实体属性对应的列名
                    foreach (object temp in objs)
                    {
                        if (temp is AttributeColumn)
                        {
                            strColumnName = (temp as AttributeColumn).ColumnName;
                            table.Columns.Add(strColumnName, pro.PropertyType);

                            if (!ht.Contains(strColumnName))
                                ht.Add(pro.Name, strColumnName);
                        }
                    }
                else if (objs2.Length == 0)
                {
                    table.Columns.Add(pro.Name, pro.PropertyType);

                    if (!ht.Contains(pro.Name))
                        ht.Add(pro.Name, pro.Name);
                }
            }

            //为表添加行并赋值
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (T item in lstT)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[ht[prop.Name].ToString()] = prop.GetValue(item);
                }
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
