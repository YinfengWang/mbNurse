//------------------------------------------------------------------------------------
//
//  ϵͳ����        : ҽԺ��Ϣϵͳ
//  ��ϵͳ����      : ͨ��ģ��
//  ��������        : 
//  ����            : ComFunction.cs
//  ���ܸ�Ҫ        : ��ͨ����
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
    /// Ŀ¼���ļ�������
    /// </summary>
    public class DirFile
    {
        public DirFile()
        {
        }


        #region Ŀ¼
        /// <summary>
        /// ����Ŀ¼
        /// </summary>
        /// <param name="dirInfoOld"></param>
        /// <param name="dirInfoNew"></param>
        public static void CopyDirectory(DirectoryInfo dirInfoOld, DirectoryInfo dirInfoNew)
        {
            string newDirFullName = Path.Combine(dirInfoNew.FullName, dirInfoOld.Name);

            // ���Ŀ��Ŀ¼������, ������
            if (Directory.Exists(newDirFullName) == false)
            {
                Directory.CreateDirectory(newDirFullName);
            }

            // �����ļ�
            FileInfo[] arrOldFile = dirInfoOld.GetFiles();
            foreach (FileInfo fileInfo in arrOldFile)
            {
                CopyFile(fileInfo.FullName, Path.Combine(newDirFullName, fileInfo.Name));
            }

            // ������Ŀ¼
            DirectoryInfo[] arrOldSubDir = dirInfoOld.GetDirectories();
            DirectoryInfo dirNew = new DirectoryInfo(newDirFullName);
            foreach (DirectoryInfo dirInfo in arrOldSubDir)
            {
                CopyDirectory(dirInfo, dirNew);
            }
        }


        /// <summary>
        /// ����Ŀ¼
        /// </summary>
        /// <param name="dirInfoOld"></param>
        /// <param name="dirInfoNew"></param>
        public static void CopyDirectory_NoSub(string dirSrc, string dirDest)
        {
            // ���Ŀ��Ŀ¼������, ������
            if (Directory.Exists(dirDest) == false)
            {
                Directory.CreateDirectory(dirDest);
            }

            DirectoryInfo dir = new DirectoryInfo(dirSrc);

            // �����ļ�
            FileInfo[] arrFile = dir.GetFiles();
            foreach (FileInfo fileInfo in arrFile)
            {
                CopyFile(fileInfo.FullName, Path.Combine(dirDest, fileInfo.Name));
            }
        }


        /// <summary>
        /// ����Ŀ¼
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
        /// ��ȡĿ¼�µ������ļ�
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
        /// ��ȡĿ¼�µ������ļ�
        /// </summary>
        /// <param name="path"></param>
        /// <param name="trv"></param>
        /// <param name="parentNode"></param>
        /// <param name="level"></param>
        public static void GetDirFiles(string path, ref ArrayList files)
        {
            // �ж�Ŀ¼�Ƿ����
            if (Directory.Exists(path) == false)
            {
                return;
            }

            // ��ȡ��Ŀ¼
            String[] arrDir = Directory.GetDirectories(path);

            foreach (string str in arrDir)
            {
                GetDirFiles(str, ref files);
            }

            // ��ȡ�ļ�
            String[] arrFile = Directory.GetFiles(path);

            foreach (string str in arrFile)
            {
                files.Add(str);
            }
        }
        #endregion


        #region �ļ�
        /// <summary>
        /// ɾ���ļ�
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFile(string path)
        {
            // �ж��ļ��Ƿ����
            if (File.Exists(path) == false)
            {
                return;
            }

            // ȥ���ļ���ֻ������
            FileInfo fi = new FileInfo(path);
            if ((fi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                fi.Attributes = fi.Attributes & (~FileAttributes.ReadOnly);
            }

            // ɾ���ļ�
            File.Delete(path);
        }


        /// <summary>
        /// ����Copy�ļ�, ���Դ���
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
        /// ��ȡ�ļ�������·������
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
        ///     ��ȡDLL·��
        /// </summary>
        /// <returns></returns>
        private static string getDllPath()
        {
            string dllFile = Assembly.GetExecutingAssembly().CodeBase;
            int length = dllFile.LastIndexOf("/", StringComparison.Ordinal);
            return dllFile.Substring(8, length - 8);
        }
        #endregion


        #region ��ͨ
        /// <summary>
        /// �ж��ǲ����ļ�
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
        /// �ж��ǲ���Ŀ¼
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
