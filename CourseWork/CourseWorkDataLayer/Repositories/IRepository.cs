using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CourseWork.DataLayer.Repositories
{
    public interface IRepository<T>
    {
        bool AddRange(params T[] items);

        bool RemoveRange(params string[] identificators);

        List<T> GetAll();

        T Get(string id);

        List<T> GetWhere(Expression<Func<T, bool>> whereExpression);

        bool UpdateRange(params T[] items);
    }
}
