using System.Collections.Generic;

namespace CourseWork.DataLayer.Repositories
{
    public interface IRepository<T>
    {
        bool Add(T item);

        bool Remove(string id);

        IEnumerable<T> GetAll();

        T Find(string id);
    }
}
