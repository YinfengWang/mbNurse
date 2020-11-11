using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace HISPlus
{
    /// <summary>
    /// 文件操作API接口
    /// </summary>
    public class FileAPI
    {
        #region 接口声明
        [StructLayout(LayoutKind.Sequential)]
        public struct OVERLAPPED
        {
            int Internal;
            int InternalHigh;
            int Offset;
            int OffSetHigh;
            int hEvent;
        }


        [DllImport("kernel32.dll")]
        public static extern int CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            int dwShareMode,
            int lpSecurityAttributes,
            int dwCreationDisposition,
            int dwFlagsAndAttributes,
            int hTemplateFile
        );


        [DllImport("kernel32.dll")]
        public static extern bool WriteFile(
            int hFile,
            byte[] lpBuffer,
            int nNumberOfBytesToWrite,
            ref int lpNumberOfBytesWritten,
            ref OVERLAPPED lpOverlapped
        );


        [DllImport("kernel32.dll")]
        public static extern int ReadFile(
            int hFile,
            byte[] lpBuffer,
            int nNumberOfBytesToRead,
            ref int lpNumberOfBytesRead,
            ref OVERLAPPED lpOverlapped
        );


        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(int hObject);
        #endregion

        private int iHandle;

        public FileAPI()
        {
        }


        public bool Open(string fileName)
        {
            iHandle=CreateFile(fileName,0x40000000,0,0,3,0,0);

            return (iHandle != -1);
        }


        public bool Write(String Mystring)
        {
            int         i = 0;
            OVERLAPPED  x = new OVERLAPPED();
            byte[]      mybyte = System.Text.Encoding.Default.GetBytes(Mystring);

            return WriteFile(iHandle, mybyte, mybyte.Length, ref i, ref x);
        }


        public string Read(int nBytes)
        { 
            int         i = 0;
            OVERLAPPED  x = new OVERLAPPED();
            byte[]      mybyte = new byte[nBytes];

            ReadFile(iHandle, mybyte, nBytes, ref i, ref x);

            if (i > 0)
            {
                return System.Text.Encoding.Default.GetString(mybyte);
            }
            else
            {
                return string.Empty;
            }
        }


        public bool Close()
        {
            return CloseHandle(iHandle);
        }
    }
}
