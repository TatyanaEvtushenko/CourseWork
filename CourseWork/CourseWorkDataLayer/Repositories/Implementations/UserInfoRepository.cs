using CourseWork.DataLayer.Data;
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
    }
}
