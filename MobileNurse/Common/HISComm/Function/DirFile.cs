//------------------------------------------------------------------------------------
//
//  系统名称        : 医院信息系统
//  子系统名称      : 通用模块
//  对象类型        : 
//  类名            : ComFunction.cs
//  功能概要        : 共通函数
//  作成者          : 付军
//  作成日          : 2007-01-19
//  版本            : 1.0.0.0
// 
//------------------< 变更历史 >------------------------------------------------------
//  变更日期        :
//  变更者          :
//  变更内容        :
//  版本            :
//------------------------------------------------------------------------------------

using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using Path = System.IO.Path;
using Dir = System.IO.Directory;
using Env = System.Environment;

namespace HISPlus
{
    /// <summary>
    /// 目录和文件操作类
    /// </summary>
    public class DirFile
    {
        public DirFile()
        {
        }


        #region 目录
        /// <summary>
        /// 拷贝目录
        /// </summary>
        /// <param name="dirInfoOld"></param>
        /// <param name="dirInfoNew"></param>
        public static void CopyDirectory(DirectoryInfo dirInfoOld, DirectoryInfo dirInfoNew)
        {
            string newDirFullName = Path.Combine(dirInfoNew.FullName, dirInfoOld.Name);

            // 如果目标目录不存在, 创建它
            if (Directory.Exists(newDirFullName) == false)
            {
                Directory.CreateDirectory(newDirFullName);
            }

            // 拷贝文件
            FileInfo[] arrOldFile = dirInfoOld.GetFiles();
            foreach (FileInfo fileInfo in arrOldFile)
            {
                CopyFile(fileInfo.FullName, Path.Combine(newDirFullName, fileInfo.Name));
            }

            // 拷贝子目录
            DirectoryInfo[] arrOldSubDir = dirInfoOld.GetDirectories();
            DirectoryInfo dirNew = new DirectoryInfo(newDirFullName);
            foreach (DirectoryInfo dirInfo in arrOldSubDir)
            {
                CopyDirectory(dirInfo, dirNew);
            }
        }


        /// <summary>
        /// 拷贝目录
        /// </summary>
        /// <param name="dirInfoOld"></param>
        /// <param name="dirInfoNew"></param>
        public static void CopyDirectory_NoSub(string dirSrc, string dirDest)
        {
            // 如果目标目录不存在, 创建它
            if (Directory.Exists(dirDest) == false)
            {
                Directory.CreateDirectory(dirDest);
            }

            DirectoryInfo dir = new DirectoryInfo(dirSrc);

            // 拷贝文件
            FileInfo[] arrFile = dir.GetFiles();
            foreach (FileInfo fileInfo in arrFile)
            {
                CopyFile(fileInfo.FullName, Path.Combine(dirDest, fileInfo.Name));
            }
        }


        /// <summary>
        /// 拷贝目录
        /// </summary>
        /// <param name="OldDirectoryStr"></param>
        /// <param name="NewDirectoryStr"></param>
        public static void CopyDirectory(string oldDir, string newDir)
        {
            DirectoryInfo dirInfoOld = new DirectoryInfo(oldDir);
            DirectoryInfo dirInfoNew = new DirectoryInfo(newDir);

            CopyDirectory(dirInfoOld, dirInfoNew);
        }


        /// <summary>
        /// 获取目录下的所有文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="trv"></param>
        /// <param name="parentNode"></param>
        /// <param name="level"></param>
        public static ArrayList GetDirFiles(string path)
        {
            ArrayList files = new ArrayList();

            GetDirFiles(path, ref files);

            return files;
        }


        /// <summary>
        /// 获取目录下的所有文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="trv"></param>
        /// <param name="parentNode"></param>
        /// <param name="level"></param>
        public static void GetDirFiles(string path, ref ArrayList files)
        {
            // 判断目录是否存在
            if (Directory.Exists(path) == false)
            {
                return;
            }

            // 获取子目录
            String[] arrDir = Directory.GetDirectories(path);

            foreach (string str in arrDir)
            {
                GetDirFiles(str, ref files);
            }

            // 获取文件
            String[] arrFile = Directory.GetFiles(path);

            foreach (string str in arrFile)
            {
                files.Add(str);
            }
        }
        #endregion


        #region 文件
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFile(string path)
        {
            // 判断文件是否存在
            if (File.Exists(path) == false)
            {
                return;
            }

            // 去掉文件的只读属性
            FileInfo fi = new FileInfo(path);
            if ((fi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                fi.Attributes = fi.Attributes & (~FileAttributes.ReadOnly);
            }

            // 删除文件
            File.Delete(path);
        }


        /// <summary>
        /// 尽力Copy文件, 忽略错误
        /// </summary>
        /// <param name="srcFile"></param>
        /// <param name="destFile"></param>
        public static void CopyFile(string srcFile, string destFile)
        {
            try
            {
                DeleteFile(destFile);

                File.Copy(srcFile, destFile, true);
            }
            catch { }
        }


        /// <summary>
        /// 获取文件的完整路径名称
        /// </summary>
        /// <param name="currentPath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFullFileName(string currentPath, string fileName)
        {
            string placeHolder = @"..\";

            while (fileName.StartsWith(placeHolder) == true)
            {
                currentPath = Path.GetDirectoryName(currentPath);
                fileName = fileName.Substring(placeHolder.Length);
            }

            return Path.Combine(currentPath, fileName);
        }

        public static Image GetImageFile(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;
            int pos = path.LastIndexOf(Path.DirectorySeparatorChar);
            if (pos > 0)
            {
                path = path.Substring(pos + 1, (path.Length - pos) - 1);
            }
            path = Path.Combine(Path.Combine(Path.Combine(getDllPath(), "Resource"), "ICON"), path);
            if (File.Exists(path))
                return Image.FromFile(path);
            return null;
        }


        /// <summary>
        ///     获取DLL路径
        /// </summary>
        /// <returns></returns>
        private static string getDllPath()
        {
            string dllFile = Assembly.GetExecutingAssembly().CodeBase;
            int length = dllFile.LastIndexOf("/", StringComparison.Ordinal);
            return dllFile.Substring(8, length - 8);
        }
        #endregion


        #region 共通
        /// <summary>
        /// 判断是不是文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 判断是不是目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsDir(string path)
        {
            if (Directory.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
