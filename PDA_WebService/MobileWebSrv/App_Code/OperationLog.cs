/*
 Create Date:2015-05-29
 Author:Lpd
 */

using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net;

/// <summary>
///OperationLog 的摘要说明
/// </summary>
public class OperationLog
{
    public OperationLog()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 日志
    /// </summary>
    /// <param name="apiAddress">api地址</param>
    /// <param name="functionName">方法名称</param>
    /// <param name="parameterList">参数列表</param>
    /// <param name="resultValue">返回结果</param>
    public static void OutputOperationLog(string apiAddress, string functionName, string parameterList, string resultValue)
    {
        string strPath = HttpContext.Current.Server.MapPath("OperLogs");
        if (!Directory.Exists(strPath))
        {
            Directory.CreateDirectory(strPath);
        }
        //if (Directory.Exists(strPath) && !Directory.Exists(strPath + @"\" + DateTime.Now.ToString("yyyy-MM-dd")))
        //{
        //    Directory.CreateDirectory(strPath + @"\" + DateTime.Now.ToString("yyyy-MM-dd"));
        //}

        try
        {
            FileStream fs = new FileStream(strPath + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine("-----------------------------------------------------------------------");
            sw.WriteLine("接口地址:   :" + apiAddress);           
            sw.WriteLine("调用方法    :" + functionName);
            sw.WriteLine("传入参数    :" + parameterList);
            sw.WriteLine("返回结果    :" + resultValue);
            sw.WriteLine("操作人      :" + "admin");
            sw.WriteLine("操作时间    :" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sw.WriteLine("-----------------------------------------------------------------------");
            sw.WriteLine("\r\n"); sw.WriteLine("\r\n");
            sw.Close();
        }
        catch (Exception)
        {
        }


    }


    public static void OutputApplicationLog(string applicationName, int applicationCount)
    {
        string strPath = HttpContext.Current.Server.MapPath("ApplicationLog" + "\\" + applicationName);
        if (!Directory.Exists(strPath))
        {
            Directory.CreateDirectory(strPath);
        }
        try
        {
            FileStream fs = new FileStream(strPath + @"\" + DateTime.Now.ToString("yyyyMMddHH") + ".txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine("-----------------------------------------------------------------------");
            sw.WriteLine("time:" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sw.WriteLine("applicationName:   :" + applicationName);
            sw.WriteLine("applicationCount    :" + applicationCount);
            sw.WriteLine("-----------------------------------------------------------------------");
            sw.WriteLine("\r\n"); sw.WriteLine("\r\n");
            sw.Close();
        }
        catch (Exception)
        {
        }


    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationName"></param>
    /// <param name="applicationCount"></param>
    /// <param name="time"></param>
    public static void OutputApplicationLogTime(string applicationName, int applicationCount, string time, string sql, string ip)
    {
        string strPath = HttpContext.Current.Server.MapPath("ApplicationLog" + "\\" + applicationName);
        if (!Directory.Exists(strPath))
        {
            Directory.CreateDirectory(strPath);
        }
        try
        {
            FileStream fs = new FileStream(strPath + @"\" + DateTime.Now.ToString("yyyyMMddHH") + ".txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine("-----------------------------------------------------------------------");
            sw.WriteLine("当前时间:" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sw.WriteLine("IP   :" + ip);
            sw.WriteLine("方法名称:   :" + applicationName);
            sw.WriteLine("调用次数    :" + applicationCount);
            sw.WriteLine("操作用时    :" + time);
            sw.WriteLine("SQL:" + sql);
            sw.WriteLine("-----------------------------------------------------------------------");
            sw.WriteLine("\r\n"); sw.WriteLine("\r\n");
            sw.Close();
        }
        catch (Exception)
        {
        }


    }

    /// <summary>
    /// 方法名
    /// </summary>
    /// <returns></returns>
    public static string getMethodName()
    {
        StackTrace st = new StackTrace(1, true);
        string m = st.GetFrame(0).GetMethod().Name.ToString();
        return m;
    }

    public class DataTableConvertJson
    {

        #region dataTable转换成Json格式
        /// <summary>  
        /// dataTable转换成Json格式  
        /// </summary>  
        /// <param name="dt"></param>  
        /// <returns></returns>  
        public static string DataTable2Json(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"");
            jsonBuilder.Append(dt.TableName);
            jsonBuilder.Append("\":[");
            jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }

        #endregion dataTable转换成Json格式

        #region DataSet转换成Json格式
        /// <summary>  
        /// DataSet转换成Json格式  
        /// </summary>  
        /// <param name="ds">DataSet</param> 
        /// <returns></returns>  
        public static string Dataset2Json(DataSet ds)
        {
            StringBuilder json = new StringBuilder();

            foreach (DataTable dt in ds.Tables)
            {
                json.Append("{\"");
                json.Append(dt.TableName);
                json.Append("\":");
                json.Append(DataTable2Json(dt));
                json.Append("}");
            } return json.ToString();
        }
        #endregion

        /// <summary>
        /// Msdn
        /// </summary>
        /// <param name="jsonName"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJson(string jsonName, DataTable dt)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("{\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString() + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }

        /// <summary>
        /// DataTable转Json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dt)
        {
            StringBuilder result = new StringBuilder();
            result.Append("{\"totalCount\":" + dt.Rows.Count + ",\"data\":");
            result.Append( JsonConvert.SerializeObject(dt, new DataTableConverter()));
            result.Append("}");
            return result.ToString();
        }

        public static string ObjectToJson(object obj,string funcName)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.Append("{\"" + funcName + "\":" + obj.ToString());
            sbStr.Append("}");
            return sbStr.ToString();
        }
    }
}
