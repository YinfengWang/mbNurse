using System;
using System.Collections.Generic;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using HISPlus.UserControls;

namespace HISPlus
{
    public partial class ControlSetting : FormDo
    {
        public ControlSetting()
        {
            InitializeComponent();
        }

        private DocControlTemplate _model;

        private IList<DocControlTemplate> _listDataSource;

        private void ControlSetting_Load(object sender, EventArgs e)
        {
            try
            {
                base.LoadingShow();

                BindDropDownList();

                DocCommon.SetSpinEditLimit(txtOffset);
                DocCommon.SetSpinEditLimit(txtWidth);
                DocCommon.SetSpinEditLimit(txtHeight);

                ucGridView1.AllowEdit = true;
                ucGridView1.Add("模板编号", "Id", false);
                ucGridView1.Add("模板名称", "Name");

                ucGridView1.Add("控件类型", "DocControlType.Name");
                ucGridView1.Add("控件字体", "ControlFont");
                //this.Add("控件宽度", "ControlWidth");
                //this.Add("控件高度", "ControlHeight");
                //this.Add("控件偏移量", "ControlOffset");              
                ucGridView1.Add("启用", "IsEnabled", typeof(byte));
                ucGridView1.Add("备注", "Remark");
                
                ucGridView1.Init();

                Bind();
            }
            catch (Exception exception)
            {
                Error.ErrProc(exception);
            }
            finally
            {
                base.LoadingClose();
            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void Bind()
        {
            _listDataSource = EntityOper.GetInstance().LoadAll<DocControlTemplate>();
            ucGridView1.DataSource = _listDataSource;
        }

        /// <summary>
        /// 绑定下拉列表
        /// </summary>
        private void BindDropDownList()
        {
            // 控件状态的下拉列表
            //DataSet ds = documentDbI.GetAllControlType();
            //ddlControlType.Properties.DataSource = ds.Tables[0].DefaultView;

            //ddlControlType.Properties.ValueMember = "CONTROL_TYPE_ID";
            //ddlControlType.Properties.DisplayMember = "CONTROL_TYPE_NAME";
            //ddlControlType.Properties.Columns.AddRange(new LookUpColumnInfo[]
            //{
            //    new LookUpColumnInfo("CONTROL_TYPE_NAME", "类型"),
            //    new LookUpColumnInfo("REMARK", "说明")
            //});

            ddlControlType.Properties.DataSource = EntityOper.GetInstance().LoadAll<DocControlType>();

            ddlControlType.Properties.ValueMember = "Id";
            ddlControlType.Properties.DisplayMember = "Name";
            ddlControlType.Properties.Columns.AddRange(new[]
            {
                new LookUpColumnInfo("Name", "类型"),
                new LookUpColumnInfo("Remark", "说明")
            });
            // 选中第一项
            ddlControlType.ItemIndex = 0;

            txtFont.Properties.ReadOnly = true;
        }

        private void txtFont_Click(object sender, EventArgs e)
        {
            txtFont.Text = DocCommon.ShowFontDialog(txtFont.Text);
        }

        private void SetValueToControl()
        {
            txtFont.Text = _model.ControlFont;
            txtTemplateName.Text = _model.Name;
            txtFont.Text = _model.ControlFont;
            txtHeight.Text = _model.ControlHeight.ToString();
            txtOffset.Text = _model.ControlOffset.ToString();
            txtWidth.Text = _model.ControlWidth.ToString();
            chkEnabled.Checked = _model.IsEnabled == 1;
            txtRemark.Text = _model.Remark;
            ddlControlType.EditValue = _model.DocControlType.Id;
            if (ddlControlType.ItemIndex < 0)
                ddlControlType.ItemIndex = 0;
        }

        private void SetValueFromControl()
        {
            _model.Name = txtTemplateName.Text;
            _model.ControlFont = txtFont.Text;
            _model.ControlWidth = Convert.ToDecimal(txtWidth.Text);
            _model.ControlHeight = Convert.ToDecimal(txtHeight.Text);
            _model.ControlOffset = Convert.ToDecimal(txtOffset.Text);
            _model.DocControlType.Id = (decimal)ddlControlType.EditValue;
            _model.IsEnabled = Convert.ToByte(chkEnabled.Checked);
            _model.Remark = txtRemark.Text;
            _model.UpdateTimestamp = GVars.OracleAccess.GetSysDate();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SetValueFromControl();
            EntityOper.GetInstance().SaveOrUpdate(_model);
            ucGridView1.RefreshData();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            _model = new DocControlTemplate
            {
                DocControlType = new DocControlType(),
                IsEnabled = 1,
                Name = "模板" + (_listDataSource.Count + 1),
                CreateTimestamp = GVars.OracleAccess.GetSysDate(),
                UpdateTimestamp = GVars.OracleAccess.GetSysDate(),
            };
            _listDataSource.Add(_model);
            ucGridView1.DataSource = _listDataSource;
            SetValueToControl();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            EntityOper.GetInstance().Delete(_model);
            _listDataSource.Remove(_model);            
            ucGridView1.RefreshData();
            //XtraMessageBox.Show("删除成功!");
        }

        private void ucGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (ucGridView1.SelectRowsCount > 0)
            {
                _model = ucGridView1.GetSelectRow() as DocControlTemplate;
                if (_model == null) return;

                SetValueToControl();
            }
        }
    }
}