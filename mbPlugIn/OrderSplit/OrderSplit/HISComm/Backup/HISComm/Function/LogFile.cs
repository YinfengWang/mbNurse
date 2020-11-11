//------------------------------------------------------------------------------------
//
//  ϵͳ����        : ҽԺ��Ϣϵͳ
//  ��ϵͳ����      : ͨ��ģ��
//  ��������        : 
//  ����            : LogFile.cs
//  ���ܸ�Ҫ        : ��ͨ����
//  ������          : ����
//  ������          : 2007-01-19
//  �汾            : 1.0.0.0
// 
//------------------< �����ʷ >------------------------------------------------------
//  �������        :
//  �����          :
//  �������        :
//  �汾            :
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
	/// ComFunctions ��ժҪ˵����
	/// </summary>
	public class LogFile
	{
		public LogFile()
		{
		}
		

		#region �ļ�
		/// <summary>
		/// ��ȡLog�ļ���(��������·��)
		/// </summary>
		/// <remarks>
		/// �ļ����ĸ�ʽ: Ӧ�ó���ǰ·�� + '\Log\' + ��ǰ���� + 'Log.txt'
		/// </remarks>
		/// <returns>Log�ļ���(��������·��)</returns>
		static public string GetLogFileName()
		{
			string fileName = Path.Combine(Env.CurrentDirectory, "Log");
			return Path.Combine(fileName, DateTime.Now.ToLongDateString() + "Log.txt");
		}


		/// <summary>
		/// дLog��־
		/// </summary>
		/// <param name="strLogFile">Log�ļ���</param>
		/// <param name="strMsg">Ҫд�������</param>
		static public void WriteLog(string logFile, string msg)
		{
			try
			{
				string dateNow	= DateTime.Now.ToString();				// ϵͳ��ǰ����(Ĭ��Ϊ����)
				string logPath	= Path.GetDirectoryName(logFile);
				
				// ���Ŀ¼������,������
				if(!Dir.Exists(logPath))
				{
					Dir.CreateDirectory(logPath);
				}
                
				// д��Log����
				StreamWriter logWriter = new StreamWriter(logFile, true);
				logWriter.WriteLine(dateNow + ComConst.STR.CRLF + msg);
				logWriter.Close();
			}
			catch (Exception ex)
			{
			}
		}
		
		
		/// <summary>
		/// дLog��־
		/// </summary>
		/// <param name="strMsg">Ҫд�������</param>
		static public void WriteLog(string msg)
		{
			WriteLog(GetLogFileName(), msg);
		}
		#endregion
	}
}
