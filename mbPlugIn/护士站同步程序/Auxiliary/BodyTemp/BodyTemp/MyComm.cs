using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BodyTemp
{
    public class MyComm
    {
        #region 通用类

        /// <summary>
        /// 同步信息
        /// </summary>
        public class SyncInfo
        {
            public string Comment;
            public string SrcSqlFile;
            public string DestSql;
            public string Filter;
            public string TableName;
            /// <summary>
            /// 日志文件名前缀，如果为空，则不写日志
            /// </summary>
            public string FnPreLog;
        }

        /// <summary>
        /// 日志类
        /// </summary>
        public class LogClass
        {
            public LogClass()
            {
            }

            #region 取文件名
            /// <summary>
            /// 获取日志文件名(包含绝对路径)
            /// </summary>
            /// <remarks>
            /// 文件名的格式: 应用程序当前路径 + '\Log\' + 当前日期 + 'Log.txt'
            /// </remarks>
            /// <returns>Log文件名(包含绝对路径)</returns>
            static public string GetLogFileName(int fnType)
            {
                return GetLogFileName(fnType, "");
            }
            static public string GetLogFileName(int fnType, string fnPre)
            {
                string fn = string.Empty;
                DateTime dt = DateTime.Now;
                switch (fnType)
                {
                    case SysConst.LogFnType.YMD:
                        fn = dt.ToLongDateString() + ".log";
                        break;
                    case SysConst.LogFnType.YMDH:
                        fn = dt.ToLongDateString() + String.Format("{0:D2}", dt.Hour) + ".log";
                        break;
                    default:
                        break;
                }
                if (!string.IsNullOrEmpty(fnPre.Trim()))
                    fn = fnPre + SysConst.STR.UnderLine + fn;

                string path = Path.Combine(Env.CurrentDirectory, "Log");

                return Path.Combine(path, fn);
            }
            #endregion

            #region 写日志WriteLog
            /// <summary>
            /// 写Log日志
            /// </summary>
            /// <param name="strLogFile">Log文件名</param>
            /// <param name="strMsg">要写入的内容</param>
            static public void WriteLog(string logFile, string msg)
            {
                try
                {
                    string dateNow = DateTime.Now.ToString();				// 系统当前日期(默认为本机)
                    string logPath = Path.GetDirectoryName(logFile);

                    // 如果目录不存在,创建它
                    if (!Dir.Exists(logPath))
                    {
                        Dir.CreateDirectory(logPath);
                    }

                    // 写入Log内容
                    StreamWriter logWriter = new StreamWriter(logFile, true);
                    logWriter.WriteLine(dateNow + SysConst.STR.TAB + msg);
                    logWriter.Close();
                }
                catch (Exception ex)
                {
                }
            }

            /// <summary>
            /// 写日志 默认文件名：“日期.log”
            /// </summary>
            /// <param name="msg"></param>
            static public void WriteLog(string msg)
            {
                WriteLog(GetLogFileName(SysConst.LogFnType.YMD), msg);
            }
            /// <summary>
            /// 写日志
            /// </summary>
            /// <param name="msg"></param>
            /// <param name="fnType">日志文件名类型SysConst.LogFnType</param>
            static public void WriteLog(string msg, int fnType)
            {
                WriteLog(GetLogFileName(fnType), msg);
            }
            /// <summary>
            /// 写日志
            /// </summary>
            ///<param name="fnType">日志文件名类型SysConst.LogFnType</param>
            ///<param name="fnPre">文件名前缀</param>
            static public void WriteLog(string msg, int fnType, string fnPre)
            {
                WriteLog(GetLogFileName(fnType, fnPre.Trim()), msg);
            }
            #endregion

        }

        #endregion

        #region 系统常量
        public class SysConst
        {
            /// <summary>
            /// 信息输出到那个设备
            /// </summary>
            public struct MsgOptDev
            {
                public const string ALL = "0^";
                public const string SCREEN = "1^";
                public const string LOG = "2^";
                public const string LOGHH = "3^";
            }

            /// <summary>
            /// 日志文件名格式
            /// </summary>
            public struct LogFnType
            {
                public const int YMD = 1;
                public const int YMDH = 2;
            }
            /// <summary>
            /// 字符串
            /// </summary>
            public struct STR
            {
                public static readonly string SQUARE_BRACKET_L = "[";                      // 方括号
                public static readonly string SQUARE_BRACKET_R = "]";                      // 方括号
                public static readonly string BRACE_LEFT = "{";                      // 大括号
                public static readonly string BRACE_RIGHT = "}";                      // 大括号
                public static readonly string BACKSLASH = "\\";                     // 反斜线
                public static readonly string SLASH = "/";                      // 斜线

                /// <summary>
                /// 竖线
                /// </summary>
                public static readonly string VERTICAL_LINE = "|";

                public static readonly string POINT = ".";                      // 点
                public static readonly string SUSPENSION_POINTS = "...";                    // 省略号

                /// <summary>
                /// 横线，即中划线
                /// </summary>
                public const string LINEATION = "-";

                /// <summary>
                /// 下划线
                /// </summary>
                public static readonly string UnderLine = "_";

                /// <summary>
                /// =号
                /// </summary>
                public static readonly string EQUAL = "=";

                /// <summary>
                /// 格式控制符.换行符
                /// </summary>
                public static readonly string CRLF = "\r\n";                   // 回车换行符
                public static readonly string TAB = "\t";                     // 跳格键
                /// <summary>
                /// // 空白符
                /// </summary>
                public static readonly string BLANK = " ";
                public static readonly string COLON = ":";                      // 冒号
                /// <summary>
                /// 逗号
                /// </summary>
                public static readonly string COMMA = ",";
                public static readonly string SEMICOLON = ";";                      // 分号
                public static readonly string SINGLE_QUOTATION = "'";                      // 单引号
                public static readonly string DOUBLE_QUOTATION = "''";                     // 双引号

                public static readonly string YEAR = "岁";                     // 年字符串
                public static readonly string MONTH = "月";                     // 月字符串
                public static readonly string DAY = "天";                     // 天字符串

                public static readonly string LEFT_ARROW = "<-";
                public static readonly string RIGHT_ARROW = "->";
            }

            /// <summary>
            /// 日期格式
            /// </summary>
            public struct FMT_DATE
            {
                /// <summary>
                /// 长日期格式.yyyy-MM-dd HH:mm:ss
                /// </summary>
                public static readonly string LONG = "yyyy-MM-dd HH:mm:ss";

                public static readonly string LONG_COMPACT = "yyyyMMddHHmmss";         // 压缩的日期格式   DATE_COMPACT_FORMAT

                /// <summary>
                /// 长日期格式.显示精度到分钟.yyyy-MM-dd HH:mm
                /// </summary>
                public static readonly string LONG_MINUTE = "yyyy-MM-dd HH:mm";

                /// <summary>
                /// 短日期格式 yyyy-MM-dd
                /// </summary>
                public static readonly string SHORT = "yyyy-MM-dd";

                /// <summary>
                /// yyyy年MM月dd日
                /// </summary>
                public static readonly string SHORT_CN = "yyyy年MM月dd日";

                /// <summary>
                /// yyyyMMdd
                /// </summary>
                public static readonly string SHORT_COMPACT = "yyyyMMdd";               // 短日期格式

                /// <summary>
                /// HH:mm:ss
                /// </summary>
                public static readonly string TIME = "HH:mm:ss";
                public static readonly string TIME_SHORT = "HH:mm";                  // 短时间格式       DATE_TIME_SHORT_FORMAT
                public static readonly string TIME_COMPACT = "HHmmss";                 // 短时间格式       DATE_TIME_NOCOLON
                public static readonly string TIME_ITEM = "00";                     // 时间每段的格式   TIME_ITEM_FORMAT

                public static readonly string MONTH_DAY = "MM-dd";                  //                  DATE_MONTH_DAY_FORMAT
            }

            /// <summary>
            /// 常用数字
            /// </summary>
            public struct VAL
            {
                /// <summary>
                /// 一年12个月
                /// </summary>
                public static readonly int MONTHS_PER_YEAR = 12;
                /// <summary>
                /// 一天24小时
                /// </summary>
                public static readonly int HOURS_PER_DAY = 24;
                /// <summary>
                /// 一周7天
                /// </summary>
                public static readonly int DAYS_PER_WEEK = 7;

                public static readonly int TWIPS_PER_PIXEL = 15;                       // 象素转换成缇的比率

                public static readonly int NO = 0;
                public static readonly int YES = 1;

                public static readonly int FAILED = -1;
            }

            /// <summary>
            /// 货币格式
            /// </summary>
            public struct FMT_MONEY
            {
                public static readonly string NORMAL = "0.00";                   // 金额显示
                public static readonly string STORAGE = "0.0000";                 // 存储
            }

            public SysConst()
            {
            }
        }
        #endregion
    }
}
