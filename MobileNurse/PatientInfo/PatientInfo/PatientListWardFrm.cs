using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace HISPlus
{
    public partial class PatientListWardFrm : FormDo
    {
        #region 变量
        public event PatientChangedEventHandler PatientChanged;                              // 定义事件

        protected string _deptCode = string.Empty;

        protected string _patientId = string.Empty;
        protected string _visitId = string.Empty;
        /// <summary>
        /// 病人姓名
        /// </summary>
        public string PatientName = string.Empty;
        protected bool _showWaitInpPatient = false;                                        // 是否显示待入科病人
        protected bool _showOutHospital3Days = false;

        private PatientDbI patientDbI = null;
        private HospitalDbI hospitalDbI = null;
        public DataSet DsPatient = null;
        public DataSet DsWardList = null;


        /// <summary>
        /// 右键菜单图片列表
        /// </summary>
        private ImageList _menuImageLists;

        private IList<MenuManager> _listMenus;

        /// <summary>
        /// 入院日期
        /// </summary>
        public DateTime InpDateTime = DataType.DateTime_Null();

        

        #endregion


        public PatientListWardFrm( )
        {
            InitializeComponent();
        }


        #region 属性
        public string DeptCode
        {
            get { return _deptCode; }
            set { _deptCode = value; }
        }


        public string PatientId
        {
            get { return _patientId; }
        }


        public string VisitId
        {
            get { return _visitId; }
        }


        /// <summary>
        /// 属性[显示待入科病人]
        /// </summary>
        public bool ShowWaitInpPatient
        {
            get
            {
                return _showWaitInpPatient;
            }
            set
            {
                _showWaitInpPatient = value;
            }
        }

        /// <summary>
        /// 属性[显示三天之内的病人]
        /// </summary>
        public bool ShowOutHospital3Days
        {
            get
            {
                return _showOutHospital3Days;
            }
            set
            {
                _showOutHospital3Days = value;
            }
        }

        #endregion

        #region 窗体事件
        private void PatientListWardFrm_Load(object sender, EventArgs e)
        {
            try
            {
                ReloadData();
                
                ucGridView1.EnableOddEvenRow = true;
                //ucGridView1.Add("床号", "BED_NO",false);
                ucGridView1.Add("床标", "BED_LABEL", 30);
                ucGridView1.Add("姓名", "NAME");
                ucGridView1.ShowFindPanel = true;
                ucGridView1.KeyUpOnGrid += gridView1_KeyUp;
                ucGridView1.SelectionChanged += ucGridView1_SelectionChanged;
                ucGridView1.Init();

                //ucGridView1.GetSelectCellValue(

                _listMenus = EntityOper.GetInstance().FindByProperty<MenuManager>("Enabled", (byte)1);
                //InitDoc();
                //InitPopupMenu();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        //private void InitDoc()
        //{
        //    IList<DocTemplateClass> listTemplateClass = new DocTemplateClassDAL().LoadAll();
        //    IList<DocTemplate> listElements = new DocTemplateDAL().LoadAll();

        //    IList<DocTemplateDept> listDepts = new DocTemplateDeptDAL().FindByProperty("DeptCode", GVars.User.DeptCode);

        //    listElements =
        //        listElements.Where(p => p.IsGlobal == 1 || listDepts.Any(q => q.DocTemplate.Id == p.Id)).ToList();

        //    if (listElements.Count == 0) return;

        //    foreach (DocTemplateClass templateClass in listTemplateClass.OrderBy(p => p.ParentId).ThenBy(p => p.SortId))
        //    {
        //        string nodeId = string.Format("{0}{1}{2}", ConnMenu.DocMenuId, ComConst.STR.UnderLine, templateClass.Id);
        //        string parentNodeId = string.Format("{0}{1}{2}", ConnMenu.DocMenuId, ComConst.STR.UnderLine, templateClass.ParentId);

        //        MenuManager menuManager = new MenuManager
        //        {
        //            Id = nodeId,
        //            Name = templateClass.Name,
        //            Enabled = 1,
        //            ParentId = templateClass.ParentId == 0 ? ConnMenu.DocMenuId : parentNodeId
        //        };

        //        DocTemplateClass @class = templateClass;

        //        IList<DocTemplate> docTemplates = listElements.Where(p => p.DocTemplateClass.Id == @class.Id).ToList();

        //        if (docTemplates.Any())
        //            _listMenus.Add(menuManager);
        //        else
        //            return;

        //        foreach (DocTemplate template in docTemplates)
        //        {
        //            string templateId = string.Format("{0}{1}{2}", nodeId, ComConst.STR.UnderLine, template.Id);

        //            menuManager = new MenuManager
        //            {
        //                Id = templateId,
        //                Name = template.TemplateName,
        //                ParentId = nodeId,
        //                Enabled = 1
        //            };

        //            _listMenus.Add(menuManager);
        //        }
        //    }
        //}

        //private void InitPopupMenu()
        //{
        //    popupMenu.ItemLinks.Clear();
        //    barManager1.Items.Clear();

        //    IList<UserCardMenu> listUserCardMenus = EntityOper.GetInstance().FindByProperty<UserCardMenu>("UserId", GVars.User.ID);

        //    listUserCardMenus = listUserCardMenus.OrderBy(p => p.ParentMenuId).ThenBy(p => p.SortId).ToList();

        //    const string filter = "MODULE_CODE = '{0}'";

        //    if (_menuImageLists == null)
        //        _menuImageLists = new ImageList();
        //    barManager1.Images = _menuImageLists;
        //    foreach (UserCardMenu model in listUserCardMenus)
        //    {
        //        DataRow[] drFind = GVars.User.Rights.Tables[0].Select(string.Format(filter, model.MenuId));

        //        MenuManager menuManager = _listMenus.FirstOrDefault(p => p.Id == model.MenuId);
        //        if (menuManager == null) continue;

        //        model.Name = menuManager.Name;
        //        model.IconPath = menuManager.IconPath;

        //        if (model.ParentMenuId == "0")
        //        {
        //            // 判断是否包含子项
        //            BarItem bar;
        //            if (listUserCardMenus.Count(p => p.ParentMenuId == model.MenuId) > 0)
        //            {
        //                bar = new BarSubItem();
        //            }
        //            else
        //            {
        //                bar = new BarButtonItem();
        //                bar.ItemClick += new ItemClickEventHandler(bar_ItemClick);
        //            }

        //            bar.Caption = model.Name;
        //            bar.Tag = model.MenuId;

        //            Image image =DirFile.GetImageFile(model.IconPath);
        //            if (image != null)
        //            {
        //                _menuImageLists.Images.Add(image);
        //                bar.ImageIndex = _menuImageLists.Images.Count - 1;
        //            }

        //            popupMenu.ItemLinks.Add(bar);
        //            barManager1.Items.Add(bar);
        //        }
        //        else
        //        {
        //            BarButtonItem buttonItemChild = new BarButtonItem();
        //            buttonItemChild.Caption = model.Name;
        //            buttonItemChild.Tag = model.MenuId;
        //            buttonItemChild.ItemClick += bar_ItemClick;
        //            Image image =DirFile.GetImageFile(model.IconPath);
        //            if (image != null)
        //            {
        //                _menuImageLists.Images.Add(image);
        //                buttonItemChild.ImageIndex = _menuImageLists.Images.Count - 1;
        //            }
        //            bool hasParent = false;
        //            foreach (LinkPersistInfo item in popupMenu.LinksPersistInfo)
        //            {
        //                BarSubItem subItem = item.Item as BarSubItem;
        //                if (subItem == null) continue;
        //                if (Convert.ToString(subItem.Tag) != model.ParentMenuId) continue;
        //                //subItem.LinksPersistInfo.Add(itemChild);
        //                subItem.ItemLinks.Add(buttonItemChild);
        //                barManager1.Items.Add(buttonItemChild);
        //                hasParent = true;
        //                break;
        //            }
        //            if (hasParent == false)
        //            {
        //                popupMenu.ItemLinks.Add(buttonItemChild);
        //                barManager1.Items.Add(buttonItemChild);
        //            }
        //        }
        //    }
        //}


        /// <summary>
        /// 右键菜单项单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //void bar_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    try
        //    {
        //        base.LoadingShow();

        //        BarButtonItem button = e.Item as BarButtonItem;
        //        if (button == null) return;

        //        string nodeId = button.Tag as string;
        //        if (string.IsNullOrEmpty(nodeId)) return;

        //        // 护理评估单
        //        if (nodeId.Contains(ComConst.STR.UnderLine))
        //        {
        //            string templateId = nodeId.Substring(nodeId.LastIndexOf(ComConst.STR.UnderLine) + 1);
        //            if (string.IsNullOrEmpty(templateId)) return;

        //            MdiFrm.GetInstance().CreateDocForm(Convert.ToDecimal(templateId), button.Caption);
        //        }
        //        else
        //        {
        //            MenuManager model = _listMenus.FirstOrDefault(p => p.Id == nodeId);
        //            if (model == null) return;

        //            if (model.Assembly == null || model.FormName == null)
        //            {
        //                XtraMessageBox.Show("该菜单没有界面,请检查自定义配置！", "提示", MessageBoxButtons.OK,
        //                   MessageBoxIcon.Information);
        //                return;
        //            }

        //            Form form = MdiFrm.GetFormInDll(model.Assembly, model.FormName);
        //            if (form != null)
        //            {
        //                MdiFrm.GetInstance().CreateTabPage(form, model, model.ConnectionPatient);
        //            }
        //            else
        //            {
        //                XtraMessageBox.Show("窗口数据配置错误,请联系系统管理员！", "系统提示", MessageBoxButtons.OK,
        //                    MessageBoxIcon.Information);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Error.ErrProc(ex);
        //    }
        //    finally
        //    {
        //        base.LoadingClose();
        //    }
        //}

        void ucGridView1_SelectionChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                string patientId = Convert.ToString(ucGridView1.GetSelectCellValue("PATIENT_ID"));

                if (!patientId.Equals(_patientId))
                {
                    _patientId = patientId;
                    PatientName = Convert.ToString(ucGridView1.GetSelectCellValue("NAME"));
                    _visitId = Convert.ToString(ucGridView1.GetSelectCellValue("VISIT_ID"));
                    if (PatientChanged != null)
                    {
                        PatientChanged(this, new PatientEventArgs(_patientId, _visitId));
                    }

                    lblSex.Text = Convert.ToString(ucGridView1.GetSelectCellValue("SEX"));
                    lblBedNo.Text = Convert.ToString(ucGridView1.GetSelectCellValue("BED_NO"));
                    lblPatientId.Text = Convert.ToString(ucGridView1.GetSelectCellValue("PATIENT_ID"));
                    if (ucGridView1.GetSelectCellValue("DATE_OF_BIRTH").ToString().Length > 0)
                    {
                        this.lblAge.Text = PersonCls.GetAge((DateTime)ucGridView1.GetSelectCellValue("DATE_OF_BIRTH"), DateTime.Now);
                    }
                    else
                    {
                        this.lblAge.Text = string.Empty;
                    }
                    lblInpDate.Text = DataType.GetDateTimeShort(ucGridView1.GetSelectCellValue("ADMISSION_DATE_TIME").ToString());   // 入院日期

                    InpDateTime = (DateTime)ucGridView1.GetSelectCellValue("ADMISSION_DATE_TIME");

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
        /// 病人列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            dgv.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        /// <summary>
        /// 菜单[刷新]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmnuRefresh_Click(object sender, EventArgs e)
        {
            lookUpEdit1_EditValueChanged(null, null);
        }
        #endregion

        #region 共通函数
        private void initFrmVal()
        {
            patientDbI = new PatientDbI(GVars.OracleAccess);
            hospitalDbI = new HospitalDbI(GVars.OracleAccess);

            //dsPatient = patientDbI.GetWardPatientList(_deptCode);
            DsWardList = hospitalDbI.Get_WardList_Nurse();

            if (_showWaitInpPatient == true)
            {
                DataRow drNew = DsWardList.Tables[0].NewRow();

                drNew["DEPT_CODE"] = "-1";
                drNew["DEPT_NAME"] = "待入科";

                DsWardList.Tables[0].Rows.Add(drNew);
            }

            //if (_showOutHospital3Days == true)
            //{
            //    DataRow drNew = DsWardList.Tables[0].NewRow();

            //    drNew["DEPT_CODE"] = "-2";
            //    drNew["DEPT_NAME"] = "已出院(三天)";

            //    DsWardList.Tables[0].Rows.Add(drNew);
            //}
        }

        private void InitDisp1()
        {
            if (DsWardList == null) return;

            lookUpEdit1.Properties.ValueMember = "DEPT_CODE";// 实际要用的字段;   //相当于editvalue
            lookUpEdit1.Properties.DisplayMember = "DEPT_NAME"; //要显示的字段;    //相当于text
            lookUpEdit1.Properties.DataSource = DsWardList.Tables[0].DefaultView;

            LookUpColumnInfo col = new LookUpColumnInfo("DEPT_NAME", "选择科室");       // 定义列信息 对应的字段名称及字段表头即Caption                     
            lookUpEdit1.Properties.Columns.Add(col);                  // 向 LookUpEdit 中添加列

            string filter = string.Empty;
            _deptCode = GVars.User.DeptCode;
            if (_deptCode.Length > 0)
            {
                filter = "DEPT_CODE = " + SqlManager.SqlConvert(_deptCode);
                lookUpEdit1.Enabled = false;
            }
            else
            {
                filter = "DEPT_CODE = " + SqlManager.SqlConvert("-1");
                lookUpEdit1.Enabled = true;
            }
            if (DsWardList.Tables[0].Select(filter).Length > 0)
            {
                lookUpEdit1.EditValue = _deptCode;
            }
            else if (_deptCode.Length > 0)
                lookUpEdit1.Properties.NullText = @"无效科室";
            else
                // 选中第一项
                lookUpEdit1.ItemIndex = 0;
        }
        #endregion

        #region 接口
        /// <summary>
        /// 定位病人
        /// </summary>
        public void LocatePatient(string patientId)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;
                if (patientId.Equals(_patientId) == true)
                {
                    return;
                }
                ucGridView1.SelectRow("PATIENT_ID", patientId);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }

        /// <summary>
        /// 重新加载数据
        /// </summary>
        public void ReloadData()
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;

                initFrmVal();

                InitDisp1();

                GVars.App.UserInput = true;
                lookUpEdit1_EditValueChanged(null, null);
            }
            finally
            {
                GVars.App.UserInput = blnStore;
            }
        }


        /// <summary>
        /// 重新加载病人列表
        /// </summary>
        public void ReloadPatientList()
        {
            lookUpEdit1_EditValueChanged(null, null);
        }
        #endregion


        /// <summary>
        /// 病区改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVars.App.UserInput == false)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                if (lookUpEdit1.EditValue != null && !string.IsNullOrEmpty(lookUpEdit1.EditValue.ToString()))
                {
                    _deptCode = lookUpEdit1.EditValue.ToString();
                }

                DsPatient = patientDbI.GetWardPatientList(_deptCode);
                DsPatient.Tables[0].DefaultView.Sort = "BED_NO";

                ucGridView1.ClearSelection();
                ucGridView1.DataSource = DsPatient.Tables[0].DefaultView;

                if (ucGridView1.SelectRowsCount > 0)
                {
                    ucGridView1_SelectionChanged(null, null);
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        /// 病人列表按键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F5)
                {
                    ucGridView1.SelectFirstRow();
                    lookUpEdit1_EditValueChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void lblPatientId_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(lblPatientId.Text, true);
        }
    }
}
