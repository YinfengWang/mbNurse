// ***********************************************************************
// Assembly         : BodyTemp
// Author           : LPD
// Created          : 12-04-2015
//
// Last Modified By : LPD
// Last Modified On : 12-21-2015
// ***********************************************************************
// <copyright file="Program.cs" company="心医国际">
//     Copyright (c) 心医国际. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

/// <summary>
/// The BodyTemp namespace.
/// </summary>
namespace BodyTemp
{
    /// <summary>
    /// Class Program.
    /// </summary>
    static class Program
    {

        /// <summary>
        /// Shows the window asynchronous.
        /// </summary>
        /// <param name="hWnd">The authentication WND.</param>
        /// <param name="cmdShow">The command show.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(int hWnd, int cmdShow);

        /// <summary>
        /// Finds the window.
        /// </summary>
        /// <param name="lpClassName">Name of the lp class.</param>
        /// <param name="lpWindowName">Name of the lp window.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// Sets the foreground window.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("User32")]
        static extern int SetForegroundWindow(int hwnd);

        /// <summary>
        /// The arguments w_ normal
        /// </summary>
        private const int SW_NORMAL = 1; //正常弹出窗体 

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Process currentProcess = Process.GetCurrentProcess();

            Mutex mutex = new Mutex(false, "GTranslate");
            bool Running = !mutex.WaitOne(0, false);
            if (!Running)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                BonusSkins.Register();
                SkinManager.EnableFormSkins();
                UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
                Application.Run(new MainForm());
            }
            else
            {
                //MessageBox.Show("程序已经运行,禁止重复运行！" + Process.GetProcessesByName(currentProcess.ProcessName)[0].MainWindowTitle, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);  
                int WINDOW_HANDLER = FindWindow(null, "Google Translate   V 1.0  BY: Z.A.CHEN");   //程序标题或类名二选1  
                if (WINDOW_HANDLER > 0)
                {
                    //MessageBox.Show(WINDOW_HANDLER.ToString());  
                    ShowWindowAsync(WINDOW_HANDLER, SW_NORMAL);
                    SetForegroundWindow(WINDOW_HANDLER);
                }
                Environment.Exit(0);
            }  

            
            //Application.Run(new MainForm());
        }
    }
}
