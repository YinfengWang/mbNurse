using System;
using System.Collections.Generic;
using System.Linq;

namespace HISPlus
{
    /// <summary>
    /// 枚举类
    /// </summary>
    public static class Enums
    {
        //        1	TextBox
        //2	Label
        //3	CheckBox
        //4	SelectMul
        //5	SelectSingle
        //6	TextArea
        //7	Date
        //8	Time
        //9	DateTime

        /// <summary>
        /// 枚举: 控件类型
        /// </summary>
        public enum ControlType
        {
            /// <summary>
            /// 文本框
            /// </summary>
            TextBox = 1,
            /// <summary>
            /// 文本
            /// </summary>
            Label = 2,
            /// <summary>
            /// 复选框
            /// </summary>
            CheckBox = 3,
            /// <summary>
            /// 子项多选
            /// </summary>
            SelectMul = 4,
            /// <summary>
            /// 子项单选
            /// </summary>
            SelectSingle = 5,
            /// <summary>
            /// 多行文本框
            /// </summary>
            TextArea = 6,
            /// <summary>
            /// 日期控件
            /// </summary>
            Date = 7,
            /// <summary>
            /// 时间控件
            /// </summary>
            Time = 8,
            /// <summary>
            /// 日期时间控件
            /// </summary>
            DateTime = 9,
            /// <summary>
            /// 单选框
            /// </summary>
            RadioButton=10,
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public enum DataType
        {
            字符串 = 0,
            整数 = 1,
            浮点数 = 2,
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public enum ControlStatus
        {
            可用 = 1,
            不可用 = 2,
            //隐藏 = 3,
            //显示=4
        }

        /// <summary>
        /// 文书类型
        /// </summary>
        public enum TemplateType
        {
            标准文书 = 1,
            评分文书 = 2,
        }

        /// <summary>
        /// 关联类型
        /// </summary>
        public enum RelationType
        {
            默认,
            病人信息,
            签名,
            总分,
            分隔线,
            表格,
        }


        /// <summary>
        /// 关联病人信息项
        /// </summary>
        public enum RelationPatientItem
        {
            病人Id,
            住院次数,
            住院号,
            姓名,
            性别,
            //            国籍, 
            //            民族,
            入院时间,
            //            入科时间,
            床号,
            床标号,
            生日,
            诊断,
            护理等级,
            //            年龄, 
            //            医保类别,
            //            病区名称,
            //            病区号,
            所在科室,
            //            身高, 
            //            体重,
            //            职业,
            //            邮政编码,
            //            家庭电话,
            //            联系人姓名,
            //            联系人电话,
            //            身份证号码,
            //            家庭住址,
            //            术后天数,
            //            中医诊断,
            主治医生,
            病情状态,
            年龄,
        }


        /// <summary>
        /// 将枚举转换成Dictionary&lt;int, string&gt;
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static List<EnumItem> EnumToDicionary(Type enumType)
        {
            return (from int i in Enum.GetValues(enumType) select new EnumItem(i, Enum.GetName(enumType, i))).ToList();
        }

        // 获取枚举常量和名称的方法示例
        //object ojb = Enum.GetName(typeof(Test_Enum), Test_Enum.eight);
        //int i = (int)Test_Enum.eight;
    }

    [Serializable]
    public struct EnumItem
    {
        private int _key;
        private string _value;

        public EnumItem(int key, string value)
        {
            _key = key;
            _value = value;
        }

        public int Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
