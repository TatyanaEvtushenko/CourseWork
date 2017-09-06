using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories.Implementations
{
    public class NewsRepository: Repository<News>
    {
        protected override DbSet<News> Table => DbContext.News;

        protected override string GetIdentificator(News item) => item.Id;

        public NewsRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
