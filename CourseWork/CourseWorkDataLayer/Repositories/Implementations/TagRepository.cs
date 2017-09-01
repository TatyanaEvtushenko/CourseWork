using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;

namespace CourseWork.DataLayer.Repositories.Implementations
{
    public class TagRepository : IRepository<Tag>
    {
        private readonly ApplicationDbContext _dbContext;

        public TagRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddRange(params Tag[] items)
        {
            try
            {
                _dbContext.Tags.AddRange(items);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool RemoveRange(params string[] identificators)
        {
            try
            {
                var items = _dbContext.Tags.Where(item => identificators.Contains(item.Id));
                _dbContext.Tags.RemoveRange(items);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Tag> GetAll()
        {
            return _dbContext.Tags.ToList();
        }

        public Tag Get(string id)
        {
            return _dbContext.Tags.Find(id);
        }

        public List<Tag> GetWhere(Expression<Func<Tag, bool>> whereExpression)
        {
            return _dbContext.Tags.Where(whereExpression).ToList();
        }
    }
}
