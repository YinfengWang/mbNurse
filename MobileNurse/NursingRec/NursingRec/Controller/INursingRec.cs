using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonEntity;
using HISPlus.Models;

namespace HISPlus.Controller
{
    public interface INursingRec
    {
        event EventHandler<PatientQueryArgs> PatientChanged;        

        void BindData(IEnumerable<VitalSignsRec> dataSource);
    }
}
