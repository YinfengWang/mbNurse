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
	public class MsgDbI
	{
        protected DbAccess  _connection;


        public MsgDbI(DbAccess connection)
		{
            _connection = connection;
		}
        
        
        /// <summary>
        /// ����Ĭ����Ϣ
        /// </summary>
        public void LoadMsg_Default(ref Message msg)
        {
            // ������Ϣ
            msg.AddMsg("WD001", "{0} �Ѿ�����!");
            
            // ������Ϣ
            msg.AddMsg("ED001", "�Ҳ�����ʼ���ļ�����ȷ��{0}λ��·����");
            msg.AddMsg("ED002", "��ҩҩ��Ϊ�գ����������ã�");
            msg.AddMsg("ED003", "{0}�ļ���û���ҵ����ݿ������ַ�����");
            msg.AddMsg("ED004", "�����ַ�������ʧ�ܣ�");
            msg.AddMsg("ED005", "ϵͳ����");
            msg.AddMsg("ED006", "����û��ʹ�ñ�ϵͳ��Ȩ��!");
            msg.AddMsg("ED008", "����Ŀ�Ѿ��л����¼��,����ɾ��!");
            
            // ��ʾ��Ϣ
            msg.AddMsg("ID001", "�û���Ϊ�ա�" + ComConst.STR.CRLF + "�������û������ȡȷ�ϰ�ť��");
            msg.AddMsg("ID002", "����Ϊ�ա�" + ComConst.STR.CRLF + "�����������ȡȷ�ϰ�ť��");
            msg.AddMsg("ID003", "������û���/���롣���������룡");
            msg.AddMsg("ID004", "���ε�¼ʧ�ܣ��˳���¼");
            msg.AddMsg("ID005", "��¼ϵͳ");
            msg.AddMsg("ID006", "��¼ʧ��");
            msg.AddMsg("ID007", "�����˳�");
            msg.AddMsg("ID008", "�쳣�˳�");
            
            msg.AddMsg("ID012", "��ѡ����Ԫ!");
        }
        
        
        /// <summary>
        /// ���ر��������ݿ��е���Ϣ
        /// </summary>
        public void LoadMsg_Db(ref Message msg)
        {
            DataSet ds = _connection.SelectData("SELECT * FROM SYSTEM_TEXT");

            // ��֯����
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow drRow in ds.Tables[0].Rows)
                {
                    msg.AddMsg(drRow["TEXT_ID"].ToString(), drRow["TEXT_CONTENT"].ToString());
                }
            }
        }
	}
}
