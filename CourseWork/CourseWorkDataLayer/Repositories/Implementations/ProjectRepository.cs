using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;

namespace CourseWork.DataLayer.Repositories.Implementations
{
    public class ProjectRepository : IRepository<Project>
    {
        private readonly ApplicationDbContext _dbContext;

        public ProjectRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddRange(params Project[] items)
        {
            try
            {
                _dbContext.Projects.AddRange(items);
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
                var items = _dbContext.Projects.Where(item => identificators.Contains(item.Id));
                _dbContext.Projects.RemoveRange(items);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Project> GetAll()
        {
            return _dbContext.Projects.ToList();
        }

        public Project Get(string id)
        {
            return _dbContext.Projects.Find(id);
        }

        public List<Project> GetWhere(Expression<Func<Project, bool>> whereExpression)
        {
            return _dbContext.Projects.Where(whereExpression).ToList();
        }
    }
}
