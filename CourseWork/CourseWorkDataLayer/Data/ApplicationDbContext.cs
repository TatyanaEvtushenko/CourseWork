using CourseWork.DataLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FinancialPurpose> FinancialPurposes { get; set; }
        public DbSet<FinancialPurposeInProject> FinancialPurposeInProjects { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectSubscriber> ProjectSubscribers { get; set; }
        public DbSet<Raiting> Raitings { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagInProject> TagInProjects { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
