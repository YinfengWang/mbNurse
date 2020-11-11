using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace HISPlus
{
    /// <summary>
    /// WIN32 API接口
    /// </summary>
    public class Win32API
    {
        // Window消息
        public static int WM_LBUTTONDOWN    = 0x201;

        // 发送消息
        public static int EM_GETLINECOUNT   = 0xBA;                     // 获取文本框总行数
        public static int EM_GETSEL         = 0xB0;                     // 获取编辑框一段选定内容的起点与终点字符的个数
        public static int EM_LINEINDEX      = 0xBB;                     // 每一行第一字符在全文中的字符序号
        public static int EM_LINEFROMCHAR   = 0xC9;                     // 获取光标所在行数
        public static int EM_GETLINE        = 0xC4;                     // 获取一行的内容
        
        [DllImport("user32.dll")]
        public static extern int SendMessageA(int hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int SendMessage(int hwnd, int wMsg, int wParam, StringBuilder lParam);
        
        // 修改本地日期时间
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

        // 获取空闲时间
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
