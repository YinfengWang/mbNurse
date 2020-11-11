using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class NursingRecDocument : Form
    {
        #region 窗体变量
        private NursingRecDocumentCom   com         = new NursingRecDocumentCom();
        
        private PatientDbI              patientCom;                                             // 病人
        
        private DataSet                 dsPatient   = null;
        private string                  patientId   = string.Empty;
        private string                  visitId     = string.Empty;
        
        private DataRow[]               drShow      = null;
        
        private int                     BLANK_ROWS  = 40;
        private int                     COL_DESC    = 12;
        #endregion
        
        public NursingRecDocument()
        {
            InitializeComponent();

            this.txtBedLabel.KeyDown += new KeyEventHandler( txtBedLabel_KeyDown );
            this.txtBedLabel.GotFocus += new EventHandler(imeCtrl_GotFocus);
            this.grdTitle.EnterCell += new EventHandler( grdTitle_EnterCell );
                        
            this.grdTitle.DblClick += new EventHandler( grdTitle_DblClick );
        }
        
        
        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NursingRecDocument_Load( object sender, EventArgs e )
        {
            try
            {
                initFrmVal();
                initDisp();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 床标文本回车来查询病人信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtBedLabel_KeyDown( object sender, KeyEventArgs e )
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
                
                // 清空界面
                initDisp();
                
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
        /// 按钮[查询]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click( object sender, EventArgs e )
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (patientId.Length == 0)
                {
                    GVars.Msg.Show("E00011");                                                   // 请先确定病人!
                    return;
                }

                // 获取数据
                com.SetDescWidth(grdTitle.get_ColWidth(COL_DESC) / ComConst.VAL.TWIPS_PER_PIXEL + 12);
                com.Now = GVars.OracleAccess.GetSysDate();                                                 // 获取现在的时间
                com.SelectData(patientId, visitId, dtRngStart.Value, dtRngEnd.Value);           // 获取数据                
                
                // 显示数据
                showNursingRec(string.Empty);
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        
        /// <summary>
        /// 进入单元格事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void grdTitle_EnterCell( object sender, EventArgs e )
        {
        }
        
        
        /// <summary>
        /// 双击表格以编辑病情观察及护理措施
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void grdTitle_DblClick( object sender, EventArgs e )
        {
            try
            {
                DateTime    dtTimePoint = GVars.OracleAccess.GetSysDate();
                string      timeStr     = string.Empty;
                int         rowStart    = grdTitle.FixedRows;                               // 某一个时间记录在表格中的开始行
                string      desc        = string.Empty;
                DataRow     drEdit      = null;
                
                string      filterTime  = "(TIME_POINT >= '{0}' AND TIME_POINT <= '{1}')";  // 查询条件
                string      timePoint   = string.Empty;
                string      timePointEnd= string.Empty;
                
                if (drShow == null) { return; }
                
                // 获取记录
                if (grdTitle.Row >= grdTitle.FixedRows && grdTitle.Row < grdTitle.Rows)
                {
                    timeStr = grdTitle.get_TextMatrix(grdTitle.Row, grdTitle.Cols - 1);
                    
                    for(int i = grdTitle.Row; i >= grdTitle.FixedRows; i--)
                    {
                        if (timeStr.Equals(grdTitle.get_TextMatrix(i, grdTitle.Cols - 1)) == false)
                        {
                            rowStart = i + 1;
                            break;
                        }
                    }
                    
                    if (timeStr.Length > 0)
                    {
                        dtTimePoint     = DateTime.Parse(timeStr);
                        timePoint       = dtTimePoint.ToString(ComConst.FMT_DATE.LONG_MINUTE);
                        timePointEnd    = timePoint + ":59";
                        
                        DataRow[] drFind = com.DsShow.Tables[0].Select(string.Format(filterTime, timePoint, timePointEnd));
                        
                        if (drFind.Length > 0)
                        {
                            drEdit = drFind[0];
                            desc = drEdit["OBSERVE_NURSE"].ToString();
                        }
                    }
                }
                
                // 编辑记录
                TextEditor frmShow = new TextEditor();
                
                frmShow.TimePoint = dtTimePoint.ToString(ComConst.FMT_DATE.LONG);
                frmShow.Desc      = desc.Replace(ComConst.STR.TAB, string.Empty);
                
                frmShow.ShowDialog();
                
                // 保存记录
                if (frmShow.ArrDesc != null)
                {
                    desc = string.Empty;
                    
                    for(int i = 0; i < frmShow.ArrDesc.Length;i ++)
                    {
                        desc += frmShow.ArrDesc[i] + ComConst.STR.TAB;
                    }
                    
                    desc = desc.TrimEnd();
                    
                    // 查找记录
                    dtTimePoint     = DateTime.Parse(frmShow.TimePoint);
                    timePoint       = dtTimePoint.ToString(ComConst.FMT_DATE.LONG_MINUTE);
                    timePointEnd    = timePoint + ":59";
                    
                    DataRow[] drFind = com.DsShow.Tables[0].Select(string.Format(filterTime, timePoint, timePointEnd));
                    
                    if (drFind.Length > 0)
                    {
                        drEdit = drFind[0];
                        
                        drEdit["OBSERVE_NURSE"] = desc;
                        drEdit["ROWS_DESC"]     = frmShow.Lines.ToString();
                        
                        // 判断是否是删除
                        if (desc.Trim().Length == 0
                            && drEdit["TEMPERATURE"].ToString().Length == 0
                            && drEdit["PULSE"].ToString().Length == 0
                            && drEdit["BREATH"].ToString().Length == 0
                            && drEdit["BLOOD_PRESSURE"].ToString().Length == 0
                            && drEdit["COL1"].ToString().Length == 0
                            && drEdit["COL2"].ToString().Length == 0
                            && drEdit["ITEM_OUT"].ToString().Length == 0
                            && drEdit["ITEM_IN"].ToString().Length == 0
                        )
                        {
                            drEdit.Delete();                                
                        }
                    }
                    else
                    {
                        drEdit = com.DsShow.Tables[0].NewRow();
                        
                        drEdit["PATIENT_ID"]     = patientId;
                        drEdit["VISIT_ID"]       = visitId;
                        drEdit["TIME_POINT"]     = timePoint;
                        drEdit["OBSERVE_NURSE"]  = desc;
                        drEdit["SUM_FLG"]        = "0";
                        drEdit["SHOW_ORDER"]     = "0";
                        drEdit["ROWS_COUNT_PRE"] = "0";
                        drEdit["ROWS_COUNT"]     = "1";
                        drEdit["ROWS_DESC"]      = frmShow.Lines.ToString();
                        
                        com.DsShow.Tables[0].Rows.Add(drEdit);
                    }
                    
                    // 按钮状态
                    this.btnSave.Enabled = true;
                }
                
                // 刷新显示
                showNursingRec(frmShow.TimePoint);
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[保存]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click( object sender, EventArgs e )
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (patientId.Length == 0)
                {
                    GVars.Msg.Show("E00011");                           // E00011	请先确定病人!

                    return;
                }

                if (com.SaveData() == true)
                {
                    GVars.Msg.Show("I00004", "保存");                   // {0}成功!
                    
                    this.btnSave.Enabled = false;
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        
        /// <summary>
        /// 按钮[续打]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContinuePrint_Click( object sender, EventArgs e )
        {
            
        }
        
        
        /// <summary>
        /// 按钮[退出]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click( object sender, EventArgs e )
        {
            try
            {
                this.Close();
            }
            catch(Exception ex)
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
        #endregion
        
        
        #region 共通函数
        /// <summary>
        /// 初始化窗体变量
        /// </summary>
        private void initFrmVal()
        {
            patientCom = new PatientDbI(GVars.OracleAccess);
            
            com.CreateDataTable();
        }
        

        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {
            grdTitle.RowHeightMin = 250;
            grdTitle.set_RowHeight(0, 600);
            
            grdTitle.set_TextMatrix(0, 0, "  日" + ComConst.STR.CRLF + ComConst.STR.CRLF + "  期");
            grdTitle.set_TextMatrix(0, 1, "  时" + ComConst.STR.CRLF + ComConst.STR.CRLF + "  间");
            grdTitle.set_TextMatrix(0, 2, " 体" + ComConst.STR.CRLF + ComConst.STR.CRLF + " 温");
            grdTitle.set_TextMatrix(0, 3, "脉" + ComConst.STR.CRLF + ComConst.STR.CRLF + "搏");
            grdTitle.set_TextMatrix(0, 4, "呼" + ComConst.STR.CRLF + ComConst.STR.CRLF + "吸");
            grdTitle.set_TextMatrix(0, 5, "  血" + ComConst.STR.CRLF + ComConst.STR.CRLF + "  压");
            
            // 清除表格内容
            grdTitle.Rows = grdTitle.FixedRows;
            grdTitle.Rows = grdTitle.FixedRows + BLANK_ROWS;
            
            grdTitle.Row = 0;
            grdTitle.Col = COL_DESC;
            grdTitle.CellAlignment = 4;
            
            // 清除病人信息
            lblName.Text    = string.Empty;
            lblGender.Text  = string.Empty;
            lblAge.Text     = string.Empty;
            lblDeptName.Text = string.Empty;
            lblDocNo.Text   = string.Empty;
            lblDiagnose.Text = string.Empty;

            // 按钮位置
        }        
        
        
        /// <summary>
        /// 显示病人的基本信息
        /// </summary>
        private void showPatientInfo()
        { 
            patientId   = string.Empty;
            visitId     = string.Empty;
            
            // 如果没有数据退出
            if (dsPatient == null || dsPatient.Tables.Count == 0 || dsPatient.Tables[0].Rows.Count == 0)
            {
                GVars.Msg.Show("W00005");                               // 该病人不存在!
                return;
            }
            
            // 显示病人的基本信息
            DataRow dr = dsPatient.Tables[0].Rows[0];
            PersonCls person = new PersonCls();

            string age = dr["DATE_OF_BIRTH"].ToString();
            if (age.Length > 0)
            {
                age = PersonCls.GetAge(DateTime.Parse(age), GVars.OracleAccess.GetSysDate());
            }
            
            this.lblName.Text       = dr["NAME"].ToString();            // 病人姓名
            this.lblGender.Text     = dr["SEX"].ToString();             // 病人性别
            this.lblAge.Text        = age;                              // 年龄
            this.lblDeptName.Text   = dr["DEPT_NAME"].ToString();       // 科别
            this.lblDocNo.Text      = dr["INP_NO"].ToString();          // 住院号
            this.lblDiagnose.Text   = dr["DIAGNOSIS"].ToString();       // 诊断
            
            patientId   = dr["PATIENT_ID"].ToString();
            visitId     = dr["VISIT_ID"].ToString();
        }
        
        
        /// <summary>
        /// 显示护理记录
        /// </summary>
        /// <param name="timePoint"></param>
        /// <param name="rowStart"></param>
        private void showNursingRec(string timePoint)
        {
            DataRow[] drShow2 = null;
            
            int     rowStart = 0;
            
            // 如果没有确定时间点, 显示全部数据
            if (timePoint.Length == 0)
            {
                rowStart = grdTitle.FixedRows;
                drShow = com.DsShow.Tables[0].Select(string.Empty, "TIME_POINT ASC, SHOW_ORDER ASC");
                drShow2 = drShow;
            }
            // 如果指定时间点, 显示该时间点及以后的数据
            else
            {
                // 获取该时间点以前的数据所占行数
                string filter = "TIME_POINT < " + HISPlus.SqlManager.SqlConvert(timePoint);
                drShow2 = com.DsShow.Tables[0].Select(filter, "TIME_POINT ASC, SHOW_ORDER ASC");
                rowStart = grdTitle.FixedRows + getShowRows(drShow2);
                
                filter = "TIME_POINT >= " + HISPlus.SqlManager.SqlConvert(timePoint);
                drShow2 = com.DsShow.Tables[0].Select(filter, "TIME_POINT ASC, SHOW_ORDER ASC");
            }
            
            // 计算行数
            int rows = getShowRows(drShow2);
            
            // 清空表格
            grdTitle.Rows   = rowStart;
            grdTitle.Rows   = rowStart + rows + BLANK_ROWS;
            
            // 显示内容
            showNursingRec(drShow2, rowStart);
            
            // 定位当前行
            grdTitle.Row = rowStart;
        }
        
        
        /// <summary>
        /// 显示护理数据
        /// </summary>
        private void showNursingRec(DataRow[] drArray, int rowStart)
        {
            string strDate  = string.Empty;
            string strTime  = string.Empty;
            DateTime timePoint;
            
            int     row     = rowStart;
            int     col     = grdTitle.FixedCols;
            string  desc    = string.Empty;
            string  timeStr = string.Empty;
            bool    blnSum  = false;
            
            int     rowDesc = 0;
            string[] arrDesc = null;
            
            // 获取前一记录的时间
            if (rowStart > grdTitle.FixedRows)
            {
                string strValue = grdTitle.get_TextMatrix(rowStart - 1, grdTitle.Cols - 1);
                if (strValue.Length > 0)
                {
                    timePoint = DateTime.Parse(strValue);
                    
                    strDate = timePoint.ToString("MM.dd");
                    strTime = timePoint.ToString(ComConst.FMT_DATE.TIME_SHORT);
                }
            }
            
            // 显示数据
            for(int i = 0; i < drArray.Length; i++)
            {
                DataRow dr = drArray[i];
                
                timePoint = DateTime.Parse(dr["TIME_POINT"].ToString());
                
                blnSum = ("1".Equals(dr["SUM_FLG"].ToString()));
                
                // 如果时间点变化了, 而病情没写完, 写完病情
                if ((strDate.Equals(timePoint.ToString("MM.dd")) == false
                    || strTime.Equals(timePoint.ToString(ComConst.FMT_DATE.TIME_SHORT)) == false)
                    && (arrDesc != null && rowDesc < arrDesc.Length))
                {
                    for(int j = rowDesc; j < arrDesc.Length; j++)
                    {
                        grdTitle.set_TextMatrix(row, grdTitle.Cols - 1, timeStr);               // 时间点
                        grdTitle.set_TextMatrix(row++, COL_DESC, arrDesc[rowDesc++]);           // 病情观察
                    }
                }
                
                // 如果是小计
                if (blnSum == true)
                {
                    desc    = dr["OBSERVE_NURSE"].ToString();
                    arrDesc = desc.Split(ComConst.STR.TAB.ToCharArray());
                    rowDesc = 0;

                    col = 0;
                    grdTitle.set_TextMatrix(row, col++, strDate);                               // 日期
                     
                    for (int j = 0; j < arrDesc.Length; j++)
                    {
                        col = COL_DESC;

                        if (rowDesc < arrDesc.Length)
                        {
                            grdTitle.set_TextMatrix(row, col++, arrDesc[rowDesc++]);            // 病情观察
                        }
                        else
                        {
                            grdTitle.set_TextMatrix(row, col++, string.Empty);                  // 病情观察
                        }

                        grdTitle.set_TextMatrix(row, col++, dr["NURSE"].ToString());            // 护士

                        // 时间点
                        timeStr = dr["TIME_POINT"].ToString();
                        grdTitle.set_TextMatrix(row, col++, timeStr);

                        row++;
                    }

                    continue;
                }
                
                // 如果日期变了
                if (strDate.Equals(timePoint.ToString("MM.dd")) == false)
                {
                    col = 0;
                    
                    desc    = dr["OBSERVE_NURSE"].ToString();
                    arrDesc = desc.Split(ComConst.STR.TAB.ToCharArray());
                    rowDesc = 0;
                    
                    strDate = timePoint.ToString("MM.dd");
                    strTime = timePoint.ToString(ComConst.FMT_DATE.TIME_SHORT);
                    
                    grdTitle.set_TextMatrix(row, col++, strDate);                       // 日期
                    grdTitle.set_TextMatrix(row, col++, strTime);                       // 时间
                }
                // 如果时间变了
                else if (strTime.Equals(timePoint.ToString(ComConst.FMT_DATE.TIME_SHORT)) == false)
                {
                    col = 1;
                    
                    desc    = dr["OBSERVE_NURSE"].ToString();
                    arrDesc = desc.Split(ComConst.STR.TAB.ToCharArray());
                    rowDesc = 0;
                    
                    strTime = timePoint.ToString(ComConst.FMT_DATE.TIME_SHORT);
                    
                    grdTitle.set_TextMatrix(row, col++, strTime);                       // 时间
                }
                // 同一时间点测量的数据
                else
                {
                    col = 2;
                }
                                
                grdTitle.set_TextMatrix(row, col++, dr["TEMPERATURE"].ToString());      // 体温
                grdTitle.set_TextMatrix(row, col++, dr["PULSE"].ToString());            // 脉搏
                grdTitle.set_TextMatrix(row, col++, dr["BREATH"].ToString());           // 呼吸
                grdTitle.set_TextMatrix(row, col++, dr["BLOOD_PRESSURE"].ToString());   // 血压
                col++;                                                                  // 空1
                col++;                                                                  // 空2
                grdTitle.set_TextMatrix(row, col++, dr["ITEM_IN"].ToString());          // 入量项目
                grdTitle.set_TextMatrix(row, col++, dr["ITEM_IN_AMOUNT"].ToString());   // 入量值
                grdTitle.set_TextMatrix(row, col++, dr["ITEM_OUT"].ToString());         // 出量项目
                grdTitle.set_TextMatrix(row, col++, dr["ITEM_OUT_AMOUNT"].ToString());  // 出量值
                
                if (rowDesc < arrDesc.Length)
                {
                    grdTitle.set_TextMatrix(row, col++, arrDesc[rowDesc++]);            // 病情观察
                }
                else
                {
                    grdTitle.set_TextMatrix(row, col++, string.Empty);                  // 病情观察
                }
                
                grdTitle.set_TextMatrix(row, col++, dr["NURSE"].ToString());            // 护士
                
                // 时间点
                timeStr = dr["TIME_POINT"].ToString();
                grdTitle.set_TextMatrix(row, col++, timeStr);
                
                row++;                
            }
            
            // 如果病情没写完, 写完病情
            if (arrDesc != null && rowDesc < arrDesc.Length)
            {
                for(int j = rowDesc; j < arrDesc.Length; j++)
                {
                    grdTitle.set_TextMatrix(row, grdTitle.Cols - 1, timeStr);           // 时间点
                    grdTitle.set_TextMatrix(row, COL_DESC, arrDesc[rowDesc++]);         // 病情观察
                    row++;
                }
            }
        }
        
        
        /// <summary>
        /// 获取显示的行数, 并把结果保存到记录中
        /// </summary>
        /// <returns></returns>
        private int getShowRows(DataRow[] drArray)
        {
            int allRows     = 0;                    // 总行数
            int descRows    = 0;                    // 描述部份所占行数
            int rows        = 0;                    // 某一个时间点的行数
            
            string timePoint = string.Empty;
            DataRow drRec   = null;
            
            for(int i = 0; i < drArray.Length; i++)
            {
                DataRow dr = drArray[i];                                
                
                if (timePoint.Equals(dr["TIME_POINT"].ToString()) == false)
                {
                    rows = (rows > descRows ? rows : descRows);
                    allRows += rows;
                    
                    if (drRec != null && rows != int.Parse(drRec["ROWS_COUNT"].ToString()))
                    {
                        drRec["ROWS_COUNT"] = rows;
                    }
                    
                    descRows = int.Parse(dr["ROWS_DESC"].ToString());                    
                    rows    = 1;
                    
                    timePoint = dr["TIME_POINT"].ToString();
                    drRec = dr;
                }
                else
                {
                    rows++;
                }
            }
            
            if (drArray.Length > 0)
            {
                rows = (rows > descRows ? rows : descRows);
                allRows += rows;
                                
                if (rows != int.Parse(drRec["ROWS_COUNT"].ToString()))
                {
                    drRec["ROWS_COUNT"] = rows;
                }
            }
            
            return allRows;            
        }
        #endregion

        private void btnReprint_Click(object sender, EventArgs e)
        {

        }

        private void grdTitle_Enter(object sender, EventArgs e)
        {

        }
    }
}