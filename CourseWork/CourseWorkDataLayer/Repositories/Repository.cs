using System;
using System.Collections.Generic;
using System.Linq;
using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories
{
    public abstract class Repository<T> where T : class
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

        public List<TResult> GetUnique<TResult>(Func<T, TResult> gettinResultExpression)
        {
            return Table.Select(gettinResultExpression).Distinct().ToList();
        }

        public List<T> GetAll()
        {
            return Table.ToList();
        }

        public T Get(object id)
        {
            return Table.Find(id);
        }

        public List<T> GetWhere(Func<T, bool> whereExpression)
        {
            return Table.Where(whereExpression).ToList();
        }

        public T FirstOrDefault(Func<T, bool> whereExpression)
        {
            return Table.FirstOrDefault(whereExpression);
        }

        public int Count(Func<T, bool> whereExpression)
        {
            return Table.Count(whereExpression);
        }

        public virtual UserInfo[] SortByField(string fieldName, bool ascending)
        {
            return null;
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
