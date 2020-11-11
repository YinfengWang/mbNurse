using System;
using System.Collections.Generic;
using CommonEntity;
using NHibernate;
using NHibernate.Criterion;

namespace HISPlus.Models
{
    public class NursingTourModel : Entity<NursingTourModel>, IEntity
    {
        public IEnumerable<Xunshi> Find(string patientId, string visitId, DateTime startDate, DateTime endDate)
        {
            ICriteria ic = session.CreateCriteria(typeof(Xunshi));

            ic.Add(Restrictions.Eq("Id.PatientId", patientId));
            ic.Add(Restrictions.Eq("Id.VisitId", Convert.ToInt64(visitId)));
            ic.Add(Restrictions.Between("Id.ExecuteDate", startDate, endDate));
            return ic.List<Xunshi>();
        }
    }
}
