using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CourseWork.DataLayer.Repositories
{
    public interface IRepository<T>
    {
        object GetIdentificator(T item);

        bool AddRange(params T[] items);

        bool RemoveRange(params object[] identificators);

        bool RemoveWhere(Func<T, bool> whereExpression);

        bool UpdateRange(params T[] items);

        List<TResult> GetUnique<TResult>(Func<T, TResult> gettingResultExpression,
            params Expression<Func<T, object>>[] includeStatements);

        List<T> GetAll(params Expression<Func<T, object>>[] includeStatements);

        T FirstOrDefault(Func<T, bool> whereExpression, params Expression<Func<T, object>>[] includeStatements);

        int Count(Func<T, bool> whereExpression);

        List<T> GetWhere(Func<T, bool> whereExpression, params Expression<Func<T, object>>[] includeStatements);

        List<T> GetOrdered<TKey>(Func<T, TKey> orderExpression, int count, bool isDescending,
            params Expression<Func<T, object>>[] includeStatements);
    }
}
