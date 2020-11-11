using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class StaticsFrm : FormDo
    {
        #region 变量
        private DataSet         dsDeptDict      = null;                     // 科室字典
        private DataSet         dsResult        = null;                     // 查询结果
        
        private ExcelConfigData excelConfig     = new ExcelConfigData();    // Excel 打印配置
        private ExcelAccess     excelAccess     = new ExcelAccess();        // Excel 接口
        protected string        _template       = "护理工作量统计";         // 模板文件
        protected string        _querySql       = "WorkLoadQuery";          // 查询SQL语句
        private DataSet         dsPrint         = null;                     // 打印信息        
        #endregion

        public StaticsFrm()
        {
            InitializeComponent();
        }
        
        
        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkLoadStaticsFrm_Load(object sender, EventArgs e)
        {
            try
            {
                initFrmVal();

                cmbDept.DisplayMember = "DEPT_NAME";
                cmbDept.ValueMember   = "DEPT_CODE";
                
                cmbDept.DataSource    = dsDeptDict.Tables[0].DefaultView;
                cmbDept.SelectedValue = GVars.User.DeptCode;
                cmbDept.Enabled       = (cmbDept.SelectedValue.ToString().Equals(GVars.User.DeptCode) == false);                
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 科室改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {                
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[查询]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                
                dsResult = queryData();
                dsResult = dataTreate();
                
                dgvData.DataSource = dsResult.Tables[0].DefaultView;

                btnPrint.Enabled = (dsResult != null && dsResult.Tables[0].Rows.Count > 0);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[打印]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // 获取打印信息
                getPrintInfo();
                
                if (excelConfig.ConfigSections.Count == 0)
                {
                    loadExcelPrintConfig(_template);
                }

                if (excelConfig.ConfigSections.Count > 0)
                {
                    excelTemplatePrint();
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[退出]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
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
        private void initFrmVal()
        {
            string sql = "SELECT * FROM DEPT_DICT WHERE CLINIC_ATTR = '2' AND OUTP_OR_INP = '1' ";
            dsDeptDict = GVars.OracleAccess.SelectData(sql, "DEPT_DICT");
        }
        
        
        /// <summary>
        /// 数据处理
        /// </summary>
        /// <returns></returns>
        private DataSet dataTreate()
        {
            if (dsResult == null) return  null;
            
            // 增加一个合计
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsResult.Tables[0].NewRow();
                
                dr["护士"] = "合计";
                
                foreach(DataColumn dc in dsResult.Tables[0].Columns)
                {
                    if (dc.ColumnName.Equals("护士") == true) continue;
                    dr[dc.ColumnName] = getColSum(dc.ColumnName);
                }
                
                dsResult.Tables[0].Rows.Add(dr);
            }
            
            return dsResult;
        }
        
        
        /// <summary>
        /// 查询某一列的合计
        /// </summary>
        /// <returns></returns>
        private long getColSum(string colName)
        {
            long sum = 0;
            foreach(DataRow dr in dsResult.Tables[0].Rows)
            {
                if (dr[colName] == DBNull.Value) continue;
                sum += long.Parse(dr[colName].ToString());
            }
            
            return sum;
        }
        
        
        /// <summary>
        /// 获取文件内容, 去掉文件中的注释
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string getSqlContent(string fileName)
        {
            // 条件检查
            if (fileName.IndexOf(".") < 0)
            {
                fileName += ".sql";
            }

            fileName = Path.Combine(Path.Combine(Application.StartupPath, "SQL"), fileName);

            if (File.Exists(fileName) == false)
            {
                throw new Exception(fileName + "不存在!");
            }

            // 获取文件内容
            StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("gb2312"));
            string content = sr.ReadToEnd();
            sr.Close();

            content = content.Replace("\r", string.Empty);
            content = content.Replace("\n", ComConst.STR.BLANK);

            // 截除注释
            int pos = content.IndexOf(@"//");

            if (pos == -1)
            {
                return content;
            }
            else if (pos < 1)
            {
                return string.Empty;
            }
            else
            {
                return content.Substring(0, pos);
            }
        }        
        #endregion

        
        #region 数据交互
        /// <summary>
        /// 获取护士列表
        /// </summary>
        /// <returns></returns>
        private DataSet getNurseList(string wardCode)
        {
            // 查找病区对应的科室
            string sql = "SELECT DEPT_CODE FROM DEPT_VS_WARD WHERE WARD_CODE = " + SQL.SqlConvert(wardCode);
            DataSet ds = GVars.OracleAccess.SelectData(sql);
            string deptList = string.Empty;
            
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                if (deptList.Length > 0) deptList += "','";
                deptList += dr["DEPT_CODE"].ToString();
            }
            
            if (deptList.Length == 0) return null;
            
            if (deptList.Length > 0) deptList = "('" + deptList + "')";
            
            // 查找护士列表
            sql = "SELECT * FROM STAFF_DICT WHERE DEPT_CODE IN " + deptList;
            return GVars.OracleAccess.SelectData(sql);
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        private DataSet queryData()
        {
            if (cmbDept.SelectedValue == null) return null;
            
            // 查询数据
            string sql = getSqlContent(_querySql + ".sql");
            
            sql = sql.Replace("{DATE_BEGIN}", SQL.GetOraDbDate_Short(dtpBegin.Value));
            sql = sql.Replace("{DATE_END}", SQL.GetOraDbDate_Short(dtpEnd.Value.AddDays(1)));
            sql = sql.Replace("{DEPT_CODE}", SQL.GetOraDbDate_Short(cmbDept.SelectedValue.ToString()));
            
            DataSet ds = GVars.OracleAccess.SelectData_NoKey(sql);

            return ds;
        }
                
        #endregion
        
        
        #region 打印有关
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
                    
                    case "WORK_LOAD":
                        if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                        {
                            drData = dsResult.Tables[0].Rows[0];
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
            DataRow[] drFind = dsResult.Tables[0].Select(string.Empty);
            
            // 进行多行数据输出
            int rowIndex = 0;
            for (int i = 0; i < drFind.Length; i++)
            {
                DataRow dr = drFind[i];
                
                // 输出多行项目
                for (int k = 0; k < excelConfig.ConfigSections.Count; k++)
                {
                    ExcelConfigSection configSection = excelConfig.ConfigSections[k];
                    if (configSection.MultiRows == false) continue;
                    
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
            // 如果是条码
            if (colName.Equals(ExcelConfigData.STR_BARCODE) == true)
            {
                if (tagInfo.Trim().Length == 0) return;

                // 获取条码值
                string barCode = string.Empty;
                string[] parts = tagInfo.Split("+".ToCharArray());
                for (int i = 0; i < parts.Length; i++)
                {
                    colName = parts[i].Trim();

                    if (colName.StartsWith(@"'") && colName.EndsWith(@"'") && colName.Length > 2)
                    {
                        barCode += colName.Substring(1, colName.Length - 2);
                        continue;
                    }

                    if (dr.Table.Columns.Contains(colName) == false) return;
                    if (dr[colName] == DBNull.Value) return;

                    barCode += dr[colName].ToString();
                }

                return;
            }

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
                dt.Columns.Add("DATE_BEGIN", typeof(DateTime));
                dt.Columns.Add("DATE_END", typeof(DateTime));
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

            drEdit["DATE_BEGIN"]    = dtpBegin.Value;
            drEdit["DATE_END"]      = dtpEnd.Value;
            drEdit["DEPT_NAME"]     = GVars.User.DeptName;
            
            if (dsPrint.Tables[0].Rows.Count == 0)
            {
                dsPrint.Tables[0].Rows.Add(drEdit);
            }
        }
        #endregion          
    }
}
