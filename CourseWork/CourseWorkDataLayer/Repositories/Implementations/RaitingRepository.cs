using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories.Implementations
{
    public class RaitingRepository : Repository<Raiting>
    {
        public RaitingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<Raiting> Table => DbContext.Raitings;

        protected override string GetIdentificator(Raiting item) => item.Id;
    }
}
