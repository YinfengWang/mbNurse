using System;
using System.Collections.Generic;
using System.Text;
using HISPlus.COMAPP.Function;

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
                ErrInfo frmErr = new ErrInfo();
                frmErr.SetErrMsg(ex.Message);
                frmErr.ShowDialog();
                
			}
			catch
			{
				// ���������, �������κδ���
			}
		}
	}
}