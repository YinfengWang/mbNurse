//------------------------------------------------------------------------------------
//
//  ϵͳ����        : �����ƶ�ҽ��
//  ��ϵͳ����      : ������վ
//  ��������        : 
//  ����            : ComFunctionApp.cs
//  ���ܸ�Ҫ        : ��ͨ����
//  ������          : ����
//  ������          : 2006-05-08
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
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Env = System.Environment;
using Net = System.Net;
using System.IO;
using System.Drawing;

namespace HISPlus
{    
    public struct SYSTEMTIME
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMilliseconds;

        /// <summary>
        /// ��System.DateTimeת����
        /// </summary>
        /// <param name="time">System.DateTime���͵�ʱ�䡣</param>
        public void FromDateTime(DateTime time)
        {
            wYear = (ushort)time.Year;
            wMonth = (ushort)time.Month;
            wDayOfWeek = (ushort)time.DayOfWeek;
            wDay = (ushort)time.Day;
            wHour = (ushort)time.Hour;
            wMinute = (ushort)time.Minute;
            wSecond = (ushort)time.Second;
            wMilliseconds = (ushort)time.Millisecond;
        }

        /// <summary>
        /// ת��ΪSystem.DateTime���͡�
        /// </summary>
        /// <returns></returns>
        public DateTime ToDateTime()
        {
            return new DateTime(wYear, wMonth, wDay, wHour, wMinute, wSecond, wMilliseconds);
        }


        /// <summary>
        /// ��̬������ת��ΪSystem.DateTime���͡�
        /// </summary>
        /// <param name="time">SYSTEMTIME���͵�ʱ�䡣</param>
        /// <returns></returns>
        public static DateTime ToDateTime(SYSTEMTIME time)
        {
            return time.ToDateTime();
        }

    }
    

	/// <summary>
	/// ComFunctionApp ��ժҪ˵����
	/// </summary>
	public class CeAPI
	{
        public CeAPI()
		{
		}
        
        [DllImport("coredll.dll")]
        public static extern bool SetLocalTime(ref SYSTEMTIME Time);

        [DllImport("coredll.dll")]
        public static extern void GetLocalTime(ref SYSTEMTIME Time);


        /// <summary>
        /// ���ñ���ʱ��
        /// </summary>
        public static void SetLocalDateTime(DateTime dtNow)
        {
            SYSTEMTIME sysTime = new SYSTEMTIME();
            sysTime.FromDateTime(dtNow);

            SetLocalTime(ref sysTime);
        }
	}    
}
