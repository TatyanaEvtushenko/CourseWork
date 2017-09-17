using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories.Implementations
{
    public class MessageRepository : Repository<Message>
    {
	    public MessageRepository(ApplicationDbContext dbContext) : base(dbContext)
	    {
	    }

	    protected override DbSet<Message> Table => DbContext.Messages;

		public override object GetIdentificator(Message item) => item.Id;
    }
}
