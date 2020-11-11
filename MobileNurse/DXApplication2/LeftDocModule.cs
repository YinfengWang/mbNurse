using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using HISPlus;

namespace DXApplication2
{
    public partial class LeftDocModule : XtraForm
    {
        public LeftDocModule()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选项单击事件
        /// </summary>
        public event NavBarLinkEventHandler LinkClicked;

        private void LeftDocModule_Load(object sender, EventArgs e)
        {
            Init();
        }

        /// <summary>
        ///     护理文书创建导航菜单树
        /// </summary>
        private void Init()
        {
            IList<DocTemplateClass> listTemplateClass = EntityOper.GetInstance().LoadAll<DocTemplateClass>();
            IList<DocTemplate> listElements = EntityOper.GetInstance().LoadAll<DocTemplate>();

            IList<DocTemplateDept> listDepts = EntityOper.GetInstance().FindByProperty<DocTemplateDept>("DeptCode", GVars.User.DeptCode);           

            listElements =
                listElements.Where(p => p.IsGlobal == 1 || listDepts.Any(q => q.DocTemplate.Id == p.Id)).ToList();

            if (listElements.Count == 0) return;
            int i = 0;
            foreach (DocTemplateClass templateClass in listTemplateClass)
            {
                //父级菜单
                var nbg = new NavBarGroup
                {
                    Caption = templateClass.Name,
                    Name = templateClass.Name,

                    Expanded = i==0,
                    Tag = templateClass,
                    Hint = templateClass.Name,
                    GroupCaptionUseImage = NavBarImage.Small,
                };

                i = 1;
                //nbg.SmallImage = GetImageFile(model.IconPath);

                navBarControl1.Groups.Add(nbg);


                DocTemplateClass @class = templateClass;

                IList<DocTemplate> docTemplates =
                    listElements.Where(p => p.DocTemplateClass.Id == @class.Id).ToList();

                foreach (DocTemplate template in docTemplates)
                {
                    //子级菜单
                    var item = new NavBarItem
                    {
                        Caption = template.TemplateName,
                        Name = template.TemplateName,
                        Hint = template.TemplateName,
                        Tag = template.Id,                        
                    };

                    item.LinkClicked += item_LinkClicked; ; //赋予单击事件

                    nbg.ItemLinks.Add(item);

                    navBarControl1.Items.Add(item);
                }
            }
        }

        void item_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            if (LinkClicked != null)
            {
                LinkClicked(sender, e);
            }
        }
    }
}