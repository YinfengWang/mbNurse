using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using CommonEntity;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraGrid.Views.Layout.ViewInfo;
using HISPlus;
using DocDAL;
namespace DXApplication2
{
    public partial class PatientCard : FormDo, IBasePatient
    {
        #region 公共变量/属性

        /// <summary>
        /// 是否自动生成列.默认为自动生成,如果手动添加列时,会自动
        /// </summary>
        private bool _isAutoGenerateColumns = true;

        /// <summary>
        /// 特级护理背景色
        /// </summary>
        private readonly Color _nursingLevel0Color = Color.Orange;
        /// <summary>
        /// 一级护理背景色
        /// </summary>
        private readonly Color _nursingLevel1Color = Color.Red;
        /// <summary>
        /// 二级护理背景色
        /// </summary>
        private readonly Color _nursingLevel2Color = Color.Lime;
        /// <summary>
        /// 三级护理背景色
        /// </summary>
        private readonly Color _nursingLevel3Color = Color.DeepSkyBlue;

        /// <summary>
        /// 右键菜单图片列表
        /// </summary>
        private ImageList _menuImageLists;

        private IList<MenuManager> _listMenus;


        private DocNursingDbi docNursingDbI = null;
        /// <summary>
        /// 病人列表
        /// </summary>
        private DataSet _dsPatient;

        /// <summary>
        /// 排序后的护理等级字典
        /// </summary>
        private DataTable _dtNursingClass;

        private PatientDbI _patientDbI;
        private NursingDbI _nursingDbi;
        /// <summary>
        /// doc_template表，预警数据
        /// </summary>
        DataSet dsYj = new DataSet();
        /// <summary>
        /// 是否质控员
        /// </summary>
        private bool _isQualityControl = false;
        #endregion 公共变量/属性

        public PatientCard()
        {
            InitializeComponent();
            docNursingDbI = new DocNursingDbi(GVars.OracleAccess);

            //设置提示信息
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
        }
        /// <summary>
        /// 病人列表刷新 2015.11.25 add 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PatientListRefresh(object sender, EventArgs e)
        {
            base.LoadingShow();
            _patientDbI = new PatientDbI(GVars.OracleAccess);
            _nursingDbi = new NursingDbI(GVars.OracleAccess);

            SetNursingLevelColor();
            //2015.12.23 del
            //_dsPatient = _patientDbI.GetWardPatientList(GVars.User.DeptCode);
            //_dsPatient.Tables[0].DefaultView.Sort = "BED_NO";

            //和病人列表使用同一个数据源
            _dsPatient = MdiFrm.GetInstance().GetPatientlist();

            #region 注释
            // Color color2 = Color.FromArgb(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]), Convert.ToInt32(strArray[2]));

            //ucGridView1.DataSource = DsPatient.Tables[0].DefaultView;

            //ucGridView1.ShowFindPanel = true;
            //ucGridView1.ShowHeaders = true;
            //this.Add("姓名", "NAME");
            //this.Add("性别", "SEX");

            //this.lblAge.Text = PersonCls.GetAge((DateTime)ucGridView1.GetSelectCellValue("DATE_OF_BIRTH"), DateTime.Now);

            //this.Add("年龄", "DATE_OF_BIRTH");
            //this.Add("出生日期", "DATE_OF_BIRTH");
            ////this.Add("主要诊断", "DIAGNOSIS");
            //this.Add("ID", "PATIENT_ID");
            //this.Add("住院号", "INP_NO");
            //this.Add("护理等级", "NURSING_CLASS_NAME");
            //this.Add("病情状态", "PATIENT_STATUS_NAME");
            ////this.Add("入院日期", "ADMISSION_DATE_TIME");  

            //this.Add("床号", "BED_NO",false);
            #endregion

            gridControl1.DataSource = _dsPatient.Tables[0].DefaultView;
            Init();

            //this.layoutView1.OptionsFind.FindFilterColumns = "BED_NO,NAME,PATIENT_STATUS_NAME,ADMISSION_DATE_TIME,DATE_OF_BIRTH,SEX";
            this.layoutView1.OptionsFind.FindMode = FindMode.Always;
            layoutView1.OptionsFilter.AllowFilterEditor = true;

            SetNursingLevelCount();
            base.LoadingClose();
        }
        private void PatientCard1_Load(object sender, EventArgs e)
        {
            try
            {
                _isQualityControl = IsQualityControl();
                //2015.11.25 add
                PatientListRefresh(null, null);
                #region 移至PatientListRefresh
                //_patientDbI = new PatientDbI(GVars.OracleAccess);
                //_nursingDbi = new NursingDbI(GVars.OracleAccess);

                //SetNursingLevelColor();
                //_dsPatient = _patientDbI.GetWardPatientList(GVars.User.DeptCode);
                //_dsPatient.Tables[0].DefaultView.Sort = "BED_NO";

                //#region 注释
                //// Color color2 = Color.FromArgb(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]), Convert.ToInt32(strArray[2]));

                ////ucGridView1.DataSource = DsPatient.Tables[0].DefaultView;

                ////ucGridView1.ShowFindPanel = true;
                ////ucGridView1.ShowHeaders = true;
                ////this.Add("姓名", "NAME");
                ////this.Add("性别", "SEX");

                ////this.lblAge.Text = PersonCls.GetAge((DateTime)ucGridView1.GetSelectCellValue("DATE_OF_BIRTH"), DateTime.Now);

                ////this.Add("年龄", "DATE_OF_BIRTH");
                ////this.Add("出生日期", "DATE_OF_BIRTH");
                //////this.Add("主要诊断", "DIAGNOSIS");
                ////this.Add("ID", "PATIENT_ID");
                ////this.Add("住院号", "INP_NO");
                ////this.Add("护理等级", "NURSING_CLASS_NAME");
                ////this.Add("病情状态", "PATIENT_STATUS_NAME");
                //////this.Add("入院日期", "ADMISSION_DATE_TIME");  

                ////this.Add("床号", "BED_NO",false);
                //#endregion

                //gridControl1.DataSource = _dsPatient.Tables[0].DefaultView;
                //Init();

                ////this.layoutView1.OptionsFind.FindFilterColumns = "BED_NO,NAME,PATIENT_STATUS_NAME,ADMISSION_DATE_TIME,DATE_OF_BIRTH,SEX";
                //this.layoutView1.OptionsFind.FindMode = FindMode.Always;
                //layoutView1.OptionsFilter.AllowFilterEditor = true;

                //SetNursingLevelCount();
                #endregion

                _listMenus = EntityOper.GetInstance().FindByProperty<MenuManager>("Enabled", (byte)1);

                const string filter = "MODULE_CODE = '{0}'";
                foreach (MenuManager model in _listMenus)
                {
                    DataRow[] drFind = GVars.User.Rights.Tables[0].Select(string.Format(filter, model.ModuleCode));
                    if (model.ParentId != "0" && drFind.Length == 0)
                        model.Enabled = 0;
                }

                _listMenus = _listMenus.Where(p => p.Enabled == (byte)1).OrderBy(p => p.ParentId).ThenBy(p => p.SortId).ToList();

                InitDoc();
                InitPopupMenu();

                //layoutView1.OptionsView.ViewMode = LayoutViewMode.MultiColumn;  //2016-05-06
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

                //if (docTemplates.Any())
                //    _listMenus.Add(menuManager);
                //else
                //    continue;

                if (docTemplates.Any())
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

                //if (model.ParentMenuId != "0" && drFind.Length == 0) continue;

                // 护理评估菜单不需要
                if (model.MenuId == ConnMenu.DocMenuId) continue;

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
                        bar.ItemClick += bar_ItemClick;
                    }

                    bar.Caption = model.Name;
                    bar.Tag = model.MenuId;

                    Image image = MdiFrm.GetImageFile(model.IconPath);
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
                    Image image = MdiFrm.GetImageFile(model.IconPath);
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
                if (nodeId != null && nodeId.Contains(ComConst.STR.UnderLine))
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

        private void SetNursingLevelColor()
        {
            DataSet ds = _nursingDbi.GetNursingColors();
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count < 4)
            {
                throw new Exception("未设定护理等级,请联系管理员.");
            }
            #region//2015.10.26
            _dtNursingClass = ds.Tables[0];
            _dtNursingClass.DefaultView.Sort = "SERIAL_NO,NURSING_CLASS_CODE ASC";
            _dtNursingClass = _dtNursingClass.DefaultView.ToTable();
            #endregion

            List<Control> lst = new List<Control>();
            lst.Add(lblNursingLevel0);
            lst.Add(lblNursingLevel1);
            lst.Add(lblNursingLevel2);
            lst.Add(lblNursingLevel3);
            lst.Add(lblNursingLevel4);
            lst.Add(lblNursingLevel5);
            List<Control> lstCount = new List<Control>();
            lstCount.Add(lblNursingLevel0Count);
            lstCount.Add(lblNursingLevel1Count);
            lstCount.Add(lblNursingLevel2Count);
            lstCount.Add(lblNursingLevel3Count);
            lstCount.Add(lblNursingLevel4Count);
            lstCount.Add(lblNursingLevel5Count);
            for (int level = 0; level < lst.Count; level++)
            {
                if (level >= _dtNursingClass.Rows.Count)//2015.10.26
                {
                    lst[level].Visible = false;
                    lstCount[level].Visible = false;
                    continue;
                }

                DataRow dr = _dtNursingClass.Rows[level];//2015.10.26 mod ds.Tables[0].Rows[level];
                if (dr["NURSING_CLASS_CODE"] != null /*&& Convert.ToInt32(dr["NURSING_CLASS_CODE"]) == level*/)//2015.10.26 del
                {
                    string color = dr["SHOW_COLOR"].ToString();
                    if (!string.IsNullOrEmpty(color))
                    {
                        string[] charColor = color.Split(',');
                        if (charColor.Length == 3)
                        {
                            lst[level].BackColor = Color.FromArgb(Convert.ToInt32(charColor[0]), Convert.ToInt32(charColor[1]), Convert.ToInt32(charColor[2]));
                        }
                    }
                    if (dr["NURSING_CLASS_NAME"] != null)//2015.10.20 ADD
                    {
                        lst[level].Text = dr["NURSING_CLASS_NAME"].ToString();
                    }
                }
                else
                {
                    lst[level].Visible = false;//2015.10.26
                    lstCount[level].Visible = false;
                }

            }
            //lblNursingLevel0.BackColor = _nursingLevel0Color;
            //lblNursingLevel1.BackColor = _nursingLevel1Color;
            //lblNursingLevel2.BackColor = _nursingLevel2Color;
            //lblNursingLevel3.BackColor = _nursingLevel3Color;
        }

        private void SetNursingLevelCount()
        {
            const string formatString = "{0} 人";
            DataTable dt = _dsPatient.Tables[0];
            string strClass = string.Empty;
            lblNursingLevel0Count.Text = string.Format(formatString, dt.Select(GetNursingClassFilter(0)).Length);//2015.10.26 NURSING_CLASS -》SERIAL_NO
            lblNursingLevel1Count.Text = string.Format(formatString, dt.Select(GetNursingClassFilter(1)).Length);
            lblNursingLevel2Count.Text = string.Format(formatString, dt.Select(GetNursingClassFilter(2)).Length);
            lblNursingLevel3Count.Text = string.Format(formatString, dt.Select(GetNursingClassFilter(3)).Length);
            lblNursingLevel4Count.Text = string.Format(formatString, dt.Select(GetNursingClassFilter(4)).Length);
            lblNursingLevel5Count.Text = string.Format(formatString, dt.Select(GetNursingClassFilter(5)).Length);
            lblTotalCount.Text = string.Format("[{0}] 人", dt.Rows.Count);
        }


        /// <summary>
        /// 初始化控件属性。
        /// 窗体初始化顺序：        
        ///     UserControl.构造方法 -->        
        ///     引用窗体.构造方法 -->
        ///     UserControl.Load -->
        ///     引用窗体.Load 。      
        /// </summary>
        private void Init()
        {
            this.repositoryItemPictureEdit1.PictureAlignment = ContentAlignment.MiddleCenter;
            this.repositoryItemPictureEdit1.PictureInterpolationMode = InterpolationMode.HighQualityBicubic;
            this.repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;

            if (layoutView1.Columns.Count == 0)
            {
                layoutView1.PopulateColumns();
            }

            // 是否可编辑
            layoutView1.OptionsBehavior.Editable = true;
            this.layoutView1.OptionsBehavior.ReadOnly = false;

            layoutView1.OptionsSelection.MultiSelect = true;
            layoutView1.OptionsView.CardsAlignment = CardsAlignment.Center;

            //layoutView1.OptionsFilter.AllowFilterEditor = true;

            layoutView1.OptionsFind.AllowFindPanel = false;
            layoutView1.OptionsFind.AlwaysVisible = false;
            //layoutView1.OptionsFind.ClearFindOnClose = false;
            //layoutView1.OptionsFind.ShowFindButton = false;
            //layoutView1.OptionsFind.ShowClearButton = false;

            this.layoutView1.AppearancePrint.FieldCaption.Options.UseBackColor = true;
            this.layoutView1.AppearancePrint.FieldCaption.Options.UseForeColor = true;
            this.layoutView1.CardHorzInterval = 15;
            this.layoutView1.CardVertInterval = 20;

            //this.layoutView1.OptionsMultiRecordMode.MultiColumnScrollBarOrientation = DevExpress.XtraGrid.Views.Layout.ScrollBarOrientation.Horizontal;
            this.layoutView1.OptionsView.ContentAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutView1.OptionsView.ShowHeaderPanel = false;

            this.layoutView1.OptionsView.ShowCardExpandButton = false;
        }

        #region 添加列

        /// <summary>
        /// 将列添加到集合中
        /// </summary>
        /// <param name="caption">列标题单元格的标题文本。</param>
        /// <param name="fieldName">绑定的数据库列的名称。</param>
        /// <param name="visible">指示该列是否可见。默认为可见。</param>
        /// <param name="columnEdit"></param>
        /// <param name="groupIndex">分级索引,从0开始</param>
        /// <param name="width"></param>
        /// <param name="minWidth"></param>
        /// <param name="format">格式化字符串</param>
        private void Add(string caption, string fieldName, bool visible
            , RepositoryItem columnEdit, int groupIndex = -1, int width = 50, int minWidth = 30, string format = null)
        {
            if (_isAutoGenerateColumns)
            {
                _isAutoGenerateColumns = false;
            }

            LayoutViewColumn col = new LayoutViewColumn();
            col.Caption = caption;
            col.FieldName = fieldName;
            col.MinWidth = minWidth;
            col.Width = width;
            //col.Visible = visible;
            col.VisibleIndex = layoutView1.Columns.Count;
            col.ColumnEdit = columnEdit;
            col.GroupIndex = groupIndex;

            if (columnEdit is RepositoryItemCheckEdit)
            {
                (columnEdit as RepositoryItemCheckEdit).NullStyle = StyleIndeterminate.Unchecked;
            }
            col.OptionsColumn.AllowEdit = false;

            if (groupIndex > -1 && !string.IsNullOrEmpty(format))
            {

                //if (groupIndex == 0)
                col.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.DisplayText;
                //else
                //    col.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.Value;

                col.GroupFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                col.GroupFormat.FormatString = format;

            }
            else if (!string.IsNullOrEmpty(format))
                col.DisplayFormat.FormatString = format;

            if (string.IsNullOrEmpty(fieldName))
                col.UnboundType = DevExpress.Data.UnboundColumnType.String;

            layoutView1.Columns.Add(col);
            layoutView1.Columns[fieldName].Visible = visible;
        }

        #endregion

        private void layoutView1_CustomDrawCardCaption(object sender, DevExpress.XtraGrid.Views.Layout.Events.LayoutViewCustomDrawCardCaptionEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;

                if (e.CardCaption.Contains("床号")) return;

                DataRow dr = layoutView1.GetDataRow(e.RowHandle);
                if (dr == null) return;
                int nursingLevel = -1;
                if (dr["BED_NO"] != null)
                {
                    // 补空格，实现居中效果
                    //e.CardCaption = e.CardCaption.Replace("Record", "床号：" + (dr["BED_NO"].ToString()).PadRight(5, ' '));
                    string strEmpty = "-";
                    int i = 0;
                    while (e.Graphics.MeasureString(strEmpty, e.Appearance.Font).Width < (e.CaptionBounds.Width / 2) - 20 &&
                           i < 30)
                    {
                        strEmpty += "-";
                        i++;
                    }
                    e.CardCaption = ComConst.STR.BLANK.PadLeft(strEmpty.Length, ' ') + "床号：" + dr["BED_LABEL"];

                    if (dr["NURSING_CLASS_COLOR"] != DBNull.Value && dr["NURSING_CLASS_COLOR"] != null)
                    {
                        // 好看的渐变：
                        // 橙色变黄色
                        // 深蓝别浅蓝
                        //Color backColor = e.Appearance.BackColor;
                        Color backColor = Color.FromArgb(255, 255, 255);

                        if (dr["NURSING_CLASS_COLOR"] != null)
                        {
                            string color = dr["NURSING_CLASS_COLOR"].ToString();
                            if (!string.IsNullOrEmpty(color))
                            {
                                string[] charColor = color.Split(',');
                                if (charColor.Length == 3)
                                {
                                    backColor = Color.FromArgb(Convert.ToInt32(charColor[0]),
                                        Convert.ToInt32(charColor[1]), Convert.ToInt32(charColor[2]));
                                }
                            }
                        }
                        //switch (nursingLevel)
                        //{
                        //    case 0:
                        //        backColor = _nursingLevel0Color;
                        //        break;
                        //    case 1:
                        //        backColor = _nursingLevel1Color;
                        //        break;
                        //    case 2:
                        //        backColor = _nursingLevel2Color;
                        //        break;
                        //    case 3:
                        //        backColor = _nursingLevel3Color;
                        //        break;
                        //}
                        Brush brush = e.Cache.GetGradientBrush(e.CaptionBounds, backColor, Color.White,
                            LinearGradientMode.Horizontal);
                        e.Graphics.FillRectangle(brush, e.CaptionBounds);
                    }
                }
                e.Appearance.DrawString(e.Cache, e.CardCaption, e.CaptionBounds);
                e.CardCaption = "";
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void layoutView1_CustomDrawCardFieldValue(object sender, RowCellCustomDrawEventArgs e)
        {
            try
            {
                #region 加载患者照片
                if (e.Column == 图片)
                {
                    if (e.CellValue == null)
                        e.CellValue = layoutView1.GetDataRow(e.RowHandle)["SEX"].ToString() == "男" ? Properties.Resources.man : Properties.Resources.woman;
                }
                #endregion

                #region 风险评估预警
                if (e.Column == DOC_ICON1)
                {
                    string state = layoutView1.GetDataRow(e.RowHandle)["DOC_STATE"].ToString();
                    char split = ComConst.STR.COMMA[0];
                    string[] sArray = state.Split(split);
                    if (sArray.Length > 0 && !String.IsNullOrEmpty(sArray[0].ToString()))
                        e.CellValue = Properties.Resources.文本1;
                    else
                        e.CellValue = Properties.Resources.文本01;
                }
                if (e.Column == DOC_ICON2)
                {
                    string state = layoutView1.GetDataRow(e.RowHandle)["DOC_STATE"].ToString();
                    char split = ComConst.STR.COMMA[0];
                    string[] sArray = state.Split(split);
                    if (sArray.Length > 1 && !String.IsNullOrEmpty(sArray[1].ToString()))
                        e.CellValue = Properties.Resources.文本2;
                    else
                        e.CellValue = Properties.Resources.文本02;
                }

                if (e.Column == DOC_ICON3)
                {
                    string state = layoutView1.GetDataRow(e.RowHandle)["DOC_STATE"].ToString();
                    char split = ComConst.STR.COMMA[0];
                    string[] sArray = state.Split(split);
                    if (sArray.Length > 2 && !String.IsNullOrEmpty(sArray[2].ToString()))
                        e.CellValue = Properties.Resources.文本3;
                    else
                        e.CellValue = Properties.Resources.文本03;

                }

                if (e.Column == DOC_ICON4)
                {

                    string state = layoutView1.GetDataRow(e.RowHandle)["DOC_STATE"].ToString();
                    char split = ComConst.STR.COMMA[0];
                    string[] sArray = state.Split(split);
                    if (sArray.Length > 3 && !String.IsNullOrEmpty(sArray[3].ToString()))
                        e.CellValue = Properties.Resources.文本4;
                    else
                        e.CellValue = Properties.Resources.文本04;

                }
                #endregion

                #region 护理记录单状态 预留
                //if (e.Column == REC_ICON1)
                //{
                //    string state = layoutView1.GetDataRow(e.RowHandle)["REC_STATE"].ToString();
                //    char split = ComConst.STR.COMMA[0];
                //    string[] sArray = state.Split(split);
                //    if (sArray.Length > 0 && !String.IsNullOrEmpty(sArray[0].ToString()))
                //        e.CellValue = Properties.Resources.REC_ICON2;
                //    else
                //        e.CellValue = Properties.Resources.Null_icon;
                //}
                //if (e.Column == REC_ICON2)
                //{
                //    string state = layoutView1.GetDataRow(e.RowHandle)["REC_STATE"].ToString();
                //    char split = ComConst.STR.COMMA[0];
                //    string[] sArray = state.Split(split);
                //    if (sArray.Length > 1 && !String.IsNullOrEmpty(sArray[1].ToString()))
                //        e.CellValue = Properties.Resources.REC_ICON2;
                //    else
                //        e.CellValue = Properties.Resources.Null_icon;
                //}

                if (_isQualityControl)
                {
                    //质控员
                    if (e.Column == REC_ICON1)
                    {
                        string state = layoutView1.GetDataRow(e.RowHandle)["REC_STATE"].ToString();
                        if (state.Split(',').Length > 1)
                        {
                            switch (state.Split(',')[0].ToString())
                            {
                                case "0":
                                    e.CellValue = Properties.Resources.未审核02;
                                    break;
                                default:
                                    e.CellValue = Properties.Resources.Null_icon;
                                    break;
                            }
                        }
                        else
                            e.CellValue = Properties.Resources.Null_icon;

                    }
                    if (e.Column == REC_ICON2)
                    {
                        //护士
                        string state = layoutView1.GetDataRow(e.RowHandle)["REC_STATE"].ToString();
                        if (state.Split(',').Length > 2)
                        {
                            switch (state.Split(',')[1].ToString())
                            {
                                case "0":
                                    e.CellValue = Properties.Resources.未审核02;
                                    break;
                                default:
                                    e.CellValue = Properties.Resources.Null_icon;
                                    break;
                            }
                        }
                        else
                            e.CellValue = Properties.Resources.Null_icon;

                    }
                    if (e.Column == REC_ICON3)
                    {
                        string state = layoutView1.GetDataRow(e.RowHandle)["REC_STATE"].ToString();
                        if (state.Split(',').Length > 3)
                        {
                            switch (state.Split(',')[2].ToString())
                            {
                                case "0":
                                    e.CellValue = Properties.Resources.未审核02;
                                    break;
                                default:
                                    e.CellValue = Properties.Resources.Null_icon;
                                    break;
                            }
                        }
                        else
                            e.CellValue = Properties.Resources.Null_icon;
                    }
                    if (e.Column == REC_ICON4)
                    {
                        //质控员
                        string state = layoutView1.GetDataRow(e.RowHandle)["REC_STATE"].ToString();
                        if (state.Split(',').Length > 4)
                        {
                            switch (state.Split(',')[3].ToString())
                            {
                                case "0":
                                    e.CellValue = Properties.Resources.未审核02;
                                    break;
                                default:
                                    e.CellValue = Properties.Resources.Null_icon;
                                    break;
                            }
                        }
                        else
                            e.CellValue = Properties.Resources.Null_icon;
                    }
                }
                else
                {
                    //护士
                    if (e.Column == REC_ICON1)
                    {
                        string state = layoutView1.GetDataRow(e.RowHandle)["REC_STATE"].ToString();
                        if (state.Split(',').Length > 1)
                        {
                            switch (state.Split(',')[0])
                            {
                                case "1":
                                    e.CellValue = Properties.Resources.未通过02;
                                    break;
                                case "3":
                                    e.CellValue = Properties.Resources.待审核02;
                                    break;
                                default:
                                    e.CellValue = Properties.Resources.Null_icon;
                                    break;
                            }
                        }
                        else
                            e.CellValue = Properties.Resources.Null_icon;
                    }

                    if (e.Column == REC_ICON2)
                    {
                        string state = layoutView1.GetDataRow(e.RowHandle)["REC_STATE"].ToString();
                        if (state.Split(',').Length > 2)
                        {
                            switch (state.Split(',')[1])
                            {
                                case "1":
                                    e.CellValue = Properties.Resources.未通过02;
                                    break;
                                case "3":
                                    e.CellValue = Properties.Resources.待审核02;
                                    break;
                                default:
                                    e.CellValue = Properties.Resources.Null_icon;
                                    break;
                            }
                        }
                        else
                            e.CellValue = Properties.Resources.Null_icon;
                    }

                    if (e.Column == REC_ICON3)
                    {
                        string state = layoutView1.GetDataRow(e.RowHandle)["REC_STATE"].ToString();
                        if (state.Split(',').Length > 3)
                        {
                            switch (state.Split(',')[2])
                            {
                                case "1":
                                    e.CellValue = Properties.Resources.未通过02;
                                    break;
                                case "3":
                                    e.CellValue = Properties.Resources.待审核02;
                                    break;
                                default:
                                    e.CellValue = Properties.Resources.Null_icon;
                                    break;
                            }
                        }
                        else
                            e.CellValue = Properties.Resources.Null_icon;
                    }

                    if (e.Column == REC_ICON4)
                    {
                        string state = layoutView1.GetDataRow(e.RowHandle)["REC_STATE"].ToString();
                        if (state.Split(',').Length > 4)
                        {
                            switch (state.Split(',')[3])
                            {
                                case "1":
                                    e.CellValue = Properties.Resources.未通过02;
                                    break;
                                case "3":
                                    e.CellValue = Properties.Resources.待审核02;
                                    break;
                                default:
                                    e.CellValue = Properties.Resources.Null_icon;
                                    break;
                            }
                        }
                        else
                            e.CellValue = Properties.Resources.Null_icon;
                    }
                }
                #endregion

                #region 处理出生日期格式、在院状态颜色
                switch (e.Column.FieldName)
                {
                    case "DATA_OF_BIETH":
                        if (string.IsNullOrEmpty(e.DisplayText)) return;
                        if (e.DisplayText.Contains("岁") || e.DisplayText.Contains("月") || e.DisplayText.Contains("日")) return;
                        e.DisplayText = PersonCls.GetAge((DateTime)e.CellValue, GVars.OracleAccess.GetSysDate());
                        break;
                    case "STATE":
                        if (string.IsNullOrEmpty(e.DisplayText)) return;
                        if (e.DisplayText.Equals(HISPlus.PAT_INHOS_STATE.IN)) e.DisplayText = string.Empty;
                        if (e.DisplayText.Equals(HISPlus.PAT_INHOS_STATE.OUT)) e.Appearance.ForeColor = Color.Gray;
                        if (e.DisplayText.Equals(HISPlus.PAT_INHOS_STATE.NEW)) e.Appearance.ForeColor = Color.Red;
                        break;
                    default:
                        return;
                }
                #endregion

                #region 合计

                DataSet DsDocNotStandPat = docNursingDbI.GetDocNotStandPatInWard(GVars.User.DeptCode);

                //数据库配置需要预警的评估单
                string jySql = "SELECT * FROM MOBILE.DOC_TEMPLATE M WHERE M.IS_ALARM=1 ORDER BY M.ALARM_SERIAL_NO";
                dsYj = GVars.OracleAccess.SelectData_NoKey(jySql);

                if (DsDocNotStandPat != null && DsDocNotStandPat.Tables[0].Rows.Count > 0)
                {

                    lbl1Sum.Text = DsDocNotStandPat.Tables[0].Select("TEMPLATE_ID=" + dsYj.Tables[0].Rows[0]["TEMPLATE_ID"].ToString()).Count().ToString();
                    lbl2Sum.Text = DsDocNotStandPat.Tables[0].Select("TEMPLATE_ID=" + dsYj.Tables[0].Rows[1]["TEMPLATE_ID"].ToString()).Count().ToString();
                    lbl3Sum.Text = DsDocNotStandPat.Tables[0].Select("TEMPLATE_ID=" + dsYj.Tables[0].Rows[2]["TEMPLATE_ID"].ToString()).Count().ToString();
                    lbl4Sum.Text = DsDocNotStandPat.Tables[0].Select("TEMPLATE_ID=" + dsYj.Tables[0].Rows[3]["TEMPLATE_ID"].ToString()).Count().ToString();
                }


                #endregion
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        /// 验证当前用户是否为质控员
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

        private void layoutView1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Point pt = layoutView1.GridControl.PointToClient(Control.MousePosition);
                LayoutViewHitInfo info = layoutView1.CalcHitInfo(pt);
                if (info.InCard)
                {
                    if (info.RowHandle >= 0)
                    {
                        DataRow dr = layoutView1.GetDataRow(info.RowHandle);
                        if (dr == null) return;
                        MdiFrm.GetInstance().LocatePatient(dr["PATIENT_ID"].ToString());
                        if (e.Button == MouseButtons.Right)
                        {
                            popupMenu.ShowPopup(Control.MousePosition);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void ucButton1_Click(object sender, EventArgs e)
        {
            try
            {
                ConnMenu c = new ConnMenu();
                if (c.ShowDialog() == DialogResult.OK)
                {
                    InitPopupMenu();
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void gridControl1_Paint(object sender, PaintEventArgs e)
        {
            LayoutViewInfo viewInfo = layoutView1.GetViewInfo() as LayoutViewInfo;
            if (viewInfo == null) return;

            LayoutViewCard fCard = viewInfo.VisibleCards.FirstOrDefault(p => p.RowHandle == layoutView1.FocusedRowHandle);

            if (fCard == null) return;

            Rectangle rect = fCard.Bounds;
            e.Graphics.DrawRectangle(new Pen(Color.Lime, 2), rect);
        }

        private void lblNursingLevel0_Click(object sender, EventArgs e)
        {
            //_dsPatient.Tables[0].DefaultView.RowFilter = "NURSING_CLASS=0";
            _dsPatient.Tables[0].DefaultView.RowFilter = GetNursingClassFilter(0);//2015.10.26
        }

        private void lblTotalCount_Click(object sender, EventArgs e)
        {
            _dsPatient.Tables[0].DefaultView.RowFilter = string.Empty;
        }

        private void lblNursingLevel1_Click(object sender, EventArgs e)
        {
            _dsPatient.Tables[0].DefaultView.RowFilter = GetNursingClassFilter(1);
        }

        private void lblNursingLevel2_Click(object sender, EventArgs e)
        {
            _dsPatient.Tables[0].DefaultView.RowFilter = GetNursingClassFilter(2);
        }

        private void lblNursingLevel3_Click(object sender, EventArgs e)
        {
            _dsPatient.Tables[0].DefaultView.RowFilter = GetNursingClassFilter(3);
        }

        private void lblNursingLevel3Count_Click(object sender, EventArgs e)
        {
            //if (_dtNursingClass.Rows.Count > 4)
            //{
            //    _dsPatient.Tables[0].DefaultView.RowFilter = "NURSING_CLASS=" + _dtNursingClass.Rows[4]["NURSING_CLASS_CODE"].ToString();
            //}
        }

        private void lblNursingLevel4_Click(object sender, EventArgs e)
        {
            _dsPatient.Tables[0].DefaultView.RowFilter = GetNursingClassFilter(4);
        }

        private void lblNursingLevel5_Click(object sender, EventArgs e)
        {
            _dsPatient.Tables[0].DefaultView.RowFilter = GetNursingClassFilter(5);
        }

        /// <summary>
        /// 获取护理登记代码条件 2015.10.26
        /// </summary>
        /// <param name="nIndex">下标，从0开始</param>
        private string GetNursingClassFilter(int nIndex)
        {
            string strFilter = string.Empty;
            string strClassCode = string.Empty;
            if (nIndex >= 0 && nIndex < _dtNursingClass.Rows.Count)
            {
                strClassCode = _dtNursingClass.Rows[nIndex]["NURSING_CLASS_CODE"].ToString();
                if (strClassCode == "" || strClassCode == null)
                    strFilter = "NURSING_CLASS='' OR NURSING_CLASS IS NULL";
                else
                    strFilter = "NURSING_CLASS=" + strClassCode;
            }
            return strFilter;
        }

        public void Patient_SelectionChanged(object sender, PatientEventArgs e)
        {
            this.SelectRow("PATIENT_ID", e.PatientId);
        }
        /// <summary>
        /// 选中行
        /// </summary>
        /// <param name="fieldName">用于查找的字段名称</param>
        /// <param name="value">字段值</param>
        public void SelectRow(string fieldName, object value)
        {
            if (layoutView1.FocusedRowHandle > -1
    && layoutView1.GetRowCellValue(layoutView1.FocusedRowHandle, fieldName).Equals(value))
                return;
            for (int i = 0; i < layoutView1.RowCount; i++)
            {
                for (int j = 0; j < layoutView1.Columns.Count; j++)
                {
                    if (layoutView1.GetRowCellValue(i, fieldName).Equals(value))
                    {
                        layoutView1.FocusedRowHandle = i;
                        layoutView1.SelectRow(i);
                        break;
                    }
                }
            }
        }

        public void Patient_ListRefresh(object sender, PatientEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void layoutView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            //if (e.Value.Equals(DOC_STATE.NOT_STANDARD))
            //{
            //    layoutView1.SetRowCellValue(e.RowHandle, e.Column, "未达标");
            //}
        }


        private int x, y;
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (x != e.X || y != e.Y)
            {
                toolTip1.SetToolTip(picBox1, dsYj.Tables[0].Rows[0]["TEMPLATE_NAME"].ToString());
                x = e.X;
                y = e.Y;
            }

        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {

            if (x != e.X || y != e.Y)
            {
                toolTip1.SetToolTip(pictBox2, dsYj.Tables[0].Rows[1]["TEMPLATE_NAME"].ToString());
                x = e.X;
                y = e.Y;
            }
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (x != e.X || y != e.Y)
            {
                toolTip1.SetToolTip(picBox3, dsYj.Tables[0].Rows[2]["TEMPLATE_NAME"].ToString());
                x = e.X;
                y = e.Y;
            }
        }

        private void pictBox4_MouseMove(object sender, MouseEventArgs e)
        {
            if (x != e.X || y != e.Y)
            {
                toolTip1.SetToolTip(pictBox4, dsYj.Tables[0].Rows[3]["TEMPLATE_NAME"].ToString());
                x = e.X;
                y = e.Y;
            }
        }

        private void picBox1_MouseLeave(object sender, EventArgs e)
        {

        }
    }
}