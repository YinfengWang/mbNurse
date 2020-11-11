using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonEntity;
using HISPlus.Models;

namespace HISPlus.Controller
{
    public class NursingTourControllor
    {
        private INursingTourView View { get; set; }

        private IEnumerable<Xunshi> _xunshis;

        private IEnumerable<PatientInfo> _patients;

        private readonly NursingTourModel _model;

        public NursingTourControllor(INursingTourView view)
        {
            this.View = view;
            this.View.PatientChanged += View_PatientChanged;            
            this._model = new NursingTourModel();
        }

        void View_PatientChanged(object sender, PatientQueryArgs e)
        {
            _xunshis = _model.Find(GVars.Patient.ID, GVars.Patient.VisitId, e.StartDate, e.EndDate);
            
            this.View.BindData(_xunshis);
        }

        public void Init()
        {
            _patients = EntityOper.GetInstance().FindByProperty<PatientInfo>(
               new[] { "Id.PatientId", "Id.VisitId" },
               new object[] { GVars.Patient.ID, Convert.ToByte(GVars.Patient.VisitId) });

            // 如果住院信息没有找到,就只取病人信息
            if (!_patients.Any())
            {
                _patients = EntityOper.GetInstance().FindByProperty<PatientInfo>(
                    "Id.PatientId", GVars.Patient.ID);
            }
        }
    }
}
