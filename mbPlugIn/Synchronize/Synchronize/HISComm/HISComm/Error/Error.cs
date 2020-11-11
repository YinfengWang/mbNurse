//------------------------------------------------------------------------------------
//
//  系统名称        : 医院信息系统
//  子系统名称      : 通用模块
//  对象类型        : 
//  类名            : Error.cs
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
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
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
	public class Error
	{
		public Error()
		{
		}
		
        
		/// <summary>
		/// 错误处理
		/// </summary>
		/// <remarks>
		/// 功能:
		///     1: 显示错误信息
		///     2: 把错误记录在本地Log文件中
		/// </remarks>
		/// <param name="ex">异常</param>
		static public void ErrProc(Exception ex)
		{   
			try
			{
				ErrDspForm frmErrorShow   = new ErrDspForm();					// 显示错误提示信息的窗体
                
				string errMsg = ex.Message + ComConst.STR.CRLF + ex.StackTrace;	// 提示信息: 错误名称 + 发生位置
				
				// 显示错误信息
				frmErrorShow.setErrMsg(errMsg);
				frmErrorShow.ShowDialog();
				
				// 记录错误信息到本地Log中, 格式: 日期 + 错误提示信息(错误名称 + 发生位置)
                LogFile.WriteLog(errMsg);
			}
			catch
			{
				// 错误处理出错, 不进行任何处理
			}
		}
	}
}
