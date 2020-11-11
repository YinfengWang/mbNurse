using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class NursingRecordWZFrm : NursingRecordFrm
    {
        public NursingRecordWZFrm()
        {
            InitializeComponent();
            
            _id     = "00013";
            _guid   = "4C7E7FA4-04D5-4e49-B3CC-BE93D54BF548";
            
            _typeId = "02";
            this.Text = "危重护理记录单";
        }
    }
}
