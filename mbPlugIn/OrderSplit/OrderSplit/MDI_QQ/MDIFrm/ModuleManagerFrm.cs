using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;

namespace HISPlus
{
    public partial class ModuleManagerFrm : FormDo
    {
        #region 窗体变量
        private DataSet dsModule = null;

        /// <summary>
        /// 是否为新增数据的标志
        /// </summary>
        private bool IsNew = false;

        private bool IsChanged = false;

        private readonly List<MenuManager> _listMenus = new List<MenuManager>();

        /// <summary>
        /// 当前操作的实体
        /// </summary>
        private MenuManager currentModel;

        private DataSet _dsMenu;

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
                _listMenus.Remove(currentModel);
                currentModel = null;
                //ucTreeList1.DataSource = _listMenus;
                ucTreeList1.RefreshData();
                //ucTreeList1.SelectFirst();

                this.btnSave.Enabled = true;
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

                bool isNew = currentModel == null;
                // 保存数据到缓存
                SaveDisp();

                SaveXml();

                string id = string.Empty;
                if (isNew && currentModel != null)
                    id = currentModel.Id;

                ucTreeList1.RefreshData();

                if (!string.IsNullOrEmpty(id))
                    ucTreeList1.SelectRow(id);
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
            // 从Menu.xml中读取菜单信息并写入数据库
            // 在MenuManager.hbm.xml中设置主键ID的生成方式为手动写入:<generator class="assigned" />
            // 数据写入完成后,记得把ID生成方式还原
            _dsMenu = new DataSet();
            _dsMenu.ReadXml("Menu.xml");

            DataView dv = new DataView { Table = _dsMenu.Tables[0], Sort = "ParentId, SERIAL_NO" };
            foreach (DataRowView drv in dv)
            {
                MenuManager m = new MenuManager
                {
                    Id = drv["NodeId"].ToString(),
                    ParentId = Convert.ToString(drv["ParentId"]),
                    Enabled = drv["Enabled"] == DBNull.Value ? (byte)0 : Convert.ToByte(drv["Enabled"]),
                    IconPath = drv["Icon"].ToString(),
                    Name = drv["NodeName"].ToString(),
                    IniPath = drv["IniPath"].ToString(),
                    OpenMode = drv["WIN_OPEN_MODE"].ToString(),
                    SortId = drv["SERIAL_NO"] == null || drv["SERIAL_NO"] == DBNull.Value
                        ? 0
                        : Convert.ToDecimal(drv["SERIAL_NO"])
                };

                _listMenus.Add(m);
            }
        }


        private void initDisp()
        {
            ucTreeList1.KeyFieldName = "Id";
            ucTreeList1.ParentFieldName = "ParentId";

            ucTreeList1.Add("模块名称", "Name");
            ucTreeList1.Add("配置文件", "IniPath");
            ucTreeList1.Add("备注", "Remark");
            //ucTreeList1.Add("是否可用", "Enabled");
            ucTreeList1.Add("是否可用", "Enabled", true, true);

            ucTreeList1.AllowDrag = true;
            ucTreeList1.LevelLimit = 2;

            ucTreeList1.ImageColumnName = "IconPath";

            ucTreeList1.DataSource = _listMenus;
        }

        private void initDisp_ModuleDetail()
        {
            this.txtModuleId.Text = @"自动生成";
            this.txtModuleName.Text = string.Empty;         // 模块名称   
            this.chkEnabled.Checked = true;                 // 启用

            this.txtIniPath.Text = string.Empty;
            this.txtIniPath.Text = string.Empty;         // 类名        
            this.txtMemo.Text = string.Empty;         // 备注

            this.picIcon.Tag = string.Empty;         // 图标
            this.picIcon.Image = null;

            currentModel = null;
        }

        /// <summary>
        /// 显示当前实体
        /// </summary>
        private void ShowModuleDetail()
        {
            MenuManager model = currentModel;

            this.txtModuleId.Text = model.Id;
            // 模块名称
            this.txtModuleName.Text = model.Name;
            // 类名
            this.txtIniPath.Text = model.IniPath;
            // 启用
            this.chkEnabled.Checked = model.Enabled == 1;

            //this.txtParameter.Text =  // 参数
            // 备注
            this.txtMemo.Text = model.Remark;

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
            if (txtModuleName.Text.Trim().Length == 0)
            {
                txtModuleName.Focus();
                return false;
            }
            return true;
        }


        private void SaveXml()
        {
            IsChanged = true;

            _dsMenu.Tables[0].Clear();

            if (_listMenus.Count > 0)
            {
                foreach (MenuManager model in _listMenus)
                {
                    DataRow drEdit = _dsMenu.Tables[0].NewRow();
                    // 保存数据
                    //drEdit["MODULE_CODE"] = model.ModuleCode;                       // 模块编码
                    drEdit["NODENAME"] = model.Name;                       // 模块名称
                    //drEdit["MODULE_VERSION"] = this.txtVersion.Text.Trim();                          // 版本号
                    drEdit["IniPath"] = model.IniPath;                         // 类名
                    drEdit["ENABLED"] = model.Enabled == 1;                  // 启用                                       
                    drEdit["MEMO"] = model.Remark;                             // 备注
                    drEdit["SERIAL_NO"] = model.SortId;                             // 备注
                    drEdit["NodeId"] = model.Id;                             // 备注
                    drEdit["ParentId"] = model.ParentId;                             // 备注
                    drEdit["WIN_OPEN_MODE"] = model.OpenMode ?? "0";                             // 备注
                    drEdit["Icon"] = model.IconPath;                             // 备注

                    _dsMenu.Tables[0].Rows.Add(drEdit);
                }

                _dsMenu.WriteXml("menu.xml", XmlWriteMode.WriteSchema);
            }
        }

        private void SaveDisp()
        {
            bool isNew = currentModel == null;
            // 默认为新增
            string moduleId = string.Empty;
            if (isNew)
            {
                currentModel = new MenuManager();
                currentModel.Id = Guid.NewGuid().ToString();
                currentModel.ParentId = "0";
                _listMenus.Add(currentModel);
            }
            // 保存数据
            currentModel.Name = this.txtModuleName.Text.Trim();                       // 模块名称
            currentModel.IniPath = this.txtIniPath.Text.Trim();                         // 类名
            currentModel.Enabled = Convert.ToByte(this.chkEnabled.Checked ? 1 : 0);                  // 启用            
            currentModel.Remark = this.txtMemo.Text.Trim();                             // 备注            

            currentModel.IconPath = picIcon.Tag == null ? string.Empty : picIcon.Tag.ToString();           // 图标文件                            
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

                if (currentModel != null)
                    SaveDisp();

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
                    node.SetValue("ParentId", 0);
                    i++;
                }
            }
            else
                foreach (TreeListNode node in e.Node.ParentNode.Nodes)
                {
                    node.SetValue("SortId", i);
                    node.SetValue("ParentId", e.Node.ParentNode.GetValue("Id"));
                    i++;
                }

            SaveDisp();
            SaveXml();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = IsChanged ? 
                DialogResult.OK : DialogResult.Cancel;
        }
    }
}
