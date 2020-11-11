using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class FilterValueFrm : Form
    {
        public bool IsNumeric = false;
        public string Options = string.Empty;
        public string Result  = string.Empty;

        public FilterValueFrm()
        {
            InitializeComponent();

            lvwOptions.SelectedIndexChanged += new EventHandler(lvwOptions_SelectedIndexChanged);
        }

        
        private void FilterValueFrm_Load(object sender, EventArgs e)
        {
            try
            {
                string[] parts = Options.Split(",".ToCharArray());

                for (int i = 0; i < parts.Length; i++)
                {
                    ListViewItem item = new ListViewItem((i + 1).ToString());
                    item.SubItems.Add(parts[i]);

                    lvwOptions.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        void lvwOptions_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (lvwOptions.SelectedIndices.Count > 0)
                {
                    txtVal.Text = lvwOptions.Items[lvwOptions.SelectedIndices[0]].SubItems[1].Text;
                }
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Result = string.Empty;

                this.DialogResult = DialogResult.Cancel;

                this.Close();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsNumeric == true)
                {
                    if (DataType.IsPositive(txtVal.Text.Trim()) == false)
                    {
                        MessageBox.Show("«Î ‰»Î’˝ ˝");
                        txtVal.Focus();

                        return;
                    }
                }

                Result = txtVal.Text.Trim();

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
    }
}