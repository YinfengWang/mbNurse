using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public partial class NursingRecordGuKe : NursingRecordFrm
    {
        public NursingRecordGuKe()
        {
            InitializeComponent();
            _id = "00203";
            _guid = "a8b9b92d-bc76-4d85-b127-d8e67b38eccd";

            _typeId = "05";
            this.Text = "骨科护理记录单";
        }
    }
}
