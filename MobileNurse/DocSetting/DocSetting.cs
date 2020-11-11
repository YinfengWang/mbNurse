using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using System.Linq;
using DXApplication2;

namespace HISPlus
{
    public partial class DocSetting1 : FormDo
    {
        private const string RightEdit = "2";              //  编辑别人录入记录的权限
        private ProcessOperator process;

        /// <summary>
        /// 病区列表 </summary>
        private DataSet _dsWardList;
        private HospitalDbI _hospitalDbI;
        private DocumentDbI _documentDbI;

        /// <summary>
        /// 模板数据(正常情况下应该是仅有一行)
        /// </summary>
        private DocTemplate _templateModel;

        /// <summary>
        /// 当前操作的元素所在行
        /// </summary>
        private DocTemplateElement _currentModel;

        /// <summary>
        /// 模板列表
        /// </summary>
        private IList<DocTemplate> _listTemplates;

        /// <summary>
        /// 关联病区列表
        /// </summary>
        private IList<DocTemplateDept> _listDepts;

        /// <summary>
        /// 模板元素列表
        /// </summary>
        private IList<DocTemplateElement> _listElements;

        /// <summary>
        /// 是否已向元素控件写入数据。
        /// 为true时，已写入；
        /// 为false时，未写入。
        /// </summary>
        private bool IsWrite = false;

        /// <summary>
        /// 执行删除操作的元素列表.当点击保存时一并提交
        /// </summary>
        private IList<DocTemplateElement> _listDeleteElements;

        /// <summary>
        /// 是否需要对元素重新排序
        /// </summary>
        private bool IsSort = false;

        /// <summary>
        /// 设置表格的图片路径的提示信息
        /// </summary>
        private string GridPathInfo = "双击该处可配置Grid背景图片(需要把图片复制到程序目录下的Template文件夹)" + Environment.NewLine;

        /// <summary>
        /// 是否为数字
        /// </summary>
        private const string StrIsNumeric = "^[\\+\\-]?[0-9]*\\.?[0-9]+$";

        private TreeListNode SortNode;
        /// <summary>
        /// 电话号码
        /// </summary>
        public static string StrPhone = @"(^(\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$|(1(([35][0-9])|(47)|[8][01236789]))\d{8}$";

        private readonly CustomValidation _myValidation = new CustomValidation();

        public DocSetting1()
        {
            InitializeComponent();
        }

        private void DocSetting_Load(object sender, EventArgs e)
        {
            try
            {
                base.LoadingShow();

                Init();
                //ValidateRules();

                _id = "DOC18166-670D-4cec-84E4-EDE789DD6123";

                DocCommon.SetSpinEditLimit(txtScore, true, true);
                DocCommon.SetSpinEditLimit(txtWidth);
                DocCommon.SetSpinEditLimit(txtHeight);
                DocCommon.SetSpinEditLimit(txtOffset);
                DocCommon.SetSpinEditLimit(txtChildrenIndent);
                DocCommon.SetSpinEditLimit(txtRowSpacing);

                if (treeElement.Nodes.Count > 0)
                {
                    treeElement.FocusedNode = treeElement.Nodes[0];
                    treeElement_MouseClick(null, null);
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

        private void Init()
        {
            if (GVars.OracleAccess != null)
                _documentDbI = new DocumentDbI(GVars.OracleAccess);

            InitVariable();

            InitDept();

            // 模板类型列表

            // 模板类别列表
            SetDataSource(ddlTemplateClass, _documentDbI.GetTemplateClass(), false);
            // 控件模板列表            
            SetDataSource(ddlControlTemplate, EntityOper.GetInstance().LoadAll<DocControlTemplate>());

            ddlControlStatus.Properties.DataSource =
                Enum.GetValues(typeof(Enums.ControlStatus));
            ddlRelationType.Properties.DataSource =
                Enum.GetValues(typeof(Enums.RelationType));
            ddlTemplateType.Properties.DataSource =
                Enum.GetValues(typeof(Enums.TemplateType));
            ddlDataType.Properties.DataSource =
                Enum.GetValues(typeof(Enums.DataType));

            SetDataSource(ddlFreq, _documentDbI.GetAllFreq(), false, "SERIAL_NO", "FREQ_DESC");

            SetLookUpEditPro(ddlTemplateType);
            SetLookUpEditPro(ddlTemplateClass);
            SetLookUpEditPro(ddlControlTemplate);
            SetLookUpEditPro(ddlControlStatus);
            SetLookUpEditPro(ddlFreq);
            SetLookUpEditPro(ddlRelationType);
            SetLookUpEditPro(ddlRelationItem);
            SetLookUpEditPro(ddlDataType);

            //SetDataSource(ddlDataType, typeof(Enums.DataType));

            InitTreeControl();

            // 字体文本框设为只读
            txtElementFont.Properties.ReadOnly = true;

            // 控件值变更监控
            foreach (Control ctl in layoutControl1.Controls)
            {
                if (ctl is TextEdit)
                    (ctl as TextEdit).EditValueChanged += ElementValueChanged;
                else if (ctl is CheckEdit)
                    (ctl as CheckEdit).CheckedChanged += ElementValueChanged;
            }

            foreach (Control ctl in layoutControl2.Controls)
            {
                if (ctl is TextEdit)
                    (ctl as TextEdit).EditValueChanged += ElementValueChanged;
                else if (ctl is CheckEdit)
                    (ctl as CheckEdit).CheckedChanged += ElementValueChanged;
            }

            _listTemplates = EntityOper.GetInstance().LoadAll<DocTemplate>();
            _listTemplates = _listTemplates.OrderBy(p => p.SerialNo).ToList();//2015.10.20 add
            //ucGridView1.Add("ID", "Id");//2015.10.20 del
            ucGridView1.Add("序号", "SerialNo");//2015.10.20 add
            ucGridView1.Add("名称", "TemplateName", 150);

            //ucGridView1.Add("创建用户", "CreateUser", 200);
            //ucGridView1.Add("创建时间", "CreateDate", 200);
            //ucGridView1.Add("更新用户", "UpdateUser", 200);
            //ucGridView1.Add("更新时间", "UpdateDate", 200);
            //ucGridView1.colu
            ucGridView1.SelectionChanged += ucGridView1_SelectionChanged;
            ucGridView1.DataSource = _listTemplates;           
            ucGridView1.Init();
            
            ucGridView1.SelectFirstRow();
            ddlDataType.Properties.ShowHeader = false;
            ddlRelationType.Properties.ShowHeader = false;

            ddlRelationType.Properties.NullText = @"请选择：";
            ddlDataType.Properties.NullText = @"请选择：";
            //ddlDataType.Properties.
            //ddlRelationItem.Properties.DataSource = null;
        }

        void ucGridView1_SelectionChanged(object sender, EventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;

                base.LoadingShow();

                if (IsWrite)
                {
                    if (XtraMessageBox.Show("数据已修改,是否保存?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        btnSave_Click(null, null);
                    else
                        IsWrite = false;
                }

                object obj = ucGridView1.GetSelectRow();
                if (obj is DocTemplate)
                {
                    _templateModel = obj as DocTemplate;

                    SetValueToHead();

                    InitDeptData();
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                base.LoadingClose();
                GVars.App.UserInput = blnStore;
            }
        }

        /// <summary>
        /// 元素控件值变更时,置写状态为true。
        /// 表示下次进行其他操作时需要保存数据到当前行。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ElementValueChanged(object sender, EventArgs e)
        {
            if (GVars.App.UserInput)
            {
                if (sender is TextEdit &&
                       ((sender as TextEdit) == txtElementName
                        || (sender as TextEdit) == txtTemplateName))
                    return;
                IsWrite = _currentModel != null;
            }
        }

        #region 共通函数
        /// <summary>
        /// 初始化成员变量
        /// </summary>
        private void InitVariable()
        {
            _hospitalDbI = new HospitalDbI(GVars.OracleAccess);

            // 获取护理单元列表
            _dsWardList = _hospitalDbI.Get_WardList_Nurse();
        }

        private void InitDeptData()
        {

            _listDepts = EntityOper.GetInstance().FindByProperty<DocTemplateDept>("DocTemplate.Id", _templateModel.Id);
            this.cbxDept.SelectedIndex = -1;
            //cbxDept.HotTrackItems = false;
            //cbxDept.HotTrackSelectMode = HotTrackSelectMode.SelectItemOnHotTrackEx;            
            if (!cbxIsGlobal.Checked)
                for (int i = 0; i < _dsWardList.Tables[0].Rows.Count; i++)
                {
                    cbxDept.SetItemChecked(i, _listDepts.ToList().Exists(p =>
                        p.DeptCode == _dsWardList.Tables[0].Rows[i]["DEPT_CODE"] as string));
                }
            else
                cbxDept.UnCheckAll();
        }

        /// <summary>
        /// 初始化病区列表
        /// </summary>
        private void InitDept()
        {
            cbxDept.DisplayMember = "DEPT_NAME";
            cbxDept.ValueMember = "DEPT_CODE";
            cbxDept.DataSource = _dsWardList.Tables[0];

            cbxDept.SelectionMode = SelectionMode.MultiExtended;
            cbxDept.CheckOnClick = true;
        }
        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            new ControlSetting().ShowDialog();
        }

        /// <summary>
        /// 设置选中项的复选框状态选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDept.SelectedIndex > 0)
                cbxDept.SetItemChecked(cbxDept.SelectedIndex, !cbxDept.GetItemChecked(cbxDept.SelectedIndex));
        }

        /// <summary>
        /// 初始化TreeList控件
        /// </summary>
        private void InitTreeControl()
        {
            treeElement.Columns.AddRange(new[]
            {
                new TreeListColumn
                {                    
                    FieldName = "Id",
                    Visible = false,
                    Name = "treeColumn1"                    
                },
                new TreeListColumn
                {
                    Caption = @"元素列表",
                    FieldName = "ElementName",
                    Visible = true,
                    Name = "treeColumn2",
                    VisibleIndex = 1
                }
            });

            //// 行指示器【隐藏/显示】
            treeElement.OptionsView.ShowIndicator = false;
            //// 被选中的单元格的聚集框 【隐藏/显示】
            treeElement.OptionsView.ShowFocusedFrame = true;
            //// 双击展开节点
            treeElement.OptionsBehavior.AllowExpandOnDblClick = true;

            treeElement.OptionsBehavior.Editable = false;

            //treeElement.OptionsBehavior.MoveOnEdit = false;
            //treeElement.OptionsBehavior.ShowEditorOnMouseUp = false;

            treeElement.OptionsView.ShowHorzLines = true;
            treeElement.OptionsView.ShowVertLines = true;
            treeElement.OptionsView.ShowRoot = true;
            treeElement.OptionsView.ShowButtons = true;
            treeElement.TreeLineStyle = LineStyle.Dark;
            //treeElement.OptionsSelection.EnableAppearanceFocusedRow = true;           
            treeElement.KeyFieldName = "Id";
            treeElement.ParentFieldName = "ParentId";
        }

        /// <summary>
        /// 设置下拉列表数据源
        /// </summary>
        /// <param name="control"></param>
        /// <param name="showHeader"></param>
        private void SetLookUpEditPro(LookUpEdit control, bool showHeader = false)
        {
            control.Properties.NullText = string.Empty;
            control.Properties.ShowHeader = showHeader;
            // 选中第一项
            control.ItemIndex = 0;
            //自适应宽度
            control.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
        }

        /// <summary>
        /// 设置下拉列表数据源
        /// </summary>
        /// <param name="control"></param>        
        /// <param name="dt"></param>
        /// <param name="showHeader"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void SetDataSource(LookUpEdit control, DataTable dt,
            bool showHeader = true, string key = "ID", string value = "NAME")
        {
            control.Properties.Columns.Add(new LookUpColumnInfo(value, "名称"));
            if (showHeader)
                control.Properties.Columns.Add(new LookUpColumnInfo("REMARK", "说明"));
            control.Properties.DataSource = dt;
            control.Properties.ValueMember = key;
            control.Properties.DisplayMember = value;
        }

        /// <summary>
        /// 设置下拉列表数据源
        /// </summary>
        /// <param name="control"></param>        
        /// <param name="dt"></param>
        /// <param name="showHeader"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void SetDataSource(LookUpEdit control, object dt,
            bool showHeader = true, string key = "Id", string value = "Name")
        {
            control.Properties.Columns.Add(new LookUpColumnInfo(value, "名称"));
            if (showHeader)
                control.Properties.Columns.Add(new LookUpColumnInfo("Remark", "说明"));
            control.Properties.DataSource = dt;
            control.Properties.ValueMember = key;
            control.Properties.DisplayMember = value;
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTreeData(decimal currentId = 0)
        {
            //TreeListNode node = treeElement.FocusedNode;
            //decimal id = 0;
            //if (currentId > 0)
            //    id = currentId;
            //else if (node != null)
            //{
            //    id = (decimal)node.GetValue("Id");
            //}

            //_listElements = new DocTemplateElementDAL().
            //    FindByProperty("DocTemplate.Id", _templateModel.Id);

            //treeElement.DataSource = _listElements;

            //if (node == null) return;
            //FindNode(treeElement.Nodes, id);

            _listElements = EntityOper.GetInstance().FindByProperty<DocTemplateElement>("DocTemplate.Id", _templateModel.Id);
            _listElements = _listElements.OrderBy(p => p.ParentId)
                .ThenBy(p => p.SortId).ToList();
            treeElement.DataSource = _listElements;

            if (currentId > 0)
                FindNode(treeElement.Nodes, currentId);
            else if (treeElement.Nodes.Count > 0)
            {
                treeElement.FocusedNode = treeElement.Nodes[0];
                treeElement_MouseClick(null, null);
            }
            else
            {
                ClearControlValue();
            }

        }

        /// <summary>
        /// 设置TreeList默认选中项
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="id"></param>
        private void FindNode(TreeListNodes nodes, decimal id)
        {
            foreach (TreeListNode node in nodes)
            {
                if ((decimal)node.GetValue("Id") == id)
                {
                    treeElement.FocusedNode = node;
                    if (node.HasChildren)
                        node.Expanded = true;
                    return;
                }
                if (node.HasChildren)
                    FindNode(node.Nodes, id);
            }
        }

        /// <summary>
        /// 开始拖拽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeElement_DragEnter(object sender, DragEventArgs e)
        {
            TreeList list = (TreeList)sender;
            TreeListNode node = GetDragNode(e.Data);
            if (node == null) return;
            e.Effect = ModifierKeys == Keys.Control ?
                DragDropEffects.Copy : DragDropEffects.Move;
            //if (node.TreeList != list)
            //{
            //    e.Effect = DragDropEffects.Move;
            //}

            //if (node.ParentNode == null) return;
            //int i = 1;
            //foreach (TreeListNode node1 in node.ParentNode.Nodes)
            //{
            //    node1.SetValue("SortId", i);
            //    i++;
            //    DocTemplateElement model = treeElement.GetDataRecordByNode(node1) as DocTemplateElement;
            //    if (model != null)
            //        EntityOper.GetInstance().Update(model);
            //}
        }



        /// <summary>
        /// 拖拽后鼠标松开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeElement_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                TreeListNode node = GetDragNode(e.Data);
                if (node == null) return;

                TreeList list = (TreeList)sender;
                if (list == null) return;

                return;
                if (ModifierKeys == Keys.Control)
                {
                    DocTemplateElement model = treeElement.GetDataRecordByNode(node) as DocTemplateElement;
                    if (model == null)
                        return;

                    DocTemplateElement modelClone = model.Clone();


                    TreeListHitInfo info = list.CalcHitInfo(list.PointToClient(new Point(e.X, e.Y)));

                    //modelClone.ParentId = info.Node == null ? 0 : info.Node.Id;
                    modelClone.Id = Convert.ToDecimal(EntityOper.GetInstance().Save(modelClone)); ;
                    _listElements.Add(modelClone);
                    treeElement.RefreshDataSource();
                }


                //if (SortNode == null || SortNode.ParentNode != node.ParentNode)
                //    SortChildElement();

                //SortNode = node;

                //node.SetValue("SORT_ID", node.NextNode.GetValue("SORT_ID"));            
                //SortElement(node.ParentNode.Nodes);

                //TreeList list = (TreeList)sender;
                //if (list == node.TreeList) return;

                //TreeListHitInfo info = list.CalcHitInfo(list.PointToClient(new Point(e.X, e.Y)));
                //InsertBrush(list, node, info.Node == null ? -1 : info.Node.Id);

                // node.SetValue("PARENT_ID", node.ParentNode.GetValue("Id"));
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void InsertBrush(TreeList list, TreeListNode node, int parent)
        {
            ArrayList data = new ArrayList();
            //data.Add(node.GetValue(KeyFieldName));
            foreach (TreeListColumn column in node.TreeList.Columns)
            {
                if (column.Name != "Id")
                    data.Add(node[column]);
            }
            parent = list.AppendNode(data.ToArray(), parent).Id;

            if (node.HasChildren)
                foreach (TreeListNode n in node.Nodes)
                    InsertBrush(list, n, parent);
        }

        /// <summary>
        /// 为元素排序
        /// </summary>
        private void SortChildElement()
        {
            if (SortNode == null) return;
            int i = 1;
            if (SortNode.ParentNode == null)
            {
                foreach (TreeListNode node in treeElement.Nodes)
                {
                    node.SetValue("SortId", i);
                    i++;
                    DocTemplateElement model = treeElement.GetDataRecordByNode(node) as DocTemplateElement;
                    if (model != null)
                        EntityOper.GetInstance().Update(model);
                }
                return;
            }

            i = 1;
            foreach (TreeListNode node in SortNode.ParentNode.Nodes)
            {
                node.SetValue("SortId", i);
                i++;
                DocTemplateElement model = treeElement.GetDataRecordByNode(node) as DocTemplateElement;
                if (model != null)
                    EntityOper.GetInstance().Update(model);
            }
        }


        /// <summary>
        /// 获取拖拽对象
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private TreeListNode GetDragNode(IDataObject data)
        {
            return (TreeListNode)data.GetData(typeof(TreeListNode));
        }

        /// <summary>
        /// 添加同级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddSibling_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeElement.Nodes.Count == 0)
                {
                    // 自动创建节点
                    CreateNode(0, 1, null, _templateModel.TemplateName);
                    return;
                }


                TreeNodeClick();

                TreeListNode node = treeElement.FocusedNode;
                if (node != null && node.TreeList != null)
                {
                    int sortId;
                    if (node.ParentNode == null)
                    {
                        sortId = treeElement.Nodes.Count + 1;
                        CreateNode(Convert.ToInt32(node.GetValue("ParentId")),
                       sortId);
                    }
                    else
                    {
                        sortId = node.ParentNode.Nodes.Count + 1;
                        CreateNode(Convert.ToInt32(node.GetValue("ParentId")),
                       sortId, node.ParentNode);
                    }
                }
                else
                    CreateNode();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        /// 添加子级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddChild_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeElement.Nodes.Count == 0)
                {
                    // 自动创建节点
                    CreateNode(0, 1, null, _templateModel.TemplateName);
                    return;
                }

                TreeListNode node = treeElement.FocusedNode;
                if (node != null && node.TreeList != null)
                {
                    CreateNode(
                        Convert.ToInt32(node.GetValue("Id")),
                        node.Nodes.Count + 1, node);

                    node.Expanded = true;
                }
                else
                    CreateNode();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        /// 创建树节点
        /// </summary>
        /// <param name="parentId">父级节点.根节点的父节点为0</param>
        /// <param name="sortId">排序</param>
        /// <param name="node">父级节点元素</param>
        private void CreateNode(int parentId = 0, int sortId = 1, TreeListNode node = null, string name = "")
        {
            SetValueFromControl();

            DocTemplateElement model = new DocTemplateElement();
            //model.Id = _listElements.Count + 100;
            model.ParentId = parentId;
            model.SortId = sortId;
            model.ControlStatusId = (decimal)Enums.ControlStatus.可用;
            model.DataType = Convert.ToByte((int)Enums.DataType.字符串);
            model.CreateTimestamp = GVars.OracleAccess.GetSysDate();
            model.UpdateTimestamp = model.CreateTimestamp;

            if (!string.IsNullOrEmpty(name))
            {
                model.ElementName = model.DisplayName = name;
            }
            else
            {
                if (node == null)
                    model.ElementName = "元素00"
                        + sortId.ToString().PadLeft(2, '0');
                else
                    model.ElementName = "元素"
                    + (node.Level + 1).ToString().PadLeft(2, '0')
                    + sortId.ToString().PadLeft(2, '0');
                model.DisplayName = model.ElementName;
            }

            if (node != null)
            {

                DocControlTemplate controlTemplate =
                   EntityOper.GetInstance().Load<DocControlTemplate>(((DocControlTemplate)(node.GetValue("DocControlTemplate"))).Id);
                if (controlTemplate == null)
                    throw new Exception("无效控件模板!");

                // 如果父级元素是文本,则子元素默认为文本框
                if (controlTemplate.DocControlType.Id == (int)Enums.ControlType.Label)
                {
                    controlTemplate =
                        EntityOper.GetInstance().Load<DocControlTemplate>((decimal)Enums.ControlType.TextBox);
                    if (controlTemplate != null)
                        model.DocControlTemplate = controlTemplate;
                }
                // 如果父级元素是子项单选/多选,则子元素默认为复选框
                else if (controlTemplate.DocControlType.Id == (int)Enums.ControlType.SelectMul ||
                         controlTemplate.DocControlType.Id == (int)Enums.ControlType.SelectSingle)
                {
                    controlTemplate =
                       EntityOper.GetInstance().Load<DocControlTemplate>((decimal)Enums.ControlType.CheckBox);
                    if (controlTemplate != null)
                        model.DocControlTemplate = controlTemplate;
                }

                // 如果无合适控件模板,则和父项保持一致
                if (model.DocControlTemplate == null)
                    model.DocControlTemplate = EntityOper.GetInstance().Load<DocControlTemplate>(((DocControlTemplate)(node.GetValue("DocControlTemplate"))).Id);
            }
            // 如果是根节点，就默认为文本
            else
            {
                model.DocControlTemplate = EntityOper.GetInstance().Load<DocControlTemplate>((decimal)Enums.ControlType.Label);

                model.NewLine = Convert.ToByte(1);
            }
            model.DocTemplate = _templateModel;

            model.Id = Convert.ToDecimal(EntityOper.GetInstance().Save(model));

            _listElements.Add(model);

            SetValueToControl(model);

            _listElements = _listElements.OrderBy(p => p.ParentId)
                .ThenBy(p => p.SortId).ToList();

            treeElement.DataSource = _listElements;

            FindNode(treeElement.Nodes, model.Id);
        }

        /// <summary>
        /// 为模板控件赋值
        /// </summary>
        private void SetValueToHead()
        {
            if (_templateModel != null)
            {
                txtTemplateId.Text = _templateModel.Id.ToString();
                txtTemplateName.Text = _templateModel.TemplateName;
                txtTemplateSerialNo.Text = _templateModel.SerialNo.ToString();//2015.10.20
                txtTemplateScoreMin.Text = _templateModel.MinScore.ToString();//2015.12.17
                txtTemplateScoreMax.Text = _templateModel.MaxScore.ToString();
                cbxIsGlobal.Checked = _templateModel.IsGlobal == 1;
                ddlTemplateClass.EditValue = _templateModel.DocTemplateClass.Id;
                ddlTemplateType.EditValue = (Enums.TemplateType)_templateModel.TemplateTypeId;

                ddlFreq.EditValue = Convert.ToDecimal(_templateModel.Freq);

                if (ddlFreq.EditValue == null || Convert.ToDecimal(ddlFreq.EditValue) == 0)
                    ddlFreq.ItemIndex = 0;
                //ddlFreq.Text1 = _templateModel.Freq;

                InitTreeData();
            }
        }

        /// <summary>
        /// 从模板控件取值
        /// </summary>
        /// <returns></returns>
        private void SetValueFromHead()
        {
            if (_templateModel == null) return;
            _templateModel.TemplateName = txtTemplateName.Text;
            _templateModel.SerialNo = Convert.ToDecimal(txtTemplateSerialNo.Text);//2015.10.20
            _templateModel.MinScore = Convert.ToDecimal(txtTemplateScoreMin.Text);//2015.12.17
            _templateModel.MaxScore = Convert.ToDecimal(txtTemplateScoreMax.Text);
            _templateModel.IsGlobal = Convert.ToByte(cbxIsGlobal.Checked);
            _templateModel.DocTemplateClass.Id = (decimal)ddlTemplateClass.EditValue;
            _templateModel.TemplateTypeId = Convert.ToDecimal(ddlTemplateType.EditValue);
            _templateModel.UpdateTimestamp = GVars.OracleAccess.GetSysDate();
            _templateModel.UpdateUser = GVars.User.UserName;
            _templateModel.Freq = ddlFreq.EditValue == null ? string.Empty : ddlFreq.EditValue.ToString();
        }

        /// <summary>
        /// 从元素控件取值
        /// </summary>
        private void SetValueFromControl()
        {
            if (_currentModel == null) return;

            if (IsWrite)
            {
                //_currentModel = listElements[modelIndex];
                _currentModel.ElementName = txtElementName.Text;

                _currentModel.DisplayName = txtDisplayName.Text.Contains(GridPathInfo) ? txtDisplayName.Text.Substring(GridPathInfo.Length) : txtDisplayName.Text;

                _currentModel.ControlHeight = txtHeight.Value;
                _currentModel.ControlWidth = txtWidth.Value;
                _currentModel.ControlFont = txtElementFont.Text;
                _currentModel.ControlOffset = txtOffset.Value;
                _currentModel.MaxScore = txtTemplateItemScoreMax.Value;//2015.12.18
                _currentModel.MinScore = txtTemplateItemScoreMin.Value;
                //txtLeading.Text1=
                _currentModel.Score = (float)txtScore.Value;
                _currentModel.DataType = Convert.ToByte(ddlDataType.EditValue);
                _currentModel.ChildrenIndent = txtChildrenIndent.Value;
                _currentModel.ControlPrefix = txtPrefix.Text;
                _currentModel.ControlSuffix = txtSuffix.Text;
                _currentModel.ControlStatusId =
                    Convert.ToDecimal(ddlControlStatus.EditValue);
                _currentModel.DocControlTemplate = EntityOper.GetInstance().Load<DocControlTemplate>(
                    (decimal)ddlControlTemplate.EditValue);
                _currentModel.NewLine = Convert.ToByte(cbxNewLine.Checked);
                _currentModel.RelationType = Convert.ToDecimal(ddlRelationType.EditValue);
                _currentModel.RelationCode = Convert.ToDecimal(ddlRelationItem.EditValue);
                _currentModel.UpdateTimestamp = GVars.OracleAccess.GetSysDate();
                _currentModel.RowSpacing = txtRowSpacing.Value;



                EntityOper.GetInstance().Update(_currentModel);

                IsWrite = false;
            }
        }

        /// <summary>
        /// 验证是否为数字
        /// </summary>
        /// <param name="sNumeric"></param>
        /// <returns></returns>
        public bool IsNumeric(string sNumeric)
        {
            return (new Regex("^[\\+\\-]?[0-9]*\\.?[0-9]+$")).IsMatch(sNumeric);
        }

        /// <summary>
        /// 为元素控件赋值
        /// </summary>
        private void SetValueToControl(DocTemplateElement model)
        {
            if (IsWrite) return;

            _currentModel = model;
            txtElementName.Text = _currentModel.ElementName;
            txtDisplayName.Text = _currentModel.DisplayName;
            txtHeight.Value = _currentModel.ControlHeight;
            txtWidth.Value = _currentModel.ControlWidth;
            txtElementFont.Text = _currentModel.ControlFont;
            txtOffset.Value = _currentModel.ControlOffset;
            txtTemplateItemScoreMax.Value = _currentModel.MaxScore;//2015.12.18
            txtTemplateItemScoreMin.Value = _currentModel.MinScore;
            //txtLeading.Text1=
            txtScore.Value = (decimal)_currentModel.Score;
            txtRowSpacing.Value = _currentModel.RowSpacing;

            ddlDataType.EditValue = (Enums.DataType)_currentModel.DataType;
            ddlRelationType.EditValue = (Enums.RelationType)_currentModel.RelationType;
            ddlRelationItem.EditValue = (Enums.RelationPatientItem)_currentModel.RelationCode;
            txtChildrenIndent.Value = _currentModel.ChildrenIndent;
            txtPrefix.Text = _currentModel.ControlPrefix;
            txtSuffix.Text = _currentModel.ControlSuffix;
            ddlControlStatus.EditValue = (Enums.ControlStatus)_currentModel.ControlStatusId;
            ddlControlTemplate.EditValue = _currentModel.DocControlTemplate.Id;

            cbxNewLine.Checked = _currentModel.NewLine == 1;

            if (!string.IsNullOrWhiteSpace(txtElementFont.Text))
            {
                txtDisplayName.Font = DocCommon.FontFromString(txtElementFont.Text);
            }
            else
                txtDisplayName.Font = txtElementName.Font;

            if ((int)(ddlRelationType.EditValue) == (int)Enums.RelationType.表格)
            {
                txtDisplayName.Text = GridPathInfo + txtDisplayName.Text;
            }
            txtElementName.Focus();

            IsWrite = false;
        }

        /// <summary>
        /// 清空控件值
        /// </summary>
        private void ClearControlValue()
        {
            _currentModel = null;
            txtElementName.Text = string.Empty;
            txtDisplayName.Text = string.Empty;
            txtHeight.Value = 0;
            txtWidth.Value = 0;
            txtElementFont.Text = string.Empty;
            txtOffset.Value = 0;
            txtScore.Value = 0;
            txtRowSpacing.Value = 0;

            ddlDataType.ItemIndex = 0;
            ddlRelationType.ItemIndex = 0;
            ddlRelationItem.ItemIndex = 0;
            txtChildrenIndent.Value = 0;
            txtPrefix.Text = string.Empty;
            txtSuffix.Text = string.Empty;
            ddlControlStatus.ItemIndex = 0;
            ddlControlTemplate.ItemIndex = 0;

            cbxNewLine.Checked = false;

            txtDisplayName.Font = txtElementName.Font;

            IsWrite = false;
        }

        private void ValidateRules()
        {
            //dxValidationProvider1.ValidationMode = ValidationMode.Auto;

            //ConditionValidationRule notEmptyValidationRule = new ConditionValidationRule();             notEmptyValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;//验证条件
            //notEmptyValidationRule.ErrorText = "此栏不能为空！";//提示信息              
            //notEmptyValidationRule.ErrorType = ErrorType.Information;//错误提示类别
            //dxValidationProvider1.SetValidationRule(txtHeight,notEmptyValidationRule);            
            ConditionValidationRule rangeValidationRule = new ConditionValidationRule();
            rangeValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;
            rangeValidationRule.Value1 = 0;
            rangeValidationRule.Value2 = 100;
            rangeValidationRule.ErrorText = "0 - 100 之间任意一数字！";
            rangeValidationRule.ErrorType = ErrorType.Warning;
            dxValidationProvider1.SetValidationRule(txtHeight, rangeValidationRule);
            dxValidationProvider1.SetIconAlignment(txtHeight, ErrorIconAlignment.MiddleRight);
        }

        private void ValidateRules(TextEdit textControl, ConditionOperator oper, string errorText)
        {
            ConditionValidationRule rangeValidationRule = new ConditionValidationRule
            {
                ConditionOperator = ConditionOperator.None,
                Value1 = 0,
                Value2 = 100,
                ErrorText = "0 - 100 之间任意一数字！",
                ErrorType = ErrorType.User1
            };
            dxValidationProvider1.SetValidationRule(txtHeight, rangeValidationRule);
            dxValidationProvider1.SetIconAlignment(txtHeight, ErrorIconAlignment.MiddleRight);
        }

        /// <summary>
        /// 获取条件列表
        /// </summary>
        /// <returns></returns>
        private List<ControlRule> SetRules()
        {
            List<ControlRule> rulelist = new List<ControlRule>();
            // 高度
            rulelist.Add(new ControlRule(txtHeight, ControlRule.AddValueRex(StrIsNumeric, false)));
            // 高度
            rulelist.Add(new ControlRule(txtWidth, ControlRule.AddValueRex(StrIsNumeric, false)));

            return rulelist;
        }

        /// <summary>
        /// 为元素排序
        /// </summary>
        private void SortElement(TreeListNodes nodes)
        {
            int i = 1;
            foreach (TreeListNode node in nodes)
            {
                node.SetValue("SortId", i);
                i++;

                if (node.HasChildren)
                    SortElement(node.Nodes);
            }
        }

        /// <summary>
        /// 为元素排序
        /// </summary>
        private void SortElement(TreeListNodes nodes, ref int index)
        {
            int i = 1;
            foreach (TreeListNode node in nodes)
            {
                node.SetValue("SortId", i);
                i++;

                MdiFrm.GetInstance().ReportPercent(10 + (int)(((decimal)i / _listElements.Count) * 20));
                Thread.Sleep(100);
                MdiFrm.GetInstance().MessageInfo = "排序 " + node.GetValue("ElementName");

                if (node.HasChildren)
                    SortElement(node.Nodes, ref index);
            }
        }

        //void DoWithProcess(Action<int> percent)
        //{
        //    try
        //    {
        //        percent(0);
        //        process.MessageInfo = "保存数据";
        //        SetValueFromHead();
        //        EntityOper.GetInstance().Update(_templateModel);

        //        percent(10);
        //        //MdiFrm.GetInstance().MessageInfo = "元素排序";


        //        //int j = 0;

        //        //if (IsDisposed || !this.Parent.IsHandleCreated) return;


        //        ////this.Invoke(new Action(() => SortElement(treeElement.Nodes, ref j)));

        //        //percent(30);
        //        //MdiFrm.GetInstance().MessageInfo = "排序完成";

        //        //percent(40);
        //        SetValueFromControl();

        //      process.MessageInfo = "删除元素";

        //        // 删除元素
        //        if (_listDeleteElements != null)
        //        {
        //            new DocTemplateElementDAL().Delete(_listDeleteElements);
        //            _listDeleteElements = null;
        //        }

        //        //percent(50);
        //        process.MessageInfo = "保存元素";

        //        int i = 0;

        //        foreach (DocTemplateElement model in _listElements)
        //        {
        //            new DocTemplateElementDAL().SaveOrUpdate(model);

        //            percent(10 + (int)(((decimal)i / _listElements.Count) * 80));
        //            process.MessageInfo = "保存 " + _listElements[i].ElementName;
        //            i++;
        //        }

        //        //new DocTemplateElementDAL().SaveOrUpdate(_listElements, ref i);

        //        percent(90);
        //        Thread.Sleep(1000);
        //        process.MessageInfo = "界面刷新";
        //        SaveDept();

        //        if (IsDisposed || !this.Parent.IsHandleCreated)
        //        {
        //            percent(100);
        //            process.MessageInfo = "保存成功!";
        //            Thread.Sleep(200);
        //            return;
        //        }
        //        this.Invoke(new Action(() => InitTreeData(_currentModel == null ? 0 : _currentModel.Id)));

        //        percent(100);

        //        this.Invoke(new Action(() => btnSave.Enabled = true));
        //        //XtraMessageBox.Show("保存成功!");
        //    }
        //    catch (Exception ex)
        //    {
        //        Error.ErrProc(ex);
        //    }
        //}

        void DoWithProcess(Action<int> percent)
        {
            try
            {
                percent(0);
                MdiFrm.GetInstance().MessageInfo = "保存数据";
                SetValueFromHead();
                EntityOper.GetInstance().Update(_templateModel);

                percent(10);
                //MdiFrm.GetInstance().MessageInfo = "元素排序";


                //int j = 0;

                //if (IsDisposed || !this.Parent.IsHandleCreated) return;


                ////this.Invoke(new Action(() => SortElement(treeElement.Nodes, ref j)));

                //percent(30);
                //MdiFrm.GetInstance().MessageInfo = "排序完成";

                //percent(40);
                SetValueFromControl();

                MdiFrm.GetInstance().MessageInfo = "删除元素";

                // 删除元素
                if (_listDeleteElements != null)
                {
                    EntityOper.GetInstance().DeleteList(_listDeleteElements);
                    _listDeleteElements = null;
                }

                //percent(50);
                MdiFrm.GetInstance().MessageInfo = "保存元素";

                int i = 0;

                foreach (DocTemplateElement model in _listElements)
                {
                    EntityOper.GetInstance().SaveOrUpdate(model);

                    percent(10 + (int)(((decimal)i / _listElements.Count) * 80));
                    MdiFrm.GetInstance().MessageInfo = "保存 " + _listElements[i].ElementName;
                    i++;
                }

                //new DocTemplateElementDAL().SaveOrUpdate(_listElements, ref i);

                percent(90);
                Thread.Sleep(1000);
                MdiFrm.GetInstance().MessageInfo = "界面刷新";
                SaveDept();

                if (IsDisposed || !this.Parent.IsHandleCreated)
                {
                    percent(100);
                    MdiFrm.GetInstance().MessageInfo = "保存成功!";
                    Thread.Sleep(200);
                    return;
                }
                this.Invoke(new Action(() => InitTreeData(_currentModel == null ? 0 : _currentModel.Id)));

                percent(100);

                this.Invoke(new Action(() => btnSave.Enabled = true));
                //XtraMessageBox.Show("保存成功!");
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
                btnSave.Enabled = false;
                #region 注释
                //process = new ProcessOperator { BackgroundWork = this.DoWithProcess, MessageInfo = "正在执行中" };
                //process.Start();

                //MdiFrm.GetInstance().RunBackground();

                //MdiFrm.GetInstance().BackgroundWork = this.DoWithProcess;
                //MdiFrm.GetInstance().MessageInfo = "正在执行中";
                //MdiFrm.GetInstance().Start();

                //return;
                //base.LoadingShow();

                ////输入参数基本验证
                //_myValidation.RuleList = SetRules();
                //if (!_myValidation.Validate())
                //    return;
                #endregion

                SetValueFromHead();

                EntityOper.GetInstance().Update(_templateModel);

                //if (IsSort)
                //    SortChildElement();

                //SortElement(treeElement.Nodes);

                SetValueFromControl();

                // 删除元素
                if (_listDeleteElements != null)
                {
                    EntityOper.GetInstance().DeleteList(_listDeleteElements);
                    _listDeleteElements = null;
                }

                //new DocTemplateElementDAL().SaveOrUpdate(_listElements);

                SaveDept();
                InitTreeData(_currentModel == null ? 0 : _currentModel.Id);

                XtraMessageBox.Show("保存成功!");
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                //base.LoadingClose();
                btnSave.Enabled = true;
            }
        }


        private void treeElement_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                // 开启拖拽模式时才会启用双击事件
                if (!cbxDrag.Checked)
                    return;
                treeElement.OptionsBehavior.AllowExpandOnDblClick = false;
                TreeNodeClick();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void treeElement_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e == null || e.Button == MouseButtons.Left)
                {
                    // 关闭拖拽模式时才会启用单击事件
                    if (cbxDrag.Checked)
                        return;

                    treeElement.OptionsBehavior.AllowExpandOnDblClick = true;
                    TreeNodeClick();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    TreeListNode node = treeElement.FocusedNode;
                    if (node == null) return;

                    TreeListHitInfo hitInfo = treeElement.CalcHitInfo(e.Location);

                    //TreeListHitInfo info = list.CalcHitInfo(list.PointToClient(new Point(e.X, e.Y)));

                    if (hitInfo.Node == null) return;

                    hitInfo.Node.Selected = true;
                    popupMenu1.ShowPopup(Control.MousePosition);
                }

            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void TreeNodeClick()
        {
            TreeListNode node = treeElement.FocusedNode;
            if (node == null) return;

            DocTemplateElement model = treeElement.GetDataRecordByNode(node) as DocTemplateElement;
            if (model == null) return;

            if (_currentModel == model)
            {
                txtElementName.Focus();
                return;
            }

            SetValueFromControl();

            SetValueToControl(model);

            // 只有Grid网格才显示表格编辑按钮
            btnGridEdit.Visible = ((Enums.RelationType)model.RelationType == Enums.RelationType.表格);

            if (btnGridEdit.Visible) return;

            TreeListNode parentNode;
            for (parentNode = node; parentNode.ParentNode != null; parentNode = parentNode.ParentNode)
            {
                DocTemplateElement tempModel = treeElement.GetDataRecordByNode(parentNode) as DocTemplateElement;
                if (tempModel == null) return;

                if ((Enums.RelationType)tempModel.RelationType == Enums.RelationType.表格)
                {
                    btnGridEdit.Visible = true;
                    break;
                }
            }
        }

        private void cbxShowId_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxShowId.Checked)
                treeElement.Columns["ElementName"].FieldName = "ElementIdName";
            else
                treeElement.Columns["ElementIdName"].FieldName = "ElementName";
        }

        private void txtElementName_Click(object sender, EventArgs e)
        {
            if (txtElementName.Tag == null)
                txtElementName.Tag = true;
            if ((bool)txtElementName.Tag)
            {
                txtElementName.SelectAll();
                txtElementName.Tag = false;
            }
            else
                txtElementName.Tag = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                base.LoadingShow();

                if (treeElement.FocusedNode == null)
                    return;
                if (treeElement.FocusedNode.HasChildren)
                {
                    XtraMessageBox.Show("无法删除!该元素含有子元素.");
                    return;
                }

                DocTemplateElement model = _listElements.FirstOrDefault(p => p.Id == (decimal)treeElement.FocusedNode.GetValue("Id"));

                if (_listDeleteElements == null)
                    _listDeleteElements = new List<DocTemplateElement>();
                if (model != null && _listDeleteElements.IndexOf(model) == -1)
                    _listDeleteElements.Add(model);
                // 不直接删除,点击保存时才会提交删除
                // new DocTemplateElementDAL().Delete(model);

                treeElement.Nodes.Remove(treeElement.FocusedNode);

                // 清空当前元素
                _currentModel = null;

                if (_listElements.IndexOf(model) > -1)
                    _listElements.Remove(model);

                if (_listElements.Count == 0)
                    ClearControlValue();
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

        /// <summary>
        /// 元素字体变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtElementFont_Click(object sender, EventArgs e)
        {
            txtElementFont.Text = DocCommon.ShowFontDialog(txtElementFont.Text);

            if (!string.IsNullOrWhiteSpace(txtElementFont.Text))
            {
                txtDisplayName.Font = DocCommon.FontFromString(txtElementFont.Text);
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            new DesignTemplate(_templateModel.Id, null, false).ShowDialog();
        }

        private void cbxDrag_CheckedChanged(object sender, EventArgs e)
        {
            //// 拖拽
            treeElement.AllowDrop = cbxDrag.Checked;
            treeElement.OptionsBehavior.DragNodes = cbxDrag.Checked;
            IsSort = true;
        }


        /// <summary>
        /// 高亮选中项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeElement_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            if (e.Node != treeElement.FocusedNode) return;

            e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);

            Rectangle r = new Rectangle(e.EditViewInfo.ContentRect.Left,
                e.EditViewInfo.ContentRect.Top,
                Convert.ToInt32(e.Graphics.MeasureString(e.CellText, treeElement.Font).Width + 1),
                Convert.ToInt32(e.Graphics.MeasureString(e.CellText, treeElement.Font).Height));

            e.Graphics.FillRectangle(SystemBrushes.Highlight, r);

            e.Graphics.DrawString(e.CellText, treeElement.Font, SystemBrushes.HighlightText, r);

            e.Handled = true;
        }

        private void btnNewTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                base.LoadingShow();

                DocTemplate model = new DocTemplate
                {
                    DisplayName = "文书模板" + _listTemplates.Count,
                    TemplateName = "文书模板" + _listTemplates.Count,
                    IsGlobal = 0,
                    DocTemplateClass = new DocTemplateClass { Id = 1 },
                    TemplateTypeId = 1,
                    CreateTimestamp = GVars.OracleAccess.GetSysDate(),
                    UpdateTimestamp = GVars.OracleAccess.GetSysDate(),
                    CreateUser = GVars.User.UserName,
                    UpdateUser = GVars.User.UserName,
                    IsEnabled = 1
                };

                model.Id = (decimal)EntityOper.GetInstance().Save(model);

                _listTemplates.Add(model);

                ucGridView1.DataSource = _listTemplates;

                ucGridView1.SelectRow("Id", model.Id);


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

        private void btnDelTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                base.LoadingShow();

                if (ucGridView1.SelectRowsCount == 0)
                {
                    XtraMessageBox.Show("请选中行后点击删除.");
                    return;
                }

                // 确认删除 您确认要删除当前记录吗?
                if (GVars.Msg.Show("Q0005") != DialogResult.Yes)
                {
                    return;
                }

                object obj = ucGridView1.GetSelectRow();
                if (obj is DocTemplate)
                {
                    DocTemplate template = obj as DocTemplate;
                    if (_listElements.Count > 0)
                    {
                        XtraMessageBox.Show("已添加元素,不可删除!(可置为禁用状态)");
                        return;
                    }
                    // 删除元素
                    if (_listDeleteElements != null)
                    {
                        EntityOper.GetInstance().Delete(_listDeleteElements);
                        _listDeleteElements = null;
                    }

                    // 删除模板科室
                    IList<DocTemplateDept> listDepts = EntityOper.GetInstance().FindByProperty<DocTemplateDept>("DocTemplate.Id", template.Id);

                    if (listDepts.Count > 0)
                    {
                        EntityOper.GetInstance().DeleteList(listDepts);
                    }

                    // 清空当前模板
                    _templateModel = null;

                    EntityOper.GetInstance().Delete(template);

                    _listTemplates.Remove(template);

                    ucGridView1.RefreshData();
                    //ucGridView1.DataSource = _listTemplates;
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

        private void ddlRelationType_EditValueChanged(object sender, EventArgs e)
        {
            if ((int)(ddlRelationType.EditValue) == (int)Enums.RelationType.病人信息)
            {
                ddlRelationItem.Properties.DataSource = Enum.GetValues(typeof(Enums.RelationPatientItem));
            }
            else
                ddlRelationItem.Properties.DataSource = null;

            if ((int)(ddlRelationType.EditValue) == (int)Enums.RelationType.表格)
            {
                txtDisplayName.Properties.ReadOnly = true;
                txtDisplayName.DoubleClick += txtDisplayName_DoubleClick;
            }
            else
            {
                if (txtDisplayName.Properties.ReadOnly)
                {
                    txtDisplayName.Properties.ReadOnly = false;
                    txtDisplayName.DoubleClick -= txtDisplayName_DoubleClick;
                }
            }
        }



        void txtDisplayName_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\Template";
            openFileDialog1.Filter = @"(图片)|*.bmp;*.jpeg;*.jpg;*.png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //获取选定的文件名
                string filename = openFileDialog1.SafeFileName;
                txtDisplayName.Text = GridPathInfo + filename;

                SetValueFromControl();
            }
        }

        private void SaveDept()
        {
            if (!cbxIsGlobal.Checked)
            {
                //listDepts
                IList<DocTemplateDept> list = new List<DocTemplateDept>();
                DocTemplate template = EntityOper.GetInstance().Load<DocTemplate>(_templateModel.Id);
                //System.Data.DataRowView
                foreach (DataRowView item in cbxDept.CheckedItems)
                {
                    DocTemplateDept model = new DocTemplateDept();
                    model.DocTemplate = template;
                    model.DeptCode = item["DEPT_CODE"] as string;

                    DocTemplateDept temp = _listDepts.FirstOrDefault(p => p.DeptCode == model.DeptCode);
                    if (temp != null)
                        model.Id = temp.Id;

                    list.Add(model);

                }
                EntityOper.GetInstance().SaveOrUpdate(list);
            }
        }

        private void txtTemplateName_TextChanged(object sender, EventArgs e)
        {
            //SetValueFromHead();
            if (_templateModel == null) return;
            _templateModel.TemplateName = txtTemplateName.Text;
            ucGridView1.RefreshData();
        }

        private void txtElementName_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (txtDisplayName.Text == e.OldValue.ToString())
                txtDisplayName.Text = e.NewValue.ToString();
        }

        private void txtElementName_TextChanged(object sender, EventArgs e)
        {
            if (_currentModel == null) return;

            if (treeElement.FocusedNode == null)
                return;

            _currentModel.ElementName = txtElementName.Text;

            treeElement.RefreshNode(treeElement.FocusedNode);
            IsWrite = true;
        }

        private void treeElement_AfterDragNode(object sender, NodeEventArgs e)
        {
            int i = 1;
            if (e.Node.ParentNode == null)
            {
                foreach (TreeListNode node in treeElement.Nodes)
                {
                    node.SetValue("SortId", i);
                    i++;
                    DocTemplateElement model = treeElement.GetDataRecordByNode(node) as DocTemplateElement;
                    if (model != null)
                        EntityOper.GetInstance().Update(model);
                }
                return;
            }

            i = 1;
            foreach (TreeListNode node in e.Node.ParentNode.Nodes)
            {
                node.SetValue("SortId", i);
                i++;
                DocTemplateElement model = treeElement.GetDataRecordByNode(node) as DocTemplateElement;
                if (model != null)
                    EntityOper.GetInstance().Update(model);
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                AddSplitLine(false);
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                AddSplitLine();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        /// <summary>
        /// 添加分隔线
        /// </summary>
        private void AddSplitLine(bool isAfter = true)
        {
            TreeListNode node = treeElement.FocusedNode;

            // 取节点位置
            int sortId = Convert.ToInt32(node.GetValue("SortId"));

            // 如果是在节点之前添加，新节点序号就是当前节点，然后把当前节点序号+1
            // 如果在当前节点之后，新节点的序号则为当前节点序号+1
            int newNodeSortId = sortId + (isAfter ? 1 : 0);
            sortId++;
            if (!isAfter)
            {
                node.SetValue("SortId", sortId);
            }
            // 把当前节点后的所有同级节点序号加1，并保存到数据库
            int tmepSortId = sortId;
            IEnumerable<DocTemplateElement> list = _listElements.Where(p => p.ParentId == Convert.ToInt32(node.GetValue("ParentId")) && p.SortId >= tmepSortId);

            var docTemplateElements = list as IList<DocTemplateElement> ?? list.ToList();
            if (docTemplateElements.Any())
            {
                foreach (DocTemplateElement model in docTemplateElements)
                {
                    model.SortId = model.SortId + 1;
                }
                EntityOper.GetInstance().Update<DocTemplateElement>(docTemplateElements);
            }

            // 创建新节点--分隔线
            if (node != null && node.TreeList != null)
            {
                if (node.ParentNode == null)
                {
                    CreateLineNode(Convert.ToInt32(node.GetValue("ParentId")), newNodeSortId);
                }
                else
                {
                    CreateLineNode(Convert.ToInt32(node.GetValue("ParentId")), newNodeSortId);
                }

            }
            else
                CreateLineNode();
        }

        /// <summary>
        /// 创建分隔线节点
        /// </summary>
        /// <param name="parentId">父级节点.根节点的父节点为0</param>
        /// <param name="sortId">排序</param>
        private void CreateLineNode(int parentId = 0, int sortId = 1)
        {
            SetValueFromControl();

            DocTemplateElement model = new DocTemplateElement();
            model.ParentId = parentId;
            model.SortId = sortId;
            model.ControlStatusId = (decimal)Enums.ControlStatus.可用;
            model.DataType = Convert.ToByte((int)Enums.DataType.字符串);
            model.RelationType = Convert.ToDecimal(Enums.RelationType.分隔线);
            model.CreateTimestamp = GVars.OracleAccess.GetSysDate();
            model.UpdateTimestamp = model.CreateTimestamp;

            model.ElementName = "------";

            model.DisplayName = model.ElementName;

            // 如果是根节点，就默认为文本            

            model.DocControlTemplate =
                EntityOper.GetInstance().Load<DocControlTemplate>((decimal)Enums.ControlType.Label);

            model.NewLine = 1;
            model.DocTemplate = _templateModel;

            model.Id = Convert.ToDecimal(EntityOper.GetInstance().Save(model));

            _listElements.Add(model);

            SetValueToControl(model);

            _listElements = _listElements.OrderBy(p => p.ParentId)
                .ThenBy(p => p.SortId).ToList();

            treeElement.DataSource = _listElements;

            FindNode(treeElement.Nodes, model.Id);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //SelectElement s = new SelectElement();
            //s.ShowDialog();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode node = treeElement.FocusedNode;
            if (node != null && node.TreeList != null)
            {
                int sortId;
                if (node.ParentNode == null)
                {
                    sortId = treeElement.Nodes.Count + 1;
                }
                else
                {
                    sortId = node.ParentNode.Nodes.Count + 1;
                }

                SelectElement s = new SelectElement(_templateModel, Convert.ToInt32(node.GetValue("ParentId")), sortId);
                if (s.ShowDialog() == DialogResult.OK)
                {
                    InitTreeData(node.Id);
                }
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode node = treeElement.FocusedNode;
            if (node != null && node.TreeList != null)
            {
                SelectElement s = new SelectElement(_templateModel, Convert.ToInt32(node.GetValue("Id")), node.Nodes.Count + 1);
                if (s.ShowDialog() == DialogResult.OK)
                {
                    InitTreeData(node.Id);
                }
            }
        }

        private void btnDeleteAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                base.LoadingShow();

                if (treeElement.FocusedNode == null)
                    return;
                TreeListNode node = treeElement.FocusedNode;
                if (treeElement.FocusedNode.HasChildren)
                {
                    if (XtraMessageBox.Show("强制删除会连同子元素一起删除，是否确定？", "操作提示", MessageBoxButtons.OKCancel) !=
                        DialogResult.OK)
                        return;
                }

                DocTemplateElement model = _listElements.FirstOrDefault(p => p.Id == (decimal)node.GetValue("Id"));

                if (_listDeleteElements == null)
                    _listDeleteElements = new List<DocTemplateElement>();
                if (model != null && _listDeleteElements.IndexOf(model) == -1)
                    _listDeleteElements.Add(model);
                // 不直接删除,点击保存时才会提交删除
                // new DocTemplateElementDAL().Delete(model);

                if (node.HasChildren)
                    DeleteElements(node.Nodes);

                treeElement.Nodes.Remove(node);

                // 清空当前元素
                _currentModel = null;

                if (_listElements.IndexOf(model) > -1)
                    _listElements.Remove(model);

                // 删除元素
                if (_listDeleteElements != null)
                {
                    EntityOper.GetInstance().Delete(_listDeleteElements);
                    _listDeleteElements = null;
                }

                if (_listElements.Count == 0)
                    ClearControlValue();
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

        private void DeleteElements(TreeListNodes nodes)
        {
            //foreach (TreeListNode node in nodes)
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                TreeListNode node = nodes[i];
                DocTemplateElement model = _listElements.FirstOrDefault(p => p.Id == (decimal)node.GetValue("Id"));

                if (node.HasChildren)
                {
                    DeleteElements(node.Nodes);
                }

                if (model != null)
                {
                    _listDeleteElements.Add(model);
                    treeElement.Nodes.Remove(node);
                    if (_listElements.IndexOf(model) > -1)
                        _listElements.Remove(model);
                }
            }
        }

        private void btnGridEdit_Click(object sender, EventArgs e)
        {
            TreeListNode node = treeElement.FocusedNode;
            if (node == null) return;

            if (((Enums.RelationType)_currentModel.RelationType == Enums.RelationType.表格))
            {
                listElements = new List<DocTemplateElement>();
                listElements.Add(_currentModel);
                GetGridElements(node);

                DesignTemplate a = new DesignTemplate(_templateModel.Id, null, false, true);
                a.ListTemplateElements = listElements;
                if (DialogResult.OK == a.ShowDialog())
                {
                    InitTreeData(_currentModel == null ? 0 : _currentModel.Id);
                }
            }
            else
            {
                TreeListNode parentNode;
                for (parentNode = node; parentNode.ParentNode != null; parentNode = parentNode.ParentNode)
                {
                    DocTemplateElement tempModel = treeElement.GetDataRecordByNode(parentNode) as DocTemplateElement;
                    if (tempModel == null) return;

                    if ((Enums.RelationType)tempModel.RelationType == Enums.RelationType.表格)
                    {
                        listElements = new List<DocTemplateElement>();
                        listElements.Add(tempModel);
                        GetGridElements(parentNode);

                        DesignTemplate a = new DesignTemplate(_templateModel.Id, null, false, true);
                        a.ListTemplateElements = listElements;
                        if (DialogResult.OK == a.ShowDialog())
                        {
                            InitTreeData(tempModel.Id);
                        }

                        break;
                    }
                }
            }
        }

        private List<DocTemplateElement> listElements;
        /// <summary>
        /// 获取选择状态的数据主键ID集合
        /// </summary>
        /// <param name="parentNode">父级节点</param>
        private void GetGridElements(TreeListNode parentNode)
        {
            if (parentNode.Nodes.Count == 0)
            {
                return;//递归终止
            }

            foreach (TreeListNode node in parentNode.Nodes)
            {
                DocTemplateElement model = treeElement.GetDataRecordByNode(node) as DocTemplateElement;
                if (model == null) return;

                listElements.Add(model);
                GetGridElements(node);
            }
        }

        private void txtTemplateSerialNo_TextChanged(object sender, EventArgs e)
        {
            if (_templateModel == null) return;
            _templateModel.SerialNo = Convert.ToDecimal(txtTemplateSerialNo.Text);
            ucGridView1.RefreshData();
        }
    }
}
