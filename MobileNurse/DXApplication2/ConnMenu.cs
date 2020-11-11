using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using HISPlus;

namespace DXApplication2
{
    public partial class ConnMenu : DevExpress.XtraEditors.XtraForm
    {

        /// <summary>
        /// 菜单集合
        /// </summary>
        private IList<MenuManager> _listMenus;

        /// <summary>
        /// 用户自定义右键菜单集合
        /// </summary>
        private IList<UserCardMenu> _listUserCardMenus;

        /// <summary>
        /// 删除列表
        /// </summary>
        private IList<UserCardMenu> _DeleteUserCardMenus;

        /// <summary>
        /// 护理评估父级菜单ID。
        /// 此菜单下为护理评估单，均作特殊处理.
        /// 所有评估单，以下划线作分隔符。
        /// 如评估单类别为42_1，
        /// 评估单为42_1_1。
        /// </summary>
        public const string DocMenuId = "42";

        public ConnMenu()
        {
            InitializeComponent();
        }

        private void ConnMenu_Load(object sender, EventArgs e)
        {
            try
            {
                trlMenus.Add("Id", "Id", false);
                trlMenus.Add("名称", "Name");
                trlMenus.Add("已添加", "Enabled", false, true);
                trlMenus.KeyFieldName = "Id";
                trlMenus.ParentFieldName = "ParentId";
                trlMenus.ImageColumnName = "IconPath";
                trlMenus.AllowDrag = true;
                trlMenus.AllowDrop = false;
                trlMenus.DragFlag = true;

                _listMenus = EntityOper.GetInstance().FindByProperty<MenuManager>("Enabled", (byte)1);

                trlUser.Add("MenuId", "MenuId", false);
                trlUser.Add("名称", "Name");
                trlUser.KeyFieldName = "MenuId";
                trlUser.ParentFieldName = "ParentMenuId";
                trlUser.ImageColumnName = "IconPath";
                trlUser.AllowDrag = true;
                trlUser.AllowDrop = true;

                _listUserCardMenus = EntityOper.GetInstance().FindByProperty<UserCardMenu>("UserId", GVars.User.ID);
                InitDoc();
                trlMenus.DataSource = _listMenus;

                Init();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void InitDoc()
        {
            _listMenus = _listMenus.Where(p => p.ParentId != DocMenuId).OrderBy(p => p.ParentId).ThenBy(p => p.SortId).ToList();
            
            const string filter = "MODULE_CODE = '{0}'";
            foreach (MenuManager model in _listMenus)
            {
                DataRow[] drFind = GVars.User.Rights.Tables[0].Select(string.Format(filter, model.ModuleCode));
                if (model.ParentId != "0" && drFind.Length == 0)
                    model.Enabled = 0;
            }

            _listMenus = _listMenus.Where(p => p.Enabled == (byte)1).OrderBy(p => p.ParentId).ThenBy(p => p.SortId).ToList();

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
                    Enabled = 1,
                    ParentId = templateClass.ParentId == 0 ? DocMenuId : parentNodeId
                };

                DocTemplateClass @class = templateClass;

                IList<DocTemplate> docTemplates = listElements.Where(p => p.DocTemplateClass.Id == @class.Id).ToList();

                if (docTemplates.Any())
                    _listMenus.Add(menuManager);
                else
                    continue;

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

        private void Init()
        {
            if (_listUserCardMenus == null)
                _listUserCardMenus = new List<UserCardMenu>();

            if (_listUserCardMenus.Count == 0)
            {
                trlUser.DataSource = _listUserCardMenus;
                return;
            }

            foreach (UserCardMenu userMenu in _listUserCardMenus)
            {
                UserCardMenu menu = userMenu;
                MenuManager model = _listMenus.FirstOrDefault(p => p.Id == menu.MenuId );
                if (model == null) continue;

                menu.Name = model.Name;
                menu.IconPath = model.IconPath;

                // 已添加的菜单加删除线标识
                model.Enabled = 0;
            }

            _listUserCardMenus = _listUserCardMenus.Where(p => !string.IsNullOrEmpty(p.Name)).OrderBy(p => p.ParentMenuId).ThenBy(p => p.SortId).ToList();

            trlUser.DataSource = _listUserCardMenus;
            trlUser.ExpandAll();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;

                if (_listUserCardMenus == null)
                    _listUserCardMenus = new List<UserCardMenu>();

                if (trlUser.Nodes.Count == 0) return;

                //SaveNodes(trlUser.Nodes);

                if (_listUserCardMenus.Count == 0) return;

                SortNodes(trlUser.Nodes);

                EntityOper.GetInstance().SaveOrUpdate<UserCardMenu>(_listUserCardMenus);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void SaveNodes(TreeListNodes nodes)
        {
            int sortId = 0;
            foreach (TreeListNode node
                in nodes)
            {
                if (node.HasChildren)
                    SaveNodes(node.Nodes);

                sortId++;
                TreeListNode node1 = node;
                string id = node1.GetValue("Id") as string;
                if (_listUserCardMenus.Count(p => p.MenuId == id) > 0) continue;
                UserCardMenu userMenu = new UserCardMenu
                {
                    MenuId = id,
                    ParentMenuId = (node1.ParentNode == null ? "0" : node1.ParentNode.GetValue("Id").ToString()),
                    UserId = GVars.User.ID,
                    DeptCode = GVars.User.DeptCode,
                    SortId = sortId
                };

                _listUserCardMenus.Add(userMenu);
            }
        }

        private void SortNodes(TreeListNodes nodes)
        {
            int sortId = 1;
            foreach (TreeListNode node
                in nodes)
            {
                if (node.HasChildren)
                    SortNodes(node.Nodes);

                node.SetValue("SortId", sortId);

                sortId++;
            }
        }


        private void trlUser_AfterDragNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {

        }

        private TreeListNode DragNode;

        private void trlUser_DragDrop(object sender, DragEventArgs e)
        {
            DragNode = (TreeListNode)e.Data.GetData(typeof(TreeListNode));

            if (DragNode.TreeList.Parent == trlUser) return;

            btnSave.Enabled = true;

            CopyMenu(DragNode);

            //DragNode.ExpandAll();

            trlUser.RefreshData();
            trlMenus.RefreshData();
        }

        private void CopyMenu(TreeListNode node)
        {
            string id = node.GetValue("Id").ToString();
            if (string.IsNullOrEmpty(id)) return;
            MenuManager model = _listMenus.FirstOrDefault(p => p.Id == id);
            if (model == null) return;

            if (_listUserCardMenus.Count(p => p.MenuId == id) > 0) return;

            //node.SetValue("MenuId", model.Name);
            //node.SetValue("Name", model.Name);
            //node.SetValue("IconPath", model.IconPath);

            string parentId = "0";

            if (node.ParentNode != null)
            {
                UserCardMenu menu =
                    _listUserCardMenus.FirstOrDefault(p => p.MenuId == node.ParentNode.GetValue("Id").ToString());
                if (menu != null)
                    parentId = menu.MenuId;
            }

            UserCardMenu userMenu = new UserCardMenu
            {
                MenuId = id,
                ParentMenuId = parentId,
                UserId = GVars.User.ID,
                DeptCode = GVars.User.DeptCode
            };

            userMenu.Name = model.Name;
            userMenu.IconPath = model.IconPath;


            // 已添加的菜单加删除线标识
            model.Enabled = 0;

            _listUserCardMenus.Add(userMenu);

            if (node.HasChildren)
                foreach (TreeListNode nodeChild in node.Nodes)
                    CopyMenu(nodeChild);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (trlUser.SelectedNode != null)
                {
                    if (trlUser.SelectedNode.HasChildren)
                    {
                        DialogResult result = XtraMessageBox.Show("是否同时删除子菜单?", "提示", MessageBoxButtons.YesNoCancel);
                        if (result == DialogResult.Cancel)
                            return;
                        DeleteNodes(trlUser.SelectedNode, result == DialogResult.Yes);
                    }
                    else
                        DeleteNodes(trlUser.SelectedNode, false);

                    trlUser.RefreshData();
                    trlMenus.RefreshData();
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }



        private void DeleteNodes(TreeListNode node, bool deleteAll = false)
        {
            UserCardMenu model = trlUser.GetDataRecordByNode(node) as UserCardMenu;
            if (model == null) return;
            EntityOper.GetInstance().Delete(model);
            _listUserCardMenus.Remove(model);
            // 如果包含子项，是否一块删除？
            if (node.HasChildren)
                if (deleteAll)
                    DeleteModel(model);
                else
                {
                    // 如果子项不删除，就把其父级ID清除
                    IList<UserCardMenu> cardMenus =
                        _listUserCardMenus.Where(p => p.ParentMenuId == model.MenuId).ToList();
                    foreach (UserCardMenu cardMenu in cardMenus)
                    {
                        cardMenu.ParentMenuId = "0";
                    }
                }
            else
            {
                MenuManager menuManager = _listMenus.FirstOrDefault(p => p.Id == model.MenuId);
                if (menuManager != null)
                    menuManager.Enabled = 1;
            }
        }

        private void DeleteModel(UserCardMenu model)
        {
            IList<UserCardMenu> cardMenus = _listUserCardMenus.Where(p => p.ParentMenuId == model.MenuId).ToList();

            if (cardMenus.Any())
            {
                for (int i = cardMenus.Count() - 1; i >= 0; i--)
                {
                    DeleteModel(cardMenus[i]);
                }

                // 在删除时，如果数据已变更，需要重新从数据库提取，以免异常
                if (model.Id > 0)
                {
                    UserCardMenu u = EntityOper.GetInstance().Get<UserCardMenu>(model.Id);
                    if (u != null)
                        EntityOper.GetInstance().Delete(u);
                }
                _listUserCardMenus.Remove(model);
                MenuManager menuManager = _listMenus.FirstOrDefault(p => p.Id == model.MenuId);
                if (menuManager != null)
                    menuManager.Enabled = 1;
            }
            else
            {
                // 在删除时，如果数据已变更，需要重新从数据库提取，以免异常
                if (model.Id > 0)
                {
                    UserCardMenu u = EntityOper.GetInstance().Get<UserCardMenu>(model.Id);
                    if (u != null)
                        EntityOper.GetInstance().Delete(u);
                }

                _listUserCardMenus.Remove(model);
                MenuManager menuManager = _listMenus.FirstOrDefault(p => p.Id == model.MenuId);
                if (menuManager != null)
                    menuManager.Enabled = 1;
            }
        }
    }
}