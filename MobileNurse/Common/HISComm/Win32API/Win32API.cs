using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace HISPlus
{
    /// <summary>
    /// WIN32 API�ӿ�
    /// </summary>
    public class Win32API
    {
        // Window��Ϣ
        public static int WM_LBUTTONDOWN    = 0x201;

        // ������Ϣ
        public static int EM_GETLINECOUNT   = 0xBA;                     // ��ȡ�ı���������
        public static int EM_GETSEL         = 0xB0;                     // ��ȡ�༭��һ��ѡ�����ݵ�������յ��ַ��ĸ���
        public static int EM_LINEINDEX      = 0xBB;                     // ÿһ�е�һ�ַ���ȫ���е��ַ����
        public static int EM_LINEFROMCHAR   = 0xC9;                     // ��ȡ�����������
        public static int EM_GETLINE        = 0xC4;                     // ��ȡһ�е�����
        
        [DllImport("user32.dll")]
        public static extern int SendMessageA(int hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int SendMessage(int hwnd, int wMsg, int wParam, StringBuilder lParam);
        
        // �޸ı�������ʱ��
        [ StructLayout( LayoutKind.Sequential )]
        public class SystemTime
        {
            public ushort year;
            public ushort month;
            public ushort dayofweek;
            public ushort day;
            public ushort hour;
            public ushort minute;
            public ushort second;
            public ushort milliseconds;
        }
        
        [ DllImport( "Kernel32.dll" )]
        public static extern Boolean SetSystemTime([In,Out] SystemTime st);

        // ��ȡ����ʱ��
        [StructLayout(LayoutKind.Sequential)]
        public struct LASTINPUTINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwTime;
        }

        [DllImport("user32.dll")]
        public static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        public static long GetLastInputTime()
        {
            LASTINPUTINFO vLastInputInfo = new LASTINPUTINFO();
            vLastInputInfo.cbSize = Marshal.SizeOf(vLastInputInfo);
            if (!GetLastInputInfo(ref vLastInputInfo)) return 0;
            return Environment.TickCount - (long)vLastInputInfo.dwTime;
        }

        public Win32API()
        {
        }
    }
}
