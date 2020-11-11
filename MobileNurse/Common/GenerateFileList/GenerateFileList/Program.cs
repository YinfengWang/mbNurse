namespace GenerateFileList
{
    using System;
    using System.Windows.Forms;

    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UpdFileInfoFrm());
            Application.Run(new SyncFileInDirFrm());
        }
    }
}

