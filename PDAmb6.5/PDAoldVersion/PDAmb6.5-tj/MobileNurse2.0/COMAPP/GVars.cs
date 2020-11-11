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
	/// UniversalVarApp 的摘要说明。
	/// </summary>
	public class GVars
	{
        // 应用程序
        public  static AppCur                       App             = new AppCur();             // 应用程序
        
        public  static SqlCeSync                    sqlceLocal      = new SqlCeSync();
        
        public  static DataSet                      Parameters      = null;
        public  static Message                      Msg             = new Message();            // 消息对象
        
        
        // 用户
        public  static UserCls                      User            = new UserCls();
        // public  static UserDbI                      UserDbi         = new UserDbI();
        public static  PatientDbI                   PatDbI          = new PatientDbI();
        
        public static  UserWebSrv.UserWebSrv        WebUser         = new UserWebSrv.UserWebSrv();
        
        // 病人
        public  static PatientCls                   Patient         = new PatientCls();
        public  static DataSet                      DsPatient       = null;
        public  static int                          PatIndex        = -1;
        
        // HIS数据
        public  static HISDataWebSrv.HISDataWebSrv  HISDataSrv      = new HISPlus.HISDataWebSrv.HISDataWebSrv();

        
        // 扫描器
        #if SCANNER
        public static   Reader                      ScanReader      = null;				        // 扫描器对象实例
        public static   ReaderData                  ScanReaderBuffer = null;
        #endif
        
        public  static string                       Barcode_Patient = string.Empty;
        public  static string                       Barcode_Object  = string.Empty;
        public  static bool                         ScanPatient     = false;
        
        // 是否是演示版
        public  static bool                         Demo            = false;

        public static ArrayList                     patientFilterList = new ArrayList();        //病人过滤条件
        
        public GVars()
		{
		}


        public static DateTime GetDateNow()
        {
            return sqlceLocal.GetSysDate();
        }        
	}
}
