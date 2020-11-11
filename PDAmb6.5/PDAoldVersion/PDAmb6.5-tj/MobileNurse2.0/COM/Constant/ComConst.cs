//------------------------------------------------------------------------------------
//
//  ϵͳ����        : ҽԺ��Ϣϵͳ
//  ��ϵͳ����      : ��ͨģ��
//  ��������        : 
//  ����            : ComConst.cs
//  ���ܸ�Ҫ        : ��������
//  ������          : ����
//  ������          : 2007-01-22
//  �汾            : 1.0.0.0
// 
//------------------< �����ʷ >------------------------------------------------------
//  �������        :
//  �����          :
//  �������        :
//  �汾            :
//------------------------------------------------------------------------------------
using System;

namespace HISPlus
{
	/// <summary>
	/// ComConst ��ժҪ˵����
	/// </summary>
    public class ComConst
    {
        // �����ַ�
        public struct STR
        {
            public static readonly string SQUARE_BRACKET_L  = "[";                      // ������
            public static readonly string SQUARE_BRACKET_R  = "]";                      // ������
            public static readonly string BRACE_LEFT        = "{";                      // ������
            public static readonly string BRACE_RIGHT       = "}";                      // ������
            public static readonly string BACKSLASH         = "\\";                     // ��б��
            public static readonly string SLASH             = "/";                      // б��
            public static readonly string POINT             = ".";                      // ��
            public static readonly string SUSPENSION_POINTS = "...";                    // ʡ�Ժ�
            public static readonly string LINEATION         = "-";                      // �л���
            public static readonly string EQUAL             = "=";                      // =

            // ��ʽ���Ʒ�
            public static readonly string CRLF              = "\r\n";                   // �س����з�
            public static readonly string TAB               = "\t";                     // �����
            public static readonly string BLANK             = " ";                      // �հ׷�
            public static readonly string COLON             = ":";                      // ð��
            public static readonly string COMMA             = ",";                      // ����
            public static readonly string SEMICOLON         = ";";                      // �ֺ�
            public static readonly string SINGLE_QUOTATION  = "'";                      // ������
            public static readonly string DOUBLE_QUOTATION  = "''";                     // ˫����

            public static readonly string YEAR              = "��";                     // ���ַ���
            public static readonly string MONTH             = "��";                     // ���ַ���
            public static readonly string DAY               = "��";                     // ���ַ���
        }
                 
        // ���ڸ�ʽ
        public struct FMT_DATE
        {
            public static readonly string LONG              = "yyyy-MM-dd HH:mm:ss";    // �����ڸ�ʽ       DATE_LONG_FORMAT
            public static readonly string LONG_COMPACT      = "yyyyMMddHHmmss";         // ѹ�������ڸ�ʽ   DATE_COMPACT_FORMAT

            public static readonly string LONG_MINUTE       = "yyyy-MM-dd HH:mm";       //                  DATE_LONG_MINUTE_FORMAT
            
            public static readonly string SHORT             = "yyyy-MM-dd";             // �����ڸ�ʽ       DATE_SHORT_FORMAT
            public static readonly string SHORT_COMPACT     = "yyyyMMdd";               // �����ڸ�ʽ

            public static readonly string TIME              = "HH:mm:ss";               // ʱ���ʽ         DATE_TIME_FORMAT
            public static readonly string TIME_SHORT        = "HH:mm";                  // ��ʱ���ʽ       DATE_TIME_SHORT_FORMAT
            public static readonly string TIME_COMPACT      = "HHmmss";                 // ��ʱ���ʽ       DATE_TIME_NOCOLON
            public static readonly string TIME_ITEM         = "00";                     // ʱ��ÿ�εĸ�ʽ   TIME_ITEM_FORMAT

            public static readonly string MONTH_DAY         = "MM-dd";                  //                  DATE_MONTH_DAY_FORMAT
        }

        // ��������
        public struct VAL
        {
            public static readonly int    MONTHS_PER_YEAR   = 12;                       // һ��12����
            public static readonly int    HOURS_PER_DAY     = 24;                       // һ��24Сʱ
            public static readonly int    DAYS_PER_WEEK     = 7;                        // һ��7��
    		
            public static readonly int    TWIPS_PER_PIXEL   = 15;                       // ����ת����羵ı���

		    public static readonly int    NO                = 0;
		    public static readonly int	  YES               = 1;

            public static readonly int    FAILED            = -1;
        }
        
		// ���Ҹ�ʽ
        public struct FMT_MONEY
        {
            public static readonly string NORMAL            = "0.00";                   // �����ʾ
            public static readonly string STORAGE           = "0.0000";                 // �洢
        }
		
        public ComConst()
        {
        }
    }
}
