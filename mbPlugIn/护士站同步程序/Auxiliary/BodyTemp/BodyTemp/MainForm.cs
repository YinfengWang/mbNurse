// ***********************************************************************
// Assembly         : BodyTemp
// Author           : LPD
// Created          : 12-04-2015
//
// Last Modified By : LPD
// Last Modified On : 12-04-2015
// ***********************************************************************
// <copyright file="MainForm.cs" company="心医国际">
//     Copyright (c) 心医国际. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Xy.Auxiliary.Bll;
using Xy.Auxiliary.PubCls;
using Xy.Auxiliary.Entity;
using HISPlus;
using System.Threading;
using OrderSplitHelper;
using System.Xml;
using System.Diagnostics;

/// <summary>
/// The BodyTemp namespace.
/// </summary>
namespace BodyTemp
{
    /// <summary>
    /// Class MainForm.
    /// </summary>
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        OrdersM_Bll orderBll = new OrdersM_Bll();
        OrdersExecute_Bll orderExeBll = new OrdersExecute_Bll();
        BodytempAutoupdate_Bll bodyTemp = new BodytempAutoupdate_Bll();
        OrderSplit osp = new OrderSplit();
        public object ojb = new object();

        /// <summary>
        /// Web服务，拆分方法
        /// </summary>
        //SplitOrders.OrderSplitServiceClient splitOrdersDll = new SplitOrders.OrderSplitServiceClient();

        /// <summary>
        /// 记录日志的条数,当界面显示超过此数字时,清空界面数据。
        /// </summary>
        private int recodeRowCount = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["recodeRowCount"]);

        /// <summary>
        /// 医嘱拆分天数
        /// </summary>
        private int SplitDay = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SPLIT_DATES"]);

        public System.Timers.Timer timerSyncSplit = new System.Timers.Timer();

        public System.Timers.Timer timeCheckUpdate = new System.Timers.Timer();

        public MainForm()
        {
            InitializeComponent();
            //timerSync.Enabled = true;
            timerSyncSplit.Enabled = true;
            Control.CheckForIllegalCrossThreadCalls = false;
            //获取间隔时间
            timerSyncSplit.Interval = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["timeInterval"]);
            timerSyncSplit.Elapsed += new System.Timers.ElapsedEventHandler(timerSyncSplit_Elapsed);

            //timerSync.Interval = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["timeInterval"]);

            //获取检测自动更新时间，默认1小时
            timeCheckUpdate.Interval = 60 * 60 * 1000;
            timeCheckUpdate.Elapsed += new System.Timers.ElapsedEventHandler(timeCheckUpdate_Elapsed);
            timeCheckUpdate.Enabled = false;
        }




        /// <summary>
        /// 同步医嘱
        /// </summary>
        /// <param name="newOrdes">Orders.ini文件中的新医嘱</param>
        /// <returns></returns>
        protected bool SyncDoctorAdvice(string newOrders)
        {

            bool _syncFlag = true;

            try
            {
                //1:验证该医嘱是否已经存在ORDER_M表
                //2:不存在-> 进行同步
                //3:日志记录

                /*根据'|'将医嘱分为多条*/
                string[] arrOrders = newOrders.Split("|".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                //arrOrders = arrOrders.GroupBy(p => p).Select(p => p.Key).ToArray();
                for (int m = 0; m < arrOrders.Length; m++)
                {
                    if (string.IsNullOrEmpty(arrOrders[m]))
                        continue;

                    string patientID = arrOrders[m].Split(',')[0];    //病人ID
                    string visitID = arrOrders[m].Split(',')[1];      //住院次数
                    string orderNo = arrOrders[m].Split(',')[2];      //医嘱序号
                    string orderSubNo = string.Empty;  //医嘱子序号

                    //1:根据patientID，visitID，orderNo在His中获取到医嘱的全部内容。
                    DataTable dtHisOrders = orderBll.GetHisOrders(patientID, Convert.ToDecimal(visitID), Convert.ToDecimal(orderNo));

                    //根据patientID，visitID，orderNo在His中获取到医嘱的orderSubNo
                    for (int n = 0; n < dtHisOrders.Rows.Count; n++)
                    {
                        orderSubNo = dtHisOrders.Rows[n]["ORDER_SUB_NO"].ToString();

                        //医嘱详情
                        string ordersDetail = " 【" + patientID + "," + visitID + "," + orderNo + "," + orderSubNo + "】";

                        //true:医嘱已存在orders_m表   false:未同步
                        bool _isExists = orderBll.Exists(patientID, Convert.ToDecimal(visitID), Convert.ToDecimal(orderNo), Convert.ToDecimal(orderSubNo));
                        if (_isExists)
                        {
                            OrdersM_Entity orderEntity = orderBll.GetOrdersModel(patientID, Convert.ToDecimal(visitID), Convert.ToDecimal(orderNo), Convert.ToDecimal(orderSubNo));
                            //此医嘱已经存在Orders_M表,需再次验证此医嘱在HIS的状态
                            if (dtHisOrders.Rows[n]["ORDER_STATUS"].ToString() == orderEntity.ORDER_STATUS && dtHisOrders.Rows[n]["STOP_DATE_TIME"].ToString() == orderEntity.STOP_DATE_TIME.ToString())
                            {
                                //医嘱状态和停止时间一致
                                //此医嘱已经同步过,结束此次循环，进入下条医嘱。
                                lstSyncRecodeList.Items.Add("此医嘱已经存在,无需再次同步--->" + ordersDetail);
                                continue;
                            }
                            else
                            {
                                //更新医嘱状态操作
                                lstSyncRecodeList.Items.Add("检索到需要变更的医嘱--->" + ordersDetail);

                                //日志
                                LogHelper.SetSyncLogs("检索到需要变更的医嘱--->" + ordersDetail);

                                //返回变更结果
                                bool _updFlag = orderBll.UpdMobileOrdersFromHis(dtHisOrders.Rows[n]);
                                if (_updFlag)
                                {

                                    lstSyncRecodeList.Items.Add("医嘱【变更】--->" + ordersDetail + "【成功】");
                                    lstSyncSuccess.Items.Add("医嘱【变更】:" + ordersDetail + "【成功】");

                                    //日志
                                    LogHelper.SetSyncLogs("医嘱【变更】--->" + ordersDetail + "【成功】");
                                }
                                else
                                {

                                    lstSyncRecodeList.Items.Add("医嘱【变更】--->" + "【" + patientID + "," + visitID + "," + orderNo + "," + orderSubNo + "】【失败】");
                                    if (!lstSyncFail.Items.Contains(ordersDetail))
                                        lstSyncFail.Items.Add("医嘱【变更】:" + ordersDetail + "【失败】");

                                    //日志
                                    LogHelper.SetSyncLogs("医嘱【变更】:" + ordersDetail + "【失败】");

                                }

                            }


                        }
                        else
                        {
                            //新医嘱
                            //开始同步此医嘱
                            lstSyncRecodeList.Items.Add("检索到需要新增同步的医嘱--->" + ordersDetail);
                            lstSyncRecodeList.Items.Add("开始同步医嘱...");

                            //日志
                            LogHelper.SetSyncLogs("检索到需要新增同步的医嘱--->" + ordersDetail);

                            //开始同步业务
                            //1:根据patientID，visitID，orderNo在His中获取到医嘱的全部内容。
                            //2:将医嘱详细信息插入，移动护理库中。
                            //DataTable dtHisOrders = orderBll.GetHisOrders(patientID, Convert.ToDecimal(visitID), Convert.ToDecimal(orderNo));

                            //写入移动护理库
                            bool _insFlag = orderBll.SyncHisOrdersToMobile(dtHisOrders.Rows[n]);


                            if (_insFlag)
                            {
                                lstSyncRecodeList.Items.Add("医嘱【新增】--->" + ordersDetail + "【成功】");
                                //写入同步成功列表
                                lstSyncSuccess.Items.Add("医嘱【新增】:" + ordersDetail + "【成功】");

                                //日志
                                LogHelper.SetSyncLogs("医嘱【新增】--->" + ordersDetail + "【成功】");
                            }
                            else
                            {

                                lstSyncRecodeList.Items.Add("医嘱【新增】--->" + ordersDetail + "【失败】");
                                //写入同步失败列表
                                lstSyncFail.Items.Add("医嘱【新增】:" + ordersDetail + "【失败】");

                                //日志
                                LogHelper.SetSyncLogs("医嘱【新增】--->" + ordersDetail + "【失败】");
                            }


                        }
                    }

                }
            }
            catch (Exception ex)
            {
                _syncFlag = false;
                lstSyncRecodeList.Items.Add(ex.Message);
            }



            return _syncFlag;
        }

        /// <summary>
        /// 拆分医嘱
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool SplitDoctorAdvice(string newOrders)
        {
            bool _splitState = true; //拆分成功
            try
            {
                //3:遍历从ORDER_M中取到的医嘱,在ORDER_EXECUTO表中查找是否已经存在(存在:忽略  不存在:拆分)
                //4:

                //1:获取ini文件中需要拆分的医嘱,在ORDER_M中找到对应医嘱的全部内容
                DataTable getIniOrdersDt = GetIniFileOrdersDt(newOrders);

                //Orders_M表中的医嘱不为空.
                if (getIniOrdersDt != null && getIniOrdersDt.Rows.Count > 0)
                {
                    lstSpiltRecodeList.Items.Add("检索到需要拆分的的医嘱:" + getIniOrdersDt.Rows.Count + "条");
                    lstSpiltRecodeList.Items.Add("准备开始拆分...");

                    //拆分
                    LogHelper.SetSplitLogs("检索到需要拆分的的医嘱:" + getIniOrdersDt.Rows.Count + "条");
                    LogHelper.SetSplitLogs("准备开始拆分...");

                    for (int m = 0; m < getIniOrdersDt.Rows.Count; m++)
                    {
                        string patientID = getIniOrdersDt.Rows[m]["PATIENT_ID"].ToString();    //病人ID
                        string visitID = getIniOrdersDt.Rows[m]["VISIT_ID"].ToString();      //住院次数
                        string orderNo = getIniOrdersDt.Rows[m]["ORDER_NO"].ToString();      //医嘱序号
                        //string orderSubNo = getIniOrdersDt.Rows[m]["ORDER_SUB_NO"].ToString();  //医嘱子序号

                        //医嘱详情
                        string ordersDetail = " 【" + patientID + "," + visitID + "," + orderNo + "】";

                        //1:验证是否已经拆分过
                        //2:进行拆分
                        bool _isSplit = orderExeBll.OrdersExecuteExists(patientID, Convert.ToDecimal(visitID), Convert.ToDecimal(orderNo));
                        if (_isSplit)
                        {
                            //1:已经拆分过
                            lstSpiltRecodeList.Items.Add("此医嘱已经拆分,无需再次拆分--->" + ordersDetail);

                            //日志
                            LogHelper.SetSplitLogs("此医嘱已经拆分,无需再次拆分--->" + ordersDetail);
                            continue;
                        }
                        else
                        {

                            //object o = getIniOrdersDt;
                            //ParameterizedThreadStart tStart = new ParameterizedThreadStart(SplitOrdersToOrderExecute);
                            //Thread thread = new Thread(tStart);
                            //thread.Start(o);//传递参数
                            DataTable dtMobileOrdersBy = orderBll.GetMobileOrders_M(patientID, Convert.ToDecimal(visitID), Convert.ToDecimal(orderNo));
                            if (SplitOrdersToOrderExecute(dtMobileOrdersBy))
                            {
                                lstSpiltRecodeList.Items.Add("此医嘱拆分完成." + ordersDetail);
                                if (!lstSplitSuccess.Items.Contains(ordersDetail))
                                    lstSplitSuccess.Items.Add(ordersDetail);

                                //日志
                                LogHelper.SetSplitLogs("此医嘱拆分完成." + ordersDetail);
                            }
                            else
                            {
                                lstSpiltRecodeList.Items.Add("此医嘱拆分失败." + ordersDetail);
                                if (!lstSplitFail.Items.Contains(ordersDetail))
                                    lstSplitFail.Items.Add(ordersDetail);
                                _splitState = false;

                                //日志
                                LogHelper.SetSplitLogs("此医嘱拆分失败." + ordersDetail);
                            }
                        }

                    }

                }
                else
                {
                    lstSpiltRecodeList.Items.Add("未检索到需要拆分的的医嘱...");
                    //日志
                    LogHelper.SetSplitLogs("未检索到需要拆分的的医嘱...");
                }
            }
            catch (Exception ex)
            {
                lstSpiltRecodeList.Items.Add("异常信息:" + ex.Message);
                _splitState = false;
            }




            //SetShowBar();
            return _splitState;
        }

        /// <summary>
        /// 拆分医嘱
        /// 调用服务器拆分程序的DLL
        /// </summary>
        /// <param name="drOrders"></param>
        private bool SplitOrdersToOrderExecute(object dtOrders)
        {
            //拆分结果
            bool _splitFlag = false;
            try
            {
                DataSet dsOrders = new DataSet();
                //需要传递给DLL的DataTable
                DataTable dt = (DataTable)dtOrders;
                dsOrders.Tables.Add(dt.Copy());

                //DataTable转Json,记录日志(用于测试,发布时请删除)
                string xmlValue = Dataset2Json(dsOrders);

                //日志
                LogHelper.SetSplitLogs("传递给DLL记录条数:" + dsOrders.Tables[0].Rows.Count);
                LogHelper.SetSplitLogs("传递给DLL记录值为:" + xmlValue);

                lock (ojb)
                {
                    //调用服务器DLL进行拆分
                    _splitFlag = osp.SplitMedicalOrders(dsOrders);

                    //调用Web服务进行拆分,已弃用
                    //_splitFlag = splitOrdersDll.SplitMedicalOrders(dsOrders);

                }
            }
            catch (Exception ex)
            {
                lstSpiltRecodeList.Items.Add("异常信息:" + ex.Message);
                _splitFlag = false;
            }
            return _splitFlag;
        }

        /// <summary>
        /// 根据ini文件中的医嘱在orders_m表找到医嘱内容
        /// </summary>
        /// <param name="newOrders"></param>
        /// <returns></returns>
        private DataTable GetIniFileOrdersDt(string newOrders)
        {
            return orderBll.GetIniFileOrdersDt(newOrders);
        }

        /// <summary>
        /// 清除界面的值
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void toolClear_Click(object sender, EventArgs e)
        {
            this.lstSyncRecodeList.Items.Clear();
            this.lstSpiltRecodeList.Items.Clear();
            this.lstSyncSuccess.Items.Clear();
            this.lstSyncFail.Items.Clear();
            this.lstSplitSuccess.Items.Clear();
            this.lstSyncFail.Items.Clear();
        }

        /// <summary>
        /// 关闭时,隐藏在右下角
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.ShowInTaskbar = false;
                this.myIcon.Icon = this.Icon;
                this.Hide();
            }
        }

        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                myMenu.Show();
            }

            if (e.Button == MouseButtons.Left)
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
            }
        }


        /// <summary>
        /// 退出程序
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void toolExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 设置滚动条及界面清空
        /// </summary>
        private void SetShowBar()
        {
            //定位到最后一行
            if (lstSyncRecodeList.Items.Count > 0)
                lstSyncRecodeList.SelectedIndex = lstSyncRecodeList.Items.Count - 1;
            if (lstSyncSuccess.Items.Count > 0)
                lstSyncSuccess.SelectedIndex = lstSyncSuccess.Items.Count - 1;
            if (lstSyncFail.Items.Count > 0)
                lstSyncFail.SelectedIndex = lstSyncFail.Items.Count - 1;
            if (lstSpiltRecodeList.Items.Count > 0)
                lstSpiltRecodeList.SelectedIndex = lstSpiltRecodeList.Items.Count - 1;
            if (lstSplitSuccess.Items.Count > 0)
                lstSplitSuccess.SelectedIndex = lstSplitSuccess.Items.Count - 1;
            if (lstSplitFail.Items.Count > 0)
                lstSplitFail.SelectedIndex = lstSplitFail.Items.Count - 1;


            //当界面显示超过此数字时,清空界面数据，清空界面的值
            if (lstSyncRecodeList.Items.Count > recodeRowCount)
                lstSyncRecodeList.Items.Clear();
            if (lstSyncSuccess.Items.Count > recodeRowCount)
                lstSyncSuccess.Items.Clear();
            if (lstSyncFail.Items.Count > recodeRowCount)
                lstSyncFail.Items.Clear();
            if (lstSpiltRecodeList.Items.Count > recodeRowCount)
                lstSpiltRecodeList.Items.Clear();
        }

        /// <summary>
        /// 定时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void timerSync_Tick(object sender, EventArgs e)
        //{
        //try
        //{
        //    //日志存放路径
        //    string logPath = System.Windows.Forms.Application.StartupPath + "\\Log"; ;
        //    if (!Directory.Exists(logPath))
        //    {
        //        Directory.CreateDirectory(logPath);
        //    }

        //    //1:读取Orders.ini，获取本地Orders.ini配置信息
        //    string iniPath = Path.Combine(Path.Combine(Application.StartupPath, "orders"), "Orders.ini");
        //    if (!File.Exists(iniPath))
        //    {
        //        //文件不存在，返回
        //        lstSyncRecodeList.Items.Add("系统环境不完整,缺少Orders.INI配置文件！");
        //        return;
        //    }
        //    else
        //    {


        //        //记录Orders.ini文件中的新医嘱,已'|'分割
        //        string iniNewOrdersValue = GetOrdersIniNewAdvice(iniPath);

        //        //如果Orders.IN中有新医嘱数据
        //        if (!string.IsNullOrEmpty(iniNewOrdersValue))
        //        {
        //            //同步
        //            //Orders.ini文件存在,开始记录日志
        //            lstSyncRecodeList.Items.Add("--------------------------------------" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-----------------------------------------");

        //            //1:同步至Order_M表
        //            if (SyncDoctorAdvice(iniNewOrdersValue))
        //            {

        //            }
        //            lstSyncRecodeList.Items.Add("----------------------------------------------END---------------------------------------------------------");
        //            lstSyncRecodeList.Items.Add("\r\n");
        //            lstSyncRecodeList.Items.Add("\r\n");


        //            //拆分
        //            lstSpiltRecodeList.Items.Add("--------------------------------------" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-----------------------------------------");
        //            if (SplitDoctorAdvice(iniNewOrdersValue))
        //            {
        //                DelIniOrders(iniPath);
        //            }
        //            //
        //            lstSpiltRecodeList.Items.Add("----------------------------------------------END---------------------------------------------------------");
        //            lstSpiltRecodeList.Items.Add("\r\n");
        //            lstSpiltRecodeList.Items.Add("\r\n");
        //        }
        //        else
        //        {
        //            lstSyncRecodeList.Items.Add("--------------------------------------" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-----------------------------------------");
        //            lstSpiltRecodeList.Items.Add("--------------------------------------" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-----------------------------------------");
        //            //没有需要同步的医嘱
        //            lstSyncRecodeList.Items.Add("没有需要同步的医嘱...");
        //            lstSpiltRecodeList.Items.Add("没有需要拆分的医嘱...");
        //            lstSyncRecodeList.Items.Add("----------------------------------------------END---------------------------------------------------------");
        //            lstSpiltRecodeList.Items.Add("----------------------------------------------END---------------------------------------------------------");
        //        }



        //        //设置滚动条
        //        SetShowBar();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    lstSyncRecodeList.Items.Add(ex.Message);
        //    lstSyncRecodeList.Items.Add("----------------------------------------------END---------------------------------------------------------");
        //    lstSyncRecodeList.Items.Add("\r\n");
        //    lstSyncRecodeList.Items.Add("\r\n");
        //}

        //}

        /// <summary>
        /// 定时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timerSyncSplit_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {


                //1:读取Orders.ini，获取本地Orders.ini配置信息
                string iniPath = Path.Combine(Path.Combine(Application.StartupPath, "orders"), "Orders.ini");
                if (!File.Exists(iniPath))
                {
                    //文件不存在，返回
                    lstSyncRecodeList.Items.Add("系统环境不完整,缺少Orders.INI配置文件！");
                    return;
                }
                else
                {


                    //记录Orders.ini文件中的新医嘱,已'|'分割
                    string iniNewOrdersValue = GetOrdersIniNewAdvice(iniPath);

                    //如果Orders.IN中有新医嘱数据
                    if (!string.IsNullOrEmpty(iniNewOrdersValue))
                    {
                        //同步
                        //Orders.ini文件存在,开始记录日志
                        lstSyncRecodeList.Items.Add("--------------------------------------" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-----------------------------------------");

                        //1:同步至Order_M表
                        if (SyncDoctorAdvice(iniNewOrdersValue))
                        {

                        }
                        lstSyncRecodeList.Items.Add("----------------------------------------------END---------------------------------------------------------");
                        lstSyncRecodeList.Items.Add("\r\n");
                        lstSyncRecodeList.Items.Add("\r\n");



                        lstSpiltRecodeList.Items.Add("--------------------------------------" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-----------------------------------------");

                        //拆分操作
                        if (SplitDoctorAdvice(iniNewOrdersValue))
                        {
                            DelIniOrders(iniPath);
                        }
                        //
                        lstSpiltRecodeList.Items.Add("----------------------------------------------END---------------------------------------------------------");
                        lstSpiltRecodeList.Items.Add("\r\n");
                        lstSpiltRecodeList.Items.Add("\r\n");
                    }
                    else
                    {
                        lstSyncRecodeList.Items.Add("--------------------------------------" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-----------------------------------------");
                        lstSpiltRecodeList.Items.Add("--------------------------------------" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-----------------------------------------");
                        //没有需要同步的医嘱
                        lstSyncRecodeList.Items.Add("没有需要同步的医嘱...");
                        lstSpiltRecodeList.Items.Add("没有需要拆分的医嘱...");

                        //日志
                        LogHelper.SetSyncLogs("没有需要同步的医嘱...");
                        LogHelper.SetSplitLogs("没有需要拆分的医嘱...");

                        lstSyncRecodeList.Items.Add("----------------------------------------------END---------------------------------------------------------");
                        lstSpiltRecodeList.Items.Add("----------------------------------------------END---------------------------------------------------------");
                    }



                    //设置滚动条
                    SetShowBar();
                }
            }
            catch (Exception ex)
            {
                lstSyncRecodeList.Items.Add(ex.Message);
                lstSyncRecodeList.Items.Add("----------------------------------------------END---------------------------------------------------------");
                lstSyncRecodeList.Items.Add("\r\n");
                lstSyncRecodeList.Items.Add("\r\n");
            }
        }

        /// <summary>
        /// 定时检测自动更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timeCheckUpdate_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //本地的配置文件
            string updFileConfigPath = System.Environment.CurrentDirectory + "\\" + "UpdConfig.xml";

            //当前数据库中BodytempAutoupdate表数据
            DataTable dtBodyTemp = bodyTemp.GetBodytempAutoupdate();

            //如果当前目录不存在更新文件xml，则创建，全部下载
            if (!File.Exists(updFileConfigPath))
            {
                CreateXmlFile(dtBodyTemp, updFileConfigPath);
                //退出当前程序
                Application.Exit();
                //执行更新程序
                Process.Start(System.Environment.CurrentDirectory + "\\" + "Xy.Auxiliary.AutoUpdate.exe");
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(updFileConfigPath);

                

                //验证版本号，进行更新操作
                for (int i = 0; i < dtBodyTemp.Rows.Count; i++)
                {
                    //XPATH解析XML
                    XmlElement xet = doc.SelectSingleNode("/FILES/file[FILENAME='" + dtBodyTemp.Rows[i]["FILENAME"].ToString() + "'] ") as XmlElement;
                    //本地不存在此文件名
                    if (!doc.InnerText.Contains(dtBodyTemp.Rows[i]["FILENAME"].ToString()))
                    {
                        //执行更新
                        //退出当前程序
                        Application.Exit();
                        //执行更新程序
                        Process.Start(System.Environment.CurrentDirectory + "\\" + "Xy.Auxiliary.AutoUpdate.exe");
                        return;
                    }
                    //本地存在此文件名,且版本号不一致
                    if (doc.InnerText.Contains(dtBodyTemp.Rows[i]["FILENAME"].ToString()) 
                        &&
                        Convert.ToInt32(dtBodyTemp.Rows[i]["VERSION"].ToString()) != Convert.ToInt32(xet.ChildNodes[1].InnerText))
                    {
                        //执行更新
                        //退出当前程序
                        Application.Exit();
                        //执行更新程序
                        Process.Start(System.Environment.CurrentDirectory + "\\" + "Xy.Auxiliary.AutoUpdate.exe");
                    }

                }
            }
        }

        public void CreateXmlFile(DataTable dt, string fileSavePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点  
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            //创建根节点  
            XmlNode root = xmlDoc.CreateElement("FILES");
            xmlDoc.AppendChild(root);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                XmlNode node1 = xmlDoc.CreateNode(XmlNodeType.Element, "file", null);
                CreateNode(xmlDoc, node1, "FILENAME", dt.Rows[i]["FILENAME"].ToString());
                CreateNode(xmlDoc, node1, "VERSION", "1");
                root.AppendChild(node1);
            }

            try
            {
                xmlDoc.Save(fileSavePath);
            }
            catch (Exception e)
            {

            }
            
            
        }

        /// <summary>    
        /// 创建节点    
        /// </summary>    
        /// <param name="xmldoc"></param>  xml文档  
        /// <param name="parentnode"></param>父节点    
        /// <param name="name"></param>  节点名  
        /// <param name="value"></param>  节点值  
        ///   
        public void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }

        /// <summary>
        /// 获取Order.ini文件中需要同步的医嘱
        /// </summary>
        /// <param name="iniPath">Order.ini文件路径</param>
        /// <returns></returns>
        private string GetOrdersIniNewAdvice(string iniPath)
        {
            //方法返回的新医嘱字符串
            string returnNewOrdersValue = string.Empty;

            //打开iniPath路径的文件
            FileStream aFile = new FileStream(iniPath, FileMode.Open);
            StreamReader sr = new StreamReader(aFile, System.Text.Encoding.Default);
            string strLine = sr.ReadToEnd();

            //根据\r\n分割字符串
            Regex reg = new Regex(@"\r\n");
            string[] arrOrders = reg.Split(strLine);

            //遍历Orders.ini中的节点
            for (int i = 0; i < arrOrders.Length; i++)
            {
                //查找info节点
                if (arrOrders[i].Contains("info"))
                {
                    returnNewOrdersValue = arrOrders[i].Remove(0, 5);
                }
            }
            aFile.Close();
            sr.Close();
            sr.Dispose();
            return returnNewOrdersValue;
        }

        /// <summary>
        /// 删除Order.ini遗嘱信息
        /// </summary>
        /// <param name="iniPath"></param>
        private void DelIniOrders(string iniPath)
        {
            FileStream aFile = new FileStream(iniPath, FileMode.Open);
            StreamReader sr = new StreamReader(aFile, System.Text.Encoding.Default);
            string strLine = sr.ReadToEnd();
            //Regex reg = new Regex(@"\r\n");
            //string[] arrOrders = reg.Split(strLine);
            string[] arrOrders = strLine.Split("\r\n".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            List<string> lstOrders = arrOrders.ToList();
            string resultValue = string.Empty;
            for (int i = 0; i < lstOrders.Count; i++)
            {
                if (lstOrders[i].Contains("info"))
                {
                    lstOrders[i] = lstOrders[i].Remove(5);
                }
                resultValue += lstOrders[i] + "\r\n";
            }
            aFile.Close();
            sr.Close();
            File.WriteAllText(iniPath, resultValue, Encoding.Default);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.toolSetting.Checked = true;

            //打开程序,隐藏到托盘
            this.ShowInTaskbar = false;
            this.myIcon.Icon = this.Icon;
            this.Hide();
        }


        /// <summary>  
        /// DataSet转换成Json格式  
        /// </summary>  
        /// <param name="ds">DataSet</param> 
        /// <returns></returns>  
        public static string Dataset2Json(DataSet ds)
        {
            StringBuilder json = new StringBuilder();

            foreach (DataTable dt in ds.Tables)
            {
                json.Append("{\"");
                json.Append(dt.TableName);
                json.Append("\":");
                json.Append(DataTable2Json(dt));
                json.Append("}");
            } return json.ToString();
        }

        public static string DataTable2Json(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"");
            jsonBuilder.Append(dt.TableName);
            jsonBuilder.Append("\":[");
            jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }

        /// <summary>
        /// 右键托盘显示窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolShowFrm_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            //this.TopMost = true;
        }

        /// <summary>
        /// 右键托盘隐藏窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolHide_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.myIcon.Icon = this.Icon;
            this.Hide();
        }

        /// <summary>
        /// 更新设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolAutoUpdate_Click(object sender, EventArgs e)
        {
            SetAutoUpdate setAutoUpdate = new SetAutoUpdate();
            setAutoUpdate.ShowDialog();
        }



    }
}
