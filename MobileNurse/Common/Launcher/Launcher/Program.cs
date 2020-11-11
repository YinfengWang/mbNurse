using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Data;
using DevExpress.Skins;

namespace HISPlus
{
    static class Program
    {
        #region 变量
        private static string loginDll_Name = string.Empty;
        private static string loginFrm_Name = string.Empty;
        private static string mdiDll_Name = "DXApplication2.dll";
        private static string midFrm_Name = "DXApplication2.MDIFrm";
        private static string homePageName;
        private static string homePageDllName;
        private static Icon _applicationLogo;
        /// <summary>
        /// 应用程序图标
        /// </summary>
        public static Icon ApplicationLogo
        {
            get { return _applicationLogo; }
            set { _applicationLogo = value; }
        }
        #endregion

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);                              
            try
            {
                // 参数1: 用户名  参数2: 密码
                if (args != null && args.Length == 2)
                {
                    GVars.User.UserName = args[0];
                    GVars.User.PWD = args[1];
                }

                // 获取消息
                if (getMsgDict() == false)
                {
                    return;
                }

                // 设置应用程序属性
                if (getAppProperty() == false)
                {
                    GVars.Msg.Show();
                    return;
                }

                // 禁止多个实例运行
                if (GVars.App.OneInstance && GVars.App.IsRun())
                {
                    GVars.Msg.Show("WD001", GVars.App.Title);                                               // {0} 已经启动!
                    return;
                }

                // 登录窗口
                if (loginFrm_Name.Length > 0 && loginDll_Name.Length > 0)
                {
                    //DXApplication2.MdiFrm m = new DXApplication2.MdiFrm();                    
                    //Application.Run(m);

                    Form loginFrm = DllOperator.GetFormInDll(loginDll_Name, loginFrm_Name);
                    loginFrm.AutoScaleMode = AutoScaleMode.Dpi;
                    if (File.Exists(@"Resource\ICON\logo.ico"))
                    {
                        using (Bitmap b = new Bitmap(Bitmap.FromFile(@"Resource\ICON\logo.ico")))
                        {
                            _applicationLogo = Icon.FromHandle(b.GetHicon());
                        }
                        if (_applicationLogo != null)
                            loginFrm.Icon = _applicationLogo;
                    }
                    if (File.Exists(@"Resource\ICON\login.png"))
                        loginFrm.BackgroundImage = Image.FromFile(@"Resource\ICON\login.png");
                    loginFrm.ShowDialog();
                }

                // 如果登录成功, 打开主窗体
                if (GVars.App.Verified == true)
                {
                    // 预处理
                    LogFile.WriteLog(GVars.Msg.GetMsg("ID005") + ComConst.STR.TAB + GVars.User.UserName);   // 登录系统

                    // 设置本地时间
                    if (GVars.App.ResetLocalTime == true && GVars.OracleAccess != null)
                    {
                        App.SetSystemTime(GVars.OracleAccess.GetSysDate());
                    }

                    // 打开主窗口
                    if (midFrm_Name.Length > 0 && mdiDll_Name.Length > 0)
                    {
                        DevExpress.UserSkins.BonusSkins.Register();
                        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-CN");
                        //DevExpress.Utils.AppearanceObject.DefaultFont = new Font("Segoe UI", 8);
                        //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2007 Green");
                        //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Liquid Sky");
                        SkinManager.EnableFormSkins();
                        Form mdiFrm;
                        if (!string.IsNullOrEmpty(homePageName) && !string.IsNullOrEmpty(homePageDllName))
                            mdiFrm = DllOperator.GetFormInDll(mdiDll_Name, midFrm_Name, DllOperator.GetFormInDll(homePageDllName, homePageName));
                        else
                            mdiFrm = DllOperator.GetFormInDll(mdiDll_Name, midFrm_Name);
                        mdiFrm.AutoScaleMode = AutoScaleMode.Dpi;
                        if (File.Exists(@"Resource\ICON\MdiLogo.png"))
                            ((DXApplication2.MdiFrm)mdiFrm).Logo = Image.FromFile(@"Resource\ICON\MdiLogo.png");

                        if (_applicationLogo != null)
                            mdiFrm.Icon = _applicationLogo;

                        Application.Run(mdiFrm);
                    }

                    // 退出处理
                    LogFile.WriteLog(GVars.Msg.GetMsg("ID007") + ComConst.STR.TAB + GVars.User.UserName);   // 正常退出
                }
                else
                {
                    LogFile.WriteLog(GVars.Msg.GetMsg("ID006") + ComConst.STR.TAB + GVars.User.UserName);   // 登录失败
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteLog(GVars.Msg.GetMsg("ID008") + ComConst.STR.TAB + GVars.User.UserName);       // 异常退出
                MessageBox.Show(GVars.Msg.GetMsg("ED005") + ComConst.STR.CRLF + ex.Message);                // 系统错误
            }
        }


        /// <summary>
        /// 获取应用程序属性
        /// </summary>
        static private bool getAppProperty()
        {
            DataSet dsConfig = null;

            // 判断文件是否存在
            string appConfigFile = "Application.xml";
            appConfigFile = Path.Combine(Application.StartupPath, appConfigFile);

            if (File.Exists(appConfigFile) == false)
            {
                GVars.Msg.MsgId = "I0001";                               // 请设置 {0} 文件!
                GVars.Msg.MsgContent.Add(appConfigFile);

                dsConfig = createAppConfigSchema();
                dsConfig.WriteXml(appConfigFile, XmlWriteMode.WriteSchema);

                return false;
            }

            // 获取应用程序配置
            dsConfig = new DataSet();
            dsConfig.ReadXml(appConfigFile, XmlReadMode.ReadSchema);
            if (dsConfig.Tables.Count == 0)
            {
                GVars.Msg.MsgId = "E0001";                              // {0} 配置错误!
                GVars.Msg.MsgContent.Add(appConfigFile);
                return false;
            }

            foreach (DataRow dr in dsConfig.Tables[0].Rows)
            {
                string propertyName = dr["PropertyName"].ToString();
                string propertyValue = dr["PropertyValue"].ToString();
                string comment = dr["Comment"].ToString();

                switch (propertyName)
                {
                    case "TITLE":               // 应用程序名称
                        GVars.App.Title = propertyValue;
                        break;
                    case "NAME":                // 应用程序名称
                        GVars.App.Name = propertyValue;
                        break;
                    case "ONE_INSTANCE":        // 是否只运行一个实例
                        GVars.App.OneInstance = "1".Equals(propertyValue);
                        break;
                    case "RIGHT_ID":            // 权限开头码
                        GVars.App.Right = propertyValue;
                        break;
                    case "VERSION":             // 版本
                        GVars.App.Version = propertyValue;
                        break;
                    case "COPY_RIGHT":          // 版权
                        GVars.App.CopyRight = propertyValue;
                        break;
                    case "MAX_MDI_FORM":        // 是否最大化主窗体
                        GVars.App.MaxMdiFrm = ("1".Equals(propertyValue));
                        break;
                    case "QUESTION_EXIT":       // 退出前询问
                        GVars.App.QuestionExit = ("1".Equals(propertyValue));
                        break;
                    case "SYN_LOCAL_DATETIME":  // 是否用服务器时间更正本地时间
                        GVars.App.ResetLocalTime = "1".Equals(propertyValue);
                        break;
                    case "INI_FILE":            // INI文件
                        GVars.IniFile.FileName = Path.Combine(Application.StartupPath, propertyValue);
                        break;
                    case "LOGIN":               // 登录窗体
                        loginFrm_Name = propertyValue;
                        loginDll_Name = comment;
                        break;
                    case "MDI":                 // 主窗体
                        midFrm_Name = propertyValue;
                        mdiDll_Name = comment;
                        break;
                    case "HOMEPAGE":                 // 首页
                        homePageName = propertyValue;
                        homePageDllName = comment;
                        break;

                    default:
                        break;
                }
            }

            return true;
        }


        /// <summary>
        /// 创建应用程序配置的框架
        /// </summary>
        /// <returns></returns>
        static private DataSet createAppConfigSchema()
        {
            // 创建框架
            DataSet dsConfig = new DataSet("Application");

            DataTable dtApp = new DataTable("Application");
            dtApp.Columns.Add("PropertyName", Type.GetType("System.String"));
            dtApp.Columns.Add("PropertyValue", Type.GetType("System.String"));
            dtApp.Columns.Add("Comment", Type.GetType("System.String"));

            dsConfig.Tables.Add(dtApp);

            // 添加默认属性
            // 应用程序名称
            DataRow drNew = dtApp.NewRow();
            drNew["PropertyName"] = "TITLE";
            drNew["PropertyValue"] = "应用程序名称";
            dtApp.Rows.Add(drNew);

            // 应用程序简称
            drNew = dtApp.NewRow();
            drNew["PropertyName"] = "NAME";
            drNew["PropertyValue"] = "Not Set";
            dtApp.Rows.Add(drNew);

            // 权限ID
            drNew = dtApp.NewRow();
            drNew["PropertyName"] = "RIGHT_ID";
            drNew["PropertyValue"] = "2";
            dtApp.Rows.Add(drNew);

            // 版本
            drNew = dtApp.NewRow();
            drNew["PropertyName"] = "VERSION";
            drNew["PropertyValue"] = "版本1.0";
            dtApp.Rows.Add(drNew);

            // 版权
            drNew = dtApp.NewRow();
            drNew["PropertyName"] = "COPY_RIGHT";
            drNew["PropertyValue"] = "版权所有：北京天健源达科技有限公司";
            dtApp.Rows.Add(drNew);

            // 版权
            drNew = dtApp.NewRow();
            drNew["PropertyName"] = "QUESTION_EXIT";
            drNew["PropertyValue"] = "1";
            drNew["Comment"] = "退出前询问 是否退出";
            dtApp.Rows.Add(drNew);

            // 只运行一个实例
            drNew = dtApp.NewRow();
            drNew["PropertyName"] = "ONE_INSTANCE";
            drNew["PropertyValue"] = "1";
            drNew["Comment"] = "是否只运行一个实例 1:是 0:否";
            dtApp.Rows.Add(drNew);

            // INI文件
            drNew = dtApp.NewRow();
            drNew["PropertyName"] = "INI_FILE";
            drNew["PropertyValue"] = "MOBILE_NURSE.INI";
            drNew["Comment"] = "ini文件";
            dtApp.Rows.Add(drNew);

            // 用服务器时间更新本地时间
            drNew = dtApp.NewRow();
            drNew["PropertyName"] = "SYN_LOCAL_DATETIME";
            drNew["PropertyValue"] = "1";
            drNew["Comment"] = "是否用服务器时间更正本地时间";
            dtApp.Rows.Add(drNew);

            return dsConfig;
        }


        /// <summary>
        /// 获取消息字典
        /// </summary>
        /// <returns></returns>
        static private bool getMsgDict()
        {
            DataSet dsMsg = null;

            // 判断文件是否存在
            string msgFile = "Msg.xml";
            msgFile = Path.Combine(Application.StartupPath, msgFile);

            if (File.Exists(msgFile) == false)
            {
                MessageBox.Show("请设置 " + msgFile + " 文件!");

                dsMsg = createMsgDict();
                dsMsg.WriteXml(msgFile, XmlWriteMode.WriteSchema);

                return false;
            }

            // 获取消息字典
            dsMsg = new DataSet();
            dsMsg.ReadXml(msgFile, XmlReadMode.ReadSchema);
            if (dsMsg.Tables.Count == 0)
            {
                MessageBox.Show(msgFile + " 配置错误!");

                return false;
            }

            if (GVars.Msg == null)
                GVars.Msg = new Message();
            // 加载消息字典
            GVars.Msg.Clear();

            foreach (DataRow dr in dsMsg.Tables[0].Rows)
            {
                string msgId = dr["MSG_ID"].ToString();
                string msgContent = dr["MSG_CONTENT"].ToString();

                GVars.Msg.AddMsg(msgId, msgContent);
            }

            return true;
        }


        /// <summary>
        /// 创建消息字典的框架
        /// </summary>
        /// <returns></returns>
        static private DataSet createMsgDict()
        {
            // 创建框架
            DataSet dsMsg = new DataSet("MSG");

            DataTable dtMsg = new DataTable("MSG");
            dtMsg.Columns.Add("MSG_ID", Type.GetType("System.String"));
            dtMsg.Columns.Add("MSG_CONTENT", Type.GetType("System.String"));

            dsMsg.Tables.Add(dtMsg);

            // 添加默认属性
            // 应用程序名称
            DataRow drNew = dtMsg.NewRow();
            drNew["MSG_ID"] = "W01";
            drNew["MSG_CONTENT"] = "消息示例";
            dtMsg.Rows.Add(drNew);

            return dsMsg;
        }
    }
}