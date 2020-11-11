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
    public partial class NurseRecordWz : Form
    {
       #region 窗体变量
        //private NursingRecDocumentCom   com         = new NursingRecDocumentCom();
        
        private PatientDbI              patientCom;                                             // 病人
        
        private DataSet                 dsPatient   = null;
        private string                  patientId   = string.Empty;
        private string                  visitId     = string.Empty;
        private bool toselect = false;
        private object obj = null;

        private string CellTemp = string.Empty;
        private int linenumber = 10;

       #endregion

        public NurseRecordWz()
        {
            InitializeComponent();

            this.txtBedLabel.KeyDown += new KeyEventHandler( txtBedLabel_KeyDown );
            this.txtBedLabel.GotFocus += new EventHandler(imeCtrl_GotFocus);

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
                
                PatientGridView.DataSource = PatientData(patientId, visitId).Tables[0].DefaultView;
                toselect = false;
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
                //com.SetDescWidth(grdTitle.get_ColWidth(COL_DESC) / ComConst.VAL.TWIPS_PER_PIXEL + 12);
                PatientGridView.DataSource = PatientData(patientId, visitId, dtRngStart.Value, dtRngEnd.Value).Tables[0].DefaultView;           // 获取数据                
                
                // 显示数据
                showNursingRec(string.Empty);
                toselect = true;
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
        /// 按钮[保存]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click( object sender, EventArgs e )
        {
            if (txtBedLabel.Text.Trim().Length == 0)
            {
                return;
            }
            try
            {
                string sql = "DELETE FROM NURSING_RECORD WHERE ";
                sql += "PATIENT_ID = '" + patientId + "'";
                sql += "AND VISIT_ID = '" + visitId + "'";
                sql += "AND NURSING_DATE = " + SQL.GetOraDbDate(this.PatientGridView["Column15", PatientGridView.CurrentCell.RowIndex].Value.ToString());
                GVars.OracleAccess.ExecuteNoQuery(sql);
                if (toselect)
                {
                    PatientGridView.DataSource = PatientData(patientId, visitId, dtRngStart.Value, dtRngEnd.Value).Tables[0].DefaultView;
                }
                if (!toselect)
                {
                    PatientGridView.DataSource = PatientData(patientId, visitId).Tables[0].DefaultView;
                }



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
        /// 按钮[打印]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnContinuePrint_Click( object sender, EventArgs e )
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // 获取需要打印的记录
                //string filter = string.Empty;
                DataTable dtl;
                DataRow[] drData = null;
                if (toselect)
                {
                    dtl = PatientData(patientId, visitId, dtRngStart.Value, dtRngEnd.Value).Tables[0];
                    drData = dtl.Select();
                }
                if (!toselect)
                {
                    dtl = PatientData(patientId, visitId).Tables[0];
                    drData = dtl.Select();
                }
                //打印输出的字符串处理
                string str = null;
                StreamReader sr = new StreamReader(@"Template\护理记录模板(危重).txt", Encoding.Default);
                str = sr.ReadToEnd();
                sr.Close();
                string[] htmtxt = str.Split(new char[] { '⊙' });
                string htmtxt1 = htmtxt[0].ToString();
                string htmtxt2 = htmtxt[1].ToString().Replace("[#姓名]", lblName.Text).Replace("[#科别]", lblDeptName.Text).Replace("[#床号]", txtBedLabel.Text).Replace("[#住院号]", patientId).Replace("[#临床诊断]", lblDiagnose.Text);
                string htmtxt3 = htmtxt[2].ToString();

                string htmtxt4 = null;
                if (drData.Length > 0)
                {

                    linenumber = int.Parse(GVars.IniFile.ReadString("LINENUMBER", "NurseRecordWz", string.Empty));
                    DateTime dt3;
                    for (int i = 0; i < drData.Length; i++)
                    {
                        DateTime dtm = DateTime.Parse(drData[i][0].ToString());
                        string sss = htmtxt[3].ToString();
                        if (drData[i][14].ToString() != null)
                        {
                            try
                            {
                                dt3 = DateTime.Parse(drData[i][14].ToString());
                            }
                            catch
                            {
                                dt3 = DateTime.Now;
                            }
                            //取特定列的值
                            if (dt3.Hour > 7 && dt3.Hour < 19)
                            {
                                sss = sss.Replace("[#0#]", "style5");
                            }
                            else
                            {
                                sss = sss.Replace("[#0#]", "style6");

                            }
                            if (drData[i][6].ToString() != null)
                            {

                                if (drData[i][6].ToString() == "总结")
                                {
                                    sss = sss.Replace("style6", "style5");
                                    sss = sss.Replace("<tr bordercolor='#000000'>", "<tr bordercolor='#FF0000'>");
                                }

                            }
                        }

                        

                        TextDesc DET = new TextDesc();
                        string desc = DET.TextEditDesc(drData[i][13].ToString(), linenumber);
                        string[] descs = desc.Replace("\r\n", "⊙").Split(new char[] { '⊙' });
                        string Strhtm = sss;
                        for (int j = 0; j < descs.Length; j++)
                        {
                            sss = Strhtm;
                            if (j == 0)
                            {
                                sss = sss.Replace("[#0]", dtm.ToShortDateString()).Replace("[#1]", dtm.ToShortTimeString());
                                sss = sss.Replace("[#2]", drData[i][2].ToString());
                                sss = sss.Replace("[#3]", drData[i][3].ToString());
                                sss = sss.Replace("[#4]", drData[i][4].ToString());
                                sss = sss.Replace("[#5]", drData[i][5].ToString());
                                sss = sss.Replace("[#6]", drData[i][6].ToString());
                                sss = sss.Replace("[#7]", drData[i][7].ToString());
                                sss = sss.Replace("[#8]", drData[i][8].ToString());
                                sss = sss.Replace("[#9]", drData[i][9].ToString());
                                sss = sss.Replace("[#10]", drData[i][10].ToString());
                                sss = sss.Replace("[#11]", drData[i][11].ToString());
                                sss = sss.Replace("[#12]", drData[i][12].ToString());
                                sss = sss.Replace("[#13]", descs[j].ToString());
                                htmtxt4 += sss;
                            }
                            else
                            {
                                sss = sss.Replace("[#0]",  " ").Replace("[#1]", " ");
                                sss = sss.Replace("[#2]", " ");
                                sss = sss.Replace("[#3]", " ");
                                sss = sss.Replace("[#4]", " ");
                                sss = sss.Replace("[#5]", " ");
                                sss = sss.Replace("[#6]", " ");
                                sss = sss.Replace("[#7]", " ");
                                sss = sss.Replace("[#8]", " ");
                                sss = sss.Replace("[#9]", " ");
                                sss = sss.Replace("[#10]", " ");
                                sss = sss.Replace("[#11]", " ");
                                sss = sss.Replace("[#12]", " ");
                                sss = sss.Replace("[#13]", descs[j].ToString());
                                htmtxt4 += sss;
                            }
                        }
                            
                    }
                }
                else
                {
                    htmtxt4 = htmtxt[3].ToString();
                }
                string htmtxt5 = htmtxt[4].ToString();

                string txtValue = htmtxt1 + htmtxt2 + htmtxt3 + htmtxt4 + htmtxt5;
                FormPrint fmp = new FormPrint(txtValue);
                fmp.ShowDialog();



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
            
        }
        

        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {

            
            // 清除病人信息
            lblName.Text    = string.Empty;
            lblGender.Text  = string.Empty;
            lblAge.Text     = string.Empty;
            lblDeptName.Text = string.Empty;
            lblDocNo.Text   = string.Empty;
            lblDiagnose.Text = string.Empty;

            // 清除行记录
        }


        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        private DataSet PatientData(string patientId, string visitId)
        {
            // 获取病情记录
            string sql = string.Empty;

            sql += "SELECT NURSING_DATE as RDATE,NURSING_DATE as STIME,NURSING_TMP,NURSING_PULSE,NURSING_BREATH,NURSING_SATURATION,NURSING_BP,DRUG_NAME,DRUG_AMOUNT,FOOD_NAME,FOOD_AMOUNT,DRAINAGE_NAME,DRAINAGE_AMOUNT,NURSING_STATE_ILL,NURSING_DATE";
            sql += " FROM ";
            sql += "NURSING_RECORD ";
            sql += "WHERE ";
            sql += "PATIENT_ID = " + SQL.SqlConvert(patientId);
            sql += "AND VISIT_ID = " + SQL.SqlConvert(visitId);
            sql += "ORDER BY ";
            sql += " NURSING_DATE ASC";

            DataSet ds = GVars.OracleAccess.SelectData(sql);
            return ds;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="visitId"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        private DataSet PatientData(string patientId, string visitId, DateTime dtStart, DateTime dtEnd)
        {
            // 获取病情记录
            string sql = string.Empty;

            sql += "SELECT NURSING_DATE as RDATE,NURSING_DATE as STIME,NURSING_TMP,NURSING_PULSE,NURSING_BREATH,NURSING_SATURATION,NURSING_BP,DRUG_NAME,DRUG_AMOUNT,FOOD_NAME,FOOD_AMOUNT,DRAINAGE_NAME,DRAINAGE_AMOUNT,NURSING_STATE_ILL,NURSING_DATE";
            sql += "  FROM ";
            sql += "NURSING_RECORD ";
            sql += "WHERE ";
            sql += "PATIENT_ID = " + SQL.SqlConvert(patientId);
            sql += "AND VISIT_ID = " + SQL.SqlConvert(visitId);

            sql += "AND NURSING_DATE >= " + SQL.GetOraDbDate(dtStart.ToString());
            sql += "AND NURSING_DATE < " + SQL.GetOraDbDate(dtEnd.ToString());

            sql += "ORDER BY ";
            sql += " NURSING_DATE ASC";

            DataSet ds = GVars.OracleAccess.SelectData(sql);
            return ds;
        }


        /// <summary>
        /// 显示病人的基本信息
        /// </summary>
        private void showPatientInfo()
        {
            patientId = string.Empty;
            visitId = string.Empty;

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

            this.lblName.Text = dr["NAME"].ToString();            // 病人姓名
            this.lblGender.Text = dr["SEX"].ToString();             // 病人性别
            this.lblAge.Text = age;                              // 年龄
            this.lblDeptName.Text = dr["DEPT_NAME"].ToString();       // 科别
            this.lblDocNo.Text = dr["INP_NO"].ToString();          // 住院号
            this.lblDiagnose.Text = dr["DIAGNOSIS"].ToString();       // 诊断

            patientId = dr["PATIENT_ID"].ToString();
            visitId = dr["VISIT_ID"].ToString();
        }
        
        
        /// <summary>
        /// 显示护理记录
        /// </summary>
        /// <param name="timePoint"></param>
        /// <param name="rowStart"></param>
        private void showNursingRec(string timePoint)
        {
 
        }
        
        
        /// <summary>
        /// 显示护理数据
        /// </summary>
        private void showNursingRec(DataRow[] drArray, int rowStart)
        {
 
        }
        
        
 
        #endregion

        private void PatientGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            obj = e.FormattedValue;
        }

        private void PatientGridView_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            //if (PatientGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
            //{
            //    MessageBox.Show(e.RowIndex.ToString());
            //}
        }

        private void PatientGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PatientGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (txtBedLabel.Text.Trim().Length == 0)
            {
                return;
            }
            PatientGridView.Rows[e.RowIndex].ErrorText = "";
            DateTime tm = DateTime.Now;
            if (PatientGridView.Rows[e.RowIndex].Cells["Column1"].Value.ToString().Length < 1 && obj != null)
            {
                string sql = "insert into NURSING_RECORD (DEPT_CODE,PATIENT_ID,VISIT_ID,NURSING_DATE,";
                sql = sql + PatientGridView.Columns[e.ColumnIndex].DataPropertyName + ",";
                sql = sql + "DB_USER,USER_NAME)";
                sql = sql + "values( ";
                sql = sql + "'" + GVars.User.DeptCode + "'";
                sql = sql + ",'" + patientId + "'";
                sql = sql + ",'" + visitId + "'";
                sql = sql + "," + SQL.GetOraDbDate(tm);
                sql = sql + ",'" + PatientGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value + "'";
                sql = sql + ",'" + GVars.User.ID + "'";
                sql = sql + ",'" + GVars.User.Name + "'";
                sql = sql + ")";
                GVars.OracleAccess.ExecuteNoQuery(sql);
                PatientGridView.Rows[e.RowIndex].Cells["Column15"].Value = tm;
                PatientGridView.Rows[e.RowIndex].Cells["Column1"].Value = tm;
                PatientGridView.Rows[e.RowIndex].Cells["Column2"].Value = tm;
            }
            else
            {
                string sql = "UPDATE NURSING_RECORD ";
                sql += "SET ";
                sql += PatientGridView.Columns[e.ColumnIndex].DataPropertyName + " = '" + PatientGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value + "'";
                sql += "WHERE ";
                sql += "PATIENT_ID = '" + patientId + "'";
                sql += "AND VISIT_ID = '" + visitId + "'";
                sql += "AND NURSING_DATE = " + SQL.GetOraDbDate(PatientGridView.Rows[e.RowIndex].Cells["Column15"].Value.ToString());
                GVars.OracleAccess.ExecuteNoQuery(sql);
            }
            obj = null;
        }

        private void PatientGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DateTime dt3;
            string st1 = "07:00";
            string st2 = "18:59";
            DateTime dt1 = Convert.ToDateTime(st1);
            DateTime dt2 = Convert.ToDateTime(st2); 

            foreach (DataGridViewRow dgv in PatientGridView.Rows)
            {
                if (dgv.Cells[14].Value != null)
                {
                    try
                    {
                        dt3 = DateTime.Parse(dgv.Cells["Column15"].Value.ToString());
                    }
                    catch
                    {
                        dt3 = DateTime.Now;
                    }
                    //取特定列的值
                    if (dt3.Hour > 7 && dt3.Hour < 19)
                    {
                        dgv.DefaultCellStyle.ForeColor = Color.Blue;
                    }
                    else
                    {
                        dgv.DefaultCellStyle.ForeColor = Color.Red;
                    }
                }
                if (dgv.Cells["Column7"].Value != null)
                {
                    try
                    {
                        if (dgv.Cells["Column7"].Value.ToString() == "总结")
                        {
                            dgv.DefaultCellStyle.ForeColor = Color.Blue;
                            dgv.DefaultCellStyle.BackColor = Color.LightCoral;
                            //dgv.DefaultCellStyle.                 
                        }
                    }
                    catch { }
                }

                


            }

        }

        private void PatientGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (PatientGridView.Columns[e.ColumnIndex] == Column16 && txtBedLabel.Text!="")   // Column 是你的 ButtonColumn 名稱
            {
                FormTextEditor frmShow = new FormTextEditor("");
                //frmShow.Desc = desc.Replace(ComConst.STR.TAB, string.Empty);
                frmShow.ShowDialog();
                PatientGridView.Rows[e.RowIndex].Cells["Column14"].Value += frmShow.Desc;
                DateTime tm = DateTime.Now;
                if (PatientGridView.Rows[e.RowIndex].Cells["Column1"].Value.ToString().Length < 1 && obj != null)
                {
                    string sql = "insert into NURSING_RECORD (DEPT_CODE,PATIENT_ID,VISIT_ID,NURSING_DATE,";
                    sql = sql + PatientGridView.Columns["Column14"].DataPropertyName + ",";
                    sql = sql + "DB_USER,USER_NAME)";
                    sql = sql + "values( ";
                    sql = sql + "'" + GVars.User.DeptCode + "'";
                    sql = sql + ",'" + patientId + "'";
                    sql = sql + ",'" + visitId + "'";
                    sql = sql + "," + SQL.GetOraDbDate(tm);
                    sql = sql + ",'" + PatientGridView.Rows[e.RowIndex].Cells["Column14"].Value + "'";
                    sql = sql + ",'" + GVars.User.ID + "'";
                    sql = sql + ",'" + GVars.User.Name + "'";
                    sql = sql + ")";
                    GVars.OracleAccess.ExecuteNoQuery(sql);
                    PatientGridView.Rows[e.RowIndex].Cells["Column15"].Value = tm;
                    PatientGridView.Rows[e.RowIndex].Cells["Column1"].Value = tm;
                    PatientGridView.Rows[e.RowIndex].Cells["Column2"].Value = tm;
                }
                else
                {
                    string sql = "UPDATE NURSING_RECORD ";
                    sql += "SET ";
                    sql += PatientGridView.Columns["Column14"].DataPropertyName + " = '" + PatientGridView.Rows[e.RowIndex].Cells["Column14"].Value + "'";
                    sql += "WHERE ";
                    sql += "PATIENT_ID = '" + patientId + "'";
                    sql += "AND VISIT_ID = '" + visitId + "'";
                    sql += "AND NURSING_DATE = " + SQL.GetOraDbDate(PatientGridView.Rows[e.RowIndex].Cells["Column15"].Value.ToString());
                    GVars.OracleAccess.ExecuteNoQuery(sql);
                }
                obj = null;




            }
        }

        private void txtBedLabel_TextChanged(object sender, EventArgs e)
        {

        }






    }
}