using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Dictionaries;
using CourseWork.DataLayer.Enums.Configurations;
using CourseWork.DataLayer.Migrations;
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

        public override object GetIdentificator(UserInfo item) => item.UserName;

        public Object[] GetDisplayableInfo(string[] userNames)
        {
            var userNamesSet = userNames.ToImmutableHashSet();
            var query = from userInfo in Table
                join project in DbContext.Projects on userInfo.UserName equals project.OwnerUserName into userProjects
                where userNamesSet.Contains(userInfo.UserName)
                select (Object) new
                {
                    UserName = userInfo.UserName,
                    RegistrationTime = userInfo.RegistrationTime,
                    Avatar = userInfo.Avatar,
                    About = userInfo.About,
                    ProjectNumber = userProjects.Count(),
                    Contacts = userInfo.Contacts
                };
            return query.ToArray();
        }

        public UserInfo[] SortByField(string fieldName, bool ascending, Func<UserInfo, bool> filterRequest)
        {
            return ascending ? SortByFieldAscending(fieldName, filterRequest) : SortByFieldDescending(fieldName, filterRequest);
        }

        private UserInfo[] SortByFieldAscending(string fieldName, Func<UserInfo, bool> filterRequest)
        {
            return UserInfoFieldNamesDictionary.UserInfoFieldNames.ContainsKey(fieldName)
                ? GetWhereEager(filterRequest, item => item.Projects).OrderBy(UserInfoFieldNamesDictionary.UserInfoFieldNames[fieldName]).ToArray()
                : null;
        }

        private UserInfo[] SortByFieldDescending(string fieldName, Func<UserInfo, bool> filterRequest)
        {
            return UserInfoFieldNamesDictionary.UserInfoFieldNames.ContainsKey(fieldName)
                ? GetWhereEager(filterRequest, item => item.Projects).OrderByDescending(UserInfoFieldNamesDictionary.UserInfoFieldNames[fieldName]).ToArray()
                : null;
        }
    }
}
