using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonEntity;
using HISPlus.Models;

namespace HISPlus.Controller
{
    public class NursingRecControllor
    {
        private INursingRec View { get; set; }

        private IEnumerable<VitalSignsRec> _xunshis;

        private IEnumerable<PatientInfo> _patients;

        /// <summary>
        /// 数据库访问
        /// </summary>
        private readonly VitalSignsRecModel _model;

        /// <summary>
        /// 当前操作实体
        /// </summary>
        public VitalSignsRec CurrentModel ;       

        public NursingRecControllor(INursingRec view)
        {
            this.View = view;
            this.View.PatientChanged += View_PatientChanged;
            this._model = new VitalSignsRecModel();
            this.CurrentModel = new VitalSignsRec();
            this.CurrentModel.Id = new VitalSignsRecId();
            this.CurrentModel.Id.TimePoint = DateTime.MinValue;
        }

        void View_PatientChanged(object sender, PatientQueryArgs e)
        {
            _xunshis = _model.Find(GVars.Patient.ID, GVars.Patient.VisitId, e.StartDate, e.EndDate, e.VitalSigns);

            this.View.BindData(_xunshis);
        }

        public void SetCurrentModel(object obj)
        {
            if (obj is VitalSignsRec)
                CurrentModel = obj as VitalSignsRec;
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
