using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SqlConvert = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class frmNurseRecord : Form
    {
        #region 窗体变量
        private PatientDbI patientCom;                                  // 病人共通
        private DataSet dsPatient = null;                               // 病人信息
        private string patientId = string.Empty;                        // 病人标识号
        private string visitId = string.Empty;                          // 住院标识
        private DataSet dsVitalSignRec = null;                          // 体征记录
        private DataSet dsNursingRecord = null;                         // 护理记录
        private DataTable dtNursingData = null;                         // 对应于界面的记录表(最初的获取)
        private DataTable dtNursingDataNew = null;                      // 对应于界面的记录表(操作后的获取)
        private int ROW_HEIGHT = 0;                                     // 行的初始高度
        private const int OFF_SET = 30;                                 // 偏移量
        private int selectRow = -1;                                     // 点中的行号
        private HISPlus.ExcelAccess excelAccess = null;                 // Excel打印访问接口
        private Graphics grfx = null;
        private bool scrollValue = true;                                // 垂直滚动条滚动的值
        #endregion

        public frmNurseRecord()
        {
            InitializeComponent();

            this.Load += new EventHandler(frmNurseRecord_Load);
            this.txtBedLabel.KeyDown += new KeyEventHandler(txtBedLabel_KeyDown);
            this.txtBedLabel.GotFocus += new EventHandler(imeCtrl_GotFocus);

            this.grdRecord.EnterCell += new EventHandler(grdRecord_EnterCell);
            this.grdRecord.LeaveCell += new EventHandler(txtData_LostFocus);
            //this.txtData.GotFocus += new EventHandler(txtData_GotFocus);
            this.txtData.LostFocus += new EventHandler(txtData_LostFocus);
            this.txtData.KeyDown += new KeyEventHandler(txtData_KeyDown);
            this.grdRecord.Scroll += new EventHandler(grdRecord_Scroll);
        }


        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmNurseRecord_Load(object sender, EventArgs e)
        {
            try
            {
                grfx = this.CreateGraphics();
                ROW_HEIGHT = (int)(grfx.MeasureString("医", this.grdRecord.Font).Height) * 15;         // 表格行的初始高度
                
                patientCom = new PatientDbI(GVars.OracleAccess);
                
                initDisp();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 床标号回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtBedLabel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // 条件检查
                if (e.KeyCode != Keys.Return)
                {
                    return;
                }

                // 获取查询条件
                if (txtBedLabel.Text.Trim().Length == 0)
                {
                    return;
                }

                // 获取病人信息
                dsPatient = patientCom.GetInpPatientInfo_FromBedLabel(txtBedLabel.Text.Trim(), GVars.User.DeptCode);
                if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
                {
                    GVars.Msg.Show("W00005");                           // 该病人不存在!	
                    return;
                }
                
                // 显示病人信息
                showPatientInfo();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        /// <summary>
        /// 可编辑框textbox回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtData_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // 条件检查
                if (e.KeyCode != Keys.Return)
                {
                    return;
                }

                // 获取查询条件
                if (txtData.Text.Trim().Length == 0)
                {
                    return;
                }

                System.Drawing.Font strFont = new Font("Arial", 11);
                int rowNum = getRowNum(txtData.Text.Trim(), strFont, txtData.Width);
                float heightUnit = grfx.MeasureString("医", strFont).Height;
                float widthUnit = grfx.MeasureString("医", strFont).Width;

                if ((txtData.Height - (rowNum * heightUnit)) < heightUnit)
                {
                    txtData.Height += ROW_HEIGHT / 15;
                    grdRecord.set_RowHeight(selectRow, txtData.Height * 15);
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 可编辑框textbox获焦事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtData_GotFocus(object sender, EventArgs e)
        {
            txtData.Visible = true;
        }


        /// <summary>
        /// 可编辑框textbox失焦事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtData_LostFocus(object sender, EventArgs e)
        {
            if (txtData.Visible == true)
            {
                int rowNum = getRowNum(txtData.Text.Trim(), this.grdRecord.Font, grdRecord.get_ColWidth(13) / 15);

                grdRecord.set_RowHeight(selectRow, rowNum * ROW_HEIGHT);
                grdRecord.set_TextMatrix(selectRow, 13, txtData.Text);
                txtData.Visible = false;
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

                dsVitalSignRec = getPatientVitalSignRec(patientId, visitId);
                dsNursingRecord = getPatientNursingRecord(patientId, visitId);
                mergeNursingData(ref dtNursingData);
                showPatientVitalSignRec();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        /// <summary>
        /// 按钮[打印](打印时必须选择查询一天的记录)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // 获取表格控件上的数据
                getGrdNursingData(ref dtNursingDataNew);

                // 比较是否有数据变化,若有并且未保存,给予提示
                for (int i = 0; i < dtNursingData.Rows.Count; i++)
                {
                    string nursingData = dtNursingData.Rows[i]["NURSING"].ToString().Trim();
                    string nursingDataNew = dtNursingDataNew.Rows[i]["NURSING"].ToString().Trim();

                    if (nursingData.Equals(nursingDataNew) == false)
                    {
                        GVars.Msg.Show("ID010");         // 数据还未保存,请先保存后再打印!
                        return;
                    }
                }

                if (dtRngStart.Value.ToString(ComConst.FMT_DATE.SHORT) != dtRngEnd.Value.ToString(ComConst.FMT_DATE.SHORT))
                {
                    GVars.Msg.Show("ED007");                   // 只能打印一天的记录!
                    return;
                }

                // 获取需要打印的记录
                //string filter = string.Empty;
                string filter = "RECORDING_DATE >= " + SqlConvert.SqlConvert(dtRngStart.Value.ToString(ComConst.FMT_DATE.SHORT));
                filter += "AND RECORDING_DATE <= " + SqlConvert.SqlConvert(dtRngEnd.Value.ToString(ComConst.FMT_DATE.SHORT));            
                DataRow[] drData = dtNursingData.Select(filter);

                int row = 10, page = 1;

                // 打印
                if (excelAccess == null)
                {
                    excelAccess = new HISPlus.ExcelAccess();
                }

                setPrintInfo(filter);

                excelTemplatePrint(filter, drData, row, page, true);
            }
            catch (Exception ex)
            {
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
                this.Cursor = Cursors.WaitCursor;

                for (int i = 0; i < grdRecord.Rows; i++)
                {
                    bool isExsit = false;

                    foreach (DataRow dr in dsNursingRecord.Tables[0].Rows)
                    {
                        string recordDate = DataType.GetMD(dr["RECORDING_DATE"].ToString());
                        string timePoint = DataType.GetShortTime(dr["TIME_POINT"].ToString());

                        if (grdRecord.get_TextMatrix(i, 0).Trim().CompareTo(recordDate.Trim()) == 0
                            && grdRecord.get_TextMatrix(i, 1).Trim().CompareTo(timePoint.Trim()) == 0)
                        {
                            isExsit = true;

                            // 修改记录
                            if (grdRecord.get_TextMatrix(i, 13).Trim().CompareTo(dr["REPRSENTATION"].ToString().Trim()) != 0)
                            {
                                dr["REPRSENTATION"] = grdRecord.get_TextMatrix(i, 13).Trim();
                            }

                            break;
                        }
                    }

                    // 新增记录
                    if(isExsit == false)
                    {
                        DataRow drNew = dsNursingRecord.Tables[0].NewRow();
                        drNew["RECORDING_DATE"] = dtNursingData.Rows[i]["RECORDING_DATE"].ToString();
                        drNew["TIME_POINT"] = dtNursingData.Rows[i]["TIME_POINT"].ToString();
                        drNew["PATIENT_ID"] = patientId;
                        drNew["VISIT_ID"] = visitId;
                        drNew["REPRSENTATION"] = grdRecord.get_TextMatrix(i, 13).Trim();
                        drNew["START_ROW"] = dtNursingData.Rows[i]["START_ROW"].ToString();
                        drNew["START_PAGE"] = dtNursingData.Rows[i]["START_PAGE"].ToString();
                        drNew["END_ROW"] = dtNursingData.Rows[i]["END_ROW"].ToString();
                        drNew["END_PAGE"] = dtNursingData.Rows[i]["END_PAGE"].ToString();
                        dsNursingRecord.Tables[0].Rows.Add(drNew);
                    }
                }

                updateNursingRecord();
                mergeNursingData(ref dtNursingData);          // 将对应于界面的护理记录表更新
                GVars.Msg.Show("ID009");
                btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                GVars.Msg.Show("ED006");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        /// <summary>
        /// 按钮[关闭]
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


        /// <summary>
        /// 按钮[续打](打印时必须选择查询一天的记录)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // 获取表格控件上的数据
                getGrdNursingData(ref dtNursingDataNew); 

                // 比较是否有数据变化,若有,给予提示(不能续打)
                for (int i = 0; i < dtNursingData.Rows.Count; i++)
                {
                    string nursingData = dtNursingData.Rows[i]["NURSING"].ToString().Trim();
                    string nursingDataNew = dtNursingDataNew.Rows[i]["NURSING"].ToString().Trim();

                    if (nursingData.Equals(nursingDataNew) == false && i < dtNursingData.Rows.Count - 1)
                    {
                        GVars.Msg.Show("ID011");                // 已打印过的记录有更改,不能续打!
                        return;
                    }

                    if (nursingData.Equals(nursingDataNew) == false && i == dtNursingData.Rows.Count - 1)
                    {
                        GVars.Msg.Show("ID010");                // 数据还未保存,请先保存后再打印!
                        return;
                    }
                }

                if (dtRngStart.Value.ToString(ComConst.FMT_DATE.SHORT) != dtRngEnd.Value.ToString(ComConst.FMT_DATE.SHORT))
                {
                    GVars.Msg.Show("ED007");                   // 只能打印一天的记录!
                    return;
                }

                // 获取需要打印的记录
                string filter = string.Empty;
                filter += "RECORDING_DATE >= " + SqlConvert.SqlConvert(dtRngStart.Value.ToString(ComConst.FMT_DATE.SHORT));
                filter += "AND RECORDING_DATE <= " + SqlConvert.SqlConvert(dtRngEnd.Value.ToString(ComConst.FMT_DATE.SHORT));   
                filter += "AND START_ROW < 10";
                DataRow[] drData = dtNursingData.Select(filter);

                int row = 10, page = 1;

                for (int i = 0; i < dtNursingData.Rows.Count; i++)
                {
                     int rowNew = Convert.ToInt32(dtNursingData.Rows[i]["END_ROW"].ToString());
                     int pageNew = Convert.ToInt32(dtNursingData.Rows[0]["END_PAGE"].ToString());
                    
                    if (rowNew > 0)
                    {
                        row = rowNew + 1;
                        page = pageNew;

                        if (row > 32)
                        {
                            row = 10;
                            page = pageNew + 1;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                // 打印
                if (excelAccess == null)
                {
                    excelAccess = new HISPlus.ExcelAccess();
                }

                setPrintInfo(filter);

                excelTemplatePrint(filter, drData, row, page, true);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        /// <summary>
        /// 表格控件EnterCell事件
        /// </summary>
        private void grdRecord_EnterCell(object sender, EventArgs e)
        {
            scrollValue = true;      // 是否触发滚动条事件

            // 判断用户是否有权更改记录
            if (GVars.User.Name.CompareTo(grdRecord.get_TextMatrix(grdRecord.Row, 14)) == 0)
            {
                // 定位输入框textbox
                if (grdRecord.Col == 13)
                {
                    int top = 0;
                    for (int i = grdRecord.TopRow; i < grdRecord.Row; i++)
                    {
                        top += grdRecord.get_RowHeight(i);
                    }

                    txtData.Top = grdRecord.Top + top / 15;
                    txtData.Height = grdRecord.get_RowHeight(grdRecord.Row) / 15;

                    // 判断界面显示的表格控件最后一列是否完整,若不完整则屏蔽掉滚动条事件
                    if ((txtData.Top + txtData.Height) > (grdRecord.Top + grdRecord.Height / 15))
                    {
                        scrollValue = false;
                    }

                    txtData.Focus();
                    txtData.Visible = true;
                    btnSave.Enabled = true;
                    txtData.Text = grdRecord.get_TextMatrix(grdRecord.Row, grdRecord.Col);
                    selectRow = grdRecord.Row;
                }
            }
        }


        /// <summary>
        /// 表格控件的滚动条事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdRecord_Scroll(object sender, EventArgs e)
        {
            if (scrollValue == true)
            {
                txtData.Top = txtDate.Top;
                txtData.Visible = false;
            }
            else
            {
                grdRecord_EnterCell(sender, e);
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
        #endregion


        #region 公共函数
        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {
            showPatientInfo();          
            setGrid();
            initDataTable(); 
        }


        /// <summary>
        /// 初始化表结构
        /// </summary>
        private void initDataTable()
        {
            dtNursingData = new DataTable("NURSING_DATA");

            DataColumn myDataColumn;  
            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.DateTime");
            //myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "RECORDING_DATE";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.DateTime");
            //myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "TIME_POINT";
            dtNursingData.Columns.Add(myDataColumn);

            //myDataColumn = new DataColumn();
            //myDataColumn.DataType = Type.GetType("System.String");
            //myDataColumn.ColumnName = "PATIENT_ID";
            //dtNursingData.Columns.Add(myDataColumn);

            //myDataColumn = new DataColumn();
            //myDataColumn.DataType = Type.GetType("System.Int");
            //myDataColumn.ColumnName = "VISIT_ID";
            //dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "TEMPERATURE";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "PULSE";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "BREATH";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "BLOOD";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "BLANK1";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "BLANK2";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "PROJECT";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "REAL_IN";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "PEE";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "STOOL";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "BLANK3";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "NURSING";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "SIGN";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "START_ROW";
            myDataColumn.DefaultValue = "0";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "START_PAGE";
            myDataColumn.DefaultValue = "1";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "END_ROW";
            myDataColumn.DefaultValue = "0";
            dtNursingData.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "END_PAGE";
            myDataColumn.DefaultValue = "1";
            dtNursingData.Columns.Add(myDataColumn);

            dtNursingData.AcceptChanges();
            dtNursingDataNew = dtNursingData.Clone();
            dtNursingDataNew.AcceptChanges();
        }


        /// <summary>
        /// 显示病人的基本信息
        /// </summary>
        private void showPatientInfo()
        {
            // 清空界面
            this.txtBedLabel.Text = string.Empty;                       // 病人床标号
            this.lblPatientName.Text = string.Empty;                    // 病人姓名
            this.lblGender.Text = string.Empty;                         // 病人性别
            this.lblAge.Text = string.Empty;                            // 年龄
            this.lblDeptName.Text = string.Empty;                       // 科别
            this.lblDate.Text = string.Empty;                        　 // 记录日期
            this.lblDiagnosis.Text = string.Empty;                      // 临床诊断

            // 如果没有数据退出
            if (dsPatient == null || dsPatient.Tables.Count == 0)
            {
                return;
            }

            // 显示病人的基本信息
            DataRow dr = dsPatient.Tables[0].Rows[0];
            
            PersonCls person = new PersonCls();
            if (DataType.IsDateTime(dr["DATE_OF_BIRTH"].ToString()) == true)
            {
                person.Birthday = DateTime.Parse(dr["DATE_OF_BIRTH"].ToString());
            }

            this.txtBedLabel.Text       = dr["BED_LABEL"].ToString();                   // 病人床标号
            this.lblPatientName.Text    = dr["NAME"].ToString();                        // 病人姓名
            this.lblGender.Text         = dr["SEX"].ToString();                         // 病人性别
            this.lblAge.Text            = person.GetAge(DateTime.Now);                  // 年龄
            this.lblDeptName.Text       = dr["DEPT_NAME"].ToString();                   // 科别
            this.lblDate.Text           = DataType.GetYMD(DateTime.Now.ToString());     // 当前日期
            this.lblDiagnosis.Text      = dr["DIAGNOSIS"].ToString();                   // 临床诊断

            patientId   = dr["PATIENT_ID"].ToString();
            visitId     = dr["VISIT_ID"].ToString();
        }


        /// <summary>
        /// 设置表格控件
        /// </summary>
        private void setGrid()
        {
            // 设置可编辑框textbox
            txtData.Visible = false;
            txtData.Left = txtNurse.Left;
            txtData.Width = txtNurse.Width;

            // 设置表格的位置
            grdRecord.Top = txtDate.Bottom;
            grdRecord.Left = txtDate.Left;

            // 设置表格的行数行高,列数及列宽
            grdRecord.Rows = 1;
            grdRecord.Cols = 15;
            grdRecord.set_ColWidth(0, txtDate.Width * 15);
            grdRecord.set_ColWidth(1, txtTime.Width * 15);
            grdRecord.set_ColWidth(2, txtTemp.Width * 15);
            grdRecord.set_ColWidth(3, txtPulse.Width * 15);
            grdRecord.set_ColWidth(4, txtBreath.Width * 15);
            grdRecord.set_ColWidth(5, txtBlood.Width * 15);
            grdRecord.set_ColWidth(6, txtBlank1.Width * 15);
            grdRecord.set_ColWidth(7, txtBlank2.Width * 15);
            grdRecord.set_ColWidth(8, txtProject.Width * 15);
            grdRecord.set_ColWidth(9, txtRealIn.Width * 15);
            grdRecord.set_ColWidth(10, txtPee.Width * 15);
            grdRecord.set_ColWidth(11, txtStool.Width * 15);
            grdRecord.set_ColWidth(12, txtBlank3.Width * 15);
            grdRecord.set_ColWidth(13, txtNurse.Width * 15);
            grdRecord.set_ColWidth(14, txtSign.Width * 15);
        }


        /// <summary>
        /// 获取病人的体征记录
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>
        private DataSet getPatientVitalSignRec(string patientId, string visitId)
        {
            string sql = string.Empty;

            string sqlDateRng = "( TO_DATE(VITAL_SIGNS_REC.RECORDING_DATE) >= "
                                + "TO_DATE('" + dtRngStart.Value.ToString(ComConst.FMT_DATE.SHORT) + "', 'yyyy-MM-dd') "
                                + "AND TO_DATE(VITAL_SIGNS_REC.RECORDING_DATE) <= "
                                + "TO_DATE('" + dtRngEnd.Value.ToString(ComConst.FMT_DATE.SHORT) + "', 'yyyy-MM-dd') "
                                + ") ";

            sql += "SELECT RECORDING_DATE, ";       // 记录日期
            sql += "TIME_POINT, ";                  // 时间点
            sql += "VITAL_SIGNS, ";                 // 记录项目
            sql += "CLASS_CODE, ";                  // 类别代码
            sql += "VITAL_CODE, ";                  // 项目代码
            sql += "VITAL_SIGNS_CVALUES, ";         // 项目数值
            sql += "NURSE ";
            sql += "FROM VITAL_SIGNS_REC ";             // 病人体症记录
            sql += "WHERE PATIENT_ID = " + SqlConvert.SqlConvert(patientId);        // 病人标识号
            sql += "AND VISIT_ID = " + SqlConvert.SqlConvert(visitId);
            sql += "AND " + sqlDateRng;
            sql += "AND CLASS_CODE <> 'C' ";
            sql += "AND CLASS_CODE <> 'D' ";
            sql += "ORDER BY RECORDING_DATE, ";         // 记录日期
            sql += "TIME_POINT, ";                  // 时间点
            sql += "CLASS_CODE, ";                  // 类别代码
            sql += "VITAL_CODE ";                   // 项目代码

            return GVars.OracleAccess.SelectData(sql);
        }


        /// <summary>
        /// 获取病情观察及护理措施
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <returns></returns>
        private DataSet getPatientNursingRecord(string patientId, string visitId)
        {
            string sql = string.Empty;

            sql += "SELECT * FROM NURSING_RECORD ";
            sql += "WHERE PATIENT_ID = " + SqlConvert.SqlConvert(patientId);        // 病人标识号
            sql += "AND VISIT_ID = " + SqlConvert.SqlConvert(visitId);
            sql += "AND RECORDING_DATE >= " + SqlConvert.SqlConvert(dtRngStart.Value.ToString(ComConst.FMT_DATE.SHORT));
            sql += "AND RECORDING_DATE <= " + SqlConvert.SqlConvert(dtRngEnd.Value.ToString(ComConst.FMT_DATE.SHORT));
            sql += "ORDER BY RECORDING_DATE, ";         // 记录日期
            sql += "TIME_POINT ";                       // 时间点

            return GVars.SqlserverAccess.SelectData(sql, "NURSING_RECORD");
        }


        /// <summary>
        /// 显示病人的体征信息
        /// </summary>
        /// <returns></returns>
        private bool showPatientVitalSignRec()
        {
            if (dsVitalSignRec == null || dsVitalSignRec.Tables.Count == 0 || dsVitalSignRec.Tables[0].Rows.Count == 0)
            {
                return true;
            }
            
            // 显示数据
            grdRecord.Rows = dtNursingData.Rows.Count;
       
            int row = 0;
            int nursingWidth = grdRecord.get_ColWidth(13) / 15; 

            foreach (DataRow dr in dtNursingData.Rows)
            {
                grdRecord.set_TextMatrix(row, 0, DataType.GetMD(dr["RECORDING_DATE"].ToString()));
                grdRecord.set_TextMatrix(row, 1, DataType.GetShortTime(dr["TIME_POINT"].ToString()));
                grdRecord.set_TextMatrix(row, 2, dr["TEMPERATURE"].ToString());
                grdRecord.set_TextMatrix(row, 3, dr["PULSE"].ToString());
                grdRecord.set_TextMatrix(row, 4, dr["BREATH"].ToString());
                grdRecord.set_TextMatrix(row, 5, dr["BLOOD"].ToString());
                grdRecord.set_TextMatrix(row, 8, dr["PROJECT"].ToString());
                grdRecord.set_TextMatrix(row, 9, dr["REAL_IN"].ToString());
                grdRecord.set_TextMatrix(row, 10, dr["PEE"].ToString());
                grdRecord.set_TextMatrix(row, 11, dr["STOOL"].ToString());
                grdRecord.set_TextMatrix(row, 13, dr["NURSING"].ToString());
                grdRecord.set_TextMatrix(row, 14, dr["SIGN"].ToString());

                int rowNum = getRowNum(dr["NURSING"].ToString(), this.grdRecord.Font, nursingWidth);
                grdRecord.set_RowHeight(row, rowNum * ROW_HEIGHT + OFF_SET);

                row++;
            }

            lblDate.Text = DataType.GetYMD(dsVitalSignRec.Tables[0].Rows[0]["RECORDING_DATE"].ToString());

            return true;
        }


        /// <summary>
        /// 合并相同时间点的护理数据
        /// </summary>
        /// <param name="dt"></param>
        private void mergeNursingData(ref DataTable dt)
        {
            if (dsVitalSignRec == null || dsVitalSignRec.Tables.Count == 0)
            {
                return;
            }

            dt.Clear();             // 清除原有的界面护理数据记录
            dt.AcceptChanges();

            string timePoint = string.Empty;
            int row = 0;

            for (int i = 0; i < dsVitalSignRec.Tables[0].Rows.Count; i++)
            {
                string timePointStr = dsVitalSignRec.Tables[0].Rows[i]["TIME_POINT"].ToString();

                if (timePoint.Equals(timePointStr) == true)
                {
                    setVitalSignsValue(dtNursingData.Rows[row - 1], dsVitalSignRec.Tables[0].Rows[i]); 
                }
                else               // 新增
                {
                    DataRow drNew = dt.NewRow();
                    drNew["RECORDING_DATE"] = dsVitalSignRec.Tables[0].Rows[i]["RECORDING_DATE"].ToString();
                    drNew["TIME_POINT"] = dsVitalSignRec.Tables[0].Rows[i]["TIME_POINT"].ToString();

                    if (setVitalSignsValue(drNew, dsVitalSignRec.Tables[0].Rows[i]) == false)
                    {
                        continue;
                    }
                   
                    foreach (DataRow dr1 in dsNursingRecord.Tables[0].Rows)
                    {
                        if (dr1["TIME_POINT"].ToString().Trim().CompareTo(timePointStr.Trim()) == 0)
                        {
                            drNew["NURSING"] = dr1["REPRSENTATION"].ToString();
                            drNew["START_ROW"] = dr1["START_ROW"].ToString();
                            drNew["START_PAGE"] = dr1["START_PAGE"].ToString();
                            drNew["END_ROW"] = dr1["END_ROW"].ToString();
                            drNew["END_PAGE"] = dr1["END_PAGE"].ToString();
                            break;
                        }
                    }

                    drNew["SIGN"] = dsVitalSignRec.Tables[0].Rows[i]["NURSE"].ToString();
                    dt.Rows.Add(drNew);
                    row++;

                    timePoint = timePointStr;
                }
            }

            dt.AcceptChanges();
        }


        /// <summary>
        /// 设置项目值
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="drSrc"></param>
        /// <returns>true:该项目值需要显示在界面上 false:该项目值不需要显示在界面上</returns>
        private bool setVitalSignsValue(DataRow dr, DataRow drSrc)
        {
            bool isShow = true;
            string vitalSigns = drSrc["VITAL_SIGNS"].ToString();

            if (vitalSigns.Length > 2 && String.Compare(vitalSigns.Substring(2), "体温") == 0)
            {
                vitalSigns = "体温";
            }

            switch (vitalSigns)
            {
                case "体温":
                    dr["TEMPERATURE"] = drSrc["VITAL_SIGNS_CVALUES"].ToString();
                    break;

                case "脉搏":
                    dr["PULSE"] = drSrc["VITAL_SIGNS_CVALUES"].ToString();
                    break;

                case "呼吸":
                    dr["BREATH"] = drSrc["VITAL_SIGNS_CVALUES"].ToString();
                    break;

                case "血压":
                    dr["BLOOD"] = drSrc["VITAL_SIGNS_CVALUES"].ToString();
                    break;

                case "摄入液量":
                    dr["PROJECT"] = drSrc["VITAL_SIGNS"].ToString();
                    dr["REAL_IN"] = drSrc["VITAL_SIGNS_CVALUES"].ToString();
                    break;

                case "排除液量":
                    dr["PEE"] = drSrc["VITAL_SIGNS_CVALUES"].ToString();
                    break;

                case "大便":
                    dr["STOOL"] = drSrc["VITAL_SIGNS_CVALUES"].ToString();
                    break;

                default:
                    isShow = false;
                    break;
            }

            return isShow;
        }


        /// <summary>
        /// 获取表格控件上的数据
        /// </summary>
        private void getGrdNursingData(ref DataTable dt)
        {
            dt.Clear();
            dt.AcceptChanges();

            for (int i = 0; i < grdRecord.Rows; i++)
            {
                DataRow drNew = dt.NewRow();
                drNew["NURSING"] = grdRecord.get_TextMatrix(i, 13);
                dt.Rows.Add(drNew);
            }
            dt.AcceptChanges();
        }


        /// <summary>
        /// 用Excel模板打印，比较适合套打、格式、统计分析报表、图形分析、自定义打印
        /// </summary>
        /// <remarks>用Excel打印，步骤为：打开、写数据、打印预览、关闭</remarks>
        private void excelTemplatePrint(string filter, DataRow[] drData, int row, int page, bool isFirst)
        {
            // 打开打印模板
            string excelFile = System.IO.Path.Combine(System.Environment.CurrentDirectory, "护理记录单.xls");

            excelAccess.Open(excelFile);	                            // 用模板文件
            excelAccess.IsVisibledExcel = true;
            excelAccess.FormCaption = string.Empty;

            excelAccess.PrintInfo = excelAccess.printNursingData(filter, drData, grfx, ref dsNursingRecord, row, page, excelAccess.PatientInfo, isFirst);

            // 打印
            excelAccess.PrintPreview();			                        // 预览

            excelAccess.Close(false);	                                // 关闭

            while (excelAccess.PrintInfo.Pagination == true)            // 分页
            {
                excelTemplatePrint(filter, excelAccess.PrintInfo.DataRowList, excelAccess.PrintInfo.RowNum, excelAccess.PrintInfo.PageNum, excelAccess.PrintInfo.IsShow);
            }

            // 将记录有行号的数据集保存至数据库
            updateNursingRecord();           
        }


        /// <summary>
        /// 将记录有行号的护理记录数据集保存至sql数据库
        /// </summary>
        private void updateNursingRecord()
        {
            // 将记录有行号的数据集保存至数据库
            string sql = string.Empty;
            sql += "SELECT * FROM NURSING_RECORD ";
            sql += "WHERE PATIENT_ID = " + SqlConvert.SqlConvert(patientId);        // 病人标识号
            sql += "AND VISIT_ID = " + SqlConvert.SqlConvert(visitId);
            sql += "AND RECORDING_DATE >= " + SqlConvert.SqlConvert(dtRngStart.Value.ToString(ComConst.FMT_DATE.SHORT));
            sql += "AND RECORDING_DATE <= " + SqlConvert.SqlConvert(dtRngEnd.Value.ToString(ComConst.FMT_DATE.SHORT));
            sql += "ORDER BY RECORDING_DATE, ";         // 记录日期
            sql += "TIME_POINT ";                       // 时间点

            GVars.SqlserverAccess.Update(ref dsNursingRecord, "NURSING_RECORD", sql); 
        }


        /// <summary>
        /// 要打印的病人的基本信息
        /// </summary>
        private void setPrintInfo(string filter)
        {
            DataTable dt = dtNursingData.Copy();
            excelAccess.DTNursingDataNew = dt; 
            excelAccess.PatientInfo.Name = lblPatientName.Text;
            excelAccess.PatientInfo.Gender = lblGender.Text;
            excelAccess.PatientInfo.Age = lblAge.Text;
            excelAccess.PatientInfo.DeptName = lblDeptName.Text;
            excelAccess.PatientInfo.BedNo = txtBedLabel.Text.Trim();
            excelAccess.PatientInfo.Date = lblDate.Text;
            excelAccess.PatientInfo.Diagnosis = lblDiagnosis.Text;
        }


        /// <summary>
        /// 返回字符串在文本框中所占行数(考虑到换行的情况)
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="strFont"></param>
        /// <param name="colWidth"></param>
        /// <returns></returns>
        private int getRowNum(string strValue, Font strFont, int colWidth)
        {
            string[] strSplit = strValue.Split('\r');
            int rowNum = 0;

            float heightUnit = grfx.MeasureString("医", strFont).Height;
            float widthUnit = grfx.MeasureString("医", strFont).Width;
            float widthAll = 0.0F;
         
            for (int i = 0; i < strSplit.Length; i++)
            {
                widthAll = grfx.MeasureString(strSplit[i], strFont).Width;
                int rowNumer = 0;

                if (widthAll > colWidth)
                {
                    rowNumer = (int)(widthAll / colWidth);
                    float mode = widthAll % colWidth;
                    if (mode > 0)
                    {
                        rowNumer += 1;
                    }
                }
                else
                {
                    rowNumer += 1;
                }

                rowNum += rowNumer;
            }

            return rowNum;
        }
        #endregion
    }
}