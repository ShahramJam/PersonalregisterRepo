using System.Collections.Generic;

namespace Personalregister
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Update(T item);
        void Delete(T item);
        IReadOnlyList<T> List();
    }
}
