using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using HISPlus.UserControls;

namespace HISPlus
{
    public partial class ModuleManagerFrm : FormDo
    {
        #region 窗体变量
        private ApplicationMangerCom com = null;
        private DataSet dsModule = null;
        private IList<MenuManager> _listMenus;

        /// <summary>
        /// 是否为新增数据的标志
        /// </summary>
        private bool IsNew = false;

        /// <summary>
        /// 当前操作的实体
        /// </summary>
        private MenuManager currentModel;

        /// <summary>
        /// 默认应用程序编码
        /// </summary>
        private const string AppCode = "001";

        /// <summary>
        /// 护理评估父级菜单ID。
        /// 此菜单下为护理评估单，均作特殊处理.
        /// 所有评估单，以下划线作分隔符。
        /// 如评估单类别为42_1，
        /// 评估单为42_1_1。
        /// </summary>
        private const string DocMenuId = "42";

        /// <summary>
        /// 禁止编辑的文本，如果禁止编辑，就自动把控件置为只读
        /// </summary>
        private const string DisabledText = @"不可编辑";
        #endregion

        public ModuleManagerFrm()
        {
            InitializeComponent();

            _id = "00001";
            _guid = "792dd125-87f0-442d-aecc-fefa89621120";
        }


        #region 窗体事件
        private void ModuleManagerFrm_Load(object sender, EventArgs e)
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


        /// <summary>
        /// 按钮[新增]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                initDisp_ModuleDetail();

                this.btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
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
                // 条件检查
                if (txtModuleCode.Text.Trim().Length == 0)
                {
                    txtModuleCode.Focus();
                    return;
                }

                if (txtModuleCode.ReadOnly)
                {
                    XtraMessageBox.Show("护理评估单不可删除！");
                    return;
                }

                // 询问
                if (GVars.Msg.Show("Q0004", "模块") != DialogResult.Yes)       // 您确认要删除当前{0}吗?
                {
                    return;
                }

                // 如果是新增,直接清空即可
                if (currentModel == null || string.IsNullOrEmpty(currentModel.Id))
                {
                    initDisp_ModuleDetail();
                    return;
                }

                // 删除
                //string guid = txtFormGuid.Text1.Trim();
                //DataRow[] drFind = dsModule.Tables[0].Select("FORM_GUID = " + SqlManager.SqlConvert(guid));

                EntityOper.GetInstance().Delete(currentModel);
                _listMenus = com.GetMenus(AppCode);
                ucTreeList1.DataSource = _listMenus;
                ucTreeList1.RefreshData();
                ucTreeList1.SelectFirst();

                //string moduleCode = txtModuleCode.Text.Trim();
                //DataRow[] drFind = dsModule.Tables[0].Select("MODULE_CODE = " + SqlManager.SqlConvert(moduleCode));

                //if (drFind.Length > 0)
                //{
                //    drFind[0].Delete();

                //    com.SaveModuleList(ref dsModule);

                //    // 刷新显示
                //    dsModule = com.GetModuleList();
                //    ucTreeList1.DataSource = dsModule.Tables[0].DefaultView;
                //}

                this.btnSave.Enabled = false;
            }
            catch (Exception ex)
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
            try
            {
                if (chkDisp() == false)
                {
                    return;
                }

                if (txtModuleCode.ReadOnly && currentModel != null)
                {
                    string nodeId = currentModel.Id;

                    if (nodeId != null && nodeId.Contains(ComConst.STR.UnderLine))
                    {
                        string templateId = nodeId.Substring(nodeId.LastIndexOf(ComConst.STR.UnderLine) + 1);

                        if (string.IsNullOrEmpty(templateId)) return;

                        // 评估单模板类别
                        if (nodeId.IndexOf(ComConst.STR.UnderLine,nodeId.IndexOf(ComConst.STR.UnderLine)+1 ) < 0)
                        {
                            DocTemplateClass templateClass =
                                EntityOper.GetInstance().Get<DocTemplateClass>(Convert.ToDecimal(templateId));
                            if (templateClass == null) return;

                            templateClass.Name = txtModuleName.Text.Trim();
                            templateClass.Enabled = Convert.ToByte(chkEnabled.Checked);
                            EntityOper.GetInstance().Update(templateClass);
                            IList<DocTemplate> list =
                                EntityOper.GetInstance()
                                    .FindByProperty<DocTemplate>("DocTemplateClass.Id", Convert.ToDecimal(templateId));
                            if (list.Count == 0) return;

                            foreach (DocTemplate model in list)
                            {
                                model.IsEnabled = templateClass.Enabled;
                            }
                            EntityOper.GetInstance().Update(list);

                            IList<MenuManager> list2 = _listMenus.Where(p => p.ParentId == nodeId).ToList();
                            foreach (MenuManager model in list2)
                            {
                                model.Enabled = templateClass.Enabled;
                            }
                        }
                            // 评估单模板
                        else
                        {
                            DocTemplate template = EntityOper.GetInstance().Get<DocTemplate>(Convert.ToDecimal(templateId));

                            if (template == null)
                                return;

                            template.TemplateName = txtModuleName.Text.Trim();
                            template.IsEnabled = Convert.ToByte(chkEnabled.Checked);

                            EntityOper.GetInstance().Update(template);
                        }

                        currentModel.Name = txtModuleName.Text.Trim();
                        currentModel.Enabled = Convert.ToByte(chkEnabled.Checked);
                        ucTreeList1.RefreshData();
                    }
                }
                else
                {
                    bool isNew = currentModel == null;
                    // 保存数据到缓存
                    saveDisp();
                    string id = string.Empty;
                    if (isNew && currentModel != null)
                        id = currentModel.Id;
                    _listMenus = com.GetMenus(AppCode);
                    ucTreeList1.RefreshData();

                    if (!string.IsNullOrEmpty(id))
                        ucTreeList1.SelectRow(id);
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
            finally
            {
                this.btnSave.Enabled = false;
            }
        }
        #endregion


        #region 共通函数
        private void initFrmVal()
        {
            com = new ApplicationMangerCom();

            dsModule = com.GetModuleList();

            _listMenus = com.GetMenus(AppCode).OrderBy(p => p.ParentId).ThenBy(p => p.SortId).ToList();
        }


        private void initDisp()
        {
            //ucGridView1.ShowRowIndicator = true;
            //ucGridView1.Add("模块编码", "ModuleCode");
            //ucGridView1.Add("模块名称", "Name");
            //ucGridView1.Add("版本号", "Version");
            //ucGridView1.Add("动态库", "Assembly");
            //ucGridView1.Add("类名", "FormName");
            //ucGridView1.Add("备注", "Remark");
            //ucGridView1.Add("是否可用", "Enabled", new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit());

            ucTreeList1.KeyFieldName = "Id";
            ucTreeList1.ParentFieldName = "ParentId";

            ucTreeList1.Add("模块名称", "Name");
            ucTreeList1.Add("模块编码", "ModuleCode");
            ucTreeList1.Add("版本号", "Version");
            ucTreeList1.Add("动态库", "Assembly");
            ucTreeList1.Add("类名", "FormName");
            ucTreeList1.Add("备注", "Remark");
            //ucTreeList1.Add("是否可用", "Enabled");
            ucTreeList1.Add("是否可用", "Enabled", true, true);

            ucTreeList1.AllowDrag = true;
            ucTreeList1.LevelLimit = 2;

            ucTreeList1.ImageColumnName = "IconPath";

            InitDoc();
            ucTreeList1.DataSource = _listMenus;
        }

        private void InitDoc()
        {
            _listMenus = _listMenus.Where(p => p.ParentId != DocMenuId).OrderBy(p => p.ParentId).ThenBy(p => p.SortId).ToList();

            IList<DocTemplateClass> listTemplateClass = EntityOper.GetInstance().LoadAll<DocTemplateClass>();
            IList<DocTemplate> listElements = EntityOper.GetInstance().LoadAll<DocTemplate>();

            IList<DocTemplateDept> listDepts = EntityOper.GetInstance().FindByProperty<DocTemplateDept>("DeptCode", GVars.User.DeptCode);

            listElements =
                listElements.Where(p => p.IsGlobal == 1 || listDepts.Any(q => q.DocTemplate.Id == p.Id)).ToList();

            if (listElements.Count == 0) return;

            foreach (DocTemplateClass templateClass in listTemplateClass.OrderBy(p => p.ParentId).ThenBy(p => p.SortId))
            {
                string nodeId = string.Format("{0}{1}{2}", DocMenuId, ComConst.STR.UnderLine, templateClass.Id);
                string parentNodeId = string.Format("{0}{1}{2}", DocMenuId, ComConst.STR.UnderLine, templateClass.ParentId);

                MenuManager menuManager = new MenuManager
                {
                    Id = nodeId,
                    Name = templateClass.Name,
                    Enabled = templateClass.Enabled,
                    ParentId = templateClass.ParentId == 0 ? DocMenuId : parentNodeId,
                    ModuleCode = DisabledText,
                    FormName = DisabledText,
                    Assembly = DisabledText,
                    Rights = DisabledText,
                    Remark = DisabledText,
                    Version = DisabledText,
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
                        Enabled = template.IsEnabled,
                        ModuleCode = DisabledText,
                        FormName = DisabledText,
                        Assembly = DisabledText,
                        Rights = DisabledText,
                        Remark = DisabledText,
                        Version = DisabledText,
                    };

                    _listMenus.Add(menuManager);
                }
            }
        }

        private void initDisp_ModuleDetail()
        {
            this.txtModuleId.Text = @"自动生成";
            this.txtModuleName.Text = string.Empty;         // 模块名称   
            this.chkEnabled.Checked = true;                 // 启用

            this.txtModuleCode.Text = string.Empty;         // 模块编码
            this.txtVersion.Text = string.Empty;         // 版本号
            this.txtDllName.Text = string.Empty;         // 动态库名
            this.txtTypeName.Text = string.Empty;         // 类名        
            this.txtRights.Text = string.Empty;         // 权限列表
            this.txtMemo.Text = string.Empty;         // 备注

            if (txtModuleCode.ReadOnly)
            {
                txtModuleCode.ReadOnly = false;
                txtVersion.ReadOnly = false;
                txtDllName.ReadOnly = false;
                txtTypeName.ReadOnly = false;
                txtRights.ReadOnly = false;
                txtMemo.ReadOnly = false;
            }

            this.picIcon.Tag = string.Empty;         // 图标
            this.picIcon.Image = null;

            this.txtModuleCode.Focus();
            currentModel = null;
        }

        /// <summary>
        /// 显示当前实体
        /// </summary>
        private void ShowModuleDetail()
        {
            MenuManager model = currentModel;

            this.txtModuleId.Text = model.Id;
            // 模块编码
            this.txtModuleCode.Text = model.ModuleCode;
            // 模块名称
            this.txtModuleName.Text = model.Name;
            // 版本号
            this.txtVersion.Text = model.Version;
            // 动态库名
            this.txtDllName.Text = model.Assembly;
            // 类名
            this.txtTypeName.Text = model.FormName;
            // 启用
            this.chkEnabled.Checked = model.Enabled == 1;

            //this.txtParameter.Text =  // 参数
            // 权限列表
            this.txtRights.Text = model.Rights;
            // 备注
            this.txtMemo.Text = model.Remark;

            if (txtModuleCode.Text == DisabledText && txtTypeName.Text == DisabledText &&
                txtVersion.Text == DisabledText && txtRights.Text == DisabledText && txtMemo.Text == DisabledText &&
                txtDllName.Text == DisabledText)
            {
                txtModuleCode.ReadOnly = true;
                txtVersion.ReadOnly = true;
                txtDllName.ReadOnly = true;
                txtTypeName.ReadOnly = true;
                txtRights.ReadOnly = true;
                txtMemo.ReadOnly = true;
            }
            else if (txtModuleCode.ReadOnly)
            {
                txtModuleCode.ReadOnly = false;
                txtVersion.ReadOnly = false;
                txtDllName.ReadOnly = false;
                txtTypeName.ReadOnly = false;
                txtRights.ReadOnly = false;
                txtMemo.ReadOnly = false;
            }

            // 图标
            picIcon.Image = null;
            if (string.IsNullOrEmpty(model.IconPath)) return;

            string fileName = Path.Combine(Application.StartupPath, model.IconPath);
            if (File.Exists(fileName))
            {
                picIcon.Image = Image.FromFile(fileName);
                picIcon.Tag = model.IconPath;
            }
        }


        private bool chkDisp()
        {
            if (txtModuleCode.Text.Trim().Length == 0)
            {
                txtModuleCode.Focus();
                
                return false;
            }

            if (txtModuleName.Text.Trim().Length == 0)
            {
                txtModuleName.Focus();
                return false;
            }

            // 模块编码为0时，表示为父节点
            if (txtModuleCode.Text != @"0")
            {
                //if (txtDllName.Text.Trim().Length == 0)
                //{
                //    txtDllName.Focus();
                //    return false;
                //}

                if (txtTypeName.Text.Trim().Length == 0)
                {
                    txtTypeName.Focus();
                    return false;
                }
            }

            return true;
        }


        private bool saveDisp()
        {
            // 获取当前日期
            DateTime dtNow = GVars.OracleAccess.GetSysDate();

            bool isNew = currentModel == null;
            // 默认为新增
            string moduleId = string.Empty;
            if (isNew)
            {
                currentModel = new MenuManager();
                _listMenus.Add(currentModel);
            }
            // 保存数据
            currentModel.ModuleCode = this.txtModuleCode.Text.Trim();                       // 模块编码
            currentModel.Name = this.txtModuleName.Text.Trim();                       // 模块名称
            currentModel.Version = this.txtVersion.Text.Trim();                          // 版本号
            currentModel.Assembly = this.txtDllName.Text.Trim();                          // 动态库名
            currentModel.FormName = this.txtTypeName.Text.Trim();                         // 类名
            currentModel.Enabled = Convert.ToByte(this.chkEnabled.Checked ? 1 : 0);                  // 启用            
            currentModel.Rights = this.txtRights.Text.Trim();                           // 权限列表
            currentModel.Remark = this.txtMemo.Text.Trim();                             // 备注
            currentModel.AppCode = AppCode;

            currentModel.IconPath = picIcon.Tag == null ? string.Empty : picIcon.Tag.ToString();           // 图标文件

            if (isNew)
                currentModel.Id = EntityOper.GetInstance().Save(currentModel).ToString();
            else
                EntityOper.GetInstance().Update(currentModel);
            return true;
        }


        private void locate(string moduleCode)
        {
            // ucTreeList1.SelectedNode("ModuleCode", moduleCode);
            //// 定位
            //if (moduleCode.Length > 0)
            //{
            //    foreach (DataGridViewRow dr in dgvModule.Rows)
            //    {
            //        if (dr.Cells["MODULE_CODE"].Value.ToString().Equals(moduleCode) == true)
            //        {
            //            dr.Selected = true;
            //            break;
            //        }
            //    }
            //}
        }
        #endregion

        private void ucTreeList1_AfterSelect(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            bool blnStore = GVars.App.UserInput;

            try
            {
                GVars.App.UserInput = false;    // 为了避免 触发文本框编辑事件

                // 初始化 应用程序详细信息的显示
                initDisp_ModuleDetail();

                if (ucTreeList1.SelectRowData == null)
                {
                    return;
                }

                MenuManager model = ucTreeList1.SelectRowData as MenuManager;
                if (model == null) return;

                currentModel = model;

                ShowModuleDetail();

                this.btnSave.Enabled = false;
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
        /// 按钮[图标]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectIcon_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.DefaultExt = "ico";
                openFileDialog1.InitialDirectory = Application.StartupPath;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog1.FileName.Length > 0 && openFileDialog1.CheckFileExists)
                    {
                        string fileName = openFileDialog1.FileName;
                        int pos = fileName.IndexOf(Application.StartupPath);

                        if (pos >= 0)
                        {
                            fileName = fileName.Substring(Application.StartupPath.Length + 1,
                                fileName.Length - Application.StartupPath.Length - 1);
                        }
                        else
                        {
                            fileName = Path.GetFileName(fileName);
                        }

                        picIcon.Tag = fileName;
                        picIcon.Image = Image.FromFile(openFileDialog1.FileName);

                        this.btnSave.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void ucTreeList1_AfterDragNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            int i = 1;
            if (e.Node.ParentNode == null)
            {
                foreach (TreeListNode node in ucTreeList1.Nodes)
                {
                    node.SetValue("SortId", i);
                    i++;
                    MenuManager model = ucTreeList1.GetRow(node) as MenuManager;
                    if (model != null)
                        EntityOper.GetInstance().Update(model);
                }
                return;
            }

            foreach (TreeListNode node in e.Node.ParentNode.Nodes)
            {
                node.SetValue("SortId", i);
                i++;
                MenuManager model = ucTreeList1.GetRow(node) as MenuManager;
                if (model != null)
                    EntityOper.GetInstance().Update(model);
            }
        }
    }
}
