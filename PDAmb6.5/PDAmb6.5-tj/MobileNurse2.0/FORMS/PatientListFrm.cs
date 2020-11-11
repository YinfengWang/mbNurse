using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace HISPlus
{
    public partial class PatientListFrm : Form
    {
        private string patientId = string.Empty;
        
        public PatientListFrm()
        {
            InitializeComponent();
        }

        
        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientListFrm_Load(object sender, EventArgs e)
        {
            try
            {
                this.timer1.Enabled = true;                
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 按钮 [过滤]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                PatientFilterFrm filterFrm = new PatientFilterFrm();
                if (filterFrm.ShowDialog() == DialogResult.OK)
                {
                    GVars.DsPatient = null;
                    
                    // 按过滤条件查询病人
                    Cursor.Current = Cursors.WaitCursor;

                    if (filterFrm.ArrFilter.Count > 0)
                    {
                        // GVars.DsPatient = GVars.PatDbI.GetPatientInfo_Filter(filterFrm.FilterItems);
                        for (int i = 0; i < filterFrm.ArrFilter.Count; i++)
                        {
                            DataSet ds = GVars.PatDbI.GetPatientInfo_Filter(filterFrm.ArrFilter[i].ToString());
                            if (GVars.DsPatient == null)
                            {
                                GVars.DsPatient = ds;
                            }
                            else
                            {
                                GVars.DsPatient.Tables[0].Merge(ds.Tables[0]);
                            }
                            
                            Application.DoEvents();
                        }
                    }
                    else
                    {
                        GVars.DsPatient = GVars.PatDbI.GetPatientInfo_Filter("全部");
                        //GVars.DsPatient = GVars.PatDbI.GetPatientList();
                    }
                    
                    showPatientList();
                }
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        
        
        /// <summary>
        /// 输入框获取焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_GotFocus(object sender, EventArgs e)
        {
            try
            {
                inputPanel1.Enabled = true;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
                
        /// <summary>
        /// 输入框失去焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_LostFocus(object sender, EventArgs e)
        {
            try
            {
                inputPanel1.Enabled = false;
            }
            catch(Exception ex)
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
                btnQuery.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                
                string filter = string.Empty;
                
                // 如果没有输入查询条件, 退出
                if (txtSearch.Text.Trim().Length > 0)
                {
                    string condition = txtSearch.Text.Trim();
                    filter = "NAME LIKE '%" + condition + "%' OR BED_LABEL = '" + condition + "'";
                }
                                
                DateTime dtNow = GVars.GetDateNow();
                DataRow[] drShow = GVars.DsPatient.Tables[0].Select(filter, "BED_NO");
                
                try
                {
                    lvwPatientList.Items.Clear();

                    // 显示数据
                    for (int i = 0; i < drShow.Length; i++)
                    {
                        DataRow curRow = drShow[i];
                                                
                        ListViewItem item = new ListViewItem(curRow["BED_LABEL"].ToString());               // 床标号
                        item.SubItems.Add(curRow["NAME"].ToString());                                       // 姓名
                        item.SubItems.Add(curRow["SEX"].ToString());                                        // 性别
                        
                        if (curRow["DATE_OF_BIRTH"].ToString().Length > 0)
                        {
                            item.SubItems.Add(PersonCls.GetAge((DateTime)(curRow["DATE_OF_BIRTH"]), dtNow)); // 年龄
                        }
                        else
                        {
                            item.SubItems.Add(string.Empty);
                        }
                        
                        item.SubItems.Add(curRow["PATIENT_ID"].ToString());                                 // 病人标识号
                        item.SubItems.Add(curRow["VISIT_ID"].ToString());                                   // 住院序号
                        
                        lvwPatientList.Items.Add(item);
                        
                        item.BackColor = ComFunctionApp.GetColor(curRow["NURSING_CLASS_COLOR"].ToString(), 0); // 护理等级
                        
                    }

                    // 默认选中第一个病人
                    if (lvwPatientList.Items.Count > 0)
                    {
                        lvwPatientList.Items[0].Selected = true;
                    }
                }
                finally
                {
                    lvwPatientList.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                btnQuery.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }
        
        
        /// <summary>
        /// 菜单 [确定]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwPatientList.SelectedIndices.Count == 0)
                {
                    GVars.Msg.Show("E00060", "病人");               // "请选择{0}!"
                    return;
                }
                
                ListViewItem item = lvwPatientList.Items[lvwPatientList.SelectedIndices[0]];
                
                GVars.Patient.ID = item.SubItems[lvwPatientList.Columns.Count - 2].Text;
                GVars.Patient.VisitId = item.SubItems[lvwPatientList.Columns.Count - 1].Text;
                GVars.Patient.Name    = item.SubItems[1].Text;
                
                this.DialogResult = DialogResult.OK;                
            }
            catch (Exception ex)            
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 菜单 [取消]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCancel_Click(object sender, EventArgs e)
        {
            try
            {
                // 如果不存在病人列表
                if (lvwPatientList.Items.Count == 0)
                {
                    GVars.Patient.ID      = string.Empty;
                    GVars.Patient.VisitId = string.Empty;
                    GVars.Patient.Name    = string.Empty;
                    
                    this.DialogResult = DialogResult.OK;
                    return;
                }
                
                // 判断当前病人在列表中是否存在
                bool blnExist = false;
                foreach (ListViewItem item in lvwPatientList.Items)
                {
                    if (item.SubItems[lvwPatientList.Columns.Count - 2].Text.Equals(GVars.Patient.ID) == true)
                    {
                        blnExist = true;
                        break;
                    }
                }
                
                // 如果不存在, 指定第一个病人为当前病人
                if (blnExist == false)
                {
                    ListViewItem item = lvwPatientList.Items[0];

                    GVars.Patient.ID      = item.SubItems[lvwPatientList.Columns.Count - 2].Text;
                    GVars.Patient.VisitId = item.SubItems[lvwPatientList.Columns.Count - 1].Text;
                    GVars.Patient.Name    = item.SubItems[1].Text;
                }
                
                this.DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        /// <summary>
        /// 菜单[刷新]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSync_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                
                // 删除本地多余的病人
                syncPatientInfo();

                // 重新获取病人列表
                initFrmVal();

                // 显示病人列表
                initDisp();
            }
            catch (Exception ex)
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
        /// 时钟事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                timer1.Enabled = false;
                
                initFrmVal();
                initDisp();
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
        #endregion
        
        
        #region 共通函数
        /// <summary>
        /// 初始化窗体变量
        /// </summary>
        private void initFrmVal()
        {
            // 获取本地病人列表
             
            if (GVars.patientFilterList != null && GVars.patientFilterList.Count > 0)
            {
                for (int i = 0; i < GVars.patientFilterList.Count; i++)
                {
                    DataSet ds = GVars.PatDbI.GetPatientInfo_Filter(GVars.patientFilterList[i].ToString());
                    if (GVars.DsPatient == null || GVars.DsPatient.Tables.Count == 0)
                    {
                        GVars.DsPatient = ds;
                    }
                    else
                    {
                        GVars.DsPatient.Tables[0].Merge(ds.Tables[0]);
                    }
                }
            }
            else
            {
                GVars.DsPatient = GVars.PatDbI.GetPatientInfo_Filter("全部");
            }
            
            // 设置当前病人
            patientId = GVars.Patient.ID;
        }
        
        
        /// <summary>
        /// 初始化界面显示
        /// </summary>
        private void initDisp()
        {
            showPatientList();
            
            // 在病人列表中定位当前病人
            foreach (ListViewItem item in lvwPatientList.Items)
            {
                if (item.SubItems[lvwPatientList.Columns.Count - 2].Text.Equals(GVars.Patient.ID) == true)
                {
                    item.Selected = true;
                    break;
                }
            }
        }
        
        
        /// <summary>
        /// 显示病人列表
        /// </summary>
        private void showPatientList()
        {
            DateTime dtNow = GVars.GetDateNow();
            
            Cursor.Current = Cursors.WaitCursor;
            
            // 清除原来的数据
            lvwPatientList.BeginUpdate();
            
            try
            {
                lvwPatientList.Items.Clear();
                
                if (GVars.DsPatient == null || GVars.DsPatient.Tables.Count == 0)
                {
                    return;
                }

                DataRow[] drPatient = GVars.DsPatient.Tables[0].Select(string.Empty, "BED_NO ASC");
                // 显示数据
                for (int i = 0; i < drPatient.Length; i++)
                {
                    DataRow curRow = drPatient[i];
                    
                    ListViewItem item = new ListViewItem(curRow["BED_LABEL"].ToString());               // 床标号
                    item.SubItems.Add(curRow["NAME"].ToString());                                       // 姓名
                    item.SubItems.Add(curRow["SEX"].ToString());                                        // 性别
                    
                    if (curRow["DATE_OF_BIRTH"].ToString().Length > 0)
                    {
                        item.SubItems.Add(PersonCls.GetAge((DateTime)(curRow["DATE_OF_BIRTH"]), dtNow)); // 年龄
                    }
                    else
                    {
                        item.SubItems.Add(string.Empty);
                    }
                    item.SubItems.Add(curRow["PATIENT_ID"].ToString());                                 // 病人标识号
                    item.SubItems.Add(curRow["VISIT_ID"].ToString());                                   // 住院序号
                    
                    lvwPatientList.Items.Add(item);
                    
                    item.BackColor = ComFunctionApp.GetColor(curRow["NURSING_CLASS_COLOR"].ToString(), 0); // 护理等级
                }
                
                if (lvwPatientList.Items.Count > 0)
                {
                    lvwPatientList.Items[0].Selected = true;
                }
            }
            finally
            {
                lvwPatientList.EndUpdate();
                
                Cursor.Current = Cursors.Default;
            }
        }                               
        
        
        /// <summary>
        /// 同步病人信息
        /// </summary>
        private void syncPatientInfo()
        {
            // 服务器端获取的数据
            //if (GVars.DsPatient == null || GVars.DsPatient.Tables.Count == 0 || GVars.DsPatient.Tables[0].Rows.Count == 0)
            //{
            //    return;
            //}
            GVars.DsPatient = GVars.PatDbI.GetPatientInfo_Filter("全部");
            GVars.DsPatient.WriteXml(@"DsPatient.xml", XmlWriteMode.WriteSchema);
            
            // 本地获取的数据
            DataSet dsPatientLocal = GVars.sqlceLocal.SelectData("SELECT * FROM PATIENT_INFO", "PATIENT_INFO");
            if (dsPatientLocal == null || dsPatientLocal.Tables.Count == 0 || dsPatientLocal.Tables[0].Rows.Count == 0)
            {
                return;
            }
            
            dsPatientLocal.WriteXml(@"dsPatientLocal.xml", XmlWriteMode.WriteSchema);
            
            // 比较两端数据
            DataRow[] drSrc  = GVars.DsPatient.Tables[0].Select(string.Empty, "PATIENT_ID ASC");
            DataRow[] drDest = dsPatientLocal.Tables[0].Select(string.Empty, "PATIENT_ID ASC");
            
            int idx_src = 0;
            int idx_dest = 0;
            
            string key_src = string.Empty;
            string key_dest = string.Empty;
            int compare = 0;
            
            // 处理结果
            ArrayList arrSql = new ArrayList();
            
            while(idx_src < drSrc.Length && idx_dest < drDest.Length)
            {
                key_src  = drSrc[idx_src]["PATIENT_ID"].ToString().ToUpper();
                key_dest = drDest[idx_dest]["PATIENT_ID"].ToString().ToUpper();
                
                // 主键比较
                compare = key_src.CompareTo(key_dest);
                
                // 如果主键相等
                if (compare == 0) 
                {
                    idx_dest++;
                    idx_src++;
                    continue;
                }
                
                // 如果源比目标小, 表示源比目标多一条数据   (忽略)
                if (compare < 0)
                {
                    idx_src++;
                    continue;
                }
                
                // 如果源比目标大, 表示目标比源多一条数据   (删除目标数据)
                arrSql.Add("DELETE FROM PATIENT_INFO WHERE PATIENT_ID = " + SqlManager.SqlConvert(key_dest));
                idx_dest++;
            }
            
            // 如果源比目标大, 表示目标比源多一条数据
            while (idx_dest < drDest.Length)
            {
                key_dest = drDest[idx_dest]["PATIENT_ID"].ToString().ToUpper();
                arrSql.Add("DELETE FROM PATIENT_INFO WHERE PATIENT_ID = " + SqlManager.SqlConvert(key_dest));
                idx_dest++;
            }
            
            if (arrSql.Count > 0)
            {
                GVars.sqlceLocal.ExecuteNoQuery(ref arrSql);
            }
        }
        #endregion
    }
}