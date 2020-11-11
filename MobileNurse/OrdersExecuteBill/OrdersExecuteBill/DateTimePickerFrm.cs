using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class DateTimePickerFrm : Form
    {
        public DateTime DateTimeSet = DateTime.Now;public DateTimePickerFrm()
        {
            InitializeComponent();
        }
        
        private void DateTimePickerFrm_Load(object sender, EventArgs e)
        {
            try
            {
                monthCalendar1.TodayDate = DateTimeSet.Date;
                dateTimePicker1.Value    = DateTimeSet;
                
                refreshShow();
            }
            catch(Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                refreshShow();
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            try
            {
                refreshShow();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                refreshShow();
            }
            catch (Exception ex)
            {
                Error.ErrProc(ex);
            }
        }
        
        
        private void refreshShow()
        {
            DateTimeSet = monthCalendar1.TodayDate.Date.AddHours(dateTimePicker1.Value.Hour).AddMinutes(dateTimePicker1.Value.Minute);
            this.Text = DateTimeSet.ToString(ComConst.FMT_DATE.LONG_MINUTE);
        }

        private void btnCancel_Click(object sender, EventArgs e)
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
    }
}
