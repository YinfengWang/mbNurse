using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HISPlus
{
    public class NursingRecView_BloodSugar : NursingViewRec
    {
        public NursingRecView_BloodSugar()
        {
            _id             = "00040";
            _guid           = "707E20C8-18C6-4a61-B5EC-A486E70AADE8";
            
            //_paraVitalCode  = "BLOOD_SUGAR_CODE";
            //_template       = "血糖观察表";
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // NursingRecView_BloodSugar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.ClientSize = new System.Drawing.Size(993, 576);
            this.Name = "NursingRecView_BloodSugar";
            this.Text = "血糖观察表";
            this.ResumeLayout(false);

        }
    }
}
