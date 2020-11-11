// ***********************************************************************
// Assembly         : Xy.Auxiliary.PubCls
// Author           : LPD
// Created          : 12-23-2015
//
// Last Modified By : LPD
// Last Modified On : 12-23-2015
// ***********************************************************************
// <copyright file="LogHelper.cs" company="心医国际(西安)">
//     Copyright (c) 心医国际(西安). All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

/// <summary>
/// The PubCls namespace.
/// </summary>
namespace Xy.Auxiliary.PubCls
{
    /// <summary>
    /// 日志操作类
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 同步操作日志类
        /// </summary>
        public static void SetSyncLogs(string operContext)
        {
            //日志存放路径
            string logPath = System.Threading.Thread.GetDomain().BaseDirectory + "\\Log\\Sync"; ;
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            try
            {
                FileStream fs = new FileStream(logPath + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine("-----------------------------------------------------------------------");
                sw.WriteLine("操作说明:   :" + operContext);
                sw.WriteLine("操作时间    :" + DateTime.Now);
                sw.WriteLine("-----------------------------------------------------------------------");
                sw.WriteLine("\r\n"); sw.WriteLine("\r\n");
                sw.Close();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 拆分操作日志类
        /// </summary>
        /// <param name="operContext"></param>
        public static void SetSplitLogs(string operContext)
        {
            //日志存放路径
            string logPath = System.Threading.Thread.GetDomain().BaseDirectory + "\\Log\\Split"; ;
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            try
            {
                FileStream fs = new FileStream(logPath + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine("-----------------------------------------------------------------------");
                sw.WriteLine("操作说明:   :" + operContext);
                sw.WriteLine("操作时间    :" + DateTime.Now);
                sw.WriteLine("-----------------------------------------------------------------------");
                sw.WriteLine("\r\n"); sw.WriteLine("\r\n");
                sw.Close();
            }
            catch (Exception)
            {
            }
        }

       
    }
}
