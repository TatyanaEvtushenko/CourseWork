using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories.Implementations.Implementations
{
    public class TagRepository : Repository<Tag>
    {
        public TagRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<Tag> Table => DbContext.Tags;

        public override object GetIdentificator(Tag item) => new {item.Name, item.ProjectId};
    }
}