using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.OracleClient;
using System.IO;

namespace Common
{
    public class LogEnum
    {
        /// <summary>
        /// 枚举类型 记录日志
        /// </summary>
        public enum WriteLog
        {
            不记录任何日志 = 0,
            仅记录数据库日志 = 1,
            仅记录文件日志 = 2,
            记录文件及数据库日志 = 3,
            记录数据库日志失败情况下记录文件日志 = 4,
            记录文件日志失败情况下记录数据库日志 = 5,
        }
    }

    public class TJComm
    {
        public string strConn;

        private LogEnum.WriteLog? _enumWriteLog;

        /// <summary>
        /// 枚举类型 记录日志
        /// </summary>
        public LogEnum.WriteLog? EnumWriteLog
        {
            get
            {
                //if (_enumWriteLog == null)
                //    _enumWriteLog = GetEnumWriteLog();
                //return _enumWriteLog;
                // 仅供测试用
                return LogEnum.WriteLog.记录文件及数据库日志;
            }
            set { _enumWriteLog = value; }
        }

        public TJComm()
        {
            strConn = genConn();
        }



        /// <summary>
        /// 获取是否记录日志的状态值
        /// </summary>
        /// <returns>返回记录日志的枚举值</returns>
        public LogEnum.WriteLog GetEnumWriteLog()
        {
            string sql = "select parameter_value from app_configer_parameter where emp_no = '*' and app_name = 'WEBSERVICE' and dept_code = '*' and parameter_name = 'WRITE_DBLOG'";

            string flag = string.Empty;

            LogEnum.WriteLog enumWriteLog = LogEnum.WriteLog.不记录任何日志;

            try
            {
                //flag = OracleHelper.ExecuteScalar(sql).ToString(); ;
                ////日志开关是否打开
                //if (flag == "1")
                //{
                //    enumWriteLog = LogEnum.WriteLog.记录数据库日志失败情况下记录文件日志;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return enumWriteLog;
        }

        /// <summary>
        /// 在数据库中写日志。成功1；失败-1；没有插入0
        /// </summary>
        /// <param name="app_name"></param>
        /// <param name="operator_no"></param>
        /// <param name="operation"></param>
        /// <param name="win_no"></param>
        /// <param name="status"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public int WriteDbLog(string app_name, string operator_no, string operation, string win_no, string status, string log)
        {
            string sql = string.Empty;
            string flag = string.Empty;
            string pmout1 = string.Empty;
            string pmout2 = string.Empty;

            string separator = "　";

            //写文本日志
            int rtn = this.WriteLog(
                //app_name + separator +
                //operator_no + separator +
                operation + separator +
                //win_no + separator +
                status + Environment.NewLine +
                log + Environment.NewLine + "LENGTH=" + log.Length + Environment.NewLine);

            return 1;
        }

        /// <summary>
        /// 记录日志到文本文件
        /// </summary>
        /// <param name="logtxt">日志内容</param>
        /// <returns></returns>
        public int WriteLog(string logtxt)
        {
            string LogFile = Common.Static.GetWebConfigValue("logFile");
            string log = string.Empty;
            DateTime dt = DateTime.Now;
            log = LogFile + dt.Year + dt.Month + dt.Day + ".log";

            StreamWriter sw = null;
            try
            {
                // 当目录不存在时,创建
                if (!Directory.Exists(LogFile))
                    Directory.CreateDirectory(LogFile);
                
                sw = new StreamWriter(log, true);
                sw.WriteLine(System.DateTime.Now);
                sw.WriteLine(logtxt);

                sw.Flush();
            }
            catch
            {
                //1025伪代码，当写文本失败时，写服务器日志
                return -1;
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }

            return 1;
        }

        /// <summary>
        /// 解密函数，用于解密连接字符串。最好和HIS对比算法是否一致，以确保通用性。
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public string descript(string strIn)
        {
            int i, k;
            char chr;
            string strRtn = string.Empty;


            if (strIn == "" || strIn == null)
                return "";

            for (i = 1; i <= strIn.Length; i++)
            {
                chr = strIn[i - 1];//
                k = (int)chr;
                if (i % 2 == 0)
                    k = k - i + 32;
                else
                    k = k + i - 8;

                strRtn = strRtn + (char)k;
            }

            return strRtn;
        }

        /// <summary>
        /// 生成连接字符串,版本1
        /// </summary>
        /// <param name="Data_Source"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string genConn(string Data_Source, string user, string password)
        {
            //"Data Source=orcl_zxy;USER=BILLSERVICE;PASSWORD=billservice;"
            //此连接字符串较简单，可扩展加入更多参数
            return "Data Source=" + Data_Source + ";USER=" + descript(user) + ";PASSWORD=" + descript(password) + ";";

        }

        /// <summary>
        /// 生成连接字符串,版本2：从web.config中获取
        /// </summary>
        /// <returns></returns>
        public string genConn()
        {
            return genConn(Common.Static.GetWebConfigValueWLog("ORA"),
                            Common.Static.GetWebConfigValueWLog("ORAUSER"),
                            Common.Static.GetWebConfigValueWLog("ORAPASSWORD"));
        }
    }
}
