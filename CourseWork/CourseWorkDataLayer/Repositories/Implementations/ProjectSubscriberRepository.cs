using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories.Implementations
{
    public class ProjectSubscriberRepository : Repository<ProjectSubscriber>
    {
        public ProjectSubscriberRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<ProjectSubscriber> Table => DbContext.ProjectSubscribers;

        protected override string GetIdentificator(ProjectSubscriber item) => item.Id;
    }
}
