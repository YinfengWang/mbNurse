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
    public partial class NursingRec : Form
    {
        #region 窗体变量
        private const string RIGHT_ID = "020204";                       // 本界面需要的权限

        private PatientDbI  patientCom;                                 // 病人
        private NursingDbI  nursingCom;
        
        private DataSet dsPatient   = null;                             // 病人信息
        private DataSet dsNursing   = null;                             // 护理信息
        private DataSet dsNursingItemDict = null;                       // 护理项目字典
        private DataRow[] drNursingItem   = null;
        
        private string patientId    = string.Empty;                     // 病人ID号
        private string visitId      = string.Empty;                     // 本次就诊序号
        
        private bool    blnRights   = false;                            // 是否具有修改权限
        private bool    blnSuperUser= false;                            // 超级用户
        #endregion


        public NursingRec()
        {
            InitializeComponent();

            this.KeyPress += new KeyPressEventHandler( NursingRec_KeyPress );

            this.txtBedLabel.GotFocus += new EventHandler(imeCtrl_GotFocus);
            this.txtBedLabel.KeyDown += new KeyEventHandler( txtBedLabel_KeyDown );

            this.txtValue.GotFocus += new EventHandler(imeCtrl_GotFocus);
            this.cmbClass.GotFocus += new EventHandler(imeCtrl_GotFocus);
            this.cmbItem.GotFocus += new EventHandler(imeCtrl_GotFocus);
        }

        
        #region 窗体事件
        private void NursingRec_Load( object sender, EventArgs e )
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
        /// 窗体的KeyPress事件
        /// </summary>
        /// <remarks>
        /// 主要处理回车变成Tab
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void NursingRec_KeyPress( object sender, KeyPressEventArgs e )
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

                // 查询病人护理记录
                btnQuery_Click(sender, e);
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
                btnQuery.Enabled = false;
                
                // 获取护理记录
                dsNursing = nursingCom.GetNursingData(patientId, visitId, dtRngStart.Value, dtRngEnd.Value, cmbItem_Filter.Text);
                
                // 显示护理记录
                showNursingData();
            }
            catch(Exception ex)
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
        /// 护理记录列表选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwNursingRec_SelectedIndexChanged( object sender, EventArgs e )
        {
            try
            {
                if (lvwNursingRec.SelectedIndices.Count == 0)
                {
                    return;
                }
                
                // 获取当前记录
                DataRow dr = dsNursing.Tables[0].Rows[lvwNursingRec.SelectedIndices[0]];
                
                // 显示当前记录
                DateTime dtRec = (DateTime)dr["TIME_POINT"];
                dtPickerDate.Value = dtRec;                             // 日期
                dtPickerTime.Value = dtRec;                             // 时间
                
                cmbClass.SelectedIndex = nursingCom.GetNursingItemAttribute(dr["VITAL_CODE"].ToString());
                
                cmbItem.Text = dr["VITAL_SIGNS"].ToString();            // 项目名称
                txtValue.Text = dr["VITAL_SIGNS_CVALUES"].ToString();   // 值
                lblUnit.Text = "(" + dr["UNITS"].ToString() + ")";      // 单位
                
                lblUnit.Visible = (lblUnit.Text.Length > 2);
                
                blnRights = (blnSuperUser || GVars.User.Name.Equals(dr["NURSE"].ToString()));
                                
                // 根据权限设置界面状态
                this.btnSave.Enabled = blnRights;
                this.btnDelete.Enabled = blnRights;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 护理类别改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbClass_SelectedIndexChanged( object sender, EventArgs e )
        {
            try
            {                
                cmbItem.Items.Clear();
                
                drNursingItem = dsNursingItemDict.Tables[0].Select("ATTRIBUTE = " + SqlConvert.SqlConvert(cmbClass.SelectedIndex.ToString()));
                
                for(int i = 0; i < drNursingItem.Length; i++)
                {
                    DataRow dr = drNursingItem[i];
                    
                    cmbItem.Items.Add(dr["VITAL_SIGNS"].ToString());                
                }
                                
                cmbItem.Text = string.Empty;
                
                txtValue.Enabled = true;
                if (cmbClass.SelectedIndex == cmbClass.Items.Count - 1)
                {
                    txtValue.Enabled = false;
                    txtValue.Text   = string.Empty;
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
                
        private void cmbClass_Filter_SelectedIndexChanged( object sender, EventArgs e )
        {
            try
            {                
                cmbItem_Filter.Items.Clear();
                
                if (cmbClass_Filter.SelectedIndex < 1)
                {
                    return;
                }
                
                string attribute = (cmbClass_Filter.SelectedIndex - 1).ToString();
                drNursingItem = dsNursingItemDict.Tables[0].Select("ATTRIBUTE = " + SqlConvert.SqlConvert(attribute));
                                                
                for(int i = 0; i < drNursingItem.Length; i++)
                {
                    DataRow dr = drNursingItem[i];
                    
                    cmbItem_Filter.Items.Add(dr["VITAL_SIGNS"].ToString());                
                }
                
                cmbItem_Filter.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        
        /// <summary>
        /// 护理项目改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbItem_SelectedIndexChanged( object sender, EventArgs e )
        {
            try
            {
                if (cmbItem.SelectedIndex > -1)
                {
                    string unit = drNursingItem[cmbItem.SelectedIndex]["UNIT"].ToString();
                                                        
                    lblUnit.Text = "(" + unit + ")";
                    lblUnit.Visible = (unit.Length > 0);
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[新增]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click( object sender, EventArgs e )
        {
            try
            {
                this.cmbItem.SelectedIndex = -1;
                this.txtValue.Text = string.Empty;
                this.lblUnit.Visible = false;
                
                // 根据权限设置界面状态
                blnRights = true;
                
                this.btnSave.Enabled = blnRights;
                this.btnDelete.Enabled = blnRights;
                
                this.dtPickerTime.Focus();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮[删除]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click( object sender, EventArgs e )
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnDelete.Enabled = false;
                
                // 检查输入
                if (chkDisp() == false)
                {
                    GVars.Msg.Show();
                    return;
                }
                
                // 确认删除
                if (GVars.Msg.Show("Q00019") != DialogResult.Yes)
                {
                    return;
                }
                   
                // 保存数据
                deleteRec();
                
                // 重新显示数据
                btnQuery_Click(sender, e);
                
                // 清除界面
                this.cmbItem.SelectedIndex = -1;
                this.txtValue.Text = string.Empty;
                this.lblUnit.Visible = false;
            }            
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                btnDelete.Enabled = true;
                this.Cursor = Cursors.Default;
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
                
                // 检查输入
                if (chkDisp() == false)
                {
                    GVars.Msg.Show();
                    return;
                }
                
                // 保存数据
                saveData();
                
                // 重新显示数据
                btnQuery_Click(sender, e);
                
                // 提示
                // GVars.Msg.Show("I00004", "保存");   // {0}成功!
                this.btnSave.Enabled = false;

                // 清除界面
                this.cmbItem.SelectedIndex = -1;
                this.txtValue.Text = string.Empty;
                this.lblUnit.Visible = false;
                
                this.dtPickerTime.Focus();
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
            nursingCom = new NursingDbI(GVars.OracleAccess, GVars.SqlserverAccess);
            
            dsNursingItemDict = nursingCom.GetNursingItemDict_Sql(GVars.User.DeptCode, -1); // 护理项目字典

            blnSuperUser = GVars.UserDbI.ChkSuperUser(GVars.User.ID, RIGHT_ID);
        }
        
        
        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {
            // 清空病人信息
            this.lblPatientID.Text = string.Empty;                      // 病人标识号
            this.lblVisitID.Text = string.Empty;                        // 本次住院标识
            this.lblPatientName.Text = string.Empty;                    // 病人姓名
            this.lblGender.Text = string.Empty;                         // 病人性别
            this.lblDeptName.Text = string.Empty;                       // 科别
            this.lblInpNo.Text = string.Empty;                          // 住院号
            this.lblInpDate.Text = string.Empty;                        // 入院日期
            
            // 清空项目列表
            lvwNursingRec.Items.Clear();
            
            // 清空项目属性
            this.cmbItem.SelectedIndex = -1;
            this.txtValue.Text = string.Empty;
            this.lblUnit.Visible = false;
        }
        
        
        /// <summary>
        /// 显示病人的基本信息
        /// </summary>
        private void showPatientInfo()
        { 
            // 清空界面
            this.txtBedLabel.Text = string.Empty;                       // 病人床标号
            this.lblPatientID.Text = string.Empty;                      // 病人标识号
            this.lblVisitID.Text = string.Empty;                        // 本次住院标识
            this.lblPatientName.Text = string.Empty;                    // 病人姓名
            this.lblGender.Text = string.Empty;                         // 病人性别
            this.lblDeptName.Text = string.Empty;                       // 科别
            this.lblInpNo.Text = string.Empty;                          // 住院号
            this.lblInpDate.Text = string.Empty;                        // 入院日期
            
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
            
            this.txtBedLabel.Text = dr["BED_LABEL"].ToString();                                 // 病人床标号
            this.lblPatientID.Text = dr["PATIENT_ID"].ToString();                               // 病人标识号
            this.lblVisitID.Text = dr["VISIT_ID"].ToString();                                   // 本次住院标识
            this.lblPatientName.Text = dr["NAME"].ToString();                                   // 病人姓名
            this.lblGender.Text = dr["SEX"].ToString();                                         // 病人性别
            this.lblDeptName.Text = dr["DEPT_NAME"].ToString();                                 // 科别
            this.lblInpNo.Text = dr["INP_NO"].ToString();                                       // 住院号
            this.lblInpDate.Text = DataType.GetDateTimeShort(dr["ADMISSION_DATE_TIME"].ToString());   // 入院日期
            
            patientId   = dr["PATIENT_ID"].ToString();
            visitId     = dr["VISIT_ID"].ToString();
        }


        /// <summary>
        /// 显示病人的护理记录
        /// </summary>
        private void showNursingData()
        {
            try
            {
                lvwNursingRec.BeginUpdate();
                lvwNursingRec.Items.Clear();
                
                if (dsNursing == null || dsNursing.Tables.Count == 0 || dsNursing.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                
                ListViewItem    item    = null;
                DateTime        dtRec;
                
                string          recDate0 = string.Empty;
                string          recTime0 = string.Empty;
                string          recDate = string.Empty;
                string          recTime = string.Empty;
                
                foreach(DataRow dr in dsNursing.Tables[0].Rows)
                {
                    dtRec   = (DateTime)dr["TIME_POINT"];
                    
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
                    
                    lvwNursingRec.Items.Add(item);
                }
            }
            finally
            {
                if (lvwNursingRec.Items.Count > 1)
                {
                    lvwNursingRec.TopItem = lvwNursingRec.Items[lvwNursingRec.Items.Count - 1];
                }
                
                lvwNursingRec.EndUpdate();
            }
        }        
        
        
        /// <summary>
        /// 检查界面输入是否完整
        /// </summary>
        /// <returns></returns>
        private bool chkDisp()
        {
            if (patientId.Length == 0)
            {
                GVars.Msg.MsgId = "E00011";     // E00011	请先确定病人!
                GVars.Msg.ErrorSrc = txtBedLabel;

                return false;
            }

            if (this.cmbClass.SelectedIndex == -1)
            {
                GVars.Msg.MsgId = "E00004";     // 必须输入{0}!
                GVars.Msg.MsgContent.Add("类别");
                GVars.Msg.ErrorSrc = cmbClass;
                
                return false;
            }
            
            if (this.cmbItem.SelectedIndex == -1)
            {
                GVars.Msg.MsgId = "E00004";     // 必须输入{0}!
                GVars.Msg.MsgContent.Add("项目");
                GVars.Msg.ErrorSrc = cmbItem;            
                return false;
            }
            
            if (this.txtValue.Text.Trim().Length == 0 && this.txtValue.Enabled == true)
            {
                GVars.Msg.MsgId = "E00004";     // 必须输入{0}!
                GVars.Msg.MsgContent.Add("值");
                GVars.Msg.ErrorSrc = txtValue;
                
                return false;
            }

            // 时间不能大于当前系统时间
            DateTime dt = dtPickerDate.Value.Date;
            DateTime dtTime = dtPickerTime.Value;

            dt = dt.AddHours(dtTime.Hour).AddMinutes(dtTime.Minute).AddSeconds(dtTime.Second);

            if (dt.CompareTo(GVars.OracleAccess.GetSysDate()) > 0)
            {
                GVars.Msg.MsgId = "E00057";     // 日期不能大于当前系统日期!
                GVars.Msg.ErrorSrc = dtPickerDate;

                return false;
            }

            return true;
        }
        
        
        /// <summary>
        /// 保存数据
        /// </summary>
        private void saveData()
        {
            SqlConvert.DbField[] arr = new SqlConvert.DbField[11];
            
            string tableName = "VITAL_SIGNS_REC";
            string recDate = dtPickerDate.Value.ToString(ComConst.FMT_DATE.SHORT);
            string recTime = dtPickerTime.Value.ToString(ComConst.FMT_DATE.TIME);
            DataRow drItem = drNursingItem[cmbItem.SelectedIndex];
                        
            int i = 0;
            arr[i++] = SqlConvert.GetDbField_Ora("PATIENT_ID", patientId, SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("VISIT_ID", visitId, SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("CLASS_CODE", drItem["CLASS_CODE"].ToString(), SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("VITAL_CODE", drItem["VITAL_CODE"].ToString(), SqlManager.FIELD_TYPE.STR);            
            arr[i++] = SqlConvert.GetDbField_Ora("RECORDING_DATE", recDate, SqlManager.FIELD_TYPE.DATE);
            arr[i++] = SqlConvert.GetDbField_Ora("TIME_POINT", recDate + ComConst.STR.BLANK + recTime, SqlManager.FIELD_TYPE.DATE);
            
            arr[i++] = SqlConvert.GetDbField_Ora("VITAL_SIGNS", cmbItem.Text, SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("UNITS", drItem["UNIT"].ToString(), SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("VITAL_SIGNS_CVALUES", txtValue.Text.Trim(), SqlManager.FIELD_TYPE.STR);            
            arr[i++] = SqlConvert.GetDbField_Ora("WARD_CODE", GVars.User.DeptCode, SqlManager.FIELD_TYPE.STR);            
            arr[i++] = SqlConvert.GetDbField_Ora("NURSE", GVars.User.Name, SqlManager.FIELD_TYPE.STR);
            
            // 判断记录是否存在
            string where = SqlConvert.getFieldValuePairAssert(arr, 6);
            string sql = "SELECT PATIENT_ID FROM " + tableName + SqlConvert.getSQLWhere(where);
            
            // 如果记录存在
            if (GVars.OracleAccess.SelectValue(sql) == true)
            {
                sql = SqlConvert.GetSqlUpdate(tableName, arr, 11, where);
            }
            // 如果记录不存在
            else
            {
                sql = SqlConvert.GetSqlInsert(tableName, arr, 11);
            }
            
            GVars.OracleAccess.ExecuteNoQuery(sql);
        }
        
                    
        /// <summary>
        /// 保存数据
        /// </summary>
        private void deleteRec()
        {
            SqlConvert.DbField[] arr = new SqlConvert.DbField[6];
            
            string tableName = "VITAL_SIGNS_REC";
            string recDate = dtPickerDate.Value.ToString(ComConst.FMT_DATE.SHORT);
            string recTime = dtPickerTime.Value.ToString(ComConst.FMT_DATE.TIME);
            DataRow drItem = drNursingItem[cmbItem.SelectedIndex];
                        
            int i = 0;
            arr[i++] = SqlConvert.GetDbField_Ora("PATIENT_ID", patientId, SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("VISIT_ID", visitId, SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("CLASS_CODE", drItem["CLASS_CODE"].ToString(), SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("VITAL_CODE", drItem["VITAL_CODE"].ToString(), SqlManager.FIELD_TYPE.STR);            
            arr[i++] = SqlConvert.GetDbField_Ora("RECORDING_DATE", recDate, SqlManager.FIELD_TYPE.DATE);
            arr[i++] = SqlConvert.GetDbField_Ora("TIME_POINT", recDate + ComConst.STR.BLANK + recTime, SqlManager.FIELD_TYPE.DATE);
            
            // 判断记录是否存在
            string where = SqlConvert.getFieldValuePairAssert(arr, 6);
            string sql = "SELECT PATIENT_ID FROM " + tableName + SqlConvert.getSQLWhere(where);
            
            // 如果记录存在
            if (GVars.OracleAccess.SelectValue(sql) == true)
            {
                sql = "DELETE FROM " + tableName + SqlConvert.getSQLWhere(where);
                GVars.OracleAccess.ExecuteNoQuery(sql);
                
                GVars.Msg.Show("I00004", "删除");     // {0}成功!
            }
            // 如果记录不存在
            else
            {
                GVars.Msg.Show("E00055", "删除");     // {0}失败!
            }
        }
        #endregion
    }
}