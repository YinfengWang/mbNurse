using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Microsoft.Win32;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    partial class NurseScheduleDayFrm : FormDo
    {   
        #region 变量
        private ExcelConfigData excelConfig     = new ExcelConfigData();    // Excel 打印配置
        private ExcelAccess     excelAccess     = new ExcelAccess();        // Excel 接口
        private string          _template       = "排班表";                 // 模板文件
        private DataSet         dsPrint         = null;                     // 打印信息
        private DataSet         dsDayList       = null;                     // 日期列表
        #endregion
        
        
        /// <summary>
        /// 加载Excel打印配置文件
        /// </summary>
        /// <param name="templateFileName"></param>
        private void loadExcelPrintConfig(string templateFileName)
        {
            excelConfig.Clear();

            // 读取配置文件
            string iniFile = Path.Combine(Application.StartupPath, "Template\\" + templateFileName + ".ini");
            if (File.Exists(iniFile) == true)
            {
                StreamReader sr = new StreamReader(iniFile);

                try
                {
                    string configInfo = sr.ReadToEnd();
                    excelConfig.ParseConfigInfo(configInfo);
                }
                finally
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
        }
        
        
        /// <summary>
        /// 用Excel模板打印
        /// </summary>
        /// <remarks>比较适合套打、格式、统计分析报表、图形分析、自定义打印</remarks>
        private void excelTemplatePrint()
        {
            string strExcelTemplateFile = Path.Combine(Application.StartupPath, "Template\\" + _template + ".xls");

            excelAccess.Open(strExcelTemplateFile);         // 打开模板文件
            excelAccess.IsVisibledExcel = true;
            excelAccess.FormCaption = string.Empty;

            excelTemplatePrintData();                       // 输出数据
            
            //excel.Print();				                // 打印
            excelAccess.PrintPreview();			            // 预览
            excelAccess.Close(false);				        // 关闭并释放			
        }
        
        
        /// <summary>
        /// 向Excel中输出数据
        /// </summary>
        private void excelTemplatePrintData()
        {
            int maxRowIndex = 0;
            
            // 输出单行项目
            for (int k = 0; k < excelConfig.ConfigSections.Count; k++)
            {
                ExcelConfigSection configSection = excelConfig.ConfigSections[k];
                if (configSection.MultiRows == true) continue;
                
                // 确定数据源
                DataRow drData = null;
                switch (configSection.TableName)
                {
                    case "PRINT_INFO":
                        if (dsPrint != null && dsPrint.Tables.Count > 0 && dsPrint.Tables[0].Rows.Count > 0)
                        {
                            drData = dsPrint.Tables[0].Rows[0];
                        }

                        break;

                    case "DATE_LIST":
                        if (dsDayList != null && dsDayList.Tables.Count > 0 && dsDayList.Tables[0].Rows.Count > 0)
                        {
                            drData = dsDayList.Tables[0].Rows[0];
                        }
                        break;
                    default:
                        break;
                }

                // 输出数据
                for (int n = 0; n < configSection.ConfigItems.Count; n++)
                {
                    ExcelItem excelItem = configSection.ConfigItems[n];
                    setExcelCellText(drData, excelItem.ItemId, excelItem.Row, excelItem.Col, excelItem.CheckValue);
                }
            }
            
            // 查找印数据
            DataRow[] drFind = dsScheduleDisp.Tables[0].Select(string.Empty, dsScheduleDisp.Tables[0].DefaultView.Sort);
            
            // 进行多行数据输出
            int rowIndex = 0;
            for (int i = 0; i < drFind.Length; i++)
            {
                DataRow dr = drFind[i];
                
                // 输出多行项目
                for (int k = 0; k < excelConfig.ConfigSections.Count; k++)
                {
                    ExcelConfigSection configSection = excelConfig.ConfigSections[k];
                    if (configSection.TableName.ToUpper().Trim().Equals("WORK_ARRANAGE") == false) continue;
                    
                    // 输出数据
                    for (int n = 0; n < configSection.ConfigItems.Count; n++)
                    {
                        ExcelItem excelItem = configSection.ConfigItems[n];
                        setExcelCellText(dr, excelItem.ItemId, excelItem.Row + rowIndex, excelItem.Col, excelItem.CheckValue);
                    }
                }
                
                rowIndex++;
            }
        }


        /// <summary>
        /// 向单元格赋值
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="colName"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void setExcelCellText(DataRow dr, string colName, int row, int col, string tagInfo)
        {
            // 如果不存在这个字段
            if (dr.Table.Columns.Contains(colName) == false)
            {
                return;
            }

            // 输出该字段值
            if (dr[colName] == DBNull.Value)
            {
                excelAccess.SetCellText(row, col, string.Empty);
            }
            else if (dr.Table.Columns[colName].DataType == typeof(DateTime))
            {
                excelAccess.SetCellText(row, col, ((DateTime)(dr[colName])).ToString(ComConst.FMT_DATE.LONG));
            }
            else
            {
                excelAccess.SetCellText(row, col, dr[colName].ToString());
            }
        }


        /// <summary>
        /// 获取打印信息
        /// </summary>
        /// <returns></returns>
        private void getPrintInfo()
        {
            if (dsPrint == null) dsPrint = new DataSet();
            if (dsPrint.Tables.Count == 0)
            {
                DataTable dt = dsPrint.Tables.Add("PRINT_INFO");
                dt.Columns.Add("SHIFT_DATE", typeof(DateTime));
                dt.Columns.Add("DEPT_CODE", typeof(string));
                dt.Columns.Add("DEPT_NAME", typeof(string));
            }
            
            DataRow drEdit = null;
            if (dsPrint.Tables[0].Rows.Count == 0)
            {
                drEdit = dsPrint.Tables[0].NewRow();
            }
            else
            {
                drEdit = dsPrint.Tables[0].Rows[0];
            }
            
            drEdit["SHIFT_DATE"]    = dtPicker.Value.Date;
            drEdit["DEPT_CODE"]     = cmbDept.SelectedValue;
            drEdit["DEPT_NAME"]     = cmbDept.Text;
            
            if (dsPrint.Tables[0].Rows.Count == 0)
            {
                dsPrint.Tables[0].Rows.Add(drEdit);
            }
        }
        
        
        /// <summary>
        /// 获取打印信息
        /// </summary>
        /// <returns></returns>
        private void getDayList()
        {
            if (dsDayList == null) dsDayList = new DataSet();
            if (dsDayList.Tables.Count == 0)
            {
                DataTable dt = dsDayList.Tables.Add("DAY_LIST");
                dt.Columns.Add("DAY_1", typeof(string));
                dt.Columns.Add("DAY_2", typeof(string));
                dt.Columns.Add("DAY_3", typeof(string));
                dt.Columns.Add("DAY_4", typeof(string));
                dt.Columns.Add("DAY_5", typeof(string));
                dt.Columns.Add("DAY_6", typeof(string));
                dt.Columns.Add("DAY_7", typeof(string));
            }
            
            DataRow drEdit = null;
            if (dsDayList.Tables[0].Rows.Count == 0)
            {
                drEdit = dsDayList.Tables[0].NewRow();
            }
            else
            {
                drEdit = dsDayList.Tables[0].Rows[0];
            }
            
            drEdit["DAY_1"]   = arrLabelDate[0].Text;
            drEdit["DAY_2"]   = arrLabelDate[1].Text;
            drEdit["DAY_3"]   = arrLabelDate[2].Text;
            drEdit["DAY_4"]   = arrLabelDate[3].Text;
            drEdit["DAY_5"]   = arrLabelDate[4].Text;
            drEdit["DAY_6"]   = arrLabelDate[5].Text;
            drEdit["DAY_7"]   = arrLabelDate[6].Text;
            
            if (dsDayList.Tables[0].Rows.Count == 0)
            {
                dsDayList.Tables[0].Rows.Add(drEdit);
            }
        }        
    }
}
