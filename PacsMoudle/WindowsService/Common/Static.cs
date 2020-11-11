using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Common
{
    /// <summary>
    /// 系统静态变量
    /// </summary>
    public class Static
    {

        //private static int _testNum = 0;
        ///// <summary>
        ///// 测试,
        ///// </summary>
        //public static int TestNum
        //{
        //    get { return ++Static._testNum; }
        //    set { Static._testNum = value; }
        //}

        private static System.Web.HttpContext _HttpCurrent;
        /// <summary>
        /// 当前Http请求
        /// </summary>
        public static System.Web.HttpContext HttpCurrent
        {
            get
            {
                if (Common.Static._HttpCurrent == null)
                    Common.Static._HttpCurrent = HttpContext.Current;
                return Static._HttpCurrent;
            }
            set { Static._HttpCurrent = value; }
        }

        private static List<string> _PacsUserBackground = new List<string>();
        ///// <summary>
        ///// Pacs后台进程的操作用户
        ///// </summary>
        //public static List<string> PacsUserBackground
        //{
        //    get { return Static._PacsUserBackground; }
        //    set { Static._PacsUserBackground = value; }
        //}

        /// <summary>
        /// [生成用户下所有的PACS图片] 添加新的后台进程.不存在并添加成功时,返回TRUE;如果已存在就返回FALSE.
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public static bool PacsUserBackgroundAdd(string userId)
        {
            if (_PacsUserBackground.Contains(userId))
                return false;
            else
                _PacsUserBackground.Add(userId);
            return true;
        }


        /// <summary>
        /// 后台进程完成后,删除
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public static bool PacsUserBackgroundDelete(string userId)
        {
            if (_PacsUserBackground.Contains(userId))
                return _PacsUserBackground.Remove(userId);
            return true;
        }


        private static List<string> _PacsExamBackground = new List<string>();
        ///// <summary>
        ///// Pacs后台进程的操作用户
        ///// </summary>
        //public static List<string> PacsUserBackground
        //{
        //    get { return Static._PacsUserBackground; }
        //    set { Static._PacsUserBackground = value; }
        //}

        /// <summary>
        ///  [根据检查号生成PACS图片] 添加新的后台进程.不存在并添加成功时,返回TRUE;如果已存在就返回FALSE.
        /// </summary>
        /// <param name="ExamNo">检查号</param>
        /// <returns></returns>
        public static bool PacsExamBackgroundAdd(string ExamNo)
        {
            int i = 20;
            if (_PacsExamBackground.Contains(ExamNo))
            {
                while (_PacsExamBackground.Contains(ExamNo) && i > 0)
                {
                    System.Threading.Thread.Sleep(1000);
                    i--;
                }
                return false;
            }
            else
                _PacsExamBackground.Add(ExamNo);
            return true;
        }

        /// <summary>
        /// 后台进程完成后,删除
        /// </summary>
        /// <param name="examNo">检查号</param>
        /// <returns></returns>
        public static bool PacsExamBackgroundDelete(string examNo)
        {
            if (_PacsExamBackground.Contains(examNo))
                return _PacsExamBackground.Remove(examNo);
            return true;
        }

        /// <summary>
        /// 读WebConfig配置参数
        /// </summary>
        /// <param name="parameter">参数名称</param>
        /// <returns>返回参数的字符串</returns>        
        public static string GetWebConfigValue(string parameter)
        {
            object obj = System.Configuration.ConfigurationManager.AppSettings[parameter];
            if (obj == null)
            {
                throw new Exception("WebConfig配置文件参数:" + parameter + "不存在,请联系管理员!");
            }
            return obj as string;
        }

        /// <summary>
        /// 读WebConfig配置参数,当读取异常时,写入文件日志
        /// </summary>
        /// <param name="parameter">参数名称</param>
        /// <returns>返回参数的字符串</returns>        
        public static string GetWebConfigValueWLog(string parameter)
        {
            object obj = System.Configuration.ConfigurationManager.AppSettings[parameter];
            if (obj == null)
            {
                new TJComm().WriteDbLog("WEBSERVICE", "server", "Web.config配置文件", "",
                    string.Empty, "WebConfig配置文件参数:" + parameter + "不存在!!");
                throw new Exception("WebConfig配置文件参数:" + parameter + "不存在,请联系管理员!");
            }
            return obj as string;
        }


        /// <summary>
        /// 传入文件所在服务器相对路径,获取文件URL路径
        /// </summary>
        /// <param name="filePath">文件相对服务器路径</param>
        /// <returns>文件URL</returns>
        public static string GetImageUrl(string filePath)
        {
            Uri url = Common.Static.HttpCurrent.Request.Url;
            //string strUrl = url.AbsoluteUri.Replace(url.AbsolutePath, @"/");
            string strUrl = url.AbsoluteUri.Remove(
                url.AbsoluteUri.IndexOf(
                Common.Static.HttpCurrent.Request.AppRelativeCurrentExecutionFilePath.Replace(@"~/", string.Empty)));

            if (!strUrl.StartsWith("http"))
            {
                url = HttpContext.Current.Request.Url;
                strUrl = url.AbsoluteUri.Replace(url.AbsolutePath, @"/");
            }
            return strUrl + filePath.Replace(@"\", @"/");

        }
    }
}
