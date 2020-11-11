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
using HISPlus.UserControls;
using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class PIORecFrm : FormDo, IBasePatient
    {
        #region 变量
        private const string RIGHT_VIEW = "0";                      //  查看权限
        private const string RIGHT_EDIT = "1";                      //  录入权限
        private const string RIGHT_MODIFY = "2";                      //  编辑别人录入记录的权限

        protected string _template = "护理计划表";             // 模板文件

        private PatientDbI patientDbI = null;                     // 病人信息接口
        private DataSet dsPatient = null;                     // 病人信息
        private string patientId = string.Empty;             // 病人ID号
        private string visitId = string.Empty;             // 本次就诊序号

        private ExcelAccess excelAccess = new ExcelAccess();
        private List<ExcelItem> arrExcelItem = new List<ExcelItem>();
        private int printCols = 7;

        private DbInfo dbDbI = null;
        private DataSet dsScheduleRec = null;

        private TreeNodeSelectorFrm diagnosisSelector = null;           // 护理诊断选择
        private PIOSettingFrm pioSelection = null;
        private DictItemSelectFrm itemSelection = null;                 // 项目选择

        private string preValue = string.Empty;             // 

        private bool _isNewLine = false;   //是否新增的行
        private bool _isDel = false;       //是否删除行

        #endregion


        struct ExcelItem
        {
            public int Row;
            public int Col;
            public string ItemId;
            public string CheckValue;
        }


        public PIORecFrm()
        {
            InitializeComponent();

            _id = "00058";
            _guid = "171F3931-6910-4da8-8625-D4123765F076";
        }


        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EvaluationEverydayFrm_Load(object sender, EventArgs e)
        {
            try
            {
                if (DesignMode) return;

                btnSave.Enabled = false;
                btnPrint.Enabled = false;

                dbDbI = new DbInfo(GVars.OracleAccess);
                patientDbI = new PatientDbI(GVars.OracleAccess);

                _userRight = GVars.User.GetUserFrmRight(_id);

                pioSelection = new PIOSettingFrm();
                pioSelection.SelectionMode = true;
                pioSelection.Text = @"护理计划选择";

                diagnosisSelector = new TreeNodeSelectorFrm();
                diagnosisSelector.Text = @"护理诊断选择";

                itemSelection = new DictItemSelectFrm();

                InitControl();

                Patient_SelectionChanged(null, new PatientEventArgs(GVars.Patient.ID, GVars.Patient.VisitId));
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            ucGridView1.ShowRowIndicator = true;
            ucGridView1.AllowEdit = true;
            ucGridView1.ColumnAutoWidth = false;
            ucGridView1.EnableOddEvenRow = true;
            ucGridView1.Add("病人ID", "PATIENT_ID", false);
            ucGridView1.Add("住院序号", "VISIT_ID", false);
            ucGridView1.Add("记录编号", "RECORD_ID", false);
            ucGridView1.Add("护理诊断ID", "DIAGNOSIS_ID", false);

            ucGridView1.Add("日期时间", "CREATE_DATE", ComConst.FMT_DATE.LONG_MINUTE, 120);
            ucGridView1.Add("序号", "SERIAL_NO", ColumnStatus.WordWrap);
            ucGridView1.Add("护理问题", "DIAGNOSIS", 100, ColumnStatus.WordWrap);
            //ucGridView1.Add("预期目标", "OBJECTIVE",180, ColumnStatus.WordWrap);
            //ucGridView1.Add("观察评估", "ASSESSMENT", 180, ColumnStatus.WordWrap);
            ucGridView1.Add("护理措施", "ASSESSMENT", 180, ColumnStatus.WordWrap);
            //ucGridView1.Add("教育", "EDUCATION", 180, ColumnStatus.WordWrap);
            //ucGridView1.Add("备注", "MEMO", 180, ColumnStatus.WordWrap);
            ucGridView1.Add("开始时间", "BEGIN_DATE", ComConst.FMT_DATE.LONG_MINUTE, 120);
            ucGridView1.Add("护士签名", "BEGIN_OPERATOR", 70);
            ucGridView1.Add("效果评价", "MEMO", 100);
            ucGridView1.Add("停止时间", "END_DATE", ComConst.FMT_DATE.LONG_MINUTE, 120);
            ucGridView1.Add("护士签名", "END_OPERATOR", 70);

            ucGridView1.Add("创建时间", "CREATE_DATE", false);
            ucGridView1.Add("创建人", "CREATOR", false);
            ucGridView1.Add("序号", "SERIAL_NO", false);
            ucGridView1.DoubleClick += ucGridView1_DoubleClick;
            ucGridView1.CellValueChanged += ucGridView1_CellValueChanged;
            ucGridView1.SelectionChanged += ucGridView1_SelectionChanged;
            ucGridView1.ShowingEditor += ucGridView1_ShowingEditor;

            ucGridView1.Init();
        }

        void ucGridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            string value = ucGridView1.SelectedRow["END_DATE"] as string;
            if (string.IsNullOrEmpty(value)) return;

            if (string.IsNullOrEmpty(_userRight))
                _userRight = GVars.User.GetUserFrmRight(_id);

            if (string.IsNullOrEmpty(_userRight) || _userRight.IndexOf(RIGHT_MODIFY) < 0)
            {
                e.Cancel = true;
                ucGridView1.ShowToolTip("没有权限,无法编辑!");
            }
        }

        void ucGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                btnDelete.Enabled = true;
                btnStop.Enabled = true;

                if (btnStop.Enabled)
                {
                    DataRow dgvRow = ucGridView1.SelectedRow;

                    if (dgvRow["END_DATE"] == null || dgvRow["END_DATE"].ToString().Length == 0)
                    {
                        btnStop.TextValue = "停止";
                    }
                    else
                    {
                        btnStop.TextValue = "取消停止";
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        void ucGridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            btnSave.Enabled = true;
        }

        void ucGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string diagnoseId = ucGridView1.SelectedRow["DIAGNOSIS_ID"].ToString();
                itemSelection.MultiSelect = true;
                itemSelection.ParentNodeId = diagnoseId;
                itemSelection.ResultContainSerialNo = true;

                switch (ucGridView1.FocusedColumn.FieldName)
                {
                    case "OBJECTIVE":           // 预期目标
                        itemSelection.DictId = "21";
                        break;
                    case "ASSESSMENT":          // 观察评估
                        itemSelection.DictId = "22";
                        break;
                    case "TREATE":              // 治疗
                        itemSelection.DictId = "23";
                        break;
                    case "EDUCATION":           // 教育
                        itemSelection.DictId = "24";
                        break;
                    case "MEMO":
                        itemSelection.DictId = "19";  //评价
                        break;
                    default:
                        return;
                }

                if (itemSelection.ShowDialog() == DialogResult.OK)
                {
                    ucGridView1.SetRowCellValue(ucGridView1.FocusedRowHandle, ucGridView1.FocusedColumn,
                        itemSelection.ItemName);

                    btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void dgvNursingSchedule_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                //btnDelete.Enabled = dgvNursingSchedule.SelectedRows.Count > 0;
                //btnStop.Enabled   = dgvNursingSchedule.SelectedRows.Count > 0;

                //if (btnStop.Enabled == true)
                //{
                //    DataGridViewRow dgvRow = dgvNursingSchedule.SelectedRows[0];

                //    if (dgvRow.Cells["END_DATE"].Value == null || dgvRow.Cells["END_DATE"].Value.ToString().Length == 0)
                //    {
                //        btnStop.Text1 = "停止";
                //    }
                //    else
                //    {
                //        btnStop.Text1 = "取消停止";
                //    }                    
                //}
            }
            catch (Exception ex)
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
            bool blnStore = GVars.App.UserInput;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                getScheduleRec(patientId, visitId);

                btnNew.Enabled = patientId.Length > 0;
                btnSave.Enabled = false;
                btnPrint.Enabled = patientId.Length > 0;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                GVars.App.UserInput = blnStore;
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

                if (dsScheduleRec != null && dsScheduleRec.Tables[0].Rows.Count > 0 && !_isDel)
                {
                    if (dsPatient != null && dsPatient.Tables[0].Rows.Count > 0)
                    {
                        string admission_date_time = dsPatient.Tables[0].
                        Select(" PATIENT_ID ='" + patientId + "' AND VISIT_ID='" + visitId + "'")[0]["ADMISSION_DATE_TIME"].ToString();
                        foreach (DataRow rows in dsScheduleRec.Tables[0].Rows)
                        {
                            if (Convert.ToDateTime(rows["CREATE_DATE"].ToString()) < Convert.ToDateTime(admission_date_time))
                            {
                                MessageBox.Show("存在小于入院日期的记录：【" + Convert.ToDateTime(rows["CREATE_DATE"]).ToString("yyyy-MM-dd HH:mm") + "】，请修改后再保存！",
                                    "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                    
                }

                this.Cursor = Cursors.WaitCursor;

                if (dbDbI.SaveData(null, new object[] { dsScheduleRec }) == true)
                {
                    dsScheduleRec.AcceptChanges();

                    btnSave.Enabled = false;
                }

                btnSave.Enabled = false;
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
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (dsScheduleRec == null || dsScheduleRec.Tables[0].Rows.Count == 0)
                {
                    return;
                }

                //dsPatient = patientDbI.GetInpPatientInfo_FromID(GVars.Patient.ID, GVars.Patient.VisitId);
                dsPatient = patientDbI.GetInpPatientInfo_FromID(patientId, visitId);

                if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
                {
                    dsPatient = patientDbI.GetPatientInfo_FromID(patientId);
                }

                ExcelTemplatePrint();
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
        /// 按钮[新增]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {

                _isNewLine = true;
                if (diagnosisSelector.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                // 插入到当前行后面 序号变更
                string serialNo = "1";
                int serialNum = 1;
                DataTable dddd = (ucGridView1.DataSource as DataTable);
                if (ucGridView1.SelectRowsCount > 0)
                {
                    serialNo = ucGridView1.RowCount.ToString();//ucGridView1.SelectedRow["SERIAL_NO"].ToString();
                    string filter = "SERIAL_NO > " + SQL.SqlConvert(serialNo);
                    DataRow[] drFind = dsScheduleRec.Tables[0].Select(filter);

                    for (int i = 0; i < drFind.Length; i++)
                    {
                        drFind[i]["SERIAL_NO"] = int.Parse(drFind[i]["SERIAL_NO"].ToString()) + diagnosisSelector.ItemIdArr.Count;
                    }

                    serialNum = int.Parse(serialNo) + 1;
                }
                else
                {
                    DataRow[] drFind = dsScheduleRec.Tables[0].Select(string.Empty, "SERIAL_NO DESC");
                    if (drFind.Length > 0)
                    {
                        serialNo = drFind[0]["SERIAL_NO"].ToString();
                        serialNum = int.Parse(serialNo) + 1;
                    }
                }

                DateTime dtNow = dbDbI.GetSysDate();

                for (int i = 0; i < diagnosisSelector.ItemIdArr.Count; i++)
                {
                    DataRow drNew = dsScheduleRec.Tables[0].NewRow();

                    drNew["PATIENT_ID"] = patientId;
                    drNew["VISIT_ID"] = visitId;
                    drNew["RECORD_ID"] = (getMaxRecordId() + 1).ToString();
                    drNew["DIAGNOSIS"] = diagnosisSelector.ItemNameArr[i].ToString();
                    drNew["DIAGNOSIS_ID"] = diagnosisSelector.ItemIdArr[i].ToString();
                    //drNew["OBJECTIVE"]      = pioSelection.NursingObjective;
                    //drNew["MEASURE_TYPE"]   = pioSelection.NursingMeasureType[i].ToString();
                    //drNew["MEASURE_TITLE"]  = pioSelection.NursingMeasure[i].ToString();
                    //drNew["MEASURE_CONTENT"]= pioSelection.NursingMeasureContent[i].ToString();
                    drNew["SERIAL_NO"] = serialNum.ToString();
                    drNew["CREATE_DATE"] = dtNow;
                    drNew["CREATOR"] = GVars.User.Name;
                    drNew["BEGIN_DATE"] = dtNow;
                    drNew["BEGIN_OPERATOR"] = GVars.User.Name;

                    serialNum++;
                    dsScheduleRec.Tables[0].Rows.Add(drNew);
                }

                // 按钮状态
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[删除]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // 确认删除
                if (GVars.Msg.Show("Q0006", "删除", "当前记录") != DialogResult.Yes)        // 您确认要 {0} {1} 吗?
                {
                    return;
                }

                ucGridView1.DeleteSelectRow();

                btnSave.Enabled = true;

                _isNewLine = false;
                _isDel = true;

                btnSave_Click(null, null);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[停止]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                //DataGridViewRow dgvRow = dgvNursingSchedule.SelectedRows[0];
                DataRow dgvRow = ucGridView1.SelectedRow;
                if (dgvRow["END_DATE"] == null || dgvRow["END_DATE"].ToString().Length == 0)
                {
                    dgvRow["END_DATE"] = dbDbI.GetSysDate();
                    dgvRow["END_OPERATOR"] = GVars.User.Name;
                }
                else
                {
                    dgvRow["END_DATE"] = DBNull.Value;
                    dgvRow["END_OPERATOR"] = string.Empty;
                }

                if (dgvRow["END_DATE"] == null || dgvRow["END_DATE"].ToString().Length == 0)
                {
                    btnStop.TextValue = "停止";
                }
                else
                {
                    btnStop.TextValue = "取消停止";
                }

                btnSave.Enabled = true;
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
            this.Close();
        }
        #endregion


        #region 共通函数
        /// <summary>
        /// 用Excel模板打印，比较适合套打、格式、统计分析报表、图形分析、自定义打印
        /// </summary>
        /// <remarks>用Excel打印，步骤为：打开、写数据、打印预览、关闭</remarks>
        private void ExcelTemplatePrint()
        {
            // 加载excel节点
            loadExcelItem(_template);

            string strExcelTemplateFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Template\\" + _template + ".xls");

            excelAccess.Open(strExcelTemplateFile);				//用模板文件
            excelAccess.IsVisibledExcel = true;
            excelAccess.FormCaption = string.Empty;

            int row = 0;
            int col = 0;
            string itemId = string.Empty;
            string checkValue = string.Empty;

            DataRow[] drFind = null; //dsEvalRec.Tables[0].Select(string.Empty, "ITEM_ID");
            DataRow drRec = null;

            // 输出固定记录
            ExcelItem excelItem;
            for (int i = 0; i < arrExcelItem.Count; i++)
            {
                excelItem = arrExcelItem[i];

                if (getVariableValue(excelItem.ItemId, ref checkValue) == true)
                {
                    excelAccess.SetCellText(excelItem.Row, excelItem.Col, checkValue);
                    continue;
                }
            }

            // 输出其它记录
            int idx_rec = 0;

            foreach (DataRow dr in dsScheduleRec.Tables[0].Rows)
            {
                foreach (DataColumn dc in dsScheduleRec.Tables[0].Columns)
                {
                    if (getItemAttr(dc.ColumnName, ref row, ref col, ref checkValue) == true)
                    {
                        if (dr[dc.ColumnName] != null)
                        {
                            string itemValue = dr[dc.ColumnName].ToString();
                            if (itemValue.Length == 0) itemValue = " ";
                            excelAccess.SetCellText(row + idx_rec, col, itemValue);
                        }
                    }
                }

                // 签名
                if (getItemAttr("NURSE_SIGN", ref row, ref col, ref checkValue) == true)
                {
                    string nurseSign = string.Empty;
                    if (dr["BEGIN_DATE"] != null)
                    {
                        nurseSign = dr["BEGIN_DATE"].ToString();
                        nurseSign += " " + dr["BEGIN_OPERATOR"].ToString();
                    }

                    if (nurseSign.Length > 0 && dr["BEGIN_DATE"] != null)
                    {
                        nurseSign += ComConst.STR.CRLF;
                        nurseSign += dr["END_DATE"].ToString();
                        nurseSign += " " + dr["END_OPERATOR"].ToString();
                    }

                    excelAccess.SetCellText(row + idx_rec, col, nurseSign);
                }

                idx_rec++;
            }

            //excel.Print();				           //打印
            excelAccess.PrintPreview();			       //预览
            excelAccess.Close(false);				   //关闭并释放			
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
            switch (variable)
            {
                case "AGE":
                    if (dsPatient.Tables[0].Rows[0]["DATE_OF_BIRTH"].ToString().Length > 0)
                    {
                        DateTime dt = (DateTime)dsPatient.Tables[0].Rows[0]["DATE_OF_BIRTH"];

                        variableValue = PersonCls.GetAgeYear(dt, GVars.OracleAccess.GetSysDate()).ToString();
                        return true;
                    }

                    return false;
                default:
                    break;
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
        /// 获取护理计划
        /// </summary>
        /// <returns></returns>
        private void getScheduleRec(string patientId, string visitId)
        {
            string filter = "PATIENT_ID = " + SQL.SqlConvert(patientId)
                            + "AND VISIT_ID = " + SQL.SqlConvert(visitId);
            filter += " ORDER BY SERIAL_NO";
            dsScheduleRec = dbDbI.GetTableData("NURSING_PLAN_REC", filter);

            dsScheduleRec.Tables[0].DefaultView.Sort = "SERIAL_NO";

            ucGridView1.DataSource = dsScheduleRec.Tables[0].DefaultView;
        }


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


        private bool getItemAttr(string itemId, ref int row, ref int col, ref string checkValue)
        {
            bool blnFind = false;
            ExcelItem excelItem;
            for (int i = 0; i < arrExcelItem.Count; i++)
            {
                excelItem = arrExcelItem[i];
                if (excelItem.ItemId.Equals(itemId) == true)
                {
                    row = excelItem.Row;
                    col = excelItem.Col;
                    checkValue = excelItem.CheckValue;

                    blnFind = true;
                    //arrExcelItem.Remove(excelItem);
                    //return true;
                }
            }
            return blnFind;
        }


        /// <summary>
        /// 获取最大序列号
        /// </summary>
        /// <returns></returns>
        private int getMaxRecordId()
        {
            int maxRecordId = 0;
            int recordId = 0;

            foreach (DataRow dr in dsScheduleRec.Tables[0].Rows)
            {
                if (int.TryParse(dr["RECORD_ID"].ToString(), out recordId) == true)
                {
                    if (maxRecordId < recordId)
                    {
                        maxRecordId = recordId;
                    }
                }
            }

            return maxRecordId;
        }
        #endregion

        public void Patient_SelectionChanged(object sender, PatientEventArgs e)
        {


            GVars.Patient.ID = e.PatientId;
            GVars.Patient.VisitId = e.VisitId;

            patientId = GVars.Patient.ID;
            visitId = GVars.Patient.VisitId;
            dsPatient = patientDbI.GetInpPatientInfo_FromID(GVars.Patient.ID, GVars.Patient.VisitId);
            btnQuery_Click(null, null);

            if (GVars.Patient.STATE == HISPlus.PAT_INHOS_STATE.OUT)
            {
                btnNew.Enabled = false;
                btnSave.Enabled = false;
            }
        }

        void IBasePatient.Patient_ListRefresh(object sender, PatientEventArgs e)
        {

        }
    }
}
