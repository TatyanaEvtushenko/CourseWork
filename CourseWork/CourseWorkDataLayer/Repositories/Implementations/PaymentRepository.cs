using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories.Implementations
{
    public class PaymentRepository : Repository<Payment>
    {
        public PaymentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<Payment> Table => DbContext.Payments;

        public override object GetIdentificator(Payment item) => item.Id;
    }
}
