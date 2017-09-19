using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories.Implementations
{
    public class RatingRepository : Repository<Rating>
    {
        public RatingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<Rating> Table => DbContext.Ratings;

        public override object GetIdentificator(Rating item) => new {item.UserName, item.ProjectId};
    }
}
