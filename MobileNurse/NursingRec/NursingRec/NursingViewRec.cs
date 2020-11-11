using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraEditors.Repository;
using DXApplication2;
using HISPlus.UserControls;
using SqlConvert = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class NursingViewRec : FormDo, IBasePatient
    {
        #region 窗体变量
        protected string _template = "血压脉搏观察表";
        protected string _paraVitalCode = "PULSE_BLOOD_PRESSURE_CODE";
        protected int _minCount = 0;                        // 最少次数
        protected int dayStart = 0;                        // 一天从哪点开始
        protected List<string> vitalCodeList = new List<string>();       // 项目列表
        protected List<int> vitalCodeLimitList = new List<int>();          // 项目限制列表

        private NursingDbI nursingCom;
        private PatientDbI patientDbI = null;

        //private string              vitalCodeList       = string.Empty;             // 代码列表
        private DataSet dsNursing = null;                     // 护理信息

        private DataSet dsPatient = null;                     // 病人信息
        private string patientId = string.Empty;             // 病人ID号
        private string visitId = string.Empty;             // 本次就诊序号

        private DateTime dtTimePoint = DataType.DateTime_Null();
        private ExcelAccess excelAccess = new ExcelAccess();
        #endregion


        public NursingViewRec()
        {
            _id = "00039";
            _guid = "7F029E65-778D-4b6c-A932-B066D5FC5AC5";

            InitializeComponent();

            this.KeyPress += NursingRec_KeyPress;
        }


        #region 窗体事件
        private void NursingRec_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.DesignMode) return;

                initFrmVal();
                initDisp();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 窗体的KeyPress事件
        /// </summary>
        /// <remarks>
        /// 主要处理回车变成Tab
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void NursingRec_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // 如果是回车, 转换成Tab键
                if (e.KeyChar.Equals((char)Keys.Return))
                {
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);

                    e.Handled = true;
                }
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
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnQuery.Enabled = false;

                changePatientSearch();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                btnQuery.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }


        /// <summary>
        /// 护理记录CHECKED事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwNursingRec_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                if (e.Item.Checked == true)
                {                    
                    dsNursing.Tables[0].Rows[(int)(e.Item.Tag)].RejectChanges();
                }
                else
                {
                    dsNursing.Tables[0].Rows[(int)(e.Item.Tag)].SetModified();
                }
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
                ExcelTemplatePrint();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 确保为半角输入法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void imeCtrl_GotFocus(object sender, EventArgs e)
        {
            IME.ChangeControlIme(this.ActiveControl.Handle);
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
        /// 初始化窗体变量
        /// </summary>
        private void initFrmVal()
        {
            nursingCom = new NursingDbI(GVars.OracleAccess);
            patientDbI = new PatientDbI(GVars.OracleAccess);

            getParameters();

            _userRight = GVars.User.GetUserFrmRight(_id);
        }


        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {
            // 清空记录列表
            lvwNursingRec.Items.Clear();

            //ucGridView1.AddCheckBoxColumn("Checked");

            ucGridView1.MultiSelect = true;

            ucGridView1.AddCheckBoxColumn("CHECKED");
            //ucGridView1.Add("选择", "Checked", new RepositoryItemCheckEdit());

            //ucGridView1.Add("日期", "TIME_POINT", true, 0, ComConst.FMT_DATE.SHORT_CN);
            ucGridView1.Add("日期", "TIME_POINT", ComConst.FMT_DATE.SHORT, ColumnStatus.Unique);
            ucGridView1.Add("时间", "TIME_POINT", ComConst.FMT_DATE.TIME_SHORT, ColumnStatus.Unique);
            //ucGridView1.Add("日期", "TIME_POINT");
            ucGridView1.Add("名称", "VITAL_SIGNS");
            ucGridView1.Add("值", "VITAL_SIGNS_CVALUES");
            ucGridView1.Add("单位", "UNITS");
            ucGridView1.Add("记录人", "NURSE");
            ucGridView1.Add("备注", "MEMO");

            ucGridView1.Init();
        }


        void IBasePatient.Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
            patientId = e.PatientId;
            visitId = e.VisitId;

            btnQuery_Click(sender, e);
        }

        /// <summary>
        /// 显示病人的护理记录
        /// </summary>
        private void showNursingData()
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;

                lvwNursingRec.BeginUpdate();
                lvwNursingRec.Items.Clear();

                if (dsNursing == null || dsNursing.Tables.Count == 0 || dsNursing.Tables[0].Rows.Count == 0)
                {
                    return;
                }

                if (!dsNursing.Tables[0].Columns.Contains("CHECKED"))
                {
                    dsNursing.Tables[0].Columns.Add("CHECKED", typeof(bool));
                    //dsNursing.Tables[0].Columns["Checked"].Expression = "1";
                    dsNursing.Tables[0].Columns["CHECKED"].DefaultValue = true;

                    foreach (DataRow dr in dsNursing.Tables[0].Rows)
                    {
                        dr["CHECKED"] = true;
                    }
                }
                //if (!dsNursing.Tables[0].Columns.Contains("TIME_POINT1"))
                //{
                //    dsNursing.Tables[0].Columns.Add("TIME_POINT1");
                //    dsNursing.Tables[0].Columns["TIME_POINT1"].Expression = "TIME_POINT";
                //}

                //dsNursing.Tables[0].DefaultView.Sort = "TIME_POINT,TIME_POINT1";

                ucGridView1.DataSource = dsNursing.Tables[0].DefaultView;

                ListViewItem item = null;
                DateTime dtRec;

                string recDate0 = string.Empty;
                string recTime0 = string.Empty;
                string recDate = string.Empty;
                string recTime = string.Empty;

                int recIndex = 0;
                foreach (DataRow dr in dsNursing.Tables[0].Rows)
                {
                    dtRec = (DateTime)dr["TIME_POINT"];

                    recDate = dtRec.ToString(ComConst.FMT_DATE.SHORT);
                    recTime = dtRec.ToString(ComConst.FMT_DATE.TIME_SHORT);

                    if (recDate0.Equals(recDate) == true)
                    {
                        item = new ListViewItem();                                              // 日期

                        if (recTime0.Equals(recTime) == true)
                        {
                            item.SubItems.Add(string.Empty);
                        }
                        else
                        {
                            item.SubItems.Add(recTime);
                        }
                    }
                    else
                    {
                        item = new ListViewItem(recDate);                                       // 日期
                        item.SubItems.Add(recTime);                                             // 时间                        
                    }

                    recTime0 = recTime;
                    recDate0 = recDate;

                    // item.SubItems.Add(dr["CLASS_NAME"].ToString().Substring(0, 2));             // 类别
                    item.SubItems.Add(dr["VITAL_SIGNS"].ToString());                            // 名称
                    item.SubItems.Add(dr["VITAL_SIGNS_CVALUES"].ToString());                    // 值
                    item.SubItems.Add(dr["UNITS"].ToString());                                  // 单位
                    item.SubItems.Add(dr["NURSE"].ToString());                                  // 记录人
                    item.SubItems.Add(dr["MEMO"].ToString());                                   // 备注

                    lvwNursingRec.Items.Add(item);

                    item.Checked = true;
                    item.Tag = recIndex;
                    recIndex++;
                }
            }
            finally
            {
                if (lvwNursingRec.Items.Count > 1)
                {
                    lvwNursingRec.TopItem = lvwNursingRec.Items[lvwNursingRec.Items.Count - 1];
                }

                lvwNursingRec.EndUpdate();

                GVars.App.UserInput = blnStore;
            }
        }


        private void changePatientSearch()
        {
            // 获取护理记录
            //dsPatient = patientDbI.GetInpPatientInfo_FromID(patientId, visitId);
            dsPatient = patientDbI.GetInpPatientInfo_FromID(patientId, visitId);
            if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
            {
                dsPatient = patientDbI.GetPatientInfo_FromID(patientId);
            }

            // 过滤条件
            string filterItems = string.Empty;
            for (int i = 0; i < vitalCodeList.Count; i++)
            {
                if (filterItems.Length > 0) filterItems += "','";
                filterItems += vitalCodeList[i];
            }

            if (filterItems.Length > 0)
            {
                filterItems = "'" + filterItems + "'";
            }

            dsNursing = nursingCom.GetNursingItemsData(patientId, visitId, dtRngStart.Value, dtRngEnd.Value.AddDays(1), filterItems);

            // 处理数据
            if (_minCount > 1)
            {
                treateData();
            }

            // 显示护理记录
            showNursingData();
        }


        /// <summary>
        /// 获取参数
        /// </summary>
        private void getParameters()
        {
            // 获取参数
            if (GVars.App.AppParameters == null)
            {
                GVars.App.ReloadParameters();
            }

            // 一天从哪点开始
            string paraValue = GVars.App.GetParameters(GVars.User.DeptCode, string.Empty, "TEMPERATURE_START_TIME");
            int.TryParse(paraValue, out dayStart);

            // 项目列表
            paraValue = GVars.App.GetParameters(GVars.User.DeptCode, GVars.User.ID, _paraVitalCode);

            // 解析参数
            string[] parts = paraValue.Split(ComConst.STR.COMMA.ToCharArray());
            if (parts.Length > 1)
            {
                int.TryParse(parts[0].Trim(), out _minCount);
            }

            for (int i = 1; i <= parts.Length / 2; i++)
            {
                vitalCodeList.Add(parts[i].Trim());
            }

            int intValue = 0;
            for (int i = parts.Length / 2 + 1; i < parts.Length; i++)
            {
                if (int.TryParse(parts[i].Trim(), out intValue) == true)
                {
                    vitalCodeLimitList.Add(intValue);
                }
                else
                {
                    vitalCodeLimitList.Add(-1);
                }
            }
        }


        /// <summary>
        /// 数据处理
        /// </summary>
        private void treateData()
        {
            if (dsNursing == null || dsNursing.Tables.Count == 0)
            {
                return;
            }

            for (int i = 0; i < vitalCodeList.Count; i++)
            {
                // 如果没有限制条件, 不处理
                if (vitalCodeLimitList[i] <= 0) continue;

                // 获取数据
                string vitalCode = vitalCodeList[i];
                string filter = "VITAL_CODE = " + SqlConvert.SqlConvert(vitalCode);
                DataRow[] drFind = dsNursing.Tables[0].Select(filter, "TIME_POINT");

                int interHours = vitalCodeLimitList[i];

                DateTime dtStart = DataType.DateTime_Null();
                DateTime dtCurr = DataType.DateTime_Null();

                for (int rec = 0; rec < drFind.Length; rec++)
                {
                    DataRow dr = drFind[rec];

                    // 删除一个间隔中的第一条记录
                    dtCurr = (DateTime)dr["TIME_POINT"];
                    if (dtCurr.Subtract(dtStart).TotalHours > interHours)
                    {
                        dtStart = dtCurr.Date.AddHours((int)(dtCurr.Hour / interHours) * interHours + dayStart);

                        dr.Delete();
                        continue;
                    }
                }
            }

            dsNursing.AcceptChanges();
        }


        /// <summary>
        /// 用Excel模板打印，比较适合套打、格式、统计分析报表、图形分析、自定义打印
        /// </summary>
        /// <remarks>用Excel打印，步骤为：打开、写数据、打印预览、关闭</remarks>
        private void ExcelTemplatePrint()
        {
            string strExcelTemplateFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Template\\" + _template + ".xls");

            excelAccess.Open(strExcelTemplateFile);				//用模板文件
            excelAccess.IsVisibledExcel = true;
            excelAccess.FormCaption = string.Empty;

            // 读取配置文件
            string iniFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Template\\" + _template + ".ini");
            int startRow = 0;
            int startCol = 0;
            int maxCol = 0;
            int rows = 20;                   // 每页的行数

            ArrayList arrVitalCode = new ArrayList();
            if (System.IO.File.Exists(iniFile) == true)
            {
                StreamReader sr = new StreamReader(iniFile);

                int row = 0;
                int col = 0;
                string itemId = string.Empty;
                string line = string.Empty;
                string varValue = string.Empty;

                while ((line = sr.ReadLine()) != null)
                {
                    // 获取配置
                    if (getParts(line, ref row, ref col, ref itemId) == false)
                    {
                        int.TryParse(line, out rows);

                        continue;
                    }

                    if (getVariableValue(itemId, ref varValue) == true)
                    {
                        excelAccess.SetCellText(row, col, varValue);
                        continue;
                    }
                    else
                    {
                        if (row > 0 && startRow == 0)
                        {
                            startRow = row;
                        }

                        if (col > 0 && startCol == 0)
                        {
                            startCol = col;
                        }

                        if (maxCol < col) maxCol = col;

                        arrVitalCode.Add(itemId);
                    }
                }

                sr.Close();
            }

            if (dsNursing == null || dsNursing.Tables.Count == 0)
            {
                return;
            }

            if (startCol == 0 || startRow == 0 || arrVitalCode.Count == 0)
            {
                return;
            }

            DateTime dtPre = DataType.DateTime_Null();
            int rowIndx = startRow - 1;
            int colIndx = 1;
            bool containCheckedColumn = false;
            containCheckedColumn = dsNursing.Tables[0].Columns.Contains("CHECKED");
            for (int i = 0; i < dsNursing.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dsNursing.Tables[0].Rows[i];
                DateTime dtCurrent = (DateTime)(dr["TIME_POINT"]);

                if (containCheckedColumn)
                {
                    // 如果取消选中,就跳过
                    if (Convert.ToBoolean(dr["CHECKED"]) == false)
                    {
                        continue;
                    }
                }

                //if (dr.RowState == DataRowState.Modified)
                //{
                //    continue;
                //}

                colIndx = getCol(ref arrVitalCode, dr["VITAL_CODE"].ToString(), startCol);
                if (colIndx == 0)
                {
                    continue;
                }

                if (dtPre.CompareTo(dtCurrent) != 0)
                {
                    dtPre = dtCurrent;
                    rowIndx++;

                    excelAccess.SetCellText(rowIndx, startCol - 2, dr["TIME_POINT"].ToString());
                    excelAccess.SetCellText(rowIndx, startCol - 1, dr["TIME_POINT"].ToString());
                }

                excelAccess.SetCellText(rowIndx, colIndx, dr["VITAL_SIGNS_CVALUES"].ToString());

                if (_minCount < 2 && maxCol > 0)
                {
                    excelAccess.SetCellText(rowIndx, maxCol + 1, dr["MEMO"].ToString());
                    excelAccess.SetCellText(rowIndx, maxCol + 2, dr["NURSE"].ToString());
                }
                else
                {
                    excelAccess.SetCellText(rowIndx, maxCol + 1, dr["NURSE"].ToString());
                }
            }

            //excel.Print();				           // 打印
            excelAccess.PrintPreview();			       // 预览

            excelAccess.Close(false);				   // 关闭并释放			
        }


        private int getCol(ref ArrayList arrVitalCode, string vitalCode, int startCol)
        {
            for (int i = 0; i < arrVitalCode.Count; i++)
            {
                if (vitalCode.Equals((string)(arrVitalCode[i])) == true)
                {
                    return startCol + i;
                }
            }

            return 0;
        }


        /// <summary>
        /// 获取配置文件的一行
        /// </summary>
        /// <param name="line"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>				
        private bool getParts(string line, ref int row, ref int col, ref string itemid)
        {
            line = line.Replace(ComConst.STR.BLANK, string.Empty);
            string[] arrParts = line.Split(ComConst.STR.COMMA.ToCharArray());

            if (arrParts.Length < 2) return false;

            itemid = arrParts[1];

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
            if (dsPatient == null || dsPatient.Tables.Count == 0)
            {
                return false;
            }

            if (dsPatient.Tables[0].Columns.Contains(variable) == true)
            {
                variableValue = dsPatient.Tables[0].Rows[0][variable].ToString();
                return true;
            }

            return false;
        }
        #endregion


        void IBasePatient.Patient_ListRefresh(object sender, PatientEventArgs e)
        {
            
        }
    }
}