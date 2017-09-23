using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories.Implementations.Implementations
{
    public class CommentRepository : Repository<Comment>
    {
	    public CommentRepository(ApplicationDbContext dbContext) : base(dbContext)
	    {
	    }

	    protected override DbSet<Comment> Table => DbContext.Comments;

        public override object GetIdentificator(Comment item) => item.Id;
	}
}
