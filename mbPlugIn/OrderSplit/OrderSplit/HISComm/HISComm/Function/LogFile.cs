//------------------------------------------------------------------------------------
//
//  系统名称        : 医院信息系统
//  子系统名称      : 通用模块
//  对象类型        : 
//  类名            : LogFile.cs
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
using System.Collections;
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
	public class LogFile
	{
		public LogFile()
		{
		}
		

		#region 文件
		/// <summary>
		/// 获取Log文件名(包含绝对路径)
		/// </summary>
		/// <remarks>
		/// 文件名的格式: 应用程序当前路径 + '\Log\' + 当前日期 + 'Log.txt'
		/// </remarks>
		/// <returns>Log文件名(包含绝对路径)</returns>
		static public string GetLogFileName()
		{
			string fileName = Path.Combine(Env.CurrentDirectory, "Log");
			return Path.Combine(fileName, DateTime.Now.ToLongDateString() + "Log.txt");
		}


		/// <summary>
		/// 写Log日志
		/// </summary>
		/// <param name="strLogFile">Log文件名</param>
		/// <param name="strMsg">要写入的内容</param>
		static public void WriteLog(string logFile, string msg)
		{
			try
			{
				string dateNow	= DateTime.Now.ToString();				// 系统当前日期(默认为本机)
				string logPath	= Path.GetDirectoryName(logFile);
				
				// 如果目录不存在,创建它
				if(!Dir.Exists(logPath))
				{
					Dir.CreateDirectory(logPath);
				}
                
				// 写入Log内容
				StreamWriter logWriter = new StreamWriter(logFile, true);
				logWriter.WriteLine(dateNow + ComConst.STR.CRLF + msg);
				logWriter.Close();
			}
			catch (Exception ex)
			{
			}
		}
		
		
		/// <summary>
		/// 写Log日志
		/// </summary>
		/// <param name="strMsg">要写入的内容</param>
		static public void WriteLog(string msg)
		{
			WriteLog(GetLogFileName(), msg);
		}
		#endregion
	}
}
