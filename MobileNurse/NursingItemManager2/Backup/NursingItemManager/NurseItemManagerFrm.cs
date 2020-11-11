using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class NurseItemManagerFrm : FormDo
    {
        #region 变量
        private  HospitalDbI    hospitalDbI     = null;
        private  NursingDbI     nursingDbI      = null;
        
        private  string         deptCode        = string.Empty;
        
        private  DataSet        dsNursingUnit   = null;
        private  DataSet        dsNursingItem   = null;
        private  DataSet        dsNursingItemMobile = null;
        
        private DataTable       dtItemAttribute = null;
        private DataTable       dtValType       = null;
        #endregion
        
        public NurseItemManagerFrm()
        {
            InitializeComponent();
            
            _id     = "00014";
            _guid   = "AF22365D-4D4A-4b20-91DB-BA2B13C9DD4D";
        }
        
        
        #region 窗体事件
        private void NurseItemManagerFrm_Load(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {
                GVars.App.UserInput = false;
                
                initFrmVal();
                initDisp();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }
        
        
        #region DataGridView
        private void dgv_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            dgv.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }


        private void dgvDept_SelectionChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                // 条件检查
                if (dgvDept.SelectedRows.Count == 0)
                {
                    return;
                }
                
                // 获取科室代码
                DataGridViewRow dgvRow = dgvDept.SelectedRows[0];
                deptCode = dgvRow.Cells["DEPT_CODE"].Value.ToString();
                
                // 查找该病区护理项目
                dsNursingItem = getNursingItem();
                
                dsNursingItem.Tables[0].DefaultView.Sort = "VITAL_CODE";
                
                dgvNursingItem.AutoGenerateColumns = false;
                dgvNursingItem.DataSource = dsNursingItem.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        
        
        /// <summary>
        /// 护理项目选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvNursingItem_SelectionChanged(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            
            try
            {   
                GVars.App.UserInput = false;
                
                if (dgvNursingItem.SelectedRows.Count == 0)
                {
                    return;
                }
                
                DataGridViewRow dgvRow = dgvNursingItem.SelectedRows[0];
                showNurseItemProperty(ref dgvRow);
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }                 
        #endregion
        
        
        private void item_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }
                
                this.btnSave.Enabled = true;
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            
            this.Cursor = Cursors.WaitCursor;
            
            try
            {
                GVars.App.UserInput = false;
                
                if (chkDisp() == false)
                {
                    return;
                }
                
                string vitalCode =txtVitalCode.Text.Trim();
                
                saveDisp();
                
                nursingDbI.SaveNursingItemDictMobile(ref dsNursingItemMobile, deptCode);
                
                // 刷新显示
                dsNursingItem = getNursingItem();
                
                dsNursingItem.Tables[0].DefaultView.Sort = "VITAL_CODE";
                
                dgvNursingItem.AutoGenerateColumns = false;
                dgvNursingItem.DataSource = dsNursingItem.Tables[0].DefaultView;
                
                // 定位
                GVars.App.UserInput = true;
                locate(vitalCode);
            }
            catch(Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                GVars.App.UserInput = blnStore;
            }
        }        
        #endregion
        
        
        #region 共通函数
        private void initFrmVal()
        {
            hospitalDbI = new HospitalDbI(GVars.OracleAccess);
            nursingDbI = new NursingDbI(GVars.OracleAccess);
            
            dsNursingUnit = getNursingUnits();
            
            // 初始化
            dtItemAttribute = new DataTable("Attribute");
            dtItemAttribute.Columns.Add("CODE", Type.GetType("System.String"));
            dtItemAttribute.Columns.Add("TEXT", Type.GetType("System.String"));
            
            DataRow drNew = dtItemAttribute.NewRow();
            drNew["CODE"] = "0";        
            drNew["TEXT"] = "生命体征";
            dtItemAttribute.Rows.Add(drNew);
            
            drNew = dtItemAttribute.NewRow();
            drNew["CODE"] = "1";        
            drNew["TEXT"] = "入量";
            dtItemAttribute.Rows.Add(drNew);
            
            drNew = dtItemAttribute.NewRow();
            drNew["CODE"] = "2";        
            drNew["TEXT"] = "出量";
            dtItemAttribute.Rows.Add(drNew);            
            
            drNew = dtItemAttribute.NewRow();
            drNew["CODE"] = "3";        
            drNew["TEXT"] = "其它计量";
            dtItemAttribute.Rows.Add(drNew); 
            
            drNew = dtItemAttribute.NewRow();
            drNew["CODE"] = "4";        
            drNew["TEXT"] = "护理事件";
            dtItemAttribute.Rows.Add(drNew);
            
            ATTRIBUTE.DataSource = dtItemAttribute;
            ATTRIBUTE.ValueMember = "CODE";
            ATTRIBUTE.DisplayMember = "TEXT";
            
            cmbProperty.DataSource = dtItemAttribute;
            cmbProperty.ValueMember = "CODE";
            cmbProperty.DisplayMember = "TEXT";
            
            // 值类型表
            dtValType = new DataTable("ValType");
            dtValType.Columns.Add("CODE", Type.GetType("System.String"));
            dtValType.Columns.Add("TEXT", Type.GetType("System.String"));
            
            drNew = dtValType.NewRow();
            drNew["CODE"] = "0";        
            drNew["TEXT"] = "字符型";
            dtValType.Rows.Add(drNew);
            
            drNew = dtValType.NewRow();
            drNew["CODE"] = "1";        
            drNew["TEXT"] = "数值型";
            dtValType.Rows.Add(drNew);
            
            VALUE_TYPE.DataSource = dtValType;
            VALUE_TYPE.ValueMember = "CODE";
            VALUE_TYPE.DisplayMember = "TEXT";
            
            cmbValType.DataSource = dtValType;
            cmbValType.ValueMember = "CODE";
            cmbValType.DisplayMember = "TEXT";
        }
        
        
        private void initDisp()
        {
            // 护理单元
            dsNursingUnit.Tables[0].DefaultView.Sort = "DEPT_NAME";
            
            dgvDept.AutoGenerateColumns = false;
            dgvDept.DataSource = dsNursingUnit.Tables[0].DefaultView;
            
            // 界面控件
            this.btnSave.Enabled = false;
        }
        
        
        private DataSet getNursingUnits()
        {
            return hospitalDbI.Get_WardList_Nurse();
        }        
        
        
        private DataSet getNursingItem()
        {
            try
            {
                DataSet dsItem  = nursingDbI.GetNursingItemDict(deptCode);
                dsNursingItemMobile = nursingDbI.GetNursingItemDictMobile(deptCode);
                
                DataColumn[] pkColumns = new DataColumn[3];
                pkColumns[0] = dsNursingItemMobile.Tables[0].Columns["CLASS_CODE"];
                pkColumns[1] = dsNursingItemMobile.Tables[0].Columns["VITAL_CODE"];
                pkColumns[2] = dsNursingItemMobile.Tables[0].Columns["DEPT_CODE"];
                dsNursingItemMobile.Tables[0].PrimaryKey = pkColumns;
                
                pkColumns = new DataColumn[3];
                pkColumns[0] = dsItem.Tables[0].Columns["CLASS_CODE"];
                pkColumns[1] = dsItem.Tables[0].Columns["VITAL_CODE"];
                pkColumns[2] = dsItem.Tables[0].Columns["DEPT_CODE"];
                dsItem.Tables[0].PrimaryKey = pkColumns;
                
                //dsItem.WriteXml(@"D:\dsItem.xml", XmlWriteMode.WriteSchema);
                //dsNursingItemMobile.WriteXml(@"D:\dsNursingItemMobile.xml", XmlWriteMode.WriteSchema);
                
                dsItem.Tables[0].Merge(dsNursingItemMobile.Tables[0], false, MissingSchemaAction.Add);
                
                return dsItem;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
                
                return null;
            }
        }
        
        
        private bool showNurseItemProperty(ref DataGridViewRow dgvRow)
        {
            this.txtClassCode.Text      = dgvRow.Cells["CLASS_CODE"].Value.ToString();      // 类别
            this.txtVitalCode.Text      = dgvRow.Cells["VITAL_CODE"].Value.ToString();      // 代码
            this.txtVitalSigns.Text     = dgvRow.Cells["VITAL_SIGNS"].Value.ToString();     // 名称
            this.txtUnit.Text           = dgvRow.Cells["UNIT"].Value.ToString();            // 单位
            
            int intVal = 0;
            
            this.cmbProperty.SelectedIndex = -1;                                            // 属性
            if (int.TryParse(dgvRow.Cells["ATTRIBUTE"].Value.ToString(), out intVal) == true)
            {
                if (-1 < intVal && intVal < cmbProperty.Items.Count)
                {
                    this.cmbProperty.SelectedIndex = intVal;                                // 属性
                }
            }
            
            this.txtShowOrder.Text      = dgvRow.Cells["SHOW_ORDER"].Value.ToString();      // 显示顺序
            
            cmbValType.SelectedIndex = -1;
            if (int.TryParse(dgvRow.Cells["VALUE_TYPE"].Value.ToString(), out intVal) == true)
            {
                if (-1 < intVal && intVal < cmbValType.Items.Count)
                {
                    this.cmbValType.SelectedIndex = intVal;                                 // 值类型
                }
            }
            
            this.txtValRng.Text         = dgvRow.Cells["VALUE_SCOPE"].Value.ToString();     // 取值范围
            this.chkEnabled.Checked     = dgvRow.Cells["ENABLED"].Value.ToString().Equals("1");   // 启用
            this.txtMemo.Text           = dgvRow.Cells["MEMO"].Value.ToString();            // 备注
            
            return true;
        }
        
        
        /// <summary>
        /// 检查用户界面输入数据是否正确
        /// </summary>
        /// <returns></returns>
        private bool chkDisp()
        {
            if (txtVitalCode.Text.Trim().Length == 0)
            {
                dgvNursingItem.Focus();
                return false;
            }
            
            if (cmbProperty.SelectedIndex == -1)
            {
                cmbProperty.Focus();
                return false;
            }
            
            int intVal = 0;
            if (int.TryParse(txtShowOrder.Text, out intVal) == false)
            {
                txtShowOrder.Focus();
                return false;
            }
            
            if (cmbValType.SelectedIndex == -1)
            {
                cmbValType.Focus();
                return false;
            }
            
            return true;
        }
        
        
        /// <summary>
        /// 把界面的数据保存到缓存中
        /// </summary>
        /// <returns></returns>
        private bool saveDisp()
        {
            DateTime dtNow = GVars.OracleAccess.GetSysDate();
            
            // 查找记录
            DataRow drEdit = null;
            
            string filter = "DEPT_CODE = " + SqlManager.SqlConvert(deptCode)
                        + "AND VITAL_CODE = " + SqlManager.SqlConvert(txtVitalCode.Text.Trim());
            
            DataRow[] drFind = dsNursingItemMobile.Tables[0].Select(filter);
            
            if (drFind.Length == 0)
            {
                drEdit = dsNursingItemMobile.Tables[0].NewRow();
            }
            else
            {
                drEdit = drFind[0];
            }
            
            // 保存数据
            drEdit["DEPT_CODE"]     = deptCode;                                                 // 护理单元代码
            drEdit["CLASS_CODE"]    = txtClassCode.Text.Trim();                                 // 类型
            drEdit["VITAL_CODE"]    = txtVitalCode.Text.Trim();                                 // 项目代码
            drEdit["VITAL_SIGNS"]   = txtVitalSigns.Text.Trim();                                // 项目名称
            drEdit["UNIT"]          = txtUnit.Text.Trim();                                      // 单位
            drEdit["ATTRIBUTE"]     = cmbProperty.SelectedValue.ToString();                     // 属性
            drEdit["SHOW_ORDER"]    = txtShowOrder.Text.Trim();                                 // 显示顺序
            drEdit["VALUE_TYPE"]    = cmbValType.SelectedValue.ToString();                      // 值类型
            drEdit["VALUE_SCOPE"]   = txtValRng.Text.Trim();                                    // 取值范围
            // drEdit["PROPORTION"] = deptCode;
            drEdit["ENABLED"]       = (chkEnabled.Checked? "1": "0");                           // 启用
            drEdit["MEMO"]          = txtMemo.Text.Trim();                                      // 备注
            
            if (drFind.Length == 0)
            {
                dsNursingItemMobile.Tables[0].Rows.Add(drEdit);
            }
            
            return true;
        }
        
        
        private void locate(string vitalCode)
        {
             // 定位
            if (vitalCode.Length > 0)
            {
                foreach(DataGridViewRow dr in dgvNursingItem.Rows)
                {
                    if (dr.Cells["VITAL_CODE"].Value.ToString().Equals(vitalCode) == true)
                    {
                        dr.Selected = true;
                        break;
                    }
                }
            }
        }        
        #endregion       
    }
}
