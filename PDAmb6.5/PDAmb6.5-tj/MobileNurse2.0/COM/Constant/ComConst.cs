//------------------------------------------------------------------------------------
//
//  系统名称        : 医院信息系统
//  子系统名称      : 共通模块
//  对象类型        : 
//  类名            : ComConst.cs
//  功能概要        : 常量定义
//  作成者          : 付军
//  作成日          : 2007-01-22
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        :
//  变更者          :
//  变更内容        :
//  版本            :
//------------------------------------------------------------------------------------
using System;

namespace HISPlus
{
	/// <summary>
	/// ComConst 的摘要说明。
	/// </summary>
    public class ComConst
    {
        // 常用字符
        public struct STR
        {
            public static readonly string SQUARE_BRACKET_L  = "[";                      // 方括号
            public static readonly string SQUARE_BRACKET_R  = "]";                      // 方括号
            public static readonly string BRACE_LEFT        = "{";                      // 大括号
            public static readonly string BRACE_RIGHT       = "}";                      // 大括号
            public static readonly string BACKSLASH         = "\\";                     // 反斜线
            public static readonly string SLASH             = "/";                      // 斜线
            public static readonly string POINT             = ".";                      // 点
            public static readonly string SUSPENSION_POINTS = "...";                    // 省略号
            public static readonly string LINEATION         = "-";                      // 中划线
            public static readonly string EQUAL             = "=";                      // =

            // 格式控制符
            public static readonly string CRLF              = "\r\n";                   // 回车换行符
            public static readonly string TAB               = "\t";                     // 跳格键
            public static readonly string BLANK             = " ";                      // 空白符
            public static readonly string COLON             = ":";                      // 冒号
            public static readonly string COMMA             = ",";                      // 逗号
            public static readonly string SEMICOLON         = ";";                      // 分号
            public static readonly string SINGLE_QUOTATION  = "'";                      // 单引号
            public static readonly string DOUBLE_QUOTATION  = "''";                     // 双引号

            public static readonly string YEAR              = "岁";                     // 年字符串
            public static readonly string MONTH             = "月";                     // 月字符串
            public static readonly string DAY               = "天";                     // 天字符串
        }
                 
        // 日期格式
        public struct FMT_DATE
        {
            public static readonly string LONG              = "yyyy-MM-dd HH:mm:ss";    // 长日期格式       DATE_LONG_FORMAT
            public static readonly string LONG_COMPACT      = "yyyyMMddHHmmss";         // 压缩的日期格式   DATE_COMPACT_FORMAT

            public static readonly string LONG_MINUTE       = "yyyy-MM-dd HH:mm";       //                  DATE_LONG_MINUTE_FORMAT
            
            public static readonly string SHORT             = "yyyy-MM-dd";             // 短日期格式       DATE_SHORT_FORMAT
            public static readonly string SHORT_COMPACT     = "yyyyMMdd";               // 短日期格式

            public static readonly string TIME              = "HH:mm:ss";               // 时间格式         DATE_TIME_FORMAT
            public static readonly string TIME_SHORT        = "HH:mm";                  // 短时间格式       DATE_TIME_SHORT_FORMAT
            public static readonly string TIME_COMPACT      = "HHmmss";                 // 短时间格式       DATE_TIME_NOCOLON
            public static readonly string TIME_ITEM         = "00";                     // 时间每段的格式   TIME_ITEM_FORMAT

            public static readonly string MONTH_DAY         = "MM-dd";                  //                  DATE_MONTH_DAY_FORMAT
        }

        // 常用数字
        public struct VAL
        {
            public static readonly int    MONTHS_PER_YEAR   = 12;                       // 一年12个月
            public static readonly int    HOURS_PER_DAY     = 24;                       // 一天24小时
            public static readonly int    DAYS_PER_WEEK     = 7;                        // 一周7天
    		
            public static readonly int    TWIPS_PER_PIXEL   = 15;                       // 象素转换成缇的比率

		    public static readonly int    NO                = 0;
		    public static readonly int	  YES               = 1;

            public static readonly int    FAILED            = -1;
        }
        
		// 货币格式
        public struct FMT_MONEY
        {
            public static readonly string NORMAL            = "0.00";                   // 金额显示
            public static readonly string STORAGE           = "0.0000";                 // 存储
        }
		
        public ComConst()
        {
        }
    }
}
