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
        /// <summary>
        /// Ӧ�ó���
        /// </summary>
        public      static App             App             = new App();                         // Ӧ�ó���


        /// <summary>
        /// Oracle���ݿ�ķ��ʶ���
        /// </summary>
        public      static DbAccess        OracleHis;                                           // �����ݿ������
        public      static DbAccess        OracleMobile;                                        // �����ݿ������
        
        /// <summary>
        /// Ini�ļ�
        /// </summary>
        public      static IniFile         IniFile         = new IniFile();                     // ���������ļ�


        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public      static Message         Msg             = new Message();                     // ��Ϣ����


        /// <summary>
        /// ��ǰ�û�����
        /// </summary>
		public      static UserCls         User            = new UserCls();                     // �û�
        
        
        /// <summary>
        /// ��ǰ���˶���
        /// </summary>
        public      static PatientCls      Patient         = new PatientCls();                  // ����
        
        
        /// <summary>
        /// ���ܽ��ܹ���
        /// </summary>
        public      static EnDecrypt       EnDecryptor     = new EnDecrypt();                   // ������
        

        public GVars()
		{
		}
	}
}
