using System;
using System.Collections.Generic;
using System.Linq;
using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Dictionaries;
using CourseWork.DataLayer.Enums.Configurations;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories.Implementations
{
    public class UserInfoRepository : Repository<UserInfo>
    {
        public UserInfoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<UserInfo> Table => DbContext.UserInfos;

        protected override string GetIdentificator(UserInfo item) => item.UserName;

        public List<Object> GetUserListItemViewModels(Func<UserInfo, bool> whereExpression)
        {
            var query = from userInfo in Table
                join project in DbContext.Projects on userInfo.UserName equals project.OwnerUserName into userProjects
                where whereExpression.Invoke(userInfo)
                select (Object) new
                {
                    Username = userInfo.UserName,
                    LastLoginTime = userInfo.LastLoginTime.ToString(),
                    RegistrationTime = userInfo.RegistrationTime.ToString(),
                    ProjectNumber = userProjects.Count(),
                    Raiting = userInfo.Raiting.ToString(),
                    Status = EnumConfiguration.StatusDisplayNames[userInfo.Status],
                    StatusCode = (int)userInfo.Status,
                    IsBlocked = userInfo.IsBlocked
                };
            return query.ToList();
        }

        public Object GetDisplayableInfo(string userName)
        {
            var query = from userInfo in Table
                join project in DbContext.Projects on userInfo.UserName equals project.OwnerUserName into userProjects
                where userInfo.UserName.Equals(userName)
                select (Object) new
                {
                    UserName = userInfo.UserName,
                    RegistrationTime = userInfo.RegistrationTime,
                    Avatar = userInfo.Avatar,
                    About = userInfo.About,
                    ProjectNumber = userProjects.Count()
                };
            return query.SingleOrDefault();
        }

        public UserInfo[] SortByField(string fieldName, bool ascending)
        {
            return ascending ? SortByFieldAscending(fieldName) : SortByFieldDescending(fieldName);
        }

        private UserInfo[] SortByFieldAscending(string fieldName)
        {
            return UserInfoFieldNamesDictionary.UserInfoFieldNames.ContainsKey(fieldName)
                ? Table.OrderBy(UserInfoFieldNamesDictionary.UserInfoFieldNames[fieldName]).ToArray()
                : null;
        }

        private UserInfo[] SortByFieldDescending(string fieldName)
        {
            return UserInfoFieldNamesDictionary.UserInfoFieldNames.ContainsKey(fieldName)
                ? Table.OrderByDescending(UserInfoFieldNamesDictionary.UserInfoFieldNames[fieldName]).ToArray()
                : null;
        }
    }
}
