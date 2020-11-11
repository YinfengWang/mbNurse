using System.Collections.Generic;

namespace CommonEntity
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
