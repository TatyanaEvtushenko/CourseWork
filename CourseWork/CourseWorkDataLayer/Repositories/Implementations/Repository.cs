using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CourseWork.DataLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories.Implementations
{
    public interface IRepository
    {
    }

    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext DbContext;

        protected Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public abstract object GetIdentificator(T item);

        protected abstract DbSet<T> Table { get; }

        public bool AddRange(params T[] items)
        {
            return SaveActionResult(() => Table.AddRange(items));
        }

        public bool RemoveRange(params object[] identificators)
        {
            foreach (var id in identificators)
            {
                System.Diagnostics.Debug.WriteLine("Hello: " + id.ToString());
            }
            var items = Table.Where(item => identificators.Contains(GetIdentificator(item)));
            return SaveActionResult(() => Table.RemoveRange(items));
        }

        public bool RemoveWhere(Func<T, bool> whereExpression)
        {
            var items = Table.Where(whereExpression);
            return SaveActionResult(() => Table.RemoveRange(items));
        }

        public bool UpdateRange(params T[] items)
        {
            return SaveActionResult(() =>
            {
                foreach (var item in items)
                {
                    DbContext.Entry(item).State = EntityState.Modified;
                }
            });
        }

        public List<TResult> GetUnique<TResult>(Func<T, TResult> gettingResultExpression,
            params Expression<Func<T, object>>[] includeStatements)
        {
            return GetEager(includeStatements).Select(gettingResultExpression).Distinct().ToList();
        }

        public List<T> GetAll(params Expression<Func<T, object>>[] includeStatements)
        {
            return GetEager(includeStatements).ToList();
        }

        public T FirstOrDefault(Func<T, bool> whereExpression, params Expression<Func<T, object>>[] includeStatements)
        {
            return GetEager(includeStatements).FirstOrDefault(whereExpression);
        }

        public int Count(Func<T, bool> whereExpression)
        {
            return Table.Count(whereExpression);
        }

        public decimal Sum(Func<T, decimal> whereExpression)
        {
            return Table.Sum(whereExpression);
        }

        public List<T> GetWhere(Func<T, bool> whereExpression, params Expression<Func<T, object>>[] includeStatements)
        {
            return GetEager(includeStatements).Where(whereExpression).ToList();
        }

        public List<T> GetOrdered<TKey>(Func<T, TKey> orderExpression, int count, bool isDescending,
            params Expression<Func<T, object>>[] includeStatements)
        {
            var items = GetEager(includeStatements);
            items = isDescending ? items.OrderByDescending(orderExpression) : items.OrderBy(orderExpression);
            return items.Take(count).ToList();
        }

        private IEnumerable<T> GetEager(params Expression<Func<T, object>>[] includeStatements)
        {
            var query = (IQueryable<T>) Table;
            foreach (var includeStatement in includeStatements)
            {
                query = query.Include(includeStatement);
            }
            return query;
        }

        private bool SaveActionResult(Action action)
        {
            try
            {
                action();
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}