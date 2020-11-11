using System;
using System.Collections.Generic;
using CommonEntity;
using NHibernate;
using NHibernate.Criterion;

namespace HISPlus.Models
{
    public class VitalSignsRecModel : Entity<VitalSignsRecModel>, IEntity
    {
        public IEnumerable<VitalSignsRec> Find(string patientId, string visitId, DateTime startDate, DateTime endDate,string vitalSigns)
        {
            ICriteria ic = session.CreateCriteria(typeof(VitalSignsRec));

            ic.Add(Restrictions.Eq("Id.PatientId", patientId));
            ic.Add(Restrictions.Eq("Id.VisitId", Convert.ToInt64(visitId)));
            ic.Add(Restrictions.Between("Id.TimePoint", startDate, endDate));
            if(!string.IsNullOrEmpty(vitalSigns))
                ic.Add(Restrictions.Eq("VitalSigns", vitalSigns));
            return ic.List<VitalSignsRec>();
        }
    }
}
