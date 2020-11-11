using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class PatientFilterFrm : Form
    {
        public ArrayList ArrFilter = new ArrayList();
        protected string _filterName;

        private DataRow[] drShow = null;
        
        public PatientFilterFrm()
        {
            InitializeComponent();
        }
        
        
        public string FilterItems
        {
            get { return _filterName;}
        }
        
        
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientFilterFrm_Load(object sender, EventArgs e)
        {
            try
            {
                initFrmVal();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        private void btnSetVal_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwFilter.SelectedIndices.Count == 0)
                {
                    MessageBox.Show("请选择项目!");
                    return;
                }

                DataRow dr = drShow[lvwFilter.SelectedIndices[0]];
                if (dr["OPTIONS"].ToString().Length == 0)
                {
                    MessageBox.Show("当前项目没有可选值!");                   
                    return;
                }

                FilterValueFrm selectFrm = new FilterValueFrm();
                selectFrm.IsNumeric = (dr["VALUE_TYPE"].ToString().Equals("1"));
                selectFrm.Options = dr["OPTIONS"].ToString();
                
                if (selectFrm.ShowDialog() == DialogResult.OK)
                {
                    ListViewItem item = lvwFilter.Items[lvwFilter.SelectedIndices[0]];
                    item.SubItems[1].Text = selectFrm.Result;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        

        /// <summary>
        /// 按钮[退出]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        /// <summary>
        /// 按钮[确定]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                ArrFilter.Clear();
                GVars.patientFilterList.Clear();
                foreach(ListViewItem item in lvwFilter.Items)
                {
                    if (item.Checked == true)
                    {
                        string filterName = item.SubItems[0].Text;
                        if (item.SubItems[1].Text.Length > 0)
                        {
                            filterName += ComConst.STR.COLON + item.SubItems[1].Text;
                        }
                        
                        ArrFilter.Add(filterName);
                        GVars.patientFilterList.Add(filterName);
                    }
                }
                
                if (ArrFilter.Count > 0)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void initFrmVal()
        {
            // 清除界面显示
            lvwFilter.Items.Clear();

            // 获取数据
            DataSet dsFilterNames = GVars.PatDbI.GetPatFilterName();
            
            if (dsFilterNames == null || dsFilterNames.Tables.Count == 0)
            {
                return;
            }
            
            // 显示数据
            drShow = dsFilterNames.Tables[0].Select(string.Empty, "SHOW_ORDER");
            
            for (int i = 0; i < drShow.Length; i++)
            {
                DataRow dr = drShow[i];
                
                ListViewItem item = new ListViewItem(dr["FILTER_NAME"].ToString());
                
                string options = dr["OPTIONS"].ToString();
                
                if (options.Length > 0)
                {
                    string[] parts = options.Split(",".ToCharArray());
                    if (parts.Length > 0)
                    {
                        options = parts[0].Trim();
                    }
                }
                
                item.SubItems.Add(options);
                
                lvwFilter.Items.Add(item);
            }
        }
    }
}