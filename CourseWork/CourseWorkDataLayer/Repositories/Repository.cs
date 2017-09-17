using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories
{
    public abstract class Repository<T> where T : class
    {
        protected readonly ApplicationDbContext DbContext;

        protected abstract DbSet<T> Table { get; }

        protected abstract string GetIdentificator(T item);

        protected Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public string GetNewId() => Guid.NewGuid().ToString();

        public bool AddRange(params T[] items)
        {
            return SaveActionResult(() => Table.AddRange(items));
        }

        public bool RemoveRange(params string[] identificators)
        {
            var items = Table.Where(item => identificators.Contains(GetIdentificator(item)));
            return SaveActionResult(() => Table.RemoveRange(items));
        }

        public bool RemoveWhere(Func<T, bool> whereExpression)
        {
            var identificators = GetWhere(whereExpression).Select(GetIdentificator).ToArray();
            return RemoveRange(identificators);
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

        public List<TResult> GetUnique<TResult>(Func<T, TResult> gettinResultExpression)
        {
            return Table.Select(gettinResultExpression).Distinct().ToList();
        }

        public List<T> GetAll()
        {
            return Table.ToList();
        }

        public T Get(string id)
        {
            return Table.Find(id);
        }

        public List<T> GetWhere(Func<T, bool> whereExpression)
        {
            return Table.Where(whereExpression).ToList();
        }

        public List<T> GetWhereEager<TProperty>(Expression<Func<T, TProperty>> includeStatement, Func<T, bool> whereExpression)
        {
            return Table.Include(includeStatement).AsEnumerable().Where(whereExpression).ToList();
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
