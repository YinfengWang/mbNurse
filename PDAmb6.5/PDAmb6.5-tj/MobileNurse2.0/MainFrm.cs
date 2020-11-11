using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

using Symbol;
using Symbol.Barcode;
using HISPlus.FORMS;
using System.Collections;

namespace HISPlus
{
    public partial class MainFrm : Form
    {
        #region 变量
        private PatientNavigator patNavigator = new PatientNavigator();     // 病人导航

        // 自动更新有关
        private AutoUpdateWebSrv.AutoUpdateWebSrv autoUpdSrv = null;

        private bool tipShowed = false;            // 已经提示过要更新了
        private string appPath = string.Empty;
        public static bool _exit = false;
        private string updateExe = string.Empty;
        private string updFlagFile = string.Empty;
        //private string                  logMsg          = string.Empty;

        private bool questExit = true;             // 退出前询问
        private bool needUpdate = false;            // 是否需要升级
        private int exitCounter = 0;                // 点击退出次数, 5次时, 弹出询问对话框
        private DateTime dtPreClick = DateTime.Now;     // 前一次点击退出的时间
        #endregion

        Form frmShow = null;//子界面
        public MainFrm()
        {
            InitializeComponent();

        }


        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrm_Load(object sender, EventArgs e)
        {
            try
            {
                this.timer1.Enabled = true;
                picBackground.SizeMode = PictureBoxSizeMode.StretchImage;
                CreateMenuItem();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 时钟消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {


            try
            {
                Cursor.Current = Cursors.WaitCursor;
                timer1.Enabled = false;
                timer2.Enabled = false;

                initFrm();

                initDisp();

                startThread();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                enableDisp(false);

                //自动登陆
                mnuLogin_Click(null, null);
                Cursor.Current = Cursors.Default;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            try
            {
                timer3.Enabled = false;
                //MessageBox.Show(logMsg);
                this.Text = "移动护理";
                if (GVars.User.Name.Length > 0)
                {
                    this.Text += " (" + GVars.User.Name + ")";
                }

                if (tipShowed == false && File.Exists(Path.Combine(appPath, "new_SDMN.exe")) == true)
                {
                    tipShowed = true;
                    MessageBox.Show("有最新程序, 请退出程序, 以完成更新!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                timer3.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }


        /// <summary>
        /// 背景图单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBackground_MouseUp(object sender, MouseEventArgs e)
        {

            try
            {
                string funcName = getFuncNameByPos(e.X, e.Y).Trim();
                if (funcName.Length > 0)
                {
                    RunFunc(funcName);
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 扫描器 读取通知 事件的委托程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scanReader_ReadNotify(object sender, EventArgs e)
        {

#if SCANNER
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // 获取扫描的条码
                string barcode = GVars.ScanReaderBuffer.Text.Trim();

                // 如果不包含空格, 表示是病人的腕带
                if (barcode.IndexOf(ComConst.STR.BLANK) < 0 && barcode.IndexOf("T") < 0)
                {
                    if (patNavigator.ScanedPatient(barcode) == false)
                        return;
                    //GVars.Msg.Show("W00005");   // 该病人不存在!
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                GVars.ScanReader.Actions.Read(GVars.ScanReaderBuffer);              // 再次开始等待扫描
            }
#endif
        }


        /// <summary>
        /// 窗体退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrm_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                if (questExit == true && needUpdate == false)
                {
                    if (DateTime.Now.Subtract(dtPreClick).TotalSeconds > 2)
                    {
                        exitCounter = 0;
                    }

                    exitCounter++;
                    if (exitCounter >= 1)
                    {
                        GVars.Msg.MsgId = "Q00003";                             // "您确认要退出本系统吗? ";
                        if (GVars.Msg.Show() != DialogResult.Yes)
                        {
                            e.Cancel = true;
                        }
                        exitCounter = 0;
                    }
                    else
                    {
                        e.Cancel = true;
                    }

                    dtPreClick = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrm_Closed(object sender, EventArgs e)
        {
            try
            {
                // 终止线程   
                endThread();

                // 扫描器释放

                #region  扫描器释放

                GVars.ScanReader.Actions.Flush();	    // 终止扫描器所有的等待读取
                GVars.ScanReader.Actions.Disable();	    // 销毁扫描器对象
                GVars.ScanReader.Dispose();			    // 销毁对象
                GVars.ScanReaderBuffer.Dispose();		// 销毁Buffer

                #endregion

                this.Tag = ComConst.VAL.NO;

                this.Close();

                // 启动更新程序
                string exeFile = Path.Combine(appPath, "Rename.exe");
                string mdiExeFile = Path.Combine(appPath, "new_SDMN.exe");
                if (File.Exists(exeFile) == true && File.Exists(mdiExeFile) == true)
                {
                    System.Diagnostics.Process.Start(exeFile, string.Empty);
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion


        #region 共通函数
        /// <summary>
        /// 初始化窗体变量
        /// </summary>
        private void initFrm()
        {

            appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());

            // 后台线程
            GVars.sqlceLocal.ServerIp = GVars.App.ServerIp;
            GVars.sqlceLocal.WardCode = GVars.App.DeptCode;

            GVars.WebUser.Url = UrlIp.ChangeIpInUrl(GVars.WebUser.Url, GVars.App.ServerIp);
            GVars.PatDbI.SetServerIp(GVars.App.ServerIp);


            // 设置病人导航按钮
            patNavigator.BtnPrePatient = this.btnPrePatient;
            patNavigator.BtnCurrentPatient = this.btnCurrPatient;
            patNavigator.BtnNextPatient = this.btnNextPatient;
            patNavigator.BtnPatientList = this.btnListPatient;

            patNavigator.MenuItemPatient = mnuPatient;

            // 扫描器设置


            #region 扫描器设置
            try
            {
                GVars.ScanReader = new Reader();
                GVars.ScanReaderBuffer = new ReaderData(ReaderDataTypes.Text, ReaderDataLengths.DefaultText);
                GVars.ScanReader.ReadNotify += new EventHandler(scanReader_ReadNotify);     // 为扫描器对象绑定 读取通知 事件

                GVars.ScanReader.Actions.Enable();                                          // 激活扫描器
                GVars.ScanReader.Actions.Read(GVars.ScanReaderBuffer);                      // 等待读取条码

            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }

            #endregion

            // 获取应用程序参数
            GVars.Parameters = GVars.sqlceLocal.SelectData("SELECT * FROM APP_CONFIG");

            // 获取病人列表
            //GVars.DsPatient = GVars.PatDbI.GetPatientList();
            GVars.DsPatient = GVars.PatDbI.GetPatientInfo_Filter("全部");

            if (GVars.DsPatient != null && GVars.DsPatient.Tables.Count > 0
                && GVars.DsPatient.Tables[0].Rows.Count > 0)
            {
                DataRow dr = GVars.DsPatient.Tables[0].Rows[0];

                GVars.Patient.ID = dr["PATIENT_ID"].ToString();
                GVars.Patient.VisitId = dr["VISIT_ID"].ToString();
                GVars.Patient.Name = dr["NAME"].ToString();

                GVars.PatIndex = 0;
            }
        }


        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {
            // 显示第一个病人
            patNavigator.SetPatientButtons();
        }


        /// <summary>
        /// 激活界面
        /// </summary>
        private void enableDisp(bool enabled)
        {

            if (GVars.User.Name.Equals("ADMIN") == true && enabled == true)
            {
                mnuLogin.Enabled = !enabled;                     // 用户登录
                mnuExit.Enabled = enabled;                      // 退出

                mnuSetting.Enabled = enabled;                      // 系统设置
                mnuSysStatus.Enabled = enabled;                      // 系统状态

                // 如果是实际运行   
                if (false)
                {
                    return;
                }
            }

            // 菜单
            mnuLogin.Enabled = !enabled;                         // 用户登录
            mnuExit.Enabled = enabled;                          // 退出
            mnuChangePwd.Enabled = enabled;                          // 修改密码

            mnuSetting.Enabled = (GVars.User.Name.Equals("ADMIN"));// 系统设置
            mnuSysStatus.Enabled = enabled;                          // 系统状态

            mnuPatient.Enabled = enabled;                          // 当前病人

            // 按钮
            btnListPatient.Enabled = enabled;
            btnPrePatient.Enabled = enabled;
            btnCurrPatient.Enabled = enabled;
            btnNextPatient.Enabled = enabled;

            picBackground.Enabled = enabled;
        }


        /// <summary>
        /// 通过位置获取功能名称
        /// </summary>
        /// <returns></returns>
        private string getFuncNameByPos(int x, int y)
        {
            // 设定坐标范围


            #region   分辨率 为 240*320 时
            //Rectangle rect_Orders = new Rectangle(17, 20, 37, 60);    // 医嘱
            //Rectangle rect_Nurse = new Rectangle(73, 20, 32, 60);    // 护理
            //Rectangle rect_Execute = new Rectangle(123, 20, 35, 60);   // 执行
            //Rectangle rect_Handoff = new Rectangle(178, 20, 33, 60);   // 交接班

            //Rectangle rect_EvalDays = new Rectangle(17, 93, 37, 63);    // 每日
            //Rectangle rect_EvalInp = new Rectangle(73, 93, 32, 63);    // 入院
            //Rectangle rect_Exam = new Rectangle(123, 93, 35, 63);   // 检查
            //Rectangle rect_Lab = new Rectangle(178, 93, 33, 63);   // 检验

            //Rectangle rect_Oper = new Rectangle(17, 166, 37, 57);   // 手术
            //Rectangle rect_Edu = new Rectangle(73, 166, 32, 57);   // 健康教育

            //Rectangle rect_Specim = new Rectangle(123, 166, 35, 63);  // 标本管理

            #endregion

            #region  分辨率  为  480*800 时

            System.Drawing.Rectangle rect_Orders = new System.Drawing.Rectangle(20, 41, 84, 92);    // 医嘱
            System.Drawing.Rectangle rect_Execute = new System.Drawing.Rectangle(135, 41, 84, 92);   // 执行单
            System.Drawing.Rectangle rect_Nurse = new System.Drawing.Rectangle(251, 41, 84, 92);   // 护理信息
            System.Drawing.Rectangle rect_Specim = new System.Drawing.Rectangle(365, 41, 84, 92);   // 护理巡视

            System.Drawing.Rectangle rect_Handoff = new System.Drawing.Rectangle(20, 192, 84, 92);    // 每日评估  
            System.Drawing.Rectangle rect_EvalDays = new System.Drawing.Rectangle(135, 192, 84, 92);    // 入院评估   
            System.Drawing.Rectangle rect_EvalInp = new System.Drawing.Rectangle(251, 192, 84, 92);   // 检查结果
            System.Drawing.Rectangle rect_Exam = new System.Drawing.Rectangle(365, 192, 84, 92);   //检验结果

            System.Drawing.Rectangle rect_Edu = new System.Drawing.Rectangle(20, 344, 84, 92);   // 健康教育
            System.Drawing.Rectangle rect_Oper = new System.Drawing.Rectangle(135, 344, 84, 92);   //手术核对 

            System.Drawing.Rectangle rect_Lab = new System.Drawing.Rectangle(251, 344, 84, 92);  //标本管理

            #endregion

            Point ptMouse = new Point(x, y);

            // 获取功能名称


            if (rect_Orders.Contains(ptMouse)) { return "医嘱单"; }
            if (rect_Nurse.Contains(ptMouse)) { return "护理信息"; }
            if (rect_Execute.Contains(ptMouse)) { return "执行单"; }
            if (rect_Handoff.Contains(ptMouse)) { return "每日评估"; }
            if (rect_EvalDays.Contains(ptMouse)) { return "入院评估"; }
            if (rect_EvalInp.Contains(ptMouse)) { return "检查结果"; }
            if (rect_Exam.Contains(ptMouse)) { return "检验结果"; }
            if (rect_Lab.Contains(ptMouse)) { return "标本管理"; }
            if (rect_Oper.Contains(ptMouse)) { return "手术核对"; }
            if (rect_Edu.Contains(ptMouse)) { return "健康教育"; }
            if (rect_Specim.Contains(ptMouse)) { return "护理巡视"; }

            return string.Empty;
        }


        /// <summary>
        /// 运行指定功能
        /// </summary>
        public void RunFunc(string funcName)
        {

            if (funcName.Trim().Length == 0)
            {
                return;
            }

            // 扫描函数
            ScanReader_ReadNotify scan_ReadNotify = null;


            try
            {
                //Form frmShow = null;
                frmShow = null;//子界面
                // 医嘱
                if ("医嘱单".Equals(funcName))
                {
                    OrdersFrm frm = new OrdersFrm();

                    scan_ReadNotify = new ScanReader_ReadNotify(frm.ScanReader_ReadNotify);
                    frmShow = frm;
                }

                // 护理
                if ("护理信息".Equals(funcName))
                {
                    VitalRecorderFrm frm = new VitalRecorderFrm();

                    scan_ReadNotify = new ScanReader_ReadNotify(frm.ScanReader_ReadNotify);
                    frmShow = frm;
                }

                // 执行单
                if ("执行单".Equals(funcName))
                {
                    OrdersExecuteFrm frm = new OrdersExecuteFrm();
                    frmShow = frm;

                    scan_ReadNotify = new ScanReader_ReadNotify(frm.ScanReader_ReadNotify);
                }

                // 
                if ("交接班".Equals(funcName))
                {
                    NurseWorkHandoffFrm frm = new NurseWorkHandoffFrm();
                    frmShow = frm;

                    scan_ReadNotify = new ScanReader_ReadNotify(frm.ScanReader_ReadNotify);
                }

                // 每日评估
                if ("每日评估".Equals(funcName))
                {
                    ItemValueInputFrm frm = new ItemValueInputFrm();
                    frm.DictID = "01";
                    //frm.OneTime = false;
                    frm.Text = "每日护理评估单";

                    scan_ReadNotify = new ScanReader_ReadNotify(frm.ScanReader_ReadNotify);

                    frmShow = frm;
                }

                // 入院评估
                if ("入院评估".Equals(funcName))
                {
                    ItemValueInputFrm frm = new ItemValueInputFrm();
                    frm.DictID = "02";
                    //frm.OneTime = true;
                    frm.Text = "住院护理评估单";

                    scan_ReadNotify = new ScanReader_ReadNotify(frm.ScanReader_ReadNotify);

                    frmShow = frm;
                }

                // 检查
                if ("检查结果".Equals(funcName))
                {
                    ExamFrm frm = new ExamFrm();
                    frmShow = frm;

                    scan_ReadNotify = null;//new ScanReader_ReadNotify(frm.ScanReader_ReadNotify);
                }

                // 检验
                if ("检验结果".Equals(funcName))
                {
                    LabTestFrm frm = new LabTestFrm();
                    frmShow = frm;

                    scan_ReadNotify = null;//new ScanReader_ReadNotify(frm.ScanReader_ReadNotify);
                }

                // 手术
                if ("手术核对".Equals(funcName))
                {
                    OperationFrm frm = new OperationFrm();
                    frmShow = frm;

                    scan_ReadNotify = new ScanReader_ReadNotify(frm.ScanReader_ReadNotify);
                }

                // 健康教育
                if ("健康教育".Equals(funcName))
                {
                    HealthEduFrm frm = new HealthEduFrm();
                    frmShow = frm;
                    scan_ReadNotify = new ScanReader_ReadNotify(frm.ScanReader_ReadNotify);
                }

                // 标本管理
                if ("标本管理".Equals(funcName))
                {
                    SpecimentFrm frm = new SpecimentFrm();
                    frmShow = frm;
                    scan_ReadNotify = new ScanReader_ReadNotify(frm.ScanReader_ReadNotify);
                }

                // 巡视
                if ("护理巡视".Equals(funcName))
                {
                    XunShiFrm frm = new XunShiFrm();
                    frmShow = frm;
                    scan_ReadNotify = new ScanReader_ReadNotify(frm.ScanReader_ReadNotify);
                }


                // 显示功能
                if (frmShow != null)
                {
#if SCANNER
                    if (scan_ReadNotify != null)
                    {
                        GVars.ScanReader.ReadNotify -= new EventHandler(scanReader_ReadNotify);
                        GVars.ScanReader.ReadNotify += new EventHandler(scan_ReadNotify);
                    }
#endif

                    frmShow.ShowDialog();

                    // 设置病人按钮
                    patNavigator.SetPatientButtons();

#if SCANNER
                    if (scan_ReadNotify != null)
                    {
                        GVars.ScanReader.ReadNotify -= new EventHandler(scan_ReadNotify);
                        GVars.ScanReader.ReadNotify += new EventHandler(scanReader_ReadNotify);
                    }
#endif

                    scan_ReadNotify = null;

                    // 显示导航的功能
                    if (frmShow.Tag != null && frmShow.Tag.ToString().Length > 0)
                    {
                        RunFunc(frmShow.Tag.ToString());
                    }
                }
            }
            finally
            {
                // 释放扫描接口
                scan_ReadNotify = null;
            }
        }
        #endregion


        #region 菜单
        /// <summary>
        /// 菜单 [登录]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuLogin_Click(object sender, EventArgs e)
        {
            try
            {
                LoginForm frmLogin = new LoginForm();
                frmLogin.ShowDialog();

                if (GVars.App.Verified == true)
                {
                    enableDisp(true);

                    mnuLogin.Enabled = false;
                    mnuExit.Enabled = true;
                }

                inputPanel1.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }


        /// <summary>
        /// 菜单 [退出]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuExit_Click(object sender, EventArgs e)
        {
            try
            {
                GVars.User.Name = string.Empty;

                enableDisp(false);

                mnuLogin.Enabled = true;
                mnuExit.Enabled = false;

                DeleteDefaultUserFile();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        /// 删除默认用户的文件
        /// </summary>
        private void DeleteDefaultUserFile()
        {
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
            string configFile = Path.Combine(appPath, "DefaultUser.txt");
            if (File.Exists(configFile))
            {
                File.Delete(configFile);
            }
        }

        /// <summary>
        /// 菜单 [修改密码]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuChangePwd_Click(object sender, EventArgs e)
        {
            try
            {
                ChangePwd changePwd = new ChangePwd();
                changePwd.ShowDialog();

                inputPanel1.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 菜单 [系统设置]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSetting_Click(object sender, EventArgs e)
        {
            try
            {
                SettingFrm showFrm = new SettingFrm();
                showFrm.ShowDialog();

                inputPanel1.Enabled = false;

                if (showFrm.Saved == true)
                {
                    questExit = false;

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 菜单 [系统状态]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSysStatus_Click(object sender, EventArgs e)
        {
            try
            {
                SysStatusFrm showFrm = new SysStatusFrm();
                showFrm.ShowDialog();

                inputPanel1.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        #endregion


        #region 后台线程
        /// <summary>
        /// 开始线程
        /// </summary>
        public void startThread()
        {
            if (GVars.Demo == true)
            {
                return;
            }

            GVars.sqlceLocal.StartDownLoad();
            GVars.sqlceLocal.StartUpLoad();

            // 开始检查自动更新
            Thread thread = new Thread(new ThreadStart(checkNeedUpdate));
            thread.Start();
        }


        /// <summary>
        /// 结束线程
        /// </summary>
        private void endThread()
        {
            if (GVars.Demo == true)
            {
                return;
            }

            GVars.sqlceLocal.Exit = true;
            _exit = true;
        }
        #endregion


        #region 自动升级
        /// <summary>
        /// 检查是否需要升级
        /// </summary>
        private void checkNeedUpdate()
        {
            try
            {

                string log = string.Empty;
                autoUpdSrv = new AutoUpdateWebSrv.AutoUpdateWebSrv();

                log += "初始化Web服务" + ComConst.STR.CRLF;
                //logMsg = log;

                string appCode = "002";
                string serverIp = GVars.App.ServerIp;

                if (appCode.Length == 0 || serverIp.Length == 0)
                {
                    log += "Appcode 或 服务器IP为空 " + ComConst.STR.CRLF;
                    //logMsg = log;
                    return;
                }

                // 如果有新程序, 退出
                if (File.Exists(Path.Combine(appPath, "new_SDMN.exe")) == true)
                {
                    log += "更新文件已经存在" + ComConst.STR.CRLF;
                    //logMsg = log;
                    return;
                }

                // 如果没有新程序, 检查服务器是否有新程序
                autoUpdSrv.Url = UrlIp.ChangeIpInUrl(autoUpdSrv.Url, serverIp);
                log += "设置服务IP地址" + ComConst.STR.CRLF;
                //logMsg = log;
                while (_exit == false)
                {
                    //logMsg = log;
                    for (int i = 0; i < 2; i++)
                    {
                        if (_exit) return;
                        Thread.Sleep(1000);
                    }

                    // 如果有新程序, 退出
                    if (File.Exists(Path.Combine(appPath, "new_SDMN.exe")) == true)
                    {
                        needUpdate = true;
                        //logMsg += "更新文件已经存在" + ComConst.STR.CRLF;
                        return;
                    }

                    getNewVersion(appCode);

                    for (int i = 0; i < 60 * 60 * 2; i++)
                    {
                        if (_exit) return;
                        Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                //logMsg += ex.Message + ComConst.STR.CRLF;
            }
        }


        private bool getNewVersion(string appCode)
        {
            try
            {


                // 下载服务器的Config.xml文件, 并另存为Config2.xml
                //logMsg += "下载Config.xml文件" + ComConst.STR.CRLF;
                if (downLoadServerFile(appCode, "Config.xml") == false)
                {
                    return false;
                }

                // 检查是否有新版本
                //logMsg += "检查是否有新版本" + ComConst.STR.CRLF;
                if (chkNewVersion() == true)
                {
                    // 下载更新文件列表
                    if (downLoadServerFile(appCode, "FileList.xml") == false)
                    {
                        return false;
                    }

                    // 打开更新文件列表
                    DataSet dsFileList = new DataSet();
                    dsFileList.ReadXml(Path.Combine(appPath, "new_FileList.xml"), XmlReadMode.ReadSchema);
                    if (dsFileList == null || dsFileList.Tables.Count == 0 || dsFileList.Tables[0].Rows.Count == 0)
                    {
                        return false;
                    }

                    // 下载文件
                    foreach (DataRow dr in dsFileList.Tables[0].Rows)
                    {
                        downLoadServerFile(appCode, dr["FileName"].ToString());
                        Thread.Sleep(100);
                    }

                    //logMsg += "下载新程序完成" + ComConst.STR.CRLF;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// 检查是否有更新
        /// </summary>
        /// <returns></returns>
        private bool chkNewVersion()
        {

            string configFile = Path.Combine(appPath, "Config.xml");
            string configFile2 = Path.Combine(appPath, "new_Config.xml");

            if (File.Exists(configFile) && File.Exists(configFile2))
            {
                DataSet dsConfig = new DataSet();
                DataSet dsConfig2 = new DataSet();
                dsConfig.ReadXml(configFile, XmlReadMode.ReadSchema);
                dsConfig2.ReadXml(configFile2, XmlReadMode.ReadSchema);

                Thread.Sleep(500);
                File.Delete(configFile2);

                // 自动下载新旧版本切换时的判断
                if (File.Exists(Path.Combine(appPath, "new_FileList.xml")) == false
                    && File.Exists(Path.Combine(appPath, "FileList.xml")) == false)
                {
                    return true;
                }

                // 正常情况下的判断
                if (dsConfig.Tables.Count > 0 && dsConfig.Tables[0].Rows.Count > 0
                    && dsConfig2.Tables.Count > 0 && dsConfig2.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsConfig.Tables[0].Rows[0];
                    DataRow dr2 = dsConfig2.Tables[0].Rows[0];

                    if (dr["VERSION"] == DBNull.Value) return true;
                    if (dr["VERSION"] != DBNull.Value && dr2["VERSION"] != DBNull.Value)
                    {
                        if ((dr["VERSION"].ToString().Equals(dr2["VERSION"].ToString())))
                        {
                            return false;
                        }
                        else
                        {
                            if (dsConfig2.Tables[0].Columns.Count == dsConfig.Tables[0].Columns.Count)
                            {
                                for (int i = 0; i < dsConfig2.Tables[0].Columns.Count; i++)
                                {
                                    if (dsConfig2.Tables[0].Columns[i].ColumnName != "WARD_CODE")
                                    {
                                        dsConfig.Tables[0].Rows[0][i] = dsConfig2.Tables[0].Rows[0][i].ToString();
                                    }
                                }
                            }
                            else
                            {
                                dr["VERSION"] = dr2["VERSION"].ToString();
                            }
                            

                            

                            dsConfig.WriteXml(configFile, XmlWriteMode.WriteSchema);

                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 下载的新文件名后加 "_new"
        /// </summary>
        /// <param name="appCode"></param>
        /// <param name="localPath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool downLoadServerFile(string appCode, string fileName)
        {
            try
            {
                // 下载服务器的Config.xml文件, 并另存为Config2.xml
                byte[] fileBytes = autoUpdSrv.GetServerFile(appCode, fileName);
                if (fileBytes.Length < 10) return false;

                MemoryStream mstream = new MemoryStream(fileBytes);

                FileStream f = new FileStream(Path.Combine(appPath, "new_" + fileName), FileMode.OpenOrCreate);
                mstream.WriteTo(f);
                mstream.Close();
                f.Close();
                mstream = null;
                f = null;

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion



        private void TLastMinute_Tick(object sender, EventArgs e)
        {


            #region[以前]
            //mnuExit_Click(sender, e);//注销用户
            //    GVars.lastTime = Convert.ToInt32(GVars.App.LastMinute) * 60;//还原计时
            //    LoginForm frm = new LoginForm();
            //    frm.ShowDialog();
            //    TLastMinute.Enabled = true;//计时开始
            #endregion

            //if (MessageBox.Show("因长时间未操作，程序自动退出，请重新登录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            //{
            //    questExit = false;
            //    if (frmShow != null) {
            //        frmShow.Close();
            //    }
            //    GVars.sqlceLocal.Exit = true;
            //    MainFrm_Closed(sender, e);
            //   // this.Dispose(true);
            //    Application.Exit();
            //}


        }

        /// <summary>
        /// 创建评估但菜单项
        /// </summary>
        private void CreateMenuItem()
        {
            contextPgd.MenuItems.Clear();

            //获取到启用状态的，为全院的，科室相关联的的
            //string sqlStr = "SELECT * FROM DOC_TEMPLATE A WHERE A.IS_ENABLED=1 AND A.IS_GLOBAL=1 ORDER BY A.TEMPLATE_ID ";
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
            string configFile = Path.Combine(appPath, "Config.xml");
            DataSet dsConfig = new DataSet("Config");
            dsConfig.ReadXml(configFile, XmlReadMode.ReadSchema);
            DataRow dr = dsConfig.Tables[0].Rows[0];

            //配置文件中，配置的病区
            string deptCode = dr["WARD_CODE"].ToString();

            string sqlStr = @"SELECT * FROM DOC_TEMPLATE A WHERE A.IS_ENABLED=1 AND A.IS_GLOBAL=1
                                UNION
                                SELECT * FROM DOC_TEMPLATE B WHERE B.TEMPLATE_ID IN(
                                SELECT B.TEMPLATE_ID FROM DOC_TEMPLATE_DEPT B WHERE B.DEPT_CODE='" + deptCode + "') AND B.IS_GLOBAL=0 AND B.IS_ENABLED=1";
            DataSet dsAssessMent = GVars.sqlceLocal.SelectData(sqlStr);
            DataTable dtAssessMent = new DataTable();
            if (dsAssessMent != null && dsAssessMent.Tables[0].Rows.Count > 0)
            {
                dtAssessMent = dsAssessMent.Tables[0];

                //获取评估单名称
                //var assessMentName = from assess in dtAssessMent.AsEnumerable() select assess.Field<string>("TEMPLATE_NAME");
                //ArrayList assessMentLst = new ArrayList ();
                //foreach (string item in assessMentName)
                //{
                //    assessMentLst.Add(item);
                //}
                //string[] arrPgd = new string[] { "每日护理评估单", "住院护理评估单", "手术护理记录单", "坠床跌倒危险因素评分", "普儿科护理记录单", "患者日常生活能力评估单", "压疮护理评估单", "妇产科重症护理记录单I", "医院满意度调查表", "手术室术前随访记录单", "手术室术后随访记录单" };
                for (int i = 0; i < dtAssessMent.Rows.Count; i++)
                {
                    MenuItem mi = new MenuItem();
                    mi.Text = dtAssessMent.Rows[i]["TEMPLATE_NAME"].ToString();
                    mi.Click += new EventHandler(mi_Click);
                    contextPgd.MenuItems.Add(mi);
                }
            }


        }

        void mi_Click(object sender, EventArgs e)
        {
            // 扫描函数
            //ScanReader_ReadNotify scan_ReadNotify = null;
            string pgdName = (sender as MenuItem).Text;

            ItemValueInputFrm frm = new ItemValueInputFrm();
            frm.Text = pgdName;
            //scan_ReadNotify = new ScanReader_ReadNotify(frm.ScanReader_ReadNotify);
            frmShow = frm;
            frm.OneTime = GetIsOneTime(pgdName);

            if (frmShow != null)
            {
#if SCANNER
                //if (scan_ReadNotify != null)
                //{
                //    GVars.ScanReader.ReadNotify -= new EventHandler(scanReader_ReadNotify);
                //    GVars.ScanReader.ReadNotify += new EventHandler(scan_ReadNotify);
                //}
#endif

                frmShow.ShowDialog();

                // 设置病人按钮
                patNavigator.SetPatientButtons();

#if SCANNER
                //if (scan_ReadNotify != null)
                //{
                //    GVars.ScanReader.ReadNotify -= new EventHandler(scan_ReadNotify);
                //    GVars.ScanReader.ReadNotify += new EventHandler(scanReader_ReadNotify);
                //}
#endif

                //scan_ReadNotify = null;

                // 显示导航的功能
                if (frmShow.Tag != null && frmShow.Tag.ToString().Length > 0)
                {
                    RunFunc(frmShow.Tag.ToString());
                }
            }

            #region Demo


            //if ("每日护理评估单".Equals(pgdName))
            //{
            //    ItemValueInputFrm frm = new ItemValueInputFrm();
            //    frm.Text = "每日护理评估单";
            //    scan_ReadNotify = new ScanReader_ReadNotify(frm.ScanReader_ReadNotify);
            //    frmShow = frm;
            //}

            //// 护理
            //if ("住院护理评估单".Equals(pgdName))
            //{
            //    ItemValueInputFrm frm = new ItemValueInputFrm();
            //    frm.Text = "住院护理评估单";
            //    scan_ReadNotify = new ScanReader_ReadNotify(frm.ScanReader_ReadNotify);
            //    frmShow = frm;
            //}

            //// 执行单
            //if ("压疮护理评估单".Equals(pgdName))
            //{
            //    ItemValueInputFrm frm = new ItemValueInputFrm();
            //    frmShow = frm;
            //    frm.Text = "压疮护理评估单";
            //    scan_ReadNotify = new ScanReader_ReadNotify(frm.ScanReader_ReadNotify);
            //}


            //// 每日评估
            //if ("患者日常生活能力评估单".Equals(pgdName))
            //{
            //    ItemValueInputFrm frm = new ItemValueInputFrm();
            //    frm.DictID = "01";
            //    frm.OneTime = false;
            //    frm.Text = "患者日常生活能力评估单";

            //    scan_ReadNotify = new ScanReader_ReadNotify(frm.ScanReader_ReadNotify);

            //    frmShow = frm;
            //}
            #endregion
        }


        /// <summary>
        /// 判断oneTime
        /// </summary>
        /// <param name="pgdName"></param>
        /// <returns></returns>
        private bool GetIsOneTime(string pgdName)
        {
            bool _result = false;
            switch (pgdName)
            {
                case "每日护理评估单":
                    _result = true;
                    break;
                case "住院护理评估单":
                    _result = true;
                    break;
                default:
                    break;
            }
            return _result;
        }
    }
}