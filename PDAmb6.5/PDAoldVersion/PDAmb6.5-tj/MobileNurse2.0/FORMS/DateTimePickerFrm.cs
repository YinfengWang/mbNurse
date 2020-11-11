using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class DateTimePickerFrm : Form
    {
        public DateTime CurrentDateTime = DateTime.Now;
        
        public DateTimePickerFrm()
        {
            InitializeComponent();
        }
        
        
        private void AddNurseEventFrm_Load(object sender, EventArgs e)
        {
            try
            {
                dtpDate.MaxDate = CurrentDateTime;
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        
        private void mnuOk_Click(object sender, EventArgs e)
        {
            try
            {
                // 检查设定的时间是否大于当前日期
                DateTime dt = dtpDate.Value.Date.AddHours(dtpTime.Value.Hour).AddMinutes(dtpTime.Value.Minute);
                if (dt.CompareTo(CurrentDateTime) > 0)
                {
                    GVars.Msg.Show("I00011");       // 记录时间不能大于当前时间!
                    return;
                }
                
                CurrentDateTime = dt;
                
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }            
        }
        
        
        private void mnuCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }
}