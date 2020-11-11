using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using HISPlus;

namespace HISPlus
{
    public partial class SelectElement : FormDo
    {
        private int _parentId;
        private int _sortId;
        private DocTemplate _templateModel;

        public SelectElement()
        {
            InitializeComponent();
        }

        public SelectElement(DocTemplate template, int parentId, int sortId = 1)
        {
            this._parentId = parentId;
            this._sortId = sortId;
            this._templateModel = template;
            InitializeComponent();
        }

        private void SelectElement_Load(object sender, EventArgs e)
        {
            try
            {
                base.LoadingShow();
                //this.DialogResult = DialogResult.Cancel;

                initDisp();
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


        private void initDisp()
        {
            IList<DocTemplateElement> list = EntityOper.GetInstance().LoadAll<DocTemplateElement>();

            ucTreeList1.KeyFieldName = "Id";
            ucTreeList1.ParentFieldName = "ParentId";

            ucTreeList1.Add("元素名称", "ElementName");
            ucTreeList1.Add("元素模板", "DocControlTemplate.Name");
            //ucTreeList1.Add("创建时间", "CreateTimestamp");

            ucTreeList1.MultiSelect = true;
            //ucTreeList1.Add("模块编码", "ModuleCode");
            //ucTreeList1.Add("版本号", "Version");
            //ucTreeList1.Add("动态库", "Assembly");
            //ucTreeList1.Add("类名", "FormName");
            //ucTreeList1.Add("备注", "Remark");            
            ucTreeList1.DataSource = list;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            try
            {
                base.LoadingShow();

                TreeListMultiSelection s = ucTreeList1.Selection;

                IList<DocTemplateElement> listNew = new List<DocTemplateElement>();

                foreach (TreeListNode node in s)
                {
                    DocTemplateElement model = ucTreeList1.GetDataRecordByNode(node) as DocTemplateElement;
                    if (model == null)
                        continue;

                    model.Id = 0;
                    model.ParentId = this._parentId;
                    model.DocTemplate = _templateModel;
                    model.SortId = this._sortId;
                    this._sortId++;
                    model.Id = Convert.ToDecimal(EntityOper.GetInstance().Save(model));

                    if (node.HasChildren)
                    {
                        Save(node.Nodes, model.Id);
                    }
                }

                this.DialogResult = DialogResult.OK;
                //foreach(TreeListNode node in ucTreeList1)
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

        private void Save(TreeListNodes nodes, decimal parentId)
        {
            foreach (TreeListNode node in nodes)
            {
                DocTemplateElement model = ucTreeList1.GetDataRecordByNode(node) as DocTemplateElement;
                if (model == null)
                    continue;

                model.Id = 0;
                model.ParentId = parentId;
                model.DocTemplate = _templateModel;

                model.Id = Convert.ToDecimal(EntityOper.GetInstance().Save(model));

                if (node.HasChildren)
                {
                    Save(node.Nodes, model.Id);
                }
            }
        }

    }
}