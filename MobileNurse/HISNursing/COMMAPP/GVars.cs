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
	public class GVars
	{
        // Ӧ�ó���
        public      static AppCur          App             = new AppCur();                      // Ӧ�ó���

        // ���ݿ����
        public      static DbAccess        OracleAccess;                                        // �����ݿ������
        public      static DbAccess        SqlserverAccess;                                     // ��Sqlserver���ݿ������
        
        // Ӧ�����1
        public      static IniFile         IniFile         = new IniFile();                     // ���������ļ�
        public      static Message         Msg             = new Message();                     // ��Ϣ����

		public      static UserCls         User            = new UserCls();                     // �û�
        public      static UserDbI         UserDbI;                                             // ���û��йص����ݷ���
        
        public      static HospitalDbI     HospitalDbI;
        
        // Ӧ�����2
        public      static EnDecrypt       EnDecryptor     = new EnDecrypt();                   // ������
        public      static OrderSplitter   OrderConvertor  = new OrderSplitter();               // ҽ�������
        
        
        public GVars()
		{
		}

        public static void ReloadDbSetting()
        {
            UserDbI = new UserDbI(OracleAccess, SqlserverAccess);
            HospitalDbI = new HospitalDbI(OracleAccess);
            
            GVars.OrderConvertor.OracleAccess = GVars.OracleAccess;
        }
	}
}
