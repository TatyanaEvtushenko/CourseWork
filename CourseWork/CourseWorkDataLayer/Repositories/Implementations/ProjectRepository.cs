using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories.Implementations
{
    public class ProjectRepository : Repository<Project>
    {
        public ProjectRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<Project> Table => DbContext.Projects;

        protected override string GetIdentificator(Project item) => item.Id;
    }
}
