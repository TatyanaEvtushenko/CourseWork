using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories.Implementations
{
    public class TagInProjectRepository : Repository<TagInProject>
    {
        public TagInProjectRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<TagInProject> Table => DbContext.TagInProjects;

        protected override string GetIdentificator(TagInProject item) => item.Id;
    }
}
