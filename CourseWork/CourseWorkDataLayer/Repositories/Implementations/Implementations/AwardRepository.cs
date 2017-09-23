using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories.Implementations.Implementations
{
    public class AwardRepository : Repository<Award>
    {
        public AwardRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override object GetIdentificator(Award item) => new {item.UserName, item.AwardType};

        protected override DbSet<Award> Table => DbContext.Awards;
    }
}
