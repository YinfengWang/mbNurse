using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraGrid.Columns;
using DXApplication2;
using HISPlus.UserControls;

namespace HISPlus
{
    public partial class HealthEduRec : FormDo, IBasePatient, IGridCellChecked
    {
        #region 变量
        private const string RIGHT_EDIT = "2";              //  编辑别人录入记录的权限
        private string patientId = string.Empty;     // 病人ID号
        private string visitId = string.Empty;     // 本次就诊序号
        private HealthEduDbI healthDbI = null;             // 健康教育接口
        private PatientDbI patientDbI = null;             // 病人接口
        private DataSet dsPatient = null;             // 病人信息
        private DataSet dsRec = null;             // 健康教育记录

        private HISDictDbI dictDbI = null;         // 数据字典接口
        private DataSet dsEdu_Pre = null;         // 教育前准备情况
        private DataSet dsEdut_Method = null;         // 教育方法
        private DataSet dsEdu_Master = null;         // 教育掌握情况
        private DataSet dsEdu_Clog = null;         // 教育障碍
        private ArrayList arrExcelItem = new ArrayList();
        private ExcelAccess excelAccess = new ExcelAccess();
        #endregion

        #region 结构
        struct ExcelItem
        {
            public int Row;
            public int Col;
            public string ItemId;
            public string CheckValue;
        }
        #endregion

        public HealthEduRec()
        {
            InitializeComponent();

            _id = "00047";
            _guid = "CBEFDD0F-E38A-4074-821F-AB2F6CFE53BB";
        }



        #region 共通函数
        /// <summary>
        /// 初始化窗体变量
        /// </summary>
        private void initFrmVal()
        {
            healthDbI = new HealthEduDbI(GVars.OracleAccess);
            patientDbI = new PatientDbI(GVars.OracleAccess);

            _userRight = GVars.User.GetUserFrmRight(_id);

            dictDbI = new HISDictDbI(GVars.OracleAccess);

            dsEdu_Pre = dictDbI.GetDictItem("12");                // 教育前准备情况
            dsEdut_Method = dictDbI.GetDictItem("11");                // 教育方法
            dsEdu_Master = dictDbI.GetDictItem("10");                // 教育掌握情况
            dsEdu_Clog = dictDbI.GetDictItem("13");                // 教育障碍
        }


        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {
            ucGridView1.AllowEdit = true;
            //ucGridView1.AddCheckBoxColumn("CHECKED",null,false);            
            ucGridView1.Add("", "CHECKED", typeof(bool));
            ucGridView1.Add("类别", "ITEM_TYPE", ColumnStatus.ReadOnly);
            ucGridView1.Add("名称", "ITEM_NAME", ColumnStatus.ReadOnly);
            ucGridView1.Add("项目ID", "ITEM_ID", false);

            DataTable dt = new DataTable();
            dt.Columns.Add("VALUE");
            dt.Rows.Add(new object[] { "病人" });
            dt.Rows.Add(new object[] { "家属" });
            ucGridView1.Add("教育对象", "EDU_OBJECT", dt, "VALUE", "VALUE", 15);

            ucGridView1.Add("准备情况", "PRECONDITION", dsEdu_Pre.Tables[0], "ITEM_NAME", "ITEM_NAME");
            ucGridView1.Add("教育方法", "EDU_METHOD", dsEdut_Method.Tables[0], "ITEM_NAME", "ITEM_NAME");
            ucGridView1.Add("掌握情况", "MASTERED_DEGREE", dsEdu_Master.Tables[0], "ITEM_NAME", "ITEM_NAME");
            ucGridView1.Add("学习障碍", "EDU_CLOG", dsEdu_Clog.Tables[0], "ITEM_NAME", "ITEM_NAME");
            ucGridView1.Add("教育时间", "EDU_DATE", ComConst.FMT_DATE.LONG_MINUTE);
            ucGridView1.Add("教育者", "EDU_NURSE", ColumnStatus.ReadOnly);
            ucGridView1.Add("备注", "MEMO");

            ucGridView1.ShowingEditor += ucGridView1_ShowingEditor;
            ucGridView1.CellValueChanged += ucGridView1_CellValueChanged;
            //ucGridView1.DataSourceChanged += ucGridView1_DataSourceChanged;

            ucGridView1.Init();
        }

        void ucGridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                GVars.App.UserInput = false;

                if (e.Column.FieldName == "CHECKED")
                {
                    DataRow dgvRow = ucGridView1.SelectedRow;

                    DateTime dtNow = GVars.OracleAccess.GetSysDate();

                    bool blnEdit = dgvRow["CHECKED"].ToString().Equals("True");

                    if (blnEdit)
                    {
                        dgvRow["EDU_DATE"] = dtNow.ToString(ComConst.FMT_DATE.LONG);
                        dgvRow["EDU_OBJECT"] = "病人"; // 教育对象
                        dgvRow["EDU_NURSE"] = GVars.User.Name; // 教育者
                    }
                    else
                    {
                        dgvRow["EDU_DATE"] = DBNull.Value; // 教育时间
                        dgvRow["EDU_OBJECT"] = DBNull.Value; // 教育对象
                        dgvRow["EDU_NURSE"] = DBNull.Value; // 教育者
                    }
                }

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }

        void ucGridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                string nurse = ucGridView1.SelectedRow["EDU_NURSE"].ToString().Trim();

                //if (ucGridView1.FocusedColumn.FieldName == "CHECKED")
                //{
                //    if (_userRight.IndexOf(RIGHT_EDIT) < 0
                //        && (nurse.Length > 0 && nurse.Equals(GVars.User.Name) == false)
                //        )
                //    {
                //        e.Cancel = true;
                //        ucGridView1.ShowToolTip("权限不足,无法编辑!");
                //    }
                //}

                bool blnEdu = false;

                if (ucGridView1.SelectedRow["CHECKED"] != null)
                {
                    blnEdu = ucGridView1.SelectedRow["CHECKED"].ToString().Equals("True");
                }

                bool blnEdit = blnEdu && (_userRight.IndexOf(RIGHT_EDIT) >= 0 || (nurse.Length > 0 && nurse.Equals(GVars.User.Name)));

                if (!blnEdit)
                {
                    e.Cancel = true;
                    ucGridView1.ShowToolTip("未选中该项或权限不足,无法编辑!");
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        void IBasePatient.Patient_SelectionChanged(object sender, PatientEventArgs e)
        {

           
            GVars.Patient.ID = e.PatientId;
            GVars.Patient.VisitId = e.VisitId;

            patientId = GVars.Patient.ID;
            visitId = GVars.Patient.VisitId;

            //dsRec = new DataSet();
            //dsRec.ReadXml(@"d:\dsRec.xml", XmlReadMode.ReadSchema);

            dsRec = healthDbI.GetHealthEduRec(GVars.User.DeptCode, patientId, visitId);
            foreach (DataColumn dc in dsRec.Tables[0].Columns)
            {
                dc.ReadOnly = false;
            }

            //dsRec.WriteXml(@"d:\dsRec.xml", XmlWriteMode.WriteSchema);

            showHealthRec();
        }

        /// <summary>
        /// 显示健康教育记录
        /// </summary>
        private void showHealthRec()
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;

                if (dsRec != null && dsRec.Tables.Count > 0)
                {
                    DataTable dt = dsRec.Tables[0];

                    if (!dt.Columns.Contains("CHECKED"))
                    {
                        dt.Columns.Add("CHECKED", typeof(bool));
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr["CHECKED"] = dr["EDU_NURSE"].ToString().Length > 0;
                        }
                    }

                    dsRec.Tables[0].DefaultView.Sort = "ITEM_ID";
                    ucGridView1.DataSource = dsRec.Tables[0].DefaultView;
                    btnPrint.Enabled = true;
                }
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }


        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="templateFileName"></param>
        private void loadExcelItem(string templateFileName)
        {
            arrExcelItem.Clear();

            // 读取配置文件
            string iniFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Template\\" + templateFileName + ".ini");
            if (System.IO.File.Exists(iniFile) == true)
            {
                StreamReader sr = new StreamReader(iniFile);

                string line = string.Empty;
                int row = 0;
                int col = 0;
                string itemId = string.Empty;
                string checkValue = string.Empty;

                while ((line = sr.ReadLine()) != null)
                {
                    // 获取配置
                    if (getParts(line, ref row, ref col, ref itemId, ref checkValue) == false)
                    {
                        continue;
                    }

                    ExcelItem excelItem = new ExcelItem();
                    excelItem.Row = row;
                    excelItem.Col = col;
                    excelItem.ItemId = itemId;
                    excelItem.CheckValue = checkValue;

                    arrExcelItem.Add(excelItem);
                }

                sr.Close();
                sr.Dispose();
            }
        }


        /// <summary>
        /// 获取节点属性
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="checkValue"></param>
        /// <returns></returns>
        private bool getItemAttr(string itemId, ref int row, ref int col, ref string checkValue)
        {
            ExcelItem excelItem;
            for (int i = 0; i < arrExcelItem.Count; i++)
            {
                excelItem = (ExcelItem)arrExcelItem[i];
                if (excelItem.ItemId.Equals(itemId) == true)
                {
                    row = excelItem.Row;
                    col = excelItem.Col;
                    checkValue = excelItem.CheckValue;
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// 获取配置文件的一行
        /// </summary>
        /// <param name="line"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>				
        private bool getVariableValue(string variable, ref string variableValue)
        {
            if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
            {
                return false;
            }

            // 年龄单独处理
            if (variable.ToUpper().Equals("AGE") == true)
            {
                if (dsPatient.Tables[0].Rows[0]["DATE_OF_BIRTH"].ToString().Length > 0)
                {
                    DateTime dt = (DateTime)dsPatient.Tables[0].Rows[0]["DATE_OF_BIRTH"];

                    variableValue = PersonCls.GetAgeYear(dt, GVars.OracleAccess.GetSysDate()).ToString();
                    return true;
                }

                return false;
            }

            // 其它病人基本信息
            if (dsPatient.Tables[0].Columns.Contains(variable) == true)
            {
                variableValue = dsPatient.Tables[0].Rows[0][variable].ToString();
                return true;
            }

            return false;
        }


        /// <summary>
        /// 获取配置文件的一行
        /// </summary>
        /// <param name="line"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>				
        private bool getParts(string line, ref int row, ref int col, ref string itemid, ref string checkValue)
        {
            line = line.Replace(ComConst.STR.BLANK, string.Empty);
            string[] arrParts = line.Split(ComConst.STR.COMMA.ToCharArray());

            if (arrParts.Length < 3) return false;

            itemid = arrParts[1];
            checkValue = arrParts[2];

            // 获取行列
            arrParts = arrParts[0].Split(":".ToCharArray());
            if (arrParts.Length <= 1)
            {
                return false;
            }

            // 行号
            if (int.TryParse(arrParts[0], out row) == false)
            {
                return false;
            }

            // 列号
            col = ExcelAccess.GetCol(arrParts[1]);

            return true;
        }


        /// <summary>
        /// 用Excel模板打印，比较适合套打、格式、统计分析报表、图形分析、自定义打印
        /// </summary>
        /// <remarks>用Excel打印，步骤为：打开、写数据、打印预览、关闭</remarks>
        private void ExcelTemplatePrint2()
        {
            string templateFileName = "健康教育评价表";
            string strExcelTemplateFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Template\\" + templateFileName + ".xls");

            // 加载excel节点
            loadExcelItem(templateFileName);

            excelAccess.Open(strExcelTemplateFile);				// 用模板文件
            excelAccess.IsVisibledExcel = true;
            excelAccess.FormCaption = string.Empty;

            DataRow[] drFind = dsRec.Tables[0].Select(string.Empty, "EDU_DATE, EDU_OBJECT");
            DataRow drRec = null;
            int idx_rec = 0;

            DateTime dtRecord = DateTime.MinValue;
            string itemId = string.Empty;
            string checkValue = string.Empty;

            // 输出固定记录
            ExcelItem excelItem;
            for (int i = 0; i < arrExcelItem.Count; i++)
            {
                excelItem = (ExcelItem)arrExcelItem[i];

                if (getVariableValue(excelItem.ItemId, ref checkValue) == true)
                {
                    excelAccess.SetCellText(excelItem.Row, excelItem.Col, checkValue);
                }
            }

            // 输出其它记录
            int lineIndex = 0;
            while (idx_rec < drFind.Length)
            {
                drRec = drFind[idx_rec];

                if (drRec["EDU_DATE"] == DBNull.Value || drRec["EDU_DATE"].ToString().Length == 0)
                {
                    idx_rec++;
                    continue;
                }

                for (int i = 0; i < arrExcelItem.Count; i++)
                {
                    excelItem = (ExcelItem)arrExcelItem[i];

                    if (dsRec.Tables[0].Columns.Contains(excelItem.ItemId) == false)
                    {
                        continue;
                    }

                    excelAccess.SetCellText(excelItem.Row + lineIndex, excelItem.Col, drRec[excelItem.ItemId].ToString());
                }

                lineIndex++;
                idx_rec++;
            }

            //excel.Print();				           // 打印
            excelAccess.PrintPreview();			       // 预览

            excelAccess.Close(false);				   // 关闭并释放			
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HealthEduRec_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                initFrmVal();
                initDisp();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 按钮[保存]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //出院病人不能插入新纪录
                //if (GVars.Patient.STATE == HISPlus.PAT_INHOS_STATE.OUT)
                //{
                //    btnSave.Enabled = false;
                //    MessageBox.Show("出院病人不能操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                healthDbI.SaveHealEduRec(dsRec.GetChanges(), GVars.User.DeptCode, patientId, visitId);
                dsRec.AcceptChanges();

                btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
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
                this.Cursor = Cursors.WaitCursor;

                //dsPatient = patientDbI.GetInpPatientInfo_FromID(patientId, visitId);
                dsPatient = patientDbI.GetInpPatientInfo_FromID(patientId, visitId);
                if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
                {
                    dsPatient = patientDbI.GetPatientInfo_FromID(patientId);
                }

                ExcelTemplatePrint2();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        public bool GridCellChecked(int rowHandle, GridColumn column)
        {
            string nurse = ucGridView1.GetRowCellValue(rowHandle, "EDU_NURSE").ToString().Trim();

            if (column.FieldName == "CHECKED")
            {
                if (_userRight.IndexOf(RIGHT_EDIT) < 0
                    && (nurse.Length > 0 && nurse.Equals(GVars.User.Name) == false)
                    )
                {
                    ucGridView1.ShowToolTip("权限不足,无法编辑!");
                    return false;
                }
            }
            return true;
        }


        void IBasePatient.Patient_ListRefresh(object sender, PatientEventArgs e)
        {
            
        }
    }
}
