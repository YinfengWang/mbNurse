//------------------------------------------------------------------------------------
//  类名            : PersonCls.cs
//  功能概要        : 自然人类
//  作成者          : 付军
//  作成日          : 2008-04-10
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        : 
//  变更者          : 
//  变更内容        : 
//  版本            : 
//------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace HISPlus
{
    public class PersonCls
    {
        #region 变量定义
        protected string        _name           = string.Empty;                 // 姓名
        protected string        _gender         = string.Empty;                 // 性别: 男/女/未知
        protected DateTime      _birthday       = DataType.DateTime_Null();     // 出生日期

        protected string        _body_Height    = string.Empty;                 // 身高
        protected string        _body_Weight    = string.Empty;                 // 体重
        #endregion

        public PersonCls()
        {
            _birthday = DataType.DateTime_Null();
        }

        #region 属性
        public string Name
        {
            get { return _name;}
            set { _name = value;}
        }


        public string Gender
        {
            get { return _gender;}
            set { _gender = value;}
        }


        public DateTime Birthday
        {
            get { return _birthday;}
            set { _birthday = value;}
        }


        public string Body_Height
        {
            get { return _body_Height;}
            set { _body_Height = value;}
        }


        public string Body_Weight
        {
            get { return _body_Weight;}
            set { _body_Weight = value;}
        }
        #endregion


        #region 方法
        /// <summary>
        /// 获取年龄字符串
        /// </summary>
        /// <returns></returns>
        public string GetAge(DateTime dtNow)
        {
            return GetAge(_birthday, dtNow);
        }


        public static string GetAge(DateTime dtBirthday, DateTime dtNow)
        {
            string  strAge      = string.Empty;                         // 年龄的字符串表示
            int     intYear     = 0;                                    // 岁
            int     intMonth    = 0;                                    // 月
            int     intDay      = 0;                                    // 天

            // 如果没有设定出生日期, 返回空
            if (DataType.DateTime_IsNull(ref dtBirthday) == true)
            {
                return string.Empty;
            }
            
            // 计算天数
            intDay = dtNow.Day - dtBirthday.Day;
            if (intDay < 0)
            {
                dtNow = dtNow.AddMonths(-1);
                intDay += DateTime.DaysInMonth(dtNow.Year, dtNow.Month);
            }
            
            // 计算月数
            intMonth = dtNow.Month - dtBirthday.Month;
            if (intMonth < 0)
            {
                intMonth += 12;
                dtNow = dtNow.AddYears(-1);
            }
            
            // 计算年数
            intYear = dtNow.Year - dtBirthday.Year;
            
            // 格式化年龄输出
            if (intYear >= 1)                                            // 年份输出
            {
                strAge = intYear.ToString() + "岁";
            }
            
            if (intMonth > 0 && intYear <= 5)                           // 五岁以下可以输出月数
            {
                strAge += intMonth.ToString() + "月";
            }
            
            if (intDay >= 0 && intYear < 1)                              // 一岁以下可以输出天数
            {
                if (strAge.Length == 0 || intDay > 0)
                {
                    strAge += intDay.ToString() + "日";
                }
            }
            
            return strAge;
        }
        #endregion
    }
}
