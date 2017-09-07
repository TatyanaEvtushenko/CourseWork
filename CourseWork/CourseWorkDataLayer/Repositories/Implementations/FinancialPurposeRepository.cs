using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories.Implementations
{
    public class FinancialPurposeRepository : Repository<FinancialPurpose>
    {
        public FinancialPurposeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<FinancialPurpose> Table => DbContext.FinancialPurposes;

        protected override string GetIdentificator(FinancialPurpose item) => item.Id;
    }
}
