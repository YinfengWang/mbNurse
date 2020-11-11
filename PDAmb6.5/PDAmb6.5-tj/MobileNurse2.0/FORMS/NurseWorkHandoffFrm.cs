using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace HISPlus
{
    public partial class NurseWorkHandoffFrm : Form
    {
        #region 变量
        private PatientNavigator patNavigator   = new PatientNavigator();   // 病人导航
        
        private NurseDbI         nurseDbI       = new NurseDbI();
        private DataSet          dsExchangeWork = null;
        
        private DateTime         dtStart        = DataType.DateTime_Null();
        private DateTime         dtCurrent      = DataType.DateTime_Null();
        private DateTime         dtStand        = DataType.DateTime_Null();
        
        private int              workPos        = 2;                        // 0: 早, 1:晚        
        private int              workCount      = 2;                        // 班次数
        private ArrayList        arrHandoffTime = new ArrayList();          // 交接班时间点
        
        private bool             editable       = false;                    // 是否可以编辑
        #endregion
        
        
        public NurseWorkHandoffFrm()
        {
            InitializeComponent();
        }
        
        
        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NurseWorkHandoffFrm_Load(object sender, EventArgs e)
        {
            try
            {
                patNavigator.BtnPrePatient = this.btnPrePatient;
                patNavigator.BtnCurrentPatient = this.btnCurrPatient;
                patNavigator.BtnNextPatient = this.btnNextPatient;
                patNavigator.BtnPatientList = this.btnListPatient;
                
                patNavigator.MenuItemPatient = mnuPatient;
                
                patNavigator.PatientChanged = new DataChanged(patientChanged);
                
                this.timer1.Enabled = true;                
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 时钟消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                
                // 获取参数
                //getParameters();
                
                initFrmVal();
                
                initDisp();
                
                //showExchangeRec();
                
                //setWorkButtons();
                // 菜单控制
                foreach (MenuItem mnu in mnuNavigator.MenuItems)
                {
                    if (mnu.Text.IndexOf(this.Text) >= 0)
                    {
                        mnu.Enabled = false;
                    }
                    else
                    {
                        mnu.Click += new EventHandler(mnuNavigator_Func_Click);
                    }
                }
            }
            catch(Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }


        /// <summary>
        /// 菜单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuNavigator_Func_Click(object sender, EventArgs e)
        {
            try
            {
                MenuItem mnu = sender as MenuItem;
                if (mnu == null) return;

                this.Tag = mnu.Text;

                this.Close();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮 [前一个班次]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreTimePoint_Click(object sender, EventArgs e)
        {
            try
            {
                // 获取班次
                workPos--;
                
                if (workPos < 0)
                {
                    workPos = workCount - 1;
                    dtStand = dtStand.AddDays(-1);
                }
                
                // 显示交班内容
                showExchangeRec();
                
                // 设置按钮
                setWorkButtons();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮 [后一个班次]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextTimePoint_Click(object sender, EventArgs e)
        {
            try
            {
                // 获取班次
                workPos++;
                
                if (workPos >= workCount)
                {
                    workPos = 0;
                    dtStand = dtStand.AddDays(1);
                }
                
                // 显示交班内容
                showExchangeRec();
                
                // 设置按钮
                setWorkButtons();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
              
        
        /// <summary>
        ///  内容改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNurseRecord_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                //btnSave.Enabled = editable;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮 [保存]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveData() == true)
                {
                    setWorkButtons();
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 菜单[返回]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }   
        #endregion
        
        
        #region 共通函数
        /// <summary>
        /// 初始化变量
        /// </summary>
        private void initFrmVal()
        {
            string result = nurseDbI.GetNurseWorkHandoffRec(GVars.Patient.ID, GVars.Patient.VisitId);
            if (result.Contains(ComConst.STR.CRLF) == false)
            {
                result = result.Replace("\n", "\r\n");
            }
            
            txtNurseRecord.Text = result;
        }
        
        
        /// <summary>
        /// 获取参数
        /// </summary>
        private void getParameters()
        {
            // 获取参数
            string val = string.Empty;
            string[] parts = null;
            
            foreach(DataRow dr in GVars.Parameters.Tables[0].Rows)
            {           
                val = dr["PARAMETER_VALUE"].ToString();
                     
                switch(dr["PARAMETER_NAME"].ToString())
                {
                    case "WORK_FLOW_DEFINE":                            // 交接班定义
                        
                        parts = val.Split(",".ToCharArray());
                        if (parts.Length > 1)
                        {
                            workCount   = int.Parse(parts[0].Trim());
                            
                            for(int i = 1; i < parts.Length; i++)
                            {
                                arrHandoffTime.Add(parts[i]);
                            }
                        }
                        
                        break;
                    default:
                        break;
                }
            }
        }


        /// <summary>
        /// 初始化医嘱
        /// </summary>
        private void initDisp()
        {   
            // 充置病人按钮
            patNavigator.SetPatientButtons();
            
            // 时间按钮
            //setWorkButtons();
        }
        
        
        /// <summary>
        /// 病人改变事件
        /// </summary>
        private void patientChanged()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                initFrmVal();
                
                initDisp();
                
                //showExchangeRec();
                
                //setWorkButtons();
            }
            catch(Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        
        
        /// <summary>
        /// 显示交接班内容
        /// </summary>
        private void showExchangeRec()
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                // 如果没有交班内容而且 是当天当前班次, 可以编辑
                editable = ((workPos == getCurrentWorkPos()) && dtStand.Date.Equals(GVars.GetDateNow().Date));;
                
                GVars.App.UserInput = false;
                
                txtNurseRecord.Text = string.Empty;
                
                if (dsExchangeWork == null || dsExchangeWork.Tables.Count == 0)
                {
                    return;
                }
                
                string filter = "RECORD_TIME = " + SqlManager.SqlConvert(dtStand.Date.ToString(ComConst.FMT_DATE.LONG));
                DataRow[] drFind = dsExchangeWork.Tables[0].Select(filter);
                if (drFind.Length == 0)
                {
                    return;
                }
                
                // 如果有交班内容, 只有自已可修改自已的交班记录                
                DataRow dr = drFind[0];
                
                string desc = string.Empty;// "病情: " + dr["INP_DIAGNOSIS_" + (workPos + 1).ToString()].ToString() + ComConst.STR.CRLF;                
                string colDesc = string.Empty;
                string colUser = string.Empty;
                
                if (getColumnName(workPos, ref colDesc, ref colUser) == true)
                {
                    desc += dr[colDesc].ToString();
                    editable = (dr[colUser].ToString().Trim().Length == 0 
                                || dr[colUser].ToString().Equals(GVars.User.Name));
                }
                
                txtNurseRecord.Text = desc;
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }
        
        
        /// <summary>
        /// 设置
        /// </summary>
        private void setWorkButtons()
        {
            //btnPreTimePoint.Enabled = true;
            //btnCurrTimePoint.Enabled = true;
            //btnNextTimePoint.Enabled = true;
            
            //btnCurrTimePoint.Text = dtStand.Day.ToString() + "日 " + getWorkPosName(workPos); 
            
            //// 第一条
            //if (dtStand.Date.CompareTo(dtStart.Date) == 0 && workPos == 0)
            //{
            //    btnPreTimePoint.Enabled = false;    
            //}
            
            //// 最后一条
            //if (dtStand.Date.CompareTo(dtCurrent.Date) == 0 && workPos == getCurrentWorkPos())
            //{
            //    btnNextTimePoint.Enabled = false;
            //}
            
            //txtNurseRecord.ReadOnly = (editable == false);
            //btnSave.Enabled = false;
        }
        
        
        /// <summary>
        /// 获取当前班次
        /// </summary>
        /// <returns></returns>
        private int getCurrentWorkPos()
        {   
            DateTime dtNow = GVars.GetDateNow();
            int workPosTerm = 0;
            for(int i = 0; i < arrHandoffTime.Count; i++)
            {
                if (int.Parse((string)(arrHandoffTime[i])) > dtNow.Hour)
                {
                    break;
                }
                
                workPosTerm++;
            }
            
            return workPosTerm;
        }
        
        
        /// <summary>
        /// 获取班次名称
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private string getWorkPosName(int pos)
        {
            if (pos == 0)
            {
                return "早班";
            }
            
            if (pos == workCount - 1)
            {
                return "晚班";
            }
               
            return "中班";
        }                
        
        
        /// <summary>
        /// 获取字段名称
        /// </summary>
        /// <returns></returns>
        private bool getColumnName(int workPosTemp, ref string colDesc, ref string colUser)
        {
            // 早班
            if (workPosTemp == 0)
            {
                colDesc = "CIRCUMSTANCE_1";
                colUser = "USER_NAME_1";
                return true;
            }
            
            // 晚班
            if (workPosTemp == workCount - 1)
            {
                colDesc = "CIRCUMSTANCE_3";
                colUser = "USER_NAME_3";
                return true;
            }
            
            // 中班
            if (workPosTemp == 1 && workCount > 2)
            {
                colDesc = "CIRCUMSTANCE_2";
                colUser = "USER_NAME_2";
                return true;
            }
            
            return false;
        }
        
        
        /// <summary>
        ///  保存数据
        /// </summary>
        /// <returns></returns>
        private bool saveData()
        {
            string colDesc = string.Empty;
            string colUser = string.Empty;
            if (getColumnName(workPos, ref colDesc, ref colUser) == false)
            {
                return false;
            }
            
            DateTime dtNow = GVars.GetDateNow();
            
            // 查找对应的记录
            string filter = "RECORD_TIME = " + SqlManager.SqlConvert(dtStand.Date.ToString(ComConst.FMT_DATE.LONG));
            DataRow[] drFind = dsExchangeWork.Tables[0].Select(filter);
            
            DataRow drEdit = null;
            if (drFind.Length == 0)
            {
                drEdit = dsExchangeWork.Tables[0].NewRow();
            }
            else
            {
                drEdit = drFind[0];
            }
            
            drEdit["PATIENT_ID"]    = GVars.Patient.ID;
            drEdit["VISIT_ID"]      = GVars.Patient.VisitId;
            drEdit[colDesc]         = txtNurseRecord.Text.Trim(); 
            drEdit["RECORD_TIME"]   = dtStand.Date;
            drEdit[colUser]         = GVars.User.Name;
            drEdit["UPD_DATE_TIME"] = dtNow;
            
            if (drFind.Length == 0)
            {
                dsExchangeWork.Tables[0].Rows.Add(drEdit);
            }
            
            nurseDbI.SaveNurseWorkHandoffRec(ref dsExchangeWork, GVars.Patient.ID, GVars.Patient.VisitId);
            dsExchangeWork.AcceptChanges();
            
            return true;
        }
        #endregion                
        
        
        #region 扫描器
        /// <summary>
        /// 扫描器 读取通知 事件的委托程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ScanReader_ReadNotify(object sender, EventArgs e)
        {
            #if SCANNER
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                // 获取病人ID号
                string barcode = GVars.ScanReaderBuffer.Text.Trim();
                
                // 如果不包含空格, 表示是病人的腕带
                if (barcode.IndexOf( ComConst.STR.BLANK) < 0 && barcode.IndexOf("T") < 0)
                {
                    if (patNavigator.ScanedPatient(barcode) == false) GVars.Msg.Show("W00005");   // 该病人不存在!
                                        
                    return;
                }                
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                GVars.ScanReader.Actions.Read(GVars.ScanReaderBuffer);  // 再次开始等待扫描
            }
            #endif
        }
        #endregion        
    
    }
}