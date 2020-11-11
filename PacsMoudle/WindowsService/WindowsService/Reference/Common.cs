using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace PACSMonitor
{
    public class Common
    {
        private static string _logName = string.Empty;
        /// <summary>
        /// 监控日志名称
        /// </summary>
        public static string LogName
        {
            get
            {
                if (!_logName.Contains(DateTime.Now.ToString("yyyyMMdd")))
                    _logName = Common.GetConfigValue("logPath") + DateTime.Now.ToString("yyyyMMdd") + "_PACS.log";
                return _logName;
            }
        }

        private static string _PacsSavePath;
        /// <summary>
        /// pacs保存路径
        /// </summary>
        public static string PacsSavePath
        {
            get
            {
                if (_PacsSavePath == null)
                    _PacsSavePath = Common.GetConfigValue("PacsSavePath");
                //if (!_PacsSavePath.EndsWith(@"\"))
                //    _PacsSavePath += @"\";
                return _PacsSavePath;
            }
            set { _PacsSavePath = value; }
        }

        private static string _LogPath;
        /// <summary>
        /// 日志目录
        /// </summary>
        public static string LogPath
        {
            get
            {
                if (Common._LogPath == null)
                    Common._LogPath = Common.GetConfigValue("logPath");
                if (!_LogPath.EndsWith(@"\"))
                    _LogPath += @"\";
                return _LogPath;
            }
            set { Common._LogPath = value; }
        }

        private static LogRefresh _LogRefresh;
        public static LogRefresh LogRefresh
        {
            get
            {
                if (Common._LogRefresh == null)
                {
                    _LogRefresh = new PACSMonitor.LogRefresh();
                    _LogRefresh.IsAuto = bool.Parse(Common.GetConfigValue("IsAuto"));
                    _LogRefresh.IsHand = bool.Parse(Common.GetConfigValue("IsHand"));
                    _LogRefresh.IsTimer = bool.Parse(Common.GetConfigValue("IsTimer"));
                    _LogRefresh.TimerSecond = int.Parse(Common.GetConfigValue("TimerSecond"));
                }
                return _LogRefresh;
            }
            set { Common._LogRefresh = value; }
        }

        /// <summary>
        /// 读App.config配置参数
        /// </summary>
        /// <param name="parameter">参数名称</param>
        /// <returns>返回参数的字符串</returns>        
        public static string GetConfigValue(string parameter)
        {
            object obj = ConfigurationManager.AppSettings[parameter];
            if (obj == null)
            {
                throw new Exception("WebConfig配置文件参数:" + parameter + "不存在,请联系管理员!");
            }
            return obj as string;
        }

        private static string fileName = System.IO.Path.GetFileName(Application.ExecutablePath);
        /// <summary>
        /// 更新App.config配置
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="newValue">新值</param>
        /// <returns></returns>
        public static bool updateSeeting(string key, string newValue)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(fileName);
            string value = config.AppSettings.Settings[key].Value = newValue;
            config.Save();
            return true;
        }

        /// <summary>
        /// 写监控日志.    (当设定自动更新时,刷新Log窗口)
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteLog(string msg)
        {
            //Thread t = new Thread(new ThreadStart(() => new Dicom()));
            //t.IsBackground = true;
            //t.Start();
            new Thread(() =>
            {
                msg = DateTime.Now.ToLocalTime() + Environment.NewLine + msg + Environment.NewLine;

                FileStream fs = new FileStream(Common.LogName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                //StreamReader sr = new StreamReader(fs, System.Text.Encoding.GetEncoding("gb2312"));
                using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default))
                // using (StreamWriter sw = new StreamWriter(LogName, true, System.Text.Encoding.Default))
                {
                    try
                    {
                        sw.WriteLine(msg);
                    }
                    catch (Exception ex)
                    {
                        sw.WriteLine("文件写入失败" + ex.Message);
                    }
                }
            }).Start();
        }
    }
}
