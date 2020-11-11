using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonEntity;
using HISPlus.Models;

namespace HISPlus.Controller
{
    public interface INursingTourView
    {
        event EventHandler<PatientQueryArgs> PatientChanged;

        void BindData(IEnumerable<Xunshi> dataSource);
    }

    public class PatientQueryArgs : EventArgs
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string VitalSigns { get; private set; }

        public PatientQueryArgs(DateTime startDate, DateTime endDate, string vitalSigns = "")
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.VitalSigns = vitalSigns;
        }
    }
}
