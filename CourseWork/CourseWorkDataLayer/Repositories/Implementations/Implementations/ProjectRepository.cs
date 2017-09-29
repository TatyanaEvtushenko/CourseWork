using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories.Implementations.Implementations
{
    public class ProjectRepository : Repository<Project>
    {
        public ProjectRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<Project> Table => DbContext.Projects;

        public override object GetIdentificator(Project item) => item.Id;
    }
}