//------------------------------------------------------------------------------------
//
//  系统名称        : 无线移动医疗
//  子系统名称      : 护理工作站
//  对象类型        : 
//  类名            : ComFunctionApp.cs
//  功能概要        : 共通函数
//  作成者          : 付军
//  作成日          : 2006-05-08
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
        /// 从System.DateTime转换。
        /// </summary>
        /// <param name="time">System.DateTime类型的时间。</param>
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
        /// 转换为System.DateTime类型。
        /// </summary>
        /// <returns></returns>
        public DateTime ToDateTime()
        {
            return new DateTime(wYear, wMonth, wDay, wHour, wMinute, wSecond, wMilliseconds);
        }


        /// <summary>
        /// 静态方法。转换为System.DateTime类型。
        /// </summary>
        /// <param name="time">SYSTEMTIME类型的时间。</param>
        /// <returns></returns>
        public static DateTime ToDateTime(SYSTEMTIME time)
        {
            return time.ToDateTime();
        }

    }
    

	/// <summary>
	/// ComFunctionApp 的摘要说明。
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
        /// 设置本地时间
        /// </summary>
        public static void SetLocalDateTime(DateTime dtNow)
        {
            SYSTEMTIME sysTime = new SYSTEMTIME();
            sysTime.FromDateTime(dtNow);

            SetLocalTime(ref sysTime);
        }
	}    
}
