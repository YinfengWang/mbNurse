using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class NursingRecordChild : NursingRecordFrm
    {
        public NursingRecordChild()
        {
            InitializeComponent();

            _id = "00201";
            _guid = "78d2b0a5-6417-4739-aaa7-f2b7047e45c5";

            _typeId = "03";
            this.Text = "儿科护理记录单";
        }
    }
}
