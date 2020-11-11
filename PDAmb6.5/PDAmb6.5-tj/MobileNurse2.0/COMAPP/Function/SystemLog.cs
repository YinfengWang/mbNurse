// ***********************************************************************
// Assembly         : SDMN
// Author           : LPD
// Created          : 11-11-2015
//
// Last Modified By : LPD
// Last Modified On : 11-11-2015
// ***********************************************************************
// <copyright file="SystemLog.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;

/// <summary>
/// The Function namespace.
/// </summary>
namespace HISPlus.COMAPP.Function
{
    /// <summary>
    /// 记录系统日志
    /// </summary>
    public class SystemLog
    {
        /// <summary>
        /// 系统异常日志
        /// </summary>
        /// <param name="exceptionInfo">异常信息</param>        
        public static void OutputExceptionLog(string exceptionInfo)
        {
            //设置存放路径
            string strPath = GetAppRunPath() + "\\SysLog\\ExceptionLogs";

            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
            try
            {
                /*
                 根据Config的值，判断日志存放天数，超过则删除                 
                 */

                //string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
                //string configFile = Path.Combine(appPath, "Config.xml");

                //DateTime dtimeLastDay = DateTime.Now.AddDays(-1);//根据Config配置，计算出需删除的最早日期
                //System.IO.DirectoryInfo dir = new DirectoryInfo(strPath);
                //FileInfo[] fiList = null;
                //if (dir.Exists)
                //{
                //    fiList = dir.GetFiles();
                //}
                //foreach (var item in fiList)
                //{
                //    //小于
                //    if (Convert.ToDateTime(item.Name.Remove(item.Name.Length - 4, 4)) < dtimeLastDay)
                //    {
                //        File.Delete(item.FullName);
                //    }

                //}


                FileStream fs = new FileStream(strPath + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine("-----------------------------------------------------------------------");
                //sw.WriteLine("函数名称    :" + functionName);
                sw.WriteLine("异常信息    :" + exceptionInfo);
                sw.WriteLine("发生时间    :" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sw.WriteLine("-----------------------------------------------------------------------");
                sw.WriteLine("\r\n"); sw.WriteLine("\r\n");
                sw.Close();
            }
            catch (Exception ex)
            {

            }


        }

        /// <summary>
        /// 记录消耗时间
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <param name="funName">函数名称</param>
        /// <param name="patientID">病人号码</param>
        /// <param name="operDate">操作时间</param>
        /// <param name="elapsedTime">时间差</param>
        public static void RecordExpendTime(string moduleName, string funName, string patientID, string operDate, string elapsedTime)
        {


            string strPath = GetAppRunPath() + "\\SysLog\\RecorExpendTime";
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }

            DeleteHisotyLogs(strPath);

            try
            {
                FileStream fs = new FileStream(strPath + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine("-----------------------------------------------------------------------");
                sw.WriteLine("模块名称:   :" + moduleName);
                sw.WriteLine("方法名称    :" + funName);
                sw.WriteLine("病人号码    :" + patientID);
                sw.WriteLine("消耗时间    :" + (Convert.ToDouble(elapsedTime) / 1000).ToString("f2") + "秒");
                sw.WriteLine("操作人      :" + GVars.User.Name);
                sw.WriteLine("操作时间    :" + operDate);
                sw.WriteLine("-----------------------------------------------------------------------");
                sw.WriteLine("\r\n"); sw.WriteLine("\r\n");
                sw.Close();
            }
            catch (Exception)
            {
            }
        }

        //获取程序运行路径，最后不包含"/"
        public static string GetAppRunPath()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        }

        /// <summary>
        /// PDA中记录日志
        /// </summary>
        /// <param name="apiAddress">接口地址</param>
        /// <param name="functionName">方法名称</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="parameterList">参数</param>
        /// <param name="oType">0:不记录返回结果  1:记录返回结果</param>
        /// <param name="resultValue">返回结果</param>
        public static void SetWebServiceLogs(string functionName, string parameterList, DataSet ds)
        {

            string strPath = GetAppRunPath() + "\\SysLog\\OutPutWebSrvLogs";
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }

            DeleteHisotyLogs(strPath);

            try
            {
                FileStream fs = new FileStream(strPath + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine("-----------------------------------------------------------------------");

                sw.WriteLine("调用方法    :" + functionName);
                sw.WriteLine("传入参数    :" + parameterList);
                //sw.WriteLine("开始时间    :" + startTime);
                sw.WriteLine("结束时间    :" + DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss"));
                string resultValue = ds.Tables[0].Rows.Count.ToString() + "行";
                sw.WriteLine("返回结果    :" + resultValue);
                sw.WriteLine("操作时间    :" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sw.WriteLine("-----------------------------------------------------------------------");
                sw.WriteLine("\r\n"); sw.WriteLine("\r\n");
                sw.Close();
            }
            catch (Exception)
            {
            }


        }

        public static void SetTestLog(string function, string strValue)
        {

            string strPath = GetAppRunPath() + "\\SysLog\\TestLog";
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
            //DeleteHisotyLogs(strPath);

            FileStream fs = new FileStream(strPath + @"\" + function + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            sw.BaseStream.Seek(0, SeekOrigin.End);

            sw.WriteLine("tableName:    :" + function);
            sw.WriteLine("值:    :" + strValue);

            sw.WriteLine("\r\n"); sw.WriteLine("\r\n");
            sw.Close();


        }

        /// <summary>
        /// 删除历史日志记录
        /// LPD
        /// </summary>
        public static void DeleteHisotyLogs(string strPath)
        {

            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
            string configFile = Path.Combine(appPath, "Config.xml");
            DataSet dsConfig = new DataSet("Config");
            dsConfig.ReadXml(configFile, XmlReadMode.ReadSchema);
            DataRow dr = dsConfig.Tables[0].Rows[0];

            //配置文件中，配置删除的天数
            int days = Convert.ToInt32(dr["LOG_DAYS"].ToString());
            /*
                根据Config的值，判断日志存放天数，超过则删除                 
                */
            //string appPath = GetAppRunPath();

            //根据Config配置，计算出需删除的最早日期
            DateTime dtimeLastDay = DateTime.Now.AddDays(days);
            System.IO.DirectoryInfo dir = new DirectoryInfo(strPath);
            FileInfo[] fiList = null;
            if (dir.Exists)
            {
                fiList = dir.GetFiles();
            }
            foreach (var item in fiList)
            {
                //小于
                if (Convert.ToDateTime(item.Name.Remove(item.Name.Length - 4, 4)) < dtimeLastDay)
                {
                    File.Delete(item.FullName);
                }

            }
        }


        //将DataSet转换为xml对象字符串
        public static string ConvertDataSetToXML(DataSet xmlDS)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;

            try
            {
                stream = new MemoryStream();
                //从stream装载到XmlTextReader
                writer = new XmlTextWriter(stream, Encoding.Unicode);

                //用WriteXml方法写入文件.
                xmlDS.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);

                UnicodeEncoding utf = new UnicodeEncoding();
                return utf.GetString(arr, 0, arr.Length).Trim();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}
