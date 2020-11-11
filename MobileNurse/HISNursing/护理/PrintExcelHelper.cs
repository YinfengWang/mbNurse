namespace HISPlus
{
    using Excel;
    using System;
    using System.Collections;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class PrintExcelHelper
    {
        protected object missing = Missing.Value;
        protected string outputFile = null;
        protected int sheet1 = 0;
        protected int sheet2 = 0;
        protected string templetFile = null;

        public PrintExcelHelper(string templetFilePath, string outputFilePath)
        {
            if (templetFilePath == null)
            {
                throw new Exception("Excel模板文件路径不能为空！");
            }
            if (outputFilePath == null)
            {
                throw new Exception("输出Excel文件路径不能为空！");
            }
            if (!File.Exists(templetFilePath))
            {
                throw new Exception("指定路径的Excel模板文件不存在！");
            }
            this.templetFile = templetFilePath;
            this.outputFile = outputFilePath;
        }

        public void DataTableToExcel(System.Data.DataTable dt, int rows, int top, int left, int rowCountnum, int rowsnum, string sheetPrefixName, Hashtable ht)
        {
            Worksheet worksheet2;
            int num5;
            int num6;
            string[] strArray;
            string[] strArray2;
            int num7;
            int num8;
            int num10;
            int num12;
            int num13;
            int num14;
            int count = dt.Rows.Count;
            int num2 = dt.Columns.Count;
            if ((sheetPrefixName == null) || (sheetPrefixName.Trim() == ""))
            {
                sheetPrefixName = "Sheet";
            }
            DateTime now = DateTime.Now;
            Application o = new ApplicationClass {
                Visible = false
            };
            DateTime time2 = DateTime.Now;
            Workbook workbook = o.Workbooks.Open(this.templetFile, this.missing, this.missing, this.missing, this.missing, this.missing, this.missing, this.missing, this.missing, this.missing, this.missing, this.missing, this.missing);
            Worksheet worksheet = (Worksheet) workbook.Sheets.get_Item(1);
            if ((rowsnum - 1) > 0)
            {
                worksheet2 = (Worksheet) workbook.Worksheets.get_Item(2);
                worksheet2.Name = sheetPrefixName + "-2";
                int num3 = 0;
                if (((rowsnum - 1) + count) < rowCountnum)
                {
                    num3 = count;
                }
                else
                {
                    num3 = (rowCountnum - rowsnum) + 1;
                }
                for (int i = 0; i < num3; i++)
                {
                    num5 = 0;
                    while (num5 < (num2 - 1))
                    {
                        worksheet2.Cells[((top + rowsnum) - 1) + i, left + num5] = dt.Rows[i][num5].ToString();
                        num5++;
                    }
                    this.sheet2++;
                }
                worksheet2 = (Worksheet) workbook.Worksheets.get_Item(1);
                worksheet2.Name = sheetPrefixName + "-1";
                if (ht.Count > 0)
                {
                    for (num6 = 0; num6 < ht.Count; num6++)
                    {
                        strArray = ht[num6.ToString()].ToString().Split(new char[] { '|' });
                        strArray2 = strArray[0].Split(new char[] { '$' });
                        num7 = int.Parse(strArray2[0].ToString());
                        num8 = int.Parse(strArray2[1].ToString());
                        worksheet2.Cells[num7, num8] = strArray[1].ToString();
                    }
                }
                int num9 = (rowCountnum - rowsnum) + 1;
                for (num10 = 0; num10 < (count - num9); num10++)
                {
                    num5 = 0;
                    while (num5 < (num2 - 1))
                    {
                        worksheet2.Cells[top + num10, left + num5] = dt.Rows[num10 + num9][num5].ToString();
                        num5++;
                    }
                    this.sheet1++;
                }
                if ((count - num9) < rowCountnum)
                {
                    int num11 = count - num9;
                    if (num11 > 0)
                    {
                        for (num12 = num11; num12 < rowCountnum; num12++)
                        {
                            num5 = 0;
                            while (num5 < (num2 - 1))
                            {
                                worksheet2.Cells[top + num12, left + num5] = " ";
                                num5++;
                            }
                        }
                    }
                }
                if ((count - num9) > rowCountnum)
                {
                    num13 = (count - num9) % rowCountnum;
                    num14 = ((count - num9) + rowCountnum) - num13;
                    int num15 = count - num9;
                    if (num13 > 0)
                    {
                        for (num12 = num15; num12 < num14; num12++)
                        {
                            num5 = 0;
                            while (num5 < (num2 - 1))
                            {
                                worksheet2.Cells[top + num12, left + num5] = " ";
                                num5++;
                            }
                        }
                    }
                }
            }
            else
            {
                worksheet2 = (Worksheet) workbook.Worksheets.get_Item(1);
                worksheet2.Name = sheetPrefixName + "-1";
                if (ht.Count > 0)
                {
                    for (num6 = 0; num6 < ht.Count; num6++)
                    {
                        strArray = ht[num6.ToString()].ToString().Split(new char[] { '|' });
                        strArray2 = strArray[0].Split(new char[] { '$' });
                        num7 = int.Parse(strArray2[0].ToString());
                        num8 = int.Parse(strArray2[1].ToString());
                        worksheet2.Cells[num7, num8] = strArray[1].ToString();
                    }
                }
                for (num10 = 0; num10 < count; num10++)
                {
                    num5 = 0;
                    while (num5 < (num2 - 1))
                    {
                        worksheet2.Cells[top + num10, left + num5] = dt.Rows[num10][num5].ToString();
                        num5++;
                    }
                    this.sheet1++;
                }
                if (count < rowCountnum)
                {
                    for (num12 = count; num12 < rowCountnum; num12++)
                    {
                        num5 = 0;
                        while (num5 < (num2 - 1))
                        {
                            worksheet2.Cells[top + num12, left + num5] = " ";
                            num5++;
                        }
                    }
                }
                if (count > rowCountnum)
                {
                    num13 = count % rowCountnum;
                    num14 = (count + rowCountnum) - num13;
                    if (num13 > 0)
                    {
                        for (num12 = count; num12 < num14; num12++)
                        {
                            for (num5 = 0; num5 < (num2 - 1); num5++)
                            {
                                worksheet2.Cells[top + num12, left + num5] = " ";
                            }
                        }
                    }
                }
            }
            try
            {
                workbook.SaveAs(this.outputFile, this.missing, this.missing, this.missing, this.missing, this.missing, XlSaveAsAccessMode.xlExclusive, this.missing, this.missing, this.missing, this.missing);
                workbook.Close(null, null, null);
                o.Workbooks.Close();
                o.Application.Quit();
                o.Quit();
                Marshal.ReleaseComObject(worksheet);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(o);
                worksheet = null;
                workbook = null;
                o = null;
                GC.Collect();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                Process[] processesByName = Process.GetProcessesByName("Excel");
                foreach (Process process in processesByName)
                {
                    DateTime startTime = process.StartTime;
                    if ((startTime > now) && (startTime < time2))
                    {
                        process.Kill();
                    }
                }
            }
        }

        public void DataTableToExcel(System.Data.DataTable dt, int rows, int top, int left, int rowCountnum, int rowsnum, string sheetPrefixName, string topstr)
        {
            Worksheet worksheet2;
            int num5;
            int num7;
            int num9;
            int num10;
            int num11;
            int count = dt.Rows.Count;
            int num2 = dt.Columns.Count;
            if ((sheetPrefixName == null) || (sheetPrefixName.Trim() == ""))
            {
                sheetPrefixName = "Sheet";
            }
            DateTime now = DateTime.Now;
            Application o = new ApplicationClass {
                Visible = false
            };
            DateTime time2 = DateTime.Now;
            Workbook workbook = o.Workbooks.Open(this.templetFile, this.missing, this.missing, this.missing, this.missing, this.missing, this.missing, this.missing, this.missing, this.missing, this.missing, this.missing, this.missing);
            Worksheet worksheet = (Worksheet) workbook.Sheets.get_Item(1);
            if (rowsnum > 0)
            {
                worksheet2 = (Worksheet) workbook.Worksheets.get_Item(2);
                worksheet2.Name = sheetPrefixName + "-2";
                int num3 = 0;
                if (((rowsnum - 1) + count) < rowCountnum)
                {
                    num3 = count;
                }
                else
                {
                    num3 = (rowCountnum - rowsnum) + 1;
                }
                for (int i = 0; i < num3; i++)
                {
                    num5 = 0;
                    while (num5 < (num2 - 1))
                    {
                        worksheet2.Cells[((top + rowsnum) - 1) + i, left + num5] = dt.Rows[i][num5].ToString();
                        num5++;
                    }
                    this.sheet2++;
                }
                worksheet2 = (Worksheet) workbook.Worksheets.get_Item(1);
                worksheet2.Name = sheetPrefixName + "-1";
                if (topstr != "")
                {
                    worksheet2.Cells[6, 1] = topstr;
                }
                int num6 = (rowCountnum - rowsnum) + 1;
                for (num7 = 0; num7 < (count - num6); num7++)
                {
                    num5 = 0;
                    while (num5 < (num2 - 1))
                    {
                        worksheet2.Cells[top + num7, left + num5] = dt.Rows[num7 + num6][num5].ToString();
                        num5++;
                    }
                    this.sheet1++;
                }
                if ((count - num6) < rowCountnum)
                {
                    int num8 = count - num6;
                    if (num8 > 0)
                    {
                        for (num9 = num8; num9 < rowCountnum; num9++)
                        {
                            num5 = 0;
                            while (num5 < (num2 - 1))
                            {
                                worksheet2.Cells[top + num9, left + num5] = " ";
                                num5++;
                            }
                        }
                    }
                }
                if ((count - num6) > rowCountnum)
                {
                    num10 = (count - num6) % rowCountnum;
                    num11 = ((count - num6) + rowCountnum) - num10;
                    int num12 = count - num6;
                    if (num10 > 0)
                    {
                        for (num9 = num12; num9 < num11; num9++)
                        {
                            num5 = 0;
                            while (num5 < (num2 - 1))
                            {
                                worksheet2.Cells[top + num9, left + num5] = " ";
                                num5++;
                            }
                        }
                    }
                }
            }
            else
            {
                worksheet2 = (Worksheet) workbook.Worksheets.get_Item(1);
                worksheet2.Name = sheetPrefixName + "-1";
                if (topstr != "")
                {
                    worksheet2.Cells[6, 1] = topstr;
                }
                for (num7 = 0; num7 < count; num7++)
                {
                    num5 = 0;
                    while (num5 < (num2 - 1))
                    {
                        worksheet2.Cells[top + num7, left + num5] = dt.Rows[num7][num5].ToString();
                        num5++;
                    }
                    this.sheet1++;
                }
                if (count < rowCountnum)
                {
                    for (num9 = count; num9 < rowCountnum; num9++)
                    {
                        num5 = 0;
                        while (num5 < (num2 - 1))
                        {
                            worksheet2.Cells[top + num9, left + num5] = " ";
                            num5++;
                        }
                    }
                }
                if (count > rowCountnum)
                {
                    num10 = count % rowCountnum;
                    num11 = (count + rowCountnum) - num10;
                    if (num10 > 0)
                    {
                        for (num9 = count; num9 < num11; num9++)
                        {
                            for (num5 = 0; num5 < (num2 - 1); num5++)
                            {
                                worksheet2.Cells[top + num9, left + num5] = " ";
                            }
                        }
                    }
                }
            }
            try
            {
                workbook.SaveAs(this.outputFile, this.missing, this.missing, this.missing, this.missing, this.missing, XlSaveAsAccessMode.xlExclusive, this.missing, this.missing, this.missing, this.missing);
                workbook.Close(null, null, null);
                o.Workbooks.Close();
                o.Application.Quit();
                o.Quit();
                Marshal.ReleaseComObject(worksheet);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(o);
                worksheet = null;
                workbook = null;
                o = null;
                GC.Collect();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                Process[] processesByName = Process.GetProcessesByName("Excel");
                foreach (Process process in processesByName)
                {
                    DateTime startTime = process.StartTime;
                    if ((startTime > now) && (startTime < time2))
                    {
                        process.Kill();
                    }
                }
            }
        }

        public void InvokeExcelPrint(string strFilePath, int num)
        {
            ApplicationClass o = new ApplicationClass();
            object saveChanges = Missing.Value;
            if (!File.Exists(strFilePath))
            {
                throw new FileNotFoundException();
            }
            try
            {
                o.Visible = true;
                Workbooks target = o.Workbooks;
                Type type = target.GetType();
                MethodInfo[] methods = type.GetMethods();
                object obj2 = strFilePath;
                Workbook workbook = (Workbook) type.InvokeMember("Open", BindingFlags.InvokeMethod, null, target, new object[] { obj2, true, true });
                if ((num > 0) && (this.sheet2 > 0))
                {
                    Worksheet worksheet = (Worksheet) workbook.Worksheets["页-2"];
                    worksheet.PrintPreview(true);
                }
                if (this.sheet1 > 0)
                {
                    ((Worksheet) workbook.Worksheets["页-1"]).PrintPreview(true);
                }
                o.Visible = false;
                workbook.Close(saveChanges, saveChanges, saveChanges);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (o != null)
                {
                    o.Quit();
                    Marshal.ReleaseComObject(o);
                    o = null;
                }
                GC.Collect();
            }
        }
    }
}

