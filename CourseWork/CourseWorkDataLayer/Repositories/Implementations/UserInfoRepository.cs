using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories.Implementations
{
    public class UserInfoRepository : IRepository<UserInfo>
    {
        private readonly ApplicationDbContext _dbContext;

        public UserInfoRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddRange(params UserInfo[] items)
        {
            try
            {
                _dbContext.UserInfos.AddRange(items);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveRange(params string[] identificators)
        {
            try
            {
                var items = _dbContext.UserInfos.Where(item => identificators.Contains(item.UserId));
                _dbContext.UserInfos.RemoveRange(items);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<UserInfo> GetAll()
        {
            return _dbContext.UserInfos.ToList();
        }

        public UserInfo Get(string id)
        {
            return _dbContext.UserInfos.Find(id);
        }

        public List<UserInfo> GetWhere(Expression<Func<UserInfo, bool>> whereExpression)
        {
            return _dbContext.UserInfos.Where(whereExpression).ToList();
        }

        public bool UpdateRange(params UserInfo[] items)
        {
            try
            {
                foreach (var item in items)
                {
                    _dbContext.Entry(item).State = EntityState.Modified;
                }
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
