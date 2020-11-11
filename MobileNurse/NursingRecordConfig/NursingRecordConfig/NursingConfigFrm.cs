using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using DevExpress.XtraEditors;

namespace HISPlus
{
    public partial class NursingConfigFrm : FormDo
    {
        /// <summary>
        /// 修改nursing_record_define
        /// </summary>
        private DataSet dsUpdate;//修改nursing_record_define
        /// <summary>
        /// 存储某个表的列名
        /// </summary>
        private DataSet dsColumn;//存储某个表的列名
        /// <summary>
        /// 修改nursing_record_type
        /// </summary>
        private DataSet ds_recordType;//修改nursing_record_type
        /// <summary>
        /// 关联科室
        /// </summary>
        private DataTable dt_xCb;//关联科室
        /// <summary>
        /// 关联护理事件
        /// </summary>
        private DataTable dtVital;//关联护理事件
        /// <summary>
        /// 关联科室
        /// </summary>
        private DataSet dsVital;//关联科室
        /// <summary>
        /// 关联字典
        /// </summary>
        private DataTable dtDictCode;//关联字典
        private NursingConfigCom com;
        public NursingConfigFrm()
        {
            InitializeComponent();
            _id = "00095";
            _guid = "260F5DA0-C906-4460-ABE8-DB76AAC2A81F";
        }

        private void NursingConfigFrm_Load(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false; // 为了避免 触发文本框编辑事件

                initFrmVal();
                initDisp();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }
        /// <summary>
        /// 获取数据集
        /// </summary>
        private void initFrmVal()
        {
            com = new NursingConfigCom();
            dsUpdate = com.GetConfigList("nursing_record_define", "");
            dsColumn = com.ReturnColName("NURSING_RECORD_CONTENT");
            ds_recordType = com.GetConfigList("nursing_record_type", "");
            dt_xCb = com.GetConfigList("dept_dict", "clinic_attr=2 and outp_or_inp=1").Tables[0];
            dtVital = com.GetConfigList("nursing_item_dict", "attribute=4 and ward_code=" + SqlManager.SqlConvert(GVars.User.DeptCode)).Tables[0];
            dsVital = com.GetConfigList("nursing_record_print_skip_flg", "");
            dtDictCode = com.GetConfigList("HIS_DICT", "").Tables[0];

            ucGridView1.ShowRowIndicator = true;
            ucGridView1.ColumnAutoWidth = true;
            ucGridView1.Add("标题", "COL_TITLE", 150);
            ucGridView1.Add("参数", "VITAL_CODE", 50);
            ucGridView1.Add("宽度", "COL_WIDTH", 50);
            ucGridView1.Add("序号", "SERIAL_NO", 50);
            ucGridView1.Add("对应列", "STORE_COL_NAME", 120);
            //ucGridView1.Add("类型", "COL_TYPE", 100);
            //ucGridView1.Add("字典ID", "DICT_ID", 100);
            //ucGridView1.Add("字典取值", "SELECTED_VALUE", 100);
            //ucGridView1.Add("是否多选", "MULTI_VALUE", 100);
            //ucGridView1.Add("是否多行", "MULTI_LINE", 100);
            //ucGridView1.Add("是否签名", "SIGN_FLAG", 100);
            ucGridView1.Add("列ID", "COL_ID", 100);
            ucGridView1.SelectionChanged += ucGridView1_SelectionChanged;
            //ucGridView1.ColumnAutoWidth = false;
            ucGridView1.Init();

            ucGvType.ShowRowIndicator = true;
            ucGvType.Add("护理记录单名称", "DESCRIPTIONS");
            ucGvType.Add("打印行数", "ROW_COUNT", false);
            ucGvType.Add("科室代码", "DEPT_CODE", false);
            ucGvType.Add("护理代码", "TYPE_ID", false);

            ucGvType.Init();


            this.cbType.Items.AddRange(new object[] {
            "String",
            "Number",
            "DateTime"});

            this.cb_SIGN_FLAG.Items.AddRange(new object[] {
            "否",
            "是"});

            this.cb_MULTI_VALUE.Items.AddRange(new object[] {
            "否",
            "是"});

            this.cb_MULTI_LINE.Items.AddRange(new object[] {
            "否",
            "是"});

            this.cb_selectValue.AddRange(new object[] {
            "",
            "取ID",
            "取值",
            "取模版"});
        }

        /// <summary>
        /// 绑定数据集
        /// </summary>
        private void initDisp()
        {
            this.cb_storeCol.DataSource = dsColumn.Tables[0].DefaultView;
            this.cb_storeCol.DisplayMember = "COLUMN_NAME";
            this.cb_storeCol.ValueMember = "COLUMN_NAME";

            DataRow dr = dtDictCode.NewRow();
            dr["DICT_ID"] = "";
            dr["DESCRIPTIONS"] = "";
            dtDictCode.Rows.InsertAt(dr, 0);
            this.cbDictCode.DataSource = dtDictCode;
            this.cbDictCode.DisplayMember = "DESCRIPTIONS";
            this.cbDictCode.ValueMember = "DICT_ID";

            cbxDeptCode.DisplayMember = "DEPT_NAME";
            cbxDeptCode.ValueMember = "DEPT_CODE";
            cbxDeptCode.SelectionMode = SelectionMode.MultiExtended;
            cbxDeptCode.CheckOnClick = true;

            cbxDeptCode.DataSource = dt_xCb;

            cbxVital.DisplayMember = "VITAL_SIGNS";
            cbxVital.ValueMember = "VITAL_CODE";
            cbxVital.SelectionMode = SelectionMode.MultiExtended;
            cbxVital.CheckOnClick = true;

            cbxVital.DataSource = dtVital;
            //dgvModule.AutoGenerateColumns = false;
            //dgv_type.AutoGenerateColumns = false;

            dsUpdate.Tables[0].DefaultView.Sort = "SERIAL_NO";
            //dgvModule.DataSource = dsUpdate.Tables[0].DefaultView;
            ucGridView1.DataSource = dsUpdate.Tables[0].DefaultView;

            ds_recordType.Tables[0].DefaultView.Sort = "type_id";
            //dgv_type.DataSource = ds_recordType.Tables[0].DefaultView;
            ucGvType.DataSource = ds_recordType.Tables[0].DefaultView;
        }


        void ucGridView1_SelectionChanged(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;    // 为了避免 触发文本框编辑事件

                // 初始化 应用程序详细信息的显示
                initDisp_ModuleDetail();

                if (ucGridView1.SelectRowsCount == 0)
                {
                    return;
                }

                DataRow dr = ucGridView1.SelectedRow;
                if (dr == null) return;
                showModuleDetail(dr);

                this.btnSave.Enabled = false;
                this.bt_saveConfig.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }

        /// <summary>
        /// 清空界面显示
        /// </summary>
        private void initDisp_ModuleDetail()
        {
            this.txtNumber.Text = string.Empty;
            this.txtTitle.Text = string.Empty;
            cbType.SelectedIndex = 0;
            txtVital_code.Text = string.Empty;
            this.cbDictCode.SelectedIndex = 0;
            cb_selectValue.SelectedIndex = 0;
            cb_MULTI_VALUE.SelectedIndex = 0;
            txt_width.Text = string.Empty;
            cb_MULTI_LINE.SelectedIndex = 0;
            cb_SIGN_FLAG.SelectedIndex = 0;
            txtserial_no.Text = string.Empty;
            cb_storeCol.SelectedIndex = 0;
        }

        /// <summary>
        /// 单击行时 控件中显示该行的各值
        /// </summary>
        /// <param name="drApp"></param>
        private void showModuleDetail(DataGridViewRow drApp)
        {
            this.txtNumber.Text = drApp.Cells["COL_ID"].Value.ToString();
            this.txtTitle.Text = drApp.Cells["col_title"].Value.ToString();
            txtVital_code.Text = drApp.Cells["vital_code"].Value.ToString();
            txt_width.Text = drApp.Cells["col_width"].Value.ToString();
            txtserial_no.Text = drApp.Cells["serial_no"].Value.ToString();
            cb_storeCol.SelectedValue = drApp.Cells["store_col_name"].Value.ToString().Trim();
            string col_type = drApp.Cells["COL_TYPE"].Value.ToString();
            switch (col_type)
            {
                case "0":
                    cbType.SelectedValue = "String";
                    break;
                case "1":
                    cbType.SelectedValue = "Number";
                    break;
                case "2":
                    cbType.SelectedValue = "DateTime";
                    break;
                default:
                    cbType.SelectedValue = string.Empty;
                    break;
            }
            if (drApp.Cells["Dict_id"].Value.ToString().Trim().Length != 0)
            {
                DataRow[] drShowCode = dtDictCode.Select("dict_id=" + SqlManager.SqlConvert(drApp.Cells["Dict_id"].Value.ToString()));
                if (drShowCode.Length != 0)
                {
                    //this.cbDictCode.Text1 = drShowCode[0]["DESCRIPTIONS"].ToString();
                    this.cbDictCode.SelectedValue = drApp.Cells["Dict_id"].Value.ToString();
                }
            }

            string selectValue = drApp.Cells["SELECTED_VALUE"].Value.ToString();
            switch (selectValue)
            {
                case "0":
                    this.cb_selectValue.SelectedValue = "取ID";
                    break;
                case "1":
                    cb_selectValue.SelectedValue = "取值";
                    break;
                case "2":
                    cb_selectValue.SelectedValue = "取模版";
                    break;
                default:
                    cb_selectValue.SelectedValue = string.Empty;
                    break;
            }
            this.cb_MULTI_VALUE.SelectedIndex = ShowData(drApp.Cells["MULTI_VALUE"].Value.ToString());
            this.cb_MULTI_LINE.SelectedIndex = ShowData(drApp.Cells["MULTI_LINE"].Value.ToString());
            this.cb_SIGN_FLAG.SelectedIndex = ShowData(drApp.Cells["SIGN_FLAG"].Value.ToString());
        }

        /// <summary>
        /// 单击行时 控件中显示该行的各值
        /// </summary>
        /// <param name="drApp"></param>
        private void showModuleDetail(DataRow drApp)
        {
            this.txtNumber.Text = drApp["COL_ID"].ToString();
            this.txtTitle.Text = drApp["col_title"].ToString();
            txtVital_code.Text = drApp["vital_code"].ToString();
            txt_width.Text = drApp["col_width"].ToString();
            txtserial_no.Text = drApp["serial_no"].ToString();
            cb_storeCol.SelectedValue = drApp["store_col_name"].ToString().Trim();
            string col_type = drApp["COL_TYPE"].ToString();
            switch (col_type)
            {
                case "0":
                    cbType.SelectedValue = "String";
                    break;
                case "1":
                    cbType.SelectedValue = "Number";
                    break;
                case "2":
                    cbType.SelectedValue = "DateTime";
                    break;
                default:
                    cbType.SelectedValue = string.Empty;
                    break;
            }
            if (drApp["Dict_id"].ToString().Trim().Length != 0)
            {
                DataRow[] drShowCode = dtDictCode.Select("dict_id=" + SqlManager.SqlConvert(drApp["Dict_id"].ToString()));
                if (drShowCode.Length != 0)
                {
                    //this.cbDictCode.Text1 = drShowCode[0]["DESCRIPTIONS"].ToString();
                    this.cbDictCode.SelectedValue = drApp["Dict_id"].ToString();
                }
            }

            string selectValue = drApp["SELECTED_VALUE"].ToString();
            switch (selectValue)
            {
                case "0":
                    this.cb_selectValue.SelectedValue = "取ID";
                    break;
                case "1":
                    cb_selectValue.SelectedValue = "取值";
                    break;
                case "2":
                    cb_selectValue.SelectedValue = "取模版";
                    break;
                default:
                    cb_selectValue.SelectedValue = string.Empty;
                    break;
            }
            this.cb_MULTI_VALUE.SelectedIndex = ShowData(drApp["MULTI_VALUE"].ToString());
            this.cb_MULTI_LINE.SelectedIndex = ShowData(drApp["MULTI_LINE"].ToString());
            this.cb_SIGN_FLAG.SelectedIndex = ShowData(drApp["SIGN_FLAG"].ToString());
        }

        /// <summary>
        /// 显示“是或否”
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int ShowData(string value)
        {
            switch (value)
            {
                case "1":
                    return 1;
                default:
                    return 0;
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                cbxDeptCode.UnCheckAll();
                cbxDeptCode.UnSelectAll();

                cbxVital.UnCheckAll();
                cbxVital.UnSelectAll();

                this.txtModuleCode.Text = com.ReturnMaxTypeId();
                this.txtModuleName.Text = string.Empty;
                this.txtModuleName.Focus();
                txtRowCount.Text = string.Empty;
                this.btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // 条件检查
                if (txtModuleCode.Text.Trim().Length == 0)
                {
                    return;
                }
                if (GVars.Msg.Show("Q0004", "模块") != DialogResult.Yes)       // 您确认要删除当前{0}吗?
                {
                    return;
                }

                string moduleCode = txtModuleCode.Text.Trim();
                string filter = "type_id = " + SqlManager.SqlConvert(moduleCode);
                DataRow[] drFind = ds_recordType.Tables[0].Select(filter);

                if (drFind.Length > 0)
                {
                    drFind[0].Delete();
                    com.SaveConfigList(ref ds_recordType, "nursing_record_type");
                    ds_recordType = com.GetConfigList("nursing_record_type", "");
                    ds_recordType.Tables[0].DefaultView.Sort = "TYPE_ID";
                    //this.dgv_type.DataSource = ds_recordType.Tables[0].DefaultView;
                    ucGvType.DataSource = ds_recordType.Tables[0].DefaultView;
                }

                //当nursing_record_type中删除某一记录单时nursing_record_define中对应的项目也将删除
                DataSet ds = com.GetConfigList("nursing_record_type", "");
                DataRow[] drFind_exist = ds.Tables[0].Select("type_id=" + SqlManager.SqlConvert(moduleCode));
                DataSet ds_define = com.GetConfigList("nursing_record_define", "");
                DataRow[] dr_type = ds_define.Tables[0].Select("type_id=" + SqlManager.SqlConvert(moduleCode));
                if (drFind_exist.Length == 0)
                {
                    for (int i = 0; i < dr_type.Count(); i++)
                    {
                        dr_type[i].Delete();
                    }
                    com.SaveConfigList(ref ds_define, "nursing_record_define");
                    // 刷新显示
                    //ds_define = com.GetConfigList("nursing_record_define", "");
                    //ds_define.Tables[0].DefaultView.Sort = "SERIAL_NO";
                    //dgvModule.DataSource = ds_define.Tables[0].DefaultView;
                    //ucGridView1.DataSource = ds_define.Tables[0].DefaultView;

                    // 会自动刷新显示
                }

                this.btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region 修改nursing_recode_type
                if (this.txtModuleName.Text.Trim().Length == 0)
                {
                    txtModuleName.Focus();
                    return;
                }
                string moduleCode = this.txtModuleCode.Text.Trim();
                DataRow[] drFind_recordType = ds_recordType.Tables[0].Select("type_id=" + SqlManager.SqlConvert(moduleCode));
                DataRow drEdit_recordType = null;
                if (drFind_recordType.Length == 0)
                {
                    drEdit_recordType = ds_recordType.Tables[0].NewRow();
                }
                else
                {
                    drEdit_recordType = drFind_recordType[0];
                }
                drEdit_recordType["TYPE_ID"] = this.txtModuleCode.Text.Trim();
                drEdit_recordType["DESCRIPTIONS"] = this.txtModuleName.Text.Trim();
                string dept_codeStr = ReturnCheckStr(dt_xCb, this.cbxDeptCode);
                drEdit_recordType["DEPT_CODE"] = dept_codeStr;
                if (this.txtRowCount.Text.Trim().Length != 0)
                {
                    drEdit_recordType["ROW_COUNT"] = Convert.ToDecimal(this.txtRowCount.Text.Trim());
                }
                if (drFind_recordType.Length == 0)
                {
                    ds_recordType.Tables[0].Rows.Add(drEdit_recordType);
                }
                #endregion

                #region 修改nursing_record_print_skip_flg
                for (int i = 0; i < dtVital.Rows.Count; i++)
                {
                    if (this.cbxVital.GetItemChecked(i) == true)
                    {
                        string strVital = this.cbxVital.GetItemValue(i).ToString();
                        DataRow[] drVital = dsVital.Tables[0].Select("ward_code=" + SqlManager.SqlConvert(GVars.User.DeptCode) + " and vital_code=" + SqlManager.SqlConvert(strVital));
                        if (drVital.Length == 0)
                        {
                            DataRow drEditVital = null;
                            drEditVital = dsVital.Tables[0].NewRow();
                            drEditVital["ward_code"] = GVars.User.DeptCode;
                            drEditVital["vital_code"] = strVital;
                            drEditVital["vital_signs"] = cbxVital.GetItemText(i);
                            dsVital.Tables[0].Rows.Add(drEditVital);
                        }
                    }
                    else
                    {
                        string strVital = this.cbxVital.GetItemValue(i).ToString();
                        DataRow[] drVital = dsVital.Tables[0].Select("ward_code=" + SqlManager.SqlConvert(GVars.User.DeptCode) + " and vital_code=" + SqlManager.SqlConvert(strVital));
                        if (drVital.Length > 0)
                        {
                            drVital[0].Delete();
                        }
                    }
                }
                com.SaveConfigList(ref dsVital, "nursing_record_print_skip_flg");
                #endregion
                com.SaveConfigList(ref ds_recordType, "nursing_record_type");
                ds_recordType = com.GetConfigList("nursing_record_type", "");
                ds_recordType.Tables[0].DefaultView.Sort = "type_id";
                //this.dgv_type.DataSource = ds_recordType.Tables[0].DefaultView;
                ucGvType.DataSource = ds_recordType.Tables[0].DefaultView;
                ucGvType.SelectRow("TYPE_ID", moduleCode);
                //locate(moduleCode, dgv_type);

                this.btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        /// 将获取的选项转换为串
        /// </summary>
        /// <param name="dtName"></param>
        /// <param name="clbName"></param>
        /// <returns></returns>
        private string ReturnCheckStr(DataTable dtName, CheckedListBoxControl clbName)
        {
            StringBuilder sb_dept_code = new StringBuilder();
            for (int i = 0; i < dtName.Rows.Count; i++)
            {
                if (clbName.GetItemChecked(i))
                {
                    string ss = clbName.GetItemValue(i).ToString();
                    sb_dept_code.Append(ss.Substring(ss.IndexOf(',') + 1) + ",");
                }
            }
            //去掉最后一个逗号
            string dept_codeStr = string.Empty;
            if (sb_dept_code.ToString().Length != 0)
            {
                dept_codeStr = sb_dept_code.ToString().Substring(0, sb_dept_code.ToString().Length - 1);
            }
            return dept_codeStr;
        }
        /// <summary>
        /// 检测控件输入的值是否合法
        /// </summary>
        /// <returns></returns>
        private bool chkDisp()
        {
            if (txtModuleCode.Text.Trim().Length == 0)
            {
                return false;
            }

            if (txtModuleName.Text.Trim().Length == 0)
            {
                txtModuleName.Focus();
                return false;
            }

            if (txt_width.Text.Trim().Length == 0)
            {
                txt_width.Focus();
                return false;
            }
            if (cb_storeCol.Text.Trim().Length == 0)
            {
                cb_storeCol.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        ///  将修改或新增的数据暂时保存到表中
        /// </summary>
        /// <returns></returns>
        private bool saveDisp()
        {
            string moduleCode = this.txtModuleCode.Text.Trim();
            DataRow[] drFind = dsUpdate.Tables[0].Select("type_id = " + SqlManager.SqlConvert(moduleCode) + " and COL_ID=" + SqlManager.SqlConvert(this.txtNumber.Text.Trim()));
            DataRow drEdit = null;

            #region 写入nursing_record_define
            if (drFind.Length == 0)
            {
                this.txtNumber.Text = com.ReturnMaxTypeId(txtModuleCode.Text);
                drEdit = dsUpdate.Tables[0].NewRow();
            }
            else
            {
                drEdit = drFind[0];
            }
            drEdit["TYPE_ID"] = this.txtModuleCode.Text.Trim();
            drEdit["COL_ID"] = this.txtNumber.Text.Trim();
            drEdit["COL_TITLE"] = this.txtTitle.Text.Trim();

            drEdit["COL_TYPE"] = this.cbType.SelectedIndex;//0为空

            drEdit["VITAL_CODE"] = this.txtVital_code.Text.Trim();
            if (this.cbDictCode.Text.Trim().Length != 0)
            {
                drEdit["DICT_ID"] = this.cbDictCode.SelectedValue.ToString();
            }
            else
            {
                drEdit["DICT_ID"] = "0";
            }

            if (this.cb_selectValue.SelectedIndex != 0)
            {
                drEdit["SELECTED_VALUE"] = this.cb_selectValue.SelectedIndex - 1;//0为空
            }
            else
            {
                drEdit["SELECTED_VALUE"] = 0;
            }

            drEdit["MULTI_VALUE"] = this.cb_MULTI_VALUE.SelectedIndex;//0为空
            drEdit["COL_WIDTH"] = Convert.ToDecimal(this.txt_width.Text.Trim());

            drEdit["MULTI_LINE"] = this.cb_MULTI_LINE.SelectedIndex;

            drEdit["SIGN_FLAG"] = this.cb_SIGN_FLAG.SelectedIndex;
            if (this.txtserial_no.Text.Trim().Length != 0)
            {
                drEdit["SERIAL_NO"] = Convert.ToDecimal(this.txtserial_no.Text.Trim());
            }
            drEdit["STORE_COL_NAME"] = this.cb_storeCol.SelectedValue.ToString();
            if (drFind.Length == 0)
            {
                dsUpdate.Tables[0].Rows.Add(drEdit);
            }
            #endregion

            return true;
        }

        private void txtItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                this.btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void txtDifin_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                this.bt_saveConfig.Enabled = true;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void bt_addConfig_Click(object sender, EventArgs e)
        {
            try
            {
                initDisp_ModuleDetail();
                this.txtTitle.Focus();
                this.btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void bt_delConfig_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtModuleCode.Text.Trim().Length == 0)
                {
                    return;
                }

                if (GVars.Msg.Show("Q0004", "模块") != DialogResult.Yes)       // 您确认要删除当前{0}吗?
                {
                    return;
                }

                string moduleCode = txtModuleCode.Text.Trim();
                string filter = "type_id = " + SqlManager.SqlConvert(moduleCode) + " and col_id=" + SqlManager.SqlConvert(txtNumber.Text);
                DataRow[] drFind = dsUpdate.Tables[0].Select(filter);

                if (drFind.Length > 0)
                {
                    drFind[0].Delete();
                    com.SaveConfigList(ref dsUpdate, "nursing_record_define");
                    dsUpdate = com.GetConfigList("nursing_record_define", "");
                    //dgvModule.DataSource = dsUpdate.Tables[0].DefaultView.Sort = "SERIAL_NO";
                    dsUpdate.Tables[0].DefaultView.RowFilter =
                        "type_id=" + SqlManager.SqlConvert(this.txtModuleCode.Text.Trim());
                    dsUpdate.Tables[0].DefaultView.Sort = "SERIAL_NO";
                    ucGridView1.DataSource = dsUpdate.Tables[0].DefaultView;
                }
                //当nursing_record_define中没有该记录单的项目时将该记录单也删除
                //DataSet ds = com.GetConfigList("nursing_record_define", "");
                //DataRow[] drFind_exist = ds.Tables[0].Select("type_id=" + SqlManager.SqlConvert(moduleCode));
                //DataRow[] dr_type = ds_recordType.Tables[0].Select("type_id=" + SqlManager.SqlConvert(moduleCode));
                //if (drFind_exist.Length == 0)
                //{
                //    dr_type[0].Delete();
                //    com.SaveConfigList(ref ds_recordType, "nursing_record_type");
                //    // 刷新显示
                //    dsUpdate = com.GetConfigList("nursing_record_define", "");
                //    dgvModule.DataSource = dsUpdate.Tables[0].DefaultView.Sort = "SERIAL_NO";
                //}

                this.btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void bt_saveConfig_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkDisp() == false)
                {
                    return;
                }

                string moduleCode = txtModuleCode.Text.Trim();

                // 保存数据到缓存
                saveDisp();
                string colId = txtNumber.Text.Trim();

                // 保存数据到DB中
                com.SaveConfigList(ref dsUpdate, "nursing_record_define");

                // 刷新显示
                dsUpdate = com.GetConfigList("nursing_record_define", "type_id=" + SqlManager.SqlConvert(this.txtModuleCode.Text));
                dsUpdate.Tables[0].DefaultView.Sort = "SERIAL_NO";

                //dgvModule.DataSource = dsUpdate.Tables[0].DefaultView;
                ucGridView1.DataSource = dsUpdate.Tables[0].DefaultView;
                ucGridView1.SelectRow("COL_ID", colId);

                this.btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void ucGvType_SelectionChanged(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;
            try
            {
                GVars.App.UserInput = false;

                this.txtModuleCode.Text = string.Empty;
                this.txtModuleName.Text = string.Empty;
                this.txtRowCount.Text = string.Empty;

                cbxDeptCode.UnCheckAll();
                cbxDeptCode.UnSelectAll();

                cbxVital.UnCheckAll();
                cbxVital.UnSelectAll();

                DataRow drApp = ucGvType.SelectedRow;
                this.txtModuleCode.Text = drApp["type_id"].ToString();                          // 模块编码
                this.txtModuleName.Text = drApp["descriptions"].ToString();
                this.txtRowCount.Text = drApp["ROW_COUNT"].ToString();
                this.txtRecordType.Text = drApp["descriptions"].ToString();
                this.btnSave.Enabled = false;
                dsUpdate = com.GetConfigList("nursing_record_define", " type_id=" + SqlManager.SqlConvert(this.txtModuleCode.Text));
                dsUpdate.Tables[0].DefaultView.Sort = "SERIAL_NO";
                ucGridView1.DataSource = dsUpdate.Tables[0].DefaultView;

                DataRow[] dr_code = ds_recordType.Tables[0].Select("type_id=" + SqlManager.SqlConvert(txtModuleCode.Text));
                if (dr_code[0]["DEPT_CODE"].ToString().Trim().Length != 0)
                {
                    string[] strList = dr_code[0]["DEPT_CODE"].ToString().Split(',');
                    foreach (string code in strList)
                    {
                        for (int i = 0; i < dt_xCb.Rows.Count; i++)
                        {
                            cbxDeptCode.SetItemChecked(i, dt_xCb.Rows[i]["DEPT_CODE"].ToString().Equals(code));
                        }
                    }
                }

                DataRow[] dr_vital = dsVital.Tables[0].Select("ward_code=" + SqlManager.SqlConvert(GVars.User.DeptCode));
                DataSet ds_item = com.GetConfigList("nursing_item_dict", "attribute=4 and ward_code=" + SqlManager.SqlConvert(GVars.User.DeptCode));
                for (int i = 0; i < dr_vital.Length; i++)
                {
                    for (int j = 0; j < ds_item.Tables[0].Rows.Count; j++)
                    {
                        if (dr_vital[i]["vital_code"].ToString().Equals(ds_item.Tables[0].Rows[j]["vital_code"].ToString()))
                        {
                            this.cbxVital.SetItemChecked(i, true);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }

        private void cbxDeptCode_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (GVars.App.UserInput)
                btnSave.Enabled = true;
        }

        private void cbxVital_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (GVars.App.UserInput)
                btnSave.Enabled = true;
        }

    }
}
