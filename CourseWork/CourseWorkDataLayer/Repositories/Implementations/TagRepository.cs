using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories.Implementations
{
    public class TagRepository : Repository<Tag>
    {
        public TagRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<Tag> Table => DbContext.Tags;

        protected override string GetIdentificator(Tag item) => item.Id;
    }
}
