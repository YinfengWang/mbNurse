﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonEntity;
using CommPrinter;
using DXApplication2;
using HISPlus.Controller;
using HISPlus.UserControls;
using SQL = HISPlus.SqlManager;
using System.Collections;
using System.IO;

namespace HISPlus
{
    public partial class NursingTour1 : FormDo, IBasePatient, INursingTourView
    {

        #region 变量

        public NursingTourControllor Controllor { get; private set; }

        public event EventHandler<PatientQueryArgs> PatientChanged;

        public void BindData(IEnumerable<Xunshi> dataSource)
        {
            ucGridView1.DataSource = dataSource;
        }

        private ExcelConfigData excelConfig = new ExcelConfigData();    // Excel 打印配置

        protected List<string> vitalCodeList = new List<string>();       // 项目列表
        protected List<int> vitalCodeLimitList = new List<int>();          // 项目限制列表

        private string patientId = string.Empty;             // 病人ID号
        private string visitId = string.Empty;             // 本次就诊序号
        protected string _template = "巡视单";
        protected string _typeId;
        protected int _minCount = 0;                        // 最少次数
        protected int dayStart = 0;                        // 一天从哪点开始

        private DataSet dsNursing = null;                     // 护理信息
        private DataSet dsPatient = null;                     // 病人信息

        private NursingDbI nursingCom;
        private PatientDbI patientDbI = null;

        private ExcelAccess excelAccess = new ExcelAccess();

        #endregion

        #region  初始化数据
        public NursingTour1()
        {
            InitializeComponent();
            _id = "00208";
            _guid = "A6A1400C-4546-482d-HSJY-ACABFF900208";

            this.Controllor = new NursingTourControllor(this);
        }
        #endregion

        #region  查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (PatientChanged != null)
                    PatientChanged(sender, new PatientQueryArgs(dtRngStart.Value, dtRngEnd.Value));

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
        #endregion

        #region   窗体加载
        private void NursingTour_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsInitializing)
                {
                    Controllor.Init();
                }

                initFrmVal();
                initDisp();

                //lvwNursingRec.CheckBoxes = false;
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
            nursingCom = new NursingDbI(GVars.OracleAccess);
            patientDbI = new PatientDbI(GVars.OracleAccess);

            _userRight = GVars.User.GetUserFrmRight(_id);
        }


        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {
            // 清空记录列表
            //lvwNursingRec.Items.Clear();

            // 选中第一个病人
            //if (patientListFrm.PatientId.Length > 0)
            //{
            //    Patient_SelectionChanged(null, new PatientEventArgs(patientListFrm.PatientId, patientListFrm.VisitId));
            //}

            patientId = GVars.Patient.ID;
            visitId = GVars.Patient.VisitId;

            ucGridView1.Add("日期", "Id.ExecuteDate", ComConst.FMT_DATE.SHORT, ColumnStatus.Unique);
            ucGridView1.Add("时间", "Id.ExecuteDate", ComConst.FMT_DATE.TIME_SHORT, ColumnStatus.Unique);
            ucGridView1.Add("记录人", "Nurse");
            ucGridView1.Add("备注", "Content", 200);

            ucGridView1.Init();

            btnQuery_Click(null, null);
        }


        public void Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
            if (PatientChanged != null)
                PatientChanged(sender, new PatientQueryArgs(dtRngStart.Value, dtRngEnd.Value));
        }        

        private void changePatientSearch()
        {
            dsPatient = patientDbI.GetInpPatientInfo_FromID(patientId, visitId);
            if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
            {
                dsPatient = patientDbI.GetPatientInfo_FromID(patientId);
            }

            dsNursing = GVars.OracleAccess.SelectData_NoKey("select * from xunshi where patient_Id=" + SQL.SqlConvert(patientId) + " and visit_id=" + SQL.SqlConvert(visitId) + " and EXECUTE_DATE >" + SQL.GetOraDbDate_Short(dtRngStart.Value) + " AND EXECUTE_DATE<" + SQL.GetOraDbDate_Short(dtRngEnd.Value.AddDays(1)));
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
                string filter = "VITAL_CODE = " + SQL.SqlConvert(vitalCode);
                DataRow[] drFind = dsNursing.Tables[0].Select(filter, "EXECUTE_DATE");

                int interHours = vitalCodeLimitList[i];

                DateTime dtStart = DataType.DateTime_Null();
                DateTime dtCurr = DataType.DateTime_Null();

                for (int rec = 0; rec < drFind.Length; rec++)
                {
                    DataRow dr = drFind[rec];

                    // 删除一个间隔中的第一条记录
                    dtCurr = (DateTime)dr["EXECUTE_DATE"];
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

            for (int i = 0; i < dsNursing.Tables[0].Rows.Count; i++)
            {
                int colIndx = 1;
                rowIndx++;
                DataRow dr = dsNursing.Tables[0].Rows[i];
                excelAccess.SetCellText(rowIndx, colIndx++, dr["EXECUTE_DATE"].ToString());
                excelAccess.SetCellText(rowIndx, colIndx++, dr["EXECUTE_DATE"].ToString());
                excelAccess.SetCellText(rowIndx, colIndx++, dr["NURSE"].ToString());
                excelAccess.SetCellText(rowIndx, colIndx++, dr["CONTENT"].ToString());


            }
            // 打印
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

        #region   打印
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
        #endregion

        #region 关闭
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region  添加 自定义 巡视数据
        private void bntSet_Click(object sender, EventArgs e)
        {

            FrmSetTour fst = new FrmSetTour();
            fst.ShowDialog();
        }
        #endregion
    }
}