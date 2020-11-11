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
    public partial class NursingInterventionFrm : DictItemContentFrm
    {
        public NursingInterventionFrm()
        {
            InitializeComponent();
            
            _dictId         = "08";
            _listTitle      = "护理计划";
            _contentTitle   = "措施及目标";
            _deptOwn        = false;
            _id             = "00049";
            _guid           = "ACBF6F36-A0D7-44a1-95E2-E0CA3532502E";
        }
    }
}
