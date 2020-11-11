using System;
using System.Collections.Generic;
using System.Text;
using HISPlus.COMAPP.Function;

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
                ErrInfo frmErr = new ErrInfo();
                frmErr.SetErrMsg(ex.Message);
                frmErr.ShowDialog();
                
			}
			catch
			{
				// 错误处理出错, 不进行任何处理
			}
		}
	}
}