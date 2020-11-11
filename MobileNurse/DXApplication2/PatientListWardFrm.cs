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
using HISPlus;
using SQL = HISPlus.SqlManager;
using DevExpress.XtraGrid.Views.Grid;

using DocDAL;
namespace DXApplication2
{
    public struct DOC_STATE
    {
        /// <summary>
        /// 不达标
        /// </summary>
        public const string NOT_STANDARD = "1";
        //public Color NOT_STANDARD_FONTCOLOR = Color.Red;
        /// <summary>
        /// 达标
        /// </summary>
        public const string STANDARD = "0";
        // public Color STANDARD_FONTCOLOR = Color.Black;


    }
    public struct REC_STATE
    {
        /// <summary>
        /// 未审核、待审核、通过、未通过
        /// </summary>
        public const string NO_CHECK = "0";
        //public Color NO_CHECK_FONTCOLOR = Color.Black;
        /// <summary>
        /// 待审核
        /// </summary>
        public const string WAIT_CHECK = "1";
        //public Color WAIT_CHECK_COLOR = Color.Yellow;
        /// <summary>
        /// 通过
        /// </summary>
        public const string PASS = "2";
        //public Color PASS_COLOR = Color.Green;
        /// <summary>
        /// 未通过
        /// </summary>
        public const string NOT_PASS = "3";
        //public Color NOT_PASS_COLOR = Color.Red;
    }
    public partial class PatientListWardFrm : FormDo
    {
        #region 变量
        public event PatientChangedEventHandler PatientChanged;                              // 定义事件
        public event PatientListRefreshEventHandler PatientRefresh;//2015.11.25 病人列表刷新事件

        protected string _deptCode = string.Empty;

        protected string _patientId = string.Empty;
        protected string _visitId = string.Empty;
        protected string _state = string.Empty;
        /// <summary>
        /// 病人姓名
        /// </summary>
        public string PatientName = string.Empty;
        protected bool _showWaitInpPatient = false;                                        // 是否显示待入科病人
        protected bool _showOutHospital3Days = false;

        private PatientDbI patientDbI = null;
        private HospitalDbI hospitalDbI = null;
        private DocNursingDbi docNursingDbI = null;
        public DataSet DsPatient = null;
        public DataSet DsWardList = null;
        public DataSet DsDocNotStandPat = null;

        /// <summary>
        /// DOC_TEMPLATE表开启预警的TEMPLATE_ID
        /// </summary>
        public DataSet DsDocTemplateAlarm = null;

        /// <summary>
        /// 右键菜单图片列表
        /// </summary>
        private ImageList _menuImageLists;

        private IList<MenuManager> _listMenus;

        /// <summary>
        /// 入院日期
        /// </summary>
        public DateTime InpDateTime = DataType.DateTime_Null();

        /// <summary>
        /// 查询病人窗体
        /// </summary>
        QueryPatFrm queryPat = null;

        private bool _isRefresh = false;

        #endregion


        public PatientListWardFrm()
        {
            InitializeComponent();

            ucGridView1.KeyUpOnGrid += gridView1_KeyUp;
            ucGridView1.SelectionChanged += ucGridView1_SelectionChanged;
            ucGridView1.RowStyle += gvDefault_RowStyle;
            //ucGridView1.RowCellStyle += gvDefault_RowCellStyle; 2016.01.11屏蔽，因为实现代码已屏蔽。改HISCOMM版本和L版本不一致
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

        public string STATE
        {
            get { return _state; }
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
                queryPat = new QueryPatFrm();

                ReloadData();

                //ucGridView1.Add("床号", "BED_NO",false);
                ucGridView1.Add("床标", "BED_LABEL", 35);
                ucGridView1.Add("姓名", "NAME", 65);
                ucGridView1.Add("状态", "STATE", 40);//病人在院状态，由在院、出院、带入科
                ucGridView1.Add("级别", "NURSING_CLASS_NAME", 50);//ABBREVIATION
                ucGridView1.Add("评估单状态", "DOC_STATE", false);
                ucGridView1.Add("记录单状态", "REC_STATE", false);
                ucGridView1.ShowFindPanel = true;//2015.12.07 true->false --->del
                ucGridView1.ColumnsEvenOldRowColor = "STATE";//,DOC_STATE,REC_STATE";
                ucGridView1.Init(); //2015.12.07 

                _listMenus = EntityOper.GetInstance().FindByProperty<MenuManager>("Enabled", (byte)1);
                InitDoc();
                InitPopupMenu();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void InitDoc()
        {
            IList<DocTemplateClass> listTemplateClass = EntityOper.GetInstance().LoadAll<DocTemplateClass>();
            IList<DocTemplate> listElements = EntityOper.GetInstance().LoadAll<DocTemplate>();

            IList<DocTemplateDept> listDepts = EntityOper.GetInstance().FindByProperty<DocTemplateDept>("DeptCode", GVars.User.DeptCode);

            listElements =
                listElements.Where(p => p.IsGlobal == 1 || listDepts.Any(q => q.DocTemplate.Id == p.Id)).ToList();

            if (listElements.Count == 0) return;

            foreach (DocTemplateClass templateClass in listTemplateClass.OrderBy(p => p.ParentId).ThenBy(p => p.SortId))
            {
                string nodeId = string.Format("{0}{1}{2}", ConnMenu.DocMenuId, ComConst.STR.UnderLine, templateClass.Id);
                string parentNodeId = string.Format("{0}{1}{2}", ConnMenu.DocMenuId, ComConst.STR.UnderLine, templateClass.ParentId);

                MenuManager menuManager = new MenuManager
                {
                    Id = nodeId,
                    Name = templateClass.Name,
                    Enabled = 1,
                    ParentId = templateClass.ParentId == 0 ? ConnMenu.DocMenuId : parentNodeId
                };

                DocTemplateClass @class = templateClass;

                IList<DocTemplate> docTemplates = listElements.Where(p => p.DocTemplateClass.Id == @class.Id).ToList();

                if (docTemplates.Any())
                    _listMenus.Add(menuManager);
                else
                    return;

                foreach (DocTemplate template in docTemplates)
                {
                    string templateId = string.Format("{0}{1}{2}", nodeId, ComConst.STR.UnderLine, template.Id);

                    menuManager = new MenuManager
                    {
                        Id = templateId,
                        Name = template.TemplateName,
                        ParentId = nodeId,
                        Enabled = 1
                    };

                    _listMenus.Add(menuManager);
                }
            }
        }

        private void InitPopupMenu()
        {
            popupMenu.ItemLinks.Clear();
            barManager1.Items.Clear();

            IList<UserCardMenu> listUserCardMenus = EntityOper.GetInstance().FindByProperty<UserCardMenu>("UserId", GVars.User.ID);

            listUserCardMenus = listUserCardMenus.OrderBy(p => p.ParentMenuId).ThenBy(p => p.SortId).ToList();

            const string filter = "MODULE_CODE = '{0}'";

            if (_menuImageLists == null)
                _menuImageLists = new ImageList();
            barManager1.Images = _menuImageLists;
            foreach (UserCardMenu model in listUserCardMenus)
            {
                DataRow[] drFind = GVars.User.Rights.Tables[0].Select(string.Format(filter, model.MenuId));

                MenuManager menuManager = _listMenus.FirstOrDefault(p => p.Id == model.MenuId);
                if (menuManager == null) continue;

                model.Name = menuManager.Name;
                model.IconPath = menuManager.IconPath;

                if (model.ParentMenuId == "0")
                {
                    // 判断是否包含子项
                    BarItem bar;
                    if (listUserCardMenus.Count(p => p.ParentMenuId == model.MenuId) > 0)
                    {
                        bar = new BarSubItem();
                    }
                    else
                    {
                        bar = new BarButtonItem();
                        bar.ItemClick += new ItemClickEventHandler(bar_ItemClick);
                    }

                    bar.Caption = model.Name;
                    bar.Tag = model.MenuId;

                    Image image = DirFile.GetImageFile(model.IconPath);
                    if (image != null)
                    {
                        _menuImageLists.Images.Add(image);
                        bar.ImageIndex = _menuImageLists.Images.Count - 1;
                    }

                    popupMenu.ItemLinks.Add(bar);
                    barManager1.Items.Add(bar);
                }
                else
                {
                    BarButtonItem buttonItemChild = new BarButtonItem();
                    buttonItemChild.Caption = model.Name;
                    buttonItemChild.Tag = model.MenuId;
                    buttonItemChild.ItemClick += bar_ItemClick;
                    Image image = DirFile.GetImageFile(model.IconPath);
                    if (image != null)
                    {
                        _menuImageLists.Images.Add(image);
                        buttonItemChild.ImageIndex = _menuImageLists.Images.Count - 1;
                    }
                    bool hasParent = false;
                    foreach (LinkPersistInfo item in popupMenu.LinksPersistInfo)
                    {
                        BarSubItem subItem = item.Item as BarSubItem;
                        if (subItem == null) continue;
                        if (Convert.ToString(subItem.Tag) != model.ParentMenuId) continue;
                        //subItem.LinksPersistInfo.Add(itemChild);
                        subItem.ItemLinks.Add(buttonItemChild);
                        barManager1.Items.Add(buttonItemChild);
                        hasParent = true;
                        break;
                    }
                    if (hasParent == false)
                    {
                        popupMenu.ItemLinks.Add(buttonItemChild);
                        barManager1.Items.Add(buttonItemChild);
                    }
                }
            }
        }


        /// <summary>
        /// 右键菜单项单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bar_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                base.LoadingShow();

                BarButtonItem button = e.Item as BarButtonItem;
                if (button == null) return;

                string nodeId = button.Tag as string;
                if (string.IsNullOrEmpty(nodeId)) return;

                // 护理评估单
                if (nodeId.Contains(ComConst.STR.UnderLine))
                {
                    string templateId = nodeId.Substring(nodeId.LastIndexOf(ComConst.STR.UnderLine) + 1);
                    if (string.IsNullOrEmpty(templateId)) return;

                    MdiFrm.GetInstance().CreateDocForm(Convert.ToDecimal(templateId), button.Caption);
                }
                else
                {
                    MenuManager model = _listMenus.FirstOrDefault(p => p.Id == nodeId);
                    if (model == null) return;

                    if (model.Assembly == null || model.FormName == null)
                    {
                        XtraMessageBox.Show("该菜单没有界面,请检查自定义配置！", "提示", MessageBoxButtons.OK,
                           MessageBoxIcon.Information);
                        return;
                    }

                    Form form = MdiFrm.GetFormInDll(model.Assembly, model.FormName);
                    if (form != null)
                    {
                        MdiFrm.GetInstance().CreateTabPage(form, model, model.ConnectionPatient);
                    }
                    else
                    {
                        XtraMessageBox.Show("窗口数据配置错误,请联系系统管理员！", "系统提示", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                base.LoadingClose();
            }
        }

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
                    _state = Convert.ToString(ucGridView1.GetSelectCellValue("STATE"));

                    //临时 Patient实例应和Patient_Info一致
                    GVars.Patient.ID = _patientId;
                    GVars.Patient.VisitId = _visitId;
                    GVars.Patient.STATE = _state;
                    GVars.Patient.DeptCode = Convert.ToString(ucGridView1.GetSelectCellValue("DEPT_CODE"));//病区
                    GVars.Patient.DeptName = Convert.ToString(ucGridView1.GetSelectCellValue("DEPT_NAME"));
                    //

                    if (PatientChanged != null)
                    {
                        PatientChanged(this, new PatientEventArgs(_patientId, _visitId, _state));
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
            //lookUpEdit1_EditValueChanged(null, null);
            _isRefresh = true;
            cmbDeptList_SelectedIndexChanged(null, null);
        }
        #endregion

        #region 共通函数
        private void initFrmVal()
        {
            patientDbI = new PatientDbI(GVars.OracleAccess);
            hospitalDbI = new HospitalDbI(GVars.OracleAccess);
            docNursingDbI = new DocNursingDbi(GVars.OracleAccess);

            //dsPatient = patientDbI.GetWardPatientList(_deptCode);
            DsWardList = GVars.User.DsDepts;//2016.01.26 hospitalDbI.Get_WardList_Nurse();

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
            if (GVars.User.DsDepts == null) return;

            //LookUpColumnInfo col = new LookUpColumnInfo("DEPT_NAME", "选择科室");       // 定义列信息 对应的字段名称及字段表头即Caption                     
            //lookUpEdit1.Properties.Columns.Add(col);                  // 向 LookUpEdit 中添加列

            //lookUpEdit1.Properties.ValueMember = "DEPT_CODE";// 实际要用的字段;   //相当于editvalue
            //lookUpEdit1.Properties.DisplayMember = "DEPT_NAME"; //要显示的字段;    //相当于text
            //lookUpEdit1.Properties.DataSource = DsWardList.Tables[0].DefaultView;

            //lookUpEdit1.Properties.PopupWidth = 300;

            //测试
            cmbDeptList.ValueMember = "DEPT_CODE";
            cmbDeptList.DisplayMember = "DEPT_NAME";
            cmbDeptList.DataSource = DsWardList.Tables[0].DefaultView;
            cmbDeptList.SelectedIndex = 0;


            string filter = string.Empty;
            _deptCode = GVars.User.DeptCode;
            if (_deptCode.Length > 0)
            {
                filter = "DEPT_CODE = " + SqlManager.SqlConvert(_deptCode);
                //lookUpEdit1.Enabled = false;
            }
            else
            {
                filter = "DEPT_CODE = " + SqlManager.SqlConvert("-1");
                //lookUpEdit1.Enabled = true;
            }
            //lookUpEdit1.Enabled = true;

            //if (DsWardList.Tables[0].Select(filter).Length > 0)
            //{
            //    lookUpEdit1.EditValue = _deptCode;
            //}
            //else if (_deptCode.Length > 0)
            //    lookUpEdit1.Properties.NullText = @"无效科室";
            //else
            //    // 选中第一项
            //    lookUpEdit1.ItemIndex = 0;

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
                //lookUpEdit1_EditValueChanged(null, null);
                cmbDeptList_SelectedIndexChanged(null, null);
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
            //lookUpEdit1_EditValueChanged(null, null);
            cmbDeptList_SelectedIndexChanged(null, null);
        }
        #endregion

        /// <summary>
        /// 病区下拉框正在改变，如有标签页，不能改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void lookUpEdit1_EditValueChanging(object sender, ChangingEventArgs e)
        //{
        //    if (!MdiFrm.GetInstance().CanRefresh())
        //    {
        //        MessageBox.Show("在切换科室前，请先关闭除'病人卡片'外的所有标签页!", "提示");
        //        e.Cancel = true;
        //        return;
        //    }
        //}

        /// <summary>
        /// 病区改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        if (GVars.App.UserInput == false)
        //        {
        //            return;
        //        }


        //        if (!MdiFrm.GetInstance().CanRefresh())
        //        {
        //            MessageBox.Show("在切换科室前，请先关闭除'病人卡片'外的所有标签页!", "提示");

        //            return;
        //        }


        //        this.Cursor = Cursors.WaitCursor;
        //        if (lookUpEdit1.EditValue != null && !string.IsNullOrEmpty(lookUpEdit1.EditValue.ToString()))
        //        {
        //            _deptCode = lookUpEdit1.EditValue.ToString();
        //            //切换科室后，修改相关全局变量，并刷新界面，如：主框架的科室名称
        //            //if (sender is DevExpress.XtraEditors.LookUpEdit)
        //            //{
        //            GVars.User.DeptCode = _deptCode;
        //            GVars.User.DeptName = hospitalDbI.Get_DeptName(GVars.User.DeptCode);
        //            //}

        //        }

        //        if (cmbDeptList.Items.Count >0 && !string.IsNullOrEmpty(cmbDeptList.SelectedValue.ToString()))
        //        {
        //            _deptCode = cmbDeptList.SelectedValue.ToString();
        //            //切换科室后，修改相关全局变量，并刷新界面，如：主框架的科室名称.
        //            //if (sender is DevExpress.XtraEditors.LookUpEdit)
        //            //{
        //            GVars.User.DeptCode = _deptCode;
        //            GVars.User.DeptName = hospitalDbI.Get_DeptName(GVars.User.DeptCode);
        //            //}
        //        }

        //        //2015.12.04增加状态
        //        this.SetPatientState();

        //        DataColumn[] dtCol = new DataColumn[3];
        //        dtCol[0] = DsPatient.Tables[0].Columns["PATIENT_ID"];
        //        dtCol[1] = DsPatient.Tables[0].Columns["VISIT_ID"];
        //        dtCol[2] = DsPatient.Tables[0].Columns["STATE"];
        //        DsPatient.Tables[0].PrimaryKey = dtCol;
        //        DsPatient.Tables[0].DefaultView.Sort = "BED_NO";

        //        ucGridView1.ClearSelection();
        //        ucGridView1.DataSource = DsPatient.Tables[0].DefaultView;

        //        if (ucGridView1.SelectRowsCount > 0)
        //        {
        //            ucGridView1_SelectionChanged(null, null);
        //        }

        //        #region 2015.11.25测试事件，参数应替换
        //        PatientName = Convert.ToString(ucGridView1.GetSelectCellValue("NAME"));
        //        _visitId = Convert.ToString(ucGridView1.GetSelectCellValue("VISIT_ID"));
        //        if (PatientRefresh != null)
        //        {
        //            PatientRefresh(this, new PatientEventArgs(_patientId, _visitId));
        //        }
        //        #endregion

        //        this.Cursor = Cursors.Default;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Cursor = Cursors.Default;
        //        Error.ErrProc(ex);
        //    }
        //}

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
                    //lookUpEdit1_EditValueChanged(null, null);
                    cmbDeptList_SelectedIndexChanged(null, null);
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

        private void ucGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (ucGridView1.GetMouseRowHandle() >= 0)
                if (e.Button == MouseButtons.Right)
                {
                    Point p = Control.MousePosition;
                    p.X = Screen.PrimaryScreen.WorkingArea.Width - MdiFrm.GetInstance().Right - 200;
                    popupMenu.ShowPopup(p);

                    //当前的屏幕除任务栏外的工作域大小
                    //this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
                    //this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;

                    //当前的屏幕包括任务栏的工作域大小
                    //this.Width=System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                    //this.Height=System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;

                    //任务栏大小
                    //this.Width=System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width-System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
                    //this.Height=System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height-System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;

                    //winform实现全屏显示
                    //WinForm:
                    //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    //this.TopMost = true;  
                }
        }

        private void cmenuQueryPat_Click(object sender, EventArgs e)
        {
            DataRow drPatient;
            try
            {
                if (queryPat.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < queryPat.drPatient.Length; i++)
                    {
                        drPatient = DsPatient.Tables[0].NewRow();
                        drPatient["PATIENT_ID"] = queryPat.drPatient[i]["PATIENT_ID"];
                        drPatient["VISIT_ID"] = queryPat.drPatient[i]["VISIT_ID"];
                        drPatient["NAME"] = queryPat.drPatient[i]["NAME"];
                        drPatient["BED_LABEL"] = queryPat.drPatient[i]["BED_LABEL"];
                        drPatient["BED_NO"] = queryPat.drPatient[i]["BED_NO"];
                        drPatient["DATE_OF_BIRTH"] = queryPat.drPatient[i]["DATE_OF_BIRTH"];
                        drPatient["ADMISSION_DATE_TIME"] = queryPat.drPatient[i]["ADMISSION_DATE_TIME"];
                        drPatient["STATE"] = queryPat.drPatient[i]["STATE"];

                        drPatient["INP_NO"] = queryPat.drPatient[i]["INP_NO"];
                        drPatient["SEX"] = queryPat.drPatient[i]["SEX"];
                        drPatient["DIAGNOSIS"] = queryPat.drPatient[i]["DIAGNOSIS"];
                        drPatient["ADM_WARD_DATE_TIME"] = queryPat.drPatient[i]["ADM_WARD_DATE_TIME"];

                        //2016.01.27 add 主要用于把当前病人科室保存在全局中（原仅有护理单元），在转科后，评估单
                        //病人信息节点关联科室为病人转出前的科室。
                        drPatient["WARD_CODE"] = queryPat.drPatient[i]["WARD_CODE"];
                        drPatient["DEPT_CODE"] = queryPat.drPatient[i]["DEPT_CODE"];
                        drPatient["DEPT_NAME"] = queryPat.drPatient[i]["DEPT_NAME"];
                        DsPatient.Tables[0].Rows.Add(drPatient);
                    }
                }
                if (queryPat != null)
                {
                    queryPat.Close();
                    queryPat = new QueryPatFrm();
                }

            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        private void gvDefault_RowStyle(object sender, RowStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.RowHandle >= 0)
            {
                e.Appearance.BackColor = Color.White;
                string state = view.GetRowCellDisplayText(e.RowHandle, view.Columns["STATE"]);
                switch (state)
                {
                    case HISPlus.PAT_INHOS_STATE.OUT:
                        e.Appearance.ForeColor = Color.Gray;
                        break;
                    case HISPlus.PAT_INHOS_STATE.NEW:
                        e.Appearance.ForeColor = Color.Red;
                        break;
                    case HISPlus.PAT_INHOS_STATE.IN:
                        e.Appearance.ForeColor = Color.Black;
                        break;
                    case HISPlus.PAT_INHOS_STATE.TRANSFER:
                        e.Appearance.ForeColor = Color.Purple;
                        break;
                }

                string docState = view.GetRowCellDisplayText(e.RowHandle, view.Columns["DOC_STATE"]);
                switch (docState)
                {
                    case DOC_STATE.NOT_STANDARD:
                        // e.Appearance.BackColor = Color.Red;
                        //e.Appearance.
                        //view.
                        break;
                }
            }
        }
        private void gvDefault_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //try
            //{
            //    //根据单元格取值改变单元格背景色 
            //    DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            //    DevExpress.Utils.AppearanceDefault appBlueRed = new DevExpress.Utils.AppearanceDefault(Color.White, Color.Red, Color.Empty, Color.Blue, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            //    //DevExpress.Utils.AppearanceDefault appYB = new DevExpress.Utils.AppearanceDefault(Color.White, Color.Yellow, Color.Empty, Color.YellowGreen, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            //    if (e.Column.FieldName == "STATE")
            //    {
            //        if (view.GetRowCellValue(e.RowHandle, "DOC_STATE").ToString() == DOC_STATE.NOT_STANDARD)
            //        {
            //            DevExpress.Utils.AppearanceHelper.Apply(e.Appearance, appBlueRed);
            //        }
            //    }
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    throw (ex);
            //}
        }

        int mcount = 0;
        private void SetPatientState()
        {
            mcount++;
            string docState = string.Empty;
            string recState = string.Empty;
            //2015.12.20 新增评估单、记录单状态

            DsPatient = patientDbI.GetWardPatientList(_deptCode);
            try
            {
                //SELECT * FROM v_not_standard_docpat_template WHERE  WARD_CODE = " + SqlManager.SqlConvert(wardCode.ToString())
                DsDocNotStandPat = docNursingDbI.GetDocNotStandPatInWard(_deptCode);

                //SELECT TEMPLATE_ID FROM MOBILE.DOC_TEMPLATE A WHERE A.IS_ALARM = 1 ORDER BY A.ALARM_SERIAL_NO
                //预警的TEMPLATE_ID
                DsDocTemplateAlarm = docNursingDbI.GetDocTemplateAlarm();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
                return;
            }
            DsPatient.Tables[0].Columns.Add("STATE", System.Type.GetType("System.String"));
            DsPatient.Tables[0].Columns.Add("DOC_STATE", System.Type.GetType("System.String"));
            DsPatient.Tables[0].Columns.Add("REC_STATE", System.Type.GetType("System.String"));

            DateTime dtNow = GVars.OracleAccess.GetSysDate(), dtAdm;

            #region MyRegion

            //获取需要预警的type_id
            string strAlarmTypeID = string.Empty;
            DataTable dtRec = new DataTable();
            string strAlarmTypeIDSql = "SELECT M.TYPE_ID FROM NURSING_RECORD_TYPE  M WHERE M.IS_ALARM=1 ORDER BY M.ALARM_SERIAL_NO";
            DataSet dsAlarmTypeID = GVars.OracleAccess.SelectData_NoKey(strAlarmTypeIDSql);
            if (dsAlarmTypeID != null && dsAlarmTypeID.Tables.Count > 0 && dsAlarmTypeID.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsAlarmTypeID.Tables[0].Rows.Count; i++)
                {
                    strAlarmTypeID += dsAlarmTypeID.Tables[0].Rows[i][0].ToString() + ",";
                }
                strAlarmTypeID = strAlarmTypeID.Length > 0 ? strAlarmTypeID.Remove(strAlarmTypeID.Length - 1, 1) : "''";
            }
            if (!String.IsNullOrEmpty(strAlarmTypeID))
            {

                //查询SQL
                string sqlRecStr = string.Empty;
                sqlRecStr = @"SELECT * FROM (SELECT T.*, ROW_NUMBER() OVER(PARTITION BY PATIENT_ID,TYPE_ID ORDER BY T.RECORD_DATE DESC) RN    " +
                                     "         FROM (SELECT A.* FROM MOBILE.NURSING_RECORD_CONTENT A, MOBILE.PATIENT_INFO B    " +
                                     "                WHERE B.WARD_CODE = '" + GVars.User.DeptCode + "'    " +
                                     "                  AND A.TYPE_ID IN (" + strAlarmTypeID + ")    " +
                                     "                  AND B.PATIENT_ID = A.PATIENT_ID    " +
                                     "                  AND B.VISIT_ID = A.VISIT_ID) T) T WHERE T.RN = 1";

                DataSet dsRec = GVars.OracleAccess.SelectData_NoKey(sqlRecStr);
                // DataTable dtRec = new DataTable();
                if (dsRec != null && dsRec.Tables.Count > 0 && dsRec.Tables[0].Rows.Count > 0)
                {
                    dtRec = dsRec.Tables[0];
                }
            }

            #endregion

            //遍历病人
            for (int i = 0; i < DsPatient.Tables[0].Rows.Count; i++)
            {
                DataRow drPat = DsPatient.Tables[0].Rows[i];

                #region 设置入院状态
                dtAdm = DateTime.Parse(drPat["ADMISSION_DATE_TIME"].ToString());
                if (dtAdm > dtNow.Date)
                    DsPatient.Tables[0].Rows[i]["STATE"] = PAT_INHOS_STATE.NEW;
                else
                    DsPatient.Tables[0].Rows[i]["STATE"] = PAT_INHOS_STATE.IN;
                #endregion

                #region 设置评估单达标状态

                string sql = "PATIENT_ID = " + SQL.SqlConvert(drPat["PATIENT_ID"].ToString()) + " AND VISIT_NO = " + SQL.SqlConvert(drPat["VISIT_ID"].ToString());
                DataRow[] drDocPat = DsDocNotStandPat.Tables[0].Select(sql);
                //拼接不达标的评估单ID，保存在DOC_STATE中，仅处理不达标的病人
                if (drDocPat.Length > 0)
                {
                    /*遍历病人,当前病人的TEMPLATE_ID如果需要预警,累加*/
                    docState = string.Empty;
                    foreach (DataRow drAlarm in DsDocTemplateAlarm.Tables[0].Rows)
                    {
                        for (int j = 0; j < drDocPat.Length; j++)
                        {
                            if (drAlarm["TEMPLATE_ID"].ToString() == drDocPat[j]["TEMPLATE_ID"].ToString())
                                docState += drAlarm["TEMPLATE_ID"].ToString();
                        }
                        docState += ComConst.STR.COMMA;
                    }
                    if (!string.IsNullOrEmpty(docState))
                        DsPatient.Tables[0].Rows[i]["DOC_STATE"] = docState;
                }

                #endregion


            }

            #region 设置护理记录单状态标志

            if (dtRec.Rows.Count > 0)
            {
                for (int i = 0; i < DsPatient.Tables[0].Rows.Count; i++)
                {
                    recState = string.Empty;
                    string patientId = DsPatient.Tables[0].Rows[i]["PATIENT_ID"].ToString();
                    DataRow[] dr = dtRec.Select(" PATIENT_ID='" + patientId + "'");
                    for (int m = 0; m < dr.Length; m++)
                    {
                        recState += dr[m]["CHECK_FLAG"].ToString() + ComConst.STR.COMMA;
                    }
                    recState = recState.Length > 0 ? recState.Remove(recState.Length - 1, 1) : recState;
                    DsPatient.Tables[0].Rows[i]["REC_STATE"] = recState;
                }
            }

            #endregion
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        /// <summary>
        /// 是否质控员 true:是 false:否
        /// </summary>
        /// <returns></returns>
        private bool IsQualityControl()
        {
            bool _isQualityControl = false;
            //获取APP_CONFIG中科室审核权限的集合
            string strValue = string.Empty;
            DataSet dsCheckRole = GVars.OracleAccess.SelectData("SELECT PARAMETER_VALUE FROM MOBILE.APP_CONFIG  WHERE PARAMETER_NAME='CHECK_ROLE'");
            if (dsCheckRole != null && dsCheckRole.Tables.Count > 0 && dsCheckRole.Tables[0].Rows.Count > 0)
            {
                strValue = dsCheckRole.Tables[0].Rows[0][0].ToString();
                string[] arrRoleValue = strValue.Split("|".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrRoleValue.Length > 0)
                {
                    for (int i = 0; i < arrRoleValue.Length; i++)
                    {
                        //找到当前登录的科室对应的护士权限编号
                        if (arrRoleValue[i].Split('-')[0].ToString() == GVars.User.DeptCode)
                        {
                            if (arrRoleValue[i].Contains(GVars.User.UserName))
                            {
                                _isQualityControl = true;
                            }
                        }
                    }
                }
            }
            return _isQualityControl;
        }


        private void cmbDeptList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!_isRefresh)
                {
                    if (GVars.App.UserInput == false)
                    {
                        return;
                    }
                    if (!MdiFrm.GetInstance().CanRefresh())
                    {

                        MessageBox.Show("在切换科室前，请先关闭除'病人卡片'外的所有标签页!", "提示");
                        return;
                    }
                }

                this.Cursor = Cursors.WaitCursor;


                if (cmbDeptList.Items.Count > 0 && !string.IsNullOrEmpty(cmbDeptList.SelectedValue.ToString()))
                {
                    _deptCode = cmbDeptList.SelectedValue.ToString();
                    //切换科室后，修改相关全局变量，并刷新界面，如：主框架的科室名称.
                    //if (sender is DevExpress.XtraEditors.LookUpEdit)
                    //{
                    GVars.User.DeptCode = _deptCode;
                    GVars.User.DeptName = hospitalDbI.Get_DeptName(GVars.User.DeptCode);
                    //}
                }

                //2015.12.04增加状态
                this.SetPatientState();

                DataColumn[] dtCol = new DataColumn[3];
                dtCol[0] = DsPatient.Tables[0].Columns["PATIENT_ID"];
                dtCol[1] = DsPatient.Tables[0].Columns["VISIT_ID"];
                dtCol[2] = DsPatient.Tables[0].Columns["STATE"];
                DsPatient.Tables[0].PrimaryKey = dtCol;
                DsPatient.Tables[0].DefaultView.Sort = "BED_NO";

                ucGridView1.ClearSelection();
                ucGridView1.DataSource = DsPatient.Tables[0].DefaultView;

                if (ucGridView1.SelectRowsCount > 0)
                {
                    ucGridView1_SelectionChanged(null, null);
                }

                #region 2015.11.25测试事件，参数应替换
                PatientName = Convert.ToString(ucGridView1.GetSelectCellValue("NAME"));
                _visitId = Convert.ToString(ucGridView1.GetSelectCellValue("VISIT_ID"));
                if (PatientRefresh != null)
                {
                    PatientRefresh(this, new PatientEventArgs(_patientId, _visitId));
                }
                #endregion

                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                _isRefresh = false;
                this.Cursor = Cursors.Default;
                Error.ErrProc(ex);
            }
        }


    }
}
