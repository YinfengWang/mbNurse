using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class NursingRecordMiNiao : NursingRecordFrm
    {
        public NursingRecordMiNiao()
        {
            InitializeComponent();
            _id = "00202";
            _guid = "a59b0a25-f36b-4375-9d54-ec967afbbe85";

            _typeId = "04";
            this.Text = "泌尿科护理记录单";
        }
    }
}
