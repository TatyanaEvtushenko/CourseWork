﻿using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Repositories.Implementations.Implementations
{
    public class ApplicationUserRepository : Repository<ApplicationUser>
    {
        public ApplicationUserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<ApplicationUser> Table => DbContext.Users;

        public override object GetIdentificator(ApplicationUser item) => item.Id;
    }
}