using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Management;

namespace HISPlus
{
    public class PrinterInfo
    {
        public bool printerOK = false;                                // 打印机是否准备好
        public Hashtable htPrinter = new Hashtable();
        public IniFile IniFile;

        public PrinterInfo()
        {
            
        }

        /// <summary>
        /// 访问打印机
        /// </summary>
        public void getPrinterInfo()
        {
            ManagementObjectSearcher query = null;         // 打印机查询
            ManagementObjectCollection queryCollection = null;         // 打印机查询结果

            try
            {
                // 查询输液贴打印机
                string printerName = IniFile.ReadString("PRINTER", "OrdersExecuteBill", string.Empty);
                if (IsPrintExists(printerName) == false)
                {
                    throw new Exception("OrdersExecuteBill的打印机:" + printerName + "不存在或者名称错误！");
                }
                string sql = "SELECT * FROM Win32_Printer WHERE NAME LIKE '%" + printerName + "%'";
                //string sql = "SELECT * FROM Win32_Printer WHERE NAME = " + SqlManager.SqlConvert(printerName);
                query = new ManagementObjectSearcher(sql);
                queryCollection = query.Get();
                foreach (ManagementObject mo in queryCollection)
                {
                    htPrinter.Add("OrdersExecuteBill", mo);
                    break;
                }

                query.Dispose();
                queryCollection.Dispose();


                // 查询腕带打印机
                printerName = IniFile.ReadString("PRINTER", "WristBandPrint", string.Empty);
                if (IsPrintExists(printerName) == false)
                {
                    throw new Exception("WristBandPrint的打印机:" + printerName + "不存在或者名称错误！");
                }
                sql = "SELECT * FROM Win32_Printer WHERE NAME LIKE '%" + printerName + "%'";
                //sql = "SELECT * FROM Win32_Printer WHERE NAME = " + SqlManager.SqlConvert(printerName);

                query = new ManagementObjectSearcher(sql);
                queryCollection = query.Get();
                foreach (ManagementObject mo in queryCollection)
                {
                    htPrinter.Add("WristBandPrint", mo);
                    break;
                }

                query.Dispose();
                queryCollection.Dispose();

                // 查询护理记录打印机
                printerName = IniFile.ReadString("PRINTER", "NursingRecord", string.Empty);
                if (IsPrintExists(printerName) == false)
                {
                    throw new Exception("NursingRecord的打印机:" + printerName + "不存在或者名称错误！");
                }
                sql = "SELECT * FROM Win32_Printer WHERE NAME LIKE '%" + printerName + "%'";
                //sql = "SELECT * FROM Win32_Printer WHERE NAME = " + SqlManager.SqlConvert(printerName);

                query = new ManagementObjectSearcher(sql);
                queryCollection = query.Get();
                foreach (ManagementObject mo in queryCollection)
                {
                    htPrinter.Add("NursingRecord", mo);
                    break;
                }

                query.Dispose();
                queryCollection.Dispose();

                // 查询体温图打印机
                printerName = IniFile.ReadString("PRINTER", "BodyTemperature", string.Empty);
                if (IsPrintExists(printerName) == false)
                {
                    throw new Exception("BodyTemperature的打印机:" + printerName + "不存在或者名称错误！");
                }
                sql = "SELECT * FROM Win32_Printer WHERE NAME LIKE '%" + printerName + "%'";
                //sql = "SELECT * FROM Win32_Printer WHERE NAME = " + SqlManager.SqlConvert(printerName);

                query = new ManagementObjectSearcher(sql);
                queryCollection = query.Get();
                //if (queryCollection.Count == 0)
                //{
                //    throw new Exception(string.Format("{0}的打印机{1}发生错误，请检查打印机名称和网络连接!", new object[2] { "BodyTemperature", printerName }));
                //}
                foreach (ManagementObject mo in queryCollection)
                {
                    htPrinter.Add("BodyTemperature", mo);
                    break;
                }

            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                this.printerOK = true;

                if (queryCollection != null)
                {
                    queryCollection.Dispose();
                }

                if (query != null)
                {
                    query.Dispose();
                }
            }
        }


        /// <summary>
        /// 打印机是否存在
        /// </summary>
        /// <param name="printerName"></param>
        /// <returns></returns>
        private bool IsPrintExists(string printerName)
        {
            foreach (string pName in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                if (pName.IndexOf(printerName)>=0)
                {
                    return true;
                }
            }
            return false;
        }

        public void SetDefaultPrinter(string printerName, out string Result)
        {
            Result = "";
            try
            {
                if (printerOK == false)
                {
                    Result = "正在设置打印机, 请稍候!";
                }
                else
                {

                    ((ManagementObject)htPrinter[printerName]).InvokeMethod("SetDefaultPrinter", null);
                    Result = "";
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

    }
}
