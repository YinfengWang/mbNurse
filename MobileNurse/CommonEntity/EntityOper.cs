using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CommonEntity;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace HISPlus
{
    public class EntityOper
    {
        public readonly ISessionFactory _sessionFactory;

        private ISession session;

        private static EntityOper _instance;
        private static readonly object LockHelper = new object();

        /// <summary>
        /// 获取实体操作实例-单例。
        /// </summary>
        /// <returns></returns>
        public static EntityOper GetInstance()
        {
            if (_instance != null) return _instance;
            lock (LockHelper)
            {
                if (_instance == null)
                    // ReSharper disable once PossibleMultipleWriteAccessInDoubleCheckLocking
                    return _instance = new EntityOper();
            }
            return _instance;
        }

        private EntityOper()
        {
            Configuration cfg = new Configuration();
            cfg.Configure(Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "nhibernate.cfg.xml"));
            cfg.Properties.Add(NHibernate.Cfg.Environment.ConnectionString,
                GVars.OracleAccess.ConnectionString
                );
            _sessionFactory = cfg.BuildSessionFactory();

            session = _sessionFactory.OpenSession();
            // cfg.Configure(Assembly.GetExecutingAssembly(), "nhibernate.cfg.xml");
        }

        public object Save<T>(T entity)
        {
            using (ISession openSession = _sessionFactory.OpenSession())
            {
                var id = openSession.Save(entity);
                openSession.Flush();
                return id;
            }
        }

        public void Update<T>(T entity)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                session.Update(entity);
                session.Flush();
            }
        }

        public void Update<T>(IEnumerable<T> ilist) where T : IEntity
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                foreach (T t in ilist)
                {
                    session.Update(t);
                }
                session.Flush();
            }
        }

        public void Merge<T>(T entity)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                session.Save(entity);
                session.Flush();
            }
        }

        public void SaveOrUpdate<T>(T entity) where T : IEntity
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                session.SaveOrUpdate(entity);
                session.Flush();
            }
        }

        /// <summary>
        /// 删除单个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void Delete<T>(T entity)
        {
            // if (entity.GetType().Name == "List`1")
            //using (ISession session = _sessionFactory.OpenSession())
            //{
            //    foreach (T t in entity)
            //    {
            //        session.Delete(t);
            //    }
            //    session.Flush();
            //}
            //entity.
            //if (entity is IEnumerable)
            //{
            //    DeleteList(entity);
            //}
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
        public T Get<T>(object id)
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
        public T Load<T>(object id)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                return session.Load<T>(id);
            }
        }

        public void Save<T>(IEnumerable<T> ilist)
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

        public void Merge<T>(IEnumerable<T> ilist)
        {
            foreach (T t in ilist)
            {
                session.Merge(t);
            }
            session.Flush();
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ilist"></param>
        public void DeleteList<T>(IEnumerable<T> ilist)
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                foreach (T t in ilist)
                {
                    session.Delete(t);
                }
                session.Flush();
            }
        }

        public void SaveOrUpdate<T>(IList<T> ilist) where T : IEntity
        {
            if (ilist == null) throw new ArgumentNullException("ilist");
            using (ISession session = _sessionFactory.OpenSession())
            {
                foreach (T t in ilist)
                {
                    session.SaveOrUpdate(t);
                }
                session.Flush();
            }
        }

        public void SaveOrUpdate<T>(IEnumerable<T> ilist) where T : IEntity
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

        public IList<T> LoadAll<T>() where T : IEntity
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                return session.Query<T>().ToList();
            }
        }

        public IList<T> FindByProperty<T>(string propertyName, object value)
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

        public IList<T> FindByProperty<T>(string[] propertyNames, object[] values)
        {
            ICriteria ic = null;
            ic = session.CreateCriteria(typeof (T));

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
