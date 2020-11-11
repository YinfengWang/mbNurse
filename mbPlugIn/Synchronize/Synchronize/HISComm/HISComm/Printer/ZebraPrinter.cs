//------------------------------------------------------------------------------------
//
//  ϵͳ����        : ͨ��
//  ��ϵͳ����      : ͨ��ģ��
//  ��������        : 
//  ����            : ZebraPrinter.cs
//  ���ܸ�Ҫ        : �����ӡ���Ľӿ�
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
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace HISPlus
{
    /// <summary>
	/// ZebraPrinter ��ժҪ˵����
	/// </summary>
    public class ZebraPrinter
    {
        // ������
        static int PRINT_NORMAL             = 1000;                     // ������ӡ
        static int PRINT_ERR_PARA           = 1001;                     // ��������
        static int PRINT_ERR_OVERFLOW       = 1002;                     // ������Ϣ������

        // ͨѶ��
        static int COMM_ERR_PORT_OPEN       = 2001;                     // ��ͨѶ�˿ڴ����δ���Ӵ�ӡ��
        static int COMM_ERR_PORT_WRITE      = 2002;                     // д�˿ڴ���
        static int COMM_ERR_PORT_CLOSE      = 2003;                     // �ر�ͨѶ�˿ڴ�

        // �ļ�����
        static int FILE_ERR_OPEN            = 3001;                     // �ļ��򿪴���
        static int FILE_ERR_READ            = 3002;                     // ���ļ�����
        static int FILE_ERR_WRITE           = 3003;                     // д�ļ�����
        static int FILE_ERR_FORMAT          = 3004;                     // �ļ����ͻ��ʽ����

        // ��ӡ������
        static int PRINTER_ERR_NO_PAPER     = 4001;                     // ��ӡ��ȱֽ
        static int PRINTER_ERR_NO_CARBON    = 4002;                     // ��ӡ��ȱ̼��
        static int PRINTER_ERR_HEAD_UP      = 4003;                     // ��ӡͷ̧��
        static int PRINTER_ERR_CUTTER       = 4004;                     // �е�����

        
        // ��ͨѶ�˿ں���
        // ����:	
        //      Long Port       ��ͨѶ�˿ڣ�1-����һ;2-���ڶ�;3-����һ;4-���ڶ���
        //      Long BaudSpeed  ��������(9600,19200,38400) 
        // ����ֵ:  
        //      0   ��������
        //      -1  �򿪶˿ڳ���
        // ˵��	���첽��ʽ�򿪶˿ڶ˿ڣ������ӳ٣���ͨѶ�˿�Ϊ���ڷ�ʽʱ��ͨѶ���ʲ�������. 
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_PortOpen(Int32 Port, Int32 BaudSpeed);


        // �ر�ͨѶ�˿ں���
        // ����ֵ
        //      0   ��������
        //     -1   �رն˿ڳ���
        // ˵��	��ӡ������������
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_PortClose();


        // ���ô�ӡ���������ʺ���
        // ������	Long BaudSpeed		���ô��ڵ�����ֵ(9600,19200,38400) 
        // ����ֵ	0 �ɹ�
        //    -1 ʧ��
        // ˵��	ֻ��ͨ�����������ô�ӡ���Ĵ������ʣ�����ֵ��Ϊ��9600,19200,38400 
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetPrinterComSpeed(Int32 BaudSpeed);


        // ���ô�ӡ�����Ⱥ���
        // ������	Long Dots	���ô�ӡ�����ȣ�203 ��300��
        // ����ֵ	0 �ɹ�
        //    -1 ʧ��
        // ˵��	203 ��ָ203.2dpi ���ȵĴ�ӡ����8dots/mm����300 ��ָ300dpi���ȵĴ�ӡ����11.81dots/mm��;��������ô�ӡ�����ȣ���Ĭ
        // ��Ϊ203.2dpi ��ӡ����
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetPrinterDots(Int32 Dots);


        // ���ô�ӡ����ӡ�ڶȺ���
        // ������	Long DarkValue	���ô�ӡ�ĺڶ�ֵ
        // ����ֵ	0 �ɹ�
        //    -1 ʧ��
        // ˵��	��ӡ�ڶ�Ϊ1-15 ������ֵ����ӡ��Ĭ��Ϊ10 
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetDarkness(Int32 DarkValue);


        // ���ô�ӡ���Ĵ�ӡ���ʺ���
        // ����	Long PrintRate	��ӡ����
        // ����ֵ	0 �ɹ�
        //    -1 ʧ��
        // ˵��	��ӡ���ʵĲ���ֵΪ1-10 ������ֵ��C4��Pd4 ��ӡ��Ĭ��Ϊ2 ���Ϊ3 
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetPrintSpeed(Int32 PrintRate);


        // ���ô�ӡ��������Բ�㺯��
        // ����	Long iX	��ӡ����ˮƽ��ʼλ��
        //    Long iY	��ӡ����ֱ��ʼλ��
        // ����ֵ	0 �ɹ�
        //    -1 ʧ��
        // ˵��	��ӡ��ʼλ��Ҫ����ʵ�ʱ�ǩ�ߴ���ȷ���������Ժ���Ϊ��λ��
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetPrintXY(Int32 iX, Int32 iY);


        // ���ô�ӡ����ӡ������ת180 �Ⱥ���
        // ����	 Long iDirect��ת��־��0-������ӡ��1-��ת180 �ȣ�
        // ����ֵ	0 �ɹ�
        //    -1 ʧ��
        // ˵��	ͨ�����ÿ�ʹ��ӡ��Ϣ��ת�������ȵķ���
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetPrintOrientation(Int32 iDirect);


        // ���ô�ӡ��ÿ�δ�ӡ�Ƿ�Ҫ���˺���
        // ����	Long iBack	���˱�־��0-���ˣ�Ĭ�ϣ���1-�����ˣ�
        // ����ֵ	0 �ɹ�
        //    -1 ʧ��
        // ˵��	�ú�����Ҫ���������е�ʱʹ��
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetPrintBack(Int32 iBack);


        // ���ô�ӡ����ӡÿ�ű�ǩ������ֽ�ߴ纯��
        // ����	Long iFeed��ֽ���ȣ�ȡֵ��Χ0-30�� 
        // ����ֵ	0 �ɹ�
        //    -1 ʧ��
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetPrintFeedDim(Int32 iFeed);


        // ���ñ�ǩֽ�ĳߴ纯��
        // ����	 Long iflag	ֽ������(0-��϶ֽ,1-�ڱ�ֽ) 
        //     Long iWidth	���ñ�ǩ�Ŀ��(0mm-150mm����Ϊ-1 ����Կ������) 
        //     Long iHigh	���ñ�ǩ�ĸ߶ȣ�0-150, ��Ϊ-1 ����Ը߶����úͷ�϶���ã�
        //     Long iGap      ���ñ�ǩ�ķ�϶��ȣ���ڱ�ĸ߶ȣ�
        // ����ֵ	0 �ɹ�
        //    -1 ʧ��
        // ˵��	���������÷�϶ֽ���ڱ�ֽ������ֽ���Ժ���Ϊ��λ,iWidth ��iHigh ����ͬʱΪ-1
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetPaperSize(Int32 iflag, Int32 iWidth, Int32 iHigh, Int32 iGap);


        // �����ӡ�����溯��
        // ����ֵ
        //      0 �ɹ�
        //      -1 ʧ��	
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetClear();


        // ��ʼ����ӡ�����������ô�ӡ���ڶȣ���ӡ���ʣ���ֽģʽ����
        // ����	Long PrintRate	��ӡ����
        //    Long DarkNess	��ӡ�ڶ�
        //    Long PrintMode	��ֽģʽ(1,�ؾ�;0, ���ؾ�) 
        // ����ֵ	0 �ɹ�
        //    -1 ʧ��
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_SetInitialize(Int32 PrintRate, Int32 DarkNess, Int32 PrintMode);


        // ������Ϣ����
        // ����ֵ	���ش���ż�������ʾ��Ϣ
        [DllImport("zebracom.dll")]
        public static extern string VS_GetLastError();


        // ����ͼƬ����ӡ���ڴ��к���
        // ����		char PicturePath	Ԥ���ص�ͼƬ·��
        //        Char AimName		���ص���ӡ��Ŀ���ļ���
        // ����ֵ	0 �ɹ�
        //    -1 ʧ��
        // ˵��	Intermec C4,Intermec Pd4 ��ӡ��ֻ������PCX �ļ�
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_DownPicture(string PicturePath, string AimName);


        //����	Long oXprint	����ַ���ˮƽλ�ã�X �����꣩
        //    Long oYprint	����ַ��Ĵ�ֱλ�ã�Y �����꣩
        //    Long YNRotation	�ַ��Ƿ���ת��0����;1��90;2-180;3-270;��
        //    Long FontStyle	�������ͣ�1��7 ����ͬ�����֣���ĸ;8 Ϊ�����ַ��� 
        //    Long oXMult	�ַ���ˮƽ��ȣ�1-8��
        //    Long oYMult	�ַ��Ĵ�ֱ��ȣ�1-10�� 
        //    Long YNImage	�ַ��Ƿ��б���(0���ޣ�1����) 
        //    Char Text	����ַ���Ϣ
        //����ֵ	0 �ɹ�
        //    -1 ʧ��
        //˵��	�����������ַ�����������ĸ�������ַ�
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_PrintCharacter(Int32 oXprint, Int32 oYprint, Int32 YNRotation, Int32 FontStyle, Int32 oXMult, Int32 oYMult, Int32 YNImage, string Text);


        //����	Long oXprint	����ַ���ˮƽλ�ã��������꣩
        //    Long oYprint	����ַ��Ĵ�ֱλ�ã��������꣩
        //    Long FontName	�������ƣ�0-����;1-����,2-����,3���飩 
        //    Long FontStyle	�������ͣ�0-������1-��б��2-�Ӵ֣�3-��б�Ӵ֣�4-���»��ߣ�5-�»�����б��6-�»��߼Ӵ֣�7-�»��߼Ӵ�
        //��б�� 
        //    Long FontSize	�����С��0-100��
        //    Long YNRotation	�ַ��Ƿ���ת��0������ת;1��180 �ȣ�
        //    Char Text	����ַ���Ϣ
        //����ֵ	0 �ɹ�
        //    -1 ʧ��
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_PrintChineseStr(Int32 oXprint, Int32 oYprint, Int32 FontName, Int32 FontStyle, Int32 FontSize, Int32 YNRotation, string Text);


        //��ӡ�����뺯��
        //����	Long oXprint	����ַ���ˮƽλ�ã�X �����꣩
        //    Long oYprint	����ַ��Ĵ�ֱλ�ã�Y �����꣩
        //    Long YNRotation	�ַ��Ƿ���ת��0����;1��90;2-180;3-270�� 
        //    Long BarStyle	�������ͣ�1-- Code128;2--Interleaved 2 of 5;3--Code 39 std. or extended 3;4--ean13;5-Code93;6--ucc/ean128;7--upcA8---upcE;)�� 
        //    Long BarNarrow	���뾫��(1-10) 
        //    Long BarWidth	������(1-15) 
        //    Long BarHigh	����߶�(1-100) 
        //    Long YNHuman	�Ƿ��ӡ��ʶ����Ϣ(1-��ӡ;0-����ӡ) 
        //    Char BarStr	������Ϣ
        //����ֵ	0 �ɹ�
        //    -1 ʧ��
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_PrintBarcode1(Int32 oXprint, Int32 oYprint, Int32 YNRotation, Int32 BarStyle, Int32 BarNarrow, Int32 BarWidth, Int32 BarHigh, Int32 YNHuman, string BarStr);


        // ��ӡֱ�ߺ���
        // ����	Long oXStart	��ʼ��X ������
        //    Long oYStart	��ʼ��Y ������
        //    Long HoriWidth	ˮƽ���(0mm---150mm) 
        //    Long VertWidth	��ֱ���(0mm---150mm) 
        // ����ֵ	0 �ɹ�
        //    -1 ʧ��
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_PrintLine(Int32 oXStart, Int32 oYStart, Int32 HoriWidth, Int32 VertWidth);


        //��һ��������ͼ����
        //����	Long oXStart	��ʼ��X ������
        //    Long oYStart	��ʼ��Y ������
        //    Long oXEnd	��ֹ��X ������
        //    Long oYEnd	��ֹ��Y ������
        //    Long LineDots	�����ߵĴ�ϸ 
        //����ֵ	0 �ɹ�
        //    -1 ʧ��
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_PrintBox(Int32 oXStart, Int32 oYStart, Int32 oXEnd, Int32 oYEnd, Int32 LineDots);


        // ��ӡ���ڴ�ӡ���ڴ��е�ͼƬ��Ϣ����
        // ����	Long oXprint	����ַ���ˮƽλ�ã�X �����꣩
        //    Long oYprint	����ַ��Ĵ�ֱλ�ã�Y �����꣩
        //    Char GrapName	ͼƬ����
        // ����ֵ	0 �ɹ�
        //    -1 ʧ��
        // ˵��ͼƬ����Ԥ�����ص���ӡ���ڴ��У�C4 ��ӡ��ֻ�ܴ�ӡPCX ��ʽ��ͼƬ 
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_PrintMemGraphics(Int32 oXprint, Int32 oYprint, string GrapName);


        //��ӡ�������ַ���Ϣ����
        //����	Long oXprint	����ַ���ˮƽλ�ã�X �����꣩
        //    Long oYprint	����ַ��Ĵ�ֱλ�ã�Y �����꣩
        //    Long XWidth	�ַ���Ϣ��ˮƽ��ȣ����ֽ�Ϊ��λ��
        //    Long YHigh	�ַ���Ϣ�Ĵ�ֱ�߶ȣ��Ե�Ϊ��λ��
        //    Char Text	�������ַ���Ϣ
        //����ֵ	0 �ɹ�
        //    -1 ʧ��
        //˵��	�������ͼƬ��Ϣ;���Text Ϊ��##�������ʾ��д���ַ���Ϣ,ֻд��������Ϣ; 
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_PrintBinary(Int32 oXprint, Int32 oYprint, Int32 XWidth, Int32 YHigh, string Text);


        //��ӡBmp ��ʽ��ͼƬ��Ϣ����
        //����	Long oXprint	����ַ���ˮƽλ�ã�X �����꣩
        //    Long oYprint	����ַ��Ĵ�ֱλ�ã�Y �����꣩
        //    Char GrapName	ͼƬ���� 
        //����ֵ	0 �ɹ�
        //    -1 ʧ��
        //˵��	ͼƬ��.BMP λͼ�ļ�������ҪԤ�����ص��������,ͼƬ����Ϊ�����ڼ�����еľ���·����
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_PrintBmp(Int32 oXprint, Int32 oYprint, string GrapName);


        //������ӡ����ӡ���ݺ���
        //����	Long sumLabel	Ҫ��ӡ�ı�ǩ������ֵΪ1-5000�� 
        //����ֵ	0 �ɹ�
        //    -1 ʧ��
        [DllImport("zebracom.dll")]
        public static extern Int32 VS_Print(Int32 sumLabel);


        /// <summary>
        /// ͨ���˿ںŻ�ȡ�˿�����
        /// </summary>
        /// <param name="portNo"></param>
        /// <returns></returns>
        public static string GetPortName(int portNo)
        {
            switch (portNo)
            {
                case 1: 
                    return "LPT1"; 
                
                case 2: 
                    return "LPT2"; 
                
                case 3:                     
                    return "COM1"; 
                
                case 4: 
                    return "COM2"; 
                
                default: 
                    throw new Exception("����Ĵ�ӡ�˿ں�!");
            }
        }


        /// <summary>
        /// ��ȡMaxiCode�����ӡ����
        /// </summary>
        /// <returns></returns>
        public static string GetMaxicodePrintCmd(int x, int y, string barcode)
        { 
            char    charDouble  = '"';
            char    charBlank   = ' ';
            string  strDouble   = new string(charDouble, 1);
            string  strBlank    = new string(charBlank, 50);

            return "b" + x.ToString() + "," + y.ToString() 
                 + ",M,m4," + strDouble + barcode + strDouble + ComConst.STR.CRLF
                 + "P1" + ComConst.STR.CRLF;
        }
    }
}
