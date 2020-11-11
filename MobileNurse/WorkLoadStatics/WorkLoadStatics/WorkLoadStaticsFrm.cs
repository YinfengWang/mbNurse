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
    public partial class WorkLoadStaticsFrm : FormDo
    {
        #region 变量
        private DataSet dsDeptDict = null;                     // 科室字典
        private DataSet dsStaff = null;                     // 工作人员
        private DataSet dsResult = null;                     // 查询结果 
        private DataSet dsConfig = null;                    //配置参数



        private ExcelConfigData excelConfig = new ExcelConfigData();    // Excel 打印配置
        private ExcelAccess excelAccess = new ExcelAccess();        // Excel 接口
        protected string _template = string.Empty;       // 模板文件
        protected string _querySql = string.Empty;        // 查询SQL语句
        private DataSet dsPrint = null;                     // 打印信息  

        private string CONFIGPARAMETERS = string.Empty;             // 参数配置

        #endregion

        #region  初始化
        public WorkLoadStaticsFrm()
        {
            InitializeComponent();

            _id = "00070";
            _guid = "BDC33AF5-053F-4ba0-B75A-8167860A6BA0";
        }
        #endregion

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

                SetGridViewStyle();
                BindWardList();
                BindUcCmbType();
                BindCmbYzChildType();
                //initFrmVal();

                //cmbWardCode.DisplayMember = "DEPT_NAME";
                //cmbWardCode.ValueMember = "DEPT_CODE";

                //cmbWardCode.DataSource = dsDeptDict.Tables[0].DefaultView;

                //if (GVars.User.DeptCode.Length == 0)
                //{
                //    cmbWardCode.Enabled = true;
                //}
                //else
                //{
                //    cmbWardCode.SelectedValue = GVars.User.DeptCode;
                //    cmbWardCode.Enabled = (cmbWardCode.SelectedValue.ToString().Equals(GVars.User.DeptCode) == false);
                //}
                //cmbSql.DisplayMember = "CONFIGNAME";
                //cmbSql.ValueMember = "CONFIGPARAMETERS";

                //cmbSql.DataSource = dsConfig.Tables[0].DefaultView;

                //// 默认选中首项
                //if (dsConfig.Tables[0].Rows.Count > 0)
                //    cmbSql.SelectedIndex = 0;

                //ucGridView1.Init();

                //btnQuery.PerformClick();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 设置GridView样式信息
        /// </summary>
        private void SetGridViewStyle()
        {
            gridView1.OptionsView.ShowGroupPanel = false;   //去掉默认字

            gridView1.IndicatorWidth = 40;
            gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;  //取消选中时的虚拟框

            //gridView1.OptionsView.ColumnAutoWidth = false;   //不设置自动列宽

            gridView1.OptionsBehavior.Editable = false;      //单元格不可编辑

            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect; //选择行，不选择单元格

            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;


            this.gridView1.Appearance.OddRow.BackColor = Color.White;  // 设置奇数行颜色 // 默认也是白色 可以省略 
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;   // 使能 // 和和上面绑定 同时使用有效 
            this.gridView1.Appearance.EvenRow.BackColor = Color.WhiteSmoke; // 设置偶数行颜色 
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;   // 使能 // 和和上面绑定 同时使用有效

            gridView1.OptionsMenu.EnableColumnMenu = false;
            gridView1.OptionsMenu.EnableFooterMenu = false;
            gridView1.OptionsMenu.EnableGroupPanelMenu = false;
        }

        /// <summary>
        /// 科室改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    dsStaff = getNurseList(cmbWardCode.SelectedValue.ToString());
            //}
            //catch (Exception ex)
            //{
            //    Error.ErrProc(ex);
            //}
        }


        /// <summary>
        /// 按钮[查询]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            #region MyRegion

            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;

            //    dsResult = queryData();
            //    if (dsResult != null)
            //    {
            //        dsResult = dataTreate();
            //        ucGridView1.DataSource = dsResult.Tables[0].DefaultView;
            //        btnPrint.Enabled = (dsResult != null && dsResult.Tables[0].Rows.Count > 0);
            //    }
            //    else
            //    {
            //        ucGridView1.DataSource = null;
            //        btnPrint.Enabled = false;
            //    }
            //    this.Cursor = Cursors.Default;
            //}
            //catch (Exception ex)
            //{
            //    Error.ErrProc(ex);
            //}

            #endregion

            //病区代码
            string wardCode = GVars.User.DeptCode;

            //类型
            string stype = ucCmbType.SelectedValue.ToString().ToUpper();

            //子类
            string stypeChild = string.Empty;

            if (stype == "HLXM")
            {
                stypeChild = ucCmbChildType.SelectedIndex.ToString();  //此处需修改
            }
            else if (stype == "YZ")
            {
                stypeChild = ucCmbChildType.SelectedValue.ToString();
            }
            //开始时间
            string startDate = dtpBegin.Value.Date.ToString("yyyy-MM-dd") + " 00:00:01";
            //结束时间
            string endDate = dtpEnd.Value.Date.ToString("yyyy-MM-dd") + " 23:59:59";

            //根据条件，返回查询的SQL
            string strTypeSql = BindGridViewData(stype, wardCode, stypeChild, startDate, endDate);

            try
            {
                //重新绑定数据源，需清除原先的列
                gridView1.Columns.Clear();
                DataSet ds = GVars.OracleAccess.SelectData(strTypeSql);
                gCtlStatice.DataSource = ds.Tables[0];
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    btnPrint.Enabled = true;
                }
                else
                {
                    btnPrint.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                btnPrint.Enabled = true;
                throw;
            }


        }

        /// <summary>
        /// 返回查询执行的SQL
        /// </summary>
        /// <param name="sType"></param>
        /// <param name="wardCode"></param>
        /// <param name="stypeChild"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private string BindGridViewData(string sType, string wardCode, string stypeChild, string startDate, string endDate)
        {
            //返回的SQL
            string sqlValue = string.Empty;

            sType = sType.ToUpper();
            if (sType == "YZ")
            {
                DataSet dtClassTypeValue = GVars.OracleAccess.SelectData("SELECT * FROM ORDER_SUB_CLASS_DICT WHERE CLASS_CODE='" + stypeChild + "' ");

                //根据子类获取ClassType
                string classType = (dtClassTypeValue != null && dtClassTypeValue.Tables[0].Rows.Count > 0) ? dtClassTypeValue.Tables[0].Rows[0]["CLASS_TYPE"].ToString() : "";

                //根据子类获取ClassID
                string classID = (dtClassTypeValue != null && dtClassTypeValue.Tables[0].Rows.Count > 0) ? dtClassTypeValue.Tables[0].Rows[0]["CLASS_ID"].ToString() : "";

                #region 医嘱工作量统计SQL

                if (classType == "1")
                {
                    #region 1

                    //sqlValue = @"SELECT M.NURSE,OSCD.CLASS_NAME,M.ADNAME,M.CNT         " +
                    //                        "    FROM MOBILE.ORDER_SUB_CLASS_DICT OSCD,          " +
                    //                        "    (SELECT OE.EXECUTE_NURSE NURSE,OE.ADMINISTRATION ADNAME,COUNT(*) CNT     " +
                    //                        "    FROM MOBILE.ORDERS_EXECUTE OE ,ADMINISTRATION_DICT AD             " +
                    //                        "    WHERE OE.IS_EXECUTE='1' AND    " +
                    //                        "    TO_DATE(TO_CHAR(OE.EXECUTE_DATE_TIME,'YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')>=TO_DATE('" + startDate + "','YYYY-MM-DD HH24:MI:SS')    " +
                    //                        "    AND TO_DATE(TO_CHAR(OE.EXECUTE_DATE_TIME,'YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')<=TO_DATE('" + endDate + "','YYYY-MM-DD HH24:MI:SS') AND OE.WARD_CODE='1001N'     " +
                    //                        "    AND OE.ADMINISTRATION = AD.ADMINISTRATION_NAME    " +
                    //                        "    AND AD.CLASS_ID=1       " +
                    //                        "    GROUP BY OE.EXECUTE_NURSE,OE.ADMINISTRATION) M          " +
                    //                        "    WHERE OSCD.CLASS_ID=" + classID + "";

                    sqlValue = @" SELECT OE.EXECUTE_NURSE 护士,OE.ADMINISTRATION 类别,COUNT(*) 人次     " +
                                            "    FROM MOBILE.ORDERS_EXECUTE OE ,ADMINISTRATION_DICT AD             " +
                                            "    WHERE OE.IS_EXECUTE='1' AND    " +
                                            "    TO_DATE(TO_CHAR(OE.EXECUTE_DATE_TIME,'YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')>=TO_DATE('" + startDate + "','YYYY-MM-DD HH24:MI:SS')    " +
                                            "    AND TO_DATE(TO_CHAR(OE.EXECUTE_DATE_TIME,'YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')<=TO_DATE('" + endDate + "','YYYY-MM-DD HH24:MI:SS') AND OE.WARD_CODE='1001N'     " +
                                            "    AND OE.ADMINISTRATION = AD.ADMINISTRATION_NAME    " +
                                            "    AND AD.CLASS_ID=1       " +
                                            "    GROUP BY OE.EXECUTE_NURSE,OE.ADMINISTRATION";

                    #endregion

                }
                else
                {
                    #region 2

                    //非药疗
                    //sqlValue = @"SELECT NURSE,OSCD.CLASS_NAME,ADNAME,CNT  " +
                    //            "    FROM MOBILE.ORDER_SUB_CLASS_DICT OSCD,  " +
                    //            "    (SELECT OE.EXECUTE_NURSE NURSE,OE.ADMINISTRATION ADNAME,COUNT(*) CNT FROM MOBILE.ORDERS_EXECUTE OE  " +
                    //            "    WHERE OE.IS_EXECUTE='1' AND TO_DATE(TO_CHAR(OE.EXECUTE_DATE_TIME,'YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')>=TO_DATE('" + startDate + "','YYYY-MM-DD HH24:MI:SS')  " +
                    //            "    AND TO_DATE(TO_CHAR(OE.EXECUTE_DATE_TIME,'YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')<=TO_DATE('" + endDate + "','YYYY-MM-DD HH24:MI:SS') AND OE.WARD_CODE='1001N'  " +
                    //            "    AND OE.ORDER_CODE IN (SELECT NMSI.ITEM_CODE FROM NO_MEDICATION_STATISTICS_ITEM NMSI WHERE NMSI.CLASS_ID=4)   " +
                    //            "    GROUP BY OE.EXECUTE_NURSE,OE.ADMINISTRATION)  " +
                    //            "   WHERE OSCD.CLASS_ID=" + classID + "";

                    sqlValue = @" SELECT OE.EXECUTE_NURSE AS 护士,OE.ADMINISTRATION AS 类别,COUNT(*)  AS 人次 FROM MOBILE.ORDERS_EXECUTE OE  " +
                               "    WHERE OE.IS_EXECUTE='1' AND TO_DATE(TO_CHAR(OE.EXECUTE_DATE_TIME,'YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')>=TO_DATE('" + startDate + "','YYYY-MM-DD HH24:MI:SS')  " +
                               "    AND TO_DATE(TO_CHAR(OE.EXECUTE_DATE_TIME,'YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')<=TO_DATE('" + endDate + "','YYYY-MM-DD HH24:MI:SS') AND OE.WARD_CODE='1001N'  " +
                               "    AND OE.ORDER_CODE IN (SELECT NMSI.ITEM_CODE FROM NO_MEDICATION_STATISTICS_ITEM NMSI WHERE NMSI.CLASS_ID=4)   " +
                               "    GROUP BY OE.EXECUTE_NURSE,OE.ADMINISTRATION ";

                    #endregion
                }

                #endregion
            }
            else if (sType == "HLXM")
            {
                #region 护理项目统计SQL

                //sqlValue = @"SELECT V.NURSE AS 护士,NID.VITAL_SIGNS AS 名称,COUNT(V.VISIT_ID) AS 人次   " +
                //            "FROM VITAL_SIGNS_REC V,NURSING_ITEM_DICT NID   " +
                //            "WHERE NID.IS_STATISTICS=1   " +
                //            "AND V.VITAL_CODE=NID.VITAL_CODE   " +
                //            "AND TO_DATE(TO_CHAR(V.TIME_POINT,'YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')>=TO_DATE('" + startDate + "','YYYY-MM-DD HH24:MI:SS')   " +
                //            "AND TO_DATE(TO_CHAR(V.TIME_POINT,'YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')<=TO_DATE('" + endDate + "','YYYY-MM-DD HH24:MI:SS')   " +
                //            "AND NID.ATTRIBUTE='" + stypeChild + "' AND V.WARD_CODE='" + GVars.User.DeptCode + "'   " +
                //            "GROUP BY V.NURSE,NID.VITAL_SIGNS";
                sqlValue = @" select b.nurse 护士,b.vital_signs 名称,count(1) 人次 from VITAL_SIGNS_REC b   " +
                                          "       where b.ward_code='" + GVars.User.DeptCode + "' and   " +
                                          "        TO_DATE(TO_CHAR(b.TIME_POINT, 'YYYY-MM-DD HH24:MI:SS'),   " +
                                          "                     'YYYY-MM-DD HH24:MI:SS') >=   " +
                                          "             TO_DATE('" + startDate + "', 'YYYY-MM-DD HH24:MI:SS')   " +
                                          "         AND TO_DATE(TO_CHAR(b.TIME_POINT, 'YYYY-MM-DD HH24:MI:SS'),   " +
                                          "                     'YYYY-MM-DD HH24:MI:SS') <=   " +
                                          "             TO_DATE('" + endDate + "', 'YYYY-MM-DD HH24:MI:SS')   group by b.nurse,b.vital_signs";


                #endregion
            }
            else if (sType == "HLXS")
            {
                #region 护理巡视统计SQL

                sqlValue = @"SELECT A.NURSE AS 护士,COUNT(1) AS 人次 FROM XUNSHI A WHERE A.WARD_CODE='" + GVars.User.DeptCode + "'  " +
                                  "  AND EXECUTE_DATE  " +
                                  "   BETWEEN TO_DATE('" + startDate + "','YYYY-MM-DD HH24:MI:SS')   " +
                                  "   AND  TO_DATE('" + endDate + "','YYYY-MM-DD HH24:MI:SS')  " +
                                  " GROUP BY A.NURSE";

                #endregion
            }
            else if (sType == "BBCJ")
            {
                #region 标本采集统计SQL

                sqlValue = @"SELECT S.GATHER_NURSE 护士,SD.SPECIMAN_NAME 项目,COUNT(*) 人次   " +
                            "    FROM SPECIMENT_FLOW_REC S,SPECIMAN_DICT SD   " +
                            "    WHERE TO_DATE(TO_CHAR(S.GATHER_TIME,'YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')>=TO_DATE('" + startDate + "','YYYY-MM-DD HH24:MI:SS')   " +
                            "    AND TO_DATE(TO_CHAR(S.GATHER_TIME,'YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')<=TO_DATE('" + endDate + "','YYYY-MM-DD HH24:MI:SS')   " +
                            "    AND S.SPECIMEN_ID=SD.SPECIMAN_CODE    " +  //AND SD.DEPT_CODE='" + GVars.User.DeptCode + "'
                            "    GROUP BY S.GATHER_NURSE,SD.SPECIMAN_NAME";

                #endregion
            }

            return sqlValue;
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

        #endregion

        #region 共通函数
        /// <summary>
        /// 初始化窗体变量
        /// </summary>
        private void initFrmVal()
        {


            // 加载 科室
            string sql = "SELECT * FROM DEPT_DICT WHERE CLINIC_ATTR = '2'";
            dsDeptDict = GVars.OracleAccess.SelectData(sql, "DEPT_DICT");

            //加载 科室对应的参数配置数据

            string sqlCS = "select  * from  configuration  where dept_code =" + SQL.SqlConvert(GVars.User.DeptCode);
            dsConfig = GVars.OracleAccess.SelectData(sqlCS, "configuration");


        }




        /// <summary>
        /// 数据处理
        /// </summary>
        /// <returns></returns>
        private DataSet dataTreate()
        {
            if (dsStaff == null) return null;
            if (dsResult == null) return null;

            string filter = "NAME = '{0}'";
            DataRow[] drFind = dsResult.Tables[0].Select(string.Empty);
            for (int i = 0; i < drFind.Length; i++)
            {
                string nurse = drFind[i][0].ToString();


                DataRow[] drFind2 = dsStaff.Tables[0].Select(string.Format(filter, nurse));
                if (drFind2.Length == 0)
                {
                    drFind[i].Delete();
                }
            }

            dsResult.AcceptChanges();

            // 增加一个合计
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsResult.Tables[0].NewRow();

                dr[0] = "合计";

                foreach (DataColumn dc in dsResult.Tables[0].Columns)
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
            foreach (DataRow dr in dsResult.Tables[0].Rows)
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


            string sqlcon = string.Empty;
            // 条件检查
            if (fileName.IndexOf(".") < 0)
            {
                fileName += ".sql";
            }

            fileName = Path.Combine(Path.Combine(Application.StartupPath, "SQL"), fileName);


            if (File.Exists(fileName) == false)
            {
                MessageBox.Show(fileName + "不存在!");
            }
            else
            {
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
                    sqlcon = content;
                }
                else if (pos < 1)
                {
                    sqlcon = string.Empty;
                }
                else
                {
                    sqlcon = content.Substring(0, pos);
                }
            }
            return sqlcon;
        }


        /// <summary>
        /// 获取过滤条件
        /// </summary>
        /// <returns></returns>
        private string getFilter_NurseList(string colName, string deptCode)
        {
            string nurseList = string.Empty;
            DataRow[] drFind = dsStaff.Tables[0].Select(string.Empty);
            if (drFind.Length == 0)
            {
                return "(1 = 2)";
            }

            for (int i = 0; i < drFind.Length; i++)
            {
                if (nurseList.Length > 0) nurseList += "','";
                nurseList += drFind[i]["NAME"].ToString();
            }

            return colName + " IN " + "('" + nurseList + "') ";
        }
        #endregion

        #region 数据交互
        /// <summary>
        /// 获取护士列表
        /// </summary>
        /// <returns></returns>
        private DataSet getNurseList(string wardCode)
        {
            string sql = "SELECT * FROM STAFF_DICT WHERE DEPT_CODE = " + SQL.SqlConvert(wardCode);
            return GVars.OracleAccess.SelectData(sql);
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        private DataSet queryData()
        {
            DataSet ds = null;
            // 查询数据
            if (_querySql.Length > 0)
            {
                string sql = getSqlContent(_querySql + ".sql");
                if (sql.Length > 0)
                {

                    sql = sql.Replace("{DATE_BEGIN}", SQL.GetOraDbDate_Short(dtpBegin.Value));
                    sql = sql.Replace("{DATE_END}", SQL.GetOraDbDate_Short(dtpEnd.Value.AddDays(1)));
                    ds = GVars.OracleAccess.SelectData_NoKey(sql);
                }

            }
            else
            {
                MessageBox.Show("你选择的目标SQL不存在");
            }


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
            else
            {
                MessageBox.Show("对不起！" + _template + "模板不存在");
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
            DataRow[] drFind = dsResult.Tables[0].Select(dsResult.Tables[0].DefaultView.RowFilter);

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

            drEdit["DATE_BEGIN"] = dtpBegin.Value;
            drEdit["DATE_END"] = dtpEnd.Value;
            drEdit["DEPT_NAME"] = GVars.User.DeptName;

            if (dsPrint.Tables[0].Rows.Count == 0)
            {
                dsPrint.Tables[0].Rows.Add(drEdit);
            }
        }
        #endregion

        #region 统计项目变化时
        private void cmbSql_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;

            //    CONFIGPARAMETERS = cmbSql.SelectedValue.ToString();
            //    _querySql = CONFIGPARAMETERS;
            //    _template = cmbSql.Text;
            //    this.Cursor = Cursors.Default;
            //}
            //catch (Exception ex)
            //{
            //    this.Cursor = Cursors.Default;
            //    Error.ErrProc(ex);
            //}
        }
        #endregion

        /// <summary>
        /// 获取病区列表
        /// </summary>
        /// <param name="wardCode"></param>
        /// <returns></returns>
        private DataTable DtWardCode(string wardCode = "")
        {
            string strWardCodeSql = "SELECT * FROM DEPT_DICT WHERE CLINIC_ATTR = '2'";
            if (!string.IsNullOrEmpty(wardCode))
            {
                strWardCodeSql += " AND DEPT_CODE='" + GVars.User.DeptCode + "'";
            }
            DataTable dtWardCode = new DataTable();
            DataSet dsWardCode = GVars.OracleAccess.SelectData(strWardCodeSql, "DEPT_DICT");
            if (dsWardCode != null && dsWardCode.Tables[0].Rows.Count > 0)
                dtWardCode = dsWardCode.Tables[0];
            return dtWardCode;
        }

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        private void BindWardList()
        {
            this.cmbWardCode.Items.Clear();
            cmbWardCode.DropDownStyle = ComboBoxStyle.DropDownList;
            DataTable dt = DtWardCode(GVars.User.DeptCode);
            //this.cmbWardCode.Properties.Items.Add("--请选择--");

            if (dt != null && dt.Rows.Count > 0)
            {
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    cmbWardCode.Items.Add(dt.Rows[i]["DEPT_NAME"].ToString());
                //}
                //this.cmbWardCode1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                this.cmbWardCode.DataSource = dt;
                this.cmbWardCode.DisplayMember = "DEPT_NAME";
                this.cmbWardCode.ValueMember = "DEPT_CODE";
            }

            cmbWardCode.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定分类
        /// </summary>
        private void BindUcCmbType()
        {
            this.ucCmbType.Items.Clear();
            this.ucCmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            string cmbTypeSql = "SELECT * FROM NURSING_CATEGORY_DICT WHERE ENABLED=1";
            DataSet dsType = GVars.OracleAccess.SelectData(cmbTypeSql, "NURSING_CATEGORY_DICT");
            DataTable dtType = (dsType != null && dsType.Tables[0].Rows.Count > 0) ? dsType.Tables[0] : null;
            if (dtType != null && dtType.Rows.Count > 0)
            {
                this.ucCmbType.DataSource = dtType;
                this.ucCmbType.DisplayMember = "CNAME";
                this.ucCmbType.ValueMember = "CCODE";
                //this.ucCmbType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                ucCmbType.SelectedIndex = 0;
            }

        }

        ///// <summary>
        ///// 绑定子类
        ///// </summary>
        //private void BindUcCmbChildType()
        //{
        //    this.ucCmbChildType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        //    ucCmbChildType.SelectedIndex = 0;
        //}

        /// <summary>
        /// Grid显示行数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        /// <summary>
        /// 隐藏子分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucCmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ucCmbType.SelectedValue.ToString() == "yz")
            {
                BindCmbYzChildType();
            }
            if (ucCmbType.SelectedValue.ToString() == "hlxm")
            {
                BindCmbHlxmChild();
            }
            if (ucCmbType.SelectedValue.ToString() == "hlxs" || ucCmbType.SelectedValue.ToString() == "bbcj")
            {
                ucCmbChildType.Visible = false;
                lblChild.Visible = false;
            }
            else
            {
                ucCmbChildType.Visible = true;
                lblChild.Visible = true;
            }
        }

        /// <summary>
        /// 绑定医嘱子类
        /// </summary>
        private void BindCmbYzChildType()
        {
            this.ucCmbChildType.DataSource = null;
            this.ucCmbChildType.Items.Clear();
            this.ucCmbChildType.DropDownStyle = ComboBoxStyle.DropDownList;
            string cmbChildTypeSql = "SELECT * FROM ORDER_SUB_CLASS_DICT";
            DataSet dsTypeChild = GVars.OracleAccess.SelectData(cmbChildTypeSql);
            DataTable dtTypeChild = (dsTypeChild != null && dsTypeChild.Tables[0].Rows.Count > 0) ? dsTypeChild.Tables[0] : null;
            if (dtTypeChild != null && dtTypeChild.Rows.Count > 0)
            {
                //ucCmbChildType.Items.Add("--全部--");

                this.ucCmbChildType.DataSource = dtTypeChild;
                this.ucCmbChildType.DisplayMember = "CLASS_NAME";
                this.ucCmbChildType.ValueMember = "CLASS_CODE";
                //this.ucCmbType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

                ucCmbChildType.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 绑定护理项目子类
        /// </summary>
        private void BindCmbHlxmChild()
        {
            this.ucCmbChildType.DataSource = null;
            this.ucCmbChildType.Items.Clear();
            this.ucCmbChildType.DropDownStyle = ComboBoxStyle.DropDownList;
            ucCmbChildType.Items.Insert(0, "生命体征");
            //ucCmbChildType.Items.Insert(1, "入量");
            //ucCmbChildType.Items.Insert(2, "出量");
            //ucCmbChildType.Items.Insert(3, "其他计量");
            //ucCmbChildType.Items.Insert(4, "护理事件");
            ucCmbChildType.SelectedIndex = 0;


            //            string cmbChildTypeSql = @"SELECT V.NURSE,NID.VITAL_SIGNS,COUNT(V.VISIT_ID)
            //                                        FROM VITAL_SIGNS_REC V,NURSING_ITEM_DICT NID
            //                                        WHERE NID.IS_STATISTICT=1
            //                                        AND V.VITAL_CODE=NID.VITAL_CODE
            //                                        AND TO_DATE(TO_CHAR(V.TIME_POINT,'YYYY-MM-DD'),'YYYY-MM-DD')>=TO_DATE('2013-01-01','YYYY-MM-DD')
            //                                        AND TO_DATE(TO_CHAR(V.TIME_POINT,'YYYY-MM-DD'),'YYYY-MM-DD')<=TO_DATE('2013-12-31','YYYY-MM-DD')
            //                                        AND NID.ATTRIBUTE='0' AND V.WARD_CODE='1001N'
            //                                        GROUP BY V.NURSE,NID.VITAL_SIGNS";
            //            DataSet dsTypeChild = GVars.OracleAccess.SelectData(cmbChildTypeSql);
            //            DataTable dtTypeChild = (dsTypeChild != null && dsTypeChild.Tables[0].Rows.Count > 0) ? dsTypeChild.Tables[0] : null;
            //            if (dtTypeChild != null && dtTypeChild.Rows.Count > 0)
            //            {
            //                this.ucCmbChildType.DataSource = dtTypeChild;
            //                this.ucCmbChildType.DisplayMember = "CLASS_NAME";
            //                this.ucCmbChildType.ValueMember = "CLASS_CODE";
            //                ucCmbChildType.SelectedIndex = 0;
            //            }
        }
    }
}
