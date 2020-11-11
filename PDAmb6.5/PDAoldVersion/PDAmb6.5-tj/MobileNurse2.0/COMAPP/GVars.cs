using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;
using System.Collections;
using Symbol;
using Symbol.Barcode;


namespace HISPlus
{
    public delegate void DataChanged();
    public delegate void ScanReader_ReadNotify(object sender, EventArgs e);
    
    
	/// <summary>
	/// UniversalVarApp ��ժҪ˵����
	/// </summary>
	public class GVars
	{
        // Ӧ�ó���
        public  static AppCur                       App             = new AppCur();             // Ӧ�ó���
        
        public  static SqlCeSync                    sqlceLocal      = new SqlCeSync();
        
        public  static DataSet                      Parameters      = null;
        public  static Message                      Msg             = new Message();            // ��Ϣ����
        
        
        // �û�
        public  static UserCls                      User            = new UserCls();
        // public  static UserDbI                      UserDbi         = new UserDbI();
        public static  PatientDbI                   PatDbI          = new PatientDbI();
        
        public static  UserWebSrv.UserWebSrv        WebUser         = new UserWebSrv.UserWebSrv();
        
        // ����
        public  static PatientCls                   Patient         = new PatientCls();
        public  static DataSet                      DsPatient       = null;
        public  static int                          PatIndex        = -1;
        
        // HIS����
        public  static HISDataWebSrv.HISDataWebSrv  HISDataSrv      = new HISPlus.HISDataWebSrv.HISDataWebSrv();

        
        // ɨ����
        #if SCANNER
        public static   Reader                      ScanReader      = null;				        // ɨ��������ʵ��
        public static   ReaderData                  ScanReaderBuffer = null;
        #endif
        
        public  static string                       Barcode_Patient = string.Empty;
        public  static string                       Barcode_Object  = string.Empty;
        public  static bool                         ScanPatient     = false;
        
        // �Ƿ�����ʾ��
        public  static bool                         Demo            = false;

        public static ArrayList                     patientFilterList = new ArrayList();        //���˹�������
        
        public GVars()
		{
		}


        public static DateTime GetDateNow()
        {
            return sqlceLocal.GetSysDate();
        }        
	}
}
