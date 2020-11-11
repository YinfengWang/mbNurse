using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus.FORMS
{
    public partial class SelectTime : Form
    {
     


        private string start = string.Empty;

        public string Start
        {
            get { return start; }
            set { start = value; }
        }
        private string stop = string.Empty;

        public string Stop
        {
            get { return stop; }
            set { stop = value; }
        }
        public SelectTime()
        {
            InitializeComponent();
        }
        private void butSure_Click(object sender, EventArgs e)
        {
            Start = DataStartTime.Value.ToString();
            Stop = DataStopTime.Value.ToString();
            if (DateTime.Compare(DataStartTime.Value, DataStopTime.Value) > 0)
            {
                MessageBox.Show("对不起,开始时间要小于结束时间！");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}