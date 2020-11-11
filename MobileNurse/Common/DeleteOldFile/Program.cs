using System;
using System.IO;
using System.Threading;

namespace DeleteOldFile
{
    internal static class Program
    {
        private static bool DeleteOld()
        {
            Exception exception;
            bool flag = true;
            try
            {                
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                FileInfo[] files = new DirectoryInfo(baseDirectory).GetFiles();
                for (int i = 0; i < files.Length; i++)
                {
                    try
                    {
                        FileInfo info2 = files[i];
                        if (info2.Name.StartsWith("new_"))
                        {
                            Console.WriteLine("处理文件: " + info2.Name);
                            Thread.Sleep(10);
                            string path = Path.Combine(baseDirectory, info2.Name.Substring(4));
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }
                            File.Copy(info2.FullName, path);
                            File.Delete(info2.FullName);
                        }
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        flag = false;
                        Console.WriteLine(exception.Message);
                    }
                }
                if (flag)
                {
                    Console.WriteLine("更新完成!");
                    Thread.Sleep(10);
                    return flag;
                }
                Console.WriteLine("更新失败! 再次偿试更新");
                Thread.Sleep(10);
            }
            catch (Exception exception2)
            {
                exception = exception2;
                flag = false;
                Console.WriteLine(exception.Message);
                Thread.Sleep(10);
            }
            return flag;
        }

        private static void Main(string[] args)
        {
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(200);
                    if (DeleteOld())
                    {
                        return;
                    }
                    Thread.Sleep(0x7d0);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Thread.Sleep(0x7d0);
            }
        }
    }
}

