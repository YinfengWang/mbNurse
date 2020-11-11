using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HISPlus
{
    public interface IEntity<T>
    {
        object Save(T entity);

        void Update(T entity);

        void Delete(T entity);

        T Get(object id);

        T Load(object id);

        IList<T> LoadAll();

        IList<T> FindByProperty(string propertyName, object value);
    }
}
