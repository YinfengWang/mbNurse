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
        public MsgDbI()
		{
		}
        
        
        /// <summary>
        /// ����Ĭ����Ϣ
        /// </summary>
        public void LoadMsg(ref Message msg)
        {
            msg.AddMsg("E00001","{0}������󳤶�{1}!");
            msg.AddMsg("E00002","����ʧ��!");
            msg.AddMsg("E00003","������û���������!");
            msg.AddMsg("E00004","��������{0}!");
            msg.AddMsg("E00005","ɾ��{0}ʧ��!");
            msg.AddMsg("E00006","{0}�Ѿ�����, �����벻ͬ��{1}!");
            msg.AddMsg("E00007","{0}������{1}!");
            msg.AddMsg("E00008","������������벻ͬ! ���������롣");
            msg.AddMsg("E00009","�������!");
            msg.AddMsg("E00010","�����޸�ʧ��!");
            msg.AddMsg("E00011","����ȷ������!");
            msg.AddMsg("E00012","����������!");
            msg.AddMsg("E00013","�������ڱ���Ϊ������ܱ���!");
            msg.AddMsg("E00014","����ȷ������!");
            msg.AddMsg("E00015","ʱ���ʽ����ȷ, Ӧ����HH:mm:ss��ʽ��");
            msg.AddMsg("E00016","{0}����ȷ, ��Ч��ΧΪ {1} - {2}");
            msg.AddMsg("E00017","ֻ��ִ�е����ִ�е�!");
            msg.AddMsg("E00018","����ϵͳû�а�װ��д����ʶ��������������Ҫ�����ϵͳ�ϰ�װһ����д����ʶ����!");
            msg.AddMsg("E00019","ֻ��������ҽ�������ֹ�����!");
            msg.AddMsg("E00020","ֻ��¼���߻򳬼��û������޸��ϴμ�¼�Ľ��!");
            msg.AddMsg("E00021","����ѡ��Ҫ��ֵ�ҽ��!");
            msg.AddMsg("E00022","����ȷ��Ҫɾ����ҽ��ִ�е�!");
            msg.AddMsg("E00023","ֻ��ɾ���Ѿ�ֹͣ��ҽ����ҽ��ִ�е�!");
            msg.AddMsg("E00024","��ҽ��ִ�е��Ѿ�ִ�й���, ����ɾ����ִ�й���ҽ��ִ�е�!");
            msg.AddMsg("E00025","��ӡģ�嶨�����!");
            msg.AddMsg("E00026","�ò���û��ҽ��ִ�е�!");
            msg.AddMsg("E00027","����ѡ����!");
            msg.AddMsg("E00028","��ȡ�������ϵ��ļ�������������:{0}��");
            msg.AddMsg("E00029","�����ļ�������������Ϣ��ȫ!");
            msg.AddMsg("E00030","�����ļ����Ƿ��Ķ�!");
            msg.AddMsg("E00031","����ʱ���ʽ����ȷ, Ӧ����2000-10-08 12:30:00��ʽ��");
            msg.AddMsg("E00032","û��ȡ���������ڿ���!");
            msg.AddMsg("E00033","��ǰҽ���Ѿ�У�ԣ�����[����]��");
            msg.AddMsg("E00034","����ȷ������λ��!");
            msg.AddMsg("E00035","ҽ���Ѿ�У��, ����ɾ����ǰҽ��!");
            msg.AddMsg("E00036","��ҽ������ҽ��������ɾ����ҽ������ɾ����ҽ��!");
            msg.AddMsg("E00037","ҽ����¼Ϊ��,ɾ����Ч!");
            msg.AddMsg("E00038","���鵥��ִ��,����ɾ��!");
            msg.AddMsg("E00039","���鵥�ѼƼ�,����ɾ��!");
            msg.AddMsg("E00040","��ҽ�������ǵ�һ��ҽ��!");
            msg.AddMsg("E00041","����ҽ�� ֹͣʱ��ǿղ�������ҽ��!");
            msg.AddMsg("E00042","����ҩ��ҽ�����ܹ��ɸ���ҽ��!");
            msg.AddMsg("E00043",";����ͬ��ҽ�����ܹ��ɸ���ҽ��!");
            msg.AddMsg("E00044","ֻ�г���ҽ������ֹͣ!");
            msg.AddMsg("E00045","ֻ����ִ�е�ҽ������ֹͣ!");
            msg.AddMsg("E00046","ѡ��ļ�����Ŀ�б걾��һ��,������ѡ��!");
            msg.AddMsg("E00047","����ѡ����ɾ������Ŀ");
            msg.AddMsg("E00048","ʱ���ʽ����ȷ,����������!");
            msg.AddMsg("E00049","������������!");
            msg.AddMsg("E00050","������걾!");
            msg.AddMsg("E00051","ѡ��ı걾����Ŀ�걾��һ��!");
            msg.AddMsg("E00052","�����������Ŀ!");
            msg.AddMsg("E00053","��{0}��Ŀ��ǰ����Ŀ�����һ��!");
            msg.AddMsg("E00054","��ѡ��ҽ��!");
            msg.AddMsg("E00055","{0}ʧ��!");
            msg.AddMsg("E00056", "���û���������Ĳ���, �����л��ɸ��û�!");
            msg.AddMsg("E00057", "������{0}!");
            msg.AddMsg("E00058", "�����������ĸ��ͷ!");
            msg.AddMsg("E00059", "�����ļ�����!");
            msg.AddMsg("E00060", "��ѡ��{0}!");
            msg.AddMsg("E00061", "����Ŀû����������!����ϵͳ����Ա��ϵ!");
            msg.AddMsg("E00062", "��ά���ʽ����ȷ!");
            msg.AddMsg("E00063", "ƿǩ���ڲ���!");


            msg.AddMsg("I00001","�����޸ĳɹ�!");
            msg.AddMsg("I00002","��ʼ������ɹ�!");
            msg.AddMsg("I00003","������豸��ʱ������, ϵͳ�������豸ʱ����Ϊ�̵�ʱ��!");
            msg.AddMsg("I00004","{0}�ɹ�!");
            msg.AddMsg("I00005","����ƥ����ȷ!");
            msg.AddMsg("I00006","��ȷ�Ĳ���!");
            //msg.AddMsg("I00007","�����ڵ�ǰ�Ĳ���!");
            msg.AddMsg("I00007","ҩƷ�벡�˲���!");
            msg.AddMsg("I00008", "��ѡ��һ��{0}!");
            msg.AddMsg("I00009", "�����¼�ʱ�䲻�ܴ��ڵ�ǰʱ��!");
            msg.AddMsg("I00010", "��ʱ�����������, �����޸�ʱ��!");
            msg.AddMsg("I00011", "��¼ʱ�䲻�ܴ��ڵ�ǰʱ��!");
            msg.AddMsg("I00012", "������ִ�е�ƥ��!");
            
            msg.AddMsg("Q00001","���������û�б���, ��Ҫ������?");
            msg.AddMsg("Q00002","��ȷ��Ҫɾ�� {0} {1} ��?");
            msg.AddMsg("Q00003","��ȷ��Ҫ�˳���ϵͳ��? ");
            msg.AddMsg("Q00004","��ȷ��{0}��?");
            msg.AddMsg("Q00005","��ȷ��Ҫ�����û�: {0} ��������?");
            msg.AddMsg("Q00006","�����Ѹ���,��Ҫ������?");
            msg.AddMsg("Q00007","���������û�б���, Ҫ������?");
            msg.AddMsg("Q00008","���ڰ���ʱ������ݱ��浽Db��, ȷʵҪ�˳���?");
            msg.AddMsg("Q00009","�ϴα��������û����ȫ�ϴ������ݿ���, �Ƿ���ʾδ���������!");
            msg.AddMsg("Q00010","��ȷ���Ѿ��ֹ�ͬ����������ϴ�û�б���ɹ�����������?");
            msg.AddMsg("Q00011","����û��ͬ�����, �����ֹ�ͬ����?");
            msg.AddMsg("Q00012","����û��ͬ���ɹ�, �����ֹ�ͬ����?");
            msg.AddMsg("Q00013","��ȷ��Ҫɾ��ѡ���ҽ��ִ�е���?");
            msg.AddMsg("Q00014","ͬʱɾ����Ӧ�ļ������뵥��?");
            msg.AddMsg("Q00015","��ȷ��Ҫȡ����ѡ��ļ����Ŀ��?");
            msg.AddMsg("Q00016","�Ƿ񱣴���ѡ���ļ����Ŀ?");
            msg.AddMsg("Q00017","���Ŀ��ҽ����������¼�������Ŀ���Ƿ����?");
            msg.AddMsg("Q00018","������Ŀ���ཫ���������¼�������Ŀ���Ƿ����?");
            msg.AddMsg("Q00019","��ȷ��Ҫɾ����ǰ����?");
            msg.AddMsg("Q00020","������Һ��Ϣδ����, Ҫ������?");
            msg.AddMsg("Q00021","ִ�е��벡��ƥ��, ��ȷ��{0}��?");
            msg.AddMsg("Q00022","��ǰ������δ����, ��ȷ��{0}��?");
            
            msg.AddMsg("S00000","");
            msg.AddMsg("S00001","��ӭʹ�ñ�ϵͳ!");
            msg.AddMsg("S00002","���ݻ������!");
            msg.AddMsg("T00001","��Ŀ\\{0}H");
            msg.AddMsg("T10001","�����б� (��{0}��)");
            msg.AddMsg("W00001","ȡ�������뵥�Ŵ���!ɾ���������뵥ʧ��!");
            msg.AddMsg("W00002","���ܽ�����ҽ����ҽ����Ϊ��ҽ����");
            msg.AddMsg("W00003","ֹͣʱ�䲻�����ڵ�ǰʱ��!");
            msg.AddMsg("W00004","��ҩ���ߴ�����");
            msg.AddMsg("W00005","�ò��˲�����!");
            msg.AddMsg("W00006","ѡ������ڲ��ܴ��ڽ���!");
            msg.AddMsg("W00007","��ѡ��{0}!");            
        }
        
        
        /// <summary>
        /// ���ر��������ݿ��е���Ϣ
        /// </summary>
        public void LoadMsg_Db(ref Message msg)
        {

        }
	}
}