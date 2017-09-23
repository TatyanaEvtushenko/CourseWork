using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories.Implementations.Implementations
{
    public class ProjectSubscriberRepository : Repository<ProjectSubscriber>
    {
        public ProjectSubscriberRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<ProjectSubscriber> Table => DbContext.ProjectSubscribers;

        public override object GetIdentificator(ProjectSubscriber item) => new {item.UserName, item.ProjectId};
    }
}
