using System.Linq;
using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Dictionaries;
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

        public override UserInfo[] SortByField(string fieldName, bool ascending)
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
