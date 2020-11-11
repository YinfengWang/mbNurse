using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HISPlus
{
    public class DataRowComparer : IEqualityComparer<DataRow>
    {
        private string[] columns;
        public DataRowComparer(string[] columns)
        {
            if (columns == null || columns.Length == 0)
                throw new ArgumentException();
            this.columns = columns;
        }

        public bool Equals(DataRow x, DataRow y)
        {
            if (object.ReferenceEquals(x, y))
                return true;
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
                return false;
            foreach (string column in columns)
            {
                //比较两个数据行时排除包含“_TIMESTAMP”字段
                if (column.IndexOf("_TIMESTAMP") > -1)
                    continue;
                if (x.Table.Columns.Contains(column) && y.Table.Columns.Contains(column))
                {
                    if (x[column].GetType() == typeof(DateTime) && y[column].GetType() == typeof(DateTime))
                    {
                        DateTime time1 = Convert.ToDateTime(x[column]);
                        DateTime time2 = Convert.ToDateTime(y[column]);
                        if (DateTime.Compare(time1, time2) != 0)
                            return false;
                    }
                    else if (x[column].GetType() == typeof(decimal) && y[column].GetType() == typeof(decimal))
                    {
                        decimal d1 = Convert.ToDecimal(x[column]);
                        decimal d2 = Convert.ToDecimal(y[column]);
                        if (decimal.Compare(d1, d2) != 0)
                            return false;
                    }
                    else
                    {
                        if (!x[column].ToString().Equals(y[column].ToString()))
                            return false;
                    }
                }
            }
            return true;
        }

        public int GetHashCode(DataRow obj)
        {
            if (object.ReferenceEquals(obj, null))
                return 0;
            int hashCode = 0;
            foreach (string column in columns)
            {
                if (obj.Table.Columns.Contains(column))
                {
                    //比较两个数据行时排除包含“_TIMESTAMP”字段
                    if (column.IndexOf("_TIMESTAMP") > -1)
                        continue;
                    if (obj[column].GetType() == typeof(DateTime))
                    {
                        DateTime time1 = Convert.ToDateTime(obj[column]);
                        hashCode += time1.GetHashCode();
                    }
                    else if (obj[column].GetType() == typeof(decimal))
                    {
                        decimal d1 = Convert.ToDecimal(obj[column]);
                        hashCode += d1.GetHashCode();
                    }
                    else
                    {
                        hashCode += obj[column].ToString().GetHashCode();
                    }

                }
            }
            return hashCode;
        }
    }
}
