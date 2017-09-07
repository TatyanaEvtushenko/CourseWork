using System.Collections.Generic;
using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories.Implementations
{
    public class ApplicationUserRepository : Repository<ApplicationUser>
    {
        public ApplicationUserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<ApplicationUser> Table => DbContext.Users;

        protected override string GetIdentificator(ApplicationUser item) => item.Id;

        public Dictionary<string, string> GetIdUsernameDictionary()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            IEnumerable<ApplicationUser> users = GetAll();
            foreach (var user in users)
            {
                result.Add(user.Id, user.UserName);
            }
            return result;
        }
    }
}