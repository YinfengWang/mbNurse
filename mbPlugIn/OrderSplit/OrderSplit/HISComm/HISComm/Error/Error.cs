//------------------------------------------------------------------------------------
//
//  ϵͳ����        : ҽԺ��Ϣϵͳ
//  ��ϵͳ����      : ͨ��ģ��
//  ��������        : 
//  ����            : Error.cs
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
	/// ComFunctions ��ժҪ˵����
	/// </summary>
	public class Error
	{
		public Error()
		{
		}
		
        
		/// <summary>
		/// ������
		/// </summary>
		/// <remarks>
		/// ����:
		///     1: ��ʾ������Ϣ
		///     2: �Ѵ����¼�ڱ���Log�ļ���
		/// </remarks>
		/// <param name="ex">�쳣</param>
		static public void ErrProc(Exception ex)
		{   
			try
			{
				ErrDspForm frmErrorShow   = new ErrDspForm();					// ��ʾ������ʾ��Ϣ�Ĵ���
                
				string errMsg = ex.Message + ComConst.STR.CRLF + ex.StackTrace;	// ��ʾ��Ϣ: �������� + ����λ��
				
				// ��ʾ������Ϣ
				frmErrorShow.setErrMsg(errMsg);
				frmErrorShow.ShowDialog();
				
				// ��¼������Ϣ������Log��, ��ʽ: ���� + ������ʾ��Ϣ(�������� + ����λ��)
                LogFile.WriteLog(errMsg);
			}
			catch
			{
				// ���������, �������κδ���
			}
		}
	}
}
