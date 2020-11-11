using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HISPlus.UserControls;
using SqlConvert = HISPlus.SqlManager;
using SQL = HISPlus.SqlManager;

namespace HISPlus
{
    public partial class NursingRec : FormDo, IBasePatient
    {
        #region 窗体变量

        private NursingDbI nursingCom;

        private DataSet dsNursing = null;                     // 护理信息
        private DataRow[] drNursing = null;
        private DataSet dsNursingItemDict = null;                     // 护理项目字典
        private DataRow[] drNursingItem = null;
        private string vitalCodePreInput = string.Empty;             // 可以提前录入的项目代码

        private string patientId = string.Empty;             // 病人ID号
        private string visitId = string.Empty;             // 本次就诊序号
        private bool blnAdd = false;

        private DateTime dtTimePoint = DataType.DateTime_Null();

        private const string RIGHT_EDIT = "2";                      //  编辑别人录入记录的权限

        /// <summary>
        /// 当前正在编辑or新增的行
        /// </summary>
        private DataRow CurrentRow;
        #endregion


        public NursingRec()
        {
            _id = "00015";
            _guid = "FBA89860-BE5C-4c9e-962A-502768A48FEF";

            _right = @"0:查看
                        1:录入
                        2:修改";

            InitializeComponent();

            this.KeyPress += new KeyPressEventHandler(NursingRec_KeyPress);

            this.txtValue.GotFocus += new EventHandler(imeCtrl_GotFocus);
            this.cmbClass.GotFocus += new EventHandler(imeCtrl_GotFocus);
            this.cmbItem.GotFocus += new EventHandler(imeCtrl_GotFocus);
        }


        #region 窗体事件
        private void NursingRec_Load(object sender, EventArgs e)
        {
            try
            {
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


        void IBasePatient.Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
            patientId = e.PatientId;
            visitId = e.VisitId;

            // 获取护理记录
            dsNursing = nursingCom.GetNursingData(patientId, visitId, dtRngStart.Value, dtRngEnd.Value, cmbItem_Filter.Text);

            // 显示护理记录
            showNursingData();
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

                // 获取护理记录
                dsNursing = nursingCom.GetNursingData(patientId, visitId, dtRngStart.Value, dtRngEnd.Value, cmbItem_Filter.Text);

                // 显示护理记录
                showNursingData();
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
        /// 护理类别改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbItem.Items.Clear();

                drNursingItem = dsNursingItemDict.Tables[0].Select(
                                "ATTRIBUTE = " + SqlConvert.SqlConvert(cmbClass.SelectedIndex.ToString()),
                                "SHOW_ORDER");

                for (int i = 0; i < drNursingItem.Length; i++)
                {
                    DataRow dr = drNursingItem[i];

                    cmbItem.Items.Add(dr["VITAL_SIGNS"].ToString());
                }

                //cmbItem.Text1 = string.Empty;

                txtValue.Enabled = true;
                txtValue.Focus();
                //if (cmbClass.SelectedIndex == cmbClass.Items.Count - 1)
                //{
                //    txtValue.Enabled = false;
                //    txtValue.Text1   = string.Empty;
                //}
                if (cmbItem.Items.Count >= 0)
                {
                    cmbItem.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void cmbClass_Filter_SelectedIndexChanged(object sender, EventArgs e)
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

                for (int i = 0; i < drNursingItem.Length; i++)
                {
                    DataRow dr = drNursingItem[i];

                    cmbItem_Filter.Items.Add(dr["VITAL_SIGNS"].ToString());
                }

                cmbItem_Filter.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 护理项目改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbItem.SelectedIndex > -1)
                {
                    string unit = drNursingItem[cmbItem.SelectedIndex]["UNIT"].ToString();

                    lblUnit.Text = "(" + unit + ")";
                    lblUnit.Visible = (unit.Length > 0);

                    this.btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = true;
        }


        /// <summary>
        /// 按钮[新增]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                dtTimePoint = DataType.DateTime_Null();

                blnAdd = true;

                InitControl();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void InitControl()
        {
            initDisp_Edit(true);

            this.cmbItem.SelectedIndex = -1;
            this.txtValue.Text = string.Empty;
            this.lblUnit.Visible = false;
            this.txtMemo.Text = string.Empty;

            // 根据权限设置界面状态
            this.btnSave.Enabled = true;
            this.btnDelete.Enabled = false;

            this.dtPickerTime.Focus();
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
                this.Cursor = Cursors.WaitCursor;
                btnDelete.Enabled = false;

                if (blnAdd)
                {
                    InitControl();
                }
                else
                {
                    if (ucGridView1.SelectRowsCount == 0)
                    {
                        return;
                    }

                    // 确认删除 您确认要删除当前记录吗?
                    if (GVars.Msg.Show("Q0005") != DialogResult.Yes)
                    {
                        return;
                    }

                    //if (CurrentRow != null)
                    //    CurrentRow.Delete();

                    //记录删除的护理记录

                    string patient_id = ucGridView1.SelectedRow["PATIENT_ID"].ToString();
                    string visit_id = ucGridView1.SelectedRow["VISIT_ID"].ToString();
                    string recording_date = Convert.ToDateTime(ucGridView1.SelectedRow["RECORDING_DATE"].ToString()).ToString("yyyy-MM-dd");
                    string time_point = ucGridView1.SelectedRow["TIME_POINT"].ToString();
                    string class_code = ucGridView1.SelectedRow["CLASS_CODE"].ToString();
                    string vital_code = ucGridView1.SelectedRow["VITAL_CODE"].ToString();



                    //ucGridView1.DeleteSelectRow();
                    nursingCom.DelNursingIetm(patient_id, visit_id, recording_date, time_point, class_code, vital_code);
                    //nursingCom.SaveNursingData(ref dsNursing, patientId, visitId);


                    dtTimePoint = DataType.DateTime_Null();

                    // 重新显示数据
                    btnQuery_Click(sender, e);
                }

                // 清除界面
                this.cmbItem.SelectedIndex = -1;
                this.txtValue.Text = string.Empty;
                this.lblUnit.Visible = false;
                this.txtMemo.Text = string.Empty;

                initDisp_Edit(false);
            }
            catch (Exception ex)
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
        private void btnSave_Click(object sender, EventArgs e)
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
                if (saveData() == false)
                {
                    GVars.Msg.Show();
                    return;
                }

                // 重新显示数据
                initDisp_Edit(false);

                btnQuery_Click(sender, e);

                // 提示
                // GVars.Msg.Show("I00004", "保存");   // {0}成功!
                this.btnSave.Enabled = false;

                // 清除界面
                //this.cmbItem.SelectedIndex = -1;
                //this.txtValue.Text1 = string.Empty;
                //this.lblUnit.Visible = false;

                this.dtPickerTime.Focus();
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
            nursingCom = new NursingDbI(GVars.OracleAccess);

            dsNursingItemDict = nursingCom.GetNursingItemDictMobile(GVars.User.DeptCode); // 护理项目字典

            getParameters();

            _userRight = GVars.User.GetUserFrmRight(_id);

            GVars.App.Now = GVars.OracleAccess.GetSysDate();
            // blnSuperUser = userCom.ChkSuperUser(GVars.User.ID, RIGHT_ID);
        }


        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {
            this.cmbClass_Filter.Items.AddRange(new object[] {
            "",
            "生命体征",
            "入量",
            "出量",
            "其它记量",
            "护理事件"});

            this.cmbClass.Items.AddRange(new object[] {
            "生命体征",
            "入量",
            "出量",
            "其它记量",
            "护理事件"});

            // 清空项目属性
            this.cmbItem.SelectedIndex = -1;
            this.txtValue.Text = string.Empty;
            this.lblUnit.Visible = false;

            this.txtMemo.Text = string.Empty;

            initDisp_Edit(false);

            dtPickerDate.Value = GVars.App.Now;
            dtPickerTime.Value = GVars.App.Now;

            //patientChanged();
            ucGridView1.Add("日期", "TIME_POINT", ComConst.FMT_DATE.SHORT, ColumnStatus.Unique);
            ucGridView1.Add("时间", "TIME_POINT", ComConst.FMT_DATE.TIME_SHORT, ColumnStatus.Unique);
            ucGridView1.Add("名称", "VITAL_SIGNS");
            ucGridView1.Add("值", "VITAL_SIGNS_CVALUES");
            ucGridView1.Add("单位", "UNITS");
            ucGridView1.Add("记录人", "NURSE");
            ucGridView1.Add("备注", "MEMO");
            ucGridView1.Init();
        }


        private void initDisp_Edit(bool editable)
        {
            this.dtPickerDate.Enabled = editable;
            this.dtPickerTime.Enabled = editable;
            this.cmbClass.Enabled = editable;
            this.cmbItem.Enabled = editable;
            this.txtValue.Enabled = editable;
        }


        /// <summary>
        /// 显示病人的护理记录
        /// </summary>
        private void showNursingData()
        {
            ucGridView1.DataSource = dsNursing.Tables[0];
        }


        /// <summary>
        ///  检查界面输入是否完整
        /// </summary>
        /// <returns></returns>
        private bool chkDisp()
        {
            if (this.cmbClass.SelectedIndex == -1)
            {
                GVars.Msg.MsgId = "E0012";     // 必须输入{0}!
                GVars.Msg.MsgContent.Add("类别");
                GVars.Msg.ErrorSrc = cmbClass;

                return false;
            }

            if (this.cmbItem.SelectedIndex == -1)
            {
                GVars.Msg.MsgId = "E0012";     // 必须输入{0}!
                GVars.Msg.MsgContent.Add("项目");
                GVars.Msg.ErrorSrc = cmbItem;
                return false;
            }

            if (this.cmbClass.SelectedIndex < cmbClass.Items.Count - 1 &&
                this.txtValue.Text.Trim().Length == 0 && this.txtValue.Enabled == true)
            {
                GVars.Msg.MsgId = "E0012";     // 必须输入{0}!
                GVars.Msg.MsgContent.Add("值");
                GVars.Msg.ErrorSrc = txtValue;

                return false;
            }

            // 时间不能大于当前系统时间
            DateTime dt = dtPickerDate.Value.Date;
            DateTime dtTime = dtPickerTime.Value;

            dt = dt.AddHours(dtTime.Hour).AddMinutes(dtTime.Minute).AddSeconds(dtTime.Second);

            DataRow drItem = drNursingItem[cmbItem.SelectedIndex];

            if (vitalCodePreInput.IndexOf(drItem["VITAL_CODE"].ToString()) >= 0)
            //if (cmbItem.Text1.IndexOf("出院") >= 0 || cmbItem.Text1.IndexOf("手术") >= 0)
            {
                return true;
            }

            GVars.App.Now = GVars.OracleAccess.GetSysDate();

            if (dt.CompareTo(GVars.App.Now) > 0)
            {
                GVars.Msg.MsgId = "E0011";              // 日期不能大于当前系统日期!
                GVars.Msg.ErrorSrc = dtPickerDate;

                return false;
            }

            return true;
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        private bool saveData()
        {
            string recDate = dtPickerDate.Value.ToString(ComConst.FMT_DATE.SHORT);
            DataRow drItem = drNursingItem[cmbItem.SelectedIndex];
            DataRow dr;
            if (blnAdd)
            {
                dr = dsNursing.Tables[0].NewRow();
            }
            else
                dr = CurrentRow;

            dr["PATIENT_ID"] = patientId;
            dr["VISIT_ID"] = visitId;
            dr["CLASS_CODE"] = drItem["CLASS_CODE"];
            dr["VITAL_CODE"] = drItem["VITAL_CODE"].ToString();
            dr["RECORDING_DATE"] = recDate;
            dr["TIME_POINT"] = dtPickerDate.Value.ToString("yyyy-MM-dd") + " " + dtPickerTime.Value.ToString("HH:mm:ss"); //dtPickerDate.Value.ToString(ComConst.FMT_DATE.LONG);
            dr["VITAL_SIGNS"] = cmbItem.Text;
            dr["UNITS"] = drItem["UNIT"].ToString();
            dr["VITAL_SIGNS_CVALUES"] = txtValue.Text.Trim();
            dr["WARD_CODE"] = GVars.User.DeptCode;
            dr["NURSE"] = GVars.User.Name;
            dr["MEMO"] = txtMemo.Text.Trim();

            //if (blnAdd) dsNursing.Tables[0].Rows.Add(dr);

            //return nursingCom.SaveNursingData(ref dsNursing, patientId, visitId);

            //解决保存时如果报主键冲突的错误时，不将新增数据先显示到界面上。
            DataSet ds = dsNursing.Copy();
            if (blnAdd) ds.Tables[0].Rows.Add(dr.ItemArray);
            bool result = nursingCom.SaveNursingData(ref ds, patientId, visitId);
            if (result && blnAdd)
            {
                dsNursing.Tables[0].Rows.Add(dr);
                dsNursing.AcceptChanges();
            }
            return result;
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        private void deleteRec()
        {
            SqlConvert.DbField[] arr = new SqlConvert.DbField[5];

            string tableName = "VITAL_SIGNS_REC";
            string recDate = dtPickerDate.Value.ToString(ComConst.FMT_DATE.SHORT);
            string recTime = dtPickerTime.Value.ToString(ComConst.FMT_DATE.TIME);
            DataRow drItem = drNursingItem[cmbItem.SelectedIndex];

            int i = 0;
            arr[i++] = SqlConvert.GetDbField_Ora("PATIENT_ID", patientId, SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("VISIT_ID", visitId, SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("CLASS_CODE", drItem["CLASS_CODE"].ToString(), SqlManager.FIELD_TYPE.STR);
            arr[i++] = SqlConvert.GetDbField_Ora("VITAL_CODE", drItem["VITAL_CODE"].ToString(), SqlManager.FIELD_TYPE.STR);
            //arr[i++] = SqlConvert.GetDbField_Ora("RECORDING_DATE", recDate, SqlManager.FIELD_TYPE.DATE);
            arr[i++] = SqlConvert.GetDbField_Ora("TIME_POINT", recDate + ComConst.STR.BLANK + recTime, SqlManager.FIELD_TYPE.DATE);

            // 判断记录是否存在
            string where = SqlConvert.getFieldValuePairAssert(arr, 5);
            string sql = "SELECT PATIENT_ID FROM " + tableName + SqlConvert.getSQLWhere(where);

            // 如果记录存在
            if (GVars.OracleAccess.SelectValue(sql) == true)
            {
                sql = "DELETE FROM " + tableName + SqlConvert.getSQLWhere(where);
                GVars.OracleAccess.ExecuteNoQuery(sql);

                GVars.Msg.Show("I0009", "删除");     // {0}成功!
            }
            // 如果记录不存在
            else
            {
                MessageBox.Show(sql);
                // GVars.Msg.Show("E0005", "删除" + ComConst.STR.CRLF + sql);     // {0}失败!
            }
        }


        /// <summary>
        /// 获取参数
        /// </summary>
        private void getParameters()
        {
            if (GVars.App.AppParameters == null)
            {
                GVars.App.ReloadParameters();
            }

            vitalCodePreInput = GVars.App.GetParameters(GVars.User.DeptCode, GVars.User.ID, "VITAL_CODE_PRE_INPUT");
        }
        #endregion

        private void ucGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (ucGridView1.SelectedRow == null)
                {
                    return;
                }
                else
                {
                    btnDelete.Enabled = true;
                }

                // 获取当前记录
                CurrentRow = ucGridView1.SelectedRow;

                // 显示当前记录
                DateTime dtRec = (DateTime)CurrentRow["TIME_POINT"];
                dtPickerDate.Value = dtRec;                             // 日期
                dtPickerTime.Value = dtRec;                             // 时间

                cmbClass.SelectedIndex = nursingCom.GetNursingItemAttribute(CurrentRow["VITAL_CODE"].ToString(), GVars.User.DeptCode);

                cmbItem.SelectedValue = CurrentRow["VITAL_SIGNS"].ToString();            // 项目名称
                txtValue.Text = CurrentRow["VITAL_SIGNS_CVALUES"].ToString();   // 值
                lblUnit.Text = "(" + CurrentRow["UNITS"].ToString() + ")";      // 单位

                lblUnit.Visible = (lblUnit.Text.Length > 2);
                txtMemo.Text = CurrentRow["MEMO"].ToString();                   // 备注

                dtTimePoint = (DateTime)CurrentRow["TIME_POINT"];

                bool enabled = (_userRight.IndexOf(RIGHT_EDIT) >= 0 || GVars.User.Name.Equals(CurrentRow["NURSE"].ToString()));

                // 根据权限设置界面状态
                initDisp_Edit(enabled);

                blnAdd = false;

                //this.btnSave.Enabled = enabled;
                //this.btnDelete.Enabled = enabled;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        void IBasePatient.Patient_ListRefresh(object sender, PatientEventArgs e)
        {

        }

    }

    struct PrimaryKeyItem
    {
        public string patient_id;
        public string visit_id;
        public string recording_date;
        public string time_point;
        public string class_code;
        public string vital_code;
    }
}