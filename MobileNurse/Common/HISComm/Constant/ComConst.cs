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
        /// <summary>
        /// �ַ���
        /// </summary>
        public struct STR
        {
            public static readonly string SQUARE_BRACKET_L = "[";                      // ������
            public static readonly string SQUARE_BRACKET_R = "]";                      // ������
            public static readonly string BRACE_LEFT = "{";                      // ������
            public static readonly string BRACE_RIGHT = "}";                      // ������
            public static readonly string BACKSLASH = "\\";                     // ��б��
            public static readonly string SLASH = "/";                      // б��

            /// <summary>
            /// ����
            /// </summary>
            public static readonly string VERTICAL_LINE = "|";                     

            public static readonly string POINT = ".";                      // ��
            public static readonly string SUSPENSION_POINTS = "...";                    // ʡ�Ժ�

            /// <summary>
            /// ���ߣ����л���
            /// </summary>
            public const string LINEATION = "-";

            /// <summary>
            /// �»���
            /// </summary>
            public static readonly string UnderLine = "_";

            /// <summary>
            /// =��
            /// </summary>
            public static readonly string EQUAL = "=";

            /// <summary>
            /// ��ʽ���Ʒ�.���з�
            /// </summary>
            public static readonly string CRLF = "\r\n";                   // �س����з�
            public static readonly string TAB = "\t";                     // �����
            /// <summary>
            /// // �հ׷�
            /// </summary>
            public static readonly string BLANK = " ";
            public static readonly string COLON = ":";                      // ð��
            /// <summary>
            /// ����
            /// </summary>
            public static readonly string COMMA = ",";
            public static readonly string SEMICOLON = ";";                      // �ֺ�
            public static readonly string SINGLE_QUOTATION = "'";                      // ������
            public static readonly string DOUBLE_QUOTATION = "''";                     // ˫����

            public static readonly string YEAR = "��";                     // ���ַ���
            public static readonly string MONTH = "��";                     // ���ַ���
            public static readonly string DAY = "��";                     // ���ַ���
        }

        /// <summary>
        /// ���ڸ�ʽ
        /// </summary>
        public struct FMT_DATE
        {
            /// <summary>
            /// �����ڸ�ʽ.yyyy-MM-dd HH:mm:ss
            /// </summary>
            public static readonly string LONG = "yyyy-MM-dd HH:mm:ss";
            
            public static readonly string LONG_COMPACT = "yyyyMMddHHmmss";         // ѹ�������ڸ�ʽ   DATE_COMPACT_FORMAT

            /// <summary>
            /// �����ڸ�ʽ.��ʾ���ȵ�����.yyyy-MM-dd HH:mm
            /// </summary>
            public static readonly string LONG_MINUTE = "yyyy-MM-dd HH:mm";

            /// <summary>
            /// �����ڸ�ʽ yyyy-MM-dd
            /// </summary>
            public static readonly string SHORT = "yyyy-MM-dd";

            /// <summary>
            /// yyyy��MM��dd��
            /// </summary>
            public static readonly string SHORT_CN = "yyyy��MM��dd��";

            /// <summary>
            /// yyyyMMdd
            /// </summary>
            public static readonly string SHORT_COMPACT = "yyyyMMdd";               // �����ڸ�ʽ

            /// <summary>
            /// HH:mm:ss
            /// </summary>
            public static readonly string TIME = "HH:mm:ss";              
            public static readonly string TIME_SHORT = "HH:mm";                  // ��ʱ���ʽ       DATE_TIME_SHORT_FORMAT
            public static readonly string TIME_COMPACT = "HHmmss";                 // ��ʱ���ʽ       DATE_TIME_NOCOLON
            public static readonly string TIME_ITEM = "00";                     // ʱ��ÿ�εĸ�ʽ   TIME_ITEM_FORMAT

            public static readonly string MONTH_DAY = "MM-dd";                  //                  DATE_MONTH_DAY_FORMAT
        }

        /// <summary>
        /// ��������
        /// </summary>
        public struct VAL
        {
            /// <summary>
            /// һ��12����
            /// </summary>
            public static readonly int MONTHS_PER_YEAR = 12;                      
            /// <summary>
            /// һ��24Сʱ
            /// </summary>
            public static readonly int HOURS_PER_DAY = 24;                       
            /// <summary>
            /// һ��7��
            /// </summary>
            public static readonly int DAYS_PER_WEEK = 7;                   

            public static readonly int TWIPS_PER_PIXEL = 15;                       // ����ת����羵ı���

            public static readonly int NO = 0;
            public static readonly int YES = 1;

            public static readonly int FAILED = -1;
        }

        /// <summary>
        /// ���Ҹ�ʽ
        /// </summary>
        public struct FMT_MONEY
        {
            public static readonly string NORMAL = "0.00";                   // �����ʾ
            public static readonly string STORAGE = "0.0000";                 // �洢
        }

        public ComConst()
        {
        }
    }
}
