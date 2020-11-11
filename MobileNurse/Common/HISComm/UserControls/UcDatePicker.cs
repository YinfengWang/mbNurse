using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace HISPlus.UserControls
{
    public partial class UcDatePicker : UserControl
    {
        public UcDatePicker()
        {
            InitializeComponent();
        }

        public DateTime Value
        {
            get { return dateEdit1.DateTime; }
            set
            {
                dateEdit1.DateTime = value;
            }
        }

        private void UcDatePicker_Load(object sender, EventArgs e)
        {
            // 中文格式化显示
            dateEdit1.Properties.Mask.EditMask = @"yyyy年MM月dd日";
            dateEdit1.Properties.Mask.UseMaskAsDisplayFormat = true;

            dateEdit1.DateTime = DateTime.Now;
        }
    }
}
