//------------------------------------------------------------------------------------
//
//  系统名称        : 医院信息系统
//  子系统名称      : 通用模块
//  对象类型        : 
//  类名            : ComFunction.cs
//  功能概要        : 共通函数
//  作成者          : 付军
//  作成日          : 2007-01-19
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        :
//  变更者          :
//  变更内容        :
//  版本            :
//------------------------------------------------------------------------------------

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using Path	= System.IO.Path;
using Dir	= System.IO.Directory;
using Env	= System.Environment;

namespace HISPlus
{    
	/// <summary>
	/// ComFunctions 的摘要说明。
	/// </summary>
	public class DataType
	{
		public DataType()
		{
		}
        

        #region 数据类型
        /// <summary>
        /// 获取DateTime的Null值
        /// </summary>
        /// <remarks>假定一个"1-1-1 12:00:00"值作为Null值</remarks>
        /// <returns>DateTime</returns>
        static public DateTime DateTime_Null()
        {
            return new DateTime(1700, 1, 1);
        }
        
        
        /// <summary>
        /// 判断DateTime是否为Null
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <returns>TRUE: 是为Null; FALSE: 不为Null</returns>
        static public bool DateTime_IsNull(ref DateTime dt)
        {
            return DateTime_Null().Date.Equals(dt.Date);
        }


        /// <summary>
        /// 把字符串转换成为日期
        /// </summary>
        /// <param name="strDateTime">日期字符串</param>
        /// <returns>日期</returns>
        static public DateTime DateTimeFromString(string strDateTime)
        {
            if (strDateTime.Trim().Length == 0)
            {
                return DateTime_Null();
            }
            else
            {
                return DateTime.Parse(strDateTime);
            }
        }


        /// <summary>
        /// 从一个字符串中尽量提取日期时间字符串
        /// </summary>
        /// <remarks>
        ///  1: 年月日时分秒可以被任意非数字字符分隔
        ///  2: 年月日时分秒可以不用任何分隔符分隔,提取时按年四位数整数, 其它都有两位整数
        ///  3: 1与2两种情况的结合体也可以识别, 如 200012-2 20:12-43, 可以正确识别
        /// </remarks>
        /// <param name="dtSrc">日期时间字符串</param>
        /// <returns>TRUE: 成功; FALSE: 失败</returns>
        static public bool GetDateTime(ref string dtSrc)
        {
            string strChar   = string.Empty;
            string strSect   = string.Empty;
            string strSep    = string.Empty;
            int    pos       = 0;
            int    val       = 0;
            int    year      = 0;
            int    month     = 0;
            int    maxDays   = 0;
            bool   blnNum    = false;
            int    len       = 4;
            
            string[] arrNum  = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
            string[] arrPart = {"20", "00", "00", "00", "00", "00"};
            
            // 字符串拆分
            dtSrc += " ";                       // " "为任意非数字字符, 目的在于方便接下的的for循环可以处理完整个字符串
            
            for(int i = 0; i < dtSrc.Length; i++)
            {
                strChar = dtSrc.Substring(i, 1);
                
                // 判断是否是数字
                blnNum = false;
                for(int j = 0; j < arrNum.Length; j++)
                {
                    if (strChar.Equals(arrNum[j]) == true)
                    {
                        blnNum = true;
                        break;
                    }
                }
                
                // 如果是数字
                if (blnNum == true)
                {
                    strSect += strChar;
                    
                    if (strSect.Length > len && pos < arrPart.Length)
                    {
                        arrPart[pos++] = strSect.Substring(0, len);
                        len = 2;
                        
                        strSect = strChar;
                    }
                }
                else
                {
                    if (strSect.Length > 0 && pos < arrPart.Length)
                    {
                        arrPart[pos++] = strSect;
                        len = 2;

                        strSect = string.Empty;
                    }
                }
            }
            
            if (pos < 3)            
            {
                return false;
            }
            
            // 正确性判断
            for(int i = 0; i < arrPart.Length; i++)
            {
                val = int.Parse(arrPart[i]);
                
                switch(i)
                {
                    case 0:     // 年
                        if (val < 50) {val += 2000; }
                        if (50 <= val && val < 100) {val += 1900; }
                        
                        if (val > 9999) { return false; }
                        
                        year = val;
                        break;
                        
                    case 1:     // 月
                        if (val < 1 || val > 12) { return false; } 
                        
                        month = val;
                        break;
                        
                    case 2:     // 日
                        maxDays = 31;
                        
                        if (month == 4 || month == 6 || month == 9 || month == 11)
                        {
                            maxDays = 30;
                        }
                        
                        if (month == 2)
                        {
                            maxDays = 28;

                            if (DateTime.IsLeapYear(year) == true)
                            {
                                maxDays = 29;
                            }
                        }
                        
                        if (val < 1 || val > maxDays) { return false; } 
                        break;
                    
                    case 3:     // 时
                        if (val < 0 || 23 < val) { return false; }
                        break;

                    case 4:     // 分
                    case 5:     // 秒
                        if (val < 0 || 59 < val) { return false; }
                        break;
                }
            }
            
            // 组合成日期时间字符串
            dtSrc = arrPart[0] + "-" + arrPart[1] + "-" + arrPart[2] + " " + arrPart[3] + ":" + arrPart[4] + ":" + arrPart[5];
            return true;
        }


        /// <summary>
        /// 把一个表示日期的字符串转换成短日期格式
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        static public string GetDateTimeShort(string dt)
        {
            if (dt.Trim().Length > 0)
            {
                return DateTime.Parse(dt).ToString(ComConst.FMT_DATE.SHORT);
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 把一个表示日期的字符串转换成长日期格式
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        static public string GetDateTimeLong(string dt)
        {
            if (dt.Trim().Length > 0)
            {
                return DateTime.Parse(dt).ToString(ComConst.FMT_DATE.LONG);
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 获取短时间格式
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        static public string GetShortTime(string dt)
        {
            if (dt.Trim().Length > 0)
            {
                return DateTime.Parse(dt).ToString(ComConst.FMT_DATE.TIME_SHORT);
            }
            else
            {
                return string.Empty;
            }
        }
        
        
        /// <summary>
        /// 获取月日(如8.2)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        static public string GetMD(string dt)
        {
            if (dt.Trim().Length > 0)
            {
                DateTime dtValue = DateTime.Parse(dt);
                int month = dtValue.Month;
                int day = dtValue.Day;
                return month.ToString() + "." + day.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        
        
        /// <summary>
        /// 获取年月日, 如2007年12月6日
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        static public string GetYMD(string dt)
        {
            if (dt.Trim().Length > 0)
            {
                DateTime dtValue = DateTime.Parse(dt);

                int year    = dtValue.Year;
                int month   = dtValue.Month;
                int day     = dtValue.Day;

                return year.ToString() + ComConst.STR.YEAR 
                    + month.ToString() + ComConst.STR.MONTH 
                    + day.ToString() + ComConst.STR.DAY;
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 获取数字的罗马表示
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        static public string GetRomaNum(int num)
        {
            if (num <= 0) { return string.Empty; }
            
            string[] arrNum = {"Ⅹ","Ⅰ","Ⅱ","Ⅲ","Ⅳ","Ⅴ","Ⅵ","Ⅶ","Ⅷ","Ⅸ"};
            string romaNum = string.Empty;
            
            string numStr = num.ToString();
            
            for(int i = 0; i < numStr.Length; i++)
            {
                romaNum += arrNum[int.Parse(numStr.Substring(i, 1))];
            }
            
            return romaNum;
        }
        #endregion
        

		#region 判断函数
		/// <summary>
		/// 判断一个字符串是不是正整数
		/// </summary>
		/// <remarks>仅包含数字</remarks>
		/// <param name="text">待验证的字符串</param>
		/// <returns>TRUE: 符合要求; FALSE: 不符合要求</returns>
		public static bool IsNumber(string text)
		{
			return Regex.IsMatch(text, @"^\d+$");
		}


        /// <summary>
        /// 检查某个数是不是正数(包括0)
        /// </summary>
        /// <param name="text">要检查的字符串</param>
        /// <returns>TRUE: 成功; FALSE: 失败</returns>
        static public bool IsPositive(String text)
        {
            Regex objNotPositivePattern=new Regex("[^0-9.]");
            Regex objPositivePattern=new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
            Regex objTwoDotPattern=new Regex("[0-9]*[.][0-9]*[.][0-9]*");

            return !objNotPositivePattern.IsMatch(text) &&
                objPositivePattern.IsMatch(text)  &&
                !objTwoDotPattern.IsMatch(text);
        }


        /// <summary>
        /// 检查一个字符串是不是合法的时间格式
        /// 如果是: 把输入字符串转换成标准的日期格式HH:MM:SS
        /// </summary>
        /// <param name="timeStr">表示时间的字符</param>
        /// <returns>TRUE: 是; FALSE: 不是</returns>
        static public bool IsTime(ref string timeStr)
        {
            const int MAX_TIME_ITEM_COUNT   = 3;
            
            int intValue                    = 0;
            
            string[] astrItem = timeStr.Split(ComConst.STR.COLON.ToCharArray());
            
            if (astrItem.Length < 1) return false;
            
            for(int i = 0; i < astrItem.Length && i < MAX_TIME_ITEM_COUNT; i++)
            {
                if (astrItem[i].Trim().Length == 0)
                {
                    astrItem[i] = ComConst.FMT_DATE.TIME_ITEM;
                }
                
                // 如果不是数字字符串, 退出
                if (IsNumber(astrItem[i]) == false)
                {
                    return false;
                }
                
                intValue = int.Parse(astrItem[i]);
                
                // 小时
                if (i == 0)
                {
                    // 小时范围 0 <= X < 24
                    if (intValue < 0 || intValue > 23)
                    {
                        return false;
                    }
                    
                    timeStr = intValue.ToString(ComConst.FMT_DATE.TIME_ITEM);
                }
                    // 分钟或秒
                else
                {
                    // 分钟或秒范围 0 <= X < 60
                    if (intValue < 0 || intValue > 60)
                    {
                        return false;
                    }
                    
                    timeStr += ComConst.STR.COLON + intValue.ToString(ComConst.FMT_DATE.TIME_ITEM);
                }
            }
            
            // 补足不够的部份
            for(int i = astrItem.Length; i < MAX_TIME_ITEM_COUNT; i++)
            {
                timeStr += ComConst.STR.COLON + ComConst.FMT_DATE.TIME_ITEM;
            }

            return true;
        }


        /// <summary>
        /// 检查一个短日期是否是正确的日期格式
        /// </summary>
        /// <remarks>YYYY-MM-DD或YYYYMMDD</remarks>
        /// <param name="dtStr"></param>
        /// <returns></returns>
        static public bool IsShortDate(string dtStr)
        { 
            // 去掉所有空格
            dtStr = dtStr.Replace(ComConst.STR.BLANK, string.Empty);

            if (dtStr.Length < 8) { return false;}

            dtStr = dtStr.Replace(ComConst.STR.SLASH,       ComConst.STR.LINEATION);
            dtStr = dtStr.Replace(ComConst.STR.BACKSLASH,   ComConst.STR.LINEATION);

            // 如果没分隔符
            if (dtStr.IndexOf(ComConst.STR.LINEATION) < 0)
            {
                dtStr = dtStr.Substring(0, 4) + ComConst.STR.LINEATION 
                      + dtStr.Substring(4, 2) + ComConst.STR.LINEATION 
                      + dtStr.Substring(6, 2);
            }

            // 获取年月日的每一个部份
            string[] arrPart = dtStr.Split(ComConst.STR.LINEATION.ToCharArray());

            if (arrPart.Length != 3) { return false; }
            
            if (IsPositive(arrPart[0]) == false
                || IsPositive(arrPart[1]) == false
                || IsPositive(arrPart[2]) == false)
            {
                return false;
            }

            // 检查日期是否正确            
            return true;
        }
		#endregion
	}
}
