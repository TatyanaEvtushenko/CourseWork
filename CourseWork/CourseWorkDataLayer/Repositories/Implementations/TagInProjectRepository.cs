using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;

namespace CourseWork.DataLayer.Repositories.Implementations
{
    public class TagInProjectRepository : IRepository<TagInProject>
    {
        private readonly ApplicationDbContext _dbContext;

        public TagInProjectRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddRange(params TagInProject[] items)
        {
            try
            {
                _dbContext.TagInProjects.AddRange(items);
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
                var items = _dbContext.TagInProjects.Where(item => identificators.Contains(item.Id));
                _dbContext.TagInProjects.RemoveRange(items);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<TagInProject> GetAll()
        {
            return _dbContext.TagInProjects.ToList();
        }

        public TagInProject Get(string id)
        {
            return _dbContext.TagInProjects.Find(id);
        }

        public List<TagInProject> GetWhere(Expression<Func<TagInProject, bool>> whereExpression)
        {
            return _dbContext.TagInProjects.Where(whereExpression).ToList();
        }
    }
}
