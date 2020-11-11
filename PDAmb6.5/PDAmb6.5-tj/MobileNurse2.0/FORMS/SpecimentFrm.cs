using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class SpecimentFrm : Form
    {
        #region 变量
        private const string     STR_GATHER         = "采集";
        private const string     STR_CANCEL         = "撤销";
        
        private const int        COL_TEST_NO        = 1;
        private const int        COL_NURSE          = 2;
        private const int        COL_TIME           = 3;
        
        private PatientNavigator patNavigator       = new PatientNavigator();   // 病人导航
        private DataSet          dsSpeciment        = null;                     // 标本信息

        private string           waveFile           = string.Empty;             // 声音文件
        private Symbol.Audio.Controller audioCtrl   = null;                     // 声音控件
        NurseDbI nurseDbi = new NurseDbI();
        #endregion


        public SpecimentFrm()
        {
            InitializeComponent();
        }
        
        
        #region 窗体事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrdersExecuteFrm_Load(object sender, EventArgs e)
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
                
                // 定位病人
                patNavigator.SetPatientButtons();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 执行单选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwSpeciment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                setButtons();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 根据过滤条件显示医嘱
        /// </summary>
        private void showSpecimentList(object sender, System.EventArgs e)
        {
            showSpecimentList();
        }
        
        
        /// <summary>
        /// 按钮[撤销]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                string testNo = lvwSpeciment.Items[lvwSpeciment.SelectedIndices[0]].SubItems[COL_TEST_NO].Text;
                
                if (btnCancel.Text.Equals(STR_CANCEL) == true)
                {
                    // 确认撤销
                    if (GVars.Msg.Show("Q00004", "撤销该标本采集") != DialogResult.Yes)     // "Q00004", "您确认{0}吗?"
                    {
                        return;
                    }

                    Cursor.Current = Cursors.WaitCursor;
                    cancelGather(testNo);
                }
                else
                {
                    Cursor.Current = Cursors.WaitCursor;
                    gatherSpeciment(testNo);
                }
                
                setButtons();
                
                Cursor.Current = Cursors.Default;
            }
            catch(Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 菜单 [返回]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCancel_Click(object sender, EventArgs e)
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
        /// 时钟事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                GVars.App.UserInput = false;
                timer1.Enabled = false;
                
                initFrmVal();
                initDisp();
                
                GVars.App.UserInput = true;
                showSpecimentList(new object(), new System.EventArgs());

                // 初始化声音控件
                if (audioCtrl == null)
                {
                    Symbol.Audio.Device device = (Symbol.Audio.Device)Symbol.StandardForms.SelectDevice.Select(
                        Symbol.Audio.Controller.Title, Symbol.Audio.Device.AvailableDevices);

                    if (device != null)
                    {
                        audioCtrl = new Symbol.Audio.StandardAudio(device);
                    }
                }

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
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
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
        /// 声音播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (panelMsg.Visible == false || audioCtrl == null)
                {
                    return;
                }

                if (System.IO.File.Exists(waveFile) == true)
                {
                    audioCtrl.PlayAudio(1500, 2670, waveFile);
                }
            }
            catch
            {
            }
        }


        /// <summary>
        /// 提示框单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelMsg_Click(object sender, EventArgs e)
        {
            try
            {
                panelMsg.Visible = false;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpecimentFrm_Closed(object sender, EventArgs e)
        {
            try
            {
                if (audioCtrl != null)
                {
                    audioCtrl.Dispose();
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
        /// 初始化医嘱数据
        /// </summary>
        private void initFrmVal()
        {
            GVars.HISDataSrv.Url = UrlIp.ChangeIpInUrl(GVars.HISDataSrv.Url, GVars.App.ServerIp);
            
            if (GVars.Patient.ID != null && GVars.Patient.ID.Length > 0)
            {
                dsSpeciment = GVars.HISDataSrv.GetSpeciment(GVars.Patient.ID, GVars.Patient.VisitId);
                dsSpeciment.AcceptChanges();
            }

            // 声音文件
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
            waveFile = System.IO.Path.Combine(appPath, "Mismatch.wav");
        }
        
        
        /// <summary>
        /// 初始化医嘱
        /// </summary>
        private void initDisp()
        {   
            // 定位病人
            patNavigator.SetPatientButtons();
        }
        
        
        /// <summary>
        /// 根据过滤条件显示医嘱
        /// </summary>
        private void showSpecimentList()
        {
            // 预处理
            if (GVars.App.UserInput == false)
            {
                return;
            }
            
            // 显示医嘱执行单
            bool blnStore = GVars.App.UserInput;
            
            try 
            {            
                Cursor.Current = Cursors.WaitCursor;
                GVars.App.UserInput = false;
                
                this.lvwSpeciment.BeginUpdate();
                
                //清空原来的列表
                this.lvwSpeciment.Items.Clear();

                if (dsSpeciment == null || dsSpeciment.Tables.Count == 0)
                {
                    return;
                }
                
                DataRow[] drFind = dsSpeciment.Tables[0].Select(getFilter(), "TEST_NO");
                for(int i = 0; i < drFind.Length; i++)
                {
                    ListViewItem item = new ListViewItem(drFind[i]["SPECIMEN"].ToString());

                    item.SubItems.Add(drFind[i]["TEST_NO"].ToString()); //2015-11-16

                    item.SubItems.Add(drFind[i]["GATHER_NURSE"].ToString());
                    item.SubItems.Add(drFind[i]["GATHER_TIME"].ToString());
                    //item.SubItems.Add(drFind[i]["SPCM_RECEIVED_OPERATOR"].ToString());
                    //item.SubItems.Add(drFind[i]["SPCM_RECEIVED_DATE_TIME"].ToString());
                    
                    item.Tag = drFind[i]["CONSIGN_SHIPPER"].ToString();
                    //item.Tag = drFind[i]["RESULT_STATUS"].ToString();
                    
                    lvwSpeciment.Items.Add(item);
                }                
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                GVars.App.UserInput = blnStore;
                this.lvwSpeciment.EndUpdate();
            }
        }

        
        /// <summary>
        /// 重新加载病人医嘱
        /// </summary>
        private void patientChanged()
        {
            Cursor.Current = Cursors.WaitCursor;
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                GVars.App.UserInput = false;
                
                if (GVars.Patient.ID != null && GVars.Patient.ID.Length > 0)
                {
                    dsSpeciment = GVars.HISDataSrv.GetSpeciment(GVars.Patient.ID, GVars.Patient.VisitId);
                    dsSpeciment.AcceptChanges();
                }
                
                initDisp();
                
                GVars.App.UserInput = true;
                showSpecimentList(new object(), new System.EventArgs());
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
                Cursor.Current = Cursors.Default;
            }
        }      
        
        
        /// <summary>
        /// 设置按钮
        /// </summary>
        private void setButtons()
        {
            btnCancel.Enabled   = false;
            
            // 如果没有选中
            if (lvwSpeciment.SelectedIndices.Count == 0)
            {
                return;
            }
            
            // 获取当前标本采集人姓名
            ListViewItem item = lvwSpeciment.Items[lvwSpeciment.SelectedIndices[0]];
            string gatherNurse = item.SubItems[COL_NURSE].Text;
            
            // 如果已送出
            if (item.Tag != null && item.Tag.ToString().Length > 0 && Convert.ToInt32(item.Tag.ToString())>1)
            {
                btnCancel.Enabled = false;
                return;
            }
            
            // 如果未采集
            if (gatherNurse.Length == 0)
            {
                btnCancel.Text = STR_GATHER;
                btnCancel.Enabled = true;
            }
            // 如果已采集
            else
            {
                btnCancel.Text  = STR_CANCEL;
                btnCancel.Enabled = gatherNurse.Equals(GVars.User.Name);
            }
        }
        
        
        /// <summary>
        /// 查询过滤条件
        /// </summary>
        /// <returns></returns>
        private string getFilter()
        {
            if (chkWait.Checked && chkGathered.Checked)
            {
                return string.Empty;
            }
            
            if (chkWait.Checked == false && chkGathered.Checked == false)
            {
                return "(1 = 2) ";
            }
            
            if (chkWait.Checked == true)
            {
                //return "GATHER_TIME IS NULL ";
                return "SPCM_RECEIVED_DATE_TIME IS NULL ";
            }
            
            if (chkGathered.Checked == true)
            {
                //return "GATHER_TIME IS NOT NULL ";
                return "SPCM_RECEIVED_DATE_TIME IS NOT NULL ";
            }
            
            return string.Empty;         
        }
        
        
        /// <summary>
        /// 保存结果
        /// </summary>
        /// <returns></returns>
        private bool gatherSpeciment(string testNo)
        {
            DateTime dtNow = GVars.sqlceLocal.GetSysDate();
            
            // 界面更新
            ListViewItem item = lvwSpeciment.Items[lvwSpeciment.SelectedIndices[0]];
            if (item.SubItems[COL_TIME].Text.Length == 0)
            {
                item.SubItems[COL_TIME].Text    = dtNow.ToString(ComConst.FMT_DATE.LONG_MINUTE);
                item.SubItems[COL_NURSE].Text   = GVars.User.Name;
            }
            else
            {
                return false;
            }
            
            // 数据保存
            string filter = "TEST_NO = " + SqlManager.SqlConvert(testNo);

            

            DataRow[] drFind = dsSpeciment.Tables[0].Select(filter);
            DataRow drEdit = drFind[0];


            DataSet dsMobileSpeciment = dsSpeciment;

            drEdit["TEST_NO"] = testNo;
            drEdit["SPECIMEN_ID"] = "1";
            drEdit["GATHER_NURSE"]  = GVars.User.Name;
            drEdit["GATHER_TIME"]   = dtNow;
            drEdit["SAMPLE_OPERATOR"] = GVars.User.Name; //2015-11-16
            //drEdit["SPCM_RECEIVED_NO"] = GVars.User.ID;
            drEdit["SPCM_RECEIVED_DATE_TIME"] = dtNow;
            drEdit["SPCM_SAMPLE_DATE_TIME"] = dtNow;

             

            drEdit.AcceptChanges();
            drEdit.SetAdded();
            
            // 保存到Db中
            GVars.HISDataSrv.SaveSpeciment(dsSpeciment.GetChanges());
            dsSpeciment.AcceptChanges();


            #region Save Mobile

            //  2016-05-09

            if (!dsMobileSpeciment.Tables[0].Columns.Contains("WARD_CODE"))
            {
                DataColumn dcWardCode = new DataColumn("WARD_CODE", typeof(string));
                dsMobileSpeciment.Tables[0].Columns.Add(dcWardCode);
            }

            DataRow[] drMobileFind = dsMobileSpeciment.Tables[0].Select(filter);
            DataRow drMobileEdit = drMobileFind[0];

            drMobileEdit["TEST_NO"] = testNo;
            drMobileEdit["SPECIMEN_ID"] = "1";
            drMobileEdit["GATHER_NURSE"] = GVars.User.Name;
            drMobileEdit["GATHER_TIME"] = dtNow;
            //drEdit["SPCM_RECEIVED_OPERATOR"] = GVars.User.Name; 2015-11-16
            //drEdit["SPCM_RECEIVED_NO"] = GVars.User.ID;
            

            drMobileEdit["WARD_CODE"] = GVars.User.DeptCode; ;//2016-05-12 add

            drMobileEdit.AcceptChanges();
            drMobileEdit.SetAdded();

            // 保存到Db中
            nurseDbi.SaveSpeciment(dsMobileSpeciment);
            dsMobileSpeciment.AcceptChanges();

            #endregion

            return true;
        }
        
        
        /// <summary>
        /// 撤销采集
        /// </summary>
        /// <returns></returns>
        private bool cancelGather(string testNo)
        {
            // 界面更新
            ListViewItem item = lvwSpeciment.Items[lvwSpeciment.SelectedIndices[0]];
            item.SubItems[COL_TIME].Text    = string.Empty;
            item.SubItems[COL_NURSE].Text   = string.Empty;

            // 数据保存
            string filter = "TEST_NO = " + SqlManager.SqlConvert(testNo);

            DataSet dsMobileSpeciment = dsSpeciment;

            DataRow[] drFind = dsSpeciment.Tables[0].Select(filter);
            DataRow drEdit = drFind[0];

            drEdit["GATHER_NURSE"] = string.Empty;
            drEdit["GATHER_TIME"] = DBNull.Value;
            //drEdit["SPCM_RECEIVED_OPERATOR"] = string.Empty; ;  2015-11-16
            //drEdit["SPCM_RECEIVED_DATE_TIME"] = DBNull.Value;
            //drEdit["SPCM_RECEIVED_NO"] = string.Empty;
            // 保存到Db中
            GVars.HISDataSrv.SaveSpeciment(dsSpeciment.GetChanges());
            dsSpeciment.AcceptChanges();

            #region 撤销 Mobile
            DataRow[] drMobileFind = dsMobileSpeciment.Tables[0].Select(filter);
            DataRow drMobileEdit = drMobileFind[0];

            drMobileEdit["GATHER_NURSE"] = string.Empty;
            drMobileEdit["GATHER_TIME"] = DBNull.Value;
            // 保存到Db中
            nurseDbi.SaveSpeciment(dsMobileSpeciment.GetChanges());
            //GVars.HISDataSrv.SaveSpeciment(dsSpeciment.GetChanges());
            dsMobileSpeciment.AcceptChanges();
            #endregion

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
                int    type    = getBarcodeType(barcode);
                
                // 如果不包含空格, 表示是病人的腕带
                if (type == 1)
                {
                    if (patNavigator.ScanedPatient(barcode) == false) GVars.Msg.Show("W00005");   // 该病人不存在!
                    
                    return;
                }                
                // 如果扫描的是标本标签
                else if (type == 2)
                {
                    if (scanObject(barcode) == false)
                    {
                        panelMsg.Visible = (chkMatch(barcode) == false);
                        return;
                    }
                    
                    // 进行保存
                    gatherSpeciment(barcode);
                    setButtons();            
                }
                else
                {
                    panelMsg.Visible = true;
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
        
        
        /// <summary>
        /// 扫描输液卡
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private bool scanObject(string barcode)
        {
            // 在表格中定位
            for(int i = 0; i < lvwSpeciment.Items.Count; i++)
            {
                if (lvwSpeciment.Items[i].SubItems[COL_TEST_NO].Text.Equals(barcode) == true)
                {
                    lvwSpeciment.EnsureVisible(i);
                    lvwSpeciment.Items[i].Selected = true;
                    return true;
                }
            }
            
            return false;
        }
        
        
        /// <summary>
        /// 检查是否匹配
        /// </summary>
        /// <returns></returns>
        private bool chkMatch(string barcode)
        {
            if (dsSpeciment == null || dsSpeciment.Tables.Count == 0)
            {
                return false;
            }

            string filter = "TEST_NO = " + SqlManager.SqlConvert(barcode);//TEST_NO_SRC  2015-11-16
            
            DataRow[] drFind = dsSpeciment.Tables[0].Select(filter);
            return (drFind.Length > 0);
        }
        
        
        /// <summary>
        /// 获取条码类型
        /// </summary>
        /// <returns>1: 腕带 2:标本 0: 没找到</returns>
        private int getBarcodeType(string barcode)
        {
            string filter = string.Empty;
            
            // 判断是否是腕带
            if (GVars.DsPatient != null && GVars.DsPatient.Tables.Count > 0)
            {
                filter = "PATIENT_ID = " + SqlManager.SqlConvert(barcode);
                DataRow[] drFind = GVars.DsPatient.Tables[0].Select(filter);
                if (drFind.Length > 0) return 1;
            }
            
            // 判断是否是标签
            if (dsSpeciment != null && dsSpeciment.Tables.Count > 0)
            {
                filter = "TEST_NO = " + SqlManager.SqlConvert(barcode);  
                 
                DataRow[] drFind = dsSpeciment.Tables[0].Select(filter);
                if (drFind.Length > 0) return 2;
            }
            
            return 0;
        }
        #endregion
    }
}