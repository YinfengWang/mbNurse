//------------------------------------------------------------------------------------
//
//  ϵͳ����        : ҽ������վ
//  ��ϵͳ����      : ͨ��ģ��
//  ��������        : 
//  ����            : UniversalVarApp.cs
//  ���ܸ�Ҫ        : Ӧ�ó�������
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

namespace HISPlus
{
	/// <summary>
	/// UniversalVarApp ��ժҪ˵����
	/// </summary>
	public class Business
	{
        // ����
        public struct Nurse
        {
            public static int EventStartTime;                                                   // �����¼���ʼʱ��(��Ҫ�����¼�)
        }


        // ��ҩ;��
        public struct Administration
        {
            public static string Drug       = string.Empty;                                     // ��ҩ;��
            public static string Inject     = string.Empty;                                     // ע��;��
            public static string Transfuse  = string.Empty;                                     // ��Һ;��
        }


        // ����
        public struct Function
        {
            public static bool AutoSplitOrders = false;                                         // �Ƿ��Զ����ҽ��
        }

        public Business()
		{
		}                      
	}
}
