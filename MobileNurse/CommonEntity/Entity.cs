using System.Collections.Generic;
using System.Linq;
using HISPlus;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace CommonEntity
{
    public class Entity<T> : IEntity<T>
    {
        private readonly ISessionFactory _sessionFactory;

        protected ISession session;

        protected Entity()
        {
            try
            {
                Configuration cfg = new Configuration();
                cfg.Configure("nhibernate.cfg.xml");
                cfg.Properties.Add(NHibernate.Cfg.Environment.ConnectionString,
                    GVars.OracleAccess.ConnectionString
                    );
                _sessionFactory = cfg.BuildSessionFactory();

                session = _sessionFactory.OpenSession();
                // cfg.Configure(Assembly.GetExecutingAssembly(), "nhibernate.cfg.xml");
            }
            catch (System.Exception ex)
            {
                Error.ErrProc(ex);
            }
        }

        public object Save(T entity)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                var id = session.Save(entity);
                session.Flush();
                return id;
            }
        }
       
        public void Update(T entity)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                session.Update(entity);
                session.Flush();
            }
        }

        public void Merge(T entity)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                session.Save(entity);
                session.Flush();
            }
        }

        public void SaveOrUpdate(T entity)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                session.SaveOrUpdate(entity);
                session.Flush();
            }
        }

        public void Delete(T entity)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {

                session.Delete(entity);
                session.Flush();
            }
        }

        /// <summary>
        /// 直接从数据库检索
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(object id)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                return session.Get<T>(id);
            }
        }

        /// <summary>
        /// 1,首先查找session的persistent Context中是否有缓存，如果有则直接返回
        /// 2,如果没有则判断是否是lazy，如果不是直接访问数据库检索，查到记录返回，查不到抛出异常
        ///3,如果是lazy则需要建立代理对象，对象的initialized属性为false，target属性为null
        ///4, 在访问获得的代理对象的属性时,检索数据库，如果找到记录则把该记录的对象复制到代理对象的target
        ///上，并将initialized=true，如果找不到就抛出异常 。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Load(object id)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                return session.Load<T>(id);
            }
        }

        public void Save(IEnumerable<T> ilist)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                foreach (T t in ilist)
                {
                    session.Save(t);
                }
                session.Flush();
            }
        }

        public void Merge(IEnumerable<T> ilist)
        {
            foreach (T t in ilist)
            {
                session.Merge(t);
            }
            session.Flush();
        }

        public void Delete(IEnumerable<T> ilist)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                foreach (T t in ilist)
                {
                    if (t != null)
                        session.Delete(t);
                }
                session.Flush();
            }
        }

        public void SaveOrUpdate(IEnumerable<T> ilist)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                foreach (T t in ilist)
                {
                    session.SaveOrUpdate(t);
                }
                session.Flush();
            }
        }

        public void SaveOrUpdate(IEnumerable<T> ilist, ref int index)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                foreach (T t in ilist)
                {
                    index++;
                    session.SaveOrUpdate(t);
                }
                session.Flush();
            }
        }

        public IList<T> LoadAll()
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                return session.Query<T>().ToList();
            }
        }

        public IList<T> FindByProperty(string propertyName, object value)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                if (propertyName == "ParentId")
                    return session.CreateCriteria(typeof(T))
                         .Add(Restrictions.Eq(propertyName, value))
                         .AddOrder(Order.Asc("SortId"))
                         .List<T>();

                return session.CreateCriteria(typeof(T))
                    .Add(Restrictions.Eq(propertyName, value))
                    .List<T>();
            }
        }

        public IList<T> FindByProperty(string[] propertyNames, object[] values)
        {
            ICriteria ic = session.CreateCriteria(typeof(T));
            for (int i = 0; i < propertyNames.Length; i++)
            {
                ic.Add(Restrictions.Eq(propertyNames[i], values[i]));
            }

            //if (propertyNames.Contains("ParentId")
            //            || propertyNames.Contains("DocTemplate.Id"))
            //    if (ic != null) ic.AddOrder(Order.Asc("SortId")).List<T>();

            if (ic != null) return ic.List<T>();

            return session.Query<T>().ToList();
        }
    }
}
